<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page Language="c#" Trace="false" codebehind="search.aspx.cs" Inherits="Digita.Tustena.SearchDataStorage" EnableViewState="false" AutoEventWireup="True" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>Search</title>
	</HEAD>
	<body>
		<form id="Default" method="post" runat="server">
			<p><asp:label id="LblQuery" runat="server" Width="100px">Query:</asp:label><asp:textbox id="txtQuery" accessKey="Q" runat="server" Width="300px"></asp:textbox><domval:RequiredDomValidator id="reqValQuery" runat="server" ErrorMessage="RequiredFieldValidator" Display="Dynamic"
					ControlToValidate="txtQuery">You must specify a query.</domval:RequiredDomValidator><br>
				<asp:label id="LblQueryType" runat="server" Width="100px">Query Type:</asp:label><asp:dropdownlist id="cboQueryType" accessKey="T" runat="server" Width="300px">
					<asp:ListItem Value="All" Selected="True">All Words</asp:ListItem>
					<asp:ListItem Value="Any">Any Words</asp:ListItem>
					<asp:ListItem Value="Boolean">Boolean Expression</asp:ListItem>
					<asp:ListItem Value="Exact">Exact Expression</asp:ListItem>
					<asp:ListItem Value="Natural">Natural Language</asp:ListItem>
				</asp:dropdownlist><br>
				<asp:label id="LblDirectory" runat="server" Width="100px">Directory:</asp:label><asp:dropdownlist id="cboDirectory" accessKey="D" runat="server" Width="300px">
					<asp:ListItem Value="/" Selected="True">Entire Site</asp:ListItem>
					<asp:ListItem Value="/Products">Products</asp:ListItem>
					<asp:ListItem Value="/Products/App1">Products: App1</asp:ListItem>
					<asp:ListItem Value="/Products/App2">Products: App2</asp:ListItem>
					<asp:ListItem Value="/Services">Services</asp:ListItem>
					<asp:ListItem Value="/Help">Help</asp:ListItem>
				</asp:dropdownlist><br>
				<asp:label id="LblSortOrder" runat="server" Width="100px">Sort Order:</asp:label><asp:dropdownlist id="cboSortBy" accessKey="S" runat="server" Width="135px">
					<asp:ListItem Value="Rank" Selected="True">Search Rank</asp:ListItem>
					<asp:ListItem Value="DocTitle">Document Title</asp:ListItem>
					<asp:ListItem Value="Write">Last Modified</asp:ListItem>
				</asp:dropdownlist><asp:dropdownlist id="CboSortOrder" runat="server" Width="100px">
					<asp:ListItem Value="ASC" Selected="True">Ascending</asp:ListItem>
					<asp:ListItem Value="DESC">Descending</asp:ListItem>
				</asp:dropdownlist><asp:button id="BtnSearch" runat="server" Text="Search"></asp:button></p>
			<p><asp:label id="LblResultCount" runat="server" Font-Italic="True" visible="False">X documents were found:</asp:label></p>
			<p><asp:datagrid id="DgResultsGrid" runat="server" PageSize="25" AllowPaging="True" AutoGenerateColumns="False"
					Visible="False" GridLines="None">
					<itemstyle horizontalalign="Left" verticalalign="Top"></itemstyle>
					<headerstyle font-bold="True"></headerstyle>
					<columns>
						<asp:TemplateColumn HeaderText="Rank">
							<headerstyle width="60px"></headerstyle>
							<itemtemplate>
								<asp:Literal id=Label1 runat="server" Text='<%# ((int)DataBinder.Eval(Container, "DataSetIndex")) + 1 %>'>
								</asp:Literal>
							</itemtemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Document Information">
							<itemstyle horizontalalign="Left" verticalalign="Top"></itemstyle>
							<itemtemplate>
								<p>
									<asp:hyperlink runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "VPath")%>'>
										<%# GetTitle(Container.DataItem)%>
									</asp:hyperlink><br>
									
										<%# GetCharacterization(Container.DataItem)%>
									<br>
									<i>
										<asp:hyperlink runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "VPath")%>'>http://<%# Request.ServerVariables["SERVER_NAME"]%><%# DataBinder.Eval(Container.DataItem, "VPath")%></asp:hyperlink>
										- Last Modified:
										
											<%# DataBinder.Eval(Container.DataItem, "Write")%>
										</i></p>
							</itemtemplate>
						</asp:TemplateColumn>
					</columns>
					<pagerstyle mode="NumericPages"></pagerstyle>
				</asp:datagrid></p>
		</form>
	</body>
</HTML>
