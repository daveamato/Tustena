<%@ Page language="c#" Codebehind="ViewContact.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.ViewContact" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>ViewContact</title>
    <script type="text/javascript" src="/js/common.js"></script>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
  <body style="BACKGROUND-COLOR: #ffffff">
    <form id="Form1" method="post" runat="server">
		<asp:Repeater ID="Repeater1" Runat=server>
			<ItemTemplate>
				<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
				<tr>
                        <td colspan=2 align=right>
                            <asp:LinkButton ID="editCmd" CommandName="editCmd" runat=server><img border=0 src="/i/modify2.gif" /></asp:LinkButton>
                        </td>
                </tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt15")%>
				</td>
				<td class="VisForm">
				<asp:Literal id="Title" runat="server"/>
				<%#DataBinder.Eval(Container.DataItem,"Name")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt16")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"Surname")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt21")%>
				</td>
				<td class="VisForm">
				<table class=normal width="100%">
					<tr>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"PHONE_1")%>&nbsp;
						</td>
						<td width="10" align=right>
							<asp:Literal id="VoipCallPhone_1" runat="server"/>
						</td>
					</tr>
				</table>
				<asp:Literal id="CompanyPhone" runat="server"/>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt22")%>
				</td>
				<td class="VisForm">
				<table class=normal width="100%">
					<tr>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"PHONE_2")%>&nbsp;
						</td>
						<td width="10" align=right>
							<asp:Literal id="VoipCallPhone_2" runat="server"/>
						</td>
					</tr>
				</table>

				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt23")%>
				</td>
				<td class="VisForm">
				<table class=normal width="100%">
					<tr>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>&nbsp;
						</td>
						<td width="10" align=right>
							<asp:Literal id="VoipCallMobilePhone_1" runat="server"/>
						</td>
					</tr>
				</table>

				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt24")%>
				</td>
				<td class="VisForm">
				<table class=normal width="100%">
					<tr>
						<td>
							<%#DataBinder.Eval(Container.DataItem,"MobilePhone_2")%>&nbsp;
						</td>
						<td width="10" align=right>
							<asp:Literal id="VoipCallMobilePhone_2" runat="server"/>
						</td>
					</tr>
				</table>

				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt46")%>
				</td>
				<td class="VisForm">
				<%#DataBinder.Eval(Container.DataItem,"Fax")%>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt25")%>
				</td>
				<td class="VisForm">
				<a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>"><%#DataBinder.Eval(Container.DataItem,"Email")%></a>
				</td>
				</tr>
				</table>
			</ItemTemplate>
		</asp:Repeater>

		<table id="edittable" runat=server border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt15")%>
				</td>
				<td>
				    <asp:TextBox ID="txtName" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt16")%>
				</td>
				<td>
				    <asp:TextBox ID="txtSurname" runat=server CssClass="BoxDesign" ReadOnly=true></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt21")%>
				</td>
				<td>
				    <asp:TextBox ID="txtPHONE_1" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt22")%>
				</td>
				<td>
				    <asp:TextBox ID="txtPHONE_2" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt23")%>
				</td>
				<td>
				    <asp:TextBox ID="txtMobilePhone_1" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt24")%>
				</td>
				<td>
				    <asp:TextBox ID="txtMobilePhone_2" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt46")%>
				</td>
				<td>
				    <asp:TextBox ID="txtFax" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
				<td width="40%">
				<%=wrm.GetString("Reftxt25")%>
				</td>
				<td>
				    <asp:TextBox ID="txtEmail" runat=server CssClass="BoxDesign"></asp:TextBox>
				</td>
				</tr>
				<tr>
                <td colspan=2 align=right>
                    <twc:LocalizedLinkButton ID="modSave" runat=server CssClass="Save" Text="Save"></twc:LocalizedLinkButton>
                    <asp:Literal ID="litID" runat=server Visible=false></asp:Literal>
                </td>
                </tr>
				</table>
     </form>
  </body>
</html>
