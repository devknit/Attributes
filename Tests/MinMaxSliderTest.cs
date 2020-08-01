
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class MinMaxSliderTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , MinMaxSlider( 0.0f, 1.0f)]
		Vector2 minMaxSlider0 = new Vector2( 0.25f, 0.75f);
		[SerializeField, MinMaxSlider( 1, 256)]
		internal Vector2Int minMaxSlider1 = new Vector2Int( 16, 128);
		[MinMaxSlider( -1.0f, 1.0f)]
		public Vector2 minMaxSlider2 = new Vector2( 0.0f, 0.0f);
	#pragma warning restore 414
	}
}
