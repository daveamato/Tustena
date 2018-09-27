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
using System.Web;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class QuickLogView : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nologin","<script>parent.location.href=parent.location.href;</script>");
			}
			else
			{
				DataTable dt;
dt = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM QUICKLOG WHERE OWNERID={0} ORDER BY ID DESC",UC.UserId)).Tables[0];
				StringBuilder sb = new StringBuilder();
				sb.Append("<table cellpadding=0 cellspacing=0 class=normal width=\"100%\">");
				foreach(DataRow dr in dt.Rows)
				{
					string contact = string.Empty;
					switch((byte)dr["tableid"])
					{
						case 0:
							contact = DatabaseConnection.SqlScalar("SELECT 'C'+CAST(ID AS VARCHAR)+'|'+ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') AS CONTACT FROM BASE_CONTACTS WHERE ID=" + dr["contactid"]);
							break;
						case 1:
							contact = DatabaseConnection.SqlScalar("sELECT 'L'+CAST(ID AS VARCHAR)+'|'+ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') AS CONTACT FROM CRM_LEADS WHERE ID=" + dr["contactid"]);
							break;
						case 2:
							contact = DatabaseConnection.SqlScalar("SELECT 'A'+CAST(ID AS VARCHAR)+'|'+COMPANYNAME AS CONTACT FROM BASE_COMPANIES WHERE ID=" + dr["contactid"]);
							break;
					}
					if(contact.Length>0)
					sb.AppendFormat("<tr><td style=\"cursor:pointer;border-bottom:1px solid black;\" onclick=\"parent.refreshactivity('{1}','ViewActivity')\">{0}</td></tr>",contact.Split('|')[1],contact.Split('|')[0]);
				}
				sb.Append("</table>");
				LtrContact.Text=sb.ToString();
			}
			Response.Cache.SetCacheability(HttpCacheability.NoCache);
		}



		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}
