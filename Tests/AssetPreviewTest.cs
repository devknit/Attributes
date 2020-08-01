
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class AssetPreviewTest : MonoBehaviour
	{
		[AssetPreview]
		public Sprite sprite0;
		[AssetPreview( 96, 96)]
		public GameObject prefab0;
		[AssetPreview( 128, 128)]
		public Transform test0;
	}
#pragma warning restore 414
}
