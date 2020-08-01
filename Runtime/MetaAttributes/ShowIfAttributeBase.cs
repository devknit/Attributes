
namespace Attributes
{
	public abstract class ShowIfAttributeBase : MetaAttribute
	{
		public ShowIfAttributeBase( string condition)
		{
			ConditionOperator = ConditionOperator.kAnd;
			Conditions = new string[]{ condition };
		}
		public ShowIfAttributeBase( ConditionOperator conditionOperator, params string[] conditions)
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
