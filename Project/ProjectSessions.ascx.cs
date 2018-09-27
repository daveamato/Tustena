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
using System.Reflection;
using System.Drawing;

namespace Digita.Tustena.Project
{
    public partial class ProjectSessions : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSaveSection.Attributes.Add("onclick", "return CheckWeight();");
        }

        public void FillSectionParent(string id)
        {
            SectionParent.DataTextField = "TITLE";
            SectionParent.DataValueField = "ID";
            if(id.Length>0)
                SectionParent.DataSource = DatabaseConnection.CreateDataset(string.Format("SELECT ID,TITLE FROM PROJECT_SECTION WHERE ID<>{0} AND IDRIF={1}",id,prjID));
            else
                SectionParent.DataSource = DatabaseConnection.CreateDataset(string.Format("SELECT ID,TITLE FROM PROJECT_SECTION WHERE IDRIF={0}", prjID));

            SectionParent.DataBind();
            SectionParent.Items.Insert(0, new ListItem("Sezione principale", "0"));
        }

        public void BindSections()
        {
            DataSet ds =FillSectionList;
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class=normal cellpadding=2 cellspacing=0 width=\"100%\"><tr>");
            sb.AppendFormat("<td class=\"GridTitle\" width=\"50%\">{0}</td>", "Titolo");
            sb.AppendFormat("<td class=\"GridTitle\" width=\"25%\">{0}</td>", "Avanzamento");
            sb.AppendFormat("<td class=\"GridTitle\" width=\"25%\">{0}</td>", "Data fine pianificata");
            sb.Append("</tr>");
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sb.AppendFormat("<tr onclick=\"openSection({0})\" style=\"cursor:pointer;\">", dr["ID"].ToString());
                sb.AppendFormat("<td class=\"GridItem\">{0}</td>", dr["TITLE"].ToString());
                string avquery = @"select progress,weight from project_timing right outer join project_section_members on (project_timing.idrif=project_section_members.id and project_timing.riftype=1)
