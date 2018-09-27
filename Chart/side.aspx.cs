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
using System.Drawing.Imaging;
using System.IO;
using System.Web.UI;
using WebChart;

namespace Digita.Tustena
{
	public partial class Side_chart : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string[] s = new string[2] {"No data", "0"};
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

			ChartCollection wcCharts = new ChartCollection(wcEng);
			wcEng.Charts = wcCharts;
			wcEng.RenderHorizontally = true;
			wcEng.Border.Color = Color.FromArgb(99, Color.SteelBlue);
			wcEng.ShowXValues = true;
			wcEng.ShowYValues = true;
			wcEng.ChartPadding = 30;
			wcEng.Padding = 1;
			wcEng.TopPadding = 0;


			ColumnChart chart = new ColumnChart();
			chart.Shadow.Visible = false;
			chart.MaxColumnWidth = Convert.ToInt32(wcEng.Size.Height/s.Length);
			chart.Fill.Color = Color.FromArgb(90, Color.SteelBlue);
			chart.Line.Color = Color.SteelBlue;
			chart.Line.Width = 2;
			chart.DataLabels.Visible = true;
			chart.DataLabels.ShowValue = true;

			for (int i = 0; i < s.Length; i++)
			{
				ChartPoint cp = new ChartPoint();
				cp.XValue = s[i];
				cp.YValue = Convert.ToInt32(s[i + 1]);
				chart.Data.Add(cp);
				i += 1;
			}
			wcCharts.Add(chart);

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
