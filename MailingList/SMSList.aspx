<%@ Page Language="c#" trace="false" codebehind="SMSList.aspx.cs" Inherits="Digita.Tustena.SMSList"  AutoEventWireup="false"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<html>
<head id="head" runat="server">
<script language="javascript" src="/js/dynabox.js"></script>
<script language="javascript" src="/js/hashtable.js"></script>
<script language="javascript" src="/js/makeoresteso.js"></script>
<script>

function message_onChange(obj)
{	var msg = obj.value;//document.submitform.message.value;
	var cleft;
	msg = msg.replace('\n', ' ');
	msg = msg.replace('\r', '');
	msg = msg.replace('', 'o\'');
	msg = msg.replace('', 'a\'');
	msg = msg.replace('', 'e\'');
	msg = msg.replace('', 'e\'');
	msg = msg.replace('', 'u\'');
	msg = msg.replace('', 'i\'');
	msg = msg.replace('', 'ae');
	msg = msg.replace('', 'oe');
	msg = msg.replace('', 'aa');
	msg = msg.replace('', 'Ae');
	msg = msg.replace('', 'Oe');
	msg = msg.replace('', 'Aa');
	cleft = 161-msg.length-obj.value.length;
	if (cleft < 0) {
		alert("Hai finito i caratteri.\n");
		obj.value=obj.value.substr(0,160);
	}else
		document.getElementById("counter").innerHTML = cleft;
		obj.value=msg;

}

function PopChange(obj){
	var element=document.getElementById(obj)
	if(element.fireEvent)
		element.fireEvent('onchange');
	else{
		var	evt = document.createEvent("Events");
		evt.initEvent("change", true, false);
		element.dispatchEvent(evt);
	}
}

function AddFixedParams(elemento,elementovalue,w){
	var a = getElement(elemento);
	var b = getElement(elementovalue);
	var outBox = getElement("MLFixedMails");
	var t = getElement("ListFixedParams");

	with(outBox)
	{
		options[options.length] = new Option(a.value,w+b.value);
		t.value+=w+b.value+"|";
	}
	a.value="";
	b.value="";
}

function RemoveFixedParams(){

	var outBox = getElement("MLFixedMails");
	var t = getElement("ListFixedParams");
	if (outBox.options.length>0)
	{
		for (var indx = 0; indx < outBox.options.length;indx++)
		{
			if (outBox.options[indx].selected == true)
			{
				outBox.options[indx] = null;
				indx = -1;
			}
		}
	}
	t.value="";
	if (outBox.options.length>0)
	{
		for (var indx = 0; indx < outBox.options.length;indx++)
		{
			t.value+=outBox.options[indx].value+"|";
		}
	}

}


function ReloadParams(a,c,l){
	var CompanyFields = new Array("Advanced_CompanyName","Advanced_Address","Advanced_City","Advanced_State","Advanced_Nation","Advanced_Zip","Advanced_Phone","Advanced_Fax","Advanced_Email","Advanced_Site","Advanced_Code","SAdvanced_CompanyType","SAdvanced_ContactType","RAdvanced_Billed","RAdvanced_Employees","SAdvanced_Estimate","SAdvanced_Category");
	var ContactFields = new Array("AdvancedContacts_Address","AdvancedContacts_City","AdvancedContacts_State","AdvancedContacts_Nation","AdvancedContacts_Zip","AdvancedContacts_Email","SAdvancedContacts_Category");
	var LeadFields = new Array("AdvancedLead_Address","AdvancedLead_City","AdvancedLead_State","AdvancedLead_Nation","AdvancedLead_Zip","AdvancedLead_Email","SAdvancedLead_Category");
	try{
	for(i=0;i<CompanyFields.length;i++){
		if(a[i].length>1)
		{
			for(x=1;x<a[i].length;x++){
				newor(CompanyFields[i],a[i][x]);
			}
		}
	}
	}catch(e){}
	try{
	for(i=0;i<ContactFields.length;i++){
		if(c[i].length>1)
		{
			for(x=1;x<c[i].length;x++){
				newor(ContactFields[i],c[i][x]);
			}
		}
	}
	}catch(e){}
	try{
	for(i=0;i<LeadFields.length;i++){
		if(l[i].length>1)
		{
			for(x=1;x<l[i].length;x++){
				newor(LeadFields[i],l[i][x]);
			}
		}
	}
	}catch(e){}
}



