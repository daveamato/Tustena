<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="Office" TagName="SelectOffice" Src="~/Common/SelectOffice.ascx" %>

<%@ Page Language="c#" Trace="false" Codebehind="Meeting.aspx.cs" Inherits="Digita.Tustena.Meeting"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />

    <script type="text/javascript" src="/js/calendars.js"></script>
    <script type="text/javascript" src="/js/autodate.js"></script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/SelectOffice.js"></script>

    <script>
 function ViewCompany(e)
 {
	var id = (document.getElementById("CompanyId")).value;
	if(id.length>0)
		CreateBox('/Common/ViewCompany.aspx?render=no&id='+id,e,500,300);
 }
 function ViewContact(e)
 {
	var id = (document.getElementById("F_ContactID")).value;
	if(id.length>0)
		CreateBox('/Common/ViewContact.aspx?render=no&id='+id,e,500,300);
 }
    </script>

    <script>
function ShowAddress(){
	if (document.getElementById("CheckSite").checked){
		document.getElementById("TblRoom").style.display= "inline";
		document.getElementById("TblAddress").style.display= "none";
	}else{
		document.getElementById("TblRoom").style.display= "none";
		document.getElementById("TblAddress").style.display= "inline";
	}
}
function ShowRecurrence(){
	if (document.getElementById("CheckRecurrent").checked){
		document.getElementById("TableRecurrence").style.display= "inline";
	}else{
		document.getElementById("TableRecurrence").style.display= "none";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
	}
}



function ActivateRecMode(){
	//if (document.getElementById("RecType").value == "99"){
		document.getElementById("RecMode").disabled=false;
		ActivateForms();
	/*
	}else{
		document.getElementById("RecMode").disabled=true;
	}
	*/
}

function ActivateForms(){
	switch (document.getElementById("RecMode").value){
		case "1":
			document.getElementById("SpanRecDaily").style.display= "inline";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			break;
		case "2":
			document.getElementById("SpanRecWeekly").style.display= "inline";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			break;
		case "3":
			document.getElementById("SpanRecMonthly").style.display= "inline";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			break;
		case "4":
			document.getElementById("SpanRecMonthlyDay").style.display= "inline";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			break;
		case "5":
			document.getElementById("SpanRecYearly").style.display= "inline";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			break;
		case "6":
			document.getElementById("SpanRecYearlyDay").style.display= "inline";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
			break;
	}
}

