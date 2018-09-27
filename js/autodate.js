/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

function HourUp(o){ o = getElement(o); var change = true; if(o.name=="F_StartHour"&&o.value==""){ o.value="08:00"
change = false; }else if(o.value==""){ o.value="18:00"
change = false; }
if (change){ var hours = parseFloat(o.value.substr(0,2))
var minutes = parseFloat(o.value.substr(3,2))
if(o.value.indexOf("PM")>0)
hours+=12; if(hours==24)hours=0; if (minutes >= 30){ if (hours == 23){ o.value = '00:00'; return; }
o.value = (hours + 1) + ':00'; }else{ o.value = hours + ':30'; }
if (o.value.length < 5) o.value = '0' + o.value; }
}
function HourDown(o){ o = getElement(o); var change = true; if(o.name=="F_StartHour"&&o.value==""){ o.value="08:00"
change = false; }else if(o.value==""){ o.value="18:00"
change = false; }
if (change){ var hours = parseFloat(o.value.substr(0,2))
var minutes = parseFloat(o.value.substr(3,2))
if(o.value.indexOf("PM")>0)
hours+=12; if(hours==24)hours=0; 
if (minutes <= 30 && minutes > 0){ o.value = hours + ':00'; if (o.value.length < 5) o.value = '0' + o.value; return; }
if (minutes > 30){ o.value = hours + ':30'; return; }
if (minutes == 0){ if (hours == 0){ o.value = '23:30'; }else{ o.value = (hours - 1) + ':30'; }
if (o.value.length < 5) o.value = '0' + o.value; }
}
}

function HourCheck(o,e){ TimePattern=FixPatternTime(TimePattern); var validnum = '0123456789'; var validdiv = ':.'; var ampm = 'ap '; var timediv; var keypressed = String.fromCharCode(xKey(e)).toLowerCase(); if(TimePattern.length<5)
timediv = TimePattern.substr(1,1); else
timediv = TimePattern.substr(2,1); if (e.preventDefault) { e.preventDefault(); } else { e.returnValue = false; }
if(o.value.length<5 && o.value.length>2 && validdiv.indexOf(o.value.substr(2,1))==-1)
o.value=""; 
if(o.value.length>TimePattern.length) return false; switch(o.value.length)
{ case 1:	if(!(validdiv.indexOf(keypressed) > -1 || validnum.indexOf(keypressed) > -1)) return false; break; case 2:	if(validnum.indexOf(keypressed) == -1)
o.value += timediv; else if(parseFloat(keypressed)<6)
o.value += timediv + keypressed; return false; break; case 5:	if(ampm.indexOf(keypressed) == -1) return false; break; case 6:	if(ampm.indexOf(keypressed) == -1) return false; break; case 7:	if(keypressed == "m") return false; break; default:
if(validnum.indexOf(keypressed) == -1) return false; }

if (o.value.indexOf(timediv) != 3 && o.value.length > 3){ }
if ((o.value.length == 0) && (parseFloat(keypressed) > 2)){ o.value = '0' + keypressed + timediv; return false; }else if (o.value.length == 1){ if(validdiv.indexOf(keypressed) > -1 && o.value.substr(0,1) < 3)
{ o.value = '0'+o.value.substr(0,1)+keypressed; return false; }
if (parseFloat(o.value + keypressed) > 23){ o.value += keypressed; o.style.backgroundColor='#FFFFC0'; o.value = ''; return false; }
o.value += keypressed + timediv; return false; }else if (o.value.length == 2){ o.value += timediv; if (validdiv.indexOf(keypressed) != -1) return false; }else if ((o.value.length == 3)&& (parseFloat(keypressed) > 5)){ o.value += '0' + keypressed; }else if (o.value.length == 4){ if (parseFloat(o.value.substr(o.value.length-1,1 + keypressed)) > 59){ o.value += keypressed; o.style.backgroundColor='#FFFFC0'; o.value = o.value.substr(0,o.value.length-2); return false; }
}else if (o.value.length == 5){ if(keypressed=='a'){ o.value += " AM"; }
if(keypressed=='p'){ o.value += " PM"; }
if(keypressed==' '){ o.value += " "; }
return false; }
else if (o.value.length == 6){ if(keypressed=='a'){ o.value += " AM"; }
if(keypressed=='p'){ o.value += " PM"; }
return false; }
else if (o.value.length == 7){ if(keypressed=='m'){ o.value += "M"; return false; }
}
if (o.value.length < 5) o.value += keypressed; o.style.backgroundColor=''; }

