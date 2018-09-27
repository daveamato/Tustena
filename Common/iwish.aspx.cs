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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Common
{
	public partial class iwish : G //System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					Type.Items.Add(Root.rm.GetString("Feedtxt6"));
					Type.Items.Add(Root.rm.GetString("Feedtxt7"));
					Type.Items[0].Selected=true;
					Button2.Text =Root.rm.GetString("Feedtxt8");
					if(Request["live"]!=null)
						livechat.Visible=true;
					else
						livechat.Visible=false;

				}
				else
				{
					Panel1.Visible = false;
					Panel2.Visible = true;

					MessagesHandler.SendMail("dennis@digita.it", "tustena@tustena.com",
						"[Tustena - I Wish][" + UC.UserName + "]" + IWSubject.Text,
						Type.SelectedItem.Text + Environment.NewLine + IWNote.Text);
				}
			}
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
	}
}
