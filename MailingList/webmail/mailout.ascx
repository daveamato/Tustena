<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Control Language="c#" AutoEventWireup="true" Codebehind="mailout.ascx.cs" Inherits="Digita.Tustena.MailingList.webmail.mailout" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<twc:jscontrolid id="jsc" runat=server />
<table class="normal" width="100%" align="center">
	<tr>
		<td colspan="2">
			<div><twc:LocalizedLiteral text="Mailtxt1" runat="server" id=LocalizedLiteral1 /></div>
			<asp:TextBox ID="MailAddressFrom" Runat="server" CssClass="BoxDesign" width="150px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<div>
				<table cellspacing="0" cellpadding="0" class="normal">
					<tr>
						<td><twc:LocalizedLiteral text="Mailtxt2" runat="server" id=LocalizedLiteral2 />
							<domval:RequiredDomValidator id="rvMailAddressTo" EnableClientScript="True" runat="server" ControlToValidate="MailAddressTo"
								ErrorMessage="*" />
							<domval:RegexDomValidator ID="mailValidator" runat="server" ValidationExpression="(^\s*)(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6}(\s*)$"
                                                        ErrorMessage="*" ControlToValidate="MailAddressTo" />
						</td>
						<td><asp:RadioButtonList id="CrossWith" runat="server" cssClass="normal"></asp:RadioButtonList></td>
						<td><img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="OpenSearchBox(event)"></td>
						<td><asp:CheckBox id="CreateActivity" runat="server" /><twc:LocalizedLiteral text="Mailtxt18" runat="server" id=LocalizedLiteral3 /></td>
					</tr>
				</table>
			</div>
			<asp:TextBox ID="MailAddressToID" Runat="server" style="DISPLAY:none"></asp:TextBox>
			<asp:TextBox ID="MailAddressTo" Runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td>
			<div><twc:LocalizedLiteral text="Mailtxt3" runat="server" id=LocalizedLiteral4 /></div>
			<asp:TextBox ID="MailAddressCc" Runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
		<td>
			<div><twc:LocalizedLiteral text="Mailtxt4" runat="server" id=LocalizedLiteral5 /></div>
			<asp:TextBox ID="MailAddressCcn" Runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td>
			<div><twc:LocalizedLiteral text="Mailtxt5" runat="server" id=LocalizedLiteral6 /></div>
			<asp:TextBox ID="MailObject" Runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
		<td>
		    <span id="StorageModule" runat=server>
			    <div><twc:LocalizedLiteral text="Mailtxt6" runat="server" id=LocalizedLiteral7 /></div>
			    <table width="50%" cellspacing="0" cellpadding="0">
				    <tr>
					    <td width="60%">
						    <asp:TextBox id="DocumentDescription" runat="server" CssClass="BoxDesign" readonly />
						    <asp:TextBox id="IDDocument" runat="server" style="DISPLAY:none"></asp:TextBox>
					    </td>
					    <td width="40%" nowrap>&nbsp; <twc:LocalizedImg src="/i/sheet.gif" border=0 alt="Acttxt98" style="CURSOR:pointer" onclick="OpenAttach(event)" runat=server />
						    <img src="/i/deletedoc.gif" border="0" style="CURSOR:pointer" onclick="ClearDocument()">
					    </td>
				    </tr>
			    </table>
			</span>
		</td>
	</tr>
	<tr>
		<td colspan="2">
			<div><twc:LocalizedLiteral text="Mletxt3" runat="server" id=LocalizedLiteral8 /></div>
			<asp:TextBox ID="MailMessage" Runat="server" CssClass="BoxDesign" TextMode="MultiLine"
				Height="150px"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right" colspan="2">
			<asp:LinkButton ID="Submitbtn" Runat="server" cssClass="Save" onclick="SubmitBtn_Click"></asp:LinkButton>
			<asp:Label ID="ActivityCross" Runat="server" Visible="False"></asp:Label>
		</td>
	</tr>
</table>
<script>
		function ClearDocument()
		{
 			(document.getElementById(jsControlId+"DocumentDescription")).value = "";
 			(document.getElementById(jsControlId+"IDDocument")).value = "";
 			var obj = document.getElementById(jsControlId+"LinkDocument");
 			if (obj != null)
 				obj.style.display = "none";
		}

		function OpenAttach(e){
			CreateBox('/common/PopFile.aspx?render=no&textbox=<%=this.ID%>_DocumentDescription&textboxID=<%=this.ID%>_IDDocument',e,550,400);
		}
			function OpenSearchBox(e){
				var x;

				if((document.getElementById(jsControlId+"CrossWith_0")).checked)
					x=0;
				else if	((document.getElementById(jsControlId+"CrossWith_1")).checked)
						x=1;
					 else if ((document.getElementById(jsControlId+"CrossWith_2")).checked)
							x=2;
						  else
							x=3;


				switch(x){
					case 0:
						CreateBox('/Common/PopCompany.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textbox2='+jsControlId+'MailAddressToID&email=1',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textboxID='+jsControlId+'MailAddressToID&email=1',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox='+jsControlId+'MailAddressTo&textboxID='+jsControlId+'MailAddressToID&email=1',e,400,300);
						break;
				}
			}
</script>

