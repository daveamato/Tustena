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


namespace Digita.Tustena.Project {

    public partial class ProjectManagerStandard {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm form1;
        protected System.Web.UI.WebControls.Literal TitlePage;
        protected System.Web.UI.WebControls.LinkButton LblNewProject;
        protected Digita.Tustena.WebControls.TustenaRepeater NewRepeater1;
        protected Digita.Tustena.WebControls.TustenaTabber Tabber;
        protected Digita.Tustena.WebControls.TustenaTabberRight TustenaTabberRight1;
        protected System.Web.UI.WebControls.LinkButton CloseProject;
        protected Digita.Tustena.WebControls.TustenaTab visProject;
        protected System.Web.UI.HtmlControls.HtmlTable ProjectTable;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator prjTitleValidator;
        protected System.Web.UI.WebControls.TextBox prjTitle;
        protected System.Web.UI.WebControls.Literal prjID;
        protected System.Web.UI.WebControls.TextBox prjDescription;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator prjOwnerIDValidator;
        protected System.Web.UI.WebControls.TextBox prjOwnerID;
        protected System.Web.UI.WebControls.TextBox prjOwner;
        protected System.Web.UI.WebControls.TextBox OtherOwnerID;
        protected System.Web.UI.WebControls.TextBox OtherOwnerTxt;
        protected System.Web.UI.WebControls.CheckBox prjOpen;
        protected System.Web.UI.WebControls.CheckBox prjSuspend;
        protected System.Web.UI.HtmlControls.HtmlTable tblEvents;
        protected Digita.Tustena.Project.ProjectEvents ProjectEvents1;
        protected System.Web.UI.HtmlControls.HtmlTable tblRelations;
        protected Digita.Tustena.Project.ProjectSectionRelation ProjectSectionRelation1;
        protected System.Web.UI.HtmlControls.HtmlTable tblSendmail;
        protected System.Web.UI.WebControls.RadioButtonList selectMailType;
        protected System.Web.UI.WebControls.RadioButtonList selectMailSend;
        protected System.Web.UI.WebControls.TextBox MailOwnerID;
        protected System.Web.UI.WebControls.TextBox MailOwnerType;
        protected System.Web.UI.WebControls.TextBox MailOwnerRealID;
        protected System.Web.UI.WebControls.TextBox MailOwner;
        protected System.Web.UI.WebControls.LinkButton btnSendMails;
        protected System.Web.UI.WebControls.LinkButton btnSaveprj;
        protected System.Web.UI.WebControls.Label sss;
        protected Digita.Tustena.WebControls.TustenaTab visSections;
        protected Digita.Tustena.Project.ProjectSessions ProjectSessions1;
        protected Digita.Tustena.WebControls.TustenaTab visTeams;
        protected Digita.Tustena.Project.TeamManager TeamManager1;
    }
}
