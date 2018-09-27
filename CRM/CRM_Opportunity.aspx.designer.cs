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

    public partial class CRMOpportunity {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.LinkButton RefreshRepeaterCompany;
        protected System.Web.UI.WebControls.LinkButton RefreshRepeaterLead;
        protected System.Web.UI.WebControls.LinkButton RefreshCloseFrame;
        protected System.Web.UI.WebControls.LinkButton RefreshRepeaterProducts;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral1;
        protected System.Web.UI.WebControls.LinkButton BtnNew;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral2;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral3;
        protected System.Web.UI.WebControls.TextBox FindTxt;
        protected System.Web.UI.WebControls.LinkButton BtnFind;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral4;
        protected Digita.Tustena.WebControls.TustenaRepeater NewRepeaterOpportunity;
        protected System.Web.UI.HtmlControls.HtmlGenericControl tabControl;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTabberRight TustenaTabberRight1;
        protected Digita.Tustena.WebControls.GoBackBtn Back;
        protected Digita.Tustena.WebControls.TustenaTab visOpportunity;
        protected System.Web.UI.HtmlControls.HtmlTable OpportunityCard;
        protected System.Web.UI.WebControls.LinkButton Opportunity_SubmitUP;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral5;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator TitleValidator;
        protected System.Web.UI.WebControls.TextBox Opportunity_Title;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral6;
        protected System.Web.UI.WebControls.TextBox Opportunity_OwnerID;
        protected System.Web.UI.WebControls.TextBox Opportunity_Owner;
        protected System.Web.UI.HtmlControls.HtmlGenericControl PopAccount;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral7;
        protected System.Web.UI.WebControls.Label LabelCurrency;
        protected System.Web.UI.WebControls.DropDownList Opportunity_Currency;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral8;
        protected System.Web.UI.WebControls.Label Opportunity_ExpectedRevenueSymbol;
        protected System.Web.UI.WebControls.TextBox Opportunity_ExpectedRevenue;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral9;
        protected System.Web.UI.WebControls.Label Opportunity_IncomeProbabilitySymbol;
        protected System.Web.UI.WebControls.TextBox Opportunity_IncomeProbability;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral10;
        protected System.Web.UI.WebControls.Label Opportunity_AmountClosedSymbol;
        protected System.Web.UI.WebControls.TextBox Opportunity_AmountClosed;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral11;
        protected System.Web.UI.WebControls.TextBox Opportunity_CreatedBy;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral12;
        protected System.Web.UI.WebControls.TextBox Opportunity_LastModifiedBy;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral13;
        protected System.Web.UI.WebControls.TextBox Opportunity_Description;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral14;
        protected System.Web.UI.WebControls.Repeater RepeaterEstProduct;
        protected System.Web.UI.HtmlControls.HtmlGenericControl Opportunity_Access;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral15;
        protected Digita.Tustena.SelectOffice Office;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral16;
        protected Digita.Tustena.SelectOffice OfficeBasic;
        protected System.Web.UI.WebControls.TextBox Opportunity_ID;
        protected System.Web.UI.WebControls.TextBox Opportunity_AdminAccount;
        protected System.Web.UI.WebControls.LinkButton Opportunity_Submit;
        protected System.Web.UI.WebControls.LinkButton Opportunity_LoadLead;
        protected System.Web.UI.WebControls.LinkButton Opportunity_LoadCompany;
        protected System.Web.UI.WebControls.LinkButton Opportunity_Modify;
        protected Digita.Tustena.WebControls.TustenaTab visCompany;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral17;
        protected System.Web.UI.WebControls.TextBox FindCompany;
        protected System.Web.UI.WebControls.LinkButton FindCompanyButton;
        protected System.Web.UI.WebControls.LinkButton Opportunity_NewCo;
        protected System.Web.UI.WebControls.LinkButton Opportunity_SearchCompany;
        protected System.Web.UI.WebControls.LinkButton Opportunity_CompanyList;
        protected System.Web.UI.HtmlControls.HtmlTable TblSearchCompany;
        protected Digita.Tustena.Common.SearchCompany SearchCompany;
        protected System.Web.UI.WebControls.LinkButton SearchCompanyBtn;
        protected Digita.Tustena.WebControls.TustenaRepeater RepeaterCompany;
        protected System.Web.UI.WebControls.Label RepeaterCompanyInfo;
        protected Digita.Tustena.WebControls.TustenaTab visLead;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral18;
        protected System.Web.UI.WebControls.TextBox FindLead;
        protected System.Web.UI.WebControls.LinkButton FindLeadButton;
        protected System.Web.UI.WebControls.LinkButton Opportunity_NewLead;
        protected System.Web.UI.WebControls.LinkButton Opportunity_SrcLead;
        protected System.Web.UI.WebControls.LinkButton Opportunity_ElencoLead;
        protected System.Web.UI.HtmlControls.HtmlTable TblSrcLead;
        protected Digita.Tustena.Common.SearchLead SrcLead;
        protected System.Web.UI.WebControls.LinkButton SrcLeadBtn;
        protected Digita.Tustena.WebControls.TustenaRepeater RepeaterLead;
        protected System.Web.UI.WebControls.Label RepeaterLeadInfo;
        protected Digita.Tustena.WebControls.TustenaTab visActivity;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral19;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral20;
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
        protected Digita.Tustena.WebControls.TustenaTab VisPartner;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral21;
        protected System.Web.UI.WebControls.LinkButton AddNewPartner;
        protected System.Web.UI.HtmlControls.HtmlTable PartnerCard;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral22;
        protected System.Web.UI.WebControls.TextBox TextboxPartnerOppID;
        protected System.Web.UI.WebControls.TextBox TextboxPartnerID;
        protected System.Web.UI.WebControls.TextBox TextboxPartner;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral23;
        protected System.Web.UI.WebControls.TextBox Opportunity_NotePartner;
        protected System.Web.UI.WebControls.LinkButton PartnerSubmit;
        protected System.Web.UI.WebControls.Repeater RepeaterPartner;
        protected System.Web.UI.WebControls.Label RepeaterInfo;
        protected Digita.Tustena.WebControls.TustenaTab VisCompetitor;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral24;
        protected System.Web.UI.WebControls.LinkButton AddNewCompetitor;
        protected System.Web.UI.HtmlControls.HtmlTable CompetitorCard;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral25;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral26;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator chk_Comp_ID;
        protected System.Web.UI.WebControls.TextBox Competitor_CompetitorID;
        protected System.Web.UI.WebControls.TextBox Competitor_CompanyName;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral27;
        protected System.Web.UI.WebControls.RadioButtonList Competitor_Evaluation;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption1;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption2;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption3;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption4;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption5;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption6;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption7;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption8;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption9;
        protected System.Web.UI.WebControls.ListItem Competitor_EvaluationOption10;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral28;
        protected System.Web.UI.WebControls.TextBox Competitor_Description;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral29;
        protected System.Web.UI.WebControls.TextBox Competitor_Strengths;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral30;
        protected System.Web.UI.WebControls.TextBox Competitor_Weaknesses;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral31;
        protected System.Web.UI.WebControls.TextBox Competitor_Note;
        protected System.Web.UI.WebControls.TextBox IDCompetitor;
        protected System.Web.UI.WebControls.LinkButton CloseCompetitor;
        protected System.Web.UI.WebControls.LinkButton SaveCompetitor;
        protected System.Web.UI.WebControls.Repeater RepeaterCompetitor;
        protected System.Web.UI.WebControls.Label CompetitorInfo;
        protected Digita.Tustena.WebControls.TustenaTab visDocuments;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral32;
        protected System.Web.UI.WebControls.LinkButton NewDoc;
        protected System.Web.UI.WebControls.Label FileRepInfo;
        protected System.Web.UI.WebControls.Repeater FileRep;
        protected Digita.Tustena.CRM_OppCompany OppCompany;
        protected Digita.Tustena.CRM_OppLead OppLead;
    }
}
