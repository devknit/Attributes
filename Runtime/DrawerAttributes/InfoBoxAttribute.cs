
using System;

namespace Attributes
{
	public enum InfoBoxType
	{
		kNormal,
		kWarning,
		kError
	}
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = true, Inherited = true)]
	public sealed class InfoBoxAttribute : DrawerAttribute
	{
		public InfoBoxAttribute( string text, InfoBoxType type=default)
		{
			Text = text;
			Type = type;
		}
		public string Text
		{
			get;
			private set;
		}
		public InfoBoxType Type
		{
			get;
			private set;
		}
	}
}
