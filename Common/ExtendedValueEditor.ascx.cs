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
using System.Globalization;
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class ExtendedValueEditor : UserControl
	{
		private UserConfig UC = new UserConfig();


		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];


		public bool Grouped
		{
			get
			{
				object o = this.ViewState["_Grouped" + this.ID];
				if (o == null)
					return false;
				else
					return (bool)o;
			}
			set
			{
				this.ViewState["_Grouped" + this.ID] = value;
			}
		}

		public string KId
		{
			get
			{
				object o = this.ViewState["_KId" + this.ID];
				if (o == null)
					return string.Empty;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_KId" + this.ID] = value;
			}
		}

		public bool Multiline
		{
			get
			{
				object o = this.ViewState["_Multiline" + this.ID];
				if (o == null)
					return false;
				else
					return (bool)o;
			}
			set
			{
				this.ViewState["_Multiline" + this.ID] = value;
			}
		}

		public string Lang
		{
			get
			{
				object o = this.ViewState["_Lang" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_Lang" + this.ID] = value;
			}
		}

		public string DataValueField
		{
			get
			{
				object o = this.ViewState["_DataValueField" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_DataValueField" + this.ID] = value;
			}
		}

		public FieldType DataValueFieldType
		{
			get
			{
				object o = this.ViewState["_DataValueFieldType" + this.ID];
				if (o == null)
					return FieldType.varchar;
				else
					return (FieldType)o;
			}
			set
			{
				this.ViewState["_DataValueFieldType" + this.ID] = value;
			}
		}

		public string DataValueField2
		{
			get
			{
				object o = this.ViewState["_DataValueField2" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_DataValueField2" + this.ID] = value;
			}
		}

		public FieldType DataValueFieldType2
		{
			get
			{
				object o = this.ViewState["_DataValueFieldType2" + this.ID];
				if (o == null)
					return FieldType.varchar;
				else
					return (FieldType)o;
			}
			set
			{
				this.ViewState["_DataValueFieldType2" + this.ID] = value;
			}
		}

		public string[] DataValueText
		{
			get
			{
				object o = this.ViewState["_DataValueText" + this.ID];
				if (o == null)
					return null;
				else
					return (string[])o;
			}
			set
			{
				this.ViewState["_DataValueText" + this.ID] = value;
			}
		}

		public string TableName
		{
			get
			{
				object o = this.ViewState["_TableName" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_TableName" + this.ID] = value;
			}
		}

		public string CrossId
		{
			get
			{
				object o = this.ViewState["_CrossId" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_CrossId" + this.ID] = value;
			}
		}

        public int MaxSizeField1
        {
            get
            {
                object o = this.ViewState["_MaxSizeField1" + this.ID];
                if (o == null)
                    return 200;
                else
                    return (int)(o);
            }
            set
            {
                this.ViewState["_MaxSizeField1" + this.ID] = value;
            }
        }

        public int MaxSizeField2
        {
            get
            {
                object o = this.ViewState["_MaxSizeField2" + this.ID];
                if (o == null)
                    return 200;
                else
                    return (int)(o);
            }
            set
            {
                this.ViewState["_MaxSizeField2" + this.ID] = value;
            }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			updBoatType.Text=Root.rm.GetString("SaveAll");
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];
		}

		protected override void OnPreRender(EventArgs e)
		{
			if(Page.IsPostBack)
            {
				ReloadDataSet();
			}
			base.OnPreRender (e);
		}


		private void FillDropCulture(DropDownList MyUICulture)
		{
				foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
					if (ConfigSettings.SupportedLanguages.IndexOf(ci.Parent.Name) > -1)
					{
						bool notExists = true;
						foreach (ListItem i in MyUICulture.Items)
						{
							if(i.Value == ci.Parent.Name)
							{
								notExists = false;
								break;
							}
						}
                        if (notExists && ci.Parent.Name.Length >= 2)
							MyUICulture.Items.Add(new ListItem(ci.TwoLetterISOLanguageName, ci.Parent.Name.Substring(0, 2)));
					}
				}



				MyUICulture.SelectedValue = UC.Culture.Substring(0, 2);
		}

		public DataSet ReloadDataSet()
		{
			return ReloadDataSet("0");
		}
		public DataSet ReloadDataSet(string level)
		{
			string lang = (this.Lang!=null && this.Lang.Length>0)?this.Lang:"''";
			if(this.DataValueField2.Length>0){
				if(this.KId.Length>0 && this.Grouped && level!="0")
					return DatabaseConnection.CreateDataset(string.Format("SELECT ID,{0},{1},{2},{4},VIEWORDER FROM {3} WHERE {0}={5} ORDER BY VIEWORDER",this.KId,this.DataValueField,this.DataValueField2,this.TableName,lang,level));
				else
					return DatabaseConnection.CreateDataset(string.Format("SELECT ID,{0},{1},{3},VIEWORDER FROM {2} ORDER BY VIEWORDER",this.DataValueField,this.DataValueField2,this.TableName,lang));
			}
			else
			{
				if(this.KId.Length>0 && this.Grouped && level!="0")
					return DatabaseConnection.CreateDataset(string.Format("SELECT ID,{0},{1},{3},VIEWORDER FROM {2} WHERE {0}={4} ORDER BY VIEWORDER",this.KId,this.DataValueField,this.TableName,lang,level));
				else
					return DatabaseConnection.CreateDataset(string.Format("SELECT ID,{0},{2},VIEWORDER FROM {1} ORDER BY VIEWORDER",this.DataValueField,this.TableName,lang));

			}
		}


		public void LoadGrid()
		{
			dgValueEditor.DataSource = ReloadDataSet();
			dgValueEditor.Columns[0].HeaderText = this.DataValueText[0];

			if(this.DataValueField2.Length>0)
			{
				dgValueEditor.Columns[1].HeaderText = this.DataValueText[1];
				dgValueEditor.Columns[1].Visible=true;
			}
			else
				dgValueEditor.Columns[1].Visible=false;

			if(this.Lang.Length>0)
			{
				dgValueEditor.Columns[2].HeaderText = "Lang";
				dgValueEditor.Columns[2].Visible=true;
			}else
				dgValueEditor.Columns[2].Visible=false;

			if(this.Grouped)
			{
				dgValueEditor.Columns[5].Visible=true;
				dgValueEditor.Columns[5].HeaderText = "Other Languages";
			}else
				dgValueEditor.Columns[5].Visible=false;


			dgValueEditor.Columns[3].HeaderText =Root.rm.GetString("Fretxt20");
			dgValueEditor.Columns[4].HeaderText =Root.rm.GetString("Delete");

			dgValueEditor.DataBind();
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.dgValueEditor.ItemCommand += new DataGridCommandEventHandler(this.BoatType_ItemCommand);
			this.dgValueEditor.ItemDataBound +=new DataGridItemEventHandler(dgValueEditor_ItemDataBound);
			this.Load += new EventHandler(this.Page_Load);
			this.updBoatType.Click +=new EventHandler(updBoatType_Click);

		}
		#endregion

		private void BoatType_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			bool toupdate=true;
			if (e.CommandName == "Add")
			{
				using(DigiDapter dg=new DigiDapter())
				{
					object field1=new object();
					switch(this.DataValueFieldType)
					{
						case FieldType.numeric:
							try
							{
								field1=Convert.ToDecimal(((TextBox)e.Item.FindControl("Newtype")).Text);
							}
							catch
							{
								toupdate=false;
							}
							break;
						case FieldType.varchar:
							try
							{
								field1=((TextBox)e.Item.FindControl("Newtype")).Text;
							}
							catch
							{
								toupdate=false;
							}
							break;
					}
					dg.Add(this.DataValueField, field1);
					if(this.DataValueField2.Length>0)
					{
						object field2=new object();
						switch(this.DataValueFieldType2)
						{
							case FieldType.numeric:
								try
								{
									field2=Convert.ToDecimal(((TextBox)e.Item.FindControl("Newtype2")).Text);
								}
								catch
								{
									toupdate=false;
								}
								break;
							case FieldType.varchar:
								try
								{
									field2=((TextBox)e.Item.FindControl("Newtype2")).Text;
								}
								catch
								{
									toupdate=false;
								}
								break;
						}
						dg.Add(this.DataValueField2, field2);
					}
					if(this.Lang.Length>0)
					{
						dg.Add(this.Lang, ((DropDownList)e.Item.FindControl("NewMyUICulture")).SelectedValue);
					}
					dg.Add("VIEWORDER", (((TextBox)e.Item.FindControl("newvieworder")).Text.Length>0)?((TextBox)e.Item.FindControl("newvieworder")).Text:"0");
					if(toupdate)
						dg.Execute(this.TableName);
				}

			}

			if(toupdate)
				LoadGrid();
			else
                Page.ClientScript.RegisterStartupScript(this.GetType(), "attention", "<script>alert('" + Root.rm.GetString("ErpConftxt9") + "');</script>");
		}



		private void updBoatType_Click(object sender, EventArgs e)
		{
			foreach (DataGridItem di in dgValueEditor.Items)
			{
				if (di.ItemType == ListItemType.Item || di.ItemType == ListItemType.AlternatingItem)
				{
					if (((CheckBox)di.FindControl("chkDelete")).Checked)
					{
						DatabaseConnection.DoCommand(string.Format("DELETE FROM {0} WHERE ID={1}",this.TableName,((Literal)di.FindControl("rowid")).Text));
					}
					else
					{
						using(DigiDapter dg=new DigiDapter())
						{
							object field1=new object();
							switch(this.DataValueFieldType)
							{
								case FieldType.numeric:
									field1=Convert.ToDecimal(((TextBox)di.FindControl("type")).Text);
									break;
								case FieldType.varchar:
									field1=((TextBox)di.FindControl("type")).Text;
									break;
							}

							dg.Add(this.DataValueField, field1);
							if(this.DataValueField2.Length>0)
							{
								object field2=new object();
								switch(this.DataValueFieldType2)
								{
									case FieldType.numeric:
										field2=Convert.ToDecimal(((TextBox)di.FindControl("type2")).Text);
										break;
									case FieldType.varchar:
										field2=((TextBox)di.FindControl("type2")).Text;
										break;
								}
								dg.Add(this.DataValueField2, field2);
							}
							if(this.Lang.Length>0)
							{
								dg.Add(this.Lang, ((DropDownList)di.FindControl("MyUICulture")).SelectedValue);
							}
							dg.Add("VIEWORDER", (((TextBox)di.FindControl("vieworder")).Text.Length>0)?((TextBox)di.FindControl("vieworder")).Text:"0");
							dg.Execute(this.TableName,"id="+((Literal)di.FindControl("rowid")).Text);
						}

					}
				}
			}

			LoadGrid();
		}

		private void dgValueEditor_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					TextBox type = (TextBox) e.Item.FindControl("type");
					type.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.DataValueField));
                    type.MaxLength = MaxSizeField1;


					if(this.DataValueField2.Length>0)
					{
						TextBox type2 = (TextBox) e.Item.FindControl("type2");
						type2.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.DataValueField2));
                        type2.MaxLength = MaxSizeField2;
                        if (this.Multiline)
						{
							type2.TextMode=TextBoxMode.MultiLine;
							type2.Height = Unit.Pixel(50);
						}
					}
					if(this.Lang.Length>0)
					{
						DropDownList MyUICulture = (DropDownList) e.Item.FindControl("MyUICulture");
						FillDropCulture(MyUICulture);
						MyUICulture.SelectedIndex=-1;
						MyUICulture.SelectedValue=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.Lang)).ToLower();
					}


					TextBox vieworder = (TextBox) e.Item.FindControl("vieworder");
					vieworder.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "vieworder"));
					Literal rowid = (Literal) e.Item.FindControl("rowid");
					rowid.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					if(this.Multiline)
					{
						type.TextMode=TextBoxMode.MultiLine;
						type.Height = Unit.Pixel(50);
					}

					if(this.Grouped)
					{
						Control obCtl = e.Item.FindControl("OtherLangDatagrid");
						if (null != obCtl && obCtl is DataGrid)
						{
							DataSet ds = this.ReloadDataSet(Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id")));
							DataGrid dg = (DataGrid)obCtl;
							dg.ItemDataBound +=new DataGridItemEventHandler(dg_ItemDataBound);

							dg.Columns[1].Visible=dgValueEditor.Columns[1].Visible;
							dg.Columns[2].Visible=dgValueEditor.Columns[2].Visible;
							dg.Columns[3].Visible=dgValueEditor.Columns[3].Visible;
							dg.Columns[4].Visible=dgValueEditor.Columns[4].Visible;


							dg.DataSource = ds;
							dg.DataBind();

						}
					}
					break;
				case ListItemType.Footer:
					if(this.Multiline)
					{
						TextBox newtype = (TextBox) e.Item.FindControl("Newtype");
						TextBox newtype2 = (TextBox) e.Item.FindControl("Newtype2");
						newtype.TextMode=TextBoxMode.MultiLine;
						newtype.Height = Unit.Pixel(50);
                        newtype.MaxLength = MaxSizeField1;
						newtype2.TextMode=TextBoxMode.MultiLine;
						newtype2.Height = Unit.Pixel(50);
                        newtype2.MaxLength = MaxSizeField1;

					}
					if(this.Lang.Length>0)
					{
						DropDownList NewMyUICulture = (DropDownList) e.Item.FindControl("NewMyUICulture");
						FillDropCulture(NewMyUICulture);
					}
					LinkButton Linkbutton1 = (LinkButton)e.Item.FindControl("Linkbutton1");
					Linkbutton1.Text =Root.rm.GetString("Add");
					break;
			}
		}

		public enum FieldType
		{
			varchar = 0,
			numeric
		}

		private void dg_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					TextBox type = (TextBox) e.Item.FindControl("LangType");
					type.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.DataValueField));

					if(this.DataValueField2.Length>0)
					{
						TextBox type2 = (TextBox) e.Item.FindControl("LangType2");
						type2.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.DataValueField2));
						if(this.Multiline)
						{
							type2.TextMode=TextBoxMode.MultiLine;
							type2.Height = Unit.Pixel(50);
						}
					}
					if(this.Lang.Length>0)
					{
						DropDownList MyUICulture = (DropDownList) e.Item.FindControl("LangMyUICulture");
						FillDropCulture(MyUICulture);
						MyUICulture.SelectedIndex=-1;
						MyUICulture.SelectedValue=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, this.Lang)).ToLower();
					}


					TextBox vieworder = (TextBox) e.Item.FindControl("LangViewOrder");
					vieworder.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "vieworder"));
					Literal rowid = (Literal) e.Item.FindControl("LangRowId");
					rowid.Text=Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					if(this.Multiline)
					{
						type.TextMode=TextBoxMode.MultiLine;
						type.Height = Unit.Pixel(50);
					}


					break;
				case ListItemType.Footer:
					if(this.Multiline)
					{
						TextBox newtype = (TextBox) e.Item.FindControl("NewLangType");
						TextBox newtype2 = (TextBox) e.Item.FindControl("NewLangType2");
						newtype.TextMode=TextBoxMode.MultiLine;
						newtype.Height = Unit.Pixel(50);
						newtype2.TextMode=TextBoxMode.MultiLine;
						newtype2.Height = Unit.Pixel(50);
					}
					if(this.Lang.Length>0)
					{
						DropDownList NewMyUICulture = (DropDownList) e.Item.FindControl("NewLangMyUICulture");
						FillDropCulture(NewMyUICulture);
					}
					LinkButton Linkbutton2 = (LinkButton)e.Item.FindControl("Linkbutton2");
					Linkbutton2.Text =Root.rm.GetString("Add");
					break;
			}
		}
	}
}
