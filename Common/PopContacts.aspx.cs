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
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using System.Data;
using System.Web.UI;

namespace Digita.Tustena
{
	public partial class PopContacts : G
	{



			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

        private void InitializeComponent()
        {
            this.Load += new EventHandler(this.Page_Load);
            this.Find.Click += new EventHandler(this.Find_Click);
            this.NewRef.Click += new EventHandler(this.Find_Click);
            this.RapSubmit.Click += new EventHandler(this.RapSubmit_Click);
            this.ContactReferrer.ItemDataBound += new RepeaterItemEventHandler(ContactReferrer_ItemDataBound);

        }

        void ContactReferrer_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    break;
            }
        }
		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>window.opener.location.href=window.opener.location.href;self.close();</script>";
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

                    if (Request.QueryString["Mode"] != null)
                    {
                        int Mode = Convert.ToInt32(Request.QueryString["Mode"]);

                        if (Mode == 1)
                        {
                            CheckFunctions.Visible = true;
                        }
                        else
                        {
                            CheckLeads.Checked = false;
                        }
                    }



				}
                Titolo.Text = Root.rm.GetString("Paztxt12");
				NewReferrer.Visible = false;
				NewRef.Text =Root.rm.GetString("Prftxt11");
				RapSubmit.Text =Root.rm.GetString("Prftxt12");
				string js;
				string control = Request.QueryString["textbox"].ToString();
				if (Request.QueryString["CompanyID"] != null)
				{
					if (Request.QueryString["CompanyID"].ToString().Length > 0)
						FindCompany(int.Parse(Request.QueryString["CompanyID"]));
				}
				string control2 = (Request.QueryString["textbox2"] != null) ? Request.QueryString["textbox2"].ToString() : "";
				string control3 = (Request.QueryString["textboxID"] != null) ? Request.QueryString["textboxID"].ToString() : "";
				string control4 = (Request.QueryString["textboxCompanyID"] != null) ? Request.QueryString["textboxCompanyID"].ToString() : "";

				string clickControl=null;
				string eventFunction=null;
				if(Request.QueryString["click"]!=null)
					clickControl = Request.QueryString["click"].ToString();
				if(Request.QueryString["event"]!=null)
					eventFunction = Request.QueryString["event"].ToString();

				js = "<script>" + Environment.NewLine;
				js += "function SetRef(tx,az,id,cid,email){" + Environment.NewLine;

				if(Request.QueryString["email"] != null)
					js += "dynaret('" + control + "').value=email;" + Environment.NewLine;
				else
					js += "dynaret('" + control + "').value=tx;" + Environment.NewLine;


				if (control2.Length > 0) js += "dynaret('" + control2 + "').value=az;" + Environment.NewLine;
				if (control3.Length > 0) js += "dynaret('" + control3 + "').value=id;" + Environment.NewLine;
				if (control4.Length > 0) js += "dynaret('" + control4 + "').value=cid;" + Environment.NewLine;
				js += "	window.onload=null;self.close();" + Environment.NewLine;
				if(clickControl!=null)
					js += "clickElement(dynaret('" + clickControl + "'));"+ Environment.NewLine;
				if(eventFunction!=null)
					js += "dynaevent('"+eventFunction+"');"+ Environment.NewLine;
				js += "	parent.HideBox();" + Environment.NewLine;
				if (Request.QueryString["change"] != null) js += "	parent.PopChange('" + control3 + "');" + Environment.NewLine;

				js += "}</script>" + Environment.NewLine;

				ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", js);
				Find.Text =Root.rm.GetString("Prftxt5");
			}
		}

		private void FindCompany(int cID)
		{
			string sqlString = "SELECT BASE_CONTACTS.ID,(BASE_CONTACTS.SURNAME+' '+ISNULL(BASE_CONTACTS.NAME,'')) AS REFERENTE,BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.ID AS COMPANYID, BASE_CONTACTS.EMAIL, BASE_CONTACTS.SALESPERSONID, BASE_CONTACTS.OWNERID FROM BASE_CONTACTS ";
				sqlString+= "LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID WHERE BASE_CONTACTS.LIMBO=0 AND (BASE_CONTACTS.COMPANYID=" + cID + ") ";
			string queryGroup = GroupsSecure("BASE_CONTACTS.GROUPS",UC);
			if (queryGroup.Length > 0)
			{
				sqlString+=String.Format(" AND ({0})", queryGroup);
			}
			if(UC.Zones.Length>0)
				sqlString+=String.Format(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE",UC));

			sqlString+= " ORDER BY REFERENTE";
			ContactReferrer.DataSource = DatabaseConnection.CreateDataset(sqlString);
			ContactReferrer.DataBind();
		}

		public void Find_Click(object sender, EventArgs e)
		{
            switch (((LinkButton)sender).ID)
            {
                case "Find":
                    string sqlString, sqlString2;
                    string top = String.Empty;
                    string fullText = String.Empty;
                    if (RadioList1.Items[1].Selected) fullText = "%";
                    top = "top " + NRes.SelectedValue;


                    if (CheckLeads.Checked && CheckContacts.Checked)
                    {
                        sqlString = "SELECT " + top + " BASE_CONTACTS.ID,(BASE_CONTACTS.SURNAME+' '+ISNULL(BASE_CONTACTS.NAME,'')) AS REFERENTE,BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.ID AS COMPANYID, BASE_CONTACTS.EMAIL, BASE_CONTACTS.SALESPERSONID, BASE_CONTACTS.OWNERID FROM BASE_CONTACTS ";
						sqlString+= "LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID WHERE BASE_CONTACTS.LIMBO=0 AND (" + GroupsSecure("BASE_CONTACTS.GROUPS") + ") AND (BASE_CONTACTS.NAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.SURNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.PHONE_1 LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.MOBILEPHONE_1 LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ";

                        sqlString2 = "SELECT " + top + " CRM_LEADS.ID,(CRM_LEADS.SURNAME+' '+ISNULL(CRM_LEADS.NAME,'')) AS REFERENTE,CRM_LEADS.COMPANYNAME AS COMPANYNAME, CRM_LEADS.COMPANYID, CRM_LEADS.EMAIL, CRM_CROSSLEAD.SALESPERSON AS SALESPERSONID, CRM_LEADS.OWNERID FROM CRM_LEADS INNER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID=CRM_CROSSLEAD.LEADID ";
					    sqlString2 += "WHERE (CRM_LEADS.LIMBO=0 AND ACTIVE=1 AND ((" + GroupsSecure("CRM_LEADS.GROUPS") + ") OR CRM_LEADS.OWNERID=" + UC.UserId + ")) AND (CRM_LEADS.NAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.SURNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.PHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.MOBILEPHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.COMPANYNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ";
                        sqlString += " ORDER BY REFERENTE";
                        sqlString2 += " ORDER BY REFERENTE";
                        if (UC.Zones.Length > 0)
                        {
                            sqlString += String.Format(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE", UC));
                            sqlString2 += String.Format(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE", UC));
                        }


                        DataTable contacts = DatabaseConnection.CreateDataset(sqlString).Tables[0];
                        DataTable leads = DatabaseConnection.CreateDataset(sqlString2).Tables[0];

                        DataTable FinalTb = DataManipulation.Union(contacts, leads);
                        ContactReferrer.DataSource = FinalTb;
                        ContactReferrer.DataBind();

                    }
                    else if (CheckLeads.Checked)
                    {
                        sqlString = "SELECT " + top + " CRM_LEADS.ID,(CRM_LEADS.SURNAME+' '+ISNULL(CRM_LEADS.NAME,'')) AS REFERENTE,CRM_LEADS.COMPANYNAME, CRM_LEADS.COMPANYID, CRM_LEADS.EMAIL, CRM_CROSSLEAD.SALESPERSON AS SALESPERSONID, CRM_LEADS.OWNERID FROM CRM_LEADS INNER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID=CRM_CROSSLEAD.LEADID ";
					sqlString += "WHERE (CRM_LEADS.LIMBO=0 AND ACTIVE=1 AND ((" + GroupsSecure("CRM_LEADS.GROUPS") + ") OR CRM_LEADS.OWNERID=" + UC.UserId + ")) AND (CRM_LEADS.NAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.SURNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.PHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.MOBILEPHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.COMPANYNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ";
                        if (UC.Zones.Length > 0)
                        {
                            sqlString += String.Format(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE", UC));
                        }

                        ContactReferrer.DataSource = DatabaseConnection.CreateDataset(sqlString);
                        ContactReferrer.DataBind();
                    }
                    else if (CheckContacts.Checked)
                    {
                        sqlString = "SELECT " + top + " BASE_CONTACTS.ID,(BASE_CONTACTS.SURNAME+' '+ISNULL(BASE_CONTACTS.NAME,'')) AS REFERENTE,BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.ID AS COMPANYID, BASE_CONTACTS.EMAIL, BASE_CONTACTS.SALESPERSONID, BASE_CONTACTS.OWNERID FROM BASE_CONTACTS ";
						sqlString+= "LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID WHERE BASE_CONTACTS.LIMBO=0 AND (" + GroupsSecure("BASE_CONTACTS.GROUPS") + ") AND (BASE_CONTACTS.NAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.SURNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.PHONE_1 LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR BASE_CONTACTS.MOBILEPHONE_1 LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ";
                        if (UC.Zones.Length > 0)
                        {
                            sqlString += String.Format(" AND ({0})", ZoneSecure("BASE_CONTACTS.COMMERCIALZONE", UC));
                        }

                        ContactReferrer.DataSource = DatabaseConnection.CreateDataset(sqlString);
                        ContactReferrer.DataBind();
                    }
                    break;
                case "NewRef":
                    Find.Visible = false;
                    FindIt.Visible = false;
                    ContactReferrer.Visible = false;
                    NewReferrer.Visible = true;
                    break;
            }
		}

		public void RapSubmit_Click(object sender, EventArgs e)
		{
			object newId;
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OWNERID", UC.UserId);
				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("CREATEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("NAME", Name.Text);
				dg.Add("SURNAME", Surname.Text);
				dg.Add("GROUPS", "|" + UC.UserGroupId + "|");

				newId = dg.Execute("BASE_CONTACTS", "ID=-1", DigiDapter.Identities.Identity);
			}

			string js;
			string control = Request.QueryString["textbox"].ToString();
			string control3 = (Request.QueryString["textboxID"] != null) ? Request.QueryString["textboxID"].ToString() : "";
			js = "<script>";
			js += "	dynaret('" + control + "').value='" + Name.Text + " " + Surname.Text + "';";
			if (control3.Length > 0) js += "	dynaret('" + control3 + "').value=" + newId.ToString() + ";";
			js += "	window.onload=null;self.close();";
			js += "	parent.HideBox();";
			js += "</script>";
			SomeJS.Text = js;

		}

	}
}
