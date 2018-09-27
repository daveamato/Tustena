<%@ Page language="c#" Codebehind="ActivityMail.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.ActivityMail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>Send Mail</title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
	<script language="javascript" src="/js/common.js"></script>
	<script>
		function WriteBack(val){
		dynaret('destinationEmail').value=val;
		self.close();
		parent.HideBox();
		}
	</script>
  </head>
  <body style="BACKGROUND-COLOR: #ffffff">
    <form id="Form1" method="post" runat="server">
		<asp:Repeater ID="Repeater1" Runat=server EnableViewState=False>
			<ItemTemplate>
				<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt17")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt25")%>
				</td>
				<td class="VisForm">
				<a href="javascript:WriteBack('<%#DataBinder.Eval(Container.DataItem,"Email")%>')"><%#DataBinder.Eval(Container.DataItem,"Email")%></a>
				</td>
				</tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>
				<asp:Repeater ID="Repeater2" Runat=server EnableViewState=False>
			<ItemTemplate>
				<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt15")%>
				</td>
				<td class="VisForm">
				<asp:Literal id="Title" runat="server"/>
				<%#DataBinder.Eval(Container.DataItem,"Name")%> <%#DataBinder.Eval(Container.DataItem,"Surname")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt25")%>
				</td>
				<td class="VisForm">
				<a href="javascript:WriteBack('<%#DataBinder.Eval(Container.DataItem,"Email")%>')"><%#DataBinder.Eval(Container.DataItem,"Email")%></a>
				</td>
				</tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>
				<asp:Repeater ID="Repeater3" Runat=server EnableViewState=False>
			<ItemTemplate>
				<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt15")%>
				</td>
				<td class="VisForm">
				<asp:Literal id="Title" runat="server"/>
				<%#DataBinder.Eval(Container.DataItem,"Name")%> <%#DataBinder.Eval(Container.DataItem,"Surname")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt17")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt25")%>
				</td>
				<td class="VisForm">
				<a href="javascript:WriteBack('<%#DataBinder.Eval(Container.DataItem,"Email")%>')"><%#DataBinder.Eval(Container.DataItem,"Email")%></a>
				</td>
				</tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>
		<asp:Label id="Info" runat=server class=normal style="color:red"/>
     </form>
  </body>
</html>
