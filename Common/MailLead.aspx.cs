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
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class MailLead : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				Session["leadid"] = Request.QueryString["id"];
			}
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.Submitbtn.Click += new EventHandler(Submitbtn_Click);
		}

		#endregion

		private void Submitbtn_Click(object sender, EventArgs e)
		{
			DataRow drLead = DatabaseConnection.CreateDataset("SELECT * FROM LEADFORMAIL_VIEW WHERE ID=" + int.Parse(Session["leadid"].ToString())).Tables[0].Rows[0];
			StringBuilder ml = new StringBuilder();
			ml.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"2\" width=\"100%\" align=\"center\">");
			ml.AppendFormat("<tr><td>{0}</td></tr>", MailMessage.Text);
			ml.Append("</table><br>");
			ml.Append("<table border=\"0\" cellpadding=\"3\" cellspacing=\"2\" width=\"70%\" align=\"center\" style=\"border: 1px solid #000000;FONT-SIZE: 10px; FONT-FAMILY: Verdana\">");
			ml.Append("<tr>");
			ml.Append("<td style=\"border-bottom: 1px solid #000000;\" colspan=2>");
			ml.Append("<BR>");
			ml.AppendFormat("<b>{0}</b>",Root.rm.GetString("Ledtxt2"));
			ml.Append("</td>");
			ml.Append("</tr>");
			ml.Append("<tr>");
			ml.AppendFormat("<td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt15"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td>", drLead["Name"].ToString());


			ml.AppendFormat("</tr><tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt16"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["Surname"].ToString());

			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt17"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["CompanyName"].ToString());

			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Ledtxt18"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM COMPANYTYPE WHERE K_ID=" + drLead["industry"].ToString() + " AND LANG='" + UC.CultureSpecific + "'"));

			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt18"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["BusinessRole"].ToString());

			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Ledtxt5"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["phone"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Ledtxt6"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["MobilePhone"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt46"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["Fax"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt25"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["Email"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt45"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", string.Format("{0:d}", drLead["BirthDay"].ToString()));
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt49"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["BirthPlace"].ToString());

			ml.AppendFormat("<tr><td style=\"border-bottom: 1px solid #000000;\" colspan=2><BR><b>{0}</b></td></tr>",Root.rm.GetString("Ledtxt3"));
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt28"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["Address"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt29"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["City"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt30"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["Province"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt53"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["State"].ToString());
			ml.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>",Root.rm.GetString("Reftxt31"));
			ml.AppendFormat("<td bgcolor=\"#dddddd\">{0}</td></tr>", drLead["ZIPCode"].ToString());

			ml.Append("<tr><td colspan=2>");
string sql;
sql = "SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + (byte) CRMTables.CRM_Leads + " ORDER BY VIEWORDER";
			StringBuilder S = new StringBuilder();
			DataSet ds = DatabaseConnection.CreateDataset(sql);
			if (ds.Tables[0].Rows.Count > 0)
			{
				S.AppendFormat("<table width=\"100%\"><tr><td style=\"border-bottom: 1px solid #000000;FONT-SIZE: 10px; FONT-FAMILY: Verdana\"><br><b>{0}</b></td></tr></table>",Root.rm.GetString("Bcotxt50"));
				S.Append("<table width=\"100%\" style=\"FONT-SIZE: 10px; FONT-FAMILY: Verdana\">");
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					bool isParentCrossed = true;
					DataSet Q = DatabaseConnection.CreateDataset("SELECT PARENTFIELD,PARENTFIELDVALUE FROM ADDEDFIELDS WHERE ID=" + dr["id"].ToString());
					string pf = Q.Tables[0].Rows[0][0].ToString();
					string pfv = Q.Tables[0].Rows[0][1].ToString();
					if (pf.Length > 0)
					{
						IDataReader sqlDr = DatabaseConnection.CreateReader("SELECT " + pf + " FROM CRM_LEADS WHERE ID=" + int.Parse(Session["leadid"].ToString()));
						sqlDr.Read();
						isParentCrossed = (sqlDr[0].ToString() == pfv);
						sqlDr.Close();
					}

					if (isParentCrossed)
					{
						S.AppendFormat("<tr><td width=\"40%\" bgcolor=\"#aaaaaa\">{0}</td>", dr["name"]);
						DataSet afCross = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + dr["id"] + " AND ID=" + int.Parse(Session["leadid"].ToString()));
						if (afCross.Tables[0].Rows.Count > 0)
						{
							S.AppendFormat("<td  bgcolor=\"#dddddd\" style=\"FONT-SIZE: 10px; FONT-FAMILY: Verdana\">{0}&nbsp;</td></tr>", afCross.Tables[0].Rows[0][0]);
						}
						else
						{
							S.Append("<td  bgcolor=\"#dddddd\" style=\"FONT-SIZE: 10px; FONT-FAMILY: Verdana\">&nbsp;</td></tr>");
						}

						afCross.Clear();
					}
				}
				S.Append("</table>");
			}

			ml.AppendFormat("{0}</td></tr>", S.ToString());
			ml.Append("</table>");

			DataTable dtac = DatabaseConnection.CreateDataset("SELECT TYPE,ACTIVITYDATE,STATE,OWNERID,SUBJECT,DESCRIPTION FROM CRM_WORKACTIVITY WHERE LEADID=" + int.Parse(Session["leadid"].ToString())).Tables[0];
			if (dtac.Rows.Count > 0)
			{
				ml.AppendFormat("<br><table width=\"70%\" style=\"border: 1px solid #000000\" align=center><tr><td style=\"border-bottom: 1px solid #000000;FONT-SIZE: 10px; FONT-FAMILY: Verdana\"><br><b>{0}</b></td></tr>",Root.rm.GetString("Mletxt6"));
				foreach (DataRow drac in dtac.Rows)
				{
					ml.Append("<tr><td><table cellpadding=0 cellspacing=0 style=\"border: 1px solid #000000;FONT-SIZE: 10px; FONT-FAMILY: Verdana\">");

					switch (Convert.ToInt16(drac["type"]))
					{
						case 1:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt6"), drac["subject"].ToString());
							break;
						case 2:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt7"), drac["subject"].ToString());
							break;
						case 3:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt8"), drac["subject"].ToString());
							break;
						case 4:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt9"), drac["subject"].ToString());
							break;
						case 5:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt10"), drac["subject"].ToString());
							break;
						case 6:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt11"), drac["subject"].ToString());
							break;
						case 7:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt12"), drac["subject"].ToString());
							break;
						case 8:
							ml.AppendFormat("<tr><td bgcolor=\"#dddddd\">{0}:{1}</td></tr>",Root.rm.GetString("Wortxt13"), drac["subject"].ToString());
							break;
					}
					ml.AppendFormat("<tr><td bgcolor=\"#ffffff\">{0}:{1}</td></tr>",Root.rm.GetString("Acttxt38"),UC.LTZ.ToLocalTime(Convert.ToDateTime(drac["activitydate"],UC.myDTFI)));
					ml.AppendFormat("<tr><td bgcolor=\"#ffffff\">{0}:{1}</td></tr>",Root.rm.GetString("Acttxt77"),Root.rm.GetString("Wortxt" + drac["state"].ToString()));
					ml.AppendFormat("<tr><td bgcolor=\"#ffffff\">{0}:{1}</td></tr>",Root.rm.GetString("Acttxt65"), DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE ID=" + drac["ownerid"].ToString()));
					ml.AppendFormat("<tr><td bgcolor=\"#ffffff\">{0}:{1}</td></tr>",Root.rm.GetString("Acttxt2"), drac["description"].ToString());
					ml.Append("</table>");
					ml.Append("</td></tr>");

				}

				ml.Append("</table>");
			}

			MessagesHandler.SendMail(MailAddress.Text,UC.MailingAddress,"[Tustena] " + MailObject.Text,ml.ToString());

			Session.Remove("leadid");
			ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" +Root.rm.GetString("Mletxt5") + "');self.close();parent.HideBox();</script>");
		}
	}
}
