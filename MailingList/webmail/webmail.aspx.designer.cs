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


namespace Digita.Tustena.WebMail {

    public partial class webmail {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.WebControls.LinkButton MailIn;
        protected System.Web.UI.WebControls.LinkButton NewMail;
        protected System.Web.UI.WebControls.LinkButton ReadMail;
        protected System.Web.UI.WebControls.Label lblError;
        protected Digita.Tustena.MailingList.webmail.mailout SendNewMail;
        protected System.Web.UI.WebControls.Panel login;
        protected System.Web.UI.WebControls.TextBox mailserver;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
        protected System.Web.UI.WebControls.TextBox userid;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
        protected System.Web.UI.WebControls.TextBox password;
        protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator3;
        protected System.Web.UI.WebControls.Panel messages;
        protected System.Web.UI.WebControls.Label Lblmsginfo;
        protected System.Web.UI.WebControls.Repeater Repeatermsg;
        protected System.Web.UI.HtmlControls.HtmlTable tblpaging;
        protected System.Web.UI.WebControls.LinkButton PreviousMessPage;
        protected System.Web.UI.WebControls.Label MessPageID;
        protected System.Web.UI.WebControls.LinkButton NextMessPage;
        protected System.Web.UI.WebControls.DataGrid dgmessages;
    }
}
