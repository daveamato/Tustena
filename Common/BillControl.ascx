<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="BillControl.ascx.cs" Inherits="Digita.Tustena.Common.BillControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
	<tr>
		<td style="BORDER-BOTTOM: #000000 1px solid" align="left" valign="bottom">
			<asp:Label id="PurchaseDescription" runat="server" class="normal" />&nbsp;
		</td>
		<td style="BORDER-BOTTOM: #000000 1px solid" align="right" valign="bottom" nowrap>
			<twc:GoBackBtn id="GoBackPurc" runat="server" visible="false" />
			&nbsp;&nbsp;
			<asp:LinkButton id="NewPurchase" runat="server" class="save" onClick="btn_Click" />
		</td>
	</tr>
	<tr>
		<td colspan="2" align="center">
			<asp:Label id="RepeaterPurchaseInfo" runat="server" class="divautoformRequired" />
		</td>
	</tr>
</table>
<asp:Repeater id="RepeaterPurchase" runat="server" OnItemCommand="RepeaterPurchaseCommand">
	<HeaderTemplate>
		<table border="0" cellpadding="3" cellspacing="0" width="98%" class="normal" align="center">
			<tr>
				<td class="GridTitle" width="1%">&nbsp;</td>
				<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt36" runat="server"/></td>
				<td class="GridTitle" width="15%"><twc:LocalizedLiteral text="CRMcontxt28" runat="server"/></td>
				<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt37" runat="server"/></td>
				<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt31" runat="server"/></td>
				<td class="GridTitle" width="4%"><twc:LocalizedLiteral text="CRMcontxt32" runat="server"/></td>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr>
			<td class="GridItem">
				<asp:ImageButton id="OpenPurchase" CommandName="OpenPurchase" runat="server" ImageURL="/i/modify2.gif" />&nbsp;
				<asp:Literal id="PurchaseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' visible="false"/>
			</td>
			<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "BillingDate", "{0:d}")%>&nbsp;</td>
			<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "BillNumber")%>&nbsp;</td>
			<td class="GridItem" align="right"><%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:###,###.00}")%>&nbsp;</td>
			<td class="GridItem"><%# DataBinder.Eval(Container.DataItem, "PaymentDate", "{0:d}")%>&nbsp;</td>
			<td class="GridItem" align="center"><img src='/i/<%# ((bool)DataBinder.Eval(Container.DataItem, "Payment"))?"checkon.gif":"checkoff.gif" %>'></td>
		</tr>
	</ItemTemplate>
	<AlternatingItemTemplate>
		<tr>
			<td class="GridItemAltern">
				<asp:ImageButton id="OpenPurchase" CommandName="OpenPurchase" runat="server" ImageURL="/i/modify2.gif" />&nbsp;
				<asp:Literal id="PurchaseID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ID")%>' visible="false"/>
			</td>
			<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "BillingDate", "{0:d}" runat="server)%>&nbsp;</td>
			<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "BillNumber")%>&nbsp;</td>
			<td class="GridItemAltern" align="right"><%# DataBinder.Eval(Container.DataItem, "TotalPrice", "{0:###,###.00}")%>&nbsp;</td>
			<td class="GridItemAltern"><%# DataBinder.Eval(Container.DataItem, "PaymentDate", "{0:d}")%>&nbsp;</td>
			<td class="GridItemAltern" align="center"><img src='/i/<%# ((bool)DataBinder.Eval(Container.DataItem, "Payment"))?"checkon.gif":"checkoff.gif" %>'></td>
		</tr>
	</AlternatingItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:Repeater>
