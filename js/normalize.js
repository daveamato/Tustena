/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function NormalizeName(fieldtxt) { var pattern = /(\w)(\S*)/; var a = fieldtxt.value.split(/\s+/g); for (i = 0 ; i < a.length ; i ++ ) { var parts = a[i].match(pattern); var firstLetter = parts[1].toUpperCase(); var restOfWord = parts[2].toLowerCase(); a[i] = firstLetter + restOfWord; }
fieldtxt.value = a.join(' '); }
