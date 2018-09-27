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
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class NoMoreHelp : G
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DatabaseConnection.DoCommand("delete from MenuMap where UserID="+int.Parse(Request.QueryString["user"])+" and MenuID="+int.Parse(Request.QueryString["menu"]));

				if(Request.QueryString["menu"]!=Request.QueryString["menudef"])
				{
					DataRow dr = DatabaseConnection.CreateDataset("select id,'/'+folder+'/'+link+'&si='+cast(id as varchar) as pagedefault from newmenu where id="+int.Parse(Request.QueryString["menudef"])).Tables[0].Rows[0];
					DatabaseConnection.DoCommand("insert into MenuMap (UserID,MenuID,FirstTime,NewHomePage,NewHomePageID) values ("+int.Parse(Request.QueryString["user"])+","+int.Parse(Request.QueryString["menu"])+",1,'"+dr["pagedefault"].ToString()+"',"+dr["id"].ToString()+")");
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
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
