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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Dash
{
	public partial class Dash2 : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					BtnSubmit.Text =Root.rm.GetString("Dastxt8");
					Days.Items.Add(new ListItem(Root.rm.GetString("Das2txt7"), "0"));
					Days.Items.Add(new ListItem("30 " +Root.rm.GetString("Das2txt8"), "30"));
					Days.Items.Add(new ListItem("60 " +Root.rm.GetString("Das2txt8"), "60"));
					Days.Items.Add(new ListItem("90 " +Root.rm.GetString("Das2txt8"), "90"));
					Days.Items.Add(new ListItem("120 " +Root.rm.GetString("Das2txt8"), "120"));
					Days.SelectedIndex = 0;
				}
			}
		}

		public void RepeaterSearchDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					string id = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));

					Label Subject = (Label) e.Item.FindControl("Subject");
					int aType = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Type");
					Subject.Text = ImgType(aType);
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "subject"));
					if (sub.Length > 30)
					{
						Regex r = new Regex(@"(?s)\b.{1,28}\b");
						Subject.ToolTip = sub;
						Subject.Text += "&nbsp;<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + ">" + r.Match(sub) + "&hellip;" + "</a>";
						Subject.Attributes.Add("style", "cursor:help;");
					}
					else
					{
						Subject.Text += "&nbsp;<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + ">" + sub + "</a>";
					}

					Literal AcDate = (Literal) e.Item.FindControl("AcDate");
					AcDate.Text = Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"),UC.myDTFI).ToShortDateString();

					try
					{
						int ids = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyID");
						if (ids > 0)
						{
							Literal CompanyName = (Literal) e.Item.FindControl("CompanyName");
							CompanyName.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + ids);
						}
					}
					catch
					{
					}
					try
					{
						int ids = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "ReferrerID");
						if (ids > 0)
						{
							Literal ContactName = (Literal) e.Item.FindControl("ContactName");
							ContactName.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM BASE_CONTACTS WHERE ID=" + ids);
						}
					}
					catch
					{
					}
					try
					{
						int ids = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadID");
						if (ids > 0)
						{
							Literal LeadName = (Literal) e.Item.FindControl("LeadName");
							LeadName.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(COMPANYNAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM CRM_LEADS WHERE ID=" + ids);
						}
					}
					catch
					{
					}
					try
					{
						int ids = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "OwnerID");
						if (ids > 0)
						{
							Literal OwnerName = (Literal) e.Item.FindControl("OwnerName");
							OwnerName.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') FROM ACCOUNT WHERE UID=" + ids.ToString());
						}
					}
					catch
					{
					}
					break;
			}
		}

		private string ImgType(int aType)
		{
			string img = String.Empty;
			switch (aType)
			{
				case 1:
					img = "<img src=/i/a/Phone.gif>";
					break;
				case 2:
					img = "<img src=/i/a/letter.gif>";
					break;
				case 3:
					img = "<img src=/i/a/fax.gif>";
					break;
				case 4:
					img = "<img src=/i/a/Pin.gif>";
					break;
				case 5:
					img = "<img src=/i/a/Email.gif>";
					break;
				case 6:
					img = "<img src=/i/a/Hands.gif>";
					break;
				case 7:
					img = "<img src=/i/a/generic.gif>";
					break;
				case 8:
					img = "<img src=/i/a/case.gif>";
					break;
			}
			return img + "&nbsp;";
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.BtnSubmit.Click += new EventHandler(this.SubmitBtn_Click);
			this.Load += new EventHandler(this.Page_Load);
			this.RepeaterSearch.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterSearchDataBound);

		}

		#endregion

		private void SubmitBtn_Click(object sender, EventArgs e)
		{
			StringBuilder s = new StringBuilder();
			StringBuilder p = new StringBuilder();
			s.Append("SELECT * FROM LASTCONTACT_VIEW ");
			if (Days.SelectedIndex > 0)
			{
				p.AppendFormat(" (DATEDIFF(DAY, ACTIVITYDATE, GETDATE())>{0}) AND ", Days.SelectedValue);
				Days.SelectedIndex = 0;
			}
			if (((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text.Length > 0)
			{
				p.AppendFormat(" (OWNERID={0}) AND ", ((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text);
				((TextBox) Page.FindControl("TextboxSearchOwnerID")).Text = String.Empty;
				((TextBox) Page.FindControl("TextboxSearchOwner")).Text = String.Empty;
			}
			if (((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text.Length > 0)
			{
				p.AppendFormat(" (COMPANYID={0}) AND ", ((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text);
				((TextBox) Page.FindControl("TextboxSearchCompanyID")).Text = String.Empty;
				((TextBox) Page.FindControl("TextboxSearchCompany")).Text = String.Empty;
			}
			if (((TextBox) Page.FindControl("TextboxSearchContactID")).Text.Length > 0)
			{
				p.AppendFormat(" (REFERRERID={0}) AND ", ((TextBox) Page.FindControl("TextboxSearchContactID")).Text);
				((TextBox) Page.FindControl("TextboxSearchContactID")).Text = String.Empty;
				((TextBox) Page.FindControl("TextboxSearchContact")).Text = String.Empty;
			}
			if (((TextBox) Page.FindControl("TextboxSearchLeadID")).Text.Length > 0)
			{
				p.AppendFormat(" (LEADID={0}) AND ", ((TextBox) Page.FindControl("TextboxSearchLeadID")).Text);
				((TextBox) Page.FindControl("TextboxSearchLeadID")).Text = String.Empty;
				((TextBox) Page.FindControl("TextboxSearchLead")).Text = String.Empty;
			}
			if (((TextBox) Page.FindControl("TextboxOpportunityID")).Text.Length > 0)
			{
				p.AppendFormat(" (OPPORTUNITYID={0}) AND ", ((TextBox) Page.FindControl("TextboxOpportunityID")).Text);
				((TextBox) Page.FindControl("TextboxOpportunityID")).Text = String.Empty;
				((TextBox) Page.FindControl("TextboxOpportunity")).Text = String.Empty;
			}

			if (p.Length > 0)
				s.AppendFormat(" WHERE ({0}) ", p.ToString().Substring(0, p.Length - 4));

			if (s.ToString().IndexOf("WHERE") > 0)
				s.Append("ORDER BY ACTIVITYDATE ASC");
			else
				s.AppendFormat("ORDER BY ACTIVITYDATE ASC");


			Trace.Warn(s.ToString());

			RepeaterSearchPaging.PageSize = UC.PagingSize;
			RepeaterSearchPaging.RepeaterObj = RepeaterSearch;
			RepeaterSearchPaging.sqlRepeater = s.ToString();
			RepeaterSearchPaging.BuildGrid();


		}
	}
}
