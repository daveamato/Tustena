<%@ Page language="c#" Codebehind="PopUpReport.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PopUpReport" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>PopFile</title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
	<script language="javascript" src="/js/common.js"></script>
  <body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
    <form id="Form1" method="post" runat="server">
		<table width="98%" border="0" cellspacing="0" align="center">
			<tr >
				<td align="left" width="100">
					<asp:TextBox id="FindIt" autoclick="Find" runat="server" class="BoxDesign" />
				</td>
				<td align="left" >
					<asp:LinkButton id="Find" runat="server" class="save"/>
				</td>
			</tr>
		</table>
		<br>
					<asp:Repeater id="ReportRepeater" runat="server">
						<HeaderTemplate>
							<table class="tblstruct normal">
								<tr>
									<td class="GridTitle" width="99%"><%=wrm.GetString("Mlevtxt1")%></td>
								</tr>
						</HeaderTemplate>
						<ItemTemplate>
							<tr>
								<td class="GridItem">
									<span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "id") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "description"))%>')" class="linked"><%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "description"))%></span>
								</td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern">
									<span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "id") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "description"))%>')" class="linked"><%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "description"))%></span>
								</td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
							</table>
						</FooterTemplate>
					</asp:Repeater>
    </form>
  </body>
</html>
