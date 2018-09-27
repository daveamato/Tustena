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
using System.Drawing;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class Agenda : G
	{
		public DataTable CalendarDataTable;
		public DataTable eventDataTable;


		private Recurrence _recurrence=null;
		private Recurrence recurrence
			{
			get
			{
				if(_recurrence==null)
				_recurrence = new Recurrence(UC);
				return _recurrence;
			}
		}

		private string calReminder = String.Empty;
		private static int europeCalendar = 0;


			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
				this.Load += new EventHandler(this.Page_Load);
			this.ImpButtonOn.Click += new EventHandler(this.ImpersonateOn_Click);
			this.ImpButton.Click += new EventHandler(this.ImpersonateOff_Click);
			this.OfficeOn.Click += new EventHandler(this.ImpersonateOfficeOn_Click);
			this.ImpOfficebutton.Click += new EventHandler(this.ImpersonateOfficeOff_Click);
			this.CalOfficePrevWeek.Click += new EventHandler(this.CalOfficeWeek_Click);
			this.CalOfficeNextWeek.Click += new EventHandler(this.CalOfficeWeek_Click);

			}
		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Session["backafterlogin"] = "/calendar/agenda.aspx?m=25&si=2";
				Response.Redirect("/login.aspx");
			}
			else
			{
                ClientScript.RegisterClientScriptBlock(this.GetType(), "msgstr", string.Format("<script>confmsg=\"{0}\";confmsg2=\"{1}\";confmsg3=\"{2}\"</script>",Root.rm.GetString("Evnttxt69"),Root.rm.GetString("Evnttxt70"),Root.rm.GetString("Evnttxt71")));
				DeleteGoBack();

				europeCalendar = (UC.FirstDayOfWeek)?0:1;

				Back.Click += new EventHandler(BtnBack_Click);
				Back.CssClass = "sideBtn";
				Back.Text = Root.rm.GetString("behind");
				calDate.Visible = false;

				FillImpersUser();
				GetOwnerName();
				Setvisoffices(false);
				HeaderCalOfficeso.Visible = false;
				if (UC.ImpersonateUser != 0)
				{
					ImpButton.Visible = true;
					ImpersonateUser.Visible = false;

					ImpButtonOn.Visible = false;
				}

				if (UC.ImpersonateOffice != 0)
				{
					Setvisoffices(true);
					ImpOfficebutton.Visible = true;
					ImpersonateOffice.Visible = false;
					OfficeOn.Visible = false;
				}

				ImpButtonOn.Attributes.Add("onclick","if(ImpersonateID.value.length==0){return false;}");
				ImpButtonOn.Text =Root.rm.GetString("Caltxt13");
				ImpButtonOn.Attributes.Add("onclick","return ValidImpersonate()");
				ImpButton.Text =Root.rm.GetString("Caltxt14");
				OfficeOn.Text =Root.rm.GetString("Caltxt16");
				ImpOfficebutton.Text =Root.rm.GetString("Caltxt17");
				CalOfficePrevWeek.Text =Root.rm.GetString("Caltxt49");
				CalOfficeNextWeek.Text =Root.rm.GetString("Caltxt50");

				if (!isGoBack) Back.Visible = false;
				if (!Page.IsPostBack)
				{
					if (Request.Params["month"] != null && Request.Params["year"] != null)
					{
						FillCalendar(Convert.ToInt32(Request.Params["month"]), Convert.ToInt32(Request.Params["year"]));
						if(Convert.ToInt32(Request.Params["month"])==DateTime.Now.Month && Convert.ToInt32(Request.Params["year"])==DateTime.Now.Year)
							FillWeek(DateTime.Now);
						else
							FillWeek(Convert.ToDateTime(Request.Params["month"] + "/01/" + Request.Params["year"], InvariantCultureForDB));

					}
					else
					{
						if (Request.Params["m"] != null && Request.Params["date"] == null)
						{
							FillCalendar(DateTime.Now.Month, DateTime.Now.Year);
							if (Session["Week"] == null)
								FillWeek(DateTime.Now);
							else
								FillWeek(Convert.ToDateTime(Session["Week"]));
						}
					}

					if (Request.Params["date"] != null)
					{
						try
						{
							DateTime tempDate = Convert.ToDateTime(Request.Params["date"]);
							Session["Week"] = tempDate;
							FillCalendar(tempDate.Month, tempDate.Year);
							FillWeek(tempDate);
						}
						catch
						{
							DateTime tempDate = DateTime.Now;
							Session["Week"] = tempDate;
							FillCalendar(tempDate.Month, tempDate.Year);
							FillWeek(tempDate);
						}
					}
					if (Request.Params["del"] != null)
					{
						Delete(int.Parse(Request.Params["del"]));
						DateTime tempDate = Convert.ToDateTime(Request.Params["datedel"]);
						FillCalendar(tempDate.Month, tempDate.Year);
						FillWeek(tempDate);
					}
					else if (Request.Params["dels"] != null)
					{
						DeleteSecondary(int.Parse(Request.Params["dels"]));
						DateTime tempDate = Convert.ToDateTime(Request.Params["datedel"]);
						FillCalendar(tempDate.Month, tempDate.Year);
						FillWeek(tempDate);
					}
					else if (Request.Params["conf"] != null)
					{
						Confirm(int.Parse(Request.Params["conf"]));
						DateTime tempDate = Convert.ToDateTime(Request.Params["datedel"]);
						FillCalendar(tempDate.Month, tempDate.Year);
						FillWeek(tempDate);
					}

					if (Request.Params["datauff"] != null)
					{
						if(UC.Office>0)
						{
							CalendarioMensile.Visible = false;
							Week.Visible = false;
							DailyPanel.Visible = false;
							CalOfficeso.Visible = true;
							WeekDate.Text = Request.Params["datauff"];
							FillOfficeCalendar(Convert.ToDateTime(Request.Params["datauff"]));
						}
						else
						{
							ClientScript.RegisterStartupScript(this.GetType(), "nooffice","<script>alert('" + Root.rm.GetString("NeedOffice")+"')</script>");
						}
					}

					if (Request.Params["dy"] != null)
					{
						CalendarioMensile.Visible = false;
						Week.Visible = false;
						CalOfficeso.Visible = false;
						DailyPanel.Visible = true;
						try
						{
							AgTitle.Text = AgendaOwner.Text + "&nbsp;&nbsp;&nbsp;" + Convert.ToDateTime(Request.Params["dy"],UC.myDTFI).ToLongDateString() + "</center>";
							FillDay(Convert.ToDateTime(Request.Params["dy"],UC.myDTFI));
						}
						catch
						{
							AgTitle.Text = AgendaOwner.Text + "&nbsp;&nbsp;&nbsp;" + DateTime.Now.ToLongDateString() + "</center>";
							FillDay(DateTime.Now);
						}

						CalendarDay(DateTime.Now.Month, DateTime.Now.Year);
						calDate.VisibleDate = DateTime.Now;
					}


				}
			}
		}

        private void Setvisoffices(bool v)
        {
            visoffices.Visible = v;
            visoffices1.Visible = v;
            visoffices2.Visible = v;
        }

		private void FillCalendar(int month, int year)
		{
			StringBuilder calenderStr = new StringBuilder();

			DateTime startDate = new DateTime(year, month, 1);
			int currentUId = (UC.ImpersonateUser == 0) ? UC.UserId : UC.ImpersonateUser;
			DateTime LastDay = LastDayOfMonth(month, year);

			DateTime FirstWeekDayNextMonth = LastDay.AddDays(1);
			DateTime FirstWeekNextMonth = LastDay.AddDays(7 - ((int) LastDay.DayOfWeek));

			DateTime LastWeekPrevMonth = startDate.AddDays(-7);

			StringBuilder sqlCalendar = new StringBuilder();



			TimeSpan mindiffstart = UC.LTZ.GetUtcOffset(new DateTime(year,month,1,0,0,0));

			TimeSpan mindiffend =UC.LTZ.GetUtcOffset(new DateTime(year,month,DateTime.DaysInMonth(year,month),0,0,0));

			bool dayLightSavingMonth = (!mindiffstart.Equals(mindiffend));


			if(dayLightSavingMonth)
			{
				sqlCalendar.AppendFormat("SELECT ID,UID,SECONDUID,STARTDATE,ENDDATE,RECURRID,CONFIRMATION,CONTACT,COMPANY,NOTE,DATEADD(N,{0},STARTDATE) AS LOCALSTARTDATE,DATEADD(N,{0},ENDDATE) AS LOCALENDDATE,CONVERT(VARCHAR(10),DATEADD(N,{0},STARTDATE),112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR FROM BASE_CALENDAR WHERE ((RECURRID > 0 AND STARTDATE<'{4}') OR (STARTDATE BETWEEN '{2}' AND '{3}')) AND UID={1} ORDER BY STARTDATE,ENDDATE;", mindiffstart.TotalMinutes.ToString(), currentUId, LastWeekPrevMonth.ToString(@"yyyyMMdd"), FirstWeekNextMonth.ToString(@"yyyyMMdd"),FirstWeekDayNextMonth.ToString(@"yyyyMMdd"));
			}
			else
			{
				sqlCalendar.AppendFormat("SELECT ID,UID,SECONDUID,STARTDATE,ENDDATE,RECURRID,CONFIRMATION,CONTACT,COMPANY,NOTE,DATEADD(N,{0},STARTDATE) AS LOCALSTARTDATE,DATEADD(N,{0},ENDDATE) AS LOCALENDDATE,CONVERT(VARCHAR(10),DATEADD(N,{0},STARTDATE),112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR FROM BASE_CALENDAR WHERE ((RECURRID > 0 AND STARTDATE<'{4}') OR (STARTDATE BETWEEN '{2}' AND '{3}')) AND UID={1} ORDER BY STARTDATE,ENDDATE;", mindiffstart.TotalMinutes.ToString(), currentUId, LastWeekPrevMonth.ToString(@"yyyyMMdd"), FirstWeekNextMonth.ToString(@"yyyyMMdd"),FirstWeekDayNextMonth.ToString(@"yyyyMMdd"));
			}

			sqlCalendar.AppendFormat("SELECT ID,STARTDATE,TITLE,NOTE,RECURRID,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE FROM BASE_EVENTS WHERE ((MONTH(STARTDATE)={0} AND YEAR(STARTDATE)={1}) OR (RECURRID > 0) OR (STARTDATE BETWEEN '{3}' AND '{4}') OR (STARTDATE BETWEEN '{5}' AND '{6}')) AND UID={2} ORDER BY STARTDATE;", month, year, currentUId, FirstWeekDayNextMonth.ToString(@"yyyyMMdd"), FirstWeekNextMonth.ToString(@"yyyyMMdd"), LastWeekPrevMonth.ToString(@"yyyyMMdd"), startDate.ToString(@"yyyyMMdd"));

			Trace.Warn("sqlcal", sqlCalendar.ToString());
			DataSet dsCalendar = DatabaseConnection.CreateDataset(sqlCalendar.ToString());
			CalendarDataTable = dsCalendar.Tables[0];
			eventDataTable = dsCalendar.Tables[1];

			DataColumn dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "isRec";
			dcDynColumn.DataType = Type.GetType("System.Byte");
			dcDynColumn.DefaultValue = 0;
			CalendarDataTable.Columns.Add(dcDynColumn);
			DataColumn dcDynColumn2 = new DataColumn();
			dcDynColumn2.ColumnName = "isRec";
			dcDynColumn2.DataType = Type.GetType("System.Byte");
			dcDynColumn2.DefaultValue = 0;
			eventDataTable.Columns.Add(dcDynColumn2);
			int rowsToCicleCal = CalendarDataTable.Rows.Count;
			int rowsToCicleEvn = eventDataTable.Rows.Count;

			if (CalendarDataTable.Select("RecurrID>0").Length > 0)
			{
				DateTime MDT = new DateTime(year,month,1);
				DateTime MDT2 = MDT.AddDays(DateTime.DaysInMonth(year, month) - 1);


				for (int i = 0; i < rowsToCicleCal; i++)
				{
					ArrayList AL = recurrence.Remind((int) CalendarDataTable.Rows[i]["recurrid"], MDT, MDT2);

					if (AL.Count > 0)
					{
						foreach (DateTime ALT in AL)
						{
							if(ALT.ToString(@"yyyyMMdd").Equals(CalendarDataTable.Rows[i]["ISOSTARTDATE"].ToString()))
								continue;
							DataRow row = CalendarDataTable.NewRow();
							for (int i2 = 0; i2 < CalendarDataTable.Columns.Count - 1; i2++)
								row[i2] = CalendarDataTable.Rows[i][i2];
							row["isRec"] = 1;
							row["Localstartdate"] = ALT.Date.Add(((DateTime) CalendarDataTable.Rows[i]["startdate"]).TimeOfDay);
							row["Localenddate"] = ALT.Date.Add(((DateTime) CalendarDataTable.Rows[i]["enddate"]).TimeOfDay);
							row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
							CalendarDataTable.Rows.Add(row);
						}
						if (Convert.ToInt32(((string) CalendarDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != month)
							CalendarDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
					}
				}
			}

			if (eventDataTable.Select("RecurrID>0").Length > 0)
			{
				DateTime MDT = new DateTime(year,month,1);

				DateTime MDT2 = MDT.AddDays(DateTime.DaysInMonth(year, month) - 1);

				for (int i = 0; i < rowsToCicleEvn; i++)
				{
					ArrayList AL = recurrence.Remind((int) eventDataTable.Rows[i]["recurrid"], MDT, MDT2);

					if (AL.Count > 0)
					{
						foreach (DateTime ALT in AL)
						{
							DataRow row = eventDataTable.NewRow();
							for (int i2 = 0; i2 < eventDataTable.Columns.Count - 1; i2++)
								row[i2] = eventDataTable.Rows[i][i2];
							row["isRec"] = 1;
							row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
							eventDataTable.Rows.Add(row);
						}

						if (Convert.ToInt32(((string) eventDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != month)
							eventDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
					}
				}
			}


			Current.Text =UC.myDTFI.GetMonthName(startDate.Month).ToUpper() + "&nbsp;<span class=HeaderG onmouseover=\"showmenu(event,linkset[4])\" onMouseout=\"dhm()\">" + startDate.Year.ToString() + "</span>";
			CurrentMonth.Text = startDate.Month.ToString();
			CurrentYear.Text = startDate.Year.ToString();

			int prevMonth, prevyear, nextmonth, nextyear;
			if (startDate.Month == 1)
			{
				prevMonth = 12;
				prevyear = startDate.Year - 1;
			}
			else
			{
				prevMonth = startDate.Month - 1;
				prevyear = startDate.Year;
			}
			if (startDate.Month == 12)
			{
				nextmonth = 1;
				nextyear = startDate.Year + 1;
			}
			else
			{
				nextmonth = startDate.Month + 1;
				nextyear = startDate.Year;
			}

			StringBuilder jsc = new StringBuilder();
			jsc.Append("<script language=\"JavaScript1.2\">");
			jsc.Append("var linkset=new Array();" + (char) 13);
			string linkOut = String.Empty;
			for (int calMonth = 1; calMonth <= prevMonth; calMonth++)
			{
				linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"location.href=\\'agenda.aspx?month=" + calMonth + "&year=" + startDate.Year + "\\'\\\">&nbsp;&nbsp;" +UC.myDTFI.GetMonthName(calMonth).ToUpper() + "&nbsp;&nbsp;</div>";
			}
			jsc.Append("linkset[0]='" + linkOut + "';" + (char) 13);
			linkOut = String.Empty;
			for (int calMonth = nextmonth; calMonth < 13; calMonth++)
			{
				linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"location.href=\\'agenda.aspx?month=" + calMonth + "&year=" + startDate.Year + "\\'\\\">&nbsp;&nbsp;" +UC.myDTFI.GetMonthName(calMonth).ToUpper() + "&nbsp;&nbsp;</div>";
			}

			string firstHour = (new DateTime(2000,10,10,UC.StartHourAM,0,0,0)).ToShortTimeString();

			jsc.Append("linkset[1]='" + linkOut + "';" + (char) 13);
			linkOut = "<div class=\\\"menuitems\\\" onclick=\\\"OpenTEvent(\\'NEW\\',dayid + \\'/" + startDate.Month + "/" + startDate.Year + "&ora1="+firstHour+"\\');\\\">&nbsp;&nbsp;" +Root.rm.GetString("Caltxt6") + "&nbsp;&nbsp;</div>";
			linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"OpenMeeting(dayid + \\'/" + startDate.Month + "/" + startDate.Year + "\\');\\\">&nbsp;&nbsp;" +Root.rm.GetString("Caltxt45") + "&nbsp;&nbsp;</div>";
			linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"OpenEventS(\\'NEW\\',dayid + \\'/" + startDate.Month + "/" + startDate.Year + "&ora1="+firstHour+"\\');\\\">&nbsp;&nbsp;" +Root.rm.GetString("Caltxt7") + "&nbsp;&nbsp;</div>";
			linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"viewday(dayid,\\'" + startDate.Month + "\\',\\'" + startDate.Year + "\\');\\\">&nbsp;&nbsp;" +Root.rm.GetString("Caltxt29") + "&nbsp;&nbsp;</div>";
			jsc.Append("linkset[2]='" + linkOut + "';" + (char) 13);
			linkOut = String.Empty;
			for (int calMonth = startDate.AddYears(-5).Year; calMonth <= startDate.AddYears(+5).Year; calMonth++)
			{
				linkOut += "<div class=\\\"menuitems\\\" onclick=\\\"location.href=\\'agenda.aspx?month=" + startDate.Month + "&year=" + calMonth + "\\'\\\">&nbsp;&nbsp;" + calMonth.ToString() + "&nbsp;&nbsp;</div>";
			}
			jsc.Append("linkset[4]='" + linkOut + "';" + (char) 13);
			linkOut = String.Empty;
			jsc.AppendFormat("var wk = '{0}';",Root.rm.GetString("Caltxt44"));
			jsc.Append("</script>");
			Jscriptmenu.Text = jsc.ToString();
			Previous.Text = "<span style=\"cursor:pointer\" onMouseover=\"showmenu(event,linkset[0])\" onMouseout=\"dhm()\">&nbsp;&lt;&lt;</span>";
			Previous.Text += "<a href=\"agenda.aspx?month=" + prevMonth + "&year=" + prevyear + "\"><span class=\"HeaderP\">&nbsp;" +UC.myDTFI.GetMonthName(prevMonth).ToUpper() + "</span></a>";
			Next.Text = "<a href=\"agenda.aspx?month=" + nextmonth + "&year=" + nextyear + "\"><span class=\"HeaderP\">" +UC.myDTFI.GetMonthName(nextmonth).ToUpper() + "</span></a>";
			Next.Text += "<span style=\"cursor:pointer\" onMouseover=\"showmenu(event,linkset[1])\" onMouseout=\"dhm()\">&nbsp;&gt;&gt;</span>";


			StringBuilder remTable = new StringBuilder();
			remTable.AppendFormat("SELECT ID,CAST(CONVERT(VARCHAR(10),DATEADD(N,{0},REMINDERDATE),112)AS INT) AS REMINDERISODATE, ", mindiffstart.TotalMinutes.ToString());
			remTable.AppendFormat("CAST(CONVERT(VARCHAR(10),DATEADD(DD,-ADVANCEREMIND,DATEADD(N,{0},REMINDERDATE)),112) AS INT) AS REMINDERISOALLARM,", mindiffstart.TotalMinutes.ToString());
			remTable.Append("ADVANCEREMIND ");
			remTable.Append("FROM CRM_REMINDER ");
			remTable.AppendFormat("wHERE REMINDERDATE BETWEEN '{0}' AND '{1}' ",UC.LTZ.ToUniversalTime(startDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(LastDay).ToString(@"yyyyMMdd"));
			remTable.AppendFormat("AND OWNERID={0} ", currentUId);

			Trace.Warn("remtable", remTable.ToString());

			DataTable ReminderTable = DatabaseConnection.CreateDataset(remTable.ToString()).Tables[0];

			int weekDay;
			string bgColor, ggcolor;
			string holiday;
			string holidayText;
			string existsEvent;
			string existsReminder;
			StringBuilder weekDaysTitle = new StringBuilder();
			DateTime weekStart;
			DateTime day = DateTime.Now;
			if ((Convert.ToInt32(day.DayOfWeek) == 0 && europeCalendar == 1) || (Convert.ToInt32(day.DayOfWeek) == 6 && europeCalendar == 0))
			{
				weekStart = day.AddDays(-6);
			}
			else
			{
				weekStart = day.AddDays(-(Convert.ToInt32(day.DayOfWeek) - europeCalendar));
			}

			for (int wd = 0; wd < 7; wd++)
			{
				weekDaysTitle.AppendFormat("<td width=\"14%\" align=\"center\" Class=\"ListResultTitle\" style=\"cursor:default\">{0}</td>",UC.myDTFI.GetDayName(weekStart.AddDays(wd).DayOfWeek));
			}
			DaysTitle.Text =  weekDaysTitle.ToString();
			for (int wd = 1; wd < 7; wd++)
			{
				calenderStr.Append("<tr>");
				for (int x = europeCalendar; x < 7+europeCalendar; x++)
				{
					if (recurrence.isCelebration(startDate, out holiday))
					{
						bgColor = "Cbfe";
						ggcolor = "Cora";
						holidayText = "<img src=\"/i/celeb.gif\" border=\"0\" alt=\"" + holiday + "\">&nbsp;";
					}
					else
					{
						bgColor = "Cbno";
						ggcolor = String.Empty;
						holidayText = String.Empty;
					}

					DataRow[] drExistsEvent = eventDataTable.Select("ISOSTARTDATE='" + startDate.ToString(@"yyyyMMdd") + "'");
					if (drExistsEvent.Length > 0)
					{
						string eventTool = String.Empty;
						foreach (DataRow drevent in drExistsEvent)
						{
							eventTool += "<b>" +UC.LTZ.ToLocalTime((DateTime) drevent["startdate"]).ToString(@"HH:mm") + "</b><br>";
							eventTool += drevent["title"] + "<br>";
						}
						existsEvent = "<img src=\"/i/eventosi.gif\" border=\"0\" onMouseOver=\"dtt('" + G.ParseJSString(eventTool) + "')\" onMouseout=\"dtt();\">&nbsp;";
					}
					else
					{
						existsEvent = String.Empty;
					}

					existsReminder = String.Empty;
					DataRow[] DrExistsReminder = ReminderTable.Select("ReminderIsoDate='" + startDate.ToString(@"yyyyMMdd") + "' or ReminderIsoAllarm='" + startDate.ToString(@"yyyyMMdd") + "'");
					if (DrExistsReminder.Length > 0)
					{
						existsReminder = "<img src=\"/i/bell.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"location.href='/CRM/CRM_Reminder.aspx?m=25&si=41&d=" + startDate.ToString(@"yyyyMMdd") + "'\">&nbsp;";
					}


					if (x == 7)
					{
						weekDay = 0;
					}
					else
					{
						weekDay = x;
					}
					if (startDate.DayOfWeek == 0)
					{
						bgColor = "Cbfe";
						ggcolor = "Cora";
					}
					if (startDate == DateTime.Today) bgColor = "Cbtd";
					if (Convert.ToInt32(startDate.DayOfWeek) == weekDay && Convert.ToInt32(startDate.Month) == month)
					{
						calenderStr.AppendFormat("<td width=\"14%\" height=\"50\" class=\"{0}\" valign=\"top\" onmouseover=\"mOvr(this,'Cbmo',event)\" onmouseout=\"mOut(this,'{0}',event)\" >", bgColor);
						calenderStr.Append("<table width=\"100%\"><tr><td><table class=\"ctbl\"><tr>");
						calenderStr.AppendFormat("<td class=\"DNCell {0}\" onMouseover=\"dtt(wk);\" onMouseout=\"dtt();\" onClick=\"location.href='agenda.aspx?date={1}'\">", ggcolor, startDate.ToShortDateString());
						calenderStr.AppendFormat("<span class=\"Dcal {0}\">{1}</span></td>", ggcolor, startDate.Day.ToString());
						calenderStr.Append("<td align=\"left\" valign=\"top\"><span class=\"Dcal\" onclick=\"OpenTEvent('NEW','" + startDate.ToString(@"dd/MM/yyyy") + "&ora1=09:00')\" onmouseover=\"showmenu2(event,linkset[2]," + startDate.Day.ToString() + ")\" onMouseout=\"dhm()\">&nbsp;&nbsp;<img src=\"/i/sheet.gif\"></span>");
						calenderStr.AppendFormat("&nbsp;{0}{2}{1}</td>", existsEvent, holidayText, existsReminder);
						calenderStr.Append("</tr></table></td></tr><tr><td>");

						calenderStr.Append(FillEvents(startDate, bgColor, CalendarDataTable));

						calenderStr.Append("</td></tr></table></td>");
						startDate = startDate.AddDays(1);
					}
					else
					{
						calenderStr.Append("<td width=\"14%\" height=\"50\" bgColor=\"#F2F2F2\">&nbsp;</td>");
					}
				}
				calenderStr.Append("</tr>");
				if (wd == 5 && startDate.Month != month) break;
			}
			Days.Text = calenderStr.ToString();
		}

		private string FillEvents(DateTime day, string bgColor, DataTable calendarDataTable)
		{
			StringBuilder eventsStr = new StringBuilder();
			byte[] myday = new byte[24];
			string[] myevent = new string[24];
			int ii;
			string backCol = bgColor;

			DataRow[] drExistsAppointment = calendarDataTable.Select("ISOSTARTDATE='" + day.ToString(@"yyyyMMdd") + "' OR (LOCALSTARTDATE<='" + day.ToString() + "' AND LOCALENDDATE>='" + day.ToString() + "')");
			if (drExistsAppointment.Length > 0)
			{
				eventsStr.Append("<table width=\"90%\" height=\"30\" cellspacing=1 cellpadding=0 align=\"center\">");
				eventsStr.Append("<tr>");


				TimeSpan mindiffstart = UC.LTZ.GetUtcOffset(day);

				int hTimeZone = (int)(UC.HTimeZone);
				hTimeZone = Convert.ToInt32(mindiffstart.TotalHours);
				int overflow = 0;
				foreach (DataRow drSelectAppointment in drExistsAppointment)
				{
					if (overflow > 0)
					{
						for (int i = 0; i < overflow; i++)
							myday[i] = 2;
						overflow = 0;
					}
					int startHour = (((DateTime) drSelectAppointment["LocalStartDate"]).Day == day.Day) ? (int) drSelectAppointment["starthour"] + hTimeZone : 0;
					int endHour = (((DateTime) drSelectAppointment["LocalEndDate"]).Day == day.Day) ? (int) drSelectAppointment["endhour"] + hTimeZone : 23;
					if(startHour<0)startHour+=23;
					if(endHour<0)endHour+=23;
					if (endHour > 23)
					{
						overflow = endHour - 23;
						endHour = 23;
					}
					int endHourNew = ((endHour - startHour) < 1) ? endHour + 1 : endHour;

					for (int i = startHour; i < endHourNew; i++)
					{
						Trace.Warn(i.ToString(), UC.StartHourAM.ToString() + " - " + UC.HTimeZone.ToString());
						if (i < UC.StartHourAM || (i >= UC.EndHourAM && i < UC.StartHourPM) || i >= UC.EndHourPM)
						{
							myday[i] = 3;
						}
						else
						{
							if (Convert.ToInt32(drSelectAppointment["Confirmation"]) == 1)
							{
								myday[i] = 2;
							}
							else
							{
								myday[i] = 1;
							}
						}
						if ((Byte) drSelectAppointment["isRec"] == 1) myday[i] = 4;
						myevent[i] = " onMouseover=\"dtt('" +UC.LTZ.ToLocalTime((DateTime) drSelectAppointment["startdate"]).ToString(@"HH:mm") + " - " +UC.LTZ.ToLocalTime((DateTime) drSelectAppointment["enddate"]).ToString(@"HH:mm") + "<br>" + G.ParseJSString(drSelectAppointment["CONTACT"].ToString()) +
							((drSelectAppointment["Company"].ToString().Length > 0) ? "<br>[" + G.ParseJSString(drSelectAppointment["Company"].ToString()) + "]" : "") + "');\" onMouseout=\"dtt();\"";
					}
				}

				for (ii = 7; ii < 21; ii++)
				{
					if (myday[ii] != 0)
					{
						switch (myday[ii])
						{
							case 1:
								backCol = "Cncnf";
								break;
							case 2:
								backCol = "Capp";
								break;
							case 3:
								backCol = "Cfo";
								break;
							case 4:
								backCol = "CRec";
								break;
						}
						eventsStr.Append("<td height=\"50%\" width=\"4\" class=\"HCell " + backCol + "\"");
						eventsStr.Append(" onClick=\"location.href='agenda.aspx?dy=" + day.ToString(@"dd/MM/yyyy") + "'\" ");
						eventsStr.Append(myevent[ii]);
					}
					else
					{
						backCol = bgColor;
						eventsStr.Append("<td height=\"50%\" width=\"4\" class=\"HCell " + backCol + "\"");
					}

					eventsStr.Append(">&nbsp;</td>");
					if (ii == 13) eventsStr.Append("</tr><tr>");
				}

				eventsStr.Append("</tr></table>");
				return eventsStr.ToString();
			}
			else
			{
				return "";
			}
		}

		private void FillWeek(DateTime gg)
		{
			StringBuilder weekStr = new StringBuilder();
			DateTime weekStart;

			if ((Convert.ToInt32(gg.DayOfWeek) == 0 && europeCalendar == 1) || (Convert.ToInt32(gg.DayOfWeek) == 6 && europeCalendar == 0))
			{
				weekStart = gg.AddDays(-6);
			}
			else
			{
				weekStart = gg.AddDays(-(Convert.ToInt32(gg.DayOfWeek) - europeCalendar));
			}
			Office.Text = "<a href=\"#\" class=\"sidebtn\" onclick=\"location.href='agenda.aspx?datauff=" + weekStart.ToShortDateString() + "'\">" +Root.rm.GetString("Caltxt30") + "</a>";
			weekStr.Append("<table border=\"0\" cellpadding=\"2\" cellspacing=\"2\" align=\"center\" width=\"98%\"><tr bgColor=\"orange\">");

			for (int i = 0; i < 7; i++)
			{
				string bgColorDay = (DateTime.Now.Date == weekStart.AddDays(i).Date) ? " Cbtd" : " Sebg";
				if(UC.myDTFI.ShortDatePattern.StartsWith("dd"))
					weekStr.Append("<td width=\"14%\" class=\"WeekTitle" + bgColorDay + "\" align=\"center\" onclick=\"location.href='agenda.aspx?dy=" + weekStart.AddDays(i).ToShortDateString() + "'\">" +UC.myDTFI.GetDayName(weekStart.AddDays(i).DayOfWeek) + "&nbsp;" + weekStart.AddDays(i).Day.ToString() + "/" + weekStart.AddDays(i).Month.ToString() + "</td>");
				else
					weekStr.Append("<td width=\"14%\" class=\"WeekTitle" + bgColorDay + "\" align=\"center\" onclick=\"location.href='agenda.aspx?dy=" + weekStart.AddDays(i).ToShortDateString() + "'\">" +UC.myDTFI.GetDayName(weekStart.AddDays(i).DayOfWeek) + "&nbsp;" + weekStart.AddDays(i).Month.ToString() + "/" + weekStart.AddDays(i).Day.ToString() + "</td>");
			}
			weekStr.Append("</tr><tr>");

			for (int ii = 0; ii < 7; ii++)
			{
				weekStr.Append("<td valign=\"top\">");

				DataRow[] drAppExists = CalendarDataTable.Select("ISOSTARTDATE='" + weekStart.AddDays(ii).ToString(@"yyyyMMdd") + "' OR (LOCALSTARTDATE<='" + weekStart.AddDays(ii).ToShortDateString() + "' AND LOCALENDDATE>='" + weekStart.AddDays(ii).ToShortDateString() + "')");


				if (drAppExists.Length > 0)
				{
					foreach (DataRow drEvent in drAppExists)
					{

						string backCol = String.Empty;
						if ((Byte) drEvent["isRec"] == 1) backCol = " CRec";
						string startHour = (((DateTime) drEvent["LocalStartDate"]).Day == weekStart.AddDays(ii).Day) ?UC.LTZ.ToLocalTime((DateTime) drEvent["startdate"]).ToShortTimeString() : "00:00";
						string endHour = (((DateTime) drEvent["LocalEndDate"]).Day == weekStart.AddDays(ii).Day) ?UC.LTZ.ToLocalTime((DateTime)drEvent["enddate"]).ToShortTimeString() : "00:00";
						weekStr.Append("<table style=\"cursor: pointer;\" onMouseover=\"dtt('" + StaticFunctions.FixCarriage(drEvent["note"].ToString()) + "');\" onMouseout=\"dtt();\" class=\"ctbl\" align=\"center\" width=\"100%\"><tr>");
						weekStr.Append("<td align=\"center\" class=\"AppWeek" + backCol + "\" style=\"cursor: pointer;\" onclick=\"OpenTEvent('MOD','&id=" + drEvent["id"] + "');\">" + startHour + " - " + endHour + "</td></tr>");
						weekStr.Append("<tr><td class=\"AppWeekBottom\" onclick=\"OpenTEvent('MOD','&id=" + drEvent["id"] + "');\">" + drEvent["contact"].ToString() + "<br>");
						if (drEvent["company"].ToString().Length > 0) weekStr.Append("[" + drEvent["company"].ToString() + "]");
						weekStr.Append("</td></tr></table>");
						weekStr.Append("<table><tr bgColor=\"#F2F2F2\"><td align=\"center\">");
						weekStr.AppendFormat("<img src=\"/i/modify.gif\" onclick=\"OpenTEvent('MOD','&id=" + drEvent["id"] + "');\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt31") + "\">&nbsp;");
						weekStr.AppendFormat("<img src=\"/i/confirm.gif\" class=\"icona\" onclick=\"location.href='agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&conf=" + drEvent["id"] + "'\" alt=\"" +Root.rm.GetString("Caltxt32") + "\">&nbsp;");
						weekStr.AppendFormat("<img src=\"/i/onhold.gif\" class=\"icona\" onclick=\"location.href='agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&conf=" + drEvent["id"] + "'\" alt=\"" +Root.rm.GetString("Caltxt33") + "\">&nbsp;");

						int crossact = Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE CALENDARID=" + drEvent["id"]));

						if (drEvent["uid"].ToString() == drEvent["seconduid"].ToString() || drEvent["seconduid"] == DBNull.Value)
						{
							if (crossact > 0)
								weekStr.AppendFormat("<img src=\"/i/erase.gif\" onclick=\"Rimuovi('agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&del=" + drEvent["id"] + "',10)\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt34") + "\">");
							else
								weekStr.AppendFormat("<img src=\"/i/erase.gif\" onclick=\"Rimuovi('agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&del=" + drEvent["id"] + "',0)\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt34") + "\">");
						}
						else
						{
							if (crossact > 0)
								weekStr.AppendFormat("<img src=\"/i/erase.gif\" onclick=\"Rimuovi('agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&del=" + drEvent["id"] + "',11)\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt34") + "\">");
							else
								weekStr.AppendFormat("<img src=\"/i/erase.gif\" onclick=\"Rimuovi('agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&del=" + drEvent["id"] + "',1)\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt34") + "\">");
						}

						weekStr.AppendFormat("<img src=\"/i/lens.gif\" onclick=\"OpenTEvent('VIE','&id=" + drEvent["id"] + "');\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt31") + "\">&nbsp;");
						weekStr.AppendFormat("<img src=\"/i/printer.gif\" onclick=\"PrintApp('" + drEvent["id"] + "');\" class=\"icona\">&nbsp;");
						weekStr.Append("</td></tr>");
						weekStr.Append("</table>");
					}
				}

				DataRow[] drExistsEvent = eventDataTable.Select("ISOSTARTDATE='" + weekStart.AddDays(ii).ToString(@"yyyyMMdd") + "'");
				if (drExistsEvent.Length > 0)
				{
					foreach (DataRow drEvent in drExistsEvent)
					{
						weekStr.Append("<table border=\"0\" class=\"ctbl\" style=\"cursor: pointer;\" align=\"center\" width=\"100%\" onMouseover=\"dtt('" + StaticFunctions.FixCarriage(drEvent["note"].ToString()) + "');\" onMouseout=\"dtt();\"><tr>");
						weekStr.Append("<td align=\"center\"  class=\"EveWeek\">");
						weekStr.Append("<span >" +UC.LTZ.ToLocalTime((DateTime) drEvent["startdate"]).ToString(@"HH:mm") + "</span></td></tr>");
						weekStr.Append("<tr><td class=\"EveWeekBottom\" onclick=\"OpenEventS('MOD','&id=" + drEvent["id"] + "');\">" + drEvent["title"] + "</td></tr></table>");
						weekStr.Append("<table><tr bgColor=\"#F2F2F2\"><td align=\"center\">");
						weekStr.AppendFormat("<img src=\"/i/modify.gif\" onclick=\"OpenEventS('MOD','&id=" + drEvent["id"] + "');\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt31") + "\">&nbsp;");
						weekStr.AppendFormat("<img src=\"/i/erase.gif\" onclick=\"Rimuovi('agenda.aspx?datedel=" + weekStart.AddDays(ii).ToShortDateString() + "&dels=" + drEvent["id"] + "')\" class=\"icona\" alt=\"" +Root.rm.GetString("Caltxt35") + "\">");
						weekStr.Append("</td></tr>");
						weekStr.Append("</table>");
					}
				}

				weekStr.Append("&nbsp;</td>");
			}

			weekStr.Append("</tr></table>");
			Week.Text = weekStr.ToString();
		}


		private void Delete(int id)
		{
			string sqlString = "SELECT * FROM BASE_CALENDAR WHERE ID ="+ id;
			string delSql = "DELETE FROM BASE_CALENDAR WHERE ID ="+ id;
			DataSet myDataSet = DatabaseConnection.CreateDataset(sqlString);

			if (myDataSet.Tables[0].Rows.Count > 0)
			{
				DataRow dr = myDataSet.Tables[0].Rows[0];


				if (UC.ImpersonateUser != (int)dr["UID"] && (int)dr["UID"] != UC.UserId)
				{
					ClientScript.RegisterStartupScript(this.GetType(), "delmessage", "<script>alert('" +Root.rm.GetString("Caltxt53") + "');</script>");
					return;
				}

				string messBody =Root.rm.GetString("Caltxt47") + "\n";

				messBody +=UC.LTZ.ToLocalTime((DateTime) dr["startdate"]).ToShortDateString() + " ";
				messBody +=UC.LTZ.ToLocalTime((DateTime) dr["startdate"]).ToString(@"HH:mm") + " - " +UC.LTZ.ToLocalTime((DateTime) dr["enddate"]).ToString(@"HH:mm") + "\n" + dr["CONTACT"].ToString() + ((dr["Company"].ToString().Length > 0) ? "\n[" + G.ParseJSString(dr["Company"].ToString()) + "]\n" : "\n") + dr["note"];

				vCalendar.vEvent evt = new vCalendar.vEvent();
				evt.UID = "vCal";
				evt.DTStart = Convert.ToDateTime(UC.LTZ.ToLocalTime((DateTime) dr["startdate"]));
				evt.DTEnd = Convert.ToDateTime(UC.LTZ.ToLocalTime((DateTime) dr["startdate"]));
				evt.Summary =Root.rm.GetString("Caltxt27");
				evt.URL = String.Empty;
				evt.description = messBody;

				Message(int.Parse(dr["uidins"].ToString()),Root.rm.GetString("Caltxt36"), messBody, messBody);
				if ((int) dr["recurrid"] > 0)
				{
					recurrence.DeleteRecurrence((int) dr["recurrid"]);
				}
				DatabaseConnection.DoCommand(delSql);
			}

			if (Request.Params["a"] == "1")
			{
				DatabaseConnection.DoCommand("DELETE FROM BASE_CALENDAR WHERE SENCONDIDOWNER=" + id);
			}

			if (Request.Params["act"] == "1")
			{
				DatabaseConnection.DoCommand("DELETE FROM CRM_WORKACTIVITY WHERE CALENDARID=" + id);
			}
			else
			{
				DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET CALENDARID=NULL WHERE CALENDARID=" + id);
			}

			DatabaseConnection.CommitTransaction();
		}

		private void DeleteSecondary(int id)
		{
			string sqlString = "DELETE FROM BASE_EVENTS WHERE ID =" + id;
			DatabaseConnection.DoCommand(sqlString);
			DatabaseConnection.CommitTransaction();
		}

		private void Confirm(int id)
		{
			string sqlString = "SELECT ID,UIDINS,CONFIRMATION,CONTACT FROM BASE_CALENDAR WHERE ID ="+id;
			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if (dg.HasRows)
				{
					if (Convert.ToInt32(dg.ExternalReader["uidins"]) != UC.UserId)
					{
						if (Convert.ToInt16(dg.ExternalReader["CONFIRMATION"]) == 0)
						{
							Message(int.Parse(dg.ExternalReader["uidins"].ToString()),Root.rm.GetString("Caltxt39"),Root.rm.GetString("Caltxt37") + " \"" + dg.ExternalReader["CONTACT"].ToString() + "\" " +Root.rm.GetString("Caltxt40"),Root.rm.GetString("Caltxt37") + " \"" + dg.ExternalReader["CONTACT"].ToString() + "\" " +Root.rm.GetString("Caltxt40"));
							dg.Add("CONFIRMATION", 1);
						}
						else
						{
							Message(int.Parse(dg.ExternalReader["uidins"].ToString()),Root.rm.GetString("Caltxt41"),Root.rm.GetString("Caltxt37") + " \"" + dg.ExternalReader["CONTACT"].ToString() + "\" " +Root.rm.GetString("Caltxt42"),Root.rm.GetString("Caltxt37") + " \"" + dg.ExternalReader["CONTACT"].ToString() + "\" " +Root.rm.GetString("Caltxt42"));
							dg.Add("CONFIRMATION", 0);
						}
					}
					else
					{
						if (Convert.ToInt16(dg.ExternalReader["CONFIRMATION"]) == 0)
						{
							dg.Add("CONFIRMATION", 1);
						}
						else
						{
							dg.Add("CONFIRMATION", 0);
						}
					}
				}
			dg.Execute("BASE_CALENDAR","id="+id);
			}
		}

		public void CalOfficeWeek_Click(object sender, EventArgs e)
		{
			DateTime d;

			switch (((LinkButton) sender).ID)
			{
				case "CalOfficePrevWeek":
					d = Convert.ToDateTime(WeekDate.Text);
					d = d.AddDays(-7);
					WeekDate.Text = d.ToShortDateString();
					FillOfficeCalendar(d);
					break;
				case "CalOfficeNextWeek":
					d = Convert.ToDateTime(WeekDate.Text);
					d = d.AddDays(7);
					WeekDate.Text = d.ToShortDateString();
					FillOfficeCalendar(d);
					break;
			}
			CalendarioMensile.Visible = false;
			Week.Visible = false;
			DailyPanel.Visible = false;
			CalOfficeso.Visible = true;
		}

		private void FillOfficeCalendar(DateTime weekStart)
		{
			StringBuilder weekStr = new StringBuilder();
			string sqlCalendar;

			weekStr.Append("<table border=\"0\" cellpadding=\"2\" cellspacing=\"2\" align=\"center\" width=\"98%\"><tr bgColor=\"orange\">");
			weekStr.AppendFormat("<td width=\"14%\" class=\"GridTitle\">{0}</td>", Root.rm.GetString("Pactxt1"));
			for (int i = 0; i < 7; i++)
			{
				weekStr.Append("<td width=\"12%\" class=\"GridTitle\"> " +UC.myDTFI.GetDayName(weekStart.AddDays(i).DayOfWeek) + "&nbsp;" + weekStart.AddDays(i).Day.ToString() + "/" + weekStart.AddDays(i).Month.ToString() + "</td>");
			}

			DataSet dsContact = new DataSet();
			HeaderCalOfficeso.Visible = true;
			if (UC.OfficeAgenda.Length > 1)
			{
				Setvisoffices(true);
				FillOffices();
				if (ImpersonateOffice.Visible)
				{
					dsContact = DatabaseConnection.CreateDataset("SELECT UID,NAME,SURNAME FROM ACCOUNT WHERE OFFICEID=" + UC.Office.ToString());
					TitleOfficeso.Text = GetOfficeName(UC.Office);
				}
				else
				{
					if (UC.ImpersonateOffice != -1)
					{
						dsContact = DatabaseConnection.CreateDataset("SELECT UID,NAME,SURNAME FROM ACCOUNT WHERE OFFICEID=" + UC.ImpersonateOffice);
					}
					else
					{
						string q = "OfficeID=" + UC.Office.ToString() + " OR ";
						for (int i = 2; i < ImpersonateOffice.Items.Count; i++)
							q += "OfficeID=" + ImpersonateOffice.Items[i].Value + " OR ";

						dsContact = DatabaseConnection.CreateDataset("SELECT UID,NAME,SURNAME FROM ACCOUNT WHERE (" + q.Substring(0, q.Length - 3) + ")");
					}

					TitleOfficeso.Text = GetOfficeName(UC.ImpersonateOffice);
				}
			}
			else
			{
				dsContact = DatabaseConnection.CreateDataset("SELECT UID,NAME,SURNAME FROM ACCOUNT WHERE OFFICEID=" + UC.Office);
				TitleOfficeso.Text = GetOfficeName(UC.Office);
			}


			TimeSpan mindiffstart = UC.LTZ.GetUtcOffset(weekStart);

			foreach (DataRow drContact in dsContact.Tables[0].Rows)
			{
				weekStr.Append("<tr><td width=\"14%\" height=\"40\" class=\"GridTitle\">" + drContact["Name"] + " " + drContact["Surname"] + "</td>");

				sqlCalendar = "sELECT *,DATEADD(N," + mindiffstart.TotalMinutes.ToString() + ",STARTDATE) AS LOCALSTARTDATE,DATEADD(N," + mindiffstart.TotalMinutes.ToString() + ",ENDDATE) AS LOCALENDDATE,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR FROM BASE_CALENDAR WHERE ((MONTH(STARTDATE)=" + weekStart.Month.ToString() + " AND YEAR(STARTDATE)=" + weekStart.Year.ToString() + ") OR (MONTH(ENDDATE)=" + weekStart.AddDays(7).Month.ToString() + " AND YEAR(ENDDATE)=" + weekStart.AddDays(7).Year.ToString() + ") OR (RECURRID > 0)) AND UID=" + drContact["uid"].ToString() + ";";

				DataSet dsCalendar = DatabaseConnection.CreateDataset(sqlCalendar);
				CalendarDataTable = dsCalendar.Tables[0];

				DataColumn dcDynColumn = new DataColumn();
				dcDynColumn.ColumnName = "isRec";
				dcDynColumn.DataType = Type.GetType("System.Byte");
				dcDynColumn.DefaultValue = 0;
				CalendarDataTable.Columns.Add(dcDynColumn);

				if (CalendarDataTable.Select("RecurrID>0").Length > 0)
				{
					DateTime MDT = weekStart;
					DateTime MDT2 = MDT.AddDays(6);
					int rowsToCicleCal = CalendarDataTable.Rows.Count;


					for (int i = 0; i < rowsToCicleCal; i++)
					{
						ArrayList AL = recurrence.Remind((int) CalendarDataTable.Rows[i]["recurrid"], MDT, MDT2);

						if (AL.Count > 0)
						{
							foreach (DateTime ALT in AL)
							{
								DataRow row = CalendarDataTable.NewRow();
								for (int i2 = 0; i2 < CalendarDataTable.Columns.Count - 1; i2++)
									row[i2] = CalendarDataTable.Rows[i][i2];
								row["isRec"] = 1;
								row["Localstartdate"] = ALT.Date.Add(((DateTime) CalendarDataTable.Rows[i]["startdate"]).TimeOfDay);
								row["Localenddate"] = ALT.Date.Add(((DateTime) CalendarDataTable.Rows[i]["enddate"]).TimeOfDay);
								row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
								CalendarDataTable.Rows.Add(row);
							}
							if (Convert.ToInt32(((string) CalendarDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != weekStart.Month)
								CalendarDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
						}
					}
				}

				for (int xx = 1; xx < 8; xx++)
				{
					weekStr.Append("<td width=\"12%\" class=\"Cbno Tepy\">&nbsp;");
					weekStr.Append(FillEvents(weekStart, "#FFFFFF", CalendarDataTable));
					weekStr.Append("</td>");
					weekStart = weekStart.AddDays(1);
				}

				weekStart = weekStart.AddDays(-7);
				weekStr.Append("</tr>");

			}
			weekStr.Append("</table>");
			CalOfficeso.Text = weekStr.ToString();
		}

		private string GetOfficeName(int of)
		{
			if (of != -1)
			{
				DataSet ds = DatabaseConnection.CreateDataset("SELECT OFFICE FROM OFFICES WHERE ID=" + of);
				if(ds.Tables[0].Rows.Count>0)
				{
					DataRow dr = ds.Tables[0].Rows[0];
					return dr["Office"].ToString().ToUpper();
				}
				else
				{
					return String.Empty;
				}
			}
			else
			{
				return Root.rm.GetString("Caltxt48");
			}
		}

		private void FillOffices()
		{
			string[] arryUff = UC.OfficeAgenda.Split('|');
			string query = String.Empty;
			foreach (string ut in arryUff)
			{
				query += "ID=" + ut + " OR ";
			}
			query = query.Substring(6, query.Length - 17);
			ImpersonateOffice.DataTextField = "Office";
			ImpersonateOffice.DataValueField = "id";
			string sqlString = "SELECT ID, OFFICE FROM OFFICES WHERE " + query + " ORDER BY OFFICE";
			ImpersonateOffice.DataSource = DatabaseConnection.CreateReader(sqlString);
			ImpersonateOffice.DataBind();
			ImpersonateOffice.Items.Insert(0, new ListItem(Root.rm.GetString("Caltxt48"), "-1"));

			ImpersonateOffice.SelectedIndex = 0;
		}

		public DataTable FillDayWap(DateTime data, UserConfig UC)
		{
			this.UC = UC;
			this.UC.ImpersonateUser = UC.UserId;
			return FillDayExtended(data, true);
		}

		private void FillDay(DateTime data)
		{
			HtmlImage PrintDay = (HtmlImage)Page.FindControl("PrintDay");
			PrintDay.Attributes.Add("onclick","NewWindow('/Calendar/DailyPrint.aspx?Daily="+data.ToShortDateString()+"&render=no', '', 400, 300, 'yes')");
			HtmlImage PrintDay2 = (HtmlImage)Page.FindControl("PrintDay2");
			PrintDay2.Attributes.Add("onclick","NewWindow('/Calendar/DailyPrint.aspx?Daily="+data.ToShortDateString()+"&render=no', '', 400, 300, 'yes')");
			calDate.Visible = true;
			Session["FillDay"] = data;
			FillDayExtended(data, false);
		}

		private DataTable FillDayExtended(DateTime data, bool isWap)
		{
			int currentUId = (UC.ImpersonateUser == 0) ? UC.UserId : UC.ImpersonateUser;
			StringBuilder sqlCalendar = new StringBuilder();
			sqlCalendar.AppendFormat("SELECT ID,STARTDATE,ENDDATE,RECURRID,CONTACT,COMPANY,NOTE,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR, PHONE, ROOM, ADDRESS, CITY, PROVINCE, CAP FROM BASE_CALENDAR WHERE (CONVERT(VARCHAR(10),STARTDATE,112)='{0}' OR RECURRID>0) AND UID={1};",data.ToString(@"yyyyMMdd"),currentUId);
			sqlCalendar.AppendFormat("SELECT ID,STARTDATE,TITLE,NOTE,RECURRID,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR FROM BASE_EVENTS WHERE (CONVERT(VARCHAR(10),STARTDATE,112)='{0}' OR RECURRID>0) AND UID={1};",data.ToString(@"yyyyMMdd"),currentUId);
			Trace.Warn("SQL", sqlCalendar.ToString());
			DataSet dsCalendar = DatabaseConnection.CreateDataset(sqlCalendar.ToString());
			CalendarDataTable = dsCalendar.Tables[0];
			eventDataTable = dsCalendar.Tables[1];

			StringBuilder gg = new StringBuilder();

			int hTimeZone = Convert.ToInt32(UC.HTimeZone);

			DataColumn dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "isRec";
			dcDynColumn.DataType = Type.GetType("System.Byte");
			dcDynColumn.DefaultValue = 0;
			CalendarDataTable.Columns.Add(dcDynColumn);
			DataColumn dcDynColumn2 = new DataColumn();
			dcDynColumn2.ColumnName = "isRec";
			dcDynColumn2.DataType = Type.GetType("System.Byte");
			dcDynColumn2.DefaultValue = 0;
			eventDataTable.Columns.Add(dcDynColumn2);
			int rowsToCicleCal = CalendarDataTable.Rows.Count;
			int rowsToCicleEvn = eventDataTable.Rows.Count;
			if (CalendarDataTable.Select("RecurrID>0").Length > 0)
			{
				DateTime MDT = data;
				DateTime MDT2 = MDT.AddSeconds(86399);
				Trace.Warn("MDT", MDT.ToString());
				Trace.Warn("MDT2", MDT2.ToString());

				for (int i = 0; i < rowsToCicleCal; i++)
				{
					ArrayList AL = recurrence.Remind((int) CalendarDataTable.Rows[i]["recurrid"], MDT, MDT2);

					if (AL.Count > 0)
					{
						foreach (DateTime ALT in AL)
						{
							DataRow row = CalendarDataTable.NewRow();

							for (int i2 = 0; i2 < CalendarDataTable.Columns.Count - 1; i2++)
								row[i2] = CalendarDataTable.Rows[i][i2];
							row["isRec"] = 1;
							row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
							CalendarDataTable.Rows.Add(row);
						}
						if (Convert.ToInt32(((string) CalendarDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != data.Month)
							CalendarDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
					}
				}
			}


			if (eventDataTable.Select("RecurrID>0").Length > 0)
			{
				DateTime MDT = data;
				DateTime MDT2 = MDT.AddSeconds(86399);


				for (int i = 0; i < rowsToCicleEvn; i++)
				{
					ArrayList AL = recurrence.Remind((int) eventDataTable.Rows[i]["recurrid"], MDT, MDT2);

					Trace.Warn("AL", AL.Count.ToString());

					if (AL.Count > 0)
					{
						foreach (DateTime ALT in AL)
						{
							DataRow row = eventDataTable.NewRow();
							for (int i2 = 0; i2 < eventDataTable.Columns.Count - 1; i2++)
								row[i2] = eventDataTable.Rows[i][i2];
							row["isRec"] = 1;
							row["ISOSTARTDATE"] = ALT.ToString(@"yyyyMMdd");
							eventDataTable.Rows.Add(row);
						}

						if (Convert.ToInt32(((string) eventDataTable.Rows[i]["ISOSTARTDATE"]).Substring(4, 2)) != data.Month)
							eventDataTable.Rows[i]["ISOSTARTDATE"] = "19800101";
					}
				}
			}

			if (isWap)
			{
				return CalendarDataTable;
			}

			DataRow[] drExistsAppointment = CalendarDataTable.Select("ISOSTARTDATE='" + data.ToString(@"yyyyMMdd") + "'");
			long idNumber = 0;

			for (int dayI = 0; dayI < 23; dayI++)
			{
				if (dayI%2 == 0)
					gg.AppendFormat("<tr><td valign=\"top\" class=\"GridItem\">{0}:00</td><td class=\"GridItem ", dayI.ToString());
				else
					gg.AppendFormat("<tr><td valign=\"top\" class=\"GridItemAltern\">{0}:00</td><td class=\"GridItemAltern ", dayI.ToString());


				if (drExistsAppointment.Length > 0)
				{
					bool exists = false;
					string appString = String.Empty;
					foreach (DataRow selectAppointmentDr in drExistsAppointment)
					{

                        int hstart = UC.LTZ.ToLocalTime((DateTime)selectAppointmentDr["STARTDATE"]).Hour;
                        int hend = UC.LTZ.ToLocalTime((DateTime)selectAppointmentDr["ENDDATE"]).Hour;
                        if (hstart == dayI && idNumber != Convert.ToInt32(selectAppointmentDr["id"]))
						{
							exists = true;
							string appText =UC.LTZ.ToLocalTime((DateTime) selectAppointmentDr["startdate"]).ToString(@"HH:mm") + " - " +UC.LTZ.ToLocalTime((DateTime) selectAppointmentDr["enddate"]).ToString(@"HH:mm") + "<br>" + selectAppointmentDr["CONTACT"].ToString() + ((selectAppointmentDr["Company"].ToString().Length > 0) ? "<br>[" + G.ParseJSString(selectAppointmentDr["Company"].ToString()) + "]<br>" : "<br>") + selectAppointmentDr["note"];
							idNumber = Convert.ToInt64(selectAppointmentDr["id"]);
							appString = "Cbgg\" onclick=\"OpenTEvent('MOD','&id=" + idNumber.ToString() + "');\">" + appText;
						}
						else
						{
							if (hstart < dayI && hend >= dayI && idNumber == Convert.ToInt64(selectAppointmentDr["id"]))
							{
								exists = true;
								appString = "Cbgg\" onclick=\"OpenTEvent('MOD','&id=" + idNumber.ToString() + "');\">";
							}
						}


					}
					gg.Append(appString.ToString());
					if (!exists)
						gg.AppendFormat(" onclick=\"OpenTEvent('NEW','{0}&ora1={1}:00');\">&nbsp;</td>", data.ToShortDateString(), dayI.ToString());
					else
						gg.Append("&nbsp;</td>");
				}
				else
				{
					gg.AppendFormat(" onclick=\"OpenTEvent('NEW','{0}&ora1={1}:00');\">&nbsp;</td>", data.ToShortDateString(), dayI.ToString());
				}


				if (dayI%2 == 0)
				{
					gg.Append("<td class=\"GridItem\"");
				}
				else
				{
					gg.Append("<td class=\"GridItemAltern\"");
				}


				DataRow[] drExistsEvent = eventDataTable.Select("ISOSTARTDATE='" + data.ToString(@"yyyyMMdd") + "' AND STARTHOUR=" + (dayI - hTimeZone).ToString());
				if (drExistsEvent.Length > 0)
				{
					Trace.Warn("eventi", drExistsEvent.Length.ToString());
					gg.Append(">");
					foreach (DataRow drs in drExistsEvent)
					{
						gg.Append("<table width=\"100%\"><tr><td class=\"normal Cbgg\"");
						gg.Append(" onclick=\"OpenEventS('MOD','&id=" + Convert.ToString(drs["id"]) + "');\">" +UC.LTZ.ToLocalTime((DateTime) drs["startdate"]).ToString(@"HH:mm") + "<br>" + drs["title"] + "<br>" + drs["note"]);
						gg.Append("</td></tr></table>");
					}

					gg.Append("&nbsp;</td></tr>");
				}
				else
				{
					gg.Append(" onclick=\"OpenEventS('NEW','" + data.ToShortDateString() + "&ora1=" + dayI.ToString() + ":00');\">&nbsp;</td></tr>");
				}

			}
			Detail.Text = gg.ToString();
			return null;
		}

		private void FillImpersUser()
		{
			if(Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(UID) FROM ACCOUNT WHERE DIARYACCOUNT LIKE '|%" + UC.UserId.ToString() + "|%'"))<=0)
				visimpute.Visible = false;

		}

		private void GetOwnerName()
		{
			if (UC.ImpersonateUser == 0)
			{
				AgendaOwner.Text = UC.UserRealName;
			}
			else
			{
				DataSet ds = DatabaseConnection.CreateDataset("SELECT (SURNAME+' '+NAME) AS UTETX FROM ACCOUNT WHERE UID=" + UC.ImpersonateUser);
				DataRow dr = ds.Tables[0].Rows[0];
				AgendaOwner.Text = dr["Utetx"].ToString();
			}
		}

		public void ImpersonateOn_Click(Object sender, EventArgs e)
		{
			if(Request.Form["ImpersonateID"].ToString().Length>0)
			{
				UC.ImpersonateUser = int.Parse(Request.Form["ImpersonateID"]);
				Response.Redirect(Request.Url.ToString());
			}

		}

		public void ImpersonateOff_Click(Object sender, EventArgs e)
		{
			UC.ImpersonateUser = 0;
			Response.Redirect(Request.Url.ToString());
		}

		public void ImpersonateOfficeOn_Click(Object sender, EventArgs e)
		{
			UC.ImpersonateOffice = int.Parse(Request.Params["ImpersonateOffice"]);
			Response.Redirect(Request.Url.ToString());
		}

		public void ImpersonateOfficeOff_Click(Object sender, EventArgs e)
		{
			UC.ImpersonateOffice = 0;
			Response.Redirect(Request.Url.ToString());
		}

		public DateTime LastDayOfMonth(int strMonth, int strYear)
		{
			DateTime dteDate = new DateTime(strYear, strMonth, 1);
			dteDate = dteDate.AddMonths(1);
			dteDate = dteDate.AddDays(-1);
			return dteDate;
		}

		protected void Change_Date(object sender, EventArgs e)
		{
			CalendarDay(calDate.SelectedDate.Month, calDate.SelectedDate.Year);
			calDate.VisibleDate = calDate.SelectedDate;
			GetOwnerName();
			CalendarioMensile.Visible = false;
			Week.Visible = false;
			CalOfficeso.Visible = false;
			HeaderCalOfficeso.Visible = false;
			DailyPanel.Visible = true;

			AgTitle.Text = AgendaOwner.Text + "&nbsp;&nbsp;&nbsp;" + calDate.SelectedDate.ToLongDateString() + "</center>";
			FillDay(calDate.SelectedDate);
		}

		public void Change_Month(Object source, MonthChangedEventArgs e)
		{
			CalendarDay(e.NewDate.Month, e.NewDate.Year);
			calDate.VisibleDate = e.NewDate;
			GetOwnerName();
			CalendarioMensile.Visible = false;
			Week.Visible = false;
			CalOfficeso.Visible = false;
			HeaderCalOfficeso.Visible = false;
			DailyPanel.Visible = true;
			calDate.Visible = true;

			AgTitle.Text = AgendaOwner.Text + "&nbsp;&nbsp;&nbsp;" + Convert.ToDateTime(Session["FillDay"]).ToLongDateString() + "</center>";
			FillDay(Convert.ToDateTime(Session["FillDay"]));
		}

		public void DayRender(Object source, DayRenderEventArgs e)
		{
			if (calReminder.IndexOf("|" + (e.Day.Date.Day).ToString() + "|") > -1
				&& e.Day.Date.Month == calDate.VisibleDate.Month)
			{
				e.Cell.BackColor = Color.LightSteelBlue;
			}

			if (e.Day.Date.Day == calDate.TodaysDate.Day
				&& e.Day.Date.Month == calDate.TodaysDate.Month
				&& e.Day.Date.Year == calDate.TodaysDate.Year)
				e.Cell.BackColor = Color.Gold;
		}

		private void CalendarDay(int month, int year)
		{
			DbSqlParameterCollection Msc=new DbSqlParameterCollection();

			DbSqlParameter parameterMonth = new DbSqlParameter("@Month", SqlDbType.Int, 4);
			parameterMonth.Value = month;
			Msc.Add(parameterMonth);

			DbSqlParameter parameterYear = new DbSqlParameter("@Year", SqlDbType.Int, 4);
			parameterYear.Value = year;
			Msc.Add(parameterYear);

			DbSqlParameter parameterOwnerID = new DbSqlParameter("@OwnerID", SqlDbType.Int, 4);
			parameterOwnerID.Value = (UC.ImpersonateUser == 0) ? UC.UserId : UC.ImpersonateUser;
			Msc.Add(parameterOwnerID);

			DbSqlParameter parameterDays = new DbSqlParameter("@Days", SqlDbType.VarChar, 1000);
			parameterDays.Parameter.Direction = ParameterDirection.Output;
			Msc.Add(parameterDays);

			try
			{
				DatabaseConnection.DoStored("DayCalendar",Msc);

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
			Trace.Warn("CalReminder", calReminder);
		}

	}

}
