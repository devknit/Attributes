
using UnityEngine;

namespace Attributes.Tests
{
	public enum TestEnum : int
	{
		kNone = 0,
		kB = 1 << 0,
		kC = 1 << 1,
		kD = 1 << 2,
		kE = 1 << 3,
		kF = 1 << 4,
		kAll = ~0
	}
	public sealed class EnumFlagsTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField , EnumFlags]
		TestEnum flags0 = default;
		[SerializeField, EnumFlags]
		internal TestEnum flags1 = default;
		[EnumFlags]
		public TestEnum flags2;
	#pragma warning restore 414
	}
}
