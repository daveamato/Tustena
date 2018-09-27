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
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Catalog
{
	public partial class CatalogHome : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DataTable dt;
dt = DatabaseConnection.CreateDataset("SELECT DISTINCT TOP 10 PRODUCTID,COUNT(*) AS NTIMES FROM CRM_BILLROWS GROUP BY PRODUCTID").Tables[0];
				string res = String.Empty;
				foreach (DataRow dr in dt.Rows)
				{
					if (dr[0].ToString() != "0")
						res += DatabaseConnection.SqlScalar("SELECT SHORTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID=" + dr[0].ToString()) + "|" + dr[1].ToString() + "|";
					else
						res +=Root.rm.GetString("Cahtxt3") + "|" + dr[1].ToString() + "|";
				}

				if (res.Length > 0)
					Result.Text = string.Format("<img src=\"/chart/pie.aspx?data={0}\">", res.Substring(0, res.Length - 1));
				else
					Result.Text =Root.rm.GetString("Cahtxt4");

				StringBuilder legend = new StringBuilder();

				legend.Append("<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">");
				legend.Append("<tr><td style=\"border-bottom:1px solid black;font-size:1px;\">&nbsp;</td><td style=\"border-bottom:1px solid black\">&nbsp;</td><td class=normal style=\"border-bottom:1px solid black\" width=\"50px\">n</td></tr>");
				Pie_chart pc = new Pie_chart();
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					string title = DatabaseConnection.SqlScalar("SELECT SHORTDESCRIPTION FROM CATALOGPRODUCTS WHERE ID=" + dt.Rows[i][0].ToString());
					if (title.Length == 0) title =Root.rm.GetString("Cahtxt3");
					legend.AppendFormat("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"{0}\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{1}</td>", pc.color[i].Name, title);
					legend.AppendFormat("<td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr>", dt.Rows[i][1].ToString());
				}
				pc.Dispose();
				legend.Append("</table");
				Legend.Text = legend.ToString();
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
