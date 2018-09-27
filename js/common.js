/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/
isMac = (navigator.appVersion.indexOf("Mac")!=-1) ? true : false; NS4 = (document.layers) ? true : false; IEmac = ((document.all)&&(isMac)) ? true : false; safari = ((navigator.userAgent.indexOf("Safari")!=-1)&&(isMac)) ? true : false; IE4plus = (document.all) ? true : false; IE4 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 4.")!=-1)) ? true : false; IE5 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 5.")!=-1)) ? true : false; IE6 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 6.")!=-1)) ? true : false; IE6SP2 = ((document.all)&&(navigator.appVersion.indexOf("MSIE 6.")!=-1)&&(window.navigator.userAgent.indexOf("SV1") != -1)) ? true : false; ver4 = (NS4 || IE4plus) ? true : false; NS6 = document.getElementById&&!document.all ? true : false; OPERAMINI = (screen.height == 5000 && screen.availHeight == 5000 ) ? true : false; 

function errorHandler(msg,url,lno){ var errImage = new Image; var xy = "NS"; if(IE4plus) xy = event.clientX + "|" + event.clientY; errImage.src = "/error/jserror.aspx?error="+escape("Message:"+msg+"<br>\nUrl:"+url+"<br>\nLine:"+lno+"<br>\nCoords:"+xy); window.status="Javascript error:"+msg+" * Url:"+url+" * Line:"+lno; return true; }

var gSafeOnload = new Array(); 
function SafeAddOnload(f)
{ if (IEmac && IE4)
{ window.onload = SafeOnload; gSafeOnload[gSafeOnload.length] = f; }
else if  (window.onload)
{ if (window.onload != SafeOnload)
{ gSafeOnload[0] = window.onload; window.onload = SafeOnload; }
gSafeOnload[gSafeOnload.length] = f; }
else
window.onload = f; }
function SafeOnload()
{ for (var i=0;i<gSafeOnload.length;i++)
gSafeOnload[i](); 
}

function FixRedCrossold(){ if (!IE6 || IE6SP2) return; var aImages = document.getElementsByTagName('img'); for (var i = 0; i < aImages.length; i++) { aImages[i].src=aImages[i].src; }
}

function FixRedCross(){ if (!IE6) return; var imageLoadOk = true; try { for (var i = 0; i < document.images.length; i++) { if (!document.images[i].mimeType) { document.images[i].src = document.images[i].src; imageLoadOk = false; }
}
} catch(arg) {   imageLoadOk = false; } if (!imageLoadOk) setTimeout('FixRedCross();', 1000); }


SafeAddOnload(FixRedCross); 
var tcount=0; function SessionUp(interval,max){ if (SessiontimerID) clearTimeout(SessiontimerID); if (tcount < max)
{ if(tcount>0)
{ var SessionImage = new Image; SessionImage.src = "/session.aspx"; }
tcount++; window.setTimeout('SessionUp('+interval+','+max+')',interval); }else{ SessionCheck(); }
}
function SessionCheck(){ if (ObjExists('Last30'))
{ NonBlockingAlert(Last30,true); setTimeout("location.href='/login.aspx?timeout=true'", 30000); }

}
var SessiontimerID = null; if (ObjExists('SessionTimeout')){ SessiontimerID = setTimeout("SessionCheck()", SessionTimeout-30000); }

function ObjExists(objId){ if ( typeof( window[ objId ] ) != "undefined" )
return true; return false; }

