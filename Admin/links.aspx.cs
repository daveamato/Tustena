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
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Admin
{
	public partial class links : G
	{
		protected LocalizedLiteral LocalizedLiteral1;
		protected LocalizedLiteral LocalizedLiteral2;
		protected LocalizedLiteral LocalizedLiteral3;

		protected void Page_Load(object sender, EventArgs e)
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
					FormTable.Visible = false;
					BtnCompanyType.Text =Root.rm.GetString("Listxt1");
					BtnContactType.Text =Root.rm.GetString("Listxt2");
					BtnContactEstimate.Text =Root.rm.GetString("Listxt3");

					MyUICulture.Items.Add("Choose another language");
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
							if (notExists)
								MyUICulture.Items.Add(new ListItem((Request.Form["eng"] == "0" ? ci.NativeName : ci.EnglishName), ci.Parent.Name.Substring(0, 2)));
						}
					}

					foreach (ListItem i in MyUICulture.Items)
					{
						Trace.Warn(i.Text, i.Value);
					}

					MyUICulture.SelectedValue = UC.Culture.Substring(0, 2);
				}
			}
		}

		public void Btn_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "BtnCompanyType":
					WhichList.Text = "\"" +Root.rm.GetString("Listxt1") + "\"";
					FillList("Description", "CompanyType");
					break;
				case "BtnContactType":
					WhichList.Text = "\"" +Root.rm.GetString("Listxt2") + "\"";
					FillList("ContactType", "ContactType");
					break;
				case "BtnContactEstimate":
					WhichList.Text = "\"" +Root.rm.GetString("Listxt3") + "\"";
					FillList("Estimate", "ContactEstimate");
					break;
				case "SubmitElement":
					ChangeElement(Convert.ToInt32(IDSelectElement.Text), ElementForm.Text, ElementCamp.Text);
					break;
			}
		}

		private void FillList(string field, string table)
		{
			Session["campo"] = field;
			Session["table"] = table;

			ListElement.DataSource = DatabaseConnection.CreateDataset("SELECT " + field + " AS LISTITEM, ID, LANG, K_ID FROM " + table + " WHERE ID=K_ID ORDER BY " + field);
			ListElement.DataBind();
		}

		public DataView getOtherLanguage(int id)
		{
			string Field = Session["campo"].ToString();
			string table = Session["table"].ToString();
			return DatabaseConnection.CreateDataset("SELECT " + Field + " AS LISTITEM, ID, LANG, K_ID FROM " + table + " WHERE ID<>K_ID AND K_ID=" + id.ToString() + " ORDER BY " + Field).Tables[0].DefaultView;
		}

		public void ElementsListCommand(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Modify":
					ElementDescription.Text = ((Literal) e.Item.FindControl("OpenEl")).Text;
					IDSelectElement.Text = ((Literal) e.Item.FindControl("IdElement")).Text;
					ElementForm.Text = Session["table"].ToString();
					ElementCamp.Text = Session["campo"].ToString();
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
					FormTable.Visible = true;
					break;
				case "Delete":
					CancelElement(int.Parse(((Literal) e.Item.FindControl("IdElement")).Text), Session["table"].ToString(), Session["campo"].ToString());
					break;
				case "New":
					ElementDescription.Text = String.Empty;
					IDSelectElement.Text = "-1";
					ElementForm.Text = Session["table"].ToString();
					ElementCamp.Text = Session["campo"].ToString();
					KElement.Text = "0";
					FormTable.Visible = true;
					break;
				case "NewElementLang":
					ElementDescription.Text = String.Empty;
					IDSelectElement.Text = "-1";
					ElementForm.Text = Session["table"].ToString();
					ElementCamp.Text = Session["campo"].ToString();
					KElement.Text = ((Literal) e.Item.FindControl("IdElement")).Text;
					FormTable.Visible = true;
					break;
			}
		}

		public void ElementsListDatabound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					try
					{
						LinkButton NewElement = (LinkButton) e.Item.FindControl("NuovoElemento");
						NewElement.Text =Root.rm.GetString("Listxt6");
					}
					catch
					{
					}
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					try
					{
						LinkButton DeleteElement = (LinkButton) e.Item.FindControl("DeleteElement");
						DeleteElement.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Listxt7") + "');");
						DeleteElement.Text =Root.rm.GetString("Delete");
						LinkButton ModElement = (LinkButton) e.Item.FindControl("ModElement");
						ModElement.Text =Root.rm.GetString("Listxt10");
						LinkButton NewElement = (LinkButton) e.Item.FindControl("NewElement");
						NewElement.Text =Root.rm.GetString("Listxt9");
					}
					catch
					{
					}
					break;
			}
		}

		private void CancelElement(int id, string table, string field)
		{
			string SqlString = "DELETE FROM " + table + " WHERE ID=" + id;
			DatabaseConnection.DoCommand(SqlString);
			FillList(field, table);
		}

		private void ChangeElement(int id, string table, string field)
		{
			string newId = String.Empty;

			using (DigiDapter dg = new DigiDapter("SELECT * FROM " + table + " WHERE ID=" + id))
			{
				if (!dg.HasRows)
				{
				}

				dg.Add(field, ElementDescription.Text);
				dg.Add("LANG", MyUICulture.SelectedValue);
				object obNewId = dg.Execute(table, "ID=" + id, DigiDapter.Identities.Identity);
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
					dg.Execute(table, "ID=" + newId, DigiDapter.Identities.Identity);
				}
			}

			FillList(field, table);
			FormTable.Visible = false;
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
			this.BtnCompanyType.Click += new EventHandler(this.Btn_Click);
			this.BtnContactType.Click += new EventHandler(this.Btn_Click);
			this.BtnContactEstimate.Click += new EventHandler(this.Btn_Click);
			this.SubmitElement.Click += new EventHandler(this.Btn_Click);
			this.ListElement.ItemCommand += new RepeaterCommandEventHandler(this.ElementsListCommand);
			this.ListElement.ItemDataBound += new RepeaterItemEventHandler(this.ElementsListDatabound);

		}

		#endregion
	}
}
