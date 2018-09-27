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
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Digita.Tustena.Core;
using Digita.Tustena.MailingList;

namespace Digita.Tustena
{
	public partial class immagine : Page
	{
		private string imgFolder;
		public UserConfig UC;


			#region Codice generato da Progettazione Web Form

			protected override void OnInit(EventArgs e)
			{
				InitializeComponent();
				base.OnInit(e);
			}

			private void InitializeComponent()
			{
				this.Load += new EventHandler(this.Page_Load);
			this.BtnLoad.Click += new EventHandler(this.BtnLoad_Click);
			this.FileList.ItemCommand += new RepeaterCommandEventHandler(this.RepeaterCommand);
			this.FileList.ItemDataBound += new RepeaterItemEventHandler(this.RepeaterDataBound);

			}
		#endregion

		public void Page_Load(Object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			imgFolder = WebEditorUtils.RootUserFilesPath;

			if (!IsPostBack)
			{
				BtnLoad.Text = Root.rm.GetString("MLImage6");
				this.FillRep();
			}
			CreateFileList();
		}

		protected void BtnLoad_Click(Object sender, EventArgs e)
		{
			UploadFile();
			this.FillRep();
		}

		public void RepeaterDataBound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					LinkButton Erase = (LinkButton) e.Item.FindControl("Erase");
					Erase.Text = Root.rm.GetString("MLImage4");
					break;
			}
		}

		protected void RepeaterCommand(Object sender, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Erase":
					Literal lbl = (Literal) e.Item.FindControl("FileName");
					deletefile(lbl.Text);
					break;
			}
		}

		private void deletefile(string file)
		{
			string fullPath = Path.Combine(this.imgFolder, file);
			FileInfo MyFile = new FileInfo(fullPath);
			MyFile.Delete();
		}

		private void UploadFile()
		{
			byte[] ms = new byte[inpFile.PostedFile.ContentLength];
			inpFile.PostedFile.InputStream.Read(ms, 0, ms.Length);
			string fileName = Path.GetFileName(inpFile.PostedFile.FileName);
			string filePath = Path.Combine(this.imgFolder, fileName);
			FileStream fs = new FileStream(filePath, FileMode.Create);
			fs.Write(ms, 0, ms.Length);
			fs.Close();
			fs = null;
		}

		private DataTable CreateFileList()
		{
			DirectoryInfo MyDir = new DirectoryInfo(imgFolder);

			DataTable t = new DataTable();
			t.Columns.Add(new DataColumn("filename"));
			t.Columns.Add(new DataColumn("filesize"));

			DataRow dr;

			foreach (FileInfo Files in MyDir.GetFiles())
			{
				dr = t.NewRow();
				dr["filename"] = Files.Name;
				dr["filesize"] = Files.Length/1000;
				t.Rows.Add(dr);
			}

			return t;
		}

		private void FillRep()
		{
			DataTable t = this.CreateFileList();
			this.FileList.DataSource = t;
			this.FileList.DataBind();
		}
	}

}
