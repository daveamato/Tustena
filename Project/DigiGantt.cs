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
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Digita.Tustena.Project
{
    class DigiGantt
    {
        private int dayColumnSize = 14;
        public int DayColumnSize
        {
            get { return dayColumnSize; }
            set { dayColumnSize = value; }
        }
        private int rowsSize = 14;

        public int RowsSize
        {
            get { return rowsSize; }
            set { rowsSize = value; }
        }
        private int maxLegendWidth = 200;

        public int MaxLegendWidth
        {
            get { return maxLegendWidth; }
            set { maxLegendWidth = value; }
        }
        int rowSpace = 3;

        public int RowSpace
        {
            get { return rowSpace; }
            set { rowSpace = value; }
        }
        private int yearRow = -1;
        private int monthRow = -1;
        private int weekRow = -1;
        private int dayRow = -1;

        int ganttHeight = -1;
        private int top = 0;
        private int left = 0;
        private DateTime ganttStart;
        private DateTime ganttEnd;
        Graphics g;
        Font monthFont,descFont, descFontBold, monthDayFont;
        StringFormat sf;
        public Image DrawSchema(out CoordsMaps coordsMaps, int colHeight, DateTime startDate, DateTime endDate, RowsFlags Pflags)
        {

            System.Globalization.CultureInfo UICulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            System.Globalization.Calendar cal = UICulture.Calendar;
            ganttStart = startDate;
            ganttEnd = endDate;

            int days = endDate.Subtract(startDate).Days;
            int rowCount = 0;

            if ((Pflags & RowsFlags.NoYears) != RowsFlags.NoYears)
                yearRow = (rowCount++) * rowsSize + top;
            int startYear = startDate.Year;
            int firstColYear = 0;

            if ((Pflags & RowsFlags.NoMonths) != RowsFlags.NoMonths)
                monthRow = (rowCount++) * rowsSize + top;
            int startMonth = startDate.Month;
            int firstColMonth = 0;

            if ((Pflags & RowsFlags.NoWeeks) != RowsFlags.NoWeeks)
                weekRow = (rowCount++) * rowsSize + top;
            int startWeek = cal.GetWeekOfYear(startDate, UICulture.DateTimeFormat.CalendarWeekRule, UICulture.DateTimeFormat.FirstDayOfWeek);
            int firstColWeek = 0;

            if ((Pflags & RowsFlags.NoDays) != RowsFlags.NoDays)
                dayRow = (rowCount++) * rowsSize + top;
            this.ganttHeight = colHeight + dayRow + 1;
            Image img = new Bitmap(days * dayColumnSize + 1, this.ganttHeight);
            g = Graphics.FromImage(img);
            g.Clear(Color.White);

            SolidBrush brush = new SolidBrush(Color.Black);
            SolidBrush dayBrush = new SolidBrush(Color.LightGray);
            SolidBrush weekendBrush = new SolidBrush(Color.DarkGray);
            sf = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces | StringFormatFlags.MeasureTrailingSpaces);
            Font yearFont = FontToFitVertical(new FontFamily("Arial"), FontStyle.Regular, rowsSize, sf);
            monthFont = FontToFitVertical(new FontFamily("Arial"), FontStyle.Regular, rowsSize, sf);
            descFont = FontToFitVertical(new FontFamily("Arial"), FontStyle.Regular, rowsSize, sf);
            descFontBold = FontToFitVertical(new FontFamily("Arial"), FontStyle.Bold, rowsSize, sf);
            monthDayFont = FontToFitHorizontal(new FontFamily("Arial"), FontStyle.Regular, dayColumnSize, sf);
            Pen pen = new Pen(Color.DarkGray, 1F);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            int dayLeft = 0;
            Rectangle ra = new Rectangle();
            CoordsMaps cm = new CoordsMaps();
            for (int i = 0; i <= days; i++)
            {
                int currentPixel = i * dayColumnSize;
                DateTime todayDate = cal.AddDays(startDate, i);
                if (yearRow != -1 && (todayDate.Year != startYear || i == days))
                {
                    ra = new Rectangle(left + firstColYear, yearRow, currentPixel - firstColYear, rowsSize);
                    g.DrawRectangle(pen, ra);
                    g.DrawString(startYear.ToString(), monthFont, brush, ra, sf);
                    cm.Add(new CoordsMap(RowTypes.Years, ra, startYear));
                    startYear = todayDate.Year;
                    firstColYear = currentPixel;
                }
                if (monthRow != -1 && (todayDate.Day == 1 || i == days))
                {
                    ra = new Rectangle(left + firstColMonth, monthRow, currentPixel - firstColMonth, rowsSize);
                    string month = UICulture.DateTimeFormat.GetMonthName(startMonth);
                    if (yearRow == -1)
                        month += " " + todayDate.Year;
                    g.DrawRectangle(pen, ra);
                    if (g.MeasureString(month, monthFont, 100000, sf).Width < ra.Width + 4)
                        g.DrawString(month, monthFont, brush, ra, sf);
                    else if (g.MeasureString(UICulture.DateTimeFormat.GetMonthName(startMonth), monthFont, 100000, sf).Width < ra.Width + 4)
                        g.DrawString(UICulture.DateTimeFormat.GetMonthName(startMonth), monthFont, brush, ra, sf);

                    cm.Add(new CoordsMap(RowTypes.Months, ra, startMonth));
                    startMonth = todayDate.Month;
                    firstColMonth = currentPixel;
                }
                if (weekRow != -1 && (todayDate.DayOfWeek == UICulture.DateTimeFormat.FirstDayOfWeek || i == days))
                {
                    ra = new Rectangle(left + firstColWeek, weekRow, currentPixel - firstColWeek, rowsSize);
                    g.DrawRectangle(pen, ra);
                    g.DrawString(startWeek.ToString(), monthFont, brush, ra, sf);
                    cm.Add(new CoordsMap(RowTypes.Weeks, ra, startWeek));
                    startWeek++;
                    firstColWeek = currentPixel;

                }
                if (dayRow != -1 && i != days)
                {
                    ra = new Rectangle(left + dayLeft, dayRow, dayColumnSize, rowsSize);
                    dayLeft += (dayColumnSize);

                    Rectangle r = ra;
                    r.Height = colHeight;
                    if (todayDate.DayOfWeek == DayOfWeek.Sunday || todayDate.DayOfWeek == DayOfWeek.Saturday)
                        g.FillRectangle(dayBrush, r);
                    g.DrawRectangle(pen, r);
                    cm.Add(new CoordsMap(RowTypes.Days, r, i));

                    g.DrawString(cal.GetDayOfMonth(todayDate).ToString(), monthDayFont, brush, ra, sf);
                    Rectangle r2 = ra;
                    r2.Y += monthDayFont.Height;
                    g.DrawString(UICulture.DateTimeFormat.GetShortestDayName(cal.GetDayOfWeek(todayDate)).ToUpper().Substring(0, 1), monthDayFont, brush, r2, sf);
                    if (todayDate.ToShortDateString() == DateTime.Now.ToShortDateString())
                    {
                        g.DrawLine(new Pen(Color.Red), currentPixel, dayRow, currentPixel, dayRow + colHeight);
                    }
                }
            }
            dayRow += monthDayFont.Height;
            coordsMaps = cm;
            return img;
        }

        public Image CloseGanttChart(Image bmp, int lastRow)
        {
            ganttHeight = dayRow + (lastRow * (rowsSize + rowSpace) + rowsSize);
            Bitmap newBmp = new Bitmap(bmp.Width, ganttHeight);
            Graphics outGraphic = System.Drawing.Graphics.FromImage(newBmp);
            outGraphic.DrawImage(bmp, 0, 0);
            outGraphic.DrawLine(new Pen(Color.DarkGray, 1F), 0, ganttHeight - 1, bmp.Width, ganttHeight - 1);
            return newBmp;
        }



        public Image DrawLegend(ref CoordsMaps coordsMaps, string[][] labels, int startRow)
        {
            Bitmap outBmp = new Bitmap(1, 1);
            for (int i = 0; i < labels.Length; i++)
            {
                int tempStartRow = startRow;
                Bitmap bmp = new Bitmap(1, 1);
                Graphics graphic = System.Drawing.Graphics.FromImage(bmp);
                SolidBrush brush = new SolidBrush(Color.Black);
                int margins = 5;
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Near;
                graphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                int width = 0;
                foreach (string label in labels[i])
                {
                    int testWidth = Convert.ToInt32(graphic.MeasureString(label, descFont, new PointF(0, 0), sf).Width);
                    if (testWidth > width)
                        width = testWidth;
                    if (width > maxLegendWidth)
                    {
                        width = maxLegendWidth;
                        break;
                    }
                }
                width += Convert.ToInt32(0.05 * width) + margins * 2;
                Rectangle ra = new Rectangle(0, 0, width - 1, ganttHeight - 1);
                bmp = new Bitmap(width, ganttHeight);

                graphic = System.Drawing.Graphics.FromImage(bmp);
                graphic.Clear(Color.White);
                Pen pen = new Pen(Color.DarkGray, 1F);
                graphic.DrawRectangle(pen, ra);
                Regex rx = new Regex(@"\{([PSE])(\d{2})\}");
                Match m;
                int height = rowsSize + rowSpace;
                for (int j = 0; j < labels[0].Length;j++ )
                {
                    Rectangle r = new Rectangle(0, dayRow + (tempStartRow++ * height) - rowSpace, width, height);
                    graphic.DrawLine(pen, 0, r.Y, width, r.Y);
                    if (labels[i].Length <= j)
                        continue;
                    PointF point = new PointF(margins, r.Y + rowSpace);
                    if ((m = rx.Match(labels[i][j])).Success)
                    {
                        point.X += 3 * int.Parse(m.Groups[2].ToString());
                        switch (m.Groups[1].ToString())
                        {
                            case "P":
                                graphic.DrawImage(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Digita.Tustena.Project.Resources.project.png")), point);
                                break;
                            case "S":
                                graphic.DrawImage(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Digita.Tustena.Project.Resources.section.png")), point);
                                break;
                            case "E":
                                graphic.DrawImage(Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("Digita.Tustena.Project.Resources.event.png")), point);
                                break;
                        }
                        point.X += 15;
                        labels[i][j] = labels[i][j].Replace(m.Groups[0].ToString(), "");

                    }
                    if (tempStartRow == 1)
                        graphic.DrawString(labels[i][j].Replace("\n", ""), descFontBold, brush, point, sf);
                    else
                        graphic.DrawString(labels[i][j].Replace("\n", ""), descFont, brush, point, sf);
                     if (i == 0)
                         coordsMaps.Add(new CoordsMap(RowTypes.Legend, r, labels[i][j]));
                }
                if (labels.Length == 1)
                    return bmp;

                outBmp = AppendImage(outBmp, bmp);
                graphic.DrawLine(pen, 0, dayRow + (tempStartRow * height), width, dayRow + (tempStartRow * height));

             }

            return outBmp;

        }

        private Bitmap AppendImage(Bitmap bmp, Bitmap appendBmp)
        {
            Bitmap newBmp = new Bitmap(bmp.Width + appendBmp.Width - 1, appendBmp.Height);
            Graphics outGraphic = System.Drawing.Graphics.FromImage(newBmp);
            outGraphic.DrawImage(bmp, 0, 0);
            outGraphic.DrawImage(appendBmp, bmp.Width - 1, 0);
            outGraphic.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, newBmp.Width - 2, dayRow - 3));
            return newBmp;
        }

        public CoordsMap DrawSection(DateTime startDate, DateTime endDate, Color colour, int row, int progress, int reference)
        {
            int drawIndex = rowsSize * row;
            int days = startDate.Subtract(ganttStart).Days;
            if (days < 0)
                throw new Exception("startDate can't be lower then Gantt chart");
            int duration = endDate.Subtract(startDate).Days+1;
            if (duration < 0)
                throw new Exception("endDate error");
            Rectangle r = new Rectangle(left + days * dayColumnSize, dayRow + drawIndex + row * rowSpace, duration * dayColumnSize, dayColumnSize);
            SolidBrush brush = new SolidBrush(colour);
            SolidBrush progressBrush = new SolidBrush(Color.Black);
            Pen pen = new Pen(Color.Black, 1F);
            g.FillRectangle(brush, r);
            g.DrawRectangle(pen, r);
            if (progress > -1)
            {
                int progressLength = (int)(((decimal)r.Width / 100) * progress);
                int sectionMiddle = (int)(r.Y + r.Height / 2);
                g.DrawLine(new Pen(Color.Black, (int)(r.Height / 4)), r.X, sectionMiddle, r.X + progressLength, sectionMiddle);
                g.DrawLine(pen, r.X + progressLength, sectionMiddle - (int)(r.Height / 4), r.X + progressLength, sectionMiddle + (int)(r.Height / 4));
                SizeF size = g.MeasureString(progress.ToString() + "%", monthFont, 100000, sf);
                if (size.Width < r.Width + 4 && (progressLength / 2) + 4 > size.Width)
                {
                    g.FillRectangle(brush, r.X + (progressLength / 2) - (size.Width / 2), r.Y + 1, size.Width, r.Height - 2);
                    g.DrawString(progress.ToString() + "%", monthFont, progressBrush, new PointF(r.X + (progressLength / 2), r.Y + (r.Height / 2)), sf);
                }
            }
            return new CoordsMap(RowTypes.Section, r, progress, reference);
        }

        public CoordsMap DrawGhost(DateTime startDate, DateTime endDate, int row,Color colour, int reference)
        {
            int drawIndex = rowsSize * row;
            int days = startDate.Subtract(ganttStart).Days;
            if (days < 0)
                throw new Exception("startDate can't be lower then Gantt chart");
            int duration = endDate.Subtract(startDate).Days + 1;
            if (duration < 0)
                throw new Exception("endDate error");
            Rectangle r = new Rectangle(left + days * dayColumnSize, dayRow + drawIndex + row * rowSpace, duration * dayColumnSize, dayColumnSize);
            Pen pen = new Pen(Color.Black, 1F);
            pen.DashPattern = new float[] { 5, 5};
            g.CompositingQuality = CompositingQuality.GammaCorrected;
            g.FillRectangle(new SolidBrush(Color.FromArgb(85, colour)), r);
            g.CompositingQuality = CompositingQuality.Default;
            g.DrawRectangle(pen, r);
            return new CoordsMap(RowTypes.Section, r, 0, reference);
        }

        public CoordsMap DrawProject(DateTime startDate, DateTime endDate, int row)
        {
            int drawIndex = rowsSize * row;
            int days = startDate.Subtract(ganttStart).Days;
            if (days < 0)
                throw new Exception("startDate can't be lower then Gantt chart");
            int duration = endDate.Subtract(startDate).Days + 1;
            if (duration < 0)
                throw new Exception("endDate error");
            Rectangle r = new Rectangle(left + days * dayColumnSize, dayRow + drawIndex + row * rowSpace, duration * dayColumnSize, dayColumnSize);
            Pen pen = new Pen(Color.Black, 4F);
            g.DrawLine(pen, r.X, r.Y+2, r.X + r.Width, r.Y+2);

            SolidBrush brush = new SolidBrush(Color.Black);
            Point[] p1 = new Point[]{
                new Point(r.X+(int)(r.Height / 2),r.Y+2),
                new Point(r.X, r.Y+2 + (int)(r.Height / 2)),
                new Point(r.X, r.Y+2),
                new Point(r.X + r.Width, r.Y+2),
                new Point(r.X + r.Width, r.Y+2 + (int)(r.Height / 2)),
                new Point(r.X + r.Width - (int)(r.Height / 2), r.Y+2)
            };

            g.FillPolygon(brush, p1);
            g.DrawPolygon(pen, p1);

            return new CoordsMap(RowTypes.Project, r, 0);
        }

        public CoordsMap DrawPoint(DateTime pointDate, Color colour, int row, int reference, string description)
        {
            int drawIndex = rowsSize * row;
            int days = pointDate.Subtract(ganttStart).Days;
            if (days < 0)
                throw new Exception("pointDate can't be lower then Gantt chart");
            Rectangle r = new Rectangle(left + days * dayColumnSize, dayRow + drawIndex + row * rowSpace, dayColumnSize, dayColumnSize);
            SolidBrush brush = new SolidBrush(colour);
            Pen pen = new Pen(Color.Black, 1F);
            Point[] p = new Point[]{
                new Point(r.X+(int)(r.Width/2),r.Y),
                new Point(r.X+r.Width,r.Y+(int)(r.Height/2)),
                new Point(r.X+(r.Width/2),r.Y+r.Height),
                new Point(r.X,r.Y+(int)(r.Height/2))
            };
            g.FillPolygon(brush, p);
            g.DrawPolygon(pen, p);

            return new CoordsMap(RowTypes.Event, r, 0, reference, description);
        }

        public CoordsMap DrawExpected(DateTime pointDate, Color colour, int row, int reference, string description)
        {
            int drawIndex = rowsSize * row;
            int days = pointDate.Subtract(ganttStart).Days;
            if (days < 0)
                throw new Exception("pointDate can't be lower then Gantt chart");
            Rectangle r = new Rectangle(left + days * dayColumnSize, dayRow + drawIndex + row * rowSpace, dayColumnSize, dayColumnSize);
            SolidBrush brush = new SolidBrush(colour);
            Pen pen = new Pen(Color.Black, 1F);
            Point[] p = new Point[]{
                new Point(r.X+(int)(r.Width/3*2),r.Y-2),
                new Point(r.X+(int)(r.Width/3*4),r.Y-2),
                new Point(r.X+r.Width,r.Y+(int)(r.Height/2))
            };
            g.FillPolygon(brush, p);
            g.DrawPolygon(pen, p);

            return new CoordsMap(RowTypes.Event, r, 0, reference, description);
        }

        public void DrawConnector(DateTime date1, int row1, DateTime date2, int row2)
        {
            int drawIndex1 = rowsSize * row1;
            int drawIndex2 = rowsSize * row2;
            int days1 = date1.Subtract(ganttStart).Days;
            int days2 = date2.Subtract(ganttStart).Days;
            Pen pen = new Pen(Color.Maroon, 2F);
            SolidBrush brush = new SolidBrush(Color.Maroon);
            ArrayList pointArr = new ArrayList();
            ArrayList arrowArr = new ArrayList();
            bool arrowVertical = true;

            if (days1 >= days2)
            {
                pointArr.Add(new Point(left + (days1) * dayColumnSize, dayRow + drawIndex1 + row1 * rowSpace + (int)(rowsSize / 2)));
                pointArr.Add(new Point((int)(left + (days1 - 0.5) * dayColumnSize), dayRow + drawIndex1 + row1 * rowSpace + (int)(rowsSize / 2)));
                pointArr.Add(new Point((int)(left + (days1 - 0.5) * dayColumnSize), dayRow + drawIndex1 + row1 * rowSpace + rowsSize + (int)(rowSpace / 2)));
                pointArr.Add(new Point((int)(left + (days2 - 0.5) * dayColumnSize), dayRow + drawIndex1 + row1 * rowSpace + rowsSize + (int)(rowSpace / 2)));

                if (days1 == days2)
                {
                    pointArr.Add(new Point((int)(left + (days2 - 0.5) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace + (int)(rowsSize / 2)));
                    pointArr.Add(new Point((int)(left + (days2) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace + (int)(rowsSize / 2)));
                    arrowVertical = false;
                }
                else
                    pointArr.Add(new Point((int)(left + (days2 - 0.5) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace));

            }
            else
            {
                pointArr.Add(new Point(left + (days1 + 1) * dayColumnSize, dayRow + drawIndex1 + row1 * rowSpace + (int)(rowsSize / 2)));
                if (row1 == row2 - 1)
                {
                    pointArr.Add(new Point((int)(left + (days2 + 0.5) * dayColumnSize), dayRow + drawIndex1 + row1 * rowSpace + (int)(rowsSize / 2)));
                }
                else
                {

                      pointArr.Add(new Point((int)(left + (days2 + 0.5) * dayColumnSize), dayRow + drawIndex1 + row1 * rowSpace + (int)(rowsSize / 2)));
                }
                pointArr.Add(new Point((int)(left + (days2 + 0.5) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace));
            }

            if (arrowVertical)
            {         //arrow
                arrowArr.Add(new Point((int)(left + (days2 + 0.5) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace));
                arrowArr.Add(new Point((int)(left + (days2 + 0.8) * dayColumnSize), dayRow + drawIndex2  + row2 * rowSpace - (int)(rowsSize * 0.5)));
                arrowArr.Add(new Point((int)(left + (days2 + 0.2) * dayColumnSize), dayRow + drawIndex2  + row2 * rowSpace - (int)(rowsSize * 0.5)));
            }
            else
            {

                arrowArr.Add(new Point(left + (days2) * dayColumnSize, dayRow + drawIndex2 + row2 * rowSpace + (int)(rowsSize / 2)));
                arrowArr.Add(new Point((int)(left + (days2 - 0.3) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace + (int)(rowsSize * 0.75)));
                arrowArr.Add(new Point((int)(left + (days2 - 0.3) * dayColumnSize), dayRow + drawIndex2 + row2 * rowSpace + (int)(rowsSize * 0.25)));

            }


            g.DrawLines(pen, (Point[])pointArr.ToArray(typeof(System.Drawing.Point)));
            g.FillPolygon(brush, (Point[])arrowArr.ToArray(typeof(System.Drawing.Point)));



        }

        private Font FontToFitHorizontal(FontFamily fontFamily, FontStyle fontStyle, int targetWidth, StringFormat sf)
        {
            Font font = null;
            SizeF size = new SizeF(targetWidth + 1, targetWidth);
            int fontSize = targetWidth - (targetWidth / 3);
            while (size.Width > targetWidth)
            {
                font = new Font(fontFamily, --fontSize, fontStyle, GraphicsUnit.Pixel);
                size = g.MeasureString("XX", font, 100000, sf);
            }
            return font;
        }
        private Font FontToFitVertical(FontFamily fontFamily, FontStyle fontStyle, int targetHeight, StringFormat sf)
        {
            Font font = null;
            SizeF size = new SizeF(targetHeight, targetHeight + 1);
            int fontSize = targetHeight;
            while (size.Height > targetHeight)
            {
                font = new Font(fontFamily, --fontSize, fontStyle, GraphicsUnit.Pixel);
                size = g.MeasureString("X", font, 100000, sf);
            }
            return font;
        }
        public string ImageMapRender(string imgUrl, DigiGantt.CoordsMaps coordsMaps)
        {
            return ImageMapRender(imgUrl, coordsMaps, -1);
        }
        public string ImageMapRender(string imgUrl, DigiGantt.CoordsMaps coordsMaps, int pixelSize)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<div><table cellspacing=0 cellpadding=0><tr><td valign=top>");

            sb.AppendFormat("<img src=\"{0}\" id=\"GanttLegend\" usemap=\"#GanttLegend\" style=\"border-width:0px\" />", imgUrl + "&draw=2");

            sb.Append("</td><td valign=top><div id=\"GanttMapCont\" style=\"overflow-x:auto;\">");
            sb.AppendFormat("<img src=\"{0}\" id=\"GanttMap\" usemap=\"#GanttMap\" style=\"border-width:0px;border-right:1px solid darkgray\" /></br></br></div></td></tr></table><map name=\"GanttLegend\">", imgUrl + "&draw=1");
            foreach (DigiGantt.CoordsMap coordsMap in coordsMaps)
            {
                if (coordsMap.RowType == DigiGantt.RowTypes.Legend)
                {
                    string coords = coordsMap.Rectangle.X + "," + coordsMap.Rectangle.Y + "," + (coordsMap.Rectangle.X + coordsMap.Rectangle.Width) + "," + (coordsMap.Rectangle.Y + coordsMap.Rectangle.Height);
                    sb.AppendFormat("<area shape=\"rect\" coords=\"{0}\" href=\"javascript:alert('{2}')\" title=\"{1}\" alt=\"{1}\" />", coords, coordsMap.Title, coordsMap.Reference);
                }
            }
            sb.AppendFormat("</map><map name=\"GanttMap\">", imgUrl);
            foreach (DigiGantt.CoordsMap coordsMap in coordsMaps)
            {
                if (coordsMap.RowType == DigiGantt.RowTypes.Section || coordsMap.RowType == DigiGantt.RowTypes.Event)
                {
                    string coords = coordsMap.Rectangle.X + "," + coordsMap.Rectangle.Y + "," + (coordsMap.Rectangle.X + coordsMap.Rectangle.Width) + "," + (coordsMap.Rectangle.Y + coordsMap.Rectangle.Height);
                    sb.AppendFormat("<area shape=\"rect\" coords=\"{0}\"  href=\"javascript:EditSection('{2}')\" title=\"{1}\" alt=\"{1}\" />", coords, (coordsMap.RowType == DigiGantt.RowTypes.Event) ? coordsMap.Title : coordsMap.Value + "%", coordsMap.Reference);
                }
            }
            sb.Append("</map>");
            sb.AppendFormat("<script>document.getElementById('GanttMapCont').style.width={0}-document.getElementById('GanttLegend').width-1</script>", (pixelSize == -1) ? "document.body.offsetWidth-20" : pixelSize.ToString());
            return sb.ToString();
        }
        public byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert,ImageFormat formatOfImage)
        {
            byte[] Ret;

            try
            {

                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }

            return Ret;
        }

        [Flags]
        public enum RowsFlags
        {
            None = 0, NoYears = 1, NoMonths = 2, NoWeeks = 4, NoDays = 8, NoHours = 16
        }
        public enum RowTypes
        {
            None, Years, Months, Weeks, Days, Hours, Event, Section, Project, Legend
        }
        public class CoordsMaps: ICollection
        {
            int counter = 0;
            ArrayList RectanglesArray = new ArrayList();
            public void Add(CoordsMap coordsMap)
            {
                coordsMap.Id = counter++;
                RectanglesArray.Add(coordsMap);
            }


            #region ICollection Members

            public void CopyTo(Array array, int index)
            {
                this.RectanglesArray.CopyTo(array, index);
            }

            public int Count
            {
                get
                {
                    return (this.RectanglesArray.Count);
                }
            }

            public bool IsSynchronized
            {
                get
                {
                    return (false);
                }
            }

            public object SyncRoot
            {
                get
                {
                    return (this);
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this.RectanglesArray.GetEnumerator();
            }

            #endregion
        }
        public class CoordsMap
        {
            private int id;

            public int Id
            {
                get { return id; }
                set { id = value; }
            }
            private RowTypes rowType;

            internal RowTypes RowType
            {
                get { return rowType; }
                set { rowType = value; }
            }
            private Rectangle rectangle;

            public Rectangle Rectangle
            {
                get { return rectangle; }
                set { rectangle = value; }
            }
            private int _value;

            public int Value
            {
                get { return this._value; }
                set { this._value = value; }
            }

            private string title;

            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            private int reference;

            public int Reference
            {
                get { return reference; }
                set { reference = value; }
            }

            public CoordsMap(RowTypes rowType, Rectangle rectangle, int value)
            {
                this.rowType = rowType;
                this.rectangle = rectangle;
                this._value = value;
            }
            public CoordsMap(RowTypes rowType, Rectangle rectangle, int value, int reference)
                : this(rowType, rectangle, value)
            {
                this.reference = reference;
            }
            public CoordsMap(RowTypes rowType, Rectangle rectangle, int value, int reference, string title)
                : this(rowType, rectangle, value, reference)
            {
                this.title = title;
            }
            public CoordsMap(RowTypes rowType, Rectangle rectangle, string title)
                : this(rowType, rectangle, 0)
            {
                this.title = title;
            }

        }

    }

}
