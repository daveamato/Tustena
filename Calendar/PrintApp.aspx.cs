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
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class PrintApp : G
	{


				#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
				this.Load += new EventHandler(this.Page_Load);
			this.ViewAppointmentForm.ItemDataBound += new RepeaterItemEventHandler(this.ItemDataBoundView);

			}
				#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				FillViewCard(int.Parse(Request.Params["id"].ToString()));
			}
		}

		private void FillViewCard(int id)
		{
			string sqlString = "SELECT BASE_CALENDAR.*,CONVERT(VARCHAR(10),BASE_CALENDAR.STARTDATE,105) AS DATA, CONVERT(VARCHAR(5),BASE_CALENDAR.STARTDATE,114) AS DALLE,CONVERT(VARCHAR(5),BASE_CALENDAR.ENDDATE,114) AS ALLE, ";
			sqlString += "(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS USERNAME ";
			sqlString += "FROM BASE_CALENDAR LEFT OUTER JOIN ACCOUNT ON BASE_CALENDAR.UID = ACCOUNT.UID WHERE BASE_CALENDAR.ID=" + id;
			ViewAppointmentForm.DataSource = DatabaseConnection.CreateDataset(sqlString);
			ViewAppointmentForm.DataBind();

		}

		public void ItemDataBoundView(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal DateLTZ = (Literal) e.Item.FindControl("Date");
					DateLTZ.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToShortDateString();
					Literal DateFromLTZ = (Literal) e.Item.FindControl("DateFrom");
					DateFromLTZ.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToString(@"HH:mm");
					Literal DateToLTZ = (Literal) e.Item.FindControl("DateTo");
					DateToLTZ.Text =UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "enddate"))).ToString(@"HH:mm");

					break;
			}
		}


	}
}
