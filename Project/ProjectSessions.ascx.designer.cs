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

    public partial class ProjectSessions {
        protected Digita.Tustena.WebControls.jscontrolid jsc;
        protected System.Web.UI.WebControls.TextBox SelectSession;
        protected System.Web.UI.HtmlControls.HtmlTable MainTable;
        protected System.Web.UI.WebControls.LinkButton btnNewSession;
        protected System.Web.UI.WebControls.Literal lblSection;
        protected System.Web.UI.WebControls.LinkButton btnSelectSession;
        protected System.Web.UI.HtmlControls.HtmlTable SessionTable;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator SectionNameValidator;
        protected System.Web.UI.WebControls.TextBox SectionName;
        protected System.Web.UI.WebControls.TextBox SectionDescription;
        protected System.Web.UI.WebControls.DropDownList SectionParent;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator SectionOwnerValidator;
        protected System.Web.UI.WebControls.TextBox SectionOwnerID;
        protected System.Web.UI.WebControls.TextBox SectionOwner;
        protected System.Web.UI.WebControls.Literal SectionProgress;
        protected System.Web.UI.HtmlControls.HtmlSelect FilterColor;
        protected System.Web.UI.HtmlControls.HtmlTableRow VariationRow;
        protected System.Web.UI.WebControls.Literal LitVariation;
        protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator PlannedStartDateValidator;
        protected System.Web.UI.WebControls.TextBox PlannedStartDate;
        protected System.Web.UI.WebControls.TextBox PlannedEndDate;
        protected System.Web.UI.WebControls.TextBox PlannedMinuteDuration;
        protected System.Web.UI.WebControls.TextBox RealStartDate;
        protected System.Web.UI.WebControls.TextBox RealEndDate;
        protected System.Web.UI.WebControls.TextBox CurrentMinuteDuration;
        protected System.Web.UI.WebControls.RadioButtonList CostType;
        protected System.Web.UI.WebControls.TextBox SectionAmount;
        protected System.Web.UI.WebControls.LinkButton btnSaveSection;
        protected Digita.Tustena.Project.ProjectToDo ProjectToDo1;
    }
}
