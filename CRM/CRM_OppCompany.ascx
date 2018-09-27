<%@ Control Language="c#" AutoEventWireup="true" Codebehind="CRM_OppCompany.ascx.cs" Inherits="Digita.Tustena.CRM_OppCompany" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
  <twc:jscontrolid id="jsc" runat=server />
  <script>
  var Closed=0;
  var Lost = 0;

  function SelectOther()
  {
	var obj = document.getElementById(jsControlId+"CompanyLostReasons");
	var obj1 = document.getElementById(jsControlId+"NewLostReason");
	if(obj.options[obj.selectedIndex].value=="A99")
	{
		obj1.style.display='';
		obj1.style.background='#ffffff';
		obj.style.display='none';
	}
  }

  function selectchange(obj)
  {
	if(obj.options[obj.selectedIndex].value=="6")
	{
		Closed=1;
		var obj2 = document.getElementById(jsControlId+"CompanyNewPhaseList");
		obj2.selectedIndex=0;
		for(var i=0;i<obj2.options.length;i++){
			if(obj2.options[i].value=="13"){
				obj2.selectedIndex=i;
				break;
			}
		}
		var obj3 = document.getElementById(jsControlId+"CompanyNewProbList");
		obj3.selectedIndex=0;
		for(var i=0;i<obj3.options.length;i++){
			if(obj3.options[i].value=="22"){
				obj3.selectedIndex=i;
				break;
			}
		}
	}

	if(obj.options[obj.selectedIndex].value=="6" || obj.options[obj.selectedIndex].value=="3" || obj.options[obj.selectedIndex].value=="2" || obj.options[obj.selectedIndex].value=="1")
	{
		var closedate = document.getElementById(jsControlId+"CompanyNewCloseDate");
		closedate.value = formatDate(new Date(),DatePattern);
		if(obj.options[obj.selectedIndex].value=="3" || obj.options[obj.selectedIndex].value=="2" || obj.options[obj.selectedIndex].value=="1")
			Lost=1;
	}else{
		var closedate = document.getElementById(jsControlId+"CompanyNewCloseDate");
		closedate.value = "";
	}

  }

  function checkbeforesubmit()
  {
	if(Closed==1){
		var x = document.getElementById(jsControlId+"CompanyNewAmountClosed");
		var ok=true;
		if(x.value.lenght<1 || x.value=="0"){
			(document.getElementById(jsControlId+"AmountValidator")).style.display="inline";
			ok = false;
		}else
			(document.getElementById(jsControlId+"AmountValidator")).style.display="none";
		x = document.getElementById(jsControlId+"CompanyNewCloseDate");
		if(x.value.lenght<0){
			(document.getElementById(jsControlId+"CloseDateValidator")).style.display="inline";
			ok = false;
		}else
			(document.getElementById(jsControlId+"CloseDateValidator")).style.display="none";
		return ok;
	}else if(Lost==1){
		var lost = document.getElementById(jsControlId+"CompanyLostReasons");
		if(lost.selectedIndex<1){
			(document.getElementById("LostReasonsValidator")).style.display="inline";
			return false;
		}else{
			(document.getElementById("LostReasonsValidator")).style.display="none";
			return true;
		}
	}else{
		return true;
	}
  }

  function ViewHideRef()
  {
	var obj = document.getElementById(jsControlId+"ReferrerList");
	var obj2 = document.getElementById(jsControlId+"TableNewReferrer");
	var ViewHideReferrer = document.getElementById(jsControlId+"ViewHideReferrer");

	if (obj.style.display == 'none'){
		obj.style.display = '';
   		obj2.style.display = 'none';
   		ViewHideReferrer.innerHTML = '<%=wrm.GetString("CRMopptxt38")%>'
	}else{
		obj.style.display = 'none';
   		obj2.style.display = '';
   		ViewHideReferrer.innerHTML = '<%=wrm.GetString("CRMopptxt39")%>'
	}
	if (parent.adjustIFrameSize)parent.adjustIFrameSize(window);
  }

  function getchangecurrency(){
	//var x = document.getElementById("Opportunity_Currency");
	//var change = x.options[x.selectedIndex].value.split("|");
	//return change[1];
	return 1;

}

