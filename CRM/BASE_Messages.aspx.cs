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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class Messages : G
	{


		protected LinkButton Deletebtn;

		private int messageToOpen=0;

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.ViewMessagesRicevuti.Click += new EventHandler(this.ViewMessages_Click);
			this.ViewMessagesInviati.Click += new EventHandler(this.ViewMessages_Click);

			this.DeleteAllSent.Click+=new EventHandler(DeleteAllSent_Click);
			this.DeleteAllReceived.Click+=new EventHandler(DeleteAllReceived_Click);

			this.NewRepMessagges.ItemCommand += new RepeaterCommandEventHandler(this.ItemCommandRep);
			this.NewRepMessagges.ItemDataBound += new RepeaterItemEventHandler(this.ItemDataBound_Repeater);
			this.NewRepMessagesSent.ItemCommand += new RepeaterCommandEventHandler(this.ItemCommandRepInviati);
			this.NewRepMessagesSent.ItemDataBound += new RepeaterItemEventHandler(this.ItemDataBound_RepeaterInviati);
		}

		#endregion


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				initNewRepeater();

				if (!Page.IsPostBack)
				{
					this.DeleteAllSent.Text =Root.rm.GetString("Mestxt17");
					this.DeleteAllReceived.Text =Root.rm.GetString("Mestxt16");
					ViewMessagesRicevuti.Text =Root.rm.GetString("Mestxt13");
					ViewMessagesInviati.Text =Root.rm.GetString("Mestxt14");
					if (Request.QueryString["r"] == "1" || Request.QueryString["r"] == null)
					{
						HeaderMessages.Text =Root.rm.GetString("Mestxt13");
						FillRep();
					}
					else
					{
						HeaderMessages.Text =Root.rm.GetString("Mestxt14");
						FillRepInviati();
					}
				}

			}
		}

		private void initNewRepeater()
		{
			this.NewRepMessagges.UsePagedDataSource=true;
			this.NewRepMessagges.PageSize=UC.PagingSize;
			this.NewRepMessagesSent.UsePagedDataSource=true;
			this.NewRepMessagesSent.PageSize=UC.PagingSize;
		}

		private void ViewMessages_Click(object sender, EventArgs e)
		{
			if (((LinkButton) sender).ID == "ViewMessagesRicevuti")
			{
				FillRep();
				HeaderMessages.Text =Root.rm.GetString("Mestxt13");
			}
			else
			{
				FillRepInviati();
				HeaderMessages.Text =Root.rm.GetString("Mestxt14");
			}
		}

		private void FillRep()
		{
			NewRepMessagges.Visible = true;
			NewRepMessagesSent.Visible = false;

			StringBuilder sqlString = new StringBuilder();

			sqlString.Append("SELECT *, (ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME) AS USERNAME ");
			sqlString.Append("FROM BASE_MESSAGES INNER JOIN ACCOUNT ON BASE_MESSAGES.FROMACCOUNT = ACCOUNT.UID ");
			sqlString.AppendFormat("WHERE TOACCOUNT={0} AND INOUT=0 ORDER BY BASE_MESSAGES.CREATEDDATE DESC", UC.UserId.ToString());

			this.NewRepMessagges.sqlDataSource=sqlString.ToString();
			this.NewRepMessagges.DataBind();

			DeleteAllSent.Visible=false;
			DeleteAllReceived.Visible=true;
		}

		private void FillRepInviati()
		{
			NewRepMessagesSent.Visible = true;
			NewRepMessagges.Visible = false;


			StringBuilder sqlString = new StringBuilder();

			sqlString.Append("SELECT *, (ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME) AS USERNAME ");
			sqlString.Append("FROM BASE_MESSAGES INNER JOIN ACCOUNT ON BASE_MESSAGES.TOACCOUNT = ACCOUNT.UID ");
			sqlString.AppendFormat("WHERE FROMACCOUNT={0} AND INOUT=1 ORDER BY BASE_MESSAGES.CREATEDDATE DESC", UC.UserId.ToString());

			this.NewRepMessagesSent.sqlDataSource=sqlString.ToString();
			this.NewRepMessagesSent.DataBind();


			DeleteAllSent.Visible=true;
			DeleteAllReceived.Visible=false;
		}



		private void ItemDataBound_Repeater(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					if (messageToOpen > 0)
					{
						if (messageToOpen == int.Parse(((Literal) e.Item.FindControl("IDMess")).Text))
						{
							Literal Mess = (Literal) e.Item.FindControl("ViewText");
							Mess.Text = "<tr><td colspan=\"5\" class=\"ListResult\" style=\"padding-top: 1px\">";
							Mess.Text += StaticFunctions.FixCarriage(((Literal) e.Item.FindControl("TextMess")).Text, false) + "</td></tr>";
						}
					}
					Literal ImgMessage = (Literal)e.Item.FindControl("ImgMessage");
					if((bool)DataBinder.Eval((DataRowView) e.Item.DataItem, "Readed"))
					{
						ImgMessage.Text="<img src=/i/buttonmessageread.gif>";
					}else
					{
						ImgMessage.Text="<img src=/i/buttonmessageunread.gif>";
					}
					break;
			}
		}

		private void ItemDataBound_RepeaterInviati(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					if (messageToOpen > 0)
					{
						if (messageToOpen == int.Parse(((Literal) e.Item.FindControl("IDMess")).Text))
						{
							Literal Mess = (Literal) e.Item.FindControl("ViewText");
							Mess.Text = "<tr><td colspan=\"5\" class=\"ListResult\" style=\"padding-top: 1px\">";
							Mess.Text += StaticFunctions.FixCarriage(((Literal) e.Item.FindControl("TextMess")).Text, false) + "</td></tr>";
						}
					}
					break;
			}
		}

		private void ItemCommandRep(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(this.NewRepMessagges.MultiDeleteListArray,"Base_Messages");
					FillRep();
					break;
				case "OpenMessage":
					messageToOpen=int.Parse(((Literal) (e.Item.FindControl("IDMess"))).Text);
					DatabaseConnection.DoCommand("UPDATE BASE_MESSAGES SET READED=1 WHERE ID="+messageToOpen);
					FillRep();
					break;
			}
		}

		private void ItemCommandRepInviati(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(this.NewRepMessagesSent.MultiDeleteListArray,"Base_Messages");
					FillRepInviati();
					break;
				case "OpenMessage":
					messageToOpen=int.Parse(((Literal) (e.Item.FindControl("IDMess"))).Text);
					DatabaseConnection.DoCommand("UPDATE BASE_MESSAGES SET READED=1 WHERE ID="+messageToOpen);
					FillRepInviati();
					break;
			}
		}


		private void DeleteAllSent_Click(object sender, EventArgs e)
		{
			DatabaseConnection.DoCommand("DELETE FROM BASE_MESSAGES WHERE INOUT=1 AND FROMACCOUNT=" + UC.UserId);
			this.FillRepInviati();
		}

		private void DeleteAllReceived_Click(object sender, EventArgs e)
		{
			DatabaseConnection.DoCommand("DELETE FROM BASE_MESSAGES WHERE INOUT=0 AND TOACCOUNT=" + UC.UserId);
			FillRep();
		}
	}



}
