
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class ValidateInputTest : MonoBehaviour
	{
		[ValidateInput( "NotZero0", "int0 は 0 以外でなければいけません")]
		public int int0;
		
		bool NotZero0( int value)
		{
			return value != 0;
		}
		
		[SerializeField]
		ValidateInputNest1 nest1;
	}
	[System.Serializable]
	public class ValidateInputNest1
	{
		[AllowNesting, ValidateInput("NotNullOrEmpty1")]
		public string string1;

		bool NotNullOrEmpty1( string value)
		{
			return string.IsNullOrEmpty( value) == false;
		}
	}
#pragma warning restore 414
}
