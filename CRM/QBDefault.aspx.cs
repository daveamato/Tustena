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
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.DataSetTransformation;
using Digita.Tustena.Report;

namespace Digita.Tustena
{
	public partial class QBDefault : G
	{



		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				QBRepeaterPaging.Visible=false;
				if (!Page.IsPostBack)
				{
					BtnSearch.Text=Root.rm.GetString("Find");
					BtnDelCategory.Text=Root.rm.GetString("Cattxt10");
					BtnDelCategory.Attributes.Add("onclick","return confirm('" + Root.rm.GetString("QBUtxt28")+"');");

					FillListCategory();

					if(Session["ReportToOpen"] != null)
					{
						Session["QueryID"] = Session["ReportToOpen"];
						FillParams(int.Parse(Session["QueryID"].ToString()));
						Session.Remove("ReportToOpen");
					}
					else
					{
						FillCustomerQuery();
						QBSubmitStep3F.Attributes.Add("onclick", "return ParamsValidator()");
					}
				}
			}
		}

		private void FillListCategory()
		{
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION FROM QB_CATEGORIES").Tables[0];
			if(dt.Rows.Count>0)
			{
				ListCategory.DataTextField = "description";
				ListCategory.DataValueField = "id";
				ListCategory.DataSource=dt;
				ListCategory.DataBind();
			}
			ListCategory.Items.Insert(0,Root.rm.GetString("CRMcontxt53"));
			ListCategory.Items[0].Value = "0";
		}

		private void FillCustomerQuery()
		{
			string sql;
			sql = "SELECT ID,DESCRIPTION,QUERYTYPE,TITLE FROM QB_CUSTOMERQUERY WHERE (" + GroupsSecure("QB_CUSTOMERQUERY.GROUPS") + ") AND QUERYTYPE=0 ORDER BY TITLE";
			FillCustomerQuery(sql);
		}
		private void FillCustomerQuery(string sql)
		{
			QBParam.Visible = false;
			QBResult.Visible = false;
			QBRepeaterPaging.PageSize = UC.PagingSize;
			QBRepeaterPaging.RepeaterObj = QBRepeater;
			QBRepeaterPaging.sqlRepeater = sql;
			QBRepeaterPaging.BuildGrid();
			if(QBRepeater.Items.Count>0)
			{
				QBRepeater.Visible=true;
				QBRepeaterInfo.Visible=false;
			}
			else
			{
				QBRepeater.Visible=false;
				QBRepeaterInfo.Text=Root.rm.GetString("QBUtxt20");
			}
		}


		public void btn_Click(Object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "QBSubmitStep3F":
					FillResult();
					QBResult.Visible = true;
					QBParam.Visible = false;
					break;
			}
		}

		public void QBRepeaterItemDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					LinkButton del = (LinkButton) e.Item.FindControl("Delete");
					del.Attributes.Add("onclick", "return confirm('" +Root.rm.GetString("QBUtxt3") + "');");
                    del.Text = Root.rm.GetString("QBUtxt2");
                    LinkButton Modify = (LinkButton)e.Item.FindControl("Modify");
                    Modify.Text = Root.rm.GetString("QBUtxt19");
					break;
			}
		}

		public void QBRepeaterCommand(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "QBDescription":
					Session["QueryID"] = ((Literal) e.Item.FindControl("QueryID")).Text;
					FillParams(int.Parse(((Literal) e.Item.FindControl("QueryID")).Text));
					break;
				case "Delete":
					Delete(int.Parse(((Literal) e.Item.FindControl("QueryID")).Text));
					FillCustomerQuery();
					break;
				case "Modify":
					Session["QueryID"] = ((Literal) e.Item.FindControl("QueryID")).Text;
					Response.Redirect("QBEdit.aspx?m=25&si=42");
					break;
			}
		}

		private void Delete(int id)
		{
			string sqlString = "DELETE FROM QB_CUSTOMERQUERY WHERE ID ="+id;
			DatabaseConnection.DoCommand(sqlString);
		}


		private void FillParams(int id)
		{
			StringBuilder p = new StringBuilder();
			p.Append("<table border=\"0\" cellspacing=\"0\" width=\"100%\" align=\"left\" class=\"normal\">");

			DataSet qb = DatabaseConnection.CreateDataset("SELECT * FROM QB_CUSTOMERQUERYPARAMFIELDS WHERE IDQUERY=" + id);

			foreach (DataRow s in qb.Tables[0].Rows)
			{

				if ((int) s["IDTable"] > 0)
				{
					string fixedValue = (s["FixedValue"] != null) ? s["FixedValue"].ToString() : "";
					DataSet qbf = DatabaseConnection.CreateDataset("SELECT * FROM QB_ALL_FIELDS WHERE ID=" + s["IDField"]);
					DataRow drf = qbf.Tables[0].Rows[0];
					p.Append("<tr><td nowrap valign=top>" +Root.rm.GetString("QBTxt" + drf["rmValue"]) + "</td>");
					QBSubmitStep3F.Attributes.Add("onclick", "makeor()");
					switch (drf["FieldType"].ToString())
					{
						case "0": // TextFont normale
						case "6":
							p.AppendFormat("<td width=\"80%\"><span id=\"Param_{0}qc\"><input type=\"text\" name=\"Param_{0}\" id=\"Param_{0}\" value=\"{1}\" class=\"BoxDesign\"></span><img src=/images/plus.gif onclick=\"newor('Param_{0}')\" style=\"cursor:pointer\"><img src=/images/minus.gif onclick=\"delor('Param_{0}')\" style=\"cursor:pointer\"></td></tr>", s["IDField"], fixedValue);
							break;
						case "1": // dropdown
						case "2":
							DataSet dsDrop = DatabaseConnection.CreateDataset("SELECT * FROM QB_DROPDOWNPARAMS WHERE IDRIF=" + s["IDField"]);
							StringBuilder SBdrop = new StringBuilder();
							StringBuilder SBdropparams = new StringBuilder();
							if(dsDrop.Tables[0].Rows.Count>0)
							{
								SBdrop.AppendFormat("SELECT {0} AS KEYFIELD,{1} AS TEXTFIELD ", dsDrop.Tables[0].Rows[0]["ValueField"].ToString(), dsDrop.Tables[0].Rows[0]["TextField"].ToString());
								SBdrop.AppendFormat("FROM {0} ", dsDrop.Tables[0].Rows[0]["RefTable"].ToString());
								if (dsDrop.Tables[0].Rows[0]["LangField"].ToString().Length > 0)
									SBdropparams.AppendFormat(" {0}='{1}' ", dsDrop.Tables[0].Rows[0]["LangField"].ToString(), UC.Culture.Substring(0, 2));


								if (dsDrop.Tables[0].Rows[0]["P1"].ToString().Length > 0)
								{
									SBdropparams.Append(" ");
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

								p.AppendFormat("<td width=\"80%\"><span id=\"SParam_{0}qc\"><input type=\"hidden\" name=\"Param_{0}\" id=\"Param_{0}\">", s["IDField"]);


								p.AppendFormat("<select name=\"SParam_{0}\" id=\"SParam_{0}\" class=\"BoxDesign\">", s["IDField"]);
								p.AppendFormat("<option value=\"-1\">{0}</option>",Root.rm.GetString("All"));
								foreach (DataRow d in dsDrop.Tables[0].Rows)
								{
									p.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", d["keyfield"].ToString(), d["textfield"].ToString(), (fixedValue == d["keyfield"].ToString()) ? "selected" : "");
								}

								p.AppendFormat("</select></span><img src=/images/plus.gif onclick=\"newor('SParam_{0}')\" style=\"cursor:pointer\"><img src=/images/minus.gif onclick=\"delor('SParam_{0}')\" style=\"cursor:pointer\"></td></tr>", s["IDField"]);

							}
							break;

						case "3": // data
							p.Append("<td width=\"80%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" readOnly name=\"Param_{0}\" value=\"{1}\" class=\"BoxDesign\">", s["IDField"], (fixedValue.Length > 0) ? fixedValue : DateTime.Now.ToShortDateString());
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopUpDate.aspx?textbox=Param_{0}',event,195,195)\">", s["IDField"]);
							p.Append("</td></tr></table></td></tr>");
							break;

						case "4": // checkbox
							p.Append("<td width=\"90%\">");
							p.AppendFormat("<input type=\"checkbox\" name=\"Param_{0}\" ID=\"Param_{0}\" value=\"true\" {1}></td></tr>", s["IDField"], (fixedValue == "true") ? "checked" : "");
							break;
						case "5": // account
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							string fixedText = String.Empty;
							if (fixedValue.Length > 0)
							{
								fixedText = DatabaseConnection.SqlScalar("SELECT (NAME+' '+SURNAME) AS USERNAME FROM ACCOUNT WHERE UID=" + fixedValue);
							}

							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"{1}\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"{2}\">", s["IDField"], fixedValue, fixedText);
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopAccount.aspx?render=no&textbox=Paramt_{0}&textbox2=Param_{0}',event)\">", s["IDField"]);

							p.Append("</td></tr></table></td></tr>");
							break;
						case "8": // range di date
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" readOnly name=\"Param_{0}\" class=\"BoxDesign\" value=\"{1}\">", s["IDField"], (fixedValue.Length > 0) ? fixedValue.Split('|')[0] : DateTime.Now.ToShortDateString());
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopUpDate.aspx?textbox=Param_{0}',event,195,195)\">", s["IDField"]);
							p.Append("</td></tr>");
							p.Append("<tr><td>");
							p.AppendFormat("<input type=\"text\" id=\"Param2_{0}\" readOnly name=\"Param2_{0}\" class=\"BoxDesign\" value=\"{1}\">", s["IDField"], (fixedValue.Length > 0) ? fixedValue.Split('|')[1] : DateTime.Now.ToShortDateString());
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/SmallCalendar.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopUpDate.aspx?textbox=Param2_{0}',event,195,195)\">", s["IDField"]);
							p.Append("</td></tr></table></td></tr>");
							break;
						case "9": // pop aziende
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							fixedText = String.Empty;
							if (fixedValue.Length > 0)
							{
								fixedText = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + fixedValue);
							}

							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"{1}\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"{2}\">", s["IDField"], fixedValue, fixedText);
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopCompany.aspx?render=no&textbox=Paramt_{0}&textbox2=Param_{0}',event,500,400)\">", s["IDField"]);

							p.Append("</td></tr></table></td></tr>");
							break;
						case "10": // pop contatti
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							fixedText = String.Empty;
							if (fixedValue.Length > 0)
							{
								fixedText = DatabaseConnection.SqlScalar("SELECT (ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')) AS CONTACT FROM BASE_CONTACTS WHERE ID=" + fixedValue);
							}

							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"{1}\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"{2}\">", s["IDField"], fixedValue, fixedText);
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/popcontacts.aspx?render=no&textbox=Paramt_{0}&textboxID=Param_{0}',event,400,300)\">", s["IDField"]);

							p.Append("</td></tr></table></td></tr>");
							break;
						case "11": // pop opportunit
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");
							fixedText = String.Empty;
							if (fixedValue.Length > 0)
							{
								fixedText = DatabaseConnection.SqlScalar("SELECT TITLE FROM CRM_OPPORTUNITY WHERE ID=" + fixedValue);
							}

							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" style=\"display:none\" value=\"{1}\"><input type=\"text\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" class=\"BoxDesign\" value=\"{2}\">", s["IDField"], fixedValue, fixedText);
							p.Append("</td><td width=\"30\">");
							p.AppendFormat("&nbsp;<img src=\"/i/user.gif\" border=\"0\" style=\"cursor:pointer\" onclick=\"CreateBox('/Common/PopOpportunity.aspx?render=no&textbox=Paramt_{0}&textboxID=Param_{0}',event,400,300)\">", s["IDField"]);

							p.Append("</td></tr></table></td></tr>");
							break;
						case "12":
							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr><td>");

							p.AppendFormat("<input type=\"text\" id=\"Param_{0}\" name=\"Param_{0}\" class=\"BoxDesign\" value=\"{1}\">", s["IDField"], fixedValue.Split('|')[0]);
							p.Append("</td><td width=\"50%\" class=\"normal\">");
							if(fixedValue.Split('|').Length<2)
							{
								p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"0\" checked><", s["IDField"]);
								p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"1\" >>", s["IDField"]);
							}
							else
							{
								p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"0\" {1}><", s["IDField"], (fixedValue.Split('|')[1] == "0") ? "checked" : "");
								p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"1\" {1}>>", s["IDField"], (fixedValue.Split('|')[1] == "1") ? "checked" : "");
							}

							p.Append("</td></tr></table>");
							break;
						case "13": // dropdown valori fissi da rm
							DataTable dtDrop = DatabaseConnection.CreateDataset("SELECT * FROM QB_FIXEDDROPDOWNPARAMS WHERE IDRIF=" + s["IDField"]).Tables[0];
							p.AppendFormat("<td width=\"90%\"><span id=\"SParam_{0}qc\"><input type=\"hidden\" name=\"Param_{0}\" ID=\"Param_{0}\">", s["IDField"]);

							p.AppendFormat("<select name=\"SParam_{0}\" ID=\"SParam_{0}\" class=\"BoxDesign\">", s["IDField"]);
							foreach (DataRow d in dtDrop.Rows)
							{
								p.AppendFormat("<option value=\"{0}\" {2}>{1}</option>", d["dropvalue"].ToString(),Root.rm.GetString("QBTxt" + d["rmvalue"].ToString()), (fixedValue == d["dropvalue"].ToString()) ? "selected" : "");
							}

							p.AppendFormat("</select></span><img src=/images/plus.gif onclick=\"newor('SParam_{0}')\" style=\"cursor:pointer\"><img src=/images/minus.gif onclick=\"delor('Param_{0}')\" style=\"cursor:pointer\"></td></tr>", s["IDField"]);

							break;
						case "14": // radio attivit fatte/da fare/annullate
							string paramt = "1";

							p.Append("<td width=\"90%\">");
							p.Append("<table width=\"100%\" cellspacing=0 cellpadding=0>");
							p.Append("<tr>");
							p.Append("<td width=\"50%\" class=\"normal\">");
							p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"1\" {1}>{2}", s["IDField"], (paramt == "1") ? "checked" : "",Root.rm.GetString("Acttxt71"));
							p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"0\" {1}>{2}", s["IDField"], (paramt == "0") ? "checked" : "",Root.rm.GetString("Acttxt72"));
							p.AppendFormat("<input type=\"radio\" id=\"Paramt_{0}\" name=\"Paramt_{0}\" value=\"2\" {1}>{2}", s["IDField"], (paramt == "2") ? "checked" : "",Root.rm.GetString("Acttxt103"));
							p.Append("</td></tr></table>");
							break;
					}
				}
				else
				{
					string fixedValue = (s["FixedValue"] != null) ? s["FixedValue"].ToString() : "";
					DataSet qbf = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELDS WHERE ID=" + s["IDField"]);
					DataRow drf = qbf.Tables[0].Rows[0];
					p.Append("<tr><td nowrap valign=top>" + drf["name"] + "</td>");
					QBSubmitStep3F.Attributes.Add("onclick", "makeor()");
					switch (drf["Type"].ToString())
					{
						case "4": // dropdown da free fields
							string[] dsDropFree = drf["items"].ToString().Split('|');
							p.AppendFormat("<td width=\"90%\"><span id=\"SParam_-{0}qc\"><input type=\"hidden\" name=\"Param_-{0}\">", s["IDField"]);
							p.AppendFormat("<select name=\"SParam_-{0}\" id=\"SParam_-{0}\" class=\"BoxDesign\">", s["IDField"]);
							foreach (string d in dsDropFree)
							{
								if (d.Length > 0) p.AppendFormat("<option value=\"{0}\" {1}>{0}</option>", d, (fixedValue == d) ? "selected" : "");
							}
							p.AppendFormat("</select></span><img src=/images/plus.gif onclick=\"newor('SParam_-{0}')\" style=\"cursor:pointer\"><img src=/images/minus.gif onclick=\"delor('SParam_-{0}')\" style=\"cursor:pointer\"></td></tr>", s["IDField"]);
							break;
						case "1": // Text da free fields
						case "2":
							p.AppendFormat("<td width=\"90%\"><span id=\"Param_-{0}qc\"><input type=\"text\" name=\"Param_-{0}\" id=\"Param_-{0}\" value=\"{1}\" class=\"BoxDesign\"></span><img src=/images/plus.gif onclick=\"newor('Param_-{0}')\" style=\"cursor:pointer\"><img src=/images/minus.gif onclick=\"delor('Param_-{0}')\" style=\"cursor:pointer\"></td></tr>", s["IDField"], fixedValue);
							break;

					}
				}
			}

			p.Append("</table>");

			LabelParameters.Text = p.ToString();
			QBParam.Visible = true;
			QBRepeater.Visible = false;

		}

		private void FillResult()
		{
			bool ViewOn = (RadioResult.SelectedValue == "HTML");
			string delimiter = String.Empty;
			if(!ViewOn)
				HttpContext.Current.Items.Add("render","no");

			QueryBuilderManager qb = new QueryBuilderManager();
			Hashtable Params = new Hashtable();
			string sqlHash = "SELECT QB_CUSTOMERQUERYPARAMFIELDS.IDFIELD,QB_ALL_FIELDS.FIELD,QB_ALL_FIELDS.FIELDTYPE,QB_CUSTOMERQUERYPARAMFIELDS.IDTABLE " +
				"FROM QB_CUSTOMERQUERYPARAMFIELDS " +
				"INNER JOIN QB_ALL_FIELDS ON QB_CUSTOMERQUERYPARAMFIELDS.IDFIELD = QB_ALL_FIELDS.ID " +
				"WHERE QB_CUSTOMERQUERYPARAMFIELDS.IDQUERY=" + int.Parse(Session["QueryID"].ToString());
			DataSet dsQB = DatabaseConnection.CreateDataset(sqlHash);

			foreach (DataRow s in dsQB.Tables[0].Rows)
			{
				if ((int) s[3] > 0)
				{
					switch (s[2].ToString())
					{
						case "8":
							Params.Add(s[1].ToString().ToLower(), Request.Form["Param_" + s[0].ToString()] + "|" + Request.Form["Param2_" + s[0].ToString()]);
							break;
						case "12":
							Params.Add(s[1].ToString().ToLower(), Request.Form["Param_" + s[0].ToString()] + "|" + Request.Form["Paramt_" + s[0].ToString()]);
							break;
						case "1":
						case "13":
						case "2":
							if(Request.Form["Param_" + s[0].ToString()].Length>0)
							{
								if(Request.Form["Param_" + s[0].ToString()].IndexOf("-1")<0)
									Params.Add(s[1].ToString().ToLower(), Request.Form["Param_" + s[0].ToString()]);
							}
							else
							{
								if(Request.Form["SParam_" + s[0].ToString()].IndexOf("-1")<0)
									Params.Add(s[1].ToString().ToLower(), Request.Form["SParam_" + s[0].ToString()]);
							}

							break;
						case "4":
							Params.Add(s[1].ToString().ToLower(), (Request.Form["Param_" + s[0].ToString()] != null) ? Request.Form["SParam_-" + s[0].ToString()] : "false");
							break;
						case "14":
							Params.Add(s[1].ToString().ToLower(), Request.Form["Paramt_" + s[0].ToString()]);
							break;
						default:
							Params.Add(s[1].ToString().ToLower(), Request.Form["Param_" + s[0].ToString()]);
							break;
					}
					Trace.Warn("params-" + s[1].ToString().ToLower(), Request.Form["Param_" + s[0].ToString()]);
				}
				else
				{
					DataRow freeParameter = DatabaseConnection.CreateDataset("SELECT * FROM ADDEDFIELDS WHERE ID=" + s[0].ToString()).Tables[0].Rows[0];
					switch (freeParameter["Type"].ToString())
					{
						case "4":
							Params.Add("-" + freeParameter["Name"].ToString(), (Request.Form["SParam_-" + s[0].ToString()] != null) ? Request.Form["SParam_-" + s[0].ToString()] : "false");
							break;
						case "1":
						case "2":
							Params.Add("-" + freeParameter["Name"].ToString(), Request.Form["Param_-" + s[0].ToString()]);
							break;
					}
					Trace.Warn("params-" + "-" + freeParameter["Name"].ToString(), Request.Form["Param_" + s[0].ToString()]);
				}


			}

			DataTable d = qb.QBManager(Convert.ToInt32(Session["QueryID"]), Params);




			if(d.Rows.Count>0)
			{
				StringBuilder sb = new StringBuilder();

				if (!ViewOn)
				{
					switch (RadioResult.SelectedValue)
					{
						case "CSV1":
							delimiter = ",";
							break;
						case "CSV2":
							delimiter = ";";
							break;
						case "PDF":
						case "RTF":
							delimiter = ((char) 2) + "";
							break;
						default:
							delimiter = ",";
							break;
					}
				}

				if (ViewOn)
					sb.Append("<table class=\"normal\" id=\"unique_id\" width=\"98%\" border=1 cellspacing=0 bgcolor=#ffffff><tr>");
				int errocount = 0;
				try
				{
					foreach (DataColumn cc in d.Columns)
					{
						if (ViewOn)
							sb.AppendFormat("<th>{0}</th>", (cc.ColumnName.StartsWith("{+}") || cc.ColumnName.StartsWith("{t}")) ? cc.ColumnName.Substring(3, cc.ColumnName.Length - 3) : cc.ColumnName);
						else
							sb.AppendFormat("\"{0}\"{1}", (cc.ColumnName.StartsWith("{+}") || cc.ColumnName.StartsWith("{t}")) ? cc.ColumnName.Substring(3, cc.ColumnName.Length - 3) : cc.ColumnName, delimiter);
					errocount++;
					}
				}catch(Exception ex)
				{
					string error = "DEBUG CHECK\r\n" + Session["QueryID"].ToString() + "\r\n";
					foreach(DictionaryEntry de in  Params)
					error += de.Key + " - "+ de.Value + "\r\n";
					foreach (DataColumn cc in d.Columns)
					error += ">" + cc.ColumnName + "\r\n";
					error += errocount;
					throw new Exception(ex.Message + "\r\n" + error);
				}

				if (ViewOn)
					sb.Append("</tr>");
				else
				{
					if (RadioResult.SelectedValue == "PDF" || RadioResult.SelectedValue == "RTF")
					{
						if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
						sb.Append((char) 1);
					}
					else
					{
						if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
						sb.Append("\r\n");
					}
				}

				double[] sum = new double[d.Columns.Count];
				bool[] sumType = new bool[d.Columns.Count];
				bool boolSum = false;
				foreach (DataRow dr in d.Rows)
				{
					int indexSum = 0;
					if (ViewOn) sb.Append("<tr>");
					foreach (DataColumn cc in d.Columns)
					{
						if (cc.ColumnName.StartsWith("{+}") || cc.ColumnName.StartsWith("{t}"))
						{
							double sumtoadd=0;
							try
							{
								sumtoadd=Convert.ToDouble(dr[cc.ColumnName]);
							}
							catch
							{
								sumtoadd=0;
							}
							sum[indexSum] += sumtoadd;
							sumType[indexSum] = (cc.ColumnName.StartsWith("{t}")); // true = tempo , false = numerico
							boolSum = true;
						}
						string timeToPrint = String.Empty;
						if (cc.ColumnName.StartsWith("{t}"))
						{
							int duration = 0;
							try
							{
								duration=Convert.ToInt32(dr[cc.ColumnName]);
							}
							catch
							{
								duration=0;
							}

							if (duration > 0)
							{
								if (duration < 60)
								{
									timeToPrint = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
								}
								else
								{
									timeToPrint = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
										((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
								}

							}
						}
						if (ViewOn)
						{
							if (cc.ColumnName.StartsWith("{t}"))
								sb.AppendFormat("<td valign=top>{0}&nbsp;</td>", timeToPrint);
							else
                                if(dr[cc.ColumnName].ToString().StartsWith("Mottxt"))
                                {
                                    sb.AppendFormat("<td valign=top>{0}&nbsp;</td>", Root.rm.GetString(dr[cc.ColumnName].ToString()));
                                }else{
                                        sb.AppendFormat("<td valign=top>{0}&nbsp;</td>", dr[cc.ColumnName]);
                                }

						}
						else
						{
							if (dr[cc.ColumnName].ToString().Length > 0)
								if (cc.ColumnName.StartsWith("{t}"))
									sb.AppendFormat("\"{0}\"{1}", timeToPrint, delimiter);
								else
									sb.AppendFormat("\"{0}\"{1}", dr[cc.ColumnName], delimiter);
							else
								sb.AppendFormat("{0}", delimiter);
						}
						indexSum++;
					}
					if (ViewOn)
						sb.Append("</tr>");
					else
					{
						if (RadioResult.SelectedValue == "PDF" || RadioResult.SelectedValue == "RTF")
						{
							sb.Remove(sb.Length - 1, 1);
							sb.Append((char) 1);
						}
						else
						{
							sb.Remove(sb.Length - 1, 1);
							sb.Append("\r\n");
						}
					}
				}

				if (boolSum)
				{
					if (ViewOn)
					{
						sb.Append("<tr>");

						for (int i = 0; i < sum.Length; i++)
						{
							sb.Append("<td>");
							if (sum[i] > 0)
							{
								if (sumType[i])
								{
									int duration = Convert.ToInt32(sum[i]);
									string timeToPrint = String.Empty;
									if (duration > 0)
									{
										if (duration < 60)
										{
											timeToPrint = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
										}
										else
										{
											timeToPrint = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
												((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
										}
									}
									sb.Append("<b>" + timeToPrint + "</b>");
								}
								else
									sb.Append("<b>" + sum[i].ToString() + "</b>");
							}
							else
								sb.Append("&nbsp;");
							sb.Append("</td>");
						}
						sb.Append("</tr>");
					}
					else
					{
						for (int i = 0; i < sum.Length; i++)
						{
							if (sum[i] > 0)
								if (sumType[i])
								{
									int duration = Convert.ToInt32(sum[i]);
									string timeToPrint = String.Empty;
									if (duration > 0)
									{
										if (duration < 60)
										{
											timeToPrint = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
										}
										else
										{
											timeToPrint = ((Convert.ToInt32(duration/60) > 9) ? Convert.ToInt32(duration/60).ToString() : "0" + Convert.ToInt32(duration/60).ToString()) + ":" +
												((Convert.ToInt32(duration%60) > 9) ? Convert.ToInt32(duration%60).ToString() : "0" + Convert.ToInt32(duration%60).ToString());
										}
									}
									sb.AppendFormat("\"{0}\"{1}", timeToPrint, delimiter);
								}
								else
									sb.AppendFormat("\"{0}\"{1}", sum[i].ToString(), delimiter);
							else
								sb.AppendFormat("{0}", delimiter);
						}
						sb.Remove(sb.Length - 1, 1);
						sb.Append("\r\n");
					}
				}

				if (ViewOn)
				{
					sb.Append("</table>");
					if (RadioResult.SelectedValue == "HTML")
					{
                        sb.Append("</table><script src=\"/js/reporthtml.js\"></script>");
                        QBResult.Text = sb.ToString();
					}
				}
				else if (RadioResult.SelectedValue == "Excel")
				{
					ExportUtils eu = new ExportUtils();
					DataSet dt = new DataSet();
					dt.Tables.Add(d);
					eu.ExportDataSet(dt, "report", ExportUtils.DataSetExportType.ExcelXML, HttpContext.Current);
					return;
				}
				else if (RadioResult.SelectedValue == "XML")
				{
					ExportUtils eu = new ExportUtils();
					DataSet dt = new DataSet();
					dt.Tables.Add(d);
					eu.ExportDataSet(dt, "report", ExportUtils.DataSetExportType.XML, HttpContext.Current);
					return;
				}
				else if (RadioResult.SelectedValue == "ADO")
				{
					ExportUtils eu = new ExportUtils();
					DataSet dt = new DataSet();
					dt.Tables.Add(d);
					eu.ExportDataSet(dt, "report", ExportUtils.DataSetExportType.ADORecordSet, HttpContext.Current);
					return;
				}
				else
				{
					if (RadioResult.SelectedValue != "PDF" && RadioResult.SelectedValue != "RTF")
					{
						Response.AddHeader("Content-Disposition", "attachment; filename=Result.csv");
						Response.AddHeader("Expires", "Thu, 01 Dec 1994 16:00:00 GMT ");
						Response.AddHeader("Pragma", "nocache");
						Response.ContentType = "application/vnd.ms-excel";
						Response.Write(sb.ToString());
						return;
					}
					else if (RadioResult.SelectedValue == "PDF")
					{
						PDFReport p = new PDFReport();
						MemoryStream MyStream = p.PDFRender(d);
						Response.AddHeader("Content-Disposition", "attachment; filename=report.pdf");
						Response.CacheControl = "Private";
						Response.AddHeader("Pragma", "No-Cache");
						Response.ContentType = "application/pdf";
						Response.BinaryWrite(MyStream.GetBuffer());
						Response.End();
						return;
					}
					else
					{
						PDFReport p = new PDFReport();
						MemoryStream MyStream = p.RTFRender(d);
						Response.AddHeader("Content-Disposition", "attachment; filename=report.rtf");
						Response.CacheControl = "Private";
						Response.AddHeader("Pragma", "No-Cache");
						Response.ContentType = "application/rtf";
						Response.BinaryWrite(MyStream.GetBuffer());
						Response.End();
						return;
					}
				}
			}
			else
			{
				QBResult.ForeColor = Color.Red;
				QBResult.CssClass = "normal";
				QBResult.Text=Root.rm.GetString("QBUtxt26");
			}
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
		this.Load += new EventHandler(this.Page_Load);
			this.BtnSearch.Click +=new EventHandler(BtnSearch_Click);
			this.BtnDelCategory.Click +=new EventHandler(btndelcategory_Click);
			this.QBSubmitStep3F.Click += new EventHandler(this.btn_Click);
			this.QBRepeater.ItemCommand += new RepeaterCommandEventHandler(this.QBRepeaterCommand);
			this.QBRepeater.ItemDataBound += new RepeaterItemEventHandler(this.QBRepeaterItemDataBound);

		}
		#endregion

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			StringBuilder SearchSQL = new StringBuilder();
			SearchSQL.AppendFormat("SELECT ID,DESCRIPTION,QUERYTYPE,TITLE FROM QB_CUSTOMERQUERY WHERE ({0}) AND QUERYTYPE=0 ",GroupsSecure("QB_CUSTOMERQUERY.GROUPS"));
			if(Search.Text.Length>0)
			{
				SearchSQL.AppendFormat("AND (TITLE LIKE '%{0}%' OR DESCRIPTION LIKE '%{0}%') ",DatabaseConnection.FilterInjection(Search.Text));
			}
			if(ListCategory.SelectedValue!="0")
			{
				SearchSQL.AppendFormat("AND (CATEGORY='{0}') ",ListCategory.SelectedValue);
			}
			SearchSQL.Append("ORDER BY TITLE");
			this.FillCustomerQuery(SearchSQL.ToString());
		}

		private void btndelcategory_Click(object sender, EventArgs e)
		{
			DatabaseConnection.DoCommand(String.Format("UPDATE QB_CUSTOMERQUERY SET CATEGORY=0 WHERE CATEGORY={0}",int.Parse(ListCategory.SelectedValue)));
			DatabaseConnection.DoCommand(String.Format("DELETE FROM QB_CATEGORIES WHERE ID={0}",int.Parse(ListCategory.SelectedValue)));
			FillListCategory();
		}

		public enum QueryType
		{
			Customer=0,
			None,
			NotSelectable,
			Company,
			Leads,
			Activity,
			Opportuniy
		}
	}
}
