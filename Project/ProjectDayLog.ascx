<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectDayLog.ascx.cs" Inherits="Digita.Tustena.Project.ProjectDayLog" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<twc:jscontrolid id="jsc" runat=server Identifier="Day"/>
<script>
    function FillSections()
    {
        if(!ObjExists('Ajax')){
			ele.onkeyup=null;
			return;}
        var type=document.getElementById(jsControlIdDay+"DayOwnerType");
        var res = Ajax.Project.FillSection(type.value);

        if(typeof(res) == 'object')
        {
                var sections=document.getElementById(jsControlIdDay+"DayLogSection");
                sections.options.length = 0;

            	try{
					if(typeof(res.value.Tables) == 'object')
					{
					    for (i=0; i<res.value.Tables.Table.Rows.length; i++){
					        sections.options[i] = new Option(res.value.Tables.Table.Rows[i].TITLE,res.value.Tables.Table.Rows[i].ID);
					    }
					}
					}catch(e){}
        }
    }
</script>
<table cellpadding=0 cellspacing=0 width="100%">
    <tr>
        <td>
            <div>Operatore</div>
            <table width="100%" cellspacing=0 cellpadding=0>
			        <tr>
			        <td>
			            <asp:TextBox id="DayOwnerID" runat=server style="display:none"></asp:TextBox>
			            <asp:TextBox id="DayOwnerType" runat=server style="display:none"></asp:TextBox>
			            <asp:TextBox id="DayOwner" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
			        </td>
			        <td width="30">
			             &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/project/PopGetMember.aspx?render=no&MemberId='+jsControlIdDay+'DayOwnerID&MemberName='+jsControlIdDay+'DayOwner&MemberType='+jsControlIdDay+'DayOwnerType&ProjectId='+ToDoProject+'&event=FillSections()',event,400,400)">
			        </td>
			    </tr>
            </table>
        </td>
        <td>
            <div>Data</div>
            <table width="100%" cellspacing=0 cellpadding=0>
					  <tr>
					    <td>
							<asp:TextBox id="DayLogDate" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign"></asp:TextBox>
						</td>
						<td width="30">
							&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+jsControlIdDay+'DayLogDate&Start='+(document.getElementById(jsControlIdDay+'DayLogDate')).value,event,195,195)">
						</td>
					  </tr>
			</table>
        </td>
        <td>
            <div>Ore impiegate</div>
            <asp:TextBox ID="DayLogDuration" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan=3>
            <div>Descrizione lavoro</div>
            <asp:TextBox ID="DayLogDescription" runat=server TextMode=MultiLine Height="50" CssClass="BoxDesign"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td colspan=3>
            <div>Materiale usato</div>
            <asp:TextBox ID="DayLogMaterial" runat=server TextMode=MultiLine Height="50" CssClass="BoxDesign"></asp:TextBox>
        </td>
    </tr>
    <tr>
       <td>
            <div>Sezione</div>
            <asp:DropDownList ID="DayLogSection" runat=server CssClass="BoxDesign"></asp:DropDownList>
       </td>
       <td>
            <div>To Do</div>
            <asp:DropDownList ID="DayLogTodo" runat=server CssClass="BoxDesign"></asp:DropDownList>
       </td>
       <td>
            <div>Avanzamento attuale %</div>
            <asp:TextBox ID="DayLogProgress" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)" MaxLength=2></asp:TextBox>
       </td>
    </tr>
    <tr>
        <td colspan=3 align=right>
            <asp:LinkButton ID="DayLogSave" runat=server CssClass="Save"></asp:LinkButton>
        </td>
    </tr>
</table>
