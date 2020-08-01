
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public sealed class LineAttribute : DrawerAttribute
	{
		public const float kDefaultThickness = 1.0f;
		public const ConstColor kDefaultColor = default;
		
		public LineAttribute( float thickness=kDefaultThickness, ConstColor color=kDefaultColor)
		{
			Thickness = thickness;
			Color = color;
		}
		public float Thickness
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
