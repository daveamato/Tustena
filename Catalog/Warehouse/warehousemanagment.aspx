<%@ Page Language="C#" AutoEventWireup="true" Codebehind="warehousemanagment.aspx.cs"
    Inherits="Digita.Tustena.Catalog.Warehouse.warehousemanagment" %>
<%@ Register TagPrefix="war" TagName="WarehouseRows" Src="~/Catalog/Warehouse/WarehouseRows.ascx" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="head" runat="server">
    <title></title>
    <script type="text/javascript" src="/js/dynabox.js"></script>
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
                                    <%=wrm.GetString("Waretxt1")%>
                                </div>
                                <asp:LinkButton ID="btnNew" runat="server" CssClass="sidebtn"/>
                                <asp:LinkButton ID="btnRemove" runat="server" CssClass="sidebtn"/>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table cellpadding=0 cellspacing=0 width="800">
                        <tr>
                            <td>
                               <table width="100%" cellspacing=0 cellpadding=0>
											<tr><td valign="top">
											<asp:TextBox id="EstProductID" runat="server" cssclass="BoxDesign" style="display:none"/>
											<asp:TextBox id="EstProduct" runat="server" cssclass="BoxDesign"/>
											</td><td width="30" valign="top">
											&nbsp;<img src="/i/lookup.gif" border="0" style="cursor:pointer;" onclick="CreateBox('/Common/PopCatalog.aspx?render=no&ptx=EstProduct&pid=EstProductID',event,400,300)">
											<asp:LinkButton ID="btnGo" runat=server Text="GO"></asp:LinkButton>
											</td></tr>
							   </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <war:WarehouseRows id="WarehouseRows1" runat=server></war:WarehouseRows>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
