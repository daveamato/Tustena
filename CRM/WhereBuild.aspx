<%@ Page language="c#" Codebehind="WhereBuild.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Report.WebForm1" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>WhereBuilder</title>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td>
						Campi:<br>
						<asp:listbox id="ListBox1" runat="server" Width="400px">
							<asp:ListItem Value="Tabella1.item1">Tabella1.item1</asp:ListItem>
							<asp:ListItem Value="Tabella2.item1">Tabella2.item1</asp:ListItem>
							<asp:ListItem Value="Tabella1.item2">Tabella1.item2</asp:ListItem>
						</asp:listbox>
						<asp:button id="Button_add" runat="server" Text="Inserisci"></asp:button>
					</td>
				</tr>
				<tr>
					<td>
						Parametri:<br>
						<asp:TextBox id="Parameter" runat="server"></asp:TextBox>
						<asp:button id="Button_Parameter" runat="server" Text="Inserisci"></asp:button>
					</td>
				</tr>
				<tr>
					<td>
						Operatori:<br>
						<asp:button id="Button_or" runat="server" Text="Or"></asp:button>
						<asp:button id="Button_and" runat="server" Text="And"></asp:button>
						<asp:button id="Button_Openpar" runat="server" Text="("></asp:button>
						<asp:button id="Button_Closepar" runat="server" Text=")"></asp:button>
						<asp:button id="Button_Equal" runat="server" Text="="></asp:button>
						<asp:button id="Button_Major" runat="server" Text=">"></asp:button>
						<asp:button id="Button_Minus" runat="server" Text="<"></asp:button>
						<asp:button id="Button_MajorEqual" runat="server" Text=">="></asp:button>
						<asp:button id="Button_MinusEqual" runat="server" Text="<="></asp:button>
						<asp:button id="Button_Different" runat="server" Text="!="></asp:button>
						<asp:button id="Button_like" runat="server" Text="like"></asp:button>
					</td>
				</tr>
				<TR>
					<TD>
						<P><asp:Literal id="WhereQuery" runat="server"></asp:Literal></P>
						<P>
							<asp:Label id="ErrorLabel" runat="server" ForeColor="Red"></asp:Label>
							<domval:RegexDomValidator id="RegularExpressionValidator1" runat="server" ErrorMessage="Solo numeri e lettere"
								Display="Dynamic" ControlToValidate="Parameter" ValidationExpression="[a-zA-Z0-9]+"></domval:RegexDomValidator></P>
					</TD>
				</TR>
				<TR>
					<TD>
						<asp:button id="Button_rem" runat="server" Text="Annulla Ultimo Inserimento"></asp:button>
						<asp:button id="Button_save" runat="server" Text="Salva "></asp:button>
					</TD>
				</TR>
			</table>
		</form>
	</body>
</HTML>
