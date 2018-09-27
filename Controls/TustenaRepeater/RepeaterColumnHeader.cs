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
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Digita.Tustena.WebControls
{
    [ToolboxData("<{0}:RepeaterMultiDelete runat=server></{0}:RepeaterMultiDelete>"), ToolboxItem(typeof(WebControlToolboxItem)), Designer(typeof(ContainerControlDesigner))]
	public class RepeaterColumnHeader : HtmlGenericControl
	{

        public RepeaterColumnHeader()
        {
        }

        public RepeaterColumnHeader(string tagName): base(tagName)
        {
        }

 	    private string linkText = "";

		public string LinkText
		{
			get { return linkText; }
			set { linkText = value; }
		}

		private string cssClass;

		public string CssClass
		{
			get { return cssClass; }
			set { cssClass = value; }
		}


		private string dataCol;

		public string DataCol
		{
			get { return dataCol; }
			set { dataCol = value; }
		}

		private string resource = null;

		public string Resource
		{
			get { return resource; }
			set { resource = value; }
		}

		private string width = null;

		public string Width
		{
			get { return width; }
			set { width = value; }
		}
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			base.CreateChildControls();
			if (this.DataCol != null)
			{
				RepeaterHeaderLink lb = new RepeaterHeaderLink();
				if(resource!=null)
				{
					ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];
					lb.Text = rm.GetString(resource);
				}
				else
					lb.Text = this.InnerHtml;
				lb.CssClass = "HeaderLink";
				lb.ID = "Headerlink" + this.ID;
				lb.DataCol = this.DataCol;

				this.linkText = lb.Text;

				this.InnerHtml = "";

				HtmlTableCell td = new HtmlTableCell("TD");
				td.Attributes.Add("class", CssClass);
				td.Width=this.width;
				this.Controls.Add(td);
				td.Controls.Add(lb);
				lb.Click += new EventHandler(((TustenaRepeater) this.Parent.Parent.Parent).HeaderClick);
			}
			else
			{
				Label lbl = new Label();
				lbl.Text = this.InnerHtml;
				lbl.CssClass = "HeaderText";
				lbl.ID = "HeaderText" + this.ID;

				this.InnerHtml = "";

				HtmlTableCell td = new HtmlTableCell("TD");
				td.Attributes.Add("class", CssClass);
				td.Width=this.width;
				this.Controls.Add(td);
				td.Controls.Add(lbl);
			}


		}

		protected override void Render(HtmlTextWriter writer)
		{
			EnsureChildControls();
				foreach(Control c in Controls)
					c.RenderControl(writer);
		}
	}
}
