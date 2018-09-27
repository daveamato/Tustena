/* * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2004 Frederico Caldeira Knabben
 *
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 *
 * For further information visit:
 * 		http://www.fckeditor.net/
 *
 * File Name: FCKeditor.cs
 * 	This is the FCKeditor Asp.Net control.
 *
 * Version:  2.1
 * Modified: 2005-02-27 19:44:36
 *
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FredCK.FCKeditorV2
{
	public enum LanguageDirection
	{
		LeftToRight,
		RightToLeft
	}

	[ DefaultProperty("Value") ]
	[ ValidationProperty("Value") ]
	[ ToolboxData("<{0}:FCKeditor runat=server></{0}:FCKeditor>") ]
	[ Designer("FredCK.FCKeditorV2.FCKeditorDesigner") ]
	[ ParseChildren(false) ]
	public class FCKeditor : Control, IPostBackDataHandler
	{
		private FCKeditorConfigurations oConfig ;

		public FCKeditor()
		{
			oConfig = new FCKeditorConfigurations() ;
		}

		#region Base Configurations Properties

		[ Browsable( false ) ]
		public FCKeditorConfigurations Config
		{
			get { return oConfig ; }
		}

		[ DefaultValue( "" ) ]
		public string Value
		{
			get { return (string)IsNull( ViewState["Value"], "" ) ; }
			set { ViewState["Value"] = value ; }
		}

		[ DefaultValue( "/FCKeditor/" ) ]
		public string BasePath
		{
			get
			{
				if ( ViewState["BasePath"] == null )
				{
					return (string)IsNull(
						ConfigurationManager.AppSettings["FCKeditor:BasePath"],
						"/FCKeditor/" ) ;
				}
				else
					return (string)ViewState["BasePath"] ;
			}
			set { ViewState["BasePath"] = value ; }
		}

		[ DefaultValue( "Default" ) ]
		public string ToolbarSet
		{
			get { return (string)IsNull( ViewState["ToolbarSet"], "Default" ) ; }
			set { ViewState["ToolbarSet"] = value ; }
		}

		#endregion

		#region Appearence Properties

		[ Category( "Appearence" ) ]
		[ DefaultValue( "100%" ) ]
		public Unit Width
		{
			get { return (Unit)IsNull( ViewState["Width"], Unit.Parse("100%", CultureInfo.InvariantCulture) ) ; }
			set { ViewState["Width"] = value ; }
		}

		[ Category("Appearence") ]
		[ DefaultValue( "200px" ) ]
		public Unit Height
		{
			get { return (Unit)IsNull( ViewState["Height"], Unit.Parse("200px", CultureInfo.InvariantCulture) ) ; }
			set { ViewState["Height"] = value ; }
		}

		#endregion

		#region Configurations Properties

		[ Category("Configurations") ]
		public string CustomConfigurationsPath
		{
			set { this.Config["CustomConfigurationsPath"] = value ; }
		}

		[ Category("Configurations") ]
		public string EditorAreaCSS
		{
			set { this.Config["EditorAreaCSS"] = value ; }
		}

		[ Category("Configurations") ]
		public string BaseHref
		{
			set { this.Config["BaseHref"] = value ; }
		}

		[ Category("Configurations") ]
		public string SkinPath
		{
			set { this.Config["SkinPath"] = value ; }
		}

		[ Category("Configurations") ]
		public string PluginsPath
		{
			set { this.Config["PluginsPath"] = value ; }
		}

		[ Category("Configurations") ]
		public bool FullPage
		{
			set { this.Config["FullPage"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool Debug
		{
			set { this.Config["Debug"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool AutoDetectLanguage
		{
			set { this.Config["AutoDetectLanguage"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string DefaultLanguage
		{
			set { this.Config["DefaultLanguage"] = value ; }
		}

		[ Category("Configurations") ]
		public LanguageDirection ContentLangDirection
		{
			set { this.Config["ContentLangDirection"] = ( value == LanguageDirection.LeftToRight ? "ltr" : "rtl" )  ; }
		}

		[ Category("Configurations") ]
		public bool EnableXHTML
		{
			set { this.Config["EnableXHTML"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool EnableSourceXHTML
		{
			set { this.Config["EnableSourceXHTML"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool FillEmptyBlocks
		{
			set { this.Config["FillEmptyBlocks"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool FormatSource
		{
			set { this.Config["FormatSource"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool FormatOutput
		{
			set { this.Config["FormatOutput"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string FormatIndentator
		{
			set { this.Config["FormatIndentator"] = value ; }
		}

		[ Category("Configurations") ]
		public bool GeckoUseSPAN
		{
			set { this.Config["GeckoUseSPAN"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool StartupFocus
		{
			set { this.Config["StartupFocus"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ForcePasteAsPlainText
		{
			set { this.Config["ForcePasteAsPlainText"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ForceSimpleAmpersand
		{
			set { this.Config["ForceSimpleAmpersand"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public int TabSpaces
		{
			set { this.Config["TabSpaces"] = value.ToString() ; }
		}

		[ Category("Configurations") ]
		public bool UseBROnCarriageReturn
		{
			set { this.Config["UseBROnCarriageReturn"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ToolbarStartExpanded
		{
			set { this.Config["ToolbarStartExpanded"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public bool ToolbarCanCollapse
		{
			set { this.Config["ToolbarCanCollapse"] = ( value ? "true" : "false" ) ; }
		}

		[ Category("Configurations") ]
		public string FontColors
		{
			set { this.Config["FontColors"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontNames
		{
			set { this.Config["FontNames"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontSizes
		{
			set { this.Config["FontSizes"] = value ; }
		}

		[ Category("Configurations") ]
		public string FontFormats
		{
			set { this.Config["FontFormats"] = value ; }
		}

		[ Category("Configurations") ]
		public string StylesXmlPath
		{
			set { this.Config["StylesXmlPath"] = value ; }
		}

		[ Category("Configurations") ]
		public string LinkBrowserURL
		{
			set { this.Config["LinkBrowserURL"] = value ; }
		}

		[ Category("Configurations") ]
		public string ImageBrowserURL
		{
			set { this.Config["ImageBrowserURL"] = value ; }
		}

		#endregion

		#region Rendering

		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write( "<div>" ) ;

			if ( this.CheckBrowserCompatibility() )
			{
				string sLink = this.BasePath ;
				if ( sLink.StartsWith( "~" ) )
					sLink = this.ResolveUrl( sLink ) ;

				sLink += "editor/fckeditor.html?InstanceName=" + this.ClientID ;
				if ( this.ToolbarSet.Length > 0 ) sLink += "&Toolbar=" + this.ToolbarSet ;

				writer.Write(
					"<input type=\"hidden\" id=\"{0}\" name=\"{1}\" value=\"{2}\">",
						this.ClientID,
						this.UniqueID,
					HttpUtility.HtmlEncode( this.Value ) ) ;

				writer.Write(
					"<input type=\"hidden\" id=\"{0}___Config\" value=\"{1}\">",
						this.ClientID,
						this.Config.GetHiddenFieldString() ) ;

				writer.Write(
					"<iframe id=\"{0}___Frame\" src=\"{1}\" width=\"{2}\" height=\"{3}\" frameborder=\"no\" scrolling=\"no\"></iframe>",
						this.ClientID,
						sLink,
						this.Width,
						this.Height ) ;
			}
			else
			{
				writer.Write(
					"<textarea name=\"{0}\" rows=\"4\" cols=\"40\" style=\"width: {1}; height: {2}\" wrap=\"virtual\">{3}</textarea>",
						this.UniqueID,
						this.Width,
						this.Height,
					HttpUtility.HtmlEncode( this.Value ) ) ;
			}

			writer.Write( "</div>" ) ;
		}

		public bool CheckBrowserCompatibility()
		{
			HttpBrowserCapabilities oBrowser = Page.Request.Browser ;

			if (oBrowser.Browser == "IE" && ( oBrowser.MajorVersion >= 6 || ( oBrowser.MajorVersion == 5 && oBrowser.MinorVersion >= 0.5 ) ) && oBrowser.Win32)
				return true ;
			else
			{
				Match oMatch = Regex.Match( this.Page.Request.UserAgent, @"(?<=Gecko/)\d{8}" ) ;
				return ( oMatch.Success && int.Parse( oMatch.Value, CultureInfo.InvariantCulture ) >= 20030210 ) ;
			}
		}

		#endregion

		#region Postback Handling

		public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if ( postCollection[postDataKey] != this.Value )
			{
				this.Value = postCollection[postDataKey] ;
				return true ;
			}
			return false ;
		}

		public void RaisePostDataChangedEvent()
		{
		}

		#endregion

		#region Tools

		private object IsNull( object valueToCheck, object replacementValue )
		{
			return valueToCheck == null ? replacementValue : valueToCheck ;
		}

		#endregion
	}
}
