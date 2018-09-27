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
using Digita.Tustena.Base;

namespace Digita.Tustena.Catalog
{
    public partial class CatalogListManagment : G
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
                    btnListSave.Text = Root.rm.GetString("Save");
                    FillRepeater();
                }
            }
        }

        private void FillRepeater()
        {
            NewRepeater1.DataSource = DatabaseConnection.CreateDataset("select * from CatalogPriceListDescription").Tables[0];
            NewRepeater1.DataBind();

            ListId.Text = "-1";
            ListDescription.Text = string.Empty;
            ListPercentage.Text = string.Empty;
            ListIncrease.Text = string.Empty;
            ListIncrease.Text = string.Empty;
        }

        protected override void OnInit(EventArgs e)
        {
            InitializeComponents();
            base.OnInit(e);
        }

        private void InitializeComponents()
        {
            this.NewRepeater1.ItemDataBound += new RepeaterItemEventHandler(NewRepeater1_ItemDataBound);
            this.NewRepeater1.ItemCommand += new RepeaterCommandEventHandler(NewRepeater1_ItemCommand);
            this.btnListSave.Click += new EventHandler(btnListSave_Click);
        }

        void btnListSave_Click(object sender, EventArgs e)
        {
            using(DigiDapter dg = new DigiDapter())
            {
                dg.Add("DESCRIPTION", ListDescription.Text);
                dg.Add("PERCENTAGE", (ListPercentage.Text.Length>0)?decimal.Parse(ListPercentage.Text):0);
                dg.Add("INCREASE", (ListIncrease.Text.Length>0)?decimal.Parse(ListIncrease.Text):0);
                dg.Execute("CATALOGPRICELISTDESCRIPTION", "ID=" + ListId.Text);
            }
            FillRepeater();
        }

        void NewRepeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "OpenList":
                    DataRow dr = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRICELISTDESCRIPTION WHERE ID=" + ((Literal)e.Item.FindControl("ListId")).Text).Tables[0].Rows[0];
                    ListId.Text = dr["ID"].ToString();
                    ListDescription.Text = dr["DESCRIPTION"].ToString();
                    ListPercentage.Text = dr["PERCENTAGE"].ToString();
                    ListIncrease.Text = dr["INCREASE"].ToString();
                    break;
                case "MultiDeleteButton":
                    DeleteChecked.MultiDelete(this.NewRepeater1.MultiDeleteListArray, "CATALOGPRICELISTDESCRIPTION");
                    FillRepeater();
                    break;
            }
        }

        void NewRepeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:


                    break;
            }
        }
    }
}
