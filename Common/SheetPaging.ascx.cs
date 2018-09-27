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
using System.Web.UI.WebControls;

namespace Digita.Tustena.Common
{
	public partial class SheetPaging : UserControl
	{



		public string[] IDArray
		{
			get{
				object o = this.ViewState["_IDArray" + this.ID];
				if (o == null)
					return null;
				else
					return (string[]) o;
			}
			set { this.ViewState["_IDArray" + this.ID] = value; }
		}

		public int CurrentPosition
		{
			get
			{
			object o = this.ViewState["_CurrentPosition" + this.ID];
			if (o == null)
			return 0;
			else
			return (int) o;
			}
			set { this.ViewState["_CurrentPosition" + this.ID] = value; }

		}

		public int GetCurrentID
		{
			get
			{
				return int.Parse(IDArray[CurrentPosition].Split('|')[0]);
			}
		}


		protected void Page_Load(object sender, EventArgs e)
		{
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Prev.Click +=new EventHandler(Prev_Click);
			this.Next.Click +=new EventHandler(Next_Click);
			this.Load += new EventHandler(this.Page_Load);
		}
		#endregion

		public event EventHandler PrevClick;

		protected void OnPrevClick(EventArgs e)
		{
			if(PrevClick != null)
			{
				PrevClick(this, e);
			}
		}

		public event EventHandler NextClick;

		protected void OnNextClick(EventArgs e)
		{
			if(NextClick != null)
			{
				NextClick(this, e);
			}
		}

		private void Prev_Click(object sender, EventArgs e)
		{
			if(CurrentPosition>0)CurrentPosition--;
			enabledisable();
			OnPrevClick(e);
		}

		private void Next_Click(object sender, EventArgs e)
		{
			if(CurrentPosition<IDArray.Length-1)CurrentPosition++;
			enabledisable();
			OnNextClick(e);
		}

		public void enabledisable()
		{
			if(CurrentPosition<=0)
			{
				Prev.Enabled=false;
				Prev.ToolTip="";
			}
			else
			{
				Prev.Enabled=true;
				try
				{
					Prev.ToolTip=IDArray[CurrentPosition-1].Split('|')[1];
				}catch
				{
					Prev.ToolTip="";
				}

			}

			if(CurrentPosition==IDArray.Length-1)
			{
				Next.Enabled=false;
				Next.ToolTip="";
			}
			else
			{
				Next.Enabled=true;
				try
				{
					Next.ToolTip=IDArray[CurrentPosition+1].Split('|')[1];
				}
				catch
				{
					Next.ToolTip="";
				}
			}

		}
	}
}
