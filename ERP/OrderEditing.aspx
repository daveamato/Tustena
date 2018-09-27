<%@ Register TagPrefix="uc1" TagName="RowEditing" Src="RowEditing.ascx" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Page language="c#" Codebehind="OrderEditing.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.ERP.OrderEditing" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head id="head" runat=server>
    <title>QuoteEditing</title>
    <script type="text/javascript" src="/js/autodate.js"></script>
    <script type="text/javascript" src="/js/clone.js"></script>
    <twc:LocalizedScript resource="Quotxt45" runat="server" />
  </HEAD>
  <body id="body" runat="server">
	<SCRIPT>var idcdoc=1;</SCRIPT>
	<script>
		function CheckShipConfiguration()
        {
           var s = document.getElementById("ShipDescription");
           var myindex  = s.selectedIndex;
	       var SelValue = s.options[myindex].value.split('|');
	       if(SelValue[1]=='0')
	       {
	            document.getElementById("ShipData").style.display="none";
	            document.getElementById("Calendargif").style.display="none";
	       }else
	       {
	            document.getElementById("ShipData").style.display="";
	            document.getElementById("Calendargif").style.display="";
	       }
        }

		function addDocument()
		{
			cloneObj('TableDocument',idcdoc++,'tblDocument');
			var tempidcdoc=idcdoc-1;
			cleardoc('_'+(tempidcdoc));
		}

		function removeDocument(cloneparam1)
		{
			removeCloned(cloneparam1,'tblDocument');
			idcdoc--;
			if(idcdoc<1)
				cleardoc('');
		}

		function openDataStorage(cloneparam1,cloneparam2)
		{
			CreateBox('/common/PopFile.aspx?render=no&textbox='+cloneparam1+'&textboxID='+cloneparam2+'&extfilter=pdf',event,600,500)
		}

		function OpenSearchBox(e){
				var x;

				if((document.getElementById("CrossWith_0")).checked)
					x=0;
				else if	((document.getElementById("CrossWith_1")).checked)
						x=1;
					 else if ((document.getElementById("CrossWith_2")).checked)
							x=2;
						  else
							x=3;


				switch(x){
					case 0:
						CreateBox('/Common/PopCompany.aspx?render=no&textbox=CrossWithText&textbox2=CrossWithID&event=CheckAddress()',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID&event=CheckAddress()',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox=CrossWithText&textboxID=CrossWithID&event=CheckAddress()',e,400,300);
						break;
				}
			}

			function CheckAddress()
			{
				if(!ObjExists('Ajax')){
				ele.onkeyup=null;
				return;}

				var type;
				if((document.getElementById("CrossWith_0")).checked)
					type="0";
					else if	((document.getElementById("CrossWith_1")).checked)
						type="1";
						else if ((document.getElementById("CrossWith_2")).checked)
		  					type="2";

				var id = document.getElementById("CrossWithID").value;

				var res = Ajax.Quote.CheckAddress(id,type);
				if(typeof(res) == 'object')
				{
					try{
					//if(typeof(res.value.Tables) == 'object')
					{
						//var address = res.value.Tables.Table.Rows[0].address+"\n"+res.value.Tables.Table.Rows[0].city+"\n"+res.value.Tables.Table.Rows[0].province+"\n"+res.value.Tables.Table.Rows[0].zip+" - "+res.value.Tables.Table.Rows[0].nation;
						var address = res.value;
						document.getElementById("QAddress").value=address;
						document.getElementById("SAddress").value=address;
						/*
						document.getElementById("QCity").value=res.value.Tables.Table.Rows[0].city;
						document.getElementById("Qprovince").value=res.value.Tables.Table.Rows[0].province;
						document.getElementById("QNation").value=res.value.Tables.Table.Rows[0].nation;
						document.getElementById("QCap").value=res.value.Tables.Table.Rows[0].zip;
						*/
					}
					}catch(e){}
				}
			}

			function CheckManager()
			{

				if(!ObjExists('Ajax')){
				ele.onkeyup=null;
				return;}

				var id = document.getElementById("TextboxSearchOwnerID").value;

				var res = Ajax.Quote.CheckManager(id);
				if(typeof(res) == 'object')
				{

					try{
					if(typeof(res.value.Tables) == 'object')
					{
						document.getElementById("TextboxSearchManagerID").value=res.value.Tables.Table.Rows[0].id;
						document.getElementById("TextboxSearchManager").value=res.value.Tables.Table.Rows[0].description;
					}
					}catch(e){
						document.getElementById("TextboxSearchManagerID").value='';
						document.getElementById("TextboxSearchManager").value='';
					}

					if(typeof( res.value.Tables.Table.Rows[0].listprice )!='undefined')
						{
						    var droplist = document.getElementById("PriceList");
			                var list = document.getElementById("currentPriceList");
			                for(var i=0;i<droplist.options.length;i++)
			                {
		            	        if(droplist.options[i].value==res.value.Tables.Table.Rows[0].listprice)
		            	        {
				                    droplist.selectedIndex=i;
				                    if(droplist.options[droplist.selectedIndex].value!=list.value)
				                        RemoveAllRows();
				                    list.value=droplist.options[droplist.selectedIndex].value;
				                    droplist.disabled=true;
				                    break;
				                }
				            }
						}
				}
			}

			function cleardoc(suffix)
			{
				document.getElementById("DocumentDescription"+suffix).value='';
				document.getElementById("IDDocument"+suffix).value='';

			}

			function ChangeList()
			{
			    var droplist = document.getElementById("PriceList");
			    var list = document.getElementById("currentPriceList");
			    if(confirm(Quotxt45))
			    {
			        list.value=droplist.options[droplist.selectedIndex].value;
			        RemoveAllRows();
			    }else{
			        for(var i=0;i<droplist.options.length;i++)
			        {
		            	if(droplist.options[i].value==list.value)
		            	{
				            droplist.selectedIndex=i;
				            break;
				        }
				    }
			    }
			}

	</script>
    <form id="Form1" method="post" runat="server">
    <domval:DomValidationSummary
					id="valSum"
					ShowMessageBox=true
					DisplayMode="BulletList"
					EnableClientScript="true"
					runat="server"/>

		<table cellpadding=0 cellspacing=0 class="normal" bgcolor="#ffffff" style="BORDER-RIGHT:#000 1px solid; BORDER-TOP:#000 1px solid;BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid" width="90%" align=center>
			<tr>
				<td colspan=3 class="GridTitle">
					<b><%=wrm.GetString("Ordtxt1")%></b>
				</td>
			</tr>
			<tr>
				<td align=right colspan=3 style="padding-top:5px;padding-right:5px">
						<asp:LinkButton ID="btnSave" Runat=server CssClass="Save"></asp:LinkButton>

						<asp:Literal ID="lblPrint" Runat=server Visible=true></asp:Literal>
				</td>
			</tr>
			<tr>
				<td width="50%" valign=top style="padding-left:5px;">

			<table cellpadding=0 cellspacing=0 class="normal" width="100%">
			<tr>
				<td width="30%" valign=top>
					<%=wrm.GetString("Ordtxt2")%>
					<domval:RequiredDomValidator ID="QSubjectValidator" Display=Static ControlToValidate="QSubject" Runat="server">*</domval:RequiredDomValidator>
				</td>
				<td>
					<asp:TextBox ID="QSubject" Runat="server" CssClass="BoxDesignReq" TextMode=MultiLine Height="40px" Rows=3></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<%=wrm.GetString("Ordtxt3")%>
				</td>
				<td>
					<asp:TextBox ID="Qnumber" Runat="server" CssClass="BoxDesign"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td>
					<%=wrm.GetString("Quotxt12")%>
				</td>
				<td>
					<asp:DropDownList ID="QPayment" Runat="server" CssClass="BoxDesign"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td>
					<%=wrm.GetString("Quotxt8")%>
					<domval:RequiredDomValidator ID="TextboxSearchOwnerIDValidator" Display=Static ControlToValidate="TextboxSearchOwnerID" ControlToHi="TextboxSearchOwner" Runat="server">*</domval:RequiredDomValidator>
				</td>
				<td>
					<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchOwnerID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchOwner" runat="server" Width="100%" CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="25" align=right>
									    <span id="SelectOwner" runat=server>
										    &nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=TextboxSearchOwner&textbox2=TextboxSearchOwnerID&event=CheckManager()',event)">
										</span>
									</td>
									</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<span style=" text-transform:capitalize"><%=wrm.GetString("Usrtxt83").ToLower()%></span>
				</td>
				<td>
					<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchManagerID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchManager" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="45" style="wrap:no" align=right>
									    <span id="SelectManager" runat=server>
    										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&sales=1&textbox=TextboxSearchManager&textbox2=TextboxSearchManagerID',event)">
	    									&nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip8")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxSearchManagerID');CleanField('TextboxSearchManager')">
	    							    </span>
									</td>
									</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td>
					<span style=" text-transform:capitalize"><%=wrm.GetString("Quotxt30").ToLower()%></span>
				</td>
				<td>
					<table width="100%" cellspacing=0 cellpadding=0>
									<tr><td>
										<asp:TextBox id="TextboxSearchSignalerID" runat="server" Width="100%" style="display:none"></asp:TextBox>
										<asp:TextBox id="TextboxSearchSignaler" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
									</td>
									<td width="45" style="wrap:no" align=right>
										&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=TextboxSearchSignaler&textboxID=TextboxSearchSignalerID',event,400,300)">
										&nbsp;<img src="/i/erase.gif" alt="<%=wrm.GetString("AcTooltip8")%>" border="0" style="CURSOR:pointer" onclick="CleanField('TextboxSearchSignalerID');CleanField('TextboxSearchSignaler')">
									</td>
									</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="30%">
					<%=wrm.GetString("Ordtxt4")%>
				</td>
				<td>
					<asp:DropDownList ID="QStage" Runat="server" CssClass="BoxDesign"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td width="30%">
					<%=wrm.GetString("Ordtxt5")%>
					<domval:RequiredDomValidator ID="QuoteDataValidator" Display=Static ControlToValidate="QuoteData" Runat="server">*</domval:RequiredDomValidator>
				</td>
				<td>
					<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td>
											<asp:TextBox id="QuoteData" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesignReq"></asp:TextBox>
										</td>
										<td width="25" align=right>
											&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=QuoteData&Start='+(document.getElementById('QuoteData')).value,event,195,195)">
										</td>
										</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td width="30%">
					<%=wrm.GetString("Ordtxt9")%>
				</td>
				<td>
					<table width="100%" cellspacing=0 cellpadding=0>
										<tr><td>
											<asp:TextBox id="QValidData" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign"></asp:TextBox>
										</td>
										<td width="25" align=right>
											&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=QValidData&Start='+(document.getElementById('QValidData')).value,event,195,195)">
										</td>
										</tr>
					</table>
				</td>
			</tr>
			<tr>
			    <td width="30%">
			        <%=wrm.GetString("Quotxt44")%>
			    </td>
			    <td>
			        <asp:DropDownList ID="PriceList" runat=server CssClass=BoxDesign old=true></asp:DropDownList>
			        <asp:TextBox ID="currentPriceList" runat=server style="display:none"></asp:TextBox>
			    </td>
			</tr>

			<tr>
				<td width="30%">
					<%=wrm.GetString("Quotxt36")%>
				</td>
				<td>
				    <table width="100%" cellspacing=0 cellpadding=0>
						<tr>
						    <td width="50%" style="padding-right:5px">
						        <asp:DropDownList ID="ShipDescription" runat=server CssClass="BoxDesign"></asp:DropDownList>
						    </td>
						    <td>
    							<asp:TextBox id="ShipData" runat="server" Width="100%" onkeypress="DataCheck(this,event)" CssClass="BoxDesign" style="display:none"></asp:TextBox>
	    					</td>
		    				<td width="25" align=right>
			    				&nbsp;<img src="/i/SmallCalendar.gif" id="Calendargif" border="0" style="CURSOR:pointer;display:none" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=ShipData&Start='+(document.getElementById('QuoteData')).value,event,195,195)">
				    		</td>
						</tr>
					</table>

				</td>
			</tr>
			<tr id="actRow" runat=server>
				<td width="30%">
					<%=wrm.GetString("Quotxt28")%>
				</td>
				<td>
					<asp:CheckBox ID="CheckActivity" Runat=server></asp:CheckBox>
				</td>
			</tr>

		</table>


		</td>
		<td width="50">&nbsp;</td>
		<td valign=top style="padding-right:5px;">
			<table cellpadding=0 cellspacing=0 class="normal" width="100%">
				<tr>
					<td colspan=2>
						<div>
							<table cellspacing="0" cellpadding="0" class="normal">
								<tr>
									<td><%=wrm.GetString("Tictxt38")%>
									<domval:RequiredDomValidator ID="CrossWithIDValidator" Display=Static ControlToValidate="CrossWithID" ControlToHi="CrossWithText" Runat="server">*</domval:RequiredDomValidator>
									</td>
									<td><asp:RadioButtonList id="CrossWith" runat="server" cssClass="normal"></asp:RadioButtonList></td>
									<td><img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="OpenSearchBox(event)"></td>
								</tr>
							</table>
						</div>
						<asp:TextBox ID="CrossWithID" Runat="server" style="DISPLAY:none"></asp:TextBox>
						<asp:TextBox ID="CrossWithText" Runat="server" CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox>
					</td>
				</tr>

				<tr>
					<td width="40%" valign=top>
						<%=wrm.GetString("Quotxt14")%>
					</td>
					<td>
						<asp:TextBox ID="QAddress" Runat="server" CssClass="BoxDesign" TextMode=MultiLine Rows=3 Height="60px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td width="40%" valign=top>
						<%=wrm.GetString("Quotxt31")%>
					</td>
					<td>
						<asp:TextBox ID="SAddress" Runat="server" CssClass="BoxDesign" TextMode=MultiLine Rows=3 Height="60px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td colspan=2 style="padding-top:10px" align=right>
						<asp:Literal id="QuoteInfo" runat=server/>
						<asp:LinkButton id="parentQuote" runat="server" class=normal/>
					</td>
				</tr>
			</table>
		</td>
	</tr>

