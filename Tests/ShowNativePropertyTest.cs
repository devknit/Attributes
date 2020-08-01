
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ShowNativePropertyTest : MonoBehaviour
	{
		[ShowNativeProperty]
		Transform Transform
		{
			get{ return transform; }
		}
		[ShowNativeProperty]
		Transform ParentTransform
		{
			get{ return transform.parent; }
		}
	}
#pragma warning restore 414
}
