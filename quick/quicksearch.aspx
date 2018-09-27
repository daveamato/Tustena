<%@ Page language="c#" Codebehind="quicksearch.aspx.cs" AutoEventWireup="false" Inherits="Digita.Tustena.contactsearch" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head runat=server>
<link rel="stylesheet" type="text/css" href="/css/G.css">
<script language="Javascript" src="/js/common.js"></script>
<script language="Javascript" src="/js/menudigita.js"></script>
<script language="Javascript" src="/js/dynabox.js"></script>
<style>
.quickimg{
cursor: pointer;
margin-right: 3px;
}
table{
table-layout:fixed;
}
td{
text-overflow:ellipsis;
overflow:hidden;
white-space:nowrap;
 }
.lc1{background-color:#FDEEF4}
.lc2{background-color:#FCDFFF}
.lc3{background-color:#CFECEC}
.lc4{background-color:#B5EAAA}
.lc5{background-color:#FFF8C6}
.lc6{background-color:#BDEDFF}

</style>
</head>
	<body id="body" runat=server>
	<script>
	    var currentstatus='';
	    var currentactype='';
		var selectActivity;
		var activityLost = '<%=wrm.GetString("Quicktxt17")%>';
		var activityFuture = '<%=wrm.GetString("Quicktxt18")%>';
		selectActivity="<div class=\"menuitems\" onclick=\"newActivity(ownerId, 'Fax');\">&nbsp;&nbsp;<%=wrm.GetString("Acttxt47")%>&nbsp;&nbsp;</div>";
		selectActivity+="<div class=\"menuitems\" onclick=\"newActivity(ownerId, 'Letter');\">&nbsp;&nbsp;<%=wrm.GetString("Acttxt48")%>&nbsp;&nbsp;</div>";
		selectActivity+="<div class=\"menuitems\" onclick=\"newActivity(ownerId, 'Generic');\">&nbsp;&nbsp;<%=wrm.GetString("Acttxt49")%>&nbsp;&nbsp;</div>";

		function SelectContact(contactID)
		{
			 newActivity(contactID,"ViewActivity");
		}

		function newAppointment(contactID)
		{
			NewWindow('/calendar/PopAppointment.aspx?render=no&contactID=' + contactID,'Appointment','600','500','no')
		}
		function newActivity(contactID,action,actid)
		{
			parent.refreshactivity(contactID, action, actid);
			//parent.frames[3].location = "quickactivity.aspx?render=no&contactID=" + contactID + "&Action=" + action;
		}
		function newMail(contactID)
		{
			NewWindow('/common/PopMailHome.aspx?render=no&to=' + contactID,'Appointment','600','500','no')
		}

		function switchonoff(id)
		{
		  currentstatus=id;
		  table = document.getElementById("TableRendered");
		  if(table!=null){
		    cells = table.getElementsByTagName("tr");
		    for (var i = 0; i < cells.length; i++) {
    			status = cells[i].getAttribute("status");
    			if(id=="-1"){
    			    cells[i].style.display='';
    			    currentstatus='';
    			}else
    			    if ( status != id)
    			        cells[i].style.display='none';
		            else{
		                if(currentactype.length>0){
		                   actype = cells[i].getAttribute("actype");
		                    if ( actype != currentactype)
    			                cells[i].style.display='none';
		                    else
		                        cells[i].style.display='';
		                }else
		                    cells[i].style.display='';
		            }
		    }
		  }
		}

		function filterphn(type)
		{
		  currentactype=type;
		  table = document.getElementById("TableRendered");
		  if(table!=null){
		    cells = table.getElementsByTagName("tr");
		    for (var i = 0; i < cells.length; i++) {
    			actype = cells[i].getAttribute("actype");
    			if ( actype != type)
    			    cells[i].style.display='none';
		        else{
		            if(currentstatus.length>0){
		                status = cells[i].getAttribute("status");
		                if ( status != currentstatus)
    			            cells[i].style.display='none';
		                else
		                    cells[i].style.display='';
		            }else
		                cells[i].style.display='';
		        }
		    }
		  }

		}

		function parseTime(t)
		{
		var mins = parseInt(t.substr(1,2))*15;
		var hour = Math.round(mins/60);
		mins = mins%60;
		if(mins<10)
		 mins="0"+mins;
		var mins2 = parseInt(t.substr(4,2))*15;
		var hour2 = Math.round(mins2/60);
		mins2 = mins2%60;
		if(mins2<10)
		 mins2="0"+mins2;
		return hour + "." + mins + " - " + hour2 + "." + mins2;
		}

		function checkTime(t)
		{
		var start = parseInt(t.substr(1,2))*15;
		var stop = parseInt(t.substr(4,2))*15;
		var clock = new Date();
		var minutes = clock.getMinutes()+(clock.getHours()*60);
		if(start > minutes || stop < minutes){
		var ti = parseTime(t);
		var msg = '<%=wrm.GetString("Quicktxt16")%>';
		msg = msg.replace(/%s/, ti);
		if(confirm(msg))
		 return true;
		else
		 return false;
		}
		return false;
		}

		function imgType(t)
		{
		var imgTypeSrc="";
		var imgTypeAlt="";
					if(t=='C')
					{
						imgTypeSrc = "/quick/i/contact.gif";
						imgTypeAlt ='<%=wrm.GetString("Quicktxt8")%>';
					}
					else if(t=='L')
					{
						imgTypeSrc = "/quick/i/lead.gif";
						imgTypeAlt ='<%=wrm.GetString("Quicktxt9")%>';
					}
					else if(t=='A')
					{
						imgTypeSrc = "/quick/i/act.gif";
						imgTypeAlt ='<%=wrm.GetString("Quicktxt10")%>';
					}
					return "<img src=\""+imgTypeSrc+"\" alt=\""+imgTypeAlt+"\">";
		}

		function openInfo(id)
		{
					if(id.charAt(0)=='C')
					{
						FrameCreateBox('/Common/ViewContact.aspx?render=no&id='+id.substring(1),event,500,300);
					}
					else if(id.charAt(0)=='L')
					{
						FrameCreateBox('/Common/ViewLead.aspx?render=no&id='+id.substring(1),event,500,350);
					}
					else if(id.charAt(0)=='A')
					{
						FrameCreateBox('/Common/ViewCompany.aspx?render=no&id='+id.substring(1),event,500,300);
					}
		}

		var sortColumn = 6;
		function ArrSorter(a,b){ // for strings
		if(a[sortColumn]=="") return -1;
		return a[sortColumn] > b[sortColumn] ? 1 : a[sortColumn] < b[sortColumn] ? -1 : 0;
		}

		function RenderTable(arr, i)
		{
		var output;
		if(i==null){
			i=0;
		}
			output=("<table id=\"TableRendered\" border=\"0\" width=\"100%\" cellspacing=0 cellpadding=0>");
		var cssclass='GridItem';
		arr.sort(ArrSorter);
		for(i=0;i<arr.length;i++)
		{
			if(arr[i].length==1)
			{
				output+=("<tr><td class=\"GridItem\" colspan=5 style=\"color:red\"><b>"+arr[i][0]+"</b></td></tr>");
				break;
			}
			var id = arr[i][0];
			var quickname = arr[i][1];
			var quickcompany = arr[i][2];
			var quickPhone = arr[i][3];
			var quickEmail = arr[i][4];
			var oState = arr[i][5];
			var recall = arr[i][6];
			var actid = arr[i][7];
			var s="";
			if(i%2==0)
				cssclass='GridItemAltern lc'+oState;
			else
				cssclass='GridItem lc'+oState;

            var actype = 0;
            if(recall.length>0){
			    if(recall.substring(0,1)=="E")
			            actype=1;
			        else if(recall.substring(0,1)=="T")
			            actype=3;
			        else if(parseInt(recall)>0)
			            actype=4;
			        else if(parseInt(recall)<0)
			            actype=2;
			}
			s+="<tr status=\""+oState+"\" actype=\""+actype+"\"><td class=\""+cssclass+"\" width=\"80\">";
			s+="<img src=\"i/act.gif\" onmouseover=\"showmenuquick(event,selectActivity,'"+id+"')\" onMouseout=\"dhm()\" class=\"quickimg\">";
			s+="<img src=\"i/cal.gif\" onclick=\"newAppointment('"+id+"');\" class=\"quickimg\">";
			s+="<img src=\"i/mail.gif\" onclick=\"newMail('"+id+"');\" class=\"quickimg\">";

			//s+="<img src=\"i/phngrn.gif\" title='<%=wrm.GetString("Quicktxt1")%>' onclick=\"newActivity('"+id+"','PhoneIn','"+actid+"');\" class=\"quickimg\">";
			if(recall.length>0){
			    if(recall.substring(0,1)=="E")
			            s+="<img src=\"i/phn.gif\" title='<%=wrm.GetString("Quicktxt2")%>' onclick=\"newActivity('"+id+"','PhoneOut','"+actid+"');\" class=\"quickimg\">"
			        else if(recall.substring(0,1)=="T")
				        s+="<img src=\"i/phngld.gif\" title='"+parseTime(recall)+"' onclick=\"if(checkTime('"+recall+"'))newActivity('"+id+"','PhoneOut','"+actid+"');\" class=\"quickimg\">"
			        else if(parseInt(recall)>0)
				        s+="<img src=\"i/phngrn.gif\" title='"+activityFuture.replace(/%s/,recall)+"' onclick=\"newActivity('"+id+"','PhoneOut','"+actid+"');\" class=\"quickimg\">";
			        else if(parseInt(recall)<0)
				        s+="<img src=\"i/phnred.gif\" title='"+activityLost.replace(/%s/,recall.substring(1))+"' onclick=\"newActivity('"+id+"','PhoneOut','"+actid+"');\" class=\"quickimg\">";
			}else
				s+="<img src=\"i/phnblu.gif\" title='<%=wrm.GetString("Quicktxt2")%>' onclick=\"newActivity('"+id+"','PhoneOut','"+actid+"');\" class=\"quickimg\">";
            s+="<img src=\"/i/lens.gif\" onclick=\"openInfo('"+id+"');\" class=\"quickimg\">";
			s+="</td><td class=\""+cssclass+"\" width=\"25%\" title='"+quickname+"'>";
			s+= imgType(id.charAt(0));
			s+="<a href=\"javascript:SelectContact('"+id+"')\">"+quickname+"</a></td>";
			s+="<td class=\""+cssclass+"\" width=\"25%\" title='"+quickcompany+"'>"+quickcompany+"</td>";
			s+="<td class=\""+cssclass+"\" width=\"20%\" title='"+quickPhone+"'>"+quickPhone+"</td>";
			s+="<td class=\""+cssclass+"\" title='"+quickEmail+"'>"+quickEmail+"</td></tr>";
			output+=(s);
			}
		output+=("</table>");
		document.getElementById("content").innerHTML = output;
		}
		</script>

		<form id="Form1" method="post" runat="server">
		<span id="content"></span>
		<asp:Literal ID="QuickRepeater" runat=server/>
		<p align=center>
			<asp:Label ID="LitquickRepeaterInfo" Runat=server cssclass="normal" style="color:red"></asp:Label>
		</p>

		</form>
	</body>
</html>

