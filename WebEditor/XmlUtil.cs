/* * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2004 Frederico Caldeira Knabben
 *
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 *
 * For further information visit:
 * 		http://www.fckeditor.net/
 *
 * File Name: XmlUtil.cs
 * 	Useful tools for XML.
 *
 * Version:  2.1
 * Modified: 2004-12-01 02:12:48
 *
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System.Xml;

namespace FredCK.FCKeditorV2
{
	internal sealed class XmlUtil
	{
		private XmlUtil()
		{}

		public static XmlNode AppendElement( XmlNode node, string newElementName )
		{
			return AppendElement( node, newElementName, null ) ;
		}

		public static XmlNode AppendElement( XmlNode node, string newElementName, string innerValue )
		{
			XmlNode oNode ;

			if ( node is XmlDocument )
                oNode = node.AppendChild( ((XmlDocument)node).CreateElement( newElementName ) ) ;
			else
				oNode = node.AppendChild( node.OwnerDocument.CreateElement( newElementName ) ) ;

			if ( innerValue != null )
				oNode.AppendChild( node.OwnerDocument.CreateTextNode( innerValue ) ) ;

			return oNode ;
		}

		public static XmlAttribute CreateAttribute( XmlDocument xmlDocument, string name, string value )
		{
			XmlAttribute oAtt = xmlDocument.CreateAttribute( name ) ;
			oAtt.Value = value ;
			return oAtt ;
		}

		public static void SetAttribute( XmlNode node, string attributeName, string attributeValue )
		{
			if ( node.Attributes[ attributeName ] != null )
				node.Attributes[ attributeName ].Value = attributeValue ;
			else
				node.Attributes.Append( CreateAttribute( node.OwnerDocument, attributeName, attributeValue ) ) ;
		}
	}
}
