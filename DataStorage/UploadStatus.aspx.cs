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
using System.Web.UI;
using Digita.Tustena.UploadManager;

namespace UploadSpike
{
	public class UpStatus : Page
	{
		private void Page_Load(object sender, EventArgs e)
		{
			UploadStatus status = HttpUploadModule.GetUploadStatus();
			Response.Clear();
			Response.ContentType = "application/javascript";
			Response.Write("//uploadstatus\n");
			if (status != null)
			{
				decimal percent = Math.Round((decimal) ((100.0/status.ContentLength)*status.Position), 0);
				switch (status.State)
				{
					case UploadState.ReceivingData:
						Response.Write("document.getElementById(\"progressBar\").style.width=\"" + percent.ToString() + "%\"\n");
						Response.Write("document.getElementById(\"datatext\").innerHTML='" + status.Position.ToString() + " / " + status.ContentLength.ToString() + " - " + percent.ToString() + "%'\n");
						break;
					case UploadState.Complete:
						if (percent < 100)
							Response.Write("document.getElementById(\"datatext\").innerHTML='Upload Interrupted!!'\n");
						else
						{
							Response.Write("document.getElementById(\"progressBar\").style.width=\"100%\"\n");
							Response.Write("document.getElementById(\"datatext\").innerHTML='Upload Complete!!'\n");
						}
						Response.Write("setTimeout('self.close();', 2000);\n");
						break;

					case UploadState.Error:
						Response.Write("document.getElementById(\"datatext\").innerHTML='Upload Error!!'\n");
						Response.Write("setTimeout('self.close();', 2000);\n");
						break;
				}
			}
			Response.End();

		}

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
	}
}
