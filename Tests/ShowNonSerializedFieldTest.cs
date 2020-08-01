
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ShowNonSerializedFieldTest : MonoBehaviour
	{
		[ShowNonSerializedField]
		string stringValue;
		
		[ShowNonSerializedField]
		int intValue = 10;

		[ShowNonSerializedField]
		const float kFloatValue = 3.14159f;

		[ShowNonSerializedField]
		static readonly Vector3 kVector3Value = Vector3.one;
	}
#pragma warning restore 414
}
