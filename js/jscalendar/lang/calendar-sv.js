/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("sndag",
"mndag",
"tisdag",
"onsdag",
"torsdag",
"fredag",
"lrdag",
"sndag"); 
Calendar._SDN_len = 2; Calendar._SMN_len = 3; 
Calendar._MN = new Array
("januari",
"februari",
"mars",
"april",
"maj",
"juni",
"juli",
"augusti",
"september",
"oktober",
"november",
"december"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Om kalendern"; 
Calendar._TT["ABOUT"] =
"DHTML Datum/tid-vljare\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Fr senaste version g till: http://www.dynarch.com/projects/calendar/\n" +
"Distribueras under GNU LGPL.  Se http://gnu.org/licenses/lgpl.html fr detaljer." +
"\n\n" +
"Val av datum:\n" +
"- Anvnd knapparna \xab, \xbb fr att vlja r\n" +
"- Anvnd knapparna " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " fr att vlja mnad\n" +
"- Hll musknappen nedtryckt p ngon av ovanstende knappar fr snabbare val."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Val av tid:\n" +
"- Klicka p en del av tiden fr att ka den delen\n" +
"- eller skift-klicka fr att minska den\n" +
"- eller klicka och drag fr snabbare val."; 
Calendar._TT["PREV_YEAR"] = "Fregende r (hll fr menu)"; Calendar._TT["PREV_MONTH"] = "Fregende mnad (hll fr menu)"; Calendar._TT["GO_TODAY"] = "G till dagens datum"; Calendar._TT["NEXT_MONTH"] = "Fljande mnad (hll fr menu)"; Calendar._TT["NEXT_YEAR"] = "Fljande r (hll fr menu)"; Calendar._TT["SEL_DATE"] = "Vlj datum"; Calendar._TT["DRAG_TO_MOVE"] = "Drag fr att flytta"; Calendar._TT["PART_TODAY"] = " (idag)"; Calendar._TT["MON_FIRST"] = "Visa mndag frst"; Calendar._TT["SUN_FIRST"] = "Visa sndag frst"; Calendar._TT["CLOSE"] = "Stng"; Calendar._TT["TODAY"] = "Idag"; Calendar._TT["TIME_PART"] = "(Skift-)klicka eller drag fr att ndra tid"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d"; Calendar._TT["TT_DATE_FORMAT"] = "%A %d %b %Y"; 
Calendar._TT["WK"] = "vecka"; 