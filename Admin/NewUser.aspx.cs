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
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Win32;

namespace Digita.Tustena
{
	public partial class NewUser : G
	{

		public DataSet groups;

		private bool freeUsers;
		private int freeAccount = 0;

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (visMail.Visible == true && !M.IsModule(ActiveModules.Lead))
                Tabber.HideTabs += visMail.ID;

            base.OnPreRenderComplete(e);
        }


		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				F_UserNameValidator.ErrorMessage =Root.rm.GetString("Usrtxt21");
				RegularExpressionValidator1.ErrorMessage =Root.rm.GetString("Usrtxt21");
				F_PasswordValidator.ErrorMessage =Root.rm.GetString("Usrtxt22");
				F_GroupValidator.ErrorMessage =Root.rm.GetString("Usrtxt26");
				valSum.HeaderText=Root.rm.GetString("ValidSummary");
				SecureMail.RepeatDirection=RepeatDirection.Horizontal;
				SecureMail.RepeatColumns=2;

				trZone.Visible=true;

				DeleteGoBack();

				int activeAccount;
				activeAccount = (int) DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM ACCOUNT WHERE ACTIVE=1");
				freeAccount = ((int) DatabaseConnection.SqlScalartoObj("SELECT TOP 1 MAXUSER FROM TUSTENA_DATA") - activeAccount);

				if (freeAccount > 0)
				{
					LtrNewUser.Text = "<span class=\"ContactHeader\">" +Root.rm.GetString("Usrtxt54") + "&nbsp;" + freeAccount.ToString() + "</span>";
					LblNewUser.Text = StaticFunctions.Capitalize(Root.rm.GetString("Usrtxt10"));
					freeUsers = true;
				}
				else
				{
					LtrNewUser.Text = "<span class=\"normal\">" + StaticFunctions.Capitalize(Root.rm.GetString("Usrtxt55")) + "</span>";
					LblNewUser.Text = StaticFunctions.Capitalize(Root.rm.GetString("Usrtxt10"));
					freeUsers = false;
				}

