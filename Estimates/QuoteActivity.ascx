<%@ Control Language="c#" AutoEventWireup="false" Codebehind="QuoteActivity.ascx.cs" Inherits="Digita.Tustena.Estimates.QuoteActivity" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="pschema" TagName="ProductSchema" Src="~/Catalog/ProductSchema.ascx" %>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<script>
var cl=0;
function ValidateProduct(){
	var ProductID = getElement("<%=this.ID%>_EstProductID");
	if(ProductID.value!=""){
			getElement("rvProductID").style.display="none";
			return true;
	}else{
			getElement("rvProductID").style.display="inline";
			return false;
	}
}

function ValidateFreeProduct(){
	var FreeProduct = getElement("<%=this.ID%>_EstFreeProduct");
	var FreeQta = getElement("<%=this.ID%>_EstFreeQta");
	var FreeUp = getElement("<%=this.ID%>_EstFreeUp");
	var FreeReduct = getElement("<%=this.ID%>_EstFreeReduct");
	var FreeVat = getElement("<%=this.ID%>_EstFreeVat");
	var pass=true;
	if(FreeProduct.value!=""){
			getElement("AxkFreeProduct").style.display="none";
	}else{
			getElement("AxkFreeProduct").style.display="inline";
			pass=false;
	}
	if(FreeQta.value!="" && FreeQta.value>0){
			getElement("AxkFreeQta").style.display="none";
	}else{
			getElement("AxkFreeQta").style.display="inline";
			pass=false;
	}
	if(FreeUp.value!="" && FreeUp.value>0){
			getElement("AxkFreeUp").style.display="none";
	}else{
			getElement("AxkFreeUp").style.display="inline";
			pass=false;
	}
	if(FreeReduct.value!="" && FreeReduct.value>-1){
			getElement("AxkFreeReduct").style.display="none";
	}else{
			getElement("AxkFreeReduct").style.display="inline";
			pass=false;
	}
	if(FreeVat.value!="" && FreeVat.value>-1){
			getElement("AxkFreeVat").style.display="none";
	}else{
			getElement("AxkFreeVat").style.display="inline";
			pass=false;
	}
	if(pass)
		return true;
	return false;
}

function EraseClient(x){
	(getElement("<%=this.ID%>_EstClientID")).value="";
	(getElement("<%=this.ID%>_EstClientName")).value="";
	cl=x;
}

function changecurrency(){
	var x = getElement("<%=this.ID%>_EstCurrency");
	var change = x.options[x.selectedIndex].value.split("|");
	var y = getElement("<%=this.ID%>_EstChange");
	y.value = change[1].replace(".",",");
}


function HidePart(objid,x)
{

   var obj = getElement("<%=this.ID%>_"+objid);
   var objimg = getElement("<%=this.ID%>_img" + objid);
   if (obj == null) return;

   if(x!=null){
		obj.style.display = 'none';
		objimg.src='/images/down.gif';
   }else{
		if (obj.style.display == 'none'){
			obj.style.display = '';
   			objimg.src='/images/up.gif';
		}else{
			obj.style.display = 'none';
			objimg.src='/images/down.gif';
		}
   }
}
function OpenCatalog(){
	CreateBox("/Common/PopCatalog.aspx?render=no&ptx=<%=this.ID%>_estProduct&pid=<%=this.ID%>_estProductID&um=<%=this.ID%>_estUm&qta=<%=this.ID%>_estQta&up=<%=this.ID%>_estUp&vat=<%=this.ID%>_estVat&pl=<%=this.ID%>_estPl&pf=<%=this.ID%>_estPf&ch="+document.getElementById("<%=this.ID%>_EstChange").value,event,400,300);
}

function ChangeLayout(obj){
	var obj = getElement(obj);
	var objnor = getElement("NormalLayout");
	var objacqua = getElement("AcquaLayout");
	var objistit = getElement("IstitLayout");

	if(objnor==obj){
		objnor.style.display='';
		objacqua.style.display='none';
		objistit.style.display='none';
	}else if(objacqua==obj){
		objnor.style.display='none';
		objacqua.style.display='';
		objistit.style.display='none';
	}else{
		objnor.style.display='none';
		objacqua.style.display='none';
		objistit.style.display='';
	}
}

