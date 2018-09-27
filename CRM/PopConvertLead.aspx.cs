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

namespace Digita.Tustena.CRM
{
	public partial class PopConvertLead : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
                ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>window.opener.location.href=window.opener.location.href;self.close();</script>");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					int id = int.Parse(Request["id"]);
					Session["Ltocovert"]=id;
					DataRow drLead=DatabaseConnection.CreateDataset("SELECT COMPANYNAME,SURNAME FROM CRM_LEADS WHERE ID=" + id).Tables[0].Rows[0];
					if(drLead[0].ToString().Length > 0)
						RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Ledtxt30"),"0"));

					if(drLead[1].ToString().Length > 0 && drLead[1].ToString().ToLower()!="n/a")
					{
						RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Ledtxt31"),"1"));
						RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Ledtxt32"),"2"));
						RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Ledtxt33"),"3"));
						SearchTable.Visible=true;
					}else
					{
						SearchTable.Visible=false;
					}
					RadioButtonList1.SelectedIndex=0;
					BtnConvert.Text =Root.rm.GetString("Ledtxt34");
					BtnConvert.Attributes.Add("onclick","return CheckConvert();");
					FindCompany.Text =Root.rm.GetString("Find");
				}
			}
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.BtnConvert.Click += new EventHandler(this.btnConvert_Click);
			this.RepCompany.ItemDataBound += new RepeaterItemEventHandler(this.RepCompany_ItemDataBound);
			this.FindCompany.Click += new EventHandler(this.FindCompany_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void ConvertLead(string id, string type)
		{
			bool isNewCompany = ((DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM CRM_LEADS WHERE ID=" + id)).Length > 0);
			Session.Remove("Ltocovert");
			DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CRM_LEADS WHERE ID=" + id).Tables[0].Rows[0];
			string NewCompanyID = String.Empty;
			string NewContactID = String.Empty;

			if (isNewCompany && (type=="0" || type=="1"))
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("OWNERID", dr["OwnerID"]);
					dg.Add("CREATEDBYID", UC.UserId);
					dg.Add("COMPANYNAME", dr["CompanyName"]);
					dg.Add("COMPANYNAMEFILTERED", StaticFunctions.FilterSearch(dr["CompanyName"].ToString()));
					dg.Add("PHONE", dr["Phone"]);
					dg.Add("FAX", dr["Fax"]);
					dg.Add("EMAIL", dr["email"].ToString().Trim(' '));

					dg.Add("INVOICINGADDRESS", dr["Address"]);
					dg.Add("INVOICINGCITY", dr["City"]);
					dg.Add("INVOICINGSTATEPROVINCE", dr["Province"]);
					dg.Add("INVOICINGSTATE", dr["State"]);
					dg.Add("INVOICINGZIPCODE", dr["ZIPCode"]);
					dg.Add("DESCRIPTION", dr["Notes"]);
					dg.Add("GROUPS", dr["Groups"]);
					dg.Add("FROMLEAD", id);
					dg.Add("COMMERCIALZONE",dr["CommercialZone"]);

					string industry = DatabaseConnection.SqlScalar("SELECT INDUSTRY FROM CRM_CROSSLEAD WHERE LEADID=" + id);
					if (industry.Length > 0)
						dg.Add("COMPANYTYPEID", industry);

					object newId = dg.Execute("BASE_COMPANIES", DigiDapter.Identities.Identity);

					NewCompanyID = newId.ToString();
				}
			}

			if(type=="1" || type=="2" || type=="3")
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("NAME", dr["Name"]);
					dg.Add("SURNAME", dr["Surname"]);
					dg.Add("OWNERID", dr["OwnerID"]);
					dg.Add("TITLE", dr["Title"]);
					dg.Add("ADDRESS_1", dr["Address"]);
					dg.Add("CITY_1", dr["City"]);
					dg.Add("PROVINCE_1", dr["Province"]);
					dg.Add("STATE_1", dr["State"]);
					dg.Add("ZIPCODE_1", dr["ZIPCode"]);
					dg.Add("VATID", dr["VatID"]);
					dg.Add("TAXIDENTIFICATIONNUMBER", dr["TaxIdentificationNumber"]);
					dg.Add("EMAIL", dr["Email"].ToString().Trim(' '));
					dg.Add("PHONE_1", dr["Phone"]);
					dg.Add("FAX", dr["Fax"]);
					dg.Add("MOBILEPHONE_1", dr["MobilePhone"]);
					dg.Add("BUSINESSROLE", dr["BusinessRole"]);
					dg.Add("NOTES", dr["Notes"]);
					dg.Add("GROUPS", dr["Groups"]);
					dg.Add("COMMERCIALZONE",dr["CommercialZone"]);

					if(type=="1" || type=="3")
					{
						switch(type)
						{
							case "1":
								if (isNewCompany)
									dg.Add("COMPANYID", NewCompanyID);
								else
									dg.Add("COMPANYID", dr["CompanyID"]);
								break;
							case "3":
								if(TextboxSearchCompanyID.Text.Length>0)
									dg.Add("COMPANYID", TextboxSearchCompanyID.Text);
								break;
						}
					}
					dg.Add("BIRTHDAY", dr["BirthDay"]);
					dg.Add("BIRTHPLACE", dr["BirthPlace"]);
					dg.Add("CATEGORIES", dr["Categories"]);
					dg.Add("FROMLEAD", id);

					object newId = dg.Execute("BASE_CONTACTS", DigiDapter.Identities.Identity);

					NewContactID = newId.ToString();
				}
			}
			DatabaseConnection.DoCommand("UPDATE CRM_LEADS SET LIMBO=1 WHERE ID=" + id);
			if (isNewCompany && (type=="0" || type=="1"))
				DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET COMPANYID=" + NewCompanyID + ",LEADID=NULL WHERE LEADID=" + id);
			else
				DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET REFERRERID=" + NewContactID + ",LEADID=NULL WHERE LEADID=" + id);

			if (isNewCompany && (type=="0" || type=="1"))
			{
				DatabaseConnection.DoCommand("UPDATE CRM_OPPORTUNITYCONTACT SET CONTACTID=" + NewCompanyID + ", CONTACTTYPE=0 WHERE CONTACTTYPE=1 AND CONTACTID=" + id);
				DatabaseConnection.DoCommand("UPDATE CRM_CROSSOPPORTUNITY SET CONTACTID=" + NewCompanyID + ", CONTACTTYPE=0 WHERE CONTACTTYPE=1 AND CONTACTID=" + id);
				Session["fromlead"] = NewCompanyID;

				ClientScript.RegisterStartupScript(this.GetType(), "redir","<script>parent.location.href='/CRM/crm_companies.aspx?m=25&dgb=1&si=29';</script>");
			}
			else
			{

				ClientScript.RegisterStartupScript(this.GetType(), "redir","<script>parent.location.href='/CRM/base_contacts.aspx?m=25&si=31&action=VIEW&full=" + NewContactID +"';</script>");
			}

		}

		private void btnConvert_Click(object sender, EventArgs e)
		{
			ConvertLead(Session["Ltocovert"].ToString(),RadioButtonList1.SelectedValue);
		}

		private void RepCompany_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					HtmlContainerControl Company = (HtmlContainerControl) e.Item.FindControl("CompanyRep");
					Company.InnerHtml = (string) DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName");
					if (Company.InnerHtml.Length > 40)
					{
						Regex rx = new Regex(@"(?s)\b.{1,39}\b");
						Company.Attributes.Add("title", Company.InnerHtml);
						Company.InnerHtml = rx.Match(Company.InnerHtml) + "&hellip;";
						Company.Attributes.Add("style", "cursor:help;");
					}

					string setRef = "SetCompany('" + G.ParseJSString(Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyName"))) +
						"','" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "ID")) +
						"')";

					Company.Attributes.Add("onclick", setRef);

					break;
			}
		}

		private void FindCompany_Click(object sender, EventArgs e)
		{
			string fullText = "%";

			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT BASE_COMPANIES.ID,BASE_COMPANIES.COMPANYNAME FROM BASE_COMPANIES ");
			sb.AppendFormat("WHERE (LIMBO=0 AND ({1})) AND (BASE_COMPANIES.COMPANYNAME LIKE '{0}%' OR BASE_COMPANIES.PHONE LIKE '{0}%' OR BASE_COMPANIES.EMAIL LIKE '{0}%') ORDER BY BASE_COMPANIES.COMPANYNAME", fullText + DatabaseConnection.FilterInjection(TextboxSearchCompany.Text), GroupsSecure("BASE_COMPANIES.GROUPS"));

			RepCompany.DataSource = DatabaseConnection.CreateDataset(sb.ToString());
			RepCompany.DataBind();
		}
	}
}
