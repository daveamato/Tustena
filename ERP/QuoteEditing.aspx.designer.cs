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


namespace Digita.Tustena.ERP {

    public partial class QuoteEditing {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.DomValidators.DomValidationSummary valSum;
        protected System.Web.UI.WebControls.LinkButton btnSave;
        protected System.Web.UI.WebControls.LinkButton btnClone;
        protected System.Web.UI.WebControls.LinkButton btnCopyToOrder;
        protected System.Web.UI.WebControls.Literal lblPrint;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator QSubjectValidator;
        protected System.Web.UI.WebControls.TextBox QSubject;
        protected System.Web.UI.WebControls.TextBox Qnumber;
        protected System.Web.UI.WebControls.DropDownList QPayment;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator TextboxSearchOwnerIDValidator;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwnerID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwner;
        protected System.Web.UI.HtmlControls.HtmlGenericControl SelectOwner;
        protected System.Web.UI.WebControls.TextBox TextboxSearchManagerID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchManager;
        protected System.Web.UI.HtmlControls.HtmlGenericControl SelectManager;
        protected System.Web.UI.WebControls.TextBox TextboxSearchSignalerID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchSignaler;
        protected System.Web.UI.WebControls.DropDownList QStage;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator QuoteDataValidator;
        protected System.Web.UI.WebControls.TextBox QuoteData;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator QValidDataValidator;
        protected System.Web.UI.WebControls.TextBox QValidData;
        protected System.Web.UI.WebControls.DropDownList PriceList;
        protected System.Web.UI.WebControls.TextBox currentPriceList;
        protected System.Web.UI.WebControls.DropDownList ShipDescription;
        protected System.Web.UI.WebControls.TextBox ShipData;
        protected System.Web.UI.HtmlControls.HtmlTableRow actRow;
        protected System.Web.UI.WebControls.CheckBox CheckActivity;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator CrossWithIDValidator;
        protected System.Web.UI.WebControls.RadioButtonList CrossWith;
        protected System.Web.UI.WebControls.TextBox CrossWithID;
        protected System.Web.UI.WebControls.TextBox CrossWithText;
        protected System.Web.UI.WebControls.TextBox QAddress;
        protected System.Web.UI.WebControls.TextBox SAddress;
        protected System.Web.UI.WebControls.Literal QuoteInfo;
        protected Digita.Tustena.ERP.RowEditing Rowediting1;
        protected System.Web.UI.WebControls.TextBox QuoteDescription;
        protected System.Web.UI.WebControls.CheckBox chkIncludePDFDoc;
        protected System.Web.UI.WebControls.TextBox DocumentDescription;
        protected System.Web.UI.WebControls.TextBox IDDocument;
        protected System.Web.UI.WebControls.LinkButton btnSave2;
        protected System.Web.UI.WebControls.Literal lblPrint2;
    }
}
