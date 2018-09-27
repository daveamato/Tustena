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
using System.Text;
using Digita.Tustena.Base;

namespace Digita.Tustena.Project
{
    public partial class ProjectUpdate : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                this.InitAjax();
                if (!Page.IsPostBack)
                {
                    this.DayLogSection.Attributes.Add("onchange", "FillSelectToDo()");
                    this.DayLogTodo.Attributes.Add("onchange", "FillToDoProgress()");
                    FillRepeater();
                }
            }
        }

        protected void FillRepeater()
        {
            Session.Remove("selectprj");
            string q = string.Format(@"SELECT ID,TITLE FROM PROJECT WHERE CUSTOMERID={0} AND (ADMINACCOUNT LIKE '%|{1}|%' OR OWNER={1})
            OR (SELECT TOP 1 PROJECT_MEMBERS.ID FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
WHERE PROJECT_TEAMS.PROJECTID=PROJECT.ID AND PROJECT_MEMBERS.USERID={1} AND
(PROJECT_MEMBERS.TYPE=0 OR PROJECT_MEMBERS.TYPE=2)) IS NOT NULL",UC.CompanyId,UC.UserId);

            NewRepeater1.sqlDataSource = q;
            NewRepeater1.DataBind();
            NewRepeater1.Visible = true;
            Tabber.Visible = false;
        }

        public long prjID
        {
            get
            {
                object o = this.Session["_prjID" + this.ID];
                if (o == null)
                    return 0;
                else
                    return (long)o;
            }
            set
            {
                this.Session["_prjID" + this.ID] = value;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "prj", string.Format("<script>var ToDoProject={0}</script>", value));

            }
        }

        private void InitAjax()
        {
            Manager.Register(this, "Ajax.Project", Debug.None);
        }

        [Method]
        public DataSet FillSection(string MemberId)
        {

            string q = string.Format(@"SELECT PROJECT_TEAMS.DESCRIPTION+'-->'+PROJECT_SECTION.TITLE as SECTION, CAST(PROJECT_SECTION.ID AS VARCHAR)+'|'+CAST(PROJECT_MEMBERS.ID AS VARCHAR) AS IDS FROM PROJECT_SECTION
INNER JOIN PROJECT_SECTION_MEMBERS ON PROJECT_SECTION.ID = PROJECT_SECTION_MEMBERS.IDSECTIONRIF
INNER JOIN PROJECT_MEMBERS ON PROJECT_SECTION_MEMBERS.MEMBERID = PROJECT_MEMBERS.ID
INNER JOIN PROJECT_TEAMS on PROJECT_MEMBERS.TEAM = PROJECT_TEAMS.ID
WHERE PROJECT_SECTION.IDRIF = {1} AND USERID={0}
GROUP BY PROJECT_SECTION.TITLE , PROJECT_SECTION.ID,PROJECT_TEAMS.DESCRIPTION,PROJECT_MEMBERS.ID", MemberId, prjID);
            return DatabaseConnection.CreateDataset(q);

        }

        [Method]
        public DataSet FillToDo(string MemberId,string Section)
        {

            string q = string.Format(@"SELECT TODODESCRIPTION, ID
                                        FROM DBO.PROJECT_SECTION_MEMBERS
                                        WHERE (MEMBERID = {0}) AND (IDSECTIONRIF = {1})", MemberId, Section);
            return DatabaseConnection.CreateDataset(q);

        }

        [Method]
        public string[] FillToDoProgress(string ToDoId)
        {
            string[] t = new string[] { "", "", "", "" };
            DataSet ds = DatabaseConnection.CreateDataset(string.Format(@"SELECT PLANNEDMINUTEDURATION +isnull((select sum(plannedminute) from project_timing_variation where idtiming=PROJECT_TIMING.ID),0) as PLANNEDMINUTEDURATION,
CURRENTMINUTEDURATION, PLANNEDENDDATE, PROGRESS, (select top 1 newplanneddate from project_timing_variation where idtiming=PROJECT_TIMING.ID order by newplanneddate desc) as newplanneddate
FROM PROJECT_TIMING WHERE RIFTYPE=1 AND IDRIF={0}", ToDoId));
            if (ds.Tables[0].Rows.Count > 0)
            {
                t[0] = ds.Tables[0].Rows[0][0].ToString();
                t[1] = ds.Tables[0].Rows[0][1].ToString();
                if(ds.Tables[0].Rows[0][4]!=System.DBNull.Value)
                    t[2] = UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0][4]).ToShortDateString();
                else
                    t[2] = UC.LTZ.ToLocalTime((DateTime)ds.Tables[0].Rows[0][2]).ToShortDateString();
                t[3] = ds.Tables[0].Rows[0][3].ToString();
            }
            return t;
        }

        #region Codice generato da Progettazione Web Form
        override protected void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.DayLogSave.Click += new EventHandler(DayLogSave_Click);
            this.NewRepeater1.ItemCommand += new RepeaterCommandEventHandler(NewRepeater1_ItemCommand);
            this.btnViewTiming.Click += new EventHandler(btnViewTiming_Click);
        }

        void btnViewTiming_Click(object sender, EventArgs e)
        {
            MakeTiming();
        }

        void NewRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "btnTiming":
                    NewRepeater1.Visible = false;
                    spanTiming.Visible = true;
                    Session["selectprj"] = ((Label)e.Item.FindControl("prjID")).Text;
                    imgTiming.Attributes.Add("onclick", string.Format("CreateBox('/project/PopGetMember.aspx?render=no&MemberId=TimeOwnerID&MemberName=TimeOwner&MemberType=TimeOwnerType&MemberRealId=TimeOwnerRealID&MemberTeamId=TimeOwnerTeam&ProjectId={0}',event,400,400)", Session["selectprj"]));
                    break;
                case "btnOpenProject":
                    Label lblprjID = (Label)e.Item.FindControl("prjID");
                    prjID = long.Parse(lblprjID.Text);
                    InitForm();
                    NewRepeater1.Visible = false;
                    Tabber.Visible = true;
                    break;
            }
        }

        private void MakeTiming()
        {
            ProjectReport pr = new ProjectReport();
            pr.UC = UC;
            pr.prjID = long.Parse(Session["selectprj"].ToString());
            bool allmember = true;
            if (TimeOwnerRealID.Text.Length > 0)
            {
                pr.MemberId = long.Parse(TimeOwnerRealID.Text);
                allmember = false;
            }
            string print = string.Format("<p align=right><img src=/i/printer.gif onclick=\"PrintTiming({0},{1})\"></p>", Session["selectprj"].ToString(), 9);
            lblTiming.Text = print + pr.ProjectTiming(true, allmember);

        }

        void InitForm()
        {
            DayOwnerID.Text=UC.UserId.ToString();
            DayOwner.Text=DatabaseConnection.SqlScalar("select isnull(name,'')+' '+isnull(surname,'') from account where uid="+UC.UserId);
            DayLogDate.Text=UC.LTZ.ToLocalTime(DateTime.UtcNow).ToShortDateString();
            DayLogSection.DataTextField="SECTION";
            DayLogSection.DataValueField="IDS";
            DayLogSection.DataSource=FillSection(UC.UserId.ToString());
            DayLogSection.DataBind();
            DayLogSection.Items.Insert(0,new ListItem("Seleziona ...","0"));
        }

        void DayLogSave_Click(object sender, EventArgs e)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("IDRIF", long.Parse(Request["DayLogSection"].Split('|')[0]));
                dg.Add("TODORIF", long.Parse(Request["DayLogTodo"]));
                dg.Add("MEMBERID", long.Parse(DayOwnerRealID.Text));
                dg.Add("DESCRIPTION", DayLogDescription.Text);
                dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(DayLogDate.Text, UC.myDTFI)));
                dg.Add("MINUTEDURATION", DayLogDuration.Text);
                if(DayLogMaterial.Text.Length>0)
                    dg.Add("MATERIAL", DayLogMaterial.Text);
                if (DayLogDelay.Text.Length > 0)
                    dg.Add("DELAY", DayLogDelay.Text);

                dg.Add("CREATEDDATE", DateTime.UtcNow);
                dg.Add("CREATEDBYID", UC.UserId);
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", UC.UserId);

                dg.Execute("PROJECT_DAYLOG");
            }

            if (AddMinute.Text.Length > 0)
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("IDTIMING", long.Parse(DatabaseConnection.SqlScalar("select id from PROJECT_TIMING where RIFTYPE=1 AND IDRIF=" + Request["DayLogTodo"])));
                    dg.Add("PLANNEDMINUTE", int.Parse(plusminus.SelectedValue+AddMinute.Text));
                    dg.Add("DESCRIPTION", VariationDescription.Text);
                    if (NewData.Text.Length > 0)
                    {
                        dg.Add("NEWPLANNEDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(NewData.Text,UC.myDTFI)));
                    }
                    dg.Add("CREATEDBYID", UC.UserId);
                    dg.Add("CREATEDDATE", DateTime.UtcNow);
                    dg.Add("RIFTYPE", 1);
                    dg.Execute("PROJECT_TIMING_VARIATION");
                }

                StringBuilder sb = new StringBuilder();
                string prjtxt = DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT WHERE ID=" + prjID);
                string sectiontxt = DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT_SECTION WHERE ID=" + Request["DayLogSection"].Split('|')[0]);
                sb.AppendFormat("Progetto: {0}{1}", prjtxt, System.Environment.NewLine);
                sb.AppendFormat("Sezione: {0}{1}", sectiontxt, System.Environment.NewLine);
                sb.AppendFormat("To Do: {0}{1}", DatabaseConnection.SqlScalar("SELECT TODODESCRIPTION FROM PROJECT_SECTION_MEMBERS WHERE ID=" + Request["DayLogTodo"]), System.Environment.NewLine);
                sb.AppendFormat("Utente: {0}{1}{1}", DayOwner.Text, System.Environment.NewLine);
                sb.AppendFormat("Motivo della variazione:{1}{0}", VariationDescription.Text, System.Environment.NewLine);
                sb.AppendFormat("Ore:{0}{1}", plusminus.SelectedValue + AddMinute.Text, System.Environment.NewLine);
                if (NewData.Text.Length > 0)
                    sb.AppendFormat("Nuova data prevista:{0}{1}", NewData.Text, System.Environment.NewLine);

                string mailTo = string.Empty;
                string[] adminId = DatabaseConnection.SqlScalar("select adminaccount+cast(owner as varchar(10)) from project where id=" + prjID).Split('|');
                foreach (string adm in adminId)
                {
                    if (adm.Length > 0)
                        mailTo += DatabaseConnection.SqlScalar("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID=" + adm) + ";";
                }
                mailTo += DatabaseConnection.SqlScalar(string.Format("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID=(select memberid from project_section where id={0})", Request["DayLogSection"].Split('|')[0]));

                MessagesHandler.SendMail(mailTo, "project@tustena.com", string.Format("Variazione tempi [{0}] [{1}]", prjtxt, sectiontxt), sb.ToString());

                ProjectData pd = new ProjectData();
                if (NewData.Text.Length > 0)
                    pd.ChangeStartEndFromRelation(long.Parse(Request["DayLogSection"].Split('|')[0]),
                        Convert.ToDateTime(NewData.Text, UC.myDTFI),
                        Convert.ToDateTime(DatabaseConnection.SqlScalartoObj("select plannedenddate from project_timing where riftype=0 and idrif=" + Request["DayLogSection"].Split('|')[0])),
                        UC.UserId);
            }

            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("CURRENTMINUTEDURATION", int.Parse(Request["DayLogCurrent"]) + int.Parse(DayLogDuration.Text));
                decimal planned = decimal.Parse(Request["DayLogPlanned"]);
                if (AddMinute.Text.Length > 0)
                {
                    planned+= decimal.Parse(plusminus.SelectedValue+AddMinute.Text);
                }
                decimal progress = (decimal.Parse(Request["DayLogCurrent"]) + decimal.Parse(DayLogDuration.Text))*100/planned;
                dg.Add("PROGRESS", Convert.ToByte(progress));
                dg.Execute("PROJECT_TIMING", "RIFTYPE=1 AND IDRIF=" + Request["DayLogTodo"]);
            }

            if (DayLogDelay.Text.Length > 0)
            {
                StringBuilder sb = new StringBuilder();
                string prjtxt = DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT WHERE ID=" + prjID);
                string sectiontxt =DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT_SECTION WHERE ID=" + Request["DayLogSection"].Split('|')[0]);
                sb.AppendFormat("Progetto: {0}{1}", prjtxt ,System.Environment.NewLine);
                sb.AppendFormat("Sezione: {0}{1}", sectiontxt, System.Environment.NewLine);
                sb.AppendFormat("To Do: {0}{1}",DatabaseConnection.SqlScalar("SELECT TODODESCRIPTION FROM PROJECT_SECTION_MEMBERS WHERE ID=" + Request["DayLogTodo"]), System.Environment.NewLine);
                sb.AppendFormat("Utente: {0}{1}{1}",DayOwner.Text,System.Environment.NewLine);
                sb.AppendFormat("Motivazione del ritardo:{1}{0}",DayLogDelay.Text,System.Environment.NewLine);

                string mailTo = string.Empty;
                string[] adminId = DatabaseConnection.SqlScalar("select adminaccount+cast(owner as varchar(10)) from project where id="+prjID).Split('|');
                foreach(string adm in adminId)
                {
                    if(adm.Length>0)
                        mailTo+=DatabaseConnection.SqlScalar("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID="+adm)+";";
                }
                mailTo+=DatabaseConnection.SqlScalar(string.Format("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID=(select memberid from project_section where id={0})",Request["DayLogSection"].Split('|')[0]));

                MessagesHandler.SendMail(mailTo, "project@tustena.com", string.Format("Notifica di ritardo [{0}] [{1}]",prjtxt,sectiontxt), sb.ToString());
            }

            FillRepeater();
        }

        #endregion
    }
}
