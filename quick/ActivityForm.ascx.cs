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
using System.Globalization;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.WorkingCRM
{
	public delegate void OnSaveActivity(bool saved);

	public partial class ActivityForm : UserControl
	{
		public event OnSaveActivity OnSaveActivity;



		public DateTimeFormatInfo DBCult = CultureInfo.InvariantCulture.DateTimeFormat; //new CultureInfo("en-US").DateTimeFormat;

		private UserConfig UC = new UserConfig();
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];


		public ActivityForm()
		{
			if (HttpContext.Current.Session.IsNewSession)
				HttpContext.Current.Response.Redirect("/login.aspx");
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
		}

		public string GetContactLeadID
		{
			get
			{
				string o = string.Empty;
				if (TextboxContactID.Text.Length > 0)
					o = "C" + TextboxContactID.Text;
				else if (TextboxLeadID.Text.Length > 0)
					o = "L" + TextboxLeadID.Text;

				return o;
			}
		}

		public string Activity
		{
			get
			{
				object o = this.ViewState["_ActivityID" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set { this.ViewState["_ActivityID" + this.ID] = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			MoveLog.Visible = false;
			if (!Page.IsPostBack)
			{
				CheckToBill.Text =Root.rm.GetString("Acttxt52");
				CheckCommercial.Text =Root.rm.GetString("Acttxt53");
				CheckTechnical.Text =Root.rm.GetString("Acttxt54");

				Activity_InOut.Items.Add(new ListItem(String.Format("<img src=/i/incoming.gif alt=\"{0}\">",Root.rm.GetString("AcTooltip1")), "1"));
				Activity_InOut.Items.Add(new ListItem(String.Format("<img src=/i/outcoming.gif alt=\"{0}\">",Root.rm.GetString("AcTooltip2")), "0"));
				Activity_InOut.Items[1].Selected = true;

				ChildAction.Text = "<img src=\"/i/newevent.gif\" alt=\"" +Root.rm.GetString("AcTooltip20") + "\" border=0 style=\"cursor:pointer\">";

				Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt71"), "1"));
				Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt72"), "0"));
				Activity_ToDo.Items.Add(new ListItem(Root.rm.GetString("Acttxt103"), "2"));
				Activity_ToDo.RepeatDirection = RepeatDirection.Horizontal;
				Activity_ToDo.RepeatColumns = 3;


				IMGAvailability.Alt =Root.rm.GetString("Evnttxt42");

				SubmitBtn.Text =Root.rm.GetString("Acttxt87");
				SubmitBtnDoc.Text =Root.rm.GetString("Acttxt88");
				if (this.Activity.Length > 0)
					this.FillForm();
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
			this.SubmitBtn.Click += new EventHandler(this.SubmitBtn_Click);
			this.SubmitBtnDoc.Click += new EventHandler(this.SubmitBtn_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		public void FillForm()
		{
			ActivityID.Text = Activity;
			DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKING_VIEW WHERE ID=" + Activity);
			DataRow dr = ds.Tables[0].Rows[0];

			ViewAcType((int) dr["type"]);

			if ((int) dr["type"] == 6 && dr["VisitID"] != DBNull.Value)
			{
				try
				{
					DataRow drHours = DatabaseConnection.CreateDataset("SELECT STARTDATE,ENDDATE,SECONDUID,UID FROM BASE_CALENDAR WHERE ID=" + dr["VisitID"].ToString()).Tables[0].Rows[0];
					F_StartHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drHours["startdate"])).ToShortTimeString();
					F_EndHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drHours["enddate"])).ToShortTimeString();
					if (drHours["SecondUID"] != drHours["UID"])
					{
						IdCompanion.Text = drHours["SecondUID"].ToString();
						Companion.Text = DatabaseConnection.SqlScalar("SELECT NAME+' '+SURNAME FROM ACCOUNT WHERE UID=" + drHours["SecondUID"].ToString());
					}
				}
				catch
				{
				}
			}


			TextboxObject.Text = dr["Subject"].ToString();

			Activity_InOut.SelectedIndex = -1;
			if ((bool) dr["InOut"])
				Activity_InOut.SelectedIndex = 0;
			else
				Activity_InOut.SelectedIndex = 1;


			switch (Convert.ToInt32(dr["ToDo"]))
			{
				case 0:
					Activity_ToDo.SelectedIndex = 1;
					break;
				case 1:
					Activity_ToDo.SelectedIndex = 0;
					break;
				case 2:
					Activity_ToDo.SelectedIndex = 2;
					break;
			}


			if ((int) dr["type"] == 6 && Convert.ToInt32(dr["ToDo"]) != 0)
				Appointmenthours.Attributes.Add("style", "display:none");

			TextBoxData.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToShortDateString();
			TextBoxHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(dr["ActivityDate"], UC.myDTFI)).ToShortTimeString();


			DropDownListStatus.SelectedIndex = -1;
			foreach (ListItem li in DropDownListStatus.Items)
			{
				if (li.Value == dr["State"].ToString())
				{
					li.Selected = true;
					break;
				}
			}


			DropDownListPriority.SelectedIndex = -1;
			foreach (ListItem li in DropDownListPriority.Items)
			{
				if (li.Value == dr["Priority"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			TextboxCompanyID.Text = dr["CompanyID"].ToString();
			TextboxCompany.Text = dr["CompanyName"].ToString();
			TextboxLeadID.Text = dr["LeadID"].ToString();
			TextboxLead.Text = (dr["LeadName"].ToString().Length > 3) ? dr["LeadName"].ToString() : "";
			TextboxOwnerID.Text = dr["OwnerID"].ToString();
			TextboxOwner.Text = dr["OwnerName"].ToString();
			CheckToBill.Checked = (bool) dr["ToBill"];
			CheckCommercial.Checked = (bool) dr["Commercial"];
			CheckTechnical.Checked = (bool) dr["Technical"];


			DropDownListPreAlarm.SelectedIndex = -1;
			foreach (ListItem li in DropDownListPreAlarm.Items)
			{
				if (li.Value == dr["DaysAllarm"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			TextboxContactID.Text = dr["ReferrerID"].ToString();
			TextboxContact.Text = dr["ContactName"].ToString();

			TextboxOpportunityID.Text = dr["OpportunityID"].ToString();
			TextboxOpportunity.Text = dr["OpportunityDescription"].ToString();


			DropDownListClassification.SelectedIndex = -1;
			foreach (ListItem li in DropDownListClassification.Items)
			{
				if (li.Value == dr["Classification"].ToString())
				{
					li.Selected = true;
					break;
				}
			}

			TextboxParentID.Text = dr["ParentID"].ToString();
			TextboxParent.Text = dr["ParentSubject"].ToString();
			TextboxDescription.Text = dr["Description"].ToString();
			TextboxDescription2.Text = dr["Description2"].ToString();

			if ((int) dr["Duration"] > 0)
			{
				if ((int) dr["Duration"] < 60)
				{
					TextBoxDurationM.Text = dr["Duration"].ToString();
				}
				else
				{
					int dur = (int) dr["Duration"];
					TextBoxDurationH.Text = Convert.ToInt32(dur/60).ToString();
					TextBoxDurationM.Text = Convert.ToInt32(dur%60).ToString();
				}
			}



			if (dr["DocID"] != DBNull.Value)
			{
				DataTable dtDoc = DatabaseConnection.CreateDataset("SELECT FILECROSSTABLES.IDFILE,FILEMANAGER.FILENAME FROM FILECROSSTABLES LEFT OUTER JOIN FILEMANAGER ON FILECROSSTABLES.IDFILE=FILEMANAGER.ID WHERE FILEMANAGER.ID=" + dr["DocID"].ToString()).Tables[0];

				if (dtDoc.Rows.Count > 0)
				{
					((TextBox) Page.FindControl("IDDocument")).Text = dtDoc.Rows[0]["idfile"].ToString();
					DocumentDescription.Text = dtDoc.Rows[0]["filename"].ToString();
				}
				else
				{
					try
					{
						using (DigiDapter dg = new DigiDapter())
						{
							dg.Add("IDFILE", dr["DocID"]);
							dg.Add("TABLENAME", "CRM_WorkActivity");
							dg.Add("IDRIF", Convert.ToInt64(Activity));

							dg.Execute("FILECROSSTABLES");
						}
						DataTable tDocNew = DatabaseConnection.CreateDataset("SELECT FILECROSSTABLES.IDFILE,FILEMANAGER.FILENAME FROM FILECROSSTABLES LEFT OUTER JOIN FILEMANAGER ON FILECROSSTABLES.IDFILE=FILEMANAGER.ID WHERE FILEMANAGER.ID=" + dr["DocID"].ToString()).Tables[0];
						IDDocument.Text = tDocNew.Rows[0]["idfile"].ToString();
						DocumentDescription.Text = tDocNew.Rows[0]["filename"].ToString();
					}
					catch
					{
					}
				}
			}

			if (IDDocument.Text.Length > 0)
				LinkDocument.Visible = true;
			else
				LinkDocument.Visible = false;


			GetLog(Activity);


		}

		private void GetLog(string id)
		{
			DataTable dtLog = DatabaseConnection.CreateDataset("SELECT MOVEDATE,ACTIONTYPE,PREVVALUE,NEXTVALUE, ACCOUNT.NAME+' '+ACCOUNT.SURNAME AS OWNERNAME FROM ACTIVITYMOVELOG LEFT OUTER JOIN ACCOUNT ON ACTIVITYMOVELOG.OWNERID = ACCOUNT.UID WHERE ACTIVITYMOVELOG.ACID=" + id).Tables[0];
			if (dtLog.Rows.Count > 0)
			{
				Table LogTable = new Table();
				int i = 0;
				LogTable.ID = "LogTable";
				LogTable.Width = Unit.Percentage(100);
				LogTable.Attributes.Add("style", "display:none");
				TableRow trTitle = new TableRow();
				TableCell tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt11"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt110"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt111"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt112"));
				trTitle.Cells.Add(tdtitle);

				tdtitle = new TableCell();
				tdtitle.CssClass = "GridTitle";
				tdtitle.Text = StaticFunctions.Capitalize(Root.rm.GetString("Acttxt113"));
				trTitle.Cells.Add(tdtitle);

				LogTable.Rows.Add(trTitle);
				foreach (DataRow drlog in dtLog.Rows)
				{
					i++;
					TableRow tr = new TableRow();
					for (int cell = 0; cell < 5; cell++)
					{
						TableCell td = new TableCell();
						td.CssClass = ((i%2) == 0) ? "GridItem" : "GridItemAltern";
						switch (cell)
						{
							case 0:
								td.Text = ((DateTime) drlog[0]).ToShortDateString();
								break;
							case 1:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text =Root.rm.GetString("Acttxt104");
										break;
									case ActivityMoveLog.MoveDate:
										td.Text =Root.rm.GetString("Acttxt105");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text =Root.rm.GetString("Acttxt106");
										break;
									case ActivityMoveLog.MoveContact:
										td.Text =Root.rm.GetString("Acttxt107");
										break;
									case ActivityMoveLog.MoveLead:
										td.Text =Root.rm.GetString("Acttxt108");
										break;
									case ActivityMoveLog.MoveDone:
										td.Text =Root.rm.GetString("Acttxt109");
										break;
									case ActivityMoveLog.MailSent:
										td.Text =Root.rm.GetString("Acttxt119");
										break;
								}
								break;
							case 2:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS ACCOUNTNAME FROM ACCOUNT WHERE UID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveDate:
										DateTime dt;
										if (drlog[2].ToString().Length > 8)
											dt = DateTime.ParseExact((string) drlog[2], @"yyyyMMdd H.mm", null);
										else
											dt = DateTime.ParseExact((string) drlog[2], @"yyyyMMdd", null);
										td.Text = dt.ToString("g");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveContact:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS CONTACTNAME FROM BASE_CONTACTS WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveLead:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'')) AS LEADNAME FROM CRM_LEADS WHERE ID=" + drlog[2].ToString());
										break;
									case ActivityMoveLog.MoveDone:
										switch (drlog[2].ToString())
										{
											case "0":
												td.Text =Root.rm.GetString("Acttxt72");
												break;
											case "1":
												td.Text =Root.rm.GetString("Acttxt71");
												break;
											case "2":
												td.Text =Root.rm.GetString("Acttxt103");
												break;
										}
										break;
									case ActivityMoveLog.MailSent:
										td.Text = drlog[2].ToString();
										break;
								}

								break;
							case 3:
								switch ((ActivityMoveLog) drlog[1])
								{
									case ActivityMoveLog.MoveOwner:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS ACCOUNTNAME FROM ACCOUNT WHERE UID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveDate:

										DateTime dt;
										if (drlog[3].ToString().Length > 8)
											dt = DateTime.ParseExact((string) drlog[3], @"yyyyMMdd H.mm", null);
										else
											dt = DateTime.ParseExact((string) drlog[3], @"yyyyMMdd", null);

										td.Text = dt.ToString("g");
										break;
									case ActivityMoveLog.MoveCompany:
										td.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveContact:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')) AS CONTACTNAME FROM BASE_CONTACTS WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveLead:
										td.Text = DatabaseConnection.SqlScalar("SELECT (ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'')) AS LEADNAME FROM CRM_LEADS WHERE ID=" + drlog[3].ToString());
										break;
									case ActivityMoveLog.MoveDone:
										switch (drlog[3].ToString())
										{
											case "0":
												td.Text =Root.rm.GetString("Acttxt72");
												break;
											case "1":
												td.Text =Root.rm.GetString("Acttxt71");
												break;
											case "2":
												td.Text =Root.rm.GetString("Acttxt103");
												break;
										}
										break;
									case ActivityMoveLog.MailSent:
										td.Text = drlog[3].ToString();
										break;
								}
								break;
							case 4:
								td.Text = drlog[4].ToString();
								break;
						}
						tr.Cells.Add(td);
					}
					LogTable.Rows.Add(tr);
				}
				MoveLogTable.Controls.Add(LogTable);
				MoveLogTable.EnableViewState = false;
				MoveLog.Visible = true;
			}

		}

		private void ViewAcType(int type)
		{
			LabelTypeActivity.Text = type.ToString();
			switch (type)
			{
				case (int) ActivityTypeN.PhoneCall:
					FillDropDown("1");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Letter:
					FillDropDown("2");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Fax:
					FillDropDown("3");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Memo:
					FillDropDown("4");
					PanelOwner.Visible = true;
					CheckToBill.Visible = false;
					CheckCommercial.Visible = false;
					CheckTechnical.Visible = false;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Email:
					FillDropDown("5");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;

					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Visit:
					FillDropDown("6");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability_holder.Visible = true;
					IMGAvailability.Visible = true;
					Appointmenthours.Visible = true;
					HourPanel.Visible = false;
					Activity_ToDo.Attributes.Add("onclick", "ActivateHours()");
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Generic:
					FillDropDown("7");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = true;
					CheckTechnical.Visible = true;
					SecondDescription.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt74");
					LabelDescription.Text =Root.rm.GetString("Acttxt86");

					IMGAvailability.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.CaseSolution:
					FillDropDown("8");
					PanelOwner.Visible = true;
					CheckToBill.Visible = true;
					CheckCommercial.Visible = false;
					CheckTechnical.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt75");
					LabelDescription2.Text =Root.rm.GetString("Acttxt76");
					SecondDescription.Visible = true;

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = true;
					PanelQuoteExpire.Visible = false;
					tblQuoteStage.Visible = false;
					break;
				case (int) ActivityTypeN.Quote:
					FillDropDown("8");
					PanelOwner.Visible = true;
					CheckToBill.Visible = false;
					CheckCommercial.Visible = false;
					CheckTechnical.Visible = false;
					LabelData.Text =Root.rm.GetString("Acttxt38");
					LabelDescription.Text =Root.rm.GetString("Acttxt75");
					LabelDescription2.Text =Root.rm.GetString("Acttxt76");
					SecondDescription.Visible = false;

					IMGAvailability_holder.Visible = false;
					Appointmenthours.Visible = false;
					HourPanel.Visible = false;

					PanelQuoteExpire.Visible = true;
					tblQuoteStage.Visible = true;

					break;
			}

		}

		protected void FillDropDown(string a)
		{
			DropDownList dp = DropDownListStatus;
			dp.Items.Clear();
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt0"), "0"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt1"), "1"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt2"), "2"));
			dp.Items[0].Selected = true;
			dp = DropDownListPriority;
			dp.Items.Clear();
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt3"), "0"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt4"), "1"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt5"), "2"));
			dp.Items[0].Selected = true;
			dp = DropDownListClassification;
			dp.Items.Clear();
			DataSet wc = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRMWORKINGCLASSIFICATION WHERE TYPE=" + a + " AND LANG='" + UC.Culture.Substring(0, 2) + "' ORDER BY DROPPOSITION");
			if (wc.Tables[0].Rows.Count > 0)
			{
				dp.DataTextField = "Description";
				dp.DataValueField = "ID";
				dp.DataSource = wc;
				dp.DataBind();
				dp.Items[0].Selected = true;
			}

			dp = DropDownListPreAlarm;
			dp.Items.Clear();
			for (int i = 0; i < 6; i++)
			{
				dp.Items.Add(new ListItem(i.ToString() + " " +Root.rm.GetString("Acttxt124"), i.ToString()));
			}
			dp.Items[0].Selected = true;


		}

		private bool SaveActivity(string sender)
		{
			string newId = ActivityID.Text;
			string modified1 = ModifyActivity(int.Parse(LabelTypeActivity.Text), int.Parse(newId));

			if (newId != ActivityID.Text)
				newId = ActivityID.Text;

			if (IDDocument.Text.Length > 0)
				LinkDocument.Visible = true;
			else
				LinkDocument.Visible = false;

			bool modified = true;
			if (modified1 == "0")
				ActivityInfo.Text =Root.rm.GetString("Acttxt92");
			else
			{
				modified = false;
				if (modified1 == "1")
					ActivityInfo.Text =Root.rm.GetString("Acttxt93");
				else
					ActivityInfo.Text = modified1;
			}

			if (modified)
			{
				if (Activity_ToDo.SelectedIndex == 0)
				{
					DateTime activityDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text, UC.myDTFI));
					if (TextboxCompanyID.Text.Length > 0)
						InsertLastContact(TextboxCompanyID.Text, 0, activityDate);
					if (TextboxContactID.Text.Length > 0)
						InsertLastContact(TextboxContactID.Text, 1, activityDate);
					if (TextboxLeadID.Text.Length > 0)
						InsertLastContact(TextboxLeadID.Text, 2, activityDate);
				}
				if (LabelTypeActivity.Text == "6")
				{
					if (Activity_ToDo.Items[1].Selected)
					{
						DataRow dract = DatabaseConnection.CreateDataset("SELECT * FROM CRM_WORKACTIVITY WHERE ID=" + newId).Tables[0].Rows[0];
						string appId = AppointmentUpdate((dract["VisitID"] == DBNull.Value) ? "-1" : dract["VisitID"].ToString(),
						                                 dract["OwnerID"].ToString(),
						                                 TextBoxData.Text,
						                                 F_StartHour.Text,
						                                 F_EndHour.Text,
						                                 TextboxContact.Text,
						                                 TextboxContactID.Text,
						                                 TextboxCompany.Text,
						                                 TextboxCompanyID.Text,
						                                 TextboxObject.Text,
						                                 (dract["VisitID"] == DBNull.Value) ? "-1" : dract["VisitID"].ToString());
						DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET VISITID='" + appId + "' WHERE ID='" + newId + "'");
						if (IdCompanion.Text.Length > 0)
						{
							string newAppId = DatabaseConnection.SqlScalar("SELECT SENCONDIDOWNER FROM BASE_CALENDAR WHERE ID=" + appId);
							if (newAppId.Length <= 0) newAppId = "-1";
							string crossId = AppointmentUpdate(newAppId,
							                                   IdCompanion.Text,
							                                   TextBoxData.Text,
							                                   F_StartHour.Text,
							                                   F_EndHour.Text,
							                                   TextboxContact.Text,
							                                   TextboxContactID.Text,
							                                   TextboxCompany.Text,
							                                   TextboxCompanyID.Text,
							                                   TextboxObject.Text,
							                                   appId);
							DatabaseConnection.DoCommand("UPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossId + " WHERE ID=" + appId);
						}
					}
				}



			}


			if (TextboxOpportunityID.Text.Length > 0)
			{
				if (TextboxCompanyID.Text.Length > 0)
				{
					int checkCross = Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=0 AND CONTACTID = {0} AND OPPORTUNITYID = {1};", TextboxCompanyID.Text, TextboxOpportunityID.Text)));
					if (checkCross <= 0)
					{
						InsertNewOpportunity(TextboxOpportunityID.Text, TextboxCompanyID.Text, "0");
					}
				}
				if (TextboxLeadID.Text.Length > 0)
				{
					int checkCross = Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("SELECT COUNT(*) FROM CRM_OPPORTUNITYCONTACT WHERE CONTACTTYPE=1 AND CONTACTID = {0} AND OPPORTUNITYID = {1};", TextboxLeadID.Text, TextboxOpportunityID.Text)));
					if (checkCross <= 0)
					{
						InsertNewOpportunity(TextboxOpportunityID.Text, TextboxLeadID.Text, "1");
					}
				}
			}

			if (SendMail.Checked && destinationEmail.Value.Length > 0)
			{
				string notifyEmail = DatabaseConnection.SqlScalar("SELECT NOTIFYEMAIL FROM ACCOUNT WHERE UID=" + UC.UserId);
				MessagesHandler.SendMail(destinationEmail.Value,
				                         (notifyEmail.Length > 0) ? notifyEmail : UC.UserName,
				                         TextboxObject.Text,
				                         TextboxDescription.Text);
			}

			GetLog(newId);

			return modified;
		}

		protected virtual string ModifyActivity(int a, int id)
		{
			{
				bool PageValid = true;
				string errorCode = String.Empty;
				if (Activity_ToDo.Items[1].Selected && a == 6)
				{
					if (!StartHourValidator.IsValid ||
						!EndHourValidator.IsValid)
					{
						PageValid = false;
						errorCode = "1";
					}
				}
				if (!DataValidator.IsValid)
				{
					PageValid = false;
					errorCode += "2";
				}

				if (TextboxObject.Text.Length <= 0)
				{
					PageValid = false;
					errorCode += "3";
				}

				if (PageValid &&
					(TextboxCompanyID.Text.Length > 0 ||
						TextboxContactID.Text.Length > 0 ||
						TextboxLeadID.Text.Length > 0))
				{
					DigiDapter dg = new DigiDapter();

					dg.Add("TYPE", a, 'I');
					dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
					dg.Add("CREATEDBYID", UC.UserId, 'I');


					dg.Add("OWNERID", TextboxOwnerID.Text);

					if (TextboxCompanyID.Text.Length > 0)
						dg.Add("COMPANYID", TextboxCompanyID.Text);
					else
						dg.Add("COMPANYID", DBNull.Value);

					if (TextboxContactID.Text.Length > 0)
						dg.Add("REFERRERID", TextboxContactID.Text);
					else
						dg.Add("REFERRERID", DBNull.Value);

					if (TextboxLeadID.Text.Length > 0)
						dg.Add("LEADID", TextboxLeadID.Text);
					else
						dg.Add("LEADID", DBNull.Value);

					if (TextboxOpportunityID.Text.Length > 0)
						if (DatabaseConnection.SqlScalar("SELECT COUNT(ID) FROM CRM_OPPORTUNITY WHERE ID=" + TextboxOpportunityID.Text) != "0")
							dg.Add("OPPORTUNITYID", TextboxOpportunityID.Text);
						else
							dg.Add("OPPORTUNITYID", DBNull.Value);
					else
						dg.Add("OPPORTUNITYID", DBNull.Value);

					if (IDDocument.Text.Length > 0)
						dg.Add("DOCID", IDDocument.Text);
					else
						dg.Add("DOCID", DBNull.Value);

					dg.Add("SUBJECT", TextboxObject.Text);
					dg.Add("DESCRIPTION", TextboxDescription.Text);
					dg.Add("DESCRIPTION2", TextboxDescription2.Text);

					dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");
					dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
					dg.Add("LASTMODIFIEDBYID", UC.UserId.ToString());
					dg.Add("INOUT", (Activity_InOut.SelectedValue == "1") ? true : false);
					dg.Add("TODO", Activity_ToDo.SelectedValue);

					if (F_StartHour.Text.Length > 0)
						TextBoxHour.Text = F_StartHour.Text;


					dg.Add("ACTIVITYDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)));
					if (DropDownListClassification.SelectedIndex != -1)
						dg.Add("CLASSIFICATION", DropDownListClassification.SelectedValue);
					if (TextboxParentID.Text.Length > 0)
						dg.Add("PARENTID", TextboxParentID.Text);
					else
						dg.Add("PARENTID", 0);
					dg.Add("STATE", DropDownListStatus.SelectedValue);
					dg.Add("PRIORITY", DropDownListPriority.SelectedValue);

					dg.Add("TOBILL", CheckToBill.Checked);
					dg.Add("COMMERCIAL", CheckCommercial.Checked);
					dg.Add("TECHNICAL", CheckTechnical.Checked);

					if (DropDownListPreAlarm.SelectedIndex > 0)
					{
						dg.Add("ALLARM", UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text, UC.myDTFI)));
						dg.Add("DAYSALLARM", DropDownListPreAlarm.SelectedValue);
					}

					int duration = 0;
					if (TextBoxDurationH.Text.Length > 0)
					{
						try
						{
							duration += Convert.ToInt32(TextBoxDurationH.Text)*60;
						}
						catch
						{
							duration += 0;
						}
					}

					if (TextBoxDurationM.Text.Length > 0)
					{
						try
						{
							duration += Convert.ToInt32(TextBoxDurationM.Text);
						}
						catch
						{
							duration += 0;
						}
					}
					dg.Add("DURATION", duration);

					dg.Execute("CRM_WORKACTIVITY", "ID=" + id, DigiDapter.Identities.Row);

					DataRow myDataRow = dg.GetNewRow;
					DataRow oldData = dg.GetOriginalRow;

					if (SendMail.Checked && destinationEmail.Value.Length > 0)
					{
						if (dg.RecordInserted)
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   ('" + myDataRow["ID"].ToString() + "', " + (byte) ActivityMoveLog.MailSent + ", '" + UC.UserName + "', '" + destinationEmail.Value + "', GETDATE(), '" + UC.UserId + "')");
						else
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MailSent + ", '" + UC.UserName + "', '" + destinationEmail.Value + "', GETDATE(), '" + UC.UserId + "')");
					}


					ActivityID.Text = myDataRow["ID"].ToString();
					string newId = myDataRow["ID"].ToString();
					if (Activity_ToDo.SelectedValue == "0" || DropDownListPreAlarm.SelectedValue.Length > 0)
						ModifyReminderActivity(newId, TextBoxData.Text);

					if (dg.RecordInserted)
					{
						string oId = TextboxOwnerID.Text;
						if (TextboxCompanyID.Text.Length > 0)
						{
							string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=0 AND ACCOUNT=" + oId + " AND CROSSID=" + TextboxCompanyID.Text);
							if (acId.Length > 0)
								DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
							else
								DatabaseConnection.DoCommand("INSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + TextboxCompanyID.Text + ",0)");
						}
						if (TextboxContactID.Text.Length > 0)
						{
							string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=1 AND ACCOUNT=" + oId + " AND CROSSID=" + TextboxContactID.Text);
							if (acId.Length > 0)
								DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
							else
								DatabaseConnection.DoCommand("iNSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + TextboxContactID.Text + ",1)");
						}
						if (TextboxLeadID.Text.Length > 0)
						{
							string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=2 AND ACCOUNT=" + oId + " AND CROSSID=" + TextboxLeadID.Text);
							if (acId.Length > 0)
								DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
							else
								DatabaseConnection.DoCommand("INSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + TextboxLeadID.Text + ",2)");
						}
					}

					if (Activity_ToDo.Items[0].Selected && a == 6)
						Appointmenthours.Attributes.Add("style", "display:none");


					if (dg.recordUpdated)
					{
						if (oldData["OwnerID"].ToString() != myDataRow["OwnerID"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveOwner + ", '" + oldData["OwnerID"].ToString() + "', '" + myDataRow["OwnerID"].ToString() + "', GETDATE(), '" + UC.UserId + "')");

						if (oldData["ActivityDate"].ToString() != myDataRow["ActivityDate"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveDate + ", '" + (UC.LTZ.ToLocalTime((DateTime) oldData["ActivityDate"])).ToString(@"yyyyMMdd H.mm") + "', '" + (UC.LTZ.ToLocalTime((DateTime) myDataRow["ActivityDate"])).ToString(@"yyyyMMdd H.mm") + "', GETDATE(), '" + UC.UserId + "')");

						if (oldData["CompanyID"].ToString() != myDataRow["CompanyID"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveCompany + ", '" + oldData["CompanyID"].ToString() + "', '" + myDataRow["CompanyID"].ToString() + "', GETDATE(), '" + UC.UserId + "')");

						if (oldData["ReferrerID"].ToString() != myDataRow["ReferrerID"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveContact + ", '" + oldData["ReferrerID"].ToString() + "', '" + myDataRow["ReferrerID"].ToString() + "', GETDATE(), '" + UC.UserId + "')");

						if (oldData["LeadID"].ToString() != myDataRow["LeadID"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveLead + ", '" + oldData["LeadID"].ToString() + "', '" + myDataRow["LeadID"].ToString() + "', GETDATE(), '" + UC.UserId + "')");

						if (oldData["ToDo"].ToString() != myDataRow["ToDo"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveDone + ", '" + oldData["ToDo"].ToString() + "', '" + myDataRow["ToDo"].ToString() + "', GETDATE(), '" + UC.UserId + "')");
					}

					return "0";
				}
				else
				{
					string error = String.Empty;
					if (errorCode.IndexOf("1") > 0 || errorCode.IndexOf("2") > 0)
						error +=Root.rm.GetString("Acttxt101") + "<br>";

					if (!(TextboxObject.Text.Length > 0))
						error +=Root.rm.GetString("Acttxt100") + "<br>";

					if (!(TextboxCompanyID.Text.Length > 0) &&
						!(TextboxContactID.Text.Length > 0))
						error +=Root.rm.GetString("Acttxt102") + "<br>";

					if (Activity_ToDo.Items[0].Selected && a == 6)
						Appointmenthours.Attributes.Add("style", "display:none");

					return error;
				}
			}
		}

		private void ModifyReminderActivity(string id, string data)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OWNERID", TextboxOwnerID.Text, 'I');
				dg.Add("K_ID", id);
				dg.Add("TABLENAME", "CRM_WorkActivity", 'I');

				dg.Add("REMINDERDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(data, UC.myDTFI)));
				dg.Add("NOTE", TextboxObject.Text);
				dg.Add("ADVANCEREMIND", (DropDownListPreAlarm.SelectedValue.Length > 0) ? DropDownListPreAlarm.SelectedValue : "0");
				dg.Execute("CRM_REMINDER", "K_ID =" + id + " AND TABLENAME='CRM_WORKACTIVITY' AND OWNERID=" + int.Parse(TextboxOwnerID.Text));
			}
		}

		public void InsertNewOpportunity(string opid, string id, string type)
		{
			StringBuilder sqlString = new StringBuilder();

			sqlString.AppendFormat("SELECT * FROM CRM_OPPORTUNITYCONTACT WHERE ID=-1");
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
				dg.Add("ESTIMATEDCLOSEDATE", DateTime.Now.AddDays(Convert.ToDouble(DatabaseConnection.SqlScalar("SELECT TOP 1 ESTIMATEDDATEDAYS FROM TUSTENA_DATA"))));

				dg.Add("ENDDATE", DBNull.Value);

				dg.Add("SALESPERSON", TextboxOwnerID.Text);

				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("CREATEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
				dg.Add("LASTMODIFIEDBYID", UC.UserId);
				dg.Add("LASTMODIFIEDDATE", UC.LTZ.ToUniversalTime(DateTime.Now));
				dg.Execute("CRM_OPPORTUNITYCONTACT");
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

		internal string AppointmentUpdate(string appId,
		                                  string uID,
		                                  string dt,
		                                  string fromhour,
		                                  string tohour,
		                                  string contactname,
		                                  string contactID,
		                                  string companyname,
		                                  string companyID,
		                                  string note,
		                                  string newId)
		{
			object InsertedID;
			DigiDapter dg = new DigiDapter();
			dg.Add("UID", uID);
			dg.Add("UIDINS", UC.UserId);
			dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(dt + " " + fromhour, UC.myDTFI)));
			dg.Add("ENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(dt + " " + tohour, UC.myDTFI)));
			dg.Add("CONTACT", contactname);
			dg.Add("COMPANY", companyname);
			if (companyID.Length > 0) dg.Add("COMPANYID", companyID);
			if (contactID.Length > 0) dg.Add("CONTACTID", contactID);


			dg.Add("NOTE", note);
			if (appId == newId)
				dg.Where = "ID=" + appId;
			else
				dg.Where = "UID=" + uID + " AND SENCONDIDOWNER=" + newId;

			if (IdCompanion.Text.Length > 0)
			{
				if (uID == IdCompanion.Text)
					dg.Add("SECONDUID", uID);
				else
					dg.Add("SECONDUID", IdCompanion.Text);
			}
			if (appId != newId)
				dg.Add("SENCONDIDOWNER", newId);

			InsertedID = dg.Execute("BASE_CALENDAR", DigiDapter.Identities.Identity);

			if (appId == "-1") appId = InsertedID.ToString();

			return appId;


		}

		private void InsertLastContact(string iD, byte type, DateTime data)
		{
			string tb = String.Empty;
			switch (type)
			{
				case 0:
					tb = "BASE_COMPANIES";
					break;
				case 1:
					tb = "BASE_CONTACTS";
					break;
				case 2:
					tb = "CRM_LEADS";
					break;
			}

			try
			{
				DateTime oldDate = Convert.ToDateTime(DatabaseConnection.SqlScalar("SELECT LASTCONTACT FROM " + tb + " WHERE ID=" + iD));
				if (oldDate < data)
					DatabaseConnection.DoCommand("UPDATE " + tb + " SET LASTCONTACT='" + data.ToString(@"yyyyMMdd") + "' WHERE ID=" + iD);
			}
			catch
			{
				DatabaseConnection.DoCommand("UPDATE " + tb + " SET LASTCONTACT=GETDATE() WHERE ID=" + iD);
			}
		}

		private void SubmitBtn_Click(object sender, EventArgs e)
		{
			bool saved = SaveActivity(((LinkButton) sender).ID);
			if (saved)
				this.OnSaveActivity(true);

		}
	}
}
