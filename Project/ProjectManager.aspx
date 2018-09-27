<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectManager.aspx.cs" Inherits="Digita.Tustena.Project.ProjectManager" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="hibrid" Namespace="Digita.Tustena.HibridControls" Assembly="HibridControls" %>
<%@ Register TagPrefix="GControl" TagName="GroupControl" Src="~/Common/GroupControl.ascx" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
    <script type="text/javascript" src="/js/dynabox.js"></script>
    <title>Untitled Page</title>
</head>
<body id="body" runat=server>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                </td>
                <td valign="top" height="100%" class="pageStyle">
                <twc:TustenaTabber ID="Tabber" Width="840" runat="server">
                 <twc:TustenaTab ID="visProject" ClientSide="true" runat="server" Header="Progetto">
                    <hibrid:HibridContainer ID=hibridc Columns=2 RWMode="ReadWrite" runat=server width="100%">
                        <hibrid:HibridTextBox ID=HibridTextBox0 Width="98%" Label="Title" Text=xxx runat=server cssclass="BoxDesign"></hibrid:HibridTextBox>
                        <hibrid:HibridTextBox ID=HibridTextBox1 Label="Description" TextMode="Multiline" Text=xxx runat=server cssclass="BoxDesign" style="height:50px"></hibrid:HibridTextBox>
                        <hibrid:HibridDateBox ID=PlannedStartDate Label="Data inizio pianificata" Text="20/03/1974" runat=server cssclass="BoxDesign"></hibrid:HibridDateBox>
                        <hibrid:HibridDateBox ID=RealStartDate Label="Data inizio reale" Text="20/03/1974" runat=server cssclass="BoxDesign"></hibrid:HibridDateBox>
                        <hibrid:HibridDateBox ID=PlannedEndDate Label="Data fine pianificata" Text="20/03/1974" runat=server cssclass="BoxDesign"></hibrid:HibridDateBox>
                        <hibrid:HibridDateBox ID=RealEndDate Label="Data fine reale" Text="20/03/1974" runat=server cssclass="BoxDesign"></hibrid:HibridDateBox>

                        <hibrid:HibridTitle Label="SICUREZZA" ID="pip" runat=server></hibrid:HibridTitle>
                        <hibrid:HibridLinkBox ID="HibridLinkBox" Label="Proprietario" Text="" runat=server LinkClick="CreateBox('/common/PopAccount.aspx?render=no&textbox=TextboxSearchOwner&textbox2=TextboxSearchOwnerID',event)" cssclass="BoxDesign"></hibrid:HibridLinkBox>
                        <hibrid:HibridCustom ID="HGroups" runat=Server colspan=2>
                            <GControl:GroupControl runat="server" ID="Groups" />
                        </hibrid:HibridCustom>
                    </hibrid:HibridContainer>
                  </twc:TustenaTab>
                  <twc:TustenaTab ID="visSections" ClientSide="true" runat="server" Header="Sezioni">
                  </twc:TustenaTab>
                </twc:TustenaTabber>
                </td>
            </tr>
    </table>


    </form>
</body>
</html>
