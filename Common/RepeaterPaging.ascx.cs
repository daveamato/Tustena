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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class RepeaterPaging : UserControl
	{
		private int rowCount = 0;

        private bool dontUse = false;

        public bool DontUse
        {
            get { return dontUse; }
            set { dontUse = value; }
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Prev.Text = Root.rm.GetString("PreviousTxt") + "&nbsp;";
			this.Next.Text = Root.rm.GetString("NextTxt");
		}

		public string OrderField
		{
			get
			{
				object o = this.ViewState["_OrderField" + this.ID];
				if (o == null)
					return null;
				else
					return (string) o;
			}

			set { this.ViewState["_OrderField" + this.ID] = value; }
		}

		public orderType OrderType
		{
			get
			{
				object o = this.ViewState["_OrderType" + this.ID];
				if (o == null)
					return orderType.ascending;
				else
					return (orderType) o;
			}

			set { this.ViewState["_OrderType" + this.ID] = value; }
		}

		public enum orderType
		{
			ascending = 0,
			descending
		}

		public Repeater RepeaterObj
		{
			get
			{
				object o = this.ViewState["_RepeaterObj" + this.ID];
				if (o == null)
					return null;
				else
					return (Repeater) this.Parent.FindControl(o.ToString());
			}

			set { this.ViewState["_RepeaterObj" + this.ID] = value.ID; }
		}

		public int RowCount
		{
			get { return rowCount; }
		}

		public int CurrentPage
		{
			get
			{
				object o = this.ViewState["_CurrentPage" + this.ID];
				if (o == null)
					return 0; // default prima pagina
				else
					return (int) o;
			}

			set { this.ViewState["_CurrentPage" + this.ID] = value; }
		}

		public int PageSize
		{
			get
			{
				object o = this.ViewState["_PageSize" + this.ID];
				if (o == null)
					return 5; // default 5 voci
				else
					return (int) o;
			}

			set { this.ViewState["_PageSize" + this.ID] = value; }
		}

		public string sqlRepeater
		{
			get
			{
				object o = this.ViewState["_sqlRepeater" + this.ID];
				if (o == null)
					return "";
				else
					return (string) o;
			}
			set { this.ViewState["_sqlRepeater" + this.ID] = value; }
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
			this.Prev.Click += new EventHandler(Page_Repeater_Click);
			this.Next.Click += new EventHandler(Page_Repeater_Click);
		}

		#endregion

		private void Page_Repeater_Click(object sender, EventArgs e)
		{
			if (((LinkButton) sender).ID == "Prev")
			{
				CurrentPage -= 1;
			}
			else if (((LinkButton) sender).ID == "Next")
			{
				CurrentPage += 1;
			}
			BuildGrid();

		}

		public void Reorder()
		{
			Reorder(this.sqlRepeater);
		}

		public void Reorder(string finalQuery)
		{
			if (this.OrderField.Length > 0)
			{
				if (finalQuery.LastIndexOf("ORDER") > 0)
				{
					finalQuery = finalQuery.Substring(0, finalQuery.IndexOf("ORDER") - 1);
				}
				finalQuery += " ORDER BY " + this.OrderField + " " + ((this.OrderType == orderType.ascending) ? "ASC" : "DESC");
			}
			this.sqlRepeater = finalQuery;
		}

		public void BuildGrid()
		{
                 DataSet Items = DatabaseConnection.CreateDataset(sqlRepeater);
                BuildGrid(Items);
		}

		public void BuildGrid(DataSet Items)
		{
			if (Items.Tables[0].Rows.Count > 0)
			{
				PagedDataSource objPds = new PagedDataSource();
				objPds.DataSource = Items.Tables[0].DefaultView;
				objPds.AllowPaging = true;
				objPds.PageSize = PageSize;
				rowCount = objPds.DataSourceCount;

				if ((rowCount + PageSize) <= (PageSize*(this.CurrentPage + 1)))
					CurrentPage--;

				objPds.CurrentPageIndex = CurrentPage;

				Prev.Enabled = !objPds.IsFirstPage;
				Next.Enabled = !objPds.IsLastPage;

				RepeaterObj.DataSource = objPds;
				RepeaterObj.DataBind();
			}
			else
			{
				rowCount = 0;
				RepeaterObj.DataSource = Items;
				RepeaterObj.DataBind();
			}

            this.Visible = RepeaterObj.Visible && rowCount > PageSize && !dontUse;

		}

	}
}
