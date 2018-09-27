/**********************************************************************\* #################################################################### *
* #                           TUSTENA CRM                            # *
* # ---------------------------------------------------------------- # *
* #     Copyright 20032005 Digita S.r.l. All Rights Reserved.      # *
* # This file may not be redistributed in whole or significant part. # *
* # ---------------------------------------------------------------- # *
* #                      http:
* #################################################################### *
\**********************************************************************/

var detect = navigator.userAgent.toLowerCase(); var OS,browser,version,res,thestring; var note = ""

if (checkIt('konqueror'))
{ browser = "Konqueror"; OS = "Linux"; }
else if (checkIt('safari')) browser = "Safari"; else if (checkIt('omniweb')) browser = "OmniWeb"; else if (checkIt('opera')) browser = "Opera"; else if (checkIt('webtv')) browser = "WebTV"; else if (checkIt('icab')) browser = "iCab"; else if (checkIt('msie')) browser = "Internet Explorer"; else if (!checkIt('compatible'))
{ browser = "Netscape Navigator"
version = detect.charAt(8); }
else browser = "Browser sconosciuto"; 
if (!version) version = detect.charAt(place + thestring.length); 
if (!OS)
{ if (checkIt('linux')) OS = "Linux"; else if (checkIt('x11')) OS = "Unix"; else if (checkIt('mac')) OS = "Mac"
else if (checkIt('win')) OS = "Windows"
else OS = "Sistema operativo sconosciuto"; }
document.write('<table class=normal width=80%><tr><td width=50% align=right>Browser:</td><td width=50% align=left>' + browser + ' ' + version + '</td></tr>'); document.write('<tr><td align=right>Sistema:</td><td align=left>' + OS + '</td></tr>'); 

if (navigator.javaEnabled()) var JavaStatus="Ok"; else
var JavaStatus="No"; document.write('<tr><td align=right>Java:</td><td align=left>' + JavaStatus + '</td></tr>'); document.write('<tr><td align=right>Lingua:</td><td align=left><img src=flags/' + "it" + '.gif></td></tr>'); 
document.write('<tr><td align=right>Risoluzione:</td><td align=left>' + window.screen.width+" X "+window.screen.height + '</td></tr>'); 

if (window.screen.width<1024)
note += "E' consigliata una risoluzione di 1024x768 o superore<br>"; if (browser=="Opera"&&version==6)
note += "per un corretto funzionamento  consigliato l'aggiornamento a <a href='http://www.opera.com'>Opera 7</a><br>"; if (browser=="Internet Explorer"&&version==5&&OS=="Mac")
note += "Il browser in uso non  supportato, la compatibilit  in fase di studio<br>"; if (browser=="Safari"||browser=="OmniWeb"||browser=="iCab")
note += "Non  garantita la piena conpatibilit con il browser in uso<br>scaricare <a href='http://www.netscape.com/ie'>Netscape 7+</a><br>"; if (note == "")
note += "Configurazione client approvata"


document.write('<tr><td colspan=2 style="color:red" align=center>' + note + '</td></tr></table>'); 
function checkIt(string)
{ place = detect.indexOf(string) + 1; thestring = string; return place; }
