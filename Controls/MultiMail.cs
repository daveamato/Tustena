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
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.Design;
using System.ComponentModel;

namespace Digita.Tustena.WebControls
{
    [ToolboxData("<{0}:MultiMail runat=server></{0}:MultiMail>"), ToolboxItem(typeof(WebControlToolboxItem)), Designer(typeof(ContainerControlDesigner))]
    public class MultiMail : HtmlGenericControl
	{

        public MultiMail()
        {
        }

        public MultiMail(string tagName)
        {
        }

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected override void Render(HtmlTextWriter writer)
		{
			string eMail = Attributes["email"];
			EnsureChildControls();
			TextWriter tempWriter = new StringWriter();
			if (!HasControls() || eMail==null)
			{
				writer.Write("MultiMail List can be empty, is stupid....");
			}
			foreach (Control c in Controls)
			{
				c.RenderControl(new HtmlTextWriter(tempWriter));
			}
			string[] st = eMail.Split(new char[]{';',' '});
			if(st.Length==0)
			{
				string str = string.Format(tempWriter.ToString(),"","");
				writer.Write(str);
			}
			else
			for(int i=0;i<st.Length;i++)
				{
					string str = string.Format(tempWriter.ToString(),(i==0)?"":i.ToString(),st[i]);
					writer.Write(str);
				}

		}
	}
}
