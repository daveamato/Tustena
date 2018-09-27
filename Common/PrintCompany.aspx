<%@ Page language="c#" Codebehind="PrintCompany.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PrintCompany" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>:: Tustena ::</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
	</HEAD>
	<body>
					<form id="Form1" method="post" runat="server">
						<TABLE cellSpacing="0" cellPadding="0" border="0">

							<TR vAlign="top">

								<TD>
									<asp:Literal id="SomeJS" runat="server" /></TD>
								<TD>
									<asp:Literal id="IDCompany" runat="server" visible="false" /></TD>
							</TR>
							<TR vAlign="top">

								<TD colSpan="2">
									<table class="normal" height="66" width="49">
										<tr>
											<td nowrap>
												<input type="checkbox" name="print1" checked value="1" disabled><span style="text-transform:uppercase"><%=wrm.GetString("Prnopptxt1")%></span>
												<br>
												<input type="checkbox" name="print2" checked value="2"><span style="text-transform:uppercase"><%=wrm.GetString("Bcotxt30")%></span>
												<br>
												<input type="checkbox" name="print3" checked value="3"><span style="text-transform:uppercase"><%=wrm.GetString("Bcotxt45")%></span>
											</td>
										</tr>
										<tr>
											<td valign="bottom" align=right>
												<asp:LinkButton id="SubmitPrint" runat="server" Text="OK" CssClass="Save"/>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
	</body>
</HTML>
