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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.MailingList
{
	public partial class sendsms : G
	{
		private int SmsCredit = 0;

		private int smscredit()
		{

			return int.Parse(DatabaseConnection.SqlScalar("SELECT TOP 1 SMSCREDIT FROM TUSTENA_DATA"));

		}

		protected void Page_Load(object sender, EventArgs e)
		{
			TxtMessage.Attributes.Add("onChange","message_onChange(this);");
			TxtMessage.Attributes.Add("onKeyUp","message_onChange(this);");
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{

				try
				{
					SmsCredit = smscredit();
				}
				catch{}

				if(!Page.IsPostBack)
				{
					MsgLabel.Text="You have "+SmsCredit.ToString()+" SMS left in your pocket";
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
			this.BtnSend.Click += new EventHandler(this.Send_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void Send_Click(object sender, EventArgs e)
		{
			if(SmsCredit>=0)
			{
				if(TxtNumber.Text.Length>10)
				{
					if(TxtMessage.Text.Length>0)
					{
						wmsmsgateway sms = new wmsmsgateway();
						sms.mytakecredit +=new TakeCredit(UpdateCredit);
						sms.myeventerror +=new ErrorHandler(sms_myeventerror);
						int smsResult = sms.SendSMS(TxtNumber.Text,TxtMessage.Text);

						switch(smsResult)
						{
							case -1:
								MsgLabel.Text=Root.rm.GetString("SMStxt1");
								break;
							case 0:
								MsgLabel.Text=Root.rm.GetString("SMStxt2");
								break;
							case 1:
								MsgLabel.Text=Root.rm.GetString("SMStxt3");
								break;
							case 2:
								MsgLabel.Text=Root.rm.GetString("SMStxt4");
								break;
						}




					}
					else
					{
						MsgLabel.Text=Root.rm.GetString("SMStxt5");
					}
				}
				else
				{
					MsgLabel.Text=Root.rm.GetString("SMStxt6");
				}
			}
			else
				MsgLabel.Text="You have NO SMS left in your pocket!";
		}

		private void UpdateCredit(int credit)
		{
			if(credit>0)
				DatabaseConnection.DoCommand("UPDATE TUSTENA_DATA SET SMSCREDIT=SMSCREDIT-"+credit);
		}

		private void sms_myeventerror(string error)
		{
			G.SendError("[Tustena SMS] Error",error);
		}
	}
}
