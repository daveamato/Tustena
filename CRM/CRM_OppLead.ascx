<%@ Control Codebehind="CRM_OppLead.ascx.cs" Language="c#" AutoEventWireup="true"
    Inherits="Digita.Tustena.CRM_OppLead" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<twc:jscontrolid ID="jsc" runat="server" />

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

<twc:TustenaTabber ID="L_Tabber" Width="840" runat="server">
    <twc:TustenaTabberRight runat="server" ID="Tustenatabberright1">
        <twc:GoBackBtn ID="btnGoBack" runat="server" />
        <spag:SheetPaging ID="SheetP" runat="server"></spag:SheetPaging>
    </twc:TustenaTabberRight>
    <twc:TustenaTab ID="L_Table_Tab1" Header="Lead" ClientSide="true" runat="server">
        <table border="0" cellpadding="0" cellspacing="3" width="99%">
            <tr>
                <td width="33%" valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" width="99%">
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt72" runat="server"></twc:LocalizedLiteral>
                                    <domval:RequiredDomValidator ID="CompanyNewCompanyIDVal" runat="server" EnableClientScript="False"
                                        ControlToValidate="CompanyNewCompanyID" ErrorMessage="*"></domval:RequiredDomValidator>
                                </div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="CompanyNewCompanyID" runat="server" Style="display: none" />
                                            <asp:TextBox ID="CompanyNewCompany" runat="server" cssclass="BoxDesign" ReadOnly="true" />
                                        </td>
                                        <td width="20" nowrap>
                                            <img src="/i/user.gif" border="0" style="cursor: pointer" onclick="FrameCreateBox('/common/PopLead.aspx?render=no&textbox='+jsControlId+'CompanyNewCompany&textboxID='+jsControlId+'CompanyNewCompanyID',event,400,300);">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt75" runat="server" />
                                    <domval:RequiredDomValidator ID="CompanyNewExpectedRevenueVal" runat="server" EnableClientScript="False"
                                        ControlToValidate="CompanyNewExpectedRevenue" ErrorMessage="*"></domval:RequiredDomValidator>
                                </div>
                                <asp:TextBox ID="CompanyNewExpectedRevenue" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt14" runat="server" />
                                <span id="AmountValidator" style="color: red; display: none;">*</span> </div>
                                <asp:TextBox ID="CompanyNewAmountClosed" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt13" runat="server" /></div>
                                <asp:TextBox ID="CompanyNewAmountRevenuePercent" runat="server" ReadOnly="true" cssclass="BoxDesign" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="33%" valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" width="99%">
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt3" runat="server" />
                                    <domval:RequiredDomValidator ID="CompanyNewStateListVal" runat="server" EnableClientScript="False"
                                        InitialValue="0" ControlToValidate="CompanyNewStateList" ErrorMessage="*"></domval:RequiredDomValidator>
                                </div>
                                <asp:DropDownList ID="CompanyNewStateList" old="true" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt4" runat="server" /></div>
                                <asp:DropDownList ID="CompanyNewPhaseList" old="true" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt5" runat="server" /></div>
                                <asp:DropDownList ID="CompanyNewProbList" old="true" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="Mottxt6" runat="server" />
                                    <span id="LostReasonsValidator" style="color: red; display: none;">*</span>
                                </div>
                                <asp:DropDownList ID="CompanyLostReasons" old="true" runat="server" cssclass="BoxDesign" />
                                <asp:TextBox ID="NewLostReason" runat="server" cssclass="BoxDesign" Style="display: none;" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td width="34%" valign="top">
                    <table border="0" cellpadding="0" cellspacing="3" width="99%">
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt76" runat="server" /></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="CompanyNewSalesPersonID" runat="server" Style="display: none" />
                                            <asp:TextBox ID="CompanyNewSalesPerson" runat="server" cssclass="BoxDesign" ReadOnly="true" />
                                        </td>
                                        <td width="20" nowrap>
                                            <img src="/i/user.gif" border="0" style="cursor: pointer" onclick="FrameCreateBox('/common/PopAccount.aspx?render=no&textbox=CompanyNewSalesPerson&textbox2=CompanyNewSalesPersonID',event);">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt77" runat="server" /></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="CompanyNewStartDate" runat="server" Width="100%" CssClass="BoxDesign"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td width="30">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewStartDate&Start='+(document.getElementById('CompanyNewStartDate')).value,event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt78" runat="server" /></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="CompanyNewEstimatedCloseDate" runat="server" Width="100%" CssClass="BoxDesign"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td width="30">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewEstimatedCloseDate&Start='+(document.getElementById('CompanyNewEstimatedCloseDate')).value,event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <div>
                                    <twc:LocalizedLiteral Text="CRMopptxt79" runat="server" />
                                    <span id="CloseDateValidator" style="color: red; display: none;">*</span>
                                </div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="CompanyNewCloseDate" runat="server" Width="100%" CssClass="BoxDesign"
                                                ReadOnly="true"></asp:TextBox>
                                        </td>
                                        <td width="30">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="FrameCreateBox('/Common/PopUpDate.aspx?Textbox=CompanyNewCloseDate&Start='+(document.getElementById('CompanyNewCloseDate')).value,event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table id="InsertProductTable" runat="server" style="display: inline" border="0"
                        cellspacing="0" width="100%" align="center" class="normal">
                        <tr>
                            <td class="GridTitle">
                                <twc:LocalizedLiteral Text="CRMopptxt81" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" width="100%" align="center" class="normal" style="border: 1px solid #000000;">
                                    <tr>
                                        <td width="30%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt65" runat="server" />
                                                <span id="rvProductID" class="normal" style="display: none; color: red;">*</span>
                                            </div>
                                            <table width="100%" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td valign="top">
                                                        <asp:TextBox ID="EstProductID" runat="server" cssclass="BoxDesign" Style="display: none" />
                                                        <asp:TextBox ID="EstProduct" runat="server" cssclass="BoxDesign" ReadOnly="true" />
                                                    </td>
                                                    <td width="30" valign="top">
                                                        &nbsp;<img src="/i/lookup.gif" border="0" style="cursor: pointer;" onclick="CreateBox('/Common/PopCatalog.aspx?render=no&ptx='+jsControlId+'EstProduct&pid='+jsControlId+'EstProductID&um='+jsControlId+'EstUm&qta='+jsControlId+'EstQta&up='+jsControlId+'EstUp&vat='+jsControlId+'EstVat&pl='+jsControlId+'EstPl&pf='+jsControlId+'EstPf&ch='+getchangecurrency(),event,400,300)">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt66" runat="server" /></div>
                                            <asp:TextBox ID="EstUm" runat="server" ReadOnly="true" cssclass="BoxDesign" />
                                        </td>
                                        <td width="10%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt67" runat="server" /></div>
                                            <asp:TextBox ID="EstQta" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                                        </td>
                                        <td width="20%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt68" runat="server" /></div>
                                            <asp:TextBox ID="EstUp" runat="server" ReadOnly="true" cssclass="BoxDesign" />
                                        </td>
                                        <td width="10%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt69" runat="server" /></div>
                                            <asp:TextBox ID="EstVat" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                                        </td>
                                        <td width="20%">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt70" runat="server" /></div>
                                            <asp:TextBox ID="EstPl" runat="server" ReadOnly="true" cssclass="BoxDesign" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="5">
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt72" runat="server" /></div>
                                            <asp:TextBox ID="EstDescription2" runat="server" cssclass="BoxDesign" />
                                        </td>
                                        <td>
                                            <div>
                                                <twc:LocalizedLiteral Text="CRMcontxt71" runat="server" /></div>
                                            <asp:TextBox ID="EstPf" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:LinkButton ID="BtnCalcPrice" runat="server" CssClass="normal" />
                                        </td>
                                        <td colspan="3" align="right">
                                            <asp:LinkButton ID="BtnAddProduct" runat="server" CssClass="normal" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <br>
                    <asp:Repeater ID="RepeaterEstProduct" runat="server">
                        <HeaderTemplate>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
                                <tr>
                                    <td class="GridTitle" width="30%">
                                        <twc:LocalizedLiteral Text="CRMcontxt65" runat="server" /></td>
                                    <td class="GridTitle" width="10%">
                                        <twc:LocalizedLiteral Text="CRMcontxt66" runat="server" /></td>
                                    <td class="GridTitle" width="10%">
                                        <twc:LocalizedLiteral Text="CRMcontxt67" runat="server" /></td>
                                    <td class="GridTitle" width="20%">
                                        <twc:LocalizedLiteral Text="CRMcontxt68" runat="server" /></td>
                                    <td class="GridTitle" width="10%">
                                        <twc:LocalizedLiteral Text="CRMcontxt69" runat="server" /></td>
                                    <td class="GridTitle" width="19%">
                                        <twc:LocalizedLiteral Text="CRMcontxt71" runat="server" /></td>
                                    <td class="GridTitle" width="1%">
                                        &nbsp;</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem" width="30%">
                                    <asp:Label ID="ShortDescription" runat="server" /></td>
                                <td class="GridItem" width="10%">
                                    <asp:Label ID="UM" runat="server" /></td>
                                <td class="GridItem" width="10%">
                                    <asp:Label ID="Qta" runat="server" /></td>
                                <td class="GridItem" width="20%">
                                    <asp:Label ID="UnitPrice" runat="server" /></td>
                                <td class="GridItem" width="10%">
                                    <asp:Label ID="Vat" runat="server" /></td>
                                <td class="GridItem" width="19%">
                                    <asp:Label ID="FinalPrice" runat="server" /></td>
                                <td class="GridItem" width="1%">
                                    <asp:LinkButton ID="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
                                    <asp:Literal ID="ObjectID" runat="server" Visible="false" />
                                    &nbsp;
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern" width="30%">
                                    <asp:Label ID="ShortDescription" runat="server" /></td>
                                <td class="GridItemAltern" width="10%">
                                    <asp:Label ID="UM" runat="server" /></td>
                                <td class="GridItemAltern" width="10%">
                                    <asp:Label ID="Qta" runat="server" /></td>
                                <td class="GridItemAltern" width="20%">
                                    <asp:Label ID="UnitPrice" runat="server" /></td>
                                <td class="GridItemAltern" width="10%">
                                    <asp:Label ID="Vat" runat="server" /></td>
                                <td class="GridItemAltern" width="19%">
                                    <asp:Label ID="FinalPrice" runat="server" /></td>
                                <td class="GridItemAltern" width="1%">
                                    <asp:LinkButton ID="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
                                    <asp:Literal ID="ObjectID" runat="server" Visible="false" />
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
                    <twc:LocalizedLiteral Text="CRMopptxt27" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="3" height="100">
                    <asp:TextBox ID="CompanyNewNote" runat="server" TextMode="MultiLine" Height="100px"
                        CssClass="BoxDesign" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    &nbsp;
                    <asp:Label ID="CompanyNewInfo" runat="server" cssclass="divautoformRequired" />
                </td>
                <td align="right">
                    <asp:TextBox ID="CompanyNewID" runat="server" Visible="false" />
                    <asp:LinkButton ID="CompanyNewSubmit" runat="server" CssClass="Save" />
                </td>
            </tr>
        </table>
    </twc:TustenaTab>
    <twc:TustenaTab ID="L_Table_Tab6" LangHeader="CRMopptxt57" ClientSide="false" runat="server">
        <table border="0" cellpadding="0" cellspacing="3" width="100%">
            <tr>
                <td>
                    <asp:Repeater ID="ViewForm" runat="server" Visible="false">
                        <HeaderTemplate>
                            <table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td width="45%" valign="TOP">
                                    <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                        <tr>
                                            <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                <br>
                                                <b>
                                                    <twc:LocalizedLiteral Text="Ledtxt2" runat="server" /></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt15" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <asp:Literal ID="Title" runat="server" />
                                                <%#DataBinder.Eval(Container.DataItem,"Name")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt16" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"Surname")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt17" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <asp:LinkButton ID="CompanyLink" runat="server" CommandName="CompanyLink" />
                                                <asp:Literal ID="CompanyLabel" runat="server" />
                                                <asp:Literal ID="CompanyIdForLink" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CompanyID")%>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt18" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"BusinessRole")%>
                                            </td>
                                        </tr>
                                        <tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Ledtxt5" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"PHONE")%>
                                                <asp:Literal ID="CompanyPhone" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Ledtxt6" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt46" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"Fax")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt25" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>">
                                                    <%#DataBinder.Eval(Container.DataItem,"Email")%>
                                                </a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt45" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"BirthDay","{0:d}")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt49" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"BirthPlace")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%" valign="top">
                                                <twc:LocalizedLiteral Text="CRMcontxt45" runat="server" />
                                            </td>
                                            <td valign="top">
                                                <asp:Repeater ID="RepCategoriesView" runat="server">
                                                    <HeaderTemplate>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td width="100%" class="normal VisForm">
                                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                <br>
                                                <b>
                                                    <twc:LocalizedLiteral Text="Ledtxt3" runat="server" /></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt28" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"Address")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt29" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"City")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt30" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"Province")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt53" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"State")%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="40%">
                                                <twc:LocalizedLiteral Text="Reftxt31" runat="server" />
                                            </td>
                                            <td class="VisForm">
                                                <%#DataBinder.Eval(Container.DataItem,"ZIPCode")%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="45%" valign="TOP">
                                    <asp:Repeater ID="RepCrossLead" runat="server" DataSource='<%# getLeadInfo(Convert.ToInt32(DataBinder.Eval(Container.DataItem, "id")) ) %>'>
                                        <HeaderTemplate>
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                    <br>
                                                    <b>
                                                        <twc:LocalizedLiteral Text="Ledtxt4" runat="server" /></b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt7" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt8" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"ContactName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt10" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt11" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"StatusDescription")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt12" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"RatingDescription")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt13" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"ProductInterestDescription")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt20" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"LeadCurrencyDescription")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt14" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"PotentialRevenue","{0:c}")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt15" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"EstimatedCloseDate","{0:d}")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt16" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"SourceDescription")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt17" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"Campaign")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt18" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"IndustryName")%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <twc:LocalizedLiteral Text="Ledtxt19" runat="server" />
                                                </td>
                                                <td class="VisForm">
                                                    <%#DataBinder.Eval(Container.DataItem,"SalesPersonName")%>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" colspan="3">
                                    <twc:LocalizedLiteral Text="Reftxt51" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td width="100%" colspan="3" style="height: 50px" class="VisForm" valign="top">
                                    <%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem,"Notes")),false)%>
                                    &nbsp;
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
    <twc:TustenaTab ID="L_Table_Tab3" LangHeader="CRMopptxt41" ClientSide="false" runat="server">
        <table border="0" cellpadding="0" cellspacing="3" width="100%">
            <tr>
                <td>
                    <asp:Repeater ID="CompanyCrossCompetitor" runat="server">
                        <HeaderTemplate>
                            <table class="tblstruct normal">
                                <tr>
                                    <td class="GridTitle" width="20%">
                                        <twc:LocalizedLiteral Text="CRMopptxt41" runat="server" /></td>
                                    <td class="GridTitle" width="70%">
                                        <twc:LocalizedLiteral Text="CRMopptxt51" runat="server" /></td>
                                    <td class="GridTitle" width="10%">
                                        &nbsp;</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="ViewCompetitor" runat="server">
                                <tr>
                                    <td class="GridItem" width="20%" valign="top">
                                        <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    </td>
                                    <td class="GridItem" width="70%">
                                        <asp:Label ID="Relation" runat="server" />&nbsp;</td>
                                    <td class="GridItem" width="10%" valign="top">
                                        <asp:LinkButton ID="ModRelation" CommandName="ModRelation" runat="server" CssClass="normal" /></td>
                                </tr>
                            </div>
                            <div id="ModifyCompetitor" runat="server">
                                <tr>
                                    <td class="GridItem" width="20%" valign="top">
                                        <asp:Literal ID="IDcross" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                            Visible="false" />
                                        <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    </td>
                                    <td class="GridItem" width="70%">
                                        <asp:TextBox ID="TbxRelation" runat="server" Height="80px" CssClass="BoxDesign" TextMode="MultiLine" /></td>
                                    <td class="GridItem" width="10%" valign="top">
                                        <asp:LinkButton ID="SaveRelation" CommandName="SaveRelation" runat="server" CssClass="normal" /></td>
                                </tr>
                            </div>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <div id="ViewCompetitor" runat="server">
                                <tr>
                                    <td class="GridItemAltern" width="20%" valign="top">
                                        <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    </td>
                                    <td class="GridItemAltern" width="70%">
                                        <asp:Label ID="Relation" runat="server" />&nbsp;</td>
                                    <td class="GridItemAltern" width="10%" valign="top">
                                        <asp:LinkButton ID="ModRelation" CommandName="ModRelation" runat="server" CssClass="normal" />&nbsp;</td>
                                </tr>
                            </div>
                            <div id="ModifyCompetitor" runat="server">
                                <tr>
                                    <td class="GridItemAltern" width="20%" valign="top">
                                        <asp:Literal ID="IDcross" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' />
                                        <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    </td>
                                    <td class="GridItemAltern" width="70%">
                                        <asp:TextBox ID="TbxRelation" runat="server" Height="80px" CssClass="BoxDesign" TextMode="MultiLine" /></td>
                                    <td class="GridItemAltern" width="10%" valign="top">
                                        <asp:LinkButton ID="SaveRelation" CommandName="SaveRelation" runat="server" CssClass="normal" /></td>
                                </tr>
                            </div>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </twc:TustenaTab>
    <twc:TustenaTab ID="L_Table_Tab4" LangHeader="CRMopptxt32" ClientSide="false" runat="server">
        <table border="0" cellpadding="0" cellspacing="3" width="100%">
            <tr>
                <td align="right">
                    <span class="normal">
                        <twc:LocalizedLiteral Text="Wortxt14" runat="server" /></span>
                    <asp:LinkButton ID="NewActivityPhoneCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityLetterCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityFaxCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityMemoCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityEmailCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityVisitCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivityGenericCoOp" runat="server" cssclass="normal" />
                    <asp:LinkButton ID="NewActivitySolutionCoOp" runat="server" cssclass="normal" />
                </td>
            </tr>
            <tr>
                <td>
                    <Chrono:ActivityChronology ID="AcCronoAzOp" runat="server" />
                    <center>
                        <asp:Label ID="RepeaterActivityAzOpInfo" runat="server" cssclass="divautoformRequired" /></center>
                </td>
            </tr>
        </table>
    </twc:TustenaTab>
</twc:TustenaTabber>