function NonBlockingAlert(textMsg,isOk,timer)
{ if(typeof( textMsg )=='undefined') return; var okBtn; if(isOk){ okBtn = '<div style="margin-top:5px;cursor:pointer;width:50px;padding:2px;font-size:12px;border:1px solid black;" onclick="document.getElementById(\'nbAlert\').style.visibility=\'hidden\'">OK</div>'; textMsg += okBtn; }
var alertmsg = '<table id="nbAlert" style="z-index:5;position:absolute;top:0;height:100%;width:100%;text-align:center;vertical-align:middle;filter: DropShadow(Color=#333333, OffX=1, OffY=1, Positive=true)"><tr><td><table style="text-align:center;vertical-align:middle;"><tr><td style="background-color:silver;font-family:arial;font-size:15px;font-weight : bold;color:#222222;padding:10 20 10 20">'+textMsg+'</td></tr></table></td></tr></table>'; var elm = document.createElement('div'); elm.innerHTML=alertmsg; document.body.appendChild(elm); if((typeof( timer )!='undefined') && timer>0) setTimeout('document.getElementById(\'nbAlert\').style.visibility=\'hidden\'', timer)
}

function FirstLastTable()
{ var aTables = document.getElementsByTagName('table'); for (var i = 0; i < aTables.length; i++)
{ try{ if(aTables[i].firstChild.firstChild.firstChild.className != "GridTitle")
continue; }catch(e){continue;}
var aTrs = aTables[i].getElementsByTagName('tr'); { for (var k = 0; k < aTrs.length; k++)
{ var current = aTrs[k].firstChild.className; if(aTrs[k].childNodes.length==1)
{ aTrs[k].firstChild.className += " " + current + "first " + current + "last"; }else{ aTrs[k].firstChild.className += " " + current + "first"; aTrs[k].lastChild.className += " " + current + "last"; }
}
}
}
}


function initTabs(e) { 
if (!document.getElementById) return; if (!window.event) { window.event = e; }
var sTempSrc,prevcolor,setfocus,debugtxt; var aInputs = document.getElementsByTagName('input'); var aTextAreas = document.getElementsByTagName('textarea'); for (var i = 0; i < aTextAreas.length; i++)
{ if (aTextAreas[i].disabled==false && aTextAreas[i].style.display=="") { if(aTextAreas[i].getAttribute('readonly')!=true && aTextAreas[i].getAttribute('readonly')!='readonly'){ prevcolor = aTextAreas[i].style.borderColor; aTextAreas[i].onfocus=function(e){this.style.borderColor='#FF0000';}; aTextAreas[i].onblur=function(e){this.style.borderColor=prevcolor;}; if((!setfocus) && aTextAreas[i].getAttribute('startfocus')!=null){ setfocus=true;aTextAreas[i].focus();}; }else
if(aTextAreas[i].className.substr(aTextAreas[i].className.length-3)=="Req")
aTextAreas[i].style.background='#FFFCED'; else
aTextAreas[i].style.background='#EEEEEE'; }
}
for (var i = 0; i < aInputs.length; i++) { if (aInputs[i].getAttribute('type') == 'text' || aInputs[i].getAttribute('type') == 'password')
if(!(aInputs[i].disabled==false && aInputs[i].style.display=="" && (aInputs[i].getAttribute('readonly')!=true && aInputs[i].getAttribute('readonly')!='readonly')))
{ if(aInputs[i].className.substr(aInputs[i].className.length-3)=="Req")
aInputs[i].style.background='#FFFCED'; else
aInputs[i].style.background='#EEEEEE'; aInputs[i].tabIndex=-1; }else{ prevcolor = aInputs[i].style.borderColor; if(aInputs[i].getAttribute('markrange')!=null)
aInputs[i].addonfocus=function(e){this.style.borderColor='#FF0000';marktextrange(this,0,this.length);}; else
aInputs[i].addonfocus=function(e){this.style.borderColor='#FF0000';}; aInputs[i].onblur=function(e){this.style.borderColor=prevcolor;}; if((!setfocus) && aInputs[i].getAttribute('startfocus')!=null){setfocus=true;aInputs[i].focus();}; sTempSrc=i; if(aInputs[i].getAttribute('jumpret')!=null){ try{ eval("aInputs[i].addonkeypress=function(e){if(xKey(e)==13 || xKey(e)==9){ document.getElementById(aInputs[" + i + "].getAttribute('jumpret')).focus();PreventDef(e);}}"); }catch(ex){}
XBrowserAddHandler(aInputs[i],"keypress","addonkeypress"); continue; }
if(aInputs[i].getAttribute('autoclick')!=null)
eval("aInputs[i].addonkeypress=function(e){if(xKey(e)==13){ eval(unescape(document.getElementById(aInputs[" + i + "].getAttribute('autoclick')).href));PreventDef(e);}}"); else
while(aInputs[++sTempSrc]!=null){ if(aInputs[sTempSrc].getAttribute('type') == 'text' && aInputs[sTempSrc].disabled==false && aInputs[sTempSrc].style.display=="" && aInputs[sTempSrc].getAttribute('readonly')!=true)
{ if(aInputs[i].getAttribute('noret')==null)
try{ eval("aInputs[i].addonkeypress=function(e){if(xKey(e)==13 || xKey(e)==9){ document.getElementsByTagName('input')[" + sTempSrc + "].focus();PreventDef(e);}}"); }catch(ex){}
else
eval("aInputs[i].addonkeypress=function(e){if(xKey(e)==13){PreventDef(e);}}"); break; }
}
if(aInputs[i].addonkeypress) XBrowserAddHandler(aInputs[i],"keypress","addonkeypress"); if(aInputs[i].addonfocus) XBrowserAddHandler(aInputs[i],"focus","addonfocus"); }
}
}

