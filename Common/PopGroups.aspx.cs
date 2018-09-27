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
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class PopGroups : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				string js;
				string control = Request.QueryString["textbox"].ToString();
				string control2 = (Request.QueryString["textbox2"] != null) ? Request.QueryString["textbox2"].ToString() : "";

				js = "<script>" + Environment.NewLine;
				js += "function SetRef(tx,gr){" + Environment.NewLine;
				js += "	dynaret('" + control + "').value=tx;" + Environment.NewLine;
				if (control2.Length > 0) js += "	dynaret('" + control2 + "').value=gr;" + Environment.NewLine;
				js += "	self.close();" + Environment.NewLine;
				js += "	parent.HideBox();}" + Environment.NewLine;
				js += "</script>" + Environment.NewLine;
				ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", js);
				FindGroups();
			}
		}

		private void FindGroups()
		{
string sql;
sql = "SELECT ID,DESCRIPTION FROM GROUPS";
			Groups.DataSource = DatabaseConnection.CreateDataset(sql);
			Groups.DataBind();
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
