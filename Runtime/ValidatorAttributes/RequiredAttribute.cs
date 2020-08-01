
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class RequiredAttribute : ValidatorAttribute
	{
		public RequiredAttribute( string message=default)
		{
			Message = message;
		}
		public string Message
		{
			get;
			private set;
		}
	}
}
