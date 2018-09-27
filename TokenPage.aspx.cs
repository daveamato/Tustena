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
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class TokenPage : G
	{
		const int tokenInterval = 30;
		protected void Page_Load(object sender, EventArgs e)
		{
			try
			{


				if(Session["UserConfig"]!=null)
				{
					DatabaseConnection.DoCommand("DELETE FROM TOKENS WHERE DATEDIFF(N,EXPIRE, GETDATE())>"+tokenInterval);
					transfer(Request.QueryString["Page"]);
				}
				else
				{
					if(Request.QueryString["token"]!=null)
					{
						Guid guid = new Guid(Request.QueryString["token"]);
						DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM TOKENS WHERE TOKEN='"+guid.ToString()+"' AND DATEDIFF(N,EXPIRE, GETDATE())<"+tokenInterval).Tables[0];
						if(dt.Rows.Count>0)
						{
							string usr = dt.Rows[0]["UserName"].ToString();
							string pwd = dt.Rows[0]["pass"].ToString();
							DatabaseConnection.DoCommand(String.Format("DELETE FROM TOKENS WHERE TOKEN='{0}'",DatabaseConnection.FilterInjection(Request.QueryString["token"])));
							UserConfig UC;
							UC = UserData.LoadPersonalData(usr,pwd,-1);
							DatabaseConnection.DoCommand(String.Format("INSERT INTO LOGINLOG (USERID) VALUES ({0})" , UC.UserId ));
							Session["UserConfig"]=UC;
							DatabaseConnection.DoCommand("DELETE FROM TOKENS WHERE DATEDIFF(N,EXPIRE, GETDATE())>"+tokenInterval);
                            Cache.Remove(UC.UserId.ToString());
							transfer(Request.QueryString["Page"]);
						}
						else
						{
							DatabaseConnection.DoCommand("DELETE FROM TOKENS WHERE DATEDIFF(N,EXPIRE, GETDATE())>"+tokenInterval);
							Info.Text="Token not present or expire.";
						}
					}
					else
						Info.Text="Token not present";
				}
			}
			catch
			{
				Info.Text="Authentication error";
			}
		}

		private void transfer(string p)
		{
			switch(p)
			{
				case "1":
					Response.Redirect("/Calendar/agenda.aspx?m=25&si=2");
					break;
				case "2":
					Response.Redirect("/CRM/crm_companies.aspx?m=25&dgb=1&si=29");
					break;
				case "3":
					Response.Redirect("/CRM/base_contacts.aspx?m=25&dgb=1&si=31");
					break;
				case "4":
					Response.Redirect("/CRM/CRM_Lead.aspx?m=25&dgb=1&si=53");
					break;
				case "5":
					Response.Redirect("/WorkingCRM/AllActivity.aspx?m=25&dgb=1&si=38");
					break;
				default:
					Response.Redirect("/today.aspx?m=25&dgb=1");
					break;
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
