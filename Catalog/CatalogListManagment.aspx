<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CatalogListManagment.aspx.cs" Inherits="Digita.Tustena.Catalog.CatalogListManagment" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <title>List Managment</title>
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">
</head>

<body id="body" runat="server">
    <form id="form1" runat="server">
    <twc:TustenaTabber ID="Tabber" Width="800" runat="server" EditTab="tabList">
       <twc:TustenaTab ID="tabList" LangHeader="Captxt33" runat="server" ClientSide="true">
            <table width="100%" class="normal" align="center">
                <tr>
                    <td width="60%">
                        <div><%=wrm.GetString("Captxt30")%></div>
                        <asp:Literal ID="ListId" runat="server" Visible=false></asp:Literal>
                        <asp:TextBox ID="ListDescription" runat="server" CssClass="BoxDesign" MaxLength="50"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <div>% <%=wrm.GetString("Captxt31")%></div>
                        <asp:TextBox ID="ListPercentage" runat="server" onkeypress="NumbersOnly(event,'',this)" CssClass="BoxDesign" MaxLength="2"></asp:TextBox>
                    </td>
                    <td width="15%">
                        <div>% <%=wrm.GetString("Captxt32")%></div>
                        <asp:TextBox ID="ListIncrease" runat="server" onkeypress="NumbersOnly(event,'',this)" CssClass="BoxDesign" MaxLength="2"></asp:TextBox>
                    </td>
                    <td width="10%">
                        <div>&nbsp;</div>
                        <asp:LinkButton ID="btnListSave" runat=server CssClass="Save"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <twc:TustenaRepeater ID="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="false" FilterCol="Description" AllowSearching="false" width="100%">
                        <HeaderTemplate>
                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <td width="60%" Class="GridTitle"><%=wrm.GetString("Captxt30")%></td>
                            <td width="15%" Class="GridTitle">% <%=wrm.GetString("Captxt31")%></td>
                            <td width="15%" Class="GridTitle">% <%=wrm.GetString("Captxt32")%></td>
                            <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                             <tr>
                                <td class="GridItem">
                                 <asp:Literal ID="ListId" Visible=false runat=server Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Literal>
                                 <asp:LinkButton ID="OpenList" CommandName="OpenList" runat=server CssClass=normal Text='<%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItem">
                                 <%#DataBinder.Eval(Container.DataItem,"PERCENTAGE")%>
                                </td>
                                <td class="GridItem">
                                 <%#DataBinder.Eval(Container.DataItem,"INCREASE")%>
                                </td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                             </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                             <tr>
                                <td class="GridItemAltern">
                                 <asp:Literal ID="ListId" Visible=false runat=server Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Literal>
                                 <asp:LinkButton ID="OpenList" CommandName="OpenList" runat=server CssClass=normal Text='<%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItemAltern">
                                 <%#DataBinder.Eval(Container.DataItem,"PERCENTAGE")%>
                                </td>
                                <td class="GridItemAltern">
                                 <%#DataBinder.Eval(Container.DataItem, "INCREASE")%>
                                </td>
                                <twc:RepeaterMultiDelete CssClass="GridItemAltern" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                             </tr>
                        </AlternatingItemTemplate>
            </twc:TustenaRepeater>
       </twc:TustenaTab>
    </twc:TustenaTabber>

    </form>
</body>
</html>
