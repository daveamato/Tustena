<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectUpdate.aspx.cs" Inherits="Digita.Tustena.Project.ProjectUpdate" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<%@ Register TagPrefix="day" TagName="ProjectDayLog" Src="~/project/ProjectDayLog.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="head" runat="server">
    <link rel="stylesheet" type="text/css" media="all" href="/css/ttabber.css" />
    <script type="text/javascript" src="/js/dynabox.js"></script>
     <script type="text/javascript" src="/js/autodate.js"></script>
    <title>Untitled Page</title>
</head>
<twc:jscontrolid id="jsc" runat=server Identifier="Day"/>
<script>
    function FillSections()
    {
        if(!ObjExists('Ajax')){
			ele.onkeyup=null;
			return;}
        var memberid=document.getElementById(jsControlIdDay+"DayOwnerID");
        var res = Ajax.Project.FillSection(memberid.value);

        if(typeof(res) == 'object')
        {
                var sections=document.getElementById(jsControlIdDay+"DayLogSection");
                sections.options.length = 0;
                sections.options[0] = new Option("Seleziona ...","0");
            	try{
					if(typeof(res.value.Tables) == 'object')
					{
					    for (i=0; i<res.value.Tables.Table.Rows.length; i++){
					        sections.options[i+1] = new Option(res.value.Tables.Table.Rows[i].SECTION,res.value.Tables.Table.Rows[i].IDS);
					    }
					}
					}catch(e){}
        }
    }

    function FillSelectToDo()
    {
         if(!ObjExists('Ajax')){
			ele.onkeyup=null;
			return;}
        var memberid=document.getElementById(jsControlIdDay+"DayOwnerRealID");
        var sections=document.getElementById(jsControlIdDay+"DayLogSection");
        var op = sections.options[sections.selectedIndex].value.split("|");
        var res = Ajax.Project.FillToDo(op[1],op[0]);
        memberid.value=	op[1];
        if(typeof(res) == 'object')
        {
                var ToDo=document.getElementById(jsControlIdDay+"DayLogTodo");
                ToDo.options.length = 0;
                ToDo.options[0] = new Option("Seleziona ...","0");
            	try{
					if(typeof(res.value.Tables) == 'object')
					{
					    for (i=0; i<res.value.Tables.Table.Rows.length; i++){
					        ToDo.options[i+1] = new Option(res.value.Tables.Table.Rows[i].TODODESCRIPTION,res.value.Tables.Table.Rows[i].ID);
					    }
					}
					}catch(e){}
        }
    }

    function FillToDoProgress()
    {
        if(!ObjExists('Ajax')){
			ele.onkeyup=null;
			return;}
	    var ToDo=document.getElementById(jsControlIdDay+"DayLogTodo");
	    var res = Ajax.Project.FillToDoProgress(ToDo.options[ToDo.selectedIndex].value);
	    if(typeof(res) == 'object')
        {
            var o = res.value;

            document.getElementById(jsControlIdDay+"DayLogPlanned").value=o[0];
            document.getElementById(jsControlIdDay+"DayLogCurrent").value=o[1];
            document.getElementById(jsControlIdDay+"DayLogEndDate").value=o[2];
            document.getElementById(jsControlIdDay+"DayLogProgress").value=o[3];
        }
    }

    function allmembers()
    {
        document.getElementById(jsControlIdDay+"TimeOwnerID").value='';
        document.getElementById(jsControlIdDay+"TimeOwnerID").value='';
        document.getElementById(jsControlIdDay+"TimeOwnerType").value='';
        document.getElementById(jsControlIdDay+"TimeOwnerRealID").value='';
        document.getElementById(jsControlIdDay+"TimeOwnerTeam").value='';
        document.getElementById(jsControlIdDay+"TimeOwner").value='Tutti';
    }

    function Expand(o)
    {
        rowShowHide("detail_"+o);
    }

    function PrintTiming(p,m)
    {
        var mypage = "/project/projectprintreport.aspx?render=no&Report=2&Prj="+p;
        NewWindow(mypage, '', 600, 500, 'no');
    }
