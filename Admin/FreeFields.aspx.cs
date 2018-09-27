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
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public class FreeFileds : G
	{
		protected TextBox Field;
		protected TextBox ViewOrderField;
		protected TextBox FieldID;
		protected DropDownList Type;
		protected TextBox SelectItem;
		protected Label ContactInfo;
		protected Repeater RepContacts;
		protected HtmlTable AddContactFields;
		protected LinkButton FreeForContact;
		protected LinkButton FreeForReferenti;
		protected LinkButton FreeForLeads;
		protected LinkButton FreeForContact2;
		protected LinkButton FreeForReferenti2;
		protected LinkButton FreeForLeads2;
		protected LinkButton SubmitContact;
		protected GroupControl Groups;
		protected Literal SomeJS;
		protected DropDownList FieldRef;
		protected DropDownList FieldRefValue;
		protected LocalizedLiteral LocalizedLiteral1;
		protected LocalizedLiteral LocalizedLiteral2;
		protected LocalizedLiteral LocalizedLiteral3;
		protected LocalizedLiteral LocalizedLiteral4;
		protected LocalizedLiteral LocalizedLiteral5;
		protected LocalizedLiteral LocalizedLiteral6;
		protected LocalizedLiteral LocalizedLiteral7;
		protected HtmlTable tabControl;
		protected HtmlTableCell tdTab1;
		protected HtmlTableCell tdTab2;
		protected HtmlTableCell tdTab3;
		protected LinkButton BtnNew;
		protected Literal HelpLabel;

		public void Page_Load(object sender, EventArgs e)
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
					AddContactFields.Visible = false;
					FreeForContact.Text =Root.rm.GetString("Fretxt2");
					FreeForReferenti.Text =Root.rm.GetString("Fretxt3");
					FreeForLeads.Text =Root.rm.GetString("Fretxt0");
					FreeForContact2.Text =Root.rm.GetString("Fretxt2").ToUpper();
					FreeForReferenti2.Text =Root.rm.GetString("Fretxt3").ToUpper();
					FreeForLeads2.Text =Root.rm.GetString("Fretxt0").ToUpper();

					ListItem lt = new ListItem();
					lt.Value = "1";
					lt.Text =Root.rm.GetString("Fretxt4");
					Type.Items.Add(lt);
					lt = new ListItem();
					lt.Value = "2";
					lt.Text =Root.rm.GetString("Fretxt5");
					Type.Items.Add(lt);
					lt = new ListItem();
					lt.Value = "3";
					lt.Text =Root.rm.GetString("Fretxt11");
					Type.Items.Add(lt);
					lt = new ListItem();
					lt.Value = "4";
					lt.Text =Root.rm.GetString("Fretxt12");
					Type.Items.Add(lt);

					Type.Attributes.Add("onchange", "ShowHideItems(this);");

					SubmitContact.Text =Root.rm.GetString("Fretxt6");
					BtnNew.Text =Root.rm.GetString("Fretxt21");
					HelpLabel.Text = FillHelp("HelpFreeField");
					Session["TableFreeField"] = "Base_Companies";
					FillRepContacts();
				}
			}
		}

		public void TabSwitch()
		{
			TabSwitch(false);
		}

		public void TabSwitch(bool refresh)
		{
			switch (Session["TableFreeField"].ToString())
			{
				case "Base_Companies":
					Page.RegisterStartupScript("tabActive", "<script>ViewHideTabs('tdTab1');</script>");
					if (refresh) FillRepContacts();
					break;
				case "Base_Contacts":
					Page.RegisterStartupScript("tabActive", "<script>ViewHideTabs('tdTab2');</script>");
					if (refresh) FillRepReference();
					break;
				case "CRM_Leads":
					Page.RegisterStartupScript("tabActive", "<script>ViewHideTabs('tdTab3');</script>");
					if (refresh) FillRepLead();
					break;
			}

		}



		public void FreeForContact_click(object sender, EventArgs e)
		{
			AddContactFields.Visible = false;
			switch (((LinkButton) sender).ID)
			{
				case "FreeForContact":
				case "FreeForContact2":
					Session["TableFreeField"] = "Base_Companies";
					FillRepContacts();
					break;
				case "FreeForReferenti":
				case "FreeForReferenti2":
					Session["TableFreeField"] = "Base_Contacts";
					FillRepReference();
					break;
				case "FreeForLeads":
				case "FreeForLeads2":
					Session["TableFreeField"] = "CRM_Leads";
					FillRepLead();
					break;
			}
			TabSwitch();

		}

		private void FillRepLead()
		{
			RepContacts.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM ADDEDFIELDS WHERE TABLENAME={0} ORDER BY VIEWORDER", (byte) CRMTables.CRM_Leads));
			RepContacts.DataBind();
			RepContacts.Visible = true;

			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE REFERENCETABLE=" + (byte) CRMTables.CRM_Leads);
			FieldRef.Items.Clear();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem li = new ListItem();
				li.Text =Root.rm.GetString("SQLFree" + dr["rmValue"].ToString());
				li.Value = dr["ID"].ToString();
				FieldRef.Items.Add(li);
			}
			FieldRef.Items.Insert(0,Root.rm.GetString("SQLFree0"));
			FieldRef.Items[0].Value = "0";

		}


		private void FillRepReference()
		{
			RepContacts.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM ADDEDFIELDS WHERE TABLENAME={0} ORDER BY VIEWORDER", (byte) CRMTables.Base_Contacts));
			RepContacts.DataBind();
			RepContacts.Visible = true;

			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE REFERENCETABLE=" + (byte) CRMTables.Base_Contacts);
			FieldRef.Items.Clear();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem li = new ListItem();
				li.Text =Root.rm.GetString("SQLFree" + dr["rmValue"].ToString());
				li.Value = dr["ID"].ToString();
				FieldRef.Items.Add(li);
			}
			FieldRef.Items.Insert(0,Root.rm.GetString("SQLFree0"));
			FieldRef.Items[0].Value = "0";
		}

		private void FillRepContacts()
		{
			RepContacts.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM ADDEDFIELDS WHERE TABLENAME={0} ORDER BY VIEWORDER", (byte) CRMTables.Base_Companies));
			RepContacts.DataBind();
			RepContacts.Visible = true;

			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE REFERENCETABLE=" + (byte) CRMTables.Base_Companies);
			FieldRef.Items.Clear();
			foreach (DataRow dr in ds.Tables[0].Rows)
			{
				ListItem li = new ListItem();
				li.Text =Root.rm.GetString("SQLFree" + dr["rmValue"].ToString());
				li.Value = dr["ID"].ToString();
				FieldRef.Items.Add(li);
			}
			FieldRef.Items.Insert(0,Root.rm.GetString("SQLFree0"));
			FieldRef.Items[0].Value = "0";
		}

		public void FieldRef_Change(object sender, EventArgs e)
		{
			StringBuilder query = new StringBuilder();
			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE ID=" + DatabaseConnection.FilterInjection(FieldRef.SelectedValue));
			if (ds.Tables[0].Rows.Count > 0)
			{
				DataRow dr = ds.Tables[0].Rows[0];
				switch (dr["QueryType"].ToString())
				{
					case "0":
						query.AppendFormat("SELECT {0} AS V1,{1} AS V2 FROM {2} ", dr["SonFieldValue"].ToString(), dr["SonFieldTxt"].ToString(), dr["SonTable"].ToString());
						query.AppendFormat("WHERE LANG='{0}'", UC.Culture.Substring(0, 2));
						break;
					case "1":
						query.AppendFormat("SELECT {0} AS V1,{1} AS V2 FROM {2} ", dr["SonFieldValue"].ToString(), dr["SonFieldTxt"].ToString(), dr["SonTable"].ToString());
						break;
				}
				FieldRefValue.DataTextField = "v2";
				FieldRefValue.DataValueField = "v1";
				FieldRefValue.DataSource = DatabaseConnection.CreateDataset(query.ToString());
				FieldRefValue.DataBind();
				FieldRefValue.Items.Insert(0,Root.rm.GetString("SQLFree0"));
				FieldRefValue.Items[0].Value = "0";
			}
		}

		public void NewEntry_Click(object sender, EventArgs e)
		{
			AddContactFields.Visible = true;
			Field.Text = String.Empty;
			ViewOrderField.Text = String.Empty;
			Type.SelectedIndex = 0;
			FieldRef.SelectedIndex = 0;
			FieldRefValue.SelectedIndex = 0;
			Groups.SetGroups("|" + UC.UserGroupId + "|");
			TabSwitch();

		}

		public void SubmitContact_Click(object sender, EventArgs e)
		{
			switch (Session["TableFreeField"].ToString())
			{
				case "Base_Companies":
					if (AddFields((byte) CRMTables.Base_Companies))
					{
						ContactInfo.Text = String.Empty;
						FillRepContacts();
					}
					else
					{
						ContactInfo.Text =Root.rm.GetString("Fretxt9");
					}
					break;
				case "Base_Contacts":
					if (AddFields((byte) CRMTables.Base_Contacts))
					{
						ContactInfo.Text = String.Empty;
						FillRepReference();
					}
					else
					{
						ContactInfo.Text =Root.rm.GetString("Fretxt9");
					}
					break;
				case "CRM_Leads":
					if (AddFields((byte) CRMTables.CRM_Leads))
					{
						ContactInfo.Text = String.Empty;
						FillRepLead();
					}
					else
					{
						ContactInfo.Text =Root.rm.GetString("Fretxt9");
					}
					break;
			}
			TabSwitch();
		}

		private bool AddFields(byte tablename)
		{
			if (Field.Text.Length == 0) return false;

			int fieldID=int.Parse(FieldID.Text);

			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("TABLENAME", tablename);
				dg.Add("NAME", Field.Text);
				try
				{
					dg.Add("VIEWORDER", Convert.ToInt32(ViewOrderField.Text));
				}
				catch
				{
					dg.Add("VIEWORDER", 1);
				}


				dg.Add("TYPE", Type.SelectedItem.Value);

				if (FieldRef.SelectedIndex > 0 && FieldRefValue.SelectedIndex > 0)
				{
					dg.Add("PARENTFIELD", DatabaseConnection.SqlScalar("SELECT REFERENCEFIELD FROM ADDEDFIELD_DROPDOWN WHERE ID=" + int.Parse(FieldRef.SelectedValue)));
					dg.Add("PARENTQUERY", FieldRef.SelectedValue.ToString());
					dg.Add("PARENTFIELDVALUE", FieldRefValue.SelectedValue);
				}
				else
				{
					dg.Add("PARENTFIELD", DBNull.Value);
					dg.Add("PARENTFIELDVALUE", DBNull.Value);
					dg.Add("PARENTQUERY", DBNull.Value);
				}

				if (Type.SelectedItem.Value == "4") dg.Add("ITEMS", "|" + SelectItem.Text.Replace(',', '|') + "|");
				if (Groups.GetValue.Length > 0)
				{
					dg.Add("GROUPS", Groups.GetValue);
				}
				else
				{
					dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");
				}
				dg.Execute("ADDEDFIELDS", "ID=" + fieldID);
			}
			FieldID.Text = "-1";
			Field.Text = String.Empty;
			ViewOrderField.Text = String.Empty;
			Groups.SetGroups("|" + UC.UserGroupId + "|");
			return true;
		}

		private void ItemDataBound_RepContacts(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label FieldGroups = (Label) e.Item.FindControl("FieldGroups");
					string[] Groups = ((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "Groups")).Split('|');
					string query = String.Empty;
					foreach (string gr in Groups)
					{
						if (gr.Length > 0) query += " OR ID=" + gr;
					}
					Trace.Warn("querydesc", query);
					if (query.Length > 4) query = query.Substring(4);
					string sqlQuery;
					sqlQuery = "SELECT DESCRIPTION FROM GROUPS";
					if (query.Length > 0)
						sqlQuery += "WHERE " + query;

					DataSet ds = DatabaseConnection.CreateDataset(sqlQuery);
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						FieldGroups.Text += dr[0].ToString() + ", ";
					}
					FieldGroups.Text = FieldGroups.Text.Trim(',', ' ');
					Label TypeDescription = (Label) e.Item.FindControl("TypeDescription");
				switch (Convert.ToInt16(DataBinder.Eval((DataRowView) e.Item.DataItem, "Type")))
				{
					case 1:
						TypeDescription.Text =Root.rm.GetString("Fretxt4");
						break;
					case 2:
						TypeDescription.Text =Root.rm.GetString("Fretxt5");
						break;
					case 3:
						TypeDescription.Text =Root.rm.GetString("Fretxt11");
						break;
					case 4:
						TypeDescription.Text =Root.rm.GetString("Fretxt12");
						break;
				}


					LinkButton DelField = (LinkButton) e.Item.FindControl("DelField");
					DelField.Text =Root.rm.GetString("Fretxt7");
					DelField.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Fretxt8") + "');");
					LinkButton ModField = (LinkButton) e.Item.FindControl("ModField");
					ModField.Text =Root.rm.GetString("Fretxt10");

					if (DataBinder.Eval((DataRowView) e.Item.DataItem, "ParentQuery").ToString().Length > 0)
					{
						DataRow drw = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE ID=" + int.Parse(DataBinder.Eval((DataRowView) e.Item.DataItem, "ParentQuery").ToString())).Tables[0].Rows[0];
						Label secampo = (Label) e.Item.FindControl("FieldParam");

						string sqlResult = DatabaseConnection.SqlScalar(String.Format("SELECT TOP 1 {0} FROM {1} WHERE {2}='{3}'", drw["sonfieldtxt"].ToString(), drw["sontable"].ToString(), drw["sonfieldvalue"].ToString(), DatabaseConnection.FilterInjection(DataBinder.Eval((DataRowView) e.Item.DataItem, "ParentFieldValue").ToString())));


						secampo.Text =Root.rm.GetString("SQLFree" + drw["rmvalue"].ToString()) + " = " + sqlResult;
					}

					break;
			}
		}


		private void RepContacts_Command(object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelField":
					string delSql = "DELETE FROM ADDEDFIELDS WHERE ID =" + int.Parse(((Literal) e.Item.FindControl("FieldID")).Text);
					DatabaseConnection.DoCommand(delSql);
					break;
				case "ModField":
					AddContactFields.Visible = true;
					string sqlStringMod = "SELECT * FROM ADDEDFIELDS WHERE ID =" + int.Parse(((Literal) e.Item.FindControl("FieldID")).Text);
					DataSet ds = DatabaseConnection.CreateDataset(sqlStringMod);
					Field.Text = ds.Tables[0].Rows[0]["Name"].ToString();
					ViewOrderField.Text = ds.Tables[0].Rows[0]["ViewOrder"].ToString();
					Groups.SetGroups(ds.Tables[0].Rows[0]["Groups"].ToString());
					FieldID.Text = ((Literal) e.Item.FindControl("FieldID")).Text;
					Type.SelectedItem.Selected = false;
					foreach (ListItem li in Type.Items)
					{
						if (li.Value == ds.Tables[0].Rows[0]["Type"].ToString())
						{
							li.Selected = true;
							break;
						}
					}

					DataSet dsDrop = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE REFERENCETABLE=" + ds.Tables[0].Rows[0]["tablename"].ToString());
					DataSet dsdropvalue = new DataSet();
					FieldRef.Items.Clear();
					foreach (DataRow dr in dsDrop.Tables[0].Rows)
					{
						ListItem li = new ListItem();
						li.Text =Root.rm.GetString("SQLFree" + dr["rmValue"].ToString());
						li.Value = dr["ID"].ToString();
						if (ds.Tables[0].Rows[0]["ParentField"].ToString() == dr["ReferenceField"].ToString())
						{
							li.Selected = true;
							dsdropvalue = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELD_DROPDOWN WHERE ID=" + int.Parse(dr["id"].ToString()));
						}
						FieldRef.Items.Add(li);
					}
					FieldRef.Items.Insert(0,Root.rm.GetString("SQLFree0"));
					FieldRef.Items[0].Value = "0";

					if (dsdropvalue.Tables.Count > 0)
					{
						DataRow drdropvalue = dsdropvalue.Tables[0].Rows[0];
						StringBuilder query = new StringBuilder();
						switch (drdropvalue["querytype"].ToString())
						{
							case "0":
								query.AppendFormat("SELECT {0} AS V1,{1} AS V2 FROM {2} ", drdropvalue["SonFieldValue"].ToString(), drdropvalue["SonFieldTxt"].ToString(), drdropvalue["SonTable"].ToString());
								query.AppendFormat("WHERE LANG='{0}'", UC.Culture);
								break;
							case "1":
								query.AppendFormat("SELECT {0} AS V1,{1} AS V2 FROM {2} ", drdropvalue["SonFieldValue"].ToString(), drdropvalue["SonFieldTxt"].ToString(), drdropvalue["SonTable"].ToString());
								break;
						}

						FieldRefValue.DataTextField = "v2";
						FieldRefValue.DataValueField = "v1";
						FieldRefValue.DataSource = DatabaseConnection.CreateDataset(query.ToString());
						FieldRefValue.DataBind();
						FieldRefValue.Items.Insert(0,Root.rm.GetString("SQLFree0"));
						FieldRefValue.Items[0].Value = "0";
					}
					foreach (ListItem li in FieldRefValue.Items)
					{
						if (li.Value == ds.Tables[0].Rows[0]["ParentFieldValue"].ToString())
						{
							li.Selected = true;
							break;
						}
					}

					if (ds.Tables[0].Rows[0]["Type"].ToString() == "4")
					{
						SomeJS.Text = "<script>ShowHideItems(document.getElementById('TYPE'));</script>";
					}
					ds.Clear();
					break;
			}
			this.TabSwitch(true);
		}


		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.RepContacts.ItemDataBound += new RepeaterItemEventHandler(ItemDataBound_RepContacts);
			this.RepContacts.ItemCommand += new RepeaterCommandEventHandler(RepContacts_Command);			this.FreeForContact.Click += new EventHandler(this.FreeForContact_click);
			this.FreeForReferenti.Click += new EventHandler(this.FreeForContact_click);
			this.FreeForLeads.Click += new EventHandler(this.FreeForContact_click);
			this.FreeForContact2.Click += new EventHandler(this.FreeForContact_click);
			this.FreeForReferenti2.Click += new EventHandler(this.FreeForContact_click);
			this.FreeForLeads2.Click += new EventHandler(this.FreeForContact_click);
			this.BtnNew.Click += new EventHandler(this.NewEntry_Click);
			this.SubmitContact.Click += new EventHandler(this.SubmitContact_Click);

		}

		#endregion
	}
}
