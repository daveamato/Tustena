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
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Estimates
{
	public partial class EstimateTemplate : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					DataTable myDataTable;
					myDataTable = DatabaseConnection.CreateDataset("SELECT BODY,LOGO FROM TEMPLATES WHERE TEMPLATENAME='ESTIMATE' AND LANG='" + UC.CultureSpecific + "'").Tables[0];
					if (myDataTable.Rows.Count == 0)
					{
						myDataTable = DatabaseConnection.CreateDataset("SELECT BODY,LOGO FROM TEMPLATES WHERE TEMPLATENAME='ESTIMATE' AND LANG='EN'").Tables[0];
					}
					if (myDataTable.Rows.Count > 0)
					{
						TextReader r = new StringReader(myDataTable.Rows[0][0].ToString());
						DataSet content = new DataSet();
						content.ReadXml(r);
						FreeText.Text = content.Tables[0].Rows[0][0].ToString();
					}
					ViewLogo.Src = (myDataTable.Rows[0][1] == DBNull.Value) ? "/logos/logo.gif" : "/logos/" + myDataTable.Rows[0][1].ToString();

					NomeFile.Attributes.Add("onchange", "LoadLogo()");
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
			this.SaveTemplate.Click += new EventHandler(SaveTemplate_Click);
		}

		#endregion

		private void SaveTemplate_Click(object sender, EventArgs e)
		{
			TemplateAdmin ta = new TemplateAdmin();
			string[] s = new string[1];
			s[0] = FreeText.Text;
			string body = ta.GetXMLBody(s);

			string sqlString;
			sqlString = "SELECT ID FROM TEMPLATES WHERE TEMPLATENAME = 'ESTIMATE'";
			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if (dg.HasRows)
				{
					dg.Add("TEMPLATENAME", "ESTIMATE", 'I');
					dg.Add("LANG", UC.CultureSpecific, 'I');

				}
				if (NomeFile.Value.Length > 0)
				{
					string PathTemplate;
					PathTemplate = Path.Combine(Request.PhysicalApplicationPath, "Logos");
					if (File.Exists(Path.Combine(PathTemplate, Path.GetFileName(NomeFile.Value))))
						File.Delete(Path.Combine(PathTemplate, Path.GetFileName(NomeFile.Value)));

					NomeFile.PostedFile.SaveAs(Path.Combine(PathTemplate, Path.GetFileName(NomeFile.Value)));
					dg.Add("LOGO", Path.GetFileName(NomeFile.Value));
				}

				dg.Add("BODY", body);

				dg.Execute("TEMPLATES", "ID=" + dg.ExternalReaderRowId);
			}
			Response.Redirect("/estimates/estimatehome.aspx");

		}
	}
}
