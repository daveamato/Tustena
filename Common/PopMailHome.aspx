<%@ Register TagPrefix="mail" TagName="mailout" Src="~/MailingList/WebMail/mailout.ascx" %>
<%@ Page language="c#" Codebehind="PopMailHome.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PopMailHome" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head runat=server>
    <title>PopMailHome</title>
    <LINK href="/css/G.css" type="text/css" rel="stylesheet">
  <script language="javascript" src="/js/common.js"></script>
  <script type="text/javascript" src="/js/dynabox.js"></script>
</HEAD>
  <body>

    <form id="Form1" method="post" runat="server">
		<mail:mailout id="mo" runat="server" />
    </form>

  </body>
</HTML>
