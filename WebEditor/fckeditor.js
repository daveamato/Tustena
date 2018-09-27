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
* File Name: fckeditor.js
* 	This is the integration file for JavaScript.
*
* 	It defines the FCKeditor class that can be used to create editor
* 	instances in a HTML page in the client side. For server side
* 	operations, use the specific integration system.
*
* File Authors:
* 		Frederico Caldeira Knabben (fredck@fckeditor.net)
*/

var FCKeditor = function( instanceName, width, height, toolbarSet, value )
{ this.InstanceName	= instanceName ; this.Width			= width			|| '100%' ; this.Height			= height		|| '200' ; this.ToolbarSet		= toolbarSet	|| 'Default' ; this.Value			= value			|| '' ; this.BasePath		= '/fckeditor/' ; this.CheckBrowser	= true ; this.DisplayErrors	= true ; this.EnableSafari	= false ; this.EnableOpera	= false ; 
this.Config			= new Object() ; 
this.OnError		= null ; }

FCKeditor.prototype.Version			= '2.3' ; FCKeditor.prototype.VersionBuild	= '1054' ; 
FCKeditor.prototype.Create = function()
{ if ( !this.InstanceName || this.InstanceName.length == 0 )
{ this._ThrowError( 701, 'You must specify an instance name.' ) ; return ; }

document.write( '<div>' ) ; 
if ( !this.CheckBrowser || this._IsCompatibleBrowser() )
{ document.write( '<input type="hidden" id="' + this.InstanceName + '" name="' + this.InstanceName + '" value="' + this._HTMLEncode( this.Value ) + '" style="display:none" />' ) ; document.write( this._GetConfigHtml() ) ; document.write( this._GetIFrameHtml() ) ; }
else
{ var sWidth  = this.Width.toString().indexOf('%')  > 0 ? this.Width  : this.Width  + 'px' ; var sHeight = this.Height.toString().indexOf('%') > 0 ? this.Height : this.Height + 'px' ; document.write('<textarea name="' + this.InstanceName + '" rows="4" cols="40" style="WIDTH: ' + sWidth + '; HEIGHT: ' + sHeight + '">' + this._HTMLEncode( this.Value ) + '<\/textarea>') ; }

document.write( '</div>' ) ; }

FCKeditor.prototype.ReplaceTextarea = function()
{ if ( !this.CheckBrowser || this._IsCompatibleBrowser() )
{ var oTextarea = document.getElementById( this.InstanceName ) ; var colElementsByName = document.getElementsByName( this.InstanceName ) ; var i = 0; while ( oTextarea || i == 0 )
{ if ( oTextarea && oTextarea.tagName == 'TEXTAREA' )
break ; oTextarea = colElementsByName[i++] ; }

if ( !oTextarea )
{ alert( 'Error: The TEXTAREA with id or name set to "' + this.InstanceName + '" was not found' ) ; return ; }

oTextarea.style.display = 'none' ; this._InsertHtmlBefore( this._GetConfigHtml(), oTextarea ) ; this._InsertHtmlBefore( this._GetIFrameHtml(), oTextarea ) ; }
}

FCKeditor.prototype._InsertHtmlBefore = function( html, element )
{ if ( element.insertAdjacentHTML )
element.insertAdjacentHTML( 'beforeBegin', html ) ; else
{ var oRange = document.createRange() ; oRange.setStartBefore( element ) ; var oFragment = oRange.createContextualFragment( html ); element.parentNode.insertBefore( oFragment, element ) ; }
}

FCKeditor.prototype._GetConfigHtml = function()
{ var sConfig = '' ; for ( var o in this.Config )
{ if ( sConfig.length > 0 ) sConfig += '&amp;' ; sConfig += escape(o) + '=' + escape( this.Config[o] ) ; }

return '<input type="hidden" id="' + this.InstanceName + '___Config" value="' + sConfig + '" style="display:none" />' ; }

FCKeditor.prototype._GetIFrameHtml = function()
{ var sFile = (/fcksource=true/i).test( window.top.location.search ) ? 'fckeditor.original.html' : 'fckeditor.html' ; 
var sLink = this.BasePath + 'editor/' + sFile + '?InstanceName=' + this.InstanceName ; if (this.ToolbarSet) sLink += '&Toolbar=' + this.ToolbarSet ; 
return '<iframe id="' + this.InstanceName + '___Frame" src="' + sLink + '" width="' + this.Width + '" height="' + this.Height + '" frameborder="0" scrolling="no"></iframe>' ; }

FCKeditor.prototype._IsCompatibleBrowser = function()
{ var sAgent = navigator.userAgent.toLowerCase() ; 
if ( sAgent.indexOf("msie") != -1 && sAgent.indexOf("mac") == -1 && sAgent.indexOf("opera") == -1 )
{ var sBrowserVersion = navigator.appVersion.match(/MSIE (.\..)/)[1] ; return ( sBrowserVersion >= 5.5 ) ; }

if ( navigator.product == "Gecko" && navigator.productSub >= 20030210 )
return true ; 
if ( this.EnableOpera )
{ var aMatch = sAgent.match( /^opera\/(\d+\.\d+)/ ) ; if ( aMatch && aMatch[1] >= 9.0 )
return true ; }

if ( this.EnableSafari && sAgent.indexOf( 'safari' ) != -1 )
return ( sAgent.match( /safari\/(\d+)/ )[1] >= 312 ) ; 
return false ; }

FCKeditor.prototype._ThrowError = function( errorNumber, errorDescription )
{ this.ErrorNumber		= errorNumber ; this.ErrorDescription	= errorDescription ; 
if ( this.DisplayErrors )
{ document.write( '<div style="COLOR: #ff0000">' ) ; document.write( '[ FCKeditor Error ' + this.ErrorNumber + ': ' + this.ErrorDescription + ' ]' ) ; document.write( '</div>' ) ; }

if ( typeof( this.OnError ) == 'function' )
this.OnError( this, errorNumber, errorDescription ) ; }

FCKeditor.prototype._HTMLEncode = function( text )
{ if ( typeof( text ) != "string" )
text = text.toString() ; 
text = text.replace(/&/g, "&amp;") ; text = text.replace(/"/g, "&quot;") ; text = text.replace(/</g, "&lt;") ; text = text.replace(/>/g, "&gt;") ; text = text.replace(/'/g, "&#39;") ; 
return text ; }
