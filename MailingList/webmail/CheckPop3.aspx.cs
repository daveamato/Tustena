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
using System.Web.UI.WebControls;
using Digita.Pop3;
using Digita.Tustena.Core;

namespace Digita.Tustena
{
	public partial class CheckPop3 : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
                ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				string pop3 = Request["pop3"];
				string addr = Request["addr"];
				string pass = Request["pass"];

				bool secure = false;
				if(pop3.StartsWith("!"))
				{
					pop3=pop3.Substring(1);
					secure=true;
				}
				try
				{
					using (Pop3Client email = new Pop3Client(addr, pass, pop3, secure))
					{
						email.OpenInbox();
						long x= email.MessageCount;
						email.CloseConnection();
						Address.Text =Root.rm.GetString("Mailtxt20");
					}
				}
				catch (Pop3PasswordException)
				{
					Address.Text =Root.rm.GetString("Mailtxt16");
				}
				catch (Pop3LoginException)
				{
					Address.Text =Root.rm.GetString("Mailtxt17");
				}
				catch (Pop3ConnectException)
				{
					Address.Text=Root.rm.GetString("Mailtxt16");
				}
				catch
				{
					Address.Text =Root.rm.GetString("Mailtxt16");
				}


			}
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
