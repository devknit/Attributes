
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ReadOnlyTest : MonoBehaviour
	{
		[ReadOnly]
		public int readOnlyInteger = 16;
		[ReadOnly]
		public float readOnlyFloat = 3.14159265f;
		[ReadOnly]
		public string readOnlyString;
	}
#pragma warning restore 414
}
