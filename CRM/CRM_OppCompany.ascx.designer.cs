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

    public partial class CRM_OppCompany {
        protected Digita.Tustena.WebControls.jscontrolid jsc;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTabberRight Tustenatabberright1;
        protected Digita.Tustena.WebControls.GoBackBtn btnGoBack;
        protected Digita.Tustena.Common.SheetPaging SheetP;
        protected Digita.Tustena.WebControls.TustenaTab Table_Tab1;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator CompanyNewCompanyIDVal;
        protected System.Web.UI.WebControls.TextBox CompanyNewCompanyID;
        protected System.Web.UI.WebControls.TextBox CompanyNewCompany;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator CompanyNewExpectedRevenueVal;
        protected System.Web.UI.WebControls.TextBox CompanyNewExpectedRevenue;
        protected System.Web.UI.WebControls.TextBox CompanyNewAmountClosed;
        protected System.Web.UI.WebControls.TextBox CompanyNewAmountRevenuePercent;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator CompanyNewStateListVal;
        protected System.Web.UI.WebControls.DropDownList CompanyNewStateList;
        protected System.Web.UI.WebControls.DropDownList CompanyNewPhaseList;
        protected System.Web.UI.WebControls.DropDownList CompanyNewProbList;
        protected System.Web.UI.WebControls.DropDownList CompanyLostReasons;
        protected System.Web.UI.WebControls.TextBox NewLostReason;
        protected System.Web.UI.WebControls.TextBox CompanyNewSalesPersonID;
        protected System.Web.UI.WebControls.TextBox CompanyNewSalesPerson;
        protected System.Web.UI.WebControls.TextBox CompanyNewStartDate;
        protected System.Web.UI.WebControls.TextBox CompanyNewEstimatedCloseDate;
        protected System.Web.UI.WebControls.TextBox CompanyNewCloseDate;
        protected System.Web.UI.HtmlControls.HtmlTable InsertProductTable;
        protected System.Web.UI.WebControls.TextBox EstProductID;
        protected System.Web.UI.WebControls.TextBox EstProduct;
        protected System.Web.UI.WebControls.TextBox EstUm;
        protected System.Web.UI.WebControls.TextBox EstQta;
        protected System.Web.UI.WebControls.TextBox EstUp;
        protected System.Web.UI.WebControls.TextBox EstVat;
        protected System.Web.UI.WebControls.TextBox EstPl;
        protected System.Web.UI.WebControls.TextBox EstDescription2;
        protected System.Web.UI.WebControls.TextBox EstPf;
        protected System.Web.UI.WebControls.LinkButton BtnCalcPrice;
        protected System.Web.UI.WebControls.LinkButton BtnAddProduct;
        protected System.Web.UI.WebControls.Repeater RepeaterEstProduct;
        protected System.Web.UI.WebControls.TextBox CompanyNewNote;
        protected System.Web.UI.WebControls.Label CompanyNewInfo;
        protected System.Web.UI.WebControls.TextBox CompanyNewID;
        protected System.Web.UI.WebControls.LinkButton CompanyNewSubmit;
        protected Digita.Tustena.WebControls.TustenaTab Table_Tab6;
        protected System.Web.UI.WebControls.Repeater ViewContact;
        protected Digita.Tustena.WebControls.TustenaTab Table_Tab2;
        protected System.Web.UI.WebControls.Repeater RepeaterReferrer;
        protected System.Web.UI.WebControls.Label RepeaterReferrerInfo;
        protected System.Web.UI.WebControls.TextBox ReferrerID;
        protected System.Web.UI.WebControls.TextBox ReferrerText;
        protected System.Web.UI.WebControls.TextBox Role;
        protected System.Web.UI.WebControls.TextBox PercDecisional;
        protected System.Web.UI.WebControls.DropDownList Character;
        protected System.Web.UI.WebControls.LinkButton SaveNewReferrer;
        protected Digita.Tustena.WebControls.TustenaTab Table_Tab3;
        protected System.Web.UI.WebControls.Repeater CompanyCrossCompetitor;
        protected System.Web.UI.WebControls.Label CompanyCrossCompetitorInfo;
        protected Digita.Tustena.WebControls.TustenaTab Table_Tab4;
        protected System.Web.UI.WebControls.LinkButton NewActivityPhoneCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityLetterCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityFaxCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityMemoCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityEmailCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityVisitCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivityGenericCoOp;
        protected System.Web.UI.WebControls.LinkButton NewActivitySolutionCoOp;
        protected Digita.Tustena.WorkingCRM.ActivityChronology AcCronoAzOp;
        protected System.Web.UI.WebControls.Label RepeaterActivityAzOpInfo;
    }
}
