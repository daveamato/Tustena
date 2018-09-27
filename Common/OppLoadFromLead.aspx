<%@ Register TagPrefix="src" TagName="SearchLead" Src="~/common/SearchLead.ascx" %>
<%@ Page language="c#" Codebehind="OppLoadFromLead.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.OppLoadFromLead" %>
<html>
<head id="head" runat="server">
<script type="text/javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
			<table width="100%" cellspacing="0" cellpadding="0">
				<tr>

					<td valign="top">
						<table class="tblstruct">
							<tr>
								<td align="left" class="BorderBottomTitles">
									<span class="divautoform">
										<b>
											<%=wrm.GetString("CRMopptxt1")%>
											:
											<asp:Literal Runat="server" ID="OppName"></asp:Literal>
											<asp:Literal Runat="server" ID="OppID" visible=false></asp:Literal>
										</b>
									</span>
								</td>
							</tr>
						</table>
						<table id="TblSrcLead" runat="server" border="0" cellpadding="3" cellspacing="0" width="70%"
							class="normal" align="left">
							<tr>
								<td style="padding-left:20px">
									<src:SearchLead id="SrcLead" runat="server"></src:SearchLead>
								</td>
							</tr>
							<tr>
								<td align="right" style="PADDING-TOP:10px">
									<asp:LinkButton id="SrcLeadbtn" runat="server" cssClass="Save" />
								</td>
							</tr>
						</table>


						<asp:Repeater id="Repeater1" runat="server">
							<HeaderTemplate>
								<table border="0" cellpadding="2" cellspacing="1" width="98%" align="center">
								<tr>
									<td class="GridTitle" colspan=5>
										<asp:LinkButton id="SelectAll" CommandName="SelectAll" runat="server" cssClass="Save"/>
										<asp:LinkButton id="InsertOpp" CommandName="InsertOpp" runat="server" cssClass="Save"/>
									</td>
									<td class="GridTitle" align="right">
											<asp:LinkButton id="ViewSearch" CommandName="ViewSearch" runat="server" cssClass="Save"/>
									</td>
								</tr>
								<tr>
									<td class="GridTitle" width="1%">&nbsp;</td>
									<td class="GridTitle"><%=wrm.GetString("Reftxt9")%></td>
									<td class="GridTitle"><%=wrm.GetString("Reftxt10")%></td>
									<td class="GridTitle"><%=wrm.GetString("Reftxt11")%></td>
									<td class="GridTitle"><%=wrm.GetString("Reftxt12")%></td>
									<td class="GridTitle"><%=wrm.GetString("Ledtxt29")%></td>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem" width="1%">
										<asp:CheckBox ID="ChkForInsert" Runat=server></asp:CheckBox>
									</td>
									<td class="GridItem">
									<asp:Literal runat="server" id="ID" visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'/>
									<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>
									</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>&nbsp;</td>
									<td class="GridItem"><%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>&nbsp;</td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="GridItemAltern" width="1%">
										<asp:CheckBox ID="ChkForInsert" Runat=server></asp:CheckBox>
									</td>
									<td class="GridItemAltern">
									<asp:Literal runat="server" id="ID" visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'/>
									<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>
									</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"Phone")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>&nbsp;</td>
									<td class="GridItemAltern"><%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>&nbsp;</td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>

					</td>
				</tr>
			</table>
		</form>


</body>
</html>
