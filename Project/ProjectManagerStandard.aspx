<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ProjectManagerStandard.aspx.cs" Inherits="Digita.Tustena.Project.ProjectManagerStandard" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="sect" TagName="ProjectSessions" Src="~/project/ProjectSessions.ascx" %>
<%@ Register TagPrefix="team" TagName="TeamManager" Src="~/project/TeamManager.ascx" %>
<%@ Register TagPrefix="evt" TagName="ProjectEvents" Src="~/project/ProjectEvents.ascx" %>
<%@ Register TagPrefix="rel" TagName="ProjectSectionRelation" Src="~/project/ProjectSectionRelation.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
    <script type="text/javascript" src="/js/dynabox.js"></script>
     <script type="text/javascript" src="/js/autodate.js"></script>
    <script type="text/javascript" src="/js/clone.js"></script>
    <title>Untitled Page</title>
    <script>
        var idc=1;
        function addOwner()
		{
			cloneObj('OtherOwnerTable',idc++,'OtherOwner');
			var tempidcdoc=idc-1;
			clearOwner('_'+(tempidcdoc));
		}

		function removeOwner(cloneparam1)
		{
			removeCloned(cloneparam1,'OtherOwner');
			idc--;
			if(idc<1)
				clearOwner('');
		}

		function clearOwner(suffix)
	    {
				document.getElementById("OtherOwnerID"+suffix).value='';
				document.getElementById("OtherOwnerTxt"+suffix).value='';
		}

		function fillOwner(suffix,idvalue,txtvalue)
		{
		    if(suffix>0){
				document.getElementById("OtherOwnerID_"+suffix).value=idvalue;
				document.getElementById("OtherOwnerTxt_"+suffix).value=txtvalue;
			}else{
			    document.getElementById("OtherOwnerID").value=idvalue;
				document.getElementById("OtherOwnerTxt").value=txtvalue;
			}
		}
    </script>
