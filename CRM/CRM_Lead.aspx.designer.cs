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


namespace Digita.Tustena {

    public partial class CRM_Lead {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.LinkButton LbnNew;
        protected System.Web.UI.WebControls.LinkButton BtnAdvanced;
        protected System.Web.UI.WebControls.TextBox Search;
        protected System.Web.UI.WebControls.LinkButton BtnSearch;
        protected System.Web.UI.WebControls.DropDownList ListGroups;
        protected System.Web.UI.WebControls.DropDownList ListCategory;
        protected System.Web.UI.WebControls.LinkButton BtnGroup;
        protected System.Web.UI.WebControls.TextBox RapRagSoc;
        protected System.Web.UI.WebControls.TextBox RapPhone;
        protected System.Web.UI.WebControls.TextBox RapEmail;
        protected System.Web.UI.WebControls.LinkButton RapSubmit;
        protected System.Web.UI.WebControls.Label RapInfo;
        protected Digita.Tustena.WebControls.TustenaRepeater NewRepeater1;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.GoBackBtn Back;
        protected Digita.Tustena.Common.SheetPaging SheetP;
        protected Digita.Tustena.WebControls.TustenaTab visContact;
        protected System.Web.UI.WebControls.Repeater ViewForm;
        protected System.Web.UI.HtmlControls.HtmlTable referenceForm;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_ID;
        protected System.Web.UI.WebControls.LinkButton SubmitRef;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Title;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Name;
        protected System.Web.UI.WebControls.Label RequiredFieldValidatorCognome;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Surname;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_CompanyID;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_CompanyName;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_BusinessRole;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Phone;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_MobilePhone;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Fax;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator RegularExpressionValidator2;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_EMail;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_WebSite;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_BirthDay;
        protected System.Web.UI.WebControls.Label DateFormat;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_BirthPlace;
        protected System.Web.UI.WebControls.Repeater RepCategories;
        protected System.Web.UI.WebControls.LinkButton RefreshRepCategories;
        protected System.Web.UI.WebControls.DropDownList dropZones;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Address;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_City;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Province;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_State;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_ZIPCode;
        protected System.Web.UI.WebControls.TextBox CrossLead_AssociatedCompany;
        protected System.Web.UI.WebControls.TextBox CrossLead_CompanyName;
        protected System.Web.UI.WebControls.TextBox CrossLead_AssociatedContact;
        protected System.Web.UI.WebControls.TextBox CrossLead_ContatcName;
        protected System.Web.UI.WebControls.TextBox CrossLead_LeadOwner;
        protected System.Web.UI.WebControls.TextBox CrossLead_OwnerName;
        protected System.Web.UI.WebControls.DropDownList CrossLead_Status;
        protected System.Web.UI.WebControls.DropDownList CrossLead_Rating;
        protected System.Web.UI.WebControls.DropDownList CrossLead_ProductInterest;
        protected System.Web.UI.WebControls.DropDownList CrossLead_LeadCurrency;
        protected System.Web.UI.WebControls.TextBox CrossLead_PotentialRevenue;
        protected System.Web.UI.WebControls.TextBox CrossLead_EstimatedCloseDate;
        protected System.Web.UI.WebControls.DropDownList CrossLead_Source;
        protected System.Web.UI.WebControls.TextBox CrossLead_Campaign;
        protected System.Web.UI.WebControls.DropDownList CrossLead_Industry;
        protected System.Web.UI.WebControls.TextBox CrossLead_SalesPerson;
        protected System.Web.UI.WebControls.TextBox CrossLead_SalesPersonName;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Notes;
        protected Digita.Tustena.Common.FreeFields EditFreeFields;
        protected Digita.Tustena.GroupControl Groups;
        protected System.Web.UI.WebControls.TextBox CRM_Leads_Categories;
        protected System.Web.UI.WebControls.LinkButton Submit2;
        protected Digita.Tustena.WebControls.TustenaTab visActivity;
        protected System.Web.UI.WebControls.LinkButton NewActivityPhone;
        protected System.Web.UI.WebControls.LinkButton NewActivityLetter;
        protected System.Web.UI.WebControls.LinkButton NewActivityFax;
        protected System.Web.UI.WebControls.LinkButton NewActivityMemo;
        protected System.Web.UI.WebControls.LinkButton NewActivityEmail;
        protected System.Web.UI.WebControls.LinkButton NewActivityVisit;
        protected System.Web.UI.WebControls.LinkButton NewActivityGeneric;
        protected System.Web.UI.WebControls.LinkButton NewActivitySolution;
        protected Digita.Tustena.WorkingCRM.ActivityChronology AcCrono;
        protected System.Web.UI.WebControls.Label RepeaterActivityInfo;
        protected Digita.Tustena.WebControls.TustenaTab visQuote;
        protected Digita.Tustena.ERP.CustomerQuote CustomerQuote1;
        protected Digita.Tustena.ERP.CustomerQuote Customerquote2;
        protected System.Web.UI.HtmlControls.HtmlTable advancedSearch;
        protected Digita.Tustena.Common.SearchLead srcComp;
        protected System.Web.UI.WebControls.LinkButton SrcBtn;
        protected System.Web.UI.WebControls.Literal SomeJS;
    }
}
