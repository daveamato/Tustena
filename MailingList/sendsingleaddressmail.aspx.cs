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
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;
using System.Xml;
using Digita.Mailer;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena.MailingList
{
	public partial class sendsingleaddressmail : G
	{

		private ArrayList mailsdata = new ArrayList();
		private string fromad;
		private string subject;
		private string mailbody;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				RegexMailValidator1.ValidationExpression = @"(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$";
				RegexMailValidator2.ValidationExpression = @"(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$";

				RegexMailValidator1.ErrorMessage =Root.rm.GetString("Mailtxt1");
				RequiredMailValidator1.ErrorMessage =Root.rm.GetString("Mailtxt1");

				RegexMailValidator2.ErrorMessage =Root.rm.GetString("Mailtxt2");
				RequiredMailValidator2.ErrorMessage =Root.rm.GetString("Mailtxt2");

				valSum.HeaderText=Root.rm.GetString("ValidSummary");

				MailRepeater.Visible=false;
				if (!Page.IsPostBack)
				{
					BtnSearch.Text =Root.rm.GetString("Find");
					SendMail.Text =Root.rm.GetString("Send");
					FlagSearch.RepeatDirection = RepeatDirection.Horizontal;

					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt11"), "0"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt8"), "1"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt9"), "2"));
					FlagSearch.Items.Add(new ListItem(Root.rm.GetString("Quicktxt10"), "3"));
					FlagSearch.Items[0].Selected = true;

					ViewState["mailid"]=Session["MailToSendID"];
					Session.Remove("MailToSendID");
					MailSubject.Text = DatabaseConnection.SqlScalar("SELECT SUBJECT FROM ML_MAIL WHERE ID="+ViewState["mailid"]);
					Info1.Attributes.Add("src","mailpreview.aspx?render=no&id="+ViewState["mailid"]);

					if(Session["welcomemail"]!=null)
					{
						string[] welcome = Session["welcomemail"].ToString().Split('|');
						MailAddress.Text=welcome[0];
						this.MailAddressToID.Text=welcome[1];
						this.CrossWith.Text=welcome[2];
						Session.Remove("welcomemail");
						this.FromMailAddress.Text=UC.MailingAddress;
						this.CreateActivity.Checked=true;
						ViewState.Add("fromwelcome",true);
					}
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
			this.SendMail.Click+=new EventHandler(SendMail_Click);
			this.BtnSearch.Click+=new EventHandler(BtnSearch_Click);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			bool all = this.FlagSearch.Items[0].Selected;
			DataTable contacts = new DataTable();
			DataTable leads = new DataTable();
			DataTable companies = new DataTable();
			DataTable finaldt = new DataTable();

			if(all || this.FlagSearch.Items[1].Selected)
			{
				contacts = DatabaseConnection.CreateDataset(string.Format("SELECT EMAIL,ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') AS CONTACT, '1' AS TYPE, ID FROM BASE_CONTACTS WHERE (NAME LIKE '%{0}%' OR SURNAME LIKE '%{0}%')",SearchTextBox.Text)).Tables[0];
			}
			if(all || this.FlagSearch.Items[2].Selected)
			{
				leads = DatabaseConnection.CreateDataset(string.Format("SELECT EMAIL,ISNULL(SURNAME,'')+' '+ISNULL(NAME,'') AS CONTACT, '2' AS TYPE, ID FROM CRM_LEADS WHERE (NAME LIKE '%{0}%' OR SURNAME LIKE '%{0}%')",SearchTextBox.Text)).Tables[0];
			}
			if(all || this.FlagSearch.Items[3].Selected)
			{
				companies = DatabaseConnection.CreateDataset(string.Format("SELECT EMAIL,COMPANYNAME AS CONTACT, '0' AS TYPE, ID FROM BASE_COMPANIES WHERE (COMPANYNAME LIKE '%{0}%')",SearchTextBox.Text)).Tables[0];
			}
			if(all)
			{
				DataTable tempdt = DataManipulation.Union(contacts,leads);
				finaldt = DataManipulation.Union(tempdt,companies);
			}else
			{
				switch(this.FlagSearch.SelectedIndex)
				{
					case 1:
						finaldt=contacts;
						break;
					case 2:
						finaldt=leads;
						break;
					case 3:
						finaldt=companies;
						break;
				}
			}
			this.MailRepeater.DataSource=finaldt;
			this.MailRepeater.DataBind();
			MailRepeater.Visible=true;
		}

		private void SendMail_Click(object sender, EventArgs e)
		{
			ArrayList mails = new ArrayList();
			NewMailingList.MailList mailstruct = new NewMailingList.MailList();
			DataRow drmail = DatabaseConnection.CreateDataset("SELECT BODY,SUBJECT FROM ML_MAIL WHERE ID="+int.Parse(ViewState["mailid"].ToString())).Tables[0].Rows[0];


			DataTable dt = new DataTable();
			mailstruct.Email = MailAddress.Text;
			mailstruct.CompanyName = "";
			mailstruct.Name = "";
			mailstruct.Surname = "";
			mailstruct.Address = "";
			mailstruct.City = "";
			mailstruct.Province = "";
			mailstruct.Nation = "";
			mailstruct.Zip = "";
			mailstruct.RefID = "";
			mails.Add(mailstruct);


			PrepareMailFields(mails, this.FromMailAddress.Text, drmail[1].ToString(), drmail[0].ToString());
			StartSend(ViewState["mailid"].ToString());

		}

		public void PrepareMailFields(ArrayList mails, string fromad, string subject, string mailbody)
		{
			this.mailsdata = mails;
			this.fromad = fromad;
			this.subject = subject;
			this.mailbody = headFix(mailbody, "utf-8");
		}

		private string headFix(string message, string charset)
		{

			Match oMatch = Regex.Match(message, @"(<html[\s\S]*?>[\s\S]*?<head[\s\S]*?>[\s\S]*?</head[\s\S]*?>[\s\S]*?<body[\s\S]*?>[\s\S]*?</body[\s\S]*?>[\s\S]*?</html[\s\S]*?>)");
			if (oMatch.Success)
			{
				message = Regex.Replace(message, @"(<head.*?>[\s\S]*?)(<title[\s\S]*/title[\s\S]*>)([\s\S]*</head[\s\S]*>)", "$1$3");
				message = Regex.Replace(message, @"(<head.*?>[\s\S]*?)(<meta[\s\S]*?http-equiv=[ '""]?Content-Type[ '""]?[\s\S]*?>)([\s\S]*</head[\s\S]*>)", "$1$3");
				message = Regex.Replace(message, @"</head.*?>", String.Format("<meta http-equiv=\"Content-Type\" content=\"text/html; charset={0}\"/>\r\n</head>", charset));
			}
			else
			{
				message = String.Format("<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"/>\r\n</head>\r\n<body>\r\n{0}\r\n</body>\r\n</html>", message);
			}
			return message;
		}

		public void StartSend(string mlid)
		{
			String vbCrLf = "\r\n";
			String vbTab = "\t";
			NewMessage msg;
			inline imagescid = new inline();
			string[] cids;
			imagescid.cidinclude(ref mailbody, WebEditorUtils.WebUserFilesPath, out cids);
			msg = new NewMessage(fromad, subject, mailbody, NewMessage.MessageType.Html);
			msg.AddHeader("X-Originating-IP", Request.UserHostAddress);
			msg.AddHeader("X-Originating-UA", Request.UserAgent);

			msg.AddHeader("Received", "from " + Request.UserHostName + " [" + Request.UserHostAddress + "]" +
				vbCrLf + vbTab + "by " + Request.Url.Host + " [" + Request.ServerVariables["LOCAL_ADDR"] + "] (Digita.MimeParser) via HTTP;" +
				vbCrLf + vbTab + DateTime.UtcNow.ToString(@"ddd, dt MMM yyyy HH\:mm\:ss", new CultureInfo("en-US")) + " +0000");

			msg.AddRecipient("to@to.to", NewMessage.RecipientTypes.To);


			foreach (string ss in cids)
			{
				if (ss != null)
				{
					string localfilename;
					localfilename = Path.Combine(WebEditorUtils.RootUserFilesPath, ss.Replace('/', Path.DirectorySeparatorChar));
					msg.AddAttachment(localfilename, ss.Substring(ss.LastIndexOf("/") > 0 ? ss.LastIndexOf("/") + 1 : ss.LastIndexOf("/")), StaticFunctions.ContentTypeMatch(ss), NewMessage.Encodings.Base64, true);
				}
			}

			DataTable dtAttach = DatabaseConnection.CreateDataset("SELECT FILEMANAGER.ID, FILEMANAGER.FILENAME, FILEMANAGER.GUID FROM FILEMANAGER RIGHT OUTER JOIN ML_ATTACHMENT ON FILEMANAGER.ID = ML_ATTACHMENT.FILEID WHERE ML_ATTACHMENT.MLID = " + int.Parse(ViewState["mailid"].ToString())).Tables[0];
			if (dtAttach.Rows.Count > 0)
			{
				string fileName;
				fileName = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + dtAttach.Rows[0]["guid"].ToString() + Path.GetExtension(dtAttach.Rows[0]["filename"].ToString());
				if(File.Exists(fileName))
					msg.AddAttachment(fileName, dtAttach.Rows[0]["filename"].ToString(), "application/octet-stream", NewMessage.Encodings.Base64, false);
			}


			string templatePath = Path.Combine(Request.PhysicalApplicationPath + "template", "mailing");
			string templateFilename = "tustena_signature.htm";
			string templateFileNamePlain = "tustena_signature.txt";
			string signature = String.Empty;
			string signaturePlain = String.Empty;

			if (File.Exists(Path.Combine(Path.Combine(templatePath, UC.CultureSpecific), templateFilename)))
				templateFilename = Path.Combine(Path.Combine(templatePath, UC.CultureSpecific), templateFilename);
			else
				templateFilename = Path.Combine(templatePath, templateFilename);
			if (File.Exists(templateFilename))
			{
				StreamReader objReader = new StreamReader(templateFilename);
				signature = objReader.ReadToEnd();
				objReader.Close();
			}

			if (File.Exists(Path.Combine(Path.Combine(templatePath, UC.CultureSpecific), templateFileNamePlain)))
				templateFileNamePlain = Path.Combine(Path.Combine(templatePath, UC.CultureSpecific), templateFileNamePlain);
			else
				templateFileNamePlain = Path.Combine(templatePath, templateFileNamePlain);
			if (File.Exists(templateFileNamePlain))
			{
				StreamReader objReader = new StreamReader(templateFileNamePlain);
				signaturePlain = objReader.ReadToEnd();
				objReader.Close();
			}

			DataRow myCompany;
			myCompany = DatabaseConnection.CreateDataset("SELECT COMPANYNAME,ADDRESS,ZIPCODE,CITY,STATE FROM TUSTENA_DATA").Tables[0].Rows[0];

			signature = signature.Replace("%COMPANY%", myCompany[0].ToString());
			signature = signature.Replace("%ADDRESS%", myCompany[1].ToString());
			signature = signature.Replace("%CAP%", myCompany[2].ToString());
			signature = signature.Replace("%COUNTRY%", myCompany[3].ToString());
			signature = signature.Replace("%STATE%", myCompany[4].ToString());
			signature = signature.Replace("%MLID%", mlid);

			signaturePlain = signaturePlain.Replace("%COMPANY%", myCompany[0].ToString());
			signaturePlain = signaturePlain.Replace("%ADDRESS%", myCompany[1].ToString());
			signaturePlain = signaturePlain.Replace("%CAP%", myCompany[2].ToString());
			signaturePlain = signaturePlain.Replace("%COUNTRY%", myCompany[3].ToString());
			signaturePlain = signaturePlain.Replace("%STATE%", myCompany[4].ToString());
			signaturePlain = signaturePlain.Replace("%MLID%", mlid);

			msg.AddSignature(signature, signaturePlain);
			string message;
			switch(ConfigSettings.SpoolFormat)
			{
				case "xmail":
					message = msg.ToString(NewMessage.MessageFormats.XMailSpool);
					break;
				case "mssmtp":
					message = msg.ToString(NewMessage.MessageFormats.MSSMTPSpool);
					break;
				default:
					message = msg.ToString(NewMessage.MessageFormats.RFC822);
					break;
			}

			SpoolMail(message);

			if(CreateActivity.Checked)
				CreateMailActivity();
			if (ViewState["fromwelcome"]!=null && isGoBack)
			{
				GoBackClick();
			}
		}

		private void SpoolMail(string mailMessage)
		{
			string fileName;
			if(ConfigSettings.UseSpoolService)
			{
				fileName = ConfigSettings.MailMailingPath + Guid.NewGuid().ToString() + ".xml";

				XmlTextWriter myXmlTextWriter = new XmlTextWriter(fileName, null);
				myXmlTextWriter.Formatting = Formatting.Indented;
				myXmlTextWriter.WriteStartDocument(false);
				myXmlTextWriter.WriteStartElement("ml");

				myXmlTextWriter.WriteStartElement("message");
				myXmlTextWriter.WriteCData(mailMessage);
				myXmlTextWriter.WriteEndElement();
				myXmlTextWriter.WriteStartElement("mailing");
				int i = 1;
				foreach (Object o in mailsdata)
				{
					NewMailingList.MailList ob = (NewMailingList.MailList) o;
					myXmlTextWriter.WriteStartElement("mail");
					myXmlTextWriter.WriteAttributeString("id", (i++).ToString());
					myXmlTextWriter.WriteAttributeString("to", ob.Email.Trim(' '));
					myXmlTextWriter.WriteAttributeString("refid", ob.RefID);
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.CompanyName");
					myXmlTextWriter.WriteAttributeString("value", ob.CompanyName);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.Name");
					myXmlTextWriter.WriteAttributeString("value", ob.Name);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.Surname");
					myXmlTextWriter.WriteAttributeString("value", ob.Surname);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.Address");
					myXmlTextWriter.WriteAttributeString("value", ob.Address);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.City");
					myXmlTextWriter.WriteAttributeString("value", ob.City);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.Province");
					myXmlTextWriter.WriteAttributeString("value", ob.Province);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.Nation");
					myXmlTextWriter.WriteAttributeString("value", ob.Nation);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteStartElement("field");
					myXmlTextWriter.WriteAttributeString("name", "Tustena.ZipCode");
					myXmlTextWriter.WriteAttributeString("value", ob.Zip);
					myXmlTextWriter.WriteEndElement();
					myXmlTextWriter.WriteEndElement();
				}
				myXmlTextWriter.WriteEndElement();
				myXmlTextWriter.WriteEndElement();
				myXmlTextWriter.Flush();
				myXmlTextWriter.Close();
				ClientScript.RegisterStartupScript(this.GetType(), "sent",string.Format("<script>alert('{0}');</script>", Root.rm.GetString("Acttxt119")));
			}
			else
			{
				foreach (Object o in mailsdata)
				{
					NewMailingList.MailList ob = (NewMailingList.MailList) o;
					string tempMessage;
					fileName = ConfigSettings.MailSpoolPath + Guid.NewGuid().ToString() + ".spool";

					tempMessage=mailMessage.Replace("to@to.to",ob.Email);
					tempMessage=tempMessage.Replace("Tustena.RefId",ob.RefID);
					tempMessage=tempMessage.Replace("Tustena.CompanyName",ob.CompanyName);
					tempMessage=tempMessage.Replace("Tustena.Name",ob.Name);
					tempMessage=tempMessage.Replace("Tustena.Surname",ob.Surname);
					tempMessage=tempMessage.Replace("Tustena.Address",ob.Address);
					tempMessage=tempMessage.Replace("Tustena.City",ob.City);
					tempMessage=tempMessage.Replace("Tustena.Province",ob.Province);
					tempMessage=tempMessage.Replace("Tustena.Nation",ob.Nation);
					tempMessage=tempMessage.Replace("Tustena.ZipCode",ob.Zip);

					try
					{
						using(StreamWriter tw = new StreamWriter(fileName,false))
						{
							tw.Write(tempMessage);
							ClientScript.RegisterStartupScript(this.GetType(), "sent",string.Format("<script>alert('{0}');</script>", Root.rm.GetString("Acttxt119")));
						}
					}
					catch(Exception ex)
					{
						Context.Items["warning"]="Spooler write error!";
						G.SendError("[Tustena] Spooler write error!",ex.Message);
						return;
					}
				}
			}
		}

		private void CreateMailActivity()
		{
			{
				ActivityInsert ai = new ActivityInsert();
				string A="";
				string C="";
				string L="";

				switch(CrossWith.Text)
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

				if (ViewState["fromwelcome"]!=null)
					this.MailSubject.Text="[WELCOME EMAIL] "+this.MailSubject.Text;

				if(A.Length>0 || C.Length>0 || L.Length>0)
					ai.InsertActivity("5", "", UC.UserId.ToString(), C, A, L, this.MailSubject.Text, "", UC.LTZ.ToUniversalTime(DateTime.Now), UC,1);
			}
		}
	}
}
