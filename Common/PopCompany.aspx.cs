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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class PopAziende : G
	{

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>window.opener.location.href=window.opener.location.href;self.close();</script>");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					RadioList1.Items.Add(new ListItem(Root.rm.GetString("Paztxt4")));
					RadioList1.Items.Add(new ListItem(Root.rm.GetString("Paztxt5")));
					RadioList1.Items[0].Selected = true;
					NRes.RepeatDirection = RepeatDirection.Horizontal;
					NRes.RepeatColumns = 2;
					ReSearchAdvanced.Visible = false;
					SetFocus(FindIt);
				}

				string js;
				string control = Request.QueryString["textbox"].ToString();
				string control2 = (Request.QueryString["textbox2"] != null) ? Request.QueryString["textbox2"].ToString() : "";
				string ind = (Request.QueryString["ind"] != null) ? Request.QueryString["ind"].ToString() : "";
				string cit = (Request.QueryString["cit"] != null) ? Request.QueryString["cit"].ToString() : "";
				string prov = (Request.QueryString["prov"] != null) ? Request.QueryString["prov"].ToString() : "";
				string cap = (Request.QueryString["cap"] != null) ? Request.QueryString["cap"].ToString() : "";
				string tel = (Request.QueryString["tel"] != null) ? Request.QueryString["tel"].ToString() : "";

				string clickControl=null;
				string eventFunction=null;
				if(Request.QueryString["click"]!=null)
					clickControl = Request.QueryString["click"].ToString();
				if(Request.QueryString["event"]!=null)
					eventFunction = Request.QueryString["event"].ToString();

				js = "<script>" + Environment.NewLine;
				js += "function SetRef(tx,az,ind,cit,prov,cap,tel,email){" + Environment.NewLine;
				if (Request.QueryString["email"] != null)
					js += "dynaret('" + control + "').value=email;" + Environment.NewLine;
				else
					js += "dynaret('" + control + "').value=tx;" + Environment.NewLine;

				if (control2.Length > 0) js += "	dynaret('" + control2 + "').value=az;" + Environment.NewLine;
				if (ind.Length > 0) js += "	dynaret('" + ind + "').value=ind;" + Environment.NewLine;
				if (cit.Length > 0) js += "	dynaret('" + cit + "').value=cit;" + Environment.NewLine;
				if (prov.Length > 0) js += "	dynaret('" + prov + "').value=prov;" + Environment.NewLine;
				if (cap.Length > 0) js += "	dynaret('" + cap + "').value=cap;" + Environment.NewLine;
				if (tel.Length > 0) js += "	dynaret('" + tel + "').value=tel;" + Environment.NewLine;
				js += "window.onload=null;self.close();"+ Environment.NewLine;
				if(clickControl!=null)
					js += "clickElement(dynaret('" + clickControl + "'));"+ Environment.NewLine;
				if(eventFunction!=null)
					js += "dynaevent('"+eventFunction+"');"+ Environment.NewLine;
				js +="parent.HideBox();" + Environment.NewLine;
				if (Request.QueryString["change"] != null) js += "	parent.PopChange('" + control2 + "');" + Environment.NewLine;

				js += "}</script>" + Environment.NewLine;
				ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", js);
				BtnFind.Text =Root.rm.GetString("Prftxt5");
				NewCompany.Text =Root.rm.GetString("Paztxt2");
				ViewAdvanced.Text =Root.rm.GetString("Paztxt9");
				NewCompanyTable.Visible = false;
				if (Request.QueryString["current"] != null)
					if (Request.QueryString["current"].Length > 0)
						Findcompany(DatabaseConnection.FilterInjection(Request.QueryString["current"].ToString()), true);
			}
		}

		public void New_Click(object sender, EventArgs e)
		{
			NewCompanyTable.Visible = true;
			CompaniesRep.Visible = false;
		}


		public void RapSubmit_Click(Object sender, EventArgs e)
		{
			if (RapRagSoc.Text.Length > 0)
			{
				using (DigiDapter dg = new DigiDapter())
				{

					dg.InsertOnly();
					dg.Add("CREATEDBYID", UC.UserId, 'I');
					dg.Add("COMPANYNAME", RapRagSoc.Text);
					dg.Add("COMPANYNAMEFILTERED", StaticFunctions.FilterSearch(RapRagSoc.Text));
					dg.Add("PHONE", RapPhone.Text);
					dg.Add("EMAIL", RapEmail.Text);
					dg.Add("OWNERID", UC.UserId);
					dg.Add("GROUPS", "|" + UC.AdminGroupId + "|" + UC.UserGroupId + "|");
					object newId = dg.Execute("Base_Companies", DigiDapter.Identities.Identity);
					string setRef = "SetRef('" + G.ParseJSString(RapRagSoc.Text) +
						"','" + newId.ToString() +
						"','','','',''," +
						"'" + G.ParseJSString(RapPhone.Text) +
						"','" + G.ParseJSString(RapEmail.Text) + "');";

					ClientScript.RegisterStartupScript(this.GetType(), "start", "<script>" + setRef + "</script>");
				}
			}
		}

		public void Find_Click(object sender, EventArgs e)
		{
			Findcompany(DatabaseConnection.FilterInjection(FindIt.Text));
		}

		private void Findcompany(string name)
		{
			Findcompany(name, false);
		}

		private void Findcompany(string name, bool full)
		{
			string searchfiltered = StaticFunctions.FilterSearch(name);
			string fullText = String.Empty;
			if (RadioList1.Items[1].Selected || full) fullText = "%";
			StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TOP {0} BASE_COMPANIES.ID,BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.INVOICINGADDRESS, BASE_COMPANIES.INVOICINGCITY, BASE_COMPANIES.INVOICINGSTATEPROVINCE, BASE_COMPANIES.INVOICINGZIPCODE, BASE_COMPANIES.PHONE, BASE_COMPANIES.EMAIL,BASE_COMPANIES.COMMERCIALZONE, BASE_COMPANIES.SALESPERSONID, BASE_COMPANIES.OWNERID FROM BASE_COMPANIES ", NRes.SelectedValue);
            sb.AppendFormat("WHERE (LIMBO=0 AND ({1})) AND (BASE_COMPANIES.COMPANYNAME LIKE '{0}%' OR BASE_COMPANIES.PHONE LIKE '{0}%' OR BASE_COMPANIES.EMAIL LIKE '{0}%') ", fullText + name, GroupsSecure ("BASE_COMPANIES.GROUPS"));
            if (UC.Zones.Length>0)
				sb.AppendFormat(" AND ({0})", ZoneSecure("BASE_COMPANIES.COMMERCIALZONE"));

			sb.Append("ORDER BY BASE_COMPANIES.COMPANYNAME");
			CompaniesRep.DataSource = DatabaseConnection.CreateDataset(sb.ToString());
			CompaniesRep.DataBind();
		}

		public void CompaniesDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
                    string salesperson = (DataBinder.Eval((DataRowView)e.Item.DataItem, "SalesPersonID")).ToString();
                    string ownerid = (DataBinder.Eval((DataRowView)e.Item.DataItem, "OwnerID")).ToString();
                    if (salesperson.Length > 0 && salesperson != UC.UserId.ToString() && ownerid != UC.UserId.ToString() && UC.AdminGroupId != UC.UserGroupId)
                    {
                        e.Item.Visible = false;
                    }
                    else
                    {

                        HtmlContainerControl Company = (HtmlContainerControl)e.Item.FindControl("CompanyRep");
                        Company.InnerHtml = (string)DataBinder.Eval((DataRowView)e.Item.DataItem, "CompanyName");
                        if (Company.InnerHtml.Length > 40)
                        {
                            Regex r = new Regex(@"(?s)\b.{1,39}\b");
                            Company.Attributes.Add("title", Company.InnerHtml);
                            Company.InnerHtml = r.Match(Company.InnerHtml) + "&hellip;";
                            Company.Attributes.Add("style", "cursor:help;");
                        }

                        string setRef = "SetRef('" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "CompanyName"))) +
                            "','" + Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "ID")) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "InvoicingAddress"))) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "InvoicingCity"))) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "InvoicingStateProvince"))) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "InvoicingZIPCode"))) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "Phone"))) +
                            "','" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "Email"))) + "')";

                        Company.Attributes.Add("onclick", setRef);
                    }
					break;
			}
		}

		public void ViewAdvanced_click(object sender, EventArgs e)
		{
			PrepareAdvanced();
		}

		public void btn_Click(object sender, EventArgs e)
		{
			StringBuilder sqlMaster = new StringBuilder();
			StringBuilder sqlString = new StringBuilder();
			bool avBool = false;
            sqlMaster.Append("SELECT BASE_COMPANIES.ID,BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.INVOICINGADDRESS, BASE_COMPANIES.INVOICINGCITY, BASE_COMPANIES.INVOICINGSTATEPROVINCE, BASE_COMPANIES.INVOICINGZIPCODE, BASE_COMPANIES.PHONE, BASE_COMPANIES.EMAIL, BASE_COMPANIES.SALESPERSONID, BASE_COMPANIES.OWNERID FROM BASE_COMPANIES ");

			sqlMaster.Append("LEFT OUTER JOIN ACCOUNT ON BASE_COMPANIES.OWNERID = ACCOUNT.UID ");
			sqlMaster.AppendFormat("WHERE LIMBO=0 ");

			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_CompanyName")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.COMPANYNAME LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_CompanyName")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Address")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.INVOICINGADDRESS LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTADDRESS LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEADDRESS LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Address")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_City")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.INVOICINGCITY LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTCITY LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSECITY LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_City")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_State")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.INVOICINGSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSESTATEPROVINCE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_State")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Zip")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEZIPCODE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Zip")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Zip")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEZIPCODE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Zip")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Phone")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTPHONE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEPHONE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Phone")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Fax")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.FAX LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTFAX LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEFAX LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Fax")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Email")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.EMAIL LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTEMAIL LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEEMAIL LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Email")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Site")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.WEBSITE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Site")).Text);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Code")).Text.Length > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.COMPANYCODE LIKE '%{0}%') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Code")).Text);
				avBool = true;
			}
			if (((DropDownList) ReSearchAdvanced.FindControl("Advanced_CompanyType")).SelectedIndex > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.COMPANYTYPEID = '{0}') AND ", ((DropDownList) ReSearchAdvanced.FindControl("Advanced_CompanyType")).SelectedValue);
				avBool = true;
			}
			if (((DropDownList) ReSearchAdvanced.FindControl("Advanced_ContactType")).SelectedIndex > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.CONTACTTYPEID = '{0}') AND ", ((DropDownList) ReSearchAdvanced.FindControl("Advanced_ContactType")).SelectedValue);
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Billed")).Text.Length > 0)
			{
				RadioButtonList Advanced_BillCheck = (RadioButtonList) ReSearchAdvanced.FindControl("Advanced_BillCheck");
				switch (Advanced_BillCheck.SelectedValue)
				{
					case "1":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED = '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Billed")).Text);
						break;
					case "2":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED < '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Billed")).Text);
						break;
					case "3":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED > '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Billed")).Text);
						break;
				}
				avBool = true;
			}
			if (((TextBox) ReSearchAdvanced.FindControl("Advanced_Employees")).Text.Length > 0)
			{
				RadioButtonList Advanced_EmployeesCheck = (RadioButtonList) ReSearchAdvanced.FindControl("Advanced_EmployeesCheck");
				switch (Advanced_EmployeesCheck.SelectedValue)
				{
					case "1":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED = '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Employees")).Text);
						break;
					case "2":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED < '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Employees")).Text);
						break;
					case "3":
						sqlString.AppendFormat("(BASE_COMPANIES.BILLED > '{0}') AND ", ((TextBox) ReSearchAdvanced.FindControl("Advanced_Employees")).Text);
						break;
				}
				avBool = true;
			}
			if (((DropDownList) ReSearchAdvanced.FindControl("Advanced_Estimate")).SelectedIndex > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.ESTIMATE = {0}) AND ", ((DropDownList) ReSearchAdvanced.FindControl("Advanced_Estimate")).SelectedValue);
				avBool = true;
			}
			if (((DropDownList) ReSearchAdvanced.FindControl("Advanced_Category")).SelectedIndex > 0)
			{
				sqlString.AppendFormat("(BASE_COMPANIES.CATEGORIES LIKE '%|{0}|%') AND ", ((DropDownList) ReSearchAdvanced.FindControl("Advanced_Category")).SelectedValue);
				avBool = true;
			}

			string queryGroup = GroupsSecure("BASE_COMPANIES.GROUPS");

            if (queryGroup.Length > 0)
                sqlString.AppendFormat("({0}) AND ", queryGroup);

			if(UC.Zones.Length>0)
				sqlString.AppendFormat(" ({0}) AND ", ZoneSecure("BASE_COMPANIES.COMMERCIALZONE"));

			if (sqlString.ToString().Length > 0)
			{
				string sqlAv = sqlString.ToString();
				if (avBool)
				{
					sqlAv = sqlAv.Substring(0, sqlAv.Length - 4);
				}
				CompaniesRep.DataSource = DatabaseConnection.CreateDataset(sqlMaster.ToString() + " AND (" + sqlAv + ")" + " ORDER BY BASE_COMPANIES.COMPANYNAME");
			}
			else
			{
				CompaniesRep.DataSource = DatabaseConnection.CreateDataset(sqlMaster.ToString() + " ORDER BY BASE_COMPANIES.COMPANYNAME");
			}


			CompaniesRep.DataBind();

			if (CompaniesRep.Items.Count > 0)
			{
				CompaniesRep.Visible = true;
				ReSearchAdvanced.Visible = false;
				ReSearchSimple.Visible = true;
			}
			else
			{
				CompaniesRep.Visible = false;
			}

		}

		private void PrepareAdvanced()
		{
			DropDownList Advanced_CompanyType = (DropDownList) ReSearchAdvanced.FindControl("Advanced_CompanyType");
			DropDownList Advanced_ContactType = (DropDownList) ReSearchAdvanced.FindControl("Advanced_ContactType");
			DropDownList Advanced_Estimate = (DropDownList) ReSearchAdvanced.FindControl("Advanced_Estimate");
			DropDownList Advanced_Category = (DropDownList) ReSearchAdvanced.FindControl("Advanced_Category");

			Fill_Sectors(Advanced_CompanyType);
			Fill_ContactType(Advanced_ContactType);
			Fill_Evaluation(Advanced_Estimate);

			Advanced_Category.DataTextField = "Description";
			Advanced_Category.DataValueField = "id";
			Advanced_Category.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
			Advanced_Category.DataBind();
			Advanced_Category.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			Advanced_Category.Items[0].Value = "0";

			CompaniesRep.Visible = false;
			ReSearchSimple.Visible = false;
			ReSearchAdvanced.Visible = true;

		}

		private void Fill_Sectors(DropDownList st)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY DESCRIPTION");
			st.DataSource = ds;
			st.DataTextField = "Description";
			st.DataValueField = "K_ID";
			st.DataBind();
			st.Items.Insert(0,Root.rm.GetString("CRMcontxt13"));
			st.Items[0].Value = "0";
		}

		private void Fill_ContactType(DropDownList ct)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,CONTACTTYPE FROM CONTACTTYPE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY CONTACTTYPE");
			ct.DataSource = ds;
			ct.DataTextField = "ContactType";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0,Root.rm.GetString("CRMcontxt14"));
			ct.Items[0].Value = "0";
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
					this.BtnFind.Click += new EventHandler(this.Find_Click);
			this.ViewAdvanced.Click += new EventHandler(this.ViewAdvanced_click);
			this.NewCompany.Click += new EventHandler(this.New_Click);
			this.RapSubmit.Click += new EventHandler(this.RapSubmit_Click);
			this.SearchAdvanced.Click += new EventHandler(this.btn_Click);
			this.CompaniesRep.ItemDataBound += new RepeaterItemEventHandler(this.CompaniesDataBound);
		}

		#endregion

		private void Fill_Evaluation(DropDownList ct)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,ESTIMATE FROM CONTACTESTIMATE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY FIELDORDER");
			ct.DataSource = ds;
			ct.DataTextField = "Estimate";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0,Root.rm.GetString("CRMcontxt15"));
			ct.Items[0].Value = "0";
		}
	}
}
