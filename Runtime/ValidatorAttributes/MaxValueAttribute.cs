﻿
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class MaxValueAttribute : ValidatorAttribute
	{
		public MaxValueAttribute( float maxValue)
		{
			MaxValue = maxValue;
		}
		public MaxValueAttribute( int maxValue)
		{
			MaxValue = maxValue;
		}
		public float MaxValue
		{
			get;
			private set;
		}
	}
}
