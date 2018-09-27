<%@ Page Language="c#" Trace="false" Codebehind="CRM_Lead.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.CRM_Lead" ValidateRequest="false" %>
<%@ Register TagPrefix="free" TagName="FreeFields" Src="~/common/FreeFields.ascx" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="src" TagName="SearchLead" Src="~/common/SearchLead.ascx" %>
<%@ Register TagPrefix="spag" TagName="SheetPaging" Src="~/common/SheetPaging.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="quote" TagName="CustomerQuote" Src="~/erp/CustomerQuote.ascx" %>
<html>
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <script type="text/javascript" src="/js/autodate.js"></script>

    <script language="javascript">
var g_fFieldsChanged=1;

function OpenCheckmail(email,e){
	//window.open("checkmail.aspx?render=no&mail=" + email, "", "fullscreen=no,scrolling=no,toolbar=no,status=no,menubar=no,scrollbars=yes,resizable=no,directories=no,location=no,width=200,height=100,left=100,top=100")
	CreateBox("/Common/checkmail.aspx?render=no&mail=" + email,e,200,100);
}

function SelABCHeader(letter){
        location.href="CRM_Lead.aspx?m=25&si=53&list="+letter;
}

function SubmitContact(){
	var c = document.getElementById("Search");
	document.SearchC.searchcontact.value=c.value;
	document.SearchC.submit();
}

var checkTimerId;
var companySurnameTest="";

function testCompany(ele)
{
  if(!ObjExists('Ajax.Leads')){
	ele.onkeyup=null;
	return;}
  if(ele.value.length<2) return;
  clearTimeout(checkTimerId);
    checkTimerId = setTimeout('testCompanySurnameCmd(\''+RemoveCrLf(ele.value)+'\',1)',1000);
}

function testSurname(ele)
{
  if(!ObjExists('Ajax.Leads')){
	ele.onkeyup=null;
	return;}
  if(ele.value.length<2) return;
  clearTimeout(checkTimerId);
    checkTimerId = setTimeout('testCompanySurnameCmd(\''+RemoveCrLf(ele.value)+'\',2)',1000);
}


function testCompanySurnameCmd(str, type)
{
var cCompanySurname;
var oCompany = document.getElementById('CRM_Leads_CompanyName');
var oSurname = document.getElementById('CRM_Leads_Surname');
if(!(companySurnameTest.indexOf(str.toUpperCase())>-1)){
 var res = Ajax.Contacts.CheckDuplicatedLeads(RemoveCrLf(oCompany.value), RemoveCrLf(oSurname.value));
  if(res.error != null)
	return;
  cCompanySurname = res.value;
  }else
  cCompanySurname = companySurnameTest;
    if(cCompanySurname.length>0){
    companySurnameTest=cCompanySurname.toUpperCase();
    if(oCompany.value.length>0)oCompany.style.color = '#ff0000';
    if(oSurname.value.length>0)oSurname.style.color = '#ff0000';
	  }else{
    oCompany.style.color = '#000000';
    oSurname.style.color = '#000000';
    }
}
    </script>

