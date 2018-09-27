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
using System.Web;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.SideBar
{
	public partial class Calendar : SideBarControl
	{
        public Calendar()
        {
            this.ID = "Calendar";
        }
		protected void Page_Load(object sender, EventArgs e)
		{
			UserConfig UC=(UserConfig)HttpContext.Current.Session["UserConfig"];
            this.NoTitle = true;
			string lang = String.Empty;
			switch(UC.CultureSpecific)
			{
				case "it":
					lang = "var dayarray=new Array('D','L','M','M','G','V','S','D');var montharray=new Array('Gennaio','Febbraio','Marzo','Aprile','Maggio','Giugno','Luglio','Agosto','Settembre','Ottobre','Novembre','Dicembre');";
					break;
				case "es":
					lang = "var dayarray=new Array('D','L','M','Ms','J','V','S','D');var montharray=new Array('Enero','Febrero','Marzo','Abril','Mayo','Junio','Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre');";
					break;
				default:
					lang = "var dayarray=new Array('S','M','T','W','T','F','S','S');var montharray=new Array('January','February','March','April','May','June','July','August','September','October','November','December');";
				break;
			}
			jscript.Text=String.Format("<script>{1}var eurocal = {0};</script>",(UC.FirstDayOfWeek)?"false":"true",lang);
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
