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
	public class RepeaterMultiDelete : HtmlGenericControl
	{

		public RepeaterMultiDelete()
		{
		}

        public RepeaterMultiDelete(string tagName)
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
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			RepeaterItem repItem = (RepeaterItem) this.Parent;
			if(repItem.ItemType==ListItemType.Header)
			{
				LinkButton lb = new LinkButton();
				lb.Text = "<img border=0 src='/i/trash.gif' alt='"+rm.GetString("MLtxt2")+"'>";
				lb.ID = "Delete" + this.ID;
				lb.Attributes.Add("onclick", "return confirm('" + rm.GetString("DeleteGroup") + "');");

				this.linkText = "<img border=0 src='/i/trash.gif' alt='"+rm.GetString("MLtxt2")+"'>";

				this.InnerHtml = "";

				HtmlGenericControl td = new HtmlGenericControl("TD");
				td.Attributes.Add("class", CssClass);
				td.Attributes.Add("width", "20");
				td.Attributes.Add("style", "text-align:center");
				this.Controls.Add(td);
				td.Controls.Add(lb);
				lb.CommandName="MultiDeleteButton";
			}
            ((TustenaRepeater)this.Parent.Parent.Parent).useMultiDelete = true;
        }

		protected override void Render(HtmlTextWriter writer)
		{
			EnsureChildControls();
			RepeaterItem repItem = (RepeaterItem) this.Parent;
			if(repItem.ItemType!=ListItemType.Header)
			{
				writer.Write(string.Format("<td class=\"{1}\" align=\"center\" style=\"text-indent:0;text-align:center\"><input type=\"checkbox\" name=\"md_{0}\"></td>",repItem.ItemIndex,CssClass));
			}else
				foreach(Control c in Controls)
					c.RenderControl(writer);
		}

		private void lb_Command(object sender, CommandEventArgs e)
		{
			TustenaRepeater rep = (TustenaRepeater)this.Parent.Parent.Parent;
			rep.innerRepeater_ItemCommand(this, (RepeaterCommandEventArgs)e);
		}
	}
}
