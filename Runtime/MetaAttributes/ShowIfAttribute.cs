
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ShowIfAttribute : ShowIfAttributeBase
	{
		public ShowIfAttribute( string condition) : base( condition)
		{
			Inverted = false;
		}
		public ShowIfAttribute( ConditionOperator conditionOperator, params string[] conditions) : base( conditionOperator, conditions)
		{
			Inverted = false;
		}
	}
}
