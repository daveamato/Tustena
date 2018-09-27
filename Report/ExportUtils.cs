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
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Digita.Tustena.DataSetTransformation
{
	public class ExportUtils
	{
		public string XSLTPath = "/XSLT/";

		public string GetADORecordSet(DataSet ds, HttpContext context)
		{
			string transformedDataSet = TransformToString(ds.GetXml(), context.Request.MapPath(XSLTPath) + "ADORecordset.xsl", true);

			MemoryStream aMemStr = new MemoryStream();
			XmlTextWriter xwriter = new XmlTextWriter(aMemStr, Encoding.Default);

			WriteADONamespaces(ref xwriter);

			WriteSchemaElement(ds, ds.Tables[0].TableName, ref xwriter);

			RewriteFullElements(ref xwriter, transformedDataSet);

			xwriter.Flush();
			xwriter.Close();

			string strXml = Encoding.UTF8.GetString(aMemStr.ToArray());
			return strXml;
		}


		private void RewriteFullElements(ref XmlTextWriter wrt, string aDOXmlString)
		{
			XmlTextReader rdr = new XmlTextReader(aDOXmlString, XmlNodeType.Document, null);

			rdr.MoveToContent();

			while (rdr.ReadState != ReadState.EndOfFile)
			{
				if (rdr.Name == "s:Schema")
				{
					wrt.WriteNode(rdr, false);
					wrt.Flush();
				}
				else if (rdr.Name == "z:row" && rdr.NodeType == XmlNodeType.Element)
				{
					wrt.WriteStartElement("z", "row", "#RowsetSchema");
					rdr.MoveToFirstAttribute();
					wrt.WriteAttributes(rdr, true);
					wrt.Flush();
				}
				else if (rdr.Name == "z:row" && rdr.NodeType == XmlNodeType.EndElement)
				{
					wrt.WriteEndElement();
					wrt.Flush();
				}
				else if (rdr.Name == "rs:data" && rdr.NodeType == XmlNodeType.Element)
				{
					wrt.WriteStartElement("rs", "data", "urn:schemas-microsoft-com:rowset");
				}
				else if (rdr.Name == "rs:data" && rdr.NodeType == XmlNodeType.EndElement)
				{
					wrt.WriteEndElement();
					wrt.Flush();
				}

				rdr.Read();
			}
			wrt.WriteEndElement();
			wrt.Flush();
		}


		private void WriteADONamespaces(ref XmlTextWriter writer)
		{
			writer.WriteProcessingInstruction("xml", "version='1.0' encoding='ISO-8859-1'");

			writer.WriteStartElement("", "xml", "");
			writer.WriteAttributeString("xmlns", "s", null, "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			writer.WriteAttributeString("xmlns", "dt", null, "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882");
			writer.WriteAttributeString("xmlns", "rs", null, "urn:schemas-microsoft-com:rowset");
			writer.WriteAttributeString("xmlns", "z", null, "#RowsetSchema");
			writer.Flush();
		}


		private void WriteSchemaElement(DataSet ds, string dbname, ref XmlTextWriter writer)
		{
			writer.WriteStartElement("s", "Schema", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			writer.WriteAttributeString("id", "RowsetSchema");

			writer.WriteStartElement("s", "ElementType", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			writer.WriteAttributeString("name", "", "row");
			writer.WriteAttributeString("content", "", "eltOnly");
			writer.WriteAttributeString("rs", "updatable", "urn:schemas-microsoft-com:rowset", "true");

			WriteSchema(ds, dbname, ref writer);

			writer.WriteFullEndElement();

			writer.WriteFullEndElement();
			writer.Flush();
		}


		private void WriteSchema(DataSet ds, string dbname, ref XmlTextWriter writer)
		{
			Int32 i = 1;

			foreach (DataColumn dc in ds.Tables[0].Columns)
			{
				dc.ColumnMapping = MappingType.Attribute;

				writer.WriteStartElement("s", "AttributeType", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");

				writer.WriteAttributeString("name", "", XmlConvert.EncodeName(dc.ColumnName));
				writer.WriteAttributeString("rs", "number", "urn:schemas-microsoft-com:rowset", i.ToString());

				writer.WriteStartElement("s", "datatype", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
				switch (dc.DataType.ToString())
				{
					case ("System.String"):
					{
						writer.WriteAttributeString("dt", "type", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "string");
						writer.WriteAttributeString("dt", "maxlength", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "255");
						writer.WriteAttributeString("rs", "maybenull", "urn:schemas-microsoft-com:rowset", dc.AllowDBNull.ToString());
						break;
					}
					case ("System.Int16"):
					case ("System.Int32"):
					{
						writer.WriteAttributeString("dt", "type", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "number");
						writer.WriteAttributeString("rs", "dbtype", "urn:schemas-microsoft-com:rowset", "numeric");
						writer.WriteAttributeString("dt", "maxlength", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "19");
						writer.WriteAttributeString("rs", "scale", "urn:schemas-microsoft-com:rowset", "0");
						writer.WriteAttributeString("rs", "precision", "urn:schemas-microsoft-com:rowset", "9");
						writer.WriteAttributeString("rs", "fixedlength", "urn:schemas-microsoft-com:rowset", "true");
						writer.WriteAttributeString("rs", "maybenull", "urn:schemas-microsoft-com:rowset", dc.AllowDBNull.ToString());
						break;
					}
					case ("System.DateTime"):
					{
						writer.WriteAttributeString("dt", "type", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "dateTime");
						writer.WriteAttributeString("rs", "dbtype", "urn:schemas-microsoft-com:rowset", "variantdate");
						writer.WriteAttributeString("dt", "maxlength", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "16");
						writer.WriteAttributeString("rs", "fixedlength", "urn:schemas-microsoft-com:rowset", dc.AllowDBNull.ToString());
						break;
					}
				}
				writer.WriteEndElement();

				writer.WriteEndElement();
				writer.Flush();
				i++;
			}

			writer.WriteStartElement("s", "AttributeType", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			writer.WriteAttributeString("name", "", "TotalCount");
			writer.WriteAttributeString("rs", "number", "urn:schemas-microsoft-com:rowset", i.ToString());

			writer.WriteStartElement("s", "datatype", "uuid:BDC6E3F0-6DA3-11d1-A2A3-00AA00C14882");
			writer.WriteAttributeString("dt", "type", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "number");
			writer.WriteAttributeString("rs", "dbtype", "urn:schemas-microsoft-com:rowset", "numeric");
			writer.WriteAttributeString("dt", "maxlength", "uuid:C2F41010-65B3-11d1-A29F-00AA00C14882", "19");
			writer.WriteAttributeString("rs", "scale", "urn:schemas-microsoft-com:rowset", "0");
			writer.WriteAttributeString("rs", "precision", "urn:schemas-microsoft-com:rowset", "9");
			writer.WriteAttributeString("rs", "fixedlength", "urn:schemas-microsoft-com:rowset", "true");
			writer.WriteAttributeString("rs", "maybenull", "urn:schemas-microsoft-com:rowset", "false");
			writer.WriteEndElement();

			writer.WriteEndElement();
			writer.Flush();

		}


		private string GetDatatype(string dtype)
		{
			switch (dtype)
			{
				case "System.Int32":
				case "System.Int16":
					return "int";
				case "System.DateTime":
					return "dateTime";
				default:
					return "string";
			}
		}

		public enum DataSetExportType
		{
			XML,
			ExcelXML,
			TabDelimited,
			CommaDelimited,
			ADORecordSet
		}


		public void ExportDataSet(DataSet ds, string fileName, DataSetExportType type, HttpContext context)
		{

			switch (type)
			{
				case DataSetExportType.ExcelXML:
				{
					context.Response.Clear();
					context.Response.ClearHeaders();
					context.Response.ContentType = "Application/x-msexcel";
					context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xls");
					context.Response.Output.Write(TransformToString(
						ds.GetXml(),
						context.Request.MapPath(XSLTPath) + type.ToString() + ".xsl",
						false));
					break;
				}
				case DataSetExportType.CommaDelimited:
				{
					context.Response.ClearHeaders();
					context.Response.ContentType = "application/vnd.ms-excel";
					context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".csv");
					context.Response.Output.Write(TransformToString(
						ds.GetXml(),
						context.Request.MapPath(XSLTPath) + type.ToString() + ".xsl",
						true));
					break;
				}
				case DataSetExportType.TabDelimited:
				{
					context.Response.ClearHeaders();
					context.Response.ContentType = "Application/x-msexcel";
					context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".txt");
					context.Response.Output.Write(TransformToString(
						ds.GetXml(),
						context.Request.MapPath(XSLTPath) + type.ToString() + ".xsl",
						true));
					break;
				}
				case DataSetExportType.XML:
				{
					context.Response.ClearHeaders();
					context.Response.ContentType = "text/xml";
					context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xml");
					context.Response.Output.Write(ds.GetXml());
					break;
				}
				case DataSetExportType.ADORecordSet:
				{
					context.Response.ClearHeaders();
					context.Response.ContentType = "text/xml";
					context.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xml");
					context.Response.Output.Write(GetADORecordSet(ds, context));
					break;
				}
			}
		}

		public string TransformToString(string xmlData, string xslFilePath)
		{
			return TransformToString(xmlData, xslFilePath, true);
		}

		public string TransformToString(string xmlData, string xslFilePath, bool omitXMLDeclartion)
		{
            XslCompiledTransform xslt = new XslCompiledTransform();

			xslt.Load(xslFilePath);

			XmlTextReader xr = new XmlTextReader(new StringReader(xmlData));

			XPathDocument mydata = new XPathDocument(xr);

			MemoryStream aMemStr = new MemoryStream();

			XmlWriter writer = new XmlTextWriter(aMemStr, null);

            xslt.Transform(mydata, null, writer);
			writer.Close();

			string strXml = Encoding.UTF8.GetString(aMemStr.ToArray());

			return (omitXMLDeclartion) ? strXml : "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + strXml;
		}


		public MemoryStream TransformToMemoryStream(string xmlData, string xslFilePath)
		{
            XslCompiledTransform xslt = new XslCompiledTransform();

			xslt.Load(xslFilePath);

			XmlTextReader xr = new XmlTextReader(new StringReader(xmlData));

			XPathDocument mydata = new XPathDocument(xr);

			MemoryStream aMemStr = new MemoryStream();

			xslt.Transform(mydata, null, aMemStr);

			return aMemStr;
		}

		public MemoryStream TransformDataSet(DataSet ds, string xstFilePath)
		{
			MemoryStream instream = new MemoryStream();
			MemoryStream outstream = new MemoryStream();
			ds.WriteXml(instream, XmlWriteMode.IgnoreSchema);

			instream.Position = 0;
            XslCompiledTransform xslt = new XslCompiledTransform();
			xslt.Load(xstFilePath);

			XmlTextReader xmlTr = new XmlTextReader(instream);


			XPathDocument xpathdoc = new XPathDocument(xmlTr);

			XPathNavigator nav = xpathdoc.CreateNavigator();

			XsltArgumentList xslArg = new XsltArgumentList();

			xslArg.AddParam("tablename", "", ds.Tables[0].TableName);

			xslt.Transform(nav, xslArg, outstream);
			return outstream;
		}

		public void TransformToFile(string xmlData, string xslFilePath, string resultFileName)
		{
            XslCompiledTransform xslt = new XslCompiledTransform();

			xslt.Load(xslFilePath);

			XmlTextReader xr = new XmlTextReader(new StringReader(xmlData));

			XPathDocument mydata = new XPathDocument(xr);

			XmlWriter writer = new XmlTextWriter(resultFileName, Encoding.UTF8);

			xslt.Transform(mydata, null, writer);

			writer.Close();
		}
	}
}
