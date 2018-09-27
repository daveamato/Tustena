<%@ Page Language="c#" Trace="false" codebehind="newlang.aspx.cs" EnableViewState="false" Inherits="Digita.Tustena.Language" AutoEventWireup="false"%>
<HTML>
  <HEAD runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
  </HEAD>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
<table cellspacing=5 cellpadding=5><tr><td>
<span class=normal><b><asp:Label id="TitleLbl" runat="server"/></b></span><br>
</td></tr><tr><td>
<form runat="server">
<p><asp:DropDownList id="MyUICulture" runat="server" Width="280px"/><br>
<input type="hidden" value="2" name="eng">
<font size=2 style="CURSOR:pointer">[<asp:Label id="EngTxt" runat="server"/>]</font></p>
</td></tr><tr><td>
<span class=normal><b><asp:Label id="Examplelbl" runat="server"/></b><br>
<asp:Literal id="Example" runat="server"/></span>
</td></tr><tr><td align="right">
<asp:LinkButton id="SubmitCat" runat="server" class="save"  /></FORM>
</td></tr></table>
</body>
</HTML>
