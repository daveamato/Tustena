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

    public partial class NewUser {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlTableCell tblAdminUser;
        protected System.Web.UI.WebControls.Literal TitlePage;
        protected System.Web.UI.WebControls.LinkButton LblNewUser;
        protected System.Web.UI.WebControls.TextBox Find;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral1;
        protected System.Web.UI.WebControls.DropDownList ListGroups;
        protected System.Web.UI.WebControls.LinkButton BtnFind;
        protected System.Web.UI.WebControls.Label TitlePageRight;
        protected System.Web.UI.WebControls.Literal LtrNewUser;
        protected System.Web.UI.WebControls.Literal HelpLabel;
        protected System.Web.UI.WebControls.Label LblHeader;
        protected System.Web.UI.WebControls.Repeater Repeater1;
        protected System.Web.UI.WebControls.Label Repeater1Info;
        protected System.Web.UI.WebControls.Label LabelInfo;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTabberRight TustenaTabberRight1;
        protected System.Web.UI.WebControls.LinkButton Submit2;
        protected Digita.Tustena.WebControls.TustenaTab visInfoUser;
        protected System.Web.UI.WebControls.DomValidators.DomValidationSummary valSum;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral2;
        protected System.Web.UI.WebControls.LinkButton Rubric2;
        protected System.Web.UI.WebControls.LinkButton ExistRubric2;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral3;
        protected System.Web.UI.WebControls.DomValidators.RegexDomValidator RegularExpressionValidator1;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_UserNameValidator;
        protected System.Web.UI.WebControls.TextBox F_UserName;
        protected System.Web.UI.WebControls.TextBox F_UID;
        protected System.Web.UI.WebControls.TextBox Mode;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral4;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_PasswordValidator;
        protected System.Web.UI.WebControls.TextBox F_Password;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral5;
        protected System.Web.UI.WebControls.TextBox Name;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral6;
        protected System.Web.UI.WebControls.TextBox Surname;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral7;
        protected System.Web.UI.WebControls.TextBox EMail;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral8;
        protected System.Web.UI.WebControls.CheckBox PersonalContact;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral9;
        protected System.Web.UI.WebControls.CheckBox FlagModifyAppointment;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral24;
        protected System.Web.UI.WebControls.CheckBox FlagCreateContact;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral10;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_GroupValidator;
        protected System.Web.UI.WebControls.DropDownList F_Group;
        protected System.Web.UI.HtmlControls.HtmlTableRow SecondaryGroup;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral11;
        protected System.Web.UI.WebControls.Repeater RepGroup;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral12;
        protected System.Web.UI.WebControls.DropDownList F_Officeso;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral13;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_WorkStartValidator;
        protected System.Web.UI.WebControls.TextBox F_WorkStart;
        protected System.Web.UI.HtmlControls.HtmlGenericControl BtnStartWork;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral14;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator EndWorkValidator;
        protected System.Web.UI.WebControls.TextBox EndWork;
        protected System.Web.UI.HtmlControls.HtmlGenericControl BtnEndWork;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral15;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_WorkStart2Validator;
        protected System.Web.UI.WebControls.TextBox F_WorkStart2;
        protected System.Web.UI.HtmlControls.HtmlGenericControl BtnStartWork2;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral16;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator F_Fine_Lavoro2Validator;
        protected System.Web.UI.WebControls.TextBox EndWork2;
        protected System.Web.UI.HtmlControls.HtmlGenericControl BtnEndWork2;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral17;
        protected System.Web.UI.WebControls.CheckBoxList RecurrenceWeeklyDays;
        protected System.Web.UI.HtmlControls.HtmlTableRow trZone;
        protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
        protected System.Web.UI.WebControls.RadioButtonList FlagCommercial;
        protected System.Web.UI.WebControls.DropDownList IdManager;
        protected System.Web.UI.WebControls.Repeater RepeaterZones;
        protected System.Web.UI.WebControls.Table WorkTimeForm;
        protected System.Web.UI.WebControls.CheckBox Responsable;
        protected System.Web.UI.WebControls.CheckBox Subaltern;
        protected System.Web.UI.WebControls.DropDownList IdResponsable;
        protected Digita.Tustena.WebControls.TustenaTab visConfig;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral18;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral19;
        protected System.Web.UI.WebControls.TextBox Paging;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral20;
        protected System.Web.UI.WebControls.DomValidators.RangeDomValidator valSessionTimeout;
        protected System.Web.UI.WebControls.TextBox SessionTimeout;
        protected System.Web.UI.WebControls.TextBox F_IdRubrica;
        protected System.Web.UI.WebControls.TextBox ViewF_IdRubrica;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral23;
        protected System.Web.UI.WebControls.DropDownList PriceList;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral21;
        protected System.Web.UI.WebControls.CheckBox FullScreen;
        protected Digita.Tustena.WebControls.LocalizedLiteral Localizedliteral25;
        protected System.Web.UI.WebControls.CheckBox ViewBirthDate;
        protected System.Web.UI.WebControls.Label LblCSSGuid;
        protected System.Web.UI.WebControls.TextBox CSSGuid;
        protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral22;
        protected System.Web.UI.WebControls.DropDownList DropCulture;
        protected System.Web.UI.WebControls.DropDownList TimeZoneNew;
        protected Digita.Tustena.WebControls.TustenaTab visMail;
        protected System.Web.UI.WebControls.RadioButtonList SecureMail;
        protected System.Web.UI.WebControls.TextBox MailServer;
        protected System.Web.UI.WebControls.TextBox MailUser;
        protected System.Web.UI.WebControls.TextBox MailPassword;
        protected Digita.Tustena.WebControls.TustenaTab visAgenda;
        protected System.Web.UI.WebControls.DropDownList DropFirstDay;
        protected Digita.Tustena.SelectOffice Office;
        protected System.Web.UI.HtmlControls.HtmlTableRow TROfficeTitle;
        protected System.Web.UI.HtmlControls.HtmlTableRow TROfficeFields;
        protected System.Web.UI.WebControls.ListBox OfficesAll;
        protected System.Web.UI.WebControls.ListBox OfficesSel;
        protected System.Web.UI.WebControls.TextBox OfficeSelText;
        protected Digita.Tustena.WebControls.TustenaTab visHomePage;
        protected System.Web.UI.WebControls.Repeater RepHomePage;
        protected Digita.Tustena.WebControls.TustenaTab visGroups;
        protected Digita.Tustena.GroupControl GroupControl;
        protected System.Web.UI.WebControls.Label LabelInfoX;
    }
}
