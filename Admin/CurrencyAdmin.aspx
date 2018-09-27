<%@ Page language="c#" Codebehind="CurrencyAdmin.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Admin.CurrencyAdmin" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
		<html>
<head id="head" runat="server">
<script>
		function change(x)
		{
			var re = new RegExp (',', 'gi') ;
			var CurrChange1 = document.getElementById("CurrChange1");
			var CurrChange2 = document.getElementById("CurrChange2");
			CurrChange2.value = CurrChange2.value.replace(re, '.') ;
			CurrChange1.value = CurrChange1.value.replace(re, '.') ;
			switch (x)
			{
			case 0:
				CurrChange2.value = RoundToNdp((1 / CurrChange1.value),4);
				break;
			case 1:
				CurrChange1.value = RoundToNdp((1 / CurrChange2.value),4);
				break;
			}
		}

		function RoundToNdp(X, N) {
			var T = Number('1e'+N);
			return Math.round(X*T)/T
		}

		</script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
			<table width="100%" cellspacing="0">
				<tr>
					<td width="140" class="SideBorderLinked" valign="top">
						<table width="100%" border="0" class=normal>
							<tr>
								<td align="left" class="BorderBottomTitles">
									<span class="divautoform"><b>
											<twc:LocalizedLiteral Text="Curtxt1" runat="server" />
										</b></span>
								</td>
							</tr>
							<tr>
								<td align="center">
									<div><twc:LocalizedLiteral Text="Curtxt6" runat="server" /></div>
									<asp:Literal id="CurrencySymbol" runat="server"/>
								</td>
							</tr>
							<tr>
								<td align="center">
									<asp:LinkButton id="ChangeCurrency" runat="server" cssClass="Save"/>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top">
						<table id="CompanyCurrencyTable" runat="server" CELLSPACING="5" CELLPADDING="3" BORDER="0" WIDTH="90%" class="normal">
							<tr>
								<td>
									<asp:Label id="CurInfo1" runat="server" cssClass=normal enableviewstate="false"/>
								</td>
							</tr>
							<tr>
								<td>
									<table CELLSPACING="0" CELLPADDING="0" WIDTH="50%" class="normal">
										<tr>
											<td colspan=2>
												<div><twc:LocalizedLiteral Text="Curtxt9" runat="server" /></div>
												<asp:DropDownList id="DropCompanyCurrency" runat="server"  CssClass="BoxDesign"/>
											</td>
										</tr>
										<tr>
											<td>
												<div><twc:LocalizedLiteral Text="Curtxt10" runat="server" /></div>
												<asp:TextBox ID="NewCurrName" Runat="server" CssClass="BoxDesign" MaxLength="20"></asp:TextBox>
											</td>
											<td style="padding-left:5px">
												<div><twc:LocalizedLiteral Text="Curtxt5" runat="server" /></div>
												<asp:TextBox ID="NewCurrSymbol" Runat="server" CssClass="BoxDesign" WIDTH="20" MaxLength="2"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td>
												<asp:Label ID="CurInfo2" Runat="server" CssClass="divautoformRequired"/>
											</td>
											<td align="right" valign=bottom colspan=2>
												<asp:LinkButton ID="NewCurrSubmit" Runat="server" CssClass="Save"/>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
						<table id="CurrencyTable" runat="server" CELLSPACING="5" CELLPADDING="3" BORDER="0" WIDTH="90%" class="normal">
							<tr>
								<td width="35%">
								<div><twc:LocalizedLiteral Text="Curtxt2" runat="server" /></div>
									<asp:TextBox ID="CurrName" Runat="server" CssClass="BoxDesign" MaxLength="20"></asp:TextBox>
								</td>
								<td width="10%">
								<div><twc:LocalizedLiteral Text="Curtxt5" runat="server" /></div>
									<asp:TextBox ID="CurrSymbol" Runat="server" CssClass="BoxDesign" MaxLength="20"></asp:TextBox>
								</td>
								<td width="25%">
								<div><twc:LocalizedLiteral Text="Curtxt3" runat="server" /></div>
									<asp:TextBox ID="CurrChange1" onkeypress="NumbersOnly(event,'.,',this)" Runat="server" CssClass="BoxDesign" MaxLength="20"></asp:TextBox>
								</td>
								<td width="25%">
								<div><twc:LocalizedLiteral Text="Curtxt4" runat="server" /></div>
									<asp:TextBox ID="CurrChange2" onkeypress="NumbersOnly(event,'.,',this)" Runat="server" CssClass="BoxDesign" MaxLength="20"></asp:TextBox>
								</td>
								<td align="right" width="5%" valign=bottom>
									<asp:TextBox ID="CurrID" Runat="server" visible=false></asp:TextBox>
									<asp:LinkButton ID="CurrSubmit" Runat="server" CssClass="Save"/>
									<a href="http://www.oanda.com/convert/classic?user=tustena&lang=en" target="_blank"><span class="Save">Verifica cambio</span></a>
								</td>
							</tr>
						</table>
						<asp:Repeater id="CurrencyRepeater" runat="server">
							<HeaderTemplate>
								<table border="0" cellspacing="0" width="98%" align="center" class="normal">
									<tr>
										<td class="GridTitle" width="25%"><twc:LocalizedLiteral Text="Curtxt2" runat="server" /></td>
										<td class="GridTitle" width="10%"><twc:LocalizedLiteral Text="Curtxt5" runat="server" /></td>
										<td class="GridTitle" width="25%"><twc:LocalizedLiteral Text="Curtxt3" runat="server" /></td>
										<td class="GridTitle" width="25%"><twc:LocalizedLiteral Text="Curtxt4" runat="server" /></td>
										<td class="GridTitle" width="15%">&nbsp;</td>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
									<tr>
										<td class="GridItem" width="25%"><%#DataBinder.Eval(Container.DataItem,"Currency")%></td>
										<td class="GridItem" width="10%"><%#DataBinder.Eval(Container.DataItem,"CurrencySymbol")%></td>
										<td class="GridItem" width="25%"><%#DataBinder.Eval(Container.DataItem,"ChangeToEuro")%></td>
										<td class="GridItem" width="25%"><%#DataBinder.Eval(Container.DataItem,"ChangeFromEuro")%></td>
										<td class="GridItem" width="15%">
											<asp:Literal id="CurrencyID" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
											<asp:LinkButton id="Edit" CommandName="Edit" runat="server" cssClass="Save"/>
											<asp:LinkButton id="Delete" CommandName="Delete" runat="server" cssClass="Save"/>
										</td>
									</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
									<tr>
										<td class="GridItemAltern" width="25%"><%#DataBinder.Eval(Container.DataItem,"Currency")%></td>
										<td class="GridItemAltern" width="10%"><%#DataBinder.Eval(Container.DataItem,"CurrencySymbol")%></td>
										<td class="GridItemAltern" width="25%"><%#DataBinder.Eval(Container.DataItem,"ChangeToEuro")%></td>
										<td class="GridItemAltern" width="25%"><%#DataBinder.Eval(Container.DataItem,"ChangeFromEuro")%></td>
										<td class="GridItemAltern" width="15%">
											<asp:Literal id="CurrencyID" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
											<asp:LinkButton id="Edit" CommandName="Edit" runat="server" cssClass="Save"/>
											<asp:LinkButton id="Delete" CommandName="Delete" runat="server" cssClass="Save"/>
										</td>
									</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>
					</td>
				</tr>
			</table>
		</form>

</body>
</html>
