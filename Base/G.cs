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

#define    VIEWSTATETOSQLSERVER
using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.IO;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using System.Web.UI.HtmlControls;

namespace Digita.Tustena
{
	public class G : Page, ITransaction
	{
		public static DateTimeFormatInfo InvariantCultureForDB; //new CultureInfo("en-US").DateTimeFormat;
		public UserConfig UC;
		public string mode = ConfigSettings.Mode;

		public ResourceManager wrm
		{
			get { return Core.Root.rm; }
		}

		public G()
		{
		}

		protected override void OnInit(EventArgs e)
		{
			Trace.Warn("G Costruttore");
			DatabaseConnection.needTransaction = true;
			InvariantCultureForDB = CultureInfo.InvariantCulture.DateTimeFormat;
			UC = new UserConfig();
			base.OnInit(e);
		}

		protected override void OnUnload(EventArgs e)
		{
			DatabaseConnection.CommitTransaction();
			base.OnUnload(e);
		}

		protected override void OnError(EventArgs e)
		{
			DatabaseConnection.RollBackTransaction();
			base.OnError(e);
		}

		public UserConfig CurrentUC
		{
			get { return (UserConfig) HttpContext.Current.Session["UserConfig"]; }
		}

		public static string GetGroupDescription(int groupId)
		{
			return DatabaseConnection.SqlScalar("SELECT DESCRIPTION FROM GROUPS WHERE ID=" + groupId);
		}


		public static string ParseJSString(string s)
		{
			s = s.Replace("'", "\\\'");
			s = s.Replace("\"", "&quot;");
			s = s.Replace("\n", " ");
			s = s.Replace("\r", "");
			return s;
		}

		public string Capitalize(string s)
		{
			return StaticFunctions.Capitalize(s);
		}

		public static string FixCarriage(string text)
		{
			return StaticFunctions.FixCarriage(text);
		}

		public static string FixCarriage(string text, bool js)
		{
			return StaticFunctions.FixCarriage(text, js);
		}


		private LosFormatter _formatter = null;


		protected override void SavePageStateToPersistenceMedium(object viewState)
		{
			string str = "VIEWSTATE#" + Session.SessionID.ToString() + "#" + DateTime.Now.Ticks.ToString();
			ClientScript.RegisterHiddenField("__VIEWSTATE_KEY", str);
            ClientScript.RegisterHiddenField("__VIEWSTATE", "");

			if (_formatter == null)
			{
				_formatter = new LosFormatter();
			}

#if VIEWSTATETOSQLSERVER
			StringWriter _writer = new StringWriter();
			_formatter.Serialize(_writer, viewState);

			DbSqlParameterCollection Msc = new DbSqlParameterCollection();


			DbSqlParameter ViewPage = new DbSqlParameter("@Page", SqlDbType.VarChar, 50);
			if (Session["ViewStatePage"] != null)
			{
				ViewPage.Value = Session["ViewStatePage"].ToString();
				Session["ViewStatePage"] = null;
			}
			else
			{
				ViewPage.Value = Request.Path.ToString();
			}
			Msc.Add(ViewPage);

			DbSqlParameter UserId = new DbSqlParameter("@UserId", SqlDbType.Int, 4);
			UserId.Value = UC.UserId;
			Msc.Add(UserId);

			DbSqlParameter SessionId = new DbSqlParameter("@SessionId", SqlDbType.VarChar, 55);
			SessionId.Value = str;
			Msc.Add(SessionId);
			DbSqlParameter ViewStateObj = new DbSqlParameter("@ViewState", SqlDbType.Text);
			ViewStateObj.Value = _writer.ToString();
			Msc.Add(ViewStateObj);

			try
			{
				try
				{
					DatabaseConnection.DoStored("ViewStateMgr", Msc);
				}
				catch (Exception e)
				{
					G.SendError("[Tustena] DataBase Error", e.Message);
					Response.Clear();
					Response.Write(String.Format(Root.rm.GetString("DbConnetionError"), e.Message));
					Response.End();
				}
			}
			catch
			{
				DatabaseConnection.DoCommand("DELETE FROM VIEWSTATEMANAGER WHERE SESSIONID='" + Session.SessionID.ToString() + "'");
			}
			finally
			{
			}
#else
			Cache.Add(str, viewState, null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Default, null);
#endif
		}

		protected override object LoadPageStateFromPersistenceMedium()
		{
			if (_formatter == null)
			{
				_formatter = new LosFormatter();
			}

			string str = Request.Form["__VIEWSTATE_KEY"];
			if (!str.StartsWith("VIEWSTATE#"))
			{
				throw new Exception("Invalid viewstate key:" + str);
			}
#if VIEWSTATETOSQLSERVER
			DbSqlParameterCollection Msc = new DbSqlParameterCollection();

			DbSqlParameter SessionId = new DbSqlParameter("@SessionId", SqlDbType.VarChar, 55);
			SessionId.Value = str; //Session.SessionID.ToString();
			Msc.Add(SessionId);

			string viewState = String.Empty;
			try
			{
				viewState = (string) DatabaseConnection.DoStoredScalar("LoadViewStateMgr", Msc, false);
			}
			catch (Exception ex)
			{
				SendError("LoadPageStateFromPersistenceMedium", ex.ToString() + "<br><br>"); // + error);

			}
			finally
			{
			}
			if (viewState == null)
			{
				Response.Redirect(Request.UrlReferrer.ToString());
				Response.End();
			}
			return _formatter.Deserialize(viewState);
#else
			if (!str.StartsWith("VIEWSTATE#"))
			{
				throw new Exception("Invalid viewstate key:" + str);
			}
			return Cache[str];
#endif
		}

