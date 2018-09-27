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


namespace Digita.Tustena.Common {

    public partial class BillControl {
        protected System.Web.UI.WebControls.Label PurchaseDescription;
        protected Digita.Tustena.WebControls.GoBackBtn GoBackPurc;
        protected System.Web.UI.WebControls.LinkButton NewPurchase;
        protected System.Web.UI.WebControls.Label RepeaterPurchaseInfo;
        protected System.Web.UI.WebControls.Repeater RepeaterPurchase;
        protected System.Web.UI.HtmlControls.HtmlTable CardPurchase;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator rvBillNumber;
        protected System.Web.UI.WebControls.TextBox Purchase_BillNumber;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator rvBillingDate;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator cvBillingDate;
        protected System.Web.UI.WebControls.TextBox Purchase_BillingDate;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator rvExpirationDate;
        protected System.Web.UI.WebControls.TextBox Purchase_ExpirationDate;
        protected System.Web.UI.WebControls.TextBox Purchase_PaymentDate;
        protected System.Web.UI.WebControls.CheckBox Purchase_Payment;
        protected System.Web.UI.WebControls.TextBox Purchase_TextboxOwnerID;
        protected System.Web.UI.WebControls.TextBox Purchase_TextboxOwner;
        protected System.Web.UI.WebControls.TextBox Purchase_ProductID;
        protected System.Web.UI.WebControls.TextBox Purchase_Product;
        protected System.Web.UI.WebControls.TextBox Purchase_Um;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valPurchase_Qta;
        protected System.Web.UI.WebControls.TextBox Purchase_Qta;
        protected System.Web.UI.WebControls.TextBox Purchase_Up;
        protected System.Web.UI.WebControls.DomValidators.RangeDomValidator valPurchase_Vat;
        protected System.Web.UI.WebControls.TextBox Purchase_Vat;
        protected System.Web.UI.WebControls.TextBox Purchase_Pl;
        protected System.Web.UI.WebControls.TextBox Purchase_Description2;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valPurchase_Pf;
        protected System.Web.UI.WebControls.TextBox Purchase_Pf;
        protected System.Web.UI.WebControls.LinkButton BtnCalcPrice;
        protected System.Web.UI.WebControls.LinkButton BtnAddProduct;
        protected System.Web.UI.WebControls.TextBox EstFreeProduct;
        protected System.Web.UI.WebControls.TextBox EstFreeUm;
        protected System.Web.UI.WebControls.TextBox EstFreeQta;
        protected System.Web.UI.WebControls.TextBox EstFreeUp;
        protected System.Web.UI.WebControls.TextBox EstFreeVat;
        protected System.Web.UI.WebControls.TextBox EstFreePf;
        protected System.Web.UI.WebControls.LinkButton BtnAddFreeProductOpp;
        protected System.Web.UI.WebControls.Repeater RepeaterPurchaseProduct;
        protected System.Web.UI.WebControls.TextBox Purchase_Note;
        protected System.Web.UI.WebControls.LinkButton Purchase_Exit;
        protected System.Web.UI.WebControls.LinkButton Purchase_Submit;
        protected System.Web.UI.WebControls.TextBox Purchase_ID;
    }
}
