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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using Digita.Mailer;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena.MailingList.webmail
{
	public delegate void OnSendMail(bool saved);

	public partial class mailout : UserControl
	{
		public event OnSendMail OnSendMail;

		protected Repeater RepAttach;
		protected LinkButton MailIn;

		protected CheckBox CreaAttivita;
		UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];

        protected override void OnPreRender(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (!M.IsModule(ActiveModules.Storage))
                StorageModule.Visible = false;
            base.OnPreRender(e);
        }


		protected void Page_Load(object sender, EventArgs e)
		{
				if(!Page.IsPostBack)
				{
					Submitbtn.Text=Root.rm.GetString("Send");
					string notifyemail = DatabaseConnection.SqlScalar("SELECT NOTIFYEMAIL FROM ACCOUNT WHERE UID="+UC.UserId);
					MailAddressFrom.Text=(notifyemail.Length>0)?notifyemail:UC.UserName;

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
					CrossWith.SelectedIndex=0;
					if(StaticFunctions.IsBlank(Request["to"]))return;
					string to = DatabaseConnection.FilterInjection(Request["to"]);
					try
					{
						switch(to.Substring(0,1))
						{
							case "A":
								MailAddressTo.Text=DatabaseConnection.SqlScalar("SELECT EMAIL FROM BASE_COMPANIES ID="+to.Substring(1,to.Length-1));
								CrossWith.Items[0].Selected=true;
								break;
							case "C":
								MailAddressTo.Text=DatabaseConnection.SqlScalar("SELECT EMAIL FROM BASE_CONTACTS ID="+to.Substring(1,to.Length-1));
								CrossWith.Items[1].Selected=true;
								break;
							case "L":
								MailAddressTo.Text=DatabaseConnection.SqlScalar("SELECT EMAIL FROM CRM_LEADS WHERE ID="+to.Substring(1,to.Length-1));
								CrossWith.Items[2].Selected=true;
								break;
						}
						ActivityCross.Text=to;
						MailAddressToID.Text=to.Substring(1,to.Length-1);
						CreateActivity.Checked=true;
                        if (MailAddressTo.Text.IndexOf(';') > 0)
                            MailAddressTo.Text = MailAddressTo.Text.Split(';')[0];
					}
					catch{}
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

		protected void SubmitBtn_Click(object sender, EventArgs e)
		{
			SmtpEmailer emailer = new SmtpEmailer();
			emailer.From = MailAddressFrom.Text;
			emailer.Subject = MailObject.Text;
			emailer.SendAsHtml = true;

			if(MailAddressTo.Text.Length>0)
			{
				if(MailAddressTo.Text.IndexOf(';')>1)
				{
					string[] MailAddress = MailAddressTo.Text.Split(';');
					foreach(string to in MailAddress)
						emailer.To.Add(to);
				}
				else
				{
					emailer.To.Add(MailAddressTo.Text);
				}

			}

			if(MailAddressCc.Text.Length>0)
			{
				if(MailAddressCc.Text.IndexOf(';')>1)
				{
					string[] MailCc = MailAddressCc.Text.Split(';');
					foreach(string to in MailCc)
						emailer.CC.Add(to);
				}
				else
				{
					emailer.To.Add(MailAddressCc.Text);
				}
			}

			if(MailAddressCcn.Text.Length>0)
			{
				if(MailAddressCcn.Text.IndexOf(';')>1)
				{
					string[] MailCcn = MailAddressCcn.Text.Split(';');
					foreach(string to in MailCcn)
						emailer.BCC.Add(to);
				}
				else
				{
					emailer.To.Add(MailAddressCcn.Text);
				}
			}



			emailer.Body = MailMessage.Text.Replace("\r","").Replace("\n","<br>");
			string filetodelete="";
			if(this.IDDocument.Text.Length>0)
			{
				DataTable dtAttach;
				dtAttach = DatabaseConnection.CreateDataset(String.Format("SELECT GUID,FILENAME FROM FILEMANAGER WHERE ID={0}",int.Parse(IDDocument.Text))).Tables[0];
				if(dtAttach.Rows.Count>0)
				{
					try
					{
						string dirPath;
						string filePath;
						dirPath = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + dtAttach.Rows[0]["guid"].ToString();
						filePath = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + dtAttach.Rows[0]["filename"].ToString();
						FileFunctions.CheckDir(ConfigSettings.DataStoragePath,true);
						if (File.Exists(dirPath+ Path.GetExtension(dtAttach.Rows[0]["filename"].ToString())))
							File.Copy( dirPath+ Path.GetExtension(dtAttach.Rows[0]["filename"].ToString()), filePath);
						else
							File.Copy( dirPath, filePath);

						SmtpAttachment att = new SmtpAttachment();
						att.FileName=filePath;
						att.ContentType="application/octet-stream";
						att.Location = AttachmentLocation.Attachment;
						emailer.Attachments.Add(att);
						filetodelete=filePath;
					}
					catch{}
				}

			}

			switch (ConfigSettings.SpoolFormat)
			{
				case "mssmtp":
					emailer.SendIISSMTPMessage(ConfigSettings.MailSpoolPath);
					break;
				case "xmail":
					emailer.SendXMailMessage(ConfigSettings.MailSpoolPath);
					break;
				default:
					emailer.Host=ConfigSettings.SMTPServer;
					emailer.AuthenticationMode=AuthenticationType.Plain;
					if(ConfigSettings.SMTPAuthRequired)
					{
						emailer.User=ConfigSettings.SMTPUser;
						emailer.Password=ConfigSettings.SMTPPassword;
					}
					emailer.SendMessageAsync();
					break;
			}

			if(filetodelete.Length>0)
				File.Delete(filetodelete);


			if(CreateActivity.Checked && MailAddressToID.Text.Length>0)
			{
				ActivityInsert ai = new ActivityInsert();
				string A="";
				string C="";
				string L="";

				switch(CrossWith.SelectedValue)
				{
					case "0":
						A=MailAddressToID.Text;
						C="";
						L="";
						break;
					case "1":
						C=MailAddressToID.Text;
						A="";
						L="";
						break;
					case "2":
						L=MailAddressToID.Text;
						A="";
						C="";
						break;
				}
				if(A.Length>0 || C.Length>0 || L.Length>0)
					ai.InsertActivity("5", "", UC.UserId.ToString(), C, A, L, this.MailObject.Text, this.MailMessage.Text, UC.LTZ.ToUniversalTime(DateTime.Now), UC,1);
			}
			if(Session["fromtopbar"]!=null)
			{
				Session.Remove("fromtopbar");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Mailtxt19") + "');self.close();</script>");
			}else if(Session["fromquick"]!=null)
			{
				Session.Remove("fromquick");
				OnSendMail(true);
			}
			else{
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('" + Root.rm.GetString("Mailtxt19") + "');location.href='/mailinglist/webmail/webmail.aspx?m=46&dgb=1&si=63';</script>");
			}


		}

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
			}
		}
	}
}
