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
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Admin
{
	public partial class offices : UserControl
	{
		public G G = new G();
		private UserConfig UC = new UserConfig();
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion


		public void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];

			if (!Page.IsPostBack)
			{
				Categorie_Grid.Columns[0].HeaderText =Root.rm.GetString("Aoftxt1");

                HelpLabel.Text = G.FillHelp("HelpOffices", UC);
				FillGrid();
			}
		}

		private void FillGrid()
		{
			Categorie_Grid.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM OFFICES ORDER BY OFFICE");
			Categorie_Grid.DataBind();
		}


		public void Categorie_Grid_DataBound(object source, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer)
				((LinkButton) e.Item.FindControl("AddOffice")).Text =Root.rm.GetString("Aoftxt2");

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				((LinkButton) e.Item.FindControl("LnkDel")).Text =Root.rm.GetString("Delete");

				try
				{
					LinkButton lb = (LinkButton) (e.Item.Cells[1].Controls[0]);
					lb.Text =Root.rm.GetString("Rename");
				}
				catch (Exception)
				{
				}
			}
			if (e.Item.ItemType == ListItemType.EditItem)
			{
				((LinkButton) e.Item.FindControl("LnkDel")).Text =Root.rm.GetString("Delete");
				try
				{
					LinkButton lb = (LinkButton) (e.Item.Cells[1].Controls[0]);
					lb.Text =Root.rm.GetString("Save");
					LinkButton lb1 = (LinkButton) (e.Item.Cells[1].Controls[2]);
					lb1.Text =Root.rm.GetString("Cancel");
				}
				catch (Exception)
				{
				}
			}
		}

		public void CategoryGridItemCommand(object source, DataGridCommandEventArgs e)
		{
			string sqlString;
			Trace.Warn(e.CommandName);
			switch (e.CommandName)
			{
				case "Edit":
					Categorie_Grid.EditItemIndex = e.Item.ItemIndex;
					FillGrid();
					break;
				case "Cancel":
					Categorie_Grid.EditItemIndex = -1;
					FillGrid();
					break;
				case "Update":
					int CatId=int.Parse(((Literal) e.Item.FindControl("IDCat")).Text);
					sqlString = "SELECT * FROM OFFICES WHERE OFFICE ='" + CatId + "';";
					Trace.Warn("", sqlString);
					using (DigiDapter dg = new DigiDapter())
					{
						dg.UpdateOnly();
						dg.Add("OFFICE", ((TextBox) e.Item.FindControl("LnkNameTextBox")).Text);
						dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
						dg.Add("LASTMODIFIEDBYID", UC.UserId);
						dg.Execute("OFFICES", "ID=" + int.Parse(((Literal) e.Item.FindControl("IDCat")).Text));
					}

					Categorie_Grid.EditItemIndex = -1;
					FillGrid();
					break;
				case "Delete":
					sqlString = "dELETE FROM OFFICES WHERE ID ='" + int.Parse(((Literal) e.Item.FindControl("IDCat")).Text) + "';";
					DatabaseConnection.DoCommand(sqlString);
					Categorie_Grid.EditItemIndex = -1;
					FillGrid();
					break;
				case "Insert":
					string newOffice = ((TextBox) e.Item.FindControl("TxtNewCatName")).Text;
					if (newOffice.Length == 0)
						return;
                    if (Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("SELECT COUNT(*) FROM OFFICES WHERE OFFICE='{0}'",DatabaseConnection.FilterInjection(newOffice))))>0)
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "officeduplicate", "<script>alert('" + Root.rm.GetString("Duplicate") + "');</script>");
                        return;
                    }
                    {
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("OFFICE", newOffice);
							dg.Add("CREATEDBYID", UC.UserId);
							dg.Execute("OFFICES");
						}
					}
					Categorie_Grid.EditItemIndex = -1;
					FillGrid();
					break;
			}
		}
	}
}
