<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Page Language="c#" inherits="Digita.Tustena.immagine" CodeBehind="default_Image.aspx.cs"  AutoEventWireup="false"%>
<html>
<head runat=server>
	<title><twc:LocalizedLiteral text="MLImage1" runat="server"/></title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
	<style>
	BODY
		{
		FONT-FAMILY: Verdana;FONT-SIZE: xx-small;
		}
	TABLE
		{
	    FONT-SIZE: xx-small;
	    FONT-FAMILY: Tahoma
		}
	INPUT
		{
		font:8pt verdana,arial,sans-serif;
		}
	select
		{
		height: 22px;
		top:2;
		font:8pt verdana,arial,sans-serif
		}
	.bar
		{
		BORDER-TOP: #99ccff 1px solid; BACKGROUND: #336699; WIDTH: 100%; BORDER-BOTTOM: #000000 1px solid; HEIGHT: 20px
		}
	</style>
</head>
<body onload="checkImage()" link=Blue vlink=MediumSlateBlue alink=MediumSlateBlue leftmargin=5 rightmargin=5 topmargin=5 bottommargin=5 bgcolor=Gainsboro>
<form runat="server">
	<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td valign=top>
				<table border=0 cellpadding=3 cellspacing=3 align=center>
					<tr>
						<td align=center style="BORDER-TOP: #336699 1px solid;BORDER-LEFT: #336699 1px solid;BORDER-RIGHT: #336699 1px solid;BORDER-BOTTOM: #336699 1px solid;" bgcolor=White>
						<div id="divImg" style="overflow:auto;width:150;height:170;visibility:hidden;"></div>
						</td>
  						<td valign=top>
							<table border=0 cellpadding=0 cellspacing=0 width=260>
								<tr>
									<td>
										<div class="bar" style="padding-left: 5px;">
											<font size="2" face="tahoma" color="white"><b><twc:LocalizedLiteral text="MLImage1" runat="server"/></b></font>
										</div>
									</td>
								</tr>
							</table>
							<asp:repeater id="FileList" runat="server">
								<headertemplate>
									<div style="overflow:auto;height:120;width:260;BORDER-LEFT: #316AC5 1px solid;BORDER-RIGHT: LightSteelblue 1px solid;BORDER-BOTTOM: LightSteelblue 1px solid;">
									<table cellpadding="3" cellspacing="0" width="240">
								</headertemplate>
								<itemtemplate>
										<tr>
											<td valign="top">
												<asp:Literal id="FileName" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "filename") %>' />
											</td>
											<td valign="top">
												<%# DataBinder.Eval(Container.DataItem, "filesize", "{0} KB") %>
											</td>
											<td valign="top">
												<span id="Seleziona" class="normal" style="cursor:pointer;text-decoration:underline;" onclick="selectImage('<%# DataBinder.Eval(Container.DataItem, "filename") %>')"><twc:LocalizedLiteral text="MLImage3" runat="server"/></span>
											</td>
											<td valign="top">
												<asp:LinkButton id="Erase" runat="server" commandName="Erase" />
											</td>
										</tr>
								</itemtemplate>
								<footertemplate>
									</table>
									</div>
								</footertemplate>
							</asp:repeater>
							<twc:LocalizedLiteral text="MLImage5" runat="server"/><br>
							<INPUT type="file" id="inpFile" name=inpFile runat="server" size=22 class="autoform"><br>
							<asp:Button id="BtnLoad" runat="server"  />
						</td>
					</tr>
					<tr>
						<td colspan=2>
							<hr>
							<table border=0 width=340 cellpadding=0 cellspacing=1>
								<tr>
									<td>
										<twc:LocalizedLiteral text="MLImage7" runat="server"/>
									</td>
									<td colspan=3>
										<INPUT type="text" id="inpImgURL" name=inpImgURL size="39" />
									</td>
								</tr>
								<tr>
									<td>
										<twc:LocalizedLiteral text="MLImage8" runat="server"/>
									</td>
									<td colspan=3>
										<INPUT type="text" id="inpImgAlt" name=inpImgAlt size=39></td>
								</tr>
								<tr>
									<td>
										<twc:LocalizedLiteral text="MLImage9" runat="server"/>
									</td>
									<td>
										<select ID="inpImgAlign" NAME="inpImgAlign">
											<option value="" selected><twc:LocalizedLiteral text="MLImage10" runat="server"/></option>
											<option value="absBottom"><twc:LocalizedLiteral text="MLImage11" runat="server"/></option>
											<option value="absMiddle"><twc:LocalizedLiteral text="MLImage12" runat="server"/></option>
											<option value="baseline"><twc:LocalizedLiteral text="MLImage13" runat="server"/></option>
											<option value="bottom"><twc:LocalizedLiteral text="MLImage14" runat="server"/></option>
											<option value="left"><twc:LocalizedLiteral text="MLImage15" runat="server"/></option>
											<option value="middle"><twc:LocalizedLiteral text="MLImage16" runat="server"/></option>
											<option value="right"><twc:LocalizedLiteral text="MLImage17" runat="server"/></option>
											<option value="textTop"><twc:LocalizedLiteral text="MLImage18" runat="server"/></option>
											<option value="top"><twc:LocalizedLiteral text="MLImage19" runat="server"/></option>
										</select>
									</td>
									<td>
										<twc:LocalizedLiteral text="MLImage20" runat="server"/>
									</td>
									<td>
										<select id=inpImgBorder name=inpImgBorder>
											<option value=0>0</option>
											<option value=1>1</option>
											<option value=2>2</option>
											<option value=3>3</option>
											<option value=4>4</option>
											<option value=5>5</option>
										</select>
									</td>
								</tr>
								<tr>
									<td>
										<twc:LocalizedLiteral text="MLImage21" runat="server"/>
									</td>
									<td>
										<INPUT type="text" ID="inpImgWidth" NAME="inpImgWidth" size=2></td>
									<td>
										<twc:LocalizedLiteral text="MLImage22" runat="server"/>
									</td>
									<td>
										<INPUT type="text" ID="inpHSpace" NAME="inpHSpace" size=2>
									</td>
								</tr>
								<tr>
									<td>
										<twc:LocalizedLiteral text="MLImage23" runat="server"/>
									</td>
									<td>
										<INPUT type="text" ID="inpImgHeight" NAME="inpImgHeight" size=2>
									</td>
									<td>
										<twc:LocalizedLiteral text="MLImage24" runat="server"/>
									</td>
									<td>
										<INPUT type="text" ID="inpVSpace" NAME="inpVSpace" size=2>
									</td>
								</tr>
							</table>
						</td>
					</tr>
					<tr>
						<td align=center colspan=2>
							<table cellpadding=0 cellspacing=0 align=center>
								<tr>
									<td>
										<INPUT type="button" value="<twc:LocalizedLiteral text="MLImage25" runat="server"/>" onclick="self.close();" style="height: 22px;font:8pt verdana,arial,sans-serif" ID="Button1" NAME="Button1">
									</td>
									<td>
										<span id="btnImgInsert" style="display:none">
										<INPUT type="button" value="<twc:LocalizedLiteral text="MLImage26" runat="server"/>" onclick="InsertImage();self.close();" style="height: 22px;font:8pt verdana,arial,sans-serif" ID="Button2" NAME="Button2">
										</span>
										<span id="btnImgUpdate" style="display:none">
										<INPUT type="button" value="<twc:LocalizedLiteral text="MLImage27" runat="server"/>" onclick="UpdateImage();self.close();" style="height: 22px;font:8pt verdana,arial,sans-serif" ID="Button3" NAME="Button3">
										</span>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
				<br>
			</td>
		</tr>
	</table>
