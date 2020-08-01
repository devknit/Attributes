
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class InputAxisTest : MonoBehaviour
	{
		[InputAxis]
		public string inputAxis0;
		[InputAxis]
		public string inputAxis1;
		[InputAxis]
		public string inputAxis2;
	}
#pragma warning restore 414
}
