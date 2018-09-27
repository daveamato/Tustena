<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ProductSchema.ascx.cs" Inherits="Digita.Tustena.Catalog.ProductSchema" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<asp:Repeater ID="ProductRepeater" Runat=server>
<ItemTemplate>
<br clear=all style="page-break-before:always">
<table cellpadding=0 width="100%" border=0 style="FONT-SIZE: 12px; COLOR: black; FONT-FAMILY: Arial">
	<tr>
		<td width="30%" valign=top align=left style="BORDER-BOTTOM:black 1px solid">
			<asp:Literal ID="ProductImage" Runat=server></asp:Literal>
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
		<table cellpadding=0 width="100%" style="FONT-SIZE: 12px; COLOR: black; FONT-FAMILY: Arial">
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
</ItemTemplate>
</asp:Repeater>
