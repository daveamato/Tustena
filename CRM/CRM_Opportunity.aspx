<%@ Page Language="c#" Codebehind="CRM_Opportunity.aspx.cs" Inherits="Digita.Tustena.CRMOpportunity"
    EnableViewState="true" Trace="false" ValidateRequest="false" AutoEventWireup="false" %>

<%@ Register TagPrefix="TOffice" TagName="SelectOffice" Src="~/Common/SelectOffice.ascx" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="Opp" TagName="CRM_OppCompany" Src="~/CRM/CRM_OppCompany.ascx" %>
<%@ Register TagPrefix="Opl" TagName="CRM_OppLead" Src="~/CRM/CRM_OppLead.ascx" %>
<%@ Register TagPrefix="src" TagName="SearchLead" Src="~/common/SearchLead.ascx" %>
<%@ Register TagPrefix="src" TagName="SearchCompany" Src="~/common/SearchCompany.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/SelectOffice.js"></script>

    <script language="javascript" src="/js/autodate.js"></script>

    <script>
function getchangecurrency(){
	var d = document.getElementById("Opportunity_Currency");
	var change = d.options[x.selectedIndex].value.split("|");
	return change[1];
}

function ValidateProduct(){
	var d = document.getElementById("EstProductID");
	if(d.value!=""){
			(document.getElementById("rvProductID")).style.display="none";
			return true;
	}else{
			(document.getElementById("rvProductID")).style.display="inline";
			return false;
	}
}
    </script>

