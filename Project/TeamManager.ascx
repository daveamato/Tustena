<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TeamManager.ascx.cs" Inherits="Digita.Tustena.Project.TeamManager" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<twc:jscontrolid id="jsc" runat=server Identifier="Team"/>
<script>
var idcTeam=1;

    function SelectMember(id,txt,type)
    {
            if(document.getElementById(type).selectedIndex==0)
            {
                CreateBox('/common/PopAccount.aspx?render=no&textbox='+txt+'&textbox2='+id,event)
            }else
            {
                CreateBox('/common/popcontacts.aspx?render=no&textbox='+txt+'&textboxID='+id,event,400,300);
            }
    }


        function addMember()
		{
			cloneObj('MembersTable',idcTeam++,jsControlIdTeam+'Members');
			var tempidcdoc=idcTeam-1;
			clearMember('_'+(tempidcdoc));
		}

		function removeMember(cloneparam1)
		{
		    var suffix='';
		    if(cloneparam1.indexOf('_')>0)
		        suffix = cloneparam1.substring(cloneparam1.indexOf('_'));
		    document.getElementById("MemberToDelete").value+='|'+document.getElementById("TeamMemberRealId"+suffix).value;
			removeCloned(cloneparam1,jsControlIdTeam+'Members');
			idcTeam--;
			if(idcTeam<1)
				clearMember('');
		}

		function clearMember(suffix)
	    {
				document.getElementById("TeamMemberId"+suffix).value='';
				document.getElementById("TeamMember"+suffix).value='';
				document.getElementById("TeamMemberRealId"+suffix).value='-1';
				document.getElementById("MemberType"+suffix).selectedIndex=0;
		}

		function fillMember(suffix,realid,id,txt,type)
		{
		    if(suffix>0)
		    {
		        document.getElementById("TeamMemberRealId_"+suffix).value=realid;
				document.getElementById("TeamMemberId_"+suffix).value=id;
				document.getElementById("TeamMember_"+suffix).value=txt;
				document.getElementById("MemberType_"+suffix).selectedIndex=type;
			}else
			{
			    document.getElementById("TeamMemberRealId").value=realid;
				document.getElementById("TeamMemberId").value=id;
				document.getElementById("TeamMember").value=txt;
				document.getElementById("MemberType").selectedIndex=type;
			}
		}
</script>
<table cellpadding=0 cellspacing=0 id="MainTable" runat=server>
    <tr>
        <td align=right>
            <asp:LinkButton ID="btnNewTeam" runat=server CssClass="Save">Nuovo team</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <twc:TustenaRepeater ID="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="false" FilterCol="Description" AllowSearching="false" Width="840">
                        <HeaderTemplate>
                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" Width="70%" DataCol="Description"></twc:RepeaterColumnHeader>
                            <twc:RepeaterColumnHeader ID="Repeatercolumnheader2" runat="Server"
                                CssClass="GridTitle" Width="30%" DataCol="LeaderName"></twc:RepeaterColumnHeader>

                             <twc:RepeaterHeaderEnd ID="RepeaterHeaderEnd1" runat="server">
                            </twc:RepeaterHeaderEnd>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="GridItem">
                                    <asp:Label ID="teamID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                    <asp:Label ID="leaderID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"LEADERID")%>'></asp:Label>
                                    <asp:LinkButton ID="btnOpenTeam" CommandName="btnOpenTeam" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItem">
                                    <asp:Literal ID="litLeader" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"LEADERNAME")%>'></asp:Literal>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Label ID="teamID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                    <asp:Label ID="leaderID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"LEADERID")%>'></asp:Label>
                                    <asp:LinkButton ID="btnOpenTeam" CommandName="btnOpenTeam" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"DESCRIPTION")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItemAltern">
                                    <asp:Literal ID="litLeader" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"LEADERNAME")%>'></asp:Literal>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>

                </twc:TustenaRepeater>
        </td>
    </tr>
</table>

<table cellpadding=0 cellspacing=3 id="TeamTable" runat=server width="100%">
    <tr>
        <td colspan=4 width="100%">
            <div>Descrizione
            <domval:RequiredDomValidator ID="TeamDescriptionValidator" Display=Static ControlToValidate="TeamDescription" Runat="server">*</domval:RequiredDomValidator>
            </div>
            <asp:TextBox ID="TeamDescription" runat=server CssClass="BoxDesignReq"></asp:TextBox>
            <asp:Literal ID="TeamID" runat=server Visible=false></asp:Literal>
        </td>
    </tr>
    <tr>
        <td width="25%" valign=top>
            Proprietario
            <domval:RequiredDomValidator ID="TeamOwnerValidator" Display=Static ControlToValidate="TeamOwner" Runat="server">*</domval:RequiredDomValidator>
        </td>
        <td width="25%" valign=top>
            <table width="100%" cellspacing=0 cellpadding=0>
			    <tr><td>
			           <asp:TextBox id="TeamOwnerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
			           <asp:TextBox id="TeamOwner" runat="server" Width="100%"  CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox>
			       </td>
			       <td width="30">
			          &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox='+jsControlIdTeam+'TeamOwner&textbox2='+jsControlIdTeam+'TeamOwnerID',event)">
			       </td>
			    </tr>
            </table>
        </td>

        <td width="20%" valign=top>
            Membri&nbsp;<img src=/i/plus.gif onclick="addMember();" style="cursor:pointer;">
            <input type=text name="MemberToDelete" id="MemberToDelete" style="display:none">
        </td>
        <td width="30%" id="Members">
            <table cellpadding=0 cellspacing=0 width="100%" id="MembersTable">
                <tr>
                    <td nowrap>
                        <table width="100%" cellspacing=0 cellpadding=0>
			                <tr>
			                    <td>
			                        <select id="MemberType" name="MemberType" old=true>
                                        <option value=0>Utente</option>
                                        <option value=1>Contatto</option>
                                    </select>
			                    </td>
			                   <td width="50%">
			                       <input type=text id="TeamMemberRealId" name="TeamMemberRealId" style="display:none" value="-1">
			                       <input type=text id="TeamMemberId" name="TeamMemberId" style="display:none">
			                       <input type=text id="TeamMember" name="TeamMember" class="BoxDesign" readonly>

			                   </td>
			                   <td width="60" nowrap>
			                      &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" cloneparam2="TeamMemberId" cloneparam1="TeamMember" cloneparam3="MemberType" onclick="SelectMember(this.getAttribute('cloneparam2'),this.getAttribute('cloneparam1'),this.getAttribute('cloneparam3'));">
			                      <img src=/i/erase.gif cloneparam1="MembersTable" onclick="removeMember(this.getAttribute('cloneparam1'));" style="cursor:pointer;">
			                   </td>
			                </tr>
                        </table>
                    </td>
                </tr>
            </table>


        </td>
    </tr>
    <tr>
        <td colspan=4 align=right>
            <asp:LinkButton ID="btnSaveTeam" runat=server CssClass="Save"></asp:LinkButton>
        </td>
    </tr>
</table>
