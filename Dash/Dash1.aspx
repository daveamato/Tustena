<%@ Page language="c#" Codebehind="Dash1.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Dash.Dash1"%>
<html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellspacing="0">
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
                        <%=wrm.GetString("Dastxt3")%>
						</td>
					</tr>
				</table>
				<table class="normal" width="100%" id="tableData" cellSpacing="3" cellPadding="0" border="0"
					runat="server">
					<TBODY>
						<tr>
							<td width="20%" valign="top"><asp:radiobuttonlist id="RadioButtonList1" runat="server" cssClass="normal"></asp:radiobuttonlist>
								<br>
								<asp:LinkButton ID="SubmitBtn" cssclass="save" runat="server" Text="OK"></asp:LinkButton>
							</td>
							<td width="20%" valign="top">
								<table width="100%" cellSpacing="0" cellPadding="0" border="0">
									<tr>
										<td>
											<asp:DropDownList id="Drop1" old="true" runat="server" cssClass="BoxDesign"></asp:DropDownList>
										</td>
									</tr>
									<tr>
										<td>
										</td>
									</tr>
								</table>
							</td>
							<td width="80%" valign="top">
								<asp:Repeater id="OpportunityRepeater" runat="server">
									<HeaderTemplate>
										<table width="100%" cellSpacing="0" cellPadding="0" border="0">
											<tr>
												<td class="GridTitle" width="100%" colspan="2"><%=wrm.GetString("Das1txt12")%></td>
											</tr>
											<tr>
												<td class="GridItemAltern" width="1%">
													<asp:CheckBox id="HeaderCheck" runat="server" checked="true"></asp:CheckBox>
												</td>
												<td class="GridItemAltern" width="99%"><%=wrm.GetString("Das1txt13")%></td>
											</tr>
									</HeaderTemplate>
									<ItemTemplate>
										<tr>
											<td class="GridItem" width="1%">
												<asp:CheckBox id="ItemCheck" runat="server"></asp:CheckBox>
												<asp:Literal id="IdOp" runat=server visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'>
												</asp:Literal>
											</td>
											<td class="GridItem" width="99%"><%#DataBinder.Eval(Container.DataItem,"Title")%></td>
										</tr>
									</ItemTemplate>
									<AlternatingItemTemplate>
										<tr>
											<td class="GridItemAltern" width="1%">
												<asp:CheckBox id="ItemCheck" runat="server"></asp:CheckBox>
												<asp:Literal id="IdOp" runat=server visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'>
												</asp:Literal>
											</td>
											<td class="GridItemAltern" width="99%"><%#DataBinder.Eval(Container.DataItem,"Title")%></td>
										</tr>
									</AlternatingItemTemplate>
									<FooterTemplate>
				</table>
				</FooterTemplate> </asp:Repeater>
			</td>
		</tr>
	</table>
	<table id="graphResult" runat="server" cellSpacing="3" cellPadding="0" border="0" align="center">
		<tr>
			<td width="100%" colspan="2" align="center">
				<b>
					<asp:Label id="graphTitle" runat="server" cssClass="normal" style="FONT-SIZE:15px" /></b>
			</td>
		</tr>
		<tr>
			<td width="50%" valign="top">
				<asp:Label id="Result" runat="server" cssClass="normal" />
			</td>
			<td width="50%" valign="top">
				<asp:Literal id="Legend" runat="server" />
			</td>
		</tr>
	</table>
	</TD></TR></TBODY></TABLE>
</form>

</body>
</html>
