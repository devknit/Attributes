
namespace Attributes
{
	public abstract class EnableIfAttributeBase : MetaAttribute
	{
		public EnableIfAttributeBase( string condition)
		{
			ConditionOperator = ConditionOperator.kAnd;
			Conditions = new string[]{ condition };
		}
		public EnableIfAttributeBase( ConditionOperator conditionOperator, params string[] conditions)
		{
			ConditionOperator = conditionOperator;
			Conditions = conditions;
		}
		public string[] Conditions
		{
			get;
			private set;
		}
		public ConditionOperator ConditionOperator
		{
			get;
			private set;
		}
		public bool Inverted
		{
			get;
			protected set;
		}
	}
}
