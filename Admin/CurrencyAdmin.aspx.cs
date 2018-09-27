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
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Admin
{
	public partial class CurrencyAdmin : G
	{

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
					Start();
				}
			}
		}

		private void Start()
		{
			CurrSubmit.Text =Root.rm.GetString("Save");
			NewCurrSubmit.Text =Root.rm.GetString("Save");
			CurrChange1.Attributes.Add("onchange", "change(0)");
			CurrChange2.Attributes.Add("onchange", "change(1)");
			CancelForm();

			CurrencyRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CURRENCYTABLE").Tables[0];
			CurrencyRepeater.DataBind();
			CurrencyRepeater.Visible = true;
			CurrencyTable.Visible = true;

			CurrencySymbol.Text = DatabaseConnection.SqlScalar("SELECT CURRENCY FROM CURRENCYTABLE WHERE (CHANGETOEURO=1 AND CHANGEFROMEURO=1)");
			ChangeCurrency.Text =Root.rm.GetString("Curtxt7");

			CompanyCurrencyTable.Visible = false;
		}

		private void CancelForm()
		{
			CurrName.Text = String.Empty;
			CurrChange1.Text = String.Empty;
			CurrChange2.Text = String.Empty;
			CurrSymbol.Text = String.Empty;
			CurrID.Text = "-1";
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
			this.CurrSubmit.Click += new EventHandler(CurrSubmit_Click);
			this.CurrencyRepeater.ItemDataBound += new RepeaterItemEventHandler(CurrencyRepeater_ItemDataBound);
			this.CurrencyRepeater.ItemCommand += new RepeaterCommandEventHandler(CurrencyRepeater_ItemCommand);
			this.ChangeCurrency.Click += new EventHandler(ChangeCurrency_Click);
			this.NewCurrSubmit.Click += new EventHandler(NewCurrSubmit_Click);
		}

		#endregion

		private void CurrSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				int id = int.Parse(CurrID.Text);
				string sqlString = "SELECT * FROM CURRENCYTABLE WHERE ID =" + id;
				using (DigiDapter dg = new DigiDapter(sqlString))
				{
					if (!dg.HasRows)
					{
					}
					dg.Add("CURRENCY", CurrName.Text);
					dg.Add("CURRENCYSYMBOL", CurrSymbol.Text);
					dg.Add("CHANGETOEURO", StaticFunctions.FixDecimal(CurrChange1.Text));
					dg.Add("CHANGEFROMEURO", StaticFunctions.FixDecimal(CurrChange2.Text));
					dg.Execute("CURRENCYTABLE", "ID=" + id);
				}
				CurrencyRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CURRENCYTABLE").Tables[0];
				CurrencyRepeater.DataBind();
				CancelForm();
				Start();
			}
			catch
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" +Root.rm.GetString("Curtxt12") + "');</script>");
			}
		}

		private void CurrencyRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton Edit = (LinkButton) e.Item.FindControl("Edit");
					Edit.Text =Root.rm.GetString("Modify");
					LinkButton Delete = (LinkButton) e.Item.FindControl("Delete");
					Delete.Text =Root.rm.GetString("Delete");
					Delete.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Curtxt13") + "')");
					if ((double) DataBinder.Eval((DataRowView) e.Item.DataItem, "ChangeToEuro") == 1 && (double) DataBinder.Eval((DataRowView) e.Item.DataItem, "ChangeToEuro") == 1)
					{
						Edit.Visible = false;
						Delete.Visible = false;
					}
					break;
			}
		}

		private void CurrencyRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			int lId = int.Parse(((Literal) e.Item.FindControl("CurrencyID")).Text);
			switch (e.CommandName)
			{
				case "Edit":
					DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CURRENCYTABLE WHERE ID=" + lId).Tables[0].Rows[0];
					CurrName.Text = dr["Currency"].ToString();
					CurrSymbol.Text = dr["CurrencySymbol"].ToString();
					CurrChange1.Text = dr["ChangeToEuro"].ToString();
					CurrChange2.Text = dr["ChangeFromEuro"].ToString();
					CurrID.Text = dr["id"].ToString();
					break;
				case "Delete":
					DatabaseConnection.DoCommand(String.Format("DELETE FROM CURRENCYTABLE WHERE ID={0}", lId));
					Start();
					break;
			}
		}

		private void ChangeCurrency_Click(object sender, EventArgs e)
		{
			CompanyCurrencyTable.Visible = true;
			CurrencyTable.Visible = false;
			this.CurrencyRepeater.Visible = false;
			CurInfo1.Text = String.Format(Root.rm.GetString("Curtxt8"), DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGPRODUCTS"));

			DropCompanyCurrency.DataTextField = "Currency";
			DropCompanyCurrency.DataValueField = "id";
			DropCompanyCurrency.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT ID,CURRENCY FROM CURRENCYTABLE WHERE CURRENCY<>'{0}'", DatabaseConnection.FilterInjection(CurrencySymbol.Text)));
			DropCompanyCurrency.DataBind();
			DropCompanyCurrency.Items.Insert(0, new ListItem(Root.rm.GetString("Curtxt9"), "-1"));
			DropCompanyCurrency.SelectedIndex = 0;
		}

		private void NewCurrSubmit_Click(object sender, EventArgs e)
		{
			if (DropCompanyCurrency.SelectedValue != "-1")
			{
				DatabaseConnection.DoCommand("UPDATE CURRENCYTABLE SET CHANGETOEURO=0, CHANGEFROMEURO=0 WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");
				DatabaseConnection.DoCommand("UPDATE CURRENCYTABLE SET CHANGETOEURO=1, CHANGEFROMEURO=1 WHERE ID=" + int.Parse(DropCompanyCurrency.SelectedValue));
				Start();
			}
			else
			{
				if (NewCurrName.Text.Length <= 0 || NewCurrSymbol.Text.Length <= 0)
					CurInfo2.Text =Root.rm.GetString("Curtxt11");
				else
				{
					DatabaseConnection.DoCommand("UPDATE CURRENCYTABLE SET CHANGETOEURO=0, CHANGEFROMEURO=0 WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");
					DatabaseConnection.DoCommand(String.Format("INSERT INTO CURRENCYTABLE(CURRENCY, CHANGETOEURO, CHANGEFROMEURO, CURRENCYSYMBOL) VALUES( '{0}', 1, 1, '{2}')", DatabaseConnection.FilterInjection(NewCurrName.Text), DatabaseConnection.FilterInjection(NewCurrSymbol.Text)));
					Start();
				}
			}
		}
	}
}
