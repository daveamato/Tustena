<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="Tustena" %>
<%@ Page language="c#" ValidateRequest="false" Codebehind="neweditor.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.MailingList.neweditor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head runat=server>
		<title>FCKeditor - Sample</title>
		<script runat="server" language="C#">
	protected override void OnLoad(EventArgs e)
	{
		FCKeditor1.BasePath = "/webeditor/";
		//FCKeditor1.StyleXmlPath = "../fckstyles.xml";
	}
		</script>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
		<meta name="robots" content="noindex, nofollow">
		<link href="../sample.css" rel="stylesheet" type="text/css">
			<script type="text/javascript" src="/webeditor/fckeditor.js"></script>
			<script type="text/javascript">

function FCKeditor_OnComplete( editorInstance )
{
	window.status = editorInstance.Description ;
}

			</script>
	</HEAD>
	<body>
		<h1>FCKeditor - ASP.Net - Sample 1</h1>
		This sample displays a normal HTML form with an FCKeditor with full features
		enabled.
		<hr>
		<form method="post">
			<FCKeditorV2:FCKeditor id="FCKeditor1" runat="server" value='This is some <strong>sample text</strong>. You are using <a href="http://www.fckeditor.net/">FCKeditor</a>.'></FCKeditorV2:FCKeditor>
			<br>
			<input type="submit" value="Submit">
		</form>
	</body>
</HTML>
