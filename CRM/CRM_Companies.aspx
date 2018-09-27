<%@ Page Language="c#" Trace="false" Codebehind="CRM_Companies.aspx.cs" Inherits="Digita.Tustena.CRM_Companies"
    EnableViewState="true" ValidateRequest="false" AutoEventWireup="false" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="free" TagName="FreeFields" Src="~/common/FreeFields.ascx" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="src" TagName="SearchCompany" Src="~/common/SearchCompany.ascx" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
<%@ Register TagPrefix="quote" TagName="CustomerQuote" Src="~/erp/CustomerQuote.ascx" %>
<%@ Register TagPrefix="score" TagName="CompanyScore" Src="~/Common/CompanyScore.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Import Namespace="System.Data" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
    <script type="text/javascript" src="/js/tooltip.js"></script>
    <script type="text/javascript" src="/js/menudigita.js"></script>
    <script type="text/javascript" src="/js/dynabox.js"></script>
    <script type="text/javascript" src="/js/autodate.js"></script>


    <twc:LocalizedScript resource="ValidEmail,CRMcontxt78" runat="server" />
   <script language="javascript">
   function ClientCallbackOptions(result, context){
      var dropdown2 = document.forms[0].elements[context];
      dropdown2.onclick=new function(){};
      dropdown2.innerHTML= "";
      var rows = result.split('|');
      for (var i = 0; i < rows.length - 1; ++i){
         var option = document.createElement("OPTION");
         option.innerHTML = rows[i++];
         option.value = rows[i];
         dropdown2.appendChild(option);
      }
   }

   function ClientCallbackError(result, context){
      alert(result);
   }
</script>

    <script language="javascript">
var g_fFieldsChanged=1;


function CheckRapidMail(){
	var e = document.getElementById("RapEmail");
	var valid = validateEmail(e.value);
	if(valid)
		return valid;
	else{
		var info = document.getElementById("RapInfo");
		info.innerHTML=ValidEmail;
		return valid;
	}
}

function ValidateProduct(){
	var x = document.getElementById("Purchase_ProductID");
	if(x.value!=""){
			(document.getElementById("rvProductID")).style.display="none";
			return true;
	}else{
			(document.getElementById("rvProductID")).style.display="inline";
			return false;
	}
}

function ValidatecompanyName()
{
	var x = document.getElementById("CompanyName");
	if(x.value!="")
	{
			return true;
	}
	else
	{
			alert(CRMcontxt78);
			g_fFieldsChanged=0;
			return false;
	}
}

function IndCopy(){
	document.forms[0].Shipment_Address.value = document.forms[0].Invoice_Address.value;
	document.forms[0].Shipment_City.value = document.forms[0].Invoice_City.value;
	document.forms[0].Shipment_StateProvince.value = document.forms[0].Invoice_StateProvince.value;
	document.forms[0].Shipment_Zip.value = document.forms[0].Invoice_Zip.value;
}

function SelABCHeader(letter){
	var obj = document.forms[0].visualizationType;
    for (var i=0;i<obj.length;i++) {
        if (obj[i].checked == true)
        location.href="crm_companies.aspx?m=25&si=29&list="+letter+"&type=" +obj[i].value;
    }
}

function ChangeFree(s,o){
    var selectarr = document.getElementsByTagName('TR');
    for (var i = 0; i < selectarr.length; i++){
    	if(selectarr[i].getAttribute('ParentField')!=null){
    		if(selectarr[i].getAttribute('ParentField')==s && selectarr[i].getAttribute('ParentFieldValue')==o.value)
    			selectarr[i].style.display = '';
    		else
    			selectarr[i].style.display = 'none';
    	}
    }
}

function SubmitReferrer(){
	var c = document.getElementById("Search");
	document.SearchC.searchcontact.value=c.value;
	document.SearchC.submit();
}

function SubmitLead(){
	var c = document.getElementById("Search");
	document.SearchL.searchcontact.value=c.value;
	document.SearchL.submit();
}


function ValidateFreeProduct(){
	var FreeProduct = getElement("EstFreeProduct");
	var FreeQta = getElement("EstFreeQta");
	var FreeUp = getElement("EstFreeUp");
	var FreeVat = getElement("EstFreeVat");
	var pass=true;
	if(FreeProduct.value!=""){
			getElement("AxkFreeProduct").style.display="none";
	}else{
			getElement("AxkFreeProduct").style.display="inline";
			pass=false;
	}
	if(FreeQta.value!="" && FreeQta.value>0){
			getElement("AxkFreeQta").style.display="none";
	}else{
			getElement("AxkFreeQta").style.display="inline";
			pass=false;
	}
	if(FreeUp.value!="" && FreeUp.value>0){
			getElement("AxkFreeUp").style.display="none";
	}else{
			getElement("AxkFreeUp").style.display="inline";
			pass=false;
	}
	if(FreeVat.value!="" && FreeVat.value>-1){
			getElement("AxkFreeVat").style.display="none";
	}else{
			getElement("AxkFreeVat").style.display="inline";
			pass=false;
	}
	if(pass)
		return true;
	return false;
}

function AjaxTextBox(tbname, txt){
if(ObjExists('Ajax'))
	return Ajax.Companies.SuggestState(txt);
}

