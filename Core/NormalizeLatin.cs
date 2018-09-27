/// <license>TUSTENA PUBLIC LICENSE v1.0</license>
/// <copyright>
/// Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
///
/// Tustena CRM is a trademark of:
/// Digita S.r.l.
/// Viale Enrico Fermi 14/z
/// 31011 Asolo (Italy)
/// Tel. +39-0423-951251
/// Mail. info@digita.it
///
/// This file contains Original Code and/or Modifications of Original Code
/// as defined in and that are subject to the Tustena Public Source License
/// Version 1.0 (the 'License'). You may not use this file except in
/// compliance with the License. Please obtain a copy of the License at
/// http://www.tustena.com/TPL/ and read it before using this
// file.
///
/// The Original Code and all software distributed under the License are
/// distributed on an 'AS IS' basis, WITHOUT WARRANTY OF ANY KIND, EITHER
/// EXPRESS OR IMPLIED, AND DIGITA S.R.L. HEREBY DISCLAIMS ALL SUCH WARRANTIES,
/// INCLUDING WITHOUT LIMITATION, ANY WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE, QUIET ENJOYMENT OR NON-INFRINGEMENT.
/// Please see the License for the specific language governing rights and
/// limitations under the License.
///
/// YOU MAY NOT REMOVE OR ALTER THIS COPYRIGHT NOTICE!
/// </copyright>

using System;
using System.Collections;
using System.Text;
using System.Web;

namespace Digita.Tustena.Core
{


	public class Normalize
	{
		private static Hashtable conversionTable = null;

		public static string NormalizeLatin(string latinStr)
		{
			string retStr = string.Empty;
			char c;

			if (conversionTable == null)
				InitConversionTable();

			for (int i = 0; i < latinStr.Length; i++)
			{
				c = (char)latinStr[i];
				if (conversionTable.Contains(c))
					retStr += conversionTable[c];
				else
					retStr += c;
			}
			return retStr;

		}


		private static Hashtable InitConversionTable()
		{
			if(conversionTable == null)
			{
				NormalizationTable();
			}
				return conversionTable;
		}

