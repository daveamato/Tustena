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
using Digita.Tustena.Project;
using Digita.Tustena.Base;

namespace Digita.Tustena.Project
{
    public partial class ProjectManagerStandard : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                if (!Page.IsPostBack)
                {
                    btnSaveprj.Text = Root.rm.GetString("Save");
                    LblNewProject.Text = "Nuovo progetto";
                    TitlePage.Text = "Progetti";

                    FillRepeater();
                }
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (Tabber.Visible)
                {
                    if (this.prjID.Text.Length > 0 && this.prjID.Text != "-1")
                    {
                        TeamManager1.prjID = long.Parse(prjID.Text);
                        ProjectEvents1.prjID = long.Parse(prjID.Text);
                        ProjectEvents1.FillSections();
                        ProjectEvents1.FillEvents();
                        ProjectSectionRelation1.prjID = long.Parse(prjID.Text);
                        ProjectSectionRelation1.FillSections();
                        ProjectSectionRelation1.FillRelations();
                        string adminaccount=DatabaseConnection.SqlScalar("SELECT ADMINACCOUNT FROM PROJECT WHERE ID=" + prjID.Text);
                        if(adminaccount.Length>0)
                            FillAdminAccount(adminaccount);
                    }
                }
            }
            base.OnPreRender(e);
        }

        protected void FillRepeater()
        {
            NewRepeater1.sqlDataSource = string.Format("SELECT ID,TITLE FROM PROJECT WHERE CUSTOMERID={0} AND (ADMINACCOUNT LIKE '%|{1}|%' OR OWNER={1})",UC.CompanyId,UC.UserId);
            NewRepeater1.DataBind();
            NewRepeater1.Visible = true;
            Tabber.Visible = false;
        }

