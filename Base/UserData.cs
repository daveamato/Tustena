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
using System.Web;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Win32;

namespace Digita.Tustena.Base
{
	public class UserData
	{

		public static UserConfig LoadPersonalData(string userName, string passWord, int customerId)
		{
			string strSQL;
			strSQL = "SELECT USERACCOUNT, PASSWORD, GROUPID, UID, MANAGERID, CONVERT(VARCHAR(5),WORKSTART_1,114) AS STARTWORKHOUR, CONVERT(VARCHAR(5),WORKEND_2,114) AS ENDWORKHOUR, OFFICEID, (ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS USERNAME, DIARYACCOUNT, OFFICEACCOUNT, DATEPART(HH,WORKSTART_1) AS STARTHOUR, DATEPART(HH,WORKEND_1) AS ENDHOUR, TIMEZONES.MTIMEZONE, TIMEZONES.HTIMEZONE, TIMEZONES.DAYLIGHTSAVINGSTART, TIMEZONES.DAYLIGHTSAVINGEND, TIMEZONES.MDAYLIGHT, DATEPART(HH,WORKSTART_2) AS STARTHOUR2, DATEPART(HH,WORKEND_2) AS ENDHOUR2, WORKDAYS, PERSISTLOGIN, TIMEZONES.DTZ, TIMEZONES.DAYLIGHTSAVINGSTART, TIMEZONES.DAYLIGHTSAVINGEND, PAGING, FULLSCREEN, VIEWBIRTHDATE, CULTURE,  ACCOUNT.ACTIVE, ACCOUNT.SESSIONTIMEOUT, ACCOUNT.INSERTGROUPS, ACCOUNT.FIRSTDAYOFWEEK, ACCOUNT.OTHERGROUPS, TUSTENA_DATA.EMAIL AS CUSTOMEREMAIL, TUSTENA_DATA.DEBUGMODE, ACCOUNT.ZONES, ACCOUNT.ACCESSLEVEL FROM ACCOUNT LEFT OUTER JOIN TIMEZONES ON ACCOUNT.TIMEZONE = TIMEZONES.SHORTNAME CROSS JOIN TUSTENA_DATA ";

			if(customerId>-1)
			{
				strSQL += " WHERE (USERACCOUNT=@USERNAME AND PASSWORD=@PASSWORD AND TUSTENA_DATA.ACTIVE=1) ";
			}
			else
			{
				strSQL += " WHERE (USERACCOUNT=@USERNAME AND PASSWORD=@PASSWORD AND TUSTENA_DATA.ACTIVE=1) ";
			}

			DbSqlParameterCollection param = new DbSqlParameterCollection();
			param.Add(new DbSqlParameter("@USERNAME", userName));
			param.Add(new DbSqlParameter("@PASSWORD", passWord));


			DataSet ds = DatabaseConnection.SecureCreateDataset(strSQL,param);
			if (ds.Tables[0].Rows.Count>0)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				bool activeCompany = true;
				bool inTest = false;
				bool activeAccount = Convert.ToBoolean(dr["active"]);
				DataRow acBool;
				acBool = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENA_DATA").Tables[0].Rows[0];

		activeCompany=true;

				string cc = Thread.CurrentThread.CurrentCulture.DisplayName;
				if (activeCompany && activeAccount)
				{
					UserConfig UC = new UserConfig();
					UC.UserName = dr["UserAccount"].ToString();
					UC.UserGroupId = Convert.ToInt32(dr["GroupID"]);
					UC.Logged = LoggedStatus.yes;
					UC.UserId = Convert.ToInt32(dr["UID"]);
					UC.MyBossId = Convert.ToInt32(dr["ManagerID"]);
					UC.WorkStartHour = (string)dr["StartWorkHour"];
					UC.WorkEndHour = (string)dr["EndWorkHour"];
					UC.Office = (int)dr["OfficeID"];
					UC.UserRealName = (string)dr["UserName"];
					UC.UserAgenda = (string)dr["DiaryAccount"];
					UC.OfficeAgenda = (string)dr["OfficeAccount"];
					UC.StartHourAM = (int)dr["StartHour"];
					UC.EndHourAM = (int)dr["EndHour"];
					UC.StartHourPM = (int) dr["starthour2"];
					UC.EndHourPM = (int) dr["endhour2"];
					UC.WeekWorkDays = (Byte) dr["WorkDays"];
					UC.PagingSize = int.Parse(dr["Paging"].ToString());
					UC.FullScreen = (bool) dr["FullScreen"];
					UC.ViewBirthDate = (bool) dr["ViewBirthDate"];
					UC.ImpersonateUser = 0;
					UC.ImpersonateOffice = 0;
					UC.DebugMode = (bool) dr["debugmode"];
					UC.InsertGroups = (dr["InsertGroups"] == DBNull.Value) ? "" : (string) dr["InsertGroups"];
                    UC.AccessLevel = (int)dr["accesslevel"];
					UC.FirstDayOfWeek = (bool) dr["FirstDayOfWeek"];

					UC.MailingAddress = (string) dr["CustomerEmail"];

					if ((dr["DayLightSavingStart"].ToString() != null && dr["DayLightSavingStart"].ToString().Length != 0))
					{
						if (DateTime.Now >= (DateTime) dr["DayLightSavingStart"] && DateTime.Now <= (DateTime) dr["DayLightSavingEnd"])
						{
							UC.HTimeZone = (double) dr["HTimeZone"] + (double) dr["DTZ"];
						}
						else
						{
							UC.HTimeZone = (double) dr["HTimeZone"];
						}
					}
					else
					{
						UC.HTimeZone = (double) dr["HTimeZone"];
					}

					if (HttpContext.Current.Request.Cookies["CulturePref"] == null)
						UC.Culture = dr["Culture"].ToString();
					else if (HttpContext.Current.Request.Cookies["CulturePref"].ToString().Length > 1)
						UC.Culture = HttpContext.Current.Request.Cookies["CulturePref"].Value.ToString();
					else
						UC.Culture = CultureInfo.CurrentUICulture.Name;

					UC.CultureSpecific = UC.Culture.Substring(UC.Culture.Length - 2).ToLower();

					UC.PersistLogin = (bool) dr["PersistLogin"];

                    if (HttpContext.Current.Session!=null)
                        HttpContext.Current.Session.Timeout = (int) dr["SessionTimeout"];

					StringBuilder othergroups = new StringBuilder();
					othergroups.AppendFormat("id={0} OR DEPENDENCY LIKE '%|{0}|%' OR ", dr[2].ToString());
					string[] OG = dr["OtherGroups"].ToString().Split('|');
					if (OG.Length > 0)
					{
						foreach (string ogid in OG)
							if (ogid.Length > 0)
								othergroups.AppendFormat("ID={0} OR DEPENDENCY LIKE '%|{0}|%' OR ", ogid);
					}

					othergroups.Remove(othergroups.Length - 3, 3);
					string sqlString = "SELECT ID,DEPENDENCY FROM GROUPS WHERE " + othergroups.ToString();

					DataSet dsGroup = DatabaseConnection.CreateDataset(sqlString);
					string dependency = "|";
					if (dsGroup.Tables[0].Rows.Count > 0)
					{
						foreach (DataRow drdip in dsGroup.Tables[0].Rows)
						{
							dependency += drdip["id"].ToString() + "|";
						}
					}
					if (dependency.Length > 1)
					{
						UC.GroupDependency = dependency;
					}
					else
					{
						UC.GroupDependency = "0";
					}

					DataSet DSAdmGrID;
					DSAdmGrID = DatabaseConnection.CreateDataset("SELECT ADMINGROUPID FROM TUSTENA_DATA");
					UC.AdminGroupId = (long) DSAdmGrID.Tables[0].Rows[0][0];

					UC.LTZ = TimeZones.GetTimeZone(Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT TIMEZONEINDEX FROM ACCOUNT WHERE UID=" + UC.UserId)));
					UC.myDTFI = new CultureInfo(UC.Culture).DateTimeFormat;
					UC.Zones=(Convert.ToInt32(dr["accesslevel"].ToString())>0 && dr["zones"].ToString().Length>0)?dr["zones"].ToString():string.Empty;
                    UC.Modules = (ActiveModules)acBool["MODULES"];

					return UC;
				}
				else
				{
					UserConfig UC=new UserConfig();
					if(inTest)
						UC.Logged=LoggedStatus.testing;
					else
						UC.Logged=LoggedStatus.no;
					return UC;
				}
			}
			else
			{
				UserConfig UC=new UserConfig();
				UC.Logged=LoggedStatus.no;
				return UC;
			}
		}


	}
}
