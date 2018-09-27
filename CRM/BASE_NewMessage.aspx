<%@ Page Language="c#" Trace="false" codebehind="BASE_NewMessage.aspx.cs" Inherits="Digita.Tustena.CRM.NewMessage" AutoEventWireup="false"%>
<%@ Register TagPrefix="TOffice" TagName="SelectOffice" Src="~/Common/SelectOffice.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>

<HTML>
  <head runat=server>
		<title>:: Tustena ::</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
		<script language="javascript" src="/js/common.js"></script>
		<script type="text/javascript" src="/js/SelectOffice.js"></script>
</HEAD>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0">
		<form runat="server">
		<domval:DomValidationSummary
		id="valSum"
		ShowMessageBox=true
   DisplayMode="BulletList"
   EnableClientScript="true"
   runat="server"/>
			<table width="98%" align="center" runat="server" id="tableEvent">
				<tr>
					<td align="left" class="BorderBottomTitles">
						<span class="divautoform">
							<b>
								<%=wrm.GetString("Nmstxt1")%>
							</b>
						</span>
					</td>
				</tr>
				<tr>
					<td>
						<TOffice:SelectOffice runat="server" id="Office"/>
						<asp:Table id="ChangeTable" runat="server" width="100%" HorizontalAlign="center">

							<asp:TableRow>
								<asp:TableCell ColumnSpan="3">
									<div class="divautoform"><%=wrm.GetString("Nmstxt9")%>
									<domval:RequiredDomValidator id="SubjectValidator" EnableClientScript="true" Display=Static runat="server" ControlToValidate="Subject">*</domval:RequiredDomValidator>
									</div>
									<asp:TextBox ID="Subject" runat="server" cssClass="inputautoform" />
								</asp:TableCell>
							</asp:TableRow>
							<asp:TableRow>
								<asp:TableCell ColumnSpan="3">
									<div class="divautoform"><%=wrm.GetString("Nmstxt10")%>
									<domval:RequiredDomValidator id="TextValidator" EnableClientScript="true" Display=Static runat="server" ControlToValidate="Text">*</domval:RequiredDomValidator>
									</div>
									<asp:TextBox ID="Text" runat="server" Rows="5" TextMode="MultiLine" cssClass="textareaautoform" />

								</asp:TableCell>
							</asp:TableRow>
							<asp:TableRow>
								<asp:TableCell ColumnSpan="3" HorizontalAlign="right">
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
