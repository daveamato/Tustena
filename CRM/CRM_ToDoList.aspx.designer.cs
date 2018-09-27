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

    public partial class CRMToDoList {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm Form1;
        protected System.Web.UI.WebControls.LinkButton NewTask;
        protected System.Web.UI.WebControls.TextBox FindTxt;
        protected System.Web.UI.WebControls.LinkButton BtnSearch;
        protected System.Web.UI.WebControls.Repeater RepTask;
        protected System.Web.UI.WebControls.Label RepeaterInfo;
        protected System.Web.UI.WebControls.Repeater RepTask2;
        protected System.Web.UI.WebControls.Label RepeaterInfo2;
        protected System.Web.UI.HtmlControls.HtmlTable TaskCard;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator OwnerValidator;
        protected System.Web.UI.WebControls.TextBox ToDoList_OwnerID;
        protected System.Web.UI.WebControls.TextBox ToDoList_Owner;
        protected System.Web.UI.HtmlControls.HtmlGenericControl PopAccount;
        protected System.Web.UI.WebControls.CompareValidator CvDateFine;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator ExpirationDateValidator;
        protected System.Web.UI.WebControls.TextBox ToDoList_ExpirationDate;
        protected System.Web.UI.HtmlControls.HtmlGenericControl PopAccount4;
        protected System.Web.UI.WebControls.TextBox ToDoList_CompanyID;
        protected System.Web.UI.WebControls.TextBox ToDoList_CompanyName;
        protected System.Web.UI.HtmlControls.HtmlGenericControl PopAccount2;
        protected System.Web.UI.WebControls.TextBox ToDoList_OpportunityID;
        protected System.Web.UI.WebControls.Label LinkOpportunity;
        protected System.Web.UI.WebControls.TextBox ToDoList_OpportunityTitle;
        protected System.Web.UI.HtmlControls.HtmlGenericControl PopAccount3;
        protected System.Web.UI.WebControls.TextBox ToDoList_Task;
        protected System.Web.UI.WebControls.TextBox ToDoList_Outcome;
        protected System.Web.UI.WebControls.Literal TaskID;
        protected System.Web.UI.WebControls.LinkButton SaveTask;
    }
}
