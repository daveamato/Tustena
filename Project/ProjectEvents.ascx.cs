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
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using System.Text;

namespace Digita.Tustena.Project
{
    public partial class ProjectEvents : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
        }

        public void FillSections()
        {
            string query = "SELECT ID,TITLE FROM PROJECT_SECTION WHERE IDRIF=" + prjID;
            DataTable dt = DatabaseConnection.CreateDataset(query).Tables[0];
            string slist = "<select id=\"EventSection\" name=\"EventSection\" old=true>";
            slist += "<option value=0>Seleziona...</option>";
            foreach (DataRow dr in dt.Rows)
            {
                slist += string.Format("<option value={0}>{1}</option>", dr[0].ToString(), dr[1].ToString());
            }
            slist += "</select>";
            litSectionList.Text = slist;
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
            }
        }

        public void SaveEvents()
        {
            if (Request["EventTxt"] != null && Request["EventTxt"].Length > 0)
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("DESCRIPTION", Request["EventTxt"]);
                    dg.Add("EVENTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["EventDate"],UC.myDTFI)));
                    dg.Add("PROJECTID", prjID);
                    dg.Add("SECTIONID", long.Parse(Request["EventSection"]));
                    dg.Execute("PROJECT_EVENTS", "ID=" + Request["EventId"]);
                }

                int other = 1;
                while (Request["EventTxt_" + other] != null && Request["EventTxt_" + other].Length > 0)
                {
                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("DESCRIPTION", Request["EventTxt_" + other]);
                        dg.Add("EVENTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["EventDate_" + other], UC.myDTFI)));
                        dg.Add("PROJECTID", prjID);
                        dg.Add("SECTIONID", long.Parse(Request["EventSection_" + other]));
                        dg.Execute("PROJECT_EVENTS", "ID=" + Request["EventId_" + other]);
                    }
                    other++;
                }
            }

            if (Request["EventToDelete"] != null && Request["EventToDelete"].Length > 0)
            {
                string[] ids = Request["EventToDelete"].Split('|');
                foreach (string id in ids)
                {
                    if (id.Length > 0)
                    {
                        DatabaseConnection.DoCommand("DELETE FROM PROJECT_EVENTS WHERE ID=" + id);
                    }
                }
            }
        }

        public void FillEvents()
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            DataSet ds = DatabaseConnection.CreateDataset("select * from Project_events where projectid="+prjID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<script>FillEvent(0,'{0}','{1}','{2}',{4});{3}", G.ParseJSString(ds.Tables[0].Rows[0]["DESCRIPTION"].ToString()), UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0]["EVENTDATE"]).ToShortDateString(), ds.Tables[0].Rows[0]["SECTIONID"].ToString(), System.Environment.NewLine, ds.Tables[0].Rows[0]["ID"].ToString());
                if (ds.Tables[0].Rows.Count > 1)
                {
                    for(int i=1;i<ds.Tables[0].Rows.Count;i++)
                        sb.AppendFormat("addEvent();{3}FillEvent({4},'{0}','{1}','{2}',{5});{3}", G.ParseJSString(ds.Tables[0].Rows[i]["DESCRIPTION"].ToString()), UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[i]["EVENTDATE"]).ToShortDateString(), ds.Tables[0].Rows[i]["SECTIONID"].ToString(), System.Environment.NewLine, i, ds.Tables[0].Rows[i]["ID"].ToString());
                }
                sb.Append("</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "loadevents", sb.ToString());
            }
        }
    }
}
