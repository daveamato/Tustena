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
using System.Web.UI;
using System.Web.UI.WebControls;
using Digita.Tustena.Database;
using Digita.Tustena.WebControls;

namespace Digita.Tustena.Common
{
	public partial class PopScoreValues : G
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Login())
			{
				ClientScript.RegisterStartupScript(this.GetType(), "ret","<script>opener.location.href=opener.location.href;self.close();</script>");
			}
			else
			{
				Localizedliteral3.Visible=false;
				ViewState["crossid"]=Request["crossid"];
				ViewState["type"]=Request["type"];
				scorerepeater.DataSource=DatabaseConnection.CreateDataset("SELECT * FROM SCOREDESCRIPTION");
				scorerepeater.DataBind();
				if(scorerepeater.Items.Count<=0)
				{
					this.scorerepeater.Visible=false;
					this.Localizedliteral3.Visible=true;
				}
			}
		}

		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.scorerepeater.ItemDataBound+=new RepeaterItemEventHandler(scorerepeater_ItemDataBound);
			this.scorerepeater.ItemCommand+=new RepeaterCommandEventHandler(scorerepeater_ItemCommand);
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion

		private void scorerepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
				case ListItemType.Item:
				case ListItemType.AlternatingItem:
					TextBox ScoreDescription = (TextBox)e.Item.FindControl("ScoreDescription");
					Label ScoreID = (Label)e.Item.FindControl("ScoreID");
					ScoreID.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "id").ToString();
					ScoreDescription.Text=DataBinder.Eval((DataRowView) e.Item.DataItem, "Description").ToString();
					DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM SCOREVALUES WHERE IDDESCRIPTION="+ScoreID.Text+" AND IDCROSS="+ViewState["crossid"]+" AND TYPE="+ViewState["type"]).Tables[0];
					if(dt.Rows.Count>0)
					{
						LinkButton VotePlus = (LinkButton)e.Item.FindControl("VotePlus");
						LinkButton VoteMinus = (LinkButton)e.Item.FindControl("VoteMinus");
						DataTable dvote = DatabaseConnection.CreateDataset("SELECT ID FROM SCORELOG WHERE OWNERID="+UC.UserId+" AND IDVALUE="+dt.Rows[0]["id"]+" AND CONVERT(VARCHAR(10),VOTEDATE,112)='"+DateTime.Now.ToString(@"yyyyMMdd")+"'").Tables[0];
						if(dvote.Rows.Count>0)
						{
							VotePlus.Visible=false;
							VoteMinus.Visible=false;
						}
					}


					break;
			}
		}

		private void scorerepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
		{
			int scoreValue=0;
			int voteNumber=1;
			Label ScoreID = (Label)e.Item.FindControl("ScoreID");
			DataTable dt = DatabaseConnection.CreateDataset("SELECT * FROM SCOREVALUES WHERE IDDESCRIPTION="+ScoreID.Text+" AND IDCROSS="+ViewState["crossid"]+" AND TYPE="+ViewState["type"]).Tables[0];
			if(dt.Rows.Count>0)
			{
				scoreValue=Convert.ToInt32(dt.Rows[0]["scorevalue"]);
				voteNumber+=Convert.ToInt32(dt.Rows[0]["votenumber"]);
			}

			int vote=0;
			switch(e.CommandName)
			{
				case "VotePlus":
					scoreValue++;
					vote=1;
					break;
				case "VoteMinus":
					scoreValue--;
					vote=-1;
					break;
			}

			long newid=0;
			using(DigiDapter dg = new DigiDapter())
			{
				dg.Add("SCOREVALUE",scoreValue);
				dg.Add("VOTENUMBER",voteNumber);
				dg.Add("IDDESCRIPTION",ScoreID.Text);
				dg.Add("IDCROSS",ViewState["crossid"],'I');
				dg.Add("TYPE",ViewState["type"],'I');

				object nid;
				if(dt.Rows.Count>0)
				{
					nid=dt.Rows[0]["id"];
					dg.Execute("SCOREVALUES","id="+dt.Rows[0]["id"],DigiDapter.Identities.Identity);
				}
				else
					nid=dg.Execute("SCOREVALUES",DigiDapter.Identities.Identity);

				newid=Convert.ToInt64(nid);
			}

			DatabaseConnection.DoCommand(string.Format("INSERT INTO SCORELOG (IDVALUE,VOTEDATE,OWNERID,VOTE) VALUES ({0},GETDATE(),{1},{2})",newid,UC.UserId,vote));
			ClientScript.RegisterStartupScript(this.GetType(), "close","<script>self.close();parent.HideBox();</script>");
		}
	}
}
