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
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class contactsearch : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nologin","<script>parent.location.href=parent.location.href;</script>");
			}
			else
			{
				if(Request["searchText"]!=null && Request["searchText"].Length>0)
				{
					FillquickRepeater();
				}else if(Request["searchFromLetter"]!=null && Request["searchFromLetter"].Length>0)
				{
					FillquickRepeaterFromLetter(DatabaseConnection.FilterInjection(Request["searchFromLetter"]));
				}
			}
		}

		private void FillquickRepeaterFromLetter(string letter)
		{
			if(letter=="*")
				letter = string.Empty;
			StringBuilder sbSearch = new StringBuilder();
			string queryGroup=string.Empty;
			DataTable dtContacts= new DataTable();
			if(!(Request["oppID"]!=null && Request["oppID"].Length>0))
			{
				sbSearch.Append("SELECT ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') AS QUICKNAME,");
				sbSearch.Append("BASE_COMPANIES.COMPANYNAME AS QUICKCOMPANY,");
				sbSearch.Append("BASE_CONTACTS.PHONE_1 AS QUICKPHONE,");
				sbSearch.Append("BASE_CONTACTS.EMAIL AS QUICKEMAIL, ");
				sbSearch.Append("'C'+CAST(BASE_CONTACTS.ID AS VARCHAR) AS QUICKID ");

                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                {
                    sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
                }
                else
                {
                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                    }
                    else
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                    }
                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                        sbSearch.Append(",NULL AS OPPSTATE");
                }

                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
                else
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

				sbSearch.Append(" FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_COMPANIES.ID=BASE_CONTACTS.COMPANYID ");
				if(Request["oppID"]!=null && Request["oppID"].Length>0)
					sbSearch.AppendFormat("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=0 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} ",int.Parse(Request["oppID"]));
                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                    sbSearch.AppendFormat(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=BASE_CONTACTS.ID) ");
				sbSearch.AppendFormat("WHERE (BASE_CONTACTS.NAME LIKE '{0}%' OR BASE_CONTACTS.SURNAME LIKE '{0}%')",letter);
                queryGroup = GroupsSecure("BASE_CONTACTS.GROUPS", UC);
				if (queryGroup.Length > 0)
				{
					sbSearch.AppendFormat(" AND ({0})", queryGroup);
				}
				dtContacts = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];
			}

			sbSearch.Length=0;
			sbSearch.Append("SELECT ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'') AS QUICKNAME,");
			sbSearch.Append("CRM_LEADS.COMPANYNAME AS QUICKCOMPANY,");
			sbSearch.Append("CRM_LEADS.PHONE AS QUICKPHONE,");
			sbSearch.Append("CRM_LEADS.EMAIL AS QUICKEMAIL, ");
			sbSearch.Append("'L'+CAST(CRM_LEADS.ID AS VARCHAR) AS QUICKID");

            if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
            {
                sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
            }
            else
            {
                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                {
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE", Request["oppID"].ToString());
                    sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                    sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                }
                else
                {
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                    sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                    sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                }
                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    sbSearch.AppendFormat(",(SELECT top 1 CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=CRM_LEADS.ID AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID={0} AND CRM_CROSSOPPORTUNITY.TYPE=1 AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=1) AS OPPSTATE ", Request["oppID"]);
            }

            if (Request["oppID"] != null && Request["oppID"].Length > 0)
                sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
            else
                sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

            sbSearch.Append(" FROM CRM_LEADS ");
			if(Request["oppID"]!=null && Request["oppID"].Length>0)
				sbSearch.AppendFormat("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_LEADS.ID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=1 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} ",int.Parse(Request["oppID"]));
            if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                sbSearch.AppendFormat(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=CRM_LEADS.ID) ");
			sbSearch.AppendFormat("WHERE (CRM_LEADS.NAME LIKE '{0}%' OR CRM_LEADS.SURNAME LIKE '{0}%') ",letter);
			queryGroup = GroupsSecure("CRM_LEADS.GROUPS",UC);
			if (queryGroup.Length > 0)
			{
				sbSearch.AppendFormat(" AND ({0})", queryGroup);
			}
			DataTable dtLead = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];

			sbSearch.Length=0;
			sbSearch.Append("SELECT BASE_COMPANIES.COMPANYNAME AS QUICKNAME,");
			sbSearch.Append("BASE_COMPANIES.COMPANYNAME AS QUICKCOMPANY,");
			sbSearch.Append("BASE_COMPANIES.PHONE AS QUICKPHONE,");
			sbSearch.Append("BASE_COMPANIES.EMAIL AS QUICKEMAIL, ");
			sbSearch.Append("'A'+CAST(BASE_COMPANIES.ID AS VARCHAR) AS QUICKID ");

            if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
            {
                sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
            }
            else
            {
                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                {
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={1} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE", Request["oppID"].ToString());
                    sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={1} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                    sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={1} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                }
                else
                {
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                    sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                    sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                }

                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    sbSearch.AppendFormat(",(SELECT top 1 CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=BASE_COMPANIES.ID AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID={0} AND CRM_CROSSOPPORTUNITY.TYPE=1 AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=0) AS OPPSTATE ", Request["oppID"]);
            }

            if (Request["oppID"] != null && Request["oppID"].Length > 0)
                sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
            else
                sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

			sbSearch.Append(" FROM BASE_COMPANIES ");
			if(Request["oppID"]!=null && Request["oppID"].Length>0)
				sbSearch.AppendFormat("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=0 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} ",int.Parse(Request["oppID"]));
            if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                sbSearch.AppendFormat(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=BASE_COMPANIES.ID) ");
			sbSearch.AppendFormat("WHERE (BASE_COMPANIES.EMAIL LIKE '{0}%' ",letter);
				sbSearch.AppendFormat("OR BASE_COMPANIES.COMPANYNAME LIKE '{0}%')",Request["searchText"]);
            queryGroup = GroupsSecure("BASE_COMPANIES.GROUPS", UC);
            if (queryGroup.Length > 0)
            {
                sbSearch.AppendFormat(" AND ({0})", queryGroup);
            }
			DataTable dtCompany = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];

			DataTable dtTemp = new DataTable();
			DataTable dtComplete = new DataTable();

			if(!(Request["oppID"]!=null && Request["oppID"].Length>0))
			{
				dtTemp = DataManipulation.Union(dtContacts,dtLead);
				dtComplete = DataManipulation.Union(dtTemp,dtCompany);
			}
			else
			{
				dtComplete = DataManipulation.Union(dtLead,dtCompany);
			}

			dtComplete.DefaultView.Sort="quickName Asc";
			if(dtComplete.Rows.Count>0)
			{
				FillJsRepeater(dtComplete);
		}
		else
		LitquickRepeaterInfo.Text =Root.rm.GetString("Quicktxt12");

		}

		private void FillquickRepeater()
		{
			StringBuilder sbSearch = new StringBuilder();
			DataTable dtContacts = new DataTable();
			DataTable dtLead = new DataTable();
			DataTable dtCompany = new DataTable();

			if(!(Request["oppID"]!=null && Request["oppID"].Length>0))
			{
				if(Request["searchtype"]=="0"||Request["searchtype"]=="1")
				{
					sbSearch.Append("SELECT ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') AS QUICKNAME,");
					sbSearch.Append("BASE_COMPANIES.COMPANYNAME AS QUICKCOMPANY,");
					sbSearch.Append("BASE_CONTACTS.PHONE_1 AS QUICKPHONE,");
					sbSearch.Append("BASE_CONTACTS.EMAIL AS QUICKEMAIL, ");
					sbSearch.Append("'C'+CAST(BASE_CONTACTS.ID AS VARCHAR) AS QUICKID ");

                    if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                    {
                        sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
                    }
                    else
                    {

                        if (Request["oppID"] != null && Request["oppID"].Length > 0)
                        {
                            sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE",  Request["oppID"].ToString());
                            sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                            sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                        }
                        else
                        {
                            sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                            sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                            sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                        }
                        if (Request["oppID"] != null && Request["oppID"].Length > 0)
                            sbSearch.Append(",NULL AS OPPSTATE");
                    }

                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND REFERRERID=BASE_CONTACTS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
                    else
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND REFERRERID=BASE_CONTACTS.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

					sbSearch.Append(" FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_COMPANIES.ID=BASE_CONTACTS.COMPANYID ");
                    if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                        sbSearch.AppendFormat(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=BASE_CONTACTS.ID) ");
					sbSearch.AppendFormat("WHERE (BASE_CONTACTS.NAME LIKE '%{0}%' OR BASE_CONTACTS.SURNAME LIKE '%{0}%' ",Request["searchText"]);
					sbSearch.AppendFormat("OR BASE_CONTACTS.PHONE_1 LIKE '%{0}%' OR BASE_CONTACTS.EMAIL LIKE '%{0}%' ",Request["searchText"]);
                    string queryGroup = GroupsSecure("BASE_CONTACTS.GROUPS", UC);
					if (queryGroup.Length > 0)
					{
						sbSearch.AppendFormat("AND ({0}) ", queryGroup);
					}
					sbSearch.AppendFormat("OR BASE_COMPANIES.COMPANYNAME LIKE '%{0}%')",Request["searchText"]);
					dtContacts = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];
				}
			}

			if(Request["searchtype"]=="0"||Request["searchtype"]=="2")
			{
				sbSearch.Length=0;
				sbSearch.Append("SELECT ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'') AS QUICKNAME,");
				sbSearch.Append("CRM_LEADS.COMPANYNAME AS QUICKCOMPANY,");
				sbSearch.Append("CRM_LEADS.PHONE AS QUICKPHONE,");
				sbSearch.Append("CRM_LEADS.EMAIL AS QUICKEMAIL, ");
				sbSearch.Append("'L'+CAST(CRM_LEADS.ID AS VARCHAR) AS QUICKID ");

                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                {
                    sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
                }
                else
                {
                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                    }
                    else
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                    }

                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                        sbSearch.AppendFormat(",(SELECT top 1 CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=CRM_LEADS.ID AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID={0} AND CRM_CROSSOPPORTUNITY.TYPE=1 AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=1) AS OPPSTATE ", Request["oppID"]);
                }

                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND LEADID=CRM_LEADS.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
                else
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND LEADID=CRM_LEADS.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

				sbSearch.Append(" FROM CRM_LEADS ");
				if(Request["oppID"]!=null && Request["oppID"].Length>0)
					sbSearch.AppendFormat("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITYCONTACT.ContactID = CRM_LEADS.ID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=1 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} ",int.Parse(Request["oppID"]));
                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                    sbSearch.AppendFormat(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=CRM_LEADS.ID) ");
				sbSearch.AppendFormat("WHERE (CRM_LEADS.NAME LIKE '%{0}%' OR CRM_LEADS.SURNAME LIKE '%{0}%' ",Request["searchText"]);
				sbSearch.AppendFormat("OR CRM_LEADS.PHONE LIKE '%{0}%' OR CRM_LEADS.EMAIL LIKE '%{0}%' ",Request["searchText"]);
				string queryGroup = GroupsSecure("CRM_LEADS.GROUPS",UC);
				if (queryGroup.Length > 0)
				{
					sbSearch.AppendFormat("AND ({0}) ", queryGroup);
				}
				sbSearch.AppendFormat("OR CRM_LEADS.COMPANYNAME LIKE '%{0}%')",Request["searchText"]);
				dtLead = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];
			}

			if(Request["searchtype"]=="0"||Request["searchtype"]=="3")
			{
				sbSearch.Length=0;
				sbSearch.Append("SELECT BASE_COMPANIES.COMPANYNAME AS QUICKNAME,");
				sbSearch.Append("BASE_COMPANIES.COMPANYNAME AS QUICKCOMPANY,");
				sbSearch.Append("BASE_COMPANIES.PHONE AS QUICKPHONE,");
				sbSearch.Append("BASE_COMPANIES.EMAIL AS QUICKEMAIL, ");
				sbSearch.Append("'A'+CAST(BASE_COMPANIES.ID AS VARCHAR) AS QUICKID ");

                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                {
                    sbSearch.AppendFormat(",CRM_WORKACTIVITYSEARCH_VIEW.ACTIVITYDATE,CRM_WORKACTIVITYSEARCH_VIEW.RECALLENDHOUR,CRM_WORKACTIVITYSEARCH_VIEW.ID AS ACTID");
                }
                else
                {
                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR", Request["oppID"].ToString());
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS ACTID", Request["oppID"].ToString());
                    }
                    else
                    {
                        sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS ACTIVITYDATE");
                        sbSearch.AppendFormat(",(SELECT top 1 RECALLENDHOUR FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS RECALLENDHOUR");
                        sbSearch.AppendFormat(",(SELECT top 1 ID FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=0 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS ACTID");
                    }

                    if (Request["oppID"] != null && Request["oppID"].Length > 0)
                        sbSearch.AppendFormat(",(SELECT top 1 CRM_OPPORTUNITYTABLETYPE.K_ID FROM CRM_CROSSOPPORTUNITY LEFT OUTER JOIN CRM_OPPORTUNITYTABLETYPE ON CRM_CROSSOPPORTUNITY.TABLETYPEID=CRM_OPPORTUNITYTABLETYPE.K_ID WHERE CRM_CROSSOPPORTUNITY.CONTACTID=BASE_COMPANIES.ID AND CRM_CROSSOPPORTUNITY.OPPORTUNITYID={0} AND CRM_CROSSOPPORTUNITY.TYPE=1 AND CRM_CROSSOPPORTUNITY.CONTACTTYPE=0) AS OPPSTATE ", Request["oppID"]);
                }

                if (Request["oppID"] != null && Request["oppID"].Length > 0)
                    sbSearch.AppendFormat(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND COMPANYID=BASE_COMPANIES.ID AND OPPORTUNITYID={0} ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY", Request["oppID"].ToString());
                else
                    sbSearch.Append(",(SELECT top 1 ACTIVITYDATE FROM CRM_WORKACTIVITYSEARCH_VIEW WHERE TODO=1 AND COMPANYID=BASE_COMPANIES.ID ORDER BY ACTIVITYDATE ASC) AS EXECUTEDACTIVITY");

				sbSearch.Append(" FROM BASE_COMPANIES ");
				if(Request["oppID"]!=null && Request["oppID"].Length>0)
					sbSearch.AppendFormat("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_OPPORTUNITYCONTACT.CONTACTID = BASE_COMPANIES.ID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE=0 AND CRM_OPPORTUNITYCONTACT.OPPORTUNITYID={0} ",int.Parse(Request["oppID"]));
                if (Request["checkAct"] != null && Request["checkAct"].Length > 0 && Request["checkAct"] == "1")
                    sbSearch.Append(" LEFT OUTER JOIN CRM_WORKACTIVITYSEARCH_VIEW ON (CRM_WORKACTIVITYSEARCH_VIEW.TODO=0 AND CRM_WORKACTIVITYSEARCH_VIEW.REFERRERID=BASE_COMPANIES.ID) ");
                sbSearch.AppendFormat("WHERE (BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.EMAIL LIKE '%{0}%' ",Request["searchText"]);
				sbSearch.AppendFormat("OR BASE_COMPANIES.COMPANYNAME LIKE '%{0}%')",Request["searchText"]);
                string queryGroup = GroupsSecure("BASE_COMPANIES.GROUPS", UC);
                if (queryGroup.Length > 0)
                {
                    sbSearch.AppendFormat("AND ({0}) ", queryGroup);
                }
				dtCompany = DatabaseConnection.CreateDataset(sbSearch.ToString()).Tables[0];
			}

			DataTable dtTemp = new DataTable();
			DataTable dtComplete = new DataTable();
			switch(Request["searchtype"])
			{
				case "0":
					if(!(Request["oppID"]!=null && Request["oppID"].Length>0))
					{
						dtTemp = DataManipulation.Union(dtContacts,dtLead);
						dtComplete = DataManipulation.Union(dtTemp,dtCompany);
					}else
					{
						dtComplete = DataManipulation.Union(dtLead,dtCompany);
					}
					break;
				case "1":
					dtComplete = dtContacts.Copy();
					break;
				case "2":
					dtComplete = dtLead.Copy();
					break;
				case "3":
					dtComplete = dtCompany.Copy();
					break;
			}

			if(dtComplete.Rows.Count>0)
			{
				dtComplete.DefaultView.Sort="quickName Asc";
				FillJsRepeater(dtComplete);
			}
			else
				LitquickRepeaterInfo.Text =Root.rm.GetString("Quicktxt12");
		}

		private void FillJsRepeater(DataTable dtComplete)
		{
			StringBuilder sb = new StringBuilder();
			int i=0;
			sb.Append("<script>var a=new Array;");
			foreach(DataRow dr in dtComplete.Rows)
			{
				string oppstate="0";
				if(Request["oppID"]!=null && Request["oppID"].Length>0 && dr["QuickID"].ToString().Substring(0,1)!="C")
				{
					if( dr["OPPSTATE"]!=System.DBNull.Value)
                           oppstate = dr["OPPSTATE"].ToString();
				}

                string recall = String.Empty;
                if (dr["ACTIVITYDATE"] != System.DBNull.Value)
                {
                    DateTime actdate = Convert.ToDateTime(dr["ACTIVITYDATE"]);
                    recall = ((actdate.Year * 366 + actdate.DayOfYear) - (DateTime.UtcNow.Year * 366 + DateTime.UtcNow.DayOfYear)).ToString();
                    if (recall == "0")
                    {
                        DateTime start = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ACTIVITYDATE"]));
                        if (StaticFunctions.IsDate(dr["RECALLENDHOUR"].ToString()))
                        {
                            DateTime end = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["RECALLENDHOUR"]));
                            recall = "T" + ToDayQuarter(start).ToString().PadLeft(2, '0') + "-" + ToDayQuarter(end).ToString().PadLeft(2, '0');
                        }
                        else
                        {
                            recall = "T" + ToDayQuarter(start).ToString() + "-76";
                        }
                    }
                }
                else
                {
                    if (dr["EXECUTEDACTIVITY"] != System.DBNull.Value)
                    {
                        recall = "E";
                    }

                }

                string actid = string.Empty;
                if (dr["ACTID"] != System.DBNull.Value)
                    actid = dr["ACTID"].ToString();

				sb.AppendFormat("a[{5}]=new Array('{0}','{1}','{2}','{3}','{4}','{6}','{7}','{8}');\n",dr["QUICKID"],ParseJSString(dr["quickname"].ToString()),ParseJSString(dr["quickcompany"].ToString()),ParseJSString(dr["quickPhone"].ToString()),ParseJSString(dr["quickEmail"].ToString()),i++,oppstate,recall,actid);
				if(i>500)
				{
					sb.AppendFormat("a[{1}]=new Array('{0}');\n",string.Format(Root.rm.GetString("top100"),"500"),i++);
					break;
				}
			}
			sb.Append("RenderTable(a);</script>");
			this.QuickRepeater.Text= sb.ToString();
		}

        private int ToDayQuarter(DateTime date)
        {
            double minute = new TimeSpan(date.Ticks-new DateTime(date.Year,date.Month,date.Day).Ticks).TotalMinutes / 15;
            return Convert.ToInt32(Math.Round(minute, 0));
        }

        private DateTime FromDayQuarter(int quarters)
        {
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMinutes(quarters*4);
        }


		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}
		#endregion

		}
}
