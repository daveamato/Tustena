<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectEvents.ascx.cs" Inherits="Digita.Tustena.Project.ProjectEvents" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<twc:jscontrolid id="jsc" runat=server Identifier="Event"/>
<script type="text/javascript" src="/js/clone.js"></script>
<script>
var idcevent=1;

function addEvent()
{
	cloneObj('EventItem',idcevent++,'EventContainer');
	var tempidcdoc=idcevent-1;
	clearEvent('_'+(tempidcdoc));
}

function removeEvent(cloneparam1)
{
    var d = document.getElementById("EventToDelete");
	if(cloneparam1.indexOf('_')>0)
	{
	   var suffix=cloneparam1.substr(cloneparam1.indexOf('_'));
	   d.value += document.getElementById("EventId"+suffix).value+"|";
	}else
	   d.value += document.getElementById("EventId").value+"|";
	removeCloned(cloneparam1,'EventContainer');
	idcevent--;
	if(idcevent<1)
		clearEvent('');
}

function clearEvent(suffix)
{
	document.getElementById("EventTxt"+suffix).value='';
	document.getElementById("EventDate"+suffix).value='';
	document.getElementById("EventId"+suffix).value='';
	document.getElementById("EventSection"+suffix).selectedIndex=0;
}

function FillEvent(suffix,eventtxt,eventdate,section,eventid)
{
    var evs=null;
    if(suffix>0){
        document.getElementById("EventTxt_"+suffix).value=eventtxt;
	    document.getElementById("EventDate_"+suffix).value=eventdate;
	    document.getElementById("EventId_"+suffix).value=eventid;
	    evs = document.getElementById("EventSection_"+suffix);
    }else{
	    document.getElementById("EventTxt").value=eventtxt;
	    document.getElementById("EventDate").value=eventdate;
	    document.getElementById("EventId").value=eventid;
	    evs = document.getElementById("EventSection");
	}

	//evs.selectedIndex=-1;
	    for(i=0;i<evs.options.length;i++)
	    {
	        if(section==evs.options[i].value)
	        {
	            evs.selectedIndex=i;
	            break;
	        }
	    }
}
</script>
<table cellpadding=0 cellspacing=0 class=normal width="100%">
    <tr>
        <td>Eventi&nbsp;<img src=/i/plus.gif onclick="addEvent();" style="cursor:pointer;">
        <input type=text id="EventToDelete" name="EventToDelete" style="display:none">
        </td>
    </tr>
    <tr>
        <td id="EventContainer" width="100%">
         <table cellpadding=0 cellspacing=5 id="EventItem" width="100%" style="border-bottom:1px solid #000000">
                <tr>
                    <td valign=top width="50%">
                        <div>Evento&nbsp;<img src=/i/erase.gif cloneparam1="EventItem" onclick="removeEvent(this.getAttribute('cloneparam1'));" style="cursor:pointer;"></div>
                        <input type=text id="EventTxt" name="EventTxt" class="BoxDesign" />
                        <input type=text id="EventId" name="EventId" style="display:none" value="-1">
                    </td>
                    <td valign=top width="25%">
                        <div>Data</div>
                        <table width="100%" cellspacing=0 cellpadding=0>
			            <tr><td>
			                   <input type=text id="EventDate" name="EventDate" Class="BoxDesign" ReadOnly>
			               </td>
			               <td width="30">
			                  &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" cloneparam1="EventDate" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+this.getAttribute('cloneparam1')+'&Start='+(document.getElementById(''+this.getAttribute('cloneparam1')+'')).value,event,195,195)">
			               </td>
			            </tr>
                        </table>
                    </td>
                    <td valign=top width="25%">
                        <div>Sezione</div>
                        <asp:Literal ID="litSectionList" runat=server></asp:Literal>
                     </td>
                </tr>
         </table>
        </td>
    </tr>
</table>
