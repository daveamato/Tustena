/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Domingo",
"Lunes",
"Martes",
"Mircoles",
"Jueves",
"Viernes",
"Sbado",
"Domingo"); 

Calendar._SDN = new Array
("Dom",
"Lun",
"Mar",
"Mi",
"Jue",
"Vie",
"Sb",
"Dom"); 
Calendar._FD = 1; 
Calendar._MN = new Array
("Enero",
"Febrero",
"Marzo",
"Abril",
"Mayo",
"Junio",
"Julio",
"Agosto",
"Septiembre",
"Octubre",
"Noviembre",
"Diciembre"); 
Calendar._SMN = new Array
("Ene",
"Feb",
"Mar",
"Abr",
"May",
"Jun",
"Jul",
"Ago",
"Sep",
"Oct",
"Nov",
"Dic"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Acerca del calendario"; 
Calendar._TT["ABOUT"] =
"Selector DHTML de Fecha/Hora\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Para conseguir la ltima versin visite: http://www.dynarch.com/projects/calendar/\n" +
"Distribuido bajo licencia GNU LGPL. Visite http://gnu.org/licenses/lgpl.html para ms detalles." +
"\n\n" +
"Seleccin de fecha:\n" +
"- Use los botones \xab, \xbb para seleccionar el ao\n" +
"- Use los botones " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " para seleccionar el mes\n" +
"- Mantenga pulsado el ratn en cualquiera de estos botones para una seleccin rpida."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Seleccin de hora:\n" +
"- Pulse en cualquiera de las partes de la hora para incrementarla\n" +
"- o pulse las maysculas mientras hace clic para decrementarla\n" +
"- o haga clic y arrastre el ratn para una seleccin ms rpida."; 
Calendar._TT["PREV_YEAR"] = "Ao anterior (mantener para men)"; Calendar._TT["PREV_MONTH"] = "Mes anterior (mantener para men)"; Calendar._TT["GO_TODAY"] = "Ir a hoy"; Calendar._TT["NEXT_MONTH"] = "Mes siguiente (mantener para men)"; Calendar._TT["NEXT_YEAR"] = "Ao siguiente (mantener para men)"; Calendar._TT["SEL_DATE"] = "Seleccionar fecha"; Calendar._TT["DRAG_TO_MOVE"] = "Arrastrar para mover"; Calendar._TT["PART_TODAY"] = " (hoy)"; 
Calendar._TT["DAY_FIRST"] = "Hacer %s primer da de la semana"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Cerrar"; Calendar._TT["TODAY"] = "Hoy"; Calendar._TT["TIME_PART"] = "(Mayscula-)Clic o arrastre para cambiar valor"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d/%m/%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%A, %e de %B de %Y"; 
Calendar._TT["WK"] = "sem"; Calendar._TT["TIME"] = "Hora:"; 