<%@ Register TagPrefix="app" TagName="NewAppointmentControl" Src="~/calendar/Appointment.ascx" %>
<%@ Page language="c#" Codebehind="PopAppointment.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.PopAppointment" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>PopAppointment</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
		<script type="text/javascript" src="/js/common.js"></script>
		<script type="text/javascript" src="/js/autodate.js"></script>
		<script type="text/javascript" src="/js/dynabox.js"></script>
		<script type="text/javascript" src="/js/Appointment.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<app:NewAppointmentControl id="NewAppointmentControl" runat="server" />
		</form>
	</body>
</HTML>
