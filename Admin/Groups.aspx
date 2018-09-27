<%@ Page Language="c#" Trace="false" codebehind="Groups.aspx.cs" Inherits="Digita.Tustena.Admin_Groups" AutoEventWireup="false"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" cellspacing="0">
		<tr>
			<td width="140" class="SideBorderLinked" valign="top">
				<table width="100%" border="0">
					<tr>
						<td align="left" class="BorderBottomTitles">
							<span class="divautoform">
								<b>
									<twc:LocalizedLiteral Text="Agrtxt7" runat="server" id="LocalizedLiteral1" />
								</b>
							</span>
						</td>
					</tr>
					<tr>
						<td align="center">
							<asp:LinkButton id="NewBtn" runat="server" class="save"  />
						</td>
					</tr>
				</table>
			</td>
			<td valign="top">
				<table width="98%" border="0">
					<tr>
						<td align="left" class="BorderBottomTitles">
							<span class="divautoform">
								<b>
									<twc:LocalizedLiteral Text="Agrtxt7" runat="server" id="LocalizedLiteral2" />
								</b>
							</span>
							<br>
						</td>
					</tr>
					<tr>
						<td align="left">
							<asp:Literal id="HelpLabel" runat="server" />
						</td>
					</tr>
					<tr>
						<td align="left">
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
											<div class="divautoform">
												<twc:LocalizedLiteral Text="Agrtxt9" runat="server" />
												<asp:TextBox id="GroupText" runat="server" class="inputautoform" />
										</asp:TableCell>
									</asp:TableRow>
									<asp:TableRow>
										<asp:TableCell width="45%">
											<div class="divautoform">
												<twc:LocalizedLiteral Text="Agrtxt11" runat="server" />
												<asp:ListBox id="ListGroups" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
										</asp:TableCell>
										<asp:TableCell align="center" width="10%">
											<table>
												<tr>
													<td>
														<asp:button id="Btn_FwwAll"  runat="server" text=">>|" Cssclass="btn"></asp:button>
													</td>
												</tr>
												<tr>
													<td>
														<asp:button id="Btn_Fww"  runat="server" text=">" Cssclass="btn"></asp:button>
													</td>
												</tr>
												<tr>
													<td>
														<asp:button id="Btn_Rww"  runat="server" text="<" Cssclass="btn"></asp:button>
													</td>
												</tr>
												<tr>
													<td>
														<asp:button id="Btn_RwwAll"  runat="server" text="|<<" Cssclass="btn"></asp:button>
													</td>
												</tr>
											</table>
										</asp:TableCell>
										<asp:TableCell width="45%">
											<div class="divautoform">
												<twc:LocalizedLiteral Text="Agrtxt5" runat="server" />
												<asp:ListBox id="ListDip" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
										</asp:TableCell>
									</asp:TableRow>
									<asp:TableRow>
										<asp:TableCell colspan="3" align="right">
											<asp:LinkButton id="Submit" runat="server" class="save"  />
										</asp:TableCell>
									</asp:TableRow>
									<asp:TableRow>
										<asp:TableCell colspan="3">
											<asp:Label id="LblError" runat="server" class="normal" />
										</asp:TableCell>
									</asp:TableRow>
								</asp:Table>
							</p>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</form>

</body>
</html>
