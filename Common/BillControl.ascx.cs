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
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Common
{
	public partial class BillControl : UserControl
	{
		private UserConfig UC = new UserConfig();

		private long refID = 0;
		private RefType billRefType = RefType.Company;
		private bool fromQuote = false;
		private long purchaseID = 0;
		private long quoteID = 0;

		public long QuoteID
		{
			set
			{
				this.quoteID = value;
				ViewState["quoteID"] = value;
			}
			get { return this.quoteID; }
		}

		public long PurchaseID
		{
			set
			{
				this.purchaseID = value;
				ViewState["purchaseID"] = value;
			}
			get { return this.purchaseID; }
		}

		public long RefID
		{
			set
			{
				this.refID = value;
				ViewState["refID"] = value;
			}
			get { return this.refID; }
		}

		public RefType BillRefType
		{
			set
			{
				this.billRefType = value;
				ViewState["billRefType"] = value;
			}
			get { return this.billRefType; }
		}

		public bool FromQuote
		{
			set
			{
				this.fromQuote = value;
				ViewState["fromQuote"] = value;
			}
			get { return this.fromQuote; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

			BtnAddProduct.Text = Root.rm.GetString("CRMcontxt74");
			BtnCalcPrice.Text = Root.rm.GetString("CRMcontxt75");
			BtnAddFreeProductOpp.Text = Root.rm.GetString("CRMcontxt74");
			Purchase_Submit.Text = Root.rm.GetString("CRMcontxt34");
			Purchase_Exit.Text = Root.rm.GetString("CRMcontxt43");
			NewPurchase.Text = Root.rm.GetString("CRMcontxt38");
			NewPurchase.Visible = false;

			if (ViewState["quoteID"] != null) QuoteID = (long) ViewState["quoteID"];
			if (ViewState["purchaseID"] != null) PurchaseID = (long) ViewState["purchaseID"];
			if (ViewState["refID"] != null) RefID = (long) ViewState["refID"];
			if (ViewState["billRefType"] != null) BillRefType = (RefType) ViewState["billRefType"];
			if (ViewState["fromQuote"] != null) FromQuote = (bool) ViewState["fromQuote"];
		}

		protected void FillPurchase()
		{
			string sqlString = string.Empty;
			switch (BillRefType)
			{
				case RefType.Company:
					sqlString = "SELECT CRM_BILL.* FROM CRM_BILL ";
					sqlString += "WHERE CRM_BILL.COMPANYID=" + RefID + " ORDER BY CRM_BILL.BILLINGDATE,CRM_BILL.BILLNUMBER;";
					break;
				case RefType.Contact:
					break;
				case RefType.Lead:
					break;
			}

			RepeaterPurchase.DataSource = DatabaseConnection.CreateDataset(sqlString);
			RepeaterPurchase.DataBind();
			if (RepeaterPurchase.Items.Count > 0)
			{
				RepeaterPurchase.Visible = true;
				RepeaterPurchaseInfo.Visible = false;
			}
			else
			{
				RepeaterPurchase.Visible = false;
				RepeaterPurchaseInfo.Text = Root.rm.GetString("CRMcontxt35");
			}
			CardPurchase.Visible = false;
		}

		public enum RefType
		{
			Company = 0,
			Contact,
			Lead
		}

		private void btnAddFreeProductOpp_Click(object sender, EventArgs e)
		{
			ArrayList np = new ArrayList();
			if (Session["newBillProd"] != null)
				np = (ArrayList) Session["newBillProd"];

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
			newprod.Reduction = 0;
			newprod.ObId = np.Count;
			np.Add(newprod);


			Session["newBillProd"] = np;
			RepeaterPurchaseProduct.DataSource = np;
			RepeaterPurchaseProduct.DataBind();
			RepeaterPurchaseProduct.Visible = true;

			EstFreeProduct.Text = string.Empty;
			EstFreeUm.Text = string.Empty;
			EstFreeQta.Text = string.Empty;
			EstFreeUp.Text = string.Empty;
			EstFreeVat.Text = string.Empty;
			EstFreePf.Text = string.Empty;
		}

		public void btnAddProduct_Click(object sender, EventArgs e)
		{
			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id = Convert.ToInt64(Purchase_ProductID.Text);
			newprod.ShortDescription = Purchase_Product.Text;
			newprod.LongDescription = Purchase_Description2.Text;
			newprod.UM = Purchase_Um.Text;
			newprod.Qta = Convert.ToDouble(Purchase_Qta.Text);
			newprod.UnitPrice = StaticFunctions.FixDecimal(Purchase_Up.Text);
			newprod.Vat = (Purchase_Vat.Text.Length > 0) ? Convert.ToDecimal(Purchase_Vat.Text) : 0;
			newprod.ListPrice = StaticFunctions.FixDecimal(Purchase_Pl.Text);
			newprod.FinalPrice = StaticFunctions.FixDecimal(Purchase_Pf.Text);

			ArrayList np = new ArrayList();
			if (Session["newBillProd"] != null)
				np = (ArrayList) Session["newBillProd"];

			newprod.ObId = np.Count;

			np.Add(newprod);
			Session["newBillProd"] = np;

			RepeaterPurchaseProduct.DataSource = np;
			RepeaterPurchaseProduct.DataBind();
			RepeaterPurchaseProduct.Visible = true;

		}


		public void PurchaseProductCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelPurPro":
					Literal ObjectID = (Literal) e.Item.FindControl("ObjectID");
					ArrayList np = new ArrayList();
					np = (ArrayList) Session["newBillProd"];
					RemoveAndRestoreProduct(ref np, Convert.ToInt32(ObjectID.Text));
					Session["newBillProd"] = np;
					RepeaterPurchaseProduct.DataSource = np;
					RepeaterPurchaseProduct.DataBind();
					RepeaterPurchaseProduct.Visible = true;
					break;
				case "ModPurPro":
					ObjectID = (Literal) e.Item.FindControl("ObjectID");
					np = new ArrayList();
					np = (ArrayList) Session["newBillProd"];
					PurchaseProduct pProd = (PurchaseProduct) np[Convert.ToInt32(ObjectID.Text)];
					if (pProd.id == 0)
					{
						EstFreeProduct.Text = pProd.ShortDescription;
						EstFreeProduct.Text = pProd.LongDescription;
						EstFreeUm.Text = pProd.UM;
						EstFreeQta.Text = Convert.ToInt32(pProd.Qta).ToString();
						EstFreeUp.Text = Convert.ToDecimal(pProd.UnitPrice).ToString();
						EstFreeVat.Text = Convert.ToInt32(pProd.Vat).ToString();
						EstFreePf.Text = Convert.ToDecimal(pProd.ListPrice).ToString();
						EstFreePf.Text = Convert.ToDecimal(pProd.FinalPrice).ToString();
					}
					else
					{
						Purchase_ProductID.Text = pProd.id.ToString();
						Purchase_Product.Text = pProd.ShortDescription;
						Purchase_Description2.Text = pProd.LongDescription;
						Purchase_Um.Text = pProd.UM;
						Purchase_Qta.Text = pProd.Qta.ToString();
						Purchase_Up.Text = pProd.UnitPrice.ToString();
						Purchase_Vat.Text = pProd.Vat.ToString();
						Purchase_Pl.Text = pProd.ListPrice.ToString();
						Purchase_Pf.Text = pProd.FinalPrice.ToString();
					}

					RemoveAndRestoreProduct(ref np, Convert.ToInt32(ObjectID.Text));
					Session["newBillProd"] = np;
					RepeaterPurchaseProduct.DataSource = np;
					RepeaterPurchaseProduct.DataBind();
					RepeaterPurchaseProduct.Visible = true;
					break;
			}
		}


		private void RemoveAndRestoreProduct(ref ArrayList np, int iNdex)
		{
			np.RemoveAt(iNdex);
			for (int i = 0; i < np.Count; i++)
			{
				PurchaseProduct pProd = (PurchaseProduct) np[i];
				pProd.ObId = i;
				np[i] = pProd;
			}
		}

		public void FillProduct()
		{
			ArrayList pp = new ArrayList();
			if (!FromQuote)
			{
				DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_BILL WHERE ID=" + PurchaseID);
				Purchase_ID.Text = PurchaseID.ToString();
				Purchase_BillingDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillingDate"]).ToShortDateString();
				Purchase_PaymentDate.Text = (ds.Tables[0].Rows[0]["PaymentDate"] != DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0]["PaymentDate"]).ToShortDateString() : "";
				Purchase_ExpirationDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"]).ToShortDateString();
				Purchase_BillNumber.Text = ds.Tables[0].Rows[0]["BillNumber"].ToString();

				Purchase_Note.Text = ds.Tables[0].Rows[0]["Note"].ToString();
				Purchase_Payment.Checked = (bool) ds.Tables[0].Rows[0]["Payment"];
				if ((long) ds.Tables[0].Rows[0]["OwnerID"] > 0)
				{
					this.Purchase_TextboxOwnerID.Text = ds.Tables[0].Rows[0]["OwnerID"].ToString();
					this.Purchase_TextboxOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS OWNER FROM ACCOUNT WHERE UID=" + ds.Tables[0].Rows[0]["OwnerID"].ToString());
				}
				else
				{
					this.Purchase_TextboxOwnerID.Text = string.Empty;
					this.Purchase_TextboxOwner.Text = string.Empty;
				}

				ds.Clear();
				ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_BILLROWS WHERE BILLID=" + PurchaseID);

				foreach (DataRow d in ds.Tables[0].Rows)
				{
					PurchaseProduct Pprod = new PurchaseProduct();
					Pprod.id = Convert.ToInt64(d["ProductID"]);
					Pprod.ShortDescription = DatabaseConnection.SqlScalar("SELECT SHORTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID=" + d["ProductID"].ToString());
					Pprod.LongDescription = d["Description"].ToString();
					Pprod.UM = (string) d["UM"];
					Pprod.Qta = (int) d["Qta"];
					Pprod.UnitPrice = (decimal) d["UnitPrice"];
					Pprod.Vat = Convert.ToInt32(d["Tax"]);
					Pprod.ListPrice = (decimal) d["ListPrice"];
					Pprod.FinalPrice = (decimal) d["FinalPrice"];
					Pprod.ObId = pp.Count;
					pp.Add(Pprod);
				}
			}
			else
			{
				DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM QUOTE_VIEW WHERE QUOTEID=" + this.QuoteID);
				Purchase_ID.Text = QuoteID.ToString();
				Purchase_BillingDate.Text = DateTime.Now.ToShortDateString();
				Purchase_PaymentDate.Text = String.Empty;
				Purchase_ExpirationDate.Text = String.Empty;
				Purchase_BillNumber.Text = String.Empty;

				Purchase_Note.Text = ds.Tables[0].Rows[0]["Description"].ToString();
				Purchase_Payment.Checked = false;
				if ((int) ds.Tables[0].Rows[0]["OwnerID"] > 0)
				{
					this.Purchase_TextboxOwnerID.Text = ds.Tables[0].Rows[0]["OwnerID"].ToString();
					this.Purchase_TextboxOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS OWNER FROM ACCOUNT WHERE UID=" + ds.Tables[0].Rows[0]["OwnerID"].ToString());
				}
				else
				{
					this.Purchase_TextboxOwnerID.Text = string.Empty;
					this.Purchase_TextboxOwner.Text = string.Empty;
				}

				ds.Clear();

				DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM ESTIMATEDROWS WHERE ESTIMATEID=" + QuoteID).Tables[0];

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
							newprod.Vat = (pInfo["Vat"] != DBNull.Value) ? (decimal) pInfo["Vat"] : 0;
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

							pp.Add(newprod);
						}
					}
					else
					{
						PurchaseProduct newprod = new PurchaseProduct();
						newprod.id = 0;
						newprod.ShortDescription = ddr["Description"].ToString();
						newprod.LongDescription = ddr["Description"].ToString();
						newprod.UM = "";
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
						pp.Add(newprod);
					}
				}
			}

			Session["newBillProd"] = pp;

			RepeaterPurchaseProduct.DataSource = pp;
			RepeaterPurchaseProduct.DataBind();
			RepeaterPurchaseProduct.Visible = true;
		}

		public void PurchaseProductDatabound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label ShortDescription = (Label) e.Item.FindControl("ShortDescription");
					Label UM = (Label) e.Item.FindControl("UM");
					Label Qta = (Label) e.Item.FindControl("Qta");
					Label UnitPrice = (Label) e.Item.FindControl("UnitPrice");
					Label FinalPrice = (Label) e.Item.FindControl("FinalPrice");
					Literal ObjectID = (Literal) e.Item.FindControl("ObjectID");
					PurchaseProduct newprod = (PurchaseProduct) e.Item.DataItem;
					ShortDescription.Text = newprod.ShortDescription;
					UM.Text = newprod.UM.ToString();
					Qta.Text = newprod.Qta.ToString();
					UnitPrice.Text = newprod.UnitPrice.ToString("c");
					FinalPrice.Text = newprod.FinalPrice.ToString("c");
					ObjectID.Text = newprod.ObId.ToString();
					break;
			}
		}

		public void btnCalcPrice_Click(object sender, EventArgs e)
		{
			try
			{
				TextBox EstPl = (TextBox) Page.FindControl("Purchase_Pl");
				TextBox EstPf = (TextBox) Page.FindControl("Purchase_Pf");
				TextBox EstQta = (TextBox) Page.FindControl("Purchase_Qta");
				TextBox EstUp = (TextBox) Page.FindControl("Purchase_Up");
				EstPl.Text = Convert.ToString(Math.Round((Convert.ToDecimal(EstQta.Text)*Convert.ToDecimal(EstUp.Text)), 2));
				EstPf.Text = EstPl.Text;
			}
			catch
			{
			}
		}

		public void btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "Purchase_Submit":


					rvBillNumber.Validate();
					cvBillingDate.Validate();
					rvBillingDate.Validate();
					rvExpirationDate.Validate();

					if (rvBillNumber.IsValid &&
						cvBillingDate.IsValid &&
						rvBillingDate.IsValid &&
						rvExpirationDate.IsValid)
					{
						InsertNewPurchase(PurchaseID.ToString());

						CardPurchase.Visible = false;
						RepeaterPurchase.Visible = true;

					}

					break;
				case "Purchase_Exit":
					CardPurchase.Visible = false;
					RepeaterPurchase.Visible = true;

					break;
				case "NewPurchase":

					ClearTextBoxPurchase(CardPurchase.Controls.GetEnumerator());
					Session.Remove("newBillProd");
					RepeaterPurchaseProduct.Visible = false;
					CardPurchase.Visible = true;
					RepeaterPurchase.Visible = false;

					NewPurchase.Visible = false;
					GoBackPurc.Visible = true;
					break;
			}
		}

		public void RepeaterPurchaseCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenPurchase":
					Literal PurchaseID = (Literal) e.Item.FindControl("PurchaseID");
					DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_BILL WHERE ID=" + int.Parse(PurchaseID.Text));

					TextBox Purchase_BillingDate = (TextBox) CardPurchase.FindControl("Purchase_BillingDate");
					TextBox Purchase_PaymentDate = (TextBox) CardPurchase.FindControl("Purchase_PaymentDate");
					TextBox Purchase_ExpirationDate = (TextBox) CardPurchase.FindControl("Purchase_ExpirationDate");
					TextBox Purchase_BillNumber = (TextBox) CardPurchase.FindControl("Purchase_BillNumber");

					TextBox Purchase_Note = (TextBox) CardPurchase.FindControl("Purchase_Note");
					CheckBox Purchase_Payment = (CheckBox) CardPurchase.FindControl("Purchase_Payment");
					TextBox Purchase_ID = (TextBox) CardPurchase.FindControl("Purchase_ID");

					Purchase_ID.Text = PurchaseID.Text;
					Purchase_BillingDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["BillingDate"]).ToShortDateString();
					Purchase_PaymentDate.Text = (ds.Tables[0].Rows[0]["PaymentDate"] != DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0]["PaymentDate"]).ToShortDateString() : "";
					Purchase_ExpirationDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"]).ToShortDateString();
					Purchase_BillNumber.Text = ds.Tables[0].Rows[0]["BillNumber"].ToString();

					Purchase_Note.Text = ds.Tables[0].Rows[0]["Note"].ToString();
					Purchase_Payment.Checked = (bool) ds.Tables[0].Rows[0]["Payment"];
					if ((long) ds.Tables[0].Rows[0]["OwnerID"] > 0)
					{
						this.Purchase_TextboxOwnerID.Text = ds.Tables[0].Rows[0]["OwnerID"].ToString();
						this.Purchase_TextboxOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS OWNER FROM ACCOUNT WHERE UID=" + ds.Tables[0].Rows[0]["OwnerID"].ToString());
					}
					else
					{
						this.Purchase_TextboxOwnerID.Text = string.Empty;
						this.Purchase_TextboxOwner.Text = string.Empty;
					}

					ds.Clear();

					ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_BILLROWS WHERE BILLID=" + int.Parse(PurchaseID.Text));
					ArrayList pp = new ArrayList();
					foreach (DataRow d in ds.Tables[0].Rows)
					{
						PurchaseProduct Pprod = new PurchaseProduct();
						Pprod.id = Convert.ToInt64(d["ProductID"]);
						Pprod.ShortDescription = DatabaseConnection.SqlScalar("SELECT SHORTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID=" + d["ProductID"].ToString());
						Pprod.LongDescription = d["Description"].ToString();
						Pprod.UM = (string) d["UM"];
						Pprod.Qta = (int) d["Qta"];
						Pprod.UnitPrice = (decimal) d["UnitPrice"];
						Pprod.Vat = Convert.ToInt32(d["Tax"]);
						Pprod.ListPrice = (decimal) d["ListPrice"];
						Pprod.FinalPrice = (decimal) d["FinalPrice"];
						Pprod.ObId = pp.Count;
						pp.Add(Pprod);
					}

					Session["newBillProd"] = pp;

					RepeaterPurchaseProduct.DataSource = pp;
					RepeaterPurchaseProduct.DataBind();
					RepeaterPurchaseProduct.Visible = true;

					CardPurchase.Visible = true;
					RepeaterPurchase.Visible = false;


					break;
			}
		}

		private void ClearTextBoxPurchase(IEnumerator i)
		{
			while (i.MoveNext())
			{
				if (i.Current.GetType().Name == "TextBox")
				{
					TextBox t = (TextBox) i.Current;
					if (t.ID == "Purchase_ID")
						t.Text = "-1";
					else if (t.ID == "Purchase_RowsNumber")
						t.Text = "1";
					else
						t.Text = String.Empty;
				}
				else
				{
					Control y = (Control) i.Current;
					ClearTextBoxPurchase(y.Controls.GetEnumerator());
				}
			}

		}

		private void InsertNewPurchase(string id)
		{
			decimal subTotal = 0;
			string BillID;
			ArrayList pp = new ArrayList();
			using (DigiDapter dg = new DigiDapter())
			{
				if (Session["newBillProd"] != null)
				{
					pp = (ArrayList) Session["newBillProd"];
					foreach (PurchaseProduct Pprod in pp)
					{
						subTotal += Pprod.FinalPrice;
					}
				}

				dg.Add("BILLINGDATE", Convert.ToDateTime(((TextBox) CardPurchase.FindControl("Purchase_BillingDate")).Text, UC.myDTFI));
				dg.Add("EXPIRATIONDATE", Convert.ToDateTime(((TextBox) CardPurchase.FindControl("Purchase_ExpirationDate")).Text, UC.myDTFI));
				dg.Add("BILLNUMBER", ((TextBox) CardPurchase.FindControl("Purchase_BillNumber")).Text);
				if (((TextBox) CardPurchase.FindControl("Purchase_PaymentDate")).Text.Length > 0)
					dg.Add("PAYMENTDATE", Convert.ToDateTime(((TextBox) CardPurchase.FindControl("Purchase_PaymentDate")).Text, UC.myDTFI));
				dg.Add("PAYMENT", (((CheckBox) CardPurchase.FindControl("Purchase_Payment")).Checked) ? 1 : 0);
				dg.Add("TOTALPRICE", subTotal);

				dg.Add("NOTE", ((TextBox) CardPurchase.FindControl("Purchase_Note")).Text);
				dg.Add("CREATEDDATE", DateTime.UtcNow);
				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("COMPANYID", RefID);
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);

				if (Purchase_TextboxOwnerID.Text.Length > 0 && Convert.ToInt32(Purchase_TextboxOwnerID.Text) > 0)
					dg.Add("OWNERID", Convert.ToInt32(this.Purchase_TextboxOwnerID.Text));
				else
					dg.Add("OWNERID", 0);

				object newId = dg.Execute("CRM_BILL", "ID=" + id, DigiDapter.Identities.Identity);

				BillID = (id == "-1") ? newId.ToString() : id;
			}

			if (id != "-1")
			{
				DatabaseConnection.DoCommand("DELETE FROM CRM_BILLROWS WHERE BILLID=" + id);
			}

			if (pp.Count > 0)
			{
				foreach (PurchaseProduct Pprod in pp)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("CREATEDDATE", DateTime.UtcNow);
						dg.Add("CREATEDBYID", UC.UserId);
						dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
						dg.Add("LASTMODIFIEDBYID", UC.UserId);
						dg.Add("BILLID", BillID);
						dg.Add("DESCRIPTION", Pprod.LongDescription);
						dg.Add("UNITPRICE", Pprod.UnitPrice);
						dg.Add("LISTPRICE", Pprod.ListPrice);
						dg.Add("FINALPRICE", Pprod.FinalPrice);
						dg.Add("TAX", Pprod.Vat);
						dg.Add("PRODUCTID", Pprod.id);
						dg.Add("UM", Pprod.UM);
						dg.Add("QTA", Pprod.Qta);
						dg.Execute("CRM_BILLROWS");
					}
				}
			}
			Session.Remove("newBillProd");

			if (this.FromQuote)
			{
                Page.ClientScript.RegisterStartupScript(this.GetType(), "registrato", "<script>alert('" + Root.rm.GetString("Esttxt45") + "');self.close();</script>");
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
			this.BtnAddFreeProductOpp.Click += new EventHandler(this.btnAddFreeProductOpp_Click);
		}

		#endregion
	}
}
