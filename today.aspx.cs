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
using System.Globalization;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class Today : G
	{
		protected DropDownList ListType;
		private ArrayList idDate = new ArrayList();

		private Recurrence recurrence
		{
			get
			{
				return new Recurrence(UC);
			}
		}
		public Today()
		{
		}

		public Today(UserConfig UC)
		{
			this.UC = UC;
		}

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				Session.Remove("goback1");
				Thread.CurrentThread.CurrentCulture = new CultureInfo(UC.Culture);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(UC.Culture);
				Response.Cookies["CulturePref"].Value = UC.Culture;
				Trace.Warn("UC.culturespecific", UC.CultureSpecific);
				Trace.Warn("UC.culture", UC.Culture);

				CheckDB();

				if (Request.QueryString["e"] != null)
				{
					Context.Items.Add("warning",Root.rm.GetString("Warning"));
				}
				if (Request.QueryString["e1"] != null)
				{
					Context.Items.Add("warning",Root.rm.GetString("Warning2"));
				}

				if (UC.FullScreen)
					FullScreen.Text = "<script language=\"JavaScript1.2\" src=\"/js/FullScreen.js\"></script>";
				FillRep();
				FillReminder();

				FilterActivity1.Text=Root.rm.GetString("Deftxt28");
				FilterActivity2.Text=Root.rm.GetString("Deftxt29");
				FilterActivity3.Text=Root.rm.GetString("Deftxt28");
				FilterActivity4.Text=Root.rm.GetString("Deftxt29");

				if(!Page.IsPostBack)
				{
					FilterActivity1.Visible=true;
					FilterActivity2.Visible=false;
					FilterActivity3.Visible=true;
					FilterActivity4.Visible=false;
					string idAgenda;
					idAgenda = DatabaseConnection.SqlScalar("SELECT TOP 1 IDAGENDA FROM TUSTENA_DATA");
					if(idAgenda.Length>0)
					{
						LblCompany.Text=DatabaseConnection.SqlScalar("SELECT TOP 1 COMPANYNAME FROM TUSTENA_DATA");
					}
				}


			}
		}

		private void CheckDB()
		{
			int dbFull = 0;
			string sqlString;
			sqlString = "SELECT COUNT(*) FROM BASE_COMPANIES";
			try
			{
				dbFull = (int)DatabaseConnection.SqlScalartoObj(sqlString);
			}catch
			{
				dbFull=0;
			}

			sqlString = "SELECT COUNT(*) FROM BASE_CONTACTS";
			try
			{
				dbFull += (int)DatabaseConnection.SqlScalartoObj(sqlString);
			}catch
			{}

			if (dbFull == 0)
			{
				if (UC.CultureSpecific.ToLower() == "it")
					Suggestions.Attributes.Add("onclick", "CreateBox('/help/it/dbfull.htm',event,300,190)");
				else
					Suggestions.Attributes.Add("onclick", "CreateBox('/help/en/dbfull.htm',event,300,190)");

				ClientScript.RegisterStartupScript(this.GetType(), "nothing", "<script>var a = clickElement(document.getElementById(\"Suggestions\"));</script>");
			}


		}

		private void FillRep()
		{



			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT COUNT(*) ");
			sqlString.Append("FROM BASE_MESSAGES INNER JOIN ACCOUNT ON BASE_MESSAGES.FROMACCOUNT = ACCOUNT.UID ");
			sqlString.AppendFormat("WHERE TOACCOUNT={0} AND INOUT=0 AND READED=0", UC.UserId.ToString());


			RepMessagesInfo.Text = DatabaseConnection.SqlScalar(sqlString.ToString());
			RepMessagesInfo.Visible = true;

			sqlString.Length = 0;
			sqlString.AppendFormat("SELECT COUNT(*) FROM CRM_TODOLIST_VIEW WHERE (EXPIRATIONDATE>='{0}' OR (EXPIRATIONDATE<'{0}' AND FLAGEXECUTED=0)) AND OWNERID={1};",UC.LTZ.ToUniversalTime(DateTime.Now).ToString(@"yyyyMMdd"), UC.UserId.ToString());

			RepTaskInfo.Text = ((int)DatabaseConnection.SqlScalartoObj(sqlString.ToString())).ToString();
			RepTaskInfo.Visible = true;


			int repEvents = 0;
			repEvents += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM BASE_EVENTS WHERE CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),GETDATE(),112) AND UID=" + UC.UserId.ToString());
			RepEventInfo.Visible = true;



			DataTable dt = DatabaseConnection.CreateDataset("SELECT RECURRID FROM BASE_EVENTS WHERE RECURRID>0 AND UID=" + UC.UserId).Tables[0];

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ArrayList AL = recurrence.Remind((int) dt.Rows[i]["recurrid"], DateTime.Now, DateTime.Now);
				repEvents += AL.Count;
			}
			RepEventInfo.Text = repEvents.ToString();

			ShowAppointments();
			ShowFutureAppointments();

				if (UC.ViewBirthDate)
				{
					SpanBithDate.Visible = true;
					ShowBirthDate();
				}
				else
				{
					SpanBithDate.Visible = false;
				}

				FillRepeaterActivityDay(false);
				FillRepeaterActivityLost(false);


				DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

				int acLost;
				acLost = (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ACTIVITYDATE<'" +UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd") + "' AND ((" + GroupsSecure() + ") OR OWNERID=" + UC.UserId + ")");

				PlaceActivityLost.Text = "(" + acLost.ToString() + ")";
				PlaceActivityLost.Attributes.Add("onclick","location.href='/WorkingCRM/AllActivity.aspx?m=25&dgb=1&si=38&lost=1'");

				if (RepeaterActivityLost.Items.Count > 0)
					AttLost.Visible = true;
				else
					AttLost.Visible = false;
			}

		private void FillRepeaterActivityDay(bool my)
		{

			DataTable dtA = ActivityToday(my);
			RepeaterActivityDay.DataSource = dtA;
			RepeaterActivityDay.DataBind();
			if (RepeaterActivityDay.Items.Count > 0)
				AttInDay.Visible = true;
			else
				AttInDay.Visible = false;
		}

		public DataTable ActivityToday(bool my)
		{
			DateTime fromToday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0,0);
			DateTime today = fromToday.AddMinutes(1439);
			DataTable dtA;

			string fromDate =UC.LTZ.ToUniversalTime(fromToday).ToString(@"yyyyMMdd HH:mm:ss").Replace('.',':');
			string toDate =UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH:mm:ss").Replace('.',':');

			string tempQuery = String.Format("(ACTIVITYDATE BETWEEN '{0}' AND '{1}')",fromDate,toDate);

			if(my)
			{
				dtA = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ("+tempQuery+") AND ((" + GroupsSecure() + ") OR OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE").Tables[0];
			}
			else
			{
				dtA = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ("+tempQuery+") AND (OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE").Tables[0];
			}
			return dtA;
		}

		private void FillRepeaterActivityLost(bool my)
		{
			DataTable dtA = LostActivity(my);
			RepeaterActivityLost.DataSource = dtA;
			RepeaterActivityLost.DataBind();
		}

		public DataTable LostActivity(bool my)
		{
			DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,0,0,0);
			DataTable dtA = new DataTable();

			if(my)
			{
				dtA = DatabaseConnection.CreateDataset("SELECT TOP 10 * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ACTIVITYDATE< '"+UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH.mm.ss").Replace('.',':')+"' AND (("+GroupsSecure()+") OR OWNERID="+UC.UserId+") ORDER BY ACTIVITYDATE DESC").Tables[0];
			}
			else
			{
				dtA = DatabaseConnection.CreateDataset("SELECT TOP 10 * FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND ACTIVITYDATE< '"+UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd HH.mm.ss").Replace('.',':')+"' AND (OWNERID=" + UC.UserId + ") ORDER BY ACTIVITYDATE DESC").Tables[0];
			}
			return dtA;
		}


		public static string ImgType(int aType)
		{
			string img = String.Empty;
			switch (aType)
			{
				case 1:
					img = "<img src=/i/a/Phone.gif border=0>";
					break;
				case 2:
					img = "<img src=/i/a/letter.gif border=0>";
					break;
				case 3:
					img = "<img src=/i/a/fax.gif border=0>";
					break;
				case 4:
					img = "<img src=/i/a/Pin.gif border=0>";
					break;
				case 5:
					img = "<img src=/i/a/Email.gif border=0>";
					break;
				case 6:
					img = "<img src=/i/a/Hands.gif border=0>";
					break;
				case 7:
					img = "<img src=/i/a/generic.gif border=0>";
					break;
				case 8:
					img = "<img src=/i/a/case.gif border=0>";
					break;
				case 9:
					img = "<img src=/i/a/quote.gif border=0>";
					break;
			}
			return img;
		}

		private void ShowBirthDate()
		{
			string sqlString = "SELECT BASE_CONTACTS.ID,ISNULL(BASE_CONTACTS.NAME,'')+' '+BASE_CONTACTS.SURNAME AS REFERENTE ";
			sqlString += "FROM BASE_CONTACTS ";
			sqlString += "WHERE (BASE_CONTACTS.LIMBO = 0) ";
			sqlString += " AND ((BASE_CONTACTS.FLAGGLOBALORPERSONAL=2 AND  BASE_CONTACTS.OWNERID=" + UC.UserId + ") OR (BASE_CONTACTS.FLAGGLOBALORPERSONAL<>2)) ";
			sqlString += " AND (MONTH(BASE_CONTACTS.BIRTHDAY)=" + DateTime.Now.Month + " AND DAY(BASE_CONTACTS.BIRTHDAY)=" + DateTime.Now.Day + ");";

			ContactBirthDate.DataSource = DatabaseConnection.CreateDataset(sqlString);
			ContactBirthDate.DataBind();
			if (ContactBirthDate.Items.Count > 0)
			{
				ContactBirthDate.Visible = true;
				ContactBirthDateInfo.Visible = false;
			}
			else
			{
				ContactBirthDate.Visible = false;
				ContactBirthDateInfo.Visible = true;
				ContactBirthDateInfo.Text =Root.rm.GetString("Deftxt27");
			}
		}
		public void ShowAppointments()
		{
			DataSet dsRem = DatabaseConnection.CreateDataset("SELECT ID,RECURRID FROM BASE_CALENDAR WHERE RECURRID>0 AND UID=" + UC.UserId);
			ArrayList idstr = new ArrayList();
			string query = String.Empty;
			if (dsRem.Tables[0].Rows.Count > 0)
			{
				idstr.Clear();
				foreach (DataRow dr in dsRem.Tables[0].Rows)
				{
					DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
					DateTime dt2 = Convert.ToDateTime(dt1.AddSeconds(86399));
					ArrayList remDates = recurrence.Remind((int) dr["recurrid"], dt1, dt2);

					if (remDates.Count > 0)
					{
						idstr.Add(Convert.ToInt64(dr["id"]));
					}
				}

				dsRem.Clear();
				if (idstr.Count > 0)
				{
					foreach (long id in idstr)
					{
						query += "ID=" + id.ToString() + " OR ";
					}
				}
			}
			if (query.Length > 0)
			{//MYSQL
				RepAppointment.DataSource = DatabaseConnection.CreateReader("SELECT STARTDATE,ENDDATE,CONTACT,COMPANY FROM BASE_CALENDAR WHERE (CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),GETDATE(),112) AND UID=" + UC.UserId + ") OR (" + query.Substring(0, query.Length - 4) + ") ORDER BY STARTDATE;");
			}
			else
			{
				RepAppointment.DataSource = DatabaseConnection.CreateReader("SELECT STARTDATE,ENDDATE,CONTACT,COMPANY FROM BASE_CALENDAR WHERE CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),GETDATE(),112) AND UID='" + UC.UserId + "' ORDER BY STARTDATE;");
			}

			RepAppointment.DataBind();
			if (RepAppointment.Items.Count == 0)
			{
				RepAppointmentInfo.Text =Root.rm.GetString("Deftxt4");
				RepAppointment.Visible = false;
				RepAppointmentInfo.Visible = true;
			}

		}

		public void ShowFutureAppointments()
		{
			DataSet dsRem = DatabaseConnection.CreateDataset("SELECT ID,RECURRID FROM BASE_CALENDAR WHERE RECURRID>0 AND UID=" + UC.UserId);
			ArrayList idstr = new ArrayList();

			string query = String.Empty;
			if (dsRem.Tables[0].Rows.Count > 0)
			{
				idstr.Clear();
				idDate.Clear();
				foreach (DataRow dr in dsRem.Tables[0].Rows)
				{
					DateTime dt1 = Convert.ToDateTime(DateTime.Now.AddDays(1).ToShortDateString());
					DateTime dt2 = Convert.ToDateTime(dt1.AddSeconds(172799));
					ArrayList remDates = recurrence.Remind((int) dr["recurrid"], dt1, dt2);

					if (remDates.Count > 0)
					{
						ViewReminder nr = new ViewReminder();
						idstr.Add(Convert.ToInt64(dr["id"]));
						nr.id=Convert.ToInt64(dr["id"]);
						nr.Date=(DateTime)remDates[0];
						idDate.Add(nr);
					}
				}

				dsRem.Clear();
				if (idstr.Count > 0)
				{
					foreach (long id in idstr)
					{
						query += "ID=" + id.ToString() + " OR ";
					}
				}
			}
			if (query.Length > 0)
			{
				FutureAppointmentRepeater.DataSource = DatabaseConnection.CreateReader("SELECT STARTDATE,ENDDATE,CONTACT,COMPANY,ID FROM BASE_CALENDAR WHERE ((CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),DATEADD(DAY,1,GETDATE()),112) OR CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),DATEADD(DAY,2,GETDATE()),112)) AND UID=" + UC.UserId + ") OR (" + query.Substring(0, query.Length - 4) + ") ORDER BY STARTDATE;");
			}
			else
			{
				FutureAppointmentRepeater.DataSource = DatabaseConnection.CreateReader("SELECT STARTDATE,ENDDATE,CONTACT,COMPANY,ID FROM BASE_CALENDAR WHERE (CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),DATEADD(DAY,1,GETDATE()),112) OR CONVERT(VARCHAR(10),STARTDATE,112)=CONVERT(VARCHAR(10),DATEADD(DAY,2,GETDATE()),112)) AND UID='" + UC.UserId + "' ORDER BY STARTDATE;");
			}

			FutureAppointmentRepeater.DataBind();
			if (FutureAppointmentRepeater.Items.Count == 0)
			{
				FutureAppointmentInfo.Text =Root.rm.GetString("Deftxt4");
				FutureAppointmentRepeater.Visible = false;
				FutureAppointmentInfo.Visible = true;
			}

		}

		public int ShowReminder()
		{
			int recurrenceTask = 0;
			DataSet dsRem = DatabaseConnection.CreateDataset("SELECT ID,FLAGRECURRENCE FROM TODOLIST WHERE OWNERID=" + UC.UserId + " AND FLAGRECURRENCE<>0");
			ArrayList idstr = new ArrayList();

			if (dsRem.Tables[0].Rows.Count > 0)
			{
				idstr.Clear();
				foreach (DataRow dr in dsRem.Tables[0].Rows)
				{
					DateTime dt1 = Convert.ToDateTime(DateTime.Now.ToShortDateString());
					DateTime dt2 = Convert.ToDateTime(dt1.AddDays(1));
					ArrayList remDates = recurrence.Remind((int) dr["FlagRecurrence"], dt1, dt2);

					if (remDates.Count > 0)
					{
						idstr.Add((int) dr["id"]);
					}
				}

				dsRem.Clear();
				if (idstr.Count > 0)
				{
					string query = String.Empty;
					foreach (int id in idstr)
					{
						query += "id=" + id.ToString() + " OR ";
					}

					recurrenceTask = (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM TODOLIST WHERE " + query.Substring(0, query.Length - 4));

				}
				else
				{
					recurrenceTask = 0;
				}

			}
			else
			{
				recurrenceTask = 0;
			}
			return recurrenceTask;
		}


		private void FillReminder()
		{
			int ReminderFull = 0;
			DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			string dateRem =UC.LTZ.ToUniversalTime(today).ToString(@"yyyyMMdd");

			ReminderFull += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND OWNERID=" + UC.UserId.ToString() + " AND CONVERT(VARCHAR(10),REMINDERDATE,112)='" + dateRem + "'");
			ReminderFull += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND TABLENAME='CRM_OPPORTUNITY' AND OWNERID=" + UC.UserId.ToString() + " AND CONVERT(VARCHAR(10),REMINDERDATE,112)='" + dateRem + "'");
			ReminderFull += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND TABLENAME='BASE_COMPANIES' AND OWNERID=" + UC.UserId.ToString() + " AND CONVERT(VARCHAR(10),REMINDERDATE,112)='" + dateRem + "'");
			ReminderFull += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_REMINDER WHERE OPPORTUNITYID<>0 AND TABLENAME='BASE_COMPANIES' AND OWNERID=" + UC.UserId.ToString() + " AND CONVERT(VARCHAR(10),REMINDERDATE,112)='" + dateRem + "'");
			ReminderFull += (int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_REMINDER WHERE OPPORTUNITYID=0 AND TABLENAME='BASE_CONTACTS' AND OWNERID=" + UC.UserId.ToString() + " AND CONVERT(VARCHAR(10),REMINDERDATE,112)='" + dateRem + "'");


			RepeaterActivityInfo.Text = ReminderFull.ToString();
			RepeaterActivityInfo.Visible = true;
		}








		public void RepMenuDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label link = (Label) e.Item.FindControl("LocationHRef");
					string lk = DataBinder.Eval((DataRowView) e.Item.DataItem, "link").ToString();
					string vc =Root.rm.GetString("Menutxt" + DataBinder.Eval((DataRowView) e.Item.DataItem, "rmvalue").ToString());
					string id = DataBinder.Eval((DataRowView) e.Item.DataItem, "id").ToString();
					string fo = DataBinder.Eval((DataRowView) e.Item.DataItem, "folder").ToString();

					if (lk.IndexOf(".aspx") > 0)
						if (fo.Length > 0)
							link.Attributes.Add("onclick",String.Format("location.href='/{2}/{0}&si={1}'",lk, id, fo));
						else
							link.Attributes.Add("onclick",String.Format("location.href='{0}&si={1}'", lk, id));
					else
						link.Attributes.Add("onclick",String.Format("{0}", lk));
					link.Text = vc;


					break;
			}

		}

		public void RepeaterSearchDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal activitywith = (Literal) e.Item.FindControl("activitywith");
					activitywith.Text = (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName")).Trim().Length>0)?Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName")):string.Empty;
					activitywith.Text += (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "ContactName")).Trim().Length>0)?" "+Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "ContactName")):string.Empty;
					activitywith.Text += (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadName")).Trim().Length>0)?" "+Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadName")):string.Empty;

					string id = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					Label Subject = (Label) e.Item.FindControl("Subject");
					int aType = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Type");
					Subject.Text = String.Format("<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac={0}>{1}</a>&nbsp;",id,ImgType(aType));
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Subject"));
					Subject.Text += String.Format("&nbsp;<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac={0}>{1}</a>",id,sub);

					Literal AcDate = (Literal) e.Item.FindControl("AcDate");
					AcDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"),UC.myDTFI)).ToString("g");

					break;

			}
		}

		public void RepeaterSearchCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "":

					break;
			}
		}


		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.FilterActivity1.Click +=new EventHandler(FilterActivity1_Click);
			this.FilterActivity2.Click +=new EventHandler(FilterActivity2_Click);
			this.FilterActivity3.Click +=new EventHandler(FilterActivity3_Click);
			this.FilterActivity4.Click +=new EventHandler(FilterActivity4_Click);
			this.LblCompany.Click +=new EventHandler(Company_Click);
			this.Load += new EventHandler(this.Page_Load);
			this.RepeaterActivityDay.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterSearchCommand);
			this.RepeaterActivityLost.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterSearchCommand);
			this.RepeaterActivityDay.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterSearchDataBound);
			this.RepeaterActivityLost.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterSearchDataBound);
			this.FutureAppointmentRepeater.ItemDataBound+=new RepeaterItemEventHandler(FutureAppointmentRepeater_ItemDataBound);

		}
		#endregion

		private void FilterActivity1_Click(object sender, EventArgs e)
		{

			FilterActivity1.Visible=false;
			FilterActivity2.Visible=true;
			this.FillRepeaterActivityDay(true);

		}

		private void FilterActivity2_Click(object sender, EventArgs e)
		{
			FilterActivity1.Visible=true;
			FilterActivity2.Visible=false;
			this.FillRepeaterActivityDay(false);
		}

		private void FilterActivity3_Click(object sender, EventArgs e)
		{
			FilterActivity3.Visible=false;
			FilterActivity4.Visible=true;
			this.FillRepeaterActivityLost(true);
		}

		private void FilterActivity4_Click(object sender, EventArgs e)
		{
			FilterActivity3.Visible=true;
			FilterActivity4.Visible=false;
			this.FillRepeaterActivityLost(false);
		}

		private void Company_Click(object sender, EventArgs e)
		{
			string idagenda;
			idagenda = DatabaseConnection.SqlScalar("SELECT TOP 1 IDAGENDA FROM TUSTENA_DATA");
			SetGoBack("/today.aspx?m=25&si=25&gb=1",new string[]{idagenda,"d"});
			Response.Redirect("/crm/crm_companies.aspx?m=25&si=29&gb=1");
		}

		private struct ViewReminder
		{
			public long id;
			public DateTime Date;
		}

		private void FutureAppointmentRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					System.Web.UI.HtmlControls.HtmlTableRow approw = (System.Web.UI.HtmlControls.HtmlTableRow)e.Item.FindControl("approw");

					Literal startdate = (Literal)e.Item.FindControl("startdate");
					long idrow = (long)DataBinder.Eval(e.Item.DataItem, "id");

					DateTime dd = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "startdate"));

					startdate.Text=UC.LTZ.ToLocalTime(dd).ToShortDateString();

					if(idDate.Count>0)
					{
						foreach(ViewReminder vr in idDate)
						{
							if(vr.id==idrow)
							{
								startdate.Text=UC.LTZ.ToLocalTime(vr.Date).ToShortDateString();
								break;
							}
						}
					}
					approw.Attributes.Add("onclick",string.Format("location.href='/calendar/agenda.aspx?Date={0}'",startdate.Text));

					break;
			}
		}
	}
}
