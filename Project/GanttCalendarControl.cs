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
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace Digita.Tustena.Project
{
	[DefaultProperty("DataTableCalendar"), ToolboxData("<{0}:GanttCalendarControl runat=server></{0}:GanttCalendarControl>")]
	public class GanttCalendarControl : WebControl
	{
		private string m_xmlData, m_blockColor, m_toggleColor, m_cellWidth, m_cellHeight, m_blankGifPath;
		private int m_year, m_quarter;
		string[] m_names;

		public GanttCalendarControl()
		{
			m_names = new string[3];
		}

#region Public Properties

		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public string XMLData
		{
			get
			{
				return m_xmlData;
			}

			set
			{
				m_xmlData = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue(1)]
		public int Quarter
		{
			get
			{
				return m_quarter;
			}

			set
			{
				m_quarter = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("")]
		public int Year
		{
			get
			{
				return m_year;
			}

			set
			{
				m_year = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("red")]
		public string BlockColor
		{
			get
			{
				return m_blockColor;
			}

			set
			{
				m_blockColor = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("15")]
		public int CellWidth
		{
			get
			{
				return int.Parse(m_cellWidth);
			}

			set
			{
				m_cellWidth = value.ToString();
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("15")]
		public int CellHeight
		{
			get
			{
				return int.Parse(m_cellHeight);
			}

			set
			{
				m_cellHeight = value.ToString();
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("#dcdcdc")]
		public string ToggleColor
		{
			get
			{
				return m_toggleColor;
			}

			set
			{
				m_toggleColor = value;
			}
		}

		[Bindable(true), Category("Appearance"), DefaultValue("#dcdcdc")]
		public string BlankGifPath
		{
			get
			{
				return m_blankGifPath;
			}

			set
			{
				m_blankGifPath = value;
			}
		}
		#endregion

		#region Event Handling

		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
            if (!base.Page.ClientScript.IsStartupScriptRegistered("EventCalendar"))
			{
                base.Page.ClientScript.RegisterStartupScript(this.GetType(), "EventCalendar",
					"<script>" +
					"function ResizeTables()" +
					"{" +
					"document.getElementById('divcal').style.width = '1px';" +
					"document.getElementById('divcal').style.width = document.getElementById('tblcal').clientWidth + 'px';" +
					"};" +
					"</script>");

                base.Page.ClientScript.RegisterStartupScript(this.GetType(), "EventCalendarOnLoad",
					"<SCRIPT FOR=window EVENT=onload>" +
					"ResizeTables();" +
					"</script>");

                base.Page.ClientScript.RegisterStartupScript(this.GetType(), "EventCalendarOnResize",
					"<SCRIPT FOR=window EVENT=onresize>" +
					"ResizeTables();" +
					"</script>");
			}
		}

		protected override void Render(HtmlTextWriter writer)
		{

			writer.AddAttribute("border", "0");
			writer.AddAttribute("cellSpacing","0");
			writer.AddAttribute("cellPadding","0");
			writer.AddAttribute("width", "100%");
			writer.RenderBeginTag("table");
			writer.RenderBeginTag("tr");


			writer.AddAttribute("valign", "top");
			writer.AddAttribute("align", "right");
			writer.AddAttribute("height",m_cellWidth);
			writer.RenderBeginTag("td nowrap");

			RenderLeftHandPane(writer);

			writer.RenderEndTag();

			writer.AddAttribute("id", "tblcal");
			writer.AddAttribute("valign", "top");
			writer.AddAttribute("align", "left");
			writer.AddAttribute("height",m_cellWidth);
			writer.AddAttribute("width", "100%");
			writer.RenderBeginTag("td nowrap");

			RenderRightHandPane(writer);

			writer.RenderEndTag();
			writer.RenderEndTag();
			writer.RenderEndTag();
		}
		#endregion

		#region Helper Functions
		private void RenderLeftHandPane(HtmlTextWriter writer)
		{

			string blocktext, grouptext;

			writer.AddAttribute("border", "1");
			writer.AddAttribute("style", "FONT-SIZE: " + Font.Size + ";FONT-FAMILY: " + Font.Name + ";BORDER-COLLAPSE: collapse");
			writer.AddAttribute("borderColor","#000000");
			writer.AddAttribute("cellSpacing","0");
			writer.AddAttribute("cellPadding","0");
			writer.RenderBeginTag("table");

			writer.RenderBeginTag("tr");
			writer.AddAttribute("height",m_cellWidth);
			writer.RenderBeginTag("td");
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();

			writer.RenderBeginTag("tr");
			writer.AddAttribute("height",m_cellWidth);
			writer.RenderBeginTag("td");
			writer.Write("&nbsp;");
			writer.RenderEndTag();
			writer.RenderEndTag();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(m_xmlData);

			XmlNodeList xmlRows = xmlDoc.SelectNodes("//group");
			foreach(XmlNode xmlRow in xmlRows)
			{
				grouptext = xmlRow.SelectSingleNode("name").InnerText;
				writer.RenderBeginTag("tr");
				writer.AddAttribute("bgcolor", m_toggleColor);
				writer.AddAttribute("height", m_cellHeight);
				writer.RenderBeginTag("td nowrap");
				writer.Write("&nbsp;<b>" + grouptext + "</b>&nbsp;");
				writer.RenderEndTag();
				writer.RenderEndTag();

				XmlNodeList xmlBlocks = xmlRow.SelectNodes("block");
				if(xmlBlocks.Count==0)
				{
					writer.RenderBeginTag("tr");

					blocktext = xmlRow.SelectSingleNode("name").InnerText;

					writer.AddAttribute("height", m_cellHeight);
					writer.RenderBeginTag("td nowrap");
					writer.Write("&nbsp;" + blocktext + "&nbsp;<img src='" + m_blankGifPath + "' height=12 width=1 />");
					writer.RenderEndTag();

					writer.RenderEndTag();
				}
				else
					foreach(XmlNode xmlBlock in xmlBlocks)
				{
					writer.RenderBeginTag("tr");

					blocktext = xmlBlock.SelectSingleNode("name").InnerText;

					writer.AddAttribute("height", m_cellHeight);
					writer.RenderBeginTag("td nowrap");
					writer.Write("&nbsp;" + blocktext + "&nbsp;<img src='" + m_blankGifPath + "' height=12 width=1 />");
					writer.RenderEndTag();

					writer.RenderEndTag();
				}
			}
			writer.RenderEndTag();
		}

		private void RenderRightHandPane(HtmlTextWriter writer)
		{
			string startdate, enddate, dayname, href, blocktext, blockcolor;
			int month;
			bool week = false;
			QuarterHelper quarter = new QuarterHelper(m_year, m_quarter);

			writer.AddAttribute("style", "width:1px; overflow-x:scroll;");
			writer.AddAttribute("id", "divcal");
			writer.RenderBeginTag("div");

			writer.AddAttribute("border", "1");
			writer.AddAttribute("style", "FONT-SIZE: " + Font.Size + ";FONT-FAMILY: " + Font.Name + ";BORDER-COLLAPSE: collapse");
			writer.AddAttribute("borderColor","#000000");
			writer.AddAttribute("cellSpacing","0");
			writer.AddAttribute("cellPadding","0");
			writer.RenderBeginTag("table");

			writer.RenderBeginTag("tr");
			for(int i=1;i<=3;i++)
			{
				month = i+(3*(quarter.QuarterIndex-1));
				writer.AddAttribute("align", "center");
				writer.AddAttribute("colspan", quarter.TotalDaysInMonth(month).ToString());
				writer.RenderBeginTag("td");
				writer.AddAttribute("height", m_cellHeight);
				writer.Write(quarter.GetMonthName(i));
				writer.RenderEndTag();
			};
			writer.RenderEndTag();

			writer.RenderBeginTag("tr");
			for(int i=(3*quarter.QuarterIndex-2);i<=(3*quarter.QuarterIndex);i++)
			{
				for(int j=1;j<=quarter.TotalDaysInMonth(i);j++)
				{
					dayname = quarter.GetDayName(i,j);
					if(dayname == "M") week = !week;

					writer.AddAttribute("align", "center");
					writer.AddAttribute("width", m_cellWidth);
					writer.AddAttribute("height", m_cellHeight);
					if(week) writer.AddAttribute("bgcolor", m_toggleColor);
					writer.RenderBeginTag("td");
					writer.Write(dayname);
					writer.RenderEndTag();
				}
			}
			writer.RenderEndTag();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(m_xmlData);

			XmlNodeList xmlRows = xmlDoc.SelectNodes("//group");
			int crow = 0;
			string[] colors = new string[]{"#ff0000","#00ff00","#0000ff"};
			foreach(XmlNode xmlRow in xmlRows)
			{

				writer.RenderBeginTag("tr");
				writer.AddAttribute("colspan", (quarter.Days).ToString());
				writer.AddAttribute("width", ((quarter.Days) * int.Parse(m_cellWidth)).ToString());
				writer.AddAttribute("height", m_cellHeight);
				writer.RenderBeginTag("td");
				writer.Write("<img src='" + m_blankGifPath + "' height=1 width=" + ((quarter.Days) * int.Parse(m_cellWidth)).ToString() + "/>&nbsp");
				writer.RenderEndTag();
				writer.RenderEndTag();

				XmlNode node = xmlRow.SelectSingleNode("blockcolor");
				if(node != null)
				{
					blockcolor = node.InnerText;
					if(blockcolor.Length==0)
						blockcolor=colors[crow++];
				}
				else
				{
					blockcolor = m_blockColor;
				};


				XmlNodeList xmlBlocks = xmlRow.SelectNodes("block");
				if(xmlBlocks.Count==0)
				{
					startdate = xmlRow.SelectSingleNode("StartDate").InnerText;
					node = xmlRow.SelectSingleNode("EndDate");
					if(node != null)
						enddate = node.InnerText;
					else
						enddate = DateTime.Parse(startdate).AddYears(10).ToString();
					href = "";//xmlRow.SelectSingleNode("href").InnerText;
					blocktext = xmlRow.SelectSingleNode("name").InnerText;
					FillDays(quarter, startdate, enddate, writer, blockcolor, href, blocktext);

				}
				else
				foreach(XmlNode xmlBlock in xmlBlocks)
				{
					startdate = xmlBlock.SelectSingleNode("StartDate").InnerText;
					node = xmlBlock.SelectSingleNode("EndDate");
					if(node != null)
						enddate = node.InnerText;
					else
						enddate = DateTime.Parse(startdate).AddYears(10).ToString();
					href = xmlBlock.SelectSingleNode("href").InnerText;
					blocktext = xmlBlock.SelectSingleNode("name").InnerText;

					FillDays(quarter, startdate, enddate, writer, blockcolor, href, blocktext);
				}
			}
			writer.RenderEndTag(); // close table tag
			writer.RenderEndTag(); // close div tag
		}

		private void FillDays(QuarterHelper quarter, string startdate, string enddate, HtmlTextWriter writer, string blockcolor, string href, string blocktext)
		{
			int endindex;
			int startindex;
			string strHTML;
			int imagewidth;
			writer.RenderBeginTag("tr");
			startindex = quarter.getColumnIndex(startdate);
			endindex = quarter.getColumnIndex(enddate);
			for(int i=0;i<startindex;i++)
			{
				writer.AddAttribute("width", m_cellWidth);
				writer.AddAttribute("height", m_cellHeight);
				writer.RenderBeginTag("td");
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			};

			writer.AddAttribute("colspan", (endindex-startindex+1).ToString());
			writer.AddAttribute("bgColor", blockcolor);
			writer.AddAttribute("width", m_cellWidth);
			writer.AddAttribute("height", m_cellHeight);
			writer.RenderBeginTag("td nowrap");
			if(href != string.Empty)
			{

				imagewidth = (endindex-startindex+1)*int.Parse(m_cellWidth);

				strHTML =
					@"<a href='" +
						href +
						@"'><img src='" + m_blankGifPath + "' border=0 width=" +
						imagewidth +
						@" height=" + m_cellHeight +
						@" alt='" + blocktext +
						@"'/></a>";

				writer.Write(strHTML);
			}
			else
			{
				writer.Write("&nbsp;");
			}
			writer.RenderEndTag();


			for(int i=endindex;i<quarter.Days-1;i++)
			{
				writer.AddAttribute("width", m_cellWidth);
				writer.AddAttribute("height", m_cellHeight);
				writer.RenderBeginTag("td");
				writer.Write("&nbsp;");
				writer.RenderEndTag();
			};
			writer.RenderEndTag();
		}

		#endregion

		#region Helper Classes
		private class QuarterHelper
		{
			private int m_year;
			private int m_quarter;
			private string[] m_names = new string[3];
			private int m_NoOfDays;

			public QuarterHelper()
			{}

			public QuarterHelper(int year, int quarter)
			{
				m_quarter = quarter;
				m_year = year;
				m_names = getQuarterNames();
				m_NoOfDays = getDaysInQuarter(m_year, m_quarter);
			}

			public int Year
			{
				get
				{
					return m_year;
				}
			}

			public int QuarterIndex
			{
				get
				{
					return m_quarter;
				}
			}

			public string[] Names
			{
				get
				{
					return m_names;
				}
			}

			public int Days
			{
				get
				{
					return m_NoOfDays;
				}
			}

			public string GetMonthName(int i)
			{
				return m_names[i-1];
			}

			public int TotalDays()
			{
				int retval = 0;
				switch(m_quarter)
				{
					case 1:
						retval = DateTime.DaysInMonth(m_year, 1);
						retval += DateTime.DaysInMonth(m_year, 2);
						retval += DateTime.DaysInMonth(m_year, 3);
						break;
					case 2:
						retval = DateTime.DaysInMonth(m_year, 4);
						retval += DateTime.DaysInMonth(m_year, 5);
						retval += DateTime.DaysInMonth(m_year, 6);
						break;
					case 3:
						retval = DateTime.DaysInMonth(m_year, 7);
						retval += DateTime.DaysInMonth(m_year, 8);
						retval += DateTime.DaysInMonth(m_year, 9);
						break;
					case 4:
						retval = DateTime.DaysInMonth(m_year, 10);
						retval += DateTime.DaysInMonth(m_year, 11);
						retval += DateTime.DaysInMonth(m_year, 12);
						break;
				}
				return retval;
			}

			public int TotalDaysInMonth(int i)
			{
				return DateTime.DaysInMonth(m_year, i);
			}

			public string GetDayName(int month, int day)
			{
				string retval = string.Empty;
				DateTime d = new DateTime(m_year, month, day);
				switch(d.DayOfWeek)
				{
					case DayOfWeek.Monday: retval = "M"; break;
					case DayOfWeek.Tuesday: retval = "T"; break;
					case DayOfWeek.Wednesday: retval = "W"; break;
					case DayOfWeek.Thursday: retval = "T"; break;
					case DayOfWeek.Friday: retval = "F"; break;
					case DayOfWeek.Saturday: retval = "S"; break;
					case DayOfWeek.Sunday: retval = "S"; break;
				}
				return retval;
			}

			private string[] getQuarterNames()
			{
				string[] retval = new string[3];

				switch(m_quarter)
				{
					case 1:
						retval[0] = "January";
						retval[1] = "February";
						retval[2] = "March";
						break;
					case 2:
						retval[0] = "April";
						retval[1] = "May";
						retval[2] = "June";
						break;
					case 3:
						retval[0] = "July";
						retval[1] = "August";
						retval[2] = "September";
						break;
					case 4:
						retval[0] = "October";
						retval[1] = "November";
						retval[2] = "December";
						break;
				}

				return retval;

			}

			private int getDaysInQuarter(int year, int quarter)
			{
				DateTime dtS, dtE;
				if(quarter<4)
				{
					dtS = new DateTime(year, (3*quarter-2), 1);
					dtE = new DateTime(year, (3*quarter-2) + 3, 1);
				}
				else
				{
					dtS = new DateTime(year, (3*quarter-2), 1);
					dtE = new DateTime(year+1, 1, 1);
				}

				TimeSpan ts = new TimeSpan(dtE.Subtract(dtS).Ticks);
				return ts.Days;
			}

			public int getColumnIndex(string day)
			{
				DateTime dt = DateTime.Parse(day);
				int offset = 0;
				int retval = 0;
				for(int i=1;i<m_quarter;i++)
				{
					offset += getDaysInQuarter(m_year, i);
				}
				retval = (dt.DayOfYear - 1) - offset;
				if(retval<0) retval = 0;
				if(retval>getDaysInQuarter(m_year, m_quarter)) retval = getDaysInQuarter(m_year, m_quarter);
				return retval;

			}
		}
		#endregion

	}
}
