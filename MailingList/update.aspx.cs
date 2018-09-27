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
using System.Web.UI;
using Digita.Tustena.Database;

namespace Digita.Tustena.MailingList.include
{
	public partial class MLUpdate : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
				action();
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

		private void action()
		{
			switch (Request["a"])
			{
				case "remove":
					remove(0);
					break;
				case "update":
					update();
					break;
				case "abuse":
					abuse();
					break;
				case "stats":
					stats();
					break;
				default:
					Response.Write("Unknown command. Sorry.");
					break;
			}
		}

		private void remove(byte abuse)
		{
			string[] refid=DatabaseConnection.FilterInjection(Request["e"]).Split('$');
			try
			{
				DatabaseConnection.DoCommand("INSERT INTO ML_REMOVEDFROM (IDML,IDREF,TYPE,ABUSE) VALUES ("+int.Parse(Request["ml"].ToString())+","+refid[1]+","+refid[0]+","+abuse.ToString()+")");
				Response.Write("Your email address has been removed from the mailing list.");
			}catch
			{

			}


		}

		private void update()
		{
			Response.Write("Unknown command. Sorry.");

		}

		private void abuse()
		{
			string body = string.Empty;
			string[] refid=Request["e"].Split('$');
			try
			{
				switch (refid[0])
				{
					case "0":
						body = "Company: " + DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID="+refid[1]);
						break;
					case "1":
						body = "Contact: " + DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM BASE_CONTACTS WHERE ID="+refid[1]);
						break;
					case "2":
						body = "Lead: " + DatabaseConnection.SqlScalar("SELECT ISNULL(COMPANYNAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FORM CRM_LEADS WHERE ID="+refid[1]);
						break;
				}
				G.SendError("Report Abuse",body);

				remove(1);
			}
			catch{}

		}

		private void stats()
		{
			Response.Clear();
			Response.ContentType = "image/gif";
			Response.TransmitFile(Path.Combine(Server.MapPath("/images"), "q.gif"));
			Response.Flush();
			Response.End();
		}
	}
}
