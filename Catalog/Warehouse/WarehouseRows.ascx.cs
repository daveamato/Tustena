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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Catalog.Warehouse
{
    public partial class WarehouseRows : System.Web.UI.UserControl
    {
        private UserConfig UC = new UserConfig();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {
            InitializeComponents();
            base.OnInit(e);
        }

        private void InitializeComponents()
        {
            this.WareHouseEditor.ItemDataBound += new DataGridItemEventHandler(WareHouseEditor_ItemDataBound);
            this.WareHouseEditor.ItemCommand += new DataGridCommandEventHandler(WareHouseEditor_ItemCommand);
        }

        void WareHouseEditor_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Add":
                    using (DigiDapter dg = new DigiDapter())
                    {
                        dg.Add("IDRIF", refID);
                        dg.Add("CODE1",((TextBox)e.Item.FindControl("Newcode1")).Text);
                        dg.Add("CODE2", ((TextBox)e.Item.FindControl("Newcode2")).Text);
                        dg.Add("CODE3", ((TextBox)e.Item.FindControl("Newcode3")).Text);
                        if (((TextBox)e.Item.FindControl("Newavailable")).Text.Length > 0)
                            dg.Add("QTAAVAILABLE", ((TextBox)e.Item.FindControl("Newavailable")).Text);
                        else
                            dg.Add("QTAAVAILABLE", 0);
                        if (((TextBox)e.Item.FindControl("Newordered")).Text.Length > 0)
                            dg.Add("QTAORDERED", ((TextBox)e.Item.FindControl("Newordered")).Text);
                        else
                            dg.Add("QTAORDERED", 0);

                        dg.Add("EXPECTEDARRIVAL", ((TextBox)e.Item.FindControl("Newexpectedarrival")).Text);
                        dg.Add("REALARRIVAL", ((TextBox)e.Item.FindControl("Newrealarrival")).Text);
                        dg.Add("STATUS", ((RadioButtonList)e.Item.FindControl("Newstatus")).SelectedValue);
                        dg.Execute("WAREHOUSE");
                    }
                    break;
                case "Modify":
                    foreach (DataGridItem di in WareHouseEditor.Items)
                    {
                        if (di.ItemType == ListItemType.Item || di.ItemType == ListItemType.AlternatingItem)
                        {
                            using (DigiDapter dg = new DigiDapter())
                            {
                                dg.Add("IDRIF", refID);
                                dg.Add("CODE1", ((TextBox)di.FindControl("code1")).Text);
                                dg.Add("CODE2", ((TextBox)di.FindControl("code2")).Text);
                                dg.Add("CODE3", ((TextBox)di.FindControl("code3")).Text);
                                if (((TextBox)e.Item.FindControl("Newavailable")).Text.Length > 0)
                                    dg.Add("QTAAVAILABLE", ((TextBox)e.Item.FindControl("available")).Text);
                                else
                                    dg.Add("QTAAVAILABLE", 0);
                                if (((TextBox)e.Item.FindControl("Newordered")).Text.Length > 0)
                                    dg.Add("QTAORDERED", ((TextBox)e.Item.FindControl("ordered")).Text);
                                else
                                    dg.Add("QTAORDERED", 0);
                                dg.Add("EXPECTEDARRIVAL", ((TextBox)di.FindControl("expectedarrival")).Text);
                                dg.Add("REALARRIVAL", ((TextBox)di.FindControl("realarrival")).Text);
                                dg.Add("STATUS", ((RadioButtonList)di.FindControl("status")).SelectedValue);
                                dg.Execute("WAREHOUSE", "ID=" + ((Literal)di.FindControl("id")).Text);
                            }
                        }
                    }
                    break;
            }
            FillWareHouse();
        }

        void WareHouseEditor_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    RadioButtonList status = (RadioButtonList)e.Item.FindControl("status");
                    status.Items.Add(new ListItem(Root.rm.GetString("Waretxt4"),"0"));
                    status.Items.Add(new ListItem(Root.rm.GetString("Waretxt5"),"1"));
                    status.Items.Add(new ListItem(Root.rm.GetString("Waretxt6"),"2"));
                    status.Items.Add(new ListItem(Root.rm.GetString("Waretxt7"),"3"));
                    status.RepeatColumns = 4;
                    status.RepeatDirection = RepeatDirection.Horizontal;
                    ((Literal)e.Item.FindControl("id")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem,"id"));
                    ((TextBox)e.Item.FindControl("code1")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "code1"));
                    ((TextBox)e.Item.FindControl("code2")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "code2"));
                    ((TextBox)e.Item.FindControl("code3")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "code3"));
                    ((TextBox)e.Item.FindControl("available")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "qtaavailable"));
                    ((TextBox)e.Item.FindControl("ordered")).Text = Convert.ToString(DataBinder.Eval((DataRowView)e.Item.DataItem, "qtaordered"));
                    ((TextBox)e.Item.FindControl("expectedarrival")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView)e.Item.DataItem, "expectedarrival"),UC.myDTFI)).ToShortDateString();
                    ((TextBox)e.Item.FindControl("realarrival")).Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView)e.Item.DataItem, "realarrival"), UC.myDTFI)).ToShortDateString();
                    status.SelectedIndex = Convert.ToInt32(DataBinder.Eval((DataRowView)e.Item.DataItem, "status"));
                    break;
                case ListItemType.Footer:
                    RadioButtonList Newstatus = (RadioButtonList)e.Item.FindControl("Newstatus");
                    ((LinkButton)e.Item.FindControl("Linkbutton1")).Text = "ADD";
                    ((LinkButton)e.Item.FindControl("Linkbutton2")).Text = "REFRESH";
                    Newstatus.Items.Add(new ListItem(Root.rm.GetString("Waretxt4"), "0"));
                    Newstatus.Items.Add(new ListItem(Root.rm.GetString("Waretxt5"), "1"));
                    Newstatus.Items.Add(new ListItem(Root.rm.GetString("Waretxt6"), "2"));
                    Newstatus.Items.Add(new ListItem(Root.rm.GetString("Waretxt7"), "3"));
                    Newstatus.RepeatColumns = 4;
                    Newstatus.RepeatDirection = RepeatDirection.Horizontal;
                    Newstatus.SelectedIndex = 0;
                    break;
            }
        }

        public void FillWareHouse()
        {
            UC = (UserConfig)HttpContext.Current.Session["UserConfig"];
            string query = string.Format(@"SELECT * FROM WAREHOUSE WHERE IDRIF={0}",refID);
            WareHouseEditor.DataSource = DatabaseConnection.CreateDataset(query);
            WareHouseEditor.Columns[0].HeaderText = Root.rm.GetString("Waretxt8");
            WareHouseEditor.Columns[1].HeaderText = Root.rm.GetString("Waretxt9");
            WareHouseEditor.Columns[2].HeaderText = Root.rm.GetString("Waretxt10");
            WareHouseEditor.Columns[3].HeaderText = Root.rm.GetString("Waretxt11");
            WareHouseEditor.Columns[4].HeaderText = Root.rm.GetString("Waretxt12");
            WareHouseEditor.Columns[5].HeaderText = Root.rm.GetString("Waretxt13");
            WareHouseEditor.Columns[6].HeaderText = Root.rm.GetString("Waretxt14");

            WareHouseEditor.DataBind();
        }

        public void SetProductName(string p)
        {
            ProductName.Text = p;
        }

        public long refID
        {
            get
            {
                object o = this.ViewState["_refID" + this.ID];
                if (o == null)
                    return 0;
                else
                    return (long)o;
            }
            set
            {
                this.ViewState["_refID" + this.ID] = value;
            }
        }
    }
}
