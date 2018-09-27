/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function extlaunch(sw)
{ company = Trim(document.getElementById("companyTD").innerText); addr = Trim(document.getElementById("addrTD").innerText); zip = Trim(document.getElementById("zipTD").innerText); city = Trim(document.getElementById("cityTD").innerText); phone = Trim(document.getElementById("phoneTD").innerText); switch(sw)
{ case 1: url = "http://www.google.com/search?hl=en&ie=UTF-8&q=" + company; break; case 2: url = "http://www.paginegialle.it/pg/cgi/pgsearch.cgi?btt=1&ts=1&l=1&cb=0&ind=&nc=&qs=" + company + "&dv=" + cap; break; case 3: url = "http://www.maporama.it/share/Map.asp?COUNTRYCODE=IT&_XgoGCAddress=" + addr + "&Zip=" + zip + "&State=&_XgoGCTownName=" + city; break; case 4: url = "http://www.paginebianche.it/pb/numero?btt=1&nt=" + phone; break; }
win = window.open(url); tooltip(); }

function launchmenu2()
{ var txt=""; txt+= "<a href=\"javascript:extlaunch(1)\">Cerca con Google</a><br>"
txt+= "<a href=\"javascript:extlaunch(2)\">Cerca su Pagine Gialle</a><br>"
txt+= "<a href=\"javascript:extlaunch(3)\">Cerca su Maporama</a><br>"
txt+= "<a href=\"javascript:extlaunch(4)\">Cerca il Numero</a>"
tooltip(txt,true); }

function Trim(s)
{ if(s.length > 0)
while ((s.substring(0,1) == ' ') || (s.substring(0,1) == '\n') || (s.substring(0,1) == '\r'))
{ s = s.substring(1,s.length); }
if(s.length > 0)
while ((s.substring(s.length-1,s.length) == ' ') || (s.substring(s.length-1,s.length) == '\n') || (s.substring(s.length-1,s.length) == '\r'))
{ s = s.substring(0,s.length-1); }
return escape(s); }
launchmenu2(); 