function textareajump(e,next)
{ if(xKey(e)==13)
{ document.getElementById(next).focus(); PreventDef(e); }
}

function PreventDef(e)
{ if(e==null)e=event; if (e.preventDefault) { e.preventDefault(); } else { e.returnValue = false; }
}

function NumbersOnly(e,validdiv,obj){ if(validdiv.indexOf('.,')>-1 && ObjExists('CurrencyPattern'))
validdiv = CurrencyPattern; var validnum = '0123456789' + validdiv; var keypressed = String.fromCharCode(xKey(e)); if (validnum.indexOf(keypressed) == -1)
PreventDef(e); if(validdiv.indexOf(keypressed) > -1 && obj!=null)
for (var i = 0; i < obj.value.length; i++)
if (validdiv.indexOf(obj.value.charAt(i)) > -1)
PreventDef(e); }
function marktextrange(o, iStart, iLength)
{ if (o.createTextRange)
{ var oRange = o.createTextRange(); oRange.moveStart("character", iStart); oRange.moveEnd("character", iLength - o.value.length); oRange.select(); 
}
else if (o.setSelectionRange)
{ o.setSelectionRange(iStart, iLength); }

o.focus(); }


function XBrowserAddHandler(target,eventName,handlerName) { if ( target.addEventListener ) { target.addEventListener(eventName, function(e){target[handlerName](e);}, false); } else if ( target.attachEvent ) { target.attachEvent("on" + eventName, function(e){target[handlerName](e);}); } else { var originalHandler = target["on" + eventName]; if ( originalHandler ) { target["on" + eventName] = function(e){originalHandler(e);target[handlerName](e);}; } else { target["on" + eventName] = target[handlerName]; }
}
}

var GoesUp = false; function FirstUp(obj,e){ if(e.shiftKey||e.ctrlKey)
return; var mykey = xKey(e); var tmpLen = obj.value.length; if (tmpLen==0||GoesUp==true){ PreventDef(e); obj.value += String.fromCharCode(mykey).toUpperCase(); }
if (mykey==32)
GoesUp = true; else
GoesUp = false; }

function xKey(e) { eventChooser = (document.all) ? event.keyCode : e.which; which = String.fromCharCode(eventChooser); return which.charCodeAt(0); }

