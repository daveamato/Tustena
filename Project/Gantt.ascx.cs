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
using Digita.Tustena.Project;
using System.Drawing;
using System.Collections.Specialized;
using System.Web.Caching;
using Digita.Tustena.Core;

namespace Digita.Tustena.Project
{
    public partial class Gantt : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();

        ArrayList connectors =new ArrayList();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public bool NoRender = false;

        public System.Drawing.Image Img1 = null;
        public System.Drawing.Image Img2 = null;

        public void MakeGantt()
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            DataSet prjInfo = DatabaseConnection.CreateDataset(@"SELECT PROJECT.TITLE, ACCOUNT.NAME + ' ' + ACCOUNT.SURNAME AS OWNERNAME
FROM PROJECT INNER JOIN ACCOUNT ON PROJECT.OWNER = ACCOUNT.UID
WHERE ID=" + prjID);
            DataSet sections = DatabaseConnection.CreateDataset(String.Format(@"SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.DESCRIPTION,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID,
ACCOUNT.NAME+' '+ACCOUNT.SURNAME AS OWNERNAME, PROJECT_TIMING.PLANNEDMINUTEDURATION, PROJECT_SECTION.COLOR
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
INNER JOIN ACCOUNT ON PROJECT_SECTION.MEMBERID=ACCOUNT.UID
WHERE PROJECT_SECTION.PARENT IS NULL AND PROJECT_SECTION.IDRIF={0}
ORDER BY PROJECT_SECTION.SECTIONORDER;
SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.DESCRIPTION,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID,
ACCOUNT.NAME+' '+ACCOUNT.SURNAME AS OWNERNAME, PROJECT_TIMING.PLANNEDMINUTEDURATION, PROJECT_SECTION.COLOR
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
INNER JOIN ACCOUNT ON PROJECT_SECTION.MEMBERID=ACCOUNT.UID
WHERE PROJECT_SECTION.PARENT IS NOT NULL AND PROJECT_SECTION.IDRIF={0} ORDER BY PROJECT_SECTION.SECTIONORDER;", prjID));

            if (sections.Tables[0].Rows.Count > 0)
            {

                DigiGantt.CoordsMaps rects = new DigiGantt.CoordsMaps();
                DigiGantt dg = new DigiGantt();

                DataRow startdates = DatabaseConnection.CreateDataset(string.Format(@"SELECT TOP 1 PLANNEDSTARTDATE,
(select top 1 newstartdate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_SECTION.IDRIF={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
order by newstartdate asc) as variation,
(select top 1 newstartdate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_Timing.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.IDRIF={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newstartdate asc) as globalvariation
FROM PROJECT_SECTION
INNER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=0)
WHERE PROJECT_SECTION.IDRIF={0} AND PLANNEDSTARTDATE IS NOT NULL ORDER BY PLANNEDSTARTDATE ASC;", prjID)).Tables[0].Rows[0];

                DateTime firststartDate = UC.LTZ.ToLocalTime((DateTime)startdates[0]);
                DateTime startDate;
                if (startdates[1] != System.DBNull.Value)
                {
                    if (startdates[2] != System.DBNull.Value && ((DateTime)startdates[1]) < ((DateTime)startdates[2]))
                        startDate = UC.LTZ.ToLocalTime((DateTime)startdates[1]);
                    else
                        startDate = UC.LTZ.ToLocalTime((DateTime)startdates[2]);
                }
                else
                    if (startdates[2] != System.DBNull.Value && firststartDate < ((DateTime)startdates[2]))
                        startDate = firststartDate;
                    else if (startdates[2] != System.DBNull.Value)
                            startDate = UC.LTZ.ToLocalTime((DateTime)startdates[2]);
                        else
                            startDate = firststartDate;

                DataRow enddates = DatabaseConnection.CreateDataset(string.Format(@"SELECT top 1 PLANNEDENDDATE,
(select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_SECTION.IDRIF={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
order by newplanneddate desc) as variation,
(select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_Timing.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.IDRIF={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newplanneddate desc) as globalvariation
FROM PROJECT_SECTION
 JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=0)
WHERE PROJECT_SECTION.IDRIF={0} AND PLANNEDSTARTDATE IS NOT NULL ORDER BY PLANNEDENDDATE DESC;", prjID)).Tables[0].Rows[0];

                DateTime firstendDate = UC.LTZ.ToLocalTime((DateTime)enddates[0]);
                DateTime endDate= firstendDate;
                if (enddates[1] != System.DBNull.Value)
                {
                    if(enddates[2] != System.DBNull.Value && ((DateTime)enddates[1])>((DateTime)enddates[2]))
                        endDate = UC.LTZ.ToLocalTime((DateTime)enddates[1]);
                    else if(enddates[2] != System.DBNull.Value)
                            endDate = UC.LTZ.ToLocalTime((DateTime)enddates[2]);
                        else if(((DateTime)enddates[1])>firstendDate)
                            endDate = UC.LTZ.ToLocalTime((DateTime)enddates[1]);

                }
                else
                    if (enddates[2] != System.DBNull.Value && firstendDate > ((DateTime)enddates[2]))
                        endDate = firstendDate;
                    else if (enddates[2] != System.DBNull.Value)
                        endDate = UC.LTZ.ToLocalTime((DateTime)enddates[2]);


                System.Drawing.Image img = dg.DrawSchema(out rects, 2000, startDate.AddDays(-2), endDate.AddDays(2), DigiGantt.RowsFlags.NoYears);
                rects.Add(dg.DrawProject(startDate, endDate, 1));
                if (firstendDate != endDate)
                {
                    rects.Add(dg.DrawExpected(firstendDate, Color.LightGreen, 1, 0, "Variazione data"));
                }
                int j = 2;
                ArrayList arsections = new ArrayList();
                ArrayList arsections2 = new ArrayList();
                ArrayList arsections3 = new ArrayList();
                ArrayList arsections4 = new ArrayList();
                ArrayList arsections5 = new ArrayList();

                arsections.Add("Descrizione");
                arsections2.Add("Inizio");
                arsections3.Add("Fine");
                arsections4.Add("Durata");
                arsections5.Add("Responsabile");
                arsections.Add("{P00}" + prjInfo.Tables[0].Rows[0][0].ToString());
                arsections2.Add(startDate.ToString(@"dd/MM/yy"));
                arsections3.Add(endDate.ToString(@"dd/MM/yy"));
                arsections4.Add(DatabaseConnection.SqlScalar(string.Format(@"SELECT SUM(PLANNEDMINUTEDURATION)
FROM PROJECT_SECTION
INNER JOIN PROJECT_TIMING ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION.ID AND PROJECT_TIMING.RIFTYPE=0)
WHERE PROJECT_SECTION.IDRIF={0}", prjID)));
                arsections5.Add(prjInfo.Tables[0].Rows[0][1].ToString());
                foreach (DataRow dr in sections.Tables[0].Rows)
                {
                    j = MakeGanttBar(ref rects, ref dg, j, ref arsections, ref arsections2, ref arsections3, ref arsections4, ref arsections5, dr, 0, sections);
                }

                DataTable dtrelations = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT_RELATIONS WHERE PROJECTID=" + prjID + " ORDER BY ID").Tables[0];
                foreach (DataRow drrelations in dtrelations.Rows)
                {
                    int r1 = 0;
                    int r2 = 0;
                    foreach (string i in connectors)
                    {
                        string[] s = i.Split('|');
                        if (s[0] == drrelations["FIRSTRIFID"].ToString())
                        {
                            r1 = int.Parse(s[1]);
                            break;
                        }
                    }
                    foreach (string i in connectors)
                    {
                        string[] s = i.Split('|');
                        if (s[0] == drrelations["SECONDRIFID"].ToString())
                        {
                            r2 = int.Parse(s[1]);
                            break;
                        }
                    }
                    DateTime d1 = new DateTime();
                    if (drrelations["RELATIONTYPE"].ToString() == "0")
                        d1 = UC.LTZ.ToLocalTime((DateTime)DatabaseConnection.SqlScalartoObj("SELECT PLANNEDSTARTDATE FROM PROJECT_TIMING WHERE RIFTYPE=0 AND IDRIF=" + drrelations["FIRSTRIFID"].ToString()));
                    else
                    {
                        d1 = UC.LTZ.ToLocalTime((DateTime)DatabaseConnection.SqlScalartoObj("SELECT PLANNEDENDDATE FROM PROJECT_TIMING WHERE RIFTYPE=0 AND IDRIF=" + drrelations["FIRSTRIFID"].ToString()));
                        object variationend = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
order by newplanneddate desc", drrelations["FIRSTRIFID"].ToString()));
                        object variationend2 = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newplanneddate desc", drrelations["FIRSTRIFID"].ToString()));

                        if (variationend != null && variationend!=System.DBNull.Value)
                        {
                            if ((variationend2 != null && variationend2 != System.DBNull.Value) && ((DateTime)variationend2) > ((DateTime)variationend))
                                d1 = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2));
                            else
                                d1 = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend));
                        }
                        else
                            if (variationend2 != null && variationend2 != System.DBNull.Value)
                                d1 = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2));
                    }

                    DateTime d2 = UC.LTZ.ToLocalTime((DateTime)DatabaseConnection.SqlScalartoObj("SELECT PLANNEDSTARTDATE FROM PROJECT_TIMING WHERE RIFTYPE=0 AND IDRIF=" + drrelations["SECONDRIFID"].ToString()));

                    object variationstart = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newstartdate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newstartdate desc", drrelations["SECONDRIFID"].ToString()));

                    if (variationstart != null && variationstart != System.DBNull.Value)
                        d2 = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationstart));

                    dg.DrawConnector(d1, r1, d2, r2);
                }

                img = dg.CloseGanttChart(img, j);

                string[][] mdArr = new string[5][] { ((string[])arsections.ToArray(typeof(System.String))), ((string[])arsections2.ToArray(typeof(System.String))), ((string[])arsections3.ToArray(typeof(System.String))), ((string[])arsections4.ToArray(typeof(System.String))), ((string[])arsections5.ToArray(typeof(System.String))) };
                System.Drawing.Image img2 = dg.DrawLegend(ref rects, mdArr, 0);

                if (NoRender)
                {
                    this.Img1 = img;
                    this.Img2 = img2;
                }
                else
                {
                    Cache.Add("Projectimg1_" + prjID, dg.ConvertImageToByteArray(img, System.Drawing.Imaging.ImageFormat.Png), null, DateTime.Now.AddSeconds(30), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
                    Cache.Add("Projectimg2_" + prjID, dg.ConvertImageToByteArray(img2, System.Drawing.Imaging.ImageFormat.Png), null, DateTime.Now.AddSeconds(30), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);
                    litGantt.Text = dg.ImageMapRender("/project/GanttImg.aspx?prjID=" + prjID, rects);
                }
            }
            else
            {
                litGantt.Text = "<span style=\"color:red\">&nbsp;&nbsp;Nessuna sezione in questo progetto.</span>";
            }
        }

        private int MakeGanttBar(ref DigiGantt.CoordsMaps rects, ref DigiGantt dg, int j, ref ArrayList arsections, ref ArrayList arsections2, ref ArrayList arsections3, ref ArrayList arsections4, ref ArrayList arsections5, DataRow dr, int padLeft, DataSet sections)
        {
            string avquery = @"SELECT PROGRESS,WEIGHT FROM PROJECT_TIMING RIGHT OUTER JOIN PROJECT_SECTION_MEMBERS ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION_MEMBERS.ID AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF=" + dr["ID"].ToString();

            ProjectCalculator pc = new ProjectCalculator();
            int average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]);

            arsections.Add("{S" + padLeft.ToString().PadLeft(2, '0') + "}" + dr["TITLE"].ToString());
            DateTime plannedstart = UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDSTARTDATE"]);

            object variationstart = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newstartdate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newstartdate desc", dr["ID"].ToString()));

            DateTime DrawStart = plannedstart;
            if (variationstart != null && variationstart != System.DBNull.Value)
            {
                arsections2.Add(UC.LTZ.ToLocalTime(Convert.ToDateTime(variationstart)).ToString(@"dd/MM/yy"));
                DrawStart = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationstart));
            }
            else
                arsections2.Add(plannedstart.ToString(@"dd/MM/yy"));


