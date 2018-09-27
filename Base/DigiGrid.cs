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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Web.UI.WebControls;

namespace Digita.Tustena
{
	public delegate void ChangedEventHandler(object sender, EventArgs e);

	[
		DefaultProperty("DigiGrid")
	]



	public class DigiGrid : DataGrid
	{
		private bool shouldRebind = false;
		private bool editingMode = false;
		private int[] FieldTypes;
		private string[] FieldNames;
		private int fieldCount;
		private DataTable dt = null;


		public Hashtable DropFields
		{
			get { return dropFields; }
			set { dropFields = value; }
		}

		private Hashtable dropFields;

		public string HiddenColumns
		{
			get { return _HiddenColumns; }
			set { _HiddenColumns = value; }
		}

		private string _HiddenColumns = string.Empty;


		public event ChangedEventHandler Changed;

		protected virtual void OnChanged(EventArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}


		public DataSet DataSetSource
		{
			get { return (DataSet) ViewState["TableSource"]; }
			set { ViewState["TableSource"] = value; }
		}




		[
			Bindable(true),
				Browsable(true),
				Category("Appearance"),
				Description("Name of CSS class to apply to textboxes in edit mode.")
		]
		public string EditTextBoxCssClass
		{
			get
			{
				string s = (string) ViewState["EditTextBoxCssClass"];
				if (s == null)
					return String.Empty;
				return s;
			}
			set { ViewState["EditTextBoxCssClass"] = value; }
		}



		protected override void OnInit(EventArgs e)
		{
			if (Page != null)
			{

				EditCommandColumn c0 = new EditCommandColumn();
				c0.EditText = "<img src='/i/edit.gif' border=0 alt='edit this item'>";
				c0.CancelText = "<img src='/i/cancel.gif' border=0 alt=cancel>";
				c0.UpdateText = "<img src='/i/update.gif' border=0 alt='save changes'>";
				c0.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
				c0.HeaderStyle.Width = new Unit("35px");
				this.Columns.Add(c0);

				ButtonColumn c1 = new ButtonColumn();
				c1.CommandName = "Delete";
				c1.Text = "<img src='/i/delete.gif' border=0 alt='delete this item'>";
				c1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
				c1.HeaderStyle.Width = new Unit("35px");
				this.Columns.Add(c1);

				this.ShowFooter = true;
				this.ItemStyle.VerticalAlign = VerticalAlign.Top;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			if (Page != null)
			{
				dt = DataSetSource.Tables[0];
				fieldCount = 0;
				fieldCount = dt.Columns.Count;
				string[] tempName = new string[fieldCount];
				int[] tempType = new int[fieldCount];
				FieldTypes = tempType;
				FieldNames = tempName;

				bool accessType = false;
				for (int i = 0; i < fieldCount; i++)
				{
					string fldType = Convert.ToString(dt.Columns[i].DataType);
					switch (fldType)
					{
						case "System.String":
							FieldTypes[i] = 1;
							break;
						case "System.Byte":
							FieldTypes[i] = 2;
							break;
						case "System.Decimal":
							FieldTypes[i] = 2;
							break;
						case "System.Double":
							FieldTypes[i] = 2;
							break;
						case "System.Int16":
							FieldTypes[i] = 2;
							break;
						case "System.Int32":
							FieldTypes[i] = 2;
							break;
						case "System.Int64":
							FieldTypes[i] = 2;
							break;
						case "System.Single":
							FieldTypes[i] = 2;
							break;
						case "System.DateTime":
							if (accessType)
							{
								FieldTypes[i] = 5;
							}
							else
							{
								FieldTypes[i] = 3;
							}
							break;
						case "System.Boolean":
							FieldTypes[i] = 4;
							break;
						default:
							FieldTypes[i] = 0;
							break;
					}
					FieldNames[i] = Convert.ToString(dt.Columns[i].ColumnName);
				}
			}

			if (!Page.IsPostBack)
			{
				shouldRebind = true;
			}

		}

		protected override void OnPageIndexChanged(DataGridPageChangedEventArgs e)
		{
			if (!editingMode)
			{
				this.CurrentPageIndex = e.NewPageIndex;
				shouldRebind = true;
			}

		}

		protected override void OnEditCommand(DataGridCommandEventArgs e)
		{
			if (!editingMode)
			{
				this.EditItemIndex = e.Item.ItemIndex;
				shouldRebind = true;
			}
		}

		protected override void OnCancelCommand(DataGridCommandEventArgs e)
		{
			this.EditItemIndex = -1;
			shouldRebind = true;
		}

		protected override void OnDeleteCommand(DataGridCommandEventArgs e)
		{
			if (!editingMode)
			{
				DataSetSource.Tables[0].Rows[e.Item.ItemIndex].Delete();
				this.EditItemIndex = -1;
				OnChanged(EventArgs.Empty);
				shouldRebind = true;
			}
		}

		protected override void OnUpdateCommand(DataGridCommandEventArgs e)
		{
			this.EditItemIndex = -1;
			int fieldCountG = e.Item.Cells.Count - 2;
			for (int i = 0; i < fieldCountG; i++)
			{
				if (FieldNames[i].ToString() != this.DataKeyField)
				{
					DataSetSource.Tables[0].Rows[e.Item.ItemIndex][FieldNames[i].ToString()] = ((TextBox) e.Item.Cells[i + 2].Controls[0]).Text;
				}
			}
			OnChanged(EventArgs.Empty);
			shouldRebind = true;
		}

		internal int fileColumn = -1;
		internal string ddlFieldName = String.Empty;

		protected override void OnItemDataBound(DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				int icount=0;
				foreach (TableCell c in e.Item.Controls)
				{
					if(DropFields.ContainsKey(c.Text))
					{
						fileColumn=icount;
						ddlFieldName=c.Text;
					}


					icount++;
				}
			}
			else if (e.Item.ItemType == ListItemType.EditItem)
			{
				int icount=0;
				foreach (TableCell c in e.Item.Controls)
				{
					if (c.Controls[0].GetType() == typeof (TextBox))
					{
						TextBox t = (TextBox) c.Controls[0];
						if(icount==fileColumn+1)
						{
							t.Style["display"] = "none";
							DropDownList ifc = new DropDownList();
							string[] dropvalue = DropFields[ddlFieldName].ToString().Split(',');
							for(int i=0;i<dropvalue.Length;i+=2)
							{
								ifc.Items.Add(new ListItem(dropvalue[i+1],dropvalue[i]));
							}

							ifc.ID="Drop_"+ddlFieldName;
							c.Controls.Add(ifc);
							ifc.Attributes.Add("onchange","this.parentNode.firstChild.value=this.value");
						}
						if ((this.EditTextBoxCssClass != null && this.EditTextBoxCssClass.Length != 0))
							t.CssClass = this.EditTextBoxCssClass;
						else
							t.Style["width"] = "100%";

						int rows = (t.Text.Length/50) + 1;
						if (rows > 1)
						{
							t.TextMode = TextBoxMode.MultiLine;
							t.Rows = rows + 3;

						}
					}
					icount++;
				}
			}
		}


