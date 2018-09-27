<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" AutoEventWireup="true" Codebehind="ConfigFreeFields.ascx.cs" Inherits="Digita.Tustena.Admin.ConfigFreeFields" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<twc:jscontrolid id="jsc" runat=server Identifier="Fre"/>
<script type="text/javascript">
function ShowHideItems(obj){
	var ItemsTitle = document.getElementById(jsControlIdFre+"ItemsTitle");
	var ItemsBody = document.getElementById(jsControlIdFre+"ItemsBody");

	if (obj.options[obj.selectedIndex].value=="4"){
		ItemsTitle.style.display="";
		ItemsBody.style.display="";
	} else {
		ItemsTitle.style.display="none";
		ItemsBody.style.display="none";
	}
}
</script>

<table width="100%">
	<tr>
		<td align="right">
			<asp:LinkButton ID="BtnNew" runat="server" class="save" />
		</td>
	</tr>
</table>
<table id="AddContactFields" border="0" cellspacing="0" width="80%" align="center" class="normal"
	runat="server">
	<tr>
		<td width="20%"><twc:LocalizedLiteral Text="Fretxt13" runat="server" id="LocalizedLiteral2" /></td>
		<td width="30%"><asp:TextBox ID="Field" runat="server" class="BoxDesign" /></td>
		<td width="50%">&nbsp;</td>
	</tr>
	<tr>
		<td width="20%"><twc:LocalizedLiteral Text="Fretxt20" runat="server" id="LocalizedLiteral3" /></td>
		<td width="30%"><asp:TextBox ID="ViewOrderField" runat="server" class="BoxDesign" /></td>
		<td width="50%">&nbsp;</td>
	</tr>
	<tr>
		<td width="20%"><twc:LocalizedLiteral Text="Fretxt14" runat="server" id="LocalizedLiteral4" /></td>
		<td width="30%">
			<asp:DropDownList ID="Type" runat="server" old="true" class="BoxDesign" />
		</td>
		<td width="50%">&nbsp;</td>
	</tr>
	<tr>
		<td width="20%"><twc:LocalizedLiteral Text="Fretxt17" runat="server" id="LocalizedLiteral5" /></td>
		<td width="30%">
			<asp:DropDownList ID="FieldRef" runat="server" old="true" class="BoxDesign" AutoPostBack="true" OnSelectedIndexChanged="FieldRef_Change" />
		</td>
		<td width="50%">&nbsp;</td>
	</tr>
	<tr>
		<td width="20%"><twc:LocalizedLiteral Text="Fretxt18" runat="server" id="LocalizedLiteral6" /></td>
		<td width="30%">
			<asp:DropDownList ID="FieldRefValue" runat="server" old="true" class="BoxDesign" />
		</td>
		<td width="50%">&nbsp;</td>
	</tr>
	<tr id="ItemsTitle" style="DISPLAY:none">
		<td width="100%" colspan="3"><twc:LocalizedLiteral Text="Fretxt15" runat="server" id="LocalizedLiteral7" /></td>
	</tr>
	<tr id="ItemsBody" style="DISPLAY:none">
		<td width="100%" colspan="3">
			<asp:TextBox ID="SelectItem" TextMode="Multiline" runat="server" class="BoxDesign" Height="30" />
		</td>
	</tr>
	<tr>
		<td colspan="3">
			<GControl:GroupControl runat="server" id="Groups" />
		</td>
	</tr>
	<tr>
		<td colspan="3" align="right">
			<asp:LinkButton ID="SubmitContact" runat="server" class="save" />
			<asp:TextBox ID="FieldID" runat="server" class="BoxDesign" visible="false" Text="-1" />
		</td>
	</tr>
	<tr>
		<td colspan="3">
			<asp:Label ID="ContactInfo" runat="server" class="divautoformRequired" />&nbsp;
			<br>
		</td>
	</tr>
</table>
<center>
	<asp:Repeater id="RepContacts" runat="server">
		<HeaderTemplate>
			<table cellpadding=0 cellspacing=0 width="100%">
				<tr>
					<td class="GridTitle" width="20%">
						<twc:LocalizedLiteral Text="Fretxt13" runat="server" ID="Localizedliteral1" /></td>
					<td class="GridTitle" width="10%">
						<twc:LocalizedLiteral Text="Fretxt14" runat="server" ID="Localizedliteral8" /></td>
					<td class="GridTitle" width="30%">
						<twc:LocalizedLiteral Text="Fretxt16" runat="server" ID="Localizedliteral9" /></td>
					<td class="GridTitle" width="20%">
						<twc:LocalizedLiteral Text="Fretxt19" runat="server" ID="Localizedliteral10" /></td>
					<td class="GridTitle" width="10%">
						<twc:LocalizedLiteral Text="Fretxt20" runat="server" ID="Localizedliteral11" /></td>
					<td class="GridTitle" width="10%">&nbsp;</td>
				</tr>
		</HeaderTemplate>
		<ItemTemplate>
			<tr>
				<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "name") %></td>
				<td class="GridItem">
					<asp:Label id="TypeDescription" runat="server" /></td>
				<td class="GridItem">
					<asp:Label id="FieldGroups" runat="server" /></td>
				<td class="GridItem">
					<asp:Label id="FieldParam" runat="server" /></td>
				<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "vieworder") %></td>
				<td class="GridItem Lcit" nowrap>
					<asp:LinkButton id="ModField" runat="server" CommandName="ModField" />&nbsp;
					<asp:LinkButton id="DelField" runat="server" CommandName="DelField" />
					<asp:Literal id="FieldID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false"/>
				</td>
			</tr>
		</ItemTemplate>
		<AlternatingItemTemplate>
			<tr>
				<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "name") %></td>
				<td class="GridItemAltern">
					<asp:Label id="TypeDescription" runat="server" /></td>
				<td class="GridItemAltern">
					<asp:Label id="FieldGroups" runat="server" /></td>
				<td class="GridItemAltern">
					<asp:Label id="FieldParam" runat="server" /></td>
				<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "vieworder") %></td>
				<td class="GridItemAltern Lcit" nowrap>
					<asp:LinkButton id="ModField" runat="server" CommandName="ModField" />&nbsp;
					<asp:LinkButton id="DelField" runat="server" CommandName="DelField" />
					<asp:Literal id="FieldID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>' visible="false"/>
				</td>
			</tr>
		</AlternatingItemTemplate>
		<FooterTemplate>
			</table>
		</FooterTemplate>
	</asp:Repeater></center>


