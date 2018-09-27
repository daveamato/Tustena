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
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace Digita.Tustena.Core
{
    public class StaticFunctions
    {
        private StaticFunctions()
        {
        }

        public static bool IsNumber(string s)
        {
            try
            {
                int.Parse(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void InvertObjects(ref object obj1, ref object obj2)
        {
            object tempObj = obj1;
            obj1 = obj2;
            obj2 = tempObj;
        }


        public static string FixDecimalJS(string s)
        {
            int pos = s.LastIndexOf(",");
            int pos1 = s.LastIndexOf(".");
            string decimalSep = string.Empty;
            if (pos == -1 || pos1 == -1)
            {
                decimalSep = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
                if (decimalSep != ".")
                    s = s.Replace(decimalSep, ".");
            }

            return s;
        }
        private static Regex fdRx = null;
        public static decimal FixDecimal(string s)
        {
            if (fdRx == null)
                fdRx = new Regex(@"[^\d\.,]");
            s = fdRx.Replace(s, "");

            string decimalSep = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
            int pos = s.LastIndexOf(",");
            int pos1 = s.LastIndexOf(".");
            if (pos == -1 || pos1 == -1)
            {
                if (decimalSep == ",")
                    s = s.Replace(".", ",");
                else
                    s = s.Replace(",", ".");
            }
            else
            {
                if (pos > pos1 && decimalSep == ".")
                    s = s.Replace(".", "").Replace(",", ".");
                else if (pos < pos1 && decimalSep == ",")
                    s = s.Replace(",", "").Replace(".", ",");
            }
            try
            {
                Decimal d = Decimal.Parse(s, NumberStyles.Currency);
                return d;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static string RemoveSpecialChar(string s)
        {
            return Regex.Replace(s, @"[^\w\.-]", string.Empty);
        }

        public static string FilterSearch(string s)
        {
            return Core.Normalize.NormalizeLatin(Regex.Replace(s, @"[^\w\d]", string.Empty));
        }

        public static string FixPhoneNumber(string pnum)
        {
            if (Regex.IsMatch(pnum, @"^[\+0-9]"))
            {
                pnum = Regex.Replace(pnum, @"[ ]*?\((.*?)\)$", "");
                pnum = Regex.Replace(pnum, @"[^\+0-9]", "");
            }
            return pnum;
        }

        public static string CutOverflow(string text, int cellWidth)
        {
            return String.Format("<nobr style=\"overflow:hidden; text-overflow:ellipsis; white-space:nowrap; width:{0}px\" title={1}>{1}</nobr>", text, cellWidth);
        }

        public static string FixNull(object value)
        {
            return (value == DBNull.Value || value == null || (value != null && ((string)value).Length == 0)) ? string.Empty : value.ToString();
        }

        public static bool IsBlank(string value)
        {
            return value == null || value.Length == 0;
        }

        public static bool IsNotBlank(string value)
        {
            return !IsBlank(value);
        }

        public static bool IsDate(string str)
        {
            try
            {
                Convert.ToDateTime(str);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static string Capitalize(string s)
        {
            return s.Substring(0, 1).ToUpper() + s.Substring(1, s.Length - 1).ToLower();
        }

        public static string FixCarriage(string text)
        {
            text = Regex.Replace(text, @"(['])|([\\])+", @"&quot;");
            text = Regex.Replace(text, @"([\r]|[\n]|[\t])+", @"\$1$2");
            text = Regex.Replace(text, @"([""])+", "<br>");
            return text;
        }

        public static string FixCarriage(string text, bool js)
        {
            return Regex.Replace(text, @"([\r]|[\n]|[\t])+", "<br>");
        }

        public static string ParseURL(string text)
        {
            return Regex.Replace(text, @"http://[^\s\r\n\t""'>]+", @"<a href=""$0"" target=""_blank"">$0</a>");
        }

        public static string TruncateStr(string text, int length)
        {
            if (text.Length < length)
            {
                return text;
            }
            else
            {
                text = text.Substring(0, length);
                while (true)
                {
                    if (text.EndsWith(" "))
                    {
                        break;
                    }
                    else
                    {
                        text = text.Substring(0, text.Length - 1);
                    }
                }
                return text + "&hellip;";
            }
        }

        public static string ContentTypeMatch(string imgFile)
        {
            switch (Path.GetExtension(imgFile))
            {
                case ".gif":
                    return "image/gif";
                case ".jpg":
                case ".jpe":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".bmp":
                    return "image/bmp";
                default:
                    return "application/octet-stream";
            }
        }

        public static string FormatFileSize(long size)
        {
            string[] units = new string[] { "B", "Kb", "Mb", "Gb", "Tb" };
            int i = 0;
            float newSize;
            newSize = size;
            while (newSize >= 1024 && i < units.Length)
            {
                newSize /= 1024;
                i++;
            }
            return string.Format("{0} {1}", newSize.ToString("##.##"), units[i]);
        }

        public static bool IsInZone(string zone, string zonelist)
        {
            if (zone == "0")
                return true;

            return (zonelist.IndexOf(zone) >= 0);
        }
        public static bool IsClientScriptEnabled(System.Web.UI.Page page, bool value)
        {
            try
            {
                if (page.Request.Browser.W3CDomVersion.Major < 1)
                    return false;

                if (page.Request.Browser.EcmaScriptVersion.CompareTo(new Version(1, 2)) < 0)
                    return false;


                return value;
            }
            catch
            {
                return false;
            }
        }
    }
}
