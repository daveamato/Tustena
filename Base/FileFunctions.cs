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
using System.IO;
using System.Runtime.InteropServices;
using Digita.Tustena.Core;

namespace Digita.Tustena.Base
{
	public class FileFunctions
	{

		private FileFunctions(){}
		[DllImport("msvcrt.dll", SetLastError=true)]
		private static extern int _mkdir(string path);

		public static DirectoryInfo CreateDirectory(string path)
		{
			ArrayList oDirsToCreate = new ArrayList();

			DirectoryInfo oDir = new DirectoryInfo(Path.GetFullPath(path));

			while (oDir != null && !oDir.Exists)
			{
				oDirsToCreate.Add(oDir.FullName);
				oDir = oDir.Parent;
			}

			if (oDir == null)
				throw(new DirectoryNotFoundException("Directory \"" + oDirsToCreate[oDirsToCreate.Count - 1] + "\" not found."));

			for (int i = oDirsToCreate.Count - 1; i >= 0; i--)
			{
				string sPath = (string) oDirsToCreate[i];
				int iReturn = -1;
				try
				{
					iReturn = _mkdir(sPath);
				}catch{}
				if (iReturn != 0)
					throw new ApplicationException("Error calling [msvcrt.dll]:_wmkdir(" + sPath + "), error code: " + iReturn);
			}

			return new DirectoryInfo(path);
		}
		public static bool CheckDir(string dir, bool create)
		{
			bool exist = Directory.Exists(Path.Combine(ConfigSettings.DataStoragePath, dir));
			if (create && !exist)
			{
				CreateDirectory(Path.Combine(ConfigSettings.DataStoragePath, dir));
				exist = true;
			}
			return exist;
		}

		public static string GetFileImg(string ext)
		{
			string img = String.Empty;
			switch (ext.ToLower())
			{
				case ".gif":
				case ".jpg":
				case ".bmp":
				case ".png":
				case ".jpeg":
					img = "/icons/image.gif";
					break;
				case ".doc":
				case ".rtf":
					img = "/icons/doc.gif";
					break;
				case ".eml":
					img = "/icons/email.gif";
					break;
				case ".pdf":
					img = "/icons/pdf.gif";
					break;
				case ".zip":
					img = "/icons/zip.gif";
					break;
				case ".ppt":
				case ".pps":
					img = "/icons/ppt.gif";
					break;
				case ".xls":
					img = "/icons/xls.gif";
					break;
				case ".wav":
				case ".mp3":
					img = "/icons/sound.gif";
					break;
				default:
					img = "/icons/generic.gif";
					break;
			}
			return img;
		}

		public static long FolderSize(string path)
		{
			long fSize = 0;
			try
			{
				fSize = FolderFileSize(path);
				DirectoryInfo[] folders = (new DirectoryInfo(path)).GetDirectories();
				foreach (DirectoryInfo folder in folders)
				{
					fSize += FolderSize(folder.FullName);

				}
			}
			catch
			{
			}
			return fSize;
		}

		public static long FolderFileSize(string path)
		{
			long size = 0;
			try
			{
				FileInfo[] files = (new DirectoryInfo(path)).GetFiles();
				foreach (FileInfo file in files)
				{
					size += file.Length;
				}
			}
			catch
			{
			}
			return size;
		}

		public static bool WriteTextToFile(string file, string text)
		{
			if(Directory.Exists(Path.GetDirectoryName(file)))
			{
				TextWriter tw = File.CreateText(file);
				tw.Write(text);
				tw.Close();
				return true;
			}
			return false;
		}
	}
}
