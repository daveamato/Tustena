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
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Net.Security;
using System.Security.Policy;

namespace ConsoleApplication1
{
    public class Plaxo
    {
        string clientPart = "'Client', 'TustenaPlaxo/1.0', 'OS', 'windows/service pack infinity', 'Platform', 'Tustena/2006'";
        public Plaxo()
        {
        }

        public ArrayList GetContacts(string username, string password)
        {
            string PLXI = Plaxo_GetPLXI();
            string Uhid = Plaxo_GetUhid(PLXI, username, password);
            string PlaxoHeader = Plaxo_MakeHeader(PLXI, Uhid, password);
            string jsPseudoCode = Plaxo_GetContacts(PlaxoHeader);
            Debug.Write(jsPseudoCode);
            return PlaxoParsing(jsPseudoCode);
        }

        public ArrayList PlaxoParsing(string jsPseudoCode)
        {
            ArrayList arList = new ArrayList();
            Regex rx = new Regex(@"\[[\s\S]*?\]", RegexOptions.Multiline);
            MatchCollection ma = rx.Matches(jsPseudoCode);
            if (ma.Count > 0)
            {
                bool needAdd = false;
                bool newStart;
                rx = new Regex(@"'(.*?)'", RegexOptions.Multiline);
                foreach (Match m in ma)
                {
                    MatchCollection ma2 = rx.Matches(m.Value);
                    Hashtable hs = new Hashtable();
                    newStart = true;
                    int i = 0;
                    string fieldName = String.Empty;
                    bool setID = false;
                    bool setType = false;
                    if (ma2.Count > 0)
                    {
                        int p = -1;
                        foreach (Match m2 in ma2)
                        {
                            p++;
                            if (needAdd && setID)
                            {
                                hs.Add("ServerItemID", m2.Groups[1].Value);
                                setID = false;
                                continue;
                            }
                            if (needAdd && setType)
                            {
                                hs.Add("Type", m2.Groups[1].Value);
                                setType = false;
                                continue;
                            }
                            switch (m2.Groups[1].Value)
                            {
                                case "Data":
                                    newStart = false;
                                    continue;
                                case "ServerItemID":
                                    if (needAdd)
                                        setID = true;
                                    break;
                                case "Type":
                                    if (needAdd)
                                        setType = true;
                                    break;
                                default:
                                    if (p == 0)
                                    {
                                        if (m2.Groups[1].Value == "Add")
                                            needAdd = true;
                                        else
                                            needAdd = false;
                                    }
                                    break;

                            }
                            if (!newStart && needAdd && !setID)
                            {
                                if (i++ % 2 == 0)
                                    fieldName = m2.Groups[1].Value;
                                else
                                {
                                    if (m2.Groups[1].Value.Length > 0)
                                    {
                                        hs.Add(fieldName, m2.Groups[1].Value);
                                    }
                                    Debug.WriteLine(fieldName);
                                }
                            }
                        }
                    }
                    if (hs.Count > 0)
                        arList.Add(hs);
                }
            }
            return arList;

        }


        private string Plaxo_GetPLXI()
        {
            string s = PlaxoComm("['Header', 'ProtoVer', '1'," + clientPart + "]\n['/Header']\n['CreateGUID']\n['/CreateGUID']");
            Regex rx = new Regex("(PLXI:\\d+)");
            Match m = rx.Match(s);
            if (m.Success)
                return m.Groups[0].Value;
            else
                throw new Exception(s);
        }

        private string Plaxo_GetUhid(string plxi, string username, string password)
        {
            string s = PlaxoComm(String.Format("['Header', 'ProtoVer', '1', 'ClientID', '{0}', 'Identifier','{1}','AuthMethod', 'Plaxo', 'Password','{2}', " + clientPart + "]\n['/Header']", plxi, username, password));

            Regex rx = new Regex("'Uhid',\\s?'(\\d+)'");
            Match m = rx.Match(s);
            if (m.Success)
                return m.Groups[1].Value;
            else
                throw new Exception(s);
        }

        private string Plaxo_MakeHeader(string plxi, string Uhid, string password)
        {
            return String.Format("['Header', 'ProtoVer', '1', 'ClientID', '{0}', 'Uhid','{1}', 'Password','{2}'," + clientPart + "]\n['/Header']\n", plxi, Uhid, password);
        }

        private string Plaxo_GetFolders(string plaxoHeader)
        {
            string s = PlaxoComm(plaxoHeader + "['Get', 'Type', 'folder', 'Target', 'folders']['/Get']");
            return s;
        }

        private string Plaxo_GetContacts(string plaxoHeader)
        {
            string s = PlaxoComm(plaxoHeader + "['Sync', 'Target', 'Contacts', 'Source', 'DevLocalContacts', 'LastAnchor', '0', 'NextAnchor', '1']\n['/Sync'] ");
            return s;
        }

        private string PlaxoComm(string httpStr)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(MyCertValidationCb);
            WebClient wc = new WebClient();

            wc.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            byte[] data = wc.DownloadData("https://oapi.plaxo.com/rest?package=" + httpStr);

            return Encoding.ASCII.GetString(data);
        }
        public bool MyCertValidationCb(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) == SslPolicyErrors.RemoteCertificateChainErrors)
            {
                return false;
            }
            else if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                Zone z = Zone.CreateFromUrl(((HttpWebRequest)sender).RequestUri.ToString());
                if (z.SecurityZone == System.Security.SecurityZone.Intranet || z.SecurityZone == System.Security.SecurityZone.MyComputer)
                {
                    return false;
                }
                return false;
            }
            return false;
        }

        public class trustedCertificatePolicy : ICertificatePolicy
        {
            public trustedCertificatePolicy()
            {
            }

            public bool CheckValidationResult(ServicePoint sp,
                                              X509Certificate certificate,
                                              WebRequest request, int problem)
            {
                return true;
            }

        }
    }
}
#region "Plaxo fields"
#endregion
