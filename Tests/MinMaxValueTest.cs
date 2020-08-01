
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class MinMaxValueTest : MonoBehaviour
	{
		[MinValue( 0)]
		public int min0;
		[MaxValue( 0)]
		public int max0;
		[MinValue( 0), MaxValue( 1)]
		public float range01;
		[SerializeField]
		MinMaxValueNest1 nest1;
	}	
	[System.Serializable]
	public class MinMaxValueNest1
	{
		[AllowNesting, MinValue( 0)] /*!< 明示的に入れ子を許可する必要があります */
		public int min0;
		[AllowNesting, MaxValue( 0)] /*!< 明示的に入れ子を許可する必要があります */
		public int max0;
		[AllowNesting, MinValue( 0), MaxValue( 1)] /*!< 明示的に入れ子を許可する必要があります */
		public float range01;
	}
#pragma warning restore 414
}
