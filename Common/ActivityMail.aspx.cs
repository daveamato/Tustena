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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class ActivityMail : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Login())
			{
				if ((Request.QueryString["id1"] != null && Request.QueryString["id1"].Length != 0)) Repeater1.DataSource = DatabaseConnection.SecureCreateDataset("SELECT COMPANYNAME, EMAIL, SHIPMENTEMAIL, WAREHOUSEEMAIL FROM BASE_COMPANIES WHERE EMAIL<>'' AND ID=@ID", new DbSqlParameter("@ID", int.Parse(Request.QueryString["id1"]).ToString()));
				if ((Request.QueryString["id2"] != null && Request.QueryString["id2"].Length != 0)) Repeater2.DataSource = DatabaseConnection.SecureCreateDataset("SELECT NAME, SURNAME, EMAIL FROM BASE_CONTACTS WHERE EMAIL<>'' AND ID=@ID", new DbSqlParameter("@ID", int.Parse(Request.QueryString["id2"]).ToString()));
				if ((Request.QueryString["id3"] != null && Request.QueryString["id3"].Length != 0)) Repeater3.DataSource = DatabaseConnection.SecureCreateDataset("SELECT NAME, SURNAME, COMPANYNAME, EMAIL FROM CRM_LEADS WHERE EMAIL<>'' AND ID=@ID", new DbSqlParameter("@ID", int.Parse(Request.QueryString["id3"]).ToString()));
				Repeater1.DataBind();
				Repeater2.DataBind();
				Repeater3.DataBind();

				if (Repeater1.Items.Count == 0 && Repeater2.Items.Count == 0 && Repeater3.Items.Count == 0)
					Info.Text =Root.rm.GetString("NoMail");
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