</table>
<br>
<table cellpadding=0 cellspacing=0 class="normal" bgcolor="#ffffff" style="BORDER-RIGHT:#000 1px solid; BORDER-TOP:#000 1px solid;BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid" width="90%" align=center>
			<tr>
				<td colspan=3 class="GridTitle">
					<b><%=wrm.GetString("Ordtxt10")%></b>
				</td>
			</tr>
			<tr>
				<td>
					<table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
                        <tr>
                            <td width="10%" valign="top" nowrap>
                                <div><%=wrm.GetString("CRMcontxt28")%></div>
                                <asp:textbox id="InvoiceNumber" runat="server" class="BoxDesignReq" />
                            </td>
                            <td width="10%" valign="top" nowrap>
                                <div><%=wrm.GetString("CRMcontxt27")%></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:textbox id="InvoiceDate" runat="server" class="BoxDesignReq" maxlength="10" />
                                        </td>
                                        <td width="30" valign="top">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer;" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=InvoiceDate',event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%" valign="top" nowrap>
                                <div><%=wrm.GetString("CRMcontxt73")%></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:textbox id="InvoiceExpirationDate" runat="server" class="BoxDesign" maxlength="10" />
                                        </td>
                                        <td width="30" valign="top">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer;" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=InvoiceExpirationDate',event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%" valign="top" nowrap>
                                <div><%=wrm.GetString("CRMcontxt31")%></div>
                                <table width="100%" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td valign="top">
                                            <asp:textbox id="InvoicePaymentDate" runat="server" class="BoxDesign" maxlength="10" />
                                        </td>
                                        <td width="30" valign="top">
                                            &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer;" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=InvoicePaymentDate',event,195,195)">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10%" valign="top" nowrap>
                                <div><%=wrm.GetString("CRMcontxt32")%></div>
                                <asp:checkbox id="InvoicePaid" runat="server" />
                                <asp:Literal id="InvoiceId" runat="server" visible=false/>
                            </td>

                        </tr>
                    </table>
				</td>
			</tr>
