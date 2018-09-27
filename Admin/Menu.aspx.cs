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
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class Admin_Menu : G
	{
		private DataSet ds = new DataSet();
		private DataSet ds1 = new DataSet();



			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
				this.Load += new EventHandler(this.Page_Load);
			this.Btn_FwwAll.Click += new EventHandler(this.Btn_FwwAll_Click);
			this.Btn_Fww.Click += new EventHandler(this.Btn_Fww_Click);
			this.Btn_Rww.Click += new EventHandler(this.Btn_Rww_Click);
			this.Btn_RwwAll.Click += new EventHandler(this.Btn_RwwAll_Click);
			this.Submit.Click += new EventHandler(this.Submit_Click);

			}
		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					FillGrid();

					ListGroups.DataTextField = "Description";
					ListGroups.DataValueField = "id";
					ListGroups.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS ORDER BY ID;");
					ListGroups.DataBind();
					ViewState["IDGroup"] = "-1";
					HelpLabel.Text = FillHelp("HelpMenu");

					Submit.Text =Root.rm.GetString("Amntxt2");
				}
			}
		}

		private void FillGrid()
		{
			if (mode == "0" && !UC.DebugMode)
			{
				ds = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE MENUTITLE=1 AND ACCESSGROUP<>'|0|' AND MODE=0 AND MODE=0 AND ("+(int)UC.Modules+" & MODULES)=MODULES ORDER BY SORTORDER");
				ds1 = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE MENUTITLE<>1 AND ACCESSGROUP<>'|0|' AND MODE=0 AND ("+(int)UC.Modules+" & MODULES)=MODULES ORDER BY SORTORDER");
            }
			else
			{
                ds = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE MENUTITLE=1 AND ACCESSGROUP<>'|0|' AND (" + (int)UC.Modules + " & MODULES)=MODULES ORDER BY SORTORDER");
				ds1 = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE MENUTITLE<>1 AND ACCESSGROUP<>'|0|' AND ("+(int)UC.Modules+" & MODULES)=MODULES ORDER BY SORTORDER");
            }
			ds.Tables[0].TableName = "Menu";
			ds1.Tables[0].TableName = "SubMenu";

			Menu_Grid.DataSource = ds;
			Menu_Grid.DataBind();


		}

		protected DataView getSubMenuDataSource(int parent)
		{
			DataView SubMenu = ds1.Tables["SubMenu"].DefaultView;
			SubMenu.RowFilter = "ParentMenu=" + parent.ToString();
			return SubMenu;
		}

		public void Menu_Grid_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					e.Item.Cells[0].Text =Root.rm.GetString("Amntxt1");
					e.Item.Cells[1].Text =Root.rm.GetString("Amntxt2");
					e.Item.Cells[3].Text =Root.rm.GetString("Amntxt3");
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton LnkEdt = (LinkButton) e.Item.FindControl("LnkEdt");
					LnkEdt.Text =Root.rm.GetString("Amntxt8");
					Label NomeMenu = (Label) e.Item.FindControl("MenuName");
					NomeMenu.Text =Root.rm.GetString("Menutxt" + DataBinder.Eval((DataRowView) e.Item.DataItem, "rmvalue").ToString()).ToUpper();
					NomeMenu.CssClass = "menu";
					Literal LtrDip;
					int id = int.Parse(((Literal) e.Item.Cells[0].FindControl("IDMenu")).Text);
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE ID=" + id + " AND (" + (int)UC.Modules + " & MODULES)=MODULES");
DataRow dr = ds.Tables[0].Rows[0];
					string[] arryD = dr["accessgroup"].ToString().Split('|');
					dr.Delete();
					ds.Clear();
					string query = String.Empty;
					foreach (string ut in arryD)
					{
						query += "ID=" + ut + " OR ";
					}
					query = query.Substring(7, query.Length - 17);
					ds = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS WHERE " + query);
					string result = String.Empty;
					foreach (DataRow row in ds.Tables[0].Rows)
					{
						result += row["Description"] + ", ";
					}
					LtrDip = (Literal) e.Item.Cells[1].FindControl("LtrDip");
					if (result.Length > 2)
					{
						result = result.Substring(0, result.Length - 2);
					}
					LtrDip.Text = result;

					break;
			}
		}


		public void SubMenu_Grid_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					e.Item.Cells[0].Text =Root.rm.GetString("Amntxt1");
					e.Item.Cells[1].Text =Root.rm.GetString("Amntxt2");
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton LnkEdtSub = (LinkButton) e.Item.FindControl("LnkEdtSub");
					LnkEdtSub.Text =Root.rm.GetString("Amntxt8");
					Literal NameSubMenu = (Literal) e.Item.FindControl("NameSubMenu");
					NameSubMenu.Text =Root.rm.GetString("Menutxt" + DataBinder.Eval((DataRowView) e.Item.DataItem, "rmvalue").ToString());
					Literal LtrDip;
					int id = int.Parse(((Literal) e.Item.Cells[0].FindControl("IdSubMenu")).Text);
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE ID=" + id + " AND (" + (int)UC.Modules + " & MODULES)=MODULES");
 DataRow dr = ds.Tables[0].Rows[0];
					string[] arryD = dr["accessgroup"].ToString().Split('|');
					dr.Delete();
					ds.Clear();
					string query = String.Empty;
					foreach (string ut in arryD)
					{
						query += "ID=" + ut + " OR ";
					}
					query = query.Substring(7, query.Length - 17);
					ds = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS WHERE " + query);
					string result = String.Empty;
					foreach (DataRow DR1 in ds.Tables[0].Rows)
					{
						result += DR1["Description"] + ", ";
					}
					LtrDip = (Literal) e.Item.Cells[1].FindControl("LtrDipSub");
					if (result.Length > 2)
					{
						result = result.Substring(0, result.Length - 2);
					}
					LtrDip.Text = result;

					break;
			}
		}

		public void Menu_Grid_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			Trace.Warn("P", e.CommandName);
			switch (e.CommandName)
			{
				case "Edit":
					Groups_Table.Visible = true;
					Menu_Grid.Visible = false;
					FillTable(int.Parse(((Literal) e.Item.FindControl("IDMenu")).Text));
					FillGrid();
					break;
				case "SEdit":
					Groups_Table.Visible = true;
					Menu_Grid.Visible = false;
					FillTable(int.Parse(((Literal) e.Item.FindControl("IdSubMenu")).Text));
					FillGrid();
					break;
			}
		}


		private void FillTable(int id)
		{
			ViewState["IDMenu"] = id;
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE ID=" + id + " AND (" + (int)UC.Modules + " & MODULES)=MODULES");
 DataRow dr = ds.Tables[0].Rows[0];
			MenuText.Text =Root.rm.GetString("Menutxt" + dr["rmvalue"].ToString());
			string[] arryD = dr["accessgroup"].ToString().Split('|');
			dr.Delete();
			ds.Clear();
			string query = String.Empty;
			foreach (string ut in arryD)
			{
				query += "ID=" + ut + " OR ";
			}
			query = query.Substring(7, query.Length - 17);
			ListDip.DataTextField = "Description";
			ListDip.DataValueField = "id";
			ListDip.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS WHERE " + query);
			ListDip.DataBind();
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

		public void Submit_Click(Object sender, EventArgs e)
		{
string SQLString;
SQLString = "UPDATE COMPANYMENU SET ACCESSGROUP='{0}' WHERE MENUID =" + int.Parse(ViewState["IDMenu"].ToString());
			string dep = "|";
			foreach (ListItem im in ListDip.Items)
			{
				dep += im.Value.ToString() + "|";
			}
			if (dep.Length < 2) dep = "|" + UC.AdminGroupId.ToString() + "|";
			if (dep.IndexOf("|" + UC.AdminGroupId.ToString() + "|") < 0) dep = "|" + UC.AdminGroupId.ToString() + dep;
			DatabaseConnection.DoCommand(String.Format(SQLString,dep));
			ListDip.Items.Clear();
			Groups_Table.Visible = false;
			Menu_Grid.Visible = true;
			FillGrid();
		}


	}
}
