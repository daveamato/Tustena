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

using System.Collections;
using System.Resources;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public delegate void SaveLead();
	public delegate void HideLeadControl();
	public partial class CRM_OppLead : System.Web.UI.UserControl
	{
		public event SaveLead Savecomp;
		public event HideLeadControl Hidecontrol;

		protected TustenaTab L_Table_Tab2;

		private decimal changeToEuro = 0;
		private string curSymbol = String.Empty;

		private decimal subTotal = 0;

		private UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

		private int opId
		{
			get
			{
				return int.Parse(ViewState["OpID"].ToString());
			}
		}

		public string OpID
		{
			set
			{
				ViewState["OpID"] = int.Parse(value.ToString());
			}
		}
		private int coId
		{
			get
			{
				return int.Parse(ViewState["CoID"].ToString());
			}
		}

		public string CoID
		{
			set
			{
				ViewState["CoID"] = int.Parse(value.ToString());
			}
		}
		private int coCrossId
		{
			get
			{
				return int.Parse(ViewState["CoCrossID"].ToString());
			}
		}
		public string CoCrossID
		{
			set
			{
				string s = value.ToString();
				int coCrossId = 0;
				if(s.Length>0)
					coCrossId = int.Parse(value.ToString());
				ViewState["CoCrossID"] = coCrossId;
			}
		}

		public void BindData()
		{
			Session.Remove("newprod");

			BtnAddProduct.Text =Root.rm.GetString("CRMopptxt82");
			BtnAddProduct.Attributes.Add("onclick", "return ValidateProduct()");
			BtnCalcPrice.Text =Root.rm.GetString("Esttxt16");

			if(Session["SheetLead"]!=null)
			{
				string[] temparray=(string[])Session["SheetLead"];
				this.SheetP.IDArray=temparray;
				for(int i=0;i<temparray.Length;i++)
				{
					if(temparray[i].Split('|')[0].CompareTo(coId.ToString())==0)
					{
						this.SheetP.CurrentPosition=i;
						this.SheetP.enabledisable();
						break;
					}
				}
				Session.Remove("SheetLead");
			}

			FillRepeaterProducts(opId,coId);

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

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(!this.Visible)
				return;

			G.DeleteGoBack();
			G.AddKeepAlive();

			DataRow dr = DatabaseConnection.CreateDataset("SELECT CHANGETOEURO,CHANGEFROMEURO,CURRENCYSYMBOL FROM CURRENCYTABLE WHERE ID=(SELECT TOP 1 CURRENCY FROM CRM_OPPORTUNITY WHERE ID=" + opId +")").Tables[0].Rows[0];
			changeToEuro = Convert.ToDecimal(dr[0]);
			curSymbol = dr[2].ToString();


		}

		protected string FixCarriage(string txt, bool js)
		{
			return Core.StaticFunctions.FixCarriage(txt,js);
		}

		protected ResourceManager wrm = Core.Root.rm;

		private void LoadData()
		{
			AcCronoAzOp.ParentID = coId;
			AcCronoAzOp.FromFrame = true;
			AcCronoAzOp.OpportunityID = opId;
			AcCronoAzOp.AcType = (byte) AType.Company;
			AcCronoAzOp.Refresh();


			NewActivityPhoneCoOp.Text =Root.rm.GetString("Wortxt6");
			NewActivityLetterCoOp.Text =Root.rm.GetString("Wortxt7");
			NewActivityFaxCoOp.Text =Root.rm.GetString("Wortxt8");
			NewActivityMemoCoOp.Text =Root.rm.GetString("Wortxt9");
			NewActivityEmailCoOp.Text =Root.rm.GetString("Wortxt10");
			NewActivityVisitCoOp.Text =Root.rm.GetString("Wortxt11");
			NewActivityGenericCoOp.Text =Root.rm.GetString("Wortxt12");
			NewActivitySolutionCoOp.Text =Root.rm.GetString("Wortxt13");


			string queryState = "SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION,CRM_CROSSOPPORTUNITY.TYPE,CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=" + coId + " AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID=" + opId + " AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=1";
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

			FillDropListAZ(CompanyNewStateList, "sELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=1 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azStateListSelect.Length > 0) ? azStateListSelect : "");
			FillDropListAZ(CompanyNewPhaseList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=2 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azPhaseListSelect.Length > 0) ? azPhaseListSelect : "");
			FillDropListAZ(CompanyNewProbList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=3 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azProbListSelect.Length > 0) ? azProbListSelect : "");

			DataSet tempDS = DatabaseConnection.CreateDataset("SELECT NOTE,EXPECTEDREVENUE,AMOUNTCLOSED,INCOMEPROBABILITY,SALESPERSON,STARTDATE,ESTIMATEDCLOSEDATE,ENDDATE,LOSTREASON FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=1 AND OPPORTUNITYID=" + opId + " AND CONTACTID=" + coId);

			if(tempDS.Tables[0].Rows[0]["LostReason"]!=DBNull.Value)
			{
				this.CompanyLostReasons.SelectedIndex=-1;
				foreach(ListItem li in this.CompanyLostReasons.Items)
				{
					if(li.Value==tempDS.Tables[0].Rows[0]["LostReason"].ToString())
					{
						li.Selected=true;
						break;
					}
				}
			}

			CompanyNewNote.Text = (string) tempDS.Tables[0].Rows[0]["Note"];
			CompanyNewCompany.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')+'-'+ISNULL(COMPANYNAME,'')) FROM CRM_LEADS WHERE ID=" + coId);

			CompanyNewExpectedRevenue.Text = Math.Round((Convert.ToDecimal(tempDS.Tables[0].Rows[0]["ExpectedRevenue"])*changeToEuro), 2).ToString();
			CompanyNewAmountClosed.Text = Math.Round((Convert.ToDecimal(tempDS.Tables[0].Rows[0]["AmountClosed"])*changeToEuro), 2).ToString();
			CompanyNewAmountRevenuePercent.Text = Math.Round((Convert.ToDecimal(tempDS.Tables[0].Rows[0]["IncomeProbability"])*changeToEuro), 2).ToString();

			CompanyNewSalesPersonID.Text = tempDS.Tables[0].Rows[0]["SalesPerson"].ToString();
			CompanyNewSalesPerson.Text = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + tempDS.Tables[0].Rows[0]["SalesPerson"].ToString());
			CompanyNewStartDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(tempDS.Tables[0].Rows[0]["StartDate"].ToString())).ToShortDateString();
			CompanyNewEstimatedCloseDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(tempDS.Tables[0].Rows[0]["EstimatedCloseDate"].ToString())).ToShortDateString();
			if (tempDS.Tables[0].Rows[0]["EndDate"].ToString().Length > 0)
				CompanyNewCloseDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(tempDS.Tables[0].Rows[0]["EndDate"].ToString())).ToShortDateString();
			else
				CompanyNewCloseDate.Text = String.Empty;

			L_Tabber.Selected = L_Table_Tab1.ID;
			CompanyNewCompanyID.Text = coId.ToString();
			CompanyNewID.Text = coCrossId.ToString();
			Session["NewCompanyID"] = CompanyNewCompanyID.Text;


			this.SheetP.Visible=true;
		}

		private void FillLostReason()
		{
			CompanyLostReasons.Items.Add(new ListItem(Root.rm.GetString("Mottxt7"),"0"));
			DataTable dt;
 dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_OPPLOSTREASONS").Tables[0];
			foreach(DataRow dr in dt.Rows)
			{
				string itemText = (dr[1].ToString().IndexOf("Mot")>-1)?Root.rm.GetString(dr[1].ToString()):dr[1].ToString();
				CompanyLostReasons.Items.Add(new ListItem(itemText,dr[0].ToString()));
			}
			CompanyLostReasons.Items.Add(new ListItem(Root.rm.GetString("Other"),"A99"));
			CompanyLostReasons.Attributes.Add("onchange","SelectOther()");
		}


		private void FillVisualizza(int id)
		{
			Session["CurrentRefId"] = id;
			string sqlString = "SELECT CRM_LEADS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2, BASE_COMPANIES.PHONE AS AZPHONE ";
			sqlString += "FROM CRM_LEADS LEFT OUTER JOIN BASE_COMPANIES ON CRM_LEADS.COMPANYID = BASE_COMPANIES.ID ";
			ViewForm.DataSource = DatabaseConnection.CreateDataset(sqlString + "WHERE CRM_LEADS.ID=" + id);
			ViewForm.DataBind();
			ViewForm.Visible = true;
		}

		public void ViewFormOnItemDatabound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Categories")).Length > 0)
					{
						string[] cats = ((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "Categories")).Split('|');
						string queryCat = String.Empty;
						foreach (string ca in cats)
						{
							if (ca.Length > 0) queryCat += " ID=" + int.Parse(ca) + " OR ";
						}
						queryCat = queryCat.Substring(0, queryCat.Length - 4);
						Repeater RepCategoriesView = (Repeater) e.Item.FindControl("RepCategoriesView");
						RepCategoriesView.DataSource = DatabaseConnection.CreateDataset("SELECT DESCRIPTION FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) AND (" + queryCat + ")");
						RepCategoriesView.DataBind();
					}



					break;
			}
		}

		public DataView getLeadInfo(int id)
		{
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT CRM_CROSSLEAD.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, ");
			sqlString.Append("BASE_CONTACTS.SURNAME + ' ' + BASE_CONTACTS.NAME AS CONTACTNAME, ");
			sqlString.Append("CRM_OPPORTUNITY.TITLE AS OPPORTUNITYNAME, ");
			sqlString.Append("ACCOUNT_1.SURNAME + ' ' + ACCOUNT_1.NAME AS OWNERNAME, ");
			sqlString.Append("ACCOUNT_2.SURNAME + ' ' + ACCOUNT_2.NAME AS SALESPERSONNAME, ");
			sqlString.Append("COMPANYTYPE.DESCRIPTION AS INDUSTRYNAME, ");
			sqlString.Append("CRM_LEADDESCRIPTION1.DESCRIPTION AS STATUSDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION2.DESCRIPTION AS RATINGDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION3.DESCRIPTION AS PRODUCTINTERESTDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION4.DESCRIPTION AS SOURCEDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION5.DESCRIPTION AS LEADCURRENCYDESCRIPTION ");
			sqlString.Append("FROM CRM_CROSSLEAD ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_2 ON CRM_CROSSLEAD.SALESPERSON = ACCOUNT_2.UID ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_1 ON CRM_CROSSLEAD.LEADOWNER = ACCOUNT_1.UID ");
			sqlString.Append("LEFT OUTER JOIN CRM_OPPORTUNITY ON CRM_CROSSLEAD.ASSOCIATEDOPPORTUNITY = CRM_OPPORTUNITY.ID ");
			sqlString.Append("LEFT OUTER JOIN BASE_CONTACTS ON CRM_CROSSLEAD.ASSOCIATEDCONTACT = BASE_CONTACTS.ID ");
			sqlString.Append("LEFT OUTER JOIN BASE_COMPANIES ON CRM_CROSSLEAD.ASSOCIATEDCOMPANY = BASE_COMPANIES.ID ");

			sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION1 ON CRM_CROSSLEAD.STATUS = CRM_LEADDESCRIPTION1.K_ID AND CRM_LEADDESCRIPTION1.LANG='" + UC.Culture.Substring(0, 2) + "' ");
			sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION2 ON CRM_CROSSLEAD.RATING = CRM_LEADDESCRIPTION2.K_ID AND CRM_LEADDESCRIPTION2.LANG='" + UC.Culture.Substring(0, 2) + "' ");
			sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION3 ON CRM_CROSSLEAD.PRODUCTINTEREST = CRM_LEADDESCRIPTION3.K_ID AND CRM_LEADDESCRIPTION3.LANG='" + UC.Culture.Substring(0, 2) + "' ");
			sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION4 ON CRM_CROSSLEAD.SOURCE = CRM_LEADDESCRIPTION4.K_ID AND CRM_LEADDESCRIPTION4.LANG='" + UC.Culture.Substring(0, 2) + "' ");
			sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION5 ON CRM_CROSSLEAD.LEADCURRENCY = CRM_LEADDESCRIPTION5.K_ID AND CRM_LEADDESCRIPTION5.LANG='" + UC.Culture.Substring(0, 2) + "' ");

			sqlString.Append("LEFT OUTER JOIN COMPANYTYPE ON CRM_CROSSLEAD.INDUSTRY = COMPANYTYPE.K_ID AND COMPANYTYPE.LANG='" + UC.Culture.Substring(0, 2) + "' ");

			return DatabaseConnection.CreateDataset(sqlString.ToString() + "WHERE CRM_CROSSLEAD.LEADID=" + id).Tables[0].DefaultView;
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
					DatabaseConnection.DoCommand(String.Format("UPDATE CRM_CROSSCONTACTCOMPETITOR SET RELATION='{1}' WHERE ID={1}",int.Parse(((Literal) e.Item.FindControl("IDcross")).Text),DatabaseConnection.FilterInjection(((TextBox) e.Item.FindControl("TbxRelation")).Text)));

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
		}

		public void NewActivity_Click(Object sender, EventArgs e)
		{
			G.SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=37&gb=1",new string[]{opId.ToString(),"al",this.coCrossId.ToString(),this.coId.ToString()});
			Session["AcOpportunityID"] = opId;
			switch (((LinkButton) sender).ID)
			{
				case "NewActivityPhoneCoOp":
					Session["AType"] = 1;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityLetterCoOp":
					Session["AType"] = 2;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityFax":
				case "NewActivityFaxCoOp":
					Session["AType"] = 3;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityMemoCoOp":
					Session["AType"] = 4;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityEmailCoOp":
					Session["AType"] = 5;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityVisitCoOp":
					Session["AType"] = 6;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivityGenericCoOp":
					Session["AType"] = 7;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
				case "NewActivitySolutionCoOp":
					Session["AType"] = 8;
					Session["AcLeadID"] = CompanyNewCompanyID.Text;
					break;
			}
			Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>parent.location.href=\"/WorkingCRM/AllActivity.aspx?m=25&si=38\";</script>");
		}

		public void Tab_Click(string tabId)
		{
			switch (tabId)
			{
				case "L_Table_Tab1":
					L_Tabber.Selected = L_Table_Tab1.ID;

					break;
				case "L_Table_Tab3":
					L_Tabber.Selected = L_Table_Tab3.ID;

					FillRepeaterContactCompetitor();
					break;
				case "L_Table_Tab4":
					L_Tabber.Selected = L_Table_Tab4.ID;

					FillRepeaterActivityAzOp();
					break;
				case "L_Table_Tab6":
					L_Tabber.Selected = L_Table_Tab6.ID;

					FillVisualizza(int.Parse(CompanyNewCompanyID.Text));
					break;
			}
		}

		private void FillRepeaterContactCompetitor()
		{
			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM COMPETITORCROSSOPPORTUNITY_VIEW WHERE CONTACTTYPE=1 AND OPPORTUNITYID=" + opId + " AND CONTACTID=" + int.Parse(CompanyNewCompanyID.Text));
			CompanyCrossCompetitor.DataSource = ds;
			CompanyCrossCompetitor.DataBind();
		}

		private void InsertNewAZ(int id)
		{
			StringBuilder sqlString = new StringBuilder();
			if (id == -1)
				sqlString.AppendFormat("SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTID = {0} AND OPPORTUNITYID = {1} AND CONTACTTYPE=1;", CompanyNewCompanyID.Text, opId);
			else
				sqlString.AppendFormat("SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE ID={0};", id);
			using (DigiDapter dg = new DigiDapter(sqlString.ToString()))
			{
				DataTable tempDt = DatabaseConnection.CreateDataset(sqlString.ToString()).Tables[0];
				int tempId = (tempDt.Rows.Count>0)?Convert.ToInt32(tempDt.Rows[0][0]):id;

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
					dg.Add("CONTACTTYPE", 1);

					if (CompanyNewProbList.SelectedItem.Value != null && CompanyNewProbList.SelectedItem.Value != "0")
					{
						int percentage = Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE ID=" + CompanyNewProbList.SelectedItem.Value));

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

					if(this.CompanyLostReasons.SelectedIndex>0)
					{
						if(this.CompanyLostReasons.SelectedValue=="A99")
						{
							using(DigiDapter dgp = new DigiDapter())
							{
								dgp.Add("Description",this.NewLostReason.Text);

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
							if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_CROSSCONTACTCOMPETITOR WHERE CONTACTTYPE=1 AND COMPETITORID=" + dr[0].ToString() + " AND CONTACTID=" + int.Parse(CompanyNewCompanyID.Text))) == 0)
							{
							DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSCONTACTCOMPETITOR (COMPETITORID,CONTACTID,CONTACTTYPE) VALUES (" + dr[0].ToString() + "," + int.Parse(CompanyNewCompanyID.Text) + ",1)");
							}
						}


					}
					string associatedOpp = DatabaseConnection.SqlScalar("SELECT OTHEROPPORTUNIES FROM CRM_CROSSLEAD WHERE LEADID="+compId);
					if(associatedOpp.Length>0)
					{
						if(associatedOpp.IndexOf("|"+opId+"|")<0)
							DatabaseConnection.DoCommand("UPDATE CRM_CROSSLEAD SET OTHEROPPORTUNIES=OTHEROPPORTUNIES+'|"+opId+"|' WHERE LEADID="+compId);
					}
					else
						DatabaseConnection.DoCommand("UPDATE CRM_CROSSLEAD SET OTHEROPPORTUNIES='|"+opId+"|' WHERE LEADID="+compId);

					SaveProductOpportunity(opId,compId,1);
					this.Visible=false;
					Savecomp();

				}
				else
				{
					CompanyNewInfo.Text =Root.rm.GetString("CRMopptxt74");
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
					Label CompanyNameCompetitorCrossContact = (Label) L_Table_Tab3.FindControl("CompanyNameCompetitorCrossContact");
					Label IDCompetitorCrossContact = (Label) e.Item.FindControl("IDCompetitorCrossContact");
					Label CompanyIDCompetitorCrossContact = (Label) L_Table_Tab3.FindControl("CompanyIDCompetitorCrossContact");
					CompanyIDCompetitorCrossContact.Text = IDCompetitorCrossContact.Text;
					Label RelationCompetitorCrossContact = (Label) e.Item.FindControl("RelationCompetitorCrossContact");
					CompanyNameCompetitorCrossContact.Text = OpenCompetitorCrossContact.Text;
					DropDownList TypeCompetitorCrossContact = (DropDownList) L_Table_Tab3.FindControl("TypeCompetitorCrossContact");

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
			AcCronoAzOp.AcType = (byte) AType.Lead;
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
			string sqlString = String.Format("SELECT ID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTTYPE=1 AND CONTACTID = {0} AND OPPORTUNITYID = {1} AND TYPE= {2};", conID, opID, type);

			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if(!dg.HasRows)
				{
					dg.Add("OPPORTUNITYID", opID,'I');
					dg.Add("CONTACTID", conID,'I');
					dg.Add("TYPE", type,'I');
					dg.Add("CONTACTTYPE", 1,'I');
				}


				dg.Add("TABLETYPEID", valueID);
				if(dg.HasRows)
					dg.Execute("CRM_CROSSOPPORTUNITY","ID="+dg.ExternalReader[0]);
				else
					dg.Execute("CRM_CROSSOPPORTUNITY","ID=-1");

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
			FillDropListAZ(CompanyNewProbList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=3 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", "");
			CompanyNewNote.Text = String.Empty;
			CompanyNewCompany.Text = String.Empty;
			CompanyNewCompanyID.Text = String.Empty;
			CompanyNewSalesPersonID.Text = UC.UserId.ToString();
			CompanyNewSalesPerson.Text = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + UC.UserId);
			CompanyNewStartDate.Text = DateTime.Now.ToShortDateString();
			CompanyNewEstimatedCloseDate.Text = DateTime.Now.AddDays(Convert.ToDouble(DatabaseConnection.SqlScalar("SELECT TOP 1 ESTIMATEDDATEDAYS FROM TUSTENA_DATA"))).ToShortDateString();
			CompanyNewCloseDate.Text = String.Empty;


			L_Tabber.Selected=L_Table_Tab1.ID;

			CompanyNewSubmit.Visible = true;
			this.SheetP.Visible=false;

		}

		private void SheetP_NextClick(object sender, EventArgs e)
		{
			CoID = this.SheetP.GetCurrentID.ToString();
			CoCrossID = this.SheetP.IDArray[this.SheetP.CurrentPosition].Split('|')[2];

			ViewState["CoID"]=coId;
			ViewState["CoCrossID"]=coCrossId;
			this.LoadData();
		}

		private void SheetP_PrevClick(object sender, EventArgs e)
		{
			CoID = this.SheetP.GetCurrentID.ToString();
			CoCrossID = this.SheetP.IDArray[this.SheetP.CurrentPosition].Split('|')[2];
			ViewState["CoID"]=coId;
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
			this.SheetP.NextClick +=new EventHandler(SheetP_NextClick);
			this.SheetP.PrevClick +=new EventHandler(SheetP_PrevClick);
			this.Load += new EventHandler(this.Page_Load);
			this.BtnAddProduct.Click +=new EventHandler(btnAddProduct_Click);
			this.BtnCalcPrice.Click +=new EventHandler(btnCalcPrice_Click);
			this.RepeaterEstProduct.ItemCommand+=new RepeaterCommandEventHandler(RepeaterEstProduct_ItemCommand);
			this.RepeaterEstProduct.ItemDataBound+=new RepeaterItemEventHandler(RepeaterEstProduct_ItemDataBound);

			this.CompanyNewSubmit.Click += new EventHandler(this.btn_Click);
			this.NewActivityPhoneCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityLetterCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityFaxCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityMemoCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityEmailCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityVisitCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityGenericCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivitySolutionCoOp.Click += new EventHandler(this.NewActivity_Click);
			this.CompanyCrossCompetitor.ItemCommand += new RepeaterCommandEventHandler(this.CompanyCrossCompetitorCommand);
			this.ViewForm.ItemDataBound += new RepeaterItemEventHandler(this.ViewFormOnItemDatabound);
			this.CompanyCrossCompetitor.ItemDataBound += new RepeaterItemEventHandler(this.CompanyCrossCompetitorDataBound);

			this.L_Tabber.TabClick +=new TabClickDelegate(Tab_Click);
			this.btnGoBack.Click+=new EventHandler(btnGoBack_Click);
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
			newprod.Qta = Convert.ToInt32(((TextBox) Page.FindControl("EstQta")).Text);

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

					for(int i=0;i<np.Count;i++)
					{
						PurchaseProduct PP = (PurchaseProduct)np[i];
						PP.ObId=i;
						np[i]=PP;
					}
					Session["newprod"] = np;
					RepeaterEstProduct.DataSource = np;
					RepeaterEstProduct.DataBind();
					RepeaterEstProduct.Visible = true;
					Session["prodchange"] = "true";
					break;
			}
		}

		private void SaveProductOpportunity(int opid, int cid, byte type)
		{
			try
			{
				ArrayList pp = (ArrayList) Session["newprod"];
				if(pp.Count>0)
				{
					DatabaseConnection.DoCommand("DELETE FROM CRM_OPPPRODUCTROWS WHERE TYPE=" + type + " AND LEADORCOMPANYID=" + cid + " AND OPPORTUNITYID=" + opid);

					decimal expectedRevenue = 0;
					foreach (PurchaseProduct Pprod in pp)
					{
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("OPPORTUNITYID", opid);
							dg.Add("LEADORCOMPANYID", cid);
							dg.Add("TYPE", type);
							dg.Add("DESCRIPTION", Pprod.LongDescription);
							dg.Add("UPRICE", Pprod.UnitPrice);
							dg.Add("NEWUPRICE", Pprod.FinalPrice);
							dg.Add("CATALOGID", Pprod.id);
							dg.Add("QTA", Pprod.Qta);
							expectedRevenue += Pprod.FinalPrice;
							dg.Execute("CRM_OPPPRODUCTROWS");
						}
					}

					Session.Remove("newprod");

					string sqlString = "SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=" + type + " AND CONTACTID=" + cid + " AND OPPORTUNITYID=" + opid;

					DataSet dsContact = DatabaseConnection.CreateDataset(sqlString);

					foreach (DataRow row in dsContact.Tables[0].Rows)
					{
						using(DigiDapter dg = new DigiDapter())
						{
							dg.Add("EXPECTEDREVENUE", expectedRevenue);

							string percId = DatabaseConnection.SqlScalar("SELECT TABLETYPEID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTID = " + row["contactid"].ToString() + " AND OPPORTUNITYID = " + opid + " AND CONTACTTYPE=" + row["ContactType"].ToString() + " AND TYPE= 3;");
							if (percId.Length > 0)
							{
								decimal percentage = Convert.ToDecimal(DatabaseConnection.SqlScalar("SELECT PERCENTAGE FROM CRM_OPPORTUNITYTABLETYPE WHERE ID=" + int.Parse(percId)));
								dg.Add("INCOMEPROBABILITY",(expectedRevenue*percentage/100));
							}
							else
							{
								dg.Add("INCOMEPROBABILITY", 0);
							}
							dg.Execute("CRM_OPPORTUNITYCONTACT","ID="+row["ID"].ToString());
						}

					}
				}
			}
			catch{}
		}

		private void FillRepeaterProducts(int opid, int cid)
		{
			DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM CRM_OPPPRODUCTROWS WHERE TYPE=1 AND LEADORCOMPANYID="+cid+" AND OPPORTUNITYID=" + opid).Tables[0];

			if(dt.Rows.Count>0)
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

		private void btnGoBack_Click(object sender, EventArgs e)
		{
			Hidecontrol();
		}
	}
}
