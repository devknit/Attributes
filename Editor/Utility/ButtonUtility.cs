﻿
using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public static class ButtonUtility
	{
		public static bool IsEnabled( Object target, MethodInfo method)
		{
			EnableIfAttributeBase enableIfAttribute = method.GetCustomAttribute<EnableIfAttributeBase>();
			if( enableIfAttribute == null)
			{
				return true;
			}

			List<bool> conditionValues = PropertyUtility.GetConditionValues( target, enableIfAttribute.Conditions);
			if( conditionValues.Count > 0)
			{
				return PropertyUtility.GetConditionsFlag(conditionValues, enableIfAttribute.ConditionOperator, enableIfAttribute.Inverted);
			}
			string message = string.Format( 
				"{0} が機能するにはbool型のフィールド/プロパティ/メソッドが必要です",
				enableIfAttribute.GetType().Name);
			Debug.LogWarning( message, target);
			
			return false;
		}
		public static bool IsVisible( Object target, MethodInfo method)
		{
			ShowIfAttributeBase showIfAttribute = method.GetCustomAttribute<ShowIfAttributeBase>();
			if( showIfAttribute == null)
			{
				return true;
			}

			List<bool> conditionValues = PropertyUtility.GetConditionValues( target, showIfAttribute.Conditions);
			if( conditionValues.Count > 0)
			{
				return PropertyUtility.GetConditionsFlag(conditionValues, showIfAttribute.ConditionOperator, showIfAttribute.Inverted);
			}
			string message = string.Format( 
				"{0} が機能するにはbool型のフィールド/プロパティ/メソッドが必要です",
				showIfAttribute.GetType().Name);
			Debug.LogWarning( message, target);
			
			return false;
		}
	}
}
