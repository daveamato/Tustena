/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function DirectCalc(o,e)
{ var key = xKey(e); switch(key){ case 13:
Calc("="); break; case 8:
return null; break; }

if(butt.indexOf(String.fromCharCode(key))==-1){ PreventDef(e); return null; }
else
if(String.fromCharCode(key)=="=")
{ o.value = o.value.substring(0,o.value.length-1); Calc("="); }
}
var firstpart = "0"; var calced=false; var perced=false; function Calc(k)
{ var obj = document.getElementById("CalcLine"); switch(k)
{ case "+":
case "-":
case "/":
case "*":
if(calced){ firstpart = firstpart.substring(0,firstpart.length-1)+k; break; }
calced = true; if(firstpart.match(/[\*+-/]/g)==null)
{ firstpart = obj.value + k; break; }else{ obj.value = firstpart = eval(firstpart+obj.value); firstpart += k; break; }
case "%":
obj.value+=k; case "=":
if(firstpart!="0")
obj.value = firstpart+obj.value; try{ if(obj.value.indexOf('%')>-1)
obj.value=eval(parsePercent(obj.value)); else
obj.value=eval(obj.value); }catch(ex){obj.value="Error";}
if(k!="=" && k!="%") obj.value+=k; firstpart="0"; calced = true; 
break; default:
if(calced)
obj.value=k; else
obj.value+=k; calced=false; break; }
}

function parsePercent(s)
{ percpos = s.search("\\d{1,2}%"); if(percpos>-1)
{ perc = s.match("(\\d{1,2})%")[1]; if(perc.length==1)
perc = "*0.0"+perc; else
perc = "*0."+perc; part = s.substring(0,percpos); newstr = part+"("+s.substring(0,percpos-1)+perc+")"; return newstr; }
}

function RenderCalc(){ butt = "123/456*789-0%+=,.^()"; 
var caltxt="<table class=normal width=\"98%\"><tr><td colspan=4><input type=text id=CalcLine style=\"width:100%\" class=BoxDesign onkeypress='DirectCalc(this,event)';></td></tr><tr>"; for(var i=0;i<16;i++){ if(i%4==0)
caltxt+="</tr><tr>"; caltxt+="<td class=normal style=\"cursor:pointer;margin:2px; border:1px solid black;width:25%\" onclick=\"Calc('"+butt.charAt(i)+"')\">"+butt.charAt(i)+"</td>"; }
caltxt+="</tr></table>"; document.getElementById("CalculatorBox").innerHTML=caltxt; }
if(!OPERAMINI)RenderCalc(); 