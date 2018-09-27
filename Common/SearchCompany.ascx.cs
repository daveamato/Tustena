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
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class SearchCompany : UserControl
	{


		public DataTable SearchResult;

		private UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

		private bool onlyName = false;
		private bool primaryAddress = false;
		private bool secondAddress = false;
		private bool thirdAddress = false;
		private bool mlMailField = false;

		public bool OnlyName
		{
			get { return onlyName; }
			set { onlyName = value; }
		}

		public bool PrimaryAddress
		{
			get { return primaryAddress; }
			set { primaryAddress = value; }
		}

		public bool SecondAddress
		{
			get { return secondAddress; }
			set { secondAddress = value; }
		}

		public bool ThirdAddress
		{
			get { return thirdAddress; }
			set { thirdAddress = value; }
		}

		public bool MLMailField
		{
			get { return mlMailField; }
			set { mlMailField = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
                PrepareGeoSearch();
				PrepareAdvanced();
			}
		}

        private void PrepareGeoSearch()
        {
            geoCountry.Items.Add(new ListItem("IT","IT"));

        }

		private void PrepareAdvanced()
		{
			Fill_Sectors(SAdvanced_CompanyType);
			Fill_ContactType(SAdvanced_ContactType);
			Fill_Evaluation(SAdvanced_Estimate);

			SAdvanced_Category.DataTextField = "Description";
			SAdvanced_Category.DataValueField = "id";
			SAdvanced_Category.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
			SAdvanced_Category.DataBind();
			SAdvanced_Category.Items.Insert(0, Root.rm.GetString("CRMcontxt53"));
			SAdvanced_Category.Items[0].Value = "0";

			SAdvanced_Opportunity.DataTextField = "Title";
			SAdvanced_Opportunity.DataValueField = "id";
			SAdvanced_Opportunity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE (CREATEDBYID=" + UC.UserId + " AND (ADMINACCOUNT LIKE '%|" + UC.UserId + "|%' OR BASICACCOUNT LIKE '%|" + UC.UserId + "|%'))");
			SAdvanced_Opportunity.DataBind();
			SAdvanced_Opportunity.Items.Insert(0, Root.rm.GetString("CRMopptxt83"));
			SAdvanced_Opportunity.Items[0].Value = "0";
		}

		private void Fill_Sectors(DropDownList st)
		{
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY DESCRIPTION");
			st.DataSource = ds;
			st.DataTextField = "Description";
			st.DataValueField = "K_ID";
			st.DataBind();
			st.Items.Insert(0, Root.rm.GetString("CRMcontxt13"));
			st.Items[0].Value = "0";
		}

		private void Fill_ContactType(DropDownList ct)
		{
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT K_ID,CONTACTTYPE FROM CONTACTTYPE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY CONTACTTYPE");
			ct.DataSource = ds;
			ct.DataTextField = "ContactType";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0, Root.rm.GetString("CRMcontxt14"));
			ct.Items[0].Value = "0";
		}

		private void Fill_Evaluation(DropDownList ct)
		{
DataSet ds;
 ds = DatabaseConnection.CreateDataset("SELECT K_ID,ESTIMATE FROM CONTACTESTIMATE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY FIELDORDER");
			ct.DataSource = ds;
			ct.DataTextField = "Estimate";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0, Root.rm.GetString("CRMcontxt15"));
			ct.Items[0].Value = "0";
		}

		public DataTable GetCompanyTable()
		{
			return DatabaseConnection.CreateDataset(this.GetCompanyQuery()).Tables[0];
		}

		public string GetCompanyQuery()
		{
			string formAdvanced_CompanyName = String.Empty;
			string formAdvanced_Address = String.Empty;
			string formAdvanced_City = String.Empty;
			string formAdvanced_State = String.Empty;
			string formAdvanced_Nation = String.Empty;
			string formAdvanced_Zip = String.Empty;
			string formAdvanced_Phone = String.Empty;
			string formAdvanced_Fax = String.Empty;
			string formAdvanced_Email = String.Empty;
			string formAdvanced_Site = String.Empty;
			string formAdvanced_Code = String.Empty;
			string formSAdvanced_CompanyType = String.Empty;
			string formSAdvanced_ContactType = String.Empty;
			string formAdvanced_Billed = String.Empty;
			string formAdvanced_Employees = String.Empty;
			string formSAdvanced_Estimate = String.Empty;
			string formSAdvanced_Category = String.Empty;
			string formSAdvanced_Opportunity = String.Empty;

			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("Advanced_CompanyName") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_CompanyName.IndexOf(Request[strKey]) < 0)
						formAdvanced_CompanyName += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Address") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Address.IndexOf(Request[strKey]) < 0)
						formAdvanced_Address += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_City") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_City.IndexOf(Request[strKey]) < 0)
						formAdvanced_City += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_State") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_State.IndexOf(Request[strKey]) < 0)
						formAdvanced_State += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Nation") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Nation.IndexOf(Request[strKey]) < 0)
						formAdvanced_Nation += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Zip") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Zip.IndexOf(Request[strKey]) < 0)
						formAdvanced_Zip += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Phone") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Phone.IndexOf(Request[strKey]) < 0)
						formAdvanced_Phone += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Fax") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Fax.IndexOf(Request[strKey]) < 0)
						formAdvanced_Fax += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Email") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Email.IndexOf(Request[strKey]) < 0)
						formAdvanced_Email += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Site") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Site.IndexOf(Request[strKey]) < 0)
						formAdvanced_Site += Request[strKey] + "|";
				}
				if (strKey.IndexOf("Advanced_Code") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Code.IndexOf(Request[strKey]) < 0)
						formAdvanced_Code += Request[strKey] + "|";
				}
				if (strKey.IndexOf("SAdvanced_CompanyType") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && formSAdvanced_CompanyType.IndexOf(Request[strKey]) < 0)
						formSAdvanced_CompanyType += Request[strKey] + "|";
				}
				if (strKey.IndexOf("SAdvanced_ContactType") > -1 && formSAdvanced_ContactType.IndexOf(Request[strKey]) < 0)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						formSAdvanced_ContactType += Request[strKey] + "|";
				}

				if (strKey.IndexOf("RFormAdvanced_Billed") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Billed.IndexOf(Request[strKey]) < 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 16) suffix = strKey.Substring(16, strKey.Length - 16);
						switch (Request[strKey])
						{
							case "0":
								formAdvanced_Billed += "a" + Request["Advanced_Billed" + suffix] + "|";
								break;
							case "1":
								formAdvanced_Billed += "b" + Request["Advanced_Billed" + suffix] + "|";
								break;
							case "2":
								formAdvanced_Billed += "c" + Request["Advanced_Billed" + suffix] + "|";
								break;
							case "3":
								formAdvanced_Billed += "d" + Request["Advanced_Billed" + suffix] + "|";
								break;
							case "4":
								formAdvanced_Billed += "e" + Request["Advanced_Billed" + suffix] + "|";
								break;
							case "5":
								formAdvanced_Billed += "f" + Request["Advanced_Billed" + suffix] + "|";
								break;
						}
					}
				}

				if (strKey.IndexOf("RAdvanced_Employees") > -1)
				{
					if (Request[strKey].Length > 0 && formAdvanced_Employees.IndexOf(Request[strKey]) < 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 19) suffix = strKey.Substring(19, strKey.Length - 19);
						switch (suffix)
						{
							case "0":
								formAdvanced_Employees += "a" + Request["Advanced_Employees" + suffix] + "|";
								break;
							case "1":
								formAdvanced_Employees += "b" + Request["Advanced_Employees" + suffix] + "|";
								break;
							case "2":
								formAdvanced_Employees += "c" + Request["Advanced_Employees" + suffix] + "|";
								break;
							case "3":
								formAdvanced_Employees += "d" + Request["Advanced_Employees" + suffix] + "|";
								break;
							case "4":
								formAdvanced_Employees += "e" + Request["Advanced_Employees" + suffix] + "|";
								break;
							case "5":
								formAdvanced_Employees += "f" + Request["Advanced_Employees" + suffix] + "|";
								break;
						}
					}
				}
				if (strKey.IndexOf("SAdvanced_Estimate") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && formSAdvanced_Estimate.IndexOf(Request[strKey]) < 0)
						formSAdvanced_Estimate += Request[strKey] + "|";
				}
				if (strKey.IndexOf("SAdvanced_Category") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && formSAdvanced_Category.IndexOf(Request[strKey]) < 0)
						formSAdvanced_Category += Request[strKey] + "|";
				}
				if (strKey.IndexOf("SAdvanced_Opportunity") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && formSAdvanced_Opportunity.IndexOf(Request[strKey]) < 0)
						formSAdvanced_Opportunity += Request[strKey] + "|";
				}
			}


			string queryType = String.Empty;
			string Advanced_CompanyName = String.Empty;
			string Advanced_Address = String.Empty;
			string Advanced_City = String.Empty;
			string Advanced_State = String.Empty;
			string Advanced_Zip = String.Empty;
			string Advanced_Phone = String.Empty;
			string Advanced_Fax = String.Empty;
			string Advanced_Email = String.Empty;
			string Advanced_Site = String.Empty;
			string Advanced_Code = String.Empty;
			string SAdvanced_CompanyType = String.Empty;
			string SAdvanced_ContactType = String.Empty;
			string Advanced_Billed = String.Empty;
			string Advanced_Employees = String.Empty;
			string SAdvanced_Estimate = String.Empty;
			string SAdvanced_Category = String.Empty;
			string SAdvanced_Opportunity = String.Empty;

			queryType = " AND ((BASE_COMPANIES.FLAGGLOBALORPERSONAL=2 AND  BASE_COMPANIES.OWNERID=" + UC.UserId.ToString() + ") oR (BASE_COMPANIES.FLAGGLOBALORPERSONAL<>2))";

			StringBuilder sqlString = new StringBuilder();
			sqlString.AppendFormat("SELECT TOP {0} BASE_COMPANIES.ID,",DatabaseConnection.MaxResult);
            if (this.onlyName) sqlString.Append("BASE_COMPANIES.COMPANYNAME,BASE_COMPANIES.PHONE,BASE_COMPANIES.FAX,BASE_COMPANIES.EMAIL,BASE_COMPANIES.DESCRIPTION,BASE_COMPANIES.GROUPS, BASE_COMPANIES.SALESPERSONID, BASE_COMPANIES.OWNERID  ");
			if (this.primaryAddress) sqlString.Append("BASE_COMPANIES.INVOICINGADDRESS,BASE_COMPANIES.INVOICINGCITY,BASE_COMPANIES.INVOICINGSTATEPROVINCE,BASE_COMPANIES.INVOICINGSTATE,BASE_COMPANIES.INVOICINGZIPCODE,");
			if (this.secondAddress) sqlString.Append("BASE_COMPANIES.SHIPMENTADDRESS,BASE_COMPANIES.SHIPMENTCITY,BASE_COMPANIES.SHIPMENTSTATEPROVINCE,BASE_COMPANIES.SHIPMENTSTATE,BASE_COMPANIES.SHIPMENTZIPCODE,BASE_COMPANIES.SHIPMENTPHONE,BASE_COMPANIES.SHIPMENTFAX,BASE_COMPANIES.SHIPMENTEMAIL,");
			if (this.thirdAddress) sqlString.Append("BASE_COMPANIES.WAREHOUSEADDRESS,BASE_COMPANIES.WAREHOUSECITY,BASE_COMPANIES.WAREHOUSESTATEPROVINCE,BASE_COMPANIES.WAREHOUSESTATE,BASE_COMPANIES.WAREHOUSEZIPCODE,BASE_COMPANIES.WAREHOUSEPHONE,BASE_COMPANIES.WAREHOUSEFAX,BASE_COMPANIES.WAREHOUSEEMAIL,");
			if (this.mlMailField) sqlString.Append("CAST(BASE_COMPANIES.ID AS VARCHAR(12))+'|'+ISNULL(BASE_COMPANIES.MLEMAIL, BASE_COMPANIES.EMAIL)+'|'+BASE_COMPANIES.COMPANYNAME AS IDMAIL,");
			sqlString.Remove(sqlString.Length - 1, 1);
			sqlString.Append(" FROM BASE_COMPANIES ");
			if (formSAdvanced_Opportunity.Length > 0)
			{
				sqlString.Append("INNER JOIN CRM_OPPORTUNITYCONTACT ON BASE_COMPANIES.ID = CRM_OPPORTUNITYCONTACT.CONTACTID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 0 ");
			}
            if (geoCheck.Checked)
            {
                sqlString.AppendFormat("JOIN GEO.DBO.NEARCITIES('{0}','{1}',{2}) AS GEO ON GEO.CITY = INVOICINGCITY ", DatabaseConnection.FilterInjection(geoCity.Text), geoCountry.SelectedValue, DatabaseConnection.FilterInjection(geoDistance.Text));
            }

			sqlString.AppendFormat("WHERE (BASE_COMPANIES.LIMBO=0) {0} AND (", queryType);

			string[] strKeys = formSAdvanced_Opportunity.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_Opportunity += String.Format("(CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = {0}) OR ", int.Parse(strKey));
			}

			strKeys = formAdvanced_CompanyName.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_CompanyName += String.Format("(BASE_COMPANIES.COMPANYNAME LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}

			strKeys = formAdvanced_Address.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Address += String.Format("(BASE_COMPANIES.INVOICINGADDRESS LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_City.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_City += String.Format("(BASE_COMPANIES.INVOICINGCITY LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTCITY LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSECITY LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_State.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_State += String.Format("(BASE_COMPANIES.INVOICINGSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSESTATEPROVINCE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_Nation.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_State += String.Format("(BASE_COMPANIES.INVOICINGSTATE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTSTATE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSESTATE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_Zip.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Zip += String.Format("(BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEZIPCODE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}

			strKeys = formAdvanced_Phone.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Phone += String.Format("(BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTPHONE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEPHONE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}

			strKeys = formAdvanced_Fax.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Fax += String.Format("(BASE_COMPANIES.FAX LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTFAX LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEFAX LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_Email.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Email += String.Format("(BASE_COMPANIES.EMAIL LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTEMAIL LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEEMAIL LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_Site.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Site += String.Format("(BASE_COMPANIES.WEBSITE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formAdvanced_Code.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					Advanced_Code += String.Format("(BASE_COMPANIES.COMPANYCODE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formSAdvanced_CompanyType.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_CompanyType += String.Format("(BASE_COMPANIES.COMPANYTYPEID = '{0}') OR ", DatabaseConnection.FilterInjection(strKey));
			}
			strKeys = formSAdvanced_ContactType.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_ContactType += String.Format("(BASE_COMPANIES.CONTACTTYPEID = '{0}') OR ", DatabaseConnection.FilterInjection(strKey));
			}

			strKeys = formAdvanced_Billed.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
				{
					switch (strKey.Substring(0, 1))
					{
						case "a":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED = {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "b":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED <= {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "c":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED < {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "d":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED <> {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "e":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED > {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "f":
							Advanced_Billed += String.Format("(BASE_COMPANIES.BILLED >= {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
					}
				}
			}

			strKeys = formAdvanced_Employees.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
				{
					switch (strKey.Substring(0, 1))
					{
						case "a":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES = {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "b":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES <= {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "c":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES < {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "d":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES <> {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "e":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES > {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
						case "f":
							Advanced_Employees += String.Format("(BASE_COMPANIES.EMPLOYEES >= {0}) OR ", int.Parse(strKey.Substring(1, strKey.Length - 1)));
							break;
					}
				}
			}
			strKeys = formSAdvanced_Estimate.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_Estimate += String.Format("(BASE_COMPANIES.ESTIMATE = {0}) OR ", strKey);
			}
			strKeys = formSAdvanced_Category.Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_Category += String.Format("(BASE_COMPANIES.CATEGORIES LIKE '%|{0}|%') OR ", DatabaseConnection.FilterInjection(strKey));
			}


			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;

			if (Advanced_CompanyName.Length > 0)
			{
				sqlAv += sAnd + Advanced_CompanyName.Substring(0, Advanced_CompanyName.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Address.Length > 0)
			{
				sqlAv += sAnd + Advanced_Address.Substring(0, Advanced_Address.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_City.Length > 0)
			{
				sqlAv += sAnd + Advanced_City.Substring(0, Advanced_City.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_State.Length > 0)
			{
				sqlAv += sAnd + Advanced_State.Substring(0, Advanced_State.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Zip.Length > 0)
			{
				sqlAv += sAnd + Advanced_Zip.Substring(0, Advanced_Zip.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Phone.Length > 0)
			{
				sqlAv += sAnd + Advanced_Phone.Substring(0, Advanced_Phone.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Fax.Length > 0)
			{
				sqlAv += sAnd + Advanced_Fax.Substring(0, Advanced_Fax.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Email.Length > 0)
			{
				sqlAv += sAnd + Advanced_Email.Substring(0, Advanced_Email.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Site.Length > 0)
			{
				sqlAv += sAnd + Advanced_Site.Substring(0, Advanced_Site.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Code.Length > 0)
			{
				sqlAv += sAnd + Advanced_Code.Substring(0, Advanced_Code.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_CompanyType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_CompanyType.Substring(0, SAdvanced_CompanyType.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_ContactType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_ContactType.Substring(0, SAdvanced_ContactType.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Billed.Length > 0)
			{
				sqlAv += sAnd + Advanced_Billed.Substring(0, Advanced_Billed.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (Advanced_Employees.Length > 0)
			{
				sqlAv += sAnd + Advanced_Employees.Substring(0, Advanced_Employees.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_Estimate.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Estimate.Substring(0, SAdvanced_Estimate.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Category.Substring(0, SAdvanced_Category.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_Opportunity.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Opportunity.Substring(0, SAdvanced_Opportunity.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (sqlAv.EndsWith("AND ("))
				sqlAv = sqlAv.Substring(0, sqlAv.Length - 5);

			if (AdvancedCreatedDate1.Text.Length > 0 && AdvancedCreatedDate2.Text.Length > 0)
			{
				if (StaticFunctions.IsDate(AdvancedCreatedDate1.Text) && StaticFunctions.IsDate(AdvancedCreatedDate1.Text))
				{

					sqlAv += " AND (BASE_COMPANIES.CREATEDDATE BETWEEN '" + Convert.ToDateTime(AdvancedCreatedDate1.Text+" 00:00").ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND '" + Convert.ToDateTime(AdvancedCreatedDate2.Text+" 23:59").ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') ";
				}
			}

			G g = new G();
			string queryGroup = g.GroupsSecure("BASE_COMPANIES.GROUPS",UC);
			if (queryGroup.Length > 0)
			{
				sqlAv+=String.Format(" AND ({0})", queryGroup);
			}
			if(UC.Zones.Length>0)
				sqlAv+=String.Format(" AND ({0})", g.ZoneSecure("BASE_COMPANIES.COMMERCIALZONE",UC));


			return sqlAv + " ORDER BY BASE_COMPANIES.COMPANYNAME";
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
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
