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
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Brettle.Web.NeatUpload;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Microsoft.Web.UI.WebControls;
using TreeView = Microsoft.Web.UI.WebControls.TreeView;
using TreeNode = Microsoft.Web.UI.WebControls.TreeNode;

namespace Digita.Tustena.Catalog
{
	public partial class CatalogProducts : G
	{

		protected TextBox TxtVat;

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
					GraphStart();
					GraphResult.Visible = true;
					this.Repeaterpaging1.Visible=false;

					CreaTree(tvCategoryTree, 0, UC);
					CreaTree(tvCategoryTreeSearch, 0, true, UC);
					FindProduct.Text =Root.rm.GetString("Captxt12");
					AddProduct.Text =Root.rm.GetString("Captxt13");

					BtnSubmit.Text =Root.rm.GetString("Captxt14");
					BtnSubmit.Attributes.Add("onclick","ShowProgressBar()");
                    Tabber.Visible = false;


					RadioActive.Items.Add(new ListItem(Root.rm.GetString("Captxt19"), "1"));
					RadioActive.Items.Add(new ListItem(Root.rm.GetString("Captxt20"), "0"));
					RadioPublish.Items.Add(new ListItem(Root.rm.GetString("Captxt19"), "1"));
					RadioPublish.Items.Add(new ListItem(Root.rm.GetString("Captxt20"), "0"));

					CurrentCurrency.Text = DatabaseConnection.SqlScalar("sELECT CURRENCYSYMBOL FROM CURRENCYTABLE WHERE CHANGETOEURO=1 AND CHANGEFROMEURO=1");
					CurrentCurrency2.Text=CurrentCurrency.Text;
					listVat.DataSource= DatabaseConnection.CreateDataset("SELECT TAXVALUE,TAXDESCRIPTION FROM TAXVALUES").Tables[0];

