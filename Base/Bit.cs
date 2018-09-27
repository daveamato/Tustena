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

namespace Digita.Tustena
{
	public class Bit
	{
		private long[] B = new long[64];
		private long[] C = new long[64];
		private long[] N = new long[64];

		public Bit()
		{
			byte k;

			B[0] = 1;
			C[0] = ~B[0];
			for(k=1;k<64;k++)
			{
				B[k] = B[k-1]<<1;
				C[k] = ~B[k];
			}
			for(k=0;k<64;k++)
			{
				N[k] = 0;
			}
		}
		public void BitOn(ref byte byteContainer, byte bitNumber)
		{
			byteContainer = (byte)(byteContainer | B[bitNumber]);
		}

		public void BitOn(ref uint intContainer, byte bitNumber)
		{
			intContainer = (uint)(intContainer | B[bitNumber]);
		}

		public void BitOn(ref long longContainer, byte bitNumber)
		{
			longContainer = longContainer | B[bitNumber];
		}

		public void BitOff(ref byte byteContainer, byte bitNumber)
		{
			byteContainer = (byte)(byteContainer & C[bitNumber]);
		}

		public void BitOff(ref int uIntContainer, byte bitNumber)
		{
			uIntContainer = (int)(uIntContainer & C[bitNumber]);
		}

		public void BitOff(ref long longContainer, byte bitNumber)
		{
			longContainer = longContainer & C[bitNumber];
		}

		public bool isBitOn(ref byte byteContainer, byte bitNumber)
		{
			long mask = 0;
			BitOn(ref mask,bitNumber);
			return((mask & byteContainer)>0)?true:false;
		}

		public bool isBitOn(ref int uIntContainer, byte bitNumber)
		{
			long mask = 0;
			BitOn(ref mask,bitNumber);
			return((mask & uIntContainer)>0)?true:false;
		}
		public bool isBitOn(ref long longContainer, byte bitNumber)
		{
			long mask = 0;
			BitOn(ref mask,bitNumber);
			return((mask & longContainer)>0)?true:false;
		}

	}
}