function selmails(w){
	var a = document.getElementById("ReSearchCompanies");
	var c = document.getElementById("AdvancedContacts");
	var l = document.getElementById("AdvancedLeads");
	var f = document.getElementById("ReSearchFixedMails");
	switch(w){
		case 0:
			a.style.display='';
			c.style.display='none';
			l.style.display='none';
			f.style.display='none';
			break;
		case 1:
			a.style.display='none';
			c.style.display='';
			l.style.display='none';
			f.style.display='none';
			break;
		case 2:
			a.style.display='none';
			c.style.display='none';
			l.style.display='';
			f.style.display='none';
			break;
		case 3:
			a.style.display='none';
			c.style.display='none';
			l.style.display='none';
			f.style.display='';
			break;
	}
}
</script>


</head>
<body id="body" runat="server">
<form runat="server">
<table width="100%" border="0" cellspacing="0">
<tr>
<td width="140" class="SideBorderLinked" valign="top">
<table width="98%" border="0" cellspacing="0" cellpadding=0 align="center">
	<tr >
		<td align="left" class="BorderBottomTitles" valign=top>
			<span class="divautoform"><b>Mailing List</b></span>
		</td>
	</tr>
	<tr >
		<td align="left" valign=top>
		<asp:LinkButton id="BtnNewML" runat="server" cssClass="normal" />
		</td>
	</tr>
</table>
</td>
<td valign="top" height="100%">
<table width="98%" border="0" cellspacing="0" cellpadding=0 align="center">
	<tr >
		<td align="left" class="BorderBottomTitles" valign=top>
			<span class="divautoform"><b>Mailing List</b></span>
		</td>
	</tr>
	<tr >
		<td align="left" valign=top style="font-size:5px;">
			<asp:Label id="MailToSend" runat="server" cssClass="normal"/>
			<asp:Label id="MailToSendID" runat="server" cssClass="normal" visible="false"/>
		</td>
	</tr>
</table>
  <asp:Repeater id="MailingListRep" runat="server">
  <HeaderTemplate>
  	<table cellpadding="2" cellspacing="1" width="98%" class="normal" align="center">
  	<tr>
  	<td width="50%" class="GridTitle">
  	Mailing List
  	</td>
  	<td width="20%" class="GridTitle" align="right">
  	<%=Root.rm.GetString("MLtxt5")%>
  	</td>
  	<td width="20%" class="GridTitle" align="right">
  	<%=Root.rm.GetString("MLtxt34")%>
  	</td>
  	<td width="10%" class="GridTitle">
  	&nbsp;
  	</td>
  	</tr>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
  	<td class="GridItem">
  		<asp:Literal id="MLTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>
  		<asp:Literal id="MLid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' visible="false"/>
  		<asp:Literal id="MLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' visible="false"/>
  	</td>
  	<td class="GridItem" align="right">
  		<span onclick="CreateBox('/MailingList/MailLog.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);" style="cursor:pointer"><%#DataBinder.Eval(Container.DataItem,"nsend")%></span>
  	</td>
  	<td class="GridItem" align="right">
  		<%#DataBinder.Eval(Container.DataItem,"nmess")%>
  	</td>
  	<td width="10%" class="GridItem" nowrap>
  		<asp:LinkButton id="SendMail" runat="server" CommandName="SendMail"/>
  		&nbsp;
  		<asp:LinkButton id="DeleteMail" runat="server" CommandName="DeleteMail"/>
  		&nbsp;
  		<asp:LinkButton id="ModifyMail" runat="server" CommandName="ModifyMail"/>
  		&nbsp;
  		<span class="normal" style="cursor:pointer;text-decoration:underline" onclick="CreateBox('/MailingList/VerifyMailInML.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);"><%=Root.rm.GetString("MLtxt42").ToUpper()%></span>
  	</td>
  	</tr>
  </ItemTemplate>
  <AlternatingItemTemplate>
    <tr>
  	<td class="GridItemAltern">
  		<asp:Literal id="MLTitle" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Description")%>'/>
  		<asp:Literal id="MLid" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"id")%>' visible="false"/>
  		<asp:Literal id="MLSubject" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Subject")%>' visible="false"/>
  	</td>
  	<td class="GridItemAltern" align="right">
  		<span onclick="CreateBox('/MailingList/MailLog.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);" style="cursor:pointer"><%#DataBinder.Eval(Container.DataItem,"nsend")%></span>
  	</td>
  	  	<td class="GridItemAltern" align="right">
  		<%#DataBinder.Eval(Container.DataItem,"nmess")%>
  	</td>
  	<td width="10%" class="GridItemAltern">
  		<asp:LinkButton id="SendMail" runat="server" CommandName="SendMail"/>
  		&nbsp;
  		<asp:LinkButton id="DeleteMail" runat="server" CommandName="DeleteMail"/>
  		&nbsp;
  		<asp:LinkButton id="ModifyMail" runat="server" CommandName="ModifyMail"/>
  		&nbsp;
  		<span class="normal" style="cursor:pointer;text-decoration:underline" onclick="CreateBox('/MailingList/VerifyMailInML.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);"><%=Root.rm.GetString("MLtxt42").ToUpper()%></span>
  	</td>
  	</tr>
  </AlternatingItemTemplate>
  <FooterTemplate>
  </table>
  </FooterTemplate>
  </asp:Repeater>
  <Pag:RepeaterPaging id="MailListPaging" visible=false runat=server/>

