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
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using Digita.Tustena.Database;

namespace Digita.Tustena.Base
{
	public class UsageStat : IDisposable
	{
		private static String Url = "http://www.tustena.com/usage/log.aspx";
		private string dllInfo;
		private string dllHash;
		private string uniqueId;

		public UsageStat(string server_name)
		{
			if (server_name.ToLower().IndexOf("localhost") < 0)
			{
				if(LoadInfo())
					SendStats();
			}
		}

		private bool LoadInfo()
		{
			bool toupdate=false;
			try
			{

				DataTable dt = DatabaseConnection.CreateDataset("SELECT TOP 1 * FROM VERSION ORDER BY ID").Tables[0];

				string strPath = Assembly.GetExecutingAssembly().CodeBase;
				string fullName = Assembly.GetExecutingAssembly().FullName;
				if (strPath.StartsWith("file:"))
					strPath = strPath.Remove(0, 8);
				if (fullName.Length > 0)
				{
					Match m = Regex.Match(fullName, "Version=([^, ]+r?)");

					if (m.Success)
						dllInfo = m.Groups[1].Value;
				}
				FileStream fs = File.OpenRead(strPath);
				dllHash = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ReadFully(fs)));

				if(dt.Rows.Count==0)
				{
					DatabaseConnection.DoCommand(string.Format("INSERT INTO VERSION (DBVERSION,SWVERSION,DLLHASH) VALUES (0,'{0}','{1}')",dllInfo,dllHash));
					toupdate=true;
				}
				else
				{
					if(dt.Rows[0]["swversion"].ToString()!=dllInfo || dt.Rows[0]["dllhash"].ToString()!=dllHash)
					{
						DatabaseConnection.DoCommand(string.Format("INSERT INTO VERSION (SWVERSION,DLLHASH,MODIFIED) VALUES ('{0}','{1}',1)",dllInfo,dllHash));
						toupdate=true;
					}
				}

			}
			catch
			{
				toupdate=false;
			}

			return toupdate;
		}

		private void SendStats()
		{
			new Thread(new ThreadStart(WebCom));
		}

		private void WebCom()
		{
			using (WebClient wc = new WebClient())
			{
				wc.QueryString.Add("d", DateTime.Now.ToUniversalTime().ToString());
				wc.QueryString.Add("v", dllInfo);
				wc.QueryString.Add("h", dllHash);
				wc.DownloadData(Url);
			}
		}

		public static byte[] ReadFully(Stream stream)
		{
			byte[] buffer = new byte[32768];
			using (MemoryStream ms = new MemoryStream())
			{
				while (true)
				{
					int read = stream.Read(buffer, 0, buffer.Length);
					if (read <= 0)
						return ms.ToArray();
					ms.Write(buffer, 0, read);
				}
			}
		}


		public void Dispose()
		{
		}

	}
}
