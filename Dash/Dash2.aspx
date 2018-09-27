<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Page trace="false" language="c#" Codebehind="Dash2.aspx.cs" Inherits="Digita.Tustena.Dash.Dash2"  AutoEventWireup="false"%>
	<html>
<head id="head" runat="server">
<script language="javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0">
  <TBODY>
		<tr>
<td width="140" height="100%" class="SideBorderLinked" valign="top">
	<table width="98%" border="0" cellspacing="0" align="center" cellpadding=0>
	<tr>
		<td class="sideContainer">
					<div class="sideTitle"><%=wrm.GetString("Dastxt1")%></div>
					<asp:LinkButton id="ViewTableData" runat="server" cssClass="sidebtn"/>
						</td>
					</tr>
				</table>
			</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
                        <%=wrm.GetString("Dastxt5")%>
						</td>
					</tr>
				</table>
				<table cellpadding=0 cellspacing=0 border=0 class="normal" width="95%" align=center>
						<tr>
						<td width="20%">
							<div><%=wrm.GetString("Das2txt2")%></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchCompanyID" runat="server" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchCompany" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=TextboxSearchCompany&textbox2=TextboxSearchCompanyID',event,500,400)">
									</td>
									</tr>
								</table>
						</td>
						<td width="20%">
							<div><%=wrm.GetString("Das2txt3")%></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchContactID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchContact" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=TextboxSearchContact&textboxID=TextboxSearchContactID&companyID=' + document.getElementById('TextboxSearchCompanyID').value,event,400,300)">
									</td>
									</tr>
								</table>
						</td>
						<td width="20%">
							<div><%=wrm.GetString("Das2txt4")%></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchLeadID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchLead" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopLead.aspx?render=no&textbox=TextboxSearchLead&textboxID=TextboxSearchLeadID&companyID=' + document.getElementById('TextboxSearchCompanyID').value,event,400,300)">
									</td>
									</tr>
								</table>
						</td>
						<td width="19%">
							<div><%=wrm.GetString("Das2txt5")%></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxOpportunityID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxOpportunity" runat="server" Width="100%"  CssClass="BoxDesign" readonly="true"></asp:TextBox>
									</td>
									<td width="30">
										<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopOpportunity.aspx?render=no&textbox=TextboxOpportunity&textboxID=TextboxOpportunityID',event)">
									</td>
									</tr>
								</table>
						</td>
					</tr>
					<tr>
						<td width="20%">
							<div><%=wrm.GetString("Das2txt1")%></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchOwnerID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchOwner" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=TextboxSearchOwner&textbox2=TextboxSearchOwnerID',event)">
									</td>
									</tr>
								</table>
						</td>
						<td>
							<div><%=wrm.GetString("Das2txt6")%></div>
							<asp:DropDownList id="Days" runat="server" CssClass="BoxDesign"></asp:DropDownList>
						</td>
						<td colspan=2 >
							<br><asp:LinkButton id="BtnSubmit" cssclass="save" runat="server" Text="OK"></asp:LinkButton>
						</td>
						</tr>
				</table>
				<br>
				<asp:Repeater id="RepeaterSearch" runat="server" >
					<HeaderTemplate>
						<table class="tblstruct normal">
							<tr>
								<td class="GridTitle" width="5%"><%=wrm.GetString("Acttxt11")%></td>
								<td class="GridTitle" width="20%"><%=wrm.GetString("Acttxt29")%></td>
								<td class="GridTitle" width="20%"><%=wrm.GetString("Das2txt2")%></td>
								<td class="GridTitle" width="20%"><%=wrm.GetString("Das2txt3")%></td>
								<td class="GridTitle" width="20%"><%=wrm.GetString("Das2txt4")%></td>
								<td class="GridTitle" width="15%"><%=wrm.GetString("Das2txt1")%></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
							<tr>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="5%">
									<asp:Literal id="AcDate" runat="server"/>
								</td>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Label ID="Subject" Runat=server></asp:Label>
								</td>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="CompanyName" Runat=server></asp:Literal>
								</td>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="ContactName" Runat=server></asp:Literal>
								</td>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="LeadName" Runat=server></asp:Literal>
								</td>
								<td class="GridItem" style="BORDER-RIGHT-STYLE: none;" width="15%">
									<asp:Literal ID="OwnerName" Runat=server></asp:Literal>
								</td>
							</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="5%">
									<asp:Literal id="AcDate" runat="server"/>
								</td>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Label ID="Subject" Runat=server></asp:Label>
								</td>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="CompanyName" Runat=server></asp:Literal>
								</td>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="ContactName" Runat=server></asp:Literal>
								</td>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="20%">
									<asp:Literal ID="LeadName" Runat=server></asp:Literal>
								</td>
								<td class="GridItemAltern" style="BORDER-RIGHT-STYLE: none;" width="15%">
									<asp:Literal ID="OwnerName" Runat=server></asp:Literal>
								</td>
							</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
				<Pag:RepeaterPaging id="RepeaterSearchPaging" visible=false runat=server /></TD></TR></TBODY></TABLE>
     </form>

</body>
</html>
