
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class MinValueAttribute : ValidatorAttribute
	{
		public MinValueAttribute( float minValue)
		{
			MinValue = minValue;
		}
		public MinValueAttribute( int minValue)
		{
			MinValue = minValue;
		}
		public float MinValue
		{
			get;
			private set;
		}
	}
}
