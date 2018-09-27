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
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Ajax;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.ERP;
using Digita.Tustena.WebControls;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
    public partial class CRM_Companies : G //, ICallbackEventHandler
    {

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;
            if (visDocuments.Visible == true && !M.IsModule(ActiveModules.Storage))
                Tabber.HideTabs += visDocuments.ID;
            if (visOpportunity.Visible == true && !M.IsModule(ActiveModules.Lead))
                Tabber.HideTabs += visOpportunity.ID;
            if (visEstimate.Visible == true && !M.IsModule(ActiveModules.Sales))
                Tabber.HideTabs += visEstimate.ID+visProducts.ID;

            base.OnPreRenderComplete(e);
        }

        public void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                InitSearchListRepeater();

                if (Request["Ajax"] != null)
                    InitAjax();
                DeleteGoBack();
                GroupDescription.Text = " (" + G.GetGroupDescription(UC.UserGroupId) + ")";


                NewDoc.Text = Root.rm.GetString("CRMrubtxt4");
                NewContact.Text = Root.rm.GetString("Bcotxt31");

                activeMode.Value = String.Empty;

                AdvancedSearch.Visible = false;

                if(Tabber.Expand)
                    AcCrono.NoPaging();

                if (!Page.IsPostBack)
                {
                    FilldropZones();
                    NewCompany.Text = Root.rm.GetString("CRMcontxt22");
                    AcCrono.ParentID = 0;
                    AcCrono.OpportunityID = 0;
                    AcCrono.AcType = (byte)AType.Company;

                    NewActivityPhone.Text = Root.rm.GetString("Wortxt6");
                    NewActivityLetter.Text = Root.rm.GetString("Wortxt7");
                    NewActivityFax.Text = Root.rm.GetString("Wortxt8");
                    NewActivityMemo.Text = Root.rm.GetString("Wortxt9");
                    NewActivityEmail.Text = Root.rm.GetString("Wortxt10");
                    NewActivityVisit.Text = Root.rm.GetString("Wortxt11");
                    NewActivityGeneric.Text = Root.rm.GetString("Wortxt12");
                    NewActivitySolution.Text = Root.rm.GetString("Wortxt13");

                    Days.Items.Add(new ListItem(Root.rm.GetString("Das2txt7"), "0"));
                    Days.Items.Add(new ListItem("30 " + Root.rm.GetString("Das2txt8"), "30"));
                    Days.Items.Add(new ListItem("60 " + Root.rm.GetString("Das2txt8"), "60"));
                    Days.Items.Add(new ListItem("90 " + Root.rm.GetString("Das2txt8"), "90"));
                    Days.Items.Add(new ListItem("120 " + Root.rm.GetString("Das2txt8"), "120"));
                    Days.SelectedIndex = 0;

                    Session["listfromsearch"] = "0";
                    Fill_Sectors(ListSector);
                    Fill_ContactType(ListType);
                    Fill_Owners(ListOwners);


                    BtnSearch.Text = Root.rm.GetString("Bcotxt2");
                    RapSubmit.Text = Root.rm.GetString("CRMcontxt34");
                    RapSubmit.Attributes.Add("onclick", "return CheckRapidMail()");
                    CancelCon.Text = Root.rm.GetString("Bcotxt40");
                    SubmitCon.Text = Root.rm.GetString("CRMcontxt34");
                    SubmitRef.Text = Root.rm.GetString("CRMcontxt34");

                    BtnViewAdvanced.Text = Root.rm.GetString("CRMcontxt52");

                    visActivity.Header = Root.rm.GetString("Bcotxt45");
                    visOpportunity.Header = Root.rm.GetString("Bcotxt46");

                    visEstimate.Header = Root.rm.GetString("Menutxt63");
                    visDocuments.Header = Root.rm.GetString("CRMcontxt58");
                    visProducts.Header = Root.rm.GetString("CRMcontxt59");

                    SaveNewProduct.Text = Root.rm.GetString("Bcotxt4");
                    ResetNewProduct.Text = Root.rm.GetString("Bcotxt40");
                    InsertNewProduct.Text = Root.rm.GetString("CRMComptxt7");
                    NewProduct.Visible = false;

                    G.FillListGroups(UC, ListGroups);
                    ListCategory.DataTextField = "Description";
                    ListCategory.DataValueField = "id";
					ListCategory.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
                    ListCategory.DataBind();
                    ListCategory.Items.Insert(0, Root.rm.GetString("CRMcontxt53"));
                    ListCategory.Items[0].Value = "0";


                    ContactForm.Visible = false;
                    Tabber.Visible = false;
                    RapInfo.Text = String.Empty;

                    SrcBtn.Text = Root.rm.GetString("Find");


                    if (Request.Params["action"] != null)
                    {
                        if (Request.Params["action"] == "NEW")
                        {
                            PrepareForNewCompany();
                        }
                    }

                    if (Request.Params["gb"] != null && isGoBack)
                    {
                        Stack ba = new Stack();
                        ba = (Stack)Session["goback1"];
                        GoBack gb = new GoBack();
                        gb = (GoBack)ba.Pop(); //ba[ba.Count - 1];
                        string[] par = gb.parameter.Split('|');
                        if (par[1].Length > 0)
                        {
                            if (par[1] != "nn")
                                FillView(int.Parse(par[1]));
                        }
                        if (par[2].Length > 0)
                        {

                            switch (par[2])
                            {
                                case "c":
                                    Tabber.Selected = visReferrer.ID;
                                    break;
                                case "a":
                                    FillRepeaterActivity(int.Parse(par[1].ToString()));
                                    Tabber.Selected = visActivity.ID;
                                    break;
                                case "o":
                                    FillRepeaterOpportunity(int.Parse(par[1].ToString()));
                                    Tabber.Selected = visOpportunity.ID;
                                    break;

                                case "d":
                                    Tabber.Selected = visContact.ID;
                                    break;

                                case "f":
                                    Session["review"] = "0";
                                    FillFileRep(int.Parse(par[1].ToString()));
                                    Tabber.Selected = visDocuments.ID;
                                    break;
                                case "g":
                                    Tabber.Selected = visProducts.ID;
                                    break;
                                default:
                                    Tabber.Selected = visContact.ID;
                                    break;
                            }

                        }
                        Session["goback1"] = ba;
                    }

                    if (Request.Form["searchcontact"] != null)
                    {
                        Search.Text = Request.Form["searchcontact"];
                        FillFromSearch();
                    }


                    if (Session["fromlead"] != null)
                    {
                        FillView(int.Parse(Session["fromlead"].ToString()));
                        Session.Remove("fromlead");
                    }

                    if (Session["openId"] != null)
                    {
                        FillView(int.Parse(Session["openId"].ToString()));
                        Session.Remove("openId");
                    }

                    FillSearchListRepeater(false, string.Empty);
                }

            }
        }



        private void FilldropZones()
        {
            dropZones.DataValueField = "id";
            dropZones.DataTextField = "description";
			dropZones.DataSource=DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM ZONES ORDER BY VIEWORDER");
            dropZones.DataBind();
            dropZones.Items.Insert(0, new ListItem(Root.rm.GetString("Choose"), "0"));
        }


        private void FillForm(int id)
        {
            Fill_Sectors(Sector);
            Fill_ContactType(ContactType);
            Fill_Evaluation(Evaluation);
            DataSet dsCompanies;
            dsCompanies = DatabaseConnection.CreateDataset("SELECT * FROM BASE_COMPANIES WHERE ID = " + id);

            DataRow drCompanies = dsCompanies.Tables[0].Rows[0];
            ContactId.Text = id.ToString();
            CompanyName.Text = drCompanies["CompanyName"].ToString();
            CompanyCode.Text = drCompanies["CompanyCode"].ToString();
            VatId.Text = drCompanies["vatId"].ToString();
            Phone.Text = drCompanies["Phone"].ToString();
            Fax.Text = drCompanies["Fax"].ToString();
            Email.Text = drCompanies["EMail"].ToString();
            OwnerId.Text = drCompanies["OwnerID"].ToString();
            OwnerName.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM ACCOUNT WHERE UID=" + drCompanies["OwnerID"].ToString());
            if (drCompanies["SalesPersonID"] != System.DBNull.Value && drCompanies["SalesPersonID"].ToString().Length > 0)
            {
                SalesPersonID.Text = drCompanies["SalesPersonID"].ToString();
                SalesPerson.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM ACCOUNT WHERE UID=" + drCompanies["SalesPersonID"].ToString());
            }
            WebSite.Text = drCompanies["WebSite"].ToString();
            Invoice_Address.Text = drCompanies["InvoicingAddress"].ToString();
            Invoice_City.Text = drCompanies["InvoicingCity"].ToString();
            Invoice_StateProvince.Text = drCompanies["InvoicingStateProvince"].ToString();
            Invoice_State.Text = drCompanies["InvoicingState"].ToString();
            Invoice_Zip.Text = drCompanies["InvoicingZIPCode"].ToString();
            Shipment_Address.Text = drCompanies["ShipmentAddress"].ToString();
            Shipment_City.Text = drCompanies["ShipmentCity"].ToString();
            Shipment_StateProvince.Text = drCompanies["ShipmentStateProvince"].ToString();
            Shipment_State.Text = drCompanies["ShipmentState"].ToString();
            Shipment_Zip.Text = drCompanies["ShipmentZIPCode"].ToString();
            Shipment_Phone.Text = drCompanies["ShipmentPhone"].ToString();
            Shipment_Fax.Text = drCompanies["ShipmentFax"].ToString();
            Shipment_Email.Text = drCompanies["ShipmentEMail"].ToString();
            Warehouse_Address.Text = drCompanies["WarehouseAddress"].ToString();
            Warehouse_City.Text = drCompanies["WarehouseCity"].ToString();
            Warehouse_StateProvince.Text = drCompanies["WarehouseStateProvince"].ToString();
            Warehouse_State.Text = drCompanies["WarehouseState"].ToString();
            Warehouse_Zip.Text = drCompanies["WarehouseZIPCode"].ToString();
            Warehouse_Phone.Text = drCompanies["WarehousePhone"].ToString();
            Warehouse_Fax.Text = drCompanies["WarehouseFax"].ToString();
            Warehouse_Email.Text = drCompanies["WarehouseEMail"].ToString();

            Employees.Text = drCompanies["Employees"].ToString();
            TurnOver.Text = String.Format("{0:c}", drCompanies["Billed"].ToString());
            Description.Text = drCompanies["Description"].ToString();
            CategoriesRep.Text = drCompanies["Categories"].ToString();
            EmailML.Text = drCompanies["MLEmail"].ToString();
            MlCheck.Checked = (bool)drCompanies["MLFlag"];

            dropZones.SelectedItem.Selected = false;
            foreach (ListItem li in dropZones.Items)
            {
                if (li.Value == drCompanies["CommercialZone"].ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
            Sector.SelectedItem.Selected = false;
            foreach (ListItem li in Sector.Items)
            {
                if (li.Value == drCompanies["CompanyTypeID"].ToString())
                {
                    li.Selected = true;
                    break;
                }
            }
            ContactType.SelectedItem.Selected = false;
            foreach (ListItem li in ContactType.Items)
            {
                if (li.Value == drCompanies["ContactTypeID"].ToString())
                {
                    li.Selected = true;
                    break;
                }
            }

            Evaluation.SelectedItem.Selected = false;
            foreach (ListItem li in Evaluation.Items)
            {
                if (li.Value == drCompanies["Estimate"].ToString())
                {
                    li.Selected = true;
                    break;
                }
            }

            groups.SetGroups(drCompanies["Groups"].ToString());

            FillRepCategories();

            EditFreeFields.CheckFreeFields(id,CRMTables.Base_Companies, UC);

            ContactForm.Visible = true;
            SearchListRepeater.Visible = false;
            ViewContact.Visible = false;
            SetFocus(CompanyName);


            AddKeepAlive();
        }


        private void FillRepCategories()
        {
            if (ContactId.Text == "-1")
            {
                visContact.Visible = true;
            }
            DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) ORDER BY FLAGPERSONAL DESC").Tables[0];
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (CategoriesRep.Text.IndexOf("|" + dr["id"].ToString() + "|") > -1)
                        dr["tocheck"] = "1";
                }
            }
            RepCategories.DataSource = new DataView(dt, "", "tocheck desc", DataViewRowState.CurrentRows);
            RepCategories.DataBind();
        }

        public void RepCategories_DataBound(Object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    Literal IDCat = (Literal)e.Item.FindControl("IDCat");
                    if (CategoriesRep.Text.IndexOf("|" + IDCat.Text + "|") > -1)
                    {
                        CheckBox Check = (CheckBox)e.Item.FindControl("Check");
                        Check.Checked = true;
                    }

                    DataSet ds = DatabaseConnection.CreateDataset("SELECT ID FROM BASE_COMPANIES WHERE CATEGORIES LIKE '%|" + DatabaseConnection.FilterInjection(IDCat.Text) + "|%'");
                    if (ds.Tables[0].Rows.Count < 1)
                    {
                        LinkButton DeleteCat = (LinkButton)e.Item.FindControl("DeleteCat");
                        DeleteCat.Text = "<img src='/i/erase.gif' border=0 alt='" + Root.rm.GetString("CRMcontxt49") + "'>";
                        DeleteCat.Attributes.Add("onclick", "g_fFieldsChanged=0;return confirm('" + Root.rm.GetString("CRMcontxt50") + "');");
                    }
                    break;
            }
        }

        public void RepCategories_Command(Object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "deleteCat":
                    string count = "SELECT COUNT(*) FROM BASE_COMPANIES WHERE CATEGORIES LIKE '|" + ((Literal)e.Item.FindControl("IDCat")).Text + "|'";

                    int empty = (int)DatabaseConnection.SqlScalartoObj(count);

                    if (empty < 1)
                    {
                        string sqlString = "DELETE FROM CRM_CONTACTCATEGORIES WHERE ID =" + int.Parse(((Literal)e.Item.FindControl("IDCat")).Text);
                        DatabaseConnection.DoCommand(sqlString);
                    }
                    FillRepCategories();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "xxx", "<script>g_fFieldsChanged=1;</script>");
                    break;
            }
        }




        private void FillView(int id)
        {
            Session["contact"] = id;
            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT TOP 1 BASE_COMPANIES.*, (ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNERNAME, (SALES.NAME+' '+SALES.SURNAME) AS SALESPERSONNAME, ACCOUNT.SELFCONTACTID AS SELFCONTACTOWNER, SALES.SELFCONTACTID, (MODBY.NAME+' '+MODBY.SURNAME) AS LASTMODIFIEDBY, CONTACTTYPE.CONTACTTYPE AS CONTACTTYPE, COMPANYTYPE.DESCRIPTION AS COMPANYTYPE, CONTACTESTIMATE.ESTIMATE AS ESTIMATEDESC ");
            sqlString.Append("FROM BASE_COMPANIES LEFT OUTER JOIN COMPANYTYPE ON BASE_COMPANIES.COMPANYTYPEID = COMPANYTYPE.K_ID AND COMPANYTYPE.LANG='" + UC.Culture.Substring(0, 2) + "' LEFT OUTER JOIN ");
            sqlString.Append("CONTACTTYPE ON BASE_COMPANIES.CONTACTTYPEID = CONTACTTYPE.K_ID AND CONTACTTYPE.LANG='" + UC.Culture.Substring(0, 2) + "' LEFT OUTER JOIN ");
            sqlString.Append("ACCOUNT ON BASE_COMPANIES.OWNERID = ACCOUNT.UID LEFT OUTER JOIN ");
            sqlString.Append("ACCOUNT AS SALES ON BASE_COMPANIES.SALESPERSONID = SALES.UID LEFT OUTER JOIN ");
            sqlString.Append("ACCOUNT AS MODBY ON BASE_COMPANIES.LASTMODIFIEDBYID = MODBY.UID LEFT OUTER JOIN ");
            sqlString.AppendFormat("CONTACTESTIMATE ON BASE_COMPANIES.ESTIMATE = CONTACTESTIMATE.K_ID AND CONTACTESTIMATE.LANG='" + UC.Culture.Substring(0, 2) + "' WHERE BASE_COMPANIES.ID={0}", id);

            DataSet dscompany = DatabaseConnection.CreateDataset(sqlString.ToString());
            Recent.AddItem(UC.UserId, dscompany.Tables[0].Rows[0]["COMPANYNAME"].ToString(), id, RecentType.Company, RecentMode.Read);

            ViewContact.DataSource = dscompany;
            ViewContact.DataBind();

            ViewContact.Visible = true;
            this.visReferrer.Visible = true;
            ContactForm.Visible = false;
            SearchListRepeater.Visible = false;
            Tabber.Visible = true;



            try
            {
                if (this.sheetP.IDArray.Length <= 0)
                {
                    this.sheetP.CurrentPosition = 0;
                    this.sheetP.enabledisable();
                }
            }
            catch
            {
                this.sheetP.IDArray = new string[0];
                this.sheetP.CurrentPosition = -1;
                this.sheetP.enabledisable();
            }
        }

        public void ContactReferrerItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    LinkButton delete = (LinkButton)e.Item.FindControl("Delete");
                    delete.Attributes.Add("onclick", "return confirm('" + Root.rm.GetString("CRMrubtxt2") + "');");
                    delete.Text = "<img src='/i/erase2.gif' border=0 alt='" + Root.rm.GetString("CRMcontxt44") + "'>";

                    LinkButton Activate = (LinkButton)e.Item.FindControl("Activate");

                    if ((bool)DataBinder.Eval((DataRowView)e.Item.DataItem, "Active"))
                    {
                        Activate.Text = "<img border=0 alt=\"" + Root.rm.GetString("CRMrubtxt9") + "\" src=/i/checkon.gif>";
                        Activate.Attributes.Add("onclick", "return confirm('" + Root.rm.GetString("CRMrubtxt7") + "');");
                    }
                    else
                    {
                        Activate.Text = "<img border=0 alt=\"" + Root.rm.GetString("CRMrubtxt10") + "\" src=/i/checkoff.gif>";
                        Activate.Attributes.Add("onclick", "return confirm('" + Root.rm.GetString("CRMrubtxt8") + "');");
                        LinkButton OpenContact = (LinkButton)e.Item.FindControl("OpenContact");
                        OpenContact.CssClass = "LinethroughGray linked";
                    }

                    Literal callPhone_1 = (Literal)e.Item.FindControl("callPhone_1");
                    string ph = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "Phone_1"));
                    if (ph.Length > 0)
                        callPhone_1.Text = ph + MakeVoipString(StaticFunctions.FixPhoneNumber(ph));

                    Literal callMobilePhone_1 = (Literal)e.Item.FindControl("callMobilePhone_1");
                    ph = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "MobilePhone_1"));
                    if (ph.Length > 0)
                        callMobilePhone_1.Text = ph + MakeVoipString(StaticFunctions.FixPhoneNumber(ph));
                    break;
            }
        }

        public void ContactReferrerCommand(Object sender, RepeaterCommandEventArgs e)
        {
            Literal IdRef = (Literal)e.Item.FindControl("IdRef");
            switch (e.CommandName)
            {
                case "activate":
                    LinkButton Activate = (LinkButton)e.Item.FindControl("Activate");
                    if (Activate.Text.IndexOf("checkon") > 0)
                        DatabaseConnection.DoCommand("UPDATE BASE_CONTACTS SET ACTIVE=0 WHERE ID=" + int.Parse(IdRef.Text));
                    else
                        DatabaseConnection.DoCommand("UPDATE BASE_CONTACTS SET ACTIVE=1 WHERE ID=" + int.Parse(IdRef.Text));

                    ReferrerDataSource(int.Parse(Session["contact"].ToString()));
                    break;
                case "openContact":
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] { Session["contact"].ToString(), "c" });
                    Response.Redirect("/CRM/base_contacts.aspx?action=VIEW&m=25&si=31&full=" + IdRef.Text);
                    break;
                case "delete":
                    Delete(int.Parse(IdRef.Text), UC.UserId, "BASE_CONTACTS");

                    ReferrerDataSource(int.Parse(Session["contact"].ToString()));
                    Tabber.Selected = visReferrer.ID;
                    break;
            }
        }

        private void Delete(int id, int userId, string table)
        {
            using (DigiDapter dg = new DigiDapter("SELECT ID,LIMBO,LASTACTIVITY,LASTMODIFIEDDATE,LASTMODIFIEDBYID FROM " + table + " WHERE ID =" + id))
            {
                dg.UpdateOnly();
                dg.Add("LIMBO", 1);
                dg.Add("LASTACTIVITY", 2);
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", userId);
                dg.Execute(table, "ID =" + id);
            }
        }


        public void ViewContact_OnItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    ((FreeFields)e.Item.FindControl("ViewFreeFields")).ViewFreeFields(Convert.ToInt32(DataBinder.Eval((DataRowView)e.Item.DataItem, "id")), CRMTables.Base_Companies, UC);

                        LinkButton ModCon = (LinkButton)e.Item.FindControl("ModCon");
                    ModCon.Text = Root.rm.GetString("Bcotxt16");

                    LinkButton BackToSearch = (LinkButton)e.Item.FindControl("BackToSearch");
                    BackToSearch.Text = Root.rm.GetString("CRMrubtxt6");

                    if (Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "Categories")).Length > 0)
                    {
                        string[] cats = ((string)DataBinder.Eval((DataRowView)e.Item.DataItem, "Categories")).Split('|');
                        string queryCat = String.Empty;
                        foreach (string ca in cats)
                        {
                            if (ca.Length > 0) queryCat += " ID=" + int.Parse(ca) + " OR ";
                        }
                        queryCat = queryCat.Substring(0, queryCat.Length - 4);
                        Repeater RepCategoriesView = (Repeater)e.Item.FindControl("RepCategoriesView");

                        RepCategoriesView.DataSource = DatabaseConnection.CreateDataset("SELECT DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) AND (" + queryCat + ")");
                        RepCategoriesView.DataBind();
                    }


                    ReferrerDataSource(Convert.ToInt32(DataBinder.Eval((DataRowView)e.Item.DataItem, "id")));



                    visContact.LangHeader = "Bcotxt10";
                    Tabber.Selected = visContact.ID;

                    if (DataBinder.Eval((DataRowView)e.Item.DataItem, "FromLead") != DBNull.Value)
                    {
                        Literal LeadInfo = (Literal)e.Item.FindControl("LeadInfo");
                        LeadInfo.Text = "<img src=/i/lookup.gif alt=\"" + Root.rm.GetString("Ledtxt28") + "\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/LeadInfo.aspx?render=no&si=29&id=" + Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "FromLead")) + "',event,380,350)\">";
                    }

                    LinkButton BtnMailAuth = (LinkButton)e.Item.FindControl("BtnMailAuth");
                    BtnMailAuth.Text = "<img src=/images/email.gif alt='" + Root.rm.GetString("Bcotxt55") + "' border=0>";

                    BtnMailAuth.Visible = !(bool)DataBinder.Eval((DataRowView)e.Item.DataItem, "MLFlag");

                    Literal VoipCall = (Literal)e.Item.FindControl("VoipCall");
                    if ((DataBinder.Eval((DataRowView)e.Item.DataItem, "Phone")).ToString().Length > 0)
                    {
                        string ph = StaticFunctions.FixPhoneNumber((string)DataBinder.Eval((DataRowView)e.Item.DataItem, "Phone"));
                        VoipCall.Text = MakeVoipString(ph);
                    }
                    else
                        VoipCall.Text = String.Empty;


                    CompanyScore score1 = (CompanyScore)e.Item.FindControl("score1");
                    score1.Score = 50;
                    score1.CrossID = (Int64)DataBinder.Eval((DataRowView)e.Item.DataItem, "id");
                    string rank = DatabaseConnection.SqlScalar("SELECT RANK FROM RANK_VIEW WHERE IDCROSS=" + (Int64)DataBinder.Eval((DataRowView)e.Item.DataItem, "id") + " AND TYPE=0");
                    if (rank != string.Empty)
                        if (Convert.ToInt32(rank) >= 0)
                        {
                            int r = Convert.ToInt32(rank);
                            if (r < 0)
                                r = r * -50 / 100;
                            else
                                r = (r / 2) + 50;
                            score1.Score = Convert.ToByte(r);
                        }


                    score1.MakeScore();
                    long idzone = (Int64)DataBinder.Eval((DataRowView)e.Item.DataItem, "CommercialZone");
                    if (idzone != 0)
                    {
                        Literal lblZone = (Literal)e.Item.FindControl("lblZone");
						lblZone.Text=DatabaseConnection.SqlScalar(string.Format("SELECT DESCRIPTION FROM ZONES WHERE ID={0}",idzone));
                    }

                    if (DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID") != System.DBNull.Value)
                        if (Convert.ToInt64(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID")) > 0)
                        {
                            string selfcontact = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTID"));
                            Literal litViewSalesContact = (Literal)e.Item.FindControl("litViewSalesContact");
                            litViewSalesContact.Text = string.Format("&nbsp;<img src=\"/i/lens.gif\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/ViewContact.aspx?render=no&id={0}',event,500,300);\">", selfcontact);
                        }

                    if (DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTOWNER") != System.DBNull.Value)
                        if (Convert.ToInt64(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTOWNER")) > 0)
                        {
                            string selfcontact = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "SELFCONTACTOWNER"));
                            Literal litViewOwnerContact = (Literal)e.Item.FindControl("litViewOwnerContact");
                            litViewOwnerContact.Text = string.Format("&nbsp;<img src=\"/i/lens.gif\" style=\"CURSOR:pointer\" onclick=\"CreateBox('/Common/ViewContact.aspx?render=no&id={0}',event,500,300);\">", selfcontact);
                        }
                    break;
            }
        }


        private void FillRepeaterActivity(int companyID)
        {
            AcCrono.ParentID = companyID;
            AcCrono.AcType = (byte)AType.Company;

            AcCrono.FromSheet = "a";
            AcCrono.Refresh();

            if (AcCrono.ItemCount > 0)
            {
                AcCrono.Visible = true;
            }
            else
            {
                AcCrono.Visible = false;
                RepeaterActivityInfo.Text = Root.rm.GetString("CRMcontxt24");
            }
        }

        public void RepeaterActivityDatabound(object source, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    LinkButton openActivity = (LinkButton)e.Item.FindControl("openActivity");
                    string type = DataBinder.Eval((DataRowView)e.Item.DataItem, "Type").ToString();
                    switch (type)
                    {
                        case "1":
                            openActivity.Text = Root.rm.GetString("Acttxt23");
                            break;
                        case "2":
                            openActivity.Text = Root.rm.GetString("Acttxt24");
                            break;
                        case "3":
                            openActivity.Text = Root.rm.GetString("Acttxt25");
                            break;
                        case "4":
                            openActivity.Text = Root.rm.GetString("Acttxt26");
                            break;
                        case "5":
                            openActivity.Text = Root.rm.GetString("Acttxt27");
                            break;
                        case "6":
                            openActivity.Text = Root.rm.GetString("Acttxt28");
                            break;
                        case "7":
                            openActivity.Text = Root.rm.GetString("Acttxt37");
                            break;
                    }

                    HtmlImage Incoming = (HtmlImage)e.Item.FindControl("Incoming");
                    if (DataBinder.Eval((DataRowView)e.Item.DataItem, "InOut").ToString() == "True")
                        Incoming.Src = "/i/incoming.gif";
                    else
                        Incoming.Src = "/i/outcoming.gif";

                    break;
            }
        }

        private void FillRepeaterOpportunity(int companyID)
        {
            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("SELECT ACCOUNT.NAME + ' ' + ACCOUNT.SURNAME AS OWNER, CRM_OPPORTUNITY.TITLE, CRM_OPPORTUNITY.ID ");
            sqlString.Append("FROM CRM_OPPORTUNITY INNER JOIN ");
            sqlString.Append("CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITY.ID = CRM_OPPORTUNITYCONTACT.OPPORTUNITYID LEFT OUTER JOIN ");
            sqlString.Append("ACCOUNT ON CRM_OPPORTUNITY.OWNERID = ACCOUNT.UID ");
            sqlString.AppendFormat("WHERE (CRM_OPPORTUNITYCONTACT.CONTACTID={0} AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=0) ", companyID);

            RepeaterOpportunity.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
            RepeaterOpportunity.DataBind();
            if (RepeaterOpportunity.Items.Count > 0)
            {
                RepeaterOpportunity.Visible = true;
            }
            else
            {
                RepeaterOpportunity.Visible = false;
                RepeaterOpportunityInfo.Text = Root.rm.GetString("CRMcontxt25");
            }
            RepeaterOpportunityInfo.Visible = RepeaterOpportunity.Visible;
        }

        protected void ReferrerDataSource(int id)
        {
            DataTable dt = DatabaseConnection.CreateDataset("SELECT ID,SURNAME,NAME, PHONE_1,MOBILEPHONE_1,EMAIL,ACTIVE FROM BASE_CONTACTS WHERE (COMPANYID=" + id + " OR OTHERCOMPANYID LIKE '%|" + id + "|%') AND LIMBO=0 ORDER BY ACTIVE DESC").Tables[0];
            if (dt.Rows.Count > 0)
            {
                string[] myarray = new string[dt.Rows.Count];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myarray[i] = dt.Rows[i]["ID"] + "|" + dt.Rows[i]["Surname"] + " " + dt.Rows[i]["Name"];
                }
                if (myarray.Length > 0)
                    Session["Contactarray"] = myarray;
            }
            ContactReferrer.DataSource = dt;
            ContactReferrer.DataBind();
            if (ContactReferrer.Items.Count > 0)
                ContactReferrer.Visible = true;
            else
            {
                ContactReferrer.Visible = false;
                ContactReferrerInfo.Text = Root.rm.GetString("CRMcontxt26");
            }
        }




        private void ModifyDataSet(int id)
        {
            using (DigiDapter dg = new DigiDapter("SELECT * FROM BASE_COMPANIES WHERE ID = " + id))
            {
                if (!dg.HasRows)
                {
                    dg.Add("CREATEDBYID", UC.UserId, 'I');
                }

                if (OwnerId.Text.Length > 0)
                    dg.Add("OWNERID", OwnerId.Text);
                else
                    dg.Add("OWNERID", UC.UserId);

                if (SalesPersonID.Text.Length > 0)
                    dg.Add("SALESPERSONID", SalesPersonID.Text);


                dg.Add("COMPANYNAME", CompanyName.Text);
                dg.Add("COMPANYNAMEFILTERED", StaticFunctions.FilterSearch(CompanyName.Text));

                if (id == -1)
                {
                    Object progressive;
                    progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS WHERE TYPE=1");
                    if (progressive != null && !((bool)progressive))
                    {
                        DataRow drprog;
                        drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=1").Tables[0].Rows[0];
                        string newprog = string.Empty;

                        if (Convert.ToInt32(drprog["NPROGRESTART"]) > 0)
                            if (DateTime.Now >= new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) && new DateTime(DateTime.Now.Year, Convert.ToInt32(drprog["NPROGRESTART"]), 1) >= (DateTime)drprog["LASTRESET"])
                            {
                                DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROGSTART,LASTRESET=GETDATE() WHERE TYPE=1");
                                drprog = DatabaseConnection.CreateDataset("SELECT * FROM QUOTENUMBERS WHERE TYPE=1").Tables[0].Rows[0];
                            }
                        newprog += (((int)drprog["NPROG"]) + 1).ToString();
                        if ((bool)drprog["CHECKDAY"])
                            newprog += "-" + DateTime.Now.Day.ToString();
                        if ((bool)drprog["CHECKMONTH"])
                            newprog += "-" + DateTime.Now.Month.ToString();
                        if ((bool)drprog["CHECKYEAR"])
                        {
                            if ((bool)drprog["TWODIGITYEAR"])
                                newprog += "-" + DateTime.Now.ToString("yyyy");
                            else
                                newprog += "-" + DateTime.Now.ToString("yy");
                        }



                        this.CompanyCode.Text = newprog;
                        DatabaseConnection.DoCommand("UPDATE QUOTENUMBERS SET NPROG=NPROG+1 WHERE TYPE=1");
                    }
                }

                dg.Add("COMPANYCODE", CompanyCode.Text);

                dg.Add("VATID", VatId.Text);
                dg.Add("PHONE", Phone.Text);
                dg.Add("FAX", Fax.Text);
                dg.Add("EMAIL", Email.Text.Trim(' '));
                dg.Add("WEBSITE", WebSite.Text);
                if (TurnOver.Text.Length > 0)
                    dg.Add("BILLED", Decimal.Parse(TurnOver.Text, UC.myDTFI));
                else
                    dg.Add("BILLED", Decimal.Parse("0", UC.myDTFI));

                if (Employees.Text.Length > 0)
                    dg.Add("EMPLOYEES", Employees.Text);
                else
                    dg.Add("EMPLOYEES", 0);
                dg.Add("INVOICINGADDRESS", Invoice_Address.Text);
                dg.Add("INVOICINGCITY", Invoice_City.Text);
                dg.Add("INVOICINGSTATEPROVINCE", Invoice_StateProvince.Text);
                dg.Add("INVOICINGSTATE", Invoice_State.Text);
                dg.Add("INVOICINGZIPCODE", Invoice_Zip.Text);
                dg.Add("SHIPMENTADDRESS", Shipment_Address.Text);
                dg.Add("SHIPMENTCITY", Shipment_City.Text);
                dg.Add("SHIPMENTSTATEPROVINCE", Shipment_StateProvince.Text);
                dg.Add("SHIPMENTSTATE", Shipment_State.Text);
                dg.Add("SHIPMENTZIPCODE", Shipment_Zip.Text);
                dg.Add("SHIPMENTPHONE", Shipment_Phone.Text);
                dg.Add("SHIPMENTFAX", Shipment_Fax.Text);
                dg.Add("SHIPMENTEMAIL", Shipment_Email.Text);
                dg.Add("WAREHOUSEADDRESS", Warehouse_Address.Text);
                dg.Add("WAREHOUSECITY", Warehouse_City.Text);
                dg.Add("WAREHOUSESTATEPROVINCE", Warehouse_StateProvince.Text);
                dg.Add("WAREHOUSESTATE", Warehouse_State.Text);
                dg.Add("WAREHOUSEZIPCODE", Warehouse_Zip.Text);
                dg.Add("WAREHOUSEPHONE", Warehouse_Phone.Text);
                dg.Add("WAREHOUSEFAX", Warehouse_Fax.Text);
                dg.Add("WAREHOUSEEMAIL", Warehouse_Email.Text);
                dg.Add("DESCRIPTION", Description.Text);
                dg.Add("MLEMAIL", EmailML.Text);
                dg.Add("MLFLAG", MlCheck.Checked);

                dg.Add("COMPANYTYPEID", Sector.SelectedItem.Value);
                dg.Add("CONTACTTYPEID", ContactType.SelectedItem.Value);
                dg.Add("ESTIMATE", Evaluation.SelectedItem.Value);

                if (dropZones.SelectedIndex > 0)
                    dg.Add("COMMERCIALZONE", dropZones.SelectedValue);
                else
                    dg.Add("COMMERCIALZONE", 0);

                if (groups.GetValue.Length > 0)
                {
                    dg.Add("GROUPS", groups.GetValue);
                }
                else
                {
                    dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");
                }

                dg.Add("LASTACTIVITY", 1, 'U'); //MODIFICA
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow, 'U');
                dg.Add("LASTMODIFIEDBYID", UC.UserId, 'U');

                string cat = "|";
                foreach (RepeaterItem it in RepCategories.Items)
                {
                    CheckBox Check = (CheckBox)it.FindControl("Check");
                    if (Check.Checked)
                        cat += ((Literal)it.FindControl("IDCat")).Text + "|";
                }
                if (cat.Length > 1)
                    dg.Add("CATEGORIES", cat);
                else
                    dg.Add("CATEGORIES", DBNull.Value);

                object newId = dg.Execute("BASE_COMPANIES", "ID=" + id, DigiDapter.Identities.Identity);

                if (dg.RecordInserted)
                {
                    EditFreeFields.FillFreeFields(int.Parse(newId.ToString()),CRMTables.Base_Companies,UC);
                    DatabaseConnection.CommitTransaction();
                    FillView(int.Parse(newId.ToString()));
                }
                else
                {
                    EditFreeFields.FillFreeFields(id,CRMTables.Base_Companies,UC);
                    DatabaseConnection.CommitTransaction();
                    FillView(id);
                }
            }

        }

        public void BtnSearch_Click(Object sender, EventArgs e)
        {
            FillFromSearch();
            DeleteGoBack(true);
        }


        private void FillFromSearch()
        {
            string sqlString = MakeSearchQuery();

            this.FillSearchListRepeater(true, sqlString);

            Tabber.Visible = false;
            ContactForm.Visible = false;
            ViewContact.Visible = false;
            Session["listfromsearch"] = "1";
        }

        internal virtual string MakeSearchQuery()
        {
            string queryType = String.Empty;
            string queryGroup = GroupsSecure("BASE_COMPANIES.GROUPS");

            queryType = " AND ((BASE_COMPANIES.FLAGGLOBALORPERSONAL=2 AND  BASE_COMPANIES.OWNERID=" + UC.UserId + ") OR (BASE_COMPANIES.FLAGGLOBALORPERSONAL<>2))";

            StringBuilder sqlString = new StringBuilder();

            sqlString.AppendFormat("SELECT BASE_COMPANIES.*, ", DatabaseConnection.MaxResult);
            sqlString.Append("(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS OWNER ");
            sqlString.Append("FROM BASE_COMPANIES ");
            sqlString.Append("LEFT OUTER JOIN ACCOUNT ON BASE_COMPANIES.OWNERID = ACCOUNT.UID ");

            string search = DatabaseConnection.FilterInjection(Search.Text);
            string searchfiltered = StaticFunctions.FilterSearch(Search.Text);

			sqlString.AppendFormat("WHERE (LIMBO=0) AND (BASE_COMPANIES.COMPANYNAMEFILTERED LIKE '%{2}%' OR BASE_COMPANIES.COMPANYCODE LIKE '%{0}%' OR BASE_COMPANIES.WEBSITE LIKE '%{0}%' OR BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.INVOICINGADDRESS LIKE '%{0}%' OR BASE_COMPANIES.INVOICINGCITY LIKE '%{0}%' OR BASE_COMPANIES.INVOICINGSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%'){1}", search, queryType,searchfiltered);

            if (ListSector.SelectedIndex > 0)
            {
                sqlString.AppendFormat(" AND (COMPANYTYPEID={0})", ListSector.SelectedValue);
            }
            if (ListType.SelectedIndex > 0)
            {
                sqlString.AppendFormat(" AND (CONTACTTYPEID={0})", ListType.SelectedValue);
            }
            if (ListCategory.SelectedIndex > 0)
            {
                sqlString.AppendFormat(" AND (CATEGORIES LIKE '%|{0}|%')", ListCategory.SelectedValue);
            }

            if (ListOwners.SelectedIndex > 0)
            {
                sqlString.AppendFormat(" AND (OWNERID={0})", ListOwners.SelectedValue);
            }

            if (ListGroups.SelectedIndex > 0)
            {
                if (ListGroups.SelectedItem.Value != "0")
                {
                    string dep = GroupDependency(Convert.ToInt32(ListGroups.SelectedItem.Value));
                    if (dep.Length > 1)
                    {
                        string[] arryD = dep.Split('|');
                        string qGroup = String.Empty;

                        foreach (string ut in arryD)
                        {
                            if (ut.Length > 0) qGroup += "BASE_COMPANIES.GROUPS LIKE '%|" + ut + "|%' OR ";
                        }
                        if (qGroup.Length > 0) qGroup = qGroup.Substring(0, qGroup.Length - 3);
                        sqlString.AppendFormat(" AND ({0})", qGroup);
                    }
                    else
                    {
                        sqlString.AppendFormat(" AND (BASE_COMPANIES.GROUPS LIKE '%|{0}|%')", ListGroups.SelectedItem.Value);
                    }
                }
            }
            else
            {
                if (queryGroup.Length > 0)
                {
                    sqlString.AppendFormat(" AND ({0})", queryGroup);
                }
            }
            if (UC.Zones.Length > 0)
                sqlString.AppendFormat(" AND ({0})", ZoneSecure("BASE_COMPANIES.COMMERCIALZONE"));

            if (Days.SelectedIndex > 0)
            {
                sqlString.AppendFormat(" AND (NOCONTACT=0 AND LASTCONTACT<GETDATE()-{0})", Days.SelectedValue);
            }
            return sqlString.ToString();
        }

        public void RapSubmit_Click(Object sender, EventArgs e)
        {
            if (RapCompanyName.Text.Length > 0)
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("CREATEDBYID", UC.UserId);
                    dg.Add("COMPANYNAME", RapCompanyName.Text);
                    dg.Add("PHONE", RapTelephone.Text);
                    dg.Add("EMAIL", RapEmail.Text.Trim(' '));
                    dg.Add("OWNERID", UC.UserId);
                    dg.Add("GROUPS", "|" + UC.AdminGroupId + "|" + UC.UserGroupId + "|");
                    dg.Add("COMPANYNAMEFILTERED", StaticFunctions.FilterSearch(RapCompanyName.Text));

                    object newId = dg.Execute("BASE_COMPANIES", "ID=-1", DigiDapter.Identities.Identity);

                    SetGoBack("/crm/crm_companies.aspx?m=25&si=29&gb=1", new string[2] { newId.ToString(), "d" });

                    RapInfo.Text = "\"<a href=/crm/crm_companies.aspx?m=25&si=29&gb=1><span class=divautoformRequired>" + RapCompanyName.Text + "</span></a>\" " + Root.rm.GetString("Bcotxt43");
                    RapCompanyName.Text = String.Empty;
                    RapTelephone.Text = String.Empty;
                    RapEmail.Text = String.Empty;

                }
            }
            else
            {
                RapInfo.Text = Root.rm.GetString("Bcotxt48");
            }
        }

        public void ConSubmit_Click(Object sender, EventArgs e)
        {
            ModifyDataSet(int.Parse(ContactId.Text));
            sheetP.Visible = true;
        }

        public void ConCancel_Click(Object sender, EventArgs e)
        {
            if (ContactId.Text == "-1")
            {
                Response.Redirect("/CRM/crm_companies.aspx?m=1&si=3");
            }
            else
            {
                FillView(int.Parse(ContactId.Text));
                sheetP.Visible = true;
            }

        }


        public void SearchListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "modify":
                    FillForm(int.Parse(((Literal)e.Item.FindControl("IDCat")).Text));
                    break;
                case "MultiDeleteButton":
                    DeleteChecked.MultiLimbo(this.SearchListRepeater.MultiDeleteListArray, "Base_Companies", UC.UserId);
                    this.SearchListRepeater.DataBind();
                    break;
                case "view":
                    Session["contact"] = ((Literal)e.Item.FindControl("IDCat")).Text;
                    if (this.sheetP.IDArray.Length > 0)
                    {
                        string[] tempArray = this.sheetP.IDArray;
                        for (int i = 0; i < tempArray.Length; i++)
                        {
                            if (tempArray[i].Split('|')[0].CompareTo(((Literal)e.Item.FindControl("IDCat")).Text) == 0)
                            {
                                this.sheetP.CurrentPosition = i;
                                this.sheetP.enabledisable();
                                break;
                            }
                        }
                    }
                    FillView(int.Parse(((Literal)e.Item.FindControl("IDCat")).Text));

                    break;
            }
        }

        public void SearchListRepeater_ItemDataBound(Object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    DataRowView drv = (DataRowView)e.Item.DataItem;

                    Label sc = (Label)e.Item.FindControl("ShortDescription");
                    string desc = drv["Description"].ToString();
                    sc.Text = desc;
                    sc.Attributes.Add("onmouseover", "this.title = this.innerHTML");
                    LinkButton View = (LinkButton)e.Item.FindControl("View");
                    View.Attributes.Add("onmouseover", "this.title = this.innerHTML");
                    string salesperson = (DataBinder.Eval((DataRowView)e.Item.DataItem, "SalesPersonid")).ToString();
                    string ownerid = (DataBinder.Eval((DataRowView)e.Item.DataItem, "OwnerID")).ToString();
                    if (salesperson.Length > 0 && salesperson != UC.UserId.ToString() && ownerid != UC.UserId.ToString() && UC.AdminGroupId != UC.UserGroupId)
                    {
                        e.Item.Visible = false;
                    }
                    break;
            }
        }

		public void ViewContact_Grid_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "modCon":

					FillForm(int.Parse(((Literal) e.Item.FindControl("IDCon")).Text));

                    string strScript = "<SCRIPT>" + Environment.NewLine +
                        "needsave('" + Root.rm.GetString("CRMcontxt77") + "');" + Environment.NewLine +
                        "</SCRIPT>";

                    ClientScript.RegisterStartupScript(this.GetType(), "anything", strScript);
                    CancelCon.Attributes.Add("onclick", "needsave('no')");
                    SubmitCon.Attributes.Add("onclick", "needsave('no');return ValidatecompanyName();");
                    SubmitRef.Attributes.Add("onclick", "needsave('no');return ValidatecompanyName();");

					RefreshRepCategories.Attributes.Add("onclick", "needsave('no')");
                    ContactForm.Visible = true;
					visContact.Visible = true;
					Tabber.EditTab = "visContact";
					activeMode.Value = "edit";
					sheetP.Visible = false;
					break;
				case "NewRef":
					Response.Redirect("/CRM/base_contacts.aspx?action=NEW");
					break;
				case "backToSearch":
					Tabber.Visible = false;
					SearchListRepeater.Visible = true;
					break;
				case "btnMailAuth":
					TemplateAdmin ta = new TemplateAdmin();
					ta.TemplateName = "MailAuthContacts";
					ta.Language = UC.CultureSpecific;
                    ta.ApplicationPath = Request.PhysicalApplicationPath;
                    string template = ta.GetTemplate();
                    if (template == "0" || template == "1")
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Bcotxt58") + "');</script>");
                    }
                    else
                    {
                        Guid gu = Guid.NewGuid();
                        template = template.Replace("[guid]", gu.ToString());
                        template = template.Replace("[Tustena.Account]", UC.UserRealName);

                        int iDCon = int.Parse(((Literal)e.Item.FindControl("IDCon")).Text);
                        template = template.Replace("[Tustena.Email]", ((Literal)e.Item.FindControl("LtrEmail")).Text);
						DatabaseConnection.DoCommand("INSERT INTO ML_AUTH (ID,TABLEID,FIELDID) VALUES ('" + gu.ToString() + "',0," + iDCon + ")");
                        MessagesHandler.SendMail(((Literal)e.Item.FindControl("LtrEmail")).Text,
                                 UC.MailingAddress,
                                 "[Tustena] " + Root.rm.GetString("Bcotxt56"),
                                 template);
                        ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Bcotxt57") + "');</script>");
                    }
                    break;
            }
        }

        private void InitAjax()
        {
            Manager.Register(this, "Ajax.Companies", Debug.None);
        }

        [Method]
        public string CheckDuplicatedCompanies(string companyName)
        {
            return DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE COMPANYNAME LIKE '" + DatabaseConnection.FilterInjection(companyName) + "%'");
        }

        [Method]
        public DataSet SuggestState(string state)
        {
            return DatabaseConnection.CreateDataset("SELECT NAME_IT AS NAME FROM COUNTRY WHERE NAME_IT LIKE '" + DatabaseConnection.FilterInjection(state) + "%'");
        }

        public void RepeaterActivityCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "OpenAC":
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[2] { Session["contact"].ToString(), "a" });
                    Response.Redirect("/CRM/CRM_Activity.aspx?a=" + ((Label)e.Item.FindControl("IDAc")).Text + "&t=" + ((Label)e.Item.FindControl("TypeAc")).Text);
                    break;
            }
        }

        public void RepeaterOpportunityCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "openOP":
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[2] { Session["contact"].ToString(), "o" });
                    Response.Redirect("/CRM/CRM_Opportunity.aspx?m=25&si=37&o=" + ((Literal)e.Item.FindControl("IdOp")).Text);
                    break;
            }
        }

        private void Fill_Sectors(DropDownList st)
        {
            DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY DESCRIPTION");
            st.DataSource = ds;
            st.DataTextField = "Description";
            st.DataValueField = "K_ID";
            st.DataBind();
            st.Items.Insert(0, Root.rm.GetString("CRMcontxt13"));
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
            ct.Items.Insert(0, Root.rm.GetString("CRMcontxt14"));
            ct.Items[0].Value = "0";
        }

        private void Fill_Owners(DropDownList ct)
        {
            ct.DataTextField = "Utetx";
            ct.DataValueField = "uid";
			ct.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(NAME+' '+SURNAME) AS UTETX FROM ACCOUNT");
            ct.DataBind();
            ct.Items.Insert(0, Root.rm.GetString("Caltxt51"));
            ct.SelectedIndex = 0;
            ct.Items[0].Value = "0";
        }

        private void Fill_Evaluation(DropDownList ct)
        {
            DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,ESTIMATE FROM CONTACTESTIMATE WHERE LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY FIELDORDER");
            ct.DataSource = ds;
            ct.DataTextField = "Estimate";
            ct.DataValueField = "K_ID";
            ct.DataBind();
            ct.Items.Insert(0, Root.rm.GetString("CRMcontxt15"));
            ct.Items[0].Value = "0";
        }

        public void NewActivity_Click(Object sender, EventArgs e)
        {
            SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] { Session["contact"].ToString(), "a" });
            Session["AcCompanyID"] = Session["contact"].ToString();
            switch (((LinkButton)sender).ID)
            {
                case "NewActivityPhone":
                    Session["AType"] = 1;
                    break;
                case "NewActivityLetter":
                    Session["AType"] = 2;
                    break;
                case "NewActivityFax":
                    Session["AType"] = 3;
                    break;
                case "NewActivityMemo":
                    Session["AType"] = 4;
                    break;
                case "NewActivityEmail":
                    Session["AType"] = 5;
                    break;
                case "NewActivityVisit":
                    Session["AType"] = 6;
                    break;
                case "NewActivityGeneric":
                    Session["AType"] = 7;
                    break;
                case "NewActivitySolution":
                    Session["AType"] = 8;
                    break;
            }
            Response.Redirect("/WorkingCRM/AllActivity.aspx?m=25&si=38");
        }

        public void Btn_Click(Object sender, EventArgs e)
        {
            this.sheetP.Visible = false;
            switch (((LinkButton)sender).ID)
            {

                case "RefreshRepCategories":
                    FillRepCategories();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "xxx", "<script>g_fFieldsChanged=1;</script>");
                    break;
                case "BtnViewAdvanced":
                    PrepareAdvanced();
                    break;
                case "AllActivity":
                    FillRepeaterActivity(int.Parse(Session["contact"].ToString()));
                    Tabber.Selected = visActivity.ID;
                    break;
                case "NewDoc":
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] { Session["contact"].ToString(), "f" });
                    Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=c");
                    break;
                case "SaveNewProduct":
                    SaveProduct();
                    NewProduct.Visible = false;
                    RepeaterProducts.Visible = true;
                    break;
                case "ResetNewProduct":
                    CRM_CompetitorProducts_ProductName.Text = String.Empty;
                    CRM_CompetitorProducts_Price.Text = String.Empty;
                    CRM_CompetitorProducts_Description.Text = String.Empty;
                    CRM_CompetitorProducts_UnitPrice.Text = String.Empty;
                    CRM_CompetitorProducts_Package.Text = String.Empty;
                    NewProduct.Visible = false;
                    RepeaterProducts.Visible = true;
                    break;
                case "InsertNewProduct":
                    CRM_CompetitorProducts_ProductName.Text = String.Empty;
                    CRM_CompetitorProducts_Price.Text = String.Empty;
                    CRM_CompetitorProducts_Description.Text = String.Empty;
                    CRM_CompetitorProducts_ID.Text = "-1";
                    CRM_CompetitorProducts_UnitPrice.Text = String.Empty;
                    CRM_CompetitorProducts_Package.Text = String.Empty;
                    NewProduct.Visible = true;
                    RepeaterProducts.Visible = false;
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] { Session["contact"].ToString(), "g" });
                    InsertNewProduct.Visible = false;
                    GoBackProd.Visible = true;
                    break;
                case "NewContact":
                    SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", new string[] { Session["contact"].ToString(), "c" });
                    Response.Redirect("/CRM/base_contacts.aspx?m=25&si=31&action=NEW");
                    break;
            }
        }

        private void ShowHideTabs(bool visible)
        {
            visActivity.Visible = visible;
            visOpportunity.Visible = visible;

            visEstimate.Visible = visible;
            visDocuments.Visible = visible;
            visProducts.Visible = visible;
        }

        private void SaveProduct()
        {
            int id = int.Parse(CRM_CompetitorProducts_ID.Text);
            string sqlString = "SELECT ID FROM CRM_COMPETITORPRODUCTS WHERE ID=" + id;
            using (DigiDapter dg = new DigiDapter(sqlString))
            {
                if (!dg.HasRows)
                {
                    dg.Add("COMPETITORID", Session["Contact"].ToString(), 'I');
                }
                dg.Add("PRODUCTNAME", (CRM_CompetitorProducts_ProductName.Text.Length > 0) ? CRM_CompetitorProducts_ProductName.Text : "");
                dg.Add("PRICE", (CRM_CompetitorProducts_Price.Text.Length > 0) ? StaticFunctions.FixDecimal(CRM_CompetitorProducts_Price.Text) : 0);
                dg.Add("PRODUCTDESCRIPTION", (CRM_CompetitorProducts_Description.Text.Length > 0) ? CRM_CompetitorProducts_Description.Text : "");
                if (CRM_CompetitorProducts_UnitPrice.Text.Length > 0)
                    dg.Add("UNITPRICE", StaticFunctions.FixDecimal(CRM_CompetitorProducts_UnitPrice.Text));
                if (CRM_CompetitorProducts_Package.Text.Length > 0)
                    dg.Add("PACKAGE", CRM_CompetitorProducts_Package.Text);
                dg.Execute("CRM_COMPETITORPRODUCTS", "ID=" + id);
            }
            FillRepeaterProducts(int.Parse(Session["Contact"].ToString()));

        }


        private void PrepareAdvanced()
        {
            SearchListRepeater.Visible = false;
            Tabber.Visible = false;
            AdvancedSearch.Visible = true;
        }


        private void ClearTextBoxPurchase(IEnumerator i)
        {
            while (i.MoveNext())
            {
                if (i.Current.GetType().Name == "TextBox")
                {
                    TextBox t = (TextBox)i.Current;
                    if (t.ID == "purchase_ID")
                        t.Text = "-1";
                    else if (t.ID == "purchase_RowsNumber")
                        t.Text = "1";
                    else
                        t.Text = String.Empty;
                }
                else
                {
                    Control y = (Control)i.Current;
                    ClearTextBoxPurchase(y.Controls.GetEnumerator());
                }
            }
        }



        public void ViewAgenda_Click(object sender, EventArgs e)
        {
            string[] bparam = new string[2];
            if (Tabber.Visible && Session["contact"] != null)
            {
                bparam[0] = Session["contact"].ToString();

                switch (Tabber.Selected)
                {
                    case "visReferrer":
                        bparam[1] = "c";
                        break;
                    case "visActivity":
                        bparam[1] = "a";
                        break;
                    case "visOpportunity":
                        bparam[1] = "o";
                        break;
                    case "visReminder":
                        bparam[1] = "r";
                        break;
                    case "visPurchase":
                        bparam[1] = "p";
                        break;
                    default:
                        bparam[1] = "d";
                        break;
                }
            }
            else
            {
                bparam[1] = "nn";
            }
            SetGoBack("/CRM/crm_companies.aspx?m=25&si=29&gb=1", bparam);
            Response.Redirect("/calendar/agenda.aspx?m=25&si=2");
        }

        public void TabControl_Click(string tabId)
        {
            switch (tabId)
            {
                case "visActivity":
                    FillRepeaterActivity(int.Parse(Session["contact"].ToString()));
                    visActivity.Visible = true;
                    break;
                case "visOpportunity":
                    FillRepeaterOpportunity(int.Parse(Session["contact"].ToString()));
                    visOpportunity.Visible = true;
                    break;
                case "visEstimate":
                    Session["CustomerQuote"] = "0|" + Session["contact"].ToString();
                    CustomerQuote1.IsQuote = true;
                    CustomerQuote1.CustomerData = new string[] { "0", Session["contact"].ToString() };
                    CustomerQuote1.Bind();
                    CustomerQuote2.IsQuote = false;
                    CustomerQuote2.CustomerData = new string[] { "0", Session["contact"].ToString() };
                    CustomerQuote2.Bind();
                    visEstimate.Visible = true;
                    break;
                case "visProducts":
                    FillRepeaterProducts(int.Parse(Session["contact"].ToString()));
                    visDocuments.Visible = true;
                    break;
                case "visDocuments":
                    Session["review"] = "0";
                    FillFileRep(int.Parse(Session["contact"].ToString()));
                    visProducts.Visible = true;
                    break;
            }
        }


        private void FillRepeaterProducts(int id)
        {
            RepeaterProducts.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM CRM_COMPETITORPRODUCTS WHERE COMPETITORID=" + id);
            RepeaterProducts.DataBind();
            if (RepeaterProducts.Items.Count > 0)
            {
                RepeaterProductsInfo.Visible = false;
                RepeaterProducts.Visible = true;
            }
            else
            {
                RepeaterProductsInfo.Visible = true;
                RepeaterProducts.Visible = false;
                RepeaterProductsInfo.Text = Root.rm.GetString("CRMcontxt62");
            }
        }

        public void RepeaterProductsCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "openProd":
                    Literal IdProduct = (Literal)e.Item.FindControl("IdProduct");
                    DataSet ProdDS = DatabaseConnection.CreateDataset("SELECT * FROM CRM_COMPETITORPRODUCTS WHERE ID=" + int.Parse(IdProduct.Text));
                    CRM_CompetitorProducts_ID.Text = IdProduct.Text;
                    CRM_CompetitorProducts_ProductName.Text = ProdDS.Tables[0].Rows[0]["ProductName"].ToString();
                    CRM_CompetitorProducts_Price.Text = String.Format("{0:###00.00}", ProdDS.Tables[0].Rows[0]["Price"]);
                    CRM_CompetitorProducts_Description.Text = ProdDS.Tables[0].Rows[0]["ProductDescription"].ToString();
                    CRM_CompetitorProducts_UnitPrice.Text = String.Format("{0:###00.00}", ProdDS.Tables[0].Rows[0]["UnitPrice"]);
                    CRM_CompetitorProducts_Package.Text = String.Format("{0:###00.00}", ProdDS.Tables[0].Rows[0]["Package"]);
                    RepeaterProducts.Visible = false;
                    NewProduct.Visible = true;

                    break;
            }
        }

        public void FileRepCommand(object source, RepeaterCommandEventArgs e)
        {
            Trace.Warn(e.CommandName);
            Literal FileId = (Literal)e.Item.FindControl("FileId");
            switch (e.CommandName)
            {
                case "down":

                    DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileId.Text));

                    string fileName;
					fileName = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"];
                    string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();
                    string downFile = fileName + Path.GetExtension(realFileName);
                    if (File.Exists(downFile))
                    {
                        Response.AddHeader("Content-Disposition", "attachment; fileName=" + realFileName);
                        Response.ContentType = "application/octet-stream";
                        Response.TransmitFile(downFile);
                        Response.Flush();
                        Response.End();
                        return;
                    }
                    else if (File.Exists(fileName))
                    {
                        File.Move(fileName, downFile);
                        Response.AddHeader("Content-Disposition", "attachment; fileName=" + realFileName);
                        Response.ContentType = "application/octet-stream";
                        Response.TransmitFile(downFile);
                        Response.Flush();
                        Response.End();
                        return;
                    }
                    else
                    {
                        G.SendError("File lost", downFile);
                    }

                    break;
                case "modify":
                case "revision":
                    Session["IdDoc"] = FileId.Text;
                    SetGoBack("crm_companies.aspx?m=25&si=29&gb=1", new string[2] { Session["contact"].ToString(), "f" });
                    if (e.CommandName == "Modify")
                        Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=m");
                    else
                        Response.Redirect("/DataStorage/DataStorage.aspx?action=NEW&t=e");
                    break;
                case "reviewNumber":
                    Session["review"] = "1";
                    StringBuilder sqlString = new StringBuilder();
                    int fileId = int.Parse(((Literal)e.Item.FindControl("FileId")).Text);
                    sqlString.Append("SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ");
                    sqlString.Append("LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ");
                    sqlString.Append("LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ");
                    sqlString.Append("WHERE (FILEMANAGER.OWNERID=" + UC.UserId + " OR " + GroupsSecure("FILEMANAGER.GROUPS") + ") ");
                    sqlString.Append("AND (FILEMANAGER.ID=" + fileId + " OR FILEMANAGER.ISREVIEW=" + fileId + ") ");
                    sqlString.Append("ORDER BY REVIEWNUMBER DESC");

                    FileRep.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
                    FileRep.DataBind();
                    break;
            }
        }

        public void FileRepDatabound(Object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    LinkButton modify = (LinkButton)e.Item.FindControl("Modify");
                    modify.Text = Root.rm.GetString("Dsttxt16");
                    LinkButton Revision = (LinkButton)e.Item.FindControl("Revision");
                    Revision.Text = Root.rm.GetString("Dsttxt13");

                    LinkButton ReviewNumber = (LinkButton)e.Item.FindControl("ReviewNumber");
                    Literal LbReviewNumber = (Literal)e.Item.FindControl("LbReviewNumber");
                    if (Session["review"].ToString() == "0")
                    {
                        ReviewNumber.Visible = (ReviewNumber.Text != "0");
                        Revision.Visible = true;
                        LbReviewNumber.Visible = false;
                    }
                    else
                    {
                        ReviewNumber.Visible = false;
                        Revision.Visible = false;
                        LbReviewNumber.Visible = (LbReviewNumber.Text != "0");
                    }
                    LinkButton down = (LinkButton)e.Item.FindControl("Down");
                    string ext = Path.GetExtension(down.Text);
                    Image FileImg = (Image)e.Item.FindControl("FileImg");
                    FileImg.ImageUrl = FileFunctions.GetFileImg(ext);

                    Literal FileSize = (Literal)e.Item.FindControl("FileSize");
                    decimal size = Convert.ToDecimal(DataBinder.Eval((DataRowView)e.Item.DataItem, "size"));
                    FileSize.Text = (size / 1024).ToString("N2") + "&nbsp;Kb";
                    break;
            }
        }


        public void FillFileRep(int id)
        {
            string sqlString = "SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ";
            sqlString += "LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ";
            sqlString += "LEFT OUTER JOIN FILECROSSTABLES ON FILEMANAGER.ID=FILECROSSTABLES.IDFILE ";
            sqlString += "LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ";
            sqlString += "WHERE ISREVIEW=0 AND FILECROSSTABLES.TABLENAME='BASE_COMPANIES' AND FILECROSSTABLES.IDRIF=" + id;

            FileRep.DataSource = DatabaseConnection.CreateDataset(sqlString);
            FileRep.DataBind();
            if (FileRep.Items.Count > 0)
            {
                FileRepInfo.Visible = false;
                FileRep.Visible = true;
            }
            else
            {
                FileRepInfo.Visible = true;
                FileRep.Visible = false;
                FileRepInfo.Text = Root.rm.GetString("CRMcontxt61");
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
            this.Load += new EventHandler(this.Page_Load);

            GoBackProd.Click += new EventHandler(BtnBack_Click);
            this.SrcBtn.Click += new EventHandler(Srcbtn_Click);
            this.sheetP.NextClick += new EventHandler(SheetP_NextClick);
            this.sheetP.PrevClick += new EventHandler(SheetP_PrevClick);
            this.NewCompany.Click += new EventHandler(NewCompany_Click);

            this.ViewAgenda.Click += new EventHandler(this.ViewAgenda_Click);
            this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
            this.BtnViewAdvanced.Click += new EventHandler(Btn_Click);
            this.RapSubmit.Click += new EventHandler(this.RapSubmit_Click);
            this.SubmitRef.Click += new EventHandler(this.ConSubmit_Click);
            this.RefreshRepCategories.Click += new EventHandler(this.Btn_Click);
            this.CancelCon.Click += new EventHandler(this.ConCancel_Click);
            this.SubmitCon.Click += new EventHandler(this.ConSubmit_Click);
            this.NewContact.Click += new EventHandler(this.Btn_Click);
            this.AllActivity.Click += new EventHandler(this.Btn_Click);
            this.NewActivityPhone.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityLetter.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityFax.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityMemo.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityEmail.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityVisit.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivityGeneric.Click += new EventHandler(this.NewActivity_Click);
            this.NewActivitySolution.Click += new EventHandler(this.NewActivity_Click);



            this.NewDoc.Click += new EventHandler(this.Btn_Click);
            this.InsertNewProduct.Click += new EventHandler(this.Btn_Click);
            this.ResetNewProduct.Click += new EventHandler(this.Btn_Click);
            this.SaveNewProduct.Click += new EventHandler(this.Btn_Click);
            this.ViewContact.ItemCommand += new RepeaterCommandEventHandler(this.ViewContact_Grid_ItemCommand);
            this.RepCategories.ItemCommand += new RepeaterCommandEventHandler(this.RepCategories_Command);
            this.ContactReferrer.ItemCommand += new RepeaterCommandEventHandler(this.ContactReferrerCommand);
            this.RepeaterOpportunity.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterOpportunityCommand);

            this.FileRep.ItemCommand += new RepeaterCommandEventHandler(this.FileRepCommand);
            this.RepeaterProducts.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterProductsCommand);
            this.ViewContact.ItemDataBound += new RepeaterItemEventHandler(this.ViewContact_OnItemDataBound);
            this.RepCategories.ItemDataBound += new RepeaterItemEventHandler(this.RepCategories_DataBound);
            this.ContactReferrer.ItemDataBound += new RepeaterItemEventHandler(this.ContactReferrerItemDataBound);

            this.FileRep.ItemDataBound += new RepeaterItemEventHandler(this.FileRepDatabound);

            this.SearchListRepeater.ItemDataBound += new RepeaterItemEventHandler(this.SearchListRepeater_ItemDataBound);
            this.SearchListRepeater.ItemCommand += new RepeaterCommandEventHandler(this.SearchListRepeater_ItemCommand);
            this.Tabber.TabClick += new TabClickDelegate(TabControl_Click);
        }

        #endregion

        private void Srcbtn_Click(object sender, EventArgs e)
        {
            srccomp.OnlyName = true;
            this.FillSearchListRepeater(true, srccomp.GetCompanyQuery());

            Tabber.Visible = false;
            ContactForm.Visible = false;
            ViewContact.Visible = false;
        }


        private void InitSheetPaging(string sqlString)
        {
            DataTable dt = DatabaseConnection.CreateDataset(sqlString).Tables[0];
            string[] myarray = new string[dt.Rows.Count];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myarray[i] = dt.Rows[i]["ID"] + "|" + dt.Rows[i]["CompanyName"];
                }
            }

            sheetP.IDArray = myarray;
        }

        private void SheetP_NextClick(object sender, EventArgs e)
        {
            FillView(int.Parse(this.sheetP.GetCurrentID.ToString()));
        }

        private void SheetP_PrevClick(object sender, EventArgs e)
        {
            FillView(int.Parse(this.sheetP.GetCurrentID.ToString()));
        }

        private void NewCompany_Click(object sender, EventArgs e)
        {
            InitAjax();
            PrepareForNewCompany();
        }

        private void PrepareForNewCompany()
        {
            if (UC.CultureSpecific.ToLower() != "it") pgPaste.Visible = false;

            Fill_Sectors(Sector);
            Fill_ContactType(ContactType);
            Fill_Evaluation(Evaluation);

            CompanyName.Text = String.Empty;

            Object progressive;
                progressive = DatabaseConnection.SqlScalartoObj("SELECT DISABLED FROM QUOTENUMBERS WHERE TYPE=1");
            if (progressive != null)
            {
                if (!((bool)progressive))
                {
                    CompanyCode.ReadOnly = true;
                    CompanyCode.Text = Root.rm.GetString("Quotxt43");
                }
            }else
                CompanyCode.Text = String.Empty;

            VatId.Text = String.Empty;
            Phone.Text = String.Empty;
            Fax.Text = String.Empty;
            Email.Text = String.Empty;
            OwnerId.Text = String.Empty;
            OwnerName.Text = String.Empty;
            WebSite.Text = String.Empty;
            Invoice_Address.Text = String.Empty;
            Invoice_City.Text = String.Empty;
            Invoice_StateProvince.Text = String.Empty;
            Invoice_Zip.Text = String.Empty;
            Shipment_Address.Text = String.Empty;
            Shipment_City.Text = String.Empty;
            Shipment_StateProvince.Text = String.Empty;
            Shipment_Zip.Text = String.Empty;
            Shipment_Phone.Text = String.Empty;
            Shipment_Fax.Text = String.Empty;
            Shipment_Email.Text = String.Empty;
            Warehouse_Address.Text = String.Empty;
            Warehouse_City.Text = String.Empty;
            Warehouse_StateProvince.Text = String.Empty;
            Warehouse_Zip.Text = String.Empty;
            Warehouse_Phone.Text = String.Empty;
            Warehouse_Fax.Text = String.Empty;
            Warehouse_Email.Text = String.Empty;

            Employees.Text = String.Empty;
            TurnOver.Text = String.Empty;
            Description.Text = String.Empty;
            CategoriesRep.Text = String.Empty;
            EmailML.Text = String.Empty;
            MlCheck.Checked = true;

            Tabber.Visible = true;

            ContactForm.Visible = true;
            SearchListRepeater.Visible = false;
            ContactId.Text = "-1";
            visContact.Header = Root.rm.GetString("CRMcontxt18");
            FillRepCategories();
            EditFreeFields.CheckFreeFields(-1, CRMTables.Base_Companies, UC);
            SubmitCon.Attributes.Add("onclick", "return ValidatecompanyName();");
            SubmitRef.Attributes.Add("onclick", "return ValidatecompanyName();");
            if (UC.InsertGroups.Length > 0) groups.SetGroups(UC.InsertGroups);
            this.sheetP.Visible = false;
            this.activeMode.Value = "edit";
            Tabber.Selected = visContact.ID;
            Tabber.EditTab = "visContact";
            ViewContact.Visible = false;

        }

        private void InitSearchListRepeater()
        {
            this.SearchListRepeater.UsePagedDataSource = true;
            this.SearchListRepeater.PageSize = UC.PagingSize;
        }

        private void FillSearchListRepeater(bool bind, string query)
        {
            StringBuilder sqlString = new StringBuilder();
            if (query.Length == 0)
            {
                string queryGroup = GroupsSecure("BASE_COMPANIES.GROUPS");
                string queryType = " AND ((BASE_COMPANIES.FLAGGLOBALORPERSONAL=2 AND  BASE_COMPANIES.OWNERID=" + UC.UserId.ToString() + ") OR (BASE_COMPANIES.FLAGGLOBALORPERSONAL<>2))";

                sqlString.AppendFormat("SELECT BASE_COMPANIES.ID, BASE_COMPANIES.COMPANYNAME, BASE_COMPANIES.PHONE, BASE_COMPANIES.FAX, BASE_COMPANIES.DESCRIPTION, BASE_COMPANIES.GROUPS, BASE_COMPANIES.SALESPERSONID, BASE_COMPANIES.OWNERID ", DatabaseConnection.MaxResult);
                sqlString.Append("FROM BASE_COMPANIES ");

				sqlString.AppendFormat(" WHERE LIMBO=0 {0} ", queryType);

                if (queryGroup.Length > 0)
                {
                    sqlString.AppendFormat(" AND ({0})", queryGroup);
                }
                if (UC.Zones.Length > 0)
                    sqlString.AppendFormat(" AND ({0})", ZoneSecure("BASE_COMPANIES.COMMERCIALZONE"));

            }
            else
                sqlString.AppendFormat("{0}", query);

            this.SearchListRepeater.sqlDataSource = sqlString.ToString();

            if (bind)
            {
                this.SearchListRepeater.DataBind();
                this.SearchListRepeater.Visible = true;
            }
            InitSheetPaging(sqlString.ToString());
        }



    }
}
