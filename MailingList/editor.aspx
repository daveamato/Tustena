<%@ Page Language="c#" Inherits="Digita.Tustena.editor" ValidateRequest="false" Trace="false"
    Codebehind="editor.aspx.cs" AutoEventWireup="false" %>

<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="fckeditorv2" Namespace="FredCK.FCKeditorV2" Assembly="Tustena" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <twc:LocalizedScript resource="MLtxt48,MLtxt49,MLtxt50" runat="server" />

    <script>
		var oEditor = null;
		function AddField(f)
		{
			if(oEditor!=null)
				oEditor.InsertHtml(f);
		}

		function Save_Click()
		{
			var oRegex = new RegExp( '.*?<img.*?src=[\'|"]?(file:///([^\'|"]*)).*?>.*?', 'ig' ) ;
			var oMatch = oRegex.exec( document.getElementById('editor1').value ) ;
			if ( oMatch && oMatch.length > 1 )
			{
				alert( MLtxt48 + oMatch[2] );
				return false;
			}
			var _SaveAll = document.getElementById("SaveAll");
			clickElement(_SaveAll);
			}
			function SaveWithName()
			{
			var _fname = document.getElementById("SaveAs");
			var answer = _fname.value.toString();
			if(answer.length>0) return true;
			answer = prompt(MLtxt49,"file1");
			if(answer) {
			_fname.value = checkExtension(answer);
			return true;
			}
			return false; }
			function msgconfirm() {
			return confirm(MLtxt50);
			}
	function checkExtension(stringa) {
			var s = stringa.toLowerCase();
				if(s.indexOf(".swm")>-1) {
				if(s.substring(s.length-4,s.length)==".swm") {
					return s; } }
					return s + ".swm"; }

function ClearDocument()
{
	document.getElementById("DocumentDescription").value = "";
	document.getElementById("IDDocument").value = "";
	var obj = document.getElementById("LinkDocument");
	if (obj != null) obj.style.display = "none";
}


	function FCKeditor_OnComplete( editorInstance )
	{

		oEditor = editorInstance;
		window.status = editorInstance.Description ;
	}

