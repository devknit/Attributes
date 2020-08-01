
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class TagTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , Tag]
		string tag0 = default;
		[SerializeField, Tag]
		internal string tag1 = default;
		[Tag]
		public string tag2;
	#pragma warning restore 414
	}
}
