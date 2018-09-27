<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Page language="c#" Trace="false" Codebehind="Limbo.aspx.cs" Inherits="Digita.Tustena.Admin.Limbo"  AutoEventWireup="false"%>
<html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellspacing="0">
		<tr>
			<td width="140" class="SideBorderLinked" valign="top">
				<table width="98%" border="0" cellspacing="0" cellpadding=0>
					<tr>
                   		<td class="sideContainer">
						<div class="sideTitle"><twc:LocalizedLiteral Text="Options" runat="server" id="LocalizedLiteral1" /></div>
						<asp:LinkButton id="BtnCompany" runat="server" cssClass="sidebtn"  />
						<asp:LinkButton id="BtnContact" runat="server" cssClass="sidebtn"  />
						<asp:LinkButton id="BtnLead" runat="server" cssClass="sidebtn"  />
						</td>
					</tr>
				</table>
			</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
								<twc:LocalizedLiteral Text="Limtxt1" runat="server" id="LocalizedLiteral2" />
						</td>
					</tr>
				</table>
				<br>
				<span id="CompanyContainer" runat="server">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<asp:Repeater id="Repeater1" runat="server">
							<HeaderTemplate>
								<tr>
									<td class="GridTitle" width="39%">
										<twc:LocalizedLiteral Text="Bcotxt10" runat="server" /></td>
									<td class="GridTitle" width="10%">
										<twc:LocalizedLiteral Text="Bcotxt12" runat="server" /></td>
									<td class="GridTitle" width="10%">
										<twc:LocalizedLiteral Text="Bcotxt21" runat="server" /></td>
									<td class="GridTitle" width="1%">&nbsp;</td>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
									<td class="GridItem" nowrap><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItem" nowrap><%#DataBinder.Eval(Container.DataItem,"Fax")%>&nbsp;</td>
									<td class="GridItem Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save" />
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save" />
										<asp:Literal id="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
									<td class="GridItemAltern" nowrap><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItemAltern" nowrap><%#DataBinder.Eval(Container.DataItem,"Fax")%>&nbsp;</td>
									<td class="GridItemAltern Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save" />
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save" />
										<asp:Literal id="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate></FooterTemplate>
						</asp:Repeater>
					</table>
					<Pag:RepeaterPaging id="Repeaterpaging1" visible="false" runat="server" />
					<asp:Label id="RepeaterInfo" runat="server" visible="false" cssClass="divautoformRequired" />

				</span>
				<span id="ContactContainer" runat="server">
							<table class="tblstruct">
						<asp:Repeater id="Repeater2" runat="server">
							<HeaderTemplate>
								<tr>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt9" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt10" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt11" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt12" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt13" runat="server" /></td>
									<td class="GridTitle" width="1%">&nbsp;</td>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%></td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"CompanyName2")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"Phone_1")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"NameOwner")%>&nbsp;</td>
									<td class="GridItem Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save" />
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save" />
										<asp:Literal id="IDCon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name") %></td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"CompanyName2")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"Phone_1")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"NameOwner")%>&nbsp;</td>
									<td class="GridItemAltern Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save" />
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save" />
										<asp:Literal id="IDCon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate></FooterTemplate>
						</asp:Repeater>
					</table>
					<Pag:RepeaterPaging id="Repeaterpaging2" visible="false" runat="server" />

							<asp:Label id="RepeaterInfoC" runat="server" visible="false" Class="divautoformRequired" />
						 	</span>
				<span id="LeadContainer" runat="server">
					<table class="tblstruct">
						<asp:Repeater id="Repeater3" runat="server">
							<HeaderTemplate>
								<tr>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt9" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt10" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt11" runat="server" /></td>
									<td class="GridTitle">
										<twc:LocalizedLiteral Text="Reftxt12" runat="server" /></td>
									<td class="GridTitle" width="1%">&nbsp;</td>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%></td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>&nbsp;</td>
									<td class="GridItem Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save" />
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save" />
										<asp:Literal id="IDCon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name") %></td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>&nbsp;</td>
									<td class="GridItemAltern Lcit" align="CENTER" nowrap>
										<asp:LinkButton id="Revert" Runat="server" CommandName="Revert" cssClass="Save"/>
										<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save"/>
										<asp:Literal id="IDCon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
									</td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate></FooterTemplate>
						</asp:Repeater>
					</table>
					<Pag:RepeaterPaging id="Repeater3Paging" visible="false" runat="server" />
				</span>
			</td>
		</tr>
	</table>
</form>

</body>
</html>
