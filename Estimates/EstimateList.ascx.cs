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
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Estimates
{
	public partial class EstimateList : UserControl
	{
		public G G = new G();
		private UserConfig UC = new UserConfig();
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];
		private string companyId;
		private bool isCompany = true;

		public string CompanyID
		{
			set
			{
				this.companyId = value;
				ViewState["_CompanyID"] = value;
			}
		}

		public bool IsCompany
		{
			set
			{
				this.isCompany = value;
				ViewState["_IsCompany"] = value;
			}
		}

		public int ItemCount
		{
			get { return RepeaterSearch.Items.Count; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.Session.IsNewSession)
				HttpContext.Current.Response.Redirect("/login.aspx");
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

			if (this.companyId == null) this.companyId = ViewState["_CompanyID"].ToString();
			try
			{
				this.isCompany = Convert.ToBoolean(ViewState["_IsCompany"]);
			}
			catch
			{
			}
		}

		public void Refresh()
		{
			DataTable dt;
			if (isCompany)
			{
				dt = DatabaseConnection.CreateDataset("SELECT ESTIMATES.ID,ESTIMATES.TITLE,ESTIMATES.ESTIMATEDATE,ESTIMATES.CLIENTID,BASE_COMPANIES.COMPANYNAME FROM ESTIMATES LEFT OUTER JOIN BASE_COMPANIES ON ESTIMATES.CLIENTID=BASE_COMPANIES.ID WHERE ESTIMATES.COMPANYLEAD=0 AND ESTIMATES.CLIENTID=" + companyId + " ORDER BY ESTIMATES.ESTIMATEDATE DESC").Tables[0];
			}
			else
			{
				dt = DatabaseConnection.CreateDataset("SELECT ESTIMATES.ID,ESTIMATES.TITLE,ESTIMATES.ESTIMATEDATE,ESTIMATES.CLIENTID,ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'')+' '+ISNULL(CRM_LEADS.COMPANYNAME,'') AS COMPANYNAME FROM ESTIMATES LEFT OUTER JOIN CRM_LEADS ON ESTIMATES.CLIENTID=CRM_LEADS.ID WHERE ESTIMATES.COMPANYLEAD=1 AND ESTIMATES.CLIENTID=" + companyId + " ORDER BY ESTIMATES.ESTIMATEDATE DESC").Tables[0];
			}
			RepeaterSearch.DataSource = dt;
			RepeaterSearch.DataBind();
			if (RepeaterSearch.Items.Count > 0)
			{
				RepeaterSearch.Visible = true;
				RepeaterSearchInfo.Visible = false;
			}
			else
			{
				RepeaterSearch.Visible = false;
				RepeaterSearchInfo.Visible = true;
				RepeaterSearchInfo.Text =Root.rm.GetString("Esttxt33");
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
