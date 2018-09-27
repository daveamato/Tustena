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
using System.Net;
using System.Xml;

namespace Digita.Tustena
{
	class ExchangeRate:G
	{
		public DataTable GetCurrent()
		{
			DataTable dt = null;
			if(Cache["ExchangeRate"] is DataTable)
			{
				dt = Cache["ExchangeRate"] as DataTable;
				if(dt.Rows.Count>0)
				{
					if(((DateTime)dt.Rows[0]["Date"]).ToShortDateString()==DateTime.Now.ToShortDateString())
						return Cache["ExchangeRate"] as DataTable;
				}
			}
			dt = parseWebXML();
			Cache["ExchangeRate"]=dt;
			return dt;
		}

		private string MoneyName(string shortName)
		{
			string returnValue = "NULL";
			switch(shortName)
			{
				case "DKK":  returnValue = "Danish Krone"; break;
				case "EUR":  returnValue = "Euro"; break;
				case "USD":  returnValue = "US Dollar" ; break;
				case "GBP":  returnValue = "United Kingdom Pound"; break;
				case "SEK":  returnValue = "Swedish Krona"; break;
				case "NOK":  returnValue = "Norwegian Kroner"; break;
				case "CNY":	 returnValue = "Chinese Yuan Renminbi"; break;
				case "ISK":  returnValue = "Islandske Kroner"; break;
				case "IDR":	 returnValue = "Indonesian Rupiah"; break;
				case "CHF":  returnValue = "Schweiziske Franc"; break;
				case "CAD":  returnValue = "Canadian Dollar"; break;
				case "JPY":  returnValue = "Japanese Yen"; break;
				case "RUB":  returnValue = "Russian Rouble"; break;
				case "HRK":  returnValue = "Croatian Kuna"; break;
				case "MYR":	 returnValue = "Malaysian Ringgit"; break;
				case "PHP":	 returnValue = "Philippine Peso"; break;
				case "THB":	 returnValue = "Thai Baht"; break;
				case "AUD":  returnValue = "Australske Dollars"; break;
				case "NZD":  returnValue = "New Zealand. Dollar"; break;
				case "EEK":  returnValue = "Estiske Kroon"; break;
				case "LVL":  returnValue = "Lettiske Lats"; break;
				case "LTL":  returnValue = "Litauiske Litas"; break;
				case "PLN":  returnValue = "Polske Zloty"; break;
				case "CZK":  returnValue = "Tjekkiske Koruna"; break;
				case "HUF":  returnValue = "Ungarske Forint"; break;
				case "HKD":  returnValue = "Hongkong Dollar"; break;
				case "SGD":  returnValue = "Singapore Dollar"; break;
				case "SDR":  returnValue = "Special Drawing Rights"; break;
				case "BGN":  returnValue = "Bulgarske lev"; break;
				case "CYP":  returnValue = "Cypriotiske pund"; break;
				case "MTL":  returnValue = "Maltesiske lira"; break;
				case "ROL":  returnValue = "Rumnske lei"; break;
				case "SIT":  returnValue = "Slovenske tolar"; break;
				case "SKK":  returnValue = "Slovakiske koruna"; break;
				case "TRY":  returnValue = "Tyrkiske lira"; break;
				case "KRW":  returnValue = "Sydkoreanske won"; break;
				case "ZAR":  returnValue = "Sydafrikanske rand"; break;
				default: break;
			}

			return returnValue;
		}

		private DataTable parseWebXML()
		{
			string WebAddress = "http://www.ecb.int/stats/eurofxref/eurofxref-daily.xml";
			XmlTextReader xmlReader;
			DataTable dt = new DataTable();
			DataColumn dc = new DataColumn("Date",typeof(DateTime));
			dt.Columns.Add(dc);
			dc = new DataColumn("Code",typeof(String));
			dt.Columns.Add(dc);
			dc = new DataColumn("Rate",typeof(Double));
			dt.Columns.Add(dc);
			dc = new DataColumn("Name",typeof(String));
			dt.Columns.Add(dc);

			try
			{
				xmlReader = new XmlTextReader(WebAddress);
				xmlReader.WhitespaceHandling = WhitespaceHandling.None;

			}
			catch( WebException ex)
			{
				throw new WebException("Comunicazione fallita\r\n"+ex.Message);
			}

			DateTime tim = new DateTime(0);
			try
			{
				while( xmlReader.Read() )
				{
					if ((xmlReader.Name != null && xmlReader.Name.Length != 0))
					{

						if (xmlReader.Name == "gesmes:name")
						{
							string author = xmlReader.ReadString();
						}
						for (int i = 0 ; i < xmlReader.AttributeCount; i++)
						{

							if (xmlReader.Name == "Cube")
							{
								if (xmlReader.AttributeCount == 1)
								{
									xmlReader.MoveToAttribute("time");

									tim = DateTime.Parse(xmlReader.Value);

									DataRow dr = null;

									dr = dt.NewRow();
									dr["Date"]= tim;
									dr["Code"]= "EUR";
									dr["Name"]= MoneyName("EUR");
									dr["Rate"]= 1.0;
									dt.Rows.Add(dr);
								}

								if (xmlReader.AttributeCount == 2)
								{
									xmlReader.MoveToAttribute("currency");
									string cur = xmlReader.Value;

									xmlReader.MoveToAttribute("rate");
									decimal rat = decimal.Parse(xmlReader.Value.Replace(".",","));

									DataRow dr = null;

									dr = dt.NewRow();
									dr["Date"]= tim;
									dr["Code"]= cur;
									dr["Name"]= MoneyName(cur);
									dr["Rate"]= rat;
									dt.Rows.Add(dr);
								}

							}
							xmlReader.MoveToNextAttribute();
						}
					}
				}
			}
			catch( WebException ex )
			{
				throw new WebException("Comunicazione fallita\r\n"+ex.Message);
			}

			return dt;
		}


	}
}
