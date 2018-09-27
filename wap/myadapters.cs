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
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.MobileControls;
using System.Web.UI.MobileControls.Adapters;

namespace MyAdapters
{
	public class MyFormAdapter:HtmlFormAdapter
	{
		protected override void RenderBodyTag(HtmlMobileTextWriter
			writer, IDictionary attributes)
		{
			writer.WriteBeginTag("body");
			foreach (DictionaryEntry entry in attributes)
			{
				writer.WriteAttribute((string)entry.Key, (string)
					entry.Value);
			}
			writer.WriteAttribute("leftmargin","5");
			writer.WriteAttribute("rightmargin","5");
			writer.WriteLine(">");
		}
	}

	public class MyHtmlObjectListAdapter:HtmlObjectListAdapter
	{
		protected override void RenderItemDetails(HtmlMobileTextWriter writer, ObjectListItem item)
		{
			if (Control.AllFields.Count == 0)
			{
				return;
			}
			if (base.Device.Tables)
			{
				RenderItemDetailsWithTableTags(writer, item);
				return;
			}
			RenderItemDetailsWithoutTableTags(writer, item);
		}

		private void RenderItemDetailsWithTableTags(HtmlMobileTextWriter writer, ObjectListItem item)
		{
			Style style1 = base.Style;
			Style style2 = Control.LabelStyle;
			Style style3 = Control.CommandStyle;
			Color color = (Color)style1[Style.ForeColorKey, true];
			writer.Write("<table border=0 width=\"100%\">\r\n<tr><td colspan=2>");
			writer.BeginStyleContext();
			writer.EnterStyle(style2);
			writer.WriteText(item[Control.LabelFieldIndex], true);
			writer.ExitStyle(style2);
			writer.EndStyleContext();
			writer.Write("</td></tr>\r\n<tr>");
			RenderRule(writer, color, 2);
			IObjectListFieldCollection iObjectListFieldCollection = Control.AllFields;
			int i = 0;
			IEnumerator iEnumerator = iObjectListFieldCollection.GetEnumerator();
			while (iEnumerator.MoveNext())
			{
				ObjectListField objectListField = (ObjectListField)iEnumerator.Current;
				if (objectListField.Visible)
				{
					writer.Write("<tr><td>");
					writer.BeginStyleContext();
					writer.EnterStyle(base.Style);
					writer.WriteText(objectListField.Title, true);
					writer.ExitStyle(base.Style);
					writer.EndStyleContext();
					writer.Write("</td><td>");
					writer.BeginStyleContext();
					writer.EnterStyle(style1);
					if(objectListField.Name=="CALL")
						writer.WriteText(HtmlcheckPhonenumber(item[i]), false);
					else if(objectListField.Name=="MAIL")
						writer.WriteText(HtmlMail(item[i]), false);
					else
						writer.WriteText(item[i], false);
					writer.ExitStyle(style1);
					writer.EndStyleContext();
					writer.Write("</td></tr>\r\n");
				}
				i++;
			}
			RenderRule(writer, color, 2);
			writer.Write("<tr><td colspan=2>");
			writer.BeginStyleContext();
			BooleanOption booleanOption = style3.Font.Italic;
			style3.Font.Italic = BooleanOption.False;
			writer.EnterStyle(style3);
			writer.Write("[&nbsp;");
			writer.ExitStyle(style3);
			style3.Font.Italic = booleanOption;
			writer.EnterStyle(style3);
			iEnumerator = Control.Commands.GetEnumerator();
			while (iEnumerator.MoveNext())
			{
				ObjectListCommand objectListCommand = (ObjectListCommand)iEnumerator.Current;
				RenderPostBackEventAsAnchor(writer, objectListCommand.Name, objectListCommand.Text,style3);
				writer.Write("&nbsp;|&nbsp;");
			}

			string str = (Control.BackCommandText != String.Empty) ? Control.BackCommandText : base.GetDefaultLabel(ControlAdapter.BackLabel);
			RenderPostBackEventAsAnchor(writer, BackToList, str,style3);
			writer.ExitStyle(style3);
			style3.Font.Italic = BooleanOption.False;
			writer.EnterStyle(style3);
			writer.Write("&nbsp;]");
			writer.ExitStyle(style3);
			style3.Font.Italic = booleanOption;
			writer.EndStyleContext();
			writer.Write("</td></tr></table>");
		}

		private void RenderItemDetailsWithoutTableTags(HtmlMobileTextWriter writer, ObjectListItem item)
		{
			Style style1 = base.Style;
			Style style2 = Control.LabelStyle;
			Style style3 = Control.CommandStyle;
			writer.EnterStyle(style2);
			writer.WriteText(item[Control.LabelFieldIndex], true);
			writer.ExitStyle(style2, true);
			IObjectListFieldCollection iObjectListFieldCollection = Control.AllFields;
			int i = 0;
			bool flag = style1.Font.Bold == BooleanOption.True;
			writer.EnterStyle(style1);
			IEnumerator iEnumerator = iObjectListFieldCollection.GetEnumerator();
			while (iEnumerator.MoveNext())
			{
				ObjectListField objectListField = (ObjectListField)iEnumerator.Current;
				if (objectListField.Visible)
				{
					if (!flag)
					{
						writer.Write("<b>");
					}
					writer.WriteText(String.Concat(objectListField.Title, ":"), true);
					if (!flag)
					{
						writer.Write("</b>");
					}
					writer.Write("&nbsp;");
					if(objectListField.Name=="CALL")
						writer.WriteText(HtmlcheckPhonenumber(item[i]), true);
					else if(objectListField.Name=="MAIL")
						writer.WriteText(HtmlMail(item[i]), true);
					else
						writer.WriteText(item[i], true);
					writer.WriteBreak();
				}
				i++;
			}

			writer.ExitStyle(style1);
			BooleanOption booleanOption = style3.Font.Italic;
			style3.Font.Italic = BooleanOption.False;
			writer.EnterStyle(style3);
			writer.Write("[&nbsp;");
			writer.ExitStyle(style3);
			style3.Font.Italic = booleanOption;
			writer.EnterStyle(style3);
			iEnumerator = Control.Commands.GetEnumerator();
			while (iEnumerator.MoveNext())
			{
				ObjectListCommand objectListCommand = (ObjectListCommand)iEnumerator.Current;
				RenderPostBackEventAsAnchor(writer, objectListCommand.Name, objectListCommand.Text,style3);
				writer.Write("&nbsp;|&nbsp;");
			}

			string str = (Control.BackCommandText != String.Empty) ? Control.BackCommandText : base.GetDefaultLabel(ControlAdapter.BackLabel);
			RenderPostBackEventAsAnchor(writer, BackToList, str,style3);
			writer.ExitStyle(style3);
			style3.Font.Italic = BooleanOption.False;
			writer.EnterStyle(style3);
			writer.Write("&nbsp;]");
			writer.ExitStyle(style3, Control.BreakAfter);
			style3.Font.Italic = booleanOption;
		}

