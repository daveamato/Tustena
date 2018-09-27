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
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.ERP
{
	public partial class PrintQuote : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				CloseWindow();
			}
			else
			{
				if (!Page.IsPostBack && Request["qid"]!=null)
				{
					btnPrint.Text=Root.rm.GetString("Esttxt42");
					chkheader.RepeatColumns=4;
					chkheader.RepeatDirection=RepeatDirection.Horizontal;
					chkheader.Items.Insert(0, Root.rm.GetString("PDFOff22"));
					chkheader.Items.Insert(1, Root.rm.GetString("PDFOff20"));
					chkheader.Items.Insert(2, Root.rm.GetString("PDFOff21"));
					chkheader.Items.Insert(3, Root.rm.GetString("PDFOff26"));

					chkheader.Items[0].Selected=true;

					QuoteId.Text=Request["qid"];
					DataTable dt;

					dt= DatabaseConnection.CreateDataset("SELECT COMPANYNAME,ADDRESS+' '+CITY+' '+PROVINCE+' '+ZIPCODE+' '+STATE+' PHONE '+PHONE+' FAX '+FAX+' '+EMAIL AS COMPANYADDRESS  FROM TUSTENA_DATA").Tables[0];
					CompanyName.Text = dt.Rows[0]["companyname"].ToString();
					CompanyAddress.Text = dt.Rows[0]["companyaddress"].ToString();

					string pathTemplate;
					pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
					FileFunctions.CheckDir(pathTemplate,true);
					DirectoryInfo dirInfo = new DirectoryInfo(pathTemplate);

					FileInfo[] files = dirInfo.GetFiles();
					if(files.Length>0)
					{
						chkLogo.RepeatColumns=4;
						chkLogo.RepeatDirection=RepeatDirection.Horizontal;
						foreach(FileInfo f in files)
						{
							chkLogo.Items.Add(f.Name);
						}
						chkLogo.Items[0].Selected=true;
					}



				}else
				{
					CloseWindow();
				}
			}
		}

        private void MakePDFOrder()
        {
            DataTable dtmain;
			dtmain = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM ORDERS WHERE ID={0}",QuoteId.Text)).Tables[0];
            if (dtmain.Rows.Count > 0)
            {
                string CurrencySymbol = "";
                PDFOffer pdf = new PDFOffer();
                pdf.PrintType = PDFType.Order;
                pdf.offerSubject = dtmain.Rows[0]["Subject"].ToString();

                pdf.offerNumber = dtmain.Rows[0]["Number"].ToString();

                string customer = string.Empty;
                switch (Convert.ToInt32(dtmain.Rows[0]["CrossType"]))
                {
                    case 0:
                        customer = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + dtmain.Rows[0]["CrossID"].ToString());
                        break;
                    case 1:
                        customer = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID=" + dtmain.Rows[0]["CrossID"].ToString());
                        break;
                    case 2:
                        customer = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID=" + dtmain.Rows[0]["CrossID"].ToString());
                        break;
                }

                if (dtmain.Rows[0]["Address"].ToString().Length > 0)
                    pdf.billTo = customer + "\n" + dtmain.Rows[0]["Address"].ToString();
                else
                    pdf.billTo = customer + "\n";

                if (dtmain.Rows[0]["ShipAddress"].ToString().Length > 0)
                    pdf.shipTo = customer + "\n" + dtmain.Rows[0]["ShipAddress"].ToString();
                else
                    pdf.shipTo = customer + "\n";

                switch (chkheader.SelectedIndex)
                {
                    case 0:
                        pdf.companyName = null;
                        pdf.companyInfo = null;
                        break;
                    case 1:
                        pdf.companyName = (CompanyName.Text.Length > 0) ? CompanyName.Text : null;
                        pdf.companyInfo = (CompanyAddress.Text.Length > 0) ? CompanyAddress.Text : null;
                        break;
                    case 2:
                        if (chkLogo.Items.Count > 0 && chkLogo.SelectedIndex > -1)
                        {
                            string pathTemplate;
							pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
                            pdf.logo = Path.Combine(pathTemplate, chkLogo.SelectedItem.Text);
                        }
                        break;
                    case 3:
                        pdf.companyName = (CompanyName.Text.Length > 0) ? CompanyName.Text : null;
                        pdf.companyInfo = (CompanyAddress.Text.Length > 0) ? CompanyAddress.Text : null;
                        if (chkLogo.Items.Count > 0 && chkLogo.SelectedIndex > -1)
                        {
                            string pathTemplate;
							pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
                            pdf.logo = Path.Combine(pathTemplate, chkLogo.SelectedItem.Text);
                        }
                        break;

                }

                pdf.offerDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["QuoteDate"]));
                pdf.offerDateValidity = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ExpirationDate"]));

                if (dtmain.Rows[0]["ShipId"] != DBNull.Value)
                {
                    string[] offerShip = DatabaseConnection.SqlScalar("SELECT DESCRIPTION+'|'+CAST(REQUIREDDATE AS VARCHAR(1)) AS X FROM QUOTESHIPMENT WHERE ID=" + dtmain.Rows[0]["ShipId"].ToString()).Split('|');
                    pdf.offerShipDescription = offerShip[0];
                    pdf.offerPrintShipDate = (offerShip[1] == "1");
                }

                if (dtmain.Rows[0]["ShipDate"] != DBNull.Value && UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ShipDate"])).ToShortDateString() != UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["QuoteDate"])).ToShortDateString())
                    pdf.offerShipDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ShipDate"]));
                else
                    pdf.offerShipDate = DateTime.MinValue;

                pdf.offerPayment = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM PAYMENTLIST WHERE ID=" + dtmain.Rows[0]["PaymentID"].ToString());
                pdf.salesMan = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + dtmain.Rows[0]["Ownerid"].ToString());
                if (dtmain.Rows[0]["Ship"] != System.DBNull.Value)
                    pdf.ShippingCost = CurrencySymbol + ((decimal)dtmain.Rows[0]["Ship"]).ToString("N2");
                else
                    pdf.ShippingCost = CurrencySymbol + (0).ToString("N2");
                if (dtmain.Rows[0]["subTotal"] != System.DBNull.Value)
                    pdf.subTotal = CurrencySymbol + ((decimal)dtmain.Rows[0]["subTotal"]).ToString("N2");
                else
                    pdf.subTotal = CurrencySymbol + (0).ToString("N2");

                if (dtmain.Rows[0]["GrandTotal"] != System.DBNull.Value)
                    pdf.Total = CurrencySymbol + ((decimal)dtmain.Rows[0]["GrandTotal"]).ToString("N2");
                else
                    pdf.Total = CurrencySymbol + (0).ToString("N2");

                pdf.footerNote = dtmain.Rows[0]["Description"].ToString();


                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("codice"));
                dt.Columns.Add(new DataColumn("prodotto"));
                dt.Columns.Add(new DataColumn("um"));
                dt.Columns.Add(new DataColumn("qta"));
                dt.Columns.Add(new DataColumn("iva"));
                dt.Columns.Add(new DataColumn("sconto"));
                dt.Columns.Add(new DataColumn("prezzolistino"));
                dt.Columns.Add(new DataColumn("prezzounitario"));
                dt.Columns.Add(new DataColumn("prezzo"));

                StringBuilder sbrow = new StringBuilder();

                sbrow.AppendFormat("SELECT * FROM ORDERROWS WHERE ORDERID={0}", QuoteId.Text);


                DataTable dtRow = DatabaseConnection.CreateDataset(sbrow.ToString()).Tables[0];
                decimal ReductionSum = 0;
                decimal ListPriceSum = 0;
                decimal UnitPriceSum = 0;
                decimal TaxTotal = 0;
                string UnitMesureSum = String.Empty;
                bool useCode = false;
                foreach (DataRow drr in dtRow.Rows)
                {

                    DataRow dr = dt.NewRow();

                    string lblCode = drr["ProductCode"].ToString();
                    if (!useCode && lblCode.Length > 0) useCode = true;
                    string lblProduct = drr["Description"].ToString();

                    if (drr["Description2"].ToString().Length > 0)
                    {
                        if (drr["CatalogId"] != System.DBNull.Value && drr["CatalogId"].ToString().Length > 0 && drr["CatalogId"].ToString() != "0")
                        {
                            if ((bool)DatabaseConnection.SqlScalartoObj("SELECT PRINTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID=" + drr["CatalogID"].ToString()))
                                lblProduct += "\n" + drr["Description2"].ToString();
                        }
                    }


                    string lblUM = drr["UnitMeasure"].ToString();
                    UnitMesureSum += drr["UnitMeasure"].ToString().Trim();
                    string lblQta = drr["Qta"].ToString();
                    string lblTAX = Math.Round(Convert.ToDouble(drr["tax"]), 2).ToString();
                    if (lblTAX.EndsWith(",00")) lblTAX.Substring(0, lblTAX.Length - 3);
                    lblTAX += "%";

                    decimal discount = (decimal.Round((decimal)(drr["ListPrice"]), 2) - decimal.Round((decimal)(drr["Uprice"]), 2)) * 100 / decimal.Round((decimal)(drr["ListPrice"]), 2);
                    string lblDiscount = decimal.Round(discount, 2) + "%";
                    ReductionSum += discount;
                    ListPriceSum += decimal.Round((decimal)(drr["ListPrice"]), 2);
                    UnitPriceSum += decimal.Round((decimal)(drr["Uprice"]), 2);
                    string lblList = CurrencySymbol + decimal.Round((decimal)(drr["ListPrice"]), 2).ToString();
                    string lblUnitPrice = CurrencySymbol + decimal.Round((decimal)(drr["Uprice"]), 2).ToString();
                    string lblPrice = CurrencySymbol + decimal.Round((Convert.ToDecimal((decimal)(drr["Uprice"])) * Convert.ToDecimal(drr["Qta"].ToString())), 2).ToString();

                    TaxTotal += (((decimal)(drr["Uprice"])) * ((decimal)(drr["tax"])) / 100) * Convert.ToDecimal(drr["Qta"].ToString());

                    dr.ItemArray = new string[9] { lblCode, lblProduct, lblUM, lblQta, lblTAX, lblDiscount, lblList, lblUnitPrice, lblPrice };

                    dt.Rows.Add(dr);
                }

                if (((decimal)dtmain.Rows[0]["Ship"]) > 0)
                {
                    TaxTotal += ((decimal)(dtmain.Rows[0]["Ship"])) * ((decimal)(dtmain.Rows[0]["ShipVat"])) / 100;
                }
                pdf.vat = TaxTotal.ToString("N2");

                pdf.useItemCode = useCode;
                if (UnitMesureSum.Length > 0)
                    pdf.useUM = true;
                pdf.useVat = true;
                if (ReductionSum > 0)
                    pdf.useDiscount = true;
                if (UnitPriceSum != ListPriceSum)
                    pdf.useListPrice = true;
                pdf.gridTable = dt;

                if (this.chkProductAttach.Checked)
                {
                    if (dtRow.Rows.Count > 0)
                    {
                        string files = string.Empty;
                        foreach (DataRow drr in dtRow.Rows)
                        {
                            if (drr["CatalogID"] != DBNull.Value && drr["CatalogID"].ToString().Length > 0)
                            {
                                string docname = DatabaseConnection.SqlScalar("SELECT DOCUMENT FROM CATALOGPRODUCTS WHERE ID=" + drr["CatalogID"].ToString());
                                if (Path.GetExtension(docname) == ".pdf")
                                {
									files+=Path.Combine(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "catalog",docname)+";";
                                }
                            }
                        }

                        if (files.Length > 0)
                            pdf.appendFiles = files.Substring(0, files.Length - 1);
                    }
                }

                if (this.chkAttachment.Checked)
                {
                    DataTable dtattach = DatabaseConnection.CreateDataset("SELECT * FROM ORDERDOCUMENT WHERE ORDERID=" + this.QuoteId.Text).Tables[0];
                    if (dtattach.Rows.Count > 0)
                    {
                        string files = string.Empty;
                        foreach (DataRow drattach in dtattach.Rows)
                        {
                            string docname = DatabaseConnection.SqlScalar("SELECT GUID FROM FILEMANAGER WHERE ID=" + drattach["documentid"].ToString());
							files+=Path.Combine(ConfigSettings.DataStoragePath,docname+".pdf")+";";
                        }
                        pdf.appendFiles = files.Substring(0, files.Length - 1);
                    }
                }

                pdf.GetPDF(Response);

            }
        }

		private void MakePDF()
		{
			DataTable dtmain;
			dtmain = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM QUOTES WHERE ID={0}",QuoteId.Text)).Tables[0];
			if(dtmain.Rows.Count>0)
			{
				string CurrencySymbol = "";
				PDFOffer pdf = new PDFOffer();

				pdf.offerSubject = dtmain.Rows[0]["Subject"].ToString();

				pdf.offerNumber = dtmain.Rows[0]["Number"].ToString();

				string customer=string.Empty;
				switch(Convert.ToInt32(dtmain.Rows[0]["CrossType"]))
				{
					case 0:
						customer = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID="+dtmain.Rows[0]["CrossID"].ToString());
						break;
					case 1:
						customer = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID="+dtmain.Rows[0]["CrossID"].ToString());
						break;
					case 2:
						customer = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID="+dtmain.Rows[0]["CrossID"].ToString());
						break;
				}

				if(dtmain.Rows[0]["Address"].ToString().Length>0)
					pdf.billTo = customer+"\n"+dtmain.Rows[0]["Address"].ToString();
				else
					pdf.billTo = customer+"\n";

				if(dtmain.Rows[0]["ShipAddress"].ToString().Length>0)
					pdf.shipTo = customer+"\n"+dtmain.Rows[0]["ShipAddress"].ToString();
				else
					pdf.shipTo = customer+"\n";

				switch(chkheader.SelectedIndex)
				{
					case 0:
						pdf.companyName = null;
						pdf.companyInfo = null;
						break;
					case 1:
						pdf.companyName = (CompanyName.Text.Length>0)?CompanyName.Text:null;
						pdf.companyInfo = (CompanyAddress.Text.Length>0)?CompanyAddress.Text:null;
						break;
					case 2:
						if(chkLogo.Items.Count>0 && chkLogo.SelectedIndex>-1)
						{
							string pathTemplate;
							pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
							pdf.logo = Path.Combine(pathTemplate,chkLogo.SelectedItem.Text);
						}
						break;
					case 3:
						pdf.companyName = (CompanyName.Text.Length>0)?CompanyName.Text:null;
						pdf.companyInfo = (CompanyAddress.Text.Length>0)?CompanyAddress.Text:null;
						if(chkLogo.Items.Count>0 && chkLogo.SelectedIndex>-1)
						{
							string pathTemplate;
							pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
							pdf.logo = Path.Combine(pathTemplate,chkLogo.SelectedItem.Text);
						}
						break;

				}

				pdf.offerDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["QuoteDate"]));
				pdf.offerDateValidity = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ExpirationDate"]));

                if (dtmain.Rows[0]["ShipId"] != DBNull.Value)
                {
                    string[] offerShip = DatabaseConnection.SqlScalar("SELECT DESCRIPTION+'|'+CAST(REQUIREDDATE AS VARCHAR(1)) AS X FROM QUOTESHIPMENT WHERE ID=" + dtmain.Rows[0]["ShipId"].ToString()).Split('|');
                    pdf.offerShipDescription = offerShip[0];
                    pdf.offerPrintShipDate = (offerShip[1] == "1");
                }
                if (dtmain.Rows[0]["ShipDate"] != DBNull.Value && UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ShipDate"])).ToShortDateString() != UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["QuoteDate"])).ToShortDateString())
                    pdf.offerShipDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(dtmain.Rows[0]["ShipDate"]));
                else
                    pdf.offerShipDate = DateTime.MinValue;


				pdf.offerPayment = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM PAYMENTLIST WHERE ID="+dtmain.Rows[0]["PaymentID"].ToString());
				pdf.salesMan = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+dtmain.Rows[0]["Ownerid"].ToString());
				if(dtmain.Rows[0]["Ship"]!=System.DBNull.Value)
                    pdf.ShippingCost = CurrencySymbol + ((decimal)dtmain.Rows[0]["Ship"]).ToString("N2");
				else
					pdf.ShippingCost = CurrencySymbol+(0).ToString("N2");
				if(dtmain.Rows[0]["subTotal"]!=System.DBNull.Value)
					pdf.subTotal = CurrencySymbol+((decimal)dtmain.Rows[0]["subTotal"]).ToString("N2");
				else
					pdf.subTotal = CurrencySymbol+(0).ToString("N2");

				if(dtmain.Rows[0]["GrandTotal"]!=System.DBNull.Value)
					pdf.Total = CurrencySymbol+((decimal)dtmain.Rows[0]["GrandTotal"]).ToString("N2");
				else
					pdf.Total = CurrencySymbol+(0).ToString("N2");

				pdf.footerNote = dtmain.Rows[0]["Description"].ToString();


				DataTable dt = new DataTable();

				dt.Columns.Add(new DataColumn("codice"));
				dt.Columns.Add(new DataColumn("prodotto"));
				dt.Columns.Add(new DataColumn("um"));
				dt.Columns.Add(new DataColumn("qta"));
				dt.Columns.Add(new DataColumn("iva"));
				dt.Columns.Add(new DataColumn("sconto"));
				dt.Columns.Add(new DataColumn("prezzolistino"));
				dt.Columns.Add(new DataColumn("prezzounitario"));
				dt.Columns.Add(new DataColumn("prezzo"));

				StringBuilder sbrow = new StringBuilder();

				sbrow.AppendFormat("SELECT * FROM QUOTEROWS WHERE ESTIMATEID={0}",QuoteId.Text);


				DataTable dtRow = DatabaseConnection.CreateDataset(sbrow.ToString()).Tables[0];
				decimal ReductionSum = 0;
				decimal ListPriceSum = 0;
				decimal UnitPriceSum = 0;
				decimal TaxTotal=0;
				string UnitMesureSum = String.Empty;
				bool useCode=false;
				foreach(DataRow drr in dtRow.Rows)
				{

					DataRow dr = dt.NewRow();

					string lblCode=drr["ProductCode"].ToString();
					if(!useCode && lblCode.Length>0)useCode=true;
					string lblProduct=drr["Description"].ToString();

					if(drr["Description2"].ToString().Length>0)
					{
						if(drr["CatalogId"]!=System.DBNull.Value && drr["CatalogId"].ToString().Length>0 && drr["CatalogId"].ToString()!="0")
						{
							if((bool)DatabaseConnection.SqlScalartoObj("SELECT PRINTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID="+drr["CatalogID"].ToString()))
								lblProduct+="\n"+drr["Description2"].ToString();
						}
					}


					string lblUM=drr["UnitMeasure"].ToString();
					UnitMesureSum += drr["UnitMeasure"].ToString().Trim();
					string lblQta=drr["Qta"].ToString();
					string lblTAX = Math.Round(Convert.ToDouble(drr["tax"]), 2).ToString();
					if(lblTAX.EndsWith(",00")) lblTAX.Substring(0,lblTAX.Length-3);
					lblTAX+="%";

					decimal discount = (decimal.Round((decimal)(drr["ListPrice"]),2)-decimal.Round((decimal)(drr["Uprice"]),2))*100/decimal.Round((decimal)(drr["ListPrice"]),2);
					string lblDiscount=decimal.Round(discount,2)+"%";
					ReductionSum += discount;
					ListPriceSum += decimal.Round((decimal)(drr["ListPrice"]),2);
					UnitPriceSum += decimal.Round((decimal)(drr["Uprice"]),2);
					string lblList=CurrencySymbol+decimal.Round((decimal)(drr["ListPrice"]),2).ToString();
					string lblUnitPrice=CurrencySymbol+decimal.Round((decimal)(drr["Uprice"]),2).ToString();
					string lblPrice=CurrencySymbol+decimal.Round((Convert.ToDecimal((decimal)(drr["Uprice"]))*Convert.ToDecimal(drr["Qta"].ToString())),2).ToString();

					TaxTotal+=(((decimal)(drr["Uprice"]))*((decimal)(drr["tax"]))/100)*Convert.ToDecimal(drr["Qta"].ToString());

					dr.ItemArray = new string[9]{lblCode,lblProduct,lblUM,lblQta,lblTAX,lblDiscount,lblList,lblUnitPrice,lblPrice};

					dt.Rows.Add(dr);
				}

				if(((decimal)dtmain.Rows[0]["Ship"])>0)
				{
					TaxTotal+=((decimal)(dtmain.Rows[0]["Ship"]))*((decimal)(dtmain.Rows[0]["ShipVat"]))/100;
				}
				pdf.vat = TaxTotal.ToString("N2");

				pdf.useItemCode=useCode;
				if(UnitMesureSum.Length>0)
					pdf.useUM = true;
				pdf.useVat = true;
				if(ReductionSum > 0)
					pdf.useDiscount = true;
				if(UnitPriceSum != ListPriceSum)
					pdf.useListPrice = true;
				pdf.gridTable = dt;

				if(this.chkProductAttach.Checked)
				{
					if(dtRow.Rows.Count>0)
					{
						string files=string.Empty;
						foreach(DataRow drr in dtRow.Rows)
						{
							if(drr["CatalogID"]!=DBNull.Value && drr["CatalogID"].ToString().Length>0)
							{
								string docname = DatabaseConnection.SqlScalar("SELECT DOCUMENT FROM CATALOGPRODUCTS WHERE ID="+drr["CatalogID"].ToString());
								if(Path.GetExtension(docname)==".pdf")
								{
									files+=Path.Combine(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "catalog",docname)+";";
								}
							}
						}

						if(files.Length>0)
							pdf.appendFiles	= files.Substring(0,files.Length-1);
					}
				}

				if(this.chkAttachment.Checked)
				{
					DataTable dtattach = DatabaseConnection.CreateDataset("SELECT * FROM QUOTEDOCUMENT WHERE QUOTEID="+this.QuoteId.Text).Tables[0];
					if(dtattach.Rows.Count>0)
					{
						string files=string.Empty;
						foreach(DataRow drattach in dtattach.Rows)
						{
							string docname = DatabaseConnection.SqlScalar("SELECT GUID FROM FILEMANAGER WHERE ID="+drattach["documentid"].ToString());
							files+=Path.Combine(ConfigSettings.DataStoragePath,docname+".pdf")+";";
						}
						pdf.appendFiles	= files.Substring(0,files.Length-1);
					}
				}

				pdf.GetPDF(Response);

			}
		}

		private void CloseWindow()
		{
			ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>opener.location.href=opener.location.href;self.close();</script>");
		}



		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.btnPrint.Click+=new EventHandler(btnPrint_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void btnPrint_Click(object sender, EventArgs e)
		{
            if (Request["order"] != null)
                MakePDFOrder();
            else
			    MakePDF();
		}
	}
}
