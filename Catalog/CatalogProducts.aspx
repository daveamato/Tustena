<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>

<%@ Page Language="c#" Trace="false" Codebehind="CatalogProducts.aspx.cs" AutoEventWireup="false"
    Inherits="Digita.Tustena.Catalog.CatalogProducts" %>

<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css">

    <script>
	function copyData(id, category)
	{
		HideFloatDiv('floatCategory');
		(document.getElementById("TxtIdCategory")).value = id;
		(document.getElementById("TxtTextCategory")).value = category;
	}
	function copyDataS(id, category)
	{
		(document.getElementById("FindCatID")).value = id;
		clickElement(document.getElementById("FindProduct"));
	}
	function ShowProgressBar()
	{
		var progress = document.getElementById('divProgressBar');
		progress.style.display='';
	}

    function EnableDisableList(o)
    {
        var price =document.getElementById(GetBrotherId(o, 'ListUnitPrice'));
        var cost =document.getElementById(GetBrotherId(o, 'ListCost'));
        var vat =document.getElementById(GetBrotherId(o, 'ListlistVat'));


        if(o.checked)
        {
            price.value='';
            price.readOnly=true;
            price.style.backgroundColor='#eeeeee';
            cost.value='';
            cost.readOnly=true;
            cost.style.backgroundColor='#eeeeee';
            vat.disabled=true;
        }
        else{
            price.readOnly=false;
            price.style.backgroundColor='';
            cost.readOnly=false;
            cost.style.backgroundColor='';
            vat.disabled=false;
        }
    }
    </script>

