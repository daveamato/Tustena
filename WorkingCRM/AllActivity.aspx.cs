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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.Estimates;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.WorkingCRM
{
	public partial class AllActivity : WorkingCRM
	{

		private int TotalPageDuration;
		private int TotalDuration;

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (!M.IsModule(ActiveModules.Storage))
                EditDocumentModule.Visible = false;


            if (!M.IsModule(ActiveModules.Lead))
            {
                SearchLeadModule.Visible = false;
                EditLeadModule.Visible = false;
                EditOpportunityModule.Visible = false;
            }


            base.OnPreRenderComplete(e);
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Session["backafterlogin"] = "/workingcrm/allactivity.aspx?m=25&si=38";
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				PanelChild.Visible = true;
				MoveLog.Visible = false;
				RepeaterSearchPaging.Visible = false;
				((Label) Page.FindControl("RepeaterSearchInfo")).Visible = false;


				ActQuote.Attributes.Add("onclick","alert('" + Root.rm.GetString("Quotxt37")+"');location.href='/erp/quotelist.aspx?m=67&dgb=1&si=69';return false;");

				if (!Page.IsPostBack)
				{
					FillActivity_Document();

					LinkDocument.Text = "<img src=/i/download.gif alt=\"" +Root.rm.GetString("Acttxt99") + "\" border=0>";
					CheckSendMail.Attributes.Add("onclick", "ChooseMailDest(this,event)");
					CheckSendMail.Text =Root.rm.GetString("Acttxt120");
					SubmitSearch.Text =Root.rm.GetString("Acttxt117");
					BtnSearch.Text =Root.rm.GetString("Acttxt43");
					ActPhone.Text =Root.rm.GetString("Acttxt44");
					ActVisit.Text =Root.rm.GetString("Acttxt45");
					ActEmail.Text =Root.rm.GetString("Acttxt46");
					ActFax.Text =Root.rm.GetString("Acttxt47");
					ActLetter.Text =Root.rm.GetString("Acttxt48");
					ActGeneric.Text =Root.rm.GetString("Acttxt49");
					ActCase.Text =Root.rm.GetString("Acttxt50");
					ActMemo.Text =Root.rm.GetString("Acttxt51");
					ActQuote.Text =Root.rm.GetString("Esttxt13");

					CheckToBill.Text =Root.rm.GetString("Acttxt52");
					CheckCommercial.Text =Root.rm.GetString("Acttxt53");
					CheckTechnical.Text =Root.rm.GetString("Acttxt54");

					Activity_InOut.Items.Add(new ListItem(String.Format("<img src=/i/incoming.gif alt=\"{0}\">",Root.rm.GetString("AcTooltip1")), "1"));
					Activity_InOut.Items.Add(new ListItem(String.Format("<img src=/i/outcoming.gif alt=\"{0}\">",Root.rm.GetString("AcTooltip2")), "0"));
					Activity_InOut.Items[1].Selected = true;

					ChildAction.Text = "<img src=\"/i/newevent.gif\" alt=\"" +Root.rm.GetString("AcTooltip20") + "\" border=0 style=\"cursor:pointer\">";

					Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt71"), "1"));
					Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt72"), "0"));
					Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt103"), "2"));
					Activity_ToDo.SelectedIndex = 1;
					Activity_ToDo.RepeatDirection = RepeatDirection.Horizontal;
					Activity_ToDo.RepeatColumns = 3;

					IMGAvailability.Alt =Root.rm.GetString("Evnttxt42");

					SubmitBtn.Text =Root.rm.GetString("Acttxt87");
					SubmitBtnDoc.Text =Root.rm.GetString("Acttxt88");




					AcPanel.Visible = false;
					SecondDescription.Visible = false;

					if (!isGoBack) Back.Visible = false;


					if (Request["Ac"] != null)
					{
						int ac = int.Parse(Request["Ac"]);
						if (Request["goback"] != null)
						{
							switch (Request["goback"].ToString())
							{
								case "a":
									string companyId = String.Empty;
									companyId = DatabaseConnection.SqlScalar("SELECT COMPANYID FROM CRM_WORKACTIVITY WHERE ID=" + ac);
									if (companyId.Length > 0)
										SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] {companyId, "a"});
									else
									{
										companyId = DatabaseConnection.SqlScalar("SELECT BASE_CONTACTS.COMPANYID FROM CRM_WORKACTIVITY LEFT OUTER JOIN BASE_CONTACTS ON CRM_WORKACTIVITY.REFERRERID = BASE_CONTACTS.ID WHERE CRM_WORKACTIVITY.ID=" + ac);
										if (companyId.Length > 0)
											SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] {companyId, "a"});
									}
									break;
								case "c":
									SetGoBack("/crm/base_contacts.aspx?m=25&si=31&gb=1", new string[] {DatabaseConnection.SqlScalar("SELECT REFERRERID FROM CRM_WORKACTIVITY WHERE ID=" + ac), "a"});
									break;
								case "o":
									SetGoBack("/CRM/CRM_Opportunity.aspx?m=25&si=29&gb=1", new string[] {DatabaseConnection.SqlScalar("SELECT OPPORTUNITYID FROM CRM_WORKACTIVITY WHERE ID=" + ac), "a"});
									break;
								case "l":
									SetGoBack("/crm/CRM_Lead.aspx?m=25&si=53&gb=1", new string[] {DatabaseConnection.SqlScalar("SELECT LEADID FROM CRM_WORKACTIVITY WHERE ID=" + ac), "a"});
									break;
							}
							if (!isGoBack) Back.Visible = false;
							else Back.Visible = true;
						}

						if (Session["SheetP"] != null && Request["Sa"] == "1")
						{
							this.SheetP.IDArray = (string[]) Session["SheetP"];
							if (this.SheetP.IDArray.Length > 0)
							{
								string[] tempArray = this.SheetP.IDArray;

								for (int i = 0; i < tempArray.Length; i++)
								{
									if (tempArray[i].Split('|')[0].CompareTo(ac.ToString()) == 0)
									{
										this.SheetP.CurrentPosition = i;
										this.SheetP.enabledisable();
										break;
									}
								}
							}
							Session.Remove("SheetP");
						}
						else
						{
							{
								DbSqlParameterCollection Msc = new DbSqlParameterCollection();
								DbSqlParameter pID = new DbSqlParameter("@ID", SqlDbType.Int, 4);
								pID.Value = ac;
								Msc.Add(pID);
								DataTable parent = DatabaseConnection.DoStoredTable("GetParent",Msc);

								Msc = new DbSqlParameterCollection();
								DbSqlParameter cID = new DbSqlParameter("@ID", SqlDbType.Int, 4);
								cID.Value = ac;
								Msc.Add(cID);
								DataTable child = DatabaseConnection.DoStoredTable("GetChild",Msc);

								string[] myarray = new string[parent.Rows.Count + child.Rows.Count + 1];
								int i = 0;
								if (parent.Rows.Count > 0)
								{
									foreach (DataRow d in parent.Rows)
									{
										myarray[i] = d[0].ToString() + "|" + d[1].ToString();
										i++;
									}
								}

								myarray[i] = ac + "|" + DatabaseConnection.SqlScalar("SELECT SUBJECT FROM CRM_WORKACTIVITY WHERE ID=" + ac);
								i++;

								if (child.Rows.Count > 0)
								{
									foreach (DataRow d in child.Rows)
									{
										myarray[i] = d[0].ToString() + "|" + d[1].ToString();
										i++;
									}
								}
								this.SheetP.IDArray = myarray;
								if (myarray.Length > 1)
								{
									for (int y = 0; y < myarray.Length; y++)
									{
										if (Convert.ToInt32(myarray[y].Split('|')[0]).CompareTo(ac) == 0)
										{
											this.SheetP.CurrentPosition = y;
											this.SheetP.enabledisable();
											break;
										}
									}
									this.SheetP.Visible = true;
								}
								else
								{
									this.SheetP.CurrentPosition = 0;
									this.SheetP.enabledisable();
									this.SheetP.Visible = false;
								}
							}
						}
						FillForm(ac);

						string strScript = "<SCRIPT>" + Environment.NewLine +
							"needsave('" +Root.rm.GetString("CRMcontxt77") + "')" + Environment.NewLine +
							"</SCRIPT>";

						ClientScript.RegisterStartupScript(this.GetType(), "anything", strScript);

						SubmitBtn.Attributes.Add("onclick", "needsave('no')");
						SubmitBtnDoc.Attributes.Add("onclick", "needsave('no')");
						ChildAction.Attributes.Add("onclick", "needsave('no')");

					}
					else
					{
						((Label) Page.FindControl("ActivityID")).Text = "-1";

						FillSearchForm();
						((Label) Page.FindControl("ActivityInfo")).Text = String.Empty;
						FillRepeaterSearch(1);
					}

					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt15"), "-1"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt6"), "1"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt7"), "2"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt8"), "3"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt9"), "4"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt10"), "5"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt11"), "6"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt12"), "7"));
					ChildType.Items.Add(new ListItem(Root.rm.GetString("Wortxt13"), "8"));


					if (Request["lost"] == "1")
					{
						SearchComTec.Items[6].Selected = true;
						FillRepeaterSearch();
					}

					if(Request.QueryString["linknew"] != null)
					{
						SetForNewActivity();
						ViewAcType(int.Parse(Request.QueryString["linknew"]));
						NewActivity();
						AcPanel.Visible = true;
						AddKeepAlive();
						RepeaterSearch.Visible = false;
						return;
					}
				}

				if (Session["AType"] != null)
				{
					((Label) Page.FindControl("RepeaterSearchInfo")).Visible = false;
					this.SheetP.Visible = false;

					ViewAcType((int) Session["AType"]);
					Session.Remove("AType");
					if (Session["AcCompanyID"] != null)
					{
						DataSet dr = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + Session["AcCompanyID"].ToString());
						if (dr.Tables[0].Rows.Count > 0)
						{
							((TextBox) Page.FindControl("TextboxCompanyID")).Text = dr.Tables[0].Rows[0][0].ToString();
							((TextBox) Page.FindControl("TextboxCompany")).Text = dr.Tables[0].Rows[0][1].ToString();
						}
						Session.Remove("AcCompanyID");
					}
					if (Session["AcContactID"] != null)
					{
						DataSet dr = DatabaseConnection.CreateDataset("SELECT ID,(ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')) AS CONTACTNAME FROM BASE_CONTACTS WHERE ID=" + Session["AcContactID"].ToString());
						if (dr.Tables[0].Rows.Count > 0)
						{
							((TextBox) Page.FindControl("TextboxContactID")).Text = dr.Tables[0].Rows[0][0].ToString();
							((TextBox) Page.FindControl("TextboxContact")).Text = dr.Tables[0].Rows[0][1].ToString();
						}
						Session.Remove("AcContactID");
					}
					if (Session["AcLeadID"] != null)
					{
						DataSet dr = DatabaseConnection.CreateDataset("SELECT ID,(ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')+'-'+ISNULL(COMPANYNAME,'')) AS CONTACTNAME FROM CRM_LEADS WHERE ID=" + Session["AcLeadID"].ToString());
						if (dr.Tables[0].Rows.Count > 0)
						{
							((TextBox) Page.FindControl("TextboxLeadID")).Text = dr.Tables[0].Rows[0][0].ToString();
							((TextBox) Page.FindControl("TextboxLead")).Text = dr.Tables[0].Rows[0][1].ToString();
						}
						Session.Remove("AcLeadID");
					}
					if (Session["AcOpportunityID"] != null)
					{
						DataSet dr = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE ID=" + Session["AcOpportunityID"].ToString());
						if (dr.Tables[0].Rows.Count > 0)
						{
							((TextBox) Page.FindControl("TextboxOpportunityID")).Text = dr.Tables[0].Rows[0][0].ToString();
							((TextBox) Page.FindControl("TextboxOpportunity")).Text = dr.Tables[0].Rows[0][1].ToString();
						}
						Session.Remove("AcOpportunityID");
					}
					((TextBox) Page.FindControl("TextboxOwnerID")).Text = UC.UserId.ToString();
					((TextBox) Page.FindControl("TextboxOwner")).Text = UC.UserRealName.ToString();
					((TextBox) Page.FindControl("TextBoxData")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortDateString();
					((TextBox) Page.FindControl("TextBoxHour")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortTimeString();
					AcPanel.Visible = true;
					SearchPanel.Visible=false;
					AddKeepAlive();
					this.RepeaterSearch.Visible = false;
				}
			}
		}

		private void FillSearchForm()
		{
			RepeaterSearch.Visible = false;

			SearchType.Items.Clear();
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt6"), "1"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt7"), "2"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt8"), "3"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt9"), "4"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt10"), "5"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt11"), "6"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt12"), "7"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Wortxt13"), "8"));
			SearchType.Items.Add(new ListItem(Root.rm.GetString("Esttxt2").ToUpper(), "9"));


			SearchComTec.Items.Clear();
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt52"), "0"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt53"), "1"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt54"), "2"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt55"), "3"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt56"), "4"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt57"), "5"));
			SearchComTec.Items.Add(new ListItem(Root.rm.GetString("Acttxt59"), "7"));

			SearchToDo.Items.Clear();
			SearchToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt71"), "1"));
			SearchToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt72"), "0"));
			SearchToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt103"), "2"));

			SearchPriority.Items.Clear();
			SearchPriority.Items.Add(new ListItem(Root.rm.GetString("Wortxt3"), "0"));
			SearchPriority.Items.Add(new ListItem(Root.rm.GetString("Wortxt4"), "1"));
			SearchPriority.Items.Add(new ListItem(Root.rm.GetString("Wortxt5"), "2"));

			CheckSum.Text =Root.rm.GetString("Acttxt58");

			SearchPanel.Visible = true;
			AcPanel.Visible = false;
			RepeaterSearch.Visible = false;
		}

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "BtnSearch":
					FillSearchForm();
					break;
				case "SubmitSearch":
					FillRepeaterSearch();
					break;
			}

		}

		private byte searchtype
		{
			get
			{
				object o = this.ViewState["_SearchType" + this.ID];
				if (o == null)
					return 0;
				else
					return Convert.ToByte(o);
			}
			set { this.ViewState["_SearchType" + this.ID] = value; }

		}

		private void FillRepeaterSearch()
		{
			FillRepeaterSearch(0);
		}

		private void FillRepeaterSearch(byte searchTipe)
		{
			searchtype = searchTipe;
			StringBuilder SearchQuery = new StringBuilder();
			StringBuilder TimeQuery = new StringBuilder();
			StringBuilder tempQuery = new StringBuilder();
			string finalQuery = String.Empty;

			if (searchTipe == 0)
			{
				SearchQuery.Append("FROM CRM_WORKACTIVITYSEARCH_VIEW ");
				if (SearchType.Items[8].Selected)
					SearchQuery.Append("lEFT OUTER JOIN ESTIMATES ON CRM_WORKACTIVITYSEARCH_VIEW.ID=ESTIMATES.ACTIVITYID ");
				SearchQuery.AppendFormat("WHERE ({0}) ", GroupsSecure("CRM_WORKACTIVITYSEARCH_VIEW.GROUPS"));

				TimeQuery.AppendFormat("SELECT SUM(DURATION) FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE ({0}) ", GroupsSecure("CRM_WORKACTIVITYSEARCH_VIEW.GROUPS"));
				tempQuery.Append(" AND (");

				foreach (ListItem i in SearchType.Items)
				{
					if (i.Selected)
					{
						tempQuery.AppendFormat(" TYPE={0} OR ", i.Value);
					}
				}
				if (tempQuery.ToString().Length > 6)
				{
					SearchQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
					TimeQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
				}
				tempQuery = new StringBuilder();
				tempQuery.Append(" AND (");
				if (((TextBox) Page.FindControl("TextBoxSearchFromData")).Text.Length > 0)
				{
					string fromDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchFromData")).Text,UC.myDTFI)).ToString(@"yyyyMMdd");
					string toDate;
					DateTime Dfrom = Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchFromData")).Text);
					DateTime Dtodate;

					if (((TextBox) Page.FindControl("TextBoxSearchToData")).Text.Length > 0)
					{
						toDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchToData")).Text,UC.myDTFI)).ToString(@"yyyyMMdd");
						Dtodate = Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchToData")).Text);
					}
					else
					{
						toDate = UC.LTZ.ToLocalTime(Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchFromData")).Text,UC.myDTFI).AddDays(1)).ToString(@"yyyyMMdd");
						Dtodate = Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxSearchFromData")).Text);
					}

					DateTime utcFromDate = UC.LTZ.ToUniversalTime(Dfrom);
					TimeSpan mindiffstart = new TimeSpan(Dfrom.Ticks - utcFromDate.Ticks);
					DateTime utcToDate = UC.LTZ.ToUniversalTime(Dtodate);
					TimeSpan mindiffend = new TimeSpan(Dtodate.Ticks - utcToDate.Ticks);

					tempQuery.AppendFormat("(DATEADD(N,{0},CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE)>= '{1} 00:00' AND DATEADD(N,{3},CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE)< '{2} 23:59')", mindiffstart.TotalMinutes.ToString(), fromDate, toDate, mindiffend.TotalMinutes.ToString());
				}
				if (tempQuery.ToString().Length > 6)
				{
					SearchQuery.AppendFormat("{0})", tempQuery.ToString());
					TimeQuery.AppendFormat("{0})", tempQuery.ToString());
				}

				if (((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.COMPANYID={0})", ((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text);
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.COMPANYID={0})", ((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text);
				}
				if (((TextBox) Page.FindControl("TextboxSearchContactID")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID={0})", ((TextBox) Page.FindControl("TextboxSearchContactID")).Text);
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID={0})", ((TextBox) Page.FindControl("TextboxSearchContactID")).Text);
				}
				if (((TextBox) Page.FindControl("TextboxSearchLeadID")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.LEADID={0})", ((TextBox) Page.FindControl("TextboxSearchLeadID")).Text);
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.LEADID={0})", ((TextBox) Page.FindControl("TextboxSearchLeadID")).Text);
				}
				if (((TextBox) Page.FindControl("TextboxSearchOpID")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.OPPORTUNITYID={0})", ((TextBox) Page.FindControl("TextboxSearchOpID")).Text);
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.OPPORTUNITyID={0})", ((TextBox) Page.FindControl("TextboxSearchOpID")).Text);
				}

				if (((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.OWNERID={0})", ((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text);
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.OWNERID={0})", ((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text);
				}

				tempQuery = new StringBuilder();
				tempQuery.Append(" AND (");
				foreach (ListItem li in SearchComTec.Items)
				{
					if (li.Selected)
					{
						switch (li.Value)
						{
							case "0":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.TOBILL=1 OR ");
								break;
							case "1":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.COMMERCIAL=1 OR ");
								break;
							case "2":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.TECHNICAL=1 OR ");
								break;
							case "3":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.STATE=0 OR ");
								break;
							case "4":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.STATE=1 OR ");
								break;
							case "5":
								tempQuery.Append(" CRM_WORKACTIVITYSEARCH_VIEW.STATE=2 OR ");
								break;
							case "7":
								DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
								tempQuery.Append(" (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE<'" + UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd") + "') OR ");
								break;
						}
					}
				}
				if (tempQuery.ToString().Length > 6)
				{
					SearchQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
					TimeQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
				}

				tempQuery.Length = 0;
				tempQuery.Append(" AND (");
				foreach (ListItem li in SearchToDo.Items)
				{
					if (li.Selected)
						tempQuery.AppendFormat(" CRM_WORKACTIVITYSEARCH_VIEW.TODO={0} OR ", li.Value);
				}
				if (tempQuery.ToString().Length > 6)
				{
					SearchQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
					TimeQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
				}

				tempQuery.Length = 0;
				tempQuery.Append(" AND (");
				foreach (ListItem li in SearchPriority.Items)
				{
					if (li.Selected)
						tempQuery.AppendFormat(" CRM_WORKACTIVITYSEARCH_VIEW.PRIORITY={0} OR ", li.Value);
				}
				if (tempQuery.ToString().Length > 6)
				{
					SearchQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
					TimeQuery.AppendFormat("{0})", tempQuery.ToString().Substring(0, tempQuery.ToString().Length - 3));
				}

				if (((TextBox) Page.FindControl("TextboxSearchDesc")).Text.Length > 0)
				{
					SearchQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.SUBJECT LIKE '%{0}%' OR CRM_WORKACTIVITYSEARCH_VIEW.DESCRIPTION LIKE '%{0}%')", ((TextBox) Page.FindControl("TextboxSearchDesc")).Text.Replace("'", "''"));
					TimeQuery.AppendFormat(" AND (CRM_WORKACTIVITYSEARCH_VIEW.SUBJECT LIKE '%{0}%' OR CRM_WORKACTIVITYSEARCH_VIEW.DESCRIPTION LIKE '%{0}%')", ((TextBox) Page.FindControl("TextboxSearchDesc")).Text.Replace("'", "''"));
				}


				if (SearchType.Items[8].Selected)
				{
					tempQuery.Length = 0;
					tempQuery.Append(" AND (");
					if (tempQuery.ToString().Length > 6)
					{
						SearchQuery.AppendFormat("{0})", tempQuery.ToString());
						TimeQuery.AppendFormat("{0})", tempQuery.ToString());
					}
				}

				Trace.Warn("SearchQuery", SearchQuery.ToString());

				RepeaterSearchPaging.OrderField = "CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE";
				RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.descending;

				finalQuery = "SELECT TOP "+DatabaseConnection.MaxResult+" CRM_WORKACTIVITYSEARCH_VIEW.*,(" + TimeQuery.ToString() + ") AS TOTALTIME " + SearchQuery.ToString();

				Trace.Warn("FinalQuery", finalQuery);
			}
			else
			{
				SearchQuery.AppendFormat("FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE ({0}) ", GroupsSecure());
				SearchQuery.AppendFormat(" AND (LASTMODIFIEDBYID={0}) ", UC.UserId);

				if(UC.Zones.Length>0)
				{
					SearchQuery.AppendFormat(" AND ({0})", ZoneSecure("LEADCOMMERCIALZONE"));
					SearchQuery.AppendFormat(" AND ({0})", ZoneSecure("CONTACTCOMMERCIALZONE"));
					SearchQuery.AppendFormat(" AND ({0})", ZoneSecure("COMPANYCOMMERCIALZONE"));
				}

				SearchQuery.Append(" ORDER BY LASTMODIFIEDDATE DESC");

				RepeaterSearchPaging.OrderField = "LASTMODIFIEDDATE";
				RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.descending;

				finalQuery = "SELECT TOP 5 *,(0) AS TOTALTIME " + SearchQuery.ToString();
				Session["top5"] = 1;
				Trace.Warn("FinalQuery", finalQuery);
			}

			RepeaterSearchPaging.Reorder(finalQuery);
			DataSet ds = this.InitSheetPaging(finalQuery);
			RepeaterSearchPaging.RepeaterObj = RepeaterSearch;
			RepeaterSearchPaging.PageSize = UC.PagingSize;
			RepeaterSearchPaging.sqlRepeater = finalQuery;
			RepeaterSearchPaging.BuildGrid(ds);



			if (RepeaterSearch.Items.Count > 0)
			{
				RepeaterSearch.Visible = true;
				if (searchTipe == 0)
					SearchPanel.Visible = false;
				else
					SearchPanel.Visible = true;


				((Label) Page.FindControl("RepeaterSearchInfo")).Visible = false;
			}
			else
			{
				((Label) Page.FindControl("RepeaterSearchInfo")).Text = "<br><center>" +Root.rm.GetString("Acttxt95") + "</center>";
				((Label) Page.FindControl("RepeaterSearchInfo")).Visible = true;
			}
		}

		public void RepeaterSearchCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "CmdOrderByCompany":
					RepeaterSearchPaging.OrderField = "CRM_WORKACTIVITYSEARCH_VIEW.COMPANYNAME";
					if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.ascending;
					}
					else
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.descending;
					}

					RepeaterSearchPaging.Reorder();
					RepeaterSearchPaging.BuildGrid();
					break;
				case "CmdOrderByOwner":
					RepeaterSearchPaging.OrderField = "CRM_WORKACTIVITYSEARCH_VIEW.OWNERNAME";
					if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.ascending;
					}
					else
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.descending;
					}

					RepeaterSearchPaging.Reorder();
					RepeaterSearchPaging.BuildGrid();
					break;
				case "CmdOrderByDate":
					RepeaterSearchPaging.OrderField = "CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE";
					if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.ascending;
					}
					else
					{
						RepeaterSearchPaging.OrderType = RepeaterPaging.orderType.descending;
					}

					RepeaterSearchPaging.Reorder();
					RepeaterSearchPaging.BuildGrid();
					break;
				case "DelAc":
					int exId = int.Parse(((Literal) e.Item.FindControl("ExId")).Text);
					string crossappointment = DatabaseConnection.SqlScalar("SELECT CALENDARID FROM CRM_WORKACTIVITY WHERE ID=" + exId);
					if (crossappointment.Length > 0)
					{
						DatabaseConnection.DoCommand("DELETE FROM BASE_CALENDAR WHERE ID=" + crossappointment);
						DatabaseConnection.DoCommand("DELETE FROM BASE_CALENDAR WHERE SENCONDIDOWNER=" + crossappointment);
					}
					DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET PARENTID=0 WHERE PARENTID=" + exId);
					DatabaseConnection.DoCommand("DELETE FROM CRM_WORKACTIVITY WHERE ID=" + exId);

					FillRepeaterSearch(searchtype);
					break;
			}
		}

		public void RepeaterSearchDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					Literal LtrHeader = (Literal) e.Item.FindControl("LtrHeader");
					if (Session["top5"] != null)
					{
						LtrHeader.Text =Root.rm.GetString("Acttxt122");
						Session.Remove("top5");
					}
					else
						LtrHeader.Text =Root.rm.GetString("Acttxt67");

					LinkButton CmdOrderByCompany = (LinkButton) e.Item.FindControl("CmdOrderByCompany");
					if (RepeaterSearchPaging.OrderField == "CRM_WORKACTIVITYSEARCH_VIEW.COMPANYNAME")
					{
						if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
							CmdOrderByCompany.Text = "<img border=0 src=/images/down.gif>";
						else
							CmdOrderByCompany.Text = "<img border=0 src=/images/up.gif>";
					}
					else
						CmdOrderByCompany.Text = "<img border=0 src=/images/down.gif>";

					LinkButton CmdOrderByOwner = (LinkButton) e.Item.FindControl("CmdOrderByOwner");
					if (RepeaterSearchPaging.OrderField == "CRM_WORKACTIVITYSEARCH_VIEW.OWNERNAME")
					{
						if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
							CmdOrderByOwner.Text = "<img border=0 src=/images/down.gif>";
						else
							CmdOrderByOwner.Text = "<img border=0 src=/images/up.gif>";
					}
					else
						CmdOrderByOwner.Text = "<img border=0 src=/images/down.gif>";

					LinkButton CmdOrderByDate = (LinkButton) e.Item.FindControl("CmdOrderByDate");
					if (RepeaterSearchPaging.OrderField == "CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE")
					{
						if (RepeaterSearchPaging.OrderType == RepeaterPaging.orderType.descending)
							CmdOrderByDate.Text = "<img border=0 src=/images/down.gif>";
						else
							CmdOrderByDate.Text = "<img border=0 src=/images/up.gif>";
					}
					else
						CmdOrderByDate.Text = "<img border=0 src=/images/up.gif>";
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal activitywith = (Literal) e.Item.FindControl("activitywith");
					activitywith.Text = (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName")).Trim().Length>0)?Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName")):string.Empty;
					activitywith.Text += (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "ContactName")).Trim().Length>0)?" "+Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "ContactName")):string.Empty;
					activitywith.Text += (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadName")).Trim().Length>0)?" "+Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadName")):string.Empty;

					string id = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					Label Subject = (Label) e.Item.FindControl("Subject");
					int aType = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Type");
					Subject.Text = ImgType(aType);
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Subject"));
					byte todo = (byte) DataBinder.Eval((DataRowView) e.Item.DataItem, "ToDo");

					string todoImg = String.Empty;

					switch (todo)
					{
						case 0:
							todoImg = "<img border=0 src=/i/checkoff.gif>";
							break;
						case 1:
							todoImg = "<img border=0 src=/i/checkon.gif>";
							break;
						case 2:
							todoImg = "<img border=0 src=/i/checkout.gif>";

							break;
					}

					if (sub.Length > 30)
					{
						Regex r = new Regex(@"(?s)\b.{1,50}\b");
						Subject.ToolTip = sub;
						Subject.Text += "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Sa=1&Ac=" + id + ">" + todoImg + r.Match(sub) + "&hellip;" + "</a>";
						Subject.Attributes.Add("style", "cursor:help;");
					}
					else
					{
						Subject.Text += "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Sa=1&Ac=" + id + ">" + todoImg + sub + "</a>";
					}

					if (((byte) DataBinder.Eval((DataRowView) e.Item.DataItem, "ToDo")) == 2)
						Subject.CssClass = "LinethroughGray";

					Literal AcDate = (Literal) e.Item.FindControl("AcDate");
					AcDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"), UC.myDTFI)).ToString("g");

					Literal AcTime = (Literal) e.Item.FindControl("AcTime");
					int duration = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Duration");
					if (duration > 0)
					{
						if (duration < 60)
						{
							AcTime.Text = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
						}
						else
						{
							AcTime.Text = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
								((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
						}
						TotalPageDuration += duration;
					}
					else
					{
						AcTime.Text = String.Empty;
					}
					TotalDuration = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "TotalTime");
					LinkButton DelAc = (LinkButton) e.Item.FindControl("DelAc");
					DelAc.Text =Root.rm.GetString("Acttxt39");
					DelAc.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Acttxt42") + "');");

					break;
				case ListItemType.Footer:
					if (CheckSum.Checked)
					{
						Literal LtrTime = (Literal) e.Item.FindControl("LtrTime");
						duration = TotalPageDuration;
						if (duration > 0)
						{
							if (duration < 60)
							{
								LtrTime.Text = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
							}
							else
							{
								LtrTime.Text = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
									((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
							}
						}
						else
						{
							LtrTime.Text = "&nbsp;";
						}

						LtrTime = (Literal) e.Item.FindControl("LtrTimeTotal");
						duration = TotalDuration;
						if (duration > 0)
						{
							if (duration < 60)
							{
								LtrTime.Text = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
							}
							else
							{
								LtrTime.Text = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
									((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
							}
						}
						else
						{
							LtrTime.Text = "&nbsp;";
						}
					}
					else
					{
						HtmlContainerControl TimeSum = (HtmlContainerControl) e.Item.FindControl("TimeSum");
						TimeSum.Visible = false;
					}

					Literal RepCounter = (Literal) e.Item.FindControl("RepCounter");
					RepCounter.Text = RepeaterSearchPaging.RowCount.ToString()+"&nbsp;"+((RepeaterSearchPaging.RowCount==int.Parse(DatabaseConnection.MaxResult))?Root.rm.GetString("qLimit"):"");
					break;
			}
		}


		private string ImgType(int aType)
		{
			string img = String.Empty;
			switch (aType)
			{
				case 1:
					img = "<img src=/i/a/Phone.gif>";
					break;
				case 2:
					img = "<img src=/i/a/letter.gif>";
					break;
				case 3:
					img = "<img src=/i/a/fax.gif>";
					break;
				case 4:
					img = "<img src=/i/a/Pin.gif>";
					break;
				case 5:
					img = "<img src=/i/a/Email.gif>";
					break;
				case 6:
					img = "<img src=/i/a/Hands.gif>";
					break;
				case 7:
					img = "<img src=/i/a/generic.gif>";
					break;
				case 8:
					img = "<img src=/i/a/case.gif>";
					break;
			}
			return img;
		}

		private void FillForm(int id)
		{
			((Label) Page.FindControl("ActivityID")).Text = id.ToString();
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("SELECT * FROM CRM_WORKING_VIEW WHERE ID={0}" , id);

			string queryGroup = GroupsSecure("GROUPS",UC);
			if (queryGroup.Length > 0)
				sb.AppendFormat(" AND ({0})", queryGroup);

			if(UC.Zones.Length>0)
			{
				sb.AppendFormat(" AND ({0})", ZoneSecure("LEADCOMMERCIALZONE"));
				sb.AppendFormat(" AND ({0})", ZoneSecure("CONTACTCOMMERCIALZONE"));
				sb.AppendFormat(" AND ({0})", ZoneSecure("COMPANYCOMMERCIALZONE"));
			}

			DataSet ds = DatabaseConnection.CreateDataset(sb.ToString());

			if(ds.Tables[0].Rows.Count<=0)
			{
				Response.Redirect("/today.aspx?e1=1");
			}
			DataRow dr = ds.Tables[0].Rows[0];

			ViewAcType((int) dr["type"]);

			if ((int) dr["type"] == 6 && dr["VisitID"] != DBNull.Value)
			{
					DataTable dtHours = DatabaseConnection.CreateDataset("SELECT STARTDATE,ENDDATE,SECONDUID,UID FROM BASE_CALENDAR WHERE ID=" + dr["VisitID"].ToString()).Tables[0];
					if(dtHours.Rows.Count>0)
					{
						DataRow drHours = dtHours.Rows[0];
							((TextBox) Page.FindControl("F_StartHour")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drHours["startdate"])).ToShortTimeString();
						((TextBox) Page.FindControl("F_EndHour")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drHours["enddate"])).ToShortTimeString();
						if (drHours["SecondUID"] != drHours["UID"])
						{
							((TextBox) Page.FindControl("IdCompanion")).Text = drHours["SecondUID"].ToString();
							((TextBox) Page.FindControl("Companion")).Text = DatabaseConnection.SqlScalar("SELECT NAME+' '+SURNAME FROM ACCOUNT WHERE UID=" + drHours["SecondUID"].ToString());
						}
					}
			}


			((TextBox) Page.FindControl("TextBoxSubject")).Text = dr["Subject"].ToString();
			RadioButtonList Activity_InOut = (RadioButtonList) Page.FindControl("Activity_InOut");
			Activity_InOut.SelectedIndex = -1;
			if ((bool) dr["InOut"])
				Activity_InOut.SelectedIndex = 0;
			else
				Activity_InOut.SelectedIndex = 1;
			RadioButtonList Activity_ToDo = (RadioButtonList) Page.FindControl("Activity_ToDo");

			switch (Convert.ToInt32(dr["ToDo"]))
			{
				case 0:
					Activity_ToDo.SelectedIndex = 1;
					break;
				case 1:
					Activity_ToDo.SelectedIndex = 0;
					break;
				case 2:
					Activity_ToDo.SelectedIndex = 2;
					break;
			}


			if ((int) dr["type"] == 6 && Convert.ToInt32(dr["ToDo"]) != 0)
				Appointmenthours.Attributes.Add("style", "display:none");

			((TextBox) Page.FindControl("TextBoxData")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToShortDateString();
			((TextBox) Page.FindControl("TextBoxHour")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToShortTimeString();

			DropDownList DropDownListStatus = (DropDownList) Page.FindControl("DropDownListStatus");
			DropDownListStatus.SelectedIndex = -1;
			foreach (ListItem li in DropDownListStatus.Items)
			{
				if (li.Value == dr["State"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			DropDownList DropDownListPriority = (DropDownList) Page.FindControl("DropDownListPriority");
			DropDownListPriority.SelectedIndex = -1;
			foreach (ListItem li in DropDownListPriority.Items)
			{
				if (li.Value == dr["Priority"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			((TextBox) Page.FindControl("TextboxCompanyID")).Text = dr["CompanyID"].ToString();
			((TextBox) Page.FindControl("TextboxCompany")).Text = dr["CompanyName"].ToString();
			((TextBox) Page.FindControl("TextboxLeadID")).Text = dr["LeadID"].ToString();
			((TextBox) Page.FindControl("TextboxLead")).Text = (dr["LeadName"].ToString().Length > 3) ? dr["LeadName"].ToString() : "";
			((TextBox) Page.FindControl("TextboxOwnerID")).Text = dr["OwnerID"].ToString();
			((TextBox) Page.FindControl("TextboxOwner")).Text = dr["OwnerName"].ToString();
			((CheckBox) Page.FindControl("CheckToBill")).Checked = (bool) dr["ToBill"];
			((CheckBox) Page.FindControl("CheckCommercial")).Checked = (bool) dr["Commercial"];
			((CheckBox) Page.FindControl("CheckTechnical")).Checked = (bool) dr["Technical"];

			DropDownList DropDownListPreAlarm = (DropDownList) Page.FindControl("DropDownListPreAlarm");
			DropDownListPreAlarm.SelectedIndex = -1;
			foreach (ListItem li in DropDownListPreAlarm.Items)
			{
				if (li.Value == dr["DaysAllarm"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			((TextBox) Page.FindControl("TextboxContactID")).Text = dr["ReferrerID"].ToString();
			((TextBox) Page.FindControl("TextboxContact")).Text = dr["ContactName"].ToString();

			((TextBox) Page.FindControl("TextboxOpportunityID")).Text = dr["OpportunityID"].ToString();
			((TextBox) Page.FindControl("TextboxOpportunity")).Text = dr["OpportunityDescription"].ToString();

			DropDownList DropDownListClassification = (DropDownList) Page.FindControl("DropDownListClassification");
			DropDownListClassification.SelectedIndex = -1;
			foreach (ListItem li in DropDownListClassification.Items)
			{
				if (li.Value == dr["Classification"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			((TextBox) Page.FindControl("TextboxParentID")).Text = dr["ParentID"].ToString();
			((TextBox) Page.FindControl("TextboxParent")).Text = dr["ParentSubject"].ToString();
			((TextBox) Page.FindControl("TextboxDescription")).Text = dr["Description"].ToString();
			((TextBox) Page.FindControl("TextboxDescription2")).Text = dr["Description2"].ToString();

			if ((int) dr["Duration"] > 0)
			{
				if ((int) dr["Duration"] < 60)
				{
					((TextBox) Page.FindControl("TextBoxDurationM")).Text = dr["Duration"].ToString();
				}
				else
				{
					int dur = (int) dr["Duration"];
					((TextBox) Page.FindControl("TextBoxDurationH")).Text = Convert.ToInt32(dur/60).ToString();
					((TextBox) Page.FindControl("TextBoxDurationM")).Text = Convert.ToInt32(dur%60).ToString();
				}
			}



			if (dr["DocID"] != DBNull.Value)
			{
				DataTable dtDoc = DatabaseConnection.CreateDataset("SELECT FILECROSSTABLES.IDFILE,FILEMANAGER.FILENAME FROM FILECROSSTABLES LEFT OUTER JOIN FILEMANAGER ON FILECROSSTABLES.IDFILE=FILEMANAGER.ID WHERE FILEMANAGER.ID=" + dr["DocID"].ToString()).Tables[0];
				TextBox DocumentDescription = ((TextBox) Page.FindControl("DocumentDescription"));
				if (dtDoc.Rows.Count > 0)
				{
					((TextBox) Page.FindControl("IDDocument")).Text = dtDoc.Rows[0]["idfile"].ToString();
					DocumentDescription.Text = dtDoc.Rows[0]["filename"].ToString();
				}
				else
				{
					try
					{
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("IDFILE", dr["DocID"]);
							dg.Add("TABLENAME", "CRM_WorkActivity");
							dg.Add("IDRIF", Convert.ToInt64(id));

							dg.Execute("FILECROSSTABLES");
						}
						DataTable tDocNew = DatabaseConnection.CreateDataset("SELECT FILECROSSTABLES.IDFILE,FILEMANAGER.FILENAME FROM FILECROSSTABLES LEFT OUTER JOIN FILEMANAGER ON FILECROSSTABLES.IDFILE=FILEMANAGER.ID WHERE FILEMANAGER.ID=" + dr["DocID"].ToString()).Tables[0];
						((TextBox) Page.FindControl("IDDocument")).Text = tDocNew.Rows[0]["idfile"].ToString();
						DocumentDescription.Text = tDocNew.Rows[0]["filename"].ToString();
					}
					catch
					{
					}
				}
			}

			AcPanel.Visible = true;
			AddKeepAlive();
			this.RepeaterSearch.Visible = false;

			GetLog(id);

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
		}

		private void GetLog(int id)
		{
			DataTable dtLog = DatabaseConnection.CreateDataset("SELECT MOVEDATE,ACTIONTYPE,PREVVALUE,NEXTVALUE, ACCOUNT.NAME+' '+ACCOUNT.SURNAME AS OWNERNAME FROM ACTIVITYMOVELOG LEFT OUTER JOIN ACCOUNT ON ACTIVITYMOVELOG.OWNERID = ACCOUNT.UID WHERE ACTIVITYMOVELOG.ACID=" + id).Tables[0];
			if (dtLog.Rows.Count > 0)
			{
				Table LogTable = new Table();
				int i = 0;
				LogTable.ID = "LogTable";
				LogTable.Width = Unit.Percentage(100);
				LogTable.Attributes.Add("style", "display:none");
				TableRow trTitle = new TableRow();
				TableCell tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt11"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt110"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt111"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt112"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt113"));
				trTitle.Cells.Add(tdtitle);

				LogTable.Rows.Add(trTitle);
				foreach (DataRow drlog in dtLog.Rows)
				{
					i++;
					TableRow tr = new TableRow();
					for (int cell = 0; cell < 5; cell++)
					{
						TableCell td = new TableCell();
						td.CssClass = ((i%2) == 0) ? "GridItem" : "GridItemAltern";
						switch (cell)
						{
							case 0:
								td.Text = ((DateTime) drlog[0]).ToShortDateString();
								break;
							case 1:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text =Root.rm.GetString("Acttxt104");
										break;
									case ActivityMoveLog.MoveDate:
										td.Text =Root.rm.GetString("Acttxt105");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text =Root.rm.GetString("Acttxt106");
										break;
									case ActivityMoveLog.MoveContact:
										td.Text =Root.rm.GetString("Acttxt107");
										break;
									case ActivityMoveLog.MoveLead:
										td.Text =Root.rm.GetString("Acttxt108");
										break;
									case ActivityMoveLog.MoveDone:
										td.Text =Root.rm.GetString("Acttxt109");
										break;
									case ActivityMoveLog.MailSent:
										td.Text =Root.rm.GetString("Acttxt119");
										break;
								}
								break;
							case 2:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS ACCOUNTNAME FROM ACCOUNT WHERE UID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveDate:
										DateTime dt;
										if (drlog[2].ToString().Length > 8)
											dt = DateTime.ParseExact((string) drlog[2], @"yyyyMMdd H.mm", null);
										else
											dt = DateTime.ParseExact((string) drlog[2], @"yyyyMMdd", null);
										td.Text = dt.ToString("g");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveContact:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS CONTACTNAME FROM BASE_CONTACTS WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveLead:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'')) AS LEADNAME FROM CRM_LEADS WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveDone:
										switch (drlog[2].ToString())
										{
											case "0":
												td.Text =Root.rm.GetString("Acttxt72");
												break;
											case "1":
												td.Text =Root.rm.GetString("Acttxt71");
												break;
											case "2":
												td.Text =Root.rm.GetString("Acttxt103");
												break;
										}
										break;
									case ActivityMoveLog.MailSent:
										td.Text = drlog[2].ToString();
										break;
								}

								break;
							case 3:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS ACCOUNTNAME FROM ACCOUNT WHERE UID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveDate:

										DateTime dt;
										if (drlog[3].ToString().Length > 8)
											dt = DateTime.ParseExact((string) drlog[3], @"yyyyMMdd H.mm", null);
										else
											dt = DateTime.ParseExact((string) drlog[3], @"yyyyMMdd", null);

										td.Text = dt.ToString("g");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveContact:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS CONTACTNAME FROM BASE_CONTACTS WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveLead:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'')) AS LEADNAME FROM CRM_LEADS WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveDone:
										switch (drlog[3].ToString())
										{
											case "0":
												td.Text =Root.rm.GetString("Acttxt72");
												break;
											case "1":
												td.Text =Root.rm.GetString("Acttxt71");
												break;
											case "2":
												td.Text =Root.rm.GetString("Acttxt103");
												break;
										}
										break;
									case ActivityMoveLog.MailSent:
										td.Text = drlog[3].ToString();
										break;
								}
								break;
							case 4:
								td.Text = drlog[4].ToString();
								break;
						}
						tr.Cells.Add(td);
					}
					LogTable.Rows.Add(tr);
				}
				((PlaceHolder) Page.FindControl("MoveLogTable")).Controls.Add(LogTable);
				((PlaceHolder) Page.FindControl("MoveLogTable")).EnableViewState = false;
				MoveLog.Visible = true;
			}

		}

		protected virtual void DownloadFile_Click(object sender, EventArgs e)
		{
			TextBox FileId = (TextBox) Page.FindControl("IDDocument");

			if (FileId.Text.Length > 0)
			{
				DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileId.Text));

				string fileName;
				fileName = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"];
				string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();

				string downFile = fileName + Path.GetExtension(realFileName);
				if (File.Exists(downFile))
				{
					Response.AddHeader("Content-Disposition", "attachment; fileName=" + realFileName);
					Response.ContentType = "application/octet-stream";
					Response.TransmitFile(downFile);
					Response.Flush();
					Response.End();
					return;
				}
				else if (File.Exists(fileName))
				{
					File.Move(fileName, downFile);
					Response.AddHeader("Content-Disposition", "attachment; fileName=" + realFileName);
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
		}


		protected virtual void NewActivity_Click(object sender, EventArgs e)
		{
			SetForNewActivity();

			switch (((LinkButton) sender).ID)
			{
				case "ActPhone":
					ViewAcType((int) ActivityTypeN.PhoneCall);
					break;
				case "ActVisit":
					ViewAcType((int) ActivityTypeN.Visit);
					Appointmenthours.Attributes.Add("style", "display:inline");
					break;
				case "ActEmail":
					ViewAcType((int) ActivityTypeN.Email);
					break;
				case "ActFax":
					ViewAcType((int) ActivityTypeN.Fax);
					break;
				case "ActLetter":
					ViewAcType((int) ActivityTypeN.Letter);
					break;
				case "ActGeneric":
					ViewAcType((int) ActivityTypeN.Generic);
					break;
				case "ActCase":
					ViewAcType((int) ActivityTypeN.CaseSolution);
					break;
				case "ActMemo":
					ViewAcType((int) ActivityTypeN.Memo);
					break;
				case "ActQuote":
					GoToSales();
					break;
			}
			NewActivity();
			AcPanel.Visible = true;
			AddKeepAlive();
			RepeaterSearch.Visible = false;
		}

		private void GoToSales()
		{
			ClientScript.RegisterStartupScript(this.GetType(), "","<script>alert('" + Root.rm.GetString("Quotxt37")+"');location.href='/erp/quotelist.aspx?m=67&dgb=1&si=69';</script>");
		}

		private void SetForNewActivity()
		{
			this.SheetP.Visible = false;
			((Label) Page.FindControl("ActivityID")).Text = "-1";
			((TextBox) Page.FindControl("TextboxOwnerID")).Text = UC.UserId.ToString();
			((TextBox) Page.FindControl("TextboxOwner")).Text = UC.UserRealName.ToString();
			((TextBox) Page.FindControl("TextBoxData")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortDateString();
			((TextBox) Page.FindControl("TextBoxHour")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortTimeString();
			((TextBox) Page.FindControl("TextBoxSubject")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxCompanyID")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxCompany")).Text = String.Empty;
			((TextBox) Page.FindControl("TextBoxDurationH")).Text = String.Empty;
			((TextBox) Page.FindControl("TextBoxDurationM")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxContactID")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxContact")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxOpportunityID")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxOpportunity")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxParentID")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxParent")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxDescription")).Text = String.Empty;
			((TextBox) Page.FindControl("TextboxDescription2")).Text = String.Empty;
			((TextBox) Page.FindControl("DocumentDescription")).Text = String.Empty;
			((TextBox) Page.FindControl("IDDocument")).Text = String.Empty;
			((Label) Page.FindControl("ActivityInfo")).Text = String.Empty;
			Activity_ToDo.SelectedIndex = 1;

		}


		private void FillActivityType(string img, string txt)
		{
			ActivityType.Header = string.Format("<b>{0}</b>", txt);
		}

		private void NewActivity()
		{

			TextBox tx = (TextBox) Page.FindControl("TextboxOwnerID");
			tx.Text = UC.UserId.ToString();
			tx = (TextBox) Page.FindControl("TextboxOwner");

			tx.Text = DatabaseConnection.SqlScalar("SELECT (SURNAME+' '+NAME) AS OWNER FROM ACCOUNT WHERE UID=" + UC.UserId.ToString());

			LinkDocument.Visible = false;
            ChildType.SelectedIndex = 0;
		}

		public void ChildAction_Click(object sender, EventArgs e)
		{
			if (Convert.ToInt32(ChildType.SelectedValue) > 0)
			{
				bool saved = true;
				if (((Label) Page.FindControl("ActivityID")).Text == "-1")
				{
					saved = SaveActivity(((LinkButton) sender).ID);
				}
				if (saved)
				{
					((TextBox) Page.FindControl("TextboxOwnerID")).Text = UC.UserId.ToString();
					((TextBox) Page.FindControl("TextboxOwner")).Text = UC.UserRealName.ToString();

					((TextBox) Page.FindControl("TextBoxDurationH")).Text = String.Empty;
					((TextBox) Page.FindControl("TextBoxDurationM")).Text = String.Empty;

					((TextBox) Page.FindControl("TextboxDescription")).Text = String.Empty;
					((TextBox) Page.FindControl("TextboxDescription2")).Text = String.Empty;
					((TextBox) Page.FindControl("DocumentDescription")).Text = String.Empty;
					((TextBox) Page.FindControl("IDDocument")).Text = String.Empty;


					((TextBox) Page.FindControl("TextboxParentID")).Text = ((Label) Page.FindControl("ActivityID")).Text;
					((TextBox) Page.FindControl("TextboxParent")).Text = ((TextBox) Page.FindControl("TextBoxSubject")).Text;

					((TextBox) Page.FindControl("TextBoxData")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortDateString();
					((TextBox) Page.FindControl("TextBoxHour")).Text = UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortTimeString();
					((TextBox) Page.FindControl("TextBoxSubject")).Text = String.Empty;

					((TextBox) Page.FindControl("TextboxDescription")).Text = String.Empty;
					((TextBox) Page.FindControl("TextboxDescription2")).Text = String.Empty;

					ViewAcType(Convert.ToInt32(ChildType.SelectedValue));

					((Label) Page.FindControl("ActivityID")).Text = "-1";
					((Label) Page.FindControl("ActivityInfo")).Text = String.Empty;
					Activity_ToDo.SelectedIndex = 1;
				}

			}
			else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "error", "<script>alert('" +Root.rm.GetString("Wortxt16") + "');</script>");
			}
		}

		public void Submit_Click(object sender, EventArgs e)
		{
			SaveActivity(((LinkButton) sender).ID);
		}

		private bool SaveActivity(string sender)
		{
			Label ActivityInfo = (Label) Page.FindControl("ActivityInfo");
			int newId = int.Parse(((Label) Page.FindControl("ActivityID")).Text);
			string modified1 = InsModActivity(((Literal) Page.FindControl("LabelTypeActivity")).Text, newId);

			if (newId.ToString() != ((Label) Page.FindControl("ActivityID")).Text)
				newId = int.Parse(((Label) Page.FindControl("ActivityID")).Text);

			if (((TextBox) Page.FindControl("IDDocument")).Text.Length > 0)
				LinkDocument.Visible = true;
			else
				LinkDocument.Visible = false;

			bool modified = true;
			if (modified1 == "0")
				ActivityInfo.Text = "<img src=\"/i/ok.gif\">&nbsp;" +Root.rm.GetString("Acttxt92");
			else
			{
				modified = false;
				if (modified1 == "1")
					ActivityInfo.Text =Root.rm.GetString("Acttxt93");
				else
					ActivityInfo.Text = "<img src=\"/i/alert.gif\">&nbsp;" +Root.rm.GetString("Acttxt131") ;
			}

			if (modified)
			{
				if (((RadioButtonList) Page.FindControl("Activity_ToDo")).SelectedIndex == 0)
				{
					DateTime activityDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxData")).Text, UC.myDTFI));
					if (((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0)
						InsertLastContact(((TextBox) Page.FindControl("TextboxCompanyID")).Text, 0, activityDate);
					if (((TextBox) Page.FindControl("TextboxContactID")).Text.Length > 0)
						InsertLastContact(((TextBox) Page.FindControl("TextboxContactID")).Text, 1, activityDate);
					if (((TextBox) Page.FindControl("TextboxLeadID")).Text.Length > 0)
						InsertLastContact(((TextBox) Page.FindControl("TextboxLeadID")).Text, 2, activityDate);
				}
				if (((Literal) Page.FindControl("LabelTypeActivity")).Text == "6")
				{
					if (Activity_ToDo.Items[1].Selected)
					{
						DataRow dataRow = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITY WHERE ID=" + newId).Tables[0].Rows[0];
						string appId = AppointmentUpdate((dataRow["VisitID"] == DBNull.Value) ? "-1" : dataRow["VisitID"].ToString(),
						                                 dataRow["OwnerID"].ToString(),
						                                 ((TextBox) Page.FindControl("TextBoxData")).Text,
						                                 ((TextBox) Page.FindControl("F_StartHour")).Text,
						                                 ((TextBox) Page.FindControl("F_EndHour")).Text,
						                                 ((TextBox) Page.FindControl("TextboxContact")).Text,
						                                 ((TextBox) Page.FindControl("TextboxContactID")).Text,
						                                 ((TextBox) Page.FindControl("TextboxCompany")).Text,
						                                 ((TextBox) Page.FindControl("TextboxCompanyID")).Text,
						                                 ((TextBox) Page.FindControl("TextBoxSubject")).Text,
						                                 (dataRow["VisitID"] == DBNull.Value) ? "-1" : dataRow["VisitID"].ToString());
						DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET VISITID='" + appId + "' WHERE ID='" + newId + "'");
						if (((TextBox) Page.FindControl("IdCompanion")).Text.Length > 0)
						{
							string newAppId = DatabaseConnection.SqlScalar("SELECT SENCONDIDOWNER FROM BASE_CALENDAR WHERE ID=" + appId);
							if (newAppId.Length <= 0) newAppId = "-1";
							string crossId = AppointmentUpdate(newAppId,
							                                   ((TextBox) Page.FindControl("IdCompanion")).Text,
							                                   ((TextBox) Page.FindControl("TextBoxData")).Text,
							                                   ((TextBox) Page.FindControl("F_StartHour")).Text,
							                                   ((TextBox) Page.FindControl("F_EndHour")).Text,
							                                   ((TextBox) Page.FindControl("TextboxContact")).Text,
							                                   ((TextBox) Page.FindControl("TextboxContactID")).Text,
							                                   ((TextBox) Page.FindControl("TextboxCompany")).Text,
							                                   ((TextBox) Page.FindControl("TextboxCompanyID")).Text,
							                                   ((TextBox) Page.FindControl("TextBoxSubject")).Text,
							                                   appId);
							DatabaseConnection.DoCommand("UPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossId + " WHERE ID=" + appId);
						}
					}
				}
			}


			if (((TextBox) Page.FindControl("TextboxOpportunityID")).Text.Length > 0)
			{
				int oppId = int.Parse(((TextBox) Page.FindControl("TextboxOpportunityID")).Text);
				if (((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0)
				{
					int companyId = int.Parse(((TextBox) Page.FindControl("TextboxCompanyID")).Text);
					int checkCross = Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=0 AND CONTACTID = {0} AND OPPORTUNITYID = {1};", companyId, oppId)));
					if (checkCross <= 0)
					{
						InsertNewOpportunity(oppId.ToString(), companyId.ToString(), "0");
					}
				}
				if (((TextBox) Page.FindControl("TextboxLeadID")).Text.Length > 0)
				{
					int leadId = int.Parse(((TextBox) Page.FindControl("TextboxLeadID")).Text);
					int checkCross = Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=1 AND CONTACTID = {0} AND OPPORTUNITYID = {1};", leadId, oppId)));
					if (checkCross <= 0)
					{
						InsertNewOpportunity(((TextBox) Page.FindControl("TextboxOpportunityID")).Text, ((TextBox) Page.FindControl("TextboxLeadID")).Text, "1");
					}
				}
			}


			if (CheckSendMail.Checked && ((HtmlInputText) Page.FindControl("destinationEmail")).Value.Length > 0)
			{
				string notifyEmail = DatabaseConnection.SqlScalar("SELECT NOTIFYEMAIL FROM ACCOUNT WHERE UID=" + UC.UserId);
				MessagesHandler.SendMail(((HtmlInputText) Page.FindControl("destinationEmail")).Value,
				         (notifyEmail.Length > 0) ? notifyEmail : UC.UserName,
				         ((TextBox) Page.FindControl("TextBoxSubject")).Text,
				         ((TextBox) Page.FindControl("TextboxDescription")).Text
					);
			}

			if (modified && sender == "SubmitBtnDoc")
			{
				Context.Items["NEW"] = true;
				Context.Items["CrossText"] = ((TextBox) Page.FindControl("TextBoxSubject")).Text;
				Context.Items["CrossID"] = ((Label) Page.FindControl("ActivityID")).Text;
				if ((Session["goback1"] is Stack && ((Stack) Session["goback1"]).Count > 0)) Session["BackAfterSubmit"] = true;
				Session["ViewStatePage"] = "/DataStorage/datastorage.aspx";
				Server.Transfer("/DataStorage/datastorage.aspx");
			}
			else
			{
				if (modified)
					if (isGoBack && sender != "ChildAction")
						GoBackClick(false);
			}
			GetLog(newId);

			return modified;
		}

		public void InsertNewOpportunity(string opid, string id, string type)
		{
			using (DigiDapter dg = new DigiDapter())
			{

				dg.Add("OPPORTUNITYID", opid);
				dg.Add("CONTACTID", id);
				dg.Add("CONTACTTYPE", type);

				dg.Add("EXPECTEDREVENUE", 0);
				dg.Add("AMOUNTCLOSED", 0);
				dg.Add("INCOMEPROBABILITY", 0);


				dg.Add("NOTE", String.Empty);
				dg.Add("STARTDATE", DateTime.Now);
				dg.Add("ESTIMATEDCLOSEDATE", DateTime.Now.AddDays(Convert.ToDouble(DatabaseConnection.SqlScalar("SELECT TOP 1 ESTIMATEDDATEDAYS FROM TUSTENA_DATA"))));

				dg.Add("ENDDATE", DBNull.Value);

				dg.Add("SALESPERSON", ((TextBox) Page.FindControl("TextboxOwnerID")).Text);

				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("CREATEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));

				dg.Execute("CRM_OPPORTUNITYCONTACT");
			}


			DataTable dtCross = DatabaseConnection.CreateDataset("SELECT COMPETITORID FROM CRM_OPPORTUNITYCOMPETITOR WHERE OPPORTUNITYID=" + int.Parse(opid)).Tables[0];
			foreach (DataRow dr in dtCross.Rows)
			{
				if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_CROSSCONTACTCOMPETITOR WHERE CONTACTTYPE=" + type + " AND COMPETITORID=" + dr[0].ToString() + " AND CONTACTID=" + int.Parse(id))) == 0)
				{
					DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSCONTACTCOMPETITOR (COMPETITORID,CONTACTID,CONTACTTYPE) VALUES (" + dr[0].ToString() + "," + int.Parse(id) + "," + type + ")");
				}
			}
		}

		private void ViewAcType(int type)
		{
			((Literal) Page.FindControl("LabelTypeActivity")).Text = type.ToString();
			switch (type)
			{
				case (int) ActivityTypeN.PhoneCall:
					FillActivityType("Phone.gif",Root.rm.GetString("Wortxt6"));
					FillDropDown("1");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.Letter:
					FillActivityType("letter.gif",Root.rm.GetString("Wortxt7"));
					FillDropDown("2");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.Fax:
					FillActivityType("fax.gif",Root.rm.GetString("Wortxt8"));
					FillDropDown("3");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.Memo:
					FillActivityType("Pin.gif",Root.rm.GetString("Wortxt9"));
					FillDropDown("4");
					PanelOwner.Visible = true;
					CheckToBill.Visible = false;
					CheckCommercial.Visible = false;
					CheckTechnical.Visible = false;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.Email:
					FillActivityType("email.gif",Root.rm.GetString("Wortxt10"));
					FillDropDown("5");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;

					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.Visit:
					FillActivityType("Hands.gif",Root.rm.GetString("Wortxt11"));
					FillDropDown("6");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = true;
					IMGAvailability.Visible = true;
					Appointmenthours.Visible = true;
					HourPanel.Visible = false;
					Activity_ToDo.Attributes.Add("onclick", "ActivateHours()");
					break;
				case (int) ActivityTypeN.Generic:
					FillActivityType("generic.gif",Root.rm.GetString("Wortxt12"));
					FillDropDown("7");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt74");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt86");

					IMGAvailability.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
				case (int) ActivityTypeN.CaseSolution:
					FillActivityType("case.gif",Root.rm.GetString("Wortxt13"));
					FillDropDown("8");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = false;
					CheckTechnical.Visible = false;
					((Label) Page.FindControl("LabelData")).Text =Root.rm.GetString("Acttxt38");
					((Label) Page.FindControl("LabelDescription")).Text =Root.rm.GetString("Acttxt75");
					((Label) Page.FindControl("LabelDescription2")).Text =Root.rm.GetString("Acttxt76");
					SecondDescription.Visible = true;

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					break;
			}
			SearchPanel.Visible = false;
		}


		private void FillActivity_Document()
		{
			try
			{
				DirectoryInfo template;
				template = new DirectoryInfo(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "Template");
				Activity_Document.DataSource = template.GetFiles("*.rtf");
				Activity_Document.DataBind();
				Activity_Document.Items.Insert(0,Root.rm.GetString("Acttxt94"));
				Activity_Document.SelectedIndex = 0;
			}
			catch
			{
				Activity_Document.Visible = false;
				DocGen.Visible = false;
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
			this.PreRender +=new EventHandler(AllActivity_PreRender);
			BtnSearch.Click += new EventHandler(BtnSearch_Click);
			SubmitSearch.Click += new EventHandler(BtnSearch_Click);
			ActPhone.Click += new EventHandler(NewActivity_Click);
			ActVisit.Click += new EventHandler(NewActivity_Click);
			ActEmail.Click += new EventHandler(NewActivity_Click);
			ActFax.Click += new EventHandler(NewActivity_Click);
			ActLetter.Click += new EventHandler(NewActivity_Click);
			ActGeneric.Click += new EventHandler(NewActivity_Click);
			ActCase.Click += new EventHandler(NewActivity_Click);
			ActMemo.Click += new EventHandler(NewActivity_Click);
			ActQuote.Click += new EventHandler(NewActivity_Click);
			this.DocGen.Click += new EventHandler(DocGen_Click);
			Back.Click += new EventHandler(BtnBack_Click);
			this.SheetP.NextClick += new EventHandler(SheetP_NextClick);
			this.SheetP.PrevClick += new EventHandler(SheetP_PrevClick);
			this.ChildAction.Click += new EventHandler(this.ChildAction_Click);
			this.LinkDocument.Click += new EventHandler(this.DownloadFile_Click);
			this.SubmitBtn.Click += new EventHandler(this.Submit_Click);
			this.SubmitBtnDoc.Click += new EventHandler(this.Submit_Click);
			this.RepeaterSearch.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterSearchCommand);
			this.RepeaterSearch.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterSearchDataBound);

		}

		#endregion

		private void DocGen_Click(object sender, EventArgs e)
		{
			if (Activity_Document.SelectedIndex > 0)
			{
				if (((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0)
				{
					int companyId = int.Parse(((TextBox) Page.FindControl("TextboxCompanyID")).Text);
					StringBuilder sb = new StringBuilder();
					string fileName;
					fileName = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + Activity_Document.SelectedValue;
					StreamReader sr = new StreamReader(fileName);
					while (sr.Peek() != -1)
					{
						sb.Append(sr.ReadToEnd());
					}
					sr.Close();

					DataSet dsCompany = DatabaseConnection.CreateDataset("SELECT * FROM QB_CONTACTS_VIEW WHERE ID=" + companyId);
					DataRow drCompany = dsCompany.Tables[0].Rows[0];
					DataSet DSMatching = DatabaseConnection.CreateDataset("SELECT FIELD,MATCHINGVALUE FROM QB_ALL_FIELDS WHERE TABLEID=1 AND MATCHINGVALUE IS NOT NULL");
					foreach (Match match in Regex.Matches(sb.ToString(), @"(\[Tustena\.(?<content>[\s\S]*?)\])", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture))
					{
						string[] datamatch = Regex.Replace(match.Groups["content"].Value, @"}?{?((\n|\\).*?)+\s", "").Split('.');
						Trace.Warn(match.Groups["content"].Value, datamatch[1]);
						bool matched = false;
						foreach (DataRow d in DSMatching.Tables[0].Rows)
						{
							if (datamatch.Length > 1 && d[1].ToString() == datamatch[1].Trim())
							{
								sb.Replace(match.Value, drCompany[d[0].ToString()].ToString());
								matched = true;
							}
						}
						if (!matched)
							sb.Replace(match.Value, "");
					}

					Response.Clear();
					Response.AddHeader("Content-Disposition", "attachment; fileName=test.doc");
					Response.AddHeader("Expires", "Thu, 01 Dec 1994 16:00:00 GMT ");
					Response.AddHeader("Pragma", "nocache");
					Response.ContentType = "application/rtf";
					Response.Write(sb.ToString());
					Response.Flush();
					Response.End();

				}
			}
		}

		internal string AppointmentUpdate(string appId,
		                                  string uID,
		                                  string dt,
		                                  string fromhour,
		                                  string tohour,
		                                  string contactname,
		                                  string contactID,
		                                  string companyname,
		                                  string companyID,
		                                  string note,
		                                  string newId)
		{
			object InsertedID;
			DigiDapter dg = new DigiDapter();
			dg.Add("UID", uID);
			dg.Add("UIDINS", UC.UserId);
			dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(dt + " " + fromhour, UC.myDTFI)));
			dg.Add("ENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(dt + " " + tohour, UC.myDTFI)));
			dg.Add("CONTACT", contactname);
			dg.Add("COMPANY", companyname);
			if (companyID.Length > 0) dg.Add("COMPANYID", companyID);
			if (contactID.Length > 0) dg.Add("CONTACTID", contactID);


			dg.Add("NOTE", note);
			if (appId == newId)
				dg.Where = "ID=" + appId;
			else
				dg.Where = "UID=" + uID + " AND SENCONDIDOWNER=" + newId;

			if (((TextBox) Page.FindControl("IdCompanion")).Text.Length > 0)
			{
				if (uID == ((TextBox) Page.FindControl("IdCompanion")).Text)
					dg.Add("SECONDUID", uID);
				else
					dg.Add("SECONDUID", ((TextBox) Page.FindControl("IdCompanion")).Text);
			}
			if (appId != newId)
				dg.Add("SENCONDIDOWNER", newId);

			InsertedID = dg.Execute("BASE_CALENDAR", "ID=" + appId, DigiDapter.Identities.Identity);

			if (appId == "-1") appId = InsertedID.ToString();

			return appId;


		}

		private void InsertLastContact(string id, byte type, DateTime data)
		{
			string tb = String.Empty;
			switch (type)
			{
				case 0:
					tb = "BASE_COMPANIES";
					break;
				case 1:
					tb = "BASE_CONTACTS";
					break;
				case 2:
					tb = "CRM_LEADS";
					break;
			}

			try
			{
				DateTime oldDate = Convert.ToDateTime(DatabaseConnection.SqlScalar("SELECT LASTCONTACT FROM " + tb + " WHERE ID=" + int.Parse(id)));
				if (oldDate < data)
					DatabaseConnection.DoCommand("UPDATE " + tb + " SET LASTCONTACT='" + data.ToString(@"yyyyMMdd") + "' WHERE ID=" + int.Parse(id));
			}
			catch
			{
				DatabaseConnection.DoCommand("UPDATE " + tb + " SET LASTCONTACT=GETDATE() WHERE ID=" + int.Parse(id));
			}
		}

		private DataSet InitSheetPaging(string sql)
		{
			DataSet ds = DatabaseConnection.CreateDataset(sql);
			DataTable dt = ds.Tables[0];
			string[] myarray = new string[dt.Rows.Count];
			if (dt.Rows.Count > 0)
			{
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					myarray[i] = dt.Rows[i]["ID"] + "|" + dt.Rows[i]["subject"];
				}
			}
			Session["SheetP"] = myarray;
			SheetP.IDArray = myarray;
			return ds;
		}

		private void SheetP_NextClick(object sender, EventArgs e)
		{
			FillForm(this.SheetP.GetCurrentID);
		}

		private void SheetP_PrevClick(object sender, EventArgs e)
		{
			FillForm(this.SheetP.GetCurrentID);
		}


		private void AllActivity_PreRender(object sender, EventArgs e)
		{
			 object IDDocument = Page.FindControl("IDDocument");
			if (IDDocument is TextBox && ((TextBox)IDDocument).Text.Length > 0)
				LinkDocument.Visible = true;
			else
				LinkDocument.Visible = false;
		}
	}
}
