/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/
var Xmouse, Ymouse, delayshow; var hidden = true; var preMessage="<table style='border: solid black 1px; background-color: #FFFACD;'><tr><td><span style='font : 13px Tahoma;'>"; var postMessage="</span></td></tr></table>"; document.write("<span id=\"tooltip\" style=\"filter:progid:DXImageTransform.Microsoft.DropShadow(OffX=2, OffY=2, Color='gray', Positive='true'); position:absolute; visibility:hidden;\"></span>")
function moveme(){ moveLayer('tooltip',Xmouse-30,Ymouse+18); if (!hidden) setTimeout("moveme()",100); }

function dtt(message){ if(message)
delayshow = setTimeout("tooltip('" + message.replace(/'/g,"\\'") + "')",300)
else{ clearTimeout(delayshow); tooltip(message); }
}

function tooltip(message, stop) { if(message){ hidden = false; if (document.layers){ with (document["tooltip"].document){ open(); write(preMessage + message + postMessage); close(); }
} else if (document.all) { document.all["tooltip"].innerHTML = preMessage + message + postMessage; } else if (document.getElementById){ jxdocrange = document.createRange(); jxdocrange.setStartBefore(document.getElementById("tooltip")); while (document.getElementById("tooltip").hasChildNodes()){ document.getElementById("tooltip").removeChild(document.getElementById("tooltip").lastChild); }
document.getElementById("tooltip").appendChild(jxdocrange.createContextualFragment(preMessage + message + postMessage)); }
if (document.all) { document.all["tooltip"].style.visibility = "visible"; } else if (document.layers){ document.layers["tooltip"].visibility = "show"; } else if (document.getElementById){ document.getElementById("tooltip").style.visibility = "visible"; }
} else { hidden = true; if (document.all) { document.all["tooltip"].style.visibility = "hidden"; } else if (document.layers){ document.layers["tooltip"].visibility = "hide"; } else if (document.getElementById){ document.getElementById("tooltip").style.visibility = "hidden"; }
}
if(!stop)
moveme()
else
moveLayer('tooltip',Xmouse-30,Ymouse); }
function MoveHandler(evnt) { if(document.all) { Xmouse = window.event.x + (document.documentElement.scrollLeft ?
document.documentElement.scrollLeft :
document.body.scrollLeft); Ymouse = window.event.y + (document.documentElement.scrollTop ?
document.documentElement.scrollTop :
document.body.scrollTop); } else if(document.layers||document.getElementById){ Xmouse = evnt.pageX; Ymouse = evnt.pageY; }
}

function moveLayer(Id,x,y){ if (document.all){ document.all[Id].style.left = x; document.all[Id].style.top = y; } else if (document.layers){ document.layers[Id].left = x; document.layers[Id].top = y; } else if (document.getElementById){ document.getElementById(Id).style.left = x+'px'; document.getElementById(Id).style.top = y+'px'; }
}
if (document.layers){ document.captureEvents(Event.MOUSEMOVE); }

document.onmousemove = MoveHandler; 