<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopGetMember.aspx.cs" Inherits="Digita.Tustena.Project.PopGetMember" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<script language="javascript" src="/js/common.js"></script>

<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >

    <form id="form1" runat="server">
    <table width="98%" border="0" cellspacing="0" align="center" >
	<tr>
	    <td align="left" width="100">
	        <asp:DropDownList ID="SelectTeam" runat=server CssClass="BoxDesign"></asp:DropDownList>
		</td>
		<td align="left" width="100">
			<asp:TextBox id="FindIt" startfocus autoclick="Find" runat="server" cssclass="BoxDesign" />
		</td>
		<td align="left" >
			<asp:LinkButton id="Find" runat="server" cssclass="save"/>
		</td>
	</tr>
    </table>

    <br>

    <asp:Repeater id="ContactReferrer" runat="server" >
    <HeaderTemplate>
	    <table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
	    <tr>
	    <td class="GridTitle">
		    <%=wrm.GetString("Pactxt1")%>
	    </td>
	    </tr>
    </HeaderTemplate>
    <ItemTemplate>
	    <tr>
<%# DataBinder.Eval(Container.DataItem, "MEMBERNAME")%></span>&nbsp;</td>
	    </tr>
    </ItemTemplate>
    <AlternatingItemTemplate>
	    <tr>
<%# DataBinder.Eval(Container.DataItem, "MEMBERNAME")%></span>&nbsp;</td>
	    </tr>
    </AlternatingItemTemplate>
    <FooterTemplate>
	    </table>
    </FooterTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
