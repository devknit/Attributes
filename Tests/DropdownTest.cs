
using UnityEngine;
using System.Collections.Generic;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class DropdownTest : MonoBehaviour
	{
		[Dropdown( "kIntValues")]
		public int intValue;
		[Dropdown( "StringValues")]
		public string stringValue;
		[Dropdown( "GetVectorValues")]
		public Vector3 vector3Value;
		[Dropdown( "EnumValues")]
		public TestEnum enumValue;

		static readonly int[] kIntValues = new []{ 1, 2, 3 };

		List<string> StringValues
		{
			get{ return new List<string>(){ "A", "B", "C" }; }
		}
		DropdownList<Vector3> GetVectorValues()
		{
			return new DropdownList<Vector3>()
			{
				{ "Right", Vector3.right },
				{ "Up", Vector3.up },
				{ "Forward", Vector3.forward }
			};
		}
		TestEnum[] EnumValues
		{
			get{ return new []{ TestEnum.kC, TestEnum.kE }; }
		}
	}
#pragma warning restore 414
}
