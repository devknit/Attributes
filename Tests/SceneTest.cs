
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class SceneTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , Scene]
		string scene0 = default;
		[SerializeField, Scene]
		internal int scene1 = default;
		[Scene]
		public string scene2;
	#pragma warning restore 414
	}
}
