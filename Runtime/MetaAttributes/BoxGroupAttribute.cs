
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class BoxGroupAttribute : MetaAttribute
	{
		public BoxGroupAttribute( string name = default)
		{
			Name = name;
		}
		public string Name
		{
			get;
			private set;
		}
	}
}
