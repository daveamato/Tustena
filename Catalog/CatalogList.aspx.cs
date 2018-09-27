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
using System.Text;
using Digita.Tustena.Core;
using System.IO;

namespace Digita.Tustena.Catalog
{
    public partial class CatalogList : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                initNewRepeater();
                if (!Page.IsPostBack)
                {
                    CatalogProducts.CreaTree(tvCategoryTreeSearch, 0, true, UC);
                    FindProduct.Text = Root.rm.GetString("Captxt12");
                    FillPriceList(0);
                }
            }
        }

        private void FillPriceList(long pl)
        {
            PriceList.DataValueField = "ID";
            PriceList.DataTextField = "DESCRIPTION";

            PriceList.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CATALOGPRICELISTDESCRIPTION").Tables[0];
            PriceList.DataBind();
            PriceList.Items.Insert(0, new ListItem(Root.rm.GetString("Captxt37"), "0"));

            string currentlist = pl.ToString();
            if (pl == 0)
                currentlist = DatabaseConnection.SqlScalar("SELECT LISTPRICE FROM ACCOUNT WHERE UID=" + UC.UserId);

            PriceList.SelectedIndex = 0;
            if (currentlist.Length > 0)
            {
                PriceList.SelectedIndex = -1;
                foreach (ListItem li in PriceList.Items)
                {
                    if (li.Value == currentlist)
                    {
                        li.Selected = true;
                        PriceList.Enabled = false;
                        break;
                    }
                }
                if (PriceList.SelectedIndex == -1)
                {
                    PriceList.SelectedIndex = 0;
                }
            }
            else
                PriceList.SelectedIndex = 0;

        }

        private void initNewRepeater()
        {
            this.Repeater1.UsePagedDataSource = true;
            this.Repeater1.PageSize = UC.PagingSize;
        }

        private void FillCatLabel(int id, ref string c)
        {
            DataTable catname = DatabaseConnection.CreateDataset("SELECT PARENTID,DESCRIPTION FROM CATALOGCATEGORIES WHERE ID=" + id).Tables[0];
            if (catname.Rows.Count > 0)
            {
                DataRow cc = catname.Rows[0];

                c = cc[1].ToString() + "-->" + c;
                if (cc[0].ToString() != "0")
                    FillCatLabel(int.Parse(cc[0].ToString()), ref c);
            }
            else
                c = String.Empty;
        }

        private void FillQueryCategories(int id, ref string c)
        {
            c += "CATEGORY=" + id + " OR ";
            DataTable cat = DatabaseConnection.CreateDataset("SELECT ID FROM CATALOGCATEGORIES WHERE PARENTID=" + id).Tables[0];
            if (cat.Rows.Count > 0)
            {
                foreach (DataRow d in cat.Rows)
                {
                    FillQueryCategories(int.Parse(d[0].ToString()), ref c);
                }
            }
        }

        protected void FindProduct_Click(object sender, EventArgs e)
        {
            string search = DatabaseConnection.FilterInjection(Search.Text);
            StringBuilder q = new StringBuilder();

            if (FindCatID.Text.Length > 0)
            {
                string c = String.Empty;
                FillQueryCategories(int.Parse(FindCatID.Text), ref c);
                c = c.Substring(0, c.Length - 3);
                q.AppendFormat("SELECT * FROM CATALOGPRODUCTS WHERE ({0}) AND (", c);
                q.AppendFormat("SHORTDESCRIPTION LIKE '{0}%' OR ", search);
                q.AppendFormat("LONGDESCRIPTION LIKE '{0}%' OR ", search);
                q.AppendFormat("CODE LIKE '{0}%') ", search);
				q.Append("ORDER BY CATEGORY");

            }
            else
            {
                q.AppendFormat("SELECT * FROM CATALOGPRODUCTS WHERE (");
                q.AppendFormat("SHORTDESCRIPTION LIKE '{0}%' OR ", search);
                q.AppendFormat("LONGDESCRIPTION LIKE '{0}%' OR ", search);
                q.AppendFormat("CODE LIKE '{0}%') ", search);
				q.Append("ORDER BY CATEGORY");

            }

            this.Repeater1.sqlDataSource = q.ToString();
            this.Repeater1.DataBind();
        }

        #region Codice generato da Progettazione Web Form

        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();
            base.OnInit(e);
        }

        private void InitializeComponent()
        {
            this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
            this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(Repeater1_ItemCommand);
        }

        void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Down":
                    Literal FileId = (Literal)e.Item.FindControl("LblID");
                    DataSet ds = DatabaseConnection.CreateDataset("SELECT DOCUMENT FROM CATALOGPRODUCTS WHERE ID=" + int.Parse(FileId.Text));

                    string filename;
					filename = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + "Catalog" + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["Document"].ToString();
                    string realFileName = "ProductInfo" + Path.GetExtension(filename);

                    string downFile = filename;

                    if (File.Exists(downFile))
                    {
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
                        Response.ContentType = "application/octet-stream";
                        Response.TransmitFile(downFile);
                        Response.Flush();
                        Response.End();
                        return;

                    }
                    else if (File.Exists(filename))
                    {
                        File.Move(filename, downFile);
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
                        Response.ContentType = "application/octet-stream";
                        Response.TransmitFile(downFile);
                        Response.Flush();
                        Response.End();
                        return;
                    }
                    else
                    {
                        G.SendError("File lost", downFile);
                    }
                    break;
            }
        }

        void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    Label LblCategory = (Label)e.Item.FindControl("LblCategory");
                    int cat = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Category"));
                    string c = String.Empty;
                    FillCatLabel(cat, ref c);
                    if (c.Length > 3)
                        LblCategory.Text = c.Substring(0, c.Length - 3);
                    else
                        LblCategory.Text = c;
                    if (!(bool)DataBinder.Eval(e.Item.DataItem, "Active"))
                    {
                        LblCategory.CssClass = "LinethroughGray";
                        ((Label)e.Item.FindControl("LblCode")).CssClass = "LinethroughGray";
                        ((Label)e.Item.FindControl("LblProduct")).CssClass = "LinethroughGray";
                        ((Label)e.Item.FindControl("LblPrice")).CssClass = "LinethroughGray";
                    }


                    LinkButton Down = (LinkButton)e.Item.FindControl("Down");
                    Down.Visible = false;
                    Down.ToolTip = Root.rm.GetString("Captxt25");
                    if ((DataBinder.Eval((DataRowView)e.Item.DataItem, "Document")).ToString().Length > 0)
                    {
                        string pathTemplate;
						pathTemplate = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "Catalog";
                        if (File.Exists(Path.Combine(pathTemplate, (DataBinder.Eval((DataRowView)e.Item.DataItem, "Document")).ToString())))
                        {
                            Down.Visible = true;
                        }
                    }

                    if (PriceList.SelectedValue != "0")
                    {
                        DataTable dtprice = DatabaseConnection.CreateDataset(string.Format("select * from CatalogProductPrice where productid={0} and listid={1}", Convert.ToString(DataBinder.Eval(e.Item.DataItem, "ID")), PriceList.SelectedValue)).Tables[0];
                        if (dtprice.Rows.Count > 0)
                        {
                            ((Label)e.Item.FindControl("LblPrice")).Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtprice.Rows[0]["UnitPrice"]), 2));
                            ((Label)e.Item.FindControl("LblCost")).Text = Convert.ToString(Math.Round(Convert.ToDecimal(dtprice.Rows[0]["Cost"]), 2));
                        }
                        else
                        {
                            DataTable lp = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRICELISTDESCRIPTION WHERE ID=" + PriceList.SelectedValue).Tables[0];
                            if (lp.Rows.Count > 0)
                            {
                                decimal unitprice = Convert.ToDecimal(((Label)e.Item.FindControl("LblPrice")).Text);
                                unitprice = unitprice + (unitprice * Convert.ToDecimal(lp.Rows[0]["INCREASE"]) / 100);
                                ((Label)e.Item.FindControl("LblPrice")).Text = Convert.ToString(Math.Round(unitprice, 2));
                                ((Label)e.Item.FindControl("LblCost")).Text = Convert.ToString(Math.Round((unitprice - (unitprice * Convert.ToDecimal(lp.Rows[0]["PERCENTAGE"]) / 100)), 2));
                            }
                        }
                    }

                    break;
            }
        }

        #endregion
    }
}
