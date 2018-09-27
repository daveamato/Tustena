<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MergeContacts.aspx.cs" Inherits="Digita.Tustena.CRM.MergeContacts" %>
<%@ Register TagPrefix="Merge" TagName="Merge" Src="~/Common/Merge.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <Merge:Merge ID="merge" runat=server />
    </div>
    </form>
</body>
</html>
