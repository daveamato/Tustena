<%@ Page language="c#" Codebehind="PrintOpportunity.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Common.PrintOpportunity" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>:: Tustena ::</title>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
	</HEAD>
	<body>
		<TABLE height="230" cellSpacing="0" cellPadding="0" width="143" border="0" ms_2d_layout="TRUE">
			<TR vAlign="top">
				<TD width="143" height="230">
					<form id="Form1" method="post" runat="server">
						<TABLE height="126" cellSpacing="0" cellPadding="0" width="218" border="0" ms_2d_layout="TRUE">
							<TR vAlign="top">
								<TD width="10" height="15"></TD>
								<TD width="111"></TD>
								<TD width="97"></TD>
							</TR>
							<TR vAlign="top">
								<TD height="12"></TD>
								<TD>
									<asp:Literal id="SomeJS" runat="server" /></TD>
								<TD>
									<asp:Literal id="IDOp" runat="server" visible="false" /></TD>
							</TR>
							<TR vAlign="top">
								<TD height="99"></TD>
								<TD colSpan="2">
									<table class="normal" height="98" width="30">
										<tr>
											<td nowrap>
												<input type="checkbox" name="print1" checked value="1" disabled><%=wrm.GetString("Prnopptxt1")%></input>
												<br>
												<input type="checkbox" name="print2" value="2"><%=wrm.GetString("Prnopptxt2")%></input>
												<br>
												<input type="checkbox" name="print3" value="3"><%=wrm.GetString("Prnopptxt3")%></input>
												<br>
											</td>
										</tr>
										<tr>
											<td valign="bottom">
												<asp:LinkButton id="SubmitPrint" runat="server" Text="OK" CssClass="save" />
											</td>
										</tr>
									</table>
								</TD>
							</TR>
						</TABLE>
					</form>
				</TD>
			</TR>
		</TABLE>
	</body>
</HTML>
