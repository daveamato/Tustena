/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function cloneObj(o,id,target){ var the_table = document.getElementById(o); var newTable = the_table.cloneNode(true); newTable.id = newTable.id+"_"+id; document.getElementById(target).appendChild(newTable); goThroughDOM(newTable,id); }

function removeCloned(o,target){ o=document.getElementById(o); if(o==null || target==null) return; var id = parseInt(o.id.substring(o.id.lastIndexOf('_')+1)); if(!isNaN(id)){ document.getElementById(target).removeChild(o); goThroughDOM(document.getElementById(target),id,true); }
}

function goThroughDOM(obj, id, subtract)
{ var parms = ["cloneparam1","cloneparam2","cloneparam3","cloneparam4","cloneparam5","cloneparam6","cloneparam7","cloneparam8","cloneparam9","cloneparam10"]; for (var i=0; i<obj.childNodes.length; i++) { var childObj = obj.childNodes[i]; if (childObj.id) { if(subtract){ var pos = childObj.id.lastIndexOf('_')+1; var idplus = parseInt(childObj.id.substring(pos)); if(isNaN(idplus) || idplus<=id)
continue; childObj.id = childObj.id.substring(0,pos)+(idplus-1); }else{ childObj.id = childObj.id+"_"+id; }
}
if (childObj.name) { if(subtract){ var pos = childObj.name.lastIndexOf('_')+1; var idplus = parseInt(childObj.name.substring(pos)); if(isNaN(idplus) || idplus<=id)
continue; childObj.name = childObj.name.substring(0,pos)+(idplus-1); }else{ childObj.name = childObj.name+"_"+id; }
}
for (var x in parms){ if (childObj.getAttribute && childObj.getAttribute(parms[x])!=null) { if(subtract){ var pos = childObj.getAttribute(parms[x]).lastIndexOf('_')+1; var idplus = parseInt(childObj.getAttribute(parms[x]).substring(pos)); if(isNaN(idplus) || idplus<=id)
continue; childObj.setAttribute(parms[x], childObj.getAttribute(parms[x]).substring(0,pos)+(idplus-1)); }else{ childObj.setAttribute(parms[x], childObj.getAttribute(parms[x])+"_"+id); }
}else
break; }
goThroughDOM(childObj, id, subtract); }
}
