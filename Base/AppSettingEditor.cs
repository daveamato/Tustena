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
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Xml;

namespace Digita.Tustena.Base
{
	public enum ConfigFileType
	{
		WebConfig,
		AppConfig
	}

	public class AppConfig : AppSettingsReader
	{
		public string docName = String.Empty;
		private XmlNode node = null;

		private int configType;

		public int ConfigType
		{
			get { return configType; }
			set { configType = value; }
		}

		public bool SetValue(string key, string value)
		{
			XmlDocument cfgDoc = new XmlDocument();
			loadConfigDoc(cfgDoc);
			node = cfgDoc.SelectSingleNode("//appSettings");

			if (node == null)
			{
				throw new InvalidOperationException("appSettings section not found");
			}

			try
			{
				XmlElement addElem = (XmlElement) node.SelectSingleNode("//add[@key='" + key + "']");
				if (addElem != null)
				{
					addElem.SetAttribute("value", value);
				}
				else
				{
					XmlElement entry = cfgDoc.CreateElement("add");
					entry.SetAttribute("key", key);
					entry.SetAttribute("value", value);
					node.AppendChild(entry);
				}
				saveConfigDoc(cfgDoc, docName);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
		{
			try
			{
				XmlTextWriter writer = new XmlTextWriter(cfgDocPath, null);
				writer.Formatting = Formatting.Indented;
				cfgDoc.WriteTo(writer);
				writer.Flush();
				writer.Close();
				return;
			}
			catch
			{
				throw;
			}
		}

		public bool removeElement(string elementKey)
		{
			try
			{
				XmlDocument cfgDoc = new XmlDocument();
				loadConfigDoc(cfgDoc);
				node = cfgDoc.SelectSingleNode("//appSettings");
				if (node == null)
				{
					throw new InvalidOperationException("appSettings section not found");
				}
				node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));

				saveConfigDoc(cfgDoc, docName);
				return true;
			}
			catch
			{
				return false;
			}
		}


		private XmlDocument loadConfigDoc(XmlDocument cfgDoc)
		{
			if (Convert.ToInt32(ConfigType) == Convert.ToInt32(ConfigFileType.AppConfig))
			{
				docName = ((Assembly.GetEntryAssembly()).GetName()).Name;
				docName += ".exe.config";
			}
			else
			{
				docName = HttpContext.Current.Server.MapPath("web.config");
			}
			cfgDoc.Load(docName);
			return cfgDoc;
		}

	}
}
