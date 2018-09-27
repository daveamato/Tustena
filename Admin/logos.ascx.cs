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
using System.Resources;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brettle.Web.NeatUpload;
using Digita.Tustena.Base;
using Digita.Tustena.Core;

namespace Digita.Tustena.Admin
{
	public partial class logos : UserControl
	{

		private UserConfig UC = new UserConfig();
		public static ResourceManager rm = (ResourceManager) HttpContext.Current.Application["RM"];

		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["userconfig"];
			btnUpload.Text=Root.rm.GetString("Save");
			if(!Page.IsPostBack)
				FillrepLogos();
		}

		private void FillrepLogos()
		{

			string pathTemplate;
			pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
			FileFunctions.CheckDir(pathTemplate,true);
			DirectoryInfo dirInfo = new DirectoryInfo(pathTemplate);

			repLogos.DataSource = dirInfo.GetFiles();
			repLogos.DataBind();


		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.btnUpload.Click+=new EventHandler(btnUpload_Click);
			this.repLogos.ItemDataBound+=new RepeaterItemEventHandler(repLogos_ItemDataBound);
			this.repLogos.ItemCommand+=new RepeaterCommandEventHandler(repLogos_ItemCommand);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void btnUpload_Click(object sender, EventArgs e)
		{


			if (this.inputFile.HasFile)
			{
				string ext =Path.GetExtension(inputFile.FileName).ToLower();
				if(ext==".gif" || ext==".jpg" || ext==".png" || ext==".tif")
				{
					string pathTemplate;
							pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
					FileFunctions.CheckDir(pathTemplate,true);
					string saveFile = Path.Combine(pathTemplate, inputFile.FileName);
					this.inputFile.MoveTo(saveFile,MoveToOptions.Overwrite);

					FillrepLogos();
				}else
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "zzz", "<script>alert('" + Root.rm.GetString("ErpConftxt7") + "');</script>");
			}
		}

		private void repLogos_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Header:
					LinkButton MultiDelete = (LinkButton)e.Item.FindControl("MultiDelete");
					MultiDelete.Text="<img src=/i/trash.gif border=0>";
					break;
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal litFile = (Literal)e.Item.FindControl("litFile");
					litFile.Text=(string)DataBinder.Eval(e.Item.DataItem, "Name");
					break;
			}
		}

		private void repLogos_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			string pathTemplate;
			pathTemplate = ConfigSettings.DataStoragePath+"\\logos";
			for (int i = 0; i < repLogos.Items.Count; i++)
			{
				bool isChecked = ((CheckBox) repLogos.Items[i].FindControl("chkDelete")).Checked;
				if (isChecked)
				{
					Literal litFile = (Literal) (repLogos.Items[i].FindControl("litFile"));

					if(File.Exists(Path.Combine(pathTemplate, litFile.Text)))
						File.Delete(Path.Combine(pathTemplate, litFile.Text));
				}
			}

			DirectoryInfo dirInfo = new DirectoryInfo(pathTemplate);

			repLogos.DataSource = dirInfo.GetFiles();
			repLogos.DataBind();

		}
	}
}
