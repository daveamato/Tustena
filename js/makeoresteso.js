/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

var inserthash=new Hashtable(); var param = new Array(); var insertcount; 
function delor(o){ insertcount = inserthash.containsKey(o)?inserthash.get(o):0; var qc = document.getElementById(o + 'qc'); var delnode = document.getElementById(o + (--insertcount) + 'container'); if(delnode!=null)
qc.removeChild(delnode); inserthash.put(o, insertcount); }
function newor(o,myvalue){ 
var qc = document.getElementById(o + 'qc'); var insert=false; for (var i=0;i<param.length;i++)
{ if(param[i]==o)
{ insert=true; break; }
}
insertcount = inserthash.containsKey(o)?inserthash.get(o):0; if(insertcount==0 || !insert)
{ if(o.substr(0,1) == "S")
{ sHTML = '<select old=true class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; var oSrc = document.getElementById(o); for(var i = 0; i < oSrc.options.length; i++)
{ var sel=""; if(oSrc.options[i].selected) sel=" selected"; sHTML += "<option value=" + oSrc.options[i].value + sel + ">" + oSrc.options[i].text + "</option>"; }
sHTML += '</select>'; if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd',sHTML); else
qc.innerHTML+=sHTML; 
document.getElementById(o).style.display='none'; param[param.length]=o; insertcount++; }
else
{ if(o.substr(0,1) == "R")
{ var objFormField = document.forms[0].elements[o]; intControlLength = objFormField.length; var simbol= new Array(6)
simbol[0]="="; simbol[1]="&le;"; simbol[2]="&lt;"; simbol[3]="&ne;"; simbol[4]="&gt;"; simbol[5]="&ge;"; sHTML=""; for (i=0;i<intControlLength;i++){ if(objFormField[i].checked)
sHTML += '<input type="radio" name="' + o + insertcount +'" value=' + i + ' checked>'+simbol[i]; else
sHTML += '<input type="radio" name="' + o + insertcount +'" value=' + i + '>'+simbol[i]; }

sHTML += '<input type="text" class="BoxDesign" name="' + o.substr(1,o.length-1) + insertcount + '" id="' + o.substr(1,o.length-1) + insertcount + '">'; if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd',sHTML); else
qc.innerHTML+=sHTML; 

document.getElementById(o.substr(1,o.length-1) + insertcount).value = document.getElementById(o.substr(1,o.length-1)).value; 
document.getElementById(o.substr(1,o.length-1)).style.display='none'; 
document.getElementById("Remove"+o).style.display='none'; 
param[param.length]=o; insertcount++; }
else
{ sHTML = '<input type="text" class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd',sHTML); else
qc.innerHTML+=sHTML; (document.getElementById(o + insertcount)).value = (document.getElementById(o)).value; 
document.getElementById(o).style.display='none'; param[param.length]=o; insertcount++; }
}
}
switch(o.substr(0,1))
{ case "S":
sHTML = '<span id="' + o + insertcount + 'container"><br>or<br><select old=true class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount + '">'; var oSrc = document.getElementById(o); for(var i = 0; i < oSrc.options.length; i++){ sel=""; if(myvalue!=null && oSrc.options[i].value==myvalue) sel=" selected"; sHTML += "<option value=" + oSrc.options[i].value + sel + ">" + oSrc.options[i].text + "</option>"; }
sHTML += '</select></span>'; break; case "R":
var chk = new Array(5); if(myvalue!=null) chk[myvalue]=" checked"; sHTML = '<span id="' + o + insertcount + 'container"><br>or<br><input type="radio" name="' + o + insertcount +'" value=1' + chk[0] + '>='; sHTML += '<input type="radio" name="' + o + insertcount +'" value=2' + chk[1] + '>&le;'; sHTML += '<input type="radio" name="' + o + insertcount +'" value=3' + chk[2] + '>&lt;'; sHTML += '<input type="radio" name="' + o + insertcount +'" value=4' + chk[3] + '>&ne;'; sHTML += '<input type="radio" name="' + o + insertcount +'" value=5' + chk[4] + '>&gt;'; sHTML += '<input type="radio" name="' + o + insertcount +'" value=6' + chk[5] + '>&ge;'; sHTML += '<input type="text" class="BoxDesign" name="' + o.substr(1,o.length-1) + insertcount + '" id="' + o.substr(1,o.length-1) + insertcount + '"></span>'; break; default:
sHTML = '<span id="' + o + insertcount + 'container"><br>or<br><input type="text" class="BoxDesign" name="' + o + insertcount + '" id="' + o + insertcount; if(myvalue!=null) sHTML += '" value="' + myvalue; sHTML += '"></span>'; break; }

if(qc.insertAdjacentHTML)
qc.insertAdjacentHTML('BeforeEnd',sHTML); else
qc.innerHTML+=sHTML; 

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
