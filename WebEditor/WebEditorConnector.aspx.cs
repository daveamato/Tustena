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
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using Digita.Tustena.Core;
using Digita.Tustena.MailingList;
using FredCK.FCKeditorV2;

namespace Digita.Tustena
{
	public partial class WebEditorConnector : Page
	{
		private string sUserFilesDirectory;
		UserConfig UC = new UserConfig();

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
		protected void Page_Load(object sender, EventArgs e)
		{
			UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			string sCommand = Request.QueryString["Command"];
			if (sCommand == null) return;

			string sResourceType = Request.QueryString["Type"];
			if (sResourceType == null) return;

			string sCurrentFolder = Request.QueryString["CurrentFolder"];
			if (sCurrentFolder == null) return;

			if (! sCurrentFolder.EndsWith("/"))
				sCurrentFolder += "/";
			if (! sCurrentFolder.StartsWith("/"))
				sCurrentFolder = "/" + sCurrentFolder;

			if (sCommand == "FileUpload")
			{
				this.FileUpload(sResourceType, sCurrentFolder);
				return;
			}

			Response.Clear();
			Response.CacheControl = "no-cache";

			Response.ContentEncoding = UTF8Encoding.UTF8;
			Response.ContentType = "text/xml";

			XmlDocument oXML = new XmlDocument();
			XmlNode oConnectorNode = CreateBaseXml(oXML, sCommand, sResourceType, sCurrentFolder);

			switch (sCommand)
			{
				case "GetFolders":
					this.GetFolders(oConnectorNode, sResourceType, sCurrentFolder);
					break;
				case "GetFoldersAndFiles":
					this.GetFolders(oConnectorNode, sResourceType, sCurrentFolder);
					this.GetFiles(oConnectorNode, sResourceType, sCurrentFolder);
					break;
				case "CreateFolder":
					this.CreateFolder(oConnectorNode, sResourceType, sCurrentFolder);
					break;
			}

			Response.Write(oXML.OuterXml);

			Response.End();
		}

		#region Base XML Creation

		private XmlNode CreateBaseXml(XmlDocument xml, string command, string resourceType, string currentFolder)
		{
			xml.AppendChild(xml.CreateXmlDeclaration("1.0", "utf-8", null));

			XmlNode oConnectorNode = XmlUtil.AppendElement(xml, "Connector");
			XmlUtil.SetAttribute(oConnectorNode, "command", command);
			XmlUtil.SetAttribute(oConnectorNode, "resourceType", resourceType);

			XmlNode oCurrentNode = XmlUtil.AppendElement(oConnectorNode, "CurrentFolder");
			XmlUtil.SetAttribute(oCurrentNode, "path", currentFolder);
			XmlUtil.SetAttribute(oCurrentNode, "url", GetUrlFromPath(resourceType, currentFolder));

			return oConnectorNode;
		}

		#endregion

		#region Command Handlers

		private void GetFolders(XmlNode connectorNode, string resourceType, string currentFolder)
		{
			string sServerDir = this.ServerMapFolder(resourceType, currentFolder);

			XmlNode oFoldersNode = XmlUtil.AppendElement(connectorNode, "Folders");

			DirectoryInfo oDir = new DirectoryInfo(sServerDir);
			DirectoryInfo[] aSubDirs = oDir.GetDirectories();

			for (int i = 0; i < aSubDirs.Length; i++)
			{
				XmlNode oFolderNode = XmlUtil.AppendElement(oFoldersNode, "Folder");
				XmlUtil.SetAttribute(oFolderNode, "name", aSubDirs[i].Name);
			}
		}

