
using System;

namespace Attributes
{
	public enum ButtonEnableMode
	{
		kAlways,
		kEditor,
		kPlaymode
	}
	[AttributeUsage( AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class ButtonAttribute : SpecialCaseDrawerAttribute
	{
		public ButtonAttribute( string label=default, ButtonEnableMode enabledMode=default)
		{
			this.Label = label;
			this.SelectedEnableMode = enabledMode;
		}
		public string Label
		{
			get;
			private set;
		}
		public ButtonEnableMode SelectedEnableMode
		{
			get;
			private set;
		}
	}
}
