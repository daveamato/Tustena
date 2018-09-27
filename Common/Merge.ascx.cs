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
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Digita.Tustena.Database;
using System.Text;

namespace Digita.Tustena.Common
{
    public partial class Merge : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }



        public void Run(string query, string srcTable, int[] ids, string[] labels)
        {
            query += " where ";
            foreach (int k in ids)
            {
                query += "id=" + k + " or ";
            }
            query = query.Substring(0, query.Length - 4);
            DataSet ds = DatabaseConnection.CreateDataset(query);
            if (ds.Tables[0].Rows.Count != ids.Length)
                throw new Exception("At least one ID does not exists");
            if (Request.Form["tr"] != null)
            {
                int target = int.Parse(Request.Form["tr"].ToString());
                int[] checkedArray;
                if (ViewState["checkedArray"] == null)
                {
                    ArrayList arr = new ArrayList();
                    foreach (string s in Request.Form.AllKeys)
                        if (s.StartsWith("mv_"))
                            arr.Add(int.Parse(Request.Form[s].ToString()));
                    checkedArray = (System.Int32[])arr.ToArray(typeof(System.Int32));
                    MergeRows(ds, checkedArray, labels, target);
                }
                else
                {
                    checkedArray = (int[])ViewState["checkedArray"];
                    SaveMerged(ds, checkedArray, target, srcTable, ids[target]);

                }
            }
            else
                Display(ds, ids, labels);
        }
        private void Display(DataSet ds, int[] ids,string[] labels)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int j = 0; j < labels.Length; j++)
            {
                sb.Append("<tr>");
                sb.AppendFormat("<td>{0}</td>", labels[j]);
                for (int i = 0; i < ids.Length; i++)
                    sb.AppendFormat("<td><input type=radio name=\"mv_{1}\" value=\"{2}\" {3}></td><td>{0}</td>", ds.Tables[0].Rows[i][j], j, i,(i==0)?"checked":"");
                sb.Append("</tr><tr><td>&nbsp;</td>");
            }
             for (int i = 0; i < ids.Length; i++)
                 sb.AppendFormat("<td colspan=2 align=center><input type=radio name=\"tr\" value=\"{0}\" {1}></td>", i, (i == 0) ? "checked" : "");
            sb.Append("</tr></table>");
            tb.Text = sb.ToString();
        }

        private void MergeRows(DataSet ds, int[] checkedArray, string[] labels, int target)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            for (int i = 0; i < checkedArray.Length; i++)
                sb.AppendFormat("<tr><td>{0}</td><td>{1}</td>{3}<td>{2}</td></tr>", labels[i], ds.Tables[0].Rows[target][i], ds.Tables[0].Rows[checkedArray[i]][i], (i == 0) ? "<td rowspan=" + labels.Length + ">&nbsp;=>&nbsp;</td>" : "");
            sb.AppendFormat("</table><input type=\"hidden\" name=\"tr\" value=\"{0}\">", target);
            tb.Text = sb.ToString();
            ViewState["checkedArray"] = checkedArray;
        }

        private void SaveMerged(DataSet ds, int[] checkedArray, int target, string table, int idTarget)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.UpdateOnly();
                for (int i = 0; i < checkedArray.Length; i++)
                {
                    dg.Add(ds.Tables[0].Columns[i].ColumnName, ds.Tables[0].Rows[checkedArray[i]][i],'I');
                }
                dg.Execute(table, "id=" + idTarget);
            }
        }

        protected void send_Click(object sender, EventArgs e)
        {

        }

    }
}
