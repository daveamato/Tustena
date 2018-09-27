/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function RenderCalendar(){ var days = dayarray; var months = montharray; var date = new Date(); 
var year = date.getFullYear(); var month = date.getMonth(); var today = date.getDate(); var weekday = ((eurocal)?date.getDay()+1:date.getDay()); var cal; date.setDate(1); date.setMonth(month); var firstweekday = ((eurocal)?date.getDay()-1:date.getDay()); 
cal = '<table border=0 width="100%" cellspacing=0 cellpadding=0 style="font-size:10px"><tr>'; cal += '<td colspan=7 bgcolor="orange"><center><b>'; cal += months[month]  + '   ' + year + '</b></td></tr>'; cal += '<tr>'; var ei = ((eurocal)?1:0); for(i=0+ei;i<7+ei;i++)
if(weekday==i+ei)
cal += '<td bgcolor="silver"><center><b>' + days[i] + '</b></center></td>'; else
cal += '<td bgcolor="silver"><center>' + days[i] + '</center></td>'; cal += '</tr><tr>'; 
for(i=0;i<firstweekday;i++)
cal += '<td></td>'; for(i=0;i<31;i++)
{ if(date.getDate()>i)
{ weekday = (eurocal)?date.getDay()+1:date.getDay(); if(weekday == ((eurocal)?2:0))
cal += '<tr>'; 
if(weekday == ((eurocal)?1:0))
bgcolor="bgcolor=\"silver\""; else
bgcolor=""; var day = date.getDate(); if(today==day)
cal += '<td style="cursor:pointer" bgcolor="gold" onclick="CalClick(' + day + '/' + (month+1) + '/' + year + ')"><b><center>' + day + '</center></b></td>'; else
cal += '<td style="cursor:pointer" '+bgcolor+' onclick="CalClick(' + day + '/' + (month+1) + '/' + year + ')"><center>' + day + '</center></td>'; 
}
date.setDate(date.getDate()+1); }
cal += '</td></tr></table>'; document.getElementById("CalendarBox").innerHTML=cal; }
function CalClick(d)
{ window.location='/Calendar/agenda.aspx?date=' + d; }
if(!OPERAMINI)RenderCalendar(); 