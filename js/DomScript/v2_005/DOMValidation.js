/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */


var _val_agt=navigator.userAgent.toLowerCase(); var _val_is_major=parseInt(navigator.appVersion); var _val_is_ie=((_val_agt.indexOf("msie")!=-1) && (_val_agt.indexOf("opera")==-1)); var _val_isNT=_val_agt.indexOf("windows nt")!=-1; var _val_IE=(document.all); var _val_IE4=(_val_is_ie && (_val_is_major==4) && (_val_agt.indexOf("msie 4")!=-1)); var _val_IE6=(_val_is_ie && (_val_agt.indexOf("msie 6.0")!=-1)); var _val_NS=(document.layers); var _val_DOM=(document.getElementById); var _val_isMac=(_val_agt.indexOf("Mac")==-1); var _val_allString="document."; _val_allString += (_val_IE)?"all.":(_val_DOM)?"getElementById(\"":""; var _val_styleString=(_val_IE)?".style":(_val_DOM)?"\").style":""; var _val_endAllString=(_val_DOM && !_val_IE)?"\")":""; var _val_px=(_val_DOM)?"px":""; 
var Page_DomValidationVer = "2"; var Page_IsValid = true; var Page_BlockSubmit = false; 
function ValidatorUpdateDisplay(val) { var prop = dom_getAttribute(val,"display"); 
var style_str = "", style_prefix = "display: "; 
if (typeof(prop) == "string") { if (prop == "None") { return; }
if (prop == "Dynamic") { style_str = val.isvalid ? "none" : "inline"; val.style.display = style_str; return; }
}
val.style.visibility = val.isvalid ? "hidden" : "visible"; }

var lastControlHighlighted = null; function ErrorHighlight(val, isValid){ var oldbgcolor; if(typeof(isValid) == "undefined")
isValid = val.isvalid; var hlcolor = '#ff8888'; var controlToHighlight = dom_getAttribute(val, "controltohi"); if (typeof(controlToHighlight) != "string")
controlToHighlight = dom_getAttribute(val, "controltovalidate"); if (typeof(controlToHighlight) == "string") { var obj = dom_getElementByID(controlToHighlight); if(typeof(obj.type) != "undefined" && (obj.type=="text" || obj.type=="textarea" || obj.type=="password")){ if(!isValid) { if(obj.style.backgroundColor != hlcolor)
dom_setAttribute(obj, "oldbgcolor", obj.style.backgroundColor); obj.style.backgroundColor = hlcolor; }else if((oldbgcolor = dom_getAttribute(obj, "oldbgcolor"))!=null){ obj.style.backgroundColor=oldbgcolor; }
}
}
}

function ValidatorUpdateIsValid() { var i; for (i = 0; i < Page_Validators.length; i++) { if (!Page_Validators[i].isvalid) { Page_IsValid = false; Page_BlockSubmit = true; ErrorHighlight(Page_Validators[i], false); return; }
}
Page_IsValid = true; }

function ValidatorHookupControl(control, val) { if (control != null)
{ if (typeof(control.Validators) == "undefined") { control.Validators = new Array; var ev = control.onchange; var new_ev; if (typeof(ev) == "function" ) { ev = ev.toString(); new_ev = "if (Page_IsValid || Page_BlockSubmit) {" + ev.substring(ev.indexOf("{") + 1, ev.lastIndexOf("}")) + "}"; }
else { new_ev = ""; }

var func = new Function("ValidatorOnChange('" + control.id + "'); " + new_ev); control.onchange = func; }
control.Validators[control.Validators.length] = val; }
}

function ValidatorGetValue(id) { var control; control = dom_getElementByID(id); 
if (control == null)
return ""; 
if (typeof(control.value) == "string") { return control.value; }

if (typeof(control.tagName) == "undefined" && typeof(control.length) == "number") { var j; for (j=0; j < control.length; j++) { var inner = control[j]; if (typeof(inner.value) == "string" && (inner.type != "radio" || inner.status == true)) { return inner.value; }
}
}
}

