/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Domenica",
"Lunedì",
"Martedì",
"Mercoledì",
"Giovedì",
"Venerdì",
"Sabato",
"Domenica"); 

Calendar._FD = 0; 
Calendar._SDN = new Array
("Dom",
"Lun",
"Mar",
"Mer",
"Gio",
"Ven",
"Sab",
"Dom"); 
Calendar._MN = new Array
("Gennaio",
"Febbraio",
"Marzo",
"Aprile",
"Maggio",
"Giugno",
"Luglio",
"Augosto",
"Settembre",
"Ottobre",
"Novembre",
"Dicembre"); 
Calendar._SMN = new Array
("Gen",
"Feb",
"Mar",
"Apr",
"Mag",
"Giu",
"Lug",
"Ago",
"Set",
"Ott",
"Nov",
"Dic"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Informazioni sul calendario"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Distribuito sotto licenza GNU LGPL.  Vedi http://gnu.org/licenses/lgpl.html per i dettagli." +
"\n\n" +
"Selezione data:\n" +
"- Usa \xab, \xbb per selezionare l'anno\n" +
"- Usa  " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " per i mesi\n" +
"- Tieni premuto a lungo il mouse per accedere alle funzioni di selezione veloce."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Selezione orario:\n" +
"- Clicca sul numero per incrementarlo\n" +
"- o Shift+click per decrementarlo\n" +
"- o click e sinistra o destra per variarlo."; 
Calendar._TT["PREV_YEAR"] = "Anno prec.(tieni premuto per menù)"; Calendar._TT["PREV_MONTH"] = "Mese prec. (tieni premuto per  menù)"; Calendar._TT["GO_TODAY"] = "Oggi"; Calendar._TT["NEXT_MONTH"] = "Mese succ. (tieni premuto per menù)"; Calendar._TT["NEXT_YEAR"] = "Anno succ (tieni premuto per menù)"; Calendar._TT["SEL_DATE"] = "Seleziona data"; Calendar._TT["DRAG_TO_MOVE"] = "Trascina per spostarlo"; Calendar._TT["PART_TODAY"] = " (oggi)"; 
Calendar._TT["DAY_FIRST"] = "Mostra prima %s"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Chiudi"; Calendar._TT["TODAY"] = "Oggi"; Calendar._TT["TIME_PART"] = "(Shift-)Click o trascina per cambiare il valore"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d-%m-%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a %e %b"; 
Calendar._TT["WK"] = "set"; Calendar._TT["TIME"] = "Ora:"; 