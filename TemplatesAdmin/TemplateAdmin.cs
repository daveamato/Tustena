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
using System.Data;
using System.IO;
using System.Xml;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public class TemplateAdmin
	{
		private string templateName;
		private string lang;
		private string applicationPath;
		private bool global = true;

		[DefaultValue(true)]
		public bool Global
		{
			set { global = value; }
		}

		public string ApplicationPath
		{
			set { applicationPath = value; }
		}

		public string TemplateName
		{
			set { templateName = value; }
		}

		public string Language
		{
			set { lang = value; }
		}

		public TemplateAdmin()
		{
		}

		public string GetTemplate()
		{
			return string.Empty;
		}

		public string GetLogo()
		{
			return GetLogo(templateName, lang);
		}

		public string GetTemplate(string templatename, string language)
		{
			{
				string body = String.Empty;
				object bodyObj;
					bodyObj = DatabaseConnection.SqlScalartoObj("SELECT BODY FROM TEMPLATES WHERE TEMPLATENAME='" + templatename + "' AND LANG='EN'");

				if (bodyObj is string && ((body=bodyObj.ToString()).Length>0))
				{
					TextReader r = new StringReader(body);
					DataSet content = new DataSet();
					content.ReadXml(r);

					string template;
					StreamReader objReader;

					if (global)
					{
						if (Directory.Exists(applicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + language))
							objReader = new StreamReader(applicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + language + Path.DirectorySeparatorChar + templatename + ".htm");
						else
							objReader = new StreamReader(applicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + "en" + Path.DirectorySeparatorChar + templatename + ".htm");
					}
					else
						objReader = new StreamReader(applicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + templatename + ".htm");

					template = objReader.ReadToEnd();
					objReader.Close();
					for (int i = 0; i < content.Tables[0].Rows.Count; i++)
					{
						template = template.Replace("[content" + i.ToString() + "]", content.Tables[0].Rows[i][0].ToString().Replace(Environment.NewLine, "<br>"));
					}
					return template;
				}
				else
					return "0"; // 0 nessun dato
			}
		}

		public string GetLogo(string templatename, string language)
		{
			try
			{
				string logo;
				logo = DatabaseConnection.SqlScalar(String.Format("SELECT LOGO FROM TEMPLATES WHERE TEMPLATENAME='{0}' AND LANG='{1}'", templatename, language));
				if (StaticFunctions.IsBlank(logo))
				{
					return "/logos/logo.gif";
				}
				else
				{
					return "/logos/" + logo;
				}
			}
			catch
			{
				return "/logos/logo.gif";
			}

		}

		public string GetXMLBody(string[] content)
		{
			TextWriter ms = new StringWriter();
			XmlTextWriter myXmlTextWriter = new XmlTextWriter(ms);

			myXmlTextWriter.Formatting = Formatting.Indented;
			myXmlTextWriter.WriteStartDocument(false);
			myXmlTextWriter.WriteStartElement("Contents", null);

			foreach (string s in content)
			{
				myXmlTextWriter.WriteStartElement("Content");
				myXmlTextWriter.WriteCData(s);
				myXmlTextWriter.WriteEndElement();
			}

			myXmlTextWriter.WriteEndElement();
			myXmlTextWriter.Flush();
			myXmlTextWriter.Close();

			return ms.ToString();
		}
	}
}
