<%@ Page language="c#" Codebehind="quickactivity.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.qactivity" %>
<%@ Register TagPrefix="qac" TagName="QuickActivity" Src="~/WorkingCRM/QuickActivity.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<head id="head" runat=server>
<link rel="stylesheet" type="text/css" href="/css/G.css">
<LINK REL="SHORTCUT ICON" HREF="/tustena.ico">

<script type="text/javascript" src="/js/common.js"></script>
<script type="text/javascript" src="/js/autodate.js"></script>
<script type="text/javascript" src="/js/dynabox.js"></script>
<script>
function OpenActivity(id){
	//parent.location="/WorkingCRM/AllActivity.aspx?m=25&si=38&Ac=" + id;
	NewWindow('/WorkingCRM/PopActivity.aspx?render=no&ac=' + id,'Activity','750','450','no')
}
function refreshlog()
{
	parent.refreshquicklog();
}
</script>
</head>
	<body id="body" runat=server>
		<form id="Form1" method="post" runat="server">
				<table id="ActivityTable" runat=server cellpadding=0 cellspacing=0 width="100%">
					<tr>
						<td width="50%" valign="top">
							<div class="GridTitle"><twc:LocalizedLiteral text="Acttxt29" runat="server"/></div>
							<qac:QuickActivity id="QuickActivity1" runat=server/>
						</td>
						<td width="5px">&nbsp;</td>
						<td width="50%" valign="top" id="tdOpportunity" runat="server">
							<div class="GridTitle"><twc:LocalizedLiteral text="Acttxt31" runat="server"/></div>
							<table border="0" cellpadding="0" cellspacing="3" width="99%" class="normal" align="left">
								<tr>
									<td width="100%">
										<div><twc:LocalizedLiteral text="CRMopptxt3" runat=server/></div>
										<asp:DropDownList id="CompanyNewStateList" old="true" runat="server" class="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="100%">
										<div><twc:LocalizedLiteral text="CRMopptxt4" runat=server/></div>
										<asp:DropDownList id="CompanyNewPhaseList" old="true" runat="server" class="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="100%">
										<div><twc:LocalizedLiteral text="CRMopptxt5" runat=server/></div>
										<asp:DropDownList id="CompanyNewProbList" old="true" runat="server" class="BoxDesign" />
									</td>
								</tr>
								<tr>
									<td width="100%">
										<div><twc:LocalizedLiteral text="Mottxt6" runat=server/></div>
										<asp:DropDownList id="CompanyLostReasons" old="true" runat="server" class="BoxDesign"/>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>


				<asp:Repeater id="RepeaterActivityDay" runat="server" >
				<HeaderTemplate>
					<table border="0" cellspacing="0" width="100%" align="center" class="normal">
						<tr>
							<td class="GridTitle" width="25%"><%=Capitalize(wrm.GetString("Acttxt29"))%></td>
							<td class="GridTitle" width="19%"><%=wrm.GetString("Acttxt63")%></td>
							<td class="GridTitle" width="18%"><%=wrm.GetString("Acttxt64")%></td>
							<td class="GridTitle" width="18%" nowrap><%=wrm.GetString("Acttxt65")%></td>
							<td class="GridTitle" width="20%"><%=wrm.GetString("Acttxt38")%></td>
						</tr>
				</HeaderTemplate>
				<ItemTemplate>
						<tr>
							<td class="GridItem" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="30%">
								<asp:Literal ID="Subject" Runat=server></asp:Literal>
							</td>
							<td class="GridItem" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%">
								<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
							</td>
							<td class="GridItem" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%">
								<%#DataBinder.Eval(Container.DataItem,"ContactName")%>
							</td>
							<td class="GridItem" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%" nowrap>
								<%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
							</td>
							<td class="GridItem" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="10%" nowrap>
								<asp:Literal id="AcDate" runat="server"/>
								<asp:Literal id="ExId" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
							</td>
						</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
						<tr>
							<td class="GridItemAltern" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="30%">
								<asp:Literal ID="Subject" Runat=server></asp:Literal>
							</td>
							<td class="GridItemAltern" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%">
								<%#DataBinder.Eval(Container.DataItem,"CompanyName")%>
							</td>
							<td class="GridItemAltern" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%">
								<%#DataBinder.Eval(Container.DataItem,"ContactName")%>
							</td>
							<td class="GridItemAltern" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;" width="20%" nowrap>
								<%#DataBinder.Eval(Container.DataItem,"OwnerName")%>
							</td>
							<td class="GridItemAltern" style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none;wrap:no;" width="10%" nowrap>
								<asp:Literal id="AcDate" runat="server"/>
								<asp:Literal id="ExId" runat="server" visible=false Text='<%#DataBinder.Eval(Container.DataItem,"id")%>'/>
							</td>
						</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>
			</asp:Repeater>
			<asp:Label id="LitRepeaterActivityDayInfo" runat="server" cssclass="normal" style="color:red"/>
		</form>
	</body>
</HTML>
