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
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Pop3;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.MailingList.webmail;
using Digita.Tustena.WorkingCRM;

namespace Digita.Tustena.WebMail
{
	public partial class webmail : G
	{



		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				if (!Page.IsPostBack)
				{
					PreviousMessPage.Text =Root.rm.GetString("PreviousTxt");
					NextMessPage.Text =Root.rm.GetString("NextTxt");

					MailIn.Text =Root.rm.GetString("Mailtxt9");
					ReadMail.Text =Root.rm.GetString("Mailtxt11");
					NewMail.Text =Root.rm.GetString("Mailtxt12");
					FillMsgIn(false,true);
					dgmessages.Visible = false;
				}

			}
		}

		private void FillMsgIn(bool force, bool firstLoad)
		{
			try
			{
				long msgLoop = 0;
				DataRow dtpop3 = DatabaseConnection.CreateDataset("SELECT MAILSERVER,MAILUSER,MAILPASSWORD FROM ACCOUNT WHERE UID=" + UC.UserId).Tables[0].Rows[0];
				if (dtpop3[1].ToString().Length > 0 && dtpop3[2].ToString().Length > 0 && dtpop3[0].ToString().Length > 0)
				{
					string pop3=dtpop3[0].ToString();
					bool secure = false;
					if(pop3.StartsWith("!"))
					{
						pop3=pop3.Substring(1);
						secure=true;
					}

					using (Pop3Client email = new Pop3Client(dtpop3[1].ToString(), dtpop3[2].ToString(), pop3,secure))
					{
						email.OpenInbox();

						Message[] msgs = new Message[email.MessageCount];

						if (msgs.Length > 0)
						{
							if ((Session[UC.UserName] == null || ((Session[UC.UserName] is MessageCache) && email.IsNewMessages(((MessageCache) Session[UC.UserName]).LastSerial))) || force)
							{
								msgLoop = msgs.Length - 1;
								long posid = 1;
								if (msgs.Length > UC.PagingSize)
								{
									email.PositionID = msgs.Length - UC.PagingSize;
									posid = msgs.Length - UC.PagingSize;
									msgLoop = msgs.Length - email.PositionID - 1;
									if (msgLoop < 0) msgLoop = 0;
								}
								else
								{
									email.PositionID = 0;
									msgLoop = 0;
								}
								long filledTo = msgLoop;
								while (email.NextEmail(true)) // solo top
								{
									Message msg = new Message();

									try
									{
										msg.From = (email.From.Length > 0) ? email.From : " ";
									}
									catch
									{
										msg.From = " ";
									}

									try
									{
										msg.To = (email.To.Length > 0) ? email.To : " ";
									}
									catch
									{
										msg.To = " ";
									}

									try
									{
										msg.Subject = (email.Subject.Length > 0) ? email.Subject : " ";
									}
									catch
									{
										msg.Subject = " ";
									}

									try
									{
										msg.Size = email.Size;
									}
									catch
									{
										msg.Size = 0;
									}

									try
									{
										msg.MessageID = (email.MessageID != null) ? Regex.Replace(email.MessageID.Trim('<', '>'), @"[^a-zA-Z0-9_]", "_") : "";
									}
									catch
									{
										msg.MessageID = String.Empty;
									}

									msg.MsgID = email.PositionID;
									msg.MsgSerial = email.GetSerial;
									try
									{
										msg.MsgDate =UC.LTZ.ToLocalTime(email.Date);
									}
									catch
									{
										msg.MsgDate = new DateTime(1980, 1, 1);
									}

									if (msgs.Length > UC.PagingSize)
										msgs[msgLoop--] = msg;
									else
										msgs[msgLoop++] = msg;

								}

								MessageCache MCache = new MessageCache(msgs, email.LastSerial);
								MCache.LastPosition = posid;
								MCache.FilledTo = filledTo;

								if (msgs.Length < UC.PagingSize)
									Array.Reverse(msgs);

								Session.Add(UC.UserName, MCache);
							}
							else
							{
								msgs = ((MessageCache) Session[UC.UserName]).Messages;
								msgLoop = ((MessageCache) Session[UC.UserName]).LastPosition;
							}
						}
						else
						{
							lblError.Text = "<br><center>" +Root.rm.GetString("WebMLtxt12") + "</center>";
						}

						email.CloseConnection();
						if (msgs.Length > 0)
						{
							if (msgs.Length > UC.PagingSize)
							{
								if(firstLoad)
									msgLoop=-1;
								Message[] msgs2 = new Message[UC.PagingSize];
								Array.Copy(msgs, msgLoop + 1, msgs2, 0, UC.PagingSize);
								tblpaging.Visible = true;
								MessPageID.Text = "0";
								PreviousMessPage.Enabled = false;
								NextMessPage.Enabled = true;
								Repeatermsg.DataSource = msgs2;
								Repeatermsg.DataBind();
							}
							else
							{
								tblpaging.Visible = false;
								Repeatermsg.DataSource = msgs;
								Repeatermsg.DataBind();
							}


							Lblmsginfo.Text = "<b>" + dtpop3[1].ToString() + "</b>&nbsp;" + String.Format(Root.rm.GetString("Mailtxt8"), msgs.Length.ToString(), Convert.ToString(email.TotalBytes/1024));

							if (msgs.Length == 0)
								dgmessages.Visible = false;

							messages.Visible = true;
						}
					}
				}
				else
				{
					lblError.Text = "<br><br><center><div style=\"color:red;font-size:12px;width:600px;\">" +Root.rm.GetString("WebMLtxt8") + "</div></center>";
				}
			}

			catch (Pop3PasswordException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));

			}
			catch (Pop3LoginException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt17"));

			}
			catch (Pop3ConnectException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));
			}


			this.dgmessages.Visible = false;
			Repeatermsg.Visible = true;
		}


		protected String FormattaBody(String body, String contentType)
		{
			if (contentType.Equals("P"))
			{
				body = body.Replace("\r", "").Replace("\n", "<br>");
				return body;
			}
			return body;
		}

		protected void DeleteClick(string msgID, string msgMessageID)
		{


			string PathTemplate;
			PathTemplate = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{1}{0}{1}mails", UC.UserId, Path.DirectorySeparatorChar));
			string NameOfFile = PathTemplate + Path.DirectorySeparatorChar + msgMessageID + ".tmsg";
			if (File.Exists(NameOfFile))
			{
				Message msg = new Message();
				FileStream newfile = new FileStream(NameOfFile, FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter();
				msg = (Message) bf.Deserialize(newfile);
				newfile.Close();

				foreach (MessageAttach ma in msg.Attachment)
				{
					string attpath;
					attpath = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{2}{0}{2}{1}", UC.UserId, ma.Filename, Path.DirectorySeparatorChar));
					if (File.Exists(attpath))
					{
						File.Delete(attpath);
					}
				}
				File.Delete(NameOfFile);

			}


			DataRow dtpop3 = DatabaseConnection.CreateDataset("SELECT MAILSERVER,MAILUSER,MAILPASSWORD FROM ACCOUNT WHERE UID=" + UC.UserId).Tables[0].Rows[0];
			string pop3=dtpop3[0].ToString();
			bool secure = false;
			if(pop3.StartsWith("!"))
			{
				pop3=pop3.Substring(1);
				secure=true;
			}
			using (Pop3Client email = new Pop3Client(dtpop3[1].ToString(), dtpop3[2].ToString(), pop3,secure))
			{
				email.OpenInbox();
				email.NextEmail(Convert.ToInt32(msgID));
				bool result = email.DeleteEmail();
				if (!result)
				{
					lblError.Text =Root.rm.GetString("Mailtxt26");
				}
				email.Quit();
				email.CloseConnection();
				FillMsgIn(true,false);
				dgmessages.Visible = false;
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
			this.Repeatermsg.ItemCommand += new RepeaterCommandEventHandler(Repeatermsg_ItemCommand);
			this.Repeatermsg.ItemDataBound += new RepeaterItemEventHandler(Repeatermsg_ItemDataBound);
			this.MailIn.Click += new EventHandler(MailIn_Click);
			this.ReadMail.Click += new EventHandler(ReadMail_Click);
			this.NewMail.Click += new EventHandler(NewMail_Click);
			this.dgmessages.ItemDataBound += new DataGridItemEventHandler(dgmessages_ItemDataBound);
			this.dgmessages.ItemCommand += new DataGridCommandEventHandler(dgmessages_ItemCommand);
			this.PreviousMessPage.Click += new EventHandler(PreviousMessPage_Click);
			this.NextMessPage.Click += new EventHandler(NextMessPage_Click);

		}

		#endregion

		private void Repeatermsg_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DeleteMail":
					try
					{
						DeleteClick(((Label) e.Item.FindControl("msgid")).Text, ((Label) e.Item.FindControl("msgMessageID")).Text);
					}catch{}
					break;
				case "OpenBody":

					this.dgmessages.Visible = true;
					Repeatermsg.Visible = false;



					long msgid = long.Parse(((Label) e.Item.FindControl("msgid")).Text);
					string MessageID = String.Empty;

					Message[] msgs = new Message[1];
					ArrayList msgatt = new ArrayList();

					string msgMessageID = ((Label) e.Item.FindControl("msgMessageID")).Text;
					string PathTemplate;
					PathTemplate = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{1}{0}{1}mails", UC.UserId,  Path.DirectorySeparatorChar));
					FileFunctions.CheckDir(PathTemplate, true);

					string NameOfFile = PathTemplate + Path.DirectorySeparatorChar + msgMessageID + ".tmsg";
					if (!File.Exists(NameOfFile))
					{
						DataRow dtPop3 = DatabaseConnection.CreateDataset("SELECT MAILSERVER,MAILUSER,MAILPASSWORD FROM ACCOUNT WHERE UID=" + UC.UserId).Tables[0].Rows[0];
						string pop3=dtPop3[0].ToString();
						bool secure = false;
						if(pop3.StartsWith("!"))
						{
							pop3=pop3.Substring(1);
							secure=true;
						}
						using (Pop3Client email = new Pop3Client(dtPop3[1].ToString(), dtPop3[2].ToString(), pop3, secure))
						{
							try
							{
								email.OpenInbox();
								email.AttachmentsPath = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{1}{0}{1}", UC.UserId,  Path.DirectorySeparatorChar));
								email.NextEmail(msgid);

								try
								{
									MessageID = (email.MessageID != null) ? Regex.Replace(email.MessageID.Trim('<', '>'), @"[^a-zA-Z0-9_]", "_") : "";
								}
								catch
								{
									MessageID = String.Empty;
								}

								Message msg = new Message();
                                msg.From = (email.From!=null && email.From.Length > 0) ? email.From : Root.rm.GetString("WebMLtxt11");
                                msg.To = (email.To != null && email.To.Length > 0) ? email.To : Root.rm.GetString("WebMLtxt11");
                                msg.Subject = (email.Subject != null && email.Subject.Length > 0) ? email.Subject : Root.rm.GetString("WebMLtxt11");
								msg.MsgID = msgid;

								if (email.IsMultipart)
								{
									IEnumerator enumerator = email.MultipartEnumerator;
									Queue attach = new Queue();
									string bodyhtml = null;
									string bodyplain = null;
									while (enumerator.MoveNext())
									{
										Pop3Component multipart = (Pop3Component) enumerator.Current;
										if (multipart.IsBody)
										{
											if (multipart != null && multipart.ContentType.ToLower().IndexOf("text/html") > -1)
											{
												bodyhtml = multipart.Data;
											}
											else if (multipart != null && multipart.ContentType.ToLower().IndexOf("text/plain") > -1)
											{
												bodyplain = multipart.Data; //multipart.Data.Replace("\r","").Replace("\n","<br>");
											}
										}
										else
										{
											if (multipart.ContentID != null && multipart.FilePath != null)
											{
												attach.Enqueue(multipart.ContentID);
												attach.Enqueue("/mailinglist/webmail/mailredir.aspx?render=no&att=1&img=" + multipart.FilePath.Replace(email.AttachmentsPath, ""));
											}
											else if (multipart.FilePath != null)
											{
												msgatt.Add(multipart.FilePath);
											}
										}

									}

									if (bodyhtml != null)
									{
										while (attach.Count > 0)
											bodyhtml = Regex.Replace(bodyhtml, @"(?<=src=[""']?)cid:" + ((string) attach.Dequeue()).Trim('<', '>') + @"(?=[""']?)", (string) attach.Dequeue(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
										bodyhtml = Regex.Replace(bodyhtml, @"</?(html|body|link|meta)[\s\S]*?>", "", RegexOptions.IgnoreCase | RegexOptions.Multiline);
										bodyhtml = Regex.Replace(bodyhtml, @"<title[\s\S]*?</title[\s\S]*?>", "<body_not_allowed", RegexOptions.IgnoreCase | RegexOptions.Multiline);
										bodyhtml = Regex.Replace(bodyhtml, @"<(style|script|head)[\s\S]*?>", "<!--", RegexOptions.IgnoreCase | RegexOptions.Multiline);
										bodyhtml = Regex.Replace(bodyhtml, @"</(style|script|head)[\s\S]*?>", "-->", RegexOptions.IgnoreCase | RegexOptions.Multiline);
										bodyhtml = Regex.Replace(bodyhtml, @"(?<=<a)[ ](?=[\s\S]*?>)", " target=_blank ", RegexOptions.IgnoreCase | RegexOptions.Multiline);

									}

									if (bodyhtml != null)
									{
										msg.Body = bodyhtml;
									}
									else if (bodyplain != null)
									{
										msg.Body = bodyplain.Replace("\r", "").Replace("\n", "<br>");
									}
								}
								else
								{
									if (email.IsHTML)
									{
										if (email.ContentTransferEncoding != null && email.ContentTransferEncoding.ToLower().Equals("quoted-printable"))
											msg.Body = Pop3Utils.FromQuotedPrintable(email.Body);
										else
											msg.Body = email.Body;

										msg.ContentType = "H";
									}
									else
									{
										msg.ContentType = "P";
										try
										{
											msg.Body = email.Body.Replace("\r", "").Replace("\n", "<br>");
										}
										catch
										{
											msg.Body = String.Empty;
										}
									}
								}
								email.CloseConnection();

								if (msgatt.Count > 0)
								{
									MessageAttach[] attach = new MessageAttach[msgatt.Count];
									for (int i = 0; i < msgatt.Count; i++)
									{
										MessageAttach ma = new MessageAttach();
										ma.Link = msgatt[i].ToString();
										ma.Filename = Path.GetFileName(msgatt[i].ToString());
										attach[i] = ma;
									}
									msg.Attachment = attach;
								}
								msgs[0] = msg;

								dgmessages.DataSource = msgs;
								dgmessages.DataBind();
								tblpaging.Visible = false;

								if (MessageID.Length > 1)
								{
									PathTemplate = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{1}{0}{1}mails", UC.UserId, Path.DirectorySeparatorChar));
									NameOfFile = PathTemplate + Path.DirectorySeparatorChar + MessageID + ".tmsg";

									FileStream newfile = new FileStream(NameOfFile, FileMode.Create);
									BinaryFormatter bf = new BinaryFormatter();
									bf.Serialize(newfile, msg);
									newfile.Close();
								}
							}
							catch (Pop3PasswordException)
							{
								Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));
							}
							catch (Pop3LoginException)
							{
								Context.Items.Add("warning",Root.rm.GetString("Mailtxt17"));
							}
							catch (Pop3ConnectException)
							{
								Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));
							}
							catch (Pop3DecodeException)
							{
								Context.Items.Add("warning",Root.rm.GetString("Mailtxt25"));
							}
							catch (Pop3MissingBoundaryException)
							{
								Context.Items.Add("warning",Root.rm.GetString("Mailtxt25"));
							}

						}
					}
					else
					{
						Message msg = new Message();
						FileStream newfile = new FileStream(NameOfFile, FileMode.Open);
						BinaryFormatter bf = new BinaryFormatter();
						msg = (Message) bf.Deserialize(newfile);
						newfile.Close();

						msgs[0] = msg;

						dgmessages.DataSource = msgs;
						dgmessages.DataBind();
						tblpaging.Visible = false;
					}
					break;
			}
		}

		private void MailIn_Click(object sender, EventArgs e)
		{
			FillMsgIn(false,true);
		}

		private void ReadMail_Click(object sender, EventArgs e)
		{
			FillMsgIn(true,false);
		}

		private void dgmessages_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					Label mailbody = (Label) e.Item.FindControl("mailbody");
					string body = (string) DataBinder.Eval(e.Item.DataItem, "body");


					mailbody.Text = body;//FormattaBody(body,ContentType);
					mailbody.Style.Add("word-wrap", "break-word");
					mailbody.Style.Add("width", "98%");

					LinkButton CreateActivity = (LinkButton) e.Item.FindControl("CreateActivity");
					CreateActivity.Text =Root.rm.GetString("WebMLtxt5");

					RadioButtonList CrossWith = (RadioButtonList) e.Item.FindControl("CrossWith");
					ListItem li = new ListItem(Root.rm.GetString("Mailtxt13"), "0");
					CrossWith.Items.Add(li);
					li = new ListItem(Root.rm.GetString("Mailtxt14"), "1");
					CrossWith.Items.Add(li);
                    Modules M = new Modules();
                    M.ActiveModule = UC.Modules;
                    if (M.IsModule(ActiveModules.Lead))
                    {
                        li = new ListItem(Root.rm.GetString("Mailtxt15"), "2");
                        CrossWith.Items.Add(li);
                    }
					CrossWith.RepeatDirection = RepeatDirection.Horizontal;

					try
					{
						MessageAttach[] att = (MessageAttach[]) DataBinder.Eval(e.Item.DataItem, "attachment");
						if (att.Length > 0)
						{
							Repeater RepAttachment = (Repeater) e.Item.FindControl("RepAttachment");
							RepAttachment.DataSource = att;
							RepAttachment.DataBind();
						}
					}
					catch
					{
					}

					CheckBox SaveEml = (CheckBox) e.Item.FindControl("SaveEml");
					SaveEml.Text =Root.rm.GetString("WebMLtxt9");


					break;
			}
		}

		private void NewMail_Click(object sender, EventArgs e)
		{
			SendNewMail.Visible=true;
			messages.Visible = false;
		}

		private void Repeatermsg_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton DeleteMail = (LinkButton) e.Item.FindControl("DeleteMail");
					DeleteMail.Text =Root.rm.GetString("Delete");
					Label MsgSize = (Label) e.Item.FindControl("MsgSize");
					int size = Convert.ToInt32(Server.HtmlEncode(Convert.ToString(DataBinder.Eval(e.Item.DataItem, "size"))))/1024;
					MsgSize.Text = size.ToString() + " Kb";

					LinkButton OpenBody = (LinkButton) e.Item.FindControl("OpenBody");
					if (OpenBody.Text.Length > 50)
					{
						Regex r = new Regex(@"(?s)\b.{1,47}\b");
						OpenBody.ToolTip = OpenBody.Text;
						OpenBody.Text = r.Match(OpenBody.Text) + "&hellip;";
						OpenBody.Attributes.Add("style", "cursor:help;");
					}

					Label msgFrom = (Label)e.Item.FindControl("msgFrom");
					if (msgFrom.Text.Length > 33)
					{
						Regex r = new Regex(@"(?s)\b.{1,30}\b");
						msgFrom.ToolTip = msgFrom.Text;
						msgFrom.Text = r.Match(msgFrom.Text) + "&hellip;";
						msgFrom.Attributes.Add("style", "cursor:help;");
					}

					if (size >= 2048)
					{
						OpenBody.Enabled = false;
						OpenBody.ToolTip =Root.rm.GetString("WebMLtxt13");
					}
					break;
			}
		}

		private void dgmessages_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "CreateActivity":
					ActivityInsert ai = new ActivityInsert();
					string mailbody = HttpUtility.HtmlDecode(Pop3Utils.trimhtml(((Label) e.Item.FindControl("mailbody")).Text));
					string mailsubject = HttpUtility.HtmlDecode(Pop3Utils.trimhtml(((Label) e.Item.FindControl("mailsubject")).Text));

					TextBox MailAddressToID = (TextBox) e.Item.FindControl("MailAddressToID");
					RadioButtonList CrossWith = (RadioButtonList) e.Item.FindControl("CrossWith");

					if (MailAddressToID.Text.Length > 0)
					{
						string A = String.Empty;
						string C = String.Empty;
						string L = String.Empty;

						switch (CrossWith.SelectedValue)
						{
							case "0":
								A = MailAddressToID.Text;
								C = String.Empty;
								L = String.Empty;
								break;
							case "1":
								C = MailAddressToID.Text;
								A = String.Empty;
								L = String.Empty;
								break;
							case "2":
								L = MailAddressToID.Text;
								A = String.Empty;
								C = String.Empty;
								break;
						}
						if (A.Length > 0 || C.Length > 0 || L.Length > 0)
						{
							CheckBox SaveEml = (CheckBox) e.Item.FindControl("SaveEml");
							long docid = 0;
							if (SaveEml.Checked)
							{
								DataRow dtpop3 = DatabaseConnection.CreateDataset("SELECT MAILSERVER,MAILUSER,MAILPASSWORD FROM ACCOUNT WHERE UID=" + UC.UserId).Tables[0].Rows[0];
								string pop3=dtpop3[0].ToString();
								bool secure = false;
								if(pop3.StartsWith("!"))
								{
									pop3=pop3.Substring(1);
									secure=true;
								}
								using (Pop3Client email = new Pop3Client(dtpop3[1].ToString(), dtpop3[2].ToString(), pop3, secure))
								{
									email.OpenInbox();
									email.AttachmentsPath = Path.Combine(ConfigSettings.DataStoragePath, String.Format("webmail{1}{0}{1}", UC.UserId, Path.DirectorySeparatorChar));
									email.NextEmail(Convert.ToInt64(((Label) e.Item.FindControl("MailMsgId")).Text));
									string eml = email.Original;

									Guid g = Guid.NewGuid();
									using (DigiDapter dg = new DigiDapter())
									{
										dg.Add("Guid", g);
										dg.Add("OwnerID", UC.UserId);
										dg.Add("IsReview", 0);

										dg.Add("ReviewNumber", 0);
										dg.Add("HaveRevision", false);
										dg.Add("CreatedDate", DateTime.UtcNow);
										dg.Add("CreatedByID", UC.UserId);

										dg.Add("Filename", email.Subject + ".eml");
										dg.Add("size", eml.Length);

										dg.Add("Description", email.Subject);
										dg.Add("LastModifiedDate", DateTime.UtcNow);
										dg.Add("LastModifiedByID", UC.UserId);

										dg.Add("TYPE", 0);

										dg.Add("Groups", "|" + UC.UserGroupId.ToString() + "|");
										object NewID = dg.Execute("FILEMANAGER", DigiDapter.Identities.Identity);
										docid = Convert.ToInt64(NewID);

									}
string PathTemplate;
									PathTemplate = ConfigSettings.DataStoragePath;
									string NameOfFile = PathTemplate + Path.DirectorySeparatorChar + g.ToString() + ".eml";

									FileStream newfile = new FileStream(NameOfFile, FileMode.Create);
									BinaryWriter wrtfile = new BinaryWriter(newfile);

									try
									{
										wrtfile.Write(eml);
									}
									finally
									{
										wrtfile.Close();
										newfile.Close();
										email.CloseConnection();
									}
								}
							}

							ai.InsertActivity("5", "", UC.UserId.ToString(), C, A, L, mailsubject, mailbody, DateTime.UtcNow, UC, 1, false, docid,0,0);
							ClientScript.RegisterStartupScript(this.GetType(), "accretae", "<script>alert('" +Root.rm.GetString("WebMLtxt6") + "');</script>");
						}
						else
						{
							ClientScript.RegisterStartupScript(this.GetType(), "accretae", "<script>alert('" +Root.rm.GetString("WebMLtxt7").Replace("'", "\'") + "');</script>");
						}


					}
					else
					{
						ClientScript.RegisterStartupScript(this.GetType(), "accretae", "<script>alert('" +Root.rm.GetString("WebMLtxt7").Replace("'", "\'") + "');</script>");
					}

					break;
			}
		}

		private void PreviousMessPage_Click(object sender, EventArgs e)
		{
			Message[] msgs = new Message[((MessageCache) Session[UC.UserName]).Messages.Length];
			msgs = ((MessageCache) Session[UC.UserName]).Messages;
			Message[] msgs2 = new Message[UC.PagingSize];
			int pageid = Convert.ToInt32(MessPageID.Text);
			pageid = pageid - UC.PagingSize;
			Array.Copy(msgs, pageid, msgs2, 0, UC.PagingSize);
			MessPageID.Text = pageid.ToString();
			if ((pageid - UC.PagingSize) < 0)
				PreviousMessPage.Enabled = false;
			else
				PreviousMessPage.Enabled = true;
			Repeatermsg.DataSource = msgs2;
			Repeatermsg.DataBind();

			NextMessPage.Enabled = true;
		}

		private void NextMessPage_Click(object sender, EventArgs e)
		{
			MessageCache msg = (MessageCache) Session[UC.UserName];
			long pageid = Convert.ToInt32(MessPageID.Text);
			pageid = pageid + UC.PagingSize;
			long tocopy = UC.PagingSize;

			if (msg.LastPosition > 0 && msg.FilledTo <= pageid)
				AddMsgToCache();

			Message[] msgs = new Message[((MessageCache) Session[UC.UserName]).Messages.Length];
			msgs = ((MessageCache) Session[UC.UserName]).Messages;

			Message[] msgs2 = new Message[UC.PagingSize];

			if ((pageid + UC.PagingSize) >= msgs.Length)
			{
				tocopy = msgs.Length - pageid;
				msgs2 = new Message[tocopy];
			}

			Array.Copy(msgs, pageid, msgs2, 0, tocopy);
			MessPageID.Text = pageid.ToString();
			if ((pageid + UC.PagingSize) >= msgs.Length)
				NextMessPage.Enabled = false;
			else
				NextMessPage.Enabled = true;
			Repeatermsg.DataSource = msgs2;
			Repeatermsg.DataBind();

			PreviousMessPage.Enabled = true;
		}

		private long AddMsgToCache()
		{
			try
			{
				long position = 0;
				long filledTo = 0;
				long newFilledTo = 0;
				long msgLoop = 0;
				DataRow dtPop3 = DatabaseConnection.CreateDataset("SELECT MAILSERVER,MAILUSER,MAILPASSWORD FROM ACCOUNT WHERE UID=" + UC.UserId).Tables[0].Rows[0];
				if (dtPop3[1].ToString().Length > 0 && dtPop3[2].ToString().Length > 0 && dtPop3[0].ToString().Length > 0)
				{
					string pop3=dtPop3[0].ToString();
					bool secure = false;
					if(pop3.StartsWith("!"))
					{
						pop3=pop3.Substring(1);
						secure=true;
					}
					using (Pop3Client email = new Pop3Client(dtPop3[1].ToString(), dtPop3[2].ToString(), pop3, secure))
					{
						email.OpenInbox();
						Message[] msgs = new Message[email.MessageCount];

						if (msgs.Length > 0)
						{
							long newPos = 0;
							try
							{
								position = ((MessageCache) Session[UC.UserName]).LastPosition;
								filledTo = ((MessageCache) Session[UC.UserName]).FilledTo;
								msgs = ((MessageCache) Session[UC.UserName]).Messages;
								newPos = position - UC.PagingSize;
								if ((newPos) > 0)
								{
									email.PositionID = newPos;
									msgLoop = filledTo + UC.PagingSize;
								}
								else
								{
									email.PositionID = 0;
									newPos = 0;
									msgLoop = msgs.Length - 1;
								}
							}
							catch
							{
								email.PositionID = 0;
								newPos = 0;
								msgLoop = msgs.Length - 1;
							}
							newFilledTo = msgLoop;
							Trace.Warn("position paging", position.ToString() + " " + UC.PagingSize.ToString());
							while (email.NextEmail(true) && msgLoop > filledTo && msgLoop >= 0) // solo top
							{

								Message msg = new Message();
								try
								{
									msg.From = (email.From.Length > 0) ? email.From : " ";
								}
								catch
								{
									msg.From = " ";
								}

								try
								{
									msg.To = (email.To.Length > 0) ? email.To : " ";
								}
								catch
								{
									msg.To = " ";
								}

								try
								{
									msg.Subject = (email.Subject.Length > 0) ? email.Subject : " ";
								}
								catch
								{
									msg.Subject = " ";
								}

								try
								{
									msg.Size = email.Size;
								}
								catch
								{
									msg.Size = 0;
								}

								try
								{
									msg.MessageID = (email.MessageID != null) ? Regex.Replace(email.MessageID.Trim('<', '>'), @"[^a-zA-Z0-9_]", "_") : "";
								}
								catch
								{
									msg.MessageID = String.Empty;
								}

								msg.MsgID = email.PositionID;
								msg.MsgSerial = email.GetSerial;
								try
								{
									msg.MsgDate =UC.LTZ.ToLocalTime(email.Date);
								}
								catch
								{
									msg.MsgDate = DateTime.Now;
								}

								msgs[msgLoop--] = msg;

							}

							MessageCache MCache = new MessageCache(msgs, email.LastSerial);
							MCache.LastPosition = position - UC.PagingSize;
							MCache.FilledTo = newFilledTo;
							Session.Remove(UC.UserName);
							Session.Add(UC.UserName, MCache);

						}
						else
						{
							lblError.Text = "<br><center>" +Root.rm.GetString("WebMLtxt12") + "</center>";
						}

						email.CloseConnection();

					}
				}
				else
				{
					Context.Items.Add("warning",Root.rm.GetString("WebMLtxt8"));
				}
				return msgLoop + 1;
			}

			catch (Pop3PasswordException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));
				return 0;
			}
			catch (Pop3LoginException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt17"));
				return 0;
			}
			catch (Pop3ConnectException)
			{
				Context.Items.Add("warning",Root.rm.GetString("Mailtxt16"));
				return 0;
			}

		}
	}

}
