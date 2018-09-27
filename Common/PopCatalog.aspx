<%@ Page Language="c#" Codebehind="PopCatalog.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PopCatalog" %>

<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat="server">
    <title>PopCatalog</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">

    <script language="javascript" src="/js/common.js"></script>

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script>
		function copyData(id, category)
	    {
	        HideFloatDiv('floatCategory');
	        (document.getElementById("TxtIdCategory")).value = id;
	        (document.getElementById("TxtTextCategory")).value = category;
	    }
    </script>

</head>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0">
    <form id="Form1" method="post" runat="server">
        <div id="floatCategory" style="display: none; position: absolute; border: 1px solid silver;
            margin: 10px;" onmouseout="HideFloatDiv('floatCategory')">
            <ie:TreeView ID="tvCategoryTree" runat="server" HoverStyle="background:#F2F2F2;color:black"
                DefaultStyle="font-family:Verdana;font-size:10px" SelectedStyle="background:#FFA500;color:black"
                SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:TreeView></div>
        <table width="100%" border="0" cellspacing="3" cellpadding="0" class="normal">
            <tr>
                <td width="40%">
                    <asp:TextBox ID="Search" autoclick="FindProduct" runat="server" class="Inputautoform"
                        Height="20" />
                </td>
                <td width="40%">
                    <asp:TextBox ID="TxtIdCategory" runat="server" Style="display: none" />
                    <table cellspacing="0" cellpadding="0" width="100%">
                        <tr>
                            <td>
                                <img src="/i/tree.gif" style="margin-right: 4px; cursor: pointer" onclick="ShowFloatDiv(event,'floatCategory')">
                            </td>
                            <td width="100%">
                                <asp:TextBox ID="TxtTextCategory" runat="server" CssClass="BoxDesignReq" ReadOnly="true"
                                    onclick="ShowFloatDiv(event,'floatCategory')"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="20%" align="left">
                    <asp:LinkButton ID="FindProduct" runat="server" class="Save" />
                </td>
            </tr>
        </table>
        <asp:Repeater ID="ProductRepeater" runat="server">
            <HeaderTemplate>
                <table border="0" cellpadding="2" cellspacing="1" width="98%" align="center" class="normal">
                    <tr>
                        <td class="GridTitle" width="20%">
                            <%=wrm.GetString("Captxt4")%>
                        </td>
                        <td class="GridTitle" width="80%">
                            <%=wrm.GetString("Captxt5")%>
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td class="GridItem" width="19%">
                        <%#DataBinder.Eval(Container.DataItem,"Code")%>
                        &nbsp;
                    </td>
                    <td class="GridItem" width="40%">
                        <asp:Literal ID="BtnProduct" runat="server" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td class="GridItemAltern" width="19%">
                        <%#DataBinder.Eval(Container.DataItem,"Code")%>
                        &nbsp;
                    </td>
                    <td class="GridItemAltern" width="40%">
                        <asp:Literal ID="BtnProduct" runat="server" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </form>
</body>
</html>
