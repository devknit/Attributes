
using System;

namespace Attributes
{
	[AttributeUsage( AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class AssetPreviewAttribute : DrawerAttribute
	{
		public AssetPreviewAttribute( int width=64, int height=64)
		{
			Width = width;
			Height = height;
		}
		public int Width
		{
			get;
			private set;
		}
		public int Height
		{
			get;
			private set;
		}
	}
}
