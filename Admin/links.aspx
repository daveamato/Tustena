<%@ Page Language="c#" Codebehind="links.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Admin.links" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td align="left" class="BorderBottomTitles normal">
                                <b>
                                    <twc:LocalizedLiteral Text="Listxt0" runat="server" /></b>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="BtnCompanyType" runat="server" CssClass="normal" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="BtnContactType" runat="server" CssClass="normal" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="BtnContactEstimate" runat="server" CssClass="normal" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td align="left" class="BorderBottomTitles normal" colspan="2">
                                <b>
                                    <twc:LocalizedLiteral Text="Listxt4" runat="server" />&nbsp;&nbsp;&nbsp;<asp:Literal
                                        ID="WhichList" runat="server" /></b>
                            </td>
                        </tr>
                        <tr>
                            <td width="50%">
                                <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="left">
                                    <asp:Repeater ID="ListElement" runat="server">
                                        <HeaderTemplate>
                                            <tr>
                                                <td class="GridTitle">
                                                    <twc:LocalizedLiteral Text="Listxt5" runat="server" /></td>
                                                <td class="GridTitle" width="1%">
                                                    <twc:LocalizedLiteral Text="Listxt8" runat="server" /></td>
                                                <td class="GridTitle" width="10%">
                                                    <asp:LinkButton ID="NewElement" CommandName="New" runat="server" CssClass="normal" />
                                                </td>
                                            </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td class="GridItem">
                                                    <asp:Literal ID="OpenEl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>' />
                                                </td>
                                                <td class="GridItem">
                                                    <asp:Literal ID="LangLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>' />
                                                </td>
                                                <td class="GridItem" width="10%" nowrap>
                                                    <asp:LinkButton ID="ModElement" CommandName="Modify" runat="server" CssClass="normal" />
                                                    <asp:LinkButton ID="DeleteElement" CommandName="Delete" runat="server" CssClass="normal" />
                                                    <asp:LinkButton ID="NewElement" CommandName="NewElementLang" runat="server" CssClass="normal" />
                                                    <asp:Literal ID="IdElement" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                    <asp:Literal ID="KElement" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>' />
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="ElementsListCommand" OnItemDataBound="ElementsListDatabound"
                                                DataSource='<%# getOtherLanguage( (int)DataBinder.Eval(Container.DataItem, "ID") ) %>'>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="GridItem">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Literal ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>' />
                                                        </td>
                                                        <td class="GridItem">
                                                            <asp:Literal ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>' />
                                                        </td>
                                                        <td class="GridItem" width="10%" nowrap>
                                                            <asp:LinkButton ID="Linkbutton1" CommandName="Modify" runat="server" CssClass="normal" />
                                                            <asp:LinkButton ID="Linkbutton2" CommandName="Delete" runat="server" CssClass="normal" />
                                                            <asp:Literal ID="Label3" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                            <asp:Literal ID="Label4" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </ItemTemplate>
                                        <AlternatingItemTemplate>
                                            <tr>
                                                <td class="GridItemAltern">
                                                    <asp:Literal ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>' />
                                                </td>
                                                <td class="GridItemAltern">
                                                    <asp:Literal ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>' />
                                                </td>
                                                <td class="GridItemAltern" width="10%" nowrap>
                                                    <asp:LinkButton ID="Linkbutton3" CommandName="Modify" runat="server" CssClass="normal" />
                                                    <asp:LinkButton ID="Linkbutton4" CommandName="Delete" runat="server" CssClass="normal" />
                                                    <asp:LinkButton ID="Linkbutton5" CommandName="NewElementLang" runat="server" CssClass="normal" />
                                                    <asp:Literal ID="Label7" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                    <asp:Literal ID="Label8" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>' />
                                                </td>
                                            </tr>
                                            <asp:Repeater ID="Repeater2" runat="server" OnItemCommand="ElementsListCommand" OnItemDataBound="ElementsListDatabound"
                                                DataSource='<%# getOtherLanguage( (int)DataBinder.Eval(Container.DataItem, "ID") ) %>'>
                                                <HeaderTemplate>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="GridItem">
                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                            <asp:Literal ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>' />
                                                        </td>
                                                        <td class="GridItemAltern">
                                                            <asp:Literal ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>' />
                                                        </td>
                                                        <td class="GridItemAltern" width="10%" nowrap>
                                                            <asp:LinkButton ID="Linkbutton6" CommandName="Modify" runat="server" CssClass="normal" />
                                                            <asp:LinkButton ID="Linkbutton7" CommandName="Delete" runat="server" CssClass="normal" />
                                                            <asp:Literal ID="Label11" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                            <asp:Literal ID="Label12" runat="server" Visible="False" Text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>' />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </AlternatingItemTemplate>
                                        <FooterTemplate>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                            <td valign="top" width="50%">
                                <table id="FormTable" runat="server" border="0" cellpadding="3" cellspacing="0" width="98%"
                                    class="normal" align="center">
                                    <tr>
                                        <td>
                                            <div class="normal">
                                                <twc:LocalizedLiteral Text="Listxt5" runat="server" /></div>
                                            <asp:TextBox ID="ElementDescription" runat="server" CssClass="BoxDesign" MaxLength="49"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="MyUICulture" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:LinkButton ID="SubmitElement" runat="server" CssClass="save" Text="OK"></asp:LinkButton>
                                            <asp:Literal ID="IDSelectElement" runat="server" Visible="False"></asp:Literal>
                                            <asp:Literal ID="ElementForm" runat="server" Visible="False"></asp:Literal>
                                            <asp:Literal ID="ElementCamp" runat="server" Visible="False"></asp:Literal>
                                            <asp:Literal ID="KElement" runat="server" Visible="False"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
