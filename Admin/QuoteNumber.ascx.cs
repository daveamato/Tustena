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
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Digita.Tustena.Database;
using Digita.Tustena.Core;

namespace Digita.Tustena.Admin
{
    public partial class QuoteNumber : System.Web.UI.UserControl
    {
        private Digita.Tustena.Core.UserConfig UC = new UserConfig();

        protected void Page_Load(object sender, EventArgs e)
        {
            UC = (UserConfig)HttpContext.Current.Session["userconfig"];
            btnSave.Text = Root.rm.GetString("Save");
            if(!Page.IsPostBack)
                FillRestart();
        }

        public ProgressiveType NumberType
        {
            get
            {
                object o = this.ViewState["_NumberType" + this.ID];
                if (o == null)
                    return 0;
                else
                    return (ProgressiveType)o;
            }
            set
            {
                this.ViewState["_NumberType" + this.ID] = value;
            }
        }

        private void FillRestart()
        {
            for (int nMonth = 1; nMonth <= 12; nMonth++)
            {
                DateTime MonthDate = new DateTime(2000, nMonth, 1);
                NprogRestart.Items.Add(new
                      ListItem(MonthDate.ToString("MMMM"),
                                                    nMonth.ToString()));
            }
            NprogRestart.Items.Insert(0, new ListItem(Root.rm.GetString("QuoNumtxt11"), "0"));
        }

        public void LoadData()
        {
            if (NumberType != ProgressiveType.Quote) CustomerCode.Visible = false;
            DataTable dt = DatabaseConnection.CreateDataset(string.Format("SELECT * FROM QUOTENUMBERS WHERE TYPE={0}",(byte)NumberType)).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                litId.Text = dr["ID"].ToString();
                NprogText.Text = dr["NPROG"].ToString();
                CheckDay.Checked = (bool)dr["CHECKDAY"];
                CheckMonth.Checked = (bool)dr["CHECKMONTH"];
                CheckYear.Checked = (bool)dr["CHECKYEAR"];
                CheckDisabled.Checked = (bool)dr["DISABLED"];
                YearDigit.SelectedIndex = -1;
                if ((bool)dr["TWODIGITYEAR"])
                    YearDigit.Items[1].Selected=true;
                else
                    YearDigit.Items[0].Selected=true;
                CheckCustomerCode.Checked = (bool)dr["CHECKCUSTOMERCODE"];
                NprogStarttxt.Text = dr["NPROGSTART"].ToString();
                NprogRestart.SelectedIndex = -1;
                foreach (ListItem li in NprogRestart.Items)
                {
                    if (dr["NPROGRESTART"].ToString() == li.Value)
                    {
                        li.Selected = true;
                        break;
                    }
                }

            }
            else
            {
                litId.Text = "-1";
                NprogText.Text = "1";
                CheckDay.Checked = false;
                CheckMonth.Checked = false;
                CheckYear.Checked = false;
                YearDigit.SelectedIndex = -1;

                CheckCustomerCode.Checked = false;
                CheckDisabled.Checked = false;
                NprogStarttxt.Text = "1";
                NprogRestart.SelectedIndex = 0;

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("NPROG", NprogText.Text);
                dg.Add("CHECKDAY", CheckDay.Checked);
                dg.Add("CHECKMONTH", CheckMonth.Checked);
                dg.Add("CHECKYEAR", CheckYear.Checked);
                dg.Add("TWODIGITYEAR", YearDigit.SelectedValue);
                dg.Add("CHECKCUSTOMERCODE", CheckCustomerCode.Checked);
                dg.Add("NPROGSTART", NprogStarttxt.Text);
                dg.Add("NPROGRESTART", NprogRestart.SelectedValue);
                dg.Add("DISABLED", CheckDisabled.Checked);
                dg.Add("TYPE", (byte)NumberType);
                object newid = dg.Execute("QUOTENUMBERS", "ID=" + litId.Text,DigiDapter.Identities.Identity);
                if (dg.RecordInserted)
                    litId.Text = newid.ToString();

            }
        }

    }

    public enum ProgressiveType : byte
    {
        Quote=0,
        Company,
        Contact
    }
}
