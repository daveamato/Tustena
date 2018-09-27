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
using System.Web.SessionState;

namespace Digita.Tustena
{
	public class NewGoBack
	{
		private HttpSessionState Session = HttpContext.Current.Session;
		private GoBackPage[] CurrentBackPage;

		public NewGoBack()
		{
			if (Session["GoBack"] != null)
				CurrentBackPage = (GoBackPage[]) Session["GoBack"];
			else
				CurrentBackPage = new GoBackPage[5];
		}


		public void AddPage(byte pageid, string url, string[] parsname, object[] parsobj)
		{
			if (parsname.Length != parsobj.Length)
				throw new FormatException("Arrays differs");
			Hashtable mypars = new Hashtable();
			for (int i = 0; i < parsobj.Length; i++)
				mypars.Add(parsname[i], parsobj[i]);
			AddPage(pageid, url, mypars);
		}

		public void AddPage(byte pageid, string url, Hashtable pars)
		{
			GoBackPage gbp = new GoBackPage();
			gbp.pageid = pageid;
			gbp.url = url;
			gbp.pars = pars;
			for (int i = CurrentBackPage.Length; i > 0; --i)
				CurrentBackPage[i - 1] = CurrentBackPage[i];
			CurrentBackPage[5] = gbp;
		}

		public object LastPage()
		{
			if (CurrentBackPage.Length > 0)
				return CurrentBackPage[CurrentBackPage.Length];
			else
				return null;
		}

	}

	[Serializable]
	public class GoBackPage
	{
		public byte pageid;
		public string url;
		public Hashtable pars; // = new Hashtable();
	}
}
