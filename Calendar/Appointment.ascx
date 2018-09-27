<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Appointment.ascx.cs" Inherits="Digita.Tustena.AppointmentControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import Namespace="Digita.Tustena" %>

<table width="100%" border="0">
    <tr>
        <td align="left" class="BorderBottomTitles">
            <span class="divautoform"><b>
                <twc:LocalizedLiteral text="Evnttxt2" runat="server" id=LocalizedLiteral1 />
            </b></span>
        </td>
    </tr>
</table>
<table width="100%" align="center" runat="server" id="TEvento">
    <tr>
        <td>
            <table id="AppointmentCard" runat="server" border="0" cellpadding="0" cellspacing="0"
                width="100%" class="normal" align="center">
                <tr>
                    <td width="50%" valign="top">
                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                            <tr>
                                <td width="180">
                                    <twc:LocalizedLiteral text="Evnttxt3" runat="server" id=LocalizedLiteral2 />
                                    <input type="hidden" id="HiddenStartHour" runat="server" name="HiddenStartHour">
                                    <input type="hidden" id="HiddenEndHour" runat="server" name="HiddenEndHour">
                                </td>
                                <td width="173">
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="150">
                                                <asp:TextBox ID="NewId" Visible="false" runat="server" EnableViewState="true" />
                                                <asp:DropDownList ID="DDDLser" runat="server" class="BoxDesign" OnSelectedIndexChanged="UserApp_IndexChange"
                                                    AutoPostBack="true" />
                                            </td>
                                            <td width="22" align="right">
                                                <twc:LocalizedImg src="/i/free.gif" alt="Evnttxt42 runat="server" class="BtnIcon" onclick="AppointmentVerify(1,event,'{0}');" id=UserAppImg3 border="0" name="UserAppImg3"? />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap>
                                    <twc:LocalizedLiteral text="Evnttxt4" runat="server" id=LocalizedLiteral3 />
                                    <asp:Label ID="DateEr1" runat="server" class="normal" />
                                    <domval:requireddomvalidator id="RequiredFieldValidatorData" runat="server" controltovalidate="TxtStartDate"
                                        errormessage="*" />
                                    <domval:comparedomvalidator id="cvF_Datainizio" runat="Server" operator="DataTypeCheck"
                                        display="Dynamic" type="Date" errormessage="*" controltovalidate="TxtStartDate" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtStartDate" onkeypress="DataCheck(this,event)" runat="server"
                                                    class="BoxDesign" EnableViewState="true" MaxLength="10" />
                                            </td>
                                            <td width="20" align="right">
                                                <img src="/i/allday.gif" border="0" class="BtnIcon" onclick="AllDay('<%=this.ID%>');">
                                            </td>
                                            <td width="30">
                                                <img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_TxtStartDate&amp;ISO=Yes&amp;Start='+(getElement('<%=this.ID%>_TxtStartDate')).value,event,195,195)">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap>
                                    <twc:LocalizedLiteral text="Evnttxt41" runat="server" id=LocalizedLiteral4 />
                                    <domval:comparedomvalidator id="cvF_DataFine" runat="Server" operator="DataTypeCheck"
                                        display="Dynamic" type="Date" errormessage="*" controltovalidate="TxtEndDate" />
                                    <asp:Label ID="DateEr2" runat="server" class="normal" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtEndDate" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesign"
                                                    EnableViewState="true" MaxLength="10" />
                                            </td>
                                            <td width="30">
                                                <img src="/i/SmallCalendar.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_TxtEndDate&amp;ISO=Yes',event,195,195)">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <twc:LocalizedLiteral text="Evnttxt5" runat="server" id=LocalizedLiteral5 />
                                    <domval:requireddomvalidator id="RequiredFieldValidatorOra1" runat="server" controltovalidate="TxtStartHour"
                                        errormessage="*" />
                                    <domval:regexdomvalidator id="RegularExpressionValidatorOra1" runat="server" controltovalidate="TxtStartHour"
                                        errormessage="*" validationexpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtStartHour" onkeypress="HourCheck(this,event)" runat="server"
                                                    MaxLength="8" class="BoxDesign" />
                                            </td>
                                            <td valign="middle" align="right" width="40">
                                                <img src="/images/up.gif" onclick="HourUp('<%=this.ID%>_TxtStartHour')" >
                                                <img src="/images/down.gif" onclick="HourDown('<%=this.ID%>_TxtStartHour')" >
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <twc:LocalizedLiteral text="Evnttxt6" runat="server" id=LocalizedLiteral6 />
                                    <domval:requireddomvalidator id="RequiredFieldValidatorOra2" runat="server" controltovalidate="TxtEndHour"
                                        errormessage="*" />
                                    <domval:regexdomvalidator id="RegularExpressionValidatorOra2" runat="server" controltovalidate="TxtEndHour"
                                        errormessage="*" validationexpression="^((([0]?[1-9]|1[0-2])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:|\.)[0-5][0-9]((:|\.)[0-5][0-9])?))$" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtEndHour" onkeypress="HourCheck(this,event)" runat="server" MaxLength="8"
                                                    class="BoxDesign" />
                                            </td>
                                            <td valign="middle" align="right" width="40">
                                                <img src="/images/up.gif" onclick="HourUp('<%=this.ID%>_TxtEndHour')" >
                                                <img src="/images/down.gif" onclick="HourDown('<%=this.ID%>_TxtEndHour')" >
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap>
                                    <twc:LocalizedLiteral text="Evnttxt9" runat="server" id=LocalizedLiteral7 />
                                    <img src="/i/lens.gif" class="BtnIcon" onclick="ViewCompany('<%=this.ID%>')">
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtCompany" runat="server" class="BoxDesign" EnableViewState="true" />
                                                <asp:TextBox ID="TxtCompanyID" runat="server" Style="DISPLAY: none" />
                                                <asp:TextBox ID="TxtContactID" runat="server" Style="DISPLAY: none" />
                                            </td>
                                            <td width="30">
                                                <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopCompany.aspx?render=no&amp;textbox=<%=this.ID%>_TxtCompany&amp;textbox2=<%=this.ID%>_TxtCompanyID&amp;ind=<%=this.ID%>_Address&amp;cit=<%=this.ID%>_CITTA&amp;prov=<%=this.ID%>_Province&amp;cap=<%=this.ID%>_CAP&amp;tel=<%=this.ID%>_Phone&amp;current=' + getElement('<%=this.ID%>_TxtCompany').value,event,500,400)">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <twc:LocalizedLiteral text="Evnttxt8" runat="server" id=LocalizedLiteral8 />
                                    <img src="/i/lens.gif" class="BtnIcon" onclick="ViewContact('<%=this.ID%>')">
                                    <domval:requireddomvalidator id="RequiredFieldValidatorTitle" runat="server" controltovalidate="TxtContact"
                                        errormessage="*" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtContact" runat="server" class="BoxDesign" EnableViewState="true" />
                                            </td>
                                            <td width="30">
                                                <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/popcontacts.aspx?render=no&amp;textbox=<%=this.ID%>_TxtContact&amp;textbox2=<%=this.ID%>_TxtCompany&amp;textboxID=<%=this.ID%>_TxtContactID&amp;textboxCompanyID=<%=this.ID%>_TxtCompanyID&amp;companyID=' + getElement('<%=this.ID%>_TxtCompanyID').value,event,400,200)">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <twc:LocalizedLiteral text="Evnttxt55" runat="server" id=LocalizedLiteral9 />
                                    <asp:TextBox ID="TxtAccomplistID" runat="server" class="BoxDesign" EnableViewState="true"
                                        Style="DISPLAY: none" />
                                </td>
                                <td>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="TxtAccomplist" runat="server" class="BoxDesign" EnableViewState="true"
                                                    ReadOnly="true" />
                                            </td>
                                            <td width="60" nowrap>
                                                <img src="/i/user.gif" border="0" class="BtnIcon" onclick="CreateBox('/Common/PopAccount.aspx?render=no&amp;textbox=TxtAccomplist&amp;textbox2=<%=this.ID%>_TxtAccomplistID',event)">
                                                <twc:LocalizedImg src="/i/free.gif" alt="Evnttxt42" runat="server" name="UserAppImg" id="UserAppImg"
                                                    border="0" style="CURSOR: pointer">
                                                <twc:LocalizedImg src="/i/erase.gif" alt="Evnttxt63" runat="server" name="UserAppImg2"
                                                    id="UserAppImg2" border="0" style="CURSOR: pointer">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="100">
                                    <twc:LocalizedLiteral text="Evnttxt64" runat="server" id=LocalizedLiteral10 />
                                </td>
                                <td>
                                    <asp:TextBox ID="Phone" runat="server" class="BoxDesign" jumpret="F_note" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <img src="/images/q.gif" width="5"></td>
                    <td width="50%" valign="top" height="100%">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" height="100%" align="center">
                            <tr>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                        <tr>
                                            <td width="100">
                                                <twc:LocalizedLiteral text="Evnttxt10" runat="server" id=LocalizedLiteral11 />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckSite" runat="server" Checked="true" />
                                            </td>
                                            <td width="100">
                                                <twc:LocalizedLiteral text="Evnttxt65" runat="server" id=LocalizedLiteral12 />
                                            </td>
                                            <td>
                                                <asp:CheckBox ID="CheckReminder" runat="server" Checked="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="TblRoom" runat="server" border="0" cellpadding="0" cellspacing="2" width="100%"
                                        class="normal" align="center">
                                        <tr>
                                            <td width="100">
                                                <twc:LocalizedLiteral text="Evnttxt11" runat="server" id=LocalizedLiteral13 />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Room" runat="server" class="BoxDesign" noret="true" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="TblAddress" runat="server" border="0" cellpadding="0" cellspacing="2"
                                        width="100%" class="normal" align="center">
                                        <tr>
                                            <td width="100">
                                                <twc:LocalizedLiteral text="Evnttxt12" runat="server" id=LocalizedLiteral14 />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Address" runat="server" class="BoxDesign" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <twc:LocalizedLiteral text="Evnttxt13" runat="server" id=LocalizedLiteral15 />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="City" runat="server" class="BoxDesign" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <twc:LocalizedLiteral text="Evnttxt14" runat="server" id=LocalizedLiteral16 />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Province" runat="server" class="BoxDesign" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <twc:LocalizedLiteral text="Evnttxt15" runat="server" id=LocalizedLiteral17 />
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
                            <twc:LocalizedLiteral text="Evnttxt16" runat="server" id=LocalizedLiteral18 />
                        </div>
                        <asp:TextBox ID="F_note" Rows="7" TextMode="MultiLine" class="textareaautoform" runat="server"
                            EnableViewState="true" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:LinkButton ID="Submit" CssClass="save" runat="server" class="AlfabetoNormal"
                            OnClick="Submit_Click" EnableViewState="true" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
