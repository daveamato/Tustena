/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */



Calendar._DN = new Array
("NedeÄľa",
"Pondelok",
"Utorok",
"Streda",
"Ĺ tvrtok",
"Piatok",
"Sobota",
"NedeÄľa"); 
Calendar._SDN = new Array
("Ned",
"Pon",
"Uto",
"Str",
"Ĺ tv",
"Pia",
"Sob",
"Ned"); 
Calendar._MN = new Array
("JanuĂˇr",
"FebruĂˇr",
"Marec",
"AprĂ­l",
"MĂˇj",
"JĂşn",
"JĂşl",
"August",
"September",
"OktĂłber",
"November",
"December"); 
Calendar._SMN = new Array
("Jan",
"Feb",
"Mar",
"Apr",
"MĂˇj",
"JĂşn",
"JĂşl",
"Aug",
"Sep",
"Okt",
"Nov",
"Dec"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "O kalendĂˇri"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" +
"PoslednĂş verziu nĂˇjdete na: http://www.dynarch.com/projects/calendar/\n" +
"DistribuovanĂ© pod GNU LGPL.  ViÄŹ http://gnu.org/licenses/lgpl.html pre detaily." +
"\n\n" +
"VĂ˝ber dĂˇtumu:\n" +
"- PouĹľite tlaÄŤidlĂˇ \xab, \xbb pre vĂ˝ber roku\n" +
"- PouĹľite tlaÄŤidlĂˇ " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " pre vĂ˝ber mesiaca\n" +
"- Ak ktorĂ©koÄľvek z tĂ˝chto tlaÄŤidiel podrĹľĂ­te dlhĹˇie, zobrazĂ­ sa rĂ˝chly vĂ˝ber."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"VĂ˝ber ÄŤasu:\n" +
"- Kliknutie na niektorĂş poloĹľku ÄŤasu ju zvĂ˝Ĺˇi\n" +
"- Shift-klik ju znĂ­Ĺľi\n" +
"- Ak podrĹľĂ­te tlaÄŤĂ­tko stlaÄŤenĂ©, posĂşvanĂ­m menĂ­te hodnotu."; 
Calendar._TT["PREV_YEAR"] = "PredoĹˇlĂ˝ rok (podrĹľte pre menu)"; Calendar._TT["PREV_MONTH"] = "PredoĹˇlĂ˝ mesiac (podrĹľte pre menu)"; Calendar._TT["GO_TODAY"] = "PrejsĹĄ na dneĹˇok"; Calendar._TT["NEXT_MONTH"] = "Nasl. mesiac (podrĹľte pre menu)"; Calendar._TT["NEXT_YEAR"] = "Nasl. rok (podrĹľte pre menu)"; Calendar._TT["SEL_DATE"] = "ZvoÄľte dĂˇtum"; Calendar._TT["DRAG_TO_MOVE"] = "PodrĹľanĂ­m tlaÄŤĂ­tka zmenĂ­te polohu"; Calendar._TT["PART_TODAY"] = " (dnes)"; Calendar._TT["MON_FIRST"] = "ZobraziĹĄ pondelok ako prvĂ˝"; Calendar._TT["SUN_FIRST"] = "ZobraziĹĄ nedeÄľu ako prvĂş"; Calendar._TT["CLOSE"] = "ZavrieĹĄ"; Calendar._TT["TODAY"] = "Dnes"; Calendar._TT["TIME_PART"] = "(Shift-)klik/ĹĄahanie zmenĂ­ hodnotu"; 
Calendar._TT["DEF_DATE_FORMAT"] = "$d. %m. %Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %e. %b"; 
Calendar._TT["WK"] = "tĂ˝Ĺľ"; 