
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class EnableIfAttribute : EnableIfAttributeBase
	{
		public EnableIfAttribute( string condition) : base( condition)
		{
			Inverted = false;
		}
		public EnableIfAttribute( ConditionOperator conditionOperator, params string[] conditions) : base( conditionOperator, conditions)
		{
			Inverted = false;
		}
	}
}
