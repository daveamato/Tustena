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
using Digita.Tustena.Core;
using System.Text;

namespace Digita.Tustena.Project
{
    public partial class ProjectToDo : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        override protected void OnInit(EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            base.OnInit(e);
        }

        public void BindToDo()
        {
           Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"prj","<script>var ToDoProject="+prjID+";</script>");
        }

        public void FillToDo()
        {
            string q = @"SELECT PROJECT_SECTION_MEMBERS.*, PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_TIMING.REALSTARTDATE, PROJECT_TIMING.REALENDDATE, PROJECT_TIMING.PROGRESS, PROJECT_TIMING.PLANNEDMINUTEDURATION, PROJECT_TIMING.CURRENTMINUTEDURATION, PROJECT_TIMING.WEIGHT, PROJECT_TIMING.ID AS TIMINGID
 FROM PROJECT_SECTION_MEMBERS RIGHT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION_MEMBERS.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF=" + sectionID;
            DataSet ds = DatabaseConnection.CreateDataset(q);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<script>{0}",System.Environment.NewLine);

                sb.Append("FillToDo('',");
                sb.AppendFormat("'{0}',",G.ParseJSString(ds.Tables[0].Rows[0]["TODODESCRIPTION"].ToString()));

                DataRow dr = DatabaseConnection.CreateDataset("SELECT USERID,TYPE FROM PROJECT_MEMBERS WHERE ID="+ds.Tables[0].Rows[0]["MEMBERID"].ToString()).Tables[0].Rows[0];

                sb.AppendFormat("{0},",dr["USERID"].ToString());
                switch(dr["TYPE"].ToString())
                {
                    case "0":
                    case "2":
                        sb.AppendFormat("'{0}',",G.ParseJSString(DatabaseConnection.SqlScalar("SELECT ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') FROM ACCOUNT WHERE UID="+dr["USERID"].ToString())));
                        break;
                    case "1":
                        sb.AppendFormat("'{0}',",G.ParseJSString(DatabaseConnection.SqlScalar("select ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') from BASE_CONTACTS WHERE ID="+dr["USERID"].ToString())));
                        break;
                }

                sb.AppendFormat("{0},",dr["TYPE"].ToString());
                if (ds.Tables[0].Rows[0]["plannedstartdate"] != System.DBNull.Value)
                    sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0]["plannedstartdate"]).ToShortDateString());
                else
                    sb.Append("'',");
                if (ds.Tables[0].Rows[0]["plannedenddate"] != System.DBNull.Value)
                    sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0]["plannedenddate"]).ToShortDateString());
                else
                    sb.Append("'',");
                sb.AppendFormat("'{0}',",G.ParseJSString(ds.Tables[0].Rows[0]["plannedminuteduration"].ToString()));
                if (ds.Tables[0].Rows[0]["realstartdate"] != System.DBNull.Value)
                    sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0]["realstartdate"]).ToShortDateString());
                else
                    sb.Append("'',");
                if (ds.Tables[0].Rows[0]["realenddate"] != System.DBNull.Value)
                    sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0]["realenddate"]).ToShortDateString());
                else
                    sb.Append("'',");
                sb.AppendFormat("'{0}',",G.ParseJSString(ds.Tables[0].Rows[0]["currentminuteduration"].ToString()));
                sb.AppendFormat("{0},", ds.Tables[0].Rows[0]["ID"].ToString());
                sb.AppendFormat("{0},", ((byte)ds.Tables[0].Rows[0]["PROGRESS"]).ToString());
                sb.AppendFormat("{0},", ds.Tables[0].Rows[0]["MEMBERID"].ToString());
                sb.AppendFormat("{0}", ds.Tables[0].Rows[0]["WEIGHT"].ToString());
                sb.AppendFormat(");{0}", System.Environment.NewLine);

                string variation=string.Empty;

                object newdate = DatabaseConnection.SqlScalartoObj("select newstartdate from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[0]["TIMINGID"].ToString() + " order by newstartdate desc");
                if (newdate!=null && newdate != System.DBNull.Value)
                {
                    variation += "Data inizio: " + UC.LTZ.ToLocalTime(Convert.ToDateTime(newdate)).ToShortDateString()+"<br>";
                }
                newdate = DatabaseConnection.SqlScalartoObj("select newplanneddate from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[0]["TIMINGID"].ToString() + " order by newplanneddate desc");
                if (newdate != null && newdate != System.DBNull.Value)
                {
                     variation += "Data fine: " + UC.LTZ.ToLocalTime(Convert.ToDateTime(newdate)).ToShortDateString()+"<br>";
                }

                string addminute = DatabaseConnection.SqlScalar("select sum(plannedminute) from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[0]["TIMINGID"].ToString());
                if (addminute.Length > 0)
                {
                    int hourvariation = int.Parse(addminute);
                    if (hourvariation != 0)
                    {
                        variation += "Ore previste: " + G.ParseJSString((int.Parse(ds.Tables[0].Rows[0]["plannedminuteduration"].ToString()) + hourvariation).ToString());
                    }
                }

                if (variation.Length > 0)
                {
                    sb.AppendFormat("document.getElementById(\"VariationDetail\").innerHTML = '{0}';", variation);
                    sb.AppendFormat("document.getElementById(\"imgVariation\").style.display='';");
                }
                else
                    sb.AppendFormat("document.getElementById(\"imgVariation\").style.display='none';");

                for (int tocount = 1; tocount < ds.Tables[0].Rows.Count; tocount++)
                {
                    sb.AppendFormat("addTodo();{0}", System.Environment.NewLine);
                    sb.AppendFormat("FillToDo('_{0}',",tocount);
                    sb.AppendFormat("'{0}',", G.ParseJSString(ds.Tables[0].Rows[tocount]["TODODESCRIPTION"].ToString()));

                    dr = DatabaseConnection.CreateDataset("SELECT USERID,TYPE FROM PROJECT_MEMBERS WHERE ID="+ ds.Tables[0].Rows[tocount]["MEMBERID"].ToString()).Tables[0].Rows[0];

                    sb.AppendFormat("{0},", dr["USERID"].ToString());
                    switch (dr["TYPE"].ToString())
                    {
                        case "0":
                        case "2":
                            sb.AppendFormat("'{0}',", G.ParseJSString(DatabaseConnection.SqlScalar("SELECT ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') FROM ACCOUNT WHERE UID=" + dr["USERID"].ToString())));
                            break;
                        case "1":
                            sb.AppendFormat("'{0}',", G.ParseJSString(DatabaseConnection.SqlScalar("select ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') from BASE_CONTACTS WHERE ID=" + dr["USERID"].ToString())));
                            break;
                    }

                    sb.AppendFormat("{0},", dr["TYPE"].ToString());

                    if (ds.Tables[0].Rows[tocount]["plannedstartdate"] != System.DBNull.Value)
                        sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[tocount]["plannedstartdate"]).ToShortDateString());
                    else
                        sb.Append("'',");
                    if (ds.Tables[0].Rows[tocount]["plannedenddate"] != System.DBNull.Value)
                        sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[tocount]["plannedenddate"]).ToShortDateString());
                    else
                        sb.Append("'',");

                    sb.AppendFormat("'{0}',", G.ParseJSString(ds.Tables[0].Rows[tocount]["plannedminuteduration"].ToString()));

                    if (ds.Tables[0].Rows[tocount]["realstartdate"] != System.DBNull.Value)
                        sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[tocount]["realstartdate"]).ToShortDateString());
                    else
                        sb.Append("'',");
                    if (ds.Tables[0].Rows[tocount]["realenddate"] != System.DBNull.Value)
                        sb.AppendFormat("'{0}',", UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[tocount]["realenddate"]).ToShortDateString());
                    else
                        sb.Append("'',");

                    sb.AppendFormat("'{0}',", G.ParseJSString(ds.Tables[0].Rows[tocount]["currentminuteduration"].ToString()));
                    sb.AppendFormat("{0},", ds.Tables[0].Rows[tocount]["ID"].ToString());
                    sb.AppendFormat("{0},", ((byte)ds.Tables[0].Rows[tocount]["PROGRESS"]).ToString());
                    sb.AppendFormat("{0},", ds.Tables[0].Rows[tocount]["MEMBERID"].ToString());
                    sb.AppendFormat("{0}", ds.Tables[0].Rows[tocount]["WEIGHT"].ToString());
                    sb.AppendFormat(");{0}", System.Environment.NewLine);

                    variation = string.Empty;

                    newdate = DatabaseConnection.SqlScalartoObj("select newstartdate from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[tocount]["TIMINGID"].ToString() + " order by newstartdate desc");
                    if (newdate != null && newdate != System.DBNull.Value)
                    {
                        variation += "Data inizio: " + UC.LTZ.ToLocalTime(Convert.ToDateTime(newdate)).ToShortDateString()+"<br>";
                    }
                    newdate = DatabaseConnection.SqlScalartoObj("select newplanneddate from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[tocount]["TIMINGID"].ToString() + " order by newplanneddate desc");
                    if (newdate != null && newdate != System.DBNull.Value)
                    {
                        variation += "Data fine: " + UC.LTZ.ToLocalTime(Convert.ToDateTime(newdate)).ToShortDateString()+"<br>";
                    }

                    addminute = DatabaseConnection.SqlScalar("select sum(plannedminute) from PROJECT_TIMING_VARIATION where idtiming=" + ds.Tables[0].Rows[tocount]["TIMINGID"].ToString());
                    if (addminute.Length > 0)
                    {
                        int hourvariation = int.Parse(addminute);
                        if (hourvariation != 0)
                        {
                            variation += "Ore previste: " + G.ParseJSString((int.Parse(ds.Tables[0].Rows[tocount]["plannedminuteduration"].ToString()) + hourvariation).ToString());
                        }
                    }

                    if (variation.Length > 0)
                    {
                        sb.AppendFormat("document.getElementById(\"VariationDetail_{1}\").innerHTML = '{0}';", variation,tocount);
                        sb.AppendFormat("document.getElementById(\"imgVariation_{0}\").style.display='';",tocount);
                    }
                    else
                        sb.AppendFormat("document.getElementById(\"imgVariation_{0}\").style.display='none';",tocount);
                }

                sb.AppendFormat("</script>{0}", System.Environment.NewLine);

                Page.ClientScript.RegisterStartupScript(this.GetType(), "filltodo", sb.ToString());
            }
        }

        public void UpdateToDo()
        {
            UpdateToDo(DateTime.UtcNow);
        }

        public void UpdateToDo(DateTime startdate)
        {
            if (Request["ToDoOwnerID"] != null && Request["ToDoOwnerID"].Length > 0)
            {
                string newid = Request["ToDoId"].ToString();
                bool newtodo = false;
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("IDSECTIONRIF", sectionID);
                    dg.Add("MEMBERID", Request["ToDoOwnerRealID"]);
                    dg.Add("TODODESCRIPTION", Request["JobTxt"]);

                    if (Request["ToDoId"]=="-1")
                    {
                        dg.Add("CREATEDDATE", DateTime.UtcNow);
                        dg.Add("CREATEDBYID", UC.UserId);
                    }
                    dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                    dg.Add("LASTMODIFIEDBYID", UC.UserId);

                    object nid = dg.Execute("PROJECT_SECTION_MEMBERS", "ID=" + Request["ToDoId"], DigiDapter.Identities.Identity);
                    if (dg.RecordInserted)
                    {
                        newid = nid.ToString();
                        newtodo = true;
                    }
                }

                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("RIFTYPE", 1);
                    if (newtodo)
                        dg.Add("IDRIF", newid);

                    if (Request["PlannedStartDate"] != null && Request["PlannedStartDate"].Length > 0)
                        dg.Add("PLANNEDSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["PlannedStartDate"], UC.myDTFI)));
                    else
                        dg.Add("PLANNEDSTARTDATE", startdate);
                    if (Request["PlannedEndDate"].Length > 0)
                        dg.Add("PLANNEDENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["PlannedEndDate"], UC.myDTFI)));
                    if (Request["RealStartDate"].Length > 0)
                        dg.Add("REALSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["RealStartDate"], UC.myDTFI)));
                    if (Request["RealEndDate"].Length > 0)
                        dg.Add("REALENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["RealEndDate"], UC.myDTFI)));

                    dg.Add("PLANNEDMINUTEDURATION", (Request["PlannedMinuteDuration"].Length > 0) ? int.Parse(Request["PlannedMinuteDuration"]) : 0);
                    dg.Add("CURRENTMINUTEDURATION", (Request["CurrentMinuteDuration"].Length > 0) ? int.Parse(Request["CurrentMinuteDuration"]) : 0);
                    int prog = (Request["ToDoProgress"].Length > 0) ? int.Parse(Request["ToDoProgress"]) : 0;
                    if (prog > 100) prog = 100;
                    dg.Add("PROGRESS", prog);
                    dg.Add("WEIGHT", ((Request["ToDoWeight"].Length > 0) ? int.Parse(Request["ToDoWeight"]) : 0));
                    if (Request["ToDoComplete"] == "1")
                    {
                        using (DigiDapter dgvariation = new DigiDapter())
                        {
                            dgvariation.Add("IDTIMING", long.Parse(DatabaseConnection.SqlScalar("select id from PROJECT_TIMING where RIFTYPE=1 AND IDRIF=" + newid)));
                            dgvariation.Add("PLANNEDMINUTE", 0);
                            dgvariation.Add("DESCRIPTION", "Azione completata");
                            dgvariation.Add("NEWPLANNEDDATE", DateTime.UtcNow);
                            dgvariation.Add("CREATEDBYID", UC.UserId);
                            dgvariation.Add("CREATEDDATE", DateTime.UtcNow);
                            dgvariation.Execute("PROJECT_TIMING_VARIATION");
                        }
                    }

                    if (newtodo)
                    {
                        dg.Add("CREATEDDATE", DateTime.UtcNow);
                        dg.Add("CREATEDBYID", UC.UserId);
                    }
                    dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                    dg.Add("LASTMODIFIEDBYID", UC.UserId);
                    dg.Execute("PROJECT_TIMING", "IDRIF=" + newid + " AND RIFTYPE=1");
                }

                int other = 1;
                while (Request["ToDoOwnerID_" + other] != null && Request["ToDoOwnerID_" + other].Length > 0)
                {
                    newid = Request["ToDoId_" + other].ToString();
                    newtodo = false;
                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("IDSECTIONRIF", sectionID);
                        dg.Add("MEMBERID", Request["ToDoOwnerRealID_" + other]);
                        dg.Add("TODODESCRIPTION", Request["JobTxt_" + other]);

                        if (Request["ToDoId_" + other] == "-1")
                        {
                            dg.Add("CREATEDDATE", DateTime.UtcNow);
                            dg.Add("CREATEDBYID", UC.UserId);
                        }
                        dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                        dg.Add("LASTMODIFIEDBYID", UC.UserId);

                        object nid = dg.Execute("PROJECT_SECTION_MEMBERS", "ID=" + Request["ToDoId_" + other], DigiDapter.Identities.Identity);
                        if (dg.RecordInserted)
                        {
                            newid = nid.ToString();
                            newtodo = true;
                        }
                    }

                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("RIFTYPE", 1);
                        if (newtodo)
                            dg.Add("IDRIF", newid);

                        if (Request["PlannedStartDate_" + other] != null && Request["PlannedStartDate"].Length > 0)
                            dg.Add("PLANNEDSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["PlannedStartDate_" + other], UC.myDTFI)));
                        else
                            dg.Add("PLANNEDSTARTDATE", startdate);

                        if (Request["PlannedEndDate_" + other].Length > 0)
                            dg.Add("PLANNEDENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["PlannedEndDate_" + other], UC.myDTFI)));
                        if (Request["RealStartDate_" + other].Length > 0)
                            dg.Add("REALSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["RealStartDate_" + other], UC.myDTFI)));
                        if (Request["RealEndDate_" + other].Length > 0)
                            dg.Add("REALENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(Request["RealEndDate_" + other], UC.myDTFI)));

                        dg.Add("PLANNEDMINUTEDURATION", (Request["PlannedMinuteDuration_" + other].Length > 0) ? int.Parse(Request["PlannedMinuteDuration_" + other]) : 0);
                        dg.Add("CURRENTMINUTEDURATION", (Request["CurrentMinuteDuration_" + other].Length > 0) ? int.Parse(Request["CurrentMinuteDuration_" + other]) : 0);
                        int prog = (Request["ToDoProgress_" + other].Length > 0) ? int.Parse(Request["ToDoProgress_" + other]) : 0;
                        if (prog > 100) prog = 100;
                        dg.Add("PROGRESS", prog);
                        dg.Add("WEIGHT", ((Request["ToDoWeight_" + other].Length > 0) ? int.Parse(Request["ToDoWeight_" + other]) : 0));

                        if (newtodo)
                        {
                            dg.Add("CREATEDDATE", DateTime.UtcNow);
                            dg.Add("CREATEDBYID", UC.UserId);
                        }
                        dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                        dg.Add("LASTMODIFIEDBYID", UC.UserId);
                        dg.Execute("PROJECT_TIMING", "IDRIF=" + newid + " AND RIFTYPE=1");
                    }
                    other++;
                }
            }
            if (Request["ToDoToDelete"] != null && Request["ToDoToDelete"].Length>0)
            {
                string[] ids = Request["ToDoToDelete"].Split('|');
                foreach (string id in ids)
                {
                    if (id.Length > 0)
                    {
                        DatabaseConnection.DoCommand("DELETE FROM PROJECT_SECTION_MEMBERS WHERE ID=" + id);
                        DatabaseConnection.DoCommand("DELETE FROM PROJECT_TIMING WHERE IDRIF=" + id + " AND RIFTYPE=1");
                    }
                }
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

        public long sectionID
        {
            get
            {
                object o = this.ViewState["_sectionID" + this.ID];
                if (o == null)
                    return 0;
                else
                    return (long)o;
            }
            set
            {
                this.ViewState["_sectionID" + this.ID] = value;
            }
        }
    }
}
