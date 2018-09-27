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

    public partial class ActivityForm {
        protected System.Web.UI.WebControls.Panel PanelActivity;
        protected System.Web.UI.WebControls.Literal LabelTypeActivity;
        protected System.Web.UI.WebControls.RadioButtonList Activity_InOut;
        protected System.Web.UI.WebControls.RadioButtonList Activity_ToDo;
        protected System.Web.UI.WebControls.CheckBox SendMail;
        protected System.Web.UI.HtmlControls.HtmlInputText destinationEmail;
        protected System.Web.UI.WebControls.TextBox TextboxObject;
        protected System.Web.UI.WebControls.Literal ActivityID;
        protected System.Web.UI.WebControls.TextBox TextboxCompanyID;
        protected System.Web.UI.WebControls.TextBox TextboxCompany;
        protected System.Web.UI.WebControls.DropDownList DropDownListClassification;
        protected System.Web.UI.WebControls.TextBox TextboxContactID;
        protected System.Web.UI.WebControls.TextBox TextboxContact;
        protected System.Web.UI.WebControls.TextBox TextboxParentID;
        protected System.Web.UI.WebControls.TextBox TextboxParent;
        protected System.Web.UI.WebControls.TextBox TextboxLeadID;
        protected System.Web.UI.WebControls.TextBox TextboxLead;
        protected System.Web.UI.WebControls.Panel PanelChild;
        protected System.Web.UI.WebControls.DropDownList ChildType;
        protected System.Web.UI.WebControls.LinkButton ChildAction;
        protected System.Web.UI.WebControls.TextBox TextboxOpportunityID;
        protected System.Web.UI.WebControls.TextBox TextboxOpportunity;
        protected System.Web.UI.WebControls.TextBox DocumentDescription;
        protected System.Web.UI.WebControls.TextBox IDDocument;
        protected System.Web.UI.WebControls.LinkButton LinkDocument;
        protected System.Web.UI.WebControls.DropDownList DropDownListStatus;
        protected System.Web.UI.WebControls.DropDownList DropDownListPriority;
        protected System.Web.UI.HtmlControls.HtmlTable tblQuoteStage;
        protected System.Web.UI.WebControls.TextBox Activity_QuoteNumber;
        protected System.Web.UI.WebControls.DropDownList Activity_QuoteStage;
        protected System.Web.UI.HtmlControls.HtmlGenericControl convertpurchase;
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
        protected Digita.Tustena.WebControls.LocalizedImg IMGAvailability;
        protected System.Web.UI.WebControls.Panel PanelQuoteExpire;
        protected System.Web.UI.WebControls.TextBox TextBoxDataQuoteExpire;
        protected System.Web.UI.HtmlControls.HtmlTable HourPanel;
        protected System.Web.UI.WebControls.TextBox TextBoxHour;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Appointmenthours;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator StartHourValidator;
        protected System.Web.UI.WebControls.TextBox F_StartHour;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator EndHourValidator;
        protected System.Web.UI.WebControls.TextBox F_EndHour;
        protected System.Web.UI.WebControls.TextBox IdCompanion;
        protected System.Web.UI.WebControls.TextBox Companion;
        protected Digita.Tustena.WebControls.LocalizedImg UserAppImg;
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
