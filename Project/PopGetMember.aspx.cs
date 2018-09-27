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

namespace Digita.Tustena.Project
{
    public partial class PopGetMember : G
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "endsession", "<script>opener.location.href=opener.location.href;self.close();</script>");
            }
            else
            {
                string js;

                string control = Request.QueryString["MemberName"].ToString();
                string control2 = Request.QueryString["MemberId"].ToString();
                string control3 = Request.QueryString["MemberType"].ToString();
                string control4 = string.Empty;
                string control5 = string.Empty;

                if(Request.QueryString["MemberRealId"]!=null)
                    control4 = Request.QueryString["MemberRealId"].ToString();
                if (Request.QueryString["MemberTeamId"] != null)
                    control5 = Request.QueryString["MemberTeamId"].ToString();

                ViewState["ProjectId"]=Request.QueryString["ProjectId"].ToString();
                FillTeams(ViewState["ProjectId"].ToString());

                string clickControl = null;
                string eventFunction = null;
                if (Request.QueryString["click"] != null)
                    clickControl = Request.QueryString["click"].ToString();
                if (Request.QueryString["event"] != null)
                    eventFunction = Request.QueryString["event"].ToString();

                js = "<script>";
                js += "function SetRef(id,tx,ty,rid,te){";
                if (Request.QueryString["frame"] != null)
                {
                    js += "dynaret.SetParams('" + control + "',tx);";
                    js += "dynaret.SetParams('" + control2 + "',id);";
                    if(control3!="0" && control3!="1")
                        js += "dynaret.SetParams('" + control3 + "',ty);";
                    if(control4!=string.Empty)
                        js += "dynaret.SetParams('" + control4 + "',rid);";
                    if (control5 != string.Empty)
                        js += "dynaret.SetParams('" + control5 + "',te);";
                }
                else
                {
                    js += "dynaret('" + control + "').value=tx;";
                    js += "dynaret('" + control2 + "').value=id;";
                    if (control3 != "0" && control3 != "1")
                        js += "dynaret('" + control3 + "').value=ty;";
                    if (control4 != string.Empty)
                        js += "dynaret('" + control4 + "').value=rid;";
                    if (control5 != string.Empty)
                        js += "dynaret('" + control5 + "').value=te;";
                }
                js += "self.close();";
                if (clickControl != null)
                    js += "clickElement(dynaret('" + clickControl + "'));" + Environment.NewLine;
                if (eventFunction != null)
                    js += "dynaevent('" + eventFunction + "');" + Environment.NewLine;
                js += "parent.HideBox();}";
                js += "</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "setref", js);
                Find.Text = Root.rm.GetString("Prftxt5");
                if (!Page.IsPostBack)
                {
                    if(Request.QueryString["admin"]!=null)
                        FillRepeater(control3,true);
                    else
                        FillRepeater(control3);
                }
            }
        }

        private void FillRepeater(string control3)
        {
            FillRepeater(control3, false);
        }

        private void FillRepeater(string control3,bool admin)
        {
            string userquery = @"SELECT PROJECT_MEMBERS.USERID, PROJECT_MEMBERS.ID, PROJECT_MEMBERS.TEAM, PROJECT_MEMBERS.TYPE, PROJECT_TEAMS.DESCRIPTION, ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') AS MEMBERNAME
FROM PROJECT_MEMBERS LEFT OUTER JOIN ACCOUNT ON PROJECT_MEMBERS.USERID = ACCOUNT.UID LEFT OUTER JOIN
PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM = PROJECT_TEAMS.ID WHERE PROJECT_MEMBERS.TYPE=2 AND PROJECT_TEAMS.PROJECTID=" + ViewState["ProjectId"];
            DataTable dtLeader = DatabaseConnection.CreateDataset(userquery).Tables[0];

            userquery = @"SELECT PROJECT_MEMBERS.USERID, PROJECT_MEMBERS.ID, PROJECT_MEMBERS.TEAM, PROJECT_MEMBERS.TYPE, PROJECT_TEAMS.DESCRIPTION, ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') AS MEMBERNAME
FROM PROJECT_MEMBERS LEFT OUTER JOIN ACCOUNT ON PROJECT_MEMBERS.USERID = ACCOUNT.UID LEFT OUTER JOIN
PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM = PROJECT_TEAMS.ID WHERE PROJECT_MEMBERS.TYPE=0 AND PROJECT_TEAMS.PROJECTID=" + ViewState["ProjectId"];
            DataTable dtUser = DatabaseConnection.CreateDataset(userquery).Tables[0];

            DataTable dtContacs = new DataTable();
            if (control3 != "0")
            {
                userquery = @"SELECT PROJECT_MEMBERS.USERID, PROJECT_MEMBERS.ID, PROJECT_MEMBERS.TEAM, PROJECT_MEMBERS.TYPE, PROJECT_TEAMS.DESCRIPTION, ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') AS MEMBERNAME
FROM PROJECT_MEMBERS LEFT OUTER JOIN BASE_CONTACTS ON PROJECT_MEMBERS.USERID = BASE_CONTACTS.ID LEFT OUTER JOIN
PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM = PROJECT_TEAMS.ID WHERE PROJECT_MEMBERS.TYPE=1 AND PROJECT_TEAMS.PROJECTID=" + ViewState["ProjectId"];
               dtContacs = DatabaseConnection.CreateDataset(userquery).Tables[0];
            }

            DataTable dtTemp = DataManipulation.Union(dtLeader, dtUser);
            DataTable dtComplete = new DataTable();
            if (control3 != "0")
                dtComplete = DataManipulation.Union(dtTemp, dtContacs);
            else
                dtComplete = dtTemp;



            dtComplete.DefaultView.Sort = "TEAM";
            ContactReferrer.DataSource = dtComplete;
            ContactReferrer.DataBind();
        }

        private void FillTeams(string prjId)
        {
            SelectTeam.DataValueField = "ID";
            SelectTeam.DataTextField = "DESCRIPTION";
            SelectTeam.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM PROJECT_TEAMS WHERE PROJECTID="+prjId);
            SelectTeam.DataBind();
        }
    }
}
