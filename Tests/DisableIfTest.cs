
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class DisableIfTest : MonoBehaviour
	{
		public bool disable1;
		public bool disable2;

		[ReorderableList, DisableIf( ConditionOperator.kAnd, "disable1", "disable2")]
		public int[] disableIfAll;
		[ReorderableList, DisableIf( ConditionOperator.kOr, "disable1", "disable2")]
		public int[] disableIfAny;
		[SerializeField]
		DisableIfNest1 nest1;
	}
	[System.Serializable]
	public class DisableIfNest1
	{
		public bool disable1;
		public bool disable2;
		public bool Disable1{ get { return disable1; } }
		public bool Disable2{ get { return disable2; } }
		
		[AllowNesting, DisableIf( ConditionOperator.kAnd, "Disable1", "Disable2")]
		public int disableIfAll = 1;
		[AllowNesting, DisableIf( ConditionOperator.kOr, "Disable1", "Disable2")]
		public int disableIfAny = 2;
		[SerializeField]
		DisableIfNest2 nest2;
	}
	[System.Serializable]
	public class DisableIfNest2
	{
		public bool disable1;
		public bool disable2;
		public bool IsDisable1() { return disable1; }
		public bool IsDisable2() { return disable2; }
		
		[MinMaxSlider( 0.0f, 1.0f), DisableIf( ConditionOperator.kAnd, "IsDisable1", "IsDisable2")]
		public Vector2 disableIfAll;
		[MinMaxSlider( 0.0f, 1.0f), DisableIf( ConditionOperator.kOr, "IsDisable1", "IsDisable2")]
		public Vector2 disableIfAny;
	}
#pragma warning restore 414
}
