/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

var dragswitch=0; var nsx; var nsy; var nstemp; document.write('<style><!--'); document.write('.dynaskin{position:absolute;background-color:white;border:1px solid silver;font:normal 12px Verdana;line-height:18px;z-index:100;visibility:hidden;cursor:pointer}'); document.write('#mouseoverstyle{background-color:orange}'); document.write('--></style>'); document.write('<div id="dynabox" class="dynaskin"></div>'); 

var closecross ='<div id="dragbar" onMousedown="initializedrag(event)" class="CommonColor" style="text-align:right"><span id=hidebox onclick="HideBox();return false"><b>X&nbsp;</span></div>'; 
function BoxHeader(which,w,h){ if (which.indexOf("Date")<=0){ boxobj.innerHTML= closecross + "<iframe scrolling=yes frameborder=0 width='" + w + "' height='" + h + "' name='dynaframe' src='" + which + "'></iframe>"
}else{ boxobj.innerHTML= closecross + "<iframe scrolling=no frameborder=0 width='" + w + "' height='" + h + "' name='dynaframe' src='" + which + "'></iframe>"
}
}
function FrameCreateBox(which,e,w,h){ if (!document.all&&!document.getElementById&&!document.layers){ window.open(which,'dynabox','width=' + w +',' + h +',left=100,top=100'); return; }
if(!event) event = e; boxobj=IE4plus?parent.getElement('dynabox'):getElement('dynabox'); BoxHeader(which,w,h); mainWindowTop = (typeof window.top.screenTop == 'number'? window.top.screenTop - 4:(window.top.screenY || 0)); mainWindowLeft = (typeof window.top.screenLeft == 'number'? window.top.screenLeft - 4:(window.top.screenX || 0)); eventX=IE4plus? event.screenX - mainWindowLeft : NS6? e.screenX - mainWindowLeft : e.x; eventY=IE4plus? event.screenY - mainWindowTop : NS6? e.screenY - mainWindowTop : e.y; var rightedge=IE4plus? parent.document.body.clientWidth-eventX : window.innerWidth-eventX; var bottomedge=IE4plus? parent.document.body.clientHeight-eventY : window.innerHeight-eventY; if (rightedge<boxobj.offsetWidth)
boxobj.style.left=IE4plus? parent.document.body.scrollLeft+eventX-boxobj.offsetWidth : NS6? window.pageXOffset+eventX-boxobj.offsetWidth : eventX-boxobj.offsetWidth; else
boxobj.style.left=IE4plus? parent.document.body.scrollLeft+eventX : NS6? window.pageXOffset+eventX : eventX; if (bottomedge<boxobj.offsetHeight){ boxobj.style.top=IE4plus? parent.document.body.scrollTop+eventY-boxobj.offsetHeight : NS6? window.pageYOffset+eventY-boxobj.offsetHeight : eventY-boxobj.offsetHeight; if (parseInt(boxobj.style.top)<0) boxobj.style.top='30px'; }else
boxobj.style.top=IE4plus? parent.document.body.scrollTop+event.clientY : NS6? window.pageYOffset+eventY : eventY; boxobj.style.visibility="visible"; if(window.parent.dynaframe)window.parent.dynaframe.opener = this; 
return false; }

function CreateBox(which,e,w,h){ if (!document.all&&!document.getElementById&&!document.layers){ window.open(which,'dynabox','width=' + w +',' + h +',left=100,top=100'); return; }

boxobj=getElement('dynabox'); BoxHeader(which,w,h); eventX=IE4plus? event.clientX : NS6? e.clientX : e.x; eventY=IE4plus? event.clientY : NS6? e.clientY : e.y; var rightedge=IE4plus? document.body.clientWidth-eventX : window.innerWidth-eventX; var bottomedge=IE4plus? document.body.clientHeight-eventY : window.innerHeight-eventY; if (rightedge<boxobj.offsetWidth)
{boxobj.style.left=IE4plus? document.body.scrollLeft+eventX-boxobj.offsetWidth : NS6? window.pageXOffset+eventX-boxobj.offsetWidth : eventX-boxobj.offsetWidth; if(parseInt(boxobj.style.left)<0) boxobj.style.left=5; }else
boxobj.style.left=IE4plus? document.body.scrollLeft+eventX : NS6? window.pageXOffset+eventX : eventX; if (bottomedge<boxobj.offsetHeight){ boxobj.style.top=IE4plus? document.body.scrollTop+eventY-boxobj.offsetHeight : NS6? window.pageYOffset+eventY-boxobj.offsetHeight : eventY-boxobj.offsetHeight; if (parseInt(boxobj.style.top)<0) boxobj.style.top='30px'; }else
boxobj.style.top=IE4plus? document.body.scrollTop+event.clientY : NS6? window.pageYOffset+eventY : eventY; boxobj.style.visibility="visible"; if(window.dynaframe)window.dynaframe.opener = window.parent; return false; }

