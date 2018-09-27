<%@ Page language="c#" Codebehind="uploadDialog.aspx.cs" AutoEventWireup="false" Inherits="UploadSpike.upload" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>upload</title>
		<script language="Javascript" src="/js/common.js"></script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<center>
				<div><STRONG>UPLOADING...</STRONG></div>
				<div style="BORDER-RIGHT: #006000 1px solid; PADDING-RIGHT: 1px; BORDER-TOP: #006000 1px solid; PADDING-LEFT: 1px; FONT-SIZE: 1px; PADDING-BOTTOM: 1px; BORDER-LEFT: #006000 1px solid; WIDTH: 300px; PADDING-TOP: 1px; BORDER-BOTTOM: #006000 1px solid; HEIGHT: 15px; TEXT-ALIGN: left"><span id="progressBar" style="HEIGHT: 100%; BACKGROUND-COLOR: #a00000"></span></div>
				<div><span id="dataText"></span></div>
			</center>
			<script id="jsProgress"></script>
			<script>
				function Progress(uploadid)
				{
					self.focus();
	//				loadScript('/datastorage/UploadStatus.aspx?uploadId=' + uploadid + '&' + Math.random());
					loadScript('/neatupload/jsupload.aspx?'+document.search+'&' + Math.random());

					setTimeout("Progress('"+uploadid+"')", 2000);
				}
			</script>
		</form>
	</body>
</HTML>
