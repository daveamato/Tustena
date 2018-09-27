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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class checkexist : G
	{


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				StringBuilder query = new StringBuilder();
				query.Append("SELECT COMPANYNAME, ");
				string[] valori = Request.QueryString["Company"].Split(' ');
				string tempQuery = String.Empty;
				string tempQuery2 = String.Empty;
				for (int i = 0; i < valori.Length; i++)
				{
					tempQuery += "(SELECT COUNT(*) FROM BASE_COMPANIES WHERE COMPANYNAME LIKE '%" + DatabaseConnection.FilterInjection(valori[i]) + "%' AND ID=CONF.ID) + ";
					tempQuery2 += "COMPANYNAME LIKE '%" + DatabaseConnection.FilterInjection(valori[i]) + "%' OR ";
				}

				query.Append(tempQuery.Substring(0, tempQuery.Length - 2));
				query.AppendFormat(" AS RANK ");

				query.Append("FROM BASE_COMPANIES AS CONF ");
				query.AppendFormat("WHERE LIMBO=0 AND ({0}) AND ", tempQuery2.Substring(0, tempQuery2.Length - 3));
				if (valori.Length > 1)
					query.AppendFormat("({0})>1 ORDER BY RANK DESC", tempQuery.Substring(0, tempQuery.Length - 2));
				else
					query.AppendFormat("({0})>0 ORDER BY RANK DESC", tempQuery.Substring(0, tempQuery.Length - 2));

				CompaniesRep.DataSource = DatabaseConnection.CreateDataset(query.ToString());
				CompaniesRep.DataBind();
				if (CompaniesRep.Items.Count > 0)
					ImgOK.Text =Root.rm.GetString("CRMcontxt83");
				else
				{
					CompaniesRep.Visible = false;
					ImgOK.Text =Root.rm.GetString("CRMcontxt84");
				}

			}

		}

		public void CompaniesItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label Company = (Label) e.Item.FindControl("Company");
					Company.Text = (string) DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName");
					if (Company.Text.Length > 40)
					{
						Regex rx = new Regex(@"(?s)\b.{1,39}\b");
						Company.ToolTip = Company.Text;
						Company.Text = rx.Match(Company.Text) + "&hellip;";
						Company.Attributes.Add("style", "cursor:help;");
					}
					break;
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
			this.CompaniesRep.ItemDataBound += new RepeaterItemEventHandler(this.CompaniesItemDataBound);

		}

		#endregion
	}
}
