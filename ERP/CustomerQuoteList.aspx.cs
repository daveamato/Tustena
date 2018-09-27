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
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.ERP
{
	public partial class CustomerQuoteList : G
	{


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "redirect","<script>parent.location='/login.aspx';</script>");
			}
			else
			{
				if(Session["CustomerQuote"]!=null)
				{
					CustomerQuote = Session["CustomerQuote"].ToString().Split('|');
					Bind();
				}
			}
		}

		private void Bind()
		{

			FillRepeaterList(string.Format("SELECT * FROM QUOTES WHERE (CROSSTYPE={1} AND CROSSID={2} AND ({0})) ",GroupsSecure("QUOTES.GROUPS"),CustomerQuote[0],CustomerQuote[1]));
		}

		private string[] customerQuote;
		public string[] CustomerQuote
		{
			get{return customerQuote;}
			set{customerQuote=value;}
		}

		private void FillRepeaterList(string s)
		{

			QuoteListRepeater.DataSource=DatabaseConnection.CreateDataset(s);
			QuoteListRepeater.DataBind();

			if (QuoteListRepeater.Items.Count > 0)
			{
				QuoteListRepeater.Visible = true;
				lblRepeaterPaginginfo.Visible=false;
			}
			else
			{
				QuoteListRepeater.Visible = false;
				lblRepeaterPaginginfo.Text=Root.rm.GetString("Esttxt33");
				lblRepeaterPaginginfo.Visible=true;
			}
		}

		private void QuoteListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal QuoteID = (Literal)e.Item.FindControl("QuoteID");
					Literal QuoteDate = (Literal)e.Item.FindControl("QuoteDate");
					Literal QuoteNumber = (Literal)e.Item.FindControl("QuoteNumber");
					Literal QuoteDescription = (Literal)e.Item.FindControl("QuoteDescription");
					Literal QuoteCustomer = (Literal)e.Item.FindControl("QuoteCustomer");
					Literal QuoteOwner = (Literal)e.Item.FindControl("QuoteOwner");
					Literal QuoteTotal = (Literal)e.Item.FindControl("QuoteTotal");
					LinkButton OpenQuote = (LinkButton)e.Item.FindControl("OpenQuote");

					QuoteDate.Text=UC.LTZ.ToUniversalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "expirationdate").ToString())).ToShortDateString();
					QuoteNumber.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "Number").ToString();
					QuoteDescription.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "Subject").ToString();
					QuoteID.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "ID").ToString();
				switch(Convert.ToByte(DataBinder.Eval((DataRowView) e.Item.DataItem, "CrossType")))
				{
					case 0:
						QuoteCustomer.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID="+DataBinder.Eval((DataRowView) e.Item.DataItem, "CrossID").ToString());
						break;
					case 1:
						QuoteCustomer.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID="+DataBinder.Eval((DataRowView) e.Item.DataItem, "CrossID").ToString());
						break;
					case 2:
						QuoteCustomer.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID="+DataBinder.Eval((DataRowView) e.Item.DataItem, "CrossID").ToString());
						break;
				}

					QuoteOwner.Text=DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID="+DataBinder.Eval((DataRowView) e.Item.DataItem, "OwnerID").ToString());

					QuoteTotal.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "GrandTotal").ToString();
					OpenQuote.Text = "<img src=/i/lookup.gif border=0>";

					break;
			}
		}

		private void QuoteListRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch(e.CommandName)
			{
				case "OpenQuote":
					Session["ViewQuote"]=((Literal)e.Item.FindControl("QuoteID")).Text;
					Page.ClientScript.RegisterStartupScript(this.GetType(), "redirect","<script>parent.location='/erp/quoteediting.aspx?m=67&dgb=1&si=69';</script>");
					break;
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.QuoteListRepeater.ItemCommand+=new RepeaterCommandEventHandler(QuoteListRepeater_ItemCommand);
			this.QuoteListRepeater.ItemDataBound+=new RepeaterItemEventHandler(QuoteListRepeater_ItemDataBound);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}
