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

using System.ComponentModel;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Digita.Tustena.IndexControl
{


	[DefaultProperty("Text")]
	[ToolboxData("<{0}:IndexGrid runat=server></{0}:IndexGrid>")]
	public class IndexGrid:WebControl
	{
		StringBuilder grid=new StringBuilder();
		public IndexGrid()
		{
		}

		private string id;
		public override string ID
		{
			get{return id;}
			set
			{
				id = value;
			}
		}

		private string text;

		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		public string Text
		{
			get{return text;}
			set
			{
				text = value;
			}
		}

		protected override void Render(HtmlTextWriter output)
		{
			output.Write(Text);
		}

		private  string[] columnsField = new string[0];
		public string[] ColumnsField
		{
			set
			{
				 columnsField = value;
			}
		}

		private  string[] columnsHeader = new string[0];
		public string[] ColumnsHeader
		{
			set
			{
				columnsHeader = value;
			}
		}

		private int[] columnsSize = new int[0];
		public int[] ColumnsSize
		{
			set
			{
				columnsSize = value;
			}
		}
		public void ColumnSize(int columns,int length)
		{
			columnsSize[columns]=length;
		}

		DataTable sourcetable = new DataTable();
		public DataTable SourceTable
		{
			set
			{
				sourcetable = value;
			}
			get
			{
				return sourcetable;
			}
		}

		public void BuildGrid()
		{
			grid.AppendFormat("<table id='{0}'><tr>", this.ID);

			foreach(DataColumn cc in SourceTable.Columns)
			{
				grid.AppendFormat("<td class=\"GridTitle\">{0}</td>",cc.ColumnName);
			}
			grid.Append("</tr>");
			foreach(DataRow dr in SourceTable.Rows)
			{

				grid.Append("<tr>");
				foreach(DataColumn cc in SourceTable.Columns)
				{
					grid.AppendFormat("<td class=\"{1}\">{0}</td>",dr[cc.ColumnName]);
				}
				grid.Append("</tr>");
			}
			grid.Append("</table>");
			this.Text=grid.ToString();
		}

	}
}
