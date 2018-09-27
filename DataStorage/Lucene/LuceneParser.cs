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
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;

namespace Lucene.Net.Parsing
{
	public class LuceneParser
	{

		private string pathIndex;
		private IndexWriter indexWriter;
		private string[] patterns = {"*.doc", "*.xls", "*.ppt", "*.htm", "*.html", "*.txt", "*.pdf"};

		private IndexSearcher searcher = null;

		private long byTextFonttal = 0;
		private int countTotal = 0;
		private int countSkipped = 0;


		public LuceneParser(string luceneIndexPath)
		{
			pathIndex=luceneIndexPath;
		}


		public void IndexFolder(string path)
		{
			InitWriter();

			byTextFonttal = 0;
			countTotal = 0;
			countSkipped = 0;

			DirectoryInfo di = new DirectoryInfo(path);

			DateTime start = DateTime.Now;

			addFolder(di);


			indexWriter.Optimize();
			indexWriter.Close();
		}

		public void addFolder(string dir)
		{
			InitWriter();

			DirectoryInfo directory = new DirectoryInfo(dir);
			addFolder(directory);
		}
		private void addFolder(DirectoryInfo directory)
		{
			foreach (string pattern in patterns)
			{
				foreach (FileInfo fi in directory.GetFiles(pattern))
				{

					try
					{
						addIFilterDocument(fi.FullName);

						this.countTotal++;
						this.byTextFonttal += fi.Length;

					}
					catch (Exception)
					{
						this.countSkipped++;
					}
				}
			}

			foreach (DirectoryInfo di in directory.GetDirectories())
			{
				addFolder(di);
			}
		}

		private void InitWriter()
		{
			indexWriter = new IndexWriter(this.pathIndex, new StandardAnalyzer(), true);

		}

		public void AddFile(string fiPath)
		{
			InitWriter();
			bool found = false;
			foreach (string pattern in patterns)
				if("*"+Path.GetExtension(fiPath)== pattern)
				{
					found=true;
					break;
				}
			if(!found)
				return;


			try
			{
				addIFilterDocument(fiPath);
			}
			catch
			{
				return;
			}
			indexWriter.Optimize();
			indexWriter.Close();
		}

		public void DeleteFile(string fiPath)
		{
			InitWriter();
			try
			{

			}
			catch
			{
				return;
			}
			indexWriter.Optimize();
			indexWriter.Close();
		}

		private void addIFilterDocument(string path)
		{
			Document doc = new Document();
			string filename = Path.GetFileName(path);

			doc.Add(Field.UnStored("text", LuceneParser.Parse(path)));
			doc.Add(Field.Keyword("path", path));
			doc.Add(Field.Text("title", filename));
			indexWriter.AddDocument(doc);
		}

		public string checkIndex()
		{
			try
			{
				searcher = new IndexSearcher(this.pathIndex);
				searcher.Close();
			}
			catch (IOException)
			{
				return "-";
			}

			return searcher.MaxDoc().ToString();
		}

		public DataTable search(string searchText)
		{
			DateTime start = DateTime.Now;

			try
			{
				searcher = new IndexSearcher(this.pathIndex);
			}
			catch (IOException ex)
			{
				throw new IndexDamagedException("The index doesn't exist or is damaged. Please rebuild the index.\r\n\r\nDetails:\r\n" + ex.Message);
			}

			if ((searchText.Trim() != null && searchText.Trim().Length == 0))
				return new DataTable();

			Query query = QueryParser.Parse(searchText, "text", new StandardAnalyzer());

			Hits hits = searcher.Search(query);

			DataTable dt = new DataTable();
			dt.Columns.Add("title",typeof(string));
			dt.Columns.Add("path",typeof(string));
			dt.Columns.Add("hits",typeof(string));

			for (int i = 0; i < hits.Length(); i++)
			{
				DataRow dr = dt.NewRow();
				Document doc = hits.Doc(i);

				dr["title"] = doc.Get("title");
				dr["path"] = doc.Get("path");
				dr["hits"] = hits.Score(i).ToString();
				dt.Rows.Add(dr);

			}
			searcher.Close();

			return dt;
		}

		[DllImport("query.dll", CharSet = CharSet.Unicode)]
		private extern static int LoadIFilter (string pwcsPath, IntPtr pUnkOuter, ref IFilter ppIUnk);

		[ComImport, Guid("00000000-0000-0000-C000-000000000046")]
			[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
			private interface IUnknown
		{
			[PreserveSig]
			IntPtr QueryInterface( ref Guid riid, out IntPtr pVoid );

			[PreserveSig]
			IntPtr AddRef();

			[PreserveSig]
			IntPtr Release();
		}


		private static IFilter loadIFilter(string filename)
		{
			IFilter filter = null;

			int resultLoad = LoadIFilter( filename, IntPtr.Zero, ref filter );
			if (resultLoad != (int)IFilterReturnCodes.S_OK)
			{
				return null;
			}
			return filter;
		}


		public static bool IsParseable(string filename)
		{
			return loadIFilter(filename) != null;
		}

		public static string Parse(string filename)
		{
			IFilter filter = null;

			try
			{
				StringBuilder plainTextResult = new StringBuilder();
				filter = loadIFilter(filename);

				STAT_CHUNK ps = new STAT_CHUNK();
				IFILTER_INIT mFlags = 0;

				uint i = 0;
				filter.Init( mFlags, 0, null, ref i);

				int resultChunk = 0;

				resultChunk = filter.GetChunk(out ps);
				while (resultChunk == 0)
				{
					if (ps.flags == CHUNKSTATE.CHUNK_TEXT)
					{
						uint sizeBuffer = 60000;
						int resultText = 0;
						while (resultText == Constants.FILTER_S_LAST_TEXT || resultText == 0)
						{
							sizeBuffer = 60000;
							StringBuilder sbBuffer = new StringBuilder((int)sizeBuffer);
							resultText = filter.GetText(ref sizeBuffer, sbBuffer);

							if (sizeBuffer > 0 && sbBuffer.Length > 0)
							{
								string chunk = sbBuffer.ToString(0, (int)sizeBuffer);
								plainTextResult.Append(chunk);
							}
						}
					}
					resultChunk = filter.GetChunk(out ps);
				}
				return plainTextResult.ToString();
			}
			finally
			{
				if (filter != null)
					Marshal.ReleaseComObject(filter);
			}
		}
	}
	class IndexDamagedException : Exception
	{
		public IndexDamagedException(string message) : base(message){}
	}

}
