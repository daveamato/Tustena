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


namespace Digita.Tustena {

    public partial class DataStorage {
        protected System.Web.UI.HtmlControls.HtmlHead head;
        protected System.Web.UI.HtmlControls.HtmlGenericControl body;
        protected System.Web.UI.HtmlControls.HtmlForm uploadForm;
        protected System.Web.UI.WebControls.LinkButton LbnNew;
        protected System.Web.UI.WebControls.LinkButton LbnBack;
        protected System.Web.UI.WebControls.TextBox TxtSearch;
        protected System.Web.UI.WebControls.RadioButtonList searchType;
        protected System.Web.UI.WebControls.LinkButton BtnSearchText;
        protected Microsoft.Web.UI.WebControls.TreeView tvCategoryTreeSearch;
        protected System.Web.UI.WebControls.TextBox CategoryTextSearch;
        protected System.Web.UI.WebControls.TextBox CategoryIdSearch;
        protected System.Web.UI.HtmlControls.HtmlGenericControl helptext;
        protected System.Web.UI.WebControls.Label LlblAction;
        protected System.Web.UI.WebControls.Literal HelpLabel;
        protected System.Web.UI.WebControls.Repeater FileRep;
        protected Digita.Tustena.Common.RepeaterPaging FileRepPaging;
        protected Microsoft.Web.UI.WebControls.TreeView tvCategoryTree;
        protected System.Web.UI.HtmlControls.HtmlTable fileTab;
        protected System.Web.UI.WebControls.Literal FileID;
        protected System.Web.UI.WebControls.Literal FileRevision;
        protected System.Web.UI.WebControls.Literal NRevision;
        protected System.Web.UI.HtmlControls.HtmlInputFile txtFileName;
        protected Brettle.Web.NeatUpload.InputFile inputFile;
        protected Brettle.Web.NeatUpload.ProgressBar inlineProgressBar;
        protected System.Web.UI.WebControls.TextBox CategoryText;
        protected System.Web.UI.WebControls.TextBox CategoryId;
        protected System.Web.UI.WebControls.TextBox Description;
        protected System.Web.UI.WebControls.RadioButtonList CrossWith;
        protected System.Web.UI.WebControls.TextBox CrossText;
        protected System.Web.UI.WebControls.TextBox CrossId;
        protected Digita.Tustena.GroupControl groupsDialog;
        protected System.Web.UI.WebControls.LinkButton LbnSubmit;
        protected System.Web.UI.WebControls.Label Info;
    }
}
