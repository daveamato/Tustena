/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function selectadd(selecter,selectertxt){ if (selecter.options[selecter.selectedIndex].value=="99"){ selecter.disabled=1; selecter.style.display='none'; var random = Math.random(); var nome = "inputtext" + random; selectertxt.innerHTML="<table width=\"100%\" cellpadding=0 cellspacing=0><tr><td width=\"90%\"><input type=\"text\" id=\"" + nome + "\" name=\"" + nome + "\" onchange=\"selectadd2(this,'"+selecter.id+"','"+selectertxt.id+"')\" class=\"autoform\" onkeypress=\"if(self.event.keyCode==13)selectadd2(this,'"+selecter.id+"','"+selectertxt.id+"')\"></td><td class=\"normal\">&nbsp;<a class=\"Link\">OK</a></td></tr></table>"; }else{ document.getElementById("SettoreCustom").value=""; }
}

function selectadd2(selecter,targhet,container){ objselect = document.getElementById(targhet); objselect2 = document.getElementById(container); var valore; if (selecter.value.lenght>50)
valore=selecter.value.substring(1,47) + '...'; else
valore=selecter.value; var aggiungi = true; for(var x=0;x<objselect.options.length;x++)
if(objselect.options[x].value==valore){ objselect.options[x].selected = true; aggiungi = false; }
if(aggiungi){ objselect.options[objselect.length]= new Option(objselect.options[objselect.length-1].text); objselect.options[objselect.length-1].value= objselect.options[objselect.selectedIndex].value; objselect.options[objselect.selectedIndex].text=valore; objselect.options[objselect.selectedIndex].value=valore; document.getElementById("SettoreCustom").value=valore; }
objselect2.innerHTML=""
objselect.disabled=0; objselect.style.display='inline'; }
