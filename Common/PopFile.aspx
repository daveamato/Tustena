<%@ Page language="c#" Codebehind="PopFile.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.PopFile" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
  <head runat=server>
    <title>PopFile</title>
	<link rel="stylesheet" type="text/css" href="/css/G.css">
  </head>
	<script language="javascript" src="/js/common.js"></script>
	<script>
		function copyData(id, category, txtcat, txtcatid, toclick)
		{
			(document.getElementById(txtcatid)).value = id;
			(document.getElementById(txtcat)).value = category;
			try{
				if(toclick.length>0){
					clickElement(document.getElementById(toclick));
				}
			}catch(e){}

		}
	</script>
  <body bgcolor="#e5e5e5" leftmargin="0" topmargin="2" marginwidth="0" marginheight="0" >
    <form id="Form1" method="post" runat="server">
		<table width="100%" border="0" cellspacing="0">
			<tr>
				<td width="200" class="SideBorderLinked" valign="top">
					<table width="98%" border="0" cellspacing="0" align="center">
						<tr>
							<td align="left" width="100">
								<asp:TextBox id="FindIt" autoclick="Find" runat="server" class="BoxDesign" />
							</td>
						</tr>
						<tr>
							<td align="left" >
								<asp:LinkButton id="Find" runat="server" class="save"/>
							</td>
						</tr>
						<tr>
							<td>
								<ie:treeview id="tvCategoryTreeSearch" runat="server" HoverStyle="background:#F2F2F2;color:black" DefaultStyle="font-family:Verdana;font-size:10px"
									SelectedStyle="background:#FFA500;color:black" SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:treeview>

								<asp:TextBox id="CategoryTextSearch" runat=server style="display:none"/>
								<asp:TextBox id="CategoryIdSearch" runat=server style="display:none"/>
							</td>
						</tr>
					</table>
			</td>
			<td valign="top">
					<asp:Repeater id="FileRep" runat="server">
						<HeaderTemplate>
							<table class="tblstruct normal">
								<tr>
									<td class="GridTitle" width="99%"><%=wrm.GetString("Dsttxt2")%></td>
									<td class="GridTitle" width="1%">R</td>
								</tr>
						</HeaderTemplate>
						<ItemTemplate>
							<tr>
								<td class="GridItem">
									<span title='<%# DataBinder.Eval(Container.DataItem, "description") %>' onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "id") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "filename"))%>')" class="linked"><%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "filename"))%></span>
								</td>
								<td class="GridTitle" width="1%"><%# DataBinder.Eval(Container.DataItem, "ReviewNumber") %></td>
							</tr>
						</ItemTemplate>
						<AlternatingItemTemplate>
							<tr>
								<td class="GridItemAltern">
									<span title='<%# DataBinder.Eval(Container.DataItem, "description") %>' onclick="SetRef('<%# DataBinder.Eval(Container.DataItem, "id") %>','<%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "filename"))%>')" class="linked"><%# ParseJSString((string)DataBinder.Eval(Container.DataItem, "filename"))%></span>
								</td>
								<td class="GridTitle" width="1%"><%# DataBinder.Eval(Container.DataItem, "ReviewNumber") %></td>
							</tr>
						</AlternatingItemTemplate>
						<FooterTemplate>
							</table>
						</FooterTemplate>
					</asp:Repeater>
				</td>
			</tr>
		</table>
    </form>
  </body>
</html>
