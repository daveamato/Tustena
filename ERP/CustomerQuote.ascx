<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CustomerQuote.ascx.cs" Inherits="Digita.Tustena.ERP.CustomerQuote" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
			<twc:tustenarepeater id="QuoteListRepeater" runat="server" SortDirection="asc" AllowPaging="true" AllowAlphabet="false" AllowSearching="false">
						<HEADERTEMPLATE>
							<twc:REPEATERHEADERBEGIN id=RepeaterHeaderBegin1 runat="server"></twc:REPEATERHEADERBEGIN>
							<tr>
								<td class="GridTitle" width="1%">&nbsp;</td>
								<td class="GridTitle">
								<twc:LocalizedLiteral id="LocalizedLiteral2" runat="server" Text="Quotxt4"></twc:LocalizedLiteral>
								</td>
								<td class="GridTitle">
								<twc:LocalizedLiteral id="Localizedliteral1" runat="server" Text="Quotxt5"></twc:LocalizedLiteral>
								</td>
								<td class="GridTitle">
								<twc:LocalizedLiteral id="Localizedliteral3" runat="server" Text="Quotxt6"></twc:LocalizedLiteral>
								</td>
								<td class="GridTitle">
								<twc:LocalizedLiteral id="Localizedliteral4" runat="server" Text="Quotxt7"></twc:LocalizedLiteral>
								</td>
								<td class="GridTitle">
								<twc:LocalizedLiteral id="Localizedliteral5" runat="server" Text="Quotxt8"></twc:LocalizedLiteral>
								</td>
								<td class="GridTitle" align=right>
								<twc:LocalizedLiteral id="Localizedliteral6" runat="server" Text="Quotxt9"></twc:LocalizedLiteral>
								</td>
							</tr>
							<twc:REPEATERHEADEREND id=RepeaterHeaderEnd1 runat="server"></twc:REPEATERHEADEREND>
					</HEADERTEMPLATE>
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
					</twc:tustenarepeater>

				<asp:Literal ID="lblRepeaterPaginginfo" Runat=server Visible=False></asp:Literal>