</head>
<body id="body" runat="server">
    <form method="post" runat="server">
        <table width="100%" border="0" cellspacing="0">
            <tbody>
                <tr>
                    <td width="140" class="SideBorderLinked" valign="top">
                        <table width="98%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral Text="Options" runat="server" /></div>
                                    <asp:LinkButton ID="AddProduct" runat="server" CssClass="sidebtn" /></td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral Text="CRMcontxt1" runat="server" /></div>
                                    <div class="sideFixed">
                                        <asp:TextBox ID="Search" autoclick="FindProduct" runat="server" class="BoxDesign" /></div>
                                    <div class="sideSubmit">
                                        <asp:LinkButton ID="FindProduct" runat="server" CssClass="save" /></div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <twc:LocalizedLiteral Text="Captxt3" runat="server" /></div>
                                    <div class="sideFixed">
                                        <ie:TreeView ID="tvCategoryTreeSearch" runat="server" HoverStyle="background:#F2F2F2;color:black"
                                            DefaultStyle="font-family:Verdana;font-size:10px" SelectedStyle="background:Gold;color:black"
                                            SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:TreeView></div>
                                    <asp:TextBox ID="FindCatID" runat="server" Style="display: none" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" height="100%" class="pageStyle">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left" class="pageTitle" valign="top">
                                    <twc:LocalizedLiteral Text="Captxt1" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <table id="GraphResult" runat="server" cellspacing="3" cellpadding="0" border="0"
                            align="center">
                            <tr>
                                <td width="100%" colspan="2" align="center" class="normal">
                                    <b>
                                        <twc:LocalizedLiteral Text="Cahtxt2" runat="server" />
                                    </b>&nbsp;<span class="normal" style="font-size: 12px"><twc:LocalizedLiteral Text="Cahtxt5"
                                        runat="server" /></span>
                                </td>
                            </tr>
                            <tr>
                                <td width="50%" valign="top">
                                    <asp:Label ID="Result" runat="server" CssClass="normal" />
                                </td>
                                <td width="50%" valign="top">
                                    <asp:Literal ID="Legend" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <twc:TustenaTabber ID="Tabber" Width="800" runat="server" EditTab="visControl">
                            <twc:TustenaTab ID="visControl" runat="server" LangHeader="CRMcontxt65" ClientSide="true">
                                <table id="ProductTable" runat="server" class="tblstruct normal">
                                    <tr>
                                        <td width="600" valign="top">
                                            <div id="floatCategory" style="display: none; position: absolute; border: 1px solid silver;
                                                margin: 10px;" onmouseout="HideFloatDiv('floatCategory')">
                                                <ie:TreeView ID="tvCategoryTree" runat="server" HoverStyle="background:#F2F2F2;color:black"
                                                    DefaultStyle="font-family:Verdana;font-size:10px" SelectedStyle="background:#FFA500;color:black"
                                                    SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:TreeView></div>
                                            <table width="100%" border="0" cellpadding="0" cellspacing="3" class="normal">
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt3" runat="server" />
                                                            <domval:RequiredDomValidator ID="ValtxtIdCategory" runat="server" EnableClientScript="False"
                                                                ControlToValidate="TxtIdCategory" ErrorMessage="*"></domval:RequiredDomValidator>
                                                        </div>
                                                        <asp:TextBox ID="TxtIdCategory" runat="server" Style="display: none" />
                                                        <table cellspacing="0" cellpadding="0" width="100%">
                                                            <tr>
                                                                <td>
                                                                    <img src="/i/tree.gif" style="margin-right: 4px; cursor: pointer" onclick="ShowFloatDiv(event,'floatCategory')">
                                                                </td>
                                                                <td width="100%">
                                                                    <asp:TextBox ID="TxtTextCategory" runat="server" CssClass="BoxDesignReq" ReadOnly="true"
                                                                        onclick="ShowFloatDiv(event,'floatCategory')"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt4" runat="server" /></div>
                                                        <asp:TextBox ID="TxtId" runat="server" CssClass="BoxDesign" Visible="false" />
                                                        <asp:TextBox ID="TxtCode" runat="server" CssClass="BoxDesign" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt18" runat="server" /></div>
                                                        <asp:RadioButtonList ID="RadioActive" runat="server" RepeatDirection="Horizontal"
                                                            class="normal" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt27" runat="server" /></div>
                                                        <asp:RadioButtonList ID="RadioPublish" runat="server" RepeatDirection="Horizontal"
                                                            class="normal" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt5" runat="server" />
                                                            <domval:RequiredDomValidator ID="ValtxtShortDescription" runat="server" EnableClientScript="False"
                                                                ControlToValidate="TxtShortDescription" ErrorMessage="*"></domval:RequiredDomValidator>
                                                        </div>
                                                        <asp:TextBox ID="TxtShortDescription" runat="server" CssClass="BoxDesignReq" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt6" runat="server" /></div>
                                                        <asp:TextBox ID="TxtLongDescription" runat="server" TextMode="MultiLine" Height="100px"
                                                            CssClass="BoxDesign" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt7" runat="server" /></div>
                                                        <asp:TextBox ID="TxtUnit" runat="server" CssClass="BoxDesign" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt8" runat="server" />
                                                            <domval:RequiredDomValidator ID="ValtxtQta" runat="server" EnableClientScript="False"
                                                                ControlToValidate="TxtQta" ErrorMessage="*"></domval:RequiredDomValidator>
                                                        </div>
                                                        <asp:TextBox ID="TxtQta" runat="server" CssClass="BoxDesignReq" onkeypress="NumbersOnly(event,'.,',this)" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt9" runat="server" /></div>
                                                        <asp:TextBox ID="TxtQtaBlister" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
                                                    </td>
                                                </tr>

                                                <tr style="border-bottom:1px solid black">
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral1" Text="Captxt28" runat="server" /></div>
                                                        <asp:CheckBox ID="chkPrint" runat="server" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral2" Text="Esttxt23" runat="server" /></div>
                                                        <table width="100%" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txtPriceExpire" runat="server" Width="100%" CssClass="BoxDesign"></asp:TextBox>
                                                                </td>
                                                                <td width="30">
                                                                    &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=txtPriceExpire&Start='+(document.getElementById('txtPriceExpire')).value,event,195,195)">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral ID="LocalizedLiteral3" Text="Captxt29" runat="server" /></div>
                                                        <asp:TextBox ID="txtStock" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt10" runat="server" />
                                                            <domval:RequiredDomValidator ID="ValtxtUnitPrice" runat="server" EnableClientScript="False"
                                                                ControlToValidate="TxtUnitPrice" ErrorMessage="*"></domval:RequiredDomValidator>
                                                        </div>
                                                        <asp:Literal ID="CurrentCurrency" runat="server" />
                                                        <asp:TextBox ID="TxtUnitPrice" runat="server" CssClass="BoxDesignReq" onkeypress="NumbersOnly(event,'.,',this)"
                                                            Width="100px" />
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt11" runat="server" /></div>
                                                        <asp:DropDownList ID="listVat" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt26" runat="server" /></div>
                                                        <asp:Literal ID="CurrentCurrency2" runat="server" />
                                                        <asp:TextBox ID="TxtCost" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"
                                                            Width="100px" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan=3>
                                                        <asp:Literal ID="litExcludeList" runat=server Visible=false></asp:Literal>
                                                        <asp:Repeater ID="RepOtherList" runat=server>
                                                            <HeaderTemplate>
                                                                <table cellpadding=0 cellspacing=0 width="100%">
                                                                    <tr>
                                                                        <td width="30%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral4" Text="Captxt33" runat="server" /></td>
                                                                        <td width="5%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral6" Text="Captxt35" runat="server" /></td>
                                                                        <td width="5%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral7" Text="Captxt36" runat="server" /></td>
                                                                        <td width="20%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral5" Text="Captxt10" runat="server" /></td>
                                                                        <td width="20%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral8" Text="Captxt11" runat="server" /></td>
                                                                        <td width="20%" class=GridTitle><twc:LocalizedLiteral ID="LocalizedLiteral9" Text="Captxt26" runat="server" /></td>
                                                                    </tr>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                    <tr>
                                                                        <td class=GridItem><%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%></td>
                                                                        <td class=GridItem>
                                                                            <asp:CheckBox ID="chkListEnable" runat=server Checked=true />
                                                                        </td>
                                                                        <td class=GridItem>
                                                                            <asp:CheckBox ID="chkListPrice" runat=server Checked=true />
                                                                        </td>
                                                                        <td width="20%" class=GridItem>
                                                                            <asp:Literal ID="ListId" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Literal>
                                                                            <asp:TextBox ID="ListUnitPrice" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
                                                                        </td>
                                                                        <td width="20%" class=GridItem>
                                                                            <asp:DropDownList ID="ListlistVat" runat="server"></asp:DropDownList>
                                                                        </td>
                                                                        <td width="20%" class=GridItem>
                                                                            <asp:TextBox ID="ListCost" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
                                                                        </td>
                                                                    </tr>
                                                            </ItemTemplate>
                                                            <AlternatingItemTemplate>
                                                            <tr>
                                                                        <td class=GridItemAltern><%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%></td>
                                                                        <td class=GridItemAltern>
                                                                            <asp:CheckBox ID="chkListEnable" runat=server Checked=true />
                                                                        </td>
                                                                        <td class=GridItemAltern>
                                                                            <asp:CheckBox ID="chkListPrice" runat=server Checked=true />
                                                                        </td>
                                                                        <td width="20%" class=GridItemAltern>
                                                                            <asp:Literal ID="ListId" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Literal>
                                                                            <asp:TextBox ID="ListUnitPrice" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
                                                                        </td>
                                                                        <td width="20%" class=GridItemAltern>
                                                                            <asp:DropDownList ID="ListlistVat" runat="server"></asp:DropDownList>
                                                                        </td>
                                                                        <td width="20%" class=GridItemAltern>
                                                                            <asp:TextBox ID="ListCost" runat="server" CssClass="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)"/>
                                                                        </td>
                                                                    </tr>
                                                            </AlternatingItemTemplate>
                                                            <FooterTemplate></table></FooterTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="3">
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt23" runat="server" />
                                                            <asp:Label ID="ViewImage" runat="server"></asp:Label>
                                                        </div>
                                                        <Upload:InputFile ID="ProductImage" runat="server" CssClass="BoxDesign" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <div>
                                                            <twc:LocalizedLiteral Text="Captxt24" runat="server" /></div>
                                                        <Upload:InputFile ID="ProductDocument" runat="server" CssClass="BoxDesign" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <div id="divProgressBar" style="display: none">
                                                            <Upload:ProgressBar Inline="true" Height="20px" Width="100%" ID="inlineProgressBar"
                                                                runat="server" Triggers="BtnSubmit">
                                                            </Upload:ProgressBar>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Label ID="LblInfo" runat="server" CssClass="divautoformRequired" />
                                                    </td>
                                                    <td colspan="3" align="right">
                                                        <asp:LinkButton ID="BtnSubmit" runat="server" CssClass="Save" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </twc:TustenaTab>
                        </twc:TustenaTabber>
                        <asp:Repeater ID="ProductRepeater" runat="server">
                            <HeaderTemplate>
                                <table border="0" cellpadding="2" cellspacing="1" width="98%" align="center" class="normal">
                                    <tr>
                                        <td class="normal" style="color: gray" colspan="4">
                                            <twc:LocalizedLiteral Text="Captxt21" runat="server" /></td>
                                    </tr>
                                    <tr>
                                        <td class="GridTitle" width="40%">
                                            <twc:LocalizedLiteral Text="Captxt3" runat="server" /></td>
                                        <td class="GridTitle" width="19%">
                                            <twc:LocalizedLiteral Text="Captxt4" runat="server" /></td>
                                        <td class="GridTitle" width="40%">
                                            <twc:LocalizedLiteral Text="Captxt5" runat="server" /></td>
                                        <td class="GridTitle" width="1%">
                                            &nbsp;</td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="GridItem" width="40%">
                                        <asp:Label ID="LblCategory" runat="server" CssClass="normal" />
                                        <asp:Literal ID="LblID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    </td>
                                    <td class="GridItem" width="19%">
                                        <asp:Label ID="LblCode" runat="server" CssClass="normal" Text='<%#DataBinder.Eval(Container.DataItem,"Code")%>' />&nbsp;
                                    </td>
                                    <td class="GridItem" width="40%">
                                        <asp:LinkButton ID="BtnProduct" CommandName="BtnProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>' />
                                    </td>
                                    <td class="GridItem" width="1%" nowrap>
                                        <asp:LinkButton ID="DelProduct" CommandName="DelProduct" runat="server" Text="<img src=/i/erase.gif border=0>" />
                                        &nbsp;
                                        <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<img src=/i/sheet.gif border=0>' />
                                        <span onclick="NewWindow('/catalog/printproduct.aspx?noprint=1&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',500,500,'no')"
                                            style="cursor: pointer;">
                                            <img src="/i/lens.gif" border="0"></span> <span onclick="NewWindow('/catalog/printproduct.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',400,400,'no')"
                                                style="cursor: pointer;">
                                                <img src="/i/printer.gif" border="0"></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td class="GridItemAltern" width="40%">
                                        <asp:Label ID="LblCategory" runat="server" CssClass="normal" />
                                        <asp:Literal ID="LblID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' />
                                    </td>
                                    <td class="GridItemAltern" width="19%">
                                        <asp:Label ID="LblCode" runat="server" CssClass="normal" Text='<%#DataBinder.Eval(Container.DataItem,"Code")%>' />&nbsp;
                                    </td>
                                    <td class="GridItemAltern" width="40%">
                                        <asp:LinkButton ID="BtnProduct" CommandName="BtnProduct" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ShortDescription")%>' />
                                    </td>
                                    <td class="GridItemAltern" width="1%" nowrap>
                                        <asp:LinkButton ID="DelProduct" CommandName="DelProduct" runat="server" Text="<img src=/i/erase.gif border=0>" />
                                        &nbsp;
                                        <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<img src=/i/sheet.gif border=0>' />
                                        <span onclick="NewWindow('/catalog/printproduct.aspx?noprint=1&ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',500,500,'no')"
                                            style="cursor: pointer;">
                                            <img src="/i/lens.gif" border="0"></span> <span onclick="NewWindow('/catalog/printproduct.aspx?ProductID=<%#DataBinder.Eval(Container.DataItem,"ID")%>','PrintProduct',400,400,'no')"
                                                style="cursor: pointer;">
                                                <img src="/i/printer.gif" border="0"></span>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                        <Pag:RepeaterPaging ID="Repeaterpaging1" Visible="false" runat="server" />
                        <asp:Label ID="RepeaterInfo" runat="server" Visible="false" Class="divautoformRequired" /></td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
