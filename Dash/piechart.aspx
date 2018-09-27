<%@ Page Language="C#" contenttype="image/png" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>
<%@ Import Namespace="System.Drawing.Text" %>
<%@ Import Namespace="WebChart" %>
<script runat=server>
protected void Page_Load(object src, EventArgs ev) {

Color[] color = new Color[]{
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

	string[] s = Request.QueryString["data"].Split(',');
	System.Collections.ArrayList ColorArray = new System.Collections.ArrayList();
        ChartEngine wcEng = new ChartEngine();
        wcEng.Size = new Size(400, 400);
 		wcEng.Background.Color = Color.Transparent;
        ChartCollection wcCharts = new ChartCollection(wcEng);
        wcEng.Charts = wcCharts;

        PieChart slChart = new MyPieChart();
        slChart.Line.Color = Color.Black;
        slChart.DataLabels.Visible = false;
        slChart.DataLabels.ShowXTitle = true;
        slChart.DataLabels.ShowValue = true;
        slChart.DataLabels.ShowLegend = false;
        slChart.DataLabels.Separator = ": ";
        slChart.Shadow.Visible = true;
        slChart.Explosion = 8;
        wcEng.Border.Color = Color.White;
        wcEng.GridLines = WebChart.GridLines.None;

	for(int i=0;i<s.Length;i++){
	ChartPoint cp = new ChartPoint();
	cp.XValue=s[i];
	cp.YValue=Convert.ToInt32(s[i+1]);
	slChart.Data.Add(cp);
	i+=1;
	}
        slChart.Colors=color;

        wcCharts.Add(slChart);
        wcEng.HasChartLegend = false;

        //wcEng.Legend.Position = LegendPosition.Right;
        //wcEng.Legend.Border = LegendPosition.Right;
        //wcEng.Legend.Width = 80;

        System.IO.MemoryStream memStream = new System.IO.MemoryStream();
        Bitmap bmp = wcEng.GetBitmap();
        bmp.MakeTransparent(Color.Transparent);
        bmp.Save(memStream, System.Drawing.Imaging.ImageFormat.Png);
        memStream.WriteTo(Response.OutputStream);
        Response.End();
}
class MyPieChart : WebChart.PieChart
		{
			public override void Render(System.Drawing.Graphics graphics, int x, int y)
			{
				graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				base.Render(graphics,x,y);
			}
}
</script>
