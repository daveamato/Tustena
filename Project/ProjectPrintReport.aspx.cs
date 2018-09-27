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
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Digita.Tustena.Project
{
    public partial class ProjectPrintReport : G
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Login())
            {
                ClientScript.RegisterStartupScript(this.GetType(), "endsession", "<script>opener.location.href=opener.location.href;self.close();</script>");
            }
            else
            {
                switch (Request["Report"])
                {
                    case "1": // Gantt
                        Gantt1.prjID = long.Parse(Request["Prj"].ToString());
                        Gantt1.MakeGantt();
                        break;
                    case "0": // GanttPDF
                        Gantt1.NoRender=true;
                        Gantt1.prjID = long.Parse(Request["Prj"].ToString());
                        Gantt1.MakeGantt();
                        Gantt1.Visible = false;
                        GetGanttPDF(Gantt1.Img1,Gantt1.Img2);
                        break;

                    case "2": // Timing per utente
                        ProjectReport pr = new ProjectReport();
                        pr.UC = UC;
                        pr.prjID = long.Parse(Request["Prj"].ToString());
                        if (Request["Member"] != null)
                        {
                            pr.MemberId = long.Parse(Request["Member"].ToString());
                            lblPrint.Text = pr.ProjectTiming(false, false);
                        }else
                            lblPrint.Text = pr.ProjectTiming(false, true);
                        break;
                }
            }
        }



        private void GetGanttPDF(System.Drawing.Image img2, System.Drawing.Image img)
        {
            Rectangle r = new Rectangle((img.Width + img2.Width) + 30, img.Height + 30);

            System.Drawing.Bitmap newBmp = new System.Drawing.Bitmap((int)r.Width,(int)r.Height);
            System.Drawing.Graphics outGraphic = System.Drawing.Graphics.FromImage(newBmp);
            outGraphic.DrawImage(img, 0, 0);
            outGraphic.DrawImage(img2, img.Width, 0);
            PdfWriter writer;
            Document document = new Document(r, 15, 15, 15, 15);
            Response.Clear();
            Response.ContentType = "application/pdf";
            writer = PdfWriter.getInstance(document, Response.OutputStream);
            Response.AddHeader("Content-Disposition", "attachment; filename=gantt.pdf");
            document.addCreator("Tustena CRM");
            document.addCreationDate();
            document.Open();
            iTextSharp.text.Image image = iTextSharp.text.Image.getInstance(newBmp as System.Drawing.Image, System.Drawing.Imaging.ImageFormat.Png);
            document.Add(image);
            document.Close();
            Response.End();
        }

    }
}
