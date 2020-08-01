
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class MinMaxSliderTest : MonoBehaviour
	{
		[MinMaxSlider( 0.0f, 1.0f, 0.2f)]
		public Vector2 minMaxSlider0 = new Vector2( 0.2f, 0.8f);
		[MinMaxSlider( -1.0f, 1.0f)]
		public Vector2 minMaxSlider1 = new Vector2( 0.0f, 0.0f);
		[MinMaxSlider( 1, 256)]
		public Vector2Int minMaxSlider2 = new Vector2Int( 16, 128);
		[MinMaxSlider( -128, 128, 32)]
		public Vector2Int minMaxSlider3 = new Vector2Int( -32, 32);
	}
#pragma warning restore 414
}
