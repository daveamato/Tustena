<%@ Page Language="c#" Trace="false" codebehind="AppVerify.aspx.cs" Inherits="Digita.Tustena.AppVerify" EnableViewState="false" AutoEventWireup="false"%>

<HTML>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
		<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
			<table><tr><td width=12 class="Grid GridApp">&nbsp;</td><td class="Grid"><%=wrm.GetString("Avrtxt1")%></td></tr></table>
			<asp:Literal id="Grid" runat="server" />
		</body>
</HTML>
