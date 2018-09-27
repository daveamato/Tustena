<%@ Page language="c#" Codebehind="CustomerQuoteList.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.ERP.CustomerQuoteList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head runat=server>
    <title>CustomerQuoteList</title>
  </HEAD>
  <link rel="stylesheet" type="text/css" href="/css/G.css">
  <body style="BACKGROUND-COLOR: #ffffff">

    <form id="Form1" method="post" runat="server">
				<asp:Repeater ID="QuoteListRepeater" Runat=server>
					<HeaderTemplate>
						<table cellpadding=0 cellspacing=0 width="98%" align=center>
							<tr>
								<td class="GridTitle" width="1%">&nbsp;</td>
								<td class="GridTitle"><%=wrm.GetString("Quotxt4")%></td>
								<td class="GridTitle"><%=wrm.GetString("Quotxt5")%></td>
								<td class="GridTitle"><%=wrm.GetString("Quotxt6")%></td>
								<td class="GridTitle"><%=wrm.GetString("Quotxt7")%></td>
								<td class="GridTitle"><%=wrm.GetString("Quotxt8")%></td>
								<td class="GridTitle" align=right><%=wrm.GetString("Quotxt9")%></td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
							<tr>
								<td class="GridItem">
<asp:LinkButton ID="OpenQuote" CommandName="OpenQuote" Runat=server></asp:LinkButton>
									<asp:Literal ID="QuoteID" Runat=server Visible=False></asp:Literal>
								</td>
								<td class="GridItem">
<asp:Literal ID="QuoteDate" Runat=server></asp:Literal></td>
								<td class="GridItem">
<asp:Literal ID="QuoteNumber" Runat=server></asp:Literal></td>
								<td class="GridItem">
<asp:Literal ID="QuoteDescription" Runat=server></asp:Literal></td>
								<td class="GridItem">
<asp:Literal ID="QuoteCustomer" Runat=server></asp:Literal></td>
								<td class="GridItem">
<asp:Literal ID="QuoteOwner" Runat=server></asp:Literal></td>
								<td class="GridItem" align=right>
<asp:Literal ID="QuoteTotal" Runat=server></asp:Literal></td>
							</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern">
<asp:LinkButton ID="OpenQuote" CommandName="OpenQuote" Runat=server></asp:LinkButton>
									<asp:Literal ID="QuoteID" Runat=server Visible=False></asp:Literal>
								</td>
								<td class="GridItemAltern">
<asp:Literal ID="QuoteDate" Runat=server></asp:Literal></td>
								<td class="GridItemAltern">
<asp:Literal ID="QuoteNumber" Runat=server></asp:Literal></td>
								<td class="GridItemAltern">
<asp:Literal ID="QuoteDescription" Runat=server></asp:Literal></td>
								<td class="GridItemAltern">
<asp:Literal ID="QuoteCustomer" Runat=server></asp:Literal></td>
								<td class="GridItemAltern">
<asp:Literal ID="QuoteOwner" Runat=server></asp:Literal></td>
								<td class="GridItemAltern" align=right>
<asp:Literal ID="QuoteTotal" Runat=server></asp:Literal></td>
							</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>

				<asp:Literal ID="lblRepeaterPaginginfo" Runat=server Visible=False></asp:Literal>
     </form>

  </body>
</HTML>
