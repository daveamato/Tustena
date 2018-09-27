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
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using Lucene.Net.Parsing;
using Digita.Tustena.Base;
using Digita.ImageProcessing;

namespace Digita.Tustena
{
	public partial class AdminCompany : G
	{

		protected HtmlTableCell tdTab1;
		protected HtmlTableCell tdTab2;
		protected HtmlTableCell tdTab3;
		protected HtmlTableCell tdTab4;
		protected HtmlTableCell tdTab5;
		protected LocalizedLiteral LocalizedLiteral3;
		protected LocalizedLiteral LocalizedLiteral4;
		protected LocalizedLiteral LocalizedLiteral5;
		protected LocalizedLiteral Localizedliteral32;

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (!M.IsModule(ActiveModules.Lead))
                LeadModule.Visible = false;

            base.OnPreRenderComplete(e);
        }

		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				DeleteGoBack();
				if (!Page.IsPostBack)
				{
					Submit.Text =Root.rm.GetString("Save");
					FillForm();
				}

			}
		}

		private void FillForm()
		{
			string sqlString;
			sqlString = "SELECT * FROM ADMIN_CUSTOMER_VIEW";
			IDataReader dataReader = DatabaseConnection.CreateReader(sqlString);
			if (dataReader.Read())
			{
				CompanyName.Text = (string) dataReader["COMPANYNAME"];
				PhoneNumber.Text = Convert.ToString(dataReader["PHONE"]);
				Fax.Text = Convert.ToString(dataReader["FAX"]);
				Email.Text = Convert.ToString(dataReader["EMAIL"]);
				WebSite.Text = Convert.ToString(dataReader["WEBSITE"]);
				AddressInvoice.Text = Convert.ToString(dataReader["ADDRESS"]);
				CityInvoice.Text = Convert.ToString(dataReader["CITY"]);
				ProvinceInvoice.Text = Convert.ToString(dataReader["PROVINCE"]);
				RegionInvoice.Text = Convert.ToString(dataReader["REGION"]);
				CountryInvoice.Text = Convert.ToString(dataReader["STATE"]);
				ZipInvoice.Text = Convert.ToString(dataReader["ZIPCODE"]);
				LeadDays.Text = Convert.ToString(dataReader["ESTIMATEDDATEDAYS"]);
				Voip.Text = dataReader["LINKFORVOIP"].ToString();
				InterPrefix.Text = dataReader["INTERNATIONALPREFIX"].ToString();

				long kbc = (int) dataReader["DISKSPACE"];
				long kbo = 0;
				try
				{
					kbo = Convert.ToInt64(DatabaseConnection.SqlScalar("SELECT FOLDERSIZE FROM FOLDERSIZE"))/1024/1024;
				}
				catch
				{
				}

				string pathTemplate;
				pathTemplate = ConfigSettings.DataStoragePath;
				string idxPath = Path.Combine(pathTemplate, "luceneIdx");
				int idxSize = 0;
				int fileCount = 0;
				if (Directory.Exists(idxPath))
				{
					LuceneParser lucene = new LuceneParser(idxPath);
					DirectoryInfo di = new DirectoryInfo(idxPath);
					foreach (FileInfo fi in di.GetFiles())
					{
						idxSize += (int) (fi.Length/1024);
						fileCount++;

					}
					FileIndexes.Text = lucene.checkIndex() + ", " + idxSize + " Kb";
				}
				else
					FileIndexes.Text = "n/a";
				PermittedKb.Text = dataReader["DiskSpace"].ToString() + " Mb";
				BusyKb.Text = fileCount + " files, " + kbo.ToString() + " Mb";
				if (kbo > kbc)
				{
					BusyKb.ForeColor = Color.Red;
				}
				if (dataReader["IDAgenda"] != DBNull.Value)
				{
					CompanyTextboxID.Text = Convert.ToString(dataReader["IDAGENDA"]);
					CompanyTextbox.Text = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + uint.Parse(dataReader["IDAgenda"].ToString()));
				}

				WebService_pin.Text = dataReader["pin"].ToString();
				WebService_guid.Text = dataReader["guid"].ToString();

				WebService_OwnerID.Text = dataReader["DefaultWebUser"].ToString();
				if (WebService_OwnerID.Text.Length > 0)
				{
					WebService_Owner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT FROM ACCOUNT WHERE UID=" + int.Parse(WebService_OwnerID.Text));
				}
				else
				{
					DataRow dr;
					dr = DatabaseConnection.CreateDataset("SELECT TOP 1 ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT,UID FROM ACCOUNT LEFT OUTER JOIN TUSTENA_DATA ON TUSTENA_DATA.ADMINGROUPID=ACCOUNT.GROUPID ORDER BY UID").Tables[0].Rows[0];
					WebService_Owner.Text = dr["contact"].ToString();
					WebService_OwnerID.Text = dr["uid"].ToString();
				}


				WebGate_OwnerID.Text = dataReader["WEBGATEOWNERID"].ToString();
				WebGate_Owner.Text = dataReader["WEBGATEOWNER"].ToString();
				WebGate_GroupID.Text = dataReader["WEBGATEGROUP"].ToString();
				WebGate_Group.Text = dataReader["WEBGATEGROUPDESCRIPTION"].ToString();
				WebGate_NotifyID.Text = dataReader["WEBGATENOTIFYID"].ToString();
				WebGate_Notify.Text = dataReader["WEBGATENOTIFY"].ToString();
				WebGate_WebSite.Text = dataReader["WEBGATEWEBSITE"].ToString();


            string pathTemplateLogo;
					pathTemplateLogo = ConfigSettings.DataStoragePath+"\\logos";
            FileFunctions.CheckDir(pathTemplateLogo, true);
            DirectoryInfo dirInfo = new DirectoryInfo(pathTemplateLogo);

            FileInfo[] files = dirInfo.GetFiles();
            int select = 0;
            int counter = 0;
            checkLogo.Items.Add("No Logo");
            if (files.Length > 0)
            {
                foreach (FileInfo f in files)
                {
                    checkLogo.Items.Add(f.Name);
                    counter++;
                    if (dataReader["LOGO"] != System.DBNull.Value && dataReader["LOGO"].ToString() == f.Name)
                        select = counter;
                }
            }
            checkLogo.Items[select].Selected = true;

			}
			dataReader.Close();

			bool ws = false;
			if (WebService_pin.Text.Length == 0)
			{
				Random R = new Random();
				WebService_pin.Text = R.Next(30000).ToString();
				ws = true;
			}
			if (WebService_guid.Text.Length == 0)
			{
				WebService_guid.Text = Guid.NewGuid().ToString();
				ws = true;
			}
			string wsGuid = DatabaseConnection.FilterInjection(WebService_guid.Text);
			string wsPin = DatabaseConnection.FilterInjection(WebService_pin.Text);
			if (ws) DatabaseConnection.DoCommand(String.Format("UPDATE TUSTENA_DATA SET GUID='{0}',PIN='{1}'", wsGuid, wsPin));



		}

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Submit.Click += new EventHandler(this.Submit_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		public void Submit_Click(object sender, EventArgs e)
		{
			string sqlString;
			sqlString = String.Format("SELECT ACTIVE COMPANYNAME FROM TUSTENA_DATA");
			using (DigiDapter dg = new DigiDapter(sqlString))
			{
				dg.UpdateOnly();
				if (dg.HasRows)
				{
					dg.Add("PHONE", PhoneNumber.Text);
					dg.Add("FAX", Fax.Text);
					dg.Add("EMAIL", Email.Text);
					dg.Add("WEBSITE", WebSite.Text);
					dg.Add("ADDRESS", AddressInvoice.Text);
					dg.Add("CITY", CityInvoice.Text);
					dg.Add("PROVINCE", ProvinceInvoice.Text);
					dg.Add("REGION", RegionInvoice.Text);
					dg.Add("STATE", CountryInvoice.Text);
					dg.Add("ZIPCODE", ZipInvoice.Text);
					dg.Add("ESTIMATEDDATEDAYS", LeadDays.Text);
					dg.Add("GUID", WebService_guid.Text);
					dg.Add("PIN", WebService_pin.Text);
					dg.Add("DEFAULTWEBUSER", WebService_OwnerID.Text);
					dg.Add("LINKFORVOIP", Voip.Text);
					dg.Add("INTERNATIONALPREFIX", InterPrefix.Text);

					if (CompanyTextboxID.Text.Length > 0)
						dg.Add("IDAGENDA", CompanyTextboxID.Text);
					else
						dg.Add("IDAGENDA", DBNull.Value);

                    string pathTemplateLogo;
					pathTemplateLogo = ConfigSettings.DataStoragePath+"\\logos";
                    if (checkLogo.SelectedIndex > 0)
                    {
                        dg.Add("LOGO", checkLogo.SelectedValue);
                        JpegResize R = new JpegResize();
                        int w;
                        int h;
                        R.ImageSize(Path.Combine(pathTemplateLogo, checkLogo.SelectedValue), out w, out h);
                        string localpath = pathTemplateLogo.Substring(0, pathTemplateLogo.IndexOf("logos"));
                        if (h > 50 || w > 500)
                        {
                            if (File.Exists(Path.Combine(localpath, "MainLogo.jpg")))
                                File.Delete(Path.Combine(localpath, "MainLogo.jpg"));
                            R.Resize(Path.Combine(pathTemplateLogo, checkLogo.SelectedValue), Path.Combine(localpath, "MainLogo.jpg"), 50, 500, 500, 80);
                        }
                        else
                            File.Copy(Path.Combine(pathTemplateLogo, checkLogo.SelectedValue), Path.Combine(localpath, "MainLogo.jpg"));
                    }
                    else
                    {
                        dg.Add("LOGO", DBNull.Value);
                        string localpath = pathTemplateLogo.Substring(0,pathTemplateLogo.IndexOf("logos"));
                        if (File.Exists(Path.Combine(localpath, "MainLogo.jpg")))
                            File.Delete(Path.Combine(localpath, "MainLogo.jpg"));
                    }

					if (WebService_OwnerID.Text.Length > 0)
						WebService_Owner.Text = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') AS CONTACT FROM ACCOUNT WHERE UID=" + DatabaseConnection.FilterInjection(WebService_OwnerID.Text));
					dg.Execute("TUSTENA_DATA", "ACTIVE=1");
					}
			}

			Info.Text =Root.rm.GetString("AComtxt14");

		}
	}
}
