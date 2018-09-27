<%@ Page language="c#" Codebehind="PopConvertLead.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.CRM.PopConvertLead" %>

<HTML>
  <head runat=server>
<title>:: Tustena ::</title>
<link rel="stylesheet" type="text/css" href="/css/G.css">
<script language="javascript" src="/js/common.js"></script>
<script type="text/javascript" src="/js/dynabox.js"></script>
<script>
function CheckConvert(){
	 var rad = document.getElementById("RadioButtonList1_3");
	if(rad==null)
		rad = document.getElementById("RadioButtonList1_2")
	if(rad==null)
		rad = document.getElementById("RadioButtonList1_0")
	 var azid = document.getElementById("TextboxSearchCompanyID");
	 if(rad.checked){
		if(azid.value.length<=0){
			alert('<%=wrm.GetString("Ledtxt35")%>');
			return false;
		}else{
			return true;
			}
	 }else{
	 	return true;
	 }
}
function SetCompany(c,id){
	var aztx = document.getElementById("TextboxSearchCompany");
	var azid = document.getElementById("TextboxSearchCompanyID");
	aztx.value=c;
	azid.value=id;
}
</script>
</HEAD>
<body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
		<form id="Form1" method="post" runat="server">
			<asp:RadioButtonList id="RadioButtonList1" runat="server" CssClass="normal"></asp:RadioButtonList>
			<asp:TextBox id="TextboxSearchCompanyID" runat="server" style="DISPLAY:none"></asp:TextBox>
			<table width="70%" cellspacing="0" cellpadding="3" id="SearchTable" runat=server>
				<tr>
					<td>
						<asp:TextBox id="TextboxSearchCompany" runat="server" Width="100%" CssClass="BoxDesign"></asp:TextBox>
					</td>
					<td width="50" >
						<asp:LinkButton id="FindCompany" runat=server cssClass="Save"/>
					</td>
				</tr>
			</table>
			<table width="98%" cellspacing="0" cellpadding="0">
				<tr>
					<td align="right" >
						<asp:LinkButton id="BtnConvert" runat="server" cssClass="Save"/>
					</td>
				</tr>
			</table>
			<asp:Repeater id="RepCompany" runat="server">
				 <HeaderTemplate>
					<table cellspacing="0" cellpadding="0" width="80%" align=center>
					<tr>
						<td class="GridTitle"><%=wrm.GetString("Paztxt1")%></td>
					</tr>
				 </HeaderTemplate>
				 <ItemTemplate>
					<tr>
					<td class="GridItem"><span id="CompanyRep" runat=server class="linked"/>&nbsp;</td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr>
					<td class="GridItemAltern"><span id="CompanyRep" runat=server class="linked"/>&nbsp;</td>
					</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
				</FooterTemplate>

			</asp:Repeater>
		</form>
	</body>
</HTML>
