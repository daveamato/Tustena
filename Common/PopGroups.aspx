<%@ Page language="c#" Codebehind="PopGroups.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.PopGroups" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
</head>

<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >

    <form id="Form1" method="post" runat="server">
		<asp:Repeater id="Groups" runat="server" >
		<HeaderTemplate>
			<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
			<tr>
			<td class="GridTitle"><%=wrm.GetString("Pgrtxt0")%></td>
			</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
			<td class="GridItem"><span onclick="SetRef('<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "Description"))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")))%>')" class="linked""><%# DataBinder.Eval(Container.DataItem, "Description")%></span>&nbsp;</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr>
			<td class="GridItemAltern"><span onclick="SetRef('<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "Description"))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "ID")))%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "Description")%></span>&nbsp;</td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
		</asp:Repeater>
     </form>

  </body>
</html>
