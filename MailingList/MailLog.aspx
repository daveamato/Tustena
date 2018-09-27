<%@ Page language="c#" Codebehind="MailLog.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.MailingList.MailLog" enableViewState="False"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>MailLog</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
	</HEAD>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
		<form id="Form1" method="post" runat="server">
			<asp:Repeater id="Repeater1" runat="server">
				<HeaderTemplate>
					<table cellpadding="2" cellspacing="1" width="98%" class="normal" align="center">
						<tr>
							<td width="70%" class="GridTitle">
								Mail
							</td>
							<td width="10%" class="GridTitle">
								NMail
							</td>
							<td width="20%" class="GridTitle">
								Data
							</td>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
						<td width="70%" class="GridItem">
							<%#DataBinder.Eval(Container.DataItem,"subject")%>
						</td>
						<td width="10%" class="GridItem">
							<%#DataBinder.Eval(Container.DataItem,"MailNumber")%>
						</td>
						<td width="20%" class="GridItem">
							<%#DataBinder.Eval(Container.DataItem,"SendDate","{0:d}")%>
						</td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr>
						<td width="70%" class="GridItemAltern">
							<%#DataBinder.Eval(Container.DataItem,"subject")%>
						</td>
						<td width="10%" class="GridItemAltern">
							<%#DataBinder.Eval(Container.DataItem,"MailNumber")%>
						</td>
						<td width="20%" class="GridItemAltern">
							<%#DataBinder.Eval(Container.DataItem,"SendDate","{0:d}")%>
						</td>
					</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:Repeater>
		</form>
	</body>
</HTML>
