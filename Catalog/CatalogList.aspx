<%@ Page Language="C#" AutoEventWireup="true" Codebehind="CatalogList.aspx.cs" Inherits="Digita.Tustena.Catalog.CatalogList" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>
    <script>
    function copyDataS(id, category)
	{
		(document.getElementById("FindCatID")).value = id;
		clickElement(document.getElementById("FindProduct"));
	}
    </script>
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:localizedliteral text="CRMcontxt1" runat="server" />
                                </div>
                                <div class="sideFixed">
                                    <asp:TextBox ID="Search" autoclick="FindProduct" runat="server" class="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="FindProduct" runat="server" CssClass="save" OnClick="FindProduct_Click"/></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div class="sideTitle">
                                    <twc:localizedliteral text="Quotxt44" runat="server" />
                                </div>
                                <div class="sideFixed">
                                    <asp:DropDownList ID="PriceList" runat=server CssClass=BoxDesign old=true></asp:DropDownList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <twc:localizedliteral text="Captxt3" runat="server" />
                                </div>
                                <div class="sideFixed">
                                    <ie:treeview id="tvCategoryTreeSearch" runat="server" hoverstyle="background:#F2F2F2;color:black"
                                        defaultstyle="font-family:Verdana;font-size:10px" selectedstyle="background:Gold;color:black"
                                        systemimagespath="/webctrl_client/1_0/treeimages"></ie:treeview>
                                </div>
                                <asp:TextBox ID="FindCatID" runat="server" Style="display: none" />
                            </td>
                        </tr>
                    </table>
                </td>
        <td valign="top" height="100%" class="pageStyle">
            <twc:TustenaRepeater ID="Repeater1" runat="server" CssClass="TableHlip" Width="100%"
                        SortDirection="asc" AllowPaging="true" AllowAlphabet="false" FilterCol="CompanyName"
                        AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <td class="GridTitle" width="20%">
                                <twc:LocalizedLiteral ID="LocalizedLiteral1" Text="Captxt3" runat="server" />
                            </td>
                            <twc:RepeaterColumnHeader Resource="Captxt4" id="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" width="15%" DataCol="Code"></twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Captxt5" id="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" width="40%" DataCol="ShortDescription"></twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Captxt26" id="Repeatercolumnheader4" runat="Server"
                                CssClass="GridTitle" width="10%" DataCol="Cost"></twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="CRMcontxt71" id="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" width="10%" DataCol="UnitPrice"></twc:RepeaterColumnHeader>
                            <td class="GridTitle" width="60">&nbsp;</td>
                             <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                             <tr>
                                    <td class="GridItem" width="20%">
                                        <asp:Label ID="LblCategory" runat="server" CssClass="normal" />
                                        <asp:Literal ID="LblID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    </td>
                                    <td class="GridItem" width="15%">
                                        <asp:Label ID="LblCode" runat="server" CssClass="normal" Text='<%#DataBinder.Eval(Container.DataItem,"Code")%>' />&nbsp;
                                    </td>
                                    <td class="GridItem" width="40%">
                                        <asp:Label ID="LblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>' />
                                    </td>
                                    <td class="GridItem" width="10%">
                                        <asp:Label ID="LblCost" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Cost")%>'></asp:Label>
                                    </td>
                                    <td class="GridItem" width="10%">
                                        <asp:Label ID="LblPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
                                    </td>
                                    <td class="GridItem" width="60" nowrap align=right>
                                        <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<img src=/i/sheet.gif border=0>' />
                                        <span onclick="NewWindow('/catalog/printproduct.aspx?noprint=1&list='+PriceList.options[PriceList.selectedIndex].value+'&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',500,500,'no')"
                                            style="cursor: pointer;">
                                            <img src="/i/lens.gif" border="0"></span> <span onclick="NewWindow('/catalog/printproduct.aspx?list='+PriceList.options[PriceList.selectedIndex].value+'&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',400,400,'no')"
                                                style="cursor: pointer;">
                                                <img src="/i/printer.gif" border="0"></span>
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                                <tr>
                                    <td class="GridItemAltern" width="20%">
                                        <asp:Label ID="LblCategory" runat="server" CssClass="normal" />
                                        <asp:Literal ID="LblID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    </td>
                                    <td class="GridItemAltern" width="15%">
                                        <asp:Label ID="LblCode" runat="server" CssClass="normal" Text='<%#DataBinder.Eval(Container.DataItem,"Code")%>' />&nbsp;
                                    </td>
                                    <td class="GridItemAltern" width="40%">
                                        <asp:Label ID="LblProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>' />
                                    </td>
                                    <td class="GridItemAltern" width="10%">
                                        <asp:Label ID="LblCost" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Cost")%>'></asp:Label>
                                    </td>
                                     <td class="GridItemAltern" width="10%">
                                        <asp:Label ID="LblPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UnitPrice")%>'></asp:Label>
                                    </td>
                                    <td class="GridItemAltern" width="60" nowrap align=right>
                                        <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<img src=/i/sheet.gif border=0>' />
                                        <span onclick="NewWindow('/catalog/printproduct.aspx?noprint=1&list='+PriceList.options[PriceList.selectedIndex].value+'&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',500,500,'no')"
                                            style="cursor: pointer;">
                                            <img src="/i/lens.gif" border="0"></span> <span onclick="NewWindow('/catalog/printproduct.aspx?list='+PriceList.options[PriceList.selectedIndex].value+'&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',400,400,'no')"
                                                style="cursor: pointer;">
                                                <img src="/i/printer.gif" border="0"></span>
                                    </td>
                                </tr>
                        </AlternatingItemTemplate>
            </twc:TustenaRepeater>

        </td>
        </tr> </table>
    </form>
</body>
</html>
