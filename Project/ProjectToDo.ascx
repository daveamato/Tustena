<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectToDo.ascx.cs" Inherits="Digita.Tustena.Project.ProjectToDo" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<twc:jscontrolid id="jsc" runat=server Identifier="ToDo"/>
<script type="text/javascript" src="/js/clone.js"></script>
<script type="text/javascript" src="/js/tooltip.js"></script>

<script>
var idctodo=1;
        function addTodo()
		{
			cloneObj('ToDoItem',idctodo++,'ToDoContainer');
			var tempidcdoc=idctodo-1;
			clearToDo('_'+(tempidcdoc));
		}

		function removeTodo(cloneparam1)
		{
		    var d = document.getElementById("ToDoToDelete");
		    if(cloneparam1.indexOf('_')>0)
		    {
		        var suffix=cloneparam1.substr(cloneparam1.indexOf('_'));
		        d.value += document.getElementById("ToDoId"+suffix).value+"|";
		    }else
		        d.value += document.getElementById("ToDoId").value+"|";
			removeCloned(cloneparam1,'ToDoContainer');
			idctodo--;
			if(idctodo<1)
				clearToDo('');
		}

		function clearToDo(suffix)
	    {
				document.getElementById("JobTxt"+suffix).value='';
				document.getElementById("ToDoOwnerID"+suffix).value='';
				document.getElementById("ToDoOwner"+suffix).value='';
				document.getElementById("ToDoOwnerType"+suffix).value='';
				document.getElementById("ToDoOwnerRealID"+suffix).value='';
				document.getElementById("PlannedStartDate"+suffix).value='';
				document.getElementById("PlannedEndDate"+suffix).value='';
				document.getElementById("PlannedMinuteDuration"+suffix).value='';
				document.getElementById("RealStartDate"+suffix).value='';
				document.getElementById("RealEndDate"+suffix).value='';
				document.getElementById("CurrentMinuteDuration"+suffix).value='';
				document.getElementById("ToDoId"+suffix).value='-1';
				document.getElementById("ToDoProgress"+suffix).value='0';
				document.getElementById("ToDoWeight"+suffix).value='';

		}

		function FillToDo(suffix,jobtxt,todoownerid,todoowner,todoownertype,plannedstartdate,plannedenddate,plannedminuteduration,realstartdate,realenddate,currentminuteduration,todoid,progress,todoownerrealid,weight)
	    {
				document.getElementById("JobTxt"+suffix).value=jobtxt;
				document.getElementById("ToDoOwnerID"+suffix).value=todoownerid;
				document.getElementById("ToDoOwner"+suffix).value=todoowner;
				document.getElementById("ToDoOwnerType"+suffix).value=todoownertype;
				document.getElementById("ToDoOwnerRealID"+suffix).value=todoownerrealid;
				document.getElementById("PlannedStartDate"+suffix).value=plannedstartdate;
				document.getElementById("PlannedEndDate"+suffix).value=plannedenddate;
				document.getElementById("PlannedMinuteDuration"+suffix).value=plannedminuteduration;
				document.getElementById("RealStartDate"+suffix).value=realstartdate;
				document.getElementById("RealEndDate"+suffix).value=realenddate;
				document.getElementById("CurrentMinuteDuration"+suffix).value=currentminuteduration;
				document.getElementById("ToDoId"+suffix).value=todoid;
				document.getElementById("ToDoProgress"+suffix).value=progress;
				document.getElementById("ToDoWeight"+suffix).value=weight;
				if(progress==100)
				    document.getElementById("ToDoComplete"+suffix).checked=true;
		}

		function Complete(element,av)
		{
		    if(element.checked)
		        document.getElementById(av).value="100";
		    else
		        document.getElementById(av).value="";

		}


    function VisTiming(tblTime)
    {
        var t = document.getElementById(tblTime);
        if(t.style.display=='none')
            t.style.display='';
        else
            t.style.display='none';
    }


    function CheckDate(d)
    {
       var d1=getISODate(d);
       var d2=0;
       try
       {
        d2=getISODate(document.getElementById("ProjectSessions1_PlannedStartDate").value);
       } catch(e){}
       var d3=99999999;
       try
       {
        d3=getISODate(document.getElementById("ProjectSessions1_PlannedEndDate ").value);
       } catch(e){}

       if(d1<d2 || d1>d3)
        alert("Attenzione! La data inserita  fuori dal range della sezione");
    }


function CheckWeight()
{
    var w = parseInt(document.getElementById("ToDoWeight").value);
    for(var i=1;i<idctodo;i++)
        w += parseInt(document.getElementById("ToDoWeight_"+i).value);

    if(w!=100)
    {
        alert("Attenzione! la somma dei pesi delle azioni deve essere 100.\r\nIl totale attuale  "+w);
        return false;
    }else
        return true;
}

