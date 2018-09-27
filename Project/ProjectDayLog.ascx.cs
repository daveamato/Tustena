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
using Ajax;
using Digita.Tustena.Database;
using Digita.Tustena.Core;

namespace Digita.Tustena.Project
{
    public partial class ProjectDayLog : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            this.InitAjax();
        }

        public long prjID
        {
            get
            {
                object o = this.ViewState["_prjID" + this.ID];
                if (o == null)
                    return 0;
                else
                    return (long)o;
            }
            set
            {
                this.ViewState["_prjID" + this.ID] = value;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "prj", string.Format("<script>var ToDoProject={0}</script>", value));

            }
        }

        private void InitAjax()
        {
            Manager.Register((Page)this.Parent, "Ajax.Project", Debug.None);
        }

        [Method]
        public DataSet FillSection(string type)
        {
            UserConfig UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            string q = string.Format(@"SELECT PROJECT_SECTION.TITLE, PROJECT_SECTION.ID FROM PROJECT_SECTION INNER JOIN
                      PROJECT_SECTION_MEMBERS ON PROJECT_SECTION.ID = PROJECT_SECTION_MEMBERS.IDSECTIONRIF INNER JOIN
                      PROJECT_MEMBERS ON PROJECT_SECTION_MEMBERS.MEMBERID = PROJECT_MEMBERS.ID
                      WHERE (PROJECT_MEMBERS.USERID = {0}) AND (PROJECT_MEMBERS.TYPE = {1}) AND (PROJECT_SECTION.IDRIF = {2})",UC.UserId,type,prjID);
            return DatabaseConnection.CreateDataset(q);

        }
    }
}
