<%@ Page language="c#" Codebehind="QuickAppointments.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.QuickAppointments" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <body>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
    <form id="Form1" method="post" runat="server">
		<asp:Calendar width="138" height="135" ID="calDate" Runat="server" Font-Name="verdana" Font-Size="10px"
									OnSelectionChanged="Change_Date" OnVisibleMonthChanged="Change_Month" OnDayRender="DayRender"
									DayNameFormat="FirstLetter" TodayDayStyle-BackColor="gold" DayHeaderStyle-BackColor="lightsteelblue"
									OtherMonthDayStyle-ForeColor="lightgray" NextPrevStyle-ForeColor="white" TitleStyle-BackColor="gray"
									TitleStyle-ForeColor="white" TitleStyle-Font-Bold="True" TitleStyle-Font-Size="10px" SelectedDayStyle-BackColor="Navy"
									SelectedDayStyle-Font-Bold="True" />
		<div style="position:absolute;
			width:135;
			height:165;
			background-color:#ffffff;
			overflow:auto;">
			<asp:Literal ID="LitAppointmentDetails" Runat=server></asp:Literal>
		</div>
     </form>

  </body>
</html>