#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

        private void InitializeComponent()
        {
            this.btnSaveprj.Click += new EventHandler(btnSaveprj_Click);
            this.NewRepeater1.ItemCommand += new RepeaterCommandEventHandler(NewRepeater1_ItemCommand);
            this.LblNewProject.Click += new EventHandler(LblNewProject_Click);
            this.CloseProject.Click += new EventHandler(CloseProject_Click);
            this.btnSendMails.Click += new EventHandler(btnSendMails_Click);
        }

        void btnSendMails_Click(object sender, EventArgs e)
        {
            string mailTo = string.Empty;
            string mailSubject = string.Empty;
            string mailBody = string.Empty;
            string query = string.Empty;
            DataSet ds = new DataSet();
            switch (selectMailSend.SelectedValue)
            {
                case "0":
                    string[] adminId = DatabaseConnection.SqlScalar("SELECT ADMINACCOUNT+CAST(OWNER AS VARCHAR(10)) FROM PROJECT WHERE ID=" + prjID).Split('|');
                    foreach (string adm in adminId)
                    {
                        if (adm.Length > 0)
                            mailTo += DatabaseConnection.SqlScalar("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID=" + adm) + ";";
                    }
                    query = string.Format(@"SELECT CASE ISNULL(ACCOUNT.NOTIFYEMAIL,'') WHEN '' THEN ACCOUNT.USERACCOUNT ELSE ACCOUNT.NOTIFYEMAIL END AS MAIL, PROJECT_MEMBERS.ID
FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
INNER JOIN ACCOUNT ON ACCOUNT.UID=PROJECT_MEMBERS.USERID
WHERE PROJECT_TEAMS.PROJECTID={0} AND (PROJECT_MEMBERS.TYPE=0 OR PROJECT_MEMBERS.TYPE=2)
UNION
SELECT BASE_CONTACTS.EMAIL AS MAIL ,PROJECT_MEMBERS.ID
FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
INNER JOIN BASE_CONTACTS ON BASE_CONTACTS.ID=PROJECT_MEMBERS.USERID
WHERE PROJECT_TEAMS.PROJECTID={0} AND (PROJECT_MEMBERS.TYPE=1)", this.prjID.Text);
                    ds = DatabaseConnection.CreateDataset(query);
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        mailTo += dr[0].ToString() + ";";
                    }
                    break;
                case "1":
                    adminId = DatabaseConnection.SqlScalar("SELECT ADMINACCOUNT+CAST(OWNER AS VARCHAR(10)) FROM PROJECT WHERE ID=" + prjID.Text).Split('|');
                    foreach (string adm in adminId)
                    {
                        if (adm.Length > 0)
                            mailTo += DatabaseConnection.SqlScalar("SELECT CASE ISNULL(NOTIFYEMAIL,'') WHEN '' THEN USERACCOUNT ELSE NOTIFYEMAIL END AS MAIL FROM ACCOUNT WHERE UID=" + adm) + ";";
                    }
                    break;
                case "2":
                    if (MailOwnerType.Text == "0" || MailOwnerType.Text == "2")
                    {
                        query = string.Format(@"SELECT CASE ISNULL(ACCOUNT.NOTIFYEMAIL,'') WHEN '' THEN ACCOUNT.USERACCOUNT ELSE ACCOUNT.NOTIFYEMAIL END AS MAIL, PROJECT_MEMBERS.ID
FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
INNER JOIN ACCOUNT ON ACCOUNT.UID=PROJECT_MEMBERS.USERID
WHERE PROJECT_TEAMS.PROJECTID={0} AND PROJECT_MEMBERS.ID={1}", this.prjID.Text, MailOwnerRealID.Text);
                    }
                    else
                    {
                        query = string.Format(@"SELECT BASE_CONTACTS.EMAIL AS MAIL ,PROJECT_MEMBERS.ID
FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
INNER JOIN BASE_CONTACTS ON BASE_CONTACTS.ID=PROJECT_MEMBERS.USERID
WHERE PROJECT_TEAMS.PROJECTID={0} AND PROJECT_MEMBERS.ID={1}", this.prjID.Text, MailOwnerRealID.Text);
                    }
                    ds = DatabaseConnection.CreateDataset(query);
                    mailTo = ds.Tables[0].Rows[0][0].ToString();
                    break;
            }

            ProjectReport pr = new ProjectReport();
            pr.UC = UC;
            switch (selectMailType.SelectedValue)
            {
                case "0":
                    pr.prjID = long.Parse(this.prjID.Text);
                    mailBody = pr.FillStatus();
                    MessagesHandler.SendMail(mailTo, "project@tustena.com", mailSubject, mailBody);
                    break;
                case "1":
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        pr.prjID = long.Parse(this.prjID.Text);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            pr.MemberId = long.Parse(dr[1].ToString());
                            mailBody = pr.ToDoList();
                            MessagesHandler.SendMail(dr[0].ToString(), "project@tustena.com", mailSubject, mailBody);
                        }
                    }
                    break;
            }
        }

        void CloseProject_Click(object sender, EventArgs e)
        {
            FillRepeater();
        }

        void LblNewProject_Click(object sender, EventArgs e)
        {
            Tabber.EditTab = visProject.ID;
            prjID.Text = "-1";
            prjTitle.Text = string.Empty;
            prjDescription.Text = string.Empty;
            prjOwnerID.Text = string.Empty;
            prjOwner.Text = string.Empty;
            NewRepeater1.Visible = false;
            Tabber.Visible = true;
            prjOpen.Checked = true;
            tblEvents.Visible = false;
            tblRelations.Visible = false;
            tblSendmail.Visible = false;
        }

        void NewRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "btnOpenProject":
                    Session["currentproject"] = ((Label)e.Item.FindControl("prjID")).Text;
                    Response.Redirect("/project/projectgantt.aspx?m=74&dgb=1&si=75");

                    break;
                case "btnModify":
                    tblEvents.Visible = true;
                    tblRelations.Visible = true;
                    tblSendmail.Visible = true;
                    Label prjID = (Label)e.Item.FindControl("prjID");
                    DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT WHERE ID="+prjID.Text).Tables[0].Rows[0];
                    this.prjID.Text = dr["ID"].ToString();
                    prjTitle.Text = dr["TITLE"].ToString();
                    prjDescription.Text = dr["DESCRIPTION"].ToString();
                    prjOwnerID.Text = dr["OWNER"].ToString();
                    prjOwner.Text = DatabaseConnection.SqlScalar("SELECT NAME+' '+SURNAME AS OWNER FROM ACCOUNT WHERE UID="+prjOwnerID.Text);
                    prjOpen.Checked = (bool)dr["PRJOPEN"];
                    prjSuspend.Checked = (bool)dr["PRJSUSPEND"];

                    if (dr["ADMINACCOUNT"].ToString().Length>0)
                    {
                        FillAdminAccount(dr["ADMINACCOUNT"].ToString());
                    }

                    NewRepeater1.Visible = false;
                    Tabber.Visible = true;
                    ProjectSessions1.prjID = long.Parse(prjID.Text);
                    ProjectSessions1.BindSections();
                    TeamManager1.prjID = long.Parse(prjID.Text);
                    TeamManager1.BindTeam();
                    ProjectEvents1.prjID = long.Parse(prjID.Text);
                    ProjectEvents1.FillSections();
                    ProjectEvents1.FillEvents();
                    ProjectSectionRelation1.prjID = long.Parse(prjID.Text);
                    ProjectSectionRelation1.FillSections();
                    ProjectSectionRelation1.FillRelations();
                    break;
                case "MultiDeleteButton":
                    DeleteChecked.MultiDelete(this.NewRepeater1.MultiDeleteListArray, "Project");
                    this.NewRepeater1.DataBind();
                    break;
            }
        }

        private void FillAdminAccount(string adminaccount)
        {

            if (adminaccount.StartsWith("|"))
                adminaccount = adminaccount.Substring(1);
            if (adminaccount.EndsWith("|"))
                adminaccount = adminaccount.Substring(0, adminaccount.Length - 1);


            string[] aa = adminaccount.Split('|');
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<script>fillOwner(0,{0},'{1}');{2}", aa[0], ParseJSString(DatabaseConnection.SqlScalar("SELECT NAME+' '+SURNAME AS OWNER FROM ACCOUNT WHERE UID=" + aa[0])), System.Environment.NewLine);
            if (aa.Length > 1)
            {
                for (int i = 1; i < aa.Length; i++)
                {
                    sb.AppendFormat("addOwner();fillOwner({3},{0},'{1}');{2}", aa[i], ParseJSString(DatabaseConnection.SqlScalar("SELECT NAME+' '+SURNAME AS OWNER FROM ACCOUNT WHERE UID=" + aa[i])), System.Environment.NewLine, i);
                }
            }
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "loadaccount", sb.ToString());
        }

        void btnSaveprj_Click(object sender, EventArgs e)
        {
            object newid;
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("TITLE", prjTitle.Text);
                dg.Add("DESCRIPTION", prjDescription.Text);
                dg.Add("OWNER", prjOwnerID.Text);
                dg.Add("PRJOPEN", ((prjOpen.Checked)?1:0));
                dg.Add("PRJSUSPEND", ((prjSuspend.Checked) ? 1 : 0));
                dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
                dg.Add("LASTMODIFIEDBYID", UC.UserId);

                if (prjID.Text == "-1")
                {
                    dg.Add("CREATEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
                    dg.Add("CREATEDBYID", UC.UserId);
                }

                if (Request["OtherOwnerID"] != null && Request["OtherOwnerID"].Length > 0)
                {
                    string otherowner = "|" + Request["OtherOwnerID"];
                    int other = 1;
                    while (Request["OtherOwnerID_" + other] != null && Request["OtherOwnerID_"+other].Length > 0)
                    {
                        otherowner += "|"+Request["OtherOwnerID_" + other];
                        other++;
                    }
                    dg.Add("ADMINACCOUNT", otherowner+"|");
                }else
                    dg.Add("ADMINACCOUNT", string.Empty);

                dg.Add("GROUPS", "|1|");

                newid=dg.Execute("PROJECT", "ID=" + prjID.Text, DigiDapter.Identities.Identity);

            }
            if (prjID.Text == "-1")
                prjID.Text = newid.ToString();

            ProjectEvents1.prjID = long.Parse(prjID.Text);
            ProjectEvents1.SaveEvents();
            ProjectSectionRelation1.prjID = long.Parse(prjID.Text);
            ProjectSectionRelation1.SaveRelation();
            NewRepeater1.Visible = true;
            Tabber.Visible = false;
            FillRepeater();
        }
    #endregion

    }
}
