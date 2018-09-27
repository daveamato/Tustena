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

using System.Web.UI;
using System.Web.UI.HtmlControls;
using Digita.Tustena.Core;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Digita.Tustena.WebControls
{
    [ToolboxData("<{0}:TustenaTab runat=server></{0}:TustenaTab>"), ToolboxItem(typeof(WebControlToolboxItem)), Designer(typeof(ContainerControlDesigner))]
    public class TustenaTab : HtmlGenericControl
	{

        public TustenaTab()
		{
		}

        public TustenaTab(string tagName)
		{
		}

        public string LangHeader
		{
			set{ViewState[this.ID+"_header"] = Root.rm.GetString(value);}
		}
		public string Header
		{
			get
			{
				object o = this.ViewState[this.ID+"_header"];
				if (o == null)
				{
					return ID;
				}
				else
					return (string) o;
			}
			set{ViewState[this.ID+"_header"] = value;}
		}

		public bool IsSelected
		{
			get
			{
				return Selected;
			}
		}

		internal bool Selected
		{
			get
			{
				return (((TustenaTabber) this.Parent).Selected == this.ID);
			}
			set
			{
				ViewState[this.ID+"_selected"] = this.ID;
			}
		}

		bool clientSide;
		public bool  ClientSide
		{
			get{return clientSide;}
			set{clientSide = value;}
		}

		protected override void Render(HtmlTextWriter writer)
		{
			TustenaTabber tt = (TustenaTabber) this.Parent;
			if(tt.Plain)
			{
				this.Attributes.Add("class","plainPage");
				writer.WriteLine("<div class=\"plainTitle\">"+Header+"</div>");
				base.Render(writer);
				return;
			}
			else if(tt.selectedTabName != this.ID)
			{
				this.Attributes.Add("style","display:none");
			}
			if(ClientSide || tt.selectedTabName==ID)
				base.Render(writer);
		}
	}
}
