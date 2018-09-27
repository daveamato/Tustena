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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Dash
{
	public partial class Dash4 : G
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
					tableData.Visible = true;
					graphResult.Visible = false;
					ViewTableData.Text =Root.rm.GetString("Das1txt22");
					for (int y = 2003; y < 2011; y++)
					{
						YearFrom.Items.Add(new ListItem(y.ToString()));
						YearTo.Items.Add(new ListItem(y.ToString()));
						if (DateTime.Now.Year == y)
						{
							YearFrom.SelectedIndex = YearFrom.Items.Count - 1;
							YearTo.SelectedIndex = YearTo.Items.Count - 1;
						}
					}
					for (int m = 1; m < 13; m++)
					{
						MonthFrom.Items.Add(new ListItem(m.ToString()));
						MonthTo.Items.Add(new ListItem(m.ToString()));
						if (DateTime.Now.Month == m)
							MonthTo.SelectedIndex = MonthTo.Items.Count - 1;
					}
					for (int d = 1; d < 32; d++)
					{
						DayFrom.Items.Add(new ListItem(d.ToString()));
						DayTo.Items.Add(new ListItem(d.ToString()));
						if (DateTime.Now.Day == d)
							DayTo.SelectedIndex = DayTo.Items.Count - 1;
					}

					DataTable dtsales;
dtsales = DatabaseConnection.CreateDataset("SELECT DISTINCT SALESPERSON FROM CRM_OPPORTUNITYCONTACT").Tables[0];
					string salesId = String.Empty;
					foreach (DataRow dr in dtsales.Rows)
					{
						salesId += " UID=" + dr[0].ToString() + " OR ";
					}
					if (salesId.Length > 0)
					{
						salesId = salesId.Substring(0, salesId.Length - 3);
						SalesNameRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(SURNAME+' '+NAME) AS SALESNAME FROM ACCOUNT WHERE (" + salesId + ")").Tables[0];
					}
					else
						SalesNameRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT UID,(SURNAME+' '+NAME) AS SALESNAME FROM ACCOUNT").Tables[0];
					SalesNameRepeater.DataBind();


					vsType.Items.Add(new ListItem(Root.rm.GetString("Das4txt12"), "0"));
					vsType.Items.Add(new ListItem(Root.rm.GetString("Das4txt13"), "1"));
					vsType.Items.Add(new ListItem(Root.rm.GetString("Das4txt14"), "2"));
				}
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
			this.Load += new EventHandler(this.Page_Load);
			this.BtnSubmit.Click += new EventHandler(SubmitBtn_Click);
			this.ViewTableData.Click += new EventHandler(ViewTableData_Click);
		}

		#endregion

		private void SubmitBtn_Click(object sender, EventArgs e)
		{
			switch (vsType.SelectedValue)
			{
				case "0":
					Comparison0();
					break;
				case "1":
					Comparison1();
					break;
				case "2":
					Comparison2();
					break;
			}
		}

		private void Comparison0()
		{
			string sid = getSalesID();
			DateTime fromDate = new DateTime();
			DateTime toDate = new DateTime();
			try
			{
				fromDate = new DateTime(Convert.ToInt32(YearFrom.SelectedValue), Convert.ToInt32(MonthFrom.SelectedValue), Convert.ToInt32(DayFrom.SelectedValue));
			}
			catch
			{
				fromDate = new DateTime(2003, 1, 1);
			}
			try
			{
				toDate = new DateTime(Convert.ToInt32(YearTo.SelectedValue), Convert.ToInt32(MonthTo.SelectedValue), Convert.ToInt32(DayTo.SelectedValue));
			}
			catch
			{
				toDate = new DateTime(2010, 1, 1);
			}

			DataTable data;
			if (sid.Length > 0)
			{
				data = DatabaseConnection.CreateDataset("SELECT DISTINCT SALESPERSON,SUM(EXPECTEDREVENUE) AS REVENUE,SUM(AMOUNTCLOSED) AS AMOUNT FROM CRM_OPPORTUNITYCONTACT WHERE (" + sid + ") AND ENDDATE BETWEEN '" +UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd") + "' AND '" +UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd") + "' GROUP BY SALESPERSON").Tables[0];
			}
			else
			{
				data = DatabaseConnection.CreateDataset("SELECT DISTINCT SALESPERSON,SUM(EXPECTEDREVENUE) AS REVENUE,SUM(AMOUNTCLOSED) AS AMOUNT FROM CRM_OPPORTUNITYCONTACT WHERE ENDDATE BETWEEN '" +UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd") + "' AND '" +UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd") + "' GROUP BY SALESPERSON").Tables[0];
			}


			string chartData = String.Empty;
			foreach (DataRow dr in data.Rows)
			{
				chartData += DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr[0].ToString()) + "|" + dr[1].ToString() + "|" + dr[2].ToString() + "|";
			}
			if (chartData.Length > 0)
			{
				chartData = chartData.Substring(0, chartData.Length - 1);
				Result.Text = string.Format("<img src=\"/chart/doublebar.aspx?data={0}\">", chartData);
			}

			Legend.Text = "<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">";
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"b7cee2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr>",Root.rm.GetString("Das4txt9"));
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"fbfbf2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr></table>",Root.rm.GetString("Das4txt10"));

			graphTitle.Text =Root.rm.GetString("Das4txt11");
			tableData.Visible = false;
			graphResult.Visible = true;
			SalesNameRepeater.Visible = false;


		}

		private void Comparison1()
		{
			string sid = getSalesID();
			DateTime fromDate = new DateTime();
			DateTime toDate = new DateTime();
			try
			{
				fromDate = new DateTime(Convert.ToInt32(YearFrom.SelectedValue), Convert.ToInt32(MonthFrom.SelectedValue), Convert.ToInt32(DayFrom.SelectedValue));
			}
			catch
			{
				fromDate = new DateTime(2003, 1, 1);
			}
			try
			{
				toDate = new DateTime(Convert.ToInt32(YearTo.SelectedValue), Convert.ToInt32(MonthTo.SelectedValue), Convert.ToInt32(DayTo.SelectedValue));
			}
			catch
			{
				toDate = new DateTime(2010, 1, 1);
			}

			DataTable data;
			StringBuilder q = new StringBuilder();
			if (sid.Length > 0)
			{
				q.Append("SELECT DISTINCT SALESPERSON, ");
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND AMOUNTCLOSED=0), ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND AMOUNTCLOSED>0) ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("FROM CRM_OPPORTUNITYCONTACT O1 ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("GROUP BY SALESPERSON");
				data = DatabaseConnection.CreateDataset(q.ToString()).Tables[0];
			}
			else
			{
				q.Append("SELECT DISTINCT SALESPERSON, ");
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND AMOUNTCLOSED=0), ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND AMOUNTCLOSED>0) ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("FROM CRM_OPPORTUNITYCONTACT O1 ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("GROUP BY SALESPERSON");
				data = DatabaseConnection.CreateDataset(q.ToString()).Tables[0];
			}


			string chartData = String.Empty;
			foreach (DataRow dr in data.Rows)
			{
				chartData += DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr[0].ToString()) + "|" + dr[1].ToString() + "|" + dr[2].ToString() + "|";
			}
			if (chartData.Length > 0)
			{
				chartData = chartData.Substring(0, chartData.Length - 1);
				Result.Text = string.Format("<img src=\"/chart/doublebar.aspx?data={0}\">", chartData);
			}
			Legend.Text = "<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">";
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"b7cee2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr>",Root.rm.GetString("Das4txt16"));
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"fbfbf2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr></table>",Root.rm.GetString("Das4txt17"));

			graphTitle.Text =Root.rm.GetString("Das4txt18");
			tableData.Visible = false;
			graphResult.Visible = true;
			SalesNameRepeater.Visible = false;
		}

		private void Comparison2()
		{
			string sid = getSalesID();
			DateTime fromDate = new DateTime();
			DateTime toDate = new DateTime();
			try
			{
				fromDate = new DateTime(Convert.ToInt32(YearFrom.SelectedValue), Convert.ToInt32(MonthFrom.SelectedValue), Convert.ToInt32(DayFrom.SelectedValue));
			}
			catch
			{
				fromDate = new DateTime(2003, 1, 1);
			}
			try
			{
				toDate = new DateTime(Convert.ToInt32(YearTo.SelectedValue), Convert.ToInt32(MonthTo.SelectedValue), Convert.ToInt32(DayTo.SelectedValue));
			}
			catch
			{
				toDate = new DateTime(2010, 1, 1);
			}

			DataTable data;
			StringBuilder q = new StringBuilder();
			if (sid.Length > 0)
			{
				q.Append("SELECT DISTINCT SALESPERSON, ");
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND DATEDIFF(DD,ESTIMATEDCLOSEDATE,ENDDATE)<0), ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND DATEDIFF(DD,ESTIMATEDCLOSEDATE,ENDDATE)>0) ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("FROM CRM_OPPORTUNITYCONTACT O1 ");
				q.AppendFormat("WHERE ({2}) AND ENDDATE BETWEEN '{0}' AND '{1}' ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"), sid);
				q.Append("group by SalesPerson");
				data = DatabaseConnection.CreateDataset(q.ToString()).Tables[0];
			}
			else
			{
				q.Append("SELECT DISTINCT SALESPERSON, ");
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND DATEDIFF(DD,ESTIMATEDCLOSEDATE,ENDDATE)<0), ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("(SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' AND SALESPERSON=O1.SALESPERSON AND DATEDIFF(DD,ESTIMATEDCLOSEDATE,ENDDATE)>0) ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("FROM CRM_OPPORTUNITYCONTACT O1 ");
				q.AppendFormat("WHERE ENDDATE BETWEEN '{0}' AND '{1}' ",UC.LTZ.ToUniversalTime(fromDate).ToString(@"yyyyMMdd"),UC.LTZ.ToUniversalTime(toDate).ToString(@"yyyyMMdd"));
				q.Append("GROUP BY SALESPERSON");
				data = DatabaseConnection.CreateDataset(q.ToString()).Tables[0];
			}


			string chartData = String.Empty;
			foreach (DataRow dr in data.Rows)
			{
				chartData += DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr[0].ToString()) + "|" + dr[1].ToString() + "|" + dr[2].ToString() + "|";
			}
			if (chartData.Length > 0)
			{
				chartData = chartData.Substring(0, chartData.Length - 1);
				Result.Text = string.Format("<img src=\"/chart/doublebar.aspx?data={0}\">", chartData);
			}
			Legend.Text = "<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">";
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"b7cee2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr>",Root.rm.GetString("Das4txt19"));
			Legend.Text += string.Format("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"fbfbf2\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr></table>",Root.rm.GetString("Das4txt20"));

			graphTitle.Text =Root.rm.GetString("Das4txt21");
			tableData.Visible = false;
			graphResult.Visible = true;
			SalesNameRepeater.Visible = false;
		}

		private string getSalesID()
		{
			string opp = String.Empty;
			foreach (RepeaterItem ri in SalesNameRepeater.Items)
			{
				if (ri.ItemType == ListItemType.Header)
				{
					CheckBox HeaderCheck = (CheckBox) ri.FindControl("HeaderCheck");
					if (HeaderCheck.Checked)
						break;
				}
				if (ri.ItemType == ListItemType.Item || ri.ItemType == ListItemType.AlternatingItem)
				{
					CheckBox ItemCheck = (CheckBox) ri.FindControl("ItemCheck");
					if (ItemCheck.Checked)
					{
						opp += "SALESPERSON=" + ((Literal) ri.FindControl("IdOp")).Text + " OR ";
					}
				}
			}
			if (opp.Length > 0)
				return opp.Substring(0, opp.Length - 3);
			else
				return opp;
		}

		private void ViewTableData_Click(object sender, EventArgs e)
		{
			tableData.Visible = true;
			graphResult.Visible = false;
			SalesNameRepeater.Visible = true;
		}
	}
}
