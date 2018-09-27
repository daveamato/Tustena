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
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Admin
{
	public partial class PasswordRecovery : UserControl
	{


		protected void Page_Load(object sender, EventArgs e)
		{
			SubmitBtn.Text=Root.rm.GetString("PassRecovery3");
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
			this.SubmitBtn.Click +=new EventHandler(SubmitBtn_Click);

		}
		#endregion

		private void SubmitBtn_Click(object sender, EventArgs e)
		{

			DataTable dt = DatabaseConnection.CreateDataset("SELECT PASSWORD,USERACCOUNT,NOTIFYEMAIL,CULTURE FROM ACCOUNT WHERE USERACCOUNT='"+DatabaseConnection.FilterInjection(Usr.Text)+"' OR NOTIFYEMAIL='"+DatabaseConnection.FilterInjection(Usr.Text)+"'").Tables[0];
			if(dt.Rows.Count==1)
			{
				try
				{
					DataRow dr = dt.Rows[0];

						SendUserData(dr["culture"].ToString().Substring(0,2),dr["useraccount"].ToString(),dr["password"].ToString(),dr["notifyemail"].ToString());

					LblInfo.Text=Root.rm.GetString("PassRecovery2");
				}
				catch
				{
					LblInfo.Text=Root.rm.GetString("PassRecovery1");
				}

			}else
			{
				LblInfo.Text=Root.rm.GetString("PassRecovery1");
			}

		}

		private void SendUserData(string lng, string user, string pass, string _to)
		{
			string template;
			StreamReader objReader;

			switch(lng.ToLower())
			{
				case "it":
					objReader = new StreamReader(Request.PhysicalApplicationPath + "template" + Path.DirectorySeparatorChar + "lostpassword_it.txt");
					break;
				default:
					objReader = new StreamReader(Request.PhysicalApplicationPath + "template" + Path.DirectorySeparatorChar + "lostpassword_en.txt");
					break;
			}

			template = objReader.ReadToEnd();
			objReader.Close();




			template = template.Replace("[Tustena.UserID]", user);
			template = template.Replace("[Tustena.Password]", pass);

			string from = String.Empty;
			string to = String.Empty;
			string subject = String.Empty;
			from = ConfigSettings.TustenaMainMail;
			subject = "Tustena CRM Activated";
			if(_to.Length==0)
				to=user;
			else
				to=_to;

			MessagesHandler.SendMail(to,from,subject,template);
		}
	}
}
