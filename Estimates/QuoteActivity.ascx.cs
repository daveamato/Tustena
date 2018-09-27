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
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Brettle.Web.NeatUpload;
using Digita.Tustena.Base;
using Digita.Tustena.Catalog;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using ICSharpCode.SharpZipLib.Zip;
using mhtmlwriter;

namespace Digita.Tustena.Estimates
{
	public class QuoteActivity : UserControl
	{
		protected DropDownList EstCurrency;
		protected CompareDomValidator valestChange;
		protected TextBox EstChange;
		protected TextBox EstProductID;
		protected TextBox EstProduct;
		protected TextBox EstUm;
		protected TextBox EstQta;
		protected TextBox EstUp;
		protected TextBox EstVat;
		protected TextBox EstPl;
		protected TextBox EstDescription2;
		protected TextBox EstPf;
		protected LinkButton BtnAddProduct;
		protected TextBox EstFreeProduct;
		protected TextBox EstFreeUm;
		protected TextBox EstFreeQta;
		protected TextBox EstFreeUp;
		protected TextBox EstFreeVat;
		protected TextBox EstFreePf;
		protected LinkButton BtnAddFreeProductOpp;
		protected Repeater RepeaterEstProduct;
		protected Repeater RepeaterEstProductForPrint;
		protected HtmlTableRow catalogestimate;
		protected HtmlTableRow freeestimate;
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];
		private UserConfig UC = new UserConfig();

		protected HtmlImage imgfreeestimate;
		protected TextBox EstReduc;
		protected TextBox EstGlobalReduction;
		protected TextBox EstFreeReduct;
		protected LinkButton EstPrint;

		protected DropDownList EstTemplateFile;
		protected HtmlImage imgForPrint;
		protected InputFile NewTemplate;

		private DateTime _ExpirationDate;
		private byte _QuoteStage = 1;
		private string quoteNumber = string.Empty;
		protected HtmlImage imgcatalogestimate;
		protected HtmlTableRow ForPrint;
		protected LinkButton BtnLoadTemplate;
		protected ProgressBar inlineProgressBar;

		protected ProductSchema ProductSchemaPrint;
		protected Label label;

		private decimal TotalQuotePrice;

