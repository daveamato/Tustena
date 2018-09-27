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
using System.Text;
using Digita.Tustena.Base;
using Digita.Tustena.Core;

namespace Digita.Tustena
{
	public class vCard : G
	{
		public string title = String.Empty; //Mr., Mrs., Ms., Dr.
		public string firstName = String.Empty;
		public string MiddleName = String.Empty;
		public string LastName = String.Empty;
		public string suffix = String.Empty; //I, II, Jr., Sr.
		public string formattedName = String.Empty;
		public string Nickname = String.Empty;
		public string Organization = String.Empty; //MS Outlook calls this Company
		public string OrganizationalUnit = String.Empty; //MS Outlook calls this Department
		public string role = String.Empty; //MS Outlook calls this the profession
		public string JobTitle = String.Empty;
		public string Note = String.Empty;
		public DateTime Birthday;

		public vURLs URLs = new vURLs();
		public vEmails Emails = new vEmails();
		public vTelephones Telephones = new vTelephones();
		public vAddresss Addresses = new vAddresss();

		public DateTime LastModified;

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();
			result.AppendFormat("BEGIN:VCARD{0}", Environment.NewLine);
			result.AppendFormat("VERSION:2.1{0}", Environment.NewLine);
			result.AppendFormat("N:{0};{1};{2};{3};{4}{5}", LastName, firstName, MiddleName, title, suffix, Environment.NewLine);
			if (StaticFunctions.IsNotBlank(formattedName))
				result.AppendFormat("FN:{0}{1}", formattedName, Environment.NewLine);
			if (StaticFunctions.IsNotBlank(Nickname))
				result.AppendFormat("NICKNAME:{0}{1}", Nickname, Environment.NewLine);
			if (Birthday > DateTime.MinValue)
				result.AppendFormat("BDAY:{0}{1}", Birthday.ToUniversalTime().ToString("yyyyMMdd"), Environment.NewLine);
			if (StaticFunctions.IsNotBlank(Note))
				result.AppendFormat("NOTE;ENCODING=QUOTED-PRINTABLE:{0}{1}", Note.Replace(Environment.NewLine, "=0D=0A"), Environment.NewLine);
			result.AppendFormat("ORG:{0};{1}{2}", Organization, OrganizationalUnit, Environment.NewLine);
			if (StaticFunctions.IsNotBlank(JobTitle))
				result.AppendFormat("TITLE:{0}{1}", JobTitle, Environment.NewLine);
			if (StaticFunctions.IsNotBlank(role))
				result.AppendFormat("ROLE:{0}{1}", role, Environment.NewLine);
			result.Append(Emails.ToString());
			result.Append(Telephones.ToString());
			result.Append(URLs.ToString());
			result.Append(Addresses.ToString());
			result.AppendFormat("REV:{0}{1}", LastModified.ToUniversalTime().ToString(@"yyyyMMdd\THHmmss\Z"), Environment.NewLine);
			result.AppendFormat("END:VCARD{0}", Environment.NewLine);
			return result.ToString();
		}

		public class vEmails : CollectionBase
		{
			public vEmail Add(vEmail value)
			{
				if (value.Preferred)
				{
					foreach (vEmail item in this.InnerList)
						item.Preferred = false;
				} // If
				this.InnerList.Add(value);
				return value;
			}

			public vEmail Item(int index)
			{
				return (vEmail) this.InnerList[index];
			}

			public void Remove(int index)
			{
				vEmail cust;
				cust = (vEmail) this.InnerList[index];
				if (cust != null)
				{
					this.InnerList.Remove(cust);
				}
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				foreach (vEmail item in this.InnerList)
					result.AppendFormat("{0}", item.ToString());
				return result.ToString();
			}
		}

		public class vEmail
		{
			public bool Preferred;
			public string emailAddress = String.Empty;
			public string type = "INTERNET";

			public vEmail(String email)
			{
				emailAddress = email;
			}

			public vEmail(String email, bool isPreferred)
			{
				emailAddress = email;
				Preferred = isPreferred;
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				result.Append("EMAIL");
				if (Preferred) result.Append(";PREF");
				result.AppendFormat(";{0}", type.ToUpper());
				result.AppendFormat(":{0}{1}", emailAddress, Environment.NewLine);
				return result.ToString();
			}
		}

		public class vURLs : CollectionBase
		{
			public vURL Add(vURL value)
			{
				if (value.Preferred)
				{
					for (int i = 0; i < this.InnerList.Count - 1; i++) ;
					value.Preferred = false;
				}
				this.InnerList.Add(value);
				return value;
			}

			public vURL Item(int index)
			{
				return (vURL) (this.InnerList[index]);
			} // Function

			public void Remove(int index)
			{
				vURL cust;
				cust = (vURL) (this.InnerList[index]);
				if (cust != null)
				{
					this.InnerList.Remove(cust);
				}
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				foreach (vURL item in this.InnerList)
					result.AppendFormat("{0}", item.ToString());
				return result.ToString();
			}
		}

		public class vURL
		{
			public Boolean Preferred;
			public String URL = String.Empty;
			public vLocations Location = vLocations.WORK; //MS Outlook shows the WORK location on the contact form front page

