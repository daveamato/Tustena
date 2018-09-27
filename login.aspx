<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>

<%@ Page Language="C#" Trace="false" Codebehind="login.cs" Inherits="Digita.Tustena.login"
    EnableViewState="false" AutoEventWireup="True" %>

<html>
<head runat="server">
    <link rel="stylesheet" type="text/css" href="/css/G.css">
    <style>
				.bl { MARGIN-TOP: 160px; BACKGROUND: url(/images/cbl.gif) #fff no-repeat 0px 100%; WIDTH: 260px }
				.br { BACKGROUND: url(/images/cbr.gif) no-repeat 100% 100% }
				.tl { BACKGROUND: url(/images/ctl.gif) no-repeat 0px 0px }
				.tr { PADDING-RIGHT: 10px; PADDING-LEFT: 10px; BACKGROUND: url(/images/ctr.gif) no-repeat 100% 0px; PADDING-BOTTOM: 10px; PADDING-TOP: 10px }
				.clear { FONT-SIZE: 1px; HEIGHT: 1px }
				</style>
</head>
<body>
    <center>
        <form runat="server">
            <div class="bl">
                <div class="br">
                    <div class="tl">
                        <div class="tr">
                            <table cellspacing="1" cellpadding="5" width="250" align="center">
                                <tbody>
                                    <tr>
                                        <td class="normal" colspan="2" align="center" nowrap>
                                            <b>LOGIN</b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%" align="right" nowrap>
                                            <domval:RegexDomValidator ID="LoginValidator" runat="server" ErrorMessage="*" ControlToValidate="TxtUsr"></domval:RegexDomValidator>
                                            <span class="normal">
                                                <%=wrm.GetString("Lgntxt1")%>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="BoxDesign" ID="TxtUsr" runat="server" Width="120" jumpret="txtPwd"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="50%" align="right">
                                            <span class="normal">
                                                <%=wrm.GetString("Lgntxt2")%>
                                            </span>
                                        </td>
                                        <td>
                                            <asp:TextBox CssClass="BoxDesign" ID="TxtPwd" runat="server" Width="120" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Button CssClass="ListResultTitle" ID="Submit" runat="server"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:Label ID="TxtMessage" runat="server" ForeColor="Red" Font-Names="Verdana" Font-Size="8pt"></asp:Label>
                                            <domval:RegexDomValidator ID="Validator1" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                Display="Dynamic" ValidationExpression="[0-9a-zA-Z]{4,}" ControlToValidate="TxtPwd"></domval:RegexDomValidator>
                                            <domval:RequiredDomValidator ID="Validator2" runat="server" Font-Names="Verdana"
                                                Font-Size="8pt" Display="Dynamic" ControlToValidate="TxtUsr"></domval:RequiredDomValidator>
                                            <domval:RequiredDomValidator ID="Validator3" runat="server" Font-Names="Verdana"
                                                Font-Size="8pt" Display="Dynamic" ControlToValidate="TxtPwd"></domval:RequiredDomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="30">
                                            &nbsp;</td>
                                        <tr>
                                        </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <table cellspacing="0" cellpadding="0" width="250" align="center">
                <tr>
                    <td align="right">
                        <span id="status"></span>
                    </td>
                </tr>
            </table>
            <div class="clear">
            </div>
            <br>
            <img src="/i/arrow.gif"><a href="/PassRecoveryPage.aspx" class="normal"><%=wrm.GetString("PassRecovery3")%></a><img
                src="/i/arrow2.gif">
            <asp:CheckBox Font-Names="Verdana" Font-Size="8pt" ID="CkbPL" runat="server" Visible="false">
            </asp:CheckBox>
            <input type="hidden" name="timezone" id="timezone">
        </form>

        <script type="text/javascript">
        if(document.getElementById){
       var stat = document.getElementById('status');
	var detect = navigator.userAgent.toLowerCase();
	var OS,browser,version,res,thestring;
  	var note = "";

	if (checkIt('konqueror'))
	{
		browser = "Konqueror";
		 browserimage = "konqueror.gif";
		OS = "Linux";
	}
	else if (checkIt('safari')){ browser = "Safari"; browserimage = "safari.gif";}
	else if (checkIt('omniweb')){ browser = "OmniWeb"; browserimage = "omniweb.gif";}
	else if (checkIt('opera')){ browser = "Opera"; browserimage = "opera.gif";}
	else if (checkIt('webtv')){ browser = "WebTV"; browserimage = "webtv.gif";}
	else if (checkIt('icab')){ browser = "iCab"; browserimage = "icab.gif";}
	else if (checkIt('msie')){ browser = "Internet Explorer"; browserimage = "ie.gif";}
	else if (checkIt('firefox')){ browser = "Firefox"; browserimage = "ff.gif";}
	else if (!checkIt('compatible'))
	{
		browser = "Netscape Navigator";
		browserimage = "netscape.gif";
		version = detect.charAt(8);
	}
	else browser = "<%=wrm.GetString("Lgntxt7")%>";

	img = "<img src=\"/i/browser/"+browserimage+"\">&nbsp;";

	if (!version) version = detect.charAt(place + thestring.length);

	if (!OS)
	{
		if (checkIt('linux')){ OS = "Linux"; systemimage = "linux.gif" }
		else if (checkIt('x11')){ OS = "Unix"; systemimage = "unix.gif" }
		else if (checkIt('mac')){ OS = "Mac"; systemimage = "mac.gif" }
		else if (checkIt('win')){ OS = "Windows"; systemimage = "windows.gif" }
		else OS = "<%=wrm.GetString("Lgntxt8")%>";
	}

	img += "<img src=\"/i/browser/"+systemimage+"\">&nbsp;";


	document.write('<table class=normal width=80%><tr><td width=50% align=right><%=wrm.GetString("Lgntxt9")%></td><td width=50% align=left>' + browser + ' ' + version + '</td></tr>');
	document.write('<tr><td align=right><%=wrm.GetString("Lgntxt10")%></td><td align=left>' + OS + '</td></tr>');

  document.write('<tr><td align=right><%=wrm.GetString("Lgntxt13")%></td><td align=left><img src=flags/<%=System.Globalization.CultureInfo.CurrentCulture.Name.Substring(System.Globalization.CultureInfo.CurrentCulture.Name.Length-2)%>.gif></td></tr>');
  var d=new Date;
  var tz=-d.getTimezoneOffset();
  document.getElementById('timezone').value=tz;
  tz = tz/60;
  document.write('<tr><td align=right>TimeZone:</td><td align=left>' + tz + '</td></tr>');

  document.write('<tr><td align=right><%=wrm.GetString("Lgntxt14")%></td><td align=left>' + window.screen.width+" X "+window.screen.height + '</td></tr>');

  img += "<img src=\"/i/browser/"+window.screen.width+".gif\">&nbsp;";

  img += "<img src=flags/<%=System.Globalization.CultureInfo.CurrentCulture.Name.Substring(System.Globalization.CultureInfo.CurrentCulture.Name.Length-2)%>.gif>&nbsp;";


  if (window.screen.width<1024)
  	note += "<%=wrm.GetString("Lgntxt15")%><br>";
  if (browser=="Opera"&&version==6)
  	note += "<%=wrm.GetString("Lgntxt16")%><a href='http://www.opera.com'>Opera 7</a><br>";
  if (browser=="Internet Explorer"&&version==5&&OS=="Mac")
  	note += "<%=wrm.GetString("Lgntxt17")%><br>";
  if (browser=="Safari"||browser=="OmniWeb"||browser=="iCab")
  	note += "<%=wrm.GetString("Lgntxt18")%><a href='http://www.netscape.com/ie'>Netscape 7+</a><br>";
  if (note == "")
  	note += "<%=wrm.GetString("Lgntxt19")%>";

	stat.innerHTML = img;

  document.write('<tr><td colspan=2 style="color:red" align=center>' + note + '</td></tr></table>');
  }

  function checkIt(string)
  {
	place = detect.indexOf(string) + 1;
	thestring = string;
	return place;
  }
        </script>

        <span class=normal>
        <a href="http://www.cacert.org/certs/root.crt">CACert Base SLL Certificate</a>&nbsp;-&nbsp;<a href="https://crm.tustena.com">SSL Login</a>
        </span>
    </center>
</body>
</html>