function CalcFreePrice()
{
	var qta = getElement("<%=this.ID%>_EstFreeQta");
	var red = getElement("<%=this.ID%>_EstFreeReduct");
	var up = getElement("<%=this.ID%>_EstFreeUp");

	var newprice = FixNumeric(qta.value)*FixNumeric(up.value);
	if(red.value && red.value>0)
		newprice = newprice-(newprice/100*FixNumeric(red.value));

	var pf = getElement("<%=this.ID%>_EstFreePf");
	pf.value=FixCurrency(newprice);
}

function CalcCatalogPrice()
{
	var qta = getElement("<%=this.ID%>_EstQta");
	var up = getElement("<%=this.ID%>_EstUp");
	var red = getElement("<%=this.ID%>_EstReduc");
	var vat = getElement("<%=this.ID%>_EstVat");
	var Pl = getElement("<%=this.ID%>_EstPl");

	var valor = FixNumeric(up.value);

	var newprice = qta.value*valor;
	var pf = getElement("<%=this.ID%>_EstPf");

	if(red.value && red.value >0)
		newprice = newprice - (newprice/100*red.value);

	if(vat.value && vat.value >0)
		newprice = newprice + (newprice * vat.value)/100;

	Pl.value = FixCurrency(qta.value*valor);
	pf.value = FixCurrency(newprice);
}

function ShowProgressBar()
	{
		var progress = document.getElementById("divProgressBar");
		progress.style.display='';
	}
</script>
<table width="80%" cellspacing="2" cellpadding="0" align="center" class="normal">
	<tr>
		<td width="20%">
			<div><twc:LocalizedLiteral text="Esttxt8" runat="server"/></div>
			<asp:DropDownList id="EstCurrency" runat="server" CssClass="BoxDesign"></asp:DropDownList>
		</td>
		<td width="20%">
			<div><twc:LocalizedLiteral text="Esttxt9" runat="server"/>
				<domval:CompareDomValidator id="valestChange" runat="server" ControlToValidate="EstChange" ValueToCompare="0"
					Type="Double" Operator="GreaterThanEqual" ErrorMessage="*" Display="dynamic">*
					</domval:CompareDomValidator>
			</div>
			<asp:TextBox id="EstChange" runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
		<td width="20%">
			<div><twc:LocalizedLiteral text="Esttxt39" runat="server"/></div>
			<asp:TextBox id="EstGlobalReduction" runat="server" CssClass="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)"></asp:TextBox>
		</td>
	</tr>
