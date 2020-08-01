
using UnityEngine;

namespace Attributes.Tests
{
	public sealed class LineTest : MonoBehaviour
	{
	#pragma warning disable 414
		[Line( color: ConstColor.kBlack), Header( "Black")]
		[Line( color: ConstColor.kBlue), Header( "Blue")]
		[Line( color: ConstColor.kGray), Header( "Gray")]
		[Line( color: ConstColor.kGreen), Header( "Green")]
		[Line( color: ConstColor.kIndigo), Header( "Indigo")]
		[Line( color: ConstColor.kOrange), Header( "Orange")]
		[Line( color: ConstColor.kPink), Header( "Pink")]
		[Line( color: ConstColor.kRed), Header( "Red")]
		[Line( color: ConstColor.kViolet), Header( "Violet")]
		[Line( color: ConstColor.kWhite), Header( "White")]
		[Line( color: ConstColor.kYellow), Header( "Yellow")]
		[Line( 10.0f), Header( "Thickness 10")]
		public int line0;
	#pragma warning restore 414
	}
}
