<%@ Page language="c#" Codebehind="PopPhone.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PopPhone" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>PopPhone</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
			<script language="javascript" src="/js/common.js"></script>
	</HEAD>
	<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0">
		<form runat="server">
			<table width="98%" border="0" cellspacing="2" align="center" class="normal">
				<tr>
					<td align="left">
						CountryCode:
					</td>
					<td align="left">
						<input type="text" name="CountryCode" value="<%=countryCode%>" id="CountryCode" size="5" style="WIDTH:60px" class="BoxDesign">
					</td>
					<td align="left">
						Prefix:
					</td>
					<td align="left">
						<input type="text" name="Prefix" value="<%=prefix%>" id="Prefix" size="5" style="WIDTH:100px" class="BoxDesign">
					</td>
				</tr>
				<tr>
					<td align="left">
						Number:
					</td>
					<td align="left" colspan="3">
						<input type="text" name="number" value="<%=phoneNumber%>" id="number" size="12" style="WIDTH:200px" class="BoxDesign">
					</td>
					<td align="left">
						<a href="javascript:save()" class="save">OK</a>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
