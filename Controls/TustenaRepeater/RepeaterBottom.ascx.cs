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
	public class RepeaterBottom : UserControl
	{
		protected LinkButton backBtn;
		protected PlaceHolder numbersHolder;
		protected Label Elements;
		protected LinkButton nextBtn;
		protected Digita.Tustena.WebControls.LocalizedLiteral LocalizedLiteral30;
		protected HtmlTableCell footerTd;


		protected override void OnPreRender(EventArgs e)
		{
			TustenaRepeater cr = (TustenaRepeater) this.Parent.Parent.Parent;
			Elements.Text = cr.RowCount.ToString();
			footerTd.ColSpan = 10;
			SetPageNums();
			base.OnPreRender(e);
		}

		internal void SetPageNums()
		{
			if (this.footerTd.Visible)
			{
				TustenaRepeater cr = (TustenaRepeater) this.Parent.Parent.Parent;
				int curPage=1;
				int PgCount=1;
				this.numbersHolder.Controls.Clear();
				if(!cr.isDatabinded)
				{
					if(ViewState[this.ClientID+"CCPC"]!=null)
					{
						string[] arr = ViewState[this.ClientID+"CCPC"].ToString().Split('|');
						curPage = int.Parse(arr[0]);
						PgCount = int.Parse(arr[1]);
					}
				}
				else
				{
					curPage = cr.CurrentPage;
					PgCount = cr.PageCount;
				}

				ViewState[this.ClientID+"CCPC"]=curPage+"|"+PgCount;

				if (curPage == 1)
				{
					backBtn.Visible = false;
				}
				else
				{
					backBtn.Visible = true;
				}
				if (curPage == PgCount || PgCount<1)
				{
					nextBtn.Visible = false;
				}
				else
				{
					nextBtn.Visible = true;
				}

				if(PgCount>1)
				for (int i = 1; i <= PgCount; i++)
				{
					if(i>30)
					{
						HtmlGenericControl spacer = new HtmlGenericControl();
						spacer.InnerHtml = "...";
						numbersHolder.Controls.Add(spacer);

						break;
					}
					LinkButton lb = new LinkButton();

					lb.CssClass = "repItemLink";
					lb.ID = cr.ClientID+"GoToPage_" + i;
					lb.Text = i.ToString();
					lb.Click += new EventHandler(cr.PageChangeHandler);
					if (i == curPage)
					{
						lb.Style.Add("font-weight", "bold");
					}
					this.numbersHolder.Controls.Add(lb);
					if (i != PgCount)
					{
						HtmlGenericControl spacer = new HtmlGenericControl();
						spacer.InnerHtml = "&nbsp;";
						numbersHolder.Controls.Add(spacer);
					}
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
			this.backBtn.Click += new System.EventHandler(this.backBtn_Click);
			this.nextBtn.Click += new System.EventHandler(this.nextBtn_Click);

		}

		#endregion

		private void NavButton(int increment)
		{
			TustenaRepeater cr = (TustenaRepeater) this.Parent.Parent.Parent;

			int oldPage = cr.OldPage;
			int maxNr = cr.PageCount;
			int newPage = oldPage + increment;
			cr.DoPageChange(newPage.ToString());
		}

		private void backBtn_Click(object sender, EventArgs e)
		{
			NavButton(-1);
		}

		private void nextBtn_Click(object sender, EventArgs e)
		{
			NavButton(1);
		}
	}
}