function Page_ClientValidate() { var i,ctrl; for (i = 0; i < Page_Validators.length; i++) { ValidatorValidate(Page_Validators[i]); ErrorHighlight(Page_Validators[i]); 
}
ValidatorUpdateIsValid(); ValidationSummaryOnSubmit(); Page_BlockSubmit = !Page_IsValid; return Page_IsValid; }

function ValidatorCommonOnSubmit() { var retValue = !Page_BlockSubmit; 
if (!_val_NS) { if (_val_IE)
if(event!=null)
event.returnValue = retValue; }

Page_BlockSubmit = false; window.status="validator return " + retValue; return retValue; }

function ValidatorOnChange(controlID) { var cont = dom_getElementByID(controlID); var vals = cont.Validators; var i; for (i = 0; i < vals.length; i++) { ValidatorValidate(vals[i]); ErrorHighlight(Page_Validators[i]); }
ValidatorUpdateIsValid(); return Page_IsValid; }

function ValidatorValidate(val) { val.isvalid = true; if (val.enabled != false)
{ if (typeof(val.evalfunc) == "function") { val.isvalid = val.evalfunc(val); }
}
ValidatorUpdateDisplay(val); }

function ValidatorOnLoad() { if (typeof(Page_Validators) == "undefined")
return; 
var i, val; for (i = 0; i < Page_Validators.length; i++) { val = Page_Validators[i]; var evalFunction = dom_getAttribute(val,"evaluationfunction"); if (typeof(evalFunction) == "string") { eval("val.evalfunc = " + evalFunction + ";"); }
var decimalchar = dom_getAttribute(val,"decimalchar"); if (typeof(decimalchar) == "string") { val.decimalchar = decimalchar; }
var groupchar = dom_getAttribute(val,"groupchar"); if (typeof(groupchar) == "string") { val.groupchar = groupchar; }
var digits = dom_getAttribute(val,"digits"); if (typeof(digits) == "string") { val.digits = digits; }

var isValidAttribute = dom_getAttribute(val,"isvalid"); if (typeof(isValidAttribute) == "string") { if (isValidAttribute == "False") { val.isvalid = false; Page_IsValid = false; }
else { val.isvalid = true; }
} else { val.isvalid = true; }
var enabledAttribute = dom_getAttribute(val,"enabled"); if (typeof(enabledAttribute) == "string") { val.enabled = (enabledAttribute != "False"); } else { val.enabled = true; }
var controlToValidate = dom_getAttribute(val,"controltovalidate"); if (typeof(controlToValidate) == "string") { ValidatorHookupControl(dom_getElementByID(controlToValidate), val); }
var controlhookup = dom_getAttribute(val,"controlhookup"); if (typeof(controlhookup) == "string") { if (controlhookup != "")
{ ValidatorHookupControl(dom_getElementByID(controlhookup), val); }
}
}
Page_ValidationActive = true; if (!Page_IsValid)
ValidationSummaryOnSubmit(); 
if (_val_IE4)
{ var ev = new Function("ValidationSummaryOnSubmit();"); document.onreadystatechange=ev; }

}

function RegularExpressionValidatorEvaluateIsValid(val) { var value = ValidatorGetValue(dom_getAttribute(val, "controltovalidate")); if (value == "")
return true; var rx = new RegExp(dom_getAttribute(val, "validationexpression")); var matches = rx.exec(value); return (matches != null && value == matches[0]); }


function InsensitiveRegularExpressionValidatorEvaluateIsValid(val) { var value = ValidatorGetValue(dom_getAttribute(val, "controltovalidate")); if (value == "")
return true;     var rx = new RegExp(dom_getAttribute(val, "validationexpression"), 'i');        
var matches = rx.exec(value); return (matches != null && value == matches[0]); }


function ValidatorTrim(s) { 
return s.replace(/^\s+|\s+$/g, ""); }

function RequiredFieldValidatorEvaluateIsValid(val) { return (ValidatorTrim(ValidatorGetValue(dom_getAttribute(val, "controltovalidate"))) != ValidatorTrim(dom_getAttribute(val, "initialvalue"))); }



