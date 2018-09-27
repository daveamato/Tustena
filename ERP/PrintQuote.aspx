<%@ Page language="c#" Codebehind="PrintQuote.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.ERP.PrintQuote" Trace="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<LINK href="/css/G.css" type="text/css" rel="stylesheet">
			<title>PrintQuote</title>
	</HEAD>
	<body style="BACKGROUND-COLOR: #f2f2f2" leftMargin="5" topMargin="5">
		<form id="Form1" method="post" runat="server">
			<asp:Literal id="QuoteId" runat="server" visible="false" />
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td class="GridTitle">
						<%=wrm.GetString("PDFOff19")%>
					</td>
				</tr>
				<tr>
					<td>
						<asp:RadioButtonList id="chkheader" runat="server"></asp:RadioButtonList>
					</td>
				</tr>
				<tr>
					<td class="GridTitle">
						<%=wrm.GetString("PDFOff23")%>
					</td>
				</tr>
				<tr>
					<td>
						<asp:TextBox id="CompanyName" runat="server" class="BoxDesign" />
						<asp:TextBox id="CompanyAddress" runat="server" class="BoxDesign" />
					</td>
				</tr>
				<tr>
				<tr>
					<td class="GridTitle">
						<%=wrm.GetString("PDFOff21")%>
						(<%=wrm.GetString("PDFOff25")%>)
					</td>
				</tr>
				<tr>
					<td>
						<asp:RadioButtonList id="chkLogo" runat="server"></asp:RadioButtonList>
					</td>
				</tr>
				</tr>
				<tr>
					<td>
						<asp:CheckBox id="chkProductAttach" runat="server" /><%=wrm.GetString("Quotxt34")%>
					</td>
				</tr>
				<tr>
					<td>
						<asp:CheckBox id="chkAttachment" runat="server" /><%=wrm.GetString("Quotxt38")%>
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:LinkButton id="btnPrint" runat="server" cssclass="Save" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
