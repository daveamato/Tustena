<%@ Register TagPrefix="mail" TagName="mailout" Src="~/MailingList/WebMail/mailout.ascx" %>
<%@ Page language="c#" Codebehind="SendGenericMail.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.SendGenericMail" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>SendGenericMail</title>
	</HEAD>
	<body>
		<TABLE height="1111" cellSpacing="0" cellPadding="0" width="108" border="0" ms_2d_layout="TRUE">
			<TR>
				<TD width="0" height="0"></TD>
				<TD width="10" height="0"></TD>
				<TD width="98" height="0"></TD>
			</TR>
			<TR vAlign="top">
				<TD width="0" height="15"></TD>
				<TD colSpan="2" rowSpan="2">
					<form id="Form1" method="post" runat="server">
						<TABLE height="91" cellSpacing="0" cellPadding="0" width="1099" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="10" height="15"></TD>
								<TD width="1089"></TD>
							</TR>
							<TR vAlign="top">
								<TD height="76"></TD>
								<TD>
									<table width="1088" border="0" cellspacing="0" height="75">
										<tr>
											<td width="140" class="SideBorderLinked" valign="top">
												<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
													<tr>
														<td align="left" class="BorderBottomTitles" valign="top">
															<span class="divautoform">
																<b>Web Mail</b></span>
														</td>
													</tr>
													<tr>
														<td>
															<asp:LinkButton Id="MailIn" runat="server" class="normal" />
														</td>
													</tr>
												</table>
											</td>
											<td valign="top" height="100%">
												<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
													<tr>
														<td align="left" class="BorderBottomTitles" valign="top">
															<span class="divautoform">
																<b>Web Mail</b></span>
														</td>
													</tr>
													<tr>
														<td align="left" valign="top">
															<mail:mailout id="mo" runat="server" />
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
				<TD></TD>
			</TR>
			<TR vAlign="top">
				<TD width="0" height="1096"></TD>
				<TD></TD>
				<TD>
					<script language="javascript" src="/js/dynabox.js"></script>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