function ValidatorCompare(operand1, operand2, operator, val) { 
var dataType = dom_getAttribute(val, "type"); 
var op1, op2; if ((op1 = ValidatorConvert(operand1, dataType, val)) == null){ return false; }
if (operator == "DataTypeCheck")
return true; if ((op2 = ValidatorConvert(operand2, dataType, val)) == null)
return true; if (op2 == "")
return true; 
switch (operator) { case "NotEqual":
return (op1 != op2); case "GreaterThan":
return (op1 > op2); case "GreaterThanEqual":
return (op1 >= op2); case "LessThan":
return (op1 < op2); case "LessThanEqual":
return (op1 <= op2); default:
return (op1 == op2); }
}



function CompareValidatorEvaluateIsValid(val) { var ctrl = dom_getAttribute(val, "controltovalidate"); if (null == ctrl)
return true; var value = ValidatorGetValue(ctrl); if (ValidatorTrim(value).length == 0)
return true; var compareTo = ""; 
var hookupCtrl = dom_getAttribute(val, "controlhookup"); var useCtrlToValidate = false; if (hookupCtrl != null)
{ if (typeof(hookupCtrl) == "string")
{ if (hookupCtrl != "")
useCtrlToValidate = true; }
}

if (!useCtrlToValidate) { var ctrl_literal = dom_getAttribute(val, "valuetocompare"); if (typeof(ctrl_literal) == "string") { compareTo = ctrl_literal; }
}
else { compareTo = ValidatorGetValue(dom_getAttribute(val, "controlhookup")); }
operator = dom_getAttribute(val, "operator"); return ValidatorCompare(value, compareTo, operator, val); }

function CustomValidatorEvaluateIsValid(val) { var value = ""; var ctrl = dom_getAttribute(val, "controltovalidate"); if (typeof(ctrl) == "string") { if (ctrl != "") { value = ValidatorGetValue(ctrl); if (value == "")
return true; }
}
var valid = true; var func_str = dom_getAttribute(val, "clientvalidationfunction"); if (typeof(func_str) == "string") { if (func_str != "") { eval("valid = (" + func_str + "(val, value) != false);"); }
}
return valid; }

function RangeValidatorEvaluateIsValid(val) { var value; var ctrl = dom_getAttribute(val, "controltovalidate"); if (typeof(ctrl) == "string") { if (ctrl != "") { value = ValidatorGetValue(ctrl); if (value == "")
return true; }
}

var minval = dom_getAttribute(val,"minimumvalue"); var maxval = dom_getAttribute(val,"maximumvalue"); 
if (minval == null && maxval == null)
return true; 
if (minval == "")
minval = 0; if (maxval == "")
maxval = 0; 
return ( (parseFloat(value) >= parseFloat(minval)) && (parseFloat(value) <= parseFloat(maxval))); }

function ValidatorConvert(op, dataType, val) { function GetFullYear(year) { return (year + parseInt(val.century)) - ((year < val.cutoffyear) ? 0 : 100); }
var num, cleanInput, m, exp; if (dataType == "Integer") { exp = /^\s*[-\+]?\d+\s*$/; if (op.match(exp) == null)
return null; num = parseInt(op, 10); return (isNaN(num) ? null : num); }
else if(dataType == "Double") { exp = new RegExp("^\\s*([-\\+])?(\\d+)?(\\" + val.decimalchar + "(\\d+))?\\s*$"); m = op.match(exp); if (m == null)
return null; cleanInput = (m[1]!=null ? m[1] : "") + (m[2].length>0 ? m[2] : "0") + "." + (m[4]!=null ? m[4] : ""); num = parseFloat(cleanInput); return (isNaN(num) ? null : num); }
else if (dataType == "Currency") { exp = new RegExp("^\\s*([-\\+])?(((\\d+)\\" + val.groupchar + ")*)(\\d+)"
+ ((val.digits > 0) ? "(\\" + val.decimalchar + "(\\d{1," + val.digits + "}))?" : "")
+ "\\s*$"); m = op.match(exp); if (m == null)
return null; var intermed = m[2] + m[5] ; cleanInput = m[1] + intermed.replace(new RegExp("(\\" + val.groupchar + ")", "g"), "") + ((val.digits > 0) ? "." + m[7] : 0); num = parseFloat(cleanInput); return (isNaN(num) ? null : num); }
else if (dataType == "Date") { var yearFirstExp = new RegExp("^\\s*((\\d{4})|(\\d{2}))([-./])(\\d{1,2})\\4(\\d{1,2})\\s*$"); m = op.match(yearFirstExp); var day, month, year; if (m != null && (m[2].length == 4 || val.dateorder == "ymd")) { day = m[6]; month = m[5]; year = (m[2].length == 4) ? m[2] : GetFullYear(parseInt(m[3], 10))
}
else { if (val.dateorder == "ymd"){ return null; }
var yearLastExp = new RegExp("^\\s*(\\d{1,2})([-./])(\\d{1,2})\\2((\\d{4})|(\\d{2}))\\s*$"); m = op.match(yearLastExp); if (m == null) { return null; }
if (val.dateorder == "mdy") { day = m[3]; month = m[1]; }
else { day = m[1]; month = m[3]; }
year = (m[5].length == 4) ? m[5] : GetFullYear(parseInt(m[6], 10))
}
month -= 1; var date = new Date(year, month, day); return (typeof(date) == "object" && year == date.getFullYear() && month == date.getMonth() && day == date.getDate()) ? date.valueOf() : null; }
else { return op.toString(); }
}


