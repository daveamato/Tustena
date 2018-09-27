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
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;

namespace Digita.Tustena
{
	public partial class Language : G
	{


		protected void Page_Init(Object sender, EventArgs args)
		{
			if (!Login())
			{
				Response.Write("<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				MyUICulture.Items.Add("Choose another language");
				foreach (CultureInfo ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
				{
                    if (ConfigSettings.SupportedLanguages.IndexOf(ci.Parent.Name) > -1 && ci.Parent.Name.Length>0)
						MyUICulture.Items.Add(new ListItem((Request.Form["eng"] == "0" ? ci.NativeName : ci.EnglishName), ci.Name));
				}
				MyUICulture.SelectedValue = UC.Culture;
				MyUICulture.AutoPostBack = true;
			}
		}

		protected void Page_Load(Object sender, EventArgs args)
		{
			string SelectedCulture = MyUICulture.SelectedItem.Value;
			if (Request.Form["eng"] == "0")
			{
				EngTxt.Text = String.Format(Root.rm.GetString("Lantxt1"),Root.rm.GetString("Lantxt3"));
				EngTxt.Attributes.Add("onclick", "forms(0).eng.value=1;forms(0).submit()");
			}
			else
			{
				EngTxt.Text = String.Format(Root.rm.GetString("Lantxt1"),Root.rm.GetString("Lantxt2"));
				EngTxt.Attributes.Add("onclick", "forms(0).eng.value=0;forms(0).submit()");
			}

			TitleLbl.Text =Root.rm.GetString("Lantxt4");
			Examplelbl.Text =Root.rm.GetString("Lantxt5");
			SubmitCat.Text =Root.rm.GetString("Modify");

			if (! SelectedCulture.StartsWith("Choose"))
			{

				Thread.CurrentThread.CurrentCulture = new CultureInfo(SelectedCulture);
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(SelectedCulture);

				HttpCookie cookie = new HttpCookie("CulturePref");

				cookie.Value = SelectedCulture;

				DateTime dtNow = DateTime.Now;
				TimeSpan tsMinute = new TimeSpan(365, 0, 0, 0);
				cookie.Expires = dtNow + tsMinute;
				Response.Cookies.Add(cookie);

				UC.Culture = Thread.CurrentThread.CurrentCulture.ToString();
				UC.CultureSpecific = CultureInfo.CurrentUICulture.Name.Substring(CultureInfo.CurrentUICulture.Name.Length - 2).ToLower();
				UC.myDTFI = new CultureInfo(UC.Culture).DateTimeFormat;
				Session.Remove("InitScript");
				Session["UserConfig"] = UC;

			}
			Example.Text = DateTime.Now.ToString("U");
			try
			{
				Trace.Warn("1", Thread.CurrentThread.CurrentUICulture.Parent.Name);
			}
			catch (Exception)
			{
				Trace.Warn("2", Thread.CurrentThread.CurrentUICulture.Name);
			}

		}

		private void btn_Click(object sender, EventArgs e)
		{
			if(Cache["template."+UC.UserId] is string)
			{
				Cache.Remove("template."+UC.UserId);
			}
			ClientScript.RegisterStartupScript(this.GetType(), "Closeme", "<script>opener.location.href='/today.aspx';self.close();</script>");
		}
		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Init+=new EventHandler(Page_Init);
			this.Load += new EventHandler(this.Page_Load);
			this.SubmitCat.Click+=new EventHandler(btn_Click);
		}

		#endregion

	}
}
