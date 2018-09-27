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
using System.Text;
using System.Text.RegularExpressions;
using Digita.Pop3;

namespace Digita.Tustena.MailingList.webmail
{
	public partial class ShowMsg : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			Response.Write(Session["mailbody"]);
			Session.Remove("mailbody");
		}

		private string transformFormats(string s)
		{
			Match m;
			if ((m = Regex.Match(s, "=\\?([\\d\\w\\-]+)\\?([QB])\\?(.*?)\\?=")).Success)
			{
				Encoding enc = Encoding.GetEncoding(m.Groups[1].Value);
				switch (m.Groups[2].Value)
				{
					case "Q": //Quoted printable
						s = enc.GetString(Encoding.UTF8.GetBytes(Pop3Utils.FromQuotedPrintable((m.Groups[3].Value))));
						break;
					case "B": //base 64
						s = enc.GetString(Convert.FromBase64String(m.Groups[3].Value));
						break;
				}
			}
			return s;
		}

		protected String FormattaBody(String body, String contentType)
		{
			if (contentType=="P")
			{
				return "<pre>" + body + "</pre>";
			}
			return body;
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
