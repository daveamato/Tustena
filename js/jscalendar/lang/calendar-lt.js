/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Sekmadienis",
"Pirmadienis",
"Antradienis",
"Treiadienis",
"Ketvirtadienis",
"Pentadienis",
"etadienis",
"Sekmadienis"); 

Calendar._SDN = new Array
("Sek",
"Pir",
"Ant",
"Tre",
"Ket",
"Pen",
"e",
"Sek"); 
Calendar._MN = new Array
("Sausis",
"Vasaris",
"Kovas",
"Balandis",
"Gegu",
"Birelis",
"Liepa",
"Rugpjtis",
"Rugsjis",
"Spalis",
"Lapkritis",
"Gruodis"); 
Calendar._SMN = new Array
("Sau",
"Vas",
"Kov",
"Bal",
"Geg",
"Bir",
"Lie",
"Rgp",
"Rgs",
"Spa",
"Lap",
"Gru"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Apie kalendori"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Naujausi versij rasite: http://www.dynarch.com/projects/calendar/\n" +
"Platinamas pagal GNU LGPL licencij. Aplankykite http://gnu.org/licenses/lgpl.html" +
"\n\n" +
"Datos pasirinkimas:\n" +
"- Met pasirinkimas: \xab, \xbb\n" +
"- Mnesio pasirinkimas: " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + "\n" +
"- Nuspauskite ir laikykite pels klavi greitesniam pasirinkimui."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Laiko pasirinkimas:\n" +
"- Spustelkite ant valand arba minui - skaius padids vienetu.\n" +
"- Jei spausite kartu su Shift, skaiius sumas.\n" +
"- Greitam pasirinkimui spustelkite ir pajudinkite pel."; 
Calendar._TT["PREV_YEAR"] = "Ankstesni metai (laikykite, jei norite meniu)"; Calendar._TT["PREV_MONTH"] = "Ankstesnis mnuo (laikykite, jei norite meniu)"; Calendar._TT["GO_TODAY"] = "Pasirinkti iandien"; Calendar._TT["NEXT_MONTH"] = "Kitas mnuo (laikykite, jei norite meniu)"; Calendar._TT["NEXT_YEAR"] = "Kiti metai (laikykite, jei norite meniu)"; Calendar._TT["SEL_DATE"] = "Pasirinkite dat"; Calendar._TT["DRAG_TO_MOVE"] = "Tempkite"; Calendar._TT["PART_TODAY"] = " (iandien)"; Calendar._TT["MON_FIRST"] = "Pirma savaits diena - pirmadienis"; Calendar._TT["SUN_FIRST"] = "Pirma savaits diena - sekmadienis"; Calendar._TT["CLOSE"] = "Udaryti"; Calendar._TT["TODAY"] = "iandien"; Calendar._TT["TIME_PART"] = "Spustelkite arba tempkite jei norite pakeisti"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d"; Calendar._TT["TT_DATE_FORMAT"] = "%A, %Y-%m-%d"; 
Calendar._TT["WK"] = "sav"; 