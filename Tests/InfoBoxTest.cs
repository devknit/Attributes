
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class InfoBoxTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField, InfoBox( "Normal", InfoBoxType.kNormal)]
		int normal = default;
		[SerializeField, InfoBox( "Warning", InfoBoxType.kWarning)]
		internal int warning = default;
		[InfoBox( "Error", InfoBoxType.kError)]
		public int error;
	#pragma warning restore 414
	}
}
