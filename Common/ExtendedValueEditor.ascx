<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ExtendedValueEditor.ascx.cs" Inherits="Digita.Tustena.Common.ExtendedValueEditor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<table cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<asp:datagrid id="dgValueEditor" Font-Size="10px" runat="server" ShowFooter="True" Forecolor="Black"
				GridLines="None" CellPadding="2" cellspacing="1" Backcolor="#aaaaaa" BorderWidth="1px" Bordercolor="#F2F2F2"
				Width="300px" AutoGenerateColumns="False" DataKeyField="id">
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
							<asp:TextBox id="type2" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
						</ItemTemplate>
						<FooterTemplate>
							<asp:TextBox id="Newtype2" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:DropDownList ID="MyUICulture" Runat="server" Width="50px" CssClass="BoxDesign"></asp:DropDownList>
						</ItemTemplate>
						<FooterTemplate>
							<asp:DropDownList ID="NewMyUICulture" Runat="server" Width="50px" CssClass="BoxDesign"></asp:DropDownList>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<div align="center">
								<asp:TextBox id="vieworder" runat="server" Width="30px" MaxLength=3 CssClass="inputautoform"></asp:TextBox></div>
						</ItemTemplate>
						<FooterTemplate>
							<div align="center">
								<asp:TextBox id="newvieworder" runat="server" Width="30px" MaxLength=3 CssClass="inputautoform"></asp:TextBox></div>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:CheckBox Runat="server" ID="chkDelete"></asp:CheckBox>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Center"></FooterStyle>
						<FooterTemplate>
							<asp:LinkButton Runat="server" CommandName="Add" ID="Linkbutton1" NAME="Linkbutton1" CssClass="save"></asp:LinkButton>
						</FooterTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemTemplate>
							<asp:datagrid id="OtherLangDatagrid" Font-Size="10px" runat="server" ShowFooter="True" Forecolor="Black"
								GridLines="None" CellPadding="2" cellspacing="1" Backcolor="#aaaaaa" BorderWidth="1px" Bordercolor="#F2F2F2"
								Width="300px" AutoGenerateColumns="False" DataKeyField="id">
								<ItemStyle cssclass="GridItem"></ItemStyle>
								<AlternatingItemStyle cssclass="GridItemAltern"></AlternatingItemStyle>
								<HeaderStyle cssclass="GridTitle"></HeaderStyle>
								<FooterStyle Backcolor="#DDDDDD"></FooterStyle>
								<Columns>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:TextBox id="LangType" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
											<asp:Literal ID="LangRowId" Runat="server" Visible="False"></asp:Literal>
											<asp:Literal ID="RealRowId" Runat="server" Visible="False"></asp:Literal>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="NewLangType" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:TextBox id="LangType2" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
										</ItemTemplate>
										<FooterTemplate>
											<asp:TextBox id="NewLangType2" runat="server" Width="250px" CssClass="inputautoform"></asp:TextBox>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<asp:DropDownList ID="LangMyUICulture" Runat="server" Width="50px" CssClass="BoxDesign"></asp:DropDownList>
										</ItemTemplate>
										<FooterTemplate>
											<asp:DropDownList ID="NewLangMyUICulture" Runat="server" Width="50px" CssClass="BoxDesign"></asp:DropDownList>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemTemplate>
											<div align="center">
												<asp:TextBox id="LangViewOrder" runat="server" Width="30px" MaxLength=3 CssClass="inputautoform"></asp:TextBox></div>
										</ItemTemplate>
										<FooterTemplate>
											<div align="center">
												<asp:TextBox id="NewLangViewOrder" runat="server" Width="30px"  MaxLength=3 CssClass="inputautoform"></asp:TextBox></div>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:CheckBox Runat="server" ID="LangChkDelete"></asp:CheckBox>
										</ItemTemplate>
										<FooterStyle HorizontalAlign="Center"></FooterStyle>
										<FooterTemplate>
											<asp:LinkButton Runat="server" CommandName="Add" ID="Linkbutton2" NAME="Linkbutton1" CssClass="save"></asp:LinkButton>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</ItemTemplate>
						<FooterTemplate>
							&nbsp;
						</FooterTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid>
		</td>
	</tr>
	<tr>
		<td align="right" style="BORDER-TOP:black 1px solid; BACKGROUND-COLOR:#dddddd">
			<asp:LinkButton ID="updBoatType" Runat="server" CssClass="save"></asp:LinkButton>
		</td>
	</tr>
</table>
