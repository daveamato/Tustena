<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectPrintReport.aspx.cs" Inherits="Digita.Tustena.Project.ProjectPrintReport" %>
<%@ Register TagPrefix="gantt" TagName="Gantt" Src="~/project/Gantt.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body topmargin=0 leftmargin=0 onload="self.print();">
    <form id="form1" runat="server">
    <div>
    <gantt:Gantt ID="Gantt1" runat=server></gantt:Gantt>
    <asp:Literal ID="lblPrint" runat=server></asp:Literal>
    </div>
    </form>
</body>
</html>
