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
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace Digita.Tustena.Templates
{
    public class TemplateManager
    {
        public string FillTemplateFromFile(DataTable dt, string blockID, string filename)
        {
            TextReader tr = File.OpenText(filename);
            string html = tr.ReadToEnd();
            tr.Close();
            return FillTemplateFromHtml(dt, blockID, html);
        }
        public string FillTemplateFromHtml(DataTable dt, string blockID, string html)
        {
            string matchStr = string.Format(@"<!--startblock\s?(?:id=({0}))?-->([\s\S]*?)<!--endblock\s?(?:id=({0}))?-->", (blockID == null) ? @"\w+" : blockID);
            Regex rx = new Regex(matchStr);
            MatchCollection matchBlocks;

            if ((matchBlocks = rx.Matches(html)).Count > 0)
            {
                foreach (Match matchBlock in matchBlocks)
                    if (blockID == null || matchBlock.Groups[1].Value.ToLower() == blockID.ToLower())
                    {
                        MatchCollection matchCells;
                        string outBlock = string.Empty;

                        Regex rx2 = new Regex(@"\[\[(\w+)\]\]");
                        if ((matchCells = rx2.Matches(matchBlock.Groups[2].Value)).Count > 0)
                            foreach (DataRow sdr in dt.Rows)
                            {
                                string stringBlock = matchBlock.Groups[2].Value;

                                foreach (Match matchCell in matchCells)
                                {
                                    if (sdr.Table.Columns.Contains(matchCell.Groups[1].Value))
                                        stringBlock = stringBlock.Replace(matchCell.Value, sdr[matchCell.Groups[1].Value].ToString());
                                }
                                outBlock += stringBlock;
                            }
                        html = rx.Replace(html, outBlock);
                    }
            }
            return html;
        }

        public string LocalizeTemplateFromHtml(string html)
        {
            MatchCollection matchLabels;
            Regex rx = new Regex(@"\[\{(\w+)\}\]");
            if ((matchLabels = rx.Matches(html)).Count > 0)
                foreach (Match matchLabel in matchLabels)
                    try
                    {
                        html = html.Replace(matchLabel.Groups[0].Value, Digita.Tustena.Core.Root.rm.GetString(matchLabel.Groups[1].Value));
                    }
                    catch
                    {
                        html = html.Replace(matchLabel.Groups[0].Value, string.Empty);
                    }

            return html;
        }
        }
}
