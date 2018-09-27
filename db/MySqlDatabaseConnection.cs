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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Web;
using Digita.Tustena.Base;

namespace Digita.Tustena.Database
{

	public sealed class MySqlDatabaseConnection
	{

		private MySqlDatabaseConnection(){}

		public static MySqlConnection GetConnection
		{
			get
			{
				return new MySqlConnection(ConfigurationSettings.AppSettings["CONNECTION"]);
			}
		}
		static string maxResult = null;
		public static string MaxResult
		{
			get
			{
				if(maxResult==null)
				{
					maxResult = (ConfigSettings.GetAppSetting("MAXRESULT"));
					if(StaticFunctions.IsBlank(maxResult))
						maxResult="100";
				}
				return maxResult;
			}
		}


		public static void DoCommand(string sql)
		{
			MySqlCommand cmd = new MySqlCommand(sql, GetConnection);
			try
			{
				cmd.Connection.Open();
				cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{
				cmd.Connection.Close();
			}
		}

		public static string SqlScalar(string sql)
		{
			string r = String.Empty;
			try
			{
				r = SqlScalartoObj(sql).ToString();
			}
			catch
			{
				r = String.Empty;
			}
			return r;
		}


		public static object SqlScalartoObj(string sql)
		{
			MySqlCommand cmd = new MySqlCommand(sql, GetConnection);

			try
			{
				cmd.Connection.Open();
				return cmd.ExecuteScalar();
			}
			catch
			{
				return "";
			}
			finally
			{
				cmd.Connection.Close();
			}
		}

		public static DataSet CreateDataset(string sqlString, string nome)
		{
			using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, GetConnection))
			{
				DataSet myDataSet = new DataSet();
				mySqlDataAdapter.Fill(myDataSet);
				return myDataSet;
			}
		}

		public static DataSet CreateDataset(string sqlString)
		{
			using (MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(sqlString, GetConnection))
			{
				DataSet myDataSet = new DataSet();
				try
				{
					mySqlDataAdapter.Fill(myDataSet);
				}
				catch (MySqlException ex)
				{
					throw new Exception(sqlString + ex.ToString());
				}
				return myDataSet;
			}
		}



		public static DataSet SecureCreateDataset(string sqlString, MySqlParameter param)
		{
			MySqlParameter[] p;
			p = new MySqlParameter[1];
			p[0] = param;
			return SecureCreateDataset(sqlString, p);
		}

		public static DataSet SecureCreateDataset(string sqlString, MySqlParameter[] sqlParams)
		{
			DataSet Secure = new DataSet();
			MySqlCommand cmd = GetConnection.CreateCommand();
			cmd.CommandText = sqlString;
			MySqlDataAdapter SqlDataAdapter = new MySqlDataAdapter(cmd);

			foreach (MySqlParameter ss in sqlParams)
			{
				cmd.Parameters.Add(ss);
			}
			try
			{
				cmd.Connection.Open();
				SqlDataAdapter.Fill(Secure);
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString());
			}
			finally
			{
				cmd.Connection.Close();
			}
			return Secure;
		}

		public static MySqlDataReader CreateReader(string sqlString)
		{
			MySqlCommand myCmd = new MySqlCommand(sqlString, GetConnection);
			MySqlDataReader dr = null;
			myCmd.Connection.Open();
			dr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
			return dr;
		}

		public static MySqlDataReader CreateReader(string sqlString, MySqlParameter[] sqlParams)
		{
			MySqlCommand myCmd = new MySqlCommand(sqlString, GetConnection);
			MySqlDataReader dr = null;
			myCmd.Connection.Open();
			foreach(MySqlParameter par in sqlParams)
				myCmd.Parameters.Add(par);
			dr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
			return dr;
		}

		public static MySqlCommand GetNewID(MySqlCommandBuilder bldr)
		{
			MySqlCommand cmdInsert = new MySqlCommand(bldr.GetInsertCommand().CommandText, GetConnection);
			cmdInsert.CommandText += ";SELECT SCOPE_IDENTITY() AS ID";
			MySqlParameter[] aParams = new MySqlParameter[bldr.GetInsertCommand().Parameters.Count];
			bldr.GetInsertCommand().Parameters.CopyTo(aParams, 0);
			bldr.GetInsertCommand().Parameters.Clear();

			for (int i = 0; i < aParams.Length; i++)
			{
				cmdInsert.Parameters.Add(aParams[i]);
			}
			return cmdInsert;
		}

		public static void AddToDataTable(ref DataTable t, string sql, MySqlConnection cn)
		{
			MySqlDataAdapter da = new MySqlDataAdapter(sql, cn);
			da.Fill(t);
		}

		public static bool SQLSecure(string s)
		{
			Regex Secure = new Regex("[^A-Za-z0-9_][@]");
			if (Secure.IsMatch(s))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		static Regex FilterInjectionRegex;
		public static string FilterInjection(string s)
		{
			s = s.Replace("'", "''");
			if(FilterInjectionRegex==null)
			FilterInjectionRegex = new Regex(@"%3D|=|%27|%2D|--|%3B|;", RegexOptions.IgnoreCase);
			if (FilterInjectionRegex.IsMatch(s))
			{
				G.SendError("Filterinjection",s);
				return "";
			}
			else
				return s;
		}

	}
}