function SelectOther()
  {
	var obj = document.getElementById("MailCategory");
	var obj1 = document.getElementById("NewMailCategory");
	if(obj.options[obj.selectedIndex].value=="A99")
	{
		obj1.style.display='';
		obj1.style.background='#ffffff';
		obj.style.display='none';
	}
  }
    </script>

    <script language="javascript" src="/js/dynabox.js"></script>

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
                                <asp:LinkButton ID="BtnNewML" runat="server" Class="sidebtn" />
                                <a href="/MailingList/NewMailingList.aspx?m=46&si=51&dgb=1" class="sidebtn">
                                    <%=wrm.GetString("MLtxt37")%>
                                </a><a href="/MailingList/editor.aspx?m=46&dgb=1&si=51" class="sidebtn">
                                    <%=wrm.GetString("MLtxt53")%>
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
                                    <asp:TextBox ID="Search" autoclick="BtnSearch" runat="server" class="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("CRMcontxt19")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="SearchMailCategory" runat="server" class="BoxDesign" /></div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnSearch" runat="server" class="save" /></div>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" id="TableFields" border="0" runat="server" cellspacing="0" align="center"
                        cellpadding="0">
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("MLtxt13")%>
                                </div>
                                <a href="javascript:AddField('Tustena.CompanyName')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt14")%>
                                </a><a href="javascript:AddField('Tustena.Name')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt15")%>
                                </a><a href="javascript:AddField('Tustena.Surname')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt16")%>
                                </a><a href="javascript:AddField('Tustena.Address')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt17")%>
                                </a><a href="javascript:AddField('Tustena.City')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt18")%>
                                </a><a href="javascript:AddField('Tustena.Province')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt19")%>
                                </a><a href="javascript:Tustena.Nation')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt20")%>
                                </a><a href="javascript:Tustena.ZipCode')" class="sidebtn">
                                    <%=wrm.GetString("MLtxt21")%>
                                </a>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <span id="MailEditor" runat="server">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left" class="pageTitle" valign="top">
                                    E-MAIL EDITOR
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br>
                                    <table cellspacing="0" cellpadding="0" width="100%">
                                        <tr>
                                            <td width="33%">
                                                <div class="normal">
                                                    <%=wrm.GetString("MLtxt43")%>
                                                </div>
                                                <asp:TextBox ID="NewMLDescription" runat="server" class="BoxDesign" />
                                            </td>
                                            <td width="33%">
                                                <div class="normal">
                                                    <%=wrm.GetString("MLtxt41")%>
                                                </div>
                                                <asp:TextBox ID="SaveAs" runat="server" CssClass="BoxDesign" />
                                                <asp:Literal ID="FileName" runat="server" Visible="false" />
                                            </td>
                                            <td width="34%">
                                                <div>
                                                    <%=wrm.GetString("Menutxt38")%>
                                                </div>
                                                <asp:DropDownList ID="MailCategory" old="true" runat="server" class="BoxDesign" />
                                                <asp:TextBox ID="NewMailCategory" runat="server" class="BoxDesign" Style="display: none" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="normal">
                                        <%=wrm.GetString("Acttxt85")%>
                                    </div>
                                    <table width="50%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="33%">
                                                <asp:TextBox ID="DocumentDescription" runat="server" CssClass="BoxDesign" ReadOnly />
                                                <asp:TextBox ID="IDDocument" runat="server" Style="display: none"></asp:TextBox>
                                            </td>
                                            <td width="33%" nowrap>
                                                &nbsp;
                                                <img src="/i/sheet.gif" border="0" alt='<%=wrm.GetString("Acttxt98")%>' style="cursor: pointer"
                                                    onclick="CreateBox('/common/PopFile.aspx?render=no&amp;textbox=DocumentDescription&amp;textboxID=IDDocument',event,600,500)">
                                                <img src="/i/deletedoc.gif" border="0" style="cursor: pointer" onclick="ClearDocument()">
                                            </td>
                                            <td width="33%" nowrap align="left">
                                                Welcome email
                                                <asp:CheckBox ID="welcometype" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <br>
                                    <fckeditorv2:FCKeditor ID="editor1" runat="server">
                                    </fckeditorv2:FCKeditor>
                                </td>
                            </tr>
                        </table>
                        <div style="display: none">
                            <asp:Button ID="SaveAll" runat="server" CssClass="save" />
                        </div>
                    </span>
                    <asp:Panel ID="MailListPanel" runat=server>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("MLtxt53")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <twc:TustenaRepeater ID="MailList" runat="server" SortDirection="asc" AllowPaging="true"
                                    AllowAlphabet="true" AllowSearching="false">
                                    <HeaderTemplate>
                                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                                        </twc:RepeaterHeaderBegin>
                                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderAlphabet1" runat="Server">
                                        </twc:RepeaterHeaderBegin>
                                        <twc:RepeaterColumnHeader ID="Repeatercolumnheader1" runat="Server" CssClass="GridTitle"
                                            Width="90%" DataCol="Subject">
                                            Mail</twc:RepeaterColumnHeader>
                                        </td>
                                        <td class="GridTitle" width="9%">
                                            &nbsp;</td>
                                        <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle"/>
                                        <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                                        </twc:RepeaterHeaderEnd>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="GridItem" width="90%">
                                                <asp:Literal ID="MailID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'>
                                                </asp:Literal>
                                                <asp:LinkButton ID="MailLink" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")+" - " + wrm.GetString("MLtxt41")+":"+DataBinder.Eval(Container.DataItem,"Subject")%>'
                                                    CommandName="MailLink">
                                                </asp:LinkButton></td>
                                            <td class="GridItem" nowrap width="9%">
                                                <asp:LinkButton ID="SendMail" runat="server" CommandName="SendMail"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="ModifyMail" runat="server" CommandName="MailLink"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="CopyMail" runat="server" CommandName="CopyMail"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="SendSingle" runat="server" CommandName="SendSingle"></asp:LinkButton></td>
                                            <twc:RepeaterMultiDelete CssClass="GridItem" ID="DelCheck" runat="server">
                                            </twc:RepeaterMultiDelete>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr>
                                            <td class="GridItemAltern" width="90%">
                                                <asp:Literal ID="MailID" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'>
                                                </asp:Literal>
                                                <asp:LinkButton ID="MailLink" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>'
                                                    CommandName="MailLink">
                                                </asp:LinkButton></td>
                                            <td class="GridItemAltern" nowrap width="9%">
                                                <asp:LinkButton ID="SendMail" runat="server" CommandName="SendMail"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="ModifyMail" runat="server" CommandName="MailLink"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="CopyMail" runat="server" CommandName="CopyMail"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="SendSingle" runat="server" CommandName="SendSingle"></asp:LinkButton></td>
                                            <twc:RepeaterMultiDelete CssClass="GridItemAltern" ID="DelCheck" runat="server">
                                            </twc:RepeaterMultiDelete>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </twc:TustenaRepeater>
                                <Pag:RepeaterPaging ID="MailListPaging" Visible="false" runat="server" />
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
