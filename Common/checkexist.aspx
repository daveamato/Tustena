<%@ Page language="c#" trace="false" Codebehind="checkexist.aspx.cs" Inherits="Digita.Tustena.Common.checkexist"  AutoEventWireup="false"%>
<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
<asp:Label ID="ImgOK" Runat=server CssClass=normal></asp:Label>
<asp:Repeater id="CompaniesRep" runat="server" EnableViewState=False >
<HeaderTemplate>
	<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
	<tr>
	<td class="GridTitle"><%=wrm.GetString("Paztxt1")%></td>
	</tr>
</HeaderTemplate>
<ItemTemplate>
	<tr>
	<td class="GridItem"><asp:Label id="Company" runat="server"/></span>&nbsp;</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr>
	<td class="GridItemAltern"><asp:Label id="Company" runat="server"/></span>&nbsp;</td>
	</tr>
</AlternatingItemTemplate>
<FooterTemplate>
	</table>
</FooterTemplate>
</asp:Repeater>
</body>
</html>

