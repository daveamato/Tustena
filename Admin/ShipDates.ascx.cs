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
using Digita.Tustena;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Admin
{
    public partial class ShipDates : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.btnSave.Text = Root.rm.GetString("SaveAll");
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];

			if(Page.IsPostBack)
            {
            }
        }

        public DataSet ReloadDataSet()
		{

		    return DatabaseConnection.CreateDataset("SELECT * FROM QUOTESHIPMENT");
		}

        public void LoadGrid()
        {
            dgValueEditor.DataSource = ReloadDataSet();
            dgValueEditor.Columns[0].HeaderText = Root.rm.GetString("QuoShiptxt1");
            dgValueEditor.Columns[1].HeaderText = Root.rm.GetString("QuoShiptxt2");
            dgValueEditor.Columns[2].HeaderText = "<img src=/i/trash.gif border=0>";

            dgValueEditor.DataBind();
        }

        protected void dgValueEditor_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    TextBox type = (TextBox)e.Item.FindControl("type");
                    type.Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "DESCRIPTION"));
                    CheckBox flagData = (CheckBox)e.Item.FindControl("flagData");
                    flagData.Checked = (bool)DataBinder.Eval((DataRowView)e.Item.DataItem, "REQUIREDDATE");
                    Literal rowid = (Literal)e.Item.FindControl("rowid");
                    rowid.Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "ID"));
                    break;
                case ListItemType.Footer:
                    LinkButton Linkbutton1 = (LinkButton)e.Item.FindControl("Linkbutton1");
                    Linkbutton1.Text = Root.rm.GetString("Add");
                    break;
            }
        }

        protected void dgValueEditor_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                using (DigiDapter dg = new DigiDapter())
                {
                    dg.Add("DESCRIPTION", ((TextBox)e.Item.FindControl("Newtype")).Text);
                    dg.Add("REQUIREDDATE", ((CheckBox)e.Item.FindControl("NewflagData")).Checked);
                    dg.Execute("QUOTESHIPMENT");
                }

            }
            LoadGrid();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem di in dgValueEditor.Items)
            {
                if (di.ItemType == ListItemType.Item || di.ItemType == ListItemType.AlternatingItem)
                {
                    if (((CheckBox)di.FindControl("chkDelete")).Checked)
                    {
                        DatabaseConnection.DoCommand(string.Format("DELETE FROM QUOTESHIPMENT WHERE ID={0}", ((Literal)di.FindControl("rowid")).Text));
                    }
                    else
                    {
                        using (DigiDapter dg = new DigiDapter())
                        {
                            dg.Add("DESCRIPTION", ((TextBox)di.FindControl("type")).Text);
                            dg.Add("REQUIREDDATE", ((CheckBox)di.FindControl("flagData")).Checked);
                            dg.Execute("QUOTESHIPMENT", "ID=" + ((Literal)di.FindControl("rowid")).Text);
                        }
                    }
                }
            }
            LoadGrid();
        }
    }
}
