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
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.WebControls
{

	public delegate void RepeaterPostDelegate(string sender);

	[ParseChildren(true)]
	public class TustenaRepeater : UserControl, INamingContainer , IRequiresSessionState
	{
		public event RepeaterPostDelegate RepeaterPost;

		private HttpContext current = HttpContext.Current;

		private Repeater innerRepeater;
		private PagedDataSource repSource = new PagedDataSource();

		public bool isDatabinded = false;

		private ITemplate itemTemplate = null;
		private ITemplate headerTemplate = null;
		private ITemplate footerTemplate = null;
		private ITemplate alternatingItemTemplate = null;

		private bool usePagedDataSource = true;
		public bool UsePagedDataSource
		{
			get { return usePagedDataSource; }
			set { usePagedDataSource = value; }
		}
		public string PageChangeHolder = null;
		public string DefaultSortingColumn = null;

		private string cssClass = string.Empty;
		public string CssClass
		{
			get { return cssClass; }
			set { cssClass = value; }
		}

		private string width = string.Empty;
		public string Width
		{
			get { return width; }
			set { width = value; }
		}

		private RepeaterCommandEventHandler onItemCommand;

		public event RepeaterCommandEventHandler ItemCommand
		{
			add { onItemCommand += value; }
			remove { onItemCommand -= value; }
		}

		private RepeaterItemEventHandler onItemDataBound;

		public event RepeaterItemEventHandler ItemDataBound
		{
			add { onItemDataBound += value; }
			remove { onItemDataBound -= value; }
		}

        internal bool useMultiDelete = false;
        Hashtable multiDeleteTbl = null;

		public RepeaterAlphabet ra;
		public RepeaterSearch rs;
		public RepeaterBottom rb;
		public LiteralControl lc;

        public string FilterValue
        {
            get
            {
                object o = this.ViewState[this.ClientID + "_FilterValue"];
                if (o == null)
                {
                    return string.Empty;
                }
                else
                    return (string)o;
            }
            set { this.ViewState[this.ClientID + "_FilterValue"] = value; }
        }
		private int currentPage = 1;

		public int CurrentPage
		{
			get {
				return currentPage;
			}
			set { currentPage = value; }
		}

		public PagedDataSource RepSource
		{
			get
			{
				if (repSource.DataSource == null)
				{
					repSource.DataSource = this.DataSource.DefaultView;
				}
				return repSource;
			}
		}



		public int PageSize
		{
			get
			{
					object o = this.ViewState[this.ClientID+"_PageSize"];
					if (o == null)
					{
						return 100;
					}
					else
						return (int) o;
			}
			set { this.ViewState[this.ClientID+"_PageSize"] = value; }
		}


		private string table = null;

		public string Table
		{
			get { return table; }
			set { table = value; }
		}

		private string tableId = "id";

		public string TableId
		{
			get { return tableId; }
			set { tableId = value; }
		}

		private string columns = "*";

		public string Columns
		{
			get { return columns; }
			set { columns = value; }
		}

		private string filterCol;
		public string FilterCol
		{
			get { return filterCol; }
			set { filterCol = value; }
		}

		private bool allowPaging = false;

		public bool AllowPaging
		{
			get { return allowPaging; }
			set { allowPaging = value; }
		}


		private bool allowSearching = true;

		public bool AllowSearching
		{
			get { return allowSearching; }
			set { allowSearching = value; }
		}

		private bool allowAlphabet = true;

		public bool AllowAlphabet
		{
			get { return allowAlphabet; }
			set { allowAlphabet = value; }
		}

		public ArrayList multiDeleteListArray = null;
		public ArrayList MultiDeleteListArray
		{
			get{return multiDeleteListArray;}
		}

		public int PageCount
			{
			get
			{
				if(this.usePagedDataSource)
				{
					return RepSource.PageCount;
				}
				else
				{
					object o = this.ViewState[this.ClientID+"_PageCount"];
					if (o == null)
					{
						AllowPaging = false;
						return -1;
					}
					else
						return (int) o;
				}
			}
			set { this.ViewState[this.ClientID+"_PageCount"] = value; }
		}

		public int RowCount
		{
			get
			{
				if(this.usePagedDataSource)
				{
					return RepSource.DataSourceCount;
				}
				else
				{
					object o = this.ViewState[this.ClientID+"_RowCount"];
					if (o == null)
					{
						return -1;
					}
					else
						return (int) o;
				}
			}
			set { this.ViewState[this.ClientID+"_RowCount"] = value; }
		}


		public void HeaderClick(object sender, EventArgs e)
		{
			RepeaterHeaderLink lb = (RepeaterHeaderLink) sender;
			if (SortColumn == lb.DataCol)
			{
				if (SortDirection.ToLower() != "asc")
				{
					SortDirection = "asc";
				}
				else
				{
					SortDirection = "desc";
				}
			}
			else
			{
				SortDirection = "asc";
			}

			SortColumn = lb.DataCol;

			if (this.SortColumn != "")
			{
				string sortStr = this.SortColumn + " " + this.SortDirection.ToUpper();
				DataSource.DefaultView.Sort = sortStr;
			}

			DataBind();
		}

		public void ApplySort()
		{
			if (SortColumn != "" && SortDirection != "")
			{
				DataSource.DefaultView.Sort = this.SortColumn + " " + this.SortDirection.ToUpper();
			}
		}

		public string SortDirection
		{
			get
			{
				object o = this.ViewState[this.ClientID+"_SortDir"];
				if (o == null)
					return "asc";
				else
					return (string) o;
			}
			set { this.ViewState[this.ClientID+"_SortDir"] = value; }
		}


		public string SortColumn
		{
			get
			{
				object o = this.ViewState[this.ClientID+"_SortField"];
				if (o == null)
					return "";
				else
					return (string) o;
			}
			set { this.ViewState[this.ClientID+"_SortField"] = value; }
		}

		[
			PersistenceMode(PersistenceMode.InnerProperty),
				TemplateContainer(typeof (RepeaterItem))
			]
		public ITemplate ItemTemplate
		{
			get { return itemTemplate; }
			set { itemTemplate = value; }
		}

		[
			PersistenceMode(PersistenceMode.InnerProperty),
				TemplateContainer(typeof (RepeaterItem))
			]
		public ITemplate AlternatingItemTemplate
		{
			get { return alternatingItemTemplate; }
			set { alternatingItemTemplate = value; }
		}


		[
			PersistenceMode(PersistenceMode.InnerProperty),
				TemplateContainer(typeof (RepeaterItem))
			]
		public ITemplate FooterTemplate
		{
			get { return footerTemplate; }
			set { footerTemplate = value; }
		}


		[
			PersistenceMode(PersistenceMode.InnerProperty),
				TemplateContainer(typeof (RepeaterItem))
			]
		public ITemplate HeaderTemplate
		{
			get { return headerTemplate; }
			set { headerTemplate = value; }
		}

		public int OldPage
		{
			get
			{
				int oldPage = 0;
				if (ViewState[this.ClientID + "_ViewPage"] != null)
				{
					oldPage = int.Parse(ViewState[this.ClientID + "_ViewPage"].ToString());
				}
				return oldPage;
			}
		}
		public override void DataBind()
		{
			isDatabinded = true;
			EnsureChildControls();
			base.DataBind();


			if(usePagedDataSource)
				repSource.DataSource = this.DataSource.DefaultView;

            ApplySort();

			if (allowPaging)
			{
				if(usePagedDataSource)
				{
					repSource.PageSize = this.PageSize;
					repSource.AllowPaging = true;
					int newPage = CurrentPage - 1;

					if (newPage >= repSource.PageCount)
					{
						newPage = repSource.PageCount - 1;
						CurrentPage = newPage + 1;
					}

					if (newPage < 0)
					{
						newPage = 0;
						CurrentPage = newPage + 1;
					}

					repSource.CurrentPageIndex = newPage;
				}else
				{
				}
				ViewState.Add(this.ClientID + "_ViewPage", CurrentPage);
			}
		if(usePagedDataSource)
			innerRepeater.DataSource = repSource;
				else
			innerRepeater.DataSource = DataSource.DefaultView;
			innerRepeater.DataBind();

			if ((usePagedDataSource && repSource.Count==0) || (!usePagedDataSource && DataSource.DefaultView.Count == 0))
			{
				lc.Visible = true;
				innerRepeater.Visible=false;
			}
			else
			{
				lc.Visible = false;
				innerRepeater.Visible=true;
			}

			if (rb != null)
			{
				rb.SetPageNums();
			}
		}

		public string sqlDataSource
		{
			get
			{
				object o = this.ViewState[this.ClientID+"_sqlDataSource"];
				if (o == null)
					return "";
				else
					return (string) o;
			}
			set { this.ViewState[this.ClientID+"_sqlDataSource"] = value; }
		}

		private string ParseSqlForLimits(string sql, int rowOffset, int rowCount,string kId)
		{
			string regstr = @"^(SELECT\s)([\w\.*]+)(.+?FROM\s)([\w\.]+)(.+?)(WHERE\s)(.+?)(\sORDER BY\s(.*))?(;?)$";
			string sqlId = "$2";

			string sqlEnd = String.Empty;
			string rep1 = "$1TOP {2} {0},$2$3$4$5$6({0} NOT IN (SELECT TOP {1} {0} FROM $4$5 ORDER BY {3})) AND $7 ORDER BY {3}";
			Regex rx = new Regex(regstr,RegexOptions.IgnoreCase);
			Match m = rx.Match(sql);
			int matchGroups = m.Groups.Count;
			if(m.Groups[2].Value.ToUpper()=="TOP")
				throw new AmbiguousMatchException("Top");
			if(rowOffset==1)
				sqlEnd = ";SELECT COUNT(*) FROM $4 $6$7";
			if(m.Groups[2].Value=="*")
				sqlId = kId;
			if(m.Groups[m.Groups.Count-1].Value==";")
			{
				matchGroups--;
			}
			string sqlOrder = sqlId;
			if(matchGroups==11)
			{
				sqlOrder = m.Groups[9].Value.Split(' ')[0].Replace(",","");
				if(sqlOrder.Length==0)
					sqlOrder=sqlId;
			}
			return rx.Replace(sql,String.Format(rep1+sqlEnd,sqlId, (rowOffset-1)*rowCount, rowCount,sqlOrder));
		}

		private string SqlPagerBuilder(string sql, string table, string kId, int fromPage, int rowCount, string columns, string sorting)
		{
			string SqlQuery;
			if(sql == null)
			{
				if(fromPage == 1)
					SqlQuery = string.Format("SELECT TOP {2} {3} FROM {0} ORDER BY {4};SELECT COUNT({3}) FROM {0};",table,kId,rowCount,columns,sorting);
				else
					SqlQuery = string.Format("SELECT TOP {3} {4} FROM {0} WHERE {1} NOT IN (SELECT TOP {2} {1} FROM {0} ORDER BY {5}) ORDER BY {5};",table,kId,(fromPage-1)*rowCount,rowCount,columns,sorting);
			}else
			{
					SqlQuery = ParseSqlForLimits(sql,fromPage,rowCount,kId);
			}
				return SqlQuery;
		}


		internal DataTable dataSource = null;
		public DataTable DataSource
		{
			get
			{
				if(dataSource != null)
					return dataSource;
				dataSource = new DataTable();
				if(this.usePagedDataSource)
				{

					dataSource=DatabaseConnection.CreateDataset(sqlDataSource).Tables[0];
				}
				else
				{
					string sortStr;
					if(this.SortColumn.Length==0)
						sortStr = tableId;
					else
						sortStr = this.SortColumn + " " + this.SortDirection.ToUpper();
					string limitDataSource;
					try
					{
						limitDataSource = SqlPagerBuilder(sqlDataSource, table, tableId, currentPage, PageSize, columns,sortStr);
					}
					catch(AmbiguousMatchException)
					{
						limitDataSource = sqlDataSource;
					}

					DataSet ds = DatabaseConnection.CreateDataset(limitDataSource);
					dataSource = ds.Tables[0];
					if(ds.Tables.Count==2){
					PageCount = ((int)ds.Tables[1].Rows[0][0] / this.PageSize)+1;
					RowCount = (int)ds.Tables[1].Rows[0][0];
									 }else if(RowCount == -1)
									 {
									 	RowCount = ds.Tables[0].Rows.Count;
									 }
				}
				return dataSource;
			}
			set
			{
				dataSource=value;
			}
		}


        public void DoSearch(string colName, string val, bool alphabet)
        {
            if (!alphabet)
            {
                this.rs.Visible = false;
            }
            this.FilterValue = val = val.Replace("'", "");

            if (alphabet)
            {
                this.DataSource.DefaultView.RowFilter = colName + " like '" + val + "%'";
            }
            else
            {
                this.DataSource.DefaultView.RowFilter = colName + " like '%" + val + "%'";
            }

            if (allowPaging)
            {
                string evTarget = current.Request.Form["__EVENTTARGET"];
                if (!(evTarget.IndexOf(this.ClientID + "GoToPage_") != -1 || (evTarget.IndexOf(this.ClientID) !=-1 && ( evTarget.EndsWith("nextBtn") || evTarget.EndsWith("backBtn")))))
                {
                    DataBind();
                }
            }
        }

		public void RemoveFilter()
		{
			if(this.rs!=null)this.rs.Visible = false;

			this.DataSource.DefaultView.RowFilter = "";
			DataBind();
		}

		public void PageChangeHandler(object sender, EventArgs e)
		{
			DoPageChange(((LinkButton) sender).Text);
		}
		public void DoPageChange(string page)
		{
			CurrentPage = int.Parse(page);
            if (FilterValue != null)
            {
                this.DataSource.DefaultView.RowFilter = this.filterCol + " like '" + FilterValue + "%'";
            }
			DataBind();
		}

		protected override void OnLoad(EventArgs e)
		{
			if(PageChangeHolder!=null)
			{
				DoPageChange(PageChangeHolder);
				RepeaterPostEvent();
			}

			base.OnLoad (e);
		}


		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);

			if (this.allowAlphabet)
			{
				ra = (RepeaterAlphabet) Page.LoadControl("~/Controls/TustenaRepeater/RepeaterAlphabet.ascx");
				this.Controls.Add(ra);

				if (this.allowSearching)
				{
					rs = (RepeaterSearch) Page.LoadControl("~/Controls/TustenaRepeater/RepeaterSearch.ascx");
					this.Controls.Add(rs);
					rs.Visible = false;
				}
			}
			innerRepeater = new Repeater();
			innerRepeater.ID = this.ClientID + "_Repeater";


			innerRepeater.ItemDataBound +=new RepeaterItemEventHandler(innerRepeater_ItemDataBound);

			innerRepeater.ItemCommand +=new RepeaterCommandEventHandler(innerRepeater_ItemCommand);
			if (HeaderTemplate != null)
			{
				innerRepeater.HeaderTemplate = HeaderTemplate;
			}
			if (ItemTemplate != null)
			{
				innerRepeater.ItemTemplate = ItemTemplate;
			}
			if (AlternatingItemTemplate != null)
			{
				innerRepeater.AlternatingItemTemplate = AlternatingItemTemplate;
			}
			if (FooterTemplate != null)
			{
				innerRepeater.FooterTemplate = FooterTemplate;
			}
			else if (allowPaging)
			{
				innerRepeater.FooterTemplate = Page.LoadTemplate("~/Controls/TustenaRepeater/RepeaterBottom.ascx");
			}
			Controls.Add(innerRepeater);


			lc = new LiteralControl("<center><span class='repNormaltext' style='color:red'>"+Core.Root.rm.GetString("NoData")+"</span></center>");
			lc.Visible = false;
			Controls.Add(lc);

			if (allowPaging)
			{


				if (current.Request.Form["__EVENTTARGET"] != null)
				{
					string evTarget = current.Request.Form["__EVENTTARGET"];
					if (evTarget.IndexOf(this.ClientID+"GoToPage_") != -1)
					{
						int indexLast = evTarget.LastIndexOf("_") + 1;
						int lengthOfPage = evTarget.Length - indexLast;
						PageChangeHolder = evTarget.Substring(indexLast, lengthOfPage);
					}
				}
			}
		}

		internal void RepeaterPostEvent()
		{
			if(RepeaterPost!=null)
				RepeaterPost(this.ID);
		}
        private void innerRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    if (useMultiDelete)
                    {
                        if(multiDeleteTbl==null)
                            multiDeleteTbl = new Hashtable();
                        multiDeleteTbl.Add(this.ClientID + "_md" + e.Item.ItemIndex, (DataBinder.Eval((DataRowView)e.Item.DataItem, "ID")).ToString());
                    }
                    break;
            }
            if (this.onItemDataBound != null)
            {
                this.onItemDataBound(this, e);
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            if (useMultiDelete && multiDeleteTbl != null)
                ViewState.Add(this.ClientID + "_md", multiDeleteTbl);
            base.OnPreRender(e);
        }



		public void innerRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "MultiDeleteButton":
                    if (ViewState[this.ClientID + "_md"] == null)
                        return;
                    Hashtable delQueue = ((Hashtable)ViewState[this.ClientID + "_md"]);
					if(multiDeleteListArray==null)
						multiDeleteListArray=new ArrayList();
					for(int i=0; i<innerRepeater.Items.Count;i++)
						if(Request.Form["md_"+i]=="on")
						{
                            if (delQueue.Contains(this.ClientID + "_md" + innerRepeater.Items[i].ItemIndex))
                                multiDeleteListArray.Add(delQueue[this.ClientID + "_md" + innerRepeater.Items[i].ItemIndex]);
						}
				break;
			}


			if (this.onItemCommand != null)
			{
				this.onItemCommand(this, e);
			}
		}
	}
}
