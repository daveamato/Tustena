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
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class login : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			G.NoRender();
			if (Request.Params["usr"] != null && Request.Params["pwd"] != null)
			{
				TxtUsr.Text = Request.Params["usr"].Trim();
				TxtPwd.Text = Request.Params["pwd"].Trim();
				LoadPersonalData(true);
			}

			if (Login())
			{
				if((bool)DatabaseConnection.SqlScalartoObj("SELECT WIZARD FROM TUSTENA_DATA"))
				Response.Redirect("~/wizards/newuserwizard.aspx?m=7&si=99");
				else
					Response.Redirect("today.aspx?m=25");

			}


			LoginValidator.ValidationExpression = @"(^\s*)[A-Za-z0-9_\-\.@]{2,}(\s*)$";
			{
				if (Session["loginerror"] != null)
				{
					Context.Items.Add("warning", Session["loginerror"].ToString());
					Session.Remove("loginerror");
				}

			}



			if (TxtUsr.Text.Length > 0)
				SetFocus(TxtPwd);
			else
				SetFocus(TxtUsr);
			Submit.Text =Root.rm.GetString("Lgntxt3");
			Validator1.Text =Root.rm.GetString("Lgntxt4");
			Validator2.Text =Root.rm.GetString("Lgntxt5");
			Validator3.Text =Root.rm.GetString("Lgntxt6");
			CkbPL.Text =Root.rm.GetString("Lgntxt20");
		}

		private void WinAuth()
		{
			bool useNTLM = HttpContext.Current.User is WindowsPrincipal;

			if (useNTLM && HttpContext.Current.User.Identity.IsAuthenticated)
			{
				DataRowCollection dr = DatabaseConnection.CreateDataset("SELECT ACCOUNT.USERACCOUNT, ACCOUNT.PASSWORD FROM LOCALUSER LEFT OUTER JOIN ACCOUNT ON LOCALUSER.ACCOUNTID = DBO.ACCOUNT.UID WHERE LOCALUSER.USERNAME='" + HttpContext.Current.User.Identity.Name.ToString() + "'").Tables[0].Rows;
				if (dr.Count > 0)
				{
					TxtUsr.Text = dr[0][0].ToString();
					TxtPwd.Text = dr[0][1].ToString();
					LoadPersonalData(true);
				}
			}
		}

		private void BtnLogin_Click(Object sender, EventArgs e)
		{
			if ((Page.IsPostBack) && (Page.IsValid))
			{
				if (DatabaseConnection.SQLSecure(TxtUsr.Text))
				{
					LoadPersonalData(false);
				}
				else
				{
					TxtMessage.Text =Root.rm.GetString("Lgntxt22");
				}
			}
		}

		private void LoadPersonalData(bool isPersistent)
		{
			UC = UserData.LoadPersonalData(TxtUsr.Text.Trim(), TxtPwd.Text.Trim(), -1);

			if (UC != null && UC.Logged == LoggedStatus.yes)
			{
				Session["UserConfig"] = UC;

				TxtMessage.Text =Root.rm.GetString("Lgntxt23");
				if (UC.PersistLogin || CkbPL.Checked)
				{
					string Rstr = RandomString(32, true);
					HttpCookie cookie = new HttpCookie("PersistLogin");
					cookie.Values.Add("uid", UC.UserName);
					cookie.Values.Add("code", Rstr);
					DateTime dtNow = DateTime.Now;
					TimeSpan tsMinute = new TimeSpan(5, 0, 0, 0);
					cookie.Expires = dtNow + tsMinute;
					cookie.Domain = "crm.tustena.com";
					Response.Cookies.Add(cookie);

					DatabaseConnection.DoCommand(String.Format("UPDATE ACCOUNT SET PERSISTLOGIN=1, PERSISTCODE='{0}' WHERE UID='{1}'", Rstr, UC.UserId));
				}

				UpdateAccess(-1);
				DatabaseConnection.DoCommand(String.Format("UPDATE ACCOUNT SET STATE=1,LASTLOGIN=GETDATE() WHERE UID={0}", UC.UserId));

				DatabaseConnection.DoCommand("INSERT INTO LOGINLOG (USERID) VALUES (" + UC.UserId + ")");

				Cache[UC.UserId.ToString()] = Session.SessionID;

				if (Session["backafterlogin"] == null)
				{

					if(((bool)DatabaseConnection.SqlScalartoObj("SELECT WIZARD FROM TUSTENA_DATA")))
					Response.Redirect("/wizards/newuserwizard.aspx?m=7&si=99");
					else
						Response.Redirect("today.aspx?m=25");
				}
				else
				{
					string backAfter = Session["backafterlogin"].ToString();
					Session.Remove("backafterlogin");

					if((bool)DatabaseConnection.SqlScalartoObj("SELECT WIZARD FROM TUSTENA_DATA"))
					Response.Redirect("/wizards/newuserwizard.aspx?m=7&si=99");
					else
						Response.Redirect(backAfter);
				}

			}
			else
			{
				Session["UserConfig"] = null;

				if (UC.Logged == LoggedStatus.testing)
					Session["testing"] = "true";
				else if (UC.Logged == LoggedStatus.no)
					Session["loginerror"] =Root.rm.GetString("Lgntxt21");
				else
				{
					Session["loginerror"] =Root.rm.GetString("Lgntxt25");
					Session["testing"] = "false";
				}
                if (Session["loginerror"] != null) TxtMessage.Text = Session["loginerror"].ToString();
			}
		}

		private string RandomString(int length, bool nums)
		{
			const string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
				+ "abcdefghijklmnopqrstuvwxyz";
			const string abc123 = abc + "0123456789";
			string charList;
			Random AppRandom = new Random((int) DateTime.Now.Ticks);
			if (nums)
			{
				charList = abc123;
			}
			else
			{
				charList = abc;
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				int r = AppRandom.Next(0, charList.Length);
				sb.Append(charList.Substring(r, 1));
			}
			return sb.ToString();
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Submit.Click += new EventHandler(this.BtnLogin_Click);
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion

	}
}
