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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class Meeting : G
	{
		protected HtmlTable TblRoom;
		protected HtmlTable TblAddress;
		protected LinkButton SearchUserSubmit;
		protected RequiredDomValidator RequiredFieldValidatorTitle;



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

		public void Page_Load(object sender, EventArgs e)
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
					Submit.Text =Root.rm.GetString("Meettxt51");
					HiddenStartHour.Value = UC.WorkStartHour;
					HiddenEndHour.Value = UC.WorkEndHour;
                    GetOwnerName();
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
					FillDropDownRec();

					FillRecType();
					if (Request.Params["mode"] != null)
					{
						if (Request.Params["mode"] == "NEW")
						{
							if (Request.Params["data"] != null)
							{
								F_StartDate.Text = Request.Params["data"];
							}
							if (Request.Params["ora1"] != null)
							{
								F_StartHour.Text = Request.Params["ora1"];
							}

						}
						else
						{
							FillEvent(int.Parse(Request.Params["id"]));
						}
					}



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
				lt.Text =Root.rm.GetString("Meettxt" + (26 + i).ToString());
				RecMode.Items.Add(lt);
			}

			for (i = 1; i < 6; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Meettxt" + (41 + i).ToString());
				RecMonthlyDayPU.Items.Add(lt);
				RecYearDayPU.Items.Add(lt);
			}

			for (i = 8; i < 11; i++)
			{
				ListItem lt = new ListItem();
				lt.Value = i.ToString();
				lt.Text =Root.rm.GetString("Meettxt" + (46 + i - 7).ToString());
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
			RecType.Items.Insert(0,Root.rm.GetString("Meettxt52"));
			RecType.SelectedIndex = 0;
			RecType.Items[0].Value = "0";
			RecType.Items.Insert(1,Root.rm.GetString("Meettxt53"));
			RecType.Items[1].Value = "99";
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
			dsContact = DatabaseConnection.CreateDataset("SELECT *,CONVERT(VARCHAR(10),STARTDATE,105) AS DATA, CONVERT(VARCHAR(5),STARTDATE,114) AS ORAIN,CONVERT(VARCHAR(5),ENDDATE,114) AS ORAFINE FROM BASE_CALENDAR WHERE ID = " + id);
			DataRow drContact = dsContact.Tables[0].Rows[0];
			F_StartDate.Text = drContact["data"].ToString();
			F_StartHour.Text = drContact["orain"].ToString();
			F_EndHour.Text = drContact["orafine"].ToString();
			F_Title.Text = drContact["CONTACT"].ToString();
			F_note.Text = drContact["note"].ToString();
		}

		public void Submit_Click(Object sender, EventArgs e)
		{
			if (Validator())
			{
				string sSql;
				string SqlString2;
				bool all = true;
				DataSet testCoverage;

				DateTime startDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI));
				DateTime endDate = UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_EndHour.Text, UC.myDTFI));

				sSql = "(STARTDATE>'" + startDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND STARTDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE<='" + startDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE>='" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(ENDDATE>'" + startDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<='" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "') OR ";
				sSql += "(STARTDATE>'" + startDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "' AND ENDDATE<'" + endDate.ToString(@"yyyyMMdd HH:mm", InvariantCultureForDB) + "'))";
				sSql += " OR (RECURRID>0))";


				foreach (string li in SelectOffice.GetValueArray)
				{
					if(li.Length>0)
					{
						SqlString2 = "SELECT * FROM BASE_CALENDAR WHERE UID=" + int.Parse(li) + " AND ((" + sSql;

						testCoverage = DatabaseConnection.CreateDataset(SqlString2);
						if (testCoverage.Tables[0].Rows.Count > 0)
						{
							bool free = true;

							DataRow[] drBusy = testCoverage.Tables[0].Select("RECURRID=0");
							if (drBusy.Length > 0)
								free = false;
							else
							{
								foreach (DataRow dr in drBusy)
								{
									Recurrence recurrence = new Recurrence(UC);
									ArrayList AL = recurrence.Remind((int) dr["recurrid"], Convert.ToDateTime(F_StartDate.Text + " 00:00:00", UC.myDTFI), Convert.ToDateTime(F_StartDate.Text + " 23:59:59", UC.myDTFI));
									if (AL.Count > 0)
									{
										DateTime drStart = ((DateTime) AL[0]).AddHours(((DateTime) dr["startdate"]).Hour);
										DateTime drEnd = ((DateTime) AL[0]).AddHours(((DateTime) dr["enddate"]).Hour);
										if ((drStart > startDate && drStart < endDate) ||
											(drStart <= startDate && drEnd >= endDate) ||
											(drEnd > startDate && drEnd <= endDate) ||
											(drStart > startDate && drEnd < endDate))
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

							if (!free)
							{
								all = false;
								SelectOffice.SetSelected(li);
							}
						}

					}

				}
				if (!all)
				{
					Trace.Warn("bloccato");
					Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" +Root.rm.GetString("Meettxt54") + "</span>";
				}
				else
				{
					Trace.Warn("passato");
					ModifyDataSet();
					Response.Redirect("agenda.aspx?m=25&si=2");
				}
			}
			else
			{
				Info.Text = "<center><span style=\"font-family: verdana;font-size: 12px;color: red;\">" +Root.rm.GetString("Meettxt55") + "</span>";
			}
		}

		private bool Validator()
		{
			bool valid = true;
			if (SelectOffice.GetSelectedCount < 1) valid = false;
			if (F_StartDate.Text.Length < 1) valid = false;
			if (F_EndHour.Text.Length < 1) valid = false;
			return valid;
		}

		private void ModifyDataSet()
		{
			foreach (string li in SelectOffice.GetValueArray)
			{
			  if(li.Length>0)
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("UID", li);
					dg.Add("UIDINS", UC.UserId);
					dg.Add("STARTDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_StartHour.Text, UC.myDTFI)));
					dg.Add("ENDDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(F_StartDate.Text + " " + F_EndHour.Text, UC.myDTFI)));
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
					if (CheckRecurrent.Checked)
					{
							dg.Add("RECURRID", NewRecurrence());

					}
					if(li==UC.UserId.ToString())
					{
						dg.Add("CONFIRMATION", 1);
					}

					Message(int.Parse(li), String.Format(Root.rm.GetString("Meettxt56"), F_StartDate.Text, F_StartHour.Text, F_EndHour.Text), F_Title.Text + "<br>" + F_note.Text, F_Title.Text + "<br>" + F_note.Text);
					dg.Execute("BASE_CALENDAR");
				}
			}
		}


		public void Transfer_Listbox(ListBox fromListBox, ListBox toListBox)
		{
			foreach (ListItem li in fromListBox.Items)
			{
				if (li.Selected)
				{
					toListBox.Items.Add(li);
				}
			}
			foreach (ListItem li in toListBox.Items)
			{
				li.Selected = false;
			}
		}

		public void Remove_ListBox(ListBox fromListBox)
		{
			ListBox MyLB = new ListBox();
			foreach (ListItem li in fromListBox.Items)
			{
				if (li.Selected)
				{
					MyLB.Items.Add(li);
				}
			}
			foreach (ListItem li in MyLB.Items)
			{
				fromListBox.Items.Remove(li);
			}
		}

		private int NewRecurrence()
		{
			int newId =-1;
			using (DigiDapter dg = new DigiDapter())
			{
				if (RecDateStart.Text.Length > 0)
				{
					dg.Add("STARTDATE", Convert.ToDateTime(RecDateStart.Text, UC.myDTFI));
				}
				if (RecEndDate.Text.Length > 0)
				{
					dg.Add("ENDDATE", Convert.ToDateTime(RecEndDate.Text, UC.myDTFI));
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
						dg.Add("DESCRIPTION", "Ric.personale Weekle");
						dg.Add("VAR1", RecSettSS.Text);
						int var2 = 0;
						int var3 = 0;
						foreach (ListItem i in RecSetDays.Items)
						{
							if (i.Selected)
							{
								var2 += Convert.ToInt32(i.Value);
								var3++;
							}
						}

						dg.Add("VAR2", var2);
						dg.Add("VAR3", (var3 == 1) ? 0 : 1);

						break;
					case "3":
						dg.Add("DESCRIPTION", "");
						dg.Add("VAR1", RecMonthlyDays.Text);
						dg.Add("VAR2", RecMonthlyMonths.Text);
						break;
					case "4":
						dg.Add("DESCRIPTION", "");
						dg.Add("VAR1", RecMonthlyDayPU.SelectedItem.Value);
						dg.Add("VAR2", RecMonthlyDayDays.SelectedItem.Value);
						dg.Add("VAR2", RecMonthlyDayMonths.Text);
						break;
					case "5":
						dg.Add("DESCRIPTION", "");
						dg.Add("VAR1", RecYearDays.Text);
						dg.Add("VAR2", RecYearMonths.SelectedItem.Value);
						break;
					case "6":
						dg.Add("DESCRIPTION", "");
						dg.Add("VAR1", RecYearDayPU.SelectedItem.Value);
						dg.Add("VAR2", RecYearDayDays.SelectedItem.Value);
						dg.Add("VAR3", RecYearDayMonths.SelectedItem.Value);
						break;
				}
				newId=Convert.ToInt32(dg.Execute("RECURRENCE",DigiDapter.Identities.Identity));
			}

			return newId;
		}
	}
}
