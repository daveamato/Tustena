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
using Ajax;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.ERP;
using Digita.Tustena.WebControls;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public partial class CRM_Lead : G
	{
		private string repSqlString;

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;
            if (visQuote.Visible == true && (!M.IsModule(ActiveModules.Sales) || !M.IsModule(ActiveModules.SalesWarehouse)))
                Tabber.HideTabs += visQuote.ID;

            base.OnPreRenderComplete(e);
        }

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				initNewRepeater();
				DeleteGoBack();
				if (Request["Ajax"] != null)
					InitAjax();

				Back.Click += new EventHandler(BtnBack_Click);

				visActivity.Visible = true;
				visQuote.Visible=true;

				DateFormat.Text = UC.myDTFI.ShortDatePattern.ToString();
				if (!isGoBack)
				{
					Back.Visible = false;
				}
				else
				{
					Stack ba = new Stack();
					ba = (Stack) Session["goback1"];
					GoBack gb = new GoBack();
					gb = (GoBack) ba.Peek(); //[ba.Count - 1];
					if (!(gb.sheet.ToLower().IndexOf("crm_leads.aspx") > 0))
						Back.Visible = false;
				}


				RapSubmit.Text =Root.rm.GetString("Bcotxt4");
				BtnGroup.Text =Root.rm.GetString("Bcotxt2");


				try
				{
					repSqlString = ViewState["RepSQLString"].ToString();
				}
				catch
				{
					repSqlString = String.Empty;
				}

                if (Tabber.Expand)
                    AcCrono.NoPaging();

				if (!Page.IsPostBack)
				{
					FilldropZones();
					AcCrono.ParentID = 0;
					AcCrono.OpportunityID = 0;
					AcCrono.AcType = (byte) AType.Lead;
					NewActivityPhone.Text =Root.rm.GetString("Wortxt6");
					NewActivityLetter.Text =Root.rm.GetString("Wortxt7");
					NewActivityFax.Text =Root.rm.GetString("Wortxt8");
					NewActivityMemo.Text =Root.rm.GetString("Wortxt9");
					NewActivityEmail.Text =Root.rm.GetString("Wortxt10");
					NewActivityVisit.Text =Root.rm.GetString("Wortxt11");
					NewActivityGeneric.Text =Root.rm.GetString("Wortxt12");
					NewActivitySolution.Text =Root.rm.GetString("Wortxt13");

					BtnAdvanced.Text =Root.rm.GetString("CRMcontxt52");
					advancedSearch.Visible = false;

					SrcBtn.Text =Root.rm.GetString("Find");
					G.FillListGroups(UC, this.ListGroups);

					FillListCategories();
					referenceForm.Visible = false;
					Tabber.Visible = false;

					if (Request.Params["action"] != null)
					{
						if (Request.Params["action"] == "NEW")
						{
							TextBox txtfield = (TextBox) referenceForm.FindControl("CRM_Leads_ID");
							txtfield.Text = "-1";
							txtfield = (TextBox) referenceForm.FindControl("CRM_Leads_CompanyID");
							if (Session["contact"] != null)
							{
								txtfield.Text = Session["contact"].ToString();
								TextBox txtfieldContact = (TextBox) referenceForm.FindControl("CRM_Leads_CompanyName2");
								txtfieldContact.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + Session["contact"]);

								string groups = DatabaseConnection.SqlScalar("SELECT GROUPS FROM BASE_COMPANIES WHERE ID=" + Session["contact"]);

								Session["GroupsSetGroup"] = groups;
								Session["contact"] = null;
							}

							referenceForm.Visible = true;
							Tabber.Visible = true;
							NewRepeater1.Visible = false;
							ViewForm.Visible = false;
						}
						if (Request.Params["action"] == "MOD")
						{
							ViewForm.Visible = false;
							NewRepeater1.Visible = false;
							referenceForm.Visible = true;
							FillForm(int.Parse(Request.Params["full"]));
						}
						if (Request.Params["action"] == "VIEW")
						{
							FillView(int.Parse(Request.Params["full"]));
						}
					}
					if (Request.Params["gb"] != null && Session["goback1"] != null && isGoBack)
					{
						Stack ba = new Stack();
						ba = (Stack) Session["goback1"];
						GoBack gb = new GoBack();
						gb = (GoBack) ba.Pop(); //[ba.Count - 1];
						string[] par = gb.parameter.Split('|');

						if (par[1].Length > 0) FillView(int.Parse(par[1]));
						if (par[2].Length > 0)
						{
							switch (par[2])
							{
								case "a":
									Tabber.Selected = visActivity.ID;
									break;
							}

						}
						Session["goback1"] = ba;
					}

					if (Request.Form["searchcontact"] != null)
					{
						Search.Text = Request.Form["searchcontact"];
						SearchClick("btnSearch");
					}

                    if (Session["openId"] != null)
                    {
                        FillView(int.Parse(Session["openId"].ToString()));
                        Session.Remove("openId");
                    }

					FillNewRepeater1(false,string.Empty);
				}

			}
			LbnNew.Text =Root.rm.GetString("Ledtxt1");


			BtnSearch.Text =Root.rm.GetString("Reftxt41");

			SubmitRef.Text =Root.rm.GetString("Reftxt43");
			Submit2.Text =Root.rm.GetString("Reftxt43");

		}

		private void FilldropZones()
		{
			dropZones.DataValueField="id";
			dropZones.DataTextField="description";
			dropZones.DataSource=DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM ZONES ORDER BY VIEWORDER");
			dropZones.DataBind();
			dropZones.Items.Insert(0,new ListItem(Root.rm.GetString("Choose"),"0"));
		}

		private void FillListCategories()
		{
			ListCategory.DataTextField = "Description";
			ListCategory.DataValueField = "id";
			ListCategory.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
			ListCategory.DataBind();
			ListCategory.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			ListCategory.Items[0].Value = "0";
		}

		private void ClearTextBox(IEnumerator i)
		{
			while (i.MoveNext())
			{
				if (i.Current.GetType().Name == "TextBox")
				{
					TextBox t = (TextBox) i.Current;
					if (t.ID == "CRM_Leads_ID")
						t.Text = "-1";
					else
						t.Text = String.Empty;
				}
				else
				{
					Control y = (Control) i.Current;
					ClearTextBox(y.Controls.GetEnumerator());
				}
			}
		}

		public void NewLeadFunction()
		{
			Tabber.Visible = true;
			referenceForm.Visible = true;
			NewRepeater1.Visible = false;
			ViewForm.Visible = false;
			ClearTextBox(referenceForm.Controls.GetEnumerator());
			FillCrossLeadDrop();

			Tabber.Selected = visContact.ID;
			Tabber.EditTab="visContact";

			try
			{
				if (UC.InsertGroups.Length > 0) Groups.SetGroups(UC.InsertGroups);
			}
			catch
			{
			}

			this.SheetP.Visible = false;
			this.FillRepCategories(true);
		}

		public void NewLead(object sender, EventArgs e)
		{
			InitAjax();
			NewLeadFunction();
		}

		public void BtnSearch_Click(object sender, EventArgs e)
		{
			SearchClick(((LinkButton) sender).ID);
			DeleteGoBack(true);
		}

		private void SearchClick(string sender)
		{
			Session["searchsender"] = sender;

			string queryType = String.Empty;
			string queryGroup = GroupsSecure("CRM_Leads.Groups");
			string findString = DatabaseConnection.FilterInjection(Search.Text);

			StringBuilder sqlString = new StringBuilder();


			sqlString.Append("SELECT CRM_LEADS.ID,CRM_LEADS.NAME, CRM_LEADS.SURNAME, CRM_LEADS.PHONE, CRM_LEADS.MOBILEPHONE, CRM_LEADS.COMPANYNAME ,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS NAMEOWNER, CRM_LEADS.CREATEDDATE, CRM_CROSSLEAD.SALESPERSON, CRM_LEADS.OWNERID ");
			sqlString.Append("FROM CRM_LEADS LEFT OUTER JOIN BASE_COMPANIES ON CRM_LEADS.COMPANYID = BASE_COMPANIES.ID ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ON CRM_LEADS.OWNERID = ACCOUNT.UID ");
			sqlString.Append("LEFT OUTER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID = CRM_CROSSLEAD.LEADID ");
			sqlString.AppendFormat("WHERE CRM_LEADS.LIMBO=0 AND (CRM_LEADS.SURNAME LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_LEADS.NAME LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_LEADS.PHONE LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_Leads.Email like '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_LEADS.CITY LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_LEADS.PROVINCE LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("CRM_LEADS.COMPANYNAME LIKE '%{0}%') ", findString);
			sqlString.Append(queryType);

			if (sender == "BtnGroup")
			{
				if (ListGroups.SelectedItem.Value != "0")
				{
					string dep = GroupDependency(Convert.ToInt32(ListGroups.SelectedItem.Value));
					if (dep.Length > 1)
					{
						string[] arryD = dep.Split('|');
						string qGroup = String.Empty;
						foreach (string ut in arryD)
						{
							if (ut.Length > 0) qGroup += "CRM_LEADS.GROUPS LIKE '%|" + ut + "|%' OR ";
						}
						if (qGroup.Length > 0) qGroup = qGroup.Substring(0, qGroup.Length - 3);
						sqlString.AppendFormat(" AND ({0})", qGroup);

					}
					else
					{
						sqlString.AppendFormat(" AND (CRM_LEADS.GROUPS LIKE '%|{0}|%')", ListGroups.SelectedItem.Value);

					}
				}
				if (this.ListCategory.SelectedIndex > 0)
				{
					sqlString.AppendFormat(" AND (CRM_LEADS.CATEGORIES LIKE '%|{0}|%')", ListCategory.SelectedValue);

				}

			}
			else
			{
				if (queryGroup.Length > 0)
				{
					sqlString.AppendFormat(" AND (({0}) OR CRM_LEADS.OWNERID={1})", queryGroup, UC.UserId);
				}
			}

			if(UC.Zones.Length>0)
				sqlString.AppendFormat(" AND ({0})", ZoneSecure("CRM_LEADS.COMMERCIALZONE"));

			this.FillNewRepeater1(true,sqlString.ToString());

			ViewForm.Visible = false;
			referenceForm.Visible = false;
			Tabber.Visible = false;
		}

		public void ReferenceFormSubmit(object sender, EventArgs e)
		{
			TextBox txtfield = (TextBox) referenceForm.FindControl("CRM_Leads_ID");
			if (((TextBox) referenceForm.FindControl("CRM_Leads_Name")).Text.Length > 0 || ((TextBox) referenceForm.FindControl("CRM_Leads_Surname")).Text.Length > 0)
			{
				ModifyForm(int.Parse(txtfield.Text));
				Tabber.Selected = visContact.ID;
				SheetP.Visible = true;
			}
			else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" +Root.rm.GetString("Reftxt54") + "');</script>");
				RequiredFieldValidatorCognome.Visible = true;
				Tabber.EditTab = "visContact";
				Tabber.Selected = visContact.ID;
			}
		}



		public void Repeater1_ItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					string salesperson = (DataBinder.Eval((DataRowView) e.Item.DataItem, "SalesPerson")).ToString();
					string ownerid = (DataBinder.Eval((DataRowView) e.Item.DataItem, "OwnerID")).ToString();
					if (salesperson.Length > 0 && salesperson!=UC.UserId.ToString() && ownerid!=UC.UserId.ToString() && UC.AdminGroupId!=UC.UserGroupId)
					{
							e.Item.Visible=false;
					}
					break;
			}
		}


		public void Repeater1_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "View":
					if (this.SheetP.IDArray.Length > 0)
					{
						string[] tempArray = this.SheetP.IDArray;
						for (int i = 0; i < tempArray.Length; i++)
						{
							if (tempArray[i].Split('|')[0].CompareTo(((Literal) e.Item.FindControl("ID")).Text) == 0)
							{
								this.SheetP.CurrentPosition = i;
								this.SheetP.enabledisable();
								break;
							}
						}
					}
					Tabber.Selected = visContact.ID;
					FillView(int.Parse(((Literal) e.Item.FindControl("ID")).Text));
					break;
				case "MultiDeleteButton":
					DeleteChecked.MultiLimbo(this.NewRepeater1.MultiDeleteListArray,"CRM_Leads",UC.UserId);
					this.NewRepeater1.DataBind();
					break;
			}
		}

		private void FillView(int id)
		{
			Session["CurrentRefId"] = id;
			string sqlString = "SELECT CRM_LEADS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2, BASE_COMPANIES.PHONE AS AZPHONE ";
			sqlString += "FROM CRM_LEADS LEFT OUTER JOIN BASE_COMPANIES ON CRM_LEADS.COMPANYID = BASE_COMPANIES.ID ";
			DataSet ds = DatabaseConnection.CreateDataset(sqlString + "WHERE CRM_LEADS.ID=" + id);
            string recentText = ((ds.Tables[0].Rows[0]["COMPANYNAME"].ToString().Length > 0) ? ds.Tables[0].Rows[0]["COMPANYNAME"].ToString() + " " : String.Empty) + ((ds.Tables[0].Rows[0]["NAME"].ToString().Length>0) ? ds.Tables[0].Rows[0]["NAME"].ToString() + " ":string.Empty) + ds.Tables[0].Rows[0]["SURNAME"].ToString();
            Recent.AddItem(UC.UserId, recentText, id, RecentType.Lead, RecentMode.Read);
            ViewForm.DataSource = ds;
			ViewForm.DataBind();

			Tabber.Visible = true;
			ViewForm.Visible = true;
			NewRepeater1.Visible = false;
			referenceForm.Visible = false;

			try
			{
				if (this.SheetP.IDArray.Length <= 0)
				{
					this.SheetP.CurrentPosition = 0;
					this.SheetP.enabledisable();
				}
			}
			catch
			{
				this.SheetP.IDArray = new string[0];
				this.SheetP.CurrentPosition = -1;
				this.SheetP.enabledisable();
			}
			if(visActivity.IsSelected==false && visQuote.IsSelected==false)
				Tabber.Selected = visContact.ID;
		}

		public void ViewFormOnItemDatabound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					Repeater RepCrossLead = (Repeater) e.Item.FindControl("RepCrossLead");
					RepCrossLead.ItemDataBound += new RepeaterItemEventHandler(this.RepCrossLead_ItemDataBound);
					RepCrossLead.DataSource = getLeadInfo(Convert.ToInt32(DataBinder.Eval((DataRowView) e.Item.DataItem, "id").ToString()));
					RepCrossLead.DataBind();

                    ((FreeFields)e.Item.FindControl("ViewFreeFields")).ViewFreeFields(Convert.ToInt32(DataBinder.Eval((DataRowView)e.Item.DataItem, "id").ToString()), CRMTables.CRM_Leads, UC);

					LinkButton ModCon = (LinkButton) e.Item.FindControl("ModCon");
					ModCon.Text =Root.rm.GetString("Reftxt42");

					LinkButton LeadConvert = (LinkButton) e.Item.FindControl("LeadConvert");
					LeadConvert.Text =Root.rm.GetString("Ledtxt27");

					LinkButton BackToSearch = (LinkButton) e.Item.FindControl("BackToSearch");
					BackToSearch.Text =Root.rm.GetString("CRMrubtxt6");

					LinkButton WelcomeEmail = (LinkButton) e.Item.FindControl("WelcomeEmail");
					WelcomeEmail.Text = "Welcome Email";

					DropDownList dropwelcome = (DropDownList) e.Item.FindControl("dropwelcome");
					dropwelcome.DataTextField="subject";
					dropwelcome.DataValueField="id";
					dropwelcome.DataSource = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM ML_MAIL WHERE WELCOME=1");
					dropwelcome.DataBind();
					dropwelcome.Items.Insert(0,Root.rm.GetString("Choose"));

					Literal leadmail = (Literal)e.Item.FindControl("leadmail");
					leadmail.Text = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "email"));
					HtmlTableRow tremail = (HtmlTableRow)e.Item.FindControl("tremail");
					if(leadmail.Text.Length==0 || dropwelcome.Items.Count<2)
						tremail.Visible=false;
					else
						tremail.Visible=true;

					LinkButton PrintButton = (LinkButton) e.Item.FindControl("PrintButton");
					PrintButton.Text = "<img src=/i/printer.gif border=0>";
					PrintButton.Attributes.Add("onclick", "window.open('/report/htmlreport.aspx');");

					Session["report"] = null;
					ArrayList report = new ArrayList();
					CompanyReport cr = new CompanyReport();
					cr.idfield = Convert.ToInt32(DatabaseConnection.SqlScalartoObj("SELECT ID FROM QB_CUSTOMERQUERY WHERE TITLE='Fixed6'")); //ex222;
					Hashtable HtParams = new Hashtable();
					HtParams.Add("ID", Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id")));
					cr.Params = HtParams;
					cr.Finalize = true;
					cr.Type = 0;
					cr.itemPage = 10;
					report.Add(cr);
					Session["report"] = report;

					FillRepeaterActivity(Convert.ToInt32(DataBinder.Eval((DataRowView) e.Item.DataItem, "id")));
					Session["review"] = "0";

					string azPhone = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "AzPhone"));
					if (azPhone.Length > 0)
					{
						Literal AZph = (Literal) e.Item.FindControl("CompanyPhone");
						AZph.Text = "&nbsp;" +Root.rm.GetString("Reftxt48") + "&nbsp;" + azPhone;
					}

					Literal Title = (Literal) e.Item.FindControl("Title");
					Title.Text = (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Title")).Length > 0) ? Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Title")) + "&nbsp;" : "";

					Literal CompanyLabel = (Literal) e.Item.FindControl("CompanyLabel");
					CompanyLabel.Text = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName"));

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


					Literal VoipCallPhone = (Literal) e.Item.FindControl("VoipCallPhone");
					Literal VoipCallMobilePhone = (Literal) e.Item.FindControl("VoipCallMobilePhone");
						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE")).ToString().Length > 0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE"), @"[^0-9]", "");
							VoipCallPhone.Text = MakeVoipString(ph);
						}

						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone")).ToString().Length > 0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone"), @"[^0-9]", "");
							VoipCallMobilePhone.Text = MakeVoipString(ph);
						}

					long idzone =(Int64)DataBinder.Eval((DataRowView) e.Item.DataItem, "CommercialZone");
					if(idzone!=0)
					{
						Literal lblZone = (Literal)e.Item.FindControl("lblZone");
						lblZone.Text=DatabaseConnection.SqlScalar(string.Format("SELECT DESCRIPTION FROM ZONES WHERE ID={0}",idzone));
					}
					break;
			}
		}

		public void ViewForm_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "WelcomeEmail":
					if(((DropDownList) e.Item.FindControl("dropwelcome")).SelectedIndex>0)
					{
						Session["MailToSendID"]=((DropDownList) e.Item.FindControl("dropwelcome")).SelectedValue;
						Session["welcomemail"]=((Literal)e.Item.FindControl("leadmail")).Text+"|"+Session["CurrentRefId"].ToString()+"|2";
						SetGoBack("/crm/CRM_Lead.aspx?m=25&si=53&gb=1", new string[2] {Session["CurrentRefId"].ToString(), "a"});
						Response.Redirect("/MailingList/sendsingleaddressmail.aspx?m=46&si=51");
					}
					break;
				case "ModCon":
					FillForm(int.Parse(((Literal) e.Item.FindControl("IDRef")).Text));
					string strScript = "<SCRIPT>" + Environment.NewLine +
						"needsave('" +Root.rm.GetString("CRMcontxt77") + "');" + Environment.NewLine +
						"</SCRIPT>";

					ClientScript.RegisterStartupScript(this.GetType(), "anything", strScript);

					SubmitRef.Attributes.Add("onclick", "needsave('no')");
					Submit2.Attributes.Add("onclick", "needsave('no')");
					RefreshRepCategories.Attributes.Add("onclick", "needsave('no')");

					ViewForm.Visible = false;
					NewRepeater1.Visible = false;
					referenceForm.Visible = true;
					Tabber.EditTab="visContact";

					SheetP.Visible = false;
					break;
				case "CompanyLink":
					Literal CompanyIdForLink = (Literal) e.Item.FindControl("CompanyIdForLink");
					SetGoBack("/crm/CRM_Lead.aspx?m=25&si=53&gb=1", new string[2] {CompanyIdForLink.Text, "d"});
					Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
					break;
				case "BackToSearch":
					Tabber.Visible = false;
					NewRepeater1.Visible = true;

					break;
				case "LeadConvert":
					ConvertLeadToContact(int.Parse(((Literal) e.Item.FindControl("IDRef")).Text));

					break;
			}
		}

		private void ConvertLeadToContact(int id)
		{
			bool newCompany = ((DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM CRM_LEADS WHERE ID=" + id)).Length > 0);
			DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CRM_LEADS WHERE ID=" + id).Tables[0].Rows[0];
			int NewCompanyID = 0;
			int NewContactID = 0;

			if (newCompany)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("OWNERID", dr["OwnerID"]);
					dg.Add("CREATEDBYID", UC.UserId);
					dg.Add("COMPANYNAME", dr["CompanyName"]);
					dg.Add("COMPANYNAME", StaticFunctions.FilterSearch(dr["CompanyName"].ToString()));
					dg.Add("PHONE", dr["Phone"]);
					dg.Add("FAX", dr["Fax"]);
					dg.Add("EMAIL", dr["email"].ToString().Trim(' '));

					dg.Add("INVOICINGADDRESS", dr["Address"]);
					dg.Add("INVOICINGCITY", dr["City"]);
					dg.Add("INVOICINGSTATEPROVINCE", dr["Province"]);
					dg.Add("INVOICINGSTATE", dr["State"]);
					dg.Add("INVOICINGZIPCODE", dr["ZIPCode"]);
					dg.Add("DESCRIPTION", dr["Notes"]);
					dg.Add("GROUPS", dr["Groups"]);
					dg.Add("FROMLEAD", id);

					string industry = DatabaseConnection.SqlScalar("SELECT INDUSTRY FROM CRM_CROSSLEAD WHERE LEADID=" + id);
					if (industry.Length > 0)
						dg.Add("COMPANYTYPEID", industry);

					object newId = dg.Execute("BASE_COMPANIES", DigiDapter.Identities.Identity);

					NewCompanyID = Convert.ToInt32(newId);
				}
			}

			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("NAME", dr["Name"]);
				dg.Add("SURNAME", dr["Surname"]);
				dg.Add("OWNERID", dr["OwnerID"]);
				dg.Add("TITLE", dr["Title"]);
				dg.Add("ADDRESS_1", dr["Address"]);
				dg.Add("CITY_1", dr["City"]);
				dg.Add("PROVINCE_1", dr["Province"]);
				dg.Add("STATE_1", dr["State"]);
				dg.Add("ZIPCODE_1", dr["ZIPCode"]);
				dg.Add("VATID", dr["VatID"]);
				dg.Add("TAXIDENTIFICATIONNUMBER", dr["TaxIdentificationNumber"]);
				dg.Add("EMAIL", dr["Email"].ToString().Trim(' '));
				dg.Add("PHONE_1", dr["Phone"]);
				dg.Add("FAX", dr["Fax"]);
				dg.Add("MOBILEPHONE_1", dr["MobilePhone"]);
				dg.Add("BUSINESSROLE", dr["BusinessRole"]);
				dg.Add("NOTES", dr["Notes"]);
				dg.Add("GROUPS", dr["Groups"]);
				if (newCompany)
					dg.Add("COMPANYID", NewCompanyID);
				else
					dg.Add("COMPANYID", dr["CompanyID"]);

				dg.Add("BIRTHDAY", dr["BirthDay"]);
				dg.Add("BIRTHPLACE", dr["BirthPlace"]);
				dg.Add("CATEGORIES", dr["Categories"]);
				dg.Add("FROMLEAD", id);

				object newId = dg.Execute("BASE_CONTACTS", DigiDapter.Identities.Identity);

				NewContactID = Convert.ToInt32(newId);
			}
			DatabaseConnection.DoCommand("UPDATE CRM_LEADS SET LIMBO=1 WHERE ID=" + id);
			if (newCompany)
				DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET COMPANYID=" + NewCompanyID + ",LEADID=NULL WHERE LEADID=" + id);
			else
				DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET REFERRERID=" + NewContactID + ",LEADID=NULL WHERE LEADID=" + id);

			if (newCompany)
			{
				DatabaseConnection.DoCommand("UPDATE CRM_OPPORTUNITYCONTACT SET CONTACTID=" + NewCompanyID + ", CONTACTTYPE=0 WHERE CONTACTTYPE=1 AND CONTACTID=" + id);
				DatabaseConnection.DoCommand("UPDATE CRM_CROSSOPPORTUNITY SET CONTACTID=" + NewCompanyID + ", CONTACTTYPE=0 WHERE CONTACTTYPE=1 AND CONTACTID=" + id);
				Session["fromlead"] = NewCompanyID;
				Response.Redirect("/CRM/crm_companies.aspx?m=25&dgb=1&si=29");
			}
			else
			{
				Response.Redirect("/CRM/base_contacts.aspx?action=VIEW&full=" + NewContactID);
			}

		}


		private void FillForm(int id)
		{
			string sqlString;
			sqlString = "SELECT CRM_LEADS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2 ";
			sqlString += "FROM CRM_LEADS LEFT OUTER JOIN BASE_COMPANIES ON CRM_LEADS.COMPANYID = BASE_COMPANIES.ID ";
			sqlString += "WHERE CRM_LEADS.ID=" + id;
			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			if (ds.Tables[0].Rows.Count > 0)
			{
				DataRow myDataRow = ds.Tables[0].Rows[0];

				CRM_Leads_ID.Text = myDataRow["ID"].ToString();
				CRM_Leads_CompanyID.Text = myDataRow["CompanyID"].ToString();
				CRM_Leads_CompanyName.Text = myDataRow["CompanyName"].ToString();
				CRM_Leads_Name.Text = myDataRow["Name"].ToString();
				CRM_Leads_Surname.Text = myDataRow["Surname"].ToString();
				CRM_Leads_Title.Text = myDataRow["Title"].ToString();
				CRM_Leads_Address.Text = myDataRow["Address"].ToString();
				CRM_Leads_City.Text = myDataRow["City"].ToString();
				CRM_Leads_Province.Text = myDataRow["Province"].ToString();
				CRM_Leads_ZIPCode.Text = myDataRow["ZIPCode"].ToString();
				CRM_Leads_State.Text = myDataRow["State"].ToString();
				CRM_Leads_EMail.Text = myDataRow["EMail"].ToString();
				CRM_Leads_Phone.Text = myDataRow["Phone"].ToString();
				CRM_Leads_Fax.Text = myDataRow["Fax"].ToString();
				CRM_Leads_MobilePhone.Text = myDataRow["MobilePhone"].ToString();
				CRM_Leads_BusinessRole.Text = myDataRow["BusinessRole"].ToString();
				CRM_Leads_Notes.Text = myDataRow["Notes"].ToString();
				CRM_Leads_BirthPlace.Text = myDataRow["BirthPlace"].ToString();
				CRM_Leads_Categories.Text = myDataRow["Categories"].ToString();
				CRM_Leads_BirthDay.Text = (myDataRow["BirthDay"].ToString().Length > 0) ? ((DateTime) myDataRow["BirthDay"]).ToShortDateString() : "";
				CRM_Leads_WebSite.Text = myDataRow["WebSite"].ToString();
				Groups.SetGroups(myDataRow["Groups"].ToString());

				dropZones.SelectedItem.Selected = false;
				foreach (ListItem li in dropZones.Items)
				{
					if (li.Value == myDataRow["CommercialZone"].ToString())
					{
						li.Selected = true;
						break;
					}
				}

				FillRepCategories();
                EditFreeFields.CheckFreeFields(id, CRMTables.CRM_Leads, UC);
				FillCrossLeadDrop();

				FillFormCrossLead(id);
				AddKeepAlive();
			}
			else
				HackLock("Lead:" + UC.UserId + ">" + id);

		}

		private void FillFormCrossLead(int id)
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
			sqlString.AppendFormat("WHERE CRM_CROSSLEAD.LEADID={0}", id);

			DataTable dt = DatabaseConnection.CreateDataset(sqlString.ToString()).Tables[0];
			if (dt.Rows.Count == 0)
				return;
			DataRow dr = dt.Rows[0];
			TextBox tx;
			DropDownList dp;

			tx = (TextBox) referenceForm.FindControl("CrossLead_AssociatedCompany");
			tx.Text = dr["AssociatedCompany"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_CompanyName");
			tx.Text = dr["CompanyName"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_AssociatedContact");
			tx.Text = dr["AssociatedContact"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_ContatcName");
			tx.Text = dr["ContactName"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_LeadOwner");
			tx.Text = dr["LeadOwner"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_OwnerName");
			tx.Text = dr["OwnerName"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_PotentialRevenue");
			tx.Text = (dr["PotentialRevenue"] == DBNull.Value) ? "" : Convert.ToDecimal(dr["PotentialRevenue"]).ToString(@"#.00");
			tx = (TextBox) referenceForm.FindControl("CrossLead_EstimatedCloseDate");
			if (dr["EstimatedCloseDate"] is DateTime)
				tx.Text = ((DateTime) dr["EstimatedCloseDate"]).ToShortDateString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_Campaign");
			tx.Text = dr["Campaign"].ToString();
			tx = (TextBox) referenceForm.FindControl("CrossLead_SalesPerson");
			tx.Text = dr["SalesPerson"].ToString();
			tx = (TextBox) referenceForm.FindControl("crossLead_SalesPersonName");
			tx.Text = dr["SalesPersonName"].ToString();

			dp = (DropDownList) referenceForm.FindControl("CrossLead_Status");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["Status"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
			dp = (DropDownList) referenceForm.FindControl("CrossLead_Rating");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["Rating"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
			dp = (DropDownList) referenceForm.FindControl("CrossLead_ProductInterest");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["ProductInterest"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
			dp = (DropDownList) referenceForm.FindControl("CrossLead_LeadCurrency");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["LeadCurrency"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
			dp = (DropDownList) referenceForm.FindControl("CrossLead_Source");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["Source"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
			dp = (DropDownList) referenceForm.FindControl("CrossLead_Industry");
			foreach (ListItem i in dp.Items)
			{
				if (i.Value == dr["Industry"].ToString())
				{
					i.Selected = true;
					break;
				}
			}
		}

		private void FillCrossLeadDrop()
		{
			FillDropDownLead((DropDownList) referenceForm.FindControl("CrossLead_Status"), "1",Root.rm.GetString("Ledtxt21"));
			FillDropDownLead((DropDownList) referenceForm.FindControl("CrossLead_Rating"), "2",Root.rm.GetString("Ledtxt22"));
			FillDropDownLead((DropDownList) referenceForm.FindControl("CrossLead_ProductInterest"), "3",Root.rm.GetString("Ledtxt23"));
			FillDropDownLead((DropDownList) referenceForm.FindControl("CrossLead_Source"), "4",Root.rm.GetString("Ledtxt24"));
			FillDropDownLead((DropDownList) referenceForm.FindControl("CrossLead_Industry"), "6",Root.rm.GetString("Ledtxt25"));

			DropDownList CrossLead_LeadCurrency = (DropDownList) referenceForm.FindControl("CrossLead_LeadCurrency");
			CrossLead_LeadCurrency.DataTextField = "Currency";
			CrossLead_LeadCurrency.DataValueField = "idcur";
			CrossLead_LeadCurrency.DataSource = DatabaseConnection.CreateDataset("SELECT CAST(ID AS VARCHAR(10)) IDCUR,CURRENCY FROM CURRENCYTABLE").Tables[0];
			CrossLead_LeadCurrency.DataBind();
		}


		private void ModifyForm(int id)
		{
			using (DigiDapter dg = new DigiDapter("SELECT ID FROM CRM_LEADS WHERE ID=" + id))
			{
				if (!dg.HasRows)
				{
				}
				dg.Add("CREATEDBYID", UC.UserId);

				TextBox owner = (TextBox) referenceForm.FindControl("crossLead_leadowner");
				if (owner.Text.Length > 0)
					dg.Add("OWNERID", owner.Text);
				else
					dg.Add("OWNERID", UC.UserId);

				if (CRM_Leads_CompanyID.Text.Length == 0)
					dg.Add("COMPANYID", 0);
				else
					dg.Add("COMPANYID", CRM_Leads_CompanyID.Text);

				dg.Add("COMPANYNAME", CRM_Leads_CompanyName.Text.Trim());
				dg.Add("NAME", CRM_Leads_Name.Text.Trim());
				dg.Add("SURNAME", CRM_Leads_Surname.Text.Trim());
				dg.Add("TITLE", CRM_Leads_Title.Text.Trim());
				dg.Add("ADDRESS", CRM_Leads_Address.Text.Trim());
				dg.Add("CITY", CRM_Leads_City.Text.Trim());
				dg.Add("PROVINCE", CRM_Leads_Province.Text.Trim());
				dg.Add("ZIPCODE", CRM_Leads_ZIPCode.Text.Trim());
				dg.Add("STATE", CRM_Leads_State.Text.Trim());
				dg.Add("EMAIL", CRM_Leads_EMail.Text.Trim());
				dg.Add("PHONE", CRM_Leads_Phone.Text.Trim());
				dg.Add("FAX", CRM_Leads_Fax.Text.Trim());
				dg.Add("MOBILEPHONE", CRM_Leads_MobilePhone.Text.Trim());
				dg.Add("BUSINESSROLE", CRM_Leads_BusinessRole.Text.Trim());
				dg.Add("NOTES", CRM_Leads_Notes.Text.Trim());
				dg.Add("BIRTHPLACE", CRM_Leads_BirthPlace.Text.Trim());

				if(dropZones.SelectedIndex>0)
					dg.Add("COMMERCIALZONE",dropZones.SelectedValue);
				else
					dg.Add("COMMERCIALZONE",0);

				string cat = "|";
				foreach (RepeaterItem it in RepCategories.Items)
				{
					CheckBox Check = (CheckBox) it.FindControl("Check");
					if (Check.Checked)
						cat += ((Literal) it.FindControl("IDCat")).Text + "|";
				}
				if (cat.Length > 1)
					dg.Add("CATEGORIES", cat);
				else
					dg.Add("CATEGORIES", DBNull.Value);

				try
				{
					DateTime checkDate = Convert.ToDateTime(CRM_Leads_BirthDay.Text);
					if (checkDate < new DateTime(1910, 1, 1))
						dg.Add("BIRTHDAY", DBNull.Value);
					else
						dg.Add("BIRTHDAY", checkDate);
				}
				catch
				{
					dg.Add("BIRTHDAY", DBNull.Value);
				}

				dg.Add("WEBSITE", CRM_Leads_WebSite.Text);

				if (Groups.GetValue.Length > 0)
				{
					dg.Add("GROUPS", Groups.GetValue);
				}
				else
				{
					dg.Add("GROUPS", "|" + UC.UserGroupId + "|");
				}

				if (dg.HasRows)
				{
					dg.Add("LASTACTIVITY", 1); //MODIFICA
					dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
					dg.Add("LASTMODIFIEDBYID", UC.UserId);
				}

				object newId = dg.Execute("CRM_LEADS", "ID=" + id, DigiDapter.Identities.Identity);

				if (id == -1)
				{
					int tempId = int.Parse(newId.ToString());
					ModifyCrossLead(tempId);
                    EditFreeFields.FillFreeFields(tempId, CRMTables.CRM_Leads,UC);
					DatabaseConnection.CommitTransaction();
					FillView(tempId);
				}
				else
				{
					ModifyCrossLead(id);
                    EditFreeFields.FillFreeFields(id, CRMTables.CRM_Leads,UC);
					DatabaseConnection.CommitTransaction();
					FillView(id);
				}

			}
		}

		private void ModifyCrossLead(int id)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.AddOrNull("AssociatedCompany", CrossLead_AssociatedCompany.Text);
				dg.AddOrNull("AssociatedContact", CrossLead_AssociatedContact.Text);
				dg.AddOrNull("LeadOwner", CrossLead_LeadOwner.Text);
				dg.Add("STATUS", CrossLead_Status.SelectedValue);
				dg.Add("RATING", CrossLead_Rating.SelectedValue);
				dg.Add("PRODUCTINTEREST", CrossLead_ProductInterest.SelectedValue);
				dg.Add("POTENTIALREVENUE", StaticFunctions.FixDecimal(CrossLead_PotentialRevenue.Text));
				if (CrossLead_EstimatedCloseDate.Text.Length > 0)
					dg.Add("ESTIMATEDCLOSEDATE", DateTime.Parse(CrossLead_EstimatedCloseDate.Text));
				else
					dg.Add("ESTIMATEDCLOSEDATE", DBNull.Value);
				dg.Add("LEADCURRENCY", CrossLead_LeadCurrency.SelectedValue);
				dg.Add("SOURCE", CrossLead_Source.SelectedValue);
				dg.AddOrNull("Campaign", CrossLead_Campaign.Text);
				dg.AddOrNull("Industry", CrossLead_Industry.SelectedValue);
				dg.AddOrNull("SalesPerson", CrossLead_SalesPerson.Text);
				dg.Add("LEADID", id);
				dg.Execute("CRM_CROSSLEAD", "LEADID=" + id);
			}
		}


		private void InitAjax()
		{
			Manager.Register(this, "Ajax.Leads", Debug.None);
		}

		[Method]
		public string CheckDuplicatedLeads(string company, string surname)
		{
			if (company.Length == 0)
				return DatabaseConnection.SqlScalar("SELECT NAME+'|'+SURNAME AS RET FROM CRM_LEADS WHERE SURNAME = '" + DatabaseConnection.FilterInjection(surname) + "'");
			else
				return DatabaseConnection.SqlScalar("SELECT NAME+'|'+SURNAME AS RET FROM CRM_LEADS WHERE COMPANYNAME =  '" + DatabaseConnection.FilterInjection(company) + "' AND SURNAME = '" + DatabaseConnection.FilterInjection(surname) + "'");
		}

		public void NewActivity_Click(Object sender, EventArgs e)
		{

				SetGoBack("/crm/CRM_Lead.aspx?m=25&si=53&gb=1", new string[2] {Session["CurrentRefId"].ToString(), "a"});
				Session["AcLeadID"] = Session["CurrentRefId"].ToString();
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

		public void btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "RefreshRepCategories":
					FillRepCategories();
					ClientScript.RegisterClientScriptBlock(this.GetType(), "xxx", "<script>g_fFieldsChanged=1;</script>");
					break;
			}
		}


		public void RapSubmit_Click(Object sender, EventArgs e)
		{
			if (RapRagSoc.Text.Length > 0)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("CREATEDBYID", UC.UserId);
					dg.Add("COMPANYNAME", RapRagSoc.Text);
					dg.Add("PHONE", RapPhone.Text);
					dg.Add("EMAIL", RapEmail.Text.Trim(' '));
					dg.Add("OWNERID", UC.UserId);
					dg.Add("GROUPS", "|" + UC.AdminGroupId + "|" + UC.UserGroupId + "|");
					object newId = dg.Execute("BASE_COMPANIES", "ID=-1", DigiDapter.Identities.Identity);

					SetGoBack("/crm/crm_companies.aspx?m=25&si=29&gb=1", new string[] {newId.ToString(), "d"});

					RapInfo.Text = "\"<a href=/crm/crm_companies.aspx?m=25&si=29&gb=1><span class=divautoformRequired>" + RapRagSoc.Text + "</span></a>\" " +Root.rm.GetString("Bcotxt43");
					RapRagSoc.Text = String.Empty;
					RapPhone.Text = String.Empty;
					RapEmail.Text = String.Empty;
				}

			}
			else
			{
				RapInfo.Text =Root.rm.GetString("Bcotxt48");
			}
		}

		private void FillRepeaterActivity(int refID)
		{
			AcCrono.ParentID = refID;
			AcCrono.OpportunityID = 0;
			AcCrono.AcType = (byte) AType.Lead;


			AcCrono.FromSheet = "l";
			AcCrono.Refresh();

			if (AcCrono.ItemCount > 0)
				AcCrono.Visible = true;
			else
			{
				AcCrono.Visible = false;
				RepeaterActivityInfo.Text =Root.rm.GetString("CRMcontxt24");
			}
		}

		public void FileRepDatabound(Object sender, RepeaterItemEventArgs e)
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
					Label LRN = (Label) e.Item.FindControl("LbReviewNumber");

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


		private void FillRepCategories()
		{
			FillRepCategories(false);
		}

		private void FillRepCategories(bool newlead)
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) ORDER BY FLAGPERSONAL DESC").Tables[0];
			if (dt.Rows.Count > 0 && !newlead)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (CRM_Leads_Categories.Text.IndexOf("|" + dr["id"].ToString() + "|") > -1)
						dr["tocheck"] = "1";
				}
			}
			RepCategories.DataSource = new DataView(dt, "", "tocheck desc", DataViewRowState.CurrentRows);
			RepCategories.DataBind();
		}

		public void RepCategories_DataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal IDCat = (Literal) e.Item.FindControl("IDCat");
					if (CRM_Leads_Categories.Text.IndexOf("|" + IDCat.Text + "|") > -1)
					{
						CheckBox Check = (CheckBox) e.Item.FindControl("Check");
						Check.Checked = true;
					}

					DataSet ds = DatabaseConnection.CreateDataset("SELECT ID FROM CRM_LEADS WHERE CATEGORIES LIKE '%|" + DatabaseConnection.FilterInjection(IDCat.Text) + "|%'");
					if (ds.Tables[0].Rows.Count < 1)
					{
						LinkButton DeleteCat = (LinkButton) e.Item.FindControl("DeleteCat");
						DeleteCat.Text = "<img src='/i/erase.gif' border=0 alt='" +Root.rm.GetString("CRMcontxt49") + "'>";
						DeleteCat.Attributes.Add("onclick", "g_fFieldsChanged=0;return confirm('" +Root.rm.GetString("CRMcontxt50") + "');");
					}
					break;
			}
		}

		public void RepCategories_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DeleteCat":
					string count = "SELECT COUNT(*) FROM CRM_LEADS WHERE CATEGORIES LIKE '|" + int.Parse(((Literal) e.Item.FindControl("IDCat")).Text) + "|'";

					int empty = (int) DatabaseConnection.SqlScalartoObj(count);

					if (empty < 1)
					{
						string sqlString = "DELETE FROM CRM_REFERRERCATEGORIES WHERE ID =" + int.Parse(((Literal) e.Item.FindControl("IDCat")).Text);
						DatabaseConnection.DoCommand(sqlString);
					}
					FillRepCategories();
					ClientScript.RegisterClientScriptBlock(this.GetType(), "xxx", "<script>g_fFieldsChanged=1;</script>");
					break;
			}
		}

		public bool NoLength(string tx)
		{
			return false;
		}

		public DataView getLeadInfo(int id)
		{
			string twoLetterCulture = UC.Culture.Substring(0, 2);
			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT CRM_CROSSLEAD.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, ");
			sqlString.Append("BASE_CONTACTS.SURNAME + ' ' + BASE_CONTACTS.NAME AS CONTACTNAME, ");
			sqlString.Append("CRM_OPPORTUNITY.TITLE AS OPPORTUNITYNAME, ");
			sqlString.Append("ACCOUNT_1.SURNAME + ' ' + ACCOUNT_1.NAME AS OWNERNAME, ");
			sqlString.Append("ACCOUNT_2.SURNAME + ' ' + ACCOUNT_2.NAME AS SALESPERSONNAME, ");
            sqlString.Append("ACCOUNT_2.SELFCONTACTID, ");
			sqlString.Append("COMPANYTYPE.DESCRIPTION AS INDUSTRYNAME, ");
			sqlString.Append("CRM_LEADDESCRIPTION1.DESCRIPTION AS STATUSDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION2.DESCRIPTION AS RATINGDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION3.DESCRIPTION AS PRODUCTINTERESTDESCRIPTION, ");
			sqlString.Append("CRM_LEADDESCRIPTION4.DESCRIPTION AS SOURCEDESCRIPTION, ");
			sqlString.Append("CURRENCYTABLE.CURRENCY AS LEADCURRENCYDESCRIPTION ");
			sqlString.Append("FROM CRM_CROSSLEAD ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_2 ON CRM_CROSSLEAD.SALESPERSON = ACCOUNT_2.UID ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_1 ON CRM_CROSSLEAD.LEADOWNER = ACCOUNT_1.UID ");
			sqlString.Append("LEFT OUTER JOIN CRM_OPPORTUNITY ON CRM_CROSSLEAD.ASSOCIATEDOPPORTUNITY = CRM_OPPORTUNITY.ID ");
			sqlString.Append("LEFT OUTER JOIN BASE_CONTACTS ON CRM_CROSSLEAD.ASSOCIATEDCONTACT = BASE_CONTACTS.ID ");
			sqlString.Append("LEFT OUTER JOIN BASE_COMPANIES ON CRM_CROSSLEAD.ASSOCIATEDCOMPANY = BASE_COMPANIES.ID ");

			sqlString.AppendFormat("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION1 ON CRM_CROSSLEAD.STATUS = CRM_LEADDESCRIPTION1.K_ID AND CRM_LEADDESCRIPTION1.LANG='{0}' ", twoLetterCulture);
			sqlString.AppendFormat("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION2 ON CRM_CROSSLEAD.RATING = CRM_LEADDESCRIPTION2.K_ID AND CRM_LEADDESCRIPTION2.LANG='{0}' ", twoLetterCulture);
			sqlString.AppendFormat("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION3 ON CRM_CROSSLEAD.PRODUCTINTEREST = CRM_LEADDESCRIPTION3.K_ID AND CRM_LEADDESCRIPTION3.LANG='{0}' ", twoLetterCulture);
			sqlString.AppendFormat("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION4 ON CRM_CROSSLEAD.SOURCE = CRM_LEADDESCRIPTION4.K_ID AND CRM_LEADDESCRIPTION4.LANG='{0}' ", twoLetterCulture);
			sqlString.AppendFormat("LEFT OUTER JOIN CURRENCYTABLE ON CRM_CROSSLEAD.LEADCURRENCY = CURRENCYTABLE.ID ", twoLetterCulture);

			sqlString.AppendFormat("LEFT OUTER JOIN COMPANYTYPE ON CRM_CROSSLEAD.INDUSTRY = COMPANYTYPE.K_ID AND COMPANYTYPE.LANG='{0}' ", twoLetterCulture);


			DataSet temp = DatabaseConnection.CreateDataset(sqlString.ToString() + "WHERE CRM_CROSSLEAD.LEADID=" + id);
			if (temp.Tables[0].Rows.Count > 0)
				return temp.Tables[0].DefaultView;
			else
			{
				ModifyCrossLead(id);
				return DatabaseConnection.CreateDataset(sqlString.ToString() + "WHERE CRM_CROSSLEAD.LEADID=" + id).Tables[0].DefaultView;
			}
		}

		private void FillDropDownLead(DropDownList st, string type, string noselect)
		{
			DataSet ds;
			switch (type)
			{
				case "6":
					ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY DESCRIPTION");
					break;
				default:
					ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM CRM_LEADDESCRIPTION WHERE LANG='" + UC.Culture.Substring(0, 2) + "' AND TYPE=" + type + " ORDER BY K_ID");
					break;
			}
			st.DataSource = ds;
			st.DataTextField = "Description";
			st.DataValueField = "K_ID";
			st.DataBind();
			st.Items.Insert(0, noselect);
			st.Items[0].Value = "0";
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
			this.SrcBtn.Click += new EventHandler(srcbtn_Click);
			this.BtnAdvanced.Click += new EventHandler(btnAdvanced_Click);
			this.Load += new EventHandler(this.Page_Load);
			this.LbnNew.Click += new EventHandler(this.NewLead);
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.BtnGroup.Click += new EventHandler(this.BtnSearch_Click);
			this.RapSubmit.Click += new EventHandler(this.RapSubmit_Click);
			this.SubmitRef.Click += new EventHandler(this.ReferenceFormSubmit);
			this.RefreshRepCategories.Click += new EventHandler(this.btn_Click);
			this.Submit2.Click += new EventHandler(this.ReferenceFormSubmit);
			this.NewActivityPhone.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityLetter.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityFax.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityMemo.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityEmail.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityVisit.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivityGeneric.Click += new EventHandler(this.NewActivity_Click);
			this.NewActivitySolution.Click += new EventHandler(this.NewActivity_Click);
			this.NewRepeater1.ItemCommand += new RepeaterCommandEventHandler(this.Repeater1_Command);
			this.ViewForm.ItemCommand += new RepeaterCommandEventHandler(this.ViewForm_Command);
			this.RepCategories.ItemCommand += new RepeaterCommandEventHandler(this.RepCategories_Command);
			this.NewRepeater1.ItemDataBound += new RepeaterItemEventHandler(this.Repeater1_ItemDataBound);
			this.ViewForm.ItemDataBound += new RepeaterItemEventHandler(this.ViewFormOnItemDatabound);
			this.RepCategories.ItemDataBound += new RepeaterItemEventHandler(this.RepCategories_DataBound);
			this.Tabber.TabClick += new TabClickDelegate(Tabber_TabClick);
		}


		#endregion

		private void srcbtn_Click(object sender, EventArgs e)
		{
			this.FillNewRepeater1(true,srcComp.GetLeadQuery());

			Tabber.Visible = false;
			referenceForm.Visible = false;
			ViewForm.Visible = false;
			advancedSearch.Visible=false;
		}


		private void InitSheetPaging(string sqlString)
		{
			DataTable dt = DatabaseConnection.CreateDataset(sqlString).Tables[0];
			string[] myarray = new string[dt.Rows.Count];
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					myarray[i] = dt.Rows[i]["ID"] + "|" + dt.Rows[i]["Name"] + " " + dt.Rows[i]["SurName"] + " " + dt.Rows[i]["CompanyName"];
				}
			}

			SheetP.IDArray = myarray;
		}

		private void SheetP_NextClick(object sender, EventArgs e)
		{
			FillView(this.SheetP.GetCurrentID);
		}

		private void SheetP_PrevClick(object sender, EventArgs e)
		{
			FillView(this.SheetP.GetCurrentID);
		}

		public void RepCrossLead_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal LblPotentialRevenue = (Literal) e.Item.FindControl("LblPotentialRevenue");
					double pr;
					try
					{
						pr = Convert.ToDouble(DataBinder.Eval((DataRowView) e.Item.DataItem, "PotentialRevenue"));
					}
					catch
					{
						pr = 0;
					}
					string sy;
					try
					{
						sy = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE ID=" + (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadCurrency"));
					}
					catch
					{
						{
							sy = DatabaseConnection.SqlScalar("SELECT CURRENCYSYMBOL FROM CURRENCYTABLE CHANGEFROMEURO=1 AND CHANGETOEURO=1");
						}
					}
					LblPotentialRevenue.Text = sy + " " + Math.Round(pr, 2).ToString();

                    if (DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID")!=System.DBNull.Value)
                        if (Convert.ToInt64(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID"))>0)
                        {
                            string selfcontact = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID"));
                            Literal litViewSalesContact = (Literal)e.Item.FindControl("litViewSalesContact");
                            litViewSalesContact.Text = string.Format("&nbsp;<img src=\"/i/lens.gif\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/ViewContact.aspx?render=no&id={0}',event,500,300);\">", selfcontact);
                        }
					break;
			}
		}

		private void btnAdvanced_Click(object sender, EventArgs e)
		{
			NewRepeater1.Visible = false;
			Tabber.Visible = false;
			advancedSearch.Visible = true;
			srcComp.Visible = true;
		}



		private void initNewRepeater()
		{
			this.NewRepeater1.UsePagedDataSource=true;
			this.NewRepeater1.PageSize=UC.PagingSize;
		}
		private void FillNewRepeater1(bool bind, string query)
		{
			StringBuilder sqlString = new StringBuilder();
			if(query.Length==0)
			{
				string queryGroup = GroupsSecure("CRM_Leads.Groups");

					sqlString.AppendFormat("SELECT CRM_LEADS.ID,CRM_LEADS.NAME, CRM_LEADS.SURNAME, CRM_LEADS.PHONE, CRM_LEADS.MOBILEPHONE, CRM_LEADS.COMPANYNAME ,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS NAMEOWNER, CRM_LEADS.CREATEDDATE, CRM_CROSSLEAD.SALESPERSON, CRM_LEADS.OWNERID ", DatabaseConnection.MaxResult);
					sqlString.Append("FROM CRM_LEADS LEFT OUTER JOIN BASE_COMPANIES ON CRM_LEADS.COMPANYID = BASE_COMPANIES.ID ");
					sqlString.Append("LEFT OUTER JOIN ACCOUNT ON CRM_LEADS.OWNERID = ACCOUNT.UID ");
					sqlString.Append("LEFT OUTER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID = CRM_CROSSLEAD.LEADID ");

					sqlString.Append("WHERE (CRM_LEADS.LIMBO = 0)");

					if (queryGroup.Length > 0)
					{
						sqlString.AppendFormat(" AND (({0}) OR CRM_LEADS.OWNERID={1} OR CRM_CROSSLEAD.SALESPERSON={1})", queryGroup, UC.UserId);
					}

					if(UC.Zones.Length>0)
						sqlString.AppendFormat(" AND ({0})", ZoneSecure("CRM_LEADS.COMMERCIALZONE"));

			}
			else
				sqlString.AppendFormat("{0}",query);

			if(sqlString.ToString().ToUpper().IndexOf("ORDER BY")<0)
				sqlString.Append(" ORDER BY CRM_LEADS.SURNAME");
			this.NewRepeater1.sqlDataSource=sqlString.ToString();

			if(bind)
				this.NewRepeater1.DataBind();

			this.NewRepeater1.Visible=true;
			InitSheetPaging(sqlString.ToString());
		}

		private void Tabber_TabClick(string tabId)
		{
			switch(tabId)
			{
				case "visQuote":
					CustomerQuote1.IsQuote=true;
					CustomerQuote1.CustomerData=new string[]{"2",Session["CurrentRefId"].ToString()};
					CustomerQuote1.Bind();
					Customerquote2.IsQuote=false;
					Customerquote2.CustomerData=new string[]{"2",Session["CurrentRefId"].ToString()};
					Customerquote2.Bind();

					Tabber.Selected = visQuote.ID;
					break;
			}
		}
	}
}
