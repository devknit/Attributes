
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class LabelAttribute : MetaAttribute
	{
		public LabelAttribute( string label)
		{
			Label = label;
		}
		public string Label
		{
			get;
			private set;
		}
	}
}
