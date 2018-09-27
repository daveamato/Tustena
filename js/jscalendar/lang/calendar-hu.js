/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Vasrnap",
"Htf",
"Kedd",
"Szerda",
"Cstrtk",
"Pntek",
"Szombat",
"Vasrnap"); 

Calendar._SDN = new Array
("v",
"h",
"k",
"sze",
"cs",
"p",
"szo",
"v"); 
Calendar._MN = new Array
("janur",
"februr",
"mrcius",
"prilis",
"mjus",
"jnius",
"jlius",
"augusztus",
"szeptember",
"oktber",
"november",
"december"); 
Calendar._SMN = new Array
("jan",
"feb",
"mr",
"pr",
"mj",
"jn",
"jl",
"aug",
"sze",
"okt",
"nov",
"dec"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "A kalendriumrl"; 
Calendar._TT["ABOUT"] =
"DHTML dtum/id kivlaszt\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"a legfrissebb verzi megtallhat: http://www.dynarch.com/projects/calendar/\n" +
"GNU LGPL alatt terjesztve.  Lsd a http://gnu.org/licenses/lgpl.html oldalt a rszletekhez." +
"\n\n" +
"Dtum vlaszts:\n" +
"- hasznlja a \xab, \xbb gombokat az v kivlasztshoz\n" +
"- hasznlja a " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " gombokat a hnap kivlasztshoz\n" +
"- tartsa lenyomva az egrgombot a gyors vlasztshoz."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Id vlaszts:\n" +
"- kattintva nvelheti az idt\n" +
"- shift-tel kattintva cskkentheti\n" +
"- lenyomva tartva s hzva gyorsabban kivlaszthatja."; 
Calendar._TT["PREV_YEAR"] = "Elz v (tartsa nyomva a menhz)"; Calendar._TT["PREV_MONTH"] = "Elz hnap (tartsa nyomva a menhz)"; Calendar._TT["GO_TODAY"] = "Mai napra ugrs"; Calendar._TT["NEXT_MONTH"] = "Kv. hnap (tartsa nyomva a menhz)"; Calendar._TT["NEXT_YEAR"] = "Kv. v (tartsa nyomva a menhz)"; Calendar._TT["SEL_DATE"] = "Vlasszon dtumot"; Calendar._TT["DRAG_TO_MOVE"] = "Hzza a mozgatshoz"; Calendar._TT["PART_TODAY"] = " (ma)"; 
Calendar._TT["DAY_FIRST"] = "%s legyen a ht els napja"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Bezr"; Calendar._TT["TODAY"] = "Ma"; Calendar._TT["TIME_PART"] = "(Shift-)Klikk vagy hzs az rtk vltoztatshoz"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d"; Calendar._TT["TT_DATE_FORMAT"] = "%b %e, %a"; 
Calendar._TT["WK"] = "ht"; Calendar._TT["TIME"] = "id:"; 