            DateTime plannedend = UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]);

            object variationend = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=1)
inner join PROJECT_SECTION_MEMBERS on PROJECT_TIMING.idrif=PROJECT_SECTION_MEMBERS.id
inner join PROJECT_SECTION on (PROJECT_SECTION_MEMBERS.IDSectionRif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=1
order by newplanneddate desc", dr["ID"].ToString()));
            object variationend2 = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND PROJECT_TIMING.RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0} AND PROJECT_TIMING_VARIATION.RIFTYPE=0
order by newplanneddate desc", dr["ID"].ToString()));



            DateTime DrawEnd = plannedend;
            if(variationend!=null && variationend!=System.DBNull.Value)
                if ((variationend2 != null && variationend2 != System.DBNull.Value) && ((DateTime)variationend2) > ((DateTime)variationend))
                {
                    arsections3.Add(UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2)).ToString(@"dd/MM/yy"));
                    DrawEnd = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2));
                }
                else
                {
                    arsections3.Add(UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend)).ToString(@"dd/MM/yy"));
                    DrawEnd = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend));
                }
            else
                if ((variationend2 != null && variationend2 != System.DBNull.Value))
                {
                    arsections3.Add(UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2)).ToString(@"dd/MM/yy"));
                    DrawEnd = UC.LTZ.ToLocalTime(Convert.ToDateTime(variationend2));
                }
                else
                    arsections3.Add(plannedend.ToString(@"dd/MM/yy"));

            arsections4.Add(dr["PLANNEDMINUTEDURATION"].ToString());
            arsections5.Add(dr["OWNERNAME"].ToString());
            connectors.Add(dr["ID"].ToString()+'|'+j.ToString());

            if(dr["COLOR"]!=System.DBNull.Value)
                rects.Add(dg.DrawSection(DrawStart, DrawEnd, Color.FromName(dr["COLOR"].ToString()), j++, average, Convert.ToInt32(dr["ID"])));
            else
                rects.Add(dg.DrawSection(DrawStart, DrawEnd, Color.Gold, j++, average, Convert.ToInt32(dr["ID"])));

            if (DrawEnd != plannedend || DrawStart != plannedstart)
            {
                rects.Add(dg.DrawGhost(plannedstart, plannedend, (j - 1), Color.FromName(dr["COLOR"].ToString()), 0));
            }

                if (DrawEnd != plannedend && (plannedend>DrawStart && plannedend<DrawEnd))
                    rects.Add(dg.DrawExpected(plannedend, Color.LightGreen, (j - 1), 0, "Variazione fine"));

                if (DrawStart != plannedstart && (plannedstart>DrawStart && plannedstart<DrawEnd))
                    rects.Add(dg.DrawExpected(plannedstart.AddDays(-1), Color.LightSkyBlue, (j - 1), 0, "Variazione inizio"));

            DataSet dsevents = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT_EVENTS WHERE SECTIONID=" + dr["ID"].ToString());
            if (dsevents.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow drevent in dsevents.Tables[0].Rows)
                {
                    arsections.Add("{E" + padLeft.ToString().PadLeft(2, '0') + "}" + drevent["DESCRIPTION"].ToString());
                    arsections2.Add((UC.LTZ.ToLocalTime((DateTime)drevent["EVENTDATE"])).ToString(@"dd/MM/yy"));
                    arsections3.Add(string.Empty);
                    arsections4.Add(string.Empty);
                    arsections5.Add(string.Empty);
                    rects.Add(dg.DrawPoint(UC.LTZ.ToLocalTime((DateTime)drevent["EVENTDATE"]), Color.Orange, j++, Convert.ToInt32(dr["ID"]), drevent["DESCRIPTION"].ToString()));
                }
            }

            SubSection(ref rects, ref dg, ref j, ref arsections, ref arsections2, ref arsections3, ref arsections4, ref arsections5, sections, ++padLeft, dr["ID"].ToString());
            return j;
        }

        private void SubSection(ref DigiGantt.CoordsMaps rects, ref DigiGantt dg, ref int j, ref ArrayList arsections, ref ArrayList arsections2, ref ArrayList arsections3, ref ArrayList arsections4, ref ArrayList arsections5, DataSet sections, int padLeft, string parentsectionid)
        {
            DataRow[] dr1 = sections.Tables[1].Select(string.Format("[PARENT]='{0}'", parentsectionid));
            if (dr1.Length > 0)
            {
                foreach (DataRow dr in dr1)
                {
                    j = MakeGanttBar(ref rects, ref dg, j, ref arsections, ref arsections2, ref arsections3, ref arsections4, ref arsections5, dr, padLeft, sections);
                }
            }
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
            }
        }
    }
}
