/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("ראשון",
"שני",
"שלישי",
"רביעי",
"חמישי",
"שישי",
"שבת",
"ראשון"); 

Calendar._SDN = new Array
("א",
"ב",
"ג",
"ד",
"ה",
"ו",
"ש",
"א"); 
Calendar._MN = new Array
("ינואר",
"פברואר",
"מרץ",
"אפריל",
"מאי",
"יוני",
"יולי",
"אוגוסט",
"ספטמבר",
"אוקטובר",
"נובמבר",
"דצמבר"); 
Calendar._SMN = new Array
("ינא",
"פבר",
"מרץ",
"אפר",
"מאי",
"יונ",
"יול",
"אוג",
"ספט",
"אוק",
"נוב",
"דצמ"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "אודות השנתון"; 
Calendar._TT["ABOUT"] =
"בחרן תאריך/שעה DHTML\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"הגירסא האחרונה זמינה ב: http://www.dynarch.com/projects/calendar/\n" +
"מופץ תחת זיכיון ה GNU LGPL.  עיין ב http://gnu.org/licenses/lgpl.html לפרטים נוספים." +
"\n\n" +
בחירת תאריך:\n" +
"- השתמש בכפתורים \xab, \xbb לבחירת שנה\n" +
"- השתמש בכפתורים " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " לבחירת חודש\n" +
"- החזק העכבר לחוץ מעל הכפתורים המוזכרים לעיל לבחירה מהירה יותר."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"בחירת זמן:\n" +
"- לחץ על כל אחד מחלקי הזמן כדי להוסיף\n" +
"- או shift בשילוב עם לחיצה כדי להחסיר\n" +
"- או לחץ וגרור לפעולה מהירה יותר."; 
Calendar._TT["PREV_YEAR"] = "שנה קודמת - החזק לקבלת תפריט"; Calendar._TT["PREV_MONTH"] = "חודש קודם - החזק לקבלת תפריט"; Calendar._TT["GO_TODAY"] = "עבור להיום"; Calendar._TT["NEXT_MONTH"] = "חודש הבא - החזק לתפריט"; Calendar._TT["NEXT_YEAR"] = "שנה הבאה - החזק לתפריט"; Calendar._TT["SEL_DATE"] = "בחר תאריך"; Calendar._TT["DRAG_TO_MOVE"] = "גרור להזזה"; Calendar._TT["PART_TODAY"] = " )היום("; 
Calendar._TT["DAY_FIRST"] = "הצג %s קודם"; 
Calendar._TT["WEEKEND"] = "6"; 
Calendar._TT["CLOSE"] = "סגור"; Calendar._TT["TODAY"] = "היום"; Calendar._TT["TIME_PART"] = "(שיפט-)לחץ וגרור כדי לשנות ערך"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%Y-%m-%d"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e"; 
Calendar._TT["WK"] = "wk"; Calendar._TT["TIME"] = "שעה::"; 