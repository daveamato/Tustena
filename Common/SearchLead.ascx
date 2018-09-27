<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SearchLead.ascx.cs" Inherits="Digita.Tustena.Common.SearchLead" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<script type="text/javascript" src="/js/autodate.js"></script>
<script language="javascript" src="/js/hashtable.js"></script>
<script language="javascript" src="/js/search.js"></script>
<table id="AdvancedLeads" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%"
	class="normal" align="center">
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Reftxt15" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Nameqc">
				<asp:TextBox runat="server" id="AdvancedLead_Name" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Name')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Name')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Reftxt16" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Surnameqc">
				<asp:TextBox runat="server" id="AdvancedLead_Surname" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Surname')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Surname')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Reftxt17" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_CompanyNameqc">
				<asp:TextBox runat="server" id="AdvancedLead_CompanyName" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_CompanyName')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_CompanyName')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Reftxt18" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_BusinessRoleqc">
				<asp:TextBox runat="server" id="AdvancedLead_BusinessRole" value="" class="BoxDesign"
					width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_BusinessRole')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_BusinessRole')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt26" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Addressqc">
				<asp:TextBox runat="server" id="AdvancedLead_Address" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Address')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Address')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt27" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Cityqc">
				<asp:TextBox runat="server" id="AdvancedLead_City" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_City')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_City')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt28" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Stateqc">
				<asp:TextBox runat="server" id="AdvancedLead_State" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_State')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_State')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt53" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Nationqc">
				<asp:TextBox runat="server" id="AdvancedLead_Nation" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Nation')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Nation')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt29" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Zipqc">
				<asp:TextBox runat="server" id="AdvancedLead_Zip" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Zip')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Zip')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt22" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Emailqc">
				<asp:TextBox runat="server" id="AdvancedLead_Email" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Email')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Email')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Ledtxt5" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Phoneqc">
				<asp:TextBox runat="server" id="AdvancedLead_Phone" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Phone')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Phone')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Ledtxt6" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_MobilePhoneqc">
				<asp:TextBox runat="server" id="AdvancedLead_MobilePhone" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_MobilePhone')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_MobilePhone')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Reftxt46" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_Faxqc">
				<asp:TextBox runat="server" id="AdvancedLead_Fax" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_Fax')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_Fax')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top"><twc:LocalizedLiteral text="Bcotxt23" runat="server"/></td>
		<td width="20%" valign="top" align="center"><twc:LocalizedLiteral text="CRMcontxt56" runat="server"/></td>
		<td width="60%">
			<span id="AdvancedLead_WebSiteqc">
				<asp:TextBox runat="server" id="AdvancedLead_WebSite" value="" class="BoxDesign" width="90%" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','AdvancedLead_WebSite')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','AdvancedLead_WebSite')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr runat=server id="tropportunity">
		<td width="20%" valign="top" nowrap><twc:LocalizedLiteral text="CRMopptxt1" runat="server"/></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvancedLead_Opportunityqc">
				<asp:DropDownList id="SAdvancedLead_Opportunity" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td width="2%" valign="bottom">
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvancedLead_Opportunity')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvancedLead_Opportunity')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top" nowrap><twc:LocalizedLiteral text="CRMcontxt45" runat="server"/></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvancedLead_Categoryqc">
				<asp:DropDownList id="SAdvancedLead_Category" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvancedLead_Category')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvancedLead_Category')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr runat=server id="trstatus">
		<td width="20%" valign="top" nowrap><twc:LocalizedLiteral text="Ledtxt11" runat="server"/></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvancedLead_Statusqc">
				<asp:DropDownList id="SAdvancedLead_Status" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvancedLead_Status')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvancedLead_Status')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top" nowrap><twc:LocalizedLiteral text="Ledtxt18" runat="server"/></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvancedLead_Industryqc">
				<asp:DropDownList id="SAdvancedLead_Industry" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td valign="bottom" nowrap>
			<img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvancedLead_Industry')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvancedLead_Industry')" style="CURSOR:pointer">
		</td>
	</tr>
	<tr>
		<td width="20%" valign="top" nowrap><twc:LocalizedLiteral text="CRMcontxt64" runat="server"/></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<asp:TextBox id="AdvancedLead_Owner" readonly="true" runat="server" class="BoxDesign" />
			<asp:TextBox id="AdvancedLead_OwnerID" style="DISPLAY:none" runat="server" class="BoxDesign" />
		</td>
		<td valign="bottom" nowrap>
			&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=<%=this.ID%>_AdvancedLead_Owner&textbox2=<%=this.ID%>_AdvancedLead_OwnerID',event)">
			&nbsp;<img src="/i/erase.gif" border="0" style="CURSOR:pointer" onclick="CleanField('<%=this.ID%>_AdvancedLead_OwnerID');CleanField('<%=this.ID%>_AdvancedLead_Owner')">
		</td>
	</tr>
</table>
