<%@ Page language="c#" Codebehind="sendsingleaddressmail.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.MailingList.sendsingleaddressmail" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators" Assembly="System.Web.UI.WebControls.DomValidators" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head id=head runat=server>
    <title>sendsingleaddressmail</title>
  </HEAD>
  <body id=body runat=server>
	<script>
		function seladdress(em,cross,id)
		{
			var MailAddress = document.getElementById("MailAddress");
			var MailAddressToID = document.getElementById("MailAddressToID");
			var CrossWith = document.getElementById("CrossWith");
			var MailAddress = document.getElementById("MailAddress");
			var MailList = document.getElementById("MailList");
			MailAddress.value=em;
			MailAddressToID.value=id;
			CrossWith.value=cross;
			MailList.style.display="none";
		}
	</script>
    <form id="Form1" method="post" runat="server">
    <domval:DomValidationSummary
		id="valSum"
		ShowMessageBox=true
   DisplayMode="BulletList"
   EnableClientScript="true"
   runat="server"/>
		<table width="100%" cellspacing="0">
			<tr>
				<td width="140" class="SideBorderLinked" valign="top">
				&nbsp;
				</td>
				<td>
				<table width="98%" align=center cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td valign="bottom" width="20%" align="left">
							<asp:RadioButtonList id="FlagSearch" runat="server" />
						</td>
						<td valign="bottom" width="20%">
							<asp:TextBox Id="SearchTextBox" runat="server" class="BoxDesign" Height="20" />
						</td>
						<td width="30%" align="left" style="padding-left:5px">
							<asp:LinkButton Id="BtnSearch" runat="server" class="save" />
						</td>
						<td width="50%" align="left" style="padding-left:5px">
							<asp:CheckBox id="CreateActivity" runat="server"/><%=wrm.GetString("Mailtxt18")%>
						</td>
					</tr>
					<tr>
						<td colspan=4 style="border-bottom:1px solid #000;">
						&nbsp;
						<asp:Repeater ID="MailRepeater" Runat=server>
							<HeaderTemplate>
								<table class="tblstruct" id="MailList">
										<tr>
											<td align="left" class="BorderBottomTitles">
												<twc:localizedLiteral text="MLtxt15" runat="server" ID="Localizedliteral1"/>
											</td>
											<td align="left" class="BorderBottomTitles">
												<twc:localizedLiteral text="Paztxt8" runat="server" ID="Localizedliteral2"/>
											</td>
										</tr>
							</HeaderTemplate>
							<ItemTemplate>
										<tr>
											<td align="left" class="GridItem">
												<span style="cursor:pointer" onclick="seladdress('<%#DataBinder.Eval(Container.DataItem,"email")%>','<%#DataBinder.Eval(Container.DataItem,"type")%>','<%#DataBinder.Eval(Container.DataItem,"id")%>')"><%#DataBinder.Eval(Container.DataItem,"contact")%></span>
											</td>
											<td align="left" class="GridItem">
												<span style="cursor:pointer" onclick="seladdress('<%#DataBinder.Eval(Container.DataItem,"email")%>','<%#DataBinder.Eval(Container.DataItem,"type")%>','<%#DataBinder.Eval(Container.DataItem,"id")%>')"><%#DataBinder.Eval(Container.DataItem,"email")%></span>
											</td>
										</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
										<tr>
											<td align="left" class="GridItemAltern">
												<span style="cursor:pointer" onclick="seladdress('<%#DataBinder.Eval(Container.DataItem,"email")%>','<%#DataBinder.Eval(Container.DataItem,"type")%>','<%#DataBinder.Eval(Container.DataItem,"id")%>')"><%#DataBinder.Eval(Container.DataItem,"contact")%></span>
											</td>
											<td align="left" class="GridItemAltern">
												<span style="cursor:pointer" onclick="seladdress('<%#DataBinder.Eval(Container.DataItem,"email")%>','<%#DataBinder.Eval(Container.DataItem,"type")%>','<%#DataBinder.Eval(Container.DataItem,"id")%>')"><%#DataBinder.Eval(Container.DataItem,"email")%></span>
											</td>
										</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
								</table>
							</FooterTemplate>
						</asp:Repeater>
						</td>
					</tr>
					</table>
					<table width="98%" align=center cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td valign="top" width="20%" align="left">
						<table width="98%" align=center cellspacing="0" cellpadding="0" border="0">
					<tr>
						<td>
							<div><twc:localizedLiteral text="Mailtxt1" runat="server" ID="Localizedliteral3"/>
							<domval:RegexDomValidator id="RegexMailValidator1" runat="server" ControlToValidate="FromMailAddress" Display=Static>*</domval:RegexDomValidator>
					<domval:RequiredDomValidator ID="RequiredMailValidator1" Display=Static ControlToValidate="FromMailAddress" Runat="server">*</domval:RequiredDomValidator>
							</div>
							<asp:TextBox Id="FromMailAddress" runat="server" class="BoxDesign" Height="20"/>
						</td>
						<td colspan=2>&nbsp;</td>
					</tr>
					<tr>
						<td>
							<div><twc:localizedLiteral text="Mailtxt2" runat="server" ID="Localizedliteral4"/>
							<domval:RegexDomValidator id="RegexMailValidator2" runat="server"  ControlToValidate="MailAddress" Display=Static>*</domval:RegexDomValidator>
					<domval:RequiredDomValidator ID="RequiredMailValidator2" Display=Static ControlToValidate="MailAddress" Runat="server">*</domval:RequiredDomValidator>
							</div>
							<asp:TextBox Id="MailAddress" runat="server" class="BoxDesign" Height="20"/>
							<asp:TextBox ID="CrossWith" Runat="server" style="display:none"></asp:TextBox>
							<asp:TextBox ID="MailAddressToID" Runat="server" style="display:none"></asp:TextBox>
						</td>
						<td colspan=2>&nbsp;</td>
					</tr>
					<tr>
						<td>
							<div><twc:localizedLiteral text="Mailtxt5" runat="server" ID="Localizedliteral5"/></div>
							<asp:Label Id="MailSubject" runat="server" class="BoxDesign" Height="20"/>
						</td>
						<td colspan=2>&nbsp;</td>
					</tr>
					<tr>
						<td align=right style="padding-top:3px"><asp:LinkButton ID="SendMail" Runat=server CssClass="Save"></asp:LinkButton></td>
						<td colspan=2>&nbsp;</td>
					</tr>
					</tr>
				</table>
				</td>
				<td>

				<br>
				<center>
				<iframe id="Info1" name="Info1" runat="server" width="98%" height="400"
											scrolling="yes" frameBorder="0" marginHeight="0"
											marginWidth="0" bgcolor="#e5e5e5" style="border:1px solid #000"></iframe>
				</center>

				</td>
				</tr>
				</table>
				</td>
			</tr>
		</table>
     </form>

  </body>
</HTML>
