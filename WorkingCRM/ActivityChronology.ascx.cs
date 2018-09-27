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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.WorkingCRM
{
	public partial class ActivityChronology : UserControl
	{
		private int parentID = 0;
		private byte _AcType = 0;
		private int opportunityID = 0;
		private bool viewCompany;
		private bool fromFrame = false;
		private string fromSheet = String.Empty;
		private GoBack _Back;
		public DateTimeFormatInfo myDTFI = CultureInfo.CurrentCulture.DateTimeFormat;
		public DateTimeFormatInfo DBCult = CultureInfo.InvariantCulture.DateTimeFormat; //new CultureInfo("en-US").DateTimeFormat;
		private UserConfig UC = new UserConfig();

		public ActivityChronology()
		{
			if (HttpContext.Current.Session.IsNewSession)
				HttpContext.Current.Response.Redirect("/login.aspx");
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			viewCompany = false;
		}

		public string FromSheet
		{
			set
			{
				this.fromSheet = value;
				ViewState["_FromSheet"] = value;
			}
		}

		public bool ViewCompany
		{
			set
			{
				this.viewCompany = value;
				ViewState["_ViewCompany"] = value;
			}
		}

		public bool FromFrame
		{
			set
			{
				this.fromFrame = value;
				ViewState["_FromFrame"] = value;
			}
		}

		public int ParentID
		{
			set
			{
				this.parentID = value;
				ViewState["_ParentID"] = value;
			}
		}

		public int OpportunityID
		{
			set
			{
				this.opportunityID = value;
				ViewState["_OpportunityID"] = value;
			}
		}

		public byte AcType
		{
			set
			{
				this._AcType = value;
				ViewState["_AcType"] = value;
			}
		}

		public GoBack Back
		{
			set
			{
				this._Back = value;
				ViewState["_Back"] = value;
			}
		}

		public int ItemCount
		{
			get { return RepCro.Items.Count; }
		}

		public void Page_Load(object sender, EventArgs e)
		{
			if(!this.Visible)
				return;
			if (this.parentID == 0) this.parentID = int.Parse(ViewState["_ParentID"].ToString());
			if (this.opportunityID == 0) this.opportunityID = int.Parse(ViewState["_OpportunityID"].ToString());
			if (this._AcType == 0) this._AcType = Convert.ToByte(ViewState["_AcType"]);

			try
			{
				this.fromSheet = ViewState["_FromSheet"].ToString();
			}
			catch
			{
				this.fromSheet = String.Empty;
			}
			try
			{
				this.viewCompany = Convert.ToBoolean(ViewState["_ViewCompany"]);
			}
			catch
			{
				this.viewCompany = false;
			}
			try
			{
				this.fromFrame = Convert.ToBoolean(ViewState["_FromFrame"]);
			}
			catch
			{
				this.fromFrame = false;
			}

		}

		public void Refresh()
		{
			if (this._Back != null)
			{
				SetGoBack();
			}
			else
			{
				if (ViewState["_Back"] != null)
					this._Back = (GoBack) ViewState["_Back"];
			}
			string dsQuery = String.Empty;
			if (this.opportunityID == 0)
			{
				switch (this._AcType)
				{
					case 1: // Azienda
						dsQuery = "SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE (CRM_WORKACTIVITY.COMPANYID=" + this.parentID + " OR CRM_WORKACTIVITY.REFERRERID IN (SELECT ID FROM BASE_CONTACTS WHERE COMPANYID=" + this.parentID + ")) AND CRM_WORKACTIVITY.PARENTID=0 ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE DESC";
						break;
					case 2: // Contact
						string complicateQuery = "SELECT TBL1.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME " +
							"FROM CRM_WORKACTIVITY AS TBL1 " +
							"LEFT OUTER JOIN ACCOUNT ON TBL1.OWNERID = ACCOUNT.UID " +
							"WHERE TBL1.REFERRERID=" + this.parentID + " AND (TBL1.PARENTID=0 " +
							"OR (TBL1.PARENTID<>0 AND (SELECT REFERRERID FROM CRM_WORKACTIVITY WHERE ID=TBL1.PARENTID) IS NULL )) " +
							"ORDER BY TBL1.ACTIVITYDATE DESC";
						dsQuery = complicateQuery;
						break;
					case 3: // Lead
						string complicateQueryLead = "SELECT TBL1.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME " +
							"FROM CRM_WORKACTIVITY AS TBL1 " +
							"LEFT OUTER JOIN ACCOUNT ON TBL1.OWNERID = ACCOUNT.UID " +
							"WHERE TBL1.LEADID=" + this.parentID + " AND (TBL1.PARENTID=0 " +
							"OR (TBL1.PARENTID<>0 AND (SELECT LEADID FROM CRM_WORKACTIVITY WHERE ID=TBL1.PARENTID) IS NULL )) " +
							"ORDER BY TBL1.ACTIVITYDATE DESC";
						dsQuery = complicateQueryLead;
						break;
				}
			}
			else
			{
				switch (this._AcType)
				{
					case 0:
						dsQuery = "SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE (CRM_WORKACTIVITY.OPPORTUNITYID=" + this.opportunityID + ") AND CRM_WORKACTIVITY.PARENTID=0 ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE DESC";
						break;
					case 1:
						dsQuery = "SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE (CRM_WORKACTIVITY.OPPORTUNITYID=" + this.opportunityID + ") AND (CRM_WORKACTIVITY.COMPANYID=" + this.parentID + " OR CRM_WORKACTIVITY.REFERRERID IN (SELECT ID FROM BASE_CONTACTS WHERE COMPANYID=" + this.parentID + ")) AND CRM_WORKACTIVITY.PARENTID=0 ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE DESC";
						break;
					case 2:
						string complicateQuery = "SELECT TBL1.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME " +
							"FROM CRM_WORKACTIVITY AS TBL1 " +
							"LEFT OUTER JOIN ACCOUNT ON TBL1.OWNERID = ACCOUNT.UID " +
							"WHERE (TBL1.OPPORTUNITYID=" + this.opportunityID + ") AND (TBL1.REFERRERID=" + this.parentID + " AND (TBL1.PARENTID=0 " +
							"OR (TBL1.PARENTID<>0 AND (SELECT REFERRERID FROM CRM_WORKACTIVITY WHERE ID=TBL1.PARENTID) IS NULL ))) " +
							"ORDER BY TBL1.ACTIVITYDATE DESC";
						dsQuery = complicateQuery;
						break;
					case 3:
						string complicateQueryLead = "SELECT TBL1.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME " +
							"FROM CRM_WORKACTIVITY AS TBL1 " +
							"LEFT OUTER JOIN ACCOUNT ON TBL1.OWNERID = ACCOUNT.UID " +
							"WHERE (TBL1.OPPORTUNITYID=" + this.opportunityID + ") AND (TBL1.LEADID=" + this.parentID + " AND (TBL1.PARENTID=0 " +
							"OR (TBL1.PARENTID<>0 AND (SELECT LEADID FROM CRM_WORKACTIVITY WHERE ID=TBL1.PARENTID) IS NULL ))) " +
							"ORDER BY TBL1.ACTIVITYDATE DESC";
						dsQuery = complicateQueryLead;
						break;
				}
			}

			Trace.Warn("accrono", dsQuery);

			RepCroPaging.PageSize = UC.PagingSize;
			RepCroPaging.RepeaterObj = RepCro;
			RepCroPaging.sqlRepeater = dsQuery;
			RepCroPaging.BuildGrid();

		}

		private void SetGoBack()
		{
			Stack backSheet = new Stack();
			if (Session["goback1"] is Stack && ((Stack) Session["goback1"]).Count > 0)
			{
				backSheet = (Stack) Session["goback1"];
			}
			backSheet.Push(this._Back);
			Session["goback1"] = backSheet;
		}

		public void RepCroDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					int id = Convert.ToInt32(DataBinder.Eval((DataRowView) e.Item.DataItem, "id"));
					int activeAccount;

					string sqlString = null;
					if (this.opportunityID == 0)
					{
						switch (this._AcType)
						{
							case 1:
								sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE COMPANYID=" + this.parentID + " AND PARENTID=" + id;
								break;
							case 2:
								sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE REFERRERID=" + this.parentID + " AND PARENTID=" + id;
								break;
							case 3:
								sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE LEADID=" + this.parentID + " AND PARENTID=" + id;
								break;
						}
					}
					else
					{
						if (this._AcType == 0)
						{
							sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE PARENTID=" + id;
						}
						else
						{
							switch (this._AcType)
							{
								case 1:
									sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE COMPANYID=" + this.parentID + " AND PARENTID=" + id;
									break;
								case 2:
									sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE REFERRERID=" + this.parentID + " AND PARENTID=" + id;
									break;
								case 3:
									sqlString = "SELECT COUNT(*) FROM CRM_WORKACTIVITY WHERE LEADID=" + this.parentID + " AND PARENTID=" + id;
									break;
							}
						}
					}
					activeAccount = (int) DatabaseConnection.SqlScalartoObj(sqlString);
					LinkButton lk = (LinkButton) e.Item.FindControl("Expand");
					if (activeAccount > 0)
						lk.Text = "<img src=/i/Tree/ElementPlus.jpg border=0>";
					else
						lk.Text = String.Empty;

					Label Subject = (Label) e.Item.FindControl("Subject");
					int aType = (int) DataBinder.Eval((DataRowView) e.Item.DataItem, "Type");
					byte todo = (byte) DataBinder.Eval((DataRowView) e.Item.DataItem, "ToDo");

					string todoImg = String.Empty;
					string cssClass = String.Empty;

					switch (todo)
					{
						case 0:
							todoImg = "<img border=0 src=/i/checkoff.gif>";
							break;
						case 1:
							todoImg = "<img border=0 src=/i/checkon.gif>";
							break;
						case 2:
							todoImg = "<img border=0 src=/i/checkout.gif>";
							cssClass = "class=\"LinethroughGray\"";
							break;
					}

					Subject.Text = "<table class=normal cellspacing=0 cellpadding=0><tr><td " + cssClass + ">";
					if (this.fromFrame)
						Subject.Text += "<span style=\"cursor:pointer\" onclick=\"parent.location.href='/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + "'\">" + ImgType(aType) + todoImg + "</span>&nbsp;";
					else
						Subject.Text += "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + ">" + ImgType(aType) + todoImg + "</a>&nbsp;";
					string sub = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Subject"));

					if (sub.Length > 100)
					{
						Regex rx = new Regex(@"(?s)\b.{1,98}\b");
						Subject.ToolTip = rx.Match(sub).Value + "";

						string companyString = String.Empty;
						if (this.viewCompany)
						{
							if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyID")).Length > 0)
							{
								companyString = "[" + DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID='" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyID")) + "'") + "]";
							}
							if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadID")).Length > 0)
							{
								companyString = "[" + DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID='" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadID")) + "'") + "]";
							}
						}
						Subject.Text += "[" + UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"), UC.myDTFI)).ToString("g") + "]&nbsp;" + companyString;
						if (this.fromFrame)
							Subject.Text += "<span style=\"cursor:pointer\" onclick=\"parent.location.href='/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + "'\">" + rx.Match(sub) + "&hellip;</span>";
						else
							Subject.Text += "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + ">" + rx.Match(sub) + "&hellip;</a>";


						Subject.Text += "</td>";
						Subject.Text += "<td align=right width=1% nowrap>[" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "OwnerName")) + "]</td></tr></table>";
					}
					else
					{
						string companyString = String.Empty;
						if (this.viewCompany)
						{
							if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyID")).Length > 0)
							{
								companyString = "[" + DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID='" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "CompanyID")) + "'") + "]&nbsp;";
							}
							if (Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadID")).Length > 0)
							{
								companyString = "[" + DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') FROM CRM_LEADS WHERE ID='" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "LeadID")) + "'") + "]&nbsp;";
							}
						}
						Subject.Text += "[" + UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "ActivityDate"), UC.myDTFI)).ToString("g") + "]&nbsp;" + companyString;
						if (this.fromFrame)
							Subject.Text += "<span style=\"cursor:pointer\" onclick=\"parent.location.href='/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + "'\">" + sub + "</span>";
						else
							Subject.Text += "<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id + "&goback=" + this.fromSheet + ">" + sub + "</a>";

						Subject.Text += "</td>";
						Subject.Text += "<td align=right width=1% nowrap>[" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "OwnerName")) + "]</td></tr></table>";
					}
					string tip = Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "Description")).Trim();
					if (tip.Length > 100)
					{
						Regex rx = new Regex(@"(?s)\b.{1,98}\b");
						Subject.ToolTip = rx.Match(tip).Value + "";
					}else
						Subject.ToolTip = tip;
					break;
			}
		}

		public void RepCroCommand(Object sender, RepeaterCommandEventArgs e)
		{
			Trace.Warn("commandname", e.CommandName);
			switch (e.CommandName)
			{
				case "Expand":
					LinkButton lk = (LinkButton) e.Item.FindControl("Expand");
					Literal Cronology = (Literal) e.Item.FindControl("Cronology");
					if (lk.Text.IndexOf("ElementPlus") > 0)
					{
						Literal ExId = (Literal) e.Item.FindControl("ExId");
						Cronology.Text = "<table class=normal cellspacing=0 cellpadding=0>";
						FillCronologia(Cronology, 0, ExId.Text);
						Cronology.Text += "</table>";
						Cronology.Visible = true;
						lk.Text = "<img src=/i/Tree/ElementMinus.jpg border=0>";
					}
					else
					{
						lk.Text = "<img src=/i/Tree/ElementPlus.jpg border=0>";
						Cronology.Text = String.Empty;
						Cronology.Visible = false;
					}
					break;
			}
		}

		public void FillCronologia(Literal c, int level, string id)
		{
			level++;
			DataSet dsSubFolder = new DataSet();
			if (this.opportunityID == 0)
			{
				switch (this._AcType)
				{
					case 1:
						dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE (CRM_WORKACTIVITY.COMPANYID=" + this.parentID + " ) AND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
						break;
					case 2:
						dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE CRM_WORKACTIVITY.REFERRERID=" + this.parentID + " AND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
						break;
					case 3:
						dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE CRM_WORKACTIVITY.LEADID=" + this.parentID + " AND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
						break;
				}
			}
			else
			{
				if (this._AcType == 0)
					dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
				else
				{
					switch (this._AcType)
					{
						case 1:
							dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE (CRM_WORKACTIVITY.COMPANYID=" + this.parentID + " ) AND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
							break;
						case 2:
							dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE CRM_WORKACTIVITY.REFERRERID=" + this.parentID + " aND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
							break;
						case 3:
							dsSubFolder = DatabaseConnection.CreateDataset("SELECT CRM_WORKACTIVITY.*, ACCOUNT.SURNAME + ' ' + ACCOUNT.NAME AS OWNERNAME FROM CRM_WORKACTIVITY LEFT OUTER JOIN ACCOUNT ON CRM_WORKACTIVITY.OWNERID = ACCOUNT.UID WHERE CRM_WORKACTIVITY.LEADID=" + this.parentID + " AND CRM_WORKACTIVITY.PARENTID=" + id + " ORDER BY CRM_WORKACTIVITY.ACTIVITYDATE");
							break;
					}
				}
			}
			if (dsSubFolder.Tables[0].Rows.Count > 0)
			{
				string indent = String.Empty;
				for (int i = 1; i <= level; i++)
					indent += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
				foreach (DataRow dr in dsSubFolder.Tables[0].Rows)
				{
					byte todo = (byte) dr["ToDo"];

					string todoImg = String.Empty;
					string cssClass = String.Empty;
					switch (todo)
					{
						case 0:
							todoImg = "<img border=0 src=/i/checkoff.gif>";
							break;
						case 1:
							todoImg = "<img border=0 src=/i/checkon.gif>";
							break;
						case 2:
							todoImg = "<img border=0 src=/i/checkout.gif>";
							cssClass = " class=\"LinethroughGray\"";
							break;
					}

					string title = dr["Description"].ToString().Replace("\"", "'");
					if (title.Length > 100)
					{
						Regex rx = new Regex(@"(?s)\b.{1,98}\b");
						title = rx.Match(title).Value + "&hellip;";
					}

					if (this.fromFrame)
						c.Text += String.Format("<tr><td{0}>{1}<span style=\"cursor:pointer\" onclick=\"parent.location.href='/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac={2}&goback={3}'\">{4}{5}</span>[{6}]&nbsp;<span style=\"cursor:pointer\" onclick=\"parent.location.href='/WorkingCRM/AllActivity.aspx?Ac={2}&goback={3}'\" title=\"{9}\">{7}</span></td><td align=right width=1% nowrap>[{8}]</td></tr>", cssClass, indent, dr["id"].ToString(), this.fromSheet, ImgType((int) dr["Type"]), todoImg, UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToString("g"), dr["Subject"], dr["OwnerName"], title);
					else
						c.Text += String.Format("<tr><td{0}>{1}<a href=/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac={2}&goback={3}>{4}{5}</a>&nbsp;[{6}]&nbsp;<span title=\"{9}\"><a href=/WorkingCRM/AllActivity.aspx?Ac={2}&goback={3}>{7}</a></span></td><td align=right width=1% nowrap>[{8}]</td></tr>", cssClass, indent, dr["id"].ToString(), this.fromSheet, ImgType((int) dr["Type"]), todoImg, UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToString("g"), dr["Subject"], dr["OwnerName"], title);

					FillCronologia(c, level, dr["id"].ToString());
				}
			}
		}

		private string ImgType(int aType)
		{
			string img = String.Empty;
			switch (aType)
			{
				case 1:
					img = "<img src=/i/a/Phone.gif border=0>";
					break;
				case 2:
					img = "<img src=/i/a/letter.gif border=0>";
					break;
				case 3:
					img = "<img src=/i/a/fax.gif border=0>";
					break;
				case 4:
					img = "<img src=/i/a/Pin.gif border=0>";
					break;
				case 5:
					img = "<img src=/i/a/Email.gif border=0>";
					break;
				case 6:
					img = "<img src=/i/a/Hands.gif border=0>";
					break;
				case 7:
					img = "<img src=/i/a/generic.gif border=0>";
					break;
				case 8:
					img = "<img src=/i/a/case.gif border=0>";
					break;
				case 9:
					img = "<img src=/i/a/quote.gif border=0>";
					break;
			}
			return img;
		}

        public void NoPaging()
        {
            RepCroPaging.DontUse=true;
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
		}

		#endregion
	}
}
