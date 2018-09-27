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
	public partial class PopOpportunity : G
	{


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

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				string js;
				string control = Request.QueryString["textbox"].ToString();
				string control3 = Request.QueryString["textboxID"].ToString();
                string clickControl = null;
                string eventFunction = null;
                if (Request.QueryString["click"] != null)
                    clickControl = Request.QueryString["click"].ToString();
                if (Request.QueryString["event"] != null)
                    eventFunction = Request.QueryString["event"].ToString();

				js = "<script>";
				js += "function SetRef(tx,az,id){";
				js += "	dynaret('" + control + "').value=tx;";

				if (control3.Length > 0)
				{
					js += "	dynaret('" + control3 + "').value=az;";
					js += "	try{dynaret('" + control3 + "').onchange();}catch(ex){}";
				}

				js += "	self.close();";
                if (clickControl != null)
                    js += "clickElement(dynaret('" + clickControl + "'));" + Environment.NewLine;
                if (eventFunction != null)
                    js += "dynaevent('" + eventFunction + "');" + Environment.NewLine;
                js += "	parent.HideBox();}";
				js += "</script>";
				SomeJS.Text = js;
				Find.Text =Root.rm.GetString("Prftxt5");

			}
		}

		public void Find_Click(object sender, EventArgs e)
		{
			DbSqlParameter p = new DbSqlParameter("@FINDIT", "%" + FindIt.Text + "%");

			string sqlString = "SELECT CRM_OPPORTUNITY.ID, CRM_OPPORTUNITY.TITLE FROM CRM_OPPORTUNITY WHERE CRM_OPPORTUNITY.TITLE LIKE @FINDIT";
			RepeaterOpportunity.DataSource = DatabaseConnection.SecureCreateDataset(sqlString, p);
			RepeaterOpportunity.DataBind();
		}

	}
}
