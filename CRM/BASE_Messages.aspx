<%@ Page Language="c#" Trace="false" Codebehind="BASE_Messages.aspx.cs" Inherits="Digita.Tustena.Messages" AutoEventWireup="false" %>

<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
<twc:LocalizedScript resource="Mestxt4" runat="server" />
    <script>
//window.parent.frames(0).isHistory=true;

function Rimuovi(idx){
	if (confirm(Mestxt4)) {
	 	location.href="/CRM/base_messages.aspx?del"+idx;
	}
}

function OpenPopTask(){
	window.open("/CRM/BASE_NewMessage.aspx?render=no", "", "fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,directories=no,location=no,width=420,height=370,left=100,top=100")
}

function OpenAnswer(o,u,i){
	window.open("/CRM/BASE_NewMessage.aspx?render=no&o=" + o + "&u=" + u + "&i=" + i , "", "fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,directories=no,location=no,width=420,height=370,left=100,top=100");
}

function FullMessages(r,id){
	location.href="/CRM/base_messages.aspx?p=" + document.getElementById("CurrentPage").value + "&r=" + r + "&full=" + id;
}

    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" cellspacing="0">
            <tr>
                <td width="140" height="100%" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" align="center" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <%=wrm.GetString("Options")%>
                                </div>
                                <a href="javascript:OpenPopTask()" class="sidebtn">
                                    <%=wrm.GetString("Mestxt11")%>
                                </a>
                                <asp:LinkButton ID="ViewMessagesRicevuti" runat="server" Class="sidebtn" />
                                <asp:LinkButton ID="ViewMessagesInviati" runat="server" Class="sidebtn" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <asp:Literal ID="HeaderMessages" runat="server" />
                            </td>
                            <td align="right" class="BorderBottomTitles">
                                <asp:LinkButton ID="DeleteAllSent" runat="server" CssClass="Save" />
                                <asp:LinkButton ID="DeleteAllReceived" runat="server" CssClass="Save" />
                            </td>
                        </tr>
                    </table>
                    <br>
                    <twc:TustenaRepeater ID="NewRepMessagges" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="UserName" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <td class="GridTitle" width="1%">
                                &nbsp;</td>
                            <twc:RepeaterColumnHeader Resource="Mestxt6" id="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" width="9%" DataCol="CreatedDate">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Mestxt7" id="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" width="25%" DataCol="UserName">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Mestxt8" id="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" width="55%" DataCol="Subject">
                            </twc:RepeaterColumnHeader>
                            <td class="GridTitle" width="10%">
                                &nbsp;</td>
                            <twc:RepeaterMultiDelete id="Repeatermultidelete2" runat="server" CssClass="GridTitle"
                                header="true">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem" width="1%" nowrap>
                                    <asp:Literal ID="ImgMessage" runat="server"></asp:Literal>
                                </td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenMessage" runat="server" CommandName="OpenMessage"><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}")%></asp:LinkButton></td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenMessage1" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' /></td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenMessage2" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' /></td>
                                <td class="GridItem" style="border: none;" align="center" nowrap>
                                    <asp:Literal ID="IDMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                    <asp:Literal ID="TextMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>' />
                                    <span onclick="OpenAnswer('Re:'+'<%# ParseJSString(DataBinder.Eval(Container.DataItem, "Subject").ToString())%>','<%# ParseJSString(DataBinder.Eval(Container.DataItem, "UserName").ToString()) %>','<%# DataBinder.Eval(Container.DataItem, "FromAccount")%>');"
                                        class="Save">
                                        <%=wrm.GetString("Mestxt10")%>
                                    </span>
                                </td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                            <asp:Literal ID="ViewText" runat="server" />
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern" width="1%" nowrap>
                                    <asp:Literal ID="ImgMessage" runat="server"></asp:Literal>
                                </td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenMessage" runat="server" CommandName="OpenMessage"><%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}")%></asp:LinkButton></td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenMessage1" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' /></td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenMessage2" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' /></td>
                                <td class="GridItemAltern" style="border: none;" align="center" nowrap>
                                    <asp:Literal ID="IDMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                    <asp:Literal ID="TextMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>' />
                                    <span onclick="OpenAnswer('Re:'+'<%# ParseJSString(DataBinder.Eval(Container.DataItem, "Subject").ToString())%>','<%# ParseJSString(DataBinder.Eval(Container.DataItem, "UserName").ToString()) %>','<%# DataBinder.Eval(Container.DataItem, "FromAccount")%>');"
                                        class="Save">
                                        <%=wrm.GetString("Mestxt10")%>
                                    </span>
                                </td>
                                <twc:RepeaterMultiDelete CssClass="GridItemAltern" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                            <asp:Literal ID="ViewText" runat="server" />
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                    <twc:TustenaRepeater ID="NewRepMessagesSent" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="UserName" AllowSearching="false">
                        <HeaderTemplate>
                            <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Mestxt6" id="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" width="5%" DataCol="CreatedDate">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Mestxt12" id="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" width="25%" DataCol="UserName">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader Resource="Mestxt8" id="Repeatercolumnheader3" runat="Server"
                                CssClass="GridTitle" width="70%" DataCol="Subject">
                            </twc:RepeaterColumnHeader>
                            <twc:RepeaterMultiDelete id="Repeatermultidelete2" runat="server" CssClass="GridTitle"
                                header="true">
                            </twc:RepeaterMultiDelete>
                            <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Literal ID="TextMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>' />
                                    <asp:Literal ID="IDMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                    <asp:LinkButton ID="OpenMessage" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenMessage1" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' /></td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="OpenMessage2" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' /></td>
                                <twc:RepeaterMultiDelete CssClass="GridItem" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                            <asp:Literal ID="ViewText" runat="server" />
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="TextMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>' />
                                    <asp:Literal ID="IDMess" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                    <asp:LinkButton ID="OpenMessage" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenMessage1" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' /></td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="OpenMessage2" runat="server" CommandName="OpenMessage" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>' /></td>
                                <twc:RepeaterMultiDelete CssClass="GridItemAltern" id="DelCheck" runat="server">
                                </twc:RepeaterMultiDelete>
                            </tr>
                            <asp:Literal ID="ViewText" runat="server" />
                        </AlternatingItemTemplate>
                    </twc:TustenaRepeater>
                </td>
            </tr>
        </table>
        <asp:Literal ID="SomeJS" runat="server" />
    </form>
</body>
</html>
