<%@ Control Language="c#" AutoEventWireUp="false" Codebehind="RepeaterSearch.ascx.cs" Inherits="Digita.Tustena.WebControls.RepeaterSearch" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<br>
<table width="100%" id="searchTable" cellpadding="2" cellspacing="1" align="center" runat="server">
	<tr>
		<td id="alphaListtd" class="normal" runat="server" align="center" nowrap>|&nbsp;
			<asp:DropDownList id="searchCols" runat="server" CssClass="repInputtext"></asp:DropDownList>&nbsp;=
			<asp:TextBox id="txtSearchVal" runat="server" CssClass="repInputtext"></asp:TextBox>&nbsp;
			<asp:LinkButton id="doSearch" runat="server">
				<img src="images/go.gif" border="0"></asp:LinkButton>&nbsp;|
		</td>
	</tr>
</table>