SafeAddOnload(initTabs); SafeAddOnload(FirstLastTable); 
function NewSelectReplace(name){ obj = document.getElementById(name); if (obj.options.length==0) return; var pos = FindXY(obj); ow = parseInt( obj.offsetWidth, 10 ); oh = parseInt( obj.offsetHeight, 10 ); obj.style.visibility='hidden'; var nsr = CreateDIV('new' + name); nsr.style.cssText='border:solid 1px silver; background-color:#ffffff;'; nsr.style.height = oh; nsr.style.width = ow; nsr.noWrap = true; nsr.onmousemove= DivTrackMouse; nsr.innerHTML = '<span id=\'new' + name + 'span\' style=\'text-decoration:none;padding-left:1px;overflow:hidden;text-align:left;width:' + (ow-17) + ';font-family: verdana;cursor:pointer;font-size:11px\' onclick=\'OpenDown("'+name+'")\'>' + obj.options[obj.selectedIndex].text + '&nbsp;</span><span style=\'cursor:pointer;width:15px;text-align:center;text-decoration:none;font-family:Webdings;font-size:11px;background:gainsboro;\' onclick=\'OpenDown("'+name+'")\'>6</span>'; obj.style.display='none'; obj.parentNode.insertBefore(nsr,obj); }

function NewSelectResize(){ for (var i = 0; i < selectrecheck.length; i++)
{ if(selectrecheck[i].id==null || selectrecheck[i].id.length==0) continue; var obj = document.getElementById(selectrecheck[i].id); var newobj = document.getElementById('new'+selectrecheck[i].id); if(newobj!=null && obj!=null)
{ obj.style.display=''; var newow = parseInt( newobj.offsetWidth, 10 ); newobj.style.display='none'; var ow = parseInt( obj.offsetWidth, 10 ); if(ow!=newow){ newobj.style.width=ow; newobj.innerHTML = '<span id=\'new' +  selectrecheck[i].id + 'span\' style=\'text-decoration:none;padding-left:1px;overflow:hidden;text-align:left;width:' + (ow-17) + ';font-family: verdana;cursor:pointer;font-size:10px\' onclick=\'OpenDown("'+ selectrecheck[i].id+'")\'>' + obj.options[obj.selectedIndex].text + '&nbsp;</span><span style=\'cursor:pointer;width:15px;text-align:center;text-decoration:none;font-family:Webdings;font-size:11px;background:gainsboro;\' onclick=\'OpenDown("'+ selectrecheck[i].id+'")\'>6</span>'; }
obj.style.display='none'; newobj.style.display=''; }
}
}

function CreateDIV(name){ var elm = document.createElement('div'); elm.id = name; document.body.appendChild(elm); return document.getElementById(name); }

function FindXY(obj){ if ( document.layers ) { return { x:obj.x, y:obj.y }; }
var res = { x:0,y:0 }; while ( obj ) { res.x += parseInt( obj.offsetLeft, 10 ); res.y += parseInt( obj.offsetTop, 10 ); obj = obj.offsetParent; }
return res; }

var isOpen = false; function OpenDown(objname){ if(isOpen==false){ isOpen=true; var obj = document.getElementById(objname); if(obj.onclick) obj.fireEvent('onclick'); var obj2 = document.getElementById('new'+objname); var oh = parseInt( obj2.offsetHeight, 10 ); var wh = document.body.clientHeight - 1; var st = document.body.scrollTop; var pos = FindXY(obj2); nsdd = CreateDIV('NSDD'); nsdd.style.cssText='position:absolute;border:solid 1px silver; background-color:#ffffff;scrollbar-face-color: gainsboro;scrollbar-highlight-color: #ffffff;scrollbar-3dlight-color: #silver;scrollbar-darkshadow-color: #ffffff;scrollbar-shadow-color: silver;scrollbar-arrow-color: #000000;scrollbar-track-color: #ffffff;'; nsdd.style.left = pos.x; nsdd.style.width = parseInt( obj2.offsetWidth, 10 ); nsdd.style.overflowY = 'auto'; nsdd.onmousemove= DivTrackMouse; var innerStr='<table width=100% cellspacing=0>'; for(i=0;i<obj.options.length;i++)
innerStr+= '<tr><td nowrap style=\'cursor:pointer;font-family: verdana;font-size:10px\' width=\'100%\' onclick=\'OpenDownClick("' + obj.id + '",' + nsdd.id + ',' + i + ')\' onmouseover=\'this.className="CommonColor"\' onmouseout=\'this.className="CommonColorRevert"\'>' + obj.options[i].text + '</td></tr>'; nsdd.innerHTML = innerStr + '</table>'; if((pos.y-st+nsdd.scrollHeight)>wh && (pos.y-st)>((wh-oh)/2)){ nsdd.style.top = pos.y - nsdd.scrollHeight; if(nsdd.scrollHeight>pos.y)
nsdd.style.height = pos.y; }else{ nsdd.style.top = pos.y + oh - 1; var clientH = wh + st - oh - pos.y; if(nsdd.scrollHeight>clientH)
nsdd.style.height = clientH; }
nsdd.focus(); }
}