		public string Activity
		{
			get
			{
				object o = this.ViewState["_ActivityID" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set { this.ViewState["_ActivityID" + this.ID] = value; }
		}

		public byte QuoteStage
		{
			set
			{
				this._QuoteStage = value;
				ViewState["_QuoteStage"] = value;
			}
			get { return this._QuoteStage; }
		}

		public string QuoteNumber
		{
			set
			{
				this.quoteNumber = value;
				ViewState["_QuoteNumber"] = value;
			}
			get { return this.quoteNumber; }
		}

		public DateTime ExpirationDate
		{
			set
			{
				this._ExpirationDate = value;
				ViewState["_ExpirationDate"] = value;
			}
			get { return this._ExpirationDate; }
		}

		public void InitProgressBar()
		{
		}

		private void Page_Load(object sender, EventArgs e)
		{
			try
			{
				if(this.Parent.FindControl("PanelQuote").Visible)
					Page.RegisterStartupScript("tabActive", "<script>ViewHideTabs('tdTab2');</script>");
			}catch{}

			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			Trace.Warn("Activity", this.Activity);
			DataStorage.JsUpload(Page);
			this.BtnLoadTemplate.Attributes.Add("onClick","ShowProgressBar()");

			if (!Page.IsPostBack)
			{
				this.BtnLoadTemplate.Text = "LOAD";

				FillTemplate();
				if (!(EstCurrency.Items.Count > 0))
					InitPage();
			}
		}

		private void FillTemplate()
		{
			FileFunctions.CheckDir(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template"+Path.DirectorySeparatorChar + "quote"+Path.DirectorySeparatorChar, true);
			FileInfo fi_fixed = new FileInfo(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template"+Path.DirectorySeparatorChar + "quote"+Path.DirectorySeparatorChar);
			DirectoryInfo di_fixed = fi_fixed.Directory;
			FileSystemInfo[] fsi_fixed = di_fixed.GetFiles();
			this.EstTemplateFile.Items.Clear();
			foreach (FileSystemInfo info in fsi_fixed)
				this.EstTemplateFile.Items.Add(new ListItem(info.Name, "fix_" + info.Name));

			string templateFolder;
			templateFolder = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar;
			FileFunctions.CheckDir(templateFolder, true);
			FileInfo fi = new FileInfo(templateFolder);
			DirectoryInfo di = fi.Directory;
			FileSystemInfo[] fsi = di.GetFiles();
			foreach (FileSystemInfo info in fsi)
				this.EstTemplateFile.Items.Add(info.Name);
		}


		private void InitPage()
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			BtnAddProduct.Text =Root.rm.GetString("Esttxt14");
			BtnAddProduct.Attributes.Add("onclick", "return ValidateProduct()");


			EstCurrency.Attributes.Add("onchange", "changecurrency()");
			BtnAddFreeProductOpp.Text =Root.rm.GetString("Esttxt14");
			BtnAddFreeProductOpp.Attributes.Add("onclick", "return ValidateFreeProduct()");

			EstCurrency.DataTextField = "Currency";
			EstCurrency.DataValueField = "idcur";
			EstCurrency.DataSource = DatabaseConnection.CreateDataset("sELECT CAST(ID AS VARCHAR(10))+'|'+CAST(CHANGETOEURO AS VARCHAR(10))+'|'+CURRENCYSYMBOL AS IDCUR,CURRENCY FROM CURRENCYTABLE").Tables[0];
			EstCurrency.DataBind();

			EstChange.Text = EstCurrency.Items[0].Value.Split('|')[1];

		}

		public void InitQuote()
		{
			InitPage();
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.EstProduct.TextChanged += new EventHandler(this.EstProduct_TextChanged);
			this.BtnAddProduct.Click += new EventHandler(this.btnAddProduct_Click);
			this.BtnAddFreeProductOpp.Click += new EventHandler(this.btnAddFreeProductOpp_Click);
			this.RepeaterEstProduct.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterEstProduct_ItemDataBound);
			this.RepeaterEstProduct.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterEstProduct_ItemCommand);
			this.BtnLoadTemplate.Click += new EventHandler(this.btnLoadTemplate_Click);
			this.EstPrint.Click += new EventHandler(this.estPrint_Click);
			this.RepeaterEstProductForPrint.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterEstProduct_ItemDataBound);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void btnAddProduct_Click(object sender, EventArgs e)
		{
			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id = Convert.ToInt64(EstProductID.Text);
			newprod.ShortDescription = EstProduct.Text;
			newprod.LongDescription = EstDescription2.Text;
			newprod.UM = EstUm.Text;
			newprod.Qta = Convert.ToInt32(EstQta.Text);
			decimal chFrom = (1/StaticFunctions.FixDecimal(EstChange.Text));
			newprod.UnitPrice = Math.Round(StaticFunctions.FixDecimal(EstUp.Text)*chFrom, 2);
			newprod.Vat = (EstVat.Text.Length > 0) ? Convert.ToInt32(EstVat.Text) : 0;
			newprod.ListPrice = Math.Round(StaticFunctions.FixDecimal(EstPl.Text)*chFrom, 2);
			newprod.FinalPrice = Math.Round(StaticFunctions.FixDecimal(EstPf.Text)*chFrom, 2);
			newprod.Reduction = (EstReduc.Text.Length > 0) ? Convert.ToInt32(EstReduc.Text) : 0;

			ArrayList np = new ArrayList();
			if (Session["newprod"] != null)
				np = (ArrayList) Session["newprod"];

			newprod.ObId = np.Count;

			np.Add(newprod);
			Session["newprod"] = np;

			RepeaterEstProduct.DataSource = np;
			RepeaterEstProduct.DataBind();
			RepeaterEstProduct.Visible = true;
		}



		private void RepeaterEstProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			NumberFormatInfo nfi = new CultureInfo(UC.Culture, false).NumberFormat;
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal ShortDescription = (Literal) e.Item.FindControl("ShortDescription");
					Literal UM = (Literal) e.Item.FindControl("UM");
					Literal Qta = (Literal) e.Item.FindControl("Qta");
					Literal UnitPrice = (Literal) e.Item.FindControl("UnitPrice");
					Literal Vat = (Literal) e.Item.FindControl("Vat");
					Literal Reduction = (Literal) e.Item.FindControl("Reduction");
					Literal FinalPrice = (Literal) e.Item.FindControl("FinalPrice");
					Literal ObjectID = (Literal) e.Item.FindControl("ObjectID");
					PurchaseProduct purchaseProduct = (PurchaseProduct) e.Item.DataItem;
					ShortDescription.Text = purchaseProduct.ShortDescription;
					UM.Text = purchaseProduct.UM.ToString();
					Qta.Text = purchaseProduct.Qta.ToString();
					Vat.Text = purchaseProduct.Vat.ToString();
					Reduction.Text = purchaseProduct.Reduction.ToString();
					string sy = EstCurrency.SelectedValue.Split('|')[2];
					decimal chTo = StaticFunctions.FixDecimal(EstChange.Text);

					UnitPrice.Text = sy + " " + Math.Round((purchaseProduct.UnitPrice*chTo), 2).ToString("N", nfi);
					FinalPrice.Text = sy + " " + Math.Round((purchaseProduct.FinalPrice*chTo), 2).ToString("N", nfi);
					this.TotalQuotePrice += (purchaseProduct.FinalPrice*chTo);
					ObjectID.Text = purchaseProduct.ObId.ToString();
					break;
				case ListItemType.Footer:
					Literal TotalQuote = (Literal) e.Item.FindControl("TotalQuote");
					sy = EstCurrency.SelectedValue.Split('|')[2];
					TotalQuote.Text = sy + " " + Math.Round(TotalQuotePrice, 2).ToString("N", nfi);
					break;
			}
		}

		private void RepeaterEstProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelPurPro":
					Literal ObjectID = (Literal) e.Item.FindControl("ObjectID");
					ArrayList np = new ArrayList();
					np = (ArrayList) Session["newprod"];
					np.RemoveAt(Convert.ToInt32(ObjectID.Text));

					for (int i = 0; i < np.Count; i++)
					{
						PurchaseProduct newprod = (PurchaseProduct) np[i];
						newprod.ObId = i;
						np[i] = newprod;
					}

					Session["newprod"] = np;
					RepeaterEstProduct.DataSource = np;
					RepeaterEstProduct.DataBind();
					RepeaterEstProduct.Visible = true;

					break;
			}
		}

		public void SaveQuote()
		{
			ArrayList pp = (ArrayList) Session["newprod"];
			if (Session["newprod"] != null && pp.Count > 0)
			{
				bool newQuote;

				string billId;
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("CURRENCY", EstCurrency.SelectedValue.Split('|')[0]);
					dg.Add("CHANGE", StaticFunctions.FixDecimal(EstChange.Text));
					dg.Add("EXPIRATIONDATE", this._ExpirationDate);
					dg.Add("LASTMODIFIEDDATE", DateTime.Now);
					dg.Add("LASTMODIFIEDBYID", UC.UserId);
					dg.Add("ACTIVITYID", this.Activity);
					dg.Add("STAGE", this.QuoteStage);
					if (this.QuoteNumber.Length > 0)
						dg.Add("NUMBER", this.QuoteNumber);

					try
					{
						dg.Add("REDUCTION", Convert.ToByte(this.EstGlobalReduction.Text));
					}
					catch
					{
						dg.Add("REDUCTION", 0);
					}


					dg.Execute("ESTIMATES", "ACTIVITYID=" + this.Activity, DigiDapter.Identities.Row);

					billId = dg.GetNewRow["id"].ToString();

					newQuote = dg.RecordInserted;
				}
				if (!newQuote)
				{
					DatabaseConnection.DoCommand("DELETE FROM ESTIMATEDROWS WHERE ESTIMATEID=" + billId);
				}
				foreach (PurchaseProduct Pprod in pp)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("ESTIMATEID", billId);
						dg.Add("DESCRIPTION", Pprod.ShortDescription);
						dg.Add("DESCRIPTION2", Pprod.LongDescription);
						dg.Add("UPRICE", Pprod.UnitPrice);
						dg.Add("NEWUPRICE", Pprod.FinalPrice);
						dg.Add("CATALOGID", Pprod.id);
						dg.Add("QTA", Pprod.Qta);
						dg.Add("REDUCTION", Pprod.Reduction);
						dg.Execute("ESTIMATEDROWS");

					}
				}
				Session.Remove("newprod");

			}
			else
			{
				Page.RegisterStartupScript("norows", "<script>alert('" +Root.rm.GetString("Esttxt38") + "');</script>");
			}


		}

		private void btnAddFreeProductOpp_Click(object sender, EventArgs e)
		{
			ArrayList np = new ArrayList();
			if (Session["newprod"] != null)
				np = (ArrayList) Session["newprod"];

			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id = 0;
			newprod.ShortDescription = EstFreeProduct.Text;
			newprod.LongDescription = EstFreeProduct.Text;
			newprod.UM = EstFreeUm.Text;
			newprod.Qta = Convert.ToInt32(EstFreeQta.Text);
			newprod.UnitPrice = StaticFunctions.FixDecimal(EstFreeUp.Text);
			newprod.Vat = Convert.ToInt32(EstFreeVat.Text);
			newprod.ListPrice = StaticFunctions.FixDecimal(EstFreePf.Text);
			newprod.FinalPrice = StaticFunctions.FixDecimal(EstFreePf.Text);
			newprod.Reduction = Convert.ToInt32(this.EstFreeReduct.Text);
			newprod.ObId = np.Count;
			np.Add(newprod);

			Session["newprod"] = np;
			RepeaterEstProduct.DataSource = np;
			RepeaterEstProduct.DataBind();
			RepeaterEstProduct.Visible = true;

			EstFreeProduct.Text = string.Empty;
			EstFreeUm.Text = string.Empty;
			EstFreeQta.Text = string.Empty;
			EstFreeUp.Text = string.Empty;
			EstFreeVat.Text = string.Empty;
			EstFreePf.Text = string.Empty;
		}

		public void ModifyEstimate(int activityId)
		{
			DataTable dtMain = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATES WHERE ACTIVITYID=" + activityId).Tables[0];
			if (dtMain.Rows.Count > 0)
			{
				DataRow dr = dtMain.Rows[0];
				this.Activity = activityId.ToString();
				InitPage();
				EstCurrency.SelectedIndex = -1;
				foreach (ListItem li in EstCurrency.Items)
				{
					if (li.Value.Split('|')[0] == dr["Currency"].ToString())
					{
						li.Selected = true;
						break;
					}
				}
				if (EstCurrency.SelectedIndex == -1) EstCurrency.SelectedIndex = 0;

				EstChange.Text = dr["Change"].ToString();
				this.ExpirationDate = Convert.ToDateTime(dr["ExpirationDate"]);
				this.QuoteNumber = dr["number"].ToString();
				try
				{
					this.QuoteStage = (byte) dr["Stage"];
				}
				catch
				{
					this.QuoteStage = 0;
				}


				DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATEDROWS WHERE ESTIMATEID=" + dr["id"].ToString()).Tables[0];

				ArrayList np = new ArrayList();
				foreach (DataRow ddr in dt.Rows)
				{
					if (ddr["CatalogID"].ToString() != "0")
					{
						DataSet tempNP = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRODUCTS WHERE ID=" + ddr["CatalogID"].ToString());
						if (tempNP.Tables[0].Rows.Count > 0)
						{
							DataRow pInfo = tempNP.Tables[0].Rows[0];
							PurchaseProduct newprod = new PurchaseProduct();
							newprod.id = Convert.ToInt64(ddr["CatalogID"]);
							newprod.ShortDescription = pInfo["ShortDescription"].ToString();
							newprod.LongDescription = pInfo["LongDescription"].ToString();
							newprod.UM = pInfo["Unit"].ToString();
							newprod.Qta = (double) ddr["Qta"];
							newprod.UnitPrice = (decimal) ddr["Uprice"];
							newprod.Vat = (pInfo["Vat"] != DBNull.Value) ? Convert.ToDecimal(pInfo["Vat"]) : 0;
							newprod.ListPrice = Math.Round(Convert.ToDecimal(newprod.Qta)*newprod.UnitPrice, 2);
							newprod.FinalPrice = (decimal) ddr["NewUPrice"];
							try
							{
								newprod.Reduction = (int) ddr["reduction"];
							}
							catch
							{
								newprod.Reduction = 0;
							}

							np.Add(newprod);
						}
					}
					else
					{
						PurchaseProduct newprod = new PurchaseProduct();
						newprod.id = 0;
						newprod.ShortDescription = ddr["Description"].ToString();
						newprod.LongDescription = ddr["Description"].ToString();
						newprod.UM = String.Empty;
						newprod.Qta = (double) ddr["qta"];
						newprod.UnitPrice = (decimal) ddr["Uprice"];
						newprod.Vat = 0;
						newprod.ListPrice = Math.Round(Convert.ToDecimal(newprod.Qta)*newprod.UnitPrice, 2);
						newprod.FinalPrice = (decimal) ddr["NewUPrice"];
						try
						{
							newprod.Reduction = (int) ddr["reduction"];
						}
						catch
						{
							newprod.Reduction = 0;
						}
						np.Add(newprod);
					}

				}

				Session.Remove("newprod");
				Session["newprod"] = np;

				RepeaterEstProduct.DataSource = np;
				RepeaterEstProduct.DataBind();
				RepeaterEstProduct.Visible = true;
			}

		}

		private void estPrint_Click(object sender, EventArgs e)
		{
			try
			{
				if(this.Activity==null || this.Activity.Length==0 || this.Activity=="-1")
				{
					Page.RegisterStartupScript("noprint","<script>alert('" +Root.rm.GetString("Esttxt46") + "');</script>");
					return;
				}
			}
			catch
			{
					Page.RegisterStartupScript("noprint","<script>alert('" +Root.rm.GetString("Esttxt46") + "');</script>");
					return;
			}

				StreamReader objReader;
				string template = string.Empty;

				if (this.EstTemplateFile.SelectedValue.StartsWith("fix_"))
				{
					objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template"+Path.DirectorySeparatorChar + "quote"+Path.DirectorySeparatorChar + this.EstTemplateFile.SelectedItem.Text);
					template = objReader.ReadToEnd();
					objReader.Close();
				}
				else
				{
				objReader = new StreamReader(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + this.EstTemplateFile.SelectedValue);
					template = objReader.ReadToEnd();
					objReader.Close();
				}

				objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + Request["LayOut"]);
				string style = objReader.ReadToEnd();
				objReader.Close();

				DataRow drquote = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITY WHERE ID=" + this.Activity).Tables[0].Rows[0];
				DataRow drquoteInfo = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATES WHERE ACTIVITYID=" + this.Activity).Tables[0].Rows[0];
				DataRow drquoteCompany;
			drquoteCompany = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,ADDRESS,CITY,PROVINCE,ZIPCODE FROM TUSTENA_DATA").Tables[0].Rows[0];
				template = template.Replace("Tustena.QuoteDate", UC.LTZ.ToLocalTime((DateTime) drquote["ActivityDate"]).ToShortDateString());
				template = template.Replace("Tustena.QuoteExpire", UC.LTZ.ToLocalTime((DateTime) drquoteInfo["ExpirationDate"]).ToShortDateString());
				template = template.Replace("Tustena.QuoteSubject", drquote["Subject"].ToString());

				template = template.Replace("Tustena.Owner", DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + drquote["ownerid"].ToString()));
				template = template.Replace("Tustena.QuoteNumber", drquoteInfo["number"].ToString());

				template = template.Replace("Tustena.MyCompany", drquoteCompany["companyname"].ToString());
				template = template.Replace("Tustena.MyAddress", drquoteCompany["address"].ToString());
				template = template.Replace("Tustena.MyCity", drquoteCompany["city"].ToString());
				template = template.Replace("Tustena.MyProvince", drquoteCompany["province"].ToString());
				template = template.Replace("Tustena.MyZipCode", drquoteCompany["zipcode"].ToString());

				template = template.Replace("Tustena.Message", drquote["Description"].ToString());


				if (drquote["CompanyID"] != DBNull.Value)
				{
					DataRow drCompany = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,INVOICINGADDRESS,INVOICINGCITY,INVOICINGSTATEPROVINCE,INVOICINGSTATE,INVOICINGZIPCODE FROM BASE_COMPANIES WHERE ID=" + drquote["CompanyID"].ToString()).Tables[0].Rows[0];
					template = template.Replace("Tustena.CompanyName", drCompany["CompanyName"].ToString());
					template = template.Replace("Tustena.CompanyAddress", drCompany["InvoicingAddress"].ToString());
					template = template.Replace("Tustena.CompanyZipCode", drCompany["InvoicingZIPCode"].ToString());
					template = template.Replace("Tustena.CompanyCity", drCompany["InvoicingCity"].ToString());
					template = template.Replace("Tustena.CompanyProvince", drCompany["InvoicingStateProvince"].ToString());

					if (drquote["ReferrerID"] != DBNull.Value)
					{
						DataRow drContact = DatabaseConnection.CreateDataset("SELECT TITLE,NAME,SURNAME FROM BASE_CONTACTS WHERE ID=" + drquote["ReferrerID"].ToString()).Tables[0].Rows[0];
						template = template.Replace("Tustena.ContactTitle", drContact["title"].ToString());
						template = template.Replace("Tustena.ContactName", drContact["Name"].ToString());
						template = template.Replace("Tustena.ContactSurname", drContact["Surname"].ToString());
					}

				}
				else if (drquote["ReferrerID"] != DBNull.Value)
				{
					DataRow drContact = DatabaseConnection.CreateDataset("SELECT TITLE,NAME,SURNAME FROM BASE_CONTACTS WHERE ID=" + drquote["ReferrerID"].ToString()).Tables[0].Rows[0];
					template = template.Replace("Tustena.ContactTitle", drContact["title"].ToString());
					template = template.Replace("Tustena.ContactName", drContact["Name"].ToString());
					template = template.Replace("Tustena.ContactSurname", drContact["Surname"].ToString());
				}
				else
				{
					DataRow drlead = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,TITLE,NAME,SURNAME,ADDRESS,CITY,PROVINCE,ZIPCODE,STATE FROM CRM_LEAD WHERE ID=" + drquote["LeadID"].ToString()).Tables[0].Rows[0];
					template = template.Replace("Tustena.CompanyName", drlead["CompanyName"].ToString());
					template = template.Replace("Tustena.CompanyAddress", drlead["Address"].ToString());
					template = template.Replace("Tustena.CompanyZipCode", drlead["ZIPCode"].ToString());
					template = template.Replace("Tustena.CompanyCity", drlead["ICity"].ToString());
					template = template.Replace("Tustena.CompanyProvince", drlead["Province"].ToString());
					template = template.Replace("Tustena.ContactTitle", drlead["title"].ToString());
					template = template.Replace("Tustena.ContactName", drlead["Name"].ToString());
					template = template.Replace("Tustena.ContactSurname", drlead["Surname"].ToString());

				}

				StringWriterWithEncoding SW = new StringWriterWithEncoding(Encoding.UTF8);

				HtmlTextWriter hw = new HtmlTextWriter(SW);

				hw.Write(style);
				hw.WriteLine("<br>&nbsp;");

				ArrayList np = new ArrayList();
				np = (ArrayList) Session["newprod"];
				RepeaterEstProductForPrint.DataSource = np;
				RepeaterEstProductForPrint.DataBind();
				this.RepeaterEstProductForPrint.RenderControl(hw);

				template = template.Replace("Tustena.QuoteTable", SW.ToString());
				RepeaterEstProductForPrint.Visible = false;

				if(template.IndexOf("Tustena.Products")>0)
				{
					SW = new StringWriterWithEncoding(Encoding.UTF8);
					hw = new HtmlTextWriter(SW);
					ProductSchemaPrint.idQuote = drquoteInfo["id"].ToString();
					ProductSchemaPrint.EstCurrency = EstCurrency.SelectedValue.Split('|')[2];
					ProductSchemaPrint.EstChange = EstChange.Text;
					ProductSchemaPrint.FillSchema();
					ProductSchemaPrint.RenderControl(hw);
					template = template.Replace("Tustena.Products", SW.ToString());
					DataTable dtimage = ProductSchemaPrint.QuoteProducts();
					string[] images = new string[dtimage.Rows.Count+1];
					int imgcount=0;
					foreach(DataRow dr in dtimage.Rows)
					{
						if(dr["image"]!=null)
						{
							images[imgcount++]=dr["image"].ToString();
						}
					}
					images[dtimage.Rows.Count]="TustenaLogoOS.gif";
					mht m = new mht("");

					string basePath;
				basePath = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "catalog"+Path.DirectorySeparatorChar;
					template=m.InjectImages(template,images,basePath,Request.PhysicalApplicationPath+Path.DirectorySeparatorChar + "images" + Path.DirectorySeparatorChar);

					ProductSchemaPrint.Visible=false;
				}


				Response.AddHeader("Content-Disposition", "attachment; filename=quote.doc");
				Response.AddHeader("Expires", "Thu, 01 Dec 1994 16:00:00 GMT");
				Response.AddHeader("Pragma", "nocache");
				Response.ContentType = "application/octet-stream";
				Response.Write(template);
				Response.End();

		}

		private void btnLoadTemplate_Click(object sender, EventArgs e)
		{


			FileFunctions.CheckDir(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "temp", true);
			string tempPath;
			tempPath = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "temp";
			string zip = string.Empty;
			string destName = string.Empty;
			if (this.NewTemplate.FileContent!=null)
			{

					zip = Path.Combine(tempPath, Path.GetFileName(this.NewTemplate.FileName));
					this.NewTemplate.MoveTo(zip, MoveToOptions.Overwrite);
					this.InitProgressBar();
					destName = Path.Combine(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "template", Path.GetFileNameWithoutExtension(this.NewTemplate.FileName) + ".mht");



				try
				{
					CreateMHTML(zip, tempPath, destName);
				}
				catch (Exception ex)
				{
					Trace.Warn("Exception", ex.Message);
				}
				finally
				{
					Directory.Delete(tempPath, true);
				}

			}

			FillTemplate();

			Page.RegisterStartupScript("openforprint", "<script>HidePart('ForPrint');</script>");

		}

		private void CreateMHTML(string zipfile, string tempdir, string destpath)
		{
			ExtractArchive(zipfile, tempdir);
			string filePath = FindFirstHtml(tempdir);
			if (filePath == null)
				throw new IOException("File not present");
			string dir = Path.GetDirectoryName(filePath);
			TextReader tr = File.OpenText(filePath);
			mht mhtml = new mht(dir);
			TextWriter tw = File.CreateText(destpath);
			string html = tr.ReadToEnd();
			tr.Close();
			if (html.ToLower().IndexOf("tustena.") < 0)
				throw new ExecutionEngineException("Does not contain prefix Tustena.*");
			string mhtmlFile = mhtml.WriteStructure(html);
			tw.Write(mhtmlFile);
			tw.Close();
		}

		private string FindFirstHtml(string extractDir)
		{
			string firstFile = null;
			foreach (string f in Directory.GetFiles(extractDir, "*.htm"))
			{
				firstFile = f;
			}
			if (firstFile == null)
				foreach (string d in Directory.GetDirectories(extractDir))
				{
					foreach (string f in Directory.GetFiles(d, "*.htm"))
					{
						firstFile = f;
					}
				}

			return firstFile;
		}

		public void ExtractArchive(string filename, string outputDirectory)
		{
			ZipInputStream myZipInputStream = new ZipInputStream(File.OpenRead(filename));

			ZipEntry myZipEntry;

			while ((myZipEntry = myZipInputStream.GetNextEntry()) != null)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(outputDirectory, myZipEntry.Name)));

				if (!myZipEntry.IsDirectory)
				{
					if (myZipEntry.Name.Length > 0)
					{
						FileStream myFileStream = File.Create(Path.Combine(outputDirectory, myZipEntry.Name));

						int size = 2048;
						byte[] data = new byte[2048];

						while (true)
						{
							size = myZipInputStream.Read(data, 0, data.Length);

							if (size > 0)
							{
								myFileStream.Write(data, 0, size);
							}
							else
							{
								break;
							}
						}

						myFileStream.Close();
					}
				}
				else
				{
					Directory.CreateDirectory(Path.GetDirectoryName(Path.Combine(outputDirectory, myZipEntry.Name)));
				}
			}

			myZipInputStream.Close();

		}

		private void EstProduct_TextChanged(object sender, EventArgs e)
		{

		}

	}
}
