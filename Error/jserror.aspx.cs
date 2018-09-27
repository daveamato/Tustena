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
using System.Web.UI;
using Digita.Mailer;
using Digita.Tustena.Base;
using Digita.Tustena.Core;

namespace Digita.Tustena.Error
{
	public partial class jserror : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			{
				string machineinfo;
				try
				{
					UserConfig UC = (UserConfig) Session["UserConfig"];
					machineinfo = "<span style=\"font-family:verdana;font-size=11px;\">Server:" + this.Server.MachineName + "<br>" +
						"User:" + UC.UserId + "-" + UC.UserRealName + "<br>Agent: " + Request.UserAgent;
					if (Request.UrlReferrer != null)
						machineinfo += "<br>Referer: " + Request.UrlReferrer.ToString() + "<br>";
				}
				catch
				{
					machineinfo = "Errore javascript.<br>";
				}
				string message = Request.QueryString["error"];

				MessagesHandler.SendMail(ConfigSettings.TustenaErrorMail,ConfigSettings.TustenaErrorMail,"[Tustena] Javascript Error",machineinfo + message+ "</span>");
				SmtpEmailer emailer = new SmtpEmailer();
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
