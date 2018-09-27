<%@ Page language="c#" Codebehind="PassRecoveryPage.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.PassRecoveryPage" %>
<%@ Register TagPrefix="PassRec" TagName="PasswordRecovery" Src="~/admin/PasswordRecovery.ascx" %>
<html>
<head id="head" runat="server">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >


</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
        <center>
			<style>
				.bl {background: url(/images/cbl.gif) 0 100% no-repeat #FFF; width: 260px; margin-top:160px;}
				.br {background: url(/images/cbr.gif) 100% 100% no-repeat}
				.tl {background: url(/images/ctl.gif) 0 0 no-repeat}
				.tr {background: url(/images/ctr.gif) 100% 0 no-repeat; padding:10px}
				.clear {font-size: 1px; height: 1px}
			</style>
			<div class="bl"><div class="br"><div class="tl"><div class="tr">
            <br>
			<table cellspacing="1" cellpadding="5" width="250" align="center">
				<tr>
					<td>
						<PassRec:PasswordRecovery id="Prec" runat=server/>
					</td>
				</tr>
            </table>
            <br>
            </div></div></div></div><div class="clear">&nbsp;</div>
                     <input type="hidden" name="timezone" id="timezone">
        </center>
        </form>

</body>
</html>