</table>
<table id="EstimateTable2" border="0" cellspacing="0" width="80%" align="center" class="normal">
	<tr>
		<td width="100%" class="GridTitle"><twc:LocalizedLiteral text="Esttxt15" runat="server"/></td>
		<td class="GridTitle" align="right">
			<img src="/images/up.gif" id="imgcatalogestimate" runat="server" onclick="javascript:HidePart('catalogestimate')"
				style="CURSOR:pointer">
		</td>
	</tr>
	<tr id="catalogestimate" style="DISPLAY:inline" runat="server">
		<td colspan="2">
			<table border="0" cellspacing="0" width="100%" align="center" class="normal" style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
				<tr>
					<td width="30%">
						<div><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/>
							<span id="rvProductID" class="normal" style="DISPLAY:none;COLOR:red">*</span>
						</div>
						<table width="100%" cellspacing="0" cellpadding="0">
							<tr>
								<td valign="top">
									<asp:TextBox id="EstProductID" runat="server" class="BoxDesign" style="DISPLAY:none" />
									<asp:TextBox id="EstProduct" runat="server" class="BoxDesign" ReadOnly="true" />
								</td>
								<td width="30" valign="top">
									&nbsp;<img src="/i/lookup.gif" border="0" style="CURSOR:pointer" onclick="OpenCatalog()">
								</td>
							</tr>
						</table>
					</td>
					<td width="5%">
						<div><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></div>
						<asp:TextBox id="EstUm" runat="server" ReadOnly="true" class="BoxDesign" />
					</td>
					<td width="5%">
						<div><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></div>
						<asp:TextBox id="EstQta" runat="server" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="20%">
						<div><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></div>
						<asp:TextBox id="EstUp" runat="server" ReadOnly="true" class="BoxDesign" />
					</td>
					<td width="10%">
						<div><twc:LocalizedLiteral text="Esttxt39" runat="server"/>&nbsp;%</div>
						<asp:TextBox id="EstReduc" runat="server" class="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="10%">
						<div><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/>&nbsp;%</div>
						<asp:TextBox id="EstVat" runat="server" class="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="20%">
						<div><twc:LocalizedLiteral text="CRMcontxt70" runat="server"/></div>
						<asp:TextBox id="EstPl" runat="server" ReadOnly="true" class="BoxDesign" />
					</td>
				</tr>
				<tr>
					<td colspan="6">
						<div><twc:LocalizedLiteral text="CRMcontxt72" runat="server"/></div>
						<asp:TextBox id="EstDescription2" runat="server" class="BoxDesign" />
					</td>
					<td>
						<div><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></div>
						<asp:TextBox id="EstPf" runat="server" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<span style="CURSOR:pointer;TEXT-DECORATION:underline" onclick="CalcCatalogPrice()">
							<twc:LocalizedLiteral text="Esttxt16" runat="server"/></span>
					</td>
					<td colspan="4" align="right">
						<asp:LinkButton id="BtnAddProduct" runat="server" cssclass="normal" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td style="FONT-SIZE:1px;PADDING-BOTTOM:10px">&nbsp;</td>
	</tr>
	<tr>
		<td width="100%" class="GridTitle" style="HEIGHT: 22px"><twc:LocalizedLiteral text="Esttxt29" runat="server"/></td>
		<td class="GridTitle" align="right" style="HEIGHT: 22px">
			<img src="/images/up.gif" id="imgfreeestimate" runat="server" onclick="javascript:HidePart('freeestimate')"
				style="CURSOR:pointer">
		</td>
	</tr>
	<tr id="freeestimate" runat="server" style="DISPLAY:inline">
		<td colspan="2">
			<table border="0" cellspacing="0" width="100%" align="center" class="normal" style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
				<tr>
					<td width="30%" colspan="6">
						<div><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/>
							<span id="AxkFreeProduct" class="normal" style="DISPLAY:none;COLOR:red">*</span>
						</div>
						<asp:TextBox id="EstFreeProduct" runat="server" class="BoxDesign" />
					</td>
				</tr>
				<tr>
					<td width="5%">
						<div><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></div>
						<asp:TextBox id="EstFreeUm" runat="server" class="BoxDesign" />
					</td>
					<td width="5%">
						<div><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/>
							<span id="AxkFreeQta" class="normal" style="DISPLAY:none;COLOR:red">*</span>
						</div>
						<asp:TextBox id="EstFreeQta" runat="server" value="1" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="20%">
						<div><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/>
							<span id="AxkFreeUp" class="normal" style="DISPLAY:none;COLOR:red">*</span>
						</div>
						<asp:TextBox id="EstFreeUp" runat="server" class="BoxDesign" />
					</td>
					<td width="10%">
						<div><twc:LocalizedLiteral text="Esttxt39" runat="server"/>&nbsp;% <span id="AxkFreeReduct" class="normal" style="DISPLAY:none;COLOR:red">
								*</span>
						</div>
						<asp:TextBox id="EstFreeReduct" runat="server" value="0" class="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="10%">
						<div><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/>&nbsp;% <span id="AxkFreeVat" class="normal" style="DISPLAY:none;COLOR:red">
								*</span>
						</div>
						<asp:TextBox id="EstFreeVat" runat="server" class="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
					<td width="20%">
						<div><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></div>
						<asp:TextBox id="EstFreePf" runat="server" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
					</td>
				</tr>
				<tr>
					<td colspan="2" align="left" nowrap>
						<span style="CURSOR:pointer;TEXT-DECORATION:underline" onclick="CalcFreePrice()">
							<twc:LocalizedLiteral text="Esttxt16" runat="server"/>
						</span>
					</td>
					<td colspan="4" align="right">
						<asp:LinkButton id="BtnAddFreeProductOpp" runat="server" cssclass="normal" />
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td style="FONT-SIZE:1px;PADDING-BOTTOM:10px">&nbsp;</td>
	</tr>
	<tr>
		<td colspan="2" class="GridTitle"><twc:LocalizedLiteral text="Esttxt31" runat="server"/></td>
	</tr>
	<tr>
		<td colspan="2">
			<table border="0" cellspacing="0" width="100%" align="center" class="normal" style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
				<TBODY>
					<tr>
						<td>
							<asp:Repeater id="RepeaterEstProduct" runat="server">
								<HeaderTemplate>
									<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
										<tr>
											<td class="GridTitle" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
											<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
											<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
											<td class="GridTitle" width="20%" align="right"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
											<td class="GridTitle" width="5%" align="right"><twc:LocalizedLiteral text="Esttxt39" runat="server"/></td>
											<td class="GridTitle" width="5%" align="right"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
											<td class="GridTitle" width="19%" align="right"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
											<td class="GridTitle" width="1%">&nbsp;</td>
										</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td class="GridItem" width="30%"><asp:Literal id="ShortDescription" runat="server" /></td>
										<td class="GridItem" width="10%"><asp:Literal id="UM" runat="server" /></td>
										<td class="GridItem" width="10%"><asp:Literal id="Qta" runat="server" /></td>
										<td class="GridItem" width="20%" align="right"><asp:Literal id="UnitPrice" runat="server" /></td>
										<td class="GridItem" width="5%" align="right"><asp:Literal id="Reduction" runat="server" />%</td>
										<td class="GridItem" width="5%" align="right"><asp:Literal id="Vat" runat="server" />%</td>
										<td class="GridItem" width="19%" align="right"><asp:Literal id="FinalPrice" runat="server" /></td>
										<td class="GridItem" width="1%">
											<asp:LinkButton id="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
											<asp:Literal id="ObjectID" runat="server" visible="false" />
											&nbsp;
										</td>
									</tr>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<tr>
										<td class="GridItemAltern" width="30%"><asp:Literal id="ShortDescription" runat="server" /></td>
										<td class="GridItemAltern" width="10%"><asp:Literal id="UM" runat="server" /></td>
										<td class="GridItemAltern" width="10%"><asp:Literal id="Qta" runat="server" /></td>
										<td class="GridItemAltern" width="20%" align="right"><asp:Literal id="UnitPrice" runat="server" /></td>
										<td class="GridItemAltern" width="5%" align="right"><asp:Literal id="Reduction" runat="server" />%</td>
										<td class="GridItemAltern" width="5%" align="right"><asp:Literal id="Vat" runat="server" />%</td>
										<td class="GridItemAltern" width="19%" align="right"><asp:Literal id="FinalPrice" runat="server" /></td>
										<td class="GridItemAltern" width="1%">
											<asp:LinkButton id="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
											<asp:Literal id="ObjectID" runat="server" visible="false" />
											&nbsp;
										</td>
									</tr>
								</AlternatingItemTemplate>
								<FooterTemplate>
									<tr>
										<td class="GridTitle" colspan="6"><twc:LocalizedLiteral text="Esttxt44" runat="server"/></td>
										<td class="GridTitle" style="TEXT-ALIGN: right">
											<asp:Literal ID="TotalQuote" Runat="server"></asp:Literal>
										</td>
										<td class="GridTitle">&nbsp;</td>
									</tr>
			</table>
			</FooterTemplate> </asp:Repeater>
		</td>
	</tr>
