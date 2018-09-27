/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

function HideReminder(o)
{ var obj = getElement("HeaderReminder"); var obj2 = getElement("BodyReminder"); if(o.checked)
{ obj.style.display = ''; obj2.style.display = ''; }else{ obj.style.display = 'none'; obj2.style.display = 'none'; }
}

function Hide(objid)
{ var obj = getElement(objid); var objbtn = getElement(objid + 'btn'); 
if (obj.style.display == 'none'){ obj.style.display = ''; objbtn.innerHTML = '<img src=/images/up.gif>'; }else{ obj.style.display = 'none'; objbtn.innerHTML = '<img src=/images/down.gif>'; }
}

function viewday(dd,mm,yyyy){ window.location.href='agenda.aspx?dy='+javadate(dd,mm,yyyy); }

function javadate(dd,mm,yyyy){ DatePattern = RegxReplace(DatePattern,"d{1,2}",dd); DatePattern = RegxReplace(DatePattern,"m{1,2}",mm); return RegxReplace(DatePattern,"y{1,4}",yyyy); }

function RegxReplace(inputString, fromString, toString){ var regString = new RegExp(fromString, "gi")
return inputString.replace(regString, toString)
}

function contains(parent,child){ if (parent==child) return true; var t = child; if (t && t.parentNode){ if (t.parentNode == parent)
return true; else
return contains(parent,t.parentNode); }  return false; }

function mOvr(src,clrOver,e){ var relatedTarget = e.relatedTarget || e.fromElement; if (!contains(src,relatedTarget)) { src.style.cursor = 'hand'; src.className = clrOver; }
}
function mOut(src,clrIn,e){ var relatedTarget = e.relatedTarget || e.toElement; if (!contains(src,relatedTarget)) { src.style.cursor = 'default'; src.className = clrIn; }
}


function Rimuovi(idx,ac){ switch (ac)
{ case 0 :
if (confirm(confmsg)) location.href=idx; break; case 1 :
if (confirm(confmsg)){ if (confirm(confmsg2)) { location.href=idx+"&a=1"; }
else{ location.href=idx; }
}
break; case 10 :
if (confirm(confmsg)){ if (confirm(confmsg3)) { location.href=idx+"&act=1"; }
else{ location.href=idx; }
}
break; case 11 :
if (confirm(confmsg)){ if (confirm(confmsg2)) { if (confirm(confmsg3)) { location.href=idx+"&a=1&act=1"; }
else{ location.href=idx+"&a=1"; }
}
else{ if (confirm(confmsg3)) { location.href=idx+"&act=1"; }
else{ location.href=idx; }
}
}
break; default:
if (confirm(confmsg)){ location.href=idx; }
break; }
}

function OpenTEvent(mode,data){ location.href="appointment.aspx?m=25&si=2&mode=" + mode + "&data=" + data; }
function OpenEventS(mode,data){ location.href="events.aspx?m=25&si=2&mode=" + mode + "&data=" + data; }
function ExportVcal(mode,m){ var month = document.getElementById("currentmonth"); var year = document.getElementById("currentyear"); NewWindow("vCal.aspx?render=no&mode=" + mode + "&month=" + month.value + "&year=" + year.value, "", 400,200,"no")
}
function OpenMeeting(data){ location.href="meeting.aspx?m=25&si=2&data=" + data; }
function PrintApp(id){ NewWindow("PrintApp.aspx?render=no&id=" + id, "", 400,200,"no"); }



function ViewCompany(thisid,e)
{ 
if(typeof(thisid)!="undefined" && thisid.length!=0)
thisid+="_"; else
thisid=""; 
var id = (getElement(thisid+"CompanyId")).value; if(id.length>0)
CreateBox('/Common/ViewCompany.aspx?render=no&id='+id,e,500,300); }

function ViewContact(thisid,e)
{ 
if(typeof(thisid)!="undefined" && thisid.length!=0)
thisid+="_"; else
thisid=""; 
var id = (getElement(thisid+"F_ContactID")).value; if(id.length>0)
CreateBox('/Common/ViewContact.aspx?render=no&id='+id,e,500,300); }



function ShowRoom(thisid)
{ if(typeof(thisid)!="undefined")
thisid+="_"; else
thisid=""; 
if (getElement(thisid+"CheckSite").checked){ getElement(thisid+"TblRoom").style.display= "inline"; getElement(thisid+"TblAddress").style.display= "none"; }else{ getElement(thisid+"TblRoom").style.display= "none"; getElement(thisid+"TblAddress").style.display= "inline"; }
}