		public bool Login()
		{
			if(Request.Params["logoff"]!=null && Request.Params["logoff"].Length>0)
			{
				switch(Request.Params["logoff"])
				{
					case "true":
						UC = HttpContext.Current.Session["UserConfig"] as UserConfig;
						if (UC != null)
						{
							DatabaseConnection.DoCommand("UPDATE Account set State=0 WHERE uid=" + UC.UserId);
						}
						break;
					case "reentered":
						Context.Items.Add("warning", wrm.GetString("Reentered"));
						break;
				}





				if (UC != null)
				{
					DatabaseConnection.DoCommand("UPDATE ACCOUNT SET STATE=0 WHERE UID=" + UC.UserId);
				}
				Session.Abandon();
                return false;


            }
			else
			{
				if (Session["UserConfig"] != null)
				{
					UC = HttpContext.Current.Session["UserConfig"] as UserConfig;
					IsAlreadyIn();
					return true;
				}
				else
					return false;
			}
		}

		public static void NoRender()
		{
			if (!HttpContext.Current.Items.Contains("render"))
				HttpContext.Current.Items.Add("render", "no");
		}



		public static DataSet FixDateTimeZone(DataSet dt, SimpleTimeZone LTZ)
		{
			foreach (DataColumn cc in dt.Tables[0].Columns)
			{
				if (cc.DataType.FullName.Equals("System.DateTime"))
				{
					foreach (DataRow dr in dt.Tables[0].Rows)
					{
						if (dr[cc.ColumnName] != DBNull.Value)
							dr[cc.ColumnName] = LTZ.ToLocalTime((DateTime) dr[cc.ColumnName]);
					}
				}
			}
			return dt;
		}


		public static string CheckGroup(string table, string dependency)
		{
			string[] arryD = dependency.Split('|');
			StringBuilder sbGroup = new StringBuilder();
			foreach (string ut in arryD)
			{
				if (StaticFunctions.IsBlank(table))
				{
					if (ut.Length > 0) sbGroup.AppendFormat("GROUPS LIKE '%|{0}|%' OR ", ut);
				}
				else
				{
					if (ut.Length > 0) sbGroup.AppendFormat("{0}.GROUPS LIKE '%|{1}|%' OR ", table, ut);
				}
			}
			if (sbGroup.Length > 0) sbGroup.Remove(sbGroup.Length - 3, 3);
			if (StaticFunctions.IsBlank(table))
				return "GROUPS LIKE '%|0|%' OR (" + sbGroup.ToString() + ")";
			else
				return table + ".GROUPS LIKE '%|0|%' OR (" + sbGroup.ToString() + ")";
		}

		public static bool isGoBack
		{
			get { return (HttpContext.Current.Session["goback1"] is Stack && ((Stack) HttpContext.Current.Session["goback1"]).Count > 0); }
		}

		public static void SetGoBack(string sheet, string[] parameters)
		{
			Stack backSheet = new Stack();
			if (isGoBack)
				backSheet = (Stack) HttpContext.Current.Session["goback1"];
			GoBack ba = new GoBack();
			ba.sheet = sheet;
			foreach (string s in parameters)
				ba.parameter += "|" + s;
			ba.parameter += "|";
			backSheet.Push(ba);
			HttpContext.Current.Session["goback1"] = backSheet;
		}

		public void BtnBack_Click(object sender, EventArgs e)
		{
			if (isGoBack)
			{
				GoBackClick();
			}
			else
			{
			}
		}

		public void GoBackClick()
		{
			GoBackClick(false);
		}

		public void GoBackClick(bool remove)
		{
			if (Session["goback1"] == null) return;
			Stack ba = new Stack();
			ba = (Stack) Session["goback1"];
			if (ba.Count == 0) return;
			GoBack gb = new GoBack();
			if (remove)
				gb = (GoBack) ba.Pop(); //(GoBack) ba[ba.Count - 1];
			else
				gb = (GoBack) ba.Peek();
			Response.Redirect(gb.sheet);
		}


		public void DeleteGoBack(bool nockeck)
		{
			if (nockeck)
				Session.Remove("goback1");
			else
				DeleteGoBack();

		}

		public static void DeleteGoBack()
		{
			Page executingPage = HttpContext.Current.Handler as Page;
			if (!executingPage.IsPostBack && HttpContext.Current.Request.QueryString["dgb"] != null)
				HttpContext.Current.Session.Remove("goback1");
		}

		public static void AddKeepAlive()
		{
			int int_MilliSecondsTimeOut = (HttpContext.Current.Session.Timeout*60000) - 10000;
			Page executingPage = HttpContext.Current.Handler as Page;
            executingPage.ClientScript.RegisterClientScriptBlock(executingPage.GetType(), "Reconnect", "<script>SessionUp(" + int_MilliSecondsTimeOut + ",5);</script>");
		}

		private string InitScript
		{
			get
			{
				try
				{
					return "<script>var JSLang='" + UC.Culture + "';\r\n" +
						"var SessionTimeout=" + (Session.Timeout*60000).ToString() + ";\r\n" +
						"var DatePattern='" + UC.myDTFI.ShortDatePattern.ToString() + "';\r\n" +
						"var TimePattern='" + UC.myDTFI.ShortTimePattern.ToString() + "';\r\n" +
						"var CurrencyPattern='" + CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator + "';\r\n" +
						"var Last30='" + Root.rm.GetString("last30") + "';\r\n</script>";
				}
				catch
				{
					return string.Empty;
				}
			}
		}

