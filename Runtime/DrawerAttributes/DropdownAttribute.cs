
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class DropdownAttribute : DrawerAttribute
	{
		public DropdownAttribute( string valuesName)
		{
			ValuesName = valuesName;
		}
		public string ValuesName
		{
			get;
			private set;
		}
	}
}
