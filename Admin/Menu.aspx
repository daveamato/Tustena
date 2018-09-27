<%@ Page Language="c#" Trace="false" codebehind="Menu.aspx.cs" Inherits="Digita.Tustena.Admin_Menu" AutoEventWireup="false"%>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>

<html>
<head id="head" runat="server">

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server" >
<table width="100%" cellspacing="0">
<tr>
<td width="140" class="SideBorderLinked" valign="top"  runat=server visible=false>

	<table width="100%" border="0">
	<tr>
		<td align="left" class="BorderBottomTitles">
			<span class="divautoform"><b><twc:LocalizedLiteral Text="Amntxt1" runat="server" /></b></span>
		</td>
	</tr>
	</table>

</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
                        <twc:LocalizedLiteral Text="Amntxt1" runat="server" />
				<br>
			</td>
		</tr>
		<tr>
		<td><br>
		<asp:Literal id="HelpLabel" runat="server"/>
		</td>
		</tr>
		<tr>
			<td align="left">
<asp:DataGrid id="Menu_Grid"
	width="100%"
	align="center"
	runat="server"
	cssClass="GridItem"
	AutoGenerateColumns="false"
	CellPadding="0"
	OnItemDataBound="Menu_Grid_ItemDataBound"
	OnItemCommand="Menu_Grid_ItemCommand">
	<HeaderStyle cssClass="GridTitle"></HeaderStyle>
	<SelectedItemStyle backcolor="#00C0C0"></SelectedItemStyle>
	<Columns>
	    <asp:TemplateColumn ItemStyle-VerticalAlign="top">
	    	<ItemStyle cssClass="GridItem GridItemBorderBottom Bar1"/>
	        <ItemTemplate>
	            <asp:Literal id="IDMenu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
	            <asp:Label id="MenuName" runat="server"/>
	        </ItemTemplate>
	    </asp:TemplateColumn>
	    <asp:TemplateColumn ItemStyle-VerticalAlign="top" ItemStyle-Width="30%">
	    	<ItemStyle cssClass="GridItem GridItemBorderBottom" />
	        <ItemTemplate>
	            <asp:Literal id="LtrDip" runat="server"   />
	        </ItemTemplate>
	    </asp:TemplateColumn>
	    <asp:TemplateColumn HeaderText="" ItemStyle-VerticalAlign="top" ItemStyle-Width="5%">
	    	<ItemStyle cssClass="GridItem GridItemBorderBottom" />
	        <ItemTemplate>
	            <asp:LinkButton id="LnkEdt" runat="server" CommandName="Edit" CausesValidation="false" />
	        </ItemTemplate>
	    </asp:TemplateColumn>
	    <asp:TemplateColumn ItemStyle-VerticalAlign="top">
	        <ItemTemplate>
			<asp:DataGrid runat="server"
				id="SubMenu_Grid"
				width="100%"
				align="center"
				cssClass="GridItem""
				AutoGenerateColumns="false"
				CellPadding="0"
				OnItemDataBound="SubMenu_Grid_ItemDataBound"
				OnItemCommand="Menu_Grid_ItemCommand"
				DataSource='<%# getSubMenuDataSource( (int)DataBinder.Eval(Container.DataItem, "ID") ) %>'
				>
				<HeaderStyle cssClass="GridTitle"></HeaderStyle>
				<Columns>
	    			<asp:TemplateColumn HeaderText='<%=wrm.GetString("Amntxt1") %>' runat="server"  ItemStyle-VerticalAlign="top">
	    				<ItemStyle cssClass="GridItem Bar3" />
	        			<ItemTemplate>
	            				<asp:Literal id="IdSubMenu" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false" />
	            				<asp:Literal id="NameSubMenu" runat="server"/>
	        			</ItemTemplate>
	        		</asp:TemplateColumn>
	        		<asp:TemplateColumn HeaderText='<%=wrm.GetString("Amntxt2")%>' ItemStyle-VerticalAlign="top" ItemStyle-Width="70%">
	        			<ItemStyle cssClass="GridItem" />
	        			<ItemTemplate>
	            				<asp:Literal id="LtrDipSub" runat="server"   />
	        			</ItemTemplate>
	    			</asp:TemplateColumn>
	    			<asp:TemplateColumn ItemStyle-Width="5%">
	    				<ItemStyle cssClass="GridItem" />
	        			<ItemTemplate>
	            				<asp:LinkButton id="LnkEdtSub" runat="server" Text='<%=wrm.GetString("Amntxt8")%>' CommandName="SEdit" CausesValidation="false" />
	        			</ItemTemplate>
	    			</asp:TemplateColumn>
				</Columns>
				</asp:DataGrid>

	    	</ItemTemplate>
	    </asp:TemplateColumn>
	</Columns>
</asp:DataGrid>
<p>
<asp:Table id="Groups_Table" width="90%" align="center" runat="server" CellPadding="4" visible="false">
	<asp:TableRow>
		<asp:TableCell colspan="3" >
			<div class="divautoform"><twc:LocalizedLiteral Text="Amntxt4" runat="server" /></div>
			<asp:Label id="MenuText" runat="server" cssclass="normal12"/>
		</asp:TableCell>
	</asp:TableRow>
	<asp:TableRow>
        <asp:TableCell width="45%" >
        	<div class="divautoform"><twc:LocalizedLiteral Text="Amntxt5" runat="server" /></div>
    		<asp:ListBox id="ListGroups" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
        </asp:TableCell>
        <asp:TableCell align="center" width="10%">
	<table>
            <tr>
                <td>
                    <asp:button id="Btn_FwwAll"  runat="server" text=">>" Cssclass="btn" ToolTip="Copia tutti i groups"></asp:button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button id="Btn_Fww"  runat="server" text=">" Cssclass="btn" ToolTip="Copia solo i groups selezionati"></asp:button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button id="Btn_Rww"  runat="server" text="<" Cssclass="btn" ToolTip="Rimuovi solo i groups selezionati"></asp:button>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:button id="Btn_RwwAll"  runat="server" text="<<" Cssclass="btn" ToolTip="Rimuovi tutti i groups"></asp:button>
                </td>
            </tr>
        </table>
        </asp:TableCell>
        <asp:TableCell width="45%">
        	<div class="divautoform"><twc:LocalizedLiteral Text="Amntxt6" runat="server" /></div>
    		<asp:ListBox id="ListDip" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
		<asp:TableCell colspan="3">
			<asp:Button id="Submit" runat="server" class="BtnSubmit" />
		</asp:TableCell>
    </asp:TableRow>
    <asp:TableRow>
		<asp:TableCell colspan="3">
			<asp:Label id="LblError" runat="server" class="normal" />
		</asp:TableCell>
    </asp:TableRow>
</asp:Table>
</p>
			</td>
		</tr>
	</table>
</td>
</tr>
</table>
</form>

</body>
</html>
