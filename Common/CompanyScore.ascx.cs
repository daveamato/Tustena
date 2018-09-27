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
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Digita.Tustena.Common
{
	public partial class CompanyScore : UserControl
	{


		protected void Page_Load(object sender, EventArgs e)
		{
		}

		private byte _score=5;

		public byte Score
		{
			get{return _score;}
			set{_score=value;}
		}

		private long _cross=0;
		public long CrossID
		{
			get{return _cross;}
			set{_cross=value;}
		}

		public void MakeScore()
		{
			rankerindex.Attributes.Add("style",string.Format("width:{0}px",_score));
		}

		private string Stars(byte iStars)
		{
			string stars=String.Empty;
			if (iStars > 0)
			{
				bool halfStar = false;
				if (iStars%2 > 0)
				{
					halfStar = true;
					iStars--;
				}
				iStars = (byte) (iStars/2);
				stars = String.Empty;
				for (int i = 0; i < iStars; i++)
					stars += "<img src='/images/starfull.gif'>";
				if (halfStar == true)
				{
					stars += "<img src='/images/starthalf.gif'>";
					iStars++;
				}
				for (int i = iStars + 1; i <= 5; i++)
					stars += "<img src='/images/starnone.gif'>";
			}else
				stars = "<img src='/images/starnone.gif'><img src='/images/starnone.gif'><img src='/images/starnone.gif'><img src='/images/starnone.gif'><img src='/images/starnone.gif'>";
			return stars;
		}
		#region Codice generato da Progettazione Web Form
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);

		}
		#endregion
	}
}
