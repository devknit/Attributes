
using UnityEngine;
using System.Collections.Generic;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ReorderableListTest : MonoBehaviour
	{
		[ReorderableList]
		public int[] intArray;
		[ReorderableList]
		public List<Vector3> vectorList;
		[ReorderableList]
		public List<SomeStruct> structList;
	}
	[System.Serializable]
	public struct SomeStruct
	{
		[AllowNesting, MinValue( 0)]
		public int Int;
		[ReadOnly]
		public float Float;
		[MinMaxSlider( 0.0f, 1.0f)]
		public Vector2 minMaxSlider0;
	}
#pragma warning restore 414
}
