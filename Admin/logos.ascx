<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="logos.ascx.cs" Inherits="Digita.Tustena.Admin.logos" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellpadding="0" cellspacing="5" width="80%">
	<TBODY>
		<tr>
			<TD width="80%">
				<DIV>File</DIV>
				<Upload:InputFile id="inputFile" runat="server" CssClass="BoxDesign"></Upload:InputFile>
			</TD>
			<td>
				<DIV>&nbsp;</DIV>
				<asp:LinkButton ID="btnUpload" Runat="server" CssClass="Save"></asp:LinkButton>
			</td>
		</tr>
		<tr>
			<td colspan="2">

				<asp:Repeater ID="repLogos" Runat="server">
					<HeaderTemplate>
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td class="GridTitle" width="99%">
									File
								</td>
								<td width="1%" class="GridTitle">
									<asp:LinkButton ID="MultiDelete" CommandName="MoltiDelete" Runat=server></asp:LinkButton>
								</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="GridItem" width="99%">
								<asp:Literal ID="litFile" Runat="server"></asp:Literal>
							</td>
							<td width="1%" class="GridItem">
								<asp:CheckBox ID="chkDelete" Runat=server></asp:CheckBox>
							</td>
						</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
						<tr>
							<td class="GridItemAltern" width="99%">
								<asp:Literal ID="litFile" Runat="server"></asp:Literal>
							</td>
							<td width="1%" class="GridItemAltern">
								<asp:CheckBox ID="chkDelete" Runat=server></asp:CheckBox>
							</td>
						</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
</table>
</FooterTemplate> </asp:Repeater></TD></TR></TBODY></TABLE>
