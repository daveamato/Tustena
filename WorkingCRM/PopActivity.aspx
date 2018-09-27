<%@ Page language="c#" Codebehind="PopActivity.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.WorkingCRM.PopActivity" %>
<%@ Register TagPrefix="act" TagName="ActivityForm" Src="~/quick/ActivityForm.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>PopActivity</title>
  </head>
  <link rel="stylesheet" type="text/css" href="/css/G.css">
	<script type="text/javascript" src="/js/common.js"></script>
	<script type="text/javascript" src="/js/autodate.js"></script>
	<script type="text/javascript" src="/js/dynabox.js"></script>
	<script type="text/javascript" src="/js/Appointment.js"></script>
  <body>
    <form id="Form1" method="post" runat="server">
		<act:ActivityForm id="activityForm" runat="server"/>
     </form>

  </body>
</html>
