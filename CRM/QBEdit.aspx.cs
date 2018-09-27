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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.CRM
{
	public partial class QBEdit : G
	{
		public StringBuilder tree = new StringBuilder();
		public string addImage = "/images/Rplus.gif";
		public string LastImage = "/images/last.gif";
		public string contractImage = "/images/Rminus.gif";
		public string timg = "/images/t.gif";
		public string limg = "/images/l.gif";



		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					if (Request.QueryString["n"] == "1") Session.Remove("QueryID");

				}

				if (Session["QueryID"] != null)
					Card.Attributes.Add("src", "/crm/qbtable.aspx?render=no&queryid=" + Session["QueryID"]);
				else
					Card.Attributes.Add("src", "/crm/qbtable.aspx?render=no");

				Session.Remove("QueryID");
				MyTreeView.Text = TreeViewMainTable();
			}
		}

		public string TreeViewMainTable()
		{
			DataSet myDataSet = DatabaseConnection.CreateDataset(string.Format("SELECT ID,RMVALUE FROM QB_ALL_TABLES WHERE SELECTABLE=1 AND PARENT=0 AND ({0} & MODULES)=MODULES",(int)UC.Modules));
			tree.Append("<table border=0 cellspacing=0 cellpadding=0>");
			foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
			{
				tree.Append("<tr><td><img src=\"/images/Back1px.gif\" height=1 width=5><img src=\"" + addImage + "\" id=\"img" + myDataRow["id"].ToString() + "\" style=\"cursor: pointer;\" onclick=\"showhide(id" + myDataRow["id"].ToString() + ",this)\"></td><td align=left class=\"normal\" onclick=\"showhide(id" + myDataRow["id"].ToString() + ",img" + myDataRow["id"].ToString() + ")\" style=\"cursor: hand;\"><b>" +Root.rm.GetString("QBTxt" + myDataRow["rmValue"].ToString()) + "</b></td></tr>");
				tree.Append("<tr><td></td><td>");
				printsubcategory(int.Parse(myDataRow["Id"].ToString()), true);
				tree.Append("</td><tr>");
			}
			tree.Append("</table>");
			return tree.ToString();
		}

		public void printsubtable(int parent)
		{
            DataSet myDataSet = DatabaseConnection.CreateDataset(string.Format("SELECT ID,RMVALUE FROM QB_ALL_TABLES WHERE SELECTABLE=1 AND PARENT={0} AND ({1} & MODULES)=MODULES", parent, (int)UC.Modules));
			if (myDataSet.Tables[0].Rows.Count > 0)
				foreach (DataRow myDataRow in myDataSet.Tables[0].Rows)
				{
					tree.Append("<tr><td><img src=\"/images/Back1px.gif\" height=1 width=5><img src=\"" + addImage + "\" name=\"img" + myDataRow["id"].ToString() + "\" style=\"cursor: pointer;\" onclick=\"showhide(id" + myDataRow["id"].ToString() + ",this)\"></td><td class=\"normal\" onclick=\"showhide(id" + myDataRow["id"].ToString() + ",img" + myDataRow["id"].ToString() + ")\" style=\"cursor: hand;\"><b>" +Root.rm.GetString("QBTxt" + myDataRow["rmValue"].ToString()) + "</b></td></tr>");
					tree.Append("<tr><td></td><td>");
					printsubcategory(int.Parse(myDataRow["Id"].ToString()), false);
					tree.Append("</td></tr>");
				}
		}

		private void printsubcategory(int id, bool main)
		{
			DataSet myDataSet = new DataSet();
			tree.Append("<table border=0 cellspacing=0 cellpadding=0 id=\"id" + id + "\" style=\"display: none;\">");
			DataTable dt = DatabaseConnection.CreateDataset("SELECT DISTINCT FIELDCAT_RMVALUE FROM QB_ALL_FIELDS WHERE TABLEID=" + id + " ORDER BY FIELDCAT_RMVALUE").Tables[0];
			foreach (DataRow ddr in dt.Rows)
			{
				myDataSet = DatabaseConnection.CreateDataset("SELECT ID,FIELDTYPE,FIELD,RMVALUE,FLAGPARAM FROM QB_ALL_FIELDS WHERE FIELDCAT_RMVALUE=" + ddr[0].ToString() + " AND TABLEID=" + id + " ORDER BY VIEWORDER");
				if (ddr[0].ToString() != "0")
				{
					tree.Append("<tr><td><img src=\"/images/Back1px.gif\" height=1 width=5><img src=\"" + addImage + "\" name=\"img" + id + "s" + ddr[0].ToString() + "\" style=\"cursor: hand;\" onclick=\"showhide(id" + id + "s" + ddr[0].ToString() + ",this)\"></td><td class=\"normal\" onclick=\"showhide(id" + id + "s" + ddr[0].ToString() + ",img" + id + "s" + ddr[0].ToString() + ")\" style=\"cursor: pointer;\"><b>" +Root.rm.GetString("QBUtxt" + ddr[0].ToString()) + "</b></td></tr>");
					tree.Append("<tr><td></td><td><table border=0 cellspacing=0 cellpadding=0 id=\"id" + id + "s" + ddr[0].ToString() + "\" style=\"display: none;\">");
				}

				for (int i = 0; i < myDataSet.Tables[0].Rows.Count; i++)
				{
					DataRow d = myDataSet.Tables[0].Rows[i];
					string indent = String.Empty;
					if (ddr[0].ToString() != "0") indent = "<img src=\"/images/Back1px.gif\" height=1 width=5>";
					tree.Append("<tr><td>" + indent + "<img src=\"/images/Back1px.gif\" height=1 width=5><img src=\"" + ((i == myDataSet.Tables[0].Rows.Count - 1) ? limg : timg) + "\"></td><td class=\"normal\" style=\"cursor:pointer;\" onclick=\"GetIndex('" + id.ToString() + "|" + d[0].ToString() + "|" + d[1].ToString() + "')\">" +Root.rm.GetString("QBTxt" + d["rmVAlue"].ToString()) + "</td></tr>");
				}
				if (ddr[0].ToString() != "0") tree.Append("</td></tr></table>");
			}
			myDataSet = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + id);
			if (myDataSet.Tables[0].Rows.Count > 0)
			{
				tree.Append("<tr><td><img src=\"/images/Back1px.gif\" height=1 width=5><img src=\"" + addImage + "\" name=\"img" + id + "f\" style=\"cursor: hand;\" onclick=\"showhide(id" + id + "f,this)\"></td><td class=\"normal\" onclick=\"showhide(id" + id + "f,img" + id + "f)\" style=\"cursor: pointer;\"><b>Campi Liberi</b></td></tr>");
				tree.Append("<tr><td></td><td colspan=2><table border=0 cellspacing=0 cellpadding=0 id=\"id" + id + "f\" style=\"display: none;\">");
				foreach (DataRow d in myDataSet.Tables[0].Rows)
				{
					tree.Append("<tr><td><img src=\"/images/Back1px.gif\" height=1 width=5></td><td class=\"normal\" style=\"cursor:pointer;\" onclick=\"GetIndex('-" + id.ToString() + "|" + d["ID"].ToString() + "|-" + d["type"].ToString() + "')\">" + d["name"].ToString() + "</td></tr>");
				}
				tree.Append("</td></tr></table>");
			}

			if (main) printsubtable(id);
			tree.Append("</table>");
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

		}

		#endregion
	}
}