		private void RenderPostBackEventAsAnchor(HtmlMobileTextWriter writer, string argument, string linkText, Style style)
		{
			writer.EnterFormat(style);
			writer.WriteBeginTag("a");
			base.RenderPostBackEventAsAttribute(writer, "href", argument);
			writer.Write(">");
			writer.WriteText(linkText, true);
			writer.WriteEndTag("a");
			writer.ExitFormat(style);
		}


		private void RenderRule(HtmlMobileTextWriter writer, Color foreColor, int columnSpan)
		{
			writer.Write("<tr><td colspan=");
			writer.Write(columnSpan.ToString());
			writer.Write(" bgcolor=\"");
			writer.Write((foreColor == Color.Empty) ? "#000000" : ColorTranslator.ToHtml(foreColor));
			writer.Write("\"></td></tr>");
		}


		private string HtmlcheckPhonenumber(string numero)
		{
			if (numero != null && numero != String.Empty)
			{
				Match match = Regex.Match(numero, "[\\+#]?[ \\d\\(\\)\\.-]*\\d[ \\d\\(\\)\\.-]*");
				if (!match.Success || match.Index != 0 || match.Length != numero.Length)
				{
					return numero;
				}
				char[] chs = new char[(uint)numero.Length];
				int i = 0;
				for (int j = 0; j < numero.Length; j++)
				{
					char ch = numero[j];
					if (ch >= '0' && ch <= '9' || ch == '+')
					{
						chs[i] = ch;
						i++;
					}
				}
					return "<a href=\"tel:"+new String(chs, 0, i)+"\" title=\"Call\">"+numero+"</a>";
			}
			return numero;
		}
		private string HtmlMail(string address)
		{
			return "<a href=\"mailto:"+address+"\">"+address+"</a>";

		}

	}


	public class MyWmlObjectListAdapter:WmlObjectListAdapter
	{
		protected override void RenderItemDetails(WmlMobileTextWriter writer, ObjectListItem item)
		{
			string str1 = (Control.BackCommandText != String.Empty) ? Control.BackCommandText : base.GetDefaultLabel(ControlAdapter.BackLabel);
			string str2 = (str1.Length > base.Device.MaximumSoftkeyLabelLength) ? null : str1;
			Style style = Control.LabelStyle;
			writer.EnterStyle(style);
			writer.RenderText(item[Control.LabelFieldIndex], true);
			writer.ExitStyle(style);
			writer.EnterStyle(base.Style);
			IObjectListFieldCollection iObjectListFieldCollection = Control.AllFields;
			int i = 0;
			IEnumerator iEnumerator = iObjectListFieldCollection.GetEnumerator();
			while (iEnumerator.MoveNext())
			{
				ObjectListField objectListField = (ObjectListField)iEnumerator.Current;
				if (objectListField.Visible)
				{
					if(objectListField.Name=="CALL")
			writer.RenderText(String.Format("{0}: {1}", objectListField.Title, WmlcheckPhonenumber(item[i])), true,false);
					else if(objectListField.Name=="MAIL")
						writer.RenderText(String.Format("{0}: {1}", objectListField.Title, WmlMail(item[i])), true,false);
					else
						writer.RenderText(String.Format("{0}: {1}", objectListField.Title, item[i]), true);
				}
				i++;
			}

			base.RenderPostBackEvent(writer, "__back", str2, true, str1, true);
			writer.ExitStyle(base.Style);
		}

		private string WmlcheckPhonenumber(string numero)
		{
			if (numero != null && numero != String.Empty)
			{
				Match match = Regex.Match(numero, "[\\+#]?[ \\d\\(\\)\\.-]*\\d[ \\d\\(\\)\\.-]*");
				if (!match.Success || match.Index != 0 || match.Length != numero.Length)
				{
					return numero;
				}
				char[] chs = new char[(uint)numero.Length];
				int i = 0;
				for (int j = 0; j < numero.Length; j++)
				{
					char ch = numero[j];
					if (ch >= '0' && ch <= '9' || ch == '+')
					{
				chs[i] = ch;
						i++;
					}
				}
				return "<a title=\"Call\" href=\"wtai://wp/mc;"+new String(chs, 0, i)+"!resultvar\">"+numero+"</a>";
			}
			return numero;
		}
		private string WmlMail(string address)
		{
			return "<a href=\"mailto:"+address+"\">"+address+"</a>";

		}
	}


}
