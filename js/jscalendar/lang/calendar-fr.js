/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */





Calendar._DN = new Array
("Dimanche",
"Lundi",
"Mardi",
"Mercredi",
"Jeudi",
"Vendredi",
"Samedi",
"Dimanche"); 

Calendar._SDN = new Array
("Dim",
"Lun",
"Mar",
"Mar",
"Jeu",
"Ven",
"Sam",
"Dim"); 
Calendar._MN = new Array
("Janvier",
"Fvrier",
"Mars",
"Avril",
"Mai",
"Juin",
"Juillet",
"Aot",
"Septembre",
"Octobre",
"Novembre",
"Dcembre"); 
Calendar._SMN = new Array
("Jan",
"Fev",
"Mar",
"Avr",
"Mai",
"Juin",
"Juil",
"Aout",
"Sep",
"Oct",
"Nov",
"Dec"); 
Calendar._TT = {}; Calendar._TT["INFO"] = "A propos du calendrier"; 
Calendar._TT["ABOUT"] =
"DHTML Date/Heure Selecteur\n" +
"(c) dynarch.com 2002-2005 / Author: Mihai Bazon\n" + // don't translate this this ;-)
"Pour la derniere version visitez : http://www.dynarch.com/projects/calendar/\n" +
"Distribu par GNU LGPL.  Voir http://gnu.org/licenses/lgpl.html pour les details." +
"\n\n" +
"Selection de la date :\n" +
"- Utiliser les bouttons \xab, \xbb  pour selectionner l\'annee\n" +
"- Utiliser les bouttons " + String.fromCharCode(0x2039) + ", " + String.fromCharCode(0x203a) + " pour selectionner les mois\n" +
"- Garder la souris sur n'importe quels boutons pour une selection plus rapide"; Calendar._TT["ABOUT_TIME"] = "\n\n" +
"Selection de l\'heure :\n" +
"- Cliquer sur heures ou minutes pour incrementer\n" +
"- ou Maj-clic pour decrementer\n" +
"- ou clic et glisser-deplacer pour une selection plus rapide"; 
Calendar._TT["PREV_YEAR"] = "Anne prc. (maintenir pour menu)"; Calendar._TT["PREV_MONTH"] = "Mois prc. (maintenir pour menu)"; Calendar._TT["GO_TODAY"] = "Atteindre la date du jour"; Calendar._TT["NEXT_MONTH"] = "Mois suiv. (maintenir pour menu)"; Calendar._TT["NEXT_YEAR"] = "Anne suiv. (maintenir pour menu)"; Calendar._TT["SEL_DATE"] = "Slectionner une date"; Calendar._TT["DRAG_TO_MOVE"] = "Dplacer"; Calendar._TT["PART_TODAY"] = " (Aujourd'hui)"; 
Calendar._TT["DAY_FIRST"] = "Afficher %s en premier"; 
Calendar._TT["WEEKEND"] = "0,6"; 
Calendar._TT["CLOSE"] = "Fermer"; Calendar._TT["TODAY"] = "Aujourd'hui"; Calendar._TT["TIME_PART"] = "(Maj-)Clic ou glisser pour modifier la valeur"; 
Calendar._TT["DEF_DATE_FORMAT"] = "%d/%m/%Y"; Calendar._TT["TT_DATE_FORMAT"] = "%a, %b %e"; 
Calendar._TT["WK"] = "Sem."; Calendar._TT["TIME"] = "Heure :"; 