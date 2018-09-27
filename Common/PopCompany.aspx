<%@ Page Language="c#" Trace="false" codebehind="PopCompany.aspx.cs" Inherits="Digita.Tustena.PopAziende" AutoEventWireup="false"%>
<HTML>
	<head runat=server>
		<title>:: Tustena ::</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
			<script language="javascript" src="/js/common.js"></script>
	</HEAD>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0">
		<form runat="server">
			<table id="ReSearchSimple" runat="server" width="98%" border="0" cellspacing="0" align="center">
				<tr>
					<td colspan="2" class="normal">
						<%=wrm.GetString("Paztxt3")%>
					</td>
				</tr>
				<tr>
					<td class="normal" valign="top">
						<asp:RadioButtonList id="RadioList1" runat="server" cssClass="normal"></asp:RadioButtonList>
					</td>
					<td class="normal" valign="top">
						<%=wrm.GetString("Paztxt6")%>
						<asp:RadioButtonList id="NRes" runat="server" cssClass="normal">
							<asp:ListItem Value="10" selected="true">10</asp:ListItem>
							<asp:ListItem Value="20">20</asp:ListItem>
							<asp:ListItem Value="50">50</asp:ListItem>
							<asp:ListItem Value="100">100</asp:ListItem>
						</asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<table>
							<tr>
								<td align="left" width="50%">
									<asp:TextBox id="FindIt" autoclick="BtnFind" runat="server" class="BoxDesign" />
								</td>
								<td align="left" nowrap>
									<asp:LinkButton id="BtnFind" runat="server" class="save" />
									&nbsp;
									<asp:LinkButton id="ViewAdvanced" runat="server" class="save" />
									&nbsp;
									<asp:LinkButton id="NewCompany" runat="server" class="save" />
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<asp:Repeater id="CompaniesRep" runat="server">
				<HeaderTemplate>
					<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
						<tr>
							<td class="GridTitle"><%=wrm.GetString("Paztxt1")%></td>
							<td class="GridTitle"><%=wrm.GetString("Paztxt7")%></td>
							<td class="GridTitle"><%=wrm.GetString("Paztxt8")%></td>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td class="GridItem"><span id="CompanyRep" runat="server" class="linked" />&nbsp;</td>
						<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "phone")%></td>
						<td class="GridItem LastItem"><%# DataBinder.Eval(Container.DataItem, "email")%></td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr>
						<td class="GridItemAltern"><span id="CompanyRep" runat="server" class="linked" />&nbsp;</td>
						<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "phone")%></td>
						<td class="GridItemAltern LastItem"><%# DataBinder.Eval(Container.DataItem, "email")%></td>
					</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:Repeater>
			<table id="NewCompanyTable" runat="server">
				<tr>
					<td align="left" class="BorderBottomTitles">
						<span class="divautoform">
							<b>
								<%=wrm.GetString("Bcotxt3")%>
							</b>
						</span>
					</td>
				</tr>
				<tr>
					<td>
						<div class="divautoform"><%=wrm.GetString("CRMcontxt3")%></div>
						<asp:TextBox Id="RapRagSoc" runat="server" Cssclass="Inputautoform" Height="20" />
					</td>
				</tr>
				<tr>
					<td>
						<div class="divautoform"><%=wrm.GetString("CRMcontxt4")%></div>
						<asp:TextBox Id="RapPhone" runat="server" Cssclass="Inputautoform" Height="20" />
					</td>
				</tr>
				<tr>
					<td>
						<div class="divautoform"><%=wrm.GetString("CRMcontxt5")%></div>
						<asp:TextBox Id="RapEmail" runat="server" Cssclass="Inputautoform" Height="20" />
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:LinkButton Id="RapSubmit" runat="server" Cssclass="save">OK</asp:LinkButton>
					</td>
				</tr>
			</table>
			<table id="ReSearchAdvanced" runat="server" class="tblstruct normal">
				<tr>
					<td colspan="3" align="center">
						<b>
							<%=wrm.GetString("CRMcontxt55")%>
						</b>
						<br>
					</td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt17")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_CompanyName" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt26")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Address" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt27")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_City" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt28")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_State" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt29")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Zip" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt20")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Phone" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt21")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Fax" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt22")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Email" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt23")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Site" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("Bcotxt11")%></td>
					<td width="20%" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
					<td width="50%"><asp:TextBox id="Advanced_Code" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt8")%></td>
					<td width="20%" align="center">&nbsp;</td>
					<td width="50%"><asp:DropDownList old="true" id="Advanced_CompanyType" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt9")%></td>
					<td width="20%" align="center">&nbsp;</td>
					<td width="50%"><asp:DropDownList old="true" id="Advanced_ContactType" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt10")%></td>
					<td width="20%" align="center">
						<asp:RadioButtonList id="Advanced_BillCheck" runat="server" RepeatDirection="Horizontal" class="normal">
							<asp:ListItem Value="1">=</asp:ListItem>
							<asp:ListItem Value="2">&lt;</asp:ListItem>
							<asp:ListItem Value="3">&gt;</asp:ListItem>
						</asp:RadioButtonList>
					</td>
					<td width="50%"><asp:TextBox id="Advanced_Billed" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt11")%></td>
					<td width="20%" align="center">
						<asp:RadioButtonList id="Advanced_EmployeesCheck" runat="server" RepeatDirection="Horizontal" class="normal">
							<asp:ListItem Value="1">=</asp:ListItem>
							<asp:ListItem Value="2">&lt;</asp:ListItem>
							<asp:ListItem Value="3">&gt;</asp:ListItem>
						</asp:RadioButtonList>
					</td>
					<td width="50%"><asp:TextBox id="Advanced_Employees" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt12")%></td>
					<td width="20%" align="center">&nbsp;</td>
					<td width="50%"><asp:DropDownList old="true" id="Advanced_Estimate" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td width="30%"><%=wrm.GetString("CRMcontxt45")%></td>
					<td width="20%" align="center">&nbsp;</td>
					<td width="50%"><asp:DropDownList old="true" id="Advanced_Category" runat="server" class="BoxDesign" /></td>
				</tr>
				<tr>
					<td colspan="3" align="right">
						<asp:LinkButton id="SearchAdvanced" runat="server" Text="OK" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
