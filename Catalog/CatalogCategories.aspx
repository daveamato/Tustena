<%@ Page language="c#" Codebehind="CatalogCategories.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Catalog.CatalogCategories" %>
<%@ Register TagPrefix="ie" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<html>
<head id="head" runat="server">
<script>
function copyData(id, category, parent, del, mail)
{
	(document.getElementById("TxtIdCategory")).value = id;
	(document.getElementById("TxtTextCategory")).value = category;
	(document.getElementById("TxtEmailOwner")).value = mail;
	var dropparent = (document.getElementById("DropParent"));
	dropparent.options.selectedIndex=-1;
	var optionCounter;
	for (optionCounter = 0; optionCounter < dropparent.length; optionCounter++)
	{
		if(dropparent.options[optionCounter].value==parent){
			dropparent.options[optionCounter].selected=true;
			break;
			}
	}
	if(del==1){
		(document.getElementById("DelCategories")).style.display="none";
		(document.getElementById("DelCategoriesInfo")).style.display="inline";
	}
	else{
		(document.getElementById("DelCategories")).style.display="inline";
		(document.getElementById("DelCategoriesInfo")).style.display="none";
	}
}
</script>


</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
	<table width="100%" border="0" cellspacing="0">
		<tr>
			<td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
                        <%=wrm.GetString("Cattxt1")%>
						</td>
					</tr>
				</table>
				<table width="700" border="0" cellspacing="0" cellpadding="0">
					<tr>
						<td width="40%" valign="top">
							<ie:treeview id="tvCategoryTree" runat="server" HoverStyle="background:#F2F2F2;color:black" DefaultStyle="font-family:Verdana;font-size:10px"
								SelectedStyle="background:Gold;color:black" SystemImagesPath="/webctrl_client/1_0/treeimages"></ie:treeview>
						</td>
						<td width="60%" valign="top">
							<table width="100%" border="0" cellspacing="0" cellpadding="0" class="normal">
							<tr>
								<td>
								<div><%=wrm.GetString("Cattxt3")%></div>
								<asp:textbox id="TxtIdCategory" Runat="server" style="display:none"/>
								<asp:textbox id="TxtTextCategory" Runat="server" cssClass="BoxDesign"/>
								</td>
							</tr>
							<tr>
								<td>
								<div><%=wrm.GetString("Cattxt12")%></div>
								<asp:textbox id="TxtEmailOwner" Runat="server" CssClass="BoxDesign"/>
								</td>
							</tr>

							<tr>
								<td>
									<div><%=wrm.GetString("Cattxt4")%></div>
									<asp:DropDownList id="DropParent" old="true" runat="server" cssClass="BoxDesign"></asp:DropDownList>
								</td>
							</tr>
							<tr>
								<td>
									<asp:LinkButton id="AddCategories" runat="server" cssClass="save"/>
									&nbsp;
									<asp:LinkButton id="ModCategories" runat="server" cssClass="save"/>
									&nbsp;
									<asp:LinkButton id="DelCategories" runat="server" cssClass="save"/>
									<span id="DelCategoriesInfo" style="display:none;color:red;"><%=wrm.GetString("Cattxt11")%></span>
								</td>
							</tr>
							<tr>
								<td>
									<br>
									<asp:label id="LblMessage" Runat="server" CssClass="divautoformRequired" />
								</td>
							</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>

    </form>

</body>
</html>