					listVat.DataTextField="taxdescription";
					listVat.DataValueField="taxvalue";
					listVat.DataBind();
					listVat.Items.Insert(0,new ListItem(Root.rm.GetString("Choose"),"0"));
				}
				else
				{
					GraphResult.Visible = false;
				}
			}
		}

		private void GraphStart()
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



        public static void CreaTree(Microsoft.Web.UI.WebControls.TreeView tree, int open, UserConfig uc)
		{
			CreaTree(tree, open, false, uc);
		}

        public static void CreaTree(Microsoft.Web.UI.WebControls.TreeView tree, int open, bool search, UserConfig uc)
		{

			string queryCat;
			queryCat = "SELECT * FROM CATALOGCATEGORIES WHERE PARENTID = 0";
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);

			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
				if (search)
					tv.Text = "<a href=\"javascript:copyDataS('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				else
					tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				tv.NodeData = dr["Id"].ToString();

				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open, search, uc); // Chiamata ricorsiva per fare le foglie

				tree.Nodes.Add(tv);
			}
			tree.CssClass = "normal";

		}

		public static bool FillCategoryTree(int parent, TreeNode tvUp, int open, bool search, UserConfig uc)
		{
			string queryCat;
			queryCat = "SELECT * FROM CATALOGCATEGORIES WHERE PARENTID = " + parent;
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			bool toExpand = false;
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
				if (search)
					tv.Text = "<a href=\"javascript:copyDataS('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				else
					tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				tv.NodeData = dr["Id"].ToString();
				if (!toExpand)
					toExpand = (open == Convert.ToInt32(dr["id"]));
				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open, search, uc); // Chiamata ricorsiva per fare le foglie
				if (tv.Expanded) toExpand = true;
				tvUp.Nodes.Add(tv);
			}
			return toExpand;
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.FindProduct.Click += new EventHandler(this.FindProduct_Click);
			this.AddProduct.Click += new EventHandler(this.AddProduct_Click);
			this.BtnSubmit.Click += new EventHandler(this.btnSubmit_Click);
            this.RepOtherList.ItemDataBound += new RepeaterItemEventHandler(RepOtherList_ItemDataBound);
			this.ProductRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ProductRepeater_ItemDataBound);
			this.ProductRepeater.ItemCommand += new RepeaterCommandEventHandler(this.ProductRepeater_ItemCommand);
			this.Load += new EventHandler(this.Page_Load);
		}

        void RepOtherList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    DropDownList ListlistVat = (DropDownList)e.Item.FindControl("ListlistVat");
					ListlistVat.DataSource= DatabaseConnection.CreateDataset("SELECT TAXVALUE,TAXDESCRIPTION FROM TAXVALUES").Tables[0];
                    ListlistVat.DataTextField = "taxdescription";
                    ListlistVat.DataValueField = "taxvalue";
                    ListlistVat.DataBind();
                    ListlistVat.Items.Insert(0, new ListItem(Root.rm.GetString("Choose"), "0"));

                    CheckBox chkListPrice = (CheckBox)e.Item.FindControl("chkListPrice");
                    chkListPrice.Attributes.Add("onclick", "EnableDisableList(this)");
                    CheckBox chkListEnable = (CheckBox)e.Item.FindControl("chkListEnable");
                    chkListEnable.Checked = true;
                    Literal ListId = (Literal)e.Item.FindControl("ListId");
                    TextBox ListUnitPrice = (TextBox)e.Item.FindControl("ListUnitPrice");
                    TextBox ListCost = (TextBox)e.Item.FindControl("ListCost");
                    if (litExcludeList.Text.IndexOf("|" + ListId.Text + "|") < 0)
                    {

                        DataTable dt = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM CATALOGPRODUCTPRICE WHERE LISTID={0} AND PRODUCTID={1}",ListId.Text, TxtId.Text)).Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            chkListPrice.Checked = false;
                            ListUnitPrice.ReadOnly = false;
                            ListUnitPrice.Text = Convert.ToDecimal(dt.Rows[0]["UnitPrice"]).ToString();
                            ListlistVat.SelectedIndex = -1;
                            foreach (ListItem li in ListlistVat.Items)
                            {
                                if (li.Value == dt.Rows[0]["Vat"].ToString())
                                {
                                    li.Selected = true;
                                    break;
                                }
                            }
                            ListlistVat.Enabled = true;
                            ListCost.ReadOnly = false;
                            ListCost.Text = Convert.ToDecimal(dt.Rows[0]["Cost"]).ToString();
                        }
                        else
                        {
                            ListlistVat.Enabled = false;
                            ListCost.ReadOnly = true;
                            ListUnitPrice.ReadOnly = true;
                        }
                    }
                    else
                    {
                        chkListEnable.Checked = false;
                        ListlistVat.Enabled = false;
                        ListCost.ReadOnly = true;
                        ListUnitPrice.ReadOnly = true;
                    }

                    break;
            }
        }

		#endregion

		private void ProductRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label LblCategory = (Label) e.Item.FindControl("LblCategory");
					int cat = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "Category"));
					string c = String.Empty;
					FillCatLabel(cat, ref c);
					if (c.Length > 3)
						LblCategory.Text = c.Substring(0, c.Length - 3);
					else
						LblCategory.Text = c;
					if (!(bool) DataBinder.Eval(e.Item.DataItem, "Active"))
					{
						LblCategory.CssClass = "LinethroughGray";
						((Label) e.Item.FindControl("LblCode")).CssClass = "LinethroughGray";
						((LinkButton) e.Item.FindControl("BtnProduct")).CssClass = "LinethroughGray";
					}

					LinkButton del = (LinkButton) e.Item.FindControl("DelProduct");
					if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_BILLROWS WHERE PRODUCTID=" + int.Parse(((Literal) e.Item.FindControl("LblID")).Text))) > 0)
					{
						del.Visible = false;
					}
					else
					{
						del.Visible = true;
						del.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Captxt22") + "');");
					}
					LinkButton Down = (LinkButton) e.Item.FindControl("Down");
					Down.Visible=false;
					Down.ToolTip=Root.rm.GetString("Captxt25");
					if((DataBinder.Eval((DataRowView) e.Item.DataItem, "Document")).ToString().Length>0)
					{
						string pathTemplate;
						pathTemplate = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "Catalog";
						if(File.Exists(Path.Combine(pathTemplate,(DataBinder.Eval((DataRowView) e.Item.DataItem, "Document")).ToString())))
						{
							Down.Visible=true;
						}
					}


					break;
			}
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

		private void FindProduct_Click(object sender, EventArgs e)
		{
			FindProductQuery();
		}

		private void FindProductQuery()
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

			Repeaterpaging1.PageSize = UC.PagingSize;
			Repeaterpaging1.RepeaterObj = ProductRepeater;
			Repeaterpaging1.sqlRepeater = q.ToString();
			Repeaterpaging1.CurrentPage=0;
			Repeaterpaging1.BuildGrid();

			if (ProductRepeater.Items.Count > 0)
			{
				ProductRepeater.Visible = true;
			}
			else
			{
				ProductRepeater.Visible = false;
			}
            Tabber.Visible = false;

		}

		private void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Down":
					Literal FileId = (Literal) e.Item.FindControl("LblID");
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
				case "DelProduct":
					DatabaseConnection.DoCommand("dELETE FROM CATALOGPRODUCTS WHERE ID=" + int.Parse(((Literal) e.Item.FindControl("LblID")).Text));
					FindProductQuery();
					break;
				case "BtnProduct":
					InitProgressBar();
					LblInfo.Text = String.Empty;
					DataTable d = DatabaseConnection.CreateDataset("SELECT * FROM CATALOGPRODUCTS WHERE ID=" + int.Parse(((Literal) e.Item.FindControl("LblID")).Text)).Tables[0];
					foreach (DataColumn cc in d.Columns)
					{
						switch (cc.ColumnName.ToLower())
						{
							case "category":
								TxtIdCategory.Text = d.Rows[0][cc.ColumnName].ToString();
								TxtTextCategory.Text = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM CATALOGCATEGORIES WHERE ID=" + d.Rows[0][cc.ColumnName].ToString());
								break;
							case "printdescription":
								this.chkPrint.Checked=(bool) d.Rows[0][cc.ColumnName];
								break;
							case "active":
								if ((bool) d.Rows[0][cc.ColumnName])
								{
									RadioActive.Items[0].Selected = true;
								}
								else
								{
									RadioActive.Items[1].Selected = true;
								}
								break;
							case "publish":
								if ((bool) d.Rows[0][cc.ColumnName])
								{
									RadioPublish.Items[0].Selected = true;
								}
								else
								{
									RadioPublish.Items[1].Selected = true;
								}
								break;
							case "unitprice":
								try
								{
									((TextBox) ProductTable.FindControl("txt" + cc.ColumnName)).Text = Convert.ToDecimal(d.Rows[0][cc.ColumnName]).ToString();
								}
								catch
								{
								}
								break;
							case "image":
								if(d.Rows[0][cc.ColumnName].ToString().Length>0)
								{
									this.ViewImage.Text="<img src=/i/PhotoProduct.gif border=0 style='cursor:pointer'>";
									this.ViewImage.Attributes.Add("onclick","NewWindow('/imageRepath.aspx/Catalog/"+d.Rows[0][cc.ColumnName].ToString()+"','PrintProduct',400,400,'no')");
								}
								break;
							case "vat":
								try
								{
									listVat.SelectedIndex=-1;
									foreach(ListItem li in listVat.Items)
									{
										if(Convert.ToDecimal(li.Value)==Convert.ToDecimal(d.Rows[0][cc.ColumnName].ToString()))
										{
											li.Selected=true;
											break;
										}
									}
								}
								catch
								{
									listVat.SelectedIndex=0;
								}
								break;
                            case "priceexpire":
                                if (d.Rows[0][cc.ColumnName] != DBNull.Value)
                                    txtPriceExpire.Text = UC.LTZ.ToLocalTime((DateTime)d.Rows[0][cc.ColumnName]).ToShortDateString();
                                break;
                            case "excludelist":
                                 if (d.Rows[0][cc.ColumnName] != DBNull.Value)
                                    litExcludeList.Text=d.Rows[0][cc.ColumnName].ToString();
                                break;
							default:
								try
								{
									((TextBox) ProductTable.FindControl("txt" + cc.ColumnName)).Text = d.Rows[0][cc.ColumnName].ToString();
								}
								catch
								{
								}
								break;
						}
					}

                    FillOtherList();


                    Tabber.Visible = true;
					ProductRepeater.Visible = false;
					break;
			}
		}

        private void FillOtherList()
        {
                        RepOtherList.DataSource = DatabaseConnection.CreateDataset("select * from CatalogPriceListDescription").Tables[0];
            RepOtherList.DataBind();
        }

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			SaveProduct();

		}

		private void SaveProduct()
		{
			if (Page.IsValid)
			{

				{
					long id = long.Parse(((TextBox) ProductTable.FindControl("TxtId")).Text);
					string sqlString = "SELECT ID FROM CATALOGPRODUCTS WHERE ID = " + id;
					using (DigiDapter dg = new DigiDapter(sqlString))
					{
						if (!dg.HasRows)
						{
						}
						dg.Add("CATEGORY", ((TextBox) ProductTable.FindControl("TxtIdCategory")).Text);
						dg.Add("CODE", ((TextBox) ProductTable.FindControl("TxtCode")).Text);
						dg.Add("SHORTDESCRIPTION", ((TextBox) ProductTable.FindControl("TxtShortDescription")).Text);
						dg.Add("LONGDESCRIPTION", ((TextBox) ProductTable.FindControl("TxtLongDescription")).Text);
						dg.Add("UNIT", ((TextBox) ProductTable.FindControl("TxtUnit")).Text);
                        dg.Add("QTA", StaticFunctions.FixDecimal(((TextBox)ProductTable.FindControl("TxtQta")).Text));
						if (((TextBox) ProductTable.FindControl("TxtQtaBlister")).Text.Length > 0)
                            dg.Add("QTABLISTER", StaticFunctions.FixDecimal(((TextBox)ProductTable.FindControl("TxtQtaBlister")).Text));
						dg.Add("UNITPRICE", StaticFunctions.FixDecimal(((TextBox) ProductTable.FindControl("TxtUnitPrice")).Text));
						if (listVat.SelectedIndex > 0)
							dg.Add("VAT", StaticFunctions.FixDecimal(listVat.SelectedValue));

						if (((TextBox) ProductTable.FindControl("TxtCost")).Text.Length > 0)
							dg.Add("COST", StaticFunctions.FixDecimal(((TextBox) ProductTable.FindControl("TxtCost")).Text));
						dg.Add("ACTIVE", (RadioActive.SelectedValue == "1"));
						dg.Add("PUBLISH", (RadioPublish.SelectedValue == "1"));
						dg.Add("PRINTDESCRIPTION", this.chkPrint.Checked);

                        if (txtPriceExpire.Text.Length > 0)
                            dg.Add("PriceExpire", UC.LTZ.ToUniversalTime(Convert.ToDateTime(txtPriceExpire.Text)));
                        else
                            dg.Add("PriceExpire", DBNull.Value);

                        dg.Add("Stock", StaticFunctions.FixDecimal(txtStock.Text));

						Guid gProductImage = Guid.NewGuid();
						Guid gProductDocument = Guid.NewGuid();
						string newFileNameImage=string.Empty;
						string newFileNameDocument=string.Empty;
						if(this.ProductImage.HasFile)
						{
							newFileNameImage = gProductImage.ToString()+ Path.GetExtension(ProductImage.FileName);
							dg.Add("IMAGE", newFileNameImage);
						}
						if(this.ProductDocument.HasFile)
						{
							newFileNameDocument = gProductDocument.ToString()+ Path.GetExtension(ProductDocument.FileName);
							dg.Add("DOCUMENT", newFileNameDocument);
						}

						if(newFileNameImage.Length>0 || newFileNameDocument.Length>0)
						{
								if (FileFunctions.CheckDir(ConfigSettings.DataStoragePath, true))
								{
									string pathTemplate;
									pathTemplate = ConfigSettings.DataStoragePath+Path.DirectorySeparatorChar + "catalog";

									FileFunctions.CheckDir(pathTemplate, true);
									if(this.ProductImage.FileName!=null && this.ProductImage.FileContent!=null)
									{
										this.ProductImage.FileContent.Close();
										this.ProductImage.MoveTo(Path.Combine(pathTemplate, newFileNameImage), MoveToOptions.Overwrite);
									}
									if(this.ProductDocument.FileName!=null && this.ProductDocument.FileContent!=null)
									{
										this.ProductDocument.FileContent.Close();
										this.ProductDocument.MoveTo(Path.Combine(pathTemplate, newFileNameDocument), MoveToOptions.Overwrite);
									}
									InitProgressBar();
								}
							}

						object newid = dg.Execute("CatalogProducts", "id=" + id,DigiDapter.Identities.Identity);

                        if (dg.RecordInserted)
                            id = Convert.ToInt64(newid);

                        if (RepOtherList.Items.Count > 0)
                        {
                            string excludelist = "|";
                            foreach (RepeaterItem ri in RepOtherList.Items)
                            {
                                if (!((CheckBox)ri.FindControl("chkListEnable")).Checked)
                                {
                                    excludelist += ((Literal)ri.FindControl("ListId")).Text + "|";
                                }
                                else
                                {
                                    if (!((CheckBox)ri.FindControl("chkListPrice")).Checked)
                                    {
                                        using (DigiDapter dglist = new DigiDapter())
                                        {
                                            dglist.Add("PRODUCTID", id);
                                            dglist.Add("LISTID", ((Literal)ri.FindControl("ListId")).Text);
                                            dglist.Add("UNITPRICE", StaticFunctions.FixDecimal(((TextBox)ri.FindControl("ListUnitPrice")).Text));
                                            if (((DropDownList)ri.FindControl("ListlistVat")).SelectedIndex > 0)
                                                dglist.Add("VAT", StaticFunctions.FixDecimal(((DropDownList)ri.FindControl("ListlistVat")).SelectedValue));
                                            if (((TextBox)ri.FindControl("ListCost")).Text.Length > 0)
                                                dglist.Add("COST", StaticFunctions.FixDecimal(((TextBox)ri.FindControl("ListCost")).Text));

                                            dglist.Execute("CATALOGPRODUCTPRICE", "PRODUCTID=" + id + " AND LISTID=" + ((Literal)ri.FindControl("ListId")).Text);
                                        }
                                    }
                                    else
                                        DatabaseConnection.DoCommand("DELETE FROM CATALOGPRODUCTPRICE WHERE PRODUCTID=" + id + " AND LISTID=" + ((Literal)ri.FindControl("ListId")).Text);
                                }
                            }

                            if (excludelist.Length <= 1)
                                excludelist = string.Empty;

                            DatabaseConnection.DoCommand(string.Format("UPDATE CATALOGPRODUCTS SET EXCLUDELIST='{0}' WHERE ID={1}",excludelist,id));
                        }
					}

					LblInfo.Text = string.Format(Root.rm.GetString("Captxt15"), ((TextBox) ProductTable.FindControl("TxtShortDescription")).Text);
					EraseForm();
				}


			}
			else
			{
				LblInfo.Text =Root.rm.GetString("Captxt17");
			}
		}

		private void AddProduct_Click(object sender, EventArgs e)
		{
			EraseForm();
			LblInfo.Text = String.Empty;
			InitProgressBar();
		}

		private void InitProgressBar()
		{
		}

		private void EraseForm()
		{
			TxtId.Text = "-1";
			TxtIdCategory.Text = String.Empty;
			TxtTextCategory.Text = String.Empty;
			TxtCode.Text = String.Empty;
			TxtShortDescription.Text = String.Empty;
			TxtLongDescription.Text = String.Empty;
			TxtUnit.Text = String.Empty;
			TxtQta.Text = "1";
			TxtQtaBlister.Text = String.Empty;
			TxtUnitPrice.Text = String.Empty;
			listVat.SelectedIndex = 0;
			TxtCost.Text = String.Empty;

			RadioActive.Items[0].Selected = true;
			RadioPublish.Items[0].Selected = true;
            Tabber.Visible = true;
			ProductRepeater.Visible = false;
            litExcludeList.Text = string.Empty;

            FillOtherList();
		}
	}
}
