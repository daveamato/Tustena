<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page Language="c#" trace="false" codebehind="CRM_ToDoList.aspx.cs" Inherits="Digita.Tustena.CRMToDoList"  AutoEventWireup="false"%>
<html>
<head id="head" runat="server">
<script type="text/javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" height="100%" cellspacing="3" cellpadding="0" align="center">
		<TBODY>
			<tr>
				<td width="140" height="100%" class="SideBorderLinked" valign="top">
					<table width="100%" border="0">
						<tr>
							<td align="left" class="BorderBottomTitles">
								<span class="divautoform">
									<b>
										<%=wrm.GetString("Tdltxt1")%>
										<br>
									</b>
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<asp:LinkButton id="NewTask" runat="server" class="normal"  />
							</td>
						</tr>
						<tr>
							<td align="left" class="BorderBottomTitles">
								<span class="divautoform">
									<b>
										<%=wrm.GetString("Acttxt30")%>
									</b>
								</span>
							</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox Id="FindTxt" runat="server" class="BoxDesign" Height="20" />
							</td>
						</tr>
						<tr>
							<td align="right" class="BorderBottomTitles">
								<asp:LinkButton Id="BtnSearch" runat="server" class="HeaderContacts"  />
							</td>
						</tr>
					</table>
				</td>
				<td valign="top">
					<table width="100%" border="0">
						<tr>
							<td align="left" class="BorderBottomTitles">
								<span class="divautoform">
									<b>
										<%=wrm.GetString("Tdltxt1")%>
										<br>
									</b>
								</span>
							</td>
						</tr>
					</table>
					<asp:Repeater id="RepTask" runat="server">
						<HeaderTemplate>
							<table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
								<tr>
									<td class="GridTitle" width="1">&nbsp;</td>
									<td class="GridTitle" width="15%"><%=wrm.GetString("CRMTodtxt1")%></td>
									<td class="GridTitle" width="70%"><%=wrm.GetString("CRMTodtxt3")%></td>
									<td class="GridTitle" width="10%"><%=wrm.GetString("CRMTodtxt5")%></td>
									<td class="GridTitle" width="5%">&nbsp;</td>
								</tr>
						</HeaderTemplate>
						<ItemTemplate>
							<tr>
								<td class="GridItem">
									<asp:ImageButton id="Check" runat="server" ImageUrl="/i/checkoff.gif" CommandName="CheckEseguito" />
								</td>
								<td class="GridItem" width="15%"><%# DataBinder.Eval(Container.DataItem, "CreatedByName")%>&nbsp;</td>
								<td class="GridItem" width="25%"><%# DataBinder.Eval(Container.DataItem, "Task")%>&nbsp;</td>
								<td class="GridItem" width="10%"><%# DataBinder.Eval(Container.DataItem, "ExpirationDate", "{0:d}")%>&nbsp;</td>
								<td class="GridItem" width="5%">
									<asp:Literal id="TaskID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'/>
									<asp:LinkButton id="ModTask" runat="server" class="normal" CommandName="ModTask" />
									<asp:LinkButton id="DelTask" runat="server" class="normal" CommandName="DelTask" />
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern">
									<asp:ImageButton id="Check" runat="server" ImageUrl="/i/checkoff.gif" CommandName="CheckEseguito" />
								</td>
								<td class="GridItemAltern" width="15%"><%# DataBinder.Eval(Container.DataItem, "CreatedByName")%>&nbsp;</td>
								<td class="GridItemAltern" width="25%"><%# DataBinder.Eval(Container.DataItem, "Task")%>&nbsp;</td>
								<td class="GridItemAltern" width="10%"><%# DataBinder.Eval(Container.DataItem, "ExpirationDate", "{0:d}")%>&nbsp;</td>
								<td class="GridItemAltern" width="5%">
									<asp:Literal id="TaskID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'/>
									<asp:LinkButton id="ModTask" runat="server" class="normal" CommandName="ModTask" />
									<asp:LinkButton id="DelTask" runat="server" class="normal" CommandName="DelTask" />
								</td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
	</table>
	</FooterTemplate> </asp:Repeater>
	<asp:Label id="RepeaterInfo" runat="server" visible="false" Class="divautoformRequired" />
	<table width="100%" border="0">
		<tr>
			<td align="left" class="BorderBottomTitles">
				<span class="divautoform">
					<b>
						<%=wrm.GetString("Tdltxt14")%>
						<br>
					</b>
				</span>
			</td>
		</tr>
	</table>
	<asp:Repeater id="RepTask2" runat="server">
		<HeaderTemplate>
			<table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
				<tr>
					<td class="GridTitle" width="1">&nbsp;</td>
					<td class="GridTitle" width="15%"><%=wrm.GetString("CRMTodtxt2")%></td>
					<td class="GridTitle" width="35%"><%=wrm.GetString("CRMTodtxt3")%></td>
					<td class="GridTitle" width="30%"><%=wrm.GetString("CRMTodtxt4")%></td>
					<td class="GridTitle" width="10%"><%=wrm.GetString("CRMTodtxt5")%></td>
					<td class="GridTitle" width="5%">&nbsp;</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td class="GridItem">
					<asp:ImageButton id="Check" runat="server" ImageUrl="/i/checkoff.gif" CommandName="CheckEseguito" />
				</td>
				<td class="GridItem" width="15%"><%# DataBinder.Eval(Container.DataItem, "OwnerName")%>&nbsp;</td>
				<td class="GridItem" width="35%"><%# DataBinder.Eval(Container.DataItem, "Task")%>&nbsp;</td>
				<td class="GridItem" width="30%"><%# DataBinder.Eval(Container.DataItem, "Outcome")%>&nbsp;</td>
				<td class="GridItem" width="10%"><%# DataBinder.Eval(Container.DataItem, "ExpirationDate", "{0:d}")%>&nbsp;</td>
				<td class="GridItem" width="5%">
					<asp:Literal id="TaskID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'/>
					<asp:LinkButton id="ModTask" runat="server" class="normal" CommandName="ModTask" />
					<asp:LinkButton id="DelTask" runat="server" class="normal" CommandName="DelTask" />
				</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr>
				<td class="GridItemAltern">
					<asp:ImageButton id="Check" runat="server" ImageUrl="/i/checkoff.gif" CommandName="CheckEseguito" />
				</td>
				<td class="GridItemAltern" width="15%"><%# DataBinder.Eval(Container.DataItem, "OwnerName")%>&nbsp;</td>
				<td class="GridItemAltern" width="25%"><%# DataBinder.Eval(Container.DataItem, "Task")%>&nbsp;</td>
				<td class="GridItemAltern" width="25%"><%# DataBinder.Eval(Container.DataItem, "Outcome")%>&nbsp;</td>
				<td class="GridItemAltern" width="10%"><%# DataBinder.Eval(Container.DataItem, "ExpirationDate", "{0:d}")%>&nbsp;</td>
				<td class="GridItemAltern" width="5%">
					<asp:Literal id="TaskID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'/>
					<asp:LinkButton id="ModTask" runat="server" class="normal" CommandName="ModTask" />
					<asp:LinkButton id="DelTask" runat="server" class="normal" CommandName="DelTask" />
				</td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
						</table>
						</FooterTemplate>
	</asp:Repeater>
	<asp:Label id="RepeaterInfo2" runat="server" visible="false" Class="divautoformRequired" />
	<table id="TaskCard" runat="server" width="70%" border="0" cellspacing="0" cellpadding="0"
		align="center">
		<tr>
			<td width="50%" valign="top">
				<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal">
					<tr>
						<td width="30%">
							<%=wrm.GetString("CRMTodtxt7")%>
							<domval:RequiredDomValidator id="OwnerValidator" EnableClientScript="true" Display="Dynamic" runat="server" ControlToValidate="ToDoList_Owner"
								ErrorMessage="*" />
							<asp:TextBox Id="ToDoList_OwnerID" runat="server" class="BoxDesign" style="DISPLAY:none" />
						</td>
						<td width="70%">
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td>
										<asp:TextBox Id="ToDoList_Owner" runat="server" class="BoxDesign" ReadOnly="true" />
									</td>
									<td width="30">
										<div id="PopAccount" runat="server">
											&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopAccount.aspx?render=no&textbox=ToDoList_Owner&textbox2=ToDoList_OwnerID',event)">
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="30%">
							<%=wrm.GetString("CRMTodtxt5")%>
							<asp:CompareValidator id="CvDateFine" runat="Server" Operator="DataTypeCheck" Type="Date" ErrorMessage="*"
								ControlToValidate="ToDoList_ExpirationDAte" />
							<domval:RequiredDomValidator id="ExpirationDateValidator" EnableClientScript="true" Display="Dynamic" runat="server"
								ControlToValidate="ToDoList_ExpirationDate" ErrorMessage="*" />
						</td>
						<td width="70%">
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td>
										<asp:TextBox id="ToDoList_ExpirationDate" runat="server" class="BoxDesign" maxlength="10"
											ReadOnly="true" />
									</td>
									<td width="30">
										<div id="PopAccount4" runat="server">
											&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=ToDoList_ExpirationDate',event,195,195)">
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
			<td width="50%" valign="top">
				<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal">
					<tr>
						<td width="30%">
							<%=wrm.GetString("Acttxt7")%>
							<asp:TextBox Id="ToDoList_CompanyID" runat="server" class="BoxDesign" style="DISPLAY:none" />
						</td>
						<td width="70%">
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td>
										<asp:TextBox Id="ToDoList_CompanyName" runat="server" class="BoxDesign" ReadOnly="true" />
									</td>
									<td width="30">
										<div id="PopAccount2" runat="server">
											&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=ToDoList_CompanyName&textbox2=ToDoList_CompanyID',event,500,400)">
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="30%">
							<%=wrm.GetString("Acttxt31")%>
							<div style="DISPLAY:none"><asp:TextBox Id="ToDoList_OpportunityID" runat="server" class="BoxDesign" /></div>
						</td>
						<td width="70%">
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td>
										<asp:Label id="LinkOpportunity" runat="server" class="BoxDesignView" visible="false" />
										<asp:TextBox Id="ToDoList_OpportunityTitle" runat="server" class="BoxDesign" ReadOnly="true" />
									</td>
									<td width="30">
										<div id="PopAccount3" runat="server">
											&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopOpportunity.aspx?render=no&textbox=ToDoList_OpportunityTitle&textboxID=ToDoList_OpportunityID',event,400,200)">
										</div>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal">
					<tr>
						<td>
							<%=wrm.GetString("CRMTodtxt3")%>
						</td>
					</tr>
					<tr>
						<td>
							<asp:TextBox Id="ToDoList_Task" runat="server" TextMode="MultiLine" height="50" class="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td>
							<%=wrm.GetString("CRMTodtxt4")%>
						</td>
					</tr>
					<tr>
						<td>
							<asp:TextBox Id="ToDoList_Outcome" runat="server" TextMode="MultiLine" height="50" class="BoxDesign" />
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td colspan="2" align="right">
				<asp:Literal id="TaskID" runat="server" visible="false" />
				<asp:LinkButton id="SaveTask" runat="server" class="normal"  />
			</td>
		</tr>
	</table>
	</TD></TR></TBODY></TABLE>
</form>

</body>
</html>
