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
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Web;
using System.Data;
using Digita.Tustena.Database;
using System.Collections;

namespace Digita.Tustena.Base
{
    public sealed class Scheduler
    {
        public static event EventHandler Tick;
        private static Timer mTimer = null;

        private Scheduler()
        { }



        static void Timer_Callback(object state)
        {
            OnTick(EventArgs.Empty);
        }

        private static void OnTick(EventArgs e)
        {
            EventHandler handler = Tick;

            if (handler != null)
                handler(HttpContext.Current, e);
        }

        public static void Start(int interval)
        {
            lock (typeof(Scheduler))
            {
                if (mTimer == null)
                    mTimer = new Timer(
                        new TimerCallback(Timer_Callback),
                        null,
                        interval,
                        interval);
            }
        }

        public static void Stop()
        {
            lock (typeof(Scheduler))
                if (mTimer != null)
                {
                    mTimer.Dispose();
                    mTimer = null;
                }
        }

        public static bool IsStarted
        {
            get { return mTimer != null; }
        }


        public static void ScheduleEvents(DataTable dt)
        {
            try
            {
                DataRow[] dr = dt.Select("EVENTYPE=0");
                if (dr.Length > 0)
                {
                    DateTime last = (DateTime)dr[0]["LASTEVENT"];
                    if (last.DayOfYear == DateTime.Now.DayOfYear && last.Year == DateTime.Now.Year)
                        return;
                    else
                    {
                        EventUpdate(ScheduleType.CatalogExpire);
                    }
                }
                else
                {
                    EventUpdate(ScheduleType.CatalogNew);
                }
            }
            catch
            {
                EventUpdate(ScheduleType.CatalogNew);
            }
        }

        private static void EventUpdate(ScheduleType st)
        {
            switch (st)
            {
                case ScheduleType.CatalogExpire:
                    RefreshCatalog();
                    DatabaseConnection.DoCommandWithoutTransaction("UPDATE EVENTSCHEDULER SET LASTEVENT=GETDATE() WHERE EVENTYPE=0;");

                    break;
                case ScheduleType.CatalogNew:
                    RefreshCatalog();
                    DatabaseConnection.DoCommandWithoutTransaction("INSERT INTO EVENTSCHEDULER (EVENTYPE,LASTEVENT) VALUES (0,GETDATE());");

                    break;
            }

        }

        private static void RefreshCatalog()
        {
            DataSet ds = DatabaseConnection.CreateDatasetWithoutTransaction("SELECT CATALOGPRODUCTS.*,CATALOGCATEGORIES.EMAILOWNER,CATALOGCATEGORIES.DESCRIPTION AS CATDESCRIPTION FROM CATALOGPRODUCTS LEFT OUTER JOIN CATALOGCATEGORIES ON CATALOGPRODUCTS.CATEGORY=CATALOGCATEGORIES.ID WHERE (PRICEEXPIRE IS NOT NULL) AND PRICEEXPIRE<GETDATE() ORDER BY CATALOGPRODUCTS.CATEGORY;UPDATE CATALOGPRODUCTS SET ACTIVE=0 WHERE (PRICEEXPIRE IS NOT NULL) AND PRICEEXPIRE<GETDATE();");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ArrayList al = new ArrayList();
                StringBuilder sb = new StringBuilder();
                string header = "<HTML><BODY>These products are expired :<br><table><tr><td>Code</td><td>Product</td><td>Category</td></tr>";
                string productowner = string.Empty;
                string productownerold = string.Empty;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    productowner = dr["CATEGORY"].ToString() + "|" + dr["EMAILOWNER"].ToString();
                    if (productowner != productownerold)
                    {
                        if (sb.Length > 0)
                        {
                            sb.Append("</table></BODY></HTML>");
                            string[] ms = productownerold.Split('|');
                            if(ms[2].Length>0)
                            {
                                MessagesHandler.SendMail(ms[2], "robot@tustena.com", "Catalog update", sb.ToString());
                            }
                        }
                        productownerold = productowner;
                        sb = new StringBuilder();
                    }
                    if (sb.Length == 0)
                        sb.Append(header);

                    sb.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>", dr["CODE"].ToString(), dr["SHORTDESCRIPTION"].ToString(), dr["CATDESCRIPTION"].ToString());

                }
            }
        }

        public enum ScheduleType
        {
            CatalogExpire = 0,
            CatalogNew
        }
    }
}
