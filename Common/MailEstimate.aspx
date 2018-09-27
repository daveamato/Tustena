<%@ Page language="c#" Codebehind="MailEstimate.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.MailEstimate" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>MailEstimate</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
  <body>

    <form id="Form1" method="post" runat="server">
			<table class="tblstruct normal">
				<tr>
					<td>
						<div><%=wrm.GetString("Esttxt25")%></div>
						<asp:TextBox ID="FromAddress" Runat="server" CssClass="BoxDesign"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<div><%=wrm.GetString("Mletxt1")%></div>
						<asp:TextBox ID="MailAddress" Runat="server" CssClass="BoxDesign"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<div><%=wrm.GetString("Mletxt2")%></div>
						<asp:TextBox ID="MailObject" Runat="server" CssClass="BoxDesign"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<div><%=wrm.GetString("Mletxt3")%></div>
						<asp:TextBox ID="MailMessage" Runat="server" CssClass="BoxDesign" TextMode="MultiLine"
							Height="100px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align=right>
						<asp:LinkButton ID="Submitbtn" Runat="server" cssClass="normal" Text="OK"></asp:LinkButton>
					</td>
				</tr>
			</table>
     </form>

  </body>
</html>
