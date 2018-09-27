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
using System.Web;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class QueryBuilderManager
	{
		public DataTable QBManager(int id, Hashtable htParams, bool fromId)
		{
			return QBManager(id, htParams, -1, ((UserConfig) HttpContext.Current.Session["UserConfig"]).UserGroupId, fromId);
		}

		public DataTable QBManager(int id, Hashtable htParams)
		{
			return QBManager(id, htParams, -1, ((UserConfig) HttpContext.Current.Session["UserConfig"]).UserGroupId, true);
		}

		public DataTable QBManager(int id, Hashtable htParams, int companyId, int groupId, bool fromId)
		{
			string idQuery = String.Empty;
			if (fromId)
				idQuery = id.ToString();
			else
				idQuery = DatabaseConnection.SqlScalar("SELECT ID FROM QB_CUSTOMERQUERY WHERE FROMWAP=" + id.ToString());
			string grouping = String.Empty;
			string orderby = String.Empty;

			string tblstring = String.Empty;
			int tblindex = 0;

			UserConfig ucCurrent = (UserConfig) HttpContext.Current.Session["UserConfig"];
			bool orderAndGroup = false;
			{
				StringBuilder TablesString = new StringBuilder();
				StringBuilder fieldsString = new StringBuilder();
				StringBuilder ParamsString = new StringBuilder();
				StringBuilder finalQuery = new StringBuilder();
				string firstTable = String.Empty;

				string qbTablesQuery = "SELECT QB_ALL_TABLES.ID, QB_ALL_TABLES.TABLENAME, QB_ALL_TABLES.FIXEDQUERY " +
					"FROM QB_CUSTOMERQUERYTABLES " +
					"INNER JOIN QB_ALL_TABLES ON QB_CUSTOMERQUERYTABLES.IDTABLE=QB_ALL_TABLES.ID " +
					"WHERE QB_CUSTOMERQUERYTABLES.IDQUERY=" + idQuery + " ORDER BY QB_CUSTOMERQUERYTABLES.MAINTABLE DESC";

				DataSet QBTables = DatabaseConnection.CreateDataset(qbTablesQuery);
				if (QBTables.Tables[0].Rows.Count > 0)
				{
					firstTable = QBTables.Tables[0].Rows[0][1].ToString();
					TablesString.Append(firstTable);

					tblstring = firstTable + "|";

					if (QBTables.Tables[0].Rows.Count > 1)
					foreach (DataRow d in QBTables.Tables[0].Rows)
					{
						string sqlJoin = "SELECT QB_ALL_TABLES.TABLENAME AS FIRSTTABLENAME,QB_ALL_TABLES.FIXEDQUERY, QB_ALL_TABLES_1.TABLENAME AS SECONDTABLENAME, QB_JOIN.FIRSTFIELD, QB_JOIN.SECONDFIELD, QB_JOIN.TYPE " +
							"FROM   QB_JOIN INNER JOIN " +
							"QB_ALL_TABLES ON QB_JOIN.FIRSTTABLEID = QB_ALL_TABLES.ID INNER JOIN " +
							"QB_ALL_TABLES QB_ALL_TABLES_1 ON QB_JOIN.SECONDTABLEID = QB_ALL_TABLES_1.ID " +
							"WHERE QB_JOIN.SECONDTABLEID=" + QBTables.Tables[0].Rows[0][0].ToString() + " AND QB_JOIN.FIRSTTABLEID=" + d[0].ToString();

						DataSet QBJoin = DatabaseConnection.CreateDataset(sqlJoin);
						if (QBJoin.Tables[0].Rows.Count > 0)
						{
							DataRow jd = QBJoin.Tables[0].Rows[0];
							switch ((byte) jd["type"])
							{
								case 0:
									string fixedq = jd["fixedquery"].ToString();
									if(fixedq.Length>0)fixedq=" AND "+fixedq;
									TablesString.AppendFormat(" LEFT OUTER JOIN {0} AS {4} ON {1}.{2} = {4}.{3}{5}", jd["FirstTableName"], jd["SecondTableName"], jd["SecondField"], jd["FirstField"], (tblstring.IndexOf(jd["FirstTableName"].ToString()) != -1) ? jd["FirstTableName"].ToString() + tblindex++.ToString() : jd["FirstTableName"].ToString(),fixedq);
									break;
								case 1:
									TablesString.AppendFormat(" LEFT OUTER JOIN {0} as {4} ON {1}.{2} like '%|'+cONVERT(VARCHAR(10),{4}.{3})+'|%'", jd["FirstTableName"], jd["SecondTableName"], jd["SecondField"], jd["FirstField"], (tblstring.IndexOf(jd["FirstTableName"].ToString()) != -1) ? jd["FirstTableName"].ToString() + tblindex++.ToString() : jd["FirstTableName"].ToString());
									break;
							}
							tblstring += jd["FirstTableName"] + "|";
						}
					}
				}
				else
				{
				}

				string qf = "sELECT QB_ALL_FIELDS.RMVALUE,QB_ALL_FIELDS.FIELD,QB_ALL_FIELDS.JOINID,QB_ALL_FIELDS.PARENTFIELD,QB_ALL_FIELDS.FIELDTYPE,QB_ALL_TABLES.TABLENAME,QB_CUSTOMERQUERYFIELDS.COLUMNNAME,QB_ALL_FIELDS.ID,QB_ALL_FIELDS.TABLEID FROM QB_CUSTOMERQUERYFIELDS " +
					"LEFT OUTER JOIN QB_ALL_FIELDS ON QB_CUSTOMERQUERYFIELDS.IDFIELD=QB_ALL_FIELDS.ID " +
					"LEFT OUTER JOIN QB_ALL_TABLES ON QB_ALL_FIELDS.TABLEID=QB_ALL_TABLES.ID " +
					"WHERE QB_CUSTOMERQUERYFIELDS.IDQUERY=" + idQuery + " AND QB_CUSTOMERQUERYFIELDS.FIELDVISIBLE=1 AND QB_CUSTOMERQUERYFIELDS.IDTABLE>0"; // and IDTable="+d[0].ToString();

				DataSet qpFieldsQuery = DatabaseConnection.CreateDataset(qf);

				string tableIdForId = String.Empty;

				foreach (DataRow fi in qpFieldsQuery.Tables[0].Rows)
				{
					string tableId = DatabaseConnection.SqlScalar("SELECT TABLENAME FROM QB_ALL_TABLES WHERE ID=" + fi["TableID"].ToString());

					if (tableIdForId.IndexOf(fi["TableID"].ToString() + "|") < 0)
					{
						switch (fi["TableID"].ToString())
						{
							case "1":
								fieldsString.AppendFormat("'AID' AS RMAID,{0}.ID AS AID,", tableId);
								grouping += String.Format("{0}.ID,", tableId);
								break;
							case "2":
								fieldsString.AppendFormat("'CID' AS RMCID,{0}.ID AS CID,", tableId);
								grouping += String.Format("{0}.ID,", tableId);
								break;
							case "78":
								fieldsString.AppendFormat("'LID' AS RMLID,{0}.ID AS LID,", tableId);
								grouping += String.Format("{0}.ID,", tableId);
								break;
						}
						tableIdForId += fi["TableID"].ToString() + "|";
					}

					if (fi[2] == DBNull.Value)
						switch (Convert.ToInt32(fi[4]))
						{
							case 7:
								fieldsString.AppendFormat("'{2}' AS RM{1},SUM({0}.{1}) AS {1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								orderAndGroup = true;
								break;
							case 13:
								fieldsString.AppendFormat("'{2}' AS RMNR${1},(CONVERT(VARCHAR(50),{0}.{1})+'|{3}') AS NR${1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString(), fi["ID"].ToString());
								grouping += String.Format("{0}.{1},", tableId, fi[1].ToString());
								break;
							case 6:
								fieldsString.AppendFormat("'{2}' AS RM{1},CAST({0}.{1} AS VARCHAR(8000)) AS {1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								grouping += String.Format("CAST({0}.{1} AS VARCHAR(8000)),", tableId, fi[1].ToString());
								break;
							case 3:
							case 8:
								fieldsString.AppendFormat("'{2}' AS RMDA${1},{0}.{1} AS DA${1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								grouping += String.Format("{0}.{1},", tableId, fi[1].ToString());
								break;
							case 2: // Company categories
								fieldsString.AppendFormat("'{2}' AS RMCA${1},{0}.{1} AS CA${1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								grouping += String.Format("{0}.{1},", tableId, fi[1].ToString());
								break;
							case 14: // Attivit todo
								fieldsString.AppendFormat("'{2}' AS RMAT${1},{0}.{1} AS AT${1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								grouping += String.Format("{0}.{1},", tableId, fi[1].ToString());
								break;
							default:
								fieldsString.AppendFormat("'{2}' AS RM{1},{0}.{1},", tableId, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString());
								grouping += String.Format("{0}.{1},", tableId, fi[1].ToString());
								break;
						}
					else
					{
						string SqlJoinField = "SELECT QB_ALL_TABLES.TABLENAME AS FIRSTTABLENAME, QB_ALL_TABLES_1.TABLENAME AS SECONDTABLENAME, QB_JOIN.FIRSTFIELD, QB_JOIN.SECONDFIELD, QB_JOIN.TYPE, QB_JOIN.ASTABLE " +
							"FROM   QB_JOIN INNER JOIN " +
							"QB_ALL_TABLES ON QB_JOIN.FIRSTTABLEID = QB_ALL_TABLES.ID INNER JOIN " +
							"QB_ALL_TABLES QB_ALL_TABLES_1 ON QB_JOIN.SECONDTABLEID = QB_ALL_TABLES_1.ID " +
							"WHERE QB_JOIN.ID=" + fi["JoinID"].ToString();

						DataSet drjoin = DatabaseConnection.CreateDataset(SqlJoinField);
						DataRow jd = drjoin.Tables[0].Rows[0];

						string tempTableName = (tblstring.IndexOf(jd["FirstTableName"].ToString()) != -1) ? jd["FirstTableName"].ToString() + tblindex++.ToString() : jd["FirstTableName"].ToString();
						tblstring += jd["FirstTableName"] + "|";

						switch ((byte) jd["type"])
						{
							case 0:
								TablesString.AppendFormat(" LEFT OUTER JOIN {0} AS {4} ON {1}.{2} = {4}.{3}", jd["FirstTableName"], jd["SecondTableName"], jd["SecondField"], jd["FirstField"], tempTableName);
								break;
							case 1:
								TablesString.AppendFormat(" LEFT OUTER JOIN {0} AS {4} ON {1}.{2} LIKE '%|'+CONVERT(VARCHAR(10),{4}.{3})+'|%'", jd["FirstTableName"], jd["SecondTableName"], jd["SecondField"], jd["FirstField"], tempTableName);

								break;
							case 2:
								UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
								TablesString.AppendFormat(" LEFT OUTER JOIN {0} as {4} ON {1}.{2} = {4}.{3} AND {4}.LANG='{5}'", jd["FirstTableName"], jd["SecondTableName"], jd["SecondField"], jd["FirstField"], tempTableName,UC.Culture.Substring(0,2));
								break;
						}


						if (fi[3].ToString().IndexOf(",") > 0)
						{
							string[] pf = fi[3].ToString().Split(',');
							if(tempTableName.ToLower().IndexOf("lead")>=0)
								fieldsString.AppendFormat("'{2}' AS RM{1},(ISNULL({0}.{3},'')+' '+ISNULL({0}.{4},'')) AS {1},", tempTableName, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString(), pf[0], pf[1]);
							else
								fieldsString.AppendFormat("'{2}' AS RM{1},(ISNULL({0}.{3},'')) AS {1},", tempTableName, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString(), pf[0], pf[1]);
							grouping += String.Format("{0}.{1},{0}.{2},", tempTableName, pf[0], pf[1]);
						}
						else
						{
							fieldsString.AppendFormat("'{2}' AS RM{1},{0}.{3} AS {1},", tempTableName, fi[1].ToString(), (fi["ColumnName"].ToString().Length > 0) ? fi["ColumnName"].ToString() : fi[0].ToString(), fi[3].ToString());
							grouping += String.Format("{0}.{1},", tempTableName, fi[3].ToString());
						}
					}
				}

				if (fieldsString.Length > 0) fieldsString.Remove(fieldsString.Length - 1, 1);





				foreach (DataRow d in QBTables.Tables[0].Rows)
				{
					int idfield = -1;
					object vvv = DatabaseConnection.SqlScalartoObj("SELECT IDFIELD FROM QB_CUSTOMERQUERYPARAMFIELDS WHERE IDQUERY=" + idQuery + " AND IDTABLE=" + d[0].ToString());
					if (vvv != null)
						idfield = (int) vvv;

					if (idfield > 0)
					{
						string qfp = "SELECT QB_ALL_FIELDS.RMVALUE,QB_ALL_FIELDS.FIELD,QB_ALL_FIELDS.FIELDTYPE,QB_ALL_FIELDS.JOINID,QB_ALL_FIELDS.PARENTFIELD FROM QB_CUSTOMERQUERYPARAMFIELDS " +
							"INNER JOIN QB_ALL_FIELDS ON QB_CUSTOMERQUERYPARAMFIELDS.IDFIELD=QB_ALL_FIELDS.ID " +
							"WHERE QB_CUSTOMERQUERYPARAMFIELDS.IDQUERY=" + idQuery + " AND QB_CUSTOMERQUERYPARAMFIELDS.IDTABLE=" + d[0].ToString();

						DataSet qpFieldsQueryP = DatabaseConnection.CreateDataset(qfp);

						foreach (DataRow f in qpFieldsQueryP.Tables[0].Rows)
						{
							foreach (DictionaryEntry myDE in htParams)
							{
								if (myDE.Key.ToString().ToLower() == f[1].ToString().ToLower())
								{
									string table0 = d[1].ToString();
									string field1 = f[1].ToString();
									string field2 = String.Empty;
									if (f["JoinID"].ToString().Length > 0)
									{
										string SqlJoinFieldG = "SELECT QB_ALL_TABLES.TABLENAME AS FIRSTTABLENAME, QB_ALL_TABLES_1.TABLENAME AS SECONDTABLENAME, QB_JOIN.FIRSTFIELD, QB_JOIN.SECONDFIELD " +
											"FROM   QB_JOIN INNER JOIN " +
											"QB_ALL_TABLES ON QB_JOIN.FIRSTTABLEID = QB_ALL_TABLES.ID INNER JOIN " +
											"QB_ALL_TABLES QB_ALL_TABLES_1 ON QB_JOIN.SECONDTABLEID = QB_ALL_TABLES_1.ID " +
											"WHERE QB_JOIN.ID=" + f["JoinID"].ToString();

										DataTable drjoin = DatabaseConnection.CreateDataset(SqlJoinFieldG).Tables[0];
										table0 = drjoin.Rows[0][0].ToString();
										field1 = f["ParentField"].ToString();
										field2 = drjoin.Rows[0]["SecondField"].ToString();
									}

									if (myDE.Value.ToString().Length > 0)
									{
										string andOr = (fromId) ? "AND" : "OR";

										switch (f[2].ToString())
										{
											case "0": //textbox (like)
												if (myDE.Value.ToString().IndexOf("|") > 0)
												{
													string[] tempQuery = myDE.Value.ToString().Split('|');

													string query = String.Empty;
													foreach (string t in tempQuery)
													{
														if (field1.IndexOf(",") > 0)
														{
															string[] morefields = field1.Split(',');
															foreach (string mf in morefields)
															{
                                                                query += String.Format("{0}.{1} {3}LIKE '{2}%' OR ", table0, mf, ((t.StartsWith("-")) ? DatabaseConnection.FilterInjection(t.Substring(1)) : DatabaseConnection.FilterInjection(t)), ((t.StartsWith("-")) ? "NOT " : string.Empty));
															}
														}
														else
                                                            query += String.Format("{0}.{1} {3}LIKE '{2}%' OR ", table0, field1, ((t.StartsWith("-")) ? DatabaseConnection.FilterInjection(t.Substring(1)) : DatabaseConnection.FilterInjection(t)), ((t.StartsWith("-")) ? "NOT " : string.Empty));
													}
													ParamsString.AppendFormat(" {1} ({0})", query.Substring(0, query.Length - 3), andOr);

												}
												else
												{
													string query = String.Empty;

														if (field1.IndexOf(",") > 0)
														{
															string[] morefields = field1.Split(',');
															foreach (string mf in morefields)
															{
                                                                query += String.Format("{0}.{1} {3}LIKE '{2}%' OR ", table0, mf, ((myDE.Value.ToString().StartsWith("-")) ? DatabaseConnection.FilterInjection(myDE.Value.ToString().Substring(1)) : DatabaseConnection.FilterInjection(myDE.Value.ToString())), ((myDE.Value.ToString().StartsWith("-")) ? "NOT " : string.Empty));
															}
														}
														else
                                                            query += String.Format("{0}.{1} {3}LIKE '{2}%' OR ", table0, field1, ((myDE.Value.ToString().StartsWith("-")) ? DatabaseConnection.FilterInjection(myDE.Value.ToString().Substring(1)) : DatabaseConnection.FilterInjection(myDE.Value.ToString())), ((myDE.Value.ToString().StartsWith("-")) ? "NOT " : string.Empty));

													ParamsString.AppendFormat(" {1} ({0})", query.Substring(0, query.Length - 3), andOr);
												}

												break;
											case "1": //dropdown con =
											case "13":

												if (myDE.Value.ToString().IndexOf("|") > 0)
												{
													string[] tempQuery = myDE.Value.ToString().Split('|');
													string query = String.Empty;
													foreach (string t in tempQuery)
													{
														query += String.Format("{0}.{1} = '{2}' OR ",  d[1].ToString(), (field2.Length>0)?field2:f[1].ToString(), t);
													}
													ParamsString.AppendFormat(" {1} ({0})", query.Substring(0, query.Length - 3), andOr);
												}
												else
												{
													if (f["ParentField"] == DBNull.Value)
													{
														ParamsString.AppendFormat(" {3} {0}.{1} = '{2}'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
													}
													else
													{
														ParamsString.AppendFormat(" {3} {0}.{1} = '{2}'", d[1].ToString(), field2, myDE.Value, andOr);
													}
												}
												break;
											case "2": //dropdown con like e pipe
												if (myDE.Value.ToString().IndexOf("|") > 0)
												{
													string[] tempQuery = myDE.Value.ToString().Split('|');

													string query = String.Empty;
													foreach (string t in tempQuery)
													{
														query += String.Format("{0}.{1} LIKE '%|{2}|%' OR ", table0, field1, t);
													}
													ParamsString.AppendFormat(" {1} ({0})", query.Substring(0, query.Length - 3), andOr);
												}
												else
													ParamsString.AppendFormat(" {3} {0}.{1} LIKE '%|{2}|%'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;
											case "3": //data secca
												ParamsString.AppendFormat(" {3} Convert(varchar(10),{0}.{1},112)='{2}'", d[1].ToString(), f[1].ToString(), ucCurrent.LTZ.ToUniversalTime(Convert.ToDateTime(myDE.Value, ucCurrent.myDTFI)).ToString(@"yyyyMMdd 23:59:59"), andOr);
												break;
											case "4": //checkbox
												if (myDE.Value != null)
												{
													ParamsString.AppendFormat(" {2} {0}.{1} = 1", d[1].ToString(), f[1].ToString(), andOr);
												}
												else
												{
													ParamsString.AppendFormat(" {2} {0}.{1} = 0", d[1].ToString(), f[1].ToString(), andOr);
												}
												break;
											case "5": // risearch per propietario
												ParamsString.AppendFormat(" {3} {0}.{1} = '{2}'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;
											case "8": //data between
												string[] valori = myDE.Value.ToString().Split('|');
												ParamsString.AppendFormat(" {4} {0}.{1} between '{2}' and '{3}'", d[1].ToString(), f[1].ToString(), ucCurrent.LTZ.ToUniversalTime(Convert.ToDateTime(valori[0], ucCurrent.myDTFI)).ToString(@"yyyyMMdd"), Convert.ToDateTime(valori[1], ucCurrent.myDTFI).ToString(@"yyyyMMdd 23:59:59"), andOr);
												break;
											case "9": // risearch per aziende
												ParamsString.AppendFormat(" {3} {0}.{1} = '{2}'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;
											case "10": // risearch per contatto
												ParamsString.AppendFormat(" {3} {0}.{1} = '{2}'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;
											case "11": // risearch per opportunit
												ParamsString.AppendFormat(" {3} {0}.{1} LIKE '%|{2}|%'", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;
											case "12": // risearch per opportunit
												valori = myDE.Value.ToString().Split('|');
												if (valori[1] == "0")
												{
													ParamsString.AppendFormat(" {3} {0}.{1} = {2}", d[1].ToString(), f[1].ToString(), valori[0], andOr);
												}
												else
												{
													ParamsString.AppendFormat(" {3} {0}.{1} > {2}", d[1].ToString(), f[1].ToString(), valori[0], andOr);
												}
												break;
											case "14": // Radio Button
												ParamsString.AppendFormat(" {3} {0}.{1} = {2}", d[1].ToString(), f[1].ToString(), myDE.Value, andOr);
												break;

										}
									}
								}
							}
						}
					}
					else
					{
						if (idfield == 0)
						{
							foreach (DictionaryEntry myDE in htParams)
							{
								ParamsString.AppendFormat(" AND {0}.ID = {1}", d[1].ToString(), myDE.Value);
							}
						}
					}
				}

				foreach (DataRow d in QBTables.Tables[0].Rows)
				{
					if (d["FixedQuery"].ToString().Length > 0 && TablesString.ToString().IndexOf(d["FixedQuery"].ToString())<0)
					{
						ParamsString.AppendFormat(" AND ({0})", d["FixedQuery"].ToString());
					}
				}



				finalQuery.AppendFormat("SELECT {0} ", fieldsString.ToString());
				finalQuery.AppendFormat("FROM {0} ", TablesString.ToString());
				finalQuery.Append("WHERE ");
				if (fromId)
					if((finalQuery.ToString().EndsWith("WHERE ") ||
						finalQuery.ToString().EndsWith("WHERE"))
						)
						if((ParamsString.ToString().StartsWith(" AND") ||
							ParamsString.ToString().StartsWith("AND")))
							finalQuery.AppendFormat("{0}", ParamsString.ToString().Substring(4, ParamsString.Length - 4));
						else
							finalQuery.AppendFormat("{0}", ParamsString.ToString());
					else
						if((ParamsString.ToString().StartsWith(" AND") ||
						ParamsString.ToString().StartsWith("AND")) || StaticFunctions.IsBlank(ParamsString.ToString()))
							finalQuery.AppendFormat("{0}", ParamsString.ToString());
						else
							finalQuery.AppendFormat(" AND {0}", ParamsString.ToString());
				else
					if((finalQuery.ToString().EndsWith("WHERE ") ||
					finalQuery.ToString().EndsWith("WHERE"))
					)
						finalQuery.AppendFormat("{0}", ParamsString.ToString().Substring(3, ParamsString.Length - 3));
					else
						finalQuery.AppendFormat("AND ({0})", ParamsString.ToString().Substring(3, ParamsString.Length - 3));



				string groupby = DatabaseConnection.SqlScalar("SELECT GROUPBY FROM QB_CUSTOMERQUERY WHERE ID=" + idQuery);
				if (groupby.Length > 0 || orderAndGroup)
				{
					string[] t;
					if(groupby.Length > 0)
						t = groupby.Split('|');
					else
					{
						groupby = DatabaseConnection.SqlScalar("SELECT TOP 1 CAST(IDTABLE AS VARCHAR)+'|'+CAST(IDFIELD AS VARCHAR) AS GROUPBY FROM QB_CUSTOMERQUERYFIELDS WHERE IDQUERY="+idQuery+" ORDER BY OPTIONS");
						t = groupby.Split('|');
					}



					string qbGroup = "SELECT QB_ALL_TABLES.TABLENAME,QB_ALL_FIELDS.FIELD AS GROUPBY,QB_ALL_FIELDS.JOINID,QB_ALL_FIELDS.PARENTFIELD FROM QB_ALL_TABLES " +
						"JOIN QB_ALL_FIELDS ON QB_ALL_TABLES.ID=QB_ALL_FIELDS.TABLEID " +
						"WHERE QB_ALL_TABLES.ID=" + t[0] + " AND QB_ALL_FIELDS.ID=" + t[1] + ";";
					DataTable dtGroup = DatabaseConnection.CreateDataset(qbGroup).Tables[0];
					string groupFor = String.Empty;
					if (dtGroup.Rows[0]["JoinID"].ToString().Length > 0)
					{
						string SqlJoinFieldG = "SELECT QB_ALL_TABLES.TABLENAME AS FIRSTTABLENAME, QB_ALL_TABLES_1.TABLENAME AS SECONDTABLENAME, QB_JOIN.FIRSTFIELD, QB_JOIN.SECONDFIELD " +
							"FROM   QB_JOIN INNER JOIN " +
							"QB_ALL_TABLES ON QB_JOIN.FIRSTTABLEID = QB_ALL_TABLES.ID INNER JOIN " +
							"QB_ALL_TABLES QB_ALL_TABLES_1 ON QB_JOIN.SECONDTABLEID = QB_ALL_TABLES_1.ID " +
							"WHERE QB_JOIN.ID=" + dtGroup.Rows[0]["JoinID"].ToString();

						DataTable drjoin = DatabaseConnection.CreateDataset(SqlJoinFieldG).Tables[0];
						if (dtGroup.Rows[0]["ParentField"].ToString().IndexOf(",") > 0)
						{
							string[] compositegroup = dtGroup.Rows[0]["ParentField"].ToString().Split(',');

							foreach (string cg in compositegroup)
							{
								groupFor += drjoin.Rows[0][0].ToString() + "." + cg + ",";
							}
							groupFor = groupFor.Substring(0, groupFor.Length - 1);
						}
						else
							groupFor = drjoin.Rows[0][0].ToString() + "." + dtGroup.Rows[0]["ParentField"].ToString();
					}
					else
					{
						if (dtGroup.Rows[0][1].ToString().IndexOf(",") > 0)
						{
							string[] compositegroup = dtGroup.Rows[0][1].ToString().Split(',');

							foreach (string cg in compositegroup)
							{
								groupFor += dtGroup.Rows[0][0].ToString() + "." + cg + ",";
							}
							groupFor = groupFor.Substring(0, groupFor.Length - 1);

						}
						else
							groupFor = dtGroup.Rows[0][0].ToString() + "." + dtGroup.Rows[0][1].ToString();
					}

					if (grouping.IndexOf(groupFor) > 0)
					{
						grouping.Remove(grouping.IndexOf(groupFor), groupFor.Length + 1);
					}

					if (orderAndGroup)
					{
						if (grouping.Length > 0)
							finalQuery.AppendFormat(" GROUP BY {0},{1}", groupFor, grouping.Substring(0, grouping.Length - 1));
						else
							finalQuery.AppendFormat(" GROUP BY {0}", groupFor);
					}
					else
					{
						finalQuery.AppendFormat(" ORDER BY {0}", groupFor);
					}
				}


				if (orderby.Length > 0 && finalQuery.ToString().IndexOf("GROUP BY") < 1)
					finalQuery.AppendFormat(" ORDER BY {0}", orderby);

				HttpContext.Current.Trace.Warn("finalquery",finalQuery.ToString());

				DataSet finalDs = DatabaseConnection.CreateDataset(finalQuery.ToString());
				G.FixDateTimeZone(finalDs,ucCurrent.LTZ);

				DataTable labels = new DataTable();
				DataTable freeLabels = new DataTable();

				if (finalDs.Tables[0].Rows.Count > 0)
				{
					string fieldString = String.Empty;
					int fieldIndex = 0;
					DataRow drlabels = finalDs.Tables[0].Rows[0];
					foreach (DataColumn cc in finalDs.Tables[0].Columns)
					{
						if (cc.ColumnName.ToLower().StartsWith("rm"))
						{
							DataColumn dcDynColumn = new DataColumn();
							string columnName = Root.rm.GetString("QBTxt" + drlabels[cc.ColumnName].ToString());
							if (columnName != null)
							{
								if (fieldString.IndexOf(columnName + "|") != -1)
									dcDynColumn.ColumnName = columnName + "_" + fieldIndex++;
								else
									dcDynColumn.ColumnName = columnName;

								fieldString += columnName + "|";
							}
							else
							{
								dcDynColumn.ColumnName = drlabels[cc.ColumnName].ToString();
								fieldString += drlabels[cc.ColumnName].ToString() + "|";
							}
							dcDynColumn.DataType = Type.GetType("System.String");
							labels.Columns.Add(dcDynColumn);
						}
					}

					foreach (DataRow dd in finalDs.Tables[0].Rows)
					{
						DataRow d = labels.NewRow();
						foreach (DataColumn cc in finalDs.Tables[0].Columns)
						{
							if (!cc.ColumnName.ToLower().StartsWith("rm"))
							{
								switch (cc.ColumnName.Substring(0, 3))
								{
									case "AID":
									case "CID":
									case "LID":
										d[dd["rm" + cc.ColumnName].ToString()] = dd[cc.ColumnName].ToString();
										break;
									case "DA$":
										try
										{
											d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = Convert.ToDateTime(dd[cc.ColumnName].ToString()).ToString();
										}
										catch
										{
											try
											{
												d[dd["rm" + cc.ColumnName].ToString()] = Convert.ToDateTime(dd[cc.ColumnName].ToString()).ToString();
											}
											catch
											{
												d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = DBNull.Value;
											}
										}
										break;
									case "NR$":
										try
										{
											if (dd[cc.ColumnName].ToString().Length > 0)
											{
												string[] Nrm = dd[cc.ColumnName].ToString().Split('|');
												string Nrmvalue = DatabaseConnection.SqlScalar("SELECT RMVALUE FROM QB_FIXEDDROPDOWNPARAMS WHERE IDRIF=" + Nrm[1] + " AND DROPVALUE=" + Nrm[0]);
												d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = Root.rm.GetString("QBTxt" + Nrmvalue);
											}
											else
												d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = String.Empty;
										}
										catch
										{
											d[dd["rm" + cc.ColumnName].ToString()] = dd[cc.ColumnName].ToString();
										}
										break;
									case "CA$":
										try
										{
											string[] Cat = dd[cc.ColumnName].ToString().Split('|');
											string queryCat = String.Empty;
											foreach (string c in Cat)
											{
												if (c.Length > 0)
													queryCat += " ID=" + c + " OR ";
											}
											DataTable catdt = DatabaseConnection.CreateDataset("SELECT DESCRIPTION FROM CRM_CONTACTCATEGORIES WHERE " + queryCat.Substring(0, queryCat.Length - 3)).Tables[0];
											string catList = String.Empty;
											foreach (DataRow dr in catdt.Rows)
											{
												catList += dr[0].ToString() + ",";
											}

											d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = catList;
										}

										catch
										{
										}


										break;
									case "AT$":
										string todo=string.Empty;

										switch(dd[cc.ColumnName].ToString())
										{
											case "1":
												todo=Root.rm.GetString("Acttxt71");
												break;
											case "0":
												todo=Root.rm.GetString("Acttxt72");
												break;
											case "2":
												todo=Root.rm.GetString("Acttxt103");
												break;
										}

										d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = todo;
										break;
									default:
										try
										{
											d[Root.rm.GetString("QBTxt" + dd["rm" + cc.ColumnName].ToString())] = dd[cc.ColumnName].ToString();
										}
										catch
										{

											d[dd["rm" + cc.ColumnName].ToString()] = dd[cc.ColumnName].ToString();

										}

										break;
								}
							}
						}
						labels.Rows.Add(d);
					}

					string fieldsFlag = String.Empty;
					bool toDelete = true;
					foreach (DataRow d in QBTables.Tables[0].Rows)
					{
						StringBuilder FreeFields = new StringBuilder();
						string wichid = String.Empty;
						string qff = "SELECT ADDEDFIELDS.NAME,ADDEDFIELDS.ID " +
							"FROM QB_CUSTOMERQUERYFIELDS " +
							"INNER JOIN ADDEDFIELDS ON QB_CUSTOMERQUERYFIELDS.IDFIELD = ADDEDFIELDS.ID " +
							"WHERE QB_CUSTOMERQUERYFIELDS.IDQUERY=" + idQuery + " AND IDTABLE=-" + d[0].ToString();
						DataSet qpFieldsQueryF = DatabaseConnection.CreateDataset(qff);
						if (qpFieldsQueryF.Tables[0].Rows.Count > 0)
						{
							DataColumn[] newcolumns = new DataColumn[qpFieldsQueryF.Tables[0].Rows.Count + 1];
							int i = 0;
							foreach (DataRow fi in qpFieldsQueryF.Tables[0].Rows)
							{
								newcolumns[i++] = new DataColumn(fi[0].ToString(), Type.GetType("System.String"));
							}

							switch (d[0].ToString())
							{
								case "1":
									wichid = "AID";
									break;
								case "2":
									wichid = "CID";
									break;
								case "78":
									wichid = "LID";
									break;
							}
							fieldsFlag += wichid + "|";
							newcolumns[i++] = new DataColumn(wichid, Type.GetType("System.String"));
							freeLabels.Columns.AddRange(newcolumns);

							DataTable freeparams = DatabaseConnection.CreateDataset("SELECT IDFIELD FROM QB_CUSTOMERQUERYPARAMFIELDS WHERE IDQUERY=" + idQuery + " AND IDTABLE=-" + d[0].ToString()).Tables[0];

							if (freeparams.Rows.Count > 0)
							{
								toDelete = false;
								FreeFields.Append("SELECT SECONDA.* FROM (");
								FreeFields.AppendFormat("SELECT ADDEDFIELDS.NAME, ADDEDFIELDS_CROSS.FIELDVAL, ADDEDFIELDS_CROSS.ID AS {0} ", wichid);
								FreeFields.Append("FROM ADDEDFIELDS ");
								FreeFields.Append("INNER JOIN ADDEDFIELDS_CROSS ON ADDEDFIELDS.ID = ADDEDFIELDS_CROSS.IDRIF ");
								FreeFields.Append("INNER JOIN QB_CUSTOMERQUERYFIELDS ON ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");
								FreeFields.AppendFormat("WHERE (ADDEDFIELDS.TABLENAME = {0}) AND QB_CUSTOMERQUERYFIELDS.IDQUERY={1} AND ", d[0].ToString(), idQuery);
								FreeFields.Append("ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");

								foreach (DictionaryEntry myDE in htParams)
								{

									if (myDE.Key.ToString().Substring(0, 1) == "-")
									{
										FreeFields.AppendFormat("AND ADDEDFIELDS.NAME='{0}' AND ADDEDFIELDS_CROSS.FIELDVAL LIKE '%{1}%' ", myDE.Key.ToString().Substring(1, myDE.Key.ToString().Length - 1), myDE.Value.ToString());
									}
								}

								FreeFields.Append(") AS PRIMA ");
								FreeFields.Append("LEFT OUTER JOIN (");

								FreeFields.AppendFormat("SELECT ADDEDFIELDS.NAME, ADDEDFIELDS_CROSS.FIELDVAL, ADDEDFIELDS_CROSS.ID AS {0} ", wichid);
								FreeFields.Append("FROM ADDEDFIELDS ");
								FreeFields.Append("INNER JOIN ADDEDFIELDS_CROSS ON ADDEDFIELDS.ID = ADDEDFIELDS_CROSS.IDRIF ");
								FreeFields.Append("INNER JOIN QB_CUSTOMERQUERYFIELDS ON ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");
								FreeFields.AppendFormat("WHERE (ADDEDFIELDS.TABLENAME = {0}) AND QB_CUSTOMERQUERYFIELDS.IDQUERY={1} AND ", d[0].ToString(), idQuery);
								FreeFields.Append("ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");

								FreeFields.AppendFormat(") as seconda on prima.{0} = seconda.{0} order by seconda.AID,seconda.name", wichid);

							}
							else
							{
								FreeFields.AppendFormat("SELECT ADDEDFIELDS.NAME, ADDEDFIELDS_CROSS.FIELDVAL, ADDEDFIELDS_CROSS.ID AS {0} ", wichid);
								FreeFields.Append("FROM ADDEDFIELDS ");
								FreeFields.Append("INNER JOIN ADDEDFIELDS_CROSS ON ADDEDFIELDS.ID = ADDEDFIELDS_CROSS.IDRIF ");
								FreeFields.Append("INNER JOIN QB_CUSTOMERQUERYFIELDS ON ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");
								FreeFields.AppendFormat("WHERE (ADDEDFIELDS.TABLENAME = {0}) AND QB_CUSTOMERQUERYFIELDS.IDQUERY={1} AND ", d[0].ToString(), idQuery);
								FreeFields.Append("ADDEDFIELDS.ID=QB_CUSTOMERQUERYFIELDS.IDFIELD ");
								FreeFields.Append("ORDER BY ADDEDFIELDS_CROSS.ID ");
							}


							DataTable dtFree = DatabaseConnection.CreateDataset(FreeFields.ToString()).Tables[0];
							foreach (DataRow crossid in dtFree.Rows)
							{
								try
								{
									DataRow[] tempdr = freeLabels.Select("AID=" + crossid[wichid]);
									DataRow drfiledval;
									if (tempdr.Length > 0)
										drfiledval = tempdr[0];
									else
										drfiledval = freeLabels.NewRow();
									drfiledval[crossid["Name"].ToString()] = crossid["fieldval"].ToString();
									drfiledval[wichid] = crossid[wichid].ToString();
									if (!(tempdr.Length > 0))
										freeLabels.Rows.Add(drfiledval);
								}
								catch
								{
									try
									{
										DataRow drfiledval;
										drfiledval = freeLabels.NewRow();
										drfiledval[crossid["Name"].ToString()] = crossid["fieldval"].ToString();
										drfiledval[wichid] = crossid[wichid].ToString();
										freeLabels.Rows.Add(drfiledval);
									}catch{}
								}

							}
						}
					}
					if (freeLabels.Rows.Count > 0 && labels.Rows.Count > 0)
					{
						string[] wich = fieldsFlag.Split('|');
						foreach (string field in wich)
							if (field.Length > 0) DataManipulation.JoinTableByID(labels, freeLabels, field, field, toDelete);
					}
				}

				DataColumn[] keyColumns = new DataColumn[labels.Columns.Count];
				for(int dc=0;dc<labels.Columns.Count;dc++)
				{
					keyColumns[dc] = labels.Columns[dc];
				}
				DataManipulation.RemoveDuplicates(labels,keyColumns);

				for ( int i = 0; i < labels.Columns.Count; ++i )
				{
					if ( labels.Columns[i].ColumnName == "AID" )
						labels.Columns.RemoveAt(i--);
				}

				return labels;
			}

		}
	}
}
