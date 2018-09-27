<%@ Page language="c#" trace="false" Codebehind="AllActivity.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.WorkingCRM.AllActivity" validateRequest="false"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@Import Namespace="System.Data"%>
 <html>
<head id="head" runat="server">
<link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
<twc:LocalizedScript resource="AppVrf,Acttxt121" runat="server" />
 <script>
 var g_fFieldsChanged=1;
 var txtverdisp=AppVrf;
 var txtseldest=Acttxt121;

 </script>

 <script type="text/javascript" src="/js/allactivity.js"></script>
 <style type="text/css">
		.box { float: left;}
		#boxContent { border: 1px solid #555555; background: #ffffff;  position: relative; left: -3px; top: -3px;}
		#boxContainer { position: relative; background: #555555; margin: 4px;}
  </style>
		<script language="javascript" src="/js/dynabox.js"></script>
		<script language="javascript" src="/js/autodate.js"></script>

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" cellspacing="0" cellpadding="0">
				<tr>
					<td width="140" class="SideBorderLinked" valign="top">
						<table cellspacing=0 cellpadding=0 width="100%">
							<tr>
								<td class="sideContainer">
								<div class="sideTitle"><%=wrm.GetString("Acttxt29")%></div>
								<asp:LinkButton ID="BtnSearch" Runat="server" CssClass="sidebtn" />
								</td></tr>
								<tr><td>&nbsp;</td></tr>
								<tr>
								<td class="sideContainer">
								<div class="sideTitle"><%=wrm.GetString("Options")%></div>
									<asp:LinkButton ID="ActPhone" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActVisit" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActEmail" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActFax" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActLetter" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActGeneric" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActCase" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActMemo" Runat="server" CssClass="sidebtn" />
									<asp:LinkButton ID="ActQuote" Runat="server" CssClass="sidebtn" visible=true />
								</td>
							</tr>
						</table>
					</td>
            <td valign="top" height="100%" class="pageStyle">
						<asp:Panel id="SearchPanel" Runat=server>
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
							<%=wrm.GetString("Acttxt60")%>
						</td>
					</tr>
				</table>
				<br>
						<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0" align=center class=normal>
							<tr>
								<td valign=top width="150">
									<div><b><%=wrm.GetString("Acttxt73")%></b></div>
									<asp:CheckBoxList id="SearchType" runat="server" cssClass=normal/>
								</td>
								<td valign=top width="150">
									<div><b><%=wrm.GetString("Acttxt61")%>
									<domval:CompareDomValidator id="cvTextBoxSearchFromData" runat="Server" Operator="DataTypeCheck" Display=Dynamic Type="Date" ErrorMessage="*" ControlToValidate="TextBoxSearchFromData"/>
									</div>
									<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td>
											<asp:TextBox id="TextBoxSearchFromData" runat="server" Width="100%"  CssClass="BoxDesign"></asp:TextBox>
										</td>
										<td width="30">
											&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=TextBoxSearchFromData&Start='+(document.getElementById('TextBoxSearchFromData')).value,event,195,195)">
										</td>
										</tr>
									</table>
									<div><b><%=wrm.GetString("Acttxt62")%>
									<domval:CompareDomValidator id="cvTextBoxSearchToData" runat="Server" Operator="DataTypeCheck" Display=Dynamic Type="Date" ErrorMessage="*" ControlToValidate="TextBoxSearchToData"/>
									</div>
									<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td>
											<asp:TextBox id="TextBoxSearchToData" runat="server" Width="100%"  CssClass="BoxDesign"></asp:TextBox>
										</td>
										<td width="30">
											&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=TextBoxSearchToData&Start='+(document.getElementById('TextBoxSearchToData')).value,event,195,195)">
										</td>
										</tr>
									</table>
									<div><%=wrm.GetString("Acttxt63")%></div>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr>
									<td>
										<asp:TextBox id="TextboxSearchCompanyID" runat="server" style="display:none;"></asp:TextBox>
										<asp:TextBox id="TextboxSearchCompany" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=TextboxSearchCompany&textbox2=TextboxSearchCompanyID',event,500,400)">
									</td>
									</tr>
									</table>
									<div><%=wrm.GetString("Acttxt64")%></div>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchContactID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchContact" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=TextboxSearchContact&textboxID=TextboxSearchContactID&companyID=' + getElement('TextboxSearchCompanyID').value,event,400,300)">
									</td>
									</tr>
									</table>
									<span id="SearchLeadModule" runat=server>
									    <div><%=wrm.GetString("Acttxt89")%></div>
									    <table width="100%" cellspacing=0 cellpadding=0>
									    <tr><td>
										    <asp:TextBox id="TextboxSearchLeadID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										    <asp:TextBox id="TextboxSearchLead" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									    </td>
									    <td width="30">
										    &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopLead.aspx?render=no&textbox=TextboxSearchLead&textboxID=TextboxSearchLeadID&companyID=' + getElement('TextboxSearchCompanyID').value,event,400,300)">
									    </td>
									    </tr>
									    </table>

									    <div><%=wrm.GetString("Acttxt97")%></div>
									    <table width="100%" cellspacing=0 cellpadding=0>
									    <tr><td>
										    <asp:TextBox id="TextboxSearchOpID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										    <asp:TextBox id="TextboxSearchOp" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									    </td>
									    <td width="30">
										    &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopOpportunity.aspx?render=no&textbox=TextboxSearchOp&textboxID=TextboxSearchOpID',event,400,300)">
									    </td>
									    </tr>
									    </table>
                                    </span>
									<div><%=wrm.GetString("Acttxt65")%></b></div>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchOwnerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchOwner" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=TextboxSearchOwner&textbox2=TextboxSearchOwnerID',event)">
									</td>
									</tr>
									</table>
								</td>
								<td width="10">
								</td>
								<td valign=top width="130" style="wrap:nobr">
									<div><b><%=wrm.GetString("Acttxt66")%></b></div>
									<asp:CheckBoxList id="SearchComTec" runat="server" cssClass=normal/>
									<br>
									<div><b><%=wrm.GetString("Acttxt96")%></b></div>
									<asp:CheckBox id="CheckSum" runat="server" cssClass=normal/>
								</td>
								<td valign=top width="130">
									<div><b><%=wrm.GetString("Acttxt116")%></b></div>
									<asp:CheckBoxList id="SearchToDo" runat="server" cssClass=normal/>
									<br>
									<div><b><%=wrm.GetString("Acttxt78")%></b></div>
									<asp:CheckBoxList id="SearchPriority" runat="server" cssClass=normal/>
									<br>
									<div><b><%=wrm.GetString("Acttxt115")%></b></div>
									<asp:TextBox id="TextboxSearchDesc" runat="server" Textmode="multiline" height="50" Width="100%"  CssClass="BoxDesign"></asp:TextBox>
								</td>
							</tr>
							<tr>
								<td colspan=5 align=right><br>
									<asp:LinkButton id="SubmitSearch" runat=server cssClass=save></asp:LinkButton>
								</td>

							</tr>
						</table>
						</asp:Panel>

						<asp:Repeater id="RepeaterSearch" runat="server">
							<HeaderTemplate>
		               <table width="100%" border="0" cellspacing="0" cellpadding="0">
				            <tr>
						        <td align="left" class="pageTitle" valign="top">
											<asp:Literal id="LtrHeader" runat=server/>
									</td>
								</tr>
								</table>
								<br>
					               <table width="100%" border="0" cellspacing="0" cellpadding="0">
									<tr>
										<td class="GridTitle" width="30%"><%=wrm.GetString("Acttxt29")%></td>
										<td class="GridTitle" width="35%"><%=wrm.GetString("Acttxt7").ToUpper()%>/<%=wrm.GetString("Acttxt8").ToUpper()%>/<%=wrm.GetString("Acttxt89").ToUpper()%>
											<asp:LinkButton id="CmdOrderByCompany" CommandName="CmdOrderByCompany" runat="server" visible=false></asp:LinkButton>
										</td>
										<td class="GridTitle" width="15%" nowrap><%=wrm.GetString("Acttxt65").ToUpper()%>
										<asp:LinkButton id="CmdOrderByOwner" CommandName="CmdOrderByOwner" runat="server"></asp:LinkButton>
										</td>
										<td class="GridTitle" width="15%"><%=wrm.GetString("Acttxt11")%>
										<asp:LinkButton id="CmdOrderByDate" CommandName="CmdOrderByDate" runat="server"></asp:LinkButton>
										</td>

										<td class="GridTitle" width="4%"><%=wrm.GetString("Acttxt68")%></td>
										<td class="GridTitle" width="1%">&nbsp;</td>
									</tr>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td class="ActGridItem" width="30%">
										<asp:Label ID="Subject" Runat=server></asp:Label>
									</td>
									<td class="ActGridItem" width="35%">
										<asp:Literal id="activitywith" runat="server"></asp:Literal>
									</td>
									<td class="ActGridItem" width="15%" nowrap>
										<%# ((DataRowView)Container.DataItem)["OwnerName"] %>
									</td>
									<td class="ActGridItem" width="15%" nowrap>
										<asp:Literal id="AcDate" runat="server"/>
										<asp:Literal id="ExId" runat="server" visible=false Text='<%# ((DataRowView)Container.DataItem)["id"] %>'/>
									</td>
									<td class="ActGridItem" width="4%" align="right">
										<asp:Literal id="AcTime" runat="server"/>
									</td>
									<td class="GridItem"><asp:LinkButton id="DelAc" runat="server" CommandName="DelAc"/></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td class="ActGridItemAltern" width="30%">
										<asp:Label ID="Subject" Runat=server></asp:Label>
									</td>
									<td class="ActGridItemAltern" width="35%">
										<asp:Literal id="activitywith" runat="server"></asp:Literal>
									</td>
									<td class="ActGridItemAltern" width="15%" nowrap>
										<%# ((DataRowView)Container.DataItem)["OwnerName"] %>
									</td>
									<td class="ActGridItemAltern" width="15%" nowrap>
										<asp:Literal id="AcDate" runat="server"/>
										<asp:Literal id="ExId" runat="server" visible=false Text='<%# ((DataRowView)Container.DataItem)["id"] %>'/>
									</td>
									<td class="ActGridItemAltern" width="4%" align="right">
										<asp:Literal id="AcTime" runat="server"/>
									</td>
									<td class="GridItemAltern"><asp:LinkButton id="DelAc" runat="server" CommandName="DelAc"/></td>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								<div id="TimeSum" runat="server">
									<tr>
										<td class="GridTitle" width="95%" colspan=5 style="text-align:right">
											<%=wrm.GetString("Acttxt69")%>
										</td>
										<td class="GridTitle" width="95%" style="text-align:right">
											<asp:Literal id="LtrTime" runat="server"/>
										</td>
										<td class="GridTitle" width="95%" style="text-align:right">&nbsp;</td>
									</tr>
									<tr>
										<td class="GridTitle" width="95%" colspan=5 style="text-align:right">
											<%=wrm.GetString("Acttxt70")%>
										</td>
										<td class="GridTitle" width="95%" style="text-align:right">
											<asp:Literal id="LtrTimeTotal" runat="server"/>
										</td>
										<td class="GridTitle" width="95%" style="text-align:right">&nbsp;</td>
									</tr>
								</div>
								<tr>
									<td class="GridTitle" width="100%" colspan="7">
										<%=wrm.GetString("CounterTxt")%><asp:Literal id="RepCounter" runat=server/>
									</td>
								</tr>
								</table>
							</FooterTemplate>
						</asp:Repeater>
						<Pag:RepeaterPaging id="RepeaterSearchPaging" visible=false runat="server" />
						<asp:Label id="RepeaterSearchInfo" EnabledViewState="false" runat="server" cssClass="normal" style="color:red"/>
						<input type="hidden" id="TotalSize" runat="server">

						<asp:Panel id="AcPanel" Runat=server>

						<twc:tustenatabber id="Tabber" width="840" runat="server" EditTab="ActivityType">
							<twc:TustenaTabberRight runat=server>
		                      <twc:GobackBtn id="Back" runat="server" />
				              <spag:SheetPaging id="SheetP" runat="server"/>
							</twc:TustenaTabberRight>
						<twc:TustenaTab id="ActivityType" header="Lead" clientside=true runat=server>
					<asp:Panel id="PanelActivity" runat="server">
					<TABLE cellSpacing="1" cellPadding="1" width="99%">
					<TR>
					  <TD>
						<TABLE cellSpacing="1" cellPadding="1" width="99%">
							<TR>
								<TD valign="top" colspan="2" style="padding-left:5px;padding-right:5px;">
									<div>
									<table cellSpacing="0" cellPadding="0" class="normal" width="100%">
										<tr>
										<td width="25%">
											<asp:Literal id="LabelTypeActivity" runat="server" visible=false></asp:Literal>
											<%=wrm.GetString("Acttxt6")%>
										</td>
										<td align=right nowrap>
										    <table cellSpacing="0" cellPadding="0" class="normal">
										        <tr>
										        <td>
											        <asp:RadioButtonList id="Activity_InOut" runat="server" RepeatColumns="2">
											        </asp:RadioButtonList>
										        </td>
										        <td>
											        <asp:RadioButtonList id="Activity_ToDo" runat="server" RepeatColumns="2" cssClass="normal"></asp:RadioButtonList>
										        </td>
										        <td>
											        <asp:CheckBox ID="CheckSendMail" Runat=server TextAlign=Right CssClass="normal"></asp:CheckBox>
										        </td>
										        <td>
											        &nbsp;<input type="text" id="destinationEmail" value="" class="BoxDesign" style="display:none;width:150px;" runat="server">
										        </td>
										        </tr>
										    </table>
										</td>
										</tr>
									</table>
									</div>

											<asp:TextBox id="TextBoxSubject" runat="server" Width="100%" CssClass="BoxDesignReq" MaxLength="100" jumpret="TextboxDescrizione" startfocus></asp:TextBox>
											<asp:Label id="ActivityID" runat="server" style="display:none"/>

								</TD>
							</TR>
							<TR>
								<TD width="35%" valign="bottom" style="padding-left:5px;">
									<table cellspacing=0 cellpadding=0 class="normal">
									<tr><td>
										<%=wrm.GetString("Acttxt63")%>
									</td>
									<td>
										&nbsp;<img src="/i/lens.gif" alt="<%=wrm.GetString("AcTooltip3")%>" style="CURSOR:pointer" onclick="ViewCompany(event)">
									</td>
									</tr>
									</table>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxCompanyID" runat="server" style="display:none;"></asp:TextBox>
										<asp:TextBox id="TextboxCompany" runat="server" Width="100%"  CssClass="BoxDesignReq" readonly="true"></asp:TextBox>
									</td>
									<td width="60" style="wrap:no">
										&nbsp;<img src="/i/user.gif" alt="<%=wrm.GetString("AcTooltip4")%>" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=TextboxCompany&textbox2=TextboxCompanyID',event,500,400)">
										&nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip8")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxCompanyID');CleanField('TextboxCompany')">
									</td>
									</tr>
									</table>
								</TD>
								<TD width="35%" valign="bottom" style="padding-right:5px;">
									<div class="normal"><%=wrm.GetString("Acttxt82")%></div>
									<asp:DropDownList id="DropDownListClassification" runat="server" Width="100%"  CssClass="BoxDesign"></asp:DropDownList>
								</TD>
							</TR>

							<TR>
								<TD width="35%" valign="bottom"  style="padding-left:5px;">
									<table cellspacing=0 cellpadding=0 class="normal">
									<tr>
										<td valign="top">
											<%=wrm.GetString("Acttxt64")%>
										</td>
										<td valign="top">
											&nbsp;<img src="/i/lens.gif" alt="<%=wrm.GetString("AcTooltip3")%>" style="CURSOR:pointer" onclick="ViewContact(event)">
										</td>
									</tr>
									</table>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr>
										<td>
											<asp:TextBox id="TextboxContactID" runat="server" Width="100%" style="display:none"></asp:TextBox>
											<asp:TextBox id="TextboxContact" runat="server" Width="100%"  CssClass="BoxDesignReq" readonly="true"></asp:TextBox>
										</td>
									<td width="60" style="wrap:no">
										&nbsp;<img src="/i/user.gif" alt="<%=wrm.GetString("AcTooltip5")%>" border="0" style="cursor:pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=TextboxContact&textboxID=TextboxContactID&companyID=' + getElement('TextboxCompanyID').value,event,400,300)">
										&nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip9")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxContactID');CleanField('TextboxContact')">
									</td>
									</tr>
									</table>
								</TD>
								<TD width="35%" valign="bottom" style="padding-right:5px;">
									<div class="normal"><%=wrm.GetString("Acttxt83")%></div>
									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxParentID" runat="server" style="display:none"></asp:TextBox>
										<asp:TextBox id="TextboxParent" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="60" style="wrap:no">
										&nbsp;<img src="/i/newevent.gif" alt="<%=wrm.GetString("AcTooltip16")%>" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopActivity.aspx?render=no&textbox=TextboxParent&textboxID=TextboxParentID&company=' + getElement('TextboxCompanyID').value + '&contact=' + getElement('TextboxContactID').value + '&activity=' + document.getElementById('ActivityID').innerHTML ,event,400,300)">
										&nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip17")%>" border="0" style="cursor:pointer" onclick="CleanField('TextboxParentID');CleanField('TextboxParent')">
									</td>
									</tr>
									</table>
								</TD>
							</TR>
							<TR>
								<TD width="35%" valign="bottom" style="padding-left:5px;">
								    <span id="EditLeadModule" runat=server>
									    <table cellspacing=0 cellpadding=0 class="normal">
									    <tr><td>
										    <%=wrm.GetString("Acttxt89")%>
									    </td>
									    <td>
										    &nbsp;<img src="/i/lens.gif" alt="<%=wrm.GetString("AcTooltip3")%>" style="CURSOR:pointer" onclick="ViewLead(event)">
									    </td>
									    </tr>
									    </table>
									    <table width="100%" cellspacing=0 cellpadding=0>
									    <tr><td>
										    <asp:TextBox id="TextboxLeadID" runat="server" style="display:none;"></asp:TextBox>
										    <asp:TextBox id="TextboxLead" runat="server" Width="100%" CssClass="BoxDesignReq" readonly="true"></asp:TextBox>
									    </td>
									    <td width="60" style="wrap:no">
										    &nbsp;<img src="/i/user.gif" alt="<%=wrm.GetString("AcTooltip6")%>" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopLead.aspx?render=no&textbox=TextboxLead&textboxID=TextboxLeadID',event,400,300)">
										    &nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip10")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxLeadID');CleanField('TextboxLead')">
									    </td>
									    </tr>
									    </table>
									</span>
								</TD>
								<TD width="35%" valign="bottom" nowrap style="padding-right:5px;">
									<asp:Panel id="PanelChild" runat=server>
									<div class="normal"><%=wrm.GetString("Acttxt84")%></div>
									<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td width="91%">
											<asp:DropDownList ID="ChildType" Runat=server Width="100%" CssClass="BoxDesign"></asp:DropDownList>
										</td>
										<td width="30">
											&nbsp;<asp:LinkButton ID="ChildAction" Runat=server></asp:LinkButton>
										</td>
										</tr>
									</table>
									</asp:Panel>
								</TD>
							</TR>
							<TR>
								<TD width="35%" valign="bottom" style="padding-left:5px;">
								    <span id="EditOpportunityModule" runat=server>
									    <div class="normal"><%=wrm.GetString("Acttxt31")%></div>
									    <table width="100%" cellspacing=0 cellpadding=0>
									    <tr><td>
										    <asp:TextBox id="TextboxOpportunityID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										    <asp:TextBox id="TextboxOpportunity" runat="server" Width="100%"  CssClass="BoxDesign" readonly="true"></asp:TextBox>
									    </td>
									    <td width="60" style="wrap:no">
										    &nbsp;<img src="/i/user.gif" alt="<%=wrm.GetString("AcTooltip7")%>" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopOpportunity.aspx?render=no&textbox=TextboxOpportunity&textboxID=TextboxOpportunityID',event)">
										    &nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip11")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxOpportunityID');CleanField('TextboxOpportunity');">
									    </td>
									    </tr>
									    </table>
									</span>
								</TD>
								<TD width="35%" valign="bottom" Class="normal" style="padding-right:5px;">
								    <span id="EditDocumentModule" runat=server>
									    <div class="normal"><%=wrm.GetString("Acttxt85")%></div>
									    <table width="100%" cellspacing=0 cellpadding=0>
										    <tr>
											    <td width="60%">
												    <asp:TextBox id="DocumentDescription" runat="server" CssClass="BoxDesign" readonly/>
												    <asp:TextBox id="IDDocument" runat="server" style="display:none;"></asp:TextBox>
											    </td>
											    <td width="40%" nowrap>
												    &nbsp;
												    <img src=/i/sheet.gif border=0 alt='<%=wrm.GetString("AcTooltip18")%>' style="cursor:pointer" onclick="CreateBox('/common/PopFile.aspx?render=no&textbox=DocumentDescription&textboxID=IDDocument',event,600,500)"/>
												    <img src=/i/deletedoc.gif border=0 alt='<%=wrm.GetString("AcTooltip19")%>' style="cursor:pointer" onclick="ClearDocument()"/>
												    <asp:LinkButton id="LinkDocument" runat="server" Width="100%"  CssClass="normal"></asp:LinkButton>
											    </td>
										    </tr>
									    </table>
									</span>
								</td>
							</TR>
							<TR>
								<TD width="35%" valign="top" style="padding-left:5px;">
									<table width="100%" cellspacing=2 cellpadding=0>
										<tr>
											<td width="50%">
												<div class="normal"><%=wrm.GetString("Acttxt77")%></div>
												<asp:DropDownList id="DropDownListStatus" runat="server" Width="100%" onchange="changestatus(this.value)" CssClass="BoxDesign"></asp:DropDownList>
											</td>
											<td width="50%">
												<div class="normal"><%=wrm.GetString("Acttxt78")%></div>
												<asp:DropDownList id="DropDownListPriority" runat="server" Width="100%"  CssClass="BoxDesign"></asp:DropDownList>
											</td>
										</tr>
									</table>
									&nbsp;
								</td>
								<TD width="35%" valign="top" nowrap style="padding-right:5px;">
									<table width="100%" style="display:none;">
										<tr><td>
											<div class="normal"><%=wrm.GetString("Acttxt91")%></div>
											<asp:DropDownList id="Activity_Document" runat="server" class="BoxDesign"/>
										</td><td width="20">
											<asp:LinkButton id="DocGen" runat="server" Text="OK" class="normal"/>
										</td></tr>
									</table>
								</TD>
							</tr>
							<TR>
								<TD valign="top" colspan="2" style="padding-left:5px;padding-right:5px;">
									<table width="100%" cellspacing=2 cellpadding=0>
										<tr>
											<td width="100%" nowrap>
												<img src="/images/down.gif" onclick="ExpandDescription()" style="cursor:pointer">
												<asp:Label id="LabelDescription" runat="server" Width="100%"  CssClass="normal"></asp:Label>
											</td>
										</tr>
										<tr>
											<td>
												<asp:TextBox id="TextboxDescription" runat="server" Width="100%" Height="100px" CssClass="BoxDesign"
													TextMode="MultiLine" wrap></asp:TextBox>
											</td>
										</tr>
									</table>
								</TD>
							</TR>
							<asp:Panel id="SecondDescription" runat="server" visible=false>
							<TR>
								<TD width="100%" valign="top" colspan="2" style="padding-left:5px;padding-right:5px;">
									<div><asp:Label id="LabelDescription2" runat="server" Width="100%"  CssClass="normal">Descrizione 2</asp:Label></div>
									<asp:TextBox id="TextboxDescription2" runat="server" Width="100%" Height="100px" CssClass="BoxDesign"
										TextMode="MultiLine"></asp:TextBox>
								</TD>
							</TR>
							</asp:Panel>
						</TABLE>
						</td>
						<TD valign="top" style="padding-top:13px;padding-left:5px;border-left:1px solid #555555;">
						<TABLE cellSpacing="1" cellPadding="1" width="99%">
							<TR>
								<TD valign="top" style="padding-left:5px;padding-right:5px;">
									<asp:Panel id="PanelOwner" runat=server>
										<div class="normal"><%=wrm.GetString("Acttxt65")%></div>
										<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td>
											<asp:TextBox id="TextboxOwnerID" runat="server" Width="100%" style="display:none;"></asp:TextBox>
											<asp:TextBox id="TextboxOwner" runat="server" Width="100%" CssClass="BoxDesign" readonly="true"></asp:TextBox>
										</td>
										<td width="30">
											&nbsp;<img src="/i/user.gif" alt='<%=wrm.GetString("AcTooltip12")%>' border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=TextboxOwner&textbox2=TextboxOwnerID',event)">
										</td>
										</tr>
										</table>
									</asp:Panel>
									<div>
									<asp:Label id="LabelData" runat="server" Width="100%"  CssClass="normal">Data</asp:Label>
										<domval:RequiredDomValidator id="DataValidator" EnableClientScript="False" Display=Dynamic
											runat="server" ControlToValidate="TextBoxData" ErrorMessage="*"/>
										<domval:CompareDomValidator id="CvRecDate" runat="Server" Operator="DataTypeCheck" Display=Dynamic Type="Date" ErrorMessage="*" ControlToValidate="TextBoxData"/>
									</div>

									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextBoxData" onkeypress="DataCheck(this,event)" runat="server" Width="100%"  CssClass="BoxDesign"></asp:TextBox>
									</td>
									<td width="60" valign="center" align="center">
										<asp:PlaceHolder ID="IMGAvailability_holder" runat="server">
										&nbsp;
										<img src="/i/free.gif" alt='<%=wrm.GetString("AcTooltip13")%>' id="IMGAvailability" runat="server" border="0" style="cursor:pointer" onclick="AppointmentVerify(1,event);">
										</asp:PlaceHolder>
										&nbsp;<img src="/i/SmallCalendar.gif" alt='<%=wrm.GetString("AcTooltip14")%>' border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?datediff=1&textbox=TextBoxData&Start='+(document.getElementById('TextBoxData')).value,event,195,195)">
									</td>
									</tr>
									<tr>
										<td colspan=3>
											<table id="HourPanel" runat=server width="100%" cellspacing="0" cellpadding="0" border="0" class=normal style="padding-top:2px;">
	    										<tr>
	    										<td>
	    											<%=Capitalize(wrm.GetString("Evnttxt6"))%>
	    										</td>
	        									<td>
	            									<asp:TextBox ID="TextBoxHour" onkeypress="HourCheck(this,event)" runat="server" maxlength="8" width="50" class="BoxDesign"  />
	        									</td>
	        									<td valign="middle" align="left" width="40">
	            	  								<img src="/images/up.gif" onclick="HourUp('TextBoxHour')" />
	            									<img src="/images/down.gif" onclick="HourDown('TextBoxHour')" />
	        									</td>
	    										</tr>
										    </table>
										</td>
									</tr>
									</table>
									<span id="Appointmenthours" runat=server>
									<table width="100%" cellspacing="0" cellpadding="0" border="0" class=normal>
									<tr>
										<td width="40">
										<%=Capitalize(wrm.GetString("Evnttxt5"))%>
		    							<domval:RequiredDomValidator id="StartHourValidator" EnableClientScript="false" Display=Dynamic
											runat="server" ControlToValidate="F_StartHour" ErrorMessage="*"/>
										</td>
										<td>
										<table width="100%" cellspacing="0" cellpadding="0" border="0">
	    										<tr>
	        									<td width="50">
	            									<asp:TextBox ID="F_StartHour" onkeypress="HourCheck(this,event)" runat="server" maxlength="8" width="50" class="BoxDesign"  />
	        									</td>
	        									<td valign="middle" align="left" width="40">
	            	  								<img src="/images/up.gif" onclick="HourUp('F_StartHour')" />
	            									<img src="/images/down.gif" onclick="HourDown('F_StartHour')" />
	        									</td>
	    										</tr>
										</table>
										</td>
									</tr>
									<tr>
										<td width="40">
										<%=Capitalize(wrm.GetString("Evnttxt6"))%>
										<domval:RequiredDomValidator id="EndHourValidator" EnableClientScript="false" Display=Dynamic
											runat="server" ControlToValidate="F_EndHour" ErrorMessage="*"/>
									</td>
									<td>
									<table width="100%" cellspacing="0" cellpadding="0" border="0">
	    									<tr>
	        								<td width="50">
	            								<asp:TextBox ID="F_EndHour" onkeypress="HourCheck(this,event)" runat="server" maxlength="8"  width="50" class="BoxDesign"  />
	        								</td>
	        								<td valign="middle" align="left" width="40">
	            								<img src="/images/up.gif" onclick="HourUp('F_EndHour')" />
	            								<img src="/images/down.gif" onclick="HourDown('F_EndHour')" />
	        								</td>
	    									</tr>
									</table>
									</td>
									</tr>
									</table>
									<table width="100%" cellspacing=0 cellpadding=0 class=normal>
										<tr><td colspan=2><%=Capitalize(wrm.GetString("Evnttxt55"))%></td></tr>
										<tr><td width="90%">
											<asp:TextBox id="IdCompanion" runat="server" Width="100%" style="display:none;"></asp:TextBox>
											<asp:TextBox id="Companion" runat="server" Width="100%" CssClass="BoxDesign" readonly="true"></asp:TextBox>
										</td>
										<td width="50" valign=bottom><nobr>
											<img src="/i/user.gif" alt='<%=wrm.GetString("AcTooltip12")%>' border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopAccount.aspx?render=no&textbox=Companion&textbox2=IdCompanion',event)">
											<img src="/i/free.gif" alt="<%=wrm.GetString("AcTooltip13")%>" name="UserAppImg" id="UserAppImg" border="0" style="cursor:pointer" onclick="AppointmentVerify(2,event);">
											<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip15")%>" name="UserAppImg" id="UserAppImg" border="0" style="cursor:pointer" onclick="RemoveCompanion();">
											</nobr>
										</td>
										</tr>
									</table>
									</span>

										<table width="100%">
										<tr>
										<td>
										<div class="normal"><%=wrm.GetString("Acttxt79")%></div>

											<asp:DropDownList id="DropDownListPreAlarm" runat="server" Width="100%"  CssClass="BoxDesign"></asp:DropDownList>

										</td>
										</tr>
										<tr>
										<td style="padding-top:10px">
										<table width="100%" style="border:1px solid #dcdcdc">
											<tr>
												<td bgcolor="#DCDCDC" class="normal">
													<%=wrm.GetString("Acttxt123")%>
												</td>
											</tr>
											<tr>
											<td>
											<asp:CheckBox ID="CheckToBill" Runat=server Text="Da Fatturare" TextAlign=Right CssClass="normal"></asp:CheckBox>
											</td>
											</tr>
											<tr>
											<td>
											<asp:CheckBox ID="CheckCommercial" Runat=server Text="Commerciale" TextAlign=Right CssClass="normal" Checked=True></asp:CheckBox>
											</td>
											</tr>
											<tr>
											<td>
											<asp:CheckBox ID="CheckTechnical" Runat=server Text="Tecnica" TextAlign=Right CssClass="normal"></asp:CheckBox>
											</td>
											</tr>

											<tr>
											<td>
											<div class="normal"><%=wrm.GetString("Acttxt81")%></div>
											<table id="duration" width="100%" cellspacing=0 cellpadding=0 class=normal style="padding-top:3px">
												<tr><td width="50%" nowrap>
													<asp:TextBox id="TextBoxDurationH" runat="server" width="30px" MaxLength=2 CssClass="BoxDesign"></asp:TextBox>&nbsp;hh
												</td>
												<td width="50%" nowrap>
													<asp:TextBox id="TextBoxDurationM" runat="server" width="30px" MaxLength=2 CssClass="BoxDesign"></asp:TextBox>&nbsp;mm
												</td>
												</tr>
											</table>
											</td>
											</tr>
										</table>
										</td>
										</tr>
										</table>
								</TD>
							</TR>
						</TABLE>
						</td>
						</tr>
						</table>
						<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="99%" border="0">
							<tr id="MoveLog" runat="server">
								<td colspan=2>
									<div class=normal style="cursor:pointer;" onclick="ViewHideLog()"><span onclick="ViewHideLog()" id="LogView">[+]</span> <%=wrm.GetString("Acttxt114")%></div>
									<asp:PlaceHolder id="MoveLogTable" runat=server/>
								</td>
							</tr>
							<TR>
							<td align=left nowrap style="padding-left:10px">
								<asp:Label id="ActivityInfo" runat="server" cssClass="divautoformRequired"/>
							</td>
							<td align=right>
							<asp:LinkButton ID="SubmitBtn" Runat=server CssClass="save"></asp:LinkButton>
							&nbsp;&nbsp;
							<asp:LinkButton ID="SubmitBtnDoc" Runat=server CssClass="save"></asp:LinkButton>
							</td>
							</TR>
						</table>
						</asp:Panel>
						 </twc:TustenaTab>
						</twc:TustenaTabber>
					</asp:Panel>
					</td>
				</tr>
			</table>
		</form>

</body>
</html>
