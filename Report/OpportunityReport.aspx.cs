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
using System.IO;
using System.Text;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.report
{
	public partial class OpportunityReport : G
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (Login())
			{
				string id = Request.Params["id"];
				StringBuilder qo = new StringBuilder();

				qo.AppendFormat("SELECT TITLE AS [{0}],",Root.rm.GetString("CRMopptxt10"));
				qo.AppendFormat("OWNER AS [{0}],",Root.rm.GetString("CRMopptxt11"));
				qo.AppendFormat("AMOUNTCLOSED AS [{0}],",Root.rm.GetString("CRMopptxt13"));
				qo.AppendFormat("EXPECTEDREVENUE AS [{0}],",Root.rm.GetString("CRMopptxt14"));
				qo.AppendFormat("CREATEDBY AS [{0}],",Root.rm.GetString("CRMopptxt15"));
				qo.AppendFormat("LASTMODIFIEDBY AS [{0}],",Root.rm.GetString("CRMopptxt16"));
				qo.AppendFormat("DESCRIPTION AS [{0}] ",Root.rm.GetString("CRMopptxt17"));
				qo.Append("FROM CRM_OPPORTUNITY_VIEW WHERE ID=@ID");
				DataTable d = DatabaseConnection.SecureCreateDataset(qo.ToString(), new DbSqlParameter("@ID", id)).Tables[0];

				htmlcontact(d, false, 0,Root.rm.GetString("Prnopptxt9"));

				if (Request.Params["p"][0] == '1')
				{
					StringBuilder SqlCompanies = new StringBuilder();
					SqlCompanies.Append("SELECT ");
					SqlCompanies.AppendFormat("BASE_COMPANIES.COMPANYNAME AS [{0}],",Root.rm.GetString("CRMopptxt24").ToUpper());
					SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					SqlCompanies.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt3").ToUpper());
					SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					SqlCompanies.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt4").ToUpper());
					SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					SqlCompanies.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 3 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt28"));
					SqlCompanies.Append("CRM_OPPORTUNITYCONTACT.NOTE AS [NOTE] ");
					SqlCompanies.Append("FROM CRM_OPPORTUNITYCONTACT INNER JOIN ");
					SqlCompanies.AppendFormat("BASE_COMPANIES ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID WHERE CRM_OPPORTUNITYCONTACT.CONTACTTYPE=0 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID=@ID");

					Trace.Warn("az", SqlCompanies.ToString());
					DataTable az = DatabaseConnection.SecureCreateDataset(SqlCompanies.ToString(), new DbSqlParameter("@ID", id)).Tables[0];
					if (az.Rows.Count > 0)
						HtmlList(az, false,Root.rm.GetString("Prnopptxt2").ToUpper());
				}

				if (Request.Params["p"][1] == '1')
				{
					StringBuilder sqlLead = new StringBuilder();
					sqlLead.Append("SELECT ");
					sqlLead.AppendFormat("CRM_LEADS.COMPANYNAME AS [{0}],",Root.rm.GetString("CRMopptxt24").ToUpper());
					sqlLead.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					sqlLead.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					sqlLead.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					sqlLead.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt3").ToUpper());
					sqlLead.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					sqlLead.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					sqlLead.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					sqlLead.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt4").ToUpper());
					sqlLead.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
					sqlLead.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
					sqlLead.AppendFormat("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID AND CRM_OPPORTUNITYTABLETYPE.LANG='{0}' ",UC.Culture.Substring(0,2));
					sqlLead.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 3 AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS [{1}],", id,Root.rm.GetString("CRMopptxt28"));
					sqlLead.Append("CRM_OPPORTUNITYCONTACT.NOTE AS [NOTE] ");
					sqlLead.Append("FROM CRM_OPPORTUNITYCONTACT LEFT OUTER JOIN ");
					sqlLead.AppendFormat("CRM_LEADS ON CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_LEADS.ID WHERE CRM_OPPORTUNITYCONTACT.CONTACTTYPE=1 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID=@ID");


					DataTable az = DatabaseConnection.SecureCreateDataset(sqlLead.ToString(),new DbSqlParameter("@ID",id)).Tables[0];
					if (az.Rows.Count > 0)
						HtmlList(az, false,Root.rm.GetString("Prnopptxt3").ToUpper());
				}

				if (Request.Params["p"][2] == '1')
				{
					StringBuilder sqlCompetitor = new StringBuilder();
					sqlCompetitor.Append("SELECT ");

					DataTable cm = DatabaseConnection.CreateDataset(sqlCompetitor.ToString()).Tables[0];
					if (cm.Rows.Count > 0)
						HtmlList(cm, true,Root.rm.GetString("Prnopptxt12"));
				}

				Response.Write("<body>");
				Response.Write(report);
				Response.Write("</body>");
			}
		}

		private string report;

		public string getReport
		{
			get
			{
				if (report == null || report.Trim().Length == 0)
				{
					try
					{
						TextReader tr = File.OpenText(Path.Combine(Request.PhysicalApplicationPath, @"template\print\htmlformtemplate.htm"));
						report = tr.ReadToEnd();
						tr.Close();
					}
					catch (Exception ex)
					{
						return ex.ToString();
					}
				}
				return report;
			}
			set { report = value; }
		}

		private void htmlcontact(DataTable dt, bool finalize, int itempage, string qbdesc)
		{
			StringBuilder sb = new StringBuilder(report);
			StringBuilder sb2 = new StringBuilder();
			int total = dt.Columns.Count;
			int ListBreak = 0;
			if (itempage > 0)
			{
				if (total > itempage)
					total = (total%2 == 0) ? total/2 : total/2 + 1;
			}
			sb2.AppendFormat(@"<div class=""TableTitle"">&nbsp;{0}</div>", qbdesc);

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				sb2.Append(@"<table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Container""><tr><td valign=top><table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Internal"">");
				foreach (DataColumn cc in dt.Columns)
				{
					sb2.AppendFormat(@"<tr><td valign=top width=""15%"" nowrap class=""NameField"">{0}:&nbsp;</td><td valign=top class=""DataField"">{1}</td></tr>", cc.ColumnName, dt.Rows[i][cc.ColumnName]);
					if (itempage > 0)
					{
						if (total == (ListBreak++))
							sb2.Append(@"</table></td><td width=""50%"" valign=top><table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Internal"">");
					}
				}
				sb2.Append("</table></tr></td></table>");
			}

			sb.Replace("<!CODE!>", sb2.ToString());
			sb2 = null;
			if (!finalize)
				sb.Append("<!CODE!>");
			report = sb.ToString();
			sb = null;
		}


		private void HtmlList(DataTable dt, bool finalize, string qbdesc)
		{
			StringBuilder sb = new StringBuilder(report);
			StringBuilder sb2 = new StringBuilder();

			sb2.AppendFormat(@"<div class=""TableTitle"">&nbsp;{0}</div>", qbdesc);
			sb2.Append(@"<table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Container""><tr><td valign=top><table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Internal""><tr>");
			foreach (DataColumn cc in dt.Columns)
			{
				sb2.AppendFormat(@"<td valign=top width=""15%"" nowrap class=""NameField NameList"">{0}&nbsp;</td>", cc.ColumnName);
			}
			sb2.Append(@"</tr>");
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				sb2.Append(@"<tr>");
				foreach (DataColumn cc in dt.Columns)
				{
					sb2.AppendFormat(@"<td valign=top class=""DataField"">{0}&nbsp;</td>", dt.Rows[i][cc.ColumnName]);
				}
				sb2.Append(@"</tr>");
			}
			sb2.Append("</table>");
			sb.Replace("<!CODE!>", sb2.ToString());
			sb2 = null;
			if (!finalize)
				sb.Append("<!CODE!>");
			report = sb.ToString();
			sb = null;
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
		}

		#endregion
	}
}
