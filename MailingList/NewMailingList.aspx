<%@ Page Language="c#" trace="false" codebehind="NewMailingList.aspx.cs" Inherits="Digita.Tustena.NewMailingList"  AutoEventWireup="false"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Register TagPrefix="Pag" TagName="RepeaterPaging" Src="~/Common/RepeaterPaging.ascx" %>
<html>
<head id="head" runat="server">
<script language="javascript" src="/js/dynabox.js"></script>
<script language="javascript" src="/js/hashtable.js"></script>
<script language="javascript" src="/js/makeoresteso.js"></script>
<script language="Javascript" src="/js/autodate.js"></script>
<script>
 function CleanField(f){
	var fi=getElement(f);
	fi.value="";
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

function AddFixedParams(element,elementvalue,w){
	var a = getElement(element);
	var b = getElement(elementvalue);
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
                <table width="98%" border="0" cellspacing="0" cellpadding="0" runat="server" ID="Table1">
                    <tr>
 		<td class="sideContainer">
			<div class="sideTitle"><%=wrm.GetString("Options")%></div>
            <asp:linkbutton id="BtnNewML" runat="server" cssclass="sidebtn" />
			<a href="/MailingList/editor.aspx?m=46&dgb=1&si=51" Class="sidebtn"><%=wrm.GetString("MLtxt53")%></a>

            </td>
                   </tr>
                </table>
            </td>
            <td valign="top" height="100%" class="pageStyle">
<table width="98%" border="0" cellspacing="0" cellpadding=0 align="center">
	<tr>
		<td align="left" class="pageTitle" valign="top">
			Mailing List
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
  	<%=wrm.GetString("MLtxt5")%>
  	</td>
  	<td width="20%" class="GridTitle" align="right">
  	<%=wrm.GetString("MLtxt34")%>
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
  		<span class="normal" style="cursor:pointer;text-decoration:underline" onclick="CreateBox('/MailingList/VerifyMailInML.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);"><%=wrm.GetString("MLtxt42")%></span>
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
  		<span class="normal" style="cursor:pointer;text-decoration:underline" onclick="CreateBox('/MailingList/VerifyMailInML.aspx?render=no&id=<%#DataBinder.Eval(Container.DataItem,"id")%>',event,500,250);"><%=wrm.GetString("MLtxt42")%></span>
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
    <div><%=wrm.GetString("MLtxt6")%>
	<domval:RequiredDomValidator id="rvNewMLTitle"
		runat="server" ControlToValidate="NewMLTitle" ErrorMessage="*"/>
    </div>
    <asp:TextBox id="NewMLTitle" runat="server" cssClass="BoxDesign"/>
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
    <b><%=wrm.GetString("MLtxt7")%>
		<input type="radio" name="Selmails" onclick="selmails(0)" checked><%=wrm.GetString("MLtxt27")%>
		<input type="radio" name="Selmails" onclick="selmails(1)"><%=wrm.GetString("MLtxt28")%>
		<input type="radio" name="Selmails" onclick="selmails(2)"><%=wrm.GetString("MLtxt29")%>
		<input type="radio" name="Selmails" onclick="selmails(3)"><%=wrm.GetString("MLtxt30")%>
    </b><br>
    </td>
  </tr>
  <tr>
  <td>
  <table id="ReSearchCompanies" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">

    <tr>
	  <td width="20%" valign="top" nowrap><%=wrm.GetString("Bcotxt17")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
	    <span id="Advanced_CompanyNameqc">
			<asp:TextBox runat=server id="Advanced_CompanyName" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_CompanyName')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_CompanyName')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Addressqc">
			<asp:TextBox runat=server id="Advanced_Address" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Cityqc">
			<asp:TextBox runat=server id="Advanced_City" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_City')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_City')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Stateqc">
			<asp:TextBox runat=server id="Advanced_State" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_State')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_State')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Nationqc">
			<asp:TextBox runat=server id="Advanced_Nation" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Nation')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Nation')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Zipqc">
			<asp:TextBox runat=server id="Advanced_Zip" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Zip')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Zip')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt20")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Phoneqc">
			<asp:TextBox runat=server id="Advanced_Phone" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Phone')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Phone')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt21")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Faxqc">
			<asp:TextBox runat=server id="Advanced_Fax" text="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Fax')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Fax')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Emailqc">
			<asp:TextBox runat=server id="Advanced_Email" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Email')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Email')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt23")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Siteqc">
			<asp:TextBox runat=server id="Advanced_Site" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Site')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Site')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=wrm.GetString("Bcotxt11")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="Advanced_Codeqc">
			<asp:TextBox runat=server id="Advanced_Code" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('Advanced_Code')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('Advanced_Code')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt8")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt9")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt10")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt11")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt12")%></td>
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
		<td width="20%" valign="top" nowrap><%=wrm.GetString("CRMopptxt1")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt45")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
		<span id="SAdvanced_Categoryqc">
			<asp:DropDownList id="SAdvanced_Category" old=true runat="server" cssClass="BoxDesign"/>
	    </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvanced_Category')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvanced_Category')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt64")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
			<asp:TextBox id="Advanced_Owner" readonly=true runat="server" cssClass="BoxDesign"/>
			<asp:TextBox id="Advanced_OwnerID" style="display:none;" runat="server" class="BoxDesign"/>
	  </td><td valign=bottom nowrap>
			&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=Advanced_Owner&textbox2=Advanced_OwnerID',event)">
			&nbsp;<img src="/i/erase.gif" border="0" style="cursor:pointer" onclick="CleanField('Advanced_OwnerID');CleanField('Advanced_Owner')">
      </td>
    </tr>
  </table>
  <table id="AdvancedContacts" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Addressqc">
			<asp:TextBox runat=server id="AdvancedContacts_Address" text="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Cityqc">
			<asp:TextBox runat=server id="AdvancedContacts_City" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_City')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_City')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Stateqc">
			<asp:TextBox runat=server id="AdvancedContacts_State" text="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_State')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_State')" style="cursor:pointer">
      </td>
    </tr>
	<tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Nationqc">
			<asp:TextBox runat=server id="AdvancedContacts_Nation" text="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Nation')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Nation')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Zipqc">
			<asp:TextBox runat=server id="AdvancedContacts_Zip" text="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Zip')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Zip')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedContacts_Emailqc">
			<asp:TextBox runat=server id="AdvancedContacts_Email" text="" class="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedContacts_Email')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedContacts_Email')" style="cursor:pointer">
	  </td>
    </tr>
    <tr>
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt45")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
		<span id="SAdvancedContacts_Categoryqc">
			<asp:DropDownList id="SAdvancedContacts_Category" old=true runat="server" cssClass="BoxDesign"/>
	    </span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('SAdvancedContacts_Category')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('SAdvancedContacts_Category')" style="cursor:pointer">
      </td>
    </tr>
  </table>
  <table id="AdvancedLeads" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt26")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
      <td width="60%">
        <span id="AdvancedLead_Addressqc">
			<asp:TextBox runat=server id="AdvancedLead_Address" value="" cssClass="BoxDesign" width="90%"/>
		</span>
	  </td><td valign=bottom nowrap>
		 <img src=/images/plus.gif onclick="newor('AdvancedLead_Address')" style="cursor:pointer">
		 <img src=/images/minus.gif onclick="delor('AdvancedLead_Address')" style="cursor:pointer">
      </td>
    </tr>
    <tr>
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt27")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
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
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt28")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
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
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt53")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
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
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt29")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
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
	  <td width="20%" valign="top"><%=wrm.GetString("Bcotxt22")%></td>
      <td width="20%" valign="top" align="center"><%=wrm.GetString("CRMcontxt56")%></td>
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
		<td width="20%" valign="top" nowrap><%=wrm.GetString("CRMopptxt1")%></td>
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
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt45")%></td>
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
    <tr>
      <td width="20%" valign="top" nowrap><%=wrm.GetString("CRMcontxt64")%></td>
      <td width="20%" valign="top" align="center">&nbsp;</td>
      <td width="60%">
			<asp:TextBox id="AdvancedLead_Owner" readonly=true runat="server" class="BoxDesign"/>
			<asp:TextBox id="AdvancedLead_OwnerID" style="display:none;" runat="server" class="BoxDesign"/>
	  </td><td valign=bottom nowrap>
			&nbsp;<img src="/i/user.gif" border="0" style="cursor:pointer" onclick="CreateBox('/common/PopAccount.aspx?render=no&textbox=AdvancedLead_Owner&textbox2=AdvancedLead_OwnerID',event)">
			&nbsp;<img src="/i/erase.gif" border="0" style="cursor:pointer" onclick="CleanField('AdvancedLead_OwnerID');CleanField('AdvancedLead_Owner')">
      </td>
    </tr>
  </table>
   <table id="ReSearchFixedMails" style="display:none" runat="server" border="0" cellpadding="0" cellspacing="0" width="100%" class="normal" align="center">
	<tr>
		<td width="40%" valign=top>
			<div><%=wrm.GetString("Acttxt63")%></div>
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
			<div><%=wrm.GetString("Acttxt64")%></div>
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
			<div><%=wrm.GetString("Acttxt89")%></div>
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
			<asp:ListBox id="MLFixedMails" runat="server" cssclass="listboxautoform" Rows="10" SelectionMode="Multiple" />
			<input type=hidden runat=server id="ListFixedParams" name="ListFixedParams">
		</td>
		<td width="10%" valign="top">
			<span class="Save" style="cursor:pointer" onclick="RemoveFixedParams()"><%=wrm.GetString("Delete")%></span>
		</td>
	</tr>
   </table>
  </td>
  </tr>
  <tr>
    <td align="right" nowrap>
    <asp:LinkButton id="SearchAdvanced" visible=false runat="server" />
    &nbsp;&nbsp;
    <asp:LinkButton id="SaveML" runat="server"  cssClass="save"/>
    </td>
  </tr>
  </table>
