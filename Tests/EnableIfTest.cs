
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class EnableIfTest : MonoBehaviour
	{
		public bool enable1;
		public bool enable2;

		[ReorderableList, EnableIf( ConditionOperator.kAnd, "enable1", "enable2")]
		public int[] enableIfAll;
		[ReorderableList, EnableIf( ConditionOperator.kOr, "enable1", "enable2")]
		public int[] enableIfAny;
		[SerializeField]
		EnableIfNest1 nest1;
	}
	[System.Serializable]
	public class EnableIfNest1
	{
		public bool enable1;
		public bool enable2;
		public bool Enable1{ get { return enable1; } }
		public bool Enable2{ get { return enable2; } }
		
		[AllowNesting, EnableIf( ConditionOperator.kAnd, "Enable1", "Enable2")]
		public int enableIfAll = 1;
		[AllowNesting, EnableIf( ConditionOperator.kOr, "Enable1", "Enable2")]
		public int enableIfAny = 2;
		[SerializeField]
		EnableIfNest2 nest2;
	}
	[System.Serializable]
	public class EnableIfNest2
	{
		public bool enable1;
		public bool enable2;
		public bool IsEnable1() { return enable1; }
		public bool IsEnable2() { return enable2; }
		
		[MinMaxSlider( 0.0f, 1.0f), EnableIf( ConditionOperator.kAnd, "IsEnable1", "IsEnable2")]
		public Vector2 enableIfAll;
		[MinMaxSlider( 0.0f, 1.0f), EnableIf( ConditionOperator.kOr, "IsEnable1", "IsEnable2")]
		public Vector2 enableIfAny;
	}
#pragma warning restore 414
}
