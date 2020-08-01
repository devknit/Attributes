
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class AllowNestingAttribute : DrawerAttribute
	{
	}
}
