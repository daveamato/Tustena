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
using System.Text;

namespace Drew.Util
{
	[Serializable]
	public class FilterBuilder
	{
		#region CreateEmpty

		public static FilterBuilder CreateEmpty()
		{
			return new FilterBuilder();
		}


		#endregion

		const string DateFormat = "dd MMM yyyy HH:mm:ss";

		ConditionNode _topNode;

		public FilterBuilder()
		{
			_topNode = null;
		}


		#region Equals

		public FilterBuilder EqualTo(string columnName, object equalTo)
		{
			return And(CreateEqualToCondition(columnName, equalTo));
		}

		public FilterBuilder OrEqualTo(string columnName, object equalTo)
		{
			return Or(CreateEqualToCondition(columnName, equalTo));
		}

		private ConditionNode CreateEqualToCondition(string columnName, object equalTo)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(equalTo), "=");
			return condition;
		}


		#endregion

		#region NotEqualTo

		public FilterBuilder NotEqualTo(string columnName, object notEqualTo)
		{
			return And(CreateNotEqualToCondition(columnName, notEqualTo));

		}

		public FilterBuilder OrNotEqualTo(string columnName, object notEqualTo)
		{
			return Or(CreateNotEqualToCondition(columnName, notEqualTo));
		}

		private ConditionNode CreateNotEqualToCondition(string columnName, object notEqualTo)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(notEqualTo), "<>");
			return condition;
		}

		#endregion

		#region GreaterThan

		public FilterBuilder GreaterThan(string columnName, object greaterThan)
		{
			return And(CreateGreaterThanCondition(columnName, greaterThan));
		}

		public FilterBuilder OrGreaterThan(string columnName, object greaterThan)
		{
			return Or(CreateGreaterThanCondition(columnName, greaterThan));
		}

		private ConditionNode CreateGreaterThanCondition(string columnName, object greaterThan)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(greaterThan), ">");
			return condition;
		}

		#endregion

		#region GreaterThanOrEqualTo

		public FilterBuilder GreaterThanOrEqualTo(string columnName, object greaterThanOrEqualTo)
		{
			return And(CreateGreaterThanOrEqualToCondition(columnName, greaterThanOrEqualTo));
		}

		public FilterBuilder OrGreaterThanOrEqualTo(string columnName, object greaterThanOrEqualTo)
		{
			return Or(CreateGreaterThanOrEqualToCondition(columnName, greaterThanOrEqualTo));
		}

		private ConditionNode CreateGreaterThanOrEqualToCondition(string columnName, object greaterThanOrEqualTo)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(greaterThanOrEqualTo), ">=");
			return condition;
		}


		#endregion

		#region LessThan

		public FilterBuilder LessThan(string columnName, object lessThan)
		{
			return And(CreateLessThanCondition(columnName, lessThan));
		}

		public FilterBuilder OrLessThan(string columnName, object lessThan)
		{
			return Or(CreateLessThanCondition(columnName, lessThan));
		}

		private ConditionNode CreateLessThanCondition(string columnName, object lessThan)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(lessThan), "<");
			return condition;
		}

		#endregion

		#region LessThanOrEqualTo

		public FilterBuilder LessThanOrEqualTo(string columnName, object lessThanOrEqualTo)
		{
			return And(CreateLessThanOrEqualToCondition(columnName, lessThanOrEqualTo));
		}

		public FilterBuilder OrLessThanOrEqualTo(string columnName, object lessThanOrEqualTo)
		{
			return Or(CreateLessThanOrEqualToCondition(columnName, lessThanOrEqualTo));
		}

		private ConditionNode CreateLessThanOrEqualToCondition(string columnName, object lessThanOrEqualTo)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(lessThanOrEqualTo), "<=");
			return condition;
		}


		#endregion

		#region Like

		public FilterBuilder Like(string columnName, string likePattern)
		{
			return And(CreateLikeCondition(columnName, likePattern));
		}

		public FilterBuilder OrLike(string columnName, string likePattern)
		{
			return Or(CreateLikeCondition(columnName, likePattern));
		}

		private ConditionNode CreateLikeCondition(string columnName, string likePattern)
		{
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(likePattern), " Like ");
			return condition;
		}

		#endregion

		#region ContainsString

		public FilterBuilder ContainsString(string columnName, string subString)
		{
			return And(CreateContainsStringCondition(subString, columnName));
		}

		public FilterBuilder OrContainsString(string columnName, string subString)
		{
			return Or(CreateContainsStringCondition(subString, columnName));
		}

		private ConditionNode CreateContainsStringCondition(string subString, string columnName)
		{
			string likePattern = string.Format("%{0}%", subString);
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(likePattern), " Like ");
			return condition;
		}

		#endregion

		#region StartsWithString

		public FilterBuilder StartsWithString(string columnName, string prefix)
		{
			return And(CreateStartsWithStringCondition(prefix, columnName));
		}

		public FilterBuilder OrStartsWithString(string columnName, string prefix)
		{
			return Or(CreateStartsWithStringCondition(prefix, columnName));
		}

		private ConditionNode CreateStartsWithStringCondition(string prefix, string columnName)
		{
			string likePattern = string.Format("{0}%", prefix);
			ConditionNode condition
				= new OperatorNode(new ColumnNameNode(columnName), new ValueNode(likePattern), " Like ");
			return condition;
		}

		#endregion

		#region ContainsStringWithinCommaSeparatedValues

		public FilterBuilder ContainsStringWithinCommaSeparatedValues(string columnName, string csvValue)
		{
			return And(CreateContainsStringWithinCommaSeparatedValuesCondition(columnName, csvValue));
		}

		public FilterBuilder OrContainsStringWithinCommaSeparatedValues(string columnName, string csvValue)
		{
			return Or(CreateContainsStringWithinCommaSeparatedValuesCondition(columnName, csvValue));
		}

		private ConditionNode CreateContainsStringWithinCommaSeparatedValuesCondition(string columnName, string csvValue)
		{
			FilterBuilder likeFilter = new FilterBuilder();
			likeFilter.EqualTo(columnName, csvValue);
			likeFilter.OrLike(columnName, string.Format("{0},%", csvValue));
			likeFilter.OrLike(columnName, string.Format("%,{0}", csvValue));
			likeFilter.OrLike(columnName, string.Format("%,{0},%", csvValue));
			ConditionNode condition = likeFilter._topNode;
			return condition;
		}

		#endregion

		#region IsNotNull

		public FilterBuilder IsNotNull(string columnName)
		{
			return And(CreateIsNotNullCondition(columnName));
		}

		public FilterBuilder OrIsNotNull(string columnName)
		{
			return Or(CreateIsNotNullCondition(columnName));
		}

		private ConditionNode CreateIsNotNullCondition(string columnName)
		{
			ConditionNode condition
				= new NullCheckingNode(new ColumnNameNode(columnName), false);
			return condition;
		}

		#endregion

		#region IsNull

		public FilterBuilder IsNull(string columnName)
		{
			return And(CreateIsNullCondition(columnName));
		}

		public FilterBuilder OrIsNull(string columnName)
		{
			return Or(CreateIsNullCondition(columnName));
		}

		private ConditionNode CreateIsNullCondition(string columnName)
		{
			ConditionNode condition
				= new NullCheckingNode(new ColumnNameNode(columnName), true);
			return condition;
		}

		#endregion

		#region ToString and GetExpressionString

		public override string ToString()
		{
			if (!HasExpression)
				return string.Empty;

			StringBuilder sb = new StringBuilder();
			sb.Append("Where ");
			sb.Append(GetExpressionString());
			return sb.ToString();
		}


		public string GetExpressionString()
		{
			if (!HasExpression)
				return string.Empty;

			return _topNode.GetExpressionString();
		}


		#endregion

		#region And & Or operator support

		public FilterBuilder Or(params FilterBuilder[] innerQueries)
		{
			foreach (FilterBuilder innerQuery in innerQueries)
			{
				if (innerQuery._topNode!=null)
					Or(innerQuery._topNode);
			}

			return this;
		}

		FilterBuilder Or(ConditionNode newCondition)
		{
			if (_topNode==null)
			{
				_topNode = newCondition;
			}
			else
			{
				MergeConditionIntoTree(LogicalOperator.OR, newCondition);
			}
			return this;
		}

		public FilterBuilder And(params FilterBuilder[] innerQueries)
		{
			foreach (FilterBuilder innerQuery in innerQueries)
			{
				if (innerQuery._topNode!=null)
					And(innerQuery._topNode);
			}

			return this;
		}

		FilterBuilder And(ConditionNode newCondition)
		{
			if (newCondition==null)
				throw new ArgumentNullException("newCondition", "condition may not be null");

			if (_topNode==null)
			{
				_topNode = newCondition;
			}
			else
			{
				MergeConditionIntoTree(LogicalOperator.AND, newCondition);
			}
			return this;
		}

		void MergeConditionIntoTree(LogicalOperator desiredLogicalOperator, ConditionNode newCondition)
		{
			if (newCondition==null)
				throw new ArgumentNullException("newCondition", "condition may not be null");

			if (_topNode is LogicalNode && ((LogicalNode)_topNode).Operator==desiredLogicalOperator)
			{
				LogicalNode logicalNode = _topNode as LogicalNode;
				logicalNode.AddCondition(newCondition);
			}
			else
			{
				LogicalNode newTopNode = new LogicalNode(desiredLogicalOperator);
				newTopNode.AddCondition(_topNode);
				newTopNode.AddCondition(newCondition);
				_topNode = newTopNode;
			}
		}

		#endregion

		#region Properties

		public bool HasExpression
		{
			get
			{
				return _topNode!=null;
			}
		}

		#endregion

		#region Node classes

		enum LogicalOperator
		{
			AND,
			OR
		}

		abstract class ConditionNode
		{
			public abstract string GetExpressionString();
		}

		class LogicalNode : ConditionNode
		{
			ArrayList _childNodes; // may contain only ConditionNode entries...
			LogicalOperator _operator; // eg. AND or OR

			public LogicalNode(LogicalOperator logicalOperator)
			{
				_operator = logicalOperator;
				_childNodes = new ArrayList();
			}

			public override string GetExpressionString()
			{
				StringBuilder sb = new StringBuilder();
				for (int i=0; i<_childNodes.Count; i++)
				{
					if (i>0)
					{
						sb.AppendFormat(" {0} ", OperatorName);
					}
					ConditionNode child = (ConditionNode)_childNodes[i];

					string formatString;
					if (child is LogicalNode)
						formatString = "({0})";
					else
						formatString = "{0}";

					sb.AppendFormat(formatString, child.GetExpressionString());
				}
				return sb.ToString();
			}

			public void AddCondition(ConditionNode condition)
			{
				if (condition==null)
					throw new ArgumentNullException("condition", "Cannot add a null condition.");

				_childNodes.Add(condition);
			}

			public LogicalOperator Operator
			{
				get
				{
					return _operator;
				}
			}

			string OperatorName
			{
				get
				{
					if (_operator==LogicalOperator.AND)
						return "And";
					else if (_operator==LogicalOperator.OR)
						return "Or";
					else
						throw new Exception("Unsupported logical operator: " + _operator);
				}
			}
		}

		class OperatorNode : ConditionNode
		{
			string _operator;
			LiteralNode _left;
			LiteralNode _right;

			public OperatorNode(LiteralNode left, LiteralNode right, string operatorString)
			{
				_left = left;
				_right = right;
				_operator = operatorString;
			}

			public override string GetExpressionString()
			{
				return string.Format("{0}{1}{2}", _left.ToString(), _operator, _right.ToString());
			}
		}

		class NullCheckingNode : ConditionNode
		{
			bool _mustBeNull;
			ColumnNameNode _column;
			public NullCheckingNode(ColumnNameNode column, bool desireNull)
			{
				_column = column;
				_mustBeNull = desireNull;
			}
			public override string GetExpressionString()
			{
				if (_mustBeNull)
					return string.Format("{0} Is Null", _column.ToString());
				else
					return string.Format("{0} Is Not Null", _column.ToString());
			}
		}


		abstract class LiteralNode
		{
		}

		class ValueNode : LiteralNode
		{
			const string DateFormat = "dd MMM yyyy HH:mm:ss";

			object _value;
			public ValueNode(object val)
			{
				_value = val;
			}
			public override string ToString()
			{
				return FormatValue(_value);
			}
			string FormatValue(object val)
			{
				if (val is DateTime)
				{
					return string.Format("'{0}'", ((DateTime)val).ToString(DateFormat));
				}
				else if (val is int)
				{
					return string.Format("{0}", val);
				}
				else
				{
					EscapeString(ref val);
					return string.Format("'{0}'", val);
				}
			}

			void EscapeString(ref object val)
			{
				val = val.ToString().Replace("'", "''");
			}
		}

		class ColumnNameNode : LiteralNode
		{
			string _columnName;
			public ColumnNameNode(string columnName)
			{
				_columnName = columnName;
			}
			public override string ToString()
			{
				return string.Format("[{0}]", _columnName);
			}
		}


		#endregion

		#region Equals & GetHashCode

		public override bool Equals(object obj)
		{
			FilterBuilder that = obj as FilterBuilder;

			if (that==null)
				return false;

			return this.GetExpressionString().Equals(that.GetExpressionString());
		}

		public override int GetHashCode()
		{
			return GetExpressionString().GetHashCode();
		}



		#endregion
	}
}

