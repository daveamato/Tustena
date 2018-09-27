<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="office" TagName="Offices" Src="~/admin/offices.ascx" %>
<%@ Register TagPrefix="groups" TagName="Groups" Src="~/admin/groups.ascx" %>
<%@ Page Language="c#"  AutoEventWireup="false" Trace="false" codebehind="Company.aspx.cs" Inherits="Digita.Tustena.AdminCompany"%>
<html>
<head id="head" runat="server">
<link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">
	<script type="text/javascript" src="/js/dynabox.js"></script>
	<script>
var win,companyCode;
function makeform()
{
	companyCode=(document.getElementById('WebService_guid').innerHTML+document.getElementById('WebService_pin').value);
	win=window.open("/template/webGateForm.htm",'dynabox','width=500,height=500,left=100,top=100');
	fillcode(companyCode);
	alwaysOnTop();
}

function fillcode()
{
	var obj = win.document.getElementById('templatesource');
	if(obj)
		obj.value=obj.value.replace("%COMPANYCODE%",companyCode);
	else
		setTimeout("fillcode()",500);
}

function alwaysOnTop() {
	if(win && win.open && !win.closed)win.focus();
	setTimeout("alwaysOnTop()",1500);
}

	</script>

</head>
<body id="body" runat="server">
<form runat="server">

		<table width="100%" border="0" cellspacing="0">
			<tr>
				<td width="140" class="SideBorderLinked" valign="top" runat="server" visible="false">
					<table width="98%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td class="sideContainer">
								<div class="sideTitle"><twc:LocalizedLiteral Text="AComtxt1" runat="server" id="LocalizedLiteral1" /></div>
							</td>
						</tr>
					</table>
				</td>
				<td valign="top" height="100%" class="pageStyle">
					<table width="100%" border="0" cellspacing="0" cellpadding="0">
						<tr>
							<td align="left" class="pageTitle" valign="top">
								<twc:LocalizedLiteral Text="AComtxt0" runat="server" id="LocalizedLiteral2" />
							</td>
						</tr>
					</table>
					<br>
					<![if !IE]>
					<link rel="stylesheet" type="text/css" href="/css/tabControl_all.css">
	<![endif]><!--[if IE]><LINK media=screen href="/css/tabControl.css"
      type=text/css rel=stylesheet><![endif]-->

	<twc:tustenatabber id="Tabber" width="840" runat="server">
							<twc:TustenaTab id="TabCompany" runat="server" clientside="true" langheader="AComtxt1">
								<TABLE class="normal" id="ContactForm" cellSpacing="0" cellPadding="3" width="98%" align="center"
									border="0" runat="server">
									<TR>
										<TD vAlign="top" width="50%">
											<TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="center" border="0">
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral6" runat="server" Text="AComtxt2"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:Label class="normal" id="CompanyName" runat="server"></asp:Label></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral7" runat="server" Text="AComtxt3"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="PhoneNumber" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral8" runat="server" Text="AComtxt4"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="Fax" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral9" runat="server" Text="AComtxt5"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="Email" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral10" runat="server" Text="AComtxt6"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="WebSite" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral11" runat="server" Text="AComtxt27"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:Label id="PermittedKb" runat="server" cssClass="normal"></asp:Label></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral12" runat="server" Text="AComtxt28"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:Label id="BusyKb" runat="server" cssClass="normal"></asp:Label></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="Localizedliteral31" runat="server" Text="AComtxt29"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:Label id="FileIndexes" runat="server" cssClass="normal"></asp:Label></TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top" width="50%">
											<TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="center" border="0">
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral13" runat="server" Text="AComtxt7"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="AddressInvoice" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral14" runat="server" Text="AComtxt8"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="CityInvoice" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral15" runat="server" Text="AComtxt9"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="ProvinceInvoice" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral16" runat="server" Text="AComtxt10"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="RegionInvoice" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral17" runat="server" Text="AComtxt11"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="CountryInvoice" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral18" runat="server" Text="AComtxt12"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="ZipInvoice" runat="server"></asp:TextBox></TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD class="BorderBottomTitles" align="left" colSpan="2"><B>
												<twc:LocalizedLiteral id="LocalizedLiteral19" runat="server" Text="AComtxt20"></twc:LocalizedLiteral></B></TD>
									</TR>
									<TR>
										<TD vAlign="top" width="50%">
											<TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="center" border="0">
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral20" runat="server" Text="AComtxt24"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox id="CompanyTextboxID" style="DISPLAY: none" runat="server"></asp:TextBox>
																	<asp:TextBox id="CompanyTextbox" runat="server" ReadOnly="true" CssClass="BoxDesign" Width="100%"></asp:TextBox></TD>
																<TD width="30">&nbsp;<IMG style="CURSOR: pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&amp;textbox=CompanyTextbox&amp;textbox2=CompanyTextboxID',event,500,400)"
																		src="/i/user.gif" border="0">
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR id="LeadModule" runat=server>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral21" runat="server" Text="AComtxt21"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="LeadDays" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral22" runat="server" Text="AComtxt25"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="Voip" runat="server"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral23" runat="server" Text="AComtxt26"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:TextBox class="BoxDesign" id="InterPrefix" runat="server"></asp:TextBox></TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top" width="50%">
										    <TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="left" border="0">
												<TR>
													<TD width="40%">
													</TD>
													<td>
													    <asp:RadioButtonList ID="checkLogo" runat=server RepeatColumns=3 RepeatDirection=Horizontal RepeatLayout=Table>
													    </asp:RadioButtonList>
													</td>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD class="BorderBottomTitles" align="left" colSpan="2"><B>
												<twc:LocalizedLiteral id="LocalizedLiteral24" runat="server" Text="AComtxt15"></twc:LocalizedLiteral></B></TD>
									</TR>
									<TR>
										<TD vAlign="top" width="50%">
											<TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="left" border="0">
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral25" runat="server" Text="AComtxt22"></twc:LocalizedLiteral></TD>
													<TD>
														<asp:Label id="WebService_guid" runat="server"></asp:Label>
														<asp:TextBox class="BoxDesign" onkeypress="NumbersOnly(event,'')" id="WebService_pin" runat="server"
															width="50px" maxlength="5"></asp:TextBox></TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral26" runat="server" Text="AComtxt23"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox id="WebService_OwnerID" style="DISPLAY: none" runat="server" Width="100%"></asp:TextBox>
																	<asp:TextBox id="WebService_Owner" runat="server" ReadOnly="true" CssClass="BoxDesign" Width="100%"></asp:TextBox></TD>
																<TD width="30">&nbsp;<IMG style="CURSOR: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&amp;textbox=WebService_Owner&amp;textbox2=WebService_OwnerID',event)"
																		src="/i/user.gif" border="0">
																</TD>
															</TR>
															<TR>
																<TD><A class="normal" href="javascript:makeform();">generate HTML form</A>
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top" width="50%"></TD>
									</TR>
									<TR style="DISPLAY: none">
										<TD vAlign="top" width="50%">
											<TABLE class="normal" cellSpacing="2" cellPadding="0" width="100%" align="center" border="0">
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral27" runat="server" Text="AComtxt16"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox id="WebGate_OwnerID" style="DISPLAY: none" runat="server"></asp:TextBox>
																	<asp:TextBox class="BoxDesign" id="WebGate_Owner" runat="server"></asp:TextBox></TD>
																<TD noWrap align="center" width="50"><IMG style="CURSOR: pointer" onclick="CreateBox('/Common/PopAccount.aspx?render=no&amp;textbox=WebGate_Owner&amp;textbox2=WebGate_OwnerID',event)"
																		src="/i/user.gif" border="0">
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral28" runat="server" Text="AComtxt17"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox id="WebGate_GroupID" style="DISPLAY: none" runat="server"></asp:TextBox>
																	<asp:TextBox class="BoxDesign" id="WebGate_Group" runat="server"></asp:TextBox></TD>
																<TD noWrap align="center" width="50"><IMG style="CURSOR: pointer" onclick="CreateBox('/Common/PopGroups.aspx?render=no&amp;textbox=WebGate_Group&amp;textbox2=WebGate_GroupID',event)"
																		src="/i/user.gif" border="0">
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral29" runat="server" Text="AComtxt18"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox id="WebGate_NotifyID" style="DISPLAY: none" runat="server"></asp:TextBox>
																	<asp:TextBox class="BoxDesign" id="WebGate_Notify" runat="server"></asp:TextBox></TD>
																<TD noWrap align="center" width="50"><IMG style="CURSOR: pointer" onclick="CreateBox('/Common/PopAccount.aspx?render=no&amp;textbox=WebGate_Notify&amp;textbox2=WebGate_NotifyID',event)"
																		src="/i/user.gif" border="0">
																</TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
												<TR>
													<TD width="40%">
														<twc:LocalizedLiteral id="LocalizedLiteral30" runat="server" Text="AComtxt19"></twc:LocalizedLiteral></TD>
													<TD>
														<TABLE cellSpacing="0" cellPadding="0" width="100%">
															<TR>
																<TD>
																	<asp:TextBox class="BoxDesign" id="WebGate_WebSite" runat="server"></asp:TextBox></TD>
															</TR>
														</TABLE>
													</TD>
												</TR>
											</TABLE>
										</TD>
										<TD vAlign="top" width="50%"></TD>
									</TR>
									<TR>
										<TD align="left">
											<asp:Label class="divautoformRequired" id="Info" runat="server"></asp:Label>&nbsp;
										</TD>
										<TD align="right">
											<asp:LinkButton id="Submit" runat="server" Cssclass="save"></asp:LinkButton></TD>
									</TR>
								</TABLE>
							</twc:TustenaTab>
							<twc:TustenaTab id="TabGroup" runat="server" clientside="true" langheader="Menutxt16">
								<groups:Groups id="groups" runat="server"></groups:Groups>
							</twc:TustenaTab>
							<twc:TustenaTab id="TabOffice" runat="server" clientside="true" langheader="Menutxt15">
								<office:Offices id="Offices" runat="server"></office:Offices>
							</twc:TustenaTab>
						</twc:tustenatabber>
				</td>
			</tr>
		</table>
	</form>

</body>
</html>
