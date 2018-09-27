<%@ Control Language="c#" AutoEventWireup="false" Codebehind="EstimateList.ascx.cs" Inherits="Digita.Tustena.Estimates.EstimateList" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
				<asp:Repeater id="RepeaterSearch" runat="server">
					<HeaderTemplate>
						<table border="0" cellspacing="0" width="98%" align="center" class="normal">
							<tr>
								<td class="GridTitle" width="50%"><%=wrm.GetString("Esttxt2")%></td>
								<td class="GridTitle" width="39%"><%=wrm.GetString("Esttxt4")%></td>
								<td class="GridTitle" width="10%"><%=wrm.GetString("Esttxt3")%></td>
								<td class="GridTitle" width="1%">&nbsp;</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
							<tr>
								<td class="GridItem" width="50%"><a href="/estimates/EstimateHome.aspx?m=25&si=60&e=<%#DataBinder.Eval(Container.DataItem,"id")%>"><%#DataBinder.Eval(Container.DataItem,"Title")%></a></td>
								<td class="GridItem" width="39%"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
								<td class="GridItem" width="10%"><%#DataBinder.Eval(Container.DataItem,"EstimateDate","{0:d}")%></td>
								<td class="GridItem" width="1%" nowrap>
									<asp:Literal id="EstID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' visible=false/>
									<img src="/i/printer.gif"  border="0" onclick="CreateBox('/Estimates/EstimatePrint.aspx?render=no&e=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,600,600)" style="cursor:pointer;">
									<img src="/images/email.gif"  border="0" onclick="CreateBox('/Common/MailEstimate.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "Clientid")%>&e=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,300,300)" style="cursor:pointer;">
									<asp:LinkButton id="BtnModify" runat="server" CommandName="btnModify" cssClass="normal"/>
								</td>
							</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern" width="50%"><a href="/estimates/EstimateHome.aspx?m=25&si=60&e=<%#DataBinder.Eval(Container.DataItem,"id")%>"><%#DataBinder.Eval(Container.DataItem,"Title")%></a></td>
								<td class="GridItemAltern" width="39%"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
								<td class="GridItemAltern" width="10%"><%#DataBinder.Eval(Container.DataItem,"EstimateDate","{0:d}")%></td>
								<td class="GridItemAltern" width="1%" nowrap>
									<asp:Literal id="EstID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' visible=false/>
									<img src="/i/printer.gif"  border="0" onclick="CreateBox('/Estimates/EstimatePrint.aspx?render=no&e=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,600,600)" style="cursor:pointer;">
									<img src="/images/email.gif"  border="0" onclick="CreateBox('/Common/MailEstimate.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "Clientid")%>&e=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,300,300)" style="cursor:pointer;">
									<asp:LinkButton id="BtnModify" runat="server" CommandName="btnModify" cssClass="normal"/>
								</td>
							</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
				<center><asp:Label ID="RepeaterSearchInfo" Runat=server EnableViewState=False CssClass="divautoformRequired"></asp:Label></center>
