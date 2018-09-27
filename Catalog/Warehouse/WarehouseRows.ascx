<%@ Control Language="C#" Codebehind="WarehouseRows.ascx.cs"
    Inherits="Digita.Tustena.Catalog.Warehouse.WarehouseRows" %>
<script type="text/javascript" src="/js/autodate.js"></script>
<table cellpadding=0 cellspacing=0 width="800" >
    <tr>
        <td Class="GridTitle">
            <asp:Literal ID="ProductName" runat=server></asp:Literal>
        </td>
    </tr>
</table>
<asp:DataGrid ID="WareHouseEditor" Font-Size="10px" runat="server" ShowFooter="True"
    ForeColor="Black" GridLines="None" CellPadding="2" CellSpacing="1" BackColor="#aaaaaa"
    BorderWidth="1px" BorderColor="#F2F2F2" Width="800px" AutoGenerateColumns="False"
    DataKeyField="id">
    <ItemStyle CssClass="GridItem"></ItemStyle>
    <AlternatingItemStyle CssClass="GridItemAltern"></AlternatingItemStyle>
    <HeaderStyle CssClass="GridTitle"></HeaderStyle>
    <FooterStyle BackColor="#DDDDDD"></FooterStyle>
    <Columns>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="code1" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
                <asp:Literal ID="id" runat="server" Visible="False"></asp:Literal>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newcode1" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="code2" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newcode2" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="code3" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newcode3" runat="server" Width="80px" CssClass="inputautoform"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="available" runat="server" Width="30px" CssClass="inputautoform"
                    onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newavailable" runat="server" Width="30px" CssClass="inputautoform"
                    onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="ordered" runat="server" Width="30px" CssClass="inputautoform" onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newordered" runat="server" Width="30px" CssClass="inputautoform"
                    onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="expectedarrival" runat="server" Width="80px" CssClass="inputautoform"
                    onkeypress="DataCheck(this,event)"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newexpectedarrival" runat="server" Width="80px" CssClass="inputautoform"
                    onkeypress="DataCheck(this,event)"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:TextBox ID="realarrival" runat="server" Width="80px" CssClass="inputautoform"
                    onkeypress="DataCheck(this,event)"></asp:TextBox>
            </ItemTemplate>
            <FooterTemplate>
                <asp:TextBox ID="Newrealarrival" runat="server" Width="80px" CssClass="inputautoform"
                    onkeypress="DataCheck(this,event)"></asp:TextBox>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemTemplate>
                <asp:RadioButtonList ID="status" runat="server" Width="80px" CssClass="inputautoform">
                </asp:RadioButtonList>
            </ItemTemplate>
            <FooterTemplate>
                <asp:RadioButtonList ID="Newstatus" runat="server" Width="80px" CssClass="inputautoform">
                </asp:RadioButtonList>
            </FooterTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <ItemStyle HorizontalAlign="Center"></ItemStyle>
            <ItemTemplate>
                <asp:CheckBox runat="server" ID="chkDelete"></asp:CheckBox>
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Center"></FooterStyle>
            <FooterTemplate>
                <asp:LinkButton runat="server" CommandName="Add" ID="Linkbutton1" NAME="Linkbutton1"
                    CssClass="save"></asp:LinkButton>
                    <br />
                <asp:LinkButton runat="server" CommandName="Modify" ID="Linkbutton2" NAME="Linkbutton1"
                    CssClass="save"></asp:LinkButton>
            </FooterTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>

