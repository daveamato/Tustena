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
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Digita.Tustena.Report
{
	public partial class WebForm1 : Page
	{

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
			this.Button_add.Click += new EventHandler(this.Button_Click);
			this.Button_Parameter.Click += new EventHandler(this.Button_Click);
			this.Button_or.Click += new EventHandler(this.Button_Click);
			this.Button_and.Click += new EventHandler(this.Button_Click);
			this.Button_Openpar.Click += new EventHandler(this.Button_Click);
			this.Button_Closepar.Click += new EventHandler(this.Button_Click);
			this.Button_Equal.Click += new EventHandler(this.Button_Click);
			this.Button_Major.Click += new EventHandler(this.Button_Click);
			this.Button_Minus.Click += new EventHandler(this.Button_Click);
			this.Button_MajorEqual.Click += new EventHandler(this.Button_Click);
			this.Button_MinusEqual.Click += new EventHandler(this.Button_Click);
			this.Button_Different.Click += new EventHandler(this.Button_Click);
			this.Button_like.Click += new EventHandler(this.Button_Click);
			this.Button_rem.Click += new EventHandler(this.Button_Click);
			this.Button_save.Click += new EventHandler(this.Button_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void Button_Click(object sender, EventArgs e)
		{
			ErrorLabel.Text = String.Empty;
			int parCount = 0;
			int itemCount = 0;
			int lastitem = -1;

			Stack stackLastItem = new Stack();
			if (ViewState["itemcount"] != null)
				itemCount = (int) ViewState["itemcount"];
			if (ViewState["stacklastitem"] != null)
				stackLastItem = (Stack) ViewState["stacklastitem"];
			if (ViewState["parcount"] != null)
				parCount = (int) ViewState["parcount"];
			ArrayList myArray;
			if (ViewState["wb"] != null)
				myArray = (ArrayList) ViewState["wb"];
			else
				myArray = new ArrayList();
			if (stackLastItem.Count > 0)
				lastitem = (int) stackLastItem.Pop();
			else
				lastitem = 0;
			stackLastItem.Push(lastitem);
			switch (((Button) sender).ID)
			{
				case "Button_Parameter":
					if (lastitem == 1)
					{
						if (Parameter.Text.Length > 0)
						{
							myArray.Add('{' + Parameter.Text.Trim('-', ' ', '@', '!', '%', '&') + '}');
							lastitem = 2;
						}
						else
						{
							ErrorLabel.Text = "Campo vuoto!";
							lastitem = -1;
						}
					}
					else
					{
						ErrorLabel.Text = "Parametro non consentito in questa posizione";
						lastitem = -1;
					}
					break;
				case "Button_Equal":
					if (lastitem == 3)
					{
						myArray.Add("=");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_Major":
					if (lastitem == 3)
					{
						myArray.Add(">");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_Minus":
					if (lastitem == 3)
					{
						myArray.Add("<");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_MajorEqual":
					if (lastitem == 3)
					{
						myArray.Add(">=");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_MinusEqual":
					if (lastitem == 3)
					{
						myArray.Add("<=");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_Different":
					if (lastitem == 3)
					{
						myArray.Add("!=");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_like":
					if (lastitem == 3)
					{
						myArray.Add("like");
						lastitem = 1;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_or":
					if (lastitem == 5 || lastitem == 2)
					{
						myArray.Add("OR");
						lastitem = 0;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_and":
					if (lastitem == 5 || lastitem == 2)
					{
						myArray.Add("AND");
						lastitem = 0;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Operatore non consentito in questa posizione";
					}
					break;
				case "Button_Openpar":
					if (lastitem != -1)
					{
						myArray.Add("(");
						parCount++;
						lastitem = 4;
					}
					else
					{
						lastitem = -1;
						ErrorLabel.Text = "Parentesi non consentita in questa posizione";
					}
					break;
				case "Button_Closepar":
					if (parCount > 0 && (lastitem == 2 || lastitem == 5))
					{
						myArray.Add(")");
						parCount--;
						lastitem = 5;
					}
					else
					{
						if (lastitem != 4)
							ErrorLabel.Text = "Nessuna parentesi aperta!";
						else
							ErrorLabel.Text = "Parentesi appena aperta, vuoi gi chiudere?";
						lastitem = -1;
					}
					break;
				case "Button_add":
					if (lastitem == -1 || lastitem == 0 || lastitem == 1 || lastitem == 4)
					{
						if (ListBox1.SelectedIndex > -1)
						{
							myArray.Add(ListBox1.SelectedValue);
							itemCount++;
							lastitem = 3;
						}
						else
						{
							lastitem = -1;
							ErrorLabel.Text = "Nessun valore selezionato";
						}
					}
					else
						ErrorLabel.Text = "Chiave non consentita in questa posizione";
					break;
				case "Button_rem":
					if (myArray.Count > 0)
					{
						myArray.RemoveAt(myArray.Count - 1);
						itemCount++;
						if (lastitem == 4)
							parCount--;
						else if (lastitem == 5)
							parCount++;
						lastitem = -1;
						stackLastItem.Pop();
					}
					break;
				case "Button_save":
					if (myArray.Count > 2 && parCount < 1 && (lastitem == 2 || lastitem == 3 || lastitem == 5))
					{
						if (lastitem == 3)
						{
							stackLastItem.Pop();
							if ((int) stackLastItem.Pop() == 1)
							{
								WhereQuery.Text = "saved";
								return;
							}
						}
					}
					break;
			}
			WhereQuery.Text = PrintValues(myArray, ' ').ToString();

			if (lastitem != -1)
				stackLastItem.Push(lastitem);

			ViewState["wb"] = myArray;
			ViewState["parcount"] = parCount;
			ViewState["itemcount"] = itemCount;
			ViewState["stacklastitem"] = stackLastItem;
		}

		public string PrintValues(IEnumerable myCollection, char mySeparator)
		{
			IEnumerator myEnumerator = myCollection.GetEnumerator();
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


	}
}
