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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace Digita.Tustena.Report
{
	public partial class NewDoc : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			gendoc();
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion

		private void gendoc()
		{
			StringBuilder sb = new StringBuilder();
			string fileName = Server.MapPath(".") + Path.DirectorySeparatorChar + "test.doc";
			StreamReader sr = new StreamReader(fileName);
			while (sr.Peek() != -1)
			{
				sb.Append(sr.ReadToEnd());
			}
			sr.Close();
			foreach (Match match in Regex.Matches(sb.ToString(), @"(\[Tustena\.(?<content>.*?)\])", RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture))
			{
				Response.Write("il campo: " + match.Groups["content"].Value);
				Response.Write(" del database deve rimpiazzare il valore: " + match.Value);
				Response.Write(" del file<br>");
				sb.Replace(match.Value, "test");
			}

			Response.Clear();
			Response.AddHeader("Content-Disposition", "attachment; filename=test.doc");
			Response.AddHeader("Expires", "Thu, 01 Dec 1994 16:00:00 GMT ");
			Response.AddHeader("Pragma", "nocache");
			Response.ContentType = "text/rtf";
			Response.Write(sb.ToString());
			Response.Flush();
			Response.End();

		}

	}
}
