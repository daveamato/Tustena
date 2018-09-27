<%@ Page Language="c#" Trace="false" Codebehind="PopContacts.aspx.cs" Inherits="Digita.Tustena.PopContacts"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<html>
<head runat="server">
    <title>:: Tustena ::</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="1" marginwidth="0" marginheight="0">

    <script language="javascript" src="/js/common.js"></script>

    <form runat="server">
        <asp:Literal ID="SomeJS" runat="server" />
         <table width="98%" border="0" cellspacing="3" align="center">
            <tr>
                <td>
                    <table width="98%" border="0" cellspacing="3" align="center">
                        <tr>
                            <td>
                                <asp:Label ID="Titolo" runat="Server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="CheckFunctions" runat="server" visible="False" width="70%" border="0" cellspacing="0" align="left">
                        <tr width="50%">
                            <td>
                                <%=wrm.GetString("Paztxt10")%>
                            </td>
                            <td>
                                <asp:CheckBox runat="Server" ID="CheckLeads" Checked="True" CssClass="normal" />
                            </td>
                            <td>
                                <%=wrm.GetString("Paztxt11")%>
                            </td>
                            <td>
                                <asp:CheckBox runat="Server" ID="CheckContacts" Checked="True" CssClass="normal" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="normal" valign="top">
                    <%=wrm.GetString("Paztxt6")%>
                    <asp:RadioButtonList ID="NRes" runat="server" CssClass="normal">
                        <asp:ListItem Value="10" Selected="true">10</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                        <asp:ListItem Value="50">50</asp:ListItem>
                        <asp:ListItem Value="100">100</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
                       <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" align="left">
                        <tr width="100%">
                            <td class="normal">
                            <%=wrm.GetString("Paztxt13")%>
                                <asp:RadioButtonList ID="RadioList1" runat="server" CssClass="normal">
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="3" align="left">
                        <tr width="100%">
                            <td width="40%">
                                <asp:TextBox ID="FindIt" autoclick="Find" runat="server" class="BoxDesign" />
                            </td>
                            <td >
                                <asp:LinkButton ID="Find" runat="server" class="save" />
                            </td>
                            <td >
                                <asp:LinkButton ID="NewRef" runat="server" class="save" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="ContactReferrer" runat="server">
                        <HeaderTemplate>
                            <br>
                            <table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
                                <tr>
                                    <td class="GridTitle">
                                        <%=wrm.GetString("Prftxt1")%>
                                    </td>
                                    <td class="GridTitle">
                                        <%=wrm.GetString("Prftxt6")%>
                                    </td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <span onclick="SetRef('<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "REFERENTE")))%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "CompanyName")))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# DataBinder.Eval(Container.DataItem, "companyid")%>','<%# DataBinder.Eval(Container.DataItem, "email")%>')"
                                        class="linked">
                                        <%# DataBinder.Eval(Container.DataItem, "REFERENTE")%>
                                    </span>&nbsp;</td>
                                <td class="GridItem">
                                    <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                    &nbsp;</td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <span onclick="SetRef('<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "REFERENTE")))%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "CompanyName")))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# DataBinder.Eval(Container.DataItem, "companyid")%>','<%# DataBinder.Eval(Container.DataItem, "email")%>')"
                                        class="linked">
                                        <%# DataBinder.Eval(Container.DataItem, "REFERENTE")%>
                                    </span>&nbsp;</td>
                                <td class="GridItemAltern">
                                    <%# DataBinder.Eval(Container.DataItem, "CompanyName")%>
                                    &nbsp;</td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="NewReferrer" runat="server" border="0" cellpadding="0" cellspacing="0"
                        width="98%" class="normal" align="left">
                        <tr>
                            <td align="left" class="BorderBottomTitles" style="padding-top: 20px;" colspan="2">
                                <span class="divautoform"><b>
                                    <%=wrm.GetString("Prftxt10")%>
                                </b></span>
                            </td>
                        </tr>
                        <tr>
                            <td width="40%" valign="top">
                                <%=wrm.GetString("Prftxt8")%>
                                <domval:RequiredDomValidator ID="RequiredFieldValidatorSurname" runat="server" ControlToValidate="Surname"
                                    ErrorMessage="*" />
                            </td>
                            <td>
                                <asp:TextBox ID="Surname" runat="server" CssClass="BoxDesign" onKeyPress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%" valign="top">
                                <%=wrm.GetString("Prftxt7")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Name" runat="server" CssClass="BoxDesign" onKeyPress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:LinkButton ID="RapSubmit" runat="server" CssClass="save" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
