<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ActivityForm.ascx.cs" Inherits="Digita.Tustena.WorkingCRM.ActivityForm" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
	var formID = "<%=this.ID%>";
</script>
<script type="text/javascript" src="/js/activityform.js"></script>
<asp:Panel id="PanelActivity" runat="server">
<TABLE cellSpacing=1 cellPadding=1 width="99%">
  <TR>
    <TD>
      <TABLE cellSpacing=1 cellPadding=1 width="99%">
        <TR>
          <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" vAlign=top
colSpan=2>
            <DIV>
            <TABLE class=normal cellSpacing=0 cellPadding=0>
              <TR>
                <TD>
<asp:Literal id=LabelTypeActivity runat="server" visible="false"></asp:Literal><twc:LocalizedLiteral text="Acttxt6" runat="server"/></TD>
                <TD>
<asp:RadioButtonList id=Activity_InOut runat="server" RepeatColumns="2"></asp:RadioButtonList></TD>
                <TD>
<asp:RadioButtonList id=Activity_ToDo runat="server" RepeatColumns="2" cssClass="normal"></asp:RadioButtonList></TD>
                <TD>
<asp:CheckBox id=SendMail CssClass="normal" TextAlign="Right" Runat="server"></asp:CheckBox></TD>
                <TD>&nbsp;<INPUT class=BoxDesign id=destinationEmail
                  style="DISPLAY: none; WIDTH: 150px" type=text
                  name=destinationEmail runat="server">
            </TD></TR></TABLE></DIV>
<asp:TextBox id=TextboxObject runat="server" CssClass="BoxDesign" startfocus jumpret="TextboxDescrizione" MaxLength="100" Width="100%"></asp:TextBox>
<asp:Literal id=ActivityID runat="server" visible="false"></asp:Literal></TD></TR>
        <TR>
          <TD style="PADDING-LEFT: 5px" vAlign=bottom width="35%">
            <TABLE class=normal cellSpacing=0 cellPadding=0>
              <TR>
                <TD><twc:LocalizedLiteral text="Acttxt63" runat="server"/></TD>
                <TD>&nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  onclick=ViewCompany(event)
                  alt="AcTooltip3" src="/i/lens.gif" runat="server" />
              </TD></TR></TABLE>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxCompanyID style="DISPLAY: none" runat="server"></asp:TextBox>
<asp:TextBox id=TextboxCompany runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD style="wrap: no" width=60>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/Common/PopCompany.aspx?render=no&amp;textbox={0}TextboxCompany&amp;textbox2={0}TextboxCompanyID',event,500,400)"
                  alt="AcTooltip4" src="/i/user.gif"
                  border=0 runat="server" /> &nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  controlonclick="CleanField('{0}TextboxCompanyID');CleanField('{0}TextboxCompany')"
                  alt="AcTooltip8" src="/i/erase.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></TD>
          <TD style="PADDING-RIGHT: 5px" vAlign=bottom width="35%">
            <DIV class=normal><twc:LocalizedLiteral text="Acttxt82" runat="server"/></DIV>
<asp:DropDownList id=DropDownListClassification runat="server" CssClass="BoxDesign" Width="100%"></asp:DropDownList></TD></TR>
        <TR>
          <TD style="PADDING-LEFT: 5px" vAlign=bottom width="35%">
            <TABLE class=normal cellSpacing=0 cellPadding=0>
              <TR>
                <TD vAlign=top><twc:LocalizedLiteral text="Acttxt64" runat="server"/></TD>
                <TD vAlign=top>&nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  onclick=ViewContact(event)
                  alt="AcTooltip3" src="/i/lens.gif" runat="server" />
              </TD></TR></TABLE>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxContactID style="DISPLAY: none" runat="server" Width="100%"></asp:TextBox>
<asp:TextBox id=TextboxContact runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD style="wrap: no" width=60>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/common/popcontacts.aspx?render=no&amp;textbox={0}TextboxContact&amp;textboxID={0}TextboxContactID&amp;companyID=' + getElement('{0}TextboxCompanyID').value,event,400,300)"
                  alt="AcTooltip5" src="/i/user.gif"
                  border=0 runat="server" /> &nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  controlonclick="CleanField('{0}TextboxContactID');CleanField('{0}TextboxContact')"
                  alt="AcTooltip9" src="/i/erase.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></TD>
          <TD style="PADDING-RIGHT: 5px" vAlign=bottom width="35%">
            <DIV class=normal><twc:LocalizedLiteral text="Acttxt83" runat="server"/></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxParentID style="DISPLAY: none" runat="server"></asp:TextBox>
