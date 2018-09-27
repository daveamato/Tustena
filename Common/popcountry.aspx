<%@ Page language="c#" Codebehind="popcountry.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.popcountry" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
<script src="/js/country.js" language="javascript"></script>
</head>

<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
    <form id="globe" method="post" runat="server">
    <select name="region" onChange="populateCountry(document.globe,document.globe.region.options[document.globe.region.selectedIndex].value)">
	<option selected value=''>Select Region</option>
	<option value='asia'>Asia</option>
	<option value='africa'>Africa</option>
	<option value='australia'>Australia</option>
	<option value='europe'>Europe</option>
	<option value='middleeast'>Middle East</option>
	<option value='lamerica'>Latin America</option>
	<option value='namerica'>North America</option>
	<option value='samerica'>South America</option>
	</select>
	<select name="country" onChange="populateUSstate(document.globe,document.globe.country.options[document.globe.country.selectedIndex].text)">
	<option value=''><--------------------</option>
	</select>
     </form>

  </body>
</html>
