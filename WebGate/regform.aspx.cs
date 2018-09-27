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
using System.Web.UI;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class WebGate1 : Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
			if (Request.Form["CID"] != null)
			{

				string company = Request.Form["CID"].ToString();
				string OwnerID;
				if (Request.Form["OwnerID"] != null)
					OwnerID = Request.Form["OwnerID"].ToString();
				else
					OwnerID = "46";

				string groups;
				if (Request.Form["Groups"] != null)
					groups = Request.Form["Groups"].ToString();
				else
					groups = "13";

				if (Request.UrlReferrer.ToString().StartsWith("http://www.digita.it"))
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("OWNERID", OwnerID);
						dg.Add("CREATEDBYID", OwnerID);
						dg.Add("GROUPS", "|" + groups + "|");

						if (Request.Form["CompanyName"].Length > 0)
						{
							dg.Add("COMPANYNAME", Request.Form["CompanyName"].ToString());
							dg.Add("COMPANYNAME", StaticFunctions.FilterSearch(Request.Form["CompanyName"].ToString()));
						}

						if (Request.Form["InvoicingAddress"].Length > 0)
							dg.Add("INVOICINGADDRESS", Request.Form["InvoicingAddress"].ToString());

						if (Request.Form["InvoicingZipCode"].Length > 0)
							dg.Add("INVOICINGZIPCODE", Request.Form["InvoicingZipCode"].ToString());

						if (Request.Form["InvoicingCity"].Length > 0)
							dg.Add("INVOICINGCITY", Request.Form["InvoicingCity"].ToString());

						if (Request.Form["InvoicingStateProvince"].Length > 0)
							dg.Add("INVOICINGSTATEPROVINCE", Request.Form["InvoicingStateProvince"].ToString());

						if (Request.Form["Phone"].Length > 0)
							dg.Add("PHONE", Request.Form["Phone"].ToString());

						if (Request.Form["Fax"].Length > 0)
							dg.Add("FAX", Request.Form["Fax"].ToString());

						if (Request.Form["Email"].Length > 0)
							dg.Add("EMAIL", Request.Form["Email"].ToString());

						dg.Execute("BASE_COMPANIES");
					}
				}
			}
		}

		#region Web Form Designer generated code

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
