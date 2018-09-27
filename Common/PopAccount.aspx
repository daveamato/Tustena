<%@ Page Language="c#" Trace="false" codebehind="PopAccount.aspx.cs" Inherits="Digita.Tustena.PopAccount"  AutoEventWireup="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<script language="javascript" src="/js/common.js"></script>

<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >

<form runat="server">
<asp:Literal id="SomeJS" runat="server" />
<table width="98%" border="0" cellspacing="0" align="center">
	<tr >
		<td align="left" width="100">
			<asp:TextBox id="FindIt"  startfocus autoclick="Find" runat="server" class="BoxDesign" />
		</td>
		<td align="left" >
			<asp:LinkButton id="Find" runat="server" class="save"/>
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
		<asp:Literal ID="HeaderInfo" Runat=server></asp:Literal>
	</td>
	</tr>
</HeaderTemplate>
<ItemTemplate>
	<tr>
	<td class="GridItem"><span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "UID") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "UserName"))%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "UserName")%></span>&nbsp;</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr>
	<td class="GridItemAltern"><span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "UID") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "UserName"))%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "UserName")%></span>&nbsp;</td>
	</tr>
</AlternatingItemTemplate>
<FooterTemplate>
	</table>
</FooterTemplate>
</asp:Repeater>
</form>
</body>
</html>
