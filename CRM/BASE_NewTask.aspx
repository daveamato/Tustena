<%@ Page Language="c#" Trace="false" codebehind="BASE_NewTask.aspx.cs" Inherits="Digita.Tustena.NewTask"  AutoEventWireup="false"%>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>

<HTML>
  <head runat=server>
		<title>:: Tustena ::</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
  </HEAD>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form runat="server">
			<table width="98%" align="center" runat="server" id="tableEvent">
				<tr>
					<td align="left" class="BorderBottomTitles">
						<span class="divautoform">
							<b>
								<%=wrm.GetString("Ntstxt1")%>
							</b>
						</span>
					</td>
				</tr>
				<tr>
					<td>
						<asp:Table id="ChangeTable" runat="server" width="100%" align="center">
							<asp:TableRow>
								<asp:TableCell>
									<div class="divautoform"><%=wrm.GetString("Ntstxt2")%></div>
									<asp:TextBox ID="Subject" runat="server" class="inputautoform" />
								</asp:TableCell>
							</asp:TableRow>
							<asp:TableRow>
								<asp:TableCell>
									<div class="divautoform"><%=wrm.GetString("Ntstxt3")%></div>
									<asp:TextBox ID="Text" runat="server" Rows="5" TextMode="MultiLine" class="textareaautoform" />
								</asp:TableCell>
							</asp:TableRow>
							<asp:TableRow>
								<asp:TableCell>
									<GControl:GroupControl runat="server" id="groups" />
								</asp:TableCell>
							</asp:TableRow>
							<asp:TableRow>
								<asp:TableCell align="right">
									<asp:LinkButton ID="Submit" runat="server" cssClass="Save" />
								</asp:TableCell>
							</asp:TableRow>
						</asp:Table>
						<asp:Literal id="SomeJS" runat="server" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
