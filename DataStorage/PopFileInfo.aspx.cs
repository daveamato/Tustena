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
using System.IO;
using Digita.Tustena.Base;
using Digita.Tustena.Core;

namespace Digita.Tustena
{
    public partial class GetFileInfo : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                Response.Redirect("/login.aspx");
            }
            else
            {
                if (Request.QueryString["downid"] != null)
                {
                    Download(int.Parse(Request.QueryString["downid"]));
                    return;
                }
                int fileId = int.Parse(Request.QueryString["fileid"]);

                DataSet ds = DatabaseConnection.CreateDataset(DataStorage.ReviewFileInfoQuery(fileId, UC.UserId, GroupsSecure("FILEMANAGER.GROUPS")).ToString());
                StringBuilder sb = new StringBuilder();
                bool firstloop = true;
                for (int i = ds.Tables[0].Rows.Count-1; i >= 0; i--)
                {
                    sb.Append("<table cellspacing=3>");
                    DataRow dr = ds.Tables[0].Rows[i];
                    if (!firstloop)
                    {
                        sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["REVIEWNUMBER"].ToString(), Core.Root.rm.GetString("Dsttxt13"));
                    }
                    else
                    {
                        sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["FILENAME"].ToString(), Core.Root.rm.GetString("Dsttxt2"));
                        sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["CREATEDDATE"].ToString(), Core.Root.rm.GetString("Dsttxt24"));
                        sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["CREATOR"].ToString(), Core.Root.rm.GetString("Dsttxt25"));
                        sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["OWNER"].ToString(), Core.Root.rm.GetString("Dsttxt26"));
                    }
                    decimal size = Convert.ToDecimal(dr["SIZE"]);
                    sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", (size / 1024).ToString("N2") + "&nbsp;Kb", Core.Root.rm.GetString("Dsttxt4"));
                    sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["LASTMODIFIEDDATE"].ToString(), Core.Root.rm.GetString("Dsttxt27"));
                    sb.AppendFormat("<tr><td>{1}:</td><td>{0}</td></tr>", dr["LASTMODIFIER"].ToString(), Core.Root.rm.GetString("Dsttxt28"));
                    sb.AppendFormat("<tr><td class=download colspan=\"2\" onclick=\"location='?downid={0}'\"><img src=\"/i/download.gif\">&nbsp;Download file</td><tr>", dr["ID"].ToString());
                    sb.Append("</table><br>");
                    firstloop = false;
                }
                lblRevision.Text = sb.ToString();

            }
        }

        private void Download(int FileId)
        {
            DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + FileId);


					FileFunctions.CheckDir(ConfigSettings.DataStoragePath, true);

            string filename;
					filename = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"].ToString();
            string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();

            string downFile = filename + Path.GetExtension(realFileName);

            if (File.Exists(downFile))
            {
                Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(downFile);
                Response.Flush();
                Response.End();
                return;

            }
            else if (File.Exists(filename))
            {
                File.Move(filename, downFile);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
                Response.ContentType = "application/octet-stream";
                Response.TransmitFile(downFile);
                Response.Flush();
                Response.End();
                return;
            }
            else
            {
                G.SendError("File lost", downFile);
            }


        }
    }
}
