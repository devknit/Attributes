
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class HideIfTest : MonoBehaviour
	{
		public bool hide1;
		public bool hide2;

		[ReorderableList, HideIf( ConditionOperator.kAnd, "hide1", "hide2")]
		public int[] hideIfAll;
		[ReorderableList, HideIf( ConditionOperator.kOr, "hide1", "hide2")]
		public int[] hideIfAny;
		[SerializeField]
		HideIfNest1 nest1;
	}
	[System.Serializable]
	public class HideIfNest1
	{
		public bool hide1;
		public bool hide2;
		public bool Hide1{ get { return hide1; } }
		public bool Hide2{ get { return hide2; } }
		
		[AllowNesting, HideIf( ConditionOperator.kAnd, "Hide1", "Hide2")]
		public int hideIfAll = 1;
		[AllowNesting, HideIf( ConditionOperator.kOr, "Hide1", "Hide2")]
		public int hideIfAny = 2;
		[SerializeField]
		HideIfNest2 nest2;
	}
	[System.Serializable]
	public class HideIfNest2
	{
		public bool hide1;
		public bool hide2;
		public bool IsHide1() { return hide1; }
		public bool IsHide2() { return hide2; }
		
		[MinMaxSlider( 0.0f, 1.0f), HideIf( ConditionOperator.kAnd, "IsHide1", "IsHide2")]
		public Vector2 hideIfAll;
		[MinMaxSlider( 0.0f, 1.0f), HideIf( ConditionOperator.kOr, "IsHide1", "IsHide2")]
		public Vector2 hideIfAny;
	}
#pragma warning restore 414
}
