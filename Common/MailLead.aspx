<%@ Page language="c#" Codebehind="MailLead.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.MailLead" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>MailLead</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table class="tblstruct normal">
				<tr>
					<td>
						<div><%=wrm.GetString("Mailtxt2")%></div>
						<asp:TextBox ID="MailAddress" Runat="server" CssClass="BoxDesign"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<div><%=wrm.GetString("Mailtxt5")%></div>
						<asp:TextBox ID="MailObject" Runat="server" CssClass="BoxDesign"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td>
						<div><%=wrm.GetString("Nmstxt10")%></div>
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
</HTML>
