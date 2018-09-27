/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Svtdiena",
"Pirmdiena",
"Otrdiena",
"Trediena",
"Ceturdiena",
"Piektdiena",
"Sestdiena",
"Svtdiena"); 

Calendar._SDN = new Array
("Sv",
"Pr",
"Ot",
"Tr",
"Ce",
"Pk",
"Se",
"Sv"); 
Calendar._MN = new Array
("Janvris",
"Februris",
"Marts",
"Aprlis",
"Maijs",
"Jnijs",
"Jlijs",
"Augusts",
"Septembris",
"Oktobris",
"Novembris",
"Decembris"); 
Calendar._SMN = new Array
("Jan",
"Feb",
"Mar",
"Apr",
"Mai",
"Jn",
"Jl",
"Aug",
"Sep",
"Okt",
"Nov",
"Dec"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Par kalendru"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"For latest version visit: http://www.dynarch.com/projects/calendar/\n" +
"Distributed under GNU LGPL.  See http://gnu.org/licenses/lgpl.html for details." +
"\n\n" +
"Datuma izvle:\n" +
"- Izmanto \xab, \xbb pogas, lai izvltos gadu\n" +
"- Izmanto " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + "pogas, lai izvltos mnesi\n" +
"- Turi nospiestu peles pogu uz jebkuru no augstk mintajm pogm, lai patrintu izvli."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Laika izvle:\n" +
"- Uzklikini uz jebkuru no laika dam, lai palielintu to\n" +
"- vai Shift-klikis, lai samazintu to\n" +
"- vai noklikini un velc uz attiecgo virzienu lai maintu trk."; 
Calendar._TT["PREV_YEAR"] = "Iepr. gads (turi izvlnei)"; Calendar._TT["PREV_MONTH"] = "Iepr. mnesis (turi izvlnei)"; Calendar._TT["GO_TODAY"] = "odien"; Calendar._TT["NEXT_MONTH"] = "Nkoais mnesis (turi izvlnei)"; Calendar._TT["NEXT_YEAR"] = "Nkoais gads (turi izvlnei)"; Calendar._TT["SEL_DATE"] = "Izvlies datumu"; Calendar._TT["DRAG_TO_MOVE"] = "Velc, lai prvietotu"; Calendar._TT["PART_TODAY"] = " (odien)"; 
Calendar._TT["DAY_FIRST"] = "Attlot %s k pirmo"; 
Calendar._TT["WEEKEND"] = "1,7"; 
Calendar._TT["CLOSE"] = "Aizvrt"; Calendar._TT["TODAY"] = "odien"; Calendar._TT["TIME_PART"] = "(Shift-)Klikis vai prvieto, lai maintu"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d-%m-%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %e %b"; 
Calendar._TT["WK"] = "wk"; Calendar._TT["TIME"] = "Laiks:"; 