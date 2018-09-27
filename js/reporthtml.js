/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

if(!document.all)document.captureEvents(Event.MOUSEDOWN); 
var rowStart = 1; var moz_firstSort = true; 
var col_evenColor = "#FFFFFF"; var col_oddColor = "#E0E0E0"; var col_highlightColor = "#F7C003"; 
var theTable; 
function init() { initRows(); initHeading(); }

function initHeading() { for(i=0;i<theTable.getElementsByTagName("tr")[0].childNodes.length;i++) { if(theTable.getElementsByTagName("tr")[0].childNodes[i].tagName == "TH") { theTable.getElementsByTagName("tr")[0].childNodes[i].id = "col" + i; theTable.getElementsByTagName("tr")[0].childNodes[i].onclick = function(){initSort(this.id)}; theTable.getElementsByTagName("tr")[0].childNodes[i].style.backgroundColor = "#0083C6"; theTable.getElementsByTagName("tr")[0].childNodes[i].style.color = "#FFFFFF"; theTable.getElementsByTagName("tr")[0].childNodes[i].style.cursor = "pointer"; }
}
}

function initSort(column) { handleSort(column.substring(3)); }

function initRows() { theTable = document.getElementById("unique_id"); colorizeTableRows(); for(i=rowStart;i<theTable.getElementsByTagName("tr").length;i++) { theTable.getElementsByTagName("tr")[i].id = "row" + i; theTable.getElementsByTagName("tr")[i].onclick = function(){handleRowClick(this.id)}; }
}

function colorizeTableRows() { for(i=rowStart;i<theTable.getElementsByTagName("tr").length;i++) { i%2?theTable.getElementsByTagName("tr")[i].style.backgroundColor = col_evenColor:theTable.getElementsByTagName("tr")[i].style.backgroundColor = col_oddColor; }
}

function colorizeTableColumn(column) { for(i=rowStart;i<theTable.getElementsByTagName("tr").length;i++) { for(z=0;z<theTable.getElementsByTagName("tr")[i].childNodes.length;z++) { if(theTable.getElementsByTagName("tr")[i].childNodes[z].tagName == "TD")theTable.getElementsByTagName("tr")[i].childNodes[z].style.backgroundColor = ""; }
theTable.getElementsByTagName("tr")[i].childNodes[column].style.backgroundColor = col_highlightColor; }
}

function handleRowClick(row) { highlightTableRow(row); }

function highlightTableRow(row) { colorizeTableRows(); theTable.getElementsByTagName("tr")[row].style.backgroundColor = col_highlightColor; }

function handleSort(column) { sortBy = new Array(); dataID = column; for(i=rowStart;i<theTable.getElementsByTagName("tr").length;i++) { sortBy[sortBy.length] = theTable.getElementsByTagName("tr")[i].childNodes[dataID].innerHTML; }
if(rowStart) { resetHeaderColors(); theTable.getElementsByTagName("tr")[0].childNodes[dataID].style.backgroundColor = col_highlightColor; }
sortRowData(dataID); }

function resetHeaderColors() { for(i=0;i<theTable.getElementsByTagName("tr")[0].childNodes.length;i++) { if(theTable.getElementsByTagName("tr")[0].childNodes[i].tagName == "TH")theTable.getElementsByTagName("tr")[0].childNodes[i].style.backgroundColor = "#0083C6"; }
}

function sortRowData(dataID) { order = new Array(); colorizeTableColumn(dataID); for(i=rowStart;i<theTable.getElementsByTagName("tr").length;i++) order[order.length] = new Array(theTable.getElementsByTagName("tr")[i].childNodes[dataID].innerHTML,i); order=order.sort(byName); reorderTable(order); }

function byName(a, b) { var anew = a[0].toLowerCase(); var bnew = b[0].toLowerCase(); if (anew < bnew) return -1; if (anew > bnew) return 1; return 0; }


function reorderTable(order) { mDiv = new Array(); for(i=0;i<order.length;i++) mDiv[mDiv.length] = theTable.getElementsByTagName("tr")[order[i][1]].cloneNode(true); z=0; for(i=rowStart;i<theTable.getElementsByTagName("tbody")[0].childNodes.length;i++) { if(theTable.getElementsByTagName("tbody")[0].childNodes[i].tagName == "TR") { try { if(document.all) { theTable.getElementsByTagName("tbody")[0].insertBefore(mDiv[z],theTable.getElementsByTagName("tr")[i]); } else { theTable.getElementsByTagName("tbody")[0].insertBefore(mDiv[z],theTable.getElementsByTagName("tr")[document.getElementsByTagName("tr").length]); }
} catch(err) { }
z++; }
}
removeChildren(mDiv.length+1); initRows(); }

function removeChildren(startFrom) { err =""; if(document.all) { do { try { theTable.getElementsByTagName("tbody")[0].removeChild(theTable.getElementsByTagName("tr")[startFrom]) } catch (err) { } } while (err == ""); } else { z=0; for(i=1;z<startFrom-1;i++) { if(theTable.getElementsByTagName("tbody")[0].childNodes[i].tagName == "TR") { if(moz_firstSort) { theTable.getElementsByTagName("tbody")[0].removeChild(theTable.getElementsByTagName("tbody")[0].childNodes[i]); } else { theTable.getElementsByTagName("tbody")[0].removeChild(theTable.getElementsByTagName("tbody")[0].childNodes[startFrom+1]); }
z++; }
}
}
if(moz_firstSort)moz_firstSort = false; }

function makeNegative() { TDs = theTable.getElementsByTagName('td'); for (var i=0; i<TDs.length; i++) { var temp = TDs[i]; if (temp.firstChild.nodeValue.indexOf('-') == 0) temp.className = "negative"; }
}

init(); 