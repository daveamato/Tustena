<%@ Page Language="c#" Codebehind="QBDefault.aspx.cs" Inherits="Digita.Tustena.QBDefault"
    Trace="false" AutoEventWireup="false" %>

<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script language="javascript" src="/js/hashtable.js"></script>

    <style>
/* Sortable tables */
table.sortable a.sortheader {
    background-color:#eee;
    color:#666666;
    font-weight: bold;
    text-decoration: none;
    display: block;
}
table.sortable span.sortarrow {
    color: black;
    text-decoration: none;
}
</style>

    <script language="javascript" src="/js/makeor.js"></script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <a href="javascript:location.href='QBEdit.aspx?m=55&si=42&n=1'" class="sidebtn">
                                    <%=wrm.GetString("QBUtxt11")%>
                                </a><a href="javascript:location.href='QBDefault.aspx?m=55&si=42'" class="sidebtn">
                                    <%=wrm.GetString("QBUtxt12")%>
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Reftxt4")%>
                                </div>
                                <div class="sideFixed">
                                    <asp:TextBox ID="Search" autoclick="BtnSearch" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt54")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListCategory" runat="server" class="BoxDesign" /></div>
                                <asp:LinkButton ID="BtnDelCategory" runat="server" CssClass="sidebtn" />
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnSearch" runat="server" CssClass="save" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("QBUtxt4")%>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 5px;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <asp:Repeater ID="QBRepeater" runat="server">
                        <HeaderTemplate>
                            <table border="0" cellpadding="2" cellspacing="1" width="100%" align="center" class="normal">
                                <tr>
                                    <td class="GridTitle" colspan="3">
                                        <%=wrm.GetString("QBUtxt23")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="GridTitle" width="25%">
                                        <%=wrm.GetString("QBUtxt21")%>
                                    </td>
                                    <td class="GridTitle" width="74%">
                                        <%=wrm.GetString("QBUtxt22")%>
                                    </td>
                                    <td class="GridTitle" width="1%">
                                        &nbsp;</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem" width="25%">
                                    <asp:LinkButton ID="QBDescription" CommandName="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' />
                                </td>
                                <td class="GridItem" width="74%">
                                    <%#DataBinder.Eval(Container.DataItem,"Description")%>
                                    &nbsp;
                                </td>
                                <td class="GridItem" width="1%" nowrap>
                                    <asp:LinkButton runat="server" ID="Modify" CommandName="Modify" CssClass="normal"
                                        Text='<%=wrm.GetString("QBUtxt19")%>' />
                                    <asp:LinkButton runat="server" ID="Delete" CommandName="Delete" CssClass="normal"
                                        Text='<%=wrm.GetString("QBUtxt2")%>' />
                                    <asp:Literal ID="QueryID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern" width="25%">
                                    <asp:LinkButton ID="QBDescription" CommandName="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' />
                                </td>
                                <td class="GridItemAltern" width="74%">
                                    <%#DataBinder.Eval(Container.DataItem,"Description")%>
                                    &nbsp;
                                </td>
                                <td class="GridItemAltern" width="1%" nowrap>
                                    <asp:LinkButton runat="server" ID="Modify" CommandName="Modify" CssClass="normal"
                                        Text='<%=wrm.GetString("QBUtxt19")%>' />
                                    <asp:LinkButton runat="server" ID="Delete" CommandName="Delete" CssClass="normal"
                                        Text='<%=wrm.GetString("QBUtxt2")%>' />
                                    <asp:Literal ID="QueryID" Visible="false" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <center>
                        <asp:Label ID="QBRepeaterInfo" runat="server" CssClass="divautoformRequired" /></center>
                    <Pag:RepeaterPaging ID="QBRepeaterPaging" Visible="false" runat="server" />
                    <table id="QBParam" runat="server" width="100%" cellspacing="5">
                        <tr>
                            <td width="300" valign="top">
                                <div>
                                    <%=wrm.GetString("QBUtxt23")%>
                                </div>
                                <asp:Literal ID="LabelParameters" runat="server" />
                            </td>
                            <td valign="top">
                                &nbsp;
                            </td>
                            <td valign="top">
                                <div>
                                    <%=wrm.GetString("QBUtxt25")%>
                                </div>
                                <asp:RadioButtonList ID="RadioResult" runat="server" RepeatColumns="1" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow">
                                    <asp:ListItem Value="HTML" Selected="True">HTML</asp:ListItem>
                                    <asp:ListItem Value="PDF">PDF</asp:ListItem>
                                    <asp:ListItem Value="RTF">RTF [Word]</asp:ListItem>
                                    <asp:ListItem Value="Excel">Excel XML</asp:ListItem>
                                    <asp:ListItem Value="XML">XML</asp:ListItem>
                                    <asp:ListItem Value="ADO">ADO Recordset</asp:ListItem>
                                    <asp:ListItem Value="CSV1">CSV (,) [Excel 2000]</asp:ListItem>
                                    <asp:ListItem Value="CSV2">CSV (;) [Excel XP/2003]</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                &nbsp;</td>
                            <td align="left">
                                <br>
                                <asp:LinkButton ID="QBSubmitStep3F" runat="server" CssClass="save" Text="Export" />
                            </td>
                        </tr>
                    </table>
                    <center>
                        <asp:Label ID="QBResult" runat="server" EnableViewState="false" /></center>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
