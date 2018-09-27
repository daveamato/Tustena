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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Digita.Tustena.Error
{
	public class RepairDb : Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

			DataTable dt = G.CreateDataset("SELECT OPPORTUNITYID,CONTACTID,CUSTOMERID FROM CRM_CROSSOPPORTUNITY WHERE CONTACTTYPE=0 GROUP BY OPPORTUNITYID,CONTACTID,CUSTOMERID").Tables[0];
			int quante=0;
			foreach(DataRow dr in dt.Rows)
			{
				for(int i=1;i<=3;i++)
				{
					string dt1 = G.SqlScalar("SELECT COUNT(*) FROM CRM_CROSSOPPORTUNITY WHERE (CONTACTTYPE = 0 AND TYPE="+i.ToString()+") AND OPPORTUNITYID="+dr[0].ToString()+" AND CONTACTID="+dr[1].ToString());
					if(dt1=="0")
					{
						StringBuilder s = new StringBuilder();
						s.Append("INSERT INTO CRM_CROSSOPPORTUNITY (OPPORTUNITYID, CONTACTID, TABLETYPEID, TYPE, CUSTOMERID, CONTACTTYPE)");
						s.Append("VALUES( ");
						s.AppendFormat("{0},",dr[0].ToString());
						s.AppendFormat("{0},",dr[1].ToString());
						s.Append("0,");
						s.AppendFormat("{0},",i.ToString());
						s.AppendFormat("{0},",dr[2].ToString());
						s.Append("0)");

						G.DoCommand(s.ToString());
						quante++;
					}
				}

			}
			Response.Write(quante.ToString());

		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
