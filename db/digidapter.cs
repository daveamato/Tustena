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
using System.Data.SqlClient;
using System.Text;

namespace Digita.Tustena.Database
{
	public class DigiDapter : IDisposable
	{
		private object retObj = null;
		private DataRow newRow = null;
		private Identities identityMode = 0;
		private bool insert = false;
		private bool update = false;
		private string where = "ID=-1";
		private SqlConnection cn;
		private bool needCloseConnection = false;
		private Queue parameters = new Queue();
		private DataRow originalRow = null;
		private bool hasRows = false;

		public bool HasRows
		{
			get { return hasRows; }
		}


		private struct FieldsAndValues
		{
			public string key;
			public object val;
			public char mode;

			public FieldsAndValues(string key, object val, char mode)
			{
				this.key = key;
				this.val = val;
				this.mode = mode;
			}
		}

		public DigiDapter()
		{
			cn = DatabaseConnection.GetConnection;

		}

		public DigiDapter(SqlConnection connection)
		{
			cn = connection;
		}

		public DigiDapter(string directQuery)
		{
			cn = DatabaseConnection.GetConnection;
			SqlCommand myCmd = new SqlCommand(directQuery, (DatabaseConnection.IsTransaction) ? cn : DatabaseConnection.GetNewConnection);
			if (myCmd.Connection.State != ConnectionState.Open)
				myCmd.Connection.Open();
			if (DatabaseConnection.IsTransaction)
				myCmd.Transaction = DatabaseConnection.CurrentTransaction;
			needCloseConnection = true;
			FillObjectColumns(myCmd);
		}

		private void FillObjectColumns(SqlCommand myCmd)
		{
			SqlDataReader externalReader = myCmd.ExecuteReader(CommandBehavior.SingleRow);
			originalRow = GetDataRow(externalReader);
			externalReader.Close();
		}

		public void Add(string key, object val)
		{
			parameters.Enqueue(new FieldsAndValues(key, val, 'A'));
		}

		public void AddOrNull(string key, object val)
		{
			if (val is string && ((string) val).Length == 0)
				Add(key, DBNull.Value);
			else
				Add(key, val);
		}

		public void Add(string key, object val, bool unicode)
		{
			parameters.Enqueue(new FieldsAndValues(key, val, 'A'));
		}

		public void Add(string key, object val, char mode)
		{
			parameters.Enqueue(new FieldsAndValues(key, val, mode));
		}

		public void Add(string key, object val, char mode, bool unicode)
		{
			parameters.Enqueue(new FieldsAndValues(key, val, mode));
		}

		public bool RecordInserted
		{
			get { return insert; }
		}

		public void InsertOnly()
		{
			insert = true;
		}

		public void UpdateOnly()
		{
			update = true;
		}


		public bool recordUpdated
		{
			get { return update; }
		}

		public Identities IdentityMode
		{
			get { return identityMode; }
		}

		public object RetObj
		{
			get { return retObj; }
		}

		public string Where
		{
			get { return where; }
			set { where = value; }
		}


		public int ExternalReaderRowId
		{
			get
			{
				if (originalRow[0] is int)
					return (int) originalRow[0];
				else
					return -1;
			}
		}

		public SqlConnection Connection
		{
			get { return cn; }
			set { cn = value; }
		}

		public DataRow GetOriginalRow
		{
			get { return originalRow; }
		}

		public DataRow GetNewRow
		{
			get { return newRow; }
		}

		public DataRow ExternalReader
		{
			get { return originalRow; }
		}


		private DataRow GetDataRow(SqlDataReader rd)
		{
				DataTable St = rd.GetSchemaTable();
				DataTable dt = new DataTable();
				DataColumn dc;

				for (int i = 0; i < St.Rows.Count; i ++)
				{
					dc = new DataColumn(St.Rows[i]["ColumnName"].ToString(),Type.GetType(St.Rows[i]["DataType"].ToString()));
					dt.Columns.Add(dc);
				}
				DataRow NewRow = dt.NewRow();
			if (rd.Read())
			{
				hasRows=true;
				object[] values = new object[rd.FieldCount];
				rd.GetValues(values);
				NewRow.ItemArray = values;
			}
			return NewRow;
		}

		public object Execute(string table)
		{
			insert = true;
			return Execute(table, where, parameters, identityMode);
		}

		public object Execute(string table, string where)
		{
			return Execute(table, where, parameters, identityMode);
		}

