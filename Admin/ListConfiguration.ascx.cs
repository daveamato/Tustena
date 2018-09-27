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

using System.Globalization;
using System.Resources;
using System.Web.UI;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Admin
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public partial class ListConfiguration : System.Web.UI.UserControl
	{

		protected System.Web.UI.WebControls.Literal Literal16;

		private UserConfig UC = new UserConfig();


		protected void Page_Load(object sender, System.EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];
			if (!Page.IsPostBack)
			{
				Form.Visible = false;
				foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (ConfigSettings.SupportedLanguages.IndexOf(ci.Parent.Name) > -1)
					{
						bool notExists = true;
						foreach (ListItem i in MyUICulture.Items)
						{
							if (i.Value == ci.Parent.Name)
							{
								notExists = false;
								break;
							}
						}
                        if (notExists && ci.Parent.Name.Length>=2)
							MyUICulture.Items.Add(new ListItem((Request.Form["eng"] == "0" ? ci.NativeName : ci.EnglishName), ci.Parent.Name.Substring(0, 2)));
					}
				}



				MyUICulture.SelectedValue = UC.Culture.Substring(0, 2);
			}
		}

		public void ChangeList(string list)
		{
			switch (list)
			{
				case "BtnCompanyType":

					FillList("Description", "CompanyType");
					break;
				case "BtnContactType":

					FillList("ContactType", "ContactType");
					break;
				case "BtnContactEstimate":

					FillList("Estimate", "ContactEstimate");
					break;
				case "BtnLeadOrigin":

					FillList("Description", "CRM_LeadDescription",4);
					break;
			}
		}

		private void FillList(string listsfield, string liststable)
		{
			FillList(listsfield, liststable,-1);
		}

		private void FillList(string listsfield, string liststable,int type)
		{
			Session["listsfield"] = listsfield;
			Session["liststable"] = liststable;
			ViewState["type"]=type;
			if(type==-1)
				ListElement.DataSource = DatabaseConnection.CreateDataset("SELECT " + listsfield + " AS LISTITEM, ID, LANG, K_ID FROM " + liststable + " WHERE ID=K_ID ORDER BY " + listsfield);
			else
                ListElement.DataSource = DatabaseConnection.CreateDataset("SELECT " + listsfield + " AS LISTITEM, ID, LANG, K_ID FROM " + liststable + " WHERE ID=K_ID AND TYPE=" + type + " ORDER BY " + listsfield);
			ListElement.DataBind();
		}

		public DataView getOtherLanguage(int id)
		{
			string listsField = Session["listsfield"].ToString();
			string listsTable = Session["liststable"].ToString();
			return DatabaseConnection.CreateDataset("SELECT " + listsField + " AS LISTITEM, ID, LANG, K_ID FROM " + listsTable + " WHERE ID<>K_ID AND K_ID=" + id + " ORDER BY " + listsField).Tables[0].DefaultView;
		}

		public void ElementsListCommand(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Modify":
					ElementDescription.Text = ((Literal) e.Item.FindControl("OpenEl")).Text;
					IDSelectElement.Text = ((Literal) e.Item.FindControl("IdElement")).Text;
					ElementForm.Text = Session["liststable"].ToString();
					ElementCamp.Text = Session["listsfield"].ToString();
					string lang = ((Literal) e.Item.FindControl("LangLabel")).Text;
					MyUICulture.SelectedIndex = -1;
					foreach (ListItem i in MyUICulture.Items)
					{
						if (i.Value == lang)
						{
							i.Selected = true;
							break;
						}
					}
					KElement.Text = ((Literal) e.Item.FindControl("KElement")).Text;
					Form.Visible = true;
					break;
				case "Delete":
					DeleteComponent(int.Parse(((Literal) e.Item.FindControl("IdElement")).Text), Session["liststable"].ToString(), Session["listsfield"].ToString());
					break;
				case "New":
					ElementDescription.Text = String.Empty;
					IDSelectElement.Text = "-1";
					ElementForm.Text = Session["liststable"].ToString();
					ElementCamp.Text = Session["listsfield"].ToString();
					KElement.Text = "0";
					Form.Visible = true;
					break;
				case "NewElementLang":
					ElementDescription.Text = String.Empty;
					IDSelectElement.Text = "-1";
					ElementForm.Text = Session["liststable"].ToString();
					ElementCamp.Text = Session["listsfield"].ToString();
					KElement.Text = ((Literal) e.Item.FindControl("IdElement")).Text;
					Form.Visible = true;
					break;
			}
		}

		public void ElementsListDatabound(object source, RepeaterItemEventArgs e)
		{
			LinkButton NewElement;
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					try
					{
						NewElement = (LinkButton) e.Item.FindControl("NewElement");
						NewElement.Text =Root.rm.GetString("Listxt6");
					}
					catch
					{
					}
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
						LinkButton DeleteElement = (LinkButton) e.Item.FindControl("DeleteElement");
						DeleteElement.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Listxt7") + "');");
						DeleteElement.Text =Root.rm.GetString("Delete");
						LinkButton ModElement = (LinkButton) e.Item.FindControl("ModElement");
						ModElement.Text =Root.rm.GetString("Listxt10");
						try
						{
							NewElement = (LinkButton) e.Item.FindControl("NewElement");
							NewElement.Text =Root.rm.GetString("Listxt9");
						}
						catch
						{
						}

					break;
			}
		}

		private void DeleteComponent(int id, string liststable, string listsfield)
		{
			string delSql = "DELETE FROM " + liststable + " WHERE ID=" + id;
			DatabaseConnection.DoCommand(delSql);
			FillList(listsfield, liststable,(int)ViewState["type"]);
		}

		private void ChangeElement(int id, string liststable, string listsfield)
		{
			string newId = String.Empty;

			using (DigiDapter dg = new DigiDapter("SELECT * FROM " + liststable + " WHERE ID=" + id))
			{
				if (!dg.HasRows)
				{
				}

				dg.Add(listsfield, ElementDescription.Text);
				dg.Add("LANG", MyUICulture.SelectedValue);
				if((int)ViewState["type"]!=-1)dg.Add("TYPE", (int)ViewState["type"]);

				object obNewId = dg.Execute(liststable, "ID=" + id, DigiDapter.Identities.Identity);
				try
				{
					newId = obNewId.ToString();
				}
				catch
				{
				}

			}
			if ((newId != null && newId.Length != 0) && id.ToString() != newId)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("K_ID", (KElement.Text == "0") ? newId : KElement.Text);
					dg.Execute(liststable, "ID=" + newId, DigiDapter.Identities.Identity);
				}
			}

			FillList(listsfield, liststable, (int)ViewState["type"]);
			Form.Visible = false;
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{

			this.SubmitElement.Click+=new EventHandler(SubmitElement_Click);
			this.ListElement.ItemCommand += new RepeaterCommandEventHandler(this.ElementsListCommand);
			this.ListElement.ItemDataBound += new RepeaterItemEventHandler(this.ElementsListDatabound);
		}
		#endregion

		private void SubmitElement_Click(object sender, EventArgs e)
		{
			ChangeElement(int.Parse(IDSelectElement.Text), ElementForm.Text, ElementCamp.Text);
		}
	}
}