<table id="CardPurchase" border="0" cellpadding="0" cellspacing="0" width="90%" class="normal"
	align="center" runat="server">
	<TBODY>
		<tr>
			<td width="100%">
				<table border="0" cellpadding="3" cellspacing="0" width="100%" class="normal" align="center">
					<tr>
						<td width="10%" valign="top" nowrap>
							<DIV></DIV>
							<twc:LocalizedLiteral text="CRMcontxt28" runat="server"/>
							<domval:RequiredDomValidator id="rvBillNumber" EnableClientScript="False" runat="server" ControlToValidate="Purchase_BillNumber"
								ErrorMessage="*" />
							<DIV></DIV>
							<asp:TextBox id="Purchase_BillNumber" runat="server" class="BoxDesign" />
						</td>
						<td width="10%" valign="top" nowrap>
							<DIV></DIV>
							<twc:LocalizedLiteral text="CRMcontxt27" runat="server"/>
							<domval:RequiredDomValidator id="rvBillingDate" EnableClientScript="False" runat="server" ControlToValidate="Purchase_BillingDate"
								ErrorMessage="*" />
							<domval:CompareDomValidator id="cvBillingDate" EnableClientScript="False" runat="Server" Operator="DataTypeCheck"
								Type="Date" ErrorMessage="*" ControlToValidate="Purchase_BillingDate" />
							<DIV></DIV>
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td valign="top">
										<asp:TextBox id="Purchase_BillingDate" runat="server" class="BoxDesign" maxlength="10" />
									</td>
									<td width="30" valign="top">
										&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_Purchase_BillingDate',event,195,195)">
									</td>
								</tr>
							</table>
						</td>
						<td width="10%" valign="top" nowrap>
							<DIV></DIV>
							<twc:LocalizedLiteral text="CRMcontxt73" runat="server"/>
							<domval:RequiredDomValidator id="rvExpirationDate" EnableClientScript="False" runat="server" ControlToValidate="Purchase_ExpirationDate"
								ErrorMessage="*" />
							<DIV></DIV>
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td valign="top">
										<asp:TextBox id="Purchase_ExpirationDate" runat="server" class="BoxDesign" maxlength="10" />
									</td>
									<td width="30" valign="top">
										&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_Purchase_ExpirationDate',event,195,195)">
									</td>
								</tr>
							</table>
						</td>
						<td width="10%" valign="top" nowrap>
							<div>
								<twc:LocalizedLiteral text="CRMcontxt31" runat="server"/>
							</div>
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td valign="top">
										<asp:TextBox id="Purchase_PaymentDate" runat="server" class="BoxDesign" maxlength="10" />
									</td>
									<td width="30" valign="top">
										&nbsp;<img src="/i/SmallCalendar.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_Purchase_PaymentDate',event,195,195)">
									</td>
								</tr>
							</table>
						</td>
						<td width="10%" valign="top" nowrap>
							<div><twc:LocalizedLiteral text="CRMcontxt32" runat="server"/></div>
							<asp:checkbox id="Purchase_Payment" runat="server" />
						</td>
						<td width="20%" valign="top" nowrap>
							<div><twc:LocalizedLiteral text="CRMcontxt85" runat="server"/></div>
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td>
										<asp:TextBox id="Purchase_TextboxOwnerID" runat="server" Width="100%" style="DISPLAY:none"></asp:TextBox>
										<asp:TextBox id="Purchase_TextboxOwner" runat="server" Width="100%" CssClass="BoxDesign"
											readonly="true"></asp:TextBox>
									</td>
									<td width="30">
										&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=<%=this.ID%>_Purchase_TextboxOwner&textbox2=<%=this.ID%>_Purchase_TextboxOwnerID',event)">
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
				<table border="0" cellpadding="1" cellspacing="0" width="100%" class="normal" align="center"
					style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
					<tr>
						<td width="100%" class="GridTitle" colspan="6"><twc:LocalizedLiteral text="CRMcontxt86" runat="server"/></td>
					</tr>
					<tr>
						<td width="30%">
							<div><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/>
								<span id="rvProductID" class="normal" style="DISPLAY:none;COLOR:red">*</span>
							</div>
							<table width="100%" cellspacing="0" cellpadding="0">
								<tr>
									<td valign="top">
										<asp:TextBox id="Purchase_ProductID" runat="server" class="BoxDesign" style="DISPLAY:none" />
										<asp:TextBox id="Purchase_Product" runat="server" class="BoxDesign" ReadOnly="true" />
									</td>
									<td width="30" valign="top">
										&nbsp;<img src="/i/lookup.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCatalog.aspx?render=no&ptx=<%=this.ID%>_Purchase_Product&pid=<%=this.ID%>_Purchase_ProductID&um=<%=this.ID%>_Purchase_Um&qta=<%=this.ID%>_Purchase_Qta&up=<%=this.ID%>_Purchase_Up&vat=<%=this.ID%>_Purchase_Vat&pl=<%=this.ID%>_Purchase_Pl&pf=<%=this.ID%>_Purchase_Pf',event,400,300)">
									</td>
								</tr>
							</table>
						</td>
						<td width="10%">
							<div><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></div>
							<asp:TextBox id="Purchase_Um" runat="server" ReadOnly="true" class="BoxDesign" />
						</td>
						<td width="10%">
							<div><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/>
								<domval:CompareDomValidator id="valPurchase_Qta" runat="server" ControlToValidate="Purchase_Qta" ValueToCompare="0"
									Type="Integer" Operator="GreaterThanEqual" ErrorMessage="*" Display="dynamic"></domval:CompareDomValidator>*
							</div>
							<asp:TextBox id="Purchase_Qta" runat="server" class="BoxDesign" />
						</td>
						<td width="20%">
							<div><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></div>
							<asp:TextBox id="Purchase_Up" runat="server" ReadOnly="true" class="BoxDesign" />
						</td>
						<td width="10%">
							<div><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/>&nbsp;(%)
								<domval:RangeDomValidator id="valPurchase_Vat" ControlToValidate="Purchase_Vat" MinimumValue="0" MaximumValue="100"
									Type="Integer" ErrorMessage="*" Display="dynamic" runat="server"></domval:RangeDomValidator>*
							</div>
							<asp:TextBox id="Purchase_Vat" runat="server" class="BoxDesign" />
						</td>
						<td width="20%">
							<div><twc:LocalizedLiteral text="CRMcontxt70" runat="server"/></div>
							<asp:TextBox id="Purchase_Pl" runat="server" ReadOnly="true" class="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td colspan="5">
							<div><twc:LocalizedLiteral text="CRMcontxt72" runat="server"/></div>
							<asp:TextBox id="Purchase_Description2" runat="server" class="BoxDesign" />
						</td>
						<td>
							<div><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/>
								<domval:CompareDomValidator id="valPurchase_Pf" runat="server" ControlToValidate="Purchase_Pf" ValueToCompare="0"
									Type="Double" Operator="GreaterThanEqual" ErrorMessage="*" Display="dynamic"></domval:CompareDomValidator>*
							</div>
							<asp:TextBox id="Purchase_Pf" runat="server" class="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<asp:LinkButton id="BtnCalcPrice" runat="server" cssclass="normal" onclick="btnCalcPrice_Click" />
						</td>
						<td colspan="3" align="right">
							<asp:LinkButton id="BtnAddProduct" runat="server" cssclass="save" onclick="btnAddProduct_Click" />
						</td>
					</tr>
				</table>
				<br>
				<table border="0" cellspacing="0" width="100%" align="center" class="normal" style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
					<tr>
						<td width="100%" class="GridTitle" colspan="6"><twc:LocalizedLiteral text="CRMcontxt87" runat="server"/></td>
					</tr>
					<tr>
						<td width="30%" colspan="6">
							<div><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></div>
							<asp:TextBox id="EstFreeProduct" runat="server" class="BoxDesign" />
						</td>
					</tr>
					<tr>
						<td width="5%">
							<div><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></div>
							<asp:TextBox id="EstFreeUm" runat="server" class="BoxDesign" />
						</td>
						<td width="5%">
							<div><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></div>
							<asp:TextBox id="EstFreeQta" runat="server" value="1" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
						</td>
						<td width="20%">
							<div><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></div>
							<asp:TextBox id="EstFreeUp" runat="server" class="BoxDesign" />
						</td>
						<td width="10%">
							<div><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/>&nbsp;%</div>
							<asp:TextBox id="EstFreeVat" runat="server" class="BoxDesign" maxlength="2" onkeypress="NumbersOnly(event,'.,',this)" />
						</td>
						<td width="20%">
							<div><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></div>
							<asp:TextBox id="EstFreePf" runat="server" class="BoxDesign" onkeypress="NumbersOnly(event,'.,',this)" />
						</td>
					</tr>
					<tr>
						<td colspan="6" align="right">
							<asp:LinkButton id="BtnAddFreeProductOpp" runat="server" cssclass="Save" />
						</td>
					</tr>
				</table>
				<br>
			</td>
		</tr>
		<tr>
			<td>
				<asp:Repeater id="RepeaterPurchaseProduct" runat="server" OnItemDataBound="PurchaseProductDatabound"
					OnItemCommand="PurchaseProductCommand">
					<HeaderTemplate>
						<table border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center"
							style="BORDER-RIGHT:#000000 1px solid; BORDER-TOP:#000000 1px solid; BORDER-LEFT:#000000 1px solid; BORDER-BOTTOM:#000000 1px solid">
							<tr>
								<td class="GridTitle" width="30%"><twc:LocalizedLiteral text="CRMcontxt65" runat="server"/></td>
								<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt66" runat="server"/></td>
								<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt67" runat="server"/></td>
								<td class="GridTitle" width="20%"><twc:LocalizedLiteral text="CRMcontxt68" runat="server"/></td>
								<td class="GridTitle" width="10%"><twc:LocalizedLiteral text="CRMcontxt69" runat="server"/></td>
								<td class="GridTitle" width="19%"><twc:LocalizedLiteral text="CRMcontxt71" runat="server"/></td>
								<td class="GridTitle" width="1%">&nbsp;</td>
							</tr>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td class="GridItem" width="30%"><asp:Label id="ShortDescription" runat="server" /></td>
							<td class="GridItem" width="10%"><asp:Label id="UM" runat="server" /></td>
							<td class="GridItem" width="10%"><asp:Label id="Qta" runat="server" /></td>
							<td class="GridItem" width="20%"><asp:Label id="UnitPrice" runat="server" /></td>
							<td class="GridItem" width="10%"><asp:Label id="Vat" runat="server" /></td>
							<td class="GridItem" width="19%"><asp:Label id="FinalPrice" runat="server" /></td>
							<td class="GridItem" width="1%" nowrap>
								<asp:LinkButton id="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
								<asp:LinkButton id="ModPurPro" CommandName="ModPurPro" runat="server" Text="<img src=/i/modify2.gif border=0>" />
								<asp:Literal id="ObjectID" runat="server" visible="false" />
								&nbsp;
							</td>
						</tr>
					</ItemTemplate>
					<AlternatingItemTemplate>
						<tr>
							<td class="GridItemAltern" width="30%"><asp:Label id="ShortDescription" runat="server" /></td>
							<td class="GridItemAltern" width="10%"><asp:Label id="UM" runat="server" /></td>
							<td class="GridItemAltern" width="10%"><asp:Label id="Qta" runat="server" /></td>
							<td class="GridItemAltern" width="20%"><asp:Label id="UnitPrice" runat="server" /></td>
							<td class="GridItemAltern" width="10%"><asp:Label id="Vat" runat="server" /></td>
							<td class="GridItemAltern" width="19%"><asp:Label id="FinalPrice" runat="server" /></td>
							<td class="GridItemAltern" width="1%" nowrap>
								<asp:LinkButton id="DelPurPro" CommandName="DelPurPro" runat="server" Text="<img src=/i/erase.gif border=0>" />
								<asp:LinkButton id="ModPurPro" CommandName="ModPurPro" runat="server" Text="<img src=/i/modify2.gif border=0>" />
								<asp:Literal id="ObjectID" runat="server" visible="false" />
								&nbsp;
							</td>
						</tr>
					</AlternatingItemTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:Repeater>
		</TD></TR>
<tr>
	<td valign="top" align="left" width="100%">
		<div><twc:LocalizedLiteral text="CRMcontxt42" runat="server"/></div>
		<asp:TextBox id="Purchase_Note" runat="server" TextMode="MultiLine" height="50" class="BoxDesign" />
	</td>
</tr>
<tr>
	<td valign="bottom" nowrap align="right">
		<asp:LinkButton id="Purchase_Exit" runat="server" class="save" onClick="btn_Click" />
		&nbsp;&nbsp;
		<asp:LinkButton id="Purchase_Submit" runat="server" class="save" onClick="btn_Click" />
		<asp:TextBox id="Purchase_ID" runat="server" visible="false" />
	</td>
</tr>
</TBODY></TABLE>
