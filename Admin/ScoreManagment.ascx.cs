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
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Admin
{
	public partial class ScoreManagment1 : UserControl
	{
		private UserConfig UC = new UserConfig();
		protected LocalizedLiteral Localizedliteral1;
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];
			if(!Page.IsPostBack)
			{
				submit.Text=Root.rm.GetString("Save");
				RefreshTable();
			}
		}

		private void RefreshTable()
		{
			scorerepeater.DataSource=DatabaseConnection.CreateDataset("SELECT * FROM SCOREDESCRIPTION");
			scorerepeater.DataBind();
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.scorerepeater.ItemDataBound += new RepeaterItemEventHandler(this.scorerepeater_ItemDataBound);
			this.scorerepeater.ItemCommand += new RepeaterCommandEventHandler(this.scorerepeater_ItemCommand);
			this.submit.Click += new EventHandler(this.submit_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private byte ScorePercent=0;
		private void scorerepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					TextBox ScoreDescription = (TextBox)e.Item.FindControl("ScoreDescription");
					TextBox ScoreWeight = (TextBox)e.Item.FindControl("ScoreWeight");
					Label ScoreID = (Label)e.Item.FindControl("ScoreID");
					ScoreID.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "id").ToString();
					ScoreDescription.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "Description").ToString();
					ScoreWeight.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "Weight").ToString();
					ScorePercent+=Convert.ToByte(ScoreWeight.Text);
					break;
				case ListItemType.Footer:
					TextBox ScoreTotal = (TextBox)e.Item.FindControl("ScoreTotal");
					ScoreTotal.Text=ScorePercent.ToString();
					LinkButton AddItem = (LinkButton)e.Item.FindControl("AddItem");
					AddItem.Text=Root.rm.GetString("MakeNew");
					break;
			}
		}

		private void submit_Click(object sender, EventArgs e)
		{
			foreach(RepeaterItem  ri in scorerepeater.Items)
			{
				Label ScoreID = (Label)ri.FindControl("ScoreID");
				TextBox ScoreDescription = (TextBox)ri.FindControl("ScoreDescription");
				TextBox ScoreWeight = (TextBox)ri.FindControl("ScoreWeight");
				byte w = 0;
				try
				{
					w = byte.Parse(ScoreWeight.Text);
				}
				catch
				{
					w=0;
				}
				DatabaseConnection.DoCommand(string.Format("UPDATE SCOREDESCRIPTION SET DESCRIPTION='{0}', WEIGHT={1} WHERE ID={2}",DatabaseConnection.FilterInjection(ScoreDescription.Text),w,ScoreID.Text));
			}
			RefreshTable();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Scotxt3") + "')</script>");
		}

		private void scorerepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "AddItem":
							string AddDescription = ((TextBox)e.Item.FindControl("AddDescription")).Text;
							string AddWeight = ((TextBox)e.Item.FindControl("AddWeight")).Text;
							byte w = 0;
							try
							{
								w = byte.Parse(AddWeight);
							}catch
							{
								w=0;
							}
							DatabaseConnection.DoCommand(string.Format("INSERT INTO SCOREDESCRIPTION (DESCRIPTION,WEIGHT) VALUES ('{0}',{1})",DatabaseConnection.FilterInjection(AddDescription),w));

					break;
				case "DeleteItems":
					foreach(RepeaterItem  ri in scorerepeater.Items)
					{
						CheckBox todelete = (CheckBox)ri.FindControl("todelete");
						if(todelete.Checked)
						{
							DatabaseConnection.DoCommand("DELETE FROM SCOREDESCRIPTION WHERE ID="+((Label)ri.FindControl("ScoreID")).Text);
						}
					}

					break;
			}
			RefreshTable();
		}
	}
}
