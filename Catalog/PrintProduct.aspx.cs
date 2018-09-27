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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Catalog
{
	public partial class PrintProduct : Page
	{
		private UserConfig UC = new UserConfig();

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];
			string idProduct = Request["ProductID"];
            long list = 0;
            if (Request["list"] != null) list = long.Parse(Request["list"]);
			DataTable dtProduct;
			dtProduct = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM CATALOGPRODUCTS WHERE ID={0}",idProduct)).Tables[0];
			if(dtProduct.Rows.Count>0)
			{
				if(dtProduct.Rows[0]["Image"].ToString().Length>0)
				{
					string pathTemplate;
					pathTemplate = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "Catalog"+Path.DirectorySeparatorChar +dtProduct.Rows[0]["Image"].ToString();

					if(File.Exists(pathTemplate))
					{
						ProductImage.ImageUrl="/imageRepath.aspx/Catalog/"+dtProduct.Rows[0]["Image"].ToString();
					}else
						ProductImage.ImageUrl="/images/TustenaLogoOS.gif";
				}
				else
					ProductImage.ImageUrl="/images/TustenaLogoOS.gif";

				ProductTitle.Text=dtProduct.Rows[0]["ShortDescription"].ToString();
				ProductDescription.Text=dtProduct.Rows[0]["LongDescription"].ToString();
				TxtUnit.Text = dtProduct.Rows[0]["Unit"].ToString();
				TxtQta.Text = dtProduct.Rows[0]["Qta"].ToString();
				TxtQtaBlister.Text = dtProduct.Rows[0]["QtaBlister"].ToString();
				CurrentCurrency.Text = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");

                TxtUnitPrice.Text = dtProduct.Rows[0]["UnitPrice"].ToString();
                if (list != 0)
                {
                    DataTable dtprice = DatabaseConnection.CreateDataset(string.Format("select * from CatalogProductPrice where productid={0} and listid={1}", idProduct, list)).Tables[0];
                    if (dtprice.Rows.Count > 0)
                    {
                        TxtUnitPrice.Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"]), 2));
                    }
                    else
                    {
                        DataTable lp = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRICELISTDESCRIPTION WHERE ID=" + list).Tables[0];
                        if (lp.Rows.Count > 0)
                        {
                            decimal unitprice = (decimal)dtProduct.Rows[0]["UnitPrice"];
                            unitprice = unitprice + (unitprice * Convert.ToDecimal(lp.Rows[0]["INCREASE"]) / 100);
                            TxtUnitPrice.Text = Convert.ToString(Math.Round(unitprice, 2));
                        }
                    }
                }
                if(Request["noprint"]==null)
				    ClientScript.RegisterStartupScript(this.GetType(), "close","<script>self.print();self.close();</script>");
			}else
				ClientScript.RegisterStartupScript(this.GetType(), "close","<script>self.close();</script>");
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
