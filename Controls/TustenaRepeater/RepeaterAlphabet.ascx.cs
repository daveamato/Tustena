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
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Digita.Tustena.WebControls
{
	public class RepeaterAlphabet : UserControl
	{
		protected Literal ABCLiteral;
		protected Literal ABCSelected;
		protected LinkButton filterList;
		protected LinkButton removeFilter;
		protected HtmlTableCell alphaListtd;
		protected PlaceHolder searchButton;
		string colName=null;
		private ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		private void Page_Load(object sender, EventArgs e)
		{
			if(!Page.ClientScript.IsStartupScriptRegistered("selABC"))
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"selABC", "<script>function SelABCHeader(o,id){document.getElementById(id+\"ABCSelected\").value=o;document.forms[0].submit();}</script>");
			TustenaRepeater ar = (TustenaRepeater) this.Parent;

			ABCSelected.Text = "<input id=\""+this.ClientID+"ABCSelected\" name=\""+this.ClientID+"ABCSelected\" style=\"DISPLAY:none\">";

			string ABCSel = Request.Form[this.ClientID+"ABCSelected"];
            if (Digita.Tustena.Core.StaticFunctions.IsBlank(ABCSel) && ar.FilterValue.Length==1)
                ABCSel = ar.FilterValue;
			ABCLiteral.Text = ABCHeaderHtml(ABCSel, ar.AllowSearching);
			if(Page.IsPostBack && ABCSel!=null && ABCSel.Length>0){
					ar.RepeaterPostEvent();
					if(ABCSel=="!")
						ar.rs.Visible = true;
					else
						setFilter(ABCSel);
				}
		}

		private string ABCHeaderHtml(string mylist, bool search)
		{
			StringBuilder abcHtml = new StringBuilder();
			string list = " ";
			string bgColor;
			if (mylist != null && mylist.Length>0) list = mylist;
			abcHtml.Append("<div id=AlfabetoRow align=center>");
			for (int x = 65; x <= 90; x++)
			{
				char ch = (char) x;
				if (list.Substring(0, 1) == "*" || ch.ToString() == list.Substring(0, 1))
				{
					bgColor = "class=\"AlfabetoSelected\"";
				}
				else
				{
					bgColor = String.Empty;
				}
				abcHtml.AppendFormat("&nbsp;<SPAN {0} onclick=\"SelABCHeader('{1}','{2}')\">{1}</SPAN>&nbsp;|", bgColor, Convert.ToChar(x),this.ClientID);
			}
			abcHtml.AppendFormat("&nbsp;<span onclick=\"SelABCHeader('*','{1}')\">{0}</span>", rm.GetString("Reftxt2"),this.ClientID);
			if(search)
				abcHtml.AppendFormat("&nbsp;|&nbsp;<span onclick=\"SelABCHeader('!','{1}')\">{0}</span>", "filter", this.ClientID);
			abcHtml.Append("</div>");
			return abcHtml.ToString();
		}



		private void setFilter(string ABCSel)
		{
			TustenaRepeater ar = (TustenaRepeater) this.Parent;


				if(ABCSel=="*")
				{
					ar.RemoveFilter();
				}

				if (ar.rs != null)
				{
					ar.rs.Visible = false;
					ar.DoSearch(ar.rs.SearchCols.Items[ar.rs.SearchCols.SelectedIndex].Value, ABCSel, true);
				}
				else if(ar.ra != null)
				{
					Repeater r = (Repeater) this.Parent.FindControl(this.Parent.ClientID + "_Repeater");
					colName = ar.FilterCol;
					populateSearchList(r);

					ar.DoSearch(colName, ABCSel, true);
				}


		}
		private void populateSearchList(Control p)
		{
			foreach (Control c in p.Controls)
			{
				string cType = c.GetType().Name;
				if (cType == "RepeaterColumnHeader" && colName == null)
				{
					colName = ((RepeaterColumnHeader) c).DataCol;
					if (colName!=null)
					{
						break;
					}
				}
				else if (c.HasControls())
				{
					populateSearchList(c);
				}
			}

		}
		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}


		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion


	}
}
