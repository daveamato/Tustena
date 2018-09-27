/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function wordWindow() { this._forms = []; 
this._getWordObject = _getWordObject; this._wordInputStr = _wordInputStr; this._adjustIndexes = _adjustIndexes; this._isWordChar = _isWordChar; this._lastPos = _lastPos; 
this.wordChar = /[a-zA-Z]/; this.windowType = "wordWindow"; this.originalSpellings = new Array(); this.suggestions = new Array(); this.checkWordBgColor = "pink"; this.normWordBgColor = "white"; this.text = ""; this.textInputs = new Array(); this.indexes = new Array(); 
this.resetForm = resetForm; this.totalMisspellings = totalMisspellings; this.totalWords = totalWords; this.totalPreviousWords = totalPreviousWords; this.getTextVal = getTextVal; this.setFocus = setFocus; this.removeFocus = removeFocus; this.setText = setText; this.writeBody = writeBody; this.printForHtml = printForHtml; }

function resetForm() { if( this._forms ) { for( var i = 0; i < this._forms.length; i++ ) { this._forms[i].reset(); }
}
return true; }

function totalMisspellings() { var total_words = 0; for( var i = 0; i < this.textInputs.length; i++ ) { total_words += this.totalWords( i ); }
return total_words; }

function totalWords( textIndex ) { return this.originalSpellings[textIndex].length; }

function totalPreviousWords( textIndex, wordIndex ) { var total_words = 0; for( var i = 0; i <= textIndex; i++ ) { for( var j = 0; j < this.totalWords( i ); j++ ) { if( i == textIndex && j == wordIndex ) { break; } else { total_words++; }
}
}
return total_words; }


function getTextVal( textIndex, wordIndex ) { var word = this._getWordObject( textIndex, wordIndex ); if( word ) { return word.value; }
}

function setFocus( textIndex, wordIndex ) { var word = this._getWordObject( textIndex, wordIndex ); if( word ) { if( word.type == "text" ) { word.focus(); word.style.backgroundColor = this.checkWordBgColor; }
}
}

function removeFocus( textIndex, wordIndex ) { var word = this._getWordObject( textIndex, wordIndex ); if( word ) { if( word.type == "text" ) { word.blur(); word.style.backgroundColor = this.normWordBgColor; }
}
}

function setText( textIndex, wordIndex, newText ) { var word = this._getWordObject( textIndex, wordIndex ); var beginStr; var endStr; if( word ) { var pos = this.indexes[textIndex][wordIndex]; var oldText = word.value; beginStr = this.textInputs[textIndex].substring( 0, pos ); endStr = this.textInputs[textIndex].substring(
pos + oldText.length,
this.textInputs[textIndex].length
); this.textInputs[textIndex] = beginStr + newText + endStr; 
var lengthDiff = newText.length - oldText.length; this._adjustIndexes( textIndex, wordIndex, lengthDiff ); 
word.size = newText.length; word.value = newText; this.removeFocus( textIndex, wordIndex ); }
}


function writeBody() { var d = window.document; var is_html = false; 
d.open(); 
for( var txtid = 0; txtid < this.textInputs.length; txtid++ ) { var end_idx = 0; var begin_idx = 0; d.writeln( '<form name="textInput'+txtid+'">' ); var wordtxt = this.textInputs[txtid]; this.indexes[txtid] = []; 
if( wordtxt ) { var orig = this.originalSpellings[txtid]; if( !orig ) break; 
d.writeln( '<div class="plainText">' ); for( var i = 0; i < orig.length; i++ ) { do { begin_idx = wordtxt.indexOf( orig[i], end_idx ); end_idx = begin_idx + orig[i].length; if( begin_idx == -1 ) break; var before_char = wordtxt.charAt( begin_idx - 1 ); var after_char = wordtxt.charAt( end_idx ); } while (
this._isWordChar( before_char )
|| this._isWordChar( after_char )
); 
this.indexes[txtid][i] = begin_idx; 
for( var j = this._lastPos( txtid, i ); j < begin_idx; j++ ) { d.write( this.printForHtml( wordtxt.charAt( j ))); }

d.write( this._wordInputStr( orig[i] )); 
if( i == orig.length-1 ){ d.write( printForHtml( wordtxt.substr( end_idx ))); }
}

d.writeln( '</div>' ); 
}
d.writeln( '</form>' ); }

this._forms = d.forms; d.close(); }

function _lastPos( txtid, idx ) { if( idx > 0 )
return this.indexes[txtid][idx-1] + this.originalSpellings[txtid][idx-1].length; else
return 0; }

function printForHtml( n ) { return n ; 
var htmlstr = n; if( htmlstr.length == 1 ) { switch ( n ) { case "\n":
htmlstr = '<br/>'; break; case "<":
htmlstr = '&lt;'; break; case ">":
htmlstr = '&gt;'; break; }
return htmlstr; } else { htmlstr = htmlstr.replace( /</g, '&lt' ); htmlstr = htmlstr.replace( />/g, '&gt' ); htmlstr = htmlstr.replace( /\n/g, '<br/>' ); return htmlstr; }
}

function _isWordChar( letter ) { if( letter.search( this.wordChar ) == -1 ) { return false; } else { return true; }
}

function _getWordObject( textIndex, wordIndex ) { if( this._forms[textIndex] ) { if( this._forms[textIndex].elements[wordIndex] ) { return this._forms[textIndex].elements[wordIndex]; }
}
return null; }

function _wordInputStr( word ) { var str = '<input readonly '; str += 'class="blend" type="text" value="' + word + '" size="' + word.length + '">'; return str; }

function _adjustIndexes( textIndex, wordIndex, lengthDiff ) { for( var i = wordIndex + 1; i < this.originalSpellings[textIndex].length; i++ ) { this.indexes[textIndex][i] = this.indexes[textIndex][i] + lengthDiff; }
}
