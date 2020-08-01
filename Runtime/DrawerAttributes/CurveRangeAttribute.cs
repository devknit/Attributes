
using System;
using UnityEngine;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class CurveRangeAttribute : DrawerAttribute
	{
		public CurveRangeAttribute(Vector2 min, Vector2 max, ConstColor color=default)
		{
			Min = min;
			Max = max;
			Color = color;
		}
		public CurveRangeAttribute( ConstColor color) : this( Vector2.zero, Vector2.one, color)
		{
		}
		public CurveRangeAttribute( float minX, float minY, float maxX, float maxY, ConstColor color=default) : this( new Vector2( minX, minY), new Vector2( maxX, maxY), color)
		{
		}
		public Vector2 Min
		{
			get;
			private set;
		}
		public Vector2 Max
		{
			get;
			private set;
		}
		public ConstColor Color
		{
			get;
			private set;
		}
	}
}
