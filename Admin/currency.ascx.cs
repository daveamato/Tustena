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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Admin
{
	public partial class currency : UserControl
	{

		public G G = new G();
		private UserConfig UC = new UserConfig();
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];

			if (!Page.IsPostBack)
			{
				Start();
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
			currencyTable.Visible = true;

			currencySymbol.Text = DatabaseConnection.SqlScalar("SELECT CURRENCY FROM CURRENCYTABLE WHERE (CHANGETOEURO=1 AND CHANGEFROMEURO=1)");
			ChangeCurrency.Text =Root.rm.GetString("Curtxt7");

			companyCurrencyTable.Visible = false;
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
			this.ChangeCurrency.Click += new EventHandler(this.ChangeCurrency_Click);
			this.NewCurrSubmit.Click += new EventHandler(this.NewCurrSubmit_Click);
			this.CurrSubmit.Click += new EventHandler(this.CurrSubmit_Click);
			this.CurrencyRepeater.ItemDataBound += new RepeaterItemEventHandler(this.CurrencyRepeater_ItemDataBound);
			this.CurrencyRepeater.ItemCommand += new RepeaterCommandEventHandler(this.CurrencyRepeater_ItemCommand);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void CurrSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				int cId = int.Parse(CurrID.Text);
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("CURRENCY", CurrName.Text);
					dg.Add("CURRENCYSYMBOL", CurrSymbol.Text);
					dg.Add("CHANGETOEURO", StaticFunctions.FixDecimal(CurrChange1.Text));
					dg.Add("CHANGEFROMEURO", StaticFunctions.FixDecimal(CurrChange2.Text));
					dg.Execute("CURRENCYTABLE", "ID =" + cId);
				}
				CurrencyRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CURRENCYTABLE").Tables[0];
				CurrencyRepeater.DataBind();
				CancelForm();
				Start();
			}
			catch
			{
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Curtxt12") + "');</script>");
			}
		}

		private void CurrencyRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton edit = (LinkButton) e.Item.FindControl("edit");
					edit.Text =Root.rm.GetString("Modify");
					LinkButton delete = (LinkButton) e.Item.FindControl("Delete");
					delete.Text =Root.rm.GetString("Delete");
					delete.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Curtxt13") + "')");
					if ((double) DataBinder.Eval((DataRowView) e.Item.DataItem, "ChangeToEuro") == 1 && (double) DataBinder.Eval((DataRowView) e.Item.DataItem, "ChangeToEuro") == 1)
					{
						edit.Visible = false;
						delete.Visible = false;
					}
					break;
			}
		}

		private void CurrencyRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			int cId = int.Parse(((Literal) e.Item.FindControl("CurrencyID")).Text);
			switch (e.CommandName)
			{
				case "edit":
					DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CURRENCYTABLE WHERE ID=" + cId).Tables[0].Rows[0];
					CurrName.Text = dr["Currency"].ToString();
					CurrSymbol.Text = dr["CurrencySymbol"].ToString();
					CurrChange1.Text = dr["ChangeToEuro"].ToString();
					CurrChange2.Text = dr["ChangeFromEuro"].ToString();
					CurrID.Text = dr["id"].ToString();
					break;
				case "Delete":
					DatabaseConnection.DoCommand("DELETE FROM CURRENCYTABLE WHERE ID=" + cId);
					Start();
					break;
			}
		}

		private void ChangeCurrency_Click(object sender, EventArgs e)
		{
			companyCurrencyTable.Visible = true;
			currencyTable.Visible = false;
			this.CurrencyRepeater.Visible = false;
			CurrInfo.Text = String.Format(Root.rm.GetString("Curtxt8"), DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CATALOGPRODUCTS"));

			DropCompanyCurrency.DataTextField = "Currency";
			DropCompanyCurrency.DataValueField = "id";
			DropCompanyCurrency.DataSource = DatabaseConnection.CreateDataset("SELECT ID,CURRENCY FROM CURRENCYTABLE WHERE CURRENCY<>'" + DatabaseConnection.FilterInjection(currencySymbol.Text) + "'");
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
					CurrInfo2.Text =Root.rm.GetString("Curtxt11");
				else
				{
					DatabaseConnection.DoCommand("UPDATE CURRENCYTABLE SET CHANGETOEURO=0, CHANGEFROMEURO=0 WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");
					DatabaseConnection.DoCommand(String.Format("INSERT INTO CURRENCYTABLE(CURRENCY, CHANGETOEURO, CHANGEFROMEURO, CURRENCYSYMBOL) VALUES( '{0}', 1, 1, '{1}')", DatabaseConnection.FilterInjection(NewCurrName.Text), DatabaseConnection.FilterInjection(NewCurrSymbol.Text)));
					Start();
				}
			}
		}
	}
}
