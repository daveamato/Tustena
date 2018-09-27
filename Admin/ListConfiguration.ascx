<%@ Control Language="c#" AutoEventWireup="true" Codebehind="ListConfiguration.ascx.cs" Inherits="Digita.Tustena.Admin.ListConfiguration" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<table width="100%" border="0" cellspacing="0" cellpadding="0">
	<tr>
		<td width="50%">
			<table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="left">
				<asp:Repeater ID="ListElement" runat="server">
					<HeaderTemplate>
						<tr>
							<td class="GridTitle">
								<twc:LocalizedLiteral Text="Listxt5" runat="server" ID="Localizedliteral1" /></td>
							<td class="GridTitle" width="1%">
								<twc:LocalizedLiteral Text="Listxt8" runat="server" ID="Localizedliteral3" /></td>
							<td class="GridTitle" width="10%">
								<asp:LinkButton id="NewElement" CommandName="New" runat="server" CssClass="normal" />
							</td>
						</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="GridItem">
								<asp:Literal id="OpenEl" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>'/>
							</td>
							<td class="GridItem">
								<asp:Literal id="LangLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>'/>
							</td>
							<td class="GridItem" width="10%" nowrap>
								<asp:Literal id="litInfo" runat="server"></asp:Literal>
								<asp:LinkButton id="ModElement" CommandName="Modify" runat="server" CssClass="normal" />
								<asp:LinkButton id="DeleteElement" CommandName="Delete" runat="server" CssClass="normal" />
								<asp:LinkButton id="Linkbutton1" CommandName="NewElementLang" runat="server" CssClass="normal" />
								<asp:Literal id="IdElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
								<asp:Literal id="KElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>'/>
							</td>
						</tr>
						<asp:Repeater ID="ListElement" Runat="server" OnItemCommand="ElementsListCommand" OnItemDataBound="ElementsListDatabound" DataSource='<%# getOtherLanguage( (int)DataBinder.Eval(Container.DataItem, "ID") ) %>' >
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem">
										&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Literal id="OpenEl" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>'/>
									</td>
									<td class="GridItem">
										<asp:Literal id="LangLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>'/>
									</td>
									<td class="GridItem" width="10%" nowrap>
										<asp:Literal id="litInfo" runat="server"></asp:Literal>
										<asp:LinkButton id="ModElement" CommandName="Modify" runat="server" CssClass="normal" />
										<asp:LinkButton id="DeleteElement" CommandName="Delete" runat="server" CssClass="normal" />
										<asp:Literal id="IdElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
										<asp:Literal id="KElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>'/>
									</td>
								</tr>
								<tr>
							</ItemTemplate>
							<FooterTemplate>
							</FooterTemplate>
						</asp:Repeater>
					</ItemTemplate>
					<AlternatingItemTemplate>
						<tr>
							<td class="GridItemAltern">
								<asp:Literal id="OpenEl" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>'/>
							</td>
							<td class="GridItemAltern">
								<asp:Literal id="LangLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>'/>
							</td>
							<td class="GridItemAltern" width="10%" nowrap>
								<asp:Literal id="litInfo" runat="server"></asp:Literal>
								<asp:LinkButton id="ModElement" CommandName="Modify" runat="server" CssClass="normal" />
								<asp:LinkButton id="DeleteElement" CommandName="Delete" runat="server" CssClass="normal" />
								<asp:LinkButton id="NewElement" CommandName="NewElementLang" runat="server" CssClass="normal" />
								<asp:Literal id="IdElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
								<asp:Literal id="KElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>'/>
							</td>
						</tr>
						<asp:Repeater ID="ListElement" Runat="server" OnItemCommand="ElementsListCommand" OnItemDataBound="ElementsListDatabound" DataSource='<%# getOtherLanguage( (int)DataBinder.Eval(Container.DataItem, "ID") ) %>' >
							<HeaderTemplate>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem">
										&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:Literal id="OpenEl" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ListItem")%>'/>
									</td>
									<td class="GridItemAltern">
										<asp:Literal id="LangLabel" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "lang")%>'/>
									</td>
									<td class="GridItemAltern" width="10%" nowrap>
										<asp:Literal id="litInfo" runat="server"></asp:Literal>
										<asp:LinkButton id="ModElement" CommandName="Modify" runat="server" CssClass="normal" />
										<asp:LinkButton id="DeleteElement" CommandName="Delete" runat="server" CssClass="normal" />
										<asp:Literal id="IdElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
										<asp:Literal id="KElement" runat="server" Visible=False text='<%# DataBinder.Eval(Container.DataItem, "k_id")%>'/>
									</td>
								</tr>
								<tr>
							</ItemTemplate>
							<FooterTemplate>
							</FooterTemplate>
						</asp:Repeater>
					</AlternatingItemTemplate>
					<FooterTemplate>
					</FooterTemplate>
				</asp:Repeater>
			</table>
		</td>
		<td valign="top" width="50%">
			<table id="Form" runat="server" border="0" cellpadding="3" cellspacing="0" width="98%"
				class="normal" align="center">
				<tr>
					<td>
						<div class="normal"><twc:LocalizedLiteral Text="Listxt5" runat="server" id="Localizedliteral4" /></div>
						<asp:TextBox ID="ElementDescription" Runat="server" CssClass="BoxDesign" MaxLength="49"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<asp:DropDownList id="MyUICulture" runat="server" />
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:LinkButton ID="SubmitElement" Runat="server" CssClass="Save" Text="OK"></asp:LinkButton>
						<asp:Literal ID="IDSelectElement" Runat="server" Visible="False"></asp:Literal>
						<asp:Literal ID="ElementForm" Runat="server" Visible="False"></asp:Literal>
						<asp:Literal ID="ElementCamp" Runat="server" Visible="False"></asp:Literal>
						<asp:Literal ID="KElement" Runat="server" Visible="False"></asp:Literal>
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
