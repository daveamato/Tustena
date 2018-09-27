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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using System.Data;

namespace Digita.Tustena
{
	public partial class quick : G
	{
		private string calAppointment = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					LblHeader.Text = G.ABCHeaderHtml(Request.Params["List"]);
					BtnSearch.Text =Root.rm.GetString("Find");

					BtnSearch.Attributes.Add("onclick", "return searchcontact()");
                    filterActivity.Text = Root.rm.GetString("Quicktxt15");
					FlagSearch.RepeatDirection = RepeatDirection.Horizontal;

					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt11"), "0"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt8"), "1"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt9"), "2"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt10"), "3"));
					FlagSearch.Items[0].Selected = true;
                    colorlegend.Text = GetColorLegend;
                    colorlegend.Attributes.Add("style", "display:none");
                    phnLegend.Text = GetPhnLegend;

				}
			}
		}

        private string GetPhnLegend
        {
            get
            {
                string ret = string.Empty;
                ret += string.Format("&nbsp;<img style=\"cursor:pointer\" alt=\"{0}\" onclick=\"FilterPhn('0')\" src=\"i/phnblu.gif\">", Root.rm.GetString("Quicktxt19"));
                ret += string.Format("&nbsp;<img style=\"cursor:pointer\" alt=\"{0}\" onclick=\"FilterPhn('1')\" src=\"i/phn.gif\">", Root.rm.GetString("Quicktxt20"));
                ret += string.Format("&nbsp;<img style=\"cursor:pointer\" alt=\"{0}\" onclick=\"FilterPhn('2')\" src=\"i/phnred.gif\">", Root.rm.GetString("Quicktxt21"));
                ret += string.Format("&nbsp;<img style=\"cursor:pointer\" alt=\"{0}\" onclick=\"FilterPhn('3')\" src=\"i/phngld.gif\">", Root.rm.GetString("Quicktxt22"));
                ret += string.Format("&nbsp;<img style=\"cursor:pointer\" alt=\"{0}\" onclick=\"FilterPhn('4')\" src=\"i/phngrn.gif\">", Root.rm.GetString("Quicktxt23"));
                return ret;
            }
        }

        private string GetColorLegend
        {
            get
            {
                string ret = string.Empty;
                DataTable dt = Database.DatabaseConnection.CreateDataset("select k_id,description from CRM_OpportunityTableType where type=1 and lang='" + UC.Culture.Substring(0, 2) + "'").Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    ret += string.Format("&nbsp;<span class=\"legend lc{0}\" onclick=\"FilterState('{0}')\">{1}</span>", dr[0], dr[1]);
                }
                ret += string.Format("&nbsp;<span class=\"legend lc0\" onclick=\"FilterState('-1')\">{0}</span>", "All");
                return ret;
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
