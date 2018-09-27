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
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;

namespace Digita.Tustena.Common
{
	public partial class ViewContact : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Login())
			{
                litID.Text = Request.Params["id"].ToString();
                edittable.Visible = false;
                FillRepeater();
			}
		}

        private void FillRepeater()
        {
            Repeater1.DataSource = DatabaseConnection.SecureCreateDataset("SELECT * FROM BASE_CONTACTS WHERE ID=@ID AND (" + GroupsSecure("GROUPS") + ")", new DbSqlParameter("@ID", litID.Text));
            Repeater1.DataBind();
        }

		#region Codice generato da Progettazione Web Form

		protected override void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);
            this.modSave.Click += new EventHandler(modSave_Click);
			this.Repeater1.ItemDataBound +=new RepeaterItemEventHandler(Repeater1_ItemDataBound);
            this.Repeater1.ItemCommand += new RepeaterCommandEventHandler(Repeater1_ItemCommand);
        }
        #endregion

        void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            modEdit();
        }

        private void modEdit()
        {
            DataRow dr = DatabaseConnection.SecureCreateDataset("SELECT NAME,SURNAME,PHONE_1,PHONE_2,MOBILEPHONE_1,MOBILEPHONE_2,FAX,EMAIL FROM BASE_CONTACTS WHERE ID=@ID", new DbSqlParameter("@ID", litID.Text)).Tables[0].Rows[0];

            txtName.Text = dr[0].ToString();
            txtSurname.Text = dr[1].ToString();
            txtPHONE_1.Text = dr[2].ToString();
            txtPHONE_2.Text = dr[3].ToString();
            txtMobilePhone_1.Text = dr[4].ToString();
            txtMobilePhone_2.Text = dr[5].ToString();
            txtFax.Text = dr[6].ToString();
            txtEmail.Text = dr[7].ToString();

            Repeater1.Visible = false;
            edittable.Visible = true;
        }


        void modSave_Click(object sender, EventArgs e)
        {
            using (DigiDapter dg = new DigiDapter())
            {
                dg.Add("NAME", txtName.Text);
                dg.Add("SURNAME", txtSurname.Text);
                dg.Add("PHONE_1", txtPHONE_1.Text);
                dg.Add("PHONE_2", txtPHONE_2.Text);
                dg.Add("MOBILEPHONE_1", txtMobilePhone_1.Text);
                dg.Add("MOBILEPHONE_2", txtMobilePhone_2.Text);
                dg.Add("FAX", txtFax.Text);
                dg.Add("EMAIL", txtEmail.Text);

                dg.Execute("BASE_CONTACTS", "ID=" + litID.Text);
            }

            FillRepeater();
            Repeater1.Visible = true;
            edittable.Visible = false;
        }


		private void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					Literal VoipCallPhone_1 = (Literal) e.Item.FindControl("VoipCallPhone_1");
					Literal VoipCallPhone_2 = (Literal) e.Item.FindControl("VoipCallPhone_2");
					Literal VoipCallMobilePhone_1 = (Literal) e.Item.FindControl("VoipCallMobilePhone_1");
					Literal VoipCallMobilePhone_2 = (Literal) e.Item.FindControl("VoipCallMobilePhone_2");

						if((DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_1")).ToString().Length>0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_1"),@"[^0-9]","");
							VoipCallPhone_1.Text=MakeVoipString(ph);
						}
						if((DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_2")).ToString().Length>0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "PHONE_2"),@"[^0-9]","");
							VoipCallPhone_2.Text=MakeVoipString(ph);
						}
						if((DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_1")).ToString().Length>0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_1"),@"[^0-9]","");
							VoipCallMobilePhone_1.Text=MakeVoipString(ph);
						}
						if((DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_2")).ToString().Length>0)
						{
							string ph = Regex.Replace((string) DataBinder.Eval((DataRowView) e.Item.DataItem, "MobilePhone_2"),@"[^0-9]","");
							VoipCallMobilePhone_2.Text=MakeVoipString(ph);
						}
					break;
			}
		}
	}
}