function TrackMouse(){ if(isOpen==true){ if(window.event.clientX!=DivTMx){ window.document.body.removeChild(nsdd); isOpen=false; }
}
}

var DivTMx=0;DivTMy=0; function DivTrackMouse(){ DivTMx = window.event.clientX; DivTMy = window.event.clientY; }

function OpenDownClick(name,name2,line){ eval('document.getElementById(\'new' + name + 'span\').innerHTML=document.getElementById(\'' + name + '\').options[' + line + '].text'); document.getElementById(name).selectedIndex=line; 
if(document.getElementById(name).onchange) document.getElementById(name).fireEvent('onchange'); document.body.removeChild(name2); isOpen=false; }
var selectrecheck = new Array(); function NewSelectInit(){ if(!document.getElementById || isMac) return; window.document.body.onmousemove=TrackMouse; window.onresize=NewSelectResize; var selectarr = document.getElementsByTagName('SELECT'); for (var i = 0; i < selectarr.length; i++)
{ if (selectarr[i].disabled==false&&selectarr[i].multiple==false&&selectarr[i].id!=''&&selectarr[i].getAttribute('old')==null)
{ NewSelectReplace(selectarr[i].id); selectrecheck[selectrecheck.length]=selectarr[i]; }
}
}

if(IE6) SafeAddOnload(NewSelectInit); 
function highlightTable(tName){ var htable = document.getElementById(tName); var hrows = htable.getElementsByTagName('tr'); for (i=1; i<hrows.length; i++ ) { hrows[i].onmouseover  = function() { this.style.backgroundColor='#FFE87C';}; hrows[i].onmouseout = function() { this.style.backgroundColor='';}
}
}

var mypostback; function AddPostbackFunction(func){ if(typeof( __doPostBack )=='undefined')return; mypostback=__doPostBack; eval("__doPostBack=function(e){"+func+";mypostback(e)};"); }

function JsPostback(aStr,post,newurl)
{ var arr = aStr.split(","); for (var i=0; i < arr.length; i++)
{ var arr2 = arr[i].split("="); if(arr2.length==2){ if(document.getElementById(arr2[0])==null){ elm=document.createElement('input'); elm.setAttribute("type", "text"); elm.style.display='none'; elm.setAttribute("value", arr2[1]); elm.id = arr2[0]; elm.name = arr2[0]; document.forms[0].appendChild(elm); }else{ document.getElementById(arr2[0]).value = arr2[1]; }
}}
if(post)
{ if(newurl)
document.forms[0].action = newurl; document.forms[0].submit(); }
}

function getElement(objectID){ return IE4plus? document.all[objectID] : NS6? document.getElementById(objectID) : NS4? document.layers[objectID] : ""; }

