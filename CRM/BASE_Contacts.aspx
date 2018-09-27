<%@ Page Language="c#" Trace="false" Codebehind="BASE_Contacts.aspx.cs" Inherits="Digita.Tustena.Base_Contacts"
    AutoEventWireup="false" %>
<%@ Register TagPrefix="free" TagName="FreeFields" Src="~/common/FreeFields.ascx" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
<%@ Register TagPrefix="quote" TagName="CustomerQuote" Src="~/erp/CustomerQuote.ascx" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
    <script type="text/javascript" src="/js/dynabox.js"></script>
    <script type="text/javascript" src="/js/autodate.js"></script>
    <script type="text/javascript" src="/js/clone.js"></script>

    <script language="javascript">
function skyper(node){
    node.onload=null;
    if(node.getAttribute('alt')!=null && node.getAttribute('alt').length>0){
    node.onclick=function(){window.location="skype:"+node.getAttribute('alt')+"?call"};
    node.src = "http://mystatus.skype.com/smallicon/"+node.getAttribute('alt');
    }else
    node.src = "/i/noskype.gif";

}

var g_fFieldsChanged=1;

function OpenCheckmail(email,e){
	CreateBox("/Common/checkmail.aspx?render=no&mail=" + email,e,200,100);
}

function CheckPersonal(){
	if (document.getElementById("Personal").checked)document.getElementById("Global").checked=false;
}

function CheckGlobal(){
	if (document.getElementById("Global").checked)document.getElementById("personal").checked=false;
	HideGroups();
}

function HideGroups(){
	if (document.getElementById("Personal").checked || document.getElementById("Global").checked)
	document.getElementById('groupauth').style.display='none'
	else
	document.getElementById('groupauth').style.display='';
}

function SelABCHeader(letter){
	var obj = document.forms[0].visualizationType;
    for (var i=0;i<obj.length;i++) {
        if (obj[i].checked == true)
        location.href="base_contacts.aspx?m=25&si=31&list="+letter+"&type=" +obj[i].value;
    }
}

function SubmitContact(){
	var c = document.getElementById("Search");
	document.SearchC.searchcontact.value=c.value;
	document.SearchC.submit();
}

var checkTimerId;
var nameSurnameTest="";

function testName(ele)
{
  if(!ObjExists('Ajax')){
	ele.onkeyup=null;
	return;}
  if(ele.value.length<2) return;
  clearTimeout(checkTimerId);
    checkTimerId = setTimeout('testNameSurnameCmd(\''+RemoveCrLf(ele.value)+'\',1)',1000);
}

function testSurname(ele)
{
  if(!ObjExists('Ajax.Contacts')){
	ele.onkeyup=null;
	return;}
  if(ele.value.length<2) return;
  clearTimeout(checkTimerId);
    checkTimerId = setTimeout('testNameSurnameCmd(\''+RemoveCrLf(ele.value)+'\',2)',1000);
}

