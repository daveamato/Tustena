<%@ Page language="c#" Codebehind="qbtable.aspx.cs" Inherits="Digita.Tustena.CRM.qbtable" trace="false" AutoEventWireup="false" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat=server>
		<title>qbtable</title>
		<style>
	.bwdh { WIDTH: 18px; TEXT-ALIGN: center }
	.qbbtn { CURSOR: pointer }
	.qbbtn2 { CURSOR: default }
		</style>
		<link rel="stylesheet" type="text/css" href="/css/G.css">
		<script language="javascript" src="/js/common.js"></script>
		<script type="text/javascript" src="/js/dynabox.js"></script>
		<script src="/js/qbtable.js" language=javascript></script>
	</HEAD>
	<script>
	function selectadd(selecter,selectertxt)
	{
		if (selecter.options[selecter.selectedIndex].value=="add")
		{
			selecter.style.display="none";
			selectertxt.style.display="inline";
		}
	}

	</script>
	<body onload="init();parent.active=true;">
		<form id="Form1" method="post" runat="server">

			<table id="QBSearchStep1" runat="server" width="100%" border="0" cellspacing="0" class="normal">
			<tr>
				<td width="100%" colspan="3">
				<div><%=wrm.GetString("QBUtxt9")%>
				<domval:RequiredDomValidator ID="QBQueryNameValidator" Display=Dynamic ControlToValidate="QBQueryName" ErrorMessage="*" Runat="server"/>
				</div>
				<asp:TextBox id="QBQueryName" runat="server" class="BoxDesign"/>
				<asp:TextBox id="QBQueryID" runat="server" visible=false/>
				</td>
			</tr>
			<tr style="display:inline">
				<td width="100%" colspan="3">
				<div><%=wrm.GetString("QBUtxt27")%></div>
				<asp:TextBox id="HiddenNewCategory" runat="server" class="BoxDesign" style="display:none"/>
				<select id="QBQueryCategory" runat="server" class="BoxDesign"></select>

				</td>
			</tr>
			<tr>
				<td width="100%" colspan="3">
				<div><%=wrm.GetString("QBUtxt10")%>
				</div>
				<asp:TextBox id="QBQueryDescription" runat="server" class="BoxDesign" TextMode="Multiline"/>
				</td>
			</tr>
			</table>
			<asp:Repeater ID="Repqbtable" Runat=server>
				<HeaderTemplate>
					<TABLE id="QBTable" width="100%" cellSpacing="1" cellPadding="0" border="0">
					<TR>
						<TD class="GridTitle" nowrap><%=wrm.GetString("QBUtxt13")%></TD>
						<TD class="GridTitle" nowrap><%=wrm.GetString("QBUtxt14")%></TD>
						<TD class="GridTitle" colSpan="6" nowrap><%=wrm.GetString("QBUtxt15")%></TD>
						<TD class="GridTitle" width="40%" nowrap><%=wrm.GetString("QBUtxt16")%></TD>
						<TD class="GridTitle" width="1%" nowrap>&nbsp;</TD>
					</TR>
				</HeaderTemplate>
				<ItemTemplate>
					<TR id="<%#DataBinder.Eval(Container.DataItem,"trid")%>">
						<TD class="GridItem">
						<asp:Literal ID="LabelID" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"trid")%>' visible=false></asp:Literal>
						<INPUT id='opt<%#DataBinder.Eval(Container.DataItem,"trid")%>' name='opt<%#DataBinder.Eval(Container.DataItem,"trid")%>' type="hidden" value='<%#DataBinder.Eval(Container.DataItem,"Options")%>'>
						<asp:Literal ID="FieldName" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"label")%>'></asp:Literal>
						<asp:Literal ID="FieldType" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"fieldtype")%>' Visible=False></asp:Literal>
						</TD>
						<TD class="GridItem">
							<INPUT class="BoxDesign" id='afl<%#DataBinder.Eval(Container.DataItem,"trid")%>' name='afl<%#DataBinder.Eval(Container.DataItem,"trid")%>' type="text" value='<%#DataBinder.Eval(Container.DataItem,"fieldlabel")%>'>
						</TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='upp<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="moverow(this,0)" alt='<%=wrm.GetString("QBTabtxt9")%>' src="/i/qbi/up0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='dwn<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="moverow(this,1)" alt='<%=wrm.GetString("QBTabtxt10")%>' src="/i/qbi/dwn0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='vis<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="View(this)" alt='<%=wrm.GetString("QBTabtxt11")%>' src="/i/qbi/vis0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='grp<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="GroupBy(this)" alt='<%=wrm.GetString("QBTabtxt12")%>' src="/i/qbi/grp0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn0" id='cpr<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="Principal(this)" alt='<%=wrm.GetString("QBTabtxt13")%>' src="/i/qbi/cpr0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='par<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="Param(this)" alt='<%=wrm.GetString("QBTabtxt14")%>' src="/i/qbi/par0.gif"></TD>
						<TD class="GridItem">
							<asp:Label ID="LblParameter" Runat=server Width="100%"></asp:Label>
						</TD>
						<TD class="GridItem">
							<asp:LinkButton id="DelCommand"	CommandName="DelCommand" runat="server" Text="<img class=qbbtn border=0 src=/i/qbi/del.gif>"></asp:LinkButton>
						</TD>
					</TR>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<TR id="<%#DataBinder.Eval(Container.DataItem,"trid")%>">
						<TD class="GridItemAltern">
							<asp:Literal ID="LabelID" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"trid")%>' visible=false></asp:Literal>
							<INPUT id='opt<%#DataBinder.Eval(Container.DataItem,"trid")%>' name='opt<%#DataBinder.Eval(Container.DataItem,"trid")%>' type="hidden" value='<%#DataBinder.Eval(Container.DataItem,"Options")%>'>
							<asp:Literal ID="FieldName" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"label")%>'></asp:Literal>
							<asp:Literal ID="FieldType" Runat=server Text='<%#DataBinder.Eval(Container.DataItem,"fieldtype")%>' Visible=False></asp:Literal>
							</TD>
						<TD class="GridItemAltern">
							<INPUT class="BoxDesign" id='afl<%#DataBinder.Eval(Container.DataItem,"trid")%>' name='afl<%#DataBinder.Eval(Container.DataItem,"trid")%>' type="text" value='<%#DataBinder.Eval(Container.DataItem,"fieldlabel")%>'>
						</TD>
						<TD class="GridItemAltern bwdh"><IMG class="qbbtn" id='upp<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="moverow(this,0)" alt='<%=wrm.GetString("QBTabtxt9")%>' src="/i/qbi/up0.gif"></TD>
						<TD class="GridItemAltern bwdh"><IMG class="qbbtn" id='dwn<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="moverow(this,1)" alt='<%=wrm.GetString("QBTabtxt10")%>' src="/i/qbi/dwn0.gif"></TD>
						<TD class="GridItem bwdh"><IMG class="qbbtn" id='vis<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="View(this)" alt='<%=wrm.GetString("QBTabtxt11")%>' src="/i/qbi/vis0.gif"></TD>
						<TD class="GridItemAltern bwdh"><IMG class="qbbtn" id='grp<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="GroupBy(this)" alt='<%=wrm.GetString("QBTabtxt12")%>' src="/i/qbi/grp0.gif"></TD>
						<TD class="GridItemAltern bwdh"><IMG class="qbbtn0" id='cpr<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="Principal(this)" alt='<%=wrm.GetString("QBTabtxt13")%>' src="/i/qbi/cpr0.gif"></TD>
						<TD class="GridItemAltern bwdh"><IMG class="qbbtn" id='par<%#DataBinder.Eval(Container.DataItem,"trid")%>' onclick="Param(this)" alt='<%=wrm.GetString("QBTabtxt14")%>' src="/i/qbi/par0.gif"></TD>
						<TD class="GridItemAltern">
							<asp:Label ID="LblParameter" Runat=server Width="100%"></asp:Label>
						</TD>
						<TD class="GridItemAltern">
							<asp:LinkButton id="DelCommand"	CommandName="DelCommand" runat="server" Text="<img class=qbbtn border=0 src=/i/qbi/del.gif>"></asp:LinkButton>
						</TD>
					</TR>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</TABLE>
				</FooterTemplate>
			</asp:Repeater>
			<table width="100%" cellSpacing="1" cellPadding="0" border="0">
				<tr>
				<td align=right>
					<asp:LinkButton ID="SaveReport" runat="server" cssClass="normal"/>
				</td>
				</tr>
			</table>
			<asp:Label id="QBSaveInfo" runat="server" cssClass="divautoformRequired"/>
			<input name=AddNewRow id=AddNewRow type=hidden>
			<asp:LinkButton id=NewRow Runat=server></asp:LinkButton>
		</form>

	</body>
</HTML>
