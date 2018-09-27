/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */


var loadedcolor='navy' ; var unloadedcolor='lightgrey'; var barheight=15; var barwidth=350; var bordercolor='black'; 

var action=function()
{ }

//
//
//

var w3c=(document.getElementById)?true:false; var ns4=(document.layers)?true:false; var ie4=(document.all && !w3c)?true:false; var ie5=(document.all && w3c)?true:false; var ns6=(w3c && navigator.appName.indexOf("Netscape")>=0)?true:false; var blocksize=(barwidth-2)/100; barheight=Math.max(4,barheight); var loaded=0; var perouter=0; var perdone=0; var images=new Array(); var txt=''; if(ns4){ txt+='<table cellpadding=0 cellspacing=0 border=0><tr><td>'; txt+='<ilayer name="perouter" width="'+barwidth+'" height="'+barheight+'">'; txt+='<layer width="'+barwidth+'" height="'+barheight+'" bgcolor="'+bordercolor+'" top="0" left="0"></layer>'; txt+='<layer width="'+(barwidth-2)+'" height="'+(barheight-2)+'" bgcolor="'+unloadedcolor+'" top="1" left="1"></layer>'; txt+='<layer name="perdone" width="'+(barwidth-2)+'" height="'+(barheight-2)+'" bgcolor="'+loadedcolor+'" top="1" left="1"></layer>'; txt+='</ilayer>'; txt+='</td></tr></table>'; }else{ txt+='<div id="perouter" onmouseup="hidebar()" style="position:relative; visibility:hidden; background-color:'+bordercolor+'; width:'+barwidth+'px; height:'+barheight+'px;">'; txt+='<div style="position:absolute; top:1px; left:1px; width:'+(barwidth-2)+'px; height:'+(barheight-2)+'px; background-color:'+unloadedcolor+'; z-index:100; font-size:1px;"></div>'; txt+='<div id="perdone" style="position:absolute; top:1px; left:1px; width:0px; height:'+(barheight-2)+'px; background-color:'+loadedcolor+'; z-index:100; font-size:1px;"></div>'; txt+='</div>'; }

document.write(txt); 
function incrCount(prcnt){ loaded+=prcnt; setCount(loaded); }

function decrCount(prcnt){ loaded-=prcnt; setCount(loaded); }

function setCount(prcnt){ loaded=prcnt; if(loaded<0)loaded=0; if(loaded>=100){ loaded=100; setTimeout('hidebar()', 400); }
clipid(perdone, 0, blocksize*loaded, barheight-2, 0); }

function findlayer(name,doc){ var i,layer; for(i=0;i<doc.layers.length;i++){ layer=doc.layers[i]; if(layer.name==name)return layer; if(layer.document.layers.length>0)
if((layer=findlayer(name,layer.document))!=null)
return layer; }
return null; }

function progressBarInit(){ perouter=(ns4)?findlayer('perouter',document):(ie4)?document.all['perouter']:document.getElementById('perouter'); perdone=(ns4)?perouter.document.layers['perdone']:(ie4)?document.all['perdone']:document.getElementById('perdone'); clipid(perdone,0,0,barheight-2,0); if(ns4)perouter.visibility="show"; else perouter.style.visibility="visible"; }

function hidebar(){ action(); }

function clipid(id,t,r,b,l){ if(ns4){ id.clip.left=l; id.clip.top=t; id.clip.right=r; id.clip.bottom=b; }else id.style.width=r; }

window.onload=progressBarInit; 
window.onresize=function(){ if(ns4)setTimeout('history.go(0)' ,400); }
