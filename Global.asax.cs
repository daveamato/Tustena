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
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Web;
using Digita.Tustena.Base;
using Digita.Tustena.Core;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;
using System.Net;
using System.Web.Caching;
using System.Data;
using System.Data.SqlClient;

namespace Digita.Tustena
{

	public class Global : HttpApplication //Digita.Tustena.Precompile //HttpApplication //
	{
		private IContainer components = null;


		public Global()
		{
			InitializeComponent();
		}

        protected void Application_Start(Object sender, EventArgs e)
        {
            const string baseResourceFile = "TustenaLanguage";
            Assembly primaryResource = Assembly.Load(baseResourceFile);
            Application["RM"] = new ResourceManager(baseResourceFile, primaryResource);

            if (ConfigSettings.SchedulerInterval.Length > 0)
            {
                if (!Digita.Tustena.Base.Scheduler.IsStarted)
                    Digita.Tustena.Base.Scheduler.Start(Convert.ToInt32(ConfigSettings.SchedulerInterval));
                else
                    G.SendError("Scheduler already Start", DateTime.Now.ToString());
                Digita.Tustena.Base.Scheduler.Tick += new EventHandler(Scheduler_Tick);
            }

            string initError = null;
            if (!CheckSystemStatus(out initError))
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.Write(initError);
                HttpContext.Current.Response.End();
            }

        }

		protected void Session_Start(Object sender, EventArgs e)
		{
			if (Request.Url.Host.ToLower().StartsWith("wap"))
				Response.Redirect("/wap/default.aspx");

            Digita.Tustena.WebControls.SideBarContainer.RemapModules();


		}


		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

