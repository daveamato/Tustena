/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Søndag",
"Mandag",
"Tirsdag",
"Onsdag",
"Torsdag",
"Fredag",
"Lørdag",
"Søndag"); 

Calendar._SDN = new Array
("Søn",
"Man",
"Tir",
"Ons",
"Tor",
"Fre",
"Lør",
"Søn"); 
Calendar._MN = new Array
("Januar",
"Februar",
"Mars",
"April",
"Mai",
"Juni",
"Juli",
"August",
"September",
"Oktober",
"November",
"Desember"); 
Calendar._SMN = new Array
("Jan",
"Feb",
"Mar",
"Apr",
"Mai",
"Jun",
"Jul",
"Aug",
"Sep",
"Okt",
"Nov",
"Des"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Om kalenderen"; 
Calendar._TT["ABOUT"] =
"DHTML Dato-/Tidsvelger\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"For nyeste versjon, gå til: http://www.dynarch.com/projects/calendar/\n" +
"Distribuert under GNU LGPL.  Se http://gnu.org/licenses/lgpl.html for detaljer." +
"\n\n" +
"Datovalg:\n" +
"- Bruk knappene \xab og \xbb for å velge år\n" +
"- Bruk knappene " + String.fromCharCode(0x2039) + " og " + String.fromCharCode(0x203a) + " for å velge måned\n" +
"- Hold inne musknappen eller knappene over for raskere valg."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Tidsvalg:\n" +
"- Klikk på en av tidsdelene for å øke den\n" +
"- eller Shift-klikk for å senke verdien\n" +
"- eller klikk-og-dra for raskere valg.."; 
Calendar._TT["PREV_YEAR"] = "Forrige. år (hold for meny)"; Calendar._TT["PREV_MONTH"] = "Forrige. måned (hold for meny)"; Calendar._TT["GO_TODAY"] = "Gå til idag"; Calendar._TT["NEXT_MONTH"] = "Neste måned (hold for meny)"; Calendar._TT["NEXT_YEAR"] = "Neste år (hold for meny)"; Calendar._TT["SEL_DATE"] = "Velg dato"; Calendar._TT["DRAG_TO_MOVE"] = "Dra for å flytte"; Calendar._TT["PART_TODAY"] = " (idag)"; Calendar._TT["MON_FIRST"] = "Vis mandag først"; Calendar._TT["SUN_FIRST"] = "Vis søndag først"; Calendar._TT["CLOSE"] = "Lukk"; Calendar._TT["TODAY"] = "Idag"; Calendar._TT["TIME_PART"] = "(Shift-)Klikk eller dra for å endre verdi"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d.%m.%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e"; 
Calendar._TT["WK"] = "uke"; 