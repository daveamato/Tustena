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

    public partial class Base_Contacts {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.WebControls.LinkButton BtnNew;
        protected System.Web.UI.WebControls.TextBox Search;
        protected System.Web.UI.WebControls.LinkButton BtnSearch;
        protected System.Web.UI.WebControls.DropDownList ListGroups;
        protected System.Web.UI.WebControls.DropDownList ListCategory;
        protected System.Web.UI.WebControls.LinkButton BtnGroup;
        protected System.Web.UI.WebControls.TextBox RapRagSoc;
        protected System.Web.UI.WebControls.TextBox RapPhone;
        protected System.Web.UI.WebControls.TextBox RapEmail;
        protected System.Web.UI.WebControls.LinkButton RapSubmit;
        protected System.Web.UI.WebControls.Label RapInfo;
        protected System.Web.UI.WebControls.Literal LblTitle;
        protected System.Web.UI.WebControls.Literal GroupDescription;
        protected Digita.Tustena.WebControls.TustenaRepeater SearchListRepeater;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.GoBackBtn Back;
        protected Digita.Tustena.Common.SheetPaging SheetP;
        protected Digita.Tustena.WebControls.TustenaTab visContact;
        protected System.Web.UI.WebControls.Repeater ViewForm;
        protected System.Web.UI.HtmlControls.HtmlTable ReferenceForm;
        protected System.Web.UI.WebControls.TextBox Referring_CompanyID;
        protected System.Web.UI.WebControls.TextBox Referring_ID;
        protected Digita.Tustena.WebControls.GoBackBtn BackCo;
        protected System.Web.UI.WebControls.LinkButton SubmitRef;
        protected System.Web.UI.WebControls.TextBox Referring_Title;
        protected System.Web.UI.WebControls.TextBox Referring_Name;
        protected System.Web.UI.WebControls.Label RequiredFieldValidatorCognome;
        protected System.Web.UI.WebControls.TextBox Referring_Surname;
        protected System.Web.UI.WebControls.TextBox Referring_CompanyTX;
        protected System.Web.UI.WebControls.Literal othercompaniestebles;
        protected System.Web.UI.WebControls.TextBox Referring_BusinessRole;
        protected System.Web.UI.WebControls.TextBox Referring_TaxIdentificationNumber;
        protected System.Web.UI.WebControls.TextBox Referring_VatID;
        protected System.Web.UI.WebControls.CheckBox Personal;
        protected System.Web.UI.WebControls.CheckBox Global;
        protected System.Web.UI.WebControls.Repeater RepCategories;
        protected System.Web.UI.WebControls.LinkButton RefreshRepCategories;
        protected System.Web.UI.WebControls.DropDownList dropZones;
        protected System.Web.UI.WebControls.TextBox SalesPersonID;
        protected System.Web.UI.WebControls.TextBox SalesPerson;
        protected System.Web.UI.WebControls.TextBox Referring_CODE;
        protected System.Web.UI.WebControls.TextBox Referring_Phone_1;
        protected System.Web.UI.WebControls.TextBox Referring_Phone_2;
        protected System.Web.UI.WebControls.TextBox Referring_MobilePhone_1;
        protected System.Web.UI.WebControls.TextBox Referring_MobilePhone_2;
        protected System.Web.UI.WebControls.TextBox Referring_Fax;
        protected System.Web.UI.WebControls.TextBox Referring_Skype;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator RegularExpressionValidator1;
        protected System.Web.UI.WebControls.TextBox Referring_EMail;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator RegularExpressionValidator2;
        protected System.Web.UI.WebControls.CheckBox Referring_MLFlag;
        protected System.Web.UI.WebControls.TextBox Referring_MLEmail;
        protected System.Web.UI.WebControls.TextBox Referring_BirthDay;
        protected System.Web.UI.WebControls.Label DateFormat;
        protected System.Web.UI.WebControls.TextBox Referring_BirthPlace;
        protected System.Web.UI.WebControls.RadioButtonList Referring_Sex;
        protected System.Web.UI.WebControls.TextBox Referring_Notes;
        protected System.Web.UI.WebControls.TextBox Referring_Address_1;
        protected System.Web.UI.WebControls.TextBox Referring_City_1;
        protected System.Web.UI.WebControls.TextBox Referring_Province_1;
        protected System.Web.UI.WebControls.TextBox Referring_State_1;
        protected System.Web.UI.WebControls.TextBox Referring_ZIPCode_1;
        protected System.Web.UI.WebControls.TextBox Referring_Address_2;
        protected System.Web.UI.WebControls.TextBox Referring_City_2;
        protected System.Web.UI.WebControls.TextBox Referring_Province_2;
        protected System.Web.UI.WebControls.TextBox Referring_State_2;
        protected System.Web.UI.WebControls.TextBox Referring_ZIPCode_2;
        protected Digita.Tustena.Common.FreeFields EditFreeFields;
        protected Digita.Tustena.GroupControl Groups;
        protected System.Web.UI.WebControls.TextBox Referring_Categories;
        protected System.Web.UI.WebControls.LinkButton Submit2;
        protected Digita.Tustena.WebControls.TustenaTab visActivity;
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
        protected Digita.Tustena.WebControls.TustenaTab visQuote;
        protected Digita.Tustena.ERP.CustomerQuote CustomerQuote1;
        protected Digita.Tustena.ERP.CustomerQuote Customerquote2;
        protected Digita.Tustena.WebControls.TustenaTab visDocuments;
        protected System.Web.UI.WebControls.LinkButton NewDoc;
        protected System.Web.UI.WebControls.Label FileRepInfo;
        protected System.Web.UI.WebControls.Repeater FileRep;
    }
}
