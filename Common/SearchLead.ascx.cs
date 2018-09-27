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
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class SearchLead : UserControl
	{

		public DataTable SearchResult;
		UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

		protected void Page_Load(object sender, EventArgs e)
		{
			if(!Page.IsPostBack)
			{
				PrepareAvanzate();
			}
		}

		bool fromOpportunity = false;
		public bool FromOpportunity
		{
			get { return fromOpportunity; }
			set { fromOpportunity = value; }
		}

		long opportunityID = 0;
		public long OpportunityID
		{
			get { return opportunityID; }
			set { opportunityID = value; }
		}

		private void PrepareAvanzate()
		{
			SAdvancedLead_Category.DataTextField = "Description";
			SAdvancedLead_Category.DataValueField = "id";
			SAdvancedLead_Category.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT ID,DESCRIPTION FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID={0}))",UC.UserId));
			SAdvancedLead_Category.DataBind();
			SAdvancedLead_Category.Items.Insert(0, Root.rm.GetString("CRMcontxt53"));
			SAdvancedLead_Category.Items[0].Value = "0";

			FillAdvancedLead();

			SAdvancedLead_Status.DataSource = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM CRM_LEADDESCRIPTION WHERE LANG='" + UC.Culture.Substring(0, 2) + "' AND TYPE=1 ORDER BY K_ID");
			SAdvancedLead_Status.DataTextField = "Description";
			SAdvancedLead_Status.DataValueField = "K_ID";
			SAdvancedLead_Status.DataBind();
			SAdvancedLead_Status.Items.Insert(0, Root.rm.GetString("Ledtxt21"));
			SAdvancedLead_Status.Items[0].Value = "0";

			SAdvancedLead_Industry.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='{0}' ORDER BY DESCRIPTION",UC.Culture.Substring(0, 2)));
			SAdvancedLead_Industry.DataTextField = "Description";
			SAdvancedLead_Industry.DataValueField = "K_ID";
			SAdvancedLead_Industry.DataBind();
			SAdvancedLead_Industry.Items.Insert(0, Root.rm.GetString("Ledtxt21"));
			SAdvancedLead_Industry.Items[0].Value = "0";
		}

		public void FillAdvancedLead()
		{
			SAdvancedLead_Opportunity.DataTextField = "Title";
			SAdvancedLead_Opportunity.DataValueField = "id";
			SAdvancedLead_Opportunity.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE (CREATEDBYID={0} AND (ADMINACCOUNT LIKE '%|{0}|%' OR BASICACCOUNT LIKE '%|{0}|%'))",UC.UserId));
			SAdvancedLead_Opportunity.DataBind();
			SAdvancedLead_Opportunity.Items.Insert(0, Root.rm.GetString("CRMopptxt83"));
			SAdvancedLead_Opportunity.Items[0].Value = "0";
			if(FromOpportunity)
			{
				SAdvancedLead_Opportunity.SelectedIndex=-1;
				foreach(ListItem li in SAdvancedLead_Opportunity.Items)
				{
					if(li.Value==OpportunityID.ToString())
					{
						li.Selected=true;
						break;
					}
				}
				SAdvancedLead_Opportunity.Enabled=false;
				tropportunity.Visible=false;
				trstatus.Visible=false;
			}
		}

		public DataTable GetLeadTable()
		{
			return DatabaseConnection.CreateDataset(this.GetLeadQuery()).Tables[0];
		}

		public string GetLeadQuery()
		{

			string formLead_Address = string.Empty;
			string formLead_City = string.Empty;
			string formLead_State = string.Empty;
			string formLead_Nation = string.Empty;
			string formLead_Zip = string.Empty;
			string formLead_Email = string.Empty;
			string formSLead_Opportunity = string.Empty;
			string formSLead_Category = string.Empty;

			string formLead_Name = string.Empty;
			string formLead_Surname = string.Empty;
			string formLead_CompanyName = string.Empty;
			string formLead_BusinessRole = string.Empty;
			string formLead_Phone = string.Empty;
			string formLead_MobilePhone = string.Empty;
			string formLead_Fax = string.Empty;
			string formLead_WebSite = string.Empty;
			string formSLead_Status = string.Empty;
			string formSLead_Industry = string.Empty;


			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("AdvancedLead_Name") > -1)
				{
					TextParam(strKey, ref formLead_Name);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Surname") > -1)
				{
					TextParam(strKey, ref formLead_Surname);
				}
				else
					if (strKey.IndexOf("Advanced_CompanyName") > -1)
				{
					TextParam(strKey, ref formLead_CompanyName);
				}
				else
					if (strKey.IndexOf("AdvancedLead_BusinessRole") > -1)
				{
					TextParam(strKey, ref formLead_BusinessRole);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Phone") > -1)
				{
					TextParam(strKey, ref formLead_Phone);
				}
				if (strKey.IndexOf("AdvancedLead_MobilePhone") > -1)
				{
					TextParam(strKey, ref formLead_MobilePhone);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Fax") > -1)
				{
					TextParam(strKey, ref formLead_Fax);
				}
				else
					if (strKey.IndexOf("AdvancedLead_WebSite") > -1)
				{
					TextParam(strKey, ref formLead_WebSite);
				}
				else
					if (strKey.IndexOf("Advanced_CompanyName") > -1)
				{
					TextParam(strKey, ref formLead_CompanyName);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Address") > -1)
				{
					TextParam(strKey, ref formLead_Address);
				}
				else
					if (strKey.IndexOf("AdvancedLead_City") > -1)
				{
					TextParam(strKey, ref formLead_City);
				}
				else
					if (strKey.IndexOf("AdvancedLead_State") > -1)
				{
					TextParam(strKey, ref formLead_State);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Nation") > -1)
				{
					TextParam(strKey, ref formLead_Nation);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Zip") > -1)
				{
					TextParam(strKey, ref formLead_Zip);
				}
				else
					if (strKey.IndexOf("AdvancedLead_Email") > -1)
				{
					TextParam(strKey, ref formLead_Email);
				}
				else
					if (strKey.IndexOf("SAdvancedLead_Category") > -1)
				{
					SelectParam(strKey, ref formSLead_Category);
				}
				else
					if (strKey.IndexOf("SAdvancedLead_Opportunity") > -1)
				{
					SelectParam(strKey, ref formSLead_Opportunity);
				}
				else
					if (strKey.IndexOf("SAdvancedLead_Status") > -1)
				{
					SelectParam(strKey, ref formSLead_Status);
				}
				else
					if (strKey.IndexOf("SAdvancedLead_Industry") > -1)
				{
					SelectParam(strKey, ref formSLead_Industry);
				}
			}




			string AdvancedLead_Address = string.Empty;
			string AdvancedLead_City = string.Empty;
			string AdvancedLead_State = string.Empty;
			string AdvancedLead_Zip = string.Empty;
			string AdvancedLead_Email = string.Empty;
			string SAdvancedLead_Opportunity = string.Empty;
			string SAdvancedLead_Category = string.Empty;

			string AdvancedLead_Name = string.Empty;
			string AdvancedLead_Surname = string.Empty;
			string AdvancedLead_CompanyName = string.Empty;
			string AdvancedLead_BusinessRole = string.Empty;
			string AdvancedLead_Phone = string.Empty;
			string AdvancedLead_MobilePhone = string.Empty;
			string AdvancedLead_Fax = string.Empty;
			string AdvancedLead_WebSite = string.Empty;
			string SAdvancedLead_Status = string.Empty;
			string SAdvancedLead_Industry = string.Empty;

			StringBuilder sqlString = new StringBuilder();
			sqlString.AppendFormat("SELECT TOP {0} CRM_LEADS.*, CRM_CROSSLEAD.SALESPERSON,",DatabaseConnection.MaxResult);


			sqlString.Remove(sqlString.Length-1,1);
			sqlString.Append(" FROM CRM_LEADS LEFT OUTER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID = CRM_CROSSLEAD.LEADID ");
			if(formSLead_Opportunity.Length>0)
			{
				sqlString.Append("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_LEADS.ID = CRM_OPPORTUNITYCONTACT.CONTACTID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 1 ");
			}
			sqlString.Append("WHERE (CRM_LEADS.LIMBO=0) AND (");

			string[] strKeys = formSLead_Opportunity.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedLead_Opportunity += String.Format("(CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = '{0}') OR ", strKey);
			}

			strKeys = formLead_Name.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Name += String.Format("(CRM_LEADS.NAME LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_Surname.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Surname += String.Format("(CRM_LEADS.SURNAME LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_CompanyName.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_CompanyName += String.Format("(CRM_LEADS.COMPANYNAME LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_BusinessRole.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_BusinessRole += String.Format("(CRM_LEADS.BUSINESSROLE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_Address.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Address += String.Format("(CRM_LEADS.ADDRESS LIKE '%{0}%') OR ", strKey);
			}
			strKeys = formLead_City.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_City += String.Format("(CRM_LEADS.CITY LIKE '%{0}%' ) OR ", strKey);
			}
			strKeys = formLead_State.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_State += String.Format("(CRM_LEADS.PROVINCE LIKE '%{0}%') OR ", strKey);
			}
			strKeys = formLead_Nation.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_State += String.Format("(CRM_LEADS.STATE LIKE '%{0}%') OR ", strKey);
			}


			strKeys = formLead_Zip.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Zip += String.Format("(CRM_LEADS.ZIPCODE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_MobilePhone.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_MobilePhone += String.Format("(CRM_LEADS.MOBILEPHONE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_Phone.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Phone += String.Format("(CRM_LEADS.PHONE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = formLead_Fax.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Fax += String.Format("(CRM_LEADS.FAX LIKE '%{0}%') OR ", strKey);
			}


			strKeys = formLead_Email.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_Email += String.Format("(CRM_LEADS.EMAIL LIKE '%{0}%') OR ", strKey);
			}


			strKeys = formLead_WebSite.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					AdvancedLead_WebSite += String.Format("(CRM_LEADS.WEBSITE LIKE '%{0}%') OR ", strKey);
			}


			strKeys = formSLead_Category.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedLead_Category += String.Format("(CRM_LEADS.CATEGORIES LIKE '%|{0}|%') OR ", strKey);
			}
			strKeys = formSLead_Status.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedLead_Status += String.Format("(CRM_CROSSLEAD.STATUS = '{0}') OR ", strKey);
			}
			strKeys = formSLead_Industry.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedLead_Industry += String.Format("(CRM_CROSSLEAD.INDUSTRY = '{0}') OR ", strKey);
			}

			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;


			if (AdvancedLead_CompanyName.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_CompanyName.Substring(0, AdvancedLead_CompanyName.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (AdvancedLead_Name.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Name.Substring(0, AdvancedLead_Name.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_Surname.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Surname.Substring(0, AdvancedLead_Surname.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_BusinessRole.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_BusinessRole.Substring(0, AdvancedLead_BusinessRole.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_Phone.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Phone.Substring(0, AdvancedLead_Phone.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_MobilePhone.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_MobilePhone.Substring(0, AdvancedLead_MobilePhone.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_Fax.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Fax.Substring(0, AdvancedLead_Fax.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_WebSite.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_WebSite.Substring(0, AdvancedLead_WebSite.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_Address.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Address.Substring(0, AdvancedLead_Address.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_City.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_City.Substring(0, AdvancedLead_City.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_State.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_State.Substring(0, AdvancedLead_State.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (AdvancedLead_Zip.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Zip.Substring(0, AdvancedLead_Zip.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (AdvancedLead_Email.Length > 0)
			{
				sqlAv += sAnd + AdvancedLead_Email.Substring(0, AdvancedLead_Email.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (SAdvancedLead_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvancedLead_Category.Substring(0, SAdvancedLead_Category.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvancedLead_Opportunity.Length > 0)
			{
				sqlAv += sAnd + SAdvancedLead_Opportunity.Substring(0, SAdvancedLead_Opportunity.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvancedLead_Status.Length > 0)
			{
				sqlAv += sAnd + SAdvancedLead_Status.Substring(0, SAdvancedLead_Status.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvancedLead_Industry.Length > 0)
			{
				sqlAv += sAnd + SAdvancedLead_Industry.Substring(0, SAdvancedLead_Industry.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (AdvancedLead_OwnerID.Text.Length > 0)
			{

				sqlAv += sAnd + String.Format("CRM_LEADS.OWNERID = '{0}')", AdvancedLead_OwnerID.Text);
				sAnd = " AND (";
			}

			if(sqlAv.EndsWith("AND ("))
				sqlAv=sqlAv.Substring(0,sqlAv.Length-5);

			G g=new G();
			if(UC.Zones.Length>0)
				sqlString.AppendFormat(" AND ({0})", g.ZoneSecure("CRM_LEADS.COMMERCIALZONE",UC));
			return sqlAv + " ORDER BY CRM_LEADS.SURNAME";
		}

		private void SelectParam(string strKey, ref string paramSelect)
		{
			if (Request[strKey].Length > 0 && Request[strKey] != "0" && paramSelect.IndexOf(Request[strKey]) < 0)
				paramSelect += Request[strKey] + "|";
		}

		private void TextParam(string strKey, ref string paramString)
		{
			if (Request[strKey].Length > 0 && paramString.IndexOf(Request[strKey]) < 0)
				paramString += DatabaseConnection.FilterInjection(Request[strKey]) + "|";
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
