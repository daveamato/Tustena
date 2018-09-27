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
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena.CRM
{
	public partial class qbtable : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "", "<script>parent.location.href=parent.location.href;</script>");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					SaveReport.Text =Root.rm.GetString("QBUtxt8");
					SaveReport.Visible = false;
					QBQueryName.Attributes.Add("onkeyup", "parent.active=true");
					FillQBQueryCategory();

					Session.Remove("qbtable");
					if (Request.Params["queryid"] != null)
					{
						QBQueryID.Text = Request.Params["queryid"];
						if (FillDataTable(int.Parse(Request.Params["queryid"])))
							FillRepeater((DataTable) Session["qbtable"]);
						else
						{
							QBSaveInfo.Text =Root.rm.GetString("QBUtxt17");
							QBQueryID.Text = "-1";
						}
					}
					else
						QBQueryID.Text = "-1";


				}

				if(HiddenNewCategory.Text.Length>0)
				{
					HiddenNewCategory.Attributes.Remove("style");
					QBQueryCategory.Visible=false;
				}
			}
		}

		private void FillQBQueryCategory()
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM QB_CATEGORIES").Tables[0];
			if(dt.Rows.Count>0)
			{
				QBQueryCategory.DataTextField = "description";
				QBQueryCategory.DataValueField = "id";
				QBQueryCategory.DataSource = dt;
				QBQueryCategory.DataBind();
			}
			QBQueryCategory.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			QBQueryCategory.Items[0].Value = "0";

			QBQueryCategory.Items.Add(new ListItem(Root.rm.GetString("QBTxt145"),"add"));

			QBQueryCategory.Attributes.Add("ONCHANGE","selectadd(this,HiddenNewCategory)");
		}

		private void FillDatatable()
		{
			DataTable dt = new DataTable();

			if (Session["qbtable"] != null)
			{
				dt = (DataTable) Session["qbtable"];

				foreach (RepeaterItem it in Repqbtable.Items)
				{
					if (it.ItemType == ListItemType.Item || it.ItemType == ListItemType.AlternatingItem)
					{
						DataRow[] dreorder = dt.Select("trid = " + ((Literal) it.FindControl("LabelID")).Text);
						dreorder[0]["Options"] = Request.Form["opt" + ((Literal) it.FindControl("LabelID")).Text];
						dreorder[0]["fieldlabel"] = Request.Form["afl" + ((Literal) it.FindControl("LabelID")).Text];
					}
				}
				foreach (DataRow dr in dt.Rows)
				{
					dr["trid"] = dr["Options"].ToString().Split(',')[0];
				}
			}
			else
			{
				dt = CreateNewDataTable();
			}


			if (Request.Params["AddNewRow"] != null)
			{
				string addNewRowParam = DatabaseConnection.FilterInjection(Request.Params["AddNewRow"]);
				bool exists = false;
				QBSaveInfo.Visible = false;
				foreach (DataRow dr_esiste in dt.Rows)
				{
					if (dr_esiste["fieldtype"].ToString() == addNewRowParam)
					{
						exists = true;
						break;
					}
				}
				if (!exists)
				{
					string[] newfield = addNewRowParam.Split('|');
					if (newfield[0].Substring(0, 1) != "-")
					{
						Trace.Warn("fieldquery>>>", "SELECT ID,FIELDTYPE,FIELD,RMVALUE,FLAGPARAM FROM QB_ALL_FIELDS WHERE ID=" + newfield[1]);
						DataRow dtt = DatabaseConnection.CreateDataset("SELECT ID,FIELDTYPE,FIELD,RMVALUE,FLAGPARAM FROM QB_ALL_FIELDS WHERE ID=" + newfield[1]).Tables[0].Rows[0];

						DataRow[] dfilter = dt.Select("", "TRID DESC");

						int trid = 1;
						if (dfilter.Length > 0) trid = Convert.ToInt32(dfilter[0]["trid"]) + 1;

						DataRow d = dt.NewRow();
						d[0] = d[4] = trid;
						d[1] =Root.rm.GetString("QBTxt" + dtt["rmVAlue"].ToString());
						d[2] = trid.ToString() + ",1,0,0,0";
						d[3] = String.Empty;
						d[5] = addNewRowParam;

						dt.Rows.Add(d);
					}
					else
					{
						int trid = 1;
						DataRow[] dfilter = dt.Select("", "trid DESC");
						if (dfilter.Length > 0) trid = Convert.ToInt32(dfilter[0]["trid"]) + 1;
						DataRow dtt;
						dtt = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + newfield[0].Substring(1, newfield[0].Length - 1) + " AND ID=" + newfield[1]).Tables[0].Rows[0];
						DataRow d = dt.NewRow();
						d[0] = d[4] = trid;
						d[1] = dtt["Name"].ToString();
						d[2] = trid.ToString() + ",1,0,0,0";
						d[3] = String.Empty;
						d[5] =addNewRowParam;

						dt.Rows.Add(d);
					}


				}
			}

			ArrayList tt = new ArrayList();
			foreach (DataRow dr in dt.Rows)
			{
				string[] t = dr["fieldtype"].ToString().Split('|');
				if (t[2] == "7")
				{
					string[] aggfields = DatabaseConnection.SqlScalar("SELECT AGGREGATEFIELDS FROM QB_ALL_FIELDS WHERE ID=" + t[1]).Split(',');
					string aggQuery = String.Empty;
					foreach (string af in aggfields)
					{
						aggQuery += "ID=" + af + " OR ";
					}

					DataTable aggDt = DatabaseConnection.CreateDataset("SELECT ID,FIELDTYPE,FIELD,RMVALUE,FLAGPARAM,TABLEID FROM QB_ALL_FIELDS WHERE " + aggQuery.Substring(0, aggQuery.Length - 3)).Tables[0];
					bool first = true;
					foreach (DataRow d in aggDt.Rows)
					{
						foreach (DataRow drag in dt.Rows)
						{
							string[] op = drag["Options"].ToString().Split(',');
							if (drag["fieldtype"].ToString().Split('|')[1] == d[0].ToString())
								drag["Options"] = op[0] + "," + op[1] + ",1," + op[3] + "," + op[4];
							else if (first)
								drag["Options"] = op[0] + "," + op[1] + ",2," + op[3] + "," + op[4];
						}
						first = false;
					}
				}
				bool exists = false;
				string[] tableid = dr["fieldtype"].ToString().Split('|');
				foreach (string x in tt)
				{
					if (x == tableid[0])
					{
						exists = true;
						break;
					}
				}
				if (!exists) tt.Add(t[0]);
			}

			if (tt.Count > 1)
			{
				bool first = true;
				foreach (DataRow drag in dt.Rows)
				{
					string[] op = drag["Options"].ToString().Split(',');
					if (first)
						drag["Options"] = op[0] + "," + op[1] + "," + op[2] + ",1," + op[4];
					else
						drag["Options"] = op[0] + "," + op[1] + "," + op[2] + ",0," + op[4];

					first = false;
				}
			}

			FillRepeater(dt);
			StartScript(dt);
		}

		private void FillRepeater(DataTable dt)
		{
			DataView dv = dt.DefaultView;
			dv.Sort = "trid";

			Repqbtable.DataSource = dv;
			Repqbtable.DataBind();
			Session["qbtable"] = dt;
			SaveReport.Visible = (Repqbtable.Items.Count > 0);
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
			this.NewRow.Click += new EventHandler(NewRow_Click);
			this.SaveReport.Click += new EventHandler(SaveReport_Click);
			this.Repqbtable.ItemDataBound += new RepeaterItemEventHandler(Repqbtable_ItemDataBound);
			this.Repqbtable.ItemCommand += new RepeaterCommandEventHandler(Repqbtable_ItemCommand);
		}

		#endregion

		private void NewRow_Click(object sender, EventArgs e)
		{
			FillDatatable();
		}

		private void Repqbtable_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					string[] ftype = ((Literal) e.Item.FindControl("FieldType")).Text.Split('|');
					StringBuilder p = new StringBuilder();
					string paramsStr = (Request.Form["Param_" + ftype[1]] != null) ? Request.Form["Param_" + ftype[1]] : DataBinder.Eval((DataRowView) e.Item.DataItem, "parameter").ToString();
					Trace.Warn("ftype", ftype[2]);
				switch (ftype[2])
				{
					case "0":
						p.AppendFormat("<input type=\"text\" name=\"Param_{0}\" class=\"BoxDesign\" value=\"" + paramsStr + "\">", ftype[1]);
						break;
					case "1":
					case "2":
						DataSet dsDrop = DatabaseConnection.CreateDataset("SELECT * FROM QB_DROPDOWNPARAMS WHERE IDRIF=" + int.Parse(ftype[1]));
						StringBuilder SBdrop = new StringBuilder();
						StringBuilder SBdropparams = new StringBuilder();
						SBdrop.AppendFormat("SELECT {0} AS KEYFIELD,{1} AS TEXTFIELD ", dsDrop.Tables[0].Rows[0]["ValueField"].ToString(), dsDrop.Tables[0].Rows[0]["TextField"].ToString());
						SBdrop.AppendFormat("FROM {0} ", dsDrop.Tables[0].Rows[0]["RefTable"].ToString());
						if (dsDrop.Tables[0].Rows[0]["LangField"].ToString().Length > 0)
							SBdropparams.AppendFormat(" {0}='{1}' ", dsDrop.Tables[0].Rows[0]["LangField"].ToString(), UC.Culture.Substring(0, 2));

						Trace.Warn("culture", (UC.CultureSpecific.Length > 0) ? UC.CultureSpecific : UC.Culture.Substring(0, 2));

						if (dsDrop.Tables[0].Rows[0]["P1"].ToString().Length > 0)
						{
                            if (dsDrop.Tables[0].Rows[0]["P1"].ToString().Length == 1)
                            {
							SBdropparams.Append(" ");
                            }
                            else
                            {
							SBdropparams.Append(" ");
                            }
						}
						if (dsDrop.Tables[0].Rows[0]["P2"].ToString().Length > 0)
						{
							SBdropparams.AppendFormat(" AND ({0}=0 ", dsDrop.Tables[0].Rows[0]["P2"].ToString());
							SBdropparams.AppendFormat("OR ({0}=1 AND {1}={2}))", dsDrop.Tables[0].Rows[0]["P2"].ToString(), dsDrop.Tables[0].Rows[0]["P3"].ToString(), UC.UserId);
						}

						if (dsDrop.Tables[0].Rows[0]["P5"].ToString().Length > 0)
						{
							SBdropparams.AppendFormat(" AND ({0}) ", dsDrop.Tables[0].Rows[0]["P5"].ToString());
						}

						if (SBdropparams.ToString().Length > 0)
						{
							if (SBdropparams.ToString().Substring(0, 4) == " AND")
								SBdrop.Append("WHERE " + SBdropparams.ToString().Substring(4, SBdropparams.ToString().Length - 4));
							else
								SBdrop.Append("WHERE " + SBdropparams.ToString());
						}

						dsDrop.Clear();
						dsDrop = DatabaseConnection.CreateDataset(SBdrop.ToString());

						p.AppendFormat("<select name=\"Param_{0}\" class=\"BoxDesign\">", ftype[1]);
						foreach (DataRow d in dsDrop.Tables[0].Rows)
						{
							p.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", d["keyfield"].ToString(), d["textfield"].ToString(), (paramsStr == d["keyfield"].ToString()) ? "selected" : "");
						}
						p.Append("</select>");
						break;
					case "3":
						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" readOnly name=\"Param_{0}\" class=\"BoxDesign\" value=\"" + paramsStr + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopUpDate.aspx?textbox=Param_{0}',event,195,195)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "4":
						p.AppendFormat("<input type=\"checkbox\" name=\"Param_{0}\" value=\"true\" {1}>", ftype[1], (paramsStr == "true") ? "checked" : "");
						break;
					case "5":
						string paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}
						else
						{
							paramt = DatabaseConnection.SqlScalar("SELECT (NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE UID=" + paramsStr);
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"" + paramsStr + "\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"" + paramt + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopAccount.aspx?render=no&textbox=Paramt_{0}&textbox2=Param_{0}',event)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "8":
						string range1 = String.Empty;
						string range2 = String.Empty;
						if (Request.Form["Param_" + ftype[1]] != null)
						{
							range1 = Request.Form["Param_" + ftype[1]];
							range2 = Request.Form["Param2_" + ftype[1]];
						}
						else
						{
							if (paramsStr.IndexOf("|") > 0)
							{
								range1 = paramsStr.Split('|')[0];
								range2 = paramsStr.Split('|')[1];
							}
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" readOnly name=\"Param_{0}\" class=\"BoxDesign\" value=\"" + range1 + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopUpDate.aspx?textbox=Param_{0}',event,195,195)\">", ftype[1]);
						p.Append("</td></tr>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param2_{0}\" readOnly name=\"Param2_{0}\" class=\"BoxDesign\" value=\"" + range2 + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopUpDate.aspx?textbox=Param2_{0}',event,195,195)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "9": // pop aziende
						paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}
						else
						{
							paramt = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + paramsStr);
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"" + paramsStr + "\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"" + paramt + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopCompany.aspx?render=no&textbox=Paramt_{0}&textbox2=Param_{0}',event,500,400)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "10": // pop contatti
						paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}
						else
						{
							paramt = DatabaseConnection.SqlScalar("SELECT (ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')) AS CONTACT FROM BASE_CONTACTS WHERE ID=" + paramsStr);
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"" + paramsStr + "\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"" + paramt + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/popcontacts.aspx?render=no&textbox=Paramt_{0}&textboxID=Param_{0}',event,400,300)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "11": // pop opportunit
						paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}
						else
						{
							paramt = DatabaseConnection.SqlScalar("SELECT TITLE FROM CRM_OPPORTUNITY WHERE ID=" + paramsStr);
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"" + paramsStr + "\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"" + paramt + "\">", ftype[1]);
						p.Append("</td><td width=\"30\">");
						p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"FrameCreateBox('/Common/PopOpportunity.aspx?render=no&textbox=Paramt_{0}&textboxID=Param_{0}',event,400,300)\">", ftype[1]);
						p.Append("</td></tr></table>");
						break;
					case "12":
						paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}

						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr><td>");
						p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" class=\"BoxDesign\" value=\"" + Request.Form["Param_" + ftype[1]] + "\">", ftype[1]);
						p.Append("</td><td width=\"50%\" class=\"normal\">");
						p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"0\" {1}><", ftype[1], (paramt == "0") ? "checked" : "");
						p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"1\" {1}>>", ftype[1], (paramt == "1") ? "checked" : "");
						p.Append("</td></tr></table>");
						break;
					case "13":
						DataTable dtDrop = DatabaseConnection.CreateDataset("SELECT * FROM QB_FIXEDDROPDOWNPARAMS WHERE IDRIF=" + ftype[1]).Tables[0];


						p.AppendFormat("<select name=\"Param_{0}\" class=\"BoxDesign\">", ftype[1]);
						foreach (DataRow d in dtDrop.Rows)
						{
							p.AppendFormat("<option value=\"{0}\">{1}</option>", d["dropvalue"].ToString(),Root.rm.GetString("QBTxt" + d["rmvalue"].ToString()));
						}

						p.Append("</select>");
						break;
					case "14": // radio attivit fatte/da fare/annullate
						paramt = String.Empty;
						if (Request.Form["Paramt_" + ftype[1]] != null)
						{
							paramt = Request.Form["Paramt_" + ftype[1]];
						}
						if((paramt != null && paramt.Length == 0))paramt="1";
						p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
						p.Append("<tr>");
						p.Append("<td width=\"50%\" class=\"normal\">");
						p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"1\" {1}>{2}", ftype[1], (paramt == "1") ? "checked" : "",Root.rm.GetString("Acttxt71"));
						p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"0\" {1}>{2}", ftype[1], (paramt == "0") ? "checked" : "",Root.rm.GetString("Acttxt72"));
						p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"2\" {1}>{2}", ftype[1], (paramt == "2") ? "checked" : "",Root.rm.GetString("Acttxt103"));
						p.Append("</td></tr></table>");
						break;
					case "-4": // dropdown da free fields
						string[] dsDropFree = DatabaseConnection.SqlScalar("SELECT ITEMS FROM ADDEDFIELDS WHERE ID=" + DatabaseConnection.FilterInjection(ftype[1])).Split('|');
						p.AppendFormat("<select name=\"Param_-{0}\" class=\"BoxDesign\">", ftype[1]);
						foreach (string d in dsDropFree)
						{
							if (d.Length > 0) p.AppendFormat("<option value=\"{0}\" {1}>{0}</option>", d, (paramsStr == d) ? "selected" : "");
						}
						p.Append("</select>");
						break;
					case "-1": // Text da free fields
					case "-2":
						p.AppendFormat("<input type=\"text\" name=\"Param_{0}\" class=\"BoxDesign\" value=\"" + paramsStr + "\">", ftype[1]);
						break;
				}

					((Label) e.Item.FindControl("LblParameter")).Text = p.ToString();
					break;
			}
		}

		private void SaveReport_Click(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			if (Session["qbtable"] != null)
			{
				dt = (DataTable) Session["qbtable"];

				foreach (RepeaterItem it in Repqbtable.Items)
				{
					if (it.ItemType == ListItemType.Item || it.ItemType == ListItemType.AlternatingItem)
					{
						DataRow[] dreorder = dt.Select("trid = " + ((Literal) it.FindControl("LabelID")).Text);
						dreorder[0]["Options"] = Request.Form["opt" + ((Literal) it.FindControl("LabelID")).Text];
						dreorder[0]["fieldlabel"] = Request.Form["afl" + ((Literal) it.FindControl("LabelID")).Text];
					}
				}
				foreach (DataRow dr in dt.Rows)
				{
					dr["trid"] = dr["Options"].ToString().Split(',')[0];
				}
			}

			SaveCustomerQuery(dt);

		}


		private void SaveCustomerQuery(DataTable dt)
		{
			string oldGroup = String.Empty;
			if (QBQueryID.Text != "-1")
			{
				int queryId = int.Parse(QBQueryID.Text);
				oldGroup = DatabaseConnection.SqlScalar("SELECT GROUPS FROM QB_CUSTOMERQUERY WHERE ID=" + queryId);
				DatabaseConnection.DoCommand("DELETE FROM QB_CUSTOMERQUERY WHERE ID=" + queryId);
			}

			object newId;
			long NewCategory=0;

			if(HiddenNewCategory.Text.Length>0)
			{
				using (DigiDapter dgcat = new DigiDapter())
				{
					dgcat.Add("Description", HiddenNewCategory.Text);
					object NewCatID = dgcat.Execute("QB_CATEGORIES", "ID=-1", DigiDapter.Identities.Identity);
					NewCategory = Convert.ToInt64(NewCatID);
					this.FillQBQueryCategory();
				}
			}
			else
			{
				try
				{
					NewCategory = Convert.ToInt64(QBQueryCategory.Value);
				}
				catch
				{
					NewCategory = 0;
				}
			}


			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("TITLE", QBQueryName.Text);
				dg.Add("DESCRIPTION", QBQueryDescription.Text);

				dg.Add("CATEGORY",NewCategory);
				dg.Add("CREATEDBYID", UC.UserId);

				if (oldGroup.Length > 0)
					dg.Add("GROUPS", oldGroup);
				else
					dg.Add("GROUPS", "|" + UC.UserGroupId + "|");

				string grby = string.Empty;
				foreach (DataRow dr in dt.Rows)
				{
					if (dr["Options"].ToString().Split(',')[2] == "1")
					{
						grby +=dr["fieldtype"].ToString();
					}
				}

				dg.Add("GROUPBY", grby);

				newId = dg.Execute("QB_CUSTOMERQUERY", "ID=-1", DigiDapter.Identities.Identity);
			}

			ArrayList tt = new ArrayList();

			foreach (DataRow dr in dt.Rows)
			{
				if (dr["Options"].ToString().Split(',')[3] == "1")
				{
					tt.Add(dr["fieldtype"].ToString().Split('|')[0]);
					break;
				}
			}

			foreach (DataRow dr in dt.Rows)
			{
				bool exists = false;
				string[] t = dr["fieldtype"].ToString().Split('|');
				foreach (string x in tt)
				{
					if (x == t[0])
					{
						exists = true;
						break;
					}
				}
				if (!exists) tt.Add(t[0]);
			}


			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("INSERT INTO QB_CUSTOMERQUERYTABLES (IDQUERY,IDTABLE,MAINTABLE) VALUES ({0},{1},{2});",Convert.ToInt64(newId.ToString()),Convert.ToInt32(tt[0]),1);
			for(int i=1;i<tt.Count;i++)
			{
					sb.AppendFormat("INSERT INTO QB_CUSTOMERQUERYTABLES (IDQUERY,IDTABLE) VALUES ({0},{1});",Convert.ToInt64(newId.ToString()),Convert.ToInt32(tt[i]));
			}
			DatabaseConnection.DoCommand(sb.ToString());


			DataRow[] drsort = dt.Select("", "trid");


			for (int i = 0; i < drsort.Length; i++)
			{
				sb = new StringBuilder();
				string[] t = drsort[i]["fieldtype"].ToString().Split('|');
				sb.Append("INSERT INTO QB_CUSTOMERQUERYFIELDS (IDQUERY,IDTABLE,IDFIELD,COLUMNNAME,FIELDVISIBLE,OPTIONS) VALUES ");
				sb.AppendFormat("({0},", Convert.ToInt64(newId.ToString()));
				sb.AppendFormat("{0},", Convert.ToInt32(t[0]));
				sb.AppendFormat("{0},", Convert.ToInt32(t[1]));
				sb.AppendFormat("'{0}',", drsort[i]["fieldlabel"].ToString());
				sb.AppendFormat("{0},", (drsort[i]["Options"].ToString().Split(',')[1] == "1") ? 1 : 0);
				sb.AppendFormat("'{0}')", drsort[i]["Options"].ToString());
				Trace.Warn("sb",sb.ToString());
				DatabaseConnection.DoCommand(sb.ToString());
			}


			for (int i = 0; i < drsort.Length; i++)
			{
				string[] t = drsort[i]["fieldtype"].ToString().Split('|');
				if (drsort[i]["Options"].ToString().Split(',')[4] == "1")
				{
					sb = new StringBuilder();
					sb.Append("INSERT INTO QB_CUSTOMERQUERYPARAMFIELDS (IDQUERY,IDTABLE,IDFIELD,FIXEDVALUE) VALUES ");

					sb.AppendFormat("({0},", Convert.ToInt64(newId.ToString()));
					sb.AppendFormat("{0},", Convert.ToInt32(t[0]));
					sb.AppendFormat("{0},", Convert.ToInt32(t[1]));
					if (Request.Form["Param_" + t[1]] != null && Request.Form["Param_" + t[1]].Length > 0)
					{
						switch (t[2])
						{
							case "8":
								sb.AppendFormat("'{0}')", Request.Form["Param_" + t[1]] + "|" + Request.Form["Param2_" + t[1]]);
								break;
							case "12":
								sb.AppendFormat("'{0}')", Request.Form["Param_" + t[1]] + "|" + Request.Form["Paramt_" + t[1]]);
								break;
							default:
								sb.AppendFormat("'{0}')", Request.Form["Param_" + t[1]]);
								break;
						}
					}else
						sb.Append("'')");
					Trace.Warn("sb",sb.ToString());
					DatabaseConnection.DoCommand(sb.ToString());
				}
			}



			QBSearchStep1.Visible = false;
			Repqbtable.Visible = false;

			QBSaveInfo.Text = String.Format(Root.rm.GetString("QBUtxt18"), QBQueryName.Text);
			QBSaveInfo.Visible = true;
			SaveReport.Visible = false;

			Session["ReportToOpen"]=newId.ToString();
			ClientScript.RegisterStartupScript(this.GetType(), "OpenScript","<script>parent.location='/CRM/QBDefault.aspx?m=55&dgb=1&si=42'</script>");

		}

		private DataTable CreateNewDataTable()
		{
			DataTable dt = new DataTable();
			DataColumn dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "trid";
			dcDynColumn.DataType = Type.GetType("System.Byte");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "Label";
			dcDynColumn.DataType = Type.GetType("System.String");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "Options";
			dcDynColumn.DataType = Type.GetType("System.String");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "parameter";
			dcDynColumn.DataType = Type.GetType("System.String");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "order";
			dcDynColumn.DataType = Type.GetType("System.Byte");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "fieldtype";
			dcDynColumn.DataType = Type.GetType("System.String");
			dcDynColumn.DefaultValue = 0;
			dt.Columns.Add(dcDynColumn);
			dcDynColumn = new DataColumn();
			dcDynColumn.ColumnName = "fieldlabel";
			dcDynColumn.DataType = Type.GetType("System.String");
			dcDynColumn.DefaultValue = String.Empty;
			dt.Columns.Add(dcDynColumn);
			return dt;
		}

		private bool FillDataTable(int id)
		{
			DataTable dt = CreateNewDataTable();
			DataSet mainDs = DatabaseConnection.CreateDataset("SELECT * FROM QB_CUSTOMERQUERY WHERE ID=" + id);
			if (mainDs.Tables[0].Rows.Count > 0)
			{
				DataRow mainDr = mainDs.Tables[0].Rows[0];
				QBQueryName.Text = mainDr["Title"].ToString();
				QBQueryDescription.Text = mainDr["Description"].ToString();

				QBQueryCategory.SelectedIndex=-1;
				foreach(ListItem li in QBQueryCategory.Items)
				{
					if(li.Value==mainDr["Category"].ToString())
					{
						li.Selected=true;
						break;
					}
				}

				string queryField = "SELECT QB_CUSTOMERQUERYFIELDS.*, QB_ALL_FIELDS.RMVALUE AS RMVALUE, " +
					"QB_ALL_FIELDS.FIELD AS FIELD,QB_ALL_FIELDS.FIELDTYPE AS FIELDTYPE " +
					"FROM QB_CUSTOMERQUERYFIELDS " +
					"INNER JOIN QB_ALL_FIELDS ON QB_CUSTOMERQUERYFIELDS.IDFIELD = QB_ALL_FIELDS.ID " +
					"WHERE IDQUERY=" + id;
				DataTable fieldDt = DatabaseConnection.CreateDataset(queryField).Tables[0];

				foreach (DataRow d in fieldDt.Rows)
				{
					if ((int) d["IDTable"] > 0)
					{
						DataRow dr = dt.NewRow();
						string param = String.Empty;
						dr["trid"] = d["Options"].ToString().Split(',')[0];
						dr["Label"] =Root.rm.GetString("QBTxt" + d["rmValue"].ToString());
						dr["Options"] = d["Options"].ToString();
						param = DatabaseConnection.SqlScalar("SELECT FIXEDVALUE FROM QB_CUSTOMERQUERYPARAMFIELDS WHERE IDQUERY=" + id + " AND IDFIELD=" + d["idfield"].ToString());
						dr["parameter"] = param;
						dr["order"] = d["Options"].ToString().Split(',')[0];
						dr["fieldtype"] = d["IDTable"].ToString() + "|" + d["IDField"].ToString() + "|" + d["fieldType"].ToString();
						dr["fieldlabel"] = d["ColumnName"].ToString();
						dt.Rows.Add(dr);
					}
					else
					{
						DataRow freef = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELDS WHERE ID=" + d["IDField"]).Tables[0].Rows[0];
						DataRow dr = dt.NewRow();
						string param = String.Empty;
						dr["trid"] = d["Options"].ToString().Split(',')[0];
						dr["Label"] = freef["name"].ToString();
						dr["Options"] = d["Options"].ToString();
						param = DatabaseConnection.SqlScalar("SELECT FIXEDVALUE FROM QB_CUSTOMERQUERYPARAMFIELDS WHERE IDQUERY=" + id + " AND IDFIELD=" + d["idfield"].ToString());
						dr["parameter"] = param;
						dr["order"] = d["Options"].ToString().Split(',')[0];
						dr["fieldtype"] = d["IDTable"].ToString() + "|" + d["IDField"].ToString() + "|-" + freef["Type"].ToString();
						dr["fieldlabel"] = freef["name"].ToString();
						dt.Rows.Add(dr);
					}
				}

				Session["qbtable"] = dt;
				StartScript(dt);
				return true;
			}
			else
				return false;
		}

		private void Repqbtable_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "DelCommand":
					Literal LabelID = (Literal) e.Item.FindControl("LabelID");
					DataTable dt = (DataTable) Session["qbtable"];
					DataRow[] drToDel = dt.Select("trid = " + LabelID.Text);
					drToDel[0].Delete();
					drToDel = dt.Select("trid > " + LabelID.Text);

					foreach (DataRow ddr in drToDel)
					{
						ddr["trid"] = Convert.ToInt32(ddr["trid"]) - 1;
						string[] op = ddr["Options"].ToString().Split(',');
						ddr["Options"] = ddr["trid"].ToString() + "," + op[1] + "," + op[2] + "," + op[3] + "," + op[4];
					}
					FillRepeater(dt);
					StartScript(dt);
					break;
			}
		}

		private void StartScript(DataTable dt)
		{
			string lockArry = String.Empty;
			DataRow[] drLock = dt.Select("", "FIELDTYPE ASC");
			foreach (DataRow dlock in drLock)
			{
				string l = DatabaseConnection.SqlScalar("SELECT LOCKTABLES FROM QB_LOCKARRAY WHERE IDTABLE=" + dlock["fieldtype"].ToString().Split('|')[0]);
				if (lockArry.IndexOf(l) == -1)
				{
					lockArry += l;
				}
			}

			lockArry = lockArry.Replace(",,", ",");
			lockArry = lockArry.Trim(',');

			ClientScript.RegisterStartupScript(this.GetType(), "start", "<script>parent.LockArry = new Array(" + lockArry + ");parent.contract();</script>");
			Trace.Warn("startscript", "<script>parent.LockArry = new Array(" + lockArry + ");parent.contract();</script>");
		}
	}
}
