<%@ Page language="c#" Codebehind="PGparser.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PGParser" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>PopPhone</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
			<script language="javascript" src="/js/common.js"></script>
	</HEAD>
	<body bgcolor="#e5e5e5" leftmargin="2" topmargin="2" marginwidth="2" marginheight="2">
		<form runat="server">
						Incolla la descrizione dell'azienda direttamente dalla pagina di ricerca di PagineGialle.it:<br>
						<asp:TextBox id="pg" TextMode="MultiLine" Width="250" Height="60" cssClass="BoxDesign" Runat="server" /><br>
						<asp:Button Text="Scomponi indirizzo" Runat="server" ID="btn" />
		</form>
	</body>
</HTML>
