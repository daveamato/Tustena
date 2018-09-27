<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Page language="c#" Codebehind="QBEdit.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.CRM.QBEdit" %>
	<html>
<head id="head" runat="server">
<script type="text/javascript" src="/js/dynabox.js"></script>
	<script language="javascript">

var active=false;
var LockArry = new Array;
var FreeArry = new Array;

function lockid(obj){
for(var i=0; i<LockArry.length; i++) {
	if("id"+LockArry[i]==obj.id) return true;
	}
	return false;
}

function contract(){
	var exists;
	for(var i=0; i<LockArry.length; i++) {
		document.getElementById("id"+LockArry[i]).style.display = "none";
		document.getElementById("img"+LockArry[i]).src = "/images/last.gif";
		exists=false;
		for(var y=0; y<FreeArry.length; y++){
			if(FreeArry[y]==LockArry[i]){
				exists=true;
				break;
			}
		}
		if(!exists){
			FreeArry[FreeArry.length]=LockArry[i];
		}
	}

	for(var y=0; y<FreeArry.length; y++){
		exists=false;
		for(var i=0; i<LockArry.length; i++){
			if(LockArry[i]==FreeArry[y]){
				exists=true;
				break;
			}
		}
		if(!exists)document.getElementById("img"+FreeArry[y]).src = "/images/Rplus.gif";
	}
}

function showhide(obj,img) {
 if(lockid(obj))return;
 var xx;
 var newmenu="";
 if (obj.style.display == "none")
 {
  obj.style.display = "block";
  img.src = "/images/Rminus.gif";
 }
 else
 {
	if (obj.style.display == "block")
	{
		obj.style.display = "none";
		img.src = "/images/Rplus.gif";
	}
 }
  /*
   	for(xx=0;xx<MenuArry.length-1;xx+=2)
   	{
 		if (obj.id.substring(2)==MenuArry[xx])
 		{
 			if (MenuArry[xx+1]=="0")
 			{
				newmenu = newmenu + obj.id.substring(2) + ",1,";
 			}
 			else
 			{
 				newmenu = newmenu + obj.id.substring(2) + ",0,";
 			}
 		}
 		else
 		{
 			newmenu = newmenu + MenuArry[xx] + "," + MenuArry[xx+1] + ",";
 		}
 	}
 	*/
 	//alert(newmenu);
 	//DeleteCookie("StatoMenu")
 	//SetCookie ("StatoMenu", newmenu)
	//MenuArry = new Array;
	//MenuArry = newmenu.split(",");
}

function GetCookie(name) {
 var dc = document.cookie;
 var prefix = name + "=";
 var begin = dc.indexOf("; " + prefix);
 if (begin == -1) {
   begin = dc.indexOf(prefix);
   if (begin != 0) return null;
 } else
   begin += 2;
 var end = document.cookie.indexOf(";", begin);
 if (end == -1)
   end = dc.length;
 return unescape(dc.substring(begin + prefix.length, end));
}


function SetCookie (name, value) {
var argv = SetCookie.arguments;
var argc = SetCookie.arguments.length;
var expires = (argc > 2) ? argv[2] : null;
var path = (argc > 3) ? argv[3] : null;
var domain = (argc > 4) ? argv[4] : null;
var secure = (argc > 5) ? argv[5] : false;
document.cookie = name + "=" + value;
/*
 + ((expires == null) ? "" : ("; expires=" + expires.toGMTString())) +
((path == null) ? "" : ("; path=" + path)) +
((domain == null) ? "" : ("; domain=" + domain)) +
((secure == true) ? "; secure" : "");
*/
}

function DeleteCookie (name) {
var exp = new Date();
exp.setTime (exp.getTime() - 1);
//var cval = GetCookie (name);
//document.cookie = name + "=" + cval + "; expires=" + exp.toGMTString();
document.cookie = name + "=; expires=" + exp.toGMTString();
}

	function GetIndex(t)
	{
	if(active==false) return;
		active=false;
		var IFrameObj;
		if (IE4plus) IFrameObj = window.document.frames['Card'].document;
		 else IFrameObj = window.document.getElementById('Card').contentDocument;
		var nr = IFrameObj.getElementById("AddNewRow");
		var NewRow = IFrameObj.getElementById("NewRow");
		nr.value=t;
		IFrameObj.location=NewRow.href;
		//clickElement(NewRow);
	}

	function SetParams(p,v)
	{
		var IFrameObj;
		if (IE4plus) IFrameObj = window.document.frames['Card'].document;
		 else IFrameObj = window.document.getElementById('Card').contentDocument;
		var param = IFrameObj.getElementById(p);
		param.value=v;
	}
	</script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0">
			<tr>
			<td width="140" class="SideBorderLinked" valign="top">
				<table width="98%" border="0" cellspacing="0" cellpadding=0>
					<tr>
                   		<td class="sideContainer">
						<div class="sideTitle"><%=wrm.GetString("QBUtxt4")%></div>
                        <a href="javascript:location.href='QBEdit.aspx?m=55&si=42&n=1'" class="sidebtn"><%=wrm.GetString("QBUtxt11")%></a>
                        <a href="javascript:location.href='QBDefault.aspx?m=55&si=42'" class="sidebtn"><%=wrm.GetString("QBUtxt12")%></a>
						</td>
					</tr>
				</table>
			</td>
			<td valign="top" align="right">
				<table width="100%" height="700" border="0" cellspacing="0">
				<tr>
					<td width="30%" valign="top">
						<table width="100%" border="0" cellspacing="0" cellpadding=0>
							<tr><td class="GridTitle"><%=wrm.GetString("QBUtxt5")%></td></tr>
							<tr><td>
							<asp:Literal id="MyTreeView" runat="server"/>
							</td></tr>
						</table>
					</td>
					<td width="70%" height="100%">
						<iframe id="Card" runat=server ALLOWTRANSPARENCY="true" width="100%" height="100%" src="/crm/qbtable.aspx?render=no" scrolling=no frameBorder=0 marginHeight=0 marginWidth=0></iframe>
					</td>
				</tr>
				</table>
			</td>
			</tr>
			</table>
		</form>

</body>
</html>
