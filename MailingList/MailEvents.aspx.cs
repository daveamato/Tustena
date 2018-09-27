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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Core;

namespace Digita.Tustena.MailingList
{
	public partial class MailEvents : G
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
				Reportbtn.Text =Root.rm.GetString("Mlevtxt1");
				BirthDatebtn.Text =Root.rm.GetString("Mlevtxt2");
				FillDropDownRec();
			}
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Reportbtn.Click += new EventHandler(this.ReportBtn_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion
		private void ReportBtn_Click(object sender, EventArgs e)
		{
		}

		private void FillDropDownRec()
		{
			int i;
			for (i = 1; i < 7; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (22 + i).ToString());
				RecMode.Items.Add(lt);
			}

			for (i = 1; i < 6; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (45 + i).ToString());
				RecMonthlyDayPU.Items.Add(lt);
				RecYearDayPU.Items.Add(lt);
			}

			for (i = 8; i < 11; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (50 + i - 7).ToString());
				RecMonthlyDayDays.Items.Add(lt);
				RecYearDayDays.Items.Add(lt);
			}


			DateTime giorno = new DateTime(2003, 8, 3);
			DateTime month = new DateTime(2003, 1, 1);
			for (i = 0; i < 7; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = (i + 1).ToString();
				lt.Text =UC.myDTFI.GetDayName(giorno.AddDays(i).DayOfWeek);
				RecMonthlyDayDays.Items.Add(lt);
				RecSetDays.Items.Add(lt);
				RecYearDayDays.Items.Add(lt);
			}
			for (i = 0; i < 12; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = (i + 1).ToString();
				lt.Text =UC.myDTFI.GetMonthName(month.AddMonths(i).Month);
				RecYearMonths.Items.Add(lt);
				RecYearDayMonths.Items.Add(lt);
			}
		}
	}
}