			public vURL(String newURL)
			{
				URL = newURL;
			}

			public vURL(String newURL, Boolean isPreffered)
			{
				URL = newURL;
				Preferred = isPreffered;
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				result.Append("URL");
				if (Preferred) result.Append(";PREF");
				result.AppendFormat(";{0}", Location.ToString().ToUpper());
				result.AppendFormat(":{0}{1}", URL, Environment.NewLine);
				return result.ToString();
			}
		}

		public class vTelephones : CollectionBase
		{
			public vTelephone Add(vTelephone value)
			{
				if (value.Preferred)
				{
					foreach (vTelephone item in this.InnerList)
						item.Preferred = false;
				}
				this.InnerList.Add(value);
				return value;
			}

			public vTelephone Item(int index)
			{
				return (vTelephone) (this.InnerList[index]);
			}

			public void Remove(int index)
			{
				vTelephone cust;

				cust = (vTelephone) (this.InnerList[index]);
				if (cust != null)
					this.InnerList.Remove(cust);

			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				foreach (vTelephone item in this.InnerList)
					result.AppendFormat("{0}", item.ToString());

				return result.ToString();
			}
		}

		public class vTelephone
		{
			public Boolean Preferred;
			public string telephoneNumber = String.Empty;
			public vLocations Location;
			public vPhoneTypes type;

			public vTelephone(string number)
			{
				telephoneNumber = number;
			}

			public vTelephone(string number, Boolean isPreferred)
			{
				telephoneNumber = number;
				Preferred = isPreferred;
			}

			public vTelephone(string number, vLocations phoneLocation, vPhoneTypes phoneType, Boolean isPreferred)
			{
				telephoneNumber = number;
				Location = phoneLocation;
				type = phoneType;
				Preferred = isPreferred;
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();
				result.Append("TEL");
				if (Preferred) result.Append(";PREF");
				result.AppendFormat(";{0}", Location.ToString().ToUpper());
				result.AppendFormat(";{0}", type.ToString().ToUpper());
				result.AppendFormat(":{0}{1}", telephoneNumber, Environment.NewLine);
				return result.ToString();
			}

			public vTelephone()
			{
			}
		}

		public class vAddresss : CollectionBase
		{
			public vAddress Add(vAddress value)
			{
				if (value.Preferred)
				{
					foreach (vAddress item in this.InnerList)
						item.Preferred = false;
				}
				this.InnerList.Add(value);
				return value;
			}

			public vAddress Item(int index)
			{
				return (vAddress) (this.InnerList[index]);
			}

			public void Remove(int index)
			{
				vAddress cust;

				cust = (vAddress) (this.InnerList[index]);
				if (cust != null)
				{
					this.InnerList.Remove(cust);
				}
			}

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();

				foreach (vAddress item in this.InnerList)
					result.AppendFormat("{0}", item.ToString());

				return result.ToString();
			}
		}

		public class vAddress
		{
			public bool Preferred;
			public string addressName = String.Empty; //MS Outlook calls this Office
			public string streetAddress = String.Empty;
			public string city = String.Empty;
			public string State = String.Empty;
			public string zip = String.Empty;
			public string Country = String.Empty;
			public string addressLabel = String.Empty; //if (you don't want to waste time creating this, don't and let the vCard reader format it for you
			public vLocations Location; //HOME, WORK, CELL
			public vAddressTypes type; //PARCEL, DOM, INT

			public override string ToString()
			{
				StringBuilder result = new StringBuilder();

				result.Append("ADR");
				if (Preferred) result.Append(";PREF");
				result.AppendFormat(";{0}", Location.ToString().ToUpper());
				result.AppendFormat(";{0}", type.ToString().ToUpper());
				result.AppendFormat(";ENCODING=QUOTED-PRINTABLE:;{0}", addressName);
				result.AppendFormat(";{0}", streetAddress.Replace(Environment.NewLine, "=0D=0A"));
				result.AppendFormat(";{0}", city.Replace(Environment.NewLine, "=0D=0A"));
				result.AppendFormat(";{0}", State.Replace(Environment.NewLine, "=0D=0A"));
				result.AppendFormat(";{0}", zip.Replace(Environment.NewLine, "=0D=0A"));
				result.AppendFormat(";{0}", Country.Replace(Environment.NewLine, "=0D=0A"));
				result.Append(Environment.NewLine);

				if (addressLabel.Length > 0)
				{
					result.Append("LABEL");
					result.AppendFormat(";{0}", Location.ToString().ToUpper());
					result.AppendFormat(";{0}", type.ToString().ToUpper());
					result.AppendFormat(";ENCODING=QUOTED-PRINTABLE:{0}", addressLabel.Replace(Environment.NewLine, "=0D=0A"));
				}

				return result.ToString();
			}
		}

		public enum vLocations
		{
			HOME,
			WORK,
			CELL
		}

		public enum vAddressTypes
		{
			PARCEL, //Parcel post
			DOM, //Domestic
			INT //International
		}

		public enum vPhoneTypes
		{
			VOICE,
			FAX,
			MSG
		}
	}
}