</table></TD></TR>
<tr>
	<td style="FONT-SIZE:1px;PADDING-BOTTOM:10px">&nbsp;</td>
</tr>
<tr>
	<td width="100%" class="GridTitle"><twc:LocalizedLiteral text="Esttxt42" runat="server"/></td>
	<td class="GridTitle" align="right">
		<img src="/images/down.gif" id="imgForPrint" runat="server" onclick="javascript:HidePart('ForPrint')"
			style="CURSOR:pointer">
	</td>
</tr>
<tr id="ForPrint" runat="server" style="DISPLAY:none">
	<td>
		<table width="100%" class="normal">
			<tr>
				<td width="40%">
					<div><twc:LocalizedLiteral text="Esttxt40" runat="server"/></div>
					<asp:DropDownList ID="EstTemplateFile" Runat="server" CssClass="BoxDesign"></asp:DropDownList>
				</td>
				<td width="45%">
					<div><twc:LocalizedLiteral text="Esttxt41" runat="server"/></div>
					<Upload:InputFile id="NewTemplate" runat="server" Class="BoxDesign" />
					<div id="divProgressBar" style="DISPLAY:none">
						<Upload:ProgressBar id="inlineProgressBar" runat="server" Triggers="BtnLoadTemplate">
<asp:Label id=label runat="server" Text="Check Progress"></asp:Label>
						</Upload:ProgressBar>
					</div>
				</td>
				<td width="10%" align="right" valign="bottom">
					<asp:LinkButton ID="BtnLoadTemplate" Runat="server" CssClass="Save"></asp:LinkButton>
				</td>
				<td width="5%" align="right" valign="bottom">
					<asp:LinkButton ID="EstPrint" Runat="server">
						<img src="/i/printer.gif" border="0"></asp:LinkButton>
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<div><twc:LocalizedLiteral text="Esttxt43" runat="server"/></div>
					<input type="radio" name="LayOut" value="normal.css" checked onclick="ChangeLayout('NormalLayout');">Normal
					<input type="radio" name="LayOut" value="acqua.css" onclick="ChangeLayout('AcquaLayout');">Acqua
					<input type="radio" name="LayOut" value="istituzionale.css" onclick="ChangeLayout('IstitLayout');">Istitutional
				</td>
			</tr>
			<tr>
				<td colspan="3">
					<span id="NormalLayout" style="DISPLAY:inline">
						<style>
					.GridTitlePrint { BORDER-RIGHT: #c0c0c0 1px solid; BORDER-TOP: #c0c0c0 1px solid; FONT-SIZE: 11px; BORDER-LEFT: #c0c0c0 1px solid; BORDER-BOTTOM: black 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; HEIGHT: 18px; BACKGROUND-COLOR: #dddddd; TEXT-ALIGN: left }
					.GridItemPrint { BORDER-RIGHT: #f2f2f2 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #ffffff }
					.GridItemAlternPrint { BORDER-RIGHT: #ffffff 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #f2f2f2 }
					</style>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
							<tr>
								<td class="GridTitlePrint" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
								<td class="GridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
								<td class="GridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
								<td class="GridTitlePrint" width="20%" align="right"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
								<td class="GridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="Esttxt39" runat="server"/></td>
								<td class="GridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
								<td class="GridTitlePrint" width="19%" align="right"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
							</tr>
							<tr>
								<td class="GridItemPrint" width="30%">
									Test Product</td>
								<td class="GridItemPrint" width="10%">
									Mt</td>
								<td class="GridItemPrint" width="10%">
									100</td>
								<td class="GridItemPrint" width="20%" align="right">
									1.500,00</td>
								<td class="GridItemPrint" width="5%" align="right">
									3%</td>
								<td class="GridItemPrint" width="5%" align="right">
									20%</td>
								<td class="GridItemPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
							<tr>
								<td class="GridItemAlternPrint" width="30%">
									Test Product</td>
								<td class="GridItemAlternPrint" width="10%">
									Mt</td>
								<td class="GridItemAlternPrint" width="10%">
									100</td>
								<td class="GridItemAlternPrint" width="20%" align="right">
									1.500,00</td>
								<td class="GridItemAlternPrint" width="5%" align="right">
									3%</td>
								<td class="GridItemAlternPrint" width="5%" align="right">
									20%</td>
								<td class="GridItemAlternPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
						</table>
					</span><span id="AcquaLayout" style="DISPLAY:none">
						<style>
					.AcquaGridTitlePrint { BORDER-RIGHT: #c0c0c0 1px solid; BORDER-TOP: #c0c0c0 1px solid; FONT-SIZE: 11px; BORDER-LEFT: #c0c0c0 1px solid; BORDER-BOTTOM: black 1px solid; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; HEIGHT: 18px; BACKGROUND-COLOR: #a3c7ed; TEXT-ALIGN: left }
					.AcquaGridItemPrint { BORDER-RIGHT: #f2f2f2 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #ffffff }
					.AcquaGridItemAlternPrint { BORDER-RIGHT: #ffffff 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Verdana, Arial, Helvetica, sans-serif; BACKGROUND-COLOR: #f2f2f2 }
					</style>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
							<tr>
								<td class="AcquaGridTitlePrint" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="20%" align="right"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="Esttxt39" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
								<td class="AcquaGridTitlePrint" width="19%" align="right"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
							</tr>
							<tr>
								<td class="AcquaGridItemPrint" width="30%">
									Test Product</td>
								<td class="AcquaGridItemPrint" width="10%">
									Mt</td>
								<td class="AcquaGridItemPrint" width="10%">
									100</td>
								<td class="AcquaGridItemPrint" width="20%" align="right">
									1.500,00</td>
								<td class="AcquaGridItemPrint" width="5%" align="right">
									3%</td>
								<td class="AcquaGridItemPrint" width="5%" align="right">
									20%</td>
								<td class="AcquaGridItemPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
							<tr>
								<td class="AcquaGridItemAlternPrint" width="30%">
									Test Product</td>
								<td class="AcquaGridItemAlternPrint" width="10%">
									Mt</td>
								<td class="AcquaGridItemAlternPrint" width="10%">
									100</td>
								<td class="AcquaGridItemAlternPrint" width="20%" align="right">
									1.500,00</td>
								<td class="AcquaGridItemAlternPrint" width="5%" align="right">
									3%</td>
								<td class="AcquaGridItemAlternPrint" width="5%" align="right">
									20%</td>
								<td class="AcquaGridItemAlternPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
						</table>
					</span><span id="IstitLayout" style="DISPLAY:none">
						<style>
					.IstitGridTitlePrint { BORDER-RIGHT: #c0c0c0 1px solid; BORDER-TOP: #c0c0c0 1px solid; FONT-SIZE: 11px; BORDER-LEFT: #c0c0c0 1px solid; BORDER-BOTTOM: black 2px solid; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif; HEIGHT: 18px; BACKGROUND-COLOR: #ffffff; TEXT-ALIGN: left }
					.IstitGridItemPrint { BORDER-RIGHT: #f2f2f2 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif; BACKGROUND-COLOR: #ffffff }
					.IstitGridItemAlternPrint { BORDER-RIGHT: #ffffff 1px solid; FONT-SIZE: 11px; FONT-FAMILY: Arial, Verdana, Helvetica, sans-serif; BACKGROUND-COLOR: #f2f2f2 }
					</style>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
							<tr>
								<td class="IstitGridTitlePrint" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="20%" align="right"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="Esttxt39" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
								<td class="IstitGridTitlePrint" width="19%" align="right"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
							</tr>
							<tr>
								<td class="IstitGridItemPrint" width="30%">
									Test Product</td>
								<td class="IstitGridItemPrint" width="10%">
									Mt</td>
								<td class="IstitGridItemPrint" width="10%">
									100</td>
								<td class="IstitGridItemPrint" width="20%" align="right">
									1.500,00</td>
								<td class="IstitGridItemPrint" width="5%" align="right">
									3%</td>
								<td class="IstitGridItemPrint" width="5%" align="right">
									20%</td>
								<td class="IstitGridItemPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
							<tr>
								<td class="IstitGridItemAlternPrint" width="30%">
									Test Product</td>
								<td class="IstitGridItemAlternPrint" width="10%">
									Mt</td>
								<td class="IstitGridItemAlternPrint" width="10%">
									100</td>
								<td class="IstitGridItemAlternPrint" width="20%" align="right">
									1.500,00</td>
								<td class="IstitGridItemAlternPrint" width="5%" align="right">
									3%</td>
								<td class="IstitGridItemAlternPrint" width="5%" align="right">
									20%</td>
								<td class="IstitGridItemAlternPrint" width="19%" align="right">
									145.500,00
								</td>
							</tr>
						</table>
					</span>
				</td>
			</tr>
		</table>
		<pschema:ProductSchema id="ProductSchemaPrint" runat="server"></pschema:ProductSchema>
		<asp:Repeater id="RepeaterEstProductForPrint" runat="server">
			<HeaderTemplate>
				<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
					<tr>
						<td class="GridTitlePrint" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
						<td class="GridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
						<td class="GridTitlePrint" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
						<td class="GridTitlePrint" width="20%" align="right"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
						<td class="GridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="Esttxt39" runat="server"/></td>
						<td class="GridTitlePrint" width="5%" align="right"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
						<td class="GridTitlePrint" width="19%" align="right"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
					</tr>
			</HeaderTemplate>
			<ItemTemplate>
				<tr>
					<td class="GridItemPrint" width="30%">
						<asp:Literal id="ShortDescription" runat="server" /></td>
					<td class="GridItemPrint" width="10%">
						<asp:Literal id="UM" runat="server" /></td>
					<td class="GridItemPrint" width="10%">
						<asp:Literal id="Qta" runat="server" /></td>
					<td class="GridItemPrint" width="20%" align="right">
						<asp:Literal id="UnitPrice" runat="server" /></td>
					<td class="GridItemPrint" width="5%" align="right">
						<asp:Literal id="Reduction" runat="server" />%</td>
					<td class="GridItemPrint" width="5%" align="right">
						<asp:Literal id="Vat" runat="server" />%</td>
					<td class="GridItemPrint" width="19%" align="right">
						<asp:Literal id="FinalPrice" runat="server" />
						<asp:Literal id="ObjectID" runat="server" visible="false" />
					</td>
				</tr>
			</ItemTemplate>
			<AlternatingItemTemplate>
				<tr>
					<td class="GridItemAlternPrint" width="30%">
						<asp:Literal id="ShortDescription" runat="server" /></td>
					<td class="GridItemAlternPrint" width="10%">
						<asp:Literal id="UM" runat="server" /></td>
					<td class="GridItemAlternPrint" width="10%">
						<asp:Literal id="Qta" runat="server" /></td>
					<td class="GridItemAlternPrint" width="20%" align="right">
						<asp:Literal id="UnitPrice" runat="server" /></td>
					<td class="GridItemAlternPrint" width="5%" align="right">
						<asp:Literal id="Reduction" runat="server" />%</td>
					<td class="GridItemAlternPrint" width="5%" align="right">
						<asp:Literal id="Vat" runat="server" />%</td>
					<td class="GridItemAlternPrint" width="19%" align="right">
						<asp:Literal id="FinalPrice" runat="server" />
						<asp:Literal id="ObjectID" runat="server" visible="false" />
					</td>
				</tr>
			</AlternatingItemTemplate>
			<FooterTemplate>
				<tr>
					<td class="GridTitlePrint" colspan="6"><twc:LocalizedLiteral text="Esttxt44" runat="server"/></td>
					<td class="GridTitlePrint" style="TEXT-ALIGN: right">
						<asp:Literal ID="TotalQuote" Runat="server"></asp:Literal>
					</td>
				</tr>
				</table>
			</FooterTemplate>
		</asp:Repeater>
	</td>
</tr></TBODY></TABLE></TD></TR></TBODY></TABLE>
