
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class CurveRangeTest : MonoBehaviour
	{
		[CurveRange( -1, -1, 1, 1, ConstColor.kRed)]
		public AnimationCurve curve0;
		[CurveRange( ConstColor.kOrange)]
		public AnimationCurve curve1;
		[CurveRange( 0, 0, 10, 10)]
		public AnimationCurve curve2;
		[CurveRange( 0, 0, 1, 1, ConstColor.kGreen)]
		public AnimationCurve curve3;
		[CurveRange( 0, 0, 5, 5, ConstColor.kBlue)]
		public AnimationCurve curve4;
	}
#pragma warning restore 414
}
