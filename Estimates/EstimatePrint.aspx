<%@ Page language="c#" Codebehind="EstimatePrint.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Estimates.EstimatePrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>EstimatePrint</title>
  </head>
  <style>
	.normal{
		font-family : Verdana;
		font-size: 10px;
		color: #000000;
		}
  </style>
  <script>
	function printpage()
	{
		//window.blur();
		if (window.print)
			{window.print()}
		else
			{alert('<%=wrm.GetString("Prnopptxt8")%>');}
		//window.close();
	}
  </script>
  <body onload="printpage();" bgcolor="#FFFFFF" leftmargin="0" topmargin="0" marginwidth="0" marginheight="0" >
    <form id="Form1" method="post" runat="server">
		<asp:Label ID="Result" Runat=server EnableViewState="false"></asp:Label>
     </form>
  </body>
</html>
