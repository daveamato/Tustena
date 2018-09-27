<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>

<%@ Page Language="c#" Trace="false" Codebehind="DataStorage.aspx.cs" Inherits="Digita.Tustena.DataStorage"
    AutoEventWireup="false" %>

<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <twc:LocalizedScript resource="Reftxt57,Reftxt56,Dsttxt22" runat="server" />

    <script>
function copyData(id, category, txtcat, txtcatid, toclick)
{
	HideFloatDiv('floatCategory');
	document.getElementById(txtcatid).value = id;
	document.getElementById(txtcat).value = category;
	try{
		if(toclick.length>0){
			clickElement(document.getElementById(toclick));
		}
	}catch(e){}
}


function Presubmit(e){
	if((document.getElementById('CrossWith_0').checked==false && document.getElementById('CrossId').value.length==0) || document.getElementById('CategoryId').value.length==0){
		if(document.getElementById('CategoryId').value.length==0)
			alert(Reftxt57);
		else
		{
			alert(Reftxt56);
			document.getElementById('CrossWith_0').checked=true;
		}
		if(e.preventDefault){
			e.preventDefault();
		}else{
			e.returnValue = false;
		}
		return false;
	}
	else{
		var progress = document.getElementById('divProgressBar');
		progress.style.display='';
		return true;
		}
		//ShowUpload();
}

function Cross(e){
		document.getElementById("CrossText").value="";
		document.getElementById("crossId").value="";
		if (document.getElementById("CrossWith_1").checked==true)
		{
			CreateBox('/Common/PopCompany.aspx?render=no&textbox=CrossText&textbox2=crossId',e,500,400);
		}else{
			if (document.getElementById("CrossWith_2").checked==true)
			{
				CreateBox('/Common/popcontacts.aspx?render=no&textbox=CrossText&textboxID=CrossId',e,400,200);
			}else{
				if (document.getElementById("CrossWith_3").checked==true)
				{
					CreateBox('/Common/PopOpportunity.aspx?render=no&textbox=CrossText&textboxID=crossId',e,400,200);
				}else{
						if (document.getElementById("CrossWith_4").checked==true)
						{
							CreateBox('/Common/PopActivity.aspx?render=no&textbox=CrossText&textboxID=crossId',e,400,200);
						}else{
							alert(Dsttxt22);
						}
				}
			}
		}
}
    </script>

