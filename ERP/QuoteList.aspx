<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>

<%@ Page Language="c#" Codebehind="QuoteList.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.ERP.QuoteList" %>

<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head id="head" runat="server">

    <script type="text/javascript" src="/js/dynabox.js"></script>

    <title>QuoteList</title>
</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">

        <script>
function OpenSearchBox(e){
				var x;

				if((document.getElementById("CrossWith_0")).checked)
					x=0;
				else if	((document.getElementById("CrossWith_1")).checked)
						x=1;
					 else if ((document.getElementById("CrossWith_2")).checked)
							x=2;
						  else
							x=3;


				switch(x){
					case 0:
						CreateBox('/Common/PopCompany.aspx?render=no&textbox=CrossWithText&textbox2=CrossWithID',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID',e,400,300);
						break;
				}
			}
        </script>

        <table width="100%" border="0" cellspacing="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <asp:LinkButton ID="NewQuote" CssClass="sidebtn" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Quotxt3")%>
                                </div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Esttxt23")%>
                                </div>
                                <table class="sideFixed" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="DiffQuoteExpire" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">&le;</asp:ListItem>
                                                <asp:ListItem Value="1" Selected>&equiv;</asp:ListItem>
                                                <asp:ListItem Value="2">&ge;</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:TextBox ID="TextBoxSearchQuoteExpire" runat="server" CssClass="BoxDesign"></asp:TextBox>
                                        </td>
                                        <td width="22" valign="bottom">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=TextBoxSearchQuoteExpire&amp;Start='+(document.getElementById('TextBoxSearchQuoteExpire')).value,event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Acttxt127")%>
                                </div>
                                <div class="sideInput">
                                    <asp:DropDownList ID="SearchQuoteStage" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Acttxt128")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="TextboxSearchQuoteNumber" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Quotxt6")%>
                                </div>
                                <div class="sideInput">
                                    <asp:TextBox ID="TextboxSearchDescription" runat="server" CssClass="BoxDesign" /></div>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Acttxt65")%>
                                    </div>
                                <table class="sideFixed" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextboxSearchOwnerID" runat="server" Style="display: none"></asp:TextBox>
                                            <asp:TextBox ID="TextboxSearchOwner" runat="server" Width="100%" CssClass="BoxDesign"
                                                ReadOnly="true" />
                                        </td>
                                        <td width="22">
                                            &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&amp;textbox=TextboxSearchOwner&amp;textbox2=TextboxSearchOwnerID',event)">
                                        </td>
                                    </tr>
                                </table>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Tictxt38")%>
                                </div>
                                <table class="sideFixed" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td colspan="2">
                                            <asp:RadioButtonList ID="CrossWith" runat="server" CssClass="normal">
                                            </asp:RadioButtonList>
                                            <asp:TextBox ID="CrossWithID" runat="server" Style="display: none"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="99%">
                                            <asp:TextBox ID="CrossWithText" runat="server" CssClass="BoxDesign" ReadOnly="true" />
                                        </td>
                                        <td width="22">
                                            &nbsp;<img src="/i/user.gif" border="0" style="cursor: pointer" onclick="OpenSearchBox(event)">
                                        </td>
                                    </tr>
                                </table>
                                <div class="sideInputTitle">
                                    <%=wrm.GetString("Quotxt9")%>
                                </div>
                                <table class="sideFixed" cellspacing="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:RadioButtonList ID="RadiobuttonlistGrandTotal" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="0">&le;</asp:ListItem>
                                                <asp:ListItem Value="1" Selected>&equiv;</asp:ListItem>
                                                <asp:ListItem Value="2">&ge;</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:TextBox ID="SearchGrandTotal" runat="server" Width="100%" CssClass="BoxDesign"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="sideSubmit">
                                    <asp:LinkButton ID="btnSearch" CssClass="Save" runat="server"></asp:LinkButton></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <%=wrm.GetString("Quotxt1")%>
                            </td>
                        </tr>
                    </table>
                    <twc:TustenaRepeater ID="NewQuoteListRepeater" runat="server" SortDirection="asc"
                        AllowPaging="true" AllowAlphabet="true" FilterCol="Subject" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <td class="GridTitle" width="1%">
                                &nbsp;</td>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader1" runat="Server" CssClass="GridTitle"
                                DataCol="expirationdate" Width="15%" Resource="Quotxt4">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader2" runat="Server" CssClass="GridTitle"
                                DataCol="Number" Width="10%" Resource="Quotxt5">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader3" runat="Server" CssClass="GridTitle"
                                DataCol="Subject" Width="30%" Resource="Quotxt6">
                            </twc:RepeaterColumnHeader>
                            <td class="GridTitle">
                                <%=wrm.GetString("Quotxt7")%>
                            </td>
                            <td class="GridTitle">
                                <%=wrm.GetString("Quotxt8")%>
                            </td>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader6" runat="Server" CssClass="GridTitle"
                                DataCol="GrandTotal" Width="10%" Resource="Quotxt9">
                            </twc:RepeaterColumnHeader>
                            <td class="GridTitle" width="1%">
                                &nbsp;</td>
                            <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle"
                                header="true">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenQuote" runat="server" CommandName="OpenQuote"></asp:LinkButton>
                                    <asp:Literal ID="QuoteID" runat="server" Visible="False"></asp:Literal></td>
                                <td class="GridItem">
                                    <asp:Literal ID="QuoteDate" runat="server"></asp:Literal></td>
                                <td class="GridItem">
                                    <asp:Literal ID="QuoteNumber" runat="server"></asp:Literal></td>
                                <td class="GridItem">
                                    <asp:Literal ID="QuoteDescription" runat="server"></asp:Literal></td>
                                <td class="GridItem">
                                    <asp:Literal ID="QuoteCustomer" runat="server"></asp:Literal></td>
                                <td class="GridItem">
                                    <asp:Literal ID="QuoteOwner" runat="server"></asp:Literal></td>
                                <td class="GridItem" align="right">
                                    <asp:Literal ID="QuoteTotal" runat="server"></asp:Literal></td>
                                <td class="GridItem" align="right">
                                    <asp:Literal ID="lblPrint" runat="server" Visible="true"></asp:Literal></td>
                                <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItem">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenQuote" runat="server" CommandName="OpenQuote"></asp:LinkButton>
                                    <asp:Literal ID="QuoteID" runat="server" Visible="False"></asp:Literal></td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="QuoteDate" runat="server"></asp:Literal></td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="QuoteNumber" runat="server"></asp:Literal></td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="QuoteDescription" runat="server"></asp:Literal></td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="QuoteCustomer" runat="server"></asp:Literal></td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="QuoteOwner" runat="server"></asp:Literal></td>
                                <td class="GridItemAltern" align="right">
                                    <asp:Literal ID="QuoteTotal" runat="server"></asp:Literal></td>
                                <td class="GridItem" align="right">
                                    <asp:Literal ID="lblPrint" runat="server" Visible="true"></asp:Literal></td>
                                <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItem">
                                </twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
