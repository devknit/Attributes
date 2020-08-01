
using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Attributes.Editor
{
	public abstract class SpecialCasePropertyDrawerBase
	{
		public void OnGUI( SerializedProperty property)
		{
			if( property.IsVisible() != false)
			{
				ValidatorAttribute[] validatorAttributes = property.GetAttributes<ValidatorAttribute>();
				foreach( var validatorAttribute in validatorAttributes)
				{
					validatorAttribute.GetValidator().ValidateProperty( property);
				}

				EditorGUI.BeginChangeCheck();
				GUI.enabled = property.IsEnabled();
				OnInternalGUI( property, new GUIContent( property.GetLabel()));
				GUI.enabled = true;

				if( EditorGUI.EndChangeCheck() != false)
				{
					PropertyUtility.CallOnValueChangedCallbacks( property);
				}
			}
		}
		protected abstract void OnInternalGUI( SerializedProperty property, GUIContent label);
	}
	public static class SpecialCaseDrawerAttributeExtensions
	{
		static SpecialCaseDrawerAttributeExtensions()
		{
			drawersByAttributeType = new Dictionary<Type, SpecialCasePropertyDrawerBase>();
			drawersByAttributeType[ typeof( ReorderableListAttribute)] = ReorderableListPropertyDrawer.Instance;
		}
		public static SpecialCasePropertyDrawerBase GetDrawer( this SpecialCaseDrawerAttribute attr)
		{
			SpecialCasePropertyDrawerBase drawer;
			
			if( drawersByAttributeType.TryGetValue(attr.GetType(), out drawer) != false)
			{
				return drawer;
			}
			return null;
		}
		static Dictionary<Type, SpecialCasePropertyDrawerBase> drawersByAttributeType;
	}
}
