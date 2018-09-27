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
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;

namespace Digita.Tustena.RenderUtils
{
	public class PageUtil
	{
		public static void SetInitialFocus(Control control)
		{
			if (control.Page == null)
			{
				throw new ArgumentException("The Control must be added to a Page before you can set the IntialFocus to it.");
			}
            if (StaticFunctions.IsClientScriptEnabled(control.Page, true))
			{
				StringBuilder s = new StringBuilder();
				s.Append("\n<SCRIPT LANGUAGE='JavaScript'>\n");
				s.Append("<!--\n");
				s.Append("function SetInitialFocus()\n");
				s.Append("{\n");
				s.Append("   document.");

				Control p = control.Parent;
				while (!(p is HtmlForm))
					p = p.Parent;
				s.Append(p.ClientID);

				s.Append("['");
				s.Append(control.UniqueID);

				RadioButtonList rbl = control as RadioButtonList;
				if (rbl != null)
				{
					string suffix = "_0";
					int t = 0;
					foreach (ListItem li in rbl.Items)
					{
						if (li.Selected)
						{
							suffix = "_" + t.ToString();
							break;
						}
						t++;
					}
					s.Append(suffix);
				}

				if (control is CheckBoxList)
				{
					s.Append("_0");
				}

				s.Append("'].focus();\n");
				s.Append("}\n");

				if (control.Page.MaintainScrollPositionOnPostBack)
					s.Append("window.setTimeout(SetInitialFocus, 500);\n");
				else
					s.Append("window.onload = SetInitialFocus;\n");

				s.Append("// -->\n");
				s.Append("</SCRIPT>");

                control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), "InitialFocus", s.ToString());
			}
		}

		public bool GetStatusNotModified()
		{
			string sPath = HttpContext.Current.Request.ServerVariables["Path_Translated"];
			return GetStatusNotModified(File.GetLastWriteTime(sPath));
		}

		public bool GetStatusNotModified(DateTime latest)
		{
			bool notModified = false;

			string etag = HttpContext.Current.Request.Headers["If-None-Match"];
			if (etag != null)
			{
				notModified = (etag.Equals(latest.Ticks.ToString()));
			}
			else
			{
				string ifModifiedSince = HttpContext.Current.Request.Headers["if-modified-since"];

				if (ifModifiedSince != null)
				{
					try
					{
						if (ifModifiedSince.IndexOf(";") > -1)
						{
							ifModifiedSince = ifModifiedSince.Split(';').GetValue(0).ToString();
						}

						DateTime ifModDate = DateTime.Parse(ifModifiedSince);

						notModified = (latest <= ifModDate);
					}
					catch
					{
					}
				}
			}

			if (notModified)
			{
				HttpContext.Current.Response.StatusCode = 304;
				HttpContext.Current.Response.SuppressContent = true;
				return true;
			}

			HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.Public);
			HttpContext.Current.Response.Cache.SetLastModified(latest);
			HttpContext.Current.Response.Cache.SetETag(latest.Ticks.ToString() + ":03");

			return false;
		}
	}
}
