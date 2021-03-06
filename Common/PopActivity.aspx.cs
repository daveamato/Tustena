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
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class PopActivity : G
	{
		private string activityId = "-1";
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				string js;
				string control = Request.QueryString["textbox"].ToString();
				if(Request.QueryString["Activity"]!=null)
					activityId = Request.QueryString["Activity"].ToString();
				string control2 = Request.QueryString["textboxID"].ToString();
				js = "<script>";
				js += "function SetRef(id,tx){";
				js += "	dynaret('" + control + "').value=tx;";
				js += "	dynaret('" + control2 + "').value=id;";
				js += "	self.close();";
				js += "	parent.HideBox();}";
				js += "</script>";
				SomeJS.Text = js;
				Find.Text =Root.rm.GetString("Prftxt5");

				if (!Page.IsPostBack)
					SetFocus(FindIt);

				if (!StaticFunctions.IsBlank(Request.QueryString["company"]))
				{
					RepActivity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM CRM_WORKACTIVITY WHERE ID<>"+int.Parse(activityId)+" AND (COMPANYID=" + int.Parse(Request.QueryString["company"].ToString()) + ") ORDER BY ACTIVITYDATE DESC");
					RepActivity.DataBind();
				}

				if (!StaticFunctions.IsBlank(Request.QueryString["contact"]))
				{
					RepActivity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM CRM_WORKACTIVITY WHERE ID<>"+int.Parse(activityId)+" AND (REFERRERID=" + int.Parse(Request.QueryString["contact"]).ToString() + ") ORDER BY ACTIVITYDATE DESC");
					RepActivity.DataBind();
				}
			}
		}

		public void Find_Click(object sender, EventArgs e)
		{
			RepActivity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM CRM_WORKACTIVITY WHERE ID<>"+int.Parse(activityId)+" AND (SUBJECT LIKE '%" + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ORDER BY ACTIVITYDATE DESC");
			RepActivity.DataBind();
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
			this.Find.Click += new EventHandler(this.Find_Click);

		}

		#endregion
	}
}
