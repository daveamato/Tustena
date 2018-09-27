<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<%@ Control Language="c#" Codebehind="ActivityChronology.ascx.cs" Inherits="Digita.Tustena.WorkingCRM.ActivityChronology" EnableViewState="True" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" AutoEventWireup="false" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<asp:Repeater id="RepCro" runat="server" OnItemDataBound="RepCroDataBound" OnItemCommand="RepCroCommand">
	<HeaderTemplate>
		<table border="0" cellspacing="0" width="100%" align="center" class="normal">
			<tr>
				<td class="GridTitle" colspan="2" width="99%">&nbsp;<twc:LocalizedLiteral text="Wortxt17" runat="server"/></td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td class="actGridItem" width="1%">
				<asp:LinkButton id="Expand" CommandName="Expand" runat="server" />&nbsp;
				<asp:Literal id="ExId" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
			</td>
			<td class="actGridItem" width="98%">
				<asp:Label ID="Subject" Runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Literal id="Cronology" runat="server" visible="false" />
			</td>
		</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<tr>
			<td class="ActGridItemAltern" width="1%">
				<asp:LinkButton id="Expand" CommandName="Expand" runat="server" />&nbsp;
				<asp:Literal id="ExId" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
			</td>
			<td class="ActGridItemAltern" width="98%">
				<asp:Label ID="Subject" Runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<asp:Literal id="Cronology" runat="server" visible="false"/>
			</td>
		</tr>
	</AlternatingItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
<Pag:RepeaterPaging id="RepCroPaging" visible=false runat="server" />
