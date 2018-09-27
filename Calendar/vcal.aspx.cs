/// <license>TUSTENA PUBLIC LICENSE v1.0</license>
/// <copyright>
/// Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
///
/// Tustena CRM is a trademark of:
/// Digita S.r.l.
/// Viale Enrico Fermi 14/z
/// 31011 Asolo (Italy)
/// Tel. +39-0423-951251
/// Mail. info@digita.it
///
/// This file contains Original Code and/or Modifications of Original Code
/// as defined in and that are subject to the Tustena Public Source License
/// Version 1.0 (the 'License'). You may not use this file except in
/// compliance with the License. Please obtain a copy of the License at
/// http://www.tustena.com/TPL/ and read it before using this
// file.
///
/// The Original Code and all software distributed under the License are
/// distributed on an 'AS IS' basis, WITHOUT WARRANTY OF ANY KIND, EITHER
/// EXPRESS OR IMPLIED, AND DIGITA S.R.L. HEREBY DISCLAIMS ALL SUCH WARRANTIES,
/// INCLUDING WITHOUT LIMITATION, ANY WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE, QUIET ENJOYMENT OR NON-INFRINGEMENT.
/// Please see the License for the specific language governing rights and
/// limitations under the License.
///
/// YOU MAY NOT REMOVE OR ALTER THIS COPYRIGHT NOTICE!
/// </copyright>

using System;
using System.Data;
using System.Text;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class vCalExport : G
	{

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion
		protected void Page_Load(Object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				DataTable dr;
				switch (Request.Params["mode"])
				{
					case "single":
						dr = DatabaseConnection.CreateDataset(String.Format("SELECT TOP 1 ID,STARTDATE,ENDDATE,CONTACT,NOTE,ADDRESS,CITY,PROVINCE,CAP,PLACE FROM BASE_CALENDAR WHERE ID={0}", int.Parse(Request.Params["id"]))).Tables[0];
						break;
					case "month":
						dr = DatabaseConnection.CreateDataset(String.Format("SELECT ID,STARTDATE,ENDDATE,CONTACT,NOTE,ADDRESS,CITY,PROVINCE,CAP,PLACE FROM BASE_CALENDAR WHERE MONTH(STARTDATE)={0} AND YEAR(STARTDATE)={3} AND UID={2}", Request.Params["month"], UC.UserId, Request.Params["year"])).Tables[0];
						break;
					default:
						dr = DatabaseConnection.CreateDataset(String.Format("SELECT ID,STARTDATE,ENDDATE,CONTACT,NOTE,ADDRESS,CITY,PROVINCE,CAP,PLACE FROM BASE_CALENDAR WHERE UID={0}", UC.UserId)).Tables[0];
						break;
				}


				Response.Clear();
				Response.ContentType = "text/x-vCalendar";
				Response.AddHeader("Content-Disposition", "filename=vCal" + DateTime.Now.ToString(@"yyMMddHHmmss").ToString() + ".ics");
				vCalendar cal = new vCalendar();
				foreach (DataRow dd in dr.Rows)
				{
					vCalendar.vEvent evt = new vCalendar.vEvent();
					evt.UID = "vCal" + dd[0].ToString();
					evt.DTStart = Convert.ToDateTime(dd[1]);
					evt.DTEnd = Convert.ToDateTime(dd[2]);
					evt.Summary = dd[3].ToString();
					evt.URL = String.Empty;
					evt.description = dd[4].ToString().Replace(Environment.NewLine, "");
					if ((bool) dd[9])
					{
						evt.Location = "in sede";
					}
					else
					{
						StringBuilder location = new StringBuilder();
						if (dd[5].ToString().Length > 0)
							location.AppendFormat("{0}, ", dd[5].ToString());
						if (dd[6].ToString().Length > 0)
							location.AppendFormat("{0} ", dd[6].ToString());
						if (dd[7].ToString().Length > 0)
							location.AppendFormat("({0})", dd[7].ToString());
						if (location.Length == 0) location.Append("n/a");
						evt.Location = location.ToString();
					}
					evt.Organizer = "robot@tustena.com";
					cal.Events.Add(evt);
				}
				Response.Write(cal.ToString());
				Response.End();

			}
		}
	}

}
