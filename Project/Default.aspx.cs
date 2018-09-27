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
using System.Web.UI.WebControls;
using Digita.Tustena.Database;
using Digita.Tustena.Project;

namespace Digita.Tustena.Project
{
	public partial class ProjectsList : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                DataSet ds = DatabaseConnection.CreateDataset("SELECT PROJECT.ID, TITLE AS NAME, '' AS BLOCKCOLOR, PLANNEDSTARTDATE AS STARTDATE, REALSTARTDATE, PLANNEDENDDATE AS ENDDATE, REALENDDATE FROM PROJECT JOIN PROJECT_TIMING ON PROJECT.ID = PROJECT_TIMING.IDRIF WHERE PROJECT_TIMING.RIFTYPE=0;SELECT PROJECT_SECTION.IDRIF, PROJECT_SECTION.TITLE AS NAME, '' AS HREF, PLANNEDSTARTDATE AS STARTDATE, REALSTARTDATE, PLANNEDENDDATE AS ENDDATE, REALENDDATE FROM PROJECT_SECTION LEFT OUTER JOIN PROJECT ON PROJECT_SECTION.IDRIF = PROJECT.ID JOIN PROJECT_TIMING ON PROJECT_SECTION.ID = PROJECT_TIMING.IDRIF WHERE PROJECT_TIMING.RIFTYPE=1");
                ds.Tables[0].TableName = "group";
                ds.Tables[1].TableName = "block";
                DataRelation dl = ds.Relations.Add("projects", ds.Tables["group"].Columns["id"], ds.Tables["block"].Columns["idrif"]);
                dl.Nested = true;
                string xmlData;
                xmlData = (ds.GetXml());
                GanttControl.XMLData = xmlData;
                GanttControl.BlankGifPath = "/i/trans.gif";
                GanttControl.Year = 2006;
                GanttControl.Quarter = 1;
                GanttControl.BlockColor = "blue";
                GanttControl.ToggleColor = "#dcdcdc";
                GanttControl.CellHeight = 15;
                GanttControl.CellWidth = 15;
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

		}
		#endregion
	}

}
