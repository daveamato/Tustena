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
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Base
{
	public class DeleteChecked
	{
		public DeleteChecked()
		{
		}

		public static int MultiDelete(ArrayList ar, string dbtable)
		{
			string ids=string.Empty;
			int deleted = 0;
			foreach (string i in ar)
			{
					ids+="ID="+i+" OR ";
					deleted++;
			}
			if(ids.Length>0)
			{
				DeleteDB(ids.Substring(0,ids.Length-3),dbtable);
				return deleted;
			}
			else
				return 0;
		}

		public static int MultiLimbo(ArrayList ar, string dbtable,int UserId)
		{
			int deleted = 0;
			foreach (string i in ar)
			{
				ToLimbo(int.Parse(i),UserId,dbtable);
				deleted++;
			}

			return deleted;

		}

		public static int Delete(Repeater rep, string id, string idcheck, string dbtable)
		{
			string ids=string.Empty;
			int deleted = 0;
			for (int i = 0; i < rep.Items.Count; i++)
			{
				bool isChecked = ((CheckBox) rep.Items[i].FindControl(idcheck)).Checked;
				if (isChecked)
				{
					ids+="ID="+((Literal) (rep.Items[i].FindControl(id))).Text+" OR ";
					deleted++;
				}
			}
			if(ids.Length>0)
			{
				DeleteDB(ids.Substring(0,ids.Length-3),dbtable);
				return deleted;
			}
			else
				return 0;
		}

		private static void DeleteDB(string ids,string table)
		{
			DatabaseConnection.DoCommand("DELETE FROM "+table+" WHERE "+ids);
		}

		internal static void ToLimbo(int id, int userId, string table)
		{
			using (DigiDapter dg = new DigiDapter("SELECT ID,LIMBO,LASTACTIVITY,LASTMODIFIEDDATE,LASTMODIFIEDBYID FROM " + table + " WHERE ID =" + id))
			{
				dg.UpdateOnly();
				dg.Add("LIMBO", 1);
				dg.Add("LASTACTIVITY", 2);
				dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
				dg.Add("LASTMODIFIEDBYID", userId);
				dg.Execute(table, "ID =" + id);
			}
		}
	}
}