		private void InitMessages()
		{
			string[] ids = null;
			if (MessagesHandler.CheckForNewMessage(UC.UserId, out ids))
				Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "message", "<script>setTimeout(\"NonBlockingAlert('" + Root.rm.GetString("Mestxt18") + "',true)\",2000);</script>");

		}

        protected override void Render(HtmlTextWriter output)
		{
			string template;
			StreamReader objReader;
			Trace.Warn("Start Render");

			if (HttpContext.Current.Items.Contains("render"))
			{
				base.Render(output);
				return;
			}

			if (Request.QueryString["render"] != null)
			{
				switch (Request.QueryString["render"].ToString())
				{
					case "wiz":
						objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + "twiz.htm");
						template = objReader.ReadToEnd();
						objReader.Close();
						TextWriter tempWriter = new StringWriter();
						using (tempWriter)
						{
							base.Render(new HtmlTextWriter(tempWriter));
							if (Context.Items.Contains("warning"))
							{
								template = template.Replace("<!--BODYPART-->", Context.Items["warning"].ToString() + tempWriter.ToString());
								Context.Items.Remove("warning");
							}
							else
								template = template.Replace("<!--BODYPART-->", tempWriter.ToString());
							output.Write(template);
						}
						break;
					case "print":
						break;
					default:
                        TustenaPages(InitScript, null);
                        base.Render(output);
						break;
				}
			}
			else
			{
 				InitMessages();
                string headStr = string.Empty;
                string bodyStr = string.Empty;

				string helptemplate = HelpMenu();
				bool redrawMenu = (string.Compare((string) Session["CurrentActiveMenu"], Request.QueryString["m"]) != 0);
				if (Request.QueryString["m"] != null)
				{
					Session["CurrentActiveMenu"] = Request.QueryString["m"];
				}
				string cacheId = "template." + UC.UserId;
				if (Cache[cacheId] is string)
				{
					template = Cache[cacheId] as string;
				}
				else
				{
                    objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + "newTemplate.htm");
					template = objReader.ReadToEnd();
					objReader.Close();

					if (Session["UserConfig"] != null)
					{
						template = template.Replace("<!--SOMESCRIPT-->",InitScript);
						template = template.Replace("<!--USERNAME-->", "<span class=\"normal\">" + UC.UserRealName + "&nbsp;</span>");
                        Cache.Add(cacheId, template, new CacheDependency(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "template" + Path.DirectorySeparatorChar + "newTemplate.htm"), Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(60), CacheItemPriority.Low, null);
					}
				}
				{
					template = template.Replace("<!--MENUTOP-->", CustomMenu(redrawMenu, true)); //CreateMenuNoStretches(redrawMenu));
					string query = QuerySecurity();
					string menuTitle = String.Empty;
					if (query.Length > 0)
					{
						if (Session["CurrentActiveMenu"] != null)
							menuTitle = CustomSubMenu((StaticFunctions.IsNumber(Session["CurrentActiveMenu"].ToString()) ? Session["CurrentActiveMenu"].ToString() : "25"), query);
					}
					template = template.Replace("<!--MENUBOTTOM-->", menuTitle);



					if (Context.Items.Contains("warning"))
					{
						template += "<p><center><div id=\"ErrorBox\" class=\"err\"><img src='/i/err.gif' width='16' height='16' alt='Error' align='absmiddle'>&nbsp;<strong>WARNING:</strong>&nbsp;" + Context.Items["warning"].ToString() + "</div><center></p>" + helptemplate;
						Context.Items.Remove("warning");
					}

					if (helptemplate.Length > 0)
					{
						StringBuilder manageHelp = new StringBuilder();
						manageHelp.Append("<script>function HideHelp(){");
						manageHelp.AppendFormat("var obj = document.getElementById('HelpSpan');");
						manageHelp.AppendFormat("var obj2 = document.getElementById('RenderSpan');");
						manageHelp.AppendFormat("obj.style.display = 'none';");
						manageHelp.AppendFormat("obj2.style.display = '';");
						manageHelp.Append("}");
						manageHelp.Append("function ShowValue(Field){(document.getElementById('defaultselected')).value = Field.value;}");

						manageHelp.Append("function HideHelpNoMore(menu,user){");
						manageHelp.AppendFormat("var obj = document.getElementById('defaultselected');");
						manageHelp.Append("var menudef = obj.value;");
						manageHelp.Append("var img = new Image;");
						manageHelp.AppendFormat("img.src = '/NoMoreHelp.aspx?render=no&menu='+menu+'&user='+user+'&menudef='+menudef;");
						manageHelp.AppendFormat("setTimeout('sleep()',500);");

                        manageHelp.Append("} function sleep(){location.reload();} </script>");
						manageHelp.AppendFormat("<span id=\"HelpSpan\">{0}</span>", helptemplate);
						manageHelp.Append("<span id=\"RenderSpan\" style=\"display:none\">");
						template +=  manageHelp.ToString();
                    }
                        int pos = template.IndexOf("<!--SPLIT-->");
                        TustenaPages(template.Substring(0, pos), template.Substring(pos + 12));
                        base.Render(output);

                }
			}
		}


        private void TustenaPages(string headPart, string bodyPart)
        {

            if (headPart != null)
            {
                HtmlGenericControl headCtr = FindControl("head") as HtmlGenericControl;
                if (headCtr != null)
                {
                    Page.Controls.AddAt(0, new LiteralControl("<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">"));
                    Page.Title = ":: Tustena CRM ::";
                    headCtr.Controls.AddAt(0, new LiteralControl(headPart));
                }
            }
            if (bodyPart != null)
            {

                HtmlGenericControl bodyCtr = FindControl("body") as HtmlGenericControl;
                if (bodyCtr != null)
                {
                    bodyCtr.Attributes.Add("topmargin","0");
                    bodyCtr.Attributes.Add("leftmargin","0");
                    bodyCtr.Controls.AddAt(0, new LiteralControl(bodyPart));
                    bodyCtr.Controls.Add(new LiteralControl("<center><table cellpadding=0 cellspacing=0 style='height:40px; margin-top:5px;'><tr><td><a href='http://www.opensource.tustena.com' target='_blank'><img src='/images/TustenaLogoOS.gif' alt='Powered by Tustena CRM' border=0></a></td><td valign='top' nowrap><div style='font-family: tahoma, verdana; font-size: 12px; padding:2px 2px 3px 2px; border: 1px solid #686666; border-left: none;'>&copy; 2003-2005 <a href='http://www.digita.it/tustena' target='_blank' style='font-family: tahoma, verdana; font-size: 12px; color: #f46d12;'>Digita S.r.l.</a> All Rights Reserved.<br>Visit <a href='http://www.opensource.tustena.com' target='_blank' style='font-family: tahoma, verdana; font-size: 12px; color: #f46d12;'>www.tustena.com</a> for more information.</div></td></tr></table></center>"));
                }
            }

        }


		private string HelpMenu()
		{
			string template = String.Empty;
			if (Request.QueryString["m"] != null && Request.QueryString["si"] == null)
			{
				int menuId = int.Parse(Request.QueryString["m"]);
				if (DatabaseConnection.SqlScalar(String.Format("SELECT COUNT(ID) FROM MENUMAP WHERE USERID={0} AND MENUID={1} AND FIRSTTIME=1", UC.UserId, menuId)) == "0")
				{
					DataTable dtHelp = DatabaseConnection.CreateDataset("SELECT * FROM HELPMENU WHERE MENUID=" + menuId).Tables[0];
					if (dtHelp.Rows.Count > 0)
					{
						if (File.Exists(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + UC.Culture.Substring(0, 2) + Path.DirectorySeparatorChar + dtHelp.Rows[0]["HelpFile"].ToString()))
						{
							StreamReader objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + UC.Culture.Substring(0, 2) + Path.DirectorySeparatorChar + dtHelp.Rows[0]["HelpFile"].ToString());
							template = objReader.ReadToEnd();
							objReader.Close();
							StringBuilder footer = new StringBuilder();
							string subMenu = CreateMenuHelp(menuId.ToString(), QuerySecurity());
							footer.AppendFormat("<table width=\"100%\"><tr><td width=\"140\" class=\"SideBorderLinked\" valign=\"top\">{1}<table width=\"100%\"><tr><td width=\"100%\" align=center class=Save style=\"cursor:pointer\" onclick=\"javascript:HideHelpNoMore({2},{3});noclick(this);\">{4}</td></tr></table></td><td valign=\"top\">{0}</td></tr></table>", template, subMenu, Request.QueryString["m"], UC.UserId, Root.rm.GetString("DefaultMenu"));


							template = footer.ToString();
						}
						else
						{
							StreamReader objReader = new StreamReader(Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + "en" + Path.DirectorySeparatorChar + dtHelp.Rows[0]["HelpFile"].ToString());
							template = objReader.ReadToEnd();
							objReader.Close();
							StringBuilder footer = new StringBuilder();
							string subMenu = CreateMenuHelp(menuId.ToString(), QuerySecurity());
							footer.AppendFormat("<table width=\"100%\"><tr><td width=\"140\" class=\"SideBorderLinked\" valign=\"top\">{1}<table width=\"100%\"><tr><td width=\"100%\" align=center class=Save style=\"cursor:pointer\" onclick=\"javascript:HideHelpNoMore({2},{3});noclick(this);\">{4}</td></tr></table></td><td valign=\"top\">{0}</td></tr></table>", template, subMenu, Request.QueryString["m"], UC.UserId, Root.rm.GetString("DefaultMenu"));


							template = footer.ToString();
						}
					}
				}
				else
				{
					string newHome = DatabaseConnection.SqlScalar(String.Format("SELECT NEWHOMEPAGE FROM MENUMAP WHERE USERID={0} AND MENUID={1} AND FIRSTTIME=1", UC.UserId, menuId));
                    if (newHome.Length > 0)
                    {
                        Response.Clear();
                        Response.Redirect(newHome);
                    }
				}
			}
			return template;
		}

		private string CreateMenuHelp(string parent, string query)
		{
			StringBuilder menu = new StringBuilder();
			menu.Append("<table border=0 cellpadding=0 cellspacing=0 class=\"normal\" width=\"100%\">");

			DataSet dsParent;
			dsParent = DatabaseConnection.CreateDataset(String.Format("SELECT ID,VOICE,LINK,RMVALUE FROM TUSTENAMENU_VIEW WHERE ID={0}", parent));
			DataRow drParent = dsParent.Tables[0].Rows[0];

			menu.AppendFormat("<tr><td class=\"sideContainer\"><div class=\"sideTitle\">{0}</div><div class=\"sideFixed\">", Root.rm.GetString("Menutxt" + dsParent.Tables[0].Rows[0]["rmvalue"].ToString()));
			menu.AppendFormat("<input type=hidden id=\"defaultselected\" value=\"{0}\"><input type=\"radio\" checked onclick=\"ShowValue(this)\" name=\"MenuDef\" id=\"MenuDef\" value={0}>{1}<br>", dsParent.Tables[0].Rows[0]["id"], Root.rm.GetString("Menutxt" + drParent["rmvalue"].ToString()) + " Help");
			DataSet secondaryMenuVoices;
			if (mode == "0" && !UC.DebugMode)
			{
				secondaryMenuVoices = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU={0}{1} AND MODE=0 ORDER BY SORTORDER", parent, query));
			}
			else
			{
				secondaryMenuVoices = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU={0}{1} ORDER BY SORTORDER", parent, query));
			}
			if (secondaryMenuVoices.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow DRVocip in secondaryMenuVoices.Tables[0].Rows)
				{
					menu.AppendFormat("<input type=\"radio\" onclick=\"ShowValue(this)\" name=\"MenuDef\" id=\"MenuDef\" value={0}>{1}<br>", DRVocip["id"], Root.rm.GetString("Menutxt" + DRVocip["rmvalue"].ToString()));
				}
			}
			menu.Append("</div></td></tr></table>");
			return menu.ToString(); //.Substring(0,menu.ToString().Length-7);
		}

		public string QuerySecurity()
		{
			string query = String.Empty;
			if (Session["UserConfig"] != null && UC.GroupDependency != null)
			{
				string dependency = UC.GroupDependency;
				if (dependency == "|0|")
				{
					query = String.Empty;
				}
				else
				{
					string[] dep = dependency.Split('|');
					foreach (string group in dep)
					{
						if (group.Length > 0) query += " ACCESSGROUP LIKE '%|" + group + "|%' OR ";
					}
					query = " AND ((" + query.Substring(0, query.Length - 4) + ") OR RMVALUE=7)";
				}
				return query;
			}
			else
			{
				return "";
			}
		}

		private string CustomMenu(bool redraw, bool maintemplate)
		{
			redraw = true;
			string M = Request.QueryString["m"];
			string cacheName = String.Format("MENU.{0}.{1}", UC.UserId, UC.CultureSpecific);
			if (Cache[cacheName] is string && !redraw) return Cache[cacheName] as string;
			Cache.Remove(cacheName);
			StringBuilder menuTitle = new StringBuilder();
			menuTitle.Append("<ul class=\"inBarMenu Buttons normal\" id=\"BarMenu\">");
			string query = QuerySecurity();
			string newUrl = String.Empty;
			string highlight = String.Empty;
			bool firstLoop = true;

			DataSet titles;
			if (UC.UserId != 0)
			{
				if (mode == "0" && !UC.DebugMode)
				{
					titles = DatabaseConnection.CreateDataset(String.Format("SELECT ID,LINK,FOLDER,RMVALUE FROM TUSTENAMENU_VIEW WHERE MENUTITLE=1{0} AND MODE={1} AND ({2} & MODULES)=MODULES ORDER BY SORTORDER", query, mode, (int)UC.Modules));
                }
				else
				{
					titles = DatabaseConnection.CreateDataset(String.Format("SELECT ID,LINK,FOLDER,RMVALUE FROM TUSTENAMENU_VIEW WHERE MENUTITLE=1{0} AND ({1} & MODULES)=MODULES ORDER BY SORTORDER", query, (int)UC.Modules));
                }
				if (titles.Tables[0].Rows.Count > 0)
				{
					foreach (DataRow row in titles.Tables[0].Rows)
					{
						if (Request.QueryString.HasKeys())
						{
							newUrl = Request.QueryString.ToString();
							if (row["link"].ToString().Length > 0)
							{
								if (row["Folder"].ToString().Length > 0)
									newUrl = "/" + row["Folder"].ToString() + "/" + row["link"].ToString();
								else
									newUrl = "/" + row["link"].ToString();
							}
							else
							{
								newUrl = Request.ServerVariables["SCRIPT_NAME"].ToString() + "?" + newUrl.Replace("m=" + M, "m=" + row["id"].ToString());
							}
							highlight = (row["id"].ToString() == M ? "id=MenuSelected" : "");
							if (firstLoop)
							{
								highlight += " class=\"FirstItem\"";
								firstLoop = false;
							}
							menuTitle.AppendFormat("<li {2}><a href=\"{0}\">{1}</a></li>", newUrl, Root.rm.GetString("Menutxt" + row["rmvalue"].ToString()).ToUpper(), highlight);
						}
						else
							Response.Redirect("/login.aspx", true);
					}
					menuTitle.Append("</ul>");
					Cache.Add(cacheName, menuTitle.ToString(), null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(10), CacheItemPriority.Low, null);


					return menuTitle.ToString();
				}
			}
			return (maintemplate ? "" : "<ul class=\"Menu\" id=\"SubBarMenu\"></ul>");
		}

		private string CustomSubMenu(string parent, string query)
		{
			StringBuilder menu = new StringBuilder();

			menu.Append("<ul class=\"inBarMenu\" id=\"SubBarMenu\">");
			menu.AppendFormat("<span class=\"floattop\"><img src=\"/i/topmail.gif\" style=\"cursor:pointer;\" onclick=\"NewWindow('/Common/PopMailHome.aspx?render=no','TopMail','600','500','no')\">&nbsp;<img border=0 alt=\"{1}\" style=\"cursor:pointer\" onclick=\"NewWindow('http://www.tustena.com/{0}/manual/index.html','NewLanguage','600','600','no')\" src=/i/help.gif>&nbsp;<img src=\"/i/suggestion.gif\" onclick=\"NewWindow('/Common/iwish.aspx?render=no','NewLanguage','320','400','no')\" style=\"cursor:pointer;\">&nbsp;<img src=\"/flags/{2}.gif\" width=18 height=12 onclick=\"NewWindow('/Common/newlang.aspx?render=no','NewLanguage','300','200','no')\" style=\"cursor:pointer;\"></span>", UC.Culture.Substring(0, 2), Root.rm.GetString("Manual"), UC.CultureSpecific); //language);

			string highlight = String.Empty;
			bool firstLoop = true;

			DataSet subMenuVoices;
			if (mode == "0" && !UC.DebugMode)
			{
				subMenuVoices = DatabaseConnection.CreateDataset(String.Format("SELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU={0}{1} AND MODE=0 AND ({2} & MODULES)=MODULES ORDER BY SORTORDER", parent, query, (int)UC.Modules));
            }
			else
			{
				subMenuVoices = DatabaseConnection.CreateDataset(String.Format("sELECT * FROM TUSTENAMENU_VIEW WHERE PARENTMENU={0}{1} AND ({2} & MODULES)=MODULES ORDER BY SORTORDER", parent, query, (int)UC.Modules));
            }
			if (subMenuVoices.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow DRVocip in subMenuVoices.Tables[0].Rows)
				{
					highlight = String.Empty;
					if (firstLoop)
					{
						highlight = " class=\"FirstItem\"";
						firstLoop = false;
					}
					string subMenuSelected = string.Empty;
					if ((Request.Params["si"] != null && DRVocip["id"].ToString() == Request.Params["si"]) || (Request.Params["si"] == null && highlight.Length > 0))
						subMenuSelected = "id=\"SubMenuSelected\"";

					if (DRVocip["link"].ToString().EndsWith(";"))
						menu.AppendFormat("<li{2} {3}><a href=\"javascript:{0}\">{1}</a></li>", DRVocip["link"], Root.rm.GetString("Menutxt" + DRVocip["rmvalue"].ToString()), highlight, subMenuSelected);
					else if (DRVocip["Folder"].ToString().Length > 0)
					{
						menu.AppendFormat("<li{3} {4}><a href=\"/{0}&si={1}\">{2}</a></li>", DRVocip["folder"] + "/" + DRVocip["link"], DRVocip["id"], Root.rm.GetString("Menutxt" + DRVocip["rmvalue"].ToString()), highlight, subMenuSelected);
					}
					else
					{
						menu.AppendFormat("<li{3} {4}><a href=\"/{0}&si={1}\">{2}</a></li>", DRVocip["link"], DRVocip["id"], Root.rm.GetString("Menutxt" + DRVocip["rmvalue"].ToString()), highlight, subMenuSelected);
					}
				}
			}
			menu.Append("</ul>");
			return menu.ToString();
		}

		public void UpdateAccess(int company)
		{
			DatabaseConnection.DoCommand("UPDATE TUSTENA_DATA SET LASTACCESS = GETDATE()");

		}

		public static void Message(int id, string subject, string body, string htmlBody)
		{
			UserConfig UC = (UserConfig) HttpContext.Current.Session["UserConfig"];
			using (DigiDapter dg = new DigiDapter())
			{
				dg.Add("SUBJECT", subject.Replace("'", "''"));
				dg.Add("BODY", body.Replace("'", "''"));
				dg.Add("FROMACCOUNT", UC.UserId);
				dg.Add("TOACCOUNT", id);
			}

			if (Convert.ToBoolean(DatabaseConnection.SqlScalar("SELECT FLAGNOTIFYAPPOINTMENT FROM ACCOUNT WHERE UID=" + id)))
			{
				SendMailNotification(id, subject, htmlBody);
			}
		}

		public static void SendMailNotification(int id, string subject, string body)
		{
			DataTable cmd = DatabaseConnection.CreateDataset("SELECT NOTIFYEMAIL, USERACCOUNT FROM ACCOUNT WHERE UID=" + id).Tables[0];
			string to;
			if (cmd.Rows.Count > 1)
			{
				to = (cmd.Rows[0][0].ToString().Length > 0) ? cmd.Rows[0][0].ToString() : cmd.Rows[0][1].ToString();
				if (to.Length > 0)
				{
					MessagesHandler.SendMail(to, null, "[Tustena] " + subject, body);
				}
			}

		}


		public static void SendError(string subject, string body)
		{
			if (ConfigSettings.TustenaErrorMail.Length > 0)
				MessagesHandler.SendMail(ConfigSettings.TustenaErrorMail, ConfigSettings.TustenaErrorMail, "[Tustena] " + subject, body);
		}

		public static string GroupDependency(int gr)
		{
			string sqlString = String.Format("SELECT ID,DEPENDENCY FROM GROUPS WHERE ID={0} OR DEPENDENCY LIKE '%|{0}|%'", gr.ToString());
			DataSet dsGroup = DatabaseConnection.CreateDataset(sqlString);
			StringBuilder sbDependency = new StringBuilder("|");
			if (dsGroup.Tables[0].Rows.Count > 0)
			{
				foreach (DataRow drdip in dsGroup.Tables[0].Rows)
				{
					sbDependency.AppendFormat("{0}|", drdip["id"].ToString());
				}
			}
			return sbDependency.ToString();
		}

		public string CleanGroups(string arid)
		{
			ArrayList al = new ArrayList();
			string[] parts = arid.Split('|');
			al.InsertRange(0, parts);
			int y = 0;
			do
			{
				int x;
				for (x = y + 1; x < al.Count; x++)
				{
					if (al[x].Equals(al[y]) || al[x].ToString().Length == 0)
					{
						al.RemoveAt(x);
						break;
					}
				}
				if (x == al.Count) y++;
			} while (y < al.Count);
			string[] checkArr = new string[al.Count];
			al.CopyTo(checkArr, 0);
			return "|" + String.Join("|", checkArr, 1, y - 1) + "|";
		}

		public string GroupsSecure()
		{
			return GroupsSecure("GROUPS", UC);
		}

		public string GroupsSecure(UserConfig UC)
		{
			return GroupsSecure("GROUPS", UC);
		}

		public string GroupsSecure(string column)
		{
			return GroupsSecure(column, UC);
		}

		public string GroupsSecure(string column, UserConfig UC)
		{
			string cacheName = "GS" + UC.UserId + column;
			if (HttpContext.Current.Cache[cacheName] is string)
				return HttpContext.Current.Cache[cacheName].ToString();
			string[] arryD = UC.GroupDependency.Split('|');
			StringBuilder qGroup = new StringBuilder();
			qGroup.AppendFormat(" ({0} LIKE '%|0|%') ", column);
			foreach (string ut in arryD)
				if (ut.Length > 0)
					qGroup.AppendFormat(" OR {1} LIKE '%|{0}|%'", ut, column);
			HttpContext.Current.Cache.Add(cacheName, qGroup.ToString(), null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Low, null);
			return qGroup.ToString();
		}

		public string ZoneSecure()
		{
			return ZoneSecure("COMMERCIALZONE", UC);
		}

		public string ZoneSecure(UserConfig UC)
		{
			return ZoneSecure("COMMERCIALZONE", UC);
		}

		public string ZoneSecure(string column)
		{
			return ZoneSecure(column, UC);
		}

		public string ZoneSecure(string column, UserConfig UC)
		{
			string cacheName = "ZS" + UC.UserId + column;
			if (HttpContext.Current.Cache[cacheName] is string)
				return HttpContext.Current.Cache[cacheName].ToString();
			string[] arryD = UC.Zones.Split('|');
			StringBuilder qGroup = new StringBuilder();
			qGroup.AppendFormat(" ({0} = 0) ", column);
			foreach (string ut in arryD)
				if (ut.Length > 0)
					qGroup.AppendFormat(" OR {1} = {0}", ut, column);

			HttpContext.Current.Cache.Add(cacheName, qGroup.ToString(), null, DateTime.Now.AddMinutes(Session.Timeout), TimeSpan.Zero, CacheItemPriority.Low, null);
			return qGroup.ToString();
		}

		public static void FillListGroups(UserConfig UC, DropDownList ListGroups)
		{
			string[] arryD = UC.GroupDependency.Split('|');
			string query = String.Empty;
			foreach (string ut in arryD)
			{
				if (ut.Length > 0) query += "ID=" + ut + " OR ";
			}

			string groups;
			groups = "SELECT ID, DESCRIPTION FROM GROUPS";

			if (query.Length > 0) groups += " WHERE (" + query.Substring(0, query.Length - 3) + ")";
			ListGroups.DataSource = DatabaseConnection.CreateDataset(groups);
			ListGroups.DataTextField = "Description";
			ListGroups.DataValueField = "ID";
			ListGroups.DataBind();
			ListGroups.Items.Insert(0, Root.rm.GetString("Usrtxt6"));
			ListGroups.SelectedIndex = 0;
			ListGroups.Items[0].Value = "0";
		}


		public void HackLock(string error)
		{
			SendError("Tustena HackAttempt", error);
			Context.Items["warning"] = "Hack Attempt blocked!";
			Response.Redirect("/");
		}



		private string GoogleSearch
		{
			get
			{
				string gs = "<input type=\"text\" id=\"googlequery\" noret class=\"BoxDesign\" size=\"30\" style=\"width:80px;background-image: url(/i/google.gif);\" maxlength=\"255\">";
				gs += "<a href=\"http://www.google.com/custom?q=query&sa=Search&client=pub-0057556373251075&forid=1&ie=ISO-8859-1&oe=ISO-8859-1&cof=GALT%3A%23336699%3BGL%3A1%3BDIV%3A%2399CCCC%3BVLC%3A663399%3BAH%3Acenter%3BBGC%3AFFFFFF%3BLBGC%3A336699%3BALC%3A336699%3BLC%3A336699%3BT%3A000000%3BGFNT%3A663399%3BGIMP%3A663399%3BFORID%3A1%3B&hl=en\" onclick=\"this.href = this.href.replace('query',document.getElementById('googlequery').value)\" target=\"google_window\" style=\"color:#4A7DE7\"><img src=\"/i/lens.gif\" border=0></a>";
				return gs;
			}

		}

		public string PrintValues(Array myArr, char mySeparator)
		{
			IEnumerator myEnumerator = myArr.GetEnumerator();
			int i = 0;
			string str = String.Empty;
			while (myEnumerator.MoveNext())
			{
				if (i > 0)
				{
					i++;
				}
				else
				{
					str += mySeparator;
				}
				str += String.Format("{0}{1}", mySeparator, myEnumerator.Current);

			}
			return str;
		}

		public string MakeVoipString(string ph)
		{
			DataRow drvoip;
			drvoip = DatabaseConnection.CreateDataset("SELECT LINKFORVOIP,INTERNATIONALPREFIX FROM TUSTENA_DATA").Tables[0].Rows[0];
			if (drvoip[0].ToString().Length > 0)
			{
				return String.Format("<img src=\"/i/phone.gif\" style=\"cursor:pointer;\" alt=\"{0}\" onclick=\"voipDial('{1}{2}')\">", Root.rm.GetString("VoIP"), drvoip[0].ToString(), ph);
			}
			return String.Empty;
		}

		public string PrintValues(IEnumerable myCollection)
		{
			IEnumerator myEnumerator = myCollection.GetEnumerator();
			string str = String.Empty;
			while (myEnumerator.MoveNext())
				str += myEnumerator.Current;
			return str;
		}

		public static string ABCHeaderHtml(string mylist)
		{
			StringBuilder abcHtml = new StringBuilder();
			string list = " ";
			string bgColor;
			if (mylist != null) list = mylist;
			abcHtml.Append("<div id=AlfabetoRow align=center>");
			for (int x = 65; x <= 90; x++)
			{
				char ch = (char) x;
				if (list.Substring(0, 1) == "*" || ch.ToString() == list.Substring(0, 1))
				{
					bgColor = "class=\"AlfabetoSelected\"";
				}
				else
				{
					bgColor = String.Empty;
				}
				abcHtml.AppendFormat("&nbsp;<SPAN {0} onclick=\"SelABCHeader('{1}')\">{1}</SPAN>&nbsp;|", bgColor, Convert.ToChar(x));
			}
			abcHtml.AppendFormat("&nbsp;<span onclick=\"SelABCHeader('*')\">{0}</span>", Root.rm.GetString("Reftxt2"));
			abcHtml.Append("</div>");
			return abcHtml.ToString();
		}


		public string FillHelp(string file)
		{
			return FillHelp(file, UC);
		}

		public string FillHelp(string file, UserConfig myUC)
		{
			StreamReader objReader = null;
			if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + (myUC.Culture.Substring(0, 2)) + Path.DirectorySeparatorChar + file + ".htm"))
			{
				objReader = new StreamReader(HttpContext.Current.Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + (myUC.Culture.Substring(0, 2)) + Path.DirectorySeparatorChar + file + ".htm");
				string hl = objReader.ReadToEnd();
				objReader.Close();
				return hl.Replace("{0}", "/help/" + (myUC.Culture.Substring(0, 2)) + "/");
			}
			else if (File.Exists(HttpContext.Current.Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + "en" + Path.DirectorySeparatorChar + file + ".htm"))
			{
				objReader = new StreamReader(HttpContext.Current.Request.PhysicalApplicationPath + Path.DirectorySeparatorChar + "help" + Path.DirectorySeparatorChar + "en" + Path.DirectorySeparatorChar + file + ".htm");
				string hl = objReader.ReadToEnd();
				objReader.Close();
				return hl.Replace("{0}", "/help/" + (myUC.Culture.Substring(0, 2)) + "/");
			}
			else
				G.SendError("Tustena Path", "HelpFile not available: " + file + ".htm");
			return String.Empty;
		}


		private void IsAlreadyIn()
		{
			string cachedSession = Cache[UC.UserId.ToString()] as string;
			if (cachedSession != null)
			{
				if (Session.SessionID != cachedSession)
				{

					Cache.Remove(UC.UserId.ToString());
					Response.Redirect("~/default.aspx?logoff=reentered");
				}
			}
			else
				Cache.Add(UC.UserId.ToString(), Session.SessionID, null, DateTime.MaxValue, new TimeSpan(0, 0, HttpContext.Current.Session.Timeout, 0, 0), CacheItemPriority.NotRemovable, null);
		}

        public static string GetName(long id, GetNameFor type)
        {
            string n = string.Empty;
            switch (type)
            {
                case GetNameFor.Company: // company
                    n = DatabaseConnection.SqlScalar("SELECT COMPANYNAME FROM BASE_COMPANIES WHERE ID=" + id);
                    break;
                case GetNameFor.Contact: // contact
                    n = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM BASE_CONTACTS WHERE ID=" + id);
                    break;
                case GetNameFor.Lead: //lead
                    n = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'')+' 'COMPANYNAME FROM CRM_LEADS WHERE ID=" + id);
                    break;
                case GetNameFor.Account: // account
                    n = DatabaseConnection.SqlScalar("SELECT ISNULL(NAME,'')+' '+ISNULL(SURNAME,'') FROM ACCOUNT WHERE UID=" + id);
                    break;
            }
            return n;
        }

	}

	[Serializable]
	public class GoBack
	{
		public string sheet;
		public string parameter;
	}

	[Serializable]
	public class SQLSecure
	{
		public string Name;
		public string Value;
		public SqlDbType Type;
		public int Size;
	}

	[Serializable]
	public class CompanyReport
	{
		public int idfield;
		public Hashtable Params;
		public byte Type;
		public bool Finalize;
		public int itemPage;
		public bool morerecord = true;
	}

	[Serializable]
	public class PurchaseProduct
	{
		public long id;
		public string ShortDescription;
		public string LongDescription;
		public string UM;
		public double Qta;
		public decimal UnitPrice;
		public decimal Vat;
		public decimal ListPrice;
		public decimal FinalPrice;
		public decimal Cost;
		public int ObId;
		public int Contacts;
		public int Leads;
		public int Reduction;
		public string ProductCode;
        public decimal RealListPrice;
	}

	public enum CRMTables :byte
	{
		Base_Companies = 1,
		Base_Contacts,
		CRM_Leads,
		CRM_WorkActivity,
        Ticket_Main,
        Warehouse
	}

	public enum AType :byte
	{
		Company = 1,
		Contacts,
		Lead
	}

    public enum GetNameFor : byte
    {
        Company = 0,
        Contact,
        Lead,
        Account
    }

	public enum ActivityMoveLog :byte
	{
		MoveOwner = 0,
		MoveDate = 1,
		MoveCompany = 2,
		MoveContact = 3,
		MoveLead = 4,
		MoveDone = 5,
		MailSent = 6
	}

	public enum MailEvents :byte
	{
		Report = 0,
		BirthDate = 1,
		OnLead = 2
	}

	public enum ActivityTypeN :int
	{
		PhoneCall = 1,
		Letter = 2,
		Fax = 3,
		Memo = 4,
		Email = 5,
		Visit = 6,
		Generic = 7,
		CaseSolution = 8,
		Quote = 9
	}
}
