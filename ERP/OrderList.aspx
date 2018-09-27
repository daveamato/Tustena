<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Page language="c#" Codebehind="OrderList.aspx.cs" AutoEventWireup="true" Inherits="Digita.Tustena.ERP.OrderList" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="head" runat=server>
		<script type="text/javascript" src="/js/dynabox.js"></script>
		<title>QuoteList</title>
	</HEAD>
	<body id="body" runat="server">
		<form id="Form1" method="post" runat="server">
			<script>
function OpenSearchBox(e){
				var x;

				if((document.getElementById("CrossWith_0")).checked)
					x=0;
				else if	((document.getElementById("CrossWith_1")).checked)
						x=1;
					 else if ((document.getElementById("CrossWith_2")).checked)
							x=2;
						  else
							x=3;


				switch(x){
					case 0:
						CreateBox('/Common/PopCompany.aspx?render=no&textbox=CrossWithText&textbox2=CrossWithID',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID',e,400,300);
						break;
				}
			}
			</script>
			<table width="100%" border="0" cellspacing="0">
				<tr>
					<td width="140" class="SideBorderLinked" valign="top">
						<table width="98%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td class="sideContainer">
									<div class="sideTitle"><%=wrm.GetString("Options")%></div>
									<asp:linkbutton id="NewQuote" CssClass="sidebtn" Runat="server" onclick="NewQuote_Click" />
								</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
							</tr>
							<tr>
								<td class="sideContainer">
									<div class="sideTitle"><%=wrm.GetString("Quotxt3")%></div>
									<div class="sideInputTitle"><%=wrm.GetString("Esttxt23")%></div>
									<table class="sideFixed" cellspacing="0" cellpadding="0">
										<tr>
											<td>
												<asp:RadioButtonList id="DiffQuoteExpire" runat="server" RepeatDirection="Horizontal">
													<asp:ListItem value="0">&le;</asp:ListItem>
													<asp:ListItem value="1" Selected>&equiv;</asp:ListItem>
													<asp:ListItem value="2">&ge;</asp:ListItem>
												</asp:RadioButtonList>
												<asp:TextBox id="TextBoxSearchQuoteExpire" runat="server" CssClass="BoxDesign"></asp:TextBox>
											</td>
											<td width="22" valign="bottom">
												&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=TextBoxSearchQuoteExpire&amp;Start='+(document.getElementById('TextBoxSearchQuoteExpire')).value,event,195,195)">
											</td>
										</tr>
									</table>
									<div class="sideInputTitle"><%=wrm.GetString("Ordtxt7")%></div>
									<div class="sideInput"><asp:DropDownList id="SearchQuoteStage" runat="server" CssClass="BoxDesign" /></div>
									<div class="sideInputTitle"><%=wrm.GetString("Ordtxt3")%></div>
									<div class="sideInput"><asp:TextBox id="TextboxSearchQuoteNumber" runat="server" CssClass="BoxDesign" /></div>
									<div class="sideInputTitle"><%=wrm.GetString("Quotxt6")%></div>
									<div class="sideInput"><asp:TextBox id="TextboxSearchDescription" runat="server" CssClass="BoxDesign" /></div>
									<div class="sideInputTitle"><%=wrm.GetString("Acttxt65")%></B></div>
									<table class="sideFixed" cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td>
												<asp:TextBox id="TextboxSearchOwnerID" runat="server" style="DISPLAY:none"></asp:TextBox>
												<asp:TextBox id="TextboxSearchOwner" runat="server" Width="100%" CssClass="BoxDesign" ReadOnly="true" />
											</td>
											<td width="22">
												&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&amp;textbox=TextboxSearchOwner&amp;textbox2=TextboxSearchOwnerID',event)">
											</td>
										</tr>
									</table>
									<div class="sideInputTitle"><%=wrm.GetString("Tictxt38")%></div>
									<table class="sideFixed" cellspacing="0" cellpadding="0">
										<tr>
											<td colspan="2">
												<asp:RadioButtonList id="CrossWith" runat="server" cssClass="normal"></asp:RadioButtonList>
												<asp:TextBox ID="CrossWithID" Runat="server" style="DISPLAY:none"></asp:TextBox>
											</td>
										</tr>
										<tr>
											<td width="99%">
												<asp:TextBox ID="CrossWithText" Runat="server" CssClass="BoxDesign" ReadOnly="true" />
											</td>
											<td width="22">
												&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="OpenSearchBox(event)">
											</td>
										</tr>
									</table>
									<div class="sideInputTitle"><%=wrm.GetString("Quotxt9")%></div>
									<table class="sideFixed" cellspacing="0" cellpadding="0" width="100%">
										<tr>
											<td>
												<asp:RadioButtonList id="RadiobuttonlistGrandTotal" runat="server" RepeatDirection="Horizontal">
													<asp:ListItem value="0">&le;</asp:ListItem>
													<asp:ListItem value="1" Selected>&equiv;</asp:ListItem>
													<asp:ListItem value="2">&ge;</asp:ListItem>
												</asp:RadioButtonList>
												<asp:TextBox id="SearchGrandTotal" runat="server" Width="100%" CssClass="BoxDesign"></asp:TextBox>
											</td>
										</tr>
									</table>
									<div class="sideSubmit"><asp:linkbutton id="btnSearch" CssClass="Save" Runat="server" onclick="btnSearch_Click"></asp:linkbutton></div>
								</td>
							</tr>
						</table>
					</td>
					<td valign="top" height="100%" class="pageStyle">
						<table width="100%" border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td align="left" class="pageTitle" valign="top">
									<%=wrm.GetString("Ordtxt1")%>
								</td>
							</tr>
						</table>
						<twc:tustenarepeater id="NewQuoteListRepeater" runat="server" SortDirection="asc" AllowPaging="true"
							AllowAlphabet="true" FilterCol="Subject" AllowSearching="false">
							<HEADERTEMPLATE>
								<twc:REPEATERHEADERBEGIN id="RepeaterHeaderBegin1" runat="server"></twc:REPEATERHEADERBEGIN>

					<TD class="GridTitle" width="1%">&nbsp;</TD>
					<twc:RepeaterColumnHeader id="Repeatercolumnheader1" runat="Server" CssClass="GridTitle" DataCol="quotedate"
						width="15%" Resource="Ordtxt5"></twc:RepeaterColumnHeader>
					<twc:RepeaterColumnHeader id="Repeatercolumnheader2" runat="Server" CssClass="GridTitle" DataCol="Number"
						width="10%" Resource="Quotxt5"></twc:RepeaterColumnHeader>
					<twc:RepeaterColumnHeader id="Repeatercolumnheader3" runat="Server" CssClass="GridTitle" DataCol="Subject"
						width="30%" Resource="Quotxt6"></twc:RepeaterColumnHeader>
					<TD class="GridTitle"><%=wrm.GetString("Quotxt7")%></TD>
					<TD class="GridTitle"><%=wrm.GetString("Quotxt8")%></TD>
					<twc:RepeaterColumnHeader id="Repeatercolumnheader6" runat="Server" CssClass="GridTitle" DataCol="GrandTotal"
						width="10%" Resource="Quotxt9"></twc:RepeaterColumnHeader>
					<twc:RepeaterMultiDelete id="Repeatermultidelete2" runat="server" CssClass="GridTitle" header="true"></twc:RepeaterMultiDelete>
					<twc:REPEATERHEADEREND id="RepeaterHeaderEnd1" runat="server"></twc:REPEATERHEADEREND></HEADERTEMPLATE><ITEMTEMPLATE>
						<TR>
							<TD class="GridItem">
								<asp:LinkButton id="OpenQuote" Runat="server" CommandName="OpenQuote"></asp:LinkButton>
								<asp:Literal id="QuoteID" Runat="server" Visible="False"></asp:Literal></TD>
							<TD class="GridItem">
								<asp:Literal id="QuoteDate" Runat="server"></asp:Literal></TD>
							<TD class="GridItem">
								<asp:Literal id="QuoteNumber" Runat="server"></asp:Literal></TD>
							<TD class="GridItem">
								<asp:Literal id="QuoteDescription" Runat="server"></asp:Literal></TD>
							<TD class="GridItem">
								<asp:Literal id="QuoteCustomer" Runat="server"></asp:Literal></TD>
							<TD class="GridItem">
								<asp:Literal id="QuoteOwner" Runat="server"></asp:Literal></TD>
							<TD class="GridItem" align="right">
								<asp:Literal id="QuoteTotal" Runat="server"></asp:Literal></TD>
							<twc:RepeaterMultiDelete id="DelCheck" runat="server" CssClass="GridItem"></twc:RepeaterMultiDelete></TR>
					</ITEMTEMPLATE><ALTERNATINGITEMTEMPLATE>
						<TR>
							<TD class="GridItemAltern">
								<asp:LinkButton id="OpenQuote" Runat="server" CommandName="OpenQuote"></asp:LinkButton>
								<asp:Literal id="QuoteID" Runat="server" Visible="False"></asp:Literal></TD>
							<TD class="GridItemAltern">
								<asp:Literal id="QuoteDate" Runat="server"></asp:Literal></TD>
							<TD class="GridItemAltern">
								<asp:Literal id="QuoteNumber" Runat="server"></asp:Literal></TD>
							<TD class="GridItemAltern">
								<asp:Literal id="QuoteDescription" Runat="server"></asp:Literal></TD>
							<TD class="GridItemAltern">
								<asp:Literal id="QuoteCustomer" Runat="server"></asp:Literal></TD>
							<TD class="GridItemAltern">
								<asp:Literal id="QuoteOwner" Runat="server"></asp:Literal></TD>
							<TD class="GridItemAltern" align="right">
								<asp:Literal id="QuoteTotal" Runat="server"></asp:Literal></TD>
							<twc:RepeaterMultiDelete id="DelCheck" runat="server" CssClass="GridItem"></twc:RepeaterMultiDelete></TR>
					</ALTERNATINGITEMTEMPLATE></twc:tustenarepeater></TD></tr>
			</table>
		</form>
	</body>
</HTML>
