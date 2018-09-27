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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using FredCK.FCKeditorV2;

namespace Digita.Tustena
{
	public partial class editor : G
	{
		private string openFile;
		private string exitPage;

		protected RepeaterHeaderBegin RepeaterHeaderBegin1;
		protected RepeaterHeaderBegin RepeaterHeaderAlphabet1;
		protected RepeaterColumnHeader Repeatercolumnheader1;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				MailList.UsePagedDataSource = true;
				string s = Session.SessionID.ToString();
				DeleteGoBack();
				editor1.BasePath = "/webeditor/";
				editor1.Height = Unit.Parse("400px");


				if (!Page.IsPostBack)
				{
					ReloadCategories();

					openFile = String.Empty;
					exitPage = String.Empty;

					BtnNewML.Text =Root.rm.GetString("MLtxt36");
					BtnSearch.Text =Root.rm.GetString("Bcotxt2");
					if (Request.Params["fileToOpen"] != null)
					{
						string quick = Request.Params["fileToOpen"].ToString();
						ReadFile(quick);
						MailEditor.Visible = true;
						AddKeepAlive();
						TableFields.Visible = true;
                        MailListPanel.Visible = false;

					}
					else
					{
						MailEditor.Visible = false;
						TableFields.Visible = false;

						reloadrepeater();

						FileName.Text = "-1";
					}
				}
			}
		}

		private void ReloadCategories()
		{
			this.LoadCategories(this.SearchMailCategory,false);
			this.LoadCategories(this.MailCategory,true);
		}

		private void LoadCategories(DropDownList ddl, bool insert)
		{
			ddl.Items.Clear();
			ddl.Items.Add(new ListItem(Root.rm.GetString("Menutxt38"), "0"));
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,CATDESCRIPTION FROM ML_CATEGORIES").Tables[0];
			foreach (DataRow dr in dt.Rows)
			{
				string itemText = (dr[1].ToString().IndexOf("Mot") > -1) ?Root.rm.GetString(dr[1].ToString()) : dr[1].ToString();
				ddl.Items.Add(new ListItem(itemText, dr[0].ToString()));
			}
			if(insert)
			{
				ddl.Items.Add(new ListItem(Root.rm.GetString("Other"), "A99"));
				ddl.Attributes.Add("onchange", "SelectOther()");
			}
		}

		private void reloadrepeater()
		{
			MailList.PageSize = UC.PagingSize;
			MailList.sqlDataSource = "SELECT * FROM ML_MAIL ORDER BY ID DESC";
			MailList.DataBind();
		}

		protected void Open_Click(Object sender, EventArgs e)
		{
			this.Save();
			Response.Redirect(openFile);
		}

		protected void SaveAll_Click(Object sender, EventArgs e)
		{
			this.Save();
		}

		protected void New_Click(Object sender, EventArgs e)
		{
			this.Save();
			this.ClearEditor();
		}

		protected void Close_Click(Object sender, EventArgs e)
		{
			this.Save();
			Response.Redirect(exitPage);
		}

		private void Save()
		{
			scrivi(this.editor1.Value, this.SaveAs.Text, this.FileName.Text, this.NewMLDescription.Text);
		}

		private void ClearEditor()
		{
			this.FileName.Text = String.Empty;
			this.SaveAs.Text = String.Empty;
		}

		public void Btn_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "BtnNewML":
					MailEditor.Visible = true;
					editor1.Value="";
					AddKeepAlive();
					TableFields.Visible = true;
					MailListPanel.Visible = false;
					NewMLDescription.Text="";
					DocumentDescription.Text="";
					IDDocument.Text="";

					this.FileName.Text = "-1";
					this.SaveAs.Text = String.Empty;
					break;
			}
		}

		public void MailListDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton SendMail = (LinkButton) e.Item.FindControl("SendMail");

					SendMail.Text = "<img border=0 src='/i/persons.gif' alt=\"" + Root.rm.GetString("MLtxt0")+"\">";
					LinkButton SendSingle = (LinkButton) e.Item.FindControl("SendSingle");

					SendSingle.Text = "<img border=0 src='/i/mail.gif' alt=\"" + Root.rm.GetString("MLtxt0")+"\">";
					LinkButton ModifyMail = (LinkButton) e.Item.FindControl("ModifyMail");

					ModifyMail.Text = "<img border=0 src='/i/modify2.gif' alt='" + Root.rm.GetString("Modify")+"'>";
					LinkButton CopyMail = (LinkButton) e.Item.FindControl("CopyMail");

					CopyMail.Text = "<img border=0 src='/i/paste.gif' alt=\"" + Root.rm.GetString("Copy")+"\">";

					LinkButton MailLink = (LinkButton) e.Item.FindControl("MailLink");
					if(Convert.ToBoolean(DataBinder.Eval((DataRowView) e.Item.DataItem, "welcome")))
						MailLink.Text="<img src=\"/i/welcomemail.gif\" alt=\"Welcome Mail\" border=0>&nbsp;"+MailLink.Text;

					break;
			}
		}


		public void MailListCommand(object source, RepeaterCommandEventArgs e)
		{
			Trace.Warn("ITEMCOMMAND");

			switch (e.CommandName)
			{
				case "MailLink":
					Literal MailID = (Literal) e.Item.FindControl("MailID");
					ReadFile(MailID.Text);
					MailEditor.Visible = true;
					AddKeepAlive();
					TableFields.Visible = true;
                    MailListPanel.Visible = false;
					break;
				case "SendMail":
					Session["MailToSendID"] = ((Literal) e.Item.FindControl("MailID")).Text;
					Session["MailToSend"] = ((LinkButton) e.Item.FindControl("MailLink")).Text;
					Response.Redirect("newmailinglist.aspx?m=46&si=51");
					break;
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(MailList.MultiDeleteListArray,"ML_Mail");

					RebuildMailList();

					break;
				case "CopyMail":
					string copy;
					copy = String.Format("INSERT INTO ML_MAIL ([TITLE], [DESCRIPTION], [SUBJECT], [BODY], [GROUPS], [CREATEDBYID]) SELECT 'COPY OF '+ISNULL([TITLE],''), [DESCRIPTION], '{0} '+[SUBJECT], [BODY], [GROUPS], [CREATEDBYID] FROM [ML_MAIL] WHERE ID={1}",Root.rm.GetString("MLtxt35"), int.Parse(((Literal) e.Item.FindControl("MailID")).Text));
					DatabaseConnection.DoCommand(copy);
					reloadrepeater();
					break;
				case "SendSingle":
					Session["MailToSendID"] = ((Literal) e.Item.FindControl("MailID")).Text;
					Response.Redirect("sendsingleaddressmail.aspx?m=46&si=51");
					break;
			}
		}

		private void RebuildMailList()
		{
			MailEditor.Visible = false;
			TableFields.Visible = false;
			MailList.PageSize = UC.PagingSize;
			MailList.sqlDataSource = "SELECT * FROM ML_MAIL";
			MailList.DataBind();
			MailList.Visible = true;
			FileName.Text = "-1";
		}

		public void scrivi(string s, string nomefile, string id, string decription)
		{
			if (nomefile.Length <= 0)
			{
				nomefile=decription;
			}
			if (nomefile.Length > 0)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("CREATEDBYID", UC.UserId, 'I');
					dg.Add("GROUPS", "|" + UC.UserGroupId + "|",'I');

					dg.Add("SUBJECT", nomefile);
					dg.Add("DESCRIPTION", decription);
					dg.Add("WELCOME",(this.welcometype.Checked)?1:0);

					if (this.MailCategory.SelectedIndex > 0)
					{
						if (this.MailCategory.SelectedValue == "A99")
						{
							if(this.NewMailCategory.Text.Length>0)
								using (DigiDapter dgp = new DigiDapter())
								{
									dgp.Add("catdescription", this.NewMailCategory.Text);

									object newcatid = dgp.Execute("ML_Categories", DigiDapter.Identities.Identity);
									dg.Add("CATEGORYID", newcatid.ToString());
									ReloadCategories();
								}
						}
						else
							dg.Add("CATEGORYID", Convert.ToInt64(MailCategory.SelectedValue));
					}

					dg.Add("BODY", s);

					object newId = dg.Execute("ML_Mail", "id=" + id, DigiDapter.Identities.Identity);
					string idml = id;
					if(dg.RecordInserted)
						idml=newId.ToString();

					DatabaseConnection.DoCommand(String.Format("DELETE FROM ML_ATTACHMENT WHERE MLID={0};",idml));
					if(IDDocument.Text.Length>0)
					{
						DatabaseConnection.DoCommand(String.Format("INSERT INTO ML_ATTACHMENT (FILEID,MLID) VALUES ({0},{1})",int.Parse(IDDocument.Text),idml));
					}
				}

				MailEditor.Visible = false;
				TableFields.Visible = false;

				MailList.PageSize = UC.PagingSize;
				MailList.sqlDataSource = "SELECT * FROM ML_MAIL";
				MailList.DataBind();

				MailList.Visible = true;
				FileName.Text = "-1";
			}else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nomail","<script>alert('" + Root.rm.GetString("MLtxt45")+"')</script>");
			}


		}

		public void ReadFile(string fileName)
		{
			DataTable d = DatabaseConnection.CreateDataset("SELECT * FROM ML_MAIL WHERE ID=" + fileName).Tables[0];

			editor1.Value = d.Rows[0]["body"].ToString();
			this.SaveAs.Text = d.Rows[0]["subject"].ToString();
			this.NewMLDescription.Text = d.Rows[0]["Description"].ToString();
			this.FileName.Text = d.Rows[0]["id"].ToString();
			this.welcometype.Checked=(bool)d.Rows[0]["welcome"];
			if(d.Rows[0]["categoryid"]!=DBNull.Value)
			{
				foreach(ListItem li in this.MailCategory.Items)
				{
					if(li.Value==d.Rows[0]["categoryid"].ToString())
					{
						li.Selected=true;
						break;
					}
				}
			}

			DataTable dtAttach = DatabaseConnection.CreateDataset("SELECT FILEMANAGER.ID, FILEMANAGER.FILENAME FROM FILEMANAGER RIGHT OUTER JOIN ML_ATTACHMENT ON FILEMANAGER.ID = ML_ATTACHMENT.FILEID WHERE ML_ATTACHMENT.MLID = " + fileName).Tables[0];
			if(dtAttach.Rows.Count>0)
			{
				this.DocumentDescription.Text=dtAttach.Rows[0][1].ToString();
				this.IDDocument.Text=dtAttach.Rows[0][0].ToString();
			}else
			{
				this.DocumentDescription.Text="";
				this.IDDocument.Text="";
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
			this.MailList.ItemCommand+=new RepeaterCommandEventHandler(MailListCommand);
			this.MailList.ItemDataBound+=new RepeaterItemEventHandler(MailListDataBound);
			this.BtnNewML.Click += new EventHandler(this.Btn_Click);
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.SaveAll.Click += new EventHandler(this.SaveAll_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			StringBuilder query = new StringBuilder();
			query.Append("SELECT * FROM ML_MAIL");
			if(this.Search.Text.Length>0)
				query.AppendFormat(" AND (DESCRIPTION LIKE '%{0}' OR SUBJECT LIKE '%{0}%')",DatabaseConnection.FilterInjection(this.Search.Text));
			if(this.SearchMailCategory.SelectedIndex>0)
				query.AppendFormat(" AND (CATEGORYID={0})",this.SearchMailCategory.SelectedValue);

			MailList.sqlDataSource = query.ToString();
			MailList.DataBind();
		}
	}
}
