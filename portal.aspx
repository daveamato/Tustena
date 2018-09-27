<%@ Page language="c#" Codebehind="portal.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.portal" %>
<html>
<head id="head" runat="server">
<script>
function resizeIframe(frameid){
var currentfr=document.getElementById(frameid)
if (currentfr && !window.opera){
currentfr.style.display="block"
if (currentfr.contentDocument && currentfr.contentDocument.body.offsetHeight) //ns6 syntax
currentfr.height = currentfr.contentDocument.body.offsetHeight+FFextraHeight;
else if (currentfr.Document && currentfr.Document.body.scrollHeight) //ie5+ syntax
currentfr.height = currentfr.Document.body.scrollHeight;
if (currentfr.addEventListener)
currentfr.addEventListener("load", readjustIframe, false)
else if (currentfr.attachEvent){
currentfr.detachEvent("onload", readjustIframe) // Bug fix line
currentfr.attachEvent("onload", readjustIframe)
}
}
}

function readjustIframe(loadevt) {
var crossevt=(window.event)? event : loadevt
var iframeroot=(crossevt.currentTarget)? crossevt.currentTarget : crossevt.srcElement
if (iframeroot)
resizeIframe(iframeroot.id);
}
function init()
{resizeIframe('frm');}
window.onload=init;
</script>
</head>
<body id="body" runat="server">
<form runat="server" ID="Form1">
	<table width="100%" cellspacing="0">
			<tr>
				<td width="140" height="100%" class="SideBorderLinked" valign="top">
					<table width="98%" border="0" cellspacing="0" align="center" cellpadding=0>
					<tr>
					<td align="left" class="BorderBottomTitles" valign=top>
					<span class="divautoform"><b><%=wrm.GetString("Notetxt2")%></b></span>
					</td>
					</tr>
						<tr>
							<td align="right">
								<asp:LinkButton Id="Btnsearch" runat="server" Cssclass="HeaderContacts"  />
							</td>
						</tr>
					</table>
				</td>
				<td valign="top" height="">
					<table width="98%" border="0" cellspacing="0" align="center" cellpadding=0>
					<tr><td>
					<iframe id="frm" width="100%" src="http://www.digita.it" height="100%"></iframe>
					</td></tr></table>
	</td></tr></table>
</form>
</body>
</html>
