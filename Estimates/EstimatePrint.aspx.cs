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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Estimates
{
	public partial class EstimatePrint : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				FillTemplate(int.Parse(Request.Params["e"].ToString()));
			}
		}

		private void FillTemplate(int id)
		{
			DataRow drE = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATES WHERE ID=" + id).Tables[0].Rows[0];
			DataRow drC = DatabaseConnection.CreateDataset("SELECT * FROM BASE_COMPANIES WHERE ID=" + drE["ClientID"].ToString()).Tables[0].Rows[0];
			string curSymbol = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE ID=" + drE["currency"].ToString());
			decimal chTo = Convert.ToDecimal(drE["change"]);
			TemplateAdmin ta = new TemplateAdmin();
			ta.TemplateName = "Estimate";
			ta.Language = UC.CultureSpecific;
			ta.ApplicationPath = Request.PhysicalApplicationPath;
			ta.Global = false;
			string template = ta.GetTemplate();
			template = template.Replace("Tustena.Logo", ta.GetLogo());

			template = template.Replace("Tustena.CompanyName", drC["CompanyName"].ToString());
			template = template.Replace("Tustena.Address", drC["InvoicingAddress"].ToString());
			template = template.Replace("Tustena.ZipCode", drC["InvoicingZipCode"].ToString());
			template = template.Replace("Tustena.City", drC["InvoicingCity"].ToString());
			template = template.Replace("Tustena.Province", drC["InvoicingStateProvince"].ToString());
			template = template.Replace("Tustena.Nation", drC["InvoicingState"].ToString());

			template = template.Replace("Tustena.Contact", DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM BASE_CONTACTS WHERE ID=" + drE["Referrerid"].ToString()));

			DataTable dte = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATEDROWS WHERE ESTIMATEID=" + id).Tables[0];
			StringBuilder rows = new StringBuilder();
			rows.Append("<table border=0 cellpadding=0 cellspacing=0 width=\"100%\" class=normal align=\"center\">");
			rows.Append("<tr>");
			rows.AppendFormat("<td width=\"30%\"><b>{0}</b></td>",Root.rm.GetString("CRMcontxt65"));
			rows.AppendFormat("<td width=\"10%\"><b>{0}</b></td>",Root.rm.GetString("CRMcontxt66"));
			rows.AppendFormat("<td width=\"10%\" align=right><b>{0}</b></td>",Root.rm.GetString("CRMcontxt67"));
			rows.AppendFormat("<td width=\"20%\" align=right><b>{0}</b></td>",Root.rm.GetString("CRMcontxt68"));
			rows.AppendFormat("<td width=\"10%\" align=right><b>{0}</b></td>",Root.rm.GetString("CRMcontxt69"));
			rows.AppendFormat("<td width=\"20%\" align=right><b>{0}</b></td>",Root.rm.GetString("CRMcontxt71"));
			rows.Append("</tr>");
			decimal subTotal = 0;
			foreach (DataRow r in dte.Rows)
			{
				if (r["CatalogID"].ToString() != "0")
				{
					DataRow drp = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRODUCTS WHERE ID=" + r["CatalogID"].ToString()).Tables[0].Rows[0];
					rows.Append("<tr>");
					rows.AppendFormat("<td width=\"30%\">{0}<br>{1}</td>", drp["ShortDescription"].ToString(), r["Description"].ToString());
					rows.AppendFormat("<td width=\"10%\">{0}</td>", drp["Unit"].ToString());
					rows.AppendFormat("<td width=\"10%\" align=right>{0}</td>", r["Qta"].ToString());
					rows.AppendFormat("<td width=\"20%\" align=right>{1}&nbsp;{0}</td>", Math.Round(Convert.ToDecimal(r["Uprice"])*chTo, 2).ToString(), curSymbol);
					rows.AppendFormat("<td width=\"10%\" align=right>{0}</td>", drp["Vat"].ToString());
					rows.AppendFormat("<td width=\"20%\" align=right>{1}&nbsp;{0}</td>", Math.Round(Convert.ToDecimal(r["NewUprice"])*chTo, 2).ToString(), curSymbol);
					rows.Append("</tr>");
				}
				else
				{
					rows.Append("<tr>");
					rows.AppendFormat("<td width=\"30%\">{0}</td>", r["Description"].ToString());
					rows.AppendFormat("<td width=\"10%\">&nbsp;</td>");
					rows.AppendFormat("<td width=\"10%\" align=right>{0}</td>", r["Qta"].ToString());
					rows.AppendFormat("<td width=\"20%\" align=right>{1}&nbsp;{0}</td>", Math.Round(Convert.ToDecimal(r["Uprice"])*chTo, 2).ToString(), curSymbol);
					rows.AppendFormat("<td width=\"10%\" align=right>&nbsp;</td>");
					rows.AppendFormat("<td width=\"20%\" align=right>{1}&nbsp;{0}</td>", Math.Round(Convert.ToDecimal(r["NewUprice"])*chTo, 2).ToString(), curSymbol);
					rows.Append("</tr>");
				}
				subTotal += Convert.ToDecimal(r["NewUprice"]);
			}
			rows.AppendFormat("<tr><td colspan=6 align=right style=\"BORDER-TOP:black 1px solid;padding-top:5px;\"><b>{1}&nbsp;{0}</b></td></tr>", Math.Round(subTotal*chTo, 2).ToString(), curSymbol);
			rows.Append("</table>");
			template = template.Replace("Tustena.EstimateRows", rows.ToString());
			template = template.Replace("Tustena.EstimateNotes", drE["Description"].ToString());

			Result.Text = template;
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