</head>
<body id="body" runat=server>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                    <table width="98%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td class="sideContainer">
                                <div class="sideTitle">
                                    <asp:Literal ID="TitlePage" runat="server" /></div>
                                <asp:LinkButton ID="LblNewProject" CssClass="sidebtn" runat="server" CausesValidation="False" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td valign="top" height="100%" class="pageStyle">
                <twc:TustenaRepeater ID="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="Title" AllowSearching="false" Width="840">
                        <HeaderTemplate>
                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Prjtxt1" ID="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" Width="100%" DataCol="Title"></twc:RepeaterColumnHeader>
                            <td width="15px" Class="GridTitle">&nbsp;</td>
                            <twc:RepeaterMultiDelete id="Repeatermultidelete2" runat="server" CssClass="GridTitle" header="true"></twc:RepeaterMultiDelete>
                             <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Label ID="prjID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                    <asp:LinkButton ID="btnOpenProject" CommandName="btnOpenProject" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"TITLE")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItem">
                                    <asp:LinkButton ID="btnModify" CommandName="btnModify" runat=server><img src=/i/modify2.gif border=0 /></asp:LinkButton>
                                </td>
                                <twc:RepeaterMultiDelete id="DelCheck" runat="server" CssClass="GridItem"></twc:RepeaterMultiDelete>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Label ID="prjID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                    <asp:LinkButton ID="btnOpenProject" CommandName="btnOpenProject" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"TITLE")%>'></asp:LinkButton>
                                </td>
                                 <td class="GridItemAltern">
                                    <asp:LinkButton ID="btnModify" CommandName="btnModify" runat=server><img src=/i/modify2.gif border=0 /></asp:LinkButton>
                                </td>
                                <twc:RepeaterMultiDelete id="DelCheck" runat="server" CssClass="GridItemAltern"></twc:RepeaterMultiDelete>
                            </tr>
                        </AlternatingItemTemplate>

                </twc:TustenaRepeater>


                <twc:TustenaTabber ID="Tabber" Width="840" runat="server">
                <twc:TustenaTabberRight ID="TustenaTabberRight1" runat="server">
                    <asp:LinkButton ID="CloseProject" runat=server CssClass=Save>Lista Progetti</asp:LinkButton>
                </twc:TustenaTabberRight>
                 <twc:TustenaTab ID="visProject" ClientSide="true" runat="server" Header="Progetto">
                     <table id="ProjectTable" width="100%" runat="server" class="normal" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="border-bottom:1px solid #000000;"><b>DATI PROGETTO</b></td>
                        </tr>
                        <tr>
                            <td width="100%" valign=top>
                                <table width="100%" class="normal" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="20%" valign=top>Titolo
                                            <domval:RequiredDomValidator ID="prjTitleValidator" Display=Dynamic ControlToValidate="prjTitle" Runat="server">*</domval:RequiredDomValidator>
                                        </td>
                                        <td width="80%" valign=top><asp:TextBox ID="prjTitle" runat=server CssClass=BoxDesignReq MaxLength="200"></asp:TextBox>
                                        <asp:Literal ID="prjID" runat=server Visible=false></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="20%" valign=top>Descrizione</td>
                                        <td width="80%" valign=top><asp:TextBox ID="prjDescription" TextMode=MultiLine Height="50" runat=server CssClass=BoxDesign MaxLength="2000"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                     </table>
                     <table width="100%" cellspacing=0 cellpadding=0>
                        <tr>
                            <td colspan=3 style="border-bottom:1px solid #000000;"><b>SICUREZZA</b></td>
                        </tr>
                        <tr>
                            <td width="50%" valign=top>
                                <table cellpadding=0 cellspacing=0 width="100%">
                                    <tr>
                                        <td width="40%" valign=top>Proprietario
                                        <domval:RequiredDomValidator ID="prjOwnerIDValidator" Display=Dynamic ControlToValidate="prjOwner" Runat="server">*</domval:RequiredDomValidator>
                                        </td>
                                        <td width="60%" valign=top>
                                            <table width="100%" cellspacing=0 cellpadding=0>
									            <tr><td>
										            <asp:TextBox id="prjOwnerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										            <asp:TextBox id="prjOwner" runat="server" Width="100%"  CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox>
									            </td>
									            <td width="30">
										            &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=prjOwner&textbox2=prjOwnerID',event)">
									            </td>
									            </tr>
									        </table>
                                        </td>
                                   </tr>
                                   <tr>
                                        <td width="40%" valign=top nowrap>
                                            Utenti abilitati
                                            <img src=/i/plus.gif onclick="addOwner();" style="cursor:pointer;">
                                        </td>
                                        <td width="60%" colspan=2 id="OtherOwner">
                                            <table width="100%" cellspacing=0 cellpadding=4 id="OtherOwnerTable">
						                        <tr>
						                        <td>
							                        <img src=/i/erase.gif cloneparam1="OtherOwnerTable" onclick="removeOwner(this.getAttribute('cloneparam1'));" style="cursor:pointer;">
						                        </td>
							                    <td width="99%">
							                        <table width="100%" cellspacing=0 cellpadding=0>
									                    <tr><td>
										                    <asp:TextBox id="OtherOwnerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										                    <asp:TextBox id="OtherOwnerTxt" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									                    </td>
									                    <td width="30">
										                    &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" cloneparam2="OtherOwnerID" cloneparam1="OtherOwnerTxt" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox='+this.getAttribute('cloneparam1')+'&textbox2='+this.getAttribute('cloneparam2')+'',event)">
									                    </td>
									                    </tr>
									                </table>
							                    </td>
						                        </tr>
					                        </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80px">&nbsp;</td>
                            <td valign=top>
                                <table width="100%" class="normal" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="40%" valign=top nowrap>
                                            Progetto Aperto
                                        </td>
                                        <td width="60%">
                                            <asp:CheckBox ID="prjOpen" runat=server />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="40%" valign=top nowrap>
                                            Progetto Sospeso
                                        </td>
                                        <td width="60%">
                                            <asp:CheckBox ID="prjSuspend" runat=server />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                     </table>
                     <table id="tblEvents" runat=server width="100%" cellspacing=0 cellpadding=0>
                         <tr>
                            <td style="border-bottom:1px solid #000000;"><b>EVENTI</b></td>
                         </tr>
                         <tr>
                            <td valign=top>
                                <evt:ProjectEvents id="ProjectEvents1" runat=server></evt:ProjectEvents>
                            </td>

                         </tr>
                     </table>
                     <table id="tblRelations" runat=server width="100%" cellspacing=0 cellpadding=0>
                         <tr>
                            <td style="border-bottom:1px solid #000000;"><b>RELAZIONI</b></td>
                         </tr>
                         <tr>
                            <td valign=top>
                                <rel:ProjectSectionRelation runat="server" ID="ProjectSectionRelation1" />
                            </td>
                        </tr>
                     </table>
                     <table id="tblSendmail" runat=server width="100%" cellspacing=0 cellpadding=0>
                        <tr>
                            <td colspan=2 style="border-bottom:1px solid #000000;"><b>INVIO AGGIORNAMENTO</b></td>
                        </tr>
                        <tr>
                            <td valign=top>
                                <asp:RadioButtonList ID="selectMailType" runat=server RepeatDirection=Vertical RepeatColumns=1>
                                    <asp:ListItem Value=0 Text="Progetto completo"></asp:ListItem>
                                    <asp:ListItem Value=1 Text="ToDo utente"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td valign=top>
                                <asp:RadioButtonList ID="selectMailSend" runat=server RepeatDirection=Vertical RepeatColumns=1>
                                    <asp:ListItem Value=0 Text="Tutti i membri del progetto"></asp:ListItem>
                                    <asp:ListItem Value=1 Text="Amministratori del progetto"></asp:ListItem>
                                    <asp:ListItem Value=2 Text="Utente specifico"></asp:ListItem>
                                </asp:RadioButtonList>
                                <table width="200" cellspacing=0 cellpadding=0>
			                        <tr>
			                            <td>
			                               <asp:TextBox runat=server id="MailOwnerID" style="display:none"></asp:TextBox>
			                               <asp:TextBox runat=server id="MailOwnerType" style="display:none"></asp:TextBox>
			                               <asp:TextBox runat=server id="MailOwnerRealID" style="display:none"></asp:TextBox>
			                               <asp:TextBox runat=server id="MailOwner" CssClass="BoxDesign" ReadOnly=true></asp:TextBox>
			                           </td>
			                           <td width="30">
			                              &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/project/PopGetMember.aspx?render=no&MemberId=MailOwnerID&MemberName=MailOwner&MemberType=MailOwnerType&MemberRealId=MailOwnerRealID&ProjectId=<%=prjID.Text%>',event,400,400)">
			                           </td>
			                        </tr>
                                </table>

                                <p align=left><asp:LinkButton ID="btnSendMails" runat=server CssClass="Save">Invia</asp:LinkButton></p>
                            </td>
                        </tr>

                     </table>
                     <table cellpadding=0 cellspacing=0 width="100%">
                        <tr>
                            <td style="border-bottom:1px solid #000000;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td align=right>
                                <asp:LinkButton ID="btnSaveprj" runat=server CssClass="Save"></asp:LinkButton>
                                <asp:Label ID=sss runat=server></asp:Label>
                            </td>
                        </tr>
                     </table>


                  </twc:TustenaTab>
                  <twc:TustenaTab ID="visSections" ClientSide="true" runat="server" Header="Sezioni">
                    <sect:ProjectSessions runat="server" ID="ProjectSessions1" />

                  </twc:TustenaTab>
                  <twc:TustenaTab ID="visTeams" ClientSide="true" runat="server" Header="Teams">
                    <team:TeamManager runat="server" ID="TeamManager1" />
                  </twc:TustenaTab>





                </twc:TustenaTabber>
                </td>
            </tr>
    </table>
    </form>
</body>
</html>

