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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using WebChart;

namespace Digita.Tustena
{
	public partial class Pie_chart : Page
	{
		public Color[] color = new Color[]
			{
				Color.AliceBlue,
				Color.AntiqueWhite,
				Color.Aqua,
				Color.Aquamarine,
				Color.Azure,
				Color.Beige,
				Color.Bisque,
				Color.Black,
				Color.BlanchedAlmond,
				Color.Blue,
				Color.BlueViolet,
				Color.Brown,
				Color.BurlyWood,
				Color.CadetBlue,
				Color.Chartreuse,
				Color.Chocolate,
				Color.Coral,
				Color.Cornsilk,
				Color.Crimson,
				Color.Cyan,
				Color.DarkBlue,
				Color.DarkCyan,
				Color.DarkGoldenrod,
				Color.DarkGray,
				Color.DarkGreen,
				Color.DarkKhaki,
				Color.DarkMagenta,
				Color.DarkOliveGreen,
				Color.DarkOrange,
				Color.DarkOrchid,
				Color.DarkRed,
				Color.DarkSalmon,
				Color.DarkSeaGreen,
				Color.DarkSlateBlue,
				Color.DarkSlateGray,
				Color.DarkTurquoise,
				Color.DarkViolet,
				Color.DeepPink,
				Color.DeepSkyBlue,
				Color.DimGray,
				Color.DodgerBlue,
				Color.Firebrick,
				Color.FloralWhite,
				Color.ForestGreen,
				Color.Fuchsia,
				Color.Gainsboro,
				Color.GhostWhite,
				Color.Gold,
				Color.Goldenrod,
				Color.Gray,
				Color.Green,
				Color.GreenYellow,
				Color.Honeydew,
				Color.HotPink,
				Color.IndianRed,
				Color.Indigo,
				Color.Ivory,
				Color.Khaki,
				Color.Lavender,
				Color.LavenderBlush,
				Color.LawnGreen,
				Color.LemonChiffon,
				Color.LightBlue,
				Color.LightCoral,
				Color.LightCyan,
				Color.LightGoldenrodYellow,
				Color.LightGray,
				Color.LightGreen,
				Color.LightPink,
				Color.LightSalmon,
				Color.LightSeaGreen,
				Color.LightSkyBlue,
				Color.LightSlateGray,
				Color.LightSteelBlue,
				Color.LightYellow,
				Color.Lime,
				Color.LimeGreen,
				Color.Linen,
				Color.Magenta,
				Color.Maroon,
				Color.MediumAquamarine,
				Color.MediumBlue,
				Color.MediumOrchid,
				Color.MediumPurple,
				Color.MediumSeaGreen,
				Color.MediumSlateBlue,
				Color.MediumSpringGreen,
				Color.MediumTurquoise,
				Color.MediumVioletRed,
				Color.MidnightBlue,
				Color.MintCream,
				Color.MistyRose,
				Color.Moccasin,
				Color.NavajoWhite,
				Color.Navy,
				Color.OldLace,
				Color.Olive,
				Color.OliveDrab,
				Color.Orange,
				Color.OrangeRed,
				Color.Orchid,
				Color.PaleGoldenrod,
				Color.PaleGreen,
				Color.PaleTurquoise,
				Color.PaleVioletRed,
				Color.PapayaWhip,
				Color.PeachPuff,
				Color.Peru,
				Color.Pink,
				Color.Plum,
				Color.PowderBlue,
				Color.Purple,
				Color.Red,
				Color.RosyBrown,
				Color.RoyalBlue,
				Color.SaddleBrown,
				Color.Salmon,
				Color.SandyBrown,
				Color.SeaGreen,
				Color.SeaShell,
				Color.Sienna,
				Color.Silver,
				Color.SkyBlue,
				Color.SlateBlue,
				Color.SlateGray,
				Color.Snow,
				Color.SpringGreen,
				Color.SteelBlue,
				Color.Tan,
				Color.Teal,
				Color.Thistle,
				Color.Tomato,
				Color.Transparent,
				Color.Turquoise,
				Color.Violet,
				Color.Wheat,
				Color.White,
				Color.WhiteSmoke,
				Color.Yellow,
				Color.YellowGreen
			};

