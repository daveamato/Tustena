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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using System.Data;
using System.Web.UI;

namespace Digita.Tustena.Common
{
	public partial class PopLead : G
	{


		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				SomeJS.Text = "<script>opener.location.href=opener.location.href;self.close();</script>";
			}
			else
			{
				if (!Page.IsPostBack)
				{
					RadioList1.Items.Add(new ListItem(Root.rm.GetString("Paztxt4")));
					RadioList1.Items.Add(new ListItem(Root.rm.GetString("Paztxt5")));
					RadioList1.Items[0].Selected = true;
					NRes.RepeatDirection = RepeatDirection.Horizontal;
					NRes.RepeatColumns = 2;
				}

				NewReferrer.Visible = false;
				NewRef.Text =Root.rm.GetString("Pldtxt1");
				RapSubmit.Text =Root.rm.GetString("Prftxt12");
				string js;
				string control = Request.QueryString["textbox"].ToString();
				if (Request.QueryString["CompanyID"] != null)
				{
					if (Request.QueryString["CompanyID"].ToString().Length > 0)
						FindCompany(int.Parse(Request.QueryString["CompanyID"].ToString()));
				}
				string control2 = (Request.QueryString["textbox2"] != null) ? Request.QueryString["textbox2"].ToString() : "";
				string control3 = (Request.QueryString["textboxID"] != null) ? Request.QueryString["textboxID"].ToString() : "";
				string control4 = (Request.QueryString["textboxCompanyID"] != null) ? Request.QueryString["textboxCompanyID"].ToString() : "";

				string clickControl=null;
				string eventFunction=null;
				if(Request.QueryString["click"]!=null)
					clickControl = Request.QueryString["click"].ToString();
				if(Request.QueryString["event"]!=null)
					eventFunction = Request.QueryString["event"].ToString();

				js = "<script>";
				js += "function SetRef(tx,az,id,cid,email){";

				if (Request.QueryString["email"] != null)
					js += "dynaret('" + control + "').value=email;" + Environment.NewLine;
				else
					js += "dynaret('" + control + "').value=tx;" + Environment.NewLine;

				if (control2.Length > 0) js += "dynaret('" + control2 + "').value=az;";
				if (control3.Length > 0) js += "dynaret('" + control3 + "').value=id;";
				if (control4.Length > 0) js += "dynaret('" + control4 + "').value=cid;";

				js += "self.close();";
				if(clickControl!=null)
					js += "clickElement(dynaret('" + clickControl + "'));"+ Environment.NewLine;
				if(eventFunction!=null)
					js += "dynaevent('"+eventFunction+"');"+ Environment.NewLine;
				js += "parent.HideBox();";
				if (Request.QueryString["change"] != null) js += "	parent.PopChange('" + control3 + "');" + Environment.NewLine;

				js += "}</script>";
				SomeJS.Text = js;
				Find.Text =Root.rm.GetString("Prftxt5");
			}
		}

		private void FindCompany(int cID)
		{
			string sqlString = "SELECT CRM_LEADS.ID,(CRM_LEADS.SURNAME+' '+ISNULL(CRM_LEADS.NAME,'')) AS REFERENTE,CRM_LEADS.COMPANYNAME, CRM_LEADS.COMPANYID, CRM_LEADS.EMAIL FROM CRM_LEADS "
				+ "WHERE (CRM_LEADS.LIMBO=0 AND ACTIVE=1 AND ((" + GroupsSecure("CRM_LEADS.GROUPS") + ") OR CRM_LEADS.OWNERID=" + UC.UserId + ")) AND (CRM_LEADS.COMPANYID=@COMPANYID) ";
			DbSqlParameterCollection par = new DbSqlParameterCollection();
			par.Add(new DbSqlParameter("@COMPANYID", cID));
			string queryGroup = GroupsSecure("CRM_LEADS.GROUPS",UC);
			if (queryGroup.Length > 0)
			{
				sqlString+=String.Format(" AND ({0})", queryGroup);
			}
			if(UC.Zones.Length>0)
				sqlString+=String.Format(" AND ({0})", ZoneSecure("CRM_LEADS.COMMERCIALZONE",UC));

			sqlString+= " ORDER BY REFERENTE";
			ContactReferrer.DataSource = DatabaseConnection.SecureCreateDataset(sqlString, par);
			ContactReferrer.DataBind();
		}

		public void Find_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "Find":
					string top = String.Empty;
					string fullText = String.Empty;
					if (RadioList1.Items[1].Selected) fullText = "%";
					top = "top " + NRes.SelectedValue;
                    string sqlString = "SELECT " + top + " CRM_LEADS.ID,(CRM_LEADS.SURNAME+' '+ISNULL(CRM_LEADS.NAME,'')) AS REFERENTE,CRM_LEADS.COMPANYNAME, CRM_LEADS.COMPANYID, CRM_LEADS.EMAIL, CRM_CROSSLEAD.SALESPERSON, CRM_LEADS.OWNERID FROM CRM_LEADS INNER JOIN CRM_CROSSLEAD ON CRM_LEADS.ID=CRM_CROSSLEAD.LEADID ";
					sqlString += "WHERE (CRM_LEADS.LIMBO=0 AND ACTIVE=1 AND ((" + GroupsSecure("CRM_LEADS.GROUPS") + ") OR CRM_LEADS.OWNERID=" + UC.UserId + ")) AND (CRM_LEADS.NAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.SURNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.PHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.MOBILEPHONE LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%' OR CRM_LEADS.COMPANYNAME LIKE '" + fullText + DatabaseConnection.FilterInjection(FindIt.Text) + "%') ";
					if(UC.Zones.Length>0)
						sqlString+=String.Format(" AND ({0})", ZoneSecure("CRM_LEADS.COMMERCIALZONE",UC));

					sqlString+= " ORDER BY REFERENTE";
					ContactReferrer.DataSource = DatabaseConnection.CreateDataset(sqlString);
					ContactReferrer.DataBind();
					break;
				case "NewRef":
					Find.Visible = false;
					FindIt.Visible = false;
					ContactReferrer.Visible = false;
					NewReferrer.Visible = true;
					break;
			}
		}

		public void RapSubmit_Click(object sender, EventArgs e)
		{
			string newId = String.Empty;
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OWNERID", UC.UserId);
				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("CREATEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("NAME", Name.Text);
				dg.Add("SURNAME", Surname.Text);
				dg.Add("COMPANYNAME", CompanyName.Text);
				dg.Add("GROUPS", "|" + UC.UserGroupId + "|");
				object obNewId = dg.Execute("CRM_Leads", "ID=-1", DigiDapter.Identities.Identity);
				newId = obNewId.ToString();
			}
			DatabaseConnection.DoCommand("INSERT INTO CRM_CROSSLEAD (LEADID) VALUES (" + newId + ")");

			string js;
			string control = Request.QueryString["textbox"].ToString();
			string control3 = (Request.QueryString["textboxID"] != null) ? Request.QueryString["textboxID"].ToString() : "";
			js = "<script>";
			js += "	dynaret('" + control + "').value='" + Name.Text + " " + Surname.Text + "';";
			if (control3.Length > 0) js += "	dynaret('" + control3 + "').value=" + newId + ";";
			js += "	self.close();";
			js += "	parent.HideBox();";
			js += "</script>";
			SomeJS.Text = js;

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
			this.Find.Click += new EventHandler(this.Find_Click);
			this.NewRef.Click += new EventHandler(this.Find_Click);
			this.RapSubmit.Click += new EventHandler(this.RapSubmit_Click);
            this.ContactReferrer.ItemDataBound += new RepeaterItemEventHandler(ContactReferrer_ItemDataBound);
		}

        void ContactReferrer_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    string salesperson = (DataBinder.Eval((DataRowView)e.Item.DataItem, "SalesPerson")).ToString();
                    string ownerid = (DataBinder.Eval((DataRowView)e.Item.DataItem, "OwnerID")).ToString();
                    if (salesperson.Length > 0 && salesperson != UC.UserId.ToString() && ownerid != UC.UserId.ToString() && UC.AdminGroupId != UC.UserGroupId)
                    {
                        e.Item.Visible = false;
                    }
                    break;
            }
        }

		#endregion
	}
}
