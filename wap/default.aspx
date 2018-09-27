<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>
<%@ Register TagPrefix="MobileDynamicImage" Namespace="MobileDynamicImage" Assembly="MobileDynamicImage" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" Inherits="Digita.Tustena.Wap.Mobile" AutoEventWireup="false" trace="false" %>
<head runat=server>
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="http://schemas.microsoft.com/Mobile/Page" name="vs_targetSchema">
</HEAD>
<body Xmlns:mobile="http://schemas.microsoft.com/Mobile/WebForm">
	<mobile:form id="TustenaWL" runat="server">
		<P>
			<mobile:Label id="Text" runat="server" Alignment="Center" ForeColor="#ff0000">Tustena Wireless</mobile:Label>
		</P>
		<P>
			<mobile:Panel id="LoginPanel" runat="server">
				<P>
					<mobile:Label id="LError" runat="server" ForeColor="Red" Visible="False"></mobile:Label>
					<mobile:Label id="L1" runat="server">Username</mobile:Label>
					<mobile:TextBox id="Tusername" runat="server"></mobile:TextBox>
				</P>
				<P>
					<mobile:Label id="L2" runat="server">Password</mobile:Label>
					<mobile:TextBox id="Tpassword" runat="server"></mobile:TextBox>
				</P>
				<P>
					<mobile:Command id="Blogin" runat="server">Login</mobile:Command>
				</P>
			</mobile:Panel>
			<mobile:Panel id="MenuPanel" runat="server" Visible="False">
				<P>
					<mobile:Label id="Lwelcome" runat="server"></mobile:Label>
					<mobile:Label id="Lwelcome2" runat="server"></mobile:Label>
				</P>
				<P>
					<mobile:Command id="CalToday" runat="server">Oggi</mobile:Command>
				</P>
				<P>
					<mobile:Command id="CalTomorrow" runat="server">Domani</mobile:Command>
				</P>
				<P>
					<mobile:Command id="CallCal" runat="server">Calendario</mobile:Command>
				</P>
				<P>
					<mobile:Command id="Contacts" runat="server">Contatti</mobile:Command>
				</P>
				<P>
					<mobile:Command id="Boff" runat="server">Back</mobile:Command>
				</P>
			</mobile:Panel>
			<mobile:Panel id="CalPanel" runat="server" Visible="False">
				<mobile:Calendar id="MobileCalendar" runat="server" OnSelectionChanged="Calendar_SelectionChanged"></mobile:Calendar>
			</mobile:Panel>
			<mobile:Panel id="SearchPanel" runat="server" Visible="False">
				<mobile:Label id="ContLabel" runat="server">Cerca:</mobile:Label>
				<mobile:TextBox id="ContQuery" runat="server"></mobile:TextBox>
				<mobile:Command id="ContSearchAz" runat="server">Cerca Aziende</mobile:Command>
				<mobile:Command id="ContSearch" runat="server">Cerca Contatti</mobile:Command>
				<mobile:Command id="ContSearchLe" runat="server">Cerca Lead</mobile:Command>
				<mobile:Label id="SearchInfo" runat="server" ForeColor="#ff0000"></mobile:Label>
			</mobile:Panel>
			<mobile:Panel id="CompanyPanel" runat="server" Visible="False">
				<mobile:Label id="CompanyTitle" runat="server"></mobile:Label>
				<mobile:Objectlist id="CompanyList" runat="server" Visible="False" CommandStyle-StyleReference="subcommand"
					LabelStyle-StyleReference="title"></mobile:Objectlist>
			</mobile:Panel>
			<mobile:Panel id="AppPanel" runat="server" Visible="False">
				<mobile:Label id="AppTitle" runat="server"></mobile:Label>
				<mobile:ObjectList id="AppList" runat="server" Visible="False" CommandStyle-StyleReference="subcommand"
					LabelStyle-StyleReference="title"></mobile:ObjectList>
			</mobile:Panel>
			<mobile:Panel id="ContactPanel" runat="server" Visible="False">
				<mobile:Label id="ContactTitle" runat="server"></mobile:Label>
				<mobile:ObjectList id="ContactList" runat="server" Visible="False" CommandStyle-StyleReference="subcommand"
					LabelStyle-StyleReference="title"></mobile:ObjectList>
			</mobile:Panel>
			<mobile:Panel id="LeadPanel" runat="server" Visible="False">
				<mobile:Label id="LeadTitle" runat="server"></mobile:Label>
				<mobile:ObjectList id="LeadList" runat="server" Visible="False" CommandStyle-StyleReference="subcommand"
					LabelStyle-StyleReference="title"></mobile:ObjectList>
			</mobile:Panel>
			<P>
				<mobile:Command id="Bback" runat="server" Visible="False">Back</mobile:Command>
			</P>
	</mobile:form>
</body>
