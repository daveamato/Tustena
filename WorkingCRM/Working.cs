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
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.WorkingCRM
{

	public class ActivityInsert
	{
		public ActivityInsert()
		{
		}

		public void InsertReminder(int id, DateTime data, UserConfig uC, string ownerID, int advanceremind)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OWNERID", ownerID, 'I');
				dg.Add("K_ID", id, 'I');
				dg.Add("TABLENAME", "CRM_WorkActivity", 'I');

				dg.Add("REMINDERDATE", data);
				try
				{
					dg.Add("NOTE", HttpContext.Current.Session["Reminder_RemNote"].ToString());
					HttpContext.Current.Session.Remove("Reminder_RemNote");
				}
				catch
				{
					dg.Add("NOTE", String.Empty);
				}

				dg.Add("ADVANCEREMIND", advanceremind);

				dg.Execute("CRM_REMINDER", "K_ID =" + id + " AND TABLENAME='CRM_WORKACTIVITY' AND OWNERID=" + uC.UserId);
			}
		}

		public void InsertActivity(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int todo)
		{
			InsertActivity(a, visitID, uid, contactID, companyID, leadID, subject, description, data, uC, todo, true, 0,0, 0);
		}

		public void InsertActivity(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC)
		{
			InsertActivity(a, visitID, uid, contactID, companyID, leadID, subject, description, data, uC, 0, true, 0, 0, 0);
		}

		public void InsertActivityWithOpp(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC,int OppId)
		{
			InsertActivity(a, visitID, uid, contactID, companyID, leadID, subject, description, data, uC, 0, true, 0, 0, OppId);
		}

        public void InsertActivityWithOppRecall(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int OppId, DateTime RecallDate)
        {
            InsertActivityRecall(a, visitID, uid, contactID, companyID, leadID, subject, description, data, uC, 1, true, 0, 0, OppId, RecallDate);
        }

		public void InsertActivity(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int todo,int daysallarm)
		{
			InsertActivity(a, visitID, uid, contactID, companyID, leadID, subject, description, data, uC, 0, true, 0, daysallarm, 0);
		}

		public void InsertActivity(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int todo, bool inout, long docid, int daysallarm, int OppId)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("TYPE", a, 'I');
				dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
				dg.Add("CREATEDBYID", uC.UserId, 'I');
				if (visitID.Length > 0) dg.Add("VISITID", visitID, 'I');

				dg.Add("OWNERID", uid.ToString());

				dg.Add("SUBJECT", subject);

				dg.Add("DESCRIPTION", description);
				if (companyID.Length > 0)
					dg.Add("COMPANYID", companyID);
				if (contactID.Length > 0)
					dg.Add("REFERRERID", contactID);
				if (leadID.Length > 0)
					dg.Add("LEADID", leadID);

				dg.Add("GROUPS", "|" + uC.UserGroupId.ToString() + "|");
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", uC.UserId);
				dg.Add("INOUT", inout);
				dg.Add("TODO", todo);

				dg.Add("ACTIVITYDATE", data);
				dg.Add("PARENTID", 0);
				dg.Add("STATE", 0);
				dg.Add("PRIORITY", 0);

				dg.Add("TOBILL", false);
				dg.Add("COMMERCIAL", false);
				dg.Add("TECHNICAL", false);

				dg.Add("ALLARM", data);
				dg.Add("DAYSALLARM", daysallarm);

				dg.Add("DURATION", 0);
				if (docid > 0)
					dg.Add("DOCID", docid);

				if(OppId>0)
					dg.Add("OPPORTUNITYID", OppId);

				if (visitID.Length > 0)
				{
					object newId = dg.Execute("CRM_WORKACTIVITY", "VISITID=" + visitID, DigiDapter.Identities.Identity);
					if (dg.RecordInserted)
						InsertReminder(Convert.ToInt32(newId), data, uC, uid.ToString(), daysallarm);
					else
					{
						int OldID = int.Parse(DatabaseConnection.SqlScalar("SELECT ID FROM CRM_WORKACTIVITY WHERE VISITID=" + visitID));
						InsertReminder(OldID, data, uC, uid.ToString(), daysallarm);
					}
				}
				else
				{
					dg.Execute("CRM_WORKACTIVITY");
				}
			}
		}

        public void InsertActivityRecall(string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int todo, bool inout, long docid, int daysallarm, int OppId, DateTime RecallDate)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("TYPE", a, 'I');
                dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
                dg.Add("CREATEDBYID", uC.UserId, 'I');
                if (visitID.Length > 0) dg.Add("VISITID", visitID, 'I');

                dg.Add("OWNERID", uid.ToString());

                dg.Add("SUBJECT", subject);

                dg.Add("DESCRIPTION", description);
                if (companyID.Length > 0)
                    dg.Add("COMPANYID", companyID);
                if (contactID.Length > 0)
                    dg.Add("REFERRERID", contactID);
                if (leadID.Length > 0)
                    dg.Add("LEADID", leadID);

                dg.Add("GROUPS", "|" + uC.UserGroupId.ToString() + "|");
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", uC.UserId);
                dg.Add("INOUT", inout);
                dg.Add("TODO", todo);

                dg.Add("ACTIVITYDATE", data);
                dg.Add("RECALLENDHOUR", RecallDate);
                dg.Add("PARENTID", 0);
                dg.Add("STATE", 0);
                dg.Add("PRIORITY", 0);

                dg.Add("TOBILL", false);
                dg.Add("COMMERCIAL", false);
                dg.Add("TECHNICAL", false);

                dg.Add("ALLARM", data);
                dg.Add("DAYSALLARM", daysallarm);

                dg.Add("DURATION", 0);
                if (docid > 0)
                    dg.Add("DOCID", docid);

                if (OppId > 0)
                    dg.Add("OPPORTUNITYID", OppId);

                if (visitID.Length > 0)
                {
                    object newId = dg.Execute("CRM_WORKACTIVITY", "VISITID=" + visitID, DigiDapter.Identities.Identity);
                    if (dg.RecordInserted)
                        InsertReminder(Convert.ToInt32(newId), data, uC, uid.ToString(), daysallarm);
                    else
                    {
                        int OldID = int.Parse(DatabaseConnection.SqlScalar("SELECT ID FROM CRM_WORKACTIVITY WHERE VISITID=" + visitID));
                        InsertReminder(OldID, data, uC, uid.ToString(), daysallarm);
                    }
                }
                else
                {
                    dg.Execute("CRM_WORKACTIVITY");
                }
            }
        }

        public void ModifyActivity(string actid, string a, string visitID, string uid, string contactID, string companyID, string leadID, string subject, string description, DateTime data, UserConfig uC, int todo, bool inout, long docid, int daysallarm, int OppId)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("TYPE", a, 'I');
                dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
                dg.Add("CREATEDBYID", uC.UserId, 'I');
                if (visitID.Length > 0) dg.Add("VISITID", visitID, 'I');

                dg.Add("OWNERID", uid.ToString());

                dg.Add("SUBJECT", subject);

                dg.Add("DESCRIPTION", description);
                if (companyID.Length > 0)
                    dg.Add("COMPANYID", companyID);
                if (contactID.Length > 0)
                    dg.Add("REFERRERID", contactID);
                if (leadID.Length > 0)
                    dg.Add("LEADID", leadID);

                dg.Add("GROUPS", "|" + uC.UserGroupId.ToString() + "|");
                dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
                dg.Add("LASTMODIFIEDBYID", uC.UserId);
                dg.Add("INOUT", inout);
                dg.Add("TODO", todo);

                dg.Add("ACTIVITYDATE", data);
                dg.Add("PARENTID", 0);
                dg.Add("STATE", 0);
                dg.Add("PRIORITY", 0);

                dg.Add("TOBILL", false);
                dg.Add("COMMERCIAL", false);
                dg.Add("TECHNICAL", false);

                dg.Add("ALLARM", data);
                dg.Add("DAYSALLARM", daysallarm);

                dg.Add("DURATION", 0);
                if (docid > 0)
                    dg.Add("DOCID", docid);

                if (OppId > 0)
                    dg.Add("OPPORTUNITYID", OppId);


                dg.Execute("CRM_WORKACTIVITY", "ID=" + actid, DigiDapter.Identities.Row);

                DataRow myDataRow = dg.GetNewRow;
                DataRow oldData = dg.GetOriginalRow;

                if (dg.recordUpdated)
                {
                    if (oldData["OwnerID"].ToString() != myDataRow["OwnerID"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveOwner + ", '" + oldData["OwnerID"].ToString() + "', '" + myDataRow["OwnerID"].ToString() + "', getdate(), '" + uC.UserId + "')");

                    if (oldData["ActivityDate"].ToString() != myDataRow["ActivityDate"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveDate + ", '" + (uC.LTZ.ToLocalTime(Convert.ToDateTime(oldData["ActivityDate"]))).ToString(@"yyyyMMdd H.mm") + "', '" + (uC.LTZ.ToLocalTime(Convert.ToDateTime(myDataRow["ActivityDate"]))).ToString(@"yyyyMMdd H.mm") + "', getdate(), '" + uC.UserId + "')");

                    if (oldData["CompanyID"].ToString() != myDataRow["CompanyID"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveCompany + ", '" + oldData["CompanyID"].ToString() + "', '" + myDataRow["CompanyID"].ToString() + "', getdate(), '" + uC.UserId + "')");

                    if (oldData["ReferrerID"].ToString() != myDataRow["ReferrerID"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveContact + ", '" + oldData["ReferrerID"].ToString() + "', '" + myDataRow["ReferrerID"].ToString() + "', getdate(), '" + uC.UserId + "')");

                    if (oldData["LeadID"].ToString() != myDataRow["LeadID"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveLead + ", '" + oldData["LeadID"].ToString() + "', '" + myDataRow["LeadID"].ToString() + "', getdate(), '" + uC.UserId + "')");

                    if (oldData["ToDo"].ToString() != myDataRow["ToDo"].ToString())
                        DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + actid + ", " + (byte)ActivityMoveLog.MoveDone + ", '" + oldData["ToDo"].ToString() + "', '" + myDataRow["ToDo"].ToString() + "', getdate(), '" + uC.UserId + "')");
                }

            }
        }
	}


	public class WorkingCRM : G
	{

		protected virtual void FillDropDown(string a)
		{
			DropDownList dp = (DropDownList) Page.FindControl("DropDownListStatus");
			dp.Items.Clear();
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt0"), "0"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt1"), "1"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt2"), "2"));
			dp.Items[0].Selected = true;
			dp = (DropDownList) Page.FindControl("DropDownListPriority");
			dp.Items.Clear();
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt3"), "0"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt4"), "1"));
			dp.Items.Add(new ListItem(Root.rm.GetString("Wortxt5"), "2"));
			dp.Items[0].Selected = true;
			dp = (DropDownList) Page.FindControl("DropDownListClassification");
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

			dp = (DropDownList) Page.FindControl("DropDownListPreAlarm");
			dp.Items.Clear();
			for (int i = 0; i < 6; i++)
			{
				dp.Items.Add(new ListItem(i.ToString() + " " +Root.rm.GetString("Acttxt124"), i.ToString()));
			}
			dp.Items[0].Selected = true;


		}


		private void ModifyReminderActivity(int id, string data)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("OWNERID", ((TextBox) Page.FindControl("TextboxOwnerID")).Text, 'I');
				dg.Add("K_ID", id, 'I');
				dg.Add("TABLENAME", "CRM_WORKACTIVITY", 'I');
				dg.Add("REMINDERDATE", UC.LTZ.ToUniversalTime(Convert.ToDateTime(data, UC.myDTFI)));
				dg.Add("NOTE", ((TextBox) Page.FindControl("TextBoxSubject")).Text);
				dg.Add("ADVANCEREMIND", (((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedValue.Length > 0) ? ((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedValue : "0");

				dg.Execute("CRM_REMINDER", "K_ID =" + id + " AND TABLENAME='CRM_WORKACTIVITY' AND OWNERID=" + int.Parse(((TextBox) Page.FindControl("TextboxOwnerID")).Text));
			}
		}


		protected virtual string InsModActivity(string a, int id)
		{
		{
			bool PageValid = true;
			string errorCode = String.Empty;
			if (((RadioButtonList) Page.FindControl("Activity_ToDo")).Items[1].Selected && a == "6")
			{
				if (!((RequiredDomValidator) Page.FindControl("StartHourValidator")).IsValid ||
					!((RequiredDomValidator) Page.FindControl("EndHourValidator")).IsValid)
				{
					PageValid = false;
					errorCode = "1";
				}
			}
			if (!((RequiredDomValidator) Page.FindControl("DataValidator")).IsValid)
			{
				PageValid = false;
				errorCode += "2";
			}

			if (((TextBox) Page.FindControl("TextBoxSubject")).Text.Length <= 0)
			{
				PageValid = false;
				errorCode += "3";
			}

			if (PageValid &&
				(((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0 ||
				((TextBox) Page.FindControl("TextboxContactID")).Text.Length > 0 ||
				((TextBox) Page.FindControl("TextboxLeadID")).Text.Length > 0))
			{
				DigiDapter dg = new DigiDapter();

				dg.Add("TYPE", a, 'I');
				dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
				dg.Add("CREATEDBYID", UC.UserId, 'I');


				dg.Add("OWNERID", ((TextBox) Page.FindControl("TextboxOwnerID")).Text);


				if (((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0)
					dg.Add("COMPANYID", ((TextBox) Page.FindControl("TextboxCompanyID")).Text);
				else
				{
					if(((TextBox) Page.FindControl("TextboxContactID")).Text.Length>0)
					{
						try
						{
							string cid = DatabaseConnection.SqlScalar("SELECT COMPANYID FROM BASE_CONTACTS WHERE ID="+int.Parse(((TextBox) Page.FindControl("TextboxContactID")).Text));
							if(int.Parse(cid)>0)
							{
								dg.Add("COMPANYID", cid);
							}
						}
						catch
						{
							dg.Add("COMPANYID", DBNull.Value);
						}
					}
					else
						dg.Add("COMPANYID", DBNull.Value);

				}

				if (((TextBox) Page.FindControl("TextboxContactID")).Text.Length > 0)
					dg.Add("REFERRERID", ((TextBox) Page.FindControl("TextboxContactID")).Text);
				else
					dg.Add("REFERRERID", DBNull.Value);

				if (((TextBox) Page.FindControl("TextboxLeadID")).Text.Length > 0)
					dg.Add("LEADID", ((TextBox) Page.FindControl("TextboxLeadID")).Text);
				else
					dg.Add("LEADID", DBNull.Value);

				if (((TextBox) Page.FindControl("TextboxOpportunityID")).Text.Length > 0)
					if (DatabaseConnection.SqlScalar("SELECT COUNT(ID) FROM CRM_OPPORTUNITY WHERE ID=" + int.Parse(((TextBox) Page.FindControl("TextboxOpportunityID")).Text)) != "0")
						dg.Add("OPPORTUNITYID", ((TextBox) Page.FindControl("TextboxOpportunityID")).Text);
					else
						dg.Add("OPPORTUNITYID", DBNull.Value);
				else
					dg.Add("OPPORTUNITYID", DBNull.Value);

				if (((TextBox) Page.FindControl("IDDocument")).Text.Length > 0)
					dg.Add("DOCID", ((TextBox) Page.FindControl("IDDocument")).Text);
				else
					dg.Add("DOCID", DBNull.Value);

				dg.Add("SUBJECT", ((TextBox) Page.FindControl("TextBoxSubject")).Text);
				dg.Add("DESCRIPTION", ((TextBox) Page.FindControl("TextboxDescription")).Text);
				dg.Add("DESCRIPTION2", ((TextBox) Page.FindControl("TextboxDescription2")).Text);

				dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", UC.UserId.ToString());
				dg.Add("INOUT", (((RadioButtonList) Page.FindControl("Activity_InOut")).SelectedValue == "1") ? true : false);
				dg.Add("TODO", ((RadioButtonList) Page.FindControl("Activity_ToDo")).SelectedValue);

				if (((TextBox) Page.FindControl("F_StartHour")).Text.Length > 0)
					((TextBox) Page.FindControl("TextBoxHour")).Text = ((TextBox) Page.FindControl("F_StartHour")).Text;

                DateTime actDate = new DateTime();
                if (((TextBox)Page.FindControl("TextBoxHour")).Text.Length > 0)
                {
                    try
                    {
                        actDate = UC.LTZ.ToUniversalTime(DateTime.Parse(((TextBox)Page.FindControl("TextBoxData")).Text + " " + ((TextBox)Page.FindControl("TextBoxHour")).Text, UC.myDTFI));
                    }
                    catch
                    {
                        actDate = UC.LTZ.ToUniversalTime(DateTime.Parse(((TextBox)Page.FindControl("TextBoxData")).Text));
                        actDate = actDate.AddHours(Convert.ToDouble(DateTime.Now.Hour));
                        actDate = actDate.AddMinutes(Convert.ToDouble(DateTime.Now.Minute));
                    }
                }
                else
                {
                    actDate = UC.LTZ.ToUniversalTime(DateTime.Parse(((TextBox)Page.FindControl("TextBoxData")).Text));
                    actDate = actDate.AddHours(Convert.ToDouble(DateTime.Now.Hour));
                    actDate = actDate.AddMinutes(Convert.ToDouble(DateTime.Now.Minute));
                }

                dg.Add("ACTIVITYDATE", actDate);
				if (((DropDownList) Page.FindControl("DropDownListClassification")).SelectedIndex != -1)
					dg.Add("CLASSIFICATION", ((DropDownList) Page.FindControl("DropDownListClassification")).SelectedValue);
				if (((TextBox) Page.FindControl("TextboxParentID")).Text.Length > 0)
					dg.Add("PARENTID", ((TextBox) Page.FindControl("TextboxParentID")).Text);
				else
					dg.Add("PARENTID", 0);
				dg.Add("STATE", ((DropDownList) Page.FindControl("DropDownListStatus")).SelectedValue);
				dg.Add("PRIORITY", ((DropDownList) Page.FindControl("DropDownListPriority")).SelectedValue);

				dg.Add("TOBILL", ((CheckBox) Page.FindControl("CheckToBill")).Checked);
				dg.Add("COMMERCIAL", ((CheckBox) Page.FindControl("CheckCommercial")).Checked);
				dg.Add("TECHNICAL", ((CheckBox) Page.FindControl("CheckTechnical")).Checked);

				if (((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedIndex > 0)
				{
					dg.Add("ALLARM", UC.LTZ.ToUniversalTime(Convert.ToDateTime(((TextBox) Page.FindControl("TextBoxData")).Text, UC.myDTFI)));
					dg.Add("DAYSALLARM", ((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedValue);
				}

				int duration = 0;
				if (((TextBox) Page.FindControl("TextBoxDurationH")).Text.Length > 0)
				{
					try
					{
						duration += Convert.ToInt32(((TextBox) Page.FindControl("TextBoxDurationH")).Text)*60;
					}
					catch
					{
						duration += 0;
					}
				}

				if (((TextBox) Page.FindControl("TextBoxDurationM")).Text.Length > 0)
				{
					try
					{
						duration += Convert.ToInt32(((TextBox) Page.FindControl("TextBoxDurationM")).Text);
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

				if (((CheckBox) Page.FindControl("CheckSendMail")).Checked && ((HtmlInputText) Page.FindControl("destinationEmail")).Value.Length > 0)
				{
					if (dg.RecordInserted)
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   ('" + myDataRow["ID"].ToString() + "', " + (byte) ActivityMoveLog.MailSent + ", '" + UC.UserName + "', '" + DatabaseConnection.FilterInjection(((HtmlInputText) Page.FindControl("destinationEmail")).Value) + "', getdate(), '" + UC.UserId + "')");
					else
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   ('" + id + "', " + (byte) ActivityMoveLog.MailSent + ", '" + UC.UserName + "', '" + DatabaseConnection.FilterInjection(((HtmlInputText) Page.FindControl("destinationEmail")).Value) + "', getdate(), '" + UC.UserId + "')");
				}

				((Label) Page.FindControl("ActivityID")).Text = myDataRow["ID"].ToString();
				int newId = Convert.ToInt32(myDataRow["ID"]);
				if (((RadioButtonList) Page.FindControl("Activity_ToDo")).SelectedValue == "0" || ((DropDownList) Page.FindControl("DropDownListPreAlarm")).SelectedValue.Length > 0)
					ModifyReminderActivity(newId, ((TextBox) Page.FindControl("TextBoxData")).Text);

				if (dg.RecordInserted)
				{
					int oId = int.Parse(((TextBox) Page.FindControl("TextboxOwnerID")).Text);
					if (((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > 0)
					{
						int compId = int.Parse(((TextBox) Page.FindControl("TextboxCompanyID")).Text);
						string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=0 AND ACCOUNT=" + oId + " AND CROSSID=" + compId);
						if (acId.Length > 0)
							DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
						else
							DatabaseConnection.DoCommand("INSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + compId + ",0)");
					}
					if (((TextBox) Page.FindControl("TextboxContactID")).Text.Length > 0)
					{
						int contId = int.Parse(((TextBox) Page.FindControl("TextboxContactID")).Text);
						string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=1 AND ACCOUNT=" + oId + " AND CROSSID=" + contId);
						if (acId.Length > 0)
							DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
						else
							DatabaseConnection.DoCommand("INSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + contId + ",1)");
					}
					if (((TextBox) Page.FindControl("TextboxLeadID")).Text.Length > 0)
					{
						int leadId = int.Parse(((TextBox) Page.FindControl("TextboxLeadID")).Text);
						string acId = DatabaseConnection.SqlScalar("SELECT ID FROM LASTCONTACT WHERE TABLEID=2 AND ACCOUNT=" + oId + " AND CROSSID=" + leadId);
						if (acId.Length > 0)
							DatabaseConnection.DoCommand("UPDATE LASTCONTACT SET ACTIVITYID=" + newId + ",ACTIVITYDATE=GETDATE() WHERE ID=" + acId);
						else
							DatabaseConnection.DoCommand("INSERT INTO LASTCONTACT (ACCOUNT,ACTIVITYID,CROSSID,TABLEID) VALUES (" + oId + "," + newId + "," + leadId + ",2)");
					}
				}

				if (((RadioButtonList) Page.FindControl("Activity_ToDo")).Items[0].Selected && a == "6")
					((HtmlContainerControl) Page.FindControl("Appointmenthours")).Attributes.Add("style", "display:none");


				if (dg.recordUpdated)
				{
					if (oldData["OwnerID"].ToString() != myDataRow["OwnerID"].ToString())
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveOwner + ", '" + oldData["OwnerID"].ToString() + "', '" + myDataRow["OwnerID"].ToString() + "', getdate(), '" + UC.UserId + "')");

					if (oldData["ActivityDate"].ToString() != myDataRow["ActivityDate"].ToString())
							DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveDate + ", '" + (UC.LTZ.ToLocalTime(Convert.ToDateTime( oldData["ActivityDate"]))).ToString(@"yyyyMMdd H.mm") + "', '" + (UC.LTZ.ToLocalTime(Convert.ToDateTime( myDataRow["ActivityDate"]))).ToString(@"yyyyMMdd H.mm") + "', getdate(), '" + UC.UserId + "')");

					if (oldData["CompanyID"].ToString() != myDataRow["CompanyID"].ToString())
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveCompany + ", '" + oldData["CompanyID"].ToString() + "', '" + myDataRow["CompanyID"].ToString() + "', getdate(), '" + UC.UserId + "')");

					if (oldData["ReferrerID"].ToString() != myDataRow["ReferrerID"].ToString())
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveContact + ", '" + oldData["ReferrerID"].ToString() + "', '" + myDataRow["ReferrerID"].ToString() + "', getdate(), '" + UC.UserId + "')");

					if (oldData["LeadID"].ToString() != myDataRow["LeadID"].ToString())
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveLead + ", '" + oldData["LeadID"].ToString() + "', '" + myDataRow["LeadID"].ToString() + "', getdate(), '" + UC.UserId + "')");

					if (oldData["ToDo"].ToString() != myDataRow["ToDo"].ToString())
						DatabaseConnection.DoCommand("INSERT INTO ACTIVITYMOVELOG (ACID, ACTIONTYPE, PREVVALUE, NEXTVALUE, MOVEDATE, OWNERID) VALUES   (" + id + ", " + (byte) ActivityMoveLog.MoveDone + ", '" + oldData["ToDo"].ToString() + "', '" + myDataRow["ToDo"].ToString() + "', getdate(), '" + UC.UserId + "')");
				}

				return "0";
			}
			else
			{
				string error = String.Empty;
				if (errorCode.IndexOf("1") > -1 || errorCode.IndexOf("2") > -1)
					error +=Root.rm.GetString("Acttxt101") + "<br>";

				if (!(((TextBox) Page.FindControl("TextBoxSubject")).Text.Length > -1))
					error +=Root.rm.GetString("Acttxt100") + "<br>";

				if (!(((TextBox) Page.FindControl("TextboxCompanyID")).Text.Length > -1) &&
					!(((TextBox) Page.FindControl("TextboxContactID")).Text.Length > -1))
					error +=Root.rm.GetString("Acttxt102") + "<br>";

				if (((RadioButtonList) Page.FindControl("Activity_ToDo")).Items[0].Selected && a == "6")
					((HtmlContainerControl) Page.FindControl("Appointmenthours")).Attributes.Add("style", "display:none");

				return error;
			}
		}
		}
	}
}