</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
        <asp:LinkButton ID="RefreshRepeaterCompany" runat="server" Style="display: none"
            OnClick="RefreshOpp" Text="ok" />
        <asp:LinkButton ID="RefreshRepeaterLead" runat="server" Style="display: none" OnClick="RefreshOpp"
            Text="ok" />
        <asp:LinkButton ID="RefreshCloseFrame" runat="server" Style="display: none" OnClick="RefreshOpp"
            Text="ok" />
        <asp:LinkButton ID="RefreshRepeaterProducts" runat="server" Style="display: none"
            OnClick="RefreshOpp" Text="ok" />
        <table width="100%" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:LocalizedLiteral Text="Options" runat="server" ID="LocalizedLiteral1" /></div>
                                <asp:LinkButton ID="BtnNew" runat="server" class="sidebtn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:LocalizedLiteral Text="CRMopptxt7" runat="server" ID="LocalizedLiteral2" /></div>
                                <div class="sideInputTitle">
                                    <twc:LocalizedLiteral Text="CRMopptxt2" runat="server" ID="LocalizedLiteral3" /></div>
                                <div class="sideInput">
                                    <asp:TextBox ID="FindTxt" autoclick="BtnFind" runat="server" cssclass="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnFind" runat="server" cssclass="save" /></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <twc:LocalizedLiteral Text="CRMopptxt1" runat="server" ID="LocalizedLiteral4" />
                            </td>
                        </tr>
                    </table>
                    <br>
                    <twc:TustenaRepeater ID="NewRepeaterOpportunity" runat="server" SortDirection="asc"
                        AllowPaging="true" AllowAlphabet="true" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader1" runat="Server" Resource="CRMopptxt10"
                                CssClass="GridTitle" Width="70%" DataCol="Title">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader2" runat="Server" Resource="CRMopptxt11"
                                CssClass="GridTitle" Width="29%" DataCol="Owner">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle"
                                header="true">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Literal ID="IDOp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                        Visible="false">
                                    </asp:Literal>
                                    <asp:LinkButton ID="OpenOpportunity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>'
                                        CommandName="OpenOP">
                                    </asp:LinkButton></td>
                                <td class="GridItem">
                                    <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItem">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="IDOp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                        Visible="false">
                                    </asp:Literal>
                                    <asp:LinkButton ID="OpenOpportunity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>'
                                        CommandName="OpenOP">
                                    </asp:LinkButton></td>
                                <td class="GridItemAltern">
                                    <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItemAltern">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                    <span id="tabControl" runat="server">
                        <twc:TustenaTabber ID="Tabber" Width="840" runat="server" Expand="True">
                            <twc:TustenaTabberRight runat="server" ID="TustenaTabberRight1">
                                <twc:GoBackBtn ID="Back" runat="server"></twc:GoBackBtn>
                                <img style="cursor: pointer" onclick="CreateBox('/Common/PrintOpportunity.aspx?render=no&amp;textbox=<%=Opportunity_ID.Text%>',event,150,200)"
                                    alt='<%=wrm.GetString("Prnopptxt7")%>' src="/i/printer.gif" border="0">
                            </twc:TustenaTabberRight>
                            <twc:TustenaTab ID="visOpportunity" LangHeader="CRMopptxt1" ClientSide="true" runat="server">
                                <table id="OpportunityCard" runat="server" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:LinkButton ID="Opportunity_SubmitUP" runat="server" cssclass="save" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tbody>
                                                    <tr>
                                                        <td width="50%" valign="top">
                                                            <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal">
                                                                <tr>
                                                                    <td width="40%">
                                                                        <twc:LocalizedLiteral Text="CRMopptxt10" runat="server" ID="LocalizedLiteral5" />
                                                                        <domval:RequiredDomValidator ID="TitleValidator" runat="server" Font-Names="Verdana"
                                                                            Font-Size="8pt" Display="Dynamic" ControlToValidate="Opportunity_Title"></domval:RequiredDomValidator>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Opportunity_Title" runat="server" CssClass="BoxDesignReq" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt11" runat="server" ID="LocalizedLiteral6" />
                                                                        <asp:TextBox ID="Opportunity_OwnerID" runat="server" cssclass="BoxDesign" Style="display: none" />
                                                                    </td>
                                                                    <td>
                                                                        <table width="100%" cellspacing="0" cellpadding="0">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:TextBox ID="Opportunity_Owner" runat="server" CssClass="BoxDesignReq" />
                                                                                </td>
                                                                                <td width="30">
                                                                                    <div id="PopAccount" runat="server">
                                                                                        &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&amp;textbox=Opportunity_Owner&amp;textbox2=Opportunity_OwnerID',event)">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt12" runat="server" ID="LocalizedLiteral7" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="LabelCurrency" runat="server" CssClass="OpportunityView" />
                                                                        <asp:DropDownList ID="Opportunity_Currency" runat="server" old="true" cssclass="BoxDesign" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt13" runat="server" ID="LocalizedLiteral8" />
                                                                    </td>
                                                                    <td nowrap>
                                                                        <asp:Label ID="Opportunity_ExpectedRevenueSymbol" runat="server" cssclass="OpportunityView"
                                                                            Width="10px" />
                                                                        <asp:TextBox ID="Opportunity_ExpectedRevenue" runat="server" cssclass="OpportunityView"
                                                                            ReadOnly="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt67" runat="server" ID="LocalizedLiteral9" />
                                                                    </td>
                                                                    <td nowrap>
                                                                        <asp:Label ID="Opportunity_IncomeProbabilitySymbol" runat="server" cssclass="OpportunityView"
                                                                            Width="10px" />
                                                                        <asp:TextBox ID="Opportunity_IncomeProbability" runat="server" cssclass="OpportunityView"
                                                                            ReadOnly="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt14" runat="server" ID="LocalizedLiteral10" />
                                                                    </td>
                                                                    <td nowrap>
                                                                        <asp:Label ID="Opportunity_AmountClosedSymbol" runat="server" cssclass="OpportunityView"
                                                                            Width="10px" />
                                                                        <asp:TextBox ID="Opportunity_AmountClosed" runat="server" cssclass="OpportunityView"
                                                                            ReadOnly="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt15" runat="server" ID="LocalizedLiteral11" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Opportunity_CreatedBy" runat="server" cssclass="BoxDesignView" ReadOnly="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <twc:LocalizedLiteral Text="CRMopptxt16" runat="server" ID="LocalizedLiteral12" />
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="Opportunity_LastModifiedBy" runat="server" cssclass="BoxDesignView"
                                                                            ReadOnly="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td width="50%" valign="top" height="100%">
                                                            <table width="100%" height="100%" border="0" class="normal">
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <twc:LocalizedLiteral Text="CRMopptxt17" runat="server" ID="LocalizedLiteral13" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td height="100%" colspan="2">
                                                                        <asp:TextBox ID="Opportunity_Description" runat="server" TextMode="MultiLine" Height="100%"
                                                                            cssclass="BoxDesign" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="BorderBottomTitles" colspan="2">
                                                            <span class="divautoform"><b>
                                                                <twc:LocalizedLiteral Text="CRMopptxt80" runat="server" ID="LocalizedLiteral14" />
                                                            </b></span>
                                                            <br>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <br>
                                                            <asp:Repeater ID="RepeaterEstProduct" runat="server">
                                                                <HeaderTemplate>
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
                                                                        <tr>
                                                                            <td class="GridTitle" width="30%">
                                                                                <twc:LocalizedLiteral Text="CRMcontxt65" runat="server" /></td>
                                                                            <td class="GridTitle" width="10%">
                                                                                <twc:LocalizedLiteral Text="CRMcontxt66" runat="server" /></td>
                                                                            <td class="GridTitle" width="10%">
                                                                                <twc:LocalizedLiteral Text="CRMcontxt67" runat="server" /></td>
                                                                            <td class="GridTitle" width="20%">
                                                                                <twc:LocalizedLiteral Text="CRMcontxt68" runat="server" /></td>
                                                                            <td class="GridTitle" width="10%">
                                                                                <twc:LocalizedLiteral Text="Menutxt24" runat="server" /></td>
                                                                            <td class="GridTitle" width="19%">
                                                                                <twc:LocalizedLiteral Text="Menutxt47" runat="server" /></td>
                                                                        </tr>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td class="GridItem" width="30%">
                                                                            <asp:Label ID="ShortDescription" runat="server" /></td>
                                                                        <td class="GridItem" width="10%">
                                                                            <asp:Label ID="UM" runat="server" /></td>
                                                                        <td class="GridItem" width="10%">
                                                                            <asp:Label ID="Qta" runat="server" /></td>
                                                                        <td class="GridItem" width="20%">
                                                                            <asp:Label ID="UnitPrice" runat="server" /></td>
                                                                        <td class="GridItem" width="10%">
                                                                            <asp:Label ID="NCompany" runat="server" /></td>
                                                                        <td class="GridItem" width="19%">
                                                                            <asp:Label ID="Nlead" runat="server" /></td>
                                                                    </tr>
                                                                </ItemTemplate>
                                                                <AlternatingItemTemplate>
                                                                    <tr>
                                                                        <td class="GridItemAltern" width="30%">
                                                                            <asp:Label ID="ShortDescription" runat="server" /></td>
                                                                        <td class="GridItemAltern" width="10%">
                                                                            <asp:Label ID="UM" runat="server" /></td>
                                                                        <td class="GridItemAltern" width="10%">
                                                                            <asp:Label ID="Qta" runat="server" /></td>
                                                                        <td class="GridItemAltern" width="20%">
                                                                            <asp:Label ID="UnitPrice" runat="server" /></td>
                                                                        <td class="GridItemAltern" width="10%">
                                                                            <asp:Label ID="NCompany" runat="server" /></td>
                                                                        <td class="GridItemAltern" width="19%">
                                                                            <asp:Label ID="Nlead" runat="server" /></td>
                                                                    </tr>
                                                                </AlternatingItemTemplate>
                                                                <FooterTemplate>
                                                                    </table>
                                                                </FooterTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                    <span id="Opportunity_Access" runat="server">
                                                        <tr>
                                                            <td align="left" class="BorderBottomTitles" colspan="2">
                                                                <span class="divautoform"><b>
                                                                    <twc:LocalizedLiteral Text="CRMopptxt20" runat="server" ID="LocalizedLiteral15" />
                                                                </b></span>
                                                                <br>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="right">
                                                                <TOffice:SelectOffice runat="server" ID="Office" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" class="BorderBottomTitles" colspan="2">
                                                                <span class="divautoform"><b>
                                                                    <twc:LocalizedLiteral Text="CRMopptxt21" runat="server" ID="LocalizedLiteral16" />
                                                                </b></span>
                                                                <br>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" align="right">
                                                                <TOffice:SelectOffice runat="server" ID="OfficeBasic" />
                                                            </td>
                                                        </tr>
                                                    </span>
                                                    <tr>
                                                        <td colspan="2" align="right">
                                                            <asp:TextBox ID="Opportunity_ID" runat="server" Visible="false" />
                                                            <asp:TextBox ID="Opportunity_AdminAccount" runat="server" Visible="false" />
                                                            <asp:LinkButton ID="Opportunity_Submit" runat="server" cssclass="save" />
                                                            <asp:LinkButton ID="Opportunity_LoadLead" runat="server" cssclass="save" />
                                                            <asp:LinkButton ID="Opportunity_LoadCompany" runat="server" cssclass="save" />
                                                            <asp:LinkButton ID="Opportunity_Modify" runat="server" cssclass="save" Visible="false" />
                                                        </td>
                                                    </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    </TBODY></table>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="visCompany" LangHeader="CRMopptxt61" ClientSide="true" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="BorderBottomTitles" align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral17" runat="server" Text="CRMopptxt23"></twc:LocalizedLiteral></b></span><br>
                                        </td>
                                        <td class="BorderBottomTitles" align="right">
                                            <asp:TextBox cssclass="BoxDesign" ID="FindCompany" runat="server" noret="true" Width="150"></asp:TextBox>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="FindCompanyButton" runat="server"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="Opportunity_NewCo" runat="server"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="Opportunity_SearchCompany" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="save" ID="Opportunity_CompanyList" runat="server" Visible="false"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br>
                                            <table class="normal" id="TblSearchCompany" cellspacing="0" cellpadding="3" width="70%"
                                                align="center" border="0" runat="server">
                                                <tr>
                                                    <td>
                                                        <src:SearchCompany ID="SearchCompany" runat="server"></src:SearchCompany>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:LinkButton ID="SearchCompanyBtn" runat="server" CssClass="Save"></asp:LinkButton></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td width="100%">
                                                <twc:TustenaRepeater ID="RepeaterCompany" runat="server" Width="98%" SortDirection="asc"
                                                    AllowPaging="true" AllowAlphabet="true" FilterCol="CompanyName" AllowSearching="false">
                                                    <HeaderTemplate>
                                                        <twc:RepeaterHeaderBegin ID="Repeaterheaderbegin3" runat="server">
                                                        </twc:RepeaterHeaderBegin>
                                                        <tr>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt24" runat="server" /></td>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt3" runat="server" /></td>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt4" runat="server" /></td>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt13" runat="server" /></td>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt78" runat="server" /></td>
                                                            <td class="GridTitle">
                                                                <twc:LocalizedLiteral Text="CRMopptxt76" runat="server" /></td>
                                                            <twc:RepeaterMultiDelete ID="Repeatermultidelete4" runat="server" CssClass="GridTitle"
                                                                header="true">
                                                            </twc:RepeaterMultiDelete>
                                                        </tr>
                                                        <twc:RepeaterHeaderEnd ID="Repeaterheaderend3" runat="server">
                                                        </twc:RepeaterHeaderEnd>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="GridItem">
                                                                <asp:LinkButton ID="LabelOpenCompany" CommandName="OpenCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>' />
                                                                <asp:Literal ID="IdCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactID") %>'
                                                                    Visible="false" />
                                                                <asp:Literal ID="IDCross" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                                    Visible="false" />
                                                            </td>
                                                            <td class="GridItem">
                                                                <asp:Label ID="StateDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateDescr")%>' />&nbsp;</td>
                                                            <td class="GridItem">
                                                                <asp:Label ID="PhaseDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhaseDescr")%>' />&nbsp;</td>
                                                            <td class="GridItem">
                                                                <asp:Label ID="IncomeProbabilityLabel" runat="server" CssClass="normal" />
                                                            </td>
                                                            <td class="GridItem">
                                                                <%# DataBinder.Eval(Container.DataItem, "EstimatedCloseDate","{0:d}")%>
                                                                &nbsp;
                                                            </td>
                                                            <td class="GridItem">
                                                                <%# DataBinder.Eval(Container.DataItem, "SalesPersonName")%>
                                                                &nbsp;</td>
                                                            <twc:RepeaterMultiDelete CssClass="GridItem" ID="Repeatermultidelete5" runat="server">
                                                            </twc:RepeaterMultiDelete>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td class="GridItemAltern">
                                                                <asp:LinkButton ID="LabelOpenCompany" CommandName="OpenCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>' />
                                                                <asp:Literal ID="IdCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactID") %>'
                                                                    Visible="false" />
                                                                <asp:Literal ID="IDCross" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                                    Visible="false" />
                                                            </td>
                                                            <td class="GridItemAltern">
                                                                <asp:Label ID="StateDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateDescr")%>' />&nbsp;</td>
                                                            <td class="GridItemAltern">
                                                                <asp:Label ID="PhaseDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhaseDescr")%>' />&nbsp;</td>
                                                            <td class="GridItemAltern">
                                                                <asp:Label ID="IncomeProbabilityLabel" runat="server" CssClass="normal" />
                                                            </td>
                                                            <td class="GridItemAltern">
                                                                <%# DataBinder.Eval(Container.DataItem, "EstimatedCloseDate","{0:d}")%>
                                                                &nbsp;
                                                            </td>
                                                            <td class="GridItemAltern">
                                                                <%# DataBinder.Eval(Container.DataItem, "SalesPersonName")%>
                                                                &nbsp;</td>
                                                            <twc:RepeaterMultiDelete CssClass="GridItem" ID="Repeatermultidelete6" runat="server">
                                                            </twc:RepeaterMultiDelete>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                </twc:TustenaRepeater>
                                                <center>
                                                    <asp:Label cssclass="divautoformRequired" ID="RepeaterCompanyInfo" runat="server"></asp:Label></center>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="visLead" LangHeader="CRMopptxt72" ClientSide="true" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="BorderBottomTitles" align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral18" runat="server" Text="CRMopptxt72"></twc:LocalizedLiteral></b></span></td>
                                        <td class="BorderBottomTitles" align="right">
                                            <asp:TextBox cssclass="BoxDesign" ID="FindLead" runat="server" noret="true" Width="150"></asp:TextBox>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="FindLeadButton" runat="server"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="Opportunity_NewLead" runat="server"></asp:LinkButton>&nbsp;
                                            <asp:LinkButton cssclass="save" ID="Opportunity_SrcLead" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="save" ID="Opportunity_ElencoLead" runat="server" Visible="false"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <br>
                                            <table class="normal" id="TblSrcLead" cellspacing="0" cellpadding="3" width="70%"
                                                align="center" border="0" runat="server">
                                                <tr>
                                                    <td>
                                                        <src:SearchLead ID="SrcLead" runat="server"></src:SearchLead>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <asp:LinkButton ID="SrcLeadBtn" runat="server" CssClass="Save"></asp:LinkButton></td>
                                                </tr>
                                            </table>
                                            <twc:TustenaRepeater ID="RepeaterLead" runat="server" Width="98%" SortDirection="asc"
                                                AllowPaging="true" AllowAlphabet="true" FilterCol="CompanyName" AllowSearching="false">
                                                <HeaderTemplate>
                                                    <twc:RepeaterHeaderBegin ID="Repeaterheaderbegin2" runat="server">
                                                    </twc:RepeaterHeaderBegin>
                                                    <tr>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt72" runat="server" ID="Localizedliteral33" /></td>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt3" runat="server" ID="Localizedliteral34" /></td>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt4" runat="server" ID="Localizedliteral35" /></td>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt13" runat="server" ID="Localizedliteral36" /></td>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt78" runat="server" ID="Localizedliteral37" /></td>
                                                        <td class="GridTitle">
                                                            <twc:LocalizedLiteral Text="CRMopptxt76" runat="server" ID="Localizedliteral38" /></td>
                                                        <twc:RepeaterMultiDelete ID="Repeatermultidelete1" runat="server" CssClass="GridTitle"
                                                            header="true">
                                                        </twc:RepeaterMultiDelete>
                                                    </tr>
                                                    <twc:RepeaterHeaderEnd ID="Repeaterheaderend2" runat="server">
                                                    </twc:RepeaterHeaderEnd>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="GridItem">
                                                            <asp:LinkButton ID="OpenLead" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'
                                                                CommandName="OpenLead">
                                                            </asp:LinkButton>
                                                            <asp:Literal ID="IdCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactID") %>'
                                                                Visible="false">
                                                            </asp:Literal>
                                                            <asp:Literal ID="IDCross" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                                Visible="false">
                                                            </asp:Literal>
                                                        </td>
                                                        <td class="GridItem">
                                                            <asp:Label ID="StateDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateDescr")%>'>
                                                            </asp:Label>&nbsp;</td>
                                                        <td class="GridItem">
                                                            <asp:Label ID="PhaseDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhaseDescr")%>'>
                                                            </asp:Label>&nbsp;</td>
                                                        <td class="GridItem">
                                                            <asp:Label ID="IncomeProbabilityLabel" runat="server" CssClass="normal"></asp:Label></td>
                                                        <td class="GridItem">
                                                            <%# DataBinder.Eval(Container.DataItem, "EstimatedCloseDate","{0:d}")%>
                                                            &nbsp;</td>
                                                        <td class="GridItem">
                                                            <%# DataBinder.Eval(Container.DataItem, "SalesPersonName")%>
                                                            &nbsp;</td>
                                                        <twc:RepeaterMultiDelete CssClass="GridItem" ID="Repeatermultidelete3" runat="server">
                                                        </twc:RepeaterMultiDelete>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td class="GridItemAltern">
                                                            <asp:LinkButton ID="OpenLead" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'
                                                                CommandName="OpenLead">
                                                            </asp:LinkButton>
                                                            <asp:Literal ID="IdCompany" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactID") %>'
                                                                Visible="false">
                                                            </asp:Literal>
                                                            <asp:Literal ID="IDCross" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'
                                                                Visible="false">
                                                            </asp:Literal>
                                                        </td>
                                                        <td class="GridItemAltern">
                                                            <asp:Label ID="StateDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StateDescr")%>'>
                                                            </asp:Label>&nbsp;</td>
                                                        <td class="GridItemAltern">
                                                            <asp:Label ID="PhaseDescr" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PhaseDescr")%>'>
                                                            </asp:Label>&nbsp;</td>
                                                        <td class="GridItemAltern">
                                                            <asp:Label ID="IncomeProbabilityLabel" runat="server" CssClass="normal"></asp:Label></td>
                                                        <td class="GridItemAltern">
                                                            <%# DataBinder.Eval(Container.DataItem, "EstimatedCloseDate","{0:d}")%>
                                                            &nbsp;</td>
                                                        <td class="GridItemAltern">
                                                            <%# DataBinder.Eval(Container.DataItem, "SalesPersonName")%>
                                                            &nbsp;</td>
                                                        <twc:RepeaterMultiDelete CssClass="GridItem" ID="Repeatermultidelete3" runat="server">
                                                        </twc:RepeaterMultiDelete>
                                                    </tr>
                                                </AlternatingItemTemplate>
                                            </twc:TustenaRepeater>
                                            <center>
                                                <asp:Label cssclass="divautoformRequired" ID="RepeaterLeadInfo" runat="server"></asp:Label></center>
                                        </td>
                                    </tr>
                                </table>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="visActivity" LangHeader="Bcotxt45" ClientSide="true" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="BorderBottomTitles" align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral19" runat="server" Text="CRMopptxt32"></twc:LocalizedLiteral></b></span></td>
                                        <td class="BorderBottomTitles" align="right">
                                            <span class="normal">
                                                <twc:LocalizedLiteral ID="LocalizedLiteral20" runat="server" Text="Wortxt14"></twc:LocalizedLiteral>
                                            </span>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityPhone" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityLetter" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityFax" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityMemo" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityEmail" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityVisit" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivityGeneric" runat="server"></asp:LinkButton>
                                            <asp:LinkButton cssclass="normal" ID="NewActivitySolution" runat="server"></asp:LinkButton></td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            <Chrono:ActivityChronology ID="AcCrono" runat="server"></Chrono:ActivityChronology>
                                            <center>
                                                <asp:Label cssclass="divautoformRequired" ID="RepeaterActivityInfo" runat="server"></asp:Label></center>
                                        </td>
                                    </tr>
                                </table>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="VisPartner" LangHeader="CRMopptxt68" ClientSide="true" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="BorderBottomTitles" align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral21" runat="server" Text="CRMopptxt68"></twc:LocalizedLiteral></b></span></td>
                                        <td class="BorderBottomTitles" align="right">
                                            <asp:LinkButton cssclass="save" ID="AddNewPartner" runat="server"></asp:LinkButton></td>
                                    </tr>
                                </table>
                                <br>
                                <table id="PartnerCard" cellspacing="0" cellpadding="0" width="100%" runat="server">
                                    <tr>
                                        <td class="normal" nowrap width="5%">
                                            <twc:LocalizedLiteral ID="LocalizedLiteral22" runat="server" Text="CRMopptxt68"></twc:LocalizedLiteral></td>
                                        <td width="30%">
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="TextboxPartnerOppID" Style="display: none" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextboxPartnerID" Style="display: none" runat="server"></asp:TextBox>
                                                        <asp:TextBox ID="TextboxPartner" runat="server" CssClass="BoxDesign" ReadOnly="true"
                                                            Width="100%"></asp:TextBox></td>
                                                    <td width="30">
                                                        &nbsp;<img style="cursor: pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&amp;textbox=TextboxPartner&amp;textbox2=TextboxPartnerID',event,500,400)"
                                                            src="/i/user.gif" border="0">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="40%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="normal" colspan="3">
                                            <twc:LocalizedLiteral ID="LocalizedLiteral23" runat="server" Text="CRMopptxt48"></twc:LocalizedLiteral></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:TextBox cssclass="BoxDesign" ID="Opportunity_NotePartner" runat="server" Height="100px"
                                                TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="3">
                                            <asp:LinkButton cssclass="save" ID="PartnerSubmit" runat="server"></asp:LinkButton></td>
                                    </tr>
                                </table>
                                <asp:Repeater ID="RepeaterPartner" runat="server">
                                    <HeaderTemplate>
                                        <table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
                                            <tr>
                                                <td class="GridTitle" width="20%">
                                                    <twc:LocalizedLiteral Text="CRMopptxt68" runat="server" /></td>
                                                <td class="GridTitle" width="70%">
                                                    <twc:LocalizedLiteral Text="CRMopptxt48" runat="server" /></td>
                                                <td class="GridTitle" width="10%">
                                                    &nbsp;</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="GridItem" width="20%">
                                                <asp:Literal ID="PartnerID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                    Visible="false" />
                                                <asp:LinkButton ID="PartnerCommand" CommandName="PartnerCommand" runat="server" CssClass="normal" />
                                            </td>
                                            <td class="GridItem" width="70%">
                                                <%# DataBinder.Eval(Container.DataItem, "Note")%>
                                            </td>
                                            <td class="GridItem" width="10%">
                                                &nbsp;</td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td class="GridItemAltern" width="20%">
                                                <asp:Literal ID="PartnerID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                    Visible="false" />
                                                <asp:LinkButton ID="PartnerCommand" CommandName="PartnerCommand" runat="server" CssClass="normal" />
                                            </td>
                                            <td class="GridItemAltern" width="70%">
                                                <%# DataBinder.Eval(Container.DataItem, "Note")%>
                                            </td>
                                            <td class="GridItemAltern" width="10%">
                                                &nbsp;</td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <center>
                                    <asp:Label cssclass="normal" ID="RepeaterInfo" Style="color: red" runat="server"></asp:Label></center>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="VisCompetitor" LangHeader="CRMopptxt41" ClientSide="true" runat="server">
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td class="BorderBottomTitles" align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral24" runat="server" Text="CRMopptxt41"></twc:LocalizedLiteral></b></span></td>
                                        <td class="BorderBottomTitles" align="right">
                                            <asp:LinkButton cssclass="save" ID="AddNewCompetitor" runat="server"></asp:LinkButton></td>
                                    </tr>
                                </table>
                                <br>
                                <table cellspacing="0" cellpadding="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <table class="normal" id="CompetitorCard" cellspacing="0" cellpadding="0" width="100%"
                                                    border="0" runat="server">
                                                    <tr>
                                                        <td>
                                                            <table class="normal" width="100%">
                                                                <tr>
                                                                    <td width="15%">
                                                                        <twc:LocalizedLiteral ID="LocalizedLiteral25" runat="server" Text="CRMopptxt42"></twc:LocalizedLiteral></td>
                                                                    <td width="35%">
                                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                                            <tr>
                                                                                <td>
                                                                                    <div>
                                                                                    </div>
                                                                                    <twc:LocalizedLiteral ID="LocalizedLiteral26" runat="server" Text="CRMopptxt86"></twc:LocalizedLiteral>
                                                                                    <domval:RequiredDomValidator ID="chk_Comp_ID" runat="server" ControlToValidate="Competitor_CompetitorID"
                                                                                        EnableClientScript="False" ErrorMessage="*"></domval:RequiredDomValidator>
                                                                                    <div>
                                                                                    </div>
                                                                                    <asp:TextBox cssclass="BoxDesign" ID="Competitor_CompetitorID" Style="display: none"
                                                                                        runat="server"></asp:TextBox>
                                                                                    <asp:TextBox cssclass="BoxDesignView" ID="Competitor_CompanyName" runat="server" Enabled="false"></asp:TextBox></td>
                                                                                <td width="30">
                                                                                    &nbsp;<img style="cursor: pointer" onclick="CreateBox('/common/PopCompany.aspx?render=no&amp;textbox=Competitor_CompanyName&amp;textbox2=Competitor_CompetitorID',event,500,400)"
                                                                                        src="/i/user.gif" border="0">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td width="15%">
                                                                        <twc:LocalizedLiteral ID="LocalizedLiteral27" runat="server" Text="CRMopptxt43"></twc:LocalizedLiteral></td>
                                                                    <td width="35%">
                                                                        <asp:RadioButtonList cssclass="normal" ID="Competitor_Evaluation" runat="server" EnableViewState="true"
                                                                            RepeatColumns="10" Enabled="true">
                                                                            <asp:ListItem id="Competitor_EvaluationOption1" runat="server" Value="1" Selected />
                                                                            <asp:ListItem id="Competitor_EvaluationOption2" runat="server" Value="2" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption3" runat="server" Value="3" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption4" runat="server" Value="4" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption5" runat="server" Value="5" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption6" runat="server" Value="6" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption7" runat="server" Value="7" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption8" runat="server" Value="8" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption9" runat="server" Value="9" />
                                                                            <asp:ListItem id="Competitor_EvaluationOption10" runat="server" Value="10" />
                                                                        </asp:RadioButtonList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral28" runat="server" Text="CRMopptxt44"></twc:LocalizedLiteral><br>
                                                            <asp:TextBox cssclass="BoxDesignView" ID="Competitor_Description" runat="server" ReadOnly="true"
                                                                Height="50" TextMode="MultiLine"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral29" runat="server" Text="CRMopptxt46"></twc:LocalizedLiteral><br>
                                                            <asp:TextBox cssclass="BoxDesignView" ID="Competitor_Strengths" runat="server" ReadOnly="true"
                                                                Height="50" TextMode="MultiLine"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral30" runat="server" Text="CRMopptxt47"></twc:LocalizedLiteral><br>
                                                            <asp:TextBox cssclass="BoxDesignView" ID="Competitor_Weaknesses" runat="server" ReadOnly="true"
                                                                Height="50" TextMode="MultiLine"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral31" runat="server" Text="CRMopptxt48"></twc:LocalizedLiteral><br>
                                                            <asp:TextBox cssclass="BoxDesignView" ID="Competitor_Note" runat="server" ReadOnly="true"
                                                                Height="50" TextMode="MultiLine"></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" width="100%">
                                                            <asp:TextBox ID="IDCompetitor" runat="server" Visible="false"></asp:TextBox>
                                                            <asp:LinkButton cssclass="save" ID="CloseCompetitor" runat="server"></asp:LinkButton>&nbsp;
                                                            <asp:LinkButton cssclass="save" ID="SaveCompetitor" runat="server"></asp:LinkButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Repeater ID="RepeaterCompetitor" runat="server" OnItemCommand="RepeaterCompetitor_Command"
                                                    OnItemDataBound="RepeaterCompetitorDatabound">
                                                    <HeaderTemplate>
                                                        <table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="left">
                                                            <tr>
                                                                <td class="GridTitle" width="70%">
                                                                    <twc:LocalizedLiteral Text="CRMopptxt24" runat="server" /></td>
                                                                <td class="GridTitle" width="20%">
                                                                    <twc:LocalizedLiteral Text="CRMopptxt43" runat="server" /></td>
                                                                <td class="GridTitle" width="10%">
                                                                    &nbsp;</td>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="GridItem">
                                                                <asp:LinkButton ID="OpenCompetitor" runat="server" CommandName="OpenComp" /></td>
                                                            <td class="GridItem">
                                                                <asp:Label ID="CompetitorStars" runat="server" /></td>
                                                            <td class="GridItem">
                                                                <asp:LinkButton ID="DeleteCompetitor" runat="server" CommandName="DeleteCompetitor" />
                                                                <asp:TextBox ID="CompetitorID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CompetitorID")%>' />
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td class="GridItemAltern">
                                                                <asp:LinkButton ID="OpenCompetitor" runat="server" CommandName="OpenComp" /></td>
                                                            <td class="GridItemAltern">
                                                                <asp:Label ID="CompetitorStars" runat="server" /></td>
                                                            <td class="GridItemAltern">
                                                                <asp:LinkButton ID="DeleteCompetitor" runat="server" CommandName="DeleteCompetitor" />
                                                                <asp:TextBox ID="CompetitorID" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "CompetitorID")%>' />
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </TABLE>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <center>
                                                    <asp:Label cssclass="normal" ID="CompetitorInfo" Style="color: red" runat="server"></asp:Label></center>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </twc:TustenaTab>
                            <twc:TustenaTab ID="visDocuments" LangHeader="CRMcontxt58" ClientSide="true" runat="server">
                                <table class="normal" cellspacing="0" width="98%" align="center" border="0">
                                    <tr>
                                        <td align="left">
                                            <span class="divautoform"><b>
                                                <twc:LocalizedLiteral ID="LocalizedLiteral32" runat="server" Text="CRMcontxt58"></twc:LocalizedLiteral></b></span></td>
                                        <td align="right">
                                            <asp:LinkButton cssclass="Save" ID="NewDoc" runat="server"></asp:LinkButton></td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label cssclass="divautoformRequired" ID="FileRepInfo" runat="server"></asp:Label></td>
                                    </tr>
                                </table>
                                <asp:Repeater ID="FileRep" runat="server">
                                    <HeaderTemplate>
                                        <table border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td class="GridTitle" width="14%">
                                                    <twc:LocalizedLiteral Text="Dsttxt2" runat="server" /></td>
                                                <td class="GridTitle" width="1%">
                                                    R</td>
                                                <td class="GridTitle" width="15%">
                                                    <twc:LocalizedLiteral Text="Dsttxt3" runat="server" /></td>
                                                <td class="GridTitle" width="30%">
                                                    <twc:LocalizedLiteral Text="Dsttxt10" runat="server" /></td>
                                                <td class="GridTitle" width="9%" style="text-align: right;">
                                                    <twc:LocalizedLiteral Text="Dsttxt4" runat="server" /></td>
                                                <td class="GridTitle" width="1%">
                                                    &nbsp;</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="GridItem" nowrap>
                                                <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                    Visible="false" />
                                                <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                                &nbsp;
                                            </td>
                                            <td class="GridItem" nowrap>
                                                <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                                    runat="server" />
                                                <asp:Literal ID="LbReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                                    runat="server" />
                                                &nbsp;</td>
                                            <td class="GridItem" nowrap>
                                                <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                                                &nbsp;</td>
                                            <td class="GridItem" nowrap>
                                                <%#DataBinder.Eval(Container.DataItem,"owner")%>
                                                &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                                            <td class="GridItem" nowrap align="right">
                                                <%#DataBinder.Eval(Container.DataItem,"size")%>
                                                &nbsp;Kb</td>
                                            <td class="GridItem" nowrap>
                                                <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                                <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td class="GridItemAltern" nowrap>
                                                <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                                    Visible="false" />
                                                <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                                &nbsp;
                                            </td>
                                            <td class="GridItemAltern" nowrap>
                                                <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                                    runat="server" />
                                                <asp:Literal ID="LbReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                                    runat="server" />
                                                &nbsp;</td>
                                            <td class="GridItemAltern" nowrap>
                                                <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                                                &nbsp;</td>
                                            <td class="GridItemAltern" nowrap>
                                                <%#DataBinder.Eval(Container.DataItem,"owner")%>
                                                &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                                            <td class="GridItemAltern" nowrap align="right">
                                                <%#DataBinder.Eval(Container.DataItem,"size")%>
                                                &nbsp;Kb</td>
                                            <td class="GridItemAltern" nowrap>
                                                <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                                <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </twc:TustenaTab>
                        </twc:TustenaTabber>
                    </span>
                    <Opp:CRM_OppCompany ID="OppCompany" runat="server" Visible="false" />
                    <Opl:CRM_OppLead ID="OppLead" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
