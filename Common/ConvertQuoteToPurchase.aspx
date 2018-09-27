<%@ Register TagPrefix="Bill" TagName="BillControl" Src="~/common/BillControl.ascx" %>
<%@ Page language="c#" Codebehind="ConvertQuoteToPurchase.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.ConvertQuoteToPurchase" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>ConvertQuoteToPurchase</title>
	</HEAD>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
	<script language="javascript" src="/js/common.js"></script>
	<script language="javascript" src="/js/jslangspec.js"></script>
	<script language="javascript" src="/js/dynabox.js"></script>

	<body>
		<form id="Form1" method="post" runat="server">
			<Bill:BillControl id="BillC" runat="server"></Bill:BillControl>
		</form>
	</body>
</HTML>
