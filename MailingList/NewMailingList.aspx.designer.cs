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

    public partial class NewMailingList {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlTable Table1;
        protected System.Web.UI.WebControls.LinkButton BtnNewML;
        protected System.Web.UI.WebControls.Label MailToSend;
        protected System.Web.UI.WebControls.Label MailToSendID;
        protected System.Web.UI.WebControls.Repeater MailingListRep;
        protected Digita.Tustena.Common.RepeaterPaging MailListPaging;
        protected System.Web.UI.HtmlControls.HtmlTable NewML;
        protected System.Web.UI.HtmlControls.HtmlTable NewMLTable;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator rvNewMLTitle;
        protected System.Web.UI.WebControls.TextBox NewMLTitle;
        protected System.Web.UI.WebControls.Literal MLID;
        protected System.Web.UI.WebControls.LinkButton NewMLSubmit;
        protected System.Web.UI.HtmlControls.HtmlTable RisearchAdvanced;
        protected System.Web.UI.HtmlControls.HtmlTable ReSearchCompanies;
        protected System.Web.UI.WebControls.TextBox Advanced_CompanyName;
        protected System.Web.UI.WebControls.TextBox Advanced_Address;
        protected System.Web.UI.WebControls.TextBox Advanced_City;
        protected System.Web.UI.WebControls.TextBox Advanced_State;
        protected System.Web.UI.WebControls.TextBox Advanced_Nation;
        protected System.Web.UI.WebControls.TextBox Advanced_Zip;
        protected System.Web.UI.WebControls.TextBox Advanced_Phone;
        protected System.Web.UI.WebControls.TextBox Advanced_Fax;
        protected System.Web.UI.WebControls.TextBox Advanced_Email;
        protected System.Web.UI.WebControls.TextBox Advanced_Site;
        protected System.Web.UI.WebControls.TextBox Advanced_Code;
        protected System.Web.UI.WebControls.DropDownList SAdvanced_CompanyType;
        protected System.Web.UI.WebControls.DropDownList SAdvanced_ContactType;
        protected System.Web.UI.WebControls.TextBox Advanced_Billed;
        protected System.Web.UI.WebControls.TextBox Advanced_Employees;
        protected System.Web.UI.WebControls.DropDownList SAdvanced_Estimate;
        protected System.Web.UI.WebControls.DropDownList SAdvanced_Opportunity;
        protected System.Web.UI.WebControls.DropDownList SAdvanced_Category;
        protected System.Web.UI.WebControls.TextBox Advanced_Owner;
        protected System.Web.UI.WebControls.TextBox Advanced_OwnerID;
        protected System.Web.UI.HtmlControls.HtmlTable AdvancedContacts;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_Address;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_City;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_State;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_Nation;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_Zip;
        protected System.Web.UI.WebControls.TextBox AdvancedContacts_Email;
        protected System.Web.UI.WebControls.DropDownList SAdvancedContacts_Category;
        protected System.Web.UI.HtmlControls.HtmlTable AdvancedLeads;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_Address;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_City;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_State;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_Nation;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_Zip;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_Email;
        protected System.Web.UI.WebControls.DropDownList SAdvancedLead_Opportunity;
        protected System.Web.UI.WebControls.DropDownList SAdvancedLead_Category;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_Owner;
        protected System.Web.UI.WebControls.TextBox AdvancedLead_OwnerID;
        protected System.Web.UI.HtmlControls.HtmlTable ReSearchFixedMails;
        protected System.Web.UI.WebControls.TextBox TextboxSearchCompanyID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchCompany;
        protected System.Web.UI.WebControls.TextBox TextboxSearchContactID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchContact;
        protected System.Web.UI.WebControls.TextBox TextboxSearchLeadID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchLead;
        protected System.Web.UI.WebControls.ListBox MLFixedMails;
        protected System.Web.UI.HtmlControls.HtmlInputHidden ListFixedParams;
        protected System.Web.UI.WebControls.LinkButton SearchAdvanced;
        protected System.Web.UI.WebControls.LinkButton SaveML;
        protected System.Web.UI.WebControls.ListBox SearchResult;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spaninvio;
        protected System.Web.UI.WebControls.LinkButton MoveOne;
        protected System.Web.UI.WebControls.LinkButton MoveAll;
        protected System.Web.UI.WebControls.Literal QueryToSave;
        protected System.Web.UI.HtmlControls.HtmlTable PreviewList;
        protected System.Web.UI.WebControls.Label MLFillCount;
        protected System.Web.UI.WebControls.ListBox MLFill;
        protected System.Web.UI.WebControls.ListBox MLFillRemoved;
        protected System.Web.UI.WebControls.LinkButton RemoveMLFill;
        protected System.Web.UI.WebControls.Label MLFill2Count;
        protected System.Web.UI.WebControls.ListBox MLFill2;
        protected System.Web.UI.WebControls.ListBox MLFill2Removed;
        protected System.Web.UI.WebControls.LinkButton RemoveMLFill2;
        protected System.Web.UI.WebControls.Label MLFill3Count;
        protected System.Web.UI.WebControls.ListBox MLFill3;
        protected System.Web.UI.WebControls.ListBox MLFill3Removed;
        protected System.Web.UI.WebControls.LinkButton RemoveMLFill3;
        protected System.Web.UI.WebControls.Label MLFill4Count;
        protected System.Web.UI.WebControls.ListBox MLFill4;
        protected System.Web.UI.WebControls.ListBox MLFill4Removed;
        protected System.Web.UI.WebControls.LinkButton RemoveMLFill4;
        protected System.Web.UI.WebControls.TextBox SenderTextBox;
        protected System.Web.UI.WebControls.CheckBox ScheduleCheckBox;
        protected System.Web.UI.WebControls.TextBox ScheduleStartDate;
        protected System.Web.UI.WebControls.TextBox ScheduleStartHour;
        protected System.Web.UI.WebControls.LinkButton Verifymail;
        protected System.Web.UI.WebControls.Literal MailPreview;
        protected System.Web.UI.WebControls.LinkButton SendML;
        protected System.Web.UI.WebControls.Label LblSendError;
        protected System.Web.UI.WebControls.LinkButton BackToSendMail;
    }
}
