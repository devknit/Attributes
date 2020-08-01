
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public sealed class LineAttribute : DrawerAttribute
	{
		public LineAttribute( float thickness=1.0f, ConstColor color=default)
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