function DataCheck(o,e){ DatePattern=FixPatternDate(DatePattern); var validnum = '0123456789'; var validdiv = '/-.'; var keypressed = String.fromCharCode(xKey(e)); if(xKey(e)==8) return; if (e.preventDefault) { e.preventDefault(); } else { e.returnValue = false; }
for(i=0;i<o.value.length-1;i++)
if(isNaN(o.value.substr(i,1)))
if(DatePattern.substr(i,1) != o.value.substr(i,1))
o.value =""; 
if (validdiv.indexOf(DatePattern.charAt(o.value.length))!= -1)
o.value += DatePattern.charAt(o.value.length); if ((validnum.indexOf(keypressed) == -1) || (o.value.length > 9)) return false; 

if (DatePattern.charAt(o.value.length)=="d"){ if(DatePattern.charAt(o.value.length-1)!="d"){ if(parseFloat(keypressed)>3){ o.value += '0' + keypressed + DatePattern.charAt(o.value.length+2); }else{ o.value += keypressed; }
}else{ o.value += keypressed + DatePattern.charAt(o.value.length+1); }
}else if (DatePattern.charAt(o.value.length)=="M"){ if(DatePattern.charAt(o.value.length-1)!="M"){ if(parseFloat(keypressed)>1){ o.value += '0' + keypressed + DatePattern.charAt(o.value.length+2); }else{ o.value += keypressed; }
}else{ if(parseFloat(keypressed)<3)
o.value += keypressed + DatePattern.charAt(o.value.length+1); else if(o.value.charAt(o.value.length-1)=="0")
o.value += keypressed; }
}else if (DatePattern.charAt(o.value.length)=="y"){ if(DatePattern.charAt(o.value.length-1)!="y"){ if(parseFloat(keypressed)>2||parseFloat(keypressed)<1){ o.value += '20' + keypressed; }else{ o.value += keypressed; }
}else{ o.value += keypressed; }
}else{ o.value += DatePattern.charAt(o.value.length); }
if(o.value.length>9){ var cmonth = parseFloat(o.value.substr(DatePattern.indexOf("MM"),2)); var cday = parseFloat(o.value.substr(DatePattern.indexOf("dd"),2)); var cyear = parseFloat(o.value.substr(DatePattern.indexOf("yyyy"),4)); if(numDaysIn(cmonth,cyear)<cday){ o.style.backgroundColor='#FFFFC0'; marktextrange(o,DatePattern.indexOf("dd"),DatePattern.indexOf("dd")+2); return false; }

if(cyear>2078 || cyear<1900){ o.style.backgroundColor='#FFFFC0'; marktextrange(o,DatePattern.indexOf("yyyy"),DatePattern.indexOf("yyyy")+4); return false; }
o.style.backgroundColor=''; }
selectedtext=false; 
return false; }

function y2k(number) { return (number < 1000) ? number + 1900 : number; }

function isDate (day,month,year) { var today = new Date(); year = ((!year) ? y2k(today.getYear()):year); month = ((!month) ? today.getMonth():month-1); if (!day) return false
var test = new Date(year,month,day); if ( (y2k(test.getYear()) == year) && (month == test.getMonth()) && (day == test.getDate()) )
return true; else
return false; }

function leapYear(yr) { if (((yr % 4 == 0) && yr % 100 != 0) || yr % 400 == 0)
return true; else return false; }
function numDaysIn(mth, yr) { if (mth==4 || mth==6 || mth==9 || mth==11) return 30; else if ((mth==2) && leapYear(yr)) return 29; else if (mth==2) return 28; else return 31; }

