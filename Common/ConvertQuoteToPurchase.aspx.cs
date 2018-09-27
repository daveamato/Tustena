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
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class ConvertQuoteToPurchase : G
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				CloseWindow();
			}
			else
			{
				if (!Page.IsPostBack)
				{
					if(Session["QuoteID"]!=null && StaticFunctions.IsNumber(Session["QuoteID"].ToString()))
					{
						long quoteId = Convert.ToInt64(Session["QuoteID"]);
						DataTable dt = DatabaseConnection.CreateDataset("SELECT COMPANYID FROM QUOTE_VIEW WHERE QUOTEID="+quoteId).Tables[0];

						BillC.RefID = Convert.ToInt64(dt.Rows[0][0]);
						BillC.FromQuote = true;
						BillC.QuoteID = quoteId;
						BillC.PurchaseID=-1;
						BillC.FillProduct();
						Session.Remove("QuoteID");
					}else
						CloseWindow();

				}
			}
		}

		private void CloseWindow()
		{
			ClientScript.RegisterStartupScript(this.GetType(), "redirect", "<script>window.opener.location.href=window.opener.location.href;self.close();</script>");
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

		}
		#endregion
	}
}
