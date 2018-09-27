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
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.ERP
{
	public partial class OrderList : G
	{

		protected Digita.Tustena.WebControls.RepeaterHeaderBegin RepeaterHeaderBegin1;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader1;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader2;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader3;
		protected Digita.Tustena.WebControls.RepeaterColumnHeader Repeatercolumnheader6;
		protected Digita.Tustena.WebControls.RepeaterHeaderEnd RepeaterHeaderEnd1;
		protected System.Web.UI.WebControls.LinkButton OpenQuote;
		protected System.Web.UI.WebControls.Literal QuoteID;
		protected System.Web.UI.WebControls.Literal QuoteDate;
		protected System.Web.UI.WebControls.Literal QuoteNumber;
		protected System.Web.UI.WebControls.Literal QuoteDescription;
		protected System.Web.UI.WebControls.Literal QuoteCustomer;
		protected System.Web.UI.WebControls.Literal QuoteOwner;
		protected System.Web.UI.WebControls.Literal QuoteTotal;


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				InitNewQuoteList();
				NewQuote.Text=Root.rm.GetString("Ordtxt6");

				if(!Page.IsPostBack)
				{
                    if (UC.AccessLevel == 1)
                        FillRepeaterList(string.Format("SELECT * FROM ORDERS WHERE (CROSSTYPE>=0 AND OWNERID={0}) ", UC.UserId));
                    else
					    FillRepeaterList(string.Format("SELECT * FROM ORDERS WHERE (CROSSTYPE>=0 AND ({0})) ",GroupsSecure("ORDERS.GROUPS")));
                    OrderEditing.FillOrderStage(this.SearchQuoteStage, true, false);
					ListItem li = new ListItem(Root.rm.GetString("Mailtxt13"),"0");
					CrossWith.Items.Add(li);
					li = new ListItem(Root.rm.GetString("Mailtxt14"),"1");
					CrossWith.Items.Add(li);
					li = new ListItem(Root.rm.GetString("Mailtxt15"),"2");
					CrossWith.Items.Add(li);
					CrossWith.RepeatDirection=RepeatDirection.Vertical;
					CrossWith.Items[0].Selected=true;
					this.btnSearch.Text=Root.rm.GetString("Find");
				}
			}
		}

		private void InitNewQuoteList()
		{
			this.NewQuoteListRepeater.UsePagedDataSource=true;
			this.NewQuoteListRepeater.PageSize=UC.PagingSize;
		}

		private void FillRepeaterList(string s)
		{
			this.NewQuoteListRepeater.sqlDataSource=s;
			this.NewQuoteListRepeater.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.NewQuoteListRepeater.ItemDataBound += new RepeaterItemEventHandler(this.QuoteListRepeater_ItemDataBound);
			this.NewQuoteListRepeater.ItemCommand += new RepeaterCommandEventHandler(this.QuoteListRepeater_ItemCommand);

		}
		#endregion

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
					Session["ViewOrder"]=((Literal)e.Item.FindControl("QuoteID")).Text;
					Response.Redirect("/erp/orderediting.aspx?m=67&dgb=1&si=72");
					break;
				case "MultiDeleteButton":
					DeleteChecked.MultiDelete(this.NewQuoteListRepeater.MultiDeleteListArray,"Orders");
					this.NewQuoteListRepeater.DataBind();
					break;
			}
		}

		protected void NewQuote_Click(object sender, EventArgs e)
		{
			Response.Redirect("/erp/orderediting.aspx?m=67&dgb=1&si=72");
		}

		protected void btnSearch_Click(object sender, EventArgs e)
		{
			StringBuilder sbquery = new StringBuilder();
            if (UC.AccessLevel == 1)
                sbquery.AppendFormat("SELECT * FROM ORDERS WHERE (CROSSTYPE>=0 AND OWNERID={0}) ", UC.UserId);
            else
			    sbquery.AppendFormat("SELECT * FROM ORDERS WHERE (CROSSTYPE>=0 AND ({0})) ",GroupsSecure("ORDERS.GROUPS"));


            if (TextBoxSearchQuoteExpire.Text.Length>0)
			{
				string QuoteExpire = UC.LTZ.ToLocalTime(Convert.ToDateTime(TextBoxSearchQuoteExpire.Text)).ToString(@"yyyyMMdd");
				DateTime DQuoteExpire = Convert.ToDateTime(TextBoxSearchQuoteExpire.Text);

				TimeSpan mindiffstart = UC.LTZ.GetUtcOffset(DQuoteExpire);


				string Qtype = "=";
				switch (DiffQuoteExpire.SelectedValue)
				{
					case "0":
						Qtype = "<=";
						break;
					case "1":
						Qtype = "=";
						break;
					case "2":
						Qtype = ">=";
						break;
				}
				sbquery.AppendFormat("AND (DATEADD(N,{0},ORDERS.EXPIRATIONDATE){2} '{1} 00:00') ", mindiffstart.TotalMinutes.ToString(), QuoteExpire, Qtype);

			}
			if(SearchQuoteStage.SelectedIndex>0)
				sbquery.AppendFormat("AND STAGE={0} ",SearchQuoteStage.SelectedValue);
			if(TextboxSearchQuoteNumber.Text.Length>0)
				sbquery.AppendFormat("AND NUMBER='{0}' ",TextboxSearchQuoteNumber.Text);
			if(TextboxSearchDescription.Text.Length>0)
				sbquery.AppendFormat("AND SUBJECT LIKE '%{0}%' ",TextboxSearchDescription.Text);
			if(TextboxSearchOwnerID.Text.Length>0)
				sbquery.AppendFormat("AND OWNERID={0} ",TextboxSearchOwnerID.Text);
			if(CrossWithID.Text.Length>0)
			{
				sbquery.AppendFormat("AND (CROSSTYPE={0} AND CROSSID={1}) ",CrossWith.SelectedValue,CrossWithID.Text);
			}
			if(SearchGrandTotal.Text.Length>0)
			{
				string Qtype = "=";
				switch (RadiobuttonlistGrandTotal.SelectedValue)
				{
					case "0":
						Qtype = "<=";
						break;
					case "1":
						Qtype = "=";
						break;
					case "2":
						Qtype = ">=";
						break;
				}
				sbquery.AppendFormat("AND GRANDTOTAL{0}{1} ",Qtype,SearchGrandTotal.Text);
			}
			sbquery.Append("ORDER BY EXPIRATIONDATE");

			FillRepeaterList(sbquery.ToString());
		}
	}


}
