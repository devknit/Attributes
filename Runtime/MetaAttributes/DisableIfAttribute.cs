
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class DisableIfAttribute : EnableIfAttributeBase
	{
		public DisableIfAttribute( string condition) : base(condition)
		{
			Inverted = true;
		}
		public DisableIfAttribute( ConditionOperator conditionOperator, params string[] conditions) : base( conditionOperator, conditions)
		{
			Inverted = true;
		}
	}
}
