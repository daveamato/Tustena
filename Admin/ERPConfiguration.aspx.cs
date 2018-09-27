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
using System.Web.UI.WebControls;
using Digita.Tustena.Admin;
using Digita.Tustena.Core;
using Digita.Tustena.WebControls;
using Digita.Tustena.Common;

namespace Digita.Tustena.ERP
{
	public partial class ERPConfiguration : G
	{

        protected override void OnPreRenderComplete(EventArgs e)
        {
            Modules M = new Modules();
            M.ActiveModule = UC.Modules;

            if (!M.IsModule(ActiveModules.Sales) || !M.IsModule(ActiveModules.SalesWarehouse))
                SalesConfiguration.Visible = false;
            if (!M.IsModule(ActiveModules.Lead))
            {
                BtnLeadOrigin.Visible = false;
                FreeForLeads.Visible = false;
            }
            base.OnPreRenderComplete(e);
        }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				Response.Redirect("/login.aspx");
			}else
			{
				if(!Page.IsPostBack)
				{
					ValueEditor1.Visible=false;
					LogosEditor1.Visible=false;
					ListConfiguration1.Visible=false;
					ConfigFreeFields1.Visible=false;
					rank1.Visible=false;
					Tabber.Visible=false;

					btnTaxTable.Text=Root.rm.GetString("ErpConftxt2");
					btnPayment.Text=Root.rm.GetString("ErpConftxt3");
                    btnQuoteNumber.Text = Root.rm.GetString("ErpConftxt10");
                    btnShipDates.Text = Root.rm.GetString("ErpConftxt11");
					btnZones.Text=Root.rm.GetString("ErpConftxt6");
					btnLogos.Text=Root.rm.GetString("ErpConftxt8");
					btnScore.Text=Root.rm.GetString("Scotxt5");
					btnCurrency.Text=Root.rm.GetString("Menutxt54");
                    btnCompanyCode.Text = Root.rm.GetString("ErpConftxt13");
                    btnContactCode.Text = Root.rm.GetString("ErpConftxt14");
					HelpLabel.Text=FillHelp("ERPConfig");

					BtnCompanyType.Text =Root.rm.GetString("Listxt1");
					BtnContactType.Text =Root.rm.GetString("Listxt2");
					BtnContactEstimate.Text =Root.rm.GetString("Listxt3");
					BtnLeadOrigin.Text = "Lead Origin";

					FreeForContact.Text =Root.rm.GetString("Fretxt2");
					FreeForReferenti.Text =Root.rm.GetString("Fretxt3");
					FreeForLeads.Text =Root.rm.GetString("Fretxt0");
				}
			}
		}


		private void MenuClick(string seltab)
		{
			switch(seltab)
			{
				case "btnTaxTable":
					visControl.Header=Root.rm.GetString("ErpConftxt2");
					break;
				case "btnPayment":
					visControl.Header=Root.rm.GetString("ErpConftxt3");
					break;
                case "btnQuoteNumber":
                    visControl.Header = Root.rm.GetString("ErpConftxt10");
                    break;
				case "btnLinks":
					visControl.Header="Links";
					break;
				case "btnZones":
					visControl.Header=Root.rm.GetString("ErpConftxt6");
					break;
				case "btnLogos":
					visControl.Header=Root.rm.GetString("ErpConftxt8");
					break;
				case "btnScore":
					visControl.Header=Root.rm.GetString("Scotxt5");
					break;
				case "btnCurrency":
					visControl.Header=Root.rm.GetString("Menutxt54");
					break;
				case "BtnCompanyType":
					visControl.Header=Root.rm.GetString("Listxt1");
					break;
				case "BtnContactType":
					visControl.Header=Root.rm.GetString("Listxt2");
					break;
				case "BtnContactEstimate":
					visControl.Header=Root.rm.GetString("Listxt3");
					break;
				case "BtnLeadOrigin":
					visControl.Header="Lead Origin";
					break;
				case "FreeForContact":
					visControl.Header=Root.rm.GetString("Fretxt2");
					break;
				case "FreeForReferenti":
					visControl.Header=Root.rm.GetString("Fretxt3");
					break;
				case "FreeForLeads":
					visControl.Header=Root.rm.GetString("Fretxt0");
					break;
                case "btnShipDates":
                    visControl.Header = Root.rm.GetString("ErpConftxt11");
                    break;
                case "btnCompanyCode":
                    visControl.Header = Root.rm.GetString("ErpConftxt13");
                    break;
                case "btnContactType":
                    visControl.Header = Root.rm.GetString("ErpConftxt14");
                    break;
			}
			Tabber.Visible=true;
			HelpLabel.Visible=false;
		}

		public void Btn_Click(object sender, EventArgs e)
		{
			ListConfiguration1.ChangeList(((LinkButton) sender).ID);
			ListConfiguration1.Visible=true;
			ValueEditor1.Visible=false;
			LogosEditor1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.btnLinks.Click+=new EventHandler(btnLinks_Click);
			this.btnWizard.Click+=new EventHandler(btnWizard_Click);
            this.btnQuoteNumber.Click += new EventHandler(btnQuoteNumber_Click);
            this.btnShipDates.Click += new EventHandler(btnShipDates_Click);
            this.btnCompanyCode.Click += new EventHandler(btnCompanyCode_Click);
            this.btnContactCode.Click += new EventHandler(btnContactCode_Click);
            this.Load += new EventHandler(this.Page_Load);
		}

        void btnContactCode_Click(object sender, EventArgs e)
        {
            this.Nprog1.NumberType = ProgressiveType.Contact;
            this.Nprog1.Visible = true;
            this.Nprog1.LoadData();
            this.ValueEditor1.Visible = false;
            this.LogosEditor1.Visible = false;
            this.ListConfiguration1.Visible = false;
            this.ConfigFreeFields1.Visible = false;
            this.rank1.Visible = false;
            this.currency1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
            MenuClick(((LinkButton)sender).ID);
        }

        void btnCompanyCode_Click(object sender, EventArgs e)
        {
            this.Nprog1.NumberType = ProgressiveType.Company;
            this.Nprog1.Visible = true;
            this.Nprog1.LoadData();
            this.ValueEditor1.Visible = false;
            this.LogosEditor1.Visible = false;
            this.ListConfiguration1.Visible = false;
            this.ConfigFreeFields1.Visible = false;
            this.rank1.Visible = false;
            this.currency1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
            MenuClick(((LinkButton)sender).ID);
        }

        void btnShipDates_Click(object sender, EventArgs e)
        {
            ShipDates1.Visible = true;
            ShipDates1.LoadGrid();
            this.Nprog1.Visible = false;
            this.ValueEditor1.Visible = false;
            this.LogosEditor1.Visible = false;
            this.ListConfiguration1.Visible = false;
            this.ConfigFreeFields1.Visible = false;
            this.rank1.Visible = false;
            this.currency1.Visible = false;
            linksInfoPanel.Visible = false;
            MenuClick(((LinkButton)sender).ID);
        }

        void btnQuoteNumber_Click(object sender, EventArgs e)
        {
            this.Nprog1.NumberType = ProgressiveType.Quote;
            this.Nprog1.Visible = true;
            this.Nprog1.LoadData();
            this.ValueEditor1.Visible = false;
            this.LogosEditor1.Visible = false;
            this.ListConfiguration1.Visible = false;
            this.ConfigFreeFields1.Visible = false;
            this.rank1.Visible = false;
            this.currency1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
            MenuClick(((LinkButton)sender).ID);
        }
		#endregion

		protected void btnTaxTable_Click(object sender, EventArgs e)
		{
			ValueEditor1.Multiline=false;
			ValueEditor1.DataValueField="TaxValue";
			ValueEditor1.DataValueFieldType=ExtendedValueEditor.FieldType.numeric;
			ValueEditor1.DataValueField2="TaxDescription";
			ValueEditor1.Lang=string.Empty;
			ValueEditor1.Grouped=false;
			ValueEditor1.KId=string.Empty;
			ValueEditor1.DataValueFieldType2=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueText=new string[]{Root.rm.GetString("ErpConftxt4"), Root.rm.GetString("ErpConftxt5")};
			ValueEditor1.TableName="TaxValues";
            ValueEditor1.MaxSizeField1 = 50;
            ValueEditor1.MaxSizeField2 = 100;
			ValueEditor1.LoadGrid();
			ValueEditor1.Visible=true;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void btnPayment_Click(object sender, EventArgs e)
		{
			ValueEditor1.Multiline=true;
			ValueEditor1.DataValueField="Description";
			ValueEditor1.DataValueFieldType=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueField2=string.Empty;
			ValueEditor1.Lang="Lang";
			ValueEditor1.Grouped=false;
			ValueEditor1.KId=string.Empty;
			ValueEditor1.DataValueFieldType2=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueText=new string[]{Root.rm.GetString("ErpConftxt5")};
			ValueEditor1.TableName="PaymentList";
            ValueEditor1.MaxSizeField1 = 1000;

			ValueEditor1.LoadGrid();
			ValueEditor1.Visible=true;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void btnZones_Click(object sender, EventArgs e)
		{
			ValueEditor1.Multiline=false;
			ValueEditor1.DataValueField="Description";
			ValueEditor1.DataValueFieldType=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueField2=string.Empty;
			ValueEditor1.Lang=string.Empty;
			ValueEditor1.DataValueFieldType2=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueText=new string[]{Root.rm.GetString("ErpConftxt6")};
			ValueEditor1.TableName="Zones";
            ValueEditor1.MaxSizeField1 = 50;

			ValueEditor1.LoadGrid();
			ValueEditor1.Visible=true;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void btnLogos_Click(object sender, EventArgs e)
		{
			ValueEditor1.Visible=false;
			this.LogosEditor1.Visible=true;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void FreeField_Click(object sender, EventArgs e)
		{
			switch (((LinkButton) sender).ID)
			{
				case "FreeForContact":
					ConfigFreeFields1.SetFreeField(CRMTables.Base_Companies);
					break;
				case "FreeForReferenti":
                    ConfigFreeFields1.SetFreeField(CRMTables.Base_Contacts);
					break;
				case "FreeForLeads":
                    ConfigFreeFields1.SetFreeField(CRMTables.CRM_Leads);
					break;
			}
			ConfigFreeFields1.Visible=true;
			ValueEditor1.Visible=false;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void btnScore_Click(object sender, EventArgs e)
		{
			rank1.Visible=true;
			currency1.Visible=false;
			ValueEditor1.Visible=false;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		protected void btnCurrency_Click(object sender, EventArgs e)
		{
			currency1.Visible=true;
			rank1.Visible=false;
			ValueEditor1.Visible=false;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            linksInfoPanel.Visible = false;
			MenuClick(((LinkButton) sender).ID);
		}

		private void btnLinks_Click(object sender, EventArgs e)
		{
            linksInfoPanel.Visible = true;
			ValueEditor1.Multiline=true;
			ValueEditor1.DataValueField="Name";
			ValueEditor1.DataValueFieldType=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueField2="Url";
			ValueEditor1.Lang="Country";
			ValueEditor1.DataValueFieldType2=ExtendedValueEditor.FieldType.varchar;
			ValueEditor1.DataValueText=new string[]{"Name", "Link"};
			ValueEditor1.TableName="Links";
            ValueEditor1.MaxSizeField1 = 100;
            ValueEditor1.MaxSizeField2 = 300;
			ValueEditor1.LoadGrid();
			ValueEditor1.Visible=true;
			this.LogosEditor1.Visible=false;
			ListConfiguration1.Visible=false;
			ConfigFreeFields1.Visible=false;
			rank1.Visible=false;
			currency1.Visible=false;
            Nprog1.Visible = false;
            ShipDates1.Visible = false;
            MenuClick(((LinkButton)sender).ID);

		}

		private void btnWizard_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/wizards/newuserwizard.aspx?m=7&si=99");
		}
	}
}
