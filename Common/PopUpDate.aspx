<%@ Page Language="c#" Trace="false" codebehind="PopUpDate.aspx.cs" Inherits="Digita.Tustena.PopUpDate" AutoEventWireup="false"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <head runat=server>
		<title>Set Date</title>
		<script language="javascript" src="/js/common.js"></script>
</HEAD>
	<body topmargin="0" leftmargin="0" marginwidth="0" marginheight="0">
		<center>
			<form id="Form1" method="post" runat="server">
				<asp:Calendar width="195" height="195" ID="calDate" OnSelectionChanged="Change_Date" Runat="server"
					Font-Name="Arial" Font-Size="12px" TodayDayStyle-BackColor="gold" DayHeaderStyle-BackColor="lightsteelblue"
					OtherMonthDayStyle-ForeColor="lightgray" NextPrevStyle-ForeColor="white" TitleStyle-BackColor="gray"
					TitleStyle-ForeColor="white" TitleStyle-Font-Bold="True" TitleStyle-Font-Size="15px" SelectedDayStyle-BackColor="Navy"
					SelectedDayStyle-Font-Bold="True" />
				<input type="hidden" id="control" runat="server">
			</form>
		</center>
	</body>
</HTML>
