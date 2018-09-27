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
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.ERP
{
	public partial class RowEditing : UserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			if (!Page.IsPostBack)
			{
				this.InitPage();
				CreateVatTable();
			}
            Page.ClientScript.RegisterStartupScript(this.GetType(), "norow", "<script>function norow(){alert('" + Root.rm.GetString("Quotxt29") + "');}</script>");
		}


		protected HtmlInputText EstProduct;

		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];
		private UserConfig UC = new UserConfig();

		public string RowEstCurrency
		{
			get
			{
				return EstCurrency.SelectedValue.Split('|')[0];
			}
		}

		public string RowEstChange
		{
			get
			{
				return EstChange.Text;
			}
		}

		private void CreateVatTable()
		{
			DataTable dt = DatabaseConnection.CreateDataset("SELECT TAXVALUE,TAXDESCRIPTION FROM TAXVALUES").Tables[0];
			StringBuilder vat = new StringBuilder();
			vat.Append("<select name=\"estVat\" id=\"estVat\" old=\"true\" class=\"BoxDesign\">");
			vat.AppendFormat("<option selected value=\"0\">{0}</option>", Root.rm.GetString("Choose"));
			foreach(DataRow dr in dt.Rows)
				vat.AppendFormat("<option value=\"{0}\">{1}</option>",dr[0].ToString(),dr[1].ToString());

			vat.Append("</select>");
			litVat.Text=vat.ToString();
			vat.Length=0;
			vat.Append("<select name=\"shipVat\" id=\"shipVat\" old=\"true\" class=\"BoxDesign\">");
			vat.AppendFormat("<option selected value=\"0\">{0}</option>", Root.rm.GetString("Choose"));
			foreach(DataRow dr in dt.Rows)
				vat.AppendFormat("<option value=\"{0}\">{1}</option>",dr[0].ToString(),dr[1].ToString());

			vat.Append("</select>");
			litshipVat.Text=vat.ToString();
		}

		private void InitPage()
		{

			EstCurrency.Attributes.Add("onchange", "changecurrency()");

			EstCurrency.DataTextField = "Currency";
			EstCurrency.DataValueField = "idcur";
			EstCurrency.DataSource = DatabaseConnection.CreateDataset("SELECT CAST(ID AS VARCHAR(10))+'|'+CAST(CHANGETOEURO AS VARCHAR(10))+'|'+CURRENCYSYMBOL AS IDCUR,CURRENCY FROM CURRENCYTABLE").Tables[0];
			EstCurrency.DataBind();

			EstChange.Text = "1";

		}

		public ArrayList ProductRows()
		{
			ArrayList rows = new ArrayList();
			long id=0;
			string sdesc=string.Empty;
			string ldesc=string.Empty;
			string um=string.Empty;
			double qta=0;
			decimal up=0;
			decimal vat=0;
			decimal lprice=0;
			decimal fprice=0;
			decimal cost=0;
            decimal reallprice = 0;
			string prodcode=string.Empty;


			if(Request["estProduct"]!=null)
			{
				id=(Request["estProductID"].Length>0)?Convert.ToInt64(Request["estProductID"]):0;
				sdesc=Request["estProduct"];
				if(id>0)
				{
					DataRow dr = DatabaseConnection.CreateDataset("SELECT LONGDESCRIPTION,CODE FROM CATALOGPRODUCTS WHERE ID="+id).Tables[0].Rows[0];
					ldesc=dr[0].ToString();
					prodcode=dr[1].ToString();
				}
				um=Request["estUm"];
				qta=(Request["estQta"].Length>0)?Convert.ToDouble(Request["estQta"]):1;
				up=(Request["estUp"].Length>0)?Convert.ToDecimal(Request["estUp"]):0;
				vat=(Request["estVat"].Length>0)?Convert.ToDecimal(Request["estVat"]):0;
				lprice=(Request["estPl"].Length>0)?Convert.ToDecimal(Request["estPl"]):0;
				fprice=(Request["estPf"].Length>0)?Convert.ToDecimal(Request["estPf"]):0;
				cost=(Request["estCost"].Length>0)?Convert.ToDecimal(Request["estCost"]):0;
                reallprice = (Request["estRealListPrice"].Length > 0) ? Convert.ToDecimal(Request["estRealListPrice"]) : 0;

				PurchaseProduct newprod = GetProduct( id, sdesc, ldesc, um, qta, up, vat, lprice, fprice, cost,prodcode,reallprice);
				rows.Add(newprod);
			}

			int other=1;
			while(Request["estProduct_"+other]!=null)
			{
				id=(Request["estProductID_"+other].Length>0)?Convert.ToInt64(Request["estProductID_"+other]):0;
				if(id>0)
				{
					DataRow dr = DatabaseConnection.CreateDataset("SELECT LONGDESCRIPTION,CODE FROM CATALOGPRODUCTS WHERE ID="+id).Tables[0].Rows[0];
					ldesc=dr[0].ToString();
					prodcode=dr[1].ToString();
				}
				else
				{
					ldesc=string.Empty;
					prodcode=string.Empty;
				}
				sdesc=Request["estProduct_"+other];
				um=Request["estUm_"+other];
				qta=(Request["estQta_"+other].Length>0)?Convert.ToDouble(Request["estQta_"+other]):1;
				up=(Request["estUp_"+other].Length>0)?StaticFunctions.FixDecimal(Request["estUp_"+other]):0;
				vat=(Request["estVat_"+other].Length>0)?Convert.ToDecimal(Request["estVat_"+other]):0;
				lprice=(Request["estPl_"+other].Length>0)?Convert.ToDecimal(Request["estPl_"+other]):0;
				fprice=(Request["estPf_"+other].Length>0)?Convert.ToDecimal(Request["estPf_"+other]):0;
				cost=(Request["estCost_"+other].Length>0)?Convert.ToDecimal(Request["estCost_"+other]):0;
                reallprice = (Request["estRealListPrice_"].Length > 0) ? Convert.ToDecimal(Request["estRealListPrice_"]) : 0;

                PurchaseProduct newprod = GetProduct(id, sdesc, ldesc, um, qta, up, vat, lprice, fprice, cost, prodcode, reallprice);
				rows.Add(newprod);

				other++;
			}


			return rows;
		}

		private static PurchaseProduct GetProduct(long id,string sdesc,string ldesc,string um,double qta,decimal up,decimal vat,decimal lprice,decimal fprice,decimal cost, string prodcode, decimal reallprice)
		{
			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id=id;
			newprod.ShortDescription=sdesc;
			newprod.LongDescription=ldesc;
			newprod.UM=um;
			newprod.Qta=qta;
			newprod.UnitPrice=up;
			newprod.Vat=vat;
			newprod.ListPrice=lprice;
			newprod.FinalPrice=fprice;
			newprod.Cost=cost;
			newprod.ProductCode=prodcode;
            newprod.RealListPrice = reallprice;
			return newprod;
		}

		Type setType=Type.Quote;
		public Type SetType
		{
			set
			{
				this.setType = value;
				ViewState["setType"] = value;
			}
			get { return this.setType; }
		}

		public enum Type
		{
			Quote,
			Order,
			Bill
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{

		}
		#endregion
	}
}
