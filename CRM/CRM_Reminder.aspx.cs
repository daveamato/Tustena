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
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class CRMReminder : G
	{


		private string calReminder = String.Empty;


			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
			this.Load += new EventHandler(this.Page_Load);
			this.BtnNew.Click += new EventHandler(this.Btn_Click);
			this.Btnsearch.Click += new EventHandler(this.Btn_Click);
			this.SubmitReminder.Click += new EventHandler(this.Btn_Click);
			this.RepeaterFree.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterFreeCommand);
			this.RepeaterFree.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterFreeDataBound);

			}
		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				BtnNew.Text =Root.rm.GetString("CRMRemtxt4");
				SubmitReminder.Text =Root.rm.GetString("CRMRemtxt5");
				Btnsearch.Text =Root.rm.GetString("CRMRemtxt10");

				DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
				if (Session["today"] == null) Session["today"] = today;
				if (!Page.IsPostBack)
				{
					if (Request.QueryString["d"] != null)
					{
						int a = Convert.ToInt32(Request.QueryString["d"].Substring(0, 4));
						int m = Convert.ToInt32(Request.QueryString["d"].Substring(4, 2));
						int d = Convert.ToInt32(Request.QueryString["d"].Substring(6, 2));
						today = new DateTime(a, m, d, 12, 0, 0);
						Trace.Warn("today", today.ToString());
						Session["today"] = today;
					}
					SelDate.Text = today.ToShortDateString();
					CardReminder.Visible = false;

					FillRepeaterActivity(today, true);
					CalendarReminder(today.Month, today.Year);
					calDate.VisibleDate = today;
				}
				else
				{
					CalendarReminder(calDate.VisibleDate.Month, calDate.VisibleDate.Year);
				}
			}
		}

		protected void Change_Date(object sender, EventArgs e)
		{
			CalendarReminder(calDate.SelectedDate.Month, calDate.SelectedDate.Year);
			DateTime newToday = calDate.SelectedDate.AddHours(12);
			FillRepeaterActivity(newToday, true);

			SelDate.Text = calDate.SelectedDate.ToShortDateString();
			Session["today"] = calDate.SelectedDate;
		}

		public void Btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "btnNew":
					CardReminder.Visible = true;
					RepeaterFree.Visible = false;
					Reminder_ID.Text = "-1";
					Reminder_Reminder.Text = String.Empty;
					Reminder_RemNote.Text = String.Empty;

					HomePage.Visible = false;
					break;
				case "SubmitReminder":
					ModifyReminder();
					ReminderInfo.Text =Root.rm.GetString("CRMRemtxt6");
					ReminderInfo.Visible = true;
					HomePage.Visible = true;
					CardReminder.Visible = false;
					FillRepeaterActivity(calDate.VisibleDate, true);
					break;
				case "btnsearch":
					FillRepeaterActivity(DateTime.Now, false);
					Search.Text = String.Empty;
					break;
			}
		}

		private void ModifyReminder()
		{
			string sqlString = "SELECT ID FROM CRM_REMINDER WHERE ID =" + int.Parse(Reminder_ID.Text + " AND TABLENAME='CRM_REMINDER' AND OWNERID=" + UC.UserId);
			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if (!dg.HasRows)
				{
					dg.Add("OWNERID", UC.UserId, 'I');
					dg.Add("K_ID", 0, 'I');
					dg.Add("TABLENAME", "CRM_Reminder", 'I');
				}
				dg.Add("REMINDERDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Reminder_Reminder.Text, UC.myDTFI)));
				dg.Add("NOTE", (Reminder_RemNote.Text.Length > 0) ? Reminder_RemNote.Text : "");
				dg.Execute("CRM_Reminder", "id=" + dg.ExternalReaderRowId);
			}
		}


		public void RepeaterFreeDataBound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton DelAC = (LinkButton) e.Item.FindControl("Delete");
					DelAC.Text =Root.rm.GetString("CRMRemtxt8");
					DelAC.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("CRMRemtxt9") + "');");
					LinkButton ModAC = (LinkButton) e.Item.FindControl("Modify");
					ModAC.Text =Root.rm.GetString("CRMRemtxt12");
					Literal RemDate = (Literal) e.Item.FindControl("RemDate");
					RemDate.Text = UC.LTZ.ToLocalTime((DateTime) DataBinder.Eval((DataRowView) e.Item.DataItem, "ReminderDate")).ToShortDateString();
					break;
			}
		}

		public void ReminderDataBound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal RemDate = (Literal) e.Item.FindControl("RemDate");
					RemDate.Text = UC.LTZ.ToLocalTime((DateTime) DataBinder.Eval((DataRowView) e.Item.DataItem, "ReminderDate")).ToShortDateString();
					break;
			}
		}

		public void RepeaterFreeCommand(Object sender, RepeaterCommandEventArgs e)
		{
			Literal IDRem = (Literal) e.Item.FindControl("IDRem");
			switch (e.CommandName)
			{
				case "Delete":
					string delSql = "DELETE FROM CRM_REMINDER WHERE ID=" + int.Parse(IDRem.Text);
					DatabaseConnection.DoCommand(delSql);
					break;
				case "Modify":
					string sqlString = "SELECT * FROM CRM_REMINDER WHERE ID=" + int.Parse(IDRem.Text);
					DataSet dr = DatabaseConnection.CreateDataset(sqlString);

					Reminder_Reminder.Text = UC.LTZ.ToLocalTime((DateTime) dr.Tables[0].Rows[0]["ReminderDate"]).ToShortDateString();
					Reminder_RemNote.Text = dr.Tables[0].Rows[0]["note"].ToString();
					Reminder_ID.Text = IDRem.Text;
					CardReminder.Visible = true;
					HomePage.Visible = false;
					ReminderInfo.Visible = false;
					break;
			}
		}


		private void FillRepeaterActivity(DateTime today, bool flag)
		{
			string dateRem = UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd");
			DataSet dsRem = new DataSet();
			string sqlRem = string.Empty;

			DateTime Utctime = UC.LTZ.ToUniversalTime(today);
			TimeSpan mindiffstart = new TimeSpan(today.Ticks - Utctime.Ticks);
			if (flag)
				sqlRem = "SELECT K_ID FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND TABLENAME='CRM_WORKACTIVITY' AND OWNERID=" + UC.UserId + " AND (CONVERT(VARCHAR(10),DATEADD(N,"+mindiffstart.TotalMinutes.ToString()+",REMINDERDATE),112)='" + dateRem + "' OR (DATEDIFF(D,DATEADD(D,-ADVANCEREMIND,REMINDERDATE),'" + dateRem + "') BETWEEN 1 AND ADVANCEREMIND))";
			else
				sqlRem = "SELECT K_ID FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND TABLENAME='CRM_WORKACTIVITY' AND OWNERID=" + UC.UserId + " AND NOTE LIKE '%" + DatabaseConnection.FilterInjection(Search.Text) + "%'";

			Trace.Warn("primaquery",sqlRem);

			dsRem = DatabaseConnection.CreateDataset(sqlRem);


			if (dsRem.Tables[0].Rows.Count > 0)
			{
				StringBuilder sbSqlRem = new StringBuilder();

				foreach (DataRow DRRem in dsRem.Tables[0].Rows)
				{
					sbSqlRem.Append("CRM_WORKACTIVITY.ID=" + DRRem[0].ToString() + " OR ");
				}

				StringBuilder sqlString = new StringBuilder();

				sqlString.Append("SELECT CRM_WORKACTIVITY.*, ACCOUNT.NAME + ' ' + ACCOUNT.SURNAME AS OWNERNAME, ");
				sqlString.Append("BASE_CONTACTS.NAME + ' ' + BASE_CONTACTS.SURNAME AS REFERRINGNAME, ");
				sqlString.Append("BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, ");
				sqlString.Append("CRM_OPPORTUNITY.TITLE AS OPPORTUNITYNAME, CRM_REMINDER.NOTE AS REMINDERNOTE, CRM_REMINDER.REMINDERDATE, CRM_REMINDER.ADVANCEREMIND,CRM_REMINDER.ID AS REMID ");
				sqlString.Append("FROM CRM_WORKACTIVITY ");
				sqlString.Append("LEFT OUTER JOIN CRM_OPPORTUNITY ON CRM_WORKACTIVITY.OPPORTUNITYID = CRM_OPPORTUNITY.ID ");
				sqlString.Append("LEFT OUTER JOIN BASE_COMPANIES ON CRM_WORKACTIVITY.COMPANYID = BASE_COMPANIES.ID ");
				sqlString.Append("LEFT OUTER JOIN BASE_CONTACTS ON CRM_WORKACTIVITY.REFERRERID = BASE_CONTACTS.ID ");
				sqlString.Append("LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID ");
				sqlString.Append("LEFT OUTER JOIN CRM_REMINDER ON CRM_WORKACTIVITY.ID = CRM_REMINDER.K_ID ");
				sqlString.AppendFormat("WHERE ({0}) AND CRM_REMINDER.OWNERID={1} AND CRM_REMINDER.TABLENAME='CRM_WORKACTIVITY'", sbSqlRem.ToString().Substring(0, sbSqlRem.ToString().Length - 3), UC.UserId);

				Trace.Warn("reprem", sqlString.ToString());

				RepeaterActivity.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
				RepeaterActivity.DataBind();
				RepeaterActivity.Visible = true;
				RepeaterActivityInfo.Visible = false;
				if (RepeaterActivity.Items.Count < 1)
				{
					RepeaterActivityInfo.Text =Root.rm.GetString("CRMdeftxt12");
					RepeaterActivityInfo.Visible = true;
					RepeaterActivity.Visible = false;
				}
			}
			else
			{
				RepeaterActivityInfo.Text =Root.rm.GetString("CRMdeftxt12");
				RepeaterActivityInfo.Visible = true;
				RepeaterActivity.Visible = false;
			}

		}


		public void RepeaterActivityDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton OpenActivity = (LinkButton) e.Item.FindControl("OpenActivity");
					string type = DataBinder.Eval((DataRowView) e.Item.DataItem, "Type").ToString();
					switch (type)
					{
						case "1":
							OpenActivity.Text =Root.rm.GetString("Acttxt23");
							break;
						case "2":
							OpenActivity.Text =Root.rm.GetString("Acttxt24");
							break;
						case "3":
							OpenActivity.Text =Root.rm.GetString("Acttxt25");
							break;
						case "4":
							OpenActivity.Text =Root.rm.GetString("Acttxt26");
							break;
						case "5":
							OpenActivity.Text =Root.rm.GetString("Acttxt27");
							break;
						case "6":
							OpenActivity.Text =Root.rm.GetString("Acttxt28");
							break;
					}
					Literal RemDate = (Literal) e.Item.FindControl("RemDate");
					if (Convert.ToInt32(DataBinder.Eval((DataRowView) e.Item.DataItem, "AdvanceRemind")) > 0)
						RemDate.Text = "<img src=/i/bell.gif>&nbsp;" + UC.LTZ.ToLocalTime((DateTime) DataBinder.Eval((DataRowView) e.Item.DataItem, "ReminderDate")).ToShortDateString();
					else
						RemDate.Text = UC.LTZ.ToLocalTime((DateTime) DataBinder.Eval((DataRowView) e.Item.DataItem, "ReminderDate")).ToShortDateString();

					LinkButton DelRem = (LinkButton) e.Item.FindControl("DelRem");
					DelRem.Text =Root.rm.GetString("CRMRemtxt8");
					DelRem.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("CRMRemtxt9") + "');");
					LinkButton ModNote = (LinkButton) e.Item.FindControl("ModNote");
					ModNote.Text =Root.rm.GetString("CRMRemtxt12");
					LinkButton SaveNote = (LinkButton) e.Item.FindControl("SaveNote");
					SaveNote.Text =Root.rm.GetString("CRMRemtxt5");

					break;
			}
		}

		public void RepeaterActivityCommand(object source, RepeaterCommandEventArgs e)
		{
			Trace.Warn("RepeaterActivityCommand", e.CommandName);
			switch (e.CommandName)
			{
				case "OpenActivity":
					Response.Redirect("/WorkingCRM/AllActivity.aspx?m=25&si=38&ac=" + ((Literal) e.Item.FindControl("AcId")).Text);
					break;
				case "OpenCompany":
					SetGoBack("crm_companies.aspx?m=25&si=29&gb=1", new string[2] {((Literal) e.Item.FindControl("CompanyID")).Text, "d"});
					Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
					break;
				case "OpenContact":
					SetGoBack("base_contacts.aspx?m=25&si=31&gb=1", new string[] {((Literal) e.Item.FindControl("CoId")).Text});
					Response.Redirect("/crm/base_contacts.aspx?m=25&si=31&gb=1");
					break;
				case "DelRem":
					string sqlDel = "DELETE FROM CRM_REMINDER WHERE ID=" + int.Parse(((Literal) e.Item.FindControl("RemID")).Text);
					DatabaseConnection.DoCommand(sqlDel);

					DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
					if (Session["today"] != null)
						today = (DateTime) Session["today"];
					FillRepeaterActivity(today, true);
					break;
				case "ModNote":
					TextBox Reminder_RemNote = (TextBox) e.Item.FindControl("Reminder_RemNote");
					Literal LtrRemNote = (Literal) e.Item.FindControl("LtrRemNote");
					Reminder_RemNote.Visible = true;
					LtrRemNote.Visible = false;
					LinkButton SaveNote = (LinkButton) e.Item.FindControl("SaveNote");
					SaveNote.Visible = true;
					LinkButton ModNote = (LinkButton) e.Item.FindControl("ModNote");
					ModNote.Visible = false;
					break;
				case "SaveNote":
					Reminder_RemNote = (TextBox) e.Item.FindControl("Reminder_RemNote");
					LtrRemNote = (Literal) e.Item.FindControl("LtrRemNote");
					LtrRemNote.Text = Reminder_RemNote.Text;
					Reminder_RemNote.Visible = false;
					LtrRemNote.Visible = true;
					SaveNote = (LinkButton) e.Item.FindControl("SaveNote");
					SaveNote.Visible = false;
					ModNote = (LinkButton) e.Item.FindControl("ModNote");
					ModNote.Visible = true;
					DatabaseConnection.DoCommand("UPDATE CRM_REMINDER SET NOTE='" + DatabaseConnection.FilterInjection(Reminder_RemNote.Text) + "' WHERE ID=" + int.Parse(((Literal) e.Item.FindControl("RemID")).Text));
					break;
			}
		}




		public void RepeaterAziendeCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenCompany":
					SetGoBack("crm_companies.aspx?m=25&si=29&gb=1", new string[2] {((Literal) e.Item.FindControl("CompanyID")).Text, "d"});
					Response.Redirect("crm_companies.aspx?m=25&si=29&gb=1");
					break;
			}
		}




		public void RepeaterContattiCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "OpenCompany":
					SetGoBack("base_contacts.aspx?m=25&si=31&gb=1", new string[] {((Literal) e.Item.FindControl("CoId")).Text});
					Response.Redirect("base_contacts.aspx?m=25&si=31&gb=1");
					break;
			}
		}




		private void CalendarReminder(int month, int year)
		{
			DbSqlParameterCollection Msc=new DbSqlParameterCollection();

			DbSqlParameter parameterMonth = new DbSqlParameter("@Month", SqlDbType.Int, 4);
			parameterMonth.Value = month;
			Msc.Add(parameterMonth);

			DbSqlParameter parameterYear = new DbSqlParameter("@Year", SqlDbType.Int, 4);
			parameterYear.Value = year;
			Msc.Add(parameterYear);

			TimeSpan mindiff = UC.LTZ.GetUtcOffset(new DateTime(year, month, 1, 0, 0, 0));

			DbSqlParameter parameterLocalTime = new DbSqlParameter("@LTZ", SqlDbType.Int, 4);
			parameterLocalTime.Value = Convert.ToInt32(mindiff.TotalMinutes); //localOffset.Minutes;
			Msc.Add(parameterLocalTime);

			DbSqlParameter parameterOwnerID = new DbSqlParameter("@OwnerID", SqlDbType.Int, 4);
			parameterOwnerID.Value = UC.UserId;
			Msc.Add(parameterOwnerID);

			DbSqlParameter parameterDays = new DbSqlParameter("@Days", SqlDbType.VarChar, 1000);
			parameterDays.Parameter.Direction = ParameterDirection.Output;
			Msc.Add(parameterDays);

			try
			{
				DatabaseConnection.DoStored("NEW_ReminderCalendar",Msc);

			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{

			}
			try
			{
				calReminder = (string) parameterDays.Value;
			}
			catch
			{
				calReminder = String.Empty;
			}

			Trace.Warn("calreminder", calReminder);
		}

		public void Change_Month(Object source, MonthChangedEventArgs e)
		{
			CalendarReminder(e.NewDate.Month, e.NewDate.Year);
		}

		public void DayRender(Object source, DayRenderEventArgs e)
		{
			if (calReminder.IndexOf("|" + (e.Day.Date.Day).ToString() + "|") > -1
				&& e.Day.Date.Month == calDate.VisibleDate.Month)
				e.Cell.BackColor = Color.LightSteelBlue;

			if (e.Day.Date.Day == calDate.TodaysDate.Day
				&& e.Day.Date.Month == calDate.TodaysDate.Month
				&& e.Day.Date.Year == calDate.TodaysDate.Year)
				e.Cell.BackColor = Color.Gold;
		}


	}
}
