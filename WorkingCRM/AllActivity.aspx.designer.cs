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


namespace Digita.Tustena.WorkingCRM {

    public partial class AllActivity {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.LinkButton BtnSearch;
        protected System.Web.UI.WebControls.LinkButton ActPhone;
        protected System.Web.UI.WebControls.LinkButton ActVisit;
        protected System.Web.UI.WebControls.LinkButton ActEmail;
        protected System.Web.UI.WebControls.LinkButton ActFax;
        protected System.Web.UI.WebControls.LinkButton ActLetter;
        protected System.Web.UI.WebControls.LinkButton ActGeneric;
        protected System.Web.UI.WebControls.LinkButton ActCase;
        protected System.Web.UI.WebControls.LinkButton ActMemo;
        protected System.Web.UI.WebControls.LinkButton ActQuote;
        protected System.Web.UI.WebControls.Panel SearchPanel;
        protected System.Web.UI.WebControls.CheckBoxList SearchType;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator cvTextBoxSearchFromData;
        protected System.Web.UI.WebControls.TextBox TextBoxSearchFromData;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator cvTextBoxSearchToData;
        protected System.Web.UI.WebControls.TextBox TextBoxSearchToData;
        protected System.Web.UI.WebControls.TextBox TextboxSearchCompanyID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchCompany;
        protected System.Web.UI.WebControls.TextBox TextboxSearchContactID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchContact;
        protected System.Web.UI.HtmlControls.HtmlGenericControl SearchLeadModule;
        protected System.Web.UI.WebControls.TextBox TextboxSearchLeadID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchLead;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOpID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOp;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwnerID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwner;
        protected System.Web.UI.WebControls.CheckBoxList SearchComTec;
        protected System.Web.UI.WebControls.CheckBox CheckSum;
        protected System.Web.UI.WebControls.CheckBoxList SearchToDo;
        protected System.Web.UI.WebControls.CheckBoxList SearchPriority;
        protected System.Web.UI.WebControls.TextBox TextboxSearchDesc;
        protected System.Web.UI.WebControls.LinkButton SubmitSearch;
        protected System.Web.UI.WebControls.Repeater RepeaterSearch;
        protected Digita.Tustena.Common.RepeaterPaging RepeaterSearchPaging;
        protected System.Web.UI.WebControls.Label RepeaterSearchInfo;
        protected System.Web.UI.HtmlControls.HtmlInputHidden TotalSize;
        protected System.Web.UI.WebControls.Panel AcPanel;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.GoBackBtn Back;
        protected Digita.Tustena.Common.SheetPaging SheetP;
        protected Digita.Tustena.WebControls.TustenaTab ActivityType;
        protected System.Web.UI.WebControls.Panel PanelActivity;
        protected System.Web.UI.WebControls.Literal LabelTypeActivity;
        protected System.Web.UI.WebControls.RadioButtonList Activity_InOut;
        protected System.Web.UI.WebControls.RadioButtonList Activity_ToDo;
        protected System.Web.UI.WebControls.CheckBox CheckSendMail;
        protected System.Web.UI.HtmlControls.HtmlInputText destinationEmail;
        protected System.Web.UI.WebControls.TextBox TextBoxSubject;
        protected System.Web.UI.WebControls.Label ActivityID;
        protected System.Web.UI.WebControls.TextBox TextboxCompanyID;
        protected System.Web.UI.WebControls.TextBox TextboxCompany;
        protected System.Web.UI.WebControls.DropDownList DropDownListClassification;
        protected System.Web.UI.WebControls.TextBox TextboxContactID;
        protected System.Web.UI.WebControls.TextBox TextboxContact;
        protected System.Web.UI.WebControls.TextBox TextboxParentID;
        protected System.Web.UI.WebControls.TextBox TextboxParent;
        protected System.Web.UI.HtmlControls.HtmlGenericControl EditLeadModule;
        protected System.Web.UI.WebControls.TextBox TextboxLeadID;
        protected System.Web.UI.WebControls.TextBox TextboxLead;
        protected System.Web.UI.WebControls.Panel PanelChild;
        protected System.Web.UI.WebControls.DropDownList ChildType;
        protected System.Web.UI.WebControls.LinkButton ChildAction;
        protected System.Web.UI.HtmlControls.HtmlGenericControl EditOpportunityModule;
        protected System.Web.UI.WebControls.TextBox TextboxOpportunityID;
        protected System.Web.UI.WebControls.TextBox TextboxOpportunity;
        protected System.Web.UI.HtmlControls.HtmlGenericControl EditDocumentModule;
        protected System.Web.UI.WebControls.TextBox DocumentDescription;
        protected System.Web.UI.WebControls.TextBox IDDocument;
        protected System.Web.UI.WebControls.LinkButton LinkDocument;
        protected System.Web.UI.WebControls.DropDownList DropDownListStatus;
        protected System.Web.UI.WebControls.DropDownList DropDownListPriority;
        protected System.Web.UI.WebControls.DropDownList Activity_Document;
        protected System.Web.UI.WebControls.LinkButton DocGen;
        protected System.Web.UI.WebControls.Label LabelDescription;
        protected System.Web.UI.WebControls.TextBox TextboxDescription;
        protected System.Web.UI.WebControls.Panel SecondDescription;
        protected System.Web.UI.WebControls.Label LabelDescription2;
        protected System.Web.UI.WebControls.TextBox TextboxDescription2;
        protected System.Web.UI.WebControls.Panel PanelOwner;
        protected System.Web.UI.WebControls.TextBox TextboxOwnerID;
        protected System.Web.UI.WebControls.TextBox TextboxOwner;
        protected System.Web.UI.WebControls.Label LabelData;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator DataValidator;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator CvRecDate;
        protected System.Web.UI.WebControls.TextBox TextBoxData;
        protected System.Web.UI.WebControls.PlaceHolder IMGAvailability_holder;
        protected System.Web.UI.HtmlControls.HtmlImage IMGAvailability;
        protected System.Web.UI.HtmlControls.HtmlTable HourPanel;
        protected System.Web.UI.WebControls.TextBox TextBoxHour;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Appointmenthours;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator StartHourValidator;
        protected System.Web.UI.WebControls.TextBox F_StartHour;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator EndHourValidator;
        protected System.Web.UI.WebControls.TextBox F_EndHour;
        protected System.Web.UI.WebControls.TextBox IdCompanion;
        protected System.Web.UI.WebControls.TextBox Companion;
        protected System.Web.UI.WebControls.DropDownList DropDownListPreAlarm;
        protected System.Web.UI.WebControls.CheckBox CheckToBill;
        protected System.Web.UI.WebControls.CheckBox CheckCommercial;
        protected System.Web.UI.WebControls.CheckBox CheckTechnical;
        protected System.Web.UI.WebControls.TextBox TextBoxDurationH;
        protected System.Web.UI.WebControls.TextBox TextBoxDurationM;
        protected System.Web.UI.HtmlControls.HtmlTableRow MoveLog;
        protected System.Web.UI.WebControls.PlaceHolder MoveLogTable;
        protected System.Web.UI.WebControls.Label ActivityInfo;
        protected System.Web.UI.WebControls.LinkButton SubmitBtn;
        protected System.Web.UI.WebControls.LinkButton SubmitBtnDoc;
    }
}
