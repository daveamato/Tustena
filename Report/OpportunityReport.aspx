<%@ Page language="c#" trace="false" Codebehind="OpportunityReport.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.report.OpportunityReport" %>
<script>
function printpage()
{
	window.blur();
	if (window.print)
		{window.print()}
	else
		{alert('<%=wrm.GetString("Prnopptxt8")%>');}
	window.close();
}
</script>
