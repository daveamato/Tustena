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
using System.Collections;
using System.Web.UI;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class PrintLead : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Session["report"] = null;
			ArrayList report = new ArrayList();
			CompanyReport cr = new CompanyReport();
			Hashtable HtParams = new Hashtable();


			cr = new CompanyReport();
			cr.idfield = Convert.ToInt32(DatabaseConnection.SqlScalartoObj("SELECT ID FROM QB_CUSTOMERQUERY WHERE TITLE='Fixed6'"));//ex 222;
			HtParams = new Hashtable();
			HtParams.Add("ID", Request.Params["leadid"]);
			cr.Params = HtParams;
			cr.Finalize = true;
			cr.Type = 0;
			cr.itemPage = 10;

			report.Add(cr);

			Session["report"] = report;
			string js;
			js = "<script>";
			js += "function SetRef(){";
			js += "	window.open('/report/htmlreport.aspx');";
			js += "	self.close();";
			js += "	parent.HideBox();}";
			js += "SetRef();</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "pl", js);
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
