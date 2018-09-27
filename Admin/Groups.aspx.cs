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
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class Admin_Groups : G
	{

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("login.aspx");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					Btn_FwwAll.ToolTip =Root.rm.GetString("Agrtxt1");
					Btn_Fww.ToolTip =Root.rm.GetString("Agrtxt2");
					Btn_Rww.ToolTip =Root.rm.GetString("Agrtxt3");
					Btn_RwwAll.ToolTip =Root.rm.GetString("Agrtxt4");

					Groups_Grid.Columns[0].HeaderText =Root.rm.GetString("Agrtxt7");
					Groups_Grid.Columns[1].HeaderText =Root.rm.GetString("Agrtxt8");
					Submit.Text =Root.rm.GetString("Agrtxt10");

					FillGrid();
					FillListGroups();
					ViewState["IDGroup"] = "-1";

					Groups_Grid.Visible = true;
					NewBtn.Visible = true;
					NewBtn.Text =Root.rm.GetString("Agrtxt12");
					Groups_Table.Visible = false;
				}

			}
		}

		private void FillListGroups()
		{
			ListGroups.Items.Clear();
			ListGroups.DataTextField = "Description";
			ListGroups.DataValueField = "id";
			ListGroups.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS ORDER BY ID");
			ListGroups.DataBind();
		}

		private void FillGrid()
		{
			Groups_Grid.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS ORDER BY ID");
			Groups_Grid.DataBind();
		}

		private void FillTable(string id)
		{
			ViewState["IDGroup"] = id;
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS WHERE ID=" + id);
			DataRow dr = ds.Tables[0].Rows[0];
			GroupText.Text = dr["Description"].ToString();
			string[] arryD = dr["Dependency"].ToString().Split('|');
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

		public void NewBtnClick(Object sender, EventArgs e)
		{
			Groups_Table.Visible = true;
			GroupText.Text = String.Empty;
			NewBtn.Visible = false;
			Groups_Grid.Visible = false;

		}

		public void Submit_Click(Object sender, EventArgs e)
		{
			using (DigiDapter dg = new DigiDapter())
			{


				dg.Add("DESCRIPTION", GroupText.Text);
				string dep = "|";
				foreach (ListItem im in ListDip.Items)
				{
					dep += im.Value.ToString() + "|";
				}
				if (dep.Length < 2) dep = "|" + UC.AdminGroupId.ToString() + "|";
				if (dep.IndexOf("|" + UC.AdminGroupId.ToString() + "|") < 0) dep = "|" + UC.AdminGroupId.ToString() + dep;
				dg.Add("DEPENDENCY", dep);
				dg.Execute("GROUPS", "id=" + int.Parse(ViewState["IDGroup"].ToString()));
			}
			ViewState["IDGroup"] = "-1";
			GroupText.Text = String.Empty;
			ListDip.Items.Clear();
			FillGrid();
			FillListGroups();
			Groups_Grid.Visible = true;
			Groups_Table.Visible = false;

		}

		public void CategoryGridItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Edit":
					FillTable(((Literal) e.Item.FindControl("IDCat")).Text);
					FillGrid();
					FillListGroups();
					Groups_Grid.Visible = false;
					NewBtn.Visible = false;
					Groups_Table.Visible = true;
					break;
				case "Delete":
					DatabaseConnection.DoCommand(String.Format("DELETE FROM GROUPS WHERE ID ={0}", int.Parse(((Literal) e.Item.FindControl("IDCat")).Text)));
					Groups_Grid.EditItemIndex = -1;
					FillGrid();
					FillListGroups();
					break;
			}
		}

		public void Groups_Grid_ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Trace.Warn("", ((Literal) e.Item.Cells[0].FindControl("IDCat")).Text);
					Literal LtrDip;
					string id = ((Literal) e.Item.Cells[0].FindControl("IDCat")).Text;
					DataSet ds;
					ds = DatabaseConnection.CreateDataset("SELECT * FROM GROUPS WHERE ID=" + id);
					DataRow dr = ds.Tables[0].Rows[0];
					string[] arryD = dr["Dependency"].ToString().Split('|');
					dr.Delete();
					ds.Clear();
					string query = String.Empty;
					foreach (string ut in arryD)
					{
						if (ut.Length > 0) query += "ID=" + ut + " OR ";
					}
					query = query.Substring(0, query.Length - 3);
					ds = DatabaseConnection.CreateDataset("SELECT DESCRIPTION FROM GROUPS WHERE " + query);
					string result = String.Empty;
					foreach (DataRow dr1 in ds.Tables[0].Rows)
					{
						result += dr1["Description"] + ", ";
					}
					LtrDip = (Literal) e.Item.Cells[1].FindControl("LtrDip");
					if (result.Length > 2)
					{
						result = result.Substring(0, result.Length - 2);
					}
					LtrDip.Text = result;

					LinkButton DeleteButton = (LinkButton) e.Item.Cells[2].FindControl("LnkDel");
					DeleteButton.Text =Root.rm.GetString("Delete");
					DeleteButton.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Agrtxt6") + "');");
					LinkButton EditButton = (LinkButton) e.Item.Cells[2].FindControl("LnkEdt");
					EditButton.Text =Root.rm.GetString("Modify");
					if (result.Length < 1)
					{
						DeleteButton.Visible = false;
						EditButton.Visible = false;
					}
					break;
			}
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
		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.NewBtn.Click += new EventHandler(this.NewBtnClick);
			this.Btn_FwwAll.Click += new EventHandler(this.Btn_FwwAll_Click);
			this.Btn_Fww.Click += new EventHandler(this.Btn_Fww_Click);
			this.Btn_Rww.Click += new EventHandler(this.Btn_Rww_Click);
			this.Btn_RwwAll.Click += new EventHandler(this.Btn_RwwAll_Click);
			this.Submit.Click += new EventHandler(this.Submit_Click);
		}

		#endregion


		public void Btn_RwwAll_Click(Object sender, EventArgs e)
		{
			foreach (ListItem li in ListDip.Items)
			{
				li.Selected = true;
			}
			Remove_ListBox(ListDip);
		}
	}
}
