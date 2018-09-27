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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public partial class qactivity : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{

			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nologin","<script>parent.location.href=parent.location.href;</script>");
			}
			else
			{
				Response.Cache.SetCacheability(HttpCacheability.NoCache);
				if(!Page.IsPostBack)
				{
					ActivityTable.Visible=false;

					if(Request["Action"]!=null && Request["Action"].Length>0)
						SwitchAction(Request["Action"]);
				}
			}
		}

		private bool GetOpportunityStatus()
		{
			bool oppvalid=false;
			if(Request["contactID"]!=null && Request["contactID"].Length>0)
			{
				string ContactType=string.Empty;
				switch(Request["contactID"].Substring(0,1))
				{
					case "C":

						break;
					case "L":
						ContactType="1";
						break;
					case "A":
						ContactType="0";
						break;
				}
				if(ContactType.Length>0)
				{

					string queryState = "SELECT CRM_OPPORTUNITYTABLETYPE.DESCRIPTION,CRM_CROSSOPPORTUNITY.TYPE,CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=" + Request["contactID"].Substring(1) + " AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID=" + Request["oppID"] + " AND CRM_CROSSOPPORTUNITY.CONTACTTYPE="+ContactType;
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
					FillDropListAZ(CompanyNewStateList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=1 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azStateListSelect.Length > 0) ? azStateListSelect : "");
					FillDropListAZ(CompanyNewPhaseList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=2 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azPhaseListSelect.Length > 0) ? azPhaseListSelect : "");
					FillDropListAZ(CompanyNewProbList, "SELECT K_ID,DESCRIPTION FROM CRM_OPPORTUNITYTABLETYPE WHERE TYPE=3 AND LANG='" + UC.Culture.Substring(0, 2) + "'", "description", "k_id", (azProbListSelect.Length > 0) ? azProbListSelect : "");

					FillLostReason();

					DataSet tempDS = DatabaseConnection.CreateDataset("SELECT LOSTREASON FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE="+ContactType+" AND OPPORTUNITYID=" + Request["oppID"] + " AND CONTACTID=" + Request["contactID"].Substring(1));

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

					oppvalid=true;
				}

			}
			return oppvalid;
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

		}

		private void SwitchAction(string action)
		{
			int contactID=0;
			int leadId=0;
			int companyID=0;
			QuickLog ql = new QuickLog();
			if(Request["contactID"]!=null && Request["contactID"].Length>0)
					{
						switch(Request["contactID"].Substring(0,1))
						{
							case "C":
								contactID = int.Parse(Request["contactID"].Substring(1));
								ql.InsertLog(0,contactID,(long)UC.UserId);
								break;
							case "L":
								leadId = int.Parse(Request["contactID"].Substring(1));
								ql.InsertLog(1,leadId,(long)UC.UserId);
								break;
							case "A":
								companyID = int.Parse(Request["contactID"].Substring(1));
								ql.InsertLog(2,companyID,(long)UC.UserId);
								break;
						}

				int crosstype=0;
				switch(Request["contactID"].Substring(0,1))
				{
					case "A":
						crosstype=0;
						break;
					case "C":
						crosstype=1;
						break;
					case "L":
						crosstype=2;
						break;
				}
				string crosswith = Request["contactID"].Substring(1);
                string actid = Request["actID"].ToString();
				QuickActivity1.CrossID=crosswith;
				QuickActivity1.CrossType=crosstype;
                QuickActivity1.ActID = actid;
			}

			if(Request["oppID"]!=null && Request["oppID"].Length>0)
			{
				GetOpportunityStatus();
				QuickActivity1.OppId=Convert.ToInt32(Request["oppID"]);
				tdOpportunity.Visible=true;
			}else
				tdOpportunity.Visible=false;
			switch(action)
			{
				case "ViewActivity":
					if(Request["contactID"]!=null && Request["contactID"].Length>0)
					{
						FillRepeaterActivityDayFromSearch(Request["contactID"]);
					}
					break;
				case "PhoneIn":
					QuickActivity1.ActType=ActivityTypeN.PhoneCall;
					QuickActivity1.FillPreset();
					ActivityTable.Visible=true;
					RepeaterActivityDay.Visible=false;
					break;
				case "PhoneOut":
					QuickActivity1.ActType=ActivityTypeN.PhoneCall;
					ActivityTable.Visible=true;
					QuickActivity1.FillPreset();
					RepeaterActivityDay.Visible=false;
					break;
				case "Fax":
					QuickActivity1.ActType=ActivityTypeN.Fax;
					ActivityTable.Visible=true;
					QuickActivity1.FillPreset();
					RepeaterActivityDay.Visible=false;
					break;
				case "Letter":
					QuickActivity1.ActType=ActivityTypeN.Letter;
					ActivityTable.Visible=true;
					QuickActivity1.FillPreset();
					RepeaterActivityDay.Visible=false;
					break;
				case "Generic":
					QuickActivity1.ActType=ActivityTypeN.Generic;
					ActivityTable.Visible=true;
					QuickActivity1.FillPreset();
					RepeaterActivityDay.Visible=false;
					break;
				case "TodayActivity":
					FillRepeaterActivityDay(true);
					break;
				case "TodoActivity":
					FillRepeaterActivityLost(true);
					break;
			}
			ClientScript.RegisterStartupScript(this.GetType(), "log","<script>refreshlog();</script>");
		}

		private void FillRepeaterActivityLost(bool onlyMy)
		{
			DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
			DataTable dtA = new DataTable();

			if(!onlyMy)
			{
				dtA = DatabaseConnection.CreateDataset("SELECT TOP 10 * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ACTIVITYDATE< '"+UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH.mm.ss").Replace('.',':')+"' AND (("+GroupsSecure()+") OR OWNERID="+UC.UserId+") ORDER BY ACTIVITYDATE").Tables[0];
			}
			else
			{
				dtA = DatabaseConnection.CreateDataset("SELECT TOP 10 * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ACTIVITYDATE< '"+UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH.mm.ss").Replace('.',':')+"' AND (OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE").Tables[0];
			}
			if(dtA.Rows.Count>0)
			{
				RepeaterActivityDay.DataSource = dtA;
				RepeaterActivityDay.DataBind();
				LitRepeaterActivityDayInfo.Visible=false;
			}
			else
			{
				LitRepeaterActivityDayInfo.Text=Root.rm.GetString("Acttxt40");
				LitRepeaterActivityDayInfo.Visible=true;
				RepeaterActivityDay.Visible=false;
			}
		}

		private void InsertQuickActivity(int type, int contactID, int leadId, int companyID, string subject, bool inOut)
		{
			ActivityInsert ai = new ActivityInsert();
			ai.InsertActivity(type.ToString(), string.Empty, UC.UserId.ToString(), (contactID==0)?string.Empty:contactID.ToString(), (companyID==0)?string.Empty:companyID.ToString(), (leadId==0)?string.Empty:leadId.ToString(), subject, string.Empty, UC.LTZ.ToUniversalTime(DateTime.Now), UC, 0, inOut, 0 , 0, 0);
		}

		private void FillRepeaterActivityDayFromSearch(string contactID)
		{
			FillRepeaterActivityDayFromSearch(contactID,"activitydate");
		}

		private void FillRepeaterActivityDayFromSearch(string contactID,string order)
		{
			string query = string.Empty;
			switch(contactID.Substring(0,1))
			{
				case "C":
					if(Request["oppID"]!=null && Request["oppID"].Length>0)
					{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE OPPORTUNITYID=" + Request["oppID"] + " AND REFERRERID=" + contactID.Substring(1) + " ORDER BY "+order+" DESC";
					}
					else
					{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE REFERRERID=" + contactID.Substring(1) + " ORDER BY "+order+" DESC";
					}
						break;
						case "L":
							if(Request["oppID"]!=null && Request["oppID"].Length>0)
							{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE OPPORTUNITYID=" + Request["oppID"] + " AND LEADID=" + contactID.Substring(1) + "  ORDER BY "+order+" DESC";
							}
							else
							{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE LEADID=" + contactID.Substring(1) + "  ORDER BY "+order+" DESC";
							}
							break;
								case "A":
									if(Request["oppID"]!=null && Request["oppID"].Length>0)
									{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE OPPORTUNITYID=" + Request["oppID"] + " AND COMPANYID=" + contactID.Substring(1) + "  ORDER BY "+order+" DESC";
									}
									else
									{
						query = "SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE COMPANYID=" + contactID.Substring(1) + " ORDER BY "+order+" DESC";
									}
								break;
							}

			DataTable dtA = DatabaseConnection.CreateDataset(query).Tables[0];
			if(dtA.Rows.Count>0)
			{
				RepeaterActivityDay.DataSource = dtA;
				RepeaterActivityDay.DataBind();
				LitRepeaterActivityDayInfo.Visible=false;
				RepeaterActivityDay.Visible=true;
			}
			else
			{
				LitRepeaterActivityDayInfo.Text=Root.rm.GetString("Acttxt40");
				LitRepeaterActivityDayInfo.Visible=true;
				RepeaterActivityDay.Visible=false;
			}
		}

		private void FillRepeaterActivityDay(bool my)
		{

			DateTime fromToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0,0);
			DateTime today = fromToday.AddMinutes(1439);
			DataTable dtA;

			string fromDate =UC.LTZ.ToUniversalTime(fromToday).ToString(@"yyyyMMdd HH:mm:ss").Replace('.',':');
			string toDate =UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH:mm:ss").Replace('.',':');

			string tempQuery = String.Format("(ACTIVITYDATE BETWEEN '{0}' AND '{1}')",fromDate,toDate);

			Trace.Warn("actoday","SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ("+tempQuery+") AND ((" + GroupsSecure() + ") OR OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE");

			if(my)
			{
				dtA = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ("+tempQuery+") AND ((" + GroupsSecure() + ") OR OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE").Tables[0];
			}
			else
			{
				dtA = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ("+tempQuery+") AND (OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE").Tables[0];
			}
				if(dtA.Rows.Count>0)
				{
					RepeaterActivityDay.DataSource = dtA;
					RepeaterActivityDay.DataBind();
					LitRepeaterActivityDayInfo.Visible=false;
				}
				else
				{
					LitRepeaterActivityDayInfo.Text=Root.rm.GetString("Acttxt40");
					LitRepeaterActivityDayInfo.Visible=true;
					RepeaterActivityDay.Visible=false;
				}

			}

		public void RepeaterSearchDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					string id = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					Literal Subject = (Literal) e.Item.FindControl("Subject");
					int aType = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Type");
					Subject.Text = "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + ">" + Today.ImgType(aType) + "</a>&nbsp;";
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Subject"));

					Subject.Text += "&nbsp;<a href='javascript:OpenActivity(" + id + ")'>" + sub + "</a>";

					Literal AcDate = (Literal) e.Item.FindControl("AcDate");
					AcDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"),UC.myDTFI)).ToString("g");

					break;

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
			this.RepeaterActivityDay.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterSearchDataBound);
			this.QuickActivity1.ActivitySaved+=new activitySaved(QuickActivity1_ActivitySaved);
		}

		#endregion

		private void QuickActivity1_ActivitySaved()
		{
			ActivityTable.Visible=false;
			if(Request["oppID"]!=null && Request["oppID"].Length>0 && Request["contactID"].Substring(0,1)!="C")
			{
				int compId = int.Parse(Request["contactID"].Substring(1));
				int opId  = int.Parse(Request["oppID"]);
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
			}
			FillRepeaterActivityDayFromSearch(Request["contactID"]);
		}

		private void ModifyCrossOpportunity(int conID, int opID, int valueID, string type)
		{
			int ContactType=0;
			switch(Request["contactID"].Substring(0,1))
			{
				case "C":

					break;
				case "L":
					ContactType=1;
					break;
				case "A":
					ContactType=0;
					break;
			}
			string sqlString = String.Format("SELECT ID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTTYPE="+ContactType+" AND CONTACTID = {0} AND OPPORTUNITYID = {1} AND TYPE= {2};", conID, opID, type);

			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if(!dg.HasRows)
				{
					dg.Add("OPPORTUNITYID", opID,'I');
					dg.Add("CONTACTID", conID,'I');
					dg.Add("TYPE", type,'I');
					dg.Add("CONTACTTYPE", ContactType,'I');
				}


				dg.Add("TABLETYPEID", valueID);
				if(dg.HasRows)
					dg.Execute("CRM_CROSSOPPORTUNITY","ID="+dg.ExternalReader[0]);
				else
					dg.Execute("CRM_CROSSOPPORTUNITY","ID=-1");

			}
		}

	}
}
