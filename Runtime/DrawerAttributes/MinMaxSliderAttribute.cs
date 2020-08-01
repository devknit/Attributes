
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class MinMaxSliderAttribute : DrawerAttribute
	{
		public MinMaxSliderAttribute( float minValue, float maxValue, float padding=0.0f)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Padding = padding;
		}
		public MinMaxSliderAttribute( int minValue, int maxValue, int padding=0)
		{
			MinValue = minValue;
			MaxValue = maxValue;
			Padding = padding;
		}
		public float MinValue
		{
			get;
			private set;
		}
		public float MaxValue
		{
			get;
			private set;
		}
		public float Padding
		{
			get;
			private set;
		}
	}
}
