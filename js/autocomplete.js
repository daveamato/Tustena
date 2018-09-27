/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function AutoCompleteTextBox(TextBoxId, DivId, DivClass)
{ var oThis = this; var oText = document.getElementById(TextBoxId); var oDiv  = document.createElement('DIV'); oDiv.id = TextBoxId + "_div"; oDiv = oText.parentNode.insertBefore(oDiv, oText.nextSibling); 
this.TextBox = oText; this.Div = oDiv; 
oText.AutoCompleteTextBox = this; oText.aconkeyup = AutoCompleteTextBox.prototype.OnKeyUp; oText.aconkeypress = AutoCompleteTextBox.prototype.OnKeyPress; oText.aconblur  = AutoCompleteTextBox.prototype.OnBlur; XBrowserAddHandler(oText,"keyup","aconkeyup"); XBrowserAddHandler(oText,"keypress","aconkeypress"); XBrowserAddHandler(oText,"blur","aconblur"); 
oDiv.style.display = 'none'; oDiv.style.position = 'absolute'; 
if( DivClass )
oDiv.className = DivClass; else
{ oDiv.style.border = '1'; oDiv.style.borderColor = 'black'; oDiv.style.borderStyle = 'solid'; oDiv.style.backgroundColor = 'white'; oDiv.style.padding = '2'; }
}

AutoCompleteTextBox.prototype.Database = null; AutoCompleteTextBox.prototype.DoAutoSuggest = false; AutoCompleteTextBox.prototype.ListItemClass = ''; AutoCompleteTextBox.prototype.ListItemHoverClass = ''; AutoCompleteTextBox.prototype.CurrentListItem  = 0; AutoCompleteTextBox.prototype.CurrentLenText  = 0; 

AutoCompleteTextBox.prototype.OnBlur = function()
{ AutoCompleteTextBox.Div.style.display='none'; }

AutoCompleteTextBox.prototype.OnKeyPress = function(oEvent)
{ if(AutoCompleteTextBox.Database==null)
return; if (!oEvent)
{ oEvent = window.event; }
var iKeyCode = oEvent.keyCode; if( iKeyCode == 13 ){ AutoCompleteTextBox.TextBox.value = AutoCompleteTextBox.Database[AutoCompleteTextBox.CurrentListItem]; AutoCompleteTextBox.Div.innerHTML = ''; AutoCompleteTextBox.Div.style.display='none'; }
}

AutoCompleteTextBox.prototype.OnKeyUp = function(oEvent)
{ if (!oEvent)
{ oEvent = window.event; }
var iKeyCode = oEvent.keyCode; if( iKeyCode == 8 )
{ AutoCompleteTextBox.Div.innerHTML = ''; AutoCompleteTextBox.Div.style.display = 'none'; return; }
else if( iKeyCode == 16 || iKeyCode == 20 )
{ AutoCompleteTextBox.DoAutoSuggest = true; }
else if(iKeyCode == 40)
{ AutoCompleteTextBox.CurrentListItem++; this.value = this.value.substr(0,AutoCompleteTextBox.CurrentLenText); }
else if(iKeyCode == 38)
{ AutoCompleteTextBox.CurrentListItem--; this.value = this.value.substr(0,AutoCompleteTextBox.CurrentLenText); }
else if (iKeyCode < 32 || (iKeyCode >= 33 && iKeyCode <= 46) || (iKeyCode >= 112 && iKeyCode <= 123))
{ return; }
else
{ AutoCompleteTextBox.DoAutoSuggest = true; }
var txt = this.value; if( txt.length > 0 )
{ if(AutoCompleteTextBox.Database==null || (AutoCompleteTextBox.Database.length>0 && (txt.substr(0,1) != AutoCompleteTextBox.Database[0].substr(0,1)))){ var res = AjaxTextBox(this.name, txt); if(res == null || res.error != null){ if(res != null) alert(res.error ); return; }
var aStr = new Array(); for(var i=0;i<res.value.Tables.Table.Rows.length;i++)
aStr[aStr.length]=res.value.Tables.Table.Rows[i].NAME; AutoCompleteTextBox.Database = aStr; 
var c = GetCoords(this); var n = parseInt( this.offsetHeight, 10 ); if( !n )
{ n = 25; }
else
{ n += 2; }
AutoCompleteTextBox.Div.style.left = c.x; AutoCompleteTextBox.Div.style.top = c.y + n; }
AutoCompleteTextBox.Div.innerHTML = ''; var i, n = AutoCompleteTextBox.Database.length; var aStr = new Array(); var j = 0; if( n > 0 )
{ for ( i = 0; i < n; i++ )
{ if(AutoCompleteTextBox.Database[i].substr(0,txt.length).toUpperCase()!=txt.toUpperCase())
continue; var oDiv = document.createElement('div'); AutoCompleteTextBox.Div.appendChild(oDiv); try
{ oDiv.innerHTML = AutoCompleteTextBox.Database[i]; aStr[j] = AutoCompleteTextBox.Database[i]; if(AutoCompleteTextBox.CurrentListItem == j)
{ oDiv.style.backgroundColor = 'black'; oDiv.style.color = 'white'; }
}
catch(e)
{ debugger; return; }
oDiv.jd			= j; oDiv.noWrap       = true; oDiv.style.width  = '100%'; oDiv.className    = AutoCompleteTextBox.ListItemClass; oDiv.onmousedown  = AutoCompleteTextBox.Div_MouseDown; oDiv.onmouseover  = AutoCompleteTextBox.Div_MouseOver; oDiv.onmouseout   = AutoCompleteTextBox.Div_MouseOut; oDiv.AutoCompleteTextBox = this; j++; }
if(j>0)
AutoCompleteTextBox.Div.style.display = 'block'; else
AutoCompleteTextBox.Div.style.display = 'none'; 
if( AutoCompleteTextBox.DoAutoSuggest == true )
AutoSuggest( aStr ); }
else
{ AutoCompleteTextBox.Div.innerHTML = ''; AutoCompleteTextBox.Div.style.display='none'; }

}else{ AutoCompleteTextBox.Database=null; AutoCompleteTextBox.Div.innerHTML = ''; AutoCompleteTextBox.Div.style.display = 'none'; }
}

AutoCompleteTextBox.prototype.Div_MouseDown = function()
{ AutoCompleteTextBox.TextBox.value = this.innerHTML; AutoCompleteTextBox.CurrentListItem = this.jd; }

AutoCompleteTextBox.prototype.Div_MouseOver = function()
{ if( AutoCompleteTextBox.ListItemHoverClass.length > 0 )
this.className = AutoCompleteTextBox.ListItemHoverClass; else
{ this.style.backgroundColor = 'black'; this.style.color = 'white'; }
}

AutoCompleteTextBox.prototype.Div_MouseOut = function()
{ if( AutoCompleteTextBox.ListItemClass.length > 0 )
this.className = AutoCompleteTextBox.ListItemClass; else
{ this.style.backgroundColor = 'white'; this.style.color = 'black'; }
}

function AutoSuggest(aSuggestions)
{ var acc = AutoCompleteTextBox.TextBox; if (aSuggestions.length > 0 && ( acc.createTextRange || acc.setSelectionRange))
{ var iLen = acc.value.length; acc.value = aSuggestions[0]; marktextrange(acc, iLen, aSuggestions[0].length); AutoCompleteTextBox.CurrentLenText = iLen; }
}