</script>
<body id="body" runat="server">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td width="140" class="SideBorderLinked" valign="top">
                &nbsp;
                </td>
                <td valign=top>
                <twc:TustenaRepeater ID="NewRepeater1" runat="server" SortDirection="asc" AllowPaging="true"
                        AllowAlphabet="true" FilterCol="Title" AllowSearching="false" Width="840">
                        <HeaderTemplate>
                        <twc:RepeaterHeaderBegin ID="RepeaterHeaderBegin1" runat="server">
                            </twc:RepeaterHeaderBegin>
                            <twc:RepeaterColumnHeader Resource="Prjtxt1" ID="Repeatercolumnheader1" runat="Server"
                                CssClass="GridTitle" Width="100%" DataCol="Title"></twc:RepeaterColumnHeader>
                            <td class="GridTitle" width="20px">&nbsp;</td>
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
                                    <asp:LinkButton ID="btnTiming" CommandName="btnTiming" runat=server><img src=/i/cal.gif border=0 /></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr>
                                <td class="GridItemAltern">
                                    <asp:Label ID="prjID" runat=server Visible=false Text='<%#DataBinder.Eval(Container.DataItem,"ID")%>'></asp:Label>
                                    <asp:LinkButton ID="btnOpenProject" CommandName="btnOpenProject" runat=server Text='<%#DataBinder.Eval(Container.DataItem,"TITLE")%>'></asp:LinkButton>
                                </td>
                                <td class="GridItemAltern">
                                    <asp:LinkButton ID="btnTiming" CommandName="btnTiming" runat=server><img src=/i/cal.gif border=0 /></asp:LinkButton>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>

                </twc:TustenaRepeater>

    <twc:TustenaTabber ID="Tabber" Width="840" runat="server">
                <twc:TustenaTabberRight ID="TustenaTabberRight1" runat="server">
                    <asp:LinkButton ID="CloseProject" runat=server CssClass=Save>Lista Progetti</asp:LinkButton>
                </twc:TustenaTabberRight>
                 <twc:TustenaTab ID="visProject" ClientSide="true" runat="server" Header="Day Log">

                    <table cellpadding=0 cellspacing=0 width="100%">
                        <tr>
                            <td width="34%">
                                <div>Operatore</div>
                                <table width="100%" cellspacing=0 cellpadding=0>
			                            <tr>
			                            <td>
			                                <asp:TextBox id="DayOwnerID" runat=server style="display:none"></asp:TextBox>
			                                <asp:TextBox id="DayOwnerType" runat=server style="display:none"></asp:TextBox>
			                                <asp:TextBox id="DayOwnerRealID" runat=server style="display:none"></asp:TextBox>
			                                <asp:TextBox id="DayOwnerTeam" runat=server style="display:none"></asp:TextBox>

			                                <asp:TextBox id="DayOwner" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
			                            </td>
			                            <td width="30">
			                                 &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/project/PopGetMember.aspx?render=no&MemberId='+jsControlIdDay+'DayOwnerID&MemberName='+jsControlIdDay+'DayOwner&MemberType='+jsControlIdDay+'DayOwnerType&MemberRealId='+jsControlIdDay+'DayOwnerRealID&MemberTeamId='+jsControlIdDay+'DayOwnerTeam&ProjectId='+ToDoProject+'&event=FillSections()',event,400,400)">
			                            </td>
			                        </tr>
                                </table>
                            </td>
                            <td width="33%">
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
                            <td width="33%">
                                <div>Ore impiegate</div>
                                <asp:TextBox ID="DayLogDuration" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)" MaxLength=2></asp:TextBox>
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
                            <td colspan=3>
                                <div>Motivazione ritardo (se in essere)</div>
                                <asp:TextBox ID="DayLogDelay" runat=server TextMode=MultiLine Height="50" CssClass="BoxDesign"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                           <td width="34%" valign=top>
                                <div>Sezione</div>
                                <asp:DropDownList ID="DayLogSection" runat=server CssClass="BoxDesign"></asp:DropDownList>
                           </td>
                           <td width="33%" valign=top>
                                <div>To Do</div>
                                <asp:DropDownList ID="DayLogTodo" runat=server CssClass="BoxDesign"></asp:DropDownList>
                           </td>
                           <td width="33%">
                                <table cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td width="60%">
                                            Data fine pianificata
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DayLogEndDate" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="60%">
                                            Ore previste
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DayLogPlanned" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="60%">
                                            Ore effettuate
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DayLogCurrent" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="60%">
                                            Avanzamento attuale %
                                        </td>
                                        <td>
                                            <asp:TextBox ID="DayLogProgress" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)" ReadOnly></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                           </td>
                        </tr>
                        <tr>
                            <td colspan=3 align=right style="border-top:1px solid black">
                                <table cellpadding=0 cellspacing=0 width="100%">
                                    <tr>
                                        <td colspan=3 style="padding-top:5px;padding-bottom:5px">
                                            Se prevedi che le ore previste non siano sufficienti o siano troppe indica le ore in pi o in meno, la motivazione, e eventualmente una nuova data di presunta fine del lavoro.
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign=top  width="25%" nowrap>
                                            <div>Ore</div>
                                            <table>
                                                <tr>
                                                <td>
                                                    <asp:RadioButtonList ID="plusminus" runat=server RepeatColumns=2 RepeatDirection=Horizontal>
                                                        <asp:ListItem Text="+" Selected=True></asp:ListItem>
                                                        <asp:ListItem Text="-"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="AddMinute" runat=server CssClass="BoxDesign" onkeypress="NumbersOnly(event,'',this)" Width="50"></asp:TextBox>
                                                </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td valign=top width="25%">
                                            <div>Data</div>
                                            <table width="100%" cellspacing=0 cellpadding=0>
										        <tr><td>
											        <asp:TextBox id="NewData" runat="server" Width="100%"  CssClass="BoxDesign" onkeypress="DataCheck(this,event)"></asp:TextBox>
										        </td>
										        <td width="30">
											        &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=NewData&Start='+(document.getElementById('NewData')).value,event,195,195)">
										        </td>
										        </tr>
									        </table>
                                        </td>
                                        <td valign=top width="50%">
                                            <div>Motivazione</div>
                                            <asp:TextBox ID="VariationDescription" runat=server TextMode=MultiLine Height="50px" CssClass="BoxDesign" MaxLength="250"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=3 align=right>
                                <asp:LinkButton ID="DayLogSave" runat=server CssClass="Save">Salva</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                    </twc:TustenaTab>
                    </twc:TustenaTabber>

                    <div id="spanTiming" runat=server visible=false  style="padding-left:5px">
                                <table width="60%" cellspacing=0 cellpadding=0>
			                            <tr>
			                            <td>
			                                <asp:TextBox id="TimeOwnerID" runat=server style="display:none"></asp:TextBox>
			                                <asp:TextBox id="TimeOwnerType" runat=server style="display:none"></asp:TextBox>
			                                <asp:TextBox id="TimeOwnerRealID" runat=server style="display:none" Text="0"></asp:TextBox>
			                                <asp:TextBox id="TimeOwnerTeam" runat=server style="display:none"></asp:TextBox>

			                                <asp:TextBox id="TimeOwner" runat=server CssClass="BoxDesign" ReadOnly></asp:TextBox>
			                            </td>
			                            <td width="60">
			                                 &nbsp;<img id="imgTiming" runat=server src="/i/user.gif" border="0" style="cursor:pointer" />
			                                 &nbsp;<img src="/i/persons.gif" border="0" style="cursor:pointer" onclick="allmembers()" />
			                            </td>
			                            <td width="50%">
			                                <asp:LinkButton ID="btnViewTiming" runat=server CssClass="Save">Visualizza tempi</asp:LinkButton>
			                            </td>
			                        </tr>
                                </table>
                            <table width="95%" cellspacing=0 cellpadding=0 align=center>
			                     <tr>
			                        <td>
                                        <asp:Literal ID="lblTiming" runat=server></asp:Literal>
                                    </td>
                                 </tr>
                            </table>
                    </div>
                </td>
             </tr>
    </table>


    </form>
</body>
</html>
