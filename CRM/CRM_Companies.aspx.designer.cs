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

    public partial class CRM_Companies {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlInputHidden activeMode;
        protected System.Web.UI.WebControls.LinkButton ViewAgenda;
        protected System.Web.UI.WebControls.LinkButton NewCompany;
        protected System.Web.UI.WebControls.LinkButton BtnViewAdvanced;
        protected System.Web.UI.WebControls.TextBox Search;
        protected System.Web.UI.WebControls.DropDownList ListGroups;
        protected System.Web.UI.WebControls.DropDownList ListSector;
        protected System.Web.UI.WebControls.DropDownList ListType;
        protected System.Web.UI.WebControls.DropDownList ListCategory;
        protected System.Web.UI.WebControls.DropDownList ListOwners;
        protected System.Web.UI.WebControls.DropDownList Days;
        protected System.Web.UI.WebControls.LinkButton BtnSearch;
        protected System.Web.UI.WebControls.TextBox RapCompanyName;
        protected System.Web.UI.WebControls.TextBox RapTelephone;
        protected System.Web.UI.WebControls.TextBox RapEmail;
        protected System.Web.UI.WebControls.LinkButton RapSubmit;
        protected System.Web.UI.WebControls.Label RapInfo;
        protected System.Web.UI.HtmlControls.HtmlGenericControl personalView;
        protected System.Web.UI.WebControls.Literal GroupDescription;
        protected Digita.Tustena.WebControls.TustenaRepeater SearchListRepeater;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.Common.SheetPaging sheetP;
        protected Digita.Tustena.WebControls.TustenaTab visContact;
        protected System.Web.UI.WebControls.Repeater ViewContact;
        protected System.Web.UI.HtmlControls.HtmlTable ContactForm;
        protected System.Web.UI.WebControls.LinkButton SubmitRef;
        protected System.Web.UI.WebControls.TextBox CompanyName;
        protected System.Web.UI.HtmlControls.HtmlImage pgPaste;
        protected System.Web.UI.WebControls.TextBox Invoice_Address;
        protected System.Web.UI.WebControls.TextBox Invoice_City;
        protected System.Web.UI.WebControls.TextBox Invoice_StateProvince;
        protected System.Web.UI.WebControls.TextBox Invoice_State;
        protected System.Web.UI.WebControls.TextBox Invoice_Zip;
        protected System.Web.UI.WebControls.TextBox Phone;
        protected System.Web.UI.WebControls.TextBox Fax;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator regularExpressionValidator1;
        protected System.Web.UI.WebControls.TextBox Email;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator regularExpressionValidator2;
        protected System.Web.UI.WebControls.CheckBox MlCheck;
        protected System.Web.UI.WebControls.TextBox EmailML;
        protected System.Web.UI.WebControls.TextBox WebSite;
        protected System.Web.UI.WebControls.TextBox CompanyCode;
        protected System.Web.UI.WebControls.TextBox VatId;
        protected System.Web.UI.WebControls.DropDownList Sector;
        protected System.Web.UI.WebControls.DropDownList ContactType;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valTurnOver;
        protected System.Web.UI.WebControls.TextBox TurnOver;
        protected System.Web.UI.WebControls.TextBox Employees;
        protected System.Web.UI.WebControls.DropDownList Evaluation;
        protected System.Web.UI.WebControls.TextBox OwnerId;
        protected System.Web.UI.WebControls.TextBox OwnerName;
        protected System.Web.UI.WebControls.TextBox SalesPersonID;
        protected System.Web.UI.WebControls.TextBox SalesPerson;
        protected System.Web.UI.WebControls.Repeater RepCategories;
        protected System.Web.UI.WebControls.LinkButton RefreshRepCategories;
        protected System.Web.UI.WebControls.DropDownList dropZones;
        protected System.Web.UI.WebControls.TextBox Description;
        protected System.Web.UI.WebControls.TextBox Shipment_Address;
        protected System.Web.UI.WebControls.TextBox Shipment_City;
        protected System.Web.UI.WebControls.TextBox Shipment_StateProvince;
        protected System.Web.UI.WebControls.TextBox Shipment_State;
        protected System.Web.UI.WebControls.TextBox Shipment_Zip;
        protected System.Web.UI.WebControls.TextBox Shipment_Phone;
        protected System.Web.UI.WebControls.TextBox Shipment_Fax;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator regularExpressionValidator3;
        protected System.Web.UI.WebControls.TextBox Shipment_Email;
        protected System.Web.UI.WebControls.TextBox Warehouse_Address;
        protected System.Web.UI.WebControls.TextBox Warehouse_City;
        protected System.Web.UI.WebControls.TextBox Warehouse_StateProvince;
        protected System.Web.UI.WebControls.TextBox Warehouse_State;
        protected System.Web.UI.WebControls.TextBox Warehouse_Zip;
        protected System.Web.UI.WebControls.TextBox Warehouse_Phone;
        protected System.Web.UI.WebControls.TextBox Warehouse_Fax;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator regularExpressionValidator4;
        protected System.Web.UI.WebControls.TextBox Warehouse_Email;
        protected Digita.Tustena.Common.FreeFields EditFreeFields;
        protected Digita.Tustena.GroupControl groups;
        protected System.Web.UI.WebControls.Literal ContactId;
        protected System.Web.UI.WebControls.Literal CategoriesRep;
        protected System.Web.UI.WebControls.LinkButton CancelCon;
        protected System.Web.UI.WebControls.LinkButton SubmitCon;
        protected Digita.Tustena.WebControls.TustenaTab visReferrer;
        protected System.Web.UI.WebControls.LinkButton NewContact;
        protected System.Web.UI.WebControls.Repeater ContactReferrer;
        protected System.Web.UI.WebControls.Label ContactReferrerInfo;
        protected Digita.Tustena.WebControls.TustenaTab visActivity;
        protected System.Web.UI.WebControls.LinkButton AllActivity;
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
        protected Digita.Tustena.WebControls.TustenaTab visOpportunity;
        protected System.Web.UI.WebControls.Repeater RepeaterOpportunity;
        protected System.Web.UI.WebControls.Label RepeaterOpportunityInfo;
        protected Digita.Tustena.WebControls.TustenaTab visDocuments;
        protected System.Web.UI.WebControls.LinkButton NewDoc;
        protected System.Web.UI.WebControls.Label FileRepInfo;
        protected System.Web.UI.WebControls.Repeater FileRep;
        protected Digita.Tustena.WebControls.TustenaTab visEstimate;
        protected Digita.Tustena.ERP.CustomerQuote CustomerQuote1;
        protected Digita.Tustena.ERP.CustomerQuote CustomerQuote2;
        protected Digita.Tustena.WebControls.TustenaTab visProducts;
        protected Digita.Tustena.WebControls.GoBackBtn GoBackProd;
        protected System.Web.UI.WebControls.LinkButton InsertNewProduct;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_CompetitorID;
        protected System.Web.UI.WebControls.Label RepeaterProductsInfo;
        protected System.Web.UI.HtmlControls.HtmlTable NewProduct;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_ProductName;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valCRM_CompetitorProducts_Package;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_Package;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valCRM_CompetitorProducts_UnitPrice;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_UnitPrice;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator valCRM_CompetitorProducts_Price;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_Price;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_Description;
        protected System.Web.UI.WebControls.TextBox CRM_CompetitorProducts_ID;
        protected System.Web.UI.WebControls.LinkButton ResetNewProduct;
        protected System.Web.UI.WebControls.LinkButton SaveNewProduct;
        protected System.Web.UI.WebControls.Repeater RepeaterProducts;
        protected System.Web.UI.HtmlControls.HtmlTable AdvancedSearch;
        protected Digita.Tustena.Common.SearchCompany srccomp;
        protected System.Web.UI.WebControls.LinkButton SrcBtn;
        protected System.Web.UI.WebControls.Literal SomeJS;
    }
}
