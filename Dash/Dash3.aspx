<%@ Page language="c#" trace="false" Codebehind="Dash3.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Dash.Dash3" %>
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
                        <%=wrm.GetString("Dastxt6")%>
						</td>
					</tr>
				</table>
				<table class="normal" width="100%" id="tableData" cellSpacing="3" cellPadding="0" border="0" runat="server">
					<tr>
						<td width="20%" valign=top>
							<asp:radiobuttonlist id="RadioButtonList1" runat="server" cssClass="normal"></asp:radiobuttonlist>
						</td>
						<td width="20%" valign=top>
							<asp:DropDownList id="Drop1" old="true" runat="server" cssClass="BoxDesign"></asp:DropDownList>
						</td>
						<td width="20%" valign=top>
							<asp:DropDownList id="Drop2" old="true" runat="server" cssClass="BoxDesign"></asp:DropDownList>
						</td>
						<td width="40%" valign=top>
							<asp:LinkButton id="BtnSubmit" cssclass="save" runat=server Text="OK"></asp:LinkButton>
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
