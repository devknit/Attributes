
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class SceneTest : MonoBehaviour
	{
		[Scene]
		public string scene0;
		[Scene]
		public int scene1;
		[Scene]
		public string scene2;
	}
#pragma warning restore 414
}
