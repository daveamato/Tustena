<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls"%>
<%@ Control Language="c#" Debug="false" codebehind="GroupControl.ascx.cs" Inherits="Digita.Tustena.GroupControl"%>
<twc:jscontrolid id="jsc" runat=server />
<script>
function mover(move){
var inBox = getElement(jsControlId+"ListGroups");
var outBox = getElement(jsControlId+"ListDip");
if(move == 'addall' || move == 'add')
{
	for(x = 0;x<(inBox.length);x++)
		{
			if(inBox.options[x].selected || move == 'addall')
			{
				with(outBox)
				{
					options[options.length] = new Option(inBox.options[x].text,inBox.options[x].value);
				}
				inBox.options[x] = null;
				x = -1;
			}
		}
	sortSelect(outBox);
	}
	if(move == 'removeall' || move == 'remove')
	{

		for(x = 0;x<(outBox.length);x++)
		{
			if(outBox.options[x].value==mygroup) continue;
			if(outBox.options[x].selected || move == 'removeall')
			{
				with(inBox)
				{
					options[options.length] = new Option(outBox.options[x].text,outBox.options[x].value);
				}
				outBox.options[x] = null;
				x = -1;
			}
		}
	sortSelect(inBox);
	}

	var GroupValuetxt = "|";
	for (x=0;x<(outBox.length);x++)
	{
		GroupValuetxt += outBox.options[x].value + "|";
	}
	document.getElementById(jsControlId+"GroupValue").value = GroupValuetxt;

	return true;
}

function compareText (option1, option2) {
	return option1.text < option2.text ? -1 :
	option1.text > option2.text ? 1 : 0;
}

function sortSelect (select) {
	if(!IE4plus) return;
	var options = new Array (select.options.length);
	for (var i = 0; i < options.length; i++)
	options[i] =
		new Option (
		select.options[i].text,
		select.options[i].value,
		select.options[i].defaultSelected,
		select.options[i].selected
		);
	options.sort(compareText);
	select.options.length = 0;
	for (var i = 0; i < options.length; i++)
	select.options[i] = options[i];
}

</script>
<asp:Table id="Groups_Table" width="100%" align="center" runat="server" CellPadding="4" visible="true"
	EnableViewState="true">
	<asp:TableRow>
		<asp:TableCell width="45%">
			<div class="divautoform"><twc:LocalizedLiteral text="Grctxt6" runat="server"/></div>
			<asp:ListBox id="ListGroups" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
		</asp:TableCell>
		<asp:TableCell align="center" width="10%">
			<table>
				<tr>
					<td>
						<input type="button" id="Btn_FwwAll" onclick="mover('addall')" runat="server" value=">>"
							class="btn">
					</td>
				</tr>
				<tr>
					<td>
						<input type="button" id="Btn_Fww" onclick="mover('add')" runat="server" value=">" class="btn">
					</td>
				</tr>
				<tr>
					<td>
						<input type="button" id="Btn_Rww" onclick="mover('remove')" runat="server" value="<" class="btn">
					</td>
				</tr>
				<tr>
					<td>
						<input type="button" id="Btn_RwwAll" onclick="mover('removeall')" runat="server" value="<<"
							class="btn">
					</td>
				</tr>
			</table>
		</asp:TableCell>
		<asp:TableCell width="45%">
			<div class="divautoform"><twc:LocalizedLiteral text="Grctxt5" runat="server"/></div>
			<asp:ListBox id="ListDip" runat="server" cssclass="listboxautoform" Rows="7" SelectionMode="Multiple" />
			<input type="hidden" id="GroupValue" runat="server">
		</asp:TableCell>
	</asp:TableRow>
</asp:Table>
