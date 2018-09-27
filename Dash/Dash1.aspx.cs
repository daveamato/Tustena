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
using System.Collections;
using System.Data;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Dash
{
	public partial class Dash1 : G
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
				if (!Page.IsPostBack)
				{
					ViewTableData.Text =Root.rm.GetString("Das1txt22");
					SubmitBtn.Text =Root.rm.GetString("Dastxt8");
					RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Das1txt1"), "0"));
					RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Das1txt2"), "1"));
					RadioButtonList1.Items.Add(new ListItem(Root.rm.GetString("Das1txt3"), "2"));
					RadioButtonList1.RepeatDirection = RepeatDirection.Vertical;
					RadioButtonList1.SelectedIndex = 0;

					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt4"), "0"));
					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt5"), "1"));
					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt7"), "3"));
					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt9"), "5"));
					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt10"), "6"));
					Drop1.Items.Add(new ListItem(Root.rm.GetString("Das1txt11"), "7"));
					Drop1.SelectedIndex = 0;

					OpportunityRepeater.DataSource = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY").Tables[0];
					OpportunityRepeater.DataBind();
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
			this.ViewTableData.Click += new EventHandler(this.ViewTableData_Click);
			this.SubmitBtn.Click += new EventHandler(this.SubmitBtn_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void SubmitBtn_Click(object sender, EventArgs e)
		{
			switch (Drop1.SelectedValue)
			{
				case "1":
					DashPortIndustry();
					break;
				case "2":
					break;
				case "3":
					DashPortSalesPerson();
					break;
				case "5":
					DashPortAddress("Nation");
					break;
				case "6":
					DashPortAddress("Province");
					break;
				case "7":
					DashPortAddress("City");
					break;
			}
		}

		private void DashPortAddress(string type)
		{
			string companyLead = RadioButtonList1.SelectedValue;
			Trace.Warn("companylead", companyLead);
			StringBuilder dashQuery = new StringBuilder();
			ArrayList ar = new ArrayList();
			ArrayList arlegend = new ArrayList();

			DataTable dt;
			if (companyLead == "0" || companyLead == "2")
			{
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " AND (" + opp + ")";
				dashQuery.AppendFormat("SELECT DISTINCT {0},COUNT({0}) AS NTIMES FROM DASH1_COMPANYINDUSTRY_VIEW WHERE {1} GROUP BY {0}", type, opp);

				dt = DatabaseConnection.CreateDataset(dashQuery.ToString()).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					Trace.Warn("C " + dr[type].ToString().ToUpper(), dr["ntimes"].ToString());
					AddressPercent newsales = new AddressPercent();
					newsales.address = dr[type].ToString().ToUpper();
					newsales.ntimes = Convert.ToInt32(dr["ntimes"]);
					ar.Add(newsales);
				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " AND (" + opp + ")";
				dashQuery.AppendFormat("SELECT DISTINCT {0},COUNT({0}) AS NTIMES FROM DASH1_LEADINDUSTRY_VIEW WHERE {1} GROUP BY {0}", type, opp);

				dt = DatabaseConnection.CreateDataset(dashQuery.ToString()).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					Trace.Warn("L " + dr[type].ToString().ToUpper(), dr["ntimes"].ToString());
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						AddressPercent temp = (AddressPercent) ar[i];
						if (temp.address == dr[type].ToString().ToUpper())
						{
							temp.ntimes += Convert.ToInt32(dr["ntimes"]);
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						AddressPercent newsales = new AddressPercent();
						newsales.address = dr[type].ToString().ToUpper();
						newsales.ntimes = Convert.ToInt32(dr["ntimes"]);
						ar.Add(newsales);
					}

				}
			}

			int total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				AddressPercent temp = (AddressPercent) ar[i];
				total += temp.ntimes;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				AddressPercent temp = (AddressPercent) ar[i];
				string nationName = (temp.address.Length == 0) ?Root.rm.GetString("Das1txt14") : temp.address;
				double percent = Math.Round(Convert.ToDouble(temp.ntimes)*100/Convert.ToDouble(total), 3);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = nationName;
				leg.value = temp.ntimes;
				leg.percent = percent;
				arlegend.Add(leg);
				res += nationName + "|" + temp.ntimes + "|";
			}

			string title = String.Empty;
			switch (type)
			{
				case "Nation":
					title =Root.rm.GetString("Das1txt19");
					break;
				case "Province":
					title =Root.rm.GetString("Das1txt20");
					break;
				case "City":
					title =Root.rm.GetString("Das1txt21");
					break;
			}

			try
			{
				piechart(title, res.Substring(0, res.Length - 1), arlegend);
			}
			catch
			{
				graphTitle.Text =Root.rm.GetString("Das1txt38");
			}

		}

		private void DashPortSalesPerson()
		{
			string companyLead = RadioButtonList1.SelectedValue;

			StringBuilder dashQuery = new StringBuilder();
			ArrayList ar = new ArrayList();
			ArrayList arlegend = new ArrayList();
			string finalQuery = String.Empty;
			DataTable dt;
			if (companyLead == "0" || companyLead == "2")
			{
				dashQuery.AppendFormat("SELECT * FROM DASH1_COMPANYINDUSTRY_VIEW");
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " AND (" + opp + ")";
				finalQuery = dashQuery.ToString() + opp + " ORDER BY COMPANYTYPEID";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						SalesPersonPercent temp = (SalesPersonPercent) ar[i];
						if (temp.Salesid == Convert.ToInt32(dr["SalesPerson"]))
						{
							temp.ntimes = temp.ntimes + 1;
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						SalesPersonPercent newsales = new SalesPersonPercent();
						newsales.Salesid = Convert.ToInt32(dr["SalesPerson"]);
						newsales.Salesname = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr["SalesPerson"].ToString());
						newsales.ntimes = 1;
						ar.Add(newsales);
					}

				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;
				dashQuery.AppendFormat("SELECT * FROM DASH1_LEADINDUSTRY_VIEW");
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " and (" + opp + ")";
				finalQuery = dashQuery.ToString() + opp + " ORDER BY INDUSTRY";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						SalesPersonPercent temp = (SalesPersonPercent) ar[i];
						if (temp.Salesid == Convert.ToInt32(dr["SalesPerson"]))
						{
							temp.ntimes = temp.ntimes + 1;
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						SalesPersonPercent newsales = new SalesPersonPercent();
						newsales.Salesid = Convert.ToInt32(dr["SalesPerson"]);
						newsales.Salesname = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr["SalesPerson"].ToString());
						newsales.ntimes = 1;
						ar.Add(newsales);
					}

				}
			}

			int total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				SalesPersonPercent temp = (SalesPersonPercent) ar[i];
				total += temp.ntimes;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				SalesPersonPercent temp = (SalesPersonPercent) ar[i];
				string salesName = (temp.Salesid == 0) ?Root.rm.GetString("Das1txt14") : temp.Salesname;
				double percent = Math.Round(Convert.ToDouble(temp.ntimes)*100/Convert.ToDouble(total), 3);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = salesName;
				leg.value = temp.ntimes;
				leg.percent = percent;
				arlegend.Add(leg);
				res += salesName + "|" + temp.ntimes + "|";

			}


			try
			{
				piechart(Root.rm.GetString("Das1txt18"), res.Substring(0, res.Length - 1), arlegend);
			}
			catch
			{
				graphTitle.Text =Root.rm.GetString("Das1txt38");
			}

		}


		private void DashPortIndustry()
		{
			string companyLead = RadioButtonList1.SelectedValue;

			StringBuilder dashQuery = new StringBuilder();
			ArrayList ar = new ArrayList();
			ArrayList arlegend = new ArrayList();
			string finalQuery = String.Empty;
			DataTable dt;
			if (companyLead == "0" || companyLead == "2")
			{
				dashQuery.AppendFormat("SELECT * FROM DASH1_COMPANYINDUSTRY_VIEW");
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " AND (" + opp + ")";
				finalQuery = dashQuery.ToString() + opp + " ORDER BY COMPANYTYPEID";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						IndustryPercent temp = (IndustryPercent) ar[i];
						if (temp.industryid == Convert.ToInt32(dr["CompanyTypeID"]))
						{
							temp.ntimes = temp.ntimes + 1;
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						IndustryPercent newindustry = new IndustryPercent();
						newindustry.industryid = Convert.ToInt32(dr["CompanyTypeID"]);
						newindustry.industryname = dr["CompanyIndustry"].ToString();
						newindustry.ntimes = 1;
						ar.Add(newindustry);
					}

				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;
				dashQuery.AppendFormat("SELECT * FROM DASH1_LEADINDUSTRY_VIEW");
				string opp = getOpportunity();
				if (opp.Length > 0) opp = " AND (" + opp + ")";
				finalQuery = dashQuery.ToString() + opp + " ORDER BY INDUSTRY";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						IndustryPercent temp = (IndustryPercent) ar[i];
						if (temp.industryid == Convert.ToInt32(dr["Industry"]))
						{
							temp.ntimes = temp.ntimes + 1;
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						IndustryPercent newindustry = new IndustryPercent();
						newindustry.industryid = Convert.ToInt32(dr["Industry"]);
						newindustry.industryname = dr["LeadIndustry"].ToString();
						newindustry.ntimes = 1;
						ar.Add(newindustry);
					}

				}
			}

			int total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				IndustryPercent temp = (IndustryPercent) ar[i];
				total += temp.ntimes;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				IndustryPercent temp = (IndustryPercent) ar[i];
				string indName = (temp.industryid == 0) ?Root.rm.GetString("Das1txt14") : temp.industryname;
				double percent = Math.Round(Convert.ToDouble(temp.ntimes)*100/Convert.ToDouble(total), 0);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = indName;
				leg.value = temp.ntimes;
				leg.percent = percent;
				arlegend.Add(leg);
				res += indName + "|" + temp.ntimes + "|";
			}

			try
			{
				piechart(Root.rm.GetString("Das1txt17"), res.Substring(0, res.Length - 1), arlegend);
			}
			catch
			{
				graphTitle.Text =Root.rm.GetString("Das1txt38");
			}

		}

		private string getOpportunity()
		{
			string opp = String.Empty;
			foreach (RepeaterItem ri in OpportunityRepeater.Items)
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
						opp += "OPPORTUNITYID=" + ((Literal) ri.FindControl("IdOp")).Text + " OR ";
					}
				}
			}
			if (opp.Length > 0)
				return opp.Substring(0, opp.Length - 3);
			else
				return opp;
		}

		private void piechart(string title, string data, ArrayList leg)
		{
			StringBuilder pie = new StringBuilder();
			StringBuilder legend = new StringBuilder();

			graphTitle.Text = title;

			legend.Append("<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">");
			legend.Append("<tr><td style=\"border-bottom:1px solid black;font-size:1px;\">&nbsp;</td><td style=\"border-bottom:1px solid black\">&nbsp;</td><td class=normal style=\"border-bottom:1px solid black\" width=\"50px\">n</td><td class=normal style=\"border-bottom:1px solid black\" width=\"80px\">%</td></tr>");
			Pie_chart pc = new Pie_chart();
			for (int i = 0; i < leg.Count; i++)
			{
				Legenda l = (Legenda) leg[i];
				legend.AppendFormat("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"{0}\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{1}</td>", pc.color[i].Name, l.title);
				legend.AppendFormat("<td class=normal style=\"border-bottom:1px solid black\">{0}</td>", l.value.ToString());
				legend.AppendFormat("<td class=normal style=\"border-bottom:1px solid black\">{0}%</td></tr>", l.percent.ToString("###.00"));
			}
			pc.Dispose();
			legend.Append("</table");
			Legend.Text = legend.ToString();
			pie.AppendFormat("<img src=\"/chart/pie.aspx?data={0}\">", data);
			Trace.Warn(pie.ToString());
			Result.Text = pie.ToString();
			tableData.Visible = false;
			graphResult.Visible = true;
		}

		private void ViewTableData_Click(object sender, EventArgs e)
		{
			tableData.Visible = true;
			graphResult.Visible = false;
		}
	}
}
