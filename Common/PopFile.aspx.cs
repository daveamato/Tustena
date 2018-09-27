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
using System.Data;
using System.Text;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Microsoft.Web.UI.WebControls;
using TreeView = Microsoft.Web.UI.WebControls.TreeView;
using TreeNode = Microsoft.Web.UI.WebControls.TreeNode;

namespace Digita.Tustena
{
	public partial class PopFile : G
	{
		string ext = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				string js;
				string control = Request.QueryString["textbox"].ToString();
				string control2 = Request.QueryString["textboxID"].ToString();

				string clickControl=null;
				string eventFunction=null;
				if(Request.QueryString["click"]!=null)
					clickControl = Request.QueryString["click"].ToString();
				if(Request.QueryString["event"]!=null)
					eventFunction = Request.QueryString["event"].ToString();

				if(Request.QueryString["extfilter"]!=null)
					ext=Request.QueryString["extfilter"].ToString();

				js = "<script>";
				js += "function SetRef(id,tx){";
				if (Request.QueryString["frame"] != null)
				{
					js += "	opener.SetParams('" + control + "',tx);";
					js += "	opener.SetParams('" + control2 + "',id);";
				}
				else
				{
					js += "	dynaret('" + control + "').value=tx;";
					js += "	dynaret('" + control2 + "').value=id;";
				}
				js += "	self.close();";
				if(clickControl!=null)
					js += "clickElement(dynaret('" + clickControl + "'));"+ Environment.NewLine;
				if(eventFunction!=null)
					js += "dynaevent('"+eventFunction+"');"+ Environment.NewLine;
				js += "	parent.HideBox();}";
				js += "</script>";
				ClientScript.RegisterStartupScript(this.GetType(), "", js);
				Find.Text =Root.rm.GetString("Prftxt5");
				if(!Page.IsPostBack)
					CreaTree(tvCategoryTreeSearch, 0);
			}
		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.Find.Click += new EventHandler(Find_Click);
		}

		#endregion

		private void Find_Click(object sender, EventArgs e)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT FILEMANAGER.ID, FILEMANAGER.FILENAME, FILEMANAGER.REVIEWNUMBER, FILEMANAGER.DESCRIPTION FROM FILEMANAGER ");
			sb.Append("LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ");
			sb.AppendFormat("WHERE ((FILEMANAGER.FILENAME LIKE '%{0}%' OR FILEMANAGER.DESCRIPTION LIKE '%{0}%')",DatabaseConnection.FilterInjection(FindIt.Text));

			if(ext.Length>0)
			{
				sb.AppendFormat(" AND FILEMANAGER.FILENAME LIKE '%.{0}'", ext);
			}
			if (CategoryIdSearch.Text.Length > 0)
			{
				sb.AppendFormat(" AND TYPE={0}", int.Parse(CategoryIdSearch.Text));
				CategoryIdSearch.Text = String.Empty;
			}

			sb.AppendFormat(" AND (FILEMANAGER.OWNERID={0} OR {1}) AND ISREVIEW=0)", UC.UserId, GroupsSecure("FILEMANAGER.GROUPS"));

			FileRep.DataSource = DatabaseConnection.CreateDataset(sb.ToString());
			FileRep.DataBind();
		}

		public void CreaTree(TreeView tree, int open)
		{
string queryCat;
queryCat = "SELECT * FROM FILESCATEGORIES WHERE PARENTID = 0";
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			TreeNode tv1 = new TreeNode();

					tv1.Text = "<a href=\"javascript:copyData('', '','CategoryTextSearch','CategoryIdSearch','Find')\" style=\"color:black;text-decoration:none\">" +Root.rm.GetString("Dsttxt21") + "</a>";

			tree.Nodes.Add(tv1);
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();


				tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryTextSearch','CategoryIdSearch','Find')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";

				tv.NodeData = dr["Id"].ToString();

				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open); // Chiamata ricorsiva per fare le foglie
				tv.ImageUrl = "/webctrl_client/1_0/treeimages/folder.gif";
				tv.ExpandedImageUrl = "/webctrl_client/1_0/treeimages/folderopen.gif";
				tree.Nodes.Add(tv);
			}
			tree.CssClass = "normal";

		}

		public bool FillCategoryTree(int parent, TreeNode tvUp, int open)
		{
string queryCat;
queryCat = "SELECT * FROM FILESCATEGORIES WHERE PARENTID = " + parent;
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			bool toExpand = false;
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
						tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryTextSearch','CategoryIDSearch','Find')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
				tv.NodeData = dr["Id"].ToString();
				if (!toExpand)
					toExpand = (open == Convert.ToInt32(dr["id"]));
				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open); // Chiamata ricorsiva per fare le foglie
				if (tv.Expanded) toExpand = true;
				tv.ImageUrl = "/webctrl_client/1_0/treeimages/folder.gif";
				tv.ExpandedImageUrl = "/webctrl_client/1_0/treeimages/folderopen.gif";

				tvUp.Nodes.Add(tv);
			}
			return toExpand;
		}
	}
}
