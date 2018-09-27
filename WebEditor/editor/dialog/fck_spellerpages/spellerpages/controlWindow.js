/*
 * TUSTENA PUBLIC LICENSE v1.0
 * Please obtain a copy of the License at http://www.tustena.com/TPL/
 * and read it before using this file.
 * Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
 */

function controlWindow( controlForm ) { this._form = controlForm; 
this.windowType = "controlWindow"; this.noSuggestionSelection = FCKLang.DlgSpellNoSuggestions ; this.suggestionList  = this._form.sugg; this.evaluatedText   = this._form.misword; this.replacementText = this._form.txtsugg; this.undoButton      = this._form.btnUndo; 
this.addSuggestion = addSuggestion; this.clearSuggestions = clearSuggestions; this.selectDefaultSuggestion = selectDefaultSuggestion; this.resetForm = resetForm; this.setSuggestedText = setSuggestedText; this.enableUndo = enableUndo; this.disableUndo = disableUndo; }

function resetForm() { if( this._form ) { this._form.reset(); }
}

function setSuggestedText() { var slct = this.suggestionList; var txt = this.replacementText; var str = ""; if( (slct.options[0].text) && slct.options[0].text != this.noSuggestionSelection ) { str = slct.options[slct.selectedIndex].text; }
txt.value = str; }

function selectDefaultSuggestion() { var slct = this.suggestionList; var txt = this.replacementText; if( slct.options.length == 0 ) { this.addSuggestion( this.noSuggestionSelection ); } else { slct.options[0].selected = true; }
this.setSuggestedText(); }

function addSuggestion( sugg_text ) { var slct = this.suggestionList; if( sugg_text ) { var i = slct.options.length; var newOption = new Option( sugg_text, 'sugg_text'+i ); slct.options[i] = newOption; }
}

function clearSuggestions() { var slct = this.suggestionList; for( var j = slct.length - 1; j > -1; j-- ) { if( slct.options[j] ) { slct.options[j] = null; }
}
}

function enableUndo() { if( this.undoButton ) { if( this.undoButton.disabled == true ) { this.undoButton.disabled = false; }
}
}

function disableUndo() { if( this.undoButton ) { if( this.undoButton.disabled == false ) { this.undoButton.disabled = true; }
}
}
