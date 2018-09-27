<%@ Page language="c#" Codebehind="chronology.aspx.cs" Inherits="Digita.Tustena.WorkingCRM.chronology" trace="false" AutoEventWireup="false" %>
<%@ Register TagPrefix="Chrono" TagName="ActivityChronology" Src="~/WorkingCRM/ActivityChronology.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html>
  <head runat=server>
    <title>chronology</title>
  </head>

  <body>
	<form runat=server>
	<Chrono:ActivityChronology id="AcCrono" runat="server"/>
	</form>
  </body>
</html>
