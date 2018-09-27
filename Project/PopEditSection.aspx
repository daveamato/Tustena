<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopEditSection.aspx.cs" Inherits="Digita.Tustena.Project.PopEditSection" %>
<%@ Register TagPrefix="sect" TagName="ProjectSessions" Src="~/project/ProjectSessions.ascx" %>
<%@ Register TagPrefix="rel" TagName="ProjectSectionRelation" Src="~/project/ProjectSectionRelation.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <title>Section</title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
	<script language="javascript" src="/js/common.js"></script>
	<script language="javascript" src="/js/dynabox.js"></script>
</head>
<body id="body" runat="server">
    <form id="form1" runat="server">
    <table cellpadding=0 cellspacing=0>
        <tr>
            <td align=right style="height: 12px">
                <asp:LinkButton ID="switchControl" runat=server CssClass=Save OnClick="switchControl_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <sect:ProjectSessions runat="server" ID="ProjectSessions1" />
                <rel:ProjectSectionRelation runat="server" ID="ProjectSectionRelation1" />
            </td>
        </tr>
        <tr>
            <td align=right style="height: 12px">
                <asp:LinkButton ID="saveRelations" runat=server CssClass=Save OnClick="saveRelations_Click">Salva</asp:LinkButton>
            </td>
        </tr>
    </table>

    </form>
</body>
</html>
