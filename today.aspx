<%@ Page Language="c#" Codebehind="today.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Today"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<html>
<head id="head" runat="server">
    <style>
.brs1 {BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;}
.brs2 {BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;}
</style>

    <script type="text/javascript" src="/js/dynabox.js"></script>

</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
        <span id="Suggestions" runat="server"></span>
        <asp:Literal ID="FullScreen" runat="server" />
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <twc:SideBarContainer ID="SideBarContainer1" runat="server" Auto="true">
                            <asp:LinkButton ID="LblCompany" runat="server" /></div>
                        <div class="sideTitle">
                            <twc:LocalizedLiteral Text="Options" runat="server" /></div>
                        <twc:SideBarPlaceHolder ID="OptionsControls" runat=server />
                        <twc:SideBarDivider ID="SideBarDivider1" runat="server" />
                    </twc:SideBarContainer>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="left" class="pageTitle" valign="top">
                                <twc:LocalizedLiteral Text="Deftxt9" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <center>
                                    <asp:Label ID="RepAppointmentInfo" runat="server" Visible="false" Class="divautoformRequired" /></center>
                                <asp:Repeater ID="RepAppointment" runat="server">
                                    <HeaderTemplate>
                                        <table id="apptoday" border="0" cellpadding="2" cellspacing="1" width="100%" align="center">
                                            <tr>
                                                <td class="GridTitle" width="10%">
                                                    <twc:LocalizedLiteral Text="Deftxt10" runat="server" /></td>
                                                <td class="GridTitle" width="10%">
                                                    <twc:LocalizedLiteral Text="Deftxt11" runat="server" /></td>
                                                <td class="GridTitle" width="80%">
                                                    <twc:LocalizedLiteral Text="Deftxt12" runat="server" /></td>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr style="cursor: pointer;" onclick="location.href='/calendar/agenda.aspx?Date=<%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortDateString()%>';">
                                            <td class="GridItem">
                                                <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortTimeString()%>
                                            </td>
                                            <td class="GridItem">
                                                <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "enddate")).ToShortTimeString()%>
                                            </td>
                                            <td class="GridItem Lcit">
                                                <asp:Label ID="OggettoRe" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact")+((DataBinder.Eval(Container.DataItem, "company").ToString().Length>0)?"&nbsp;["+DataBinder.Eval(Container.DataItem, "company")+"]":"") %>' /></td>
                                        </tr>
                                        <asp:Literal ID="ViewText" runat="server" />
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr style="cursor: pointer;" onclick="location.href='/calendar/agenda.aspx?Date=<%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortDateString()%>';">
                                            <td class="GridItemAltern">
                                                <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortTimeString()%>
                                            </td>
                                            <td class="GridItemAltern">
                                                <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "enddate")).ToShortTimeString()%>
                                            </td>
                                            <td class="GridItemAltern Lcit">
                                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact")%>' /></td>
                                        </tr>
                                        <asp:Literal ID="ViewText" runat="server" />
                                    </AlternatingItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="pageTitle" valign="top">
                                            <twc:LocalizedLiteral Text="Deftxt30" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <center>
                                                <asp:Label ID="FutureAppointmentInfo" runat="server" Visible="false" Class="divautoformRequired" /></center>
                                            <asp:Repeater ID="FutureAppointmentRepeater" runat="server">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="1" width="100%" align="center">
                                                        <tr>
                                                            <td class="GridTitle" width="10%">
                                                                <twc:LocalizedLiteral Text="Deftxt17" runat="server" /></td>
                                                            <td class="GridTitle" width="10%">
                                                                <twc:LocalizedLiteral Text="Deftxt10" runat="server" /></td>
                                                            <td class="GridTitle" width="10%">
                                                                <twc:LocalizedLiteral Text="Deftxt11" runat="server" /></td>
                                                            <td class="GridTitle" width="70%">
                                                                <twc:LocalizedLiteral Text="Deftxt12" runat="server" /></td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr id="approw" runat="server" style="cursor: pointer;">
                                                        <td class="GridItem">
                                                            <asp:Literal ID="startdate" runat="server"></asp:Literal></td>
                                                        <td class="GridItem">
                                                            <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortTimeString()%>
                                                        </td>
                                                        <td class="GridItem">
                                                            <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "enddate")).ToShortTimeString()%>
                                                        </td>
                                                        <td class="GridItem Lcit">
                                                            <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact")+((DataBinder.Eval(Container.DataItem, "company").ToString().Length>0)?"&nbsp;["+DataBinder.Eval(Container.DataItem, "company")+"]":"") %>' /></td>
                                                    </tr>
                                                    <asp:Literal ID="ViewText" runat="server" />
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr id="approw" runat="server" style="cursor: pointer;">
                                                        <td class="GridItemAltern">
                                                            <asp:Literal ID="startdate" runat="server"></asp:Literal></td>
                                                        <td class="GridItemAltern">
                                                            <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "startdate")).ToShortTimeString()%>
                                                        </td>
                                                        <td class="GridItemAltern">
                                                            <%#UC.LTZ.ToLocalTime((DateTime)DataBinder.Eval(Container.DataItem, "enddate")).ToShortTimeString()%>
                                                        </td>
                                                        <td class="GridItemAltern Lcit">
                                                            <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "contact")%>' /></td>
                                                    </tr>
                                                    <asp:Literal ID="ViewText" runat="server" />
                                                </AlternatingItemTemplate>
                                                <FooterTemplate>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="1%" align="left" class="pageTitle" nowrap onclick="location.href='/calendar/agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2';"
                                            style="cursor: pointer;">
                                            <twc:LocalizedLiteral Text="Deftxt13" runat="server" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td width="49%" align="left" class="BorderBottomTitles" nowrap onclick="location.href='Calendar/agenda.aspx?dy=<%=DateTime.Now.ToShortDateString()%>&si=2';"
                                            style="cursor: pointer;">
                                            <asp:Label ID="RepEventInfo" runat="server" Visible="false" Class="divautoformRequired" />
                                        </td>
                                        <td align="left" width="1%" class="pageTitle" nowrap onclick="location.href='/crm/base_messages.aspx?m=25&si=11';"
                                            style="cursor: pointer;">
                                            <twc:LocalizedLiteral Text="Deftxt15" runat="server" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td width="49%" align="left" class="BorderBottomTitles" nowrap onclick="location.href='/crm/base_messages.aspx?m=25&si=11';"
                                            style="cursor: pointer;">
                                            <asp:Label ID="RepMessagesInfo" runat="server" Visible="false" Class="divautoformRequired" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            &nbsp;</td>
                                    </tr>
                                    <tr style="display: inline-block">
                                        <td align="left" width="1%" class="pageTitle" nowrap onclick="location.href='/crm/CRM_ToDoList.aspx?m=25&si=43';"
                                            style="cursor: pointer;">
                                            <twc:LocalizedLiteral Text="Deftxt19" runat="server" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td width="49%" align="left" class="BorderBottomTitles" nowrap onclick="location.href='/crm/CRM_ToDoList.aspx?m=25&si=43';"
                                            style="cursor: pointer;">
                                            <asp:Label ID="RepTaskInfo" runat="server" Visible="false" Class="divautoformRequired" />
                                        </td>
                                        <td width="1%" align="left" class="pageTitle" nowrap onclick="location.href='/crm/CRM_Reminder.aspx?m=25&si=41';"
                                            style="cursor: pointer;">
                                            <twc:LocalizedLiteral Text="CRMdeftxt19" runat="server" />&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td width="49%" align="left" class="BorderBottomTitles" nowrap onclick="location.href='/crm/CRM_Reminder.aspx?m=25&si=41';"
                                            style="cursor: pointer;">
                                            <asp:Label ID="RepeaterActivityInfo" runat="server" Visible="false" Class="divautoformRequired" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table id="AttInDay" runat="server" width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="pageTitle" valign="top">
                                            <twc:LocalizedLiteral Text="Deftxt25" runat="server" />
                                        </td>
                                        <td align="RIGHT" class="BorderBottomTitles">
                                            <asp:LinkButton ID="FilterActivity1" runat="server" CssClass="Save" />
                                            <asp:LinkButton ID="FilterActivity2" runat="server" CssClass="Save" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 5px;" colspan="5">
                                            <asp:Repeater ID="RepeaterActivityDay" runat="server">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="1" width="100%" align="center">
                                                        <tr>
                                                            <td class="GridTitle" width="25%">
                                                                <%=Capitalize(wrm.GetString("Acttxt29"))%>
                                                            </td>
                                                            <td class="GridTitle" width="37%">
                                                                <%=wrm.GetString("Acttxt7").ToUpper()%>
                                                                /<%=wrm.GetString("Acttxt8").ToUpper()%>/<%=wrm.GetString("Acttxt89").ToUpper()%></td>
                                                            <td class="GridTitle" width="18%" nowrap>
                                                                <%=wrm.GetString("Acttxt65")%>
                                                            </td>
                                                            <td class="GridTitle" width="20%">
                                                                <%=wrm.GetString("Acttxt38")%>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="GridItem brs1" width="30%">
                                                            <asp:Label ID="Subject" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="GridItem brs1" width="40%">
                                                            <asp:Literal ID="activitywith" runat="server"></asp:Literal>
                                                        </td>
                                                        <td class="GridItem brs1" width="20%" nowrap>
                                                            <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        </td>
                                                        <td class="GridItem brs1" width="10%" nowrap>
                                                            <asp:Literal ID="AcDate" runat="server" />
                                                            <asp:Literal ID="ExId" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td class="GridItemAltern brs1" width="30%">
                                                            <asp:Label ID="Subject" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="GridItemAltern brs1" width="20%">
                                                            <asp:Literal ID="activitywith" runat="server"></asp:Literal>
                                                        </td>
                                                        <td class="GridItemAltern brs1" style="" width="20%" nowrap>
                                                            <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        </td>
                                                        <td class="GridItemAltern brs1" width="10%" nowrap>
                                                            <asp:Literal ID="AcDate" runat="server" />
                                                            <asp:Literal ID="ExId" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' />
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
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table id="AttLost" width="100%" runat="server" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td align="left" class="pageTitle" valign="top">
                                            <twc:LocalizedLiteral Text="Deftxt26" runat="server" />
                                            <asp:Label ID="PlaceActivityLost" runat="server" CssClass="divautoformRequired" />
                                        </td>
                                        <td align="RIGHT" class="BorderBottomTitles">
                                            <asp:LinkButton ID="FilterActivity3" runat="server" CssClass="Save" />
                                            <asp:LinkButton ID="FilterActivity4" runat="server" CssClass="Save" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 5px;" colspan="2">
                                            <asp:Repeater ID="RepeaterActivityLost" runat="server">
                                                <HeaderTemplate>
                                                    <table border="0" cellpadding="2" cellspacing="1" width="100%" align="center">
                                                        <tr>
                                                            <td class="GridTitle" width="25%">
                                                                <%=Capitalize(wrm.GetString("Acttxt29"))%>
                                                            </td>
                                                            <td class="GridTitle" width="37%">
                                                                <%=wrm.GetString("Acttxt7")%>
                                                                /<%=wrm.GetString("Acttxt8")%>/<%=wrm.GetString("Acttxt89")%></td>
                                                            <td class="GridTitle" width="18%" nowrap>
                                                                <%=wrm.GetString("Acttxt65")%>
                                                            </td>
                                                            <td class="GridTitle" width="20%">
                                                                <%=wrm.GetString("Acttxt38")%>
                                                            </td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="GridItem brs2" width="30%">
                                                            <asp:Label ID="Subject" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="GridItem brs2" width="40%">
                                                            <asp:Literal ID="activitywith" runat="server"></asp:Literal>
                                                        </td>
                                                        <td class="GridItem brs2" width="20%" nowrap>
                                                            <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        </td>
                                                        <td class="GridItem brs2" width="10%" nowrap>
                                                            <asp:Literal ID="AcDate" runat="server" />
                                                            <asp:Literal ID="ExId" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' />
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <tr>
                                                        <td class="GridItemAltern brs2" width="30%">
                                                            <asp:Label ID="Subject" runat="server"></asp:Label>
                                                        </td>
                                                        <td class="GridItemAltern brs2" width="20%">
                                                            <asp:Literal ID="activitywith" runat="server"></asp:Literal>
                                                        </td>
                                                        <td class="GridItemAltern brs2" width="20%" nowrap>
                                                            <%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
                                                        </td>
                                                        <td class="GridItemAltern brs2" width="10%" nowrap>
                                                            <asp:Literal ID="AcDate" runat="server" />
                                                            <asp:Literal ID="ExId" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' />
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
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <span id="SpanBithDate" runat="server">
                                    <table class="tblstruct">
                                        <tr>
                                            <td align="left" class="pageTitle" valign="top">
                                                <twc:LocalizedLiteral Text="Deftxt24" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-bottom: 5px;">
                                                <asp:Repeater ID="ContactBirthDate" runat="server">
                                                    <HeaderTemplate>
                                                        <table border="0" cellspacing="0" cellpadding="2" width="100%" align="center">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td class="GridItem">
                                                                <span onclick="location.href='/CRM/base_contacts.aspx?action=VIEW&full=<%# DataBinder.Eval(Container.DataItem, "id") %>'"
                                                                    class="linked">
                                                                    <%# DataBinder.Eval(Container.DataItem, "REFERENTE")%>
                                                                </span>&nbsp;</td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <AlternatingItemTemplate>
                                                        <tr>
                                                            <td class="GridItemAltern">
                                                                <span onclick="location.href='/CRM/base_contacts.aspx?action=VIEW&full=<%# DataBinder.Eval(Container.DataItem, "id") %>'"
                                                                    class="linked">
                                                                    <%# DataBinder.Eval(Container.DataItem, "REFERENTE")%>
                                                                </span>&nbsp;</td>
                                                        </tr>
                                                    </AlternatingItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <center>
                                                    <asp:Label ID="ContactBirthDateInfo" runat="server" Visible="false" Class="divautoformRequired" /></center>
                                            </td>
                                        </tr>
                                    </table>
                                </span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
function Hide(objid)
{
   var obj = document.getElementById(objid);
   var objbtn = document.getElementById(objid + 'btn');
   if (obj == null) return;
   if (obj.style.display == 'none'){
	obj.style.display = '';
   	objbtn.innerHTML = '<img src=images/up.gif>';
   }else{
	obj.style.display = 'none';
   	objbtn.innerHTML = '<small>' + obj.rows.length + '<small>&nbsp;<img src=images/down.gif>';
   }
}
        </script>

    </form>
</body>
</html>
