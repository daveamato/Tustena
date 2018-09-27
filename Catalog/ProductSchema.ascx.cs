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
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Catalog
{
	public partial class ProductSchema : UserControl
	{
		private UserConfig UC = new UserConfig();

		public string idQuote
		{
			get
			{
				object o = this.ViewState["_idQuote" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set { this.ViewState["_idQuote" + this.ID] = value; }
		}

		public string EstCurrency
		{
			get
			{
				object o = this.ViewState["_EstCurrency" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set { this.ViewState["_EstCurrency" + this.ID] = value; }
		}

		public string EstChange
			{
				get
				{
					object o = this.ViewState["_EstChange" + this.ID];
					if (o == null)
						return null;
					else
						return o.ToString();
				}
				set { this.ViewState["_EstChange" + this.ID] = value; }
			}

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
		}

		public void FillSchema()
		{
			DataTable dtProduct = QuoteProducts();
			ProductRepeater.DataSource=dtProduct;
			ProductRepeater.DataBind();
		}

		public DataTable QuoteProducts()
		{
			DataTable dtProducts = DatabaseConnection.CreateDataset(string.Format("SELECT CATALOGID FROM ESTIMATEDROWS WHERE ESTIMATEID={0} AND CATALOGID>0",idQuote)).Tables[0];
			string productList = "(";
			foreach(DataRow dr in dtProducts.Rows)
			{
				productList+="ID="+dr[0].ToString()+" OR ";
			}
			productList=productList.Substring(0,productList.Length-4)+")";
			DataTable dtProduct;

			dtProduct = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM CATALOGPRODUCTS WHERE {0}",productList)).Tables[0];
			return dtProduct;
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{

			this.ProductRepeater.ItemDataBound+=new RepeaterItemEventHandler(ProductRepeater_ItemDataBound);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion



		private void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			NumberFormatInfo nfi = new CultureInfo(UC.Culture, false).NumberFormat;
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal ProductImage = (Literal)e.Item.FindControl("ProductImage");
					Literal ProductTitle = (Literal)e.Item.FindControl("ProductTitle");
					Literal ProductDescription = (Literal)e.Item.FindControl("ProductDescription");
					Literal TxtUnit = (Literal)e.Item.FindControl("TxtUnit");
					Literal TxtQta = (Literal)e.Item.FindControl("TxtQta");
					Literal TxtQtaBlister = (Literal)e.Item.FindControl("TxtQtaBlister");
					Literal CurrentCurrency = (Literal)e.Item.FindControl("CurrentCurrency");
					Literal TxtUnitPrice = (Literal)e.Item.FindControl("TxtUnitPrice");


					if((DataBinder.Eval((DataRowView) e.Item.DataItem, "Image")).ToString().Length>0)
					{
						string pathTemplate;
					pathTemplate = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "Catalog"+Path.DirectorySeparatorChar + (DataBinder.Eval((DataRowView) e.Item.DataItem, "Image")).ToString();

						if(File.Exists(pathTemplate))
						{
							ProductImage.Text=string.Format("<img src=\"{0}\">",(DataBinder.Eval((DataRowView) e.Item.DataItem, "Image")).ToString());
						}
						else
							ProductImage.Text=string.Format("<img src=\"{0}\">","TustenaLogoOS.gif");
					}
					else
						ProductImage.Text=string.Format("<img src=\"{0}\">","TustenaLogoOS.gif");

					ProductTitle.Text=(DataBinder.Eval((DataRowView) e.Item.DataItem, "ShortDescription")).ToString();
					ProductDescription.Text= (DataBinder.Eval((DataRowView) e.Item.DataItem, "LongDescription")).ToString();
					TxtUnit.Text = (DataBinder.Eval((DataRowView) e.Item.DataItem, "Unit")).ToString();
					TxtQta.Text = (DataBinder.Eval((DataRowView) e.Item.DataItem, "Qta")).ToString();
					TxtQtaBlister.Text = (DataBinder.Eval((DataRowView) e.Item.DataItem, "QtaBlister")).ToString();
				CurrentCurrency.Text = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");

				string sy = EstCurrency;
				decimal chTo = StaticFunctions.FixDecimal(EstChange);

				decimal newprice = (decimal)DatabaseConnection.SqlScalartoObj(string.Format("SELECT NEWUPRICE FROM ESTIMATEDROWS WHERE ESTIMATEID={0} AND CATALOGID={1}",this.idQuote,(DataBinder.Eval((DataRowView) e.Item.DataItem, "id")).ToString()));
				TxtUnitPrice.Text = newprice.ToString();
				break;
			}
		}
	}
}
