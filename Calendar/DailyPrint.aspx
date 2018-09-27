<%@ Page language="c#" Codebehind="DailyPrint.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Calendar.DailyPrint" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>DailyPrint</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
  <body topmargin=0 leftmargin=0 onload="self.print();self.close();">

    <form id="Form1" method="post" runat="server">
<asp:panel id="DailyPanel" runat="server" visible="false" >
<table border="0" width="98%" cellspacing=0 align="center">
<tr>
<td width="100%" >
	<asp:Literal id="LblTitle" runat="server"/>
</td>
</tr>
</table>

<table width="98%" align="center" cellspacing=0 cellpadding=2>
<tr>
<td class="GridTitle" width="5%"><twc:localizedLiteral text="Caltxt26" runat="server" ID="Localizedliteral1"/></td>
<td class="GridTitle" width="50%"><twc:localizedLiteral text="Caltxt27" runat="server" ID="Localizedliteral2"/></td>
<td class="GridTitle" width="45%"><twc:localizedLiteral text="Caltxt28" runat="server" ID="Localizedliteral3"/></td>
</tr>
<asp:Literal id="Detail" runat="server" />
</table>
</asp:panel>
     </form>

  </body>
</html>
