<%@ Page language="c#" Codebehind="PopScoreValues.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PopScoreValues" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head runat=server>
    <title>PopScoreValues</title>
    <link rel="stylesheet" type="text/css" href="/css/G.css">
  </HEAD>
  <body leftmargin=0 topmargin=0>

    <form id="Form1" method="post" runat="server">
					<twc:localizedLiteral text="Scotxt6" runat="server" ID="Localizedliteral3"/>
					<asp:Repeater ID="scorerepeater" Runat=server>
						<HeaderTemplate>
							<table cellpadding=0 cellspacing=0 width="100%" align=center>
								<tr>
									<td class="GridTitle" width="80%"><%=wrm.GetString("Scotxt1")%></td>

									<td class="GridTitle" width="20%" align=right colspan=2>
										&nbsp;
									</td>
								</tr>
						</HeaderTemplate>
						<ItemTemplate>
								<tr>
									<td class="GridItem" width="80%">
										<asp:Label ID="ScoreID" Runat=server Visible=False></asp:Label>
										<asp:TextBox ID="ScoreDescription" Runat=server cssclass="BoxDesign" Enabled=False></asp:TextBox>
									</td>

									<td class="GridItem" width="10%" align=right>
										<asp:LinkButton ID="VotePlus" CommandName="VotePlus" Runat=server><img src=/i/ThumbUp.gif border=0 align=right></asp:LinkButton>
									</td>
									<td class="GridItem" width="10%" align=right>
										<asp:LinkButton ID="VoteMinus" CommandName="VoteMinus" Runat=server><img src=/i/ThumbDown.gif border=0 align=right></asp:LinkButton>
									</td>
								</tr>
						</ItemTemplate>
						<FooterTemplate>
							</table>
						</FooterTemplate>
					</asp:Repeater>
     </form>

  </body>
</HTML>
