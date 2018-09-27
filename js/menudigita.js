/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/
var menudigita = true; document.write('<style><!--')
document.write('.menuskin{filter:progid:DXImageTransform.Microsoft.DropShadow(OffX=2, OffY=2, Color=gray, Positive=true);position:absolute;background-color:#f9f9f9;border:1px solid silver;font:normal 12px Verdana;line-height:18px;z-index:100;visibility:hidden;cursor:hand}')
document.write('#mouseoverstyle{background-color:orange}')
document.write('--></style>')
document.write('<div id="popmenu" class="menuskin" onMouseover="clearhidemenu();highlightmenu(event,\'on\')" onMouseout="highlightmenu(event,\'off\');dynamichide(event)"></div>')
var dayid, delayhide
var ownerId

function showmenu2(e,which,daynum){ dayid = daynum; showmenu(e,which); }

function showmenuquick(e,which,ownerID){ ownerId = ownerID; showmenu(e,which); }

function showmenu(e,which){ if (!document.all&&!document.getElementById&&!document.layers)
return
clearhidemenu()
menuobj=IE4plus? document.all.popmenu : NS6? document.getElementById("popmenu") : NS4? document.popmenu : ""; menuobj.thestyle=(IE4plus||NS6)? menuobj.style : menuobj; if (IE4plus||NS6)
menuobj.innerHTML=which; else{ menuobj.document.write('<layer name=gui bgColor=#E6E6E6 width=165 onmouseover="clearhidemenu()" onmouseout="hidemenu()">'+which+'</layer>'); menuobj.document.close(); }
menuobj.contentwidth=(IE4plus||NS6)? menuobj.offsetWidth : menuobj.document.gui.document.width; menuobj.contentheight=(IE4plus||NS6)? menuobj.offsetHeight : menuobj.document.gui.document.height; if(e!=null){ eventX=IE4plus? event.clientX : NS6? e.clientX : e.x; eventY=IE4plus? event.clientY : NS6? e.clientY : e.y; }else{ eventX=Xmouse-30; eventY=Ymouse; }
var rightedge=IE4plus? document.body.clientWidth-eventX : window.innerWidth-eventX
var bottomedge=IE4plus? document.body.clientHeight-eventY : window.innerHeight-eventY
if (rightedge<menuobj.contentwidth)
menuobj.thestyle.left=IE4plus? document.body.scrollLeft+eventX-menuobj.contentwidth : NS6? window.pageXOffset+eventX-menuobj.contentwidth : eventX-menuobj.contentwidth; else
menuobj.thestyle.left=IE4plus? document.body.scrollLeft+eventX : NS6? window.pageXOffset+eventX : eventX; if (bottomedge<menuobj.contentheight)
menuobj.thestyle.top=IE4plus? document.body.scrollTop+eventY-menuobj.contentheight : NS6? window.pageYOffset+eventY-menuobj.contentheight : eventY-menuobj.contentheight; else
menuobj.thestyle.top=IE4plus? document.body.scrollTop+eventY : NS6? window.pageYOffset+eventY : eventY; menuobj.thestyle.visibility="visible"; return false; }

function contains_NS6(a, b) { while (b.parentNode)
if ((b = b.parentNode) == a)
return true; return false; }
function hidemenu(){ if (window.menuobj)
menuobj.thestyle.visibility=(IE4plus||NS6)? "hidden" : "hide"; }
function dynamichide(e){ if (IE4plus&&!menuobj.contains(e.toElement))
hidemenu(); else if (NS6&&e.currentTarget!= e.relatedTarget&& !contains_NS6(e.currentTarget, e.relatedTarget))
hidemenu(); }
function dhm(){ if (IE4plus||NS6||NS4)
delayhide=setTimeout("hidemenu()",500); }
function clearhidemenu(){ if (window.delayhide)
clearTimeout(delayhide); }
function highlightmenu(e,state){ if (document.all)
source_el=event.srcElement; else if (document.getElementById)
source_el=e.target; if (source_el.className=="menuitems"){ source_el.id=(state=="on")? "mouseoverstyle" : ""
}
else{ while(source_el.id!="popmenu"){ source_el=document.getElementById? source_el.parentNode : source_el.parentElement; if (source_el.className=="menuitems"){ source_el.id=(state=="on")? "mouseoverstyle" : ""; }
}
}
}
if (IE4plus||NS6)
document.onclick=hidemenu; 

