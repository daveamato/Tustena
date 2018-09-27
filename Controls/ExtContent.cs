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

using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;

namespace Digita.Tustena.WebControls
{
	public class ExtContent : Control
	{
		public ExtContent()
		{
		}

		private string Src = string.Empty;
		public string src
		{
			set{Src=value;}
		}

		private string CssClass = string.Empty;
		public string cssClass
		{
			set{CssClass=value;}
		}

		private extContentStatusOptions extContentStatus = extContentStatusOptions.unknown;
		public extContentStatusOptions ExtContentStatus
		{
			get{return extContentStatus;}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			string content = CheckFilePath(Src);
			if(extContentStatus == extContentStatusOptions.ok)
				writer.Write(string.Format("<div id=\"{0}\" class=\"{1}\"></div>",this.ID,CssClass,content));
		}

		private string GetFileContent(string fileName)
		{
			StreamReader objReader = new StreamReader(fileName);
				string content = objReader.ReadToEnd();
				objReader.Close();
			return content;
		}

		private string GetWebContent(string fileName)
		{
			string ret = null;
			try
			{
				WebClient wc = new WebClient();
				byte[] b = wc.DownloadData(fileName);
				ret = Encoding.UTF8.GetString(b);
			}catch{}
			return ret;
		}


		private string CheckFilePath(string fileName)
		{
			if(fileName.StartsWith("http"))
			{
				string webPage = GetWebContent(fileName);
				if(webPage==null)
					extContentStatus = extContentStatusOptions.pageNotFound;
				return webPage;
			}
			else
			{
				if(File.Exists(fileName))
					return GetFileContent(fileName);
				else
				{
					extContentStatus = extContentStatusOptions.fileNotFound;
					return null;
				}
			}
		}

		public enum extContentStatusOptions
		{
			unknown,ok,fileNotFound,pageNotFound
		}
	}
}
