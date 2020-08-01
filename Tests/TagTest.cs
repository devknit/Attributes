
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class TagTest : MonoBehaviour
	{
		[Tag]
		public string tag0;
		[Tag]
		public string tag1;
		[Tag]
		public string tag2;
	}
#pragma warning restore 414
}