document.onhelp = function () { NewWindow("Help.htm","TustenaHelp",200,200,"yes"); return false; }; 
function NewWindow(mypage, myname, w, h, scroll) { var winl = (screen.width - w) / 2; var wint = (screen.height - h) / 2; winprops = 'height='+h+',width='+w+',top='+wint+',left='+winl+',scrollbars='+scroll+'fullscreen=no,toolbar=no,status=no,menubar=no,resizable=no,directories=no,location=no'; win = window.open(mypage, myname, winprops); if (parseInt(navigator.appVersion) >= 4) { win.window.focus(); }
}
function loadScript(){ if (!document.getElementById)
return
for (i=0; i<arguments.length; i++){ if (typeof arguments[i] != "string")
continue; var file=arguments[i]
var fileref=""
if (file.indexOf(".css")!=-1){ fileref=document.createElement("link")
fileref.setAttribute("rel", "stylesheet"); fileref.setAttribute("type", "text/css"); fileref.setAttribute("href", file); }
else{ fileref=document.createElement('script')
fileref.setAttribute("type","text/javascript"); fileref.setAttribute("src", file); if (i<arguments.length-1 && typeof arguments[i+1] == "function"){ func = arguments[i+1]; if(document.all) { fileref.onreadystatechange = function() { if (this.readyState == 'complete') { func(); }}} else{ fileref.onload = func; }
}
}
document.getElementsByTagName("head").item(0).appendChild(fileref)
}
}

function loadJsScript(url, id){ if(id!=null && !ObjExists(id))
if( document.createElement && document.childNodes ) { if(IE4plus)
document.write('<sc' + 'ript type=\'text/javascript\' src=\'' + url + '\'><\/sc' + 'ript>'); else{ var scriptElem = document.createElement('script'); scriptElem.setAttribute('src',url); scriptElem.setAttribute('type','text/javascript'); document.getElementsByTagName('head')[0].appendChild(scriptElem); }}
}

function ViewAgenda(){ var ob = document.getElementById("ViewAgenda"); if(ob!=null)
clickElement(ob); else
try{ window.location.href="/Calendar/agenda.aspx?m=25&si=2"; }catch(ex){}
}

function dynaret(txt){ if(opener){ return opener.document.getElementById(txt); }else if(parent){ return parent.document.getElementById(txt); }}

function dynaevent(func){ if(opener){ eval('opener.'+func); }else if(parent){ eval('parent.'+func); }}

var savedarr = new Array(); var stopsave=true; function needsave(message){ 
if(document.getElementById){ if(message=="no"){ stopsave=false; return; }
if(message==""){ var oldarr = savedarr; savedarr = new Array(); }else
window.onbeforeunload=function(){if(needsave(""))return message;}; var inparr = document.getElementsByTagName('input'); var selarr = document.getElementsByTagName('select'); var areaarr = document.getElementsByTagName('textarea'); for (var i=0; i < inparr.length; i++){ if (inparr[i].type == 'text')
savedarr[savedarr.length] = inparr[i].value; if (inparr[i].type == 'checkbox' || inparr[i].type == 'radio')
savedarr[savedarr.length] = inparr[i].checked; }
for (var i = 0; i < selarr.length; i++)
savedarr[savedarr.length] = selarr[i].selectedIndex; for (var i = 0; i < areaarr.length; i++)
savedarr[savedarr.length] = areaarr[i].value; if(message=="" && stopsave){ for (var i=0; i < oldarr.length; i++)
if(oldarr[i]!=savedarr[i])
return true; }
return false; }
}

function rowShowHide(target){ if (document.all && !IEmac)
var table_row = 'block'; else
var table_row = 'table-row'; 
obj=(document.all) ? document.all[target] : document.getElementById(target); obj.style.display=(obj.style.display=='none') ? table_row : 'none'; }

function clickElement (element) { if (element.click) { element.click(); }
else if (document.createEvent && element.dispatchEvent) { eval(element.href); }
}


function DisableSaveBtn(){ var savearr = document.getElementsByTagName('A'); for (var i = 0; i < savearr.length; i++)
{ if (savearr[i].className=="save")
{ noclick(savearr[i]); 
}
}
}

