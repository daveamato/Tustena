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
using System.Runtime.InteropServices;
using System.Text;

namespace Lucene.Net.Parsing
{
	[Flags]
	public enum IFILTER_INIT : uint
	{
		NONE                   = 0,
		CANON_PARAGRAPHS       = 1,
		HARD_LINE_BREAKS       = 2,
		CANON_HYPHENS          = 4,
		CANON_SPACES           = 8,
		APPLY_INDEX_ATTRIBUTES = 16,
		APPLY_CRAWL_ATTRIBUTES = 256,
		APPLY_OTHER_ATTRIBUTES = 32,
		INDEXING_ONLY          = 64,
		SEARCH_LINKS           = 128,
		FILTER_OWNED_VALUE_OK  = 512
	}

	public enum CHUNK_BREAKTYPE
	{
		CHUNK_NO_BREAK = 0,
		CHUNK_EOW      = 1,
		CHUNK_EOS      = 2,
		CHUNK_EOP      = 3,
		CHUNK_EOC      = 4
	}

	[Flags]
	public enum CHUNKSTATE
	{
		CHUNK_TEXT               = 0x1,
		CHUNK_VALUE              = 0x2,
		CHUNK_FILTER_OWNED_VALUE = 0x4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct PROPSPEC
	{
		public uint ulKind;
		public uint propid;
		public IntPtr lpwstr;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FULLPROPSPEC
	{
		public Guid guidPropSet;
		public PROPSPEC psProperty;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct STAT_CHUNK
	{
		public uint  idChunk;
		[MarshalAs(UnmanagedType.U4)]
		public CHUNK_BREAKTYPE breakType;
		[MarshalAs(UnmanagedType.U4)]
		public CHUNKSTATE flags;
		public uint locale;
		[MarshalAs(UnmanagedType.Struct)] public FULLPROPSPEC attribute;
		public uint idChunkSource;
		public uint cwcStartSource;
		public uint cwcLenSource;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct FILTERREGION
	{
		public uint idChunk;
		public uint cwcStart;
		public uint cwcExtent;
	}

	[ComImport]
	[Guid("89BCB740-6119-101A-BCB7-00DD010655AF")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	public interface IFilter
	{
		void Init([MarshalAs(UnmanagedType.U4)] IFILTER_INIT grfFlags, uint cAttributes, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] FULLPROPSPEC[] aAttributes, ref uint pdwFlags);
		[PreserveSig] int GetChunk(out STAT_CHUNK pStat);
		[PreserveSig] int GetText(ref uint pcwcBuffer, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder buffer);
		void GetValue(ref UIntPtr ppPropValue);
		void BindRegion([MarshalAs(UnmanagedType.Struct)]FILTERREGION origPos, ref Guid riid, ref UIntPtr ppunk);
	}

	[ComImport]
	[Guid("f07f3920-7b8c-11cf-9be8-00aa004b9986")]
	public class CFilter
	{
	}

	public class Constants
	{
		public const uint PID_STG_DIRECTORY               =0x00000002;
		public const uint PID_STG_CLASSID                 =0x00000003;
		public const uint PID_STG_STORAGETYPE             =0x00000004;
		public const uint PID_STG_VOLUME_ID               =0x00000005;
		public const uint PID_STG_PARENT_WORKID           =0x00000006;
		public const uint PID_STG_SECONDARYSTORE          =0x00000007;
		public const uint PID_STG_FILEINDEX               =0x00000008;
		public const uint PID_STG_LASTCHANGEUSN           =0x00000009;
		public const uint PID_STG_NAME                    =0x0000000a;
		public const uint PID_STG_PATH                    =0x0000000b;
		public const uint PID_STG_SIZE                    =0x0000000c;
		public const uint PID_STG_ATTRIBUTES              =0x0000000d;
		public const uint PID_STG_WRITETIME               =0x0000000e;
		public const uint PID_STG_CREATETIME              =0x0000000f;
		public const uint PID_STG_ACCESSTIME              =0x00000010;
		public const uint PID_STG_CHANGETIME              =0x00000011;
		public const uint PID_STG_CONTENTS                =0x00000013;
		public const uint PID_STG_SHORTNAME               =0x00000014;
		public const int  FILTER_E_END_OF_CHUNKS          =(unchecked((int)0x80041700));
		public const int  FILTER_E_NO_MORE_TEXT           =(unchecked((int)0x80041701));
		public const int  FILTER_E_NO_MORE_VALUES         =(unchecked((int)0x80041702));
		public const int  FILTER_E_NO_TEXT                =(unchecked((int)0x80041705));
		public const int  FILTER_E_NO_VALUES              =(unchecked((int)0x80041706));
		public const int  FILTER_S_LAST_TEXT              =(unchecked((int)0x00041709));
	}

	public enum IFilterReturnCodes : uint
	{
		S_OK = 0,
		E_ACCESSDENIED = 0x80070005,
		E_HANDLE = 0x80070006,
		E_INVALIDARG = 0x80070057,
		E_OUTOFMEMORY = 0x8007000E,
		E_NOTIMPL = 0x80004001,
		E_FAIL = 0x80000008,
		FILTER_E_PASSWORD = 0x8004170B,
		FILTER_E_UNKNOWNFORMAT = 0x8004170C,
		FILTER_E_NO_TEXT = 0x80041705,
		FILTER_E_END_OF_CHUNKS = 0x80041700,
		FILTER_E_NO_MORE_TEXT = 0x80041701,
		FILTER_E_NO_MORE_VALUES = 0x80041702,
		FILTER_E_ACCESS = 0x80041703,
		FILTER_W_MONIKER_CLIPPED = 0x00041704,
		FILTER_E_EMBEDDING_UNAVAILABLE = 0x80041707,
		FILTER_E_LINK_UNAVAILABLE = 0x80041708,
		FILTER_S_LAST_TEXT = 0x00041709,
		FILTER_S_LAST_VALUES = 0x0004170A
	}
}
