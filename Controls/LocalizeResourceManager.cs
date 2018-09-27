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
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;

namespace Digita.Tustena.WebControls
{

	public class LocalizedButton : Button
	{
		protected override void Render(HtmlTextWriter writer)
		{
			Text = Root.rm.GetString(Text);
			base.Render(writer);
		}
	} // end class LocalizedButton

	public class LocalizedLinkButton : LinkButton
	{
		protected override void Render(HtmlTextWriter writer)
		{
			Text = Root.rm.GetString(Text);
			base.Render(writer);
		}
	} // end class LocalizedButton

    public class LocalizedScript : Control
    {
        private string Resource;
        public string resource
        {
            set { Resource = value; }
        }
		protected override void Render(HtmlTextWriter writer)
		{
            if (Resource.IndexOf(',') > -1)
            {
                string ret = string.Empty;
                foreach (string sArr in Resource.Split(','))
                    ret += sArr + "='" + Core.Root.rm.GetString(sArr) + "';\r\n";
                writer.Write(string.Format("<script>{0}</script>", ret));
            }
            else
            {
                string scriptvalue=Core.Root.rm.GetString(Resource).Replace("'","\\'");
                writer.Write(string.Format("<script>{0}='{1}';</script>", Resource, scriptvalue));
            }
        }
    } // end class LocalizedHtmlAnchor


	public class LocalizedHtmlInputButton : HtmlInputButton
	{
		protected override void Render(HtmlTextWriter writer)
		{
			Value = Root.rm.GetString(Value);
			base.Render(writer);
		}
	} // end class LocalizedHtmlInputButton

	[ToolboxData("<{0}:LocalizedLabel runat=server></{0}:LocalizedLabel>")]
	public class LocalizedLabel : Label
	{
		protected override void Render(HtmlTextWriter writer)
		{
			Text = Root.rm.GetString(Text);
			base.Render(writer);
		}
	} // end class LocalizedLabel

	public class LocalizedHtmlAnchor : HtmlAnchor
	{
		protected override void Render(HtmlTextWriter writer)
		{
			InnerText = Root.rm.GetString(InnerText);
			base.Render(writer);
		}
	} // end class LocalizedHtmlAnchor

	public class LocalizedLiteral : Literal
	{
		protected override void Render(HtmlTextWriter writer)
		{
			Text = Root.rm.GetString(Text);
			base.Render(writer);
		}
	} // end class LocalizedLiteral

	public class LocalizedDiv : Control
	{

		private string Text;
		public string text
		{
			set{Text=value;}
		}
		private string Style = null;
		public string style
		{
			set{Style=value;}
		}
		private string CssClass = null;
		public string cssClass
		{
			set{CssClass=value;}
		}
		private string Width = null;
		public string width
		{
			set{Width=value;}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("<div");
			if(Style!=null)
				sb.AppendFormat(" style=\"{0}\"",Style);
			if(CssClass!=null)
				sb.AppendFormat(" class=\"{0}\"",CssClass);
			if(Width!=null)
				sb.AppendFormat(" width=\"{0}\"",Width);
			sb.AppendFormat(">{0}</div>",Root.rm.GetString(Text));
			writer.Write(sb.ToString());
		}
	}

	public class LocalizedImg : HtmlImage
	{
		private string controlonclick = null;
		public string ControlOnClick
		{
			get { return controlonclick; }
			set { controlonclick = value; }
		}

		private string getContainerControlID
		{
			get
			{
				int pos = 0;
				if((pos = this.ClientID.IndexOf("_"))>-1)
					return this.ClientID.Substring(0,pos+1);
				return string.Empty;
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			if(controlonclick!=null)
			{
				this.Attributes.Add("onclick",string.Format(controlonclick,getContainerControlID));
				this.Attributes.Remove("controlonclick");
			}
		}
		protected override void Render(HtmlTextWriter writer)
		{
			Alt = Root.rm.GetString(Alt);
			base.Render(writer);
		}
	} // end class LocalizedImg
}
