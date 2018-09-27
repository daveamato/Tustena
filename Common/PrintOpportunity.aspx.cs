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

namespace Digita.Tustena.Common
{
	public partial class PrintOpportunity : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				IDOp.Text = Request.QueryString["textbox"].ToString();

			}
		}

		public void btn_click(object sender, EventArgs e)
		{
			char[] p = "000".ToCharArray();
			foreach (string var in Request.Form)
			{
				switch (var)
				{
					case "print2":
						p[0] = '1';
						break;
					case "print3":
						p[1] = '1';
						break;
				}
			}

			string js;
			js = "<script>";
			js += "function SetRef(){";
			js += "	window.open('/report/Opportunityreport.aspx?render=no&id=" + IDOp.Text + "&p=" + (new string(p)) + "');";
			js += "	self.close();";
			js += "	parent.HideBox();}";
			js += "SetRef();</script>";
			SomeJS.Text = js;


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
			this.SubmitPrint.Click += new EventHandler(this.btn_click);

		}

		#endregion
	}
}
