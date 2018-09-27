<%@ Page language="c#" Codebehind="PopActivity.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.PopActivity" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>PopActivity</title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
	<script language="javascript" src="/js/common.js"></script>
  </head>
  <body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
    <form id="Form1" method="post" runat="server">
	<asp:Literal id="SomeJS" runat="server" />
		<table width="98%" border="0" cellspacing="0" align="center">
			<tr >
				<td align="left" width="100">
					<asp:TextBox id="FindIt" autoclick="Find" runat="server" class="BoxDesign" />
				</td>
				<td align="left" >
					<asp:LinkButton id="Find" runat="server" class="save"  />
				</td>
			</tr>
		</table>
		<br>
		<asp:Repeater id="RepActivity" runat="server" >
		<HeaderTemplate>
			<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
			<tr>
			<td class="GridTitle">Attivit&agrave;</td>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
			<td class="GridItem"><span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "ID") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "Subject"))%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "Subject")%></span>&nbsp;</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr>
			<td class="GridItemAltern"><span onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "ID") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "Subject"))%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "Subject")%></span>&nbsp;</td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
		</asp:Repeater>
     </form>

  </body>
</html>