function DisableAll(currentElement, reenable)
{ if (currentElement)
{ var i=0; var currentElementChild=currentElement.childNodes[i]; while (currentElementChild)
{ DisableAll(currentElementChild,reenable); i++; noclick(currentElementChild,"disabletable",reenable); currentElementChild=currentElement.childNodes[i]; }
}
}

function noclick(obj,disableclass,reenable){ if(!document.getElementById | obj==null) return; if(disableclass==null)
disableclass='savewait'; if(obj.noclickarchive && !reenable)return true; switch(obj.tagName){ case "TD":
case "SPAN":
case "IMG":
case "DIV":
if(reenable){ if(obj.noclickarchive){ var arr = obj.noclickarchive.split("|"); if(arr.length==2){ if(arr[0]!=null && arr[0].substring(0,8)=="function"){ if(obj.id==null || obj.id=="")obj.id=Math.round((10000000000)*Math.random()); eval("document.getElementById('"+obj.id+"').onclick="+arr[0]); }
obj.className=arr[1]; }
}
}
else
{ obj.noclickarchive=obj.onclick+"|"+obj.className; obj.onclick=''; obj.className=disableclass; }
break; case "A":
if(reenable){ if(obj.noclickarchive){ var arr = obj.noclickarchive.split("|"); if(arr.length==3){ if(arr[0]!=null && arr[0].substring(0,8)=="function"){ if(obj.id==null || obj.id=="")obj.id=Math.round((10000000000)*Math.random()); eval("document.getElementById('"+obj.id+"').onclick="+arr[0]); }
if(arr[1]!=null && arr[1].substring(0,10)=="javascript"){ if(obj.id==null || obj.id=="")obj.id=Math.round((10000000000)*Math.random()); eval("document.getElementById('"+obj.id+"').href="+arr[1]); }else
obj.href=arr[1]; obj.className=arr[2]; }
}
}
else
{ obj.noclickarchive=obj.onclick+"|"+obj.href+"|"+obj.className; obj.onclick=''; obj.href='#'; obj.className=disableclass; }
break; case "BUTTON":
case "INPUT":
if(reenable)
{ if(obj.noclickarchive){ var arr = obj.noclickarchive.split("|"); if(arr.length==2){ obj.disabled=eval(arr[0]); obj.className=arr[1]; }
}
}
else
{ obj.noclickarchive=obj.disabled+"|"+obj.className; obj.disabled=true; obj.className+=" "+disableclass; }
break; default:
return true; }
if(reenable) obj.noclickarchive=null; return false; }


function zoominout(zin){ if(!IE6) return; if(zin)
if(document.body.style.zoom!=0 & document.body.style.zoom<1.3) document.body.style.zoom*=1.1; else document.body.style.zoom=1.1; else
if(document.body.style.zoom!=0 & document.body.style.zoom>1) document.body.style.zoom*=0.9; else document.body.style.zoom=1; }

function hotkey(e){ switch(xKey(e)){ case 29:
zoominout(true); break; case 31:
zoominout(false); break; case 93:
if(IE4plus) window.status = event.clientX + "|" + event.clientY; break; 
}
}