		private static void NormalizationTable()
		{
			conversionTable = new Hashtable();
			conversionTable.Add((char)0x00AA, "a");    // FEMININE ORDINAL INDICATOR
			conversionTable.Add((char)0x00BA, "o");	// MASCULINE ORDINAL INDICATOR
			conversionTable.Add((char)0x00C0, "A");	// LATIN CAPITAL LETTER A WITH GRAVE
			conversionTable.Add((char)0x00C1, "A");	// LATIN CAPITAL LETTER A WITH ACUTE
			conversionTable.Add((char)0x00C2, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX
			conversionTable.Add((char)0x00C3, "A");	// LATIN CAPITAL LETTER A WITH TILDE
			conversionTable.Add((char)0x00C4, "A");	// LATIN CAPITAL LETTER A WITH DIAERESIS
			conversionTable.Add((char)0x00C5, "A");	// LATIN CAPITAL LETTER A WITH RING ABOVE
			conversionTable.Add((char)0x00C6, "AE");	// LATIN CAPITAL LETTER AE -- no decomposition
			conversionTable.Add((char)0x00C7, "C");	// LATIN CAPITAL LETTER C WITH CEDILLA
			conversionTable.Add((char)0x00C8, "E");	// LATIN CAPITAL LETTER E WITH GRAVE
			conversionTable.Add((char)0x00C9, "E");	// LATIN CAPITAL LETTER E WITH ACUTE
			conversionTable.Add((char)0x00CA, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX
			conversionTable.Add((char)0x00CB, "E");	// LATIN CAPITAL LETTER E WITH DIAERESIS
			conversionTable.Add((char)0x00CC, "I");	// LATIN CAPITAL LETTER I WITH GRAVE
			conversionTable.Add((char)0x00CD, "I");	// LATIN CAPITAL LETTER I WITH ACUTE
			conversionTable.Add((char)0x00CE, "I");	// LATIN CAPITAL LETTER I WITH CIRCUMFLEX
			conversionTable.Add((char)0x00CF, "I");	// LATIN CAPITAL LETTER I WITH DIAERESIS
			conversionTable.Add((char)0x00D0, "D");	// LATIN CAPITAL LETTER ETH -- no decomposition  	// Eth [D for Vietnamese]
			conversionTable.Add((char)0x00D1, "N");	// LATIN CAPITAL LETTER N WITH TILDE
			conversionTable.Add((char)0x00D2, "O");	// LATIN CAPITAL LETTER O WITH GRAVE
			conversionTable.Add((char)0x00D3, "O");	// LATIN CAPITAL LETTER O WITH ACUTE
			conversionTable.Add((char)0x00D4, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX
			conversionTable.Add((char)0x00D5, "O");	// LATIN CAPITAL LETTER O WITH TILDE
			conversionTable.Add((char)0x00D6, "O");	// LATIN CAPITAL LETTER O WITH DIAERESIS
			conversionTable.Add((char)0x00D8, "O");	// LATIN CAPITAL LETTER O WITH STROKE -- no decom
			conversionTable.Add((char)0x00D9, "U");	// LATIN CAPITAL LETTER U WITH GRAVE
			conversionTable.Add((char)0x00DA, "U");	// LATIN CAPITAL LETTER U WITH ACUTE
			conversionTable.Add((char)0x00DB, "U");	// LATIN CAPITAL LETTER U WITH CIRCUMFLEX
			conversionTable.Add((char)0x00DC, "U");	//LATIN CAPITAL LETTER U WITH DIAERESIS
			conversionTable.Add((char)0x00DD, "Y");	// LATIN CAPITAL LETTER Y WITH ACUTE
			conversionTable.Add((char)0x00DE, "Th");	// LATIN CAPITAL LETTER THORN -- no decomposition; // Thorn - Could be nothing other than thorn
			conversionTable.Add((char)0x00DF, "ss"); // [s] LATIN SMALL LETTER SHARP S -- no decomposition
			conversionTable.Add((char)0x00E0, "a");	// LATIN SMALL LETTER A WITH GRAVE
			conversionTable.Add((char)0x00E1, "a");	// LATIN SMALL LETTER A WITH ACUTE
			conversionTable.Add((char)0x00E2, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX
			conversionTable.Add((char)0x00E3, "a");	// LATIN SMALL LETTER A WITH TILDE
			conversionTable.Add((char)0x00E4, "a");	// LATIN SMALL LETTER A WITH DIAERESIS
			conversionTable.Add((char)0x00E5, "a");	// LATIN SMALL LETTER A WITH RING ABOVE
			conversionTable.Add((char)0x00E6, "ae");	// LATIN SMALL LETTER AE -- no decomposition     ;
			conversionTable.Add((char)0x00E7, "c");	// LATIN SMALL LETTER C WITH CEDILLA
			conversionTable.Add((char)0x00E8, "e");	// LATIN SMALL LETTER E WITH GRAVE
			conversionTable.Add((char)0x00E9, "e");	// LATIN SMALL LETTER E WITH ACUTE
			conversionTable.Add((char)0x00EA, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX
			conversionTable.Add((char)0x00EB, "e");	// LATIN SMALL LETTER E WITH DIAERESIS
			conversionTable.Add((char)0x00EC, "i");	// LATIN SMALL LETTER I WITH GRAVE
			conversionTable.Add((char)0x00ED, "i");	// LATIN SMALL LETTER I WITH ACUTE
			conversionTable.Add((char)0x00EE, "i");	// LATIN SMALL LETTER I WITH CIRCUMFLEX
			conversionTable.Add((char)0x00EF, "i");	// LATIN SMALL LETTER I WITH DIAERESIS
			conversionTable.Add((char)0x00F0, "d");	// LATIN SMALL LETTER ETH -- no decomposition         // small eth, "d" for benefit of Vietnamese
			conversionTable.Add((char)0x00F1, "n");	// LATIN SMALL LETTER N WITH TILDE
			conversionTable.Add((char)0x00F2, "o");	// LATIN SMALL LETTER O WITH GRAVE
			conversionTable.Add((char)0x00F3, "o");	// LATIN SMALL LETTER O WITH ACUTE
			conversionTable.Add((char)0x00F4, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX
			conversionTable.Add((char)0x00F5, "o");	// LATIN SMALL LETTER O WITH TILDE
			conversionTable.Add((char)0x00F6, "o");	// LATIN SMALL LETTER O WITH DIAERESIS
			conversionTable.Add((char)0x00F8, "o");	// LATIN SMALL LETTER O WITH STROKE -- no decompo
			conversionTable.Add((char)0x00F9, "u");	// LATIN SMALL LETTER U WITH GRAVE
			conversionTable.Add((char)0x00FA, "u");	// LATIN SMALL LETTER U WITH ACUTE
			conversionTable.Add((char)0x00FB, "u");	// LATIN SMALL LETTER U WITH CIRCUMFLEX
			conversionTable.Add((char)0x00FC, "u");	// LATIN SMALL LETTER U WITH DIAERESIS
			conversionTable.Add((char)0x00FD, "y");	// LATIN SMALL LETTER Y WITH ACUTE
			conversionTable.Add((char)0x00FE, "th");	// LATIN SMALL LETTER THORN -- no decomposition  ;   // Small thorn
			conversionTable.Add((char)0x00FF, "y");	// LATIN SMALL LETTER Y WITH DIAERESIS
			conversionTable.Add((char)0x0100, "A");	// LATIN CAPITAL LETTER A WITH MACRON
			conversionTable.Add((char)0x0101, "a");	// LATIN SMALL LETTER A WITH MACRON
			conversionTable.Add((char)0x0102, "A");	// LATIN CAPITAL LETTER A WITH BREVE
			conversionTable.Add((char)0x0103, "a");	// LATIN SMALL LETTER A WITH BREVE
			conversionTable.Add((char)0x0104, "A");	// LATIN CAPITAL LETTER A WITH OGONEK
			conversionTable.Add((char)0x0105, "a");	// LATIN SMALL LETTER A WITH OGONEK
			conversionTable.Add((char)0x0106, "C");	// LATIN CAPITAL LETTER C WITH ACUTE
			conversionTable.Add((char)0x0107, "c");	// LATIN SMALL LETTER C WITH ACUTE
			conversionTable.Add((char)0x0108, "C");	// LATIN CAPITAL LETTER C WITH CIRCUMFLEX
			conversionTable.Add((char)0x0109, "c");	// LATIN SMALL LETTER C WITH CIRCUMFLEX
			conversionTable.Add((char)0x010A, "C");	// LATIN CAPITAL LETTER C WITH DOT ABOVE
			conversionTable.Add((char)0x010B, "c");	// LATIN SMALL LETTER C WITH DOT ABOVE
			conversionTable.Add((char)0x010C, "C");	// LATIN CAPITAL LETTER C WITH CARON
			conversionTable.Add((char)0x010D, "c");	// LATIN SMALL LETTER C WITH CARON
			conversionTable.Add((char)0x010E, "D");	// LATIN CAPITAL LETTER D WITH CARON
			conversionTable.Add((char)0x010F, "d");	// LATIN SMALL LETTER D WITH CARON
			conversionTable.Add((char)0x0110, "D");	// LATIN CAPITAL LETTER D WITH STROKE -- no decomposition                     // Capital D with stroke
			conversionTable.Add((char)0x0111, "d");	// LATIN SMALL LETTER D WITH STROKE -- no decomposition                       // small D with stroke
			conversionTable.Add((char)0x0112, "E");	// LATIN CAPITAL LETTER E WITH MACRON
			conversionTable.Add((char)0x0113, "e");	// LATIN SMALL LETTER E WITH MACRON
			conversionTable.Add((char)0x0114, "E");	// LATIN CAPITAL LETTER E WITH BREVE
			conversionTable.Add((char)0x0115, "e");	// LATIN SMALL LETTER E WITH BREVE
			conversionTable.Add((char)0x0116, "E");	// LATIN CAPITAL LETTER E WITH DOT ABOVE
			conversionTable.Add((char)0x0117, "e");	// LATIN SMALL LETTER E WITH DOT ABOVE
			conversionTable.Add((char)0x0118, "E");	// LATIN CAPITAL LETTER E WITH OGONEK
			conversionTable.Add((char)0x0119, "e");	// LATIN SMALL LETTER E WITH OGONEK
			conversionTable.Add((char)0x011A, "E");	// LATIN CAPITAL LETTER E WITH CARON
			conversionTable.Add((char)0x011B, "e");	// LATIN SMALL LETTER E WITH CARON
			conversionTable.Add((char)0x011C, "G");	// LATIN CAPITAL LETTER G WITH CIRCUMFLEX
			conversionTable.Add((char)0x011D, "g");	// LATIN SMALL LETTER G WITH CIRCUMFLEX
			conversionTable.Add((char)0x011E, "G");	// LATIN CAPITAL LETTER G WITH BREVE
			conversionTable.Add((char)0x011F, "g");	// LATIN SMALL LETTER G WITH BREVE
			conversionTable.Add((char)0x0120, "G");	// LATIN CAPITAL LETTER G WITH DOT ABOVE
			conversionTable.Add((char)0x0121, "g");	// LATIN SMALL LETTER G WITH DOT ABOVE
			conversionTable.Add((char)0x0122, "G");	// LATIN CAPITAL LETTER G WITH CEDILLA
			conversionTable.Add((char)0x0123, "g");	// LATIN SMALL LETTER G WITH CEDILLA
			conversionTable.Add((char)0x0124, "H");	// LATIN CAPITAL LETTER H WITH CIRCUMFLEX
			conversionTable.Add((char)0x0125, "h");	// LATIN SMALL LETTER H WITH CIRCUMFLEX
			conversionTable.Add((char)0x0126, "H");	// LATIN CAPITAL LETTER H WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0127, "h");	// LATIN SMALL LETTER H WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0128, "I");	// LATIN CAPITAL LETTER I WITH TILDE
			conversionTable.Add((char)0x0129, "i");	// LATIN SMALL LETTER I WITH TILDE
			conversionTable.Add((char)0x012A, "I");	// LATIN CAPITAL LETTER I WITH MACRON
			conversionTable.Add((char)0x012B, "i");	// LATIN SMALL LETTER I WITH MACRON
			conversionTable.Add((char)0x012C, "I");	// LATIN CAPITAL LETTER I WITH BREVE
			conversionTable.Add((char)0x012D, "i");	// LATIN SMALL LETTER I WITH BREVE
			conversionTable.Add((char)0x012E, "I");	// LATIN CAPITAL LETTER I WITH OGONEK
			conversionTable.Add((char)0x012F, "i");	// LATIN SMALL LETTER I WITH OGONEK
			conversionTable.Add((char)0x0130, "I");	// LATIN CAPITAL LETTER I WITH DOT ABOVE
			conversionTable.Add((char)0x0131, "i");	// LATIN SMALL LETTER DOTLESS I -- no decomposition
			conversionTable.Add((char)0x0132, "I");	// LATIN CAPITAL LIGATURE IJ
			conversionTable.Add((char)0x0133, "i");	// LATIN SMALL LIGATURE IJ
			conversionTable.Add((char)0x0134, "J");	// LATIN CAPITAL LETTER J WITH CIRCUMFLEX
			conversionTable.Add((char)0x0135, "j");	// LATIN SMALL LETTER J WITH CIRCUMFLEX
			conversionTable.Add((char)0x0136, "K");	// LATIN CAPITAL LETTER K WITH CEDILLA
			conversionTable.Add((char)0x0137, "k");	// LATIN SMALL LETTER K WITH CEDILLA
			conversionTable.Add((char)0x0138, "k");	// LATIN SMALL LETTER KRA -- no decomposition
			conversionTable.Add((char)0x0139, "L");	// LATIN CAPITAL LETTER L WITH ACUTE
			conversionTable.Add((char)0x013A, "l");	// LATIN SMALL LETTER L WITH ACUTE
			conversionTable.Add((char)0x013B, "L");	// LATIN CAPITAL LETTER L WITH CEDILLA
			conversionTable.Add((char)0x013C, "l");	// LATIN SMALL LETTER L WITH CEDILLA
			conversionTable.Add((char)0x013D, "L");	// LATIN CAPITAL LETTER L WITH CARON
			conversionTable.Add((char)0x013E, "l");	// LATIN SMALL LETTER L WITH CARON
			conversionTable.Add((char)0x013F, "L");	// LATIN CAPITAL LETTER L WITH MIDDLE DOT
			conversionTable.Add((char)0x0140, "l");	// LATIN SMALL LETTER L WITH MIDDLE DOT
			conversionTable.Add((char)0x0141, "L");	// LATIN CAPITAL LETTER L WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0142, "l");	// LATIN SMALL LETTER L WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0143, "N");	// LATIN CAPITAL LETTER N WITH ACUTE
			conversionTable.Add((char)0x0144, "n");	// LATIN SMALL LETTER N WITH ACUTE
			conversionTable.Add((char)0x0145, "N");	// LATIN CAPITAL LETTER N WITH CEDILLA
			conversionTable.Add((char)0x0146, "n");	// LATIN SMALL LETTER N WITH CEDILLA
			conversionTable.Add((char)0x0147, "N");	// LATIN CAPITAL LETTER N WITH CARON
			conversionTable.Add((char)0x0148, "n");	// LATIN SMALL LETTER N WITH CARON
			conversionTable.Add((char)0x0149, "'n");	// LATIN SMALL LETTER N PRECEDED BY APOSTROPHE                              ;
			conversionTable.Add((char)0x014A, "NG");	// LATIN CAPITAL LETTER ENG -- no decomposition                             ;
			conversionTable.Add((char)0x014B, "ng");	// LATIN SMALL LETTER ENG -- no decomposition                               ;
			conversionTable.Add((char)0x014C, "O");	// LATIN CAPITAL LETTER O WITH MACRON
			conversionTable.Add((char)0x014D, "o");	// LATIN SMALL LETTER O WITH MACRON
			conversionTable.Add((char)0x014E, "O");	// LATIN CAPITAL LETTER O WITH BREVE
			conversionTable.Add((char)0x014F, "o");	// LATIN SMALL LETTER O WITH BREVE
			conversionTable.Add((char)0x0150, "O");	// LATIN CAPITAL LETTER O WITH DOUBLE ACUTE
			conversionTable.Add((char)0x0151, "o");	// LATIN SMALL LETTER O WITH DOUBLE ACUTE
			conversionTable.Add((char)0x0152, "OE");	// LATIN CAPITAL LIGATURE OE -- no decomposition
			conversionTable.Add((char)0x0153, "oe");	// LATIN SMALL LIGATURE OE -- no decomposition
			conversionTable.Add((char)0x0154, "R");	// LATIN CAPITAL LETTER R WITH ACUTE
			conversionTable.Add((char)0x0155, "r");	// LATIN SMALL LETTER R WITH ACUTE
			conversionTable.Add((char)0x0156, "R");	// LATIN CAPITAL LETTER R WITH CEDILLA
			conversionTable.Add((char)0x0157, "r");	// LATIN SMALL LETTER R WITH CEDILLA
			conversionTable.Add((char)0x0158, "R");	// LATIN CAPITAL LETTER R WITH CARON
			conversionTable.Add((char)0x0159, "r");	// LATIN SMALL LETTER R WITH CARON
			conversionTable.Add((char)0x015A, "S");	// LATIN CAPITAL LETTER S WITH ACUTE
			conversionTable.Add((char)0x015B, "s");	// LATIN SMALL LETTER S WITH ACUTE
			conversionTable.Add((char)0x015C, "S");	// LATIN CAPITAL LETTER S WITH CIRCUMFLEX
			conversionTable.Add((char)0x015D, "s");	// LATIN SMALL LETTER S WITH CIRCUMFLEX
			conversionTable.Add((char)0x015E, "S");	// LATIN CAPITAL LETTER S WITH CEDILLA
			conversionTable.Add((char)0x015F, "s");	// LATIN SMALL LETTER S WITH CEDILLA
			conversionTable.Add((char)0x0160, "S");	// LATIN CAPITAL LETTER S WITH CARON
			conversionTable.Add((char)0x0161, "s");	// LATIN SMALL LETTER S WITH CARON
			conversionTable.Add((char)0x0162, "T");	// LATIN CAPITAL LETTER T WITH CEDILLA
			conversionTable.Add((char)0x0163, "t");	// LATIN SMALL LETTER T WITH CEDILLA
			conversionTable.Add((char)0x0164, "T");	// LATIN CAPITAL LETTER T WITH CARON
			conversionTable.Add((char)0x0165, "t");	// LATIN SMALL LETTER T WITH CARON
			conversionTable.Add((char)0x0166, "T");	// LATIN CAPITAL LETTER T WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0167, "t");	// LATIN SMALL LETTER T WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0168, "U");	// LATIN CAPITAL LETTER U WITH TILDE
			conversionTable.Add((char)0x0169, "u");	// LATIN SMALL LETTER U WITH TILDE
			conversionTable.Add((char)0x016A, "U");	// LATIN CAPITAL LETTER U WITH MACRON
			conversionTable.Add((char)0x016B, "u");	// LATIN SMALL LETTER U WITH MACRON
			conversionTable.Add((char)0x016C, "U");	// LATIN CAPITAL LETTER U WITH BREVE
			conversionTable.Add((char)0x016D, "u");	// LATIN SMALL LETTER U WITH BREVE
			conversionTable.Add((char)0x016E, "U");	// LATIN CAPITAL LETTER U WITH RING ABOVE
			conversionTable.Add((char)0x016F, "u");	// LATIN SMALL LETTER U WITH RING ABOVE
			conversionTable.Add((char)0x0170, "U");	// LATIN CAPITAL LETTER U WITH DOUBLE ACUTE
			conversionTable.Add((char)0x0171, "u");	// LATIN SMALL LETTER U WITH DOUBLE ACUTE
			conversionTable.Add((char)0x0172, "U");	// LATIN CAPITAL LETTER U WITH OGONEK
			conversionTable.Add((char)0x0173, "u");	// LATIN SMALL LETTER U WITH OGONEK
			conversionTable.Add((char)0x0174, "W");	// LATIN CAPITAL LETTER W WITH CIRCUMFLEX
			conversionTable.Add((char)0x0175, "w");	// LATIN SMALL LETTER W WITH CIRCUMFLEX
			conversionTable.Add((char)0x0176, "Y");	// LATIN CAPITAL LETTER Y WITH CIRCUMFLEX
			conversionTable.Add((char)0x0177, "y");	// LATIN SMALL LETTER Y WITH CIRCUMFLEX
			conversionTable.Add((char)0x0178, "Y");	// LATIN CAPITAL LETTER Y WITH DIAERESIS
			conversionTable.Add((char)0x0179, "Z");	// LATIN CAPITAL LETTER Z WITH ACUTE
			conversionTable.Add((char)0x017A, "z");	// LATIN SMALL LETTER Z WITH ACUTE
			conversionTable.Add((char)0x017B, "Z");	// LATIN CAPITAL LETTER Z WITH DOT ABOVE
			conversionTable.Add((char)0x017C, "z");	// LATIN SMALL LETTER Z WITH DOT ABOVE
			conversionTable.Add((char)0x017D, "Z");	// LATIN CAPITAL LETTER Z WITH CARON
			conversionTable.Add((char)0x017E, "z");	// LATIN SMALL LETTER Z WITH CARON
			conversionTable.Add((char)0x017F, "s");	// LATIN SMALL LETTER LONG S
			conversionTable.Add((char)0x0180, "b");	// LATIN SMALL LETTER B WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0181, "B");	// LATIN CAPITAL LETTER B WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0182, "B");	// LATIN CAPITAL LETTER B WITH TOPBAR -- no decomposition
			conversionTable.Add((char)0x0183, "b");	// LATIN SMALL LETTER B WITH TOPBAR -- no decomposition
			conversionTable.Add((char)0x0184, "6");	// LATIN CAPITAL LETTER TONE SIX -- no decomposition
			conversionTable.Add((char)0x0185, "6");	// LATIN SMALL LETTER TONE SIX -- no decomposition
			conversionTable.Add((char)0x0186, "O");	// LATIN CAPITAL LETTER OPEN O -- no decomposition
			conversionTable.Add((char)0x0187, "C");	// LATIN CAPITAL LETTER C WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0188, "c");	// LATIN SMALL LETTER C WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0189, "D");	// LATIN CAPITAL LETTER AFRICAN D -- no decomposition
			conversionTable.Add((char)0x018A, "D");	// LATIN CAPITAL LETTER D WITH HOOK -- no decomposition
			conversionTable.Add((char)0x018B, "D");	// LATIN CAPITAL LETTER D WITH TOPBAR -- no decomposition
			conversionTable.Add((char)0x018C, "d");	// LATIN SMALL LETTER D WITH TOPBAR -- no decomposition
			conversionTable.Add((char)0x018D, "d");	// LATIN SMALL LETTER TURNED DELTA -- no decomposition
			conversionTable.Add((char)0x018E, "E");	// LATIN CAPITAL LETTER REVERSED E -- no decomposition
			conversionTable.Add((char)0x018F, "E");	// LATIN CAPITAL LETTER SCHWA -- no decomposition
			conversionTable.Add((char)0x0190, "E");	// LATIN CAPITAL LETTER OPEN E -- no decomposition
			conversionTable.Add((char)0x0191, "F");	// LATIN CAPITAL LETTER F WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0192, "f");	// LATIN SMALL LETTER F WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0193, "G");	// LATIN CAPITAL LETTER G WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0194, "G");	// LATIN CAPITAL LETTER GAMMA -- no decomposition
			conversionTable.Add((char)0x0195, "hv");	// LATIN SMALL LETTER HV -- no decomposition
			conversionTable.Add((char)0x0196, "I");	// LATIN CAPITAL LETTER IOTA -- no decomposition
			conversionTable.Add((char)0x0197, "I");	// LATIN CAPITAL LETTER I WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0198, "K");	// LATIN CAPITAL LETTER K WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0199, "k");	// LATIN SMALL LETTER K WITH HOOK -- no decomposition
			conversionTable.Add((char)0x019A, "l");	// LATIN SMALL LETTER L WITH BAR -- no decomposition
			conversionTable.Add((char)0x019B, "l");	// LATIN SMALL LETTER LAMBDA WITH STROKE -- no decomposition
			conversionTable.Add((char)0x019C, "M");	// LATIN CAPITAL LETTER TURNED M -- no decomposition
			conversionTable.Add((char)0x019D, "N");	// LATIN CAPITAL LETTER N WITH LEFT HOOK -- no decomposition
			conversionTable.Add((char)0x019E, "n");	// LATIN SMALL LETTER N WITH LONG RIGHT LEG -- no decomposition
			conversionTable.Add((char)0x019F, "O");	// LATIN CAPITAL LETTER O WITH MIDDLE TILDE -- no decomposition
			conversionTable.Add((char)0x01A0, "O");	// LATIN CAPITAL LETTER O WITH HORN
			conversionTable.Add((char)0x01A1, "o");	// LATIN SMALL LETTER O WITH HORN
			conversionTable.Add((char)0x01A2, "OI");	// LATIN CAPITAL LETTER OI -- no decomposition
			conversionTable.Add((char)0x01A3, "oi");	// LATIN SMALL LETTER OI -- no decomposition
			conversionTable.Add((char)0x01A4, "P");	// LATIN CAPITAL LETTER P WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01A5, "p");	// LATIN SMALL LETTER P WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01A6, "YR");	// LATIN LETTER YR -- no decomposition
			conversionTable.Add((char)0x01A7, "2");	// LATIN CAPITAL LETTER TONE TWO -- no decomposition
			conversionTable.Add((char)0x01A8, "2");	// LATIN SMALL LETTER TONE TWO -- no decomposition
			conversionTable.Add((char)0x01A9, "S");	// LATIN CAPITAL LETTER ESH -- no decomposition
			conversionTable.Add((char)0x01AA, "s");	// LATIN LETTER REVERSED ESH LOOP -- no decomposition
			conversionTable.Add((char)0x01AB, "t");	// LATIN SMALL LETTER T WITH PALATAL HOOK -- no decomposition
			conversionTable.Add((char)0x01AC, "T");	// LATIN CAPITAL LETTER T WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01AD, "t");	// LATIN SMALL LETTER T WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01AE, "T");	// LATIN CAPITAL LETTER T WITH RETROFLEX HOOK -- no decomposition
			conversionTable.Add((char)0x01AF, "U");	// LATIN CAPITAL LETTER U WITH HORN
			conversionTable.Add((char)0x01B0, "u");	// LATIN SMALL LETTER U WITH HORN
			conversionTable.Add((char)0x01B1, "u");	// LATIN CAPITAL LETTER UPSILON -- no decomposition
			conversionTable.Add((char)0x01B2, "V");	// LATIN CAPITAL LETTER V WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01B3, "Y");	// LATIN CAPITAL LETTER Y WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01B4, "y");	// LATIN SMALL LETTER Y WITH HOOK -- no decomposition
			conversionTable.Add((char)0x01B5, "Z");	// LATIN CAPITAL LETTER Z WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01B6, "z");	// LATIN SMALL LETTER Z WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01B7, "Z");	// LATIN CAPITAL LETTER EZH -- no decomposition
			conversionTable.Add((char)0x01B8, "Z");	// LATIN CAPITAL LETTER EZH REVERSED -- no decomposition
			conversionTable.Add((char)0x01B9, "Z");	// LATIN SMALL LETTER EZH REVERSED -- no decomposition
			conversionTable.Add((char)0x01BA, "z");	// LATIN SMALL LETTER EZH WITH TAIL -- no decomposition
			conversionTable.Add((char)0x01BB, "2");	// LATIN LETTER TWO WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01BC, "5");	// LATIN CAPITAL LETTER TONE FIVE -- no decomposition
			conversionTable.Add((char)0x01BD, "5");	// LATIN SMALL LETTER TONE FIVE -- no decomposition
			conversionTable.Add((char)0x01BE, "");	// LATIN LETTER INVERTED GLOTTAL STOP WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01BF, "w");	// LATIN LETTER WYNN -- no decomposition
			conversionTable.Add((char)0x01C0, "!");	// LATIN LETTER DENTAL CLICK -- no decomposition
			conversionTable.Add((char)0x01C1, "!");	// LATIN LETTER LATERAL CLICK -- no decomposition
			conversionTable.Add((char)0x01C2, "!");	// LATIN LETTER ALVEOLAR CLICK -- no decomposition
			conversionTable.Add((char)0x01C3, "!");	// LATIN LETTER RETROFLEX CLICK -- no decomposition
			conversionTable.Add((char)0x01C4, "DZ");	// LATIN CAPITAL LETTER DZ WITH CARON
			conversionTable.Add((char)0x01C5, "DZ");	// LATIN CAPITAL LETTER D WITH SMALL LETTER Z WITH CARON
			conversionTable.Add((char)0x01C6, "d");	// LATIN SMALL LETTER DZ WITH CARON
			conversionTable.Add((char)0x01C7, "Lj");	// LATIN CAPITAL LETTER LJ
			conversionTable.Add((char)0x01C8, "Lj");	// LATIN CAPITAL LETTER L WITH SMALL LETTER J
			conversionTable.Add((char)0x01C9, "lj");	// LATIN SMALL LETTER LJ
			conversionTable.Add((char)0x01CA, "NJ");	// LATIN CAPITAL LETTER NJ
			conversionTable.Add((char)0x01CB, "NJ");	// LATIN CAPITAL LETTER N WITH SMALL LETTER J
			conversionTable.Add((char)0x01CC, "nj");	// LATIN SMALL LETTER NJ
			conversionTable.Add((char)0x01CD, "A");	// LATIN CAPITAL LETTER A WITH CARON
			conversionTable.Add((char)0x01CE, "a");	// LATIN SMALL LETTER A WITH CARON
			conversionTable.Add((char)0x01CF, "I");	// LATIN CAPITAL LETTER I WITH CARON
			conversionTable.Add((char)0x01D0, "i");	// LATIN SMALL LETTER I WITH CARON
			conversionTable.Add((char)0x01D1, "O");	// LATIN CAPITAL LETTER O WITH CARON
			conversionTable.Add((char)0x01D2, "o");	// LATIN SMALL LETTER O WITH CARON
			conversionTable.Add((char)0x01D3, "U");	// LATIN CAPITAL LETTER U WITH CARON
			conversionTable.Add((char)0x01D4, "u");	// LATIN SMALL LETTER U WITH CARON
			conversionTable.Add((char)0x01D5, "U");	// LATIN CAPITAL LETTER U WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x01D6, "u");	// LATIN SMALL LETTER U WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x01D7, "U");	// LATIN CAPITAL LETTER U WITH DIAERESIS AND ACUTE
			conversionTable.Add((char)0x01D8, "u");	// LATIN SMALL LETTER U WITH DIAERESIS AND ACUTE
			conversionTable.Add((char)0x01D9, "U");	// LATIN CAPITAL LETTER U WITH DIAERESIS AND CARON
			conversionTable.Add((char)0x01DA, "u");	// LATIN SMALL LETTER U WITH DIAERESIS AND CARON
			conversionTable.Add((char)0x01DB, "U");	// LATIN CAPITAL LETTER U WITH DIAERESIS AND GRAVE
			conversionTable.Add((char)0x01DC, "u");	// LATIN SMALL LETTER U WITH DIAERESIS AND GRAVE
			conversionTable.Add((char)0x01DD, "e");	// LATIN SMALL LETTER TURNED E -- no decomposition
			conversionTable.Add((char)0x01DE, "A");	// LATIN CAPITAL LETTER A WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x01DF, "a");	// LATIN SMALL LETTER A WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x01E0, "A");	// LATIN CAPITAL LETTER A WITH DOT ABOVE AND MACRON
			conversionTable.Add((char)0x01E1, "a");	// LATIN SMALL LETTER A WITH DOT ABOVE AND MACRON
			conversionTable.Add((char)0x01E2, "AE");	// LATIN CAPITAL LETTER AE WITH MACRON
			conversionTable.Add((char)0x01E3, "ae");	// LATIN SMALL LETTER AE WITH MACRON
			conversionTable.Add((char)0x01E4, "G");	// LATIN CAPITAL LETTER G WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01E5, "g");	// LATIN SMALL LETTER G WITH STROKE -- no decomposition
			conversionTable.Add((char)0x01E6, "G");	// LATIN CAPITAL LETTER G WITH CARON
			conversionTable.Add((char)0x01E7, "g");	// LATIN SMALL LETTER G WITH CARON
			conversionTable.Add((char)0x01E8, "K");	// LATIN CAPITAL LETTER K WITH CARON
			conversionTable.Add((char)0x01E9, "k");	// LATIN SMALL LETTER K WITH CARON
			conversionTable.Add((char)0x01EA, "O");	// LATIN CAPITAL LETTER O WITH OGONEK
			conversionTable.Add((char)0x01EB, "o");	// LATIN SMALL LETTER O WITH OGONEK
			conversionTable.Add((char)0x01EC, "O");	// LATIN CAPITAL LETTER O WITH OGONEK AND MACRON
			conversionTable.Add((char)0x01ED, "o");	// LATIN SMALL LETTER O WITH OGONEK AND MACRON
			conversionTable.Add((char)0x01EE, "Z");	// LATIN CAPITAL LETTER EZH WITH CARON
			conversionTable.Add((char)0x01EF, "Z");	// LATIN SMALL LETTER EZH WITH CARON
			conversionTable.Add((char)0x01F0, "j");	// LATIN SMALL LETTER J WITH CARON
			conversionTable.Add((char)0x01F1, "DZ");	// LATIN CAPITAL LETTER DZ
			conversionTable.Add((char)0x01F2, "DZ");	// LATIN CAPITAL LETTER D WITH SMALL LETTER Z
			conversionTable.Add((char)0x01F3, "dz");	// LATIN SMALL LETTER DZ
			conversionTable.Add((char)0x01F4, "G");	// LATIN CAPITAL LETTER G WITH ACUTE
			conversionTable.Add((char)0x01F5, "g");	// LATIN SMALL LETTER G WITH ACUTE
			conversionTable.Add((char)0x01F6, "hv");	// LATIN CAPITAL LETTER HWAIR -- no decomposition
			conversionTable.Add((char)0x01F7, "w");	// LATIN CAPITAL LETTER WYNN -- no decomposition
			conversionTable.Add((char)0x01F8, "N");	// LATIN CAPITAL LETTER N WITH GRAVE
			conversionTable.Add((char)0x01F9, "n");	// LATIN SMALL LETTER N WITH GRAVE
			conversionTable.Add((char)0x01FA, "A");	// LATIN CAPITAL LETTER A WITH RING ABOVE AND ACUTE
			conversionTable.Add((char)0x01FB, "a");	// LATIN SMALL LETTER A WITH RING ABOVE AND ACUTE
			conversionTable.Add((char)0x01FC, "AE");	// LATIN CAPITAL LETTER AE WITH ACUTE
			conversionTable.Add((char)0x01FD, "ae");	// LATIN SMALL LETTER AE WITH ACUTE
			conversionTable.Add((char)0x01FE, "O");	// LATIN CAPITAL LETTER O WITH STROKE AND ACUTE
			conversionTable.Add((char)0x01FF, "o");	// LATIN SMALL LETTER O WITH STROKE AND ACUTE
			conversionTable.Add((char)0x0200, "A");	// LATIN CAPITAL LETTER A WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0201, "a");	// LATIN SMALL LETTER A WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0202, "A");	// LATIN CAPITAL LETTER A WITH INVERTED BREVE
			conversionTable.Add((char)0x0203, "a");	// LATIN SMALL LETTER A WITH INVERTED BREVE
			conversionTable.Add((char)0x0204, "E");	// LATIN CAPITAL LETTER E WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0205, "e");	// LATIN SMALL LETTER E WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0206, "E");	// LATIN CAPITAL LETTER E WITH INVERTED BREVE
			conversionTable.Add((char)0x0207, "e");	// LATIN SMALL LETTER E WITH INVERTED BREVE
			conversionTable.Add((char)0x0208, "I");	// LATIN CAPITAL LETTER I WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0209, "i");	// LATIN SMALL LETTER I WITH DOUBLE GRAVE
			conversionTable.Add((char)0x020A, "I");	// LATIN CAPITAL LETTER I WITH INVERTED BREVE
			conversionTable.Add((char)0x020B, "i");	// LATIN SMALL LETTER I WITH INVERTED BREVE
			conversionTable.Add((char)0x020C, "O");	// LATIN CAPITAL LETTER O WITH DOUBLE GRAVE
			conversionTable.Add((char)0x020D, "o");	// LATIN SMALL LETTER O WITH DOUBLE GRAVE
			conversionTable.Add((char)0x020E, "O");	// LATIN CAPITAL LETTER O WITH INVERTED BREVE
			conversionTable.Add((char)0x020F, "o");	// LATIN SMALL LETTER O WITH INVERTED BREVE
			conversionTable.Add((char)0x0210, "R");	// LATIN CAPITAL LETTER R WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0211, "r");	// LATIN SMALL LETTER R WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0212, "R");	// LATIN CAPITAL LETTER R WITH INVERTED BREVE
			conversionTable.Add((char)0x0213, "r");	// LATIN SMALL LETTER R WITH INVERTED BREVE
			conversionTable.Add((char)0x0214, "U");	// LATIN CAPITAL LETTER U WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0215, "u");	// LATIN SMALL LETTER U WITH DOUBLE GRAVE
			conversionTable.Add((char)0x0216, "U");	// LATIN CAPITAL LETTER U WITH INVERTED BREVE
			conversionTable.Add((char)0x0217, "u");	// LATIN SMALL LETTER U WITH INVERTED BREVE
			conversionTable.Add((char)0x0218, "S");	// LATIN CAPITAL LETTER S WITH COMMA BELOW
			conversionTable.Add((char)0x0219, "s");	// LATIN SMALL LETTER S WITH COMMA BELOW
			conversionTable.Add((char)0x021A, "T");	// LATIN CAPITAL LETTER T WITH COMMA BELOW
			conversionTable.Add((char)0x021B, "t");	// LATIN SMALL LETTER T WITH COMMA BELOW
			conversionTable.Add((char)0x021C, "Z");	// LATIN CAPITAL LETTER YOGH -- no decomposition
			conversionTable.Add((char)0x021D, "z");	// LATIN SMALL LETTER YOGH -- no decomposition
			conversionTable.Add((char)0x021E, "H");	// LATIN CAPITAL LETTER H WITH CARON
			conversionTable.Add((char)0x021F, "h");	// LATIN SMALL LETTER H WITH CARON
			conversionTable.Add((char)0x0220, "N");	// LATIN CAPITAL LETTER N WITH LONG RIGHT LEG -- no decomposition
			conversionTable.Add((char)0x0221, "d");	// LATIN SMALL LETTER D WITH CURL -- no decomposition
			conversionTable.Add((char)0x0222, "OU");	// LATIN CAPITAL LETTER OU -- no decomposition
			conversionTable.Add((char)0x0223, "ou");	// LATIN SMALL LETTER OU -- no decomposition
			conversionTable.Add((char)0x0224, "Z");	// LATIN CAPITAL LETTER Z WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0225, "z");	// LATIN SMALL LETTER Z WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0226, "A");	// LATIN CAPITAL LETTER A WITH DOT ABOVE
			conversionTable.Add((char)0x0227, "a");	// LATIN SMALL LETTER A WITH DOT ABOVE
			conversionTable.Add((char)0x0228, "E");	// LATIN CAPITAL LETTER E WITH CEDILLA
			conversionTable.Add((char)0x0229, "e");	// LATIN SMALL LETTER E WITH CEDILLA
			conversionTable.Add((char)0x022A, "O");	// LATIN CAPITAL LETTER O WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x022B, "o");	// LATIN SMALL LETTER O WITH DIAERESIS AND MACRON
			conversionTable.Add((char)0x022C, "O");	// LATIN CAPITAL LETTER O WITH TILDE AND MACRON
			conversionTable.Add((char)0x022D, "o");	// LATIN SMALL LETTER O WITH TILDE AND MACRON
			conversionTable.Add((char)0x022E, "O");	// LATIN CAPITAL LETTER O WITH DOT ABOVE
			conversionTable.Add((char)0x022F, "o");	// LATIN SMALL LETTER O WITH DOT ABOVE
			conversionTable.Add((char)0x0230, "O");	// LATIN CAPITAL LETTER O WITH DOT ABOVE AND MACRON
			conversionTable.Add((char)0x0231, "o");	// LATIN SMALL LETTER O WITH DOT ABOVE AND MACRON
			conversionTable.Add((char)0x0232, "Y");	// LATIN CAPITAL LETTER Y WITH MACRON
			conversionTable.Add((char)0x0233, "y");	// LATIN SMALL LETTER Y WITH MACRON
			conversionTable.Add((char)0x0234, "l");	// LATIN SMALL LETTER L WITH CURL -- no decomposition
			conversionTable.Add((char)0x0235, "n");	// LATIN SMALL LETTER N WITH CURL -- no decomposition
			conversionTable.Add((char)0x0236, "t");	// LATIN SMALL LETTER T WITH CURL -- no decomposition
			conversionTable.Add((char)0x0250, "a");	// LATIN SMALL LETTER TURNED A -- no decomposition
			conversionTable.Add((char)0x0251, "a");	// LATIN SMALL LETTER ALPHA -- no decomposition
			conversionTable.Add((char)0x0252, "a");	// LATIN SMALL LETTER TURNED ALPHA -- no decomposition
			conversionTable.Add((char)0x0253, "b");	// LATIN SMALL LETTER B WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0254, "o");	// LATIN SMALL LETTER OPEN O -- no decomposition
			conversionTable.Add((char)0x0255, "c");	// LATIN SMALL LETTER C WITH CURL -- no decomposition
			conversionTable.Add((char)0x0256, "d");	// LATIN SMALL LETTER D WITH TAIL -- no decomposition
			conversionTable.Add((char)0x0257, "d");	// LATIN SMALL LETTER D WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0258, "e");	// LATIN SMALL LETTER REVERSED E -- no decomposition
			conversionTable.Add((char)0x0259, "e");	// LATIN SMALL LETTER SCHWA -- no decomposition
			conversionTable.Add((char)0x025A, "e");	// LATIN SMALL LETTER SCHWA WITH HOOK -- no decomposition
			conversionTable.Add((char)0x025B, "e");	// LATIN SMALL LETTER OPEN E -- no decomposition
			conversionTable.Add((char)0x025C, "e");	// LATIN SMALL LETTER REVERSED OPEN E -- no decomposition
			conversionTable.Add((char)0x025D, "e");	// LATIN SMALL LETTER REVERSED OPEN E WITH HOOK -- no decomposition
			conversionTable.Add((char)0x025E, "e");	// LATIN SMALL LETTER CLOSED REVERSED OPEN E -- no decomposition
			conversionTable.Add((char)0x025F, "j");	// LATIN SMALL LETTER DOTLESS J WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0260, "g");	// LATIN SMALL LETTER G WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0261, "g");	// LATIN SMALL LETTER SCRIPT G -- no decomposition
			conversionTable.Add((char)0x0262, "G");	// LATIN LETTER SMALL CAPITAL G -- no decomposition
			conversionTable.Add((char)0x0263, "g");	// LATIN SMALL LETTER GAMMA -- no decomposition
			conversionTable.Add((char)0x0264, "y");	// LATIN SMALL LETTER RAMS HORN -- no decomposition
			conversionTable.Add((char)0x0265, "h");	// LATIN SMALL LETTER TURNED H -- no decomposition
			conversionTable.Add((char)0x0266, "h");	// LATIN SMALL LETTER H WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0267, "h");	// LATIN SMALL LETTER HENG WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0268, "i");	// LATIN SMALL LETTER I WITH STROKE -- no decomposition
			conversionTable.Add((char)0x0269, "i");	// LATIN SMALL LETTER IOTA -- no decomposition
			conversionTable.Add((char)0x026A, "I");	// LATIN LETTER SMALL CAPITAL I -- no decomposition
			conversionTable.Add((char)0x026B, "l");	// LATIN SMALL LETTER L WITH MIDDLE TILDE -- no decomposition
			conversionTable.Add((char)0x026C, "l");	// LATIN SMALL LETTER L WITH BELT -- no decomposition
			conversionTable.Add((char)0x026D, "l");	// LATIN SMALL LETTER L WITH RETROFLEX HOOK -- no decomposition
			conversionTable.Add((char)0x026E, "lz");	// LATIN SMALL LETTER LEZH -- no decomposition
			conversionTable.Add((char)0x026F, "m");	// LATIN SMALL LETTER TURNED M -- no decomposition
			conversionTable.Add((char)0x0270, "m");	// LATIN SMALL LETTER TURNED M WITH LONG LEG -- no decomposition
			conversionTable.Add((char)0x0271, "m");	// LATIN SMALL LETTER M WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0272, "n");	// LATIN SMALL LETTER N WITH LEFT HOOK -- no decomposition
			conversionTable.Add((char)0x0273, "n");	// LATIN SMALL LETTER N WITH RETROFLEX HOOK -- no decomposition
			conversionTable.Add((char)0x0274, "N");	// LATIN LETTER SMALL CAPITAL N -- no decomposition
			conversionTable.Add((char)0x0275, "o");	// LATIN SMALL LETTER BARRED O -- no decomposition
			conversionTable.Add((char)0x0276, "OE");	// LATIN LETTER SMALL CAPITAL OE -- no decomposition
			conversionTable.Add((char)0x0277, "o");	// LATIN SMALL LETTER CLOSED OMEGA -- no decomposition
			conversionTable.Add((char)0x0278, "ph");	// LATIN SMALL LETTER PHI -- no decomposition
			conversionTable.Add((char)0x0279, "r");	// LATIN SMALL LETTER TURNED R -- no decomposition
			conversionTable.Add((char)0x027A, "r");	// LATIN SMALL LETTER TURNED R WITH LONG LEG -- no decomposition
			conversionTable.Add((char)0x027B, "r");	// LATIN SMALL LETTER TURNED R WITH HOOK -- no decomposition
			conversionTable.Add((char)0x027C, "r");	// LATIN SMALL LETTER R WITH LONG LEG -- no decomposition
			conversionTable.Add((char)0x027D, "r");	// LATIN SMALL LETTER R WITH TAIL -- no decomposition
			conversionTable.Add((char)0x027E, "r");	// LATIN SMALL LETTER R WITH FISHHOOK -- no decomposition
			conversionTable.Add((char)0x027F, "r");	// LATIN SMALL LETTER REVERSED R WITH FISHHOOK -- no decomposition
			conversionTable.Add((char)0x0280, "R");	// LATIN LETTER SMALL CAPITAL R -- no decomposition
			conversionTable.Add((char)0x0281, "r");	// LATIN LETTER SMALL CAPITAL INVERTED R -- no decomposition
			conversionTable.Add((char)0x0282, "s");	// LATIN SMALL LETTER S WITH HOOK -- no decomposition
			conversionTable.Add((char)0x0283, "s");	// LATIN SMALL LETTER ESH -- no decomposition
			conversionTable.Add((char)0x0284, "j");	// LATIN SMALL LETTER DOTLESS J WITH STROKE AND HOOK -- no decomposition
			conversionTable.Add((char)0x0285, "s");	// LATIN SMALL LETTER SQUAT REVERSED ESH -- no decomposition
			conversionTable.Add((char)0x0286, "s");	// LATIN SMALL LETTER ESH WITH CURL -- no decomposition
			conversionTable.Add((char)0x0287, "y");	// LATIN SMALL LETTER TURNED T -- no decomposition
			conversionTable.Add((char)0x0288, "t");	// LATIN SMALL LETTER T WITH RETROFLEX HOOK -- no decomposition
			conversionTable.Add((char)0x0289, "u");	// LATIN SMALL LETTER U BAR -- no decomposition
			conversionTable.Add((char)0x028A, "u");	// LATIN SMALL LETTER UPSILON -- no decomposition
			conversionTable.Add((char)0x028B, "u");	// LATIN SMALL LETTER V WITH HOOK -- no decomposition
			conversionTable.Add((char)0x028C, "v");	// LATIN SMALL LETTER TURNED V -- no decomposition
			conversionTable.Add((char)0x028D, "w");	// LATIN SMALL LETTER TURNED W -- no decomposition
			conversionTable.Add((char)0x028E, "y");	// LATIN SMALL LETTER TURNED Y -- no decomposition
			conversionTable.Add((char)0x028F, "Y");	// LATIN LETTER SMALL CAPITAL Y -- no decomposition
			conversionTable.Add((char)0x0290, "z");	// LATIN SMALL LETTER Z WITH RETROFLEX HOOK -- no decomposition
			conversionTable.Add((char)0x0291, "z");	// LATIN SMALL LETTER Z WITH CURL -- no decomposition
			conversionTable.Add((char)0x0292, "z");	// LATIN SMALL LETTER EZH -- no decomposition
			conversionTable.Add((char)0x0293, "z");	// LATIN SMALL LETTER EZH WITH CURL -- no decomposition
			conversionTable.Add((char)0x0294, "'");	// LATIN LETTER GLOTTAL STOP -- no decomposition
			conversionTable.Add((char)0x0295, "'");	// LATIN LETTER PHARYNGEAL VOICED FRICATIVE -- no decomposition
			conversionTable.Add((char)0x0296, "'");	// LATIN LETTER INVERTED GLOTTAL STOP -- no decomposition
			conversionTable.Add((char)0x0297, "C");	// LATIN LETTER STRETCHED C -- no decomposition
			conversionTable.Add((char)0x0298, "O");	// LATIN LETTER BILABIAL CLICK -- no decomposition
			conversionTable.Add((char)0x0299, "B");	// LATIN LETTER SMALL CAPITAL B -- no decomposition
			conversionTable.Add((char)0x029A, "e");	// LATIN SMALL LETTER CLOSED OPEN E -- no decomposition
			conversionTable.Add((char)0x029B, "G");	// LATIN LETTER SMALL CAPITAL G WITH HOOK -- no decomposition
			conversionTable.Add((char)0x029C, "H");	// LATIN LETTER SMALL CAPITAL H -- no decomposition
			conversionTable.Add((char)0x029D, "j");	// LATIN SMALL LETTER J WITH CROSSED-TAIL -- no decomposition
			conversionTable.Add((char)0x029E, "k");	// LATIN SMALL LETTER TURNED K -- no decomposition
			conversionTable.Add((char)0x029F, "L");	// LATIN LETTER SMALL CAPITAL L -- no decomposition
			conversionTable.Add((char)0x02A0, "q");	// LATIN SMALL LETTER Q WITH HOOK -- no decomposition
			conversionTable.Add((char)0x02A1, "'");	// LATIN LETTER GLOTTAL STOP WITH STROKE -- no decomposition
			conversionTable.Add((char)0x02A2, "'");	// LATIN LETTER REVERSED GLOTTAL STOP WITH STROKE -- no decomposition
			conversionTable.Add((char)0x02A3, "dz");	// LATIN SMALL LETTER DZ DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02A4, "dz");	// LATIN SMALL LETTER DEZH DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02A5, "dz");	// LATIN SMALL LETTER DZ DIGRAPH WITH CURL -- no decomposition
			conversionTable.Add((char)0x02A6, "ts");	// LATIN SMALL LETTER TS DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02A7, "ts");	// LATIN SMALL LETTER TESH DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02A8, ""); // LATIN SMALL LETTER TC DIGRAPH WITH CURL -- no decomposition
			conversionTable.Add((char)0x02A9, "fn");	// LATIN SMALL LETTER FENG DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02AA, "ls");	// LATIN SMALL LETTER LS DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02AB, "lz");	// LATIN SMALL LETTER LZ DIGRAPH -- no decomposition
			conversionTable.Add((char)0x02AC, "w");	// LATIN LETTER BILABIAL PERCUSSIVE -- no decomposition
			conversionTable.Add((char)0x02AD, "t");	// LATIN LETTER BIDENTAL PERCUSSIVE -- no decomposition
			conversionTable.Add((char)0x02AE, "h");	// LATIN SMALL LETTER TURNED H WITH FISHHOOK -- no decomposition
			conversionTable.Add((char)0x02AF, "h");	// LATIN SMALL LETTER TURNED H WITH FISHHOOK AND TAIL -- no decomposition
			conversionTable.Add((char)0x02B0, "h");	// MODIFIER LETTER SMALL H
			conversionTable.Add((char)0x02B1, "h");	// MODIFIER LETTER SMALL H WITH HOOK
			conversionTable.Add((char)0x02B2, "j");	// MODIFIER LETTER SMALL J
			conversionTable.Add((char)0x02B3, "r");	// MODIFIER LETTER SMALL R
			conversionTable.Add((char)0x02B4, "r");	// MODIFIER LETTER SMALL TURNED R
			conversionTable.Add((char)0x02B5, "r");	// MODIFIER LETTER SMALL TURNED R WITH HOOK
			conversionTable.Add((char)0x02B6, "R");	// MODIFIER LETTER SMALL CAPITAL INVERTED R
			conversionTable.Add((char)0x02B7, "w");	// MODIFIER LETTER SMALL W
			conversionTable.Add((char)0x02B8, "y");	// MODIFIER LETTER SMALL Y
			conversionTable.Add((char)0x02E1, "l");	// MODIFIER LETTER SMALL L
			conversionTable.Add((char)0x02E2, "s");	// MODIFIER LETTER SMALL S
			conversionTable.Add((char)0x02E3, "x");	// MODIFIER LETTER SMALL X
			conversionTable.Add((char)0x02E4, "'");	// MODIFIER LETTER SMALL REVERSED GLOTTAL STOP
			conversionTable.Add((char)0x1D00, "A");	// LATIN LETTER SMALL CAPITAL A -- no decomposition
			conversionTable.Add((char)0x1D01, "AE");	// LATIN LETTER SMALL CAPITAL AE -- no decomposition
			conversionTable.Add((char)0x1D02, "ae");	// LATIN SMALL LETTER TURNED AE -- no decomposition
			conversionTable.Add((char)0x1D03, "B");	// LATIN LETTER SMALL CAPITAL BARRED B -- no decomposition
			conversionTable.Add((char)0x1D04, "C");	// LATIN LETTER SMALL CAPITAL C -- no decomposition
			conversionTable.Add((char)0x1D05, "D");	// LATIN LETTER SMALL CAPITAL D -- no decomposition
			conversionTable.Add((char)0x1D06, "TH");	// LATIN LETTER SMALL CAPITAL ETH -- no decomposition
			conversionTable.Add((char)0x1D07, "E");	// LATIN LETTER SMALL CAPITAL E -- no decomposition
			conversionTable.Add((char)0x1D08, "e");	// LATIN SMALL LETTER TURNED OPEN E -- no decomposition
			conversionTable.Add((char)0x1D09, "i");	// LATIN SMALL LETTER TURNED I -- no decomposition
			conversionTable.Add((char)0x1D0A, "J");	// LATIN LETTER SMALL CAPITAL J -- no decomposition
			conversionTable.Add((char)0x1D0B, "K");	// LATIN LETTER SMALL CAPITAL K -- no decomposition
			conversionTable.Add((char)0x1D0C, "L");	// LATIN LETTER SMALL CAPITAL L WITH STROKE -- no decomposition
			conversionTable.Add((char)0x1D0D, "M");	// LATIN LETTER SMALL CAPITAL M -- no decomposition
			conversionTable.Add((char)0x1D0E, "N");	// LATIN LETTER SMALL CAPITAL REVERSED N -- no decomposition
			conversionTable.Add((char)0x1D0F, "O");	// LATIN LETTER SMALL CAPITAL O -- no decomposition
			conversionTable.Add((char)0x1D10, "O");	// LATIN LETTER SMALL CAPITAL OPEN O -- no decomposition
			conversionTable.Add((char)0x1D11, "o");	// LATIN SMALL LETTER SIDEWAYS O -- no decomposition
			conversionTable.Add((char)0x1D12, "o");	// LATIN SMALL LETTER SIDEWAYS OPEN O -- no decomposition
			conversionTable.Add((char)0x1D13, "o");	// LATIN SMALL LETTER SIDEWAYS O WITH STROKE -- no decomposition
			conversionTable.Add((char)0x1D14, "oe");	// LATIN SMALL LETTER TURNED OE -- no decomposition
			conversionTable.Add((char)0x1D15, "ou");	// LATIN LETTER SMALL CAPITAL OU -- no decomposition
			conversionTable.Add((char)0x1D16, "o");	// LATIN SMALL LETTER TOP HALF O -- no decomposition
			conversionTable.Add((char)0x1D17, "o");	// LATIN SMALL LETTER BOTTOM HALF O -- no decomposition
			conversionTable.Add((char)0x1D18, "P");	// LATIN LETTER SMALL CAPITAL P -- no decomposition
			conversionTable.Add((char)0x1D19, "R");	// LATIN LETTER SMALL CAPITAL REVERSED R -- no decomposition
			conversionTable.Add((char)0x1D1A, "R");	// LATIN LETTER SMALL CAPITAL TURNED R -- no decomposition
			conversionTable.Add((char)0x1D1B, "T");	// LATIN LETTER SMALL CAPITAL T -- no decomposition
			conversionTable.Add((char)0x1D1C, "U");	// LATIN LETTER SMALL CAPITAL U -- no decomposition
			conversionTable.Add((char)0x1D1D, "u");	// LATIN SMALL LETTER SIDEWAYS U -- no decomposition
			conversionTable.Add((char)0x1D1E, "u");	// LATIN SMALL LETTER SIDEWAYS DIAERESIZED U -- no decomposition
			conversionTable.Add((char)0x1D1F, "m");	// LATIN SMALL LETTER SIDEWAYS TURNED M -- no decomposition
			conversionTable.Add((char)0x1D20, "V");	// LATIN LETTER SMALL CAPITAL V -- no decomposition
			conversionTable.Add((char)0x1D21, "W");	// LATIN LETTER SMALL CAPITAL W -- no decomposition
			conversionTable.Add((char)0x1D22, "Z");	// LATIN LETTER SMALL CAPITAL Z -- no decomposition
			conversionTable.Add((char)0x1D23, "EZH");	// LATIN LETTER SMALL CAPITAL EZH -- no decomposition
			conversionTable.Add((char)0x1D24, "'");	// LATIN LETTER VOICED LARYNGEAL SPIRANT -- no decomposition
			conversionTable.Add((char)0x1D25, "L");	// LATIN LETTER AIN -- no decomposition
			conversionTable.Add((char)0x1D2C, "A");	// MODIFIER LETTER CAPITAL A
			conversionTable.Add((char)0x1D2D, "AE");	// MODIFIER LETTER CAPITAL AE
			conversionTable.Add((char)0x1D2E, "B");	// MODIFIER LETTER CAPITAL B
			conversionTable.Add((char)0x1D2F, "B");	// MODIFIER LETTER CAPITAL BARRED B -- no decomposition
			conversionTable.Add((char)0x1D30, "D");	// MODIFIER LETTER CAPITAL D
			conversionTable.Add((char)0x1D31, "E");	// MODIFIER LETTER CAPITAL E
			conversionTable.Add((char)0x1D32, "E");	// MODIFIER LETTER CAPITAL REVERSED E
			conversionTable.Add((char)0x1D33, "G");	// MODIFIER LETTER CAPITAL G
			conversionTable.Add((char)0x1D34, "H");	// MODIFIER LETTER CAPITAL H
			conversionTable.Add((char)0x1D35, "I");	// MODIFIER LETTER CAPITAL I
			conversionTable.Add((char)0x1D36, "J");	// MODIFIER LETTER CAPITAL J
			conversionTable.Add((char)0x1D37, "K");	// MODIFIER LETTER CAPITAL K
			conversionTable.Add((char)0x1D38, "L");	// MODIFIER LETTER CAPITAL L
			conversionTable.Add((char)0x1D39, "M");	// MODIFIER LETTER CAPITAL M
			conversionTable.Add((char)0x1D3A, "N");	// MODIFIER LETTER CAPITAL N
			conversionTable.Add((char)0x1D3B, "N");	// MODIFIER LETTER CAPITAL REVERSED N -- no decomposition
			conversionTable.Add((char)0x1D3C, "O");	// MODIFIER LETTER CAPITAL O
			conversionTable.Add((char)0x1D3D, "OU");	// MODIFIER LETTER CAPITAL OU
			conversionTable.Add((char)0x1D3E, "P");	// MODIFIER LETTER CAPITAL P
			conversionTable.Add((char)0x1D3F, "R");	// MODIFIER LETTER CAPITAL R
			conversionTable.Add((char)0x1D40, "T");	// MODIFIER LETTER CAPITAL T
			conversionTable.Add((char)0x1D41, "U");	// MODIFIER LETTER CAPITAL U
			conversionTable.Add((char)0x1D42, "W");	// MODIFIER LETTER CAPITAL W
			conversionTable.Add((char)0x1D43, "a");	// MODIFIER LETTER SMALL A
			conversionTable.Add((char)0x1D44, "a");	// MODIFIER LETTER SMALL TURNED A
			conversionTable.Add((char)0x1D46, "ae");	// MODIFIER LETTER SMALL TURNED AE
			conversionTable.Add((char)0x1D47, "b");    // MODIFIER LETTER SMALL B
			conversionTable.Add((char)0x1D48, "d");    // MODIFIER LETTER SMALL D
			conversionTable.Add((char)0x1D49, "e");    // MODIFIER LETTER SMALL E
			conversionTable.Add((char)0x1D4A, "e");    // MODIFIER LETTER SMALL SCHWA
			conversionTable.Add((char)0x1D4B, "e");    // MODIFIER LETTER SMALL OPEN E
			conversionTable.Add((char)0x1D4C, "e");    // MODIFIER LETTER SMALL TURNED OPEN E
			conversionTable.Add((char)0x1D4D, "g");    // MODIFIER LETTER SMALL G
			conversionTable.Add((char)0x1D4E, "i");    // MODIFIER LETTER SMALL TURNED I -- no decomposition
			conversionTable.Add((char)0x1D4F, "k");    // MODIFIER LETTER SMALL K
			conversionTable.Add((char)0x1D50, "m");	// MODIFIER LETTER SMALL M
			conversionTable.Add((char)0x1D51, "g");	// MODIFIER LETTER SMALL ENG
			conversionTable.Add((char)0x1D52, "o");	// MODIFIER LETTER SMALL O
			conversionTable.Add((char)0x1D53, "o");	// MODIFIER LETTER SMALL OPEN O
			conversionTable.Add((char)0x1D54, "o");	// MODIFIER LETTER SMALL TOP HALF O
			conversionTable.Add((char)0x1D55, "o");	// MODIFIER LETTER SMALL BOTTOM HALF O
			conversionTable.Add((char)0x1D56, "p");	// MODIFIER LETTER SMALL P
			conversionTable.Add((char)0x1D57, "t");	// MODIFIER LETTER SMALL T
			conversionTable.Add((char)0x1D58, "u");	// MODIFIER LETTER SMALL U
			conversionTable.Add((char)0x1D59, "u");	// MODIFIER LETTER SMALL SIDEWAYS U
			conversionTable.Add((char)0x1D5A, "m");	// MODIFIER LETTER SMALL TURNED M
			conversionTable.Add((char)0x1D5B, "v");	// MODIFIER LETTER SMALL V
			conversionTable.Add((char)0x1D62, "i");	// LATIN SUBSCRIPT SMALL LETTER I
			conversionTable.Add((char)0x1D63, "r");	// LATIN SUBSCRIPT SMALL LETTER R
			conversionTable.Add((char)0x1D64, "u");	// LATIN SUBSCRIPT SMALL LETTER U
			conversionTable.Add((char)0x1D65, "v");	// LATIN SUBSCRIPT SMALL LETTER V
			conversionTable.Add((char)0x1D6B, "ue");	// LATIN SMALL LETTER UE -- no decomposition
			conversionTable.Add((char)0x1E00, "A");	// LATIN CAPITAL LETTER A WITH RING BELOW
			conversionTable.Add((char)0x1E01, "a");	// LATIN SMALL LETTER A WITH RING BELOW
			conversionTable.Add((char)0x1E02, "B");	// LATIN CAPITAL LETTER B WITH DOT ABOVE
			conversionTable.Add((char)0x1E03, "b");	// LATIN SMALL LETTER B WITH DOT ABOVE
			conversionTable.Add((char)0x1E04, "B");	// LATIN CAPITAL LETTER B WITH DOT BELOW
			conversionTable.Add((char)0x1E05, "b");	// LATIN SMALL LETTER B WITH DOT BELOW
			conversionTable.Add((char)0x1E06, "B");	// LATIN CAPITAL LETTER B WITH LINE BELOW
			conversionTable.Add((char)0x1E07, "b");	// LATIN SMALL LETTER B WITH LINE BELOW
			conversionTable.Add((char)0x1E08, "C");	// LATIN CAPITAL LETTER C WITH CEDILLA AND ACUTE
			conversionTable.Add((char)0x1E09, "c");	// LATIN SMALL LETTER C WITH CEDILLA AND ACUTE
			conversionTable.Add((char)0x1E0A, "D");	// LATIN CAPITAL LETTER D WITH DOT ABOVE
			conversionTable.Add((char)0x1E0B, "d");	// LATIN SMALL LETTER D WITH DOT ABOVE
			conversionTable.Add((char)0x1E0C, "D");	// LATIN CAPITAL LETTER D WITH DOT BELOW
			conversionTable.Add((char)0x1E0D, "d");	// LATIN SMALL LETTER D WITH DOT BELOW
			conversionTable.Add((char)0x1E0E, "D");	// LATIN CAPITAL LETTER D WITH LINE BELOW
			conversionTable.Add((char)0x1E0F, "d");	// LATIN SMALL LETTER D WITH LINE BELOW
			conversionTable.Add((char)0x1E10, "D");	// LATIN CAPITAL LETTER D WITH CEDILLA
			conversionTable.Add((char)0x1E11, "d");	// LATIN SMALL LETTER D WITH CEDILLA
			conversionTable.Add((char)0x1E12, "D");	// LATIN CAPITAL LETTER D WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E13, "d");	// LATIN SMALL LETTER D WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E14, "E");	// LATIN CAPITAL LETTER E WITH MACRON AND GRAVE
			conversionTable.Add((char)0x1E15, "e");	// LATIN SMALL LETTER E WITH MACRON AND GRAVE
			conversionTable.Add((char)0x1E16, "E");	// LATIN CAPITAL LETTER E WITH MACRON AND ACUTE
			conversionTable.Add((char)0x1E17, "e");	// LATIN SMALL LETTER E WITH MACRON AND ACUTE
			conversionTable.Add((char)0x1E18, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E19, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E1A, "E");	// LATIN CAPITAL LETTER E WITH TILDE BELOW
			conversionTable.Add((char)0x1E1B, "e");	// LATIN SMALL LETTER E WITH TILDE BELOW
			conversionTable.Add((char)0x1E1C, "E");	// LATIN CAPITAL LETTER E WITH CEDILLA AND BREVE
			conversionTable.Add((char)0x1E1D, "e");	// LATIN SMALL LETTER E WITH CEDILLA AND BREVE
			conversionTable.Add((char)0x1E1E, "F");	// LATIN CAPITAL LETTER F WITH DOT ABOVE
			conversionTable.Add((char)0x1E1F, "f");	// LATIN SMALL LETTER F WITH DOT ABOVE
			conversionTable.Add((char)0x1E20, "G");	// LATIN CAPITAL LETTER G WITH MACRON
			conversionTable.Add((char)0x1E21, "g");	// LATIN SMALL LETTER G WITH MACRON
			conversionTable.Add((char)0x1E22, "H");	// LATIN CAPITAL LETTER H WITH DOT ABOVE
			conversionTable.Add((char)0x1E23, "h");	// LATIN SMALL LETTER H WITH DOT ABOVE
			conversionTable.Add((char)0x1E24, "H");	// LATIN CAPITAL LETTER H WITH DOT BELOW
			conversionTable.Add((char)0x1E25, "h");	// LATIN SMALL LETTER H WITH DOT BELOW
			conversionTable.Add((char)0x1E26, "H");	// LATIN CAPITAL LETTER H WITH DIAERESIS
			conversionTable.Add((char)0x1E27, "h");	// LATIN SMALL LETTER H WITH DIAERESIS
			conversionTable.Add((char)0x1E28, "H");	// LATIN CAPITAL LETTER H WITH CEDILLA
			conversionTable.Add((char)0x1E29, "h");	// LATIN SMALL LETTER H WITH CEDILLA
			conversionTable.Add((char)0x1E2A, "H");	// LATIN CAPITAL LETTER H WITH BREVE BELOW
			conversionTable.Add((char)0x1E2B, "h");	// LATIN SMALL LETTER H WITH BREVE BELOW
			conversionTable.Add((char)0x1E2C, "I");	// LATIN CAPITAL LETTER I WITH TILDE BELOW
			conversionTable.Add((char)0x1E2D, "i");	// LATIN SMALL LETTER I WITH TILDE BELOW
			conversionTable.Add((char)0x1E2E, "I");	// LATIN CAPITAL LETTER I WITH DIAERESIS AND ACUTE
			conversionTable.Add((char)0x1E2F, "i");	// LATIN SMALL LETTER I WITH DIAERESIS AND ACUTE
			conversionTable.Add((char)0x1E30, "K");	// LATIN CAPITAL LETTER K WITH ACUTE
			conversionTable.Add((char)0x1E31, "k");	// LATIN SMALL LETTER K WITH ACUTE
			conversionTable.Add((char)0x1E32, "K");	// LATIN CAPITAL LETTER K WITH DOT BELOW
			conversionTable.Add((char)0x1E33, "k");	// LATIN SMALL LETTER K WITH DOT BELOW
			conversionTable.Add((char)0x1E34, "K");	// LATIN CAPITAL LETTER K WITH LINE BELOW
			conversionTable.Add((char)0x1E35, "k");	// LATIN SMALL LETTER K WITH LINE BELOW
			conversionTable.Add((char)0x1E36, "L");	// LATIN CAPITAL LETTER L WITH DOT BELOW
			conversionTable.Add((char)0x1E37, "l");	// LATIN SMALL LETTER L WITH DOT BELOW
			conversionTable.Add((char)0x1E38, "L");	// LATIN CAPITAL LETTER L WITH DOT BELOW AND MACRON
			conversionTable.Add((char)0x1E39, "l");	// LATIN SMALL LETTER L WITH DOT BELOW AND MACRON
			conversionTable.Add((char)0x1E3A, "L");	// LATIN CAPITAL LETTER L WITH LINE BELOW
			conversionTable.Add((char)0x1E3B, "l");	// LATIN SMALL LETTER L WITH LINE BELOW
			conversionTable.Add((char)0x1E3C, "L");	// LATIN CAPITAL LETTER L WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E3D, "l");	// LATIN SMALL LETTER L WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E3E, "M");	// LATIN CAPITAL LETTER M WITH ACUTE
			conversionTable.Add((char)0x1E3F, "m");	// LATIN SMALL LETTER M WITH ACUTE
			conversionTable.Add((char)0x1E40, "M");	// LATIN CAPITAL LETTER M WITH DOT ABOVE
			conversionTable.Add((char)0x1E41, "m");	// LATIN SMALL LETTER M WITH DOT ABOVE
			conversionTable.Add((char)0x1E42, "M");	// LATIN CAPITAL LETTER M WITH DOT BELOW
			conversionTable.Add((char)0x1E43, "m");	// LATIN SMALL LETTER M WITH DOT BELOW
			conversionTable.Add((char)0x1E44, "N");	// LATIN CAPITAL LETTER N WITH DOT ABOVE
			conversionTable.Add((char)0x1E45, "n");	// LATIN SMALL LETTER N WITH DOT ABOVE
			conversionTable.Add((char)0x1E46, "N");	// LATIN CAPITAL LETTER N WITH DOT BELOW
			conversionTable.Add((char)0x1E47, "n");	// LATIN SMALL LETTER N WITH DOT BELOW
			conversionTable.Add((char)0x1E48, "N");	// LATIN CAPITAL LETTER N WITH LINE BELOW
			conversionTable.Add((char)0x1E49, "n");	// LATIN SMALL LETTER N WITH LINE BELOW
			conversionTable.Add((char)0x1E4A, "N");	// LATIN CAPITAL LETTER N WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E4B, "n");	// LATIN SMALL LETTER N WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E4C, "O");	// LATIN CAPITAL LETTER O WITH TILDE AND ACUTE
			conversionTable.Add((char)0x1E4D, "o");	// LATIN SMALL LETTER O WITH TILDE AND ACUTE
			conversionTable.Add((char)0x1E4E, "O");	// LATIN CAPITAL LETTER O WITH TILDE AND DIAERESIS
			conversionTable.Add((char)0x1E4F, "o");	// LATIN SMALL LETTER O WITH TILDE AND DIAERESIS
			conversionTable.Add((char)0x1E50, "O");	// LATIN CAPITAL LETTER O WITH MACRON AND GRAVE
			conversionTable.Add((char)0x1E51, "o");	// LATIN SMALL LETTER O WITH MACRON AND GRAVE
			conversionTable.Add((char)0x1E52, "O");	// LATIN CAPITAL LETTER O WITH MACRON AND ACUTE
			conversionTable.Add((char)0x1E53, "o");	// LATIN SMALL LETTER O WITH MACRON AND ACUTE
			conversionTable.Add((char)0x1E54, "P");	// LATIN CAPITAL LETTER P WITH ACUTE
			conversionTable.Add((char)0x1E55, "p");	// LATIN SMALL LETTER P WITH ACUTE
			conversionTable.Add((char)0x1E56, "P");	// LATIN CAPITAL LETTER P WITH DOT ABOVE
			conversionTable.Add((char)0x1E57, "p");	// LATIN SMALL LETTER P WITH DOT ABOVE
			conversionTable.Add((char)0x1E58, "R");	// LATIN CAPITAL LETTER R WITH DOT ABOVE
			conversionTable.Add((char)0x1E59, "r");	// LATIN SMALL LETTER R WITH DOT ABOVE
			conversionTable.Add((char)0x1E5A, "R");	// LATIN CAPITAL LETTER R WITH DOT BELOW
			conversionTable.Add((char)0x1E5B, "r");	// LATIN SMALL LETTER R WITH DOT BELOW
			conversionTable.Add((char)0x1E5C, "R");	// LATIN CAPITAL LETTER R WITH DOT BELOW AND MACRON
			conversionTable.Add((char)0x1E5D, "r");	// LATIN SMALL LETTER R WITH DOT BELOW AND MACRON
			conversionTable.Add((char)0x1E5E, "R");	// LATIN CAPITAL LETTER R WITH LINE BELOW
			conversionTable.Add((char)0x1E5F, "r");	// LATIN SMALL LETTER R WITH LINE BELOW
			conversionTable.Add((char)0x1E60, "S");	// LATIN CAPITAL LETTER S WITH DOT ABOVE
			conversionTable.Add((char)0x1E61, "s");	// LATIN SMALL LETTER S WITH DOT ABOVE
			conversionTable.Add((char)0x1E62, "S");	// LATIN CAPITAL LETTER S WITH DOT BELOW
			conversionTable.Add((char)0x1E63, "s");	// LATIN SMALL LETTER S WITH DOT BELOW
			conversionTable.Add((char)0x1E64, "S");	// LATIN CAPITAL LETTER S WITH ACUTE AND DOT ABOVE
			conversionTable.Add((char)0x1E65, "s");	// LATIN SMALL LETTER S WITH ACUTE AND DOT ABOVE
			conversionTable.Add((char)0x1E66, "S");	// LATIN CAPITAL LETTER S WITH CARON AND DOT ABOVE
			conversionTable.Add((char)0x1E67, "s");	// LATIN SMALL LETTER S WITH CARON AND DOT ABOVE
			conversionTable.Add((char)0x1E68, "S");	// LATIN CAPITAL LETTER S WITH DOT BELOW AND DOT ABOVE
			conversionTable.Add((char)0x1E69, "s");	// LATIN SMALL LETTER S WITH DOT BELOW AND DOT ABOVE
			conversionTable.Add((char)0x1E6A, "T");	// LATIN CAPITAL LETTER T WITH DOT ABOVE
			conversionTable.Add((char)0x1E6B, "t");	// LATIN SMALL LETTER T WITH DOT ABOVE
			conversionTable.Add((char)0x1E6C, "T");	// LATIN CAPITAL LETTER T WITH DOT BELOW
			conversionTable.Add((char)0x1E6D, "t");	// LATIN SMALL LETTER T WITH DOT BELOW
			conversionTable.Add((char)0x1E6E, "T");	// LATIN CAPITAL LETTER T WITH LINE BELOW
			conversionTable.Add((char)0x1E6F, "t");	// LATIN SMALL LETTER T WITH LINE BELOW
			conversionTable.Add((char)0x1E70, "T");	// LATIN CAPITAL LETTER T WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E71, "t");	// LATIN SMALL LETTER T WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E72, "U");	// LATIN CAPITAL LETTER U WITH DIAERESIS BELOW
			conversionTable.Add((char)0x1E73, "u");	// LATIN SMALL LETTER U WITH DIAERESIS BELOW
			conversionTable.Add((char)0x1E74, "U");	// LATIN CAPITAL LETTER U WITH TILDE BELOW
			conversionTable.Add((char)0x1E75, "u");	// LATIN SMALL LETTER U WITH TILDE BELOW
			conversionTable.Add((char)0x1E76, "U");	// LATIN CAPITAL LETTER U WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E77, "u");	// LATIN SMALL LETTER U WITH CIRCUMFLEX BELOW
			conversionTable.Add((char)0x1E78, "U");	// LATIN CAPITAL LETTER U WITH TILDE AND ACUTE
			conversionTable.Add((char)0x1E79, "u");	// LATIN SMALL LETTER U WITH TILDE AND ACUTE
			conversionTable.Add((char)0x1E7A, "U");	// LATIN CAPITAL LETTER U WITH MACRON AND DIAERESIS
			conversionTable.Add((char)0x1E7B, "u");	// LATIN SMALL LETTER U WITH MACRON AND DIAERESIS
			conversionTable.Add((char)0x1E7C, "V");	// LATIN CAPITAL LETTER V WITH TILDE
			conversionTable.Add((char)0x1E7D, "v");	// LATIN SMALL LETTER V WITH TILDE
			conversionTable.Add((char)0x1E7E, "V");	// LATIN CAPITAL LETTER V WITH DOT BELOW
			conversionTable.Add((char)0x1E7F, "v");	// LATIN SMALL LETTER V WITH DOT BELOW
			conversionTable.Add((char)0x1E80, "W");	// LATIN CAPITAL LETTER W WITH GRAVE
			conversionTable.Add((char)0x1E81, "w");	// LATIN SMALL LETTER W WITH GRAVE
			conversionTable.Add((char)0x1E82, "W");	// LATIN CAPITAL LETTER W WITH ACUTE
			conversionTable.Add((char)0x1E83, "w");	// LATIN SMALL LETTER W WITH ACUTE
			conversionTable.Add((char)0x1E84, "W");	// LATIN CAPITAL LETTER W WITH DIAERESIS
			conversionTable.Add((char)0x1E85, "w");	// LATIN SMALL LETTER W WITH DIAERESIS
			conversionTable.Add((char)0x1E86, "W");	// LATIN CAPITAL LETTER W WITH DOT ABOVE
			conversionTable.Add((char)0x1E87, "w");	// LATIN SMALL LETTER W WITH DOT ABOVE
			conversionTable.Add((char)0x1E88, "W");	// LATIN CAPITAL LETTER W WITH DOT BELOW
			conversionTable.Add((char)0x1E89, "w");	// LATIN SMALL LETTER W WITH DOT BELOW
			conversionTable.Add((char)0x1E8A, "X");	// LATIN CAPITAL LETTER X WITH DOT ABOVE
			conversionTable.Add((char)0x1E8B, "x");	// LATIN SMALL LETTER X WITH DOT ABOVE
			conversionTable.Add((char)0x1E8C, "X");	// LATIN CAPITAL LETTER X WITH DIAERESIS
			conversionTable.Add((char)0x1E8D, "x");	// LATIN SMALL LETTER X WITH DIAERESIS
			conversionTable.Add((char)0x1E8E, "Y");	// LATIN CAPITAL LETTER Y WITH DOT ABOVE
			conversionTable.Add((char)0x1E8F, "y");	// LATIN SMALL LETTER Y WITH DOT ABOVE
			conversionTable.Add((char)0x1E90, "Z");	// LATIN CAPITAL LETTER Z WITH CIRCUMFLEX
			conversionTable.Add((char)0x1E91, "z");	// LATIN SMALL LETTER Z WITH CIRCUMFLEX
			conversionTable.Add((char)0x1E92, "Z");	// LATIN CAPITAL LETTER Z WITH DOT BELOW
			conversionTable.Add((char)0x1E93, "z");	// LATIN SMALL LETTER Z WITH DOT BELOW
			conversionTable.Add((char)0x1E94, "Z");	// LATIN CAPITAL LETTER Z WITH LINE BELOW
			conversionTable.Add((char)0x1E95, "z");	// LATIN SMALL LETTER Z WITH LINE BELOW
			conversionTable.Add((char)0x1E96, "h");	// LATIN SMALL LETTER H WITH LINE BELOW
			conversionTable.Add((char)0x1E97, "t");	// LATIN SMALL LETTER T WITH DIAERESIS
			conversionTable.Add((char)0x1E98, "w");	// LATIN SMALL LETTER W WITH RING ABOVE
			conversionTable.Add((char)0x1E99, "y");	// LATIN SMALL LETTER Y WITH RING ABOVE
			conversionTable.Add((char)0x1E9A, "a");	// LATIN SMALL LETTER A WITH RIGHT HALF RING
			conversionTable.Add((char)0x1E9B, "s");	// LATIN SMALL LETTER LONG S WITH DOT ABOVE
			conversionTable.Add((char)0x1EA0, "A");	// LATIN CAPITAL LETTER A WITH DOT BELOW
			conversionTable.Add((char)0x1EA1, "a");	// LATIN SMALL LETTER A WITH DOT BELOW
			conversionTable.Add((char)0x1EA2, "A");	// LATIN CAPITAL LETTER A WITH HOOK ABOVE
			conversionTable.Add((char)0x1EA3, "a");	// LATIN SMALL LETTER A WITH HOOK ABOVE
			conversionTable.Add((char)0x1EA4, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1EA5, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1EA6, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1EA7, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1EA8, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1EA9, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1EAA, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1EAB, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1EAC, "A");	// LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1EAD, "a");	// LATIN SMALL LETTER A WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1EAE, "A");	// LATIN CAPITAL LETTER A WITH BREVE AND ACUTE
			conversionTable.Add((char)0x1EAF, "a");	// LATIN SMALL LETTER A WITH BREVE AND ACUTE
			conversionTable.Add((char)0x1EB0, "A");	// LATIN CAPITAL LETTER A WITH BREVE AND GRAVE
			conversionTable.Add((char)0x1EB1, "a");	// LATIN SMALL LETTER A WITH BREVE AND GRAVE
			conversionTable.Add((char)0x1EB2, "A");	// LATIN CAPITAL LETTER A WITH BREVE AND HOOK ABOVE
			conversionTable.Add((char)0x1EB3, "a");	// LATIN SMALL LETTER A WITH BREVE AND HOOK ABOVE
			conversionTable.Add((char)0x1EB4, "A");	// LATIN CAPITAL LETTER A WITH BREVE AND TILDE
			conversionTable.Add((char)0x1EB5, "a");	// LATIN SMALL LETTER A WITH BREVE AND TILDE
			conversionTable.Add((char)0x1EB6, "A");	// LATIN CAPITAL LETTER A WITH BREVE AND DOT BELOW
			conversionTable.Add((char)0x1EB7, "a");	// LATIN SMALL LETTER A WITH BREVE AND DOT BELOW
			conversionTable.Add((char)0x1EB8, "E");	// LATIN CAPITAL LETTER E WITH DOT BELOW
			conversionTable.Add((char)0x1EB9, "e");	// LATIN SMALL LETTER E WITH DOT BELOW
			conversionTable.Add((char)0x1EBA, "E");	// LATIN CAPITAL LETTER E WITH HOOK ABOVE
			conversionTable.Add((char)0x1EBB, "e");	// LATIN SMALL LETTER E WITH HOOK ABOVE
			conversionTable.Add((char)0x1EBC, "E");	// LATIN CAPITAL LETTER E WITH TILDE
			conversionTable.Add((char)0x1EBD, "e");	// LATIN SMALL LETTER E WITH TILDE
			conversionTable.Add((char)0x1EBE, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1EBF, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1EC0, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1EC1, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1EC2, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1EC3, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1EC4, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1EC5, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1EC6, "E");	// LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1EC7, "e");	// LATIN SMALL LETTER E WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1EC8, "I");	// LATIN CAPITAL LETTER I WITH HOOK ABOVE
			conversionTable.Add((char)0x1EC9, "i");	// LATIN SMALL LETTER I WITH HOOK ABOVE
			conversionTable.Add((char)0x1ECA, "I");	// LATIN CAPITAL LETTER I WITH DOT BELOW
			conversionTable.Add((char)0x1ECB, "i");	// LATIN SMALL LETTER I WITH DOT BELOW
			conversionTable.Add((char)0x1ECC, "O");	// LATIN CAPITAL LETTER O WITH DOT BELOW
			conversionTable.Add((char)0x1ECD, "o");	// LATIN SMALL LETTER O WITH DOT BELOW
			conversionTable.Add((char)0x1ECE, "O");	// LATIN CAPITAL LETTER O WITH HOOK ABOVE
			conversionTable.Add((char)0x1ECF, "o");	// LATIN SMALL LETTER O WITH HOOK ABOVE
			conversionTable.Add((char)0x1ED0, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1ED1, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX AND ACUTE
			conversionTable.Add((char)0x1ED2, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1ED3, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX AND GRAVE
			conversionTable.Add((char)0x1ED4, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1ED5, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE
			conversionTable.Add((char)0x1ED6, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1ED7, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX AND TILDE
			conversionTable.Add((char)0x1ED8, "O");	// LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1ED9, "o");	// LATIN SMALL LETTER O WITH CIRCUMFLEX AND DOT BELOW
			conversionTable.Add((char)0x1EDA, "O");	// LATIN CAPITAL LETTER O WITH HORN AND ACUTE
			conversionTable.Add((char)0x1EDB, "o");	// LATIN SMALL LETTER O WITH HORN AND ACUTE
			conversionTable.Add((char)0x1EDC, "O");	// LATIN CAPITAL LETTER O WITH HORN AND GRAVE
			conversionTable.Add((char)0x1EDD, "o");	// LATIN SMALL LETTER O WITH HORN AND GRAVE
			conversionTable.Add((char)0x1EDE, "O");	// LATIN CAPITAL LETTER O WITH HORN AND HOOK ABOVE
			conversionTable.Add((char)0x1EDF, "o");	// LATIN SMALL LETTER O WITH HORN AND HOOK ABOVE
			conversionTable.Add((char)0x1EE0, "O");	// LATIN CAPITAL LETTER O WITH HORN AND TILDE
			conversionTable.Add((char)0x1EE1, "o");	// LATIN SMALL LETTER O WITH HORN AND TILDE
			conversionTable.Add((char)0x1EE2, "O");	// LATIN CAPITAL LETTER O WITH HORN AND DOT BELOW
			conversionTable.Add((char)0x1EE3, "o");	// LATIN SMALL LETTER O WITH HORN AND DOT BELOW
			conversionTable.Add((char)0x1EE4, "U");	// LATIN CAPITAL LETTER U WITH DOT BELOW
			conversionTable.Add((char)0x1EE5, "u");	// LATIN SMALL LETTER U WITH DOT BELOW
			conversionTable.Add((char)0x1EE6, "U");	// LATIN CAPITAL LETTER U WITH HOOK ABOVE
			conversionTable.Add((char)0x1EE7, "u");	// LATIN SMALL LETTER U WITH HOOK ABOVE
			conversionTable.Add((char)0x1EE8, "U");	// LATIN CAPITAL LETTER U WITH HORN AND ACUTE
			conversionTable.Add((char)0x1EE9, "u");	// LATIN SMALL LETTER U WITH HORN AND ACUTE
			conversionTable.Add((char)0x1EEA, "U");	// LATIN CAPITAL LETTER U WITH HORN AND GRAVE
			conversionTable.Add((char)0x1EEB, "u");	// LATIN SMALL LETTER U WITH HORN AND GRAVE
			conversionTable.Add((char)0x1EEC, "U");	// LATIN CAPITAL LETTER U WITH HORN AND HOOK ABOVE
			conversionTable.Add((char)0x1EED, "u");	// LATIN SMALL LETTER U WITH HORN AND HOOK ABOVE
			conversionTable.Add((char)0x1EEE, "U");	// LATIN CAPITAL LETTER U WITH HORN AND TILDE
			conversionTable.Add((char)0x1EEF, "u");	// LATIN SMALL LETTER U WITH HORN AND TILDE
			conversionTable.Add((char)0x1EF0, "U");	// LATIN CAPITAL LETTER U WITH HORN AND DOT BELOW
			conversionTable.Add((char)0x1EF1, "u");	// LATIN SMALL LETTER U WITH HORN AND DOT BELOW
			conversionTable.Add((char)0x1EF2, "Y");	// LATIN CAPITAL LETTER Y WITH GRAVE
			conversionTable.Add((char)0x1EF3, "y");	// LATIN SMALL LETTER Y WITH GRAVE
			conversionTable.Add((char)0x1EF4, "Y");	// LATIN CAPITAL LETTER Y WITH DOT BELOW
			conversionTable.Add((char)0x1EF5, "y");	// LATIN SMALL LETTER Y WITH DOT BELOW
			conversionTable.Add((char)0x1EF6, "Y");	// LATIN CAPITAL LETTER Y WITH HOOK ABOVE
			conversionTable.Add((char)0x1EF7, "y");	// LATIN SMALL LETTER Y WITH HOOK ABOVE
			conversionTable.Add((char)0x1EF8, "Y");	// LATIN CAPITAL LETTER Y WITH TILDE
			conversionTable.Add((char)0x1EF9, "y");	// LATIN SMALL LETTER Y WITH TILDE
			conversionTable.Add((char)0x2071, "i");	// SUPERSCRIPT LATIN SMALL LETTER I
			conversionTable.Add((char)0x207F, "n");	// SUPERSCRIPT LATIN SMALL LETTER N
			conversionTable.Add((char)0x212A, "K");	// KELVIN SIGN
			conversionTable.Add((char)0x212B, "A");	// ANGSTROM SIGN
			conversionTable.Add((char)0x212C, "B");	// SCRIPT CAPITAL B
			conversionTable.Add((char)0x212D, "C");	// BLACK-LETTER CAPITAL C
			conversionTable.Add((char)0x212F, "e");	// SCRIPT SMALL E
			conversionTable.Add((char)0x2130, "E");	// SCRIPT CAPITAL E
			conversionTable.Add((char)0x2131, "F");	// SCRIPT CAPITAL F
			conversionTable.Add((char)0x2132, "F");	// TURNED CAPITAL F -- no decomposition
			conversionTable.Add((char)0x2133, "M");	// SCRIPT CAPITAL M
			conversionTable.Add((char)0x2134, "0");	// SCRIPT SMALL O
			conversionTable.Add((char)0x213A, "0");	// ROTATED CAPITAL Q -- no decomposition
			conversionTable.Add((char)0x2141, "G");	// TURNED SANS-SERIF CAPITAL G -- no decomposition
			conversionTable.Add((char)0x2142, "L");	// TURNED SANS-SERIF CAPITAL L -- no decomposition
			conversionTable.Add((char)0x2143, "L");	// REVERSED SANS-SERIF CAPITAL L -- no decomposition
			conversionTable.Add((char)0x2144, "Y");	// TURNED SANS-SERIF CAPITAL Y -- no decomposition
			conversionTable.Add((char)0x2145, "D");	// DOUBLE-STRUCK ITALIC CAPITAL D
			conversionTable.Add((char)0x2146, "d");	// DOUBLE-STRUCK ITALIC SMALL D
			conversionTable.Add((char)0x2147, "e");	// DOUBLE-STRUCK ITALIC SMALL E
			conversionTable.Add((char)0x2148, "i");	// DOUBLE-STRUCK ITALIC SMALL I
			conversionTable.Add((char)0x2149, "j");	// DOUBLE-STRUCK ITALIC SMALL J
			conversionTable.Add((char)0xFB00, "ff");	// LATIN SMALL LIGATURE FF
			conversionTable.Add((char)0xFB01, "fi");	// LATIN SMALL LIGATURE FI
			conversionTable.Add((char)0xFB02, "fl");	// LATIN SMALL LIGATURE FL
			conversionTable.Add((char)0xFB03, "ffi");	// LATIN SMALL LIGATURE FFI
			conversionTable.Add((char)0xFB04, "ffl");	// LATIN SMALL LIGATURE FFL
			conversionTable.Add((char)0xFB05, "st");	// LATIN SMALL LIGATURE LONG S T
			conversionTable.Add((char)0xFB06, "st");	// LATIN SMALL LIGATURE ST
			conversionTable.Add((char)0xFF21, "A");	// FULLWIDTH LATIN CAPITAL LETTER B
			conversionTable.Add((char)0xFF22, "B");	// FULLWIDTH LATIN CAPITAL LETTER B
			conversionTable.Add((char)0xFF23, "C");	// FULLWIDTH LATIN CAPITAL LETTER C
			conversionTable.Add((char)0xFF24, "D");	// FULLWIDTH LATIN CAPITAL LETTER D
			conversionTable.Add((char)0xFF25, "E");	// FULLWIDTH LATIN CAPITAL LETTER E
			conversionTable.Add((char)0xFF26, "F");	// FULLWIDTH LATIN CAPITAL LETTER F
			conversionTable.Add((char)0xFF27, "G");	// FULLWIDTH LATIN CAPITAL LETTER G
			conversionTable.Add((char)0xFF28, "H");	// FULLWIDTH LATIN CAPITAL LETTER H
			conversionTable.Add((char)0xFF29, "I");	// FULLWIDTH LATIN CAPITAL LETTER I
			conversionTable.Add((char)0xFF2A, "J");	// FULLWIDTH LATIN CAPITAL LETTER J
			conversionTable.Add((char)0xFF2B, "K");	// FULLWIDTH LATIN CAPITAL LETTER K
			conversionTable.Add((char)0xFF2C, "L");	// FULLWIDTH LATIN CAPITAL LETTER L
			conversionTable.Add((char)0xFF2D, "M");	// FULLWIDTH LATIN CAPITAL LETTER M
			conversionTable.Add((char)0xFF2E, "N");	// FULLWIDTH LATIN CAPITAL LETTER N
			conversionTable.Add((char)0xFF2F, "O");	// FULLWIDTH LATIN CAPITAL LETTER O
			conversionTable.Add((char)0xFF30, "P");	// FULLWIDTH LATIN CAPITAL LETTER P
			conversionTable.Add((char)0xFF31, "Q");	// FULLWIDTH LATIN CAPITAL LETTER Q
			conversionTable.Add((char)0xFF32, "R");	// FULLWIDTH LATIN CAPITAL LETTER R
			conversionTable.Add((char)0xFF33, "S");	// FULLWIDTH LATIN CAPITAL LETTER S
			conversionTable.Add((char)0xFF34, "T");	// FULLWIDTH LATIN CAPITAL LETTER T
			conversionTable.Add((char)0xFF35, "U");	// FULLWIDTH LATIN CAPITAL LETTER U
			conversionTable.Add((char)0xFF36, "V");	// FULLWIDTH LATIN CAPITAL LETTER V
			conversionTable.Add((char)0xFF37, "W");	// FULLWIDTH LATIN CAPITAL LETTER W
			conversionTable.Add((char)0xFF38, "X");	// FULLWIDTH LATIN CAPITAL LETTER X
			conversionTable.Add((char)0xFF39, "Y");	// FULLWIDTH LATIN CAPITAL LETTER Y
			conversionTable.Add((char)0xFF3A, "Z");	// FULLWIDTH LATIN CAPITAL LETTER Z
			conversionTable.Add((char)0xFF41, "a");	// FULLWIDTH LATIN SMALL LETTER A
			conversionTable.Add((char)0xFF42, "b");	// FULLWIDTH LATIN SMALL LETTER B
			conversionTable.Add((char)0xFF43, "c");	// FULLWIDTH LATIN SMALL LETTER C
			conversionTable.Add((char)0xFF44, "d");	// FULLWIDTH LATIN SMALL LETTER D
			conversionTable.Add((char)0xFF45, "e");	// FULLWIDTH LATIN SMALL LETTER E
			conversionTable.Add((char)0xFF46, "f");	// FULLWIDTH LATIN SMALL LETTER F
			conversionTable.Add((char)0xFF47, "g");	// FULLWIDTH LATIN SMALL LETTER G
			conversionTable.Add((char)0xFF48, "h");	// FULLWIDTH LATIN SMALL LETTER H
			conversionTable.Add((char)0xFF49, "i");	// FULLWIDTH LATIN SMALL LETTER I
			conversionTable.Add((char)0xFF4A, "j");	// FULLWIDTH LATIN SMALL LETTER J
			conversionTable.Add((char)0xFF4B, "k");	// FULLWIDTH LATIN SMALL LETTER K
			conversionTable.Add((char)0xFF4C, "l");	// FULLWIDTH LATIN SMALL LETTER L
			conversionTable.Add((char)0xFF4D, "m");	// FULLWIDTH LATIN SMALL LETTER M
			conversionTable.Add((char)0xFF4E, "n");	// FULLWIDTH LATIN SMALL LETTER N
			conversionTable.Add((char)0xFF4F, "o");	// FULLWIDTH LATIN SMALL LETTER O
			conversionTable.Add((char)0xFF50, "p");	// FULLWIDTH LATIN SMALL LETTER P
			conversionTable.Add((char)0xFF51, "q");	// FULLWIDTH LATIN SMALL LETTER Q
			conversionTable.Add((char)0xFF52, "r");	// FULLWIDTH LATIN SMALL LETTER R
			conversionTable.Add((char)0xFF53, "s");	// FULLWIDTH LATIN SMALL LETTER S
			conversionTable.Add((char)0xFF54, "t");	// FULLWIDTH LATIN SMALL LETTER T
			conversionTable.Add((char)0xFF55, "u");	// FULLWIDTH LATIN SMALL LETTER U
			conversionTable.Add((char)0xFF56, "v");	// FULLWIDTH LATIN SMALL LETTER V
			conversionTable.Add((char)0xFF57, "w");	// FULLWIDTH LATIN SMALL LETTER W
			conversionTable.Add((char)0xFF58, "x");	// FULLWIDTH LATIN SMALL LETTER X
			conversionTable.Add((char)0xFF59, "y");	// FULLWIDTH LATIN SMALL LETTER Y
			conversionTable.Add((char)0xFF5A, "z");	// FULLWIDTH LATIN SMALL LETTER Z
		} // end addValues
	}

}
