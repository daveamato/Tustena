<%@ Page Language="c#" Codebehind="ERPConfiguration.aspx.cs" AutoEventWireup="false"
    Inherits="Digita.Tustena.ERP.ERPConfiguration" %>

<%@ Register TagPrefix="uc1" TagName="ExtendedValueEditor" Src="~/Common/ExtendedValueEditor.ascx" %>
<%@ Register TagPrefix="list" TagName="ListConfiguration" Src="~/admin/ListConfiguration.ascx" %>
<%@ Register TagPrefix="log" TagName="logos" Src="~/Admin/logos.ascx" %>
<%@ Register TagPrefix="fre" TagName="ConfigFreeFields" Src="~/Admin/ConfigFreeFields.ascx" %>
<%@ Register TagPrefix="rank" TagName="ScoreManagment" Src="~/admin/ScoreManagment.ascx" %>
<%@ Register TagPrefix="currency" TagName="currency" Src="~/admin/currency.ascx" %>
<%@ Register TagPrefix="QuoteNumber" TagName="QuoteNumber" Src="~/admin/quotenumber.ascx" %>
<%@ Register TagPrefix="ShipDates" TagName="ShipDates" Src="~/admin/ShipDates.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">
</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton ID="btnZones" runat="server" CssClass="sidebtn" OnClick="btnZones_Click" />
                                <asp:LinkButton ID="btnLogos" runat="server" CssClass="sidebtn" OnClick="btnLogos_Click" />
                                <asp:LinkButton ID="btnScore" runat="server" CssClass="sidebtn" OnClick="btnScore_Click" />
                                <asp:LinkButton ID="btnCurrency" runat="server" CssClass="sidebtn" OnClick="btnCurrency_Click" />
                                <asp:LinkButton ID="btnLinks" runat="server" CssClass="sidebtn" Text="Links" />
                                <asp:LinkButton ID="btnCompanyCode" runat="server" CssClass="sidebtn" />
                                <asp:LinkButton ID="btnContactCode" runat="server" CssClass="sidebtn" />
                                <asp:LinkButton ID="btnWizard" runat="server" CssClass="sidebtn" Text="Wizard" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr id="SalesConfiguration" runat=server>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Menutxt63")%>
                                </div>
                                <asp:LinkButton ID="btnTaxTable" runat="server" CssClass="sidebtn" OnClick="btnTaxTable_Click" />
                                <asp:LinkButton ID="btnPayment" runat="server" CssClass="sidebtn" OnClick="btnPayment_Click" />
                                <asp:LinkButton ID="btnQuoteNumber" runat="server" CssClass="sidebtn"></asp:LinkButton>
                                <asp:LinkButton ID="btnShipDates" runat="server" CssClass="sidebtn"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:LocalizedLiteral Text="Listxt0" runat="server" ID="LocalizedLiteral1" /></div>
                                <asp:LinkButton ID="BtnCompanyType" runat="server" CssClass="sidebtn" OnClick="Btn_Click" />
                                <asp:LinkButton ID="BtnContactType" runat="server" CssClass="sidebtn" OnClick="Btn_Click" />
                                <asp:LinkButton ID="BtnContactEstimate" runat="server" CssClass="sidebtn" OnClick="Btn_Click" />
                                <asp:LinkButton ID="BtnLeadOrigin" runat="server" CssClass="sidebtn" OnClick="Btn_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:LocalizedLiteral Text="Fretxt1" runat="server" ID="LocalizedLiteral2" /></div>
                                <asp:LinkButton ID="FreeForContact" runat="server" CssClass="sidebtn" OnClick="FreeField_Click" />
                                <asp:LinkButton ID="FreeForReferenti" runat="server" CssClass="sidebtn" OnClick="FreeField_Click" />
                                <asp:LinkButton ID="FreeForLeads" runat="server" CssClass="sidebtn" OnClick="FreeField_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("ErpConftxt1")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal ID="HelpLabel" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <twc:TustenaTabber ID="Tabber" Width="800" runat="server" EditTab="visControl">
                        <twc:TustenaTab ID="visControl" runat="server" Header="control" ClientSide="true">
                            <uc1:ExtendedValueEditor ID="ValueEditor1" runat="server"></uc1:ExtendedValueEditor>
                            <log:logos ID="LogosEditor1" runat="server"></log:logos>
                            <list:ListConfiguration ID="ListConfiguration1" runat="server"></list:ListConfiguration>
                            <fre:ConfigFreeFields ID="ConfigFreeFields1" runat="server"></fre:ConfigFreeFields>
                            <rank:ScoreManagment ID="rank1" runat="server"></rank:ScoreManagment>
                            <currency:currency ID="currency1" runat="server"></currency:currency>
                            <QuoteNumber:QuoteNumber ID="Nprog1" runat="server" />
                            <ShipDates:ShipDates ID="ShipDates1" runat="server" />
                            <asp:Panel ID="linksInfoPanel" Visible="false" runat="server">
                                <table style="border: 1px solid black; margin: 2px">
                                    <tr>
                                        <th colspan="2" align="left">
                                            Tags:</tr>
                                    <tr>
                                        <td>
                                            <i>tustena.company</i></td>
                                        <td>
                                            company name</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.address</i></td>
                                        <td>
                                            company address</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.zip</i></td>
                                        <td>
                                            zip code</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.city</i></td>
                                        <td>
                                            city</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.phone</i></td>
                                        <td>
                                            phone number</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.email</i></td>
                                        <td>
                                            email address</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <i>tustena.language</i></td>
                                        <td>
                                            language</td>
                                    </tr>
                                </table>
                                <table style="border: 1px solid black; margin: 2px">
                                    <tr>
                                        <th colspan="2" align="left">
                                            Examples:</tr>
                                    <tr>
                                        <td>
                                            Search on Google:</td>
                                        <td>
                                            http://www.google.com/search?hl=<b>tustena.language</b>&ie=UTF-8&q=<b>tustena.company</b></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Locate on Maporama:</td>
                                        <td>
                                            "http://www.maporama.it/share/Map.asp?COUNTRYCODE=<b>tustena.language</b>&_XgoGCAddress=<b>tustena.address</b>&Zip=<b>tustena.zip</b>&State=&_XgoGCTownName=<b>tustena.city</b></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </twc:TustenaTab>
                    </twc:TustenaTabber>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