function ValidateProduct(){
	var x = document.getElementById(jsControlId+"EstProductID");
	if(x.value!=""){
			(document.getElementById("rvProductID")).style.display="none";
			return true;
	}else{
			(document.getElementById("rvProductID")).style.display="inline";
			return false;
	}
}



  </script>
			<twc:tustenatabber id="Tabber" width="840" runat="server">
			<twc:TustenaTabberRight runat=server ID="Tustenatabberright1">
			<twc:GoBackBtn ID="btnGoBack" Runat=server />

			<spag:SheetPaging id="SheetP" runat=server></spag:SheetPaging>
			</twc:TustenaTabberRight>
			<twc:TustenaTab id="Table_Tab1" langheader="CRMopptxt24" clientside=true runat=server>
				<table border="0" cellpadding="0" cellspacing="3" width="99%" class="normal">
				<tr>
				<td width="33%" valign=top>
					<table border="0" cellpadding="0" cellspacing="3" width="99%" class="normal">
					<tr>
						<td width="100%">
							<div>
								<twc:LocalizedLiteral text="CRMopptxt24" runat="server"/>
								<domval:RequiredDomValidator ID="CompanyNewCompanyIDVal" Runat="server" EnableClientScript="False" ControlToValidate="CompanyNewCompanyID"
									ErrorMessage="*"></domval:RequiredDomValidator>
							</div>
							<table width="100%" cellspacing=0 cellpadding=0>
								<tr><td>
								<asp:TextBox id="CompanyNewCompanyID" runat="server" style="display:none"/>
								<asp:TextBox Id="CompanyNewCompany" runat="server" cssclass="BoxDesign" ReadOnly="true" />
								</td><td width="20" nowrap>
									<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/common/PopCompany.aspx?render=no&textbox='+jsControlId+'CompanyNewCompany&textbox2='+jsControlId+'CompanyNewCompanyID',event,500,400);">
								</td></tr>
							</table>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt75" runat="server"/>
							<domval:RequiredDomValidator ID="CompanyNewExpectedRevenueVal" Runat="server" EnableClientScript="False" ControlToValidate="CompanyNewExpectedRevenue"
									ErrorMessage="*"></domval:RequiredDomValidator>
							</div>
							<asp:TextBox id="CompanyNewExpectedRevenue" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt14" runat="server"/>
							<span id="AmountValidator" style="color:red;display:none;">*</span>
							</div>
							<asp:TextBox id="CompanyNewAmountClosed" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt13" runat="server"/></div>
							<asp:TextBox id="CompanyNewAmountRevenuePercent" runat="server" readonly="true" cssclass="BoxDesign"/>
						</td>
					</tr>

				</table>
			</td>
			<td width="33%" valign=top>
				<table border="0" cellpadding="0" cellspacing="3" width="99%" class="normal">
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt3" runat="server"/>
							<domval:RequiredDomValidator ID="CompanyNewStateListVal" Runat="server" EnableClientScript="False" InitialValue="0" ControlToValidate="CompanyNewStateList"
									ErrorMessage="*"></domval:RequiredDomValidator>
							</div>
							<asp:DropDownList id="CompanyNewStateList" old="true" runat="server" cssclass="BoxDesign" style="width:100%"/>
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt4" runat="server"/></div>
							<asp:DropDownList id="CompanyNewPhaseList" old="true" runat="server" cssclass="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="CRMopptxt5" runat="server"/></div>
							<asp:DropDownList id="CompanyNewProbList" old="true" runat="server" cssclass="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td width="100%">
							<div><twc:LocalizedLiteral text="Mottxt6" runat="server"/>
							<span id="LostReasonsValidator" style="color:red;display:none;">*</span>
							</div>
							<asp:DropDownList id="CompanyLostReasons" old="true" runat="server" cssclass="BoxDesign"/>
							<asp:TextBox id="NewLostReason" runat="server" cssclass="BoxDesign" style="display:none;"/>
						</td>
					</tr>
				</table>
				</td>
				<td width="34%" valign=top>
					<table border="0" cellpadding="0" cellspacing="3" width="99%" class="normal">
						<tr>
							<td width="100%">
								<div><twc:LocalizedLiteral text="CRMopptxt76" runat="server"/></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
									<asp:TextBox id="CompanyNewSalesPersonID" runat="server" style="display:none"/>
									<asp:TextBox Id="CompanyNewSalesPerson" runat="server" cssclass="BoxDesign" ReadOnly="true" />
									</td><td width="20" nowrap>
										<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/common/PopAccount.aspx?render=no&textbox=CompanyNewSalesPerson&textbox2=CompanyNewSalesPersonID',event);">
									</td></tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="100%">
								<div><twc:LocalizedLiteral text="CRMopptxt77" runat="server"/></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
									<asp:TextBox id="CompanyNewStartDate" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
									&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewStartDate&Start='+(document.getElementById('CompanyNewStartDate')).value,event,195,195)">
									</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="100%">
								<div><twc:LocalizedLiteral text="CRMopptxt78" runat="server"/></div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
									<asp:TextBox id="CompanyNewEstimatedCloseDate" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="30">
									&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewEstimatedCloseDate&Start='+(document.getElementById('CompanyNewEstimatedCloseDate')).value,event,195,195)">
									</td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td width="100%">
								<div><twc:LocalizedLiteral text="CRMopptxt79" runat="server"/>
								<span id="CloseDateValidator" style="color:red;display:none;">*</span>
								</div>
								<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
									<asp:TextBox id="CompanyNewCloseDate" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true" ></asp:TextBox>
									</td>
									<td width="30">
									&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewCloseDate&Start='+(document.getElementById('CompanyNewCloseDate')).value,event,195,195)">
									</td>
									</tr>
								</table>
							</td>
						</tr>
					</table>
				</td>
				</tr>
				<tr>
					<td colspan=3>
					<table id="InsertProductTable" runat=server style="display:inline" border="0" cellspacing="0" width="100%" align="center" class="normal">
					<tr>
						<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt81" runat="server"/></td>
					</tr>
	   				<tr>
	   					<td>
							<table border="0" cellspacing="0" width="100%" align="center" class="normal" style="border:1px solid #000000;">
	   							<tr>
	   								<td width="30%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/>
											<span id="rvProductID" class="normal" style="display:none;color:red;">*</span>
	   									</div>
	   									<table width="100%" cellspacing=0 cellpadding=0>
											<tr><td valign="top">
											<asp:TextBox id="EstProductID" runat="server" cssclass="BoxDesign" style="display:none"/>
											<asp:TextBox id="EstProduct" runat="server" cssclass="BoxDesign" ReadOnly="true"/>
											</td><td width="30" valign="top">
											&nbsp;<img src="/i/lookup.gif" border="0" style="cursor:pointer;" onclick="CreateBox('/Common/PopCatalog.aspx?render=no&ptx='+jsControlId+'EstProduct&pid='+jsControlId+'EstProductID&um='+jsControlId+'EstUm&qta='+jsControlId+'EstQta&up='+jsControlId+'EstUp&vat='+jsControlId+'EstVat&pl='+jsControlId+'EstPl&pf='+jsControlId+'EstPf&ch='+getchangecurrency(),event,400,300)">
											</td></tr>
										</table>
	   								</td>
	   								<td width="10%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></div>
	   									<asp:TextBox id="EstUm" runat="server"  ReadOnly="true" cssclass="BoxDesign"/>
	   								</td>
	   								<td width="10%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></div>
	   									<asp:TextBox id="EstQta" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
	   								</td>
	   								<td width="20%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></div>
	   									<asp:TextBox id="EstUp" runat="server"  ReadOnly="true" cssclass="BoxDesign"/>
	   								</td>
	   								<td width="10%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></div>
	   									<asp:TextBox id="EstVat" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
	   								</td>
	   								<td width="20%">
	   									<div><twc:LocalizedLiteral text="CRMcontxt70" runat="server"/></div>
	   									<asp:TextBox id="EstPl" runat="server"  ReadOnly="true" cssclass="BoxDesign"/>
	   								</td>
	   							</tr>
	   							<tr>
	   								<td colspan=5>
										<div><twc:LocalizedLiteral text="CRMcontxt72" runat="server"/></div>
	   									<asp:TextBox id="EstDescription2" runat="server" cssclass="BoxDesign"/>
	   								</td>
	   								<td>
	   									<div><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></div>
	   									<asp:TextBox id="EstPf" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
	   								</td>
	   							</tr>
	   							<tr>
	   								<td colspan=3>
	   									<asp:LinkButton id="BtnCalcPrice" runat="server" cssclass="save"/>
	   								</td>
	   								<td colspan=3 align="right">
	   									<asp:LinkButton id="BtnAddProduct" runat="server" cssclass="save"/>
	   								</td>
	   							</tr>
							</table>
						</td>
					</tr>
					</table>
					<br>
								<asp:Repeater id="RepeaterEstProduct" runat="server">
	 									<HeaderTemplate>
	 										<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	 										<tr>
	 											<td class="GridTitle" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
	 											<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
	 											<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
	 											<td class="GridTitle" width="20%"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
	 											<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
	 											<td class="GridTitle" width="19%"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
	 											<td class="GridTitle" width="1%">&nbsp;</td>
	 										</tr>
	 									</HeaderTemplate>
	 									<ItemTemplate>
	 										<tr>
	 											<td class="GridItem" width="30%"><asp:Label id="ShortDescription" runat="server"/></td>
	 											<td class="GridItem" width="10%"><asp:Label id="UM" runat="server"/></td>
	 											<td class="GridItem" width="10%"><asp:Label id="Qta" runat="server"/></td>
	 											<td class="GridItem" width="20%"><asp:Label id="UnitPrice" runat="server"/></td>
	 											<td class="GridItem" width="10%"><asp:Label id="Vat" runat="server"/></td>
	 											<td class="GridItem" width="19%"><asp:Label id="FinalPrice" runat="server"/></td>
	 											<td class="GridItem" width="1%">
	 												<asp:LinkButton id="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>"/>
	 												<asp:Literal id="ObjectID" runat="server" visible="false"/>
	 											&nbsp;
	 											</td>
	 										</tr>
	 									</ItemTemplate>
										<AlternatingItemTemplate>
											<tr>
	 											<td class="GridItemAltern" width="30%"><asp:Label id="Label1" runat="server"/></td>
	 											<td class="GridItemAltern" width="10%"><asp:Label id="Label2" runat="server"/></td>
	 											<td class="GridItemAltern" width="10%"><asp:Label id="Label3" runat="server"/></td>
	 											<td class="GridItemAltern" width="20%"><asp:Label id="Label4" runat="server"/></td>
	 											<td class="GridItemAltern" width="10%"><asp:Label id="Label5" runat="server"/></td>
	 											<td class="GridItemAltern" width="19%"><asp:Label id="Label6" runat="server"/></td>
	 											<td class="GridItemAltern" width="1%">
	 												<asp:LinkButton id="Linkbutton1" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>"/>
	 												<asp:Literal id="Literal1" runat="server" visible="false"/>
	 											&nbsp;
	 											</td>
	 										</tr>
										</AlternatingItemTemplate>
										<FooterTemplate>
											</table>
										</FooterTemplate>
							</asp:Repeater>

					</td>
				</tr>
				<tr>
					<td colspan="3">
						<twc:LocalizedLiteral text="CRMopptxt27" runat="server"/>
					</td>
				</tr>
				<tr>
					<td colspan="3" height="100">
						<asp:TextBox Id="CompanyNewNote" runat="server" TextMode="MultiLine" height="100px" cssclass="BoxDesign" />
					</td>
				</tr>
				<tr>
					<td colspan="2" align="right">&nbsp;
							<asp:Label Id="CompanyNewInfo" runat="server" cssclass="divautoformRequired" />
					</td>
					<td align="right">
							<asp:TextBox id="CompanyNewID" runat="server" visible="false"/>
							<asp:LinkButton Id="CompanyNewSubmit" runat="server" cssclass="save" />
					</td>
				</tr>
				</table>
			</twc:TustenaTab>
			<twc:TustenaTab id="Table_Tab6" LangHeader="CRMopptxt57" clientside=false runat=server>
					<table border="0" cellpadding="0" cellspacing="3" width="100%" class="normal">
						<tr>
						<td>
	  						<asp:Repeater id="ViewContact" runat="server" visible="false" >
							<HeaderTemplate>
								<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
							</HeaderTemplate>
							<ItemTemplate>
		 						<tr>
		 						<td style="border-bottom: 1px solid #000000;" colspan="2">
		 						<b><twc:LocalizedLiteral text="Bcotxt15" runat="server"/></b>
		 						</td>
								</tr>
		 						<tr><td width="50%" valign="TOP">
		 							<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
										<tr>
										<td width="40%">
											<twc:LocalizedLiteral text="Bcotxt17" runat="server"/>
										</td>
										<td class="VisForm">
											<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
										</td>
										</tr>

										<tr>
										<td width="40%">
										<twc:LocalizedLiteral text="Bcotxt26" runat="server"/>
										</td>
										<td class="VisForm">
										<%#DataBinder.Eval(Container.DataItem,"InvoicingAddress")%>
										</td>
										</tr>
										<tr>
										<td width="40%">
										<twc:LocalizedLiteral text="Bcotxt27" runat="server"/>
										</td>
										<td class="VisForm">
										<%#DataBinder.Eval(Container.DataItem,"InvoicingCity")%>
										</td>
									</tr>
									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt28" runat="server"/>
									</td>
									<td class="VisForm">
									<%#DataBinder.Eval(Container.DataItem,"InvoicingStateProvince")%>
									</td>
									</tr>
									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt29" runat="server"/>
									</td>
									<td class="VisForm">
									<%#DataBinder.Eval(Container.DataItem,"InvoicingZipCode")%>
									</td>
									</tr>

									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt20" runat="server"/>
									</td>
									<td class="VisForm">
									<%#DataBinder.Eval(Container.DataItem,"Phone")%>
									</td>
									</tr>
									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt21" runat="server"/>
									</td>
									<td class="VisForm">
									<%#DataBinder.Eval(Container.DataItem,"FAX")%>
									</td>
									</tr>
									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt22" runat="server"/>
									</td>
									<td class="VisForm">
									<a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>"><%#DataBinder.Eval(Container.DataItem,"Email")%></a>
									</td>
									</tr>
									<tr>
									<td width="40%">
									<twc:LocalizedLiteral text="Bcotxt23" runat="server"/>
									</td>
									<td class="VisForm">
									<a href='http://<%#DataBinder.Eval(Container.DataItem,"WebSite")%>' target="_blank"><span style="text-decoration:underline;"><%#DataBinder.Eval(Container.DataItem,"WebSite")%></span></a>
									</td>
									</tr>

	    						</table>
    						</td><td width="50%" valign="TOP">
	    						<table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="Bcotxt19" runat="server"/>
								</td>
								<td class="VisForm">
								<%#DataBinder.Eval(Container.DataItem,"CompanyCode")%>
								</td>
								</tr>
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="CRMcontxt8" runat="server"/>
								</td>
								<td class="VisForm">
								<%#DataBinder.Eval(Container.DataItem,"CompanyType")%>
								</td>
								</tr>
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="CRMcontxt9" runat="server"/>
								</td>
								<td class="VisForm">
								<%#DataBinder.Eval(Container.DataItem,"ContactType")%>
								</td>
								</tr>
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="CRMcontxt10" runat="server"/>
								</td>
								<td class="VisForm">
								&nbsp;<%#DataBinder.Eval(Container.DataItem,"Billed", "{0:###,###.00}")%>
								</td>
								</tr>
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="CRMcontxt11" runat="server"/>
								</td>
								<td class="VisForm">
								<%#DataBinder.Eval(Container.DataItem,"Employees")%>
								</td>
	    						</tr>
								<tr>
								<td width="40%">
								<twc:LocalizedLiteral text="CRMcontxt12" runat="server"/>
								</td>
								<td class="VisForm">
								<%#DataBinder.Eval(Container.DataItem,"EstimateDesc")%>
								</td>
								</tr>
    						</table>
   						</td>
   						</tr>
						<tr>
						<td width="100%" colspan="2">
							<twc:LocalizedLiteral text="CRMopptxt44" runat="server"/>
						</td>
						</tr>
								<tr>
								<td width="100%" colspan="2" style="height:50px" class="VisForm" valign="top">
								<%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem,"Description")),false)%>&nbsp;
								</td>
								</tr>
									<tr><td colspan="2">
		    						<asp:Literal id="ViewFreeFields" runat="server" />
		    						</td>
		    						</tr>
							</ItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
							</asp:Repeater>
						</td>
						</tr>
					</table>
			</twc:TustenaTab>
			<twc:TustenaTab id="Table_Tab2" langheader="CRMopptxt33" clientside=false runat=server>
					<table border="0" cellpadding="0" cellspacing="2" width="100%">
						<tr>
						<td align="right" valign="top">
						<span id="ViewHideReferrer" class="save" onclick="ViewHideRef();"><twc:LocalizedLiteral text="CRMopptxt38" runat="server"/></span>
						</td>
						</tr>
						<tr>
						<td id="ReferrerList" style="display:inline" valign="top">
								<asp:Repeater id="RepeaterReferrer" runat="server">
								<HeaderTemplate>
									<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal">
									<tr>
									<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt34" runat="server"/></td>
									<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt35" runat="server"/></td>
									<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt36" runat="server"/></td>
									<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt37" runat="server"/></td>
									<td class="GridTitle" width="10%">&nbsp;</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
								<div id="ViewReferrer" runat="server">
								<tr>
								<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "ReferrerName") %></td>
								<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "Role") %></td>
								<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "PercDecisional") %></td>
								<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "CharacterText") %></td>
								<td class="GridItem" width="10%">
								<asp:TextBox id="CrossID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'/>
								<asp:LinkButton id="ModReferrer" runat="server" CommandName="ModReferrer"/>
								</td>
								</tr>
								</div>
								<div id="ModifyReferrer" runat="server">
								<tr>
								<td class="GridItem">
								<%# DataBinder.Eval(Container.DataItem, "ReferrerName") %>
								</td>
								<td class="GridItem"><asp:TextBox id="Role" runat="server" cssclass="BoxDesign" Text='<%# DataBinder.Eval(Container.DataItem, "Role") %>'/></td>
								<td class="GridItem"><asp:TextBox id="PercDecisional" runat="server" cssclass="BoxDesign" Text='<%# DataBinder.Eval(Container.DataItem, "PercDecisional") %>'/></td>
								<td class="GridItem">
								<asp:DropDownList id="Character" runat="server" cssclass="listboxautoform"/>
								</td>
								<td class="GridItem" width="10%">
								<asp:LinkButton id="SaveReferrer" runat="server" CommandName="SaveReferrer"/>
								</td>
								</tr>
								</div>
								</ItemTemplate>
								<AlternatingItemTemplate>
								<div id="ViewReferrer" runat="server">
								<tr>
								<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "ReferrerName") %></td>
								<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "Role") %></td>
								<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "PercDecisional") %></td>
								<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "CharacterText") %></td>
								<td class="GridItemAltern" width="10%">
								<asp:TextBox id="CrossID" runat="server" visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID") %>'/>
								<asp:LinkButton id="ModReferrer" runat="server" CommandName="ModReferrer"/>
								</td>
								</tr>
								</div>
								<div id="ModifyReferrer" runat="server">
								<tr>
								<td class="GridItemAltern">
								<%# DataBinder.Eval(Container.DataItem, "ReferrerName") %>
								</td>
								<td class="GridItemAltern"><asp:TextBox id="Role" runat="server" cssclass="BoxDesign" Text='<%# DataBinder.Eval(Container.DataItem, "Role") %>'/></td>
								<td class="GridItemAltern"><asp:TextBox id="PercDecisional" runat="server" cssclass="BoxDesign" Text='<%# DataBinder.Eval(Container.DataItem, "PercDecisional") %>'/></td>
								<td class="GridItemAltern">
									<asp:DropDownList id="Character" runat="server" cssclass="listboxautoform"/>
								</td>
								<td class="GridItemAltern" width="10%">
									<asp:LinkButton id="SaveReferrer" runat="server" CommandName="SaveReferrer"/>
								</td>
								</tr>
								</div>
								</AlternatingItemTemplate>
								<FooterTemplate>
								</table>
								</FooterTemplate>
								</asp:Repeater>
								<center><asp:Label id="RepeaterReferrerInfo" runat="server" cssclass="divautoformRequired"/></center>
						</td>
						</tr>
						<tr>
						<td id="TableNewReferrer" style="display:none" valign="top">
							<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" >
								<tr>
								<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt34" runat="server"/></td>
								<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt35" runat="server"/></td>
								<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt36" runat="server"/></td>
								<td class="GridTitle"><twc:LocalizedLiteral text="CRMopptxt37" runat="server"/></td>
								<td class="GridTitle" width="10%">&nbsp;</td>
								</tr>
								<tr>
								<td class="GridItemAltern">

									<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
									<asp:TextBox id="ReferrerID" runat="server" style="display:none"/>
									<asp:TextBox id="ReferrerText" runat="server" cssclass="BoxDesign" ReadOnly="true"/>
									</td><td width="30">
									&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="FrameCreateBox('/common/popcontacts.aspx?render=no&textbox=ReferrerText&textboxID=ReferrerID&CompanyID=<%=Session["NewCompanyID"]%>',event)">
									</td></tr>
									</table>
								</td>
								<td class="GridItemAltern"><asp:TextBox id="Role" runat="server" cssclass="BoxDesign"/></td>
								<td class="GridItemAltern"><asp:TextBox id="PercDecisional" runat="server" cssclass="BoxDesign"/></td>
								<td class="GridItemAltern"><asp:DropDownList id="Character" runat="server" cssclass="listboxautoform"/></td>
								<td class="GridItemAltern" width="10%">
								<asp:LinkButton id="SaveNewReferrer" runat="server" />
								</td>
								</tr>
							</table>
						</td>
						</tr>
					</table>
			</twc:TustenaTab>
			<twc:TustenaTab id="Table_Tab3" langheader="CRMopptxt41" clientside=false runat=server>
					<table border="0" cellpadding="0" cellspacing="3" width="100%" class="normal">
						<tr>
						<td>
							<asp:Repeater id="CompanyCrossCompetitor" runat="server">
								<HeaderTemplate>
									<table class="tblstruct normal">
									<tr>
									<td class="GridTitle" width="20%"><twc:LocalizedLiteral text="CRMopptxt41" runat="server"/></td>
									<td class="GridTitle" width="70%"><twc:LocalizedLiteral text="CRMopptxt51" runat="server"/></td>
									<td class="GridTitle" width="10%">&nbsp;</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<div id="ViewCompetitor" runat=server>
										<tr>
										<td class="GridItem" width="20%" valign="top"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
										<td class="GridItem" width="70%"><asp:Label id="Relation" runat="server"/>&nbsp;</td>
										<td class="GridItem" width="10%" valign="top"><asp:LinkButton id="ModRelation" CommandName="ModRelation" runat="server" cssclass="normal"/></td>
										</tr>
									</div>
									<div id="ModifyCompetitor" runat=server>
										<tr>
										<td class="GridItem" width="20%" valign="top">
										<asp:Literal id="IDcross" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' visible="false"/>
										<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
										</td>
										<td class="GridItem" width="70%"><asp:TextBox id="TbxRelation" runat="server" Height="80px" CssClass="BoxDesign" TextMode="MultiLine"/></td>
										<td class="GridItem" width="10%" valign="top"><asp:LinkButton id="SaveRelation" CommandName="SaveRelation" runat="server" cssclass="normal"/></td>
										</tr>
									</div>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<div id="Div3" runat=server>
										<tr>
										<td class="GridItemAltern" width="20%" valign="top"><%#DataBinder.Eval(Container.DataItem,"CompanyName")%></td>
										<td class="GridItemAltern" width="70%"><asp:Label id="Relation" runat="server"/>&nbsp;</td>
										<td class="GridItemAltern" width="10%" valign="top"><asp:LinkButton id="ModRelation" CommandName="ModRelation" runat="server" cssclass="normal"/>&nbsp;</td>
										</tr>
									</div>
									<div id="Div4" runat=server>
										<tr>
										<td class="GridItemAltern" width="20%" valign="top">
										<asp:Literal id="Literal2" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
										<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
										</td>
										<td class="GridItemAltern" width="70%"><asp:TextBox id="TbxRelation" runat="server" Height="80px" CssClass="BoxDesign" TextMode="MultiLine"/></td>
										<td class="GridItemAltern" width="10%" valign="top"><asp:LinkButton id="SaveRelation" CommandName="SaveRelation" runat="server" cssclass="normal"/></td>
										</tr>
									</div>
								</AlternatingItemTemplate>
								<FooterTemplate>
									</table>
								</FooterTemplate>
							</asp:Repeater>
							<center><asp:Label id="CompanyCrossCompetitorInfo" runat="server" cssclass="divautoformRequired"/></center>

						</td>
						</tr>
					</table>
			</twc:TustenaTab>
			<twc:TustenaTab id="Table_Tab4" langheader="CRMopptxt32" clientside=false runat=server>
					<table border="0" cellpadding="0" cellspacing="3" width="100%" class="normal">
						<tr>
						<td align="right"><span class=normal><twc:LocalizedLiteral text="Wortxt14" runat="server"/></span>
	 									<asp:LinkButton id="NewActivityPhoneCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityLetterCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityFaxCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityMemoCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityEmailCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityVisitCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivityGenericCoOp" runat="server" cssclass="normal" />
	 									<asp:LinkButton id="NewActivitySolutionCoOp" runat="server" cssclass="normal" />
						</td>
						</tr>
						<tr>
						<td>
							<Chrono:ActivityChronology id="AcCronoAzOp" runat="server"/>
							<center><asp:Label id="RepeaterActivityAzOpInfo" runat="server" cssclass="divautoformRequired"/></center>

						</td>
						</tr>
					</table>
			</twc:TustenaTab>
		</twc:TustenaTabber>