		public object Execute(string table, Identities identityMode)
		{
			insert = true;
			return Execute(table, where, parameters, identityMode);
		}

		public object Execute(string table, string where, Identities identityMode)
		{
			return Execute(table, where, parameters, identityMode);
		}

		public object Execute(string table, string where, Queue parameters, Identities identityMode)
		{
			if (parameters.Count == 0)
				return null;
			SqlCommand cmInsert = new SqlCommand(String.Format("SELECT * FROM {0} WHERE {1}", table, where), cn);

			if (DatabaseConnection.IsTransaction)
			{
				cmInsert.Transaction = DatabaseConnection.CurrentTransaction;
			}
			object ret = null;
			if (cn.State != ConnectionState.Open)
			{
				cn.Open();
				needCloseConnection = true;
			}

			if (originalRow == null && !insert)
				FillObjectColumns(cmInsert);
			if (insert || !HasRows)
			{
				if (update)
					throw new Exception("Digidapter in Update only mode, can\'t insert");
				StringBuilder sb = new StringBuilder();
				StringBuilder sb2 = new StringBuilder();
				sb.AppendFormat("INSERT INTO {0} ", table);
				foreach (FieldsAndValues colname in parameters)
				{
					if (colname.mode != 'U')
					{
						sb2.AppendFormat(",@{0}", colname.key);
						if (colname.val is int || colname.val is decimal || colname.val is double)
						{
							SqlParameter sqlParam = new SqlParameter("@" + colname.key, SqlDbType.Decimal);
							sqlParam.Value = colname.val;
							cmInsert.Parameters.Add(sqlParam);
						}
						else
						{
							cmInsert.Parameters.Add(new SqlParameter("@" + colname.key, colname.val));
						}


					}
				}
				sb.AppendFormat("({1}) VALUES ({0})", sb2.Remove(0, 1), sb2.ToString().Replace("@", ""));
				if (identityMode == Identities.Identity) sb.AppendFormat(";SELECT SCOPE_IDENTITY() AS ID");
				if (identityMode == Identities.Row) sb.AppendFormat(";SELECT * FROM {0} WHERE ID=SCOPE_IDENTITY()", table);
				cmInsert.CommandText = sb.ToString();
				insert = true;
			}
			else
			{
				StringBuilder sb = new StringBuilder();
				StringBuilder sb2 = new StringBuilder();
				sb.AppendFormat("UPDATE {0} SET ", table);
				foreach (FieldsAndValues colname in parameters)
				{
					if (colname.mode != 'I')
					{
						sb2.AppendFormat(",{0}=@{0}", colname.key);
						if (colname.val is int || colname.val is decimal || colname.val is double)
						{
							SqlParameter sqlParam = new SqlParameter("@" + colname.key, SqlDbType.Decimal);
							sqlParam.Value = colname.val;
							cmInsert.Parameters.Add(sqlParam);
						}
						else
						{
							cmInsert.Parameters.Add(new SqlParameter("@" + colname.key, colname.val));
						}
					}
				}
				sb.AppendFormat("{0} WHERE {1}", sb2.Remove(0, 1), where);
				if (identityMode == Identities.Row) sb.AppendFormat(";SELECT * FROM {0} WHERE {1}", table, where);
				cmInsert.CommandText = sb.ToString();
				update = true;
			}
			try
			{
				switch (identityMode)
				{
					case Identities.None:
						cmInsert.ExecuteNonQuery();
						break;
					case Identities.Identity:
						ret = cmInsert.ExecuteScalar();
						break;
					case Identities.Row:
						ret = cmInsert.ExecuteReader(CommandBehavior.SingleRow);
						newRow = this.GetDataRow(((SqlDataReader) ret));
						((SqlDataReader) ret).Close();
						break;
				}

			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message, ex);
			}

			if (needCloseConnection && !DatabaseConnection.IsTransaction)
				cn.Close();

			retObj = ret;
			return ret;
		}

		public enum Identities :byte
		{
			None,
			Identity,
			Row
		}

		~DigiDapter()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // Finalization is now unnecessary
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!m_disposed)
			{
				if (disposing)
				{
					if (needCloseConnection && !DatabaseConnection.IsTransaction)
						if (cn.State == ConnectionState.Open)
							cn.Close();
				}
				retObj = null;
				newRow = null;
				originalRow = null;
				parameters = null;
			}

			m_disposed = true;
		}

		private bool m_disposed = false;
	}
}
