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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.CRM;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public partial class CRMOpportunity : G
	{


		protected RepeaterColumnHeader Repeatercolumnheader1;
		protected RepeaterColumnHeader Repeatercolumnheader2;
		protected HtmlTableCell tabControlTab1;
		protected HtmlTableCell tabControlTab2;
		protected HtmlTableCell tabControlTab3;
		protected HtmlTableCell tabControlTab4;



		protected HtmlTableCell tabControlTab6;
		protected HtmlTableCell tabControlTab5;
		protected HtmlTableCell tabControlTab9;

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (visDocuments.Visible == true && !M.IsModule(ActiveModules.Storage))
                Tabber.HideTabs += visDocuments.ID;

            base.OnPreRenderComplete(e);
        }

		public void Page_Load(object sender, EventArgs e)
		{
			Trace.Warn("PageLoad");
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				initNewRepeater();

				Back.Click += new EventHandler(BtnBack_Click);
				if (!isGoBack)
					Back.Visible = false;

				BtnFind.Text =Root.rm.GetString("CRMopptxt6");
				BtnNew.Text =Root.rm.GetString("CRMopptxt8");
				Opportunity_Submit.Text =Root.rm.GetString("CRMopptxt18");
				Opportunity_SubmitUP.Text =Root.rm.GetString("CRMopptxt18");
				AddNewPartner.Text =Root.rm.GetString("CRMopptxt69");
				Opportunity_Modify.Text =Root.rm.GetString("CRMopptxt19");
				Opportunity_NewCo.Text =Root.rm.GetString("CRMopptxt30");
				Opportunity_NewLead.Text =Root.rm.GetString("CRMopptxt73");
				Opportunity_CompanyList.Text =Root.rm.GetString("CRMopptxt39");
				FindCompanyButton.Text =Root.rm.GetString("Find");
				FindLeadButton.Text =Root.rm.GetString("Find");
				SrcLeadBtn.Text =Root.rm.GetString("Find");
				SearchCompanyBtn.Text =Root.rm.GetString("Find");
				Opportunity_SearchCompany.Text =Root.rm.GetString("CRMcontxt52");
				Opportunity_SrcLead.Text =Root.rm.GetString("CRMcontxt52");
				Opportunity_LoadLead.Text =Root.rm.GetString("CRMopptxt84");
				Opportunity_LoadCompany.Text =Root.rm.GetString("CRMopptxt85");


				AddNewCompetitor.Text =Root.rm.GetString("CRMopptxt49");
				SaveCompetitor.Text =Root.rm.GetString("CRMopptxt18");
				CloseCompetitor.Text =Root.rm.GetString("CRMopptxt39");




				if (!Page.IsPostBack)
				{
					TblSrcLead.Visible = false;
					TblSearchCompany.Visible = false;

					AcCrono.ParentID = 0;
					AcCrono.OpportunityID = 0;
					AcCrono.AcType = 0;
					OppCompany.Visible=false;

					NewActivityPhone.Text =Root.rm.GetString("Wortxt6");
					NewActivityLetter.Text =Root.rm.GetString("Wortxt7");
					NewActivityFax.Text =Root.rm.GetString("Wortxt8");
					NewActivityMemo.Text =Root.rm.GetString("Wortxt9");
					NewActivityEmail.Text =Root.rm.GetString("Wortxt10");
					NewActivityVisit.Text =Root.rm.GetString("Wortxt11");
					NewActivityGeneric.Text =Root.rm.GetString("Wortxt12");
					NewActivitySolution.Text =Root.rm.GetString("Wortxt13");

					NewDoc.Text =Root.rm.GetString("CRMrubtxt4");

					OpportunityCard.Visible = false;
					CompetitorCard.Visible = false;
					Opportunity_Access.Visible = false;
					Opportunity_SubmitUP.Visible = false;
					Tabber.Visible = false;

					Opportunity_Currency.DataTextField = "Currency";
					Opportunity_Currency.DataValueField = "idcur";
					Opportunity_Currency.DataSource = DatabaseConnection.CreateDataset("SELECT TOP 1 CAST(ID AS VARCHAR(10))+'|'+CAST(CHANGETOEURO AS VARCHAR(10))+'|'+CURRENCYSYMBOL AS IDCUR,CURRENCY FROM CURRENCYTABLE").Tables[0];
					Opportunity_Currency.DataBind();

					PartnerCard.Visible = false;
					PartnerSubmit.Text =Root.rm.GetString("CRMopptxt70");

					if (Request.Params["o"] != null)
					{
						ViewOpportunity(int.Parse(Request.Params["o"]));
						if (Request.Params["tab"] != null)
						{
							switch (Request.Params["tab"])
							{
								case "0":
									Tabber.Selected = visCompany.ID;
									break;
								case "1":
									Tabber.Selected = visLead.ID;
									break;
							}
						}
					}
					else
						FillRepeaterOpportunity();


					if (Request.Params["gb"] != null && isGoBack)
					{
						Stack ba = new Stack();
						ba = (Stack) Session["goback1"];
						GoBack gb = new GoBack();
						gb = (GoBack) ba.Pop(); //[ba.Count - 1];
						string[] par = gb.parameter.Split('|');

						if (par[1].Length > 0)
						{
							if (par[1] != "nn")
								ViewOpportunity(int.Parse(par[1]));
						}
						if (par[2].Length > 0)
						{
							switch (par[2])
							{
								case "a":
									Tabber.Selected = visActivity.ID;
									break;
								case "d":
									Tabber.Selected = visDocuments.ID;
									break;
								case "fa":
									Tabber.Selected = visActivity.ID;
									break;
								case "al":
									Tabber.Selected = this.visLead.ID;
									OppLead.OpID = par[1];
									OppLead.CoID = par[4];
									OppLead.CoCrossID = par[3];
									OppLead.Visible=true;
									tabControl.Visible = false;
									OppLead.BindData();
									break;
								case "ac":
									Tabber.Selected = this.visCompany.ID;
									OppCompany.OpID = par[1];
									OppCompany.CoID = par[4];
									OppCompany.CoCrossID = par[3];
									OppCompany.Visible=true;
									tabControl.Visible = false;
									OppCompany.BindData();
									break;
							}
						}
						Session["goback1"] = ba;

					}
				}
			}
		}


		private void initNewRepeater()
		{
			this.NewRepeaterOpportunity.UsePagedDataSource=true;
			this.NewRepeaterOpportunity.PageSize=UC.PagingSize;

			this.RepeaterLead.UsePagedDataSource=true;
			this.RepeaterLead.PageSize=UC.PagingSize;

			this.RepeaterCompany.UsePagedDataSource=true;
			this.RepeaterCompany.PageSize=UC.PagingSize;

			this.NewRepeaterOpportunity.UsePagedDataSource=true;
			this.NewRepeaterOpportunity.PageSize=UC.PagingSize;
		}

		public void NewActivity_Click(Object sender, EventArgs e)
		{
			SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=37&gb=1", new string[2] {Opportunity_ID.Text, "a"});
			Session["AcOpportunityID"] = Opportunity_ID.Text;
			switch (((LinkButton) sender).ID)
			{
				case "NewActivityPhone":
					Session["AType"] = 1;
					break;
				case "NewActivityLetter":
					Session["AType"] = 2;
					break;
				case "NewActivityFax":
					Session["AType"] = 3;
					break;
				case "NewActivityMemo":
					Session["AType"] = 4;
					break;
				case "NewActivityEmail":
					Session["AType"] = 5;
					break;
				case "NewActivityVisit":
					Session["AType"] = 6;
					break;
				case "NewActivityGeneric":
					Session["AType"] = 7;
					break;
				case "NewActivitySolution":
					Session["AType"] = 8;
					break;
			}
			Response.Redirect("/WorkingCRM/AllActivity.aspx?m=25&si=38");
		}

		public void Btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "BtnFind":
					FillRepeaterOpportunity();
					CompetitorCard.Visible = false;
					break;
				case "BtnNew":
					Opportunity_ID.Text = "-1";
					Session.Remove("newprod");
					EnableModify(true);
					OpportunityCard.Visible = true;
					NewRepeaterOpportunity.Visible = false;
					Tabber.Visible = true;
					visOpportunity.Header = Root.rm.GetString("CRMopptxt1");
					Tabber.Selected = visOpportunity.ID;
                    Tabber.EditTab = visOpportunity.ID;
					break;
				case "Opportunity_Submit":
				case "Opportunity_SubmitUP":
					ModifyOpportunity(int.Parse(Opportunity_ID.Text));
					break;
				case "Opportunity_Modify":
					EnableModify(false);
                    Tabber.EditTab = visOpportunity.ID;
					break;
				case "Opportunity_NewCo":
					string opId = Opportunity_ID.Text;
					string coId = "-1";
					string coCrossId = String.Empty;
					string coName = String.Empty;
					OppCompany.OpID = opId;
					OppCompany.CoID = coId;
					OppCompany.CoCrossID = coCrossId;
					OppCompany.Visible=true;
                    OppCompany.BindData();
					tabControl.Visible = false;
					break;
				case "Opportunity_NewLead":
					string LOpID = Opportunity_ID.Text;
					string LCoID = "-1";
					string LCoCrossID = String.Empty;
                    OppLead.OpID = LOpID;
                    OppLead.CoID = LCoID;
                    OppLead.CoCrossID = LCoCrossID;
                    OppLead.Visible = true;
                    OppLead.BindData();

					tabControl.Visible = false;
					break;
				case "CompanyNewSubmit":
					break;
				case "HideNewCompany":
					break;
				case "SaveNewReferrer":
					break;
				case "AddNewCompetitor":
					EnableModifyCompetitor(true, true);
					CompetitorCard.Visible = true;
					break;
				case "SaveCompetitor":
					SaveCompetitorFunction();
					CompetitorCard.Visible = false;
					break;
				case "CloseCompetitor":
					RepeaterCompetitor.Visible = true;
					CompetitorCard.Visible = false;
					break;
				case "SubmitCompetitorCrossContact":
					break;
				case "Opportunity_CompanyList":
					Opportunity_NewCo.Visible = true;
					Opportunity_CompanyList.Visible = false;
					RepeaterCompany.Visible = true;
					break;
				case "NewActivityOp":
					Response.Redirect("CRM_Activity.aspx?m=25&si=38&action=new&o=" + Opportunity_ID.Text);
					break;
				case "AddNewPartner":
					TextboxPartnerOppID.Text = "-1";
					TextboxPartnerID.Text = String.Empty;
					TextboxPartner.Text = String.Empty;
					Opportunity_NotePartner.Text = String.Empty;
					PartnerCard.Visible = true;
					break;
				case "PartnerSubmit":
					InsertPartner();
					FillRepeaterPartner();
					PartnerCard.Visible = false;
					break;
				case "FindCompanyButton":

					string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 0, ((TextBox) Page.FindControl("FindCompany")).Text);
					InitSheetPaging(raq, true);
					this.RepeaterCompany.sqlDataSource=raq;
					this.RepeaterCompany.DataBind();

					OppCompany.Visible=false;
					break;
				case "FindLeadButton":
					raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1, ((TextBox) Page.FindControl("FindLead")).Text);
					InitSheetPaging(raq, false);
					this.RepeaterLead.sqlDataSource=raq;
					this.RepeaterLead.DataBind();

					OppCompany.Visible=false;
					break;
				case "NewDoc":
					SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=37&gb=1", new string[] {Opportunity_ID.Text, "d"});
					Session["OpportunityID"] = Opportunity_ID.Text;
					Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=o");
					break;
				case "Opportunity_SrcLead":
					TblSrcLead.Visible = true;
					RepeaterLead.Visible = false;
					break;
				case "Opportunity_SearchCompany":
					TblSearchCompany.Visible = true;
					RepeaterCompany.Visible = false;
					break;
			}
		}

		public void InsertPartner()
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("COMPANYID", TextboxPartnerID.Text);
				dg.Add("OPPORTUNITYID", Opportunity_ID.Text);
				dg.Add("NOTE", Opportunity_NotePartner.Text);
				dg.Execute("CRM_OPPORTUNITYPARTNERS", "ID =" + int.Parse(TextboxPartnerOppID.Text));
			}
		}



		private void FillRepeaterOpportunity()
		{
			StringBuilder sqlString = new StringBuilder();
			StringBuilder sqlText = new StringBuilder();
			char[] BuildQuery = new char[4] {'0', '0', '0', '0'};
			sqlString.Append("SELECT ID, TITLE, OWNER ");

			sqlString.AppendFormat("FROM CRM_OPPORTUNITY_VIEW WHERE ((ADMINACCOUNT LIKE '%|{0}|%' OR BASICACCOUNT LIKE '%|{0}|%' OR OWNERID={0} OR GROUPS LIKE '%|{1}|%')) ", UC.UserId.ToString(), UC.UserGroupId.ToString());

			if (FindTxt.Text.Length > 0)
			{
				sqlText.AppendFormat("(TITLE LIKE '%{0}%' OR DESCRIPTION LIKE '%{0}%')", FindTxt.Text);
				BuildQuery[0] = '1';
			}

			Trace.Warn("BuildQuery", String.Concat(BuildQuery.GetValue(0), BuildQuery.GetValue(1), BuildQuery.GetValue(2), BuildQuery.GetValue(3)));

			switch (String.Concat(BuildQuery.GetValue(0), BuildQuery.GetValue(1), BuildQuery.GetValue(2), BuildQuery.GetValue(3)))
			{
				case "1000":
					sqlString.AppendFormat(" AND {0}", sqlText.ToString());
					break;

			}

			if (!Page.IsPostBack)
				sqlString.Append(" ORDER BY LASTMODIFIEDDATE DESC");

			Trace.Warn("OppQuery>>>", sqlString.ToString());

			Trace.Warn("query opp", sqlString.ToString());

			this.NewRepeaterOpportunity.PageSize=UC.PagingSize;
			this.NewRepeaterOpportunity.sqlDataSource=sqlString.ToString();
			this.NewRepeaterOpportunity.DataBind();



			OpportunityCard.Visible = false;
			Tabber.Visible = false;

		}

		private void ModifyOpportunity(int id)
		{
			TextBox txtfield;
			using (DigiDapter dg = new DigiDapter("SELECT * FROM CRM_OPPORTUNITY WHERE ID=" + id))
			{
				if (!dg.HasRows)
				{
				}
				for (int i = 0; i < dg.ExternalReader.ItemArray.Length; i++)
				{
					string columnName = dg.ExternalReader.Table.Columns[i].ColumnName.ToString();
					Trace.Warn("before", "Opportunity_" + columnName.ToUpper());

					Debug.Write("dg.Add(\""+columnName+"\",CRM_Leads_" + columnName + ".Text);\r\n");

					switch (columnName.ToUpper())
					{
						case "ID":
							break;
						case "OWNERID":
							txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_" + columnName);
							if (txtfield.Text.Length > 0)
							{
								dg.Add(columnName, txtfield.Text);
							}
							else
							{
								dg.Add(columnName, UC.UserId);
							}
							break;
						case "CREATEDBYID":
							if (id == -1) dg.Add(columnName, UC.UserId);
							break;
						case "LASTMODIFIEDBYID":
							dg.Add(columnName, UC.UserId);
							break;
						case "LASTMODIFIEDDATE":
							dg.Add(columnName, UC.LTZ.ToUniversalTime(DateTime.Now));
							break;
						case "CREATEDDATE":
							if (id == -1) dg.Add(columnName, UC.LTZ.ToUniversalTime(DateTime.Now));
							break;
						case "ADMINACCOUNT":
							if (Office.GetValue.Length > 0)
							{
								dg.Add(columnName, Office.GetValue);
							}
							else
							{
								dg.Add(columnName, "|" + UC.UserId.ToString() + "|");
							}
							break;
						case "BASICACCOUNT":
							if (OfficeBasic.GetValue.Length > 0)
							{
								dg.Add(columnName, OfficeBasic.GetValue);
							}
							else
							{
								dg.Add(columnName, "|" + UC.UserId.ToString() + "|");
							}
							break;
						case "GROUPS":
							string groupQuery = String.Empty;
							string[] GroupID = Office.GetValue.Split('|');
							foreach (string gr in GroupID)
							{
								if (gr.Length > 0) groupQuery += "UID=" + gr + " OR ";
							}
							foreach (string grb in GroupID)
							{
								if (grb.Length > 0) groupQuery += "uid=" + grb + " OR ";
							}

							txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_OwnerID");
							if (txtfield.Text.Length > 0)
							{
								groupQuery += "UID=" + int.Parse(txtfield.Text);
							}
							else
							{
								groupQuery += "UID=" + UC.UserId;
							}

							DataSet Group1 = DatabaseConnection.CreateDataset("SELECT DISTINCT GROUPID FROM ACCOUNT WHERE " + groupQuery + " GROUP BY GROUPID");
							string arrList = String.Empty;
							foreach (DataRow dr in Group1.Tables[0].Rows)
							{
								DataSet Group2 = DatabaseConnection.CreateDataset("SELECT DEPENDENCY FROM GROUPS WHERE ID=" + dr[0]);
								if (Group2.Tables[0].Rows.Count > 0)
								{
									arrList += (Convert.ToInt64(dr[0]) == UC.AdminGroupId) ? "|" + UC.AdminGroupId.ToString() + "|" : Group2.Tables[0].Rows[0][0];
								}
							}

							if (arrList.Length > 0)
								dg.Add(columnName, CleanGroups(arrList));
							else
								dg.Add(columnName, CleanGroups("|" + UC.AdminGroupId.ToString() + "|" + UC.UserGroupId.ToString() + "|"));
							break;
						case "CURRENCY":
							dg.Add(columnName, Opportunity_Currency.SelectedValue.Split('|')[0]);
							break;
						case "CURRENCYCHANGE":
							dg.Add(columnName, Opportunity_Currency.SelectedValue.Split('|')[1]);
							break;
						case "EXPECTEDREVENUE":
						case "INCOMEPROBABILITY":
						case "AMOUNTCLOSED":
							break;
						default:
							if (OpportunityCard.FindControl("Opportunity_" + columnName) != null)
							{
								txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_" + columnName);
								if (txtfield.Text.Length > 0) dg.Add(columnName, txtfield.Text);
							}
							break;
					}
				}

				object newId = dg.Execute("CRM_OPPORTUNITY", "ID=" + id, DigiDapter.Identities.Identity);


				if (id == -1)
				{
					ViewOpportunity(Convert.ToInt32(newId));
				}
				else
				{
					ViewOpportunity(id);
				}
			}
		}

		private void ViewOpportunity(int id)
		{
			RefreshOpportunityAmount(id);

			string sqlString = "SELECT * FROM CRM_OPPORTUNITY_VIEW WHERE ID=" + id;
			TextBox txtfield = new TextBox();
			DataSet myDataSet = DatabaseConnection.CreateDataset(sqlString);
			foreach (DataColumn cc in myDataSet.Tables[0].Columns)
			{
				if (OpportunityCard.FindControl("Opportunity_" + cc.ColumnName) != null)
				{
					try
					{
						txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_" + cc.ColumnName);
					}
					catch
					{
					}
					switch (cc.ColumnName)
					{
						case "CreatedBy":
							txtfield.Text = myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString() + " - " +
								UC.LTZ.ToLocalTime(Convert.ToDateTime(myDataSet.Tables[0].Rows[0]["CreatedDate"])).ToString();
							break;
						case "LastModifiedBy":
							txtfield.Text = myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString() + " - " +
								UC.LTZ.ToLocalTime(Convert.ToDateTime(myDataSet.Tables[0].Rows[0]["LastModifiedDate"])).ToString();
							break;

						case "AdminAccount":
							Trace.Warn(cc.ColumnName, myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							if (myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString().Length > 1)
							{
								Office.SetAccount(myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							}
							break;
						case "ExpectedRevenue":
						case "IncomeProbability":
						case "AmountClosed":
							Label symbol = (Label) OpportunityCard.FindControl("Opportunity_" + cc.ColumnName + "Symbol");
							symbol.Text = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE ID=" + myDataSet.Tables[0].Rows[0]["currency"].ToString());
							decimal change = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT CHANGETOEURO FROM CURRENCYTABLE WHERE ID=" + myDataSet.Tables[0].Rows[0]["currency"].ToString()));
							txtfield.Text = (Math.Round(Convert.ToDecimal(myDataSet.Tables[0].Rows[0][cc.ColumnName])*change, 2)).ToString();
							break;
						case "Currency":
							Opportunity_Currency.SelectedIndex = -1;
							foreach (ListItem li in Opportunity_Currency.Items)
							{
								if (myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString() == li.Value.Split('|')[0])
								{
									li.Selected = true;
									LabelCurrency.Text = li.Text;
									break;
								}
							}
							if (LabelCurrency.Text.Length <= 0) LabelCurrency.Text = Opportunity_Currency.Items[0].Text;
							Opportunity_Currency.Visible = false;
							LabelCurrency.Visible = true;
							break;
						case "CurrencyChange":
							break;
						default:
							txtfield.Text = myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString();
							break;
					}
					txtfield.ReadOnly = true;
					txtfield.CssClass = "OpportunityView";
				}
				else
				{
					switch (cc.ColumnName)
					{
						case "AdminAccount":
							Trace.Warn(cc.ColumnName, myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							if (myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString().Length > 1)
							{
								Office.SetAccount(myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							}
							break;
						case "BasicAccount":
							Trace.Warn(cc.ColumnName, myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							if (myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString().Length > 1)
							{
								OfficeBasic.SetAccount(myDataSet.Tables[0].Rows[0][cc.ColumnName].ToString());
							}
							break;
					}
				}
			}

			OpportunityCard.Visible = true;
			Tabber.Visible = true;
			Tabber.Selected=this.visOpportunity.ID;

			NewRepeaterOpportunity.Visible = false;
			Opportunity_Submit.Visible = false;
			Opportunity_SubmitUP.Visible = false;
			PopAccount.Visible = false;
			Opportunity_Access.Visible = false;

			if (myDataSet.Tables[0].Rows[0]["AdminAccount"].ToString().IndexOf("|" + UC.UserId.ToString() + "|") >= 0
				|| myDataSet.Tables[0].Rows[0]["OwnerID"].ToString() == UC.UserId.ToString()
				|| myDataSet.Tables[0].Rows[0]["Groups"].ToString().IndexOf("|" + UC.UserGroupId.ToString() + "|") >= 0)
			{
				Opportunity_Modify.Visible = true;
			}
			else
			{
				Opportunity_Modify.Visible = false;
			}


			string raq = RepeaterCompanyQuery(id, 0);
			InitSheetPaging(raq, true);
			this.RepeaterCompany.sqlDataSource=raq;
			this.RepeaterCompany.DataBind();


			raq = RepeaterCompanyQuery(id, 1);
			InitSheetPaging(raq, false);
			this.RepeaterLead.sqlDataSource=raq;
			this.RepeaterLead.DataBind();

			AcCrono.OpportunityID = id;

			AcCrono.FromSheet = "o";
			AcCrono.ViewCompany = true;
			AcCrono.Refresh();

			if (AcCrono.ItemCount == 0)
			{
				AcCrono.Visible = false;
				RepeaterActivityInfo.Visible = true;
				RepeaterActivityInfo.Text =Root.rm.GetString("CRMopptxt59");
			}
			else
			{
				AcCrono.Visible = true;
				RepeaterActivityInfo.Visible = false;
			}

			FillRepeaterCompetitor();
			FillRepeaterPartner();

			Session.Remove("newprod");
			FillRepeaterProducts(id);
			Session["review"] = "0";
			FillFileRep(id);

			SrcLead.OpportunityID = Convert.ToInt64(id);
			SrcLead.FromOpportunity = true;
			SrcLead.FillAdvancedLead();
			Opportunity_LoadLead.Visible=true;
			Opportunity_LoadCompany.Visible=true;

		}

		private void InitSheetPaging(string sqlString, bool x)
		{
			DataTable dt = DatabaseConnection.CreateDataset(sqlString).Tables[0];
			string[] myarray = new string[dt.Rows.Count];
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					myarray[i] = dt.Rows[i]["ContactID"] + "|" + dt.Rows[i]["CompanyName"] + "|" + dt.Rows[i]["ID"];
				}
			}

			if (x)
				Session["SheetCompany"] = myarray;
			else
				Session["SheetLead"] = myarray;

		}

		private void FillRepeaterProducts(int id)
		{
			DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM CRM_OPPPRODUCTROWS WHERE OPPORTUNITYID=" + id + " ORDER BY CATALOGID").Tables[0];

			ArrayList np = new ArrayList();
			long catid = 0;
			foreach (DataRow ddr in dt.Rows)
			{
				DataRow pInfo = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRODUCTS WHERE ID=" + ddr["CatalogID"].ToString()).Tables[0].Rows[0];
				PurchaseProduct newprod = new PurchaseProduct();
				if (catid != (long) ddr["CatalogID"])
				{
					newprod.id = Convert.ToInt64(ddr["CatalogID"]);
					newprod.ShortDescription = pInfo["ShortDescription"].ToString();
					newprod.LongDescription = pInfo["LongDescription"].ToString();
					newprod.UM = pInfo["Unit"].ToString();
					newprod.Qta = (int) ddr["Qta"];
					newprod.UnitPrice = (decimal) ddr["Uprice"];
					newprod.Vat = (pInfo["Vat"] != DBNull.Value) ? Convert.ToDecimal(pInfo["Vat"]) : 0;
					newprod.ListPrice = Math.Round(Convert.ToDecimal(newprod.Qta)*newprod.UnitPrice, 2);
					newprod.FinalPrice = (decimal) ddr["NewUPrice"];
					if ((byte) ddr["Type"] == 0)
					{
						newprod.Contacts = 1;
						newprod.Leads = 0;
					}
					else
					{
						newprod.Leads = 1;
						newprod.Contacts = 0;
					}

					np.Add(newprod);
					catid = (long) ddr["CatalogID"];
				}
				else
				{
					newprod = (PurchaseProduct) np[np.Count - 1];
					newprod.Qta += (int) ddr["Qta"];
					newprod.ListPrice += Math.Round(Convert.ToDecimal(newprod.Qta)*newprod.UnitPrice, 2);

					if ((byte) ddr["Type"] == 0)
						newprod.Contacts += 1;
					else
						newprod.Leads += 1;

					np[np.Count - 1] = newprod;
				}

			}

			Session.Remove("newprod");
			Session["newprod"] = np;

			RepeaterEstProduct.DataSource = np;
			RepeaterEstProduct.DataBind();
			RepeaterEstProduct.Visible = true;
		}

		private void SaveProductOpportunity(int id)
		{
			DatabaseConnection.DoCommand("DELETE FROM CRM_OPPPRODUCTROWS WHERE OPPORTUNITYID=" + id);

			ArrayList pp = (ArrayList) Session["newprod"];
			decimal expectedRevenue = 0;
			foreach (PurchaseProduct Pprod in pp)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("OPPORTUNITYID", id);
					dg.Add("DESCRIPTION", Pprod.LongDescription);
					dg.Add("UPRICE", Pprod.UnitPrice);
					dg.Add("NEWUPRICE", Pprod.FinalPrice);
					dg.Add("CATALOGID", Pprod.id);
					dg.Add("QTA", Pprod.Qta);
					dg.Execute("CRM_OPPPRODUCTROWS");
				}
				expectedRevenue += Pprod.FinalPrice;
			}


			Session.Remove("newprod");

			string sqlString = "SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + id;

			DataSet dsContact = new DataSet();
			dsContact = DatabaseConnection.CreateDataset(sqlString);

			foreach (DataRow row in dsContact.Tables[0].Rows)
			{
				using(DigiDapter dg = new DigiDapter())
				{
					dg.Add("EXPECTEDREVENUE", expectedRevenue);

					string percId = DatabaseConnection.SqlScalar("SELECT TABLETYPEID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTID = " + row["contactid"].ToString() + " AND OPPORTUNITYID = " + id + " AND CONTACTTYPE=" + row["ContactType"].ToString() + " AND TYPE= 3;");
				if (percId.Length > 0)
				{
					decimal percentage = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE ID=" + percId));
					dg.Add("INCOMEPROBABILITY", (expectedRevenue*percentage/100));
				}
				else
				{
					dg.Add("INCOMEPROBABILITY", 0);
				}
			dg.Execute("","ID="+row["id"]);
				}

			}

		}

		private void FillRepeaterPartner()
		{
			string sqlQuery = "SELECT CRM_OPPORTUNITYPARTNERS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME " +
				"FROM CRM_OPPORTUNITYPARTNERS LEFT OUTER JOIN " +
				"BASE_COMPANIES ON CRM_OPPORTUNITYPARTNERS.COMPANYID = BASE_COMPANIES.ID " +
				"WHERE CRM_OPPORTUNITYPARTNERS.OPPORTUNITYID=" + int.Parse(Opportunity_ID.Text);
			RepeaterPartner.DataSource = DatabaseConnection.CreateDataset(sqlQuery).Tables[0];
			RepeaterPartner.DataBind();
			if (RepeaterPartner.Items.Count > 0)
			{
				RepeaterPartner.Visible = true;
				RepeaterInfo.Visible = false;
			}
			else
			{
				RepeaterPartner.Visible = false;
				RepeaterInfo.Text =Root.rm.GetString("CRMopptxt71");
				RepeaterInfo.Visible = true;
			}

		}

		public void RepeaterPartner_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "PartnerCommand":
					DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CRM_OPPORTUNITYPARTNERS WHERE ID=" + int.Parse(((Literal) e.Item.FindControl("PartnerID")).Text)).Tables[0].Rows[0];
					TextboxPartnerOppID.Text = dr["ID"].ToString();
					TextboxPartnerID.Text = dr["CompanyID"].ToString();
					TextboxPartner.Text = ((LinkButton) e.Item.FindControl("PartnerCommand")).Text;
					Opportunity_NotePartner.Text = dr["Note"].ToString();
					RepeaterPartner.Visible = false;
					PartnerCard.Visible = true;
					Tabber.Selected = VisPartner.ID;
					break;
			}
		}

		public void RepeaterPartnerDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton PartnerCommand = (LinkButton) e.Item.FindControl("PartnerCommand");
					PartnerCommand.Text = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName"));
					break;
			}
		}

		private void RefreshOpportunityAmount(int id)
		{
			decimal expectedRevenue = 0;
			decimal amountClosed = 0;
			decimal IncomeProbability = 0;
			try
			{
				expectedRevenue = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT SUM(EXPECTEDREVENUE) FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + id));
			}
			catch
			{
				expectedRevenue = 0;
			}
			try
			{
				amountClosed = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT SUM(AMOUNTCLOSED) FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + id));
			}
			catch
			{
				amountClosed = 0;
			}
			try
			{
				IncomeProbability = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT SUM(INCOMEPROBABILITY) FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + id));
			}
			catch
			{
				IncomeProbability = 0;
			}

			DatabaseConnection.DoCommand("UPDATE CRM_OPPORTUNITY SET EXPECTEDREVENUE='" + expectedRevenue.ToString().Replace(",", ".") + "',AMOUNTCLOSED='" + amountClosed.ToString().Replace(",", ".") + "',INCOMEPROBABILITY='" + IncomeProbability.ToString().Replace(",", ".") + "' WHERE ID=" + id);

			Trace.Warn("querycambio", "SELECT CHANGETOEURO FROM CURRENCYTABLE WHERE ID=" + DatabaseConnection.SqlScalar("SELECT CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + id));
			decimal change = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT CHANGETOEURO FROM CURRENCYTABLE WHERE ID=(SELECT TOP 1 CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + id + ")"));

			((TextBox) OpportunityCard.FindControl("Opportunity_ExpectedRevenue")).Text = (Math.Round(expectedRevenue*change, 2)).ToString();
			((TextBox) OpportunityCard.FindControl("Opportunity_IncomeProbability")).Text = (Math.Round(IncomeProbability*change, 2)).ToString();
			((TextBox) OpportunityCard.FindControl("Opportunity_AmountClosed")).Text = (Math.Round(amountClosed*change, 2)).ToString();
		}

		public void RepeaterOpportunity_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenOP":
					ViewOpportunity(int.Parse(((Literal) e.Item.FindControl("IDOp")).Text));

					break;
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(this.NewRepeaterOpportunity.MultiDeleteListArray,"CRM_Opportunity");
					this.NewRepeaterOpportunity.DataBind();
					break;
			}
		}



		private void Delete(int id)
		{
			DatabaseConnection.DoCommand("DELETE FROM CRM_OPPORTUNITY WHERE ID =" + id);
			FillRepeaterOpportunity();
		}

		private void EnableModify(bool newOp)
		{
			TextBox txtfield;

			txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_Title");
			txtfield.ReadOnly = false;
			if (newOp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_Owner");
			txtfield.ReadOnly = false;
			if (newOp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			Opportunity_Currency.Visible = true;
			LabelCurrency.Visible = false;

			txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_Description");
			txtfield.ReadOnly = false;
			if (newOp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";


			PopAccount.Visible = true;
			if (newOp) txtfield.Text = String.Empty;
			Opportunity_Submit.Visible = true;
			Opportunity_SubmitUP.Visible = true;
			Opportunity_Modify.Visible = false;
			Opportunity_Access.Visible = true;
			Opportunity_LoadLead.Visible=false;
			Opportunity_LoadCompany.Visible=false;
			if (newOp)
			{
				txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_CreatedBy");
				txtfield.Text = String.Empty;
				txtfield = (TextBox) OpportunityCard.FindControl("Opportunity_LastModifiedBy");
				txtfield.Text = String.Empty;
			}
			FillRepeaterProducts(int.Parse(Opportunity_ID.Text));
			AddKeepAlive();
		}

		private string RepeaterCompanyQuery(int id, int contactId)
		{
			return RepeaterCompanyQuery(id, contactId, "");
		}

		private string RepeaterCompanyQuery(int id, int contactId, string find)
		{
			StringBuilder SqlCompanies = new StringBuilder();

			if (contactId == 0)
			{
				SqlCompanies.Append("SELECT CRM_OPPORTUNITYCONTACT.ID, CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, CRM_OPPORTUNITYCONTACT.CONTACTID, CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, ACCOUNT.SURNAME+' '+ACCOUNT.NAME AS SALESPERSONNAME, CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE, CRM_OPPORTUNITYCONTACT.ENDDATE, BASE_COMPANIES.COMPANYNAME,");
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS STATEDESCR,", id, contactId, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS PHASEDESCR ", id, contactId, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("FROM CRM_OPPORTUNITYCONTACT ");
				SqlCompanies.Append("INNER JOIN BASE_COMPANIES ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID ");
				SqlCompanies.Append("INNER JOIN Account ON CRM_OpportunityContact.SalesPerson = Account.UID ");
				if (find.Length > 0)
					SqlCompanies.AppendFormat("WHERE BASE_COMPANIES.COMPANYNAME LIKE '%{2}%' AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1} ORDER BY CRM_OPPORTUNITYCONTACT.ID DESC", id, contactId, DatabaseConnection.FilterInjection(find));
				else
					SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1} ORDER BY CRM_OPPORTUNITYCONTACT.ID DESC", id, contactId);


			}
			else
			{
				SqlCompanies.Append("SELECT CRM_OPPORTUNITYCONTACT.ID, CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, CRM_OPPORTUNITYCONTACT.CONTACTID, CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, ACCOUNT.SURNAME+' '+ACCOUNT.NAME AS SALESPERSONNAME, CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE, CRM_OPPORTUNITYCONTACT.ENDDATE, (ISNULL(CRM_LEADS.COMPANYNAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'')+' '+ISNULL(CRM_LEADS.NAME,'')) AS COMPANYNAME,");
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS STATEDESCR,", id, contactId, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS PHASEDESCR ", id, contactId, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("FROM CRM_OPPORTUNITYCONTACT ");
				SqlCompanies.Append("INNER JOIN CRM_LEADS ON CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_LEADS.ID ");
				SqlCompanies.Append("INNER JOIN ACCOUNT ON CRM_OPPORTUNITYCONTACT.SALESPERSON = ACCOUNT.UID ");
				if (find.Length > 0)
				{
					if (find.StartsWith("$"))
						SqlCompanies.AppendFormat("WHERE ({2}) AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1} ORDER BY CRM_OPPORTUNITYCONTACT.ID DESC", id, contactId, find.Substring(1, find.Length - 1));
					else
						SqlCompanies.AppendFormat("WHERE (CRM_LEADS.COMPANYNAME LIKE '%{2}%' OR CRM_LEADS.SURNAME LIKE '%{2}%' OR CRM_LEADS.NAME LIKE '%{2}%') AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1} ORDER BY CRM_OPPORTUNITYCONTACT.ID DESC", id, contactId, find);
				}
				else
					SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1} ORDER BY CRM_OPPORTUNITYCONTACT.ID DESC", id, contactId);


			}

			Trace.Warn("aziende", SqlCompanies.ToString());
			return SqlCompanies.ToString();
		}

		protected DataSet FillRepeaterCompany(int id, int contactid)
		{
			StringBuilder SqlCompanies = new StringBuilder();
			if (contactid == 0)
			{
				SqlCompanies.Append("SELECT CRM_OPPORTUNITYCONTACT.ID, CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, CRM_OPPORTUNITYCONTACT.CONTACTID, CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, ACCOUNT.SURNAME+' '+ACCOUNT.NAME AS SALESPERSONNAME, CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE, BASE_COMPANIES.COMPANYNAME,");
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS STATEDESCR,", id, contactid, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS PHASEDESCR ", id, contactid, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("FROM CRM_OPPORTUNITYCONTACT ");
				SqlCompanies.Append("INNER JOIN BASE_COMPANIES ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID ");
				SqlCompanies.Append("INNER JOIN ACCOUNT ON CRM_OPPORTUNITYCONTACT.SALESPERSON = ACCOUNT.UID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1}", id, contactid);
			}
			else
			{
				SqlCompanies.Append("SELECT CRM_OPPORTUNITYCONTACT.ID, CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, CRM_OPPORTUNITYCONTACT.CONTACTID, CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, ACCOUNT.SURNAME+' '+ACCOUNT.NAME AS SALESPERSONNAME, CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE, (ISNULL(CRM_LEADS.COMPANYNAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'')+' '+ISNULL(CRM_LEADS.NAME,'')) AS COMPANYNAME,");
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 1 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS STATEDESCR,", id, contactid, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("(SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION ");
				SqlCompanies.Append("FROM CRM_CROSSOPPORTUNITY INNER JOIN ");
				SqlCompanies.Append("CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID = CRM_OPPORTUNITYTABLETYPE.K_ID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYTABLETYPE.TYPE = 2 AND LANG='{2}' AND CRM_CROSSOPPORTUNITY.CONTACTTYPE={1} AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID = {0} AND CRM_CROSSOPPORTUNITY.CONTACTID = CRM_OPPORTUNITYCONTACT.CONTACTID) AS PHASEDESCR ", id, contactid, UC.Culture.Substring(0, 2));
				SqlCompanies.Append("FROM CRM_OPPORTUNITYCONTACT ");
				SqlCompanies.Append("INNER JOIN CRM_LEADS ON CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_LEADS.ID ");
				SqlCompanies.Append("INNER JOIN ACCOUNT ON CRM_OPPORTUNITYCONTACT.SALESPERSON = ACCOUNT.UID ");
				SqlCompanies.AppendFormat("WHERE CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE={1}", id, contactid);
			}

			return DatabaseConnection.CreateDataset(SqlCompanies.ToString());
		}


		public void RepeaterReferrerGeneralDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					HtmlContainerControl ModifyReferrer = (HtmlContainerControl) e.Item.FindControl("ModifyReferrer");
					ModifyReferrer.Visible = false;
					LinkButton ModReferrer = (LinkButton) e.Item.FindControl("ModReferrer");
					ModReferrer.Text =Root.rm.GetString("CRMopptxt19");
					LinkButton SaveReferrer = (LinkButton) e.Item.FindControl("SaveReferrer");
					SaveReferrer.Text =Root.rm.GetString("CRMopptxt18");
					DropDownList Character = (DropDownList) e.Item.FindControl("Character");

					foreach (ListItem li in Character.Items)
					{
						if (li.Value == DataBinder.Eval((DataRowView) e.Item.DataItem, "CharacterText").ToString())
							li.Selected = true;
					}
					break;
			}
		}

		public void RepeaterReferrerGeneralCommand(object source, RepeaterCommandEventArgs e)
		{
			HtmlContainerControl ModifyReferrer;
			HtmlContainerControl ViewReferrer;
			Trace.Warn("CommandName RepeaterReferrerCommand", e.CommandName);
			switch (e.CommandName)
			{
				case "ModReferrer":
					ModifyReferrer = (HtmlContainerControl) e.Item.FindControl("ModifyReferrer");
					ViewReferrer = (HtmlContainerControl) e.Item.FindControl("ViewReferrer");
					ModifyReferrer.Visible = true;
					ViewReferrer.Visible = false;
					break;
				case "SaveReferrer":
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("ROLE", ((TextBox) e.Item.FindControl("Role")).Text);
						try
						{
							dg.Add("PERCDECISIONAL", ((TextBox) e.Item.FindControl("PercDecisional")).Text);
						}
						catch
						{
							dg.Add("PERCDECISIONAL", 1);
						}
						dg.Add("CHARACTERTEXT", ((DropDownList) e.Item.FindControl("Character")).SelectedItem.Value);

						dg.Execute("CRM_CROSSOPPORTUNITYREFERRING", "ID=" + int.Parse(((TextBox) e.Item.FindControl("CrossID")).Text));
					}
					ModifyReferrer = (HtmlContainerControl) e.Item.FindControl("ModifyReferrer");
					ViewReferrer = (HtmlContainerControl) e.Item.FindControl("ViewReferrer");
					ModifyReferrer.Visible = false;
					ViewReferrer.Visible = true;

					break;
			}

		}

		public void RepeaterCompanyDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					int opId = int.Parse(Opportunity_ID.Text);
					LinkButton LabelOpenCompany = (LinkButton) e.Item.FindControl("LabelOpenCompany");
					if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "EndDate")).Length > 0)
						LabelOpenCompany.CssClass = "LinethroughGray linked";
					else
						LabelOpenCompany.CssClass = "linked";


					Label IncomeProbabilityLabel = (Label) e.Item.FindControl("IncomeProbabilityLabel");
					decimal change = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT CHANGETOEURO FROM CURRENCYTABLE WHERE ID=(SELECT CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + opId + ")"));
					string value = Math.Round((Convert.ToDecimal(DataBinder.Eval((DataRowView) e.Item.DataItem, "IncomeProbability"))*change), 2).ToString();
					IncomeProbabilityLabel.Text = ((Label) OpportunityCard.FindControl("Opportunity_IncomeProbabilitySymbol")).Text + "&nbsp;" + value;

					break;
			}
		}

		public void RepeaterCompanyCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenCompany":
					string opId = Opportunity_ID.Text;
					string coId = ((Literal) e.Item.FindControl("IdCompany")).Text;
					string coCrossId = ((Literal) e.Item.FindControl("IDCross")).Text;
					OppCompany.OpID = opId;
					OppCompany.CoID = coId;
					OppCompany.CoCrossID = coCrossId;
					OppCompany.Visible=true;
					tabControl.Visible = false;
					OppCompany.BindData();
					break;
				case "MultiDeleteButton":
					ArrayList ar = this.RepeaterCompany.MultiDeleteListArray;

					foreach (string i in ar)
					{
						DeleteCompanyFromOpportunity(i, "0");
					}


					string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 0);
					InitSheetPaging(raq, true);
					this.RepeaterCompany.sqlDataSource=raq;
					this.RepeaterCompany.DataBind();

					break;
			}
		}

		private void DeleteCompanyFromOpportunity(string id, string type)
		{
			DatabaseConnection.DoCommand("DELETE FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=" + type + " AND OPPORTUNITYID=" + int.Parse(Opportunity_ID.Text) + " AND CONTACTID =" + id);
		}



		private void EnableModifyCompetitor(bool newComp, bool canModify)
		{
			TextBox txtfield;
			txtfield = (TextBox) CompetitorCard.FindControl("Competitor_CompanyName");
			if (canModify) txtfield.ReadOnly = false;
			if (newComp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) CompetitorCard.FindControl("Competitor_Description");
			if (canModify) txtfield.ReadOnly = false;
			if (newComp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) CompetitorCard.FindControl("Competitor_Strengths");
			if (canModify) txtfield.ReadOnly = false;
			if (newComp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) CompetitorCard.FindControl("Competitor_Weaknesses");
			if (canModify) txtfield.ReadOnly = false;
			if (newComp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) CompetitorCard.FindControl("Competitor_Note");
			if (canModify) txtfield.ReadOnly = false;
			if (newComp) txtfield.Text = String.Empty;
			txtfield.CssClass = "BoxDesign";
			txtfield = (TextBox) CompetitorCard.FindControl("IDCompetitor");
			if (newComp)
				txtfield.Text = "-1";

			RepeaterCompetitor.Visible = false;
			CompetitorInfo.Text = String.Empty;

			RadioButtonList RBL = (RadioButtonList) CompetitorCard.FindControl("Competitor_Evaluation");
			if (canModify)
				RBL.Enabled = true;
			else
				RBL.Enabled = false;

		}

		private void SaveCompetitorFunction()
		{
			int competitorID = int.Parse(((TextBox) OpportunityCard.FindControl("Competitor_CompetitorID")).Text);
			bool newCompetitor;
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("COMPETITOR", 1, 'U');
				dg.Add("COMPANYNAME", ((TextBox) CompetitorCard.FindControl("Competitor_CompanyName")).Text, 'I');
				dg.Add("COMPANYNAME", StaticFunctions.FilterSearch(((TextBox) CompetitorCard.FindControl("Competitor_CompanyName")).Text), 'I');
				dg.Add("OWNERID", UC.UserId, 'I');
				dg.Add("GROUPS", "|" + UC.UserGroupId + "|", 'I');
				dg.Add("COMPETITOR", 1, 'I');
				dg.Add("DESCRIPTION", ((TextBox) CompetitorCard.FindControl("Competitor_Description")).Text, 'I');
				dg.Add("EVALUATION", ((RadioButtonList) CompetitorCard.FindControl("Competitor_Evaluation")).SelectedItem.Value);
				object newCompetitorID =  dg.Execute("BASE_COMPANIES", "ID=" + competitorID, DigiDapter.Identities.Identity);
				newCompetitor = dg.RecordInserted;
				if(newCompetitor)
					competitorID=(int)newCompetitorID;
			}

			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OPPORTUNITYID", Opportunity_ID.Text, 'I');
				dg.Add("COMPETITORID", competitorID);
				dg.Add("STRENGTHS", ((TextBox) CompetitorCard.FindControl("Competitor_Strengths")).Text, 'U');
				dg.Add("WEAKNESSES", ((TextBox) CompetitorCard.FindControl("Competitor_Weaknesses")).Text, 'U');
				dg.Add("NOTE", ((TextBox) CompetitorCard.FindControl("Competitor_Note")).Text, 'U');
				dg.Execute("CRM_OPPORTUNITYCOMPETITOR", "ID=" + int.Parse(((TextBox) OpportunityCard.FindControl("IDCompetitor")).Text));
				if (dg.RecordInserted)
					newCompetitor = true;
			}
			if (newCompetitor)
			{
				DataTable dtCross = DatabaseConnection.CreateDataset("SELECT CONTACTID,CONTACTTYPE FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + int.Parse(Opportunity_ID.Text)).Tables[0];
				foreach (DataRow d in dtCross.Rows)
				{
					DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSCONTACTCOMPETITOR (COMPETITORID,CONTACTID,CONTACTTYPE) VALUES (" + competitorID + "," + d[0].ToString() + "," + d[1].ToString() + ")");
				}
			}

			FillRepeaterCompetitor();
		}

		private void FillRepeaterCompetitor()
		{
			RepeaterCompetitor.DataSource = DatabaseConnection.CreateDataset("SELECT ID,COMPETITORID,EVALUATION,COMPANYNAME FROM OPPORTUNITYCOMPETITOR_VIEW WHERE OPPORTUNITYID=" + int.Parse(Opportunity_ID.Text));
			RepeaterCompetitor.DataBind();
			if (RepeaterCompetitor.Items.Count > 0)
			{
				RepeaterCompetitor.Visible = true;
				CompetitorInfo.Text = String.Empty;
			}
			else
			{
				RepeaterCompetitor.Visible = false;
				CompetitorInfo.Text =Root.rm.GetString("CRMopptxt50");
			}
		}

		public void RepeaterCompetitor_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenComp":
					FillCompetitorCard(int.Parse(((TextBox) e.Item.FindControl("CompetitorID")).Text));
					CompetitorCard.Visible = true;
					RepeaterCompetitor.Visible = false;
					Tabber.Selected = VisCompetitor.ID;
					break;
				case "DeleteCompetitor":
					deletecompetitor(int.Parse(((TextBox) e.Item.FindControl("CompetitorID")).Text));
					break;
			}
		}

		private void deletecompetitor(int id)
		{
			DatabaseConnection.DoCommand("DELETE FROM CRM_OPPORTUNITYCOMPETITOR WHERE COMPETITORID=" + id);
			DatabaseConnection.DoCommand("DELETE FROM CRM_CROSSCONTACTCOMPETITOR WHERE COMPETITORID=" + id);
			FillRepeaterCompetitor();
		}

		public void RepeaterCompetitorDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton OpenCompetitor = (LinkButton) e.Item.FindControl("OpenCompetitor");
					OpenCompetitor.Text = DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName").ToString();
					LinkButton DeleteCompetitor = (LinkButton) e.Item.FindControl("DeleteCompetitor");
					DeleteCompetitor.Text =Root.rm.GetString("CRMopptxt25");
					DeleteCompetitor.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("CRMopptxt65") + "');");

					Label CompetitorStars = (Label) e.Item.FindControl("CompetitorStars");
					byte iStars = Convert.ToByte(DataBinder.Eval((DataRowView) e.Item.DataItem, "Evaluation"));

					if (iStars > 0)
					{
						bool halfStar = false;
						if (iStars%2 > 0)
						{
							halfStar = true;
							iStars--;
						}
						iStars = (byte) (iStars/2);
						CompetitorStars.Text = String.Empty;
						for (int i = 0; i < iStars; i++)
							CompetitorStars.Text += "<img src='/images/starfull.gif'>";
						if (halfStar == true)
						{
							CompetitorStars.Text += "<img src='/images/starthalf.gif'>";
							iStars++;
						}
						for (int i = iStars + 1; i <= 5; i++)
							CompetitorStars.Text += "<img src='/images/starnone.gif'>";
					}
					break;
			}
		}

		private void FillCompetitorCard(int id)
		{
			TextBox txtfield;
			DataSet CompCard = DatabaseConnection.CreateDataset("SELECT * FROM OPPORTUNITYCOMPETITOR_VIEW WHERE COMPETITORID=" + id + " AND OPPORTUNITYID=" + Opportunity_ID.Text);

			foreach (DataColumn cc in CompCard.Tables[0].Columns)
			{
				switch (cc.ColumnName)
				{
					case "Evaluation":
						RadioButtonList Evaluation = (RadioButtonList) CompetitorCard.FindControl("Competitor_Evaluation");
						foreach (ListItem li in Evaluation.Items)
						{
							if (li.Value == CompCard.Tables[0].Rows[0][cc.ColumnName].ToString())
							{
								li.Selected = true;
								break;
							}
						}
						break;
					case "ID":
						txtfield = (TextBox) CompetitorCard.FindControl("IDCompetitor");
						txtfield.Text = CompCard.Tables[0].Rows[0][cc.ColumnName].ToString();
						break;
					default:
						try
						{
							txtfield = (TextBox) CompetitorCard.FindControl("Competitor_" + cc.ColumnName);
							txtfield.Text = CompCard.Tables[0].Rows[0][cc.ColumnName].ToString();
						}
						catch
						{
						}
						break;
				}
			}
			CompCard.Clear();
			EnableModifyCompetitor(false, true);
			SaveCompetitor.Visible = true;
		}


		public void RepeaterLeadDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					int opId = int.Parse(Opportunity_ID.Text);
					LinkButton OpenLead = (LinkButton) e.Item.FindControl("OpenLead");
					if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "EndDate")).Length > 0)
						OpenLead.CssClass = "LinethroughGray linked";
					else
						OpenLead.CssClass = "linked";

					Label IncomeProbabilityLabel = (Label) e.Item.FindControl("IncomeProbabilityLabel");
					decimal change = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT CHANGETOEURO FROM CURRENCYTABLE WHERE ID=" + DatabaseConnection.SqlScalar("SELECT CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + opId)));
					string value = Math.Round((Convert.ToDecimal(DataBinder.Eval((DataRowView) e.Item.DataItem, "IncomeProbability"))*change), 2).ToString();
					IncomeProbabilityLabel.Text = ((Label) OpportunityCard.FindControl("Opportunity_IncomeProbabilitySymbol")).Text + "&nbsp;" + value;
					break;
			}
		}

		public void RepeaterLeadCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenLead":
					string opId = Opportunity_ID.Text;
					string coId = ((Literal) e.Item.FindControl("IdCompany")).Text;
					string coCrossId = ((Literal) e.Item.FindControl("IDCross")).Text;

					OppLead.OpID = opId;
					OppLead.CoID = coId;
					OppLead.CoCrossID = coCrossId;
					OppLead.Visible=true;
					tabControl.Visible = false;
					OppLead.BindData();
					break;
				case "MultiDeleteButton":
					ArrayList ar = this.RepeaterLead.MultiDeleteListArray;

					foreach (string i in ar)
					{
						DeleteCompanyFromOpportunity(i, "1");
					}

					string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1);
					InitSheetPaging(raq, false);
					this.RepeaterLead.sqlDataSource=raq;
					this.RepeaterLead.DataBind();
					break;

			}
		}


		private void RefreshCompanies()
		{

			string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 0);
			InitSheetPaging(raq, true);
			this.RepeaterCompany.sqlDataSource=raq;
			this.RepeaterCompany.DataBind();


			tabControl.Visible = true;

			Tabber.Visible=true;
			FillRepeaterProducts(int.Parse(Opportunity_ID.Text));
		}

		private void RefreshLeads()
		{
			string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1);
			InitSheetPaging(raq, false);
			this.RepeaterLead.sqlDataSource=raq;
			this.RepeaterLead.DataBind();


			tabControl.Visible = true;
			Tabber.Visible=true;
		}

		public void RefreshOpp(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "RefreshRepeaterProducts":
					FillRepeaterProducts(int.Parse(Opportunity_ID.Text));
					break;
				case "RefreshRepeaterCompany":
					string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 0);
					InitSheetPaging(raq, true);
					this.RepeaterCompany.sqlDataSource=raq;
					this.RepeaterCompany.DataBind();

					OppCompany.Visible=false;
					tabControl.Visible = true;
					Tabber.Visible=true;
					FillRepeaterProducts(int.Parse(Opportunity_ID.Text));
					break;
				case "RefreshCloseFrame":
					OppCompany.Visible=false;
					tabControl.Visible = false;
					break;
				case "RefreshRepeaterLead":

					raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1);
					InitSheetPaging(raq, false);
					this.RepeaterLead.sqlDataSource=raq;
					this.RepeaterLead.DataBind();

					break;
			}
			RefreshOpportunityAmount(int.Parse(Opportunity_ID.Text));
		}

		private void btnAddProduct_Click(object sender, EventArgs e)
		{
			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id = Convert.ToInt64(((TextBox) Page.FindControl("EstProductID")).Text);
			newprod.ShortDescription = ((TextBox) Page.FindControl("EstProduct")).Text;
			newprod.LongDescription = ((TextBox) Page.FindControl("EstDescription2")).Text;
			newprod.UM = ((TextBox) Page.FindControl("EstUm")).Text;
			newprod.Qta = Convert.ToInt32(((TextBox) Page.FindControl("EstQta")).Text);

			decimal chFrom = (1/Convert.ToDecimal(Opportunity_Currency.SelectedValue.Split('|')[1]));
			newprod.UnitPrice = Math.Round(StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstUp")).Text)*chFrom, 2);
			newprod.Vat = (((TextBox) Page.FindControl("EstVat")).Text.Length > 0) ? Convert.ToDecimal(((TextBox) Page.FindControl("EstVat")).Text) : 0;
			newprod.ListPrice = Math.Round(StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstPl")).Text)*chFrom, 2);
			newprod.FinalPrice = Math.Round(StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstPf")).Text)*chFrom, 2);

			ArrayList np = new ArrayList();
			if (Session["newprod"] != null)
				np = (ArrayList) Session["newprod"];

			newprod.ObId = np.Count;

			np.Add(newprod);
			Session["newprod"] = np;

			RepeaterEstProduct.DataSource = np;
			RepeaterEstProduct.DataBind();
			RepeaterEstProduct.Visible = true;
			Session["prodchange"] = "true";
		}

		private void btnCalcPrice_Click(object sender, EventArgs e)
		{
			TextBox EstPl = (TextBox) Page.FindControl("EstPl");
			TextBox EstPf = (TextBox) Page.FindControl("EstPf");
			TextBox EstQta = (TextBox) Page.FindControl("EstQta");
			TextBox EstUp = (TextBox) Page.FindControl("EstUp");
			EstPl.Text = Convert.ToString(Math.Round((Convert.ToDecimal(EstQta.Text)*Convert.ToDecimal(EstUp.Text)), 2));
			EstPf.Text = EstPl.Text;
		}

		private void RepeaterEstProduct_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label ShortDescription = (Label) e.Item.FindControl("ShortDescription");
					Label UM = (Label) e.Item.FindControl("UM");
					Label Qta = (Label) e.Item.FindControl("Qta");
					Label UnitPrice = (Label) e.Item.FindControl("UnitPrice");
					Label NCompany = (Label) e.Item.FindControl("NCompany");
					Label Nlead = (Label) e.Item.FindControl("Nlead");
					PurchaseProduct newprod = (PurchaseProduct) e.Item.DataItem;
					ShortDescription.Text = newprod.ShortDescription;
					UM.Text = newprod.UM.ToString();
					Qta.Text = newprod.Qta.ToString();

					NCompany.Text = newprod.Contacts.ToString();
					Nlead.Text = newprod.Leads.ToString();

					string sy = Opportunity_Currency.SelectedValue.Split('|')[2];
					decimal chTo = Convert.ToDecimal(Opportunity_Currency.SelectedValue.Split('|')[1].Replace(".", ","));

					UnitPrice.Text = sy + " " + Math.Round((newprod.UnitPrice*chTo), 2).ToString();


					break;
			}
		}

		private void RepeaterEstProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelPurPro":
					break;
			}
		}

		public void FillFileRep(int id)
		{
			string sqlString = "SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ";
			sqlString += "LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ";
			sqlString += "LEFT OUTER JOIN FILECROSSTABLES ON FILEMANAGER.ID=FILECROSSTABLES.IDFILE ";
			sqlString += "LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ";
			sqlString += "WHERE ISREVIEW=0 AND FILECROSSTABLES.TABLENAME='CRM_OPPORTUNITY' AND FILECROSSTABLES.IDRIF=" + id;

			FileRep.DataSource = DatabaseConnection.CreateDataset(sqlString);
			FileRep.DataBind();
			if (FileRep.Items.Count > 0)
			{
				FileRepInfo.Visible = false;
				FileRep.Visible = true;
			}
			else
			{
				FileRepInfo.Visible = true;
				FileRep.Visible = false;
				FileRepInfo.Text =Root.rm.GetString("CRMcontxt61");
			}

		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
		}

		private void InitializeComponent()
		{
			this.BtnNew.Click += new EventHandler(this.Btn_Click);
			this.BtnFind.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_SubmitUP.Click += new EventHandler(this.Btn_Click);
			this.RepeaterEstProduct.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterEstProduct_ItemDataBound);
			this.RepeaterEstProduct.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterEstProduct_ItemCommand);
			this.Opportunity_Submit.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_LoadLead.Click += new EventHandler(this.Opportunity_LoadLead_Click);
			this.Opportunity_LoadCompany.Click += new EventHandler(this.Opportunity_LoadCompany_Click);
			this.Opportunity_Modify.Click += new EventHandler(this.Btn_Click);
			this.FindCompanyButton.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_NewCo.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_SearchCompany.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_CompanyList.Click += new EventHandler(this.Btn_Click);
			this.SearchCompanyBtn.Click += new EventHandler(this.SearchCompanyBtn_Click);
			this.RepeaterCompany.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterCompanyDatabound);
			this.RepeaterCompany.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterCompanyCommand);
			this.FindLeadButton.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_NewLead.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_SrcLead.Click += new EventHandler(this.Btn_Click);
			this.Opportunity_ElencoLead.Click += new EventHandler(this.Btn_Click);
			this.RepeaterLead.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterLeadDatabound);
			this.RepeaterLead.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterLeadCommand);
			this.NewActivityPhone.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityLetter.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityFax.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityMemo.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityEmail.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityVisit.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityGeneric.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivitySolution.Click += new EventHandler(this.NewActivity_Click);
			this.AddNewPartner.Click += new EventHandler(this.Btn_Click);
			this.PartnerSubmit.Click += new EventHandler(this.Btn_Click);
			this.RepeaterPartner.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterPartnerDatabound);
			this.RepeaterPartner.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterPartner_Command);
			this.AddNewCompetitor.Click += new EventHandler(this.Btn_Click);
			this.CloseCompetitor.Click += new EventHandler(this.Btn_Click);
			this.SaveCompetitor.Click += new EventHandler(this.Btn_Click);
			this.NewDoc.Click += new EventHandler(this.Btn_Click);
			this.NewRepeaterOpportunity.ItemCommand+=new RepeaterCommandEventHandler(RepeaterOpportunity_Command);

			this.Load += new EventHandler(this.Page_Load);

			this.OppCompany.Savecomp+=new SaveCompany(OppCompany_Savecomp);
			this.OppCompany.Hidecontrol+=new HideControl(OppCompany_Hidecontrol);

			this.OppLead.Savecomp+=new Digita.Tustena.SaveLead(OppLead_Savecomp);
			this.OppLead.Hidecontrol+=new Digita.Tustena.HideLeadControl(OppLead_Hidecontrol);

			this.RepeaterLead.RepeaterPost+=new RepeaterPostDelegate(RepeaterLead_RepeaterPost);
			this.RepeaterCompany.RepeaterPost+=new RepeaterPostDelegate(RepeaterCompany_RepeaterPost);
		}

		#endregion

		private void FileRep_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton Mod = (LinkButton) e.Item.FindControl("Modify");
					Mod.Text =Root.rm.GetString("Dsttxt16");
					LinkButton Rev = (LinkButton) e.Item.FindControl("Revision");
					Rev.Text =Root.rm.GetString("Dsttxt13");

					LinkButton RN = (LinkButton) e.Item.FindControl("ReviewNumber");
					Literal LRN = (Literal) e.Item.FindControl("LbReviewNumber");
					if (Session["review"].ToString() == "0")
					{
						RN.Visible = (RN.Text != "0");
						Rev.Visible = true;
						LRN.Visible = false;
					}
					else
					{
						RN.Visible = false;
						Rev.Visible = false;
						LRN.Visible = (LRN.Text != "0");
					}

					break;
			}
		}

		private void FileRep_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			Literal FileId = (Literal) e.Item.FindControl("FileId");
			switch (e.CommandName)
			{
				case "Down":

					DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileId.Text));

					string filename;
					filename = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"];
					string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();
					try
					{
						string downFile = filename + Path.GetExtension(realFileName);
						if (File.Exists(downFile))
						{
							Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
							Response.ContentType = "application/octet-stream";
							Response.TransmitFile(downFile);
							Response.Flush();
							Response.End();
							return;
						}
						else if (File.Exists(filename))
						{
							File.Move(filename, downFile);
							Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
							Response.ContentType = "application/octet-stream";
							Response.TransmitFile(downFile);
							Response.Flush();
							Response.End();
							return;
						}
						else
						{
							G.SendError("File lost", downFile);
						}


					}
					catch
					{
					}
					break;
				case "Modify":
				case "Revision":
					Session["IdDoc"] = FileId.Text;
					SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=37&gb=1", new string[2] {Opportunity_ID.Text, "d"});
					if (e.CommandName == "Modify")
						Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=m");
					else
						Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=e");
					break;
				case "ReviewNumber":
					Session["review"] = "1";
					StringBuilder sqlString = new StringBuilder();

					int fileID = int.Parse(((Literal) e.Item.FindControl("FileId")).Text);

					sqlString.Append("SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ");
					sqlString.Append("LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ");
					sqlString.Append("LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ");
					sqlString.Append("WHERE (FILEMANAGER.OWNERID=" + UC.UserId + " OR " + GroupsSecure("FILEMANAGER.GROUPS") + ") ");
					sqlString.Append("AND (FILEMANAGER.ID=" + fileID + " OR FILEMANAGER.ISREVIEW=" + fileID + ") ");
					sqlString.Append("ORDER BY REVIEWNUMBER DESC");

					FileRep.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
					FileRep.DataBind();
					break;
			}
		}

		private void SearchCompanyBtn_Click(object sender, EventArgs e)
		{
			DataTable dt = this.SearchCompany.GetCompanyTable();
			if (dt.Rows.Count > 0)
			{
				string find = "$";
				foreach (DataRow dr in dt.Rows)
				{
					find += "CRM_OPPORTUNITYCONTACT.CONTACTID=" + dr["id"].ToString() + " OR ";
				}

				string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1, find.Substring(0, find.Length - 3));
				InitSheetPaging(raq, false);
				this.RepeaterCompany.sqlDataSource=raq;
				this.RepeaterCompany.DataBind();



			}
			else
			{
				RepeaterCompany.Visible = false;

			}
			OppCompany.Visible=false;
		}

		private void SrcLeadBtn_Click(object sender, EventArgs e)
		{
			DataTable dt = this.SrcLead.GetLeadTable();
			if (dt.Rows.Count > 0)
			{
				string find = "$";
				foreach (DataRow dr in dt.Rows)
				{
					find += "CRM_OPPORTUNITYCONTACT.CONTACTID=" + dr["id"].ToString() + " OR ";
				}

				string raq = RepeaterCompanyQuery(int.Parse(Opportunity_ID.Text), 1, find.Substring(0, find.Length - 3));
				InitSheetPaging(raq, false);
				this.RepeaterLead.sqlDataSource=raq;
				this.RepeaterLead.DataBind();


				if (RepeaterLead.RowCount > 0)
				{
					TblSrcLead.Visible = false;
				}

			}
			else
			{
				RepeaterLead.Visible = false;

			}
			OppCompany.Visible=false;
		}

		private void Opportunity_LoadLead_Click(object sender, EventArgs e)
		{
			Session["FromOpp"] = this.Opportunity_ID.Text;
			Response.Redirect("/common/opploadfromlead.aspx?m=25&si=37");
		}

		private void Opportunity_LoadCompany_Click(object sender, EventArgs e)
		{
			Session["FromOpp"] = this.Opportunity_ID.Text;
			Response.Redirect("/common/opploadfromcompany.aspx?m=25&si=37");
		}

		private void OppCompany_Savecomp()
		{
			RefreshCompanies();
			Tabber.Selected=visCompany.ID;
		}

		private void OppCompany_Hidecontrol()
		{
			OppCompany.Visible=false;
			tabControl.Visible = true;
			Tabber.Visible=true;
			Tabber.Selected=visCompany.ID;
		}

		private void OppLead_Hidecontrol()
		{
			OppLead.Visible=false;
			tabControl.Visible = true;
			Tabber.Visible=true;
			Tabber.Selected=visLead.ID;
		}

		private void OppLead_Savecomp()
		{
			RefreshLeads();
			Tabber.Selected=visLead.ID;
		}

		private void RepeaterLead_RepeaterPost(string sender)
		{
			Tabber.Selected=this.visLead.ID;
		}

		private void RepeaterCompany_RepeaterPost(string sender)
		{
			Tabber.Selected=this.visCompany.ID;
		}
	}
}
