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
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Admin
{
	public partial class Limbo : G
	{

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (!M.IsModule(ActiveModules.Lead))
                BtnLead.Visible = false;

            base.OnPreRenderComplete(e);
        }


		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				BtnCompany.Text =Root.rm.GetString("Limtxt2");
				BtnContact.Text =Root.rm.GetString("Limtxt3");
				BtnLead.Text = "Lead";



				if (!Page.IsPostBack)
				{
					ContactContainer.Visible = false;
					CompanyContainer.Visible = false;
					LeadContainer.Visible = false;
				}

			}
		}



		public void Btn_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "BtnCompany":
					ContactContainer.Visible = false;
					CompanyContainer.Visible = true;
					LeadContainer.Visible = false;
					FillRepeater();
					break;
				case "BtnContact":
					CompanyContainer.Visible = false;
					ContactContainer.Visible = true;
					LeadContainer.Visible = false;
					Repeater1.Visible = false;

					RepeaterInfo.Visible = false;
					FillRepeaterC();
					break;
				case "BtnLead":
					LeadContainer.Visible = true;
					CompanyContainer.Visible = false;
					ContactContainer.Visible = false;
					this.FillLeadRepeater();
					break;
			}
		}

		public void Repeater1_ItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton lk = (LinkButton) e.Item.FindControl("Delete");
					lk.Text =Root.rm.GetString("CRMcontxt44");
					lk.Visible = true;
					lk.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Bcotxt44") + "');");
					lk = (LinkButton) e.Item.FindControl("Revert");
					lk.Text =Root.rm.GetString("Limtxt0");
					break;
			}
		}

		public void Repeater1_Grid_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Delete":
					DeleteCompany(int.Parse(((Literal) e.Item.FindControl("IDCat")).Text));
					break;
				case "Revert":
					RecoverCompany(int.Parse(((Literal) e.Item.FindControl("IDCat")).Text));
					break;
			}
		}

		private void DeleteCompany(int id)
		{
			string delSql = "DELETE FROM BASE_COMPANIES WHERE ID=" + id;
			DatabaseConnection.DoCommand(delSql);
			FillRepeater();
		}

		private void RecoverCompany(int id)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("LASTACTIVITY", 1); //MODIFICA
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("LIMBO", 0);
				dg.Execute("BASE_COMPANIES", "ID=" + id);
			}

			FillRepeater();
		}

		private void FillRepeater()
		{
			StringBuilder sqlString = new StringBuilder();

			sqlString.Append("SELECT BASE_COMPANIES.*, ");
			sqlString.Append("(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS OWNER ");
			sqlString.Append("FROM BASE_COMPANIES ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ON BASE_COMPANIES.OWNERID = ACCOUNT.UID");

			sqlString.Append(" WHERE LIMBO=1");


			sqlString.Append(" ORDER BY BASE_COMPANIES.COMPANYNAME");

			Repeaterpaging1.PageSize = UC.PagingSize;
			Repeaterpaging1.RepeaterObj = Repeater1;
			Repeaterpaging1.sqlRepeater = sqlString.ToString();
			Repeaterpaging1.CurrentPage=0;
			Repeaterpaging1.BuildGrid();


			if (Repeater1.Items.Count > 0)
			{
				Repeater1.Visible = true;
				RepeaterInfo.Visible = false;

			}
			else
			{
				RepeaterInfo.Text = "<center>" +Root.rm.GetString("Bcotxt41") + "</center>";
				Repeater1.Visible = false;
				RepeaterInfo.Visible = true;
			}

		}

		public void Repeater2_ItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton lk = (LinkButton) e.Item.FindControl("Delete");
					lk.Text =Root.rm.GetString("CRMcontxt44");
					lk.Visible = true;
					lk.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Bcotxt44") + "');");
					lk = (LinkButton) e.Item.FindControl("Revert");
					lk.Text =Root.rm.GetString("Limtxt0");
					break;
			}
		}


		public void Repeater2_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Delete":
					DeleteContact(int.Parse(((Literal) e.Item.FindControl("IDCon")).Text));
					break;
				case "Revert":
					RecoverContact(int.Parse(((Literal) e.Item.FindControl("IDCon")).Text));
					break;
			}
		}

		private void RecoverContact(int id)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("LASTACTIVITY", 1); //MODIFICA
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("LIMBO", 0);
				dg.Execute("BASE_CONTACTS", "ID=" + id);
			}
			FillRepeaterC();
		}

		private void DeleteContact(int id)
		{
			string sqlString = "DELETE FROM BASE_CONTACTS WHERE ID=" + id;
			DatabaseConnection.DoCommand(sqlString);
			FillRepeaterC();
		}



		private void FillRepeaterC()
		{
			StringBuilder sqlString = new StringBuilder();


			sqlString.Append("SELECT BASE_CONTACTS.*, BASE_COMPANIES.COMPANYNAME AS COMPANYNAME2,(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS NAMEOWNER ");
			sqlString.Append("FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ");
			sqlString.Append("LEFT OUTER JOIN ACCOUNT ON BASE_CONTACTS.OWNERID = ACCOUNT.UID ");


			sqlString.Append("WHERE BASE_CONTACTS.LIMBO = 1 ORDER BY BASE_CONTACTS.SURNAME");

			Repeaterpaging2.PageSize = UC.PagingSize;
			Repeaterpaging2.RepeaterObj = Repeater2;
			Repeaterpaging2.sqlRepeater = sqlString.ToString();
			Repeaterpaging2.CurrentPage=0;
			Repeaterpaging2.BuildGrid();

			if (Repeater2.Items.Count > 0)
			{
				Repeater2.Visible = true;
				RepeaterInfoC.Visible = false;
			}
			else
			{
				RepeaterInfoC.Text = "<center>" +Root.rm.GetString("Reftxt1") + "</center>";
				Repeater2.Visible = false;
				RepeaterInfoC.Visible = true;
			}
		}




		private void FillLeadRepeater()
		{
			Repeater3Paging.PageSize = UC.PagingSize;
			Repeater3Paging.RepeaterObj = Repeater3;
			Repeater3Paging.sqlRepeater = "SELECT ID,NAME,SURNAME,COMPANYNAME,PHONE,MOBILEPHONE FROM CRM_LEADS WHERE LIMBO=1";
			Repeater3Paging.CurrentPage=0;
			Repeater3Paging.BuildGrid();
		}

		public void Repeater3_ItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton lk = (LinkButton) e.Item.FindControl("Delete");
					lk.Text =Root.rm.GetString("CRMcontxt44");
					lk.Visible = true;
					lk.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("Bcotxt44") + "');");
					lk = (LinkButton) e.Item.FindControl("Revert");
					lk.Text =Root.rm.GetString("Limtxt0");
					break;
			}
		}


		public void Repeater3_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Delete":
					DeleteLead(int.Parse(((Literal) e.Item.FindControl("IDCon")).Text));
					break;
				case "Revert":
					RecoverLead(int.Parse(((Literal) e.Item.FindControl("IDCon")).Text));
					break;
			}
		}

		private void RecoverLead(int id)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.UpdateOnly();
				dg.Add("LASTACTIVITY", 1); //MODIFICA
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("LIMBO", 0);
				dg.Execute("CRM_LEADS", "ID=" + id);
			}
			FillLeadRepeater();
		}

		private void DeleteLead(int id)
		{
			string delSql = "DELETE FROM CRM_LEADS WHERE ID=" + id;
			DatabaseConnection.DoCommand(delSql);
			FillLeadRepeater();
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
			this.BtnCompany.Click += new EventHandler(this.Btn_Click);
			this.BtnContact.Click += new EventHandler(this.Btn_Click);
			this.BtnLead.Click += new EventHandler(this.Btn_Click);
			this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(this.Repeater1_Grid_ItemCommand);
			this.Repeater2.ItemCommand += new RepeaterCommandEventHandler(this.Repeater2_Command);
			this.Repeater3.ItemCommand += new RepeaterCommandEventHandler(this.Repeater3_Command);
			this.Repeater1.ItemDataBound += new RepeaterItemEventHandler(this.Repeater1_ItemDataBound);
			this.Repeater2.ItemDataBound += new RepeaterItemEventHandler(this.Repeater2_ItemDataBound);
			this.Repeater3.ItemDataBound += new RepeaterItemEventHandler(this.Repeater3_ItemDataBound);

		}

		#endregion
	}
}