function ValidationSummaryOnSubmit() { if (typeof(Page_ValidationSummaries) == "undefined")
return; var summary, sums, s, summ_attrib, hdr_txt, err_msg; for (sums = 0; sums < Page_ValidationSummaries.length; sums++) { summary = Page_ValidationSummaries[sums]; summary.style.display = "none"; if (!Page_IsValid) { summ_attrib = dom_getAttribute(summary, "showsummary"); if (summ_attrib != "False") { summary.style.display = ""; if (typeof(summary.displaymode) != "string") { summary.displaymode = "BulletList"; }
switch (summary.displaymode) { case "List":
headerSep = "<br>"; first = ""; pre = ""; post = "<br>"; final_block = ""; break; 
case "BulletList":
default:
headerSep = ""; first = "<ul>"; pre = "<li>"; post = "</li>"; final_block = "</ul>"; break; 
case "SingleParagraph":
headerSep = " "; first = ""; pre = ""; post = " "; final_block = "<br>"; break; }
s = ""; hdr_txt = dom_getAttribute(summary, "headertext"); if (typeof(hdr_txt) == "string") { s += hdr_txt + headerSep; }
var cnt=0; s += first; for (i=0; i<Page_Validators.length; i++) { err_msg = dom_getAttribute(Page_Validators[i], "errormessage"); if (!Page_Validators[i].isvalid && typeof(err_msg) == "string") { if (err_msg != "") { cnt++; s += pre + err_msg + post; }
}
}
s += final_block; 
if (_val_IE4)
{ if (document.readyState == "complete")
{ summary.innerHTML  = s; window.scrollTo(0,0); summary.style.visibility = "visible"; }
} else
{ summary.innerHTML = s; window.scrollTo(0,0); summary.style.visibility = "visible"; }
}
summ_attrib = dom_getAttribute(summary, "showmessagebox"); 
if (summ_attrib == "True") { s = ""; hdr_txt = dom_getAttribute(summary, "headertext"); if (typeof(hdr_txt) == "string") { s += hdr_txt + "\n"; }
for (i=0; i<Page_Validators.length; i++) { err_msg = dom_getAttribute(Page_Validators[i], "errormessage"); if (!Page_Validators[i].isvalid && typeof(err_msg) == "string") { switch (summary.displaymode) { case "List":
s += err_msg + "\n"; break; 
case "BulletList":
default:
s += "  - " + err_msg + "\n"; break; 
case "SingleParagraph":
s += err_msg + " "; break; }
}
}
alert(s); }
}
}
}


function dom_getAttribute(control,attribute)
{ var attrib; if (_val_DOM)
attrib = control.getAttribute(attribute, false); else
attrib = eval(_val_allString + control.id + "." + attribute + _val_endAllString); return attrib; }

function dom_setAttribute(control,attribute,val)
{ if (_val_DOM)
control.setAttribute(attribute, val); }

function dom_getElementByID(id)
{ var element = eval(_val_allString + id + _val_endAllString); return element; }
