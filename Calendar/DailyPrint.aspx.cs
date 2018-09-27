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
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Calendar
{
	public partial class DailyPrint : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "","<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				try
				{
					FillDayExtended(Convert.ToDateTime(Request["Daily"]));
				}catch
				{
					FillDayExtended(DateTime.Now);
				}

				DailyPanel.Visible=true;
			}
		}

		private Recurrence recurrence
		{
			get
			{
				return new Recurrence(UC);
			}
		}

		private void FillDayExtended(DateTime data)
		{
			int currentUId = (UC.ImpersonateUser == 0) ? UC.UserId : UC.ImpersonateUser;
			this.LblTitle.Text="<table cellpadding=0 cellspacing=3><tr><td><img src=/images/Tustenalogov2.gif></td>";
			if(currentUId==UC.UserId)
				LblTitle.Text += "<td valign=bottom class=\"BigTitle\">"+UC.UserRealName + " " + data.ToShortDateString()+"</td>";
			else
				LblTitle.Text += "<td valign=bottom class=\"BigTitle\">"+DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME AS USERREALNAME FROM ACCOUNT WHERE UID="+currentUId) + " " + data.ToShortDateString()+"</td>";
			this.LblTitle.Text+="</tr></table>";
			StringBuilder sqlCalendar = new StringBuilder();
			sqlCalendar.AppendFormat("SELECT ID,STARTDATE,ENDDATE,RECURRID,CONTACT,COMPANY,NOTE,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR, DATEPART(HH, ENDDATE) AS ENDHOUR FROM BASE_CALENDAR WHERE (CONVERT(VARCHAR(10),STARTDATE,112)='{0}' OR RECURRID>0) AND UID={1};",data.ToString(@"yyyyMMdd"),currentUId);
			sqlCalendar.AppendFormat("SELECT ID,STARTDATE,TITLE,NOTE,RECURRID,CONVERT(VARCHAR(10),STARTDATE,112) AS ISOSTARTDATE, DATEPART(HH, STARTDATE) AS STARTHOUR FROM BASE_EVENTS WHERE (CONVERT(VARCHAR(10),STARTDATE,112)='{0}' OR RECURRID>0) AND UID={1};",data.ToString(@"yyyyMMdd"),currentUId);
			Trace.Warn("SQL", sqlCalendar.ToString());
			DataSet dsCalendar = DatabaseConnection.CreateDataset(sqlCalendar.ToString());
			DataTable CalendarDataTable = dsCalendar.Tables[0];
			DataTable eventDataTable = dsCalendar.Tables[1];

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
						if (((int) selectAppointmentDr["starthour"] + hTimeZone) == dayI && idNumber != Convert.ToInt32(selectAppointmentDr["id"]))
						{
							exists = true;
							string appText =UC.LTZ.ToLocalTime((DateTime) selectAppointmentDr["startdate"]).ToString(@"HH:mm") + " - " +UC.LTZ.ToLocalTime((DateTime) selectAppointmentDr["enddate"]).ToString(@"HH:mm") + "<br>" + selectAppointmentDr["CONTACT"].ToString() + ((selectAppointmentDr["COMPANY"].ToString().Length > 0) ? "<br>[" + G.ParseJSString(selectAppointmentDr["COMPANY"].ToString()) + "]<br>" : "<br>") + selectAppointmentDr["note"];
							idNumber = Convert.ToInt64(selectAppointmentDr["id"]);
							appString = "Cbgg\" onclick=\"OpenTEvent('MOD','&id=" + idNumber.ToString() + "');\">" + appText;
						}
						else
						{
							if (((int) selectAppointmentDr["starthour"] + hTimeZone) < dayI && ((int) selectAppointmentDr["endhour"] + hTimeZone) >= dayI && idNumber == Convert.ToInt64(selectAppointmentDr["id"]))
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
	}
}
