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
using System.Web;
using System.Web.Mobile;
using System.Web.UI.MobileControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.Wap
{
	public partial class Mobile : SimpleG
	{
		protected Form Form1;
		protected ObjectListCommand Command1;


		protected void Page_Load(object sender, EventArgs e)
		{

			if(!((UC = (UserConfig)Session["UserConfig"])!=null && UC.Logged!=LoggedStatus.no))
			{
				if (!base.Device.IsMobileDevice)
				{
					LError.Text = "no mobile";
					LError.Visible = true;
				}
				if ((Request.Params["u"] != null) && (Request.Params["p"] != null))
				{
					login(Request.Params["u"].ToString(), Request.Params["p"].ToString());
				}
				else
				{
					HttpCookie Rcookie = Request.Cookies["WapLogin"];

					if (Rcookie != null && !Page.IsPostBack && Rcookie.HasKeys)
					{
						login(Rcookie["u"].ToString(), Rcookie["p"].ToString());
					}
				}
			}

			if (!Page.IsPostBack)
			{
				ContLabel.Text = SimpleG.rm.GetString("WAPtxt8");
				ContSearchAz.Text = SimpleG.rm.GetString("WAPtxt9");
				ContSearch.Text = SimpleG.rm.GetString("WAPtxt10");
				ContSearchLe.Text = SimpleG.rm.GetString("WAPtxt11");

			}
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Blogin.Click += new EventHandler(this.Blogin_Click);
			this.CalToday.Click += new EventHandler(this.CalToday_Click);
			this.CalTomorrow.Click += new EventHandler(this.CalTomorrow_Click);
			this.CallCal.Click += new EventHandler(this.CallCal_Click);
			this.Contacts.Click += new EventHandler(this.Contacts_Click);
			this.Boff.Click += new EventHandler(this.Boff_Click);
			this.MobileCalendar.SelectionChanged += new EventHandler(this.Calendar_SelectionChanged);
			this.ContSearchAz.Click += new EventHandler(this.ContSearchAz_Click);
			this.ContSearch.Click += new EventHandler(this.ContSearch_Click);
			this.ContSearchLe.Click += new EventHandler(this.ContSearchLe_Click);
			this.Bback.Click += new EventHandler(this.Bback_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		public string sError = "";

		private void Blogin_Click(object sender, EventArgs e)
		{
			HttpCookie cookie = new HttpCookie("WapLogin");
			cookie["u"] = Tusername.Text;
			cookie["p"] = Tpassword.Text;
			cookie.Expires = DateTime.Now.AddMonths(12);
			Response.Cookies.Add(cookie);
			login(Tusername.Text, Tpassword.Text);
		}

		private void login(string username, string password)
		{
			if (Authenticate(username.ToLower(), password.ToLower()))
			{
				Clearpanels();
				MenuPanel.Visible = true;
				Lwelcome.Text = SimpleG.rm.GetString("WAPtxt1") + " " + username;
				Lwelcome2.Text = SimpleG.rm.GetString("WAPtxt2");
			}
			else
			{
				LError.Text = sError;
				LError.Visible = true;
			}
		}

		private bool Authenticate(string userName, string passWord)
		{
			UserConfig UC = UserData.LoadPersonalData(userName, passWord, -1);
			if(UC.Logged==LoggedStatus.yes)
			{
				Session["UserConfig"] = UC;
				return true;
			}
			return false;

		}

		private void CalToday_Click(object sender, EventArgs e)
		{
			Appointments(DateTime.Now);
		}

		private void CalTomorrow_Click(object sender, EventArgs e)
		{
			Appointments(DateTime.Now.AddDays(1));
		}

		private void Appointments(DateTime Day)
		{
			AppTitle.Text = SimpleG.rm.GetString("WAPtxt6") + " " + Day.ToShortDateString();
			Agenda calwap = new Agenda();
			Clearpanels();
			AppPanel.Visible = true;
			Bback.Visible = true;
			AppList.Fields.Clear();
			DataTable CalendarDataTable = calwap.FillDayWap(Day, UC);
			foreach (DataRow d in CalendarDataTable.Rows)
			{
				d["StartDate"] = Convert.ToDateTime(d["StartDate"]).AddHours(UC.HTimeZone);
			}
			AppList.DataSource = CalendarDataTable;
			ObjectListField ListField1 = new ObjectListField();
			ListField1.DataField = "Contact";
			ListField1.Visible = false;
			ListField1.Title = SimpleG.rm.GetString("WAPtxt3").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Company";
			ListField1.Title = SimpleG.rm.GetString("WAPtxt4").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "StartDate";
			ListField1.Title = SimpleG.rm.GetString("WAPtxt5").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Note";
			ListField1.Title = "Note";
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Phone";
			ListField1.Title = "Phone";
			AppList.Fields.Add(ListField1);

			ListField1 = new ObjectListField();
			ListField1.DataField = "Room";
			ListField1.Title = SimpleG.rm.GetString("Evnttxt11").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Address";
			ListField1.Title = SimpleG.rm.GetString("Evnttxt12").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "City";
			ListField1.Title = SimpleG.rm.GetString("Evnttxt13").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Province";
			ListField1.Title = SimpleG.rm.GetString("Evnttxt14").ToUpper();
			AppList.Fields.Add(ListField1);
			ListField1 = new ObjectListField();
			ListField1.DataField = "Cap";
			ListField1.Title = SimpleG.rm.GetString("Evnttxt15").ToUpper();
			AppList.Fields.Add(ListField1);


			AppList.AutoGenerateFields = false;
			AppList.ItemsPerPage = 5;
			AppList.DataBind();
			AppList.Visible = true;
		}

		private void Bback_Click(object sender, EventArgs e)
		{
			Clearpanels();
			if (Session["UserConfig"] == null)
				LoginPanel.Visible = true;
			else
				MenuPanel.Visible = true;
		}


		private void test()
		{
			MobileCapabilities capabilities = new MobileCapabilities();
			capabilities = (MobileCapabilities) Request.Browser;
			LError.Text = " ScreenCharactersWidth:" + capabilities.ScreenCharactersWidth;
			LError.Text += " ScreenCharactersHeight:" + capabilities.ScreenCharactersHeight;
			LError.Text += " PreferredImageMime:" + capabilities.PreferredImageMime;
			LError.Text += " preferredRenderingMime:" + capabilities.PreferredRenderingMime;
			LError.Text += " PreferredRenderingType:" + capabilities.PreferredRenderingType;
			LError.Visible = true;
		}

		private void Boff_Click(object sender, EventArgs e)
		{
			Clearpanels();
			LoginPanel.Visible = true;
			Session.Abandon();
		}

		public void Calendar_SelectionChanged(object sender, EventArgs e)
		{
			Clearpanels();
			Appointments(MobileCalendar.SelectedDate);
		}

		private void CallCal_Click(object sender, EventArgs e)
		{
			Clearpanels();
			CalPanel.Visible = true;
			Bback.Visible = true;
		}

		private void Contacts_Click(object sender, EventArgs e)
		{
			Clearpanels();
			SearchPanel.Visible = true;
		}

		private string GroupsSecure(string table)
		{
			string[] ArryD = UC.GroupDependency.Split('|');
			string qgruppo = "";
			foreach (string ut in ArryD)
			{
				if (ut.Length > 0) qgruppo += table + ".GROUPS LIKE '%|" + ut + "|%' OR ";
			}
			if (qgruppo.Length > 0) qgruppo = qgruppo.Substring(0, qgruppo.Length - 3);
			return qgruppo;
		}

		private void ContSearchAz_Click(object sender, EventArgs e)
		{
			CompanyList.Fields.Clear();
			CompanyList.DataSource = null;
			StringBuilder query = new StringBuilder();
			query.Append("SELECT ID,COMPANYNAME,INVOICINGADDRESS,INVOICINGCITY,INVOICINGSTATEPROVINCE,INVOICINGSTATE,INVOICINGZIPCODE,PHONE,FAX,EMAIL ");
			query.AppendFormat("FROM BASE_COMPANIES WHERE (LIMBO=0 AND ({0})) ", (GroupsSecure("BASE_COMPANIES").Length > 0) ? GroupsSecure("BASE_COMPANIES") : "Base_Companies.groups like '%|" + UC.UserGroupId + "|%'");
			query.AppendFormat("AND (COMPANYNAME LIKE '%{0}%' OR PHONE LIKE '%{0}%')", ContQuery.Text);
			DataTable dt = DatabaseConnection.CreateDataset(query.ToString()).Tables[0];

			if (dt.Rows.Count > 0)
			{
				CompanyList.DataSource = dt;
				ObjectListField ListField1 = new ObjectListField();
				ListField1.DataField = "companyname";
				ListField1.Visible = false;
				ListField1.Title = SimpleG.rm.GetString("Bcotxt17");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Phone";
				ListField1.Name = "CALL";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt20");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "InvoicingAddress";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt26");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "InvoicingCity";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt27");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "InvoicingStateProvince";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt28");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "InvoicingState";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt53");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "InvoicingZIPCode";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt29");
				CompanyList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Fax";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt21");
				CompanyList.Fields.Add(ListField1);
				ListField1.DataField = "Email";
				ListField1.Name = "MAIL";
				ListField1.Title = SimpleG.rm.GetString("Bcotxt22");
				CompanyList.Fields.Add(ListField1);
				CompanyList.AutoGenerateFields = false;
				CompanyList.ItemsPerPage = 5;
				CompanyList.DataBind();
				CompanyList.Visible = true;
				Clearpanels();
				CompanyPanel.Visible = true;
				Bback.Visible = true;
				SearchInfo.Visible = false;
			}
			else
			{
				SearchInfo.Text = SimpleG.rm.GetString("WAPtxt7");
				SearchInfo.Visible = true;
			}
		}

		private void ContSearch_Click(object sender, EventArgs e)
		{
			ContactList.Fields.Clear();
			ContactList.DataSource = null;

			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT BASE_CONTACTS.ID,ISNULL(BASE_CONTACTS.NAME,'')+' '+ISNULL(BASE_CONTACTS.SURNAME,'') AS CONTACTNAME, BASE_CONTACTS.PHONE_1, BASE_CONTACTS.MOBILEPHONE_1,BASE_CONTACTS.ADDRESS_1,BASE_CONTACTS.CITY_1,BASE_CONTACTS.PROVINCE_1,BASE_CONTACTS.ZIPCODE_1,BASE_CONTACTS.STATE_1, BASE_COMPANIES.COMPANYNAME AS AZIENDATX ");
			sql.Append("FROM BASE_CONTACTS LEFT OUTER JOIN BASE_COMPANIES ON BASE_CONTACTS.COMPANYID = BASE_COMPANIES.ID ");
			sql.AppendFormat("WHERE (BASE_CONTACTS.LIMBO=0 AND ({1})) AND (BASE_CONTACTS.SURNAME LIKE '%{0}%' OR ", ContQuery.Text, (GroupsSecure("BASE_CONTACTS").Length > 0) ? GroupsSecure("BASE_CONTACTS") : "BASE_COMPANIES.GROUPS LIKE '%|" + UC.UserGroupId + "|%'");
			sql.AppendFormat("BASE_CONTACTS.NAME LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("BASE_CONTACTS.PHONE_1 LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("BASE_CONTACTS.MOBILEPHONE_1 LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("BASE_COMPANIES.COMPANYNAME LIKE '%{0}%') ", ContQuery.Text);
			DataTable dt = DatabaseConnection.CreateDataset(sql.ToString()).Tables[0];
			if (dt.Rows.Count > 0)
			{
				ContactList.DataSource = dt;
				ObjectListField ListField1 = new ObjectListField();
				ListField1.DataField = "ContactName";
				ListField1.Visible = false;
				ListField1.Title = SimpleG.rm.GetString("Reftxt9");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Phone_1";
				ListField1.Name = "CALL";
				ListField1.Title = SimpleG.rm.GetString("Reftxt11");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "MobilePhone_1";
				ListField1.Name = "CALL";
				ListField1.Title = SimpleG.rm.GetString("Reftxt12");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Address_1";
				ListField1.Title = SimpleG.rm.GetString("Reftxt28");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "City_1";
				ListField1.Title = SimpleG.rm.GetString("Reftxt29");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Province_1";
				ListField1.Title = SimpleG.rm.GetString("Reftxt30");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "ZIPCode_1";
				ListField1.Title = SimpleG.rm.GetString("Reftxt31");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "State_1";
				ListField1.Title = SimpleG.rm.GetString("Reftxt53");
				ContactList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "AZIENDATX";
				ListField1.Title = SimpleG.rm.GetString("Reftxt10");
				ContactList.Fields.Add(ListField1);
				ContactList.AutoGenerateFields = false;
				ContactList.ItemsPerPage = 5;
				ContactList.DataBind();
				ContactList.Visible = true;

				Clearpanels();
				ContactPanel.Visible = true;
				Bback.Visible = true;
				SearchInfo.Visible = false;


			}
			else
			{
				SearchInfo.Text = SimpleG.rm.GetString("WAPtxt7");
				SearchInfo.Visible = true;
			}
		}

		private void Clearpanels()
		{
			LoginPanel.Visible = false;
			AppPanel.Visible = false;
			CompanyPanel.Visible = false;
			ContactPanel.Visible = false;
			LeadPanel.Visible = false;
			MenuPanel.Visible = false;
			CalPanel.Visible = false;
			SearchPanel.Visible = false;
			Bback.Visible = false;
		}

		private void ContSearchLe_Click(object sender, EventArgs e)
		{
			LeadList.Fields.Clear();
			LeadList.DataSource = null;

			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT ID,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACTNAME, PHONE, MOBILEPHONE, ADDRESS, CITY, PROVINCE, ZIPCODE, STATE, COMPANYNAME ");
			sql.Append("FROM CRM_LEADS ");
			sql.AppendFormat("WHERE (LIMBO=0 AND ({1})) AND (SURNAME LIKE '%{0}%' OR ", ContQuery.Text, (GroupsSecure("CRM_Leads").Length > 0) ? GroupsSecure("CRM_LEADS") : "BASE_COMPANIES.GROUPS LIKE '%|" + UC.UserGroupId + "|%'");
			sql.AppendFormat("NAME LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("PHONE LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("MOBILEPHONE LIKE '%{0}%' OR ", ContQuery.Text);
			sql.AppendFormat("COMPANYNAME LIKE '%{0}%') ", ContQuery.Text);
			DataTable dt = DatabaseConnection.CreateDataset(sql.ToString()).Tables[0];
			if (dt.Rows.Count > 0)
			{
				LeadList.DataSource = dt;
				ObjectListField ListField1 = new ObjectListField();
				ListField1.DataField = "ContactName";
				ListField1.Visible = false;
				ListField1.Title = SimpleG.rm.GetString("Reftxt9");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "CompanyName";
				ListField1.Title = SimpleG.rm.GetString("Reftxt10");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Phone";
				ListField1.Name = "CALL";
				ListField1.Title = SimpleG.rm.GetString("Reftxt11");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "MobilePhone";
				ListField1.Name = "CALL";
				ListField1.Title = SimpleG.rm.GetString("Reftxt12");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Address";
				ListField1.Title = SimpleG.rm.GetString("Reftxt28");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "City";
				ListField1.Title = SimpleG.rm.GetString("Reftxt29");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "Province";
				ListField1.Title = SimpleG.rm.GetString("Reftxt30");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "ZIPCode";
				ListField1.Title = SimpleG.rm.GetString("Reftxt31");
				LeadList.Fields.Add(ListField1);
				ListField1 = new ObjectListField();
				ListField1.DataField = "State";
				ListField1.Title = SimpleG.rm.GetString("Reftxt53");
				LeadList.Fields.Add(ListField1);

				LeadList.AutoGenerateFields = false;
				LeadList.ItemsPerPage = 5;
				LeadList.DataBind();
				LeadList.Visible = true;
				Clearpanels();
				LeadPanel.Visible = true;
				Bback.Visible = true;
				SearchInfo.Visible = false;
			}
			else
			{
				SearchInfo.Text = SimpleG.rm.GetString("WAPtxt7");
				SearchInfo.Visible = true;
			}
		}
	}

}
