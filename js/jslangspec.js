/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function getthedate(){ var mydate=new Date(); var year=mydate.getYear(); if (year < 1000)
year+=1900; var day=mydate.getDay(); var month=mydate.getMonth(); var daym=mydate.getDate(); if (daym<10)
daym="0"+daym; var hours=mydate.getHours(); var minutes=mydate.getMinutes(); var seconds=mydate.getSeconds(); 
if (hours<=9)
hours="0"+hours; if (minutes<=9)
minutes="0"+minutes; if (seconds<=9)
seconds="0"+seconds; 
switch(JSLang.substring(0,2)){ case "it":
var dayarray=new Array("Domenica","Lunedi","Martedi","Mercoledi","Giovedi","Venerdi","Sabato"); var montharray=new Array("Gennaio","Febbraio","Marzo","Aprile","Maggio","Giugno","Luglio","Agosto","Settembre","Ottobre","Novembre","Dicembre"); var dateformat=dayarray[day]+" "+daym+" "+montharray[month]+" "+year+", "+hours+"."+minutes+"."+seconds; break; case "es":
var dayarray=new Array("Domingo","Lunes","Martes","Miercoles","Jueves","Viernes","Sabado"); var montharray=new Array("Enero","Febrero","Marzo","Abril","Mayo","Junio","Julio","Agosto","Septiembre","Octubre","Noviembre","Diciembre"); var dateformat=dayarray[day]+", "+daym+" de "+montharray[month]+" de "+year+", "+hours+":"+minutes+":"+seconds; break; default:
var dn="AM"; if (hours>=12) dn="PM"; if (hours>12) hours=hours-12; var dayarray=new Array("Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"); var montharray=new Array("January","February","March","April","May","June","July","August","September","October","November","December"); var dateformat=dayarray[day]+", "+montharray[month]+" "+daym+", "+year+" "+parseInt(hours)+":"+minutes+":"+seconds+" "+dn; }

dateformat = "-&nbsp;"+dateformat; try{ if (document.all)
document.all.clock.innerHTML=dateformat; else if(document.getElementById)
document.getElementById("clock").innerHTML=dateformat; 
else
document.write(dateformat); }catch(ex){}
}
function Showtime(){ if (JSLang!=""&&(document.all||document.getElementById)){ setInterval("getthedate()",1000); getthedate(); }
}
if ( typeof( window[ 'Last30' ] ) != "undefined" )
SafeAddOnload(Showtime); 
