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
using Digita.Tustena.Core;

namespace Digita.Tustena.Common
{
    public partial class FreeFields : System.Web.UI.UserControl
    {
        private string strOut = null;

        protected override void OnPreRender(EventArgs e)
        {
            if (strOut != null)
                freeOut.Text = strOut;
            base.OnPreRender(e);
        }
        public void FillFreeFields(int id, CRMTables crmTables, UserConfig UC)
        {
            string sqlStringQuery;
			sqlStringQuery = "SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + (byte) crmTables + " ORDER BY VIEWORDER";
            DataSet ds = DatabaseConnection.CreateDataset(sqlStringQuery);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    try
                    {
                        if (Request.Form["Free_" + dr["name"]].Length > 0)
                        {
                            string sqlString;
						sqlString = "SELECT ID FROM ADDEDFIELDS_CROSS WHERE ID = " + id + " AND IDRIF=" + dr["id"];
                            using (DigiDapter dg = new DigiDapter(sqlString))
                            {

                                dg.Add("ID", id);
                                dg.Add("IDRIF", dr["id"]);
                                dg.Add("FIELDVAL", Request.Form["Free_" + dr["name"]]);
                                if (dg.HasRows)
                                    dg.Execute("ADDEDFIELDS_CROSS", "ID = " + id + " AND IDRIF=" + dr["id"]);
                                else
                                    dg.Execute("ADDEDFIELDS_CROSS", "PKEY = -1");
                            }
                        }
                        else
                        {
						DatabaseConnection.DoCommand("DELETE FROM ADDEDFIELDS_CROSS WHERE ID = " + id + " AND IDRIF=" + dr["id"]);
                        }
                    }
                    catch { }
                }
            }
        }

        public void ViewFreeFields(int id, CRMTables crmTables, UserConfig UC)
        {
            G g = new G();
            string query = g.GroupsSecure(UC);
            if (query.Length > 0)
                query = " groups like '%|" + UC.UserGroupId + "|%'";
            string sqlString;
					sqlString = "SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + (byte)crmTables + " AND (" + query + ") ORDER BY VIEWORDER";

            DataSet ds = DatabaseConnection.CreateDataset(sqlString);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder S = new StringBuilder();
                S.AppendFormat("<table width=\"100%\"><tr><td class=\"normal Bbot\"><br><b>{0}</b></td></tr></table>", Root.rm.GetString("Bcotxt50"));
                S.Append("<table width=\"45%\" class=\"normal\">");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    bool isParentCrossed = true;
                    DataSet Q = DatabaseConnection.CreateDataset("SELECT PARENTFIELD,PARENTFIELDVALUE FROM ADDEDFIELDS WHERE ID=" + dr["id"].ToString());
                    string pf = Q.Tables[0].Rows[0][0].ToString();
                    string pfv = Q.Tables[0].Rows[0][1].ToString();
                    if (pf.Length > 0)
                    {
                        IDataReader sqlDr = DatabaseConnection.CreateReader("SELECT " + pf + " FROM " + Enum.GetName(typeof(CRMTables), crmTables).ToUpper() + " WHERE ID=" + int.Parse(Session[(crmTables==CRMTables.Base_Companies)?"contact":"CurrentRefId"].ToString()));
                        sqlDr.Read();
                        isParentCrossed = (sqlDr[0].ToString() == pfv);
                        sqlDr.Close();
                    }

                    if (isParentCrossed)
                    {
                        S.AppendFormat("<tr><td width=\"40%\">{0}</td>", dr["name"]);
                        DataSet afCross = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + dr["id"] + " AND ID=" + id);
                        if (afCross.Tables[0].Rows.Count > 0)
                        {
                            S.AppendFormat("<td bgcolor=\"#FFFFFF\" class=\"VisForm\">{0}&nbsp;</td></tr>", afCross.Tables[0].Rows[0][0]);
                        }
                        else
                        {
                            S.Append("<td bgcolor=\"#FFFFFF\" class=\"VisForm\">&nbsp;</td></tr>");
                        }

                        afCross.Clear();
                    }
                }
                S.Append("</table>");
                strOut = S.ToString();

            }
        }

        public void CheckFreeFields(int id, CRMTables crmTables, UserConfig UC)
        {
            G g=new G();
            string query = g.GroupsSecure(UC);
            if (query.Length > 0)
                query = " GROUPS LIKE '%|" + UC.UserGroupId + "|%'";

            string sqlString;
			sqlString = "SELECT * FROM ADDEDFIELDS WHERE TABLENAME=" + (byte) crmTables + " AND (" + query + ") ORDER BY VIEWORDER";

            DataSet ds = DatabaseConnection.CreateDataset(sqlString);
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("<table width=\"100%\"><tr><td class=\"normal Bbot\"><br><b>{0}</b></td></tr></table>", Root.rm.GetString("Bcotxt50"));
                sb.Append("<table width=\"45%\" class=\"normal\">");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sb.Append(FreeFieldType(crmTables, (FreeFieldsType)dr["type"], dr["name"].ToString(), int.Parse(dr["id"].ToString()), id, dr["Items"].ToString()));
                }
                sb.Append("</table>");
                strOut = sb.ToString();
            }
        }


        public string FreeFieldType(CRMTables crmTables, FreeFieldsType type, string name, int did, int id, string items)
        {
            StringBuilder sb = new StringBuilder();
            string pf = String.Empty;
            string pfv = String.Empty;
            bool isParentCrossed = false;
            DataSet Q = DatabaseConnection.CreateDataset("SELECT PARENTFIELD,PARENTFIELDVALUE FROM ADDEDFIELDS WHERE ID=" + did);
            pf = Q.Tables[0].Rows[0][0].ToString();
            pfv = Q.Tables[0].Rows[0][1].ToString();
            if (pf.Length > 0 && id != -1)
            {
                IDataReader sqlDr = DatabaseConnection.CreateReader("SELECT " + pf + " FROM " + Enum.GetName(typeof(CRMTables), crmTables).ToUpper() + " WHERE ID=" + int.Parse(Session[(crmTables == CRMTables.Base_Companies) ? "contact" : "CurrentRefId"].ToString()));
                sqlDr.Read();
                Trace.Warn("PF-PFV", pf + "-" + pfv);
                isParentCrossed = (sqlDr[0].ToString() == pfv);
                Trace.Warn("IsParentCrossed", isParentCrossed.ToString());
                sqlDr.Close();
            }

            if (pf.Length > 0)
            {
                if (isParentCrossed)
                    sb.AppendFormat("<tr ParentField=\"{1}\" ParentFieldValue=\"{2}\"><td width=\"40%\">{0}</td>", name, pf, pfv);
                else
                    sb.AppendFormat("<tr ParentField=\"{1}\" ParentFieldValue=\"{2}\" style=\"display:none;\"><td width=\"40%\">{0}</td>", name, pf, pfv);
            }
            else
                sb.AppendFormat("<tr><td width=\"40%\">{0}</td>", name);
            switch (type)
            {
                case FreeFieldsType.inputText:
                case FreeFieldsType.username:
                case FreeFieldsType.password:
                case FreeFieldsType.inputNumber:
                    if (id != -1)
                    {
                        DataSet afCross = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + did + " AND ID=" + id);
                        if (afCross.Tables[0].Rows.Count > 0)
                        {
                            sb.AppendFormat("<td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" {2}class=\"BoxDesign\" value=\"{1}\"></td></tr>", name, afCross.Tables[0].Rows[0][0], (type == FreeFieldsType.inputNumber)?"onkeypress=\"NumbersOnly(event,'.,',this)\"":"");
                        }
                        else
                        {
                            sb.AppendFormat("<td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" {1}class=\"BoxDesign\"></td></tr>", name, (type == FreeFieldsType.inputNumber)?"onkeypress=\"NumbersOnly(event,'.,',this)\"":"");
                        }
                        afCross.Clear();
                    }
                    else
                    {
                        sb.AppendFormat("<td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" {1}class=\"BoxDesign\"></td></tr>", name, (type == FreeFieldsType.inputNumber)?"onkeypress=\"NumbersOnly(event,'.,',this)\"":"");
                    }
                    break;
                case FreeFieldsType.inputDate:
                    string imgBtn = string.Format("<td width=\"30\">&nbsp;<img src=\"/i/SmallCalendar.gif\" border=0 style=\"cursor: pointer\" onclick=\"CreateBox('/Common/PopUpDate.aspx?Textbox=Free_{0}',event,195,195)\"></td>", name);
                    if (id != -1)
                    {
                        DataSet afCross = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + did + " AND ID=" + id);
                        if (afCross.Tables[0].Rows.Count > 0)
                        {
                            sb.AppendFormat("<td><table width=\"100%\" cellspacing=0 cellpadding=0><tr><td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" onkeypress=\"DataCheck(this,event)\" MaxLength=\"10\" class=\"BoxDesign\" value=\"{1}\"></td>{2}</tr></table></td></tr>", name, afCross.Tables[0].Rows[0][0], imgBtn);
                        }
                        else
                        {
                            sb.AppendFormat("<td><table width=\"100%\" cellspacing=0 cellpadding=0><tr><td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" onkeypress=\"DataCheck(this,event)\" MaxLength=\"10\" class=\"BoxDesign\"></td>{1}</tr></table></td></tr>", name, imgBtn);
                        }
                        afCross.Clear();
                    }
                    else
                    {
                        sb.AppendFormat("<td><table width=\"100%\" cellspacing=0 cellpadding=0><tr><td><input type=\"Text\" name=\"Free_{0}\" id=\"Free_{0}\" onkeypress=\"DataCheck(this,event)\" MaxLength=\"10\" class=\"BoxDesign\"></td>{1}</tr></table></td></tr>", name, imgBtn);
                    }



                    break;
                case FreeFieldsType.select:
                    string[] it = items.Split('|');
                    DataSet afCrossS = DatabaseConnection.CreateDataset("SELECT FIELDVAL FROM ADDEDFIELDS_CROSS WHERE IDRIF=" + did + " AND ID=" + id);
                    sb.AppendFormat("<td><select old=true name=\"Free_{0}\" id=\"Free_{0}\" class=\"BoxDesign\">", name);

                    foreach (string op in it)
                    {
                        try
                        {
                            if (op == afCrossS.Tables[0].Rows[0][0].ToString())
                            {
                                if (op.Length > 0) sb.AppendFormat("<option selected>{0}</option>", op);
                            }
                            else
                            {
                                if (op.Length > 0) sb.AppendFormat("<option>{0}</option>", op);
                            }
                        }
                        catch
                        {
                            if (op.Length > 0) sb.AppendFormat("<option>{0}</option>", op);
                        }
                    }
                    sb.Append("</select></td></tr>");
                    break;
            }
            return sb.ToString();
        }


        public enum FreeFieldsType: byte
        {
            inputText = 1, inputNumber = 2, inputDate = 3, select = 4, username = 5, password = 6

        }
    }
}