function drag_dropns(name){ if (!NS4)
return; temp=eval(name); temp.captureEvents(Event.MOUSEDOWN | Event.MOUSEUP); temp.onmousedown=gons; temp.onmousemove=dragns; temp.onmouseup=stopns; }

function gons(e){ temp.captureEvents(Event.MOUSEMOVE); nsx=e.x; nsy=e.y; }
function dragns(e){ if (dragswitch==1){ temp.moveBy(e.x-nsx,e.y-nsy); return false; }
}

function stopns(){ temp.releaseEvents(Event.MOUSEMOVE); }

function drag_drop(e){ if (IE4plus&&dragapproved){ crossobj.style.left=tempx+event.clientX-offsetx; crossobj.style.top=tempy+event.clientY-offsety; return false; }
else if (NS6&&dragapproved){ crossobj.style.left=tempx+e.clientX-offsetx+"px"; crossobj.style.top=tempy+e.clientY-offsety+"px"; return false; }
}

function initializedrag(e){ crossobj=NS6? getElement("dynabox") : document.all.dynabox; var firedobj=NS6? e.target : event.srcElement; var topelement=NS6? "html" : document.compatMode!="BackCompat"? "documentElement" : "body"; while (firedobj.tagName!=topelement.toUpperCase() && firedobj.id!="dragbar"){ firedobj=NS6? firedobj.parentNode : firedobj.parentElement; }

if (firedobj.id=="dragbar"){ offsetx=IE4plus? event.clientX : e.clientX; offsety=IE4plus? event.clientY : e.clientY; 
tempx=parseInt(crossobj.style.left); tempy=parseInt(crossobj.style.top); 
dragapproved=true; document.onmousemove=drag_drop; }
}
document.onmouseup=new Function("dragapproved=false"); 
function HideBox(){ getElement("dynabox").style.visibility="hidden"; }

function HideFloatDiv(objName)
{ if(IE4plus) UnWindowed(); document.getElementById(objName).style.display='none'; 
}
function ShowFloatDiv(e, objName)
{ menuobj=document.getElementById(objName); if(e!=null){ eventX=IE4plus? event.clientX : NS6? e.clientX : e.x; eventY=IE4plus? event.clientY : NS6? e.clientY : e.y; }else{ eventX=Xmouse-30; eventY=Ymouse; }
eventX=eventX-30; eventY=eventY-30; var rightedge=IE4plus? document.body.clientWidth-eventX : window.innerWidth-eventX; var bottomedge=IE4plus? document.body.clientHeight-eventY : window.innerHeight-eventY; if (rightedge<menuobj.offsetWidth)
menuobj.style.left=IE4plus? document.body.scrollLeft+eventX-menuobj.offsetWidth : NS6? window.pageXOffset+eventX-menuobj.offsetWidth : eventX-menuobj.offsetWidth; else
menuobj.style.left=IE4plus? document.body.scrollLeft+eventX : NS6? window.pageXOffset+eventX : eventX; if (bottomedge<menuobj.offsetHeight)
menuobj.style.top=IE4plus? document.body.scrollTop+eventY-menuobj.offsetHeight : NS6? window.pageYOffset+eventY-menuobj.offsetHeight : eventY-menuobj.offsetHeight; else
menuobj.style.top=IE4plus? document.body.scrollTop+eventY : NS6? window.pageYOffset+eventY : eventY; menuobj.style.display=""; menuobj.style.padding = '2px'; if(IE4plus)UnWindowed(objName); return false; }

function UnWindowed(objName,refresh)
{ if(objName == null)
{ var obj = document.getElementById('coverSelect'); if(obj!=null)
document.body.removeChild(obj); return; }
var obj = document.getElementById(objName); if(obj==null)
return; if(refresh)
{ var cF=document.getElementById('coverSelect'); if(cF==null)
return; cF.style.width=obj.offsetWidth; cF.style.height=obj.offsetHeight; cF.style.top=document.body.scrollTop+obj.offsetTop; cF.style.left=document.body.scrollLeft+obj.offsetLeft; }else{ var cF=document.createElement('iframe'); cF.setAttribute('id','coverSelect'); cF.frameBorder='0'; cF.scrolling='no'; cF.style.width=obj.offsetWidth; cF.style.height=obj.offsetHeight; cF.style.top=document.body.scrollTop+obj.offsetTop; cF.style.left=document.body.scrollLeft+obj.offsetLeft; cF.style.position='absolute'; cF.style.zIndex = 0; obj.style.zIndex++; document.body.appendChild(cF); }
setTimeout('UnWindowed("'+objName+'",true)',500); }