<table id="NewML" runat="server" border="0" cellpadding="0" cellspacing="5" width="98%" class="normal" align="center">
<tr>
<td width="60%" valign=top>
  <table id="NewMLTable" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
    <tr>
    <td>
    <div><%=Root.rm.GetString("MLtxt6")%>
	<domval:RequiredDomValidator id="rvNewMLTitle"
		runat="server" ControlToValidate="NewMLTitle" ErrorMessage="*"/>
    </div>
    <asp:TextBox id="NewMLTitle" runat="server" class="BoxDesign"/>
    <asp:Literal id="MLID" runat="server" visible="false"/>
    </td>
    </tr>

    <tr>
    <td align="right">
    <asp:LinkButton id="NewMLSubmit" visible=true runat="server" cssClass="Save" Text=" >> " />
    </td>
    </tr>
  </table>

<table id="RisearchAdvanced" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
  <tr>
    <td align="center">
    <b><%=Root.rm.GetString("MLtxt7")%>
		<input type="radio" name="Selmails" onclick="selmails(0)" checked><%=Root.rm.GetString("MLtxt27")%>
		<input type="radio" name="Selmails" onclick="selmails(1)"><%=Root.rm.GetString("MLtxt28")%>
		<input type="radio" name="Selmails" onclick="selmails(2)"><%=Root.rm.GetString("MLtxt29")%>
		<input type="radio" name="Selmails" onclick="selmails(3)"><%=Root.rm.GetString("MLtxt30")%>
    </b><br>
    </td>
  </tr>
  <tr>
  <td>
  <table id="ReSearchCompanies" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">

    <tr>
	  <td width="20%" valign="top" nowrap><%=Root.rm.GetString("Bcotxt17")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
	    <span id="Advanced_CompanyNameqc">
			<asp:TextBox runat=server id="Advanced_CompanyName" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_CompanyName')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_CompanyName')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Addressqc">
			<asp:TextBox runat=server id="Advanced_Address" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Cityqc">
			<asp:TextBox runat=server id="Advanced_City" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_City')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_City')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Stateqc">
			<asp:TextBox runat=server id="Advanced_State" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_State')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_State')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Nationqc">
			<asp:TextBox runat=server id="Advanced_Nation" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Nation')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Nation')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Zipqc">
			<asp:TextBox runat=server id="Advanced_Zip" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Zip')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Zip')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt20")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Phoneqc">
			<asp:TextBox runat=server id="Advanced_Phone" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Phone')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Phone')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt21")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Faxqc">
			<asp:TextBox runat=server id="Advanced_Fax" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Fax')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Fax')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Emailqc">
			<asp:TextBox runat=server id="Advanced_Email" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Email')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Email')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt23")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Siteqc">
			<asp:TextBox runat=server id="Advanced_Site" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Site')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Site')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("Bcotxt11")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Codeqc">
			<asp:TextBox runat=server id="Advanced_Code" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Code')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Code')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt8")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
       <span id="SAdvanced_CompanyTypeqc">
			<asp:DropDownList id="SAdvanced_CompanyType" old=true runat="server" class="BoxDesign"/>
	   </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvanced_CompanyType')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvanced_CompanyType')" style="cursor:pointer">
      </td>

    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt9")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
       <span id="SAdvanced_ContactTypeqc">
			<asp:DropDownList id="SAdvanced_ContactType" old=true runat="server" class="BoxDesign"/>
	   </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvanced_ContactType')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvanced_ContactType')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt10")%></td>
      <td width="20%" valign="top" align="center">
		&nbsp;
      </td>
      <td width="60%">
		<span id="RAdvanced_Billedqc">
			<span id="RemoveRAdvanced_Billed">
			<input type="radio" id="RAdvanced_Billed0" name="RAdvanced_Billed" value=0>=
			<input type="radio" id="RAdvanced_Billed1" name="RAdvanced_Billed" value=1>&lt;=
			<input type="radio" id="RAdvanced_Billed2" name="RAdvanced_Billed" value=2>&lt;
			<input type="radio" id="RAdvanced_Billed3" name="RAdvanced_Billed" value=3>&lt;&gt;
			<input type="radio" id="RAdvanced_Billed4" name="RAdvanced_Billed" value=4>&gt;
			<input type="radio" id="RAdvanced_Billed5" name="RAdvanced_Billed" value=5>&gt;=
			</span>
			<asp:TextBox id="Advanced_Billed" runat="server" class="BoxDesign"/>
		</span>
      </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('RAdvanced_Billed')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('RAdvanced_Billed')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt11")%></td>
      <td width="20%" valign="top" align="center">
		&nbsp;
      </td>
      <td width="60%">
      <span id="RAdvanced_Employeesqc">
			<span id="RemoveRAdvanced_Employees">
			<input type="radio" id="RAdvanced_Employees0" name="RAdvanced_Employees" value=0>=
			<input type="radio" id="RAdvanced_Employees1" name="RAdvanced_Employees" value=1>&lt;=
			<input type="radio" id="RAdvanced_Employees2" name="RAdvanced_Employees" value=2>&lt;
			<input type="radio" id="RAdvanced_Employees3" name="RAdvanced_Employees" value=3>&lt;&gt;
			<input type="radio" id="RAdvanced_Employees4" name="RAdvanced_Employees" value=4>&gt;
			<input type="radio" id="RAdvanced_Employees5" name="RAdvanced_Employees" value=5>&gt;=
			</span>
			<asp:TextBox id="Advanced_Employees" runat="server" class="BoxDesign"/>
		</span>
      </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('RAdvanced_Employees')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('RAdvanced_Employees')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt12")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
      <span id="SAdvanced_Estimateqc">
			<asp:DropDownList id="SAdvanced_Estimate" old=true runat="server" class="BoxDesign"/>
	   </span>
	  </td><td width="2%" valign=bottom>
		 <img src=/images/plus.gif onclick="newor('SAdvanced_Estimate')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvanced_Estimate')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
		<td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMopptxt1")%></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvanced_Opportunityqc">
				<asp:DropDownList id="SAdvanced_Opportunity" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td width="2%" valign="bottom">
			<img src="/images/plus.gif" onclick="newor('SAdvanced_Opportunity')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('SAdvanced_Opportunity')" style="CURSOR:pointer">
		</td>
	</tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt45")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
		<span id="SAdvanced_Categoryqc">
			<asp:DropDownList id="SAdvanced_Category" old=true runat="server" class="BoxDesign"/>
	    </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvanced_Category')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvanced_Category')" style="cursor:pointer">
      </td>
    </tr>

  </table>
  <table id="AdvancedContacts" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Addressqc">
			<asp:TextBox runat=server id="AdvancedContacts_Address" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Cityqc">
			<asp:TextBox runat=server id="AdvancedContacts_City" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_City')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_City')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Stateqc">
			<asp:TextBox runat=server id="AdvancedContacts_State" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_State')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_State')" style="cursor:pointer">
      </td>
    </tr>
	<tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Nationqc">
			<asp:TextBox runat=server id="AdvancedContacts_Nation" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Nation')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Nation')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Zipqc">
			<asp:TextBox runat=server id="AdvancedContacts_Zip" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Zip')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Zip')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Emailqc">
			<asp:TextBox runat=server id="AdvancedContacts_Email" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Email')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Email')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt45")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
		<span id="SAdvancedContacts_Categoryqc">
			<asp:DropDownList id="SAdvancedContacts_Category" old=true runat="server" class="BoxDesign"/>
	    </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvancedContacts_Category')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvancedContacts_Category')" style="cursor:pointer">
      </td>
    </tr>
  </table>
  <table id="AdvancedLeads" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Addressqc">
			<asp:TextBox runat=server id="AdvancedLead_Address" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Cityqc">
			<asp:TextBox runat=server id="AdvancedLead_City" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_City')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_City')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Stateqc">
			<asp:TextBox runat=server id="AdvancedLead_State" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_State')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_State')" style="cursor:pointer">
      </td>
    </tr>
	<tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Nationqc">
			<asp:TextBox runat=server id="AdvancedLead_Nation" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_Nation')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_Nation')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Zipqc">
			<asp:TextBox runat=server id="AdvancedLead_Zip" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_Zip')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_Zip')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=Root.rm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=Root.rm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Emailqc">
			<asp:TextBox runat=server id="AdvancedLead_Email" value="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_Email')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_Email')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
		<td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMopptxt1")%></td>
		<td width="20%" valign="top" align="center">&nbsp;</td>
		<td width="60%">
			<span id="SAdvancedLead_Opportunityqc">
				<asp:DropDownList id="SAdvancedLead_Opportunity" old="true" runat="server" class="BoxDesign" />
			</span>
		</td>
		<td width="2%" valign="bottom">
			<img src="/images/plus.gif" onclick="newor('SAdvancedLead_Opportunity')" style="CURSOR:pointer">
			<img src="/images/minus.gif" onclick="delor('SAdvancedLead_Opportunity')" style="CURSOR:pointer">
		</td>
	</tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=Root.rm.GetString("CRMcontxt45")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
		<span id="SAdvancedLead_Categoryqc">
			<asp:DropDownList id="SAdvancedLead_Category" old=true runat="server" class="BoxDesign"/>
	    </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvancedLead_Category')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvancedLead_Category')" style="cursor:pointer">
      </td>
    </tr>
  </table>
   <table id="ReSearchFixedMails" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	<tr>
		<td width="40%" valign=top>
			<div><%=Root.rm.GetString("Acttxt63")%></div>
			<table width="100%" cellspacing=0 cellpadding=0>
				<tr>
					<td>
						<asp:TextBox id="TextboxSearchCompanyID" runat="server" style="display:none;"></asp:TextBox>
						<asp:TextBox id="TextboxSearchCompany" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
					</td>
					<td width="30">
						&nbsp;<img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="CreateBox('/Common/PopCompany.aspx?render=no&textbox=TextboxSearchCompany&textbox2=TextboxSearchCompanyID&change=1',event,500,400)">
					</td>
				</tr>
			</table>
			<div><%=Root.rm.GetString("Acttxt64")%></div>
			<table width="100%" cellspacing=0 cellpadding=0>
				<tr>
					<td>
						<asp:TextBox id="TextboxSearchContactID" runat="server" Width="100%" style="display:none"></asp:TextBox>
						<asp:TextBox id="TextboxSearchContact" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
					</td>
					<td width="30">
						&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/popcontacts.aspx?render=no&textbox=TextboxSearchContact&textboxID=TextboxSearchContactID&companyID=' + getElement('TextboxSearchCompanyID').value+'&change=1',event,400,300)">
					</td>
				</tr>
			</table>
			<div><%=Root.rm.GetString("Acttxt89")%></div>
			<table width="100%" cellspacing=0 cellpadding=0>
				<tr>
					<td>
						<asp:TextBox id="TextboxSearchLeadID" runat="server" Width="100%" style="display:none"></asp:TextBox>
						<asp:TextBox id="TextboxSearchLead" runat="server" Width="100%"  CssClass="BoxDesign" ReadOnly="true"></asp:TextBox>
					</td>
					<td width="30">
						&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopLead.aspx?render=no&textbox=TextboxSearchLead&textboxID=TextboxSearchLeadID&companyID=' + getElement('TextboxSearchCompanyID').value+'&change=1',event,400,300)">
					</td>
				</tr>
			</table>
		</td>
		<td width="50%">
			<asp:ListBox id="MLFixedMails" runat="server" cssclass="listboxautoform" style="height:150px" SelectionMode="Multiple" />
			<input type=hidden runat=server id="ListFixedParams" name="ListFixedParams">
		</td>
		<td width="10%" valign="top">
			<span class="Save" style="cursor:pointer" onclick="RemoveFixedParams()"><%=Root.rm.GetString("Delete")%></span>
		</td>
	</tr>
   </table>
  </td>
  </tr>
  <tr>
    <td align="right" nowrap>
    <asp:LinkButton id="SearchAdvanced" visible=false runat="server" />
    &nbsp;&nbsp;
    <asp:LinkButton id="SaveML" runat="server"  cssClass="normal"/>
    </td>
  </tr>
  </table>
