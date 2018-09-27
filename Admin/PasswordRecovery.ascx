<%@ Control Language="c#" AutoEventWireup="false" Codebehind="PasswordRecovery.ascx.cs" Inherits="Digita.Tustena.Admin.PasswordRecovery" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<table cellpadding=0 cellspacing=5>
	<tr>
		<td colspan=2 align=middle>
			<div class="normal"><b><twc:LocalizedLiteral text="PassRecovery3" runat="server"/></b></div>
		</td>
	</tr>
	<tr>
		 <td width="50%" nowrap>
             <div class="normal"><twc:LocalizedLiteral text="Lgntxt1" runat="server"/></div>
         </td>
         <td>
             <ASP:TextBox cssclass="inputautoform" id="Usr" runat="server" width="120" autoclick="SubmitBtn"></ASP:TextBox>
         </td>
	</tr>
	<tr>
		<td colspan=2 align=middle>
			<asp:LinkButton ID="SubmitBtn" Runat=server CssClass="Save"></asp:LinkButton>
		</td>
	</tr>
	<tr>
		<td colspan=2>
			<asp:Label ID="LblInfo" Runat=server CssClass="normal" ForeColor=#ff0000></asp:Label>
		</td>
	</tr>
</table>
