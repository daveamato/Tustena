<%@ Page Language="c#" Trace="false" Codebehind="Events.aspx.cs" Inherits="Digita.Tustena.Events"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/autodate.js"></script>

    <script type="text/javascript" src="/js/autodate.js"></script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/calendars.js"></script>

    <script>
function ShowRecurrence(){
	var disp;
	if (document.getElementById("CheckRecurrent").checked)
			disp="inline";
	else
			disp="none";
			document.getElementById("MenageTitle").style.display= disp;
			document.getElementById("TableRecurrence").style.display= disp;
			document.getElementById("SpanRecDaily").style.display= disp;
			document.getElementById("SpanRecWeekly").style.display= disp;
			document.getElementById("SpanRecMonthly").style.display= disp;
			document.getElementById("SpanRecMonthlyDay").style.display= disp;
			document.getElementById("SpanRecYearly").style.display= disp;
			document.getElementById("SpanRecYearlyDay").style.display= disp;
}

function ActivateRecMode(){
		document.getElementById("RecMode").disabled=false;
		ActivateForms();
}

function ActivateForms(){
			document.getElementById("SpanRecYearlyDay").style.display= "none";
			document.getElementById("SpanRecDaily").style.display= "none";
			document.getElementById("SpanRecWeekly").style.display= "none";
			document.getElementById("SpanRecMonthly").style.display= "none";
			document.getElementById("SpanRecMonthlyDay").style.display= "none";
			document.getElementById("SpanRecYearly").style.display= "none";
	switch (document.getElementById("RecMode").value){
		case "1":
			document.getElementById("SpanRecDaily").style.display= "inline";
			break;
		case "2":
			document.getElementById("SpanRecWeekly").style.display= "inline";
			break;
		case "3":
			document.getElementById("SpanRecMonthly").style.display= "inline";
			break;
		case "4":
			document.getElementById("SpanRecMonthlyDay").style.display= "inline";
			break;
		case "5":
			document.getElementById("SpanRecYearly").style.display= "inline";
			break;
		case "6":
			document.getElementById("SpanRecYearlyDay").style.display= "inline";
			break;
	}
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
                                        <twc:localizedliteral id="LocalizedLiteral1" text="Caltxt5" runat="server" />
                                    </div>
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
                                        <twc:localizedliteral id="LocalizedLiteral2" text="Options" runat="server" />
                                    </div>
                                    <a href="#" class="sideBtn" onclick="OpenTEvent('NEW','');">
                                        <twc:localizedliteral id="LocalizedLiteral3" text="Caltxt6" runat="server" />
                                    </a>
                                    <a href="#" class="sideBtn" onclick="OpenEventS('NEW','');">
                                        <twc:localizedliteral id="LocalizedLiteral4" text="Caltxt7" runat="server" />
                                    </a>
                                    <a href="#" class="sideBtn" onclick="OpenMeeting('');">
                                        <twc:localizedliteral id="LocalizedLiteral8" text="Caltxt45" runat="server" />
                                    </a>
                                    <asp:Literal ID="Office" runat="server" />
                                    <a href="#" class="sideBtn" onclick="location.href='agenda.aspx?m=25&si=2'">
                                        <twc:localizedliteral id="LocalizedLiteral6" text="Caltxt9" runat="server" />
                                    </a>
                                    <a href="#" class="sideBtn" onclick="location.href='agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2'">
                                        <twc:localizedliteral id="LocalizedLiteral7" text="Caltxt52" runat="server" />
                                    </a>
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
                        <twc:TustenaTab ID="AppointTab" LangHeader="Caltxt7" ClientSide="true" runat="server">

                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("Evetxt1")%>
                            </td>
                        </tr>
                    </table>
                    <table width="98%" align="center" runat="server" id="TEvento">
                        <tr>
                            <td>
                                <table id="AppointmentCard" runat="server" border="0" cellpadding="0" cellspacing="0"
                                    width="840" class="normal">
                                    <tr>
                                        <td width="50%" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Evetxt2")%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="NewId" Visible="false" runat="server" EnableViewState="true" />
                                                        <asp:DropDownList ID="UserApp" runat="server" CssClass="BoxDesign" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Evetxt3")%>
                                                        <asp:Label ID="DateEr1" runat="server" CssClass="normal" />
                                                        <domval:RequiredDomValidator ID="RequiredFieldValidatorData" runat="server" ControlToValidate="StartDate"
                                                            ErrorMessage="*" />
                                                        <domval:CompareDomValidator ID="cvF_Datainizio" runat="Server" Operator="DataTypeCheck"
                                                            Display="Dynamic" Type="Date" ErrorMessage="*" ControlToValidate="StartDate" />
                                                    </td>
                                                    <td>
                                                        <table width="100%" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="StartDate" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesignReq"
                                                                        EnableViewState="true" MaxLength="10" />
                                                                </td>
                                                                <td width="30">
                                                                    &nbsp;<img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=StartDate',event,195,195)">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Evetxt4")%>
                                                        <domval:RequiredDomValidator ID="RequiredFieldValidatorOra1" runat="server" ControlToValidate="StartHour"
                                                            ErrorMessage="*" />
                                                        <domval:RegexDomValidator ID="RegularExpressionValidatorOra1" runat="server" ControlToValidate="StartHour"
                                                            ErrorMessage="*" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" />
                                                    </td>
                                                    <td>
                                                        <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="StartHour" onkeypress="HourCheck(this,event)" MaxLength="5"
                                                                        runat="server" CssClass="BoxDesignReq" />
                                                                </td>
                                                                <td valign="middle" align="right" width="40">
                                                                    <img src="/images/up.gif" onclick="HourUp('StartHour')">
                                                                    <img src="/images/down.gif" onclick="HourDown('StartHour')">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Evetxt6")%>
                                                        <domval:RequiredDomValidator ID="RequiredFieldValidatorTitle" runat="server" ControlToValidate="AgTitle"
                                                            ErrorMessage="*" />
                                                    </td>
                                                    <td class="normal">
                                                        <asp:TextBox ID="AgTitle" runat="server" CssClass="BoxDesignReq"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="50%" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div class="divautoform">
                                                <%=wrm.GetString("Evetxt7")%>
                                                <domval:RequiredDomValidator ID="RequiredFieldValidatorNote" runat="server" ControlToValidate="Note"
                                                    ErrorMessage="*" />
                                            </div>
                                            <asp:TextBox ID="Note" Rows="5" TextMode="MultiLine" CssClass="BoxDesignReq" Height="100px"
                                                runat="server" EnableViewState="true" />
                                        </td>
                                    </tr>
                                    <tr id="HeaderRecurrence" runat="server">
                                        <td colspan="2">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td align="left" class="BorderBottomTitles">
                                                        <span class="divautoform"><b>
                                                            <%=wrm.GetString("Evetxt8")%>
                                                        </b></span>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="CorpoRecurrence" runat="server">
                                        <td valign="top">
                                            <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="60%">
                                                        <%=wrm.GetString("Evetxt9")%>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="CheckRecurrent" runat="server" onClick="ShowRecurrence();ActivateRecMode();" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <span id="TableRecurrence" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="40%">
                                                        </td>
                                                        <td width="60%">
                                                            <asp:DropDownList ID="RecType" runat="server" old="true" Visible="false" CssClass="BoxDesign"
                                                                onChange="ActivateRecMode();" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt11")%>
                                                            <domval:CompareDomValidator ID="CvRecDate" runat="Server" Operator="DataTypeCheck"
                                                                Type="Date" ErrorMessage="Data non valida" ControlToValidate="RecDateStart" />
                                                        </td>
                                                        <td width="60%">
                                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="RecDateStart" runat="server" class="BoxDesign" EnableViewState="true"
                                                                            MaxLength="10" />
                                                                    </td>
                                                                    <td width="30">
                                                                        &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecDateStart',event,195,195)">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt12")%>
                                                            <domval:CompareDomValidator ID="cvRecDataFine" runat="Server" Operator="DataTypeCheck"
                                                                Display="Dynamic" Type="Date" ErrorMessage="*" ControlToValidate="RecEndDate" />
                                                        </td>
                                                        <td width="60%">
                                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td>
                                                                        <asp:TextBox ID="RecEndDate" runat="server" class="BoxDesign" EnableViewState="true"
                                                                            MaxLength="10" />
                                                                    </td>
                                                                    <td width="30">
                                                                        &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecEndDate',event,195,195)">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt13")%>
                                                        </td>
                                                        <td width="60%">
                                                            <asp:DropDownList ID="RecMode" runat="server" old="true" CssClass="BoxDesign" onChange="ActivateForms();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </td>
                                        <td valign="top">
                                            <table id="MenageTitle" style="display: none" border="0" cellpadding="0" cellspacing="2"
                                                width="100%" class="normal" align="center">
                                                <tr>
                                                    <td align="left" class="BorderBottomTitles">
                                                        <b>
                                                            <%=wrm.GetString("Evetxt20")%>
                                                        </b>
                                                    </td>
                                                </tr>
                                            </table>
                                            <span id="SpanRecDaily" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt21")%>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="RecDayDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt22")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%" colspan="2">
                                                            <%=wrm.GetString("Evetxt23")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:CheckBox ID="RecWorkingDay" runat="server" EnableViewState="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span><span id="SpanRecWeekly" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt21")%>
                                                        </td>
                                                        <td width="20%">
                                                            <asp:TextBox ID="RecSettSS" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt24")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%" valign="top">
                                                            <%=wrm.GetString("Evetxt38")%>
                                                        </td>
                                                        <td width="60%" colspan="2">
                                                            <asp:CheckBoxList ID="RecSetDays" runat="server" EnableViewState="true" class="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span><span id="SpanRecMonthly" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt25")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:TextBox ID="RecMonthlyDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt26")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:TextBox ID="RecMonthlyMonths" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span><span id="SpanRecMonthlyDay" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt21")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecMonthlyDayPU" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            &nbsp;
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecMonthlyDayDays" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt26")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:TextBox ID="RecMonthlyDayMonths" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span><span id="SpanRecYearly" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt25")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:TextBox ID="RecYearDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                Text="1" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt35")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecYearMonths" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span><span id="SpanRecYearlyDay" style="display: none">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt21")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecYearDayPU" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            &nbsp;
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecYearDayDays" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="60%">
                                                            <%=wrm.GetString("Evetxt26")%>
                                                        </td>
                                                        <td width="40%">
                                                            <asp:DropDownList ID="RecYearDayMonths" runat="server" old="true" CssClass="BoxDesign" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="right">
                                            <asp:LinkButton ID="Submit" runat="server" class="Save" EnableViewState="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="left">
                                            <asp:Literal ID="Info" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <asp:Repeater ID="ViewAppointmentForm" runat="server" Visible="true" OnItemCommand="ItemCommandView"
                                    OnItemDataBound="ItemDataBoundView">
                                    <HeaderTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td width="50%" valign="TOP">
                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt2")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt3")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <asp:Literal ID="Date" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt4")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <asp:Literal ID="DateTo" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Evetxt6")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <%# DataBinder.Eval(Container.DataItem, "TITLE")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td width="50%" valign="TOP">
                                                <table id="tblStanza" border="0" cellpadding="0" cellspacing="2" width="100%" class="normal"
                                                    align="center">
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("CreBy")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <%# DataBinder.Eval(Container.DataItem, "createute")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("InsDate")%>
                                                        </td>
                                                        <td bgcolor="#FFFFFF">
                                                            <%# DataBinder.Eval(Container.DataItem, "CreatedDate","{0:d}")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <%=wrm.GetString("Evetxt7")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" bgcolor="#FFFFFF">
                                                <%# DataBinder.Eval(Container.DataItem, "note")%>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="right">
                                                <asp:Literal ID="NewId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                <asp:LinkButton ID="Submit" runat="server" CssClass="normal" CommandName="Modify" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
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
