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

using System.Configuration;

namespace Digita.Tustena.Core
{
	public class ConfigSettings
	{
		private static string connection = null;
		private static string maxResults = null;
		private static string supportedLanguages = null;
		private static string supportedLanguagesDescription = null;
		private static string tustenaMainMail = null;
		private static string mailSpoolPath = null;
		private static string sMTPServer = null;
		private static string sMTPPort = null;
		private static string sMTPUser = null;
		private static string sMTPPassword = null;
		private static string mailMailingPath = null;
		private static string dataStoragePath = null;
		private static string spoolFormat = null;
		private static string tustenaErrorMail = null;
		private static string sMTPAuthRequired = null;
		private static string useSpoolService = null;
		private static string timerPagePath=null;
		private static string dns1 = null;
		private static string dns2 = null;
		private static string mode = null;

		private ConfigSettings()
		{
		}

		public static string Connection
		{
			get
			{
				if (connection == null)
					connection = ConfigurationManager.AppSettings["Connection"] as string;
				return connection;
			}
		}
		public static string MaxResults
		{
			get
			{
				if (maxResults == null)
                    maxResults = ConfigurationManager.AppSettings["maxResult"] as string;
				return maxResults;
			}
		}
		public static string SupportedLanguages
		{
			get
			{
				if (supportedLanguages == null)
                    supportedLanguages = ConfigurationManager.AppSettings["SupportedLanguages"] as string;
				return supportedLanguages;
			}
		}

		public static string SupportedLanguagesDescription
		{
			get
			{
				if (supportedLanguagesDescription == null)
                    supportedLanguagesDescription = ConfigurationManager.AppSettings["SupportedLanguagesDescription"] as string;
				return supportedLanguagesDescription;
			}
		}

		public static string TustenaMainMail
		{
			get
			{
				if (tustenaMainMail == null)
                    tustenaMainMail = ConfigurationManager.AppSettings["TustenaMainMail"] as string;
				return tustenaMainMail;
			}
		}

		public static string MailSpoolPath
		{
			get
			{
				if (mailSpoolPath == null)
                    mailSpoolPath = ConfigurationManager.AppSettings["MailSpoolPath"] as string;
				return mailSpoolPath;
			}
		}

		public static string SchedulerInterval
		{
			get
			{
				if (timerPagePath == null)
                    timerPagePath = ConfigurationManager.AppSettings["SchedulerInterval"] as string;
				return timerPagePath;
			}
		}

		public static string SMTPServer
		{
			get
			{
				if (sMTPServer == null)
                    sMTPServer = ConfigurationManager.AppSettings["SMTPServer"] as string;
				return sMTPServer;
			}
		}

		public static string SMTPPort
		{
			get
			{
				if (sMTPPort == null)
                    sMTPPort = ConfigurationManager.AppSettings["SMTPPort"] as string;
				return sMTPPort;
			}
		}

		public static string SMTPUser
		{
			get
			{
				if (sMTPUser == null)
                    sMTPUser = ConfigurationManager.AppSettings["SMTPUser"] as string;
				return sMTPUser;
			}
		}

		public static string SMTPPassword
		{
			get
			{
				if (sMTPPassword == null)
                    sMTPPassword = ConfigurationManager.AppSettings["SMTPPassword"] as string;
				return sMTPPassword;
			}
		}

		public static string MailMailingPath
		{
			get
			{
				if (mailMailingPath == null)
                    mailMailingPath = ConfigurationManager.AppSettings["MailMailingPath"] as string;
				return mailMailingPath;
			}
		}

		public static string DataStoragePath
		{
			get
			{
				if (dataStoragePath == null)
                    dataStoragePath = ConfigurationManager.AppSettings["DataStoragePath"] as string;
				return dataStoragePath;
			}
		}


		public static string SpoolFormat
		{
			get
			{
				if (spoolFormat == null)
                    spoolFormat = ConfigurationManager.AppSettings["SpoolFormat"] as string;
				return spoolFormat;
			}
		}

		public static string TustenaErrorMail
		{
			get
			{
				if (tustenaErrorMail == null)
                    tustenaErrorMail = ConfigurationManager.AppSettings["tustenaErrorMail"] as string;
				return tustenaErrorMail;
			}
		}

		public static bool SMTPAuthRequired
		{
			get
			{
				if (sMTPAuthRequired == null)
                    sMTPAuthRequired = ConfigurationManager.AppSettings["SMTPAuthRequired"] as string;
				return (sMTPAuthRequired == "1") ? true : false;
			}
		}

		public static bool UseSpoolService
		{
			get
			{
				if (useSpoolService == null)
                    useSpoolService = ConfigurationManager.AppSettings["UseSpoolService"] as string;
				return (useSpoolService == "1") ? true : false;
			}
		}

		public static string DNS1
		{
			get
			{
				if (dns1 == null)
                    dns1 = ConfigurationManager.AppSettings["DNS1"] as string;
				return dns1;
			}
		}

		public static string DNS2
		{
			get
			{
				if (dns2 == null)
                    dns2 = ConfigurationManager.AppSettings["DNS2"] as string;
				return dns2;
			}
		}

		public static string Mode
		{
			get
			{
				if (mode == null)
                    mode = ConfigurationManager.AppSettings["Mode"] as string;
				return mode;
			}
		}


	}
}
