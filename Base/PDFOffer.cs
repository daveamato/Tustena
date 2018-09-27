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
using System.Resources;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Digita.Tustena
{
    public class PDFOffer
    {
        public string offerSubject = null;
        public string companyInfo = null;
        public string companyName = null;
        public string offerNumber = null;
        public string offerPayment = null;
        public string salesMan = null;
        public DateTime offerDate = DateTime.Now;
        public DateTime offerDateValidity = DateTime.Now;
        public DateTime offerShipDate = DateTime.Now;
        public string offerShipDescription = string.Empty;
        public bool offerPrintShipDate = false;
        public string billTo = null;
        public string shipTo = null;
        public string logo = null;
        public string footerNote = null;
        public string subTotal = null;
        public string ShippingCost = null;
        public string vat = null;
        public string Total = null;
        public bool useItemCode = false;
        public bool useUM = false;
        public bool useVat = false;
        public bool useDiscount = false;
        public bool useListPrice = false;
        public bool useUnitPrice = true;
        public bool usePrice = true;
        public bool isAmerican = false;
        public DataTable gridTable = new DataTable();
        public string appendFiles = null;

        string[] heads = new string[] { "Codice", "Prodotto", "UM", "Qta", "IVA", "Sconto", "Prezzo Listino", "Prezzo Unitario", "Imponibile" };
        float[] headerwidths = { 10, 15, 6, 6, 6, 12, 15, 15, 15 };
        public static ResourceManager rm = (ResourceManager)HttpContext.Current.Application["RM"];

        private PdfWriter writer;

        private Document document = new Document();
        Font OfferFontBold = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(System.Drawing.Color.White));
        Font headerFontBold = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, new Color(System.Drawing.Color.Black));
        Font offerFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, new Color(System.Drawing.Color.Black));
        Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.NORMAL, new Color(System.Drawing.Color.Black));
        Font companyFontBold = FontFactory.GetFont(FontFactory.HELVETICA, 20, Font.NORMAL, new Color(System.Drawing.Color.Black));
        Font companyFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL, new Color(System.Drawing.Color.Black));
        Font offerDescriptionFont = FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.NORMAL, new Color(System.Drawing.Color.Black));
        Font noteFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.UNDERLINE | Font.BOLD, new Color(System.Drawing.Color.Black));

        float defaultLeading = 13;

        public PDFOffer()
        {
            document = new Document(PageSize.A4, 20, 20, 20, 20);
            heads = GetLabel(1).Split('|');
        }

        public void GetPDF(object output)
        {
            if (isAmerican)
            {
                string tempObj = heads[0];
                heads[0] = heads[2];
                heads[2] = tempObj;
                float tempObj2 = headerwidths[0];
                headerwidths[0] = headerwidths[2];
                headerwidths[2] = tempObj2;
            }

            if (output is HttpResponse)
            {
                HttpResponse Response = (HttpResponse)output;
                Response.Clear();
                Response.ContentType = "application/pdf";
                writer = PdfWriter.GetInstance(document, Response.OutputStream);
                Response.AddHeader("Content-Disposition", "attachment; filename=offer" + offerNumber + ".pdf");
            }
            else
                writer = PdfWriter.GetInstance(document, new FileStream("test.pdf", FileMode.Create));
            AddProperties();
            AddLogo();
            AddCompanyInfo();
            MakeHeader();
            MakeTableHeader();
            MakeTable();
            if (appendFiles != null)
                foreach (string s in appendFiles.Split(';'))
                    AddPage(s);
            FinalizeDocument();
            if (output is HttpResponse)
            {
                HttpResponse Response = (HttpResponse)output;
                Response.End();
            }
        }

        private void AddProperties()
        {
            document.AddCreator("Tustena CRM");
            document.AddCreationDate();
            document.AddSubject(offerSubject);
            document.AddTitle(String.Format(GetLabel(2), offerNumber, offerDate));
            HeaderFooter footer = new HeaderFooter(new Phrase(String.Format(GetLabel(3), offerNumber, offerDate.ToShortDateString()), companyFont), true);
            footer.Border = Rectangle.NO_BORDER;
            footer.Alignment = Element.ALIGN_RIGHT;
            document.Footer = footer;
            document.Open();
        }

        private void TustenaSign()
        {
            PdfContentByte cb = writer.DirectContent;
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.BeginText();
            cb.SetFontAndSize(bf, 5);
            cb.SetTextMatrix(0, document.PageSize.Height - 25);
            cb.ShowText("(c)2006 Tustena CRM.");
            cb.EndText();
        }

        private void AddLogo()
        {
            if (logo != null)
            {
                Image image = Image.GetInstance(logo);
                if (image.Width > document.PageSize.Width - 40)
                    image.ScaleToFit(document.PageSize.Width - 40, 80f);
                document.Add(image);
            }
        }

        private void AddCompanyInfo()
        {
            if (companyName == null && companyInfo == null) return;

            Table table = new Table(1);
            table.Offset = 5;
            table.WidthPercentage = 100;
            table.Cellpadding = 2;
            table.Border = Rectangle.NO_BORDER;
            Cell cell = new Cell();
            if (companyName != null)
                cell.Add(new Phrase(defaultLeading + 2, companyName + "\n", companyFontBold));
            if (companyInfo != null)
                cell.Add(new Paragraph(defaultLeading, companyInfo + "\n", companyFont));
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            document.Add(table);
        }

        private void MakeHeader()
        {
            Table table = new Table(2, 2);
            table.Offset = 5;
            table.WidthPercentage = 100;
            table.Cellpadding = 2;
            table.Border = Rectangle.NO_BORDER;
            Cell cell = new Cell();


            Phrase phrase = new Phrase(defaultLeading, GetLabel(4) + " ", headerFontBold);

            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, offerSubject + "\n", headerFont);
            cell.Add(phrase);
            cell.Border = Rectangle.TOP_BORDER;
            cell.Colspan = 2;
            table.AddCell(cell);
            cell = new Cell();
            phrase = new Phrase(defaultLeading, GetLabel(5) + " ", headerFontBold);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, offerNumber + "\n", headerFont);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, GetLabel(6) + " ", headerFontBold);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, offerPayment + "\n", headerFont);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, GetLabel(7) + " ", headerFontBold);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, salesMan + "\n", headerFont);
            cell.Add(phrase);
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);

            cell = new Cell();
            phrase = new Phrase(defaultLeading, GetLabel(8) + " ", headerFontBold);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, offerDate.ToShortDateString() + "\n", headerFont);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, GetLabel(9) + " ", headerFontBold);
            cell.Add(phrase);
            if (this.PrintType != PDFType.Order)
                phrase = new Phrase(defaultLeading, offerDateValidity.ToShortDateString() + "\n", headerFont);
            else
                phrase = new Phrase(defaultLeading, "\n", headerFont);
            cell.Add(phrase);
            phrase = new Phrase(defaultLeading, GetLabel(10) + " ", headerFontBold);
            cell.Add(phrase);

            if (offerShipDescription != string.Empty)
            {
                if (offerPrintShipDate)
                {
                    if (DateTime.MinValue != offerShipDate)
                        phrase = new Phrase(defaultLeading, offerShipDescription + " " + offerShipDate.ToShortDateString() + "\n", headerFont);
                    else
                        phrase = new Phrase(defaultLeading, offerShipDescription + "\n", headerFont);
                }
                else
                    phrase = new Phrase(defaultLeading, offerShipDescription + "\n", headerFont);

            }
            else
                phrase = new Phrase(defaultLeading, GetLabel(11) + "\n", headerFont);
            cell.Add(phrase);
            cell.Border = Rectangle.NO_BORDER;
            table.AddCell(cell);
            document.Add(table);

            table = new Table(2);
            table.Offset = 5;
            table.WidthPercentage = 100;
            table.Cellpadding = 2;
            table.Border = Rectangle.NO_BORDER;
            phrase = new Phrase(defaultLeading + 2, GetLabel(12) + "\n", headerFontBold);
            Paragraph par = new Paragraph(defaultLeading, billTo + "\n", headerFont);
            cell = new Cell();
            cell.Add(phrase);
            cell.Add(par);
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Border = Rectangle.TOP_BORDER;
            table.AddCell(cell);

            phrase = new Phrase(defaultLeading + 2, GetLabel(13) + "\n", headerFontBold);
            par = new Paragraph(defaultLeading, shipTo + "\n", headerFont);
            cell = new Cell();
            cell.Add(phrase);
            cell.Add(par);
            cell.VerticalAlignment = Element.ALIGN_TOP;
            cell.Border = Rectangle.TOP_BORDER;
            table.AddCell(cell);
            document.Add(table);
        }

        private void MakeTableHeader()
        {
            Table table = new Table(1);
            table.Offset = 5;
            table.WidthPercentage = 100;
            table.Padding = 2;
            Cell cell = new Cell(new Phrase(GetLabel(14), OfferFontBold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.BackgroundColor = Color.BLACK;
            cell.Leading = 10;
            table.AddCell(cell);
            document.Add(table);
        }

        private void MakeTable()
        {
            int tableColumns = heads.Length;
            if (!useItemCode)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[0];
                headerwidths[0] = 0;
            }
            if (!useUM)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[2];
                headerwidths[2] = 0;
            }
            if (!useVat)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[4];
                headerwidths[4] = 0;
            }
            if (!useDiscount)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[5];
                headerwidths[5] = 0;
            }
            if (!this.useListPrice)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[6];
                headerwidths[6] = 0;
            }
            if (!this.useUnitPrice)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[7];
                headerwidths[7] = 0;
            }
            if (!this.usePrice)
            {
                tableColumns--;
                headerwidths[1] += headerwidths[8];
                headerwidths[8] = 0;
            }

            Table table = new Table(tableColumns);
            table.CellsFitPage = true;
            table.Border = Rectangle.NO_BORDER;
            table.Padding = 2;
            table.Spacing = 0;
            float[] tempWidths = new float[tableColumns];
            int j = 0;
            for (int i = 0; i < headerwidths.Length; i++)
                if (headerwidths[i] != 0)
                    tempWidths[j++] = headerwidths[i];
            table.Widths = tempWidths;
            table.WidthPercentage = 100;
            table.DefaultColspan = 1;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            bool first = true;
            for (int i = 0; i < heads.Length; i++)
            {
                if (headerwidths[i] > 0)
                {
                    Cell HeadCell = new Cell(new Phrase(heads[i], headerFontBold));
                    if (first)
                    {
                        HeadCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                        first = false;
                    }
                    else
                        HeadCell.Border = Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
                    table.AddCell(HeadCell);
                }
            }

            table.DefaultRowspan = 1;
            Cell cell = new Cell();

            for (int i = 0; i < gridTable.Rows.Count; i++)
            {
                for (int i2 = 0; i2 < gridTable.Columns.Count; i2++)
                {
                    if (headerwidths[i2] == 0)
                        continue;
                    switch (i2)
                    {
                        case 0:
                            table.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCellBorder = Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                            break;
                        case 1:
                            table.DefaultHorizontalAlignment = Element.ALIGN_LEFT;
                            table.DefaultCellBorder = Rectangle.RIGHT_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER;
                            break;
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
                            table.DefaultCellBorder = Rectangle.BOTTOM_BORDER;
                            break;
                        case 6:
                        case 7:
                            table.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCellBorder = Rectangle.BOTTOM_BORDER;
                            break;
                        case 8:
                            table.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
                            table.DefaultCellBorder = Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER;
                            break;
                    }
                    string text = gridTable.Rows[i][i2].ToString().TrimEnd(new char[] { '\r', '\n', ' ' });
                    int pos = -1;
                    if (i2 == 0 && (pos = text.IndexOf("\n")) > -1)
                    {
                        cell = new Cell();
                        cell.Add(new Phrase(text.Substring(0, pos + 1), offerFont));
                        cell.Add(new Phrase(10, text.Substring(pos + 1), offerDescriptionFont));
                        table.AddCell(cell);
                    }
                    else
                        table.AddCell(new Phrase(text, offerFont));
                }
            }

            table.DefaultCellBorder = Rectangle.BOTTOM_BORDER;
            table.DefaultHorizontalAlignment = Element.ALIGN_RIGHT;
            table.DefaultVerticalAlignment = Element.ALIGN_BOTTOM;
            cell = new Cell(new Phrase(GetLabel(15), headerFontBold));
            cell.Colspan = tableColumns - 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(subTotal, headerFont));
            cell.Colspan = 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(GetLabel(16), headerFontBold));
            cell.Colspan = tableColumns - 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(ShippingCost, headerFont));
            cell.Colspan = 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(GetLabel(17), headerFontBold));
            cell.Colspan = tableColumns - 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(vat, headerFont));
            cell.Colspan = 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(GetLabel(18), headerFontBold));
            cell.Colspan = tableColumns - 1;
            table.AddCell(cell);
            cell = new Cell(new Phrase(Total, headerFontBold));
            cell.Leading = 12;
            cell.Colspan = 1;
            table.AddCell(cell);
            AddNoteFooter(ref table, tableColumns);
            document.Add(table);
        }

        private void AddNoteFooter(ref Table table, int tableColumns)
        {
            Cell cell = new Cell();
            int pos = 0;
            if ((pos = footerNote.IndexOf("\n")) > -1 && footerNote.Substring(0, pos - 1).EndsWith(":"))
            {
                Paragraph par = new Paragraph();
                par.Add(new Phrase(footerNote.Substring(0, pos), noteFont));
                par.Add(new Phrase(footerNote.Substring(pos), headerFont));
                cell.Add(par);
            }
            else
                cell.Add(new Paragraph(footerNote, headerFont));
            cell.Leading = 20;
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Colspan = tableColumns;
            cell.Border = 1;
            table.AddCell(cell);
        }

        private void AddPage(string fileName)
        {
            PdfReader reader = new PdfReader(fileName);
            PdfContentByte cb = writer.DirectContent;
            int n = reader.NumberOfPages;
            int i = 1;
            int rotation;

            while (i <= n)
            {
                document.SetPageSize(reader.GetPageSizeWithRotation(i));
                document.NewPage();
                PdfImportedPage page;
                page = writer.GetImportedPage(reader, i);
                rotation = reader.GetPageRotation(i);
                if (rotation == 90 || rotation == 270)
                {
                    cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                }
                else
                {
                    cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                }
                i++;
            }
        }

        private void FinalizeDocument()
        {
            document.Close();
        }

        private void HLine()
        {
            Graphic g = new Graphic();
            g.SetHorizontalLine(5f, 100f);
            document.Add(g);
        }

        PDFType printType = PDFType.Quote;
        public PDFType PrintType
        {
            set { printType = value; }
            get { return printType; }
        }

        private string GetLabel(int i)
        {
            string lbl = string.Empty;
            switch (this.PrintType)
            {
                case PDFType.Quote:
                    lbl = rm.GetString("PDFQuote0" + i);
                    break;
                case PDFType.Order:
                    lbl = rm.GetString("PDFOrder1" + i);
                    break;
                case PDFType.Invoice:
                    break;
            }
            return lbl;
        }
    }

    public enum PDFType
    {
        Quote = 0,
        Order,
        Invoice
    }
}