</head>
<body id="body" runat="server">
    <form runat="server" id="Form1">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton ID="LbnNew" runat="server" CssClass="sidebtn" />
                                <asp:LinkButton ID="BtnAdvanced" runat="server" CssClass="sidebtn" />
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
                                    <asp:LinkButton ID="BtnSearch" runat="server" CssClass="save" /></div>
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
                                    <asp:LinkButton ID="BtnGroup" runat="server" CssClass="save" /></div>
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
                                <asp:Label ID="RapInfo" runat="server" CssClass="divautoformRequired" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("Ledtxt0")%>
                            </td>
                        </tr>
                    </table>
                    <twc:TustenaRepeater ID="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="Surname" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Reftxt9" ID="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" Width="25%" DataCol="Surname">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt10" ID="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" Width="25%" DataCol="CompanyName">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt11" ID="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" Width="18%" DataCol="Phone">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Reftxt12" ID="Repeatercolumnheader4" runat="Server"
                                CssClass="GridTitle" Width="17%" DataCol="MobilePhone">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Ledtxt29" ID="Repeatercolumnheader5" runat="Server"
                                CssClass="GridTitle" Width="15%" DataCol="CreatedDate">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Literal runat="server" ID="ID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    <asp:LinkButton ID="View" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name")%>'
                                        class="linked" /></td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"Phone")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Literal runat="server" ID="ID" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    <asp:LinkButton ID="View" CommandName="View" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Surname")+"&nbsp;"+DataBinder.Eval(Container.DataItem, "Name") %>'
                                        class="linked" /></td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"Phone")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"CreatedDate","{0:d}")%>
                                    &nbsp;</td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                    <br>
                    <twc:TustenaTabber ID="Tabber" Width="840" Expand="true" runat="server">
                        <twc:TustenaTabberRight runat="server">
                            <twc:GoBackBtn ID="Back" runat="server" />
                            <spag:SheetPaging ID="SheetP" runat="server" />
                        </twc:TustenaTabberRight>
                        <twc:TustenaTab ID="visContact" Header="Lead" ClientSide="true" runat="server">
                            <asp:Repeater ID="ViewForm" runat="server" Visible="false">
                                <HeaderTemplate>
                                    <table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td style="border-bottom: 1px solid #000000;" colspan="2" nowrap>
                                            <asp:LinkButton ID="LeadConvert" CommandName="LeadConvert" CssClass="Save" runat="server"
                                                Visible="false" />&nbsp; <span class="Save" style="display: inline; cursor: pointer;"
                                                    onclick="CreateBox('/CRM/PopConvertLead.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,500,250);">
                                                    <%=wrm.GetString("Ledtxt27")%>
                                                </span>
                                        </td>
                                        <td style="border-bottom: 1px solid #000000;" align="right" nowrap>
                                            <asp:Literal ID="IDRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                                Visible="false" />
                                            <asp:LinkButton ID="BackToSearch" runat="server" CssClass="Save" CommandName="BackToSearch" />&nbsp;
                                            <asp:LinkButton ID="ModCon" CommandName="ModCon" CssClass="Save" runat="server" />
                                            <asp:LinkButton ID="PrintButton" CommandName="Print" runat="server" />
                                            <span style="cursor: pointer;" onclick="CreateBox('/Common/MailLead.aspx?render=no&id=<%# DataBinder.Eval(Container.DataItem, "id") %>',event,500,250);">
                                                <img src="/images/email.gif" alt='<%=wrm.GetString("Mltxt4")%>' border="0"></span>
                                        </td>
                                    </tr>
                                    <tr id="tremail" runat="server">
                                        <td style="border-bottom: 1px solid #000000;" colspan="3" align="right" nowrap>
                                            <table cellpadding="0" cellspacing="2">
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="dropwelcome" runat="server" Style="width: 100px;">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="WelcomeEmail" CommandName="WelcomeEmail" CssClass="Save" runat="server" />&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="45%" valign="TOP">
                                            <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                <tr>
                                                    <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                        <br>
                                                        <b>
                                                            <%=wrm.GetString("Ledtxt2")%>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt15")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:Literal ID="Title" runat="server" />
                                                        <%#DataBinder.Eval(Container.DataItem,"Name")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt16")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Surname")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt17")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <asp:LinkButton ID="CompanyLink" runat="server" CommandName="CompanyLink" />
                                                        <asp:Literal ID="CompanyLabel" runat="server" />
                                                        <asp:Literal ID="CompanyIdForLink" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"CompanyID")%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt18")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"BusinessRole")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Ledtxt5")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"PHONE")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallPhone" runat="server" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:Literal ID="CompanyPhone" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Ledtxt6")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <table class="normal" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <%#DataBinder.Eval(Container.DataItem,"MobilePhone")%>
                                                                    &nbsp;
                                                                </td>
                                                                <td width="10" align="right">
                                                                    <asp:Literal ID="VoipCallMobilePhone" runat="server" />
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
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt25")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <a href="mailto:<%#DataBinder.Eval(Container.DataItem,"Email")%>">
                                                            <asp:Literal ID="leadmail" runat="server"></asp:Literal></a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Bcotxt23")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <a href='http://<%#DataBinder.Eval(Container.DataItem,"WebSite")%>' target="_blank">
                                                            <span style="text-decoration: underline;">
                                                                <%#DataBinder.Eval(Container.DataItem,"WebSite")%>
                                                            </span></a>&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt45")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"BirthDay","{0:d}")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt49")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"BirthPlace")%>
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
                                                    <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                        <br>
                                                        <b>
                                                            <%=wrm.GetString("Ledtxt3")%>
                                                        </b>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt28")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Address")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt29")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"City")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt30")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"Province")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt53")%>
                                                    </td>
                                                    <td class="VisForm">
                                                        <%#DataBinder.Eval(Container.DataItem,"State")%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td width="40%">
                                                        <%=wrm.GetString("Reftxt31")%>
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
                                            <asp:Repeater ID="RepCrossLead" runat="server">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                            <br>
                                                            <b>
                                                                <%=wrm.GetString("Ledtxt4")%>
                                                            </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt7")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt8")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"ContactName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt10")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt11")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"StatusDescription")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt12")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"RatingDescription")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt13")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"ProductInterestDescription")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt20")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"LeadCurrencyDescription")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt14")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <asp:Literal ID="LblPotentialRevenue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt15")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"EstimatedCloseDate","{0:d}")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt16")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"SourceDescription")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt17")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"Campaign")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt18")%>
                                                        </td>
                                                        <td class="VisForm">
                                                            <%#DataBinder.Eval(Container.DataItem,"IndustryName")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="40%">
                                                            <%=wrm.GetString("Ledtxt19")%>
                                                            <asp:Literal ID="litViewSalesContact" runat=server></asp:Literal>
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
                            <table id="referenceForm" width="100%" runat="server" class="normal" cellpadding="0"
                                cellspacing="0">
                                <tr>
                                    <td style="border-bottom: 1px solid #000000;" colspan="2">
                                        &nbsp;
                                    </td>
                                    <td style="border-bottom: 1px solid #000000;" align="right">
                                        <asp:TextBox ID="CRM_Leads_ID" runat="server" Text="" Visible="false" />
                                        <asp:LinkButton ID="SubmitRef" runat="server" class="Save" />
                                    </td>
                                </tr>
                                <tr>
                                    <td width="45%" valign="TOP">
                                        <table border="0" cellpadding="0" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                    <br>
                                                    <b>
                                                        <%=wrm.GetString("Ledtxt2")%>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt47")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Title" startfocus runat="server" cssclass="BoxDesign" onkeypress="FirstUp(this,event)"
                                                        MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt15")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Name" runat="server" cssclass="BoxDesign" onkeypress="FirstUp(this,event)"
                                                        MaxLength="50" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%" class="divautoformRequired">
                                                    <%=wrm.GetString("Reftxt16")%>
                                                    <asp:Label ID="RequiredFieldValidatorCognome" runat="server" Text="*" Visible="false"
                                                        Style="color: red;" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Surname" runat="server" cssclass="BoxDesignReq" onkeyup="testSurname(this)"
                                                        jumpret="CRM_Leads_CompanyName" onkeypress="FirstUp(this,event)" MaxLength="50" />
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
                                                                <asp:TextBox ID="CRM_Leads_CompanyID" runat="server" Width="0" Style="display: none;" />
                                                                <asp:TextBox ID="CRM_Leads_CompanyName" runat="server" class="BoxDesign" TextMode="multiline"
                                                                    Enabled="true" onkeyup="testCompany(this)" onkeydown="textareajump(event,'CRM_Leads_BusinessRole')"
                                                                    MaxLength="100" />
                                                            </td>
                                                            <td nowrap>
                                                                <img src="/images/lookup.gif" alt='<%=wrm.GetString("Bcotxt54")%>' border="0" onclick="CreateBox('/Common/checkexist.aspx?render=no&Company='+(document.getElementById('CRM_Leads_CompanyName')).value,event,300,250)"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt18")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_BusinessRole" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt5")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Phone" runat="server" class="BoxDesign" MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt6")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_MobilePhone" runat="server" class="BoxDesign" MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt46")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Fax" runat="server" class="BoxDesign" MaxLength="20" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt25")%>
                                                    <domval:RegexDomValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                                        ErrorMessage="*" ControlToValidate="CRM_Leads_EMail" />
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="CRM_Leads_EMail" runat="server" class="BoxDesign" MaxLength="200" />
                                                            </td>
                                                            <td>
                                                                <img src="/images/lookup.gif" border="0" onclick="OpenCheckmail(CRM_Leads_EMail.value,event);"
                                                                    style="cursor: hand;">
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
                                                    <asp:TextBox ID="CRM_Leads_WebSite" runat="server" class="BoxDesign" />
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
                                                                <asp:TextBox ID="CRM_Leads_BirthDay" onkeypress="DataCheck(this,event)" runat="server"
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
                                                    <asp:TextBox ID="CRM_Leads_BirthPlace" runat="server" jumpret="CRM_Leads_Address"
                                                        class="BoxDesign" />
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
                                                    <span style="cursor: pointer;" onclick="g_fFieldsChanged=0;CreateBox('AddCategories.aspx?render=no&Ref=1',event,180,100)">
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
                                                <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                    <br>
                                                    <b>
                                                        <%=wrm.GetString("Ledtxt3")%>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt28")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Address" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)"
                                                        MaxLength="150" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt29")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_City" runat="server" class="BoxDesign" onkeypress="FirstUp(this,event)" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt30")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_Province" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt53")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_State" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Reftxt31")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CRM_Leads_ZIPCode" runat="server" class="BoxDesign" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="10%">
                                        &nbsp;</td>
                                    <td width="45%" valign="TOP">
                                        <table border="0" cellpadding="3" cellspacing="2" width="100%" class="normal" align="center">
                                            <tr>
                                                <td style="border-bottom: 1px solid #000000;" colspan="2">
                                                    <br>
                                                    <b>
                                                        <%=wrm.GetString("Ledtxt4")%>
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt7")%>
                                                </td>
                                                <td>
                                                    <table cellpadding="1" cellspacing="0" border="0" width="100%">
                                                        <tr>
                                                            <td width="95%">
                                                                <asp:TextBox ID="CrossLead_AssociatedCompany" runat="server" Width="0" Style="display: none;" />
                                                                <asp:TextBox ID="CrossLead_CompanyName" runat="server" class="BoxDesign" TextMode="multiline"
                                                                    ReadOnly="true" />
                                                            </td>
                                                            <td nowrap>
                                                                <img src="/images/lookup.gif" border="0" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=CrossLead_CompanyName&textbox2=CrossLead_AssociatedCompany',event,500,400)"
                                                                    style="cursor: pointer;">
                                                                <img src="/i/erase.gif" border="0" onclick="CleanFields('CrossLead_CompanyName','CrossLead_AssociatedCompany');"
                                                                    style="cursor: pointer;">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt8")%>
                                                </td>
                                                <td>
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="CrossLead_AssociatedContact" runat="server" Style="display: none;" />
                                                                <asp:TextBox ID="CrossLead_ContatcName" runat="server" class="BoxDesign" EnableViewState="true"
                                                                    ReadOnly="true" />
                                                            </td>
                                                            <td width="30">
                                                                &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/popcontacts.aspx?render=no&textbox=CrossLead_ContatcName&textboxID=CrossLead_AssociatedContact',event,400,200)">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt10")%>
                                                </td>
                                                <td>
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="CrossLead_LeadOwner" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="CrossLead_OwnerName" runat="server" Width="100%" CssClass="BoxDesign"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td width="30">
                                                                &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=CrossLead_OwnerName&textbox2=CrossLead_LeadOwner',event)">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt11")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_Status" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt12")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_Rating" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt13")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_ProductInterest" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt20")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_LeadCurrency" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt14")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CrossLead_PotentialRevenue" runat="server" onkeypress="NumbersOnly(event,'.,',this)"
                                                        Width="100%" CssClass="BoxDesign"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt15")%>
                                                </td>
                                                <td>
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="CrossLead_EstimatedCloseDate" onkeypress="DataCheck(this,event)"
                                                                    runat="server" Width="100%" CssClass="BoxDesign"></asp:TextBox>
                                                            </td>
                                                            <td width="30">
                                                                &nbsp;<img id="callead" src="/i/SmallCalendar.gif" border="0" style="cursor: pointer"
                                                                    onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=CrossLead_EstimatedCloseDate&Start='+(document.getElementById('CrossLead_EstimatedCloseDate')).value,event,195,195)">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt16")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_Source" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt17")%>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="CrossLead_Campaign" runat="server" Width="100%" CssClass="BoxDesign"
                                                        Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt18")%>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="CrossLead_Industry" runat="server" Width="100%" CssClass="BoxDesign">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="40%">
                                                    <%=wrm.GetString("Ledtxt19")%>
                                                </td>
                                                <td>
                                                    <table width="100%" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td>
                                                                <asp:TextBox ID="CrossLead_SalesPerson" runat="server" Width="100%" Style="display: none"></asp:TextBox>
                                                                <asp:TextBox ID="CrossLead_SalesPersonName" runat="server" Width="100%" CssClass="BoxDesign"
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </td>
                                                            <td width="30">
                                                                &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&sales=2&textbox=CrossLead_SalesPersonName&textbox2=CrossLead_SalesPerson',event)">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="100%" colspan="3">
                                        <%=wrm.GetString("Reftxt51")%>
                                        <br>
                                        <asp:TextBox ID="CRM_Leads_Notes" runat="server" TextMode="MultiLine" Height="50"
                                            class="BoxDesign" />
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
                                    <td style="border-top: 1px solid #000000;" align="right" colspan="3">
                                        <asp:TextBox ID="CRM_Leads_Categories" runat="server" Visible="false" />
                                        <asp:LinkButton ID="Submit2" runat="server" class="Save" />
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
                                <asp:Label ID="RepeaterActivityInfo" runat="server" class="divautoformRequired" />
                            </center>
                        </twc:TustenaTab>
                        <twc:TustenaTab ID="visQuote" LangHeader="Menutxt63" ClientSide="false" runat="server">
                            <table border="0" class="normal" width="98%">
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
                    </twc:TustenaTabber>
                    <table id="advancedSearch" runat="server" border="0" cellpadding="0" cellspacing="0"
                        width="50%" class="normal" align="center">
                        <tr>
                            <td align="center" style="padding-top: 20px;">
                                <src:SearchLead ID="srcComp" runat="server"></src:SearchLead>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="padding-top: 10px;">
                                <asp:LinkButton ID="SrcBtn" runat="server" CssClass="Save" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Literal ID="SomeJS" runat="server" />
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
