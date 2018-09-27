<%@ Page language="c#" Codebehind="CatalogHome.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Catalog.CatalogHome" %>
    <html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellspacing="0">
		<tr>
            <td width="140" class="SideBorderLinked" valign="top">
                <table width="98%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                   		<td class="sideContainer">
						<div class="sideTitle"><%=wrm.GetString("Cahtxt1")%></div>
                        <asp:linkbutton id="ViewTableData" runat="server" cssclass="sidebtn" />
						</td>
					</tr>
				</table>
			</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
							<%=wrm.GetString("Cahtxt2")%>
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
