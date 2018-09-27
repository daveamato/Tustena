<%@ Control Language="c#" AutoEventWireup="false" Codebehind="offices.ascx.cs" Inherits="Digita.Tustena.Admin.offices" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="98%" border="0">
	<tr>
		<td>
			<asp:Literal id="HelpLabel" runat="server" />
		</td>
	</tr>
	<tr>
		<td>
			<asp:DataGrid id="Categorie_Grid" width="70%" align="center" ShowFooter="True" runat="server"
				cssClass="GridItem" AutoGenerateColumns="false" CellPadding="4" OnItemCommand="CategoryGridItemCommand"
				OnItemDatabound="Categorie_Grid_DataBound">
				<HeaderStyle cssClass="GridTitle"></HeaderStyle>
				<SelectedItemStyle backcolor="#00C0C0"></SelectedItemStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemStyle cssClass="DataGridItem" />
						<ItemTemplate>
							<asp:Literal id="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
							<asp:Literal id="LnkName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Office") %>' />
						</ItemTemplate>
						<EditItemTemplate>
							<asp:Literal id="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
							<asp:TextBox id="LnkNameTextBox" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Office") %>' class="inputautoform" style="width:70%" />
						</EditItemTemplate>
						<FooterStyle cssClass="DataGridItem" />
						<FooterTemplate>
							<asp:TextBox id="TxtNewCatName" runat="server" class="inputautoform" style="width:70%" />&nbsp;
							<asp:LinkButton id="AddOffice" CommandName="Insert" runat="server" />
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:EditCommandColumn EditText='Edit' CancelText="Cancel" UpdateText="Update" ItemStyle-VerticalAlign="top"
						ItemStyle-CssClass="DataGridItem" FooterStyle-CssClass="DataGridItem" />
					<asp:TemplateColumn HeaderText="" ItemStyle-VerticalAlign="top">
						<ItemStyle cssClass="DataGridItem" />
						<ItemTemplate>
							<asp:LinkButton id="LnkDel" runat="server" Text="Delete" CommandName="Delete" CausesValidation="false"
								cssClass="normal" />
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</td>
	</tr>
</table>
