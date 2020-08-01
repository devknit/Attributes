using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class HideIfAttribute : ShowIfAttributeBase
	{
		public HideIfAttribute( string condition) :  base( condition)
		{
			Inverted = true;
		}
		public HideIfAttribute( ConditionOperator conditionOperator, params string[] conditions) : base( conditionOperator, conditions)
		{
			Inverted = true;
		}
	}
}
