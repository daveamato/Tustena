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

    public partial class AdminCompany {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral1;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral2;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTab TabCompany;
        protected System.Web.UI.HtmlControls.HtmlTable ContactForm;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral6;
        protected System.Web.UI.WebControls.Label CompanyName;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral7;
        protected System.Web.UI.WebControls.TextBox PhoneNumber;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral8;
        protected System.Web.UI.WebControls.TextBox Fax;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral9;
        protected System.Web.UI.WebControls.TextBox Email;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral10;
        protected System.Web.UI.WebControls.TextBox WebSite;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral11;
        protected System.Web.UI.WebControls.Label PermittedKb;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral12;
        protected System.Web.UI.WebControls.Label BusyKb;
        protected Digita.Tustena.WebControls.LocalizedLiteral Localizedliteral31;
        protected System.Web.UI.WebControls.Label FileIndexes;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral13;
        protected System.Web.UI.WebControls.TextBox AddressInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral14;
        protected System.Web.UI.WebControls.TextBox CityInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral15;
        protected System.Web.UI.WebControls.TextBox ProvinceInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral16;
        protected System.Web.UI.WebControls.TextBox RegionInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral17;
        protected System.Web.UI.WebControls.TextBox CountryInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral18;
        protected System.Web.UI.WebControls.TextBox ZipInvoice;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral19;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral20;
        protected System.Web.UI.WebControls.TextBox CompanyTextboxID;
        protected System.Web.UI.WebControls.TextBox CompanyTextbox;
        protected System.Web.UI.HtmlControls.HtmlTableRow LeadModule;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral21;
        protected System.Web.UI.WebControls.TextBox LeadDays;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral22;
        protected System.Web.UI.WebControls.TextBox Voip;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral23;
        protected System.Web.UI.WebControls.TextBox InterPrefix;
        protected System.Web.UI.WebControls.RadioButtonList checkLogo;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral24;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral25;
        protected System.Web.UI.WebControls.Label WebService_guid;
        protected System.Web.UI.WebControls.TextBox WebService_pin;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral26;
        protected System.Web.UI.WebControls.TextBox WebService_OwnerID;
        protected System.Web.UI.WebControls.TextBox WebService_Owner;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral27;
        protected System.Web.UI.WebControls.TextBox WebGate_OwnerID;
        protected System.Web.UI.WebControls.TextBox WebGate_Owner;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral28;
        protected System.Web.UI.WebControls.TextBox WebGate_GroupID;
        protected System.Web.UI.WebControls.TextBox WebGate_Group;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral29;
        protected System.Web.UI.WebControls.TextBox WebGate_NotifyID;
        protected System.Web.UI.WebControls.TextBox WebGate_Notify;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral30;
        protected System.Web.UI.WebControls.TextBox WebGate_WebSite;
        protected System.Web.UI.WebControls.Label Info;
        protected System.Web.UI.WebControls.LinkButton Submit;
        protected Digita.Tustena.WebControls.TustenaTab TabGroup;
        protected Digita.Tustena.Admin.groups groups;
        protected Digita.Tustena.WebControls.TustenaTab TabOffice;
        protected Digita.Tustena.Admin.offices Offices;
    }
}