</script>
<table cellpadding=0 cellspacing=0 class=normal width="100%">
    <tr>
        <td>Azioni&nbsp;<img src=/i/plus.gif onclick="addTodo();" style="cursor:pointer;">
            <input type=text id="ToDoToDelete" name="ToDoToDelete" style="display:none">
        </td>
    </tr>
    <tr>
        <td id="ToDoContainer" width="100%">
            <table cellpadding=0 cellspacing=5 id="ToDoItem" width="100%" style="border-bottom:1px solid #000000">
                <tr>
                    <td valign=top width="30%">
                        <div>Azione&nbsp;<img src=/i/erase.gif cloneparam1="ToDoItem" onclick="removeTodo(this.getAttribute('cloneparam1'));" style="cursor:pointer;"></div>
                        <textarea id="JobTxt" name="JobTxt" class="BoxDesignReq" style="height:50px"></textarea>
                        <input type=text id="ToDoId" name="ToDoId" style="display:none" value="-1">
                        <table id=imgVariation cellpadding=0 cellspacing=0 width="100%">
                            <tr>
                                <td style="font-weight:bold;color:Red">Variazioni</td>
                            </tr>
                            <tr>
                                <td align=left width="100%">
                                <span id="VariationDetail" style="color:Red"></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td valign=top width="30%">
                        <div>Assegnata a</div>
                        <table width="100%" cellspacing=0 cellpadding=0>
			            <tr><td>
			                   <input type=text id="ToDoOwnerID" name="ToDoOwnerID" style="display:none">
			                   <input type=text id="ToDoOwnerType" name="ToDoOwnerType" style="display:none">
			                   <input type=text id="ToDoOwnerRealID" name="ToDoOwnerRealID" style="display:none">
			                   <input type=text id="ToDoOwner" name="ToDoOwner" Class="BoxDesign" ReadOnly>
			               </td>
			               <td width="30">
			                  &nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" cloneparam1="ToDoOwnerID" cloneparam2="ToDoOwner" cloneparam3="ToDoOwnerType" cloneparam4="ToDoOwnerRealID" onclick="CreateBox('/project/PopGetMember.aspx?render=no&MemberId='+this.getAttribute('cloneparam1')+'&MemberName='+this.getAttribute('cloneparam2')+'&MemberType='+this.getAttribute('cloneparam3')+'&MemberRealId='+this.getAttribute('cloneparam4')+'&ProjectId='+ToDoProject,event,400,400)">
			               </td>
			            </tr>
                        </table>
                        <table cellpadding=0 cellspacing=0 width="100%">
                            <tr>
                                <td width="50%">
                                    <div>Avanzamento %</div>
                                    <input type=text id="ToDoProgress" name="ToDoProgress" Class="BoxDesign" style="width:50%" maxlength=3 onkeypress="NumbersOnly(event,'',this)" readonly>
                                    <input type=checkbox id="ToDoComplete" value=1 cloneparam1="ToDoProgress" onclick="Complete(this,''+this.getAttribute('cloneparam1')+'')" />Completo
                                </td>
                                <td width="50%" valign=top>
                                    <div>Peso %</div>
                                    <input type=text id="ToDoWeight" name="ToDoWeight" Class="BoxDesign" style="width:50%" maxlength=3 onkeypress="NumbersOnly(event,'',this)">
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td valign=top align=right width="40%">
                        <table cellpadding=0 cellspacing=0 id="tblTiming" width="100%">
                            <tr>
                                <td width="40%">Data inizio pianificata</td>
                                <td width="60%">
                                    <table width="100%" cellspacing=0 cellpadding=0>
					                  <tr>
					                    <td>
							                <input type=text id="PlannedStartDate" name="PlannedStartDate" onblur="CheckDate(this.value)"  onkeypress="DataCheck(this,event)" class="BoxDesignReq">
						                </td>
						                <td width="30">
							                &nbsp;<img src="/i/SmallCalendar.gif" border="0" cloneparam1="PlannedStartDate" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+this.getAttribute('cloneparam1')+'&Start='+(document.getElementById(''+this.getAttribute('cloneparam1')+'')).value,event,195,195)">
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
							                <input type=text id="PlannedEndDate" name="PlannedEndDate" onkeypress="DataCheck(this,event)" class="BoxDesign">
						                </td>
						                <td width="30">
							                &nbsp;<img src="/i/SmallCalendar.gif" border="0" cloneparam1="PlannedEndDate" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+this.getAttribute('cloneparam1')+'&Start='+(document.getElementById(''+this.getAttribute('cloneparam1')+'')).value,event,195,195)">
						                </td>
					                  </tr>
					                </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">Tempo previsto (ore)</td>
                                <td width="60%">
                                    <input type=text ID="PlannedMinuteDuration" name="PlannedMinuteDuration" onkeypress="NumbersOnly(event,'',this)" class="BoxDesignReq">
                                </td>
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
							                <input type=text id="RealStartDate" name="RealStartDate" onkeypress="DataCheck(this,event)" class="BoxDesign">
						                </td>
						                <td width="30">
							                &nbsp;<img src="/i/SmallCalendar.gif" border="0" cloneparam1="RealStartDate" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+this.getAttribute('cloneparam1')+'&Start='+(document.getElementById(''+this.getAttribute('cloneparam1')+'')).value,event,195,195)">
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
							                <input type=text id="RealEndDate" name="RealEndDate" onkeypress="DataCheck(this,event)" class="BoxDesign">
						                </td>
						                <td width="30">
							                &nbsp;<img src="/i/SmallCalendar.gif" border="0" cloneparam1="RealEndDate" style="cursor:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox='+this.getAttribute('cloneparam1')+'&Start='+(document.getElementById(''+this.getAttribute('cloneparam1')+'')).value,event,195,195)">
						                </td>
					                  </tr>
					                </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="40%">Tempo attuale (ore)</td>
                                <td width="60%"><input type=text ID="CurrentMinuteDuration" name="CurrentMinuteDuration" onkeypress="NumbersOnly(event,'',this)" class="BoxDesign" readonly></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>

