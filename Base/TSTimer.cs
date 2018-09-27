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
using System.Net;
using System.Web;
using System.Web.Caching;
using Digita.Tustena.Core;
using System.Threading;

namespace Digita.Tustena.Base
{
	public class TSTimer
	{
		public TSTimer()
		{
		}

		public static void RegisterCacheEntry()
		{

			if( null != HttpContext.Current.Cache[ "TSTimer" ] ) return;

			HttpContext.Current.Cache.Add( "TSTimer", "Test", null, DateTime.MaxValue,
				TimeSpan.FromSeconds(60-DateTime.Now.Second), CacheItemPriority.NotRemovable,
				new CacheItemRemovedCallback( CacheItemRemovedCallback ) );
		}

        public void tr()
        {
            G.SendError("Timer", DateTime.Now.ToString());
            Thread.Sleep(60000);
        }

		public static void CacheItemRemovedCallback(
			string key,
			object value,
			CacheItemRemovedReason reason
			)
		{

			try
			{
				DoWork();
			}
			catch
			{
			}
			HitPage();

		}

		private static void DoWork()
		{
            G.SendError("Timer", DateTime.Now.ToString());
		}

		public static void HitPage()
		{

		}

	}
}
