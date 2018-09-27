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
using Microsoft.Web.UI.WebControls;
using TreeView = Microsoft.Web.UI.WebControls.TreeView;
using TreeNode = Microsoft.Web.UI.WebControls.TreeNode;

namespace Digita.Tustena.Catalog
{
	public partial class CatalogCategories : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					AddCategories.Text =Root.rm.GetString("Cattxt2");
					ModCategories.Text =Root.rm.GetString("Cattxt6");
					DelCategories.Text =Root.rm.GetString("Cattxt10");
					CreaTree(tvCategoryTree, 0);
					DropParent.DataTextField = "Description";
					DropParent.DataValueField = "id";
					DropParent.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CATALOGCATEGORIES ORDER BY PARENTID").Tables[0];
					DropParent.DataBind();
					DropParent.Items.Insert(0, new ListItem(Root.rm.GetString("Cattxt5"), "0"));
				}

			}
		}

		public void CreaTree(TreeView tree, int open)
		{
string queryCat;
queryCat = "SELECT * FROM CatalogCategories WHERE ParentID = 0";
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);

			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
				string del = "0";
				if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGCATEGORIES WHERE PARENTID=" + dr["Id"].ToString())) > 0)
					del = "1";

				else if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGPRODUCTS WHERE CATEGORY=" + dr["Id"].ToString())) > 0)
					del = "1";


				tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "','" + dr["Description"].ToString() + "','" + dr["ParentID"].ToString() + "'," + del + ",'"+((dr["EMAILOWNER"]!=DBNull.Value)?dr["EMAILOWNER"].ToString():string.Empty)+"')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				tv.NodeData = dr["Id"].ToString();

				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open); // Chiamata ricorsiva per fare le foglie

				tree.Nodes.Add(tv);
			}
			tree.CssClass = "normal";

		}

		public bool FillCategoryTree(int parent, TreeNode tvUp, int open)
		{
string queryCat;
queryCat = "SELECT * FROM CATALOGCATEGORIES WHERE PARENTID = " + parent;
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			bool toExpand = false;
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
				string del = "0";
				if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGCATEGORIES WHERE PARENTID=" + dr["Id"].ToString())) > 0)
					del = "1";

				else if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGPRODUCTS WHERE CATEGORY=" + dr["Id"].ToString())) > 0)
					del = "1";

                tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "','" + dr["Description"].ToString() + "','" + dr["ParentID"].ToString() + "'," + del + ",'" + ((dr["EMAILOWNER"] != DBNull.Value) ? dr["EMAILOWNER"].ToString() : string.Empty) + "')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				tv.NodeData = dr["Id"].ToString();
				if (!toExpand)
					toExpand = (open == Convert.ToInt32(dr["id"]));
				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open); // Chiamata ricorsiva per fare le foglie
				if (tv.Expanded) toExpand = true;
				tvUp.Nodes.Add(tv);
			}
			return toExpand;
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
			this.AddCategories.Click += new EventHandler(AddCategories_Click);
			this.ModCategories.Click += new EventHandler(ModCategories_Click);
			this.DelCategories.Click += new EventHandler(DelCategories_Click);
		}

		#endregion

		private void AddCategories_Click(object sender, EventArgs e)
		{
			if (! ExistCategory(TxtTextCategory.Text)) // Inserisco se la categoria non esiste
			{
				int idCat = InsertRenameCategory(TxtTextCategory.Text, Convert.ToInt32(DropParent.SelectedValue), -1, TxtEmailOwner.Text);

				tvCategoryTree.Nodes.Clear();
				CreaTree(tvCategoryTree, idCat); // Ricostruisco l'albero aggiornato
			}
			else
				LblMessage.Text =Root.rm.GetString("Cattxt7"); // La categoria esiste gi

		}

		private void ModCategories_Click(object sender, EventArgs e)
		{
			if (TxtTextCategory.Text.Length > 0)
			{
				if(TxtIdCategory.Text.Length<1 || (!StaticFunctions.IsNumber(TxtIdCategory.Text)))
					TxtIdCategory.Text="-1";
				int idCat = InsertRenameCategory(TxtTextCategory.Text, Convert.ToInt32(DropParent.SelectedValue), Convert.ToInt32(TxtIdCategory.Text), TxtEmailOwner.Text);

				tvCategoryTree.Nodes.Clear();
				CreaTree(tvCategoryTree, idCat); // Ricostruisco l'albero aggiornato
			}
			else
				LblMessage.Text =Root.rm.GetString("Cattxt9"); // niente da cancellare

		}

		public bool ExistCategory(string category)
		{
			string queryCat = "SELECT DESCRIPTION FROM CATALOGCATEGORIES WHERE (DESCRIPTION = '" + DatabaseConnection.FilterInjection(category) + "')";
			DataSet dsCat = DatabaseConnection.CreateDataset(queryCat);

			if (dsCat.Tables[0].Rows.Count > 0)
				return true; // Il nome della categoria esiste gi
			else
				return false; // Il nome della categoria non esiste ancora
		}

		private int InsertRenameCategory(string newCategory, int parent, int id, string mail)
		{
			if ((newCategory != null && newCategory.Length != 0))
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("DESCRIPTION", newCategory);
					dg.Add("PARENTID", parent);
                    dg.Add("EMAILOWNER", ((mail.Length>0)?mail:string.Empty));

					object newId = dg.Execute("CATALOGCATEGORIES", "id=" + id, DigiDapter.Identities.Identity);
					if (dg.RecordInserted)
						return Convert.ToInt32(newId.ToString());
					else
						return id;
				}
			}
			else
			{
				LblMessage.Text =Root.rm.GetString("Cattxt9"); // Manca il TextFont
				return -1;
			}
		}

		private void DelCategories_Click(object sender, EventArgs e)
		{
			if (TxtIdCategory.Text.Length > 0)
			{
				DatabaseConnection.DoCommand("DELETE FROM CATALOGCATEGORIES WHERE ID=" + int.Parse(TxtIdCategory.Text));
				TxtIdCategory.Text = String.Empty;
				TxtTextCategory.Text = String.Empty;
				tvCategoryTree.Nodes.Clear();
				CreaTree(tvCategoryTree, 0);
			}
			else
				LblMessage.Text =Root.rm.GetString("Cattxt9"); // niente da cancellare
		}
	}
}
