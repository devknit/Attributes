
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class InfoBoxTest : MonoBehaviour
	{
		[InfoBox( "Normal", InfoBoxType.kNormal)]
		public int normal;
		[InfoBox( "Warning", InfoBoxType.kWarning)]
		public int warning;
		[InfoBox( "Error", InfoBoxType.kError)]
		public int error;
	}
#pragma warning restore 414
}
