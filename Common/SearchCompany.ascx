<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SearchCompany.ascx.cs"
    Inherits="Digita.Tustena.Common.SearchCompany" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="twc" Namespace="Digita.Tustena.WebControls" Assembly="Digita.Tustena.WebControls" %>
<script type="text/javascript" src="/js/autodate.js"></script>
<script language="javascript" src="/js/hashtable.js"></script>
<script language="javascript" src="/js/search.js"></script>
<table id="ReSearchCompanies" runat="server" border="0" cellpadding="0" cellspacing="0"
    width="100%" class="normal" align="center">
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="Bcotxt17" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_CompanyNameqc">
                <asp:TextBox runat="server" ID="Advanced_CompanyName" value="" CssClass="BoxDesign"
                    Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_CompanyName')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_CompanyName')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt26" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Addressqc">
                <asp:TextBox runat="server" ID="Advanced_Address" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Address')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Address')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt27" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Cityqc">
                <asp:TextBox runat="server" ID="Advanced_City" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_City')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_City')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt28" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Stateqc">
                <asp:TextBox runat="server" ID="Advanced_State" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_State')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_State')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt53" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Nationqc">
                <asp:TextBox runat="server" ID="Advanced_Nation" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Nation')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Nation')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt29" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Zipqc">
                <asp:TextBox runat="server" ID="Advanced_Zip" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Zip')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Zip')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt20" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Phoneqc">
                <asp:TextBox runat="server" ID="Advanced_Phone" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Phone')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Phone')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt21" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Faxqc">
                <asp:TextBox runat="server" ID="Advanced_Fax" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Fax')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Fax')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt22" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Emailqc">
                <asp:TextBox runat="server" ID="Advanced_Email" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Email')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Email')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top">
            <twc:LocalizedLiteral Text="Bcotxt23" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Siteqc">
                <asp:TextBox runat="server" ID="Advanced_Site" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Site')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Site')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="Bcotxt11" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            <twc:LocalizedLiteral Text="CRMcontxt56" runat="server" /></td>
        <td width="60%">
            <span id="Advanced_Codeqc">
                <asp:TextBox runat="server" ID="Advanced_Code" value="" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','Advanced_Code')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','Advanced_Code')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt8" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%">
            <span id="SAdvanced_CompanyTypeqc">
                <asp:DropDownList ID="SAdvanced_CompanyType" old="true" runat="server" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvanced_CompanyType')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvanced_CompanyType')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt9" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%">
            <span id="SAdvanced_ContactTypeqc">
                <asp:DropDownList ID="SAdvanced_ContactType" old="true" runat="server" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvanced_ContactType')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvanced_ContactType')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt10" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;
        </td>
        <td width="60%">
            <span id="RAdvanced_Billedqc"><span id="RemoveRAdvanced_Billed">
                <input type="radio" id="RAdvanced_Billed0" name="RAdvanced_Billed" value="0">=
                <input type="radio" id="RAdvanced_Billed1" name="RAdvanced_Billed" value="1">=
                <input type="radio" id="RAdvanced_Billed2" name="RAdvanced_Billed" value="2">&lt;
                <input type="radio" id="RAdvanced_Billed3" name="RAdvanced_Billed" value="3">?
                <input type="radio" id="RAdvanced_Billed4" name="RAdvanced_Billed" value="4">&gt;
                <input type="radio" id="RAdvanced_Billed5" name="RAdvanced_Billed" value="5">= </span>
                <asp:TextBox ID="Advanced_Billed" runat="server" class="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','RAdvanced_Billed')" style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','RAdvanced_Billed')" style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt11" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;
        </td>
        <td width="60%">
            <span id="RAdvanced_Employeesqc"><span id="RemoveRAdvanced_Employees">
                <input type="radio" id="RAdvanced_Employees0" name="RAdvanced_Employees" value="0">=
                <input type="radio" id="RAdvanced_Employees1" name="RAdvanced_Employees" value="1">=
                <input type="radio" id="RAdvanced_Employees2" name="RAdvanced_Employees" value="2">&lt;
                <input type="radio" id="RAdvanced_Employees3" name="RAdvanced_Employees" value="3">?
                <input type="radio" id="RAdvanced_Employees4" name="RAdvanced_Employees" value="4">&gt;
                <input type="radio" id="RAdvanced_Employees5" name="RAdvanced_Employees" value="5">=
            </span>
                <asp:TextBox ID="Advanced_Employees" runat="server" class="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','RAdvanced_Employees')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','RAdvanced_Employees')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt12" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%">
            <span id="SAdvanced_Estimateqc">
                <asp:DropDownList ID="SAdvanced_Estimate" old="true" runat="server" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td width="2%" valign="bottom">
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvanced_Estimate')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvanced_Estimate')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMopptxt1" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%">
            <span id="SAdvanced_Opportunityqc">
                <asp:DropDownList ID="SAdvanced_Opportunity" old="true" runat="server" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td width="2%" valign="bottom">
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvanced_Opportunity')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvanced_Opportunity')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="CRMcontxt45" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%">
            <span id="SAdvanced_Categoryqc">
                <asp:DropDownList ID="SAdvanced_Category" old="true" runat="server" CssClass="BoxDesign" Width="98%" />
            </span>
        </td>
        <td valign="bottom" nowrap>
            <img src="/images/plus.gif" onclick="newor('<%=this.ID%>','SAdvanced_Category')"
                style="cursor: pointer">
            <img src="/images/minus.gif" onclick="delor('<%=this.ID%>','SAdvanced_Category')"
                style="cursor: pointer">
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral Text="InsDate" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%" colspan="2">
            <table width="100%" cellspacing="0" cellpadding="0" class="normal">
                <tr>
                    <td width="45%">
                        <twc:LocalizedLiteral Text="Das4txt5" runat="server" />
                        <asp:TextBox ID="AdvancedCreatedDate1" onkeypress="DataCheck(this,event)" runat="server"
                            class="BoxDesign" EnableViewState="true" MaxLength="10" />
                    </td>
                    <td width="30" valign="bottom">
                        &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_AdvancedCreatedDate1',event,195,195)">
                    </td>
                    <td width="45%">
                        <twc:LocalizedLiteral Text="Das4txt6" runat="server" />
                        <asp:TextBox ID="AdvancedCreatedDate2" onkeypress="DataCheck(this,event)" runat="server"
                            class="BoxDesign" EnableViewState="true" MaxLength="10" />
                    </td>
                    <td width="30" valign="bottom">
                        &nbsp;<img src="/i/SmallCalendar.gif" border="0" style="cursor: pointer" onclick="CreateBox('/Common/PopUpDate.aspx?Textbox=<%=this.ID%>_AdvancedCreatedDate2',event,195,195)">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="20%" valign="top" nowrap>
            <twc:LocalizedLiteral ID="LocalizedLiteral1" Text="Geotxt1" runat="server" /></td>
        <td width="20%" valign="top" align="center">
            &nbsp;</td>
        <td width="60%" colspan=2>
            <table width="100%" cellspacing="1" cellpadding="0" class="normal">
                <tr>
                    <td valign=bottom>
                        <asp:CheckBox runat="server" ID="geoCheck" onclick="geoEnable(this)"/>
                    </td>
                    <td width="32%">
                        <twc:LocalizedLiteral ID="LocalizedLiteral2" Text="Geotxt2" runat="server" />
                        <asp:TextBox runat="server" ID="geoCity" CssClass="BoxDesign" disabled/>
                    </td>
                    <td width="32%">
                        <twc:LocalizedLiteral ID="LocalizedLiteral3" Text="Geotxt3" runat="server" />
                        <asp:TextBox runat="server" ID="geoDistance" CssClass="BoxDesign" MaxLength="2" onkeypress="NumbersOnly(event,'',this)" disabled/>
                    </td>
                    <td width="32%">
                        <twc:LocalizedLiteral ID="LocalizedLiteral4" Text="Geotxt4" runat="server" />
                        <asp:DropDownList ID="geoCountry" old="true" runat="server" CssClass="BoxDesign" disabled/>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<twc:jscontrolid id="jsc" runat=server />
<twc:LocalizedScript ID="LocalizedScript1" resource="StillBeta" runat="server" />

<script type="text/javascript">
function geoEnable(o)
{
    var geoCity = document.getElementById(jsControlId+"geoCity");
    var geoDistance = document.getElementById(jsControlId+"geoDistance");
    if(o.checked)
    {
        alert(StillBeta);
        geoCity.style.backgroundColor = '';
        geoDistance.style.backgroundColor = '';
    }
    geoCity.disabled=!o.checked;
    geoDistance.disabled=!o.checked;
    document.getElementById(jsControlId+"geoCountry").disabled=!o.checked;
}
</script>
