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
using System.Collections.Generic;
using System.Text;
using System.Data;
using Digita.Tustena.Database;
using Digita.Tustena.Core;

namespace Digita.Tustena.Project
{
    class ProjectReport
    {

        long _prjID = 0;
        public long prjID
        {
            get
            {
                return _prjID;
            }
            set
            {
                _prjID = value;
            }
        }

        long _MemberId = 0;
        public long MemberId
        {
            get
            {
                return _MemberId;
            }
            set
            {
                _MemberId = value;
            }
        }

        UserConfig _UC = null;
        public UserConfig UC
        {
            get
            {
                return _UC;
            }
            set
            {
                _UC = value;
            }
        }

        public string ProjectTiming(bool expand, bool allmembers)
        {

            StringBuilder sb = new StringBuilder();

            string query = @"SELECT PROJECT_SECTION.TITLE,PROJECT_SECTION_MEMBERS.TODODESCRIPTION,
PROJECT_TIMING.PLANNEDSTARTDATE,PROJECT_TIMING.PLANNEDENDDATE,PROJECT_TIMING.PROGRESS,
PROJECT_TIMING.PLANNEDMINUTEDURATION, PROJECT_TIMING.CURRENTMINUTEDURATION, PROJECT_SECTION_MEMBERS.ID
FROM PROJECT_SECTION_MEMBERS
INNER JOIN PROJECT_SECTION ON PROJECT_SECTION_MEMBERS.IDSECTIONRIF=PROJECT_SECTION.ID
INNER JOIN PROJECT_TIMING ON (PROJECT_SECTION_MEMBERS.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.MEMBERID={0}
ORDER BY PROJECT_SECTION.ID";

            string titlestyle = "style=\"font-family : Verdana, Arial, Helvetica, sans-serif;border:1px solid black;font-size:12px;font-weight: bold;background-color: #DDD;\"";

            string prjtxt = DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT WHERE ID=" + prjID);
            sb.AppendFormat("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\"><tr><td {1}>Progetto: {0}</td></tr></table><br>", prjtxt, titlestyle);

            if (allmembers)
            {
                string qmembers = string.Format(@"SELECT PROJECT_MEMBERS.ID, PROJECT_TEAMS.DESCRIPTION FROM PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
WHERE PROJECT_TEAMS.PROJECTID={0}
ORDER BY PROJECT_TEAMS.ID", prjID);
                DataSet ds = DatabaseConnection.CreateDataset(qmembers);
                foreach(DataRow dr in ds.Tables[0].Rows)
                    FillTimingTable(expand, ref sb, query, long.Parse(dr[0].ToString()),dr[1].ToString());

            }
            else
            {
                string team = DatabaseConnection.SqlScalar(@"SELECT PROJECT_TEAMS.DESCRIPTION from PROJECT_MEMBERS
INNER JOIN PROJECT_TEAMS ON PROJECT_MEMBERS.TEAM=PROJECT_TEAMS.ID
WHERE PROJECT_MEMBERS.ID="+MemberId);
                FillTimingTable(expand, ref sb, query, MemberId,team);
            }

            return sb.ToString();
        }

        private void FillTimingTable(bool expand, ref StringBuilder sb, string query, long memberid, string team)
        {
            string titlestyle = "style=\"border-bottom:1px solid black;font-size:10px;font-weight: bold;background-color: #DDD;\"";
            string headerstyle = "style=\"border-bottom:1px solid black;font-size:10px;font-weight: bold;\"";
            string topstyle = "style=\"border-bottom:1px solid black;font-size:12px;font-weight: bold;background-color: #DDD;\"";
            string itemsstyle = "style=\"border-bottom:1px solid black;font-size:9px;\"";

            string[] membertype = null;
            string membertxt = string.Empty;
            membertype = DatabaseConnection.SqlScalar("SELECT CAST(TYPE AS VARCHAR(10))+'|'+CAST(USERID AS VARCHAR(10)) FROM PROJECT_MEMBERS WHERE ID=" + memberid).Split('|');
            if (membertype[0] == "1")
                membertxt = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID=" + membertype[1]);
            else
                membertxt = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + membertype[1]);

            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\" bgcolor=\"#ffffff\" style=\"border-top:1px solid #000;border-left:1px solid #000;border-right:1px solid #000;font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
            sb.AppendFormat("<tr><td colspan=6 {1}>Team: {2}&nbsp;&nbsp;&nbsp;&nbsp;Utente: {0}</td></tr>", membertxt, topstyle, team);
            DataSet ds = DatabaseConnection.CreateDataset(string.Format(query, memberid.ToString()));
            string section = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["TITLE"].ToString() != section)
                {
                    section = dr["TITLE"].ToString();
                    sb.AppendFormat("<tr><td colspan=6 {1}>Sezione: {0}</td></tr>", section, titlestyle);
                }
                string lens = string.Empty;
                if (expand)
                    lens = string.Format("<img src=/i/lens.gif style=\"cursor:pointer\" onclick=\"Expand({0})\">&nbsp;", dr["ID"].ToString());
                sb.AppendFormat("<tr><td {0} valign=top>Descrizione</td><td {0}>Inizio</td><td {0}>Fine</td><td {0}>Ore previste</td><td {0}>Ore attuali</td><td {0}>Progressione</td></tr>", headerstyle);
                sb.AppendFormat("<tr><td {2} valign=top>{1}{0}</td>", dr["TODODESCRIPTION"].ToString().Replace("\r", "").Replace("\n", "<br>"), lens, itemsstyle);
                sb.AppendFormat("<td {1} valign=top>{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDSTARTDATE"]).ToShortDateString(), itemsstyle);
                sb.AppendFormat("<td {1} valign=top>{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]).ToShortDateString(), itemsstyle);
                sb.AppendFormat("<td {1} valign=top>{0}</td>", dr["PLANNEDMINUTEDURATION"].ToString(), itemsstyle);
                sb.AppendFormat("<td {1} valign=top>{0}</td>", dr["CURRENTMINUTEDURATION"].ToString(), itemsstyle);


                string bgcolor = "gold";
                if ((DateTime)dr["PLANNEDENDDATE"] < DateTime.UtcNow && int.Parse(dr["PROGRESS"].ToString()) < 100) bgcolor = "red";
                if ((DateTime)dr["PLANNEDENDDATE"] > DateTime.UtcNow && int.Parse(dr["PROGRESS"].ToString()) < 100) bgcolor = "#0df30d";
                sb.AppendFormat("<td style=\"border-bottom:1px solid #000000;width:20%\" align=left><div style=\"border:1px solid #000000;width:100%\"><div style=\"border-right:1px solid #000000;background-color:{1};width:{0}%;color:#000000;text-align:center\">{0}%</div></div></td></tr>", dr["PROGRESS"], bgcolor);

                string qdeatil = string.Format(@"SELECT STARTDATE,DESCRIPTION,MINUTEDURATION,MATERIAL,DELAY
FROM PROJECT_DAYLOG
WHERE TODORIF={0} AND MEMBERID={1}", dr["ID"].ToString(), memberid);
                DataSet dsdetail = DatabaseConnection.CreateDataset(qdeatil);

                string style = string.Empty;
                if (expand)
                    style = "style=\"display:none\"";


                sb.AppendFormat("<tr id=\"detail_{0}\" {1}>", dr["ID"].ToString(), style);
                sb.Append("<td colspan=6 align=right><table width=\"90%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\" style=\"background-color: #EEE;font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
                sb.AppendFormat("<tr><td {0} width=\"10%\">Data</td><td {0} width=\"30%\">Descrizione</td><td {0} width=\"5%\">Ore</td><td {0} width=\"25%\">Materiali</td><td {0} width=\"30%\">Ritardo</td></tr>", headerstyle);
                foreach (DataRow dritem in dsdetail.Tables[0].Rows)
                {
                    sb.AppendFormat("<tr><td {1} valign=top>{0}</td>", UC.LTZ.ToLocalTime((DateTime)dritem["STARTDATE"]).ToShortDateString(), itemsstyle);
                    sb.AppendFormat("<td {1} valign=top>{0}&nbsp;</td>", dritem["DESCRIPTION"].ToString().Replace("\r", "").Replace("\n", "<br>"), itemsstyle);
                    sb.AppendFormat("<td {1} valign=top>{0}&nbsp;</td>", dritem["MINUTEDURATION"].ToString(), itemsstyle);
                    sb.AppendFormat("<td {1} valign=top>{0}&nbsp;</td>", dritem["MATERIAL"].ToString().Replace("\r", "").Replace("\n", "<br>"), itemsstyle);
                    sb.AppendFormat("<td {1} valign=top>{0}&nbsp;</td></tr>", dritem["DELAY"].ToString().Replace("\r", "").Replace("\n", "<br>"), itemsstyle);
                }
                sb.Append("</table></td></tr>");
            }
            sb.Append("</table><br><br>");
        }

        public string ToDoList()
        {
            string prjtxt = DatabaseConnection.SqlScalar("SELECT TITLE FROM PROJECT WHERE ID=" + prjID);
            string[] membertype = DatabaseConnection.SqlScalar("SELECT CAST(TYPE AS VARCHAR(10))+'|'+CAST(USERID AS VARCHAR(10)) FROM PROJECT_MEMBERS WHERE ID=" + MemberId).Split('|');
            string membertxt = string.Empty;
            if(membertype[0]=="1")
                membertxt = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID=" + membertype[1]);
            else
                membertxt = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+membertype[1]);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
            sb.AppendFormat("<tr><td colspan=2><b>{0}</b></td><td colspan=2><b>{1}</b></td></tr>",prjtxt,membertxt);

            string query = string.Format(@"SELECT PROJECT_SECTION.TITLE,PROJECT_SECTION_MEMBERS.TODODESCRIPTION,
PROJECT_TIMING.PLANNEDSTARTDATE,PROJECT_TIMING.PLANNEDENDDATE,PROJECT_TIMING.PROGRESS
FROM PROJECT_SECTION_MEMBERS
INNER JOIN PROJECT_SECTION ON PROJECT_SECTION_MEMBERS.IDSECTIONRIF=PROJECT_SECTION.ID
INNER JOIN PROJECT_TIMING ON (PROJECT_SECTION_MEMBERS.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.MEMBERID={0}
ORDER BY PROJECT_SECTION.ID",MemberId);

            DataSet ds = DatabaseConnection.CreateDataset(query);
            string section = string.Empty;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr["TITLE"].ToString() != section)
                {
                    section = dr["TITLE"].ToString();
                    sb.AppendFormat("<tr><td colspan=4><b>{0}</b></td></tr>", section);
                }
                sb.AppendFormat("<tr><td>{0}</td>", dr["TODODESCRIPTION"].ToString().Replace("\r", "").Replace("\n", "<br>"));
                sb.AppendFormat("<td>{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDSTARTDATE"]).ToShortDateString());
                sb.AppendFormat("<td>{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]).ToShortDateString());
                string bgcolor = "gold";
                if ((DateTime)dr["PLANNEDENDDATE"] < DateTime.UtcNow && int.Parse(dr["PROGRESS"].ToString()) < 100) bgcolor = "red";
                if ((DateTime)dr["PLANNEDENDDATE"] > DateTime.UtcNow && int.Parse(dr["PROGRESS"].ToString()) < 100) bgcolor = "#0df30d";
                sb.AppendFormat("<td style=\"border:1px solid #000000;width:20%\" align=left><div style=\"border-right:1px solid #000000;background-color:{1};width:{0}%;color:#000000;text-align:center\">{0}%</div></td></tr>", dr["PROGRESS"], bgcolor);
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        public string FillStatus()
        {
            DataRow drprj = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT WHERE ID=" + prjID.ToString()).Tables[0].Rows[0];
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" style=\"font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
            sb.AppendFormat("<tr><td colspan=5 style=\"border-bottom:1px solid #000000;padding-bottom:15px;\"><b>{0}</b><br>{1}</td></tr>", drprj["TITLE"].ToString(), drprj["DESCRIPTION"].ToString().Replace("\n", "<br>"));


            DataSet sections = DatabaseConnection.CreateDataset(String.Format(@"SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.DESCRIPTION,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
WHERE PROJECT_SECTION.PARENT IS NULL AND PROJECT_SECTION.IDRIF={0}
ORDER BY PROJECT_SECTION.SECTIONORDER;
SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.DESCRIPTION,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
WHERE PROJECT_SECTION.PARENT IS NOT NULL AND PROJECT_SECTION.IDRIF={0} ORDER BY PROJECT_SECTION.SECTIONORDER;", prjID));


            foreach (DataRow dr in sections.Tables[0].Rows)
            {
                sb = MakeStructure(sb, sections, dr, string.Empty);
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        private StringBuilder MakeStructure(StringBuilder sb, DataSet sections, DataRow dr, string sectiontitle)
        {
            string avquery = @"SELECT PROGRESS,WEIGHT FROM PROJECT_TIMING RIGHT OUTER JOIN PROJECT_SECTION_MEMBERS ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION_MEMBERS.ID AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF=" + dr["ID"].ToString();
            ProjectCalculator pc = new ProjectCalculator();
            string average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]).ToString();


            sb.Append("<tr><td width=\"40%\" style=\"background-color:#ababab\"><b>Sezione</b></td><td style=\"background-color:#ababab\"><b>Inizio</b></td><td style=\"background-color:#ababab\"><b>Fine</b></td><td style=\"background-color:#ababab\"><b>Responsabile</b></td><td style=\"background-color:#ababab\"><b>Avanzamento</b></td></tr>");

            string bgcolor = "gold";
            if ((DateTime)dr["PLANNEDENDDATE"] < DateTime.UtcNow && int.Parse(average) < 100) bgcolor = "red";
            if ((DateTime)dr["PLANNEDENDDATE"] > DateTime.UtcNow && int.Parse(average) < 100) bgcolor = "#0df30d";

            sb.AppendFormat("<tr><td width=\"40%\"><b>{2}{0}</b><br>{1}</td>", dr["TITLE"].ToString(), dr["DESCRIPTION"].ToString().Replace("\n", "<br>"), sectiontitle);
            if (dr["PLANNEDSTARTDATE"] != System.DBNull.Value)
                sb.AppendFormat("<td width=\"10%\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDSTARTDATE"]).ToShortDateString());
            else
                sb.Append("<td width=\"10%\">Non definita</td>");
            if (dr["PLANNEDENDDATE"] != System.DBNull.Value)
                sb.AppendFormat("<td width=\"10%\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)dr["PLANNEDENDDATE"]).ToShortDateString());
            else
                sb.Append("<td width=\"10%\">Non definita</td>");
            sb.AppendFormat("<td width=\"20%\">{0}</td>", DatabaseConnection.SqlScalar("SELECT ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') FROM ACCOUNT WHERE UID=" + dr["MEMBERID"].ToString()));
            sb.AppendFormat("<td style=\"width:20%\"><div style=\"border:1px solid #000000;width:100%\"><div style=\"border-right:1px solid #000000;background-color:{1};width:{0}%;color:#000000;text-align:center\">{0}%</div></div></td></tr>", average, bgcolor);

            string qtask = string.Format(@"SELECT PROJECT_SECTION_MEMBERS.TODODESCRIPTION, PROJECT_SECTION_MEMBERS.MEMBERID, PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_TIMING.PROGRESS
FROM PROJECT_SECTION_MEMBERS INNER JOIN PROJECT_TIMING ON (PROJECT_SECTION_MEMBERS.ID=PROJECT_TIMING.IDRIF AND RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF={0}", dr["ID"].ToString());
            DataSet dstask = DatabaseConnection.CreateDataset(qtask);
            if (dstask.Tables[0].Rows.Count > 0)
            {
                sb.AppendFormat("<tr><td colspan=5 align=left style=\"border-bottom:1px solid #000000;padding-bottom:5px;\">");
                sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"2\" cellpadding=\"0\" style=\"font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
                sb.Append("<tr><td width=\"40%\" style=\"background-color:#ababab\"><b>Task</b></td><td style=\"background-color:#ababab\"><b>Inizio</b></td><td style=\"background-color:#ababab\"><b>Fine</b></td><td style=\"background-color:#ababab\"><b>Responsabile</b></td><td style=\"background-color:#ababab\"><b>Avanzamento</b></td></tr>");

                foreach (DataRow drtask in dstask.Tables[0].Rows)
                {
                    bgcolor = "gold";
                    if ((DateTime)dr["PLANNEDENDDATE"] < DateTime.UtcNow && int.Parse(drtask["PROGRESS"].ToString()) < 100) bgcolor = "red";
                    if ((DateTime)dr["PLANNEDENDDATE"] > DateTime.UtcNow && int.Parse(drtask["PROGRESS"].ToString()) < 100) bgcolor = "#0df30d";

                    sb.AppendFormat("<tr><td width=\"40%\">{0}</td>", drtask["TODODESCRIPTION"].ToString().Replace("\r", "").Replace("\n", "<br>"));
                    if (drtask["PLANNEDSTARTDATE"] != System.DBNull.Value)
                        sb.AppendFormat("<td width=\"10%\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)drtask["PLANNEDSTARTDATE"]).ToShortDateString());
                    else
                        sb.Append("<td width=\"10%\">Non definita</td>");
                    if (drtask["PLANNEDENDDATE"] != System.DBNull.Value)
                        sb.AppendFormat("<td width=\"10%\">{0}</td>", UC.LTZ.ToLocalTime((DateTime)drtask["PLANNEDENDDATE"]).ToShortDateString());
                    else
                        sb.Append("<td width=\"10%\">Non definita</td>");
                    sb.AppendFormat("<td width=\"20%\">{0}</td>", DatabaseConnection.SqlScalar("select isnull(account.name,'')+' '+isnull(account.surname,'') from account inner join project_members on (account.uid=project_members.userid and (project_members.type=0 or project_members.type=2)) where project_members.ID=" + drtask["MEMBERID"].ToString()));
                    sb.AppendFormat("<td style=\"border:1px solid #000000;width:20%\" align=left><div style=\"border-right:1px solid #000000;background-color:{1};width:{0}%;color:#000000;text-align:center\">{0}%</div></td>", drtask["PROGRESS"], bgcolor);
                }
                sb.Append("</table></td></tr>");
            }
            FillSubSection(ref sb, sections, dr["ID"].ToString(), dr["TITLE"].ToString() + " --> ");


            return sb;
        }

        void FillSubSection(ref StringBuilder sb, DataSet sections, string parentsectionid, string parentsectionname)
        {
            DataRow[] dr1 = sections.Tables[1].Select(string.Format("[PARENT]='{0}'", parentsectionid));
            if (dr1.Length > 0)
            {
                foreach (DataRow dr in dr1)
                {
                    sb = MakeStructure(sb, sections, dr, parentsectionname);
                }
            }
        }

        private DateTime GetForecastDate(DateTime start,DateTime end, int progress)
        {
            if (progress > 0)
            {
                long newticks = DateTime.Now.Ticks - start.Ticks;
                DateTime nd = new DateTime(((newticks / progress) * 100) + start.Ticks);
                return nd;
            }
            else
                return end;
        }

        public string ForecastDate()
        {
            DataRow drprj = DatabaseConnection.CreateDataset("SELECT * FROM PROJECT WHERE ID=" + prjID.ToString()).Tables[0].Rows[0];
            StringBuilder sb = new StringBuilder();
            sb.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"3\" style=\"font-family : Verdana, Arial, Helvetica, sans-serif;font-size: 11px;\">");
            sb.AppendFormat("<tr><td colspan=6 style=\"border-bottom:1px solid #000000;padding-bottom:15px;\"><b>{0}</b><br>{1}</td></tr>", drprj["TITLE"].ToString(), drprj["DESCRIPTION"].ToString().Replace("\n", "<br>"));
            sb.Append("<tr><td width=\"30%\" style=\"background-color:#ababab\"><b>Sezione</b></td><td style=\"background-color:#ababab\"><b>Inizio</b></td><td style=\"background-color:#ababab\"><b>Fine</b></td><td style=\"background-color:#ababab\"><b>Responsabile</b></td><td style=\"background-color:#ababab\"><b>Avanzamento</b></td><td style=\"background-color:#ababab\"><b>Previsione</b></td></tr>");

            DataSet sections = DatabaseConnection.CreateDataset(String.Format(@"SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
WHERE PROJECT_SECTION.PARENT IS NULL AND PROJECT_SECTION.IDRIF={0}
ORDER BY PROJECT_SECTION.SECTIONORDER;
SELECT PROJECT_SECTION.ID,PROJECT_SECTION.TITLE,PROJECT_SECTION.PARENT,PROJECT_TIMING.PROGRESS,PROJECT_TIMING.PLANNEDSTARTDATE, PROJECT_TIMING.PLANNEDENDDATE, PROJECT_SECTION.MEMBERID
FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT_TIMING ON (PROJECT_SECTION.ID=PROJECT_TIMING.IDRIF AND PROJECT_TIMING.RIFTYPE=0)
WHERE PROJECT_SECTION.PARENT IS NOT NULL AND PROJECT_SECTION.IDRIF={0} ORDER BY PROJECT_SECTION.SECTIONORDER;", prjID));


            foreach (DataRow dr in sections.Tables[0].Rows)
            {
                sb = MakeForecast(sb, sections, dr, string.Empty);
            }
            sb.Append("</table>");
            return sb.ToString();
        }

        private StringBuilder MakeForecast(StringBuilder sb, DataSet sections, DataRow dr, string sectiontitle)
        {
            string avquery = @"SELECT PROGRESS,WEIGHT FROM PROJECT_TIMING RIGHT OUTER JOIN PROJECT_SECTION_MEMBERS ON (PROJECT_TIMING.IDRIF=PROJECT_SECTION_MEMBERS.ID AND PROJECT_TIMING.RIFTYPE=1)
WHERE PROJECT_SECTION_MEMBERS.IDSECTIONRIF=" + dr["ID"].ToString();
            ProjectCalculator pc = new ProjectCalculator();
            string average = pc.GetSectionProgress(DatabaseConnection.CreateDataset(avquery).Tables[0]).ToString();

            object variationstart = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newstartdate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0}
order by newstartdate desc", dr["ID"].ToString()));

            DateTime DrawStart = (DateTime)dr["PLANNEDSTARTDATE"];
            if (variationstart != null && variationstart != System.DBNull.Value)
                DrawStart = Convert.ToDateTime(variationstart);


            object variationend = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND RIFTYPE=1)
inner join PROJECT_daylog on PROJECT_TIMING.idrif=PROJECT_daylog.id
inner join PROJECT_SECTION on (PROJECT_daylog.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0}
order by newplanneddate desc", dr["ID"].ToString()));
            object variationend2 = DatabaseConnection.SqlScalartoObj(String.Format(@"select top 1 newplanneddate from PROJECT_TIMING_VARIATION
inner join PROJECT_TIMING on (PROJECT_TIMING_VARIATION.idtiming=PROJECT_TIMING.ID AND RIFTYPE=0)
inner join PROJECT_SECTION on (PROJECT_TIMING.idrif = PROJECT_SECTION.id)
where PROJECT_SECTION.ID={0}
order by newplanneddate desc", dr["ID"].ToString()));

            DateTime DrawEnd = (DateTime)dr["PLANNEDENDDATE"];
            if (variationend != null && variationend != System.DBNull.Value)
                if ((variationend2 != null && variationend2 != System.DBNull.Value) && ((DateTime)variationend2) > ((DateTime)variationend))
                    DrawEnd = Convert.ToDateTime(variationend2);
                else
                    DrawEnd = Convert.ToDateTime(variationend);
            else
                if ((variationend2 != null && variationend2 != System.DBNull.Value))
                    DrawEnd = Convert.ToDateTime(variationend2);



            string bgcolor = "gold";
            if (DrawEnd < DateTime.UtcNow && int.Parse(average) < 100) bgcolor = "red";
            if (DrawEnd > DateTime.UtcNow && int.Parse(average) < 100) bgcolor = "#0df30d";

            sb.AppendFormat("<tr><td width=\"40%\" style=\"border-bottom:1px solid black;\"><b>{1}{0}</b></td>", dr["TITLE"].ToString(), sectiontitle);
            if (dr["PLANNEDSTARTDATE"] != System.DBNull.Value)
                sb.AppendFormat("<td width=\"10%\" style=\"border-bottom:1px solid black;\">{0}</td>", UC.LTZ.ToLocalTime(DrawStart).ToShortDateString());
            else
                sb.Append("<td width=\"10%\" style=\"border-bottom:1px solid black;\">Non definita</td>");
            if (dr["PLANNEDENDDATE"] != System.DBNull.Value)
                sb.AppendFormat("<td width=\"10%\" style=\"border-bottom:1px solid black;\">{0}</td>", UC.LTZ.ToLocalTime(DrawEnd).ToShortDateString());
            else
                sb.Append("<td width=\"10%\" style=\"border-bottom:1px solid black;\">Non definita</td>");
            sb.AppendFormat("<td width=\"20%\" style=\"border-bottom:1px solid black;\">{0}</td>", DatabaseConnection.SqlScalar("SELECT ISNULL(ACCOUNT.NAME,'')+' '+ISNULL(ACCOUNT.SURNAME,'') FROM ACCOUNT WHERE UID=" + dr["MEMBERID"].ToString()));
            sb.AppendFormat("<td style=\"width:20%\" style=\"border-bottom:1px solid black;\"><div style=\"border:1px solid #000000;width:100%\"><div style=\"border-right:1px solid #000000;background-color:{1};width:{0}%;color:#000000;text-align:center\">{0}%</div></div></td>", average, bgcolor);

            DateTime fd = GetForecastDate(UC.LTZ.ToLocalTime(DrawStart), UC.LTZ.ToLocalTime(DrawEnd), int.Parse(average));
            if (average == "100")
                fd = UC.LTZ.ToLocalTime(DrawEnd);
            string fdstyle = "style=\"color:{0};border-bottom:1px solid black;\"";
            if (fd <= UC.LTZ.ToLocalTime(DrawEnd))
                fdstyle = string.Format(fdstyle, "green");
            else
                fdstyle = string.Format(fdstyle, "red");
            sb.AppendFormat("<td width=\"10%\" {1}>{0}</td></tr>", fd.ToShortDateString(), fdstyle);

            FillForecastSubSection(ref sb, sections, dr["ID"].ToString(), dr["TITLE"].ToString() + " --> ");
            return sb;
        }

        void FillForecastSubSection(ref StringBuilder sb, DataSet sections, string parentsectionid, string parentsectionname)
        {
            DataRow[] dr1 = sections.Tables[1].Select(string.Format("[PARENT]='{0}'", parentsectionid));
            if (dr1.Length > 0)
            {
                foreach (DataRow dr in dr1)
                {
                    sb = MakeForecast(sb, sections, dr, parentsectionname);
                }
            }
        }

    }


}
