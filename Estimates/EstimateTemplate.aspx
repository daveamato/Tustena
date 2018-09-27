<%@ Page language="c#" trace="false" Codebehind="EstimateTemplate.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Estimates.EstimateTemplate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>EstimateTemplate</title>
	</HEAD>
	<body>
	<script>
		function LoadLogo(){
			var t = document.getElementById("NomeFile");
			var i = document.getElementById("ViewLogo");
			i.src=t.value;
			i.style.display = '';
		}
	</script>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0">
				<tr>
					<td width="140" class="SideBorderLinked" valign="top">
						<table width="100%" border="0">
							<tr>
								<td align="left" class="BorderBottomTitles normal">
									<b>
										<%=wrm.GetString("Esttxt34")%>
									</b>
								</td>
							</tr>
							<tr>
								<td>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table width="80%" border="0" class="normal">
							<tr>
								<td align="left" class="BorderBottomTitles normal">
									<b>
										<%=wrm.GetString("Esttxt34")%>
									</b>
								</td>
							</tr>
							<tr>
								<td>
								<div><%=wrm.GetString("Esttxt35")%></div>
									<input type="file" id="NomeFile" runat="server" class="BoxDesign" NAME="NomeFile">
								</td>
							</tr>
							<tr>
								<td>
									<img id="ViewLogo" runat="server" src="">
								</td>
							</tr>
							<tr>
								<td>
									<div><%=wrm.GetString("Esttxt36")%></div>
									<asp:TextBox ID="FreeText" Runat=server TextMode=MultiLine Height="50px" CssClass="BoxDesign"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td align=right>
									<asp:LinkButton ID="SaveTemplate" Runat=server CssClass="save">OK</asp:LinkButton>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
