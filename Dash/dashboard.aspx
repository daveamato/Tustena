<%@ Page language="c#" Codebehind="dashboard.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Dash.dashboard" %>
    <html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0">
				<tr>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
							<%=wrm.GetString("Dastxt1")%>
								</td>
							</tr>
						</table>
						<br>
						<table width="100%" cellpadding="2" cellspacing="1">
							<tr>
								<td class="GridTitle" width="100%"><%=wrm.GetString("Dastxt2")%></td>
							</tr>
							<tr>
								<td class="GridItem" width="100%"><a href="/dash/dash1.aspx?m=55&si=56"><%=wrm.GetString("Dastxt3")%></a></td>
							</tr>
							<tr>
								<td class="GridItemAltern" width="100%"><a href="/dash/dash3.aspx?m=55&si=56"><%=wrm.GetString("Dastxt6")%></a></td>
							</tr>
							<tr>
								<td class="GridItem" width="100%"><a href="/dash/dash4.aspx?m=55&si=56"><%=wrm.GetString("Dastxt7")%></a></td>
							</tr>
						</table>
						<br>
						<table width="100%" cellpadding="2" cellspacing="1">
							<tr>
								<td class="GridTitle" width="100%"><%=wrm.GetString("Dastxt4")%></td>
							</tr>
							<tr>
								<td class="GridItem" width="100%"><a href="/dash/dash2.aspx?m=55&si=56"><%=wrm.GetString("Dastxt5")%></a></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
     </form>


</body>
</html>
