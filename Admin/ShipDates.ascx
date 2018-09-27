<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShipDates.ascx.cs" Inherits="Digita.Tustena.Admin.ShipDates" %>

<table cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<asp:datagrid id="dgValueEditor" Font-Size="10px" runat="server" ShowFooter="True" Forecolor="Black"
				GridLines="None" CellPadding="2" cellspacing="1" Backcolor="#aaaaaa" BorderWidth="1px" Bordercolor="#F2F2F2"
				Width="300px" AutoGenerateColumns="False" DataKeyField="id" OnItemDataBound="dgValueEditor_ItemDataBound" OnItemCommand="dgValueEditor_ItemCommand">
				<ItemStyle cssclass="GridItem"></ItemStyle>
				<AlternatingItemStyle cssclass="GridItemAltern"></AlternatingItemStyle>
				<HeaderStyle cssclass="GridTitle"></HeaderStyle>
				<FooterStyle Backcolor="#DDDDDD"></FooterStyle>
				<Columns>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:TextBox id="type" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
							<asp:Literal ID="rowid" Runat="server" Visible="False"></asp:Literal>
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox id="Newtype" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:CheckBox id="flagData" runat="server" Width="250px" CssClass="inputautoform"/>
						</ItemTemplate>
						<FooterTemplate>
							<asp:CheckBox id="NewflagData" runat="server" Width="250px" CssClass="inputautoform"/>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
					    <HeaderStyle HorizontalAlign=Center />
					    <ItemStyle HorizontalAlign=Center />
					    <FooterStyle HorizontalAlign=Center />
						<ItemTemplate>
							<asp:CheckBox Runat="server" ID="chkDelete"></asp:CheckBox>
						</ItemTemplate>
						<FooterTemplate>
							<asp:LinkButton Runat="server" CommandName="Add" ID="Linkbutton1" NAME="Linkbutton1" CssClass="save"></asp:LinkButton>
						</FooterTemplate>
					</asp:TemplateColumn>

				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td align="right" style="BORDER-TOP:black 1px solid; BACKGROUND-COLOR:#dddddd">
			<asp:LinkButton ID="btnSave" Runat="server" CssClass="save" OnClick="btnSave_Click"></asp:LinkButton>
		</td>
	</tr>
</table>
