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
using Digita.Tustena.Database;

namespace Digita.Tustena.CRM
{
	public partial class NewMessage : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>window.close();</script>";
			}
			else
			{
				SubjectValidator.ErrorMessage=Root.rm.GetString("Nmstxt9");
				TextValidator.ErrorMessage=Root.rm.GetString("Nmstxt10");
				valSum.HeaderText=Root.rm.GetString("ValidSummary");

				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					Submit.Text =Root.rm.GetString("Nmstxt11");
					Office.ClearUserList=true;
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
			this.Submit.Click+=new EventHandler(this.Submit_Click);
		}

		#endregion

		private void Submit_Click(Object sender, EventArgs e)
		{
			if(Office.GetValue.Length>0)
			{
				foreach (string im in Office.GetValueArray)
				{
					object newMessageId=null;
					for(int i=0;i<2;i++)
					{
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("SUBJECT", Subject.Text);
							dg.Add("BODY", Text.Text);
							dg.Add("FROMACCOUNT", UC.UserId);
							dg.Add("TOACCOUNT", im);
							dg.Add("INOUT",i);
							if(newMessageId==null)
								newMessageId=dg.Execute("BASE_MESSAGES",DigiDapter.Identities.Identity);
							else
								dg.Execute("BASE_MESSAGES");
						}
					}
					MessagesHandler.NotifyNewMessage(int.Parse(im),Convert.ToInt32(newMessageId));
				}
				Subject.Text = String.Empty;
				Text.Text = String.Empty;

				SomeJS.Text = "<script>opener.location = opener.location;window.close();</script>";
			}else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nousers","<script>alert('" + Root.rm.GetString("Nmstxt12")+"');</script>");
			}
		}
	}
}
