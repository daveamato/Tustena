/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */




Calendar._DN = new Array
("Domingo",
"Segunda",
"Terca",
"Quarta",
"Quinta",
"Sexta",
"Sabado",
"Domingo"); 

Calendar._SDN = new Array
("Dom",
"Seg",
"Ter",
"Qua",
"Qui",
"Sex",
"Sab",
"Dom"); 
Calendar._MN = new Array
("Janeiro",
"Fevereiro",
"Marco",
"Abril",
"Maio",
"Junho",
"Julho",
"Agosto",
"Setembro",
"Outubro",
"Novembro",
"Dezembro"); 
Calendar._SMN = new Array
("Jan",
"Fev",
"Mar",
"Abr",
"Mai",
"Jun",
"Jul",
"Ago",
"Set",
"Out",
"Nov",
"Dez"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "Sobre o calendario"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Time Selector\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Ultima versao visite: http://www.dynarch.com/projects/calendar/\n" +
"Distribuido sobre GNU LGPL.  Veja http://gnu.org/licenses/lgpl.html para detalhes." +
"\n\n" +
"Selecao de data:\n" +
"- Use os botoes \xab, \xbb para selecionar o ano\n" +
"- Use os botoes " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " para selecionar o mes\n" +
"- Segure o botao do mouse em qualquer um desses botoes para selecao rapida."; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Selecao de hora:\n" +
"- Clique em qualquer parte da hora para incrementar\n" +
"- ou Shift-click para decrementar\n" +
"- ou clique e segure para selecao rapida."; 
Calendar._TT["PREV_YEAR"] = "Ant. ano (segure para menu)"; Calendar._TT["PREV_MONTH"] = "Ant. mes (segure para menu)"; Calendar._TT["GO_TODAY"] = "Hoje"; Calendar._TT["NEXT_MONTH"] = "Prox. mes (segure para menu)"; Calendar._TT["NEXT_YEAR"] = "Prox. ano (segure para menu)"; Calendar._TT["SEL_DATE"] = "Selecione a data"; Calendar._TT["DRAG_TO_MOVE"] = "Arraste para mover"; Calendar._TT["PART_TODAY"] = " (hoje)"; 
Calendar._TT["DAY_FIRST"] = "Mostre %s primeiro"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Fechar"; Calendar._TT["TODAY"] = "Hoje"; Calendar._TT["TIME_PART"] = "(Shift-)Click ou arraste para mudar valor"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d/%m/%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %e %b"; 
Calendar._TT["WK"] = "sm"; Calendar._TT["TIME"] = "Hora:"; 