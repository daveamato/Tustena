/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function changestatus(mode)
{ switch(mode){ case '2': document.getElementById("Activity_ToDo_0").checked = true; break; }
}
function ViewCompany(e)
{ var id = (document.getElementById("TextboxCompanyID")).value; if(id.length>0)
CreateBox('/Common/ViewCompany.aspx?render=no&id='+id,e,500,300); }
function ViewContact(e)
{ var id = (document.getElementById("TextboxContactID")).value; if(id.length>0)
CreateBox('/Common/ViewContact.aspx?render=no&id='+id,e,500,300); }
function ViewLead(e)
{ var id = (document.getElementById("TextboxLeadID")).value; if(id.length>0)
CreateBox('/Common/ViewLead.aspx?render=no&id='+id,e,500,350); }
function ChooseMailDest(obj, e)
{ if(obj.checked){ var id1 = (document.getElementById("TextboxCompanyID")).value; var id2 = (document.getElementById("TextboxContactID")).value; var id3 = (document.getElementById("TextboxLeadID")).value; if(id1.length>0 || id2.length>0 || id3.length>0){ CreateBox('/Common/ActivityMail.aspx?render=no&id1='+id1+'&id2='+id2+'&id3='+id3,e,500,250); document.getElementById("destinationEmail").style.display='inline'; }else{ alert(txtseldest)
obj.checked=false; }
}else{ document.getElementById("destinationEmail").style.display='none'
}
}

function AppointmentVerify(id,e){ if((getElement("TextBoxData")).value.length>0){ var idute=(id==1)?getElement("TextboxOwnerID"):getElement("IdCompanion"); 
if(idute.value.length>0)
if(id==1)
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement("TextBoxData")).value+"&id="+idute.value,e,250,150); else
CreateBox("/Common/AppVerify.aspx?render=no&date="+(getElement("TextBoxData")).value+"&id="+getElement("TextboxOwnerID").value+"|"+idute.value,e,500,150); else
alert(txtverdisp); }else{ alert(txtverdisp); }
}

function CalendarDateDiff(d)
{ }

function ClearDocument(){ (document.getElementById("DocumentDescription")).value = ""; (document.getElementById("IDDocument")).value = ""; var obj = document.getElementById("LinkDocument"); if (obj != null)
obj.style.display = "none"; }

function ActivateHours(){ var Activity_ToDo = document.getElementById("Activity_ToDo_1"); var Appointmenthours = document.getElementById("Appointmenthours"); try{ if(Activity_ToDo.checked){ Appointmenthours.style.display="inline"; }else{ Appointmenthours.style.display="none"; }
}catch(ex){}
}

function RemoveCompanion(){ var i=getElement("IdCompanion"); var a=getElement("Companion"); i.value=""; a.value=""; }

function ViewHideLog(){ var t=getElement("LogTable"); var v=getElement("LogView"); 
if(t.style.display=="inline"){ t.style.display="none"; v.innerText="[+]"; }else{ t.style.display="inline"; v.innerText="[-]"; }
}



function ExpandDescription(){ var d=getElement("TextboxDescription"); d.style.height="400px"; }


function TabClick(objName)
{ var obj = document.getElementById(objName); clickElement(obj); }

function EnableDisableQuote(checkBoxListId,checkBoxIndex)
{ var objItemChecked =
document.getElementById(checkBoxListId + '_' + checkBoxIndex); var CurrentElement =document.getElementById("SearchQuote"); 
if(objItemChecked == null)
{ return; }
var isChecked = objItemChecked.checked; if(isChecked)
{ DisableAll(CurrentElement, true)
}else{ DisableAll(CurrentElement, false)
}

}
