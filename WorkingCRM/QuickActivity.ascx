<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Register TagPrefix="domval" Namespace="System.Web.UI.WebControls.DomValidators"
    Assembly="System.Web.UI.WebControls.DomValidators" %>
<%@ Control Language="c#" AutoEventWireup="true" Codebehind="QuickActivity.ascx.cs" Inherits="Digita.Tustena.WorkingCRM.QuickActivity" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<twc:jscontrolid id="jsc" runat="server" />
<script>


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
						CreateBox('/Common/PopCompany.aspx?render=no&textbox='+jsControlId+'CrossWithText&textbox2='+jsControlId+'CrossWithID&event=CheckAddress()',e,500,400);
						break;
					case 1:
						CreateBox('/common/popcontacts.aspx?render=no&textbox='+jsControlId+'CrossWithText&textboxID='+jsControlId+'CrossWithID&event=CheckAddress()',e,400,300);
						break;
					case 2:
						CreateBox('/common/PopLead.aspx?render=no&textbox='+jsControlId+'CrossWithText&textboxID='+jsControlId+'CrossWithID&event=CheckAddress()',e,400,300);
						break;
				}
			}
</script>
<table cellSpacing="0" cellPadding="0" width="100%">
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="20%">
					        <asp:TextBox ID="TextBoxId" runat=server Visible=false></asp:TextBox>
					        <asp:textbox onkeypress="DataCheck(this,event)" id="TextBoxData" CssClass="BoxDesign" Width="100%"
							runat="server"></asp:textbox></td>
					<td vAlign="middle" align="right" width="60">&nbsp;<twc:localizedImg style="CURSOR: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?frame=1&datediff=1&amp;textbox='+jsControlId+'TextBoxData&amp;Start='+(document.getElementById(jsControlId+'TextBoxData')).value,event,195,195)"
							alt="AcTooltip14" src="/i/SmallCalendar.gif" border="0" runat="server" id="LocalizedImg1" />
					</td>
					<td width="35%" align="right">
						<table class="normal" id="HourPanel" style="PADDING-LEFT:10px;PADDING-TOP:2px" cellSpacing="0"
							cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<twc:LocalizedLiteral text="Evnttxt6" runat="server" id="LocalizedLiteral1" />
								</td>
								<td><asp:textbox class="BoxDesign" onkeypress="HourCheck(this,event)" id="TextBoxHour" runat="server"
										width="50" maxlength="8"></asp:textbox></td>
								<td vAlign="middle" align="left" width="40"><IMG onclick="HourUp(jsControlId+'TextBoxHour')" src="/images/up.gif">
									<IMG onclick="HourDown(jsControlId+'TextBoxHour')" src="/images/down.gif">
								</td>
							</tr>
						</table>
					</td>
					<td width="40%">
						<asp:dropdownlist id="dropActivityType" Runat="server" CssClass="BoxDesign"></asp:dropdownlist>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td>
			<div>
				<table class="normal" cellSpacing="0" cellPadding="0">
					<tr>
						<td><twc:LocalizedLiteral text="Tictxt38" runat="server" id="LocalizedLiteral2" />
						</td>
						<td><asp:RadioButtonList id="CrossWith" runat="server" cssClass="normal"></asp:RadioButtonList></td>
						<td><img src="/i/user.gif" border="0" style="CURSOR:pointer" onclick="OpenSearchBox(event)"></td>
					</tr>
				</table>
			</div>
			<asp:TextBox ID="CrossWithID" Runat="server" style="DISPLAY:none"></asp:TextBox>
			<asp:TextBox ID="CrossWithText" Runat="server" CssClass="BoxDesignReq" ReadOnly="true"></asp:TextBox></td>
	</tr>
	<tr>
		<td>
			<div><twc:LocalizedLiteral text="Acttxt6" runat="server" id="LocalizedLiteral3" /></div>
			<asp:TextBox ID="txtSubject" Runat="server" CssClass="BoxDesign"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td>
			<div><twc:LocalizedLiteral text="Acttxt86" runat="server" id="LocalizedLiteral4" /></div>
			<asp:TextBox ID="txtNote" Runat="server" CssClass="BoxDesign" TextMode="MultiLine" Height="50px"></asp:TextBox>
		</td>
	</tr>
	<tr>
	    <td>
	        <div><twc:LocalizedLiteral text="Acttxt133" runat="server" id="LocalizedLiteral5" /></div>
	        <table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td width="49%"><asp:textbox onkeypress="DataCheck(this,event)" id="RecallDate" CssClass="BoxDesign" Width="100%"
							runat="server"></asp:textbox></td>
					<td vAlign="middle" align="right" width="60">&nbsp;<twc:localizedImg style="CURSOR: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?frame=1&datediff=1&amp;textbox='+jsControlId+'RecallDate&amp;Start='+(document.getElementById(jsControlId+'TextBoxData')).value,event,195,195)"
							alt="AcTooltip14" src="/i/SmallCalendar.gif" border="0" runat="server" />
					</td>
					<td width="50%" align="right">
						<table class="normal" id="Table1" style="PADDING-LEFT:10px;PADDING-TOP:2px" cellSpacing="0"
							cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td>
									<twc:LocalizedLiteral text="Evnttxt5" runat="server" id="LocalizedLiteral6" />
								</td>
								<td><asp:textbox class="BoxDesign" onkeypress="HourCheck(this,event)" id="RecallTextBoxHour" runat="server"
										width="50" maxlength="8"></asp:textbox></td>
								<td vAlign="middle" align="left" width="40"><IMG onclick="HourUp(jsControlId+'RecallTextBoxHour')" src="/images/up.gif">
									<IMG onclick="HourDown(jsControlId+'RecallTextBoxHour')" src="/images/down.gif">
								</td>
								<td>
									<twc:LocalizedLiteral text="Evnttxt6" runat="server" id="LocalizedLiteral7" />
								</td>
								<td><asp:textbox class="BoxDesign" onkeypress="HourCheck(this,event)" id="RecallTextBoxHour2" runat="server"
										width="50" maxlength="8"></asp:textbox></td>
								<td vAlign="middle" align="left" width="40"><IMG onclick="HourUp(jsControlId+'RecallTextBoxHour2')" src="/images/up.gif">
									<IMG onclick="HourDown(jsControlId+'RecallTextBoxHour2')" src="/images/down.gif">
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
	    </td>
	</tr>
	<tr>
		<td align="right">
			<asp:LinkButton ID="btnSave" Runat="server" CssClass="Save" onclick="btnSave_Click"></asp:LinkButton>
		</td>
	</tr>
</table>