		protected void Page_Load(object sender, EventArgs e)
		{
			string[] s = new string[2] {"No data", "1"};
			if (Request.QueryString["data"] != null)
				s = Request.QueryString["data"].Split('|');

			ChartEngine wcEng = new ChartEngine();
			if (Request.QueryString["size"] != null)
			{
				string[] size = Request.QueryString["size"].Split('|');
				wcEng.Size = new Size(Convert.ToInt32(size[0]), Convert.ToInt32(size[1]));
			}
			else
				wcEng.Size = new Size(400, 400);
			wcEng.GridLines = GridLines.None;
			wcEng.Padding = 0;
			wcEng.TopPadding = 0;

			ChartCollection wcCharts = new ChartCollection(wcEng);
			wcEng.Charts = wcCharts;

			PieChart slChart = new MyPieChart();
			slChart.Line.Color = Color.Black;
			slChart.DataLabels.Visible = true;
			slChart.DataLabels.ShowXTitle = true;
			slChart.DataLabels.ShowValue = false;
			slChart.DataLabels.ShowLegend = false;
			slChart.Shadow.Visible = true;
			slChart.Explosion = 8;

			for (int i = 0; i < s.Length; i++)
			{
				ChartPoint cp = new ChartPoint();
				cp.XValue = s[i];
				cp.YValue = Convert.ToInt32(s[i + 1]);
				slChart.Data.Add(cp);
				i += 1;
			}
			slChart.Colors = color;

			wcCharts.Add(slChart);
			wcEng.HasChartLegend = false;

			wcEng.Legend.Position = LegendPosition.Right;
			wcEng.Legend.Width = 80;

			MemoryStream memStream = new MemoryStream();
			Bitmap bmp = wcEng.GetBitmap();
			Graphics g = Graphics.FromImage(bmp);
			Pen mypen = new Pen(Color.Black, 1F);
			g.DrawRectangle(mypen, 0, 0, bmp.Width - 1, bmp.Height - 1);
			bmp.Save(memStream, ImageFormat.Png);
			memStream.WriteTo(Response.OutputStream);
			Response.ContentType = "image/png";
			Response.End();

		}

		private class MyPieChart : PieChart
		{
			public RectangleF lastrec;

			public override void Render(Graphics graphics, int x, int y)
			{
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				base.Render(graphics, x, y);
			}

			protected override void DrawDataLabel(
				Graphics graphics,
				string text,
				RectangleF rectangle,
				Pen pen,
				Brush backgroundBrush,
				StringFormat format,
				Brush textBrush)
			{
				Pen drpen = new Pen(Color.Red, 1F);
				Point[] points = new Point[3];
				int offsetpos = 10;
				int xMiddle = (int) ((graphics.ClipBounds.Width - 9)/2);
				int YMiddle = (int) (((graphics.ClipBounds.Height + 6)*-1)/2);
				if (rectangle.Y < YMiddle)
				{
					if (rectangle.X >= xMiddle)
					{
						points[0] = new Point((int) rectangle.X, (int) (rectangle.Y + rectangle.Height));
						rectangle.Y -= offsetpos;
						rectangle.X += offsetpos;
						points[1] = new Point((int) rectangle.X, (int) (rectangle.Y + rectangle.Height));
						points[2] = new Point((int) (rectangle.X + rectangle.Width), (int) (rectangle.Y + rectangle.Height));
					}
					else
					{
						points[0] = new Point((int) (rectangle.X + rectangle.Width), (int) (rectangle.Y + rectangle.Height));
						rectangle.Y -= offsetpos;
						rectangle.X -= offsetpos;
						points[1] = new Point((int) (rectangle.X + rectangle.Width), (int) (rectangle.Y + rectangle.Height));
						points[2] = new Point((int) rectangle.X, (int) (rectangle.Y + rectangle.Height));
					}
				}
				else
				{
					if (rectangle.X >= xMiddle)
					{
						points[0] = new Point((int) rectangle.X, (int) rectangle.Y);
						rectangle.Y += offsetpos;
						rectangle.X += offsetpos;
						points[1] = new Point((int) rectangle.X, (int) (rectangle.Y + rectangle.Height));
						points[2] = new Point((int) (rectangle.X + rectangle.Width), (int) (rectangle.Y + rectangle.Height));
					}
					else
					{
						points[0] = new Point((int) (rectangle.X + rectangle.Width), (int) rectangle.Y);
						rectangle.Y += offsetpos;
						rectangle.X -= offsetpos;
						points[1] = new Point((int) (rectangle.X + rectangle.Width), (int) (rectangle.Y + rectangle.Height));
						points[2] = new Point((int) rectangle.X, (int) (rectangle.Y + rectangle.Height));
					}
				}
				if (!lastrec.IntersectsWith(rectangle))
				{
					graphics.DrawLines(drpen, points);
					base.DrawDataLabel(graphics, text, rectangle, pen, backgroundBrush, format, textBrush);
				}
				lastrec = rectangle;
			}
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
		}

		#endregion
	}
}
