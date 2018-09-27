/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

var FCKMySaveCommand = function()
{ this.Name = 'My_Save' ; }

FCKMySaveCommand.prototype.Execute = function()
{ if (typeof(window.parent.Save_Click)=='function') window.parent.Save_Click(); }
FCKMySaveCommand.prototype.GetState=function()
{ return FCK_TRISTATE_OFF; }
FCKCommands.RegisterCommand( 'My_Save', new FCKMySaveCommand()); 
var oMySaveItem		= new FCKToolbarButton( 'My_Save', FCKLang['DlgMySaveTitle'] ) ; oMySaveItem.IconPath	= FCKConfig.PluginsPath + 'Save/mysave.gif' ; 
FCKToolbarItems.RegisterItem( 'My_Save', oMySaveItem ) ; 