<script language="JavaScript">
function selectImage(sURL)
	{
	document.getElementById("inpImgURL").value = sURL;
	var imgplaceholder = document.getElementById("divImg");
	imgplaceholder.innerHTML = "<img src='' id='idImg'>";
	var img = document.getElementById("idImg");
	img.src="<%=WebEditorUtils.WebUserFilesPath%>/"+sURL;
	var width = img.width
	var height = img.height
	var resizedWidth = 150;
	var resizedHeight = 170;
	var Ratio1 = resizedWidth/resizedHeight;
	var Ratio2 = width/height;

	if(Ratio2 > Ratio1)
		{
		if(width*1>resizedWidth*1)
			img.width=resizedWidth;
		else
			img.width=width;
		}
	else
		{
		if(height*1>resizedHeight*1)
			img.height=resizedHeight;
		else
			img.height=height;
		}

	imgplaceholder.style.visibility = "visible"
	}

/***************************************************
	If you'd like to use your own Image Library :
	- use InsertImage() method to insert image
		Params : url,alt,align,border,width,height,hspace,vspace
	- use UpdateImage() method to update image
		Params : url,alt,align,border,width,height,hspace,vspace
	- use these methods to get selected image properties :
		imgSrc()
		imgAlt()
		imgAlign()
		imgBorder()
		imgWidth()
		imgHeight()
		imgHspace()
		imgVspace()

	Sample uses :
		window.opener.obj1.InsertImage(...[params]...)
		window.opener.obj1.UpdateImage(...[params]...)
		inpImgURL.value = window.opener.obj1.imgSrc()

	Note: obj1 is the editor object.
	We use window.opener since we access the object from the new opened window.
	If we implement more than 1 editor, we need to get first the current
	active editor. This can be done using :

		oName=window.opener.oUtil.oName // return "obj1" (for example)
		obj = eval("window.opener."+oName) //get the editor object

	then we can use :
		obj.InsertImage(...[params]...)
		obj.UpdateImage(...[params]...)
		inpImgURL.value = obj.imgSrc()
***************************************************/
function checkImage()
{
	if(eval(window.opener))
	{
		oName=window.opener.oUtil.oName;
		obj = eval("window.opener."+oName);

		/* preview image */
		if (obj.imgSrc()!="") selectImage(obj.imgSrc());
		document.getElementById("inpImgURL").value = obj.imgSrc();
		document.getElementById("inpImgAlt").value = obj.imgAlt();
		document.getElementById("inpImgAlign").value = obj.imgAlign();
		document.getElementById("inpImgBorder").value = obj.imgBorder();
		document.getElementById("inpImgWidth").value = obj.imgWidth();
		document.getElementById("inpImgHeight").value = obj.imgHeight();
		document.getElementById("inpHSpace").value = obj.imgHspace();
		document.getElementById("inpVSpace").value = obj.imgVspace();

	 	/* If image is selected */
		if (obj.imgSrc()!="")
			btnImgUpdate.style.display="block";
		else
			btnImgInsert.style.display="block";
	}
}
function UpdateImage()
{

	var inpImgURL =  document.getElementById("inpImgURL");
	var inpImgAlt =  document.getElementById("inpImgAlt");
	var inpImgAlign =  document.getElementById("inpImgAlign");
	var inpImgBorder =  document.getElementById("inpImgBorder");
	var inpImgWidth =  document.getElementById("inpImgWidth");
	var inpImgHeight =  document.getElementById("inpImgHeight");
	var inpHSpace =  document.getElementById("inpHSpace");
	var inpVSpace =  document.getElementById("inpVSpace");

	oName=window.opener.oUtil.oName
	eval("window.opener."+oName).UpdateImage("<%=WebEditorUtils.WebUserFilesPath%>" + inpImgURL.value,inpImgAlt.value,inpImgAlign.value,inpImgBorder.value,inpImgWidth.value,inpImgHeight.value,inpHSpace.value,inpVSpace.value);
}
function InsertImage()
{
	var inpImgURL =  document.getElementById("inpImgURL");
	var inpImgAlt =  document.getElementById("inpImgAlt");
	var inpImgAlign =  document.getElementById("inpImgAlign");
	var inpImgBorder =  document.getElementById("inpImgBorder");
	var inpImgWidth =  document.getElementById("inpImgWidth");
	var inpImgHeight =  document.getElementById("inpImgHeight");
	var inpHSpace =  document.getElementById("inpHSpace");
	var inpVSpace =  document.getElementById("inpVSpace");

	oName=window.opener.oUtil.oName;
	eval("window.opener."+oName).InsertImage("<%=WebEditorUtils.WebUserFilesPath%>" + inpImgURL.value, inpImgAlt.value, inpImgAlign.value, inpImgBorder.value, inpImgWidth.value, inpImgHeight.value, inpHSpace.value, inpVSpace.value);
}
/***************************************************/
</script>
<input type=text style="display:none;" id="inpActiveEditor" name="inpActiveEditor" contentEditable=true>
</form>
</body>
</html>
