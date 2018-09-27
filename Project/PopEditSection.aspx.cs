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

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Project
{
    public partial class PopEditSection : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "endsession", "<script>opener.location.href=opener.location.href;self.close();</script>");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    ProjectSessions1.prjID = long.Parse(DatabaseConnection.SqlScalar("SELECT IDRIF FROM PROJECT_SECTION WHERE ID=" + Request["Sec"]));
                    ProjectSessions1.SectionId = long.Parse(Request["Sec"]);
                    ProjectSessions1.GanttEdit = true;
                    ProjectSessions1.FillSection();
                    switchControl.Text = "Relazioni";

                    ProjectSectionRelation1.Visible = false;
                    saveRelations.Visible = false;
                }
            }
        }

        protected void switchControl_Click(object sender, EventArgs e)
        {
            if (!ProjectSectionRelation1.Visible)
            {
                ProjectSectionRelation1.Visible = true;
                ProjectSectionRelation1.prjID = ProjectSessions1.prjID;
                ProjectSectionRelation1.FillSections();
                ProjectSectionRelation1.FillRelations();
                saveRelations.Visible = true;
                ProjectSessions1.Visible = false;
                switchControl.Visible=false;
            }
        }

        protected void saveRelations_Click(object sender, EventArgs e)
        {
            ProjectSectionRelation1.SaveRelation();
            Session["currentproject"] = ProjectSectionRelation1.prjID;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "reloadgantt", "<script>alert('Relazioni aggiornate.');opener.location=opener.location;self.close();</script>");
        }
    }
}
