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
using System.IO;
using System.Text;
using System.Web.UI;
using Digita.Tustena.Database;

namespace Digita.Tustena.report
{
	public partial class HTMLReport : Page
	{
		protected string alertMsg = Core.Root.rm.GetString("Prnopptxt8");
		protected void Page_Load(object sender, EventArgs e)
		{

			QueryBuilderManager qb = new QueryBuilderManager();

			Trace.Warn("pageload","pageload");

			if (Session["report"] != null)
			{
				ArrayList re = new ArrayList();
				re = (ArrayList) Session["report"];

				foreach (object re1 in re)
				{
					CompanyReport cr = new CompanyReport();
					cr = (CompanyReport) re1;
					string qbdesc = String.Empty;
					DataTable d = qb.QBManager(cr.idfield, cr.Params);

					qbdesc = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM QB_CUSTOMERQUERY WHERE ID=" + cr.idfield);

					if (d.Rows.Count > 0)
						switch (cr.Type)
						{
							case 0:
								htmlcontact(d, cr.Finalize, cr.itemPage, qbdesc, cr.morerecord);
								break;
							case 1:
								HtmlList(d, cr.Finalize, qbdesc, cr.morerecord);
								break;
						}
					else
						report = "No data";

					Session["report"] = null;
				}


				Response.Write("<body onload=\"printpage();\">");
				Response.Write(report);
				Response.Write("</body>");
			}
		}

		private string report;

		public string Report
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

		private void htmlcontact(DataTable dt, bool finalize, int itempage, string qbdesc, bool morerecord)
		{
			StringBuilder sb = new StringBuilder(Report);
			StringBuilder sb2 = new StringBuilder();
			int total = dt.Columns.Count;
			int ListBreak = 0;
			if (itempage > 0)
			{
				if (total > itempage)
					total = (total%2 == 0) ? total/2 : total/2 + 1;
			}
			sb2.AppendFormat(@"<div class=""TableTitle"">&nbsp;{0}</div>", qbdesc);

			for (int i = 0; i < ((morerecord)?dt.Rows.Count:1); i++)
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
			Report = sb.ToString();
			sb = null;
		}


		private void HtmlList(DataTable dt, bool finalize, string qbdesc, bool morerecord)
		{
			StringBuilder sb = new StringBuilder(Report);
			StringBuilder sb2 = new StringBuilder();

			sb2.AppendFormat(@"<div class=""TableTitle"">&nbsp;{0}</div>", qbdesc);
			sb2.Append(@"<table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Container""><tr><td valign=top><table width=""100%"" border=0 cellspacing=1 cellpadding=1 class=""Internal""><tr>");
			foreach (DataColumn cc in dt.Columns)
			{
				sb2.AppendFormat(@"<td valign=top width=""15%"" nowrap class=""NameField NameList"">{0}&nbsp;</td>", cc.ColumnName);
			}
			sb2.Append(@"</tr>");
			for (int i = 0; i < ((morerecord)?dt.Rows.Count:1); i++)
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
			Report = sb.ToString();
			sb = null;
		}

		#region Web Form Designer generated code

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
