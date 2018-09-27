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
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Brettle.Web.NeatUpload;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena
{
	public partial class ImportExternalData : G
	{

		public string importMap = String.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			Matchdiv.Visible = false;
			dataform.Visible = false;

			DeleteGoBack();


			if (!Page.IsPostBack)
			{
				HelpLabel.Text = FillHelp("loadcsv");
				TableImport.Items.Add(new ListItem(Root.rm.GetString("Csvtxt3"), ((int) CRMTables.Base_Companies).ToString()));
				TableImport.Items.Add(new ListItem(Root.rm.GetString("Csvtxt4"), ((int) CRMTables.Base_Contacts).ToString()));

                Modules M = new Modules();
                M.ActiveModule = UC.Modules;
                if (M.IsModule(ActiveModules.Lead))
                    TableImport.Items.Add(new ListItem(Root.rm.GetString("Csvtxt22"), ((int) CRMTables.CRM_Leads).ToString()));

				TableImport.Items.Add(new ListItem(Root.rm.GetString("Csvtxt27"), ((int) CRMTables.CRM_WorkActivity).ToString()));

				TableImport.Items[0].Selected = true;

				CmdSend.Text =Root.rm.GetString("Csvtxt20");
				CmdSend.Attributes.Add("onclick", "ShowProgressBar()");
				CmdLoadData.Text =Root.rm.GetString("Csvtxt21");

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
			this.CmdSend.Click += new EventHandler(this.Upload_Click);
			this.CmdLoadData.Click += new EventHandler(this.Btn_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		public void Btn_Click(object sender, EventArgs e)
		{
			DataSet dsCSV = null;
			if (Cache[UC.UserId + "CSV"] is DataSet)
			{
				dsCSV = Cache[UC.UserId + "CSV"] as DataSet;
				Cache.Remove(UC.UserId + "CSV");
			}
			else
			{
				SendError("Cache Expired during import", UC.UserRealName + " - " + UC.UserName);
				Context.Items["warning"] = "Cache Expired during import!";
				Response.Redirect("/");
			}
			string tableRef = String.Empty;
			int loadedRows = 0;
			int savedActivities = 0;
			int reduntantRows = 0;
			int unmatchingRows = 0;

			switch (((LinkButton) sender).ID)
			{
				case "RefreshRepCategories":
					FillRepCategories();
					break;
				case "CmdLoadData":

					GetMap((CRMTables) int.Parse(TableImport.SelectedValue));
					tableRef = _CRMtables[int.Parse(TableImport.SelectedValue)];

					DataTable dtMatch = XMLDataTable();
					DataTable dtMatchFields = XMLDataTable();

					bool csvOk = true;
					try
					{
						if (dsCSV.Tables["CSVData"].Rows.Count < 1)
							csvOk = false;
					}
					catch
					{
						csvOk = false;
					}


					if (csvOk)
					{


						string sqlString = "SELECT * FROM " + tableRef + " WHERE ID = -1";
						IDataReader rd = DatabaseConnection.CreateReader(sqlString);

						DataTable St = rd.GetSchemaTable();
						DataTable dt = new DataTable();
						DataColumn dc;

						for (int i = 0; i < St.Rows.Count; i ++)
						{
							dc = new DataColumn(St.Rows[i]["ColumnName"].ToString(), Type.GetType(St.Rows[i]["DataType"].ToString()));
							dc.Unique = Convert.ToBoolean(St.Rows[i]["IsUnique"]);
							dc.AllowDBNull = Convert.ToBoolean(St.Rows[i]["AllowDBNull"]);
							dc.ReadOnly = Convert.ToBoolean(St.Rows[i]["IsReadOnly"]);
							if(dc.DataType.ToString() == "System.String")
							dc.MaxLength = (int) St.Rows[i]["ColumnSize"];
							dt.Columns.Add(dc);
						}
						DataRow templaterow = dt.NewRow();


						foreach (DataRow d in dsCSV.Tables["CSVData"].Rows)
						{
							bool noImport = false;
							string companyId = String.Empty;
							string referrerId = String.Empty;
							string leadId = String.Empty;
							string accountGroupId = String.Empty;
							string query = String.Empty;
							for (int i = 0; i < dtMatch.Rows.Count; i++)
							{
								if (dtMatch.Rows[i]["Must"].ToString() == "1" && (Request.Form[dtMatch.Rows[i]["Name"].ToString()] == "0" || d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString().Length == 0))
								{
									noImport = true;
									unmatchingRows++;
									break;
								}

								if (Request.Form[dtMatch.Rows[i]["Name"].ToString()] == "0")
									continue;
								if (Request.Form["Check_" + dtMatch.Rows[i]["Name"].ToString()] != null)
								{
									query += " AND " + dtMatchFields.Rows[i]["TblSrc"].ToString() + "='" + DatabaseConnection.FilterInjection(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString()) + "'";
								}
								if (dtMatchFields.Rows[i]["TblSrc"].ToString() == "companylink")
								{
									companyId = DatabaseConnection.SqlScalar(String.Format("SELECT ID FROM BASE_COMPANIES WHERE ({0}) AND LTRIM(RTRIM(COMPANYNAME))='{1}'", GroupsSecure(), DatabaseConnection.FilterInjection(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString())));
								}
								else if (dtMatchFields.Rows[i]["TblSrc"].ToString() == "referrerlink" && companyId.Length > 0)
								{
									referrerId = DatabaseConnection.SqlScalar(String.Format("SELECT ID FROM BASE_CONTACTS WHERE ({0}) AND LTRIM(RTRIM(COMPANYNAME))='{1}'", GroupsSecure(), DatabaseConnection.FilterInjection(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString())));
								}
								else if (dtMatchFields.Rows[i]["TblSrc"].ToString() == "leadlink")
								{
									leadId = DatabaseConnection.SqlScalar(String.Format("SELECT ID FROM CRM_LEADS WHERE ({0}) AND LTRIM(RTRIM(COMPANYNAME))='{1}'", GroupsSecure(), DatabaseConnection.FilterInjection(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString())));
								}
								else if (dtMatchFields.Rows[i]["TblSrc"].ToString() == "accountlink")
								{
									accountGroupId = DatabaseConnection.SqlScalar(String.Format("SELECT CAST(UID AS VARCHAR)+'|'+CAST(GROUPID AS VARCHAR) FROM ACCOUNT ((LTRNAME))+' '+LTRIM(RTRIM(SURNAME)))='{0}' OR (LTRIM(RTRIM(SURNAME))+' '+LTRIM(RTRIM(NAME)))='{0}' OR USERACCOUNT='{0}')", DatabaseConnection.FilterInjection(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString())));
								}
							}
							if (noImport)
								break;
							if (query.Length > 0)
							{
								string empty;
								empty = DatabaseConnection.SqlScalar(String.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tableRef, query));
								if (empty != "0")
								{
									d["DUP"] = true;
								}
							}
							if (!(bool) d["DUP"])
							{
								DataRow myDataRow = templaterow;

								string group = UC.UserGroupId.ToString();
								string uId = UC.UserId.ToString();
								if (accountGroupId.Length > 0)
								{
									string[] arr = accountGroupId.Split('|');
									uId = arr[0];
									group = arr[1];
								}
								myDataRow["OwnerID"] = uId;
								myDataRow["CreatedByID"] = uId;
								if (companyId.Length > 0)
									myDataRow["CompanyID"] = companyId;

								myDataRow["Groups"] = "|" + group + "|";

								if (tableRef.Equals("CRM_WorkActivity"))
								{
									bool skip = true;
									myDataRow["Type"] = "7";
									if (referrerId.Length > 0)
									{
										skip = false;
										myDataRow["ReferrerID"] = referrerId;
									}
									if (leadId.Length > 0)
									{
										skip = false;
										myDataRow["LeadID"] = leadId;
									}
									if (companyId.Length > 0)
										skip = false;
									if (skip)
									{
										savedActivities++;
										continue;
									}
								}


								string cat = "|";
								foreach (RepeaterItem it in RepCategories.Items)
								{
									CheckBox Check = (CheckBox) it.FindControl("Check");
									if (Check.Checked)
										cat += ((Literal) it.FindControl("IDCat")).Text + "|";
								}
								if (cat.Length > 1)
									myDataRow["Categories"] = cat;

								for (int i = 0; i < dtMatch.Rows.Count; i++)
								{
									if (Request.Form[dtMatch.Rows[i]["Name"].ToString()] != "0" &&
										dtMatchFields.Rows[i]["TblSrc"].ToString() != "companylink" &&
										dtMatchFields.Rows[i]["TblSrc"].ToString() != "referrerlink" &&
										dtMatchFields.Rows[i]["TblSrc"].ToString() != "leadlink" &&
										dtMatchFields.Rows[i]["TblSrc"].ToString() != "accountlink")
									{
										int fieldLen = dt.Columns[dtMatchFields.Rows[i]["TblSrc"].ToString()].MaxLength;
										if (myDataRow.Table.Columns[dtMatchFields.Rows[i]["TblSrc"].ToString()].DataType.ToString() == "System.Decimal")
										{
											try
											{
												decimal dec = StaticFunctions.FixDecimal(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString());

												myDataRow[dtMatchFields.Rows[i]["TblSrc"].ToString()] = dec;
											}
											catch
											{
											}
										}
										else if (myDataRow.Table.Columns[dtMatchFields.Rows[i]["TblSrc"].ToString()].DataType.ToString() == "System.DateTime")
										{
											DateTime today = new DateTime();
											try
											{
												today = DateTime.Parse(d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString().Trim(new char[] {'"', '\'', '\t', ' '}));
											}
											catch
											{
												today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
											}
											finally
											{
												myDataRow[dtMatchFields.Rows[i]["TblSrc"].ToString()] = UC.LTZ.ToUniversalTime(today);
											}
										}
										else if (fieldLen > 4000) //2147483647
										{
											myDataRow[dtMatchFields.Rows[i]["TblSrc"].ToString()] = d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString().Replace("\r", "").Replace("\n", ((char) 13 + (char) 10).ToString());
										}
										else
										{
											try
											{
												string colcontent = d[Request.Form[dtMatch.Rows[i]["Name"].ToString()]].ToString().Trim(new char[] {'"', '\'', '\t', ' '});

												if (colcontent.Length > fieldLen)
													colcontent = colcontent.Substring(0, fieldLen - 1);

												myDataRow[dtMatchFields.Rows[i]["TblSrc"].ToString()] = colcontent;
											}
											catch
											{
											}
										}
									}

								}
								switch ((CRMTables) int.Parse(TableImport.SelectedValue))
								{
									case CRMTables.Base_Companies:
										if (myDataRow["CompanyName"] == DBNull.Value)
											myDataRow["CompanyName"] = "N/A";
										break;
									case CRMTables.Base_Contacts:
									case CRMTables.CRM_Leads:
										if (myDataRow["Surname"] == DBNull.Value)
											myDataRow["Surname"] = "N/A";
										break;
									case CRMTables.CRM_WorkActivity:
										if (myDataRow["Subject"] == DBNull.Value)
											myDataRow["Subject"] = "N/A";
										break;
								}

								StringBuilder sbColumn = new StringBuilder();
								StringBuilder sbValue = new StringBuilder();
								sbColumn.AppendFormat("INSERT INTO {0} (", tableRef);
								sbValue.Append(" VALUES (");
								DbSqlParameterCollection sqlParam = new DbSqlParameterCollection();

								foreach (DataColumn cc in myDataRow.Table.Columns)
								{
									if (cc.ColumnName.ToUpper() != "ID" && myDataRow[cc.ColumnName] != DBNull.Value)
									{
										sbColumn.AppendFormat("{0},", cc.ColumnName);
										sbValue.AppendFormat("@{0},", cc.ColumnName);
										sqlParam.Add(new DbSqlParameter("@" + cc.ColumnName, myDataRow[cc.ColumnName]));
									}
								}
								DatabaseConnection.DoCommand(string.Format("{0}){1});", sbColumn.ToString(0, sbColumn.Length - 1), sbValue.ToString(0, sbValue.Length - 1)), sqlParam);

								loadedRows++;
							}
							else
							{
								reduntantRows++;
							}
						}

						LblInfo.Text =Root.rm.GetString("Csvtxt9") + loadedRows.ToString() + "<br>" +
							Root.rm.GetString("Csvtxt10") + reduntantRows.ToString() + "<br>" +
							Root.rm.GetString("Csvtxt10") + unmatchingRows.ToString();
						UploadForm.Visible = false;
						dataform.Visible = false;
						Matchdiv.Visible = false;
					}
					else
					{
						ClientScript.RegisterStartupScript(this.GetType(), "NoData", "<script>alert('" +Root.rm.GetString("Csvtxt26") + "');</script>");
					}
					break;
			}
			HelpLabel.Visible = false;
			Session.Remove("CSV");

		}

		private void GetMap(CRMTables tableName)
		{
			switch (tableName)
			{
				case CRMTables.Base_Companies:
					if (ConfigSettings.SupportedLanguages.ToLower().IndexOf(UC.CultureSpecific.ToLower()) > -1)
						importMap = String.Format("CompanyImportMap_{0}.xml", UC.CultureSpecific.ToLower());
					else
						importMap = "CompanyImportMap_en.xml";
					break;
				case CRMTables.Base_Contacts:
					if (ConfigSettings.SupportedLanguages.ToLower().IndexOf(UC.CultureSpecific.ToLower()) > -1)
						importMap = String.Format("ContactImportMap_{0}.xml", UC.CultureSpecific.ToLower());
					else
						importMap = "ContactImportMap_en.xml";
					break;
				case CRMTables.CRM_Leads:
					if (ConfigSettings.SupportedLanguages.ToLower().IndexOf(UC.CultureSpecific.ToLower()) > -1)
						importMap = String.Format("LeadImportMap_{0}.xml", UC.CultureSpecific.ToLower());
					else
						importMap = "LeadImportMap_en.xml";
					break;
				case CRMTables.CRM_WorkActivity:
					if (ConfigSettings.SupportedLanguages.ToLower().IndexOf(UC.CultureSpecific.ToLower()) > -1)
						importMap = String.Format("ActivityImportMap_{0}.xml", UC.CultureSpecific.ToLower());
					else
						importMap = "ActivityImportMap_en.xml";
					break;
			}
		}

		public void Upload_Click(object sender, EventArgs e)
		{
			if (this.filMyFile.HasFile)
			{
				Matchdiv.Visible = true;
				dataform.Visible = true;
				char divider = ',';
				GetMap((CRMTables) int.Parse(TableImport.SelectedItem.Value));

				if (Path.GetExtension(this.filMyFile.FileName).ToUpper() == ".CSV")
				{
					int nFileLen = (int) this.filMyFile.FileContent.Length;
					if (nFileLen > 0)
					{
						UploadForm.Visible = false;
						byte[] myData = new byte[nFileLen];
						Stream IntermediateStream = this.filMyFile.FileContent;
						IntermediateStream.Position = 0;
						IntermediateStream.Read(myData, 0, nFileLen);
						IntermediateStream.Close();
						string strFilename = Path.GetFileName(this.filMyFile.FileName);
						string content = DetectEncoding(myData) + "\r\n";


						LblInfo.Text =Root.rm.GetString("Csvtxt18") + " " + strFilename + "<br>" +Root.rm.GetString("Csvtxt19") + " " + nFileLen.ToString() + "<br>";

						switch (CsvType.SelectedValue)
						{
							case "V":
								divider = ',';
								break;
							case "PV":
								divider = ';';
								break;
							case "TB":
								divider = '\t';
								break;
						}
						ArrayList arrayList = null;
						try
						{
							arrayList = CsvLinesSplitter(content.Replace("\r", "").Replace("\r\r", "\r"), divider);
						}
						catch (ColumnsDiffersFromHeaders ex)
						{
							LblInfo.Text = String.Format(Root.rm.GetString("Csvtxt29"), ex.Message);
							LblInfo.ForeColor = Color.Red;
							Matchdiv.Visible = false;
							return;
						}
						catch (ColumnIndexOverflow ex)
						{
							LblInfo.Text = String.Format(Root.rm.GetString("Csvtxt29"), ex.Message);
							LblInfo.ForeColor = Color.Red;
							Matchdiv.Visible = false;
							return;
						}
						catch (MoreEmptyRowsFound ex)
						{
							LblInfo.Text = String.Format(Root.rm.GetString("Csvtxt24"), ex.Message);
							LblInfo.ForeColor = Color.Red;
							Matchdiv.Visible = false;
							return;
						}
						if (arrayList.Count == 0 || ((string[]) arrayList[0]).Length == 0)
						{
							LblInfo.Text = LblInfo.Text +Root.rm.GetString("Csvtxt7") + " [" + divider + "]";
							LblInfo.ForeColor = Color.Red;
							Results.Visible = false;
							Matchdiv.Visible = false;
							UploadForm.Visible = true;
							return;
						}

						string errorRows = String.Empty;
						DataSet dsCSV = BuildCSVDataSet(((string[]) arrayList[0]));
						if (dsCSV == null)
						{
							LblInfo.Text =Root.rm.GetString("Csvtxt28");
							LblInfo.ForeColor = Color.Red;
							Results.Visible = false;
							Matchdiv.Visible = false;
							UploadForm.Visible = true;

							return;
						}

						for (int i = 1; i < arrayList.Count; i++)
						{
							string[] fieldArr = ((string[]) arrayList[i]);
							DataRow dataRow = dsCSV.Tables["CSVData"].NewRow();

							int rowFields = 0;
							if (fieldArr.GetUpperBound(0) < dsCSV.Tables["CSVData"].Columns.Count)
								rowFields = fieldArr.GetUpperBound(0);
							else
							{
								rowFields = dsCSV.Tables["CSVData"].Columns.Count;
								errorRows += "," + i.ToString();
							}
							Regex r = new Regex(@"(?s)\b.{1,3999}\b");
							for (int ii = 0; ii <= rowFields; ii++)
							{
								if (fieldArr[ii] == null)
									break;
								string inportData = fieldArr[ii].Trim(new char[] {'"', '\'', '\t', ' '});
								if (inportData.Length < 4000)
									dataRow[ii + 2] = inportData;
								else
									dataRow[ii + 2] = r.Match(inportData);
							}
							dsCSV.Tables["CSVData"].Rows.Add(dataRow);
						}
						arrayList = null;
						LblInfo.Text +=Root.rm.GetString("Csvtxt8") + " " + dsCSV.Tables["CSVData"].Rows.Count.ToString() + "<BR>";


						Trace.Warn("lblInfo", LblInfo.Text);
						Cache.Add(UC.UserId + "CSV", dsCSV, null, DateTime.Now.AddMinutes(15), TimeSpan.Zero, CacheItemPriority.NotRemovable, null);


						DataTable ddd = new DataTable();
						ddd = dsCSV.Tables[0].Clone();
						foreach (DataRow dr in dsCSV.Tables[0].Select("ID<11"))
						{
							DataRow dataRow = ddd.NewRow();
							for (int ii = 0; ii < ddd.Columns.Count; ii++)
							{
								DataRow OriginRow = dr;
								dataRow[ii] = OriginRow[ii].ToString();
							}
							ddd.Rows.Add(dataRow);
						}
						table.DataSource = ddd;
						table.DataBind();
						table.Columns.Clear();

						for (int i = 0; i < ddd.Columns.Count; i++)
						{
							BoundColumn objbc = new BoundColumn();
							objbc.DataField = ddd.Columns[i].ColumnName;
							objbc.HeaderText = ddd.Columns[i].ColumnName;

							if (objbc.DataField == "DUP")
								objbc.Visible = false;


							table.Columns.Add(objbc);
						}


						table.Visible = true;
						dataform.Visible = true;
						LblMatch.Text = MatchGrid(XMLDataTable(), dsCSV.Tables["CSVData"]);
						Matchdiv.Visible = true;
						HelpLabel.Text = FillHelp("loadcsv2");
						Results.Visible = true;

						FillRepCategories();


					}
				}
				else
				{
					ClientScript.RegisterStartupScript(this.GetType(), "error","<script>alert('" + Root.rm.GetString("Csvtxt11")+"');</script>");
					Matchdiv.Visible = false;
				}


			}
			else
			{
				ClientScript.RegisterStartupScript(this.GetType(), "nofile", "<script>alert('" +Root.rm.GetString("Csvtxt25") + "');</script>");
			}

		}


		private ArrayList CsvLinesSplitter(string content, char separator)
		{
			ArrayList arr = new ArrayList();
			string[] csvColumns = null;
			bool firstLine = true;
			bool oneEmptyRowFound = false;
			int dividerCount = 0;
			int quotesCount = 0;
			int rowCount = -1;
			int lastDividerPosition = 0;
			int csvColumnsCount = 0;
			bool insideQuotes = false;
			for (int i = 0; i < content.Length; i++)
			{
				switch (content[i])
				{
					case ',':
					case ';':
					case '\t':
						if (content[i] != separator)
							break;
						if (insideQuotes)
							break;
						if (!firstLine)
						{
							if (dividerCount == csvColumnsCount)
								throw new ColumnsDiffersFromHeaders(rowCount.ToString());
							csvColumns[dividerCount] = content.Substring(lastDividerPosition, i - lastDividerPosition);
							lastDividerPosition = i + 1;
						}
						dividerCount++;
						break;
					case '"':
						quotesCount++;
						insideQuotes = (insideQuotes) ? false : true;
						break;
					case '\n':
						if (insideQuotes)
							break;
						if (firstLine)
						{
							firstLine = false;
							i = -1;
							csvColumnsCount = dividerCount;
						}
						else
						{
							if (dividerCount == 0)
							{
								if (oneEmptyRowFound)
									throw new MoreEmptyRowsFound(rowCount.ToString());

								oneEmptyRowFound = true;
								break;
							}



							csvColumns[dividerCount] = content.Substring(lastDividerPosition, i - lastDividerPosition);
							arr.Add(csvColumns);
						}
						csvColumns = new string[csvColumnsCount+1];
						dividerCount = quotesCount = 0;
						lastDividerPosition = i + 1;
						rowCount++;
						insideQuotes = false;
						break;
				}
			}
			return arr;
		}

		private string[] _CRMtables = new string[] {null, "Base_Companies", "Base_Contacts", "CRM_Leads", "CRM_WorkActivity"};



		private string MatchGrid(DataTable dt1, DataTable dt2)
		{
			bool asteriskNote = false;
			int iimover;
			StringBuilder output = new StringBuilder();
			StringBuilder verifyOut = new StringBuilder();
			ArrayList dt2array = new ArrayList();

			output.Append("<table class=normal width=\"600\"><tr><th colspan=\"3\">" +Root.rm.GetString("Csvtxt13") + "</th></tr><tr><td width=\"50%\">" +Root.rm.GetString("Csvtxt15") + "</td><td width=\"50%\">" +Root.rm.GetString("Csvtxt16") + "</td><td width=\"1%\">" +Root.rm.GetString("Csvtxt14") + "</td></tr>");
			for (int i = 0; i < dt1.Rows.Count; i++)
			{
				output.AppendFormat("<tr><td>{0}</td>", HttpUtility.HtmlEncode(dt1.Rows[i]["Name"].ToString().Replace("_", " ")));
				output.AppendFormat("<td><select name=\"{0}\" id=\"{0}\" width=\"100%\" class=BoxDesign>", dt1.Rows[i]["Name"].ToString().ToLower());
				dt2array.Clear();
				dt2array.AddRange(dt2.Columns);
				iimover = 1;
				for (int ii = 0; ii < dt2array.Count; ii++)
				{

					Regex ex = new Regex(dt1.Rows[i]["Matches"].ToString(), RegexOptions.IgnoreCase);
					if (ex.IsMatch(dt2array[ii].ToString()))
					{
						dt2array.Insert(iimover++, dt2array[ii]);
						dt2array.RemoveAt(ii + 1);
						break;
					}
				}
				for (int ii = 1; ii < dt2array.Count; ii++)
				{
					if (dt2array[ii].ToString() != "DUP")
						output.AppendFormat("<option>{0}</option>", dt2array[ii].ToString().Trim(new char[] {'"', '\'', '\t', ' '}).Replace("_", " "));
				}
				if (dt2array[1].ToString() == "DUP")
					output.Append("<option value=0 selected>" +Root.rm.GetString("Csvtxt17") + "</option>");
				else
					output.Append("<option value=0>" +Root.rm.GetString("Csvtxt17") + "</option>");
				output.Append("</select></td>\r\n");
				if (dt1.Rows[i]["Name"].ToString().ToLower().IndexOf("cross") < 0)
					output.AppendFormat("<td><input type=checkbox name={0}></td></tr>", "Check_" + dt1.Rows[i]["Name"].ToString().ToLower());
				else
					output.Append("<td></td></tr>");
				if (dt1.Rows[i]["Name"].ToString().ToLower().IndexOf("*") > -1)
					asteriskNote = true;
			}
			output.Append("</table>");
			if (asteriskNote)
				output.Append(Root.rm.GetString("Csvtxt30"));
			return output.ToString();
		}


		private DataTable XMLDataTable()
		{
			DataSet ds = new DataSet();

			ds.ReadXml(Request.PhysicalApplicationPath + "ImportMaps" + Path.DirectorySeparatorChar + importMap);
			foreach (DataRow dr in ds.Tables[0].Rows)
				dr["name"] = dr["Name"].ToString().ToLower().Replace(" ", "_");
			return ds.Tables[0];
		}

		private DataSet BuildCSVDataSet(string[] fieldArr)
		{
			if (fieldArr.GetUpperBound(0) > 0)
			{
				string[] matchfield = new string[fieldArr.Length];
				for (int i = 0; i < fieldArr.Length; i++)
				{
					fieldArr[i] = fieldArr[i].Trim(new char[] {'"', '\'', '\t', ' '}).ToLower();
					bool duplicate = false;
					foreach (string field in matchfield)
					{
						if (!StaticFunctions.IsBlank(field))
						{
							string s = field.Trim(new char[] {'"', '\'', '\t', ' '}).ToLower();
							if (fieldArr[i].Equals(s))
							{
								duplicate = true;
								break;
							}
						}
					}
					if (duplicate)
						matchfield[i] = fieldArr[i] + "_" + i.ToString();
					else
						matchfield[i] = fieldArr[i];
				}

				DataSet ds = new DataSet("Import");
				DataTable dtCSV = new DataTable("CSVData");
				ds.Tables.Add(dtCSV);
				DataColumn dc = dtCSV.Columns.Add("ID", typeof (int));
				dc.AllowDBNull = false;
				dc.AutoIncrement = true;
				dc.AutoIncrementSeed = 1;
				dc.AutoIncrementStep = 1;
				dc.Unique = true;
				dtCSV.PrimaryKey = new DataColumn[] {dc};
				DataColumn dup = dtCSV.Columns.Add("DUP", typeof (bool));
				dup.AllowDBNull = false;
				dup.DefaultValue = false;
				foreach (string field in matchfield)
				{
					dc = dtCSV.Columns.Add(field.ToString().Trim(' ', (char) 13), typeof (string));
					dc.MaxLength = 4000;
					dc.DefaultValue = String.Empty;
				}
				return ds;
			}
			return null;
		}


		private string DetectEncoding(byte[] bom)
		{
			return DetectEncoding(bom, false);
		}

		private string DetectEncoding(byte[] bom, bool utf8)
		{
			if (utf8)
				return Encoding.UTF8.GetString(bom);
			else
			{
				if ((bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)) // utf-8
					return Encoding.UTF8.GetString(bom);
				else if ((bom[0] == 0xff && bom[1] == 0xfe) || // ucs-2le, ucs-4le, and ucs-16le
					(bom[0] == 0xfe && bom[1] == 0xff) || // utf-16 and ucs-2
					(bom[0] == 0 && bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff)) // ucs-4
				{
					return Encoding.Unicode.GetString(bom);
				}
				else
				{
					return Encoding.Default.GetString(bom);
				}
			}

		}

		private void FillRepCategories()
		{
			string from = String.Empty;
			switch ((CRMTables) int.Parse(TableImport.SelectedValue))
			{
				case CRMTables.Base_Companies:
					from = "CRM_ContactCategories";
					break;
				case CRMTables.Base_Contacts:
				case CRMTables.CRM_Leads:
					from = "CRM_ReferrerCategories";
					break;
				case CRMTables.CRM_WorkActivity:
					tablecat.Visible = false;
					return;
			}
			DataTable dt;
			dt = DatabaseConnection.CreateDataset("SELECT ID,DESCRIPTION,'0' AS TOCHECK FROM " + from + " WHERE (FLAGPERSONAL=0 OR (FLAGPERSONAL=1 AND CREATEDBYID=" + UC.UserId + ")) ORDER BY FLAGPERSONAL DESC").Tables[0];
			if (dt.Rows.Count > 0)
			{
				RepCategories.DataSource = new DataView(dt, "", "tocheck desc", DataViewRowState.CurrentRows);
				RepCategories.DataBind();
			}
			else
				tablecat.Visible = false;
		}

	}
}
