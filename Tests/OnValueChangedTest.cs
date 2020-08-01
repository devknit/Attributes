
using UnityEngine;

namespace Attributes.Tests
{
#pragma warning disable 414
	public sealed class OnValueChangedTest : MonoBehaviour
	{
		[OnValueChanged( "OnValueChangedMethod1")]
		[OnValueChanged( "OnValueChangedMethod2")]
		public int int0;

		private void OnValueChangedMethod1()
		{
			Debug.LogFormat( "int0: {0}", int0);
		}
		private void OnValueChangedMethod2()
		{
			Debug.LogFormat( "int0: {0}", int0);
		}
		
		[SerializeField]
		OnValueChangedNest1 nest1;
	}
	[System.Serializable]
	public class OnValueChangedNest1
	{
		[AllowNesting, OnValueChanged("OnValueChangedMethod")]
		public int int1;

		private void OnValueChangedMethod()
		{
			Debug.LogFormat( "int1: {0}", int1);
		}
	}
#pragma warning restore 414
}
