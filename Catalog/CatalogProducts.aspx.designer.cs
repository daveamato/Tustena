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


namespace Digita.Tustena.Catalog {

    public partial class CatalogProducts {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.WebControls.LinkButton AddProduct;
        protected System.Web.UI.WebControls.TextBox Search;
        protected System.Web.UI.WebControls.LinkButton FindProduct;
        protected Microsoft.Web.UI.WebControls.TreeView tvCategoryTreeSearch;
        protected System.Web.UI.WebControls.TextBox FindCatID;
        protected System.Web.UI.HtmlControls.HtmlTable GraphResult;
        protected System.Web.UI.WebControls.Label Result;
        protected System.Web.UI.WebControls.Literal Legend;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTab visControl;
        protected System.Web.UI.HtmlControls.HtmlTable ProductTable;
        protected Microsoft.Web.UI.WebControls.TreeView tvCategoryTree;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator ValtxtIdCategory;
        protected System.Web.UI.WebControls.TextBox TxtIdCategory;
        protected System.Web.UI.WebControls.TextBox TxtTextCategory;
        protected System.Web.UI.WebControls.TextBox TxtId;
        protected System.Web.UI.WebControls.TextBox TxtCode;
        protected System.Web.UI.WebControls.RadioButtonList RadioActive;
        protected System.Web.UI.WebControls.RadioButtonList RadioPublish;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator ValtxtShortDescription;
        protected System.Web.UI.WebControls.TextBox TxtShortDescription;
        protected System.Web.UI.WebControls.TextBox TxtLongDescription;
        protected System.Web.UI.WebControls.TextBox TxtUnit;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator ValtxtQta;
        protected System.Web.UI.WebControls.TextBox TxtQta;
        protected System.Web.UI.WebControls.TextBox TxtQtaBlister;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral1;
        protected System.Web.UI.WebControls.CheckBox chkPrint;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral2;
        protected System.Web.UI.WebControls.TextBox txtPriceExpire;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral3;
        protected System.Web.UI.WebControls.TextBox txtStock;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator ValtxtUnitPrice;
        protected System.Web.UI.WebControls.Literal CurrentCurrency;
        protected System.Web.UI.WebControls.TextBox TxtUnitPrice;
        protected System.Web.UI.WebControls.DropDownList listVat;
        protected System.Web.UI.WebControls.Literal CurrentCurrency2;
        protected System.Web.UI.WebControls.TextBox TxtCost;
        protected System.Web.UI.WebControls.Literal litExcludeList;
        protected System.Web.UI.WebControls.Repeater RepOtherList;
        protected System.Web.UI.WebControls.Label ViewImage;
        protected Brettle.Web.NeatUpload.InputFile ProductImage;
        protected Brettle.Web.NeatUpload.InputFile ProductDocument;
        protected Brettle.Web.NeatUpload.ProgressBar inlineProgressBar;
        protected System.Web.UI.WebControls.Label LblInfo;
        protected System.Web.UI.WebControls.LinkButton BtnSubmit;
        protected System.Web.UI.WebControls.Repeater ProductRepeater;
        protected Digita.Tustena.Common.RepeaterPaging Repeaterpaging1;
        protected System.Web.UI.WebControls.Label RepeaterInfo;
    }
}
