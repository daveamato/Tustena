<%@ Page language="c#" trace="false" Codebehind="Dash4.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Dash.Dash4" %>
<html>
<head id="head" runat="server">
<script language="javascript" src="/js/dynabox.js"></script>

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
                        <%=wrm.GetString("Dastxt7")%>
                        </td>
					</tr>
				</table>
				<table id="tableData" runat="server" cellpadding=0 cellspacing=0 border=0 class="normal" width="95%" align=center>
				<tr>
				<td width="50%" valign=top>
					<table cellpadding=0 cellspacing=0 border=0 class="normal" width="95%" align=center>
						<tr>
							<td>
								<%=wrm.GetString("Das4txt1")%>
							</td>
						</tr>
						<tr>
							<td nowrap>
								<div><%=wrm.GetString("Das4txt5")%></div>
								<%=wrm.GetString("Das4txt2")%><asp:DropDownList id="YearFrom" old="true" runat="server" cssClass="BoxDesign" style="width:80px"></asp:DropDownList>
								<%=wrm.GetString("Das4txt3")%><asp:DropDownList id="MonthFrom" old="true" runat="server" cssClass="BoxDesign" style="width:50px"></asp:DropDownList>
								<%=wrm.GetString("Das4txt4")%><asp:DropDownList id="DayFrom" old="true" runat="server" cssClass="BoxDesign" style="width:50px"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td nowrap>
								<div><%=wrm.GetString("Das4txt6")%></div>
								<%=wrm.GetString("Das4txt2")%><asp:DropDownList id="YearTo" old="true" runat="server" cssClass="BoxDesign" style="width:80px"></asp:DropDownList>
								<%=wrm.GetString("Das4txt3")%><asp:DropDownList id="MonthTo" old="true" runat="server" cssClass="BoxDesign" style="width:50px"></asp:DropDownList>
								<%=wrm.GetString("Das4txt4")%><asp:DropDownList id="DayTo" old="true" runat="server" cssClass="BoxDesign" style="width:50px"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td>
								<div><%=wrm.GetString("Das4txt15")%></div>
								<asp:DropDownList id="vsType" old="true" runat="server" cssClass="BoxDesign"></asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td>
								<br>
								<asp:LinkButton id="BtnSubmit" cssclass="save" runat=server Text="OK"></asp:LinkButton>
							</td>
						</tr>

					</table>
				</td>
				<td width="50%" valign=top>
					<asp:Repeater id="SalesNameRepeater" runat="server">
							<HeaderTemplate>
								<table width="100%" cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td class="GridTitle" width="100%" colspan=2><%=wrm.GetString("Das4txt7")%></td>
								</tr>
								<tr>
									<td class="GridItemAltern" width="1%">
										<asp:CheckBox id="HeaderCheck" runat=server checked="true"></asp:CheckBox>
									</td>
									<td class="GridItemAltern" width="99%"><%=wrm.GetString("Das4txt8")%></td>
								</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="GridItem" width="1%">
										<asp:CheckBox id="ItemCheck" runat=server></asp:CheckBox>
										<asp:Literal id="IdOp" runat=server visible=false Text='<%#DataBinder.Eval(Container.DataItem,"uid")%>'></asp:Literal>
									</td>
									<td class="GridItem" width="99%"><%#DataBinder.Eval(Container.DataItem,"SalesName")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="GridItemAltern" width="1%">
										<asp:CheckBox id="ItemCheck" runat=server></asp:CheckBox>
										<asp:Literal id="IdOp" runat=server visible=false Text='<%#DataBinder.Eval(Container.DataItem,"uid")%>'></asp:Literal>
									</td>
									<td class="GridItemAltern" width="99%"><%#DataBinder.Eval(Container.DataItem,"SalesName")%></td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
					</asp:Repeater>
				</td>
				</tr>
				</table>

				<table id="graphResult" runat="server" cellSpacing="3" cellPadding="0" border="0" align="center">
					<tr>
						<td width="100%" colspan=2 align="center">
							<b><asp:Label id="graphTitle" runat=server cssClass="normal" style="font-size:15px;"/></b>
						</td>
					</tr>
					<tr>
						<td width="50%" valign=top>
							<asp:Label id="Result" runat=server cssClass="normal"/>
						</td>
						<td width="50%" valign=top>
							<asp:Literal id="Legend" runat=server/>
						</td>
					</tr>

				</table>
			</td>
		</tr>
	</table>
</form>



</body>
</html>
