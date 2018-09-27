<%@ Page language="c#" Codebehind="ErrorPage.aspx.cs" AutoEventWireup="True" Inherits="Digita.Tustena.Error.ErrorPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>ErrorPage</title>
		<script>
  function redirect(){
	location.href="/today.aspx";
  }
		</script>
	</HEAD>
	<body onload="setTimeout('redirect()',10000);">
		<TABLE height="1113" cellSpacing="0" cellPadding="0" width="267" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="267" height="1113">
					<form id="Form2" method="post" runat="server">
						<TABLE height="250" cellSpacing="0" cellPadding="0" width="1101" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="10" height="15"></TD>
								<TD width="1091"></TD>
							</TR>
							<TR vAlign="top">
								<TD height="199"></TD>
								<TD>
									<div align="center">
										<p>&nbsp;</p>
										<p>&nbsp;</p>
										<p><img src="/i/tustena_big.gif" alt="Tustena - Online CRM solution.">
										</p>
										<p>
											<font size="+1" face="Verdana, Arial, Helvetica, sans-serif"><strong>
													<asp:Literal ID="LtrError" Runat="server" />
												</strong></font>
										</p>
									</div>
								</TD>
							</TR>
							<TR vAlign="top">
								<TD height="36"></TD>
								<TD>
									<FORM id="Form1" method="post" runat="server">
										<TABLE height="250" cellSpacing="0" cellPadding="0" width="1101" border="0" ms_2d_layout="TRUE">
											<center><a href="http://www.digita.it"><img src="g.gif" border="0" alt="Web Design & Internet Software Engineering">&nbsp;Powered
													by Digita</a>&nbsp;<a href="http://www.xtarget.it"><img src="x.gif" border="0" alt="Posizionamento garantito sui motori di risearch">&nbsp;Positioning
													by XTarget</a>
										</TABLE>
									</FORM>
									</CENTER></TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
