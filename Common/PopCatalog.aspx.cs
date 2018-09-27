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
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class PopCatalog : G
	{

		private decimal ch = 1;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				if (!Page.IsPostBack)
				{
                    Digita.Tustena.Catalog.CatalogProducts.CreaTree(tvCategoryTree, 0, UC);
					FindProduct.Text =Root.rm.GetString("Captxt12");

				}

				string js;
				string ptx = Request.QueryString["ptx"].ToString();
				string pid = (Request.QueryString["pid"] != null) ? Request.QueryString["pid"].ToString() : "";
				string um = (Request.QueryString["um"] != null) ? Request.QueryString["um"].ToString() : "";
				string qta = (Request.QueryString["qta"] != null) ? Request.QueryString["qta"].ToString() : "";
				string up = (Request.QueryString["up"] != null) ? Request.QueryString["up"].ToString() : "";
				string vat = (Request.QueryString["vat"] != null) ? Request.QueryString["vat"].ToString() : "";
				string pl = (Request.QueryString["pl"] != null) ? Request.QueryString["pl"].ToString() : "";
				string pf = (Request.QueryString["pf"] != null) ? Request.QueryString["pf"].ToString() : "";
				ch = (Request.QueryString["ch"] != null) ? Convert.ToDecimal(Request.QueryString["ch"].Replace(".", ",")) : 1;

				js = "<script>" + Environment.NewLine;
				js += "function SetRef(ptx,pid,um,qta,up,vat,pl){" + Environment.NewLine;
				js += "	dynaret('" + ptx + "').value=ptx;" + Environment.NewLine;
				if (pid.Length > 0) js += "	dynaret('" + pid + "').value=pid;" + Environment.NewLine;
				if (um.Length > 0) js += "	dynaret('" + um + "').value=um;" + Environment.NewLine;
				if (qta.Length > 0) js += "	dynaret('" + qta + "').value=qta;" + Environment.NewLine;
				if (up.Length > 0) js += "	dynaret('" + up + "').value=up;" + Environment.NewLine;
				if (vat.Length > 0) js += "	dynaret('" + vat + "').value=vat;" + Environment.NewLine;
				if (pl.Length > 0) js += "	dynaret('" + pl + "').value=pl;" + Environment.NewLine;
				if (pf.Length > 0) js += "	dynaret('" + pf + "').value=pl;" + Environment.NewLine;
				js += "	self.close();" + Environment.NewLine;
				js += "	parent.HideBox();}" + Environment.NewLine;
				js += "</script>" + Environment.NewLine;

				ClientScript.RegisterClientScriptBlock(this.GetType(), "PopupScript", js);

			}
		}

		private void FindProduct_Click(object sender, EventArgs e)
		{
			FindProductQuery();
		}

		private void FillQueryCategories(int id, ref string c)
		{
			c += "CATEGORY=" + id + " OR ";
			DataTable cat = DatabaseConnection.CreateDataset("SELECT ID FROM CATALOGCATEGORIES WHERE PARENTID=" + id).Tables[0];
			if (cat.Rows.Count > 0)
			{
				foreach (DataRow d in cat.Rows)
				{
					FillQueryCategories(int.Parse(d[0].ToString()), ref c);
				}
			}
		}

		private void FindProductQuery()
		{
			StringBuilder q = new StringBuilder();
            if (TxtIdCategory.Text.Length>0)
			{
				string c = String.Empty;
                FillQueryCategories(int.Parse(TxtIdCategory.Text), ref c);
				c = c.Substring(0, c.Length - 3);
				q.AppendFormat("SELECT * FROM CATALOGPRODUCTS WHERE ({0}) AND (", c);
				q.AppendFormat("SHORTDESCRIPTION LIKE '{0}%' OR LONGDESCRIPTION LIKE '{0}%' OR CODE LIKE '{0}%') ", DatabaseConnection.FilterInjection(Search.Text));
				q.Append("AND ACTIVE=1 ORDER BY CATEGORY");
			}
			else
			{
				q.Append("SELECT * FROM CATALOGPRODUCTS WHERE (");
				q.AppendFormat("SHORTDESCRIPTION LIKE '{0}%' OR LONGDESCRIPTION LIKE '{0}%' OR CODE LIKE '{0}%') ", DatabaseConnection.FilterInjection(Search.Text));
				q.AppendFormat("AND ACTIVE=1 ORDER BY CATEGORY");
			}

			ProductRepeater.DataSource = DatabaseConnection.CreateDataset(q.ToString());
			ProductRepeater.DataBind();

			ProductRepeater.Visible = (ProductRepeater.Items.Count > 0);

		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.FindProduct.Click += new EventHandler(this.FindProduct_Click);
			this.ProductRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ProductRepeater_ItemDataBound);
			this.ProductRepeater.ItemCommand += new RepeaterCommandEventHandler(this.ProductRepeater_ItemCommand);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal BtnProduct = (Literal) e.Item.FindControl("BtnProduct");
					string p1 = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ShortDescription"));
					string p2 = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "id"));
					string p3 = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Unit"));
					string p4 = Convert.ToString(DataBinder.Eval(e.Item.DataItem, "Qta"));
					string p5 = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "UnitPrice"))*ch, 2));
					string p6 = Convert.ToString((DataBinder.Eval(e.Item.DataItem, "Vat")==DBNull.Value)?0:Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Vat")));
					string p7 = Convert.ToString(Math.Round(Convert.ToDecimal(DataBinder.Eval(e.Item.DataItem, "Qta"))*((decimal) DataBinder.Eval(e.Item.DataItem, "UnitPrice"))*ch, 2));

					BtnProduct.Text = "<span class=normal style=\"cursor:pointer\" onclick=\"SetRef('" + p1 + "','" + p2 + "','" + p3 + "','" + p4 + "','" + p5 + "','" + p6 + "','" + p7 + "')\">" +
						Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ShortDescription")) + "</span>";
					break;
			}
		}

		private void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{

		}
	}
}
