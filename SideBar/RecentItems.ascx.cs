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
using Digita.Tustena.WebControls;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using System.Text;

namespace Digita.Tustena.SideBar
{
    public partial class RecentItems : SideBarControl
    {

        protected void Page_Load(object sender, EventArgs e)
       {
           if (Page.IsPostBack && Request.Form["rect"] != null && Request.Form["recid"] != null)
               GotoRecent(int.Parse(Request.Form["recid"]), int.Parse(Request.Form["rect"]));

           this.ID = "RecentItems";
           this.Title = Core.Root.rm.GetString("Recent");
           UserConfig UC = (UserConfig)HttpContext.Current.Session["UserConfig"];
               DataTable dt = Recent.LoadRecentItems(UC.UserId);
               recentLbl.Text = RenderList(dt);
       }

        private string RenderList(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            for(int i=0;i<dt.Rows.Count;i++)
            {
                sb.AppendFormat("<div class=\"recentList\"><a href=\"javascript:JsPostback('recid={1},rect={2}',1)\">{0}</a></div>", dt.Rows[i][1], dt.Rows[i][2], dt.Rows[i][0]);
            }
            return sb.ToString();
        }

        private void GotoRecent(int id, int type)
        {
            Session["openId"] = id;
            switch ((RecentType)type)
            {
                case RecentType.Company:
                    Response.Redirect("/CRM/CRM_Companies.aspx?m=25&dgb=1&si=29");
                    break;
                case RecentType.Contact:
                    Response.Redirect("/CRM/Base_Contacts.aspx?m=25&dgb=1&si=31");
                    break;
                case RecentType.Lead:
                    Response.Redirect("/CRM/CRM_Lead.aspx?m=25&dgb=1&si=53");
                    break;
            }
        }
    }
}
