/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

sqao=false; function View(obj)
{ if(obj.src.indexOf('0.')<0)
{ if(obj.src.indexOf('1.')>0)
{ var seloncount=0; tableobj=document.getElementById('qbtable'); for(var i=1;i<tableobj.rows.length;i++){ var selobj = document.getElementById('vis'+i); if(selobj.src.indexOf('1.')>0)
seloncount++; }
if(seloncount>1)
{ obj.src='/i/qbi/vis.gif'; sethidden(obj,1,0)
var num = obj.id.charAt(3); if(num.length>0){ var selobj = document.getElementById('grp'+num); if(selobj.src.indexOf('1.')>0){ selobj.src='/i/qbi/grp.gif'; sethidden(selobj,2,0); }
}
}

}
else
{ obj.src='/i/qbi/vis1.gif'; sethidden(obj,1,1)
}
}
}
function GroupBy(obj)
{ if(obj.src.indexOf('0.')<0)
{ if(obj.src.indexOf('1.')>0)
{ var seloncount=0; tableobj=document.getElementById('qbtable'); for(var i=1;i<tableobj.rows.length;i++){ var selobj = document.getElementById('grp'+i); if(selobj.src.indexOf('1.')>0)
seloncount++; }
if(!(sqao==true && seloncount!=1))
{ obj.src='/i/qbi/grp.gif'; sethidden(obj,2,0); }

}
else
{ obj.src='/i/qbi/grp1.gif'; sethidden(obj,2,1); var num = obj.id.charAt(3); if(num.length>0){ var selobj = document.getElementById('vis'+num); selobj.src='/i/qbi/vis1.gif'; sethidden(selobj,1,1); }
}
}
}
function Principal(obj)
{ if(obj.src.indexOf('0.')<0)
{ if(obj.src.indexOf('1.')>0)
{ obj.src='/i/qbi/cpr.gif'; sethidden(obj,3,0)

}
else
{ tableobj=document.getElementById('qbtable'); for(var i=1;i<tableobj.rows.length;i++){ var selobj = document.getElementById('cpr'+i); selobj.src='/i/qbi/cpr.gif'; sethidden(selobj,3,0); }
obj.src='/i/qbi/cpr1.gif'; sethidden(obj,3,1); }
}
}
function Param(obj)
{ if(obj.src.indexOf('0.')<0)
{ if(obj.src.indexOf('1.')>0)
{ obj.src='/i/qbi/par.gif'; sethidden(obj,4,0)
}
else
{ obj.src='/i/qbi/par1.gif'; sethidden(obj,4,1)
}
}
}
function sethidden(obj,idx,v)
{ obj = getobj(obj.id,'opt'); var charArray = obj.value.split(','); if(charArray.length < 5)
{ alert('init array error!'); return; }
charArray[idx]=v; obj.value = new String(charArray); }
function getobj(name1,name2)
{ var obj = document.getElementById(name2+name1.substring(3,name1.length))
return obj; }

function moverow(me,dir)
{ var temprow = new Array(); var obj = document.getElementById('qbtable'); var idx; var idx2; idx=me.parentNode.parentNode.id; for(var i=0;i<obj.rows(idx).cells.length;i++)
{ if(dir==1){ if(idx==obj.rows.length-1) break; idx2 = parseInt(idx)+1; }else{ if(idx==1) break; idx2=parseInt(idx)-1; }
temprow[i]=obj.rows(idx).cells(i).innerHTML; obj.rows(idx).cells(i).innerHTML=obj.rows(idx2).cells(i).innerHTML; obj.rows(idx2).cells(i).innerHTML=temprow[i]; }
setpos(obj); }

function setpos(obj)
{ if(obj.rows.length==2)
{ document.getElementById('upp1').src='/i/qbi/up0.gif'; document.getElementById('dwn1').src='/i/qbi/dwn0.gif'; 
}else
for(var i=1;i<obj.rows.length;i++)
{ sethidden(document.getElementById('opt'+i),0,(document.getElementById('opt'+i).parentNode.parentNode.id)); if(document.getElementById('upp'+i).parentNode.parentNode.id==1)
{ document.getElementById('upp'+i).src='/i/qbi/up0.gif'; document.getElementById('dwn'+i).src='/i/qbi/dwn.gif'; }
else if(document.getElementById('dwn'+i).parentNode.parentNode.id==obj.rows.length-1)
{ document.getElementById('upp'+i).src='/i/qbi/up.gif'; document.getElementById('dwn'+i).src='/i/qbi/dwn0.gif'; }
else
{ document.getElementById('upp'+i).src='/i/qbi/up.gif'; document.getElementById('dwn'+i).src='/i/qbi/dwn.gif'; }
}
}
function init()
{ obj=document.getElementById('qbtable'); if(obj !=null && obj.rows.length>0){ for(var i=1;i<obj.rows.length;i++)
{ var charArray = document.getElementById('opt'+i).value.split(','); if(charArray.length < 5)
{ alert('init array error!'); return; }
if(charArray[1]==0) document.getElementById('vis'+i).src='/i/qbi/vis.gif'; else if(charArray[1]==1) document.getElementById('vis'+i).src='/i/qbi/vis1.gif'; else if(charArray[1]==2) document.getElementById('vis'+i).src='/i/qbi/vis0.gif'; if(charArray[2]==0) document.getElementById('grp'+i).src='/i/qbi/grp.gif'; else if(charArray[2]==1) document.getElementById('grp'+i).src='/i/qbi/grp1.gif'; else if(charArray[2]==2) document.getElementById('grp'+i).src='/i/qbi/grp0.gif'; else if(charArray[2]==3){ document.getElementById('grp'+i).src='/i/qbi/grp1.gif'; sqao=true; }
if(charArray[3]==0) document.getElementById('cpr'+i).src='/i/qbi/cpr.gif'; else if(charArray[3]==1) document.getElementById('cpr'+i).src='/i/qbi/cpr1.gif'; else if(charArray[3]==2) document.getElementById('cpr'+i).src='/i/qbi/cpr0.gif'; if(charArray[4]==0) document.getElementById('par'+i).src='/i/qbi/par.gif'; else if(charArray[4]==1) document.getElementById('par'+i).src='/i/qbi/par1.gif'; else if(charArray[4]==2) document.getElementById('par'+i).src='/i/qbi/par0.gif'; }
setpos(obj); }
}
