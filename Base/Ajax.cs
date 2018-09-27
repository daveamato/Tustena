/* * My Ajax.NET
 *
 * Release 5
 *
 * See release history at the end of the file for changes.
 *
 * This code is intended to be simple enough so that anybody can
 * understand it and, therefore, feel good about including it in
 * their own projects and even modifying it.
 *
 * Originally written by Jason Diamond but donated to the Public
 * Domain which means anybody can use it for any reason. If you make
 * any modifications and want to contribute them to the "official"
 * release, please send them to <mailto:jason@diamond.name>.
 *
 * People who contributed code:
 *
 * Jason Diamond <http://jason.diamond.name/>
 * Rick Strahl <http://www.west-wind.com/>
 * Thomas F Kelly, Jr.
 * Chris Payne
 *
 */

using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Ajax
{
	[AttributeUsage(AttributeTargets.Method)]
	public class MethodAttribute : Attribute
	{
	}

	[Flags]
	public enum Debug
	{
		None         = 0,
		RequestText  = 1,
		ResponseText = 2,
		Errors       = 4,
		All          = 7
	}

	public class Manager
	{
		public static void Register(Page page)
		{
			Register(page, page.GetType().FullName, false, Debug.None);
		}

		public static void Register(Page page, string prefix)
		{
			Register(page, prefix, false, Debug.None);
		}

		public static void Register(Page page, Debug debug)
		{
			Register(page, page.GetType().FullName, false, debug);
		}

		public static void Register(Page page, string prefix, Debug debug)
		{
			Register(page, prefix, false, debug);
		}

		public static void Register(Control control)
		{
			Register(control, control.GetType().FullName, true, Debug.None);
		}

		public static void Register(Control control, string prefix)
		{
			Register(control, prefix, true, Debug.None);
		}

		public static void Register(Control control, Debug debug)
		{
			Register(control, control.GetType().FullName, true, debug);
		}

		public static void Register(Control control, string prefix, bool requireID, Debug debug)
		{
			#region "Integrated javascript code removed"
			#endregion
            control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), typeof(Manager).FullName, "<script language=\"javascript\" src=\"/js/ajax.js\"></script>");

			Type type = control.GetType();
			StringBuilder controlScript = new StringBuilder();
			controlScript.Append("\n<script>\n");
			string[] prefixParts = prefix.Split('.', '+');
			controlScript.AppendFormat("var {0} = {{\n", prefixParts[0]);
			for (int i = 1; i < prefixParts.Length; ++i)
			{
				controlScript.AppendFormat("\"{0}\": {{\n", prefixParts[i]);
			}
			int methodCount = 0;
			foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Public))
			{
				object[] attributes = methodInfo.GetCustomAttributes(typeof(MethodAttribute), true);
				if (attributes != null && attributes.Length > 0)
				{
					++methodCount;
					controlScript.AppendFormat("\n\"{0}\": function(", methodInfo.Name);
					if (requireID)
					{
						controlScript.AppendFormat("id, ");
					}
					foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
					{
						controlScript.Append(paramInfo.Name + ", ");
					}
					controlScript.AppendFormat(
						"clientCallBack) {{\n\treturn Ajax_CallBack('{0}', {1}, '{2}', [",
						type.FullName,
						requireID ? "id" : "null",
						methodInfo.Name);
					int paramCount = 0;
					foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
					{
						++paramCount;
						controlScript.Append(paramInfo.Name);
						controlScript.Append(",");
					}
					if (paramCount > 0)
					{
						--controlScript.Length;
					}
					controlScript.AppendFormat("], clientCallBack, {0}, {1}, {2});\n}}",
						(debug & Debug.RequestText) == Debug.RequestText ? "true" : "false",
						(debug & Debug.ResponseText) == Debug.ResponseText ? "true" : "false",
						(debug & Debug.Errors) == Debug.Errors ? "true" : "false");

					controlScript.Append(",\n");
				}
			}
			if (methodCount == 0)
			{
				throw new ApplicationException(string.Format("{0} does not contain any public methods with the Ajax.Method attribute.", type.FullName));
			}
			controlScript.Length -= 2;
			controlScript.Append("\n\n");
			for (int i = 0; i < prefixParts.Length; ++i)
			{
				controlScript.Append("}");
			}
			controlScript.Append(";\n</script>");
            control.Page.ClientScript.RegisterClientScriptBlock(control.GetType(), "Ajax.Manager:" + type.FullName, controlScript.ToString());
			control.PreRender += new EventHandler(OnPreRender);
		}

		public static string CallBackType
		{
			get
			{
				return HttpContext.Current.Request.Form["Ajax_CallBackType"];
			}
		}

		public static string CallBackID
		{
			get
			{
				return HttpContext.Current.Request.Form["Ajax_CallBackID"];
			}
		}

		public static string CallBackMethod
		{
			get
			{
				return HttpContext.Current.Request.Form["Ajax_CallBackMethod"];
			}
		}

		public static bool IsCallBack
		{
			get
			{
				return CallBackType != null;
			}
		}

		static void OnPreRender(object s, EventArgs e)
		{
			Control control = s as Control;
			if (control != null)
			{
				MethodInfo methodInfo = FindTargetMethod(control);
				if (methodInfo != null)
				{
					object val = null;
					string error = null;
					try
					{
						object[] parameters = ConvertParameters(methodInfo, HttpContext.Current.Request);
						val = InvokeMethod(control, methodInfo, parameters);
					}
					catch (Exception ex)
					{
						error = ex.Message;
					}
					HttpResponse resp = HttpContext.Current.Response;
					WriteResult(resp, val, error);
					resp.End();
				}
			}
		}

		static MethodInfo FindTargetMethod(Control control)
		{
			string typeName = CallBackType;
			if (typeName != null)
			{
				Type type = control.GetType();
				if (type.FullName == typeName)
				{
					if (control is Page || control.ID == CallBackID)
					{
						string methodName = CallBackMethod;
						MethodInfo methodInfo = type.GetMethod(methodName);
						object[] methodAttributes = methodInfo.GetCustomAttributes(typeof(MethodAttribute), true);
						if (methodAttributes.Length > 0)
						{
							return methodInfo;
						}
					}
				}
			}
			return null;
		}

		static object[] ConvertParameters(
			MethodInfo methodInfo,
			HttpRequest req)
		{
			object[] parameters = new object[methodInfo.GetParameters().Length];
			int i = 0;
			foreach (ParameterInfo paramInfo in methodInfo.GetParameters())
			{
				object param = null;
				string paramValue = req.Form["Ajax_CallBackArgument" + i];
				if (paramValue != null)
				{
					param = Convert.ChangeType(paramValue, paramInfo.ParameterType);
				}
				parameters[i] = param;
				++i;
			}
			return parameters;
		}

		static object InvokeMethod(
			Control control,
			MethodInfo methodInfo,
			object[] parameters)
		{
			object val = null;
			try
			{
				val = methodInfo.Invoke(control, parameters);
			}
			catch (TargetInvocationException ex)
			{
				if (ex.InnerException != null)
				{
					throw ex.InnerException;
				}
				else
				{
					throw ex;
				}
			}
			return val;
		}

		public static void WriteResult(
			HttpResponse resp,
			object val,
			string error)
		{
			resp.ContentType = "application/x-javascript";
			resp.Cache.SetCacheability(HttpCacheability.NoCache);
			StringBuilder sb = new StringBuilder();
			try
			{
				WriteResult(sb, val, error);
			}
			catch (Exception ex)
			{
				sb.Length = 0;
				WriteResult(sb, null, ex.Message);
			}
			resp.Write(sb.ToString());
		}

		static void WriteResult(StringBuilder sb, object val, string error)
		{
			sb.Append("{\"value\":");
			WriteValue(sb, val);
			sb.Append(",\"error\":");
			WriteValue(sb, error);
			sb.Append("}");
		}

		static void WriteValue(StringBuilder sb, object val)
		{
			if (val == null || val == DBNull.Value)
			{
				sb.Append("null");
			}
			else if (val is string)
			{
				WriteString(sb, val as String);
			}
			else if (val is bool)
			{
				sb.Append(val.ToString().ToLower());
			}
			else if (val is double ||
				val is float ||
				val is long ||
				val is int ||
				val is short ||
				val is byte)
			{
				sb.Append(val);
			}
			else if (val is DateTime)
			{
				sb.Append("new Date(\"");
				sb.Append(((DateTime)val).ToString("MMMM, d yyyy HH:mm:ss"));
				sb.Append("\")");
			}
			else if (val is DataSet)
			{
				WriteDataSet(sb, val as DataSet);
			}
			else if (val is DataTable)
			{
				WriteDataTable(sb, val as DataTable);
			}
			else if (val is DataRow)
			{
				WriteDataRow(sb, val as DataRow);
			}
			else if (val is IEnumerable)
			{
				WriteEnumerable(sb, val as IEnumerable);
			}
			else
			{
				WriteString(sb, val.ToString());
			}
		}

		static void WriteString(StringBuilder sb, string s)
		{
			sb.Append("\"");
			foreach (char c in s)
			{
				switch (c)
				{
					case '\"':
						sb.Append("\\\"");
						break;
					case '\\':
						sb.Append("\\\\");
						break;
					case '\b':
						sb.Append("\\b");
						break;
					case '\f':
						sb.Append("\\f");
						break;
					case '\n':
						sb.Append("\\n");
						break;
					case '\r':
						sb.Append("\\r");
						break;
					case '\t':
						sb.Append("\\t");
						break;
					default:
						int i = (int)c;
						if (i < 32 || i > 127)
						{
							sb.AppendFormat("\\u{0:X04}", i);
						}
						else
						{
							sb.Append(c);
						}
						break;
				}
			}
			sb.Append("\"");
		}

		static void WriteDataSet(StringBuilder sb, DataSet ds)
		{
			sb.Append("{\"Tables\":{");
			foreach (DataTable table in ds.Tables)
			{
				sb.AppendFormat("\"{0}\":", table.TableName);
				WriteDataTable(sb, table);
				sb.Append(",");
			}
			if (ds.Tables.Count > 0)
			{
				--sb.Length;
			}
			sb.Append("}}");
		}

		static void WriteDataTable(StringBuilder sb, DataTable table)
		{
			sb.Append("{\"Rows\":[");
			foreach (DataRow row in table.Rows)
			{
				WriteDataRow(sb, row);
				sb.Append(",");
			}
			if (table.Rows.Count > 0)
			{
				--sb.Length;
			}
			sb.Append("]}");
		}

		static void WriteDataRow(StringBuilder sb, DataRow row)
		{
			sb.Append("{");
			foreach (DataColumn column in row.Table.Columns)
			{
				sb.AppendFormat("\"{0}\":", column.ColumnName);
				WriteValue(sb, row[column]);
				sb.Append(",");
			}
			if (row.Table.Columns.Count > 0)
			{
				--sb.Length;
			}
			sb.Append("}");
		}

		static void WriteEnumerable(StringBuilder sb, IEnumerable e)
		{
			bool hasItems = false;
			sb.Append("[");
			foreach (object val in e)
			{
				WriteValue(sb, val);
				sb.Append(",");
				hasItems = true;
			}
			if (hasItems)
			{
				--sb.Length;
			}
			sb.Append("]");
		}
	}
}

