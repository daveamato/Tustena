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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class SelectOffice : UserControl
	{

		private UserConfig UC;

		public string controlId;
		public bool clearUserList;

		public bool ClearUserList
		{
			get{return clearUserList;}
			set{clearUserList=value;}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.Session.IsNewSession)
				HttpContext.Current.Response.Redirect("/login.aspx");


			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			controlId = this.ID;



			SearchUserSubmit.Attributes.Add("ondownloadready", "DownloadElement = this;GroupPostBack = false;");
			SearchUserSubmit.Attributes.Add("ondownloadnotsupported", "GroupPostBack = true;");
			SearchUserSubmit.Attributes.Add("onclick", "checkMode('GetXmlAccount.aspx?c=' + document.getElementById('" + controlId + "_SearchUser').value,'" + controlId + "OfficeUsers',this);return false;");


			if (!Page.IsPostBack)
			{
				FillOffices();
				OfficeUsers.Items.Clear();
				if (SelectedUsersected.Items.Count <= 0)
				{
					SelectedUsersected.DataTextField = "UserName";
					SelectedUsersected.DataValueField = "uid";
					try
					{
						SelectedUsersected.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE UID=" + UC.UserId);
					}
					catch
					{
						SelectedUsersected.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE UID=1");
					}
					SelectedUsersected.DataBind();
				}
				if(ClearUserList)
					this.CleanUsers();
			}
			else
			{
				if (GroupValue.Value.Length > 0)
				{
					SetAccount(GroupValue.Value);
				}
			}
		}

		private void FillOffices()
		{
			Offices.DataTextField = "Office";
			Offices.DataValueField = "id";
			try
			{
				Offices.DataSource = DatabaseConnection.CreateDataset("SELECT ID, OFFICE FROM OFFICES ORDER BY OFFICE ASC");
			}
			catch
			{
				Offices.DataSource = DatabaseConnection.CreateDataset("SELECT ID, OFFICE FROM OFFICES ORDER BY OFFICE ASC");
			}
			Offices.DataBind();
			Offices.Items.Insert(0, Root.rm.GetString("Meettxt57"));
			Offices.SelectedIndex = 0;
		}

		public void Offices_SelectedIndexChanged(Object sender, EventArgs e)
		{
			if (((DropDownList) sender).SelectedIndex != 0)
			{
				OfficeUsers.DataTextField = "descrizione";
				OfficeUsers.DataValueField = "uid";
				string sqlString = "SELECT ACCOUNT.UID, (ACCOUNT.SURNAME+' '+ACCOUNT.NAME+' ('+OFFICES.OFFICE+')') AS DESCRIZIONE ";
				sqlString += "FROM ACCOUNT INNER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE ACCOUNT.OFFICEID=" + int.Parse(Offices.SelectedItem.Value) + " ORDER BY SURNAME,NAME ASC";
				OfficeUsers.DataSource = DatabaseConnection.CreateDataset(sqlString);
				OfficeUsers.DataBind();
			}
			else
			{
				OfficeUsers.Items.Clear();
			}
		}

		public void searchutesubmit_click(Object sender, EventArgs e)
		{
			StringBuilder sqlString = new StringBuilder("SELECT ACCOUNT.UID, (ISNULL(ACCOUNT.SURNAME,'')+' '+ISNULL(ACCOUNT.NAME,'')+' ('+ISNULL(OFFICES.OFFICE,'')+')') AS DESCRIZIONE ");
			sqlString.AppendFormat("FROM ACCOUNT LEFT OUTER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE (ACCOUNT.NAME LIKE '%{0}%' ",SearchUser.Text);
			sqlString.AppendFormat("OR ACCOUNT.SURNAME LIKE '%{0}%') ORDER BY SURNAME,NAME ASC",SearchUser.Text);

			OfficeUsers.DataTextField = "descrizione";
			OfficeUsers.DataValueField = "uid";
			OfficeUsers.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
			OfficeUsers.DataBind();

			SetAccount(GroupValue.Value);
		}

		public string GetValue
		{
			get { return GroupValue.Value; }
		}

		public string[] GetValueArray
		{
			get
			{
				return GetValue.Trim('|').Split('|');
			}
		}

		public int GetSelectedCount
		{
			get { return  SelectedUsersected.Items.Count;}
		}

		public void SetSelected(string value)
		{
			foreach(ListItem li in SelectedUsersected.Items)
			{
				if(li.Value==value)
				{
					li.Selected=true;
				}
			}
		}

		public void SetAccount(string value)
		{
			string query = String.Empty;
			GroupValue.Value = value;
			try
			{
				string[] arryD = value.ToString().Split('|');
				foreach (string ut in arryD)
				{
					if (ut.Length > 0) query += "ACCOUNT.UID=" + ut + " OR ";
				}
				query = query.Substring(0, query.Length - 3);

				SelectedUsersected.Items.Clear();
				if (SelectedUsersected.Items.Count <= 0)
				{
					SelectedUsersected.DataTextField = "descrizione";
					SelectedUsersected.DataValueField = "uid";
					string sqlString = "SELECT ACCOUNT.UID, (ACCOUNT.SURNAME+' '+ACCOUNT.NAME+' ('+OFFICES.OFFICE+')') AS DESCRIZIONE ";
					sqlString += "FROM ACCOUNT INNER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE (" + query + ") ORDER BY SURNAME,NAME ASC";
					SelectedUsersected.DataSource = DatabaseConnection.CreateDataset(sqlString);
					SelectedUsersected.DataBind();

					foreach(ListItem liSelected in this.SelectedUsersected.Items)
					{
						foreach(ListItem liUser in this.OfficeUsers.Items)
						{
							if(liSelected.Value==liUser.Value)
							{
								OfficeUsers.Items.Remove(liUser);
								break;
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		private void InitializeComponent()
		{

		}

		public void ResetOffice()
		{
			SelectedUsersected.DataTextField = "UserName";
			SelectedUsersected.DataValueField = "uid";
			SelectedUsersected.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE UID=" + UC.UserId);
			SelectedUsersected.DataBind();
		}

		public void CleanUsers()
		{
			SelectedUsersected.Items.Clear();
			OfficeUsers.Items.Clear();
		}
	}


}
