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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public class CRM_OppCompany : G
	{

		protected TustenaTabber Tabber;

		protected TustenaTab Table_Tab1;
		protected TustenaTab Table_Tab2;
		protected TustenaTab Table_Tab3;
		protected TustenaTab Table_Tab4;
		protected TustenaTab Table_Tab6;

		protected LinkButton CompanyNewSubmit;
		protected TextBox CompanyNewCompanyID;
		protected TextBox CompanyNewCompany;
		protected TextBox CompanyNewNote;
		protected TextBox CompanyNewID;
		protected Label CompanyNewInfo;
		protected TextBox CompanyNewExpectedRevenue;
		protected TextBox CompanyNewAmountClosed;
		protected TextBox CompanyNewAmountRevenuePercent;
		protected DropDownList CompanyNewStateList;
		protected DropDownList CompanyNewPhaseList;
		protected DropDownList CompanyNewProbList;
		protected TextBox CompanyNewSalesPersonID;
		protected TextBox CompanyNewSalesPerson;
		protected TextBox CompanyNewStartDate;
		protected TextBox CompanyNewEstimatedCloseDate;
		protected TextBox CompanyNewCloseDate;


		protected Repeater ViewContact;
		protected Repeater RepeaterReferrer;
		protected Label RepeaterReferrerInfo;
		protected Label CompanyCrossCompetitorInfo;

		protected TextBox Role;
		protected TextBox PercDecisional;
		protected DropDownList Character;
		protected LinkButton SaveNewReferrer;
		protected Repeater CompanyCrossCompetitor;

		protected ActivityChronology AcCronoAzOp;
		protected LinkButton NewActivityPhoneCoOp;
		protected LinkButton NewActivityLetterCoOp;
		protected LinkButton NewActivityFaxCoOp;
		protected LinkButton NewActivityMemoCoOp;
		protected LinkButton NewActivityEmailCoOp;
		protected LinkButton NewActivityVisitCoOp;
		protected LinkButton NewActivityGenericCoOp;
		protected LinkButton NewActivitySolutionCoOp;

		protected Label RepeaterActivityAzOpInfo;
		private int opId;
		private int coId;
		private int coCrossId;
		private decimal changeToEuro = 1;
		private string curSymbol = String.Empty;

		protected LinkButton BtnAddProduct;
		protected LinkButton BtnCalcPrice;
		protected Repeater RepeaterEstProduct;

		protected SheetPaging SheetP;
		private decimal subTotal = 0;

		protected DropDownList CompanyLostReasons;
		protected TextBox NewLostReason;

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				AddKeepAlive();
				if (!Page.IsPostBack)
				{
					Session.Remove("newprod");
					opId = int.Parse(Request.Params["OpID"].ToString());
					coId = int.Parse(Request.Params["CoID"].ToString());
					coCrossId = (Request.Params["CoCrossID"].ToString().Length>0)?int.Parse(Request.Params["CoCrossID"].ToString()):0;
					ViewState["OpID"] = opId;
					ViewState["CoID"] = coId;
					ViewState["CoCrossID"] = coCrossId;

					BtnAddProduct.Text =Root.rm.GetString("CRMopptxt82");
					BtnAddProduct.Attributes.Add("onclick", "return ValidateProduct()");
					BtnCalcPrice.Text =Root.rm.GetString("Esttxt16");

					if (Session["SheetCompany"] != null)
					{
						string[] temparray = (string[]) Session["SheetCompany"];
						this.SheetP.IDArray = temparray;
						for (int i = 0; i < temparray.Length; i++)
						{
							if (temparray[i].Split('|')[0].CompareTo(coId.ToString()) == 0)
							{
								this.SheetP.CurrentPosition = i;
								this.SheetP.enabledisable();
								break;
							}
						}
						Session.Remove("SheetCompany");
					}

				}
				else
				{
					opId = ((ViewState["OpID"].ToString().Length > 0) ? int.Parse(ViewState["OpID"].ToString()) : int.Parse(Request.Params["OpID"].ToString()));
					coId = ((ViewState["CoID"].ToString().Length > 0) ? int.Parse(ViewState["CoID"].ToString()) : int.Parse(Request.Params["CoID"].ToString()));
					coCrossId = ((ViewState["CoCrossID"].ToString().Length > 0) ? int.Parse(ViewState["CoCrossID"].ToString()) : int.Parse(Request.Params["CoCrossID"].ToString()));
					ViewState["OpID"] = opId;
					ViewState["CoID"] = coId;
					ViewState["CoCrossID"] = coCrossId;
				}

				DataRow dr = DatabaseConnection.CreateDataset("SELECT CHANGETOEURO,CHANGEFROMEURO,CURRENCYSYMBOL FROM CURRENCYTABLE WHERE ID=" + DatabaseConnection.SqlScalar("SELECT CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + opId)).Tables[0].Rows[0];
				changeToEuro = Convert.ToDecimal(dr[0]);
				if (changeToEuro.Equals(0)) changeToEuro = 1;
				curSymbol = dr[2].ToString();

				if (!Page.IsPostBack)
					FillRepeaterProducts(opId, coId);


				ViewContact.EnableViewState = false;
				if (!Page.IsPostBack)
				{
					CompanyNewSubmit.Text =Root.rm.GetString("CRMopptxt18");
					CompanyNewSubmit.Attributes.Add("onclick", "return checkbeforesubmit()");
					CompanyNewStateList.Attributes.Add("onchange", "selectchange(this)");
					if (coId != -1)
					{
						LoadData();
					}
					else
					{
						PrepareForNewCompany();
					}

				}
			}
		}

		private void FillLostReason()
		{
			CompanyLostReasons.Items.Add(new ListItem(Root.rm.GetString("Mottxt7"), "0"));
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_OPPLOSTREASONS").Tables[0];
			foreach (DataRow dr in dt.Rows)
			{
				string itemText = (dr[1].ToString().IndexOf("Mot") > -1) ?Root.rm.GetString(dr[1].ToString()) : dr[1].ToString();
				CompanyLostReasons.Items.Add(new ListItem(itemText, dr[0].ToString()));
			}
			CompanyLostReasons.Items.Add(new ListItem(Root.rm.GetString("Other"), "A99"));
			CompanyLostReasons.Attributes.Add("onchange", "SelectOther()");
		}

		private void LoadData()
		{
			AcCronoAzOp.ParentID = coId;
			AcCronoAzOp.OpportunityID = opId;
			AcCronoAzOp.AcType = (byte) AType.Company;
			AcCronoAzOp.FromFrame = true;
			AcCronoAzOp.Refresh();


			SaveNewReferrer.Text =Root.rm.GetString("CRMopptxt18");

			NewActivityPhoneCoOp.Text =Root.rm.GetString("Wortxt6");
			NewActivityLetterCoOp.Text =Root.rm.GetString("Wortxt7");
			NewActivityFaxCoOp.Text =Root.rm.GetString("Wortxt8");
			NewActivityMemoCoOp.Text =Root.rm.GetString("Wortxt9");
			NewActivityEmailCoOp.Text =Root.rm.GetString("Wortxt10");
			NewActivityVisitCoOp.Text =Root.rm.GetString("Wortxt11");
			NewActivityGenericCoOp.Text =Root.rm.GetString("Wortxt12");
			NewActivitySolutionCoOp.Text =Root.rm.GetString("Wortxt13");

			string queryState = "SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION,CRM_CROSSOPPORTUNITY.TYPE,CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=" + coId + " AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID=" + opId + " AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=0";
			DataTable dtState = DatabaseConnection.CreateDataset(queryState).Tables[0];

			string azStateListSelect = String.Empty;
			string azPhaseListSelect = String.Empty;
			string azProbListSelect = String.Empty;
			DataRow[] drstate = dtState.Select("type=1");
			try
			{
				azStateListSelect = drstate[0][2].ToString();
			}
			catch
			{
			}
			drstate = dtState.Select("type=2");
			try
			{
				azPhaseListSelect = drstate[0][2].ToString();
			}
			catch
			{
			}
			drstate = dtState.Select("type=3");
			try
			{
				azProbListSelect = drstate[0][2].ToString();
			}
			catch
			{
			}

			FillLostReason();


			FillDropListAZ(CompanyNewStateList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=1 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azStateListSelect.Length > 0) ? azStateListSelect : "");
			FillDropListAZ(CompanyNewPhaseList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=2 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azPhaseListSelect.Length > 0) ? azPhaseListSelect : "");
			FillDropListAZ(CompanyNewProbList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=3 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azProbListSelect.Length > 0) ? azProbListSelect : "");
			DataSet dsTemp = DatabaseConnection.CreateDataset("SELECT NOTE,EXPECTEDREVENUE,AMOUNTCLOSED,INCOMEPROBABILITY,SALESPERSON,STARTDATE,ESTIMATEDCLOSEDATE,ENDDATE,LOSTREASON FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID=" + opId + " AND CONTACTID=" + coId);

			if (dsTemp.Tables[0].Rows[0]["LostReason"] != DBNull.Value)
			{
				this.CompanyLostReasons.SelectedIndex = -1;
				foreach (ListItem li in this.CompanyLostReasons.Items)
				{
					if (li.Value == dsTemp.Tables[0].Rows[0]["LostReason"].ToString())
					{
						li.Selected = true;
						break;
					}
				}
			}


			CompanyNewNote.Text = (string) dsTemp.Tables[0].Rows[0]["Note"];
			CompanyNewCompany.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + coId);

			if (CompanyNewExpectedRevenue.Text.Length <= 0)
				CompanyNewExpectedRevenue.Text = Math.Round((Convert.ToDecimal(dsTemp.Tables[0].Rows[0]["ExpectedRevenue"])*changeToEuro), 2).ToString();

			CompanyNewAmountClosed.Text = Math.Round((Convert.ToDecimal(dsTemp.Tables[0].Rows[0]["AmountClosed"])*changeToEuro), 2).ToString();
			CompanyNewAmountRevenuePercent.Text = Math.Round((Convert.ToDecimal(dsTemp.Tables[0].Rows[0]["IncomeProbability"])*changeToEuro), 2).ToString();

			CompanyNewSalesPersonID.Text = dsTemp.Tables[0].Rows[0]["SalesPerson"].ToString();
			CompanyNewSalesPerson.Text = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dsTemp.Tables[0].Rows[0]["SalesPerson"].ToString());
			CompanyNewStartDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["StartDate"].ToString())).ToShortDateString();
			CompanyNewEstimatedCloseDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EstimatedCloseDate"].ToString())).ToShortDateString();
			if (dsTemp.Tables[0].Rows[0]["EndDate"].ToString().Length > 0)
				CompanyNewCloseDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dsTemp.Tables[0].Rows[0]["EndDate"].ToString())).ToShortDateString();
			else
				CompanyNewCloseDate.Text = String.Empty;

			Table_Tab1.LangHeader = DatabaseConnection.SqlScalar("SELECT TITLE FROM CRM_OPPORTUNITY WHERE ID=" + opId) + ":<br>" + CompanyNewCompany.Text;
			Table_Tab1.Selected = true;

			CompanyNewCompanyID.Text = coId.ToString();
			CompanyNewID.Text = coCrossId.ToString();
			Session["NewCompanyID"] = CompanyNewCompanyID.Text;

			Table_Tab1.Visible = true;
			Table_Tab2.Visible = false;
			Table_Tab3.Visible = false;
			Table_Tab4.Visible = false;
			Table_Tab6.Visible = false;

			this.SheetP.Visible = true;
		}

		private void FillView(int id)
		{
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT BASE_COMPANIES.*, CONTACTTYPE.CONTACTTYPE AS CONTACTTYPE, COMPANYTYPE.DESCRIPTION AS COMPANYTYPE, CONTACTESTIMATE.ESTIMATE AS ESTIMATEDESC ");
			sqlString.Append("FROM BASE_COMPANIES LEFT OUTER JOIN COMPANYTYPE ON BASE_COMPANIES.COMPANYTYPEID = COMPANYTYPE.K_ID AND COMPANYTYPE.LANG='" + UC.Culture + "' LEFT OUTER JOIN ");
			sqlString.Append("CONTACTTYPE ON BASE_COMPANIES.CONTACTTYPEID = CONTACTTYPE.ID LEFT OUTER JOIN ");
			sqlString.AppendFormat("CONTACTESTIMATE ON BASE_COMPANIES.ESTIMATE = CONTACTESTIMATE.ID WHERE BASE_COMPANIES.ID={0}", id);
			ViewContact.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
			ViewContact.DataBind();
			ViewContact.Visible = true;
		}

		public void ViewContact_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal ViewFreeFields = (Literal) e.Item.FindControl("ViewFreeFields");
					string sqlString;
					sqlString = "SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + (byte) CRMTables.Base_Companies;

					DataSet ds = DatabaseConnection.CreateDataset(sqlString);
					if (ds.Tables[0].Rows.Count > 0)
					{
						StringBuilder S = new StringBuilder();
						S.Append("<table width=\"100%\"><tr><td class=\"normal Bbot\"><br><b>FREE FIELDS</b></td></tr></table>");
						S.Append("<table width=\"50%\" class=\"normal\">");
						foreach (DataRow dr in ds.Tables[0].Rows)
						{
							S.AppendFormat("<tr><td width=\"40%\">{0}</td>", dr["name"]);
							DataSet afCross = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + dr["id"] + " AND ID=" + (Int64) DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));
							if (afCross.Tables[0].Rows.Count > 0)
							{
								S.AppendFormat("<td bgcolor=\"#FFFFFF\">{0}</td></tr>", afCross.Tables[0].Rows[0][0]);
							}
							else
							{
								S.Append("<td bgcolor=\"#FFFFFF\">&nbsp;</td></tr>");
							}
							afCross.Clear();
						}
						S.Append("</table>");
						ViewFreeFields.Text = S.ToString();
					}
					break;
			}
		}

		public void RepeaterReferrerDatabound(object source, RepeaterItemEventArgs e)
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
					FillDropListAZ(Character, "SELECT DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=4 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "description", "");
					foreach (ListItem li in Character.Items)
					{
						if (li.Value == DataBinder.Eval((DataRowView) e.Item.DataItem, "CharacterText").ToString())
							li.Selected = true;
					}
					break;
			}
		}

		public void RepeaterReferrerCommand(object source, RepeaterCommandEventArgs e)
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
						if (dg.RecordInserted) throw new Exception("Insert instead of update");
					}
					ModifyReferrer = (HtmlContainerControl) e.Item.FindControl("ModifyReferrer");
					ViewReferrer = (HtmlContainerControl) e.Item.FindControl("ViewReferrer");
					ModifyReferrer.Visible = false;
					ViewReferrer.Visible = true;
					FillRepeaterReferrer();
					break;
			}
		}

		public void CompanyCrossCompetitorCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "ModRelation":
					HtmlContainerControl ModifyCompetitor = (HtmlContainerControl) e.Item.FindControl("ModifyCompetitor");
					ModifyCompetitor.Visible = true;
					HtmlContainerControl ViewCompetitor = (HtmlContainerControl) e.Item.FindControl("ViewCompetitor");
					ViewCompetitor.Visible = false;
					break;
				case "SaveRelation":
					int idRel = int.Parse(((TextBox) e.Item.FindControl("TbxRelation")).Text);
					int idCross = int.Parse(((Literal) e.Item.FindControl("IDcross")).Text);
					DatabaseConnection.DoCommand(String.Format("UPDATE CRM_CROSSCONTACTCOMPETITOR SET WHERE ID=", idRel, idCross));
					FillRepeaterContactCompetitor();
					break;
			}
		}

		public void CompanyCrossCompetitorDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label Relation = (Label) e.Item.FindControl("Relation");
					TextBox TbxRelation = (TextBox) e.Item.FindControl("TbxRelation");
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Relation"));
					TbxRelation.Text = (sub.Length > 0) ? sub : "N/A";
					if (sub.Length > 90)
					{
						Regex rx = new Regex(@"(?s)\b.{1,87}\b");
						Relation.ToolTip = sub;
						Relation.Text += rx.Match(sub) + "&hellip;";
						Relation.Attributes.Add("style", "cursor:help;");
					}
					else
					{
						Relation.Text += (sub.Length > 0) ? sub : "N/A";
					}
					LinkButton ModRelation = (LinkButton) e.Item.FindControl("ModRelation");
					ModRelation.Text =Root.rm.GetString("CRMopptxt19");
					LinkButton SaveRelation = (LinkButton) e.Item.FindControl("SaveRelation");
					SaveRelation.Text =Root.rm.GetString("CRMopptxt18");

					HtmlContainerControl ModifyCompetitor = (HtmlContainerControl) e.Item.FindControl("ModifyCompetitor");
					ModifyCompetitor.Visible = false;
					break;
			}
		}

		private void FillDropListAZ(DropDownList list, string sqlString, string txt, string txtvalue, string selvalue)
		{
			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			string percentage = String.Empty;
			try
			{
				percentage = ds.Tables[0].Rows[0][2].ToString();
			}
			catch
			{
			}
			list.DataSource = ds;
			list.DataTextField = txt;
			list.DataValueField = txtvalue;
			list.DataBind();
			list.Items.Insert(0,Root.rm.GetString("CRMopptxt26"));
			list.Items[0].Value = "0";
			if (selvalue.Length > 0)
			{
				list.SelectedItem.Selected = false;
				foreach (ListItem li in list.Items)
				{
					if (li.Value == selvalue)
					{
						li.Selected = true;
						break;
					}
				}
			}
			if (percentage.Length > 0)
			{
				list.SelectedItem.Selected = false;
				foreach (ListItem li in list.Items)
				{
					if (li.Text.IndexOf("50") > -1)
					{
						li.Selected = true;
						break;
					}
				}
			}
		}

		public void NewActivity_Click(Object sender, EventArgs e)
		{
			SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=37&gb=1", new string[2] {opId.ToString(), "a"});
			Session["AcOpportunityID"] = opId;
			switch (((LinkButton) sender).ID)
			{
				case "NewActivityPhoneAzOp":
					Session["AType"] = 1;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityLetterAzOp":
					Session["AType"] = 2;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityFax":
				case "NewActivityFaxAzOp":
					Session["AType"] = 3;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityMemoAzOp":
					Session["AType"] = 4;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityEmailAzOp":
					Session["AType"] = 5;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityVisitAzOp":
					Session["AType"] = 6;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityGenericAzOp":
					Session["AType"] = 7;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivitySolutionAzOp":
					Session["AType"] = 8;
					Session["AcCompanyID"] = CompanyNewCompanyID.Text;
					break;
			}
			Page.RegisterStartupScript("redirect", "<script>parent.location.href=\"/WorkingCRM/AllActivity.aspx?m=25&si=38\";</script>");

		}

		public void Tab_Click(string tabId)
		{
			switch (tabId)
			{
				case "Table_Tab1":
					Table_Tab1.Visible = true;
					Table_Tab2.Visible = false;
					Table_Tab3.Visible = false;
					Table_Tab4.Visible = false;
					Table_Tab6.Visible = false;

					Table_Tab1.Selected = true;

					break;
				case "Table_Tab2":
					Table_Tab1.Visible = false;
					Table_Tab2.Visible = true;
					Table_Tab3.Visible = false;
					Table_Tab4.Visible = false;
					Table_Tab6.Visible = false;

					Table_Tab2.Selected = true;

					FillRepeaterReferrer();
					DropDownList Character = (DropDownList) Table_Tab2.FindControl("Character");
					FillDropListAZ(Character, "SELECT DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=4 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "description", "");
					break;
				case "Table_Tab3":
					Table_Tab1.Visible = false;
					Table_Tab2.Visible = false;
					Table_Tab3.Visible = true;
					Table_Tab4.Visible = false;

					Table_Tab6.Visible = false;

					Table_Tab3.Selected = true;

					FillRepeaterContactCompetitor();
					break;
				case "Table_Tab4":
					Table_Tab1.Visible = false;
					Table_Tab2.Visible = false;
					Table_Tab3.Visible = false;
					Table_Tab4.Visible = true;

					Table_Tab6.Visible = false;

					Table_Tab4.Selected = true;

					FillRepeaterActivityAzOp();
					break;
				case "Table_Tab6":
					Table_Tab1.Visible = false;
					Table_Tab2.Visible = false;
					Table_Tab3.Visible = false;
					Table_Tab4.Visible = false;

					Table_Tab6.Visible = true;

					Table_Tab6.Selected = true;

					FillView(int.Parse(CompanyNewCompanyID.Text));
					break;
			}
		}

		private void FillRepeaterContactCompetitor()
		{
			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM COMPETITORCROSSOPPORTUNITY_VIEW WHERE OPPORTUNITYID=" + opId + " AND CONTACTID=" + CompanyNewCompanyID.Text);
			if(ds.Tables[0].Rows.Count>0)
			{
				CompanyCrossCompetitor.DataSource = ds;
				CompanyCrossCompetitor.DataBind();
				CompanyCrossCompetitor.Visible=true;
				CompanyCrossCompetitorInfo.Visible = false;
			}
			else
			{
				CompanyCrossCompetitor.Visible=false;
				CompanyCrossCompetitorInfo.Visible = true;
				CompanyCrossCompetitorInfo.Text =Root.rm.GetString("CRMopptxt50");
			}
		}

		private void FillRepeaterReferrer()
		{
			StringBuilder sqlString = new StringBuilder();
			sqlString.AppendFormat("SELECT * FROM CRM_CROSSOPPREF_VIEW WHERE COMPANYID={0} AND OPPORTUNITYID={1}", (CompanyNewCompanyID.Text.Length > 0) ? int.Parse(CompanyNewCompanyID.Text).ToString() : Session["NewCompanyID"].ToString(), opId);
			Trace.Warn("SQL", sqlString.ToString());
			RepeaterReferrer.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
			RepeaterReferrer.DataBind();
			if (RepeaterReferrer.Items.Count > 0)
			{
				RepeaterReferrer.Visible = true;
				RepeaterReferrerInfo.Visible = false;
			}
			else
			{
				RepeaterReferrer.Visible = false;
				RepeaterReferrerInfo.Visible = true;
				RepeaterReferrerInfo.Text =Root.rm.GetString("CRMopptxt60");
			}
		}

		private void InsertNewAZ(int id)
		{
			int intId = id;
			StringBuilder stringBuilder = new StringBuilder();
			if (intId == -1)
				stringBuilder.AppendFormat("SELECT ID FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTID = {0} AND OPPORTUNITYID = {1};", CompanyNewCompanyID.Text, opId);
			else
				stringBuilder.AppendFormat("SELECT ID FROM CRM_OPPORTUNITYCONTACT WHERE ID={0};", intId);

			using (DigiDapter dg = new DigiDapter(stringBuilder.ToString()))
			{
				DataTable tempDt = DatabaseConnection.CreateDataset(stringBuilder.ToString()).Tables[0];
				int tempId = (tempDt.Rows.Count>0)?Convert.ToInt32(tempDt.Rows[0][0]):intId;

				if (id != -1 || (!dg.HasRows && id == -1))
				{
					dg.Add("OPPORTUNITYID", opId);
					dg.Add("CONTACTID", CompanyNewCompanyID.Text);
					try
					{
						dg.Add("EXPECTEDREVENUE", StaticFunctions.FixDecimal(CompanyNewExpectedRevenue.Text)*(1/changeToEuro));
					}
					catch
					{
						dg.Add("EXPECTEDREVENUE", 0);
					}
					try
					{
						dg.Add("AMOUNTCLOSED", StaticFunctions.FixDecimal(CompanyNewAmountClosed.Text)*(1/changeToEuro));
					}
					catch
					{
						dg.Add("AMOUNTCLOSED", 0);
					}

					if (CompanyNewProbList.SelectedItem.Value != null && CompanyNewProbList.SelectedItem.Value != "0")
					{
						int percentage = Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE ID=" + int.Parse(CompanyNewProbList.SelectedItem.Value)));

						dg.Add("INCOMEPROBABILITY", (StaticFunctions.FixDecimal(CompanyNewExpectedRevenue.Text)*percentage/100)*(1/changeToEuro));
					}
					else
					{
						dg.Add("INCOMEPROBABILITY", 0);
					}

					dg.Add("NOTE", CompanyNewNote.Text);
					dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(CompanyNewStartDate.Text)));
					dg.Add("ESTIMATEDCLOSEDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(CompanyNewEstimatedCloseDate.Text)));

					if (StaticFunctions.IsDate(CompanyNewCloseDate.Text))
						dg.Add("ENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(CompanyNewCloseDate.Text)));
					else
						dg.Add("ENDDATE", DBNull.Value);

					if (this.CompanyLostReasons.SelectedIndex > 0)
					{
						if (this.CompanyLostReasons.SelectedValue == "A99")
						{
							using (DigiDapter dgp = new DigiDapter())
							{
								dgp.Add("Description", this.NewLostReason.Text);

								object newId = dgp.Execute("CRM_OppLostReasons", "ID=-1", DigiDapter.Identities.Identity);
								dg.Add("LOSTREASON", newId.ToString());
							}
						}
						else
							dg.Add("LOSTREASON", Convert.ToInt64(CompanyLostReasons.SelectedValue));
					}

					dg.Add("SALESPERSON", CompanyNewSalesPersonID.Text);

					if (id == -1) dg.Add("CREATEDBYID", UC.UserId);
					if (id == -1) dg.Add("CREATEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
					dg.Add("LASTMODIFIEDBYID", UC.UserId);
					dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
					dg.Execute("CRM_OPPORTUNITYCONTACT","ID="+tempId);

					int compId = int.Parse(CompanyNewCompanyID.Text);
					if (CompanyNewStateList.SelectedItem.Value != null && CompanyNewStateList.SelectedItem.Value != "0")
						ModifyCrossOpportunity(compId, opId, int.Parse(CompanyNewStateList.SelectedItem.Value), "1");
					else
						ModifyCrossOpportunity(compId, opId, 0, "1");

					if (CompanyNewPhaseList.SelectedItem.Value != null && CompanyNewPhaseList.SelectedItem.Value != "0")
						ModifyCrossOpportunity(compId, opId, int.Parse(CompanyNewPhaseList.SelectedItem.Value), "2");
					else
						ModifyCrossOpportunity(compId, opId, 0, "2");

					if (CompanyNewProbList.SelectedItem.Value != null && CompanyNewProbList.SelectedItem.Value != "0")
						ModifyCrossOpportunity(compId, opId, int.Parse(CompanyNewProbList.SelectedItem.Value), "3");
					else
						ModifyCrossOpportunity(compId, opId, 0, "3");

					if (id == -1)
					{
						DataTable dtCross = DatabaseConnection.CreateDataset("SELECT COMPETITORID FROM CRM_OPPORTUNITYCOMPETITOR WHERE OPPORTUNITYID=" + opId).Tables[0];
						foreach (DataRow dr in dtCross.Rows)
						{
							if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_CROSSCONTACTCOMPETITOR WHERE CONTACTTYPE=0 AND COMPETITORID=" + dr[0].ToString() + " AND CONTACTID=" + int.Parse(CompanyNewCompanyID.Text))) == 0)
							{
								DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSCONTACTCOMPETITOR (COMPETITORID,CONTACTID,CONTACTTYPE) VALUES (" + dr[0].ToString() + "," + CompanyNewCompanyID.Text + ",0)");
							}
						}
					}

					SaveProductOpportunity(opId, compId, 0);
				Page.RegisterStartupScript("refresh", "<script>clickElement(parent.document.getElementById(\"RefreshRepeaterCompany\"));</script>");

				}
				else
				{
					CompanyNewInfo.Text =Root.rm.GetString("CRMopptxt31");
				}
			}
		}


		public void RepeaterContactCompetitor_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenComp":
					break;
				case "ViewCompetitor":
					LinkButton OpenCompetitorCrossContact = (LinkButton) e.Item.FindControl("OpenCompetitorCrossContact");
					Label CompanyNameCompetitorCrossContact = (Label) Table_Tab3.FindControl("CompanyNameCompetitorCrossContact");
					Label IDCompetitorCrossContact = (Label) e.Item.FindControl("IDCompetitorCrossContact");
					Label CompanyIDCompetitorCrossContact = (Label) Table_Tab3.FindControl("CompanyIDCompetitorCrossContact");
					CompanyIDCompetitorCrossContact.Text = IDCompetitorCrossContact.Text;
					Label RelationCompetitorCrossContact = (Label) e.Item.FindControl("RelationCompetitorCrossContact");
					CompanyNameCompetitorCrossContact.Text = OpenCompetitorCrossContact.Text;
					DropDownList TypeCompetitorCrossContact = (DropDownList) Table_Tab3.FindControl("TypeCompetitorCrossContact");

					TypeCompetitorCrossContact.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=5");
					TypeCompetitorCrossContact.DataTextField = "Description";
					TypeCompetitorCrossContact.DataValueField = "ID";
					TypeCompetitorCrossContact.DataBind();

					TypeCompetitorCrossContact.SelectedIndex = -1;
					foreach (ListItem im in TypeCompetitorCrossContact.Items)
					{
						if (im.Value == RelationCompetitorCrossContact.Text)
						{
							im.Selected = true;
							break;
						}
					}
					if (TypeCompetitorCrossContact.SelectedIndex == -1) TypeCompetitorCrossContact.SelectedIndex = 0;
					break;
			}
		}

		public void RepeaterContactCompetitorDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label CompetitorStars = (Label) e.Item.FindControl("CompetitorStars");
					byte iStars = Convert.ToByte(DataBinder.Eval((DataRowView) e.Item.DataItem, "Evaluation"));
					Repeater rp = (Repeater) e.Item.FindControl("RepeaterContactCompetitorProduct");
					if (rp.Items.Count <= 0)
					{
						Label ContactCompetitorProductInfo = (Label) e.Item.FindControl("ContactCompetitorProductInfo");
						ContactCompetitorProductInfo.Text =Root.rm.GetString("CRMopptxt54");
						ContactCompetitorProductInfo.Visible = true;
						rp.Visible = false;
					}
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


		protected void FillRepeaterActivityAzOp()
		{
			AcCronoAzOp.ParentID = int.Parse(CompanyNewCompanyID.Text);
			AcCronoAzOp.OpportunityID = opId;
			AcCronoAzOp.AcType = (byte) AType.Company;

			AcCronoAzOp.FromSheet = "o";
			AcCronoAzOp.FromFrame = true;
			AcCronoAzOp.Refresh();
			Trace.Warn("FillRepeaterActivityAzOp", CompanyNewCompanyID.Text + " - " + opId);
			if (AcCronoAzOp.ItemCount > 0)
			{
				AcCronoAzOp.Visible = true;
				RepeaterActivityAzOpInfo.Visible = false;
			}
			else
			{
				AcCronoAzOp.Visible = true;
				RepeaterActivityAzOpInfo.Visible = true;
				RepeaterActivityAzOpInfo.Text =Root.rm.GetString("CRMopptxt59");
			}
		}

		private void ModifyCrossOpportunity(int conID, int opID, int valueID, string type)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OPPORTUNITYID", opID, 'I');
				dg.Add("CONTACTID", conID, 'I');
				dg.Add("TYPE", type, 'I');

				dg.Add("TABLETYPEID", valueID);
				dg.Execute("CRM_CROSSOPPORTUNITY", String.Format("CONTACTID = {0} AND OPPORTUNITYID = {1} AND TYPE= {2};", conID, opID, type));
			}


		}

		public void btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "CompanyNewSubmit":
					if (Page.IsValid)
						InsertNewAZ(int.Parse(CompanyNewID.Text));
					break;
			}
		}

		private void PrepareForNewCompany()
		{
			AcCronoAzOp.ParentID = 0;
			AcCronoAzOp.OpportunityID = 0;
			AcCronoAzOp.AcType = 0;
			AcCronoAzOp.FromFrame = true;

			CompanyNewID.Text = "-1";
			FillDropListAZ(CompanyNewStateList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=1 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", "");
			FillDropListAZ(CompanyNewPhaseList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=2 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", "");
			FillDropListAZ(CompanyNewProbList, "SELECT K_ID,DESCRIPTION,PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=3 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", "");
			CompanyNewNote.Text = String.Empty;
			CompanyNewCompany.Text = String.Empty;
			CompanyNewCompanyID.Text = String.Empty;
			CompanyNewSalesPersonID.Text = UC.UserId.ToString();
			CompanyNewSalesPerson.Text = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + UC.UserId);
			CompanyNewStartDate.Text = DateTime.Now.ToShortDateString();
			CompanyNewEstimatedCloseDate.Text = DateTime.Now.AddDays(Convert.ToDouble(DatabaseConnection.SqlScalar("SELECT TOP 1 ESTIMATEDDATEDAYS FROM TUSTENA_DATA"))).ToShortDateString();
			CompanyNewCloseDate.Text = String.Empty;


			Table_Tab1.LangHeader =Root.rm.GetString("CRMopptxt56");
			Table_Tab1.Selected = false;
			Table_Tab1.Visible = true;
			Table_Tab2.Visible = false;
			Table_Tab3.Visible = false;
			Table_Tab4.Visible = false;
			Table_Tab6.Visible = false;

			CompanyNewSubmit.Visible = true;
			this.SheetP.Visible = false;
		}

		private void SheetP_NextClick(object sender, EventArgs e)
		{
			coId = this.SheetP.GetCurrentID;
			coCrossId = int.Parse(this.SheetP.IDArray[this.SheetP.CurrentPosition].Split('|')[2]);
			ViewState["CoID"] = coId;
			ViewState["CoCrossID"] = coCrossId;
			this.LoadData();
		}

		private void SheetP_PrevClick(object sender, EventArgs e)
		{
			coId = this.SheetP.GetCurrentID;
			coCrossId = int.Parse(this.SheetP.IDArray[this.SheetP.CurrentPosition].Split('|')[2]);
			ViewState["CoID"] = coId;
			this.LoadData();
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.SheetP.NextClick += new EventHandler(SheetP_NextClick);
			this.SheetP.PrevClick += new EventHandler(SheetP_PrevClick);
			this.Load += new EventHandler(this.Page_Load);
			this.BtnAddProduct.Click += new EventHandler(btnAddProduct_Click);
			this.BtnCalcPrice.Click += new EventHandler(btnCalcPrice_Click);
			this.RepeaterEstProduct.ItemCommand += new RepeaterCommandEventHandler(RepeaterEstProduct_ItemCommand);
			this.RepeaterEstProduct.ItemDataBound += new RepeaterItemEventHandler(RepeaterEstProduct_ItemDataBound);
			this.CompanyNewSubmit.Click += new EventHandler(this.btn_Click);
			this.SaveNewReferrer.Click += new EventHandler(this.btn_Click);
			this.NewActivityPhoneCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityLetterCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityFaxCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityMemoCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityEmailCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityVisitCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityGenericCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivitySolutionCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.RepeaterReferrer.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterReferrerCommand);
			this.CompanyCrossCompetitor.ItemCommand += new RepeaterCommandEventHandler(this.CompanyCrossCompetitorCommand);
			this.ViewContact.ItemDataBound += new RepeaterItemEventHandler(this.ViewContact_OnItemDataBound);
			this.RepeaterReferrer.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterReferrerDatabound);
			this.CompanyCrossCompetitor.ItemDataBound += new RepeaterItemEventHandler(this.CompanyCrossCompetitorDataBound);
			this.Tabber.TabClick +=new TabClickDelegate(Tab_Click);

		}

		#endregion

		private void btnCalcPrice_Click(object sender, EventArgs e)
		{
			TextBox EstPl = (TextBox) Page.FindControl("EstPl");
			TextBox EstPf = (TextBox) Page.FindControl("EstPf");
			TextBox EstQta = (TextBox) Page.FindControl("EstQta");
			TextBox EstUp = (TextBox) Page.FindControl("EstUp");
			EstPl.Text = Convert.ToString(Math.Round((StaticFunctions.FixDecimal(EstQta.Text)*StaticFunctions.FixDecimal(EstUp.Text)), 2));
			EstPf.Text = EstPl.Text;
		}

		private void btnAddProduct_Click(object sender, EventArgs e)
		{
			PurchaseProduct newprod = new PurchaseProduct();
			newprod.id = Convert.ToInt64(((TextBox) Page.FindControl("EstProductID")).Text);
			newprod.ShortDescription = ((TextBox) Page.FindControl("EstProduct")).Text;
			newprod.LongDescription = ((TextBox) Page.FindControl("EstDescription2")).Text;
			newprod.UM = ((TextBox) Page.FindControl("EstUm")).Text;
			newprod.Qta = Convert.ToDouble(StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstQta")).Text));

			decimal chFrom = 1;
			newprod.UnitPrice = Math.Round(StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstUp")).Text)*chFrom, 2);
			newprod.Vat = (((TextBox) Page.FindControl("EstVat")).Text.Length > 0) ? StaticFunctions.FixDecimal(((TextBox) Page.FindControl("EstVat")).Text) : 0;
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
					Label FinalPrice = (Label) e.Item.FindControl("FinalPrice");
					Literal ObjectID = (Literal) e.Item.FindControl("ObjectID");
					PurchaseProduct newprod = (PurchaseProduct) e.Item.DataItem;
					ShortDescription.Text = newprod.ShortDescription;
					UM.Text = newprod.UM.ToString();
					Qta.Text = newprod.Qta.ToString();

					string sy = curSymbol;
					decimal chTo = changeToEuro;

					UnitPrice.Text = sy + " " + Math.Round((newprod.UnitPrice*chTo), 2).ToString();
					FinalPrice.Text = sy + " " + Math.Round((newprod.FinalPrice*chTo), 2).ToString();
					ObjectID.Text = newprod.ObId.ToString();
					subTotal += Math.Round((newprod.FinalPrice*chTo), 2);
					break;
				case ListItemType.Footer:
					CompanyNewExpectedRevenue.Text = subTotal.ToString();
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
						PurchaseProduct PP = (PurchaseProduct) np[i];
						PP.ObId = i;
						np[i] = PP;
					}
					Session["newprod"] = np;
					RepeaterEstProduct.DataSource = np;
					RepeaterEstProduct.DataBind();
					RepeaterEstProduct.Visible = true;
					Session["prodchange"] = "true";
					break;
			}
		}

		private void SaveProductOpportunity(int opId, int cid, byte type)
		{
			try
			{
				ArrayList pp = (ArrayList) Session["newprod"];
				if (pp.Count > 0)
				{
					DatabaseConnection.DoCommand("DELETE FROM CRM_OPPPRODUCTROWS WHERE TYPE=" + type + " AND LEADORCOMPANYID=" + cid + " AND OPPORTUNITYID=" + opId);

					decimal expectedRevenue = 0;
					foreach (PurchaseProduct Pprod in pp)
					{
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("OPPORTUNITYID", opId);
							dg.Add("LEADORCOMPANYID", cid);
							dg.Add("TYPE", type);
							dg.Add("DESCRIPTION", Pprod.LongDescription);
							dg.Add("UPRICE", Pprod.UnitPrice);
							dg.Add("NEWUPRICE", Pprod.FinalPrice);
							dg.Add("CATALOGID", Pprod.id);
							dg.Add("QTA", Pprod.Qta);
							expectedRevenue += Pprod.FinalPrice;
							dg.Execute("CRM_OPPPRODUCTROWS", "ID = -1");
						}
					}

					Session.Remove("newprod");

					string sqlString = "SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=" + type + " AND CONTACTID=" + cid + " AND OPPORTUNITYID=" + opId;

					DataSet dsContact = new DataSet();
					dsContact = DatabaseConnection.CreateDataset(sqlString);

					foreach (DataRow dc in dsContact.Tables[0].Rows)
					{
						using(DigiDapter dg = new DigiDapter())
						{
							dg.Add("EXPECTEDREVENUE",expectedRevenue);

							string percId = DatabaseConnection.SqlScalar("SELECT TABLETYPEID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTID = " + dc["contactid"].ToString() + " AND OPPORTUNITYID = " + opId + " AND CONTACTTYPE=" + dc["ContactType"].ToString() + " AND TYPE= 3;");
						if (percId.Length > 0)
						{
							decimal percentage = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE ID=" + percId));
							dg.Add("INCOMEPROBABILITY",(expectedRevenue*percentage/100));
						}
						else
						{
							dg.Add("INCOMEPROBABILITY",0);
						}
						dg.Execute("CRM_OPPORTUNITYCONTACT","ID="+dc["id"].ToString());
						}
					}
				}
			}
			catch
			{
			}
		}

		private void FillRepeaterProducts(int opId, int cId)
		{
			DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM CRM_OPPPRODUCTROWS WHERE TYPE=0 AND LEADORCOMPANYID=" + cId + " AND OPPORTUNITYID=" + opId).Tables[0];

			if (dt.Rows.Count > 0)
			{
				ArrayList np = new ArrayList();
				int obId = 0;
				foreach (DataRow ddr in dt.Rows)
				{
					DataRow pInfo = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRODUCTS WHERE ID=" + ddr["CatalogID"].ToString()).Tables[0].Rows[0];
					PurchaseProduct newprod = new PurchaseProduct();
					newprod.id = Convert.ToInt64(ddr["CatalogID"]);
					newprod.ShortDescription = pInfo["ShortDescription"].ToString();
					newprod.LongDescription = pInfo["LongDescription"].ToString();
					newprod.UM = pInfo["Unit"].ToString();
					newprod.Qta = (int) ddr["Qta"];
					newprod.UnitPrice = (decimal) ddr["Uprice"];
					newprod.Vat = (pInfo["Vat"] != DBNull.Value) ? Convert.ToDecimal(pInfo["Vat"]) : 0;
					newprod.ListPrice = Math.Round(Convert.ToDecimal(newprod.Qta)*newprod.UnitPrice, 2);
					newprod.FinalPrice = (decimal) ddr["NewUPrice"];
					newprod.ObId = obId++;
					np.Add(newprod);
				}

				Session.Remove("newprod");
				Session["newprod"] = np;

				RepeaterEstProduct.DataSource = np;
				RepeaterEstProduct.DataBind();
				RepeaterEstProduct.Visible = true;
			}
		}
	}
}
