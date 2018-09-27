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
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Ajax;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.ERP
{
	public partial class QuoteEditing : G
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				this.InitAjax();

				QSubjectValidator.ErrorMessage =Root.rm.GetString("Quotxt10");
				TextboxSearchOwnerIDValidator.ErrorMessage =Root.rm.GetString("Quotxt8");
				QValidDataValidator.ErrorMessage =Root.rm.GetString("Quotxt4");
				QuoteDataValidator.ErrorMessage =Root.rm.GetString("Quotxt35");
				CrossWithIDValidator.ErrorMessage =Root.rm.GetString("Tictxt38");
				valSum.HeaderText=Root.rm.GetString("ValidSummary");

				btnSave.Attributes.Add("onclick","return calctotal();");
				btnSave2.Attributes.Add("onclick","return calctotal();");
				this.btnClone.Visible=false;
				this.btnCopyToOrder.Visible=false;

                ShipDescription.Attributes.Add("onchange", "CheckShipConfiguration()");
                PriceList.Attributes.Add("onchange", "ChangeList()");

				if(!Page.IsPostBack)
				{
					FillQpaymant();
					FillQuoteStage(this.QStage, true, false);
                    FillShipDescription();

					chkIncludePDFDoc.Text=Root.rm.GetString("Quotxt34");
					btnSave.Text=Root.rm.GetString("Save");
					btnSave2.Text=Root.rm.GetString("Save");
					btnClone.Text=Root.rm.GetString("Quotxt39");
					btnCopyToOrder.Text=Root.rm.GetString("Quotxt42");

					ListItem li = new ListItem(Root.rm.GetString("Mailtxt13"),"0");
					CrossWith.Items.Add(li);
					li = new ListItem(Root.rm.GetString("Mailtxt14"),"1");
					CrossWith.Items.Add(li);
                    Modules M = new Modules();
                    M.ActiveModule = UC.Modules;
                    if (M.IsModule(ActiveModules.Lead))
                    {
                        li = new ListItem(Root.rm.GetString("Mailtxt15"), "2");
                        CrossWith.Items.Add(li);
                    }
					CrossWith.RepeatDirection=RepeatDirection.Horizontal;
					CrossWith.Items[0].Selected=true;

					if(Session["ActivityID"]!=null)
					{
						ActivityID=Convert.ToInt32(Session["ActivityID"]);
						Session.Remove("ActivityID");
					}
					else
						ActivityID=0;

                    if (Session["ViewQuote"] != null)
                    {
                        this.FillQuote(Convert.ToInt64(Session["ViewQuote"]));
                        Session.Remove("ViewQuote");
                    }
                    else
                    {
                        newQuote();
                        QuoteID = -1;
                    }
				}
			}
		}

        private void FillPriceList()
        {
            FillPriceList(0);
        }

        private void FillPriceList(long pl)
        {
            PriceList.DataValueField = "ID";
            PriceList.DataTextField = "DESCRIPTION";

            PriceList.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CATALOGPRICELISTDESCRIPTION").Tables[0];
            PriceList.DataBind();
            PriceList.Items.Insert(0, new ListItem(Root.rm.GetString("Captxt37"), "0"));

            string currentlist=pl.ToString();
            if(pl==0)
                currentlist = DatabaseConnection.SqlScalar("SELECT LISTPRICE FROM ACCOUNT WHERE UID="+UC.UserId);

            PriceList.SelectedIndex = 0;
            if (currentlist.Length > 0)
            {
                PriceList.SelectedIndex = -1;
                foreach (ListItem li in PriceList.Items)
                {
                    if (li.Value == currentlist)
                    {
                        li.Selected = true;
                        PriceList.Enabled = false;
                        currentPriceList.Text = li.Value;
                        break;
                    }
                }
                if (PriceList.SelectedIndex == -1)
                {
                    PriceList.SelectedIndex = 0;
                    currentPriceList.Text = "0";
                }
            }else
                currentPriceList.Text = "0";

        }

		public long QuoteID
		{
			get
			{
				object o = this.ViewState["_QuoteID"];
				if (o == null)
					return 0;
				else
					return (long)o;
			}
			set
			{
				this.ViewState["_QuoteID"] = value;
			}
		}

		public int ActivityID
		{
			get
			{
				object o = this.ViewState["_ActivityID"];
				if (o == null)
					return 0;
				else
					return (int)o;
			}
			set
			{
				this.ViewState["_ActivityID"] = value;
			}
		}

		private void FillQpaymant()
		{
			QPayment.DataTextField="description";
			QPayment.DataValueField="id";
			QPayment.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM PAYMENTLIST ORDER BY VIEWORDER");
			QPayment.DataBind();
			QPayment.Items.Insert(0,new ListItem(Root.rm.GetString("Choose"),"0"));

		}

        private void FillShipDescription()
        {
            ShipDescription.DataTextField = "DESCRIPTION";
            ShipDescription.DataValueField = "IDKEYS";
			ShipDescription.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM QUOTESHIPMENT");
            ShipDescription.DataBind();
            ShipDescription.Items.Insert(0, new ListItem(Root.rm.GetString("Choose"), "0"));

        }

		public static void FillQuoteStage(DropDownList dDL, bool firstelement)
		{
			FillQuoteStage(dDL, firstelement, true);
		}
		public static void FillQuoteStage(DropDownList dDL, bool firstelement,bool checkChange)
		{
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.Draft).ToString()), ((int) QuoteStage.Draft).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.Negotiation).ToString()), ((int) QuoteStage.Negotiation).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.Submitted).ToString()), ((int) QuoteStage.Submitted).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.Delayed).ToString()), ((int) QuoteStage.Delayed).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.Confirmed).ToString()), ((int) QuoteStage.Confirmed).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.ClosedAccepted).ToString()), ((int) QuoteStage.ClosedAccepted).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.ClosedLost).ToString()), ((int) QuoteStage.ClosedLost).ToString()));
			dDL.Items.Add(new ListItem(Root.rm.GetString("Qstgtxt" + ((int) QuoteStage.ClosedAbandoned).ToString()), ((int) QuoteStage.ClosedAbandoned).ToString()));

			if (firstelement)
			{
				dDL.Items.Insert(0,Root.rm.GetString("Choose"));
				dDL.SelectedIndex = 0;
				dDL.Items[0].Value = "-1";
			}

			if(checkChange)
				dDL.Attributes.Add("onchange", "QuoteWon()");
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.btnSave.Click += new EventHandler(this.btnSave_Click);
			this.btnSave2.Click += new EventHandler(this.btnSave_Click);
			this.btnClone.Click+=new EventHandler(btnClone_Click);
			this.btnCopyToOrder.Click+=new EventHandler(btnCopyToOrder_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, EventArgs e)
		{
			ArrayList pp = Rowediting1.ProductRows();

			if (pp.Count > 0)
			{
				bool newQuote;

				long billId;
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("OWNERID",this.TextboxSearchOwnerID.Text);
					if(this.TextboxSearchManagerID.Text.Length>0)
						dg.Add("MANAGERID",this.TextboxSearchManagerID.Text);
					if(this.TextboxSearchSignalerID.Text.Length>0)
						dg.Add("SIGNALER",this.TextboxSearchSignalerID.Text);
					dg.Add("SUBJECT", this.QSubject.Text);
					dg.Add("DESCRIPTION", this.QuoteDescription.Text);
					dg.Add("CURRENCY", Rowediting1.RowEstCurrency);
					dg.Add("CHANGE", StaticFunctions.FixDecimal(Rowediting1.RowEstChange));
					DateTime ExpirationDate;
					try
					{
						ExpirationDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(QValidData.Text));
					}
					catch
					{
						ExpirationDate = DateTime.Now.AddDays(30);
					}
					dg.Add("EXPIRATIONDATE", ExpirationDate);

					DateTime QuoteDate;
					try
					{
						QuoteDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(QuoteData.Text));
					}
					catch
					{
						QuoteDate = DateTime.Now;
					}
					dg.Add("QUOTEDATE", QuoteDate);

                    if(ShipDescription.SelectedIndex>0)
                        dg.Add("SHIPID", ShipDescription.SelectedValue.Split('|')[0]);

					if(ShipData.Text.Length>0)
					{
						dg.Add("SHIPDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(ShipData.Text)));
					}

					dg.Add("LASTMODIFIEDDATE", DateTime.Now);
					dg.Add("LASTMODIFIEDBYID", UC.UserId);

					if(this.QuoteID==-1)
						dg.Add("CREATEDBYID", UC.UserId);

					dg.Add("ACTIVITYID", this.ActivityID);
					if(this.QStage.SelectedValue=="-1")
						dg.Add("STAGE", ((int) QuoteStage.Negotiation).ToString());
					else
						dg.Add("STAGE", this.QStage.SelectedValue);
					dg.Add("PAYMENTID", this.QPayment.SelectedValue);
                    Object progressive;
                    progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS WHERE TYPE=0");
                    if (progressive != null && !((bool)progressive))
                    {
                        DataRow drprog;

                        drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=0").Tables[0].Rows[0];
                        string newprog = string.Empty;

                        if(Convert.ToInt32(drprog["NPROGRESTART"])>0)
                            if (DateTime.Now >= new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) && new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) >= (DateTime)drprog["LASTRESET"])
                            {
                                DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROGSTART,LASTRESET=GETDATE() WHERE TYPE=0");
                                drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=0").Tables[0].Rows[0];
                            }
                        newprog += (((int)drprog["NPROG"]) + 1).ToString();
                        if((bool)drprog["CHECKDAY"])
                            newprog+="-"+DateTime.Now.Day.ToString();
                        if((bool)drprog["CHECKMONTH"])
                            newprog += "-" + DateTime.Now.Month.ToString();
                        if ((bool)drprog["CHECKYEAR"])
                        {
                            if ((bool)drprog["TWODIGITYEAR"])
                                newprog += "-" + DateTime.Now.ToString("yyyy");
                            else
                                newprog += "-" + DateTime.Now.ToString("yy");
                        }

                        if ((bool)drprog["CHECKCUSTOMERCODE"])
                        {
                            switch (CrossWith.SelectedIndex)
                            {
                                case 0:
                                    newprog += "-" + DatabaseConnection.SqlScalar("SELECT COMPANYCODE FROM BASE_COMPANIES WHERE ID=" + CrossWithID.Text);
                                    break;
                                case 1:

                                    break;
                                case 2:

                                    break;
                            }

                        }

                        this.Qnumber.Text = newprog;
                        DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROG+1 WHERE TYPE=0");
                    }

                    if (this.Qnumber.Text.Length > 0)
					    dg.Add("NUMBER", this.Qnumber.Text);

					dg.Add("REDUCTION", 0);

					dg.Add("CROSSTYPE",CrossWith.SelectedValue);
					dg.Add("CROSSID",CrossWithID.Text);
					dg.Add("ADDRESS",QAddress.Text);
					dg.Add("SHIPADDRESS",SAddress.Text);


					dg.Add("GRANDTOTAL",Convert.ToDecimal(Request["grandtotal"]));
					dg.Add("SUBTOTAL",Convert.ToDecimal(Request["subtotal"]));
					dg.Add("TAXTOTAL",Convert.ToDecimal(Request["taxtotal"]));
					dg.Add("SHIP",(Request["shiptotal"].Length>0)?Convert.ToDecimal(Request["shiptotal"]):0);
					dg.Add("SHIPVAT",(Request["shipVat"].Length>0)?Convert.ToDecimal(Request["shipVat"]):0);

					dg.Add("GROUPS","|" + UC.UserGroupId.ToString() + "|");

					dg.Add("INCLUDEPRODPDF",(chkIncludePDFDoc.Checked)?1:0);
                    dg.Add("LIST", currentPriceList.Text);

					billId = Convert.ToInt64(dg.Execute("QUOTES", "id=" + this.QuoteID, DigiDapter.Identities.Identity));
					if(this.QuoteID==-1)this.QuoteID=Convert.ToInt64(billId);
					newQuote = dg.RecordInserted;
				}
				if (!newQuote)
				{
					DatabaseConnection.DoCommand("DELETE FROM QUOTEROWS WHERE ESTIMATEID=" + this.QuoteID);
					DatabaseConnection.DoCommand("DELETE FROM QUOTEDOCUMENT WHERE QUOTEID=" + this.QuoteID);
				}
				foreach (PurchaseProduct Pprod in pp)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("ESTIMATEID", this.QuoteID);
						dg.Add("DESCRIPTION", Pprod.ShortDescription);
						dg.Add("DESCRIPTION2", Pprod.LongDescription);
						dg.Add("UPRICE", Pprod.UnitPrice);
						dg.Add("NEWUPRICE", Pprod.FinalPrice);
						dg.Add("LISTPRICE", Pprod.ListPrice);
						dg.Add("CATALOGID", Pprod.id);
						dg.Add("QTA", Pprod.Qta);
						dg.Add("REDUCTION", Pprod.Reduction);
						dg.Add("TAX",Pprod.Vat);
						dg.Add("COST",Pprod.Cost);
						dg.Add("PRODUCTCODE",Pprod.ProductCode);
                        dg.Add("REALLISTPRICE", Pprod.RealListPrice);
						dg.Execute("QUOTEROWS");
					}
				}
				if(Request["IDDocument"]!=null && Request["IDDocument"].ToString().Length>0)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("QUOTEID",this.QuoteID);
						dg.Add("DOCUMENTID",Convert.ToInt64(Request["IDDocument"]));
						dg.Execute("QUOTEDOCUMENT");
					}
				}

				int otherdocument=1;
				while(Request["IDDocument_"+otherdocument]!=null && Request["IDDocument_"+otherdocument].ToString().Length>0)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("QUOTEID",this.QuoteID);
						dg.Add("DOCUMENTID",Convert.ToInt64(Request["IDDocument_"+otherdocument]));
						dg.Execute("QUOTEDOCUMENT");
					}
					otherdocument++;
				}

				ClientScript.RegisterStartupScript(this.GetType(), "OK","<script>alert('"+string.Format(Root.rm.GetString("Esttxt19"),this.Qnumber.Text)+"');location.href='/erp/quotelist.aspx?m=67&dgb=1&si=69';</script>");
				FillQuote(this.QuoteID);

					if(CheckActivity.Checked && CrossWithID.Text.Length>0)
					{
						ActivityInsert ai = new ActivityInsert();
						string A="";
						string C="";
						string L="";

						switch(CrossWith.SelectedValue)
						{
							case "0":
								A=CrossWithID.Text;
								C="";
								L="";
								break;
							case "1":
								C=CrossWithID.Text;
								A="";
								L="";
								break;
							case "2":
								L=CrossWithID.Text;
								A="";
								C="";
								break;
						}
						if(A.Length>0 || C.Length>0 || L.Length>0)
							ai.InsertActivity("7", "", UC.UserId.ToString(), C, A, L,Root.rm.GetString("Esttxt2")+":"+this.QSubject.Text, this.QuoteDescription.Text, UC.LTZ.ToUniversalTime(DateTime.Now), UC,1);
					}

			}
			else
				ClientScript.RegisterStartupScript(this.GetType(), "OK","<script>alert('" + Root.rm.GetString("Quotxt29")+"');</script>");
		}

		private void FillQuote(long id)
		{
			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM QUOTES WHERE ID="+id);

			this.QuoteID=id;

			if(this.QuoteID!=-1)
			{
				HtmlTableRow actRow = (HtmlTableRow)Page.FindControl("actRow");
				actRow.Visible=false;
			}else
			{
				HtmlTableRow actRow = (HtmlTableRow)Page.FindControl("actRow");
				actRow.Visible=true;
			}

			QSubject.Text=ds.Tables[0].Rows[0]["Subject"].ToString();
			QuoteDescription.Text=ds.Tables[0].Rows[0]["Description"].ToString();
			Qnumber.Text=ds.Tables[0].Rows[0]["Number"].ToString();
			QPayment.SelectedIndex=-1;
			foreach(ListItem li in QPayment.Items)
			{
				if(ds.Tables[0].Rows[0]["PaymentID"].ToString()==li.Value)
				{
					li.Selected=true;
					break;
				}
			}

			TextboxSearchOwnerID.Text = ds.Tables[0].Rows[0]["OwnerID"].ToString();
			TextboxSearchOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+TextboxSearchOwnerID.Text) ;
			TextboxSearchManagerID.Text = ds.Tables[0].Rows[0]["ManagerID"].ToString();
			if(TextboxSearchManagerID.Text.Length>0)
				TextboxSearchManager.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+TextboxSearchManagerID.Text) ;
			TextboxSearchSignalerID.Text = ds.Tables[0].Rows[0]["Signaler"].ToString();
			if(TextboxSearchSignalerID.Text.Length>0)
				TextboxSearchSignaler.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID="+TextboxSearchSignalerID.Text) ;

			QStage.SelectedIndex=-1;
			foreach(ListItem li in QStage.Items)
			{
				if(ds.Tables[0].Rows[0]["Stage"].ToString()==li.Value)
				{
					li.Selected=true;
					break;
				}
			}

			QValidData.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"])).ToShortDateString();
			QuoteData.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(ds.Tables[0].Rows[0]["QuoteDate"])).ToShortDateString();
			if(ds.Tables[0].Rows[0]["ShipDate"]!=DBNull.Value)
				ShipData.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(ds.Tables[0].Rows[0]["ShipDate"])).ToShortDateString();
            if (ds.Tables[0].Rows[0]["ShipId"] != DBNull.Value)
            {
                ShipDescription.SelectedIndex = -1;
                foreach (ListItem li in ShipDescription.Items)
                {
                    if (li.Value.Split('|')[0] == ds.Tables[0].Rows[0]["ShipId"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
            }


			CrossWith.SelectedIndex=Convert.ToInt32(ds.Tables[0].Rows[0]["CrossType"]);
			CrossWithID.Text=ds.Tables[0].Rows[0]["CrossID"].ToString();
			switch(CrossWith.SelectedIndex)
			{
				case 0:
					CrossWithText.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID="+CrossWithID.Text);
					break;
				case 1:
					CrossWithText.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID="+CrossWithID.Text);
					break;
				case 2:
					CrossWithText.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID="+CrossWithID.Text);
					break;
			}
			QAddress.Text=ds.Tables[0].Rows[0]["Address"].ToString();
			SAddress.Text=ds.Tables[0].Rows[0]["ShipAddress"].ToString();
			this.chkIncludePDFDoc.Checked=(bool)ds.Tables[0].Rows[0]["IncludeProdPdf"];


			string createdBy = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+ds.Tables[0].Rows[0]["CreatedById"].ToString());
			string modifiedBy = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+ds.Tables[0].Rows[0]["LastModifiedByID"].ToString());

			QuoteInfo.Text="<table width=\"100%\" style=\"color:grey\"><tr><td align=right>" + Root.rm.GetString("CreBy")+":</td><td>"+createdBy+"</td><td align=right>";
			QuoteInfo.Text+=Root.rm.GetString("InsDate")+":</td><td>"+((DateTime)ds.Tables[0].Rows[0]["CreatedDate"]).ToShortDateString()+"</td></tr><tr><td align=right>";
			QuoteInfo.Text+=Root.rm.GetString("ModBy")+":</td><td>"+modifiedBy+"</td><td align=right>";
			QuoteInfo.Text+=Root.rm.GetString("ModDate")+":</td><td>"+((DateTime)ds.Tables[0].Rows[0]["LastModifiedDate"]).ToShortDateString()+"</td></tr></table>";


			StringBuilder sbship = new StringBuilder();
			if(ds.Tables[0].Rows[0]["ShipVat"].ToString().Length>0)
			{
				sbship.AppendFormat("for(var i=0;i<document.getElementById(\"shipVat\").length;i++){0}{{",Environment.NewLine);
				sbship.AppendFormat("if(document.getElementById(\"shipVat\").options[i].value=='{0}'){1}{{document.getElementById(\"shipVat\").options[i].selected=true;{1}break;}}}};{1}",((ds.Tables[0].Rows[0]["ShipVat"].ToString().Length>0)?ds.Tables[0].Rows[0]["ShipVat"].ToString():"0"),Environment.NewLine);
			}
			sbship.AppendFormat("var tempship={0};{1}",ds.Tables[0].Rows[0]["Ship"].ToString().Replace(',','.'),Environment.NewLine);
			sbship.AppendFormat("document.getElementById(\"shiptotal\").value=FixCurrency(tempship,2,true,true);{0}",Environment.NewLine);
			ClientScript.RegisterStartupScript(this.GetType(), "fillship","<script>"+sbship.ToString()+"</script>");

				DataTable dtdoc = DatabaseConnection.CreateDataset("SELECT * FROM QUOTEDOCUMENT WHERE QUOTEID="+this.QuoteID).Tables[0];
				if(dtdoc.Rows.Count>0)
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendFormat("<script>{0}",Environment.NewLine);

					if(dtdoc.Rows.Count>1)
					{
						for(int i=1;i<dtdoc.Rows.Count;i++)
						{
							sb.AppendFormat("cloneObj('TableDocument',{0},'tblDocument');cleardoc('_{0}');{1}",i,Environment.NewLine);
						}
					}
					JavascriptFillDocument(ref sb,string.Empty,dtdoc.Rows[0]);
					for(int i=1;i<dtdoc.Rows.Count;i++)
					{
						JavascriptFillDocument(ref sb,"_"+i.ToString(),dtdoc.Rows[i]);
					}
					sb.AppendFormat("idcdoc={0};{1}</script>{1}",dtdoc.Rows.Count,Environment.NewLine);
					ClientScript.RegisterStartupScript(this.GetType(), "filldoc",sb.ToString());
				}

			DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM QUOTEROWS WHERE ESTIMATEID="+this.QuoteID).Tables[0];
			if(dt.Rows.Count>0)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("<script>{0}",Environment.NewLine);

				if(dt.Rows.Count>1)
				{
					for(int i=1;i<dt.Rows.Count;i++)
					{
						sb.AppendFormat("cloneObj('TableRows',{0},'Rowediting1_PanelRows');clearrow('_{0}');{1}",i,Environment.NewLine);
					}
				}

				JavascriptFillRows(ref sb,string.Empty,dt.Rows[0]);
				for(int i=1;i<dt.Rows.Count;i++)
				{
					JavascriptFillRows(ref sb,"_"+i.ToString(),dt.Rows[i]);
				}

				sb.AppendFormat("idc={0};{1}calctotal();{1}</script>{1}",dt.Rows.Count,Environment.NewLine);
				ClientScript.RegisterStartupScript(this.GetType(), "fillrows",sb.ToString());
			}
			lblPrint.Text="<img src=/i/printer.gif border=0 style=\"cursor:pointer\" onclick=\"CreateBox('/erp/printquote.aspx?render=no&qid="+this.QuoteID+"',event,600,300);\">";
			this.btnClone.Visible=true;
			this.btnCopyToOrder.Visible=true;
            ClientScript.RegisterStartupScript(this.GetType(), "onload", "<script>CheckShipConfiguration();</script>");

            FillPriceList((long)ds.Tables[0].Rows[0]["List"]);
            isCommercial();
		}

		private void JavascriptFillDocument(ref StringBuilder sb,string suffix,DataRow dr)
		{
			string docname = DatabaseConnection.SqlScalar("SELECT FILENAME FROM FILEMANAGER WHERE ID="+dr["documentid"].ToString());
			sb.AppendFormat("document.getElementById(\"DocumentDescription{0}\").value='{1}';{2}",suffix,docname,Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"IDDocument{0}\").value='{1}';{2}",suffix,dr["documentid"].ToString(),Environment.NewLine);
		}

		private void JavascriptFillRows(ref StringBuilder sb,string suffix,DataRow dr)
		{
			sb.AppendFormat("document.getElementById(\"estProduct{0}\").value='{1}';{2}",suffix,G.ParseJSString(dr["Description"].ToString()),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estProductID{0}\").value='{1}';{2}",suffix,dr["catalogid"].ToString(),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estUm{0}\").value='{1}';{2}",suffix,dr["UnitMeasure"].ToString(),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estQta{0}\").value='{1}';{2}",suffix,dr["qta"].ToString(),Environment.NewLine);

			sb.AppendFormat("for(var i=0;i<document.getElementById(\"estVat{0}\").length;i++){1}{{",suffix,Environment.NewLine);

			sb.AppendFormat("if(document.getElementById(\"estVat{0}\").options[i].value=='{1}'){2}{{document.getElementById(\"estVat{0}\").options[i].selected=true;{2}break;}}}};{2}",suffix,((dr["tax"].ToString().Length>0)?dr["Tax"].ToString():"0"),Environment.NewLine);


			sb.AppendFormat("document.getElementById(\"estCost{0}\").value=FixCurrency('{1}',2,true,true);{2}",suffix,StaticFunctions.FixDecimalJS(dr["cost"].ToString()),Environment.NewLine);
            if (((long)dr["catalogid"]) > 0)
            {
                sb.AppendFormat("document.getElementById(\"estCost{0}\").readOnly=true;{1}",suffix,Environment.NewLine);
                sb.AppendFormat("document.getElementById(\"estCost{0}\").style.backgroundColor='#eeeeee';{1}", suffix, Environment.NewLine);
            }

			sb.AppendFormat("document.getElementById(\"estDiscount{0}\").value='{1}';{2}",suffix,dr["reduction"].ToString(),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estPl{0}\").value=FixCurrency('{1}',2,true,true);{2}",suffix,StaticFunctions.FixDecimalJS(dr["ListPrice"].ToString()),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estUp{0}\").value=FixCurrency('{1}',2,true,true);{2}",suffix,StaticFunctions.FixDecimalJS(dr["Uprice"].ToString()),Environment.NewLine);
			sb.AppendFormat("document.getElementById(\"estPf{0}\").value=FixCurrency('{1}',2,true,true);{2}",suffix,StaticFunctions.FixDecimalJS(dr["newuprice"].ToString()),Environment.NewLine);
		}

		private void InitAjax()
		{
			Manager.Register(this, "Ajax.Quote", Debug.None);
		}

		[Method]
		public string CheckAddress(string id,string type)
		{
			DataSet ds = new DataSet();
			switch(type)
			{
				case "0":
					ds=DatabaseConnection.CreateDataset("SELECT INVOICINGADDRESS AS ADDRESS,INVOICINGCITY AS CITY,INVOICINGSTATEPROVINCE AS PROVINCE,INVOICINGSTATE AS NATION,INVOICINGZIPCODE AS ZIP FROM BASE_COMPANIES WHERE ID="+int.Parse(id));
					break;
				case "1":
					ds=DatabaseConnection.CreateDataset("SELECT ADDRESS_1 AS ADDRESS,CITY_1 AS CITY,PROVINCE_1 AS PROVINCE,STATE_1 AS NATION,ZIPCODE_1 AS ZIP FROM BASE_CONTACTS WHERE ID="+int.Parse(id));
					break;
				case "2":
					ds=DatabaseConnection.CreateDataset("SELECT ADDRESS, CITY, PROVINCE,STATE AS NATION,ZIPCODE AS ZIP FROM CRM_LEADS WHERE ID="+int.Parse(id));
					break;
			}

			if(ds.Tables[0].Rows.Count>0)
			{
				DataRow d = ds.Tables[0].Rows[0];

				string newaddr = normalizeAddress(d["address"].ToString(),d["city"].ToString(),d["province"].ToString(),d["zip"].ToString(),d["nation"].ToString());
				return newaddr;
			}else
				return string.Empty;
		}

		private string normalizeAddress(string address,string city,string province,string zip,string nation)
		{
			string normal = string.Empty;
			normal = address+"\n"+city;
			if(province.Length==2)
				normal+=" ("+province+")\n";
			else
				normal+="\n"+province+"\n";
			normal+=zip;
			if(nation.Length>0)
				normal+=" - "+nation;

			return normal;
		}

        private void newQuote()
        {
            TextboxSearchOwnerID.Text = UC.UserId.ToString();
            TextboxSearchOwner.Text = UC.UserRealName;
            DataSet ds = DatabaseConnection.CreateDataset("SELECT UID AS ID,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS DESCRIPTION FROM ACCOUNT WHERE UID=(SELECT MANAGERID FROM ACCOUNT WHERE UID=" + UC.UserId + ")");
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextboxSearchManagerID.Text = ds.Tables[0].Rows[0][0].ToString();
                TextboxSearchManager.Text = ds.Tables[0].Rows[0][1].ToString();
            }
            Object progressive;
            progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS");
            if(progressive!=null)
                if (!((bool)progressive))
                {
                    Qnumber.ReadOnly = true;
                    Qnumber.Text = Root.rm.GetString("Quotxt43");
                }

            CheckActivity.Checked = true;

            FillPriceList();
            isCommercial();
        }

        public void isCommercial()
        {
            if (DatabaseConnection.SqlScalar("SELECT ACCESSLEVEL FROM ACCOUNT WHERE UID="+UC.UserId) == "2")
            {
                SelectOwner.Visible = false;
                SelectManager.Visible = false;
            }
        }

		[Method]
		public DataSet CheckManager(string id)
		{
			DataSet ds = new DataSet();
            ds = DatabaseConnection.CreateDataset(string.Format(@"select account.managerid as id,isnull(account2.name,'')+' '+isnull(account2.surname,'') as description, account.listprice from account
inner join account as account2 on account.managerid=account2.uid
where account.uid={0}",id));
			if(ds.Tables[0].Rows.Count<=0)ds=new DataSet();
			return ds;
		}

		private void btnClone_Click(object sender, EventArgs e)
		{
			DbSqlParameterCollection Msc = new DbSqlParameterCollection();

			DbSqlParameter pID = new DbSqlParameter("@id", SqlDbType.BigInt, 8);
			pID.Value = this.QuoteID;
			Msc.Add(pID);
			DbSqlParameter userID = new DbSqlParameter("@userid", SqlDbType.BigInt, 8);
			userID.Value = UC.UserId;
			Msc.Add(userID);
			object newquote = DatabaseConnection.DoStoredScalar("CloneQuote",Msc,true);
			this.FillQuote((long)newquote);

		}

		private void btnCopyToOrder_Click(object sender, EventArgs e)
		{
			DbSqlParameterCollection Msc = new DbSqlParameterCollection();
			DbSqlParameter pID = new DbSqlParameter("@id", SqlDbType.BigInt, 8);
			pID.Value = this.QuoteID;
			Msc.Add(pID);
			DbSqlParameter userID = new DbSqlParameter("@userid", SqlDbType.BigInt, 8);
			userID.Value = UC.UserId;
			Msc.Add(userID);
			object newquote = DatabaseConnection.DoStoredScalar("QuoteToOrder",Msc,true);
			Session["ViewOrder"]=newquote.ToString();
			DatabaseConnection.DoCommand("UPDATE QUOTES SET STAGE=6 WHERE ID="+this.QuoteID);
			Response.Redirect("/erp/orderediting.aspx?m=67&dgb=1&si=72");
		}
	}
}
