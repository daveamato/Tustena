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
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;

namespace Digita.Tustena
{
	public partial class vCardExport : G
	{

		#region Codice generato da Progettazione Web Form

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

		protected void Page_Load(Object sender, EventArgs e)
		{
			IDataReader dataReader;
			switch (Request.Params["mode"])
			{
				case "cn":
					dataReader = DatabaseConnection.CreateReader(String.Format("SELECT TOP 1 ID,COMPANYNAME,PHONE,FAX,EMAIL,WEBSITE FROM BASE_COMPANIES WHERE ID={0}", int.Parse(Request.Params["id"])));
					break;
				default:
					dataReader = DatabaseConnection.CreateReader(String.Format("SELECT TOP 1 ID,COMPANYNAME,PHONE,FAX,EMAIL,WEBSITE FROM BASE_COMPANIES WHERE ID={0}", int.Parse(Request.Params["id"])));
					break;
			}

			Response.Clear();
			Response.ContentType = "text/x-vCard";
			Response.AddHeader("Content-Disposition", "filename=vCard" + DateTime.Now.ToString(@"yyMMddHHmmss").ToString() + ".vcf");
			vCard card = new vCard();
			while (dataReader.Read())
			{
				if (dataReader.GetString(1).Length > 0)
					card.Organization = dataReader.GetString(1);
				if (StaticFunctions.IsNotBlank(dataReader.GetString(2)))
					card.Telephones.Add(new vCard.vTelephone(dataReader.GetString(2), vCard.vLocations.WORK, vCard.vPhoneTypes.VOICE, true));
				if (StaticFunctions.IsNotBlank(dataReader.GetString(3)))
					card.Telephones.Add(new vCard.vTelephone(dataReader.GetString(3), vCard.vLocations.WORK, vCard.vPhoneTypes.FAX, true));
				if (StaticFunctions.IsNotBlank(dataReader.GetString(4)))
					card.Emails.Add(new vCard.vEmail(dataReader.GetString(4)));
				if (StaticFunctions.IsNotBlank(dataReader.GetString(5)))
					card.URLs.Add(new vCard.vURL(dataReader.GetString(5)));


			}
			Response.Write(card.ToString());
		}
	}
}
