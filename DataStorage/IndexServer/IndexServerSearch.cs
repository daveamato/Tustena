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

using System.Data;
using System.Data.OleDb;
using System.Text;
using Cisso;

namespace Digita.Tustena
{
	public class IndexServerSearch
	{
		public DataView SimpleSearch(string txtSearch)
		{
			OleDbConnection conn = new OleDbConnection("provider=msidxs;Data Source=web");
			conn.Open();

			string sql = "Select DocTitle, Vpath, Rank from Scope() WHERE contains('" + txtSearch + "') order by Rank desc";

			DataSet ds = new DataSet();
			OleDbDataAdapter da = new OleDbDataAdapter(sql, conn);
			da.Fill(ds, "Results");
			return ds.Tables[0].DefaultView;

		}

		public DataTable Search(string txtSearch)
		{
			string SearchString = txtSearch;
			OleDbDataAdapter DA = new OleDbDataAdapter();
			DataSet DS = new DataSet("IndexServerResults");

			CissoQuery Q = new CissoQuery();
			CissoUtil Util = new CissoUtil();
			StringBuilder strbldSearch = new StringBuilder(SearchString);

			Q.Query = strbldSearch.ToString();
			Q.Catalog = "TustenaDataStorage";

			Q.SortBy = "rank[a]";

			Q.Columns = "Rank, DocTitle, vpath, filename";
			Q.MaxRecords = 50;

			Util.AddScopeToQuery(Q, "/" + "1", "deep");
			try
			{
				DA.Fill(DS, Q.CreateRecordset("nonsequential"), "IndexServerResults");

				Q = null;
				Util = null;
				return DS.Tables[0];
			}
			catch
			{
			}
			return null;
		}
	}

}
