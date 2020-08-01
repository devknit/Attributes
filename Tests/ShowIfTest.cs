
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ShowIfTest : MonoBehaviour
	{
		public bool show1;
		public bool show2;

		[ReorderableList, ShowIf( ConditionOperator.kAnd, "show1", "show2")]
		public int[] showIfAll;
		[ReorderableList, ShowIf( ConditionOperator.kOr, "show1", "show2")]
		public int[] showIfAny;
		[SerializeField]
		ShowIfNest1 nest1;
	}
	[System.Serializable]
	public class ShowIfNest1
	{
		public bool show1;
		public bool show2;
		public bool Show1{ get { return show1; } }
		public bool Show2{ get { return show2; } }
		
		[AllowNesting, ShowIf( ConditionOperator.kAnd, "Show1", "Show2")]
		public int showIfAll = 1;
		[AllowNesting, ShowIf( ConditionOperator.kOr, "Show1", "Show2")]
		public int showIfAny = 2;
		[SerializeField]
		ShowIfNest2 nest2;
	}
	[System.Serializable]
	public class ShowIfNest2
	{
		public bool show1;
		public bool show2;
		public bool IsShow1() { return show1; }
		public bool IsShow2() { return show2; }
		
		[MinMaxSlider( 0.0f, 1.0f), ShowIf( ConditionOperator.kAnd, "IsShow1", "IsShow2")]
		public Vector2 showIfAll;
		[MinMaxSlider( 0.0f, 1.0f), ShowIf( ConditionOperator.kOr, "IsShow1", "IsShow2")]
		public Vector2 showIfAny;
	}
#pragma warning restore 414
}
