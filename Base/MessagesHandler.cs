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
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using Digita.Mailer;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Base
{
	public class MessagesHandler
	{
		public MessagesHandler()
		{
		}

		public static void MultiMessageSend()
		{

		}

		public static bool CheckForNewMessage(int UID, out string[] messagesId)
		{
			messagesId = null;
			object messagesHT = HttpContext.Current.Application["messages"];
			if(!(messagesHT is Hashtable))
				return false;

			if(((Hashtable)messagesHT).ContainsKey(UID))
			{
				messagesId = ((Hashtable)messagesHT)[UID].ToString().Split('|');
				((Hashtable)messagesHT).Remove(UID);
				HttpContext.Current.Application["messages"] = messagesHT;
				return true;
			}
			else
			{
				if(((Hashtable)messagesHT).Count==0)
					HttpContext.Current.Application["messages"] = null;
				return false;
			}
		}

		public static void NotifyNewMessage(int UID, int messagesId)
		{
			object messagesHT = HttpContext.Current.Application["messages"];
			if(!(messagesHT is Hashtable))
				messagesHT = new Hashtable();

			if(((Hashtable)messagesHT).ContainsKey(UID))
			{
				string[] currentMessages = ((Hashtable)messagesHT)[UID].ToString().Split('|');
				((Hashtable)messagesHT)[UID]=String.Join("|",StringArrayAdd(currentMessages,messagesId.ToString()));
			}
			else
				((Hashtable)messagesHT).Add(UID,messagesId);
			HttpContext.Current.Application["messages"] = messagesHT;
		}

		public static string[] StringArrayAdd(string[] currentMessages, string s)
		{
			string[] newMessages = new string[currentMessages.Length+1];
			newMessages[0]=s;
			currentMessages.CopyTo(newMessages,1);
			return newMessages;
		}

		public static bool NotifyMSN(string UID, string message)
		{
			string MSNName = DatabaseConnection.SqlScalar(String.Format("SELECT MSN FROM ACCOUNT WHERE ID={0}", UID));
			if(MSNName==null)
				return false;
			FileFunctions.WriteTextToFile(Path.Combine(Path.Combine(ConfigSettings.DataStoragePath, "MSN"),Guid.NewGuid().ToString() + ".msn"),message);
			return true;
		}


        public static void SendMail(string mailTo, string mailSubject, string mailBody)
        {
            SendMail(mailTo, null, mailSubject, mailBody);
        }


		public static void SendMail(string mailTo, string mailFrom, string mailSubject, string mailBody)
		{
			SmtpEmailer emailer = new SmtpEmailer();
			emailer.Host = ConfigSettings.SMTPServer;
			try
			{
				emailer.Port = int.Parse(ConfigSettings.SMTPPort);
			}
			catch
			{
				emailer.Port = 25;
			}
			if (mailFrom == null)
				emailer.From = ConfigSettings.TustenaMainMail;
			else
				emailer.From = mailFrom;
			emailer.Subject = mailSubject;
			if (Regex.Match(mailBody, "</?\\w+\\s+[^>]*>").Success)
				emailer.SendAsHtml = true;
			else
				emailer.SendAsHtml = false;

            if (mailTo.IndexOf(';') > 0)
            {
                string[] to = mailTo.Split(';');
                foreach (string t in to)
                {
                    if(t.Length>0)
                        emailer.To.Add(t);
                }
            }else
			    emailer.To.Add(mailTo);
			emailer.Body = mailBody;
			switch (ConfigSettings.SpoolFormat)
			{
				case "mssmtp":
					emailer.SendIISSMTPMessage(ConfigSettings.MailSpoolPath);
					break;
				case "xmail":
					emailer.SendXMailMessage(ConfigSettings.MailSpoolPath);
					break;
				default:
					emailer.Host = ConfigSettings.SMTPServer;
					emailer.AuthenticationMode = AuthenticationType.Plain;
					if (ConfigSettings.SMTPAuthRequired)
					{
						emailer.User = ConfigSettings.SMTPUser;
						emailer.Password = ConfigSettings.SMTPPassword;
					}
					emailer.SendMessageAsync();
					break;
			}
		}

		public enum MultiNotify
		{
			Mail=0,MSN=1,SMS=2,Desktop=4
		}
	}
}
