<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>
<%@ Page Language="c#" AutoEventWireup="false" trace="false" codebehind="FreeFields.aspx.cs" Inherits="Digita.Tustena.FreeFileds"%>
<html>
<head id="head" runat="server">
<script language="javascript1.2">
function ShowHideItems(obj){
	var ItemsTitle = document.getElementById("ItemsTitle");
	var ItemsBody = document.getElementById("ItemsBody");

	if (obj.options[obj.selectedIndex].value=="4"){
		ItemsTitle.style.display="";
		ItemsBody.style.display="";
	} else {
		ItemsTitle.style.display="none";
		ItemsBody.style.display="none";
	}
}
	var TaBary = new Array("tdTab1","tdTab2","tdTab3");

	function ViewHideTabs(objName)
	{
		var obj = document.getElementById(objName);
		for (var i = 0; i < TaBary.length; i++) {
			var Tabobj = document.getElementById(TaBary[i]);
			document.getElementById("tdTab"+(i+1)).className = (obj==Tabobj)?'TabHorizontal_HeaderSelected':'TabHorizontal_Header';
		}
	}

</script>

</head>
<body id="body" runat="server">
<form runat="server">
	<table width="100%" height="100%" border="0" cellspacing="0">
		<TBODY>
			<tr>
				<td width="140" height="100%" class="SideBorderLinked" valign="top" runat=server visible=false>
					<table width="98%" border="0" cellspacing="0" cellpadding="0">
						<tr>
                   		<td class="sideContainer">
						<div class="sideTitle"><twc:LocalizedLiteral Text="Fretxt1" runat="server" id="LocalizedLiteral1" /></div>
                        <asp:linkbutton id="FreeForContact" runat="server" cssclass="sidebtn" />
                        <asp:linkbutton id="FreeForReferenti" runat="server" cssclass="sidebtn" />
                        <asp:linkbutton id="FreeForLeads" runat="server" cssclass="sidebtn" />
							</td>
						</tr>
					</table>
				</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
                                <twc:LocalizedLiteral Text="Fretxt1" runat="server" />
                        </td>
                    </tr>
                </table><br>				<![if !IE]>	<link rel="stylesheet" type="text/css" href="/css/tabControl_all.css">
	<![endif]><!--[if IE]><LINK media=screen href="/css/tabControl.css"
      type=text/css rel=stylesheet><![endif]-->
						<table id="tabControl" runat="server" width="100%" cellspacing=0 cellpadding=0 bgcolor="#ffffff" style="CURSOR:default">
							<TBODY>
								<tr>
									<td id="tdTab1" runat="server" class="TabHorizontal_HeaderSelected" width="15%" nowrap>
										<asp:LinkButton id="FreeForContact2" runat="server" class="normal linked" />&nbsp;
									</td>
									<td id="tdTab2" runat="server" class="TabHorizontal_Header" width="10%" nowrap>
										<asp:LinkButton id="FreeForReferenti2" runat="server" class="normal linked" />&nbsp;
									</td>
									<td id="tdTab3" runat="server" class="TabHorizontal_Header" width="10%" nowrap>
										<asp:LinkButton id="FreeForLeads2" runat="server" class="normal linked" />&nbsp;
									</td>
									<td class="TabHorizontal_Space" align="right" nowrap>
										&nbsp;</td>
								</tr>
								<tr>
									<td colspan="4" class="TabHorizontal_Content" valign="top">
										<table class="tblstruct">
											<tr>
												<td><asp:Literal id="HelpLabel" runat="server" /></td>
											</tr>
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
													<table class="tblstruct">
														<tr>
															<td class="GridTitle" width="20%"><twc:LocalizedLiteral Text="Fretxt13" runat="server" /></td>
															<td class="GridTitle" width="10%"><twc:LocalizedLiteral Text="Fretxt14" runat="server" /></td>
															<td class="GridTitle" width="30%"><twc:LocalizedLiteral Text="Fretxt16" runat="server" /></td>
															<td class="GridTitle" width="20%"><twc:LocalizedLiteral Text="Fretxt19" runat="server" /></td>
															<td class="GridTitle" width="10%"><twc:LocalizedLiteral Text="Fretxt20" runat="server" /></td>
															<td class="GridTitle" width="10%">&nbsp;</td>
														</tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr>
														<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "name") %></td>
														<td class="GridItem"><asp:Label id="TypeDescription" runat="server" /></td>
														<td class="GridItem"><asp:Label id="FieldGroups" runat="server" /></td>
														<td class="GridItem"><asp:Label id="FieldParam" runat="server" /></td>
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
														<td class="GridItemAltern"><asp:Label id="TypeDescription" runat="server" /></td>
														<td class="GridItemAltern"><asp:Label id="FieldGroups" runat="server" /></td>
														<td class="GridItemAltern"><asp:Label id="FieldParam" runat="server" /></td>
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
					</FooterTemplate> </asp:Repeater></CENTER></td>
			</tr>
		</TBODY></table>
	</TD></TR></TBODY></TABLE>
	<asp:Literal id="SomeJS" runat="server" />
</form>

</body>
</html>