</td>
<td width="40%" valign=top>
  <table style="display:none" cellpadding="0" cellspacing="0" width="100%" class="normal">
  <tr>
  	<td class="GridTitle" colspan="3"><%=wrm.GetString("MLtxt10")%></td>
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
  		<td class="GridTitle" colspan="2"><%=wrm.GetString("MLtxt23")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt39")%>&nbsp;<asp:Label id="MLFillCount" runat=server/></div>
					<asp:ListBox id="MLFill" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFillRemoved" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill" runat="server" cssclass="Save"/>
	</td>
	</tr>
	<tr>
  		<td class="GridTitle" colspan="2"><%=wrm.GetString("MLtxt24")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt39")%>&nbsp;<asp:Label id="MLFill2Count" runat=server/></div>
					<asp:ListBox id="MLFill2" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill2Removed" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
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
  		<td class="GridTitle" colspan="2"><%=wrm.GetString("MLtxt25")%></td>
	</tr>
	<tr>
	<td>
		<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt39")%>&nbsp;<asp:Label id="MLFill3Count" runat=server/></div>
					<asp:ListBox id="MLFill3" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill3Removed" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
			</tr>
		</table>

	</td>
	<td width="1%" valign="top">
			<asp:LinkButton id="RemoveMLFill3" runat="server" cssclass="Save"/>
	</td>
	</tr>
	<tr>
  		<td class="GridTitle" colspan="2"><%=wrm.GetString("MLtxt26")%></td>
	</tr>
	<tr>
	<td>
			<table width="100%" class=normal>
			<tr>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt39")%>&nbsp;<asp:Label id="MLFill4Count" runat=server/></div>
					<asp:ListBox id="MLFill4" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
				</td>
				<td width="50%">
					<div><%=wrm.GetString("MLtxt40")%></div>
					<asp:ListBox id="MLFill4Removed" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
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
	<div class=normal><%=wrm.GetString("MLtxt11")%></div>
	<asp:TextBox id="SenderTextBox" runat="server" CssClass="BoxDesign"/>
  </td>

  <td class=normal valign="bottom">
		<div class=normal><%=wrm.GetString("MLtxt46")%></div>
		<asp:CheckBox id="ScheduleCheckBox" runat="server"/>
		<asp:TextBox id="ScheduleStartDate" width="70px" runat="server" onkeypress="DataCheck(this,event)" class="BoxDesign" maxlength="10" />
		<img src="/i/SmallCalendar.gif" style="cursor:pointer;" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=ScheduleStartDate',event,195,195)">
		  &nbsp;<asp:TextBox ID="ScheduleStartHour" width="50px" onkeypress="HourCheck(this,event)" runat="server" maxlength="8" class="BoxDesign"  />
	       <img src="/images/up.gif" onclick="HourUp('ScheduleOrainizio')" />
	       <img src="/images/down.gif" onclick="HourDown('ScheduleOrainizio')" />
   </td>
   </tr>
   <tr>
   <td align=right>
        <asp:LinkButton id="Verifymail" runat=server cssClass="Save"/>
        <asp:Literal id="MailPreview" runat="server"/>

        <asp:LinkButton id="SendML" runat="server"  cssClass="Save"/>
   </td>
   </tr>
</table>

<table width="90%" align="center">
<tr>
  <td>
	<asp:Label id="LblSendError" runat=server/>
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
