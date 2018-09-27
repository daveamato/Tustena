/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

var inserthash=new Hashtable(); var param = new Array(); var insertcount; function delor(o){ insertcount = inserthash.containsKey(o)?inserthash.get(o):0; var qc = document.getElementById(o + 'qc'); var delnode = document.getElementById(o + (--insertcount) + 'container'); if(delnode!=null)
qc.removeChild(delnode); inserthash.put(o, insertcount); }

function newor(o)
{ 
var qc = document.getElementById(o + 'qc'); var insert=false; for (var i=0;i<param.length;i++)
{ if(param[i]==o)
{ insert=true; break; }
}
insertcount = inserthash.containsKey(o)?inserthash.get(o):0; if(insertcount==0 || !insert)
{ if(o.substr(0,1) == "S")
{ sHTML = '<select class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; var oSrc = document.getElementById(o); for(var i = 0; i < oSrc.options.length; i++)
{ var sel=""; if(oSrc.options[i].selected) sel=" selected"; sHTML += "<option value=" + oSrc.options[i].value + sel + ">" + oSrc.options[i].text + "</option>"; }
sHTML += '</select>'; if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd',sHTML); else{ var elm = document.createElement('span'); elm.innerHTML = sHTML; qc.appendChild(elm); }
}
else
{ if(qc.insertAdjacentHTML){ sHTML = '<input type="text" class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; qc.insertAdjacentHTML('BeforeEnd',sHTML); document.getElementById(o + insertcount).value = document.getElementById(o).value; }else{ var elm = document.createElement('input'); elm.type = "text"; elm.className = "BoxDesign"; elm.id = o + insertcount; elm.name = o + insertcount; elm.value = document.getElementById(o).value; qc.appendChild(elm); }
document.getElementById(o).style.display='none'; param[param.length]=o; insertcount++; }
switch(o.substr(0,1))
{ case "S":

sHTML = '<br>or<br>&nbsp;<select class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; var oSrc = document.getElementById(o); for(var i = 0; i < oSrc.options.length; i++)
sHTML += "<option value=" + oSrc.options[i].value + ">" + oSrc.options[i].text + "</option>"; 
sHTML += '</select>'; 
break; default:
sHTML = '<br>or<br>&nbsp;<input type="text" class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; break; }
}
if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd','<span id="' + o + insertcount + 'container">' + sHTML + '</span>'); else{ var elm = document.createElement('span'); elm.id = o + insertcount + 'container'; elm.innerHTML = sHTML; qc.appendChild(elm); }

inserthash.put(o, ++insertcount); }



function makeor(){ 
for (var i=0;i<param.length;i++)
{ 
var x = document.getElementById(param[i]); 
switch (x.tagName.toUpperCase())
{ case "INPUT":
x.value=makeorp(param[i],0); break; case "SELECT":
document.getElementById(param[i].substr(1)).value=makeorp(param[i],1); break; }

}
}

function makeorp(p,t){ var hsum=""; for (var i=0;i<insertcount;i++)
{ if(document.getElementById(p+i)!=null)
{ switch(t)
{ case 0:
if(document.getElementById(p+i).value.length>0)
hsum += document.getElementById(p+i).value + "|"; break; case 1:
if(document.getElementById(p+i)[document.getElementById(p+i).selectedIndex].value.length>0)
hsum += document.getElementById(p+i)[document.getElementById(p+i).selectedIndex].value + "|"; break; }
}
}
return hsum.substr(0, hsum.length-1); }

function ParamsValidator(){ 
}
