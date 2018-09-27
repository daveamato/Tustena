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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class Notes : G
	{

		protected Digita.Tustena.WebControls.RepeaterHeaderBegin RepeaterHeaderBegin1;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader1;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader2;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader3;
		protected Digita.Tustena.WebControls.RepeaterHeaderEnd RepeaterHeaderEnd1;
		protected System.Web.UI.WebControls.LinkButton ModifyNote;
		protected System.Web.UI.WebControls.Literal IDApp;
		protected System.Web.UI.WebControls.Literal TextApp;
		protected System.Web.UI.WebControls.TextBox AreaText;
		protected System.Web.UI.WebControls.LinkButton NoteSubmit;
		protected System.Web.UI.HtmlControls.HtmlGenericControl ViewText;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				NewRepNotes.UsePagedDataSource=true;
				NewRepNotes.PageSize=UC.PagingSize;

				if (!Page.IsPostBack)
				{
					FillRep();
				}


			}
			BtnSearch.Text =Root.rm.GetString("Notetxt4");
		}

		private void FillRep()
		{
			string sqlString = "SELECT BASE_NOTES.*,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS OWNER FROM BASE_NOTES LEFT OUTER JOIN ACCOUNT ON BASE_NOTES.OWNERID = ACCOUNT.UID ";

			string qGroup = GroupsSecure("BASE_NOTES.GROUPS");
			sqlString += "WHERE ((" + qGroup + ") OR BASE_NOTES.OWNERID=" + UC.UserId.ToString() + ") ORDER BY BASE_NOTES.CREATEDDATE DESC";

			NewRepNotes.UsePagedDataSource=true;
			NewRepNotes.PageSize=UC.PagingSize;
			NewRepNotes.sqlDataSource=sqlString.ToString();
			NewRepNotes.DataBind();


		}

		protected void ItemDataBound_Repeater(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					HtmlContainerControl ViewText = (HtmlContainerControl) e.Item.FindControl("ViewText");
					ViewText.Visible = false;
					break;
			}
		}

		protected void ItemCommandRep(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(this.NewRepNotes.MultiDeleteListArray,"Base_notes");
					FillRep();
					break;
				case "modifyNote":
					HtmlContainerControl ViewText = (HtmlContainerControl) e.Item.FindControl("ViewText");
					if (!ViewText.Visible)
					{
						ViewText.Visible = true;
					}
					else
					{
						ViewText.Visible = false;
					}
					break;
				case "noteSubmit":
					Literal IDApp = (Literal) e.Item.FindControl("IDApp");
					TextBox AreaText = (TextBox) e.Item.FindControl("AreaText");
					DatabaseConnection.DoCommand("UPDATE BASE_NOTES SET BODY = '"+DatabaseConnection.FilterInjection(AreaText.Text)+"' WHERE ID =" + int.Parse(IDApp.Text));
					FillRep();
					break;
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
			this.NewRepNotes.ItemDataBound+=new RepeaterItemEventHandler(this.ItemDataBound_Repeater);
			this.NewRepNotes.ItemCommand += new RepeaterCommandEventHandler(this.ItemCommandRep);
            this.Load += new EventHandler(this.Page_Load);
		}

		#endregion

		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			string sqlString = "SELECT BASE_NOTES.*,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS OWNER FROM BASE_NOTES LEFT OUTER JOIN ACCOUNT ON BASE_NOTES.OWNERID = ACCOUNT.UID ";
			string qGroup = GroupsSecure("BASE_NOTES.GROUPS");
			sqlString += "WHERE ((" + qGroup + ") OR BASE_NOTES.OWNERID=" + UC.UserId + ")";
			DeleteGoBack(true);
		}
	}


}
