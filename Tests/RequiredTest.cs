
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class RequiredTest : MonoBehaviour
	{
		[Required]
		public Transform trans0;
		[SerializeField]
		RequiredNest1 nest1;
	}
	[System.Serializable]
	public class RequiredNest1
	{
		[AllowNesting, Required( "trans1 is invalid custom message")]
		public Transform trans1;
	}
#pragma warning restore 414
}