function testNameSurnameCmd(str, type)
{
var cNameSurname;
var oName = document.getElementById('Referring_Name');
var oSurname = document.getElementById('Referring_Surname');
if(!(nameSurnameTest.indexOf(str.toUpperCase())>-1)){
 var res = Ajax.Contacts.CheckDuplicatedContacts(RemoveCrLf(oName.value), RemoveCrLf(oSurname.value));
  if(res.error != null)
	return;
  cNameSurname = res.value;
  }else
  cNameSurname = nameSurnameTest;
    if(cNameSurname.length>0){
    nameSurnameTest=cNameSurname.toUpperCase();
    if(oName.value.length>0)oName.style.color = '#ff0000';
    if(oSurname.value.length>0)oSurname.style.color = '#ff0000';
	  }else{
    oName.style.color = '#000000';
    oSurname.style.color = '#000000';
    }
}
    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton ID="BtnNew" runat="server" CssClass="sidebtn" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Reftxt4")%>
                                </div>
                                <div class="sideFixed">
                                    <asp:TextBox ID="Search" autoclick="BtnSearch" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnSearch" runat="server" class="save" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Reftxt52")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListGroups" runat="server" class="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt54")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="ListCategory" runat="server" class="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnGroup" runat="server" class="save" /></div>
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
                                    <%=wrm.GetString("Bcotxt17")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapRagSoc" runat="server" CssClass="BoxDesign" onkeypress="FirstUp(this,event)" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Bcotxt20")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapPhone" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Bcotxt22")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="RapEmail" autoclick="RapSubmit" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="RapSubmit" runat="server" CssClass="save" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="RapInfo" runat="server" style='color:red' />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <asp:Literal ID="LblTitle" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left" class="normal" style="padding-bottom: 20px; display: none">
                                <input type="radio" name="visualizationType" id="visualizationType0" value="0" checked><%=wrm.GetString("Reftxt6")%>
                                <span style="display: none">
                                    <input type="radio" name="visualizationType" id="visualizationType1" value="1"><%=wrm.GetString("Reftxt7")%></span>
                                <input type="radio" name="visualizationType" id="visualizationType2" value="2"><%=wrm.GetString("Reftxt8")%><asp:Literal
                                    ID="GroupDescription" runat="server"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <twc:TustenaRepeater ID="SearchListRepeater" runat="server" SortColumn="Surname" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="Surname" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Reftxt9" ID="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" Width="25%" DataCol="Surname">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt10" ID="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" Width="25%" DataCol="CompanyName2">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt11" ID="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" Width="12%" DataCol="Phone_1">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt12" ID="Repeatercolumnheader4" runat="Server"
                                CssClass="GridTitle" Width="13%" DataCol="MobilePhone_1">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt13" ID="Repeatercolumnheader5" runat="Server"
                                CssClass="GridTitle" Width="25%" DataCol="NameOwner">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Label runat="server" ID="ID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' /><asp:LinkButton
                                        ID="View" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>'
                                        CssClass="linked" /></td>
                                <td class="GridItem">
                                    <asp:Label runat="server" ID="cnpID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CompanyID")%>' /><asp:LinkButton
                                        ID="ViewCpn" CommandName="ViewCpn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName2")%>'
                                        CssClass="linked" />&nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"Phone_1")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"NameOwner")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Label runat="server" ID="ID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' /><asp:LinkButton
                                        ID="View" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name") %>'
                                        class="linked" /></td>
                                <td class="GridItemAltern">
                                    <asp:Label runat="server" ID="cnpID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CompanyID")%>' /><asp:LinkButton
                                        ID="ViewCpn" CommandName="ViewCpn" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName2")%>'
                                        CssClass="linked" />&nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"Phone_1")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"NameOwner")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                    <br>
                    <twc:TustenaTabber ID="Tabber" Width="840" expand="true" runat="server">
                        <twc:TustenaTabberRight runat="server">
                            <twc:GoBackBtn ID="Back" runat="server" />
                            <spag:SheetPaging ID="SheetP" runat="server" />
                        </twc:TustenaTabberRight>
                        <twc:TustenaTab ID="visContact" ClientSide="true" runat="server">
                            <asp:Repeater ID="ViewForm" runat="server" Visible="false">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="border-bottom: 1px solid #000000;" colspan="2">
                                            <b>
                                                <%=wrm.GetString("Reftxt14")%>
                                            </b>
                                        </td>
                                        <td style="border-bottom: 1px solid #000000;" align="right">
                                            <asp:Literal ID="IdRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                Visible="false" />
                                            <asp:LinkButton ID="CompanyContacts" runat="server" CommandName="CompanyContacts"
                                                CssClass="Save" />&nbsp;
                                            <asp:LinkButton ID="BackToSearch" runat="server" CommandName="BackToSearch" CssClass="Save" />&nbsp;
                                            <asp:LinkButton ID="ModCon" CommandName="ModCon" runat="server" CssClass="Save" />&nbsp;
                                            <asp:LinkButton ID="PrintButton" CommandName="Print" runat="server" />
                                            <asp:Literal ID="LeadInfo" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="45%" valign="TOP">
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt15")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:Literal ID="Title" runat="server" />
                                                        <%#DataBinder.Eval(Container.DataItem,"Name")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt16")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Surname")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt17")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:LinkButton ID="CompanyLink" runat="server" CommandName="CompanyLink" />
                                                        <asp:Literal ID="CompanyIdForLink" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CompanyID")%>' />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("Reftxt58")%>
                                                    </td>
                                                    <td class="VisForm" valign="top">
                                                        <asp:Repeater ID="RepOtherCompanies" runat="server" OnItemCommand="RepOtherCompanies_ItemCommand">
                                                            <HeaderTemplate>
                                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td width="100%" class="normal">
                                                                        <asp:LinkButton ID="OtherCompanyLink" runat="server" CommandName="OtherCompanyLink"
                                                                            Text='<%# DataBinder.Eval(Container.DataItem, "CompanyName")%>'></asp:LinkButton>
                                                                        <asp:Literal ID="OtherCompanyIdForLink" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
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
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt48").ToUpper()%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:Literal ID="CompanyPhone" runat="server" />
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt18")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"BusinessRole")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt19")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"TaxIdentificationNumber")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt20")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"VatID")%>
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
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("Zone").ToUpper()%>
                                                    </td>
                                                    <td class="visForm">
                                                        <asp:Literal ID="lblZone" runat="server"></asp:Literal>
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
                                            </table>
                                        </td>
                                        <td width="10%">
                                            &nbsp;
                                        </td>
                                        <td width="45%" valign="TOP">
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt11")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"CODE")%>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt21")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"PHONE_1")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallPhone_1" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt22")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"PHONE_2")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallPhone_2" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt23")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone_1")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallMobilePhone_1" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt24")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone_2")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallMobilePhone_2" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt46")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Fax")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        Skype
                                                    </td>
                                                    <td class="VisForm">
                                                        <img src="/i/noskype.gif" style="cursor: pointer;" width="14" height="14" onload="skyper(this)"
                                                            alt="<%#DataBinder.Eval(Container.DataItem,"Skype")%>">&nbsp;<%#DataBinder.Eval(Container.DataItem,"Skype")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <twc:MultiMail email='<%#DataBinder.Eval(Container.DataItem,"Email")%>' title='<%=wrm.GetString("Reftxt25")%>'
                                                    runat="server">
                                                    <tr>
                                                        <td width="40%">
                                                            <twc:LocalizedLiteral Text="Reftxt25" runat="server" />
                                                            {0}
                                                        </td>
                                                        <td class="VisForm">
                                                            <asp:Literal ID="LtrEmail" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"Email")%>' />
                                                            <a href="mailto:{1}">{1}</a>&nbsp;
                                                        </td>
                                                    </tr>
                                                </twc:MultiMail>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt51")%>
                                                    </td>
                                                    <td>
                                                        <table cellpadding="0" cellspacing="0" class="normal">
                                                            <tr>
                                                                <td width="90%" class="VisForm">
                                                                    <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"MLEmail")%>">
                                                                        <%#DataBinder.Eval(Container.DataItem,"MLEmail")%>
                                                                    </a>
                                                                </td>
                                                                <td width="5%">
                                                                    <%#(NoLength(Convert.ToString(DataBinder.Eval(Container.DataItem,"MLEmail"))))?((bool)DataBinder.Eval(Container.DataItem,"MLFlag"))?"<img src=/i/checkon.gif>":"<img src=/i/checkoff.gif>":""%>
                                                                </td>
                                                                <td width="5%">
                                                                    <asp:LinkButton ID="BtnMailAuth" CommandName="BtnMailAuth" runat="server" class="normal" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt45")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:Literal ID="BirthDay" runat="server"></asp:Literal>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt49")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"BirthPlace")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt59")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:Literal ID="Sexlbl" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("InsDate")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("ModDate")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"LastModifiedDate","{0:d}")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%" valign="top">
                                                        <%=wrm.GetString("ModBy")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"LastModifiedBy")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" colspan="3">
                                            <%=wrm.GetString("Reftxt51")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="100%" colspan="3" style="height: 50px" class="VisForm" valign="top">
                                            <%# FixCarriage(Convert.ToString(DataBinder.Eval(Container.DataItem,"Notes")),false)%>
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="border-bottom: 1px solid #000000;">
                                            <br>
                                            <b>
                                                <%=wrm.GetString("Reftxt26")%>
                                            </b>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td style="border-bottom: 1px solid #000000;">
                                            <br>
                                            <b>
                                                <%=wrm.GetString("Reftxt27")%>
                                            </b>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt28")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Address_1")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt29")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"City_1")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt30")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Province_1")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt53")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"State_1")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt31")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ZIPCode_1")%>
                                                        &nbsp;
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
                                                        <%=wrm.GetString("Reftxt32")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Address_2")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt33")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"City_2")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt34")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Province_2")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt53")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"State_2")%>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt35")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"ZipCode_2")%>
                                                        &nbsp;
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
                            <table id="ReferenceForm" width="100%" runat="server" class="normal" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td style="border-bottom: 1px solid #000000;" colspan="2">
                                        <b>
                                            <%=wrm.GetString("Reftxt36")%>
                                        </b>
                                    </td>
                                    <td style="border-bottom: 1px solid #000000; padding-bottom: 2px" align="right">
                                        <asp:TextBox ID="Referring_CompanyID" runat="server" Width="0" Style="display: none;" />
                                        <asp:TextBox ID="Referring_ID" runat="server" Text="-1" Visible="false" />
                                        <twc:GoBackBtn ID="BackCo" runat="server" />
                                        &nbsp;
                                        <asp:LinkButton ID="SubmitRef" runat="server" class="save" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="45%" valign="TOP">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt47")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Title" runat="server" startfocus class="BoxDesign" onkeypress="FirstUp(this,event)"
                                                        MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt15")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Name" runat="server" class="BoxDesign" onkeyup="testName(this)"
                                                        onkeypress="FirstUp(this,event)" MaxLength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%" class="divautoformRequired">
                                                    <%=wrm.GetString("Reftxt16")%>
                                                    <asp:Label ID="RequiredFieldValidatorCognome" runat="server" Text="*" Visible="false"
                                                        Style="color: red;" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Surname" runat="server" class="BoxDesignReq" onkeyup="testSurname(this)"
                                                        onkeypress="FirstUp(this,event)" MaxLength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt17")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_CompanyTX" runat="server" class="BoxDesign" TextMode="multiline"
                                                                    ReadOnly="true" />
                                                            </td>
                                                            <td nowrap>
                                                                <img src="/images/lookup.gif" border="0" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=Referring_CompanyTX&textbox2=Referring_CompanyID',event,500,400)"
                                                                    style="cursor: pointer;">
                                                                <img src="/i/erase.gif" border="0" onclick="CleanFields('Referring_CompanyTX','Referring_CompanyID');"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%" valign="top">
                                                    <%=wrm.GetString("Reftxt58")%>
                                                    <img src="/i/plus.gif" alt='<%=wrm.GetString("Reftxt59")%>' onclick="cloneObj('othercompanies',idc,'othercompaniescontainer');CleanFields('newothercompanies_'+idc,'newothercompaniesID_'+idc++);">
                                                </td>
                                                <td id="othercompaniescontainer" valign="top">
                                                    <asp:Literal ID="othercompaniestebles" runat="server"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt18")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_BusinessRole" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt19")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_TaxIdentificationNumber" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt20")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_VatID" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt38")%>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="Personal" runat="server" onclick="CheckPersonal()" />
                                                </td>
                                            </tr>
                                            <tr style="display: none">
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt39")%>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="Global" runat="server" onclick="CheckGlobal()" />
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
                                                                    <asp:LinkButton ID="DeleteCat" CommandName="DeleteCat" runat="server" />&nbsp;
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
                                                    <span style="cursor: pointer; text-decoration: underline" onclick="g_fFieldsChanged=0;CreateBox('AddCategories.aspx?render=no&Ref=1',event,180,100)">
                                                        <%=wrm.GetString("CRMcontxt46")%>
                                                    </span>
                                                    <asp:LinkButton ID="RefreshRepCategories" runat="server" Text="Aggiorna Cat" Style="display: none;" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%" valign="top">
                                                    <%=wrm.GetString("Zone").ToUpper()%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="dropZones" runat="server" CssClass="BoxDesign">
                                                    </asp:DropDownList>
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
                                        </table>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="45%" valign="TOP">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt11")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_CODE" runat="server" cssclass="BoxDesign" MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt21")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_Phone_1" runat="server" cssclass="BoxDesign" MaxLength="20" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Referring_Phone_1&phone=' + getElement('Referring_Phone_1').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt22")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_Phone_2" runat="server" class="BoxDesign" MaxLength="20" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Referring_Phone_2&phone=' + getElement('Referring_Phone_2').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt23")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_MobilePhone_1" runat="server" class="BoxDesign" MaxLength="20" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Referring_MobilePhone_1&phone=' + getElement('Referring_MobilePhone_1').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt24")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_MobilePhone_2" runat="server" class="BoxDesign" MaxLength="20" />
                                                            </td>
                                                            <td>
                                                                <img src="/i/phone.gif" border="0" onclick="CreateBox('/Common/PopPhone.aspx?render=no&ret=Referring_MobilePhone_2&phone=' + getElement('Referring_MobilePhone_2').value,event,340,60);"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt46")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Fax" runat="server" class="BoxDesign" MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    Skype
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Skype" runat="server" class="BoxDesign" MaxLength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt25")%>
                                                    <domval:RegexDomValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="(^\s*)((([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*);?)*$"
                                                        ErrorMessage="*" ControlToValidate="Referring_EMail" />
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_EMail" TextMode="multiline" runat="server" class="BoxDesign" />
                                                            </td>
                                                            <td>
                                                                <img src="/images/lookup.gif" border="0" onclick="OpenCheckmail(Referring_EMail.value,event);"
                                                                    style="cursor: hand;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt51")%>
                                                    <domval:RegexDomValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}$"
                                                        ErrorMessage="*" ControlToValidate="Referring_MLEmail" />
                                                </td>
                                                <td>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td width="5%">
                                                                <asp:CheckBox ID="Referring_MLFlag" runat="server" Checked="true" />
                                                            </td>
                                                            <td width="95%">
                                                                <asp:TextBox ID="Referring_MLEmail" runat="server" CssClass="BoxDesign" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt45")%>
                                                </td>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td width="50%">
                                                                <asp:TextBox ID="Referring_BirthDay" onkeypress="DataCheck(this,event)" runat="server"
                                                                    class="BoxDesign" EnableViewState="true" MaxLength="10" />
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="DateFormat" runat="server" class="normal" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt49")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_BirthPlace" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Bcotxt59")%>
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="Referring_Sex" runat="server" class="normal" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="1">M</asp:ListItem>
                                                        <asp:ListItem Value="0">F</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" colspan="3">
                                        <%=wrm.GetString("Reftxt51")%>
                                        <br>
                                        <asp:TextBox ID="Referring_Notes" runat="server" TextMode="MultiLine" Height="50"
                                            class="BoxDesign" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-bottom: 1px solid #000000;">
                                        <br>
                                        <b>
                                            <%=wrm.GetString("Reftxt26")%>
                                        </b>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #000000;">
                                        <br>
                                        <b>
                                            <%=wrm.GetString("Reftxt27")%>
                                        </b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt28")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Address_1" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt29")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_City_1" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt30")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Province_1" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt53")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_State_1" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt31")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_ZIPCode_1" runat="server" class="BoxDesign" />
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
                                                    <%=wrm.GetString("Reftxt32")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Address_2" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt33")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_City_2" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt34")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_Province_2" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt53")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_State_2" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt35")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Referring_ZIPCode_2" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <free:FreeFields ID="EditFreeFields" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: 1px solid #000000;" align="right" colspan="3">
                                        <span id="groupauth">
                                            <GControl:GroupControl runat="server" ID="Groups" />
                                        </span>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="border-top: 1px solid #000000; padding-top: 2px" align="right" height="18"
                                        colspan="3">
                                        <asp:TextBox ID="Referring_Categories" runat="server" Visible="false" />
                                        <asp:LinkButton ID="Submit2" runat="server" CssClass="Save" />
                                    </td>
                                </tr>
                            </table>
                        </twc:TustenaTab>
                        <twc:TustenaTab ID="visActivity" LangHeader="Bcotxt45" ClientSide="true" runat="server">
                            <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                                <tr>
                                    <td style="border-bottom: 1px solid #000000;" align="right" valign="bottom">
                                        <%=wrm.GetString("Wortxt14")%>
                                        <asp:LinkButton ID="NewActivityPhone" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityLetter" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityFax" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityMemo" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityEmail" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityVisit" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivityGeneric" runat="server" class="normal" />
                                        <asp:LinkButton ID="NewActivitySolution" runat="server" class="normal" />
                                    </td>
                                </tr>
                            </table>
                            <Chrono:ActivityChronology ID="AcCrono" runat="server" />
                            <center>
                                <asp:Label ID="RepeaterActivityInfo" runat="server" style='color:red' />
                            </center>
                        </twc:TustenaTab>
                        <twc:TustenaTab ID="visQuote" LangHeader="Menutxt63" ClientSide="false" runat="server">
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
                                        <quote:CustomerQuote ID="Customerquote2" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </twc:TustenaTab>
                        <twc:TustenaTab ID="visDocuments" LangHeader="CRMrubtxt3" ClientSide="true" runat="server">
                            <table border="0" width="100%" class="normal">
                                <tr>
                                    <td style="border-bottom: 1px solid #000000;" align="right" valign="bottom">
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
                                    <table class="normal">
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
                                            <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                            &nbsp;
                                        </td>
                                        <td class="GridItem" nowrap>
                                            <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
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
                                            <%#DataBinder.Eval(Container.DataItem,"size")%>
                                            &nbsp;Kb</td>
                                        <td class="GridItem" nowrap>
                                            <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                            <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
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
                                            <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                            &nbsp;
                                        </td>
                                        <td class="GridItemAltern" nowrap>
                                            <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
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
                                            <%#DataBinder.Eval(Container.DataItem,"size")%>
                                            &nbsp;Kb</td>
                                        <td class="GridItemAltern" nowrap>
                                            <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                            <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </twc:TustenaTab>
                    </twc:TustenaTabber>
                </td>
            </tr>
        </table>
    </form>
    <script>
var q = window.location.search;
var obj = document.forms[0].visualizationType
if (q.length > 1 && q.indexOf("type=")!=-1)
obj[q.charAt(q.indexOf("type=")+5)].checked=true
    </script>

    <form name="SearchC" method="post" action="crm_companies.aspx?m=25&si=29" style="display: none;">
        <input type="hidden" name="searchcontact">
    </form>
</body>
</html>
