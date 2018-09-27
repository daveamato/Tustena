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

    public partial class OrderList {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.LinkButton NewQuote;
        protected System.Web.UI.WebControls.RadioButtonList DiffQuoteExpire;
        protected System.Web.UI.WebControls.TextBox TextBoxSearchQuoteExpire;
        protected System.Web.UI.WebControls.DropDownList SearchQuoteStage;
        protected System.Web.UI.WebControls.TextBox TextboxSearchQuoteNumber;
        protected System.Web.UI.WebControls.TextBox TextboxSearchDescription;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwnerID;
        protected System.Web.UI.WebControls.TextBox TextboxSearchOwner;
        protected System.Web.UI.WebControls.RadioButtonList CrossWith;
        protected System.Web.UI.WebControls.TextBox CrossWithID;
        protected System.Web.UI.WebControls.TextBox CrossWithText;
        protected System.Web.UI.WebControls.RadioButtonList RadiobuttonlistGrandTotal;
        protected System.Web.UI.WebControls.TextBox SearchGrandTotal;
        protected System.Web.UI.WebControls.LinkButton btnSearch;
        protected Digita.Tustena.WebControls.TustenaRepeater NewQuoteListRepeater;
    }
}
