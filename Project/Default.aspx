<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.Project.ProjectsList" %>
<%@ Register TagPrefix="gantt" Namespace="Digita.Tustena.Project" Assembly="Tustena.Project" %>
<html>
<head id="head" runat="server">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

</head>
<body id="body" runat="server">
<form id="Form1" method="post" runat="server">
			    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td width="140" class="SideBorderLinked" valign="top">
                <table width="98%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td align="left" class="BorderBottomTitles" valign="top">
                            <span class="divautoform"><b>
                                CHART
                            </b></span>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:linkbutton id="LbnNew" runat="server" cssclass="HeaderContacts linked" />
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table width="98%" border="0" cellspacing="0" align="center" cellpadding="0">
                    <tr>
                        <td align="left" class="BorderBottomTitles" cellspacing="0" cellpadding="0" valign="top">
							<gantt:GanttCalendarControl id="GanttControl" runat="server" Font-Size="8pt" Font-Names="Arial"></gantt:GanttCalendarControl>
						</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
	</form>

</body>
</html>
