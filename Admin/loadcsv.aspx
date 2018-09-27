<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Page Language="c#" AutoEventWireup="false" trace="false" Inherits="Digita.Tustena.ImportExternalData" CodeBehind="loadcsv.aspx.cs" %>
<html>
<head id="head" runat="server">
<script type="text/javascript" src="/js/dynabox.js"></script>
<twc:LocalizedScript resource="Csvtxt31" runat="server" />
<script>
function expand(obj){
if(obj.src.indexOf('Down')>0){
document.getElementById('dataform').style.height='500px';
obj.src="/images/up.gif";
}else{
document.getElementById('dataform').style.height='';
obj.src="/images/down.gif";
}
}
function wait()
{
		DisableSaveBtn();
		NonBlockingAlert(Csvtxt31);
}


function NoResubmit()
{
			AddPostbackFunction("wait()");
}

function ShowProgressBar()
	{
		var progress = document.getElementById('divProgressBar');
		progress.style.display='';
	}

 SafeAddOnload(NoResubmit);
</script>

</head>
<body id="body" runat="server">
<form id="Form1" runat="server">
	<table width="100%" border="0" cellspacing="0">
		<tr>
			<td width="140" class="SideBorderLinked" valign="top" runat=server visible=false>
				&nbsp;
			</td>
			<td valign="top" height="100%"><![if !IE]>
				<link rel="stylesheet" type="text/css" href="/css/tabControl_all.css">
	<![endif]><!--[if IE]><LINK media=screen href="/css/tabControl.css"
      type=text/css rel=stylesheet><![endif]-->

	<table id="tabControl" class="tblstruct" bgcolor="#ffffff" style="CURSOR:default">
						<tr>
							<td id="tdTab1" runat="server" class="TabHorizontal_HeaderSelected" width="15%" nowrap valign="bottom">
								<img src="/i/import.gif">&nbsp;<b><twc:LocalizedLiteral Text="Csvtxt1" runat="server" id="LocalizedLiteral1" /></b>
							</td>
							<td class="TabHorizontal_Space" align="right" nowrap>
								&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="2" class="TabHorizontal_Content" valign="top">
								<table width="98%" border="0" cellspacing="0" align="center">
									<tr>
										<td valign="top">
											<asp:Literal id="HelpLabel" runat="server" />
											<br>
											<div id="UploadForm" runat="server">
												<asp:label id="LblFile" runat="server" Font-Bold="True" cssClass="normal" Text="CSV file:" />
												<Upload:InputFile id="filMyFile" runat="server" class="BoxDesign" Width="300"/>
												<br>
					 						      <div id="divProgressBar" style="DISPLAY:none">
					 						      <UPLOAD:PROGRESSBAR Inline=true Height="20px" Width="100%"
                                                  id=inlineProgressBar runat="server"
                                                  Triggers="CmdSend"></Upload:ProgressBar>

												  </div>
												<br>
												<div class="normal"><twc:LocalizedLiteral Text="Csvtxt2" runat="server" id="LocalizedLiteral2" /></div>
												<asp:RadioButtonList id="TableImport" runat="server" cssClass="normal" />
												<br>
												<div class="normal"><twc:LocalizedLiteral Text="Csvtxt5" runat="server" id="LocalizedLiteral3" /></div>
												<asp:RadioButtonList id="CsvType" runat="server" cssClass="normal">
													<asp:ListItem Value="PV" Selected="True">[;] (Outlook Express, Microsoft Excel 2003)</asp:ListItem>
													<asp:ListItem Value="V">[,] (Microsoft Excel 2000, Microsoft Outlook)</asp:ListItem>
													<asp:ListItem Value="TB">[TAB] (Microsoft Outlook)</asp:ListItem>
												</asp:RadioButtonList>
												<br>
												<asp:LinkButton id="CmdSend"  runat="server" cssClass="save" />
											</div>
											<p>
												<asp:Label id="LblInfo" runat="server" Visible="true" cssClass="normal"></asp:Label>
											</p>
											<span id="Results" runat="server" visible="false">
												<div id="dataform" runat="server" class="normal" style="BORDER-RIGHT:silver 1px solid; BORDER-TOP:silver 1px solid; OVERFLOW:auto; BORDER-LEFT:silver 1px solid; WIDTH:600px; BORDER-BOTTOM:silver 1px solid">
													<table width="100%">
														<TBODY>
															<tr>
																<td class="normal"><img src="/images/down.gif" title="Expand" onclick="expand(this)">&nbsp;<twc:LocalizedLiteral Text="Csvtxt6" runat="server" id="LocalizedLiteral4" /></td></td>
									</tr>
								</table>
								<ASP:Datagrid id="table" runat="server" cssClass="normal">
									<HeaderStyle font-bold="True" wrap="False" backcolor="#E1E1E1"></HeaderStyle>
									<PagerStyle visible="False"></PagerStyle>
									<SelectedItemStyle wrap="False"></SelectedItemStyle>
									<AlternatingItemStyle backcolor="#E0E0E0"></AlternatingItemStyle>
									<ItemStyle wrap="False"></ItemStyle>
								</ASP:Datagrid></DIV></SPAN><br>
							</td>
						</tr>
						<tr>
							<td width="100%" colspan="3">
								<div id="Matchdiv" runat="server" class="normal">
									<table width="100%" cellpadding="0" cellspacing="0" class="normal">
										<TBODY>
											<tr>
												<td>
													<asp:Label id="LblMatch" runat="server" Visible="true" cssClass="normal"></asp:Label>
													<br>
													<table id="tablecat" runat="server" width="600" cellpadding="0" cellspacing="0" class="normal">
														<TBODY>
															<tr>
																<td valign="top">
																	<b>
																		<twc:LocalizedLiteral Text="Csvtxt23" runat="server" id="LocalizedLiteral5" /></b>
																</td>
															</tr>
															<tr>
																<td valign="top">
																	<asp:Repeater id="RepCategories" runat="server">
																		<HeaderTemplate>
																			<div class="ListCategory">
																				<table border="0" cellpadding="0" cellspacing="0" width="100%">
																		</HeaderTemplate>
																		<ItemTemplate>
																			<tr>
																				<td width="5%">
																					<asp:CheckBox id="Check" runat="server" />
																					<asp:Literal id="IDCat" runat="server" text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' visible="false"/>
																				</td>
																				<td width="90%" class="normal">
																					<%# DataBinder.Eval(Container.DataItem, "Description")%>
																				</td>
																			</tr>
																		</ItemTemplate>
																		<FooterTemplate>
													</table>
								</div>
								</FooterTemplate> </asp:Repeater>
							</td>
						</tr>
					</table>
			</td>
		</tr>
	</table>
	<br>
	<asp:LinkButton id="CmdLoadData" runat="server" class="save"  /></DIV></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
</form>

</body>
</html>