function FixPatternTime(TimePattern){ TimePattern = RegxReplace(TimePattern,"h{1,2}","hh"); TimePattern = RegxReplace(TimePattern,"m{1,2}","mm"); return RegxReplace(TimePattern,"[:\.]?s{1,2}",""); }
function FixPatternDate(DatePattern){ DatePattern = RegxReplace(DatePattern,"d{1,2}","dd"); DatePattern = RegxReplace(DatePattern,"M{1,2}","MM"); return RegxReplace(DatePattern,"y{1,4}","yyyy"); }
function RegxReplace(inputString, fromString, toString){ var regString = new RegExp(fromString, "gi")
return inputString.replace(regString, toString)
}

function CheckDateFormat(s){ i=0; var sLength = s.length; while (i < sLength) { if(DatePattern.charAt(i)=="d")
if((sLength>=(i+1)) && (DatePattern.charAt(i+1)=="d")){ if((s.charAt(i+1)<"0") || (s.charAt(i+1)>"9"))
alert("0" + s.charAt(i) + "/"); else
if((s.charAt(i)<"0") || (s.charAt(i)>"3"))
return false; }else{ if((s.charAt(i)<"0") || (s.charAt(i)>"9"))
return false; }
else if(DatePattern.charAt(i)=="M")
if((sLength>=(i+1)) && (DatePattern.charAt(i+1)=="M")){ if((s.charAt(i+1)<"0") || (s.charAt(i+1)>"9"))
alert("0" + s.charAt(i) + "/"); else
if((s.charAt(i)<"0") || (s.charAt(i)>"1"))
return false; }else{ if((s.charAt(i)<"0") || (s.charAt(i)>"2"))
return false; }
else if(DatePattern.charAt(i)=="y"){ var i2 = i; if((sLength>=(i+3)) && (DatePattern.charAt(i+3)=="y")){ while (i2 < sLength) { if((s.charAt(i2)<"0") || (s.charAt(i2)>"9"))
break; i2++
}
if((i2-i)<4) return false; }else{ while (i2 < sLength) { if((s.charAt(i2)<"0") || (s.charAt(i2)>"9"))
break; i2++
}
alert(i2-i); if(((i2-i)!= 2)) return false; }
i=(i2+1); }
i++ ; }
return true; }

function addZero(vNumber){ return ((vNumber < 10) ? "0" : "") + vNumber
}

var lastObjData=""; function dateFocus(obj, restore){ if(restore && obj.value.length==0 && lastObjData.length==5)
{ obj.value = lastObjData; }
else if(!restore && obj.value.length==5)
{		lastObjData = obj.value; obj.value = ""; }
}

function formatDate(vDate, vFormat){ vFormat = FixPatternDate(vFormat); var vDay                      = addZero(vDate.getDate()); var vMonth            = addZero(vDate.getMonth()+1); var vYearLong         = addZero(vDate.getFullYear()); var vYearShort        = addZero(vDate.getFullYear().toString().substring(3,4)); var vYear             = (vFormat.indexOf("yyyy")>-1?vYearLong:vYearShort)
var vHour             = addZero(vDate.getHours()); var vMinute           = addZero(vDate.getMinutes()); var vSecond           = addZero(vDate.getSeconds()); var vDateString       = vFormat.replace(/dd/g, vDay).replace(/MM/g, vMonth).replace(/y{1,4}/g, vYear)
vDateString           = vDateString.replace(/hh/g, vHour).replace(/mm/g, vMinute).replace(/ss/g, vSecond)
return vDateString
}

function getISODate(d){ var dateletters = "ymd"; var isoDate = ""; for(var j=0;j<3;j++)
for(var i=0;i<datepattern.length;i++)
{ if(datepattern.toLowerCase()[i]==dateletters[j])
isoDate += d[i].toString(); }
return isoDate; }
