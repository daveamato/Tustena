<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ScoreManagment.ascx.cs" Inherits="Digita.Tustena.Admin.ScoreManagment1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<twc:localizedDiv text="Scotxt4" runat="server" style="PADDING-BOTTOM:5px" id="LocalizedDiv1" />
<table cellpadding="0" cellspacing="0">
	<TBODY>
		<tr>
			<td width="500">
				<asp:Repeater ID="scorerepeater" Runat="server">
					<HeaderTemplate>
						<table cellpadding="0" cellspacing="0" width="100%" align="center">
							<tr>
								<td class="GridTitle" width="80%"><twc:LocalizedLiteral text="Scotxt1" runat="server" /></td>
								<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="Scotxt2" runat="server" /></td>
								<td class="GridTitle" width="10%" align="right">
									<asp:LinkButton ID="DeleteItems" CommandName="DeleteItems" Runat="server">
										<img src="/i/trash.gif" border="0" align="right"></asp:LinkButton>
								</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="GridItem" width="80%">
								<asp:Label ID="ScoreID" Runat="server" Visible="False"></asp:Label>
								<asp:TextBox ID="ScoreDescription" Runat="server" cssclass="BoxDesign"></asp:TextBox>
							</td>
							<td class="GridItem" width="10%" align="right">
								<asp:TextBox ID="ScoreWeight" Runat="server" MaxLength="3" cssclass="BoxDesign" style="width:30px"
									onkeypress="NumbersOnly(event,'',this)"></asp:TextBox>%
							</td>
							<td class="GridItem" width="10%" align="right">
								<asp:CheckBox ID="todelete" Runat="server"></asp:CheckBox>
							</td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
						<tr>
							<td class="GridTitle" width="80%">&nbsp;</td>
							<td class="GridTitle" width="10%" align="right">
								<asp:TextBox ID="ScoreTotal" Runat="server" MaxLength="3" cssclass="BoxDesign" style="width:30px"
									Enabled="False"></asp:TextBox>%
							</td>
							<td class="GridTitle" width="10%">
								&nbsp;
							</td>
						</tr>
						<tr>
							<td class="GridTitle" width="80%">
								<asp:TextBox ID="AddDescription" Runat="server" cssclass="BoxDesign"></asp:TextBox>
							</td>
							<td class="GridTitle" width="10%">
								<asp:TextBox ID="AddWeight" Runat="server" MaxLength="3" cssclass="BoxDesign" style="width:30px"
									onkeypress="NumbersOnly(event,'',this)"></asp:TextBox>%
							</td>
							<td class="GridTitle" width="10%">
								<asp:LinkButton ID="AddItem" CommandName="AddItem" Runat="server" CssClass="Save"></asp:LinkButton>
							</td>
						</tr>
</table>
</FooterTemplate> </asp:Repeater></TD></TR>
<tr>
	<td align="right">
		<asp:LinkButton ID="submit" Runat="server" CssClass="save"></asp:LinkButton>
	</td>
</tr>
</TBODY></TABLE>
