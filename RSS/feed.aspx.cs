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
using System.Web.UI;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Rss;

namespace Digita.Tustena.RSS
{
	public partial class feed : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{

			UserConfig UC = UserData.LoadPersonalData(Request.QueryString["U"],Request.QueryString["P"],-1);
			if(UC.Logged == LoggedStatus.no)
				return;

			RssFeed feed = new RssFeed();

			feed.Version = RssVersion.RSS20;
			feed.Encoding = Encoding.UTF8;

			RssChannel rcActivities = new RssChannel();
			rcActivities.Title = "Attivit di Oggi";
			rcActivities.Description = "Attivit di Oggi";
			rcActivities.Link = new Uri("http://crm.tustena.com");
			rcActivities.LastBuildDate = DateTime.Now;
			rcActivities.Docs = "http://blogs.law.harvard.edu/tech/rss";
			rcActivities.Generator = "Tustena CRM (RSS.NET)";

			RssChannel rcLostActivities = new RssChannel();
			rcLostActivities.Title = "Attivit Scadute";
			rcLostActivities.Description = "Attivit Scadute";
			rcLostActivities.Link = new Uri("http://crm.tustena.com");
			rcLostActivities.LastBuildDate = DateTime.Now;
			rcLostActivities.Docs = "http://blogs.law.harvard.edu/tech/rss";
			rcLostActivities.Generator = "Tustena CRM (RSS.NET)";


			Today today = new Today(UC);
			DataTable activityToday = today.ActivityToday(false);

			if(activityToday.Rows.Count>0)
			{
				foreach(DataRow dr in activityToday.Rows)
				{
					RssItem item = new RssItem();

					item.Title = (dr["Subject"].ToString().Length>0)?dr["Subject"].ToString():"N/a";
					item.Description = dr["Description"].ToString();
					item.PubDate = (DateTime)dr["CreatedDate"];
					rcActivities.Items.Add(item);
				}
				feed.Channels.Add(rcActivities);
			}

			DataTable lostActivity = today.LostActivity(false);

			if(lostActivity.Rows.Count>0)
			{
				foreach(DataRow dr in lostActivity.Rows)
				{
					RssItem item = new RssItem();
					item.Title = (dr["Subject"].ToString().Length>0)?dr["Subject"].ToString():"N/a";
					item.Description = dr["Description"].ToString();
					item.PubDate = (DateTime)dr["CreatedDate"];
					rcLostActivities.Items.Add(item);
				}
				feed.Channels.Add(rcLostActivities);
			}

			Response.Clear();
			Response.ContentType = "text/xml";
			if(feed.Channels.Count>0)
				feed.Write(Response.OutputStream);
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
