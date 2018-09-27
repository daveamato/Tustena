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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class SMSList : G
	{
		protected HtmlTable RisearchAdvanced;
		protected HtmlTable PreviewList;
		protected HtmlTable NewML;
		protected HtmlTable NewMLTable;
		protected HtmlInputHidden ListFixedParams;
		protected ListBox MLFixedMails;
		protected ListBox SearchResult;
		protected ListBox MLFill;
		protected ListBox MLFill2;
		protected ListBox MLFill3;
		protected ListBox MLFill4;
		protected ListBox MLFillRemoved;
		protected ListBox MLFill2Removed;
		protected ListBox MLFill3Removed;
		protected ListBox MLFill4Removed;

		protected LinkButton RemoveMLFill;
		protected LinkButton RemoveMLFill2;
		protected LinkButton RemoveMLFill3;
		protected LinkButton RemoveMLFill4;

		protected Repeater MailingListRep;
		protected RepeaterPaging MailListPaging;
		protected Label MailToSend;
		protected Label MailToSendID;
		protected Literal QueryToSave;
		protected TextBox NewMLTitle;
		protected Literal MLID;
		protected LinkButton MoveOne;
		protected LinkButton MoveAll;
		protected LinkButton SendML;
		protected LinkButton SaveML;
		protected LinkButton NewMLSubmit;
		protected TextBox SenderTextBox;
		protected LinkButton SearchAdvanced;
		protected HtmlContainerControl spaninvio;
		protected LinkButton BtnNewML;
		protected TextBox TextboxSearchCompany;
		protected TextBox TextboxSearchCompanyID;
		protected TextBox TextboxSearchContactID;
		protected TextBox TextboxSearchLeadID;
		protected DropDownList SAdvanced_Opportunity;
		protected DropDownList SAdvancedLead_Opportunity;
		protected HtmlContainerControl MailPreview;
		protected LinkButton Verifymail;
		protected LinkButton BackToSendMail;

		protected TextBox TxtMessage;
		protected Label MsgLabel;

		private ArrayList mailsdata = new ArrayList();
		private string smsFromAdress;
		private string smsSubject;
		private string smsBody;


		public void Page_Load(object sender, EventArgs e)
		{
			PreviewList.Visible = false;
			TxtMessage.Attributes.Add("onChange", "message_onChange(this);");
			TxtMessage.Attributes.Add("onKeyUp", "message_onChange(this);");
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				BtnNewML.Text =Root.rm.GetString("MLtxt22");
				Verifymail.Text =Root.rm.GetString("Verify").ToUpper();
				NewMLSubmit.Visible = true;
				PreviewList.Visible = false;

				TextboxSearchCompanyID.Attributes.Add("onchange", "AddFixedParams('TextboxSearchCompany','TextboxSearchCompanyID','A')");
				TextboxSearchContactID.Attributes.Add("onchange", "AddFixedParams('TextboxSearchContact','TextboxSearchContactID','C')");
				TextboxSearchLeadID.Attributes.Add("onchange", "AddFixedParams('TextboxSearchLead','TextboxSearchLeadID','L')");

				if (!Page.IsPostBack)
				{
					SearchAdvanced.Text =Root.rm.GetString("MLtxt8");
					SaveML.Text =Root.rm.GetString("MLtxt9");
					SendML.Text =Root.rm.GetString("MLtxt12");
					BackToSendMail.Visible = false;
					if (Session["MailToSend"] != null)
					{
						MailToSend.Text =Root.rm.GetString("MLtxt1") + " <b>" + Session["MailToSend"].ToString() + "</b>";
						MailToSendID.Text = Session["MailToSendID"].ToString();
						Session["MailToSend"] = null;
						Session["MailToSendID"] = null;
						SenderTextBox.Text = UC.MailingAddress;
					}

					NewML.Visible = false;
					RisearchAdvanced.Visible = false;
					FillMailingRepeater();
					PrepareAdvanced();
					MLID.Text = "-1";

					RemoveMLFill.Text =Root.rm.GetString("Delete");
					RemoveMLFill2.Text =Root.rm.GetString("Delete");
					RemoveMLFill3.Text =Root.rm.GetString("Delete");
					RemoveMLFill4.Text =Root.rm.GetString("Delete");

				}
			}
		}

		public void MailingListRepItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					LinkButton SendMail = (LinkButton) e.Item.FindControl("SendMail");
					SendMail.Text =Root.rm.GetString("MLtxt0");

					LinkButton ModifyMail = (LinkButton) e.Item.FindControl("ModifyMail");
					ModifyMail.Text =Root.rm.GetString("Modify");


					LinkButton DeleteMail = (LinkButton) e.Item.FindControl("DeleteMail");
					DeleteMail.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("MLtxt3") + "');");
					DeleteMail.Text =Root.rm.GetString("MLtxt2");

					if (MailToSend.Text.Length > 0)
					{
						SendMail.Visible = true;
						ModifyMail.Visible = false;
						DeleteMail.Visible = false;
					}
					else
					{
						SendMail.Visible = false;
						ModifyMail.Visible = true;
						DeleteMail.Visible = true;
					}

					break;
			}
		}

		public void MailingListRepCommand(Object sender, RepeaterCommandEventArgs e)
		{
			Trace.Warn(e.CommandName);
			switch (e.CommandName)
			{
				case "SendMail":
					MLID.Text = ((Literal) e.Item.FindControl("MLid")).Text;
					DataTable dt = DatabaseConnection.CreateDataset("SELECT ID,QUERY FROM ML_DESCRIPTION WHERE ID=" + ((Literal) e.Item.FindControl("MLid")).Text).Tables[0];
					DataSet DSFillML;

					try
					{
						string companyQuery = GetCompanyQuery(int.Parse(dt.Rows[0][0].ToString()));
						Trace.Warn("companyquery", companyQuery);
						DSFillML = DatabaseConnection.CreateDataset(companyQuery);
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFill.DataTextField = "CompanyName";
							MLFill.DataValueField = "IDMAIL";
							MLFill.DataSource = DSFillML;
							MLFill.DataBind();
						}
						DSFillML = DatabaseConnection.CreateDataset(companyQuery.Replace("NOT EXIST", "EXIST"));
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFillRemoved.DataTextField = "CompanyName";
							MLFillRemoved.DataValueField = "IDMAIL";
							MLFillRemoved.DataSource = DSFillML;
							MLFillRemoved.DataBind();
						}
					}
					catch
					{
					}

					try
					{
						string contactquery = GetContactQuery(int.Parse(dt.Rows[0][0].ToString()));
						DSFillML = DatabaseConnection.CreateDataset(contactquery);
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFill2.DataTextField = "contact";
							MLFill2.DataValueField = "IDMAIL";
							MLFill2.DataSource = DSFillML;
							MLFill2.DataBind();
						}
						DSFillML = DatabaseConnection.CreateDataset(contactquery.Replace("NOT EXIST", "EXIST"));
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFill2Removed.DataTextField = "contact";
							MLFill2Removed.DataValueField = "IDMAIL";
							MLFill2Removed.DataSource = DSFillML;
							MLFill2Removed.DataBind();
						}
					}
					catch
					{
					}

					try
					{
						string leadquery = GetLeadQuery(int.Parse(dt.Rows[0][0].ToString()));
						DSFillML = DatabaseConnection.CreateDataset(leadquery);
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFill3.DataTextField = "contact";
							MLFill3.DataValueField = "IDMAIL";
							MLFill3.DataSource = DSFillML;
							MLFill3.DataBind();
						}
						DSFillML = DatabaseConnection.CreateDataset(leadquery.Replace("NOT EXIST", "EXIST"));
						if (DSFillML.Tables[0].Rows.Count > 0)
						{
							MLFill3Removed.DataTextField = "contact";
							MLFill3Removed.DataValueField = "IDMAIL";
							MLFill3Removed.DataSource = DSFillML;
							MLFill3Removed.DataBind();
						}
					}
					catch
					{
					}

					DataTable dtFix = DatabaseConnection.CreateDataset("SELECT * FROM ML_FIXEDPARAMS WHERE IDMAILINGLIST=" + dt.Rows[0][0].ToString()).Tables[0];
					if (dtFix.Rows.Count > 0)
					{
						DataRow drFix = dtFix.Rows[0];
						if (drFix["company"].ToString().Length > 0)
						{
							string query = String.Empty;
							string[] xs = drFix["company"].ToString().Split('|');
							foreach (string s in xs)
							{
								if (s.Length > 0)
									query += "ID=" + s + " OR ";
							}
							DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME,PHONE FROM BASE_COMPANIES WHERE (" + query.Substring(0, query.Length - 3) + ") AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_COMPANIES.ID = ML_REMOVEDFROM.IDREF AND TYPE=0 AND IDML=" + int.Parse(MLID.Text + "))")).Tables[0];
							foreach (DataRow dr in dtaz.Rows)
							{
								MLFill4.Items.Add(new ListItem(dr[1].ToString(), "A" + dr[0].ToString() + "|" + dr[2].ToString() + "|" + dr[1].ToString()));
							}
						}
						if (drFix["contact"].ToString().Length > 0)
						{
							string query = String.Empty;
							string[] xs = drFix["contact"].ToString().Split('|');
							foreach (string s in xs)
							{
								if (s.Length > 0)
									query += "ID=" + s + " OR ";
							}

							DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT,MOBILEPHONE_1 FROM BASE_CONTACTS WHERE (" + query.Substring(0, query.Length - 3) + ") AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_CONTACTS.ID = ML_REMOVEDFROM.IDREF AND TYPE=1 AND IDML=" + int.Parse(MLID.Text + "))")).Tables[0];
							foreach (DataRow dr in dtaz.Rows)
							{
								MLFill4.Items.Add(new ListItem(dr[1].ToString(), "C" + dr[0].ToString() + "|" + dr[2].ToString() + "|" + dr[1].ToString()));
							}
						}
						if (drFix["lead"].ToString().Length > 0)
						{
							string query = String.Empty;
							string[] xs = drFix["lead"].ToString().Split('|');
							foreach (string s in xs)
							{
								if (s.Length > 0)
									query += "ID=" + s + " OR ";
							}
							DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,ISNULL(COMPANYNAME,'')+' '+ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS LEAD,MOBILEPHONE FROM CRM_LEADS WHERE (" + query.Substring(0, query.Length - 3) + ") AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE CRM_LEADS.ID = ML_REMOVEDFROM.IDREF AND TYPE=2 AND IDML=" + int.Parse(MLID.Text + "))")).Tables[0];
							foreach (DataRow dr in dtaz.Rows)
							{
								MLFill4.Items.Add(new ListItem(dr[1].ToString(), "L" + dr[0].ToString() + "|" + dr[2].ToString() + "|" + dr[1].ToString()));
							}
						}
					}

					NewML.Visible = false;
					MailingListRep.Visible = false;
					RisearchAdvanced.Visible = false;
					NewMLTable.Visible = true;

					spaninvio.Visible = false;
					PreviewList.Visible = true;
					SaveML.Visible = false;

					MailPreview.Attributes.Add("onclick", "CreateBox('/mailinglist/mailpreview.aspx?render=no&id=" + MailToSendID.Text + "',event,600,500)");
					break;
				case "DeleteMail":
					delete(int.Parse(((Literal) e.Item.FindControl("MLid")).Text));
					break;
				case "ModifyMail":
					MLID.Text = ((Literal) e.Item.FindControl("MLid")).Text;
					NewMLTitle.Text = ((Literal) e.Item.FindControl("MLTitle")).Text;


					NewMLSubmit.Visible = false;
					PopulateML(int.Parse(((Literal) e.Item.FindControl("MLid")).Text));
					break;

			}
		}

		private void delete(int id)
		{
			DatabaseConnection.DoCommand("DELETE FROM ML_DESCRIPTION WHERE ID =" + id);


			NewML.Visible = false;
			RisearchAdvanced.Visible = false;
			FillMailingRepeater();
			PrepareAdvanced();
			MLID.Text = "-1";
		}


		private void FillMailingRepeater()
		{
			MailListPaging.PageSize = UC.PagingSize;
			MailListPaging.RepeaterObj = MailingListRep;
			MailListPaging.sqlRepeater = "SELECT ML_DESCRIPTION.ID,ML_DESCRIPTION.DESCRIPTION,(SELECT COUNT(*) FROM ML_LOG WHERE LISTID=ML_DESCRIPTION.ID) AS NSEND,(SELECT SUM(MAILNUMBER) FROM ML_LOG WHERE LISTID=ML_DESCRIPTION.ID) AS NMESS, ML_DESCRIPTION.SUBJECT FROM ML_DESCRIPTION WHERE SMS=1";
			MailListPaging.BuildGrid();
		}

		public void Btn_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "SearchAdvanced":
					int mlId = int.Parse(MLID.Text);
					FillRisearchAdvanced();
					SaveMailingList(mlId);
					PopulateML(mlId);
					break;
				case "NewMLSubmit":
					RisearchAdvanced.Visible = true;
					NewMLTable.Visible = true;
					NewMLSubmit.Visible = false;

					break;
				case "btnNewML":
					NewML.Visible = true;
					MailingListRep.Visible = false;
					spaninvio.Visible = false;
					SaveML.Visible = true;
					break;
				case "MoveOne":
					bool exists = false;
					foreach (ListItem i in MLFill.Items)
					{
						if (i.Value == SearchResult.SelectedItem.Value)
						{
							exists = true;
							break;
						}
					}
					if (!exists) MLFill.Items.Add(SearchResult.SelectedItem);
					break;
				case "MoveAll":
					foreach (ListItem s in SearchResult.Items)
					{
						bool existsAll = false;
						foreach (ListItem i in MLFill.Items)
						{
							if (i.Value == s.Value)
							{
								existsAll = true;
								break;
							}
						}
						if (!existsAll) MLFill.Items.Add(s);
					}
					break;
				case "DeleteOne":
					MLFill.Items.Remove(MLFill.SelectedItem);
					break;
				case "DeleteAll":
					MLFill.Items.Clear();
					break;
				case "SaveML":
					SaveMailingList(int.Parse(MLID.Text));
					break;
				case "SendML":
					ArrayList mails = new ArrayList();
					StringBuilder sendError = new StringBuilder();
					bool haveError = false;
					sendError.AppendFormat("<table width=\"50%\" cellspacing=0 cellpadding=0 class=normal><tr><td colspan=2><b>{0}</b></td></tr>",Root.rm.GetString("MLtxt33"));
					foreach (ListItem i in MLFill.Items)
					{
						SMSNumber mailstruct = new SMSNumber();
						string[] xmai = i.Value.Split('|');
						if (xmai[1].Length > 10)
						{
							mailstruct.Number = xmai[1];
							mailstruct.Contact = xmai[2];

							mails.Add(mailstruct);
						}
						else
						{
							haveError = true;
							sendError.AppendFormat("<tr><td>{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("SMStxt6"));
						}
					}
					foreach (ListItem i in MLFill2.Items)
					{
						SMSNumber mailstruct2 = new SMSNumber();
						string[] xmai = i.Value.Split('|');

						if (xmai[1].Length > 10)
						{
							mailstruct2.Number = xmai[1];
							mailstruct2.Contact = xmai[2];

							mails.Add(mailstruct2);
						}
						else
						{
							haveError = true;
							sendError.AppendFormat("<tr><td>{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("SMStxt6"));
						}
					}

					foreach (ListItem i in MLFill3.Items)
					{
						SMSNumber mailstruct3 = new SMSNumber();
						string[] xmai = i.Value.Split('|');
						if (xmai[1].Length > 10)
						{
							mailstruct3.Number = xmai[1];
							mailstruct3.Contact = xmai[2];

							mails.Add(mailstruct3);
						}
						else
						{
							haveError = true;
							sendError.AppendFormat("<tr><td>{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("SMStxt6"));
						}
					}

					foreach (ListItem i in MLFill4.Items)
					{
						SMSNumber mailstruct4 = new SMSNumber();

						string[] xmai = i.Value.Split('|');
						if (xmai[1].Length > 10)
						{
							mailstruct4.Number = xmai[1];
							mailstruct4.Contact = xmai[2];

							mails.Add(mailstruct4);
						}
						else
						{
							haveError = true;
							sendError.AppendFormat("<tr><td>{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("SMStxt6"));
						}
					}

					if (!haveError) sendError.AppendFormat("<tr><td colspan=2 style=\"color:red\">{0}</td></tr>",Root.rm.GetString("MLtxt38"));

					sendError.Append("</table>");
					((Label) Page.FindControl("SendError")).Text = sendError.ToString();
					((Label) Page.FindControl("SendError")).Visible = true;


					SendSMS(mails, TxtMessage.Text);


					break;
			}

		}

		private void SendSMS(ArrayList nums, string text)
		{
			StringBuilder numbers = new StringBuilder();
			foreach (SMSNumber smsn in nums)
			{
				numbers.AppendFormat("{0},", smsn.Number);
			}

			wmsmsgateway sms = new wmsmsgateway();
			sms.mytakecredit += new TakeCredit(UpdateCredit);
			sms.myeventerror += new ErrorHandler(sms_myeventerror);
			int smsResult = sms.SendSMS(numbers.ToString().Substring(0, numbers.Length - 1), text);

			switch (smsResult)
			{
				case -1:
					MsgLabel.Text =Root.rm.GetString("SMStxt1");
					break;
				case 0:
					MsgLabel.Text =Root.rm.GetString("SMStxt2");
					break;
				case 1:
					MsgLabel.Text =Root.rm.GetString("SMStxt3");
					break;
				case 2:
					MsgLabel.Text =Root.rm.GetString("SMStxt4");
					break;
			}
		}

		private void UpdateCredit(int credit)
		{
			if (credit > 0)
			DatabaseConnection.DoCommand("UPDATE TUSTENA_DATA SET SMSCREDIT=SMSCREDIT-" + credit.ToString());
		}

		private void sms_myeventerror(string error)
		{
			G.SendError("[Tustena SMS] Error", error);
		}

		private void SaveMailingList(int id)
		{
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("GROUPS", "|" + UC.UserGroupId + "|", 'I');
				dg.Add("SMS", 1, 'I');
				dg.Add("DESCRIPTION", NewMLTitle.Text);

				object newId = dg.Execute("ML_DESCRIPTION", "ID=" + id, DigiDapter.Identities.Identity);
				if (id == -1)
					SaveParameterList(Convert.ToInt32(newId));
				else
					SaveParameterList(id);
				MLID.Text = newId.ToString();
			}
		}

		private void CreateXML(string mailmessage)
		{
			string fileName = ConfigSettings.MailMailingPath + Guid.NewGuid().ToString() + ".xml";

			XmlTextWriter myXmlTextWriter = new XmlTextWriter(fileName, null);
			myXmlTextWriter.Formatting = Formatting.Indented;
			myXmlTextWriter.WriteStartDocument(false);
			myXmlTextWriter.WriteStartElement("ml");
			myXmlTextWriter.WriteStartElement("message");
			myXmlTextWriter.WriteCData(mailmessage);
			myXmlTextWriter.WriteEndElement();
			myXmlTextWriter.WriteStartElement("mailing");
			int i = 1;
			foreach (Object o in mailsdata)
			{
				SMSListStruct ob = (SMSListStruct) o;
				myXmlTextWriter.WriteStartElement("mail");
				myXmlTextWriter.WriteAttributeString("id", (i++).ToString());
				myXmlTextWriter.WriteAttributeString("to", ob.Email);
				myXmlTextWriter.WriteAttributeString("refid", ob.RefId);
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
		}

		public void RepCategories_Command(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "MoveAllToML":

					break;
				case "MoveToML":

					break;
			}
		}

		private void PrepareAdvanced()
		{
			DropDownList Advanced_CompanyType = (DropDownList) RisearchAdvanced.FindControl("SAdvanced_CompanyType");
			DropDownList Advanced_ContactType = (DropDownList) RisearchAdvanced.FindControl("SAdvanced_ContactType");
			DropDownList Advanced_Estimate = (DropDownList) RisearchAdvanced.FindControl("SAdvanced_Estimate");
			DropDownList Advanced_Category = (DropDownList) RisearchAdvanced.FindControl("SAdvanced_Category");
			DropDownList SAdvancedContacts_Category = (DropDownList) RisearchAdvanced.FindControl("SAdvancedContacts_Category");
			DropDownList SAdvancedLead_Category = (DropDownList) RisearchAdvanced.FindControl("SAdvancedLead_Category");


			Fill_Settori(Advanced_CompanyType);
			Fill_ContactType(Advanced_ContactType);
			Fill_Valutazione(Advanced_Estimate);

			Advanced_Category.DataTextField = "Description";
			Advanced_Category.DataValueField = "id";
			Advanced_Category.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
			Advanced_Category.DataBind();
			Advanced_Category.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			Advanced_Category.Items[0].Value = "0";

			SAdvancedContacts_Category.DataTextField = "Description";
			SAdvancedContacts_Category.DataValueField = "id";
			SAdvancedContacts_Category.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + "))");
			SAdvancedContacts_Category.DataBind();
			SAdvancedContacts_Category.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			SAdvancedContacts_Category.Items[0].Value = "0";

			SAdvancedLead_Category.DataTextField = "Description";
			SAdvancedLead_Category.DataValueField = "id";
			SAdvancedLead_Category.DataSource = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM CRM_REFERRERCATEGORIES WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId.ToString() + "))");
			SAdvancedLead_Category.DataBind();
			SAdvancedLead_Category.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			SAdvancedLead_Category.Items[0].Value = "0";

			SAdvanced_Opportunity.DataTextField = "Title";
			SAdvanced_Opportunity.DataValueField = "id";
			SAdvanced_Opportunity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE (CREATEDBYID=" + UC.UserId + " AND (ADMINACCOUNT LIKE '%|" + UC.UserId + "|%' OR BASICACCOUNT LIKE '%|" + UC.UserId + "|%'))");
			SAdvanced_Opportunity.DataBind();
			SAdvanced_Opportunity.Items.Insert(0, Root.rm.GetString("CRMopptxt83"));
			SAdvanced_Opportunity.Items[0].Value = "0";

			SAdvancedLead_Opportunity.DataTextField = "Title";
			SAdvancedLead_Opportunity.DataValueField = "id";
			SAdvancedLead_Opportunity.DataSource = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE (CREATEDBYID=" + UC.UserId + " AND (ADMINACCOUNT LIKE '%|" + UC.UserId + "|%' OR BASICACCOUNT LIKE '%|" + UC.UserId + "|%'))");
			SAdvancedLead_Opportunity.DataBind();
			SAdvancedLead_Opportunity.Items.Insert(0, Root.rm.GetString("CRMopptxt83"));
			SAdvancedLead_Opportunity.Items[0].Value = "0";
		}

		private void Fill_Settori(DropDownList st)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,DESCRIPTION FROM COMPANYTYPE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY DESCRIPTION");
			st.DataSource = ds;
			st.DataTextField = "Description";
			st.DataValueField = "K_ID";
			st.DataBind();
			st.Items.Insert(0,Root.rm.GetString("CRMcontxt13"));
			st.Items[0].Value = "0";
		}

		private void Fill_ContactType(DropDownList ct)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,CONTACTTYPE FROM CONTACTTYPE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY CONTACTTYPE");
			ct.DataSource = ds;
			ct.DataTextField = "ContactType";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0,Root.rm.GetString("CRMcontxt14"));
			ct.Items[0].Value = "0";
		}

		private void Fill_Valutazione(DropDownList ct)
		{
			DataSet ds;
			ds = DatabaseConnection.CreateDataset("SELECT K_ID,ESTIMATE FROM CONTACTESTIMATE WHERE LANG='" + UC.CultureSpecific + "' ORDER BY FIELDORDER");
			ct.DataSource = ds;
			ct.DataTextField = "Estimate";
			ct.DataValueField = "K_ID";
			ct.DataBind();
			ct.Items.Insert(0,Root.rm.GetString("CRMcontxt15"));
			ct.Items[0].Value = "0";
		}


		private void FillRisearchAdvanced()
		{
			string queryType = String.Empty;
			string queryGroup = GroupsSecure();
			string advancedCompanyName = String.Empty;
			string advancedAddress = String.Empty;
			string advancedCity = String.Empty;
			string advancedState = String.Empty;
			string advancedZip = String.Empty;
			string advancedPhone = String.Empty;
			string advancedFax = String.Empty;
			string advancedEmail = String.Empty;
			string advancedSite = String.Empty;
			string advancedCode = String.Empty;
			string SAdvanced_CompanyType = String.Empty;
			string SAdvanced_ContactType = String.Empty;
			string advancedBilled = String.Empty;
			string advancedEmployees = String.Empty;
			string SAdvanced_Estimate = String.Empty;
			string SAdvanced_Category = String.Empty;
			string STextAdvanced_Opportunity = "";

			queryType = " AND ((BASE_COMPANIES.FLAGGLOBALORPERSONAL=2 AND  BASE_COMPANIES.OWNERID=" + UC.UserId.ToString() + ") OR (BASE_COMPANIES.FLAGGLOBALORPERSONAL<>2))";

			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT CAST(BASE_COMPANIES.ID AS VARCHAR(12))+'|'+ISNULL(BASE_COMPANIES.MLEMAIL, BASE_COMPANIES.EMAIL)+'|'+BASE_COMPANIES.COMPANYNAME AS IDMAIL, BASE_COMPANIES.COMPANYNAME ");
			sqlString.Append("FROM BASE_COMPANIES ");
			if (SAdvanced_Opportunity.SelectedIndex > 0)
			{
				sqlString.Append("INNER JOIN CRM_OPPORTUNITYCONTACT ON BASE_COMPANIES.ID = CRM_OPPORTUNITYCONTACT.CONTACTID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 0 ");
			}
			sqlString.AppendFormat("WHERE LIMBO=0 {0} AND (", queryType);

			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("Advanced_CompanyName") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedCompanyName += String.Format("(BASE_COMPANIES.COMPANYNAME LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Address") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedAddress += String.Format("(BASE_COMPANIES.INVOICINGADDRESS LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_City") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedCity += String.Format("(BASE_COMPANIES.INVOICINGCITY LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTCITY LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSECITY LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_State") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedState += String.Format("(BASE_COMPANIES.INVOICINGSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSESTATEPROVINCE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Zip") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedZip += String.Format("(BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEZIPCODE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Phone") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedPhone += String.Format("(BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTPHONE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEPHONE LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Fax") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedFax += String.Format("(BASE_COMPANIES.FAX LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTFAX LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEFAX LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Email") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedEmail += String.Format("(BASE_COMPANIES.EMAIL LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTEMAIL LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEEMAIL LIKE '%{0}%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("Advanced_Site") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedSite += String.Format("(BASE_COMPANIES.WEBSITE LIKE '%{0}%') OR ", Request[strKey]);
				}
				if (strKey.IndexOf("Advanced_Code") > -1)
				{
					if (Request[strKey].Length > 0)
						advancedCode += String.Format("(BASE_COMPANIES.COMPANYCODE LIKE '%{0}%') OR ", Request[strKey]);
				}
				if (strKey.IndexOf("SAdvanced_CompanyType") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						SAdvanced_CompanyType += String.Format("(BASE_COMPANIES.COMPANYTYPEID = '{0}') OR ", Request[strKey]);
				}
				if (strKey.IndexOf("SAdvanced_ContactType") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						SAdvanced_ContactType += String.Format("(BASE_COMPANIES.CONTACTTYPEID = '{0}') OR ", Request[strKey]);
				}
				if (strKey.IndexOf("SAdvanced_Opportunity") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						STextAdvanced_Opportunity += String.Format("(CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = '{0}') OR ", Request[strKey]);
				}


				if (strKey.IndexOf("RAdvanced_Billed") > -1)
				{
					if (Request[strKey].Length > 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 16) suffix = strKey.Substring(16, strKey.Length - 16);
						switch (Request[strKey])
						{
							case "0":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED = {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
							case "1":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED <= {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
							case "2":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED < {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
							case "3":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED <> {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
							case "4":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED > {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
							case "5":
								advancedBilled += String.Format("(BASE_COMPANIES.BILLED >= {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix]));
								break;
						}
					}
				}

				if (strKey.IndexOf("RAdvanced_Employees") > -1)
				{
					if (Request[strKey].Length > 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 16) suffix = strKey.Substring(16, strKey.Length - 16);
						switch (Request[strKey])
						{
							case "0":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES = {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
							case "1":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES <= {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
							case "2":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES < {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
							case "3":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES <> {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
							case "4":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES > {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
							case "5":
								advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES >= {0}) OR ", DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix]));
								break;
						}
					}
				}
				if (strKey.IndexOf("SAdvanced_Estimate") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						SAdvanced_Estimate += String.Format("(BASE_COMPANIES.ESTIMATE = {0}) OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
				if (strKey.IndexOf("SAdvanced_Category") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						SAdvanced_Category += String.Format("(BASE_COMPANIES.CATEGORIES LIKE '%|{0}|%') OR ", DatabaseConnection.FilterInjection(Request[strKey]));
				}
			}

			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;

			if (advancedCompanyName.Length > 0)
			{
				sqlAv += sAnd + advancedCompanyName.Substring(0, advancedCompanyName.Length - 3) + ")";
				sAnd = " AND (";
			}

			if (advancedAddress.Length > 0)
			{
				sqlAv += sAnd + advancedAddress.Substring(0, advancedAddress.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedCity.Length > 0)
			{
				sqlAv += sAnd + advancedCity.Substring(0, advancedCity.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedState.Length > 0)
			{
				sqlAv += sAnd + advancedState.Substring(0, advancedState.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedZip.Length > 0)
			{
				sqlAv += sAnd + advancedZip.Substring(0, advancedZip.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedPhone.Length > 0)
			{
				sqlAv += sAnd + advancedPhone.Substring(0, advancedPhone.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedFax.Length > 0)
			{
				sqlAv += sAnd + advancedFax.Substring(0, advancedFax.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedEmail.Length > 0)
			{
				sqlAv += sAnd + advancedEmail.Substring(0, advancedEmail.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedSite.Length > 0)
			{
				sqlAv += sAnd + advancedSite.Substring(0, advancedSite.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedCode.Length > 0)
			{
				sqlAv += sAnd + advancedCode.Substring(0, advancedCode.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_CompanyType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_CompanyType.Substring(0, SAdvanced_CompanyType.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_ContactType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_ContactType.Substring(0, SAdvanced_ContactType.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedBilled.Length > 0)
			{
				sqlAv += sAnd + advancedBilled.Substring(0, advancedBilled.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (advancedEmployees.Length > 0)
			{
				sqlAv += sAnd + advancedEmployees.Substring(0, advancedEmployees.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_Estimate.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Estimate.Substring(0, SAdvanced_Estimate.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (SAdvanced_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Category.Substring(0, SAdvanced_Category.Length - 3) + ")";
				sAnd = " AND (";
			}
			if (STextAdvanced_Opportunity.Length > 0)
			{
				sqlAv += sAnd + STextAdvanced_Opportunity.Substring(0, STextAdvanced_Opportunity.Length - 3) + ")";
				sAnd = " AND (";
			}

			SearchResult.DataTextField = "CompanyName";
			SearchResult.DataValueField = "IDMAIL";

			SearchResult.DataSource = DatabaseConnection.CreateDataset(sqlAv + " ORDER BY BASE_COMPANIES.COMPANYNAME");
			SearchResult.DataBind();

			QueryToSave.Text = sqlAv + ")" + " ORDER BY BASE_COMPANIES.COMPANYNAME";
			Trace.Warn("QueryToSave", QueryToSave.Text);
		}

		private void SaveParameterList(int id)
		{
			string advancedCompanyName = String.Empty;
			string advancedAddress = String.Empty;
			string advancedCity = String.Empty;
			string advancedState = String.Empty;
			string advancedNation = String.Empty;
			string advancedZip = String.Empty;
			string advancedPhone = String.Empty;
			string advancedFax = String.Empty;
			string advancedEmail = String.Empty;
			string advancedSite = String.Empty;
			string advancedCode = String.Empty;
			string SAdvanced_CompanyType = String.Empty;
			string SAdvanced_ContactType = String.Empty;
			string advancedBilled = String.Empty;
			string advancedEmployees = String.Empty;
			string SAdvanced_Estimate = String.Empty;
			string SAdvanced_Category = String.Empty;
			string STextAdvanced_Opportunity = String.Empty;

			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("SAdvanced_Opportunity") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && STextAdvanced_Opportunity.IndexOf(Request[strKey]) < 0)
						STextAdvanced_Opportunity += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_CompanyName") > -1)
				{
					if (Request[strKey].Length > 0 && advancedCompanyName.IndexOf(Request[strKey]) < 0)
						advancedCompanyName += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Address") > -1)
				{
					if (Request[strKey].Length > 0 && advancedAddress.IndexOf(Request[strKey]) < 0)
						advancedAddress += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_City") > -1)
				{
					if (Request[strKey].Length > 0 && advancedCity.IndexOf(Request[strKey]) < 0)
						advancedCity += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_State") > -1)
				{
					if (Request[strKey].Length > 0 && advancedState.IndexOf(Request[strKey]) < 0)
						advancedState += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Nation") > -1)
				{
					if (Request[strKey].Length > 0 && advancedNation.IndexOf(Request[strKey]) < 0)
						advancedNation += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Zip") > -1)
				{
					if (Request[strKey].Length > 0 && advancedZip.IndexOf(Request[strKey]) < 0)
						advancedZip += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Phone") > -1)
				{
					if (Request[strKey].Length > 0 && advancedPhone.IndexOf(Request[strKey]) < 0)
						advancedPhone += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Fax") > -1)
				{
					if (Request[strKey].Length > 0 && advancedFax.IndexOf(Request[strKey]) < 0)
						advancedFax += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Email") > -1)
				{
					if (Request[strKey].Length > 0 && advancedEmail.IndexOf(Request[strKey]) < 0)
						advancedEmail += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Site") > -1)
				{
					if (Request[strKey].Length > 0 && advancedSite.IndexOf(Request[strKey]) < 0)
						advancedSite += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Code") > -1)
				{
					if (Request[strKey].Length > 0 && advancedCode.IndexOf(Request[strKey]) < 0)
						advancedCode += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("SAdvanced_CompanyType") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && SAdvanced_CompanyType.IndexOf(Request[strKey]) < 0)
						SAdvanced_CompanyType += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("SAdvanced_ContactType") > -1 && SAdvanced_ContactType.IndexOf(Request[strKey]) < 0)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0")
						SAdvanced_ContactType += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}

				if (strKey.IndexOf("RAdvanced_Billed") > -1)
				{
					if (Request[strKey].Length > 0 && advancedBilled.IndexOf(Request[strKey]) < 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 16) suffix = strKey.Substring(16, strKey.Length - 16);
						switch (Request[strKey])
						{
							case "0":
								advancedBilled += "a" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
							case "1":
								advancedBilled += "b" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
							case "2":
								advancedBilled += "c" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
							case "3":
								advancedBilled += "d" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
							case "4":
								advancedBilled += "e" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
							case "5":
								advancedBilled += "f" + DatabaseConnection.FilterInjection(Request["Advanced_Billed" + suffix] + "|");
								break;
						}
					}
				}

				if (strKey.IndexOf("RAdvanced_Employees") > -1)
				{
					if (Request[strKey].Length > 0 && advancedEmployees.IndexOf(Request[strKey]) < 0)
					{
						string suffix = String.Empty;
						if (strKey.Length > 19) suffix = strKey.Substring(19, strKey.Length - 19);
						switch (suffix)
						{
							case "0":
								advancedEmployees += "a" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
							case "1":
								advancedEmployees += "b" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
							case "2":
								advancedEmployees += "c" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
							case "3":
								advancedEmployees += "d" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
							case "4":
								advancedEmployees += "e" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
							case "5":
								advancedEmployees += "f" + DatabaseConnection.FilterInjection(Request["Advanced_Employees" + suffix] + "|");
								break;
						}
					}
				}
				if (strKey.IndexOf("SAdvanced_Estimate") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && SAdvanced_Estimate.IndexOf(Request[strKey]) < 0)
						SAdvanced_Estimate += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("SAdvanced_Category") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && SAdvanced_Category.IndexOf(Request[strKey]) < 0)
						SAdvanced_Category += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
			}
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("IDMAILINGLIST", id, 'I');

				dg.Add("COMPANYNAMES", (advancedCompanyName.Length > 0) ? advancedCompanyName : "");
				dg.Add("ADDRESS", (advancedAddress.Length > 0) ? advancedAddress : "");
				dg.Add("CITY", (advancedCity.Length > 0) ? advancedCity : "");
				dg.Add("PROVINCE", (advancedState.Length > 0) ? advancedState : "");
				dg.Add("NATION", (advancedNation.Length > 0) ? advancedNation : "");
				dg.Add("ZIPCODE", (advancedZip.Length > 0) ? advancedZip : "");
				dg.Add("PHONE", (advancedPhone.Length > 0) ? advancedPhone : "");
				dg.Add("FAX", (advancedFax.Length > 0) ? advancedFax : "");
				dg.Add("EMAIL", (advancedEmail.Length > 0) ? advancedFax : "");
				dg.Add("WEBSITE", (advancedSite.Length > 0) ? advancedSite : "");
				dg.Add("CODE", (advancedCode.Length > 0) ? advancedCode : "");
				dg.Add("COMPANYTYPE", (SAdvanced_CompanyType.Length > 0) ? SAdvanced_CompanyType : "");
				dg.Add("CONTACTTYPE", (SAdvanced_ContactType.Length > 0) ? SAdvanced_ContactType : "");
				dg.Add("BILLED", (advancedBilled.Length > 0) ? advancedBilled : "");
				dg.Add("EMPLOYEES", (advancedEmployees.Length > 0) ? advancedEmployees : "");
				dg.Add("ESTIMATE", (SAdvanced_Estimate.Length > 0) ? SAdvanced_Estimate : "");
				dg.Add("CATEGORIES", (SAdvanced_Category.Length > 0) ? SAdvanced_Category : "");
				dg.Add("OPPORTUNITY", (STextAdvanced_Opportunity.Length > 0) ? STextAdvanced_Opportunity : "");
				dg.Execute("ML_COMPANIES", "IDMAILINGLIST=" + id);
			}


			string advancedContactsAddress = String.Empty;
			string advancedContactsCity = String.Empty;
			string advancedContactsState = String.Empty;
			string advancedContactsNation = String.Empty;
			string advancedContactsZip = String.Empty;
			string advancedContactsEmail = String.Empty;
			string SAdvancedContacts_Category = String.Empty;

			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("AdvancedContacts_Address") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsAddress.IndexOf(Request[strKey]) < 0)
						advancedContactsAddress += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedContacts_City") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsCity.IndexOf(Request[strKey]) < 0)
						advancedContactsCity += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedContacts_State") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsState.IndexOf(Request[strKey]) < 0)
						advancedContactsState += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedContacts_Nation") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsNation.IndexOf(Request[strKey]) < 0)
						advancedContactsNation += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedContacts_Zip") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsZip.IndexOf(Request[strKey]) < 0)
						advancedContactsZip += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Phone") > -1)
				{
					if (Request[strKey].Length > 0 && advancedPhone.IndexOf(Request[strKey]) < 0)
						advancedPhone += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedContacts_Email") > -1)
				{
					if (Request[strKey].Length > 0 && advancedContactsEmail.IndexOf(Request[strKey]) < 0)
						advancedContactsEmail += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("SAdvancedContacts_Category") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && SAdvancedContacts_Category.IndexOf(Request[strKey]) < 0)
						SAdvancedContacts_Category += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
			}
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("IDMAILINGLIST", id, 'I');

				dg.Add("ADDRESS", (advancedContactsAddress.Length > 0) ? advancedContactsAddress : "");
				dg.Add("CITY", (advancedContactsCity.Length > 0) ? advancedContactsCity : "");
				dg.Add("PROVINCE", (advancedContactsState.Length > 0) ? advancedContactsState : "");
				dg.Add("NATION", (advancedContactsNation.Length > 0) ? advancedContactsNation : "");
				dg.Add("ZIPCODE", (advancedContactsZip.Length > 0) ? advancedContactsZip : "");
				dg.Add("EMAIL", (advancedContactsEmail.Length > 0) ? advancedContactsEmail : "");
				dg.Add("CATEGORIES", (SAdvancedContacts_Category.Length > 0) ? SAdvancedContacts_Category : "");

				dg.Execute("ML_CONTACTS", "IDMAILINGLIST=" + id);
			}



			string advancedLeadAddress = String.Empty;
			string advancedLeadCity = String.Empty;
			string advancedLeadState = String.Empty;
			string advancedLeadNation = String.Empty;
			string advancedLeadZip = String.Empty;
			string advancedLeadEmail = String.Empty;
			string SAdvancedLead_Category = String.Empty;
			string STextAdvancedLead_Opportunity = "";

			foreach (string strKey in Request.Params.Keys)
			{
				if (strKey.IndexOf("SAdvancedLead_Opportunity") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && STextAdvancedLead_Opportunity.IndexOf(Request[strKey]) < 0)
						STextAdvancedLead_Opportunity += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_Address") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadAddress.IndexOf(Request[strKey]) < 0)
						advancedLeadAddress += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_City") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadCity.IndexOf(Request[strKey]) < 0)
						advancedLeadCity += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_State") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadState.IndexOf(Request[strKey]) < 0)
						advancedLeadState += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_Nation") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadNation.IndexOf(Request[strKey]) < 0)
						advancedLeadNation += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_Zip") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadZip.IndexOf(Request[strKey]) < 0)
						advancedLeadZip += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("Advanced_Phone") > -1)
				{
					if (Request[strKey].Length > 0 && advancedPhone.IndexOf(Request[strKey]) < 0)
						advancedPhone += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("AdvancedLead_Email") > -1)
				{
					if (Request[strKey].Length > 0 && advancedLeadEmail.IndexOf(Request[strKey]) < 0)
						advancedLeadEmail += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
				if (strKey.IndexOf("SAdvancedLead_Category") > -1)
				{
					if (Request[strKey].Length > 0 && Request[strKey] != "0" && SAdvancedLead_Category.IndexOf(Request[strKey]) < 0)
						SAdvancedLead_Category += DatabaseConnection.FilterInjection(Request[strKey] + "|");
				}
			}

			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("IDMAILINGLIST", id, 'I');


				dg.Add("ADDRESS", (advancedLeadAddress.Length > 0) ? advancedLeadAddress : "");
				dg.Add("CITY", (advancedLeadCity.Length > 0) ? advancedLeadCity : "");
				dg.Add("PROVINCE", (advancedLeadState.Length > 0) ? advancedLeadState : "");
				dg.Add("NATION", (advancedLeadNation.Length > 0) ? advancedLeadNation : "");
				dg.Add("ZIPCODE", (advancedLeadZip.Length > 0) ? advancedLeadZip : "");
				dg.Add("EMAIL", (advancedLeadEmail.Length > 0) ? advancedLeadEmail : "");
				dg.Add("CATEGORIES", (SAdvancedLead_Category.Length > 0) ? SAdvancedLead_Category : "");
				dg.Add("OPPORTUNITY", (STextAdvancedLead_Opportunity.Length > 0) ? STextAdvancedLead_Opportunity : "");
				dg.Execute("ML_LEAD", "IDMAILINGLIST=" + id);
			}

			if (Request.Form["ListFixedParams"].Length > 0)
			{
				string companyString = String.Empty;
				string contactString = String.Empty;
				string leadString = String.Empty;

				string[] fixParams = Request.Form["ListFixedParams"].Split('|');
				foreach (string s in fixParams)
				{
					if (s.Length > 0)
					{
						switch (s.Substring(0, 1))
						{
							case "A":
								companyString += s.Substring(1) + "|";
								break;
							case "C":
								contactString += s.Substring(1) + "|";
								break;
							case "L":
								leadString += s.Substring(1) + "|";
								break;
						}
					}
				}
				using (DigiDapter dg = new DigiDapter())
				{
					dg.Add("IDMAILINGLIST", id, 'I');
					dg.Add("COMPANY", companyString);
					dg.Add("CONTACT", contactString);
					dg.Add("LEAD", leadString);
					dg.Execute("ML_FIXEDPARAMS", "IDMAILINGLIST=" + id);
				}
			}

			Response.Redirect("/MailingList/NewMailingList.aspx?m=46&dgb=1&si=52");
		}

		public string GetCompanyQuery(int id)
		{
			return GetCompanyQuery(id, UC);
		}

		public string GetCompanyQuery(int id, UserConfig uC)
		{
			DataRow dt = DatabaseConnection.CreateDataset("SELECT * FROM ML_COMPANIES WHERE IDMAILINGLIST=" + id).Tables[0].Rows[0];

			string queryType = String.Empty;
			string advancedCompanyName = String.Empty;
			string advancedAddress = String.Empty;
			string advancedCity = String.Empty;
			string advancedState = String.Empty;
			string advancedZip = String.Empty;
			string advancedPhone = String.Empty;
			string advancedFax = String.Empty;
			string advancedEmail = String.Empty;
			string advancedSite = String.Empty;
			string advancedCode = String.Empty;
			string SAdvanced_CompanyType = String.Empty;
			string SAdvanced_ContactType = String.Empty;
			string advancedBilled = String.Empty;
			string advancedEmployees = String.Empty;
			string SAdvanced_Estimate = String.Empty;
			string SAdvanced_Category = String.Empty;
			string STextAdvanced_Opportunity = String.Empty;

			queryType = " AND ((BASE_COMPANIES.FLAGGLOBALORPERSONAL=2 AND  BASE_COMPANIES.OWNERID=" + uC.UserId.ToString() + ") OR (BASE_COMPANIES.FLAGGLOBALORPERSONAL<>2))";

			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT CAST(BASE_COMPANIES.ID AS VARCHAR(12))+'|'+ISNULL(BASE_COMPANIES.PHONE,'')+'|'+BASE_COMPANIES.COMPANYNAME AS IDMAIL, BASE_COMPANIES.COMPANYNAME ");
			sqlString.Append("FROM BASE_COMPANIES ");
			sqlString.Append("INNER JOIN CRM_OPPORTUNITYCONTACT ON BASE_COMPANIES.ID = CRM_OPPORTUNITYCONTACT.CONTACTID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 0 ");
			sqlString.AppendFormat("WHERE LIMBO=0 {0} AND (", queryType);

			string[] strKeys = dt["CompanyNames"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedCompanyName += String.Format("(BASE_COMPANIES.COMPANYNAME LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["Address"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedAddress += String.Format("(BASE_COMPANIES.INVOICINGADDRESS LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["City"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedCity += String.Format("(BASE_COMPANIES.INVOICINGCITY LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTCITY LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSECITY LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Province"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedState += String.Format("(BASE_COMPANIES.INVOICINGSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTSTATEPROVINCE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSESTATEPROVINCE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["ZIPCode"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedZip += String.Format("(BASE_COMPANIES.INVOICINGZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTZIPCODE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEZIPCODE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["Phone"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedPhone += String.Format("(BASE_COMPANIES.PHONE LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTPHONE LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEPHONE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["Fax"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedFax += String.Format("(BASE_COMPANIES.FAX LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTFAX LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEFAX LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["EMail"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedEmail += String.Format("(BASE_COMPANIES.EMAIL LIKE '%{0}%' OR BASE_COMPANIES.SHIPMENTEMAIL LIKE '%{0}%' OR BASE_COMPANIES.WAREHOUSEEMAIL LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["WebSite"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedSite += String.Format("(BASE_COMPANIES.WEBSITE LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Code"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedCode += String.Format("(BASE_COMPANIES.COMPANYCODE LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["CompanyType"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_CompanyType += String.Format("(BASE_COMPANIES.COMPANYTYPEID = '{0}') OR ", strKey);
			}
			strKeys = dt["ContactType"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_ContactType += String.Format("(BASE_COMPANIES.CONTACTTYPEID = '{0}') OR ", strKey);
			}

			strKeys = dt["Billed"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
				{
					switch (strKey.Substring(0, 1))
					{
						case "a":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED = {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "b":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED <= {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "c":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED < {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "d":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED <> {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "e":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED > {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "f":
							advancedBilled += String.Format("(BASE_COMPANIES.BILLED >= {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
					}
				}
			}

			strKeys = dt["Employees"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
				{
					switch (strKey.Substring(0, 1))
					{
						case "a":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES = {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "b":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES <= {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "c":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES < {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "d":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES <> {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "e":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES > {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
						case "f":
							advancedEmployees += String.Format("(BASE_COMPANIES.EMPLOYEES >= {0}) OR ", strKey.Substring(1, strKey.Length - 1));
							break;
					}
				}
			}
			strKeys = dt["Estimate"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_Estimate += String.Format("(BASE_COMPANIES.ESTIMATE = {0}) OR ", strKey);
			}
			strKeys = dt["Categories"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvanced_Category += String.Format("(BASE_COMPANIES.CATEGORIES LIKE '%|{0}|%') OR ", strKey);
			}
			strKeys = dt["Opportunity"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					STextAdvanced_Opportunity += String.Format("(CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = '{0}') OR ", strKey);
			}

			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;
			bool haveparams = false;
			if (advancedCompanyName.Length > 0)
			{
				sqlAv += sAnd + advancedCompanyName.Substring(0, advancedCompanyName.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (advancedAddress.Length > 0)
			{
				sqlAv += sAnd + advancedAddress.Substring(0, advancedAddress.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedCity.Length > 0)
			{
				sqlAv += sAnd + advancedCity.Substring(0, advancedCity.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedState.Length > 0)
			{
				sqlAv += sAnd + advancedState.Substring(0, advancedState.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedZip.Length > 0)
			{
				sqlAv += sAnd + advancedZip.Substring(0, advancedZip.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedPhone.Length > 0)
			{
				sqlAv += sAnd + advancedPhone.Substring(0, advancedPhone.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedFax.Length > 0)
			{
				sqlAv += sAnd + advancedFax.Substring(0, advancedFax.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedEmail.Length > 0)
			{
				sqlAv += sAnd + advancedEmail.Substring(0, advancedEmail.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedSite.Length > 0)
			{
				sqlAv += sAnd + advancedSite.Substring(0, advancedSite.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedCode.Length > 0)
			{
				sqlAv += sAnd + advancedCode.Substring(0, advancedCode.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvanced_CompanyType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_CompanyType.Substring(0, SAdvanced_CompanyType.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvanced_ContactType.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_ContactType.Substring(0, SAdvanced_ContactType.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedBilled.Length > 0)
			{
				sqlAv += sAnd + advancedBilled.Substring(0, advancedBilled.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedEmployees.Length > 0)
			{
				sqlAv += sAnd + advancedEmployees.Substring(0, advancedEmployees.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvanced_Estimate.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Estimate.Substring(0, SAdvanced_Estimate.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvanced_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvanced_Category.Substring(0, SAdvanced_Category.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (STextAdvanced_Opportunity.Length > 0)
			{
				sqlAv += sAnd + STextAdvanced_Opportunity.Substring(0, STextAdvanced_Opportunity.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (haveparams)
			{
				if (sqlAv.Substring(sqlAv.Length - 6, 6).IndexOf("AND") > -1)
					sqlAv += " (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_COMPANIES.ID = ML_REMOVEDFROM.IDREF AND TYPE=0 AND IDML=" + id + ")))";
				else
					sqlAv += "AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_COMPANIES.ID = ML_REMOVEDFROM.IDREF AND TYPE=0 AND IDML=" + id + "))";

				Trace.Warn("companyquery", sqlAv + " ORDER BY BASE_COMPANIES.COMPANYNAME");

				return sqlAv + " ORDER BY BASE_COMPANIES.COMPANYNAME";
			}
			else
			{
				return "SELECT ID AS IDMAIL,COMPANYNAME FROM BASE_COMPANIES WHERE ID=-1";
			}


		}

		public string GetContactQuery(int id)
		{
			return GetContactQuery(id, UC);
		}

		public string GetContactQuery(int id, UserConfig uC)
		{
			DataRow dt = DatabaseConnection.CreateDataset("SELECT * FROM ML_CONTACTS WHERE IDMAILINGLIST=" + id).Tables[0].Rows[0];

			string advancedContactsAddress = String.Empty;
			string advancedContactsCity = String.Empty;
			string advancedContactsState = String.Empty;
			string advancedContactsNation = String.Empty;
			string advancedContactsZip = String.Empty;
			string advancedContactsEmail = String.Empty;
			string SAdvancedContacts_Category = String.Empty;


			StringBuilder sqlString = new StringBuilder();
			sqlString.Append("SELECT CAST(BASE_CONTACTS.ID AS VARCHAR(12))+'|'+CASE WHEN ISNULL(BASE_CONTACTS.MOBILEPHONE_1,'')='' THEN BASE_CONTACTS.MOBILEPHONE_2 ELSE BASE_CONTACTS.MOBILEPHONE_1 END+'|'+BASE_CONTACTS.NAME+' '+BASE_CONTACTS.SURNAME AS IDMAIL, BASE_CONTACTS.NAME+' '+BASE_CONTACTS.SURNAME AS CONTACT ");
			sqlString.Append("FROM BASE_CONTACTS ");
			sqlString.Append("WHERE LIMBO=0 AND (");


			string[] strKeys = dt["Address"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsAddress += String.Format("(BASE_CONTACTS.ADDRESS_1 LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["City"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsCity += String.Format("(BASE_CONTACTS.CITY_1 LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Province"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsState += String.Format("(BASE_CONTACTS.PROVINCE_1 LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Nation"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsNation += String.Format("(BASE_CONTACTS.STATE_1 LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["ZIPCode"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsZip += String.Format("(BASE_CONTACTS.ZIPCODE_1 LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["EMail"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedContactsEmail += String.Format("(BASE_CONTACTS.EMAIL LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Categories"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedContacts_Category += String.Format("(BASE_CONTACTS.CATEGORIES LIKE '%|{0}|%') OR ", strKey);
			}

			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;
			bool haveparams = false;


			if (advancedContactsAddress.Length > 0)
			{
				sqlAv += sAnd + advancedContactsAddress.Substring(0, advancedContactsAddress.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedContactsCity.Length > 0)
			{
				sqlAv += sAnd + advancedContactsCity.Substring(0, advancedContactsCity.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedContactsState.Length > 0)
			{
				sqlAv += sAnd + advancedContactsState.Substring(0, advancedContactsState.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedContactsNation.Length > 0)
			{
				sqlAv += sAnd + advancedContactsNation.Substring(0, advancedContactsNation.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedContactsZip.Length > 0)
			{
				sqlAv += sAnd + advancedContactsZip.Substring(0, advancedContactsZip.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (advancedContactsEmail.Length > 0)
			{
				sqlAv += sAnd + advancedContactsEmail.Substring(0, advancedContactsEmail.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvancedContacts_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvancedContacts_Category.Substring(0, SAdvancedContacts_Category.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (haveparams)
			{
				if (sqlAv.Substring(sqlAv.Length - 6, 6).IndexOf("AND") > -1)
					sqlAv += " (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_CONTACTS.ID = ML_REMOVEDFROM.IDREF AND TYPE=1 AND IDML=" + id + ")))";
				else
					sqlAv += "AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE BASE_CONTACTS.ID = ML_REMOVEDFROM.IDREF AND TYPE=1 AND IDML=" + id + "))";

				Trace.Warn("contactquery", sqlAv + " ORDER BY BASE_CONTACTS.NAME,BASE_CONTACTS.SURNAME");
				return sqlAv + " ORDER BY BASE_CONTACTS.NAME,BASE_CONTACTS.SURNAME";
			}
			else
			{
				return "SELECT ID AS IDMAIL,COMPANYNAME FROM BASE_COMPANIES WHERE ID=-1";
			}
		}

		public string GetLeadQuery(int id)
		{
			return GetLeadQuery(id, UC);
		}

		public string GetLeadQuery(int id, UserConfig uC)
		{
			DataRow dt = DatabaseConnection.CreateDataset("SELECT * FROM ML_LEAD WHERE IDMAILINGLIST=" + id).Tables[0].Rows[0];

			string advancedLeadAddress = String.Empty;
			string advancedLeadCity = String.Empty;
			string advancedLeadState = String.Empty;
			string advancedLeadNation = String.Empty;
			string advancedLeadZip = String.Empty;
			string advancedLeadEmail = String.Empty;
			string SAdvancedLead_Category = String.Empty;
			string STextAdvancedLead_Opportunity = "";


			StringBuilder sqlString = new StringBuilder();

			sqlString.Append("SELECT CAST(CRM_LEADS.ID AS VARCHAR(12))+'|'+ISNULL(CRM_LEADS.MOBILEPHONE,'')+'|'+ISNULL(CRM_LEADS.COMPANYNAME,'')+' '+ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'') AS IDMAIL, ISNULL(CRM_LEADS.COMPANYNAME,'')+' '+ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'') AS CONTACT ");
			sqlString.Append("FROM CRM_LEADS ");

			sqlString.Append("INNER JOIN CRM_OPPORTUNITYCONTACT ON CRM_LEADS.ID = CRM_OPPORTUNITYCONTACT.CONTACTID AND CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 1 ");
			sqlString.Append("WHERE LIMBO=0 AND (");

			string[] strKeys = dt["Address"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadAddress += String.Format("(CRM_LEADS.ADDRESS LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Opportunity"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					STextAdvancedLead_Opportunity += String.Format("(CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = '{0}') OR ", strKey);
			}
			strKeys = dt["City"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadCity += String.Format("(CRM_LEADS.CITY LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Province"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadState += String.Format("(CRM_LEADS.PROVINCE LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Nation"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadNation += String.Format("(CRM_LEADS.STATE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["ZIPCode"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadZip += String.Format("(CRM_LEADS.ZIPCODE LIKE '%{0}%') OR ", strKey);
			}

			strKeys = dt["EMail"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					advancedLeadEmail += String.Format("(CRM_LEADS.EMAIL LIKE '%{0}%') OR ", strKey);
			}
			strKeys = dt["Categories"].ToString().Split('|');
			foreach (string strKey in strKeys)
			{
				if (strKey.Length > 0)
					SAdvancedLead_Category += String.Format("(CRM_LEADS.CATEGORIES LIKE '%|{0}|%') OR ", strKey);
			}

			string sqlAv = sqlString.ToString();
			string sAnd = String.Empty;
			bool haveparams = false;

			if (advancedLeadAddress.Length > 0)
			{
				sqlAv += sAnd + advancedLeadAddress.Substring(0, advancedLeadAddress.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedLeadCity.Length > 0)
			{
				sqlAv += sAnd + advancedLeadCity.Substring(0, advancedLeadCity.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedLeadState.Length > 0)
			{
				sqlAv += sAnd + advancedLeadState.Substring(0, advancedLeadState.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedLeadNation.Length > 0)
			{
				sqlAv += sAnd + advancedLeadNation.Substring(0, advancedLeadNation.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (advancedLeadZip.Length > 0)
			{
				sqlAv += sAnd + advancedLeadZip.Substring(0, advancedLeadZip.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (advancedLeadEmail.Length > 0)
			{
				sqlAv += sAnd + advancedLeadEmail.Substring(0, advancedLeadEmail.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (SAdvancedLead_Category.Length > 0)
			{
				sqlAv += sAnd + SAdvancedLead_Category.Substring(0, SAdvancedLead_Category.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}
			if (STextAdvancedLead_Opportunity.Length > 0)
			{
				sqlAv += sAnd + STextAdvancedLead_Opportunity.Substring(0, STextAdvancedLead_Opportunity.Length - 3) + ")";
				sAnd = " AND (";
				haveparams = true;
			}

			if (haveparams)
			{
				if (sqlAv.Substring(sqlAv.Length - 6, 6).IndexOf("AND") > -1)
					sqlAv += " (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE CRM_LEADS.ID = ML_REMOVEDFROM.IDREF AND TYPE=2 AND IDML=" + id + ")))";
				else
					sqlAv += "AND (NOT EXISTS (SELECT * FROM ML_REMOVEDFROM WHERE CRM_LEADS.ID = ML_REMOVEDFROM.IDREF AND TYPE=2 AND IDML=" + id + "))";

				Trace.Warn("leadquery", sqlAv + " ORDER BY CRM_LEADS.COMPANYNAME,CRM_LEADS.NAME,CRM_LEADS.SURNAME");
				return sqlAv + " ORDER BY CRM_LEADS.COMPANYNAME,CRM_LEADS.NAME,CRM_LEADS.SURNAME";
			}
			else
			{
				return "SELECT ID AS IDMAIL,COMPANYNAME FROM BASE_COMPANIES WHERE ID=-1";
			}
		}

		private void PopulateML(int id)
		{
			DataSet tempDt = DatabaseConnection.CreateDataset("SELECT * FROM ML_COMPANIES WHERE IDMAILINGLIST=" + id);
			if (tempDt.Tables[0].Rows.Count > 0)
			{
				DataRow dt = tempDt.Tables[0].Rows[0];
				StringBuilder arry = new StringBuilder();

				arry.AppendFormat("<script>Companies=new Array();{0}", Environment.NewLine);
				string[] voci = dt["CompanyNames"].ToString().Split('|');
				byte[] existsArr = new byte[] {0, 0, 0};
				bool first = true;
				try
				{
					arry.Append("Companies[0]= new Array(");
					voci = dt["CompanyNames"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_CompanyName")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[1]= new Array(");
					voci = dt["Address"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Address")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[2]= new Array(");
					voci = dt["City"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_City")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[3]= new Array(");
					voci = dt["Province"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_State")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[4]= new Array(");

					voci = dt["Nation"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Nation")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);


					arry.Append("Companies[5]= new Array(");
					voci = dt["ZIPCode"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Zip")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[6]= new Array(");
					voci = dt["Phone"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Phone")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[7]= new Array(");
					voci = dt["Fax"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Fax")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[8]= new Array(");
					voci = dt["EMail"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Email")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[9]= new Array(");
					voci = dt["WebSite"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Site")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[10]= new Array(");
					voci = dt["WebSite"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Code")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[11]= new Array(");
					voci = dt["CompanyType"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvanced_CompanyType"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[12]= new Array(");
					voci = dt["ContactType"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvanced_ContactType"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[13]= new Array(");
					voci = dt["Billed"].ToString().Split('|');
					first = true;
					string check = String.Empty;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Billed")).Text = v.Substring(1, v.Length - 1);
								switch (v.Substring(0, 1))
								{
									case "a":
										check = "var ch = document.getElementById(\"RAdvanced_Billed0\");ch.checked=true;";
										break;
									case "b":
										check = "var ch = document.getElementById(\"RAdvanced_Billed1\");ch.checked=true;";
										break;
									case "c":
										check = "var ch = document.getElementById(\"RAdvanced_Billed2\");ch.checked=true;";
										break;
									case "d":
										check = "var ch = document.getElementById(\"RAdvanced_Billed3\");ch.checked=true;";
										break;
									case "e":
										check = "var ch = document.getElementById(\"RAdvanced_Billed4\");ch.checked=true;";
										break;
									case "f":
										check = "var ch = document.getElementById(\"RAdvanced_Billed5\");ch.checked=true;";
										break;
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}{1}{0}", Environment.NewLine, check);

					arry.Append("Companies[14]= new Array(");
					voci = dt["Employees"].ToString().Split('|');
					first = true;
					check = String.Empty;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("Advanced_Employees")).Text = v.Substring(1, v.Length - 1);
								switch (v.Substring(0, 1))
								{
									case "a":
										check = "var ch = document.getElementById(\"RAdvanced_Employees0\");ch.checked=true;";
										break;
									case "b":
										check = "var ch = document.getElementById(\"RAdvanced_Employees1\");ch.checked=true;";
										break;
									case "c":
										check = "var ch = document.getElementById(\"RAdvanced_Employees2\");ch.checked=true;";
										break;
									case "d":
										check = "var ch = document.getElementById(\"RAdvanced_Employees3\");ch.checked=true;";
										break;
									case "e":
										check = "var ch = document.getElementById(\"RAdvanced_Employees4\");ch.checked=true;";
										break;
									case "f":
										check = "var ch = document.getElementById(\"RAdvanced_Employees5\");ch.checked=true;";
										break;
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}{1}{0}", Environment.NewLine, check);

					arry.Append("Companies[15]= new Array(");
					voci = dt["Estimate"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvanced_Estimate"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[16]= new Array(");
					voci = dt["Categories"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvanced_Category"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Companies[17]= new Array(");
					voci = dt["Opportunity"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvanced_Opportunity"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					existsArr[0] = 1;
				}
				catch
				{
				}
				arry.AppendFormat("Contacts=new Array();{0}", Environment.NewLine);
				try
				{
					dt = DatabaseConnection.CreateDataset("SELECT * FROM ML_CONTACTS WHERE IDMAILINGLIST=" + id).Tables[0].Rows[0];

					arry.Append("Contacts[0]= new Array(");
					voci = dt["Address"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_Address")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Contacts[1]= new Array(");
					voci = dt["City"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_City")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Contacts[2]= new Array(");
					voci = dt["Province"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_State")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Contacts[3]= new Array(");

					voci = dt["Nation"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_Nation")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);


					arry.Append("Contacts[4]= new Array(");
					voci = dt["ZIPCode"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_Zip")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Contacts[5]= new Array(");
					voci = dt["EMail"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedContacts_Email")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Contacts[6]= new Array(");
					voci = dt["Categories"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvancedContacts_Category"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);
					existsArr[1] = 1;
				}
				catch
				{
				}
				arry.AppendFormat("Leads=new Array();{0}", Environment.NewLine);
				try
				{
					dt = DatabaseConnection.CreateDataset("SELECT * FROM ML_LEAD WHERE IDMAILINGLIST=" + id).Tables[0].Rows[0];
					arry.Append("Leads[0]= new Array(");
					voci = dt["Address"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_Address")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Leads[1]= new Array(");
					voci = dt["City"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_City")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Leads[2]= new Array(");
					voci = dt["Province"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_State")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Leads[3]= new Array(");

					voci = dt["Nation"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_Nation")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);


					arry.Append("Leads[4]= new Array(");
					voci = dt["ZIPCode"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_Zip")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Leads[5]= new Array(");
					voci = dt["EMail"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								((TextBox) Page.FindControl("AdvancedLead_Email")).Text = v;
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

					arry.Append("Leads[6]= new Array(");
					voci = dt["Categories"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvancedLead_Category"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);


					arry.Append("Leads[7]= new Array(");
					voci = dt["Opportunity"].ToString().Split('|');
					first = true;
					foreach (string v in voci)
					{
						if (v.Length > 0)
						{
							arry.AppendFormat("\"{0}\",", v);
							if (first)
							{
								first = false;
								DropDownList list = ((DropDownList) Page.FindControl("SAdvancedLead_Opportunity"));
								list.SelectedIndex = -1;
								foreach (ListItem i in list.Items)
								{
									if (i.Value == v)
									{
										i.Selected = true;
										break;
									}
								}
							}
						}
					}
					if (!first) arry.Remove(arry.Length - 1, 1);
					arry.AppendFormat(");{0}", Environment.NewLine);

				}
				catch
				{
				}

				DataTable dtFix = DatabaseConnection.CreateDataset("SELECT * FROM ML_FIXEDPARAMS WHERE IDMAILINGLIST=" + id).Tables[0];
				if (dtFix.Rows.Count > 0)
				{
					DataRow drFix = dtFix.Rows[0];
					if (drFix["company"].ToString().Length > 0)
					{
						string query = String.Empty;
						string[] xs = drFix["company"].ToString().Split('|');
						foreach (string s in xs)
						{
							if (s.Length > 0)
								query += "ID=" + s + " OR ";
						}
						DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME FROM BASE_COMPANIES WHERE " + query.Substring(0, query.Length - 3)).Tables[0];
						foreach (DataRow dr in dtaz.Rows)
						{
							ListFixedParams.Value += "A" + dr[0].ToString() + "|";
							MLFixedMails.Items.Add(new ListItem(dr[1].ToString(), "A" + dr[0].ToString()));
						}
					}
					if (drFix["contact"].ToString().Length > 0)
					{
						string query = String.Empty;
						string[] xs = drFix["contact"].ToString().Split('|');
						foreach (string s in xs)
						{
							if (s.Length > 0)
								query += "ID=" + s + " OR ";
						}
						DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT FROM BASE_CONTACTS WHERE " + query.Substring(0, query.Length - 3)).Tables[0];
						foreach (DataRow dr in dtaz.Rows)
						{
							ListFixedParams.Value += "C" + dr[0].ToString() + "|";
							MLFixedMails.Items.Add(new ListItem(dr[1].ToString(), "C" + dr[0].ToString()));
						}
					}
					if (drFix["lead"].ToString().Length > 0)
					{
						string query = String.Empty;
						string[] xs = drFix["lead"].ToString().Split('|');
						foreach (string s in xs)
						{
							if (s.Length > 0)
								query += "ID=" + s + " OR ";
						}
						DataTable dtaz = DatabaseConnection.CreateDataset("SELECT ID,ISNULL(COMPANYNAME,'')+' '+ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS LEAD FROM CRM_LEADS WHERE " + query.Substring(0, query.Length - 3)).Tables[0];
						foreach (DataRow dr in dtaz.Rows)
						{
							ListFixedParams.Value += "L" + dr[0].ToString() + "|";
							MLFixedMails.Items.Add(new ListItem(dr[1].ToString(), "L" + dr[0].ToString()));
						}
					}
				}


				arry.Append("ReloadParams(Companies,Contacts,Leads);</script>");
				Page.RegisterStartupScript("startarray", arry.ToString());
			}
			NewML.Visible = true;
			NewMLTable.Visible = true;
			RisearchAdvanced.Visible = true;
			MailingListRep.Visible = false;
		}

		private struct SMSNumber
		{
			public string Number;
			public string Contact;
		}

		private struct SMSListStruct
		{
			public string Email;
			public string CompanyName;
			public string Name;
			public string Surname;
			public string Address;
			public string City;
			public string Province;
			public string Nation;
			public string Zip;
			public string RefId;
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
			this.RemoveMLFill.Click += new EventHandler(removeMLFill_Click);
			this.RemoveMLFill2.Click += new EventHandler(removeMLFill_Click);
			this.RemoveMLFill3.Click += new EventHandler(removeMLFill_Click);
			this.RemoveMLFill4.Click += new EventHandler(removeMLFill_Click);
			this.Verifymail.Click += new EventHandler(VerifyMail_Click);
			this.BackToSendMail.Click += new EventHandler(BackToSendMail_Click);
			this.BtnNewML.Click += new EventHandler(this.Btn_Click);
			this.NewMLSubmit.Click += new EventHandler(this.Btn_Click);
			this.SearchAdvanced.Click += new EventHandler(this.Btn_Click);
			this.SaveML.Click += new EventHandler(this.Btn_Click);
			this.MoveOne.Click += new EventHandler(this.Btn_Click);
			this.MoveAll.Click += new EventHandler(this.Btn_Click);
			this.SendML.Click += new EventHandler(this.Btn_Click);
			this.MailingListRep.ItemCommand += new RepeaterCommandEventHandler(this.MailingListRepCommand);
			this.MailingListRep.ItemDataBound += new RepeaterItemEventHandler(this.MailingListRepItemDataBound);

		}

		#endregion

		private void removeMLFill_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "RemoveMLFill":
					for (int i = 0; i < MLFill.Items.Count; i++)
					{
						if (MLFill.Items[i].Selected)
							MLFill.Items.Remove(MLFill.Items[i--]);
					}
					break;
				case "RemoveMLFill2":
					for (int i = 0; i < MLFill2.Items.Count; i++)
					{
						if (MLFill2.Items[i].Selected)
							MLFill2.Items.Remove(MLFill2.Items[i--]);
					}
					break;
				case "RemoveMLFill3":
					for (int i = 0; i < MLFill3.Items.Count; i++)
					{
						if (MLFill3.Items[i].Selected) MLFill3.Items.Remove(MLFill3.Items[i--]);
					}
					break;
				case "RemoveMLFill4":
					for (int i = 0; i < MLFill4.Items.Count; i++)
					{
						if (MLFill4.Items[i].Selected) MLFill4.Items.Remove(MLFill4.Items[i--]);
					}
					break;
			}
			PreviewList.Visible = true;
		}

		private void VerifyMail_Click(object sender, EventArgs e)
		{
			StringBuilder er = new StringBuilder();
			er.Append("<table class=normal cellspacing=0 cellpadding=0>");

			foreach (ListItem i in MLFill.Items)
			{
				string[] xmai = i.Value.Split('|');
				if (xmai[1].Length > 0)
				{
					DataTable dt = DatabaseConnection.CreateDataset("SELECT INVOICINGADDRESS,INVOICINGCITY,INVOICINGSTATEPROVINCE,INVOICINGSTATE,INVOICINGZIPCODE FROM BASE_COMPANIES WHERE ID=" + xmai[0] + " AND MLFLAG=1 AND LIMBO=0").Tables[0];
					if (dt.Rows.Count <= 0)
						er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt44"));
					else
						er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
				}
				else
				{
					er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
				}
			}

			foreach (ListItem i in MLFill2.Items)
			{
				string[] xmai = i.Value.Split('|');
				DataTable dtB = DatabaseConnection.CreateDataset("sELECT EMAIL,NAME,SURNAME,ID,ADDRESS_1,CITY_1,PROVINCE_1,STATE_1,ZIPCODE_1,COMPANYID FROM BASE_CONTACTS WHERE ID=" + xmai[0] + " AND MLFLAG=1 AND LIMBO=0").Tables[0];
				if (dtB.Rows.Count > 0)
				{
					DataRow dt = dtB.Rows[0];
					if (dt[0].ToString().Length > 0)
					{
						er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
					}
					else
					{
						er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
					}
				}
				else
				{
					er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt44"));
				}
			}

			foreach (ListItem i in MLFill3.Items)
			{
				string[] xmai = i.Value.Split('|');
				DataRow dt = DatabaseConnection.CreateDataset("SELECT EMAIL,NAME,SURNAME,ID,ADDRESS,CITY,PROVINCE,STATE,ZIPCODE,COMPANYNAME FROM CRM_LEADS WHERE ID=" + xmai[0] + " AND LIMBO=0").Tables[0].Rows[0];
				if (dt[0].ToString().Length > 0)
				{
					er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
				}
				else
				{
					er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
				}
			}

			foreach (ListItem i in MLFill4.Items)
			{
				switch (i.Value.Substring(0, 1))
				{
					case "A":
						DataRow dr = DatabaseConnection.CreateDataset("sELECT INVOICINGADDRESS,INVOICINGCITY,INVOICINGSTATEPROVINCE,INVOICINGSTATE,INVOICINGZIPCODE,EMAIL,MLEMAIL FROM BASE_COMPANIES WHERE ID=" + int.Parse(i.Value.Substring(1))).Tables[0].Rows[0];
						if (dr["mlemail"].ToString().Length > 0 || dr["email"].ToString().Length > 0)
						{
							er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
						}
						else
						{
							er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
						}
						break;
					case "C":
						DataRow dt = DatabaseConnection.CreateDataset("SELECT EMAIL,NAME,SURNAME,ID,ADDRESS_1,CITY_1,PROVINCE_1,STATE_1,ZIPCODE_1,COMPANYID FROM BASE_CONTACTS WHERE ID=" + int.Parse(i.Value.Substring(1)) + " AND LIMBO=0").Tables[0].Rows[0];
						if (dt[0].ToString().Length > 0)
						{
							er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
						}
						else
						{
							er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
						}
						break;
					case "L":
						dt = DatabaseConnection.CreateDataset("SELECT EMAIL,NAME,SURNAME,ID,ADDRESS,CITY,PROVINCE,STATE,ZIPCODE,COMPANYNAME FROM CRM_LEADS WHERE ID=" + i.Value.Substring(1) + " AND LIMBO=0").Tables[0].Rows[0];
						if (dt[0].ToString().Length > 0)
						{
							er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>", i.Text, "Mail OK");
						}
						else
						{
							er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>", i.Text,Root.rm.GetString("MLtxt31"));
						}
						break;
				}
			}

			er.Append("</table>");
			((Label) Page.FindControl("SendError")).Text = er.ToString();
			BackToSendMail.Visible = true;
		}

		private void BackToSendMail_Click(object sender, EventArgs e)
		{
			((Label) Page.FindControl("SendError")).Visible = false;
			PreviewList.Visible = true;
			BackToSendMail.Visible = false;
		}
	}
}
