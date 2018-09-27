<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page language="c#" Codebehind="MailEvents.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.MailingList.MailEvents" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>MailEvents</title>
	</HEAD>
	<body>
		<script type="text/javascript" src="/js/autodate.js"></script>
		<script type="text/javascript" src="/js/dynabox.js"></script>
		<script type="text/javascript" src="/js/calendars.js"></script>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0">
				<TBODY>
					<tr>
						<td width="140" class="SideBorderLinked" valign="top">
							<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
								<tr>
			                   		<td class="sideContainer">
										<div class="sideTitle">Mail events</div>
								        <asp:linkbutton id="Reportbtn" runat="server" cssclass="sidebtn" />
								        <asp:linkbutton id="BirthDatebtn" runat="server" cssclass="sidebtn" />
									</td>
								</tr>
							</table>
						</td>
						<td valign="top" height="100%">
							<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
								<tr>
									<td align="left" class="BorderBottomTitles" valign="top">
										<span class="divautoform"><b>Mail events</b></span>
									</td>
								</tr>
							</table>
							<asp:Repeater id="ReportRepeater" runat="server">
								<HeaderTemplate>
									<table class="tblstruct normal">
										<tr>
											<td class="GridTitle" width="99%"><%=wrm.GetString("Mlevtxt1")%></td>
										</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td class="GridItem">
											<%# DataBinder.Eval(Container.DataItem, "description")%>
										</td>
									</tr>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<tr>
										<td class="GridItemAltern">
											<%# DataBinder.Eval(Container.DataItem, "description")%>
										</td>
									</tr>
								</AlternatingItemTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:Repeater>
			<table id="ReportMail" runat="server" width="98%" border="0" cellspacing="0" cellpadding="0"
				align="center">
				<tr>
					<td>
						<table width="100%" class="normal">
							<tr>
								<td width="50%">
									<div><%=wrm.GetString("Mlevtxt3")%></div>
									<asp:TextBox id="SelReport" runat="server" class="BoxDesign" />
									<asp:TextBox id="SelReportID" runat="server" style="DISPLAY:none" />
								</td>
								<td width="50%">
									&nbsp;<img src="/i/Pin.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpReport.aspx?render=no&textbox=SelReport&textboxID=SelReportID',event,400,300)">
								</td>
							<tr>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table width="98%" border="0" cellspacing="0" cellpadding="0" align="center">
				<tr id="CorpoRecurrence" runat="server">
					<td valign="top">
						<span id="TableRicorrente">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="40%">
										<%=wrm.GetString("Evnttxt20")%>
										<asp:CompareValidator id="CvRecDatainizio" runat="Server" Operator="DataTypeCheck" Type="Date" ErrorMessage="Data non valida"
											ControlToValidate="RecDateStart" />
									</td>
									<td width="60%">
										<table width="100%" cellspacing="0" cellpadding="0">
											<tr>
												<td>
													<asp:TextBox id="RecDateStart" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesign"
														EnableViewState="true" maxlength="10" />
												</td>
												<td width="30">
													&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecDatainizio',event,195,195)">
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td width="40%">
										<%=wrm.GetString("Evnttxt21")%>
										<domval:CompareDomValidator id="cvRecDataFine" runat="Server" Operator="DataTypeCheck" Display="Dynamic" Type="Date"
											ErrorMessage="*" ControlToValidate="RecEndDate" />
									</td>
									<td width="60%">
										<table width="100%" cellspacing="0" cellpadding="0">
											<tr>
												<td>
													<asp:TextBox id="RecEndDate" onkeypress="DataCheck(this,event)" runat="server" class="BoxDesign"
														EnableViewState="true" maxlength="10" />
												</td>
												<td width="30">
													&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=RecDataFine',event,195,195)">
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td width="40%">
										<%=wrm.GetString("Evnttxt22")%>
									</td>
									<td width="60%">
										<asp:Dropdownlist id="RecMode" old="true" runat="server" cssClass="BoxDesign" onChange="ActivateForms();" />
									</td>
								</tr>
							</table>
						</span>
					</td>
					<td valign="top" colspan="2" id="RecTitle">
						<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
							<tr>
								<td align="left" class="BorderBottomTitles">
									<b>
										<%=wrm.GetString("Evnttxt29")%>
									</b>
								</td>
							</tr>
						</table>
						<span id="SpanRecDaily" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="40%">
										<%=wrm.GetString("Evnttxt30")%>
									</td>
									<td width="20%">
										<asp:TextBox id="RecDayDays" runat="server" class="BoxDesign" EnableViewState="true" Text="1" />
									</td>
									<td width="40%">
										<%=wrm.GetString("Evnttxt31")%>
									</td>
								</tr>
								<tr>
									<td width="60%" COLSPAN="2">
										<%=wrm.GetString("Evnttxt32")%>
									</td>
									<td width="40%">
										<asp:CheckBox id="RecWorkingDay" runat="server" EnableViewState="true" />
									</td>
								</tr>
							</table>
						</span><span id="SpanRecWeekly" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="40%">
										<%=wrm.GetString("Evnttxt33")%>
									</td>
									<td width="20%">
										<asp:TextBox id="RecSettSS" runat="server" class="BoxDesign" EnableViewState="true" Text="1" />
									</td>
									<td width="40%">
										<%=wrm.GetString("Evnttxt34")%>
									</td>
								</tr>
								<tr>
									<td width="40%" valign="top">
										<%=wrm.GetString("Evnttxt35")%>
									</td>
									<td width="60%" COLSPAN="2">
										<asp:CheckBoxList id="RecSetDays" runat="server" EnableViewState="true" class="BoxDesign" />
									</td>
								</tr>
							</table>
						</span><span id="SpanRecMonthly" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt43")%>
									</td>
									<td width="40%">
										<asp:TextBox id="RecMonthlyDays" runat="server" class="BoxDesign" EnableViewState="true"
											Text="1" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt44")%>
									</td>
									<td width="40%">
										<asp:TextBox id="RecMonthlyMonths" runat="server" class="BoxDesign" EnableViewState="true"
											Text="1" />
									</td>
								</tr>
							</table>
						</span><span id="SpanRecYearlyDay" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt45")%>
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecMonthlyDayPU" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										&nbsp;
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecMonthlyDayDays" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt44")%>
									</td>
									<td width="40%">
										<asp:TextBox id="RecMonthlyDayMonths" runat="server" class="BoxDesign" EnableViewState="true"
											Text="1" />
									</td>
								</tr>
							</table>
						</span><span id="SpanRecYearly" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt43")%>
									</td>
									<td width="40%">
										<asp:TextBox id="RecYearDays" runat="server" class="BoxDesign" EnableViewState="true"
											Text="1" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt44")%>
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecYearMonths" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
							</table>
						</span><span id="SpanRecYearlyDay" style="DISPLAY: none">
							<table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt33")%>
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecYearDayPU" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										&nbsp;
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecYearDayDays" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="60%">
										<%=wrm.GetString("Evnttxt44")%>
									</td>
									<td width="40%">
										<asp:Dropdownlist id="RecYearDayMonths" runat="server" old="true" cssClass="BoxDesign" />
									</td>
								</tr>
							</table>
						</span>
					</td>
				</tr>
				<tr>
					<td colspan="3" align="right">
						<asp:LinkButton ID="Submit" runat="server" class="AlfabetoNormal" EnableViewState="true" />
					</td>
				</tr>
			</table>
			</TD></TR></TBODY></TABLE>
		</form>
	</body>
</HTML>
