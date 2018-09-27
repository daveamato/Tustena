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
	public partial class Dash3 : G
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
					BtnSubmit.Text =Root.rm.GetString("Dastxt8");
					ViewTableData.Text =Root.rm.GetString("Das1txt22");
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

					Drop2.Items.Add(new ListItem(Root.rm.GetString("Das3txt1"), "0"));
					Drop2.Items.Add(new ListItem(Root.rm.GetString("Das3txt2"), "1"));
					Drop2.Items.Add(new ListItem(Root.rm.GetString("Das3txt3"), "2"));
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
			switch (Drop1.SelectedValue)
			{
				case "1":
					DashPortIndustry();
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
				switch (Drop2.SelectedValue)
				{
					case "0":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(EXPECTEDREVENUE) AS NTIMES FROM DASH1_COMPANYINDUSTRY_VIEW  GROUP BY {0}", type);
						break;
					case "1":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(INCOMEPROBABILITY) AS NTIMES FROM DASH1_COMPANYINDUSTRY_VIEW GROUP BY {0}", type);
						break;
					case "2":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(AMOUNTCLOSED) AS NTIMES FROM DASH1_COMPANYINDUSTRY_VIEW GROUP BY {0}", type);
						break;
				}


				dt = DatabaseConnection.CreateDataset(dashQuery.ToString()).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					Trace.Warn("C " + dr[type].ToString().ToUpper(), dr["ntimes"].ToString());
					AddressRevenue newsales = new AddressRevenue();
					newsales.address = dr[type].ToString().ToUpper();
					newsales.revenue = Convert.ToInt32(dr["ntimes"]);
					ar.Add(newsales);
				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;

				switch (Drop2.SelectedValue)
				{
					case "0":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(EXPECTEDREVENUE) AS NTIMES FROM DASH1_LEADINDUSTRY_VIEW GROUP BY {0}", type);
						break;
					case "1":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(INCOMEPROBABILITY) AS NTIMES FROM DASH1_LEADINDUSTRY_VIEW GROUP BY {0}", type);
						break;
					case "2":
						dashQuery.AppendFormat("SELECT DISTINCT {0},SUM(AMOUNTCLOSED) AS NTIMES FROM DASH1_LEADINDUSTRY_VIEW GROUP BY {0}", type);
						break;
				}

				dt = DatabaseConnection.CreateDataset(dashQuery.ToString()).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					Trace.Warn("L " + dr[type].ToString().ToUpper(), dr["ntimes"].ToString());
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						AddressRevenue temp = (AddressRevenue) ar[i];
						if (temp.address == dr[type].ToString().ToUpper())
						{
							temp.revenue += Convert.ToDecimal(dr["ntimes"]);
							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						AddressRevenue newsales = new AddressRevenue();
						newsales.address = dr[type].ToString().ToUpper();
						newsales.revenue = Convert.ToDecimal(dr["ntimes"]);
						ar.Add(newsales);
					}

				}
			}

			decimal total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				AddressRevenue temp = (AddressRevenue) ar[i];
				total += temp.revenue;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				AddressRevenue temp = (AddressRevenue) ar[i];
				string indName = temp.revenue.ToString("c");
				double percent = Math.Round(Convert.ToDouble(temp.revenue)*100/Convert.ToDouble(total), 0);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = (temp.address.Length == 0) ?Root.rm.GetString("Das1txt14") : temp.address;
				leg.value = temp.revenue;
				leg.percent = percent;
				arlegend.Add(leg);
				res += indName + "|" + Math.Round(temp.revenue, 0) + "|";
			}

			string title = String.Empty;
			switch (type)
			{
				case "Nation":
				switch (Drop2.SelectedValue)
				{
					case "0":
						title =Root.rm.GetString("Das1txt29");
						break;
					case "1":
						title =Root.rm.GetString("Das1txt30");
						break;
					case "2":
						title =Root.rm.GetString("Das1txt31");
						break;
				}
					break;
				case "Province":
				switch (Drop2.SelectedValue)
				{
					case "0":
						title =Root.rm.GetString("Das1txt32");
						break;
					case "1":
						title =Root.rm.GetString("Das1txt33");
						break;
					case "2":
						title =Root.rm.GetString("Das1txt34");
						break;
				}
					break;
				case "City":
				switch (Drop2.SelectedValue)
				{
					case "0":
						title =Root.rm.GetString("Das1txt35");
						break;
					case "1":
						title =Root.rm.GetString("Das1txt36");
						break;
					case "2":
						title =Root.rm.GetString("Das1txt37");
						break;
				}
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

				finalQuery = dashQuery.ToString() + " ORDER BY COMPANYTYPEID";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						SalesPersonRevenue temp = (SalesPersonRevenue) ar[i];
						if (temp.Salesid == Convert.ToInt32(dr["SalesPerson"]))
						{
							switch (Drop2.SelectedValue)
							{
								case "0":
									temp.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
									break;
								case "1":
									temp.revenue += Convert.ToDecimal(dr["incomeprobability"]);
									break;
								case "2":
									temp.revenue += Convert.ToDecimal(dr["amountclosed"]);
									break;
							}

							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						SalesPersonRevenue newsales = new SalesPersonRevenue();
						newsales.Salesid = Convert.ToInt32(dr["SalesPerson"]);
						newsales.Salesname = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr["SalesPerson"].ToString());
						switch (Drop2.SelectedValue)
						{
							case "0":
								newsales.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
								break;
							case "1":
								newsales.revenue += Convert.ToDecimal(dr["incomeprobability"]);
								break;
							case "2":
								newsales.revenue += Convert.ToDecimal(dr["amountclosed"]);
								break;
						}
						ar.Add(newsales);
					}

				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;
				dashQuery.AppendFormat("SELECT * FROM DASH1_LEADINDUSTRY_VIEW");

				finalQuery = dashQuery.ToString() + " ORDER BY INDUSTRY";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						SalesPersonRevenue temp = (SalesPersonRevenue) ar[i];
						if (temp.Salesid == Convert.ToInt32(dr["SalesPerson"]))
						{
							switch (Drop2.SelectedValue)
							{
								case "0":
									temp.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
									break;
								case "1":
									temp.revenue += Convert.ToDecimal(dr["incomeprobability"]);
									break;
								case "2":
									temp.revenue += Convert.ToDecimal(dr["amountclosed"]);
									break;
							}

							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						SalesPersonRevenue newsales = new SalesPersonRevenue();
						newsales.Salesid = Convert.ToInt32(dr["SalesPerson"]);
						newsales.Salesname = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + dr["SalesPerson"].ToString());
						switch (Drop2.SelectedValue)
						{
							case "0":
								newsales.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
								break;
							case "1":
								newsales.revenue += Convert.ToDecimal(dr["incomeprobability"]);
								break;
							case "2":
								newsales.revenue += Convert.ToDecimal(dr["amountclosed"]);
								break;
						}
						ar.Add(newsales);
					}

				}
			}

			decimal total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				SalesPersonRevenue temp = (SalesPersonRevenue) ar[i];
				total += temp.revenue;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				SalesPersonRevenue temp = (SalesPersonRevenue) ar[i];
				string indName = temp.revenue.ToString("c");
				double percent = Math.Round(Convert.ToDouble(temp.revenue)*100/Convert.ToDouble(total), 0);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = (temp.Salesid == 0) ?Root.rm.GetString("Das1txt14") : temp.Salesname;
				leg.value = temp.revenue;
				leg.percent = percent;
				arlegend.Add(leg);
				res += indName + "|" + Math.Round(temp.revenue, 0) + "|";
			}

			try
			{
				switch (Drop2.SelectedValue)
				{
					case "0":
						piechart(Root.rm.GetString("Das1txt26"), res.Substring(0, res.Length - 1), arlegend);
						break;
					case "1":
						piechart(Root.rm.GetString("Das1txt27"), res.Substring(0, res.Length - 1), arlegend);
						break;
					case "2":
						piechart(Root.rm.GetString("Das1txt28"), res.Substring(0, res.Length - 1), arlegend);
						break;
				}
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

				finalQuery = dashQuery.ToString() + " ORDER BY COMPANYTYPEID";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						IndustryRevenue temp = (IndustryRevenue) ar[i];
						if (temp.industryId == Convert.ToInt32(dr["CompanyTypeID"]))
						{
							switch (Drop2.SelectedValue)
							{
								case "0":
									temp.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
									break;
								case "1":
									temp.revenue += Convert.ToDecimal(dr["incomeprobability"]);
									break;
								case "2":
									temp.revenue += Convert.ToDecimal(dr["amountclosed"]);
									break;
							}

							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						IndustryRevenue newindustry = new IndustryRevenue();
						newindustry.industryId = Convert.ToInt32(dr["CompanyTypeID"]);
						newindustry.industryName = dr["CompanyIndustry"].ToString();
						switch (Drop2.SelectedValue)
						{
							case "0":
								newindustry.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
								break;
							case "1":
								newindustry.revenue += Convert.ToDecimal(dr["incomeprobability"]);
								break;
							case "2":
								newindustry.revenue += Convert.ToDecimal(dr["amountclosed"]);
								break;
						}
						ar.Add(newindustry);
					}

				}
			}
			if (companyLead == "0" || companyLead == "1")
			{
				dashQuery.Length = 0;
				dashQuery.AppendFormat("SELECT * FROM DASH1_LEADINDUSTRY_VIEW");

				finalQuery = dashQuery.ToString() + " ORDER BY INDUSTRY";
				dt = DatabaseConnection.CreateDataset(finalQuery).Tables[0];

				foreach (DataRow dr in dt.Rows)
				{
					bool exists = false;
					for (int i = 0; i < ar.Count; i++)
					{
						IndustryRevenue temp = (IndustryRevenue) ar[i];
						if (temp.industryId == Convert.ToInt32(dr["Industry"]))
						{
							switch (Drop2.SelectedValue)
							{
								case "0":
									temp.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
									break;
								case "1":
									temp.revenue += Convert.ToDecimal(dr["incomeprobability"]);
									break;
								case "2":
									temp.revenue += Convert.ToDecimal(dr["amountclosed"]);
									break;
							}

							ar[i] = temp;
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						IndustryRevenue newindustry = new IndustryRevenue();
						newindustry.industryId = Convert.ToInt32(dr["Industry"]);
						newindustry.industryName = dr["LeadIndustry"].ToString();
						switch (Drop2.SelectedValue)
						{
							case "0":
								newindustry.revenue += Convert.ToDecimal(dr["expectedrevenue"]);
								break;
							case "1":
								newindustry.revenue += Convert.ToDecimal(dr["incomeprobability"]);
								break;
							case "2":
								newindustry.revenue += Convert.ToDecimal(dr["amountclosed"]);
								break;
						}
						ar.Add(newindustry);
					}

				}
			}

			decimal total = 0;
			double percTotal = 0;
			string res = String.Empty;

			for (int i = 0; i < ar.Count; i++)
			{
				IndustryRevenue temp = (IndustryRevenue) ar[i];
				total += temp.revenue;
			}

			for (int i = 0; i < ar.Count; i++)
			{
				IndustryRevenue temp = (IndustryRevenue) ar[i];
				string indName = temp.revenue.ToString("c");
				double percent = Math.Round(Convert.ToDouble(temp.revenue)*100/Convert.ToDouble(total), 0);
				percTotal += percent;
				Legenda leg = new Legenda();
				leg.title = (temp.industryId == 0) ?Root.rm.GetString("Das1txt14") : temp.industryName;
				leg.value = temp.revenue;
				leg.percent = percent;
				arlegend.Add(leg);
				res += indName + "|" + Math.Round(temp.revenue, 0) + "|";
			}

			try
			{
				switch (Drop2.SelectedValue)
				{
					case "0":
						piechart(Root.rm.GetString("Das1txt23"), res.Substring(0, res.Length - 1), arlegend);
						break;
					case "1":
						piechart(Root.rm.GetString("Das1txt24"), res.Substring(0, res.Length - 1), arlegend);
						break;
					case "2":
						piechart(Root.rm.GetString("Das1txt25"), res.Substring(0, res.Length - 1), arlegend);
						break;
				}
			}
			catch
			{
				graphTitle.Text =Root.rm.GetString("Das1txt38");
			}


		}

		private void piechart(string title, string data, ArrayList leg)
		{
			StringBuilder pie = new StringBuilder();
			StringBuilder legend = new StringBuilder();

			graphTitle.Text = title;

			legend.Append("<table cellSpacing=0 cellPadding=2 border=0 style=\"border:1px solid black\">");
			legend.Append("<tr><td style=\"border-bottom:1px solid black;font-size:1px;\">&nbsp;</td><td style=\"border-bottom:1px solid black\">&nbsp;</td><td class=normal style=\"border-bottom:1px solid black\" width=\"50px\">&nbsp;</td><td class=normal style=\"border-bottom:1px solid black\" width=\"80px\">%</td></tr>");
			Pie_chart pc = new Pie_chart();
			bool notOnlyZero = true;
			for (int i = 0; i < leg.Count; i++)
			{
				Legenda l = (Legenda) leg[i];
				legend.AppendFormat("<tr><td width=\"10px\" style=\"border-bottom:1px solid black;border-right:1px solid black\"><table cellpadding=0 cellspacing=0 width=\"10px\"><tr><td bgcolor=\"{0}\" style=\"font-size:9px\">&nbsp;</td></tr></table></td><td class=normal style=\"border-bottom:1px solid black\">{1}</td>", pc.color[i].Name, l.title);
				legend.AppendFormat("<td class=normal style=\"border-bottom:1px solid black\">{0}</td>", l.value.ToString("c"));
				legend.AppendFormat("<td class=normal style=\"border-bottom:1px solid black\">{0}</td></tr>", (l.percent >= 0) ? l.percent.ToString("###.00") + "%" : "No data");
				if (Convert.ToDecimal(l.value) != 0) notOnlyZero = false;
			}
			pc.Dispose();
			legend.Append("</table");
			Legend.Text = Legend.ToString();

			if (!notOnlyZero)
				pie.AppendFormat("<img src=\"/chart/pie.aspx?data={0}\">", data);
			else
				pie.Append("<img src=\"/chart/pie.aspx\">");

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
