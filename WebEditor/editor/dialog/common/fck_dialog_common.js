/** FCKeditor - The text editor for internet
* Copyright (C) 2003-2006 Frederico Caldeira Knabben
*
* Licensed under the terms of the GNU Lesser General Public License:
* 		http:
*
* For further information visit:
* 		http:
*
* "Support Open Source software. What about a donation today?"
*
* File Name: fck_dialog_common.js
* 	Useful functions used by almost all dialog window pages.
*
* File Authors:
* 		Frederico Caldeira Knabben (fredck@fckeditor.net)
*/

function GetE( elementId )
{ return document.getElementById( elementId )  ; }

function ShowE( element, isVisible )
{ if ( typeof( element ) == 'string' )
element = GetE( element ) ; element.style.display = isVisible ? '' : 'none' ; }

function SetAttribute( element, attName, attValue )
{ if ( attValue == null || attValue.length == 0 )
element.removeAttribute( attName, 0 ) ; else
element.setAttribute( attName, attValue, 0 ) ; }

function GetAttribute( element, attName, valueIfNull )
{ var oAtt = element.attributes[attName] ; 
if ( oAtt == null || !oAtt.specified )
return valueIfNull ? valueIfNull : '' ; 
var oValue ; 
if ( !( oValue = element.getAttribute( attName, 2 ) ) )
oValue = oAtt.nodeValue ; 
return ( oValue == null ? valueIfNull : oValue ) ; }

function IsDigit( e )
{ e = e || event ; var iCode = ( e.keyCode || e.charCode ) ; 
event.returnValue =
(
( iCode >= 48 && iCode <= 57 )
|| (iCode >= 37 && iCode <= 40)
|| iCode == 8
|| iCode == 46
) ; 
return event.returnValue ; }

String.prototype.trim = function()
{ return this.replace( /(^\s*)|(\s*$)/g, '' ) ; }

String.prototype.startsWith = function( value )
{ return ( this.substr( 0, value.length ) == value ) ; }

String.prototype.remove = function( start, length )
{ var s = '' ; 
if ( start > 0 )
s = this.substring( 0, start ) ; 
if ( start + length < this.length )
s += this.substring( start + length , this.length ) ; 
return s ; }

function OpenFileBrowser( url, width, height )
{ 
var iLeft = ( oEditor.FCKConfig.ScreenWidth  - width ) / 2 ; var iTop  = ( oEditor.FCKConfig.ScreenHeight - height ) / 2 ; 
var sOptions = "toolbar=no,status=no,resizable=yes,dependent=yes" ; sOptions += ",width=" + width ; sOptions += ",height=" + height ; sOptions += ",left=" + iLeft ; sOptions += ",top=" + iTop ; 
if ( oEditor.FCKConfig.PreserveSessionOnFileBrowser && oEditor.FCKBrowserInfo.IsIE )
{ var oWindow = oEditor.window.open( url, 'FCKBrowseWindow', sOptions ) ; 
if ( oWindow )
{ try
{ var sTest = oWindow.name ; // Yahoo returns "something", but we can't access it, so detect that and avoid strange errors for the user.
oWindow.opener = window ; }
catch(e)
{ alert( oEditor.FCKLang.BrowseServerBlocked ) ; }
}
else
alert( oEditor.FCKLang.BrowseServerBlocked ) ; }
else
window.open( url, 'FCKBrowseWindow', sOptions ) ; }
