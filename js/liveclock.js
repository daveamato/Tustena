/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

var languageinfo=navigator.language? navigator.language : navigator.userLanguage; var myfont_face = "Verdana"; 
var myfont_size = "7"; 
var myfont_color = "#000000"; 
var myback_color = ""; 
var mypre_text = ""y; 
var mywidth = 130

if (languageinfo=="it")
var my12_hour = 0; else
var my12_hour = 1; 
var myupdate = 1; 
var DisplayDate = 1; 

var ie4=document.all; var ns4=document.layers; var ns6=document.getElementById&&!document.all; 

var dn = ""; var mn = ""; var old = ""; 
var DaysOfWeek = new Array(7); DaysOfWeek[0] = " Domenica"; DaysOfWeek[1] = " Lunedi"; DaysOfWeek[2] = " Martedi"; DaysOfWeek[3] = " Mercoledi"; DaysOfWeek[4] = " Giovedi"; DaysOfWeek[5] = " Venerdi"; DaysOfWeek[6] = " Sabato"; 
var MonthsOfYear = new Array(12); MonthsOfYear[0] = "Gennaio"; MonthsOfYear[1] = "Febbraio"; MonthsOfYear[2] = "Marzo"; MonthsOfYear[3] = "Aprile"; MonthsOfYear[4] = "Maggio"; MonthsOfYear[5] = "Giugno"; MonthsOfYear[6] = "Luglio"; MonthsOfYear[7] = "Agosto"; MonthsOfYear[8] = "Settembre"; MonthsOfYear[9] = "Ottobre"; MonthsOfYear[10] = "Novembre"; MonthsOfYear[11] = "Dicembre"; 
var ClockUpdate = new Array(3); ClockUpdate[0] = 0; ClockUpdate[1] = 1000; ClockUpdate[2] = 60000; 
if (ie4||ns6) { document.write('<span id="LiveClockIE" style="width:'+mywidth+'px; background-color:'+myback_color+'"></span>'); }
else if (document.layers) { document.write('<ilayer bgColor="'+myback_color+'" id="ClockPosNS" visibility="hide"><layer width="'+mywidth+'" id="LiveClockNS"></layer></ilayer>'); }
else { old = "true"; show_clock(); }

function show_clock() { if (old == "die") { return; }
if (ns4)
document.ClockPosNS.visibility="show"
var Digital = new Date(); var day = Digital.getDay(); var mday = Digital.getDate(); var month = Digital.getMonth()+1; var year = Digital.getYear(); var hours = Digital.getHours(); var minutes = Digital.getMinutes(); var seconds = Digital.getSeconds(); 
if (mday == 1) { mn = String.Empty; }
else if (mday == 2) { mn = ""; }
else if (mday == 3) { mn = ""; }
else if (mday == 21) { mn = ""; }
else if (mday == 22) { mn = ""; }
else if (mday == 23) { mn = ""; }
else if (mday == 31) { mn = ""; }

if (my12_hour) { dn = "AM"; if (hours > 12) { dn = "PM"; hours = hours - 12; }
if (hours == 0) { hours = 12; }
} else { dn = ""; }
if (minutes <= 9) { minutes = "0"+minutes; }
if (seconds <= 9) { seconds = "0"+seconds; }

myclock = ''; myclock += '<font style="color:'+myfont_color+'; font-family:'+myfont_face+'; font-size:'+myfont_size+'pt;">'; myclock += mypre_text; myclock += hours+':'+minutes; if ((myupdate < 2) || (myupdate == 0)) { myclock += ':'+seconds; }
myclock += ' '+dn; if (DisplayDate) { 
switch (languageinfo){ case "it":
myclock += mday+'/'+month+'/'+year; break; default:
myclock += month+'/'+mday+'/'+year; break; }
}
myclock += '</font>'; 
if (old == "true") { document.write(myclock); old = "die"; return; }

if (ns4) { clockpos = document.ClockPosNS; liveclock = clockpos.document.LiveClockNS; liveclock.document.write(myclock); liveclock.document.close(); } else if (ie4) { LiveClockIE.innerHTML = myclock; } else if (ns6){ document.getElementById("LiveClockIE").innerHTML = myclock; }

if (myupdate != 0) { setTimeout("show_clock()",ClockUpdate[myupdate]); }
}
