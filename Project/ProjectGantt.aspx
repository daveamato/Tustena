<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectGantt.aspx.cs" Inherits="Digita.Tustena.Project.ProjectGantt" %>
<%@ Register TagPrefix="gantt" TagName="Gantt" Src="~/project/Gantt.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <title>Untitled Page</title>
</head>
<script>
    function PrintGantt(p)
    {
        var mypage = "/project/projectprintreport.aspx?render=no&Report=0&Prj="+p;
        NewWindow(mypage, '', 600, 500, 'yes');
    }

    function EditSection(s)
    {
        var mypage = "/project/popeditsection.aspx?render=no&Sec="+s;
        NewWindow(mypage, '', 600, 500, 'yes');
    }
</script>
<body id="body" runat=server>
    <form id="form1" runat="server">
    <asp:Literal ID="litPrint" runat=server></asp:Literal>
    <div>
    <gantt:Gantt ID="Gantt1" runat=server></gantt:Gantt>
    </div>
    <div>
    <asp:Literal id="litForecast" runat=server></asp:Literal>
    </div>
    </form>
</body>
</html>
