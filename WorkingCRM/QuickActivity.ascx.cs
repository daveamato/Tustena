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

using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.WorkingCRM
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	public delegate void activitySaved();
	public partial class QuickActivity : System.Web.UI.UserControl
	{
		public event activitySaved ActivitySaved;
		protected System.Web.UI.WebControls.DomValidators.RequiredDomValidator CrossWithIDValidator;
		private UserConfig UC = new UserConfig();

		private string crossID=string.Empty;
		public string CrossID
		{
			set{crossID=value;}
		}

        private string actID = string.Empty;
        public string ActID
        {
            set { actID = value; }
        }

		private int crossType=0;
		public int CrossType
		{
			set{crossType=value;}
		}

		private int oppId=0;

		public int OppId
		{
			set
			{
				oppId=value;
				ViewState[this.ID+"_OppId"]=value;
			}
			get
			{
				if(ViewState[this.ID+"_OppId"]!=null)
					return Convert.ToInt32(ViewState[this.ID+"_OppId"]);
				else
					return oppId;

			}
		}

		private ActivityTypeN actType=ActivityTypeN.PhoneCall;
		public ActivityTypeN ActType
		{
			set{actType=value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			btnSave.Text=Root.rm.GetString("Save");
		}

		private void FilldropActivityType()
		{
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt6"),((int)ActivityTypeN.PhoneCall).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt7"),((int)ActivityTypeN.Letter).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt8"),((int)ActivityTypeN.Fax).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt9"),((int)ActivityTypeN.Memo).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt10"),((int)ActivityTypeN.Email).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt12"),((int)ActivityTypeN.Generic).ToString()));
			dropActivityType.Items.Add(new ListItem(Root.rm.GetString("Wortxt13"),((int)ActivityTypeN.CaseSolution).ToString()));

		}

		private void FillCrossWith()
		{
			ListItem li = new ListItem(Root.rm.GetString("Mailtxt13"),"0");
			CrossWith.Items.Add(li);
			li = new ListItem(Root.rm.GetString("Mailtxt14"),"1");
			CrossWith.Items.Add(li);
			Modules M = new Modules();
            M.ActiveModule = UC.Modules;
            if (M.IsModule(ActiveModules.Lead))
            {
                li = new ListItem(Root.rm.GetString("Mailtxt15"), "2");
                CrossWith.Items.Add(li);
            }
			CrossWith.RepeatDirection=RepeatDirection.Horizontal;
			CrossWith.Items[0].Selected=true;
		}

		public void FillPreset()
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
            if (actID.Length > 0)
            {
                DataRow dr = DatabaseConnection.CreateDataset("SELECT SUBJECT, DESCRIPTION, ACTIVITYDATE FROM CRM_WORKACTIVITY WHERE ID=" + actID).Tables[0].Rows[0];
                TextBoxId.Text = actID;
                txtSubject.Text = dr[0].ToString();
                txtNote.Text = dr[1].ToString();
                DateTime getdate = UC.LTZ.ToLocalTime((DateTime)dr[2]);
                TextBoxData.Text = getdate.ToShortDateString();
                TextBoxHour.Text = getdate.ToShortTimeString();

            }
            else
            {
                DateTime getdate = UC.LTZ.ToLocalTime(DateTime.UtcNow);
                TextBoxData.Text = getdate.ToShortDateString();
                TextBoxHour.Text = getdate.ToShortTimeString();
            }
			if(crossID.Length>0)
			{
				CrossWithID.Text=crossID;
				CrossWith.Items.Clear();
				FilldropActivityType();
				FillCrossWith();
				CrossWith.SelectedIndex=-1;
				switch(crossType)
				{
					case 0:
						CrossWithText.Text=DatabaseConnection.SqlScalar(string.Format("sELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID={0}",crossID));
						CrossWith.Items[0].Selected=true;
						break;
					case 1:
						CrossWithText.Text=DatabaseConnection.SqlScalar(string.Format("sELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT FROM BASE_CONTACTS WHERE ID={0}",crossID));
						CrossWith.Items[1].Selected=true;
						break;
					case 2:
						CrossWithText.Text=DatabaseConnection.SqlScalar(string.Format("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') AS LEAD FROM CRM_LEADS WHERE ID={0}",crossID));
						CrossWith.Items[2].Selected=true;
						break;
				}
			}

			dropActivityType.SelectedIndex=-1;
			foreach(ListItem li in dropActivityType.Items)
			{
				if(Convert.ToInt32(li.Value)==(int)actType)
				{
					li.Selected=true;
					break;
				}
			}



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

		protected void btnSave_Click(object sender, EventArgs e)
		{
            if (TextBoxId.Text == string.Empty)
            {
                ActivityInsert ai = new ActivityInsert();
                if (CrossWith.Items[0].Selected)
                    ai.InsertActivity(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, CrossWithID.Text, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);
                else if (CrossWith.Items[1].Selected)
                    ai.InsertActivity(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), CrossWithID.Text, string.Empty, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);
                else
                    ai.InsertActivity(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, string.Empty, CrossWithID.Text, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);

                if (RecallDate.Text.Length > 0 && StaticFunctions.IsDate(RecallDate.Text))
                {
                    string starthour = (RecallTextBoxHour.Text.Length > 0) ? RecallTextBoxHour.Text : "00.05";
                    string endhour = (RecallTextBoxHour2.Text.Length > 0) ? RecallTextBoxHour2.Text : "23.55";
                    if (CrossWith.Items[0].Selected)
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, CrossWithID.Text, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                    else if (CrossWith.Items[1].Selected)
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), CrossWithID.Text, string.Empty, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                    else
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, string.Empty, CrossWithID.Text, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                }
            }
            else
            {
                ActivityInsert ai = new ActivityInsert();
                if (CrossWith.Items[0].Selected)
                    ai.ModifyActivity(TextBoxId.Text, dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, CrossWithID.Text, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);
                else if (CrossWith.Items[1].Selected)
                    ai.ModifyActivity(TextBoxId.Text, dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), CrossWithID.Text, string.Empty, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);
                else
                    ai.ModifyActivity(TextBoxId.Text, dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, string.Empty, CrossWithID.Text, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(TextBoxData.Text + " " + TextBoxHour.Text, UC.myDTFI)), UC, 1, true, 0, 0, OppId);

                if (RecallDate.Text.Length > 0 && StaticFunctions.IsDate(RecallDate.Text))
                {
                    string starthour = (RecallTextBoxHour.Text.Length > 0) ? RecallTextBoxHour.Text : "00.05";
                    string endhour = (RecallTextBoxHour2.Text.Length > 0) ? RecallTextBoxHour2.Text : "23.55";
                    if (CrossWith.Items[0].Selected)
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, CrossWithID.Text, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                    else if (CrossWith.Items[1].Selected)
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), CrossWithID.Text, string.Empty, string.Empty, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                    else
                        ai.InsertActivityWithOppRecall(dropActivityType.SelectedValue, string.Empty, UC.UserId.ToString(), string.Empty, string.Empty, CrossWithID.Text, ((txtSubject.Text.Length > 0) ? txtSubject.Text : Root.rm.GetString("Acttxt6")), txtNote.Text, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + starthour, UC.myDTFI)), UC, OppId, UC.LTZ.ToUniversalTime(Convert.ToDateTime(RecallDate.Text + " " + endhour, UC.myDTFI)));
                }
            }




			ActivitySaved();
		}
	}
}
