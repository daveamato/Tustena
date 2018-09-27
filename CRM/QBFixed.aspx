<%@ Page language="c#" Codebehind="QBFixed.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.QBFixed" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head id="head" runat=server>
		<title>QBFixed</title>
	</HEAD>
	<body id="body" runat=server>
		<form id="Form1" method="post" runat="server">
			<asp:Repeater id="QBRepeater" runat="server">
				<HEADERTEMPLATE>
					<TABLE class="normal" cellSpacing="1" cellPadding="2" width="98%" align="center" border="0">
						<TBODY>
							<TR>
								<TD class="GridTitle" colSpan="3"><%=wrm.GetString("QBUtxt29")%></TD>
							</TR>
							<TR>
								<TD class="GridTitle" width="25%"><%=wrm.GetString("QBUtxt21")%></TD>
								<TD class="GridTitle" width="75%"><%=wrm.GetString("QBUtxt22")%></TD>
							</TR>
				</HEADERTEMPLATE>
				<ITEMTEMPLATE>
					<TR>
						<TD class="GridItem" width="25%">
							<asp:Literal id=QueryID runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id=QBDescription runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItem" width="75%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ITEMTEMPLATE>
				<ALTERNATINGITEMTEMPLATE>
					<TR>
						<TD class="GridItemAltern" width="25%">
							<asp:Literal id=QueryID runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id=QBDescription runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItemAltern" width="74%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ALTERNATINGITEMTEMPLATE>
				<FOOTERTEMPLATE>
					</TBODY></TABLE>
				</FOOTERTEMPLATE>
			</asp:Repeater>
			<br>
				<asp:Repeater id="QBRepeaterLead" runat="server">
				<HEADERTEMPLATE>
					<TABLE class="normal" cellSpacing="1" cellPadding="2" width="98%" align="center" border="0">
						<TBODY>
							<TR>
								<TD class="GridTitle" colSpan="3"><%=wrm.GetString("QBUtxt30")%></TD>
							</TR>
							<TR>
								<TD class="GridTitle" width="25%"><%=wrm.GetString("QBUtxt21")%></TD>
								<TD class="GridTitle" width="75%"><%=wrm.GetString("QBUtxt22")%></TD>
							</TR>
				</HEADERTEMPLATE>
				<ITEMTEMPLATE>
					<TR>
						<TD class="GridItem" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItem" width="75%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ITEMTEMPLATE>
				<ALTERNATINGITEMTEMPLATE>
					<TR>
						<TD class="GridItemAltern" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItemAltern" width="74%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ALTERNATINGITEMTEMPLATE>
				<FOOTERTEMPLATE>
					</TBODY></TABLE>
				</FOOTERTEMPLATE>
			</asp:Repeater>
			<br>
			<asp:Repeater id="QBRepeaterActivity" runat="server">
				<HEADERTEMPLATE>
					<TABLE class="normal" cellSpacing="1" cellPadding="2" width="98%" align="center" border="0">
						<TBODY>
							<TR>
								<TD class="GridTitle" colSpan="3"><%=wrm.GetString("QBUtxt31")%></TD>
							</TR>
							<TR>
								<TD class="GridTitle" width="25%"><%=wrm.GetString("QBUtxt21")%></TD>
								<TD class="GridTitle" width="75%"><%=wrm.GetString("QBUtxt22")%></TD>
							</TR>
				</HEADERTEMPLATE>
				<ITEMTEMPLATE>
					<TR>
						<TD class="GridItem" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItem" width="75%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ITEMTEMPLATE>
				<ALTERNATINGITEMTEMPLATE>
					<TR>
						<TD class="GridItemAltern" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItemAltern" width="74%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ALTERNATINGITEMTEMPLATE>
				<FOOTERTEMPLATE>
					</TBODY></TABLE>
				</FOOTERTEMPLATE>
			</asp:Repeater>
			<br>
			<asp:Repeater id="QBRepeaterOpportunity" runat="server">
				<HEADERTEMPLATE>
					<TABLE class="normal" cellSpacing="1" cellPadding="2" width="98%" align="center" border="0">
						<TBODY>
							<TR>
								<TD class="GridTitle" colSpan="3"><%=wrm.GetString("QBUtxt32")%></TD>
							</TR>
							<TR>
								<TD class="GridTitle" width="25%"><%=wrm.GetString("QBUtxt21")%></TD>
								<TD class="GridTitle" width="75%"><%=wrm.GetString("QBUtxt22")%></TD>
							</TR>
				</HEADERTEMPLATE>
				<ITEMTEMPLATE>
					<TR>
						<TD class="GridItem" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItem" width="75%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ITEMTEMPLATE>
				<ALTERNATINGITEMTEMPLATE>
					<TR>
						<TD class="GridItemAltern" width="25%">
							<asp:Literal id="QueryID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>' Visible="false" />
							<asp:Literal id="RmString" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"rm")%>' Visible="false" />
							<ASP:LINKBUTTON id="QBDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Title")%>' CommandName="QBDescription" /></TD>
						<TD class="GridItemAltern" width="74%">
							<asp:Literal id=QBDescFull runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>&nbsp;
						</TD>
					</TR>
				</ALTERNATINGITEMTEMPLATE>
				<FOOTERTEMPLATE>
					</TBODY></TABLE>
				</FOOTERTEMPLATE>
			</asp:Repeater>
		</form>
	</body>
</HTML>