var NetSubmit =""
function ShowRecurrence(){ if (NetSubmit=="") NetSubmit = getElement("Submit").href; if (getElement("CheckRecurrent").checked){ getElement("RecTitle").style.display= "inline"; getElement("TableRecurrent").style.display= "inline"; getElement("Submit").href="javascript:{ValidateHidden()};" + NetSubmit; }else{ getElement("Submit").href = NetSubmit; getElement("TableRecurrent").style.display= "none"; getElement("RecTitle").style.display= "none"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; }
}


function ValidateHidden(){ }

function ActivateRecMode(){ getElement("RecMode").disabled=false; ActivateForms(); }

function ActivateForms(){ switch (getElement("RecMode").value){ case "1":
getElement("SpanRecDaily").style.display= "inline"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecMonthDay").style.display= "none"; break; case "2":
getElement("SpanRecWeekly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecMonthDay").style.display= "none"; break; case "3":
getElement("SpanRecMonthly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecMonthDay").style.display= "none"; break; case "4":
getElement("SpanRecMonthDay").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "5":
getElement("SpanRecYearly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecMonthDay").style.display= "none"; break; case "6":
getElement("SpanRecYearlyDay").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecMonthDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; break; }
}

function AllDay(thisid)
{ if(typeof(thisid)!="undefined")
thisid+="_"; else
thisid=""; 
var e = getElement(thisid+"CkAllDay"); 
if(e.checked)
{ getElement(thisid+"F_StartHour").value = getElement(thisid+"HiddenStartHour").value; getElement(thisid+"F_EndHour").value = getElement(thisid+"HiddenEndHour").value; getElement(thisid+"F_StartHour").readOnly= true; getElement(thisid+"F_EndHour").readOnly= true; }
else
{ getElement(thisid+"F_StartHour").readOnly= false; getElement(thisid+"F_EndHour").readOnly= false; }

}






function AppointmentVerify(id,e,thisid){ if(typeof(thisid)!="undefined")
thisid+="_"; else
thisid=""; 
if((getElement(thisid+"F_StartDate")).value.length>0){ var idute=(id==1)?getElement(thisid+"UserApp"):getElement(thisid+"IdCompanion"); if(idute.value.length>0)
if(id==1)
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement(thisid+"F_StartDate")).value+"&id="+idute.value,e,250,150); else
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement(thisid+"F_StartDate")).value+"&id="+getElement(thisid+"UserApp").value+"|"+idute.value,e,500,150); else
alert(appvrfy); }else{ alert(appvrfy2); }
}

function RemoveCompanion(thisid){ if(typeof(thisid)!="undefined")
thisid+="_"; else
thisid=""; 
var i=getElement(thisid+"IdCompanion"); var a=getElement(thisid+"Companion"); i.value=""; a.value=""; }

function DateCompare(date1,date2){ alert(date1.substring(0,4)); var dateF = new Date(date1.substring(0,4),
date1.substring(4,6)-1,
date1.substring(6,8)); var dateI = new Date(date2.substring(0,4),
date2.substring(4,6)-1,
date2.substring(6,8)); alert(dateF+","+dateI); if (dateF < dateI)
{ if (!confirm(confdate))
getElement(control).value = ""; }
}

function DatePosition(dateString,control) { var now = new Date(); var today = new Date(now.getYear(),now.getMonth(),now.getDate()); var century = parseInt(now.getYear()/100)*100; var date = new Date(dateString.substring(0,4),
dateString.substring(4,6)-1,
dateString.substring(6,8)); if (date < today)
{ if (!confirm(confdate))
getElement(control).value = ""; }
else if (date > today)
{ }
else
{ }
}

function BeforeSubmit(thisid){ if(typeof(thisid)!="undefined")
thisid+="_"; else
thisid=""; 
var F_Title = getElement(thisid+"F_Title"); var F_Title2 = getElement(thisid+"F_Title2"); 
if (F_Title.value.length==0 && F_Title2.value.length>0)
F_Title.value=autofill; 
var F_StartHour = getElement(thisid+"F_StartHour"); var F_EndHour = getElement(thisid+"F_EndHour"); 
if (F_EndHour.value.length==0 && F_StartHour.value.length>0){ var i = (F_StartHour.value.substr(0,1)!="0")?parseInt(F_StartHour.value.substr(0,2)):parseInt(F_StartHour.value.substr(1,1)); i++; if(F_StartHour.value.length<5)
F_EndHour.value=i+F_StartHour.value.substr(1,3); else
F_EndHour.value=i+F_StartHour.value.substr(2,3); }

}
