
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ShowNativePropertyAttribute : SpecialCaseDrawerAttribute
	{
	}
}
