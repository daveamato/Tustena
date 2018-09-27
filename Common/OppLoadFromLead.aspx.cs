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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class OppLoadFromLead : G
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
					SrcLeadbtn.Text =Root.rm.GetString("Find");
					if (Session["FromOpp"] != null)
					{
						OppID.Text = Session["FromOpp"].ToString();
						OppName.Text = DatabaseConnection.SqlScalar("SELECT TITLE FROM CRM_OPPORTUNITY WHERE ID=" + int.Parse(OppID.Text));
						Session.Remove("FromOpp");
					}
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
			this.SrcLeadbtn.Click += new EventHandler(srcLeadbtn_Click);
			this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(Repeater1_ItemDataBound);
			this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(Repeater1_ItemCommand);

		}

		#endregion

		private void srcLeadbtn_Click(object sender, EventArgs e)
		{
			Repeater1.DataSource = SrcLead.GetLeadTable();
			Repeater1.DataBind();
			Repeater1.Visible = true;
			TblSrcLead.Visible = false;
		}

		private void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					LinkButton SelectAll = (LinkButton) e.Item.FindControl("SelectAll");
					LinkButton InsertOpp = (LinkButton) e.Item.FindControl("InsertOpp");
					LinkButton ViewSearch = (LinkButton) e.Item.FindControl("ViewSearch");
					SelectAll.Text =Root.rm.GetString("InsLeadtxt1");
					InsertOpp.Text =Root.rm.GetString("InsLeadtxt2");
					ViewSearch.Text =Root.rm.GetString("InsLeadtxt3");
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					break;
			}
		}

		private void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "SelectAll":
					foreach (RepeaterItem ri in Repeater1.Items)
					{
						CheckBox ck = (CheckBox) ri.FindControl("ChkForInsert");
						ck.Checked = !(ck.Checked);
					}
					break;
				case "InsertOpp":
					foreach (RepeaterItem ri in Repeater1.Items)
					{
						CheckBox ck = (CheckBox) ri.FindControl("ChkForInsert");
						if (ck.Checked)
						{
							Literal ID = (Literal) ri.FindControl("ID");
							InsertNewOpportunity(int.Parse(OppID.Text), int.Parse(ID.Text), "1");
						}
					}
					Response.Redirect("/crm/crm_opportunity.aspx?o=" + OppID.Text + "&tab=1");
					break;
				case "ViewSearch":
					Repeater1.Visible = false;
					TblSrcLead.Visible = true;
					break;
			}
		}

		public void InsertNewOpportunity(int opid, int id, string type)
		{
			if(((int)DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT WHERE OPPORTUNITYID="+opid+" AND CONTACTID="+id+" AND CONTACTTYPE="+type))<=0)
			{
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("OPPORTUNITYID", opid);
					dg.Add("CONTACTID", id);
					dg.Add("CONTACTTYPE", type);
					dg.Add("EXPECTEDREVENUE", 0);
					dg.Add("AMOUNTCLOSED", 0);
					dg.Add("INCOMEPROBABILITY", 0);
					dg.Add("NOTE", String.Empty);
					dg.Add("STARTDATE", DateTime.Now);
					dg.Add("ESTIMATEDCLOSEDATE", DateTime.Now.AddDays(Convert.ToDouble(DatabaseConnection.SqlScalar("SELECT ESTIMATEDDATEDAYS FROM TUSTENA_DATA"))));

					dg.Add("ENDDATE", DBNull.Value);

					dg.Add("SALESPERSON", UC.UserId);

					dg.Add("CREATEDBYID", UC.UserId);
					dg.Add("CREATEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
					dg.Add("LASTMODIFIEDBYID", UC.UserId);
					dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
					dg.Execute("CRM_OpportunityContact");
				}

				DataTable dtCross = DatabaseConnection.CreateDataset("SELECT COMPETITORID FROM CRM_OPPORTUNITYCOMPETITOR WHERE OPPORTUNITYID=" + opid).Tables[0];
				foreach (DataRow dr in dtCross.Rows)
				{
					if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM CRM_CROSSCONTACTCOMPETITOR WHERE CONTACTTYPE=" + type + " AND COMPETITORID=" + dr[0].ToString() + " AND CONTACTID=" + id)) == 0)
					{
						DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSCONTACTCOMPETITOR (COMPETITORID,CONTACTID,CONTACTTYPE) VALUES (" + dr[0].ToString() + "," + id + "," + type + ")");
					}
				}
			}
		}
	}
}
