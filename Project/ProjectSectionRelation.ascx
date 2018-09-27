<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectSectionRelation.ascx.cs" Inherits="Digita.Tustena.Project.ProjectSectionRelation" %>

<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<twc:jscontrolid id="jsc" runat=server Identifier="Relation"/>
<script type="text/javascript" src="/js/clone.js"></script>
<script>
var idcrelation=1;

function addRelation()
{
	cloneObj('RelationItem',idcrelation++,'RelationContainer');
	var tempidcdoc=idcrelation-1;
	clearRelation('_'+(tempidcdoc));
}

function removeRelation(cloneparam1)
{
    var d = document.getElementById("RelationToDelete");
	if(cloneparam1.indexOf('_')>0)
	{
	   var suffix=cloneparam1.substr(cloneparam1.indexOf('_'));
	   d.value += document.getElementById("RelationId"+suffix).value+"|";
	}else
	   d.value += document.getElementById("RelationId").value+"|";
	removeCloned(cloneparam1,'RelationContainer');
	idcrelation--;
	if(idcrelation<1)
		clearRelation('');
}

function clearRelation(suffix)
{
	document.getElementById("RelationSection1"+suffix).selectedIndex=0;
	document.getElementById("RelationSection2"+suffix).selectedIndex=0;
	document.getElementById("RelationId"+suffix).value="-1";
	document.getElementById("RelationDelay"+suffix).value="";
}

function FillRelation(suffix,r1,r2,type,d,id)
{
    var evs1=null;
    var evs1=null;
    if(suffix>0){
        document.getElementById("RelationType_"+suffix).selectedIndex=type;
	    document.getElementById("RelationDelay_"+suffix).value=d;
	    document.getElementById("RelationId_"+suffix).value=id;
	    evs1 = document.getElementById("RelationSection1_"+suffix);
	    evs2 = document.getElementById("RelationSection2_"+suffix);

    }else{
	    document.getElementById("RelationType").selectedIndex=type;
	    document.getElementById("RelationDelay").value=d;
	    document.getElementById("RelationId").value=id;
	    evs1 = document.getElementById("RelationSection1");
	    evs2 = document.getElementById("RelationSection2");
	}

	evs1.selectedIndex=-1;
	    for(i=0;i<evs1.options.length;i++)
	    {
	        if(r1==evs1.options[i].value)
	        {
	            evs1.selectedIndex=i;
	            break;
	        }
	    }
	evs2.selectedIndex=-1;
	    for(i=0;i<evs2.options.length;i++)
	    {
	        if(r2==evs2.options[i].value)
	        {
	            evs2.selectedIndex=i;
	            break;
	        }
	    }
}
</script>

<table cellpadding=0 cellspacing=0 class=normal width="100%">
    <tr>
        <td>Relazioni&nbsp;<img src=/i/plus.gif onclick="addRelation();" style="cursor:pointer;">
        <input type=text id="RelationToDelete" name="RelationToDelete" >
        </td>
    </tr>
    <tr>
        <td id="RelationContainer" width="100%">
         <table cellpadding=0 cellspacing=5 id="RelationItem" width="100%" style="border-bottom:1px solid #000000">
            <tr>
                <td colspan=2>
                    Relazione&nbsp;<img src=/i/erase.gif cloneparam1="RelationItem" onclick="removeRelation(this.getAttribute('cloneparam1'));" style="cursor:pointer;">
                </td>
            </tr>
            <tr>
                <td valign=top>
                    <div>Sezione 1</div>
                    <asp:Literal ID="litSection1" runat=server></asp:Literal>
                </td>
                <td>
                    <div>Tipo relazione</div>
                    <select id="RelationType" name="RelationType" old="true">
                        <option value=0>Inizio-Inizio</option>
                        <option value=1>Fine-Inizio</option>
                    </select>
                    <input type=text id="RelationId" name="RelationId" style="display:none" value="-1">
                    Ritardo <input type=text id="RelationDelay" name="RelationDelay" class="BoxDesign" onkeypress="NumbersOnly(event,'',this)" maxlength="2" style="width:20px">
                </td>
                <td>
                    <div>Sezione 2</div>
                    <asp:Literal ID="litSection2" runat=server></asp:Literal>
                </td>
            </tr>
         </table>
        </td>
    </tr>
</table>
