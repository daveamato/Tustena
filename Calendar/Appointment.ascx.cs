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
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena
{
	public delegate void OnSaveAppointment(bool saved);
	public partial class AppointmentControl : UserControl
	{
		public event OnSaveAppointment onSaveAppointment;





		private UserConfig UC = new UserConfig();

		public string ContactID
		{
			get
			{
				object o = this.ViewState["_ContactID" + this.ID];
				if (o == null)
					return null;
				else
					return o.ToString();
			}
			set
			{
				this.ViewState["_ContactID" + this.ID] = value;

				if(value.Substring(0,1)=="C")
				{
					TxtContactID.Text = value.Substring(1);
					TxtContact.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID="+TxtContactID.Text);
				}
				else
				{
					TxtContactID.Text = value.Substring(1);
					TxtContact.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM CRM_LEADS WHERE ID="+value.Substring(1));
				}
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
				 Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "block",string.Format("<script>var appvrfy = \"{0}\";var appvrfy2 = \"{1}\";var confdate = \"{2}\";var autofill = \"{3}\";</script>",Root.rm.GetString("Evnttxt67"),Root.rm.GetString("Evnttxt66"),Root.rm.GetString("Evnttxt60"),Root.rm.GetString("Evnttxt62")));

				UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
				Submit.Attributes.Add("onclick", "BeforeSubmit('" + this.ID + "');");
				DateEr1.Text = "(" + UC.myDTFI.ShortDatePattern.ToString() + ")";
				DateEr2.Text = "(" + UC.myDTFI.ShortDatePattern.ToString() + ")";

				UserAppImg.Attributes.Add("onclick","AppointmentVerify(2,event,'"+this.ID+"');");
				UserAppImg2.Attributes.Add("onclick","RemoveCompanion('"+this.ID+"');");

				if (!Page.IsPostBack)
				{
					Submit.Text = Root.rm.GetString("Evnttxt36");
					FillUserAppointment();
					CheckSite.Attributes.Add("onclick","ShowRoom('" +this.ID + "')");
					HiddenStartHour.Value = UC.WorkStartHour;
					HiddenEndHour.Value = UC.WorkEndHour;
					NewId.Text="-1";
				}
		}

		public void Submit_Click(Object sender, EventArgs e)
		{
			bool dateValid;
			bool dateValid2 = true;
			try
			{
				dateValid = true;
				try
				{
					if (TxtEndHour.Text.Length > 0)
						Convert.ToDateTime(TxtEndHour.Text, UC.myDTFI);

					dateValid2 = true;
				}
				catch
				{
					dateValid2 = false;
				}
			}
			catch
			{
				dateValid = false;
				try
				{
					if (TxtEndHour.Text.Length > 0)
						Convert.ToDateTime(TxtEndHour.Text, UC.myDTFI);

					dateValid2 = true;
				}
				catch
				{
					dateValid2 = false;
				}
			}
			if (dateValid && dateValid2)
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

				DateTime beginTime = UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text + " " + TxtStartHour.Text, UC.myDTFI));
				DateTime endTime = UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtEndDate.Text + " " + TxtEndHour.Text, UC.myDTFI));

				sSql += "(STARTDATE>'" + beginTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND STARTDATE<'" + endTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE<='" + beginTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND ENDDATE>='" + endTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
				sSql += "(ENDDATE>'" + beginTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND ENDDATE<='" + endTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE>'" + beginTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND ENDDATE<'" + endTime.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "'))";
				sSql += " OR (RECURRID>0))";

				DataSet checkTimeLine = DatabaseConnection.CreateDataset(sSql);
				if (checkTimeLine.Tables[0].Rows.Count > 0)
				{
					bool free = true;
					DataRow[] drInUse = checkTimeLine.Tables[0].Select("RECURRID>0");
					if (drInUse.Length > 0)
					{
						foreach (DataRow dr in drInUse)
						{
							Recurrence recurrence = new Recurrence(UC);
							ArrayList AL = recurrence.Remind((int) dr["recurrid"], Convert.ToDateTime(TxtStartDate.Text + " 00:00:00", UC.myDTFI), Convert.ToDateTime(TxtStartDate.Text + " 23:59:59", UC.myDTFI));
							if (AL.Count > 0)
							{
								DateTime drStart = ((DateTime) AL[0]).AddHours(((DateTime) dr["startdate"]).Hour);
								DateTime drEnd = ((DateTime) AL[0]).AddHours(((DateTime) dr["enddate"]).Hour);
								if ((drStart > beginTime && drStart < endTime) ||
									(drStart <= beginTime && drEnd >= endTime) ||
									(drEnd > beginTime && drEnd <= endTime) ||
									(drStart > beginTime && drEnd < endTime))
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

					if (free)
					{
						if (TxtAccomplistID.Text.Length > 0)
						{
							if (ControlAccompanist())
							{
								string newId;
								newId = SaveAppointment(DDDLser.SelectedItem.Value, NewId.Text, NewId.Text);
								string crossid = SaveAppointment(TxtAccomplistID.Text, "-1", newId);
								DatabaseConnection.DoCommand("uPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossid + " WHERE ID=" + newId);
								G.Message(int.Parse(TxtAccomplistID.Text), Root.rm.GetString("Evnttxt1"), TxtStartDate.Text + "&nbsp;" + TxtStartHour.Text + "-" + TxtEndDate.Text + "<br>" + TxtContact.Text + "<br>" + F_note.Text, TxtStartDate.Text + "&nbsp;" + TxtStartHour.Text + "-" + TxtEndDate.Text + "<br>" + TxtContact.Text + "<br>" + F_note.Text);

								onSaveAppointment(true);
							}
							else
							{
								Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" + Root.rm.GetString("Evnttxt68") + "</span>";
							}
						}
						else
						{
							SaveAppointment(DDDLser.SelectedItem.Value, NewId.Text, NewId.Text);
							onSaveAppointment(true);

						}
					}
					else
					{
						Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" + Root.rm.GetString("Evnttxt72")+"</span>";
					}

				}
				else
				{
					if (TxtAccomplistID.Text.Length > 0)
					{
						if (ControlAccompanist())
						{
							string newId;
							newId = SaveAppointment(DDDLser.SelectedItem.Value, NewId.Text, NewId.Text);
							string crossid = SaveAppointment(TxtAccomplistID.Text, "-1", newId);
							DatabaseConnection.DoCommand("UPDATE BASE_CALENDAR SET SENCONDIDOWNER=" + crossid + " WHERE ID=" + newId);
							G.Message(int.Parse(TxtAccomplistID.Text), Root.rm.GetString("Evnttxt1"), TxtStartDate.Text + "&nbsp;" + TxtStartHour.Text + "-" + TxtEndDate.Text + "<br>" + TxtContact.Text + "<br>" + F_note.Text, TxtStartDate.Text + "&nbsp;" + TxtStartHour.Text + "-" + TxtEndDate.Text + "<br>" + TxtContact.Text + "<br>" + F_note.Text);
							onSaveAppointment(true);
						}
						else
						{
							Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">Attenzione, Companion occupato.</span>";
						}
					}
					else
					{
						SaveAppointment(DDDLser.SelectedItem.Value, NewId.Text, NewId.Text);
						onSaveAppointment(true);
					}
				}
			}
			else
			{
				if (!dateValid)
					DateEr1.Text += "<span style=\"color:red;\">*</span>";


				if (!dateValid2)
					DateEr2.Text += "<span style=\"color:red;\">*</span>";

			}
		}

		private bool ControlAccompanist()
		{
			string sSql;
			if (NewId.Text != "-1")
			{
				if (Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT COUNT(*) FROM BASE_CALENDAR WHERE SENCONDIDOWNER=" + int.Parse(NewId.Text) + " AND UID=" + int.Parse(TxtAccomplistID.Text))) > 0)
					return true;
				else
					sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE ID<>" + NewId.Text + " AND UID=" + TxtAccomplistID.Text + " AND ((";
			}
			else
			{
				sSql = "SELECT ID,RECURRID,STARTDATE,ENDDATE FROM BASE_CALENDAR WHERE UID=" + TxtAccomplistID.Text + " AND ((";
			}

			DateTime start = UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text + " " + TxtStartHour.Text, UC.myDTFI));
			DateTime endDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text + " " + TxtEndDate.Text, UC.myDTFI));

			sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND STARTDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
			sSql += "(STARTDATE<='" + start.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' aND ENDDATE>='" + endDate.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
			sSql += "(ENDDATE>'" + start.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND ENDDATE<='" + endDate.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "') OR ";
			sSql += "(STARTDATE>'" + start.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "' AND ENDDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", G.InvariantCultureForDB) + "'))";
			sSql += " OR (RECURRID>0))";

			DataSet checkTimeLine = DatabaseConnection.CreateDataset(sSql);
			if (checkTimeLine.Tables[0].Rows.Count > 0)
			{
				bool free = true;
				DataRow[] drInUse = checkTimeLine.Tables[0].Select("RECURRID>0");
				if (drInUse.Length > 0)
				{
					foreach (DataRow dr in drInUse)
					{
						Recurrence recurrence = new Recurrence(UC);
						ArrayList AL = recurrence.Remind((int) dr["recurrid"], Convert.ToDateTime(TxtStartDate.Text + " 00:00:00", UC.myDTFI), Convert.ToDateTime(TxtStartDate.Text + " 23:59:59", UC.myDTFI));
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
				else
				{
					free = false;
				}
				return free;
			}
			return true;
		}

		private void FillUserAppointment()
		{
			string sqlString = "SELECT UID,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE (UID=" + UC.UserId.ToString();
			sqlString += " OR DIARYACCOUNT LIKE '|%" + UC.UserId.ToString() + "|%')";
			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			DDDLser.DataTextField = "UserName";
			DDDLser.DataValueField = "uid";
			DDDLser.DataSource = ds;
			DDDLser.DataBind();
			int actualUser;
			if (UC.ImpersonateUser == 0)
			{
				actualUser = UC.UserId;
			}
			else
			{
				actualUser = UC.ImpersonateUser;
			}
			DDDLser.SelectedItem.Selected = false;
			foreach (ListItem li in DDDLser.Items)
			{
				if (li.Value == actualUser.ToString())
				{
					li.Selected = true;
					break;
				}
			}
		}


		public void UserApp_IndexChange(Object sender, EventArgs e)
		{
			if (DDDLser.SelectedItem.Value != UC.UserId.ToString())
			{
				LbFlagNotify.Text = Root.rm.GetString("Evnttxt56");
				LbFlagNotify.Visible = true;
				DatabaseConnection.DoCommand("SELECT FLAGNOTIFYAPPOINTMENT FROM ACCOUNT WHERE UID=" + DDDLser.SelectedItem.Value);
				CheckNotify.Checked = true;
				CheckNotify.Enabled = false;
				CheckNotify.Visible = true;
			}
		}


		private string SaveAppointment(string userId, string appointmentId, string secondUserId)
		{
			string sqlNewId = String.Empty;
			string visitId = String.Empty;

			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("UID", userId);
				dg.Add("UIDINS" , UC.UserId);
				dg.Add("CREATEDBYID", UC.UserId);
				dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text + " " + TxtStartHour.Text, UC.myDTFI)));
				dg.Add("ENDDATE", (TxtEndDate.Text.Length > 0) ?
					UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtEndDate.Text + " " + TxtEndHour.Text, UC.myDTFI))
					: UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text + " " + TxtEndHour.Text, UC.myDTFI)));
				dg.Add("CONTACT", TxtContact.Text);
				dg.Add("COMPANY", TxtCompany.Text);
				if (TxtCompanyID.Text.Length > 0) dg.Add("COMPANYID", TxtCompanyID.Text);
				if (TxtContactID.Text.Length > 0) dg.Add("CONTACTID", TxtContactID.Text);


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

				if (TxtAccomplistID.Text.Length > 0)
				{
					if (userId == TxtAccomplistID.Text)
						dg.Add("SECONDUID", DDDLser.SelectedItem.Value);
					else
						dg.Add("SECONDUID", TxtAccomplistID.Text);
				}

				if (userId == UC.UserId.ToString())
					dg.Add("CONFIRMATION", 1);

				dg.Add("PHONE", Phone.Text);
				if (appointmentId != secondUserId)
					dg.Add("SENCONDIDOWNER", secondUserId);
				visitId = appointmentId;
				object newId;
				if (appointmentId == secondUserId)
					newId = dg.Execute("BASE_CALENDAR", "ID=" + appointmentId, DigiDapter.Identities.Identity);
				else
					newId = dg.Execute("BASE_CALENDAR", "UID = " + userId + " AND SENCONDIDOWNER=" + secondUserId, DigiDapter.Identities.Identity);


				if(dg.RecordInserted)
				{
					sqlNewId = newId.ToString();
					visitId = sqlNewId;
				}
				else
				{
					visitId = appointmentId;
				}


			}


			if (CheckNotify.Visible)
			{
				StringBuilder body = new StringBuilder();
				body.Append("<table  border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"99%\" style=\"font-family:verdana;font-size:10px;border:1px solid black;\"align=\"center\">");
				body.Append("<tr><td width=\"50%\" VAlign=\"TOP\">");
				body.Append("<table border=\"0\" cellpadding=\"2\" cellspacing=\"5\" width=\"100%\" style=\"font-family:verdana;font-size:10px;\" align=\"center\">");
				body.Append("<tr><td style=\"border-bottom: 1px solid black;\" colspan=\"2\">");
				body.Append(DDDLser.SelectedItem.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt4"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(TxtStartDate.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt5"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(TxtStartHour.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt6"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(TxtEndDate.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt8"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(TxtContact.Text);
				body.Append("</td></tr><tr><td width=\"40%\" style=\"border-bottom: 1px solid black;\">");
				body.Append(Root.rm.GetString("Evnttxt9"));
				body.Append("</td><td style=\"border-bottom: 1px solid black;\">");
				body.Append(TxtCompany.Text);
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
				G.Message(int.Parse(userId), Root.rm.GetString("Evnttxt1"), TxtStartDate.Text + "&nbsp;" + TxtStartHour.Text + "-" + TxtEndDate.Text + "<br>" + TxtContact.Text + "<br>" + F_note.Text, body.ToString());
			}


			if (CheckReminder.Checked)
			{
				if (TxtCompanyID.Text.Length > 0 || TxtContactID.Text.Length > 0)
				{
					string subject = Root.rm.GetString("Evnttxt1");
					string description = F_note.Text + "\r\n\r\n" + Root.rm.GetString("Evnttxt5") + " " + TxtStartHour.Text + " " + Root.rm.GetString("Evnttxt6") + " " + TxtEndDate.Text;

					ActivityInsert ai = new ActivityInsert();

					if(this.ContactID.Substring(0,1)=="C")
						ai.InsertActivity("6", visitId, userId, TxtContactID.Text, TxtCompanyID.Text, "", subject, description, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text+ " " + TxtStartHour.Text, UC.myDTFI)), UC);
					else
						ai.InsertActivity("6", visitId, userId, "", "", TxtContactID.Text, subject, description, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TxtStartDate.Text+ " " + TxtStartHour.Text, UC.myDTFI)), UC);
				}
			}

			return visitId;
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{

		}
		#endregion
	}
}
