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
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Brettle.Web.NeatUpload;
using Digita.Tustena.Base;
using Digita.Tustena.Common;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Lucene.Net.Parsing;
using Microsoft.Web.UI.WebControls;
using TreeView = Microsoft.Web.UI.WebControls.TreeView;
using TreeNode = Microsoft.Web.UI.WebControls.TreeNode;

namespace Digita.Tustena
{
	public partial class DataStorage : G
	{
		protected HtmlForm form;



		private string newFileName;


		protected Label label;


		public void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}
			else
			{
				FileRepPaging.Visible = false;
				DeleteGoBack();
				LbnBack.Click += new EventHandler(BtnBack_Click);
				LbnBack.Text = Root.rm.GetString("behind");
				if (!isGoBack) LbnBack.Visible = false;


				LbnSubmit.Text =Root.rm.GetString("Dsgtxt8");
				LbnNew.Text =Root.rm.GetString("Dsgtxt10");


				LbnSubmit.Attributes.Add("onClick", "return Presubmit(event)");

				if (!Page.IsPostBack)
				{
					searchType.Items.Insert(0,new ListItem(Root.rm.GetString("Dsgtxt11")));
					searchType.Items.Insert(1,new ListItem(Root.rm.GetString("Dsgtxt12")));
					searchType.Items[0].Selected=true;

					CreateTree(tvCategoryTree, 0, 0);
					CreateTree(tvCategoryTreeSearch, 0, 1);
					HelpLabel.Text = FillHelp("HelpDataStorage");
					fileTab.Visible = false;


					ListItem li = new ListItem();

					li.Value = "0";
					li.Text =Root.rm.GetString("Dsgtxt4");
					CrossWith.Items.Add(li);
					li = new ListItem();
					li.Value = "1";
					li.Text =Root.rm.GetString("Dsgtxt1");
					CrossWith.Items.Add(li);
					li = new ListItem();
					li.Value = "2";
					li.Text =Root.rm.GetString("Dsgtxt2");
					CrossWith.Items.Add(li);

                    Modules M = new Modules();
                    M.ActiveModule = UC.Modules;

                    if (M.IsModule(ActiveModules.Lead))
                    {
                        li = new ListItem();
                        li.Value = "3";
                        li.Text = Root.rm.GetString("Dsgtxt3");
                        CrossWith.Items.Add(li);
                    }
					li = new ListItem();
					li.Value = "4";
					li.Text =Root.rm.GetString("Dsgtxt9");
					CrossWith.Items.Add(li);
					CrossWith.Items[0].Selected = true;

					if (Request.Params["t"] != null)
					{
						switch (Request.Params["t"].ToString())
						{
							case "r":
								fileTab.Visible = true;
								FileRep.Visible = false;
								Info.Text = String.Empty;
								FileID.Text = "-1";
								CrossWith.Items[2].Selected = true;
								DataSet ds = DatabaseConnection.CreateDataset("SELECT ID,(NAME+' '+SURNAME) AS REFERENTE FROM BASE_CONTACTS WHERE ID=" + int.Parse(Session["CurrentRefId"].ToString()));
								CrossId.Text = ds.Tables[0].Rows[0]["id"].ToString();
								CrossText.Text = ds.Tables[0].Rows[0]["referente"].ToString();
								Session["BackAfterSubmit"] = true;
								break;
							case "c":
								fileTab.Visible = true;
								FileRep.Visible = false;
								Info.Text = String.Empty;
								FileID.Text = "-1";
								CrossWith.Items[1].Selected = true;
								ds = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + int.Parse(Session["contact"].ToString()));
								CrossId.Text = ds.Tables[0].Rows[0]["id"].ToString();
								CrossText.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
								Session["BackAfterSubmit"] = true;
								break;
							case "o":
								fileTab.Visible = true;
								FileRep.Visible = false;
								Info.Text = String.Empty;
								FileID.Text = "-1";
								try
								{
									CrossWith.Items[3].Selected = true;
									ds = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE ID=" + int.Parse(Session["OpportunityID"].ToString()));
									CrossId.Text = ds.Tables[0].Rows[0]["id"].ToString();
									CrossText.Text = ds.Tables[0].Rows[0]["title"].ToString();
									Session["BackAfterSubmit"] = true;
								}
								catch
								{
								}
								finally
								{
									Session.Remove("OpportunityID");
								}

								break;
							case "m":
								fileTab.Visible = true;
								FileRep.Visible = false;
								Info.Text = String.Empty;
								Session["BackAfterSubmit"] = true;
								int id = Convert.ToInt32(Session["IdDoc"]);
								Session["IdDoc"] = null;
								ModifyFile(id);
								break;
							case "e":
								fileTab.Visible = true;
								FileRep.Visible = false;
								Info.Text = String.Empty;
								Session["BackAfterSubmit"] = true;
								id = Convert.ToInt32(Session["IdDoc"]);
								Session["IdDoc"] = null;
								CreateRevision(id);
								break;
						}
					}
					if (Context.Items["NEW"] != null)
						if ((bool) Context.Items["NEW"])
						{
							InitProgressBar();
							NewFile();
						}


				}
				else
				{

					helptext.Visible = false;
				}
				CrossWith.Attributes.Add("onclick", "Cross(event);");
			}
		}



		public static void JsUpload(Page page)
		{
			string uploadId = Guid.NewGuid().ToString();
		}

		public void CreateTree(TreeView tree, int open, byte whichTree)
		{
			string queryCat;
			queryCat = "SELECT * FROM FILESCATEGORIES WHERE PARENTID = 0";
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			TreeNode tv1 = new TreeNode();

			switch (whichTree)
			{
				case 0:
					tv1.Text = "<a href=\"javascript:copyData('', '','CategoryText','CategoryId')\" style=\"color:black;text-decoration:none\">" +Root.rm.GetString("Dsttxt21") + "</a>";
					break;
				case 1:
					tv1.Text = "<a href=\"javascript:copyData('', '','CategoryTextSearch','CategoryIdSearch','BtnSearchText')\" style=\"color:black;text-decoration:none\">" +Root.rm.GetString("Dsttxt21") + "</a>";
					break;
			}

			tree.Nodes.Add(tv1);
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();


				switch (whichTree)
				{
					case 0:
						tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryText','CategoryId')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
						break;
					case 1:
						tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryTextSearch','CategoryIdSearch','BtnSearchText')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
						break;
				}

				tv.NodeData = dr["Id"].ToString();

				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open, whichTree); // Chiamata ricorsiva per fare le foglie
				tv.ImageUrl = "/webctrl_client/1_0/treeimages/folder.gif";
				tv.ExpandedImageUrl = "/webctrl_client/1_0/treeimages/folderopen.gif";
				tree.Nodes.Add(tv);
			}
			tree.CssClass = "normal";

		}

		public bool FillCategoryTree(int parent, TreeNode tvUp, int open, byte whichTree)
		{
			string queryCat;
			queryCat = "SELECT * FROM FILESCATEGORIES WHERE PARENTID = " + parent;
			DataSet dsC = DatabaseConnection.CreateDataset(queryCat);
			bool toExpand = false;
			foreach (DataRow dr in dsC.Tables[0].Rows)
			{
				TreeNode tv = new TreeNode();
				switch (whichTree)
				{
					case 0:
						tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryText','CategoryId')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
						break;
					case 1:
						tv.Text = "<a href=\"javascript:copyData('" + dr["Id"].ToString() + "', '" + dr["Description"].ToString() + "','CategoryTextSearch','CategoryIdSearch','BtnSearchText')\" style=\"color:black;text-decoration:none\">" + dr["Description"].ToString() + "</a>";
						break;
				}
				tv.NodeData = dr["Id"].ToString();
				if (!toExpand)
					toExpand = (open == Convert.ToInt32(dr["id"]));
				tv.Expanded = FillCategoryTree(Convert.ToInt32(dr["Id"]), tv, open, whichTree); // Recurring call to make the blatt
				if (tv.Expanded) toExpand = true;
				tv.ImageUrl = "/webctrl_client/1_0/treeimages/folder.gif";
				tv.ExpandedImageUrl = "/webctrl_client/1_0/treeimages/folderopen.gif";

				tvUp.Nodes.Add(tv);
			}
			return toExpand;
		}

		public void btn_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "LbnSubmit":


					if(this.inputFile.HasFile)
					{
						SaveFile(this.inputFile);
					}
					else
					{
						if (FileID.Text != "-1")
							SaveFile(this.inputFile);
						else
							Info.Text = "<img src=\"/i/alert.gif\">&nbsp;"+Root.rm.GetString("Dsttxt23");
					}

					break;

				case "LbnNew":
					InitProgressBar();
					NewFile();

					break;

				case "BtnSearchText":
					if(searchType.Items[0].Selected)
					{
						TxtSearch.Text=StaticFunctions.RemoveSpecialChar(TxtSearch.Text);
						BuildRepeaterQuery(String.Format("(FILEMANAGER.FILENAME LIKE '%{0}%' OR FILEMANAGER.DESCRIPTION LIKE '%{0}%')", TxtSearch.Text));
					}
					else
					{
						TxtSearch.Text=StaticFunctions.RemoveSpecialChar(TxtSearch.Text);
						string pathTemplate;
						pathTemplate = ConfigSettings.DataStoragePath;
						LuceneParser lucene = new LuceneParser(Path.Combine(pathTemplate, "luceneIdx"));
						DataTable dt = new DataTable();
						try
						{
							dt = lucene.search(TxtSearch.Text);
						}
						catch (IndexDamagedException)
						{
							lucene.addFolder(pathTemplate);
						}
						if (dt.Rows.Count > 0)
						{
							StringBuilder sb = new StringBuilder();
							foreach (DataRow dr in dt.Rows)
							{
								sb.AppendFormat(" OR FILEMANAGER.GUID = '{0}'", dr["title"]);
							}

							BuildRepeaterQuery(String.Format("({0})", sb.ToString().Substring(4)));
						}
					}
					break;
			}
		}

		private void BuildRepeaterQuery(string condition)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER FROM FILEMANAGER ");
			sb.Append("LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ");
			sb.Append("LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ");
			sb.AppendFormat("WHERE {0}", condition);

			if (CategoryIdSearch.Text.Length > 0)
			{
				sb.AppendFormat(" AND TYPE={0}", CategoryIdSearch.Text);
				CategoryIdSearch.Text = String.Empty;
			}

			sb.AppendFormat(" AND (FILEMANAGER.OWNERID={0} OR {1}) AND ISREVIEW=0", UC.UserId, GroupsSecure("FILEMANAGER.GROUPS"));
			ViewState["sqlsearch"] = sb.ToString();
			Session["review"] = "0";

			FillRep(sb.ToString());
		}

		private string GetHash(Stream stream)
		{
			return ("0x" + BitConverter.ToString(GetHashBytes(stream)).Replace("-", String.Empty));
		}


		private byte[] GetHashBytes(Stream stream)
		{
			stream.Position = 0;
			return (new MD5CryptoServiceProvider().ComputeHash(stream));
		}



		private void NewFile()
		{

			fileTab.Visible = true;
			FileRep.Visible = false;
			Info.Text = String.Empty;
			FileID.Text = "-1";
			FileRevision.Text = String.Empty;
			NRevision.Text = String.Empty;
			CrossWith.Items[0].Selected = true;

			Description.Text = String.Empty;
			CrossText.Text = String.Empty;
			CrossId.Text = String.Empty;

			LlblAction.Text = "&nbsp;-&nbsp;" +Root.rm.GetString("Dsttxt5");

			if (Context.Items["NEW"] != null)
			{
				if ((bool) Context.Items["NEW"])
				{
					CrossWith.Items[4].Selected = true;
					CrossText.Text = Context.Items["CrossText"].ToString();
					CrossId.Text = Context.Items["CrossID"].ToString();
				}
			}
		}

		private void InitProgressBar()
		{
		}

		private void FillRep(string sqlString)
		{
			FileRepPaging.PageSize = UC.PagingSize;
			FileRepPaging.RepeaterObj = FileRep;
			FileRepPaging.sqlRepeater = sqlString;
			FileRepPaging.BuildGrid();

			if (FileRep.Items.Count > 0)
			{
				FileRep.Visible = true;
				fileTab.Visible = false;
				Info.Text = String.Empty;
				FileRepPaging.Visible = true;
			}
			else
			{
				fileTab.Visible = false;
				FileRep.Visible = false;
				Info.Text = "<img src=\"/i/alert.gif\">&nbsp;"+Root.rm.GetString("Dsttxt17");
			}
		}

		public void FileRepCommand(object source, RepeaterCommandEventArgs e)
		{
			Trace.Warn(e.CommandName);
			switch (e.CommandName)
			{
				case "Down":
					Literal FileId = (Literal) e.Item.FindControl("FileId");
					DataSet ds = DatabaseConnection.CreateDataset("SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileId.Text));


					FileFunctions.CheckDir(ConfigSettings.DataStoragePath, true);

					string filename;
					filename = ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + ds.Tables[0].Rows[0]["guid"].ToString();
					string realFileName = ds.Tables[0].Rows[0]["filename"].ToString();

					string downFile = filename + Path.GetExtension(realFileName);

					if (File.Exists(downFile))
					{
						Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
						Response.ContentType = "application/octet-stream";
						Response.TransmitFile(downFile);
						Response.Flush();
						Response.End();
						return;

					}
					else if (File.Exists(filename))
					{
						File.Move(filename, downFile);
						Response.AddHeader("Content-Disposition", "attachment; filename=" + realFileName);
						Response.ContentType = "application/octet-stream";
						Response.TransmitFile(downFile);
						Response.Flush();
						Response.End();
						return;
					}
					else
					{
						G.SendError("File lost", downFile);
					}
					break;
				case "Delete":
					DeleteFile(int.Parse(((Literal) e.Item.FindControl("FileId")).Text));
					break;
				case "Modify":
					ModifyFile(int.Parse(((Literal) e.Item.FindControl("FileId")).Text));
					InitProgressBar();
					break;
				case "Revision":
					CreateRevision(int.Parse(((Literal) e.Item.FindControl("FileId")).Text));
					InitProgressBar();
					break;
				case "ReviewNumber":
					Session["review"] = "1";
                    StringBuilder sb = ReviewFileInfoQuery(Int16.Parse(((Literal)e.Item.FindControl("FileId")).Text), UC.UserId, GroupsSecure("FILEMANAGER.GROUPS"));
					Trace.Warn("sql", sb.ToString());
					ViewState["sqlsearch"] = sb.ToString();

					FillRep(sb.ToString());
					break;
			}
		}

        public static StringBuilder ReviewFileInfoQuery(int id, int userId, string group)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT FILEMANAGER.*, FILESCATEGORIES.DESCRIPTION AS CATDESC,(ACCOUNT.NAME+' '+ACCOUNT.SURNAME) AS OWNER, (ACCOUNT3.NAME+' '+ACCOUNT3.SURNAME) AS CREATOR, (ACCOUNT2.NAME+' '+ACCOUNT2.SURNAME) AS LASTMODIFIER FROM FILEMANAGER ");
            sb.Append("LEFT OUTER JOIN FILESCATEGORIES ON FILEMANAGER.TYPE=FILESCATEGORIES.ID ");
            sb.Append("LEFT OUTER JOIN ACCOUNT ON FILEMANAGER.OWNERID=ACCOUNT.UID ");
            sb.Append("LEFT OUTER JOIN ACCOUNT AS ACCOUNT2 ON FILEMANAGER.LASTMODIFIEDBYID=ACCOUNT2.UID ");
            sb.Append("LEFT OUTER JOIN ACCOUNT AS ACCOUNT3 ON FILEMANAGER.CREATEDBYID=ACCOUNT3.UID ");
            sb.AppendFormat("WHERE (FILEMANAGER.OWNERID={0} OR {1}) ", userId, group);
            sb.AppendFormat("AND (FILEMANAGER.ID={0} OR FILEMANAGER.ISREVIEW={0}) ", id);
            sb.Append("ORDER BY REVIEWNUMBER DESC");
            return sb;
        }



		public void FileRepDatabound(Object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
                    Literal Info = (Literal)e.Item.FindControl("Info");
                    Info.Text = "<img src=/i/info.gif border=0 onclick=\"CreateBox('PopFileInfo.aspx?render=no&fileid="+DataBinder.Eval((DataRowView)e.Item.DataItem, "id")+"',event,500,400)\" alt=\"Info\">";
                    LinkButton Del = (LinkButton)e.Item.FindControl("Delete");
                    Del.Attributes.Add("onclick", "return confirm('" + Root.rm.GetString("Dsgtxt5") + "');");
                    Del.Text = "<img src=/i/deletedoc.gif border=0 alt=\"" + Root.rm.GetString("Dsgtxt6") + "\">";
                    LinkButton Mod = (LinkButton)e.Item.FindControl("Modify");
					Mod.Text = "<img src=/i/modify2.gif border=0 alt=\"" +Root.rm.GetString("Dsttxt16") + "\">";
					LinkButton Rev = (LinkButton) e.Item.FindControl("Revision");
					Rev.Text = "<img src=/i/copy.gif border=0 alt=\"" +Root.rm.GetString("Dsttxt13") + "\">";

					LinkButton RN = (LinkButton) e.Item.FindControl("ReviewNumber");
					Literal LRN = (Literal) e.Item.FindControl("LtrReviewNumber");
					if (Session["review"].ToString() == "0")
					{
						RN.Visible = (RN.Text != "0");
						Rev.Visible = true;
						LRN.Visible = false;
					}
					else
					{
						RN.Visible = false;
						Rev.Visible = false;
						LRN.Visible = true;
					}

					LinkButton down = (LinkButton) e.Item.FindControl("down");
					string ext = Path.GetExtension(down.Text);
					Image FileImg = (Image) e.Item.FindControl("FileImg");
					FileImg.ImageUrl = FileFunctions.GetFileImg(ext);

					Literal FileSize = (Literal) e.Item.FindControl("FileSize");

					break;
			}
		}


		public void SaveFile(InputFile uploaded)
		{
			if(uploaded!=null)
			{
				try
				{
					string f = Path.GetFileName(inputFile.FileName);
				}catch(Exception ex)
				{
					ClientScript.RegisterStartupScript(this.GetType(), "fileerror","<script>alert('Attention: " + ex.Message + "');</script>");
					return;
				}
			}
			bool toUpDate = false;
			byte[] hashBytes = new byte[0];
			string shashBytes = string.Empty;
			if (CategoryId.Text.Length > 0)
			{
				string sqlString = "SELECT * FROM FILEMANAGER WHERE ID=" + int.Parse(FileID.Text);
				int NewRecordId = -1;
				using (DigiDapter dg = new DigiDapter(sqlString))
				{
					Guid g = Guid.NewGuid();


						if (uploaded != null)
						{
							uploaded.FileContent.Position = 0;
							hashBytes = GetHashBytes(uploaded.FileContent);
							shashBytes = GetHash(uploaded.FileContent);

							Guid existingfileid;
							object exist;
							exist = DatabaseConnection.SqlScalartoObj(string.Format("SELECT TOP 1 GUID FROM FILEMANAGER WHERE HASH=({0}) AND [SIZE]={1}", shashBytes, uploaded.FileContent.Length));

							if(exist!=null)
							{
								existingfileid = (Guid)exist;
								newFileName = existingfileid.ToString();
								dg.Add("GUID", existingfileid, 'I');
								toUpDate = false;
							}else
							{
								newFileName = g.ToString();
								dg.Add("GUID", g, 'I');
								toUpDate = true;
							}

						}


						dg.Add("OWNERID", UC.UserId, 'I');
						dg.Add("ISREVIEW", 0, 'I');
						dg.Add("REVIEWNUMBER", (NRevision.Text.Length > 0) ? NRevision.Text : "0", 'I');
						dg.Add("HAVEREVISION", false, 'I');
						dg.Add("CREATEDDATE", DateTime.UtcNow, 'I');
						dg.Add("CREATEDBYID", UC.UserId, 'I');

					newFileName = g.ToString();

					if (uploaded != null)
					{

						dg.Add("HASH", hashBytes, 'I');

						dg.Add("FILENAME", Path.GetFileName(inputFile.FileName));
						dg.Add("SIZE", uploaded.FileContent.Length);
					}

					dg.Add("DESCRIPTION", Description.Text);
					dg.Add("LASTMODIFIEDDATE", DateTime.UtcNow);
					dg.Add("LASTMODIFIEDBYID", UC.UserId);

					dg.Add("TYPE", CategoryId.Text);
					if (groupsDialog.GetValue.Length > 0)
					{
						dg.Add("GROUPS", groupsDialog.GetValue);
					}
					else
					{
						dg.Add("GROUPS", "|" + UC.UserGroupId.ToString() + "|");
					}
					NewRecordId = Convert.ToInt32( dg.Execute("FILEMANAGER", "ID=" + int.Parse(FileID.Text), DigiDapter.Identities.Identity));
				}

				if (Convert.ToInt32(CrossWith.SelectedValue) > 0)
				{
					using (DigiDapter dg = new DigiDapter())
					{
						dg.Add("IDFILE", NewRecordId, 'I');

						switch (CrossWith.SelectedValue)
						{
							case "1":
								dg.Add("TABLENAME", "Base_Companies");
								break;
							case "2":
								dg.Add("TABLENAME", "Base_Contacts");
								break;
							case "3":
								dg.Add("TABLENAME", "CRM_Opportunity");
								break;
							case "4":
								dg.Add("TABLENAME", "CRM_WorkActivity");
								DatabaseConnection.DoCommand("UPDATE CRM_WORKACTIVITY SET DOCID=" + NewRecordId + " WHERE ID=" + int.Parse(CrossId.Text));
								break;
						}
						dg.Add("IDRIF", Convert.ToInt64(CrossId.Text));
						dg.Execute("FILECROSSTABLES", "IDFILE=" + ((int.Parse(FileID.Text)<=0)?NewRecordId.ToString():FileID.Text));
					}
				}

				if (FileRevision.Text.Length > 0)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append("UPDATE FILEMANAGER SET ");
					sb.AppendFormat("ISREVIEW={0} ", NewRecordId);
					sb.AppendFormat("WHERE ID={0} OR ISREVIEW={0}", int.Parse(FileRevision.Text));

					DatabaseConnection.DoCommand(sb.ToString());

				}
				if (uploaded != null && toUpDate)
				{
						if (FileFunctions.CheckDir(ConfigSettings.DataStoragePath, true))
					{
						string pathTemplate;
						pathTemplate = ConfigSettings.DataStoragePath;
						string saveFile = Path.Combine(pathTemplate, newFileName + Path.GetExtension(inputFile.FileName));
						uploaded.FileContent.Close();
						uploaded.MoveTo(saveFile, MoveToOptions.Overwrite);

						InitProgressBar();
						AddToLucene(pathTemplate, saveFile);
					}
				}else
				{
					if (uploaded != null)
						uploaded.FileContent.Close();
				}
				Info.Text = "<img src=\"/i/ok.gif\">&nbsp;"+Root.rm.GetString("Dsttxt18");
			}
			else
			{
				Info.Text = "<img src=\"/i/alert.gif\">&nbsp;"+Root.rm.GetString("Dsttxt19");
			}

		}


		private static void AddToLucene(string pathTemplate, string saveFile)
		{
			LuceneParser lucene = new LuceneParser(Path.Combine(pathTemplate, "luceneIdx"));
			try
			{
				lucene.AddFile(saveFile);
			}
			catch (IndexDamagedException)
			{
				lucene.addFolder(pathTemplate);
			}

			lucene = null;
		}


		private void DeleteFile(int id)
		{
			string sqlString = "SELECT * FROM FILEMANAGER WHERE ID=" + id + " OR ISREVIEW=" + id;
			string sqlString2 = "DELETE FILEMANAGER WHERE ID=" + id + " OR ISREVIEW=" + id;
			DataSet myDataSet = DatabaseConnection.CreateDataset(sqlString);
			if (myDataSet.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow d in myDataSet.Tables[0].Rows)
				{
					File.Delete(ConfigSettings.DataStoragePath + Path.DirectorySeparatorChar + d["guid"].ToString());
				}
				DatabaseConnection.DoCommand(sqlString2);
			}
			FillRep(ViewState["sqlsearch"].ToString());
		}

		private void CreateRevision(int id)
		{
			string sqlString = "SELECT *,FILECROSSTABLES.TABLENAME, FILECROSSTABLES.IDRIF FROM FILEMANAGER ";
			sqlString += "LEFT OUTER JOIN FILECROSSTABLES ON FILEMANAGER.ID=FILECROSSTABLES.IDFILE ";
			sqlString += "WHERE FILEMANAGER.ID=" + id;
			Trace.Warn("", sqlString);
			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			DataRow dr = ds.Tables[0].Rows[0];
			FileID.Text = "-1";
			FileRevision.Text = id.ToString();
			NRevision.Text = (Convert.ToInt32(dr["ReviewNumber"]) + 1).ToString();
			CategoryId.Text = dr["type"].ToString();
			CategoryText.Text = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM FILESCATEGORIES WHERE ID=" + dr["type"].ToString());

			Description.Text = dr["description"].ToString();
			if (dr["TableName"].ToString().Length > 0)
			{
				switch (dr["TableName"].ToString().ToLower())
				{
					case "Base_Companies":
						CrossWith.Items[1].Selected = true;
						DataSet dsCross = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["CompanyName"].ToString();
						break;
					case "Base_Contacts":
						CrossWith.Items[2].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,(NAME+' '+SURNAME) AS CONTACT FROM BASE_CONTACTS WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["contact"].ToString();
						break;
					case "crm_opportunity":
						CrossWith.Items[3].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["Title"].ToString();
						break;
					case "CRM_WorkActivity":
						CrossWith.Items[4].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM CRM_WORKACTIVITY WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["Subject"].ToString();
						break;
				}
			}
			fileTab.Visible = true;
			FileRep.Visible = false;
			Info.Text = String.Empty;
			FileID.Text = "-1";
			LlblAction.Text = String.Format("&nbsp;-&nbsp;{0}&nbsp;{1}",Root.rm.GetString("Dsttxt15"), dr["filename"].ToString());
		}

		private void ModifyFile(int id)
		{
			string sqlString = "SELECT *,FILECROSSTABLES.TABLENAME, FILECROSSTABLES.IDRIF FROM FILEMANAGER ";
			sqlString += "LEFT OUTER JOIN FILECROSSTABLES ON FILEMANAGER.ID=FILECROSSTABLES.IDFILE ";
			sqlString += "WHERE FILEMANAGER.ID=" + id;

			DataSet ds = DatabaseConnection.CreateDataset(sqlString);
			DataRow dr = ds.Tables[0].Rows[0];
			FileID.Text = id.ToString();
			FileRevision.Text = String.Empty;
			NRevision.Text = dr["ReviewNumber"].ToString();

			CategoryId.Text = dr["type"].ToString();
			CategoryText.Text = DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM FILESCATEGORIES WHERE ID=" + dr["type"].ToString());

			Description.Text = dr["description"].ToString();
			if (dr["TableName"].ToString().Length > 0)
			{
				switch (dr["TableName"].ToString().ToLower())
				{
					case "Base_Companies":
						CrossWith.Items[1].Selected = true;
						DataSet dsCross = DatabaseConnection.CreateDataset("SELECT ID,COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["CompanyName"].ToString();
						break;
					case "Base_Contacts":
						CrossWith.Items[2].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,(NAME+' '+SURNAME) AS CONTACT FROM BASE_CONTACTS WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["contact"].ToString();
						break;
					case "crm_opportunity":
						CrossWith.Items[3].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,TITLE FROM CRM_OPPORTUNITY WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["Title"].ToString();
						break;
					case "CRM_WorkActivity":
						CrossWith.Items[4].Selected = true;
						dsCross = DatabaseConnection.CreateDataset("SELECT ID,SUBJECT FROM CRM_WORKACTIVITY WHERE ID=" + dr["IDRif"].ToString());
						CrossId.Text = dsCross.Tables[0].Rows[0]["id"].ToString();
						CrossText.Text = dsCross.Tables[0].Rows[0]["Subject"].ToString();

						break;
				}
			}
			fileTab.Visible = true;
			FileRep.Visible = false;
			Info.Text = String.Empty;
			LlblAction.Text = "&nbsp;-&nbsp;" +Root.rm.GetString("Dsttxt16") + "&nbsp;" + dr["filename"].ToString();
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
			this.PreRender += new EventHandler(this.DataStorage_PreRender);

		}

		#endregion

		private void DataStorage_PreRender(object sender, EventArgs e)
		{
			if(groupsDialog.Visible)
				groupsDialog.SetGroups("|" + UC.UserGroupId + "|");
		}
	}
}