				if (!Page.IsPostBack)
				{
					valSum.Enabled=false;
					Tabber.Visible = false;
					HelpLabel.Text = FillHelp("HelpUser");
					TitlePage.Text =Root.rm.GetString("Usrtxt9");
					TitlePageRight.Text =Root.rm.GetString("Usrtxt9");
					BtnFind.Text =Root.rm.GetString("Usrtxt8");
					Rubric2.Text =Root.rm.GetString("Usrtxt18");
					ExistRubric2.Text =Root.rm.GetString("Usrtxt19");
					Submit2.Text =Root.rm.GetString("Usrtxt20");

					FlagCommercial.Items.Add(new ListItem(Root.rm.GetString("Usrtxt82")));
					FlagCommercial.Items.Add(new ListItem(Root.rm.GetString("Usrtxt83")));
					FlagCommercial.Items.Add(new ListItem(Root.rm.GetString("Usrtxt84")));
					FlagCommercial.Attributes.Add("onclick","EnableZones();");

					int itemValue = 1;

					for (int liv = 0; liv < 7; liv++)
					{
						ListItem li = new ListItem();
						li.Value = itemValue.ToString();
						li.Text =Root.rm.GetString("Usrtxt" + (31 + liv).ToString());
						RecurrenceWeeklyDays.Items.Add(li);
						itemValue = itemValue*2;
					}

					FillListGroups(UC,ListGroups);

                    FillPriceList();

				}
				FillHeader();
				if (!Page.IsPostBack)
				{
					F_WorkStart.Attributes.Add("onkeyup", "HourCheck(this,event)");
					EndWork.Attributes.Add("onkeyup", "HourCheck(this,event)");


					if (Request.Params["List"] != null)
					{
						FillRepeater(Request.Params["List"].ToString());
					}
					if (Request.Params["Del"] != null)
					{
						Delete(int.Parse(Request.Params["Del"].ToString()));
					}
					if (Request.Params["action"] != null)
					{
						if (Request.Params["action"] == "NEW")
						{
							PrepareForNewAccount();
						}
						if (Request.Params["action"] == "MOD")
						{
							valSum.Enabled=true;
							Tabber.Visible = true;
							F_UID.Text = Request.Params["full"];
							if (!Page.IsPostBack)
							{
								FillDropDown();
								FillForm(int.Parse(F_UID.Text).ToString());
							}
						}
					}

					if (Request.QueryString["mo"] == "1" || Request.QueryString["si"] == "24")
					{
						ModUserAccount();
						HelpLabel.Visible=true;
					}
					else
					{
						HelpLabel.Visible=false;
						Session.Remove("mo");
					}
				}
				else
				{
					try
					{
						if (Request.QueryString["mo"] == "1" || Session["mo"].ToString() == "1")
						{
							Session["mo"] = "1";
							LblHeader.Visible = false;
							Find.Visible = false;
							LtrNewUser.Text = String.Empty;
							LblNewUser.Text = String.Empty;
						}
						else
						{
							Session.Remove("mo");
						}
					}
					catch
					{
					}

					if (F_Password.Text.Length > 0)
						F_Password.Attributes.Add("value", F_Password.Text);
				}
			}
		}

        private void FillPriceList()
        {
            PriceList.DataValueField = "ID";
            PriceList.DataTextField = "DESCRIPTION";

            PriceList.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CATALOGPRICELISTDESCRIPTION").Tables[0];
            PriceList.DataBind();
            PriceList.Items.Insert(0, new ListItem(Root.rm.GetString("Captxt38"), ""));
            PriceList.Items.Insert(1, new ListItem(Root.rm.GetString("Captxt37"), "0"));
        }

		private void FillRepeaterZones(string zones)
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM ZONES").Tables[0];
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (zones.IndexOf("|" + dr["id"].ToString() + "|") > -1)
						dr["tocheck"] = "1";
				}
			}
        	RepeaterZones.DataSource=dt;
			RepeaterZones.DataBind();
		}

		private void PrepareForNewAccount()
		{
			LblHeader.Visible = false;
			LabelInfo.Text = String.Empty;
			Mode.Text = "NEW";
			FillDropDown();
			FillOffices();
			F_WorkStart.Text = new DateTime(2000, 1, 1, 9, 0, 0).ToShortTimeString();
			EndWork.Text = new DateTime(2000, 1, 1, 13, 0, 0).ToShortTimeString();
			F_WorkStart2.Text = new DateTime(2000, 1, 1, 14, 0, 0).ToShortTimeString();
			EndWork2.Text = new DateTime(2000, 1, 1, 18, 0, 0).ToShortTimeString();
			foreach (ListItem li in RecurrenceWeeklyDays.Items)
			{
				li.Selected = true;
			}

			FillRepGroups("");
			valSum.Enabled=true;
			Tabber.Visible = true;

			F_UserName.Text=string.Empty;
			F_Password.Text=string.Empty;
			F_Password.Attributes.Add("value", string.Empty);
			F_Group.SelectedIndex=0;
			EMail.Text=string.Empty;
			Name.Text=string.Empty;
			Surname.Text=string.Empty;
			F_Officeso.SelectedIndex=0;
			Paging.Text="20";
			SessionTimeout.Text="20";

			string adminTimeZone = DatabaseConnection.SqlScalar("SELECT TIMEZONEINDEX FROM ACCOUNT WHERE UID="+UC.UserId);
			foreach(ListItem li in DropCulture.Items)
			{
				if(li.Value==adminTimeZone)
				{
					li.Selected=true;
					break;
				}
			}
			FillRepGroups(string.Empty);
			FlagCommercial.SelectedIndex=0;
			FillRepeaterZones(string.Empty);

            Repeater1.Visible = false;
            Tabber.Visible = true;
		}

		private void SetRssGiud()
		{
			bool ws = false;
			if (CSSGuid.Text.Length == 0)
			{
				Random R = new Random();
				CSSGuid.Text = R.Next(30000).ToString();
				ws = true;
			}
			if (LblCSSGuid.Text.Length == 0)
			{
				LblCSSGuid.Text = Guid.NewGuid().ToString();
				ws = true;
			}
			string wsGuid = DatabaseConnection.FilterInjection(CSSGuid.Text);
			string wsPin = DatabaseConnection.FilterInjection(LblCSSGuid.Text);
			if (ws) DatabaseConnection.DoCommand(String.Format("UPDATE ACCOUNT SET RSSGUID='{0}',RSSPIN='{1}'", wsGuid, wsPin));
		}


		private void ModUserAccount()
		{
			tblAdminUser.Visible=false;
			Session["mo"] = "1";
			FillDropDown();
			FillForm(UC.UserId.ToString());
			LblHeader.Visible = false;
			Find.Visible = false;
			BtnFind.Visible = false;
			LtrNewUser.Text = String.Empty;
			LblNewUser.Text = String.Empty;
			F_UserName.Enabled = false;
			PersonalContact.Enabled = false;
			Name.Enabled = false;
			Surname.Enabled = false;
			F_Officeso.Enabled = false;
			F_WorkStart.Enabled = false;
			F_WorkStart2.Enabled = false;
			EndWork.Enabled = false;
			EndWork2.Enabled = false;
			RecurrenceWeeklyDays.Enabled = false;
			F_Group.Enabled = false;
			Responsable.Enabled = false;
			Subaltern.Enabled = false;
			OfficesAll.Visible = false;

			OfficesSel.Visible = false;
			valSum.Enabled=true;
			Tabber.Visible = true;
			TROfficeTitle.Visible = false;
			TROfficeFields.Visible = false;
			BtnStartWork.InnerHtml = "&nbsp;&nbsp;";
			BtnEndWork.InnerHtml = "&nbsp;&nbsp;";
			BtnStartWork2.InnerHtml = "&nbsp;&nbsp;";
			BtnEndWork2.InnerHtml = "&nbsp;&nbsp;";
			TitlePage.Text =Root.rm.GetString("Usrtxt53");
			TitlePageRight.Text =Root.rm.GetString("Usrtxt53");
			SecondaryGroup.Visible = false;
			FlagCommercial.Enabled=false;
			trZone.Visible=false;
            PriceList.Enabled = false;
		}

		private void FillDropDown()
		{
			string[] arryD = UC.GroupDependency.Split('|');
			string query = String.Empty;
			foreach (string ut in arryD)
			{
				query += "ID=" + ut + " OR ";
			}
			query = query.Substring(7, query.Length - 17);
			groups = DatabaseConnection.CreateDataset("SELECT ID, DESCRIPTION FROM GROUPS WHERE " + query);
			F_Group.DataSource = groups;
			F_Group.DataTextField = "Description";
			F_Group.DataValueField = "ID";
			F_Group.DataBind();
			F_Group.Items.Insert(0, new ListItem(Root.rm.GetString("Usrtxt6"), "-1"));
			F_Group.SelectedIndex = 0;

			DataSet responsableDs;
			responsableDs = DatabaseConnection.CreateDataset("SELECT UID, NAME + ' ' + SURNAME AS DESCRIPTION FROM ACCOUNT WHERE ISMANAGER=1");
			IdResponsable.DataSource = responsableDs;
			IdResponsable.DataTextField = "Description";
			IdResponsable.DataValueField = "UID";
			IdResponsable.DataBind();
			IdResponsable.Items.Insert(0,Root.rm.GetString("Usrtxt7"));
			IdResponsable.SelectedIndex = 0;
			IdResponsable.Items[0].Value = "0";

			DataSet DsManager;
			DsManager = DatabaseConnection.CreateDataset("SELECT UID, NAME + ' ' + SURNAME AS DESCRIPTION FROM ACCOUNT WHERE ACCESSLEVEL=1");
			IdManager.DataSource = DsManager;
			IdManager.DataTextField = "Description";
			IdManager.DataValueField = "UID";
			IdManager.DataBind();
			IdManager.Items.Insert(0,Root.rm.GetString("Usrtxt7"));
			IdManager.SelectedIndex = 0;
			IdManager.Items[0].Value = "0";


			F_Officeso.DataSource = DatabaseConnection.CreateDataset("SELECT * FROM OFFICES ORDER BY OFFICE ASC");
			F_Officeso.DataTextField = "Office";
			F_Officeso.DataValueField = "id";
			F_Officeso.DataBind();
			F_Officeso.Items.Insert(0,Root.rm.GetString("Usrtxt5"));
			F_Officeso.SelectedIndex = 0;
			F_Officeso.Items[0].Value = "0";

			Win32TimeZone[] tz = TimeZones.GetTimeZones();
			TimeZoneNew.Items.Clear();
			foreach (Win32TimeZone x in tz)
			{
				ListItem li = new ListItem(x.DisplayName, x.Index.ToString());
				if(li.Value==UC.Culture)
					li.Selected=true;
				TimeZoneNew.Items.Add(li);
			}
			TimeZoneNew.Items.Insert(0,new ListItem(Root.rm.GetString("Choose")));


			string[] items = ConfigSettings.SupportedLanguagesDescription.Split(';');
			foreach(string i in items)
			{
				DropCulture.Items.Add(new ListItem(i.Split('|')[0], i.Split('|')[1]));
			}


			DropFirstDay.Items.Add(new ListItem(Root.rm.GetString("Caltxt18"), "1"));
			DropFirstDay.Items.Add(new ListItem(Root.rm.GetString("Caltxt24"), "0"));
		}

		private void FillHeader()
		{
			LblHeader.Text = ABCHeaderHtml(Request.Params["List"]);

		}


		private void FillRepeater(string query)
		{
			FillRepeater(query, "");
		}

		private void FillRepeater(string query, string group)
		{
			LabelInfo.Text = String.Empty;
			string[] arryD = UC.GroupDependency.Split('|');

			if (query == "*")
			{
				Repeater1.DataSource = DatabaseConnection.CreateDataset("SELECT ACCOUNT.*,OFFICES.OFFICE AS OFFICESON FROM ACCOUNT LEFT OUTER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID");
			}
			else
			{
				if (group.Length > 0)
				{
					Repeater1.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT ACCOUNT.*,OFFICES.OFFICE AS OFFICESON FROM ACCOUNT LEFT OUTER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE (USERACCOUNT LIKE '{0}%' OR NAME LIKE '{0}%' OR SURNAME LIKE '{0}%' OR OFFICES.OFFICE LIKE '{0}%') AND GROUPID={2} ORDER BY SURNAME ASC", query, group));
				}
				else
				{
					Repeater1.DataSource = DatabaseConnection.CreateDataset(String.Format("SELECT ACCOUNT.*,OFFICES.OFFICE AS OFFICESON FROM ACCOUNT LEFT OUTER JOIN OFFICES ON ACCOUNT.OFFICEID=OFFICES.ID WHERE (USERACCOUNT LIKE '{0}%' OR NAME LIKE '{0}%' OR SURNAME LIKE '{0}%' OR OFFICES.OFFICE LIKE '{0}%') ORDER BY SURNAME ASC", query));
				}
			}
			Repeater1.DataBind();
			if (Repeater1.Items.Count > 0)
			{
				Repeater1Info.Visible = false;
				Repeater1.Visible = true;
			}
			else
			{
				Repeater1Info.Text =Root.rm.GetString("Usrtxt81");
				Repeater1Info.Visible = true;
				Repeater1.Visible = false;
			}
			valSum.Enabled=false;
			Tabber.Visible = false;
		}

		public void Submit_Click(Object sender, EventArgs e)
		{
			if (Mode.Text == "NEW")
			{
				LblHeader.Visible = false;
                if (!ModifyDataSet(-1)) return;

				LabelInfo.Text =Root.rm.GetString("Usrtxt4");
				valSum.Enabled=false;
				Tabber.Visible = false;

				int activeAccount;
				activeAccount = (int) DatabaseConnection.SqlScalartoObj("SELECT COUNT(*) FROM ACCOUNT WHERE ACTIVE=1");
				freeAccount = ((int) DatabaseConnection.SqlScalartoObj("SELECT TOP 1 MAXUSER FROM TUSTENA_DATA") - activeAccount);

				if (freeAccount > 0)
				{
					LtrNewUser.Text = "<span class=\"ContactHeader\">" +Root.rm.GetString("Usrtxt54") + "&nbsp;" + freeAccount.ToString() + "</span>";
					LblNewUser.Text =Root.rm.GetString("Usrtxt10");
				}
				else
				{
					LtrNewUser.Text = "<span class=\"normal\"><b>" +Root.rm.GetString("Usrtxt55") + "</b></span>";
					LblNewUser.Text =Root.rm.GetString("Usrtxt10");
				}

				Mode.Text = "MOD";
			}
			else
			{
                if (!ModifyDataSet(int.Parse(F_UID.Text))) return;

				LabelInfo.Text =Root.rm.GetString("Usrtxt3");
				valSum.Enabled=false;
				Tabber.Visible = false;
			}

		}

		private void FillForm(string id)
		{
			DataSet dsUser;
			if (id == "-1")
			{
				dsUser = DatabaseConnection.CreateDataset("SELECT TOP 1 * FROM ACCOUNT ORDER BY UID DESC");
			}
			else
			{
				dsUser = DatabaseConnection.CreateDataset("SELECT * FROM ACCOUNT WHERE UID = " + id);
			}

			if (dsUser.Tables[0].Rows.Count == 0)
				HackLock(UC.UserId + ">" + id);
			DataRow drUser = dsUser.Tables[0].Rows[0];
			F_UID.Text = drUser["UID"].ToString();
			Name.Text = drUser["Name"].ToString();
			Surname.Text = drUser["Surname"].ToString();
			F_WorkStart.Text = Convert.ToDateTime(drUser["WorkStart_1"]).ToString("HH':'mm");
			EndWork.Text = Convert.ToDateTime(drUser["WorkEnd_1"]).ToString("HH':'mm");
			F_WorkStart2.Text = Convert.ToDateTime(drUser["WorkStart_2"]).ToString("HH':'mm");
			EndWork2.Text = Convert.ToDateTime(drUser["WorkEnd_2"]).ToString("HH':'mm");
			F_UserName.Text = drUser["UserAccount"].ToString();
			F_Password.Attributes.Add("value", drUser["Password"].ToString());
			EMail.Text = drUser["NotifyEmail"].ToString();
			F_IdRubrica.Text = drUser["SelfContactID"].ToString();
            if(F_IdRubrica.Text.Length>0 && int.Parse(F_IdRubrica.Text)>0)
                ViewF_IdRubrica.Text = DatabaseConnection.SqlScalar("select isnull(name,'')+' '+isnull(surname,'') from BASE_Contacts where id=" + F_IdRubrica.Text);

			Responsable.Checked = Convert.ToBoolean(drUser["IsManager"]);
			Subaltern.Checked = Convert.ToBoolean(drUser["IsEmployee"]);
			PersonalContact.Checked = Convert.ToBoolean(drUser["EnablePersContact"]);
			FullScreen.Checked = Convert.ToBoolean(drUser["FullScreen"]);
			ViewBirthDate.Checked = Convert.ToBoolean(drUser["ViewBirthDate"]);
			Paging.Text = drUser["Paging"].ToString();
			SessionTimeout.Text = drUser["SessionTimeout"].ToString();

			FlagCommercial.Items[(int)drUser["accesslevel"]].Selected=true;
			if(drUser["MailServer"].ToString().StartsWith("!"))
			{
				MailServer.Text = drUser["MailServer"].ToString().Substring(1);
				SecureMail.Items[1].Selected=true;
			}
			else
			{
				MailServer.Text = drUser["MailServer"].ToString();
				SecureMail.Items[0].Selected=true;
			}
			MailUser.Text = drUser["MailUser"].ToString();
			MailPassword.Attributes.Add("value", drUser["MailPassword"].ToString());

			IdManager.SelectedItem.Selected = false;
			foreach (ListItem li in IdManager.Items)
			{
				if (li.Value == drUser["ManagerID"].ToString())
				{
					li.Selected = true;
				}
			}
			F_Group.SelectedItem.Selected = false;
			foreach (ListItem li in F_Group.Items)
			{
				if (li.Value == drUser["GroupID"].ToString())
				{
					li.Selected = true;
				}
			}
			F_Officeso.SelectedItem.Selected = false;
			foreach (ListItem li in F_Officeso.Items)
			{
				if (li.Value == drUser["OfficeID"].ToString())
				{
					li.Selected = true;
				}
			}

			try
			{
				if (drUser["InsertGroups"].ToString().Length > 0)
					GroupControl.SetGroups(drUser["InsertGroups"].ToString());
				else
					GroupControl.SetGroups("|" + drUser["GroupID"].ToString() + "|");
			}
			catch
			{
				GroupControl.SetGroups("|" + drUser["GroupID"].ToString() + "|");
			}

			DropCulture.SelectedItem.Selected = false;
			foreach (ListItem li in DropCulture.Items)
			{
				if (li.Value.Substring(0, 2) == drUser["Culture"].ToString().Substring(0, 2))
				{
					li.Selected = true;
				}
			}

			DropFirstDay.SelectedItem.Selected = false;
			if ((bool) drUser["FirstDayOfWeek"])
				DropFirstDay.SelectedIndex = 1;
			else
				DropFirstDay.SelectedIndex = 0;

			TimeZoneNew.SelectedItem.Selected = false;
			foreach (ListItem li in TimeZoneNew.Items)
			{
				if (li.Value == drUser["TimeZoneIndex"].ToString())
				{
					li.Selected = true;
				}
			}

            PriceList.SelectedIndex = -1;
            foreach (ListItem li in PriceList.Items)
            {
                if (li.Value == drUser["ListPrice"].ToString())
                {
                    li.Selected = true;
                }
            }


			RecurrenceWeeklyDays.SelectedIndex = -1;
			for (int i = 0; i < 7; i++)
				if ((Convert.ToInt32(drUser["WorkDays"]) & (0x01 << i)) != 0)
				{
					RecurrenceWeeklyDays.Items[i].Selected = true;
				}

			F_IdRubrica.Text = drUser["SelfContactID"].ToString();
            if (F_IdRubrica.Text.Length > 0 && int.Parse(F_IdRubrica.Text) > 0)
                ViewF_IdRubrica.Text = DatabaseConnection.SqlScalar("select isnull(name,'')+' '+isnull(surname,'') from BASE_Contacts where id=" + F_IdRubrica.Text);

			if (drUser["DiaryAccount"].ToString().Length > 1)
			{
				Office.SetAccount(drUser["DiaryAccount"].ToString());
			}

			if (drUser["OfficeAccount"].ToString().Length > 1)
			{
				string[] arryUff = drUser["OfficeAccount"].ToString().Split('|');
				string query = String.Empty;
				foreach (string ut in arryUff)
				{
					query += "ID=" + ut + " OR ";
				}
				query = query.Substring(6, query.Length - 17);
				OfficesSel.DataTextField = "Description";
				OfficesSel.DataValueField = "id";
				StringBuilder sqlString = new StringBuilder();
				sqlString.Append("SELECT ID, OFFICE AS DESCRIPTION ");
				sqlString.AppendFormat("FROM OFFICES WHERE {0} ORDER BY OFFICE", query);
				OfficesSel.DataSource = DatabaseConnection.CreateDataset(sqlString.ToString());
				OfficesSel.DataBind();
			}
			FillOffices();
			FillRepGroups(drUser["OtherGroups"].ToString());
			FillRepeaterZones(drUser["Zones"].ToString());
			DataSet dsTitles;
			if (mode == "0" && !UC.DebugMode)
			{
				dsTitles = DatabaseConnection.CreateDataset("SELECT *, MENUMAP.NEWHOMEPAGEID FROM TUSTENAMENU_VIEW LEFT OUTER JOIN MENUMAP ON MENUMAP.MENUID=TUSTENAMENU_VIEW.ID AND (MENUMAP.USERID=" + id + ") WHERE TUSTENAMENU_VIEW.ID<>12 AND (MENUTITLE=1" + QuerySecurityGroups(int.Parse(this.F_Group.SelectedValue)) + ") AND (MODE=" + mode + ") AND ("+(int)UC.Modules+"&MODULES)=MODULES ORDER BY SORTORDER");
            }
			else
			{
				dsTitles = DatabaseConnection.CreateDataset("SELECT *, MENUMAP.NEWHOMEPAGEID FROM TUSTENAMENU_VIEW LEFT OUTER JOIN MENUMAP ON MENUMAP.MENUID=TUSTENAMENU_VIEW.ID AND (MENUMAP.USERID=" + id + ") WHERE TUSTENAMENU_VIEW.ID<>12 AND (MENUTITLE=1" + QuerySecurityGroups(int.Parse(this.F_Group.SelectedValue)) + ") AND ("+(int)UC.Modules+"&MODULES)=MODULES ORDER BY SORTORDER");
            }
			this.RepHomePage.DataSource = dsTitles;
			this.RepHomePage.DataBind();
		}

		public void CreateVoice_Click(Object sender, EventArgs e)
		{
			if ((F_IdRubrica.Text != null && F_IdRubrica.Text.Length == 0) || F_IdRubrica.Text == "-1")
			{
				Response.Redirect("Rubrica.aspx?action=NEW");
			}
			else
			{
				Response.Redirect("Rubrica.aspx?action=MOD&full=" + F_IdRubrica.Text + "&user=" + F_UID.Text);
			}
		}

		private bool ModifyDataSet(int id)
		{
            if (id == -1)
            {
               if(Convert.ToInt32(DatabaseConnection.SqlScalar(string.Format("select count(*) from account where useraccount='{0}'",DatabaseConnection.FilterInjection(F_UserName.Text))))>0)
               {
                   ClientScript.RegisterClientScriptBlock(this.GetType(),"userduplicate","<script>alert('"+Root.rm.GetString("Usrtxt85")+"');</script>");
                   return false;
               }
            }
			bool isNew = false;
			using (DigiDapter dg = new DigiDapter())
			{
				if (freeAccount > 0)
					dg.Add("ACTIVE", 1, 'I');
				else
					dg.Add("ACTIVE", 0, 'I');


				dg.Add("NAME", Name.Text);
				dg.Add("SURNAME", Surname.Text);
				dg.Add("NOTIFYEMAIL", EMail.Text);

				dg.Add("WORKSTART_1", Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + F_WorkStart.Text));

				dg.Add("WORKEND_1", Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + EndWork.Text));

				dg.Add("WORKSTART_2", Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + F_WorkStart2.Text));

				dg.Add("WORKEND_2", Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + EndWork2.Text));


				if (PersonalContact.Checked)
					dg.Add("ENABLEPERSCONTACT", 1);
				if (FlagModifyAppointment.Checked)
					dg.Add("FLAGNOTIFYAPPOINTMENT", 1);

				dg.Add("ACCESSLEVEL", FlagCommercial.SelectedIndex);


				int gglav = 0;
				foreach (ListItem li in RecurrenceWeeklyDays.Items)
				{
					if (li.Selected) gglav += Convert.ToInt32(li.Value);
				}
				dg.Add("WORKDAYS", gglav);
				dg.Add("USERACCOUNT", F_UserName.Text);
				dg.Add("PASSWORD", F_Password.Text);

				if (F_IdRubrica.Text.Length == 0)
				{
					F_IdRubrica.Text = "-1";
				}
				if (Paging.Text.Length > 0) dg.Add("PAGING", Paging.Text);
				if (SessionTimeout.Text.Length > 0) dg.Add("SESSIONTIMEOUT", SessionTimeout.Text);

                if (FlagCreateContact.Checked && F_IdRubrica.Text == "-1")
                {
                    object newId;
                    using (DigiDapter dgcontact = new DigiDapter())
                    {
                        dgcontact.Add("OWNERID", UC.UserId);
                        dgcontact.Add("CREATEDBYID", UC.UserId);
                        dgcontact.Add("CREATEDDATE", DateTime.UtcNow);
                        dgcontact.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                        dgcontact.Add("LASTMODIFIEDBYID", UC.UserId);
                        dgcontact.Add("NAME", Name.Text);
                        dgcontact.Add("SURNAME", Surname.Text);
                        dgcontact.Add("EMAIL", F_UserName.Text);
                        dgcontact.Add("GROUPS", "|" + UC.UserGroupId + "|");

                        newId = dgcontact.Execute("BASE_CONTACTS", "ID=-1", DigiDapter.Identities.Identity);
                        F_IdRubrica.Text = newId.ToString();
                    }
                }
				dg.Add("SELFCONTACTID", Convert.ToInt32(F_IdRubrica.Text));
				dg.Add("ISMANAGER", Responsable.Checked);
				dg.Add("ISEMPLOYEE", Subaltern.Checked);
				dg.Add("FULLSCREEN", FullScreen.Checked);
				dg.Add("VIEWBIRTHDATE", ViewBirthDate.Checked);
				if(IdManager.SelectedIndex>0)
					dg.Add("MANAGERID", IdManager.SelectedItem.Value);
				else
					dg.Add("MANAGERID", 0);
				dg.Add("GROUPID", F_Group.SelectedItem.Value);

				string cat = "|";
				foreach (RepeaterItem it in RepGroup.Items)
				{
					CheckBox Check = (CheckBox) it.FindControl("Check");
					if (Check.Checked)
						cat += ((Literal) it.FindControl("IDGr")).Text + "|";
				}
				if (cat.Length > 1)
					dg.Add("OTHERGROUPS", cat);
				else
					dg.Add("OTHERGROUPS", DBNull.Value);

				dg.Add("INSERTGROUPS", GroupControl.GetValue);
				dg.Add("OFFICEID", F_Officeso.SelectedItem.Value);

				if(TimeZoneNew.SelectedIndex>0)
					dg.Add("TIMEZONEINDEX", TimeZoneNew.SelectedItem.Value);
				else
				{
					dg.Add("TIMEZONEINDEX",Convert.ToInt32(DatabaseConnection.SqlScalar("SELECT TIMEZONEINDEX FROM ACCOUNT WHERE UID=" + UC.UserId)));
				}


				dg.Add("CULTURE", DropCulture.SelectedItem.Value);
				dg.Add("FIRSTDAYOFWEEK", (DropFirstDay.SelectedItem.Value == "0"));

				if(SecureMail.Items[1].Selected)
					dg.Add("MAILSERVER", "!"+MailServer.Text);
				else
					dg.Add("MAILSERVER", MailServer.Text);
				dg.Add("MAILUSER", MailUser.Text);
				if (MailPassword.Text.Length > 0)
					dg.Add("MAILPASSWORD", MailPassword.Text);
				else
					dg.Add("MAILPASSWORD", DBNull.Value);


				dg.Add("DIARYACCOUNT", Office.GetValue);

				dg.Add("OFFICEACCOUNT", OfficeSelText.Text);

				string arryZone = String.Empty;
				for (int i = 0; i < RepeaterZones.Items.Count; i++)
				{
					bool isChecked = ((CheckBox) RepeaterZones.Items[i].FindControl("Checkbox1")).Checked;
					if (isChecked)
					{
						arryZone+="|"+((Literal) (RepeaterZones.Items[i].FindControl("id"))).Text;
					}
				}
				if(arryZone.Length>0)
						dg.Add("ZONES",arryZone+"|");
                else
                        dg.Add("ZONES", System.DBNull.Value);

                if(PriceList.SelectedIndex>0)
                    dg.Add("LISTPRICE", PriceList.SelectedValue);
                else
                    dg.Add("LISTPRICE", System.DBNull.Value);

				dg.Execute("Account", "UID=" + id);
				isNew = dg.RecordInserted;
			}
			if (!isNew)
			{
				foreach (RepeaterItem it in RepHomePage.Items)
				{
					using (DigiDapter dgp = new DigiDapter())
					{
						DropDownList DropMenuDefault = (DropDownList) it.FindControl("DropMenuDefault");
						Literal menuid = (Literal) it.FindControl("MenuID");
						if (DropMenuDefault.SelectedIndex == 0)
						{
							dgp.Add("UserID", id);
							dgp.Add("MenuID", menuid.Text);
							dgp.Add("FirstTime", 0);
							dgp.Add("NewHomePage", DBNull.Value);
							dgp.Add("NewHomePageID", DBNull.Value);
						}
						else
						{
							if (menuid.Text == "25" && DropMenuDefault.SelectedValue == "2")
							{
								dgp.Add("UserID", id);
								dgp.Add("MenuID", 25);
								dgp.Add("FirstTime", 1);
								dgp.Add("NewHomePage", "/Calendar/agenda.aspx?m=25&si=2");
								dgp.Add("NewHomePageID", 2);
							}
							else
							{
								DataRow dr = DatabaseConnection.CreateDataset("SELECT ID,'/'+folder+'/'+link+'&si='+cAST(ID AS VARCHAR) AS PAGEDEFAULT FROM NEWMENU WHERE ID=" + int.Parse(DropMenuDefault.SelectedValue)).Tables[0].Rows[0];
								dgp.Add("UserID", id);
								dgp.Add("MenuID", menuid.Text);
								dgp.Add("FirstTime", 1);
								dgp.Add("NewHomePage", dr["pagedefault"].ToString());
								dgp.Add("NewHomePageID", (int) dr["id"]);
							}
						}
						dgp.Execute("MENUMAP", "USERID=" + id + " AND MENUID=" + menuid.Text);
					}
				}

			}

			if (id == Convert.ToInt32(UC.UserId))
			{
				UserConfig UCupload;
				UCupload = UserData.LoadPersonalData(F_UserName.Text, F_Password.Text, -1);
				Session["UserConfig"] = UCupload;
			}
            return true;
		}

		public void BtnFindClick(Object sender, EventArgs e)
		{
			if (ListGroups.SelectedIndex > 0)
				FillRepeater(DatabaseConnection.FilterInjection(this.Find.Text), ListGroups.SelectedValue);
			else
				FillRepeater(DatabaseConnection.FilterInjection(Find.Text));
		}

		private void Delete(int id)
		{
			object retActive = DatabaseConnection.SqlScalartoObj(String.Format("SELECT ACTIVE FROM ACCOUNT WHERE UID ={0}", id));
			if (retActive != null)
				DatabaseConnection.DoCommand(String.Format("UPDATE ACCOUNT SET ACTIVE={1} WHERE UID={0}", id, (((int)retActive) == 0) ? 1 : 0));
			if (Request.Params["List"] != null)
				FillRepeater(Request.Params["List"].ToString());
			else
				FillRepeater("*");

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
			toListBox.SelectedItem.Selected = false;
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




		public void LblNewUser_Click(Object sender, EventArgs e)
		{
			PrepareForNewAccount();
		}

		private void FillOffices()
		{
			OfficesAll.DataTextField = "Office";
			OfficesAll.DataValueField = "id";
			OfficesAll.DataSource = DatabaseConnection.CreateDataset("SELECT ID, OFFICE FROM OFFICES ORDER BY OFFICE ASC");
			OfficesAll.DataBind();
		}

		public void Repeater1DataBound(object source, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Label td1 = (Label) e.Item.FindControl("td1");
					td1.Attributes.Add("onclick", "location.href='NewUser.aspx?m=7&si=8&action=MOD&full=" + Convert.ToString(DataBinder.Eval((DataRowView) e.Item.DataItem, "UID")) + "'");
					td1.Text = (string) DataBinder.Eval((DataRowView) e.Item.DataItem, "UserAccount");
					Label td2 = (Label) e.Item.FindControl("td2");
					td2.Text = (string) DataBinder.Eval((DataRowView) e.Item.DataItem, "Surname") + "&nbsp;" + (string) DataBinder.Eval((DataRowView) e.Item.DataItem, "Name");

					LinkButton DelAC = (LinkButton) e.Item.FindControl("Delete");
					if (Convert.ToBoolean(DataBinder.Eval((DataRowView) e.Item.DataItem, "Active")))
					{
						td1.CssClass = "normal linked";
						td2.CssClass = "normal";
						DelAC.Text =Root.rm.GetString("Usrtxt15");
						DelAC.Attributes.Add("onclick", "return confirm('" + ParseJSString(Root.rm.GetString("Usrtxt59")) + "');");
					}
					else
					{
						td1.CssClass = "LinethroughGray linked";
						td2.CssClass = "LinethroughGray";
						if (freeUsers)
						{
							DelAC.Text =Root.rm.GetString("Usrtxt63");
							DelAC.Attributes.Add("onclick", "return confirm('" + ParseJSString(Root.rm.GetString("Usrtxt64")) + "');");
						}
						else
							DelAC.Visible = false;
					}
					break;
			}
		}

		public void Repeater1Command(Object sender, RepeaterCommandEventArgs e)
		{
			Trace.Warn("commandname", e.CommandName);
			switch (e.CommandName)
			{
				case "Delete":
					Literal IDUID = (Literal) e.Item.FindControl("IDUID");
					Trace.Warn("deleteid", IDUID.Text);
					Delete(int.Parse(IDUID.Text));
					break;
			}
		}

		private void FillRepGroups(string dependency)
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM GROUPS").Tables[0];
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					if (dependency.IndexOf("|" + dr["id"].ToString() + "|") > -1)
						dr["tocheck"] = "1";

					if (dr["id"].ToString() == F_Group.SelectedValue)
					{
						dr["tocheck"] = "2";
					}


				}
			}
			RepGroup.DataSource = new DataView(dt, "", "tocheck desc", DataViewRowState.CurrentRows);
			RepGroup.DataBind();
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.RepHomePage.ItemDataBound += new RepeaterItemEventHandler(RepHomePage_ItemDataBound);
			this.RepGroup.ItemDataBound += new RepeaterItemEventHandler(RepGroup_ItemDataBound);
			this.Rubric2.Click += new EventHandler(this.CreateVoice_Click);
			this.LblNewUser.Click+=	 new EventHandler(this.LblNewUser_Click);
			this.RepeaterZones.ItemDataBound+=new RepeaterItemEventHandler(RepeaterZones_ItemDataBound);
		}

		#endregion

		private void RepGroup_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					CheckBox Check = (CheckBox) e.Item.FindControl("Check");

					switch (Convert.ToByte(DataBinder.Eval((DataRowView) e.Item.DataItem, "tocheck")))
					{
						case 0:
							Check.Checked = false;
							break;
						case 1:
							Check.Checked = true;
							break;
						case 2:
							Check.Enabled = false;
							break;
					}

					break;
			}
		}

		private DataTable CreateMenuHelp(string parent, string query)
		{

			DataSet dsSecondary;
			if (mode == "0" && !UC.DebugMode)
			{
				dsSecondary = DatabaseConnection.CreateDataset("SELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU=" + parent + query + " AND MODE=0 ORDER BY SORTORDER");
			}
			else
			{
				dsSecondary = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU={0}{1} ORDER BY SORTORDER", parent, query));
			}
			return dsSecondary.Tables[0];
		}

		public string QuerySecurityGroups(int gruid)
		{
			string sqlString = "SELECT ID,DEPENDENCY FROM GROUPS WHERE ID=" + gruid + " OR DEPENDENCY LIKE '%|" + gruid + "|%'";

			DataSet dsGroup = DatabaseConnection.CreateDataset(sqlString);
			string dependency = "|";
			if (dsGroup.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow drdip in dsGroup.Tables[0].Rows)
				{
					dependency += drdip["id"].ToString() + "|";
				}
			}

			string query = String.Empty;
			if (dependency.Length > 1)
			{
				string[] dep = dependency.Split('|');
				foreach (string group in dep)
				{
					if (group.Length > 0) query += " ACCESSGROUP LIKE '%|" + group + "|%' OR ";
				}
				query = " AND (" + query.Substring(0, query.Length - 4) + ")";

				return query;
			}
			else
			{
				return "";
			}
		}

		private void RepHomePage_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					DropDownList DropMenuDefault = (DropDownList) e.Item.FindControl("DropMenuDefault");
					DataTable menus = CreateMenuHelp(((Literal) e.Item.FindControl("MenuID")).Text, QuerySecurityGroups(int.Parse(F_Group.SelectedValue)));
					DataTable dtParent;
					int qId = int.Parse(((Literal) e.Item.FindControl("MenuID")).Text);
					dtParent = DatabaseConnection.CreateDataset("SELECT ID,VOICE,LINK,RMVALUE FROM TUSTENAMENU_VIEW WHERE ID=" + qId).Tables[0];
					Literal MenuTitle = (Literal) e.Item.FindControl("MenuTitle");
					MenuTitle.Text =Root.rm.GetString("Menutxt" + dtParent.Rows[0]["rmvalue"]).ToUpper();

					int menuId = int.Parse(((Literal) e.Item.FindControl("MenuID")).Text);
					DataTable dtHelp = DatabaseConnection.CreateDataset("SELECT * FROM HELPMENU WHERE MENUID=" + menuId).Tables[0];
					if (dtHelp.Rows.Count > 0)
					{
						if (File.Exists(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + UC.Culture.Substring(0, 2) + Path.DirectorySeparatorChar + dtHelp.Rows[0]["HelpFile"].ToString()))
							DropMenuDefault.Items.Add(new ListItem(Root.rm.GetString("Menutxt" + dtParent.Rows[0]["rmvalue"]).ToUpper() + " HELP", dtParent.Rows[0]["id"].ToString()));
						else
							DropMenuDefault.Items.Add(new ListItem(Root.rm.GetString("Menutxt" + dtParent.Rows[0]["rmvalue"]).ToUpper(), dtParent.Rows[0]["id"].ToString()));
					}
					else
						DropMenuDefault.Items.Add(new ListItem(Root.rm.GetString("Menutxt" + dtParent.Rows[0]["rmvalue"]).ToUpper(), dtParent.Rows[0]["id"].ToString()));


					string selectedMenu = (DataBinder.Eval((DataRowView) e.Item.DataItem, "NewHomePageID")).ToString();
					foreach (DataRow dr in menus.Rows)
					{
						DropMenuDefault.Items.Add(new ListItem(Root.rm.GetString("Menutxt" + dr["rmvalue"]), dr["id"].ToString()));
					}
					DropMenuDefault.SelectedIndex = -1;
					foreach (ListItem li in DropMenuDefault.Items)
					{
						if (li.Value == selectedMenu)
						{
							li.Selected = true;
							break;
						}
					}
					break;
			}
		}

		private void RepeaterZones_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					CheckBox Check = (CheckBox) e.Item.FindControl("Checkbox1");
					switch (Convert.ToByte(DataBinder.Eval((DataRowView) e.Item.DataItem, "tocheck")))
					{
						case 0:
							Check.Checked = false;
							break;
						case 1:
							Check.Checked = true;
							break;

					}

					break;
			}
		}
	}
}