var checkTimerId;
var nameTest="";
function testName(ele)
{
  if(!ObjExists('Ajax')){
	ele.onkeyup=null;
	return;}
  if(ele.value.length<5) return;
  clearTimeout(checkTimerId);
  checkTimerId = setTimeout('testNameCmd(\''+RemoveCrLf(ele.value)+'\')',1000);
}

function testNameCmd(str)
{
var cName;
if(!(nameTest.indexOf(str.toUpperCase())>-1)){
 var res = Ajax.Companies.CheckDuplicatedCompanies(str);
  if(res.error != null){
  alert(res.error );
	return;}
  cName = res.value;
  }else
  cName = nameTest;
    if(cName.length>0){
    nameTest=cName.toUpperCase();
    document.getElementById('CompanyName').style.color = '#ff0000';
	document.getElementById('companyLookupImg').src = '/i/lookupalert.gif';
	  }else{
    document.getElementById('CompanyName').style.color = '#000000';
	document.getElementById('companyLookupImg').src = '/i/lookup.gif';
    }
}

function showfilters()
{
document.getElementById("filters").style.display='inline';
document.getElementById("filtersimg").style.display='none';
}

    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <input type="hidden" id="activeMode" runat="server">
        <asp:LinkButton ID="ViewAgenda" runat="server" />
        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0" runat="server">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton ID="NewCompany" runat="server" CssClass="sidebtn" />
                                <asp:LinkButton ID="BtnViewAdvanced" runat="server" cssclass="sidebtn" />
                                <a id="extlinkBtn" href="javascript:loadScript('/Common/jsrsextlink.aspx')" class="sideBtn sideWithsub"
                                    onmouseover="loadScript('/Common/jsrsextlink.aspx')" onmouseout="dhm()"
                                    style="display: none">
                                    <%=wrm.GetString("CRMcontxt81")%>
                                </a>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("CRMcontxt1")%>
                                </div>
                                <div class="sideFixed">
                                    <asp:TextBox ID="Search" autoclick="BtnSearch" runat="server" cssClass="boxDesign" /></div>
                                    <div id="filters" style="display:none">
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt19")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListGroups" runat="server" cssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt20")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListSector" runat="server" cssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt21")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListType" runat="server" cssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt54")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListCategory" runat="server" cssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Bcotxt49")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListOwners" runat="server" cssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Das2txt6")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="Days" runat="server" CssClass="BoxDesign" /></div>
                                </div>
                                <div class="sideSubmit">
                                    <img id="filtersimg" src="/i/plus.gif" style="float:left;cursor:pointer" alt="filters" onclick="showfilters()">
                                    <asp:LinkButton ID="BtnSearch" runat="server" cssClass="save" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Bcotxt3")%>
                                </div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt3")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapCompanyName" runat="server" CssClass="boxDesign" onkeypress="FirstUp(this,event)" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt4")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapTelephone" runat="server" CssClass="boxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt5")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapEmail" runat="server" CssClass="boxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="RapSubmit" runat="server" CssClass="Save" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="left" class="BorderBottomTitles">
                                <asp:Label ID="RapInfo" runat="server" style='color:red' />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("CRMcontxt6")%>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="normal" style="padding-bottom: 20px; display: none">
                                <input type="radio" name="visualizationType" id="visualizationType0" value="0" checked><%=wrm.GetString("Bcotxt7")%>
                                <span id="personalView" runat="server" style="display: none;">
                                    <input type="radio" name="visualizationType" id="visualizationType1" value="1"><%=wrm.GetString("Bcotxt8")%>
                                </span>
                                <input type="radio" name="visualizationType" id="visualizationType2" value="2"><%=wrm.GetString("Bcotxt9")%><asp:Literal
                                    ID="GroupDescription" runat="server"></asp:Literal>
                                <input type="radio" name="visualizationType" id="visualizationType3" value="3"><%=wrm.GetString("Bcotxt49")%>
                            </td>
                        </tr>
                    </table>
                    <twc:TustenaRepeater ID="SearchListRepeater" runat="server" CssClass="TableHlip" Width="100%"
                        SortDirection="asc" SortColumn="CompanyName" AllowPaging="true" AllowAlphabet="true" FilterCol="CompanyName"
                        AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Bcotxt10" id="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" width="40%" DataCol="CompanyName">
                            </twc:RepeaterColumnHeader>
                            <td class="GridTitle" width="40%">
                                <%=wrm.GetString("CRMopptxt44")%>
                            </td>
                            <twc:RepeaterColumnHeader Resource="Bcotxt12" id="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" width="10%" DataCol="Phone">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Bcotxt21" id="Repeatercolumnheader4" runat="Server"
                                CssClass="GridTitle" width="10%" DataCol="Fax">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterMultiDelete id="Repeatermultidelete2" runat="server" CssClass="GridTitle"
                                header="true">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem GridItemHlip">
                                    <asp:Literal ID="IDCat" runat="server" Text='<%# ((DataRowView)Container.DataItem)["id"] %>'
                                        Visible="false" />
                                    <asp:LinkButton ID="View" CommandName="view" runat="server" Text='<%# ((DataRowView)Container.DataItem)["CompanyName"] %>' />
                                </td>
                                <td class="GridItem GridItemHlip">
                                    <asp:Label ID="ShortDescription" runat="server" />&nbsp;</td>
                                <td class="GridItem GridItemHlip" nowrap>
                                    <%# ((DataRowView)Container.DataItem)["Phone"] %>
                                    &nbsp;</td>
                                <td class="GridItem GridItemHlip" nowrap>
                                    <%# ((DataRowView)Container.DataItem)["Fax"] %>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern GridItemHlip">
                                    <asp:Literal ID="IDCat" runat="server" Text='<%# ((DataRowView)Container.DataItem)["id"] %>'
                                        Visible="false" />
                                    <asp:LinkButton ID="View" CommandName="view" runat="server" Text='<%# ((DataRowView)Container.DataItem)["CompanyName"] %>' />
                                </td>
                                <td class="GridItemAltern GridItemHlip">
                                    <asp:Label ID="ShortDescription" runat="server" />&nbsp;</td>
                                <td class="GridItemAltern GridItemHlip" nowrap>
                                    <%# ((DataRowView)Container.DataItem)["Phone"] %>
                                    &nbsp;</td>
                                <td class="GridItemAltern GridItemHlip" nowrap>
                                    <%# ((DataRowView)Container.DataItem)["Fax"] %>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                    <br>
                    <twc:TustenaTabber ID="Tabber" Width="800" Expand="true" runat="server">
                        <twc:TustenaTabberRight runat="server">
                            <spag:SheetPaging ID="sheetP" runat="server"></spag:SheetPaging>
                        </twc:TustenaTabberRight>
                        <twc:TustenaTab ID="visContact" ClientSide="false" runat="server">


                            <asp:Repeater ID="ViewContact" runat="server" Visible="false">
                                <HeaderTemplate>
                                    <script>document.getElementById('extlinkBtn').style.display=''</script>
                                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="border-bottom: 1px solid #000000;" colspan="2">
                                            <b>
                                                <%=wrm.GetString("Bcotxt15")%>
                                            </b>
                                        </td>
                                        <td style="border-bottom: 1px solid #000000;" align="right">
                                            <asp:Literal ID="IDCon" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                Visible="false" />
                                            <asp:LinkButton ID="BackToSearch" CssClass="Save" runat="server" CommandName="backToSearch" />&nbsp;
                                            <asp:LinkButton ID="ModCon" CommandName="modCon" runat="server" CssClass="Save" />
                                            <img src="/i/printer.gif" alt="Print" border="0" onclick="CreateBox('/Common/PrintCompany.aspx?render=no&textbox=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,200,150)"
                                                style="cursor: pointer;">
                                            <asp:LinkButton ID="PrintButton" CommandName="print" runat="server" />
                                            <asp:Literal ID="LeadInfo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="45%" valign="TOP">
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt17")%>
                                                    </td>
                                                    <td class="visForm" id="companyTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt26")%>
                                                    </td>
                                                    <td class="visForm" id="addrTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"InvoicingAddress")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt27")%>
                                                    </td>
                                                    <td class="visForm" id="cityTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"InvoicingCity")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt28")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"InvoicingStateProvince")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt53")%>
                                                    </td>
                                                    <td class="visForm" id="stateTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"InvoicingState")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt29")%>
                                                    </td>
                                                    <td class="visForm" id="zipTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"InvoicingZipCode")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt20")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <table cellpadding="0" cellspacing="0" class="normal" width="100%">
                                                            <tr>
                                                                <td id="PhoneTD">
                                                                    <%#DataBinder.Eval(Container.DataItem,"Phone")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCall" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt21")%>
                                                    </td>
                                                    <td class="visForm" id="FaxTD">
                                                        <%#DataBinder.Eval(Container.DataItem,"FAX")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt22")%>
                                                    </td>
                                                    <td class="visForm" id="EmailTD">
                                                        <asp:Literal ID="LtrEmail" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>' />
                                                        <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>">
                                                            <%#DataBinder.Eval(Container.DataItem,"Email")%>
                                                        </a>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt51")%>
                                                    </td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="normal">
                                                            <tr>
                                                                <td width="90%" class="visForm">
                                                                    <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"MLEmail")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem,"MLEmail")%>
                                                                    </a>&nbsp;
                                                                </td>
                                                                <td width="5%">
                                                                    <img src='<%# ((bool)DataBinder.Eval(Container.DataItem,"MLFlag"))?"/i/checkon.gif":"/i/checkoff.gif"%>'>&nbsp;
                                                                </td>
                                                                <td width="5%">
                                                                    <asp:LinkButton ID="BtnMailAuth" CommandName="btnMailAuth" runat="server" class="normal" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt23")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <a href='http://<%#DataBinder.Eval(Container.DataItem,"WebSite")%>' target="_blank">
                                                            <span style="text-decoration: underline;">
                                                                <%#DataBinder.Eval(Container.DataItem,"WebSite")%>
                                                            </span></a>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt12")%>
                                                    </td>
                                                    <td>
                                                        <score:CompanyScore ID="score1" runat="server"></score:CompanyScore>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                            &nbsp;</td>
                                        <td width="45%" valign="TOP">
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt19")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"CompanyCode")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt20")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"VatID")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt8")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"CompanyType")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt9")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ContactType")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt10")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem,"Billed"))>0)?DataBinder.Eval(Container.DataItem,"Billed", "{0:#,###.00}"):""%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt11")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Employees")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("CRMcontxt12")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"EstimateDesc")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("CRMcontxt45")%>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:Repeater ID="RepCategoriesView" runat="server">
                                                            <HeaderTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td width="100%" class="normal visForm">
                                                                        <%# DataBinder.Eval(Container.DataItem, "description")%>
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
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("CRMcontxt64")%>
                                                        <asp:Literal ID="litViewOwnerContact" runat=server></asp:Literal>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("Ledtxt19")%>
                                                        <asp:Literal ID="litViewSalesContact" runat=server></asp:Literal>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem, "SalesPersonName")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("Zone")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <asp:Literal ID="lblZone" runat="server"></asp:Literal>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("InsDate")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("ModDate")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"LastModifiedDate","{0:d}")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("ModBy")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"LastModifiedBy")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" colspan="3">
                                            <%=wrm.GetString("CRMopptxt44")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" colspan="3" style="height: 50px" class="visForm" valign="top">
                                            <%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem,"Description")),false)%>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom: 1px solid #000000;" colspan="2">
                                            <br>
                                            <b>
                                                <%=wrm.GetString("Bcotxt24")%>
                                            </b>
                                        </td>
                                        <td style="border-bottom: 1px solid #000000;">
                                            <br>
                                            <b>
                                                <%=wrm.GetString("Bcotxt25")%>
                                            </b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt26")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentAddress")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt27")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentCity")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt28")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentStateProvince")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt53")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentState")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt29")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentZIPCode")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt20")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentPhone")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt21")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ShipmentFAX")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt22")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"ShipmentEmail")%>">
                                                            <%#DataBinder.Eval(Container.DataItem,"ShipmentEmail")%>
                                                        </a>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt26")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseAddress")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt27")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseCity")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt28")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseStateProvince")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt53")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseState")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt29")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseZIPCode")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt20")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehousePhone")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt21")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"WarehouseFAX")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt22")%>
                                                    </td>
                                                    <td class="visForm">
                                                        <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"WarehouseEmail")%>">
                                                            <%#DataBinder.Eval(Container.DataItem,"WarehouseEmail")%>
                                                        </a>&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <free:FreeFields ID="ViewFreeFields" runat="server" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                            <table id="ContactForm" border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center" runat="server">
                                <tr>
                                    <td colspan="3" style="border-bottom: 1px solid #000000;">
                                        <b>
                                            <%=wrm.GetString("Bcotxt15")%>
                                        </b>
                                    </td>
                                    <td style="border-bottom: 1px solid #000000; padding-bottom: 2px" align="right">
                                        <asp:LinkButton ID="SubmitRef" runat="server" cssclass="save" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="45%" valign="TOP">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td width="40%">
                                                    <span class="divautoformRequired">
                                                        <%=wrm.GetString("Bcotxt17")%>
                                                    </span>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="CompanyName" runat="server" startfocus="true" cssclass="BoxDesignReq" TextMode="multiline"
                                                                    onkeyup="testName(this)" onkeydown="textareajump(event,'invoice_Address')" onkeypress="FirstUp(this,event)" />
                                                            </td>
                                                            <td valign="top" nowrap>
                                                                <img src="/i/lookup.gif" id="companyLookupImg" width="24" height="16" alt='<%=wrm.GetString("Bcotxt54")%>'
                                                                    border="0" onclick="CreateBox('/Common/checkexist.aspx?render=no&Company='+(document.getElementById('CompanyName')).value,event,300,250)"
                                                                    style="cursor: pointer;">
                                                                <img id="pgPaste" src="/i/pg.gif" alt="incolla da PagineGialle.it" border="0" onclick="CreateBox('/Common/PGParser.aspx?render=no')"
                                                                    style="cursor: pointer;" runat="server">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt26")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Invoice_Address" runat="server" cssClass="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt27")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Invoice_City" runat="server" cssClass="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt28")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Invoice_StateProvince" runat="server" cssClass="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt53")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Invoice_State" runat="server" cssClass="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt29")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Invoice_Zip" runat="server" cssClass="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt20")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Phone" runat="server" cssClass="BoxDesign" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Phone&phone=' + getElement('Phone').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt21")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Fax" runat="server" cssClass="BoxDesign" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Fax&phone=' + getElement('Fax').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt22")%>
                                                    <domval:RegexDomValidator ID="regularExpressionValidator1" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                                        ErrorMessage="*" ControlToValidate="Email" />
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Email" runat="server" cssclass="BoxDesign" />
                                                            </td>
                                                            <td>
                                                                <img src="/images/lookup.gif" border="0" onclick="CreateBox('/Common/checkmail.aspx?render=no&mail=' + getElement('Email').value,event,200,100);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt51")%>
                                                    <domval:RegexDomValidator ID="regularExpressionValidator2" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                                        ErrorMessage="*" ControlToValidate="EmailML" />
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="5%">
                                                                <asp:CheckBox ID="MlCheck" runat="server" Checked="true" />
                                                            </td>
                                                            <td width="95%">
                                                                <asp:TextBox ID="EmailML" runat="server" cssclass="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt23")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="WebSite" runat="server" cssclass="BoxDesign" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="10%">
                                        &nbsp;
                                     </td>
                                    <td width="45%" valign="top">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt19")%>
                                                </td>
                                                <td width="60%">
                                                    <asp:TextBox ID="CompanyCode" runat="server" cssclass="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                        <td width="40%">
                                            <%=wrm.GetString("Reftxt20")%>
                                        </td>
                                    <td width="60%">
                                        <asp:TextBox ID="VatId" runat="server" cssclass="BoxDesign" MaxLength="20" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <%=wrm.GetString("CRMcontxt8")%>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="Sector" runat="server" cssclass="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <%=wrm.GetString("CRMcontxt9")%>
                                    </td>
                                    <td width="60%">
                                        <asp:DropDownList ID="ContactType" runat="server" cssclass="BoxDesign" onchange="ChangeFree('ContactTypeID',this);" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <%=wrm.GetString("CRMcontxt10")%>
                                        <domval:CompareDomValidator ID="valTurnOver" runat="server" ControlToValidate="TurnOver"
                                            ValueToCompare="0" Type="Double" Operator="GreaterThanEqual" ErrorMessage=""
                                            Display="dynamic">*
                                        </domval:CompareDomValidator>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TurnOver" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <%=wrm.GetString("CRMcontxt11")%>
                                    </td>
                                    <td width="60%">
                                        <asp:TextBox ID="Employees" runat="server" cssclass="BoxDesign" onkeypress="NumbersOnly(event,'',this)" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%">
                                        <%=wrm.GetString("CRMcontxt12")%>
                                    </td>
                                    <td width="60%">
                                        <asp:DropDownList ID="Evaluation" runat="server" cssclass="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%" valign="top">
                                        <%=wrm.GetString("CRMcontxt64")%>
                                    </td>
                                    <td width="60%">
                                        <table width="100%" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="OwnerId" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                    <asp:TextBox ID="OwnerName" runat="server" Width="100%" CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="30">
                                                    &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=OwnerName&textbox2=OwnerId',event)">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%" valign="top">
                                        <%=wrm.GetString("Ledtxt19")%>
                                    </td>
                                    <td width="60%">
                                        <table width="100%" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="SalesPersonID" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                    <asp:TextBox ID="SalesPerson" runat="server" Width="100%" CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
                                                </td>
                                                <td width="30">
                                                    &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&sales=2&textbox=SalesPerson&textbox2=SalesPersonID',event)">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%" valign="top">
                                        <%=wrm.GetString("CRMcontxt45")%>
                                    </td>
                                    <td valign="top">
                                        <asp:Repeater ID="RepCategories" runat="server">
                                            <HeaderTemplate>
                                                <div class="ListCategory">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="5%">
                                                        <asp:CheckBox ID="Check" runat="server" />
                                                        <asp:Literal ID="IDCat" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>'
                                                            Visible="false" />
                                                    </td>
                                                    <td width="90%" class="normal">
                                                        <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                                    </td>
                                                    <td width="5%">
                                                        <asp:LinkButton ID="DeleteCat" CommandName="deleteCat" runat="server" />&nbsp;
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table> </div>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <span style="cursor: pointer; text-decoration: underline;" onclick="CreateBox('AddCategories.aspx?render=no',event,200,120)">
                                            <%=wrm.GetString("CRMcontxt46")%>
                                        </span>
                                        <asp:LinkButton ID="RefreshRepCategories" runat="server" Style="display: none;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="40%" valign="top">
                                        <%=wrm.GetString("Zone")%>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropZones" runat="server" CssClass="BoxDesign">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                                    </td>
                                </tr>
            <tr>
                <td width="100%" colspan="3">
                    <%=wrm.GetString("CRMopptxt44")%>
                    <br>
                    <asp:TextBox ID="Description" runat="server" TextMode="MultiLine" Height="50" class="BoxDesign" />
                </td>
            </tr>
            <tr>
                <td style="border-bottom: 1px solid #000000;" colspan="2">
                    <br>
                    <b>
                        <%=wrm.GetString("Bcotxt24")%>
                    </b>
                </td>
                <td style="border-bottom: 1px solid #000000;">
                    <br>
                    <b>
                        <%=wrm.GetString("Bcotxt25")%>
                    </b>&nbsp;&nbsp;(<span onclick="IndCopy()" class="linked"><%=wrm.GetString("Bcotxt39")%></span>)
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt26")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_Address" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt27")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_City" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt28")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_StateProvince" runat="server" class="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt53")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_State" runat="server" class="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt29")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_Zip" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt20")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_Phone" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt21")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_Fax" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt22")%>
                                <domval:RegexDomValidator ID="regularExpressionValidator3" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                    ErrorMessage="*" ControlToValidate="Shipment_Email" />
                            </td>
                            <td>
                                <asp:TextBox ID="Shipment_Email" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt26")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_Address" runat="server" cssclass="BoxDesign" onkeypress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt27")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_City" runat="server" cssclass="BoxDesign" onkeypress="FirstUp(this,event)" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt28")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_StateProvince" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt53")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_State" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt29")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_Zip" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt20")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_Phone" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt21")%>
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_Fax" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%">
                                <%=wrm.GetString("Bcotxt22")%>
                                <domval:RegexDomValidator ID="regularExpressionValidator4" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                    ErrorMessage="*" ControlToValidate="Warehouse_Email" />
                            </td>
                            <td>
                                <asp:TextBox ID="Warehouse_Email" runat="server" cssclass="BoxDesign" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <Free:FreeFields ID="EditFreeFields" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="border-top: 1px solid #000000;" align="right" colspan="3">
                    <span id="groupauth">
                        <GControl:GroupControl runat="server" ID="groups" />
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="border-top: 1px solid #000000; padding-top: 2px" height="18"
                    align="right">
                    <asp:Literal ID="ContactId" runat="server" Visible="false" />
                    <asp:Literal ID="CategoriesRep" runat="server" Visible="false" />
                    <asp:LinkButton ID="CancelCon" runat="server" CssClass="Save" />
                    &nbsp;
                    <asp:LinkButton ID="SubmitCon" runat="server" CssClass="Save" />
                </td>
            </tr>
        </table>
        </twc:TustenaTab>
        <twc:TustenaTab ID="visReferrer" LangHeader="Bcotxt30" ClientSide="true" runat="server">
            <table border="0" width="100%" class="normal">
                <tr>
                    <td style="border-bottom: 1px solid #000000;" align="right" valign="bottom">
                        <asp:LinkButton ID="NewContact" runat="server" CssClass="Save" />
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="ContactReferrer" runat="server">
                <HeaderTemplate>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                        <tr>
                            <td class="GridTitle">
                                <%=wrm.GetString("Bcotxt32")%>
                            </td>
                            <td class="GridTitle">
                                <%=wrm.GetString("Bcotxt33")%>
                            </td>
                            <td class="GridTitle">
                                <%=wrm.GetString("Bcotxt34")%>
                            </td>
                            <td class="GridTitle">
                                <%=wrm.GetString("Bcotxt35")%>
                            </td>
                            <td class="GridTitle">
                                &nbsp;</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="GridItem">
                            <asp:LinkButton ID="OpenContact" CommandName="openContact" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>' />
                            <asp:Literal ID="IdRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                Visible="false" />
                        </td>
                        <td class="GridItem">
                            <asp:Literal ID="callPhone_1" runat="server"></asp:Literal>&nbsp;</td>
                        <td class="GridItem">
                            <asp:Literal ID="callMobilePhone_1" runat="server"></asp:Literal>&nbsp;</td>
                        <td class="GridItem">
                            <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"EMAIL")%>">
                                <%#DataBinder.Eval(Container.DataItem,"EMAIL")%>
                            </a>&nbsp;</td>
                        <td class="GridItem Lcit" align="CENTER">
                            <asp:LinkButton ID="Delete" runat="server" CommandName="delete" />
                            <asp:LinkButton ID="Activate" runat="server" CommandName="activate" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="GridItemAltern">
                            <asp:LinkButton ID="OpenContact" CommandName="openContact" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>' />
                            <asp:Literal ID="IdRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                Visible="false" />
                        </td>
                        <td class="GridItemAltern">
                            <asp:Literal ID="callPhone_1" runat="server"></asp:Literal>&nbsp;</td>
                        <td class="GridItemAltern">
                            <asp:Literal ID="callMobilePhone_1" runat="server"></asp:Literal>&nbsp;</td>
                        <td class="GridItemAltern">
                            <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"EMAIL")%>">
                                <%#DataBinder.Eval(Container.DataItem,"EMAIL")%>
                            </a>&nbsp;</td>
                        <td class="GridItemAltern Lcit" align="CENTER">
                            <asp:LinkButton ID="Delete" runat="server" CommandName="delete" />
                            <asp:LinkButton ID="Activate" runat="server" CommandName="activate" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <center>
                <asp:Label ID="ContactReferrerInfo" runat="server" style='color:red' />
            </center>
        </twc:TustenaTab>
        <twc:TustenaTab ID="visActivity" ClientSide="false" runat="server">
            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                <tr>
                    <td style="border-bottom: 1px solid #000000;" align="left" valign="bottom">
                        <asp:LinkButton ID="AllActivity" runat="server" cssclass="normal" />
                        &nbsp;
                    </td>
                    <td style="border-bottom: 1px solid #000000;" align="right" valign="bottom">
                        <%=wrm.GetString("Wortxt14")%>
                        <asp:LinkButton ID="NewActivityPhone" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityLetter" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityFax" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityMemo" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityEmail" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityVisit" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivityGeneric" runat="server" cssclass="normal" />
                        <asp:LinkButton ID="NewActivitySolution" runat="server" cssclass="normal" />
                    </td>
                </tr>
            </table>
            <Chrono:ActivityChronology ID="AcCrono" runat="server" />
            <center>
                <asp:Label ID="RepeaterActivityInfo" runat="server" style='color:red' />
            </center>
        </twc:TustenaTab>
        <twc:TustenaTab ID="visOpportunity" ClientSide="false" runat="server">
            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                <tr>
                    <td style="border-bottom: 1px solid #000000;" align="right" valign="bottom">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="RepeaterOpportunity" runat="server">
                <HeaderTemplate>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                        <tr>
                            <td class="GridTitle">
                                <%=wrm.GetString("CRMopptxt10")%>
                            </td>
                            <td class="GridTitle">
                                <%=wrm.GetString("CRMopptxt11")%>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="GridItem">
                            <asp:LinkButton ID="OpenOpportunity" runat="server" CommandName="openOP" Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>' />
                            <asp:Literal ID="IdOp" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                        </td>
                        <td class="GridItem">
                            <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                            &nbsp;</td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="GridItemAltern">
                            <asp:LinkButton ID="OpenOpportunity" runat="server" CommandName="openOP" cssclass="normal"
                                Text='<%# DataBinder.Eval(Container.DataItem, "Title")%>' />
                            <asp:Literal ID="IdOp" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                        </td>
                        <td class="GridItemAltern">
                            <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                            &nbsp;</td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <center>
                <asp:Label ID="RepeaterOpportunityInfo" runat="server" style='color:red' />
            </center>
        </twc:TustenaTab>
        <twc:TustenaTab ID="visDocuments" ClientSide="false" runat="server">
            <table border="0" cellspacing="0" width="100%" align="center" class="normal">
                <tr>
                    <td align="right" style="border-bottom: 1px solid #000000;">
                        <asp:LinkButton ID="NewDoc" runat="server" cssclass="save" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="FileRepInfo" runat="server" style='color:red' />
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="FileRep" runat="server">
                <HeaderTemplate>
                    <table border="0" class="normal" width="100%" align="center">
                        <tr>
                            <td class="GridTitle" width="14%">
                                <%=wrm.GetString("Dsttxt2")%>
                            </td>
                            <td class="GridTitle" width="1%">
                                R</td>
                            <td class="GridTitle" width="15%">
                                <%=wrm.GetString("Dsttxt3")%>
                            </td>
                            <td class="GridTitle" width="30%">
                                <%=wrm.GetString("Dsttxt10")%>
                            </td>
                            <td class="GridTitle" width="9%" style="text-align: right;">
                                <%=wrm.GetString("Dsttxt4")%>
                            </td>
                            <td class="GridTitle" width="1%">
                                &nbsp;</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="GridItem" nowrap>
                            <asp:Image ID="FileImg" ImageUrl="" runat="server"></asp:Image>
                            &nbsp;
                            <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                Visible="false" />
                            <asp:LinkButton ID="Down" CommandName="down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                            &nbsp;
                        </td>
                        <td class="GridItem" nowrap>
                            <asp:LinkButton ID="ReviewNumber" CommandName="reviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                runat="server" />
                            <asp:Literal ID="LbReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                runat="server" />
                            &nbsp;</td>
                        <td class="GridItem" nowrap>
                            <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                            &nbsp;</td>
                        <td class="GridItem" nowrap>
                            <%#DataBinder.Eval(Container.DataItem,"owner")%>
                            &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                        <td class="GridItem" nowrap align="right">
                            <asp:Literal ID="FileSize" runat="server" /></td>
                        <td class="GridItem" nowrap>
                            <asp:LinkButton ID="Modify" CommandName="modify" runat="server" />
                            <asp:LinkButton ID="Revision" CommandName="revision" runat="server" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="GridItemAltern" nowrap>
                            <asp:Image ID="FileImg" ImageUrl="" runat="server"></asp:Image>
                            &nbsp;
                            <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                Visible="false" />
                            <asp:LinkButton ID="Down" CommandName="down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                            &nbsp;
                        </td>
                        <td class="GridItemAltern" nowrap>
                            <asp:LinkButton ID="ReviewNumber" CommandName="reviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                runat="server" />
                            <asp:Literal ID="LbReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                runat="server" />
                            &nbsp;</td>
                        <td class="GridItemAltern" nowrap>
                            <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                            &nbsp;</td>
                        <td class="GridItemAltern" nowrap>
                            <%#DataBinder.Eval(Container.DataItem,"owner")%>
                            &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                        <td class="GridItemAltern" nowrap align="right">
                            <asp:Literal ID="FileSize" runat="server" /></td>
                        <td class="GridItemAltern" nowrap>
                            <asp:LinkButton ID="Modify" CommandName="modify" runat="server" />
                            <asp:LinkButton ID="Revision" CommandName="revision" runat="server" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

        </twc:TustenaTab>
        <twc:TustenaTab ID="visEstimate" ClientSide="false" runat="server">
            <table border="0" class="normal" width="100%">
                <tr>
                    <td align="left" style="border-bottom: 1px solid #000000;">
                        <span class="class"><b>
                            <%=wrm.GetString("CRMcontxt76")%>
                        </b></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <quote:CustomerQuote ID="CustomerQuote1" runat="server" />
                    </td>
                </tr>
            </table>
            <br>
            <table border="0" class="normal" width="100%">
                <tr>
                    <td align="left" style="border-bottom: 1px solid #000000;">
                        <span class="class"><b>
                            <%=wrm.GetString("Ordtxt1")%>
                        </b></span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <quote:CustomerQuote ID="CustomerQuote2" runat="server" />
                    </td>
                </tr>
            </table>
        </twc:TustenaTab>
        <twc:TustenaTab ID="visProducts" ClientSide="false" runat="server">
            <table border="0" cellspacing="0" width="100%" align="center" class="normal">
                <tr>
                    <td align="right" style="border-bottom: 1px solid #000000;">
                        <twc:GoBackBtn ID="GoBackProd" runat="server" Visible="false" />
                        &nbsp;
                        <asp:LinkButton ID="InsertNewProduct" runat="server" cssclass="save" />
                        <asp:TextBox ID="CRM_CompetitorProducts_CompetitorID" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Label ID="RepeaterProductsInfo" runat="server" style='color:red' />
                    </td>
                </tr>
            </table>
            <table id="NewProduct" runat="server" border="0" cellpadding="3" cellspacing="0"
                width="95%" class="normal" align="center">
                <tr>
                    <td width="50%" valign="top">
                        <div align="center">
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal">
                                <tr>
                                    <td width="50%" nowrap>
                                        <%=wrm.GetString("CRMComptxt4")%>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="CRM_CompetitorProducts_ProductName" runat="server" Height="100%"
                                            class="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" nowrap>
                                        <%=wrm.GetString("CRMComptxt10")%>
                                        <domval:CompareDomValidator ID="valCRM_CompetitorProducts_Package" runat="server"
                                            ControlToValidate="CRM_CompetitorProducts_Package" ValueToCompare="0" Type="Integer"
                                            Operator="GreaterThanEqual" ErrorMessage="*" Display="dynamic">*
                                        </domval:CompareDomValidator>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="CRM_CompetitorProducts_Package" runat="server" Height="100%" class="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" nowrap>
                                        <%=wrm.GetString("CRMComptxt11")%>
                                        <domval:CompareDomValidator ID="valCRM_CompetitorProducts_UnitPrice" runat="server"
                                            ControlToValidate="CRM_CompetitorProducts_UnitPrice" ValueToCompare="0" Type="Double"
                                            Operator="GreaterThanEqual" ErrorMessage="*" Display="dynamic">*
                                        </domval:CompareDomValidator>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="CRM_CompetitorProducts_UnitPrice" runat="server" Height="100%" class="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="50%" nowrap>
                                        <%=wrm.GetString("CRMComptxt5")%>
                                        <domval:CompareDomValidator ID="valCRM_CompetitorProducts_Price" runat="server" ControlToValidate="CRM_CompetitorProducts_Price"
                                            ValueToCompare="0" Type="Double" Operator="GreaterThanEqual" ErrorMessage="*"
                                            Display="dynamic">*
                                        </domval:CompareDomValidator>
                                    </td>
                                    <td width="50%">
                                        <asp:TextBox ID="CRM_CompetitorProducts_Price" runat="server" Height="100%" class="BoxDesign" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td width="50%" valign="top">
                        <div align="center">
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal">
                                <tr>
                                    <td width="100%">
                                        <%=wrm.GetString("CRMcontxt60")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <asp:TextBox ID="CRM_CompetitorProducts_Description" runat="server" TextMode="MultiLine"
                                            Height="100" cssclass="BoxDesign" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="right">
                        <asp:TextBox ID="CRM_CompetitorProducts_ID" runat="server" Visible="false" />
                        <asp:LinkButton ID="ResetNewProduct" runat="server" cssclass="save" />
                        <asp:LinkButton ID="SaveNewProduct" runat="server" cssclass="save" />
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="RepeaterProducts" runat="server">
                <HeaderTemplate>
                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                        <tr>
                            <td class="GridTitle" width="40%" nowrap>
                                <%=wrm.GetString("CRMComptxt4")%>
                            </td>
                            <td class="GridTitle" width="10%" nowrap>
                                <%=wrm.GetString("CRMComptxt10")%>
                            </td>
                            <td class="GridTitle" width="25%" nowrap>
                                <%=wrm.GetString("CRMComptxt11")%>
                            </td>
                            <td class="GridTitle" width="25%" nowrap>
                                <%=wrm.GetString("CRMComptxt5")%>
                            </td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="GridItem">
                            <asp:LinkButton ID="OpenProduct" runat="server" CommandName="openProd" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                            <asp:Literal ID="IdProduct" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                        </td>
                        <td class="GridItem">
                            <%# DataBinder.Eval(Container.DataItem, "Package")%>
                            &nbsp;</td>
                        <td class="GridItem">
                            <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:###00.00}")%>
                            &nbsp;</td>
                        <td class="GridItem">
                            <%# DataBinder.Eval(Container.DataItem, "Price", "{0:###00.00}")%>
                            &nbsp;</td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="GridItemAltern">
                            <asp:LinkButton ID="OpenProduct" runat="server" CommandName="openProd" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                            <asp:Literal ID="IdProduct" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' />
                        </td>
                        <td class="GridItemAltern">
                            <%# DataBinder.Eval(Container.DataItem, "Package")%>
                            &nbsp;</td>
                        <td class="GridItemAltern">
                            <%# DataBinder.Eval(Container.DataItem, "UnitPrice", "{0:###00.00}")%>
                            &nbsp;</td>
                        <td class="GridItemAltern">
                            <%# DataBinder.Eval(Container.DataItem, "Price", "{0:###00.00}")%>
                            &nbsp;</td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            </twc:TustenaTab>
            </twc:TustenaTabber>

            <table id="AdvancedSearch" runat="server" border="0" cellpadding="0" cellspacing="0"
                class="normal" width="500" style="padding-left: 10px">
                <tr>
                    <td>
                        <src:SearchCompany ID="srccomp" runat="server"></src:SearchCompany>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:LinkButton ID="SrcBtn" runat="server" Text="search" CssClass="Save" />
                    </td>
                </tr>
            </table>
        </td> </tr> </table>
        <asp:Literal ID="SomeJS" runat="server" />
    </form>

    <script>
var q = window.location.search;
var obj = document.forms[0].visualizationType;
if (q.length > 1 && q.indexOf("type=")!=-1)
obj[q.charAt(q.indexOf("type=")+5)].checked=true;
    </script>

    <form name="SearchC" method="post" action="base_contacts.aspx?m=25&dgb=1&si=31" style="display: none;">
        <input type="hidden" name="searchcontact">
    </form>
    <form name="SearchL" method="post" action="CRM_Lead.aspx?m=25&dgb=1&si=53" style="display: none;">
        <input type="hidden" name="searchcontact">
    </form>
</body>
</html>
