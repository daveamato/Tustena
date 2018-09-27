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
using System.Text;
using Digita.Tustena.Core;

namespace Digita.Tustena.Project
{
    public partial class TeamManager : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            btnSaveTeam.Text = Root.rm.GetString("Save");
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

        public void BindTeam()
        {
            NewRepeater1.sqlDataSource = "SELECT ID,DESCRIPTION,ACCOUNT.NAME+' '+ACCOUNT.SURNAME AS LEADERNAME,LEADERID FROM PROJECT_TEAMS LEFT OUTER JOIN ACCOUNT ON PROJECT_TEAMS.LEADERID=ACCOUNT.UID WHERE PROJECT_TEAMS.PROJECTID=" + prjID;
            NewRepeater1.DataBind();
            NewRepeater1.Visible = true;
            TeamTable.Visible = false;
        }

        #region Codice generato da Progettazione Web Form

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.NewRepeater1.ItemDataBound += new RepeaterItemEventHandler(NewRepeater1_ItemDataBound);
            this.NewRepeater1.ItemCommand += new RepeaterCommandEventHandler(NewRepeater1_ItemCommand);
            this.btnSaveTeam.Click += new EventHandler(btnSaveTeam_Click);
            this.btnNewTeam.Click += new EventHandler(btnNewTeam_Click);
        }

        void btnNewTeam_Click(object sender, EventArgs e)
        {
            TeamID.Text = "-1";
            TeamDescription.Text = string.Empty;
            TeamOwner.Text = string.Empty;
            TeamOwnerID.Text = string.Empty;
            NewRepeater1.Visible = false;
            TeamTable.Visible = true;

        }

        void btnSaveTeam_Click(object sender, EventArgs e)
        {
            bool insertleader = true;
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("DESCRIPTION", TeamDescription.Text);
                dg.Add("PROJECTID", prjID);
                dg.Add("LEADERID", TeamOwnerID.Text);
                object newid = dg.Execute("PROJECT_TEAMS", "ID=" + TeamID.Text, DigiDapter.Identities.Identity);
                if (dg.RecordInserted)
                {
                    TeamID.Text = newid.ToString();
                    insertleader = true;
                }
            }

            using (DigiDapter dg = new DigiDapter())
            {

                    dg.Add("USERID", TeamOwnerID.Text);
                    dg.Add("TEAM", TeamID.Text);
                    dg.Add("TYPE", 2);
                    dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                    dg.Add("LASTMODIFIEDBYID", UC.UserId);
                    if (insertleader)
                        dg.Add("CREATEDBYID", UC.UserId);
                    dg.Execute("PROJECT_MEMBERS", "TEAM=" + TeamID.Text + " AND USERID=" + TeamOwnerID.Text);

            }

            if (Request["MemberToDelete"] != null && Request["MemberToDelete"].Length > 0)
            {
                string[] todel = Request["MemberToDelete"].Split('|');
                foreach (string td in todel)
                {
                    if (td.Length > 0 && int.Parse(td) > 0)
                    {
                        DatabaseConnection.DoCommand("DELETE FROM PROJECT_MEMBERS WHERE ID="+td);
                    }
                }
            }


            if (Request["TeamMemberId"] != null && Request["TeamMemberId"].Length > 0)
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("USERID", Request["TeamMemberId"]);
                    dg.Add("TEAM", TeamID.Text);
                    dg.Add("TYPE", Request["MemberType"]);
                    dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                    dg.Add("LASTMODIFIEDBYID", UC.UserId);
                    if (Request["TeamMemberRealId"]=="-1")
                        dg.Add("CREATEDBYID", UC.UserId);
                    dg.Execute("PROJECT_MEMBERS", "ID=" + Request["TeamMemberRealId"]);
                }
                int othermembers = 1;
                while (Request["TeamMemberId_" + othermembers] != null && Request["TeamMemberId_" + othermembers].Length > 0)
                {
                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("USERID", Request["TeamMemberId_" + othermembers]);
                        dg.Add("TEAM", TeamID.Text);
                        dg.Add("TYPE", Request["MemberType_" + othermembers]);
                        dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                        dg.Add("LASTMODIFIEDBYID", UC.UserId);
                        if (Request["TeamMemberRealId_" + othermembers] == "-1")
                            dg.Add("CREATEDBYID", UC.UserId);
                        dg.Execute("PROJECT_MEMBERS", "ID=" + Request["TeamMemberRealId_" + othermembers]);
                    }
                    othermembers++;
                }
            }
            BindTeam();
        }

        void NewRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "btnOpenTeam":

                    TeamID.Text = ((Label)e.Item.FindControl("teamID")).Text;
                    TeamDescription.Text = ((LinkButton)e.Item.FindControl("btnOpenTeam")).Text;

                    TeamOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + ((Label)e.Item.FindControl("leaderID")).Text);
                    TeamOwnerID.Text = ((Label)e.Item.FindControl("leaderID")).Text;

                    DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT_MEMBERS WHERE TEAM="+TeamID.Text).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("<script>" + System.Environment.NewLine);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string membername=string.Empty;
                            int type = 0;
                            if (dt.Rows[i]["TYPE"].ToString()=="1")
                            {
                                membername = G.ParseJSString(DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID=" + dt.Rows[i]["UserID"].ToString()));
                                type = 1;
                            }
                            else
                                membername = G.ParseJSString(DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + dt.Rows[i]["UserID"].ToString()));

                            if (i == 0)
                            {
                                sb.AppendFormat("fillMember(0,{0},{1},'{2}',{3});{4}", dt.Rows[i]["ID"].ToString(), dt.Rows[i]["USERID"].ToString(), membername, type, System.Environment.NewLine);
                            }
                            else
                            {
                                sb.AppendFormat("addMember();{4}fillMember({5},{0},{1},'{2}',{3});{4}",dt.Rows[i]["ID"].ToString(), dt.Rows[i]["USERID"].ToString(), membername, type, System.Environment.NewLine, i);
                            }

                        }
                        sb.Append("</script>" + System.Environment.NewLine);
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "loadteammember", sb.ToString());
                    }
                    NewRepeater1.Visible = false;
                    TeamTable.Visible = true;
                    break;
            }
        }

        void NewRepeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:

                    break;
            }

        }

        #endregion
    }
}
