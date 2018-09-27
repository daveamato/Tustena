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
using System.Globalization;
using System.IO;
using System.Web.UI;
using Digita.Tustena.Database;
using Digita.Mailer;

namespace Digita.Tustena.MailingList
{
	public partial class FastMailing : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string template;
			string templateUser;
			String vbCrLf = "\r\n";
			String vbTab = "\t";
			StreamReader objReader;

			objReader = new StreamReader(Request.PhysicalApplicationPath + "template" + Path.DirectorySeparatorChar + "didis.txt");
			template = objReader.ReadToEnd();
			objReader.Close();

			DataSet myDataSet = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME,EMAIL FROM BASE_COMPANIES WHERE (CATEGORIES LIKE '%|93|%') ORDER BY COMPANYNAME");

			foreach (DataRow dr in myDataSet.Tables[0].Rows)
			{
				templateUser = template;
				string user = String.Empty;
				string password = String.Empty;
				user = DatabaseConnection.SqlScalar("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE (IDRIF = 31) AND (ID = " + dr["id"].ToString() + ")");
				password = DatabaseConnection.SqlScalar("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE (IDRIF = 32) AND (ID = " + dr["id"].ToString() + ")");

				templateUser = templateUser.Replace("[companyname]", dr["companyname"].ToString());
				templateUser = templateUser.Replace("[user]", user);
				templateUser = templateUser.Replace("[password]", password);

				NewMessage msg;

				msg = new NewMessage(new EmailAddress("sara.armellini@didis.it"), "Dati Didis", templateUser, NewMessage.MessageType.Plain);
				msg.AddHeader("X-Originating-IP", Request.UserHostAddress);
				msg.AddHeader("X-Originating-UA", Request.UserAgent);

				msg.AddHeader("Received", "from " + Request.UserHostName + " [" + Request.UserHostAddress + "]" +
					vbCrLf + vbTab + "by " + Request.Url.Host + " [" + Request.ServerVariables["LOCAL_ADDR"] + "] (Digita.MimeParser) via HTTP;" +
					vbCrLf + vbTab + DateTime.UtcNow.ToString(@"ddd, dd MMM yyyy HH\:mm\:ss", new CultureInfo("en-US")) + " +0000");


				msg.AddRecipient(dr["email"].ToString().Trim(), NewMessage.RecipientTypes.To);

				TextWriter MailFile = File.CreateText(
					Path.Combine(
						Request.PhysicalApplicationPath,
						"mailinglist"+Path.DirectorySeparatorChar+"xmail"+Path.DirectorySeparatorChar+"spool"+Path.DirectorySeparatorChar+"temp" + Path.DirectorySeparatorChar + msg.messageID));

				Response.Clear();
				Response.Write("processing: \"" + dr["companyname"].ToString() + "\"<br>");
				Response.Flush();
				try
				{
					string lineToWrite = msg.ToString(NewMessage.MessageFormats.XMailSpool);
					lineToWrite = lineToWrite.Replace("=?utf-7?Q??=", "");
					lineToWrite = lineToWrite.Replace("=?utf-7?Q?", "");
					lineToWrite = lineToWrite.Replace("?=", "");


					MailFile.WriteLine(lineToWrite);
					MailFile.Close();


					Response.Clear();
					Response.Write("writing: \"" + dr["email"].ToString() + "\";\"" + dr["companyname"].ToString() + "\";\"" + user + "\";\"" + password + "\"<br>");
					Response.Flush();
				}
				catch
				{
					Response.Clear();
					Response.Write("error writing: <b>" + dr["companyname"].ToString() + "</b><br>");
					Response.Flush();
				}
			}
		}

		#region Codice generato da Progettazione Web Form

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
	}
}
