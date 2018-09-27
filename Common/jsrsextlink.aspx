<%@ Page language="c#" Codebehind="jsrsextlink.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.jsrsextlink" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
function extlaunch(sw)
{
switch(sw)
{
<%=casecode()%>
}
win = window.open(Repfields(url));
tooltip();
}

function launchmenu2()
{
var txt="";
<%=menucode()%>
//tooltip(txt,true);
showmenu(null,txt)
}

function Trim(s)
{
  if(s.length > 0)
  while ((s.substring(0,1) == ' ') || (s.substring(0,1) == '\n') || (s.substring(0,1) == '\r'))
  {
    s = s.substring(1,s.length);
  }
  if(s.length > 0)
  while ((s.substring(s.length-1,s.length) == ' ') || (s.substring(s.length-1,s.length) == '\n') || (s.substring(s.length-1,s.length) == '\r'))
  {
    s = s.substring(0,s.length-1);
  }
  s=s.replace(new RegExp("&nbsp;",'g'),"");

  return escape(s);
}

function Repfields(s)
{
try{
	var company = Trim(document.getElementById("companyTD").innerHTML);
	var addr = Trim(document.getElementById("addrTD").innerHTML);
	var zip = Trim(document.getElementById("zipTD").innerHTML);
	var city = Trim(document.getElementById("cityTD").innerHTML);
	var phone = Trim(document.getElementById("phoneTD").innerHTML);
	var email = Trim(document.getElementById("EmailTD").innerHTML);
}catch(arg){}

	s=s.replace(new RegExp("tustena.company",'g'),company);
	s=s.replace(new RegExp("tustena.address",'g'),addr);
	s=s.replace(new RegExp("tustena.zip",'g'),zip);
	s=s.replace(new RegExp("tustena.city",'g'),city);
	s=s.replace(new RegExp("tustena.phone",'g'),phone);
	s=s.replace(new RegExp("tustena.email",'g'),email);
	s=s.replace(new RegExp("tustena.language",'g'),JSLang.substring(0,2));
	return s;
}
if (IE4plus||NS6)
document.onclick=dhm; //tooltip;

launchmenu2();
