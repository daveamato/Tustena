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
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class AppVerify : G
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

		public void Page_Load(object sender, EventArgs e)
		{
			if (Login())
			{
				if (Request.Params["id"].ToString().IndexOf("|") > 0)
				{
					Grid.Text = "<table width=\"100%\" cellspacing=1 cellpadding=0><tr><td width=\"50%\" valign=top>" +
						FillDay(Convert.ToDateTime(Request.Params["date"], UC.myDTFI), int.Parse(Request.Params["id"].ToString().Split('|')[0])) +
						"</td><td width=\"50%\" valign=top>" +
						FillDay(Convert.ToDateTime(Request.Params["date"], UC.myDTFI), int.Parse(Request.Params["id"].ToString().Split('|')[1])) +
						"</td></tr></table>";
				}
				else
					Grid.Text = FillDay(Convert.ToDateTime(Request.Params["date"], UC.myDTFI), int.Parse(Request.Params["id"].ToString()));
			}
			DeleteGoBack();
		}

		private string FillDay(DateTime data, int uIDATTUALE)
		{
			string sqlCalendar = "SELECT ID,RECURRID,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR,CONTACT,COMPANY,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE (CONVERT(VARCHAR(10),STARTDATE,112)='" + data.ToString(@"yyyyMMdd") + "' OR RECURRID>0) AND UID=@UID";

			Trace.Warn("SQL", sqlCalendar);
			DataSet dsCalendar = DatabaseConnection.SecureCreateDataset(sqlCalendar, new DbSqlParameter("@UID", uIDATTUALE));
			DataTable CalendarDataTable = dsCalendar.Tables[0];

			StringBuilder gg = new StringBuilder();
			StringBuilder descTable = new StringBuilder();

			DateTime Utctime =UC.LTZ.ToUniversalTime(data);
			TimeSpan mindiffstart =  new TimeSpan(data.Ticks-Utctime.Ticks);

			int hTimeZone = Convert.ToInt32(mindiffstart.TotalHours);

			DataColumn dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "isRec";
			dcDynColumn.DataType = Type.GetType("System.Byte");
			dcDynColumn.DefaultValue = 0;
			CalendarDataTable.Columns.Add(dcDynColumn);

			int rowsToCicleCal = CalendarDataTable.Rows.Count;

			if (CalendarDataTable.Select("RecurrID>0").Length > 0)
			{
				DateTime MDT = data;
				DateTime MDT2 = MDT.AddSeconds(86399);
				Trace.Warn("MDT", MDT.ToString());
				Trace.Warn("MDT2", MDT2.ToString());

				for (int i = 0; i < rowsToCicleCal; i++)
				{
					Recurrence recurrence = new Recurrence(UC);
					ArrayList AL = recurrence.Remind((int) CalendarDataTable.Rows[i]["recurrid"], MDT, MDT2);

					if (AL.Count > 0)
					{
						foreach (DateTime ALT in AL)
						{
							DataRow row = CalendarDataTable.NewRow();
							for (int i2 = 0; i2 < CalendarDataTable.Columns.Count - 1; i2++)
								row[i2] = CalendarDataTable.Rows[i][i2];
							row["isRec"] = 1;
							row["startdate"] = ALT.Date.Add(((DateTime) row["startdate"]).TimeOfDay);
							row["enddate"] = ALT.Date.Add(((DateTime) row["enddate"]).TimeOfDay);
							row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
							CalendarDataTable.Rows.Add(row);
						}
						if (Convert.ToInt32(((string) CalendarDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != data.Month)
							CalendarDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
					}
				}
			}

			DataRow[] drAppExists = CalendarDataTable.Select("ISOSTARTDATE='" + data.ToString(@"yyyyMMdd") + "'");

			long idNumber = 0;
			gg.AppendFormat("<table><tr><td colspan=6 class=normal>{0}</td></tr><tr>", DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + uIDATTUALE));
			for (int xx = 0; xx <= 23; xx++)
			{
				if (xx%2 == 0)
					gg.Append("<td valign=\"top\" class=\"Grid ");
				else
					gg.Append("<td valign=\"top\" class=\"GridAltern ");


				if (drAppExists.Length > 0)
				{
					bool exists = false;
					string appString = String.Empty;
					foreach (DataRow drSelApp in drAppExists)
					{
						if (((int) drSelApp["starthour"] + hTimeZone) == xx
							&& idNumber != Convert.ToInt32(drSelApp["id"]))
						{
							exists = true;
							appString = "GridApp\">";
						}
						else
						{
							if (((int) drSelApp["starthour"] + hTimeZone) < xx
								&& ((int) drSelApp["endhour"] + hTimeZone) >= xx
								&& idNumber != Convert.ToInt32(drSelApp["id"]))
							{
								exists = true;
								appString = "GridApp\">";
							}
						}

					}
					gg.Append(appString.ToString());
					if (!exists)
						gg.AppendFormat("\">{0}:00</td>", xx.ToString());
					else
						gg.AppendFormat("{0}:00</td>", xx.ToString());

				}
				else
				{
					gg.AppendFormat("\">{0}:00</td>", xx.ToString());
				}

				if (xx == 5 || xx == 11 || xx == 17 || xx == 23)
					gg.Append("</tr><tr>");

			}
			gg.Append("</table>");

			int clnumber = 1;
			foreach (DataRow drSelApp in drAppExists)
			{
				string cl = ((clnumber%2) == 0) ? "GridItem" : "GridItemAltern";
				clnumber++;
				descTable.AppendFormat("<tr><td width=\"1%\" nowrap class={2}>{0}</td><td width=\"1%\" nowrap class={2}>{1}</td>", ((DateTime) drSelApp["startdate"]).AddHours(hTimeZone).ToShortTimeString(), ((DateTime) drSelApp["enddate"]).AddHours(hTimeZone).ToShortTimeString(), cl);
				descTable.AppendFormat("<td width=\"98%\" nowrap class={2}>{0}&nbsp;{1}</td></tr>", drSelApp["contact"].ToString(), drSelApp["company"].ToString(), cl);
			}
			if (descTable.Length > 0)
			{
				gg.AppendFormat("<table class=\"normal\" cellspacing=0 cellpadding=3 width=\"100%\" style=\"border-top:2px solid black;\">{0}</table>", descTable.ToString());
			}
			return gg.ToString();
		}


	}
}
