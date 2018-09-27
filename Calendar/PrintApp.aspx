<%@ Page Language="c#" Trace="false" codebehind="PrintApp.aspx.cs" Inherits="Digita.Tustena.PrintApp"  AutoEventWireup="false"%>

<HTML>
  <head runat=server>
<title>:: Tustena ::</title>
<style>
.normal { FONT-SIZE: 12px; COLOR: #000000; FONT-FAMILY: verdana }
.righe { BORDER-BOTTOM: black 1px solid }
</style>
</HEAD>
<body leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" onload="self.print();self.close();" >
<table id ="MainTable" border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" width="100%">
  <TBODY>
  <tr>
    <td width="1%" valign="top"><img src="/images/Tustenalogo.gif" border="0"></td>
    <td width="99%">&nbsp;</td>
  </tr>
  <tr>
    <td width="100%">
    	<br>
    	<asp:Repeater id="ViewAppointmentForm" runat="server" >
	<HeaderTemplate>
	<table  border="0" cellpadding="0" cellspacing="0" width="99%" class="normal" style="border:1px solid black; "align="center">
	</HeaderTemplate>
	<ItemTemplate>
	<tr>
	<td width="50%" VAlign="TOP">
	 		<table border="0" cellpadding="2" cellspacing="5" width="100%" class="normal" align="center">
			<tr>
			<td class="righe" colspan="2">
			<%# DataBinder.Eval(Container.DataItem, "UserName")%>
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt4")%>
			</td>
	                <td class="righe">
			<asp:Literal id="Date" runat="server" />
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt5")%>
			</td>
	                <td class="righe">
			<asp:Literal id="DateFrom" runat="server" />
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt6")%>
			</td>
	                <td class="righe">
			<asp:Literal id="DateTo" runat="server" />
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt8")%>
			</td>
	                <td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "contact")%>
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt9")%>
			</td>
	                <td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "company")%>
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt64")%>
			</td>
	                <td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "phone")%>
			</td>
			</tr>
	            	</table>
	</td>
	<td width="50%" VAlign="TOP">
			<table id="tblStanza" border="0" cellpadding="2" cellspacing="5" width="100%" class="normal" align="center">
			<tr>
			<td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt11")%>
			</td>
			<td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "room")%>&nbsp;
			</td>
			</tr>
			<tr>
			<td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt12")%>
			</td>
			<td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "address")%>&nbsp;
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt13")%>
			</td>
	                <td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "city")%>&nbsp;
			</td>
			</tr>
			<tr>
	                <td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt14")%>
			</td>
			<td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "province")%>&nbsp;
			</td>
			</tr>
			<tr>
			<td width="40%" class="righe">
			<%=wrm.GetString("Evnttxt15")%>
			</td>
			<td class="righe">
			<%# DataBinder.Eval(Container.DataItem, "cap")%>&nbsp;
			</td>
			</tr>
	            	</table>
	</td>
	</tr>
	<tr>
	<td colspan="2">
	<%=wrm.GetString("Evnttxt16")%>
	</td>
	</tr>
	<tr>
	<td colspan="2" >
	<%# DataBinder.Eval(Container.DataItem, "note")%>&nbsp;
	</td>
	</tr>
	</ItemTemplate>
	<FooterTemplate>
	</table>
	</FooterTemplate>
	</asp:Repeater></TD></TR></TBODY></TABLE>

</body>
</HTML>
