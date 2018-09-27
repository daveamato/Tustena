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
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Digita.Tustena.Database;
using Digita.Tustena.Project;
using System.Drawing;
using System.Collections.Specialized;
using System.Web.Caching;

namespace Digita.Tustena.Project
{
    public partial class ProjectGantt : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                if (Session["currentproject"]!=null)
                {
                    Gantt1.prjID = long.Parse(Session["currentproject"].ToString());
                    Session.Remove("currentproject");
                }
                Gantt1.MakeGantt();
                litPrint.Text = string.Format("<img src=/i/printer.gif border=0 style=\"cursor:pointer\" onclick=\"PrintGantt({0})\">",Gantt1.prjID);
                ProjectReport pr = new ProjectReport();
                pr.prjID = Gantt1.prjID;
                pr.UC = UC;
                litForecast.Text = pr.ForecastDate();
            }
        }

    }
}
