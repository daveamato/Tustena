/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Diumenge",
"Dilluns",
"Dimarts",
"Dimecres",
"Dijous",
"Divendres",
"Dissabte",
"Diumenge"); 

Calendar._SDN = new Array
("Diu",
"Dil",
"Dmt",
"Dmc",
"Dij",
"Div",
"Dis",
"Diu"); 
Calendar._MN = new Array
("Gener",
"Febrer",
"Mar",
"Abril",
"Maig",
"Juny",
"Juliol",
"Agost",
"Setembre",
"Octubre",
"Novembre",
"Desembre"); 
Calendar._SMN = new Array
("Gen",
"Feb",
"Mar",
"Abr",
"Mai",
"Jun",
"Jul",
"Ago",
"Set",
"Oct",
"Nov",
"Des"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Sobre el calendari"; 
Calendar._TT["ABOUT"] =
"DHTML Selector de Data/Hora\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"For latest version visit: http://www.dynarch.com/projects/calendar/\n" +
"Distributed under GNU LGPL.  See http://gnu.org/licenses/lgpl.html for details." +
"\n\n" +
"Sel.lecci de Dates:\n" +
"- Fes servir els botons \xab, \xbb per sel.leccionar l'any\n" +
"- Fes servir els botons " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " per se.lecciconar el mes\n" +
"- Mant el ratol apretat en qualsevol dels anteriors per sel.lecci rpida."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Time selection:\n" +
"- claca en qualsevol de les parts de la hora per augmentar-les\n" +
"- o Shift-click per decrementar-la\n" +
"- or click and arrastra per sel.lecci rpida."; 
Calendar._TT["PREV_YEAR"] = "Any anterior (Mantenir per menu)"; Calendar._TT["PREV_MONTH"] = "Mes anterior (Mantenir per menu)"; Calendar._TT["GO_TODAY"] = "Anar a avui"; Calendar._TT["NEXT_MONTH"] = "Mes segent (Mantenir per menu)"; Calendar._TT["NEXT_YEAR"] = "Any segent (Mantenir per menu)"; Calendar._TT["SEL_DATE"] = "Sel.leccionar data"; Calendar._TT["DRAG_TO_MOVE"] = "Arrastrar per moure"; Calendar._TT["PART_TODAY"] = " (avui)"; 
Calendar._TT["DAY_FIRST"] = "Mostra %s primer"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Tanca"; Calendar._TT["TODAY"] = "Avui"; Calendar._TT["TIME_PART"] = "(Shift-)Click a arrastra per canviar el valor"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e"; 
Calendar._TT["WK"] = "st"; Calendar._TT["TIME"] = "Hora:"; 