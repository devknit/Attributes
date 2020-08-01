
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class ReadOnlyTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , ReadOnly]
		int readOnlyInteger = 16;
		[SerializeField, ReadOnly]
		internal float readOnlyFloat = 3.14159265f;
		[ReadOnly]
		public string readOnlyString;
	#pragma warning restore 414
	}
}