<asp:TextBox id=TextboxParent runat="server" CssClass="BoxDesign" Width="100%" ReadOnly="true"></asp:TextBox></TD>
                <TD style="wrap: no" width=60>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/common/PopActivity.aspx?render=no&amp;textbox={0}TextboxParent&amp;textboxID={0}TextboxParentID&amp;company=' + {0}TextboxCompanyID.value + '&amp;contact=' + {0}TextboxContactID.value ,event,400,300)"
                  alt="AcTooltip16" src="/i/newevent.gif"
                  border=0 runat="server" /> &nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  controlonclick="CleanField('{0}TextboxParentID');CleanField('{0}TextboxParent')"
                  alt="AcTooltip17" src="/i/erase.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></TD></TR>
        <TR>
          <TD style="PADDING-LEFT: 5px" vAlign=bottom width="35%">
            <TABLE class=normal cellSpacing=0 cellPadding=0>
              <TR>
                <TD><twc:LocalizedLiteral text="Acttxt89" runat="server"/></TD>
                <TD>&nbsp;<twc:LocalizedImg style="CURSOR: pointer" onclick=ViewLead(event)
                  alt="AcTooltip3" src="/i/lens.gif" runat="server" />
              </TD></TR></TABLE>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxLeadID style="DISPLAY: none" runat="server"></asp:TextBox>
<asp:TextBox id=TextboxLead runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD style="wrap: no" width=60>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/Common/PopLead.aspx?render=no&amp;textbox={0}TextboxLead&amp;textboxID={0}TextboxLeadID',event,400,300)"
                  alt="AcTooltip6" src="/i/user.gif"
                  border=0 runat="server" /> &nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  controlonclick="CleanField('{0}TextboxLeadID');CleanField('{0}TextboxLead')"
                  alt="AcTooltip10" src="/i/erase.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></TD>
          <TD style="PADDING-RIGHT: 5px" vAlign=bottom noWrap
            width="35%">
<asp:Panel id=PanelChild runat="server">
            <DIV class=normal><twc:LocalizedLiteral text="Acttxt84" runat="server"/></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD width="91%">
<asp:DropDownList id=ChildType CssClass="BoxDesign" Runat="server" Width="100%"></asp:DropDownList></TD>
                <TD width=30>&nbsp;
<asp:LinkButton id=ChildAction Runat="server"></asp:LinkButton></TD></TR></TABLE></asp:Panel></TD></TR>
        <TR>
          <TD style="PADDING-LEFT: 5px" vAlign=bottom width="35%">
            <DIV class=normal><twc:LocalizedLiteral text="Acttxt31" runat="server"/></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxOpportunityID style="DISPLAY: none" runat="server" Width="100%"></asp:TextBox>
<asp:TextBox id=TextboxOpportunity runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD style="wrap: no" width=60>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/common/PopOpportunity.aspx?render=no&amp;textbox={0}TextboxOpportunity&amp;textboxID={0}TextboxOpportunityID',event)"
                  alt="AcTooltip7" src="/i/user.gif"
                  border=0 runat="server" /> &nbsp;<twc:LocalizedImg  style="CURSOR: pointer"
                  controlonclick="CleanField('{0}TextboxOpportunityID');CleanField('{0}TextboxOpportunity');"
                  alt="AcTooltip11" src="/i/erase.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></TD>
          <TD class=normal style="PADDING-RIGHT: 5px" vAlign=bottom
            width="35%"><DIV class=normal><twc:LocalizedLiteral text="Acttxt85" runat="server"/></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD width="60%">
<asp:TextBox id=DocumentDescription runat="server" CssClass="BoxDesign" readonly></asp:TextBox>
<asp:TextBox id=IDDocument style="DISPLAY: none" runat="server"></asp:TextBox></TD>
                <TD noWrap width="40%">&nbsp; <twc:LocalizedImg  style="CURSOR: pointer"
                  controlonclick="CreateBox('/common/PopFile.aspx?render=no&amp;textbox={0}DocumentDescription&amp;textboxID={0}IDDocument',event,600,500)"
                  alt="AcTooltip18" src="/i/sheet.gif"
                  border=0 runat="server" /> <twc:LocalizedImg  style="CURSOR: pointer" onclick=ClearDocument()
                  alt="AcTooltip19" src="/i/deletedoc.gif"
                  border=0 runat="server" />
