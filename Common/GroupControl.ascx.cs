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
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class GroupControl : UserControl
	{
		public UserConfig UC = new UserConfig();

		public void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.Session.IsNewSession)
				HttpContext.Current.Response.Redirect("/login.aspx");

			Page.ClientScript.RegisterStartupScript(this.GetType(), "",String.Format("<script>var mygroup={0};</script>",UC.UserGroupId));


		}

		public void Transfer_Listbox(ListBox fromListBox, ListBox toListBox)
		{
			bool exists;
			bool done = false;
			foreach (ListItem li in fromListBox.Items)
			{
				exists = false;
				if (li.Selected)
				{
					foreach (ListItem li1 in toListBox.Items)
					{
						if (li.Value == li1.Value)
						{
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						toListBox.Items.Add(li);
						done = true;
					}
				}
			}
			if (done) toListBox.SelectedItem.Selected = false;
		}

		public void Remove_ListBox(ListBox fromListBox)
		{
			ListBox MyLB = new ListBox();
			foreach (ListItem li in fromListBox.Items)
			{
				if (li.Selected)
				{
					MyLB.Items.Add(li);
				}
			}
			foreach (ListItem li in MyLB.Items)
			{
				fromListBox.Items.Remove(li);
			}
		}

		public void Btn_Fww_Click(Object sender, EventArgs e)
		{
			if (ListGroups.SelectedIndex > -1)
			{
				Transfer_Listbox(ListGroups, ListDip);
			}
		}

		public void Btn_FwwAll_Click(Object sender, EventArgs e)
		{
			foreach (ListItem li in ListGroups.Items)
			{
				li.Selected = true;
			}
			Transfer_Listbox(ListGroups, ListDip);
		}

		public void Btn_Rww_Click(Object sender, EventArgs e)
		{
			if (ListDip.SelectedIndex > -1)
			{
				Remove_ListBox(ListDip);

			}
		}

		public void Btn_RwwAll_Click(Object sender, EventArgs e)
		{
			foreach (ListItem li in ListDip.Items)
			{
				li.Selected = true;
			}
			Remove_ListBox(ListDip);
		}

		public string GetValue
		{
			get
			{
				return GroupValue.Value;
			}
			set
			{
			}
		}

		public void SetGroups(string value)
		{

			string[] arryD = value.ToString().Split('|');
			string query = String.Empty;
			ListGroups.Items.Clear();
			if (ListGroups.Items.Count <= 0)
			{
				ListGroups.DataTextField = "Description";
				ListGroups.DataValueField = "id";
				ListGroups.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS ORDER BY DESCRIPTION;");
				ListGroups.DataBind();
			}


			foreach (string ut in arryD)
			{
				if (ut.Length > 0) query += "ID=" + ut + " OR ";
			}
			if(query.Length>6)
				query = "AND ("+query.Substring(0, query.Length - 4)+")";
string gquery;
            if(query.Length>0)
                gquery = "SELECT * FROM GROUPS WHERE " + query.Substring(4) + " ORDER BY DESCRIPTION;";
            else
                gquery = "SELECT * FROM GROUPS ORDER BY DESCRIPTION;";

			DataSet ds = DatabaseConnection.CreateDataset(gquery);

			if (ds.Tables[0].Rows.Count > 0)
			{
				ListDip.Items.Clear();
				foreach (DataRow dr in ds.Tables[0].Rows)
				{
					ListItem li = new ListItem();
					li.Value = dr["Id"].ToString();
					li.Text = dr["Description"].ToString();
					ListDip.Items.Add(li);
				}
				ListItem liglobal = new ListItem();
				liglobal.Value = "0";
				liglobal.Text = "Global";
				ListDip.Items.Add(liglobal);

				if (ListDip.Items.Count > 0)
				{
					foreach (ListItem lg in ListGroups.Items)
					{
						foreach (ListItem ld in ListDip.Items)
						{
							if (ld.Value == lg.Value) lg.Selected = true;
						}
					}
					Remove_ListBox(ListGroups);
				}
			}
			else
			{
				foreach (ListItem li in ListGroups.Items)
				{
					if (li.Value == UC.UserGroupId.ToString())
					{
						li.Selected = true;
						Transfer_Listbox(ListGroups, ListDip);
						break;
					}
				}
			}
			string dep = "|";
			foreach (ListItem im in ListDip.Items)
			{
				dep += im.Value.ToString() + "|";
			}
			if (dep.Length < 2) dep = "|" + UC.UserGroupId.ToString() + "|";
			if (dep.IndexOf("|" + UC.AdminGroupId + "|") < 0) dep = "|" + UC.AdminGroupId + dep;
			GroupValue.Value = dep;

		}

		public GroupControl()
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
		}

		public void ResetGroups()
		{
			ListGroups.Items.Clear();
			ListGroups.DataTextField = "Description";
			ListGroups.DataValueField = "id";
			ListGroups.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM GROUPS WHERE ID<>{0} ORDER BY DESCRIPTION;",UC.UserGroupId));
			ListGroups.DataBind();
		}
	}
}