		private void GetFiles(XmlNode connectorNode, string resourceType, string currentFolder)
		{
			string sServerDir = this.ServerMapFolder(resourceType, currentFolder);

			XmlNode oFilesNode = XmlUtil.AppendElement(connectorNode, "Files");

			DirectoryInfo oDir = new DirectoryInfo(sServerDir);
			FileInfo[] aFiles = oDir.GetFiles();

			for (int i = 0; i < aFiles.Length; i++)
			{
				long iFileSize = (aFiles[i].Length/1024);
				if (iFileSize < 1) iFileSize = 1;

				XmlNode oFileNode = XmlUtil.AppendElement(oFilesNode, "File");
				XmlUtil.SetAttribute(oFileNode, "name", aFiles[i].Name);
				XmlUtil.SetAttribute(oFileNode, "size", iFileSize.ToString(CultureInfo.InvariantCulture));
			}
		}
		private void CreateFolder(XmlNode connectorNode, string resourceType, string currentFolder)
		{
			string sErrorNumber = "0";

			string sNewFolderName = Request.QueryString["NewFolderName"];

			if (sNewFolderName == null || sNewFolderName.Length == 0)
				sErrorNumber = "102";
			else
			{
				string sServerDir = this.ServerMapFolder(resourceType, currentFolder);
				DirectoryInfo oDir = new DirectoryInfo(sServerDir);

				try
				{
					oDir.CreateSubdirectory(sNewFolderName);
				}
				catch (ArgumentException)
				{
					sErrorNumber = "102";
				}
				catch (PathTooLongException)
				{
					sErrorNumber = "102";
				}
				catch (IOException)
				{
					sErrorNumber = "101";
				}
				catch (SecurityException)
				{
					sErrorNumber = "103";
				}
				catch (Exception)
				{
					sErrorNumber = "110";
				}
			}

			XmlNode oErrorNode = XmlUtil.AppendElement(connectorNode, "Error");
			XmlUtil.SetAttribute(oErrorNode, "number", sErrorNumber);
		}

		private void FileUpload( string resourceType, string currentFolder )
		{
			HttpPostedFile oFile = Request.Files["NewFile"] ;

			string sErrorNumber = "0" ;
			string sFileName = "" ;

			if ( oFile != null )
			{
				string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

				sFileName = Path.GetFileName( oFile.FileName ) ;

				int iCounter = 0 ;

				while ( true )
				{
					string sFilePath = Path.Combine( sServerDir, sFileName ) ;

					if ( File.Exists( sFilePath ) )
					{
						iCounter++ ;
						sFileName =
							Path.GetFileNameWithoutExtension( oFile.FileName ) +
							"(" + iCounter + ")" +
								Path.GetExtension( oFile.FileName ) ;

						sErrorNumber = "201" ;
					}
					else
					{
						oFile.SaveAs( sFilePath ) ;
						break ;
					}
				}
			}
			else
				sErrorNumber = "202" ;

			Response.Clear() ;

			Response.Write( "<script type=\"text/javascript\">" ) ;
			Response.Write( "window.parent.frames['frmUpload'].OnUploadCompleted(" + sErrorNumber + ",'" + sFileName.Replace( "'", "\\'" ) + "') ;" ) ;
			Response.Write( "</script>" ) ;

			Response.End() ;
		}

		#endregion

		#region Directory Mapping

		private string ServerMapFolder(string resourceType, string folderPath)
		{
			string sResourceTypePath = Path.Combine(this.UserFilesDirectory, resourceType);

			Directory.CreateDirectory(sResourceTypePath);

			return Path.Combine(sResourceTypePath, folderPath.TrimStart('/'));
		}

		private string GetUrlFromPath(string resourceType, string folderPath)
		{
			return this.UserFilesPath + resourceType + folderPath;
		}

		private string UserFilesPath
		{
			get
			{
	 				return WebEditorUtils.WebUserFilesPath;
			}
		}

		private string UserFilesDirectory
		{
			get
			{
				if (sUserFilesDirectory == null)
				{
					sUserFilesDirectory = WebEditorUtils.RootUserFilesPath;
				}
				return sUserFilesDirectory;
			}
		}

		#endregion
	}


}
