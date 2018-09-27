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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class Recurrence
	{
		private ArrayList ReturnDates = new ArrayList();
		UserConfig UC;
		public Recurrence(UserConfig uc)
		{
			UC = uc;
		}

		public ArrayList Remind(int id, DateTime startDate)
		{
			return Remind(id, startDate, DateTime.Parse("01/01/2050", UC.myDTFI));
		}

		public ArrayList Remind(int id, DateTime startDate, DateTime endDate)
		{
			ReturnDates.Clear();
			IDataReader recreader = DatabaseConnection.CreateReader("SELECT * FROM RECURRENCE WHERE ID=" + id);

			if (recreader != null)
			{
				if (recreader.Read())
				{
					if (endDate > recreader.GetDateTime(3)) endDate = recreader.GetDateTime(3);
					if (startDate < recreader.GetDateTime(2)) startDate = recreader.GetDateTime(2);
					endDate = EnsureEndOfDay(endDate);
					switch (recreader.GetByte(4))
					{
						case 1:
							R_Daily(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6));
							break;
						case 2:
							R_Weekly(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6), recreader.GetByte(7));
							break;
						case 3:
							R_Monthly(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6));
							break;
						case 4:
							R_Monthly_Daily(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6), recreader.GetByte(7));
							break;
						case 5:
							R_Yearly(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6));
							break;
						case 6:
							R_Yearly_Daily(startDate, endDate, recreader.GetByte(5), recreader.GetByte(6), recreader.GetByte(7));
							break;
					}
				}
			}

			recreader.Close();
			return ReturnDates;
		}

		private DateTime EnsureEndOfDay(DateTime day)
		{
			return new DateTime(day.Year,day.Month,day.Day,23,59,59,999);
		}


		private void R_Daily(DateTime today, DateTime endDate, int day, int businessDay)
		{
			DateTime nextDate = today;
			while (nextDate <= endDate)
			{
				if (businessDay == 1)
				{
					for (int i = 0; i < day; i++)
					{
						nextDate = nextDate.AddDays(1);
						if ((nextDate.DayOfWeek == DayOfWeek.Saturday) || (nextDate.DayOfWeek == DayOfWeek.Sunday) || isCelebration(nextDate))
							i--;
					}
				}
				else
				{
					nextDate = nextDate.AddDays(day);
				}
				ReturnDates.Add(nextDate);

			}
		}


		private void R_Weekly(DateTime today, DateTime endDate, int week, int weekDay, int multiple)
		{
			if(week==0)return;
			DateTime nextDay = today;
			int nextWeekDay = 0;
			int k = 0;
			int maxDay = 0;
			while (nextDay <= endDate)
			{
				{
					k = 0;
					for (int i = 0; i < 7; i++)
						if ((weekDay & (0x01 << i)) != 0)
						{
							k = i + 1;
							nextWeekDay = Convert.ToInt16(nextDay.DayOfWeek.ToString(@"d")) + 1;
							if (nextWeekDay != k)
								nextDay = nextDay.AddDays(k - nextWeekDay);
							if (nextDay >= today && nextDay <= endDate) ReturnDates.Add(nextDay);
							maxDay = i;
						}
				}
				nextDay = nextDay.AddDays((7 - maxDay)*week);

			}
		}

		private void R_Monthly(DateTime today, DateTime endDate, int day, int month)
		{
			DateTime nextDay = today;
			while (nextDay <= endDate)
			{
				if (nextDay.Day > day)
				{
					nextDay = nextDay.AddDays(DateTime.DaysInMonth(nextDay.Year, nextDay.Month) - nextDay.Day + day);
				}
				else
				{
					nextDay = nextDay.AddDays(day - nextDay.Day);
				}
				ReturnDates.Add(nextDay);
				nextDay = nextDay.AddMonths(month);
			}
		}

		private void R_Monthly_Daily(DateTime today, DateTime endDate, int when, int weekDay, int month)
		{
			DateTime nextDay = today;
			int nextWeekDay = 0;
			while (nextDay <= endDate)
			{
				switch (weekDay)
				{
					case 8:
						if (when != 5)
						{
							nextDay = nextDay.AddDays(when - nextDay.Day);
						}
						else
						{
							nextDay = nextDay.AddDays(DateTime.DaysInMonth(nextDay.Year, nextDay.Month) - nextDay.Day);
						}

						break;
					case 9:
						if (when != 5)
						{
							nextDay = nextDay.AddDays(when - nextDay.Day);
						}
						else
						{
							nextDay = nextDay.AddDays(DateTime.DaysInMonth(nextDay.Year, nextDay.Month) - nextDay.Day);
						}
						while ((nextDay.DayOfWeek == DayOfWeek.Saturday) || (nextDay.DayOfWeek == DayOfWeek.Sunday) || isCelebration(nextDay))
						{
							nextDay = nextDay.AddDays((when != 5) ? 1 : -1);
						}
						break;
					case 10:
						if (when != 5)
						{
							nextDay = nextDay.AddDays(when - nextDay.Day);
						}
						else
						{
							nextDay = nextDay.AddDays(DateTime.DaysInMonth(nextDay.Year, nextDay.Month) - nextDay.Day);
						}
						while ((nextDay.DayOfWeek != DayOfWeek.Saturday) && (nextDay.DayOfWeek != DayOfWeek.Sunday) && (!isCelebration(nextDay)))
						{
							nextDay = nextDay.AddDays((when != 5) ? 1 : -1);
						}
						break;
					default:
						if (when != 5)
						{
							nextDay = nextDay.AddDays(1 - nextDay.Day);
							nextWeekDay = Convert.ToInt16(nextDay.DayOfWeek.ToString(@"d"));
							nextWeekDay = weekDay - nextWeekDay - 1;
							if (nextWeekDay >= 0)
							{
								nextDay = nextDay.AddDays(((when - 1)*7) + nextWeekDay);
							}
							else
							{
								nextDay = nextDay.AddDays(((when)*7) + nextWeekDay);
							}

						}
						else
						{
							nextDay = nextDay.AddDays(DateTime.DaysInMonth(nextDay.Year, nextDay.Month) - nextDay.Day);
							nextWeekDay = Convert.ToInt16(nextDay.DayOfWeek.ToString(@"d")) + 1;
							if (nextWeekDay < weekDay) nextWeekDay = nextWeekDay + 7;
							nextDay = nextDay.AddDays(weekDay - nextWeekDay);
						}
						break;
				}
				ReturnDates.Add(nextDay);
				nextDay = nextDay.AddMonths(month);
			}
		}

		private void R_Yearly(DateTime today, DateTime endDate, int day, int month)
		{
			DateTime nextDay = today;
			DateTime dt;
			while (nextDay <= endDate)
			{
				dt = DateTime.Parse(day + "/" + month + "/" + nextDay.Year, UC.myDTFI);
				if (nextDay >= dt)
				{
					nextDay = nextDay.AddYears(1);
				}
				else
				{
					nextDay = dt;
				}
				ReturnDates.Add(nextDay);
			}
		}

		private void R_Yearly_Daily(DateTime today, DateTime endDate, int when, int weekDay, int month)
		{
			DateTime dtToday;
			DateTime dtEndDate;
			do
			{
				dtToday = DateTime.Parse("1" + "/" + month + "/" + today.Year, UC.myDTFI);
				dtEndDate = DateTime.Parse(DateTime.DaysInMonth(today.Year, month) + "/" + month + "/" + today.Year, UC.myDTFI);
				R_Monthly_Daily(dtToday, dtEndDate, when, weekDay, month);
				today = today.AddYears(1);
			} while (dtEndDate <= endDate);
		}

		public bool isCelebration(DateTime dt)
		{
			string nada;
			return isCelebration(dt, out nada);
		}

		private static DataTable PerstDataTable;

		public bool isCelebration(DateTime dt, out string descr)
		{
			descr = String.Empty;

			string dayCheck = dt.Day.ToString() + "/" + dt.Month.ToString();

			if (PerstDataTable == null)
			{
				DataSet recreader;
				recreader = DatabaseConnection.CreateDataset("SELECT CELDATE,DAYNAME FROM CELEBRATION WHERE (YEARS=0 OR YEARS=" + dt.Year.ToString() + ") AND NATION='" + UC.CultureSpecific.ToUpper() + "'");
				PerstDataTable = recreader.Tables[0];
			}
			DataRow[] dr = PerstDataTable.Select("Celdate='" + dayCheck + "'");
			if (dr.Length > 0)
			{
				descr = dr[0][1].ToString();
				return true;
			}

			return false;
		}

		public string GetEasterSunday(int year)
		{

			byte correction = 0;
			int month;
			int day = (19*(year%19) + 24)%30;
			day = 22 + day + ((2*(year%4) + 4*(year%7) + 6*day + 5 + correction)%7);

			if (day > 31)
			{
				month = 4;
				day -= 31;
			}
			else
			{
				month = 3;
			}

			return day + "/" + month;
		}

		public void DeleteRecurrence(int id)
		{
			DatabaseConnection.DoCommand("DELETE FROM RECURRENCE WHERE ID="+id);
		}
	}

}
