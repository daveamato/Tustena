<%@ Page Language="c#" Trace="false" codebehind="PopLead.aspx.cs" Inherits="Digita.Tustena.Common.PopLead"  AutoEventWireup="false"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<html>
<head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
</head>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="1" marginwidth="0" marginheight="0" >
<script language="javascript" src="/js/common.js"></script>



<form runat="server" ID="Form1">
<asp:Literal id="SomeJS" runat="server" />
<table width="98%" border="0" cellspacing="3" align="center">
	<tr>
		<td class=normal valign=top>
			<asp:RadioButtonList id="RadioList1" runat="server" cssClass="normal"></asp:RadioButtonList>
		</td>
		<td colspan=2 class=normal valign=top>
			<%=wrm.GetString("Paztxt6")%>
			<asp:RadioButtonList id="NRes" runat="server" cssClass="normal">
				<asp:ListItem Value="10" selected="true">10</asp:ListItem>
				<asp:ListItem Value="20">20</asp:ListItem>
				<asp:ListItem Value="50">50</asp:ListItem>
				<asp:ListItem Value="100">100</asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr >
		<td align="left" width="80%">
			<asp:TextBox id="FindIt" autoclick="Find" runat="server" class="BoxDesign" />
		</td>
		<td align="left" nowrap>
			<asp:LinkButton id="Find" runat="server" class="save"  />
		</td>
		<td align="right"  nowrap>
			<asp:LinkButton id="NewRef" runat="server" class="save"  />
		</td>
	</tr>
</table>
<asp:Repeater id="ContactReferrer" runat="server" >
<HeaderTemplate>
	<br>
	<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
	<tr>
	<td class="GridTitle"><%=wrm.GetString("Prftxt1")%></td>
	<td class="GridTitle"><%=wrm.GetString("Prftxt6")%></td>
	</tr>
</HeaderTemplate>
<ItemTemplate>
	<tr>
	<td class="GridItem"><span onclick="SetRef('<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "REFERENTE")))%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "CompanyName")))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# DataBinder.Eval(Container.DataItem, "companyid")%>','<%# DataBinder.Eval(Container.DataItem, "email")%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "REFERENTE")%></span>&nbsp;</td>
	<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%>&nbsp;</td>
	</tr>
</ItemTemplate>
<AlternatingItemTemplate>
	<tr>
	<td class="GridItemAltern"><span onclick="SetRef('<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "REFERENTE")))%>','<%# ParseJSString(Convert.ToString(DataBinder.Eval(Container.DataItem, "CompanyName")))%>','<%# DataBinder.Eval(Container.DataItem, "ID")%>','<%# DataBinder.Eval(Container.DataItem, "companyid")%>','<%# DataBinder.Eval(Container.DataItem, "email")%>')" class="linked"><%# DataBinder.Eval(Container.DataItem, "REFERENTE")%></span>&nbsp;</td>
	<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "CompanyName")%>&nbsp;</td>
	</tr>
</AlternatingItemTemplate>
<FooterTemplate>
	</table>
</FooterTemplate>
</asp:Repeater>

<table id="NewReferrer" runat="server" border="0" cellpadding="0" cellspacing="0" width="98%" class="normal" align="left">
	<tr>
		<td align="left" class="BorderBottomTitles" style="padding-top: 20px;" colspan="2">
			<span class="divautoform"><b><%=wrm.GetString("Pldtxt2")%></b></span>
		</td>
	</tr>
	<tr>
		<td width="40%" valign="top">
		<%=wrm.GetString("Prftxt8")%>
		<domval:RequiredDomValidator id="RequiredFieldValidatorSurname" runat="server" ControlToValidate="Surname" ErrorMessage="*" />
		</td>
		<td>
			<asp:TextBox Id="Surname" runat="server" Cssclass="BoxDesign" onKeyPress="FirstUp(this,event)"/>
		</td>
	</tr>
	<tr>
		<td width="40%" valign="top">
		<%=wrm.GetString("Prftxt7")%>
		</td>
		<td>
			<asp:TextBox Id="Name" runat="server" Cssclass="BoxDesign" onKeyPress="FirstUp(this,event)"/>
		</td>
	</tr>
		<tr>
		<td width="40%" valign="top">
		<%=wrm.GetString("Pldtxt3")%>
		</td>
		<td>
			<asp:TextBox Id="CompanyName" runat="server" Cssclass="BoxDesign" onKeyPress="FirstUp(this,event)"/>
		</td>
	</tr>
	<tr>
		<td align="right" colspan="2">
			<asp:LinkButton Id="RapSubmit" runat="server" Cssclass="save"  />
		</td>
	</tr>
</table>



</form>
</body>
</html>
