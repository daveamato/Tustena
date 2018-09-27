<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" AutoEventWireUp="false" Codebehind="RepeaterBottom.ascx.cs" Inherits="Digita.Tustena.WebControls.RepeaterBottom" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<tr>
	<td id="footerTd" class="GridTitle" runat="server">
		<twc:localizedliteral id="LocalizedLiteral30" runat="server" Text="Elements"></twc:localizedliteral>:&nbsp;<asp:Label id="Elements" runat="server" EnableViewState="False"></asp:Label>&nbsp;
		<asp:LinkButton id="backBtn" runat="server" CssClass="normal">
			<twc:localizedliteral id="Localizedliteral1" runat="server" Text="PreviousTxt"></twc:localizedliteral>
		</asp:LinkButton>&nbsp;
		<asp:PlaceHolder id="numbersHolder" runat="server" EnableViewState="False"></asp:PlaceHolder>&nbsp;
		<asp:LinkButton id="nextBtn" runat="server" CssClass="normal">
			<twc:localizedliteral id="Localizedliteral2" runat="server" Text="NextTxt"></twc:localizedliteral>
		</asp:LinkButton>
	</td>
</tr>
</TABLE>
