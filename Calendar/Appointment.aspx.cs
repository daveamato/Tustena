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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public partial class Appointment : G
	{






			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
				this.Load += new EventHandler(this.Page_Load);
			this.Submit.Click += new EventHandler(this.Submit_Click);
			this.ViewAppointmentForm.ItemCommand += new RepeaterCommandEventHandler(this.ItemCommandView);
			this.ViewAppointmentForm.ItemDataBound += new RepeaterItemEventHandler(this.ItemDataBoundView);

			}
		#endregion

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				Submit.Attributes.Add("onclick", "BeforeSubmit();");
				CheckReminder.Attributes.Add("onclick", "HideReminder(this);");
				DateEr1.Text = "(" + UC.myDTFI.ShortDatePattern.ToString() + ")";
				DateEr2.Text = "(" + UC.myDTFI.ShortDatePattern.ToString() + ")";
                GetOwnerName();
				if (!Page.IsPostBack)
				{
					Submit.Text =Root.rm.GetString("Evnttxt36");

					DropDownList dp = (DropDownList) Page.FindControl("DropDownListPreAlarm");
					dp.Items.Clear();
					for (int i = 0; i < 6; i++)
					{
						dp.Items.Add(new ListItem(i.ToString() + " " +Root.rm.GetString("Acttxt124"), i.ToString()));
					}
					dp.Items[0].Selected = true;

					if (Session["fromactivity"] != null)
					{
						FillFromActivity();
						HiddenStartHour.Value = UC.WorkStartHour;
						HiddenEndHour.Value = UC.WorkEndHour;
					}
					else
					{
						if (UC.ImpersonateUser != 0)
						{
							LbFlagNotify.Text =Root.rm.GetString("Evnttxt56");
							LbFlagNotify.Visible = true;

							bool flagNotify = (bool) DatabaseConnection.SqlScalartoObj("SELECT FLAGNOTIFYAPPOINTMENT FROM ACCOUNT WHERE UID=" + UC.ImpersonateUser);

							if (flagNotify)
							{
								CheckNotify.Checked = true;
								CheckNotify.Enabled = false;
							}
							else
							{
								CheckNotify.Checked = false;
							}
							CheckNotify.Visible = true;
						}


						FillDropDownRec();
						if (Request.Params["mode"] != null)
						{
							FillRecType();
							if (Request.Params["mode"] == "NEW")
							{
								SomeJS.Text = "<script>ShowRoom();</script>";
								if (Request.Params["data"] != null)
								{
									try
									{
										F_StartDate.Text = Convert.ToDateTime(Request.Params["data"], UC.myDTFI).ToString(@"dd/MM/yyyy");
									}
									catch
									{
										F_StartDate.Text = String.Empty;
									}
								}
								if (Request.Params["ora1"] != null)
								{
									try
									{
										F_StartHour.Text = Request.Params["ora1"];
									}
									catch
									{
										F_StartHour.Text = String.Empty;
									}
								}
								NewId.Text = "-1";
								F_Title2.Text = String.Empty;
								CompanyId.Text = String.Empty;
								HiddenStartHour.Value = UC.WorkStartHour;
								HiddenEndHour.Value = UC.WorkEndHour;
								AddKeepAlive();
							}
							else
							{
								if (Request.Params["mode"] == "VIE")
								{
									CheckReminder.Checked = false;
									CheckReminder.Enabled = true;
									FillViewCard(int.Parse(Request.Params["id"]));
								}
								else
								{
									FillTEvento(int.Parse(Request.Params["id"]));
									ViewAppointmentForm.Visible = false;
									AppointmentCard.Visible = true;
									HeaderRecurrence.Visible = false;
									CorpoRecurrence.Visible = false;
									SomeJS.Text = "<script>ShowRoom();</script>";
								}
							}
						}
						FillUserApp();
					}
				}
			}
		}

		private void FillFromActivity()
		{
			FillUserApp();

			F_ContactID.Text = (Session["activityContactID"] != null) ? Session["activityContactID"].ToString() : "";
			F_Title.Text = (Session["activityContactText"] != null) ? Session["activityContactText"].ToString() : "";
			F_Title2.Text = (Session["activityAziendaText"] != null) ? Session["activityAziendaText"].ToString() : "";
			CompanyId.Text = (Session["activityAziendaID"] != null) ? Session["activityAziendaID"].ToString() : "";
			NewId.Text = "-1";
			F_StartDate.Text = Session["activitydata"].ToString();
			UserApp.SelectedIndex = -1;
			foreach (ListItem i in UserApp.Items)
			{
				if (Session["activityOwnerID"].ToString() == i.Value)
				{
					i.Selected = true;
					break;
				}
			}

			F_note.Text = (Session["activitynote"] != null) ? Session["activitynote"].ToString() : "";
			CheckReminder.Checked = false;
			CheckReminder.Enabled = true;

			Session.Remove("fromactivity");
			Session.Remove("activityContactID");
			Session.Remove("activityContactText");
			Session.Remove("activityAziendaID");
			Session.Remove("activityAziendaText");
			Session.Remove("activitydata");
			Session.Remove("activityOwnerID");
			Session.Remove("activityOwner");
			Session.Remove("activitynote");
		}

		private void FillDropDownRec()
		{
			int i;
			for (i = 1; i < 7; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (22 + i).ToString());
				RecMode.Items.Add(lt);
			}

			for (i = 1; i < 6; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (45 + i).ToString());
				RecMonthlyDayPU.Items.Add(lt);
				RecYearDayPU.Items.Add(lt);
			}

			for (i = 8; i < 11; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evnttxt" + (50 + i - 7).ToString());
				RecMonthlyDayDays.Items.Add(lt);
				RecYearDayDays.Items.Add(lt);
			}


			DateTime giorno = new DateTime(2003, 8, 3);
			DateTime month = new DateTime(2003, 1, 1);
			for (i = 0; i < 7; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = (i + 1).ToString();
				lt.Text = UC.myDTFI.GetDayName(giorno.AddDays(i).DayOfWeek);
				RecMonthlyDayDays.Items.Add(lt);
				RecSetDays.Items.Add(lt);
				RecYearDayDays.Items.Add(lt);
			}
			for (i = 0; i < 12; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = (i + 1).ToString();
				lt.Text = UC.myDTFI.GetMonthName(month.AddMonths(i).Month);
				RecYearMonths.Items.Add(lt);
				RecYearDayMonths.Items.Add(lt);
			}
		}

        private void GetOwnerName()
        {
            if (UC.ImpersonateUser == 0)
            {
                AgendaOwner.Text = UC.UserRealName;
            }
            else
            {
                DataSet ds = DatabaseConnection.CreateDataset("SELECT (SURNAME+' '+NAME) AS UTETX FROM ACCOUNT WHERE UID=" + UC.ImpersonateUser);
                DataRow dr = ds.Tables[0].Rows[0];
                AgendaOwner.Text = dr["Utetx"].ToString();
            }
        }

		private void FillRecType()
		{
			RecType.DataTextField = "Description";
			RecType.DataValueField = "ID";
			RecType.DataSource = DatabaseConnection.CreateReader("SELECT ID,DESCRIPTION FROM RECURRENCE WHERE STANDARD=1 ORDER BY STANDARD DESC;");
			RecType.DataBind();
			RecType.Items.Insert(0,Root.rm.GetString("Evnttxt39"));
			RecType.SelectedIndex = 0;
			RecType.Items[0].Value = "0";
			RecType.Items.Insert(1,Root.rm.GetString("Evnttxt40"));
			RecType.Items[1].Value = "99";
		}

		private void FillUserApp()
		{
			string sqlString = "SELECT UID,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE (UID=" + UC.UserId;
			sqlString += " OR DIARYACCOUNT LIKE '|%" + UC.UserId.ToString() + "|%')";
			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			UserApp.DataTextField = "UserName";
			UserApp.DataValueField = "uid";
			UserApp.DataSource = ds;
			UserApp.DataBind();
			int actualUser;
			if (UC.ImpersonateUser == 0)
			{
				actualUser = UC.UserId;
			}
			else
			{
				actualUser = UC.ImpersonateUser;
			}
			UserApp.SelectedItem.Selected = false;
			foreach (ListItem li in UserApp.Items)
			{
				if (li.Value == actualUser.ToString())
				{
					li.Selected = true;
					break;
				}
			}


		}

		private void FillViewCard(int id)
		{
			string sqlString = "SELECT BASE_CALENDAR.*,CONVERT(VARCHAR(10),BASE_CALENDAR.STARTDATE,105) AS DATA, CONVERT(VARCHAR(5),BASE_CALENDAR.STARTDATE,114) AS DALLE,CONVERT(VARCHAR(5),BASE_CALENDAR.ENDDATE,114) AS ALLE, " +
				"(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS USERNAME " +
				"FROM BASE_CALENDAR LEFT OUTER JOIN ACCOUNT ON BASE_CALENDAR.UID = ACCOUNT.UID WHERE BASE_CALENDAR.ID=@ID AND BASE_CALENDAR.UID=@UID";
			DbSqlParameterCollection p = new DbSqlParameterCollection();
			p.Add(new DbSqlParameter("@ID", id));
			p.Add(new DbSqlParameter("@UID", ((UC.ImpersonateUser != 0) ? UC.ImpersonateUser : UC.UserId)));
			DataSet d = DatabaseConnection.SecureCreateDataset(sqlString, p);
			if (d.Tables[0].Rows.Count > 0)
			{
				ViewAppointmentForm.DataSource = d;
				ViewAppointmentForm.DataBind();
				ViewAppointmentForm.Visible = true;
				AppointmentCard.Visible = false;
			}
			else
			{
				Response.Redirect("/today.aspx?logoff=true");
			}
		}

		public void ItemDataBoundView(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal DateLTZ = (Literal) e.Item.FindControl("Date");
					DateLTZ.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToShortDateString();
					Literal DateFromLTZ = (Literal) e.Item.FindControl("DateFrom");
					DateFromLTZ.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToString(@"HH:mm");
					Literal DateToLTZ = (Literal) e.Item.FindControl("DateTo");
					DateToLTZ.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "enddate"))).ToString(@"HH:mm");
					LinkButton Submit = (LinkButton) e.Item.FindControl("Submit");
					Submit.Text =Root.rm.GetString("Evnttxt38");
					break;
			}
		}

		public void ItemCommandView(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Modify":
					FillTEvento(int.Parse(((Literal) e.Item.FindControl("NewId")).Text));
					ViewAppointmentForm.Visible = false;
					AppointmentCard.Visible = true;
					HeaderRecurrence.Visible = false;
					CorpoRecurrence.Visible = false;
					SomeJS.Text = "<script>ShowRoom();</script>";
					break;
			}
		}

		private void FillTEvento(int id)
		{
			DataSet dsContact;
			DbSqlParameterCollection p = new DbSqlParameterCollection();
			p.Add(new DbSqlParameter("@ID", id));
			p.Add(new DbSqlParameter("@UID", ((UC.ImpersonateUser != 0) ? UC.ImpersonateUser : UC.UserId)));
			dsContact = DatabaseConnection.SecureCreateDataset("SELECT * FROM BASE_CALENDAR WHERE ID = @ID AND UID = @UID", p);
			if (dsContact.Tables[0].Rows.Count > 0)
			{
				DataRow drContact = dsContact.Tables[0].Rows[0];
				NewId.Text = drContact["id"].ToString();
				F_StartDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["startdate"])).ToShortDateString();
				F_EndDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["enddate"])).ToShortDateString();
				F_StartHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["startdate"])).ToShortTimeString();
				F_EndHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["enddate"])).ToShortTimeString();
				F_Title.Text = drContact["CONTACT"].ToString();
				F_Title2.Text = drContact["Company"].ToString();
				Room.Text = drContact["ROOM"].ToString();
				Address.Text = drContact["ADDRESS"].ToString();
				City.Text = drContact["CITY"].ToString();
				Province.Text = drContact["PROVINCE"].ToString();
				CAP.Text = drContact["CAP"].ToString();
				F_note.Text = drContact["Note"].ToString();
				Phone.Text = drContact["Phone"].ToString();
				F_ContactID.Text = drContact["ContactID"].ToString();
				CompanyId.Text = drContact["CompanyID"].ToString();



				if ((bool) drContact["PLACE"])
				{
					CheckSite.Checked = true;
				}
				else
				{
					CheckSite.Checked = false;
				}
				HiddenStartHour.Value = UC.WorkStartHour;
				HiddenEndHour.Value = UC.WorkEndHour;
				CheckReminder.Enabled = false;

				if (drContact["uid"].ToString() != drContact["SecondUID"].ToString())
				{
					Companion.Text = DatabaseConnection.SqlScalar("SELECT SURNAME+' '+NAME FROM ACCOUNT WHERE UID=" + drContact["SecondUID"].ToString());
					IdCompanion.Text = drContact["SecondUID"].ToString();
				}
			}
			else
			{
				Response.Redirect("/today.aspx?logoff=true");
			}
			AddKeepAlive();
		}

		public void UserApp_IndexChange(Object sender, EventArgs e)
		{
			if (UserApp.SelectedItem.Value != UC.UserId.ToString())
			{
				LbFlagNotify.Text =Root.rm.GetString("Evnttxt56");
				LbFlagNotify.Visible = true;

				bool flagNotify = (bool) DatabaseConnection.SqlScalartoObj("SELECT FLAGNOTIFYAPPOINTMENT FROM ACCOUNT WHERE UID=" + UserApp.SelectedItem.Value);

				if (flagNotify)
				{
					CheckNotify.Checked = true;
					CheckNotify.Enabled = false;
				}
				else
				{
					CheckNotify.Checked = false;
				}
				CheckNotify.Visible = true;
			}
		}

		public void Submit_Click(Object sender, EventArgs e)
		{
			bool datevalid;
			bool datevalid2 = true;
			try
			{
				datevalid = true;
				try
				{
					if (F_EndDate.Text.Length > 0)
						Convert.ToDateTime(F_EndDate.Text, UC.myDTFI);

					datevalid2 = true;
				}
				catch
				{
					datevalid2 = false;
				}
			}
			catch
			{
				datevalid = false;
				try
				{
					if (F_EndDate.Text.Length > 0)
						Convert.ToDateTime(F_EndDate.Text, UC.myDTFI);

					datevalid2 = true;
				}
				catch
				{
					datevalid2 = false;
				}
			}

			if (datevalid && datevalid2 && F_EndDate.Text.Length > 0)
			{
				DateTime firstdate = Convert.ToDateTime(F_StartDate.Text,UC.myDTFI);
				DateTime seconddate = Convert.ToDateTime(F_EndDate.Text,UC.myDTFI);
				if(seconddate<firstdate)
				{
					DateEr2.Text += "<span style=\"color:red;\">*</span>";
					Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" +Root.rm.GetString("Evnttxt73") + "</span>";
					return;
				}
			}

			if (datevalid && datevalid2)
			{
				string sSql;
				int newID = int.Parse(NewId.Text);
				if (newID != -1)
				{
					if (UC.ImpersonateUser == 0)
					{
						sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE ID<>" + newID + " AND UID=" + UC.UserId + " AND ((";
					}
					else
					{
						sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE ID<>" + newID + " AND UID=" + UC.ImpersonateUser + " AND ((";
					}
				}
				else
				{
					if (UC.ImpersonateUser == 0)
					{
						sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE UID=" + UC.UserId.ToString() + " AND ((";
					}
					else
					{
						sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE UID=" + UC.ImpersonateUser + " AND ((";
					}

				}

				DateTime start = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI));
				DateTime endDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_EndHour.Text, UC.myDTFI));

				sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND STARTDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE<='" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE>='" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(ENDDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<='" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "'))";
				sSql += " OR (RECURRID>0))";

				DataSet testCoverage = DatabaseConnection.CreateDataset(sSql);
				if (testCoverage.Tables[0].Rows.Count > 0)
				{
					bool free = true;
					DataRow[] drBusyreal = testCoverage.Tables[0].Select("RECURRID=0");
					if (drBusyreal.Length > 0)
						free = false;
					else
					{
						DataRow[] drBusy = testCoverage.Tables[0].Select("RECURRID>0");
						if (drBusy.Length > 0)
						{
							foreach (DataRow dr in drBusy)
							{
								Recurrence recurrence = new Recurrence(UC);
								ArrayList AL = recurrence.Remind((int) dr["recurrid"], Convert.ToDateTime(F_StartDate.Text + " 00:00:00", UC.myDTFI), Convert.ToDateTime(F_StartDate.Text + " 23:59:59", UC.myDTFI));
								if (AL.Count > 0)
								{
									DateTime drStart = ((DateTime) AL[0]).AddHours(((DateTime) dr["startdate"]).Hour);
									DateTime drEnd = ((DateTime) AL[0]).AddHours(((DateTime) dr["enddate"]).Hour);
									if ((drStart > start && drStart < endDate) ||
										(drStart <= start && drEnd >= endDate) ||
										(drEnd > start && drEnd <= endDate) ||
										(drStart > start && drEnd < endDate))
									{
										free = false;
									}
									else
									{
										free = true;
									}
								}
								else
								{
									free = true;
								}
							}
						}

					}

					if (free)
					{
						if (IdCompanion.Text.Length > 0)
						{
							if (ControllaCompanion())
							{
								string newId;
								newId = ModifyDataSet(UserApp.SelectedItem.Value, NewId.Text, NewId.Text);
								string crossId = ModifyDataSet(IdCompanion.Text, "-1", newId);
								DatabaseConnection.DoCommand("UPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossId + " WHERE ID=" + newId);
								Message(int.Parse(IdCompanion.Text),Root.rm.GetString("Evnttxt1"), F_StartDate.Text + "&nbsp;" + F_StartHour.Text + "-" + F_EndHour.Text + "<br>" + F_Title.Text + "<br>" + F_note.Text, F_StartDate.Text + "&nbsp;" + F_StartHour.Text + "-" + F_EndHour.Text + "<br>" + F_Title.Text + "<br>" + F_note.Text);

								Response.Redirect("agenda.aspx?m=25&si=2&date=" + F_StartDate.Text);
							}
							else
							{
								Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" +Root.rm.GetString("Evnttxt68") + "</span>";
							}
						}
						else
						{
							ModifyDataSet(UserApp.SelectedItem.Value, NewId.Text, NewId.Text);
							Response.Redirect("agenda.aspx?m=25&si=2&date=" + F_StartDate.Text);
						}
					}
					else
					{
						Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" +Root.rm.GetString("Evnttxt72") + "</span>";
					}

				}
				else
				{
					if (IdCompanion.Text.Length > 0)
					{
						if (ControllaCompanion())
						{
							string newId;
							newId = ModifyDataSet(UserApp.SelectedItem.Value, NewId.Text, NewId.Text);
							string crossId = ModifyDataSet(IdCompanion.Text, "-1", newId);
							DatabaseConnection.DoCommand("UPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossId + " WHERE ID=" + newId);
							Message(int.Parse(IdCompanion.Text),Root.rm.GetString("Evnttxt1"), F_StartDate.Text + "&nbsp;" + F_StartHour.Text + "-" + F_EndHour.Text + "<br>" + F_Title.Text + "<br>" + F_note.Text, F_StartDate.Text + "&nbsp;" + F_StartHour.Text + "-" + F_EndHour.Text + "<br>" + F_Title.Text + "<br>" + F_note.Text);
							Response.Redirect("agenda.aspx?m=25&si=2&date=" + F_StartDate.Text);
						}
						else
						{
							Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">Attenzione, Companion occupato.</span>";
						}
					}
					else
					{
						ModifyDataSet(UserApp.SelectedItem.Value, NewId.Text, NewId.Text);
						Response.Redirect("agenda.aspx?m=25&si=2&date=" + F_StartDate.Text);
					}
				}
			}
			else
			{
				if (!datevalid)
					DateEr1.Text += "<span style=\"color:red;\">*</span>";


				if (!datevalid2)
					DateEr2.Text += "<span style=\"color:red;\">*</span>";

			}
		}

		private bool ControllaCompanion()
		{
			string sSql;
			if (int.Parse(NewId.Text) != -1)
			{
				if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM BASE_CALENDAR WHERE SENCONDIDOWNER=" + int.Parse(NewId.Text) + " AND UID=" + int.Parse(IdCompanion.Text))) > 0)
					return true;
				else
					sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE ID<>" + int.Parse(NewId.Text) + " AND UID=" + int.Parse(IdCompanion.Text) + " AND ((";
			}
			else
			{
				sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE UID=" + int.Parse(IdCompanion.Text) + " AND ((";
			}

			DateTime start = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI));
			DateTime end = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_EndHour.Text, UC.myDTFI));

			sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND STARTDATE<'" + end.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
			sSql += "(STARTDATE<='" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE>='" + end.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
			sSql += "(ENDDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<='" + end.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
			sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<'" + end.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "'))";
			sSql += " OR (RECURRID>0))";

			DataSet testCoverage = DatabaseConnection.CreateDataset(sSql);
			if (testCoverage.Tables[0].Rows.Count > 0)
			{
				bool free = true;
				DataRow[] drBusy = testCoverage.Tables[0].Select("RECURRID>0");
				if (drBusy.Length > 0)
				{
					foreach (DataRow dr in drBusy)
					{
						Recurrence recurrence = new Recurrence(UC);
						ArrayList AL = recurrence.Remind((int) dr["recurrid"], Convert.ToDateTime(F_StartDate.Text + " 00:00:00", UC.myDTFI), Convert.ToDateTime(F_StartDate.Text + " 23:59:59", UC.myDTFI));
						if (AL.Count > 0)
						{
							DateTime drStart = ((DateTime) AL[0]).AddHours(((DateTime) dr["startdate"]).Hour);
							DateTime drEnd = ((DateTime) AL[0]).AddHours(((DateTime) dr["enddate"]).Hour);
							if ((drStart > start && drStart < end) ||
								(drStart <= start && drEnd >= end) ||
								(drEnd > start && drEnd <= end) ||
								(drStart > start && drEnd < end))
							{
								free = false;
							}
							else
							{
								free = true;
							}
						}
						else
						{
							free = true;
						}
					}
				}
				else
				{
					free = false;
				}
				return free;
			}
			return true;
		}

		private string ModifyDataSet(string uteUID, string id, string newId)
		{
			string sqlNewId = String.Empty;
			string visitId = String.Empty;
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("UID", uteUID);
				dg.Add("UIDINS", UC.UserId.ToString());
				dg.Add("CREATEDBYID", UC.UserId.ToString());
				dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI)));
				dg.Add("ENDDATE", (F_EndDate.Text.Length > 0) ?
					UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_EndDate.Text + " " + F_EndHour.Text, UC.myDTFI))
					: UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_EndHour.Text, UC.myDTFI)));
				dg.Add("CONTACT", F_Title.Text);
				dg.Add("COMPANY", F_Title2.Text);
				if (CompanyId.Text.Length > 0) dg.Add("COMPANYID", CompanyId.Text);
				if (F_ContactID.Text.Length > 0) dg.Add("CONTACTID", F_ContactID.Text);


				dg.Add("NOTE", F_note.Text);
				if (CheckSite.Checked)
				{
					dg.Add("PLACE", 1);
					dg.Add("ROOM", Room.Text);
				}
				else
				{
					dg.Add("PLACE", 0);
					dg.Add("ADDRESS", Address.Text);
					dg.Add("CITY", City.Text);
					dg.Add("PROVINCE", Province.Text);
					dg.Add("CAP", CAP.Text);
				}
				if (id == "-1")
				{
					if (CheckRecurrent.Checked)
						dg.Add("RECURRID", NewRecurrence());
				}
				if (IdCompanion.Text.Length > 0)
				{
					if (uteUID == IdCompanion.Text)
						dg.Add("SECONDUID", UserApp.SelectedItem.Value);
					else
						dg.Add("SECONDUID", IdCompanion.Text);
				}

				if (uteUID == UC.UserId.ToString())
					dg.Add("CONFIRMATION", 1);

				dg.Add("PHONE", Phone.Text);
				if (id != newId)
					dg.Add("SENCONDIDOWNER", newId);
				visitId = id;
				object newId2;
				if (id == newId)
					newId2 = dg.Execute("BASE_CALENDAR", "ID=" + id, DigiDapter.Identities.Identity);
				else
					newId2 = dg.Execute("BASE_CALENDAR", "UID = " + uteUID + " AND SENCONDIDOWNER=" + newId, DigiDapter.Identities.Identity);


				if (dg.RecordInserted)
				{
					sqlNewId = newId2.ToString();
					visitId = sqlNewId;
				}
				else
				{
					visitId = id;
				}


			}



			if (CheckNotify.Visible)
			{
				StringBuilder body = new StringBuilder();
				body.Append("<table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"99%\" style=\"font-family:verdana;font-size:10px;border:1px solid black;\"align=\"center\">");
				body.Append("<tr><td width=\"50%\" VAlign=\"TOP\">");
				body.Append("<table border=\"0\" cellpadding=\"2\" cellspacing=\"5\" width=\"100%\" style=\"font-family:verdana;font-size:10px;\" align=\"center\">");
				body.Append("<tr><td style=\"border-bottom: 1px solid black;\" colspan=\"2\">");
				body.Append(UserApp.SelectedItem.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt4"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(F_StartDate.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt5"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(F_StartHour.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt6"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(F_EndHour.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt8"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(F_Title.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt9"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(F_Title2.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt64"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(Phone.Text);
				body.Append("</td></tr></table></td><td width=\"50%\" VAlign=\"TOP\">");
				body.Append("<table id=\"tblStanza\" border=\"0\" cellpadding=\"2\" cellspacing=\"5\" width=\"100%\" style=\"font-family:verdana;font-size:10px;\" align=\"center\">");
				body.Append("<tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt11"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(Room.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt12"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(Address.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt13"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(City.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt14"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(Province.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt15"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(CAP.Text);
				body.Append("</td></tr></table></td></tr><tr><td colspan=\"2\">");
				body.Append(Root.rm.GetString("Evnttxt16"));
				body.Append("</td></tr><tr><td colspan=\"2\">");
				body.Append(F_note.Text);
				body.Append("</td></tr></table>");
				Message(int.Parse(uteUID),Root.rm.GetString("Evnttxt1"), F_StartDate.Text + "&nbsp;" + F_StartHour.Text + "-" + F_EndHour.Text + "<br>" + F_Title.Text + "<br>" + F_note.Text, body.ToString());
			}


			if (CheckReminder.Checked)
			{
				if (CompanyId.Text.Length > 0 || F_ContactID.Text.Length > 0)
				{
					string subject =Root.rm.GetString("Evnttxt1");
					string description = F_note.Text + "\r\n\r\n";

					ActivityInsert ai = new ActivityInsert();
					Session["Reminder_RemNote"]=Reminder_RemNote.Text;
					ai.InsertActivity("6", visitId, uteUID, F_ContactID.Text, CompanyId.Text, "", subject, description, UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI)), UC, 0, int.Parse(((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedValue));
				}
			}

			return visitId;
		}

		private int NewRecurrence()
		{
			int newId;
			using (DigiDapter dg = new DigiDapter())
			{
				if (RecDateStart.Text.Length > 0)
				{
					dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecDateStart.Text, UC.myDTFI)));
				}
				else
				{
					dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(DateTime.Now.ToShortDateString())));
				}
				if (RecEndDate.Text.Length > 0)
				{
					dg.Add("ENDDATE", Convert.ToDateTime(RecEndDate.Text, UC.myDTFI));
				}
				else
				{
					dg.Add("ENDDATE", DateTime.Now.AddYears(50));
				}
				dg.Add("TYPE", RecMode.SelectedItem.Value);
				switch (RecMode.SelectedItem.Value)
				{
					case "1":
						dg.Add("DESCRIPTION", "Ric.personale Giornaliero");
						dg.Add("VAR1", RecDayDays.Text);
						dg.Add("VAR2", (RecWorkingDay.Checked) ? 1 : 0);
						break;
					case "2":
						int[] matrix = new int[]{0,1,2,4,8,16,32,64};
						dg.Add("DESCRIPTION", "Ric.personale Weekle");
						dg.Add("VAR1", RecSettSS.Text);
						int var2 = 0;
						int var3 = 0;
						foreach (ListItem i in RecSetDays.Items)
						{
							if (i.Selected)
							{
								var2 += matrix[Convert.ToInt32(i.Value)];
								var3++;
							}
						}

						dg.Add("VAR2", var2);
						dg.Add("VAR3", (var3 == 1) ? 0 : 1);

						break;
					case "3":
						dg.Add("DESCRIPTION", "Ric.personale Mensile");
						dg.Add("VAR1", RecMonthlyDays.Text);
						dg.Add("VAR2", RecMonthlyMonths.Text);
						break;
					case "4":
						dg.Add("DESCRIPTION", "Ric.personale Mensile giorno");
						dg.Add("VAR1", RecMonthlyDayPU.SelectedItem.Value);
						dg.Add("VAR2", RecMonthlyDayDays.SelectedItem.Value);
						dg.Add("VAR3", (RecMonthlyDayMonths.Text.Length > 0) ? RecMonthlyDayMonths.Text : "1");
						break;
					case "5":
						dg.Add("DESCRIPTION", "Ric.personale Annuale");
						dg.Add("VAR1", RecYearDays.Text);
						dg.Add("VAR2", RecYearMonths.SelectedItem.Value);
						break;
					case "6":
						dg.Add("DESCRIPTION", "Ric.personale Annuale giorno");
						dg.Add("VAR1", RecYearDayPU.SelectedItem.Value);
						dg.Add("VAR2", RecYearDayDays.SelectedItem.Value);
						dg.Add("VAR3", RecYearDayMonths.SelectedItem.Value);
						break;
				}
				newId = Convert.ToInt32(dg.Execute("RECURRENCE", DigiDapter.Identities.Identity));
			}

			return newId;
		}

	}
}