<asp:LinkButton id=LinkDocument runat="server" CssClass="normal" Width="100%"></asp:LinkButton></TD></TR></TABLE></TD></TR>
        <TR>
          <TD style="PADDING-LEFT: 5px" vAlign=top width="35%">
            <TABLE cellSpacing=2 cellPadding=0 width="100%">
              <TR>
                <TD width="50%">
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt77" runat="server"/></DIV>
<asp:DropDownList id=DropDownListStatus runat="server" CssClass="BoxDesign" Width="100%" onchange="changestatus(this.value)"></asp:DropDownList></TD>
                <TD width="50%">
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt78" runat="server"/></DIV>
<asp:DropDownList id=DropDownListPriority runat="server" CssClass="BoxDesign" Width="100%"></asp:DropDownList></TD></TR></TABLE>&nbsp;
          </TD>
          <TD style="PADDING-RIGHT: 5px" vAlign=top noWrap width="35%">
            <TABLE id=tblQuoteStage width="100%" runat="server">
              <TR>
                <TD>
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt128" runat="server"/></DIV>
<asp:TextBox id=Activity_QuoteNumber runat="server" CssClass="BoxDesign" maxlength="10"></asp:TextBox></TD>
                <TD>
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt127" runat="server"/></DIV>
<asp:DropDownList class=BoxDesign id=Activity_QuoteStage runat="server"></asp:DropDownList></TD>
                <TD vAlign=bottom><SPAN class=save id=convertpurchase
                  style="VISIBILITY: hidden; CURSOR: pointer"
                  onclick=OpenConvertPurchase() runat="server"><twc:LocalizedLiteral text="Acttxt130" runat="server"/></SPAN></TD></TR></TABLE>
            <TABLE style="DISPLAY: none" width="100%">
              <TR>
                <TD>
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt91" runat="server"/></DIV>
<asp:DropDownList class=BoxDesign id=Activity_Document runat="server"></asp:DropDownList></TD>
                <TD width=20>
<asp:LinkButton class=normal id=DocGen runat="server" Text="OK"></asp:LinkButton></TD></TR></TABLE></TD></TR>
        <TR>
          <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" vAlign=top
colSpan=2>
            <TABLE cellSpacing=2 cellPadding=0 width="100%">
              <TR>
                <TD noWrap width="100%"><IMG style="CURSOR: pointer"
                  onclick=ExpandDescription() src="/images/down.gif">
<asp:Label id=LabelDescription runat="server" CssClass="normal" Width="100%"></asp:Label></TD></TR>
              <TR>
                <TD>
<asp:TextBox id=TextboxDescription runat="server" CssClass="BoxDesign" Width="100%" wrap Height="100px" TextMode="MultiLine"></asp:TextBox></TD></TR></TABLE></TD></TR>
<asp:Panel id=SecondDescription runat="server" visible="false">
        <TR>
          <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px" vAlign=top
          width="100%" colSpan=2>
            <DIV>
<asp:Label id=LabelDescription2 runat="server" CssClass="normal" Width="100%">Descrizione 2</asp:Label></DIV>
<asp:TextBox id=TextboxDescription2 runat="server" CssClass="BoxDesign" Width="100%" Height="100px" TextMode="MultiLine"></asp:TextBox></TD></TR></asp:Panel></TABLE></TD>
    <TD
    style="PADDING-LEFT: 5px; BORDER-LEFT: #555555 1px solid; PADDING-TOP: 13px"
    vAlign=top>
      <TABLE cellSpacing=1 cellPadding=1 width="99%">
        <TR>
          <TD style="PADDING-RIGHT: 5px; PADDING-LEFT: 5px"
            vAlign=top>
<asp:Panel id=PanelOwner runat="server">
            <DIV class=normal><twc:LocalizedLiteral text="Acttxt65" runat="server"/></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextboxOwnerID style="DISPLAY: none" runat="server" Width="100%"></asp:TextBox>
<asp:TextBox id=TextboxOwner runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD width=30>&nbsp;<twc:LocalizedImg style="CURSOR: pointer"
                  controlonclick="CreateBox('/common/PopAccount.aspx?render=no&amp;textbox={0}TextboxOwner&amp;textbox2={0}TextboxOwnerID',event)"
                  alt="AcTooltip12" src="/i/user.gif"
                  border=0 runat="server" /> </TD></TR></TABLE></asp:Panel>
            <DIV>
<asp:Label id=LabelData runat="server" CssClass="normal" Width="100%">Data</asp:Label>
<domval:RequiredDomValidator id=DataValidator runat="server" EnableClientScript="False" Display="Dynamic" ControlToValidate="TextBoxData" ErrorMessage="*"></domval:RequiredDomValidator>
<domval:CompareDomValidator id=CvRecDate runat="Server" Display="Dynamic" ControlToValidate="TextBoxData" ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></domval:CompareDomValidator></DIV>
            <TABLE cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD>
