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
using System.Data.OleDb;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.DomValidators;

namespace Digita.Tustena
{
	public partial class SearchDataStorage : Page
	{
		protected OleDbCommand oleDbSelectCommand1;
		protected OleDbDataAdapter dbAdapter;
		protected OleDbConnection dbConnection;

		protected void Page_Load(object sender, EventArgs e)
		{
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.dbAdapter = new OleDbDataAdapter();
			this.oleDbSelectCommand1 = new OleDbCommand();
			this.dbConnection = new OleDbConnection();
			this.BtnSearch.Click += new EventHandler(BtnSearch_Click);
			this.DgResultsGrid.PageIndexChanged += new DataGridPageChangedEventHandler(this.dgResultsGrid_PageIndexChanged);
			this.dbAdapter.SelectCommand = this.oleDbSelectCommand1;
			this.oleDbSelectCommand1.Connection = this.dbConnection;
			this.dbConnection.ConnectionString = "Provider=MSIDXS.1;Integrated Security .=\"\";Data Source=TustenaDataStorage";
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void BtnSearch_Click(object sender, EventArgs e)
		{
			this.Search();
		}

		private void dgResultsGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DgResultsGrid.CurrentPageIndex = e.NewPageIndex;
			this.Search();
		}

		private string Command
		{
			get
			{
				string query = String.Format("SELECT Rank, VPath, DocTitle, Filename, Characterization, Write FROM SCOPE('DEEP TRAVERSAL OF \"{0}\"') WHERE NOT CONTAINS(VPath, '\"_vti_\" OR \".config\"')",this.cboDirectory.SelectedItem.Value);

				string type = this.cboQueryType.SelectedItem.Value.ToLower();
				string fmt = @" AND (CONTAINS('{0}') OR CONTAINS(DocTitle, '{0}'))";

				string text = this.txtQuery.Text.Replace(";", "");
				if (type == "all" || type == "any" || type == "boolean")
				{
					string[] words = text.Split(' ');
					int len = words.Length;
					for (int i = 0; i < len; i++)
					{
						string word = words[i];
						if (type == "boolean")
							if (String.Compare(word, "and", true) == 0 ||
								String.Compare(word, "or", true) == 0 ||
								String.Compare(word, "not", true) == 0 ||
								String.Compare(word, "near", true) == 0)
								continue;

						words[i] = String.Format(@"""{0}""", word);
						if (i < len - 1)
						{
							if (type == "all") words[i] += " AND";
							else if (type == "any") words[i] += " OR";
						}
					}

					query += String.Format(fmt, String.Join(" ", words));
				}
				else if (type == "exact")
				{
					query += String.Format(fmt, text);
				}
				else if (type == "natural")
				{
					query += String.Format(" AND FREETEXT('{0}')", text);
				}

				query += String.Format(" ORDER BY {0} {1}",
				                       this.cboSortBy.SelectedItem.Value, this.CboSortOrder.SelectedItem.Value);

				Trace.Write("Query", query);
				return query;
			}
		}

		private void Search()
		{
			try
			{
				dbAdapter.SelectCommand.CommandText = Command;
				DataSet ds = new DataSet("Results");
				dbAdapter.Fill(ds);

				this.LblResultCount.ForeColor = Color.Black;
				int rows = ds.Tables[0].Rows.Count;
				this.LblResultCount.Text = String.Format("{0} document{1} found{2}",
				                                         rows, rows == 1 ? " was" : "s were", rows == 0 ? "." : ":");

				this.DgResultsGrid.DataSource = ds;
				this.DgResultsGrid.DataBind();

				this.DgResultsGrid.Visible = (rows > 0);
			}
			catch (Exception ex)
			{
				this.LblResultCount.ForeColor = Color.Red;
				this.LblResultCount.Text = String.Format("Unable to retreive a list " +
					"of documents for the specified query: {0}", ex.Message);

				this.DgResultsGrid.Visible = false;
			}
			finally
			{
				this.LblResultCount.Visible = true;
			}
		}

		protected object GetTitle(object value)
		{
			string title = DataBinder.Eval(value, "DocTitle") as string;
			if (title != null && title.Length > 0) return title;

			return DataBinder.Eval(value, "Filename");
		}

		protected string GetCharacterization(object value)
		{
			return Server.HtmlEncode(DataBinder.Eval(value, "Characterization") as string);
		}
	}
}
