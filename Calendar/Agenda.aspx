<%@ Page Language="c#" Trace="false" Codebehind="Agenda.aspx.cs" Inherits="Digita.Tustena.Agenda"
    EnableViewState="false" AutoEventWireup="false" %>

<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/tooltip.js"></script>

    <script type="text/javascript" src="/js/menudigita.js"></script>

    <script type="text/javascript" src="/js/calendars.js"></script>

    <script language="javascript" src="/js/dynabox.js"></script>

    <script>
function ValidImpersonate()
{
	var obj = document.getElementById("Impersonate");
	if(obj.value.length<=0)
		return false;
	else
		return true;
}
    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <div id="legend" style="position: absolute; left: 100px; top: 70px; border: 1px solid black;
            visibility: hidden;">
            <table width="450" class="BodyBGColor">
                <tr>
                    <td class="list Cncnf">
                        &nbsp;</td>
                    <td class="list">
                        <twc:LocalizedLiteral ID="LocalizedLiteral1" Text="Caltxt1" runat="server" /></td>
                </tr>
                <tr>
                    <td class="list Cfo">
                        &nbsp;</td>
                    <td class="list">
                        <twc:LocalizedLiteral ID="LocalizedLiteral2" Text="Caltxt2" runat="server" /></td>
                </tr>
                <tr>
                    <td class="list Capp">
                        &nbsp;</td>
                    <td class="list">
                        <twc:LocalizedLiteral ID="LocalizedLiteral3" Text="Caltxt3" runat="server" /></td>
                </tr>
                <tr>
                    <td class="list CRec">
                        &nbsp;</td>
                    <td class="list">
                        <%=Capitalize(wrm.GetString("Evnttxt18"))%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <twc:LocalizedLabel ID="LocalizedLabel1" Text="Caltxt4" runat="server" onclick="legend.style.visibility='hidden'"
                            CssClass="Save" Style="cursor: pointer;" /></td>
                </tr>
            </table>
        </div>
        <asp:Literal ID="Jscriptmenu" runat="server" />
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <div class="HideForPrint">
                        <table width="98%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral Text="Caltxt5" runat="server" /></div>
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
                                        <twc:LocalizedLiteral Text="Options" runat="server" /></div>
                                    <a href="#" class="sideBtn" onclick="OpenTEvent('NEW','');">
                                        <twc:LocalizedLiteral Text="Caltxt6" runat="server" /></a> <a href="#" class="sideBtn"
                                            onclick="OpenEventS('NEW','');">
                                            <twc:LocalizedLiteral Text="Caltxt7" runat="server" /></a> <a href="#" class="sideBtn"
                                                onclick="OpenMeeting('');">
                                                <twc:LocalizedLiteral Text="Caltxt45" runat="server" />
                                            </a>
                                    <asp:Literal ID="Office" runat="server" />
                                    <a href="#" class="sideBtn" onclick="document.getElementById('legend').style.visibility='visible'">
                                        <twc:LocalizedLiteral Text="Caltxt8" runat="server" /></a> <a href="#" class="sideBtn"
                                            onclick="location.href='agenda.aspx?m=25&si=2'">
                                            <twc:LocalizedLiteral Text="Caltxt9" runat="server" /></a> <a href="#" class="sideBtn"
                                                onclick="location.href='agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2'">
                                                <twc:LocalizedLiteral Text="Caltxt52" runat="server" /></a> <a href="#" class="sidebtn"
                                                    onclick="ExportVcal('month',10);">
                                                    <twc:LocalizedLiteral Text="Caltxt11" runat="server" /></a>
                                    <asp:LinkButton ID="Back" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr id="visimpute" runat="server">
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral Text="Caltxt12" runat="server" /></div>
                                    <table width="100%" cellspacing="0" cellpadding="0" id="ImpersonateUser" runat="server">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="ImpersonateID" runat="server" Width="100%" Style="display: none"
                                                    onchange="alert('x')"></asp:TextBox>
                                                <asp:TextBox ID="Impersonate" runat="server" Width="100%" CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
                                            </td>
                                            <td width="22">
                                                &nbsp;<img id="ImpButtonImg" src="/i/user.gif" border="0" style="cursor: pointer"
                                                    onclick="CreateBox('/common/PopAccount.aspx?render=no&Impersonate=1&textbox=Impersonate&textbox2=ImpersonateID&click=ImpButtonOn',event)">
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton Style="display: none" ID="ImpButtonOn" runat="server" />
                                    <asp:LinkButton ID="ImpButton" runat="server" CssClass="sideBtn sideEvidence" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr id="visoffices" runat="server">
                                <td align="left" class="BorderBottomTitles">
                                    <span class="divautoform"><b>
                                        <twc:LocalizedLiteral Text="Caltxt15" runat="server" /></b></span>
                                </td>
                            </tr>
                            <tr id="visoffices1" runat="server">
                                <td align="right">
                                    <asp:DropDownList ID="ImpersonateOffice" runat="server" CssClass="BoxDesign" />
                                    <asp:LinkButton ID="OfficeOn" runat="server" CssClass="normal linked" />
                                </td>
                            </tr>
                            <tr id="visoffices2" runat="server">
                                <td>
                                    <asp:LinkButton ID="ImpOfficebutton" runat="server" CssClass="normal linked" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Calendar Width="135" Height="135" ID="calDate" runat="server" Font-Names="verdana"
                                        Font-Size="10px" Visible="true" OnSelectionChanged="Change_Date" OnVisibleMonthChanged="Change_Month"
                                        OnDayRender="DayRender" DayNameFormat="FirstLetter" TodayDayStyle-BackColor="gold"
                                        DayHeaderStyle-BackColor="lightsteelblue" OtherMonthDayStyle-ForeColor="lightgray"
                                        NextPrevStyle-ForeColor="white" TitleStyle-BackColor="gray" TitleStyle-ForeColor="white"
                                        TitleStyle-Font-Bold="True" TitleStyle-Font-Size="10px" SelectedDayStyle-BackColor="Navy"
                                        SelectedDayStyle-Font-Bold="True" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <div id="CalendarioMensile" runat="server">
                        <table border="0" cellpadding="2" cellspacing="0" width="98%" align="center">
                            <tr>
                                <td valign="bottom" width="33%">
                                    <asp:Literal ID="Previous" runat="server" />
                                </td>
                                <td valign="bottom" width="34%" align="center">
                                    <b>
                                        <asp:Label ID="Current" runat="server" CssClass="HeaderG" />
                                        <asp:TextBox ID="CurrentMonth" runat="server" Style="display: none;" />
                                        <asp:TextBox ID="CurrentYear" runat="server" Style="display: none;" />
                                    </b>
                                </td>
                                <td valign="bottom" width="33%" align="right">
                                    <asp:Literal ID="Next" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table class="tblstruct" runat="server" id="TCalendar">
                            <tr>
                                <td width="100%">
                                    <table border="0" cellpadding="2" cellspacing="1" align="center" width="100%">
                                        <tr>
                                            <asp:Literal ID="DaysTitle" runat="server" />
                                        </tr>
                                        <asp:Literal ID="Days" runat="server" />
                                    </table>
                                </td>
                            </tr>
                        </table>
                        &nbsp;&nbsp;<span id="TCalendarBtn" onclick="javascript:Hide('TCalendar')" onmouseover="dtt('<%=wrm.GetString("Caltxt46")%>');"
                            onmouseout="dtt();"><img src="/images/up.gif"></span>
                        <br>
                    </div>
                    <asp:Literal ID="Week" runat="server" />
                    <span id="HeaderCalOfficeso" runat="server">
                        <table width="98%" border="0" cellspacing="0" align="center">
                            <tr>
                                <td align="left" class="BorderBottomTitles" colspan="2">
                                    <span class="divautoform"><b>
                                        <twc:LocalizedLiteral Text="Caltxt25" runat="server" />
                                        <asp:Label ID="TitleOfficeso" runat="server" /></b></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="WeekDate" runat="server" Style="display: none" />
                                    <asp:LinkButton ID="CalOfficePrevWeek" runat="server" CssClass="normal" />
                                </td>
                                <td align="right">
                                    <asp:LinkButton ID="CalOfficeNextWeek" runat="server" CssClass="normal" />
                                </td>
                            </tr>
                        </table>
                        <br>
                    </span>
                    <asp:Literal ID="CalOfficeso" runat="server" />
                    <asp:Panel ID="DailyPanel" runat="server" Visible="false">
                        <table border="0" width="98%" cellspacing="0" align="center">
                            <tr>
                                <td width="90%">
                                    <asp:Literal ID="AgTitle" runat="server" />
                                </td>
                                <td align="right">
                                    <div class="HideForPrint" runat="server">
                                        <img src="/i/calendar.gif" style="cursor: hand;" onclick="location.href='agenda.aspx?m=25&si=2'">&nbsp;
                                        <img id="PrintDay" runat="server" src="/i/printer.gif" style="cursor: hand;">&nbsp;
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table width="98%" align="center" cellspacing="0" cellpadding="2">
                            <tr>
                                <td class="GridTitle" width="5%">
                                    <twc:LocalizedLiteral Text="Caltxt26" runat="server" /></td>
                                <td class="GridTitle" width="50%">
                                    <twc:LocalizedLiteral Text="Caltxt27" runat="server" /></td>
                                <td class="GridTitle" width="45%">
                                    <twc:LocalizedLiteral Text="Caltxt28" runat="server" /></td>
                            </tr>
                            <asp:Literal ID="Detail" runat="server" />
                        </table>
                        <div class="HideForPrint" runat="server">
                            <table border="0" cellpadding="2" cellspacing="0" width="98%" align="center">
                                <tr align="center">
                                    <td align="right">
                                        <img src="/i/calendar.gif" style="cursor: hand;" onclick="location.href='agenda.aspx?m=25&si=2'">&nbsp;
                                        <img id="PrintDay2" runat="server" src="/i/printer.gif" style="cursor: hand;">&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Literal ID="SomeJS" runat="server" />
    </form>
</body>
</html>