<asp:TextBox id=TextBoxData runat="server" CssClass="BoxDesign" Width="100%"></asp:TextBox></TD>
                <TD vAlign=middle align=center width=60>
<asp:PlaceHolder id=IMGAvailability_holder runat="server">&nbsp; <twc:LocalizedImg
                  id=IMGAvailability style="CURSOR: pointer"
                  onclick=AppointmentVerify(1,event);
                  alt="AcTooltip13" src="/i/free.gif"
                  border=0 runat="server" runat="server" /> </asp:PlaceHolder>&nbsp;<twc:LocalizedImg
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/Common/PopUpDate.aspx?datediff=1&amp;textbox={0}TextBoxData&amp;Start='+(document.getElementById('{0}TextBoxData')).value,event,195,195)"
                  alt="AcTooltip14"
                  src="/i/SmallCalendar.gif" border=0 runat="server" /> </TD></TR>
<asp:Panel id=PanelQuoteExpire runat="server" visible="false">
              <TR>
                <TD class=normal>
                  <DIV><twc:LocalizedLiteral text="Esttxt23" runat="server"/></DIV>
<asp:TextBox id=TextBoxDataQuoteExpire runat="server" CssClass="BoxDesign" Width="100%"></asp:TextBox></TD>
                <TD vAlign=bottom align=center width=60>&nbsp;<IMG
                  style="CURSOR: pointer"
                  controlonclick="CreateBox('/Common/PopUpDate.aspx?datediff=1&amp;textbox={0}TextBoxDataQuoteExpire&amp;Start='+(document.getElementById('{0}TextBoxDataQuoteExpire')).value,event,195,195)"
                  src="/i/SmallCalendar.gif" border=0> </TD></TR></asp:Panel>
              <TR>
                <TD colSpan=3>
                  <TABLE class=normal id=HourPanel style="PADDING-TOP: 2px"
                  cellSpacing=0 cellPadding=0 width="100%" border=0
                  runat="server">
                    <TR>
                      <TD><twc:LocalizedLiteral text="Evnttxt6" runat="server"/></TD>
                      <TD>
<asp:TextBox class=BoxDesign onkeypress=HourCheck(this,event) id=TextBoxHour runat="server" maxlength="8" width="50"></asp:TextBox></TD>
                      <TD vAlign=middle align=left width=40><IMG
                        controlonclick="HourUp('{0}TextBoxHour')"
                        src="/images/up.gif"> <IMG
                        controlonclick="HourDown('{0}TextBoxHour')"
                        src="/images/down.gif">
              </TD></TR></TABLE></TD></TR></TABLE><SPAN
            id=Appointmenthours runat="server">
            <TABLE class=normal cellSpacing=0 cellPadding=0 width="100%"
            border=0>
              <TR>
                <TD width="40%"><twc:LocalizedLiteral text="Evnttxt5" runat="server"/>
<domval:RequiredDomValidator id=StartHourValidator runat="server" EnableClientScript="false" Display="Dynamic" ControlToValidate="F_StartHour" ErrorMessage="*"></domval:RequiredDomValidator></TD>
                <TD>
                  <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
                    <TR>
                      <TD>
<asp:TextBox class=BoxDesign onkeypress=HourCheck(this,event) id=F_StartHour runat="server" maxlength="8" width="50"></asp:TextBox></TD>
                      <TD vAlign=middle align=left width=40><IMG
                        onclick="HourUp('F_StartHour')" src="/images/up.gif">
                        <IMG onclick="HourDown('F_StartHour')"
                        src="/images/down.gif"> </TD></TR></TABLE></TD></TR>
              <TR>
                <TD width="40%"><twc:LocalizedLiteral text="Evnttxt6" runat="server"/>
<domval:RequiredDomValidator id=EndHourValidator runat="server" EnableClientScript="false" Display="Dynamic" ControlToValidate="F_EndHour" ErrorMessage="*"></domval:RequiredDomValidator></TD>
                <TD>
                  <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
                    <TR>
                      <TD>
<asp:TextBox class=BoxDesign onkeypress=HourCheck(this,event) id=F_EndHour runat="server" maxlength="8" width="50"></asp:TextBox></TD>
                      <TD vAlign=middle align=left width=40><IMG
                        onclick="HourUp('F_EndHour')" src="/images/up.gif"> <IMG
                        onclick="HourDown('F_EndHour')" src="/images/down.gif">
                      </TD></TR></TABLE></TD></TR></TABLE>
            <TABLE class=normal cellSpacing=0 cellPadding=0 width="100%">
              <TR>
                <TD colSpan=2><twc:LocalizedLiteral text="Evnttxt55" runat="server"/></TD></TR>
              <TR>
                <TD width="90%">
