<%@ Page Language="c#" AutoEventWireup="false" Trace="false" Codebehind="NewUser.aspx.cs"
    Inherits="Digita.Tustena.NewUser" %>

<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Register TagPrefix="TOffice" TagName="SelectOffice" Src="~/Common/SelectOffice.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">

    <script type="text/javascript" src="/js/autodate.js"></script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/SelectOffice.js"></script>

    <script type="text/javascript" src="/js/minmax.js"></script>

    <script>


function SelABCHeader(letter){
        location.href="NewUser.aspx?m=7&dgb=1&si=8&list="+letter;
}

function CheckPop(e){
		var ad = document.getElementById("MailUser");
		var pa = document.getElementById("MailPassword");
		var po = document.getElementById("MailServer").value;

		if(document.getElementById("SecureMail_1").checked)
			po="!"+po;
		CreateBox("/mailinglist/webmail/checkpop3.aspx?render=no&pop3=" + po + "&addr=" + ad.value + "&pass=" + pa.value,e,200,100);
	}

	function EraseCache(e){
		CreateBox("/mailinglist/webmail/erasecache.aspx?render=no",e,200,100);
	}

	function EnableZones(){
		var tbl = document.getElementById("tblZones");
		var utype = document.getElementById("FlagCommercial_0");
		var utypecomm = document.getElementById("FlagCommercial_2");
		var spnManager = document.getElementById("spnManager");
		if(utype.checked)
			DisableAll(tbl, false);
		else
			DisableAll(tbl, true);
		if(utypecomm.checked)
			spnManager.style.display='';
		else
			spnManager.style.display='none';
	}
    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top" id="tblAdminUser" runat="server">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <asp:Literal ID="TitlePage" runat="server" /></div>
                                <asp:LinkButton ID="LblNewUser" CssClass="sidebtn" runat="server" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:LocalizedLiteral Text="Find" runat="server" /></div>
                                <div class="sideInput">
                                    <asp:TextBox ID="Find" runat="server" Height="20" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <twc:LocalizedLiteral ID="LocalizedLiteral1" runat="server" Text="CRMcontxt19" /></div>
                                <div class="sideInput">
                                    <asp:DropDownList cssclass="BoxDesign" ID="ListGroups" runat="server">
                                    </asp:DropDownList></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnFind" OnClick="BtnFindClick" runat="server" CssClass="save" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <asp:Label ID="TitlePageRight" runat="server"></asp:Label></td>
                            <td class="BorderBottomTitles" align="right">
                                <asp:Literal ID="LtrNewUser" runat="server"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td class="normal" align="left" colspan="2">
                                <asp:Literal ID="HelpLabel" runat="server"></asp:Literal></td>
                        </tr>
                    </table>
                    <asp:Label ID="LblHeader" runat="server" EnableViewState="False"></asp:Label>
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td valign="top" align="center">
                                <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1Command" OnItemDataBound="Repeater1DataBound">
                                    <HeaderTemplate>
                                        <table border="0" cellpadding="3" cellspacing="0" width="100%" align="center">
                                            <tr>
                                                <td class="GridTitle">
                                                    <twc:LocalizedLiteral Text="Usrtxt12" runat="server" /></td>
                                                <td class="GridTitle">
                                                    <twc:LocalizedLiteral Text="Usrtxt13" runat="server" /></td>
                                                <td class="GridTitle">
                                                    <twc:LocalizedLiteral Text="Usrtxt14" runat="server" /></td>
                                                <td class="GridTitle">
                                                    &nbsp;</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="GridItem">
                                                <asp:Label ID="td1" runat="server" /></td>
                                            <td class="GridItem">
                                                <asp:Label ID="td2" runat="server" /></td>
                                            <td class="GridItem">
                                                <%# DataBinder.Eval(Container.DataItem, "OfficesoN")%>
                                                &nbsp;</td>
                                            <td class="GridItem" align="center">
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" class="normal" />
                                                <asp:Literal ID="IDUID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "UID")%>' />
                                                <asp:Literal ID="UIDActive" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td class="GridItemAltern">
                                                <asp:Label ID="td1" runat="server" /></td>
                                            <td class="GridItemAltern">
                                                <asp:Label ID="td2" runat="server" /></td>
                                            <td class="GridItemAltern">
                                                <%# DataBinder.Eval(Container.DataItem, "OfficesoN")%>
                                                &nbsp;</td>
                                            <td class="GridItemAltern" align="center">
                                                <asp:LinkButton ID="Delete" runat="server" CommandName="Delete" class="normal" />
                                                <asp:Literal ID="IDUID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "UID")%>' />
                                                <asp:Literal ID="UIDActive" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Active")%>' />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Repeater1Info" runat="server" ForeColor="Red" CssClass="normal"></asp:Label></td>
                        </tr>
                        <tr>
                            <td valign="top">

                                <asp:Label ID="LabelInfo" runat="server" ForeColor="Red" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                                <twc:TustenaTabber ID="Tabber" Width="840" runat="server">
                                    <twc:TustenaTabberRight runat="server" id="TustenaTabberRight1">
                                        <asp:LinkButton ID="Submit2" OnClick="Submit_Click" runat="server" CssClass="Save"></asp:LinkButton>
                                    </twc:TustenaTabberRight>
                                    <twc:TustenaTab ID="visInfoUser" LangHeader="Usrtxt16" runat="server">
                                        <domval:DomValidationSummary ID="valSum" ShowMessageBox="true" DisplayMode="BulletList"
                                            EnableClientScript="true" runat="server" />
                                        <table class="normal" width="100%" align="center">
                                            <tr>
                                                <td>
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt16" runat="server" ID="LocalizedLiteral2" /></b>
                                                </td>
                                                <td align="right">
                                                    &nbsp;
                                                    <asp:LinkButton ID="Rubric2" runat="server" CssClass="ContactHeader" Visible="false" />
                                                    <asp:LinkButton ID="ExistRubric2" runat="server" CssClass="ContactHeader" Visible="false" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoformRequired">
                                                                    <twc:LocalizedLiteral Text="Usrtxt21" runat="server" ID="LocalizedLiteral3" />
                                                                    <domval:RegexDomValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="(^\s*)([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*)$"
                                                                        ControlToValidate="F_UserName" Display="Static">*</domval:RegexDomValidator>
                                                                    <domval:RequiredDomValidator ID="F_UserNameValidator" Display="Static" ControlToValidate="F_UserName"
                                                                        runat="server">*</domval:RequiredDomValidator>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="F_UserName" runat="server" CssClass="BoxDesignReq" />
                                                                <asp:TextBox ID="F_UID" runat="server" Visible=false />
                                                                <asp:TextBox ID="Mode" runat="server" Visible=false />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoformRequired">
                                                                    <twc:LocalizedLiteral Text="Usrtxt22" runat="server" ID="LocalizedLiteral4" />
                                                                </span>
                                                                <domval:RequiredDomValidator ID="F_PasswordValidator" Display="Static" ControlToValidate="F_Password"
                                                                    runat="server">*</domval:RequiredDomValidator>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="F_Password" runat="server" CssClass="BoxDesignReq" TextMode="Password" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt23" runat="server" ID="LocalizedLiteral5" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Name" runat="server" class="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt24" runat="server" ID="LocalizedLiteral6" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Surname" runat="server" class="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt74" runat="server" ID="LocalizedLiteral7" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="EMail" runat="server" class="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none">
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt25" runat="server" ID="LocalizedLiteral8" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="PersonalContact" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt56" runat="server" ID="LocalizedLiteral9" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="FlagModifyAppointment" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt87" runat="server" ID="LocalizedLiteral24" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="FlagCreateContact" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td width="50%" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoformRequired">
                                                                    <twc:LocalizedLiteral Text="Usrtxt26" runat="server" ID="LocalizedLiteral10" />
                                                                    <domval:RequiredDomValidator ID="F_GroupValidator" EnableClientScript="true" Display="Static"
                                                                        ControlToValidate="F_Group" InitialValue="-1" runat="server">*</domval:RequiredDomValidator>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="F_Group" runat="server" CssClass="BoxDesignReq" />
                                                            </td>
                                                        </tr>
                                                        <tr id="SecondaryGroup" runat="server">
                                                            <td width="40%" valign="top">
                                                                <div>
                                                                    <twc:LocalizedLiteral Text="Usrtxt75" runat="server" ID="LocalizedLiteral11" /></div>
                                                            </td>
                                                            <td>
                                                                <asp:Repeater ID="RepGroup" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="ListCategory">
                                                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td width="5%">
                                                                                <asp:CheckBox ID="Check" runat="server" />
                                                                                <asp:Literal ID="IDGr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                                                    Visible="false" />
                                                                            </td>
                                                                            <td width="90%" class="normal">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table> </div>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt27" runat="server" ID="LocalizedLiteral12" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="F_Officeso" runat="server" class="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoformRequired">
                                                                    <twc:LocalizedLiteral Text="Usrtxt28" runat="server" ID="LocalizedLiteral13" />
                                                                    <domval:RequiredDomValidator ID="F_WorkStartValidator" Display="Dynamic" ControlToValidate="F_WorkStart"
                                                                        ErrorMessage="*" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <table width="200" cellspacing="0" cellpadding="0" border="0">
                                                                    <tr>
                                                                        <td width="50">
                                                                            <asp:TextBox ID="F_WorkStart" runat="server" class="BoxDesign" onkeypress="HourCheck(this,event)"
                                                                                MaxLength="5" />
                                                                        </td>
                                                                        <td valign="middle" align="left">
                                                                            <div id="BtnStartWork" runat="server">
                                                                                <img src="/images/up.gif" onclick="HourUp('F_WorkStart')">
                                                                                <img src="/images/down.gif" onclick="HourDown('F_WorkStart')">
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <span class="divautoformRequired">
                                                                                <twc:LocalizedLiteral Text="Usrtxt29" runat="server" ID="LocalizedLiteral14" />
                                                                                <domval:RequiredDomValidator ID="EndWorkValidator" Display="Dynamic" ControlToValidate="EndWork"
                                                                                    ErrorMessage="*" runat="server" />
                                                                            </span>
                                                                        </td>
                                                                        <td width="50">
                                                                            <asp:TextBox ID="EndWork" runat="server" class="BoxDesign" onkeypress="HourCheck(this,event)"
                                                                                MaxLength="5" />
                                                                        </td>
                                                                        <td valign="middle" align="left">
                                                                            <div id="BtnEndWork" runat="server">
                                                                                <img src="/images/up.gif" onclick="HourUp('EndWork')">
                                                                                <img src="/images/down.gif" onclick="HourDown('EndWork')">
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoformRequired">
                                                                    <twc:LocalizedLiteral Text="Usrtxt28" runat="server" ID="LocalizedLiteral15" />
                                                                    <domval:RequiredDomValidator ID="F_WorkStart2Validator" Display="Dynamic" ControlToValidate="F_WorkStart2"
                                                                        ErrorMessage="*" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <table width="200" cellspacing="0" cellpadding="0" border="0">
                                                                    <tr>
                                                                        <td width="50">
                                                                            <asp:TextBox ID="F_WorkStart2" runat="server" class="BoxDesign" onkeypress="HourCheck(this,event)"
                                                                                MaxLength="5" />
                                                                        </td>
                                                                        <td valign="middle" align="left">
                                                                            <div id="BtnStartWork2" runat="server">
                                                                                <img src="/images/up.gif" onclick="HourUp('F_WorkStart2')">
                                                                                <img src="/images/down.gif" onclick="HourDown('F_WorkStart2')">
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <span class="divautoformRequired">
                                                                                <twc:LocalizedLiteral Text="Usrtxt29" runat="server" ID="LocalizedLiteral16" />
                                                                                <domval:RequiredDomValidator ID="F_Fine_Lavoro2Validator" Display="Dynamic" ControlToValidate="EndWork2"
                                                                                    ErrorMessage="*" runat="server" />
                                                                            </span>
                                                                        </td>
                                                                        <td width="50">
                                                                            <asp:TextBox ID="EndWork2" runat="server" class="BoxDesign" onkeypress="HourCheck(this,event)"
                                                                                MaxLength="5" />
                                                                        </td>
                                                                        <td valign="middle" align="left">
                                                                            <div id="BtnEndWork2" runat="server">
                                                                                <img src="/images/up.gif" onclick="HourUp('EndWork2')">
                                                                                <img src="/images/down.gif" onclick="HourDown('EndWork2')">
                                                                            </div>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="40%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt30" runat="server" ID="LocalizedLiteral17" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBoxList ID="RecurrenceWeeklyDays" runat="server" EnableViewState="true"
                                                                    CssClass="normal" RepeatColumns="7" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trZone" runat="server">
                                                <td colspan="2" valign="top" style="border-top: 1px solid #000;" runat="server">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr id="Tr1" runat="server">
                                                            <td valign="top" width="200">
                                                                <asp:RadioButtonList ID="FlagCommercial" runat="server">
                                                                </asp:RadioButtonList>
                                                                <div id="spnManager" style="display: none">
                                                                    <div>
                                                                        <twc:LocalizedLiteral Text="Usrtxt41" runat="server" />
                                                                    </div>
                                                                    <asp:DropDownList ID="IdManager" runat="server" cssclass="BoxDesign" Style="width: 80%" />
                                                                </div>
                                                            </td>
                                                            <td width="300">
                                                                <div>
                                                                    <twc:LocalizedLiteral Text="ErpConftxt6" runat="server" /></div>
                                                                <asp:Repeater ID="RepeaterZones" runat="server">
                                                                    <HeaderTemplate>
                                                                        <div class="ListCategory">
                                                                            <table id="tblZones" border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td width="5%">
                                                                                <asp:CheckBox ID="Checkbox1" runat="server" />
                                                                                <asp:Literal ID="id" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                                                    Visible="false" />
                                                                            </td>
                                                                            <td width="90%" class="normal">
                                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        </table> </div>
                                                                    </FooterTemplate>
                                                                </asp:Repeater>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" width="100%" valign="top">
                                                    <asp:Table ID="WorkTimeForm" runat="server" Visible="false" HorizontalAlign="center"
                                                        Width="100%" CssClass="normal">
                                                        <asp:TableRow runat="server">
                                                            <asp:TableCell ColumnSpan="2" Style="border-bottom: 1px solid #000000;">
	 				<B>
								<twc:LocalizedLiteral Text="Usrtxt38" runat="server" /></B>
	 				&nbsp;&nbsp;&nbsp;<span style="color:red;">
								<twc:LocalizedLiteral Text="Usrtxt17" runat="server" />
							</span>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow runat="server">
                                                            <asp:TableCell Width="50%" VerticalAlign="top" runat="server">
                                                                <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <span class="divautoform">
                                                                                <twc:LocalizedLiteral Text="Usrtxt39" runat="server" />
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="Responsable" runat="server" Text="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <span class="divautoform">
                                                                                <twc:LocalizedLiteral Text="Usrtxt40" runat="server" />
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:CheckBox ID="Subaltern" runat="server" Text="" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="40%">
                                                                            <span class="divautoform">
                                                                                <twc:LocalizedLiteral Text="Usrtxt41" runat="server" />
                                                                            </span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="IdResponsable" runat="server" class="BoxDesign" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:TableCell>
                                                            <asp:TableCell Width="50%" VerticalAlign="top">
					&nbsp;
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                        <asp:TableRow runat="server">
                                                            <asp:TableCell ColumnSpan="2" Style="border-bottom: 1px solid #000000;" runat="server">
							<B>
								<twc:LocalizedLiteral Text="Usrtxt57" runat="server" /></B>
                                                            </asp:TableCell>
                                                        </asp:TableRow>
                                                    </asp:Table>
                                                </td>
                                            </tr>
                                        </table>
                                    </twc:TustenaTab>
                                    <twc:TustenaTab ID="visConfig" LangHeader="Usrtxt76" ClientSide="true" runat="server">
                                        <table class="normal" width="100%" align="center">
                                            <tr>
                                                <td colspan="2">
                                                    <b>
                                                        <twc:LocalizedLiteral ID="LocalizedLiteral18" runat="server" Text="Usrtxt76"></twc:LocalizedLiteral></b></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" width="100%" colspan="2">
                                                    <table class="normal" cellspacing="2" cellpadding="0" width="100%" align="center"
                                                        border="0">
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral ID="LocalizedLiteral19" runat="server" Text="Usrtxt58"></twc:LocalizedLiteral>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="BoxDesign" ID="Paging" runat="server" Width="25px"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral ID="LocalizedLiteral20" runat="server" Text="Usrtxt66"></twc:LocalizedLiteral>
                                                                    <domval:RangeDomValidator ID="valSessionTimeout" runat="server" Display="dynamic"
                                                                        ControlToValidate="SessionTimeout" ErrorMessage="*" MinimumValue="1" MaximumValue="60"
                                                                        Type="Integer"></domval:RangeDomValidator>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox CssClass="BoxDesign" ID="SessionTimeout" runat="server" Width="25px"></asp:TextBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral runat="server" Text="CRMrubtxt1"></twc:LocalizedLiteral>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" width="30%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="F_IdRubrica" runat="server" Style="display: none"></asp:TextBox>
                                                                            <asp:TextBox ID="ViewF_IdRubrica" runat="server" Width="100%" CssClass="BoxDesign" ReadOnly="true" />
                                                                        </td>
                                                                        <td width="22">
                                                                            &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=ViewF_IdRubrica&textboxID=F_IdRubrica',event,400,300)">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral ID="LocalizedLiteral23" runat="server" Text="Usrtxt86"></twc:LocalizedLiteral>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="PriceList" runat=server CssClass=BoxDesign style="width:30%"></asp:DropDownList>
                                                            </td>
                                                        </tr>


                                                        <tr style="display: none">
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral ID="LocalizedLiteral21" runat="server" Text="Usrtxt60"></twc:LocalizedLiteral>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="FullScreen" runat="server"></asp:CheckBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral ID="Localizedliteral25" runat="server" Text="Usrtxt61"></twc:LocalizedLiteral>
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="ViewBirthDate" runat="server"></asp:CheckBox></td>
                                                        </tr>
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">RSS Key:</span>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="LblCSSGuid" runat="server"></asp:Label>
                                                                <asp:TextBox class="BoxDesign" ID="CSSGuid" runat="server" Width="45px"></asp:TextBox></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td valign="top" width="50%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="50%">
                                                    <div class="divautoform">
                                                        <twc:LocalizedLiteral ID="LocalizedLiteral22" runat="server" Text="Usrtxt68"></twc:LocalizedLiteral></div>
                                                    <asp:DropDownList class="BoxDesign" ID="DropCulture" runat="server" old="true" />
                                                </td>
                                                <td width="50%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <div class="divautoform">
                                                        <twc:LocalizedLiteral Text="Usrtxt65" runat="server" /></div>
                                                    <asp:DropDownList ID="TimeZoneNew" runat="server" class="BoxDesign" old="true" />
                                                </td>
                                                <td width="50%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </twc:TustenaTab>
                                    <twc:TustenaTab ID="visMail" LangHeader="Usrtxt69" ClientSide="true" runat="server">
                                        <table class="normal" width="100%" align="center">
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;">
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt69" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt70" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:RadioButtonList ID="SecureMail" runat="server" CssClass="normal">
                                                                    <asp:ListItem Selected=True>POP3</asp:ListItem>
                                                                    <asp:ListItem>Secure POP3</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:TextBox ID="MailServer" runat="server" CssClass="BoxDesign" Width="150px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt71" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="MailUser" runat="server" class="BoxDesign" Width="150px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" valign="top">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                                        <tr>
                                                            <td width="30%">
                                                                <span class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt72" runat="server" />
                                                                </span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="MailPassword" TextMode="Password" runat="server" class="BoxDesign"
                                                                    Width="150px" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="100%" valign="top">
                                                    <span class="Save" style="cursor: pointer;" onclick="CheckPop(event)">
                                                        <twc:LocalizedLiteral Text="Mailtxt21" runat="server" />
                                                    </span><span class="Save" style="cursor: pointer;" onclick="EraseCache(event)">
                                                        <twc:LocalizedLiteral Text="Mailtxt22" runat="server" />
                                                    </span>
                                                </td>
                                            </tr>
                                        </table>
                                    </twc:TustenaTab>
                                    <twc:TustenaTab ID="visAgenda" LangHeader="Usrtxt77" ClientSide="true" runat="server">
                                        <table class="normal" width="100%" align="center">
                                            <tr>
                                                <td width="50%" valign="top">
                                                    <div class="divautoform">
                                                        <twc:LocalizedLiteral Text="Usrtxt73" runat="server" /></div>
                                                    <asp:DropDownList ID="DropFirstDay" runat="server" class="BoxDesign" old="true" />
                                                </td>
                                                <td width="50%" valign="top">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="border-bottom: 1px solid #000000;">
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt42" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr runat="server">
                                                <td colspan="4">
                                                    <TOffice:SelectOffice runat="server" ID="Office" />
                                                </td>
                                            </tr>
                                            <tr id="TROfficeTitle" runat="server">
                                                <td colspan="2" style="border-bottom: 1px solid #000000;">
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt50" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr id="TROfficeFields" runat="server">
                                                <td colspan="4">
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="45%">
                                                                <div class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt43" runat="server" /></div>
                                                                <asp:ListBox ID="OfficesAll" runat="server" CssClass="listboxautoform" Rows="7" SelectionMode="Multiple" />
                                                            </td>
                                                            <td align="center">
                                                            <table>
							                                    <tr>
								                                    <td>
									                                    <input type="button" id="Btn_FwwAll" onclick="moverOffice('addall','OfficesAll','OfficesSel','OfficeSelText')" value=">>" class="btn">
								                                    </td>
							                                    </tr>
							                                    <tr>
								                                    <td>
									                                    <input type="button" id="Btn_Fww" onclick="moverOffice('add','OfficesAll','OfficesSel','OfficeSelText')" value=">" class="btn">
								                                    </td>
							                                    </tr>
							                                    <tr>
								                                    <td>
									                                    <input type="button" id="Btn_Rww" onclick="moverOffice('remove','OfficesAll','OfficesSel','OfficeSelText')" value="<" class="btn" >
								                                    </td>
							                                    </tr>
							                                    <tr>
								                                    <td>
									                                    <input type="button" id="Btn_RwwAll" onclick="moverOffice('removeall','OfficesAll','OfficesSel','OfficeSelText')" value="<<" class="btn">
								                                    </td>
							                                    </tr>
						                                    </table>


                                                            </td>
                                                            <td width="45%">
                                                                <div class="divautoform">
                                                                    <twc:LocalizedLiteral Text="Usrtxt51" runat="server" /></div>
                                                                <asp:ListBox ID="OfficesSel" runat="server" CssClass="listboxautoform" Rows="7" SelectionMode="Multiple" />
                                                                <asp:TextBox ID="OfficeSelText" runat=server style="display:none"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </twc:TustenaTab>
                                    <twc:TustenaTab ID="visHomePage" LangHeader="Usrtxt78" ClientSide="true" runat="server">
                                        <table class="normal" width="100%" align="center">
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;">
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt78" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;">
                                                    <asp:Repeater ID="RepHomePage" runat="server">
                                                        <HeaderTemplate>
                                                            <table width="60%" class="normal">
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td width="40%">
                                                                    <asp:Literal ID="MenuTitle" runat="server" />
                                                                    <asp:Literal ID="MenuID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                                                                </td>
                                                                <td width="60%">
                                                                    <asp:DropDownList ID="DropMenuDefault" runat="server" class="BoxDesign" old="true" />
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
                                    <twc:TustenaTab ID="visGroups" LangHeader="Usrtxt79" ClientSide="true" runat="server">
                                        <table class="normal" width="100%">
                                            <tr>
                                                <td colspan="2" style="border-bottom: 1px solid #000000;">
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Usrtxt67" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <GControl:GroupControl runat="server" ID="GroupControl" />
                                                </td>
                                            </tr>

                                        </table>
                                    </twc:TustenaTab>
                                </twc:TustenaTabber>
                                <asp:Label ID="LabelInfoX" runat="server" ForeColor="Red" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