where project_section_members.idsectionrif=" + dr["ID"].ToString();

                ProjectCalculator pc = new ProjectCalculator();
                string average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]).ToString();

                sb.AppendFormat("<td class=\"GridItem\">{0}</td>", string.Format("<span style=\"border:1px solid #000000;width:100%\"><span style=\"background-color:gold;width:{0}%;color:#000000;text-align:center\">{0}%</span></span>", average));
                if (dr["PLANNEDENDDATE"] != System.DBNull.Value)
                    sb.AppendFormat("<td class=\"GridItem\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]).ToShortDateString());
                else
                    sb.Append("<td class=\"GridItem\">&nbsp;</td>");
                sb.Append("</tr>");
                FillParentSections(ds, dr["ID"].ToString(), ref sb, 1);
            }
            sb.Append("</table>");
            lblSection.Text = sb.ToString();

            MainTable.Visible = true;
            SessionTable.Visible = false;
        }

        private void FillParentSections(DataSet ds,string id,ref StringBuilder sb,int indent)
        {
            DataRow[] dr1 = ds.Tables[1].Select(string.Format("[PARENT]='{0}'", id));
            if (dr1.Length > 0)
            {
                foreach (DataRow dr3 in dr1)
                {
                    sb.AppendFormat("<tr onclick=\"openSection({0})\" style=\"cursor:pointer;\">", dr3["ID"].ToString());
                    sb.AppendFormat("<td class=\"GridItemAltern\">{1}{0}</td>", dr3["TITLE"].ToString(), new String(' ', indent * 3).Replace(" ", "&nbsp;"));
                    string avquery = @"select progress,weight from project_timing right outer join project_section_members on (project_timing.idrif=project_section_members.id and project_timing.riftype=1)
where project_section_members.idsectionrif=" + dr3["ID"].ToString();

                    ProjectCalculator pc = new ProjectCalculator();
                    string average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]).ToString();

                    sb.AppendFormat("<td class=\"GridItemAltern\">{0}</td>", string.Format("<span style=\"border:1px solid #000000;width:100%\"><span style=\"background-color:gold;width:{0}%;color:#000000;text-align:center\">{0}%</span></span>", average));
                    if (dr3["PLANNEDENDDATE"] != System.DBNull.Value)
                        sb.AppendFormat("<td class=\"GridItemAltern\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr3["PLANNEDENDDATE"]).ToShortDateString());
                    else
                        sb.Append("<td class=\"GridItemAltern\">&nbsp;</td>");
                    sb.Append("</tr>");

                    FillParentSections(ds, dr3["ID"].ToString(), ref sb,++indent);
                    indent--;
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

        public long SectionId
        {
            get
            {
                if (SelectSession.Text.Length > 0)
                    return long.Parse(SelectSession.Text);
                else
                    return 0;
            }
            set
            {
                SelectSession.Text = value.ToString();
            }
        }

        public bool GanttEdit
        {
            get
            {
                object o = this.ViewState["_GanttEdit" + this.ID];
                if (o == null)
                    return false;
                else
                    return (bool)o;
            }
            set
            {
                this.ViewState["_GanttEdit" + this.ID] = value;
            }
        }

        private DataSet FillSectionList
        {
            get
            {

                DataSet sections = DatabaseConnection.CreateDataset(String.Format("SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDENDDATE FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0) WHERE PROJECT_SECTION.PARENT IS NULL AND PROJECT_SECTION.IDRIF={0} ORDER BY PROJECT_SECTION.SECTIONORDER;SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDENDDATE FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0) WHERE PROJECT_SECTION.PARENT IS NOT NULL AND PROJECT_SECTION.IDRIF={0} ORDER BY PROJECT_SECTION.SECTIONORDER;", this.prjID));
                return sections;
            }
        }

        private void LoadFilterColors()
        {
            bool start = false;
            FilterColor.Items.Clear();
            foreach (FieldInfo col in typeof(KnownColor).GetFields())
            {
                if (col.Name == "AliceBlue") start = true;
                if (col.FieldType == typeof(KnownColor))
                {
                    if (start)
                        FilterColor.Items.Add(new ListItem(col.Name, col.Name));
                }
            }

            for (int i = 0; i < FilterColor.Items.Count; i++)
            {
                FilterColor.Items[i].Attributes.Add("style", "background-color:" + FilterColor.Items[i].Value);
            }
        }

        #region Codice generato da Progettazione Web Form
        override protected void OnInit(EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            if (!Page.IsPostBack)
            {
                btnSaveSection.Text = Root.rm.GetString("Save");
                SessionTable.Visible = false;
            }
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            btnSelectSession.Click += new EventHandler(btnSelectSession_Click);
            btnSaveSection.Click += new EventHandler(btnSaveSection_Click);
        }

        void btnSaveSection_Click(object sender, EventArgs e)
        {
            bool newsection = false;
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("IDRIF", prjID);
                dg.Add("TITLE", SectionName.Text);
                dg.Add("DESCRIPTION", SectionDescription.Text);
                if (SectionParent.SelectedIndex < 1)
                    dg.Add("PARENT", System.DBNull.Value);
                else
                    dg.Add("PARENT", SectionParent.SelectedValue);

                dg.Add("MEMBERID", SectionOwnerID.Text);
                if (SelectSession.Text == "-1")
                {
                    dg.Add("CREATEDDATE", DateTime.UtcNow);
                    dg.Add("CREATEDBYID", UC.UserId);
                }

                dg.Add("COLOR", FilterColor.Items[FilterColor.SelectedIndex].Value);
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", UC.UserId);

                if (CostType.SelectedIndex > -1)
                {
                    dg.Add("COSTTYPE", CostType.SelectedIndex);
                }
                if (SectionAmount.Text.Length > 0)
                {
                    dg.Add("AMOUNT", StaticFunctions.FixDecimal(SectionAmount.Text));
                }

                object newid = dg.Execute("PROJECT_SECTION", "ID=" + SelectSession.Text, DigiDapter.Identities.Identity);
                if (dg.RecordInserted)
                {
                    SelectSession.Text = ((decimal)newid).ToString();
                    newsection = true;
                }
            }

            if (!newsection)
                if (int.Parse(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM PROJECT_TIMING WHERE IDRIF=" + SelectSession.Text)) <= 0)
                    newsection = true;


            ProjectToDo1.sectionID = long.Parse(SelectSession.Text);
            ProjectToDo1.UpdateToDo();
            ProjectToDo1.BindToDo();

            DateTime planneddate = ((DateTime)DatabaseConnection.SqlScalartoObj(string.Format(@"SELECT PLANNEDENDDATE FROM PROJECT_TIMING
INNER JOIN PROJECT_SECTION_MEMBERS ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION_MEMBERS.ID AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF={0}
ORDER BY PLANNEDENDDATE DESC",SelectSession.Text)));

            if (PlannedEndDate.Text.Length > 0)
                if (UC.LTZ.ToUniversalTime(Convert.ToDateTime(PlannedEndDate.Text, UC.myDTFI)) > planneddate)
                    planneddate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(PlannedEndDate.Text, UC.myDTFI));

            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("RIFTYPE", 0);
                if (newsection)
                    dg.Add("IDRIF",SelectSession.Text);
                dg.Add("PLANNEDSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(PlannedStartDate.Text, UC.myDTFI)));
                dg.Add("PLANNEDENDDATE", planneddate);
                if (RealStartDate.Text.Length>0)
                    dg.Add("REALSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(RealStartDate.Text, UC.myDTFI)));
                else
                    dg.Add("REALSTARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(PlannedStartDate.Text, UC.myDTFI)));
                if (RealEndDate.Text.Length>0)
                    dg.Add("REALENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(RealEndDate.Text, UC.myDTFI)));
                else
                    dg.Add("REALENDDATE", planneddate);

                dg.Add("PLANNEDMINUTEDURATION", (PlannedMinuteDuration.Text.Length > 0) ? int.Parse(PlannedMinuteDuration.Text) : 0);
                dg.Add("CURRENTMINUTEDURATION", (CurrentMinuteDuration.Text.Length > 0) ? int.Parse(CurrentMinuteDuration.Text) : 0);

                if (newsection)
                {
                    dg.Add("CREATEDDATE", DateTime.UtcNow);
                    dg.Add("CREATEDBYID", UC.UserId);
                }
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", UC.UserId);
                dg.Execute("PROJECT_TIMING", "IDRIF=" + SelectSession.Text);
            }



            DatabaseConnection.DoCommand(string.Format(@"update project_timing set plannedminuteduration=(
select sum(plannedminuteduration) from project_timing
inner join project_section_members on project_timing.idrif=project_section_members.ID and project_timing.riftype=1
where project_section_members.IDSectionRif={0})
where project_timing.IDRif={0} AND project_timing.riftype=0", SelectSession.Text));

            if (GanttEdit)
            {
                Session["currentproject"] = prjID;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "reloadgantt", "<script>alert('Sezione aggiornata.');opener.location=opener.location;self.close();</script>");
            }else
                BindSections();
        }

        void btnSelectSession_Click(object sender, EventArgs e)
        {
            FillSection();
        }

        public void FillSection()
        {
            string q = @"SELECT     PROJECT_SECTION.TITLE, PROJECT_SECTION.DESCRIPTION, PROJECT_SECTION.PARENT, PROJECT_SECTION.MEMBERID, PROJECT_TIMING.STATUS,
                      PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.REALSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_TIMING.REALENDDATE,
                      PROJECT_TIMING.PROGRESS, PROJECT_TIMING.PLANNEDMINUTEDURATION, PROJECT_TIMING.CURRENTMINUTEDURATION, PROJECT_TIMING.ID AS TIMINGID,
                      PROJECT_SECTION.ID,PROJECT_SECTION.COLOR, PROJECT_SECTION.COSTTYPE, PROJECT_SECTION.AMOUNT
                      FROM         PROJECT_SECTION LEFT OUTER JOIN
                      PROJECT_TIMING ON (PROJECT_SECTION.ID = PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE = 0) WHERE PROJECT_SECTION.ID=" + SelectSession.Text;
            DataRow dr = DatabaseConnection.CreateDataset(q).Tables[0].Rows[0];

            SectionName.Text = dr["TITLE"].ToString();
            SectionDescription.Text = dr["DESCRIPTION"].ToString();
            SectionOwnerID.Text = dr["MEMBERID"].ToString();
            SectionOwner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + SectionOwnerID.Text);

            if (dr["PLANNEDSTARTDATE"] != System.DBNull.Value)
                PlannedStartDate.Text = UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDSTARTDATE"]).ToShortDateString();
            if (dr["PLANNEDENDDATE"] != System.DBNull.Value)
                PlannedEndDate.Text = UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]).ToShortDateString();
            PlannedMinuteDuration.Text = dr["PLANNEDMINUTEDURATION"].ToString();

            if (dr["REALSTARTDATE"] != System.DBNull.Value)
                RealStartDate.Text = UC.LTZ.ToLocalTime((DateTime)dr["REALSTARTDATE"]).ToShortDateString();
            if (dr["REALENDDATE"] != System.DBNull.Value)
                RealEndDate.Text = UC.LTZ.ToLocalTime((DateTime)dr["REALENDDATE"]).ToShortDateString();

            FillSectionParent(SelectSession.Text);
            if (dr["PARENT"] != System.DBNull.Value)
                foreach (ListItem li in SectionParent.Items)
                {
                    if (li.Value == dr["PARENT"].ToString())
                    {
                        li.Selected = true;
                        break;
                    }
                }
            else
                SectionParent.SelectedIndex = 0;

            CostType.SelectedIndex = Convert.ToInt32((byte)dr["COSTTYPE"]);
            if (dr["Amount"] != System.DBNull.Value)
                SectionAmount.Text = ((decimal)dr["Amount"]).ToString(UC.myDTFI);

            LoadFilterColors();
            if (dr["COLOR"] != System.DBNull.Value)
            {
                for (int i = 0; i < FilterColor.Items.Count; i++)
                {
                    Trace.Warn(FilterColor.Items[i].Value, dr["COLOR"].ToString());
                    FilterColor.SelectedIndex = -1;
                    if (FilterColor.Items[i].Value == dr["COLOR"].ToString())
                    {
                        FilterColor.Items[i].Selected = true;
                        break;
                    }
                }
            }

            string avquery = @"SELECT PROGRESS,WEIGHT FROM PROJECT_TIMING RIGHT OUTER JOIN PROJECT_SECTION_MEMBERS ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION_MEMBERS.ID AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF=" + SelectSession.Text;

            ProjectCalculator pc = new ProjectCalculator();
            string average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]).ToString();

            SectionProgress.Text = string.Format("<span style=\"border:1px solid #000000;width:100%\"><span style=\"background-color:gold;width:{0}%;color:#000000;text-align:center\">{0}%</span></span>", average);

            string variation = string.Empty;
            DataRow drstart = DatabaseConnection.CreateDataset(string.Format(@"SELECT (SELECT TOP 1 NEWSTARTDATE FROM PROJECT_TIMING_VARIATION
INNER JOIN PROJECT_TIMING ON (PROJECT_TIMING_VARIATION.IDTIMING=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
WHERE PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
ORDER BY NEWSTARTDATE ASC) AS VARIATION,
(SELECT TOP 1 NEWSTARTDATE FROM PROJECT_TIMING_VARIATION
INNER JOIN PROJECT_TIMING ON (PROJECT_TIMING_VARIATION.IDTIMING=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
INNER JOIN PROJECT_SECTION ON (PROJECT_TIMING.IDRIF = PROJECT_SECTION.ID)
WHERE PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
ORDER BY NEWSTARTDATE ASC) AS GLOBALVARIATION
FROM PROJECT_SECTION
INNER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=0)
WHERE PROJECT_SECTION.ID={0};", SelectSession.Text)).Tables[0].Rows[0];
            if (drstart[1] != System.DBNull.Value)
                variation = "Data inizio: " + UC.LTZ.ToLocalTime((DateTime)drstart[1]).ToShortDateString() + "<br>";
            else if (drstart[0] != System.DBNull.Value)
                variation = "Data inizio: " + UC.LTZ.ToLocalTime((DateTime)drstart[0]).ToShortDateString() + "<br>";

            DataRow drend = DatabaseConnection.CreateDataset(string.Format(@"SELECT (select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
order by newplanneddate desc) as variation,
(select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_Timing.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newplanneddate desc) as globalvariation
FROM PROJECT_SECTION
 JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=0)
WHERE PROJECT_SECTION.ID={0};", SelectSession.Text)).Tables[0].Rows[0];
            if (drend[1] != System.DBNull.Value)
                variation += "Data fine: " + UC.LTZ.ToLocalTime((DateTime)drend[1]).ToShortDateString() + "<br>";
            else if (drend[0] != System.DBNull.Value)
                variation += "Data fine: " + UC.LTZ.ToLocalTime((DateTime)drend[0]).ToShortDateString() + "<br>";

            object varminute = DatabaseConnection.SqlScalartoObj(@"select sum(plannedminute) from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_TIMING_VARIATION.RIFTYPE=1 AND PROJECT_SECTION.ID=" + SelectSession.Text);

            if (varminute != null && varminute != System.DBNull.Value)
                variation += "Ore previste: " + (int.Parse(varminute.ToString()) + int.Parse(PlannedMinuteDuration.Text)).ToString();

            if (variation.Length > 0)
            {
                LitVariation.Text = variation;
                VariationRow.Visible = true;
            }

            MainTable.Visible = false;
            SessionTable.Visible = true;
            ProjectToDo1.prjID = prjID;
            ProjectToDo1.sectionID = long.Parse(SelectSession.Text);
            ProjectToDo1.BindToDo();
            ProjectToDo1.FillToDo();
        }

        #endregion

        protected void btnNewSession_Click(object sender, EventArgs e)
        {
            SelectSession.Text = "-1";
            SectionName.Text = string.Empty;
            SectionDescription.Text = string.Empty;
            SectionOwnerID.Text = string.Empty;
            SectionOwner.Text = string.Empty;

            PlannedStartDate.Text = string.Empty;
            PlannedEndDate.Text = string.Empty;
            PlannedMinuteDuration.Text = string.Empty;

            RealStartDate.Text = string.Empty;
            RealEndDate.Text = string.Empty;
            CurrentMinuteDuration.Text = string.Empty;

            FillSectionParent(string.Empty);
            SectionParent.SelectedIndex = 0;

            ProjectToDo1.prjID = prjID;
            ProjectToDo1.sectionID = long.Parse(SelectSession.Text);
            ProjectToDo1.BindToDo();

            MainTable.Visible = false;
            SessionTable.Visible = true;
            LoadFilterColors();
        }
    }
}
