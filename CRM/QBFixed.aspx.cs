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
using System.Diagnostics;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class QBFixed : G
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
				QBRepeater.DataSource=FillRepeater((int)QBDefault.QueryType.Company);
				QBRepeater.DataBind();
				QBRepeaterLead.DataSource=FillRepeater((int)QBDefault.QueryType.Leads);
				QBRepeaterLead.DataBind();
				QBRepeaterActivity.DataSource=FillRepeater((int)QBDefault.QueryType.Activity);
				QBRepeaterActivity.DataBind();
				QBRepeaterOpportunity.DataSource=FillRepeater((int)QBDefault.QueryType.Opportuniy);
				QBRepeaterOpportunity.DataBind();

			}
		}

		private DataTable FillRepeater(int qT)
		{
			DataTable dt = DatabaseConnection.CreateDataset(String.Format("SELECT ID,TITLE,DESCRIPTION,RM FROM QB_CUSTOMERQUERY WHERE QUERYTYPE={0}",qT)).Tables[0];
			return dt;
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.QBRepeater.ItemCommand +=new RepeaterCommandEventHandler(QBRepeater_ItemCommand);
			this.QBRepeaterLead.ItemCommand +=new RepeaterCommandEventHandler(QBRepeater_ItemCommand);
			this.QBRepeaterActivity.ItemCommand +=new RepeaterCommandEventHandler(QBRepeater_ItemCommand);
			this.QBRepeaterOpportunity.ItemCommand +=new RepeaterCommandEventHandler(QBRepeater_ItemCommand);
			this.QBRepeater.ItemDataBound +=new RepeaterItemEventHandler(QBRepeater_ItemDataBound);
			this.QBRepeaterLead.ItemDataBound +=new RepeaterItemEventHandler(QBRepeater_ItemDataBound);
			this.QBRepeaterActivity.ItemDataBound +=new RepeaterItemEventHandler(QBRepeater_ItemDataBound);
			this.QBRepeaterOpportunity.ItemDataBound +=new RepeaterItemEventHandler(QBRepeater_ItemDataBound);
		}
		#endregion

		private void QBRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{

			switch (e.CommandName)
			{
				case "QBDescription":
					Session["ReportToOpen"] = ((Literal) e.Item.FindControl("QueryID")).Text;
					Response.Redirect("/CRM/QBDefault.aspx?m=55&dgb=1&si=42");
					break;
			}

		}

		private void QBRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton desc = (LinkButton) e.Item.FindControl("QBDescription");
					Literal descfull = (Literal) e.Item.FindControl("QBDescFull");
					string[] RmString =((Literal) e.Item.FindControl("RmString")).Text.Split('|');
					try
					{
						desc.Text=Root.rm.GetString("QBFixTxt"+RmString[0]);
						descfull.Text=Root.rm.GetString("QBFixTxt"+RmString[1]);
					}
					catch
					{
						Debug.Write("Errore in QBFixed riga 102");
					}

					break;
			}
		}
	}
}
