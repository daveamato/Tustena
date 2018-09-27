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
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.MailingList.webmail
{
	public partial class VerifyMailInML : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "","<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				NewMailingList nm = new NewMailingList(UC);
				int mlId = int.Parse(Request.QueryString["id"]);
				string queryCompany = nm.GetCompanyQuery(mlId);
				string queryContact = nm.GetContactQuery(mlId);
				string queryLead = nm.GetLeadQuery(mlId);
				string queryCompanyFixed = String.Empty;
				if(nm.FixedMailQuery(mlId,"company", out queryCompanyFixed) && queryCompanyFixed.Length>0)
				{
					queryCompanyFixed="SELECT CAST(BASE_COMPANIES.ID AS VARCHAR(12))+'|'+CASE WHEN ISNULL(BASE_COMPANIES.MLEMAIL,'')='' THEN ISNULL(BASE_COMPANIES.EMAIL,'') ELSE BASE_COMPANIES.MLEMAIL END+'|'+BASE_COMPANIES.COMPANYNAME AS IDMAIL,COMPANYNAME FROM BASE_COMPANIES WHERE " + queryCompanyFixed.Substring(0, queryCompanyFixed.Length - 3);
				}
				string queryContactFixed = String.Empty;
				if(nm.FixedMailQuery(mlId,"contact",out queryContactFixed)&& queryContactFixed.Length>0)
				{
					queryContactFixed="SELECT CAST(BASE_CONTACTS.ID AS VARCHAR(12))+'|'+CASE WHEN ISNULL(BASE_CONTACTS.MLEMAIL,'')='' THEN ISNULL(BASE_CONTACTS.EMAIL,'') ELSE BASE_CONTACTS.MLEMAIL END+'|'+BASE_CONTACTS.NAME+' '+BASE_CONTACTS.SURNAME AS IDMAIL,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS COMPANYNAME FROM BASE_CONTACTS WHERE " + queryContactFixed.Substring(0, queryContactFixed.Length - 3);
				}
				string queryLeadFixed = String.Empty;
				if(nm.FixedMailQuery(mlId,"lead",out queryLeadFixed)&& queryLeadFixed.Length>0)
				{
					queryLeadFixed="SELECT CAST(CRM_LEADS.ID AS VARCHAR(12))+'|'+ISNULL(CRM_LEADS.EMAIL,'')+'|'+ISNULL(CRM_LEADS.COMPANYNAME,'')+' '+ISNULL(CRM_LEADS.NAME,'')+' '+ISNULL(CRM_LEADS.SURNAME,'') AS IDMAIL,ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' '+ISNULL(COMPANYNAME,'') AS COMPANYNAME2 FROM CRM_LEADS WHERE " + queryLeadFixed.Substring(0, queryLeadFixed.Length - 3);
				}
				StringBuilder queryChain = new StringBuilder();
				if(queryCompany.Length>0)queryChain.AppendFormat("{0};",queryCompany);
				if(queryContact.Length>0)queryChain.AppendFormat("{0};",queryContact);
				if(queryLead.Length>0)queryChain.AppendFormat("{0};",queryLead);
				if(queryCompanyFixed.Length>0)queryChain.AppendFormat("{0};",queryCompanyFixed);
				if(queryContactFixed.Length>0)queryChain.AppendFormat("{0};",queryContactFixed);
				if(queryLeadFixed.Length>0)queryChain.AppendFormat("{0};",queryLeadFixed);
				if(queryChain.Length==0)
				{
					MLSendError.Text=Root.rm.GetString("NoMail");
						return;
				}
				DataSet ds =DatabaseConnection.CreateDataset(queryChain.ToString());

				StringBuilder er = new StringBuilder();
				er.Append("<table class=normal cellspacing=3 cellpadding=0>");
				bool found = false;
				foreach(DataTable dt in ds.Tables)
				{
					foreach(DataRow dr in dt.Rows)
					{
						try
						{
							found=true;
							string[] xm = dr[0].ToString().Split('|');
							if(xm.Length>1&&xm[1].Length<=0)
								er.AppendFormat("<tr><td style=\"color:red\">{0}</td><td style=\"color:red\">{1}</td></tr>",xm[2], Root.rm.GetString("MLtxt31"));
							else
								er.AppendFormat("<tr><td style=\"color:black\">{0}</td><td style=\"color:black\">{1}</td></tr>",xm[2],"Mail OK");
						}catch{}

					}
				}
				er.Append("</table>");
				if(found)
					MLSendError.Text = er.ToString();
				else
					MLSendError.Text=Root.rm.GetString("NoMail");
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
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}