function RemoveCrLf(txt)
{ txt = txt.replace(new RegExp(/[\r|\n]/g), ""); txt = txt.replace(new RegExp(/[\t]/g), " "); txt = txt.replace(new RegExp(/'+/g), "\\'"); return txt; }

function validateEmail(email) { 
var splitted = email.match("^(.+)@(.+)$"); if(splitted == null) { return false; }

if(splitted[1] != null ) { var regexp_user=/^\"?[\w-_\.\']*\"?$/; if(splitted[1].match(regexp_user) == null) { return false; }
}

if(splitted[2] != null) { var regexp_domain=/^[\w-\.]*\.[A-Za-z]{2,4}$/; 
if(splitted[2].match(regexp_domain) == null) { var regexp_ip =/^\[\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\]$/; if(splitted[2].match(regexp_ip) == null) { return false; }
}
return true; }

return false; }

function voipDial(num)
{ if(!ObjExists("voipdial"))
{ var elm = document.createElement('iframe'); elm.id = "voipdial"; elm.style.display='none'; document.body.appendChild(elm); }
document.getElementById("voipdial").src = num; }

function FixNumeric(s)
{ return s.toString().replace(',','.'); }

function FixCurrency(num, dec, force, forcezero){ num=Math.round(num*Math.pow(10,dec))/Math.pow(10,dec); num = num.toString().replace('.',CurrencyPattern)
if(force){ pos = num.indexOf(CurrencyPattern); if(forcezero && pos == -1)
{ num+=CurrencyPattern+"0"; pos=num.length-1; }
if(pos>-1 && num.length-dec<=pos)
for(var i=1;i<dec;i++)
num+="0"; }
return num; }
function GetCoords(obj)
{ var newObj = new Object(); newObj.x = obj.offsetLeft; newObj.y = obj.offsetTop; newObj.w = obj.offsetWidth; newObj.h = obj.offsetHeight; theParent = obj.offsetParent; while(theParent != null)
{ newObj.y += theParent.offsetTop; newObj.x += theParent.offsetLeft; theParent = theParent.offsetParent; }

return newObj; }

function blurchklen(obj,max)
{ var jsLen, byteLen; jsLen = obj.value.length; byteLen=0; for (var i=0; i<obj.value.length; i++){ if (obj.value.charCodeAt(i)>256) byteLen+=2; else byteLen++; if (byteLen>max){ alert(ChkLenAgain); obj.value=obj.value.substr(0,i); obj.focus(); obj.select(); SubmitFalg = false; break; }
}
}
function stringchklen(str, max)
{ var jsLen, byteLen; jsLen = str.length; byteLen=0; for (var i=0; i<str.length; i++){ if (str.charCodeAt(i)>256) byteLen+=2; else byteLen++; if (byteLen>max)
{ return false; }
}
return true; }

function HideSideBar()
{ var aTds = document.getElementsByTagName('td'); for (var i = 0; i < aTds.length; i++) { if(aTds[i].className == "SideBorderLinked")
aTds[i].style.display = 'none'; break; }
}

function HideOperaMini()
{ document.getElementById('SideBar').style.display='none'; document.getElementById('SubBarMenu').style.display='none'; }
if(OPERAMINI) window.onload=HideSideBar; 

function CleanField(f){ var fi=getElement(f); fi.value=""; }

function CleanFields(c1,c2){ CleanField(c1); CleanField(c2); }

function FocusIfVisible(p) { var v=true; do { if(p.style!=null)
if(p.style.display!=null)
if(p.style.display.toLowerCase()=='none')
v=false; p=p.parentNode; } while(p!=null && p.parentNode!=null && v); if(v) p.focus(); }

function isNumber(a) { return typeof a == 'number' && isFinite(a); }

function GetBrotherId(o, braId)
{ var thisid = o.id; var prefix = thisid.lastIndexOf('_')+1; if(prefix>0)
braId = thisid.substring(0,prefix)+braId; return braId; }

var CalRun=3
var CalendartimerID = null; function calendaring(obj,inField,skip){ if(skip==null && CalRun>0){ var tmp = ["/js/jscalendar/calendar.js", "/js/jscalendar/calendar-setup.js", "/js/jscalendar/lang/calendar-"+JSLang.substring(0,2)+".js"]; loadScript("/js/jscalendar/calendar-win2k-cold-1.css"); for (var i in tmp)
loadScript(tmp[i],function(){--CalRun;if(CalRun==0){calendaring(obj,inField)}}); CalendartimerID = setTimeout("calendaring(document.getElementById('"+obj.id+"'),'"+inField+"',true)", 1000); return; }
if (CalendartimerID) clearTimeout(SessiontimerID); if (ObjExists('Calendar')){ Calendar.setup({inputField: inField, button: obj.id}); Calendar.dateConverter(DatePattern); Calendar.showcalendar(); }}

document.onkeypress=hotkey; 