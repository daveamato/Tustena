<%@ Page Language="c#" Debug="True" trace="false" CodeBehind="webmail.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.WebMail.webmail" %>
<%@ Register TagPrefix="mail" TagName="mailout" Src="~/MailingList/WebMail/mailout.ascx" %>
<html>
<head id="head" runat="server">
	<style type="text/css">
		.PostFrame { BORDER-RIGHT: black 1px solid; PADDING-RIGHT: 10px; BORDER-TOP: black 1px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: black 1px solid; PADDING-TOP: 10px; BORDER-BOTTOM: black 1px solid; BACKGROUND-COLOR: white }
		DIV.PostTitle { FONT-SIZE: 14pt }
		.DeleteButton { FLOAT: right }
		.PostTitle A { BORDER-TOP-WIDTH: 0px; FONT-WEIGHT: bold; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: black; BORDER-RIGHT-WIDTH: 0px; TEXT-DECORATION: none }
		.PostTitle A:active { BORDER-TOP-WIDTH: 0px; FONT-WEIGHT: bold; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: black; BORDER-RIGHT-WIDTH: 0px; TEXT-DECORATION: none }
		.PostTitle A:visited { BORDER-TOP-WIDTH: 0px; FONT-WEIGHT: bold; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; COLOR: black; BORDER-RIGHT-WIDTH: 0px; TEXT-DECORATION: none }
		.PostTitle A:hover { BORDER-TOP-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-RIGHT-WIDTH: 0px; TEXT-DECORATION: underline }
		DIV.PostInfos { FONT-WEIGHT: normal; FONT-SIZE: 8pt; TEXT-TRANSFORM: none; COLOR: #808080 }
		DIV.PostContent { PADDING-RIGHT: 0px; BORDER-TOP: #cbcbcb 1px dotted; PADDING-LEFT: 0px; FONT-SIZE: 10pt; PADDING-BOTTOM: 10px; MARGIN: 10px 0px; PADDING-TOP: 10px; BORDER-BOTTOM: #cbcbcb 1px dotted }
	</style>

	<script language="javascript" src="/js/dynabox.js"></script>
</head>
<body id="body" runat="server">
		<form runat="server">

		<table width="100%" border="0" cellspacing="0">
		<tr>
		<td width="140" class="SideBorderLinked" valign="top">
			<table width="98%" border="0" cellspacing="0" cellpadding=0 align="center">
				<tr>
               		<td class="sideContainer">
					<div class="sideTitle"><%=wrm.GetString("Options")%></div>
					<asp:linkbutton id="MailIn" runat="server" cssclass="sidebtn" />
					<asp:LinkButton Id="NewMail" runat="server" cssClass="sidebtn"/>
					<asp:LinkButton Id="ReadMail" runat="server" cssClass="sidebtn"/>
					</td>
				</tr>
			</table>
		</td>
            <td valign="top" height="100%" class="pageStyle">
                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="pageTitle" valign="top">
						Web Mail
					</td>
				</tr>
			</table>

			<asp:label id="lblError" runat="server" cssClass="normal" Style="COLOR:red" />
			<mail:mailout id="SendNewMail" runat="server"  visible="false"/>
			<asp:panel id="login" runat="server" CssClass="PostFrame" visible="false">
<H1>WebMail login</H1>Mail Server:
<asp:textbox id="mailserver" runat="server"></asp:textbox>
<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" Display="Dynamic" Text="specifica il mail server"
					ControlToValidate="mailserver"></asp:RequiredFieldValidator><BR>UserID:
<asp:textbox id="userid" runat="server"></asp:textbox>
<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" Display="Dynamic" Text="specifica uno userID"
					ControlToValidate="userid"></asp:RequiredFieldValidator><BR>Password:
<asp:textbox id="password" runat="server" TextMode="Password"></asp:textbox>
<asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" Display="Dynamic" Text="specifica una password"
					ControlToValidate="password"></asp:RequiredFieldValidator><BR><INPUT type="submit" value="Login">

</asp:panel>
			<asp:panel id="messages" runat="server" visible="false">

				<p>
				<asp:Label id="Lblmsginfo" runat=server cssClass=normal/>
				</p>

				<asp:Repeater id="Repeatermsg" runat="server">
	 			<HeaderTemplate>
					<table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
					<tr>
					<td class="GridTitle" width="10%" nowrap><%=wrm.GetString("WebMLtxt10")%></td>
					<td class="GridTitle" width="10%" nowrap><%=wrm.GetString("WebMLtxt1")%></td>
					<td class="GridTitle" width="70%" nowrap><%=wrm.GetString("WebMLtxt2")%></td>
					<td class="GridTitle" width="9%" nowrap><%=wrm.GetString("WebMLtxt3")%></td>
					<td class="GridTitle" width="1%" nowrap>&nbsp;</td>
					</tr>
				</HeaderTemplate>
				<ItemTemplate>
					<tr>
					<td class="GridItem" width="10%" nowrap><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgDate").ToString())%></td>
					<td class="GridItem" width="10%" nowrap><asp:Label id="msgFrom" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "from").ToString())%>'></asp:Label></td>
					<td class="GridItem" width="70%" nowrap><asp:LinkButton id="OpenBody" commandname="OpenBody" runat=server text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "subject").ToString())%>'/></td>
					<td class="GridItem" width="9%" nowrap align="right"><asp:Label id="MsgSize" runat="server"/></td>
					<td class="GridItem" width="1%" nowrap>
						<asp:LinkButton id="DeleteMail" commandname="DeleteMail" runat=server cssClass="Save"/>
						<asp:Label id="MsgId" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgID").ToString())%>'/>
						<asp:Label id="MsgSerial" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgSerial").ToString())%>'/>
						<asp:Label id="MsgMessageId" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MessageID").ToString())%>'/>
					</td>
					</tr>
				</ItemTemplate>
				<AlternatingItemTemplate>
					<tr>
					<td class="GridItemAltern" width="10%" nowrap><%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgDate").ToString())%></td>
					<td class="GridItemAltern" width="10%" nowrap><asp:Label id="msgFrom" runat="server" Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "from").ToString())%>'></asp:Label></td>
					<td class="GridItemAltern" width="70%" nowrap><asp:LinkButton id="OpenBody" commandname="OpenBody" runat=server text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "subject").ToString())%>'/></td>
					<td class="GridItemAltern" width="9%" nowrap align="right"><asp:Label id="MsgSize" runat="server"/></td>
					<td class="GridItemAltern" width="1%" nowrap>
						<asp:LinkButton id="DeleteMail" commandname="DeleteMail" runat=server cssClass="Save"/>
						<asp:Label id="MsgId" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgID").ToString())%>'/>
						<asp:Label id="MsgSerial" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MsgSerial").ToString())%>'/>
						<asp:Label id="MsgMessageId" runat="server" visible=false Text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "MessageID").ToString())%>'/>
					</td>
					</tr>
				</AlternatingItemTemplate>
				<FooterTemplate>
					</table>
	 			</FooterTemplate>
				</asp:Repeater>

				<table id="tblpaging" runat=server>
					<tr>
						<td><asp:LinkButton id="PreviousMessPage" runat=server cssClass="Save"/></td>
						<td><asp:Label id="MessPageID" runat="server" visible="false"/>&nbsp;</td>
						<td><asp:LinkButton id="NextMessPage" runat=server cssClass="Save"/></td>
					</tr>
				</table>
				<asp:DataGrid id="dgmessages" runat="server" AlternatingItemStyle-BackColor="lightblue"
					ShowFooter="false" ShowHeader="false" Width="100%" AutoGenerateColumns="false">
					<Columns>
						<asp:TemplateColumn>
							<ItemTemplate>
								<br>

								<div class="PostFrame">
										<table cellpadding=0 Width="99%" cellspacing=0 class="normal">
										<tr>
											<td class="normal" Width="40%">
												<%=wrm.GetString("WebMLtxt1")%>
												<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "from").ToString())%>
												<br>
												<%=wrm.GetString("WebMLtxt4")%>
												<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "to").ToString())%>
											</td>
											<td align="right" Width="60%">
													<div>
														<table cellspacing=2 cellpadding=0 class=normal Width="100%">
															<tr>
																<td><asp:RadioButtonList id="CrossWith" runat="server" cssClass=normal></asp:RadioButtonList></td>
																<td><img src="/i/user.gif" border="0" style="cursor:pointer" onclick="OpenSearchBox(event)"></td>
																<td Width="30%">
																	<asp:TextBox ID="MailAddressToID" Runat="server" style="display:none"></asp:TextBox>
																	<asp:TextBox ID="MailAddressTo" Runat="server" CssClass="BoxDesign"></asp:TextBox>
																</td>
																<td Width="50%">
																	<asp:LinkButton id="CreateActivity" runat=server CommandName="CreateActivity" cssClass="Save"></asp:LinkButton>
																</td>
															</tr>
															<tr>
																<td colspan=4>
																	<asp:CheckBox id="SaveEml" runat=server></asp:CheckBox>
																</td>
															</tr>
														</table>
													</div>
											</td>
										</tr>
										</table>
									<div class="PostTitle"><%=wrm.GetString("WebMLtxt2")%>
										<asp:Label id="mailsubject" runat=server text='<%#Server.HtmlEncode(DataBinder.Eval(Container.DataItem, "subject").ToString())%>'/>
										</b></div>
									<br>
									<div class="PostContent">
										<asp:Label id="mailbody" runat=server/>
										<asp:Label id="mailmsgid" runat=server visible=false text='<%#DataBinder.Eval( Container.DataItem, "MsgID")%>' />
										<br>
									</div>
									<div>
									<asp:Repeater id="RepAttachment" runat="server">
										<HeaderTemplate>
											<table border="0" cellpadding="0" cellspacing="0" width="99%">
											<tr>
												<td class="normal PostContent">
													<b><%=wrm.GetString("Mailtxt6")%></b>
												</td>
											</tr>
										</HeaderTemplate>
										<ItemTemplate>
											<tr>
											<td width="100%" class="normal">
											<a href='/mailinglist/webmail/mailredir.aspx?render=no&att=1&img=<%# DataBinder.Eval(Container.DataItem, "filename")%>' target="_blank"><%# DataBinder.Eval(Container.DataItem, "filename")%></a>
											</td>
											</tr>
										</ItemTemplate>
										<FooterTemplate>
											</table>
										</FooterTemplate>
									</asp:Repeater>
									</div>
								</div>
								<br>
							</ItemTemplate>
						</asp:TemplateColumn>
					</Columns>
				</asp:DataGrid>
			</asp:panel>

			</td>
			</tr>
			</table>
		</form>

		<script>
			function OpenSearchBox(e)
			{
				var x;

				if((document.getElementById(jsControlId+"CrossWith_0")).checked)
					x=0;
				else if	((document.getElementById(jsControlId+"CrossWith_1")).checked)
						x=1;
					 else if ((document.getElementById(jsControlId+"CrossWith_2")).checked)
							x=2;
						  else
							x=3;


				switch(x)
				{
					case 0:
						CreateBox('/Common/PopCompany.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textbox2='+jsControlId+'MailAddressToID&email=1',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textboxID='+jsControlId+'MailAddressToID&email=1',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textboxID='+jsControlId+'MailAddressToID&email=1',e,300);
						break;
				}
			}
		</script>
</body>
</html>

