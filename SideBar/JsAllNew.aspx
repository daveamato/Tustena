<%@ Page language="c#" Codebehind="JsAllNew.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.SideBar.JsAllNew" %>
function NewItemLaunch(sw) { switch(sw) {
<%=casecode()%>
} window.location=url; } function NewItemLaunchMenu() { var txt="";
<%=menucode()%>
showmenu(null,txt) } NewItemLaunchMenu();
