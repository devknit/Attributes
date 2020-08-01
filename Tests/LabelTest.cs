
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class LabelTest : MonoBehaviour
	{
		[SerializeField, Label( "Label 0")]
		internal int int0 = default;
		[SerializeField]
		internal LabelNest1 nest1 = default;
	}
	[System.Serializable]
	public class LabelNest1
	{
		[SerializeField, Label( "Label 1"), AllowNesting] /*!< 明示的に入れ子を許可する必要があります */
		internal int int1 = default;
		[SerializeField]
		internal LabelNest2 nest2 = default;
	}
	[System.Serializable]
	public class LabelNest2
	{
		[Label( "Label 2"), MinMaxSlider( 0, 1)] /*!< MinMaxSlider でマークされているため AllowNesting 属性は必要ありません */
		public Vector2 vector2;
	}
}
