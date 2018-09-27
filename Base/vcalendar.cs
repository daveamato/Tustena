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
using System.Text;

namespace Digita.Tustena
{


	public class vCalendar
	{
		public vEvents Events;

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat("BEGIN:VCALENDAR{0}", Environment.NewLine);

			result.AppendFormat("VERSION:1.0{0}", Environment.NewLine);
			result.AppendFormat("METHOD:PUBLISH{0}", Environment.NewLine);
			foreach (vEvent item in Events)
			{
				result.Append(item.ToString());
			}
			result.AppendFormat("END:VCALENDAR{0}", Environment.NewLine);
			return result.ToString();
		}

		public vCalendar(vEvent value)
		{
			this.Events = new vEvents();
			this.Events.Add(value);
		}

		public vCalendar()
		{
			this.Events = new vEvents();
		}

		public class vAlarm
		{
			public TimeSpan Trigger; //Anticipo allarme
			public string action; //azione di notifica
			public string description; //descrizione dell'allarme

			public vAlarm()
			{
				Trigger = TimeSpan.FromDays(1);
				action = "DISPLAY";
				description = "Reminder";
			}

			public vAlarm(TimeSpan setTrigger)
			{
				Trigger = setTrigger;
				action = "DISPLAY";
				description = "Reminder";
			}

			public vAlarm(TimeSpan setTrigger, string setAction, string setDescription)
			{
				Trigger = setTrigger;
				action = setAction;
				description = setDescription;
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				result.AppendFormat("BEGIN:VALARM{0}", Environment.NewLine);
				result.AppendFormat("TRIGGER:P{0}DT{1}H{2}M{3}", Trigger.Days, Trigger.Hours, Trigger.Minutes, Environment.NewLine);
				result.AppendFormat("ACTION:{0}{1}", action, Environment.NewLine);
				result.AppendFormat("DESCRIPTION:{0}{1}", description, Environment.NewLine);
				result.AppendFormat("END:VALARM{0}", Environment.NewLine);
				return result.ToString();
			}
		}

		public class vEvent
		{
			public string UID; // UID dell'evento

			public DateTime DTStart; // inizio
			public DateTime DTEnd; // fine
			public DateTime DTStamp; // Timestamp
			public string Summary; // sommario/soggetto
			public string Organizer; // MAILTO: o nome

			public string Location;
			public string description;
			public string URL;
			public vAlarms Alarms; // allarmi

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				result.AppendFormat("BEGIN:VEVENT{0}", Environment.NewLine);
				result.AppendFormat("UID:{0}{1}", UID, Environment.NewLine);
				result.AppendFormat("SUMMARY:{0}{1}", Summary, Environment.NewLine);
				result.AppendFormat("ORGANIZER:{0}{1}", Organizer, Environment.NewLine);
				result.AppendFormat("LOCATION:{0}{1}", Location, Environment.NewLine);
				result.AppendFormat("DTSTART:{0}{1}", DTStart.ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
				result.AppendFormat("DTEND:{0}{1}", DTEnd.ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
				result.AppendFormat("DTSTAMP:{0}{1}", DateTime.Now.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
				result.AppendFormat("DESCRIPTION:{0}{1}", description, Environment.NewLine);
				if (URL.Length > 0)
					result.AppendFormat("URL:{0}{1}", URL, Environment.NewLine);

				foreach (vAlarm item in Alarms)
				{
					result.Append(item.ToString());
				}

				result.AppendFormat("END:VEVENT{0}", Environment.NewLine);
				return result.ToString();
			}

			public vEvent()
			{
				this.Alarms = new vAlarms();
			}
		}


		public class vAlarms : CollectionBase
		{
			public vAlarm Add(vAlarm value)
			{
				this.InnerList.Add(value);
				return value;
			}

			public vAlarm Item(int index)
			{
				return (vAlarm) this.InnerList[index];
			}

			public void Remove(int index)
			{
				vAlarm cust;
				cust = (vAlarm) this.InnerList[index];
				if (cust != null)
				{
					this.InnerList.Remove(cust);
				}

			}
		}

		public class vEvents : CollectionBase

		{
			public vEvent Add(vEvent value)
			{
				this.InnerList.Add(value);
				return value;
			}

			public vEvent Item(int index)
			{
				return (vEvent) this.InnerList[index];
			}

			public void Remove(int index)
			{
				vEvent cust;
				cust = (vEvent) this.InnerList[index];
				if (cust != null)
				{
					this.InnerList.Remove(cust);
				}
			}
		}
	}

}
