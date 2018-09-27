/* * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2004 Frederico Caldeira Knabben
 *
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 *
 * For further information visit:
 * 		http://www.fckeditor.net/
 *
 * File Name: FileBrowserConnector.cs
 * 	This is the code behind of the connector.aspx page used by the
 * 	File Browser.
 *
 * Version:  2.1
 * Modified: 2005-02-02 12:19:55
 *
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System;
using System.Globalization;
using System.IO;
using System.Security;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Xml;
using Digita.Tustena.Base;
using Digita.Tustena.MailingList;

namespace FredCK.FCKeditorV2
{
	public class FileBrowserConnector : Page
	{
		private const string DEFAULT_USER_FILES_PATH = "/UserFiles/" ;

		private string sUserFilesDirectory ;

		protected override void OnLoad(EventArgs e)
		{
			string sCommand = Request.QueryString["Command"] ;
			if ( sCommand == null ) return ;

			string sResourceType = Request.QueryString["Type"] ;
			if ( sResourceType == null ) return ;

			string sCurrentFolder = Request.QueryString["CurrentFolder"] ;
			if ( sCurrentFolder == null ) return ;

			if ( ! sCurrentFolder.EndsWith( "/" ) )
				sCurrentFolder += "/" ;
			if ( ! sCurrentFolder.StartsWith( "/" ) )
				sCurrentFolder = "/" + sCurrentFolder ;

			if ( sCommand == "FileUpload" )
			{
				this.FileUpload( sResourceType, sCurrentFolder ) ;
				return ;
			}

			Response.ClearHeaders() ;
			Response.Clear() ;

			Response.CacheControl = "no-cache" ;

			Response.ContentEncoding	= UTF8Encoding.UTF8 ;
			Response.ContentType		= "text/xml" ;

			XmlDocument oXML = new XmlDocument() ;
			XmlNode oConnectorNode = CreateBaseXml( oXML, sCommand, sResourceType, sCurrentFolder ) ;

			switch( sCommand )
			{
				case "GetFolders" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "GetFoldersAndFiles" :
					this.GetFolders( oConnectorNode, sResourceType, sCurrentFolder ) ;
					this.GetFiles( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
				case "CreateFolder" :
					this.CreateFolder( oConnectorNode, sResourceType, sCurrentFolder ) ;
					break ;
			}

			Response.Write( oXML.OuterXml ) ;

			Response.End() ;
		}

		#region Base XML Creation

		private XmlNode CreateBaseXml( XmlDocument xml, string command, string resourceType, string currentFolder )
		{
			xml.AppendChild( xml.CreateXmlDeclaration( "1.0", "utf-8", null ) ) ;

			XmlNode oConnectorNode = XmlUtil.AppendElement( xml, "Connector" ) ;
			XmlUtil.SetAttribute( oConnectorNode, "command", command ) ;
			XmlUtil.SetAttribute( oConnectorNode, "resourceType", resourceType ) ;

			XmlNode oCurrentNode = XmlUtil.AppendElement( oConnectorNode, "CurrentFolder" ) ;
			XmlUtil.SetAttribute( oCurrentNode, "path", currentFolder ) ;
			XmlUtil.SetAttribute( oCurrentNode, "url", GetUrlFromPath( resourceType, currentFolder) ) ;

			return oConnectorNode ;
		}

		#endregion

		#region Command Handlers

		private void GetFolders( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

			XmlNode oFoldersNode = XmlUtil.AppendElement( connectorNode, "Folders" ) ;

			DirectoryInfo oDir = new DirectoryInfo( sServerDir ) ;
			DirectoryInfo[] aSubDirs = oDir.GetDirectories() ;

			for ( int i = 0 ; i < aSubDirs.Length ; i++ )
			{
				XmlNode oFolderNode = XmlUtil.AppendElement( oFoldersNode, "Folder" ) ;
				XmlUtil.SetAttribute( oFolderNode, "name", aSubDirs[i].Name ) ;
			}
		}

		private void GetFiles( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

			XmlNode oFilesNode = XmlUtil.AppendElement( connectorNode, "Files" ) ;

			DirectoryInfo oDir = new DirectoryInfo( sServerDir ) ;
			FileInfo[] aFiles = oDir.GetFiles() ;

			for ( int i = 0 ; i < aFiles.Length ; i++ )
			{
				Decimal iFileSize = Math.Round( (Decimal)aFiles[i].Length / 1024 ) ;
				if ( iFileSize < 1 && aFiles[i].Length != 0 ) iFileSize = 1 ;

				XmlNode oFileNode = XmlUtil.AppendElement( oFilesNode, "File" ) ;
				XmlUtil.SetAttribute( oFileNode, "name", aFiles[i].Name ) ;
				XmlUtil.SetAttribute( oFileNode, "size", iFileSize.ToString( CultureInfo.InvariantCulture ) ) ;
			}
		}

		private void CreateFolder( XmlNode connectorNode, string resourceType, string currentFolder )
		{
			string sErrorNumber = "0" ;

			string sNewFolderName = Request.QueryString["NewFolderName"] ;

			if ( sNewFolderName == null || sNewFolderName.Length == 0 )
				sErrorNumber = "102" ;
			else
			{
				string sServerDir = this.ServerMapFolder( resourceType, currentFolder ) ;

				try
				{
					FileFunctions.CreateDirectory( Path.Combine( sServerDir, sNewFolderName )) ;
				}
				catch ( ArgumentException )
				{
					sErrorNumber = "102" ;
				}
				catch ( PathTooLongException )
				{
					sErrorNumber = "102" ;
				}
				catch ( IOException )
				{
					sErrorNumber = "101" ;
				}
				catch ( SecurityException )
				{
					sErrorNumber = "103" ;
				}
				catch ( Exception )
				{
					sErrorNumber = "110" ;
				}
			}

			XmlNode oErrorNode = XmlUtil.AppendElement( connectorNode, "Error" ) ;
			XmlUtil.SetAttribute( oErrorNode, "number", sErrorNumber ) ;
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

		private string ServerMapFolder( string resourceType, string folderPath )
		{
			string sResourceTypePath = Path.Combine( this.UserFilesDirectory, resourceType ) ;

			FileFunctions.CreateDirectory( sResourceTypePath ) ;

			return Path.Combine( sResourceTypePath, folderPath.TrimStart('/') ) ;
		}

		private string GetUrlFromPath( string resourceType, string folderPath )
		{
			if ( resourceType == null || resourceType.Length == 0 )
				return this.UserFilesPath.TrimEnd('/') + folderPath ;
			else
				return this.UserFilesPath + resourceType + folderPath ;
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
				if ( sUserFilesDirectory == null )
				{
					sUserFilesDirectory = WebEditorUtils.RootUserFilesPath;

				}
				return sUserFilesDirectory ;
			}
		}

		#endregion
	}
}
