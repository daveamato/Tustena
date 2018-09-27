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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class Events : G
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
                GetOwnerName();
				if (!Page.IsPostBack)
				{
					Submit.Text =Root.rm.GetString("Evetxt36");
					DateEr1.Text = "(" + UC.myDTFI.ShortDatePattern.ToString() + ")";
					FillDropDownRec();
					FillRecType();
					if (Request.Params["mode"] != null)
					{
						if (Request.Params["mode"] == "NEW")
						{
							if (Request.Params["data"] != null)
							{
								try
								{
									StartDate.Text = Convert.ToDateTime(Request.Params["data"], UC.myDTFI).ToString(@"dd/MM/yyyy");
								}
								catch
								{
									StartDate.Text = String.Empty;
								}
							}
							if (Request.Params["ora1"] != null)
							{
								try
								{
									StartHour.Text = Request.Params["ora1"];
								}
								catch
								{
									StartHour.Text = String.Empty;
								}
							}
							NewId.Text = "-1";
						}
						else
						{
							FillViewCard(int.Parse(Request.Params["id"]));
							FillEvent(int.Parse(Request.Params["id"]));
							ViewAppointmentForm.Visible = false;
							AppointmentCard.Visible = true;
							HeaderRecurrence.Visible = false;
							CorpoRecurrence.Visible = false;
						}
					}
					FillUserApp();
				}
			}
		}

		private void FillDropDownRec()
		{
			int i;
			for (i = 1; i < 7; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evetxt" + (13 + i).ToString());
				RecMode.Items.Add(lt);
			}

			for (i = 1; i < 6; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evetxt" + (26 + i).ToString());
				RecMonthlyDayPU.Items.Add(lt);
				RecYearDayPU.Items.Add(lt);
			}

			for (i = 8; i < 11; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Evetxt" + (31 + i - 7).ToString());
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

		private void FillRecType()
		{
			RecType.DataTextField = "Description";
			RecType.DataValueField = "ID";
			RecType.DataSource = DatabaseConnection.CreateReader("SELECT ID,DESCRIPTION FROM RECURRENCE WHERE STANDARD=1 ORDER BY STANDARD DESC;");
			RecType.DataBind();
			RecType.Items.Insert(0,Root.rm.GetString("Evetxt39"));
			RecType.SelectedIndex = 0;
			RecType.Items[0].Value = "0";
			RecType.Items.Insert(1,Root.rm.GetString("Evetxt40"));
			RecType.Items[1].Value = "99";
		}

		private void FillUserApp()
		{
			string sqlString = "SELECT *,(SURNAME+' '+NAME) AS USERNAME FROM ACCOUNT WHERE UID=" + UC.UserId;
			sqlString += " OR DIARYACCOUNT LIKE '|%" + UC.UserId.ToString() + "|%'";
			UserApp.DataTextField = "UserName";
			UserApp.DataValueField = "uid";
			UserApp.DataSource = DatabaseConnection.CreateDataset(sqlString);
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
			string sqlString = "SELECT BASE_EVENTS.*, ";
			sqlString += "(ACCOUNT.SURNAME+' '+ACCOUNT.NAME) AS USERNAME,  (CREATEDBY.SURNAME+' '+CREATEDBY.NAME) AS CREATEUTE ";
			sqlString += "FROM BASE_EVENTS LEFT OUTER JOIN ACCOUNT ON BASE_EVENTS.UID = ACCOUNT.UID LEFT OUTER JOIN ACCOUNT AS CREATEDBY ON BASE_EVENTS.CREATEDBYID = CREATEDBY.UID WHERE BASE_EVENTS.ID=" + id;

			Trace.Warn("ma", sqlString);

			ViewAppointmentForm.DataSource = DatabaseConnection.CreateDataset(sqlString);
			ViewAppointmentForm.DataBind();
			ViewAppointmentForm.Visible = true;
			AppointmentCard.Visible = false;
		}

		protected void ItemDataBoundView(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal DateLTZ = (Literal) e.Item.FindControl("Date");
					DateLTZ.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToString(@"dd/MM/yyyy");
					Literal DateToLTZ = (Literal) e.Item.FindControl("DateTo");
					DateToLTZ.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(DataBinder.Eval((DataRowView) e.Item.DataItem, "startdate"))).ToString(@"HH:mm");
					LinkButton Submit = (LinkButton) e.Item.FindControl("Submit");
					Submit.Text =Root.rm.GetString("Evetxt37");
					break;
			}
		}

		protected void ItemCommandView(Object sender, RepeaterCommandEventArgs e)
		{
			Trace.Warn(e.CommandName);
			switch (e.CommandName)
			{
				case "Modify":
					FillEvent(int.Parse(((Literal) e.Item.FindControl("NewId")).Text));
					ViewAppointmentForm.Visible = false;
					AppointmentCard.Visible = true;
					HeaderRecurrence.Visible = false;
					CorpoRecurrence.Visible = false;

					break;
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

		private void FillEvent(int id)
		{
			DataSet dsContact;
			dsContact = DatabaseConnection.CreateDataset("SELECT * FROM BASE_EVENTS WHERE ID = " + id);
			DataRow drContact = dsContact.Tables[0].Rows[0];
			NewId.Text = drContact["id"].ToString();
			StartDate.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["startdate"])).ToShortDateString();
			StartHour.Text = UC.LTZ.ToLocalTime(Convert.ToDateTime(drContact["startdate"])).ToShortTimeString();
			AgTitle.Text = drContact["Title"].ToString();
			Note.Text = drContact["Note"].ToString();
		}

		protected void Submit_Click(Object sender, EventArgs e)
		{
			ModifyDataSet(NewId.Text);
			Response.Redirect("agenda.aspx?m=25&si=2");
		}

		private void ModifyDataSet(string id)
		{
			string sqlString = "SELECT ID FROM BASE_EVENTS WHERE ID = " + id;
			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				if (!dg.HasRows)
				{
					dg.Add("CREATEDBYID", UC.UserId, 'I');
				}


				dg.Add("UID", UserApp.SelectedItem.Value);
				dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(StartDate.Text + " " + StartHour.Text, UC.myDTFI)));
				dg.Add("TITLE", AgTitle.Text);
				dg.Add("NOTE", Note.Text);
				dg.Add("LASTMODIFIEDBYID", UC.UserId);


				if (id == "-1")
				{
					if (CheckRecurrent.Checked)
					{
						{
							dg.Add("RECURRID", NewRecurrence());
						}
					}
				}
				dg.Execute("BASE_EVENTS", "id=" + id);
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
			this.Submit.Click += new EventHandler(this.Submit_Click);

		}

		#endregion

		private int NewRecurrence()
		{
			int newId = -1;
			using (DigiDapter dg = new DigiDapter())
			{
				if (RecDateStart.Text.Length > 0)
				{
					dg.Add("STARTDATE", Convert.ToDateTime(RecDateStart.Text, UC.myDTFI));
				}
				else
				{
					dg.Add("STARTDATE", Convert.ToDateTime(DateTime.Now.ToShortDateString()));
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
						dg.Add("VAR3", RecMonthlyDayMonths.Text);
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
