/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function Ajax_GetXMLHttpRequest() { var x = null; if (typeof XMLHttpRequest != "undefined") { x = new XMLHttpRequest(); } else { try { x = new ActiveXObject("Msxml2.XMLHTTP"); } catch (e) { try { x = new ActiveXObject("Microsoft.XMLHTTP"); } catch (e) { }
}
}
return x; }

function Ajax_CallBack(type, id, method, args, clientCallBack, debugRequestText, debugResponseText, debugErrors) { var x = Ajax_GetXMLHttpRequest(); var url = document.location.href.substring(0, document.location.href.length - document.location.hash.length); url += (url.indexOf("?")>0)?"&ajax=1":"?ajax=1"; x.open("POST", url, clientCallBack ? true : false); x.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"); if (clientCallBack) { x.onreadystatechange = function() { if (x.readyState != 4) { return; }
if (debugResponseText) { alert(x.responseText); }
var result = eval("(" + x.responseText + ")"); if (debugErrors && result.error) { alert("error: " + result.error); }
clientCallBack(result); }
}
var encodedData = "Ajax_CallBackType=" + type; if (id) { encodedData += "&Ajax_CallBackID=" + id.split("$").join(":"); }
encodedData += "&Ajax_CallBackMethod=" + method; if (args) { for (var i in args) { encodedData += "&Ajax_CallBackArgument" + i + "=" + encodeURIComponent(args[i]); }
}
if (document.forms.length > 0) { var form = document.forms[0]; for (var i = 0; i < form.length; ++i) { var element = form.elements[i]; if (element.name) { var elementValue = null; if (element.nodeName == "INPUT") { var inputType = element.getAttribute("TYPE").toUpperCase(); if (inputType == "TEXT" || inputType == "PASSWORD" || inputType == "HIDDEN") { elementValue = element.value; } else if (inputType == "CHECKBOX" || inputType == "RADIO") { if (element.checked) { elementValue = element.value; }
}
} else if (element.nodeName == "SELECT") { elementValue = element.value; } else if (element.nodeName == "TEXTAREA") { elementValue = element.value; }
if (elementValue) { encodedData += "&" + element.name + "=" + encodeURIComponent(elementValue); }
}
}
}
if (debugRequestText) { alert(encodedData); }
x.send(encodedData); var result = null; if (!clientCallBack) { if (debugResponseText) { alert(x.responseText); }
result = eval("(" + x.responseText + ")"); if (debugErrors && result.error) { alert("error: " + result.error); }
}
delete x; return result; }
