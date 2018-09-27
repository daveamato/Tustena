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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace Digita.Tustena.WebControls
{
    [ToolboxData("<{0}:GroupDropDownList runat=server></{0}:GroupDropDownList>")]
    public class GroupDropDownList : System.Web.UI.WebControls.DropDownList
    {
        [DefaultValue(""), Category("Data")]
        public virtual string DataGroupField
        {
            get
            {
                object obj = this.ViewState["DataGroupField"];
                if (obj != null)
                {
                    return (string)obj;
                }
                return string.Empty;
            }
            set
            {
                this.ViewState["DataGroupField"] = value;
            }
        }


        protected override void RenderContents(HtmlTextWriter writer)
        {
            ListItemCollection items = this.Items;
            int itemCount = this.Items.Count;
            string curGroup = String.Empty;
            string itemGroup;
            bool bSelected = false;

            if (itemCount <= 0)
            {
                return;
            }

            for (int i = 0; i < itemCount; i++)
            {
                ListItem item = items[i];
                itemGroup = (string)item.Attributes["DataGroupField"];
                if (itemGroup != null && itemGroup != curGroup)
                {
                    if (curGroup != String.Empty)
                    {
                        writer.WriteEndTag("optgroup");
                        writer.WriteLine();
                    }

                    curGroup = itemGroup;
                    writer.WriteBeginTag("optgroup");
                    writer.WriteAttribute("label", curGroup, true);
                    writer.Write('>');
                    writer.WriteLine();
                }

                writer.WriteBeginTag("option");
                if (item.Selected)
                {
                    if (bSelected)
                    {
                        throw new HttpException("Cant_Multiselect_In_DropDownList");
                    }
                    bSelected = true;
                    writer.WriteAttribute("selected", "selected", false);
                }
                writer.WriteAttribute("value", item.Value, true);
                writer.Write('>');
                HttpUtility.HtmlEncode(item.Text, writer);
                writer.WriteEndTag("option");
                writer.WriteLine();
            }
            if (curGroup != String.Empty)
            {
                writer.WriteEndTag("optgroup");
                writer.WriteLine();
            }
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            if (this.DataGroupField == String.Empty)
            {
                return;
            }

            IEnumerable dataSource = GetResolvedDataSource(this.DataSource, this.DataMember);
            if (dataSource != null)
            {
                ListItemCollection items = this.Items;
                int i = 0;

                string groupField = this.DataGroupField;
                foreach (object obj in dataSource)
                {
                    string groupFieldValue = DataBinder.GetPropertyValue(obj, groupField, null);
                    ListItem item = items[i];
                    item.Attributes.Add("DataGroupField", groupFieldValue);
                    i++;
                }
            }

        }

        private IEnumerable GetResolvedDataSource(object dataSource, string dataMember)
        {
            if (dataSource != null)
            {
                IListSource source1 = dataSource as IListSource;
                if (source1 != null)
                {
                    IList list1 = source1.GetList();
                    if (!source1.ContainsListCollection)
                    {
                        return list1;
                    }
                    if ((list1 != null) && (list1 is ITypedList))
                    {
                        ITypedList list2 = (ITypedList)list1;
                        PropertyDescriptorCollection collection1 = list2.GetItemProperties(new PropertyDescriptor[0]);
                        if ((collection1 == null) || (collection1.Count == 0))
                        {
                            throw new HttpException("ListSource_Without_DataMembers");
                        }
                        PropertyDescriptor descriptor1 = null;
                        if ((dataMember == null) || (dataMember.Length == 0))
                        {
                            descriptor1 = collection1[0];
                        }
                        else
                        {
                            descriptor1 = collection1.Find(dataMember, true);
                        }
                        if (descriptor1 != null)
                        {
                            object obj1 = list1[0];
                            object obj2 = descriptor1.GetValue(obj1);
                            if ((obj2 != null) && (obj2 is IEnumerable))
                            {
                                return (IEnumerable)obj2;
                            }
                        }
                        throw new HttpException("ListSource_Missing_DataMember");
                    }
                }
                if (dataSource is IEnumerable)
                {
                    return (IEnumerable)dataSource;
                }
            }
            return null;
        }

    }
}
