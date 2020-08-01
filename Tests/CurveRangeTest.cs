
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class CurveRangeTest : MonoBehaviour
	{
	#pragma warning disable 414
		[SerializeField, CurveRange( -1, -1, 1, 1, ConstColor.kRed)]
		AnimationCurve curve0 = default;
		
		[SerializeField, CurveRange( ConstColor.kOrange)]
		internal AnimationCurve curve1 = default;
		
		[CurveRange( 0, 0, 10, 10)]
		public AnimationCurve curve2 = default;
		
		[SerializeField, CurveRange( 0, 0, 1, 1, ConstColor.kGreen)]
		AnimationCurve curve3 = default;
		
		[CurveRange( 0, 0, 5, 5, ConstColor.kBlue)]
		public AnimationCurve curve4 = default;
	#pragma warning restore 414
	}
}