			string culturePref = "en-US"; // default culture
			if (Request.Cookies["CulturePref"] != null)
			{
				culturePref = Request.Cookies["CulturePref"].Value;
			}
			else
			{
				try
				{
					culturePref = HttpContext.Current.Request.UserLanguages[0];
				}
				catch (Exception)
				{
					culturePref = "en-US";
				}
			}
			try
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(culturePref);
			}
			catch (Exception)
			{
				Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
			}
			Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture;

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

			{

			}
		}


		protected void Application_Error(Object sender, EventArgs e)
		{
			DatabaseConnection.RollBackTransaction();
			bool NeedReedirect = true;

			if (ConfigSettings.TustenaErrorMail.Length>0)
			{
				string mailUserName = "User Unknown";
				string mailBody = "<span style=\"font-family:verdana;font-size=12px;\"><p><b>Server:</b> " + this.Server.MachineName + "</p>";

				try
				{
					if (Session!=null && !Session.IsNewSession)
					{
						UserConfig UC = (UserConfig) Session["UserConfig"];
						mailUserName = UC.UserRealName;
						mailBody += "<p>User: " + UC.UserId + "-" + UC.UserRealName + "</p><p>Email: " + UC.MailingAddress + "</p>";
					}

				}
				catch
				{}

				try
				{
					DigiErrorHandler eh = new DigiErrorHandler();
					string message = eh.GetErrorString(Server.GetLastError().InnerException);

                    if (!eh.isParsed) NeedReedirect = false;

					mailBody += message + "</span>";
				}
				catch (Exception ex)
				{
					mailBody += "<p><b>Errore nell'Error Handler:</b> " + ex.Message + "</p><p><b>Error Code:</b> " + Error_dump() + "</p>";
				}
				finally
				{
					MessagesHandler.SendMail(ConfigSettings.TustenaErrorMail, ConfigSettings.TustenaErrorMail, "[Tustena] Error - " + mailUserName, mailBody);
					if (NeedReedirect)
					{
						Server.ClearError();
						this.Response.Redirect("/today.aspx?m=25&e=1");
					}
				}
			}
		}

		internal string Error_dump()
		{
			StackTrace st = new StackTrace(Server.GetLastError().InnerException, true);
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat(" Stack trace for current level: {0}", st.ToString());
			int count = 0;
			while (count < st.FrameCount)
			{
				StackFrame sf = st.GetFrame(count);
				sb.AppendFormat("<b>File:</b> {0}<br>", sf.GetFileName());
				sb.AppendFormat("<b>Method:</b> {0}<br>", sf.GetMethod().Name);
				sb.AppendFormat("<b>Line Number:</b> {0}<br>", sf.GetFileLineNumber());
				sb.AppendFormat("<b>Column Number:</b> {0}<br>", sf.GetFileColumnNumber());
				count++;
			}
			return sb.ToString();
		}

		protected void Session_End(Object sender, EventArgs e)
		{
			try
			{
				UserConfig UC = (UserConfig) Session["UserConfig"];
				DatabaseConnection.DoCommand("UPDATE ACCOUNT SET STATE=0 WHERE UID='" + UC.UserId + "'");
				DatabaseConnection.DoCommand("DELETE FROM VIEWSTATEMANAGER WHERE SESSIONID='" + Session.SessionID.ToString() + "'");
			}
			catch (Exception ex)
			{
				string error = ((HttpException) this.Server.GetLastError()).GetHtmlErrorMessage();
			}
		}

		protected void Application_End(Object sender, EventArgs e)
		{
		}


		public string FixLocation(string indt)
		{
			return indt;
		}

        private void Scheduler_Tick(object sender, EventArgs e)
        {
           DataSet dt = DatabaseConnection.CreateDatasetWithoutTransaction("SELECT * FROM EVENTSCHEDULER");
           Scheduler.ScheduleEvents(dt.Tables[0]);

        }

        private bool CheckSystemStatus(out string error)
        {
            object dbVersion;
            try
            {
                dbVersion = DatabaseConnection.SqlScalartoObj("select top 1 dbversion from version");
            }
            catch (Exception ex)
            {
                error = "Database read error, check connection and permissions. Error: " + ex.Message.ToString();
                return false;
            }
            try
            {
                DatabaseConnection.DoCommand(string.Format("update version set dbversion='{0}' where dbversion='{0}'", dbVersion));
            }
            catch (Exception ex)
            {
                error = "Database write error, check permissions. Error: " + ex.Message.ToString();
                return false;
            }
            try
            {
            FileCheck(ConfigSettings.MailSpoolPath);
            FileCheck(ConfigSettings.DataStoragePath);
            }
            catch (Exception ex)
            {
                error = "File IO error, check permissions. Error: " + ex.Message.ToString();
                return false;
            }
            error = null;
            return true;
        }

        private void FileCheck(string filePath)
        {
            string path = Path.Combine(filePath, "dummytestfile.txt");
            StreamWriter f = File.CreateText(path);
            f.Write("Tustena");
            f.Close();
            StreamReader r = File.OpenText(path);
            r.ReadToEnd();
            r.Close();
            File.Delete(path);
        }

		#region Codice generato da Progettazione Web Form

		private void InitializeComponent()
		{
			this.components = new Container();
		}

		#endregion
	}


	public class DigiErrorHandler
	{
		private Exception LastError = null;
		public bool isParsed = false;
		private bool compactFormat = false;
		private bool retrieveSourceLines = true;
		private string rawUrl = String.Empty;
		private string errorMessage = String.Empty;
		private string StackTrace = String.Empty;
		private string sourceCode = String.Empty;
		private string fileName = String.Empty;
		private string fullUrl = String.Empty;
		private string IPAddress = String.Empty;
		private string referer = String.Empty;
		private string browser = String.Empty;
		private string login = String.Empty;
		private string postBuffer = String.Empty;
		private string RawHttp = String.Empty;
		private int contentSize = 0;
		private int Line = 0;

		public string GetErrorString(Exception lastErrorInner)
		{
			this.LastError = lastErrorInner;
			Parse();
			StringBuilder sb = new StringBuilder(1024);
			sb.AppendFormat("<p><b>Parsed:</b> {0}</p>", this.isParsed);
			sb.AppendFormat("<p><b>RawUrl:</b> {0}</p>", this.rawUrl);
			sb.AppendFormat("<p><b>ErrorMessage:</b> {0}</p>", this.errorMessage);
			sb.AppendFormat("<p><b>StackTrace:</b> {0}</p>", this.StackTrace);
			sb.AppendFormat("<p><b>SourceCode:</b> {0}</p>", this.sourceCode);
			sb.AppendFormat("<p><b>SourceLine:</b> {0}</p>", this.Line.ToString());
			sb.AppendFormat("<p><b>FullUrl:</b> {0}</p>", this.fullUrl);
			sb.AppendFormat("<p><b>IPAddress:</b> {0}</p>", this.IPAddress);
			sb.AppendFormat("<p><b>Referer:</b> {0}</p>", this.referer);
			sb.AppendFormat("<p><b>Browser:</b> {0}</p>", this.browser);
			sb.AppendFormat("<p><b>Login:</b> {0}</p>", this.login);
			sb.AppendFormat("<p><b>PostBuffer:</b> {0}</p>", this.postBuffer.Replace("&", "<br>"));
			sb.AppendFormat("<p><b>ContentSize:</b> {0}</p>", this.contentSize.ToString());
			return sb.ToString();
		}

		public bool Parse()
		{
			HttpRequest Request = HttpContext.Current.Request;

			this.rawUrl = Request.RawUrl;

			this.fullUrl =
				string.Format("http://{0}{1}", Request.ServerVariables["SERVER_NAME"], Request.RawUrl);
			this.IPAddress = Request.UserHostAddress;

			if (Request.UrlReferrer != null)
				this.referer = Request.UrlReferrer.ToString();

			this.browser = Request.UserAgent;

			if (Request.TotalBytes > 0 && Request.TotalBytes < 6144)
			{
				this.postBuffer =
					Encoding.GetEncoding(1252).GetString(Request.BinaryRead(Request.TotalBytes))
					;
				this.contentSize = Request.TotalBytes;
			}
			else if (Request.TotalBytes > 20000) // strip the result
			{
				this.postBuffer =
					Encoding.GetEncoding(1252).GetString(Request.BinaryRead(20000)) + "...";
				this.contentSize = Request.TotalBytes;
			}


			if (this.LastError == null)
				return false;
			this.isParsed = true;


			if (LastError is FileNotFoundException)
				this.errorMessage = "File not found: " + LastError.Message;
			else
				this.errorMessage = LastError.Message;


			if (this.compactFormat)
				return true;

			this.StackTrace = LastError.StackTrace;
			if (this.retrieveSourceLines)
			{
				StringBuilder sb = new StringBuilder(1024);

				StackTrace st = new StackTrace(LastError, true);
				StackFrame sf = st.GetFrame(0);
				if (sf != null)
				{
					this.fileName = sf.GetFileName();
					this.Line = sf.GetFileLineNumber();
					if (retrieveSourceLines && this.fileName != null && File.Exists(this.fileName))
					{
						int lineNumber = sf.GetFileLineNumber();
						if (lineNumber > 0)
						{
							StreamReader sr = new StreamReader(this.fileName);

							int x = 0;
							for (x = 0; x < lineNumber - 4; x++)
								sr.ReadLine();

							sb.Append("--- Code ---\r\n");
							sb.AppendFormat("File: {0}\r\n", fileName);
							sb.AppendFormat("Method: {0}\r\n\r\n", LastError.TargetSite);
							sb.AppendFormat("Line {0}: {1}\r\n", x + 1, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 2, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 3, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 4, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 5, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 6, sr.ReadLine());
							sb.AppendFormat("Line {0}: {1}\r\n", x + 7, sr.ReadLine());

							sr.Close();
						}
					}
				}

				this.sourceCode = "<pre>" + sb.ToString() + "</pre>";
			}

			if (Request.IsAuthenticated)
				this.login = HttpContext.Current.User.Identity.Name;
			else
				this.login = "Anonymous";


			return true;
		}
	}

}
