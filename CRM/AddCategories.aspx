<%@ Page Language="c#" Trace="false" codebehind="AddCategories.aspx.cs" Inherits="Digita.Tustena.AddCategories"  AutoEventWireup="false"%>
<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
<script language="javascript" src="/js/common.js"></script>
</head>

<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" scroll=no>

<form runat="server" ID="Form1">
			<table class="tblstruct normal">
				<tr>
					<td>
						<%=wrm.GetString("CRMcontxt46")%>
					</td>
				</tr>
				<tr>
					<td>
						<asp:TextBox ID="Category" runat="server" class="BoxDesign" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:RadioButtonList id="RadioButtonList1" runat="server" class="normal" />
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:LinkButton id="SubmitCat" runat="server" class="save" Text="OK"  />
					</td>
				</tr>
			</table>
			<asp:Literal id="SomeJS" runat="server" />

</form>
</body>
</html>