function AllDay(){
	document.getElementById("F_StartHour").value = document.getElementById("HiddenStartHour").value;
	document.getElementById("F_EndHour").value = document.getElementById("HiddenEndHour").value;
}
    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <div class="HideForPrint">
                        <table width="98%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral1" Text="Caltxt5" runat="server" /></div>
                                    <div class="sideFixed">
                                        <asp:Literal ID="AgendaOwner" runat="server" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral2" Text="Options" runat="server" /></div>
                                    <a href="#" class="sideBtn" onclick="OpenTEvent('NEW','');">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral3" Text="Caltxt6" runat="server" /></a>
                                    <a href="#" class="sideBtn" onclick="OpenEventS('NEW','');">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral4" Text="Caltxt7" runat="server" />
                                    </a>
                                    <a href="#" class="sideBtn" onclick="OpenMeeting('');">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral8" Text="Caltxt45" runat="server" /></a>
                                    <asp:Literal ID="Office" runat="server" />
                                    <a href="#" class="sideBtn" onclick="location.href='agenda.aspx?m=25&si=2'">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral6" Text="Caltxt9" runat="server" /></a>
                                    <a href="#" class="sideBtn" onclick="location.href='agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2'">
                                        <twc:LocalizedLiteral ID="LocalizedLiteral7" Text="Caltxt52" runat="server" /></a>

                                    <asp:LinkButton ID="Back" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <twc:TustenaTabber ID="Tabber" Width="800" Expand="true" runat="server">
                        <twc:TustenaTab ID="AppointTab" LangHeader="Meettxt1" ClientSide="true" runat="server">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" class="pageTitle" valign="top">
                                        <twc:LocalizedLiteral Text="Meettxt1" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <table width="98%" align="center" runat="server" id="TEvento">
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="840" class="normal">
                                            <tr>
                                                <td>
                                                    <Office:SelectOffice ID="SelectOffice" runat="server"></Office:SelectOffice>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table class="normal" id="AppointmentCard" cellspacing="0" cellpadding="0" width="100%"
                                                        align="center" border="0" runat="server">
                                                        <tr>
                                                            <td valign="top" width="50%">
                                                                <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <input id="HiddenStartHour" type="hidden" runat="server">
                                                                            <input id="HiddenEndHour" type="hidden" runat="server">
                                                                            <twc:LocalizedLiteral Text="Meettxt10" runat="server" />
                                                                            <domval:RequiredDomValidator ID="RequiredFieldValidatorData" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="F_StartDate"></domval:RequiredDomValidator>
                                                                            <domval:CompareDomValidator ID="CvDate" runat="Server" ErrorMessage="*" ControlToValidate="F_StartDate"
                                                                                Type="Date" Operator="DataTypeCheck"></domval:CompareDomValidator></td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox class="BoxDesign" onkeypress="DataCheck(this,event)" ID="F_StartDate"
                                                                                            runat="server" MaxLength="10" EnableViewState="true"></asp:TextBox></td>
                                                                                    <td width="15">
                                                                                        <img src="/i/allday.gif" style="cursor: pointer" onclick="AllDay()"></td>
                                                                                    <td width="32">
                                                                                        &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=F_StartDate',event,195,195)"
                                                                                            src="/i/SmallCalendar.gif" border="0">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <twc:LocalizedLiteral Text="Meettxt11" runat="server" />
                                                                            <domval:RequiredDomValidator ID="RequiredFieldValidatorOra1" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="F_StartHour"></domval:RequiredDomValidator><domval:RegexDomValidator
                                                                                    ID="RegularExpressionValidatorOra1" runat="server" ErrorMessage="*" ControlToValidate="F_StartHour"
                                                                                    ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$"></domval:RegexDomValidator></td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox class="BoxDesign" onkeypress="HourCheck(this,event)" ID="F_StartHour"
                                                                                            runat="server"></asp:TextBox></td>
                                                                                    <td valign="middle" align="right" width="40">
                                                                                        <img onclick="HourUp('F_StartHour')" src="/images/up.gif">
                                                                                        <img onclick="HourDown('F_StartHour')" src="/images/down.gif">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <twc:LocalizedLiteral Text="Meettxt12" runat="server" />
                                                                            <domval:RequiredDomValidator ID="RequiredFieldValidatorOra2" runat="server" ErrorMessage="*"
                                                                                ControlToValidate="F_EndHour"></domval:RequiredDomValidator><domval:RegexDomValidator
                                                                                    ID="RegularExpressionValidatorOra2" runat="server" ErrorMessage="*" ControlToValidate="F_EndHour"
                                                                                    ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$"></domval:RegexDomValidator></td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox class="BoxDesign" onkeypress="HourCheck(this,event)" ID="F_EndHour"
                                                                                            runat="server"></asp:TextBox></td>
                                                                                    <td valign="middle" align="right" width="40">
                                                                                        <img onclick="HourUp('F_EndHour')" src="/images/up.gif">
                                                                                        <img onclick="HourDown('F_EndHour')" src="/images/down.gif">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <twc:LocalizedLiteral Text="Meettxt13" runat="server" />
                                                                            &nbsp;<img style="cursor: pointer" onclick="ViewContact(event)" src="/i/lens.gif">
                                                                        </td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox class="BoxDesign" ID="F_Title" runat="server" EnableViewState="true"></asp:TextBox></td>
                                                                                    <td width="30">
                                                                                        &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/popcontacts.aspx?render=no&textbox=F_Title&textbox2=F_Title2&textboxID=F_ContactID&textboxCompanyID=CompanyId&companyID=' + document.getElementById('CompanyId').value,event,400,200)"
                                                                                            src="/i/user.gif" border="0">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <twc:LocalizedLiteral Text="Meettxt58" runat="server" />
                                                                            &nbsp;<img style="cursor: pointer" onclick="ViewCompany(event)" src="/i/lens.gif">
                                                                        </td>
                                                                        <td>
                                                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:TextBox class="BoxDesign" ID="F_Title2" runat="server" EnableViewState="true"
                                                                                            jumpret="F_note"></asp:TextBox><asp:TextBox ID="CompanyId" Style="display: none"
                                                                                                runat="server"></asp:TextBox><asp:TextBox ID="F_ContactID" Style="display: none"
                                                                                                    runat="server"></asp:TextBox></td>
                                                                                    <td width="30">
                                                                                        &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=F_Title2&textbox2=CompanyId&ind=Address&cit=City&prov=Province&cap=CAP&tel=&current=' + document.getElementById('F_Title2').value,event,500,400)"
                                                                                            src="/i/user.gif" border="0">
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="20">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="top" width="50%">
                                                                <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <twc:LocalizedLiteral Text="Meettxt14" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="CheckSite" onclick="ShowAddress();" runat="server" Checked="true">
                                                                            </asp:CheckBox></td>
                                                                    </tr>
                                                                </table>
                                                                <table class="normal Tvis" id="TblRoom" cellspacing="2" cellpadding="0" width="100%"
                                                                    align="center" border="0">
                                                                    <tr>
                                                                        <td width="20%">
                                                                            <twc:LocalizedLiteral Text="Meettxt15" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox class="BoxDesign" ID="Room" runat="server" noret="true"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                                <table class="normal Tnas" id="TblAddress" cellspacing="2" cellpadding="0" width="100%"
                                                                    align="center" border="0">
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <twc:LocalizedLiteral Text="Meettxt16" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox class="BoxDesign" ID="Address" runat="server"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <twc:LocalizedLiteral Text="Meettxt17" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox class="BoxDesign" ID="City" runat="server"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <twc:LocalizedLiteral Text="Meettxt18" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox class="BoxDesign" ID="Province" runat="server"></asp:TextBox></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <twc:LocalizedLiteral Text="Meettxt19" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox class="BoxDesign" ID="CAP" runat="server" noret="true"></asp:TextBox></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <div class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Meettxt20" runat="server" /></div>
                                                                <asp:TextBox class="textareaautoform" ID="F_note" runat="server" Rows="5" EnableViewState="true"
                                                                    TextMode="MultiLine"></asp:TextBox></td>
                                                        </tr>
                                                        <tr id="HeaderRecurrence" runat="server">
                                                            <td colspan="3">
                                                                <table width="100%" border="0">
                                                                    <tr>
                                                                        <td class="BorderBottomTitles" align="left">
                                                                            <span class="divautoform"><b>
                                                                                <twc:LocalizedLiteral Text="Meettxt21" runat="server" />
                                                                            </b></span>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr id="CorpoRecurrence" runat="server">
                                                            <td valign="top">
                                                                <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <twc:LocalizedLiteral Text="Meettxt22" runat="server" />
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:CheckBox ID="CheckRecurrent" onclick="ShowRecurrence();ActivateRecMode();" runat="server">
                                                                            </asp:CheckBox></td>
                                                                    </tr>
                                                                </table>
                                                                <span id="TableRecurrence" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="40%">
                                                                            </td>
                                                                            <td width="60%">
                                                                                <asp:DropDownList ID="RecType" runat="server" old="true" onChange="ActivateRecMode();"
                                                                                    CssClass="BoxDesign" Visible="false">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt24" runat="server" />
                                                                                <domval:CompareDomValidator ID="CvRecDate" runat="Server" ErrorMessage="Data non valida"
                                                                                    ControlToValidate="RecDateStart" Type="Date" Operator="DataTypeCheck" /></td>
                                                                            <td width="60%">
                                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox class="BoxDesign" ID="RecDateStart" runat="server" MaxLength="10" EnableViewState="true"></asp:TextBox></td>
                                                                                        <td width="30">
                                                                                            &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecDateStart',event,195,195)"
                                                                                                src="/i/SmallCalendar.gif" border="0">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt25" runat="server" />
                                                                                <domval:CompareDomValidator ID="cvRecDataFine" runat="Server" ErrorMessage="*" ControlToValidate="RecEndDate"
                                                                                    Type="Date" Operator="DataTypeCheck" Display="Dynamic"></domval:CompareDomValidator></td>
                                                                            <td width="60%">
                                                                                <table cellspacing="0" cellpadding="0" width="100%">
                                                                                    <tr>
                                                                                        <td>
                                                                                            <asp:TextBox class="BoxDesign" ID="RecEndDate" runat="server" MaxLength="10" EnableViewState="true"></asp:TextBox></td>
                                                                                        <td width="30">
                                                                                            &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecEndDate',event,195,195)"
                                                                                                src="/i/SmallCalendar.gif" border="0">
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt26" runat="server" />
                                                                            </td>
                                                                            <td width="60%">
                                                                                <asp:DropDownList ID="RecMode" runat="server" old="true" onChange="ActivateForms();"
                                                                                    CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                    </table>
                                                                </span>
                                                            </td>
                                                            <td valign="top" colspan="2">
                                                                <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                    border="0">
                                                                    <tr>
                                                                        <td class="BorderBottomTitles" align="left">
                                                                            <b>
                                                                                <twc:LocalizedLiteral Text="Meettxt33" runat="server" />
                                                                            </b>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <span id="SpanRecDaily" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt34" runat="server" />
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecDayDays" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt35" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%" colspan="2">
                                                                                <twc:LocalizedLiteral Text="Meettxt36" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:CheckBox ID="RecWorkingDay" runat="server" EnableViewState="true"></asp:CheckBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </span><span id="SpanRecWeekly" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt34" runat="server" />
                                                                            </td>
                                                                            <td width="20%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecSettSS" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                            <td width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt37" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top" width="40%">
                                                                                <twc:LocalizedLiteral Text="Meettxt38" runat="server" />
                                                                            </td>
                                                                            <td width="60%" colspan="3">
                                                                                <asp:CheckBoxList class="BoxDesign" ID="RecSetDays" runat="server" EnableViewState="true">
                                                                                </asp:CheckBoxList></td>
                                                                        </tr>
                                                                    </table>
                                                                </span><span id="SpanRecMonthly" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt39" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecMonthlyDays" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt40" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecMonthlyMonths" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </span><span id="SpanRecMonthlyDay" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt34" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecMonthlyDayPU" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecMonthlyDayDays" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt40" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecMonthlyDayMonths" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                        </tr>
                                                                    </table>
                                                                </span><span id="SpanRecYearly" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt39" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:TextBox class="BoxDesign" ID="RecYearDays" runat="server" EnableViewState="true"
                                                                                    Text="1"></asp:TextBox></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt50" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecYearMonths" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                    </table>
                                                                </span><span id="SpanRecYearlyDay" style="display: none">
                                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                                        border="0">
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt41" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecYearDayPU" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                &nbsp;
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecYearDayDays" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="60%">
                                                                                <twc:LocalizedLiteral Text="Meettxt40" runat="server" />
                                                                            </td>
                                                                            <td width="40%">
                                                                                <asp:DropDownList ID="RecYearDayMonths" runat="server" old="true" CssClass="BoxDesign">
                                                                                </asp:DropDownList></td>
                                                                        </tr>
                                                                    </table>
                                                                </span>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right" colspan="3">
                                                                <asp:LinkButton CssClass="Save" ID="Submit" runat="server" EnableViewState="true"></asp:LinkButton></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" colspan="3">
                                                                <asp:Literal ID="Info" runat="server"></asp:Literal></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </twc:TustenaTab>
                            </twc:TustenaTabber>
                            </td>
                            </tr>
    </table>

    </form>

</body>
</html>
