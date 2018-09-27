<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectSessions.ascx.cs" Inherits="Digita.Tustena.Project.ProjectSessions" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="timing" TagName="ProjectToDo" Src="~/project/ProjectToDo.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>

<twc:jscontrolid id="jsc" runat=server />
<script type="text/javascript" src="/js/autodate.js"></script>
<script>
function openSection(id)
{
    document.getElementById(jsControlId+"SelectSession").value=id;
    document.getElementById(jsControlId+"btnSelectSession").click();
}


</script>
<asp:TextBox ID="SelectSession" runat=server style="display:none"></asp:TextBox>
<table cellpadding=0 cellspacing=0 id="MainTable" runat=server width="100%">
    <tr>
        <td align=right>
            <asp:LinkButton ID="btnNewSession" runat=server CssClass="Save" OnClick="btnNewSession_Click">Nuova sezione</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Literal ID="lblSection" runat=server></asp:Literal>
            <asp:LinkButton ID="btnSelectSession" runat=server style="display:none"></asp:LinkButton>
        </td>
    </tr>
</table>

<table cellpadding=0 cellspacing=0 id="SessionTable" runat=server width="98%">
 <tr>
    <td width="45%" valign=top>
        <table cellpadding=0 cellspacing=0 width="100%">
            <tr>
                <td width="40%">Sezione
                    <domval:RequiredDomValidator ID="SectionNameValidator" Display=Static ControlToValidate="SectionName" Runat="server">*</domval:RequiredDomValidator>
                </td>
                <td width="60%"><asp:TextBox ID="SectionName" runat=server CssClass="BoxDesignReq"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="40%">Descrizione</td>
                <td width="60%"><asp:TextBox ID="SectionDescription" TextMode=MultiLine Height=50 runat=server CssClass="BoxDesign"></asp:TextBox></td>
            </tr>
            <tr>
                <td width="40%">Sezione di riferimento</td>
                <td width="60%">
                    <asp:DropDownList ID="SectionParent" runat=server CssClass="BoxDesign"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td width="40%">Responsabile
                    <domval:RequiredDomValidator ID="SectionOwnerValidator" Display=Static ControlToValidate="SectionOwner" Runat="server">*</domval:RequiredDomValidator>
                </td>
                <td width="60%">
                    <table width="100%" cellspacing=0 cellpadding=0>
			            <tr><td>
			                   <asp:TextBox id="SectionOwnerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
			                   <asp:TextBox id="SectionOwner" runat="server" Width="100%"  CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox>
			               </td>
			               <td width="30">
			                  &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/project/PopGetMember.aspx?render=no&MemberId='+jsControlId+'SectionOwnerID&MemberName='+jsControlId+'SectionOwner&MemberType=0&ProjectId=<%=this.prjID %>',event,400,400)">
			               </td>
			            </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="40%">Avanzamento</td>
                <td width="60%">
                    <asp:Literal ID="SectionProgress" runat=server></asp:Literal>
                </td>
            </tr>
            <tr>
                <td width="40%">Colore Gantt</td>
                <td width="60%">
                    <SELECT id="FilterColor" name="FilterColor" runat="server" old=true style="width:200px"></SELECT>
                </td>
            </tr>
            <tr id="VariationRow" runat=server visible=false>
                <td>
                <table cellpadding=0 cellspacing=0>
                    <tr>
                        <td style="font-weight:bold;color:Red">Variazioni</td>
                    </tr>
                    <tr>
                        <td style="color:Red">
                            <asp:Literal ID="LitVariation" runat=server></asp:Literal>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
    </td>
    <td width="10%">&nbsp;</td>
    <td width="45%" valign=top>
        <table cellpadding=0 cellspacing=0 width="100%">
            <tr>
                <td width="40%">Data inizio pianificata
                    <domval:RequiredDomValidator ID="PlannedStartDateValidator" Display=Static ControlToValidate="PlannedStartDate" Runat="server">*</domval:RequiredDomValidator>
                </td>
                <td width="60%">
                    <table width="100%" cellspacing=0 cellpadding=0>
					  <tr>
					    <td>
							<asp:TextBox id="PlannedStartDate" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesignReq"></asp:TextBox>
						</td>
						<td width="30">
							&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+jsControlId+'PlannedStartDate&Start='+(document.getElementById(jsControlId+'PlannedStartDate')).value,event,195,195)">
						</td>
					  </tr>
					</table>
                </td>
            </tr>
            <tr>
                <td width="40%">Data fine pianificata</td>
                <td width="60%">
                    <table width="100%" cellspacing=0 cellpadding=0>
					  <tr>
					    <td>
							<asp:TextBox id="PlannedEndDate" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign"></asp:TextBox>
						</td>
						<td width="30">
							&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+jsControlId+'PlannedEndDate&Start='+(document.getElementById(jsControlId+'PlannedEndDate')).value,event,195,195)">
						</td>
					  </tr>
					</table>
                </td>
            </tr>
            <tr>
                <td width="40%">Tempo previsto (ore)</td>
                <td width="60%"><asp:TextBox ID="PlannedMinuteDuration" onkeypress="NumbersOnly(event,'',this)" runat=server CssClass="BoxDesign" ReadOnly=true></asp:TextBox></td>
            </tr>
            <tr>
                <td style="border-bottom:1px solid #000000" colspan=2>
                    &nbsp;
                </td>
            </tr>

            <tr>
                <td width="40%">Data inizio reale</td>
                <td width="60%">
                    <table width="100%" cellspacing=0 cellpadding=0>
					  <tr>
					    <td>
							<asp:TextBox id="RealStartDate" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign"></asp:TextBox>
						</td>
						<td width="30">
							&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+jsControlId+'RealStartDate&Start='+(document.getElementById(jsControlId+'RealStartDate')).value,event,195,195)">
						</td>
					  </tr>
					</table>
                </td>
            </tr>
            <tr>
                <td width="40%">Data fine reale</td>
                <td width="60%">
                    <table width="100%" cellspacing=0 cellpadding=0>
					  <tr>
					    <td>
							<asp:TextBox id="RealEndDate" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign"></asp:TextBox>
						</td>
						<td width="30">
							&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+jsControlId+'RealEndDate&Start='+(document.getElementById(jsControlId+'RealEndDate')).value,event,195,195)">
						</td>
					  </tr>
					</table>
                </td>
            </tr>
            <tr>
                <td width="40%">Tempo attuale (ore)</td>
                <td width="60%"><asp:TextBox ID="CurrentMinuteDuration" onkeypress="NumbersOnly(event,'',this)" runat=server CssClass="BoxDesign" ReadOnly=true></asp:TextBox></td>
            </tr>
            <tr>
                <td width="40%" valign=top>Costo</td>
                <td width="60%" nowrap valign=top>
                    <asp:RadioButtonList ID="CostType" runat=server CssClass=normal RepeatColumns=2 RepeatDirection=Horizontal Width="70%">
                        <asp:ListItem Value=0 Text="Fisso"></asp:ListItem>
                        <asp:ListItem Value=0 Text="Orario"></asp:ListItem>
                    </asp:RadioButtonList>
                    Importo <asp:TextBox ID="SectionAmount" onkeypress="NumbersOnly(event,'.,',this)" runat=server CssClass="BoxDesign" Width="30%"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
 </tr>
 <tr>
    <td colspan=3 align=right>
        <asp:LinkButton ID="btnSaveSection" runat=server CssClass="Save"></asp:LinkButton>
    </td>
 </tr>
 <tr>
    <td colspan=3>
        <timing:ProjectToDo runat="server" ID="ProjectToDo1" />
    </td>
 </tr>
</table>

