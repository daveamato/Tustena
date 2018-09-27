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
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace Digita.Tustena.Common
{
	public partial class PGParser : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "ret","<script>opener.location.href=opener.location.href;self.close();</script>");
			}
		}
		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.btn.Click += new EventHandler(this.btn_Click);
		}
		#endregion

		private void btn_Click(object sender, EventArgs e)
		{
			string pgtext = pg.Text;
			Regex rx = new Regex(@"(?<azienda>.+)\n(?<cap>\d{5})(?<citta>[^\(-]*)(?:\s\((?<prov>\w*)\)[\s-]*)(?<indirizzo>.*)\n(?:tel\D*(?<tel>[\d ]+))?\W*(?:fax\D*(?<fax>[\d ]+))?");
			Match m = rx.Match(pgtext);
			string azienda = m.Groups["azienda"].Value.Trim();
			string cap = m.Groups["cap"].Value.Trim();
			string citta = m.Groups["citta"].Value.Trim();
			string prov = m.Groups["prov"].Value.Trim();
			string indirizzo = m.Groups["indirizzo"].Value.Trim();
			string tel = m.Groups["tel"].Value.Trim();
			string fax = m.Groups["fax"].Value.Trim();

			string js = "<script>dynaret('CompanyName').value = '"+azienda+"';";
			js += "dynaret('Invoice_Zip').value = '"+cap+"';";
			js += "dynaret('Invoice_City').value = '"+citta+"';";
			js += "dynaret('Invoice_StateProvince').value = '"+prov+"';";
			js += "dynaret('Invoice_Address').value = '"+indirizzo+"';";
			js += "dynaret('Phone').value = '"+tel+"';";
			js += "dynaret('Fax').value = '"+fax+"';";
			js += "self.close();parent.HideBox();</script>";
			ClientScript.RegisterStartupScript(this.GetType(), "ret", js);

		}
	}
}
