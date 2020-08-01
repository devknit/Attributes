
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class LabelTest : MonoBehaviour
	{
		[Label( "Label 0")]
		public int int0;
		[SerializeField]
		LabelNest1 nest1;
	}
	[System.Serializable]
	public class LabelNest1
	{
		[AllowNesting, Label( "Label 1")] /*!< 明示的に入れ子を許可する必要があります */
		public int int1;
		[AllowNesting, Label( "Label 2")] /*!< 明示的に入れ子を許可する必要があります */
		public int int2;
	}
#pragma warning restore 414
}
