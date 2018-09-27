/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */



function spellChecker( textObject ) { 
this.popUpUrl = 'fck_spellerpages/spellerpages/spellchecker.html'; this.popUpName = 'spellchecker'; this.popUpProps = null ; this.spellCheckScript = 'server-scripts/spellchecker.php'; 
this.replWordFlag = "R"; this.ignrWordFlag = "I"; this.replAllFlag = "RA"; this.ignrAllFlag = "IA"; this.fromReplAll = "~RA";	// an occurance of a "replace all" word
this.fromIgnrAll = "~IA";	// an occurance of a "ignore all" word
this.wordFlags = new Array(); this.currentTextIndex = 0; this.currentWordIndex = 0; this.spellCheckerWin = null; this.controlWin = null; this.wordWin = null; this.textArea = textObject; this.textInputs = arguments; 
this._spellcheck = _spellcheck; this._getSuggestions = _getSuggestions; this._setAsIgnored = _setAsIgnored; this._getTotalReplaced = _getTotalReplaced; this._setWordText = _setWordText; this._getFormInputs = _getFormInputs; 
this.openChecker = openChecker; this.startCheck = startCheck; this.checkTextBoxes = checkTextBoxes; this.checkTextAreas = checkTextAreas; this.spellCheckAll = spellCheckAll; this.ignoreWord = ignoreWord; this.ignoreAll = ignoreAll; this.replaceWord = replaceWord; this.replaceAll = replaceAll; this.terminateSpell = terminateSpell; this.undo = undo; 
window.speller = this; }

function checkTextBoxes() { this.textInputs = this._getFormInputs( "^text$" ); this.openChecker(); }

function checkTextAreas() { this.textInputs = this._getFormInputs( "^textarea$" ); this.openChecker(); }

function spellCheckAll() { this.textInputs = this._getFormInputs( "^text(area)?$" ); this.openChecker(); }

function openChecker() { this.spellCheckerWin = window.open( this.popUpUrl, this.popUpName, this.popUpProps ); if( !this.spellCheckerWin.opener ) { this.spellCheckerWin.opener = window; }
}

function startCheck( wordWindowObj, controlWindowObj ) { 
this.wordWin = wordWindowObj; this.controlWin = controlWindowObj; 
this.wordWin.resetForm(); this.controlWin.resetForm(); this.currentTextIndex = 0; this.currentWordIndex = 0; this.wordFlags = new Array( this.wordWin.textInputs.length ); for( var i=0; i<this.wordFlags.length; i++ ) { this.wordFlags[i] = []; }

this._spellcheck(); 
return true; }

function ignoreWord() { var wi = this.currentWordIndex; var ti = this.currentTextIndex; if( !this.wordWin ) { alert( 'Error: Word frame not available.' ); return false; }
if( !this.wordWin.getTextVal( ti, wi )) { alert( 'Error: "Not in dictionary" text is missing.' ); return false; }
if( this._setAsIgnored( ti, wi, this.ignrWordFlag )) { this.currentWordIndex++; this._spellcheck(); }
}

function ignoreAll() { var wi = this.currentWordIndex; var ti = this.currentTextIndex; if( !this.wordWin ) { alert( 'Error: Word frame not available.' ); return false; }
var s_word_to_repl = this.wordWin.getTextVal( ti, wi ); if( !s_word_to_repl ) { alert( 'Error: "Not in dictionary" text is missing' ); return false; }

this._setAsIgnored( ti, wi, this.ignrAllFlag ); 
for( var i = ti; i < this.wordWin.textInputs.length; i++ ) { for( var j = 0; j < this.wordWin.totalWords( i ); j++ ) { if(( i == ti && j > wi ) || i > ti ) { if(( this.wordWin.getTextVal( i, j ) == s_word_to_repl )
&& ( !this.wordFlags[i][j] )) { this._setAsIgnored( i, j, this.fromIgnrAll ); }
}
}
}

this.currentWordIndex++; this._spellcheck(); }

function replaceWord() { var wi = this.currentWordIndex; var ti = this.currentTextIndex; if( !this.wordWin ) { alert( 'Error: Word frame not available.' ); return false; }
if( !this.wordWin.getTextVal( ti, wi )) { alert( 'Error: "Not in dictionary" text is missing' ); return false; }
if( !this.controlWin.replacementText ) { return; }
var txt = this.controlWin.replacementText; if( txt.value ) { var newspell = new String( txt.value ); if( this._setWordText( ti, wi, newspell, this.replWordFlag )) { this.currentWordIndex++; this._spellcheck(); }
}
}

function replaceAll() { var ti = this.currentTextIndex; var wi = this.currentWordIndex; if( !this.wordWin ) { alert( 'Error: Word frame not available.' ); return false; }
var s_word_to_repl = this.wordWin.getTextVal( ti, wi ); if( !s_word_to_repl ) { alert( 'Error: "Not in dictionary" text is missing' ); return false; }
var txt = this.controlWin.replacementText; if( !txt.value ) return; var newspell = new String( txt.value ); 
this._setWordText( ti, wi, newspell, this.replAllFlag ); 
for( var i = ti; i < this.wordWin.textInputs.length; i++ ) { for( var j = 0; j < this.wordWin.totalWords( i ); j++ ) { if(( i == ti && j > wi ) || i > ti ) { if(( this.wordWin.getTextVal( i, j ) == s_word_to_repl )
&& ( !this.wordFlags[i][j] )) { this._setWordText( i, j, newspell, this.fromReplAll ); }
}
}
}

this.currentWordIndex++; this._spellcheck(); }

