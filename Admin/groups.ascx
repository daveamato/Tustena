<%@ Control Language="c#" AutoEventWireup="false" Codebehind="groups.ascx.cs" Inherits="Digita.Tustena.Admin.groups"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<table width="98%" border="0">
	<tr>
		<td align="right" width="80%">
			<asp:LinkButton id="NewBtn" runat="server" class="save" onClick="NewBtnClick" />
		</td>
	</tr>
	<tr>
		<td width="80%">
			<asp:DataGrid id="Groups_Grid" width="80%" align="center" runat="server" cssClass="GridItem" AutoGenerateColumns="false"
				CellPadding="4" OnItemCommand="CategoryGridItemCommand" OnItemDataBound="Groups_Grid_ItemDataBound">
				<HeaderStyle cssClass="GridTitle"></HeaderStyle>
				<SelectedItemStyle backcolor="#00C0C0"></SelectedItemStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemStyle cssClass="DataGridItem" />
						<ItemTemplate>
							<asp:Literal id="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
							<asp:Literal id="LnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle cssClass="DataGridItem" />
						<ItemTemplate>
							<asp:Literal id="LtrDip" runat="server" />
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="" ItemStyle-VerticalAlign="top">
						<ItemStyle cssClass="DataGridItem" Wrap="False" />
						<ItemTemplate>
							<asp:LinkButton id="LnkEdt" runat="server" Text="Edit" CommandName="Edit" CausesValidation="false" />
							<asp:LinkButton id="LnkDel" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false" />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<p>
				<asp:Table id="Groups_Table" width="80%" align="center" runat="server" CellPadding="4">
					<asp:TableRow>
						<asp:TableCell colspan="3">
							<div class="divautoform"><twc:LocalizedLiteral Text="Agrtxt9" runat="server" /></div>
							<asp:TextBox id="GroupText" runat="server" class="inputautoform" />
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
						<asp:TableCell width="45%">
							<div class="divautoform"><twc:LocalizedLiteral Text="Agrtxt11" runat="server" /></div>
							<asp:ListBox id="ListGroups" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
						</asp:TableCell>
						<asp:TableCell align="center" width="10%">
							<table>
								<tr>
									<td>
										<asp:button id="Btn_FwwAll" onclick="Btn_FwwAll_Click" runat="server" text=">>|" Cssclass="btn"></asp:button>
									</td>
								</tr>
								<tr>
									<td>
										<asp:button id="Btn_Fww" onclick="Btn_Fww_Click" runat="server" text=">" Cssclass="btn"></asp:button>
									</td>
								</tr>
								<tr>
									<td>
										<asp:button id="Btn_Rww" onclick="Btn_Rww_Click" runat="server" text="<" Cssclass="btn"></asp:button>
									</td>
								</tr>
								<tr>
									<td>
										<asp:button id="Btn_RwwAll" onclick="Btn_RwwAll_Click" runat="server" text="|<<" Cssclass="btn"></asp:button>
									</td>
								</tr>
							</table>
						</asp:TableCell>
						<asp:TableCell width="45%">
							<div class="divautoform"><twc:LocalizedLiteral Text="Agrtxt5" runat="server" /></div>
							<asp:ListBox id="ListDip" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
						<asp:TableCell colspan="3" align="right">
							<asp:LinkButton id="Submit" runat="server" class="save" onClick="Submit_Click" />
						</asp:TableCell>
					</asp:TableRow>
					<asp:TableRow>
						<asp:TableCell colspan="3">
							<asp:Label id="LblError" runat="server" class="normal" />
						</asp:TableCell>
					</asp:TableRow>
				</asp:Table></p>
		</td>
	</tr>
</table>
