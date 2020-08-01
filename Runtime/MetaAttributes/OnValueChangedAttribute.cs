
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public sealed class OnValueChangedAttribute : MetaAttribute
	{
		public OnValueChangedAttribute( string callbackName)
		{
			CallbackName = callbackName;
		}
		public string CallbackName
		{
			get;
			private set;
		}
	}
}
