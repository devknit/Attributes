
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class ValidateInputAttribute : ValidatorAttribute
	{
		public ValidateInputAttribute( string callbackName, string message=default)
		{
			CallbackName = callbackName;
			Message = message;
		}
		public string CallbackName
		{
			get;
			private set;
		}
		public string Message
		{
			get;
			private set;
		}
	}
}
