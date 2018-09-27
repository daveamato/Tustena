<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopFileInfo.aspx.cs" Inherits="Digita.Tustena.GetFileInfo" EnableViewState="false" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head runat="server">
    <title>File Info</title>
    		<link rel="stylesheet" type="text/css" href="/css/G.css">
			<script language="javascript" src="/js/common.js"></script>
			<style>
			.download { cursor:pointer; vertical-align: top; }
			</style>
</head>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0">
    <form id="form1" runat="server">
    <h3><twc:LocalizedLiteral runat="server" Text="Dsttxt29" /></h3>
    <div>
    <asp:Label ID="lblRevision" runat=server/>
    </div>
    </form>
</body>
</html>
