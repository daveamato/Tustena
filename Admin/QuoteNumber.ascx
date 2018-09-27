<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuoteNumber.ascx.cs" Inherits="Digita.Tustena.Admin.QuoteNumber" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>

<table cellpadding=0 cellspacing=0 class=normal width="300">
    <tr>
        <td class="GridTitle" colspan=2><twc:LocalizedLiteral runat=server Text="QuoNumtxt1"></twc:LocalizedLiteral></td>
    </tr>
    <tr>
        <td class="GridItemAltern" width="200"><twc:LocalizedLiteral runat=server Text="QuoNumtxt12"></twc:LocalizedLiteral></td>
        <td class="GridItemAltern" width="100"><asp:CheckBox ID="CheckDisabled" runat=server /></td>
    </tr>
    <tr>
        <td class="GridItem" width="200"><twc:LocalizedLiteral runat=server Text="QuoNumtxt10"></twc:LocalizedLiteral></td>
        <td class="GridItem" width="100"><asp:TextBox ID="NprogText" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="GridItemAltern"><twc:LocalizedLiteral runat=server Text="QuoNumtxt2"></twc:LocalizedLiteral></td>
        <td class="GridItemAltern"><asp:CheckBox ID="CheckDay" runat=server /></td>
    </tr>
    <tr>
        <td class="GridItem"><twc:LocalizedLiteral runat=server Text="QuoNumtxt3"></twc:LocalizedLiteral></td>
        <td class="GridItem"><asp:CheckBox ID="CheckMonth" runat=server /></td>
    </tr>
    <tr>
        <td class="GridItemAltern"><twc:LocalizedLiteral runat=server Text="QuoNumtxt4"></twc:LocalizedLiteral></td>
        <td class="GridItemAltern">
                <table cellpadding=0 cellspacing=0>
                    <tr>
                        <td>
                            <asp:CheckBox ID="CheckYear" runat=server />
                        </td>
                        <td>
                            <asp:RadioButtonList ID="YearDigit" runat=server RepeatDirection=Horizontal CssClass="normal">
                                <asp:ListItem Value=0 Text="YY"></asp:ListItem>
                                <asp:ListItem Value=1 Text="YYYY"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
        </td>
    </tr>
    <tr id="CustomerCode" runat=server>
        <td class="GridItem"><twc:LocalizedLiteral runat=server Text="QuoNumtxt7"></twc:LocalizedLiteral></td>
        <td class="GridItem"><asp:CheckBox ID="CheckCustomerCode" runat=server /></td>
    </tr>
    <tr>
        <td class="GridItemAltern"><twc:LocalizedLiteral runat=server Text="QuoNumtxt8"></twc:LocalizedLiteral></td>
        <td class="GridItemAltern"><asp:TextBox ID="NprogStarttxt" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="GridItem"><twc:LocalizedLiteral runat=server Text="QuoNumtxt9"></twc:LocalizedLiteral></td>
        <td class="GridItem"><asp:DropDownList ID="NprogRestart" runat=server CssClass="BoxDesign"></asp:DropDownList></td>
    </tr>
    <tr>
        <td class="GridItem" colspan=2 align=right style="padding-top:5px">
            <asp:LinkButton ID="btnSave" runat=server CssClass="Save" OnClick="btnSave_Click"></asp:LinkButton>
            <asp:Literal ID="litId" runat=server Visible=false></asp:Literal>
        </td>
    </tr>
</table>
