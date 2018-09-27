
<%@ Page language="c#" Codebehind="PrintProduct.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Catalog.PrintProduct" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head runat=server>
    <title>PrintProduct</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
  </HEAD>
  <body topmargin=0 leftmargin=0 style="BACKGROUND-COLOR: white">

    <form id="Form1" method="post" runat="server">
<table cellpadding=0 width="100%" class=normal>
	<tr>
		<td width="30%" valign=top align=left style="BORDER-BOTTOM:black 1px solid">
			<asp:Image ID="ProductImage" Runat=server BorderStyle=Solid BorderColor="#000000" BorderWidth="1px"></asp:Image>
		</td>
		<td align=center style="FONT-WEIGHT: bold; FONT-SIZE: 20px; BORDER-BOTTOM: black 1px solid; FONT-FAMILY: Verdana; TEXT-ALIGN: center">
			<asp:Literal ID="ProductTitle" Runat=server></asp:Literal>
		</td>
	</tr>
	<tr>
		<td colspan=2 style="PADDING-TOP: 10px; BORDER-BOTTOM: black 1px solid; TEXT-ALIGN: justify">
			<asp:Literal ID="ProductDescription" Runat=server></asp:Literal>
		</td>
	</tr>
	<tr>
		<td colspan=2 style="PADDING-TOP:10px">
		<table cellpadding=0 width="100%" class=normal>
			<tr>
				<td>
					<twc:LocalizedLiteral Text="Captxt7" runat="server" ID="Localizedliteral1"></twc:LocalizedLiteral>
				</td>
				<td>
					<asp:Literal id="TxtUnit" Runat="server"></asp:Literal>
				</td>
			</tr>
			<tr>
				<td>
					<twc:LocalizedLiteral Text="Captxt8" runat="server" ID="Localizedliteral2"></twc:LocalizedLiteral>
				</td>
				<td>
					<asp:Literal id="TxtQta" Runat="server"></asp:Literal>
				</td>
			</tr>
			<tr>
				<td>
					<twc:LocalizedLiteral Text="Captxt9" runat="server" ID="Localizedliteral3"></twc:LocalizedLiteral>
				</td>
				<td>
					<asp:Literal id="TxtQtaBlister" Runat="server"></asp:Literal>
				</td>
			</tr>
			<tr>
				<td>
					<twc:LocalizedLiteral Text="Captxt10" runat="server" ID="Localizedliteral4"></twc:LocalizedLiteral>
				</td>
				<td>
				<b>
					<asp:Literal id="CurrentCurrency" runat="server"></asp:Literal>
					<asp:Literal id="TxtUnitPrice" Runat="server"></asp:Literal>
				</b>
				</td>
			</tr>
	</table>
</td>
</tr>
</table>
     </form>

  </body>
</HTML>
