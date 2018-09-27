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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class CRMToDoList : G
	{

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				NewTask.Text =Root.rm.GetString("Tdltxt2");
				BtnSearch.Text =Root.rm.GetString("CRMopptxt6");
				SaveTask.Text =Root.rm.GetString("CRMTodtxt8");
				PopAccount.Visible = true;
				PopAccount2.Visible = true;
				PopAccount3.Visible = true;
				PopAccount4.Visible = true;

				if (!Page.IsPostBack)
				{
					TaskCard.Visible = false;
					FillRepTask();
				}
			}
		}

		public void Btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "NewTask":
					RepTask.Visible = false;
					TaskCard.Visible = true;
					CleanForm();
					break;
				case "SaveTask":
					ModifyTask();
					RepTask.Visible = true;
					TaskCard.Visible = false;
					FillRepTask();
					break;
				case "BtnSearch":
					string find = DatabaseConnection.FilterInjection(FindTxt.Text);
					FillRepTask("SELECT * FROM CRM_TODOLIST_VIEW WHERE " +
						"(OWNERID=" + UC.UserId + ") " +
						"AND (TASK LIKE '%" + find + "%' OR OUTCOME LIKE '%" + find + "%')" +
						" ORDER BY EXPIRATIONDATE ASC;",
					            "SELECT * FROM CRM_TODOLIST_VIEW WHERE " +
					            	"(CREATEDBYID=" + UC.UserId +
					            	" AND OWNERID<>" + UC.UserId + ") " +
					            	"AND (TASK LIKE '%" + find + "%' OR OUTCOME LIKE '%" + find + "%')" +
					            	"ORDER BY EXPIRATIONDATE ASC;");
					break;
			}
		}

		private void FillRepTask(string sqlstring, string sqlstring2)
		{
			RepTask.DataSource = DatabaseConnection.CreateDataset(sqlstring);
			RepTask.DataBind();
			if (RepTask.Items.Count == 0)
			{
				RepeaterInfo.Text =Root.rm.GetString("CRMTodtxt6");
				RepeaterInfo.Visible = true;
				RepTask.Visible = false;
			}
			RepTask2.DataSource = DatabaseConnection.CreateDataset(sqlstring2);
			RepTask2.DataBind();
			if (RepTask2.Items.Count == 0)
			{
				RepeaterInfo2.Text =Root.rm.GetString("CRMTodtxt6");
				RepeaterInfo2.Visible = true;
				RepTask2.Visible = false;
			}
			Session["TaskFromFind"] = 1;
		}

		private void FillRepTask()
		{
			RepTask.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CRM_TODOLIST_VIEW WHERE (EXPIRATIONDATE>='" +UC.LTZ.ToUniversalTime(DateTime.Now).ToString(@"yyyyMMdd") + "' OR (EXPIRATIONDATE<'" +UC.LTZ.ToUniversalTime(DateTime.Now).ToString(@"yyyyMMdd") + "' AND FLAGEXECUTED=0)) AND OWNERID=" + UC.UserId + " ORDER BY EXPIRATIONDATE ASC;");
			RepTask.DataBind();
			if (RepTask.Items.Count == 0)
			{
				RepeaterInfo.Text =Root.rm.GetString("CRMTodtxt6");
				RepeaterInfo.Visible = true;
				RepTask.Visible = false;
			}
			RepTask2.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CRM_TODOLIST_VIEW WHERE (EXPIRATIONDATE>='" +UC.LTZ.ToUniversalTime(DateTime.Now).ToString(@"yyyyMMdd") + "' OR (Expirationdate<'" +UC.LTZ.ToUniversalTime(DateTime.Now).ToString(@"yyyyMMdd") + "' AND FLAGEXECUTED=0)) AND (CREATEDBYID=" + UC.UserId + " AND OWNERID<>" + UC.UserId + ") ORDER BY EXPIRATIONDATE ASC;");
			RepTask2.DataBind();
			if (RepTask2.Items.Count == 0)
			{
				RepeaterInfo2.Text =Root.rm.GetString("CRMTodtxt6");
				RepeaterInfo2.Visible = true;
				RepTask2.Visible = false;
			}
			Session["TaskFromFind"] = null;
		}

		public void RepTask_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton DelTask = (LinkButton) e.Item.FindControl("DelTask");
					DelTask.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("CRMTodtxt10") + "');");
					DelTask.Text =Root.rm.GetString("CRMTodtxt9");
					if (Convert.ToInt64(DataBinder.Eval((DataRowView) e.Item.DataItem, "CreatedByID")) != UC.UserId)
						DelTask.Visible = false;

					LinkButton ModTask = (LinkButton) e.Item.FindControl("ModTask");
					ModTask.Text =Root.rm.GetString("CRMTodtxt11");

					bool es = (bool) DataBinder.Eval((DataRowView) e.Item.DataItem, "FlagExecuted");
					ImageButton Check = (ImageButton) e.Item.FindControl("Check");
					if (es)
					{
						Check.ImageUrl = "/i/checkon.gif";
					}
					else
					{
						Check.ImageUrl = "/i/checkoff.gif";
					}

					break;
			}

		}

		public void RepTask_Command(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelTask":
					DeleteTask(int.Parse(((Literal) (e.Item.FindControl("TaskID"))).Text));
					if (Session["TaskFromFind"] != null)
					{
						string find = DatabaseConnection.FilterInjection(FindTxt.Text);
						FillRepTask("SELECT * FROM CRM_TODOLIST_VIEW WHERE " +
							"(OWNERID=" + UC.UserId + ") " +
							"AND TASK LIKE '%" + find + "%'" +
							" ORDER BY EXPIRATIONDATE ASC;",
							"SELECT * FROM CRM_TODOLIST_VIEW WHERE " +
							"(CREATEDBYID=" + UC.UserId +
							" AND OWNERID<>" + UC.UserId + ") " +
							"AND TASK LIKe '%" + find + "%'" +
							"ORDER BY EXPIRATIONDATE ASC;");
					}
					else
						FillRepTask();
					break;
				case "ModTask":
					DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_TODOLIST_VIEW WHERE ID =" + int.Parse(((Literal) (e.Item.FindControl("TaskID"))).Text));
					TaskID.Text = ds.Tables[0].Rows[0]["ID"].ToString();
					ToDoList_OwnerID.Text = ds.Tables[0].Rows[0]["OwnerID"].ToString();
					ToDoList_Owner.Text = ds.Tables[0].Rows[0]["OwnerName"].ToString();
					ToDoList_ExpirationDate.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(ds.Tables[0].Rows[0]["ExpirationDate"])).ToShortDateString();
					ToDoList_CompanyID.Text = ds.Tables[0].Rows[0]["CompanyID"].ToString();
					ToDoList_CompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
					ToDoList_OpportunityID.Text = ds.Tables[0].Rows[0]["OpportunityID"].ToString();
					ToDoList_OpportunityTitle.Text = ds.Tables[0].Rows[0]["OpportunityTitle"].ToString();
					ToDoList_Task.Text = ds.Tables[0].Rows[0]["Task"].ToString();
					ToDoList_Outcome.Text = ds.Tables[0].Rows[0]["Outcome"].ToString();
					if (Convert.ToInt64(ds.Tables[0].Rows[0]["CreatedByID"]) != UC.UserId)
					{
						ToDoList_Owner.ReadOnly = true;
						ToDoList_ExpirationDate.ReadOnly = true;
						ToDoList_Task.ReadOnly = true;
						PopAccount.Visible = false;
						PopAccount2.Visible = false;
						PopAccount3.Visible = false;
						PopAccount4.Visible = false;
					}
					TaskCard.Visible = true;
					RepTask.Visible = false;
					break;
				case "CheckEseguito":
					FinitiClick(int.Parse(((Literal) (e.Item.FindControl("TaskID"))).Text));
					if (Session["TaskFromFind"] != null)
					{
						string find = DatabaseConnection.FilterInjection(FindTxt.Text);
						FillRepTask("SELECT TOP 10 * FROM CRM_TODOLIST_VIEW WHERE " +
							"(OWNERID=" + UC.UserId + ") " +
							"AND TASK LIKE '%" + find + "%'" +
							" oRDER BY EXPIRATIONDATE ASC;",
							"SELECT TOP 10 * FROM CRM_TODOLIST_VIEW WHERE " +
							"(CREATEDBYID=" + UC.UserId +
							" AND OWNERID<>" + UC.UserId + ") " +
							"AND TASK LIKE '%" + find + "%'" +
							"ORDER BY EXPIRATIONDATE ASC;");
					}
					else
						FillRepTask();
					break;
			}
		}

		public void DeleteTask(int id)
		{
			DatabaseConnection.DoCommand("DELETE CRM_TODOLIST WHERE ID =" + id);
		}

		public void FinitiClick(int id)
		{
			DatabaseConnection.DoCommand("UPDATE CRM_TODOLIST SET FLAGEXECUTED = " + ((bool)DatabaseConnection.SqlScalartoObj("SELECT * FROM TODOLIST WHERE ID =" + id)?0:1));
		}


		private void ModifyTask()
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("CREATEDBYID", UC.UserId, 'I');
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow, 'I');
				dg.Add("LASTMODIFIEDBYID", UC.UserId, 'I');

				try
				{
					dg.Add("OWNERID", Convert.ToInt64(ToDoList_OwnerID.Text));
				}
				catch
				{
					dg.Add("OWNERID", UC.UserId);
				}

				dg.Add("EXPIRATIONDATE", ToDoList_ExpirationDate.Text);
				if (ToDoList_CompanyID.Text.Length > 0) dg.Add("COMPANYID", ToDoList_CompanyID.Text);
				if (ToDoList_OpportunityID.Text.Length > 0) dg.Add("OPPORTUNITYID", ToDoList_OpportunityID.Text);
				dg.Add("TASK", ToDoList_Task.Text);
				dg.Add("OUTCOME", ToDoList_Outcome.Text);
				dg.Add("GROUPS", UC.UserGroupId);
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow, 'U');
				dg.Add("LASTMODIFIEDBYID", UC.UserId, 'U');
				dg.Execute("CRM_ToDolist","id=" + int.Parse(TaskID.Text));
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
			this.NewTask.Click += new EventHandler(this.Btn_Click);
			this.BtnSearch.Click += new EventHandler(this.Btn_Click);
			this.SaveTask.Click += new EventHandler(this.Btn_Click);
			this.RepTask.ItemCommand += new RepeaterCommandEventHandler(this.RepTask_Command);
			this.RepTask2.ItemCommand += new RepeaterCommandEventHandler(this.RepTask_Command);
			this.RepTask.ItemDataBound += new RepeaterItemEventHandler(this.RepTask_OnItemDataBound);
			this.RepTask2.ItemDataBound += new RepeaterItemEventHandler(this.RepTask_OnItemDataBound);
		}

		#endregion

		private void CleanForm()
		{
			ToDoList_OwnerID.Text = String.Empty;
			ToDoList_Owner.Text = String.Empty;
			ToDoList_ExpirationDate.Text = String.Empty;
			ToDoList_CompanyID.Text = String.Empty;
			ToDoList_CompanyName.Text = String.Empty;
			ToDoList_OpportunityID.Text = String.Empty;
			ToDoList_OpportunityTitle.Text = String.Empty;
			ToDoList_Task.Text = String.Empty;
			ToDoList_Outcome.Text = String.Empty;
			TaskID.Text = "-1";
			ToDoList_Owner.ReadOnly = false;
			ToDoList_ExpirationDate.ReadOnly = false;
			ToDoList_Task.ReadOnly = false;
		}
	}
}
