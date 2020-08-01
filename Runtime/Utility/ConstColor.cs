
using UnityEngine;

namespace Attributes
{
	public enum ConstColor
	{
		kWhite,
		kBlack,
		kGray,
		kRed,
		kPink,
		kOrange,
		kYellow,
		kGreen,
		kBlue,
		kIndigo,
		kViolet
	}
	public static class ConstColorExtensions
	{
		public static Color GetColor( this ConstColor color)
		{
			switch( color)
			{
				case ConstColor.kWhite:		return new Color32( 255, 255, 255, 255);
				case ConstColor.kBlack:		return new Color32( 0, 0, 0, 255);
				case ConstColor.kGray:		return new Color32( 128, 128, 128, 255);
				case ConstColor.kRed:		return new Color32( 255, 0, 63, 255);
				case ConstColor.kPink:		return new Color32( 255, 152, 203, 255);
				case ConstColor.kOrange:	return new Color32( 255, 128, 0, 255);
				case ConstColor.kYellow:	return new Color32( 255, 211, 0, 255);
				case ConstColor.kGreen:		return new Color32( 98, 200, 79, 255);
				case ConstColor.kBlue:		return new Color32( 0, 135, 189, 255);
				case ConstColor.kIndigo:	return new Color32( 75, 0, 130, 255);
				case ConstColor.kViolet:	return new Color32( 128, 0, 255, 255);
			}
			return new Color32( 0, 0, 0, 255);
		}
	}
}