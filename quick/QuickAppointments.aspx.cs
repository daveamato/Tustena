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
using System.Web.UI.WebControls;
using Digita.Tustena.Database;
using Digita.Tustena.Core;

namespace Digita.Tustena
{
	public partial class QuickAppointments : G
	{
		private string calAppointment = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nologin","<script>parent.location.href=parent.location.href;</script>");
			}
			else
			{
				if(!Page.IsPostBack)
				{
					calDate.VisibleDate = DateTime.Now;
					CalendarAppointment(DateTime.Now.Month, DateTime.Now.Year);
					FillAppointmentDetail(DateTime.Now.Day,DateTime.Now.Month, DateTime.Now.Year);
				}
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
			this.Load += new EventHandler(this.Page_Load);
		}
		#endregion

		private void CalendarAppointment(int month, int year)
		{
			DbSqlParameterCollection Msc=new DbSqlParameterCollection();

			DbSqlParameter parameterMonth = new DbSqlParameter("@Month", SqlDbType.Int, 4);
			parameterMonth.Value = month;
			Msc.Add(parameterMonth);

			DbSqlParameter parameterYear = new DbSqlParameter("@Year", SqlDbType.Int, 4);
			parameterYear.Value = year;
			Msc.Add(parameterYear);

			TimeSpan mindiff = UC.LTZ.GetUtcOffset(new DateTime(year,month,1,0,0,0));

			DbSqlParameter parameterLocalTime = new DbSqlParameter("@LTZ", SqlDbType.Int, 4);
			parameterLocalTime.Value = Convert.ToInt32(mindiff.TotalMinutes);//localOffset.Minutes;
			Msc.Add(parameterLocalTime);

			DbSqlParameter parameterOwnerID = new DbSqlParameter("@OwnerID", SqlDbType.Int, 4);
			parameterOwnerID.Value = UC.UserId;
			Msc.Add(parameterOwnerID);

			DbSqlParameter parameterDays = new DbSqlParameter("@Days", SqlDbType.VarChar, 1000);
			parameterDays.Parameter.Direction = ParameterDirection.Output;
			Msc.Add(parameterDays);

			try
			{
				DatabaseConnection.DoStored("AppointmentCalendar",Msc);
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
				calAppointment = (string) parameterDays.Value;
			}
			catch
			{
				calAppointment = String.Empty;
			}

		}

		public void Change_Month(Object source, MonthChangedEventArgs e)
		{
			CalendarAppointment(e.NewDate.Month, e.NewDate.Year);
		}

		protected void Change_Date(object sender, EventArgs e)
		{
			FillAppointmentDetail(calDate.SelectedDate.Day,calDate.SelectedDate.Month, calDate.SelectedDate.Year);
			CalendarAppointment(calDate.SelectedDate.Month, calDate.SelectedDate.Year);
		}

		private void FillAppointmentDetail(int day,int month, int year)
		{
			DataTable sDt = AppointmentDetail(day, month, year);
			if(sDt.Rows.Count>0)
			{
				StringBuilder dayAppointments = new StringBuilder();
				dayAppointments.Append("<table class=normal border=0 cellspacing=0 width=\"100%\" align=center>");
				foreach(DataRow dr in sDt.Rows)
				{
					dayAppointments.AppendFormat("<tr><td><b>{0}-{1}</b></td></tr><tr><td style=\"border-bottom:1px solid black;\">{2}</td></tr>",dr[0],dr[1],dr[2]);
				}
				dayAppointments.Append("</table>");
				LitAppointmentDetails.Text=dayAppointments.ToString();
			}else
                LitAppointmentDetails.Text = Root.rm.GetString("Quicktxt24");
		}

		private DataTable AppointmentDetail(int day,int month, int year)
		{
			DbSqlParameterCollection Msc=new DbSqlParameterCollection();

			DbSqlParameter parameterMonth = new DbSqlParameter("@MONTH", SqlDbType.Int, 4);
			parameterMonth.Value = month;
			Msc.Add(parameterMonth);

			DbSqlParameter parameterYear = new DbSqlParameter("@YEAR", SqlDbType.Int, 4);
			parameterYear.Value = year;
			Msc.Add(parameterYear);

			DbSqlParameter parameterDay = new DbSqlParameter("@DAY", SqlDbType.Int, 4);
			parameterDay.Value = day;
			Msc.Add(parameterDay);

			TimeSpan mindiff = UC.LTZ.GetUtcOffset(new DateTime(year,month,1,0,0,0));

			DbSqlParameter parameterLocalTime = new DbSqlParameter("@LTZ", SqlDbType.Int, 4);
			parameterLocalTime.Value = Convert.ToInt32(mindiff.TotalMinutes);//localOffset.Minutes;
			Msc.Add(parameterLocalTime);

			DbSqlParameter parameterOwnerID = new DbSqlParameter("@OWNERID", SqlDbType.Int, 4);
			parameterOwnerID.Value = UC.UserId;
			Msc.Add(parameterOwnerID);

			DataTable appointment = DatabaseConnection.DoStoredTable("APPOINTMENTDETAIL",Msc);

			return appointment;
		}

		public void DayRender(Object source, DayRenderEventArgs e)
		{
			if (calAppointment.IndexOf("|" + (e.Day.Date.Day).ToString() + "|") > -1
				&& e.Day.Date.Month == calDate.VisibleDate.Month)
				e.Cell.BackColor = Color.LightSteelBlue;

			if (e.Day.Date.Day == calDate.TodaysDate.Day
				&& e.Day.Date.Month == calDate.TodaysDate.Month
				&& e.Day.Date.Year == calDate.TodaysDate.Year)
				e.Cell.BackColor = Color.Gold;
		}
	}

}
