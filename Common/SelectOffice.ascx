<%@ Control Language="c#" Debug="false" codebehind="SelectOffice.ascx.cs" Inherits="Digita.Tustena.SelectOffice" AutoEventWireup="true" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<table width="98%" align="center">
	<tr>
		<td width="50%">
			<div class="normal"><twc:LocalizedLiteral text="Meettxt2" runat="server" id="LocalizedLiteral1" /></div>
			<asp:Dropdownlist id="Offices" runat="server" old="true" class="BoxDesign" OnSelectedIndexChanged="Offices_SelectedIndexChanged"
				Autopostback="True" />
		</td>
		<td align="left">
			<div class="normal"><twc:LocalizedLiteral text="Meettxt3" runat="server" id="LocalizedLiteral2" /></div>
			<table>
				<tr>
					<td width="100%">
						<asp:TextBox id="SearchUser" runat="server" class="BoxDesign" noret="true" />
					</td>
					<td width="20">
						<asp:LinkButton id="SearchUserSubmit" runat="server" Class="Save" Text="OK" CausesValidation="false"
							OnClick="searchutesubmit_click" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<asp:Table id="Offices_Table" width="100%" align="center" runat="server" CellPadding="4" visible="true"
				EnableViewState="true">
				<asp:TableRow>
					<asp:TableCell width="45%">
						<div class="divautoform">
							<twc:LocalizedLiteral text="CRMopptxt63" runat="server" /></div>
						<asp:ListBox id="OfficeUsers" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple"
							ondblclick="document.getElementById('Btn_Fww').click()" />
					</asp:TableCell>
					<asp:TableCell align="center" width="10%">
						<table>
							<tr>
								<td>
									<input type="button" id="Btn_FwwAll" onclick="moverOffice('addall','<%=controlId%>_OfficeUsers','<%=controlId%>_SelectedUsersected','<%=controlId%>_GroupValue')" value=">>" class="btn">
								</td>
							</tr>
							<tr>
								<td>
									<input type="button" id="Btn_Fww" onclick="moverOffice('add','<%=controlId%>_OfficeUsers','<%=controlId%>_SelectedUsersected','<%=controlId%>_GroupValue')" value=">" class="btn">
								</td>
							</tr>
							<tr>
								<td>
									<input type="button" id="Btn_Rww" onclick="moverOffice('remove','<%=controlId%>_OfficeUsers','<%=controlId%>_SelectedUsersected','<%=controlId%>_GroupValue')" value="<" class="btn" >
								</td>
							</tr>
							<tr>
								<td>
									<input type="button" id="Btn_RwwAll" onclick="moverOffice('removeall','<%=controlId%>_OfficeUsers','<%=controlId%>_SelectedUsersected','<%=controlId%>_GroupValue')" value="<<" class="btn">
								</td>
							</tr>
						</table>
					</asp:TableCell>
					<asp:TableCell width="45%">
						<div class="divautoform">
							<twc:LocalizedLiteral text="CRMopptxt22" runat="server" /></div>
						<asp:ListBox id="SelectedUsersected" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple"
							ondblclick="document.getElementById('Btn_Rww').click()" EnableViewState="true" />
						<input type="hidden" id="GroupValue" runat="server">
					</asp:TableCell>
				</asp:TableRow>
			</asp:Table>
		</td>
	</tr>
</table>
