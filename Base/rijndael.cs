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
using System.Security.Cryptography;
using System.Text;

namespace Digita.Tustena.Survey
{
	public class RijndaelEncDec
	{
		public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
		{
			MemoryStream ms = new MemoryStream();

			Rijndael alg = Rijndael.Create();


			alg.Key = Key;
			alg.IV = IV;

			CryptoStream cs = new CryptoStream(ms,
				alg.CreateEncryptor(), CryptoStreamMode.Write);

			cs.Write(clearData, 0, clearData.Length);

			cs.Close();

			byte[] encryptedData = ms.ToArray();

			return encryptedData;
		}


		public static string Encrypt(string clearText, string Password)
		{
			byte[] clearBytes =
				Encoding.Unicode.GetBytes(clearText);

			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

			byte[] encryptedData = Encrypt(clearBytes,
				pdb.GetBytes(32), pdb.GetBytes(16));

			return Convert.ToBase64String(encryptedData);

		}


		public static byte[] Encrypt(byte[] clearData, string Password)
		{
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

			return Encrypt(clearData, pdb.GetBytes(32), pdb.GetBytes(16));

		}

		public static void Encrypt(string fileIn,
			string fileOut, string Password)
		{

			FileStream fsIn = new FileStream(fileIn,
				FileMode.Open, FileAccess.Read);
			FileStream fsOut = new FileStream(fileOut,
				FileMode.OpenOrCreate, FileAccess.Write);

			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

			Rijndael alg = Rijndael.Create();
			alg.Key = pdb.GetBytes(32);
			alg.IV = pdb.GetBytes(16);

			CryptoStream cs = new CryptoStream(fsOut,
				alg.CreateEncryptor(), CryptoStreamMode.Write);

			int bufferLen = 4096;
			byte[] buffer = new byte[bufferLen];
			int bytesRead;

			do
			{
				bytesRead = fsIn.Read(buffer, 0, bufferLen);

				cs.Write(buffer, 0, bytesRead);
			} while(bytesRead != 0);


			cs.Close();
			fsIn.Close();
		}

		public static byte[] Decrypt(byte[] cipherData,
			byte[] Key, byte[] IV)
		{
			MemoryStream ms = new MemoryStream();

			Rijndael alg = Rijndael.Create();

			alg.Key = Key;
			alg.IV = IV;

			CryptoStream cs = new CryptoStream(ms,
				alg.CreateDecryptor(), CryptoStreamMode.Write);

			cs.Write(cipherData, 0, cipherData.Length);

			cs.Close();

			byte[] decryptedData = ms.ToArray();

			return decryptedData;
		}


		public static string Decrypt(string cipherText, string Password)
		{
			byte[] cipherBytes = Convert.FromBase64String(cipherText);

			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65,
							   0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

			byte[] decryptedData = Decrypt(cipherBytes,
				pdb.GetBytes(32), pdb.GetBytes(16));

			return Encoding.Unicode.GetString(decryptedData);
		}


		public static byte[] Decrypt(byte[] cipherData, string Password)
		{
			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});


			return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
		}

		public static void Decrypt(string fileIn,
			string fileOut, string Password)
		{

			FileStream fsIn = new FileStream(fileIn,
				FileMode.Open, FileAccess.Read);
			FileStream fsOut = new FileStream(fileOut,
				FileMode.OpenOrCreate, FileAccess.Write);

			PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
				new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
							   0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
			Rijndael alg = Rijndael.Create();

			alg.Key = pdb.GetBytes(32);
			alg.IV = pdb.GetBytes(16);

			CryptoStream cs = new CryptoStream(fsOut,
				alg.CreateDecryptor(), CryptoStreamMode.Write);

			int bufferLen = 4096;
			byte[] buffer = new byte[bufferLen];
			int bytesRead;

			do
			{
				bytesRead = fsIn.Read(buffer, 0, bufferLen);

				cs.Write(buffer, 0, bytesRead);

			} while(bytesRead != 0);

			cs.Close(); // this will also close the unrelying fsOut stream
			fsIn.Close();
		}
	}
}
