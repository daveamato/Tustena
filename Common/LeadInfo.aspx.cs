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
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class LeadInfo : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				StringBuilder sqlString = new StringBuilder();
				sqlString.Append("SELECT CRM_CROSSLEAD.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, ");
				sqlString.Append("BASE_CONTACTS.SURNAME + ' ' + BASE_CONTACTS.NAME AS CONTACTNAME, ");
				sqlString.Append("CRM_OPPORTUNITY.TITLE AS OPPORTUNITYNAME, ");
				sqlString.Append("ACCOUNT_1.SURNAME + ' ' + ACCOUNT_1.NAME AS OWNERNAME, ");
				sqlString.Append("ACCOUNT_2.SURNAME + ' ' + ACCOUNT_2.NAME AS SALESPERSONNAME, ");
				sqlString.Append("COMPANYTYPE.DESCRIPTION AS INDUSTRYNAME, ");
				sqlString.Append("CRM_LEADDESCRIPTION1.DESCRIPTION AS STATUSDESCRIPTION, ");
				sqlString.Append("CRM_LEADDESCRIPTION2.DESCRIPTION AS RATINGDESCRIPTION, ");
				sqlString.Append("CRM_LEADDESCRIPTION3.DESCRIPTION AS PRODUCTINTERESTDESCRIPTION, ");
				sqlString.Append("CRM_LEADDESCRIPTION4.DESCRIPTION AS SOURCEDESCRIPTION, ");
				sqlString.Append("CRM_LEADDESCRIPTION5.DESCRIPTION AS LEADCURRENCYDESCRIPTION ");
				sqlString.Append("FROM CRM_CROSSLEAD ");
				sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_2 ON CRM_CROSSLEAD.SALESPERSON = ACCOUNT_2.UID ");
				sqlString.Append("LEFT OUTER JOIN ACCOUNT ACCOUNT_1 ON CRM_CROSSLEAD.LEADOWNER = ACCOUNT_1.UID ");
				sqlString.Append("LEFT OUTER JOIN CRM_OPPORTUNITY ON CRM_CROSSLEAD.ASSOCIATEDOPPORTUNITY = CRM_OPPORTUNITY.ID ");
				sqlString.Append("LEFT OUTER JOIN BASE_CONTACTS ON CRM_CROSSLEAD.ASSOCIATEDCONTACT = BASE_CONTACTS.ID ");
				sqlString.Append("LEFT OUTER JOIN BASE_COMPANIES ON CRM_CROSSLEAD.ASSOCIATEDCOMPANY = BASE_COMPANIES.ID ");

				sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION1 ON CRM_CROSSLEAD.STATUS = CRM_LEADDESCRIPTION1.K_ID AND CRM_LEADDESCRIPTION1.LANG='" + UC.Culture.Substring(0, 2) + "' ");
				sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION2 ON CRM_CROSSLEAD.RATING = CRM_LEADDESCRIPTION2.K_ID AND CRM_LEADDESCRIPTION2.LANG='" + UC.Culture.Substring(0, 2) + "' ");
				sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION3 ON CRM_CROSSLEAD.PRODUCTINTEREST = CRM_LEADDESCRIPTION3.K_ID AND CRM_LEADDESCRIPTION3.LANG='" + UC.Culture.Substring(0, 2) + "' ");
				sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION4 ON CRM_CROSSLEAD.SOURCE = CRM_LEADDESCRIPTION4.K_ID AND CRM_LEADDESCRIPTION4.LANG='" + UC.Culture.Substring(0, 2) + "' ");
				sqlString.Append("LEFT OUTER JOIN CRM_LEADDESCRIPTION CRM_LEADDESCRIPTION5 ON CRM_CROSSLEAD.LEADCURRENCY = CRM_LEADDESCRIPTION5.K_ID AND CRM_LEADDESCRIPTION5.LANG='" + UC.Culture.Substring(0, 2) + "' ");

				sqlString.AppendFormat("LEFT OUTER JOIN COMPANYTYPE ON CRM_CROSSLEAD.INDUSTRY = COMPANYTYPE.K_ID AND COMPANYTYPE.LANG='{0}'",UC.Culture.Substring(0, 2));

				DbSqlParameterCollection p = new DbSqlParameterCollection();
				p.Add(new DbSqlParameter("@LEADID", int.Parse(Request.Params["id"])));

				RepCrossLead.DataSource = DatabaseConnection.SecureCreateDataset(sqlString.ToString() + "WHERE CRM_CROSSLEAD.LEADID=@LEADID ", p).Tables[0].DefaultView;
				RepCrossLead.DataBind();
			}
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
