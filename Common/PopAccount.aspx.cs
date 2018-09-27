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

namespace Digita.Tustena
{
	public partial class PopAccount : G
	{

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Find.Click += new EventHandler(this.FindClick);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				string js;

				string control = Request.QueryString["textbox"].ToString();
				string control2 = Request.QueryString["textbox2"].ToString();

				string clickControl=null;
				string eventFunction=null;
				if(Request.QueryString["click"]!=null)
					clickControl = Request.QueryString["click"].ToString();
				if(Request.QueryString["event"]!=null)
					eventFunction = Request.QueryString["event"].ToString();

				js = "<script>";
				js += "function SetRef(id,tx){";
				if (Request.QueryString["frame"] != null)
				{
					js += "dynaret.SetParams('" + control + "',tx);";
					js += "dynaret.SetParams('" + control2 + "',id);";
				}
				else
				{
					js += "dynaret('" + control + "').value=tx;";
					js += "dynaret('" + control2 + "').value=id;";
				}
				js += "self.close();";
				if(clickControl!=null)
					js += "clickElement(dynaret('" + clickControl + "'));"+ Environment.NewLine;
				if(eventFunction!=null)
					js += "dynaevent('"+eventFunction+"');"+ Environment.NewLine;
				js += "parent.HideBox();}";
				js += "</script>";
				SomeJS.Text = js;
				Find.Text =Root.rm.GetString("Prftxt5");
				if (!Page.IsPostBack)
				{
					if(Request.QueryString["sales"]!=null)
						ViewState["sales"]=Request.QueryString["sales"];
					else
						ViewState["sales"]=0;


					if(Request.QueryString["Impersonate"]!=null)
					{
						ContactReferrer.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE ACTIVE=1 AND DIARYACCOUNT LIKE '|%" + UC.UserId + "|%'");
					}
					else
					{
						string query = G.GroupDependency(UC.UserGroupId);
						string qGroup = String.Empty;
						if (query.Length > 1)
						{
							string[] arryD = query.Split('|');
							foreach (string ut in arryD)
							{
								if (ut.Length > 0) qGroup += "GROUPID=" + ut + " OR ";
							}
							if (qGroup.Length > 0) qGroup = qGroup.Substring(0, qGroup.Length - 3);
						}else
							qGroup = "GROUPID=" + UC.UserGroupId;
						if(ViewState["sales"].ToString()=="0")
							ContactReferrer.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE ACTIVE=1 AND (" + qGroup + ")");
						else
							ContactReferrer.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE ACCESSLEVEL="+ViewState["sales"]+" AND ACTIVE=1 AND (" + qGroup + ")");

					}
					ContactReferrer.DataBind();
				}
			}
		}

		public void FindClick(object sender, EventArgs e)
		{
			if(Request.QueryString["Impersonate"]!=null)
			{
				ContactReferrer.DataSource = DatabaseConnection.CreateDataset("sELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE DIARYACCOUNT LIKE '|%" + UC.UserId + "|%') AND (NAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR SURNAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%')");
			}
			else
			{
				if(ViewState["sales"].ToString()=="0")
					ContactReferrer.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE (NAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR SURNAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%')");
				else
					ContactReferrer.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE ACCESSLEVEL="+ViewState["sales"]+" AND (NAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR SURNAME LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%')");
			}
			ContactReferrer.DataBind();
		}


	}
}
