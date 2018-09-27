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
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.rtf;

namespace Digita.Tustena.Report
{
    public class PDFReport : G
    {
        public MemoryStream PDFRender(DataTable dt)
        {
            return ITEXTRender(dt, false);
        }

        public MemoryStream RTFRender(DataTable dt)
        {
            return ITEXTRender(dt, true);
        }

        private MemoryStream ITEXTRender(DataTable dt, bool rtf)
        {
            Document document = new Document(PageSize.A4, 80, 50, 30, 65);
            document.AddAuthor("Tustena CRM");


            MemoryStream ms = new MemoryStream();
            if (rtf)
            {
                RtfWriter writerA = RtfWriter.GetInstance(document, ms);
            }
            else
            {
                PdfWriter writerA = PdfWriter.GetInstance(document, ms);
                writerA.ViewerPreferences = PdfWriter.PageLayoutSinglePage;
            }

            HeaderFooter header = new HeaderFooter(new Phrase("Tustena Report", FontFactory.GetFont(FontFactory.HELVETICA, 14, Font.BOLD)), false);
            HeaderFooter footer = new HeaderFooter(new Phrase("page ", FontFactory.GetFont(FontFactory.HELVETICA, 11)), true);

            document.Header = header;
            document.Footer = footer;
            document.Open();
            document.SetPageSize(PageSize.A4);
            Cell cell;
            if (dt.Rows.Count > 0)
            {
                Table table = new Table(dt.Columns.Count);
                table.TableFitsPage = false;
                table.WidthPercentage = 100;
                table.SpaceInsideCell = 2;
                table.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
                foreach (DataColumn cc in dt.Columns)
                {
                    string cellName = (cc.ColumnName.Substring(0, 3) == "{+}" || cc.ColumnName.Substring(0, 3) == "{t}") ? cc.ColumnName.Substring(3, cc.ColumnName.Length - 3) : cc.ColumnName;
                    cell = new Cell(cellName.ToString());
                    cell.BackgroundColor = new Color(200, 200, 200);
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.Header = true;
                    table.AddCell(cell);
                }
                table.EndHeaders();
                double[] sum = new double[dt.Columns.Count];
                bool[] sumType = new bool[dt.Columns.Count];
                bool boolSum = false;
                foreach (DataRow rowname in dt.Rows)
                {
                    int i = 0;
                    int indexSum = 0;
                    foreach (DataColumn cc in dt.Columns)
                    {
                        if (cc.ColumnName.Substring(0, 3) == "{+}" || cc.ColumnName.Substring(0, 3) == "{t}")
                        {
                            sum[indexSum] += Convert.ToDouble(rowname[cc.ColumnName]);
                            sumType[indexSum] = (cc.ColumnName.Substring(0, 3) == "{t}"); // true = tempo , false = numerico
                            boolSum = true;
                        }
                        indexSum++;
                        string timeToPrint = String.Empty;
                        if (cc.ColumnName.Substring(0, 3) == "{t}")
                        {
                            int duration = Convert.ToInt32(rowname[cc.ColumnName]);
                            if (duration > 0)
                            {
                                if (duration < 60)
                                {
                                    timeToPrint = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
                                }
                                else
                                {
                                    timeToPrint = ((Convert.ToInt32(duration / 60) > 9) ? Convert.ToInt32(duration / 60).ToString() : "0" + Convert.ToInt32(duration / 60).ToString()) + ":" +
                                        ((Convert.ToInt32(duration % 60) > 9) ? Convert.ToInt32(duration % 60).ToString() : "0" + Convert.ToInt32(duration % 60).ToString());
                                }

                            }
                        }
                        if (cc.ColumnName.Substring(0, 3) == "{+}" || cc.ColumnName.Substring(0, 3) == "{t}")
                        {
                            if (cc.ColumnName.Substring(0, 3) == "{t}")
                                cell = new Cell(timeToPrint);
                            else
                                cell = new Cell(rowname[cc.ColumnName].ToString());

                        }
                        else
                        {
                            cell = new Cell(rowname[cc.ColumnName].ToString());
                        }
                        if ((i++ % 2) == 0) cell.BackgroundColor = new Color(230, 230, 230);
                        table.AddCell(cell);
                    }

                }
                if (boolSum)
                {
                    for (int i = 0; i < sum.Length; i++)
                    {
                        if (sum[i] > 0)
                        {
                            if (sumType[i])
                            {
                                int duration = Convert.ToInt32(sum[i]);
                                string timeToPrint = String.Empty;
                                if (duration > 0)
                                {
                                    if (duration < 60)
                                    {
                                        timeToPrint = "00:" + ((duration > 9) ? duration.ToString() : "0" + duration.ToString());
                                    }
                                    else
                                    {
                                        timeToPrint = ((Convert.ToInt32(duration / 60) > 9) ? Convert.ToInt32(duration / 60).ToString() : "0" + Convert.ToInt32(duration / 60).ToString()) + ":" +
                                            ((Convert.ToInt32(duration % 60) > 9) ? Convert.ToInt32(duration % 60).ToString() : "0" + Convert.ToInt32(duration % 60).ToString());
                                    }
                                }

                                cell = new Cell(timeToPrint);
                            }
                            else
                                cell = new Cell(sum[i].ToString());
                        }
                        else
                            cell = new Cell("");

                        cell.BackgroundColor = new Color(200, 200, 200);
                        table.AddCell(cell);
                    }
                }
                document.Add(table);
            }
            document.Close();
            return ms;
        }
    }
}