<asp:TextBox id=IdCompanion style="DISPLAY: none" runat="server" Width="100%"></asp:TextBox>
<asp:TextBox id=Companion runat="server" CssClass="BoxDesign" Width="100%" readonly="true"></asp:TextBox></TD>
                <TD vAlign=bottom width=50><NOBR><twc:LocalizedImg  style="CURSOR: pointer"
                  onclick="CreateBox('/Common/PopAccount.aspx?render=no&amp;textbox=Companion&amp;textbox2=IdCompanion',event)"
                  alt="AcTooltip12" src="/i/user.gif"
                  border=0 runat="server" /> <twc:LocalizedImg  id=UserAppImg style="CURSOR: pointer"
                  onclick=AppointmentVerify(2,event);
                  alt="AcTooltip13" src="/i/free.gif"
                  border=0 name=UserAppImg /> <twc:LocalizedImg  id=UserAppImg
                  style="CURSOR: pointer" onclick=RemoveCompanion();
                  alt="AcTooltip15" src="/i/erase.gif"
                  border=0 name=UserAppImg runat="server" /> </NOBR></TD></TR></TABLE></SPAN>
            <TABLE width="100%">
              <TR>
                <TD>
                  <DIV class=normal><twc:LocalizedLiteral text="Acttxt79" runat="server"/></DIV>
<asp:DropDownList id=DropDownListPreAlarm runat="server" CssClass="BoxDesign" Width="100%"></asp:DropDownList></TD></TR>
              <TR>
                <TD style="PADDING-TOP: 10px">
                  <TABLE
                  style="BORDER-RIGHT: #dcdcdc 1px solid; BORDER-TOP: #dcdcdc 1px solid; BORDER-LEFT: #dcdcdc 1px solid; BORDER-BOTTOM: #dcdcdc 1px solid"
                  width="100%">
                    <TR>
                      <TD class=normal bgColor=#dcdcdc><twc:LocalizedLiteral text="Acttxt123" runat="server"/></TD></TR>
                    <TR>
                      <TD>
<asp:CheckBox id=CheckToBill CssClass="normal" TextAlign="Right" Runat="server" Text="Da Fatturare"></asp:CheckBox></TD></TR>
                    <TR>
                      <TD>
<asp:CheckBox id=CheckCommercial CssClass="normal" TextAlign="Right" Runat="server" Text="Commerciale" Checked="True"></asp:CheckBox></TD></TR>
                    <TR>
                      <TD>
<asp:CheckBox id=CheckTechnical CssClass="normal" TextAlign="Right" Runat="server" Text="Tecnica"></asp:CheckBox></TD></TR>
                    <TR>
                      <TD>
                        <DIV class=normal><twc:LocalizedLiteral text="Acttxt81" runat="server"/></DIV>
                        <TABLE class=normal id=duration style="PADDING-TOP: 3px"
                        cellSpacing=0 cellPadding=0 width="100%">
                          <TR>
                            <TD noWrap width="50%">
<asp:TextBox id=TextBoxDurationH runat="server" CssClass="BoxDesign" MaxLength="2" width="30px"></asp:TextBox>&nbsp;hh
                            </TD>
                            <TD noWrap width="50%">
<asp:TextBox id=TextBoxDurationM runat="server" CssClass="BoxDesign" MaxLength="2" width="30px"></asp:TextBox>&nbsp;mm
                            </TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE></TD></TR></TABLE>
<TABLE id=Table3 cellSpacing=1 cellPadding=1 width="99%" border=0>
  <TR id=MoveLog runat="server">
    <TD colSpan=2>
      <DIV class=normal style="CURSOR: pointer" onclick=ViewHideLog()><SPAN
      id=LogView onclick=ViewHideLog()>[+]</SPAN>
      <twc:LocalizedLiteral text="Acttxt114" runat="server"/></DIV>
<asp:PlaceHolder id=MoveLogTable runat="server"></asp:PlaceHolder></TD></TR>
  <TR>
    <TD noWrap align=left>
<asp:Label id=ActivityInfo runat="server" cssClass="divautoformRequired"></asp:Label></TD>
    <TD align=right>
<asp:LinkButton id=SubmitBtn CssClass="save" Runat="server"></asp:LinkButton>&nbsp;&nbsp;
<asp:LinkButton id=SubmitBtnDoc CssClass="save" Runat="server"></asp:LinkButton></TD></TR></TABLE>
</asp:Panel></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE></TD></TR></TBODY></TABLE>
