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

namespace Digita.Tustena
{
	public partial class AddCategories : G
	{

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					ListItem lt = new ListItem();
					lt.Value = "0";
					lt.Text =Root.rm.GetString("CRMcontxt47");
					RadioButtonList1.Items.Add(lt);
					lt = new ListItem();
					lt.Value = "1";
					lt.Text =Root.rm.GetString("CRMcontxt48");
					RadioButtonList1.Items.Add(lt);

					RadioButtonList1.Items[0].Selected = true;
				}
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
			this.SubmitCat.Click += new EventHandler(this.btn_Click);
		}

		#endregion

		public void btn_Click(object sender, EventArgs e)
		{
			if (Category.Text.Length > 0)
			{
				string sqlTable;
				if (Request.Params["Ref"] != null)
					sqlTable = "CRM_ReferrerCategories";
				else
					sqlTable = "CRM_ContactCategories";
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("DESCRIPTION", Category.Text);

					dg.Add("CREATEDBYID", UC.UserId);
					if (RadioButtonList1.Items[1].Selected) dg.Add("FLAGPERSONAL", 1);
					dg.Execute(sqlTable);
				}
				SomeJS.Text = "<script>parent.__doPostBack('RefreshRepCategories');self.close();parent.HideBox();</script>";
			}
		}

	}
}
