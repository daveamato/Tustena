<%@ Page language="c#" Codebehind="sendsms.aspx.cs" AutoEventWireup="True" Inherits="Digita.Tustena.MailingList.sendsms" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>sendsms</title>
<script language="Javascript">


function message_onChange(obj)
{	var msg = obj.value;//document.submitform.message.value;
	var cleft;
	msg = msg.replace('\n', ' ');
	msg = msg.replace('\r', '');
	msg = msg.replace('?', 'o\'');
	msg = msg.replace('?', 'a\'');
	msg = msg.replace('?', 'e\'');
	msg = msg.replace('?', 'e\'');
	msg = msg.replace('?', 'u\'');
	msg = msg.replace('?', 'i\'');
	msg = msg.replace('?', 'ae');
	msg = msg.replace('?', 'oe');
	msg = msg.replace('?', 'aa');
	msg = msg.replace('?', 'Ae');
	msg = msg.replace('?', 'Oe');
	msg = msg.replace('?', 'Aa');
	cleft = 161-msg.length-obj.value.length;
	if (cleft < 0) {
		alert("Hai finito i caratteri.\n");
		obj.value=obj.value.substr(0,160);
	}else
		document.getElementById("counter").innerHTML = cleft;
		obj.value=msg;

}
</script>
</head>
		<form id="Form1" method="post" runat="server">
			<table>
				<tr>
					<td class="normal">Phone number:</td>
				</tr>
				<tr>
					<td><asp:textbox  class="Inputautoform" id="TxtNumber" runat="server" style="width:200px"></asp:textbox></td>
				</tr>
				<tr>
					<td><table width="100%"><tr><td class="normal">Message text:</td><td align="right" class="normal">You have <span id=counter>160</span> characters left.</td></tr></table></td>
				</tr>
				<tr>
					<td><asp:textbox id="TxtMessage" runat="server" TextMode="MultiLine" Columns=40 Rows=5></asp:textbox></td>
				</tr>
				<tr>
					<td align=right><asp:linkbutton  class="normal" id="BtnSend" runat="server" Text="Submit"></asp:linkbutton></td>
				</tr>
				<tr>
					<td><asp:label  class="normal" id="MsgLabel" runat="server" ForeColor="#ff0000"></asp:label></td>
				</tr>
			</table>
			</TABLE></form>