</td>
<td width="40%" valign=top>
  <table style="display:none" cellpadding="0" cellspacing="0" width="100%" class="normal">
  <tr>
  	<td class="GridTitle" colspan="3"><%=Root.rm.GetString("MLtxt10")%></td>
  </tr>
  <tr>
  <td>
		<asp:ListBox id="SearchResult" runat="server" cssclass="listboxautoform" style="height:300px;display:none" SelectionMode="Multiple" />
  </td>
  </tr>
<span id="spaninvio" runat="server">
  <tr>
  <td align="center">
  <asp:LinkButton id="MoveOne" runat="server" Text="<img src=/i/CopyDown.gif border=0>" />&nbsp;
  <asp:LinkButton id="MoveAll" runat="server" Text="<img src=/i/CopyDownAll.gif border=0>" />
  </td>
  </tr>

 </span>
  <tr>
  <td>
  <asp:Literal id="QueryToSave" runat="server" visible="false"/>
  </td>
  </tr>
  </table>

</td>
</tr>
</table>
<table id="PreviewList" runat=server width="100%">
<tr>
  <td width="50%">
  <table width="100%">
	<tr>
  		<td class="GridTitle" colspan="2"><%=Root.rm.GetString("MLtxt23")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt39")%></div>
					<asp:ListBox id="MLFill" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFillRemoved" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill" runat="server" cssclass="Save"/>
	</td>
	</tr>
	<tr>
  		<td class="GridTitle" colspan="2"><%=Root.rm.GetString("MLtxt24")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt39")%></div>
					<asp:ListBox id="MLFill2" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill2Removed" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill2" runat="server" cssclass="Save"/>
	</td>
	</tr>
  </table>
  </td>
  <td width="50%">
  <table width="100%">
	<tr>
  		<td class="GridTitle" colspan="2"><%=Root.rm.GetString("MLtxt25")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt39")%></div>
					<asp:ListBox id="MLFill3" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill3Removed" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill3" runat="server" cssclass="Save"/>
	</td>
	</tr>
	<tr>
  		<td class="GridTitle" colspan="2"><%=Root.rm.GetString("MLtxt26")%></td>
	</tr>
	<tr>
	<td>
			<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt39")%></div>
					<asp:ListBox id="MLFill4" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=Root.rm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill4Removed" runat="server" cssclass="listboxautoform" style="height:100px" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill4" runat="server" cssclass="Save"/>
	</td>
	</tr>
  </table>
  </td>
  </tr>
  <tr>
  <td>
	<div class=normal><%=Root.rm.GetString("MLtxt11")%></div>
	<asp:TextBox id="SenderTextBox" runat="server" CssClass="BoxDesign"/>
			<table>
				<tr>
					<td><table width="100%"><tr><td class="normal">Message text:</td><td align="right" class="normal">You have <span id=counter>160</span> characters left.</td></tr></table></td>
				</tr>
				<tr>
					<td><asp:textbox id="TxtMessage" runat="server" TextMode="MultiLine" Columns=40 Rows=5></asp:textbox></td>
				</tr>
				<tr>
					<td><asp:label  class="normal" id="MsgLabel" runat="server" ForeColor="#ff0000"></asp:label></td>
				</tr>
			</table>
  </td>
  </tr>
  <tr>
  <td align="right">
  <asp:LinkButton id="Verifymail" runat=server cssClass="Save"/>
  <span class="Save" style="cursor:pointer;" id="MailPreview" runat="server"><%=Root.rm.GetString("Preview")%></span>
  <asp:LinkButton id="SendML" runat="server"  cssClass="Save"/>
  </td>
  </tr>
</table>
<table width="90%" align="center">
<tr>
  <td>
	<asp:Label id="SendError" runat=server enabledviewstate="false"/>
	<asp:LinkButton id="BackToSendMail" runat=server Text="Torna a liste" cssClass="Save"/>
  </td>
</tr>
</table>
</td>
</tr>
</table>
</form>

</body>
</html>
