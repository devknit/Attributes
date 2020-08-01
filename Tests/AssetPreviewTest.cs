
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class AssetPreviewTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , AssetPreview]
		public Sprite sprite0;
		[SerializeField , AssetPreview( 96, 96)]
		public GameObject prefab0;
		[SerializeField , AssetPreview( 128, 128)]
		public Transform test0;
	#pragma warning restore 414
	}
}
