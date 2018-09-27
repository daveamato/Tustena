<%@ Page language="c#" Codebehind="UpdateMenu.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.UpdateMenu" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head runat=server>
    <title>UpdateMenu</title>
  </HEAD>
  <body>

    <form id="Form1" method="post" runat="server">

		<twc:tustenarepeater id="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true" AllowAlphabet="true" FilterCol="UserAccount" AllowSearching="false">
						<HEADERTEMPLATE>
							<twc:REPEATERHEADERBEGIN id=RepeaterHeaderBegin1 runat="server"></twc:REPEATERHEADERBEGIN>
							<tr>
								<td>user</td>
								<td>password</td>
							</tr>
							<twc:REPEATERHEADEREND id=RepeaterHeaderEnd1 runat="server"></twc:REPEATERHEADEREND>
						</HEADERTEMPLATE>
			        	 <ItemTemplate>
			        		<tr>
								<td><%#DataBinder.Eval(Container.DataItem,"useraccount")%></td>
								<td><%#DataBinder.Eval(Container.DataItem,"PassWord")%></td>
							</tr>
						  </ItemTemplate>
		</twc:tustenarepeater>

		<twc:tustenarepeater id="Tustenarepeater1" runat="server" SortDirection="asc" AllowPaging="true" AllowAlphabet="true" FilterCol="UserAccount" AllowSearching="false">
						<HEADERTEMPLATE>
							<twc:REPEATERHEADERBEGIN id="Repeaterheaderbegin2" runat="server"></twc:REPEATERHEADERBEGIN>
							<tr>
								<td>user</td>
								<td>password</td>
							</tr>
							<twc:REPEATERHEADEREND id="Repeaterheaderend2" runat="server"></twc:REPEATERHEADEREND>
						</HEADERTEMPLATE>
			        	 <ItemTemplate>
			        		<tr>
								<td><%#DataBinder.Eval(Container.DataItem,"useraccount")%></td>
								<td><%#DataBinder.Eval(Container.DataItem,"PassWord")%></td>
							</tr>
						  </ItemTemplate>
		</twc:tustenarepeater>
     </form>

  </body>
</HTML>