</head>
<body id="body" runat="server">
    <form id="uploadForm" runat="server">
        <table cellspacing="0" width="100%" border="0">
            <tr>
                <td class="SideBorderLinked" valign="top" width="200">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton cssclass="sidebtn" ID="LbnNew" OnClick="btn_Click" runat="server" />
                                <asp:LinkButton cssclass="sidebtn" ID="LbnBack" runat="server" />
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
                                    <asp:TextBox ID="TxtSearch" runat="server" autoclick="BtnSearch" CssClass="BoxDesign" /></div>
                                <div class="sideInput">
                                    <asp:RadioButtonList ID="searchType" runat="server" CssClass="normal" />
                                </div>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="BtnSearchText" OnClick="btn_Click" runat="server" Text="OK" CssClass="save" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("CRMcontxt54")%>
                                </div>
                                <ie:TreeView ID="tvCategoryTreeSearch" runat="server" HoverStyle="background:#F2F2F2;color:black"
                                    DefaultStyle="font-family:Verdana;font-size:10px" SelectedStyle="background:#FFA500;color:black"
                                    SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:TreeView><asp:TextBox ID="CategoryTextSearch"
                                        Style="display: none" runat="server" value=""></asp:TextBox><asp:TextBox ID="CategoryIdSearch"
                                            Style="display: none" runat="server" value=""></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <span id="helptext" runat="server">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left" class="pageTitle" valign="top">
                                    <%=wrm.GetString("Dsttxt14")%>
                                    <asp:Label ID="LlblAction" runat="server"></asp:Label>
                                    <br>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="left">
                                    <asp:Literal ID="HelpLabel" runat="server"></asp:Literal></td>
                            </tr>
                        </table>
                    </span>
                    <asp:Repeater ID="FileRep" runat="server" OnItemCommand="FileRepCommand" OnItemDataBound="FileRepDatabound">
                        <HeaderTemplate>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td align="left" class="pageTitle" valign="top">
                                        <%=wrm.GetString("Dsttxt1")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="normal">
                                        &nbsp;</td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="GridTitle" width="25%">
                                        <%=wrm.GetString("Dsttxt2")%>
                                    </td>
                                    <td class="GridTitle" width="1%">
                                        R</td>
                                    <td class="GridTitle" width="15%">
                                        <%=wrm.GetString("Dsttxt3")%>
                                    </td>
                                    <td class="GridTitle" width="19%">
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
                                <td class="GridItem">
                                    <asp:Image ID="FileImg" ImageUrl="" runat="server"></asp:Image>
                                    &nbsp;
                                    <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                        Visible="false" />
                                    <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                    &nbsp;
                                </td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                        runat="server" />
                                    <asp:Literal ID="LtrReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                        runat="server" />
                                    &nbsp;</td>
                                <td class="GridItem" nowrap>
                                    <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                                    &nbsp;</td>
                                <td class="GridItem">
                                    <%#DataBinder.Eval(Container.DataItem,"owner")%>
                                    &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                                <td class="GridItem" align="right">
                                    <asp:Literal ID="FileSize" runat="server" />
                                <td class="GridItem" nowrap>
                                    <asp:Literal ID="info" runat="server" />
                                    <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                    <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
                                    <asp:LinkButton ID="Delete" CommandName="Delete" runat="server" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Image ID="FileImg" ImageUrl="" runat="server"></asp:Image>
                                    &nbsp;
                                    <asp:Literal ID="FileId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'
                                        Visible="false" />
                                    <asp:LinkButton ID="Down" CommandName="Down" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FileName")%>' />
                                    &nbsp;
                                </td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="ReviewNumber" CommandName="ReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                        runat="server" />
                                    <asp:Literal ID="LtrReviewNumber" Text='<%#DataBinder.Eval(Container.DataItem,"ReviewNumber")%>'
                                        runat="server" />
                                    &nbsp;</td>
                                <td class="GridItemAltern" nowrap>
                                    <%#DataBinder.Eval(Container.DataItem,"catdesc")%>
                                    &nbsp;</td>
                                <td class="GridItemAltern">
                                    <%#DataBinder.Eval(Container.DataItem,"owner")%>
                                    &nbsp;-&nbsp;<%#DataBinder.Eval(Container.DataItem,"createddate", "{0:d}")%>&nbsp;</td>
                                <td class="GridItemAltern" align="right">
                                    <asp:Literal ID="FileSize" runat="server" />
                                <td class="GridItemAltern" nowrap>
                                    <asp:Literal ID="info" runat="server" />
                                    <asp:LinkButton ID="Modify" CommandName="Modify" runat="server" />
                                    <asp:LinkButton ID="Revision" CommandName="Revision" runat="server" />
                                    <asp:LinkButton ID="Delete" CommandName="Delete" runat="server" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <Pag:RepeaterPaging ID="FileRepPaging" runat="server" Visible="false"></Pag:RepeaterPaging>
                    <div id="floatCategory" style="display: none; position: absolute; border: 1px solid silver;
                        margin: 10px;" onmouseout="HideFloatDiv('floatCategory')">
                        <ie:TreeView ID="tvCategoryTree" runat="server" HoverStyle="background:#F2F2F2;color:black"
                            DefaultStyle="font-family:Verdana;font-size:10px" SelectedStyle="background:#FFA500;color:black"
                            SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:TreeView></div>
                    <table id="fileTab" cellspacing="0" cellpadding="0" width="100%" border="0" runat="server">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("Dsttxt1")%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table class="normal" cellspacing="0" cellpadding="0" width="100%" border="0" width="600">
                                    <tr>
                                        <td width="20%">
                                            <%=wrm.GetString("Dsttxt6")%>
                                            <asp:Literal ID="FileID" runat="server" Visible="false"></asp:Literal><asp:Literal
                                                ID="FileRevision" runat="server" Visible="false"></asp:Literal><asp:Literal ID="NRevision"
                                                    runat="server" Visible="false"></asp:Literal></td>
                                        <td>
                                            <input class="BoxDesign" id="txtFileName" style="display: none" type="file" runat="server">
                                            <Upload:InputFile cssclass="BoxDesignReq" ID="inputFile" runat="server"></Upload:InputFile>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="divProgressBar" style="display: none;">
                                                <Upload:ProgressBar Inline="true" Height="20px" Width="100%" ID="inlineProgressBar"
                                                    runat="server" Triggers="LbnSubmit">
                                                </Upload:ProgressBar>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <%=wrm.GetString("Dsttxt7")%>
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <img src="/i/tree.gif" style="margin-right: 4px; cursor: pointer" onclick="ShowFloatDiv(event,'floatCategory')">
                                                    </td>
                                                    <td width="100%">
                                                        <asp:TextBox ID="CategoryText" runat="server" CssClass="BoxDesignReq" ReadOnly="true"
                                                            onclick="ShowFloatDiv(event,'floatCategory')"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <asp:TextBox ID="CategoryId" Style="display: none" runat="server" CssClass="BoxDesign"></asp:TextBox>
                                    <tr>
                                        <td colspan="2">
                                            <%=wrm.GetString("Dsttxt8")%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:TextBox cssclass="BoxDesign" ID="Description" runat="server" Height="50" TextMode="MultiLine"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            <%=wrm.GetString("Dsttxt9")%>
                                        </td>
                                        <td>
                                            <asp:RadioButtonList cssclass="normal" ID="CrossWith" runat="server" RepeatDirection="Horizontal"
                                                RepeatColumns="2">
                                            </asp:RadioButtonList></td>
                                    </tr>
                                    <tr>
                                        <td width="20%">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <table cellspacing="0" cellpadding="1" width="100%" border="0">
                                                <tr>
                                                    <td width="95%">
                                                        <asp:TextBox cssclass="BoxDesign" ID="CrossText" runat="server" ReadOnly="true" TextMode="multiline"></asp:TextBox>
                                                        <asp:TextBox cssclass="BoxDesign" ID="CrossId" Style="visibility: hidden" runat="server"></asp:TextBox></td>
                                                    <td nowrap>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <GControl:GroupControl ID="groupsDialog" runat="server"></GControl:GroupControl>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:LinkButton cssclass="save" ID="LbnSubmit" OnClick="btn_Click" runat="server"></asp:LinkButton></td>
                                    </tr>
                                </table>
                                <table>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:Label cssclass="divautoformRequired" ID="Info" runat="server"></asp:Label></td>
            </tr>
        </table>
    </form>
</body>
</html>