</table>
<br>
	<table cellpadding=0 cellspacing=0 class="normal" bgcolor="#ffffff" style="BORDER-RIGHT:#000 1px solid;BORDER-TOP:#000 1px solid; BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid" width="90%" align=center>
		<tr>
				<td class="GridTitle">
					<b><%=wrm.GetString("Ordtxt8")%></b>
				</td>
		</tr>
		<tr>
		<td style="padding-left:5px;padding-right:5px;">
			<br>
			<uc1:RowEditing id="Rowediting1" runat="server"></uc1:RowEditing>
		</td>
	</tr>
</table>
<br>
	<table cellpadding=0 cellspacing=0 class="normal" bgcolor="#ffffff" style="BORDER-RIGHT:#000 1px solid;BORDER-TOP:#000 1px solid; BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid" width="90%" align=center>
		<tr>
				<td class="GridTitle">
					<b><%=wrm.GetString("Quotxt19").ToUpper()%></b>
				</td>
		</tr>
		<tr>
		<td style="padding-left:5px;padding-right:5px;">
			<asp:TextBox ID="QuoteDescription" Runat="server" CssClass="BoxDesign" TextMode=MultiLine Height=100></asp:TextBox>
		</td>
		</tr>
	</table>
	<br>
	<table cellpadding=0 cellspacing=0 class="normal" bgcolor="#ffffff" style="BORDER-RIGHT:#000 1px solid;BORDER-TOP:#000 1px solid; BORDER-LEFT:#000 1px solid; BORDER-BOTTOM:#000 1px solid" width="90%" align=center>
		<tr>
				<td class="GridTitle" colspan=2>
					<b><%=wrm.GetString("Quotxt32").ToUpper()%></b>
				</td>
		</tr>
		<tr>
			<td>
				<asp:CheckBox id="chkIncludePDFDoc" runat=server/>
			</td>
			<td style="padding-top:4px;padding-right:4px;" align="right">
				<SPAN class=Save style="CURSOR: pointer" onclick="addDocument();"><%=wrm.GetString("Quotxt33")%></SPAN>
			</td>
		</tr>
		<tr>
			<td id=tblDocument colspan=2>
					<table width="50%" cellspacing=0 cellpadding=4 id="TableDocument">
						<tr>
						<td>
							<img src=/i/erase.gif cloneparam1="TableDocument" onclick="removeDocument(this.getAttribute('cloneparam1'));" style="cursor:pointer;">
						</td>
							<td width="99%">
								<asp:TextBox id="DocumentDescription" runat="server" CssClass="BoxDesign" readonly/>
								<asp:TextBox id="IDDocument" runat="server" style="display:none;"></asp:TextBox>
							</td>
							<td width="1%" nowrap>
								&nbsp;
								<img src=/i/sheet.gif border=0 alt='<%=wrm.GetString("AcTooltip18")%>' style="cursor:pointer" cloneparam2="IDDocument" cloneparam1="DocumentDescription" onclick="openDataStorage(''+this.getAttribute('cloneparam1')+'',''+this.getAttribute('cloneparam2')+'')"/>
							</td>
						</tr>
					</table>
			</td>
		</tr>
		<tr>
				<td align=right colspan=2>
					<asp:LinkButton ID="btnSave2" Runat=server CssClass="Save"></asp:LinkButton>
					<asp:Literal ID="lblPrint2" Runat=server Visible=False></asp:Literal>
				</td>
		</tr>
	</table>

     </form>

  </body>
</HTML>
