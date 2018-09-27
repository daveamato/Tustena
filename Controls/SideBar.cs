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
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.Web.UI.Design;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace Digita.Tustena.WebControls
{
    [ToolboxData("<{0}:SideBarContainer runat=server></{0}:SideBarContainer>"), ToolboxItem(typeof(WebControlToolboxItem)), Designer(typeof(ContainerControlDesigner))]
	public class SideBarContainer : HtmlGenericControl
	{
        private bool auto = false;
        public bool Auto { set { auto = value; } }

        private string excludeControls = string.Empty;
        public string exclude
        {
            set { excludeControls = value; }
        }
        public SideBarContainer()
        {
        }

		public SideBarContainer(string tagName)
		{
		}

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
		}

		public static void RemapModules()
		{
			SideBarModule[] sbma = ((SideBarModule[])HttpContext.Current.Application["modules"]);
			if(sbma==null)
				sbma = InitModules();
			ArrayList sbmArrList = new ArrayList();
			foreach(SideBarModule sbm in sbma)
			{
				if(sbm.moduleId!="")
					sbmArrList.Add(sbm);
			}
			HttpContext.Current.Session["modules"] = sbmArrList.ToArray(typeof(SideBarModule));
		}

		public static SideBarModule[] InitModules()
		{
            XmlDocument xml = new XmlDocument();
            xml.Load(HttpContext.Current.Server.MapPath("/SideBar/SideBar.xml"));
            XPathNavigator nav = xml.CreateNavigator();
            nav.MoveToRoot();
            XPathExpression expr = nav.Compile("/controls/control");
            expr.AddSort("@Order", XmlSortOrder.Ascending, XmlCaseOrder.None, "", XmlDataType.Number);
            XPathNodeIterator it = nav.Select(expr);
            SideBarModule[] Module = new SideBarModule[it.Count];
            int i = 0;
            while (it.MoveNext())
            {
                XmlElement xmlEl = ((IHasXmlNode)it.Current).GetNode() as XmlElement;
                Module[i] = new SideBarModule(xmlEl.GetAttribute("ID"),string.Format("~/SideBar/{0}.ascx",xmlEl.GetAttribute("File")));
                i++;
            }
			HttpContext.Current.Application["modules"]=Module;
			return Module;
		}

		protected override void Render(HtmlTextWriter writer)
		{
			if (auto && HttpContext.Current.Session["modules"] != null)
				foreach (SideBarModule controlName in (SideBarModule[]) HttpContext.Current.Session["modules"])
				{
					LoadControl(controlName.filePath, controlName.moduleId);
				}

			EnsureChildControls();
			base.RenderBeginTag(writer);
			bool started = false;
			bool breakAfterControl = true;
			writer.Write("<table width=\"98%\" border=0 cellspacing=0 cellpadding=0>");
			if (!HasControls())
			{
				writer.Write("SideBarControl List can be empty");
			}
			foreach (Control c in Controls)
			{
				if (breakAfterControl)
				{
					if (started)
						writer.Write("<tr><td>&nbsp;</td></tr><tr><td class=\"sideContainer\">");
					else
					{
						writer.Write("<tr><td class=\"sideContainer\">");
						started = true;
					}
					breakAfterControl = false;
				}

				if (c is SideBarControl && ((SideBarControl) c).StandAlone)
				{
                    if (!((SideBarControl)c).Active) continue;
                    if (!((SideBarControl)c).NoTitle)
                    {
                        writer.Write(String.Format("<div class=\"sideTitle\">{0}</div>", (((SideBarControl)c).Title != null) ? ((SideBarControl)c).Title : c.ID));
                    }
                    if (!((SideBarControl)c).NoBreak)
                        breakAfterControl = true;
                    c.RenderControl(writer);
					writer.Write("</td></tr>");
				}
				else if (c is SideBarDivider)
				{
					writer.Write("</td></tr>");
					breakAfterControl = true;
				}
                else if (c is SideBarPlaceHolder)
                {
                }
                else if (c is SideBarObject)
                {
                    c.Controls[0].RenderControl(writer);
                }
                else
                    c.RenderControl(writer);
			}

			writer.Write("</td></tr></table>");
			base.RenderEndTag(writer);
		}

		private bool RenderExcluded(string id)
		{
			if (id == null)
				return true;
			foreach (string excluded in excludeControls.Split(';'))
			{
				if (excluded == id)
					return true;
			}
			return false;
		}
        private int controlToAddIdx = -1;
		private void LoadControl(string ctrlName, string id)
		{
            if (excludeControls.IndexOf(id) == -1)
            {
                SideBarControl sbc;
                try
                {
                    sbc = (SideBarControl)Page.LoadControl(ctrlName);
                }
                catch (HttpException ex)
                {
                    G.SendError("[Tustena] SideBar LoadControl", ex.Message);
                    return;
                }
                if (sbc.AddTo != null)
                {
                    if (controlToAddIdx == -1)
                    {
                        for (int i = 0; i < this.Controls.Count; i++)
                            if (this.Controls[i] is SideBarPlaceHolder && sbc.AddTo == this.Controls[i].ID)
                            {
                                this.Controls.AddAt(i, sbc);
                                controlToAddIdx = i;
                                break;
                            }
                    }
                    else
                    {
                        this.Controls.AddAt(controlToAddIdx, sbc);
                    }
                }
                else
                {
                    this.Controls.Add(sbc);
                }
            }
		}
	}

	[Serializable]
	public class SideBarModule
	{
		public string moduleId = String.Empty;
		public string filePath = String.Empty;

		public SideBarModule()
		{
		}

		public SideBarModule(string moduleId, string filePath)
		{
			this.moduleId = moduleId;
			this.filePath = filePath;
		}
	}

	public class SideBarDivider : UserControl
	{
		public SideBarDivider()
		{
		}
	}

    public class SideBarPlaceHolder : UserControl
    {
        public SideBarPlaceHolder()
        {
        }
    }

    public class SideBarObject : UserControl
    {
        protected override void  OnInit(EventArgs e)
        {
            SideBarControl sbc = (SideBarControl)Page.LoadControl(controlName);
            this.Controls.Add(sbc);
            base.OnInit(e);
        }
        private string controlName;
        public string ControlName
        {
            set { controlName = "~/SideBar/" + value + ".ascx"; }
        }
    }

	public class SideBarControl : UserControl
	{
        private bool standAlone = true;
        public bool StandAlone
		{
            get { return standAlone; }
            set { standAlone = value; }
		}

        private int order = 0;
        public int Order
        {
            get { return order; }
            set { order = value; }
        }

        private bool noTitle = false;
        public bool NoTitle
        {
            get { return noTitle; }
            set { noTitle = value; }
        }
        private bool noBreak = false;
        public bool NoBreak
        {
            get { return noBreak; }
            set { noBreak = value; }
        }

        private string title = null;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string addTo = null;
        public string AddTo
        {
            get { return addTo; }
            set { addTo = value; }
        }

        public virtual bool Active
		{
            get { return true; }

		}
	}

}
