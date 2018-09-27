<%@ Page language="c#" Codebehind="LeadInfo.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.LeadInfo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>LeadInfo</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
  <body bgcolor="#FFFFFF" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >

    <form id="Form1" method="post" runat="server">
			<asp:Repeater id="RepCrossLead" runat="server" >
			<HeaderTemplate>
				<table border="0" cellpadding="3" cellspacing="2" width="100%" height="100%" class="normal" align="center" bgcolor="#FFFFFF">
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
				<td style="border-bottom: 1px solid #000000;" colspan=2>
		 		<BR>
		 		<b><%=wrm.GetString("Ledtxt4")%></b>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt7")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt8")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"ContactName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt9")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"OpportunityName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt10")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt11")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"StatusDescription")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt12")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"RatingDescription")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt13")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"ProductInterestDescription")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt20")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"LeadCurrencyDescription")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt14")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"PotentialRevenue","{0:c}")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt15")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"EstimatedCloseDate","{0:d}")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt16")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"SourceDescription")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt17")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"Campaign")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt18")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"IndustryName")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Ledtxt19")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"SalesPersonName")%>
				</td>
				</tr>
			</ItemTemplate>
			<FooterTemplate>
				</table>
			</FooterTemplate>
			</asp:Repeater>
     </form>

  </body>
</html>
