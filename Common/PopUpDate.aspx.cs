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
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Digita.Tustena
{
	public partial class PopUpDate : Page
	{

		public void Page_Load(object sender, EventArgs e)
		{
			control.Value = Request.QueryString["textbox"].ToString();
            if (Request.QueryString["event"] != null)
                ViewState["eventFunction"] = Request.QueryString["event"].ToString();

			try
			{
				if (Request.QueryString["Start"] != null)
					if (Request.QueryString["Start"].Length > 0)
						calDate.TodaysDate = Convert.ToDateTime(Request.QueryString["Start"]);


			}
			catch
			{
			}
		}

		protected void Change_Date(object sender, EventArgs e)
		{
			string strScript;
			if (Request.QueryString["frame"] != null)
			{
				strScript = "<script>opener.SetParams('" + control.Value + "','" + calDate.SelectedDate.ToShortDateString() + "');" + Environment.NewLine;
			}
			else
			{
				strScript = "<script>dynaret('" + control.Value + "').value = '";
				strScript += calDate.SelectedDate.ToShortDateString() + "';" + Environment.NewLine;
				strScript += "var target = (opener)?window.opener:parent;" + Environment.NewLine;

				if (Request.QueryString["ISO"] != null)
				{
					strScript += "target.DatePosition('" + calDate.SelectedDate.ToString(@"yyyyMMdd") + "','" + control.Value + "');" + Environment.NewLine;
				}
				if (Request.QueryString["datediff"] != null)
				{
					if (DateTime.Now >= calDate.SelectedDate)
						strScript += "target.CalendarDateDiff(0);" + Environment.NewLine;
					else
						strScript += "target.CalendarDateDiff(1);" + Environment.NewLine;
				}
			}

			strScript += "</" + "script>";

			strScript += "<script>";
			strScript += "self.close();" + Environment.NewLine;
            if (ViewState["eventFunction"] != null)
                strScript += "dynaevent('" + ViewState["eventFunction"] + "');" + Environment.NewLine;
			strScript += "	parent.HideBox();" + Environment.NewLine;
			strScript += "</" + "script>";

			ClientScript.RegisterStartupScript(this.GetType(), "anything", strScript);
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
