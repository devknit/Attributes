
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class InputAxisTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , InputAxis]
		string inputAxis0 = default;
		[SerializeField, InputAxis]
		internal string inputAxis1 = default;
		[InputAxis]
		public string inputAxis2;
	#pragma warning restore 414
	}
}
