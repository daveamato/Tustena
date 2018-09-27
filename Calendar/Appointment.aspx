<%@ Page Language="c#" Trace="false" Codebehind="Appointment.aspx.cs" Inherits="Digita.Tustena.Appointment"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />

    <script type="text/javascript" src="/js/autodate.js"></script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/calendars.js"></script>

    <twc:LocalizedScript resource="Evnttxt67,Evnttxt66,Evnttxt60,Evnttxt62" runat="server" />

    <script>var appvrfy = Evnttxt67;var appvrfy2 = Evnttxt66;var confdate = Evnttxt60;var autofill = Evnttxt62</script>

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
                                        <twc:LocalizedLiteral ID="LocalizedLiteral3" Text="Caltxt6" runat="server" /></a> <a href="#" class="sideBtn"
                                            onclick="OpenEventS('NEW','');">
                                            <twc:LocalizedLiteral ID="LocalizedLiteral4" Text="Caltxt7" runat="server" /></a>
                                            <a href="#" class="sideBtn"
                                                    onclick="OpenMeeting('');">
                                                    <twc:LocalizedLiteral ID="LocalizedLiteral8" Text="Caltxt45" runat="server" /></a>
                                    <asp:Literal ID="Office" runat="server" />
                                     <a href="#" class="sideBtn"
                                            onclick="location.href='agenda.aspx?m=25&si=2'">
                                            <twc:LocalizedLiteral ID="LocalizedLiteral6" Text="Caltxt9" runat="server" /></a> <a href="#" class="sideBtn"
                                                onclick="location.href='agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2'">
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
                        <twc:TustenaTab ID="AppointTab" LangHeader="Evnttxt2" ClientSide="true" runat="server">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" class="pageTitle" valign="top">
                                        <%=wrm.GetString("Evnttxt2")%>
                                    </td>
                                </tr>
                            </table>
                            <table width="98%" align="center" runat="server" id="TEvento">
                                <tr>
                                    <td>
                                        <table id="AppointmentCard" runat="server" border="0" cellpadding="0" cellspacing="0"
                                            width="840" class="normal">
                                            <tr>
                                                <td width="50%" valign="TOP">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="180">
                                                                <%=wrm.GetString("Evnttxt3")%>
                                                                <input type="hidden" id="HiddenStartHour" runat="server">
                                                                <input type="hidden" id="HiddenEndHour" runat="server">
                                                            </td>
                                                            <td width="173">
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td width="150">
                                                                            <asp:TextBox ID="NewId" Visible="false" runat="server" EnableViewState="true" />
                                                                            <asp:DropDownList ID="UserApp" runat="server" class="BoxDesign" OnSelectedIndexChanged="UserApp_IndexChange"
                                                                                AutoPostBack="true" />
                                                                        </td>
                                                                        <td width="22" align="right">
                                                                            <img src="/i/free.gif" alt="<%=wrm.GetString("Evnttxt42")%>" name="UserAppImg" id="UserAppImg"
                                                                                border="0" class="BtnIcon" onclick="AppointmentVerify(1,event);">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <%=wrm.GetString("Evnttxt5")%>
                                                                <asp:Label ID="DateEr1" runat="server" class="normal" />
                                                                <domval:RequiredDomValidator ID="RequiredFieldValidatorData" runat="server" ControlToValidate="F_StartDate"
                                                                    ErrorMessage="*" />
                                                                <domval:CompareDomValidator ID="CVF_StartDate" runat="Server" Operator="DataTypeCheck"
                                                                    Display="Dynamic" Type="Date" ErrorMessage="*" ControlToValidate="F_StartDate" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_StartDate" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesignReq"
                                                                                EnableViewState="true" MaxLength="10" />
                                                                        </td>
                                                                        <td width="30">
                                                                            <img id="calsd" src="/i/SmallCalendar.gif" border="0" class="BtnIcon" xxxonclick="calendaring(this,'F_StartDate')"
                                                                                onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=F_StartDate',event,195,195)">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <%=wrm.GetString("Evnttxt6")%>
                                                                <domval:CompareDomValidator ID="CVF_EndDate" runat="Server" Operator="DataTypeCheck"
                                                                    Display="Dynamic" Type="Date" ErrorMessage="*" ControlToValidate="F_EndDate" />
                                                                <asp:Label ID="DateEr2" runat="server" class="normal" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_EndDate" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesign"
                                                                                EnableViewState="true" MaxLength="10" />
                                                                        </td>
                                                                        <td width="30">
                                                                            <img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=F_EndDate&ISO=Yes',event,195,195)">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <%=wrm.GetString("Evnttxt76")%>
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="CkAllDay" runat="server" Checked="False" onclick="AllDay();" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%=wrm.GetString("Evnttxt5")%>
                                                                <domval:RequiredDomValidator ID="RequiredFieldValidatorOra1" runat="server" ControlToValidate="F_StartHour"
                                                                    ErrorMessage="*" />
                                                                <domval:RegexDomValidator ID="RegularExpressionValidatorOra1" runat="server" ControlToValidate="F_StartHour"
                                                                    ErrorMessage="*" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_StartHour" onkeypress="HourCheck(this,event)"   runat="server" MaxLength="8" class="BoxDesignReq" />
                                                                        </td>
                                                                        <td valign="middle" align="left" width="44">
                                                                            <img src="/images/up.gif" class="BtnIcon" onclick="HourUp('F_StartHour')" />
                                                                            <img src="/images/down.gif" style="cursor: pointer" onclick="HourDown('F_StartHour')" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%=wrm.GetString("Evnttxt6")%>
                                                                <domval:RequiredDomValidator ID="RequiredFieldValidatorOra2" runat="server" ControlToValidate="F_EndHour"
                                                                    ErrorMessage="*" />
                                                                <domval:RegexDomValidator ID="RegularExpressionValidatorOra2" runat="server" ControlToValidate="F_EndHour"
                                                                    ErrorMessage="*" ValidationExpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_EndHour" onkeypress="HourCheck(this,event)"  runat="server" MaxLength="8"
                                                                                class="BoxDesignReq" />
                                                                        </td>
                                                                        <td valign="middle" align="left" width="44">
                                                                            <img src="/images/up.gif" class="BtnIcon" onclick="HourUp('F_EndHour')" />
                                                                            <img src="/images/down.gif" style="cursor: pointer" onclick="HourDown('F_EndHour')" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap>
                                                                <%=wrm.GetString("Evnttxt9")%>
                                                                <img src="/i/lens.gif" class="BtnIcon" onclick="ViewCompany('',event)">
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_Title2" runat="server" class="BoxDesign" EnableViewState="true" />
                                                                            <asp:TextBox ID="CompanyId" runat="server" Style="display: none;" />
                                                                            <asp:TextBox ID="F_ContactID" runat="server" Style="display: none;" />
                                                                        </td>
                                                                        <td width="30">
                                                                            <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=F_Title2&textbox2=CompanyId&ind=Address&cit=City&prov=Province&cap=CAP&tel=Phone&current=' + getElement('F_Title2').value,event,500,400)">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%=wrm.GetString("Evnttxt8")%>
                                                                <img src="/i/lens.gif" class="BtnIcon" onclick="ViewContact('',event)">
                                                                <domval:RequiredDomValidator ID="RequiredFieldValidatorTitle" runat="server" ControlToValidate="F_Title"
                                                                    ErrorMessage="*" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_Title" runat="server" class="BoxDesignReq" EnableViewState="true" />
                                                                        </td>
                                                                        <td width="30">
                                                                            <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/popcontacts.aspx?render=no&textbox=F_Title&textbox2=F_Title2&textboxID=F_ContactID&textboxCompanyID=CompanyId&Mode=1&companyID=' + getElement('CompanyId').value,event,400,320)">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%=wrm.GetString("Evnttxt55")%>
                                                                <asp:TextBox ID="IdCompanion" runat="server" class="BoxDesign" EnableViewState="true"
                                                                    Style="display: none;" />
                                                            </td>
                                                            <td>
                                                                <table width="100%" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="Companion" runat="server" class="BoxDesign" EnableViewState="true"
                                                                                ReadOnly="true" />
                                                                        </td>
                                                                        <td width="60" nowrap>
                                                                            <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopAccount.aspx?render=no&textbox=Companion&textbox2=IdCompanion',event)">
                                                                            <img src="/i/free.gif" alt="<%=wrm.GetString("Evnttxt42")%>" name="UserAppImg" id="UserAppImg"
                                                                                border="0" style="cursor: pointer" onclick="AppointmentVerify(2,event);">
                                                                            <img src="/i/erase.gif" alt="<%=wrm.GetString("Evnttxt63")%>" name="UserAppImg" id="UserAppImg"
                                                                                border="0" style="cursor: pointer" onclick="RemoveCompanion();">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="100">
                                                                <%=wrm.GetString("Evnttxt64")%>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Phone" runat="server" class="BoxDesign" jumpret="F_note" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <img src="/images/q.gif" width="100">
                                                </td>
                                                <td width="50%" valign="TOP" height="100%">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" align="center">
                                                        <tr>
                                                            <td>
                                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                                    <tr>
                                                                        <td width="100">
                                                                            <%=wrm.GetString("Evnttxt10")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="CheckSite" runat="server" Checked="true" onclick="ShowRoom();" />
                                                                        </td>
                                                                        <td width="100">
                                                                            <%=wrm.GetString("Evnttxt65")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="CheckReminder" runat="server" Checked="false" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table id="TblRoom" border="0" cellpadding="0" cellspacing="2" width="100%" class="normal"
                                                                    align="center">
                                                                    <tr>
                                                                        <td width="100">
                                                                            <%=wrm.GetString("Evnttxt11")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Room" runat="server" class="BoxDesign" noret="true" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <table id="TblAddress" border="0" cellpadding="0" cellspacing="2" width="100%" class="normal"
                                                                    align="center">
                                                                    <tr>
                                                                        <td width="100">
                                                                            <%=wrm.GetString("Evnttxt12")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Address" runat="server" class="BoxDesign" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%=wrm.GetString("Evnttxt13")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="City" runat="server" class="BoxDesign" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%=wrm.GetString("Evnttxt14")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="Province" runat="server" class="BoxDesign" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <%=wrm.GetString("Evnttxt15")%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="CAP" runat="server" class="BoxDesign" noret="true" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="bottom" height="100%">
                                                                <asp:Label ID="LbFlagNotify" runat="server" Visible="false" class="normal" />
                                                                <asp:CheckBox ID="CheckNotify" runat="server" Checked="false" Visible="false" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="bottom" height="100%">
                                                                <asp:Literal ID="Info" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div class="divautoform">
                                                        <%=wrm.GetString("Evnttxt16")%>
                                                    </div>
                                                    <asp:TextBox ID="F_note" Rows="5" TextMode="MultiLine" class="textareaautoform" runat="server"
                                                        EnableViewState="true" />
                                                </td>
                                            </tr>
                                            <tr id="HeaderReminder" style="display: none">
                                                <td colspan="3">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td align="left" class="BorderBottomTitles">
                                                                <span class="divautoform"><b>REMINDER</b></span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="BodyReminder" style="display: none">
                                                <td colspan="3">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td width="50%" valign="top">
                                                                <div class="divautoform">
                                                                    <%=wrm.GetString("Acttxt79").ToUpper()%>
                                                                </div>
                                                                <asp:DropDownList ID="DropDownListPreAlarm" runat="server" Width="100%" CssClass="BoxDesign">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td width="50%" valign="top">
                                                                <div class="divautoform">
                                                                    <%=wrm.GetString("Evnttxt59")%>
                                                                </div>
                                                                <asp:TextBox ID="Reminder_RemNote" runat="server" TextMode="MultiLine" Height="50"
                                                                    class="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="HeaderRecurrence" runat="server">
                                                <td colspan="3">
                                                    <table width="100%" border="0">
                                                        <tr>
                                                            <td align="left" class="BorderBottomTitles">
                                                                <span class="divautoform"><b>
                                                                    <%=wrm.GetString("Evnttxt17")%>
                                                                </b></span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="CorpoRecurrence" runat="server">
                                                <td valign="top" colspan="2">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="10%">
                                                                <%=wrm.GetString("Evnttxt18")%>
                                                            </td>
                                                            <td align="left">
                                                                <asp:CheckBox ID="CheckRecurrent" runat="server" onclick="ShowRecurrence();ActivateRecMode();" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <span id="TableRecurrent" style="display: none;">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="50%" class="normal" align="center">
                                                            <tr>
                                                                <td width="10%">
                                                                </td>
                                                                <td width="90%">
                                                                    <asp:DropDownList ID="RecType" old="true" Visible="false" runat="server" CssClass="BoxDesign"
                                                                        onchange="ActivateRecMode();" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                    <%=wrm.GetString("Evnttxt20")%>
                                                                    <domval:CompareDomValidator ID="CvRecDatainizio" runat="Server" Operator="DataTypeCheck"
                                                                        Type="Date" ErrorMessage="Data non valida" ControlToValidate="RecDateStart" />
                                                                </td>
                                                                <td width="90%">
                                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="RecDateStart" runat="server" class="BoxDesign" onkeypress="DataCheck(this,event)"
                                                                                    EnableViewState="true" MaxLength="10" />
                                                                            </td>
                                                                            <td width="30">
                                                                                <img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecDateStart',event,195,195)">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                    <%=wrm.GetString("Evnttxt21")%>
                                                                    <domval:CompareDomValidator ID="cvRecDataFine" runat="Server" Operator="DataTypeCheck"
                                                                        onkeypress="DataCheck(this,event)" Display="Dynamic" Type="Date" ErrorMessage="*"
                                                                        ControlToValidate="RecEndDate" />
                                                                </td>
                                                                <td width="90%">
                                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:TextBox ID="RecEndDate" runat="server" class="BoxDesign" EnableViewState="true"
                                                                                    MaxLength="10" />
                                                                            </td>
                                                                            <td width="30">
                                                                                <img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecEndDate',event,195,195)">
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt22")%>
                                                                </td>
                                                                <td width="60%">
                                                                    <asp:DropDownList ID="RecMode" old="true" runat="server" CssClass="BoxDesign" onchange="ActivateForms();" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span>
                                                </td>
                                                <td valign="top" colspan="1" id="RecTitle" width="100%" style="display: none;">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td align="left" class="BorderBottomTitles">
                                                                <b>
                                                                    <%=wrm.GetString("Evnttxt29")%>
                                                                </b>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <span id="SpanRecDaily" style="display: none;">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt30")%>
                                                                </td>
                                                                <td width="20%">
                                                                    <asp:TextBox ID="RecDayDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt31")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="60%" colspan="1">
                                                                    <%=wrm.GetString("Evnttxt32")%>
                                                                </td>
                                                                <td width="40%" align="left">
                                                                    <asp:CheckBox ID="RecWorkingDay" runat="server" EnableViewState="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span><span id="SpanRecWeekly" style="display: none;">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt33")%>
                                                                </td>
                                                                <td width="20%">
                                                                    <asp:TextBox ID="RecSettSS" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt34")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%" valign="top">
                                                                    <%=wrm.GetString("Evnttxt35")%>
                                                                </td>
                                                                <td width="60%" colspan="2">
                                                                    <asp:CheckBoxList ID="RecSetDays" runat="server" EnableViewState="true" class="BoxDesign" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span><span id="SpanRecMonthly" style="display: none;">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td width="60%">
                                                                    <%=wrm.GetString("Evnttxt43")%>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:TextBox ID="RecMonthlyDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="60%">
                                                                    <%=wrm.GetString("Evnttxt44")%>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:TextBox ID="RecMonthlyMonths" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span><span id="SpanRecMonthDay" style="display: none">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td width="60%">
                                                                    <%=wrm.GetString("Evnttxt45")%>
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
                                                                    <%=wrm.GetString("Evnttxt44")%>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:TextBox ID="RecMonthlyDayMonths" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span><span id="SpanRecYearly" style="display: none;">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td width="60%">
                                                                    <%=wrm.GetString("Evnttxt43")%>
                                                                </td>
                                                                <td width="40%">
                                                                    <asp:TextBox ID="RecYearDays" runat="server" class="BoxDesign" EnableViewState="true"
                                                                        Text="1" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="60%">
                                                                    <%=wrm.GetString("Evnttxt44")%>
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
                                                                    <%=wrm.GetString("Evnttxt33")%>
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
                                                                    <%=wrm.GetString("Evnttxt44")%>
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
                                                <td colspan="3" align="right">
                                                    <asp:LinkButton ID="Submit" CssClass="save" runat="server" class="AlfabetoNormal"
                                                        EnableViewState="true" />
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:Repeater ID="ViewAppointmentForm" runat="server" Visible="true">
                                            <HeaderTemplate>
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="50%" valign="TOP">
                                                        <table border="0" cellpadding="2" cellspacing="5" width="100%" class="normal" align="center">
                                                            <tr>
                                                                <td>
                                                                    <asp:textbox id="RecDateStart" runat="server" CssClass="BoxDesign" onkeypress="DataCheck(this,event)"
                                                                        enableviewstate="true" maxlength="10" />
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "UserName")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt4")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <asp:Literal ID="Date" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt5")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <asp:Literal ID="DateFrom" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt6")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <asp:Literal ID="DateTo" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt8")%>
                                                                    <img src="/i/lens.gif" class="BtnIcon" onclick="CreateBox('/Common/ViewContact.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "contactid")%>',event,500,250);">
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "contact")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt9")%>
                                                                    <img src="/i/lens.gif" class="BtnIcon" onclick="CreateBox('/Common/ViewCompany.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "companyid")%>',event,500,300);">
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "company")%>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt64")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "phone")%>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td width="50%" valign="TOP">
                                                        <table id="tblStanza" border="0" cellpadding="2" cellspacing="5" width="100%" class="normal"
                                                            align="center">
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt11")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "room")%>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt12")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "address")%>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt13")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "city")%>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt14")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "province")%>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="40%">
                                                                    <%=wrm.GetString("Evnttxt15")%>
                                                                </td>
                                                                <td bgcolor="#FFFFFF">
                                                                    <%# DataBinder.Eval(Container.DataItem, "cap")%>
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <%=wrm.GetString("Evnttxt16")%>
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
                                                        <span onclick="NewWindow('vCal.aspx?render=no&mode=single&id=<%# DataBinder.Eval(Container.DataItem, "id")%>', '', 400,200,'no')"
                                                            class="AlfabetoNormal">
                                                            <%=wrm.GetString("Evnttxt37")%>
                                                        </span>&nbsp;&nbsp;
                                                        <asp:Literal ID="NewId" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                        <asp:LinkButton ID="Submit" CssClass="save" runat="server" class="AlfabetoNormal"
                                                            CommandName="Modify" />
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
        <asp:Label ID="SomeJS" runat="server" enabledviewstate="false" />
    </form>
</body>
</html>
