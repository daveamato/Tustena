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
using System.IO;
using System.Text;
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
	public partial class Base_Contacts : G
	{

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;
            if (visDocuments.Visible == true && !M.IsModule(ActiveModules.Storage))
                Tabber.HideTabs += visDocuments.ID;


            if (visQuote.Visible == true && (!M.IsModule(ActiveModules.Sales) || !M.IsModule(ActiveModules.SalesWarehouse)))
                Tabber.HideTabs += visQuote.ID;

            base.OnPreRenderComplete(e);
        }

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				initNewRepeater();
				if (Request["Ajax"] != null)
					InitAjax();
				DeleteGoBack();
				GroupDescription.Text = " (" + G.GetGroupDescription(UC.UserGroupId) + ")";

				Back.Click += new EventHandler(BtnBack_Click);
				BackCo.Click += new EventHandler(BtnBack_Click);

				DateFormat.Text = "[" + UC.myDTFI.ShortDatePattern.ToString() + "]";
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
					if (!(gb.sheet.ToLower().IndexOf("base_contacts.aspx") > 0))
						Back.Visible = false;
				}

				RapSubmit.Text =Root.rm.GetString("Bcotxt4");
				BtnGroup.Text =Root.rm.GetString("Bcotxt2");
				NewDoc.Text =Root.rm.GetString("CRMrubtxt4");

				if (Session["CurrentActiveMenu"] != null)
				{
					if (Convert.ToInt16(Session["CurrentActiveMenu"]) == 1)
					{
						LblTitle.Text =Root.rm.GetString("Reftxt5");
					}
					else
					{
						LblTitle.Text =Root.rm.GetString("Reftxt44");
					}
				}
				else
				{
					LblTitle.Text =Root.rm.GetString("Reftxt5");
				}
                if (Tabber.Expand)
                    AcCrono.NoPaging();

				if (!Page.IsPostBack)
				{
					this.FilldropZones();
					AcCrono.ParentID = 0;
					AcCrono.OpportunityID = 0;
					AcCrono.AcType = (byte) AType.Contacts;
					NewActivityPhone.Text =Root.rm.GetString("Wortxt6");
					NewActivityLetter.Text =Root.rm.GetString("Wortxt7");
					NewActivityFax.Text =Root.rm.GetString("Wortxt8");
					NewActivityMemo.Text =Root.rm.GetString("Wortxt9");
					NewActivityEmail.Text =Root.rm.GetString("Wortxt10");
					NewActivityVisit.Text =Root.rm.GetString("Wortxt11");
					NewActivityGeneric.Text =Root.rm.GetString("Wortxt12");
					NewActivitySolution.Text =Root.rm.GetString("Wortxt13");

					G.FillListGroups(UC, this.ListGroups);
					FillListCategories();

					ReferenceForm.Visible = false;
					Tabber.Visible = false;

					visContact.Header = Root.rm.GetString("CRMrubtxt1");

					if (Request.Params["action"] != null)
					{
						if (Request.Params["action"] == "NEW")
						{
							this.PrepareForNewContact();

							if (Session["contact"] != null)
							{
								Referring_CompanyID.Text = Session["contact"].ToString();
								Referring_CompanyTX.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + Session["contact"]);

								string groups = DatabaseConnection.SqlScalar("SELECT GROUPS FROM BASE_COMPANIES WHERE ID=" + Session["contact"]);
								Session["GroupsSetGroup"] = groups;


							}
							this.SheetP.Visible = false;
							visContact.Header =Root.rm.GetString("CRMrubtxt1");
							ReferenceForm.Visible = true;
							Tabber.Visible = true;
							SearchListRepeater.Visible = false;
							ViewForm.Visible = false;
						}
						if (Request.Params["action"] == "MOD")
						{
							ViewForm.Visible = false;

							SearchListRepeater.Visible = false;
							ReferenceForm.Visible = true;
							FillForm(int.Parse(Request.Params["full"]));
						}
						if (Request.Params["action"] == "VIEW")
						{
							Back.Visible = true;
							if (Session["Contactarray"] != null)
							{
								string[] tempArray = (string[]) Session["Contactarray"];
								this.SheetP.IDArray = tempArray;
								for (int i = 0; i < tempArray.Length; i++)
								{
									if (tempArray[i].Split('|')[0].CompareTo(Request.Params["full"]) == 0)
									{
										this.SheetP.CurrentPosition = i;
										this.SheetP.enabledisable();
										break;
									}
								}
								Session.Remove("Contactarray");
							}
							FillView(int.Parse(Request.Params["full"]));
						}
					}
					if (Request.Params["gb"] != null && isGoBack)
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
								case "d":
									Tabber.Selected = visDocuments.ID;
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

					if(Request.Params["action"]==null)
						FillSearchListRepeater(false,string.Empty);

                    if (Session["openId"] != null)
                    {
                        FillView(int.Parse(Session["openId"].ToString()));
                        Session.Remove("openId");
                    }
				}

			}
			BtnNew.Text =Root.rm.GetString("Reftxt40");
			BtnSearch.Text =Root.rm.GetString("Reftxt41");

			SubmitRef.Text =Root.rm.GetString("Reftxt43");
			Submit2.Text =Root.rm.GetString("Reftxt43");

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
					if (t.ID == "Referring_ID")
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

		private void NewContact(object sender, EventArgs e)
		{
			PrepareForNewContact();
		}

		private void PrepareForNewContact()
		{
			InitAjax();
            Object progressive;
            progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS WHERE TYPE=2");
            if (progressive != null)
            {
                if (!((bool)progressive))
                {
                    Referring_CODE.ReadOnly = true;
                    Referring_CODE.Text = Root.rm.GetString("Quotxt43");
                }
            }
            else
                Referring_CODE.Text = String.Empty;

			Tabber.Visible = true;
			ReferenceForm.Visible = true;
			SearchListRepeater.Visible = false;
			ViewForm.Visible = false;
			ClearTextBox(ReferenceForm.Controls.GetEnumerator());
			EditFreeFields.CheckFreeFields(-1,CRMTables.Base_Contacts,UC);
			FillRepCategories();
			visContact.Header =Root.rm.GetString("CRMrubtxt1");
			Tabber.Selected = visContact.ID;
			Tabber.EditTab="visContact";
			if (UC.InsertGroups.Length > 0) Groups.SetGroups(UC.InsertGroups);
			othercompaniestebles.Text=CreateOtherCompanies(string.Empty);

			this.SheetP.Visible = false;
		}


		private void BtnSearch_Click(object sender, EventArgs e)
		{
			SearchClick(((LinkButton) sender).ID);
			DeleteGoBack(true);
		}

		private void SearchClick(string sender)
		{
			Session["searchsender"] = sender;
			string queryType2 = String.Empty;
			string queryGroup = GroupsSecure("Base_Contacts.Groups");
			string findString = DatabaseConnection.FilterInjection(Search.Text);

			switch (Request.Form["visualizationType"])
			{
				case "0":
				case "1":
					queryType2 = string.Empty;
					break;
				case "2":
					queryType2 = " AND ((BASE_CONTACTS.FLAGGLOBALORPERSONAL<>2) AND (BASE_CONTACTS.GROUPS LIKE '%|" + UC.UserGroupId.ToString() + "|%'))";
					break;
			}
			StringBuilder sqlString = new StringBuilder();


            sqlString.Append("SELECT BASE_CONTACTS.ID,BASE_CONTACTS.NAME, BASE_CONTACTS.SURNAME, BASE_CONTACTS.PHONE_1, BASE_CONTACTS.MOBILEPHONE_1, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2, BASE_COMPANIES.ID AS COMPANYID,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS NAMEOWNER, BASE_CONTACTS.SALESPERSONID, BASE_CONTACTS.OWNERID ");
			sqlString.Append("FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ON BASE_CONTACTS.OWNERID = ACCOUNT.UID ");
			sqlString.AppendFormat("WHERE BASE_CONTACTS.LIMBO=0 AND (BASE_CONTACTS.SURNAME LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.NAME LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.PHONE_1 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.PHONE_2 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.MOBILEPHONE_1 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.MOBILEPHONE_2 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.EMAIL LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.CITY_1 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.CITY_2 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.PROVINCE_1 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("BASE_CONTACTS.PROVINCE_2 LIKE '%{0}%' OR ", findString);
			sqlString.AppendFormat("Base_Contacts.Notes like '%{0}%' OR ", findString);
            sqlString.AppendFormat("Base_Contacts.CODE like '%{0}%' OR ", findString);
			sqlString.AppendFormat("Base_Companies.CompanyName like '%{0}%') ", findString);
			sqlString.Append(queryType2);


			if (sender == "btnGroup")
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
							if (ut.Length > 0) qGroup += "BASE_CONTACTS.GROUPS LIKE '%|" + ut + "|%' OR ";
						}
						if (qGroup.Length > 0) qGroup = qGroup.Substring(0, qGroup.Length - 3);
						sqlString.AppendFormat(" AND ({0})", qGroup);

					}
					else
					{
						sqlString.AppendFormat(" AND (BASE_CONTACTS.GROUPS LIKE '%|{0}|%')", ListGroups.SelectedItem.Value);

					}
				}
				if (this.ListCategory.SelectedIndex > 0)
				{
					sqlString.AppendFormat(" AND (BASE_CONTACTS.CATEGORIES LIKE '%|{0}|%')", ListCategory.SelectedValue);

				}
			}
			else
			{
				if (queryGroup.Length > 0)
				{
					sqlString.AppendFormat(" AND ({0})", queryGroup);

				}
			}

			if(UC.Zones.Length>0)
				sqlString.AppendFormat(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE"));

			this.FillSearchListRepeater(true,sqlString.ToString());

			ViewForm.Visible = false;
			ReferenceForm.Visible = false;
			Tabber.Visible = false;
		}

		private void ReferenceFormSubmit(object sender, EventArgs e)
		{
			if (Referring_Surname.Text.Length > 0)
			{
				ModifyForm(int.Parse(Referring_ID.Text));
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

		private void initNewRepeater()
		{
			this.SearchListRepeater.UsePagedDataSource=true;
			this.SearchListRepeater.PageSize=UC.PagingSize;
		}

		private void FillSearchListRepeater(bool bind, string query)
		{
			StringBuilder sqlString = new StringBuilder();
			if(query.Length==0)
			{
				string queryType = " AND ((BASE_CONTACTS.FLAGGLOBALORPERSONAL=2 AND  BASE_CONTACTS.OWNERID=" + UC.UserId.ToString() + ") OR (BASE_CONTACTS.FLAGGLOBALORPERSONAL<>2))";
				string queryGroup = GroupsSecure("BASE_CONTACTS.GROUPS");

                sqlString.AppendFormat("SELECT BASE_CONTACTS.ID,BASE_CONTACTS.NAME, BASE_CONTACTS.SURNAME, BASE_CONTACTS.PHONE_1, BASE_CONTACTS.MOBILEPHONE_1, BASE_COMPANIES.ID AS COMPANYID, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS NAMEOWNER, BASE_CONTACTS.SALESPERSONID, BASE_CONTACTS.OWNERID ", DatabaseConnection.MaxResult);
				sqlString.Append("FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ");
				sqlString.Append("LEFT OUTER JOIN ACCOUNT ON BASE_CONTACTS.OWNERID = ACCOUNT.UID ");
			sqlString.AppendFormat("WHERE (BASE_CONTACTS.LIMBO = 0) {0}", queryType);
				if (queryGroup.Length > 0)
				{
					sqlString.AppendFormat(" AND ({0})", queryGroup);
				}

				if(UC.Zones.Length>0)
					sqlString.AppendFormat(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE"));

			}
			else
				sqlString.AppendFormat("{0}",query);

			sqlString.Append(" ORDER BY BASE_CONTACTS.SURNAME");
			this.SearchListRepeater.sqlDataSource=sqlString.ToString();

			if(bind)
				this.SearchListRepeater.DataBind();

			this.SearchListRepeater.Visible=true;

			InitSheetPaging(sqlString.ToString());
		}


		private void InitAjax()
		{
			Manager.Register(this, "Ajax.Contacts", Debug.None);
		}

		[Method]
		public string CheckDuplicatedContacts(string name, string surname)
		{
			if (name.Length == 0)
				return DatabaseConnection.SqlScalar("SELECT NAME+'|'+SURNAME AS RET FROM BASE_CONTACTS WHERE SURNAME = '" + DatabaseConnection.FilterInjection(surname) + "'");
			else
				return DatabaseConnection.SqlScalar("SELECT NAME+'|'+SURNAME AS RET FROM BASE_CONTACTS WHERE NAME =  '" + DatabaseConnection.FilterInjection(name) + "' AND SURNAME = '" + DatabaseConnection.FilterInjection(surname) + "'");
		}




		private void Repeater1_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
                case "ViewCpn":
                    Session["openId"]=int.Parse(((Label)e.Item.FindControl("cnpID")).Text);
                   Response.Redirect("/CRM/CRM_Companies.aspx?m=25&dgb=1&si=29");
                    break;
                case "View":
					if (this.SheetP.IDArray.Length > 0)
					{
						string[] tempArray = this.SheetP.IDArray;
						for (int i = 0; i < tempArray.Length; i++)
						{
							if (tempArray[i].Split('|')[0].CompareTo(((Label) e.Item.FindControl("ID")).Text) == 0)
							{
								this.SheetP.CurrentPosition = i;
								this.SheetP.enabledisable();
								break;
							}
						}
					}
					FillView(int.Parse(((Label) e.Item.FindControl("ID")).Text));
					break;
				case "MultiDeleteButton":
					DeleteChecked.MultiLimbo(this.SearchListRepeater.MultiDeleteListArray,"BASE_Contacts",UC.UserId);
					this.SearchListRepeater.DataBind();
					break;

			}
		}

		private void FillView(int id)
		{
			Session["CurrentRefId"] = id;
            string sqlString = "SELECT BASE_CONTACTS.*, (SALES.NAME+' '+SALES.SURNAME) AS SALESPERSONNAME, SALES.SELFCONTACTID, (MODBY.NAME+' '+MODBY.SURNAME) AS LASTMODIFIEDBY, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2, BASE_COMPANIES.PHONE AS AZPHONE ";
			sqlString += "FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ";
			sqlString += "LEFT OUTER JOIN ACCOUNT AS MODBY ON BASE_CONTACTS.LASTMODIFIEDBYID = MODBY.UID ";
            sqlString += "LEFT OUTER JOIN ACCOUNT AS SALES ON BASE_CONTACTS.SALESPERSONID = SALES.UID ";

			DataSet ds = DatabaseConnection.CreateDataset(sqlString + " WHERE BASE_CONTACTS.ID=" + id);
            Recent.AddItem(UC.UserId, (ds.Tables[0].Rows[0]["NAME"].ToString().Length>0)?ds.Tables[0].Rows[0]["NAME"].ToString()+ " ":"" + ds.Tables[0].Rows[0]["SURNAME"].ToString(), id, RecentType.Company, RecentMode.Read);
            ViewForm.DataSource = ds;
			ViewForm.DataBind();

			Tabber.Visible = true;
			ViewForm.Visible = true;

			SearchListRepeater.Visible = false;
			ReferenceForm.Visible = false;

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

			if(visActivity.IsSelected==false && visDocuments.IsSelected==false && visQuote.IsSelected==false)
				Tabber.Selected = visContact.ID;
		}

		private void ViewFormOnItemDatabound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

                    ((FreeFields)e.Item.FindControl("ViewFreeFields")).ViewFreeFields(Convert.ToInt32(DataBinder.Eval((DataRowView)e.Item.DataItem, "id")), CRMTables.Base_Contacts, UC);


					if (DataBinder.Eval((DataRowView) e.Item.DataItem, "FromLead") != DBNull.Value)
					{
						Literal LeadInfo = (Literal) e.Item.FindControl("LeadInfo");
						LeadInfo.Text = "<img src=/i/lookup.gif alt=\"" +Root.rm.GetString("Ledtxt28") + "\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/LeadInfo.aspx?render=no&si=31&id=" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "FromLead")) + "',event,380,350)\">";
					}


					LinkButton ModCon = (LinkButton) e.Item.FindControl("ModCon");
					ModCon.Text =Root.rm.GetString("Reftxt42");

					LinkButton BackToSearch = (LinkButton) e.Item.FindControl("BackToSearch");
					BackToSearch.Text =Root.rm.GetString("CRMrubtxt6");


					LinkButton PrintButton = (LinkButton) e.Item.FindControl("PrintButton");
					PrintButton.Text = "<img src=/i/printer.gif border=0>";
					PrintButton.Attributes.Add("onclick", "window.open('/report/htmlreport.aspx');");

					Session["report"] = null;
					ArrayList report = new ArrayList();
					CompanyReport cr = new CompanyReport();
					cr.idfield = Convert.ToInt32(DatabaseConnection.SqlScalartoObj("SELECT ID FROM QB_CUSTOMERQUERY WHERE TITLE='Fixed2'")); //ex 42;
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
					FillFileRep(Convert.ToInt32(DataBinder.Eval((DataRowView) e.Item.DataItem, "id")));
					string azPhone = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "AzPhone"));
					if (azPhone.Length > 0)
					{
						Literal AZph = (Literal) e.Item.FindControl("CompanyPhone");
						AZph.Text = azPhone;
					}

					Literal Title = (Literal) e.Item.FindControl("Title");
					Title.Text = (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Title")).Length > 0) ? Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Title")) + "&nbsp;" : "";

					LinkButton CompanyLink = (LinkButton) e.Item.FindControl("CompanyLink");
					CompanyLink.Text = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName2"));

					LinkButton CompanyContacts = (LinkButton) e.Item.FindControl("CompanyContacts");
					CompanyContacts.Text =Root.rm.GetString("Reftxt55");
					CompanyContacts.Visible = (CompanyLink.Text.Length > 0);

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



					if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "OtherCompanyID")).Length > 0)
					{
						string[] comp = ((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "OtherCompanyID")).Split('|');
						string queryID = String.Empty;
						foreach (string ca in comp)
						{
							if (ca.Length > 0) queryID += " id=" + int.Parse(ca) + " or ";
						}
						queryID = queryID.Substring(0, queryID.Length - 4);
						Repeater RepOtherCompanies = (Repeater) e.Item.FindControl("RepOtherCompanies");
						RepOtherCompanies.DataSource = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,ID FROM BASE_COMPANIES WHERE (" + queryID + ")");
						RepOtherCompanies.DataBind();
					}


					LinkButton BtnMailAuth = (LinkButton) e.Item.FindControl("BtnMailAuth");
					BtnMailAuth.Text = "<img src=/images/email.gif alt='" +Root.rm.GetString("Bcotxt55") + "' border=0>";

					BtnMailAuth.Visible = !(bool) DataBinder.Eval((DataRowView) e.Item.DataItem, "MLFlag");

					Literal Sexlbl = (Literal) e.Item.FindControl("Sexlbl");

					if (DataBinder.Eval((DataRowView) e.Item.DataItem, "Sex") == DBNull.Value)
						Sexlbl.Text =Root.rm.GetString("Bcotxt60");
					else if ((bool) DataBinder.Eval((DataRowView) e.Item.DataItem, "Sex"))
						Sexlbl.Text = "M";
					else
						Sexlbl.Text = "F";

					Literal VoipCallPhone_1 = (Literal) e.Item.FindControl("VoipCallPhone_1");
					Literal VoipCallPhone_2 = (Literal) e.Item.FindControl("VoipCallPhone_2");
					Literal VoipCallMobilePhone_1 = (Literal) e.Item.FindControl("VoipCallMobilePhone_1");
					Literal VoipCallMobilePhone_2 = (Literal) e.Item.FindControl("VoipCallMobilePhone_2");

						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_1")).ToString().Length > 0)
						{
							string ph = StaticFunctions.FixPhoneNumber((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_1"));
							VoipCallPhone_1.Text = MakeVoipString(ph);
						}
						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_2")).ToString().Length > 0)
						{
							string ph = StaticFunctions.FixPhoneNumber((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_2"));
							VoipCallPhone_2.Text = MakeVoipString(ph);
						}
						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_1")).ToString().Length > 0)
						{
							string ph = StaticFunctions.FixPhoneNumber((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_1"));
							VoipCallMobilePhone_1.Text = MakeVoipString(ph);
						}
						if ((DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_2")).ToString().Length > 0)
						{
							string ph = StaticFunctions.FixPhoneNumber((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_2"));
							VoipCallMobilePhone_2.Text = MakeVoipString(ph);
						}


					Literal BirthDay = (Literal) e.Item.FindControl("BirthDay");
					string birth = (DataBinder.Eval((DataRowView) e.Item.DataItem, "Birthday")).ToString();
					if (birth.Length > 0)
					{
						if (Convert.ToDateTime(birth).Year > 1900)
							BirthDay.Text = birth.Substring(0, 10);
						else
							BirthDay.Text = birth.Substring(0, 5);
					}
					else
						BirthDay.Text = "";

					long idzone =(Int64)DataBinder.Eval((DataRowView) e.Item.DataItem, "CommercialZone");
					if(idzone!=0)
					{
						Literal lblZone = (Literal)e.Item.FindControl("lblZone");
						lblZone.Text=DatabaseConnection.SqlScalar(string.Format("SELECT DESCRIPTION FROM ZONES WHERE ID={0}",idzone));
					}

                    if(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID")!= System.DBNull.Value)
                        if (Convert.ToInt64(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID")) > 0)
                        {
                            string selfcontact = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID"));
                            Literal litViewSalesContact = (Literal)e.Item.FindControl("litViewSalesContact");
                            litViewSalesContact.Text = string.Format("&nbsp;<img src=\"/i/lens.gif\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/ViewContact.aspx?render=no&id={0}',event,500,300);\">", selfcontact);
                        }
					break;
			}
		}

		private void FilldropZones()
		{
			dropZones.DataValueField="id";
			dropZones.DataTextField="description";
			dropZones.DataSource=DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM ZONES ORDER BY VIEWORDER");
			dropZones.DataBind();
			dropZones.Items.Insert(0,new ListItem(Root.rm.GetString("Choose"),"0"));
		}

		private void ViewForm_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "ModCon":
					FillForm(int.Parse(((Literal) e.Item.FindControl("IdRef")).Text));

					string strScript = "<SCRIPT>" + Environment.NewLine +
						"needsave('" +Root.rm.GetString("CRMcontxt77") + "');" + Environment.NewLine +
						"</SCRIPT>";

					ClientScript.RegisterStartupScript(this.GetType(), "anything", strScript);

					SubmitRef.Attributes.Add("onclick", "needsave('no')");
					Submit2.Attributes.Add("onclick", "needsave('no')");
					RefreshRepCategories.Attributes.Add("onclick", "needsave('no')");

					ViewForm.Visible = false;
					SearchListRepeater.Visible = false;
					ReferenceForm.Visible = true;


					Tabber.EditTab = "visContact";
					Tabber.Selected = visContact.ID;

					SheetP.Visible = false;
					break;
				case "CompanyContacts":
					Literal CompanyIdForLinkC = (Literal) e.Item.FindControl("CompanyIdForLink");
					SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] {CompanyIdForLinkC.Text, "c"});
					Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
					break;
				case "CompanyLink":
					Literal CompanyIdForLink = (Literal) e.Item.FindControl("CompanyIdForLink");
					SetGoBack("/crm/base_contacts.aspx?m=25&si=31&gb=1", new string[] {CompanyIdForLink.Text, "|d|"});
					Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
					break;
				case "BackToSearch":
					Tabber.Visible = false;
					SearchListRepeater.Visible = true;

					break;
				case "BtnMailAuth":
					TemplateAdmin ta = new TemplateAdmin();
					ta.TemplateName = "MailAuthContacts";
					ta.Language = UC.CultureSpecific;
					ta.ApplicationPath = Request.PhysicalApplicationPath;
					string template = ta.GetTemplate();
					if (template == "0" || template == "1")
					{
						ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" +Root.rm.GetString("Bcotxt58") + "');</script>");
					}
					else
					{
						Guid gu = Guid.NewGuid();
						template = template.Replace("[guid]", gu.ToString());
						template = template.Replace("[Tustena.Account]", UC.UserRealName);
						template = template.Replace("[Tustena.CompanyName]", DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM TUSTENA_DATA"));
						template = template.Replace("[Tustena.Email]", ((Literal) e.Item.FindControl("LtrEmail")).Text);
						DatabaseConnection.DoCommand("INSERT INTO ML_AUTH (ID,TABLEID,FIELDID) VALUES ('" + gu.ToString() + "',1," + ((Literal) e.Item.FindControl("IdRef")).Text + ")");
						MessagesHandler.SendMail(((Literal) e.Item.FindControl("LtrEmail")).Text, UC.MailingAddress,
						         "[Tustena] " +Root.rm.GetString("Bcotxt56"), template);
						ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" +Root.rm.GetString("Bcotxt57") + "');</script>");
					}
					break;
			}
		}



		private void FillForm(int id)
		{
			DataSet ds;
			DataRow myDataRow;
			string sqlString;
			sqlString = "SELECT BASE_CONTACTS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2 ";
			sqlString += "FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ";
			sqlString += "WHERE BASE_CONTACTS.ID=" + id;
			ds = DatabaseConnection.CreateDataset(sqlString);
			if (ds.Tables[0].Rows.Count > 0)
			{
				myDataRow = ds.Tables[0].Rows[0];

				Referring_ID.Text = myDataRow["Id"].ToString();
				Referring_Name.Text = myDataRow["Name"].ToString();
				Referring_Surname.Text = myDataRow["Surname"].ToString();
				Referring_Title.Text = myDataRow["Title"].ToString();
				Referring_Address_1.Text = myDataRow["Address_1"].ToString();
				Referring_Address_2.Text = myDataRow["Address_2"].ToString();
				Referring_City_1.Text = myDataRow["City_1"].ToString();
				Referring_City_2.Text = myDataRow["City_2"].ToString();
				Referring_Province_1.Text = myDataRow["Province_1"].ToString();
				Referring_Province_2.Text = myDataRow["Province_2"].ToString();
				Referring_ZIPCode_1.Text = myDataRow["ZIPCode_1"].ToString();
				Referring_ZIPCode_2.Text = myDataRow["ZIPCode_2"].ToString();
				Referring_State_1.Text = myDataRow["State_1"].ToString();
				Referring_State_2.Text = myDataRow["State_2"].ToString();
				Referring_VatID.Text = myDataRow["VatID"].ToString();
				Referring_TaxIdentificationNumber.Text = myDataRow["TaxIdentificationNumber"].ToString();
				Referring_EMail.Text = myDataRow["EMail"].ToString();
                Referring_CODE.Text = myDataRow["CODE"].ToString();
				Referring_Phone_1.Text = myDataRow["Phone_1"].ToString();
				Referring_Phone_2.Text = myDataRow["Phone_2"].ToString();
                Referring_Skype.Text = myDataRow["Skype"].ToString();
                Referring_Fax.Text = myDataRow["Fax"].ToString();
                Referring_MobilePhone_1.Text = myDataRow["MobilePhone_1"].ToString();
				Referring_MobilePhone_2.Text = myDataRow["MobilePhone_2"].ToString();
				Referring_BusinessRole.Text = myDataRow["BusinessRole"].ToString();
				Referring_Notes.Text = myDataRow["Notes"].ToString();

                if (myDataRow["SalesPersonID"] != System.DBNull.Value && myDataRow["SalesPersonID"].ToString().Length > 0)
                {
                    SalesPersonID.Text = myDataRow["SalesPersonID"].ToString();
                    SalesPerson.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM ACCOUNT WHERE UID=" + myDataRow["SalesPersonID"].ToString());
                }
				Groups.SetGroups(myDataRow["Groups"].ToString());

				othercompaniestebles.Text=CreateOtherCompanies(myDataRow["OtherCompanyID"].ToString());

				if (myDataRow["Birthday"].ToString().Length > 0)
				{
					if (Convert.ToDateTime(myDataRow["Birthday"]).Year > 1900)
						Referring_BirthDay.Text = myDataRow["Birthday"].ToString().Substring(0, 10);
					else
						Referring_BirthDay.Text = myDataRow["Birthday"].ToString().Substring(0, 5);
				}
				else
					Referring_BirthDay.Text = "";

				Referring_BirthPlace.Text = myDataRow["BirthPlace"].ToString();
				Referring_Categories.Text = myDataRow["Categories"].ToString();
				Referring_MLEmail.Text = myDataRow["MLEmail"].ToString();
				Referring_MLFlag.Checked = (bool) myDataRow["MLFlag"];
				if (myDataRow["Sex"] != DBNull.Value)
					if ((bool) myDataRow["Sex"])
						Referring_Sex.Items[0].Selected = true;
					else
						Referring_Sex.Items[1].Selected = true;
				Referring_CompanyTX.Text = myDataRow["CompanyName2"].ToString();
				Referring_CompanyID.Text = myDataRow["CompanyID"].ToString();

				dropZones.SelectedItem.Selected = false;
				foreach (ListItem li in dropZones.Items)
				{
					if (li.Value == myDataRow["CommercialZone"].ToString())
					{
						li.Selected = true;
						break;
					}
				}

				switch (Convert.ToInt16(myDataRow["FlagGlobalORPersonal"]))
				{
					case 0:
						Personal.Checked = false;
						Global.Checked = false;
						break;
					case 1:
						Personal.Checked = false;
						Global.Checked = true;
						break;
					case 2:
						Personal.Checked = true;
						Global.Checked = false;
						break;
				}

				FillRepCategories();
                EditFreeFields.CheckFreeFields(id, CRMTables.Base_Contacts, UC);
				AddKeepAlive();
			}
			else
				HackLock("Rubrica:" + UC.UserId + ">" + id);

		}


		private void ModifyForm(int id)
		{
			using (DigiDapter dg = new DigiDapter("SELECT ID FROM BASE_CONTACTS WHERE ID=" + id))
			{
				if (!dg.HasRows)
				{
					dg.Add("OWNERID", UC.UserId, 'I');
				}

				dg.Add("COMPANYID", (Referring_CompanyID.Text.Length > 0) ? int.Parse(Referring_CompanyID.Text) : 0);
				dg.Add("NAME", Referring_Name.Text);
				dg.Add("SURNAME", Referring_Surname.Text);
				dg.Add("TITLE", Referring_Title.Text);
				dg.Add("ADDRESS_1", Referring_Address_1.Text);
				dg.Add("ADDRESS_2", Referring_Address_2.Text);
				dg.Add("CITY_1", Referring_City_1.Text);
				dg.Add("CITY_2", Referring_City_2.Text);
				dg.Add("PROVINCE_1", Referring_Province_1.Text);
				dg.Add("PROVINCE_2", Referring_Province_2.Text);
				dg.Add("ZIPCODE_1", Referring_ZIPCode_1.Text);
				dg.Add("ZIPCODE_2", Referring_ZIPCode_2.Text);
				dg.Add("STATE_1", Referring_State_1.Text);
				dg.Add("STATE_2", Referring_State_2.Text);
				dg.Add("VATID", Referring_VatID.Text);
				dg.Add("TAXIDENTIFICATIONNUMBER", Referring_TaxIdentificationNumber.Text);
				dg.Add("EMAIL", Referring_EMail.Text);
				dg.Add("PHONE_1", Referring_Phone_1.Text);
				dg.Add("PHONE_2", Referring_Phone_2.Text);
                dg.Add("SKYPE", Referring_Skype.Text);
                dg.Add("FAX", Referring_Fax.Text);
                dg.Add("MOBILEPHONE_1", Referring_MobilePhone_1.Text);
				dg.Add("MOBILEPHONE_2", Referring_MobilePhone_2.Text);
				dg.Add("BUSINESSROLE", Referring_BusinessRole.Text);
				dg.Add("NOTES", Referring_Notes.Text, false);

                if (id == -1)
                {
                    Object progressive;
                    progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS WHERE TYPE=2");
                    if (progressive != null && !((bool)progressive))
                    {
                        DataRow drprog;
                        drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=2").Tables[0].Rows[0];
                        string newprog = string.Empty;

                        if (Convert.ToInt32(drprog["NPROGRESTART"]) > 0)
                            if (DateTime.Now >= new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) && new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) >= (DateTime)drprog["LASTRESET"])
                            {
                                DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROGSTART,LASTRESET=GETDATE() WHERE TYPE=2");
                                drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=2").Tables[0].Rows[0];
                            }
                        newprog += (((int)drprog["NPROG"]) + 1).ToString();
                        if ((bool)drprog["CHECKDAY"])
                            newprog += "-" + DateTime.Now.Day.ToString();
                        if ((bool)drprog["CHECKMONTH"])
                            newprog += "-" + DateTime.Now.Month.ToString();
                        if ((bool)drprog["CHECKYEAR"])
                        {
                            if ((bool)drprog["TWODIGITYEAR"])
                                newprog += "-" + DateTime.Now.ToString("yyyy");
                            else
                                newprog += "-" + DateTime.Now.ToString("yy");
                        }



                        this.Referring_CODE.Text = newprog;
                        DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROG+1 WHERE TYPE=2");
                        }
                }

                dg.Add("CODE", Referring_CODE.Text);


                if (SalesPersonID.Text.Length > 0)
                    dg.Add("SALESPERSONID", SalesPersonID.Text);

				if(dropZones.SelectedIndex>0)
					dg.Add("COMMERCIALZONE",dropZones.SelectedValue);
				else
					dg.Add("COMMERCIALZONE",0);

				if (Referring_BirthDay.Text.Length > 0)
					try
					{
						if (Referring_BirthDay.Text.Length == 10)
						{
							DateTime birth = Convert.ToDateTime(Referring_BirthDay.Text, UC.myDTFI);
							if (birth >= new DateTime(1753, 1, 1) && birth <= new DateTime(9999, 12, 31))
								dg.Add("BIRTHDAY", birth);
						}
						else
						{
							if (Referring_BirthDay.Text.Length == 5) Referring_BirthDay.Text += "/1900";
							if (Referring_BirthDay.Text.Length == 6) Referring_BirthDay.Text += "1900";
							DateTime birth = Convert.ToDateTime(Referring_BirthDay.Text, UC.myDTFI);
							if (birth >= new DateTime(1753, 1, 1) && birth <= new DateTime(9999, 12, 31))
								dg.Add("BIRTHDAY", birth);
						}
					}
					catch
					{
					}
				dg.Add("BIRTHPLACE", Referring_BirthPlace.Text);
				StringBuilder cat = new StringBuilder("|");
				foreach (RepeaterItem it in RepCategories.Items)
				{
					CheckBox Check = (CheckBox) it.FindControl("Check");
					if (Check.Checked)
						cat.AppendFormat("{0}|", ((Literal) it.FindControl("IDCat")).Text);
				}
				if (cat.Length > 1)
					dg.Add("CATEGORIES", cat.ToString());
				else
					dg.Add("CATEGORIES", DBNull.Value);

				if (Groups.GetValue.Length > 0)
					dg.Add("GROUPS", Groups.GetValue);
				else
					dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");

				dg.Add("MLEMAIL", Referring_MLEmail.Text);
				dg.Add("MLFLAG", Referring_MLFlag.Checked);
				RadioButtonList Sex = Referring_Sex;
				if (Sex.Items[0].Selected || Sex.Items[1].Selected)
					if (Sex.Items[0].Selected)
						dg.Add("SEX", true);
					else
						dg.Add("SEX", false);
				dg.Add("CREATEDBYID", UC.UserId);

				int globpers = 0;
				if (Global.Checked && !Personal.Checked) globpers = 1;
				if (!Global.Checked && Personal.Checked) globpers = 2;
				dg.Add("FLAGGLOBALORPERSONAL", globpers);

				if (dg.HasRows)
				{
					dg.Add("LASTACTIVITY", 1); //MODIFICA
					dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
					dg.Add("LASTMODIFIEDBYID", UC.UserId);
				}

					string otherCompanies = string.Empty;
					if(Request["newothercompaniesID"]!=null && Request["newothercompaniesID"].Length>0)
						otherCompanies="|"+Request["newothercompaniesID"];
					int other=1;
					while(Request["newothercompaniesID_"+other]!=null)
					{
						if(Request["newothercompaniesID_"+other].Length>0)
							otherCompanies+="|"+Request["newothercompaniesID_"+other];
						other++;
					}
					if(otherCompanies.Length>0)
					{
						dg.Add("OTHERCOMPANYID", otherCompanies+"|");
					}

				object newId = dg.Execute("BASE_CONTACTS", "ID=" + id, DigiDapter.Identities.Identity);

				if (id == -1)
				{
					int newIdInt = int.Parse(newId.ToString());
                    EditFreeFields.FillFreeFields(newIdInt, CRMTables.Base_Contacts, UC);
					DatabaseConnection.CommitTransaction();
					FillView(newIdInt);
				}
				else
				{
                    EditFreeFields.FillFreeFields(id, CRMTables.Base_Contacts, UC);
					DatabaseConnection.CommitTransaction();
					FillView(id);
				}

			} // il dispose distrugge oggetto e chiude le connessioni
			visContact.Header =Root.rm.GetString("CRMrubtxt1");
		}

		private void NewActivity_Click(Object sender, EventArgs e)
		{
			Session["AcContactID"] = Session["CurrentRefId"].ToString();
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

		private void btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "NewDoc":
					SetGoBack("/crm/base_contacts.aspx?m=25&si=31&gb=1", new string[2] {Session["CurrentRefId"].ToString(), "d"});
					Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=r");
					break;
				case "RefreshRepCategories":
					FillRepCategories();
					ClientScript.RegisterClientScriptBlock(this.GetType(), "initJS", "<script>g_fFieldsChanged=1;</script>");
					break;
			}
		}


		private void RapSubmit_Click(Object sender, EventArgs e)
		{
			if (RapRagSoc.Text.Length > 0)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("CREATEDBYID", UC.UserId);

					dg.Add("COMPANYNAME", RapRagSoc.Text);
					dg.Add("COMPANYNAMEFILTERED", StaticFunctions.FilterSearch(RapRagSoc.Text));
					dg.Add("PHONE", RapPhone.Text);
					dg.Add("EMAIL", RapEmail.Text.Trim(' '));
					dg.Add("OWNERID", UC.UserId);
					dg.Add("GROUPS", "|" + UC.AdminGroupId + "|" + UC.UserGroupId + "|");

					object newId = dg.Execute("BASE_COMPANIES", "ID=-1", DigiDapter.Identities.Identity);

					SetGoBack("/crm/crm_companies.aspx?m=25&si=29&gb=1", new string[2] {newId.ToString(), "d"});
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
			AcCrono.AcType = (byte) AType.Contacts;


			AcCrono.FromSheet = "c";
			AcCrono.Refresh();

			if (AcCrono.ItemCount > 0)
				AcCrono.Visible = true;
			else
			{
				AcCrono.Visible = false;
				RepeaterActivityInfo.Text =Root.rm.GetString("CRMcontxt24");
			}
		}


		private void FileRepCommand(object source, RepeaterCommandEventArgs e)
		{
			Trace.Warn(e.CommandName);
			Literal FileId = (Literal) e.Item.FindControl("FileId");
			switch (e.CommandName)
			{
				case "Down":

					DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileId.Text));


					string filename;
					filename = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"];
					string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();
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


					break;
				case "Modify":
				case "Revision":
					Session["IdDoc"] = FileId.Text;
					SetGoBack("/crm/base_contacts.aspx?m=25&si=31&gb=1", new string[2] {Session["CurrentRefId"].ToString(), "d"});
					if (e.CommandName == "Modify")
						Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=m");
					else
						Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=e");
					break;
				case "ReviewNumber":
					Session["review"] = "1";

					int fileId = int.Parse(((Literal) e.Item.FindControl("FileId")).Text);

					string sqlString = "SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ";
					sqlString += "LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ";
					sqlString += "LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ";
					sqlString += "WHERE ((FILEMANAGER.OWNERID=" + UC.UserId + " OR " + GroupsSecure("FILEMANAGER.GROUPS") + ") ";
					sqlString += "AND (FILEMANAGER.ID=" + fileId + " OR FILEMANAGER.ISREVIEW=" + fileId + ")) ";
					sqlString += "ORDER BY REVIEWNUMBER DESC";

					FileRep.DataSource = DatabaseConnection.CreateDataset(sqlString);
					FileRep.DataBind();
					break;
			}
		}

		private void FileRepDatabound(Object sender, RepeaterItemEventArgs e)
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

					LinkButton Down = (LinkButton) e.Item.FindControl("Down");
					string ext = Path.GetExtension(Down.Text);
					Image FileImg = (Image) e.Item.FindControl("FileImg");
					FileImg.ImageUrl = FileFunctions.GetFileImg(ext);
					break;
			}
		}


		private void FillFileRep(int id)
		{
			string sqlString = "SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ";
			sqlString += "LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ";
			sqlString += "LEFT OUTER JOIN FILECROSSTABLES ON FILEMANAGER.ID=FILECROSSTABLES.IDFILE ";
			sqlString += "LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ";
			sqlString += "WHERE ISREVIEW=0 AND FILECROSSTABLES.TABLENAME='BASE_CONTACTS' AND FILECROSSTABLES.IDRIF=" + id;

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


        private void FillRepCategories()
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) ORDER BY FLAGPERSONAL DESC").Tables[0];
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (Referring_Categories.Text.IndexOf("|" + dr["id"].ToString() + "|") > -1)
						dr["tocheck"] = "1";
				}
			}
			RepCategories.DataSource = new DataView(dt, "", "tocheck desc", DataViewRowState.CurrentRows);

			RepCategories.DataBind();
		}

		private void RepCategories_DataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal IDCat = (Literal) e.Item.FindControl("IDCat");
					if (Referring_Categories.Text.IndexOf("|" + IDCat.Text + "|") > -1)
					{
						CheckBox Check = (CheckBox) e.Item.FindControl("Check");
						Check.Checked = true;
					}

					DataSet ds = DatabaseConnection.CreateDataset("SELECT ID FROM BASE_CONTACTS WHERE CATEGORIES LIKE '%|" + IDCat.Text + "|%'");
					if (ds.Tables[0].Rows.Count < 1)
					{
						LinkButton DeleteCat = (LinkButton) e.Item.FindControl("DeleteCat");
						DeleteCat.Text = "<img src='/i/erase.gif' border=0 alt='" +Root.rm.GetString("CRMcontxt49") + "'>";
						DeleteCat.Attributes.Add("onclick", "g_fFieldsChanged=0;return confirm('" +Root.rm.GetString("CRMcontxt50") + "');");
					}
					break;
			}
		}

		private void RepCategories_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DeleteCat":
					string count = "SELECT COUNT(*) FROM BASE_CONTACTS WHERE CATEGORIES LIKE '|" + ((Literal) e.Item.FindControl("IDCat")).Text + "|'";

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

		protected bool NoLength(string tx)
		{
			return false;
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
			this.SheetP.NextClick += new EventHandler(SheetP_NextClick);
			this.SheetP.PrevClick += new EventHandler(SheetP_PrevClick);
			this.BtnNew.Click += new EventHandler(this.NewContact);
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
			this.NewDoc.Click += new EventHandler(this.btn_Click);
			this.ViewForm.ItemCommand += new RepeaterCommandEventHandler(this.ViewForm_Command);
			this.RepCategories.ItemCommand += new RepeaterCommandEventHandler(this.RepCategories_Command);
			this.FileRep.ItemCommand += new RepeaterCommandEventHandler(this.FileRepCommand);

			this.ViewForm.ItemDataBound += new RepeaterItemEventHandler(this.ViewFormOnItemDatabound);
			this.RepCategories.ItemDataBound += new RepeaterItemEventHandler(this.RepCategories_DataBound);
			this.FileRep.ItemDataBound += new RepeaterItemEventHandler(this.FileRepDatabound);
			this.SearchListRepeater.ItemCommand += new RepeaterCommandEventHandler(this.Repeater1_Command);
            this.SearchListRepeater.ItemDataBound += new RepeaterItemEventHandler(SearchListRepeater_ItemDataBound);
			this.Tabber.TabClick+=new TabClickDelegate(Tabber_TabClick);
		}

        void SearchListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    string salesperson = (DataBinder.Eval((DataRowView)e.Item.DataItem, "SalesPersonid")).ToString();
                    string ownerid = (DataBinder.Eval((DataRowView)e.Item.DataItem, "OwnerID")).ToString();
                    if (salesperson.Length > 0 && salesperson != UC.UserId.ToString() && ownerid != UC.UserId.ToString() && UC.AdminGroupId != UC.UserGroupId)
                    {
                        e.Item.Visible = false;
                    }
                    break;
            }
        }

		#endregion

		private void InitSheetPaging(string sqlString)
		{
			DataTable dt = DatabaseConnection.CreateDataset(sqlString).Tables[0];
			string[] myarray = new string[dt.Rows.Count];
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					myarray[i] = dt.Rows[i]["ID"] + "|" + dt.Rows[i]["Surname"] + " " + dt.Rows[i]["Name"];
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

		protected void RepOtherCompanies_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			Literal OtherCompanyIdForLink = (Literal) e.Item.FindControl("OtherCompanyIdForLink");
			SetGoBack("/crm/base_contacts.aspx?m=25&si=31&gb=1", new string[] {OtherCompanyIdForLink.Text, "|d|"});
			Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
		}

		private string CreateOtherCompanies(string CompanyIDs)
		{
			string tabs = "<table id=\"othercompanies{0}\" cellpadding=\"1\" cellspacing=\"0\" border=\"0\" width=\"100%\"><tr><td width=\"95%\"><input ID=\"newothercompanies{0}\" name=\"newothercompanies{0}\" Class=\"BoxDesign\" value=\"{1}\"><input ID=\"newothercompaniesID{0}\" name=\"newothercompaniesID{0}\" Class=\"BoxDesign\" value=\"{2}\" style=\"display:none\"></td><td nowrap><img src=\"/images/lookup.gif\" border=\"0\" cloneparam1=\"newothercompanies{0}\" cloneparam2=\"newothercompaniesID{0}\" onclick=\"CreateBox('/Common/PopCompany.aspx?render=no&textbox='+this.getAttribute('cloneparam1')+'&textbox2='+this.getAttribute('cloneparam2'),event,500,400)\" style=\"cursor:pointer;\"><img src=\"/i/erase.gif\" border=\"0\" cloneparam1=\"newothercompanies{0}\" cloneparam2=\"newothercompaniesID{0}\" cloneparam3=\"othercompanies{0}\" onclick=\"CleanFields(this.getAttribute('cloneparam1'),this.getAttribute('cloneparam1'));removeCloned(this.getAttribute('cloneparam3'),'othercompaniescontainer')\" style=\"cursor:pointer;\"></td></tr></table>";

			string tables = string.Empty;
			int nextid=0;
			if(CompanyIDs.Length>0)
			{
				string[] comp = CompanyIDs.Split('|');
				string queryID = String.Empty;
				foreach (string ca in comp)
				{
					if (ca.Length > 0) queryID += " ID=" + int.Parse(ca) + " OR ";
				}
				queryID = queryID.Substring(0, queryID.Length - 4);
				DataSet dscompanies;
				dscompanies = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,ID FROM BASE_COMPANIES WHERE (" + queryID + ")");

				for(int i=0;i<dscompanies.Tables[0].Rows.Count;i++)
				{
					if(i==0)
					{
						tables= string.Format(tabs,string.Empty,dscompanies.Tables[0].Rows[i][0].ToString(),dscompanies.Tables[0].Rows[i][1].ToString());
					}else
					{
						tables += string.Format(tabs,"_"+i,dscompanies.Tables[0].Rows[i][0].ToString(),dscompanies.Tables[0].Rows[i][1].ToString());
					}
					nextid=i;
				}

			}else
			{

				tables= string.Format(tabs,string.Empty,string.Empty,string.Empty);
			}

			ClientScript.RegisterClientScriptBlock(this.GetType(), "idc","<script>var idc="+(++nextid)+";</script>");
			return tables;
		}


		private void Tabber_TabClick(string tabId)
		{
			switch(tabId)
			{
				case "visQuote":
					CustomerQuote1.IsQuote=true;
					CustomerQuote1.CustomerData=new string[]{"1",Session["CurrentRefId"].ToString()};
					CustomerQuote1.Bind();
					Customerquote2.IsQuote=false;
					Customerquote2.CustomerData=new string[]{"1",Session["CurrentRefId"].ToString()};
					Customerquote2.Bind();
					Tabber.Selected = visQuote.ID;
					break;
			}
		}
	}
}

