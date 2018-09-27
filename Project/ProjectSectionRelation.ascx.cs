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
    public partial class ProjectSectionRelation : System.Web.UI.UserControl
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
            string slist = "<select id=\"RelationSection{0}\" name=\"RelationSection{0}\" old=true>";
            slist += "<option value=0>Seleziona...</option>";
            foreach (DataRow dr in dt.Rows)
            {
                slist += string.Format("<option value={0}>{1}</option>", dr[0].ToString(), dr[1].ToString());
            }
            slist += "</select>";
            litSection1.Text = string.Format(slist, "1");
            litSection2.Text = string.Format(slist, "2");
        }

        public void FillRelations()
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            DataSet ds = DatabaseConnection.CreateDataset("select * from Project_Relations where projectid=" + prjID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                DataRow dr = ds.Tables[0].Rows[0];
                sb.AppendFormat("<script>FillRelation(0,'{0}','{1}','{2}','{3}','{4}');{5}", dr["FIRSTRIFID"].ToString(),dr["SECONDRIFID"].ToString(),dr["RELATIONTYPE"].ToString(),dr["DELAY"].ToString(),dr["ID"].ToString(), System.Environment.NewLine);
                if (ds.Tables[0].Rows.Count > 1)
                {
                    for (int i = 1; i < ds.Tables[0].Rows.Count; i++)
                    {
                        dr = ds.Tables[0].Rows[i];
                        sb.AppendFormat("addRelation();{5}FillRelation({6},'{0}','{1}','{2}','{3}','{4}');{5}", dr["FIRSTRIFID"].ToString(), dr["SECONDRIFID"].ToString(), dr["RELATIONTYPE"].ToString(), dr["DELAY"].ToString(), dr["ID"].ToString(), System.Environment.NewLine,i);
                    }
                }
                sb.Append("</script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "loadrelations", sb.ToString());
            }
        }

        public void SaveRelation()
        {
            if (Request["RelationSection1"] != null && Request["RelationSection1"] != "0")
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("FIRSTRIFID", long.Parse(Request["RelationSection1"]));
                    dg.Add("SECONDRIFID", long.Parse(Request["RelationSection2"]));
                    dg.Add("RELATIONTYPE", long.Parse(Request["RelationType"]));
                    if(Request["RelationDelay"]!=null && Request["RelationDelay"].Length>0)
                        dg.Add("DELAY", long.Parse(Request["RelationDelay"]));
                    else
                        dg.Add("DELAY", 0);
                    dg.Add("PROJECTID", prjID);
                    dg.Execute("PROJECT_RELATIONS", "ID=" + Request["RelationId"]);
                }

                int other = 1;
                while (Request["RelationSection1_" + other] != null && Request["RelationSection1_" + other] != "0")
                {
                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("FIRSTRIFID", long.Parse(Request["RelationSection1_" + other]));
                        dg.Add("SECONDRIFID", long.Parse(Request["RelationSection2_" + other]));
                        dg.Add("RELATIONTYPE", long.Parse(Request["RelationType_" + other]));
                        if (Request["RelationDelay_" + other] != null && Request["RelationDelay_" + other].Length>0 && Request["RelationDelay_" + other] != "0")
                            dg.Add("DELAY", long.Parse(Request["RelationDelay_" + other]));
                        else
                            dg.Add("DELAY", 0);
                        dg.Add("PROJECTID", prjID);
                        dg.Execute("PROJECT_RELATIONS", "ID=" + Request["RelationId_" + other]);
                    }
                    other++;
                }
            }
            if (Request["RelationToDelete"] != null && Request["RelationToDelete"].Length > 0)
            {
                string[] ids = Request["RelationToDelete"].Split('|');
                foreach (string id in ids)
                {
                    if (id.Length > 0 && int.Parse(id)>0)
                    {
                        DatabaseConnection.DoCommand("DELETE FROM PROJECT_RELATIONS WHERE ID=" + id);
                    }
                }
            }

            DataTable dtrelations = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT_RELATIONS WHERE PROJECTID="+prjID+" ORDER BY ID").Tables[0];
            foreach (DataRow dr in dtrelations.Rows)
            {
                if (dr["RELATIONTYPE"].ToString() == "0")
                    DatabaseConnection.DoCommand(string.Format(@"UPDATE PROJECT_TIMING
SET PLANNEDSTARTDATE=(SELECT PLANNEDSTARTDATE FROM PROJECT_TIMING WHERE IDRIF={0} AND RIFTYPE=0),
PLANNEDENDDATE=DATEADD(DD, DATEDIFF ( DD , PLANNEDSTARTDATE , PLANNEDENDDATE ) ,(SELECT PLANNEDSTARTDATE FROM PROJECT_TIMING WHERE IDRIF={0} AND RIFTYPE=0))
WHERE IDRIF={1} AND RIFTYPE=0", dr["FIRSTRIFID"].ToString(), dr["SECONDRIFID"].ToString()));
                else
                    DatabaseConnection.DoCommand(string.Format(@"UPDATE PROJECT_TIMING
SET PLANNEDSTARTDATE=(SELECT DATEADD(dd,{2}+1,PLANNEDENDDATE) FROM PROJECT_TIMING WHERE IDRIF={0} AND RIFTYPE=0),
PLANNEDENDDATE=DATEADD(dd, DATEDIFF ( DD , PLANNEDSTARTDATE , PLANNEDENDDATE ) ,(SELECT DATEADD(dd,{2}+1,PLANNEDENDDATE) FROM PROJECT_TIMING WHERE IDRIF={0} AND RIFTYPE=0))
WHERE IDRIF={1} AND RIFTYPE=0", dr["FIRSTRIFID"].ToString(), dr["SECONDRIFID"].ToString(), dr["DELAY"].ToString()));
            }



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
    }
}
