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
using System.Web;
using System.Web.UI;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class GetXmlAccount : Page
	{
		public UserConfig UC = new UserConfig();

		public void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			string sqlString = String.Empty;
			if (Request.QueryString["d"] != null)
			{
				sqlString = "SELECT ACCOUNT.UID, (ACCOUNT.SURNAME+' '+ACCOUNT.NAME+' ('+OFFICES.OFFICE+')') AS ACCOUNTNAME ";
				sqlString += "FROM ACCOUNT INNER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE ACCOUNT.OFFICEID=" + Request.QueryString["d"].ToString() + " ORDER BY SURNAME,NAME ASC";
			}
			if (Request.QueryString["c"] != null)
			{
				string find = DatabaseConnection.FilterInjection(Request.QueryString["c"].ToString());
				sqlString = "SELECT ACCOUNT.UID, (ACCOUNT.SURNAME+' '+ACCOUNT.NAME+' ('+OFFICES.OFFICE+')') AS ACCOUNTNAME ";
				sqlString += "FROM ACCOUNT LEFT OUTER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE (ACCOUNT.NAME LIKE '%" + find + "%' ";
				sqlString += "OR ACCOUNT.SURNAME LIKE '%" + find + "%') ORDER BY SURNAME,NAME ASC";
			}

			DataSet myDataSet = DatabaseConnection.CreateDataset(sqlString);
			Response.ContentType = "text/xml";
			myDataSet.WriteXml(Response.OutputStream);

		}
	}
}
