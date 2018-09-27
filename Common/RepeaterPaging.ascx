<%@ Control Language="c#" AutoEventWireup="false" Codebehind="RepeaterPaging.ascx.cs" Inherits="Digita.Tustena.Common.RepeaterPaging" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellpadding="0" cellspacing="0" class="normal">
	<tr>
		<td>
			<input type="hidden" id="TotalSize" runat="server" NAME="TotalSize">
			<asp:LinkButton id="Prev" Text="<< Previous" runat="server" />
			&nbsp;
			<asp:LinkButton id="Next" Text="Next >>" runat="server" />
		</td>
	</tr>
</table>
