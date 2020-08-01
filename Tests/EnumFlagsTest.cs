
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
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
		[EnumFlags]
		public TestEnum flags0;
		[EnumFlags]
		public TestEnum flags1;
		[EnumFlags]
		public TestEnum flags2;
	}
#pragma warning restore 414
}
