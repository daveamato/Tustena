<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AllNew.ascx.cs" Inherits="Digita.Tustena.SideBar.AllNew" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script>
loadJsScript("/js/menudigita.js","menudigita");
loadJsScript("/js/tooltip.js","tooltip");
</script>
<a href="javascript:loadScript('/SideBar/JsAllNew.aspx')" class="sideBtn sideWithsub" onmouseover="javascript:loadScript('/SideBar/JsAllNew.aspx')" onMouseout="dhm()" nowrap><asp:Literal ID="LtrText" Runat="server"></asp:Literal></a>
