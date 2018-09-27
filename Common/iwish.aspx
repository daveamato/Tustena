<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page language="c#" Codebehind="iwish.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.iwish" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>:: Feedback ::</title>
		<LINK href="/css/G.css" type="text/css" rel="stylesheet">
			<LINK href="/tustena.ico" rel="SHORTCUT ICON">
	</HEAD>
	<body>
		<asp:Panel id="Panel1" runat="server">
			<FORM id="Form1" method="post" runat="server">
				<twc:LocalizedLiteral id="LocalizedLiteral1" runat="server" Text="Feedtxt1"></twc:LocalizedLiteral>
				<TABLE class="normal" width="320" border="0">
					<TR>
						<TD class="normal"><%=wrm.GetString("Feedtxt2")%><BR>
							<asp:textbox id="IWSubject" runat="server" Width="300px"></asp:textbox></TD>
						<TD>
							<domval:RequiredDomValidator id="RequiredFieldValidator1" runat="server" ControlToValidate="IWSubject" ErrorMessage="*"></domval:RequiredDomValidator></TD>
					</TR>
					<TR>
						<TD class="normal"><%=wrm.GetString("Feedtxt3")%><BR>
							<asp:textbox id="IWNote" runat="server" Width="300px" Rows="12" TextMode="MultiLine"></asp:textbox></TD>
						<TD>
							<domval:RequiredDomValidator id="RequiredFieldValidator2" runat="server" ControlToValidate="IWNote" ErrorMessage="*"></domval:RequiredDomValidator></TD>
					</TR>
					<TR>
						<TD class="normal" colSpan="2"><%=wrm.GetString("Feedtxt4")%>&nbsp;
							<asp:radiobuttonlist id="Type" runat="server" cssClass="normal" RepeatDirection="Horizontal"></asp:radiobuttonlist>
						</TD>
					</TR>
					<TR>
						<TD colSpan="2">
							<asp:button id="Button2" runat="server" Width="300px"></asp:button>
						</TD>
					</TR>
				</TABLE>

				<table id="livechat" runat=server align=center>
					<tr>
						<td>
						<div id="div_initiate" style="position:absolute; z-index:1; top: 40%; left:40%; visibility: hidden;"><a href="javascript:Live.initiate_accept();"><img src="http://hcl.digita.it/templates/Bliss/images/initiate.gif" border="0"></a><br><a href="javascript:Live.initiate_decline();"><img src="http://hcl.digita.it/templates/Bliss/images/initiate_close.gif" border="0"></a></div>
						<script type="text/javascript" language="javascript" src="http://hcl.digita.it/class/js/include.php?live&departmentid=2"></script>
						</td>
					</tr>
				</table>
			</FORM>
		</asp:Panel>
		<asp:Panel id="Panel2" runat="server" Visible="false" class="normal">
			<twc:LocalizedLiteral id="LocalizedLiteral2" runat="server" Text="Feedtxt5"></twc:LocalizedLiteral>
			<SCRIPT>setTimeout("self.close()",5000)</SCRIPT>
		</asp:Panel>
	</body>
</HTML>