		protected override void OnItemCreated(DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Header)
			{
				e.Item.Cells.RemoveAt(0);
				e.Item.Cells.RemoveAt(0);
				TableCell c = new TableCell();
				c.ColumnSpan = 2;
				c.Text = "<b>" + DataSetSource.Tables[0].TableName.ToUpper() + "</b>";
				e.Item.Cells.AddAt(0, c);

			}

			if (e.Item.ItemType == ListItemType.Footer)
			{
				e.Item.Cells.RemoveAt(0);
				e.Item.Cells.RemoveAt(0);
				TableCell c = new TableCell();
				c.ColumnSpan = 2;
				LinkButton l = new LinkButton();
				l.Click += new EventHandler(AddItem);
				l.Text = "Add New";
				l.CssClass = "CommandButton";
				c.Controls.Add(l);
				e.Item.Cells.AddAt(0, c);
			}


		}

		protected override void OnPreRender(EventArgs e)
		{
			if (shouldRebind)
			{
				BindData();
			}
		}

		protected override void OnItemCommand(DataGridCommandEventArgs e)
		{
			CheckEditing(e.CommandName);
		}



		[
			Bindable(false),
				Browsable(false)
		]
		public override object DataSource
		{
			get { return base.DataSource; }
			set { base.DataSource = value; }
		}


		private void CheckEditing(string commandName)
		{
			if (this.EditItemIndex != -1)
			{
				if (!commandName.Equals("Cancel") && !commandName.Equals("Update"))
				{
					editingMode = true;
				}
			}
		}

		public void BindData()
		{
			this.DataSource = DataSetSource;
			this.DataBind();
		}

		private void AddItem(Object src, EventArgs e)
		{
			CheckEditing("");
			if (!editingMode)
			{
				DataRow dr = DataSetSource.Tables[0].NewRow();
				DataSetSource.Tables[0].Rows.Add(dr);
			}


			this.CurrentPageIndex = this.PageCount - 1;
			BindData();
			this.EditItemIndex = this.Items.Count - 1;
			BindData();
		}
	}


}
