<%@ Page Language="c#" Codebehind="ViewCompany.aspx.cs" AutoEventWireup="true" Inherits="Digita.Tustena.ViewCompany" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>ViewCompany</title>

    <script type="text/javascript" src="/js/common.js"></script>

    <link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<body style="background-color: #ffffff" leftmargin=3 topmargin=3>
    <form id="Form1" method="post" runat="server">
        <asp:Repeater ID="Repeater1" runat="server" EnableViewState="True">
            <ItemTemplate>
                <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                    <tr>
                        <td colspan=2 align=right>
                            <asp:LinkButton ID="editCmd" CommandName="editCmd" runat=server><img border=0 src="/i/modify2.gif" /></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt17")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt26")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"InvoicingAddress")%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt27")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"InvoicingCity")%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt28")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"InvoicingStateProvince")%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt29")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"InvoicingZipCode")%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt20")%>
                        </td>
                        <td class="VisForm">
                            <table class="normal" width="100%">
                                <tr>
                                    <td>
                                        <%#DataBinder.Eval(Container.DataItem,"Phone")%>
                                        &nbsp;
                                    </td>
                                    <td width="10" align="right">
                                        <asp:Literal ID="VoipCall" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt21")%>
                        </td>
                        <td class="VisForm">
                            <%#DataBinder.Eval(Container.DataItem,"FAX")%>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt22")%>
                        </td>
                        <td class="VisForm">
                            <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>">
                                <%#DataBinder.Eval(Container.DataItem,"Email")%>
                            </a>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="40%">
                            <%=wrm.GetString("Bcotxt23")%>
                        </td>
                        <td class="VisForm">
                            <a href='http://<%#DataBinder.Eval(Container.DataItem,"WebSite")%>' target="_blank">
                                <span style="text-decoration: underline;">
                                    <%#DataBinder.Eval(Container.DataItem,"WebSite")%>
                                </span></a>&nbsp;
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:Repeater>
        <table id="edittable" runat=server border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt17")%>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat=server CssClass="BoxDesign" ReadOnly=true></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt26")%>
                </td>
                <td>
                    <asp:TextBox ID="txtInvoicingAddress" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt27")%>
                </td>
                <td>
                    <asp:TextBox ID="txtInvoicingCity" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt28")%>
                </td>
                <td>
                    <asp:TextBox ID="txtInvoicingStateProvince" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt29")%>
                </td>
                <td>
                    <asp:TextBox ID="txtInvoicingZipCode" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt20")%>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt21")%>
                </td>
                <td>
                    <asp:TextBox ID="txtFAX" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt22")%>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td width="40%">
                    <%=wrm.GetString("Bcotxt23")%>
                </td>
                <td>
                    <asp:TextBox ID="txtWebSite" runat=server CssClass="BoxDesign"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan=2 align=right>
                    <twc:LocalizedLinkButton ID="modSave" runat=server CssClass="Save" Text="Save"></twc:LocalizedLinkButton>
                    <asp:Literal ID="litID" runat=server Visible=false></asp:Literal>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
