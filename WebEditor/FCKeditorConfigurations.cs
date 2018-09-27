/* * FCKeditor - The text editor for internet
 * Copyright (C) 2003-2004 Frederico Caldeira Knabben
 *
 * Licensed under the terms of the GNU Lesser General Public License:
 * 		http://www.opensource.org/licenses/lgpl-license.php
 *
 * For further information visit:
 * 		http://www.fckeditor.net/
 *
 * File Name: FCKeditorConfigurations.cs
 * 	Class that holds all editor configurations.
 *
 * Version:  2.1
 * Modified: 2005-02-27 19:44:25
 *
 * File Authors:
 * 		Frederico Caldeira Knabben (fredck@fckeditor.net)
 */

using System.Collections;
using System.Text;

namespace FredCK.FCKeditorV2
{
	public class FCKeditorConfigurations
	{
		private Hashtable colConfigs ;

		internal FCKeditorConfigurations()
		{
			colConfigs = new Hashtable() ;
		}

		public string this[ string configurationName ]
		{
			get
			{
				if ( colConfigs.ContainsKey( configurationName ) )
					return (string)colConfigs[ configurationName ] ;
				else
					return null ;
			}
			set
			{
				colConfigs[ configurationName ] = value ;
			}
		}

		internal string GetHiddenFieldString()
		{
			StringBuilder osParams = new StringBuilder() ;

			foreach ( DictionaryEntry oEntry in colConfigs )
			{
				if ( osParams.Length > 0 )
					osParams.Append( '&' ) ;

				osParams.AppendFormat( "{0}={1}", EncodeConfig( oEntry.Key.ToString() ), EncodeConfig( oEntry.Value.ToString() ) ) ;
			}

			return osParams.ToString() ;
		}

		private string EncodeConfig( string valueToEncode )
		{
			string sEncoded = valueToEncode.Replace( "&", "%26" ) ;
			sEncoded = sEncoded.Replace( "=", "%3D" ) ;
			sEncoded = sEncoded.Replace( "\"", "%22" ) ;

			return sEncoded ;
		}
	}
}
