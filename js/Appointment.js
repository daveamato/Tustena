/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/


function ViewCompany(thisid,e)
{ if(thisid!=null)
thisid+="_"; else
thisid=""; 
var id = (getElement(thisid+"txtCompanyID")).value; if(id.length>0)
CreateBox('/Common/ViewCompany.aspx?render=no&id='+id,e,500,300); }

function ViewContact(thisid,e)
{ if(thisid!=null)
thisid+="_"; else
thisid=""; 
var id = (getElement(thisid+"txtContactID")).value; if(id.length>0)
CreateBox('/Common/ViewContact.aspx?render=no&id='+id,e,500,300); }



function ShowRoom(thisid)
{ if(thisid!=null)
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
getElement("SpanRecDaily").style.display= "inline"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "2":
getElement("SpanRecWeekly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "3":
getElement("SpanRecMonthly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "4":
getElement("SpanRecYearlyDay").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "5":
getElement("SpanRecYearly").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; break; case "6":
getElement("SpanRecYearlyDay").style.display= "inline"; getElement("SpanRecDaily").style.display= "none"; getElement("SpanRecWeekly").style.display= "none"; getElement("SpanRecMonthly").style.display= "none"; getElement("SpanRecYearlyDay").style.display= "none"; getElement("SpanRecYearly").style.display= "none"; break; }
}

function AllDay(thisid){ if(thisid!=null)
thisid+="_"; else
thisid=""; 
getElement(thisid+"TxtStartHour").value = getElement(thisid+"HiddenStartHour").value; getElement(thisid+"TxtEndHour").value = getElement(thisid+"HiddenEndHour").value; }

function AppointmentVerify(id,e,thisid){ if(thisid!=null)
thisid+="_"; else
thisid=""; 
if((getElement(thisid+"TxtStartDate")).value.length>0){ var idute=(id==1)?getElement(thisid+"ddlUser"):getElement(thisid+"TxtAccompanistID"); if(idute.value.length>0)
if(id==1)
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement(thisid+"TxtStartDate")).value+"&id="+idute.value,e,250,150); else
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement(thisid+"TxtStartDate")).value+"&id="+getElement(thisid+"ddlUser").value+"|"+idute.value,e,500,150); else
alert(appvrfy); }else{ alert(appvrfy2); }
}

function RemoveCompanion(thisid){ if(thisid!=null)
thisid+="_"; else
thisid=""; 
var i=getElement(thisid+"TxtAccompanistID"); var a=getElement(thisid+"TxtAccompanist"); i.value=""; a.value=""; }

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

function BeforeSubmit(thisid){ if(thisid!=null)
thisid+="_"; else
thisid=""; 
var txtContact = getElement(thisid+"TxtContact"); var txtCompany = getElement(thisid+"TxtCompany"); 
if (txtContact.value.length==0 && txtCompany.value.length>0)
txtContact.value=autofill; 
var txtStartHour = getElement(thisid+"TxtStartHour"); var txtEndHour = getElement(thisid+"TxtEndHour"); 
if (txtEndHour.value.length==0 && txtStartHour.value.length>0){ var i = (txtStartHour.value.substr(0,1)!="0")?parseInt(txtStartHour.value.substr(0,2)):parseInt(txtStartHour.value.substr(1,1)); i++; 
if(txtStartHour.value.length<5)
txtEndHour.value=i+txtStartHour.value.substr(1,3); else
txtEndHour.value=i+txtStartHour.value.substr(2,3); }

}
