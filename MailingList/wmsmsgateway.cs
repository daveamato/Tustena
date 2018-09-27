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
using System.IO;
using System.Net;
using System.Text;
using System.Xml;

namespace Digita.Tustena
{

	public delegate void ErrorHandler(string error);
	public delegate void TakeCredit(int creditunit);

	public class wmsmsgateway
	{
		public event ErrorHandler myeventerror;
		public event TakeCredit mytakecredit;
		private string userName = "margo@tustena.com";
		private string password = "SL5fKuS2LL";
		private string origin = "TustenaCRM";
		private int _smscost = -1;

		public string ResultText_
		{
			get { return resultText; }
		}

		private string resultText = String.Empty;

		public int smscost
		{
			get { return _smscost; }
		}

		public int smspocket
		{
			get { return _smspocket; }
		}

		private int _smspocket = -1;

		public int SendSMS(string number, string message)
		{
			return SendSMS(number, message, origin);
		}
		public int SendSMS(string number, string message, string origin)
		{
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.LoadXml("<?xml version=\"1.0\"?>"+
					"<sms>"+
					"<Header>"+
					"<Username>"+userName+"</Username>"+
					"<Password>"+password+"</Password>"+
					"</Header>"+
					"<Contents>"+
					"<Label>"+origin+"</Label>"+
					"<Destination>"+number+"</Destination>"+
					"<Data>"+message+"</Data>"+
					"</Contents>"+
					"</sms>");
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			doc = send(doc.OuterXml);
			if(doc == null) exceptionNotify("Errore di formattazione Messageso SMS:\n"+doc.OuterXml);
			string resultCode = String.Empty;
			string MessageID = String.Empty;
			string route = String.Empty;
			try
			{
				resultCode = checkXmlResult(ref doc,"ResultCode");
				resultText = checkXmlResult(ref doc,"ResultText");
				_smscost = checkXmlResultInt(ref doc,"CreditsAvailable");
				_smspocket = checkXmlResultInt(ref doc,"CreditsAvailable");
			}
			catch(Exception ex)
			{
				exceptionNotify(ex.Message + "\n" +doc.OuterXml);
			}
			if(mytakecredit != null && _smscost>-1)
				mytakecredit(smscost);

			return (isError(resultCode));

		}

		private string checkXmlResult( ref XmlDocument doc, string xField)
		{
			XmlNodeList xList = doc.GetElementsByTagName("ResultCode");
			if(xList.Count>0)
				return xList[0].InnerText;
			else
				return String.Empty;
		}

		private int checkXmlResultInt( ref XmlDocument doc, string xField)
		{
			XmlNodeList xList = doc.GetElementsByTagName("ResultCode");
			if(xList.Count>0)
				try
				{
					return int.Parse(xList[0].InnerText);
				}catch
				{
					return -1;
				}
			else
				return -1;
		}

		private XmlDocument send(string xmlmsg)
		{
			WebRequest httpRequest = WebRequest.Create("http://enterprise.wirelessmedia.com/sms/xml.gateway");
			httpRequest.Method="POST";
			httpRequest.ContentType = "text/xml; charset=utf-8";
			byte [] bytes = Encoding.ASCII.GetBytes(xmlmsg);
			httpRequest.ContentLength = bytes.Length;
			Stream os = httpRequest.GetRequestStream ();
			os.Write (bytes, 0, bytes.Length);
			os.Close ();

			WebResponse httpResponse = httpRequest.GetResponse();
			Stream stream = httpResponse.GetResponseStream();
			StreamReader reader = new StreamReader(stream);

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.LoadXml(reader.ReadToEnd());
			}
			catch(Exception ex)
			{
				exceptionNotify(ex.Message);
			}
			finally
			{
				reader.Close();

			}
			return doc;
		}

		private void exceptionNotify(string error)
		{
			if(myeventerror != null)
				myeventerror(error);

		}

		private int isError(string code)
		{
			switch(code)
			{
				case "00":
					return 0; // message OK
				case "01H":
					return 1; // Number Error
				case "04":
					return 2; // Queued
				default:
					exceptionNotify("SMS error, CODE: "+code);
					return -1;
			}
		}
	}
}
