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

namespace Digita.Tustena.Database
{
	public  sealed class DataManipulation
	{

		private DataManipulation(){}
		public static DataTable Union(DataTable first, DataTable second)
		{
			DataTable table = new DataTable("Union");

			DataColumn[] newcolumns = new DataColumn[first.Columns.Count];
			for (int i = 0; i < first.Columns.Count; i++)
			{
				newcolumns[i] = new DataColumn(first.Columns[i].ColumnName, first.Columns[i].DataType);
			}


			table.Columns.AddRange(newcolumns);
			table.BeginLoadData();

			foreach (DataRow row in first.Rows)
			{
				table.LoadDataRow(row.ItemArray, true);
			}

			foreach (DataRow row in second.Rows)
			{
				table.LoadDataRow(row.ItemArray, true);
			}
			table.EndLoadData();
			return table;
		}

		public static void JoinTableByID(DataTable first, DataTable second, string firstColumnID, string secondColumnID, bool firstmatch)
		{
			DataColumn[] newcolumns = new DataColumn[second.Columns.Count - 1];
			for (int i = 0; i < second.Columns.Count - 1; i++)
			{
				newcolumns[i] = new DataColumn(second.Columns[i].ColumnName, second.Columns[i].DataType);
			}

			first.Columns.AddRange(newcolumns);

			string datarowindex = String.Empty;
			for (int i = 0; i < first.Rows.Count; i++)
			{
				DataRow f = first.Rows[i];
				DataRow[] sf = second.Select(secondColumnID + "=" + f[firstColumnID].ToString());
				if (sf.Length > 0)
				{
					foreach (DataRow sfdt in sf)
					{
						foreach (DataColumn ccc in sfdt.Table.Columns)
						{
							try
							{
								f[ccc.ColumnName] = sfdt[ccc.ColumnName];
							}
							catch
							{
							}
						}
					}
				}
				else
				{
					if (!firstmatch)
					{
						datarowindex += i + "|";
					}
				}
			}
			if (datarowindex.Length > 0)
			{
				string[] indextodel = datarowindex.Split('|');
				foreach (string s in indextodel)
					if (s.Length > 0) first.Rows[Convert.ToInt32(s)].Delete();
			}
		}

		public static void Join(DataTable first, DataTable second)
		{
			DataColumn[] newcolumns = new DataColumn[second.Columns.Count];
			for (int i = 0; i < second.Columns.Count; i++)
			{
				newcolumns[i] = new DataColumn(second.Columns[i].ColumnName, second.Columns[i].DataType);
			}

			int firstcolcount = first.Columns.Count;
			first.Columns.AddRange(newcolumns);
			first.BeginLoadData();

			for (int ci = 0; ci < first.Rows.Count; ci++)
			{
				for (int i = firstcolcount; i < first.Columns.Count; i++)
				{
					first.Rows[ci][i] = second.Rows[ci][i - firstcolcount];
				}

			}
			first.EndLoadData();
		}

		public static void RemoveDuplicates(DataTable tbl, DataColumn[] keyColumns)
		{
			int rowNdx = 0;
			while(rowNdx < tbl.Rows.Count-1)
			{
				DataRow[] dups = FindDups(tbl, rowNdx, keyColumns);
				if(dups.Length>0)
				{
					foreach(DataRow dup in dups)
					{
						tbl.Rows.Remove(dup);
					}
				}
				else
				{
					rowNdx++;
				}
			}
		}


		public static DataRow[] FindDups(DataTable tbl,int sourceNdx, DataColumn[] keyColumns)
		{
			ArrayList retVal = new ArrayList();
			DataRow sourceRow = tbl.Rows[sourceNdx];
			for(int i=sourceNdx + 1; i<tbl.Rows.Count; i++)
			{
				DataRow targetRow = tbl.Rows[i];
				if(IsDup(sourceRow, targetRow, keyColumns))
				{
					retVal.Add(targetRow);
				}
			}
			return (DataRow[]) retVal.ToArray(typeof(DataRow));
		}

		internal static bool IsDup(DataRow sourceRow, DataRow targetRow, DataColumn[] keyColumns)
		{
			bool retVal = true;
			foreach(DataColumn column in keyColumns)
			{
				retVal = retVal && sourceRow[column].Equals(targetRow[column]);
				if(!retVal) break;
			}
			return retVal;
		}

	}
}
