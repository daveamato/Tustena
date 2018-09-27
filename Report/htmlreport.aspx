<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Page language="c#" Trace="false" Codebehind="htmlreport.aspx.cs" Inherits="Digita.Tustena.report.HTMLReport" AutoEventWireup="false"%>
<script>
function printpage()
{
	window.blur();
	if (window.print)
		{window.print()}
	else
		{alert('<%=alertMsg%>');}
	window.close();
}
</script>

