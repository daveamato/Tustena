/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

var GroupPostBack = true; var DownloadElement; 
function checkMode(url, lb, obj){ if(GroupPostBack){ __doPostBack(obj.id.replace('_','$')); }else{ DownloadXML(url, lb); if (event.preventDefault){ event.preventDefault(); } else { event.returnValue = false; }
}
return false; }

function DownloadXML(url, lb){ if( !DownloadElement || !DownloadElement.downloadReady) return; DownloadElement.CancelLoading(); var content = DownloadElement.LoadXML( url, false ); if( content == null ){ } else { if( content.status != null ){ var message = "Text Download Error:  " + content.status + " - " + content.statusText; SomeError( message ); } else { if( content.parseError && content.parseError.reason != "" ){ var pars = content.parseError; var message = "Parsing Error:  " + pars.errorCode + " - " +
pars.reason + "\nFile:  " + pars.url + "\nLine: " +
pars.line + " at Character: " + pars.linepos +
"\n\n" + pars.srcText; SomeError( message ); } else { populateUniqueList( content, lb ); }
}
}
return false; }

function SomeError( content ){ alert( content ); }

function populateUniqueList(content, lb){ mylb = document.getElementById(lb)
while (mylb.options.length > 0) { mylb.options[0] = null; }
var objNodeListNames=content.getElementsByTagName("AccountName"); var objNodeListValues=content.getElementsByTagName("uid"); var newOption; var testItem; var testValue; for (var i=0; i<objNodeListNames.length; i++)
{ testItem=objNodeListNames.item(i).text; testValue=objNodeListValues.item(i).text; if(testItem !="" ){ newOption = document.createElement("OPTION"); mylb.add(newOption); newOption.value=testValue; newOption.innerText=testItem; }

}
}


function moverOffice(move,inb,outb,GrValue){ 
var inBox = document.getElementById(inb); var outBox = document.getElementById(outb); 
if(move == 'addall' || move == 'add')
{ for(x = 0;x<(inBox.length);x++)
{ if(inBox.options[x].selected || move == 'addall')
{ with(outBox)
{ options[options.length] = new Option(inBox.options[x].text,inBox.options[x].value); }
inBox.options[x] = null; x = -1; }
}
sortSelect(outBox); }
if(move == 'removeall' || move == 'remove')
{ 
for(x = 0;x<(outBox.length);x++)
{ if(outBox.options[x].selected || move == 'removeall')
{ with(inBox)
{ options[options.length] = new Option(outBox.options[x].text,outBox.options[x].value); }
outBox.options[x] = null; x = -1; }
}
sortSelect(inBox); }

var GroupValuetxt = "|"; for (x=0;x<(outBox.length);x++)
{ GroupValuetxt += outBox.options[x].value + "|"; }

document.getElementById(GrValue).value = GroupValuetxt; 
return true; }

function compareText (option1, option2) { return option1.text < option2.text ? -1 :
option1.text > option2.text ? 1 : 0; }

function sortSelect (select) { if(!IE4plus) return; var options = new Array(select.options.length); for (var i = 0; i < options.length; i++)
options[i] =
new Option (
select.options[i].text,
select.options[i].value,
select.options[i].defaultSelected,
select.options[i].selected
); options.sort(compareText); select.options.length = 0; for (var i = 0; i < options.length; i++)
select.options[i] = options[i]; }
