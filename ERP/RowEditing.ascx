<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" AutoEventWireup="true" Codebehind="RowEditing.ascx.cs" Inherits="Digita.Tustena.ERP.RowEditing" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script type="text/javascript" src="/js/dynabox.js"></script>
<script type="text/javascript" src="/js/autodate.js"></script>
<script type="text/javascript" src="/js/clone.js"></script>
<style>
.TableRowsTD { BORDER-BOTTOM: #000 1px solid }
</style>
<twc:LocalizedScript ID="LocalizedScript1" resource="Esttxt47" runat="server" />
<SCRIPT>var idc=1;</SCRIPT>
<script>
function OpenCatalog(cloneparam1,cloneparam2,cloneparam3,cloneparam4,cloneparam5,cloneparam6,cloneparam7,cloneparam8,cloneparam9,cloneparam10)
{
    var currentlist =  document.getElementById("currentPriceList");
	CreateBox("/Common/PopAddProduct.aspx?render=no&list="+currentlist.value+"&ptx="+cloneparam1+"&pid="+cloneparam2+"&um="+cloneparam3+"&qta="+cloneparam4+"&up="+cloneparam5+"&vat="+cloneparam6+"&pl="+cloneparam7+"&pf="+cloneparam8+"&cost="+cloneparam9+"&reallist="+cloneparam10+"&ch="+document.getElementById("<%=this.ID%>_EstChange").value,event,400,300);
}

function ViewProductSheet(o){

    var x = document.getElementById(o);
    var currentlist =  document.getElementById("currentPriceList");
    if(x.value.length>0 && x.value!="0")
        NewWindow('/catalog/printproduct.aspx?noprint=1&list='+currentlist.value+'&ProductID='+x.value,'PrintProduct',500,500,'no');
    else
        alert(Esttxt47);
}

function changecurrency(){
	var x = document.getElementById("<%=this.ID%>_EstCurrency");
	var change = x.options[x.selectedIndex].value.split("|");
	var y = document.getElementById("<%=this.ID%>_EstChange");
	y.value = change[1].replace(".",",");
}

function RemoveRow(cloneparam,container)
{
    removeCloned(cloneparam,container);
    idc--;
    if(idc<1)
		clearrow('');
    calctotal();
}

function RemoveAllRows()
{
    while(idc>0)
    {
        RemoveRow("TableRows_"+idc,"<%=this.ID%>_PanelRows");
    }
    clearrow('');
}

function clearrow(suffix)
{
	document.getElementById("estProduct"+suffix).value='';
	document.getElementById("estProductID"+suffix).value='';
	document.getElementById("estUm"+suffix).value='';
	document.getElementById("estQta"+suffix).value='1';
	document.getElementById("estVat"+suffix).selectedIndex=0;
	document.getElementById("estCost"+suffix).value='';
	document.getElementById("estCost"+suffix).readOnly=false;
    document.getElementById("estCost"+suffix).style.backgroundColor='';
	document.getElementById("estDiscount"+suffix).value='';
	document.getElementById("estPl"+suffix).value='';
	document.getElementById("estUp"+suffix).value='';
	document.getElementById("estNoVat"+suffix).value='';
	document.getElementById("estPf"+suffix).value='';
	document.getElementById("estRealListPrice"+suffix).value='';

}

function clearTotal()
{
    document.getElementById("shiptotal").value='';
    document.getElementById("subtotal").value='';
    document.getElementById("taxtotal").value='';
    document.getElementById("shiptotalwithvat").value='';
    document.getElementById("grandtotal").value='';
    document.getElementById("gain").value='';
    document.getElementById("percgain").value='';
}

function ChangeUp(id){
	var suffix = '';
	if(id.indexOf('_')>0)
	{
		suffix=id.substring(id.indexOf('_'));
	}
	var estUp = document.getElementById("estDiscount"+suffix);
	estUp.value="0";
	recalcDiscount(suffix,2);
	calctotal();
}

function ChangeDiscount(id){

	var suffix = '';
	if(id.indexOf('_')>0)
	{
		suffix=id.substring(id.indexOf('_'));
	}
	var estUp = document.getElementById("estUp"+suffix);
	estUp.value="0";
	recalcDiscount(suffix,1);

	calctotal();
}

function recalcDiscount(suffix,t){
	var estUp = document.getElementById("estUp"+suffix);
	var estPl = document.getElementById("estPl"+suffix);

	if(estUp.value.length<=0)
		estUp.value=estPl.value;
	else if(estPl.value.length<=0)
		estPl.value=estUp.value;

	var estDiscount = document.getElementById("estDiscount"+suffix);
	switch(t)
	{
	    case 0:
	        if(estUp.value!=estPl.value && estUp.value!="0")
	        {
		        estDiscount.value = FixCurrency((parseFloat(estPl.value)-parseFloat(estUp.value))*100/parseFloat(estPl.value),2,true,true);
	        }else if((estDiscount.value!='' || estDiscount.value!="0") && estUp.value=="0")
	        {
		        estUp.value = FixCurrency(parseFloat(estPl.value)-(parseFloat(estPl.value)*parseFloat(estDiscount.value)/100),2,true,true);
	        }
	        break;
	    case 1:
	        if((estDiscount.value!='' || estDiscount.value!="0") && estUp.value=="0")
	        {
		        estUp.value = FixCurrency(parseFloat(estPl.value)-(parseFloat(estPl.value)*parseFloat(estDiscount.value)/100),2,true,true);
	        }
	        break;
	    case 2:
	        if(estUp.value!=estPl.value && estUp.value!="0")
	        {
		        estDiscount.value = FixCurrency((parseFloat(estPl.value)-parseFloat(estUp.value))*100/parseFloat(estPl.value),2,true,true);
	        }
	        break;
	}
}

function calctotal()
{
	recalcDiscount('',0);

	var rowtotal = parseFloat(document.getElementById("estUp").value.replace(',','.')) * parseFloat(document.getElementById("estQta").value.replace(',','.'));
	document.getElementById("estNoVat").value=FixCurrency(rowtotal,2,true,true);

	var subtotal = rowtotal;
	var gain = rowtotal-(parseFloat(document.getElementById("estCost").value.replace(',','.')) * parseFloat(document.getElementById("estQta").value.replace(',','.')));
	var tax = parseFloat((document.getElementById("estVat").options[document.getElementById("estVat").selectedIndex]).value.replace(',','.'));
	var vat = subtotal*tax/100;
	document.getElementById("estPf").value=FixCurrency(rowtotal+vat,2,true,true);
	var ship = 0;
	if(parseFloat(document.getElementById("shiptotal").value)>0)
		ship=parseFloat(document.getElementById("shiptotal").value.replace(',','.'));
	else
		ship=0;
	var total =subtotal+vat;

	if(!isNumber(total))
	{
	    clearTotal();
		norow();
		return false;
	}

	for(i=1;i<idc;i++)
	{
		recalcDiscount('_'+i,0);
		rowtotal = parseFloat(document.getElementById("estUp_"+i).value.replace(',','.')) * parseFloat(document.getElementById("estQta_"+i).value.replace(',','.'));
		document.getElementById("estNoVat_"+i).value=FixCurrency(rowtotal,2,true,true);

		var partial = rowtotal;
		gain += rowtotal-(parseFloat(document.getElementById("estCost_"+i).value.replace(',','.')) * parseFloat(document.getElementById("estQta_"+i).value.replace(',','.')));
		tax = parseFloat((document.getElementById("estVat_"+i).options[document.getElementById("estVat_"+i).selectedIndex]).value.replace(',','.'));
		var vatpartial = partial*tax/100;
		document.getElementById("estPf_"+i).value=FixCurrency(rowtotal+vatpartial,2,true,true);
		subtotal+=partial;
		vat+=vatpartial
		total += partial+vatpartial;
	}

	document.getElementById("subtotal").value=FixCurrency(subtotal,2,true,true);
	document.getElementById("shiptotal").value=FixCurrency(ship,2,true,true);
	if(ship>0)
	{
		var shiptax = parseFloat((document.getElementById("shipVat").options[document.getElementById("shipVat").selectedIndex]).value.replace(',','.'));
		if(shiptax>0){
		    vat=vat+(ship*shiptax/100);
		    total += (ship*shiptax/100);
		    }
			//ship=ship+(ship*shiptax/100);
	}


	document.getElementById("taxtotal").value=FixCurrency(vat,2,true,true);
	document.getElementById("grandtotal").value=FixCurrency(total+ship,2,true,true);
	document.getElementById("shiptotalwithvat").value=FixCurrency(ship,2,true,true);
	document.getElementById("gain").value=FixCurrency(gain,2,true,true);
	document.getElementById("percgain").value=FixCurrency((gain*100/subtotal),2,true,true);

	return true;
}
</script>
<asp:RadioButtonList ID="SchemaType" Runat="server" CssClass="normal"></asp:RadioButtonList>
<asp:Panel ID="PanelRows" Runat="server">

	<TABLE cellSpacing="0" cellPadding="0" width="100%">
		<TR>
			<TD width="50%">
				<TABLE style="DISPLAY: none" cellSpacing="0" cellPadding="0" width="100%">
					<TR>
						<TD>
							<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL1" text="Esttxt8" runat="server"></TWC:LOCALIZEDLITERAL></TD>
						<TD>
							<asp:DropDownList id="EstCurrency" CssClass="BoxDesign" runat="server"></asp:DropDownList></TD>
						<TD>
							<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL2" text="Esttxt9" runat="server"></TWC:LOCALIZEDLITERAL></TD>
						<TD>
							<asp:TextBox id="EstChange" CssClass="BoxDesign" runat="server"></asp:TextBox></TD>
					</TR>
				</TABLE>
			</TD>
			<TD align="right"><SPAN class=Save style="CURSOR: pointer"
      onclick="cloneObj('TableRows',idc++,'<%=this.ID%>_PanelRows');var tempidc=idc-1;clearrow('_'+(tempidc));">
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL3" text="Quotxt24" runat="server"></TWC:LOCALIZEDLITERAL></SPAN><SPAN class="Save" style="CURSOR: pointer" onclick="calctotal();">
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL4" text="Quotxt25" runat="server"></TWC:LOCALIZEDLITERAL></SPAN></TD>
		</TR>
	</TABLE>
	<TABLE class="TableRowsTD" id="TableRows" width="100%">
		<TR id="grouprow">
			<TD></TD>
		</TR>
		<TR>
			<TD><IMG style="CURSOR: pointer"
      onclick="RemoveRow(this.getAttribute('cloneparam1'),'<%=this.ID%>_PanelRows');"
      src="/i/erase.gif" cloneparam1="TableRows">
			</TD>
			<TD vAlign="top" width="18%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL5" text="CRMcontxt65" runat="server"></TWC:LOCALIZEDLITERAL>
					<span onclick="ViewProductSheet(this.getAttribute('cloneparam1'))" cloneparam1="estProductID" style="cursor:pointer;"><img src="/i/lens.gif" border=0></span>
				</DIV>
				<INPUT class="BoxDesign" id="estProductID" style="DISPLAY: none" name="estProductID">
				<TEXTAREA class="BoxDesignReq" id="estProduct" name="estProduct" rows="3"></TEXTAREA>
			</TD>
			<TD vAlign="top" width="2%">
				<DIV>&nbsp;</DIV>
				<IMG style="CURSOR: pointer" onclick="OpenCatalog(''+this.getAttribute('cloneparam1')+'',''+this.getAttribute('cloneparam2')+'',''+this.getAttribute('cloneparam3')+'',''+this.getAttribute('cloneparam4')+'',''+this.getAttribute('cloneparam5')+'',''+this.getAttribute('cloneparam6')+'',''+this.getAttribute('cloneparam7')+'',''+this.getAttribute('cloneparam8')+'',''+this.getAttribute('cloneparam9')+'',''+this.getAttribute('cloneparam10')+'')"
					src="/i/lookup.gif" border="0" cloneparam1="estProduct" cloneparam9="estCost" cloneparam8="estNoVat"
					cloneparam7="estPl" cloneparam6="estVat" cloneparam5="estUp" cloneparam4="estQta"
					cloneparam3="estUm" cloneparam2="estProductID" cloneparam10="estRealListPrice">
			</TD>
			<TD vAlign="top" width="5%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL6" text="CRMcontxt66" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesign" id="estUm" maxLength="10" name="estUm">
			</TD>
			<TD vAlign="top" width="5%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL7" text="CRMcontxt67" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesignReq" onkeypress="NumbersOnly(event,'.,',this)" id="estQta" maxLength="10"
					onchange="calctotal()" name="estQta">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL8" text="CRMcontxt69" runat="server"></TWC:LOCALIZEDLITERAL>&nbsp;%</DIV>
				<asp:Literal id="litVat" Runat="server"></asp:Literal></TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL9" text="Captxt26" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" id="estCost" style="TEXT-ALIGN: right"
					maxLength="15" name="estCost">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL10" text="Esttxt39" runat="server"></TWC:LOCALIZEDLITERAL>&nbsp;%</DIV>
				<INPUT class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" id="estDiscount" maxLength="2"
					onchange="ChangeDiscount(this.id)" name="estDiscount">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL11" text="CRMcontxt70" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesignReq" onkeypress="NumbersOnly(event,'.,',this)" id="estPl" style="TEXT-ALIGN: right"
					maxLength="15" onchange="ChangeUp(this.id)" name="estPl">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL12" text="CRMcontxt68" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesignReq" onkeypress="NumbersOnly(event,'.,',this)" id="estUp" style="TEXT-ALIGN: right"
					maxLength="15" onchange="ChangeUp(this.id)" name="estUp">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL13" text="CRMcontxt71" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" id="estNoVat" style="TEXT-ALIGN: right"
					readOnly name="estNoVat">
			</TD>
			<TD vAlign="top" width="10%">
				<DIV>
					<TWC:LOCALIZEDLITERAL id="LOCALIZEDLITERAL14" text="CRMcontxt88" runat="server"></TWC:LOCALIZEDLITERAL></DIV>
				<INPUT class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" id="estPf" style="TEXT-ALIGN: right"
					readOnly name="estPf">
			    <INPUT id="estRealListPrice" name="estRealListPrice" style="TEXT-ALIGN: right;display:none">
			</TD>
		</TR>
	</TABLE>
</asp:Panel>
<table cellpadding="0" cellspacing="0" width="98%">
	<tr>
		<td width="70%" valign="top" align=left>
			<table cellpadding="0" cellspacing="0" width="50%">
				<tr>
					<td align="left" nowrap width="50%">
						<div><twc:LocalizedLiteral text="Quotxt22" runat="server" id="LocalizedLiteral15" /></div>
						<input type="text" id="shiptotal" name="shiptotal" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
					<TD vAlign="top" width="50%">
						<DIV><twc:LocalizedLiteral text="CRMcontxt69" runat="server" id="LocalizedLiteral16" />&nbsp;%</DIV>
						<asp:Literal id="litshipVat" Runat="server"></asp:Literal>
					</TD>

				</tr>
			</table>
		</td>
		<td width="30%">
			<table cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td align="right" nowrap>
						<twc:LocalizedLiteral text="Quotxt20" runat="server" id="LocalizedLiteral18" />:
						<input type="text" readonly id="subtotal" name="subtotal" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
				</tr>
				<tr>
					<td align="right" nowrap>
						<twc:LocalizedLiteral text="Quotxt21" runat="server" id="LocalizedLiteral19" />:
						<input type="text" readonly id="taxtotal" name="taxtotal" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
				</tr>
				<tr>
				    <td align="right" nowrap>
						<twc:LocalizedLiteral text="Quotxt22" runat="server" id="LocalizedLiteral17" />:
						<input type="text" id="shiptotalwithvat" name="shiptotalwithvat" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right"
							readonly>
					</td>
				</tr>
				<tr>
					<td align="right" nowrap>
						<twc:LocalizedLiteral text="Quotxt23" runat="server" id="LocalizedLiteral20" />:
						<input type="text" readonly id="grandtotal" name="grandtotal" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
				</tr>
				<tr>
					<td align="right" nowrap style="PADDING-TOP:20px">
						<twc:LocalizedLiteral text="Quotxt40" runat="server" id="LocalizedLiteral21" />:
						<input type="text" readonly id="gain" name="gain" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
				</tr>
				<tr>
					<td align="right" nowrap>
						<twc:LocalizedLiteral text="Quotxt41" runat="server" id="LocalizedLiteral22" />:
						<input type="text" readonly id="percgain" name="percgain" class="BoxDesign" style="WIDTH:100px;TEXT-ALIGN:right">
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
