/** FCKeditor - The text editor for internet
* Copyright (C) 2003-2005 Frederico Caldeira Knabben
*
* Licensed under the terms of the GNU Lesser General Public License:
* 		http:
*
* For further information visit:
* 		http:
*
* "Support Open Source software. What about a donation today?"
*
* File Name: fckconfig.js
* 	Editor configuration settings.
* 	See the documentation for more info.
*
* File Authors:
* 		Frederico Caldeira Knabben (fredck@fckeditor.net)
*/

FCKConfig.CustomConfigurationsPath = '' ; 
FCKConfig.EditorAreaCSS = FCKConfig.BasePath + 'css/fck_editorarea.css' ; 
FCKConfig.DocType = '<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">' ; 
FCKConfig.BaseHref = '' ; 
FCKConfig.FullPage = true; 
FCKConfig.Debug = false ; 
FCKConfig.SkinPath = FCKConfig.BasePath + 'skins/default/' ; FCKConfig.PreloadImages = [ FCKConfig.SkinPath + 'images/toolbar.start.gif', FCKConfig.SkinPath + 'images/toolbar.buttonarrow.gif' ] ; FCKConfig.PluginsPath = FCKConfig.BasePath + 'plugins/' ; FCKConfig.AutoGrowMax = 400 ; 
FCKConfig.ProtectedSource.Add( /<script[\s\S]*?\/script>/gi ) ; 
FCKConfig.AutoDetectLanguage	= true ; FCKConfig.DefaultLanguage	= 'en' ; FCKConfig.ContentLangDirection	= 'ltr' ; 
FCKConfig.EnableXHTML		= true ; FCKConfig.EnableSourceXHTML	= true ; 
FCKConfig.ProcessHTMLEntities	= true ; FCKConfig.IncludeLatinEntities	= true ; FCKConfig.IncludeGreekEntities	= true ; 
FCKConfig.FillEmptyBlocks	= true ; 
FCKConfig.FormatSource		= true ; FCKConfig.FormatOutput		= true ; FCKConfig.FormatIndentator	= '    ' ; 
FCKConfig.ForceStrongEm = true ; FCKConfig.GeckoUseSPAN	= false ; FCKConfig.StartupFocus	= false ; FCKConfig.ForcePasteAsPlainText	= false ; FCKConfig.AutoDetectPasteFromWord = true ; FCKConfig.ForceSimpleAmpersand	= false ; FCKConfig.TabSpaces		= 0 ; FCKConfig.ShowBorders	= true ; FCKConfig.SourcePopup	= false ; FCKConfig.UseBROnCarriageReturn	= true ; FCKConfig.ToolbarStartExpanded	= true ; FCKConfig.ToolbarCanCollapse	= true ; FCKConfig.IEForceVScroll = false ; FCKConfig.IgnoreEmptyParagraphValue = true ; FCKConfig.PreserveSessionOnFileBrowser = false ; FCKConfig.FloatingPanelsZIndex = 10000 ; 
FCKConfig.ToolbarLocation = 'In' ; 
FCKConfig.ToolbarSets["Default"] = [
['My_Save','DocProps','-','NewPage','Preview','-'],
['Cut','Copy','Paste','PasteText','PasteWord','-','Print','SpellCheck'],
['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
['Bold','Italic','Underline','StrikeThrough','-','Subscript','Superscript'],
['OrderedList','UnorderedList','-','Outdent','Indent'],
['JustifyLeft','JustifyCenter','JustifyRight','JustifyFull'],
['Link','Unlink','Anchor'],
['Image','Table','Rule','Smiley','SpecialChar','PageBreak','UniversalKey'],
['Style','FontFormat','FontName','FontSize'],
['TextColor','BGColor','Source','FitWindow']
] ; 
FCKConfig.ToolbarSets["Basic"] = [
['Bold','Italic','-','OrderedList','UnorderedList','-','Link','Unlink','-','About']
] ; 
FCKConfig.ContextMenu = ['Generic','Link','Anchor','Image','Flash','Select','Textarea','Checkbox','Radio','TextField','HiddenField','ImageButton','Button','BulletedList','NumberedList','TableCell','Table','Form'] ; 
FCKConfig.FontColors = '000000,993300,333300,003300,003366,000080,333399,333333,800000,FF6600,808000,808080,008080,0000FF,666699,808080,FF0000,FF9900,99CC00,339966,33CCCC,3366FF,800080,999999,FF00FF,FFCC00,FFFF00,00FF00,00FFFF,00CCFF,993366,C0C0C0,FF99CC,FFCC99,FFFF99,CCFFCC,CCFFFF,99CCFF,CC99FF,FFFFFF' ; 
FCKConfig.FontNames		= 'Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana' ; FCKConfig.FontSizes		= '1/xx-small;2/x-small;3/small;4/medium;5/large;6/x-large;7/xx-large' ; FCKConfig.FontFormats	= 'p;div;pre;address;h1;h2;h3;h4;h5;h6' ; 
FCKConfig.StylesXmlPath		= FCKConfig.EditorPath + 'fckstyles.xml' ; FCKConfig.TemplatesXmlPath	= FCKConfig.EditorPath + 'fcktemplates.xml' ; 
FCKConfig.SpellChecker			= 'ieSpell' ;	// 'ieSpell' | 'SpellerPages'
FCKConfig.IeSpellDownloadUrl	= 'http://www.iespell.com/rel/ieSpellSetup220647.exe' ; 
FCKConfig.MaxUndoLevels = 15 ; 
FCKConfig.DisableImageHandles = false ; FCKConfig.DisableTableHandles = true ; 
FCKConfig.LinkDlgHideTarget		= false ; FCKConfig.LinkDlgHideAdvanced	= false ; 
FCKConfig.ImageDlgHideLink		= false ; FCKConfig.ImageDlgHideAdvanced	= false ; 
FCKConfig.FlashDlgHideAdvanced	= false ; 
FCKConfig.LinkBrowser = true ; FCKConfig.LinkBrowserURL = FCKConfig.BasePath + "filemanager/browser/default/browser.html?/webeditor/webeditorconnector.aspx" ; FCKConfig.LinkBrowserWindowWidth	= screen.width * 0.7 ; FCKConfig.LinkBrowserWindowHeight	= screen.height * 0.7 ; 
FCKConfig.ImageBrowser = true ; FCKConfig.ImageBrowserURL = FCKConfig.BasePath + "filemanager/browser/default/browser.html?Type=Image&Connector=/webeditor/webeditorconnector.aspx" ; FCKConfig.ImageBrowserWindowWidth  = FCKConfig.ScreenWidth * 0.7 ; FCKConfig.ImageBrowserWindowHeight = FCKConfig.ScreenHeight * 0.7 ; 
FCKConfig.FlashBrowser = false ; FCKConfig.FlashBrowserURL = FCKConfig.BasePath + 'filemanager/browser/default/browser.html?Type=Flash&Connector=connectors/asp/connector.asp' ; FCKConfig.FlashBrowserWindowWidth  = FCKConfig.ScreenWidth * 0.7 ; FCKConfig.FlashBrowserWindowHeight = FCKConfig.ScreenHeight * 0.7 ; 
FCKConfig.LinkUpload = false ; FCKConfig.LinkUploadURL = FCKConfig.BasePath + 'filemanager/upload/asp/upload.asp' ; FCKConfig.LinkUploadAllowedExtensions	= "" ; FCKConfig.LinkUploadDeniedExtensions	= ".(php|php3|php5|phtml|asp|aspx|ascx|jsp|cfm|cfc|pl|bat|exe|dll|reg|cgi)$" ; 
FCKConfig.ImageUpload = false ; FCKConfig.ImageUploadURL = FCKConfig.BasePath + 'filemanager/upload/asp/upload.asp?Type=Image' ; FCKConfig.ImageUploadAllowedExtensions	= ".(jpg|gif|jpeg|png)$" ; FCKConfig.ImageUploadDeniedExtensions	= "" ; 
FCKConfig.FlashUpload = false ; FCKConfig.FlashUploadURL = FCKConfig.BasePath + 'filemanager/upload/asp/upload.asp?Type=Flash' ; FCKConfig.FlashUploadAllowedExtensions	= ".(swf|fla)$" ; FCKConfig.FlashUploadDeniedExtensions	= "" ; 
FCKConfig.SmileyPath	= FCKConfig.BasePath + 'images/smiley/msn/' ; FCKConfig.SmileyImages	= ['regular_smile.gif','sad_smile.gif','wink_smile.gif','teeth_smile.gif','confused_smile.gif','tounge_smile.gif','embaressed_smile.gif','omg_smile.gif','whatchutalkingabout_smile.gif','angry_smile.gif','angel_smile.gif','shades_smile.gif','devil_smile.gif','cry_smile.gif','lightbulb.gif','thumbs_down.gif','thumbs_up.gif','heart.gif','broken_heart.gif','kiss.gif','envelope.gif'] ; FCKConfig.SmileyColumns = 8 ; FCKConfig.SmileyWindowWidth		= 320 ; FCKConfig.SmileyWindowHeight	= 240 ; 
FCKConfig.Plugins.Add( 'Save', 'en,it' ) ; 