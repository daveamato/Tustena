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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.UI;

namespace Digita.Tustena
{
	public class Precompile : HttpApplication
	{
		private static bool needsCompile = true;
		private static string applicationPath = String.Empty;
		private static string physicalPath = String.Empty;
		private static string applicationURL = String.Empty;

		private static Thread thread = null;

		protected virtual string SkipFiles
		{
			get { return @""; }
		}

		protected virtual string SkipFolders
		{
			get { return @"template;components"; }
		}

		public override void Init()
		{
			if (Precompile.needsCompile)
			{
				Precompile.needsCompile = false;

				applicationPath = HttpContext.Current.Request.ApplicationPath;
				if (!applicationPath.EndsWith("/")) { applicationPath += "/";	}

				string server = HttpContext.Current.Request.ServerVariables["SERVER_NAME"];
				bool https = HttpContext.Current.Request.ServerVariables["HTTPS"] != "off";
				applicationURL = (https ? "https://" : "http://") + server + applicationPath;

				physicalPath = HttpContext.Current.Request.PhysicalApplicationPath;
				thread = new Thread(new ThreadStart(CompileApp));
				thread.Start();
			}
		}

		private void CompileApp()
		{
			CompileFolder(physicalPath);
		}

		private void CompileFolder(string folder)
		{
			foreach (string file in Directory.GetFiles(folder, "*.as?x"))
			{
				CompileFile(file);
			}

			foreach (string subFolder in Directory.GetDirectories(folder))
			{
				bool skipFolder = false;
				foreach (string item in this.SkipFolders.Split(';'))
				{
					if ((item != null && item.Length != 0) && subFolder.ToUpper().EndsWith(item.ToUpper()))
					{
						skipFolder = true;
						break;
					}
				}
				if (!skipFolder)
				{
					CompileFolder(subFolder);
				}
			}
		}

		private void CompileFile(string file)
		{
			bool skipFile = false;
			foreach (string item in this.SkipFiles.Split(';'))
			{
				if ((item != null && item.Length != 0) && file.ToUpper().EndsWith(item.ToUpper()))
				{
					skipFile = true;
					break;
				}
			}

			if (!skipFile)
			{
				string path = file.Remove(0, physicalPath.Length);
				if (file.ToLower().EndsWith(".ascx"))
				{
					string virtualPath = applicationPath + path.Replace(@"\", "/");
					Page controlLoader = new Page();
					try
					{
						controlLoader.LoadControl(virtualPath);
					}
					finally
					{
						Debug.WriteLine(virtualPath, "Control");
					}
				}
				else if (!file.ToLower().EndsWith(".asax"))
				{
					string url = applicationURL + path.Replace(@"\", "/");
					using (HttpWebRequest.Create(url).GetResponse()) {}
					Debug.WriteLine(url, "Page");
				}
			}
		}
	}
}

