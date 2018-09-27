/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function RemoveItem(ListControl,TextControl)
{ objselect = document.getElementById(ListControl); objselect2 = document.getElementById(TextControl); 
if(objselect.selectedIndex>-1){ objselect.options[objselect.selectedIndex]=null; 
var record = '|'; 
for(var x=0;x<objselect.options.length;x++){ record += objselect.options[x].value + '|'; }

if(record.length>1)
objselect2.value = record; else
objselect2.value = ""; 
}
}
