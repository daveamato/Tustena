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
using System.Web;
using System.Web.UI;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using System.Data;
namespace Digita.Tustena.Common
{
	public partial class jsrsextlink : Page
	{
		private Hashtable hs = new Hashtable();
        private UserConfig UC;
		protected void Page_Load(object sender, EventArgs e)
		{
			Response.ContentType = "text/javascript";
			if((UserConfig)HttpContext.Current.Session["UserConfig"]==null) return;
			UC = (UserConfig)HttpContext.Current.Session["UserConfig"];
            FillList();
		}

		public void FillList()
		{
            DataTable dtattach = DatabaseConnection.CreateDataset(String.Format("SELECT NAME,URL FROM LINKS WHERE COUNTRY='{0}'", UC.CultureSpecific.ToString())).Tables[0];
            if(dtattach.Rows.Count>0)
				{
					hs.Add(dtattach.Rows[0]["name"].ToString(),dtattach.Rows[0]["url"].ToString());
				}
				else
                    hs.Add(Core.Root.rm.GetString("ErpConftxt12"), "");
		}

		public string casecode()
		{
			string txt = String.Empty;
			int i = 0;
			foreach (string str in hs.Values)
				txt += String.Format("case {0}: url = \"{1}\"; break;\r\n", i++, str);
			return txt;
		}
		public string menucode()
		{
			string txt = String.Empty;
			int i = 0;
			foreach (string str in hs.Keys)
				txt += String.Format("txt+= \"<div class=\\\"menuitems\\\" onclick=\\\"javascript:extlaunch({0})\\\">&nbsp;&nbsp;{1}&nbsp;&nbsp;</div>\";\r\n", i++, str);
			return txt;
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
		}

		#endregion
	}
}