function terminateSpell() { var msg = ""; var numrepl = this._getTotalReplaced(); if( numrepl == 0 ) { if( !this.wordWin ) { msg = ""; } else { if( this.wordWin.totalMisspellings() ) { msg += FCKLang.DlgSpellNoChanges ; } else { msg += FCKLang.DlgSpellNoMispell ; }
}
} else if( numrepl == 1 ) { msg += FCKLang.DlgSpellOneChange ; } else { msg += FCKLang.DlgSpellManyChanges.replace( /%1/g, numrepl ) ; }
if( msg ) { alert( msg ); }

if( numrepl > 0 ) { for( var i = 0; i < this.textInputs.length; i++ ) { if( this.wordWin ) { if( this.wordWin.textInputs[i] ) { this.textInputs[i].value = this.wordWin.textInputs[i]; }
}
}
}

if ( typeof( this.OnFinished ) == 'function' )
this.OnFinished(numrepl) ; 
return true; }

function undo() { var ti = this.currentTextIndex; var wi = this.currentWordIndex

if( this.wordWin.totalPreviousWords( ti, wi ) > 0 ) { this.wordWin.removeFocus( ti, wi ); 
do { if( this.currentWordIndex == 0 && this.currentTextIndex > 0 ) { this.currentTextIndex--; this.currentWordIndex = this.wordWin.totalWords( this.currentTextIndex )-1; if( this.currentWordIndex < 0 ) this.currentWordIndex = 0; } else { if( this.currentWordIndex > 0 ) { this.currentWordIndex--; }
}
} while (
this.wordWin.totalWords( this.currentTextIndex ) == 0
|| this.wordFlags[this.currentTextIndex][this.currentWordIndex] == this.fromIgnrAll
|| this.wordFlags[this.currentTextIndex][this.currentWordIndex] == this.fromReplAll
); 
var text_idx = this.currentTextIndex; var idx = this.currentWordIndex; var preReplSpell = this.wordWin.originalSpellings[text_idx][idx]; 
if( this.wordWin.totalPreviousWords( text_idx, idx ) == 0 ) { this.controlWin.disableUndo(); }

switch( this.wordFlags[text_idx][idx] ) { case this.replAllFlag :
for( var i = text_idx; i < this.wordWin.textInputs.length; i++ ) { for( var j = 0; j < this.wordWin.totalWords( i ); j++ ) { if(( i == text_idx && j >= idx ) || i > text_idx ) { var origSpell = this.wordWin.originalSpellings[i][j]; if( origSpell == preReplSpell ) { this._setWordText ( i, j, origSpell, undefined ); }
}
}
}
break; 
case this.ignrAllFlag :
for( var i = text_idx; i < this.wordWin.textInputs.length; i++ ) { for( var j = 0; j < this.wordWin.totalWords( i ); j++ ) { if(( i == text_idx && j >= idx ) || i > text_idx ) { var origSpell = this.wordWin.originalSpellings[i][j]; if( origSpell == preReplSpell ) { this.wordFlags[i][j] = undefined; }
}
}
}
break; 
case this.replWordFlag :
this._setWordText ( text_idx, idx, preReplSpell, undefined ); break; }

this.wordFlags[text_idx][idx] = undefined; this._spellcheck(); }
}

function _spellcheck() { var ww = this.wordWin; 
if( this.currentWordIndex == ww.totalWords( this.currentTextIndex) ) { this.currentTextIndex++; this.currentWordIndex = 0; if( this.currentTextIndex < this.wordWin.textInputs.length ) { this._spellcheck(); return; } else { this.terminateSpell(); return; }
}

if( this.currentWordIndex > 0 ) { this.controlWin.enableUndo(); }

if( this.wordFlags[this.currentTextIndex][this.currentWordIndex] ) { this.currentWordIndex++; this._spellcheck(); } else { var evalText = ww.getTextVal( this.currentTextIndex, this.currentWordIndex ); if( evalText ) { this.controlWin.evaluatedText.value = evalText; ww.setFocus( this.currentTextIndex, this.currentWordIndex ); this._getSuggestions( this.currentTextIndex, this.currentWordIndex ); }
}
}

function _getSuggestions( text_num, word_num ) { this.controlWin.clearSuggestions(); var a_suggests = this.wordWin.suggestions[text_num][word_num]; if( a_suggests ) { for( var ii = 0; ii < a_suggests.length; ii++ ) { this.controlWin.addSuggestion( a_suggests[ii] ); }
}
this.controlWin.selectDefaultSuggestion(); }

function _setAsIgnored( text_num, word_num, flag ) { this.wordWin.removeFocus( text_num, word_num ); this.wordFlags[text_num][word_num] = flag; return true; }

function _getTotalReplaced() { var i_replaced = 0; for( var i = 0; i < this.wordFlags.length; i++ ) { for( var j = 0; j < this.wordFlags[i].length; j++ ) { if(( this.wordFlags[i][j] == this.replWordFlag )
|| ( this.wordFlags[i][j] == this.replAllFlag )
|| ( this.wordFlags[i][j] == this.fromReplAll )) { i_replaced++; }
}
}
return i_replaced; }

function _setWordText( text_num, word_num, newText, flag ) { this.wordWin.setText( text_num, word_num, newText ); this.wordFlags[text_num][word_num] = flag; return true; }

function _getFormInputs( inputPattern ) { var inputs = new Array(); for( var i = 0; i < document.forms.length; i++ ) { for( var j = 0; j < document.forms[i].elements.length; j++ ) { if( document.forms[i].elements[j].type.match( inputPattern )) { inputs[inputs.length] = document.forms[i].elements[j]; }
}
}
return inputs; }

