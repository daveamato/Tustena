<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Page Language="c#" Codebehind="CRM_Notes.aspx.cs" Inherits="Digita.Tustena.Notes"%>

<html>
<head id="head" runat="server">
    <twc:LocalizedScript resource="Notetxt1" runat="server" />

    <script>
function Rimuovi(idx){
	if (confirm(Notetxt1)) {
	 	location.href="/CRM/base_messages.aspx?del"+idx;
	 }
}

function OpenPopTask(){
	window.open("/CRM/BASE_NewTask.aspx?render=no", "", "fullscreen=no,toolbar=no,status=no,menubar=no,scrollbars=no,resizable=no,directories=no,location=no,width=420,height=360,left=100,top=100")
}

    </script>

</head>
<body id="body" runat="server">
    <form runat="server">
        <table width="100%" cellspacing="0">
            <tbody>
                <tr>
                    <td width="140" height="100%" class="SideBorderLinked" valign="top">
                        <table class="sidemenu" width="98%" border="0" cellspacing="0" align="center" cellpadding="0">
                            <tr>
                                <td class="sideContainer">
                                    <div class="sideTitle">
                                        <%=wrm.GetString("Options")%>
                                    </div>
                                    <a href="javascript:OpenPopTask()" class="sidebtn">
                                        <%=wrm.GetString("Notetxt3")%>
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
                                        <%=wrm.GetString("Notetxt4")%>
                                    </div>
                                    <div class="sideInput">
                                        <asp:TextBox ID="TxtSearch" runat="server" CssClass="BoxDesign" /></div>
                                    <div class="sideSubmit">
                                        <asp:LinkButton ID="BtnSearch" runat="server" CssClass="Save" OnClick="BtnSearch_Click" /></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign="top" height="100%" class="pageStyle">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="left" class="pageTitle" valign="top">
                                    <%=wrm.GetString("Notetxt2")%>
                                </td>
                            </tr>
                        </table>
                        <twc:TustenaRepeater ID="NewRepNotes" runat="server" SortDirection="asc" AllowPaging="true"
                            AllowAlphabet="false" FilterCol="CreatedDate" AllowSearching="false">
                            <HeaderTemplate>
                                <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                                </twc:RepeaterHeaderBegin>
                                <twc:RepeaterColumnHeader ID="Repeatercolumnheader1" runat="Server" DataCol="CreatedDate"
                                    Width="15%" CssClass="GridTitle" Resource="Notetxt5">
                                </twc:RepeaterColumnHeader>
                                <twc:RepeaterColumnHeader ID="Repeatercolumnheader2" runat="Server" DataCol="Subject"
                                    Width="54%" CssClass="GridTitle" Resource="Notetxt6">
                                </twc:RepeaterColumnHeader>
                                <twc:RepeaterColumnHeader ID="Repeatercolumnheader3" runat="Server" DataCol="Owner"
                                    Width="30%" CssClass="GridTitle" Resource="Notetxt7">
                                </twc:RepeaterColumnHeader>
                                <twc:RepeaterMultiDelete ID="Repeatermultidelete2" runat="server" CssClass="GridTitle">
                                </twc:RepeaterMultiDelete>
                                <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                                </twc:RepeaterHeaderEnd>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td class="GridItem">
                                        <%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}") %>
                                    </td>
                                    <td class="GridItem">
                                        <asp:LinkButton ID="ModifyNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>'
                                            CommandName="modifyNote">
                                        </asp:LinkButton></td>
                                    <td class="GridItem">
                                        <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                                        <asp:Literal ID="IDApp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>'
                                            Visible="false">
                                        </asp:Literal>
                                        <asp:Literal ID="TextApp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>'
                                            Visible="false">
                                        </asp:Literal></td>
                                    <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItem">
                                    </twc:RepeaterMultiDelete>
                                </tr>
                                <div id="ViewText" runat="server">
                                    <tr>
                                        <td class="FullBorder" colspan="5">
                                            <asp:TextBox class="BoxDesign" ID="AreaText" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Body") %>'
                                                Height="100" TextMode="MultiLine">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="5">
                                            <asp:LinkButton class="normal" ID="NoteSubmit" runat="server" Text="SALVA" CommandName="noteSubmit"></asp:LinkButton></td>
                                    </tr>
                                </div>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td class="GridItemAltern">
                                        <%# DataBinder.Eval(Container.DataItem, "CreatedDate", "{0:d} {0:HH:mm}")%>
                                    </td>
                                    <td class="GridItemAltern">
                                        <asp:LinkButton ID="ModifyNote" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Subject")%>'
                                            CommandName="modifyNote">
                                        </asp:LinkButton></td>
                                    <td class="GridItemAltern">
                                        <%# DataBinder.Eval(Container.DataItem, "Owner")%>
                                        <asp:Literal ID="IDApp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id")%>'
                                            Visible="false">
                                        </asp:Literal>
                                        <asp:Literal ID="TextApp" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Body")%>'
                                            Visible="false">
                                        </asp:Literal></td>
                                    <twc:RepeaterMultiDelete ID="DelCheck" runat="server" CssClass="GridItem">
                                    </twc:RepeaterMultiDelete>
                                </tr>
                                <div id="ViewText" runat="server">
                                    <tr>
                                        <td class="FullBorder" colspan="5">
                                            <asp:TextBox class="BoxDesign" ID="AreaText" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Body") %>'
                                                Height="100" TextMode="MultiLine">
                                            </asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="5">
                                            <asp:LinkButton class="normal" ID="NoteSubmit" runat="server" Text="SALVA" CommandName="noteSubmit"></asp:LinkButton></td>
                                    </tr>
                                </div>
                            </AlternatingItemTemplate>
                        </twc:TustenaRepeater>
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>
