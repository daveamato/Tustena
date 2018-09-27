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

    public partial class Events {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlTable TEvento;
        protected System.Web.UI.HtmlControls.HtmlTable AppointmentCard;
        protected System.Web.UI.WebControls.TextBox NewId;
        protected System.Web.UI.WebControls.DropDownList UserApp;
        protected System.Web.UI.WebControls.Label DateEr1;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator RequiredFieldValidatorData;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator cvF_Datainizio;
        protected System.Web.UI.WebControls.TextBox StartDate;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator RequiredFieldValidatorOra1;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator RegularExpressionValidatorOra1;
        protected System.Web.UI.WebControls.TextBox StartHour;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator RequiredFieldValidatorTitle;
        protected System.Web.UI.WebControls.TextBox AgTitle;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator RequiredFieldValidatorNote;
        protected System.Web.UI.WebControls.TextBox Note;
        protected System.Web.UI.HtmlControls.HtmlTableRow HeaderRecurrence;
        protected System.Web.UI.HtmlControls.HtmlTableRow CorpoRecurrence;
        protected System.Web.UI.WebControls.CheckBox CheckRecurrent;
        protected System.Web.UI.WebControls.DropDownList RecType;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator CvRecDate;
        protected System.Web.UI.WebControls.TextBox RecDateStart;
        protected System.Web.UI.WebControls.DomValidators.CompareDomValidator cvRecDataFine;
        protected System.Web.UI.WebControls.TextBox RecEndDate;
        protected System.Web.UI.WebControls.DropDownList RecMode;
        protected System.Web.UI.WebControls.TextBox RecDayDays;
        protected System.Web.UI.WebControls.CheckBox RecWorkingDay;
        protected System.Web.UI.WebControls.TextBox RecSettSS;
        protected System.Web.UI.WebControls.CheckBoxList RecSetDays;
        protected System.Web.UI.WebControls.TextBox RecMonthlyDays;
        protected System.Web.UI.WebControls.TextBox RecMonthlyMonths;
        protected System.Web.UI.WebControls.DropDownList RecMonthlyDayPU;
        protected System.Web.UI.WebControls.DropDownList RecMonthlyDayDays;
        protected System.Web.UI.WebControls.TextBox RecMonthlyDayMonths;
        protected System.Web.UI.WebControls.TextBox RecYearDays;
        protected System.Web.UI.WebControls.DropDownList RecYearMonths;
        protected System.Web.UI.WebControls.DropDownList RecYearDayPU;
        protected System.Web.UI.WebControls.DropDownList RecYearDayDays;
        protected System.Web.UI.WebControls.DropDownList RecYearDayMonths;
        protected System.Web.UI.WebControls.LinkButton Submit;
        protected System.Web.UI.WebControls.Literal Info;
        protected System.Web.UI.WebControls.Repeater ViewAppointmentForm;
        protected System.Web.UI.WebControls.Literal AgendaOwner;
    }
}
