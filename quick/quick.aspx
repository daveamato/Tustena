<%@ Page Language="c#" Codebehind="quick.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.quick" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <style>
.lc0{background-color:#FFFFFF}
.lc1{background-color:#FDEEF4}
.lc2{background-color:#FCDFFF}
.lc3{background-color:#CFECEC}
.lc4{background-color:#B5EAAA}
.lc5{background-color:#FFF8C6}
.lc6{background-color:#BDEDFF}
.legend{padding:2px;border:1px solid silver;cursor:pointer;}
</style>
    <twc:LocalizedScript resource="Quicktxt14" runat="server" />

    <script>
function CheckOpp()
{
   document.getElementById("colorlegend").style.display='';
}

function searchcontact()
{
debugger;
	var sTB = document.getElementById("SearchTextBox");
	var oppID = document.getElementById("TextboxOpportunityID");
	var flagSearch0 = document.getElementById("FlagSearch_0");
	var flagSearch1 = document.getElementById("FlagSearch_1");
	var flagSearch2 = document.getElementById("FlagSearch_2");
	var flagSearch3 = document.getElementById("FlagSearch_3");
	var iframeContacts = document.getElementById("contacts");
	var ifilterActivity = document.getElementById("filterActivity");

	var sRadioChecked=0;

	var sfilterActivity=0;
	if (ifilterActivity.checked==true)sfilterActivity=1;

	if (flagSearch0.checked==true)
		sRadioChecked=0
	else if (flagSearch1.checked==true)
			sRadioChecked=1
		else if (flagSearch2.checked==true)
				sRadioChecked=2
			else if (flagSearch3.checked==true)
				sRadioChecked=3

	if(sTB.value.length>0 || oppID.value.length>0)
	{
		iframeContacts.src = "quicksearch.aspx?render=no&searchText=" + sTB.value + "&searchtype=" + sRadioChecked + "&oppID=" + oppID.value + "&checkAct=" + sfilterActivity;
	}else
	{
	alert(Quicktxt14);
	}
	return false;
}

function refreshactivity(selectedContact,action,actid)
{
	var iframeactivity = document.getElementById("activity");
	var oppID = document.getElementById("TextboxOpportunityID");
	iframeactivity.src = "quickactivity.aspx?render=no&contactID=" + selectedContact + "&Action=" + action + "&oppID=" + oppID.value + "&actID=" + actid;
}

function refreshquicklog(){
	var Info2 = document.getElementById("Info2");
	Info2.src = "quicklogview.aspx?render=no";
}

function SelABCHeader(letter)
{
	var oppid = document.getElementById('TextboxOpportunityID').value;
	var iframeContacts = document.getElementById("contacts");
	iframeContacts.src = "quicksearch.aspx?render=no&searchFromLetter=" + letter + "&oppID="+oppid+"&rnd="+Math.random();
}

function CleanField(f){
	var fi=getElement(f);
	fi.value="";
	document.getElementById("colorlegend").style.display='none';
 }

function FilterState(state)
{
		var IFrameObj;
		if (IE4plus) IFrameObj = window.document.frames['contacts'];
		 else IFrameObj = window.document.getElementById('contacts');
		 IFrameObj.switchonoff(state);
}

function FilterPhn(type)
{
		var IFrameObj;
		if (IE4plus) IFrameObj = window.document.frames['contacts'];
		 else IFrameObj = window.document.getElementById('contacts');
		 IFrameObj.filterphn(type);
}

function SetParams(p,v)
	{
		var IFrameObj;
		if (IE4plus) IFrameObj = window.document.frames['activity'].document;
		 else IFrameObj = window.document.getElementById('activity').contentDocument;
		var param = IFrameObj.getElementById(p);
		param.value=v;
	}
    </script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" valign="top" class="SideBorderLinked">
                    <table width="98%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td align="left">
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td class="sideContainer">
                                            <div class="sideTitle">
                                                <%=wrm.GetString("Quicktxt7")%>
                                            </div>
                                            <iframe id="Info1" name="Info1" runat="server" allowtransparency="true" width="100%"
                                                height="300" src="quickappointments.aspx?render=no" scrolling="no" frameborder="0"
                                                marginheight="0" marginwidth="0" bgcolor="#e5e5e5"></iframe>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="bottom">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="sideContainer">
                                            <div class="sideTitle">
                                                <%=wrm.GetString("Quicktxt13")%>
                                            </div>
                                            <iframe id="Info2" name="Info2" runat="server" allowtransparency="true" width="100%"
                                                height="124" src="quicklogview.aspx?render=no" scrolling="no" frameborder="0"
                                                marginheight="0" marginwidth="0" bgcolor="#e5e5e5"></iframe>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" class="pageStyle">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="sideContainer">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td width="20%" align="left">
                                            <asp:RadioButtonList ID="FlagSearch" runat="server" />
                                        </td>
                                        <td width="20%">
                                            <asp:TextBox ID="SearchTextBox" runat="server" cssclass="BoxDesign" />
                                        </td>
                                        <td width="10%" align="left" style="padding-left: 3px">
                                            <asp:LinkButton ID="BtnSearch" runat="server" cssclass="save" />
                                        </td>
                                        <td width="25%" align="left">
                                            <asp:CheckBox ID="filterActivity" runat=server Text="Activity" CssClass=normal />
                                        </td>
                                        <td width="5%" align="left">
                                            <%=wrm.GetString("Acttxt31")%>
                                        </td>
                                        <td width="20%" align="right">
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="TextboxOpportunityID" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                        <asp:TextBox ID="TextboxOpportunity" runat="server" Width="100%" CssClass="BoxDesign"
                                                            ReadOnly="true"></asp:TextBox>
                                                    </td>
                                                    <td width="60" nowrap>
                                                        &nbsp;<img src="/i/user.gif" alt="<%=wrm.GetString("AcTooltip7")%>" border="0" style="cursor: pointer"
                                                            onclick="CreateBox('/common/PopOpportunity.aspx?render=no&textbox=TextboxOpportunity&textboxID=TextboxOpportunityID&event=CheckOpp()',event)">
                                                        &nbsp;<img style="cursor: pointer" onclick="CleanField('TextboxOpportunityID');CleanField('TextboxOpportunity');"
                                                            alt='<%=wrm.GetString("AcTooltip11")%>' src="/i/erase.gif" border="0">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" colspan="5">
                                <asp:Label ID="LblHeader" runat="server" EnableViewState="False" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="5" class="sideContainer">
                                <table border="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <table border="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td class="GridTitle" width="80">
                                                        <%=wrm.GetString("Quicktxt6")%>
                                                    </td>
                                                    <td class="GridTitle" width="25%">
                                                        <%=wrm.GetString("Reftxt15")%>
                                                        &nbsp;<%=wrm.GetString("Reftxt16")%></td>
                                                    <td class="GridTitle" width="25%">
                                                        <%=wrm.GetString("Reftxt17")%>
                                                    </td>
                                                    <td class="GridTitle" width="20%">
                                                        <%=wrm.GetString("Reftxt48")%>
                                                    </td>
                                                    <td class="GridTitle">
                                                        <%=wrm.GetString("Reftxt25")%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="19">
                                            <img src="/images/reload.gif" onclick="document.getElementById('contacts').src=document.getElementById('contacts').src"></td>
                                    </tr>
                                </table>
                                <iframe id="contacts" name="contacts" runat="server" allowtransparency="true" width="100%"
                                    height="200" src="quicksearch.aspx?render=no" scrolling="yes" frameborder="0"
                                    marginheight="0" marginwidth="0" bgcolor="#e5e5e5"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" colspan="5">
                                <table border="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <span class="Save" style="cursor: pointer" onclick="refreshactivity('','TodayActivity');">
                                                <%=wrm.GetString("Deftxt25")%>
                                            </span><span class="Save" style="cursor: pointer" onclick="refreshactivity('','TodoActivity');">
                                                <%=wrm.GetString("Deftxt26")%>
                                            </span>
                                            </td>
                                            <td align=right style="padding-right:10px">
                                                <asp:Label ID="phnLegend" runat="server" />
                                            </td>
                                            <td align=right>
                                                <asp:Label ID="colorlegend" runat="server" />
                                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="5" class="sideContainer">
                                <iframe id="activity" name="activity" runat="server" allowtransparency="true" width="100%"
                                    height="250" src="quickactivity.aspx?render=no" scrolling="yes" frameborder="0"
                                    marginheight="0" marginwidth="0" bgcolor="#e5e5e5"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
