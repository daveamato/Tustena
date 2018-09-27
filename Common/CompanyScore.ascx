<%@ Control Language="c#" AutoEventWireup="false" Codebehind="CompanyScore.ascx.cs" Inherits="Digita.Tustena.Common.CompanyScore" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>

<table cellpadding=5 cellspacing=0 class=normal align=center>
	<tr>
		<td align=right>0</td>
		<td align=center>
			<div id="ranker" style="cursor:pointer;" onclick="CreateBox('/Common/PopScoreValues.aspx?render=no&amp;type=0&amp;crossid=<%=CrossID%>',event,300,195)"><span id="rankerindex" class="rankerindex" runat=server></span></div>
		</td>
		<td align=left>100</td>
	</tr>

</table>
