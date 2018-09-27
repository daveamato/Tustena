<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page Language="c#" codebehind="CRM_Reminder.aspx.cs" Inherits="Digita.Tustena.CRMReminder" EnableViewState="true" trace="false"  AutoEventWireup="false"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<html>
<head id="head" runat="server">
<script type="text/javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" cellspacing="0">
		<TBODY>
			<tr>
				<td width="140" class="SideBorderLinked" valign="top">
					<table width="98%" border="0" cellspacing="0" align="center" cellpadding="0">
						<tr>
						<td class="sideContainer">
						<div class="sideTitle"><twc:localizedLiteral text="CRMdeftxt1" runat="server" /></div>
						<asp:LinkButton Id="BtnNew" runat="server" class="sidebtn"  visible="false" />
						<div class="sideTitle"><%=wrm.GetString("Acttxt30")%></div>
						<div class="sideInputTitle"><%=wrm.GetString("CRMRemtxt11")%>
						<div class="sideInput"><asp:TextBox Id="Search" runat="server" Cssclass="BoxDesign" />
						<div class="sideSubmit"><asp:LinkButton Id="Btnsearch" runat="server" Cssclass="save"  /></div>
									</td>
								</tr>
								<tr><td>&nbsp;</td></tr>
								<tr>
								<td class="sideContainer">
								<div class="sideTitle"><%=wrm.GetString("Calendar")%></div>
								<asp:Calendar width="135" height="135" ID="calDate" Runat="server" Font-Name="verdana" Font-Size="10px"
									OnSelectionChanged="Change_Date" OnVisibleMonthChanged="Change_Month" OnDayRender="DayRender"
									DayNameFormat="FirstLetter" TodayDayStyle-BackColor="gold" DayHeaderStyle-BackColor="lightsteelblue"
									OtherMonthDayStyle-ForeColor="lightgray" NextPrevStyle-ForeColor="white" TitleStyle-BackColor="gray"
									TitleStyle-ForeColor="white" TitleStyle-Font-Bold="True" TitleStyle-Font-Size="10px" SelectedDayStyle-BackColor="Navy"
									SelectedDayStyle-Font-Bold="True" />
							</td>
						</tr>
					</table>
				</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
										<%=wrm.GetString("CRMRemtxt1")%>
										&nbsp;&nbsp;
										<asp:Label id="SelDate" runat="server" class="normal" />
							</td>
						</tr>
						<tr>
							<td align="left" class="normal">
								<%=wrm.GetString("CRMRemtxt3")%>
								&nbsp;
							</td>
						</tr>
					</table>
					<span id="HomePage" runat="server">
	<br>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
												<%=wrm.GetString("CRMdeftxt6")%>
												</td>
								<tr>
									<td align="left">
										<center><asp:Label id="RepeaterActivityInfo" runat="server" visible="false" Class="divautoformRequired" /></center>
										<asp:Repeater id="RepeaterActivity" runat="server" OnItemDataBound="RepeaterActivityDatabound"
											OnItemCommand="RepeaterActivityCommand">
											<HeaderTemplate>
												<table id="remactivity" border="0" cellpadding="3" cellspacing="0" width="100%" class="normal"
													align="left">
													<tr>
														<td width="5%" class="GridTitle"><%=wrm.GetString("CRMRemtxt7")%></td>
														<td class="GridTitle"><%=wrm.GetString("Acttxt29")%></td>
														<td class="GridTitle"><%=wrm.GetString("Acttxt6")%></td>
														<td class="GridTitle"><%=wrm.GetString("Acttxt7")%></td>
														<td class="GridTitle"><%=wrm.GetString("Acttxt20")%></td>
														<td class="GridTitle"><%=wrm.GetString("CRMdeftxt11")%></td>
														<td width="5%" class="GridTitle">&nbsp;</td>
													</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr>
													<td width="5%" class="GridItem" valign="top"><asp:Literal id="RemDate" runat="server" /></td>
													<td class="GridItem" valign="top">
														<asp:Literal id="AcId" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
														<asp:Literal id="RemID" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "RemID")%>'/>
														<asp:Literal id="AcType" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'/>
														<asp:LinkButton id="OpenActivity" runat="server" CommandName="OpenActivity" />
													</td>
													<td class="GridItem" valign="top"><%# DataBinder.Eval(Container.DataItem, "Subject")%>&nbsp;</td>
													<td class="GridItem" valign="top">
														<asp:Literal id="CompanyID" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "CompanyID")%>'/>
														<asp:LinkButton id="OpenCompany" runat="server" CommandName="OpenCompany" text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'/>&nbsp;
													</td>
													<td class="GridItem" valign="top">
														<asp:Literal id="CoId" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "ReferrerID")%>'/>
														<asp:LinkButton id="OpenContact" runat="server" CommandName="OpenContact" text='<%# DataBinder.Eval(Container.DataItem, "ReferringName")%>'/>&nbsp;
													</td>
													<td class="GridItem" valign="top">
														<asp:TextBox Id="Reminder_RemNote" runat="server" visible=false TextMode="MultiLine" height="50" class="BoxDesign" Text='<%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "ReminderNote")),true)%>'/>
														<asp:Literal id="LtrRemNote" runat=server text='<%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "ReminderNote")),true)%>'/>&nbsp;
													</td>
													<td class="GridItem" valign="top" nowrap>
														<asp:Linkbutton id="DelRem" CommandName="DelRem" runat="server" cssClass="Save"/>
														<asp:Linkbutton id="ModNote" CommandName="ModNote" runat="server" cssClass="Save"/>
														<asp:Linkbutton id="SaveNote" CommandName="SaveNote" runat="server" visible="false" cssClass="Save"/>
													</td>
												</tr>
											</ItemTemplate>
											<AlternatingItemTemplate>
												<tr>
													<td width="5%" class="GridItemAltern" valign="top"><asp:Literal id="RemDate" runat="server" /></td>
													<td class="GridItemAltern" valign="top">
														<asp:Literal id="AcId" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "id")%>'/>
														<asp:Literal id="RemID" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "RemID")%>'/>
														<asp:Literal id="AcType" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "Type")%>'/>
														<asp:LinkButton id="OpenActivity" runat="server" CommandName="OpenActivity" />
													</td>
													<td class="GridItemAltern" valign="top"><%# DataBinder.Eval(Container.DataItem, "Subject")%>&nbsp;</td>
													<td class="GridItemAltern" valign="top">
														<asp:Literal id="CompanyID" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "CompanyID")%>'/>
														<asp:LinkButton id="OpenCompany" runat="server" CommandName="OpenCompany" text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'/>&nbsp;
													</td>
													<td class="GridItemAltern" valign="top">
														<asp:Literal id="CoId" runat="server" visible="false" text='<%# DataBinder.Eval(Container.DataItem, "ReferrerID")%>'/>
														<asp:LinkButton id="OpenContact" runat="server" CommandName="OpenContact" text='<%# DataBinder.Eval(Container.DataItem, "ReferringName")%>'/>&nbsp;
													</td>
													<td class="GridItemAltern" valign="top">
														<asp:TextBox Id="Reminder_RemNote" runat="server" visible=false TextMode="MultiLine" height="50" class="BoxDesign" Text='<%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "ReminderNote")),true)%>'/>
														<asp:Literal id="LtrRemNote" runat=server text='<%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "ReminderNote")),true)%>'/>&nbsp;
													</td>
													<td class="GridItemAltern" valign="top" nowrap>
														<asp:Linkbutton id="DelRem" CommandName="DelRem" runat="server" cssClass="Save"/>
														<asp:Linkbutton id="ModNote" CommandName="ModNote" runat="server" cssClass="Save"/>
														<asp:Linkbutton id="SaveNote" CommandName="SaveNote" runat="server" visible="false" cssClass="Save"/>
													</td>
												</tr>
											</AlternatingItemTemplate>
											<FooterTemplate>
						</table>
	</FooterTemplate> </asp:Repeater>
	<br>
				</td>
			</tr>
		</TBODY></table>
 </SPAN> 
	<center><asp:Label id="RepeaterFreeInfo" runat="server" visible="false" Class="divautoformRequired" /></center>
	<asp:Repeater id="RepeaterFree" runat="server">
		<HeaderTemplate>
			<table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="left">
				<tr>
					<td class="GridTitle" width="10%"><%=wrm.GetString("CRMRemtxt7")%></td>
					<td class="GridTitle" width="80%"><%=wrm.GetString("CRMdeftxt11")%></td>
					<td class="GridTitle" width="10%">&nbsp</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td class="GridItem" valign="top" width="10%">
					<asp:Literal id="RemDate" runat="server" /></td>
				<td class="GridItem" valign="top" width="80%"><%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "Note")),true)%>&nbsp;</td>
				<td class="GridItem" valign="top" width="10%">
					<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save"/>
					<asp:Literal id="IDRem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
				</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr>
				<td class="GridItemAltern" valign="top" width="10%">
					<asp:Literal id="RemDate" runat="server" /></td>
				<td class="GridItemAltern" valign="top" width="80%"><%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem, "Note")),true)%>&nbsp;</td>
				<td class="GridItemAltern" valign="top" width="10%">
					<asp:LinkButton id="Delete" Runat="server" CommandName="Delete" cssClass="Save"/>
					<asp:Literal id="IDRem" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
				</td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
				</table>
		 	</FooterTemplate>
	</asp:Repeater>
	<table id="CardReminder" runat="server" border="0" cellpadding="3" cellspacing="0" width="50%"
		class="normal" align="center">
		<tr>
			<td>
				<%=wrm.GetString("Acttxt36")%>
				<asp:CompareValidator id="CvDateFine" runat="Server" Operator="DataTypeCheck" Type="Date" ErrorMessage="*"
					ControlToValidate="Reminder_Reminder" />
			</td>
		</tr>
		<tr>
			<td>
				<table width="100%" cellspacing="0" cellpadding="0">
					<tr>
						<td>
							<asp:TextBox id="Reminder_Reminder" runat="server" class="BoxDesign" maxlength="10" Width="100" />
						</td>
						<td align="left" width="90%">
							<div id="PopAccount4" runat="server">
								&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=Reminder_Reminder',event,195,195)">
							</div>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<%=wrm.GetString("CRMopptxt55")%>
			</td>
		</tr>
		<tr>
			<td>
				<asp:TextBox Id="Reminder_RemNote" runat="server" TextMode="MultiLine" height="50" class="BoxDesign" />
			</td>
		</tr>
		<tr>
			<td align="right">
				<asp:Label id="ReminderInfo" runat="server" visible="false" class="divautoformRequired" />
				<asp:Label id="Reminder_ID" runat="server" visible="false" class="divautoformRequired" />
				&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:LinkButton id="SubmitReminder" runat="server" cssClass="Save"  />
			</td>
		</tr>
	</table>
	</TD></TR></TBODY></TABLE>
	<script type="text/javascript">
function Hide(objid)
{
   var obj = document.getElementById(objid);
   var objbtn = document.getElementById(objid + 'btn');
   if (obj == null) return;
   if (obj.style.display == 'none'){
	obj.style.display = '';
   	objbtn.innerHTML = '<img src=images/up.gif>';
   }else{
	obj.style.display = 'none';
   	objbtn.innerHTML = '<small>' + (obj.rows.length-1) + '<small>&nbsp;<img src=images/down.gif>';
   }
}
	</script>
</form>

</body>
</html>
