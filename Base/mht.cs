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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mhtmlwriter
{

	public class mht
	{
		private string basePath;
		private string myNextPart = "_Tustena_CRM";

		public mht(string s)
		{
			basePath = s;
		}

		public string WriteStructure(string html)
		{
			ChangeGenerator(ref html);
			ChangeContentType(ref html);
			StringBuilder sb = new StringBuilder();
			StringBuilder attSb = new StringBuilder();
			sb.Append("MIME-Version: 1.0\r\n");
			sb.AppendFormat("Content-Type: multipart/related; boundary=\"----={0}\"\r\n", myNextPart);
			sb.Append("\r\n");
			sb.Append("If you see this Text, your viewer does not support MHTML format\r\n");
			sb.Append("\r\n");
			sb.AppendFormat("------={0}\r\n", myNextPart);
			sb.Append("Content-Type: text/html; charset=\"utf-8\"\r\n");
			ParseAttachments(ref html, ref attSb);
			sb.AppendFormat("\r\n{0}\r\n", html);
			sb.Append(attSb.ToString());
			return sb.ToString();
		}

		private void ParseAttachments(ref string html, ref StringBuilder sb)
		{
			string[] cids;
			ParseHTML(html, out cids);
			foreach (string s in cids)
			{
				string ss = s.Replace("%20", " ");
				string thePath = Path.Combine(basePath, ss.Replace("%20", " "));

				FileInfo f;
				f = new FileInfo(thePath);
				BinaryReader br;
				if (f.Exists)
					br = new BinaryReader(File.OpenRead(thePath));
				else
				{
					f = new FileInfo(ss);
					if (f.Exists)
						br = new BinaryReader(File.OpenRead(ss));
					else
						continue;
				}
				byte[] b = br.ReadBytes((int) f.Length);
				br.Close();
				string base64string = Convert.ToBase64String(b);
				WriteAttachmentHeader(ref sb, s);
				sb.AppendFormat("{0}\r\n", base64string);
			}
			WriteAttachmentFooter(ref sb);
		}

		public void ParseHTML(string bodyin, out string[] cids)
		{
			string patt = @"<img[\S\s]*src=[""'](.*?)[""'][\S\s]*?>";
			string[] tempCids = new string[0];
			Regex rx = new Regex(patt, RegexOptions.IgnoreCase | RegexOptions.Multiline);
			if (rx.IsMatch(bodyin))
			{
				MatchCollection mc = rx.Matches(bodyin);
				int i = 0;
				tempCids = new string[mc.Count];
				foreach (Match m in mc)
				{
					tempCids[i++] = m.Groups[1].ToString();
				}
			}
			cids = tempCids;
		}

		private void WriteAttachmentHeader(ref StringBuilder sb, string filename)
		{
			sb.AppendFormat("------={0}\r\n", myNextPart);
			sb.AppendFormat("Content-Location: {0}\r\n", filename);
			sb.Append("Content-Transfer-Encoding: base64\r\n");
			sb.AppendFormat("Content-Type: image/{0}\r\n\r\n", filename.Trim().ToLower().Substring(filename.Trim().LastIndexOf(".") + 1));
		}

		private void WriteAttachmentFooter(ref StringBuilder sb)
		{
			sb.AppendFormat("\r\n------={0}--\r\n", myNextPart);
		}

		private void ChangeGenerator(ref string html)
		{
			string patt = @"<meta[ ]*name=[""']?Generator[""']?[\S\s]*?>";
			Regex rx = new Regex(patt, RegexOptions.IgnoreCase | RegexOptions.Multiline);
			if (rx.IsMatch(html))
				html = rx.Replace(html, "<meta name=Generator content=\"Tustena CRM (http://www.tustena.com)\">");
		}

		private void ChangeContentType(ref string html)
		{
			string patt = @"<meta[ ]*http-equiv=[""']?Content-Type[""']?[\S\s]*?>";
			Regex rx = new Regex(patt, RegexOptions.IgnoreCase | RegexOptions.Multiline);
			if (rx.IsMatch(html))
				html = rx.Replace(html, "<meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
		}

		public string InjectImages(string html, string[] images, string basePath, string alternatePath)
		{
			StringBuilder sb = new StringBuilder();
			Regex re = new Regex("\r\n(------=.*?)--\r\n");
			Match m;
			if((m = re.Match(html)).Success)
			html = re.Replace(html,"");
			sb.Append(html);
			foreach (string s in images)
			{
				if(s.Length>0)
				{
					string thePath = Path.Combine(basePath, s);

					FileInfo f;
					f = new FileInfo(thePath);
					BinaryReader br;
					if (!f.Exists)
					{
						thePath = Path.Combine(alternatePath, s);
						f = new FileInfo(thePath);
						if (!f.Exists)
							throw new FileNotFoundException();
					}

					br = new BinaryReader(File.OpenRead(thePath));
					byte[] b = br.ReadBytes((int) f.Length);
					br.Close();
					string base64string = Convert.ToBase64String(b);
					if(m.Success)
					{
						sb.AppendFormat("\r\n{0}\r\n",m.Groups[1].Value);
						sb.AppendFormat("Content-Location: {0}\r\n", s);
						sb.Append("Content-Transfer-Encoding: base64\r\n");
						sb.AppendFormat("Content-Type: image/{0}\r\n\r\n", s.Trim().ToLower().Substring(s.Trim().LastIndexOf(".") + 1));

					}
					else
						WriteAttachmentHeader(ref sb, s);
					sb.AppendFormat("{0}\r\n", base64string);
				}
			}
			if(m.Success)
				sb.Append(m.Value);
			else
				WriteAttachmentFooter(ref sb);
			return sb.ToString();
		}


	}
}
