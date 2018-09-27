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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Digita.Tustena.WebControls
{
	public class RepeaterSearch : UserControl
	{
		protected LinkButton doSearch;
		protected TextBox txtSearchVal;
		protected internal DropDownList searchCols;
		protected HtmlTableCell alphaListtd;
		protected HtmlTable searchTable;

		private void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				populateSearchList();
			}
		}

		internal DropDownList SearchCols
		{
			get{return searchCols;}
			set{searchCols = value;}
		}

		public void AddToSearchList(string val, string text)
		{
			searchCols.Items.Add(new ListItem(text, val));
		}

		private void populateSearchList()
		{
			Repeater r = (Repeater) this.Parent.FindControl(this.Parent.ID + "_Repeater");
			populateSearchList(r);
		}

		private void populateSearchList(Control p)
		{
			foreach (Control c in p.Controls)
			{
				string cType = c.GetType().Name;
				if (cType == "RepeaterColumnHeader")
				{
					string val = ((RepeaterColumnHeader) c).DataCol;
					string text = ((RepeaterColumnHeader) c).LinkText;
					if (val!=null)
					{
						ListItem l = new ListItem(text, val);
						this.searchCols.Items.Add(l);
					}
					}
				else if (c.HasControls())
				{
					populateSearchList(c);
				}
			}

		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.doSearch.Click += new EventHandler(this.doSearch_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void doSearch_Click(object sender, EventArgs e)
		{
			TustenaRepeater cr = (TustenaRepeater) this.Parent;
			cr.DoSearch(searchCols.Items[searchCols.SelectedIndex].Value, txtSearchVal.Text, false);
		}
	}
}
