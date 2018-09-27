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
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web.UI.Design;

namespace Digita.Tustena.WebControls
{

	public delegate void TabClickDelegate(string tabId);
    [ToolboxData("<{0}:TustenaTabber runat=server></{0}:TustenaTabber>"), ToolboxItem(typeof(WebControlToolboxItem)), Designer(typeof(ContainerControlDesigner))]
    public class TustenaTabber : HtmlGenericControl
	{
		public event TabClickDelegate TabClick;

		internal string selectedTabName = String.Empty;

		public TustenaTabber()
		{
		}

        public TustenaTabber(string tagName)
		{
		}

		string editTab = null;
		public string EditTab
		{
			get{return editTab;}
			set{editTab = value;}
		}

		string hideTabs = string.Empty;
		public string HideTabs
		{
			get{return hideTabs;}
			set{hideTabs = value;}
		}

		public string Selected
		{
			get
			{
				object o = this.ViewState[this.ID+"_selected"];
				if (o == null)
				{
					return null;
				}
				else
					return (string) o;
			}
			set{ViewState[this.ID+"_selected"] = value;}
		}
		bool plain = false;
		public bool Plain
		{
			get{return plain;}
			set{plain = value;}
		}

        bool expand = false;
        public bool Expand
        {
            get { return expand; }
            set { expand = value; }
        }

		string width;
		public string Width
		{
			get{return width;}
			set{width = value;}
		}

		public Hashtable Tabs = new Hashtable();

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.Attributes.Add("style","width:"+width);
			this.Attributes.Add("class","tabber");
		}

		protected override void OnLoad(EventArgs e)
		{
			if(Page.IsPostBack && Page.Request.Form["TTPostClick"] != null && Page.Request.Form["TTPostClick"].Length>0)
				TabClicked(Page.Request.Form["TTPostClick"].ToString());
		}

		public void TabClicked(String TabId)
		{
			if(TabId=="expand")
			{
				plain = true;
				return;
			}
            else if(TabId.StartsWith("!"))
			{
                TabId = TabId.Substring(1);
            }
            else
            {
			if(TabClick!=null)
				TabClick(TabId);
            }
			selectedTabName = TabId;
			if(selectedTabName.Length!=-1)
				Selected = TabId;
		}

		private string ControlSubControl
		{
			get
			{
				   if(this.ID!=this.ClientID)
					   return "\""+this.ClientID.Replace(this.ID,"") + "\"+";
				   return string.Empty;
			}
		}

		private void JsScript(HtmlTextWriter writer, string currentServerTabs)
		{
			string script="<script>var serverTabs='"+currentServerTabs+"'; var selectedTabName='"+selectedTabName+ @"';
function expandTabs(){
		document.getElementById('TTPostClick').value = 'expand';
		document.forms[0].submit();
		return false;
}
function switchTab(id, isServer){
	if(isServer && serverTabs.indexOf(id)==-1){
		document.getElementById('TTPostClick').value = id;
		document.forms[0].submit();
		return false;
	}else{
		document.getElementById('TTPostClick').value = '!'+id;
    }

	var curSpanTab = document.getElementById(" + ControlSubControl+@"id);
	var spanTab = document.getElementById("+ControlSubControl+@"selectedTabName);
	var oldTab = document.getElementById(selectedTabName+""_header"");
	var CurTab = document.getElementById(id+""_header"");
	if(oldTab == CurTab) return;
	curSpanTab.style.display='';
	spanTab.style.display='none';
	CurTab.className='tabberHi';
	oldTab.className='';
	selectedTabName=id;
}</script>";
			writer.WriteLine(script);

		}

		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		protected override void RenderChildren(HtmlTextWriter writer)
		{
			EnsureChildControls();
			StringBuilder sb = new StringBuilder();
			if(!plain)
			{
				Control RightControl=null;
				int tabCount = 0;
				bool isServerSide = false;
				string currentServerTabs = string.Empty;
				foreach (Control c in Controls)
				{
					if(c is TustenaTab)
					{
						string tabID = c.ID;
						if(Selected==null)
							Selected=tabID;
                        TustenaTab tt = c as TustenaTab;
                        if ((editTab != null && editTab != tabID) || (hideTabs.Length > 0 && hideTabs.IndexOf(tabID) >= 0))
                        {
                            tt.Visible = false;
                            continue;
                        }
						string cssClass = "";
						if((selectedTabName.Length == 0 && tt.Selected) || tabID==selectedTabName)
						{
							selectedTabName = tabID;
							cssClass = " class=\"tabberHi\"";
							tt.Visible = true;
						}
						if(!tt.ClientSide)
						{
							isServerSide = true;
							if(tt.ID==selectedTabName)
								currentServerTabs=selectedTabName;
							else
								tt.Visible = false;

						}
						else
							isServerSide = false;
						if(editTab == tabID)
							sb.AppendFormat("<li><a href=\"javascript:void(0)\" id=\"{1}_header\" class=\"tabberHi\"><span>{0}</span></a>\n",tt.Header,tabID);
						else
							sb.AppendFormat("<li><a href=\"javascript:switchTab('{2}',{3})\" id=\"{2}_header\"{1}><span>{0}</span></a>\n",tt.Header,cssClass,tabID,isServerSide.ToString().ToLower());

						tabCount++;
					}
					else if(c is TustenaTabberRight)
					{
						RightControl=c;
					}
				}
				if(editTab==null)
					JsScript(writer,currentServerTabs);
				writer.Write(string.Format("<ul>{0}</li>",sb.ToString()));
				if(RightControl!=null)
				{
					RightControl.RenderControl(writer);
					RightControl.Visible=false;
					if(expand && editTab==null)
						writer.Write("<span id=\"tabExpand\" onclick=\"expandTabs()\"><span class=\"save\"><img src=\"/images/expand.gif\" border=0></span></span>");
				}
				writer.WriteLine(string.Format("</ul></span><span style=\"width:{0}\" id=\"tabberpage\">",width));
				base.RenderChildren(writer);
					writer.WriteLine("<input type=\"hidden\" id=\"TTPostClick\" name=\"TTPostClick\">");
			}
			else
			{

				foreach (Control c in Controls)
				{
					if(c is TustenaTab)
					{
						TustenaTab tt = c as TustenaTab;
						tt.Visible = true;
						if(!tt.ClientSide && TabClick!=null)
							TabClick(tt.ID);
					}
				}
				base.RenderChildren(writer);
			}
		}
	}
}
